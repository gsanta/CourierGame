using System;
using System.Collections.Generic;
using Zenject;

namespace GamePlay
{
    public class TurnManager
    {
        private readonly ITurns playerCommandTurns;
        private readonly ITurns playerPlayTurns;
        private readonly ITurns pedestrianTurns;
        private readonly ITurns enemyTurns;
        private ITurns activeTurn;
        private List<ITurns> turns;

        public TurnManager([Inject(Id = "PlayerCommandTurns")] ITurns playerCommandTurns, [Inject(Id = "PlayerPlayTurns")] ITurns playerPlayTurns, [Inject(Id = "PedestrianTurns")] ITurns pedestrianTurns, [Inject(Id = "EnemyTurns")] ITurns enemyTurns)
        {
            this.playerCommandTurns = playerCommandTurns;
            this.playerPlayTurns = playerPlayTurns;
            this.pedestrianTurns = pedestrianTurns;
            this.enemyTurns = enemyTurns;
            turns = new List<ITurns>
            {
                playerCommandTurns,
                playerPlayTurns,
                pedestrianTurns,
                enemyTurns
            };
        }

        public event EventHandler TurnChanged;

        public bool IsPlayerCommandTurn()
        {
            return activeTurn == playerCommandTurns;
        }

        public bool IsPlayerPlayTurn()
        {
            return activeTurn == playerPlayTurns;
        }

        public bool IsEnemyTurn()
        {
            return activeTurn == enemyTurns;
        }

        public bool IsPedestrianTurn()
        {
            return activeTurn == pedestrianTurns;
        }

        public void Step()
        {
            if (activeTurn == null)
            {
                turns.ForEach(turn => turn.Reset());
                activeTurn = playerCommandTurns;
                TurnChanged?.Invoke(this, EventArgs.Empty);
                playerCommandTurns.Execute()
                    .Then(() => {
                        activeTurn = playerPlayTurns;
                        TurnChanged?.Invoke(this, EventArgs.Empty);
                        return playerPlayTurns.Execute();
                    })
                    .Then(() => {
                        activeTurn = pedestrianTurns;
                        TurnChanged?.Invoke(this, EventArgs.Empty);
                        return pedestrianTurns.Execute();
                    })
                    .Then(() =>
                    {
                        activeTurn = enemyTurns;
                        TurnChanged?.Invoke(this, EventArgs.Empty);
                        return enemyTurns.Execute();
                    })
                    .Then(() => {
                        activeTurn = null;
                        TurnChanged?.Invoke(this, EventArgs.Empty);
                        Step();
                    });
            } else
            {
                activeTurn.Step();
            }
        }
    }
}

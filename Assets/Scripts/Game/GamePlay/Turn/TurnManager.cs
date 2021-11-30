using System;
using System.Collections.Generic;
using Zenject;

namespace GamePlay
{
    public class TurnManager
    {
        private readonly ITurns playerCommandTurns;
        private readonly ITurns playerPlayTurns;
        private ITurns activeTurn;
        private List<ITurns> turns;

        public TurnManager([Inject(Id = "PlayerCommandTurns")] ITurns playerCommandTurns, [Inject(Id = "PlayerPlayTurns")] ITurns playerPlayTurns)
        {
            this.playerCommandTurns = playerCommandTurns;
            this.playerPlayTurns = playerPlayTurns;
            turns = new List<ITurns>
            {
                playerCommandTurns,
                playerPlayTurns,
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
                        activeTurn = null;
                        TurnChanged?.Invoke(this, EventArgs.Empty);
                        Step();
                    });
            } else
            {
                activeTurn.Step();
            }
        }

        public void ResetTurns()
        {
            activeTurn = null;
            turns.ForEach(turn => turn.Abort());
            Step();
        }

        public void Pause()
        {
            playerPlayTurns.Pause();
        }

        public void Resume()
        {
            playerPlayTurns.Resume();
        }
    }
}

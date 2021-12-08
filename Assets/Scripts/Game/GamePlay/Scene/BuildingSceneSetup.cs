using GameObjects;
using Cameras;
using Movement;
using UnityEngine;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class BuildingSceneSetup : MonoBehaviour, ISceneEntryPoint
    {
        [SerializeField]
        private GameObject playerPosition;
        [SerializeField]
        private GameObject enemyPosition;
        [SerializeField]
        private GameObject buildingGameObject;
        [SerializeField]
        private GameObject bottomLeftPoint;
        private PlayerStore playerStore;
        private WorldStore worldStore;
        private TurnManager turnManager;
        private CameraController cameraController;
        private GridSystem gridSystem;
        private SubsceneStore subsceneCharacterStore;

        [Inject]
        public void Construct(PlayerStore playerStore, WorldStore worldStore, TurnManager turnManager, CameraController cameraController, GridSystem gridSystem, SubsceneStore subsceneCharacterStore)
        {
            this.playerStore = playerStore;
            this.worldStore = worldStore;
            this.turnManager = turnManager;
            this.cameraController = cameraController;
            this.gridSystem = gridSystem;
            this.subsceneCharacterStore = subsceneCharacterStore;
        }

        public void Exit()
        {
            worldStore.CurrentMap = "Map1";
            var player = playerStore.GetActivePlayer();
            player.Agent.Active = true;
            player.transform.position = new Vector3(0, player.transform.position.y, 0);
        }

        void OnEnable()
        {
            turnManager.ResetTurns();
            cameraController.PanTo(buildingGameObject);
            gridSystem.SetBottomLeft(bottomLeftPoint);
            gridSystem.TileManager.UpdateTileVisibility();
            subsceneCharacterStore.GetCharacters().ForEach(character =>
            {
                character.Agent.Active = false;
                var newPos = playerPosition.transform.position;
                var tile = gridSystem.TileManager.GetRandomTile();
                character.transform.position = new Vector3(tile.CenterPoint.x, character.transform.position.y, tile.CenterPoint.z);

            });

        }

        public void Enter()
        {
            //worldStore.CurrentMap = "Building";
            //worldStore.ActiveSceneEntryPoint = this;

            //if (worldStore.BattleState != null)
            //{
            //    var player = worldStore.BattleState.Player;
            //    player.Agent.Active = true;
            //    var newPos = playerPosition.transform.position;
            //    player.transform.position = new Vector3(newPos.x, player.transform.position.y, newPos.z);

            //    var enemy = worldStore.BattleState.Enemy;
            //    enemy.Agent.Active = true;
            //    var enemyNewPos = enemyPosition.transform.position;
            //    enemy.transform.position = new Vector3(enemyNewPos.x, enemy.transform.position.y, enemyNewPos.z);

            //    turnManager.ResetTurns();
            //} else
            //{
            //    gridSystem.TileManager.UpdateTilePositions(new IntPos(0, 0));
            //    gridSystem.TileManager.UpdateTileVisibility();

            //    subsceneCharacterStore.GetAll().ForEach(character =>
            //    {
            //        character.Agent.Active = false;
            //        var newPos = playerPosition.transform.position;
            //        var tile = gridSystem.TileManager.GetRandomTile();
            //        character.transform.position = new Vector3(tile.CenterPoint.x, character.transform.position.y, tile.CenterPoint.z);

            //    });
            //    turnManager.ResetTurns();
            //    cameraController.PanTo(buildingGameObject);
            //    gridSystem.SetBottomLeft(bottomLeftPoint);
            //}
        }
    }
}

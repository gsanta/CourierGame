using GameObjects;
using Cameras;
using Movement;
using UnityEngine;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class BuildingSceneSetup : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPosition;
        [SerializeField]
        private GameObject enemyPosition;
        [SerializeField]
        private GameObject buildingGameObject;
        [SerializeField]
        private GameObject bottomLeftPoint;
        private CharacterStore characterStore;
        private WorldStore worldStore;
        private TurnManager turnManager;
        private CameraController cameraController;
        private GridSystem gridSystem;
        private SubsceneStore subsceneStore;

        [Inject]
        public void Construct(CharacterStore characterStore, WorldStore worldStore, TurnManager turnManager, CameraController cameraController, GridSystem gridSystem, SubsceneStore subsceneStore)
        {
            this.characterStore = characterStore;
            this.worldStore = worldStore;
            this.turnManager = turnManager;
            this.cameraController = cameraController;
            this.gridSystem = gridSystem;
            this.subsceneStore = subsceneStore;
        }

        public void Exit()
        {
            worldStore.CurrentMap = "Map1";
            var player = characterStore.GetActivePlayer();
            player.Agent.Active = true;
            player.transform.position = new Vector3(0, player.transform.position.y, 0);
        }

        void OnEnable()
        {
            cameraController.PanTo(buildingGameObject);
            gridSystem.SetBottomLeft(bottomLeftPoint);
            gridSystem.TileManager.UpdateTileVisibility();
            subsceneStore.GetCharacters().ForEach(character =>
            {
                character.Agent.Active = false;
                var newPos = playerPosition.transform.position;
                var tile = gridSystem.TileManager.GetRandomTile();
                Vector3 playerPos = playerPosition.transform.position;
                character.transform.position = new Vector3(playerPos.x, character.transform.position.y, playerPos.z);
            });
            turnManager.ResetTurns();
        }

        private void OnDisable()
        {
            subsceneStore.GetCharacters().ForEach(character =>
            {
                var origPos = subsceneStore.GetOrigPosition(character);
                character.transform.position = origPos;
            });
            cameraController.PanTo(characterStore.GetActivePlayer().gameObject);
            gridSystem.SetBottomLeft(gridSystem.GridConfig.bottomLeft);
            turnManager.ResetTurns();
            subsceneStore.Clear();
        }
    }
}

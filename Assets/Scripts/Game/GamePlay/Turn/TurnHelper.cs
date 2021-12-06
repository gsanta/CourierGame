using GameObjects;
using Cameras;

namespace GamePlay
{
    public class TurnHelper
    {
        private readonly GameObjects.PlayerStore playerStore;
        private readonly CameraController cameraController;

        public TurnHelper(GameObjects.PlayerStore playerStore, CameraController cameraController)
        {
            this.playerStore = playerStore;
            this.cameraController = cameraController;
        }

        public void ChangePlayer(GameCharacter player, bool follow)
        {
            playerStore.SetActivePlayer(player);
            if (follow)
            {
                cameraController.Follow(playerStore.GetActivePlayer());
            }
        }
    }
}

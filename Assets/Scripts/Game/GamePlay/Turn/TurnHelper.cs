using Bikers;
using Cameras;

namespace GamePlay
{
    public class TurnHelper
    {
        private readonly BikerStore playerStore;
        private readonly CameraController cameraController;

        public TurnHelper(BikerStore playerStore, CameraController cameraController)
        {
            this.playerStore = playerStore;
            this.cameraController = cameraController;
        }

        public void ChangePlayer(Player player, bool follow)
        {
            playerStore.SetActivePlayer(player);
            if (follow)
            {
                cameraController.Follow(playerStore.GetActivePlayer());
            }
        }
    }
}

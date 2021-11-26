using Bikers;
using Cameras;

namespace GamePlay
{
    public class TurnHelper
    {
        private readonly Bikers.PlayerStore playerStore;
        private readonly CameraController cameraController;

        public TurnHelper(Bikers.PlayerStore playerStore, CameraController cameraController)
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

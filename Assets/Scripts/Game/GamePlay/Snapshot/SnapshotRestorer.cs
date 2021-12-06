using GameObjects;

namespace GamePlay
{
    public class SnapshotRestorer
    {
        private readonly PlayerStore playerStore;

        public SnapshotRestorer(PlayerStore playerStore)
        {
            this.playerStore = playerStore;
        }

        public void Restore(Snapshot snapshot)
        {
            playerStore.GetActivePlayer().transform.position = snapshot.playerPosition;
        }
    }
}

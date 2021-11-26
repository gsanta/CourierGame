using Bikers;

namespace GamePlay
{
    public class SnapshotCreator
    {
        private readonly PlayerStore playerStore;

        public SnapshotCreator(PlayerStore playerStore)
        {
            this.playerStore = playerStore;
        }

        public Snapshot CreateSnapshot()
        {
            var activePlayer = playerStore.GetActivePlayer();
            Snapshot snapshot = new Snapshot();
            snapshot.playerPosition = activePlayer.transform.position;

            return snapshot;
        }
    }
}

using System.Collections.Generic;

namespace GamePlay
{
    public class SnapshotStore
    {
        private SnapshotRestorer restorer;
        private SnapshotCreator creator;
        private Dictionary<string, Snapshot> snapshots = new Dictionary<string, Snapshot>();

        public SnapshotStore(SnapshotRestorer restorer, SnapshotCreator creator)
        {
            this.restorer = restorer;
            this.creator = creator;
        }

        public void CreateSnapshot(string name)
        {
            snapshots.Add(name, creator.CreateSnapshot());
        }

        public void ApplySnapshot(string name)
        {
            restorer.Restore(snapshots[name]);
            snapshots.Remove(name);
        }
    }
}

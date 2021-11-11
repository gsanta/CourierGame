using Scenes;

namespace Stats
{
    public class MoneyStore : IDirty
    {
        private int money;
        private bool dirty = false;

        public void AddMoney(int money)
        {
            this.money += money;
            this.dirty = true;
        }

        public int GetMoney()
        {
            return money;
        }

        public bool IsDirty()
        {
            return dirty;
        }

        public void ClearDirty()
        {
            dirty = false;
        }
    }
}

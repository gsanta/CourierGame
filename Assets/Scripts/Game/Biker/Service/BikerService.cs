
namespace Bikers
{
    public class BikerService
    {

        private BikerStore bikerStore;

        public BikerService(BikerStore bikerStore)
        {
            this.bikerStore = bikerStore;
        }

        public Biker FindPlayRole()
        {
            return bikerStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY);
        }

        public Biker FindPlayOrFollowRole()
        {
            return bikerStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY || courier.GetCurrentRole() == CurrentRole.FOLLOW);
        }
    }
}
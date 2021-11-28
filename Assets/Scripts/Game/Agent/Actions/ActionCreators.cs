using Bikers;
using Pedestrians;

namespace Actions
{
    public class ActionCreators
    {
        public PlayerWalkIntoBuildingActionCreator PlayerWalkIntoBuildingActionCreator { get; set; }
        public PlayerWalkActionCreator PlayerWalkActionCreator { get; set; }
        public PedestrianWalkActionCreator PedestrianWalkActionCreator { get; set; }
        public EnemyWalkActionCreator EnemyWalkActionCreator { get; set; }
        public ExitBuildingPostAction ExitBuildingPostAction { get; set; }
        public EnterBuildingPostAction EnterBuildingPostAction { get; set; }
    }
}

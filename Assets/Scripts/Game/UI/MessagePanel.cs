using GamePlay;
using TMPro;
using UnityEngine;
using Worlds;
using Zenject;

namespace UI
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text message;

        private CanvasStore canvasStore;
        private TurnManager turnManager;
        private WorldStore worldStore;
        private SceneManagerHolder sceneManager;

        [Inject]
        public void Construct(CanvasStore canvasStore, TurnManager turnManager, WorldStore worldStore, SceneManagerHolder sceneManager)
        {
            this.canvasStore = canvasStore;
            this.turnManager = turnManager;
            this.worldStore = worldStore;
            this.sceneManager = sceneManager;
        }

        public void SetMessage(string message)
        {
            this.message.text = message;
        }

        public void TakeAction()
        {
            canvasStore.HidePanel(typeof(MessagePanel));
            worldStore.BattleState.Player.Agent.AbortAction();
            worldStore.BattleState.Enemy.Agent.AbortAction();
            sceneManager.D.EnterSubScene("Building");
        }

        public void SkipAction()
        {
            canvasStore.HidePanel(typeof(MessagePanel));
            worldStore.BattleState = null;
            turnManager.Resume();
        }
    }
}

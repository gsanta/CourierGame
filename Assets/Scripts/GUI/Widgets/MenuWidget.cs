
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class MenuWidget : MonoBehaviour
    {
        private PanelStore panelStore;

        [Inject]
        public void Construct(PanelStore panelStore)
        {
            this.panelStore = panelStore;
        }

        public void OnHomeButtonClicked()
        {
            panelStore.GetPanel<MenuPanel>(typeof(MenuPanel)).gameObject.SetActive(true);
        }
    }
}

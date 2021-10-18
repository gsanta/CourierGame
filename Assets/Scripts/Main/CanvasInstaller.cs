using GUI;
using UI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField]
        private StartDayPanel startDayPanel;
        [SerializeField]
        private DeliveryPanel deliveryPanel;
        [SerializeField]
        private TimelineController timelineController;
        [SerializeField]
        private BikerPanelController bikerPanelController;
        [SerializeField]
        private MenuWidget menuWidget;

        public override void InstallBindings()
        {
            Container.Bind<StartDayPanel>().FromInstance(startDayPanel).AsSingle();
            Container.Bind<DeliveryPanel>().FromInstance(deliveryPanel).AsSingle();
            Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
            Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
            Container.Bind<BikerPanelController>().FromInstance(bikerPanelController).AsSingle();
            Container.Bind<MenuWidget>().FromInstance(menuWidget).AsSingle();
        }
    }
}

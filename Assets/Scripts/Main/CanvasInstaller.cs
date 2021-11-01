using Controls;
using GUI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField]
        private StartDayPanel startDayPanel;
        [SerializeField]
        private DeliveryPanelController deliveryPanelController;
        [SerializeField]
        private TimelineController timelineController;
        [SerializeField]
        private BikerPanelController bikerPanelController;
        [SerializeField]
        private MenuWidget menuWidget;
        [SerializeField]
        private TopPanelController topPanelController;
        [SerializeField]
        private SelectionBox selectionBox;

        public override void InstallBindings()
        {
            Container.Bind<StartDayPanel>().FromInstance(startDayPanel).AsSingle();
            Container.Bind<DeliveryPanelController>().FromInstance(deliveryPanelController).AsSingle();
            Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
            Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
            Container.Bind<BikerPanelController>().FromInstance(bikerPanelController).AsSingle();
            Container.Bind<MenuWidget>().FromInstance(menuWidget).AsSingle();
            Container.Bind<TopPanelController>().FromInstance(topPanelController).AsSingle();
            Container.Bind<SelectionBox>().FromInstance(selectionBox).AsSingle();
        }
    }
}

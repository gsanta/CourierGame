using Controls;
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
        private DeliveryPanelController deliveryPanelController;
        [SerializeField]
        private TimelineController timelineController;
        [SerializeField]
        private BikerPanelHandler bikerPanelHandler;
        [SerializeField]
        private MenuWidget menuWidget;
        [SerializeField]
        private TopPanelController topPanelController;

        public override void InstallBindings()
        {
            Container.BindFactory<Object, BikerListItem, BikerListItem.Factory>().FromFactory<PrefabFactory<BikerListItem>>();
            Container.Bind<StartDayPanel>().FromInstance(startDayPanel).AsSingle();
            Container.Bind<DeliveryPanelController>().FromInstance(deliveryPanelController).AsSingle();
            Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
            Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
            Container.Bind<BikerPanelHandler>().FromInstance(bikerPanelHandler).AsSingle();
            Container.Bind<MenuWidget>().FromInstance(menuWidget).AsSingle();
            Container.Bind<TopPanelController>().FromInstance(topPanelController).AsSingle();
        }
    }
}

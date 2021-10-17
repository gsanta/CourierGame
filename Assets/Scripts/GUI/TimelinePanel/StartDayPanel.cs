using Scenes;
using Stats;
using System;
using TMPro;
using UI;
using UnityEngine;
using Zenject;

public class StartDayPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dayLabel;

    private Timer timer;
    private PanelStore panelStore;
    private SceneLoader sceneLoader;

    [Inject]
    public void Construct(Timer timer, PanelStore panelStore, SceneLoader sceneLoader)
    {
        this.timer = timer;
        this.panelStore = panelStore;
        this.sceneLoader = sceneLoader;

        timer.DayPassed += HandleDayPassed;
        timer.DayStarted += HandleDayStarted;
    }

    private void Start()
    {
        dayLabel.SetText("Day " + timer.CurrentDay);
        HidePanels();
    }

    public void OnStart()
    {
        timer.IsDayStarted = true;
        gameObject.SetActive(false);

        timer.IsDayStarted = true;
    }

    public void OnLoadScene()
    {
        this.sceneLoader.LoadMapScene(1);
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        timer.IsDayStarted = false;
        gameObject.SetActive(true);
        dayLabel.SetText("Day " + timer.CurrentDay);
    
        HidePanels();
    }

    private void HandleDayStarted(object sender, EventArgs e)
    {
        DisplayPanels();
    }

    private void HidePanels()
    {
        panelStore.HidePanel(panelStore.GetPanel<DeliveryPanel>(typeof(DeliveryPanel)).gameObject, 0f);
        panelStore.HidePanel(panelStore.GetPanel<BikerPanel>(typeof(BikerPanel)).gameObject, 0f);
    }

    private void DisplayPanels()
    {
        panelStore.ShowPanel(panelStore.GetPanel<DeliveryPanel>(typeof(DeliveryPanel)).gameObject, 0.5f);
        panelStore.ShowPanel(panelStore.GetPanel<BikerPanel>(typeof(BikerPanel)).gameObject, 1f);
    }
}

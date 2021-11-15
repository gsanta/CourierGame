using Controls;
using GamePlay;
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
    private CanvasStore panelStore;
    private SceneLoader sceneLoader;
    private TurnManager turnManager;

    [Inject]
    public void Construct(Timer timer, CanvasStore panelStore, SceneLoader sceneLoader, TurnManager turnManager)
    {
        this.timer = timer;
        this.panelStore = panelStore;
        this.sceneLoader = sceneLoader;
        this.turnManager = turnManager;

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
        gameObject.SetActive(false);

        timer.IsDayStarted = true;
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
        turnManager.Step();
    }

    private void HidePanels()
    {
        panelStore.HidePanel(panelStore.GetPanel<BikerPanelHandler>(typeof(BikerPanelHandler)).gameObject, 0f);
    }

    private void DisplayPanels()
    {
        panelStore.ShowPanel(panelStore.GetPanel<BikerPanelHandler>(typeof(BikerPanelHandler)).gameObject, 1f);
    }
}

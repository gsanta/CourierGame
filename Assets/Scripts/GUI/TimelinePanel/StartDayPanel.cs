using Stats;
using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartDayPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dayLabel;

    private Timer timer;
    private PanelManager panelManager;

    [Inject]
    public void Construct(Timer timer, PanelManager panelManager)
    {
        this.timer = timer;
        this.panelManager = panelManager;

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
        Debug.Log("loading scene");
        SceneManager.LoadSceneAsync("Block2", LoadSceneMode.Additive);
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
        panelManager.HidePanel(panelManager.GetPanel<DeliveryPanel>(typeof(DeliveryPanel)).gameObject, 0f);
        panelManager.HidePanel(panelManager.GetPanel<BikerPanel>(typeof(BikerPanel)).gameObject, 0f);
    }

    private void DisplayPanels()
    {
        panelManager.ShowPanel(panelManager.GetPanel<DeliveryPanel>(typeof(DeliveryPanel)).gameObject, 0.5f);
        panelManager.ShowPanel(panelManager.GetPanel<BikerPanel>(typeof(BikerPanel)).gameObject, 1f);
    }
}

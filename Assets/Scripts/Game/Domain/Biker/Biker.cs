using UnityEngine;
using UnityEngine.AI;
using Zenject;
using AI;
using Domain;
using System;

public class Biker : MonoBehaviour, ICourier
{
    [SerializeField]
    public Transform viewPoint;
    [SerializeField]
    private CharacterController charController;
    [SerializeField]
    public Transform packageHolder;

    public Package package;
    private string courierName;

    private CurrentRole currentRole = CurrentRole.NONE;
    private bool isPaused = false;

    private ICourierCallbacks courierCallbacks;

    private BikerAgentComponent agent;
    private BikerPlayComponent player;

    public BikerAgentComponent Agent {
        get => GetComponent<BikerAgentComponent>();
    }

    [Inject]
    public void Construct(ICourierCallbacks courierCallbacks)
    {
        this.courierCallbacks = courierCallbacks;
    }

    protected void Start()
    {
        agent = GetComponent<BikerAgentComponent>();
        player = GetComponent<BikerPlayComponent>();
    }

    public bool Paused { 
        get => isPaused;
        set {
            isPaused = value;
            UpdatePausedState();
        }
    }

    private void UpdatePausedState()
    {
        if (isPaused)
        {
            SetCurrentRole(CurrentRole.NONE);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            agent.SetActivated(false);
        } else {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            agent.SetActivated(true);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public string GetId()
    {
        return agent.GoapAgent.AgentId;
    }

    public string GetName()
    {
        return courierName;
    }

    public void SetName(string name)
    {
        this.courierName = name;
    }

    public void SetPackage(Package package)
    {
        this.package = package;
    }

    public Package GetPackage()
    {
        return package;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetCurrentRole(CurrentRole currentRole)
    {
        if (this.currentRole != currentRole)
        {
            this.currentRole = currentRole;

            if (currentRole == CurrentRole.PLAY)
            {
                InitPlayRole();
            } else
            {
                FinishPlayRole();
            }

            courierCallbacks.OnCurrentRoleChanged(this);
            CurrentRoleChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public CurrentRole GetCurrentRole()
    {
        return currentRole;
    }

    private void FinishPlayRole()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent.SetActivated(true);
        player.SetActivated(false);
    }

    private void InitPlayRole()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        
        player.SetActivated(true);
        agent.SetActivated(false);
    }

    public event EventHandler CurrentRoleChanged;

    public class Factory : PlaceholderFactory<UnityEngine.Object, Biker>
    {
    }
}

using UnityEngine;
using UnityEngine.AI;
using Zenject;
using AI;
using Domain;
using System;

public class Courier : GAgent, ICourier
{
    [SerializeField]
    private Transform viewPoint;
    [SerializeField]
    private CharacterController charController;
    [SerializeField]
    private float moveSpeed = 5f, runSpeed = 8f;
    [SerializeField]
    private float gravityMod = 2.5f;

    private Camera cam;
    public Package package;
    private string courierName;

    private CurrentRole currentRole = CurrentRole.NONE;
    private bool isPaused = false;

    private float activeMoveSpeed;
    private Vector3 moveDir, movement;

    private PlayerInputComponent playerInputComponent;
    private ICourierCallbacks courierCallbacks;

    [Inject]
    public void Construct(PlayerInputComponent playerInputComponent, PackageStore packageStore, ICourierCallbacks courierCallbacks)
    {
        this.playerInputComponent = playerInputComponent;
        this.courierCallbacks = courierCallbacks;
        playerInputComponent.SetPlayer(this);

        actions.Add(new AssignPackageAction(this, packageStore));
        actions.Add(new DeliverPackageAction(this));
        actions.Add(new PickUpPackageAction(this));
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + horizontal, transform.rotation.eulerAngles.z);

        moveDir = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        }
        else
        {
            activeMoveSpeed = moveSpeed;
        }

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y += Physics.gravity.y * gravityMod;
        charController.Move(movement * Time.deltaTime);
    }

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
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
            SetRunning(false);
        } else {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            SetRunning(true);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public string GetId()
    {
        return agentId;
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

    protected void Update()
    {
        if (currentRole == CurrentRole.PLAY)
        {
            Move();
        }
    }

    protected override void LateUpdate()
    {
        if (currentRole != CurrentRole.PLAY)
        {
            base.LateUpdate();
        }
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        if (currentRole != CurrentRole.NONE)
        {
            cam.transform.position = viewPoint.position;
            cam.transform.rotation = viewPoint.rotation;
        }
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
        SetRunning(true);
    }

    private void InitPlayRole()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        playerInputComponent.ActivateComponent();
        SetRunning(false);
    }

    protected override WorldStates GetWorldStates()
    {
        var worldStates = new WorldStates();

        var package = GetPackage();
        if (package)
        {
            if (package.Status == DeliveryStatus.ASSIGNED)
            {
                worldStates.AddState("isPackageReserved", 3);
                worldStates.AddState("isPackagePickedUp", 3);
            } else if (package.Status == DeliveryStatus.RESERVED)
            {
                worldStates.AddState("isPackageReserved", 3);
            }
        }
        return worldStates;
    }

    public event EventHandler CurrentRoleChanged;

    public class Factory : PlaceholderFactory<UnityEngine.Object, Courier>
    {
    }
}

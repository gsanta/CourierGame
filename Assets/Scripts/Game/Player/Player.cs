using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ICourier
{
    public GameObject minimapObject;

    private PlayerStore playerStore;
    private IWorldState worldState;
    private PlayerInputComponent playerInputComponent;

    [SerializeField] private Transform viewPoint;
    [SerializeField] private CharacterController charController;
    [SerializeField] private float moveSpeed = 5f, runSpeed = 8f;
    [SerializeField] private float gravityMod = 2.5f;

    [SerializeField] private string playerName;

    private Camera cam;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    private GameObject timelineImage;
    private Timer timer;

    private int elapsedTime;
    public int ElapsedTime { get => elapsedTime; }

    private Package package;

    public string Name { get => playerName; set => playerName = value; }

    public GameObject TimelineImage { get => timelineImage; set => timelineImage = value; }

    [Inject]
    public void Construct(PlayerInputComponent playerInputComponent, Timer timer, IWorldState worldState, PlayerStore playerStore)
    {
        this.playerInputComponent = playerInputComponent;
        playerInputComponent.SetPlayer(this);

        this.timer = timer;
        this.worldState = worldState;
        this.playerStore = playerStore;
    }

    public void ActivatePlayer()
    {
        if (playerInputComponent != null)
        {
            playerInputComponent.ActivateComponent();
            timer.Elapsed = elapsedTime;
        }
    }

    public void DeactivatePlayer()
    {
        playerInputComponent.DeactivateComnponent();
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (playerStore.GetActivePlayer() == this)
        {
            Move();
            elapsedTime = timer.Elapsed;
        }
    }

    private void LateUpdate()
    {
        if (playerStore.GetActivePlayer() == this)
        {
            SetCameraPosition();
            SetMinimapPosition();
        }
    }

    private void SetCameraPosition()
    {
        //cam.transform.position = viewPoint.position;
        //cam.transform.rotation = viewPoint.rotation;
    }

    private void SetMinimapPosition()
    {
        minimapObject.transform.position = transform.position;
        minimapObject.transform.rotation = transform.rotation;
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
        Debug.Log("y: " + transform.position.y);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public string GetId()
    {
        return Name;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetPackage(Package package)
    {
        this.package = package;
    }

    public Package GetPackage()
    {
        return package;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Player>
    {
    }
}

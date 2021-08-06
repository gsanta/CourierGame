using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject minimapObject;

    private PlayerStore playerStore;
    private ITimeProvider timeProvider;
    private IWorldState worldState;
    private PlayerInputComponent playerInputComponent;

    [SerializeField] private Transform viewPoint;
    [SerializeField] private CharacterController charController;
    [SerializeField] private float moveSpeed = 5f, runSpeed = 8f;
    [SerializeField] private string playerName;
    
    private Camera cam;   
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    private GameObject timelineImage;
    private Timer timer;
    
    public string Name { get => playerName; set => playerName = value; }

    public GameObject TimelineImage { get => timelineImage; set => timelineImage = value; }

    public Timer Timer { get => timer; }

    public void SetDependencies(PlayerInputComponent playerInputComponent, PlayerStore playerStore, ITimeProvider timeProvider, IWorldState worldState)
    {
        this.playerInputComponent = playerInputComponent;
        this.playerStore = playerStore;
        this.timeProvider = timeProvider;
        this.worldState = worldState;
    }

    public void ActivatePlayer()
    {
        playerInputComponent.ActivateComponent();
    }

    public void DeactivatePlayer()
    {
        playerInputComponent.DeactivateComnponent();
    }

    void Start()
    {
        cam = Camera.main;
        timer = new Timer(timeProvider, worldState.SecondsPerDay());
    }

    void Update()
    {
        //HandleInput();
        if (playerStore.GetActivePlayer() == this)
        {
            Move();
            if (worldState.IsMeasuring())
            {
                timer.Tick();
            }
        }
    }

    private void FixedUpdate()
    {
        //rigidBody.velocity = inputVector;
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
        cam.transform.position = viewPoint.position;
        cam.transform.rotation = viewPoint.rotation;
    }

    private void SetMinimapPosition()
    {
        minimapObject.transform.position = transform.position;
        minimapObject.transform.rotation = transform.rotation;
    }

    //private void HandleInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        DeliveryPackage deliveryPackage;
    //        if (deliveryService.GetPackage(this, out deliveryPackage))
    //        {
    //            deliveryPackage.ReleasePackage();
    //        } else if (packageService.GetPackageWithinPickupRange(this, out deliveryPackage))
    //        {
    //            deliveryPackage.PickupBy(this);
    //        }
    //    }
    //}

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

        float yVal = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y = yVal;

        charController.Move(movement * Time.deltaTime);
    }
}

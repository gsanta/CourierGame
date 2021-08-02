using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerService playerService;
    [HideInInspector] public DeliveryPackageController packageService;
    [HideInInspector] public DeliveryService deliveryService;

    [SerializeField] private Transform viewPoint;
    [SerializeField] private CharacterController charController;
    [SerializeField] private float moveSpeed = 5f, runSpeed = 8f;
    [SerializeField] private string playerName;
    
    private Camera cam;   
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    public string Name { get => playerName; }

    void Start()
    {
        cam = Camera.main;

        playerService.AddPlayer(this);
        playerService.ActivatePlayer(this);
    }

    void Update()
    {
        HandleInput();
        if (playerService.GetActivePlayer() == this)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        //rigidBody.velocity = inputVector;
    }

    private void LateUpdate()
    {
        if (playerService.GetActivePlayer() == this)
        {
            SetCameraPosition();
        }
    }

    private void SetCameraPosition()
    {
        cam.transform.position = viewPoint.position;
        cam.transform.rotation = viewPoint.rotation;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeliveryPackage deliveryPackage;
            Debug.Log(deliveryService);
            if (deliveryService.GetPackage(this, out deliveryPackage))
            {
                deliveryPackage.ReleasePackage();
            } else if (packageService.GetPackageWithinPickupRange(this, out deliveryPackage))
            {
                deliveryPackage.PickupBy(this);
            }
        }
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

        float yVal = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y = yVal;

        charController.Move(movement * Time.deltaTime);
    }
}

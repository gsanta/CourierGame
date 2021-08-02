using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    public Transform viewPoint;
    public CharacterController charController;
    [HideInInspector] public PlayerService playerService;
    public float moveSpeed = 5f, runSpeed = 8f;
    public int id = 0;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;

    void Start()
    {
        cam = Camera.main;

        playerService.AddPlayer(this);
        playerService.ActivatePlayer(this);
    }

    void Update()
    {
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

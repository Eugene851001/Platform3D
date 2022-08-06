using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const float RaycastDist = 50;
    const float MoveForce = 100;

    const int MaxJumpCount = 2;

    public float WalkingSpeed = 10;
    public float JumpSpeed = 5;
    public float Speed = 10;
    public float Gravity = 20;
    public float CameraYaw;
    public Transform GroundChecker;
    public LayerMask GroundMask;
    public HashSet<Ablilities> CurrentAbilities = new HashSet<Ablilities>();

    public GameObject Box;

    public Vector3 PlatformDeltaPos;

    public AudioSource JumpSnd;

    private CharacterController controller;
    private float verticalSpeed = 0;
    private int JumpCount = 0;

    private Collider _collider;

    private NoSpamAction _jumpAction;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _collider = GetComponent<Collider>();

        _jumpAction = new NoSpamAction(200, Jump);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        UseAbilities();

        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.UpdateGameState(GameState.Pause);
        }
    }

    private void Move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        Vector3 moveBy = (transform.forward * dz + transform.right * dx).normalized * WalkingSpeed;
        //moveBy.x = moveBy.x * Mathf.Sin(CameraYaw);
        //moveBy.y = moveBy.y * Mathf.Cos(CameraYaw);

        if (!IsGrounded())
        {
            verticalSpeed -= Gravity * Time.deltaTime;
        }
        else
        {
            JumpCount = 0;
            verticalSpeed = 0;
        }

        if (Input.GetButton("Jump") && JumpCount < MaxJumpCount)
        {
            _jumpAction.Run();
        }

        moveBy.y = verticalSpeed;

        controller.Move(moveBy * Time.deltaTime + PlatformDeltaPos);
    }

    private void Jump()
    {
        JumpCount++;
        verticalSpeed = JumpSpeed;
    }

    private void UseAbilities()
    {
        if (Input.GetKey("z"))
        {
            TryMoveObject();
        }
    }

    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    void TryMoveObject()
    { 
        var ray = new Ray() { origin = transform.position, direction = transform.forward };
        var hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo, RaycastDist))
        {
            if (hitInfo.collider.gameObject.tag == "Moveable")
            {
                var rb = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * MoveForce);
            }
        }
    }
}

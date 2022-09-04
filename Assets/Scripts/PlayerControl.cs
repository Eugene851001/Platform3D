using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const float RaycastDist = 50;
    const float MoveForce = 100;

    private int MaxJumpCount = 1;

    public float WalkingSpeed = 10;
    public float JumpSpeed = 5;
    public float Speed = 10;
    public float Gravity = 20;
    public float CameraYaw;
    public Transform GroundChecker;
    public LayerMask GroundMask;
    public GameObject _ablitiesContainer;


    public GameObject Box;

    public Vector3 PlatformDeltaPos;

    public AudioSource JumpSnd;

    private CharacterController controller;
    private float verticalSpeed = 0;
    private int JumpCount = 0;

    private AbilitiesManager _abilitiesManager;
     
    private Collider _collider;

    private NoSpamAction _jumpAction;
    private bool _isPaltformGrounded;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _collider = GetComponent<Collider>();
        _abilitiesManager = _ablitiesContainer.GetComponent<AbilitiesManager>();

        _abilitiesManager.OnAbilityAdd += OnAbilityAdded;
        _abilitiesManager.OnAbilityRemove += OnAbilityRemove;

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

    public void OnCollisionPatform(bool enter)
    {
        _isPaltformGrounded = enter;
    }

    private void Move() 
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        Vector3 moveBy = (transform.forward * dz + transform.right * dx).normalized * WalkingSpeed;
        //moveBy.x = moveBy.x * Mathf.Sin(CameraYaw);
        //moveBy.y = moveBy.y * Mathf.Cos(CameraYaw);

        if (!IsGrounded() && !_isPaltformGrounded)
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
        if (Input.GetKey("z") && _abilitiesManager.Contains(Ablilities.MoveObjects))
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
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, RaycastDist))
        {
            if (hitInfo.collider.gameObject.tag == "Moveable")
            {
                var rb = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                rb.AddForceAtPosition(transform.forward * MoveForce, hitInfo.point);
            }
        }
    }

    private void OnAbilityAdded(Ablilities ability)
    {
        if (ability == Ablilities.DoubleJump)
        {
            MaxJumpCount++;
        }
    }

    private void OnAbilityRemove(Ablilities ability)
    {
        if (ability == Ablilities.DoubleJump)
        {
            MaxJumpCount--;
        }
    }
}

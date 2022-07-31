using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float WalkingSpeed = 10;
    public float JumpSpeed = 5;
    public float Speed = 10;
    public float Gravity = 20;
    public float CameraYaw;
    public Transform GroundChecker;
    public LayerMask GroundMask;

    public Vector3 PlatformDeltaPos;

    public AudioSource JumpSnd;

    private CharacterController controller;
    private float verticalSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
        else if (Input.GetButton("Jump"))
        {
            verticalSpeed = JumpSpeed;
        }
        else
        {
            verticalSpeed = 0;
        }

        moveBy.y = verticalSpeed;

        controller.Move(moveBy * Time.deltaTime + PlatformDeltaPos);
    }

    bool IsGrounded()
    {
        return controller.isGrounded;
    }
}

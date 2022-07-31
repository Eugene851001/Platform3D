using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Transform camTramsform;

    public float Distance = 5.0f;

    public const float MAX_PICTH = 89.0f;
    public const float MIN_PITCH = 0.0f;

    private float currentYaw;//relative to y axis
    private float currentPitch;//relative to x axis

    // Start is called before the first frame update
    void Start()
    {
        camTramsform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dYaw = Input.GetAxis("Mouse X");
        var dPitch = Input.GetAxis("Mouse Y");

        currentYaw += dYaw;
        currentPitch += dPitch;
        currentPitch = Mathf.Clamp(currentPitch, MIN_PITCH, MAX_PICTH);
    }

    private void LateUpdate()
    {
        var dir = new Vector3(0, 0, -Distance);
        Quaternion rotation = Quaternion.Euler(currentPitch, -currentYaw, 0);

        camTramsform.position = target.position + rotation * dir;
        camTramsform.LookAt(target.position);
        target.rotation = rotation;
    }
}

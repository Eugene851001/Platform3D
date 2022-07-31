using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float SpeedX;
    public float SpeedY;
    public float SpeedZ;

    // Update is called once per frame
    void Update()
    {
        var degrees = 360;
        transform.Rotate(
            degrees * SpeedX * Time.deltaTime, 
            degrees * SpeedY * Time.deltaTime, 
            degrees * SpeedZ * Time.deltaTime);
    }
}

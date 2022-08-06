using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private Vector3 prevPosition;
    private PlayerControl player;

    private void FixedUpdate()
    {
        if (player != null)
        {
            player.PlatformDeltaPos = transform.position - prevPosition;
        }

        prevPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerControl>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.PlatformDeltaPos = new Vector3();
            player = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player.PlatformDeltaPos = new Vector3();
            player = null;
        }
    }
}

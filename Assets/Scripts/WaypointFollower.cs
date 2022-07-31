using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Transform[] WayPoints;
    public float Speed = 5f;

    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var waypoint = WayPoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(
            transform.position, 
            waypoint.position, 
            Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoint.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= WayPoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}

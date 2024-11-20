using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    GameObject player = null;
    bool playerAttached = false;

    /*
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Inside OnTriggerEnter");

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Inside isPlayer");
            collision.gameObject.transform.SetParent(transform);
            player = collision.gameObject;
            playerAttached = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);

            player = null;
            playerAttached = false;
        }
    }

    */

    private void Update()
    {
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        //Move the platform
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        //Move the player
        if (playerAttached)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}

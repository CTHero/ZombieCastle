using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object is paired with the TriggerObject.cs component

public class ActivatedMover : MonoBehaviour, ActivatedObject
{
    private bool isMoving = false;
    private Transform startPosition;
    [SerializeField] float speed = 7.0f;
    [SerializeField] GameObject destinationWaypoint = null;

    void Start()
    {
        startPosition = transform;
    }
    public void activate()
    {
        Debug.Log("Activating the Mover");
        isMoving = true;
    }
    public void deactivate()
    {

    }
    void Update()
    {
        if(isMoving)
        {
            if (Vector3.Distance(destinationWaypoint.transform.position, transform.position) < .1f)
            {
               isMoving = false;
            }

            //Move the platform
            transform.position = Vector3.MoveTowards(transform.position, destinationWaypoint.transform.position, Time.deltaTime * speed);

        }
    }
}

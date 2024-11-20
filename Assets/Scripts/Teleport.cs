using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject destinationWaypoint = null;
    [SerializeField] private AudioSource soundEffect;


    private void OnTriggerEnter(Collider collision)
    {
        GameObject tempPlayer = collision.gameObject;
        //Debug.Log("Inside Teleport's OnTriggerEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
           // Debug.Log("Inside Teleport");

            if (destinationWaypoint != null)
            {
                if (soundEffect != null) soundEffect.Play();

                Transform destinationTransform = destinationWaypoint.transform;
                tempPlayer.transform.position = destinationTransform.position;

            }
        }

    }



}


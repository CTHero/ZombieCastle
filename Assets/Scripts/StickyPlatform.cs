using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    public GameObject player = null;
    bool playerAttached = false;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Inside OnTriggerEnter"); 
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Inside isPlayer");
            //collision.gameObject.transform.SetParent(transform);
            //collision.gameObject.transform.parent.transform.SetParent(transform);
            //player = collision.gameObject;
            player.transform.SetParent(transform);
            playerAttached = true;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.transform.SetParent(null);
            //collision.gameObject.transform.parent.transform.SetParent(null);
            player.transform.SetParent(null);
            playerAttached = false;
        }
    }
}

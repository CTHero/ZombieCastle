using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform2 : MonoBehaviour
{
    GameObject player = null;
    bool playerAttached = false;
    
    Vector3 oldPosition = Vector3.zero;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Inside OnTriggerEnter");

        if (collision.gameObject.tag == "Player")
        {
            oldPosition = transform.position;
            Debug.Log("Platform is sticky");
            collision.gameObject.transform.SetParent(transform);
            player = collision.gameObject;
            playerAttached = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerAttached)
        {
            Vector3 positionDifference = transform.position - oldPosition;
            player.transform.position += positionDifference;

            oldPosition = transform.position;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
            playerAttached = false;
            Debug.Log("Platform is no longer sticky");
        }
    }

}

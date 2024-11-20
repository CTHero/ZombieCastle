using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToTrigger;
    [SerializeField] private LayerMask layerToCollide = ~0;

    private ActivatedObject theObject;



    // Start is called before the first frame update
    void Start()
    {
        theObject = objectToTrigger.GetComponent<ActivatedObject>();
        if (theObject == null) Debug.Log("Can't find object to activate");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        //Get the layer of the object we just collided with
        LayerMask theLayer = collision.gameObject.layer;

        //Stupid shit we have to do to see if the mask and the layer are the same
        if ((layerToCollide.value & (1 << theLayer.value)) != 0)
        {
            // yup
            Debug.Log("Mask found it");
            theObject.activate();
        }
    }
}

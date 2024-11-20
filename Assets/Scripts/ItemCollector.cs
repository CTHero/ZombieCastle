using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private GameObject objectToTrigger;

    ActivatedObject theMover = null;

    private int itemsToCollectCount = 0;
    private int itemsCollectedCount = 0;

    private void Start()
    {
        theMover = objectToTrigger.GetComponent<ActivatedObject>();
    }

    public void incrementCount()
    {
        itemsToCollectCount++;
    }

    public void itemCollected(GameObject theItem)
    {
        Destroy(theItem);
        itemsCollectedCount++;
        if(itemsCollectedCount == itemsToCollectCount)
        {
            //trigger the Object
            Debug.Log("Triggering the object");
            theMover.activate();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToCollect : MonoBehaviour
{
    [SerializeField] private GameObject itemCollectingObject;

    private ItemCollector theItemCollector;

    
    void Start()
    {
        theItemCollector = itemCollectingObject.GetComponent<ItemCollector>();
        theItemCollector.incrementCount();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            theItemCollector.itemCollected(this.gameObject);
        }
    }
}

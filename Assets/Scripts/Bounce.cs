using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceStrength = 10.0F;
    [SerializeField] private AudioSource bounceSoundEffect;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Inside Bounce: OnTriggerEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found the Player Object");
            //Debug.Log("Found Player");
            rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if(rigidbody == null) rigidbody = collision.gameObject.transform.parent.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, bounceStrength, rigidbody.velocity.z);

            //Play the bounce sound effect if it exists
            if(bounceSoundEffect != null)   bounceSoundEffect.Play();

        }

    }

}

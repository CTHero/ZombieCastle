using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedParticleSystem : MonoBehaviour, ActivatedObject
{
    [SerializeField] GameObject particleObject;
    private ParticleSystem theParticleSystem;
    public float duration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //If a separate object is not set, it assumes you mean the object the component is on.
        if(particleObject == null)
        {
            particleObject = gameObject;
        }
        theParticleSystem = particleObject.GetComponent<ParticleSystem>();

    }

    public void activate()
    {
        theParticleSystem.Play();
        if (duration > 0)
        {
            Destroy(gameObject, duration);
        }
    }
    public void deactivate()
    {

    }
}

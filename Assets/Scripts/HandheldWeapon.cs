using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldWeapon : MonoBehaviour, Weapon
{
    public GameObject animatedWeapon;
    public LayerMask enemyLayer;
    public int damageInflicted = 1;
    public float weaponDelay = 1f;

    Animator animator;
    List<GameObject> colliderList = new List<GameObject>();
    bool weaponActive = true;

    void Start()
    {
        //If the animated weapon has not been set
        if(animatedWeapon == null)
        {
            //Assume this object is the animated weapon
            animatedWeapon = gameObject;
            animator = GetComponent<Animator>();
        }
        else
        {
            //Get the animator of the 
            animator = animatedWeapon.GetComponent<Animator>();
        }
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if ((enemyLayer & (1 << collider.gameObject.layer)) != 0)
        {
            if (!colliderList.Contains(collider.gameObject))
            {
                colliderList.Add(collider.gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (colliderList.Contains(collider.gameObject))
        {
            colliderList.Remove(collider.gameObject);
        }
    }

    public void activate()
    {

        if (animator != null) animator.SetInteger("state", 1);
        for (int index = 0; index < colliderList.Count; index++)
        {
            colliderList[index].GetComponent<Health>().takeDamage(damageInflicted);
        }
    }

    public void reload()
    {
       //No reloading on handheld weapons
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

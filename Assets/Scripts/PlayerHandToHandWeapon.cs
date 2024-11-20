using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state=0 is weapon idle.  state=1 is the weapon attack animation

public class PlayerHandToHandWeapon : MonoBehaviour
{

    [Header("Weapon Properties")]
    public KeyCode activationKey = KeyCode.Mouse0;
    //public int swingsPerPress = 1;
    //public float timeBetweenSwings = .25f;
    public float timeBetweenAttacks = 1f;
    public LayerMask enemyLayer;
    public int damageInflicted = 1;

    Animator animator;
    List<GameObject> colliderList = new List<GameObject>();
    bool weaponTriggered = false;
    bool actionAllowed = true;


    void Start()
    {
        //Get the reference to the animator component and save it
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        //Checks to see if the object that collided is one of our potential targets
        if ((enemyLayer.value & (1 << collider.gameObject.layer)) != 0)
        {
            //If it is, add it to the list
            if (!colliderList.Contains(collider.gameObject))
            {
                colliderList.Add(collider.gameObject);
            }
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        //If the object that leaves our collider is on our list, remove it from the list
        if (colliderList.Contains(collider.gameObject))
        {
            colliderList.Remove(collider.gameObject);
        }
    }

    private void Update()
    {
        //Checks to see if the weapon has been triggered
        weaponTriggered = Input.GetKeyDown(activationKey);

        if(weaponTriggered && actionAllowed)
        {
            actionAllowed = false;

            if (animator != null) animator.SetInteger("state", 1);
            //Goes through the list of viable target in the weapon's collider
            for (int index = 0; index < colliderList.Count; index++)
            {
                //And inflicts damage on those NPCs
                colliderList[index].GetComponent<Health>().takeDamage(damageInflicted);
            }
            //Pauses the reactivation of the weapon
            Invoke("weaponReset", timeBetweenAttacks);
        }

    }
    private void weaponReset()
    {
        actionAllowed = true;
        if (animator != null) animator.SetInteger("state", 0);
    }
}

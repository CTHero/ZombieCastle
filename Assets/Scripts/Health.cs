using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//state = 21 is the death animation.
public class Health : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private float deathDelay = 0;
    [SerializeField] private AudioSource deathSoundEffect;

    bool isPlayer = false;
    Animator animator;

    void Start()
    {
        if(gameObject.tag == "Player") isPlayer = true;
    }


    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (deathSoundEffect != null) deathSoundEffect.Play();
            if (isPlayer)
            {
                //Kill the player
            }
            else
            {
                if (animator != null) animator.SetInteger("state", 21);  //Set death animation
                Invoke(nameof(destroyEnemy), deathDelay);
            }
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }


}

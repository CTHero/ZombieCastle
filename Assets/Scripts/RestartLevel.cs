using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Inside OnTriggerEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inside Restart Level");
            if(deathSoundEffect != null) deathSoundEffect.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}

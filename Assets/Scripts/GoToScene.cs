using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int sceneNumber = 0;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private GameObject objectToTrigger;
    private ActivatedObject theObject;

    public float timeDelay = 0f;

    void Start()
    {
        theObject = objectToTrigger.GetComponent<ActivatedObject>();
        if (theObject == null) Debug.Log("Can't find object to activate");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (soundEffect != null) soundEffect.Play();
            Invoke("jumpToScene", timeDelay);
            if (theObject != null)
            {
                theObject.activate();
            }
        }

    }

    private void jumpToScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }



}

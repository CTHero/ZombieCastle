using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public AudioSource buttonClickSound;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void gotoScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void clickTest()
    {
        Debug.Log(gameObject.name);
    }

    public void activate()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        gameObject.SetActive(true);
    }
    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}

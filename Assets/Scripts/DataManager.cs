using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A singleton class for storing persistent data between scenes.
public class DataManager : MonoBehaviour
{
    public static DataManager me;

    //Data to hold
    public Vector3 lastSavePoint;
    public GameObject lifeBar = null;
    public int maxLives = 5;
    public int lifeCount = 3;
    private int startLifeCount = 3;
    private int hideCount = 0;
    public bool hiding = false;

    public int maxHealth = 10;
    public int startHealth = 5;
    int health;

    void Awake()
    {
        if (me != null) //If an instance of this class already exists
        {
            Destroy(gameObject);  //Destroy this new instance
            return;// Exit the function
        }
        // end of new code

        me = this;  // Store this instance in a static variable
        DontDestroyOnLoad(gameObject);  //Do not destroy this object when the current scene ends and a new one begins

        startLifeCount = lifeCount;
    }
    private void Start()
    {
        health = startHealth;
    }
    public void resetLives()
    {
        lifeCount = startLifeCount;
    }
    public void increaseHideCount()
    {
        hideCount++;
    }
    public void decreaseHideCount()
    {
        hideCount--;
    }
    public bool checkHiding()
    {
        return (hideCount > 0) && hiding;
    }
}

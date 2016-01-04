using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public GameObject planet;
    public float percentTrigger = .20f;
    public bool levelBeaten = false;

    public int initialCount=0;
    public int currentCount=0;
    private bool planetTriggered = false;
    public bool encounterTriggered;
    public int maxSpawnNumber = 0;
    public float maxSpawnDelay = 0.0f;
    public float totalSpawnDelay = 0.0f;

    private float startTime = 0.0f;
    private bool timeFlag = false;
    
    //get encounterTriggered bool
    //start timer #fighters of biggest spawner * its spawn delay
    //during this time, only enumerate and add to the count of enemies
    //after that timer allow for the planet to open

	// Use this for initialization
	void Start ()
    {
        initialCount = transform.FindChild("Enemies").childCount;
        
        foreach (AI_Spawner aiSpawner in transform.FindChild("Enemies").GetComponentsInChildren<AI_Spawner>())
        {
            if (aiSpawner.capacity > maxSpawnNumber)
            {
                maxSpawnNumber = aiSpawner.capacity;
                maxSpawnDelay = aiSpawner.intervalTime;
            }
        }
        totalSpawnDelay = maxSpawnNumber * maxSpawnDelay;

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (encounterTriggered)
        {
            if (!timeFlag)
            {
                startTime = Time.time;
                timeFlag = !timeFlag;
            }
            //Debug.Log(Time.time - startTime);
            //is (nearly) everything dead?
            //activate planet opening
            if (Time.time - startTime <= totalSpawnDelay)
            {
                if (transform.FindChild("Enemies").childCount > currentCount)
                {
                    currentCount = transform.FindChild("Enemies").childCount;
                    initialCount = currentCount;
                }
            }

            else if (Time.time - startTime > totalSpawnDelay)
            {
                currentCount = transform.FindChild("Enemies").childCount;
                if (currentCount <= initialCount * percentTrigger && !planetTriggered)
                {
                    planet.GetComponent<planetChest>().openMe = true;
                    planetTriggered = true;
                    levelBeaten = true;
                }
            }
        }
        
	}

    void addToCount()
    {
        currentCount++;
    }
    void subtractFromCount()
    {
        currentCount--;
    }


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public GameObject planet;
    public float percentTrigger = .20f;
    public bool levelBeaten = false;

    private int initialCount=0;
    public int currentCount=0;
    private bool planetTriggered = false;
    private bool encounterTriggered = false;
    private int maxSpawnNumber = 0;
    private float maxSpawnDelay = 0.0f;
    private float totalSpawnDelay = 0.0f;

    private float startTime = 0.0f;
    
    //get encounterTriggered bool
    //start timer #fighters of biggest spawner * its spawn delay
    //during this time, only enumerate and add to the count of enemies
    //after that timer allow for the planet to open

	// Use this for initialization
	void Start ()
    {
        initialCount = transform.FindChild("Enemies").childCount;
        encounterTriggered = true;
        foreach (AI_Spawner aiSpawner in transform.FindChild("Enemies").GetComponentsInChildren<AI_Spawner>())
        {
            if (aiSpawner.capacity > maxSpawnNumber)
            {
                maxSpawnNumber = aiSpawner.capacity;
                maxSpawnDelay = aiSpawner.intervalTime;
            }
        }
        totalSpawnDelay = maxSpawnNumber * maxSpawnDelay;

        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
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

    void addToCount()
    {
        currentCount++;
    }
    void subtractFromCount()
    {
        currentCount--;
    }


}

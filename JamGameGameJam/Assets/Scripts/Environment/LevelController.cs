﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public GameObject planet;
    public float percentTrigger = .20f;

    private int initialCount=0;
    public int currentCount=0;
    private bool planetTriggered = false;

	// Use this for initialization
	void Start ()
    {
        initialCount = transform.FindChild("Enemies").childCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //is (nearly) everything dead?
        //activate planet opening
        currentCount = transform.FindChild("Enemies").childCount;
        if (currentCount <= initialCount* percentTrigger && !planetTriggered)
        {
            planet.GetComponent<planetChest>().openMe = true;
            planetTriggered = true;
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

﻿using UnityEngine;
using System.Collections;

public class orbitAround : MonoBehaviour {

    public GameObject target;
    public float speed = 5.0f;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.RotateAround(target.transform.position, Vector3.up, speed);
	}
}

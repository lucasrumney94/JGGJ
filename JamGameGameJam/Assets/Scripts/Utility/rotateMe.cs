using UnityEngine;
using System.Collections;

public class rotateMe : MonoBehaviour {

    public Vector3 angleRotation;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale != 0.0f)
            transform.Rotate(angleRotation);
	}
}

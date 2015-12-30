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
        transform.Rotate(angleRotation);
	}
}

using UnityEngine;
using System.Collections;

public class rotateCamera : MonoBehaviour {


    public float turnSpeed = 4.0f;

    private Vector3 input;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
        //Debug.Log(inputRotation);

    }
    void LateUpdate()
    {
        float xAxis = Input.GetAxis("rotateCamera");
        transform.Rotate(new Vector3(0.0f, xAxis*turnSpeed, 0.0f));
        //float yAxis = Input.GetAxis("rotateCameraVertical");
        //transform.Rotate(new Vector3(yAxis * turnSpeed, 0.0f, 0.0f));
    }
}

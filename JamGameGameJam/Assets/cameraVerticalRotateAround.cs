using UnityEngine;
using System.Collections;

public class cameraVerticalRotateAround : MonoBehaviour {


    public float turnSpeed = 4.0f;

    private Vector3 input;

    // Update is called once per frame
    void LateUpdate()
    {
        //float xAxis = Input.GetAxis("rotateCamera");
       // transform.parent.transform.parent.transform.Rotate(new Vector3(0.0f, xAxis * turnSpeed, 0.0f)); //rotate the actual Anchor


        //transform.RotateAround(transform.parent.transform.position,new Vector3(0.0f, 1.0f, 0.0f), xAxis * turnSpeed);
        //float yAxis = Input.GetAxis("rotateCameraVertical");
       // transform.RotateAround(transform.parent.transform.position, new Vector3(1.0f, 0.0f, 0.0f), yAxis*turnSpeed); //Just rotate the camera Y pivot

        //transform.LookAt(transform.parent.transform.position);
        
    }
}

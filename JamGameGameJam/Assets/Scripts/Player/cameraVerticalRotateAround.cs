using UnityEngine;
using System.Collections;

public class cameraVerticalRotateAround : MonoBehaviour {


    public float turnSpeed = 4.0f;
    public float camSmoothTime = 0.5f;

    private Vector3 input;
    private float zCamVelocity;
    private float yCamVelocity;
  

    // Update is called once per frame
    void LateUpdate()
    {
        float xAxis = Input.GetAxis("rotateCamera");
        transform.parent.transform.Rotate(new Vector3(0.0f, xAxis * turnSpeed, 0.0f)); //rotate the actual Anchor

        this.transform.localPosition = new Vector3(0.0f, Mathf.SmoothDamp(this.transform.localPosition.y, 5.0f + 3*GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().playerRadius, ref yCamVelocity, camSmoothTime), Mathf.SmoothDamp(this.transform.localPosition.z, -5.0f-3*GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().playerRadius, ref zCamVelocity, camSmoothTime));

        //transform.RotateAround(transform.parent.transform.position,new Vector3(0.0f, 1.0f, 0.0f), xAxis * turnSpeed);
        //float yAxis = Input.GetAxis("rotateCameraVertical");
       // transform.RotateAround(transform.parent.transform.position, new Vector3(1.0f, 0.0f, 0.0f), yAxis*turnSpeed); //Just rotate the camera Y pivot

        //transform.LookAt(transform.parent.transform.position);
        
    }
}

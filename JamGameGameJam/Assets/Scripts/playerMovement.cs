using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

    public float speed = 5.0f;
    Rigidbody rb = new Rigidbody();
    
    
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveElevation = Input.GetAxis("Elevate");
        //Debug.Log(moveElevation);

        Vector3 movement = new Vector3(moveHorizontal, moveElevation, moveVertical);
        rb.AddRelativeForce(speed*movement*Time.smoothDeltaTime);

    }



// Update is called once per frame
void Update ()
    {
	
	}
}

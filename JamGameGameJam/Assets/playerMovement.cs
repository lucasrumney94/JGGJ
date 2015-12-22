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

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(speed*movement*Time.smoothDeltaTime);

    }



// Update is called once per frame
void Update ()
    {
	
	}
}

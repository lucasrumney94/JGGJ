using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

    public float baseSpeed = 200.0f;
    public float speedScale = .9f;
    public float speed = 200.0f;

    public float boost = 4.0f;

    Rigidbody rb = new Rigidbody();
    private playerStats pStats;
    
    
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveElevation = Input.GetAxis("Elevate");
        //Debug.Log(moveElevation);

        Vector3 movement = new Vector3(moveHorizontal, moveElevation, moveVertical);
        rb.AddRelativeForce(boost*speed*movement*Time.smoothDeltaTime);

    }



// Update is called once per frame
void Update ()
    {
        speed = baseSpeed + pStats.playerGlobCount * speedScale;
        //Debug.Log(pStats.playerGlobCount);
	}
}

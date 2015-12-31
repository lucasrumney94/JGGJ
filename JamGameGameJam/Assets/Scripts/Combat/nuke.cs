using UnityEngine;
using System.Collections;

public class nuke : MonoBehaviour {
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float detonationRange = 15.0f;
    public GameObject explosionSphere;
    public GameObject killSphereObject;

    private GameObject player;
    private Rigidbody physicsRigidbody;
    private float startTime;

    private bool detonate = false;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        physicsRigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        Vector3 nukeDir = toPlayer.normalized * speed * physicsRigidbody.mass;
        physicsRigidbody.AddForce(nukeDir);

        transform.LookAt(player.transform);

        
        if (toPlayer.magnitude <detonationRange && !detonate)
        {
            
            Instantiate(explosionSphere,transform.position,Quaternion.identity);
            Instantiate(killSphereObject, transform.position, Quaternion.identity);

            Destroy(gameObject);
            detonate = true;
        }

    }
}

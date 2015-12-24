//Seeking-Type AI

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Blinky : MonoBehaviour {

    public float speed = 1f;

    private GameObject player;
    private Rigidbody physicsRigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        physicsRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        //Kill velocity if it gets too high and prevent orbiting behavior
        Vector3 toVelocity = toPlayer - physicsRigidbody.velocity;
        toVelocity = toVelocity.normalized * speed;
        physicsRigidbody.AddForce(toVelocity);
        Debug.DrawRay(transform.position, toPlayer, Color.red);
        Debug.DrawRay(transform.position, toVelocity, Color.green);
    }
}

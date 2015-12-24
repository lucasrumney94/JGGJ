//Orbiting-Type AI

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Pinky : MonoBehaviour {

    public float speed = 1f;
    public float orbitRadius = 10f;

    private GameObject player;
    private Rigidbody physicsRigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        physicsRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 toPlayer = (player.transform.position - this.transform.position).normalized;
        Vector3 edge = Vector3.Cross(toPlayer, Vector3.up) * orbitRadius;
        edge += player.transform.position;
        Vector3 toOrbit = (edge - this.transform.position).normalized;
        toOrbit *= speed;
        physicsRigidbody.AddForce(toOrbit);
        Debug.DrawRay(transform.position, toPlayer, Color.red);
        Debug.DrawLine(transform.position, edge, Color.green);
    }
}

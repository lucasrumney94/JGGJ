//Seeking-Type AI

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Blinky : MonoBehaviour {

    public float speed = 1f;
    public float turnSpeed = 1f;

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
        TurnToVelocity();
    }

    private void TurnToVelocity()
    {
        Vector3 facingAngle = transform.forward;
        Debug.DrawRay(transform.position, facingAngle * 2f, Color.red);

        Vector3 velocityAngle = physicsRigidbody.velocity.normalized;
        Debug.DrawRay(transform.position, velocityAngle * 2f, Color.green);

        float angleBetween = Vector3.Angle(facingAngle, velocityAngle);

        if (angleBetween > 5f)
        {
            Vector3 torqueAxis = Vector3.Cross(facingAngle, velocityAngle).normalized * turnSpeed;
            physicsRigidbody.AddTorque(torqueAxis * (angleBetween / 360));
            Debug.DrawRay(transform.position, torqueAxis, Color.blue);
        }
    }
}

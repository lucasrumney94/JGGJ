//Orbiting-Type AI

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Pinky : MonoBehaviour {

    public bool active = false;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float orbitRadius = 10f;
    public float playerDiameter = 0f;
    public GameObject orbitTarget;

    private GameObject anchor;
    private GameObject player;
    private Rigidbody physicsRigidbody;
    private Vector3 toPlayer;

    void Start()
    {
        
        anchor = GameObject.FindGameObjectWithTag("Anchor");
        player = GameObject.FindGameObjectWithTag("Player");
        physicsRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (active)
        {
            playerDiameter = player.GetComponent<playerStats>().playerRadius * 2f;
            OrbitPlayer();
        }
        else if (orbitTarget)
        {
            OrbitTarget();
        }
    }

    private void OrbitPlayer()
    {
        toPlayer = (anchor.transform.position - this.transform.position).normalized;
        Vector3 edge = Vector3.Cross(toPlayer, Vector3.up) * (playerDiameter + 1f);
        Vector3 toOrbit = (edge).normalized;
        toOrbit *= speed;
        physicsRigidbody.AddForce(toOrbit);
        TurnToVelocity();
        //TurnToBroadside();
        Debug.DrawRay(transform.position, toPlayer, Color.red);
        Debug.DrawLine(transform.position, edge + anchor.transform.position, Color.green);
    }

    private void OrbitTarget()
    {
        Vector3 toTarget = (orbitTarget.transform.position - this.transform.position).normalized;
        Vector3 edge = Vector3.Cross(toTarget, Vector3.up) * orbitRadius;
        Vector3 toOrbit = (edge).normalized;
        toOrbit *= speed;
        physicsRigidbody.AddForce(toOrbit);
        TurnToVelocity();
        Debug.DrawRay(transform.position, toOrbit, Color.red);
        Debug.DrawLine(transform.position, edge + orbitTarget.transform.position, Color.green);
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
            Vector3 torqueAxis = Vector3.Cross(facingAngle, velocityAngle).normalized * turnSpeed * physicsRigidbody.mass;
            physicsRigidbody.AddTorque(torqueAxis * (angleBetween / 360));
        }
    }

    private void TurnToBroadside()
    {
        Vector3 desiredUp = Vector3.Cross(transform.forward, toPlayer);
        Debug.DrawRay(transform.position, desiredUp * 20f, Color.blue);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Vector3.Angle(desiredUp, transform.up));
    }
}

//Seeking-Type AI

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Blinky : MonoBehaviour {

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float swervePeriod = 5f;
    public float swerveForce = 10f;

    private float swerveDelay;

    private GameObject player;
    private Rigidbody physicsRigidbody;

    private float startTime;
    //private GameObject enemiesObject;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        physicsRigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;
        swerveDelay = Random.Range(0f, 2f * Mathf.PI);
        //enemiesObject = GameObject.FindGameObjectWithTag("Enemies");
        //gameObject.transform.parent = enemiesObject.transform;
    }

    void FixedUpdate()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        //Kill velocity if it gets too high and prevent orbiting behavior
        toPlayer = toPlayer.normalized * speed * physicsRigidbody.mass;
        physicsRigidbody.AddForce(toPlayer);
        Swerve();
        TurnToVelocity();
    }

    private void Swerve()
    {
        float yForce = Mathf.Cos((2 * Mathf.PI * (Time.time - startTime) / swervePeriod) + swerveDelay) * swerveForce;
        physicsRigidbody.AddForce(new Vector3(0f, yForce, 0f));
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
}

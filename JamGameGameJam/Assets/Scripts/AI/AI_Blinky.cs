using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI_Blinky : MonoBehaviour {

    public float speed = 1f;

    private GameObject player;
    private Rigidbody physicsRigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        physicsRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 toPlayer = this.transform.position - player.transform.position;
        toPlayer = toPlayer.normalized * speed;
        physicsRigidbody.AddForce(toPlayer);
        Debug.DrawRay(transform.position, toPlayer, Color.red);
    }
}

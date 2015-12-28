using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
    }
}

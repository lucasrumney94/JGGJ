using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public float speed;
    public float lifetime = 5f;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
    }

    void Update()
    {
        if(Time.time - startTime > lifetime)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

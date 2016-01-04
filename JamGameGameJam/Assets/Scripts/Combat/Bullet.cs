using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public float speed;
    [Range(1,3)]
    public int size;
    public float lifetime = 5f;

    private float startTime;
    private playerStats pStats;
    private globController globCont;
    private List<GameObject> globs;

    void Start()
    {
        startTime = Time.time;
        GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        globCont = GameObject.FindGameObjectWithTag("Anchor").GetComponent<globController>();
        globs = globCont.globs;

    }

    void Update()
    {
        if(Time.time - startTime > lifetime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Anchor")
        {
            if (other.gameObject.tag == "globSMALL")
            {
                globCont.globCount--;
                globCont.particleCount--;
                globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else if (size >= 2 && other.gameObject.tag == "globMEDIUM")
            {
                globCont.globCount -= 4;
                globCont.particleCount--;
                globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else if (size >= 3 && other.gameObject.tag == "globLARGE")
            {
                globCont.globCount -= 16;
                globCont.particleCount--;
                globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}

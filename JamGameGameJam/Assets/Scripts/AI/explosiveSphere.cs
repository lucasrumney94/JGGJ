using UnityEngine;
using System.Collections;

public class explosiveSphere : MonoBehaviour {

    public float explosiveForce;

    private bool exploding = false;

    private globController globCont;
	// Use this for initialization
	void Start ()
    {
        globCont = GameObject.FindGameObjectWithTag("Anchor").GetComponent<globController>();

    }
    void Awake()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (exploding)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 5f);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        exploding = true;
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            if (other.gameObject.tag != "Anchor")
                other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, transform.localScale.x);

            
        }
    }
}

using UnityEngine;
using System.Collections;

public class explosiveSphere : MonoBehaviour {

    public float explosiveForce;

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
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if(other.tag !="Anchor")
                other.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, transform.localScale.x);

            if (other.gameObject.tag == "globSMALL")
            {
                globCont.globCount--;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject, 0.5f);
            }
            else if (other.gameObject.tag == "globMEDIUM")
            {
                globCont.globCount -= 4;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject, 0.5f);
            }
            else if (other.gameObject.tag == "globLARGE")
            {
                globCont.globCount -= 16;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject, 0.5f);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class killSphere : MonoBehaviour {

    private bool exploding = false;

    private globController globCont;
	// Use this for initialization
	void Start ()
    {
        globCont = GameObject.FindGameObjectWithTag("Anchor").GetComponent<globController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (exploding)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 4f);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        exploding = true;
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            if (other.gameObject.tag == "globSMALL")
            {
                globCont.globCount--;
                globCont.particleCount--;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "globMEDIUM")
            {
                globCont.globCount -= 4;
                globCont.particleCount--;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "globLARGE")
            {
                globCont.globCount -= 16;
                globCont.particleCount--;
                globCont.globs.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}

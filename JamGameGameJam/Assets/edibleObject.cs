using UnityEngine;
using System.Collections;

public class edibleObject : MonoBehaviour {

    public float eatTime = 5.0f;
    public int globs = 10;
    public float eatDistance = 5.0f;

    private GameObject playerAnchor;

	// Use this for initialization
	void Start ()
    {
        playerAnchor = GameObject.FindGameObjectWithTag("Anchor");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (((playerAnchor.transform.position - transform.position).sqrMagnitude < eatDistance) && playerAnchor.GetComponent<globController>().ballCount>globs)
        {
            StartCoroutine("eatingTime");
        }
        else
        {
            StopCoroutine("eatingTime");
        }
	}

    IEnumerator eatingTime ()
    {
        yield return new WaitForSeconds(eatTime);
        playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }


}

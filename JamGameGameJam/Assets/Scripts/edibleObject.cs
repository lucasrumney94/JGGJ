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
        eatDistance = Mathf.Pow(transform.localScale.x/4 + playerAnchor.transform.parent.GetComponent<playerStats>().playerRadius,2); 
        if (((playerAnchor.transform.position - transform.position).sqrMagnitude < eatDistance) && playerAnchor.GetComponent<globController>().particleCount>globs)
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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing)
        {
            eatTime /= 2;
        }
        yield return new WaitForSeconds(eatTime);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
        {
            playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }


}

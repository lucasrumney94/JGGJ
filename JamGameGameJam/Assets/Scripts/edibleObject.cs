using UnityEngine;
using System.Collections;

public class edibleObject : MonoBehaviour {

    public int level = 1;
    public float eatTime = 5.0f;
    public int globs = 10;
    public float eatDistance = 5.0f;
    public float smashTimeLeft = 0.1f;
    public float lungeTimeReduction = 2.0f;

    public bool justEatOnCollision = false;

    private GameObject playerAnchor;

    private bool eating = false;
    private bool lungeFlag = true;
	// Use this for initialization
	void Start ()
    {
        playerAnchor = GameObject.FindGameObjectWithTag("Anchor");
        //SendMessageUpwards("addToCount");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!justEatOnCollision)
        {
            eatDistance = Mathf.Pow(transform.localScale.x / 4 + playerAnchor.transform.parent.GetComponent<playerStats>().playerRadius, 2);
            if (((playerAnchor.transform.position - transform.position).sqrMagnitude < eatDistance) && playerAnchor.GetComponent<globController>().globCount > globs)
            {
                if (eating == false)
                {
                    StartCoroutine("eatingTime");
                }
                eating = true;



            }
            else
            {
                eating = false;
                StopCoroutine("eatingTime");
            }

            if (eatTime <= 0.0f)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
                {
                    playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "globSMALL" || other.gameObject.tag == "globMEDIUM" || other.gameObject.tag == "globLARGE")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat || GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing )
            {
                playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator eatingTime ()
    {
        lungeFlag = true;
        
        while (eatTime > 0.0f)
        {
            eatTime -= Time.deltaTime;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing)
            {
                eatTime = smashTimeLeft;
            }
            if (!GameObject.FindGameObjectWithTag("Anchor").GetComponent<globController>().lungeRecharged && lungeFlag)
            {
                if (playerAnchor.GetComponent<globController>().globCount > globs)
                {
                    eatTime -= lungeTimeReduction;
                    lungeFlag = false;
                }
            }




            if (eatTime <= 0.0f)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
                {
                    playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
                    Destroy(this.gameObject);
                }
                yield return null;
            }


            yield return null;
        }
        //yield return new WaitForSeconds(eatTime);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
        {
            playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
            //SendMessageUpwards("subtractFromCount");
            Destroy(this.gameObject);
        }
        yield return null;
    }


}

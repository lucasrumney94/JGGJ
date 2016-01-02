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
    private bool triggered = false;

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
            //eatDistance = Mathf.Pow(transform.localScale.x / 4 + playerAnchor.transform.parent.GetComponent<playerStats>().playerRadius, 2);
            if (((playerAnchor.transform.position - transform.position).sqrMagnitude < eatDistance) && playerAnchor.GetComponent<globController>().globCount > globs && GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
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
                    //this.GetComponents<AudioSource>()[1].Play(); //play the popping sound upon death
                    //playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);

                    Destroy(this.gameObject, 0.3f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && justEatOnCollision)
        {
            if (other.gameObject.tag == "globSMALL" || other.gameObject.tag == "globMEDIUM" || other.gameObject.tag == "globLARGE" || other.gameObject.tag == "Anchor")
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat || GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing)
                {
                    this.GetComponents<AudioSource>()[1].Play(); //play the popping sound upon death
                    playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Trigger Entered!");
                    triggered = true;
                    Destroy(this.gameObject, 0.128f);
                }
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
                    //this.GetComponents<AudioSource>()[1].Play(); //play the popping sound upon death
                    //playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);

                    Destroy(this.gameObject, 0.128f);
                }
                yield return null;
            }


            yield return null;
        }
        yield return new WaitForSeconds(eatTime);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat)
        {
            this.GetComponents<AudioSource>()[1].Play(); //play the popping sound upon death
            playerAnchor.SendMessageUpwards("addGlobs", globs, SendMessageOptions.DontRequireReceiver);
            SendMessageUpwards("subtractFromCount", SendMessageOptions.DontRequireReceiver);
            
            Destroy(this.gameObject,0.128f);
        }
        yield return null;
    }


}

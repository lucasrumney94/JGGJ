using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class globController : MonoBehaviour
{

    public GameObject globSMALL;
    public GameObject globMEDIUM;
    public GameObject globLARGE;
    public int particleCount = 200;
    public int globCount = 200;
    public float clusterAffinity = 100f;
	public float smashLength = 3.0f;
	public float smashRecharge = 10.0f;
	public float expandRecharge = 1.0f;
    public float snakeSpeedFactor = 2.0f;
    public float lungeRecharge = 5.0f;
    public float lungeSpeed = 5.0f;
    public float lungeDistance = 5.0f;


    public List<GameObject> globs = new List<GameObject>();
	private GameObject player;
	private playerStats pStats;
	int i = 0;
    Vector3 randomOffset;
    private bool medium = false;
    private bool large = false;

    private GameObject Anchor;
	private bool smashRecharged = true;
    private bool explodeSelf = false;
    
    private bool expandSelf = false;
    private bool expandRecharged = true;
    [HideInInspector]
    public bool expanded = false;
    private float playerRadiusSample = 1.0f;
    private float originalClusterAffinity;
    private bool snakeSelf = false;
    private bool snakeRecharged = true;
    private float originalSpeed;
    private bool snaked = false;


    private bool lunge = false;
    private bool lunged = false;
    private bool lungeSelf = false;
    [HideInInspector]
    public bool lungeRecharged = true;


    // Use this for initialization
    void Start()
    {
        Anchor = GameObject.FindGameObjectWithTag("Anchor");

		player = GameObject.FindGameObjectWithTag("Player");
		pStats = player.GetComponent<playerStats>();

        originalClusterAffinity = clusterAffinity;

        for (i = 0; i < particleCount; i++)
        {
            randomOffset = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
            globs.Add((GameObject)Instantiate(globSMALL,randomOffset,Quaternion.identity));
        }
    }

    void Update()
    {
        Debug.Log(pStats.canEat);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        explodeSelf = Input.GetButtonDown("Explode");

        if (explodeSelf && smashRecharged && !expanded && !snaked)
        {
            //Debug.Log("SMASH ATTACK!");
			StartCoroutine("smashAttack");
			StartCoroutine("smashRecharger");
			smashRecharged = false;
        }

		expandSelf = Input.GetButtonDown("Expand");
        if (expandSelf && expandRecharged && !pStats.isSmashing && !snaked)
		{
            //start coroutines for expand
            
            expanded = !expanded;
            StartCoroutine("expandRecharger");
            pStats.canEat = false;
            expandRecharged = false;
            //Debug.Log("FORM OF... BEES!");
            playerRadiusSample = pStats.playerRadius;
        }
        if (expanded)
        {
            pStats.canEat = false;
            float scale = 20.0f;
            foreach (GameObject glob in globs)
            {

                Vector3 randomExpand = new Vector3(Random.Range(-scale * playerRadiusSample, scale * playerRadiusSample), Random.Range(-scale * playerRadiusSample, scale * playerRadiusSample), Random.Range(-scale * playerRadiusSample, scale * playerRadiusSample));
                glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * scale * (randomExpand + Anchor.transform.position - glob.transform.position).normalized * Time.smoothDeltaTime);
            }

        }
        snakeSelf = Input.GetButtonDown("Snake");
        if (snakeSelf && !explodeSelf && !expanded)
        {
            snaked = !snaked;
        }
        if (snaked)
        {
            pStats.canEat = false;
            Anchor.GetComponent<playerMovement>().boost = snakeSpeedFactor;

            //Debug.Log("FORM OF... SNAKE!");
            float tempClusterAff = clusterAffinity * 2;
            foreach (GameObject glob in globs)
            {
                tempClusterAff++;
                if (tempClusterAff > 4.5f * clusterAffinity)
                    tempClusterAff = 2.0f * clusterAffinity;
                glob.GetComponent<Rigidbody>().AddForce(tempClusterAff * (Anchor.transform.position - glob.transform.position).normalized * Time.smoothDeltaTime);
            }
        }
        if (!snaked && !expanded && !pStats.isSmashing)
        {
            pStats.canEat = true;
        }
        lungeSelf = Input.GetButtonDown("Lunge");
        //Debug.DrawRay(Anchor.transform.position, 100 * Anchor.transform.forward);
        lungeDistance = pStats.playerRadius;
        if (lungeSelf && lungeRecharged && !snaked && !expanded && !explodeSelf)
        {
            StartCoroutine("lungeRecharger");
            //Debug.Log("lunge!");
            foreach (GameObject glob in globs)
            {
                //Vector3 lungeForward =  Anchor.transform.forward*lungeDistance;
                
                //Debug.Log(Anchor.transform.forward);

                glob.GetComponent<Rigidbody>().AddForce(lungeSpeed * ((Anchor.transform.forward.normalized * lungeDistance)) * Time.smoothDeltaTime);
            }
            lungeRecharged = false;
            Anchor.GetComponent<Rigidbody>().AddForce(lungeSpeed * lungeDistance * Anchor.transform.forward.normalized * Time.smoothDeltaTime);
        }


        if (!snaked && !expanded && !explodeSelf && !lungeSelf)
        {
            Anchor.GetComponent<playerMovement>().boost = 1.0f;
            foreach (GameObject glob in globs)
            {
                //Debug.Log(glob.transform.parent.position);

                glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * (Anchor.transform.position - glob.transform.position) * Time.smoothDeltaTime);
            }
        }
        
        if (particleCount > 700)
        {
            int sCount = 0;
            int mCount = 0;
            foreach (GameObject glob in globs)
            {
                if (glob.name == "globSMALL(Clone)")
                {
                    sCount++;
                    globs.Remove(glob);
                    Destroy(glob);
                    particleCount--;
                }
                if (sCount == 4)
                {
                    medium = true;
                    addGlobs(1);
                    medium = false;
                    particleCount++;
                    break;
                }
                if (sCount < 4)
                {
                    if (glob.name == "globMEDIUM(Clone)")
                    {
                        mCount++;
                        globs.Remove(glob);
                        Destroy(glob);
                        particleCount--;
                    }
                    if (mCount == 4)
                    {
                        large = true;
                        addGlobs(1);
                        large = false;
                        particleCount++;
                        break;
                    }
                }
            }
        }
        
    }

    void addGlobs(int numberOfGlobs)
    {
        int mediumToSmallRatio = 4;
        int largeToMediumRatio = 4;
        int mediumGlobs = numberOfGlobs / mediumToSmallRatio;
        int largeGlobs = numberOfGlobs / largeToMediumRatio;
        if (numberOfGlobs < 200)
        {
            for (int j = 0; j < numberOfGlobs; j++)
            {
                particleCount++;
                globCount++;
                randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

                globs.Add((GameObject)Instantiate(globSMALL, transform.position + randomOffset, Quaternion.identity));
            }
        }
        else if (numberOfGlobs >=200 && numberOfGlobs<=700 || medium == true)
        {
            for (int j = 0; j < largeGlobs; j++)
            {
                particleCount++;
                globCount += mediumToSmallRatio;
                randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
         
                globs.Add((GameObject)Instantiate(globMEDIUM, transform.position + randomOffset, Quaternion.identity));
            }
        }
        else if (numberOfGlobs >= 700 || large == true)
        {
            for (int j = 0; j < largeGlobs; j++)
            {
                particleCount++;
                globCount += largeToMediumRatio;
                randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                globs.Add((GameObject)Instantiate(globLARGE, transform.position + randomOffset, Quaternion.identity));
            }
        }
    }


	IEnumerator smashAttack()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat = false;
		clusterAffinity = -clusterAffinity;

		yield return new WaitForSeconds(smashLength);

		clusterAffinity = originalClusterAffinity;

		yield return new WaitForSeconds(smashLength);
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().isSmashing = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().canEat = true;

	}
	IEnumerator smashRecharger()
	{
		yield return new WaitForSeconds(smashRecharge);
		smashRecharged = true;
	}


    IEnumerator expandRecharger()
    {
        yield return new WaitForSeconds(expandRecharge);
        expandRecharged = true;
    }

    IEnumerator lungeRecharger()
    {
        yield return new WaitForSeconds(lungeRecharge);
        lungeRecharged = true;
    }

}

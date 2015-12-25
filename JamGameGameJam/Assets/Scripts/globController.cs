using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class globController : MonoBehaviour
{

    public GameObject globSMALL;
    public GameObject globLARGE;
    public int particleCount = 200;
    public int globCount = 200;
    public float clusterAffinity = 100f;
	public float smashLength = 3.0f;
	public float smashRecharge = 10.0f;
	public float expandRecharge = 1.0f;


    public List<GameObject> globs = new List<GameObject>();
	private GameObject player;
	private playerStats pStats;
	int i = 0;
    Vector3 randomOffset;
    bool large = false;
    private GameObject Anchor;
	private bool smashRecharged = true;
    private bool explodeSelf = false;
    private bool expandSelf = false;
	private bool expandRecharged = true;
    private bool expanded = false;
    private float playerRadiusSample = 1.0f;

    // Use this for initialization
    void Start()
    {
        Anchor = GameObject.FindGameObjectWithTag("Anchor");

		player = GameObject.FindGameObjectWithTag("Player");
		pStats = player.GetComponent<playerStats>();

        for (i = 0; i < particleCount; i++)
        {
            randomOffset = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
            globs.Add((GameObject)Instantiate(globSMALL,randomOffset,Quaternion.identity));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        explodeSelf = Input.GetButtonDown("Explode");

        if (explodeSelf && smashRecharged && !expanded)
        {
			StartCoroutine("smashAttack");
			StartCoroutine("smashRecharger");
			smashRecharged = false;
        }

		expandSelf = Input.GetButtonDown("Expand");

		if (expandSelf && expandRecharged && !explodeSelf)
		{
            //start coroutines for expand
            
            expanded = !expanded;
            StartCoroutine("expandRecharger");
            pStats.canEat = false;
            expandRecharged = false;
            Debug.Log(expanded);
            playerRadiusSample = pStats.playerRadius;
        }
        if (expanded)
        {
            float spacing = 0.5f;
            float posI = 0.0f;
            float posJ = 0.0f;
            float posK = 0.0f;

            foreach (GameObject glob in globs)
            {

                Vector3 offset = new Vector3(spacing*posI,spacing*posJ,spacing*posK);
                posI++;
                posJ++;
                posK++;
                

                glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * (offset + Anchor.transform.position - glob.transform.position) * Time.smoothDeltaTime);
            }
        }
        else
        {
            foreach (GameObject glob in globs)
            {
                //Debug.Log(glob.transform.parent.position);

                glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * (Anchor.transform.position - glob.transform.position) * Time.smoothDeltaTime);
            }
        }
        /*if (ballCount > 700)
        {
            int count = 0;
            foreach (GameObject glob in globs)
            {
                if (glob.name == "Ball(Clone)")
                {
                    count++;
                    globs.Remove(glob);
                    Destroy(glob);
                    ballCount--;
                }
                if (count == 4)
                {
                    large = true;
                    addGlobs(1);
                    large = false;
                    ballCount++;
                    break;
                }
            }
        }*/
        
    }

    void addGlobs(int numberOfGlobs)
    {
        int largeToSmallRatio = 4;
        int largeGlobs = numberOfGlobs / largeToSmallRatio;
        if (numberOfGlobs < 400)
        {
            for (int j = 0; j < numberOfGlobs; j++)
            {
                particleCount++;
                globCount++;
                randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

                globs.Add((GameObject)Instantiate(globSMALL, transform.position + randomOffset, Quaternion.identity));
            }
        }
        else if (numberOfGlobs >=400 || large == true)
        {
            for (int j = 0; j < largeGlobs; j++)
            {
                particleCount++;
                globCount += largeToSmallRatio;
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

		clusterAffinity = -clusterAffinity;

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

}

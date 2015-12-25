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

    public List<GameObject> globs = new List<GameObject>();
    int i = 0;
    Vector3 randomOffset;
    bool large = false;
    private GameObject Anchor;
    private int explodeSelfCount = 0;


    // Use this for initialization
    void Start()
    {
        Anchor = GameObject.FindGameObjectWithTag("Anchor");
        for (i = 0; i < particleCount; i++)
        {
            randomOffset = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
            globs.Add((GameObject)Instantiate(globSMALL,randomOffset,Quaternion.identity));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool explodeSelf = Input.GetButtonDown("Explode");

        if (explodeSelf)
        {
			StartCoroutine("smashAttack");
        }


        
        foreach (GameObject glob in globs)
        {
            //Debug.Log(glob.transform.parent.position);
           
            glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * (Anchor.transform.position-glob.transform.position)*Time.smoothDeltaTime);
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
}

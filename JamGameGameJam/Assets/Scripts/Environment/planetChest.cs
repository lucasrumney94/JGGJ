using UnityEngine;
using System.Collections;

public class planetChest : MonoBehaviour
{

    public bool openMe = false;
    public bool dispense = false;
    public int numberOfGlobs = 50;

    public float randomSpreadScale = 50;

    private GameObject Anchor;
    private globController globCont;
    private bool dispensing = true;

    // Use this for initialization
    void Start()
    {
        Anchor = GameObject.FindGameObjectWithTag("Anchor");
        globCont = Anchor.GetComponent<globController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openMe)
        {
            StartCoroutine("openPlanet");
            openMe = false;
        }
        if (dispense)
        {
            StartCoroutine("dispenseGlobs");
            dispense = false;
        }
    }

    IEnumerator openPlanet()
    {
        //rotate planet open
        Vector3 playerToPlanet = Anchor.transform.position - transform.position;
        playerToPlanet = new Vector3(playerToPlanet.x, 0.0f, playerToPlanet.z);
        float topAngle = 0.0f;
        while (topAngle < 75.0f)
        {
            topAngle++;
            transform.GetChild(0).transform.RotateAround(transform.position+(-playerToPlanet.normalized*transform.localScale.x), Vector3.Cross(playerToPlanet, Vector3.up), 1.0f);

            yield return null;
        }
        yield return new WaitForSeconds(5.0f);
        //Debug.Log("Set off Spawner");
        dispense = true;


        //yield return new WaitForSeconds(5.0f);

    }
    IEnumerator dispenseGlobs()
    {
        while (dispensing)
        {
            
            int mediumToSmallRatio = 4;
            int largeToMediumRatio = 4;
            int mediumGlobs = numberOfGlobs / mediumToSmallRatio;
            int largeGlobs = numberOfGlobs / largeToMediumRatio;

            Vector3 randomSpread;

            if (numberOfGlobs < 200)
            {
                for (int j = 0; j < numberOfGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount++;
                    
                    randomSpread = new Vector3(Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale));
                    globCont.globs.Add((GameObject)Instantiate(globCont.globSMALL, transform.position+randomSpread, Quaternion.identity));
                    dispensing = false;
                }
            }
            else if (numberOfGlobs >= 200 && numberOfGlobs <= 700)
            {
                for (int j = 0; j < largeGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount += mediumToSmallRatio;

                    randomSpread = new Vector3(Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale));

                    globCont.globs.Add((GameObject)Instantiate(globCont.globMEDIUM, transform.position+randomSpread, Quaternion.identity));
                    dispensing = false;
                }
            }
            else if (numberOfGlobs >= 700)
            {
                for (int j = 0; j < largeGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount += largeToMediumRatio;

                    randomSpread = new Vector3(Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale), Random.Range(-randomSpreadScale, randomSpreadScale));

                    globCont.globs.Add((GameObject)Instantiate(globCont.globLARGE, transform.position+randomSpread, Quaternion.identity));
                    dispensing = false;
                }
            }
            yield return null;
        }
        yield return null;
    }
}
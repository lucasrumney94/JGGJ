using UnityEngine;
using System.Collections;

public class planetChest : MonoBehaviour
{

    public bool openMe = false;
    public bool dispense = false;
    public int numberOfGlobs = 50;

    private GameObject Anchor;
    private globController globCont;


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




        yield return new WaitForSeconds(5.0f);
        Debug.Log("Set off Spawner");
        dispense = true;

    }
    IEnumerator dispenseGlobs()
    {
        while (numberOfGlobs > 0)
        {
            numberOfGlobs--;
            int mediumToSmallRatio = 4;
            int largeToMediumRatio = 4;
            int mediumGlobs = numberOfGlobs / mediumToSmallRatio;
            int largeGlobs = numberOfGlobs / largeToMediumRatio;
            if (numberOfGlobs < 200)
            {
                for (int j = 0; j < numberOfGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount++;
                    
                    globCont.globs.Add((GameObject)Instantiate(globCont.globSMALL, transform.position, Quaternion.identity));
                }
            }
            else if (numberOfGlobs >= 200 && numberOfGlobs <= 700)
            {
                for (int j = 0; j < largeGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount += mediumToSmallRatio;
                    
                    globCont.globs.Add((GameObject)Instantiate(globCont.globMEDIUM, transform.position, Quaternion.identity));
                }
            }
            else if (numberOfGlobs >= 700)
            {
                for (int j = 0; j < largeGlobs; j++)
                {
                    globCont.particleCount++;
                    globCont.globCount += largeToMediumRatio;
                    
                    globCont.globs.Add((GameObject)Instantiate(globCont.globLARGE, transform.position, Quaternion.identity));
                }
            }
        }
        yield return null;
    }
}
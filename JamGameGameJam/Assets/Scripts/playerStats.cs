using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

    public float checkRadiusPeriod = 2.0f;


    [HideInInspector]
    public float playerRadius = 1.0f;
    [HideInInspector]
    public int playerGlobCount = 1;

    public bool canEat = true;
    public bool isSmashing = false;

    private GameObject Anchor;
    private float lastPlayerRadius = 1.0f;

	// Use this for initialization
	void Start () 
	{
        Anchor = GameObject.FindGameObjectWithTag("Anchor") ;
        StartCoroutine("checkRadius");
	}
	
	// Update is called once per frame
	void Update () 
	{
        //Debug.Log(Anchor.GetComponent<globController>().globCount);
    }



    IEnumerator checkRadius()
    {
        for (;;)
        {
            lastPlayerRadius = playerRadius;
            playerRadius = 0.0f;
            foreach (GameObject glob in Anchor.GetComponent<globController>().globs)
            {
                if ((Anchor.transform.position - glob.transform.position).sqrMagnitude > (playerRadius * playerRadius) && !isSmashing)
                {
                    playerRadius = (Anchor.transform.position - glob.transform.position).magnitude;
                }
                if (isSmashing)
                {
                    playerRadius = lastPlayerRadius;
                }
            }
            yield return new WaitForSeconds(checkRadiusPeriod);
        }    
    }
}

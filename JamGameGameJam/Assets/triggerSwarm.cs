using UnityEngine;
using System.Collections;

public class triggerSwarm : MonoBehaviour {

    public bool swarm = false;
    public float switchTime = 5.0f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("Timer");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    IEnumerator Timer()
    {
        for (;;)
        {
            swarm = !swarm;
            gameObject.GetComponent<globController>().expanded = swarm;
            yield return new WaitForSeconds(switchTime);
        }

    }
}

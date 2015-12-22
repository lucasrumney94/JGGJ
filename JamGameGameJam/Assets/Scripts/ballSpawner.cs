using UnityEngine;
using System.Collections;

public class ballSpawner : MonoBehaviour {


	
	public GameObject ball;
    public int ballCount;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
        
        if (ballCount < 10000)
        {
            ballCount++;
            Instantiate<GameObject>(ball);
        }
	}
}

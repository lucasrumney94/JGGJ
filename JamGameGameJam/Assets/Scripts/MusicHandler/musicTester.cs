using UnityEngine;
using System.Collections;

public class musicTester : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        musicHandler.play(0);
        musicHandler.playCrossfade(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

using UnityEngine;
using System.Collections;

public class encounterMusic : MonoBehaviour {

    private bool playerInside = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Anchor") && !playerInside)
        {
            playAction();
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Anchor") && playerInside)
        {
            playAmbientMusic();
            playerInside = false;
        }
    }


    void playAction()
    {
        //musicHandler.play(0);
        musicHandler.playCrossfade(1);

    }
    void playAmbientMusic()
    {
        musicHandler.playCrossfade(0);
    }

}

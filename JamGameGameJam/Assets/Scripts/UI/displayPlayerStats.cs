using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayPlayerStats : MonoBehaviour {

    private playerStats pStats;
    private Text myText;

    // Use this for initialization
    void Start ()
    {
        myText = GetComponent<Text>();
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        myText.text = "Globs: " + pStats.playerGlobCount.ToString();
	}
}

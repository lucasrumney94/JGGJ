using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

    public List<GameObject> levels;
    public bool gameWon = false;
    public bool gameLost = false;

    private int numLevelsbeaten = 0;
    private playerStats pStats;
    
    // Use this for initialization
	void Start ()
    {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameWon && !gameLost)
        {
            numLevelsbeaten = 0;
            foreach (GameObject level in levels)
            {
                if (level.GetComponent<LevelController>().levelBeaten)
                {
                    numLevelsbeaten++;
                }
            }
            if (numLevelsbeaten == levels.Count)
            {
                StartCoroutine("gameWinActivation");
            }
            if (pStats.playerGlobCount <= 0)
            {
                gameLost = true;
            }
        }
	}
    IEnumerator gameWinActivation()
    {

        yield return new WaitForSeconds(12.0f);
        gameWon = true;
    }
}

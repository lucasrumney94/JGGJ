using UnityEngine;
using System.Collections;

public class setAnchorParent : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        this.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
	}

}

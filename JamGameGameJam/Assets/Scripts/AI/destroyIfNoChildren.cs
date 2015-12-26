using UnityEngine;
using System.Collections;

public class destroyIfNoChildren : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}

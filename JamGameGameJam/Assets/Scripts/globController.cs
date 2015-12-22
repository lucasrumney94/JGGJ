using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class globController : MonoBehaviour
{

    public GameObject Ball;
    public int ballCount = 200;
    public float clusterAffinity = 100f;

    public List<GameObject> globs = new List<GameObject>();
    int i = 0;
    Vector3 randomOffset;

    private GameObject Anchor;

    // Use this for initialization
    void Start()
    {
        Anchor = GameObject.FindGameObjectWithTag("Anchor");
        for (i = 0; i < ballCount; i++)
        {
            randomOffset = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            globs.Add((GameObject)Instantiate(Ball,randomOffset,Quaternion.identity));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject glob in globs)
        {
            //Debug.Log(glob.transform.parent.position);
            glob.GetComponent<Rigidbody>().AddForce(clusterAffinity * (Anchor.transform.position-glob.transform.position)*Time.smoothDeltaTime);
        }
        
    }
}

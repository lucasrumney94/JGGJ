using UnityEngine;
using System.Collections;

public class AI_Spawner : MonoBehaviour {

    public float intervalTime;

    public GameObject spawnable;

    private float startTime;
    private float lastSpawnTime;

    void Start()
    {
        startTime = Time.time;
        lastSpawnTime = Time.time;
        Spawn();
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > intervalTime)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn()
    {
        Instantiate(spawnable, transform.position, transform.rotation);
    }
}

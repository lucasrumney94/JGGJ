using UnityEngine;
using System.Collections;

public class AI_Spawner : MonoBehaviour {

    public int capacity = 10;
    private int spawned = 0;
    [Range(0,10)]
    public float intervalTime;
    public float initialVelocity;

    public GameObject spawnable;

    private float startTime;
    private float lastSpawnTime;

    void Start()
    {
        startTime = Time.time;
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > intervalTime && spawned > capacity)
        {
            Spawn();
            spawned++;
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn()
    {
        GameObject freshSpawn = Instantiate(spawnable, transform.position, transform.rotation) as GameObject;
        freshSpawn.GetComponent<Rigidbody>().velocity = transform.forward * initialVelocity;
    }
}

using UnityEngine;
using System.Collections;

public class AI_Turret : MonoBehaviour
{
    public float fireDelay;
    public float bulletSpeed;
    public Vector3 muzzlePosition;

    public GameObject bullet;

    private float fireLast;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        fireLast = Time.time;
    }

    void Update()
    {
        if(Time.time - fireLast > fireDelay)
        {
            Fire();
            fireLast = Time.time;
        }
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(bullet, transform.TransformPoint(muzzlePosition), transform.rotation) as GameObject;
        newBullet.GetComponent<Bullet>().speed = bulletSpeed;
    }
}

using UnityEngine;
using System.Collections;

public class AI_Turret : MonoBehaviour
{
    public float fireDelay;
    public float bulletSpeed;
    public Vector3 muzzlePosition;
    public float aimTolerance;

    public float rotationSpeed;
    public bool turretRotation;
    public bool xRotation;
    public bool yRotation;

    public GameObject bullet;

    private float fireLast;

    private GameObject player;
    private Vector3 toPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Anchor");
        fireLast = Time.time;
    }

    void Update()
    {
        toPlayer = GameObject.FindGameObjectWithTag("Anchor").transform.position - transform.position;
        if (turretRotation)
        {
            TurnToFace();
        }

        if(Time.time - fireLast > fireDelay && fireDelay > 0f)
        {
            if (CheckAim())
            {
                Fire();
                fireLast = Time.time;

            }
        }
    }

    private void Fire()
    {
        GameObject newBullet = Instantiate(bullet, transform.TransformPoint(muzzlePosition), transform.rotation) as GameObject;
        newBullet.GetComponent<Bullet>().speed = bulletSpeed;
    }

    private void TurnToFace()
    {
        Debug.DrawRay(transform.position, transform.forward * 1000f, Color.cyan);

        transform.LookAt(GameObject.FindGameObjectWithTag("Anchor").transform);
        transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -10f, 10f), transform.localEulerAngles.y, 0f);
    }

    private bool CheckAim()
    {
        if(Vector3.Angle(transform.forward, toPlayer) < aimTolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

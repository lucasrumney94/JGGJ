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
    public bool limitRotation;
    public bool xRotation;
    public bool yRotation;
    [Range(0f, 360f)]
    public float lowerLimit;
    [Range(0f, 360f)]
    public float upperLimit;

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

    //half broken for now, don't enable angle limiting
    private void TurnToFace()
    {
        //Debug.DrawRay(transform.position, transform.forward * 1000f, Color.cyan);

        if (yRotation)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Anchor").transform);
            transform.localEulerAngles = new Vector3(0f, limitRotation ? Mathf.Clamp(transform.localEulerAngles.y, -lowerLimit, upperLimit) : transform.localEulerAngles.y, 0f);
            //Moved out of vector creation to disable limited movement along x
            //Mathf.Clamp(transform.localEulerAngles.x, -30f, 10f)
        }

        if (xRotation)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Anchor").transform);
            transform.localEulerAngles = new Vector3(limitRotation ? Mathf.Clamp(transform.localEulerAngles.x, -lowerLimit, upperLimit) : transform.localEulerAngles.x, 0f, 0f);
        }
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

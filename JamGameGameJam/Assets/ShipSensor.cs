using UnityEngine;
using System.Collections;

//Detects a ship contacting the collider and destroys the parent object if it does
public class ShipSensor : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Bullet hit a ship");
            Destroy(transform.parent.gameObject);
        }
    }
}

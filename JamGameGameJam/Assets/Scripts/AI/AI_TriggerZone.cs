﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class AI_TriggerZone : MonoBehaviour {

    public GameObject[] toActivate;

    private BoxCollider triggerZone;
    private bool playerInside;

    void Start()
    {
        triggerZone = GetComponent<BoxCollider>();
        triggerZone.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameObject.FindGameObjectWithTag("Anchor") && !playerInside)
        {
            foreach (GameObject inactive in toActivate)
            {
                ActivateObject(inactive);
            }
            playerInside = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Anchor") && playerInside)
        {
            playerInside = false;
        }
    }

    private void ActivateObject(GameObject activatible)
    {
        AI_Pinky orbiterAI = activatible.GetComponent<AI_Pinky>();
        AI_Spawner spawnerAI = activatible.GetComponent<AI_Spawner>();

        if (orbiterAI)
        {
            orbiterAI.active = true;
        }

        if (spawnerAI)
        {
            spawnerAI.active = true;
        }
    }
}

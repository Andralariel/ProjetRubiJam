using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTriggerZone : MonoBehaviour
{
    [SerializeField] private Barrel barrelScript;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject.GetComponent<InteractableObj>().type == Objets.Wheat)
        {
            if (barrelScript.doesContainWheat) return;
            if (barrelScript.isAlcohol) return;
            //Debug.Log("wheat in barrel");
            Destroy(other.gameObject);
            barrelScript.doesContainWheat = true;
            barrelScript.isEmpty = false;
            barrelScript.ChangeBarrelStates();
        }
    }
}

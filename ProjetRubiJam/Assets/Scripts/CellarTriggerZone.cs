using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellarTriggerZone : MonoBehaviour
{
    [SerializeField] private Barrel barrelScript;
    
    private void OnTriggerEnter(Collider barrel)
    {
        if (barrel.gameObject.GetComponent<Barrel>())
        {
            Debug.Log("enter barrel");
            barrelScript = barrel.gameObject.GetComponent<Barrel>();
            barrelScript.isBarrelPlaced = true;
        }
    }
    
    private void OnTriggerExit(Collider barrel)
    {
        Debug.Log("exit barrel");
        barrelScript.isBarrelPlaced = false;
        barrelScript = null;
    }
}

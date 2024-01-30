using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellarTriggerZone : MonoBehaviour
{
    [SerializeField] private Barrel barrelScript;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private GameObject currentBarrel;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject.GetComponent<Barrel>())
        {
            if (currentBarrel != null) return;
            barrelScript = other.gameObject.GetComponent<Barrel>();
            currentBarrel = other.gameObject;
            //Debug.Log("enter barrel");
            barrelScript.isBarrelPlaced = true;
            
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            var otherTransform = other.transform;
            otherTransform.position = transform.position + posOffset;
            otherTransform.rotation = new Quaternion(rotationOffset.x,rotationOffset.y,rotationOffset.z,0);

        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject != currentBarrel) return;
        Debug.Log("exit barrel");
        barrelScript.isBarrelPlaced = false;
        barrelScript = null;
        currentBarrel = null;
    }
}

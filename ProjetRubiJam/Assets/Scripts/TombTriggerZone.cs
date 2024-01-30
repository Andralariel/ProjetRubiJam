using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombTriggerZone : MonoBehaviour
{
    [SerializeField] private Tomb tombScript;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotationOffset;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject.GetComponent<InteractableObj>().type == Objets.Coffin)
        {
            if (tombScript.canDig) return;
            if (tombScript.hasCoffin) return;
            Debug.Log("coffin in tomb");
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            tombScript.currentCoffin = other.gameObject;
            tombScript.currentCoffin.GetComponent<Collider>().enabled = false;
            var otherTransform = other.transform;
            otherTransform.position = transform.position + posOffset;
            otherTransform.rotation = new Quaternion(rotationOffset.x,rotationOffset.y,rotationOffset.z,0);
            tombScript.hasCoffin = true;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CellarTriggerZone : MonoBehaviour
{
    public Image imageToFill;
    
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
            barrelScript.cellar = this;
            currentBarrel = barrelScript.gameObject;
            //Debug.Log("enter barrel");
            barrelScript.isBarrelPlaced = true;
            
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            var otherTransform = other.transform;
            otherTransform.position = transform.position + posOffset;
            otherTransform.rotation = Quaternion.Euler(rotationOffset.x,rotationOffset.y,rotationOffset.z);

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

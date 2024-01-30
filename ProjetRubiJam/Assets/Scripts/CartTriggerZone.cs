using UnityEngine;

public class CartTriggerZone : MonoBehaviour
{
    [SerializeField] private Cart cartScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject.GetComponent<InteractableObj>().type == Objets.Barrel)
        {
            if (!other.gameObject.GetComponent<Barrel>().isAlcohol) return;
            Debug.Log("barrel in cart");
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.transform.rotation = Quaternion.identity;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            
            cartScript.AddBarrelToCart(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}

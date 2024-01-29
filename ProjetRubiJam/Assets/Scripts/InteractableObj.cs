using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public virtual void PressAction()
    {
        Debug.Log("Press Action");
    }
    
    public virtual void ReleaseAction()
    {
        Debug.Log("Release Action");
    }
}

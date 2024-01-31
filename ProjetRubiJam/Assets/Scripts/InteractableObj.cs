using System;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    internal PlayerController _playerInteracting;
    internal Transform _objTransform;
    
    public Rigidbody rbObj;
    public Vector3 positionToHold;
    public float forwardThrowForce = 10;
    public float upwardThrowForce = 5;
    public Objets type = Objets.None;

    private void Awake()
    {
        _objTransform = transform;
    }

    public virtual void PressAction(PlayerController player)
    {
        Debug.Log("Press Action");

        _playerInteracting = player;
        PickUp();
    }

    public virtual void ReleaseAction()
    {
        Debug.Log("Release Action");
        
        Throw();
    }

    private void PickUp()
    {
        rbObj.constraints = RigidbodyConstraints.FreezeAll;
        
        _objTransform = transform;
        _objTransform.parent = _playerInteracting.transform;
        _objTransform.localPosition = positionToHold;
        _objTransform.localRotation = Quaternion.identity;
    }
    
    private void Throw()
    {
        rbObj.constraints = RigidbodyConstraints.None;
        
        _objTransform.parent = null;
        rbObj.AddForce(_objTransform.forward*forwardThrowForce + _objTransform.up*upwardThrowForce,ForceMode.Impulse);
    }
}

public enum Objets
{
    None,
    Wheat,
    Barrel,
    Bell,
    Coffin,
    Bones,
    Player,
}

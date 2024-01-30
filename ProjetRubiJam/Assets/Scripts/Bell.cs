using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : InteractableObj
{
    [SerializeField] private Cart cartScript;

    public List<Transform> barrelReturnPoints = new(4);
    
    public override void PressAction(PlayerController player)
    {
        Debug.Log("Action from bell");
        _playerInteracting = player;
        cartScript.CartDeparture();
    }

    public override void ReleaseAction()
    {
       
    }
}

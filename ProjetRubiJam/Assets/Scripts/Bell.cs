using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : InteractableObj
{
    [SerializeField] private Cart cartScript;
    
    public override void PressAction(PlayerController player)
    {
        Debug.Log("Action from bell");
        _playerInteracting = player;
        CallBell();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void CallBell()
    {
        if (cartScript.isWaiting)
        {
            //if (cartScript.)
        }
    }
}

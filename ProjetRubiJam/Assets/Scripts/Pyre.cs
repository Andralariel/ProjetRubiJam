using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyre : MonoBehaviour
{
    public int nbBonesBurned = 0;
    
    private void Start()
    {
        nbBonesBurned = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        if (other.gameObject.GetComponent<InteractableObj>().type != Objets.Bones) return;
        Debug.Log("Par le feu tu seras sanctifié");
        Destroy(other.gameObject);
        nbBonesBurned++;
        MonkManager.instance.AddFaith(MonkManager.instance.faithWhenBonesBurned);

        //impact sur la jauge de foi ?

        //VFX.Play

    }

 
}

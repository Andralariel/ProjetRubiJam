using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bones : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonesParts;
    
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    void GenerateRandomParts()
    {
        Random.Range(0, bonesParts.Count);
    }
}

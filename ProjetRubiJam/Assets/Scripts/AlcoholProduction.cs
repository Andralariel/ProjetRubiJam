using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholProduction : MonoBehaviour
{
    public static AlcoholProduction instance;
    
    [Header("Wheat")]
    [SerializeField] public GameObject propsWheat;

    
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

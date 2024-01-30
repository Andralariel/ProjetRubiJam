using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] private List<GameObject> barrelsOnCart = new(4);
    [SerializeField] private int sizeCart = 4;
    
    
    void Start()
    {
        barrelsOnCart.Clear();
        barrelsOnCart = new(sizeCart);
    }
    
    void Update()
    {
        
    }

    public void AddBarrelToCart()
    {
        
    }

    void CartDeparture()
    {
        
    }
    
    
}

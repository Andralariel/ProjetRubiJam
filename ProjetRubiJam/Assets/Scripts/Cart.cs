using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [Header("Loading")]
    public List<GameObject> barrelsOnCart = new(6);
    [SerializeField] private List<GameObject> posBarrel = new(6);
    [SerializeField] private int sizeCart = 6;
    public bool isWaiting;

    [Header("Departure")] 
    [SerializeField] private float travelDuration;
    [SerializeField] private float currentTravelTime;
    [SerializeField] private float travelSpeed;
    
    void Start()
    {
        barrelsOnCart.Clear();
        barrelsOnCart = new(0);
    }
    
    void Update()
    {
        
    }

    public void AddBarrelToCart(GameObject gb)
    {
        gb.transform.parent = gameObject.transform;
        barrelsOnCart.Add(gb);
        gb.transform.position = posBarrel[barrelsOnCart.Count - 1].transform.position;
    }

    public void CartDeparture()
    {
        
        
        
        barrelsOnCart.Clear();
        barrelsOnCart = new(0);
    }
    
    
}

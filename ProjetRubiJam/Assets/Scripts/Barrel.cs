using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Brewing")]
    public bool isBarrelPlaced;
    [SerializeField] private bool doesContainWheat;
    [SerializeField] private bool canBrew;
    [SerializeField] private float brewingDuration = 10f;
    [SerializeField] private float brewingSpeed = 1f;
    [SerializeField] private float brewingCurrentTime = 0f;

    [Header("Alcohol")] 
    public bool isAlcohol = false;
    public bool isEmpty = false;
    
    
    void Start()
    {
        canBrew = true;
    }


    void Update()
    {
        if (isBarrelPlaced)
        {
            BrewAlcohol();
        }
    }

    void BrewAlcohol()
    {
        if (canBrew)
        {
            if (doesContainWheat)
            {
                if (brewingCurrentTime >= brewingDuration)
                {
                    Debug.Log("brewing done");
                    doesContainWheat = false;
                    canBrew = false;
                    brewingCurrentTime = 0;

                    isAlcohol = true;
                }
                else
                {
                    brewingCurrentTime += brewingSpeed * Time.deltaTime;
                }
            }
        }
    }
    
}

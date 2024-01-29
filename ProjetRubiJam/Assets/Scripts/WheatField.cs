using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
    [SerializeField] public float wheatGrowth = 100;

    [Header("Growth")] 
    public bool canWheatGrow = true;
    [SerializeField] private float wheatGrowthSpeed = 10f;
    [SerializeField] private float wheatGrowthInterval = 1f;
    [SerializeField] private float wheatcurrentTimeGrowth = 0f;

    [Header("Harvest")] 
    public bool canWheatHarvest = false;
    [SerializeField] private float wheatHarvestSpeed = 1f;
    [SerializeField] private float wheatHarvestCurrentTime = 0f;
    [SerializeField] private float wheatHarvestInterval = 4f;
    
    [Header("Wheat")] 
    [SerializeField] private List<GameObject> listsPropsWheat;
    [SerializeField] private Transform wheatSpawnPoint;
    
    
    void Start()
    {
        canWheatGrow = true;
        canWheatHarvest = false;
    }
    
    void Update()
    {
        GrowWheat();
        
        //DEBUG
        if (Input.GetKey(KeyCode.Space))
        {
            HarvestWheat();
        }
    }

    void GrowWheat()
    {
        if (canWheatGrow)
        {
            if (wheatGrowth < 100)
            {
                if (wheatcurrentTimeGrowth >= wheatGrowthInterval)
                {
                    wheatcurrentTimeGrowth -= wheatGrowthInterval;
                    wheatGrowth += wheatGrowthSpeed; 
                }
                else
                {
                    wheatcurrentTimeGrowth += Time.deltaTime;
                }
            }
            else if (wheatGrowth >= 100f)
            {
                Debug.Log("wheat grown");
                canWheatGrow = false;
                canWheatHarvest = true;
            }
        }
    }
    
    
    public void HarvestWheat()
    {
        if (canWheatHarvest)
        {
            if (wheatHarvestCurrentTime >= wheatHarvestInterval)
            {
                Debug.Log("wheat harvested");
                wheatHarvestCurrentTime -= wheatHarvestInterval;
                
                canWheatGrow = true;
                canWheatHarvest = false;

                wheatGrowth -= wheatGrowth;
            }
            else
            {
                wheatHarvestCurrentTime += Time.deltaTime * wheatHarvestSpeed;
            }
        }
    }
    
}

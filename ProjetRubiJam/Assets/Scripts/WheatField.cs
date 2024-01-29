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
    [SerializeField] private List<GameObject> statesWheat = new(5);

    [Header("Harvest")] 
    public bool canWheatHarvest = false;
    [SerializeField] private float wheatHarvestSpeed = 1f;
    [SerializeField] private float wheatHarvestCurrentTime = 0f;
    [SerializeField] private float wheatHarvestInterval = 4f;
    
    [Header("Wheat")] 
    [SerializeField] private List<GameObject> listsPropsWheat;
    [SerializeField] private Transform wheatSpawnPoint;
    [SerializeField] private Vector3 wheatSpawnOffset = new (0, 1, 0);
    
    
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
                    ChangeWheatState();
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

    void ChangeWheatState()
    {
        switch (wheatGrowth)
        {
            case 20:
                
                break;
            
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
                wheatGrowth -= wheatGrowth;
                
                canWheatGrow = true;
                canWheatHarvest = false;

                var wheatClone = Instantiate(AlcoholProduction.instance.propsWheat, wheatSpawnPoint.position, Quaternion.identity, gameObject.transform);
                listsPropsWheat.Add(wheatClone);
                
                Vector3 newOffset = listsPropsWheat[listsPropsWheat.Count-1].transform.position;
                newOffset += wheatSpawnOffset;
                wheatSpawnPoint.position = newOffset;
            }
            else
            {
                wheatHarvestCurrentTime += Time.deltaTime * wheatHarvestSpeed;
            }
        }
    }
    
}

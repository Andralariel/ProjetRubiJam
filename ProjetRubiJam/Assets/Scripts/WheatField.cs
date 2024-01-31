using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheatField : InteractableObj
{
    [Header("Growth")] 
    [SerializeField] public float wheatGrowth = 100;
    public bool canWheatGrow = true;
    [SerializeField] private float wheatGrowthSpeed = 10f;
    [SerializeField] private float wheatGrowthInterval = 1f;
    [SerializeField] private float wheatCurrentTimeGrowth = 0f;
    [SerializeField] private Image imageToFill;
    [SerializeField] private List<GameObject> statesWheat = new(5);

    [Header("Harvest")] 
    public bool canWheatHarvest = false;
    [SerializeField] private float wheatHarvestSpeed = 1f;
    [SerializeField] private float wheatHarvestCurrentTime = 0f;
    [SerializeField] private float wheatHarvestDuration = 4f;
    
    [Header("Wheat")] 
    [SerializeField] private List<GameObject> listsPropsWheat;
    [SerializeField] private Transform wheatSpawnPoint;
    [SerializeField] private Vector3 wheatSpawnOffset = new (0, 1, 0);

    private bool _playerIsInteracting;
    
    
    void Start()
    {
        canWheatGrow = true;
        canWheatHarvest = false;
        ChangeWheatState();
    }
    
    void Update()
    {
        if (MonkManager.instance.insurrection)
        {
            wheatGrowth = 0;
            wheatHarvestCurrentTime = 0;
        }
        else
        {
            GrowWheat();
            if (!_playerIsInteracting) return;
            HarvestWheat();
        }
    }

    public override void PressAction(PlayerController player)
    {
        Debug.Log("Action from wheat");
        _playerInteracting = player;
        _playerIsInteracting = true;
    }

    public override void ReleaseAction()
    {
        _playerIsInteracting = false;
    }

    void GrowWheat()
    {
        if (canWheatGrow)
        {
            if (wheatGrowth < 100)
            {
                if (wheatCurrentTimeGrowth >= wheatGrowthInterval)
                {
                    wheatCurrentTimeGrowth -= wheatGrowthInterval;
                    wheatGrowth += wheatGrowthSpeed;
                    ChangeWheatState();
                }
                else
                {
                    wheatCurrentTimeGrowth += Time.deltaTime;
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
                statesWheat[0].SetActive(true);
                break;
            case 40:
                statesWheat[0].SetActive(false);
                statesWheat[1].SetActive(true);
                break;
            case 60:
                statesWheat[1].SetActive(false);
                statesWheat[2].SetActive(true);
                break;
            case 80:
                statesWheat[2].SetActive(false);
                statesWheat[3].SetActive(true);
                break;
            case 100:
                statesWheat[3].SetActive(false);
                statesWheat[4].SetActive(true);
                break;
        }
    }
    
    
    public void HarvestWheat()
    {
        if (canWheatHarvest)
        {
            if (wheatHarvestCurrentTime >= wheatHarvestDuration)
            {
                Debug.Log("wheat harvested");
                imageToFill.enabled = false;
                wheatHarvestCurrentTime = 0;
                wheatGrowth = 0;
                ChangeWheatState();
                statesWheat[4].SetActive(false);
                
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
                if(wheatHarvestCurrentTime == 0) imageToFill.enabled = true;
                wheatHarvestCurrentTime += Time.deltaTime * wheatHarvestSpeed;
                imageToFill.fillAmount = wheatHarvestCurrentTime / wheatHarvestDuration;
            }
        }
    }
    
    
}

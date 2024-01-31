using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pray : InteractableObj
{
    [Header("Les Péons")] 
    [SerializeField] private int currentNbPeons = 0;
    [SerializeField] private GameObject peonGb;
    [SerializeField] private List<GameObject> peonsList;
    [SerializeField] private Transform peonSpawnPoint;

    [SerializeField] private bool canSpawnPeons;
    [SerializeField] private int maxPeons = 100;
    [SerializeField] private List<int> rangeNbPeons = new(2){1,4};
    [SerializeField] private float peonsInterval = 10f;
    [SerializeField] private float currentTimerPeons = 0f;
    
    [SerializeField] private Vector2 offsetX = new(-2, 2);
    [SerializeField] private Vector2 offsetZ = new(0, 2);
    
    [Header("Prier")] 
    [SerializeField] private bool canPray;
    [SerializeField] private float prayDuration = 3f;
    [SerializeField] private float currentTimerPray = 0f;
    
    private bool _playerIsInteracting;
    
    void Start()
    {
        canSpawnPeons = true;
        canPray = true;
    }

    
    void Update()
    {
        SpawnPeons();
        
        if (!_playerIsInteracting) return;
        DonnerAmourAuxPeons();
    }
    
    public override void PressAction(PlayerController player)
    {
        Debug.Log("Action from Autel");
        _playerInteracting = player;
        _playerIsInteracting = true;
    }

    public override void ReleaseAction()
    {
        _playerIsInteracting = false;
    }
    
    

    void SpawnPeons()
    {
        if (currentNbPeons > maxPeons) return;
        if (canSpawnPeons)
        {
            if (currentTimerPeons >= peonsInterval)
            {
                currentTimerPeons = 0;
                for (int i = 0; i < Random.Range(rangeNbPeons[0], rangeNbPeons[1]+1); i++)
                {
                    Vector3 pos = peonSpawnPoint.position;
                    pos.x += Random.Range(offsetX.x, offsetX.y);
                    pos.z += Random.Range(offsetZ.x, offsetZ.y);
                    
                    var peonClone = Instantiate(peonGb, pos, Quaternion.identity, transform);
                    peonsList.Add(peonClone);
                    currentNbPeons = peonsList.Count;
                }
            }
            else
            {
                currentTimerPeons += Time.deltaTime;
            }
        }
    }
    
    
    void DonnerAmourAuxPeons()
    {
        if (canPray)
        {
            if (currentTimerPray >= prayDuration)
            {
                currentTimerPray = 0;
                foreach (var peon in peonsList)
                {
                    Destroy(peon);
                }
                peonsList.Clear();
            }
            else
            {
                currentTimerPray += Time.deltaTime;
                foreach (var peon in peonsList)
                {
                    peon.transform.LookAt(transform);
                    
                }
            }
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinSpawner : MonoBehaviour
{
    [Header("Coffin")] 
    [SerializeField] private GameObject coffinGb;
    [SerializeField] private Transform coffinSpawnPoint;
    [SerializeField] private bool canSpawnCoffin;
    [SerializeField] private int currentNbCoffin = 0;
    [SerializeField] private int maxCoffin = 20;
    [SerializeField] private List<GameObject> coffinList;
    [SerializeField] private float coffinInterval = 10f;
    [SerializeField] private float currentTimerCoffin = 0f;
    [SerializeField] private Vector2 offsetX = new(-2, 2);
    [SerializeField] private Vector2 offsetZ = new(0, 2);
    [SerializeField] private Quaternion quarternion = new Quaternion(0, 0, 0, 0);
    
    void Start()
    {
        canSpawnCoffin = true;
    }
    
    void Update()
    {
        SpawnCoffin();
    }

    void SpawnCoffin()
    {
        if (currentNbCoffin > maxCoffin) return;
        if (canSpawnCoffin)
        {
            if (currentTimerCoffin >= coffinInterval)
            {
                currentTimerCoffin = 0;
                
                Vector3 pos = coffinSpawnPoint.position;
                pos.x += Random.Range(offsetX.x, offsetX.y);
                pos.z += Random.Range(offsetZ.x, offsetZ.y);
                    
                var coffinClone = Instantiate(coffinGb, pos, quarternion, transform);
                coffinList.Add(coffinClone);
                currentNbCoffin = coffinList.Count;
            }
            else
            {
                currentTimerCoffin += Time.deltaTime;
            }
        }
    }
}

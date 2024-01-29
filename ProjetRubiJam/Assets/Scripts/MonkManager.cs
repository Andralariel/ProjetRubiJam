using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkManager : MonoBehaviour
{
    public static MonkManager instance;
    
    [Header("Variables")]
    [SerializeField] public float jaugeMoney = 50f;
    [SerializeField] public float jaugePeopleLove = 50f;
    [SerializeField] public float jaugeGodFaith = 50f;

    [SerializeField] private float jaugeDecreaseInterval = 5f;
    [SerializeField] private float currentTimeBeforeDecrease = 0f;
    [SerializeField] private float valueDecreaseMoney = 1f;
    [SerializeField] private float valueDecreaseLove= 1f;
    [SerializeField] private float valueDecreaseFaith = 1f;
    

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
        JaugeManager();
    }


    void JaugeManager()
    {
        if (currentTimeBeforeDecrease >= jaugeDecreaseInterval)
        {
            Debug.Log("diminution des jauges");
            currentTimeBeforeDecrease -= jaugeDecreaseInterval;

            jaugeMoney -= valueDecreaseMoney;
            jaugePeopleLove -= valueDecreaseLove;
            jaugeGodFaith -= valueDecreaseFaith;
            CheckJauges();
        }
        else
        {
            currentTimeBeforeDecrease += Time.deltaTime; 
        }
    }
    
    void CheckJauges()
    {
        if (jaugeMoney <= 0)
        {
            
        }
        else if (jaugePeopleLove <= 0)
        {
            
        }
        else if (jaugeGodFaith <= 0)
        {
            
        }
        else
        {
            Debug.Log("Tout va relativement bien.");
        }
    }

    void EndGame()
    {
        Debug.Log("Game ended");
        Time.timeScale = 0;
        //afficher menu score
    }
    
}

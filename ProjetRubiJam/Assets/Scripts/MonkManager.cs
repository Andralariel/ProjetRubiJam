using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkManager : MonoBehaviour
{
    public static MonkManager instance;
    
    [Header("Variables")]
    [SerializeField] public int jaugeMoney = 50;
    [SerializeField] public int jaugePeopleLove = 50;
    [SerializeField] public int jaugeGodFaith = 50;

    [SerializeField] private float timeBeforeDecrease = 5f;
    [SerializeField] private float currentDurationBeforeDecrease = 0f;
    [SerializeField] private int valueDecreaseMoney = 1;
    [SerializeField] private int valueDecreaseLove= 1;
    [SerializeField] private int valueDecreaseFaith = 1;
    

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
        if (currentDurationBeforeDecrease >= timeBeforeDecrease)
        {
            Debug.Log("diminution des jauges");
            currentDurationBeforeDecrease -= timeBeforeDecrease;

            jaugeMoney -= valueDecreaseMoney;
            jaugePeopleLove -= valueDecreaseLove;
            jaugeGodFaith -= valueDecreaseFaith;
            CheckJauges();
        }
        else
        {
            currentDurationBeforeDecrease += Time.deltaTime; 
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
    }

    void EndGame()
    {
        Debug.Log("Game ended");
        Time.timeScale = 0;
        //afficher menu score
    }
    
}

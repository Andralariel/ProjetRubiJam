using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Common")]
    public bool isEmpty = true;
    [SerializeField] private List<GameObject> barrelStates = new(3);
    [SerializeField] private MeshRenderer barrelDebug;
    
    [Header("Brewing")]
    public bool isBarrelPlaced;
    public bool doesContainWheat;
    public bool canBrew;
    [SerializeField] private float brewingDuration = 10f;
    [SerializeField] private float brewingSpeed = 1f;
    [SerializeField] private float brewingCurrentTime = 0f;

    [Header("Alcohol")] 
    public bool isAlcohol = false;

    [HideInInspector] public CellarTriggerZone cellar;

    [SerializeField] private ParticleSystem vfxready;
    void Start()
    {
        canBrew = true;
        isEmpty = true;
        ChangeBarrelStates();
        barrelDebug.enabled = false;
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
                    cellar.imageToFill.enabled = false;

                    isAlcohol = true;
                    ChangeBarrelStates();
                }
                else
                {
                    if(brewingCurrentTime == 0) cellar.imageToFill.enabled = true;
                    brewingCurrentTime += brewingSpeed * Time.deltaTime;
                    cellar.imageToFill.fillAmount = brewingCurrentTime / brewingDuration;
                }
            }
        }
    }

    public void ChangeBarrelStates()
    {
        foreach (var state in barrelStates)
        {
            state.SetActive(false);
        }
        
        if (isEmpty)
        {
            //Debug.Log("vide");
            barrelStates[0].SetActive(true);
        }
        else if (doesContainWheat)
        {
            //Debug.Log("blé");
            barrelStates[1].SetActive(true);
        }
        else if (isAlcohol)
        {
            //Debug.Log("glou");
            barrelStates[2].SetActive(true);
            vfxready.Play();
        }
    }
}

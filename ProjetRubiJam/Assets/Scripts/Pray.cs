using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Pray : InteractableObj
{
    [SerializeField] private Image imageToFill;
    
    [Header("Les Péons")] 
    [SerializeField] private int currentNbPeons = 0;
    [SerializeField] private GameObject peonGb;
    [SerializeField] private List<GameObject> peonsList;
    [SerializeField] private Transform peonSpawnPoint;

    [SerializeField] private bool canSpawnPeons;
    [SerializeField] private int maxPeons = 100;
    [SerializeField] private List<int> rangeNbPeons = new(2){1,4};
    [SerializeField] private float peonsInterval = 10f;
    [SerializeField] private float peonsIntervalWhenTerror = 3f;
    [SerializeField] private float currentTimerPeons = 0f;
    
    [SerializeField] private Vector2 offsetX = new(-2, 2);
    [SerializeField] private Vector2 offsetZ = new(0, 2);
    
    [Header("Prier")] 
    [SerializeField] private bool canPray;
    [SerializeField] private float prayDuration = 3f;
    [SerializeField] private float currentTimerPray = 0f;

    [Header("VFX")] 
    [SerializeField] private VisualEffect vfxpraying;
    [SerializeField] private ParticleSystem vfxpop;

    [SerializeField] private List<GameObject> listvfxpop;

    private bool ispraying;
    private bool _playerIsInteracting;
    private float _currentInterval;
    
    void Start()
    {
        canSpawnPeons = true;
        canPray = true;
    }

    
    void Update()
    {
        _currentInterval = MonkManager.instance.terreur ? peonsIntervalWhenTerror : peonsInterval;
        SpawnPeons();

        if (!_playerIsInteracting) currentTimerPray = 0;
        else DonnerAmourAuxPeons();
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
        ispraying = false;
        vfxpraying.Reinit();
    }
    
    

    void SpawnPeons()
    {
        if (currentNbPeons > maxPeons) return;
        if (canSpawnPeons)
        {
            if (currentTimerPeons >= _currentInterval)
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
                    var clonepop = Instantiate(vfxpop,peon.transform.position,quaternion.identity);
                    clonepop.gameObject.SetActive(true);
                    listvfxpop.Add(clonepop.gameObject);
                    clonepop.Play();
                    vfxpop.Play();
                    Destroy(peon);
                    
                }
                DestroyVfx();
                MonkManager.instance.AddLove(MonkManager.instance.loveWhenPray * peonsList.Count);
                peonsList.Clear();
                imageToFill.enabled = true; 
                ispraying = false;
                vfxpraying.Reinit();
            }
            else
            {
                if (!ispraying)
                {
                    vfxpraying.Reinit();
                    vfxpraying.SetFloat("PrayingTime", prayDuration-currentTimerPray);
                    vfxpraying.Play();
                    ispraying = true;
                    
                }
                foreach (var peon in peonsList)
                {
                    peon.transform.LookAt(transform);
                }
                if(currentTimerPray == 0) imageToFill.enabled = true;
                currentTimerPray += Time.deltaTime;
                imageToFill.fillAmount = currentTimerPray / prayDuration;
            }
        }
    }

    void DestroyVfx()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSecondsRealtime(1f);
        foreach (var vfx in listvfxpop)
        {
            Destroy(vfx);
        }
    }
}

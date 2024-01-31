using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tomb : InteractableObj
{
    public Image imageToFill;
    public float holeProgress = 100f;
    
    [Header("Digging")]
    public bool canDig = true;
    [SerializeField] private float diggingSpeed = 10f;
    [SerializeField] private float diggingInterval = 1f;
    [SerializeField] private float diggingCurrentTime = 0f;
    [SerializeField] private List<GameObject> diggingStates = new(2);

    [Header("Cover")] 
    public bool canCover = false;
    public bool hasCoffin;
    [SerializeField] private float coverSpeed = 25f;
    [SerializeField] private float coverInterval = 1f;
    [SerializeField] private float coverCurrentTime = 0f;
    public GameObject currentCoffin;
    
    [Header("Bones")] 
    [SerializeField] private List<GameObject> listsPropsBones;
    [SerializeField] private Transform bonesSpawnPoint;
    //[SerializeField] private Vector3 bonesSpawnOffset = new (0, 1, 0);

    [Header("VFX")] 
    [SerializeField] private ParticleSystem vfxdirt;

    private bool isDirt;
    
    private bool _playerIsInteracting;

    void Start()
    {
        ChangeHoleState();
    }
    
    void Update()
    {
        CoverHole();
        if (!_playerIsInteracting) return;
        DigHole();
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
        vfxdirt.Stop();
        isDirt = false;
    }

    void DigHole()
    {
        if (canDig)
        {
            if (holeProgress > 0)
            {
                if (diggingCurrentTime >= diggingInterval)
                {
                    Debug.Log("digging...");
                    diggingCurrentTime -= diggingInterval;
                    
                    if(holeProgress >= 100) imageToFill.enabled = true;
                    holeProgress -= diggingSpeed;
                    imageToFill.fillAmount = (100-holeProgress) / 100;
                }
                else
                {
                    diggingCurrentTime += Time.deltaTime;
                    if (!isDirt)
                    {
                        vfxdirt.Play();
                        isDirt = true;
                    }
                }
            }
            else if (holeProgress <= 0)
            {
                vfxdirt.Stop();
                isDirt = false;
                imageToFill.enabled = false;
                Debug.Log("hole dug");
                ChangeHoleState();
                canDig = false;
                canCover = true;
                
                SpawnBones();
            }
        }
    }

    void CoverHole()
    {
        if (canCover && hasCoffin)
        {
            if (holeProgress < 100)
            {
                if (coverCurrentTime >= coverInterval)
                {
                    Debug.Log("covering...");
                    coverCurrentTime -= coverInterval;
                    
                    if(holeProgress == 0) imageToFill.enabled = true;
                    holeProgress += coverSpeed;
                    imageToFill.fillAmount = holeProgress / 100;
                }
                else
                {
                    coverCurrentTime += Time.deltaTime;
                    if (!isDirt)
                    {
                        vfxdirt.Play();
                        isDirt = true;
                    }
                }
            }
            else if (holeProgress >= 100)
            {
                Debug.Log("hole covered");
                ChangeHoleState();
                canCover = false;
                canDig = true;
                imageToFill.enabled = false;
                vfxdirt.Stop();
                isDirt = false;

                hasCoffin = false;
                Destroy(currentCoffin);
                MonkManager.instance.AddFaith(MonkManager.instance.faithWhenCoffinCovered);
            }
        }
    }

    void ChangeHoleState()
    {
        switch (holeProgress)
        {
            case 0:
                diggingStates[0].SetActive(true);
                diggingStates[1].SetActive(false);
                break;
            case 100:
                diggingStates[1].SetActive(true);
                diggingStates[0].SetActive(false);
                break;
        }
    }

    void SpawnBones()
    {
        int index = Random.Range(0, listsPropsBones.Count);
        var boneClone = Instantiate(listsPropsBones[index], bonesSpawnPoint.position, Quaternion.identity, gameObject.transform);
        boneClone.SetActive(true);
    }
    
}

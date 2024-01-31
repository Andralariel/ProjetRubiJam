using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : InteractableObj
{
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
    
    private bool _playerIsInteracting;

    void Start()
    {
        ChangeHoleState();
    }
    
    void Update()
    {
        if (!_playerIsInteracting) return;
        DigHole();
        CoverHole();
        
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
                    holeProgress -= diggingSpeed;
                }
                else
                {
                    diggingCurrentTime += Time.deltaTime;
                }
            }
            else if (holeProgress <= 0)
            {
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
                    holeProgress += coverSpeed;
                }
                else
                {
                    coverCurrentTime += Time.deltaTime;
                }
            }
            else if (holeProgress >= 100)
            {
                Debug.Log("hole covered");
                ChangeHoleState();
                canCover = false;
                canDig = true;

                hasCoffin = false;
                Destroy(currentCoffin);
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

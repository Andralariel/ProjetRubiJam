using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [Header("Loading")]
    public List<GameObject> barrelsOnCart = new(6);
    [SerializeField] private List<GameObject> posBarrel = new(6);
    public bool isWaiting;

    [Header("Departure")] 
    [SerializeField] private float travelDuration = 5f;
    [SerializeField] private float currentTravelTime;
    [SerializeField] private float travelSpeed = 1f;

    [Header("Return")] 
    [SerializeField] private Bell bellScript;
    
    void Start()
    {
        barrelsOnCart.Clear();
        barrelsOnCart = new(0);

        isWaiting = true;
    }
    
    void Update()
    {
        if (!isWaiting)
        {
            CartTravel();
        }
        
    }

    public void AddBarrelToCart(GameObject gb)
    {
        gb.transform.parent = gameObject.transform;
        barrelsOnCart.Add(gb);
        gb.transform.position = posBarrel[barrelsOnCart.Count - 1].transform.position;
    }

    public void CartDeparture()
    {
        if (isWaiting)
        {
            if (barrelsOnCart.Count > 0)
            {
                MonkManager.instance.AddMoney(barrelsOnCart.Count * MonkManager.instance.priceBarrel);
                //move Cart forward
                isWaiting = false;
                Debug.Log("Cart gone");
            }
            else
            {
                Debug.Log("no barrel on cart");
            }
        }
    }

    void CartTravel()
    {
        if (currentTravelTime >= travelDuration)
        {
            Debug.Log("Cart revient");
            currentTravelTime = 0;
            isWaiting = true;
            CartReturn();
        }
        else
        {
            currentTravelTime += travelSpeed * Time.deltaTime;
        }
    }

    void CartReturn()
    {
        //move Cart backward
        
        for (int i = 0; i < barrelsOnCart.Count; i++)
        {
            var barrelScript = barrelsOnCart[i].GetComponent<Barrel>();
            barrelScript.canBrew = true;
            barrelScript.isEmpty = true;
            barrelScript.isAlcohol = false;
            barrelScript.ChangeBarrelStates();
            barrelsOnCart[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            barrelsOnCart[i].GetComponent<CapsuleCollider>().enabled = true;
            barrelsOnCart[i].transform.parent = null;
            barrelsOnCart[i].transform.position = bellScript.barrelReturnPoints[i].position;
            
            //StartCoroutine(RespawnCoroutine(i, duration));
        }
        barrelsOnCart.Clear();
        barrelsOnCart = new(0);
    }

    private IEnumerator RespawnCoroutine(int size, float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        for (int i = 0; i < size; i++)
        {
            
        }
        
    }
    
    
    
}

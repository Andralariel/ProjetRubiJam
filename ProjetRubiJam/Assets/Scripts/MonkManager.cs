using UnityEngine;
using UnityEngine.UI;

public class MonkManager : MonoBehaviour
{
    public static MonkManager instance;
    
    [Header("Common")]
    [SerializeField] private float jaugeDecreaseInterval = 5f;
    [SerializeField] private float currentTimeBeforeDecrease = 0f;

    [Header("Argent")]
    [SerializeField] private Image gaugeMoney;
    [SerializeField] private float maxMoney = 50f;
    [SerializeField] private float valueDecreaseMoney = 1f;
    public int priceBarrel = 20;
    
    [Header("Amour du peuple")]
    [SerializeField] private Image gaugeLove;
    [SerializeField] private float maxLove = 50f;
    [SerializeField] private float valueDecreaseLove= 1f;
    
    [Header("Foi envers Dieu")]
    [SerializeField] private Image gaugeFaith;
    [SerializeField] private float maxFaith = 50f;
    [SerializeField] private float valueDecreaseFaith = 1f;

    private float _currentMoney;
    private float _currentLove;
    private float _currentFaith;
    
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            _currentMoney = maxMoney;
            _currentLove = maxLove;
            _currentFaith = maxFaith;
            UpdateGauge();
        }
    }
    
    private void Update()
    {
        JaugeManager();
        UpdateGauge();
    }
    
    private void JaugeManager()
    {
        if (currentTimeBeforeDecrease >= jaugeDecreaseInterval)
        {
            currentTimeBeforeDecrease -= jaugeDecreaseInterval;

            _currentMoney -= valueDecreaseMoney;
            _currentLove -= valueDecreaseLove;
            _currentFaith -= valueDecreaseFaith;
            CheckJauges();
        }
        else
        {
            currentTimeBeforeDecrease += Time.deltaTime;
        }
    }
    
    private void CheckJauges()
    {
        if (_currentMoney <= 0)
        {
            
        }
        
        if (_currentLove <= 0)
        {
            
        }
        
        if (_currentFaith <= 0)
        {
            
        }
    }

    void EndGame()
    {
        Debug.Log("Game ended");
        Time.timeScale = 0;
        //afficher menu score
    }

    public void AddMoney(int money)
    {
        _currentMoney += money;
        if (_currentMoney >= maxMoney) _currentMoney = maxMoney;
    }

    private void UpdateGauge()
    {
        gaugeMoney.fillAmount = _currentMoney/maxMoney;
        gaugeLove.fillAmount = _currentLove/maxLove;
        gaugeFaith.fillAmount = _currentFaith/maxFaith;
    }
}

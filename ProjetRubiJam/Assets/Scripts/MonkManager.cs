using UnityEngine;
using UnityEngine.UI;

public class MonkManager : MonoBehaviour
{
    public static MonkManager instance;
    
    [Header("Common")]
    [SerializeField] private float jaugeDecreaseInterval = 5f;
    [SerializeField] private float currentTimeBeforeDecrease;
    [SerializeField] private float tickBufferAfterIncrease = 2;

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
    private float _moneyBuffer;
    
    private float _currentLove;
    private float _loveBuffer;
    
    private float _currentFaith;
    private float _faithBuffer;
    
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
    }
    
    private void JaugeManager()
    {
        if (currentTimeBeforeDecrease >= jaugeDecreaseInterval)
        {
            currentTimeBeforeDecrease -= jaugeDecreaseInterval;

            if (_moneyBuffer > 0) _moneyBuffer--;
            else
            {
                _currentMoney -= valueDecreaseMoney;
                if (_currentMoney < 0) _currentMoney = 0;
            }
            
            if (_loveBuffer > 0) _loveBuffer--;
            else
            {
                _currentLove -= valueDecreaseLove;
                if (_currentLove < 0) _currentLove = 0;
            }
            
            if (_faithBuffer > 0) _faithBuffer--;
            else
            {
                _currentFaith -= valueDecreaseFaith;
                if (_currentFaith < 0) _currentFaith = 0;
            }
            
            CheckJauges();
            UpdateGauge();
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
    
    private void UpdateGauge()
    {
        gaugeMoney.fillAmount = _currentMoney/maxMoney;
        gaugeLove.fillAmount = _currentLove/maxLove;
        gaugeFaith.fillAmount = _currentFaith/maxFaith;
    }
    
    public void AddMoney(int money)
    {
        _currentMoney += money;
        if (_currentMoney > maxMoney) _currentMoney = maxMoney;
        
        _moneyBuffer = tickBufferAfterIncrease;
    }
    
    public void AddLove(int love)
    {
        _currentLove += love;
        if (_currentLove > maxLove) _currentLove = maxLove;
        
        _loveBuffer = tickBufferAfterIncrease;
    }
    
    public void AddFaith(int faith)
    {
        _currentFaith += faith;
        if (_currentFaith > maxFaith) _currentFaith = maxFaith;

        _faithBuffer = tickBufferAfterIncrease;
    }
}

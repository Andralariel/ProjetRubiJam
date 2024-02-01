using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Event = Events.Event;

public class MonkManager : MonoBehaviour
{
    public static MonkManager instance;
    [HideInInspector] public bool gameStopped;

    [Header("Common")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI textEvents;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int gameDurationInSeconds = 300;
    [SerializeField] private float jaugeDecreaseInterval = 5f;
    [SerializeField] private float currentTimeBeforeDecrease;
    [SerializeField] private float tickBufferAfterIncrease = 2;
    [SerializeField] private float eventDurations = 20;
    [SerializeField] private float minDurationBetweenEvents = 20;
    [SerializeField] private float maxDurationBetweenEvents = 40;

    [Header("Argent")]
    [SerializeField] private Image gaugeMoney;
    [SerializeField] private float maxMoney = 50f;
    [SerializeField] private float valueDecreaseMoney = 1f;
    public int priceBarrel = 30;
    
    [Header("Amour du peuple")]
    [SerializeField] private Image gaugeLove;
    [SerializeField] private float maxLove = 50f;
    [SerializeField] private float valueDecreaseLove= 1f;
    public int loveWhenPray = 10;
    
    [Header("Foi envers Dieu")]
    [SerializeField] private Image gaugeFaith;
    [SerializeField] private float maxFaith = 50f;
    [SerializeField] private float valueDecreaseFaith = 1f;
    public int faithWhenBonesBurned = 20;
    public int faithWhenCoffinCovered = 10;

    [Header("Events")]
    [SerializeField] private Event moneyEvent;
    [SerializeField] private Event loveEvent;
    [SerializeField] private Event faithEvent;
    [SerializeField] private List<Event> otherEvents;

    [HideInInspector] public bool insurrection;
    [HideInInspector] public bool famine;
    [HideInInspector] public bool terreur;
    
    public List<PlayerController> playerList;
    public List<GameObject> pointForChickens;
    public Image imageForEvents;
    public Sprite famineSprite; 
    public Sprite insurrectionSprite; 
    public Sprite terreurSprite;
    public Sprite gameoverSprite;

    [Header("Random Wheat")] 
    [SerializeField] private List<WheatField> wheatFields;
    
    private float _currentMoney;
    private float _moneyBuffer;
    
    private float _currentLove;
    private float _loveBuffer;
    
    private float _currentFaith;
    private float _faithBuffer;

    private List<Event> _eventsToPlay;
    private Event _currentEvent;
    private float _eventTimer;
    private bool _playingAnEvent;
    
    private float _generalTimer;
    private bool _firstEvent;
    
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

            _eventsToPlay = new List<Event>();
            _eventTimer = -minDurationBetweenEvents;
            _generalTimer = gameDurationInSeconds;
            UpdateTimerText();

            mainMenu.SetActive(true);
            gameUI.SetActive(false);
            restartButton.SetActive(false);
            gameStopped = true;

            RandomWheatSpawn();
        }
        Time.timeScale = 0;
    }
    
    private void Update()
    {
        if (gameStopped) return;
        GeneralTimerUpdate();
        EventTimer();
        JaugeManager();
    }

    private void GeneralTimerUpdate()
    {
        _generalTimer -= Time.deltaTime;
        if (_generalTimer <= 0)
        {
            _generalTimer = 0;
            EndGame();
        }
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        var minutes = (int)_generalTimer / 60;
        var seconds = (int)_generalTimer % 60;
        if (seconds < 10)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
        
    }
    
    private void EventTimer()
    {
        if (!_firstEvent) return;
        
        _eventTimer += Time.deltaTime;
        if (_playingAnEvent)
        {
            if (_eventTimer < eventDurations + minDurationBetweenEvents) return;
            _eventTimer = 0;
            _playingAnEvent = false;
            _currentEvent.EndEvent();
        }
        else
        {
            if (_eventTimer < maxDurationBetweenEvents - minDurationBetweenEvents) return;
            StartEvent();
        }
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
                if (_currentMoney <= 0) _currentMoney = 0;
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
        if(_currentMoney==0 && _currentLove == 0 && _currentFaith == 0) EndGame();
        else if(_currentMoney < maxMoney/3 || _currentLove < maxLove/3 || _currentFaith < maxFaith/3) StartEvent();
    }

    void EndGame()
    {
        gameStopped = true;
        DOTween.KillAll();
        
        StartCoroutine(Wait());
    }
    
    private IEnumerator Wait()
    {
        imageForEvents.sprite = gameoverSprite;
        var color = imageForEvents.color;
        imageForEvents.color = new Color(color.r, color.g, color.b, 1);
        yield return new WaitForSecondsRealtime(5f);
        imageForEvents.color = new Color(color.r, color.g, color.b, 0);
        Restart();
    }

    

    private void StartEvent()
    {
        if (_playingAnEvent) return;

        _firstEvent = true;
        _playingAnEvent = true;
        _eventTimer = 0;
        _eventsToPlay.Clear();
        
        if(_currentMoney < maxMoney/3) _eventsToPlay.Add(moneyEvent);
        if(_currentLove < maxLove/3) _eventsToPlay.Add(loveEvent);
        if(_currentFaith < maxFaith/3) _eventsToPlay.Add(faithEvent);

        if (otherEvents.Count > 0)
        {
            if (Random.Range(0, _eventsToPlay.Count+1) == 0)
            {
                _currentEvent = otherEvents[Random.Range(0, otherEvents.Count)];
                _currentEvent.StartEvent();
                otherEvents.Remove(_currentEvent);
                //ShowTextEvent();
            }
            else
            {
                _currentEvent = _eventsToPlay[Random.Range(0, _eventsToPlay.Count)];
                _currentEvent.StartEvent();
                //ShowTextEvent();
            }
        }
        else
        {
            if (_eventsToPlay.Count == 0) return;
            
            _currentEvent = _eventsToPlay[Random.Range(0, _eventsToPlay.Count)];
            _currentEvent.StartEvent();
            //ShowTextEvent();
        }
    }

    private void ShowTextEvent()
    {
        textEvents.text = _currentEvent.textToShow;
        textEvents.DOFade(1, 0.25f).OnComplete(() => textEvents.DOFade(0, 0.25f));
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

    public void AddPlayerToList(PlayerController player)
    {
        playerList.Add(player);
        if (pointForChickens.Count == 0) return;
        
        pointForChickens[0].transform.SetParent(player.transform,false);
        //pointForChickens[0].transform.localPosition = Vector3.zero;
        pointForChickens.RemoveAt(0);
    }
    
    //Method for UI Button
    public void StartGame()
    {
        gameStopped = false;
        mainMenu.SetActive(false);
        gameUI.SetActive(true);

        Time.timeScale = 1;
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void RandomWheatSpawn()
    {
        if (wheatFields[0] == null) return;
        
        foreach (var field in wheatFields)
        {
            field.wheatGrowth = Random.Range(1, 4) * 20;
            field.wheatCurrentTimeGrowth = Random.Range(0f,1.3f);
        }
        
    }
}

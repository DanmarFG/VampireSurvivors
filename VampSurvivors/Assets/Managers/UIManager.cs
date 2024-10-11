using System.Collections;
using GMStates;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Slider ExpBar;
    public float maxExp = 10f;
    public int currentLevel = 0;
    
    [SerializeField]
    private GameObject _levelUpScreen;

    [SerializeField] TMP_Text currentCoinText, countDownText;
    public static GameObject levelUpScreen { get; private set; }
    private void Start()
    {
        currentLevel++;
        ExpBar.maxValue = maxExp;
        EventManager.Instance.OnAddExperience += AddExperience;
        EventManager.Instance.OnCoinCollected += AddCoins;
        EventManager.Instance.OnStartLadderEvent += StartLadderCountDown;

        currentCoinText.text = GameManager.Instance.currentCoinCount + "$";

        levelUpScreen = _levelUpScreen;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnAddExperience -= AddExperience;
        EventManager.Instance.OnCoinCollected -= AddCoins;
        EventManager.Instance.OnStartLadderEvent -= StartLadderCountDown;
    }

    public void AddCoins()
    {
        currentCoinText.text = GameManager.Instance.currentCoinCount + "$";
    }

    void AddExperience(float exp)
    {
        ExpBar.value += exp;

        if (ExpBar.value >= maxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        ExpBar.value = 0.0f;

        maxExp += Mathf.Ceil(maxExp * 0.1f);
        ExpBar.maxValue = maxExp;


        GameManager.Instance.LevelUp();
    }

    public static void ShowLevelUpScreen()
    {
        //Do animation here, this is temp
        levelUpScreen.SetActive(true);
    }

    public static void CloseLevelUpScreen()
    {
        //Do animation here, this is temp
        levelUpScreen.SetActive(false); 
    }

    void FinishLevelUp()
    {
        maxExp += maxExp * currentLevel;
        
        ExpBar.maxValue = maxExp;
        currentLevel++;
        GameManager.Instance.ChangeState(new STGamePlay());
        
    }

    int countDown = 20;

    void StartLadderCountDown()
    {
        countDown = 20;

        countDownText.gameObject.SetActive(true);

        StartCoroutine(DoCountDown());
    }

    IEnumerator DoCountDown()
    {
        while (countDown > 0)
        {
            yield return new WaitForSeconds(1f);
            countDown--;

            countDownText.text = countDown.ToString();
        }

        countDownText.gameObject.SetActive(false);
    }
}

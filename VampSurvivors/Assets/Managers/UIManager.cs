using System;
using System.Collections;
using System.Collections.Generic;
using GMStates;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider ExpBar;
    public float maxExp = 10f;
    public int currentLevel = 0;
    
    [SerializeField]
    private GameObject _levelUpScreen;

    [SerializeField] TMP_Text currentCoinText;
    public static GameObject levelUpScreen { get; private set; }
    private void Start()
    {
        currentLevel++;
        ExpBar.maxValue = maxExp;
        EventManager.Instance.OnAddExperience += AddExperience;
        EventManager.Instance.OnCoinCollected += AddCoins;

        levelUpScreen = _levelUpScreen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExperience(1f);
        }
    }

    void AddCoins()
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
}

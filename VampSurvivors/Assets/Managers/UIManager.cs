using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider ExpBar;
    public float maxExp = 10f;
    public int currentLevel = 0;

    private void Start()
    {
        currentLevel++;
        ExpBar.maxValue = maxExp;
        EventManager.Instance.OnAddExperience += AddExperience;
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
        
        GameManager.Instance.LevelUpState();
    }

    void FinishLevelUp()
    {
        maxExp += maxExp * currentLevel;
        
        ExpBar.maxValue = maxExp;
        currentLevel++;
        GameManager.Instance.StartGame();
        
    }
}

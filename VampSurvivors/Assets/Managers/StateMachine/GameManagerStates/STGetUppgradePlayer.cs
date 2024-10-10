using Managers;
using States;
using UnityEngine;

public class STGetUppgradePlayer : IState
{
    public void OnEnter()
    {
        Time.timeScale = 0.0f;

        GameManager.Instance.isInLevelUp = true;

        UIManager.levelUpScreen.SetActive(true);
        GameManager.Instance.gameIsPaused = true;
    }

    public void UpdateState()
    {

    }

    public void OnExit()
    {
        GameManager.Instance.isInLevelUp = false;
        UIManager.levelUpScreen.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.gameIsPaused = false;
    }
}

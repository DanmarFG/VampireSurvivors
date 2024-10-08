using System.Collections;
using System.Collections.Generic;
using Managers;
using States;
using UnityEngine;

namespace GMStates
{
    public class STLevelUp : IState
    {
        public void OnEnter()
        {
            Time.timeScale = 0.0f;

            GameManager.Instance.isInLevelUp = true;

            UnitManager.Instance.player.GetComponent<Player>().LevelUp();
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
}


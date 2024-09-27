using System.Collections;
using System.Collections.Generic;
using Managers;
using States;
using UnityEngine;

namespace GMStates
{
    public class STPauseGame : IState
    {
        public void OnEnter()
        {
            Time.timeScale = 0;
            GameManager.Instance.gameIsPaused = true;
        }

        public void UpdateState()
        {
        }

        public void OnExit()
        {
            Time.timeScale = 1;
            GameManager.Instance.gameIsPaused = false;
        }
    }
}

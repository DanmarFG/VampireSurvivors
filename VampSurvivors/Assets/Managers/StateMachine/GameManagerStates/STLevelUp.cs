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

            UnitManager.Instance.player.GetComponent<Player>().LevelUp();
        }

        public void UpdateState()
        {
            
        }

        public void OnExit()
        {
            Time.timeScale = 1.0f;
        }
    }
}


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
            UnitManager.Instance.player.GetComponent<Player>().LevelUp();

            //Play level upp animation here
        }

        public void UpdateState()
        {
            
        }

        public void OnExit()
        {
        }
    }
}


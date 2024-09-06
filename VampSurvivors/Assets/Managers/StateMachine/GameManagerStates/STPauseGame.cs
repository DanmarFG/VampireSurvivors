using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

namespace GMStates
{
    public class STPauseGame : IState
    {
        public void OnEnter()
        {
            Time.timeScale = 0;
        }

        public void UpdateState()
        {
        }

        public void OnExit()
        {
            Time.timeScale = 1;
        }
    }
}

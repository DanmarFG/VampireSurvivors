using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using States;

namespace GMStates
{
    public class STMainMenu : IState
    {
        public void OnEnter()
        {
            GameManager.LoadSceneAsync();
        }

        public void UpdateState()
        {
            
        }

        public void OnExit()
        {
            GameManager.LoadSceneAsync(2);
           GameManager.UnLoadSceneAsync(); 
        }
    }  
}



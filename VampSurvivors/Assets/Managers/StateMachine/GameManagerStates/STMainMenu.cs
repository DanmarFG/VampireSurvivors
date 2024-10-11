using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using States;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMStates
{
    public class STMainMenu : IState
    {
        GameObject startGameButton;

        public void OnEnter()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            startGameButton = GameObject.Find("StartGameButton");
            startGameButton.GetComponent<Button>().onClick.AddListener(LoadGamePlay);
            startGameButton.GetComponent<Button>().onClick.AddListener(EventManager.Instance.RunStarted);

            GameManager.Instance.ResetGame();
        }

        void LoadGamePlay()
        {
            GameManager.Instance.ChangeState(new STLoadScene(2));
        }

        public void UpdateState()
        {
            
        }

        public void OnExit()
        {
            startGameButton.GetComponent<Button>().onClick.RemoveAllListeners();

        }
    }  
}



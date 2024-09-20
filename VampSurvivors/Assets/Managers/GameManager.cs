using System;
using System.Collections;
using System.Collections.Generic;
using GMStates;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        bool startGame = false;
        
        [SerializeField]
        private StateController stateController;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            if (startGame)
            {
                LoadSceneAsync(2);
                stateController.SetStartState(new STGamePlay());
            }
            else
#endif
                stateController.SetStartState(new STMainMenu());

            
        }

        public void StartGame()
        {
            stateController.ChangeState(new STGamePlay());
        }

        public static void LoadSceneAsync(int buildIndex = 1)
        {
            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        }
        
        public static void UnLoadSceneAsync(int buildIndex = 1)
        {
            SceneManager.UnloadSceneAsync(buildIndex);
        }

        public void LevelUpState()
        {
            stateController.ChangeState(new STLevelUp());
        }
    } 
}


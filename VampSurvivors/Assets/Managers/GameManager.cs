using GMStates;
using Managers;
using States;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class IngameTimer
{
    public int seconds { get; private set; }
    public int minutes{ get; private set; }
    public int hours{ get; private set; }

    private int difficultyScaling = 0;

    bool timerIsOn = false;

    public IngameTimer()
    {
        seconds = 0;
        minutes = 0;
        hours = 0;
    }

public IEnumerator StartTimer()
    {
        timerIsOn = true;
        while (timerIsOn)
        {
            yield return new WaitForSeconds(1f);
            seconds++;
                EventManager.Instance.SecondHappened();
            difficultyScaling++;
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
                EventManager.Instance.MinuteHappened();
            }
            if (minutes >= 60)
            {
                hours++;
                minutes = 0;
            }

            if(difficultyScaling >= 270)
            {
                GameManager.Instance.currentDifficulty++;
                difficultyScaling = 0;
            }
        }
    }

    public void StopTimer()
    {
        timerIsOn = false;
    }

    public void ResetTimer()
    {
        seconds = 0;
        minutes = 0;
        hours = 0;
    }
    
}

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool gameIsPaused = false, isInLevelUp = false;
        
        [SerializeField]
        private StateController stateController;
        
        public Stats alltimeStats;
        public IngameTimer IngameTimer;

        [Space]
        [Header("Ingame")]
        public int currentCoinCount = 0;
        public int currentChestCount = 5;
        public int stagesCompleted = 0;
        public int currentDifficulty = 1;

        [Space]
        [Header("GameScaling")]
        public float coef = 0;
        public float playerFactor = 1;
        public float timeFactor = 0;
        public float stageFactor = 0;
        public int playerLevel = 0;


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

            IngameTimer = new IngameTimer();

            stateController.SetStartState(new STLoadScene(1));

            EventManager.Instance.OnPauseGame += PauseGame;

            SetAndGetStats();
        }

        public void SetAndGetStats()
        {
            if (!PlayerPrefs.HasKey("Kills"))
                PlayerPrefs.SetInt("Kills", 0);

            if (!PlayerPrefs.HasKey("Runs"))
                PlayerPrefs.SetInt("Runs", 0);

            if (!PlayerPrefs.HasKey("CoinsCollected"))
                PlayerPrefs.SetInt("CoinsCollected", 0);

            if (!PlayerPrefs.HasKey("StagesCompleted"))
                PlayerPrefs.SetInt("StagesCompleted", 0);

            alltimeStats.kills = PlayerPrefs.GetInt("Kills");
            alltimeStats.runs = PlayerPrefs.GetInt("Runs");
            alltimeStats.coinsCollected = PlayerPrefs.GetInt("CoinsCollected");

            EventManager.Instance.OnCoinCollected += AddCoinCollected;
            EventManager.Instance.OnEnemyDeath += AddKill;
            EventManager.Instance.OnRunStarted += AddRun;
            EventManager.Instance.OnStageCompleted += AddStagesCompleted;
            EventManager.Instance.OnplayerLevelUp += PlayerLevelUp;
        }

        public void ChangeState(IState state)
        {
            stateController.ChangeState(state);
        }

        public void LevelUp()
        {
            stateController.ChangeState(new STLevelUp());
        }

        public void PauseGame()
        {
            if(Time.timeScale != 0)
                stateController.ChangeState(new STPauseGame());
            else
                stateController.ChangeState(new STGamePlay());
        }

        int totalCoinsCollected = 0;
        void AddCoinCollected()
        {
            alltimeStats.coinsCollected++;
            currentCoinCount++;
            totalCoinsCollected++;
            if (totalCoinsCollected % 10 == 0)
                EventManager.Instance.TenCoinsPickup();

            PlayerPrefs.SetInt("CoinsCollected", alltimeStats.coinsCollected);

        }

        void AddKill()
        {
            alltimeStats.kills++;
            PlayerPrefs.SetInt("Kills", alltimeStats.kills);
        }

        void AddRun()
        {
            alltimeStats.runs++;
            PlayerPrefs.SetInt("Runs", alltimeStats.runs);
        }

        void AddStagesCompleted()
        {
            stagesCompleted++;
            alltimeStats.stagesCompleted++;
            PlayerPrefs.SetInt("Runs", alltimeStats.stagesCompleted);
        }

        public void StartTimer()
        {
            StartCoroutine(IngameTimer.StartTimer());
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        public void ResetGame()
        {
            stagesCompleted = 0;
            currentCoinCount = 0;
            currentChestCount = 5;
            currentDifficulty = 1;
            playerLevel = 0;
        }

        public void PlayerLevelUp()
        {
            playerLevel++;
        }
    }

    public struct Stats
    {
        public int kills;
        public int coinsCollected;
        public int runs;
        public int stagesCompleted;
    }
}


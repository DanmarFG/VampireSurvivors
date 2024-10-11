using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;
    
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
        }
        
        public event Action<float> OnPlayerTookDamage;
        public event Action OnPlayerDied, OnplayerLevelUp;

        public void PlayerTookDamage(float damage)
        {
            OnPlayerTookDamage?.Invoke(damage);
        }

        public void PlayerLevelUp()
        {
            OnplayerLevelUp?.Invoke();
        }

        public void PlayerDied()
        {
            OnPlayerDied?.Invoke();
        }

        public event Action<float> OnAddExperience;

        public void AddExperience(float experience)
        {            
            OnAddExperience?.Invoke(experience);
        }

        public event Action OnPauseGame, OnGamePlay;

        public void PauseGame()
        {
            OnPauseGame?.Invoke();
        }

        public void GamePlay()
        {
            OnGamePlay?.Invoke();
        }

        public event Action OnNavMeshBuilt;

        public void NavMeshFinished()
        {
            OnNavMeshBuilt?.Invoke();
        }

        public event Action OnEnemyDeath, OnCoinCollected, OnRunStarted, OnStageCompleted;
        public void EnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }

        public void CoinCollected()
        {
            OnCoinCollected?.Invoke();
        }

        public void RunStarted()
        {
            OnRunStarted?.Invoke();
        }

        public void StageComplete()
        {
            OnStageCompleted?.Invoke();
        }

        public event Action OnStartLadderEvent;

        public void StartLadderEvent()
        {
            OnStartLadderEvent?.Invoke();
        }

        public event Action OnFinishedLadderEvent;

        public void FinishedLadderEvent()
        {
            OnFinishedLadderEvent?.Invoke();
        }

        public event Action OnSecond, OnMinute;

        public void SecondHappened()
        {
            OnSecond?.Invoke();
        }

        public void MinuteHappened()
        {
            OnMinute?.Invoke();
        }

        public event Action OnTenCoinsPickup;

        public void TenCoinsPickup()
        {
            OnTenCoinsPickup?.Invoke();
        }
    } 
}


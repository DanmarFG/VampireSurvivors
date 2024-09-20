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
        public event Action OnPlayerDied;

        public void PlayerTookDamage(float damage)
        {
            OnPlayerTookDamage?.Invoke(damage);
        }

        public void PlayerDied()
        {
            OnPlayerDied?.Invoke();
        }

        public event Action<float> OnAddExperience;

        public void AddExperience(float experience)
        {
            Debug.Log("AddExperience");
            
            OnAddExperience?.Invoke(experience);
        }
    } 
}


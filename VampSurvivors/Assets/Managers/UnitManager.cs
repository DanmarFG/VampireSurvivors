using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

        public List<Unit> unitData = new List<Unit>();

        public List<Unit> enemies = new List<Unit>();

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
    
    }
}



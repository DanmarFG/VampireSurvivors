using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class StateController : MonoBehaviour
    {
        private States.IState _currentState;

        void Update()
        {
            _currentState.UpdateState();
        }

        public void SetStartState(States.IState newState)
        {
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void ChangeState(States.IState newState)
        {
            _currentState.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }
    }
}

namespace States
{
    public interface IState
    {
        public void OnEnter();

        public void UpdateState();

        public void OnExit();
    }
}



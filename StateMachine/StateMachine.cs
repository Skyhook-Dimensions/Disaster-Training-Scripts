using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        private State _nextState;
        private State _mainState;

        private void Awake()
        {
            SetNextStateToMain();
        }

        private void Update()
        {
            if (_nextState != null)
            {
                SetState(_nextState);
                _nextState = null;
            }
            
            if (CurrentState != null)
            {
                CurrentState.UpdateState();
            }
        }
        
        private void SetState(State nextState)
        {
            CurrentState?.OnExitState();
            CurrentState = nextState;
            CurrentState?.OnEnterState(this);
        }
        
        public void SetNextState(State newState)
        {
            _nextState = newState;
        }

        public void SetNextStateToMain()
        {
            _nextState = _mainState;
        }

        private void OnValidate()
        {
            if (_mainState == null)
            {
                // TODO: _mainState = new EarthquakeState(); 
            }
        }
    }
}

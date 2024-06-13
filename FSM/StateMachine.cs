using EarthQuake.EarthquakeStateMachine;
using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        private State _nextState;
        private State _defaultState;

        private void Start()
        {
            SetNextStateToDefault();
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
        
        public void SwitchToNextState(State newState)
        {
            _nextState = newState;
        }

        public void SetNextStateToDefault()
        {
            _nextState = _defaultState;
        }
        
        public void SetDefaultState(State state)
        {
            _defaultState = state;
        }
    }
}

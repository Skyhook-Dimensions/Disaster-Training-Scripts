using FSM;
using UnityEngine;

namespace EarthQuake.EarthquakeStateMachine
{
    public class PostEq : EarthQuakeBaseState
    {
        private float _stateDuration = 5f;
        public override void OnEnterState(StateMachine newStateMachine)
        {
            base.OnEnterState(newStateMachine);
            Debug.Log("PostEq Enter");
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("PostEq Update");
            if (CurrentTime >= _stateDuration)
            {
                stateMachine.SetNextStateToDefault();
            }
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("PostEq Exit");
        }
    }
}
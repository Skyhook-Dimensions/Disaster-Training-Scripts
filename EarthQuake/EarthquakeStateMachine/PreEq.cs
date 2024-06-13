using UnityEngine;
using FSM;

namespace EarthQuake.EarthquakeStateMachine
{
    public class PreEq : EarthQuakeBaseState
    {
        private float _stateDuration = 5f;
        public override void OnEnterState(StateMachine newStateMachine)
        {
            base.OnEnterState(newStateMachine);
            Debug.Log("PreEq Enter");
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("PreEq Update");
            if (CurrentTime >= _stateDuration)
            {
                stateMachine.SwitchToNextState(new DuringEq());
            }
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("PreEq Exit");
        }
    }
}

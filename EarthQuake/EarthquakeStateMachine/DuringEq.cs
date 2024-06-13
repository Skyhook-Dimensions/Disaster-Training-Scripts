using FSM;
using UnityEngine;

namespace EarthQuake.EarthquakeStateMachine
{
    public class DuringEq : EarthQuakeBaseState
    {
        private float _stateDuration = 5f;
        public override void OnEnterState(StateMachine newStateMachine)
        {
            base.OnEnterState(newStateMachine);
            Debug.Log("During Eq Enter");
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("During Eq Update");
            if (CurrentTime >= _stateDuration)
            {
                stateMachine.SwitchToNextState(new PostEq());
            }
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("During Eq Exit");
        }
    }
}
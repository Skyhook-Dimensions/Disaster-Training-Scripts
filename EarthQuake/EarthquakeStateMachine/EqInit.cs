using FSM;
using UnityEngine;

namespace EarthQuake.EarthquakeStateMachine
{
    public class EqInit : EarthQuakeBaseState
    {
        private float _stateDuration = 1f;
        public override void OnEnterState(StateMachine newStateMachine)
        {
            base.OnEnterState(newStateMachine);
            Debug.Log("Init Enter");
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Init Update");
            if (CurrentTime >= _stateDuration)
            {
                stateMachine.SwitchToNextState(new PreEq());
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("Init Exit");
        }
    }
}

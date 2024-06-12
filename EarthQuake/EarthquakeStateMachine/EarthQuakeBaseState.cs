using FSM;

namespace EarthQuake.EarthquakeStateMachine
{
    public class EarthQuakeBaseState : State
    {
        public float duration;
        // Todo: Add the class that stores the progress report of the player to update.
        
        public override void OnEnterState(StateMachine newStateMachine)
        {
            base.OnEnterState(newStateMachine);
        }
    
        public override void UpdateState()
        {
            base.UpdateState();
        
        }
    
        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}

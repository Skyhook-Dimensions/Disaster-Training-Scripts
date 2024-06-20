using FSM;

namespace EarthQuake.EarthquakeStateMachine
{
    public class EqBaseState : BaseState
    {
        public float CurrentTime { get; protected set; }
    }
}

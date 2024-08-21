namespace EarthQuake.EqStateMachine
{
    /// <summary>
    /// This is an empty state for the state machine to default to.
    /// </summary>
    public class IdleEq : EqBaseState
    {
        public IdleEq(float duration) : base(duration)
        {
        }
    }
}
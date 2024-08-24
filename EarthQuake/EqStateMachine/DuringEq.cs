using EarthQuake.Managers;

namespace EarthQuake.EqStateMachine
{
    public class DuringEq : EqBaseState
    {
        public DuringEq(float duration) : base(duration)
        {
        }

        public override void OnExit()
        {
            base.OnEnter();
            GameManagerEq.Instance.PrevState = typeof(DuringEq);
        }
    }
}
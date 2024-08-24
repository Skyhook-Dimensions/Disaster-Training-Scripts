using EarthQuake.Managers;

namespace EarthQuake.EqStateMachine
{
    public class PreEq : EqBaseState
    {
        public PreEq(float duration) : base(duration)
        {
        }

        public override void OnExit()
        {
            base.OnExit();
            GameManagerEq.Instance.PrevState = typeof(PreEq);
        }
    }
}
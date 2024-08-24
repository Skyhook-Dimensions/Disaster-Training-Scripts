using EarthQuake.Managers;

namespace EarthQuake.EqStateMachine
{
    public class PostEq : EqBaseState
    {
        public PostEq(float duration) : base(duration)
        {
        }

        public override void OnExit()
        {
            base.OnEnter();
            GameManagerEq.Instance.PrevState = typeof(PostEq);
        }
    }
}
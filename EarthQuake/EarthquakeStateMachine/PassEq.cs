namespace Eq.EqStateMachine
{
    public class PassEq : EqBaseState
    {
        public PassEq(float duration) : base(duration)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            UnityEngine.Debug.Log(this.GetType() + " Enter");
        }

        public override void OnExit()
        {
            base.OnExit();
            UnityEngine.Debug.Log(this.GetType() + " Exit");
        }
    }
}
using UnityEngine;
using FSM;

namespace Eq.EqStateMachine
{
    public class PreEq : EqBaseState
    {
        public PreEq(float duration) : base(duration)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log(this.GetType() + " Enter");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log(this.GetType() + " Exit");
        }
    }
}
using System;
using FSM;
using UnityEngine;

namespace Eq.EqStateMachine
{
    public class DuringEq : EqBaseState
    {
        public DuringEq(float duration) : base(duration)
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
using FSM;
using UnityEngine;

namespace EarthQuake.EqStateMachine
{
    public class EqBaseState : BaseState
    {
        public float CurrentTime { get; protected set; }
        public float Duration { get; protected set; }

        protected EqBaseState(float duration)
        {
            Duration = duration;
        }

        public override void OnEnter()
        {
            CurrentTime = 0;
        }

        public override void OnUpdate()
        {
            CurrentTime += Time.deltaTime;
        }
    }
}
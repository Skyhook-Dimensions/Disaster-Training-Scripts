using Eq.EqStateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class EqStateEventListener : EventListener<EqBaseState>
    {
        public void Test(EqBaseState state)
        {
            Debug.Log(state.GetType());
        }
    }
}
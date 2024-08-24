using EarthQuake.EqStateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class EqStateEventListener : EventListener<EqBaseState>
    {
        public TextMeshProUGUI text;
        public void Test(EqBaseState state)
        {
            text.text = state.GetType().ToString();
        }
    }
}
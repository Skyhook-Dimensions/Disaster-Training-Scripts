using EarthQuake.EqStateMachine;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = "Events/StateEventChannel")]
    public class EqStateEventChannel : EventChannel<EqBaseState>
    {
    }
}
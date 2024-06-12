using EarthQuake.EarthquakeStateMachine;
using FSM;
using Utils;
using UnityEngine;

namespace EarthQuake
{
    public class GameManagerEq : GenericSingleton<GameManagerEq>
    {
        [SerializeField] private StateMachine _stateMachine;
        
        protected override void Awake()
        {
            base.Awake();
            if (_stateMachine == null)
            {
                _stateMachine = this.GetComponent<StateMachine>();
            }
            _stateMachine.SetDefaultState(new IdleEarthQuakeState());
        }
    }
}

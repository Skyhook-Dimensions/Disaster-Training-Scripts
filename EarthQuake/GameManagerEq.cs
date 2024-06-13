using System;
using FSM;
using Utils;
using UnityEngine;
using EarthQuake.EarthquakeStateMachine;

namespace EarthQuake
{
    [RequireComponent(typeof(StateMachine))]
    public class GameManagerEq : GenericSingleton<GameManagerEq>
    {
        [SerializeField] private StateMachine _stateMachine;
        private EqProgressReport _eqProgressReport;
        
        protected override void Awake()
        {
            base.Awake();
            if (_stateMachine == null)
            {
                _stateMachine = this.GetComponent<StateMachine>();
            }
            _stateMachine.SetDefaultState(new IdleEarthQuakeState());
            _eqProgressReport = new EqProgressReport(); 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && _stateMachine.CurrentState is IdleEarthQuakeState)
            {
                _stateMachine.SwitchToNextState(new EqInit());
                
            }
        }
    }
}

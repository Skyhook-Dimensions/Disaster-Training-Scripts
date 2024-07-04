using System;
using Eq.EqStateMachine;
using EventSystem;
using Utils;
using UnityEngine;
using FSM;

// ReSharper disable CheckNamespace
namespace EarthQuake
{
    [RequireComponent(typeof(StateMachine))]
    public class GameManagerEq : GenericSingleton<GameManagerEq>
    {
        #region EqState properties

        [Header("Earthquake State properties"), Space(10)][SerializeField] private float _preEqDuration = 5f;
        [SerializeField] private float _duringEqDuration = 5f;
        [SerializeField] private float _postEqDuration = 5f;
        [SerializeField] private float _passEqDuration = 5f;
        [SerializeField] private float _eqInitDuration = 5f;
        // [SerializeField] private float _failEqDuration = 5f;

        #endregion EqState properties

        #region Event Channels

        [Header("Event Channels"), Space(10)][SerializeField] private EqStateEventChannel _eqStateEventChannel;

        #endregion Event Channels

        private StateMachine _stateMachine;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = GetComponent<StateMachine>();

            // declare states

            var eqInit = new EqInit(_eqInitDuration);
            var preEq = new PreEq(_preEqDuration);
            var duringEq = new DuringEq(_duringEqDuration);
            var postEq = new PostEq(_postEqDuration);
            var passEq = new PassEq(_passEqDuration);
            var idleEq = new IdleEq(Mathf.Infinity);
            // var failEq = new FailEq(_failEqDuration);

            // define transitions
            At(eqInit, preEq, CreateFuncPredicate(eqInit));
            At(preEq, duringEq, CreateFuncPredicate(preEq));
            At(duringEq, postEq, CreateFuncPredicate(duringEq));
            At(postEq, passEq, CreateFuncPredicate(postEq));
            At(passEq, idleEq, CreateFuncPredicate(passEq));

            // Any(failEq, new FuncPredicate(() => true));

            // set initial state
            _stateMachine.SetState(eqInit);
        }

        private void Start()
        {
            _stateMachine.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(IState newState)
        {
            if (_eqStateEventChannel == null) return;
            _eqStateEventChannel.Invoke((EqBaseState)newState);
        }

        private void Update()
        {
            _stateMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            _stateMachine.OnLateUpdate();
        }

        private FuncPredicate CreateFuncPredicate(EqBaseState currentRunningState)
        {
            var tempPredicate = new FuncPredicate(() => currentRunningState.CurrentTime >= currentRunningState.Duration);
            return tempPredicate;
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}
using EarthQuake.EqStateMachine;
using EventSystem;
using FSM;
using TripleA.Singletons;
using UnityEngine;

namespace EarthQuake.Managers
{
    [RequireComponent(typeof(StateMachine))]
    public class GameManagerEq : GenericSingleton<GameManagerEq>
    {
        #region EqState properties
        
        [Header("Earthquake State properties"), Space(10)][SerializeField] private float m_preEqDuration = 5f;
        [SerializeField] private float m_duringEqDuration = 5f;
        [SerializeField] private float m_postEqDuration = 5f;
        [SerializeField] private float m_passEqDuration = 5f;
        [SerializeField] private float m_eqInitDuration = 5f;
        // [SerializeField] private float m_failEqDuration = 5f;

        #endregion EqState properties

        #region Event Channels

        [Header("Event Channels")][SerializeField] private EqStateEventChannel m_eqStateEventChannel;

        #endregion Event Channels

        private StateMachine m_stateMachine;

        protected override void Awake()
        {
            base.Awake();
            m_stateMachine = GetComponent<StateMachine>();

            // declare states

            var eqInit = new EqInit(m_eqInitDuration);
            var preEq = new PreEq(m_preEqDuration);
            var duringEq = new DuringEq(m_duringEqDuration);
            var postEq = new PostEq(m_postEqDuration);
            var passEq = new PassEq(m_passEqDuration);
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
            m_stateMachine.SetState(eqInit);
        }

        private void OnEnable()
        {
            m_stateMachine.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            m_stateMachine.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(IState newState)
        {
            if (m_eqStateEventChannel == null) return;
            m_eqStateEventChannel.Invoke((EqBaseState)newState);
        }

        private void Update()
        {
            m_stateMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            m_stateMachine.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            m_stateMachine.OnLateUpdate();
        }

        private FuncPredicate CreateFuncPredicate(EqBaseState currentRunningState)
        {
            var tempPredicate = new FuncPredicate(() => currentRunningState.CurrentTime >= currentRunningState.Duration);
            return tempPredicate;
        }

        private void At(IState from, IState to, IPredicate condition) => m_stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
    }
}
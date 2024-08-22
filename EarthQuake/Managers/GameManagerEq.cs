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
        
        public float DuringEqDuration => m_duringEqDuration;

        #endregion EqState properties

        #region Event Channels

        [Header("Event Channels")][SerializeField] private EqStateEventChannel m_eqStateEventChannel;

        #endregion Event Channels

        #region StateTransitionBools

        public bool Passed { get; set; }
        public bool Retry { get; set; }
        public bool Failed { get; set; }
        public bool ShouldTransitionToDuringEq { get; set; }
        public bool Pause { get; set; }

        #endregion
        
        private StateMachine m_stateMachine;
        private EqBaseState m_prevState;

        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            m_stateMachine = GetComponent<StateMachine>();

            ResetBools();
            
            // declare states

            var init = new EqInit(m_eqInitDuration);
            var pre = new PreEq(m_preEqDuration);
            var during = new DuringEq(m_duringEqDuration);
            var post = new PostEq(m_postEqDuration);
            var pass = new PassEq(m_passEqDuration);
            var idle = new IdleEq(Mathf.Infinity);
            var fail = new FailEq(Mathf.Infinity);
            var pause = new PauseEq(Mathf.Infinity);

            // define transitions
            AddTransition(init, pre, CreateFuncPredicate(init));
            AddTransition(pre, during, new FuncPredicate(() => ShouldTransitionToDuringEq));
            AddTransition(during, post, new FuncPredicate(() => 
                !Failed && 
                during.CurrentTime >= during.Duration));
            
            // pass transition
            AddTransition(post, pass, new FuncPredicate(() => !Failed && Passed));
            AddTransition(pass, idle, CreateFuncPredicate(pass));
            
            // fail transition
            AddTransition(during, fail, new FuncPredicate(() =>
                Failed &&
                during.CurrentTime >= during.Duration));
            AddTransition(post, fail, new FuncPredicate(() => Failed));
            
            // retry transition
            AddTransition(fail, pre, new FuncPredicate(() => Retry));

            // PauseTransition
            AddAnyTransition(pause, new FuncPredicate(() =>
            {
                m_prevState = (EqBaseState)m_stateMachine.CurrentState;
                return Pause;
            }));
            AddTransition(pause, m_prevState, new FuncPredicate(() => !Pause));

            // set initial state
            m_stateMachine.SetState(init);
        }

        private void OnEnable()
        {
            m_stateMachine.onStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            m_stateMachine.onStateChanged -= OnStateChanged;
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

        #endregion UnityMethods

        #region PrivateMethods
        
        private void ResetBools()
        {
            ShouldTransitionToDuringEq = false;
            Passed = false;
            Retry = false;
            Failed = false;
        }

        private FuncPredicate CreateFuncPredicate(EqBaseState currentRunningState)
        {
            var tempPredicate = new FuncPredicate(() => currentRunningState.CurrentTime >= currentRunningState.Duration);
            return tempPredicate;
        }

        private void AddTransition(IState from, IState to, IPredicate condition) => m_stateMachine.AddTransition(from, to, condition);

        private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);

        #endregion PrivateMethods
    }
}
using System;
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
        
        [Header("Earthquake State properties")][SerializeField] private float m_preEqDuration = 5f;
        [SerializeField] private float m_duringEqDuration = 5f;
        [SerializeField] private float m_postEqDuration = 5f;
        [SerializeField] private float m_passEqDuration = 5f;
        [SerializeField] private float m_eqInitDuration = 5f;
        
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
        // public bool Paused { get; set; }

        #endregion
        
        private StateMachine m_stateMachine;
        // private PauseEq m_pauseEq;
        
        public IState PrevState { get; private set; }

        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            m_stateMachine = GetComponent<StateMachine>();

            ResetBools();
            
            InitialiseStateMachine();
        }

        private void OnEnable()
        {
            m_stateMachine.onStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            m_stateMachine.onStateChanged -= OnStateChanged;
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

        private void OnStateChanged(IState newState)
        {
            if (m_eqStateEventChannel == null) return;
            m_eqStateEventChannel.Invoke((EqBaseState)newState);
        }
        
        private void ResetBools()
        {
            ShouldTransitionToDuringEq = false;
            Passed = false;
            Retry = false;
            Failed = false;
        }
        
        private void InitialiseStateMachine()
        {
            // TODO: test all transitions
            // TODO: !!IMPORTANT!! make a state handler to implement pause and reset
            // declare states
            
            var init = new EqInit(m_eqInitDuration);
            var pre = new PreEq(m_preEqDuration);
            var during = new DuringEq(m_duringEqDuration);
            var post = new PostEq(m_postEqDuration);
            var pass = new PassEq(m_passEqDuration);
            var idle = new IdleEq(Mathf.Infinity);
            var fail = new FailEq(Mathf.Infinity);
            // m_pauseEq = new PauseEq(Mathf.Infinity);
            var reset = new ResetEq(m_eqInitDuration);
            // TODO: make exit state?? to dispose and clean up memory

            // define transitions
            AddTransition(init, pre, CreateFuncPredicate(init));
            AddTransition(pre, during, new FuncPredicate(() => ShouldTransitionToDuringEq));
            AddTransition(during, post, new FuncPredicate(() => !Failed && 
                                                                during.CurrentTime >= during.Duration));
            
            // pass transition
            AddTransition(post, pass, new FuncPredicate(() => !Failed && Passed));
            AddTransition(pass, idle, CreateFuncPredicate(pass));
            
            // fail transition
            AddTransition(during, fail, new FuncPredicate(() => Failed));
            AddTransition(post, fail, new FuncPredicate(() => Failed));
            
            // reset transition
            AddTransition(fail, reset, new FuncPredicate(() => Retry));
            // AddTransition(m_pauseEq, reset, new FuncPredicate(() => Retry));
            AddTransition(reset, pre, CreateFuncPredicate(reset));

            // PauseTransition
            // AddTransition(pre, m_pauseEq, SetPrevState(pre));
            // AddTransition(m_pauseEq, pre, CheckPrevState(pre.GetType(), m_prevState.GetType()));
            // AddTransition(during, m_pauseEq, SetPrevState(during));
            // AddTransition(m_pauseEq, during, CheckPrevState(during.GetType(), m_prevState.GetType()));
            // AddTransition(post, m_pauseEq, SetPrevState(post));
            // AddTransition(m_pauseEq, post, CheckPrevState(post.GetType(), m_prevState.GetType()));

            // set initial state
            m_stateMachine.SetState(init);
        }

        private static FuncPredicate CreateFuncPredicate(EqBaseState currentRunningState)
        {
            var predicate = new FuncPredicate(() => currentRunningState.CurrentTime >= currentRunningState.Duration);
            return predicate;
        }

        // private FuncPredicate CheckPrevState(Type stateType, Type prevStateType)
        // {
        //     return new FuncPredicate(() =>
        //     {
        //         PrevState = m_pauseEq;
        //         return Paused && stateType == prevStateType;
        //     });
        // }

        // private FuncPredicate SetPrevState(EqBaseState state)
        // {
        //     return new FuncPredicate(() =>
        //     {
        //         PrevState = state;
        //         return Paused;
        //     });
        // }

        private void AddTransition(IState from, IState to, IPredicate condition) =>
            m_stateMachine.AddTransition(from, to, condition);

        private void AddAnyTransition(IState to, IPredicate condition) =>
            m_stateMachine.AddAnyTransition(to, condition);

        #endregion PrivateMethods
    }
}
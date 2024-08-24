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
        
        [Header("Earthquake State properties")]
        // [SerializeField] private float m_preEqDuration = 5f;
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
        public bool FoundHidingSpot { get; set; }
        public bool ShouldTransitionToDuringEq { get; set; }
        public bool Paused { get; set; }
        public bool Resumed { get; set; }

        #endregion
        
        private StateMachine m_stateMachine;
        
        public Type PrevState { get; set; }

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
        
        public void ResetBools()
        {
            ShouldTransitionToDuringEq = false;
            FoundHidingSpot = false;
            Passed = false;
            Retry = false;
            Failed = false;
            Resumed = false;
        }
        
        private void InitialiseStateMachine()
        {
            // TODO: When returning from pause to prev state, the state restarts, make it so that the state resumes.
            
            // declare states
            var init = new EqInit(m_eqInitDuration);
            var pre = new PreEq(Mathf.Infinity);
            var during = new DuringEq(m_duringEqDuration);
            var post = new PostEq(m_postEqDuration);
            var pass = new PassEq(m_passEqDuration);
            var idle = new IdleEq(Mathf.Infinity);
            var fail = new FailEq(Mathf.Infinity);
            var pauseEq = new PauseEq(Mathf.Infinity);
            var reset = new ResetEq(m_eqInitDuration);
            // TODO: make exit state?? to dispose and clean up memory

            // define transitions
            AddTransition(init, pre, CreateFuncPredicate(init));
            AddTransition(pre, during, new FuncPredicate(() => ShouldTransitionToDuringEq));
            AddTransition(during, post, new FuncPredicate(() => FoundHidingSpot &&
                                                                during.CurrentTime >= during.Duration));
            
            // pass transition
            AddTransition(post, pass, new FuncPredicate(() => !Failed && Passed));
            AddTransition(pass, idle, CreateFuncPredicate(pass));
            
            // fail transition
            AddTransition(during, fail,
                new FuncPredicate(() => (!FoundHidingSpot && during.CurrentTime >= during.Duration) || Failed));
            AddTransition(post, fail, new FuncPredicate(() => Failed || post.CurrentTime >= post.Duration));
            
            // reset transition
            AddTransition(fail, reset, new FuncPredicate(() => Retry));
            AddTransition(pauseEq, reset, new FuncPredicate(() => Retry));
            AddTransition(reset, pre, CreateFuncPredicate(reset));

            // PauseTransition
            AddTransition(pre, pauseEq, new FuncPredicate(() => Paused));
            AddTransition(pauseEq, pre, new FuncPredicate(() => Resumed && pauseEq.PrevState == PrevState));
            AddTransition(during, pauseEq, new FuncPredicate(() => Paused));
            AddTransition(pauseEq, during, new FuncPredicate(() => Resumed && pauseEq.PrevState == PrevState));
            AddTransition(post, pauseEq, new FuncPredicate(() => Paused));
            AddTransition(pauseEq, post, new FuncPredicate(() => Resumed && pauseEq.PrevState == PrevState));

            // set initial state
            m_stateMachine.SetState(init);
        }

        private static FuncPredicate CreateFuncPredicate(EqBaseState currentRunningState)
        {
            var predicate = new FuncPredicate(() => currentRunningState.CurrentTime >= currentRunningState.Duration);
            return predicate;
        }

        private void AddTransition(IState from, IState to, IPredicate condition) =>
            m_stateMachine.AddTransition(from, to, condition);

        private void AddAnyTransition(IState to, IPredicate condition) =>
            m_stateMachine.AddAnyTransition(to, condition);

        #endregion PrivateMethods
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        private StateNode m_current;
        private Dictionary<Type, StateNode> m_nodes = new();
        private HashSet<ITransition> m_anyTransitions = new();

        public IState CurrentState => m_current.State;
        
        // TODO: !!IMPORTANT!! move this event invocation logic to the states' onenter methods
        public Action<IState> onStateChanged;

        public void OnUpdate()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }

            m_current.State?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            m_current.State?.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            m_current.State?.OnLateUpdate();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public void SetState(IState state)
        {
            m_current = m_nodes[state.GetType()];
            m_current.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (m_current.State == state) return;
            var previousState = m_current.State;
            var nextState = m_nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            onStateChanged?.Invoke(nextState);
            m_current = m_nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in m_anyTransitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            foreach (var transition in m_current.Transitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            return null;
        }

        /// <summary>
        /// Adds a transition that can be triggered from any state to the specified state.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition that must be satisfied for the transition to occur.</param>
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            m_anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        /// <summary>
        /// Adds a transition from one state to another state with a specified condition.
        /// </summary>
        /// <param name="from">The state to transition from.</param>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition that must be satisfied for the transition to occur.</param>
        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = m_nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                m_nodes.Add(state.GetType(), node);
            }
            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
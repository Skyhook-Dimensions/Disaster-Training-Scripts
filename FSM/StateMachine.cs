using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public Action<IState> OnStateChanged;

        public void OnUpdate()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }

            _current.State?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            _current.State?.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            _current.State?.OnLateUpdate();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (_current.State == state) return;
            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            OnStateChanged?.Invoke(nextState);
            _current = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            foreach (var transition in _current.Transitions)
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
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
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
            var node = _nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
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
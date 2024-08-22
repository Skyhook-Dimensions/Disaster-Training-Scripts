using System;
using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using Interfaces;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class FallingObjectController: Controller
	{
		[Header("Object Settings")] private List<IFallable> m_fallingObjects = new();

		private float m_maxDelay;

		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(StartAction); 
			// Todo: add pause, resume, stop();
		}

		private void Start()
		{
			m_maxDelay = GameManagerEq.Instance.DuringEqDuration * 2f;
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (IFallable fallable in m_fallingObjects)
			{
				var fallingObject = (FallingObject)fallable;
				fallingObject.StartFalling(m_maxDelay);
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (IFallable fallable in m_fallingObjects)
			{
				var fallingObject = (FallingObject)fallable;
				fallingObject.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			// Todo
			throw new NotImplementedException();
		}

		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (IFallable fallable in m_fallingObjects)
			{
				var fallingObject = (FallingObject)fallable;
				fallingObject.Stop();
			}
		}
		
		protected override void ResetAction(IState state)
		{
			// Todo
		}
	}
}
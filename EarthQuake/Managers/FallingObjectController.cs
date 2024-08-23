﻿using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class FallingObjectController : Controller
	{
		[Header("Object Settings")] [SerializeField]
		private List<FallingObject> m_fallingObjects;

		private float m_maxDelay;

		private void Start()
		{
			m_maxDelay = GameManagerEq.Instance.DuringEqDuration * 2f;
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (FallingObject fallingObject in m_fallingObjects)
			{
				fallingObject.StartFalling(m_maxDelay);
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (FallingObject fallingObject in m_fallingObjects)
			{
				fallingObject.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			return;
			if (!(GameManagerEq.Instance.PrevState.GetType() == typeof(PauseEq))) return;
			foreach (FallingObject fallingObject in m_fallingObjects)
			{
				fallingObject.Resume();
			}
		}

		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (FallingObject fallingObject in m_fallingObjects)
			{
				fallingObject.Stop();
			}
		}

		protected override void ResetAction(IState state)
		{
			if (!(state.GetType() == typeof(ResetEq))) return;
			foreach (FallingObject fallingObject in m_fallingObjects)
			{
				fallingObject.ResetValues();
			}
		}
	}
}
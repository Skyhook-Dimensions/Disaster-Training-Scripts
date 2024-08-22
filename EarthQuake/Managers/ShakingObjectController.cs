using System;
using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using Interfaces;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class ShakingObjectController : Controller
	{
		[Header("Object Settings")] private List<IShakable> m_shakingObjects = new();
		[SerializeField] private Vector3 m_minStrength;
		[SerializeField] private Vector3 m_maxStrength;

		private float m_duration;

		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(StartAction);
			// Todo: add pause, resume, stop();
		}

		private void Start()
		{
			m_duration = GameManagerEq.Instance.DuringEqDuration;
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (IShakable shakable in m_shakingObjects)
			{
				var shakingObject = (ShakingObject)shakable;
				Vector3 strength = Vector3.Lerp(m_minStrength, m_maxStrength, UnityEngine.Random.Range(0f, 1f));
				float delay = UnityEngine.Random.Range(0f, m_duration * 0.9f);
				shakingObject.StartShake(m_duration, strength, delay);
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (IShakable shakable in m_shakingObjects)
			{
				var shakingObject = (ShakingObject)shakable;
				shakingObject.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			// Todo
		}
		
		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (IShakable shakable in m_shakingObjects)
			{
				var shakingObject = (ShakingObject)shakable;
				shakingObject.Stop();
			}
		}

		protected override void ResetAction(IState state)
		{
			// Todo
		}
	}
}
using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class ShakingObjectController : Controller
	{
		[Header("Object Settings")] 
		[SerializeField] private List<ShakingObject> m_shakingObjects;

		private float m_duration;

		private void Start()
		{
			m_duration = GameManagerEq.Instance.DuringEqDuration;
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				float delay = Random.Range(0f, m_duration * 0.9f);
				shakingObject.StartShake(m_duration, delay);
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				shakingObject.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			return;
			if (!(GameManagerEq.Instance.PrevState.GetType() == typeof(PauseEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				shakingObject.Resume();
			}
		}
		
		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				shakingObject.Stop();
			}
		}

		protected override void ResetAction(IState state)
		{
			if (!(state.GetType() == typeof(ResetEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				shakingObject.ResetValues();
			}
		}
	}
}
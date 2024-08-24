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


		protected override void StartAction(IState state)
		{
			float duration = GameManagerEq.Instance.DuringEqDuration;
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (ShakingObject shakingObject in m_shakingObjects)
			{
				float delay = Random.Range(0f, duration * 0.3f);
				shakingObject.StartShake(duration, delay);
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
			if (!(GameManagerEq.Instance.PrevState == typeof(PauseEq))) return;
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
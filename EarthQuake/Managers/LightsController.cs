using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class LightsController : Controller
	{
		[Header("Light Settings")]
		[SerializeField] private List<FlickeringLight> m_flickeringLights;
		[SerializeField] private Vector2 m_flickerInterval = new(0.5f, 1.5f);
		[SerializeField] private Vector2 m_lightIntensity = new(0.5f, 1.5f);

		private void Start()
		{
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.LightIntensity = m_lightIntensity;
				flickeringLight.FlickerInterval = m_flickerInterval;
			}
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.StartLightFlicker();
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			if (!(GameManagerEq.Instance.PrevState == typeof(PauseEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.Resume();
			}
		}

		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.Stop();
			}
		}

		protected override void ResetAction(IState state)
		{
			if (!(state.GetType() == typeof(ResetEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.ResetValues();
			}
		}
	}
}
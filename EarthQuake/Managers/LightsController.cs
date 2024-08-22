using System.Collections.Generic;
using EarthQuake.EqStateMachine;
using FSM;
using Interfaces;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class LightsController : Controller
	{
		[Header("Light Settings")] private List<IFlickerer> m_flickeringLights = new();
		[SerializeField] private Vector2 m_flickerInterval = new(0.5f, 1.5f);
		[SerializeField] private Vector2 m_lightIntensity = new(0.5f, 1.5f);

		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(StartAction);
			// Todo: add pause, resume, stop();
			foreach (IFlickerer flickerer in m_flickeringLights)
			{
				var flickeringLight = (FlickeringLight)flickerer;
				flickeringLight.LightIntensity = m_lightIntensity;
				flickeringLight.FlickerInterval = m_flickerInterval;
			}
		}

		protected override void StartAction(IState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (IFlickerer flickerer in m_flickeringLights)
			{
				var flickeringLight = (FlickeringLight)flickerer;
				flickeringLight.StartLightFlicker();
			}
		}

		protected override void PauseAction(IState state)
		{
			if (!(state.GetType() == typeof(PauseEq))) return;
			foreach (IFlickerer flickerer in m_flickeringLights)
			{
				var flickeringLight = (FlickeringLight)flickerer;
				flickeringLight.Pause();
			}
		}

		protected override void ResumeAction(IState state)
		{
			// Todo
		}

		protected override void StopAction(IState state)
		{
			if (!(state.GetType() == typeof(FailEq) || state.GetType() == typeof(PassEq))) return;
			foreach (IFlickerer flickerer in m_flickeringLights)
			{
				var flickeringLight = (FlickeringLight)flickerer;
				flickeringLight.Stop();
			}
		}

		protected override void ResetAction(IState state)
		{
			// Todo
		}
	}
}
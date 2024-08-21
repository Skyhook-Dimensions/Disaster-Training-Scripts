using EarthQuake.EqStateMachine;
using EventSystem;
using UnityEngine;

namespace EarthQuake.Managers
{
	public class LightsController : EventListener<EqBaseState>
	{
		[Header("Light Settings")]
		[SerializeField] private FlickeringLight[] m_flickeringLights;
		[SerializeField] private Vector2 m_flickerInterval = new(0.5f, 1.5f);
		[SerializeField] private Vector2 m_lightIntensity = new(0.5f, 1.5f);

		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(StartFlicker);
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.LightIntensity = m_lightIntensity;
				flickeringLight.FlickerInterval = m_flickerInterval;
			}
		}

		private void StartFlicker(EqBaseState state)
		{
			if (!(state.GetType() == typeof(DuringEq))) return;
			foreach (FlickeringLight flickeringLight in m_flickeringLights)
			{
				flickeringLight.StartLightFlicker();
			}
		}
	}
}
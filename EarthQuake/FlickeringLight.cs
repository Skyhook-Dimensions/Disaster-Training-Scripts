using System;
using Interfaces;
using TripleA.ImprovedTimer.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EarthQuake
{
	public class FlickeringLight : MonoBehaviour, IFlickerer
	{
		#region FieldsAndProperties

		[SerializeField] private Light m_light;
		public Vector2 FlickerInterval { get; set; }
		public Vector2 LightIntensity { get; set; }

		private CountDownTimer m_timer;
		private float m_initialIntensity;

		#endregion FieldsAndProperties

		#region UnityMethods

		protected void Awake()
		{
			m_timer = new CountDownTimer(Random.Range(FlickerInterval.x, FlickerInterval.y));
			m_initialIntensity = m_light.intensity;
		}

		private void OnEnable()
		{
			m_timer.onTimerStart += TimerStart;
			m_timer.onTimerEnd += TimerEnd;
		}

		private void OnDisable()
		{
			m_timer.onTimerStart -= TimerStart;
			m_timer.onTimerEnd -= TimerEnd;
			m_timer.Dispose();
		}

		#endregion UnityMethods

		#region PublicMethods
		
		public void StartLightFlicker()
		{
			m_timer.Start();
		}

		public void Stop()
		{
			m_timer.Stop();
		}

		public void Pause()
		{
			m_timer.Pause();
		}

		public void Resume()
		{
			m_timer.Resume();
		}

		public void Reset()
		{
			m_timer.Reset();
			// TODO: Lerp or Flicker unnecessarily
			m_light.intensity = m_initialIntensity;
		}

		#endregion
		
		#region PrivateMethods

		private void TimerStart()
		{
			FlickerLight();
		}

		private void TimerEnd()
		{
			m_timer.Reset(Random.Range(FlickerInterval.x, FlickerInterval.y));
			m_timer.Start();
		}

		private void FlickerLight()
		{
			m_light.intensity = Random.Range(LightIntensity.x, LightIntensity.y);
		}

		#endregion PrivateMethods

	}
}

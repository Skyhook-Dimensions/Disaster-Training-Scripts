using EarthQuake.EqStateMachine;
using TripleA.ImprovedTimer.Timers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace EarthQuake
{
	public class FlickeringLight : MonoBehaviour
	{
		[SerializeField] private Light m_light;
		public Vector2 FlickerInterval { get; set; }
		public Vector2 LightIntensity { get; set; }

		private CountDownTimer m_timer;

		#region UnityMethods

		protected void Awake()
		{
			m_timer = new CountDownTimer(Random.Range(FlickerInterval.x, FlickerInterval.y));
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

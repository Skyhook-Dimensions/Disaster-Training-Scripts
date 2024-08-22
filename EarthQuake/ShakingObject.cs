using FSM;
using Interfaces;
using PrimeTween;
using UnityEngine;

namespace EarthQuake
{
	public class ShakingObject : MonoBehaviour, IShakable
	{
		#region FieldsAndProperties

		[Header("PhysicsSettings")]
		[SerializeField] private bool m_shouldUseGravity;
		[SerializeField] private Rigidbody m_rigidbody;

		private Transform m_tr;
		private Vector3 m_initialPosition;
		private Quaternion m_initialRotation;
		private Tween m_shakeTween;

		#endregion FieldsAndProperties

        #region UnityMethods

        private void Start()
        {
	        m_tr = transform;
	        m_initialPosition = m_tr.position;
	        m_initialRotation = m_tr.rotation;
	        if (m_rigidbody == null && m_shouldUseGravity)
	        {
		        m_rigidbody = GetComponent<Rigidbody>();
		        m_rigidbody.useGravity = m_shouldUseGravity;
	        }
        }

        #endregion UnityMethods

		#region PublicMethods

		public void StartShake(float duration, Vector3 strength, float delay = 0f)
		{
			m_shakeTween = Tween.ShakeLocalPosition(
				target: m_tr,
				duration: duration,
				strength: strength,
				startDelay: delay,
				easeBetweenShakes: Ease.Linear
			);
		}

		public void Stop()
		{
			if (m_shakeTween.isAlive) m_shakeTween.Stop();
		}

		public void Pause()
		{
			if (m_shakeTween.isAlive) m_shakeTween.isPaused = true;
		}

		public void Resume()
		{
			if (m_shakeTween.isAlive) m_shakeTween.isPaused = false;
		}
		
		public void Reset()
		{
			// Todo : Lerp back to initial position
			m_tr.SetPositionAndRotation(m_initialPosition, m_initialRotation);
		}

		#endregion PublicMethods
	}
}
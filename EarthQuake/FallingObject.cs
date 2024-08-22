using System;
using FSM;
using Interfaces;
using PrimeTween;
using TripleA.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EarthQuake
{
	[RequireComponent(typeof(Rigidbody), typeof(Collider))]
	public class FallingObject : MonoBehaviour, IFallable
	{
		#region FeildsAndProperties

		[SerializeField] private Transform m_forcePoint;
		[SerializeField] private float m_force = 10f;
		[SerializeField] private Rigidbody m_rigidbody;
		
		private Tween m_delayTween;
		private bool m_isFalling;
		private Vector3 m_initialPosition;
		private Quaternion m_initialRotation;

		#endregion FeildsAndProperties

		#region UnityMethods

		private void Start()
        {
			m_initialPosition = transform.position;
			m_initialRotation = transform.rotation;
        	if (m_rigidbody == null) m_rigidbody = GetComponent<Rigidbody>();
	        m_rigidbody.useGravity = true;
	        m_rigidbody.isKinematic = true;
        }

		#endregion UnityMethods

		#region PulbicMethods

		public void StartFalling(float maxDelay, float minDelay = 0f)
		{
			float delay = Random.Range(minDelay, maxDelay);
			m_delayTween = Tween.Delay(delay, Fall);
		}

		public void Stop()
		{
			if (m_delayTween.isAlive) m_delayTween.Stop();
			m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		public void Pause()
		{
			if (m_delayTween.isAlive) m_delayTween.isPaused = true;
			m_isFalling = m_rigidbody.velocity != Vector3.zero;
			m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		public void Resume()
		{
			if (m_delayTween.isAlive) m_delayTween.isPaused = false;
			m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			if (m_isFalling)
				m_rigidbody.AddForceAtPosition(
					transform.forward.Multiply(m_force, m_force, m_force), 
					m_forcePoint.position, ForceMode.Impulse);
				
		}

		public void Reset()
		{
			m_isFalling = false;
			m_rigidbody.isKinematic = true;
			// Todo : Lerp back to initial position
			transform.SetPositionAndRotation(m_initialPosition, m_initialRotation);
		}

		#endregion PulbicMethods

		#region PrivateMethods

		private void Fall()
        {
			m_rigidbody.isKinematic = false;
        	m_rigidbody.AddForceAtPosition(
        		transform.forward.Multiply(m_force, m_force, m_force), 
        		m_forcePoint.position, ForceMode.Impulse);
        }
		
		#endregion PrivateMethods
	}
}

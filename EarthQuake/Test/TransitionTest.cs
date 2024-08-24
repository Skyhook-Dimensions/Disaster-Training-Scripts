using EarthQuake.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace EarthQuake.Test
{
	public class TransitionTest : MonoBehaviour
	{
		public Button pass;
		public Button fail;
		public Button reset;
		public Button transition;
		public Button pause;
		public Button resume;
		public Button foundHidingSpot;
		
		void Start()
		{
			pass.onClick.AddListener(PassTest);
			fail.onClick.AddListener(FailTest);
			reset.onClick.AddListener(ResetTest);
			transition.onClick.AddListener(Transition);
			pause.onClick.AddListener(Pause);
			foundHidingSpot.onClick.AddListener(FoundHidingSpot);
			resume.onClick.AddListener(Resume);
		}

		private void FoundHidingSpot()
		{
			GameManagerEq.Instance.FoundHidingSpot = !GameManagerEq.Instance.FoundHidingSpot;
		}

		void PassTest()
		{
			GameManagerEq.Instance.Passed = !GameManagerEq.Instance.Passed;
		}
		
		void FailTest()
		{
			GameManagerEq.Instance.Failed = !GameManagerEq.Instance.Failed;
		}
		
		void ResetTest()
		{
			GameManagerEq.Instance.Retry = !GameManagerEq.Instance.Retry;
		}

		void Transition()
		{
			GameManagerEq.Instance.ShouldTransitionToDuringEq = !GameManagerEq.Instance.ShouldTransitionToDuringEq;
		}
		
		void Pause()
		{
			GameManagerEq.Instance.Resumed = false;
			GameManagerEq.Instance.Paused = true;
		}

		void Resume()
		{
			GameManagerEq.Instance.Paused = false;
			GameManagerEq.Instance.Resumed = true;
		}
	}
}
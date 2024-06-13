using FSM;
using UnityEngine;

namespace EarthQuake.EarthquakeStateMachine
{
	/// <summary>
	/// This is an empty state for the state machine to default to.
	/// </summary>
	public class IdleEarthQuakeState : State
	{
		public override void OnEnterState(StateMachine newStateMachine)
		{
			base.OnEnterState(newStateMachine);
			Debug.Log("Default State Enter");
		}

		public override void UpdateState()
		{
			base.UpdateState();
			Debug.Log("Default State update");
		}
		
		public override void OnExitState()
		{
			base.OnExitState();
			Debug.Log("Default State Exit");
		}
	}
}
using UnityEngine;

namespace StateMachine
{
	public abstract class State
	{
		public float CurrentTime { get; set; }

		public StateMachine stateMachine;

		public virtual void OnEnterState(StateMachine newStateMachine)
		{
			this.stateMachine = newStateMachine;
		}
		public virtual void UpdateState()
		{
			CurrentTime += Time.deltaTime; 
		}

		public virtual void OnExitState() { }
		
		#region PassThroughMethods

		protected static void Destroy(Object obj)
		{
			Object.Destroy(obj);
		}
		
		protected T GetComponent<T>() where T : Component
		{
			return stateMachine.GetComponent<T>();
		}
		
		protected Component GetComponent(System.Type type)
		{
			return stateMachine.GetComponent(type);
		}
		
		protected Component GetComponent(string type)
		{
			return stateMachine.GetComponent(type);
		}
		
		#endregion
	}
}
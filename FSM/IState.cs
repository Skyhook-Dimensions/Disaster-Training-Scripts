using System;

namespace FSM
{
	public interface IState
	{
		void OnEnter();
		void Update();
		void FixedUpdate();
		void LateUpdate();
		void OnExit();
	}
}
using EarthQuake.EqStateMachine;
using EventSystem;
using FSM;

namespace EarthQuake.Managers
{
	public abstract class Controller : EventListener<EqBaseState>
	{
		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(StartAction);
			_unityEvent.AddListener(ResetAction);
			_unityEvent.AddListener(StopAction);
			_unityEvent.AddListener(PauseAction);
			// _unityEvent.AddListener(ResumeAction);
		}

		protected abstract void StartAction(IState state);
		protected abstract void PauseAction(IState state);
		protected abstract void ResumeAction(IState state);
		protected abstract void StopAction(IState state);
		protected abstract void ResetAction(IState state);
	}
}

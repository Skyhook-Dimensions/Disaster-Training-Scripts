using EventSystem;
using FSM;

namespace EarthQuake.Managers
{
	public abstract class Controller : EventListener<IState>
	{
		protected abstract void StartAction(IState state);
		protected abstract void StopAction(IState state);
		protected abstract void PauseAction(IState state);
		protected abstract void ResumeAction(IState state);
	}
}
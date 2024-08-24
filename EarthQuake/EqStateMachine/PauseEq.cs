using System;
using EarthQuake.Managers;

namespace EarthQuake.EqStateMachine
{
	public class PauseEq : EqBaseState
	{
		public Type PrevState { get; private set; }
		public PauseEq(float duration) : base(duration)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			this.PrevState = GameManagerEq.Instance.PrevState;
		}
		
		public override void OnExit()
		{
			base.OnExit();
			GameManagerEq.Instance.PrevState = typeof(PauseEq);
		}
	}
}
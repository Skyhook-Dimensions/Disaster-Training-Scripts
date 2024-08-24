using EarthQuake.Managers;

namespace EarthQuake.EqStateMachine
{
	public class ResetEq : EqBaseState
	{
		public ResetEq(float duration) : base(duration)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			GameManagerEq.Instance.ResetBools();
		}
	}
}
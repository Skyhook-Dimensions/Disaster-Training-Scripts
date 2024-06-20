namespace FSM
{
	public abstract class BaseState : IState
	{
		public void OnEnter()
		{
			// noop
		}

		public void Update()
		{
			// noop
		}

		public void FixedUpdate()
		{
			//noop
		}

		public void LateUpdate()
		{
			//noop
		}

		public void OnExit()
		{
			//noop
		}
	}
}
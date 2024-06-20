using System;

namespace FSM
{
	public class FuncPredicate: IPredicate
	{
		readonly Func<bool> _predicate;
		
		public FuncPredicate(Func<bool> predicate)
		{
			this._predicate = predicate;
		}
		
		public bool Evaluate() => _predicate.Invoke();
	}
}
using FSM;
using JetBrains.Annotations;

namespace Interfaces
{
	public interface IPausable
	{
		void Pause();
		void Resume();
	}
}
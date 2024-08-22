using UnityEngine;

namespace Interfaces
{
	public interface IFallable : IPausable, IStoppable, IResetable
	{
		void StartFalling(float maxDelay, float minDelay = 0f);
	}
	public interface IShakable : IPausable, IStoppable, IResetable
	{
		void StartShake(float duration, Vector3 strength, float delay = 0f);
	}

	public interface IFlickerer : IPausable, IStoppable, IResetable
	{
		void StartLightFlicker();
	}
}
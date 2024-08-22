using UnityEngine;

namespace Interfaces
{
	public interface IFallable : IPausable, IStoppable
	{
		void StartFalling(float maxDelay, float minDelay = 0f);
	}
	public interface IShakable : IPausable, IStoppable
	{
		void StartShake(float duration, Vector3 strength, float delay = 0f);
	}

	public interface IFlickerer : IPausable, IStoppable
	{
		void StartLightFlicker();
	}
}
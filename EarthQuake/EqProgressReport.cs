namespace EarthQuake
{
	public class EqProgressReport
	{
		public string userName;
		public bool foundHidingSpot;
		public float timeToFindHidingSpot;
		public bool didNotUseLift;
		public bool usedStairs;
		public bool evacuatedHouseInTime;
		public float timeToEvacuateHouse;
		public bool reachedSafeZone;
		public float timeToReachSafeZone;

		public EqProgressReport()
		{
			userName = "Test";
			foundHidingSpot = false;
			didNotUseLift = false;
			usedStairs = false;
			evacuatedHouseInTime = false;
			reachedSafeZone = false;
		}
	}
}
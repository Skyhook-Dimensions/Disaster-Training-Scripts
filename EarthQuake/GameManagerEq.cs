using System;
using FSM;
using Utils;
using UnityEngine;
using EarthQuake.EarthquakeStateMachine;

namespace EarthQuake
{
    [RequireComponent(typeof(StateMachine))]
    public class GameManagerEq : GenericSingleton<GameManagerEq>
    {
    }
}

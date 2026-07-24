using UnityEngine;
using CountDown.Core;
using System;

namespace CountDown.Game
{
    public class TurnManager : Singleton<TurnManager>
    {
        public event Action OnUpdateWorld;
        public event Action OnTurnPassed;

        public void PassTurn()
        {
            OnUpdateWorld?.Invoke();
            OnTurnPassed?.Invoke();
        }
    }
}

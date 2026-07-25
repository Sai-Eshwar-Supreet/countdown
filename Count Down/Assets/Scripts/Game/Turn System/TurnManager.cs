using UnityEngine;
using CountDown.Core;
using System;

namespace CountDown.Game
{
    public class TurnManager : Singleton<TurnManager>
    {
        public event Action OnPreTurn;
        public event Action OnTurn;
        public event Action OnPostTurn;

        public void PassTurn()
        {
            OnPreTurn?.Invoke();
            OnTurn?.Invoke();
            OnPostTurn?.Invoke();
        }
    }
}

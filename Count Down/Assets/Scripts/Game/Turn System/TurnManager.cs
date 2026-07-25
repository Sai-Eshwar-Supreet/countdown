using UnityEngine;
using CountDown.Core;
using System;
using System.Collections;

namespace CountDown.Game
{
    public class TurnManager : Singleton<TurnManager>
    {
        public event Action OnPreTurn;
        public event Action<int> OnTurn;
        public event Action OnPostTurn;

        public void PassTurn(int cost)
        {
            OnPreTurn?.Invoke();
            OnTurn?.Invoke(cost);
            OnPostTurn?.Invoke();
        }
    }
}

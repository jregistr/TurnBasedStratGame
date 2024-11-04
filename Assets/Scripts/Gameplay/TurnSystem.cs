using System;
using UnityEngine;

namespace Gameplay
{
    public class TurnSystem : MonoBehaviour
    {
        public static TurnSystem Instance { get; private set; }
        
        public event EventHandler<int> TurnChangedTo; 
        private int _turnNumber;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            _turnNumber = 1;
        }

        public void NextTurn()
        {
            _turnNumber++;
            TurnChangedTo?.Invoke(this, _turnNumber);
        }
        
        public int GetCurrentTurn() => _turnNumber;
    }
}

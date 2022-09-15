using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Source.Game.Client
{
    public class LocalScoreManager : MonoBehaviour
    {
        public delegate void OnScore(Dictionary<string, int> values);
        public static event OnScore OnScoreChange;
        
        private static LocalScoreManager _manager;

        public static LocalScoreManager singletone
        {
            get { return _manager; }
        }

        private void Awake()
        {
            _manager = this;
        }

        public void OnScoreChanged(Dictionary<string, int> values)
        {
            OnScoreChange?.Invoke(values);
        }
    }
}

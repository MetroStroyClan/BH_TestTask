using System;
using Mirror;
using Source.Game.Client;
using UnityEngine;

namespace Source.Game.Server
{
    public class ServerWinManager : NetworkBehaviour
    {
        [Header("Params")] [SerializeField] private int scoreToWin;

        public delegate void OnServer();
        public static event OnServer OnServerWin;
        
        private static ServerWinManager _manager;

        public static ServerWinManager singletone
        {
            get { return _manager; }
        }

        private void Awake()
        {
            _manager = this;
        }

        [Server]
        private void OnEnable()
        {
            PlayerPool.OnScoreChangedEvent += OnScoreChanged;
        }

        [Server]
        private void OnDisable()
        {
            PlayerPool.OnScoreChangedEvent -= OnScoreChanged;
        }

        [Server]
        private void OnScoreChanged(string[] names, int[] values)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if(IsThisWin(values[i]))
                {
                    OnLocalWin(names[i]);
                    break;
                }
            }
        }

        private bool IsThisWin(int score)
        {
            return score >= scoreToWin;
        }

        private void OnLocalWin(string name)
        {
            SetSignalToClients(name);
            OnServerWin?.Invoke();
        }

        [ClientRpc(includeOwner = true)]
        private void SetSignalToClients(string name)
        {
            LocalWinManager.singletone.SetWinTrue(name);
        }
    }
}

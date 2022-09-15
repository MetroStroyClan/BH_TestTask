using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Source.Game.Client;
using UnityEngine;

namespace Source.Game.Server
{
    public class PlayerPool : NetworkBehaviour
    {
        public delegate void OnScore(string[] names, int[] scores);
        public static event OnScore OnScoreChangedEvent;
        
        private static PlayerPool _pool;

        public static PlayerPool playerPool
        {
            get
            {
                return _pool;
            }
        }
        
        private List<Player> _players;

        private void Awake()
        {
            _pool = this;
            _players = new List<Player>();
        }

        [Command(requiresAuthority = false)]
        public void AddPlayer(string playerName, int connID)
        {
            _players.Add(new Player(playerName, connID));
            SendScoresToClients();
            
            Debug.Log($"Player connected, name: {playerName} connID: {connID}");
        }

        [Server]
        public void AddScore(string playerName)
        {
            foreach (var x in _players.Where(x => x.name == playerName))
                x.score++;
            SendScoresToClients();
        }

        [Server]
        public void ResetPoints()
        {
            foreach (var x in _players)
                x.score = 0;
            
            SendScoresToClients();
        }
        
        [Server]
        private void DeletePlayer(Player player)
        {
            _players.Remove(player);
        }

        [Server]
        private void SendScoresToClients()
        {
            var values = _players.ToDictionary(x => x.name, x => x.score);
            OnScoreChanged(values.Keys.ToArray(), values.Values.ToArray());
            OnScoreChangedEvent?.Invoke(values.Keys.ToArray(), values.Values.ToArray());
        }
        
        [ClientRpc(includeOwner = true)]
        private void OnScoreChanged(string[] names, int[] scores)
        {
            var values = new Dictionary<string, int>();
            for(int i = 0; i < names.Length; i++)
                values.Add(names[i], scores[i]);
            LocalScoreManager.singletone.OnScoreChanged(values);
        }
    }
    
    public class Player
    {
        public Player(string name, int connID)
        {
            this.name = name;
            this.connectionID = connID;
            score = 0;
        }

        public string name { get; private set; }
        public int score;
        public int connectionID { get; private set; }
    }
}

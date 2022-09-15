using System.Collections;
using System.Runtime.CompilerServices;
using Mirror;
using Source.Game.Server;
using UnityEngine;

namespace Source.Game.Client
{
    public class NetManager : NetworkManager
    {
        public delegate void OnClient();
        public static event OnClient OnClientConnected;
        
        private static NetManager _manager;

        public static NetManager singletoneManager
        {
            get
            {
                return _manager;
            }
        }

        public string playerName { get; set; }

        private void Awake()
        {
            _manager = this;
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            StartCoroutine(AddPlayerInPlayerPool(playerName, NetworkClient.connection.connectionId));
        }
        
        private IEnumerator AddPlayerInPlayerPool(string plName, int conId)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerPool.playerPool.AddPlayer(plName, conId);
            OnClientConnected?.Invoke();
        }
    }
}

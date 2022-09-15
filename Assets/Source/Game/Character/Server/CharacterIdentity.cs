using System;
using System.Collections;
using Mirror;
using Source.Game.Client;
using Source.Game.Server;
using UnityEngine;

namespace Source.Game.Character.Server
{
    public class CharacterIdentity : NetworkBehaviour
    {
        private string _playerName;

        public string PlayerName
        {
            get { return _playerName; }
        }

        private void Start()
        {
            if(isLocalPlayer)
            {
                SetName(NetManager.singletoneManager.playerName);
                ServerRebootManager.singletone.AddToResetQueue(gameObject);
            }
        }

        [Command(requiresAuthority = false)]
        private void SetName(string name)
        {
            _playerName = name;
        }
    }
}

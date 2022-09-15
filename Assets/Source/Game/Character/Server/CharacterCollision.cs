using System;
using Mirror;
using Source.Game.Character.Local;
using Source.Game.Server;
using UnityEngine;

namespace Source.Game.Character.Server
{
    [RequireComponent(typeof(CharacterDash),
        typeof(CharacterIdentity))]
    public class CharacterCollision : NetworkBehaviour
    {
        [SerializeField] private string playerTag;

        private CharacterDash _characterDash;
        private CharacterIdentity _characterIdentity;
        
        private void Start()
        {
            _characterDash = GetComponent<CharacterDash>();
            _characterIdentity = GetComponent<CharacterIdentity>();
        }

        [Server]
        private void OnCollisionEnter(Collision collision)
        {
            if (_characterDash.IsDashing && (bool)collision.gameObject?.CompareTag(playerTag))
            {
                if (collision.gameObject.TryGetComponent<CharacterIdentity>(out var identity) 
                    && collision.gameObject.TryGetComponent<CharacterInjure>(out var injure))
                {
                    if(!injure.isInjured)
                    {
                        PlayerCollisionReceiver.singletone.OnCollision(
                            _characterIdentity.PlayerName, identity.PlayerName);
                        injure.OnInjure();
                    }
                }
                else
                {
                    throw new NullReferenceException("Character identity didn't found!");
                }
            }
        }
    }
}

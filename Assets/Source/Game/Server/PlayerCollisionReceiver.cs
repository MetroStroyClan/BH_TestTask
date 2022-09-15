using System;
using Mirror;
using UnityEngine;

namespace Source.Game.Server
{
    public class PlayerCollisionReceiver : MonoBehaviour
    {
        private static PlayerCollisionReceiver _receiver;

        public static PlayerCollisionReceiver singletone
        {
            get { return _receiver; }
        }

        private void Awake()
        {
            _receiver = this;
        }

        public void OnCollision(string nameA, string nameB)
        {
            Debug.Log($"Collision! {nameA} with {nameB}");
            PlayerPool.playerPool.AddScore(nameA);
        }
    }
}

using System;
using System.Collections;
using Mirror;
using UnityEngine;
using System.Collections.Generic;

namespace Source.Game.Server
{
    public class ServerRebootManager : NetworkBehaviour
    {
        [SerializeField] private float waitToReboot;
        [SerializeField] private List<NetworkStartPosition> startPositions;
        [SerializeField] private List<GameObject> objectToOffOnReboot;

        private static ServerRebootManager _manager;

        public static ServerRebootManager singletone
        {
            get { return _manager; }
        }
        
        private List<GameObject> _playersGameObjects;
        private PlayerPool _pool => PlayerPool.playerPool;

        private void Awake()
        {
            _playersGameObjects = new List<GameObject>();
            _manager = this;
        }

        [Server]
        private void OnEnable()
        {
            ServerWinManager.OnServerWin += OnWin;
        }

        [Server]
        private void OnDisable()
        {
            ServerWinManager.OnServerWin -= OnWin;
        }

        [Command(requiresAuthority = false)]
        public void AddToResetQueue(GameObject player)
        {
            _playersGameObjects.Add(player);
        }
        
        [Server]
        private void OnWin()
        {
            StartCoroutine(WaitReboot());
        }

        private IEnumerator WaitReboot()
        {
            yield return new WaitForSeconds(waitToReboot);
            SetupPlayersOnPositions();
            ResetPoints();
            OffObjectOnReboot();
        }
        
        private void SetupPlayersOnPositions()
        {
            foreach (var x in _playersGameObjects)
            {
                var randValue = UnityEngine.Random.Range(0, startPositions.Count);
                SetPlayerPosition(x, startPositions[randValue].transform.position);
            }
        }

        [Server]
        private void SetPlayerPosition(GameObject player, Vector3 position)
        {
            player.transform.position = position;
            SetPlayerClientPosition(player, position);
        }

        [ClientRpc(includeOwner = true)]
        private void SetPlayerClientPosition(GameObject player, Vector3 position)
        {
            player.transform.position = position;
        }

        [ClientRpc(includeOwner = true)]
        private void OffObjectOnReboot()
        {
            foreach (var x in objectToOffOnReboot)
                x.SetActive(false);
        }
        
        private void ResetPoints()
        {
            _pool.ResetPoints();
        }
    }
}

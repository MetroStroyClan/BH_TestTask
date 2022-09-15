using System;
using Mirror;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Server
{
    [RequireComponent(typeof(WallChecker))]
    public class CharacterServerMove : NetworkBehaviour
    {
        [SerializeField] private float syncPower;
        
        private InputManager _manager => InputManager.inputManager;

        private WallChecker _checker;

        private void Start()
        {
            _checker = GetComponent<WallChecker>();
        }

        private void Update()
        {
            if(isLocalPlayer && isClient)
                MoveOnServer(_manager.deltaMoveAxis, Time.deltaTime, _manager.GetMoveSensitivity);
        }

        [Command(requiresAuthority = false)]
        private void MoveOnServer(Vector2 axis, float deltaTime, float sensitivity)
        {
            if (!_checker.IsWallOnLocalDirection(new Vector3(axis.x, 0, axis.y)))
            {
                transform.Translate((
                        new Vector3(axis.normalized.x, 0, axis.normalized.y) 
                        * deltaTime * sensitivity),
                    Space.Self);
            }
            SetPosition(transform.position, deltaTime);
        }

        [ClientRpc(includeOwner = true)]
        private void SetPosition(Vector3 position, float deltaTime)
        {
            transform.Translate((position - transform.position) 
                                * syncPower * deltaTime, Space.World);
        }
    }
}

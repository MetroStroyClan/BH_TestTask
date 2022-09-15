using System;
using Mirror;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Local
{
    [RequireComponent(typeof(WallChecker))]
    public class CharacterLocalMove : NetworkBehaviour
    {
        private InputManager _manager => InputManager.inputManager;

        private WallChecker _checker;

        private void Start()
        {
            _checker = GetComponent<WallChecker>();
        }

        private void Update()
        {
            if(!isServer && isLocalPlayer && isClient)
                MoveLocal(_manager.deltaMoveAxis, Time.deltaTime, _manager.GetMoveSensitivity);
        }

        private void MoveLocal(Vector2 axis, float deltaTime, float sensitivity)
        {
            if (!_checker.IsWallOnLocalDirection(new Vector3(axis.x, 0, axis.y)))
            {
                transform.Translate((new Vector3(axis.normalized.x, 0, axis.normalized.y) 
                                     * deltaTime * sensitivity), Space.Self);
            }
        }
    }
}

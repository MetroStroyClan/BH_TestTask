using System;
using Mirror;
using UnityEngine;
using Source.Game.Client.Input;

namespace Source.Game.Character.Server
{
    public class CharacterServerRotation : NetworkBehaviour
    {
        private InputManager _manager => InputManager.inputManager;

        private void Update()
        {
            if(isLocalPlayer && isClient)
                RotateFromLocal(_manager.deltaMouseAxis, Time.deltaTime, _manager.GetMouseSensitivity);
        }

        [Command(requiresAuthority = false)]
        private void RotateFromLocal(Vector2 deltaAxisValue, float deltaTime, float sensitivity)
        {
            float angle = Mathf.Atan2(deltaAxisValue.x, deltaAxisValue.y) 
                          * Mathf.Rad2Deg * deltaAxisValue.magnitude;
            transform.Rotate(Vector3.up, angle * deltaTime * sensitivity);
            SetRotation(transform.rotation.eulerAngles);
        }
        
        [ClientRpc(includeOwner = false)]
        private void SetRotation(Vector3 angles)
        {
            transform.rotation = Quaternion.Euler(angles);
        }
    }
}

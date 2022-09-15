using System;
using Mirror;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Local
{
    public class CharacterLocalRotation : NetworkBehaviour
    {
        private InputManager _manager => InputManager.inputManager;

        private void Update()
        {
            if(!isServer && isLocalPlayer && isClient)
                SetRotation(_manager.deltaMouseAxis, Time.deltaTime, _manager.GetMouseSensitivity);
        }

        private void SetRotation(Vector2 deltaAxisValue, float deltaTime, float sensitivity)
        {
            float angle = Mathf.Atan2(deltaAxisValue.x, deltaAxisValue.y) 
                          * Mathf.Rad2Deg * deltaAxisValue.magnitude;
            transform.Rotate(Vector3.up, angle * deltaTime * sensitivity);
        }
    }
}

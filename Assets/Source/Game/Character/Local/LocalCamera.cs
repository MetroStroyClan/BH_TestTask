using System;
using Mirror;
using UnityEngine;

namespace Source.Game.Character.Local
{
    public class LocalCamera : NetworkBehaviour
    {
        [SerializeField] private Camera camera;

        private void Start()
        {
            CheckCamera();
        }

        private void CheckCamera()
        {
            SetCameraState((isClient && isLocalPlayer));
        }

        private void SetCameraState(bool onoff)
        {
            camera.gameObject.SetActive(onoff);
        }
    }
}

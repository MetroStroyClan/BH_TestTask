using System;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Local
{
    public class CameraVerticalRotation : MonoBehaviour
    {
        [SerializeField] private Transform lookAtTransform;
        [SerializeField] private float distanceToObject;
        [SerializeField] private float cameraSensitivity;

        private float _actualAngle = 180;

        private void Update()
        {
            SetRotation(InputManager.inputManager.deltaMouseAxis, Time.deltaTime);
            SetLookAt();
        }

        private void SetRotation(Vector2 mouseInput, float deltaTime)
        {
            _actualAngle += mouseInput.y * deltaTime * cameraSensitivity;

            if (_actualAngle < 95)
                _actualAngle = 95;
            else if (_actualAngle > 265)
                _actualAngle = 265;
            
            var localPosition = new Vector3(
                0, Mathf.Sin(_actualAngle * Mathf.Deg2Rad) * distanceToObject,
                Mathf.Cos(_actualAngle * Mathf.Deg2Rad) * distanceToObject
            );

            transform.localPosition = localPosition;
        }

        private void SetLookAt()
        {
            transform.LookAt(lookAtTransform);
        }
    }
}


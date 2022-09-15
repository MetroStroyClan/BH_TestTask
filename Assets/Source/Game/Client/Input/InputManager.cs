using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Source.Game.Client.Input
{
    public class InputManager : MonoBehaviour
    {
        [Header("Params")] 
        [SerializeField] private float mouseAxisSensitivity;
        [SerializeField] private float moveAxisSensitivity;
        [SerializeField] private float dashDistance;
        [SerializeField] private KeyCode lockCursorButton;
        [SerializeField] private KeyCode unlockCursorButton;
        [SerializeField] private MouseButton fireButton;

        public delegate void OnInputMouse();
        public static event OnInputMouse OnFire;
        
        public float GetMouseSensitivity
        {
            get { return mouseAxisSensitivity; }
        }
        
        public float GetMoveSensitivity
        {
            get { return moveAxisSensitivity; }
        }

        public float GetDashDistance
        {
            get { return dashDistance; }
        }
        
        private static InputManager _manager;

        public static InputManager inputManager
        {
            get
            {
                return _manager;
            }
        }

        public Vector2 deltaMouseAxis { get; private set; }
        public Vector2 deltaMoveAxis { get; private set; }

        private void Awake()
        {
            _manager = this;
            UnlockCursor();
        }

        private void Update()
        {
            SetInput();
            CheckCursorLockStatusKeys();
            CheckMouseButtons();
        }

        private void OnEnable()
        {
            NetManager.OnClientConnected += OnClientConnected;
        }

        private void OnDisable()
        {
            NetManager.OnClientConnected -= OnClientConnected;
        }

        private void SetInput()
        {
            SetMouseAxis(new Vector2(
                UnityEngine.Input.GetAxis("Mouse X"),
                UnityEngine.Input.GetAxis("Mouse Y")));
            
            SetMoveAxis(new Vector2(
                UnityEngine.Input.GetAxis("Horizontal"),
                UnityEngine.Input.GetAxis("Vertical")));
        }
        
        private void SetMouseAxis(Vector2 value)
        {
            deltaMouseAxis = value;
        }

        private void SetMoveAxis(Vector2 value)
        {
            deltaMoveAxis = value;
        }

        private void OnClientConnected()
        {
            LockCursor();
        }

        private void CheckCursorLockStatusKeys()
        {
            if(UnityEngine.Input.GetKey(lockCursorButton))
                LockCursor();
            
            if(UnityEngine.Input.GetKey(unlockCursorButton))
                UnlockCursor();
        }
        
        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        private void CheckMouseButtons()
        {
            if(UnityEngine.Input.GetMouseButtonDown((int)fireButton))
                OnFireAction();
        }

        private void OnFireAction()
        {
            OnFire?.Invoke();
        }
    }
}

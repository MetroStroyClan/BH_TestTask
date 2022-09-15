using System;
using System.Collections;
using Mirror;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Local
{
    [RequireComponent(typeof(WallChecker))]
    public class CharacterDash : NetworkBehaviour
    {
        [SerializeField] private float dashUpdateDeltaTime;
        [SerializeField] private float dashSpeed;
        [SerializeField] private string wallTag;
        [SerializeField] private float minDistanceToWalls;
        [SerializeField] private float dashCooldown;
        
        [SyncVar] private bool _isDashing = false;
        [SyncVar] private bool _isCanDash = true;

        private WallChecker _checker;

        private void Start()
        {
            _checker = GetComponent<WallChecker>();
        }

        public bool IsDashing
        {
            get { return _isDashing; }
        }
        
        private void OnEnable()
        {
            InputManager.OnFire += OnFire;
        }

        private void OnDisable()
        {
            InputManager.OnFire -= OnFire;
        }

        private void OnFire()
        {
            if (_isCanDash && isClient && isLocalPlayer)
            {
                if (!isServer)
                    StartCoroutine(DashRoutine());
                CmdOnFire();
            }
        }

        [Command(requiresAuthority = false)]
        private void CmdOnFire()
        {
            if(_isCanDash)
                StartCoroutine(DashRoutine());
        }
        
        private IEnumerator DashRoutine()
        {
            _isCanDash = false;
            _isDashing = true;
            
            var wait = new WaitForSeconds(dashUpdateDeltaTime);
            var dashDistance = InputManager.inputManager.GetDashDistance;
            float oneStepDistance = dashUpdateDeltaTime * dashSpeed;

            for (float actualDistance = 0; actualDistance < dashDistance; actualDistance += oneStepDistance)
            {
                if (!_checker.IsWallOnFront)
                    transform.Translate(transform.forward * oneStepDistance, Space.World);

                yield return wait;
            }
            _isDashing = false;
            
            yield return new WaitForSeconds(dashCooldown);
            _isCanDash = true;
        }
    }
}

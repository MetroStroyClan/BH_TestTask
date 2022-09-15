using System;
using Mirror;
using Source.Game.Client.Input;
using UnityEngine;

namespace Source.Game.Character.Local
{
    [RequireComponent(typeof(Animator),
        typeof(CharacterDash))]
    public class CharacterAnimator : NetworkBehaviour
    {
        [Header("Animator params")]
        [SerializeField] private string runningAnimatorBooleanName;
        [SerializeField] private string dashingAnimatorBooleanName;
        
        private Animator _animator;
        private CharacterDash _characterDash;

        private InputManager _manager => InputManager.inputManager;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _characterDash = GetComponent<CharacterDash>();
        }

        private void Update()
        {
            CheckAnimator();
        }

        private void CheckAnimator()
        {
            if (isLocalPlayer && isClient)
            {
                CheckOnDash();
                CheckOnRun();
            }
        }

        private void CheckOnDash()
        {
            AnimatorSetBool(dashingAnimatorBooleanName, _characterDash.IsDashing);
        }

        private void CheckOnRun()
        {
            AnimatorSetBool(runningAnimatorBooleanName,
                _manager.deltaMoveAxis.x != 0 || _manager.deltaMoveAxis.y != 0);
        }

        private void AnimatorSetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
            AnimatorSetBoolCmd(name, value);
        }

        [Command(requiresAuthority = false)]
        private void AnimatorSetBoolCmd(string name, bool value)
        {
            AnimatorSetBoolOnLocal(name, value);
        }

        [ClientRpc(includeOwner = false)]
        private void AnimatorSetBoolOnLocal(string name, bool value)
        {
            if (_animator)
                _animator.SetBool(name, value);
            else
                _animator = GetComponent<Animator>();
        }
    }
}

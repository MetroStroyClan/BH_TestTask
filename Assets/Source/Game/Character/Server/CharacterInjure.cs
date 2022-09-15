using System;
using System.Collections;
using Mirror;
using UnityEngine;

namespace Source.Game.Character.Server
{
    public class CharacterInjure : NetworkBehaviour
    {
        [Header("Params")] 
        [SerializeField] private float injureTime;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color injureColor;
        
        [Header("Refs")]
        [SerializeField] private Material sourceMaterial;
        [SerializeField] private SkinnedMeshRenderer renderer;

        public bool isInjured { get; private set; }

        private Material _characterMaterial;

        private void Start()
        {
            _characterMaterial = new Material(sourceMaterial);
            renderer.material = _characterMaterial;
            SetColor(defaultColor);
        }

        [Server]
        public void OnInjure()
        {
            StartCoroutine(InjureRoutine());
        }

        private IEnumerator InjureRoutine()
        {
            isInjured = true;
            SetColorCmd(injureColor);
            yield return new WaitForSeconds(injureTime);
            SetColorCmd(defaultColor);
            isInjured = false;
        }

        private void SetColor(Color color)
        {
            _characterMaterial.color = color;
        }
        
        private void SetColorCmd(Color color)
        {
            SetColorOnLocal(color);
        }

        [ClientRpc(includeOwner = true)]
        private void SetColorOnLocal(Color color)
        {
            SetColor(color);
        }
    }
}

using System;
using System.Collections.Generic;
using Source.Game.Client;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu.UI
{
    public class MenuUiManager : MonoBehaviour
    {
        [Header("Refs")] 
        [SerializeField] public List<Button> menuButtons;
        [SerializeField] public GameObject menuParentObject;

        private bool _isAddressFieldFilled = false;
        private bool _isNameFieldFilled = false;

        private NetManager _manager => NetManager.singletoneManager;

        private void Start()
        {
            CheckButtonsState();
        }

        private void OnEnable()
        {
            NetManager.OnClientConnected += OnClientConnected;
        }

        private void OnDisable()
        {
            NetManager.OnClientConnected -= OnClientConnected;
        }

        public void OnAddressChanged(InputField field)
        {
            if (!string.IsNullOrWhiteSpace(field.text))
                _isAddressFieldFilled = true;
            
            CheckButtonsState();
            
            _manager.networkAddress = field.text;
        }

        public void OnNameChanged(InputField field)
        {
            if (!string.IsNullOrWhiteSpace(field.text))
                _isNameFieldFilled = true;
            
            CheckButtonsState();
            
            _manager.playerName = field.text;
        }

        private void CheckButtonsState()
        {
            if (_isAddressFieldFilled && _isNameFieldFilled)
            {
                foreach (var x in menuButtons)
                    x.interactable = true;
            }
            else
            {
                foreach (var x in menuButtons)
                    x.interactable = false;
            }
        }

        private void OnClientConnected()
        {
            OffConnectionMenu();
        }
        
        private void OffConnectionMenu()
        {
            menuParentObject.SetActive(false);
        }
    }
}

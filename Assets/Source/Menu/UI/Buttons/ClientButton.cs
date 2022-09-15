using System;
using Source.Game.Client;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ClientButton : MonoBehaviour
    {
        private NetManager _manager => NetManager.singletoneManager;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            SetupClientButton();
        }

        private void SetupClientButton()
        {
            _button.onClick.AddListener(OnClientButtonClicked);
        }
        
        private void OnClientButtonClicked()
        {
            _manager.StartClient();
        }
    }
}

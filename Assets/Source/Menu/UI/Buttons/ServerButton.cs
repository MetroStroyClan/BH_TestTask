using UnityEngine;
using UnityEngine.UI;
using Source.Game.Client;

namespace Source.Menu.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ServerButton : MonoBehaviour
    {
        private NetManager _manager => NetManager.singletoneManager;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            SetupServerButton();
        }

        private void SetupServerButton()
        {
            _button.onClick.AddListener(OnServerButtonClicked);
        }
        
        private void OnServerButtonClicked()
        {
            _manager.StartServer();
        }
    }
}

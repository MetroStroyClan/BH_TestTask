using Source.Game.Client;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class HostButton : MonoBehaviour
    {
        private NetManager _manager => NetManager.singletoneManager;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            SetupHostButton();
        }

        private void SetupHostButton()
        {
            _button.onClick.AddListener(OnHostButtonClicked);
        }
        
        private void OnHostButtonClicked()
        {
            _manager.StartHost();
        }
    }
}

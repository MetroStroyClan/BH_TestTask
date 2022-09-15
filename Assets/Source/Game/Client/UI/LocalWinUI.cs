using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Client.UI
{
    public class LocalWinUI : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private Text winnerText;

        private void OnEnable()
        {
            LocalWinManager.OnSomeWin += OnLocalWin;
        }

        private void OnDisable()
        {
            LocalWinManager.OnSomeWin += OnLocalWin;
        }

        private void OnLocalWin(string winnerName)
        {
            winPanel.SetActive(true);
            winnerText.text = $"{winnerName} is winner!";
        }
    }
}

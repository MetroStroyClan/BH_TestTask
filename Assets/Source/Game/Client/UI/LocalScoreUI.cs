using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Client.UI
{
    public class LocalScoreUI : MonoBehaviour
    {
        [SerializeField] private Text textField;

        private void OnEnable()
        {
            LocalScoreManager.OnScoreChange += OnScoreChanged;
        }

        private void OnDisable()
        {
            LocalScoreManager.OnScoreChange -= OnScoreChanged;
        }

        private void OnScoreChanged(Dictionary<string, int> score)
        {
            var result = score.Aggregate("", (current, x) 
                => current + $"{x.Key}  {x.Value}\n");
            textField.text = result;
        }
    }
}

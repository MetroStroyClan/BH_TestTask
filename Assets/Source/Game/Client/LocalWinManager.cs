using UnityEngine;

namespace Source.Game.Client
{
    public class LocalWinManager : MonoBehaviour
    {
        public delegate void OnWin(string winner);
        public static event OnWin OnSomeWin;
        
        private static LocalWinManager _manager;

        public static LocalWinManager singletone
        {
            get { return _manager; }
        }

        private void Awake()
        {
            _manager = this;
        }

        public void SetWinTrue(string winner)
        {
            OnSomeWin?.Invoke(winner);
        }
    }
}

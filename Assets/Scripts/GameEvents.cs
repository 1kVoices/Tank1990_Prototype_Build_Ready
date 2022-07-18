using UnityEngine;
using System;

namespace Tanks
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents Singleton;

        public event Action onEndGame;

        public event Action<BotComponent> onKillBot;

        private bool _isHardBots;
        public bool GetBotsDifficulty => _isHardBots;

        private int _playerHealth;
        public int GetPlayerHp => _playerHealth;

        private int _botsHealth;
        public int GetBotsHp => _botsHealth;

        private int _botsAmount;
        public int GetBotsAmount => _botsAmount;

        private bool _isFirstStartUp = true;
        public bool GetIsFirstStartUp => _isFirstStartUp;

        void Awake()
        {
            if (!Singleton)
            {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
            }

            else Destroy(gameObject);

            _playerHealth = 1;
            _botsHealth = 1;
            _botsAmount = 1;
            _isHardBots = true;
        }

        public void EndGame() => onEndGame?.Invoke();
        
        public void KillBot(BotComponent bot) => onKillBot?.Invoke(bot);

        public void SetPlayerHealth(int i) => _playerHealth = i;

        public void SetBotDifficulty(bool b) => _isHardBots = b;

        public void SetBotsHealth(int i) => _botsHealth = i;

        public void SetBotsAmount(int i) => _botsAmount = i;

        public void SetFirstStartUp(bool b) => _isFirstStartUp = b;
    }
}

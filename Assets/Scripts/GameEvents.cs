using UnityEngine;
using System;

namespace Tanks
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents Singleton;

        public event Action onEndGame;

        public event Action<BotComponent> onKillBot;

        public bool GetBotsDifficulty { get; private set; }
        public int GetPlayerHp { get; private set; }
        public int GetBotsHp { get; private set; }
        public int GetBotsAmount { get; private set; }
        public bool GetIsFirstStartUp { get; private set; } = true;

        private void Awake()
        {
            if (!Singleton)
            {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
            }

            else Destroy(gameObject);

            GetPlayerHp = 1;
            GetBotsHp = 1;
            GetBotsAmount = 1;
            GetBotsDifficulty = true;
        }

        public void EndGame() => onEndGame?.Invoke();
        
        public void KillBot(BotComponent bot) => onKillBot?.Invoke(bot);

        public void SetPlayerHealth(int i) => GetPlayerHp = i;

        public void SetBotDifficulty(bool b) => GetBotsDifficulty = b;

        public void SetBotsHealth(int i) => GetBotsHp = i;

        public void SetBotsAmount(int i) => GetBotsAmount = i;

        public void SetFirstStartUp(bool b) => GetIsFirstStartUp = b;
    }
}

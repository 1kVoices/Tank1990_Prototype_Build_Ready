using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BotComponent _botPrefab;

        [SerializeField]
        private Transform[] _startPostitions;

        [SerializeField]
        private int _botsSpawnDelay = 1;

        private int _botsCount;

        [SerializeField]
        private bool _isHardBots;

        private List<BotComponent> _bots = new List<BotComponent>(); 

        private bool _isSpawning = false;
        private bool _isEndGame = false;

        [SerializeField]
        private GameObject _gameOverScreen;

        [SerializeField]
        private Text _playerHp;

        [SerializeField]
        private Text _playerHpShadow;

        [SerializeField]
        private PlayerConditionComponent _playerComp;

        void Start()
        {
            Invoke(nameof(BotSpawner), 1f);
            GameEvents.Singleton.onKillBot += KillBot;
            _isHardBots = GameEvents.Singleton.GetBotsDifficulty;
            _botsCount = GameEvents.Singleton.GetBotsAmount;

            _gameOverScreen.SetActive(false);

            GameEvents.Singleton.SetFirstStartUp(false);
            GameEvents.Singleton.onEndGame += EndGame;
        }

        void Update()
        {
            if (_bots.Count < 1)
                SpawnBots();

            _playerHp.text = _playerComp.GetHealth.ToString();
            _playerHpShadow.text = _playerComp.GetHealth.ToString();
        }

        void EndGame()
        {
            if (this != null)
            {
                SoundManager.Singleton.GameOver();
                if (!_isEndGame)
                {
                    _isEndGame = true;
                    StartCoroutine(EndGameCor());
                }
            }
        }

        void KillBot(BotComponent bot)
        {
            var listToKill = new List<BotComponent>();

            listToKill.Add(bot);

            foreach (var botToKill in listToKill)
            {
                _bots.Remove(botToKill);
            }
        }

        void SpawnBots()
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                StartCoroutine(BotSpawner());
            }
        }

        IEnumerator EndGameCor()
        {
            if (this != null)
            {
                _gameOverScreen.SetActive(true);
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(0);
            }
        }

        IEnumerator BotSpawner()
        {
            while (_bots.Count != _botsCount)
            {
                var bot = Instantiate(_botPrefab, _startPostitions[UnityEngine.Random.Range(0, _startPostitions.Length)]);
                bot.SetBotDifficulty(_isHardBots);
                _bots.Add(bot);

                yield return new WaitForSeconds(_botsSpawnDelay);
            }
            _isSpawning = false;
        }
    }
}

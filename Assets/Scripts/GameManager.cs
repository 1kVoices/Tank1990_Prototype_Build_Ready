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
        private Transform[] _startPositions;

        [SerializeField]
        private int _botsSpawnDelay = 1;

        private int _botsCount;

        [SerializeField]
        private bool _isHardBots;

        private readonly List<BotComponent> _bots = new List<BotComponent>();

        private bool _isSpawning;
        private bool _isEndGame;

        [SerializeField]
        private GameObject _gameOverScreen;

        [SerializeField]
        private Text _playerHp;

        [SerializeField]
        private Text _playerHpShadow;

        [SerializeField]
        private PlayerConditionComponent _playerComp;

        private void Start()
        {
            Invoke(nameof(BotSpawner), 1f);
            GameEvents.Singleton.onKillBot += KillBot;
            _isHardBots = GameEvents.Singleton.GetBotsDifficulty;
            _botsCount = GameEvents.Singleton.GetBotsAmount;

            _gameOverScreen.SetActive(false);

            GameEvents.Singleton.SetFirstStartUp(false);
            GameEvents.Singleton.onEndGame += EndGame;
        }

        private void Update()
        {
            if (_bots.Count < 1)
                SpawnBots();

            _playerHp.text = _playerComp.GetHealth.ToString();
            _playerHpShadow.text = _playerComp.GetHealth.ToString();
        }

        private void EndGame()
        {
            if (this == null) return;
            SoundManager.Singleton.GameOver();
            if (_isEndGame) return;
            _isEndGame = true;
            StartCoroutine(EndGameCor());
        }

        private void KillBot(BotComponent bot)
        {
            List<BotComponent> listToKill = new List<BotComponent>();

            listToKill.Add(bot);

            foreach (var botToKill in listToKill)
            {
                _bots.Remove(botToKill);
            }
        }

        private void SpawnBots()
        {
            if (_isSpawning) return;
            _isSpawning = true;
            StartCoroutine(BotSpawner());
        }

        private IEnumerator EndGameCor()
        {
            if (this == null) yield break;
            _gameOverScreen.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }

        private IEnumerator BotSpawner()
        {
            while (_bots.Count != _botsCount)
            {
                var bot = Instantiate(_botPrefab, _startPositions[Random.Range(0, _startPositions.Length)]);
                bot.SetBotDifficulty(_isHardBots);
                _bots.Add(bot);

                yield return new WaitForSeconds(_botsSpawnDelay);
            }
            _isSpawning = false;
        }
    }
}

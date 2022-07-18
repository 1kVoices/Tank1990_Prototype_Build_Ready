using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class MenuManager : MonoBehaviour
    {
        private AsyncOperation scene1;

        [SerializeField]
        private Text _playerHpIndicator;

        [SerializeField]
        private Slider _playerHpSlider;

        [SerializeField]
        private Text _botsHpIndicator;

        [SerializeField]
        private Slider _botsHpSlider;

        [SerializeField]
        private Text _botsAmountIndicator;

        [SerializeField]
        private Slider _botsAmountSlider;

        [SerializeField]
        private GameObject _settingsMenu;

        [SerializeField]
        private GameObject _mainMenu;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _settingsButton;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Text _volumeIndicator;

        IEnumerator Start()
        {
            scene1 = SceneManager.LoadSceneAsync(1);
            scene1.allowSceneActivation = false;
            _settingsMenu.SetActive(false);

            yield return new WaitForSeconds(GameEvents.Singleton.GetIsFirstStartUp ? 4 : 0);
            _startButton.interactable = true;
            _settingsButton.interactable = true;
            _exitButton.interactable = true;
        }

        void Update()
        {
            _playerHpIndicator.text = _playerHpSlider.value.ToString();
            _botsHpIndicator.text = _botsHpSlider.value.ToString();
            _botsAmountIndicator.text = _botsAmountSlider.value.ToString();
            _volumeIndicator.text = Mathf.Lerp(0, 100, _volumeSlider.value).ToString("F0");
        }

        public void Start_EditorEvent()
        {
            scene1.allowSceneActivation = true;
        }

        public void Settings_EditorEvent()
        {
            _mainMenu.SetActive(false);
            _settingsMenu.SetActive(true);
        }

        public void ExitSettings_EditorEvent()
        {
            _mainMenu.SetActive(true);
            _settingsMenu.SetActive(false);
        }

        public void ChangePlayerHealth_EditorEvent(float s) => GameEvents.Singleton.SetPlayerHealth((int)s);
        
        public void ChangeBotsHealth_EditorEvent(float s) => GameEvents.Singleton.SetBotsHealth((int)s);

        public void ChangeBotsAmount_EditorEvent(float s) => GameEvents.Singleton.SetBotsAmount((int)s) ;

        public void ChangeBotsDifficulty_EditorEvent(bool b) => GameEvents.Singleton.SetBotDifficulty(b);

        public void ChangeVolume_EditorEvent(float s) => SoundManager.Singleton.ChangeVolume(s);

        public void Exit_EditorEvent()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN
            Application.Quit();
#endif
        }
    }
}

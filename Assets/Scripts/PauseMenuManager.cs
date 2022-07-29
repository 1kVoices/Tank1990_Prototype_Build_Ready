using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField]
        private Text _godModeText;

        [SerializeField]
        private PlayerConditionComponent _playerComp;

        [SerializeField]
        private GameObject _hpBar;

        [SerializeField]
        private GameObject _godModeHpBar;

        public void Continue_EditorEvent()
        {
            gameObject.SetActive(false);
            SoundManager.Singleton.GamePause();
        }

        public void GodMode_EditorEvent(bool b)
        {
            _hpBar.SetActive(!b);
            _godModeHpBar.SetActive(b);

            _playerComp.GodMode(b);

            if (b) _godModeText.color = Color.red;
            else _godModeText.color = Color.white;
        }

        public void ExitToMenu_EditorEvent()
        {
            SceneManager.LoadScene(0);
        }

        private void OnEnable() => Time.timeScale = 0f;
        private void OnDisable() => Time.timeScale = 1f;
    }
}

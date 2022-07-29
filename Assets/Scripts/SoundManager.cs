using UnityEngine;
using UnityEngine.Audio;

namespace Tanks
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Singleton;

        [SerializeField]
        private AudioMixerGroup _mixer;

        [SerializeField]
        private AudioSource _mainSource;

        [SerializeField]
        private AudioSource _playerMove;

        [SerializeField]
        private AudioSource _playerStuck;

        [SerializeField]
        private AudioClip _playerShoot; 

        [SerializeField]
        private AudioClip _botHit; 

        [SerializeField]
        private AudioClip _brickHit; 

        [SerializeField]
        private AudioClip _playerHit; 

        [SerializeField]
        private AudioClip _gameOver;

        [SerializeField]
        private AudioClip _pause; 

        [SerializeField]
        private AudioClip _godMode;

        private bool _isPlaying;

        private bool _isPlaying2;

        private void Awake()
        {
            if (!Singleton)
            {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public void ChangeVolume(float s) =>_mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 20, s));
        
        public void PlayerShoot() => _mainSource.PlayOneShot(_playerShoot);

        public void HitBot() => _mainSource.PlayOneShot(_botHit);

        public void HitBrick() => _mainSource.PlayOneShot(_brickHit);

        public void PlayerHit() => _mainSource.PlayOneShot(_playerHit);

        public void PlayerMove()
        {
            if (_isPlaying) return;
            _isPlaying = true;
            _playerMove.Play();
            Invoke(nameof(ResetIsPlaying), _playerMove.clip.length);
        }

        private void ResetIsPlaying() => _isPlaying = false;
        
        public void PlayerStuck()
        {
            if (_isPlaying2) return;
            _isPlaying2 = true;
            _playerStuck.Play();
            Invoke(nameof(ResetIsPlaying2), _playerStuck.clip.length);
        }

        private void ResetIsPlaying2() => _isPlaying2 = false;
        
        public void GameOver() => _mainSource.PlayOneShot(_gameOver);

        public void GamePause() => _mainSource.PlayOneShot(_pause);

        public void GodMode() => _mainSource.PlayOneShot(_godMode);
    }
}

using System.Collections;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerConditionComponent : ConditionComponent
    {
        private Vector3 _startPoint;

        private SpriteRenderer _renderer;

        [SerializeField]
        private float _invulnerableDelay = 3f;

        [SerializeField]
        private float _activityRendererDelay = 0.2f;

        private bool _isStuck;
        public bool GetIsPlayerStuck => _isStuck;

        private Coroutine _cor;

        private bool _isGettingHit;
        public bool IsGettingHit => _isGettingHit;

        private void Start()
        {
            _startPoint = transform.position;
            _renderer = GetComponent<SpriteRenderer>();
            _health = GameEvents.Singleton.GetPlayerHp;
        }

        public void SetIsPlayerStuck(bool b) => _isStuck = b;

        public void GodMode(bool isGod)
        {
            if (isGod)
            {
                StopAllCoroutines();
                if (_cor != null) StopCoroutine(_cor);
                SoundManager.Singleton.GodMode();
                Invulnerable = true;
            }
            else _cor = StartCoroutine(Vulnerability());
        }

        public override void SetDamage(int damage)
        {
            if (Invulnerable) return;

            _isGettingHit = true;
            Invulnerable = true;
            transform.position = _startPoint;

            base.SetDamage(damage);

            SoundManager.Singleton.PlayerHit();
            if (_health <= 0)
            {
                GameEvents.Singleton.EndGame();
            }

            StartCoroutine(Vulnerability());
        }

        private IEnumerator Vulnerability()
        {
            var currentDelay = _invulnerableDelay;
            var activityDelay = _activityRendererDelay;

            while(currentDelay >= 0f)
            {
                currentDelay -= Time.deltaTime;
                activityDelay -= Time.deltaTime;

                if(activityDelay <= 0f)
                {
                    _renderer.enabled = !_renderer.enabled;
                    activityDelay = _activityRendererDelay;
                }

                yield return null;
            }

            _renderer.enabled = true;
            Invulnerable = false;
            _isGettingHit = false;
        }
    }
}

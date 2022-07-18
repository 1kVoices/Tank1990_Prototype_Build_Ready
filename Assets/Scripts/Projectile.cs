using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent))]
    public class Projectile : MonoBehaviour
    {
        private SideType _side;

        private DirectionType _direction;

        private MoveComponent _moveComp;

        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private float _lifeTime = 5f;

        void Start()
        {
            _moveComp = GetComponent<MoveComponent>();
            Destroy(gameObject, _lifeTime);
        }

        public void SetParams(DirectionType direction, SideType side) => (_direction, _side) = (direction, side);

        void Update()
        {
            _moveComp.OnMove(_direction, false);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            var fire = collision.GetComponent<FireComponent>();

            if (fire)
            {
                if (fire.GetSideType != _side)
                {
                    var bot = collision.GetComponent<BotComponent>();
                    if (bot)
                    {
                        GameEvents.Singleton.KillBot(bot);
                        SoundManager.Singleton.HitBot();
                    }

                    var condition = fire.GetComponent<ConditionComponent>();

                    condition.SetDamage(_damage);

                    Destroy(gameObject);
                }
            }

            var collider = collision.GetComponent<ColliderComponent>();

            if (collider)
            {
                if (_side == SideType.Player) SoundManager.Singleton.HitBrick();
                if (collider.DestroyProjectile) Destroy(gameObject);
                if (collider.DestroyCell) Destroy(collider.gameObject);
            }

            var flag = collision.GetComponent<FlagScript>();

            if (flag)
            {
                if (flag.GetSideType != _side)
                {
                    GameEvents.Singleton.EndGame();
                    Destroy(flag.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}

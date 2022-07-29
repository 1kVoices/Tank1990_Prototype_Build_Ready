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

        private void Start()
        {
            _moveComp = GetComponent<MoveComponent>();
            Destroy(gameObject, _lifeTime);
        }

        public void SetParams(DirectionType direction, SideType side) => (_direction, _side) = (direction, side);

        private void Update()
        {
            _moveComp.OnMove(_direction, false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
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

            var ColComponent = collision.GetComponent<ColliderComponent>();

            if (ColComponent)
            {
                if (_side == SideType.Player) SoundManager.Singleton.HitBrick();
                if (ColComponent.DestroyProjectile) Destroy(gameObject);
                if (ColComponent.DestroyCell) Destroy(ColComponent.gameObject);
            }

            var flag = collision.GetComponent<FlagScript>();

            if (!flag) return;
            if (flag.GetSideType == _side) return;
            GameEvents.Singleton.EndGame();
            Destroy(flag.gameObject);
            Destroy(gameObject);
        }
    }
}

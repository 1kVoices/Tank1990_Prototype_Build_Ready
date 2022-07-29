using UnityEngine;

namespace Tanks
{
    public class ConditionComponent : MonoBehaviour
    {
        [SerializeField, Min(1)]
        protected int _health = 3;

        protected bool Invulnerable { get; set; }

        public int GetHealth => _health;

        public virtual void SetDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Debug.Log($"{name} has been killed");
                Destroy(gameObject);
            }
        }
    }
}

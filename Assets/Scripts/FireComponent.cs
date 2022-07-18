using System.Collections;
using UnityEngine;

namespace Tanks
{
    public class FireComponent : MonoBehaviour
    {
        private bool _canFire = true;

        [SerializeField, Range(0.1f, 2f)]
        private float _delayFire = 0.25f;

        [SerializeField]
        private Projectile _prefab;
         
        [SerializeField]
        private SideType _side;

        public SideType GetSideType => _side;

        public void OnFire()
        {
            if (_canFire)
            {
                _canFire = false;

                if (_side == SideType.Player) SoundManager.Singleton.PlayerShoot();

                var bullet = Instantiate(_prefab, transform.position, transform.rotation);

                bullet.SetParams(transform.eulerAngles.ConvertRotationFromType(), _side);

                StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            _canFire = false;
            yield return new WaitForSeconds(_delayFire);
            _canFire = true;
        }
    }
}

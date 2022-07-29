using UnityEngine;

namespace Tanks
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private SideType _side;

        public void OnMove(DirectionType type, bool isSendByBot)
        {
            if (!isSendByBot)
            {
                transform.position += type.ConvertTypeFromDirection() * (Time.deltaTime * _speed);
            }
            transform.eulerAngles = type.ConvertTypeFromRotation();

            if (_side != SideType.Player) return;
            var playerStuck = GetComponent<PlayerConditionComponent>().GetIsPlayerStuck;

            if (playerStuck) SoundManager.Singleton.PlayerStuck();
            else SoundManager.Singleton.PlayerMove();
        }
    }
}

using UnityEngine;

namespace Tanks
{
    public class PlayerStuckDetector : MonoBehaviour
    {
        [SerializeField]
        private PlayerConditionComponent _playerCondition;

        void OnTriggerStay2D(Collider2D col)
        {
            var ColComponent = col.GetComponent<ColliderComponent>();

            if (ColComponent) _playerCondition.SetIsPlayerStuck(true);
            
        }

        void OnTriggerExit2D(Collider2D col) => _playerCondition.SetIsPlayerStuck(false);
    }
}

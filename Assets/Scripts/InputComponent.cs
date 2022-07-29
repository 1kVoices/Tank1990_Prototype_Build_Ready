using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent))]
    public class InputComponent : MonoBehaviour
    {
        private DirectionType _lastType = DirectionType.Top;

        private MoveComponent _moveComp;
        private FireComponent _fireComp;
        private PlayerConditionComponent _playerComp;

        [SerializeField]
        private InputAction _move;

        [SerializeField]
        private InputAction _fire;

        [SerializeField]
        private InputAction _pause;

        [SerializeField]
        private GameObject _pauseMenu;

        private void Start()
        {
            _moveComp = GetComponent<MoveComponent>();
            _fireComp = GetComponent<FireComponent>();
            _playerComp = GetComponent<PlayerConditionComponent>();

            _move.Enable();
            _fire.Enable();
            _pause.Enable();
            _pause.performed += Pause;
        }

        private void FixedUpdate()
        {
            var button = _fire.ReadValue<float>();
            if (button == 1f) _fireComp.OnFire();

            var direction = _move.ReadValue<Vector2>();
            DirectionType type;

            if (direction.x != 0f && direction.y != 0f)
            {
                type = _lastType;
            }
            else if (direction.x == 0f && direction.y == 0f) return;
            else type = _lastType = direction.ConvertDirectionFromType();

            _moveComp.OnMove(type, false);
        }

        private void Pause(InputAction.CallbackContext context)
        {
            if (_playerComp.IsGettingHit) return;
            SoundManager.Singleton.GamePause();

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }

        private void OnDestroy()
        {
            _move.Dispose();
            _fire.Dispose();
            _pause.Dispose();
        }
    }
}

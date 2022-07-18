using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Tanks
{
    public class BotComponent : MonoBehaviour
    {
        [SerializeField]
        private MoveComponent _moveComp;

        [SerializeField]
        private FireComponent _fireComp;

        [SerializeField]
        private CollisionDetector _collDetector;

        [SerializeField]
        private Collider2D _mainCol;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Sprite[] _sprites;

        private bool _isHardBot;

        private DirectionType _direction;

        private bool _isPlayerInSight;

        private List<DirectionType> _directions = 
            new List<DirectionType> { DirectionType.Top, DirectionType.Right, DirectionType.Bottom, DirectionType.Left };

        private int _layer;

        void Start()
        {
            _layer = LayerMask.GetMask("Level");
            _spriteRenderer.sprite = _sprites[UnityEngine.Random.Range(0, _sprites.Length)];
        }

        void Update()
        {
            if (_isHardBot)
                SeekPlayer();
        }

        void FixedUpdate()
        {
            _moveComp.OnMove(_direction, _isPlayerInSight);
        }

        void LateUpdate()
        {
            _fireComp.OnFire();
        }

        public void SetBotDifficulty(bool b) => _isHardBot = b;

        void SeekPlayer()
        {
            RaycastHit2D hitPlayerRight = Physics2D.Raycast(transform.position, Vector2.right, 15f, _layer);
            RaycastHit2D hitPlayerLeft = Physics2D.Raycast(transform.position, -Vector2.right, 15f, _layer);
            RaycastHit2D hitPlayerTop = Physics2D.Raycast(transform.position, Vector2.up, 15f, _layer);
            RaycastHit2D hitPlayerBot = Physics2D.Raycast(transform.position, -Vector2.up, 15f, _layer);

            Debug.DrawRay(transform.position, Vector2.right * 15f, Color.red);
            Debug.DrawRay(transform.position, -Vector2.right * 15f, Color.red);
            Debug.DrawRay(transform.position, Vector2.up * 15f, Color.red);
            Debug.DrawRay(transform.position, -Vector2.up * 15f, Color.red);


            if (hitPlayerRight.collider.GetComponent<PlayerConditionComponent>()) 
            {                                                 
                _isPlayerInSight = true;
                _direction = DirectionType.Right;
            }

            else if (hitPlayerLeft.collider.GetComponent<PlayerConditionComponent>())
            {
                _isPlayerInSight = true;
                _direction = DirectionType.Left;
            }

            else if (hitPlayerTop.collider.GetComponent<PlayerConditionComponent>())
            {
                _isPlayerInSight = true;
                _direction = DirectionType.Top;
            }

            else if (hitPlayerBot.collider.GetComponent<PlayerConditionComponent>())
            {
                _isPlayerInSight = true;
                _direction = DirectionType.Bottom;
            }

            else _isPlayerInSight = false;
        }

        public void SetDirection(DirectionType[] Options)
        {
            if (Options.Length < 1) return;
            _direction = Options[UnityEngine.Random.Range(0, Options.Length)];
            Invoke(nameof(ResetColliderCheck), 0.1f);
        }

        public void SwitchMainCollider(bool isEnable) => _mainCol.isTrigger = !isEnable;
        
        void ResetColliderCheck() => _collDetector.SetFirstCol(true);   
    }
}

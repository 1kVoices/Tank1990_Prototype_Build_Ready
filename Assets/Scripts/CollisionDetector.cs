using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField]
        private BotComponent _botComp;

        private int _layer;

        private bool _firstCol = true;
        public bool GetFirstCol => _firstCol;

        void Start()
        {
            _layer = LayerMask.GetMask("Level");
            _botComp.SetDirection(SeekRoute());
        }
        public void SetFirstCol(bool b) => _firstCol = b;

        void OnTriggerStay2D(Collider2D col)
        {
            if (col.GetComponent<ColliderComponent>())
            {
                if (_firstCol)
                {
                    _botComp.SwitchMainCollider(true);
                    _firstCol = false;
                    _botComp.SetDirection(SeekRoute());
                }
            }

            if (col.GetComponent<BotComponent>())
            {
                _botComp.SwitchMainCollider(false);
            }
        }

        DirectionType[] SeekRoute()
        {
            RaycastHit2D hitTop = Physics2D.Raycast(transform.position, Vector2.up, 0.7f, _layer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, _layer);
            RaycastHit2D hitBot = Physics2D.Raycast(transform.position, -Vector2.up, 0.7f, _layer);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 0.7f, _layer);

            var result = new List<DirectionType>();

            if (!hitTop.collider) result.Add(DirectionType.Top);
            if (!hitRight.collider) result.Add(DirectionType.Right);
            if (!hitBot.collider) result.Add(DirectionType.Bottom);
            if (!hitLeft.collider) result.Add(DirectionType.Left);

            return result.ToArray();
        }
    }
}

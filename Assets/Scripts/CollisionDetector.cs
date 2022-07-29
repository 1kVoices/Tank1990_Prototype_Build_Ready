using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField]
        private BotComponent _botComp;

        private int _layer;

        private bool GetFirstCol { get; set; } = true;

        private void Start()
        {
            _layer = LayerMask.GetMask("Level");
            _botComp.SetDirection(SeekRoute());
        }
        public void SetFirstCol(bool b) => GetFirstCol = b;

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.GetComponent<ColliderComponent>())
            {
                if (GetFirstCol)
                {
                    _botComp.SwitchMainCollider(true);
                    GetFirstCol = false;
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
            var hitTop = Physics2D.Raycast(transform.position, Vector2.up, 0.7f, _layer);
            var hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, _layer);
            var hitBot = Physics2D.Raycast(transform.position, -Vector2.up, 0.7f, _layer);
            var hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 0.7f, _layer);

            List<DirectionType> result = new List<DirectionType>();

            if (!hitTop.collider) result.Add(DirectionType.Top);
            if (!hitRight.collider) result.Add(DirectionType.Right);
            if (!hitBot.collider) result.Add(DirectionType.Bottom);
            if (!hitLeft.collider) result.Add(DirectionType.Left);

            return result.ToArray();
        }
    }
}

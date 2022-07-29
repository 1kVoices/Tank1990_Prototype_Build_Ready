using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Tanks
{
    public static class Extensions 
    {
        private static readonly Dictionary<DirectionType, Vector3> _directions;
        private static readonly Dictionary<DirectionType, Vector3> _rotations;

        static Extensions()
        {
            _directions = new Dictionary<DirectionType, Vector3>()
            {
                {DirectionType.Top, new Vector3(0f, 1f, 0f) },
                {DirectionType.Right, new Vector3(1f, 0f, 0f) },
                {DirectionType.Bottom, new Vector3(0f, -1f, -0) },
                {DirectionType.Left, new Vector3(-1f, 0f, 0f) },

            };
            _rotations = new Dictionary<DirectionType, Vector3>()
            {
                {DirectionType.Top, new Vector3(0f, 0f, 0f) },
                {DirectionType.Right, new Vector3(0f, 0f, 270f) },
                {DirectionType.Bottom, new Vector3(0f, 0f, 180f) },
                {DirectionType.Left, new Vector3(0f, 0f, 90f) },
            };
        }

        public static Vector3 ConvertTypeFromDirection(this DirectionType type) => _directions[type];

        private static DirectionType ConvertDirectionFromType(this Vector3 direction) => _directions.First(x => x.Value == direction).Key;

        public static DirectionType ConvertDirectionFromType(this Vector2 direction) => ConvertDirectionFromType((Vector3)direction);

        public static Vector3 ConvertTypeFromRotation(this DirectionType type) => _rotations[type];

        public static DirectionType ConvertRotationFromType(this Vector3 rotation) => _rotations.First(x => x.Value == rotation).Key;

    }

    public enum DirectionType : byte
    {
        Top, Right, Bottom, Left
    }

    public enum SideType : byte
    {
        None, Player, Enemy
    }
}

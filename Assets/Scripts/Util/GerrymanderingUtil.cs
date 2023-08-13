using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model;
using UnityEngine;

namespace Util
{
    public enum Direction
    {
        Left,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft
    }

    public abstract class GerrymanderingUtil
    {

        [Pure]
        public static Direction VecToDir(Vector3Int origin, Vector3Int next)
        {
            var diff = origin - next;
            
            if (diff == Vector3Int.left) return Direction.Left;
            if (diff == new Vector3Int(-1, 1, 0)) return Direction.TopLeft;
            if (diff == Vector3Int.up) return Direction.Top;
            if (diff == new Vector3Int(1, 1, 0)) return Direction.TopRight;
            if (diff == Vector3Int.right) return Direction.Right;
            if (diff == new Vector3Int(1, -1, 0)) return Direction.BottomRight;
            if (diff == Vector3Int.down) return Direction.Bottom;
            if (diff == new Vector3Int(-1, -1, 0)) return Direction.BottomLeft;
            throw new ArgumentException("out of range for difference " + diff);
        } 
        
        [Pure]
        public static Vector3Int DirToVec(Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector3Int.left,
                Direction.TopLeft => new Vector3Int(-1, 1, 0),
                Direction.Top => Vector3Int.up,
                Direction.TopRight => new Vector3Int(1, 1, 0),
                Direction.Right => Vector3Int.right,
                Direction.BottomRight => new Vector3Int(1, -1, 0),
                Direction.Bottom => Vector3Int.down,
                Direction.BottomLeft => new Vector3Int(-1, -1, 0),
                _ => Vector3Int.zero
            };
        }
        
        [Pure]
        public static int MaxIndex(IReadOnlyList<int> list)
        {
            var maxIndex = 0;

            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] > list[maxIndex])
                    maxIndex = i;
            }

            return maxIndex;
        }

        public static Color GetColor(Faction faction)
        {
            return faction switch
            {
                Faction.Neutral => Color.grey,
                Faction.Democrats => Color.blue,
                Faction.Republicans => Color.red,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
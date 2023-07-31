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
        Top,
        Right,
        Bottom
    }
    
    public enum DiagonalDirection
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
        public static Vector3Int DirToVec(Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector3Int.left,
                Direction.Top => Vector3Int.up,
                Direction.Right => Vector3Int.right,
                Direction.Bottom => Vector3Int.down,
                _ => Vector3Int.zero
            };
        }

        [Pure]
        public static Direction VecToDir(Vector3Int origin, Vector3Int next)
        {
            if ((origin - next).sqrMagnitude != 1) throw new ArgumentException("the vectors need to be 1 apart");

            var diff = origin - next;

            if (diff == Vector3Int.left) return Direction.Left;
            if (diff == Vector3Int.up) return Direction.Top;
            if (diff == Vector3Int.right) return Direction.Right;
            if (diff == Vector3Int.down) return Direction.Bottom;
            throw new ArgumentException("direction is neither left, top, right, bottom");
        }

        [Pure]
        public static DiagonalDirection VecToDiagDir(Vector3Int origin, Vector3Int next)
        {
            var diff = origin - next;
            
            if (diff == Vector3Int.left) return DiagonalDirection.Left;
            if (diff == new Vector3Int(-1, 1, 0)) return DiagonalDirection.TopLeft;
            if (diff == Vector3Int.up) return DiagonalDirection.Top;
            if (diff == new Vector3Int(1, 1, 0)) return DiagonalDirection.TopRight;
            if (diff == Vector3Int.right) return DiagonalDirection.Right;
            if (diff == new Vector3Int(1, -1, 0)) return DiagonalDirection.BottomRight;
            if (diff == Vector3Int.down) return DiagonalDirection.Bottom;
            if (diff == new Vector3Int(-1, -1, 0)) return DiagonalDirection.BottomLeft;
            throw new ArgumentException("out of range for difference " + diff);
        } 
        
        [Pure]
        public static Vector3Int DiagDirToVec(DiagonalDirection direction)
        {
            return direction switch
            {
                DiagonalDirection.Left => Vector3Int.left,
                DiagonalDirection.TopLeft => new Vector3Int(-1, 1, 0),
                DiagonalDirection.Top => Vector3Int.up,
                DiagonalDirection.TopRight => new Vector3Int(1, 1, 0),
                DiagonalDirection.Right => Vector3Int.right,
                DiagonalDirection.BottomRight => new Vector3Int(1, -1, 0),
                DiagonalDirection.Bottom => Vector3Int.down,
                DiagonalDirection.BottomLeft => new Vector3Int(-1, -1, 0),
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
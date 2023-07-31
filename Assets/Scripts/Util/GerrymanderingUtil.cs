using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model;
using UnityEngine;

namespace Util
{
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
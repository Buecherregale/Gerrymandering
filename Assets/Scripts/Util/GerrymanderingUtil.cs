﻿using System;
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
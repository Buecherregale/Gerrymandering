using UnityEngine;
using UnityEngine.Tilemaps;

namespace Util
{
    /// <summary>
    /// nur test klasse, später richtig ausbauen
    /// </summary>
    public class ManipulateBorders: MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Top,
            Right,
            Bottom
        }

        private static Vector3Int Mapper(Direction direction)
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

        [SerializeField]
        [Tooltip("4 Maps, one for every direction")]
        private Tilemap[] borderMaps;

        [SerializeField]
        [Tooltip("4 Tiles, one for every direction")]
        private Tile[] borderTiles;

        public void AddBorder(Vector3Int position, Direction direction)
        {
            SetBorder(position, direction);
            var complementary = ((int) direction + 2) % 4;
            var complDir = (Direction)complementary;
            SetBorder(position + Mapper(complDir), complDir);
        }

        private void SetBorder(Vector3Int position, Direction direction)
        {
            borderMaps[(int) direction].SetTile(position, borderTiles[(int) direction]);
        }
    }
}
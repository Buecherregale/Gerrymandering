using UnityEngine;
using UnityEngine.Tilemaps;

namespace Util
{
    /// <summary>
    /// nur test klasse, später richtig ausbauen
    /// </summary>
    public class ManipulateBorders: MonoBehaviour
    {

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
            SetBorder(position + GerrymanderingUtil.DirToVec(complDir), complDir);
        }

        private void SetBorder(Vector3Int position, Direction direction)
        {
            borderMaps[(int) direction].SetTile(position, borderTiles[(int) direction]);
        }
    }
}
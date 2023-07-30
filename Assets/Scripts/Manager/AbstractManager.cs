using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manager
{
    /// <summary>
    /// provides util for all managers
    /// access to all tilemaps
    /// </summary>
    public class AbstractManager: MonoBehaviour
    {
        [SerializeField] protected Tilemap districtMap;
        [SerializeField] protected Tilemap markMap;
        [SerializeField] protected Tilemap[] borderMaps = new Tilemap[4];

        [SerializeField] protected Tile[] borderTiles = new Tile[4];
        [SerializeField] protected Tile fillTile;
        
        /// <summary>
        /// always call this for validation as child class
        /// </summary>
        private void OnValidate()
        {
            if (districtMap == null) throw new ArgumentException();
            if (markMap == null) throw new ArgumentException();

            if (borderMaps is not { Length: 4 }) throw new ArgumentException();

            if (borderTiles is not { Length: 4 }) throw new ArgumentException();

            if (fillTile == null) throw new ArgumentException();
        }
    }
}
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
        [SerializeField] [Tooltip("Map consisting of DistrictTiles representing the votes")]
        protected Tilemap districtMap;
        [SerializeField] [Tooltip("Map for the mark tiles")] 
        protected Tilemap markMap;
        [SerializeField]  [Tooltip("8 Maps, one for every direction with diagonals")] 
        protected Tilemap[] borderMaps = new Tilemap[8];

        [SerializeField] [Tooltip("8 Tiles, one for every direction with diagonals")] 
        protected Tile[] borderTiles = new Tile[8];
        [SerializeField] [Tooltip("Tile to mark the district in winning color")]
        protected Tile markTile;
        
        /// <summary>
        /// always call this for validation as child class
        /// </summary>
        protected void OnValidate()
        {
            if (districtMap == null) throw new ArgumentException();
            if (markMap == null) throw new ArgumentException();

            if (borderMaps is not { Length: 8 }) throw new ArgumentException();

            if (borderTiles is not { Length: 8 }) throw new ArgumentException();

            if (markTile == null) throw new ArgumentException();
        }
    }
}
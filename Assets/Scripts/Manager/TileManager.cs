using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manager
{
    /// <summary>
    /// provides util for all managers
    /// access to all tilemaps
    /// </summary>
    public class TileManager: MonoBehaviour
    {
        [SerializeField] [Tooltip("Map consisting of DistrictTiles representing the votes")]
        internal Tilemap districtMap;
        [SerializeField] [Tooltip("Map for the mark tiles")] 
        internal Tilemap markMap;
        [SerializeField]  [Tooltip("8 Maps, one for every direction with diagonals")] 
        internal Tilemap[] borderMaps = new Tilemap[8];

        [SerializeField] [Tooltip("8 Grey Tiles, one for every direction with diagonals")] 
        internal Tile[] borderTilesNeutral = new Tile[8];
        [SerializeField] [Tooltip("8 Red Tiles, one for every direction with diagonals")] 
        internal Tile[] borderTilesRepublicans = new Tile[8];
        [SerializeField] [Tooltip("8 Blue Tiles, one for every direction with diagonals")] 
        internal Tile[] borderTilesDemocrats = new Tile[8];
        [SerializeField] [Tooltip("Tile to mark the district in winning color")]
        internal Tile markTile;
        
        /// <summary>
        /// always call this for validation as child class
        /// </summary>
        internal void OnValidate()
        {
            if (districtMap == null) throw new ArgumentException();
            if (markMap == null) throw new ArgumentException();

            if (borderMaps is not { Length: 8 }) throw new ArgumentException();

            if (borderTilesNeutral is not { Length: 8 }) throw new ArgumentException();
            if (borderTilesRepublicans is not { Length: 8 }) throw new ArgumentException();
            if (borderTilesDemocrats is not { Length: 8 }) throw new ArgumentException();

            if (markTile == null) throw new ArgumentException();
        }
    }
}
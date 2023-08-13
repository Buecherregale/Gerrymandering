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
        private Tile[] borderTilesNeutral = new Tile[8];
        [SerializeField] [Tooltip("8 Red Tiles, one for every direction with diagonals")] 
        private Tile[] borderTilesRepublicans = new Tile[8];
        [SerializeField] [Tooltip("8 Blue Tiles, one for every direction with diagonals")] 
        private Tile[] borderTilesDemocrats = new Tile[8];
        [SerializeField] [Tooltip("Tile to mark the district in winning color")]
        internal Tile markTile;
        
        [SerializeField] 
        private StateManager stateManager;
        [SerializeField]
        private DistrictManager districtManager;

        public Tile[][] BorderTilesByParty { get; private set; }

        
        /// <summary>
        /// When the Scene is Loaded the appropriate districts are loaded
        /// The Districts are instantiated and all references are getting set
        /// At Last the winning faction is set
        /// </summary>
        private void Awake()
        {
            BorderTilesByParty = new[] { borderTilesNeutral, borderTilesDemocrats, borderTilesRepublicans };
            
            GameObject districts = Resources.Load<GameObject>("Level/1");
            if (districts == null) {
                Debug.LogError("Districts not found");
                return;
            }
            GameObject instantiatedDistricts = Instantiate(districts, GameObject.FindGameObjectWithTag("Grid").transform);
            districtMap = instantiatedDistricts.GetComponent<Tilemap>();
            
            districtManager.LoadEveryTile();
            
            LevelData levelData = instantiatedDistricts.GetComponent<LevelData>();
            stateManager.SetWinningFaction(levelData.Winning);
            
        }

        private void Start()
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
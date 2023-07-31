using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model;
using Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Manager
{
    /// <summary>
    /// manages the <see cref="District"/>.
    /// Able to get all DistrictTiles based on position.
    /// </summary>
    public class DistrictManager: MonoBehaviour
    {
        [SerializeField] private TileManager tileManager;
        
        [NotNull] private readonly Dictionary<Vector3Int, District> _districts = new();

        /// <summary>
        /// calculates all neighbouring districts
        /// does not use diagonal neighbours
        /// tests using <see cref="Tilemap.HasTile"/>
        /// </summary>
        /// <param name="pos">cell position</param>
        /// <returns>cell position of existing neighbouring tiles</returns>
        [NotNull]
        [Pure]
        public IEnumerable<Vector3Int> CalculateNeighbours(Vector3Int pos)
        {
            Vector3Int[] neighbourOffsets =
            {
                new(0, 1, 0), // Top
                new(0, -1, 0), // Bottom
                new(1, 0, 0), // Right
                new(-1, 0, 0) // Left
            };

            return from offset in neighbourOffsets
                select pos + offset
                into neighbourPosition
                where tileManager.districtMap.HasTile(neighbourPosition)
                select neighbourPosition;
        }

        /// <summary>
        /// ALL neighbouring districts even diagonals.
        /// Does not include the null tiles.
        /// Does guarantee that the tilemap has a tile at the position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>ALL neighbouring tile locations</returns>
        [NotNull]
        [Pure]
        public IEnumerable<Vector3Int> GetAllNeighbours(Vector3Int pos)
        {
            Vector3Int[] neighbourOffsets =
            {
                new(-1, 0, 0), // Left
                new (-1, 1, 0), // Top Left
                new (0, 1, 0), // Top
                new(1, 1, 0), // Top Right
                new(1, 0, 0), // Right,
                new (1, -1, 0), // Bottom Right
                new(0, -1, 0), // Bottom
                new(-1, -1, 0) // Bottom Left
            };

            return from offset in neighbourOffsets
                select pos + offset
                into neighbourPosition
                where tileManager.districtMap.HasTile(neighbourPosition)
                select neighbourPosition;
        }

        /// <param name="pos">cell position on <see cref="TileManager.districtMap"/></param>
        /// <returns>the district at the position</returns>
        /// <exception cref="ArgumentException">if there is no district</exception>
        [NotNull]
        [Pure]
        public District GetDistrict(Vector3Int pos)
        {
            if (!_districts.ContainsKey(pos)) throw new ArgumentException("no district on position: " + pos);
            return _districts[pos];
        }

        /// <summary>
        /// tries to get the district at the position.
        /// if this returns true, district is guaranteed to not be null
        /// </summary>
        /// <param name="pos">position of the district</param>
        /// <param name="district">district at the position or NULL</param>
        /// <returns>if the district exists</returns>
        public bool TryGetDistrict(Vector3Int pos, [CanBeNull] out District district)
        {
            if (_districts.TryGetValue(pos, out var district1))
            {
                district = district1;
                return true;
            }
            district = null;
            return false;
        }
        
        /// <summary>
        /// draws the county border on every side of the district that points to a different county than district 
        /// </summary>
        /// <param name="district">the last added district</param>
        public void DrawCountyBorder([NotNull] District district)
        {
            if (district.County == null)
                return;
            GetAllNeighbours(district.Position)
                .Select(GetDistrict)
                .Where(neighbour => district.County != neighbour.County)
                .Select(neighbour => GerrymanderingUtil.VecToDir(neighbour.Position, district.Position))
                .ToList()
                .ForEach(direction => 
                    tileManager.borderMaps[(int) direction]
                        .SetTile(district.Position, tileManager.BorderTilesByParty[(int) district.County.Winning][(int) direction]));
        }

        /// <summary>
        /// deletes all county borders on the district
        /// </summary>
        /// <param name="district">a district to clear the tiles from</param>
        public void ClearCountyBorders([NotNull] District district)
        {
            foreach (var borderMap in tileManager.borderMaps)
            {
                if (borderMap.HasTile(district.Position)) borderMap.SetTile(district.Position, null);
            }
        }
        
        /// <summary>
        /// needs to be the first thing called
        /// awake or start?
        /// enters the information for every tile
        /// tiles in <see cref="TileManager.districtMap"/> have to be <see cref="DistrictTile">Districts</see>
        /// </summary>
        private void Start()
        {
            // load all districts of district map 
            var bounds = tileManager.districtMap.cellBounds;

            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (var y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var tileCellPos = new Vector3Int(x, y, 0);
                    var tile = tileManager.districtMap.GetTile<DistrictTile>(tileCellPos);
                    
                    if (tile == null) continue;
                    
                    _districts.Add(tileCellPos, new District(tile, tileCellPos));
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model;
using Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manager
{
    public class DistrictManager : AbstractManager
    {
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
                where districtMap.HasTile(neighbourPosition)
                select neighbourPosition;
        }
        
        /// <param name="pos">cell position on <see cref="AbstractManager.districtMap"/></param>
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
        /// needs to be the first thing called
        /// awake or start?
        /// enters the information for every tile
        /// tiles in <see cref="AbstractManager.districtMap"/> have to be <see cref="DistrictTile">Districts</see>
        /// <exception cref="ArgumentException">if a tile is not a <see cref="DistrictTile"/></exception>
        /// </summary>
        private void Start()
        {
            // load all districts of district map 
            var bounds = districtMap.cellBounds;

            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (var y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var tileCellPos = new Vector3Int(x, y, 0);
                    var tile = districtMap.GetTile<DistrictTile>(tileCellPos);

                    // if (tile == null) throw new ArgumentException("tile " + tile + " is not a District. Position: " + districtMap.CellToWorld(tileCellPos));
                    if (tile == null) continue;
                    
                    _districts.Add(tileCellPos, new District(tile, tileCellPos, 0));
                }
            }
        }
    }
}
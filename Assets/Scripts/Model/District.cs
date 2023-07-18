using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Model
{
    /// <summary>
    /// smallest part in the voting system
    /// it's the tile you click on
    /// it is contained by a single <see cref="Model.County"/>
    /// </summary>
    public class District: GerrymanderingTile
    {
        public County County { get; set; }
        public Faction Faction { get; set; } 

        public bool Marked { get; set; }

        /// <summary>
        /// see: <see cref="Neighbours"/>
        /// </summary>
        [SerializeField] 
        private Tilemap tilemap;
        
        /// <summary>
        /// calculates all neighbours to this tile that exist
        /// and are of type <see cref="District"/>
        /// This needs access to the tilemap AND it's position within it
        /// min length is 0 but practically it should be at least 2
        /// max length is 4 if all neighbours exist
        /// </summary>
        /// <returns>an enumerable containing neighbouring tiles</returns>
        [NotNull]
        public IEnumerable<District> Neighbours()
        {
            // Define the relative positions of the neighboring tiles
            Vector3Int[] neighborOffsets = {
                new(0, 1, 0),  // Top
                new(0, -1, 0), // Bottom
                new(1, 0, 0),  // Right
                new(-1, 0, 0)  // Left
            };

            return (from offset in neighborOffsets
                let tilePosition = Vector3Int.RoundToInt(transform.GetPosition()) 
                select tilePosition + offset into neighborPosition 
                select tilemap.GetTile<District>(neighborPosition) into neighborTile 
                where neighborTile != null 
                select neighborTile)
                .ToArray();
        }
    }
}
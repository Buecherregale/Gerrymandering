using JetBrains.Annotations;
using Manager;
using Unity;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// models the smallest part of the voting system.
    /// only holds data.
    /// Multiple Districts form a <see cref="Model.County"/>.
    /// Districts are managed by a <see cref="DistrictManager"/>
    /// </summary>
    public class District
    {
        private static int _instanceCounter;
        
        [NotNull]
        public readonly DistrictTile Tile;
        
        public readonly int Id;

        [CanBeNull]
        public County County { get; internal set; }
        public Faction Faction => Tile.Faction;
        public Vector3Int Position { get; private set; }
        
        public District([NotNull] DistrictTile tile, Vector3Int pos)
        {
            Tile = tile;
            Position = pos;
            Id = _instanceCounter;
            _instanceCounter++;
        }
        ~District()
        {
            _instanceCounter--;
        }
    }
}
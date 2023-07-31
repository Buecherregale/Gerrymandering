using Model;
using UnityEngine;

namespace Unity
{
    /// <summary>
    /// A tile representing a district in the voting system.
    /// it has a <see cref="Faction"/> which the district is voting for
    /// </summary>
    public class DistrictTile: GerrymanderingTile
    {
        [SerializeField] private Faction faction;

        public Faction Faction => faction;
    }
}
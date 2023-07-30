using Model;
using UnityEngine;

namespace Unity
{
    public class DistrictTile: GerrymanderingTile
    {
        [SerializeField] private Faction faction;

        public Faction Faction => faction;
    }
}
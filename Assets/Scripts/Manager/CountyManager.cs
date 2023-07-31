using System;
using System.Linq;
using JetBrains.Annotations;
using Model;
using UnityEngine;
using Util;

namespace Manager
{
    /// <summary>
    /// manages <see cref="County"/>.
    /// Whole class could be static
    /// TODO: coloring and borders of counties
    /// </summary>
    public class CountyManager: AbstractManager
    {
        [SerializeField] 
        private DistrictManager districtManager;

        /// <summary>
        /// adds the district to the county
        /// checks if it's already contained
        /// <see cref="CanAddToCounty"/>
        /// </summary>
        /// <param name="county">the county to add to</param>
        /// <param name="district">needs to meet <see cref="CanAddToCounty"/> requirements</param>
        /// <returns>if successful</returns>
        public bool AddDistrict([NotNull] County county, [NotNull] District district)
        {
            if (county.Districts.Contains(district)) return false;
            if (!CanAddToCounty(county, district)) return false;

            county.Districts.Add(district);
            district.County = county;
            county.Winning = CalculateWinning(county);
            
            districtManager.DrawCountyBorder(district);
            foreach (var neighbour in districtManager.CalculateNeighbours(district.Position))
            {
                var neighbourDist = districtManager.GetDistrict(neighbour);
                districtManager.ClearCountyBorders(neighbourDist);
                districtManager.DrawCountyBorder(neighbourDist);
            }
            
            return true;
        }

        /// <summary>
        /// removes the district from the county
        /// checks for contains
        /// </summary>
        /// <param name="county">the county to remove from</param>
        /// <param name="district">to remove</param>
        /// <returns>if successful</returns>
        public bool RemoveDistrict([NotNull] County county, [NotNull] District district)
        {
            if (!county.Districts.Contains(district)) return false;
            county.Districts.Remove(district);
            district.County = null;
            
            districtManager.ClearCountyBorders(district);
            foreach (var neighbour in districtManager.CalculateNeighbours(district.Position))
            {
                var neighbourDist = districtManager.GetDistrict(neighbour);
                districtManager.ClearCountyBorders(neighbourDist);
                districtManager.DrawCountyBorder(neighbourDist);
            }

            return true;
        }

        /// <summary>
        /// clears all districts in the county
        /// </summary>
        /// <param name="county">county to clear</param>
        public void Clear([NotNull] County county)
        {
            var districtsCopy = new District[county.Size];
            county.Districts.CopyTo(districtsCopy);
            foreach (var district in districtsCopy)
            {
                RemoveDistrict(county, district);
            }
        }

        /// <summary>
        /// works for any number of possible factions
        /// </summary>
        /// <returns>the faction with the most voting <see cref="District">Districts</see></returns>
        [Pure]
        private static Faction CalculateWinning([NotNull] County county)
        {
            /* holds the votes to each faction based on the (int) cast */
            var votes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var dist in county.Districts)
            {
                votes[(int) dist.Faction]++;
            }

            return (Faction) GerrymanderingUtil.MaxIndex(votes);
        }

        /// <summary>
        /// A <see cref="District"/> can be added to a county if:
        /// <list type="bullet">
        ///     <item>the district is empty OR</item>
        ///     <item>a neighbouring district is part of the county</item>
        /// </list>
        /// </summary>
        /// <param name="county">the county to try add to</param>
        /// <param name="district">the district to test</param>
        /// <returns>if the game should allow the district to be added to the county</returns>
        [Pure]
        private bool CanAddToCounty([NotNull] County county, [NotNull] District district)
        {
            return county.Size == 0 || districtManager.CalculateNeighbours(district.Position)
                .Select(neighbourPos => districtManager.GetDistrict(neighbourPos))
                .Any(neighbour => county == neighbour.County);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Util;

namespace Model
{
    /// <summary>
    /// middle level of the model
    /// contains a variable number of <see cref="District">Districts</see>
    /// it is itself contained by a single <see cref="State"/>
    /// </summary>
    public class County
    {
        private readonly List<District> _districts = new();

        private int _id;

        public Faction Winning { get; private set; }

        public County(int id)
        {
            _id = id;
        }
        
        /// <summary>
        /// adds the district to the county
        /// checks if it's already contained
        /// </summary>
        /// <param name="district">needs to meet <see cref="CanAddToCounty"/> requirements</param>
        /// <returns>if successful</returns>
        public bool AddDistrict(District district)
        {
            if (_districts.Contains(district)) return false;
            if (CanAddToCounty(district)) return false;

            _districts.Add(district);
            Winning = CalculateDominant();

            return true;
        }

        /// <summary>
        /// removes the district from the county
        /// checks for contains
        /// </summary>
        /// <param name="district">to remove</param>
        /// <returns>if successful</returns>
        public bool RemoveDistrict(District district)
        {
            if (!_districts.Contains(district)) return false;

            _districts.Remove(district);
            return true;
        }

        /// <summary>
        /// works for any number of possible factions
        /// </summary>
        /// <returns>the faction with the most voting <see cref="District">Districts</see></returns>
        [Pure]
        private Faction CalculateDominant()
        {
            /* holds the votes to each faction based on the (int) cast */
            var votes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var dist in _districts)
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
        /// <param name="district"></param>
        /// <returns>if the game should allow the district to be added to the county</returns>
        [Pure]
        private bool CanAddToCounty([NotNull] District district)
        {
            return _districts.Count == 0 || district.Neighbours().Any(neighbour =>  this == neighbour.County);
        }
    }
}
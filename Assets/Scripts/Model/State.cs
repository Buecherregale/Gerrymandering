using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Util;

namespace Model
{
    /// <summary>
    /// consists of multiple <see cref="County">Counties</see>
    /// </summary>
    public class State
    {
        private readonly List<County> _counties = new();
        
        public Faction Winning { get; private set; }

        /// <summary>
        /// adds the county to the state
        /// checks if already contained
        /// </summary>
        /// <param name="county">to add</param>
        /// <returns>if adding is successful</returns>
        public bool AddCounty(County county)
        {
            if (_counties.Contains(county)) return false;
            
            _counties.Add(county);
            Winning = CalculateDominant();
            return true;
        }

        /// <summary>
        /// removes the county again
        /// checks if contained
        /// </summary>
        /// <param name="county">to remove</param>
        /// <returns>if removing is successful</returns>
        public bool RemoveCounty(County county)
        {
            if (!_counties.Contains(county)) return false;

            _counties.Remove(county);
            Winning = CalculateDominant();
            return true;
        }

        /// <summary>
        /// pretty much same impl as <see cref="County.CalculateDominant()">County.CalculateDominant()</see> 
        /// </summary>
        /// <returns>the currently leading <see cref="Faction"/></returns>
        [Pure]
        private Faction CalculateDominant()
        {
            var countyVotes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var county in _counties)
            {
                countyVotes[(int) county.Winning]++;
            }

            return (Faction) GerrymanderingUtil.MaxIndex(countyVotes);
        }
    }
}
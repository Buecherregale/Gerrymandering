using System;
using JetBrains.Annotations;
using Model;
using Util;

namespace Manager
{
    /// <summary>
    /// manages <see cref="State"/>
    /// </summary>
    public class StateManager
    {
        /// <summary>
        /// adds a county to the state
        /// </summary>
        /// <param name="state">the state to add to</param>
        /// <param name="county">the county to add</param>
        /// <returns>success</returns>
        public bool AddCounty([NotNull] State state, [NotNull] County county)
        {
            if (state.Counties.Contains(county)) return false;
            
            state.Counties.Add(county);
            state.Winning = CalculateDominant(state);
            return true;
        }

        /// <summary>
        /// removes the county again
        /// checks if contained
        /// </summary>
        /// <param name="state">the state to remove from</param>
        /// <param name="county">to remove</param>
        /// <returns>if removing is successful</returns>
        public bool RemoveCounty([NotNull] State state, [NotNull] County county)
        {
            if (!state.Counties.Contains(county)) return false;

            state.Counties.Remove(county);
            state.Winning = CalculateDominant(state);
            return true;
        }
        
        /// <summary>
        /// pretty much same impl as <see cref="County.CalculateDominant()">County.CalculateDominant()</see> 
        /// </summary>
        /// <returns>the currently leading <see cref="Faction"/></returns>
        [Pure]
        private Faction CalculateDominant([NotNull] State state)
        {
            var countyVotes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var county in state.Counties)
            {
                countyVotes[(int) county.Winning]++;
            }

            return (Faction) GerrymanderingUtil.MaxIndex(countyVotes);
        }
    }
}
using System;
using System.Linq;
using JetBrains.Annotations;
using Model;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Manager
{
    /// <summary>
    /// manages <see cref="State"/>
    /// </summary>
    public class StateManager: MonoBehaviour
    {
        [SerializeField] private TileManager tileManager;
        [SerializeField] private CountyManager countyManager;
        [SerializeField] private DistrictManager districtManager;

        [SerializeField] private int maxCountySize = 3;
        public int MaxCountySize => maxCountySize;

        private int maxCounties;
        private int currentCountyCount;

        [NotNull]
        private readonly State _currentState = new ();

        private County _currentCounty;
        private bool _drawingCounty;

        private Faction _factionToWin = Faction.Republicans;
        [SerializeField] private TextMeshProUGUI text;
        
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
            
            currentCountyCount = state.Counties.Count;
            
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
        /// pretty much same impl as <see cref="CountyManager.CalculateWinning">County.CalculateDominant()</see> 
        /// </summary>
        /// <returns>the currently leading <see cref="Faction"/></returns>
        [Pure]
        private static Faction CalculateDominant([NotNull] State state)
        {
            var countyVotes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var county in state.Counties)
            {
                countyVotes[(int) county.Winning]++;
            }

            var maxInd = GerrymanderingUtil.MaxIndex(countyVotes);
            
            if (countyVotes.Count(x => x == countyVotes[maxInd]) >= 2)
                return Faction.Neutral;
            
            return (Faction) maxInd;
        }

        private void Start() {
            maxCounties = districtManager.GetDistrictCount() / maxCountySize;
            text.text = "take the " + _factionToWin + " to win";
        }

        #region Event Functions

        private void OnEnable()
        {
            InputEventSystem.OnInpBegin += OnInpBegin;
            InputEventSystem.OnInpDrag += OnInpDrag;
            InputEventSystem.OnInpEnd += OnInpEnd;
        }

        private void OnDisable()
        {
            InputEventSystem.OnInpBegin -= OnInpBegin;
            InputEventSystem.OnInpDrag -= OnInpDrag;
            InputEventSystem.OnInpEnd -= OnInpEnd;
        }

        private void OnInpBegin(Vector3 pos)
        {
            if (_drawingCounty) return;

            var tilePos = tileManager.districtMap.WorldToCell(pos);

            if (!districtManager.TryGetDistrict(tilePos, out var district)) return;

            if (district!.County != null)
            {
                // delete county
                RemoveCounty(_currentState, district.County);
                countyManager.Clear(district.County);
                return;
            }
            // draw new county
            _drawingCounty = true;
            _currentCounty = new County();

            if (!countyManager.AddDistrict(_currentCounty, district)) return;
            AddCounty(_currentState, _currentCounty);
        }
        
        private void OnInpDrag(Vector3 pos)
        {
            if (!_drawingCounty) return;

            var tilePos = tileManager.districtMap.WorldToCell(pos);
            if (!tileManager.districtMap.HasTile(tilePos)) return;
            var district = districtManager.GetDistrict(tilePos);

            if (district.County != null) return;
            // add to county
            countyManager.AddDistrict(_currentCounty, district);
        } 
        
        private void OnInpEnd(Vector3 pos)
        {
            if (!_drawingCounty) return;

            if (_currentCounty.Size != maxCountySize)
            {
                countyManager.Clear(_currentCounty);
                RemoveCounty(_currentState, _currentCounty);
            }

            _drawingCounty = false;
            _currentCounty = null;
            
            if (currentCountyCount == maxCounties && _currentState.Winning == _factionToWin) {
                text.text = "You took the " + _factionToWin + " to win the state";
            }
        }

        #endregion
    }
}
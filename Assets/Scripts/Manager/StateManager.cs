using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
        [SerializeField] private int maxCountyAmount = 5;
        
        public int MaxCountySize => maxCountySize;
        public int MaxCountyAmount => maxCountyAmount;
        public int CurrentCountyAmount => _currentState.Size;

        private int maxCounties;
        private int currentCountyCount;
        
        [FormerlySerializedAs("text")] [SerializeField] 
        private TextMeshProUGUI uiText;

        public UnityEvent countyAmountUpdated = new();
        
        [NotNull]
        private readonly State _currentState = new ();

        private County _currentCounty;
        private bool _drawingCounty;

        private Faction _factionToWin;
        
        public void SetWinningFaction(Faction faction)
        {
            if (faction == Faction.Neutral) {
                Debug.LogError("Faction.Neutral is not a valid faction to win");
            }
            _factionToWin = faction;
        }

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

        public Dictionary<Faction, int> VotesByFaction()
        {
            var votes = new Dictionary<Faction, int>();
            foreach (Faction faction in Enum.GetValues(typeof(Faction)))
            {
                votes.TryAdd(faction, 0);
            }

            foreach (var county in _currentState.Counties)
            {
                votes[county.Winning]++;
            }

            return votes;
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

        /// <summary>
        /// set the max counties by dividing the district count by the max county size
        /// set the text to the faction to win
        /// </summary>
        private void Start() {
            maxCounties = districtManager.GetDistrictCount() / maxCountySize;
            uiText.text = "take the " + _factionToWin + " to win";
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
                RemoveCounty(_currentState, district.County);                                                           // for some reason they have to be in that order
                countyManager.Clear(district.County);
                
                countyAmountUpdated.Invoke();
                return;
            }
            
            if (_currentState.Size == maxCountyAmount) return;
            // draw new county
            _drawingCounty = true;
            _currentCounty = new County();

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
            
            if (_currentCounty.Size < 2 || _currentCounty.Size > maxCountySize)
            {
                countyManager.Clear(_currentCounty);
                RemoveCounty(_currentState, _currentCounty);
            }
            countyAmountUpdated.Invoke();
            _drawingCounty = false;
            _currentCounty = null;
            
            // check if won
            if (currentCountyCount == maxCounties && _currentState.Winning == _factionToWin) {
                uiText.text = "You took the " + _factionToWin + " to win the state";
            }
            
            countyAmountUpdated.Invoke();
        }

        #endregion
    }
}
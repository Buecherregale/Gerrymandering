using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gerrymandering
{
    public class District
    {
        public Faction Winning { get; private set; } = Faction.Neutral;

        private readonly List<CountyTile> _tiles = new();

        public void AddTile([NotNull] CountyTile tile)
        {
            if (_tiles.Contains(tile)) return;
        
            _tiles.Add(tile);
            Winning = CalculateWinner();
        }

        [Pure]
        private Faction CalculateWinner()
        {
            var possibleFactions = Enum.GetNames(typeof(Faction)).Length;
            var votes = new int[possibleFactions];
        
            foreach (var county in _tiles)
            {
                votes[(int)CountyTile.Faction]++;
            }

            return (Faction)Enum.GetValues(typeof(Faction)).GetValue(MaxIndex(votes));
        }

        private static int MaxIndex([NotNull] IReadOnlyList<int> values)
        {
            var index = 0;
            var max = values[0];

            for (var i = 1; i < values.Count; i++)
            {
                if (values[i] <= max) continue;
            
                max = values[i];
                index = i;
            }

            return index;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Model
{
    public class County
    {
        private readonly List<District> _districts = new();

        private int _id;

        public Faction Winning { get; private set; }

        public County(int id)
        {
            _id = id;
        }
        
        public bool AddDistrict(District district)
        {
            if (_districts.Contains(district)) return false;
            if (CanAddToCounty(district)) return false;

            _districts.Add(district);
            Winning = CalculateDominant();

            return true;
        }

        public bool RemoveDistrict(District district)
        {
            if (!_districts.Contains(district)) return false;

            _districts.Remove(district);
            return true;
        }

        private Faction CalculateDominant()
        {
            /* holds the votes to each faction based on the (int) cast */
            var votes = new int[Enum.GetValues(typeof(Faction)).Length];
            
            foreach(var dist in _districts)
            {
                votes[(int)dist.Faction]++;
            }
            // find maximum index
            var index = 0;
            var max = votes[0];

            for (var i = 1; i < votes.Length; i++)
            {
                if (votes[i] <= max) continue;
                
                max = votes[i];
                index = i;
            }

            return (Faction) index;
        }

        private bool CanAddToCounty(District district)
        {
            // TODO: add 
            return false;
        }
    }
}
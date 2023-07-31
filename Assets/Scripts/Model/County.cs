using System.Collections.Generic;
using JetBrains.Annotations;
using Manager;

namespace Model
{
    /// <summary>
    /// The middle tier of the voting system.
    /// It consists of multiple, at least 2 <see cref="District">Districts</see>.
    /// Multiple counties form a <see cref="State"/>.
    /// Counties are managed by <see cref="CountyManager"/>
    /// </summary>
    public class County
    {
        private static int _instanceCounter;
        
        [NotNull] [ItemNotNull]
        public List<District> Districts { get; } = new();

        public Faction Winning { get; internal set; } = Faction.Neutral; 

        public int Size => Districts.Count;

        public readonly int Id;
        
        public County()
        {
            Id = _instanceCounter;
            _instanceCounter++;
        }
    }
}
using System.Collections.Generic;
using JetBrains.Annotations;
using Manager;

namespace Model
{
    /// <summary>
    /// A state is the big part of the model.
    /// States consist of multiple <see cref="County">Counties</see>.
    /// Managed by a <see cref="StateManager"/>
    /// </summary>
    public class State
    {
        [NotNull] [ItemNotNull]
        public List<County> Counties { get; } = new();
        
        public Faction Winning { get; internal set; } = Faction.Neutral; 

        public int Size => Counties.Count;

        public readonly int Id;

        public State(int id)
        {
            Id = id;
        }
    }
}
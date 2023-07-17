namespace Model
{
    /// <summary>
    /// smallest part in the voting system
    /// multiple districts make up a county
    /// it's the tile you click on
    /// </summary>
    public class District: GerrymanderingTile
    {
        public Faction Faction { get; set; } 

        public bool Marked { get; set; }
    }
}
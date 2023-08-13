using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

/// <summary>
/// Stores all the necessary data for a level
/// atm just the winning faction
/// </summary>
public class LevelData : MonoBehaviour
{
    [SerializeField] [Tooltip("The winning faction of the level")]
    private Faction _winning = Faction.Neutral;

    public Faction Winning => _winning;
}

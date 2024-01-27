using System.Collections.Generic;
using UnityEngine;

public class LevelBonuses : MonoBehaviour
{
    [SerializeField] private List<Bonus> _bonuses;

    public List<Bonus> Bonuses => _bonuses;
}
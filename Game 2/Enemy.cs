using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public string nameE { get; private set; }
    [field: SerializeField] public string classE { get; private set; }
    [field: SerializeField] public string raceE { get; private set; }
    [field: SerializeField] public int[] hpMax { get; private set; }
    [field: SerializeField] public int[] armor { get; private set; }
    [field: SerializeField] public int[] bonus { get; private set; }
    [field: SerializeField] public int diceCount { get; private set; }
    [field: SerializeField] public int[] diceDamage { get; private set; }
    [field: SerializeField] public Sprite spireEnemy { get; private set; }
}

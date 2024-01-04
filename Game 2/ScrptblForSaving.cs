using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrForSaving", menuName = "Scriptable/ScrForSaving")]
public class ScrptblForSaving : ScriptableObject
{
    [field: SerializeField] public bool autoAttack { get; private set; }

    #region Setters
    public void SetAutoAttackBool(bool aAttack)
    {
        autoAttack = aAttack;
    }
    #endregion
}

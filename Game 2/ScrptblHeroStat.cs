using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrHeroStat", menuName = "Scriptable/ScrHeroStat")]
public class ScrptblHeroStat : ScriptableObject
{
    //initiative = 1d20 + dexterityBonus; spabrosok = 1d20 + statBonus; Perception = 10 + wisdomBonus
    [field: SerializeField, Header("Переменные описания")] public string nameH { get; private set; }
    [field: SerializeField] public string raceH { get; private set; }
    [field: SerializeField] public string classH { get; private set; }
    [field: SerializeField] public string heightH { get; private set; }
    [field: SerializeField, Header("Переменные тнформации")] public int level { get; private set; }
    [field: SerializeField] public int experience { get; private set; }
    [field: SerializeField] public int skillBonus { get; private set; }
    [field: SerializeField] public int passivePerception { get; private set; }
    [field: SerializeField] public int hpMax { get; private set; }
    [field: SerializeField] public int hpCur { get; private set; }
    [field: SerializeField] public int armor { get; private set; } // armor class = armor + dexterityBonus + bonus armor + bonus shield
    [field: SerializeField] public int diceClass { get; private set; } // d4, d6, d8, d10, d12, d20
    [field: SerializeField] public int strength { get; private set; } // bonus = (strength - 10) / 2
    [field: SerializeField] public int strengthBonus { get; private set; }
    [field: SerializeField] public int dexterity { get; private set; }
    [field: SerializeField] public int dexterityBonus { get; private set; }
    [field: SerializeField] public int intelligence { get; private set; }
    [field: SerializeField] public int intelligenceBonus { get; private set; }
    [field: SerializeField] public int constitution { get; private set; } // hp for level = 1d'diceClass' + constitutionBonus
    [field: SerializeField] public int constitutionBonus { get; private set; }
    [field: SerializeField] public int wisdom { get; private set; }
    [field: SerializeField] public int wisdomBonus { get; private set; }
    [field: SerializeField] public int charisma { get; private set; }
    [field: SerializeField] public int charismaBonus { get; private set; }

    #region Setters hero
    public void SetHeroName(string name)
    {
        nameH = name;
    }

    public void SetHeroRace(string race)
    {
        raceH = race;
    }

    public void SetHeroClass(string clas)
    {
        classH = clas;
    }

    public void SetHeroHeight(string height)
    {
        heightH = height;
    }
    #endregion

    #region Setters info
    public void AddHeroLevel(int lvl)
    {
        level += lvl;
        skillBonus = Calculations.SkillBonus(level);
    }

    public void AddHeroExperience(int ex)
    {
        experience += ex;
    }

    public void AddHeroMaxHP(int mHP)
    {
        hpMax += mHP;

        if (hpMax < 1)
            hpMax = 1;

        if (hpCur > hpMax)
            hpCur = hpMax;
    }

    public void AddHeroCurHP(int cHP)
    {
        hpCur += cHP;

        if (hpCur > hpMax)
            hpCur = hpMax;
        else if (hpCur < 0)
            hpCur = 0;
    }

    public void SetHeroDiceClass(int dice)
    {
        diceClass += dice;
    }
    #endregion

    #region Setters stats
    public void AddHeroStrength(int str)
    {
        strength += str;
        strengthBonus = Calculations.CharacteristicModifier(strength);
    }

    public void AddHeroDexterity(int dex)
    {
        dexterity += dex;
        dexterityBonus = Calculations.CharacteristicModifier(dexterity);
        armor = 10 + Calculations.CharacteristicModifier(dexterity);
    }

    public void AddHeroIntelligence(int inte)
    {
        intelligence += inte;
        intelligenceBonus = Calculations.CharacteristicModifier(intelligence);
    }

    public void AddHeroConstitution(int cons)
    {
        constitution += cons;
        constitutionBonus = Calculations.CharacteristicModifier(constitution);
    }

    public void AddHeroWisdom(int wsd)
    {
        wisdom += wsd;
        wisdomBonus = Calculations.CharacteristicModifier(wisdom);
        passivePerception = 10 + Calculations.CharacteristicModifier(wsd);
    }

    public void AddHeroCharisma(int cha)
    {
        charisma += cha;
        charismaBonus = Calculations.CharacteristicModifier(charisma);
    }
    #endregion

    public void SetParametersWithMoreDependence(int lvl, int str, int dex, int inte, int cons, int wsd, int cha)
    {
        AddHeroLevel(lvl);
        AddHeroStrength(str);
        AddHeroDexterity(dex);
        AddHeroIntelligence(inte);
        AddHeroConstitution(cons);
        AddHeroWisdom(wsd);
        AddHeroCharisma(cha);
    }
}

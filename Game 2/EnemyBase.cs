using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public string nameEnemy, classEnemy, raceEnemy;

    public int hpEnemyMax, hpEnemyCur, armorEnemy, bonusEnemy, diceCountEnemy, diceDamageEnemy, initiativeEnemy;

    public Sprite sprEnemy;

    public EnemyBase(GameObject eneyObj, int difficulty)
    {
        Enemy enemy = eneyObj.GetComponent<Enemy>();
        float difMultiplier = Calculations.Difficulty(difficulty);

        nameEnemy = enemy.nameE;
        classEnemy = enemy.classE;
        raceEnemy = enemy.raceE;
        hpEnemyMax = (int)(Random.Range(enemy.hpMax[0], enemy.hpMax[1]) * difMultiplier);
        hpEnemyCur = hpEnemyMax;
        armorEnemy = Random.Range(enemy.armor[0], enemy.armor[1]);
        bonusEnemy = (int)(Random.Range(enemy.bonus[0], enemy.bonus[1]) * difMultiplier);
        diceCountEnemy = enemy.diceCount;
        diceDamageEnemy = (int)(Random.Range(enemy.hpMax[0], enemy.hpMax[1]) * difMultiplier);
        sprEnemy = enemy.spireEnemy;
    }

    public void SetInitiativeEnemy()
    {
        initiativeEnemy = Calculations.Initiative(bonusEnemy);
    }
}

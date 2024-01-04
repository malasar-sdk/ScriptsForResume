using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private float delayBattle;

    [SerializeField]
    private int curActiveCharacter, numEnemys;

    [SerializeField]
    private List<int> orderOfMoves;

    [SerializeField]
    private List<EnemyBase> enemyList;

    [SerializeField, Header("—сылки")]
    private ScrptblHeroStat scrptblHeroStat;

    [SerializeField]
    private ScrptblForSaving scrptblForSaving;

    [SerializeField]
    private ScrptblEnemysList scrptblEnemysList;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Armor armor;

    [SerializeField, Header("—сылки текст")]
    private TMP_Text txtNameHero;

    [SerializeField]
    private TMP_Text txtNameEnemy, txtInitHero, txtInitEnemy, txtHpHero, txtHpEnemy, txtDiceAttackHero, txtDiceAttackEnemy, txtDiceDamageHero, txtDiceDamageEnemy, txtBattleLog;

    [SerializeField]
    private GameObject missedObj;

    private void Start()
    {
        scrptblHeroStat.SetParametersWithMoreDependence(0, 0, 0, 0, 0, 0, 0); //???
        curActiveCharacter = 0;
        missedObj.SetActive(false);
        SetBattleStartSettings(1);
    }

    public void Battle()
    {
        missedObj.SetActive(false);

        if (orderOfMoves[curActiveCharacter] == 0)
        {
            int attackHero = Calculations.Attack(scrptblHeroStat.skillBonus, scrptblHeroStat.strengthBonus);
            int armorEnemy = Calculations.ArmorBonus(enemyList[0].armorEnemy, 0, 0);
            Debug.Log($"Attack Player - {attackHero}, armor Enemy - {armorEnemy}");
            txtBattleLog.text += $"\nAttack Player - {attackHero}, armor Enemy - {armorEnemy}";

            if (attackHero > armorEnemy)
            {
                int damageHero = Calculations.Damage(weapon.GetDamage(), scrptblHeroStat.strengthBonus);
                Debug.Log($"Damage Player - {damageHero}");
                txtBattleLog.text += $"\nAttack Player - {damageHero}";

                enemyList[0].hpEnemyCur -= damageHero;
                txtHpEnemy.text = $"{enemyList[0].hpEnemyCur} / {enemyList[0].hpEnemyMax}";
            }
            else
            {
                Debug.Log($"Player missed...");
                missedObj.SetActive(true);
                missedObj.GetComponent<TMP_Text>().text = "Player missed";
            }
        }
        else
        {
            int attackEnemy = Calculations.Attack(enemyList[0].bonusEnemy, 0);
            int armorPlayer = Calculations.ArmorBonus(scrptblHeroStat.armor, 5, 0);
            Debug.Log($"Attack Enemy - {attackEnemy}, armor Player - {armorPlayer}");
            txtBattleLog.text += $"\nAttack Enemy - {attackEnemy}, armor Player - {armorPlayer}";

            if (attackEnemy > armorPlayer)
            {
                int num = Calculations.DiceThrow(enemyList[0].diceCountEnemy, enemyList[0].diceDamageEnemy);
                int damageEnemy = Calculations.Damage(num, enemyList[0].bonusEnemy);
                Debug.Log($"Damage Enemy - {damageEnemy}");
                txtBattleLog.text += $"\nAttack Enemy - {damageEnemy}";

                scrptblHeroStat.AddHeroCurHP(-damageEnemy);
                txtHpHero.text = $"{scrptblHeroStat.hpCur} / {scrptblHeroStat.hpMax}";
            }
            else
            {
                Debug.Log($"Enemy missed...");
                missedObj.SetActive(true);
                missedObj.GetComponent<TMP_Text>().text = "Enemy missed";
            }

        }

        curActiveCharacter++;
        if (curActiveCharacter >= orderOfMoves.Count)
            curActiveCharacter = 0;

        Debug.Log($"Round score ==== HP Player: {scrptblHeroStat.hpCur} / {scrptblHeroStat.hpMax} ---- HP Enemy: {enemyList[0].hpEnemyCur} / {enemyList[0].hpEnemyMax}");
        Debug.Log("--------------------------\n-------------------------");
        txtBattleLog.text += $"\nRound score ==== HP Player: {scrptblHeroStat.hpCur} / {scrptblHeroStat.hpMax} ---- HP Enemy: {enemyList[0].hpEnemyCur} / {enemyList[0].hpEnemyMax}";
    }

    public void SetBattleStartSettings(int numE)
    {
        numEnemys = numE;

        GenerateEnemys();
        CheckInitiative();
    }

    public void TurnOnOffAutoAttack()
    {
        bool isAuto = scrptblForSaving.autoAttack;

        if (!isAuto)
        {
            InvokeRepeating("Battle", delayBattle, delayBattle);
            scrptblForSaving.SetAutoAttackBool(true);
        }
        else
        {
            CancelInvoke("Battle");
            scrptblForSaving.SetAutoAttackBool(false);
        }
    }

    private void GenerateEnemys()
    {
        for (int i = 0; i < numEnemys; i++)
        {
            enemyList = new List<EnemyBase>();
            GameObject obj = scrptblEnemysList.enemysFromLocation1[Random.Range(0, scrptblEnemysList.enemysFromLocation1.Count)];
            EnemyBase enemyBase = new EnemyBase(obj, 1);
            enemyList.Add(enemyBase);
        }
    }

    private void CheckInitiative()
    {
        orderOfMoves = new List<int>();
        curActiveCharacter = 0;

        int initPlayer = Calculations.Initiative(scrptblHeroStat.dexterityBonus);
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetInitiativeEnemy();
        }

        orderOfMoves = new List<int>() {0, 1};

        if (initPlayer < enemyList[0].initiativeEnemy)
            orderOfMoves.Reverse();

        Debug.Log($"Initiative Player - {initPlayer}, Initiative Enemy - {enemyList[0].initiativeEnemy}");
        txtInitHero.text = $"{initPlayer}";
        txtInitEnemy.text = $"{enemyList[0].initiativeEnemy}";
    }
}

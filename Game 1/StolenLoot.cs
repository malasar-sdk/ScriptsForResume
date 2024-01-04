using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Globalization;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StolenLoot : MonoBehaviour
{
    [SerializeField] private GameObject loot_1, loot_2, loot_3, loot_4;
    [SerializeField] private TextMeshProUGUI textCounterL1;
    private int counterL1;
    [SerializeField] private int cdTimeL1, additionalGems;

    [SerializeField]
    private DateTime timeSaved, timeCurrent;

    [SerializeField]
    private int delayTime, curDelta;

    [SerializeField]
    private bool isButtonReadyBoots = false;

    private bool isStarted = false;
    private DateTime pauseDateTime;
    private PanelInfo panelInfo;

    [SerializeField]
    private Button btnBoots;

    [SerializeField]
    private ActiveHero activeHero;

    [SerializeField]
    private ScriptableTimer scriptableTimer;

    [SerializeField]
    private TimerOnObject timerOnObject;

    private void Start()
    {
        isButtonReadyBoots = false;
        textCounterL1.gameObject.SetActive(true);
        btnBoots.interactable = false;

        panelInfo = FindObjectOfType<PanelInfo>();
        if (PlayerPrefs.GetString("isLoot1") == "open")
        {
            loot_1.SetActive(true);
            PlayerPrefs.SetString("isLoot1", "open");
            loot_1.transform.localScale = new Vector3(1,1,1);
        }

        if (PlayerPrefs.GetString("isLoot1") == "first")
        {
            loot_1.SetActive(true);
            loot_1.transform.DOScale(1, 1);
            PlayerPrefs.SetString("isLoot1", "open");
        }

        //timerOnObject = FindObjectOfType<TimerOnObject>();
    }

    private void Update()
    {
        TimeBootsChecker();
    }

    public void Loot1()
    {
        if (isButtonReadyBoots == true)
        {
            timerOnObject.SaveLastBootTime();
            //scriptableTimer.SaveLastBootTime();

            activeHero.gems += additionalGems;
            panelInfo.ChangeCountGold(activeHero.gems);

            textCounterL1.gameObject.SetActive(true);

            isButtonReadyBoots = false;
            btnBoots.interactable = false;
        }
    }
    
    void OnApplicationPause(bool isPaused)
    {
        if (isStarted)
        {
            if (isPaused)
            {
                pauseDateTime = DateTime.Now;
            }
            else
            {
                counterL1 += (int)(DateTime.Now - pauseDateTime).TotalSeconds;
            }
        }
    }

    private void TimeBootsChecker()
    {
        if (isButtonReadyBoots == false)
        {
            int delta = timerOnObject.GetBootDeltaTime();
            //int delta = scriptableTimer.GetBootDeltaTime();

            curDelta = delayTime - delta;
            textCounterL1.text = $"{curDelta}";

            if (curDelta <= 0)
            {
                isButtonReadyBoots = true;
                btnBoots.interactable = true;

                textCounterL1.gameObject.SetActive(false);
            }
        }
    }
}

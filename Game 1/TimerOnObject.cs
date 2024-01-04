using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerOnObject : MonoBehaviour
{
    [SerializeField]
    private int timePassedFromLastSave, passedHour, passedMinuts, passedSecconds, curDeltaOfBoots, curDeltaBtnShopCoins, curDeltaBtnShopGems;

    [SerializeField]
    private DateTime savedTimeLastEnter, savedTimeLastBoots, savedTimeLastBtnShopCoins, savedTimeLastBtnShopGems;

    public static TimerOnObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
    }
    private void Start()
    {
        Debug.Log("Timer start");
    }

    public void SaveLastEnterTime()
    {
        savedTimeLastEnter = DateTime.Now;
        DateTimeChecker.SetDateTime("LastEnter", savedTimeLastEnter);

        Debug.Log($"Saved last enter time - {savedTimeLastEnter}");
    }

    public void GetEnterDeltaTime()
    {
        savedTimeLastEnter = DateTimeChecker.GetDateTime("LastEnter", savedTimeLastEnter);

        TimeSpan delta = savedTimeLastEnter - DateTime.Now;
        timePassedFromLastSave = delta.Seconds;

        passedHour = TimeSpan.FromSeconds(timePassedFromLastSave).Hours;
        passedMinuts = TimeSpan.FromSeconds(timePassedFromLastSave).Minutes;
        passedSecconds = TimeSpan.FromSeconds(timePassedFromLastSave).Seconds;

        Debug.Log($"Game data loaded! Time passed - {timePassedFromLastSave} sec");
        Debug.Log($"Game data loaded! Time passed - {passedHour}:{passedMinuts}:{passedSecconds}");
    }

    public void SaveLastBootTime()
    {
        savedTimeLastBoots = DateTime.Now;
        DateTimeChecker.SetDateTime("LastBoot", savedTimeLastBoots);

        Debug.Log($"Saved last boots time - {savedTimeLastBoots}");
    }

    public int GetBootDeltaTime()
    {
        savedTimeLastBoots = DateTimeChecker.GetDateTime("LastBoot", savedTimeLastBoots);
        int deltaBootTime;

        TimeSpan delta = DateTime.Now - savedTimeLastBoots;
        deltaBootTime = delta.Seconds;
        deltaBootTime = Mathf.Clamp(deltaBootTime, 0, 7 * 24 * 60 * 60);
        curDeltaOfBoots = deltaBootTime;

        return deltaBootTime;
    }

    public void SaveLastButtonShopTime(int num)
    {
        switch (num)
        {
            case 0:
                savedTimeLastBtnShopCoins = DateTime.Now;
                DateTimeChecker.SetDateTime("LastBtnShopCoins", savedTimeLastBtnShopCoins);

                Debug.Log($"Saved last button shop Coins time - {savedTimeLastBtnShopCoins}");
                break;
            case 1:
                savedTimeLastBtnShopGems = DateTime.Now;
                DateTimeChecker.SetDateTime("LastBtnShopGems", savedTimeLastBtnShopGems);

                Debug.Log($"Saved last button shop Gems time - {savedTimeLastBtnShopGems}");
                break;
        }
    }

    public int GetButtonShopDeltaTimeCoins()
    {
        savedTimeLastBtnShopCoins = DateTimeChecker.GetDateTime("LastBtnShopCoins", savedTimeLastBtnShopCoins);
        int deltaBtnShopTimeCoins;

        TimeSpan delta0 = DateTime.Now - savedTimeLastBtnShopCoins;
        deltaBtnShopTimeCoins = delta0.Seconds;
        deltaBtnShopTimeCoins = Mathf.Clamp(deltaBtnShopTimeCoins, 0, 7 * 24 * 60 * 60);
        curDeltaBtnShopCoins = deltaBtnShopTimeCoins;

        return deltaBtnShopTimeCoins;
    }

    public int GetButtonShopDeltaTimeGems()
    {
        savedTimeLastBtnShopGems = DateTimeChecker.GetDateTime("LastBtnShopGems", savedTimeLastBtnShopGems);
        int deltaBtnShopTimeGems;

        TimeSpan delta1 = DateTime.Now - savedTimeLastBtnShopGems;
        deltaBtnShopTimeGems = delta1.Seconds;
        deltaBtnShopTimeGems = Mathf.Clamp(deltaBtnShopTimeGems, 0, 7 * 24 * 60 * 60);
        curDeltaBtnShopGems = deltaBtnShopTimeGems;

        return deltaBtnShopTimeGems;
    }

    private void OnDestroy()
    {
        SaveLastEnterTime();
    }
}

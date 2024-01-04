using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sound;

public class LevelUpgrade : MonoBehaviour
{
    [Header("Не изменяемые значения")]
    [SerializeField]
    private int priceToByLevelCoins;

    [SerializeField]
    private int priceToByLevelGems, curHeroLevel;

    [SerializeField]
    private GameObject cardPlayer;


    [Header("Изменяемые значения")]
    [SerializeField]
    private int priceStepCoins;

    [SerializeField]
    private int priceStepGems, healthStep, cooldawnStep, numMaxLevel;


    [Header("Ссылки")]

    [SerializeField]
    private TMP_Text txtHeroLevel;

    [SerializeField]
    private TMP_Text txtBuyLevelPrice, txtLevelNow, txtLevelNext, txtCurrentHp, txtNextHp, txtCurrentCd, txtNextCd;

    [SerializeField]
    private GameObject pnlBuyLevel, btnOpenLevelUp, txtCantBuyLevel, imgGems, imgCoins;

    [SerializeField]
    private ActiveHero activeHero;

    [SerializeField]
    private SaverLoader saverLoader;

    [SerializeField]
    private PanelInfo panelInfo;

    private bool isGems;

    private void Start()
    {
        saverLoader = FindObjectOfType<SaverLoader>();
        pnlBuyLevel.SetActive(false);
        txtCantBuyLevel.SetActive(false);
        imgGems.SetActive(false);
        imgCoins.SetActive(false);
    }

    public void LevelUpHero()
    {
        SoundManager.sound.playSound(SoundManager.sound.btnNiceUI);
        priceToByLevelCoins = priceStepCoins * activeHero.heroLevel;
        priceToByLevelGems = priceStepGems * activeHero.heroLevel;
        cardPlayer = activeHero.curHeroSwitched;
        curHeroLevel = activeHero.heroLevel;


        if (curHeroLevel != 3 && curHeroLevel != 6)
        {
            if (activeHero.coins >= priceToByLevelCoins)
            {
                activeHero.coins -= priceToByLevelCoins;
                panelInfo.SetMoneyToUI();

                curHeroLevel += 1;
                activeHero.heroLevel = curHeroLevel;
                cardPlayer.GetComponent<Card>().setCardLevel(curHeroLevel);

                cardPlayer.GetComponent<Player>().healthMax += healthStep;

                pnlBuyLevel.SetActive(false);
                setText();
                panelInfo.AnimForCoins(priceToByLevelCoins, true);

                CheckingMaxLevel();
            }
            else
            {
                panelInfo.AnimForCoins(priceToByLevelCoins, false);
                isGems = false;
                StartCoroutine(CrtOpenCloseDecline());
            }
        }
        else if (curHeroLevel == 3 || curHeroLevel == 6)
        {
            if (activeHero.gems >= priceToByLevelGems)
            {
                activeHero.gems -= priceToByLevelGems;
                panelInfo.SetMoneyToUI();

                curHeroLevel += 1;
                activeHero.heroLevel = curHeroLevel;
                cardPlayer.GetComponent<Card>().setCardLevel(curHeroLevel);

                //For cooldowns
                int newCd = cardPlayer.GetComponent<Player>().getHeroCooldown() - 1;
                cardPlayer.GetComponent<Player>().setHeroCooldown(newCd);

                pnlBuyLevel.SetActive(false);
                setText();
                panelInfo.AnimForGems(priceToByLevelGems, true);

                CheckingMaxLevel();
            }
            else
            {
                panelInfo.AnimForGems(priceToByLevelGems, false);
                isGems = true;
                StartCoroutine(CrtOpenCloseDecline());
            }
        }
        heroLvlText(curHeroLevel);
        //txtHeroLevel.text = curHeroLevel.ToString();
    }

    public void heroLvlText(int lvl)
    {
        switch (lvl)
        {
            case 1:
                txtHeroLevel.text = "I";
                break;
            case 2:
                txtHeroLevel.text = "II";
                break;
            case 3:
                txtHeroLevel.text = "III";
                break;
            case 4:
                txtHeroLevel.text = "IV";
                break;
            default:
                txtHeroLevel.text = "I";
                break;
        }
    }

    public void OpenPanekToBuyLevel()
    {
        SoundManager.sound.playSound(SoundManager.sound.btnNiceUI);
        pnlBuyLevel.SetActive(true);
        setText();
    }

    IEnumerator CrtOpenCloseDecline()
    {
        if (isGems)
        {
            txtCantBuyLevel.GetComponent<SetLocalVariable>().SetNestedStringEntry("nocoinstxt,MM_nogems_txt");
        }
        else
        {
            txtCantBuyLevel.GetComponent<SetLocalVariable>().SetNestedStringEntry("nocoinstxt,MM_nocoins_txt");
        }
        txtCantBuyLevel.SetActive(true);

        yield return new WaitForSeconds(2f);

        txtCantBuyLevel.SetActive(false);
    }

    public void CloseTabLevelBuy()
    {
        pnlBuyLevel.SetActive(false);
    }

    private void setText()
    {
        if (activeHero.heroLevel != 3 && activeHero.heroLevel != 6)
        {
            txtBuyLevelPrice.text = $"{priceStepCoins * activeHero.heroLevel}";
            imgCoins.SetActive(true);
            imgGems.SetActive(false);
            txtNextCd.text = $"{activeHero.curHeroSwitched.GetComponent<Player>().getHeroCooldown()}";
        }
        else
        {
            txtBuyLevelPrice.text = $"{priceStepGems * activeHero.heroLevel}";
            imgGems.SetActive(true);
            imgCoins.SetActive(false);
            txtNextCd.text = $"{activeHero.curHeroSwitched.GetComponent<Player>().getHeroCooldown() - 1}";
        }

        txtCurrentHp.text = $"{activeHero.curHeroSwitched.GetComponent<Player>().healthMax}";
        txtNextHp.text = $"{activeHero.curHeroSwitched.GetComponent<Player>().healthMax + 1}";

        txtCurrentCd.text = $"{activeHero.curHeroSwitched.GetComponent<Player>().getHeroCooldown()}";

        txtLevelNow.text = $"{activeHero.heroLevel}";
        txtLevelNext.text = $"{activeHero.heroLevel + 1}";
    }

    private void CheckingMaxLevel()
    {
        if (curHeroLevel >= numMaxLevel)
        {
            btnOpenLevelUp.SetActive(false);
        }
    }
}

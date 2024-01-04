using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;

public class PanelInfo : MonoBehaviour
{
    private bool isOptionsOpened;

    [SerializeField]
    private TMP_Text txtGems, txtCash, UpTextCoins, UpTextGems;

    [SerializeField]
    private GameObject pnlOptions, bgCoins, bgGems;

    [SerializeField]
    private ActiveHero activeHero;

    [SerializeField]
    private GameObject btnStarterPack;

    private void Start()
    {
        pnlOptions.SetActive(false);
        SetMoneyToUI();

        UpTextCoins.gameObject.SetActive(false);
        UpTextGems.gameObject.SetActive(false);

        bgCoins.GetComponent<Image>().color = new Color(255, 255, 255);
        bgGems.GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public void ChangeCountGold(int count)
    {
        float numGold = count;

        if (numGold >= 1000)
        {
            numGold = numGold / 1000;
            txtGems.text = $"{numGold.ToString("0.0")} K";
        }
        else
        {
            txtGems.text = $"{numGold}";
        }
    }

    public void ChangeCountCash(int count)
    {
        float numGems = count;

        if (count >= 1000)
        {
            numGems = numGems / 1000;
            txtCash.text = $"{numGems.ToString("0.0")} K";
        }
        else
        {
            txtCash.text = $"{numGems}";
        }
    }

    public void OpenCloseOptionsTab()
    {
        if (isOptionsOpened == false)
        {
            pnlOptions.SetActive(true);
            isOptionsOpened = true;
        }
        else
        {
            pnlOptions.SetActive(false);
            isOptionsOpened = false;
        }
    }

    public void SetMoneyToUI()
    {
        float numGold = activeHero.coins;
        float numGems = activeHero.gems;

        if (activeHero.coins >= 1000)
        {
            numGold = numGold / 1000;
            txtCash.text = $"{numGold.ToString("0.0")} K";
        }
        else
        {
            txtCash.text = $"{numGold}";
        }

        if (activeHero.gems >= 1000)
        {
            numGems = numGems / 1000;
            txtGems.text = $"{numGems.ToString("0.0")} K";
        }
        else
        {
            txtGems.text = $"{numGems}";
        }
    }

    public void CloseStarterPack()
    {
        btnStarterPack.SetActive(false);
    }

    #region Анимации для денег
    public void AnimForCoins(int coins, bool isCanBought)
    {
        if (isCanBought == true)
        {
            UpTextCoins.gameObject.SetActive(true);
            UpTextCoins.text = $"-{coins}";

            StartCoroutine(CrtAnimCoins());
        }
        else
        {
            StartCoroutine(CrtChangeColorCoins());
        }
    }

    public void AnimForGems(int gems, bool isCanBought)
    {
        if (isCanBought == true)
        {
            UpTextGems.gameObject.SetActive(true);
            UpTextGems.text = $"-{gems}";

            StartCoroutine(CrtAnimGems());
        }
        else
        {
            StartCoroutine(CrtChangeColorGems());
        }
    }

    IEnumerator CrtAnimCoins()
    {
        yield return new WaitForSeconds(1f);
        UpTextCoins.gameObject.SetActive(false);
    }

    IEnumerator CrtAnimGems()
    {
        yield return new WaitForSeconds(1f);
        UpTextGems.gameObject.SetActive(false);
    }

    IEnumerator CrtChangeColorCoins()
    {
        bgCoins.GetComponent<Image>().color = new Color(255, 0, 0);
        yield return new WaitForSeconds(1f);
        bgCoins.GetComponent<Image>().color = new Color(255, 255, 255);
    }

    IEnumerator CrtChangeColorGems()
    {
        bgGems.GetComponent<Image>().color = new Color(255, 0, 0);
        yield return new WaitForSeconds(1f);
        bgGems.GetComponent<Image>().color = new Color(255, 255, 255);
    }
    #endregion
}

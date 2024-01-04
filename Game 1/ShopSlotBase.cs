using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotBase : MonoBehaviour
{
    [SerializeField]
    private Sprite imgToSetSlot_1, imgToSetSlot_2, imgToSetSlot_3;

    [SerializeField]
    private string txtNumSlot_1, txtNumSlot_2, txtNumSlot_3, txtPrice_1, txtPrice_2, txtPrice_3;

    [SerializeField]
    private int idSlot_1, idSlot_2, idSlot_3;

    [SerializeField]
    private Image image_1, image_2, image_3;

    [SerializeField]
    private TMP_Text num_1, num_2, num_3;

    [SerializeField]
    private TMP_Text btnTxtPrice_1, btnTxtPrice_2, btnTxtPrice_3;

    [SerializeField]
    private bool isGems;

    private void Start()
    {
        if (isGems)
        {
            txtPrice_1 = Purchaser.storeController.products.WithID("gems_low").metadata.localizedPriceString;
            txtPrice_2 = Purchaser.storeController.products.WithID("gems_medium").metadata.localizedPriceString;
            txtPrice_3 = Purchaser.storeController.products.WithID("gems_high").metadata.localizedPriceString;
        }
        else
        {
            txtPrice_1 = Purchaser.storeController.products.WithID("coins_low").metadata.localizedPriceString;
            txtPrice_2 = Purchaser.storeController.products.WithID("coins_medium").metadata.localizedPriceString;
            txtPrice_3 = Purchaser.storeController.products.WithID("coins_high").metadata.localizedPriceString;
        }

        SetSlotsInfo();
    }

    private void SetSlotsInfo()
    {
        if (imgToSetSlot_1 != null)
        {
            image_1.sprite = imgToSetSlot_1;
            num_1.text = txtNumSlot_1;
            
        }
        btnTxtPrice_1.text = txtPrice_1;

        if (imgToSetSlot_2 != null)
        {
            image_2.sprite = imgToSetSlot_2;
            num_2.text = txtNumSlot_2;
            
        }
        btnTxtPrice_2.text = txtPrice_2;

        if (imgToSetSlot_3 != null)
        {
            image_3.sprite = imgToSetSlot_3;
            num_3.text = txtNumSlot_3;
            
        }
        btnTxtPrice_3.text = txtPrice_3;
    }
}

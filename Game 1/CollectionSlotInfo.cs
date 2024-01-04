using UnityEngine;
using UnityEngine.UI;
using Sound;


public class CollectionSlotInfo : MonoBehaviour
{
    [SerializeField]
    private string cardObjName, cardName, cardDescription, cardStrenght, lootCoolDown;

    [SerializeField]
    private Sprite cardSprite, cardBG, defaultBG;

    [SerializeField]
    private bool isOpened;

    [SerializeField]
    private Image imgCardVision, imgBG;

    [SerializeField]
    private GameObject objDescr, spawnerDescr;

    [SerializeField]
    private OpenDescriptionInCollection openDescriptionInCollection;

       
    private bool isZeroDisable;
    private SoundManager sound;

    private void Awake()
    {
        objDescr = GameObject.FindGameObjectWithTag("CollectionDescription");
        sound = FindObjectOfType<SoundManager>();

        openDescriptionInCollection = objDescr.GetComponent<OpenDescriptionInCollection>();
    }

    public void LoadCardInfo(GameObject obj)
    {
        cardName = obj.GetComponent<Card>().getCardName();
        cardDescription = obj.GetComponent<Card>().getCardDescription();
        
        var name = obj.name;
        cardObjName = name;
        
        
        cardStrenght = obj.GetComponent<Card>().getCardStrenght();
        cardSprite = obj.GetComponent<Card>().getCardImage();
        isOpened = obj.GetComponent<Card>().getCardBoolDiscovery();
        cardBG = obj.GetComponent<Card>().getCardImageBG();
        isZeroDisable = obj.GetComponent<Card>().getIsZero();
        imgBG.sprite = defaultBG;

       

        if(obj.GetComponent<loot>() != null)
        {
            lootCoolDown = "cd " + obj.GetComponent<loot>().getLootCooldown().ToString();
        }
        else
        {
            lootCoolDown = "";
        }

        if (isOpened == true)
        {
            Color color = new Color(225, 225, 225, 1);
            imgCardVision.color = color;
            imgBG.sprite = cardBG;
            imgCardVision.sprite = cardSprite;
        }
        else
        {
            imgCardVision.sprite = cardSprite;
            Color color = new Color(0, 0, 0, 1);
            imgCardVision.color = color;
        }
    }

    public void SetCardActive()
    {
        isOpened = true;
        Color color = new Color(225, 225, 225, 1);
        imgCardVision.color = color;
        imgBG.sprite = cardBG;
        imgCardVision.sprite = cardSprite;
    }

    public void OpenDescription()
    {
        if (isOpened == true)
        {
            sound.playSound(sound.btnPaperUI);
            openDescriptionInCollection.OpenDescription(cardObjName, cardName, cardDescription, cardSprite, cardStrenght, isZeroDisable, lootCoolDown);
            //Debug.Log("card loot " + lootCoolDown);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private int abilitySlotIndex;

    [SerializeField]
    private GameObject curAbilityCard, abilitySlot;

    [SerializeField]
    private Image imgAbility;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private ActiveHero activeHero;

    [SerializeField]
    private SaverLoader saverLoader;

    private Canvas mainCanvas;

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0);
    }

    private void Start()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        saverLoader = FindObjectOfType<SaverLoader>();

        FindThisSlot();
    }

    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).tapCount == 2)
            {
                ReturnToInventory();
            }
        }
    }

    public GameObject GetAbilityCard()
    {
        return curAbilityCard;
    }

    public void SetAbilityCard(GameObject card)
    {
        if (card != null)
        {
            curAbilityCard = card;
            imgAbility.sprite = curAbilityCard.GetComponent<Card>().getCardImage();

            this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        }
        else
        {
            curAbilityCard = null;
            imgAbility.sprite = null;

            this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0);
        }

        switch (abilitySlotIndex)
        {
            case 1:
                activeHero.abilityCard1 = curAbilityCard;
                break;

            case 2:
                activeHero.abilityCard2 = curAbilityCard;
                break;

            case 3:
                activeHero.abilityCard3 = curAbilityCard;
                break;
        }
    }

    public int GetAbilitySlotIndex() 
    { 
        return abilitySlotIndex; 
    }

    private void ReturnToInventory()
    {
        if (curAbilityCard != null)
        {
            playerInventory.AddItemToInventory(curAbilityCard);
            curAbilityCard = null;
            imgAbility.sprite= null;

            this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(mainCanvas.gameObject.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(abilitySlot.transform);
        transform.localPosition = Vector3.zero;

        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag.gameObject;

        if (item.GetComponent<AbilitySlot>())
        {
            GameObject cardTaken = item.GetComponent<AbilitySlot>().GetAbilityCard();

            if (curAbilityCard != null)
            {
                if (cardTaken != null)
                {
                    item.GetComponent<AbilitySlot>().SetAbilityCard(curAbilityCard);
                    SetAbilityCard(cardTaken);

                    imgAbility.sprite = curAbilityCard.GetComponent<Card>().getCardImage();
                }
            }
            else
            {
                if (cardTaken != null)
                {
                    item.GetComponent<AbilitySlot>().SetAbilityCard(null);
                    SetAbilityCard(cardTaken);

                    imgAbility.sprite = curAbilityCard.GetComponent<Card>().getCardImage();
                }
            }

            switch (abilitySlotIndex)
            {
                case 1:
                    activeHero.abilityCard1 = curAbilityCard;
                    break;

                case 2:
                    activeHero.abilityCard2 = curAbilityCard;
                    break;

                case 3:
                    activeHero.abilityCard3 = curAbilityCard;
                    break;
            }
        }
        else
        {
            GameObject slotTaken = item.GetComponent<ImageSlotMoving>().GetSlotObject();
            GameObject cardTaken = slotTaken.GetComponent<SLotInfo>().GetSlotObj();

            if (curAbilityCard == null)
            {
                SetAbilityCard(cardTaken);

                playerInventory.DeleteItemFromInventory(slotTaken);
                slotTaken.GetComponent<SLotInfo>().DestroyThisObject();
                playerInventory.SetInventorySlot(1);
            }
            else
            {
                playerInventory.AddToScriptableInventory(curAbilityCard);
                playerInventory.DeleteItemFromInventory(slotTaken);

                slotTaken.GetComponent<SLotInfo>().SetInfo(curAbilityCard);
                curAbilityCard = cardTaken;
            }

            imgAbility.sprite = curAbilityCard.GetComponent<Card>().getCardImage();

            switch (abilitySlotIndex)
            {
                case 1:
                    activeHero.abilityCard1 = curAbilityCard;
                    break;

                case 2:
                    activeHero.abilityCard2 = curAbilityCard;
                    break;

                case 3:
                    activeHero.abilityCard3 = curAbilityCard;
                    break;
            }
        }

        saverLoader.SaveGame();
    }

    public void FindThisSlot()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        switch (abilitySlotIndex)
        {
            case 1:
                if (activeHero.abilityCard1 != null)
                {
                    curAbilityCard = activeHero.abilityCard1;
                }

                break;

            case 2:
                if (activeHero.abilityCard2 != null)
                {
                    curAbilityCard = activeHero.abilityCard2;
                }

                break;

            case 3:
                if (activeHero.abilityCard3 != null)
                {
                    curAbilityCard = activeHero.abilityCard3;
                }

                break;

            case 4:
                if (activeHero.abilityCard4 != null)
                {
                    imgAbility.sprite = activeHero.abilityCard4;
                    this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                }

                break;
        }

        if (curAbilityCard != null)
        {
            imgAbility.sprite = curAbilityCard.GetComponent<Card>().getCardImage();
            this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        }
    }
}

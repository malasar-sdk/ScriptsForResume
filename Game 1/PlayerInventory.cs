using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int curReservedSlots, maxSlots;

    [SerializeField]
    private GameObject slotObj, inventoryObj;

    [SerializeField]
    private ScriptableInventory inventoryScriptable;

    [SerializeField]
    private Sprite testSprite;

    [SerializeField]
    private GameObject pnlSlidInventory, btnOpenInventory;

    [SerializeField]
    private float yPosUp, yPosDown, invMoveDuratiob;

    [SerializeField]
    private List<GameObject> inventorySlots;

    [SerializeField]
    private List<GameObject> allDropCards;

    [SerializeField]
    private SaverLoader saverLoader;

    [Header("Open Inventory")]
    [SerializeField]
    private Button openInventoryBtn;
    [SerializeField]
    private GameObject openInventoryBg;

    private GameObject cardForUpgrading;
    private bool isSkideInventoryOpen;

    private void Awake()
    {
        //inventoryScriptable = Resources.Load<ScriptableInventory>("Prefabs/UIObjects/Inventory");
        allDropCards.AddRange(Resources.LoadAll<GameObject>("Prefabs/Loot Cards"));
    }

    private void Start()
    {
        curReservedSlots = inventoryScriptable.inventory.Count;

        if (curReservedSlots > maxSlots)
        {
            while (curReservedSlots % 5 != 0)
            {
                curReservedSlots++;
            }

            maxSlots = curReservedSlots;
        }

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject obj = Instantiate(slotObj, inventoryObj.transform);

            inventorySlots.Add(obj);
        }

        inventoryScriptable.RebuildInventory();
        FillSlotsFromInventory();
        
    }

    public void AddItemToInventory(GameObject objCard)
    {
        if (curReservedSlots < maxSlots)
        {
            inventorySlots[curReservedSlots + 1].GetComponent<SLotInfo>().SetInfo(objCard);

            inventoryScriptable.inventory.Add(objCard);
        }
        else
        {
            SetInventorySlot(5);

            inventorySlots[curReservedSlots + 1].GetComponent<SLotInfo>().SetInfo(objCard);

            inventoryScriptable.inventory.Add(objCard);
        }

        curReservedSlots = inventoryScriptable.inventory.Count;

        //inventoryScriptable.RebuildInventory();
        //FillSlotsFromInventory();
    }

    public void DeleteItemFromInventory(GameObject objSlot)
    {
        for (int i = 0; i < inventoryScriptable.inventory.Count; i++)
        {
            if (inventoryScriptable.inventory[i] != null)
            {
                if (inventoryScriptable.inventory[i].GetComponent<Card>().getCardName() != objSlot.GetComponent<SLotInfo>().GetNameCard())
                {
                    continue;
                }
                else
                {
                    inventoryScriptable.inventory.RemoveAt(i);

                    break;
                }
            }
        }

        curReservedSlots = inventoryScriptable.inventory.Count;

        //inventoryScriptable.RebuildInventory();
        //FillSlotsFromInventory();
    }

    public void AddToScriptableInventory(GameObject obj)
    {
        inventoryScriptable.inventory.Add(obj);
        curReservedSlots = inventoryScriptable.inventory.Count;

        //inventoryScriptable.RebuildInventory();
        //FillSlotsFromInventory();
    }

    public void DeleteFromScriptableInventory(GameObject obj)
    {
        for (int i = 0; i < inventoryScriptable.inventory.Count; i++)
        {
            if (inventoryScriptable.inventory[i] != null)
            {
                if (inventoryScriptable.inventory[i].GetComponent<Card>().getCardName() != obj.GetComponent<Card>().getCardName())
                {
                    continue;
                }
                else
                {
                    inventoryScriptable.inventory.RemoveAt(i);

                    break;
                }
            }
        }
    }

    public void SetInventorySlot(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = slotObj;
            inventorySlots.Add(obj);
            Instantiate(obj, inventoryObj.transform);
        }

        //inventoryScriptable.RebuildInventory();
        //FillSlotsFromInventory();
    }

    public void FillSlotsFromInventory()
    {
        for (int i = 0;  i < inventoryScriptable.inventory.Count; i++)
        {
            if (inventoryScriptable.inventory[i] != null)
            {
                inventorySlots[i].gameObject.GetComponent<SLotInfo>().SetInfo(inventoryScriptable.inventory[i]);
            }
        }
    }

    public GameObject FindCardToUpgrade(string tagCard, int lvlCard)
    {
        for (int i = 0; i < allDropCards.Count; i++)
        {
            string name = allDropCards[i].tag;
            int lvl = allDropCards[i].GetComponent<Card>().getCardLevel();

            if (name == tagCard && lvl == lvlCard + 1)
            {
                cardForUpgrading = allDropCards[i];

                return cardForUpgrading;
            }
            else
            {
                continue;
            }
        }

        return null;
    }

    public void OpenInventory()
    {
        SoundManager.sound.playSound(SoundManager.sound.btnUI);
        if (isSkideInventoryOpen == false)
        {
            openInventoryBtn.interactable = false;
            openInventoryBg.SetActive(true);
            pnlSlidInventory.transform.DOLocalMove(new Vector2(0, yPosUp), invMoveDuratiob);
            btnOpenInventory.gameObject.transform.localRotation =  Quaternion.Euler(0, 0, 180);

            isSkideInventoryOpen = true;
        }
        else
        {
            openInventoryBtn.interactable = true;
            openInventoryBg.SetActive(false);
            pnlSlidInventory.transform.DOLocalMove(new Vector2(0, yPosDown), invMoveDuratiob);
            btnOpenInventory.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            isSkideInventoryOpen = false;
        }   
    }

    public void SortInventory()
    {
        inventoryScriptable.RebuildInventory();
    }
}

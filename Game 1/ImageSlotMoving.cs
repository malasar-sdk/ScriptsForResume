using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageSlotMoving : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject slot, newCard;

    private Canvas mainCanvas;

    private void Start()
    {
        mainCanvas = GetComponentInParent<Canvas>();
    }

    public GameObject GetSlotObject()
    {
        return slot;
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
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;

        canvasGroup.blocksRaycasts = true;
    }
}

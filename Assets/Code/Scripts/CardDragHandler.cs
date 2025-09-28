using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;
    private List<RectTransform> draggedStack = new List<RectTransform>();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransform rt = eventData.pointerDrag.GetComponent<RectTransform>();
        if (!rt.GetComponent<CardView>().data.faceUp || rt.parent.name == "RemainingDeck")
        {
            eventData.pointerDrag = null;
            return;
        }
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        draggedStack.Clear();
        int index = transform.GetSiblingIndex();
        for (int i = index; i < transform.parent.childCount; i++)
        {
            draggedStack.Add(transform.parent.GetChild(i).GetComponent<RectTransform>());
        }
        foreach (var card in draggedStack)
        {
            card.SetAsLastSibling();
        }
        rt.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta / canvas.scaleFactor;
        for (int i = 0; i < draggedStack.Count; i++)
        {
            draggedStack[i].anchoredPosition += delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            for (int i = 0; i < draggedStack.Count; i++)
            {
                draggedStack[i].anchoredPosition = startPosition + new Vector3(0, -45f * i, 0);
            }
        }
        draggedStack.Clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            RectTransform rt = eventData.pointerDrag.GetComponent<RectTransform>();
            if (rt.parent.name == "RemainingDeck")
            {
                rt.GetComponent<CardView>().data.faceUp = true;
                rt.GetComponent<CardView>().UpdateView();
                rt.SetParent(GameObject.Find("Stock").transform, false);
                rt.anchoredPosition = new Vector2(0, 0);
            }
        }
    }
}
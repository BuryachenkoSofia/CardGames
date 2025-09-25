using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransform rt = eventData.pointerDrag.GetComponent<RectTransform>();
        if (rt.parent.GetChild(rt.parent.childCount - 1) != rt || rt.parent.name == "RemainingDeck")
        {
            eventData.pointerDrag = null;
            return;
        }
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
        rt.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == startParent) rectTransform.anchoredPosition = startPosition;
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
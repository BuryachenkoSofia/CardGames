using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Suit suit;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            if (dropped.GetComponent<CardView>().data.suit == suit)
            {
                dropped.GetComponent<RectTransform>().SetParent(this.transform, false);
                dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }
    }
}

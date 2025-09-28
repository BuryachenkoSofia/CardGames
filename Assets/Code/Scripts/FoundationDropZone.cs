using UnityEngine;
using UnityEngine.EventSystems;

public class FoundationDropZone : MonoBehaviour, IDropHandler
{
    public Suit suit;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;
        if (!CheckCard(dropped)) return;
        FindFirstObjectByType<Counter>().Add();
        RectTransform rt = dropped.GetComponent<RectTransform>();
        Transform oldParent = rt.parent;
        rt.SetParent(this.transform, false);
        rt.anchoredPosition = Vector2.zero;
        if (oldParent.childCount > 0 && oldParent.name != "Stock")
        {
            CardView lastCard = oldParent.GetChild(oldParent.childCount - 1).GetComponent<CardView>();
            lastCard.data.faceUp = true;
            lastCard.UpdateView();

            oldParent.GetChild(oldParent.childCount - 1).GetComponent<DropZone>().enabled = true;
        }
        else if (oldParent.name != "Stock")
        {
            oldParent.GetComponent<DropZone>().enabled = true;
        }
        dropped.GetComponent<DropZone>().enabled = false;
    }

    private bool CheckCard(GameObject card)
    {
        if (card.GetComponent<CardView>().data.suit != suit)
        {
            return false;
        }
        if (transform.childCount == 0)
        {
            return card.GetComponent<CardView>().data.rank == Rank.Ace;
        }
        return (int)card.GetComponent<CardView>().data.rank == (int)transform.GetChild(transform.childCount - 1).GetComponent<CardView>().data.rank + 1;
    }

}
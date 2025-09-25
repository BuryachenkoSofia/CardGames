using UnityEngine;
using UnityEngine.EventSystems;

public class FoundationDropZone : MonoBehaviour, IDropHandler
{
    public Suit suit;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            if (CheckCard(dropped))
            {
                RectTransform rt = dropped.GetComponent<RectTransform>();
                Transform oldParents = rt.parent;

                rt.SetParent(this.transform, false);
                rt.anchoredPosition = Vector2.zero;

                if (oldParents.childCount != 0 && oldParents.name != "Stock")
                {
                    oldParents.GetChild(oldParents.childCount - 1).GetComponent<CardView>().data.faceUp = true;
                    oldParents.GetChild(oldParents.childCount - 1).GetComponent<CardView>().UpdateView();
                    oldParents.GetChild(oldParents.childCount - 1).gameObject.AddComponent<DropZone>();
                }
                else
                {
                    oldParents.gameObject.AddComponent<DropZone>();
                }
                Destroy(dropped.GetComponent<DropZone>());
            }
        }
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

    private bool CheckSuitPile()
    {
        return (transform.childCount == 13);
    }
}
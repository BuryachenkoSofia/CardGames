using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            if (CheckCard(dropped))
            {
                RectTransform rt = dropped.GetComponent<RectTransform>();
                Transform oldParents = rt.parent;
                if (this.tag == "Point")
                {
                    rt.SetParent(this.transform, false);
                    rt.anchoredPosition = new Vector2(0, 0);
                }
                else
                {
                    rt.SetParent(this.transform.parent, false);
                    rt.anchoredPosition = new Vector2(0, (this.transform.parent.childCount - 1) * (-45));
                }

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
                if (oldParents.name == "Stock")
                {
                    rt.gameObject.AddComponent<DropZone>();
                }
                Destroy(GetComponent<DropZone>());
            }
        }
    }
    private bool CheckCard(GameObject card)
    {
        if (this.tag == "Point") return card.GetComponent<CardView>().data.rank == Rank.King;

        bool rankCheck = (int)card.GetComponent<CardView>().data.rank == (int)this.GetComponent<CardView>().data.rank - 1;
        bool suitCheck = (card.GetComponent<CardView>().data.suit == Suit.Clubs || card.GetComponent<CardView>().data.suit == Suit.Spades) != (this.GetComponent<CardView>().data.suit == Suit.Clubs || this.GetComponent<CardView>().data.suit == Suit.Spades);
        return rankCheck && suitCheck;
    }
}

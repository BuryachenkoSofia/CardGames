using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;
        List<RectTransform> stack = GetDraggedStack(dropped);
        if (stack.Count == 0) return;

        if (CheckCard(stack[0].gameObject))
        {
            RectTransform firstCard = stack[0];
            Transform oldParent = firstCard.parent;
            Transform newParent;

            if (this.CompareTag("Point"))
            {
                newParent = this.transform;
            }
            else
            {
                newParent = this.transform.parent;
            }
            foreach (var card in stack)
            {
                card.SetParent(newParent, false);
            }
            ReorderStack(newParent);

            if (oldParent.childCount > 0 && oldParent.name != "Stock" && oldParent.name != "FoundationDropZone")
            {
                CardView lastCard = oldParent.GetChild(oldParent.childCount - 1).GetComponent<CardView>();
                lastCard.data.faceUp = true;
                lastCard.UpdateView();
                if (!oldParent.GetChild(oldParent.childCount - 1).TryGetComponent<DropZone>(out _))
                {
                    oldParent.GetChild(oldParent.childCount - 1).gameObject.AddComponent<DropZone>();
                }
            }
            else if (oldParent.name == "FoundationDropZone")
            {
                dropped.AddComponent<DropZone>();
            }
            else
            {
                if (!oldParent.TryGetComponent<DropZone>(out _))
                {
                    oldParent.gameObject.AddComponent<DropZone>();
                }
            }
            if (oldParent.name == "Stock")
            {
                if (!firstCard.TryGetComponent<DropZone>(out _))
                {
                    firstCard.gameObject.AddComponent<DropZone>();
                }
            }
            Destroy(GetComponent<DropZone>());
        }
    }

    private void ReorderStack(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            RectTransform rt = parent.GetChild(i).GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, -45f * i);
        }
    }

    private List<RectTransform> GetDraggedStack(GameObject dropped)
    {
        List<RectTransform> stack = new List<RectTransform>();
        Transform parent = dropped.transform.parent;
        for (int i = dropped.transform.GetSiblingIndex(); i < parent.childCount; i++)
        {
            stack.Add(parent.GetChild(i).GetComponent<RectTransform>());
        }
        return stack;
    }

    private bool CheckCard(GameObject card)
    {
        if (this.tag == "Point") return card.GetComponent<CardView>().data.rank == Rank.King;
        bool rankCheck = (int)card.GetComponent<CardView>().data.rank == (int)this.GetComponent<CardView>().data.rank - 1;
        bool suitCheck = (card.GetComponent<CardView>().data.suit == Suit.Clubs || card.GetComponent<CardView>().data.suit == Suit.Spades) != (this.GetComponent<CardView>().data.suit == Suit.Clubs || this.GetComponent<CardView>().data.suit == Suit.Spades);
        return rankCheck && suitCheck;
    }
}
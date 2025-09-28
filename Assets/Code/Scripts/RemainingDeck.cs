using UnityEngine;
using UnityEngine.EventSystems;

public class RemainingDeck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform stock;
    private Transform remainingDeck;

    private void Awake()
    {
        remainingDeck = this.transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if (FindFirstObjectByType<Counter>().win) return;
            FindFirstObjectByType<Counter>().Add();
            if (remainingDeck.childCount == 0 && stock.childCount > 0)
            {
                int count = stock.childCount;
                for (int i = count - 1; i >= 0; i--)
                {
                    Transform card = stock.GetChild(i);
                    card.SetParent(remainingDeck, false);
                    CardView view = card.GetComponent<CardView>();
                    view.data.faceUp = false;
                    view.UpdateView();
                    card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }
    }
}
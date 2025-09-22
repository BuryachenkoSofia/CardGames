using Unity.VisualScripting;
using UnityEngine;

public class DeckVisualizer : MonoBehaviour
{
    private DeckManager deckManager;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform deckParent;

    private void Awake()
    {
        deckManager = GetComponent<DeckManager>();
    }
    private void Start()
    {
        ShowDeck();
    }
    private void ShowDeck()
    {
        foreach (CardData cardData in deckManager.deck)
        {
            GameObject card = Instantiate(cardPrefab, deckParent);
            CardView cardView = card.GetComponent<CardView>();
            cardView.SetCard(cardData);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(deckParent.childCount * 34 - deckParent.gameObject.transform.position.x - 70, 0);
        }
    }
}
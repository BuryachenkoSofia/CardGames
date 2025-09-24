using Unity.VisualScripting;
using UnityEngine;

public class DeckVisualizer : MonoBehaviour
{
    private DeckManager deckManager;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform deckParent;
    [SerializeField] private Transform[] tableauZones;
    [SerializeField] private Transform remainingDeck;
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
        int cardIndex = 0;

        for (int i = 0; i < tableauZones.Length; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                GameObject card = Instantiate(cardPrefab, tableauZones[i]);
                CardView cardView = card.GetComponent<CardView>();
                CardData copy = new CardData { suit = deckManager.deck[cardIndex].suit, rank = deckManager.deck[cardIndex].rank, faceUp = j == i };
                cardView.SetCard(copy);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0, -j * 45f);
                cardIndex++;
                if (j == i) card.AddComponent<DropZone>();
            }
        }
        while (cardIndex < deckManager.deck.Count)
        {
            GameObject remainingCard = Instantiate(cardPrefab, remainingDeck);
            CardView remainingCardView = remainingCard.GetComponent<CardView>();
            CardData remainingCopy = new CardData { suit = deckManager.deck[cardIndex].suit, rank = deckManager.deck[cardIndex].rank, faceUp = false };
            remainingCardView.SetCard(remainingCopy);
            remainingCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            cardIndex++;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    private Image image;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private Sprite[] clubsSprites;
    [SerializeField] private Sprite[] diamondsSprites;
    [SerializeField] private Sprite[] heartsSprites;
    [SerializeField] private Sprite[] spadesSprites;

    private CardData data;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    
    public void GenerateRandomCard()
    {
        CardData card = new CardData
        {
            suit = (Suit)Random.Range(0, 4),
            rank = (Rank)Random.Range(1, 14),
            faceUp = Random.Range(0, 10) > 1
        };
        SetCard(card);
    }

    public void SetCard(CardData card)
    {
        data = card;
        UpdateView();
    }

    public void UpdateView()
    {
        if (data.faceUp) image.sprite = GetSprite(data.suit, data.rank);
        else image.sprite = backSprite;
    }

    private Sprite GetSprite(Suit suit, Rank rank)
    {
        switch (suit)
        {
            case Suit.Clubs:
                return clubsSprites[(int)rank - 1];
            case Suit.Diamonds:
                return diamondsSprites[(int)rank - 1];
            case Suit.Hearts:
                return heartsSprites[(int)rank - 1];
            case Suit.Spades:
                return spadesSprites[(int)rank - 1];
        }
        return null;
    }
}
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new List<CardData>();
    private void Awake()
    {
        CreateDeck();
        ShuffleDeck();
    }
    private void CreateDeck()
    {
        deck.Clear();
        foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new CardData
                {
                    suit = suit,
                    rank = rank,
                    faceUp = true
                });
            }
        }
    }
    private void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardData temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new List<CardData>();

    private void Awake()
    {
        CreateDeck();
        ShuffleDeckSolitaire();
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
        ShuffleList(deck, rng);
    }

    private void ShuffleDeckSolitaire()
    {
        List<CardData> playerCards = new List<CardData>(); 
        List<CardData> restCards = new List<CardData>(deck);
        System.Random rng = new System.Random();
        foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
        {
            List<CardData> blacks = restCards.FindAll(c => c.rank == rank && (c.suit == Suit.Spades || c.suit == Suit.Clubs));
            if (blacks.Count > 0)
            {
                CardData chosen = blacks[rng.Next(blacks.Count)];
                playerCards.Add(chosen);
                restCards.Remove(chosen);
            }
            List<CardData> reds = restCards.FindAll(c => c.rank == rank && (c.suit == Suit.Hearts || c.suit == Suit.Diamonds));
            if (reds.Count > 0)
            {
                CardData chosen = reds[rng.Next(reds.Count)];
                playerCards.Add(chosen);
                restCards.Remove(chosen);
            }
        }
        while (playerCards.Count < 24 && restCards.Count > 0)
        {
            int idx = rng.Next(restCards.Count);
            playerCards.Add(restCards[idx]);
            restCards.RemoveAt(idx);
        }
        ShuffleList(playerCards, rng);
        ShuffleList(restCards, rng);
        deck.Clear();
        deck.AddRange(restCards);
        deck.AddRange(playerCards);
    }

    private void ShuffleList(List<CardData> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardData temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
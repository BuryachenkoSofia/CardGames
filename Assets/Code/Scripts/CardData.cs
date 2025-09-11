public enum Suit
{
	Clubs = 0,
	Diamonds = 1,
	Hearts = 2,
	Spades = 3
}

public enum Rank
{
	Ace = 1,
	Two = 2,
	Three = 3,
	Four = 4,
	Five = 5,
	Six = 6,
	Seven = 7,
	Eight = 8,
	Nine = 9,
	Ten = 10,
	Jack = 11,
	Queen = 12,
	King = 13
}

[System.Serializable]
public class CardData
{
	public Suit suit;
	public Rank rank;
	public bool faceUp;
}
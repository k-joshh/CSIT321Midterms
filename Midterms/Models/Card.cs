namespace Midterms.Models;

public enum Suit { Hearts, Diamonds, Clubs, Spades }

public enum Rank
{
    Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8,
    Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14
}

public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public string SuitSymbol => Suit switch
    {
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        Suit.Spades => "♠",
        _ => ""
    };

    public string TextColor => (Suit == Suit.Hearts || Suit == Suit.Diamonds) ? "text-red-600" : "text-slate-900";

    public string DisplayRank => Rank switch
    {
        Rank.Ace => "A",
        Rank.King => "K",
        Rank.Queen => "Q",
        Rank.Jack => "J",
        _ => ((int)Rank).ToString()
    };
}
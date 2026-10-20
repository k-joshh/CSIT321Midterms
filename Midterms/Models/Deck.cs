namespace Midterms.Models;

public class Deck
{
    private readonly List<Card> _cards = new();
    private static readonly Random _rng = new();

    public Deck() => Reset();

    public void Reset()
    {
        _cards.Clear();
        foreach (Suit s in Enum.GetValues<Suit>())
            foreach (Rank r in Enum.GetValues<Rank>())
                _cards.Add(new Card { Suit = s, Rank = r });

        Shuffle();
    }

    public void Shuffle()
    {
        int n = _cards.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }
    }

    public Card Draw()
    {
        if (_cards.Count == 0) Reset();
        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}
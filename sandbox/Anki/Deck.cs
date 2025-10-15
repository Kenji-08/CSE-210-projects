class Deck
{
    // Attributes
    public List<Flashcard> cards = new List<Flashcard>();

    // Behaviors
    public void appendCard(Flashcard card)
    {
        cards.Add(card);
    }
}
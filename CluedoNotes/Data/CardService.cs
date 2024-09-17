namespace CluedoNotes.Data;
public class CardService
{
    public string StatusMessage { get; set; }
    public async Task<List<Card>> GetCardsAsync()
    {
        return await App._dbService.GetCardsAsync();
    }
    public async Task<Card> CreateCardAsync(Card card)
    {
        // Insert
        await App._dbService.CreateCardAsync(card);
        // return the object with the
        // auto incremented Id populated
        return card;
    }
    public async Task<Card> UpdateCardAsync(Card card)
    {
        // Update
        await App._dbService.UpdateCardAsync(card);
        // Return the updated object
        return card;
    }
    public async Task<Card> DeleteCardAsync(Card card)
    {
        // Delete
        await App._dbService.DeleteCardAsync(card);
        return card;
    }
    public async Task<Player> CreateHeldCardGuess(int playerId, List<Card> cards, TickColour tickColour)
    {
        if (playerId == 0)
            throw new Exception("Player not specified");

        return await App._dbService.CreateHeldCardGuess(playerId, cards, tickColour);
    }

    public async Task<List<HeldCard>> GetHeldCardsAsync()
    {
        return await App._dbService.GetAllHeldCards();
    }

    public async Task<HeldCard> DeleteHistroyEvent(HeldCard heldCard)
    {
        return await App._dbService.DeleteHistoryEvent(heldCard);
    }
}
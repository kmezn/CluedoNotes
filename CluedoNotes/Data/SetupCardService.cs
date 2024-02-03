﻿namespace CluedoNotes.Data;
public class SetupCardService
{
    public string StatusMessage { get; set; }
    public async Task<List<Card>> GetCardsAsync()
    {
        return await App._dbService.GetCardsAsync();
    }
    public async Task<Card> CreateCardAsync(Card card)
    {
        // Insert
        var card2 = await App._dbService.CreateCardAsync(card);
        // return the object with the
        // auto incremented Id populated

        //NOTE: Check that ID is updated when card created. 
        return card2;
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
}
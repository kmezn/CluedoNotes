﻿
namespace CluedoNotes.Data;
public class PlayerService
{
    public string StatusMessage { get; set; }
    
    public async Task<List<Player>> GetPlayersAsync()
    {
        return await App._dbService.GetPlayersAsync();   
    }
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        // Insert
        await App._dbService.CreatePlayerAsync(player);
        // return the object with the
        // auto incremented Id populated
        return player;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
            // Update
            await App._dbService.UpdatePlayerAsync(player);
            // Return the updated object
            return player;
    }

    public async Task<Player> UpdatePlayerCardsAsync(Player player, List<Card> cards)
    {
        try
        {
            var players = new List<Player>()
            {
                player
            };
            await App._dbService.CreateHeldCard(players, cards);
            // Return the updated object
            return player;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        await App._dbService.DeletePlayerAsync(player);     
        return player;
    }

    public async Task<Settings> GetSettingsAsync()
    {
        try
        {
            return await App._dbService.GetSettingsAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Settings> UpdateSettingsAsync(Settings settings)
    {
        return await App._dbService.UpdateSettingsAsync(settings);
    }

    public async Task ChangeDefaultCards(GameVersion version)
    {
        await App._dbService.ChangeDefaultCards(version);
    }
}

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

    public async Task<Player> UpdatePlayerCardsAsync(Player player)
    {
        try
        {
            if (player.Id == 0)
                throw new Exception("Player not specified");
            // Update
            await App._dbService.UpdatePlayerCardsAsync(player);
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
        return await App._dbService.GetSettingsAsync();
    }

    public async Task<Settings> UpdateSettingsAsync(Settings settings)
    {
        return await App._dbService.UpdateSettingsAsync(settings);
    }
}
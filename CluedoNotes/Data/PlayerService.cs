
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
    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        await App._dbService.DeletePlayerAsync(player);     
        return player;
    }
}
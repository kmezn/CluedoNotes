
namespace CluedoNotes.Data;
public class GameNotesService
{
    public string StatusMessage { get; set; }
    
    public async Task<List<Player>> GetPlayersAsync()
    {
        return await App._dbService.GetPlayersAsync();   
    }
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        await App._dbService.CreatePlayerAsync(player);
        return player;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
            await App._dbService.UpdatePlayerAsync(player);
            return player;
    }
    public async Task<Player> DeletePlayerAsync(Player player)
    {
        await App._dbService.DeletePlayerAsync(player);     
        return player;
    }
}
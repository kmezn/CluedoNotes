namespace CluedoNotes.Data;
public class SetupPlayerService
{
    string _dbPath;
    public string StatusMessage { get; set; }
    public SetupPlayerService(string dbPath)
    {
        _dbPath = dbPath;
    }
    
    public async Task<List<Player>> GetPlayersAsync()
    {
        return await App._dbService.GetPlayersAsync();
        
    }
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        // Insert
        var p = await App._dbService.CreatePlayerAsync(player);
        // return the object with the
        // auto incremented Id populated
        return p;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
        // Update
        var p = await App._dbService.UpdatePlayerAsync(player);
        // Return the updated object
        return p;
    }
    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        var p = await App._dbService.DeletePlayerAsync(player);     
        return p;
    }
}
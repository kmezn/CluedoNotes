
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
        await App._dbService.CreatePlayerAsync(player);
        return player;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
        await App._dbService.UpdatePlayerAsync(player);
        return player;
    }

    public async Task<Player> UpdatePlayerCardsAsync(Player player, List<Card> cards)
    {
        var players = new List<Player>()
        {
            player
        };
        await App._dbService.CreateHeldCard(players, cards);
        return player;
    }

    public async Task<Player> DeletePlayerAsync(Player player)
    {
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

    public async Task ChangeDefaultCards(GameVersion version)
    {
        await App._dbService.ChangeDefaultCards(version);
    }
}
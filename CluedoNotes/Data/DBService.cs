using SQLite;

namespace CluedoNotes.Data;
public class DBService
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection conn;
    public DBService(string dbPath)
    {
        _dbPath = dbPath;
    }
    private async Task InitAsync()
    {
        // Don't Create database if it exists
        if (conn != null)
            return;
        // Create database and Card Table
        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Card>();
        await conn.CreateTableAsync<Player>();
    }
    public async Task<List<Card>> GetCardsAsync()
    {
        await InitAsync();
        return await conn.Table<Card>().ToListAsync();
    }
    public async Task<Card> CreateCardAsync(Card card)
    {
        // Insert
        await conn.InsertAsync(card);
        // return the object with the
        // auto incremented Id populated
        return card;
    }
    public async Task<Card> UpdateCardAsync(Card card)
    {
        // Update
        await conn.UpdateAsync(card);
        // Return the updated object
        return card;
    }
    public async Task<Card> DeleteCardAsync(Card card)
    {
        // Delete
        await conn.DeleteAsync(card);
        return card;
    }

    public async Task<List<Player>> GetPlayersAsync()
    {
        await InitAsync();
        return await conn.Table<Player>().ToListAsync();
    }
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        // Insert
        await conn.InsertAsync(player);
        // return the object with the
        // auto incremented Id populated
        return player;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
        // Update
        await conn.UpdateAsync(player);
        // Return the updated object
        return player;
    }
    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        await conn.DeleteAsync(player);
        return player;
    }
}
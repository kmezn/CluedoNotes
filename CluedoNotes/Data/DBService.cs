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
        CreateTableResult createTableResult1 = await conn.CreateTableAsync<Player>();
        CreateTableResult createTableResult = await conn.CreateTableAsync<HeldCard>();

#if DEBUG
        var debugCards = new List<Card>()
        {
            new Card(){Name ="Miss Scarlett", CardType = CardType.Suspect },
            new Card(){Name = "Colonel Mustard", CardType = CardType.Suspect },
            new Card(){Name = "Mrs White", CardType = CardType.Suspect},
            new Card(){Name = "Reverend Green", CardType = CardType.Suspect },
            new Card(){Name = "Mrs Peacock", CardType = CardType.Suspect },
            new Card(){Name = "Professor Plum", CardType = CardType.Suspect },
            new Card(){Name = "Candlestick", CardType = CardType.Weapon },
            new Card(){Name = "Dagger", CardType = CardType.Weapon },
            new Card(){Name = "Lead Piping", CardType = CardType.Weapon },
            new Card(){Name = "Revolver", CardType = CardType.Weapon },
            new Card(){Name = "Rope", CardType = CardType.Weapon },
            new Card(){Name = "Spanner", CardType = CardType.Weapon },
            new Card(){Name = "Kitchen", CardType = CardType.Room },
            new Card(){Name = "Ballroom", CardType = CardType.Room },
            new Card(){Name = "Conservatory", CardType = CardType.Room },
            new Card(){Name = "Dining Room", CardType = CardType.Room },
            new Card(){Name = "Billiard Room", CardType = CardType.Room },
            new Card(){Name = "Library", CardType = CardType.Room },
            new Card(){Name = "Lounge", CardType = CardType.Room },
            new Card(){Name = "Hall", CardType = CardType.Room },
            new Card(){Name = "Study", CardType = CardType.Room },
        };
        var debugPlayers = new List<Player>()
        {
            new Player(){Name = "Player 1"},
            new Player(){Name = "Player 2"},
            new Player(){Name = "Player 3"},
            new Player(){Name = "Player 4"}
        };
        var cards = await GetCardsAsync();
        var players = await GetPlayersAsync();

        debugCards.Where(c => !cards.Any(a => a.Name == c.Name)).ToList().ForEach(async r => await CreateCardAsync(r));
        debugPlayers.Where(c => !players.Any(a => a.Name == c.Name)).ToList().ForEach(async p => await CreatePlayerAsync(p));

        

        //List<Task<Card>> cardTasks = new List<Task<Card>>();
        //debugCards.ForEach(r => cardTasks.Add(CreateCardAsync(r)));
        //var debugPlayers = new List<Player>()
        //{
        //    new Player(){Name = ""}
        //};
        //List<Task<Player>> playerTasks = new List<Task<Player>>();
        //debugPlayers.ForEach(p => playerTasks.Add(CreatePlayerAsync(p)));
        //Task.WaitAll(playerTasks.ToArray());
        //Task.WaitAll(cardTasks.ToArray());    
#endif
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
        var players = await conn.Table<Player>().ToListAsync();
        foreach (var player in players)
        {
            player.HeldCards = await conn.Table<HeldCard>()
                .Where(w => w.PlayerId == player.Id)
                .ToListAsync();
            if (player.HeldCards.Any())
                await FetchPlayerHeldCards(player);
        }

        return players;
    }

    public async Task<Player> FetchPlayerHeldCards(Player player)
    {
        //****** would it be better to have cards and players stored as _players and _cards? ******
        var allCards = await conn.Table<Card>().ToListAsync();
        foreach (var card in player.HeldCards)
        {
            card.Card = allCards.First(f => f.Id == card.CardId);
        }
        return player;
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

    public async Task<Player> UpdatePlayerCardsAsync(Player player)
    {
        // loop held cards. 
        foreach (var h in player.HeldCards)
        {
            if (h.Id == 0)
            {
                await conn.InsertAsync(h);
            }
            else
            {
                await conn.UpdateAsync(h);
            }
            
        }
        // Return the updated object
        return player;
    }

    public async Task<Player> CreateHeldCardGuess(int playerId, List<Card> cards)
    {
        var player = await conn.Table<Player>().FirstAsync(w => w.Id == playerId);
            player.HeldCards = await conn.Table<HeldCard>()
                .Where(w => w.PlayerId == player.Id)
                .ToListAsync();

        var heldId = conn.Table<HeldCard>().OrderByDescending(o => o.EventId).FirstAsync().Result.EventId;
        heldId++;

        foreach (var card in cards)
        {
            var c = new HeldCard()
            {
                CardId = card.Id,
                EventId = heldId,
                IsConfirmed = false,
                PlayerId = playerId,
            };
            await conn.InsertAsync(c);
            player.HeldCards.Add(c);
        }

        return player;
    }



    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        await conn.DeleteAsync(player);
        return player;
    }
}
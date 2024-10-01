using SQLite;

namespace CluedoNotes.Data;
public class DBService
{
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection conn;
    public DBService()
    {

    }
    private async Task InitAsync()
    {
        // Don't Create database if it exists
        if (conn != null)
            return;
        
        // Create database and Card Table
        conn = new SQLiteAsyncConnection(MauiProgram.DatabasePath, MauiProgram.Flags);
        //conn = new SQLiteAsyncConnection(_dbPath);

        await conn.CreateTableAsync<Card>();
        await conn.CreateTableAsync<Player>();
        await conn.CreateTableAsync<HeldCard>();
        await conn.CreateTableAsync<Settings>();

        var defaultSettings = new Settings()
        {
            Theme = AppTheme.Dark,
            ConfirmedCardSeenColour = TickColour.Green,
            ConfirmedCardSeenStyle = TickStyle.check,
            ConfirmedNoCardColour = TickColour.Red,
            ConfirmedNoCardStyle = TickStyle.x,
            CardShownColour = TickColour.purple,
            MyCardColour = TickColour.Blue,
            MyCardStyle = TickStyle.bookmark,
        };
        await UpdateSettingsAsync(defaultSettings);

        await InitDefaultCards(GameVersion.Classic);
        await InitDefaultPlayers();

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
    }
    public async Task ChangeDefaultCards(GameVersion version)
    {
        var existingCards = await GetCardsAsync();
        await Parallel.ForEachAsync(existingCards, async (i, token) =>
        {
            await DeleteCardAsync(i);
        });
        await InitDefaultCards(version);
    }
    private async Task InitDefaultCards(GameVersion version)
    {
        var cards = new List<Card>();
        switch (version)
        {
            case GameVersion.Classic:
                 cards.AddRange( new List<Card>()
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
                });
                break;
            case GameVersion.EnglishHerritige:
                cards.AddRange(new List<Card>()
                {
                    new Card(){Name ="Emperor Hadrian", CardType = CardType.Suspect },
                    new Card(){Name = "Winston Churchill", CardType = CardType.Suspect },
                    new Card(){Name = "Charles Darwin", CardType = CardType.Suspect},
                    new Card(){Name = "Queen Victoria", CardType = CardType.Suspect },
                    new Card(){Name = "Queen Elizabeth", CardType = CardType.Suspect },
                    new Card(){Name = "Boudica", CardType = CardType.Suspect },
                    new Card(){Name = "Roman Coins", CardType = CardType.Weapon },
                    new Card(){Name = "Excalibur Sword", CardType = CardType.Weapon },
                    new Card(){Name = "Telescope", CardType = CardType.Weapon },
                    new Card(){Name = "Rembrandt Portrait", CardType = CardType.Weapon },
                    new Card(){Name = "Crown Jewels", CardType = CardType.Weapon },
                    new Card(){Name = "Secret Documents", CardType = CardType.Weapon },
                    new Card(){Name = "Osborne", CardType = CardType.Room },
                    new Card(){Name = "Tintagel Castle", CardType = CardType.Room },
                    new Card(){Name = "Kenwood", CardType = CardType.Room },
                    new Card(){Name = "Whitby Abbey", CardType = CardType.Room },
                    new Card(){Name = "Down House", CardType = CardType.Room },
                    new Card(){Name = "Kenilworth Castle", CardType = CardType.Room },
                    new Card(){Name = "Hadrian's Wall", CardType = CardType.Room },
                    new Card(){Name = "Battle Abbey", CardType = CardType.Room },
                    new Card(){Name = "Dover Tunnels", CardType = CardType.Room },
                });
                break;
        }
        /// fix existing cards being added in addition. 
        var existingCards = await GetCardsAsync();
        cards.Where(c => !existingCards.Any(a => a.Name == c.Name)).ToList().ForEach(async r => await CreateCardAsync(r));
        
    }
    private async Task InitDefaultPlayers()
    {
        var debugPlayers = new List<Player>()
        {
            new Player(){Name = "Player 1"},
            new Player(){Name = "Player 2"},
            new Player(){Name = "Player 3"},
            new Player(){Name = "Player 4"}
        };

        var players = await GetPlayersAsync();
        debugPlayers.Where(c => !players.Any(a => a.Name == c.Name)).ToList().ForEach(async p => await CreatePlayerAsync(p));
    }
    public async Task<List<Card>> GetCardsAsync()
    {
            
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
        var players = new List<Player>
        {
            new Player()
            {
                Id = 0,
                Name = "My Cards"
            }
        };
        players.AddRange( await conn.Table<Player>().ToListAsync());

        foreach (var player in players)
        {
            player.HeldCards = await conn.Table<HeldCard>()
                .Where(w => w.PlayerId == player.Id)
                .ToListAsync();
            if (player.HeldCards.Any())
                await GetPlayerHeldCards(player);
        }

        return players;
    }

    public async Task<Player> GetPlayerHeldCards(Player player)
    {
        //****** would it be better to have cards and players stored as _players and _cards? ******
        var allCards = await conn.Table<Card>().ToListAsync();
        foreach (var card in player.HeldCards)
        {
            card.Card = allCards.First(f => f.Id == card.CardId);
        }
        return player;
    }

    public async Task<List<HeldCard>> GetAllHeldCards()
    {
        var allCards = await conn.Table<Card>().ToListAsync();
        var players = await GetPlayersAsync();
        var allHeldCards = await conn.Table<HeldCard>().ToListAsync();
        foreach(var heldCard in allHeldCards)
        {
            heldCard.Card = allCards.First(c => c.Id == heldCard.CardId);
            heldCard.Player = players.First(p => p.Id == heldCard.PlayerId);
        }

        return allHeldCards;
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

    //public async Task<Player> UpdatePlayerCardsAsync(Player player, List<Card> cards)
    //{
    //    var settings = await GetSettingsAsync();
    //    var heldId = conn.Table<HeldCard>().OrderByDescending(o => o.EventId).FirstOrDefaultAsync()?.Result?.EventId ?? 0;
    //    heldId++;
    //    var tickColour = TickColour.purple;
    //    // loop held cards. 
    //    foreach (var h in cards)
    //    {
            
            
    //    }
    //    // Return the updated object
    //    return player;
    //}

    public async Task<int> CreateHeldCard(List<Player> players, List<Card> cards)
    {
        var settings = await GetSettingsAsync();
        // default colour incase a problem arrises... 
        var tickColour = TickColour.purple;
        var tickStyle = TickStyle.warning;

        var heldId = conn.Table<HeldCard>().OrderByDescending(o => o.EventId).FirstOrDefaultAsync()?.Result?.EventId ?? 0;
        heldId++;

        foreach (var player in players.Where(p => p.TmpHasACard || p.TmpHasNoCard)) 
        {
            player.HeldCards = await conn.Table<HeldCard>()
                .Where(w => w.PlayerId == player.Id)
                .ToListAsync();
            foreach (var card in cards)
            {
                if (player.Id == 0 || player.TmpShownMyCard) 
                {
                    tickColour = settings.MyCardColour;
                    tickStyle = settings.MyCardStyle;
                }
                else if (player.TmpIsConfirmed) 
                {
                    tickColour = settings.ConfirmedCardSeenColour;
                    tickStyle = settings.ConfirmedCardSeenStyle;
                }
                else if (player.TmpHasACard)
                {
                    tickColour = settings.CardShownColour;
                }
                else if (player.TmpHasNoCard)
                {
                    tickColour = settings.ConfirmedNoCardColour;
                    tickStyle = settings.ConfirmedNoCardStyle;
                }
                
                var c = new HeldCard()
                {
                    CardId = card.Id,
                    EventId = heldId,
                    IsConfirmed = player.TmpIsConfirmed,
                    PlayerId = player.Id,
                    TickColour = tickColour,
                    TickStyle = tickStyle,
                };
                await conn.InsertAsync(c);
                player.HeldCards.Add(c);
            }
        }
        return 0;
    }

    public async Task<Player> DeletePlayerAsync(Player player)
    {
        // Delete
        await conn.DeleteAsync(player);
        return player;
    }

    public async Task<Settings> GetSettingsAsync()
    { // probably the initial landing page... 
        try
        {
            await InitAsync();
            return await conn.Table<Settings>().FirstAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task DeleteAllHistoryEvent()
    {
        var events = await GetAllHeldCards();
        await Parallel.ForEachAsync(events, async (i, token) =>
        {
            await DeleteHistoryEvent(i);
        });
    }

    public async Task<HeldCard> DeleteHistoryEvent(HeldCard heldCard)
    {
        try
        {
            await conn.DeleteAsync(heldCard);
            return heldCard;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task<Settings> UpdateSettingsAsync(Settings settings)
    {
        if (settings.Id == 0)
        {
            await conn.InsertAsync(settings);
        }
        else
        {
            await conn.UpdateAsync(settings);
        }

        // Return the updated object
        return settings;
    }

}
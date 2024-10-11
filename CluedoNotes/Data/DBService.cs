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
        // Don't Create database connection if it exists
        if (conn != null)
            return;
        
        // Create database and Tables
        conn = new SQLiteAsyncConnection(MauiProgram.DatabasePath, MauiProgram.Flags);
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

        var settingsCheck = await conn.Table<Settings>().CountAsync();
        if (settingsCheck == 0)
        {
            await UpdateSettingsAsync(defaultSettings);
        }

        var cardsCheck = await conn.Table<Card>().CountAsync();
        if (cardsCheck == 0)
        {
            await InitDefaultCards(GameVersion.Classic);
        }
        
        var playerCheck = await conn.Table<Player>().CountAsync();
        if (playerCheck == 0)
        {
            await InitDefaultPlayers();
        }
        
        
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
        /// fix existing cards being added in addition . 
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
        await conn.InsertAsync(card);
        return card;
    }
    public async Task<Card> UpdateCardAsync(Card card)
    {
        await conn.UpdateAsync(card);
        return card;
    }
    public async Task<Card> DeleteCardAsync(Card card)
    {
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
        await conn.InsertAsync(player);
        return player;
    }
    public async Task<Player> UpdatePlayerAsync(Player player)
    {
        await conn.UpdateAsync(player);
        return player;
    }

    public async Task<int> CreateHeldCard(List<Player> players, List<Card> cards)
    {
        var settings = await GetSettingsAsync();
        // default colour in case a problem arises... 
        var tickColour = TickColour.purple;
        var tickStyle = TickStyle.warning;
        var logType = LogType.MyCard;

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
                    logType = LogType.MyCard;
                }
                else if (player.TmpIsConfirmed) 
                {
                    tickColour = settings.ConfirmedCardSeenColour;
                    tickStyle = settings.ConfirmedCardSeenStyle;
                    logType = LogType.CardSeen;
                }
                else if (player.TmpHasACard)
                {
                    tickColour = settings.CardShownColour;
                    logType = LogType.PossibleCardEvent;
                }
                else if (player.TmpHasNoCard)
                {
                    tickColour = settings.ConfirmedNoCardColour;
                    tickStyle = settings.ConfirmedNoCardStyle;
                    logType = LogType.NoCardEvent;
                }

                var c = new HeldCard()
                {
                    CardId = card.Id,
                    EventId = heldId,
                    IsConfirmed = player.TmpIsConfirmed,
                    PlayerId = player.Id,
                    TickColour = tickColour,
                    TickStyle = tickStyle,
                    LogType = logType
                };
                await conn.InsertAsync(c);
                player.HeldCards.Add(c);
            }
        }
        return 0;
    }

    public async Task<Player> DeletePlayerAsync(Player player)
    {
        await conn.DeleteAsync(player);
        return player;
    }

    public async Task<Settings> GetSettingsAsync()
    {
            await InitAsync();
            return await conn.Table<Settings>().FirstAsync();
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
            await conn.DeleteAsync(heldCard);
            return heldCard;
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
        return settings;
    }

}
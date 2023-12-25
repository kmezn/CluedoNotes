using CluedoNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace CluedoNotes.Repos
{

    public class CluedoContext : DbContext
    {
        string _dbPath;
        //private SQLiteConnection conn;
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<>
        //    base.OnModelCreating(modelBuilder);
        //}
        public string StatusMessage { get; set; }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Player> Players { get; set; }




        private void Init(CluedoContext context)
        {
            if (context != null)
                return;


            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();





            //conn.CreateTable<Card>();
            //conn.CreateTable<Player>();
            //conn.CreateTable<HeldCard>();


#if DEBUG
            var players = new List<Player> {
                new Player{Name = "Kes"},
                new Player{Name = "Char"},
                new Player {Name = "Wendy"},
                new Player {Name = "Paul"},
                new Player {Name = "Luke"}
                };
            players.ForEach(p => context.Players.Add(p));

            var cards = new List<Card>
            {
                new Card{Name = "Kitchen", IsRoom  = true},
                new Card{Name = "Ballroom", IsRoom =  true },
                new Card { Name = "Library", IsRoom = true },
                new Card { Name = "Crnl Mustard", IsSuspect = true },
                new Card { Name = "Proff Plum", IsSuspect = true },
                new Card {Name ="Miss Scarlett", IsSuspect = true},
                new Card { Name ="Gun", IsWeapon = true },
                new Card { Name ="Candlestick", IsWeapon = true },
                new Card { Name ="Rope", IsWeapon = true }
            };
            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();
#endif
        }

        //public DBRepository(string dbPath)
        //{
        //    _dbPath = dbPath;
        //}

        #region Player
        public void AddNewPlayer(string name)
        {
            int result = 0;
            try
            {
                Init();
                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Player { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<Player> GetAllPlayers()
        {
            try
            {
                Init();
                return conn.Table<Player>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Player>();
        }
        public void RemovePlayer(Player player)
        {
            Init();
            conn.Delete(player);
            StatusMessage = string.Format("Player {0} Removed ", player.Name);
        }

        public Player LogCardSeen(Player player)
        {
            try
            {
                Init();
                //var p = conn.Table<Player>().First();
                //p.HeldCards.Add(new HeldCard { CardId = conn.Table<Card>().FirstOrDefault().Id, PlayerId = player.Id });
                conn.Update(player);
                //conn.Select<Card>(c => c.);
                //.Update(player.HeldCards.AddOrUpdate());
                StatusMessage = string.Format("Player {0} cards updated", player.Name);
                return player;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        public void AddNewCard(string name, bool isRoom = false, bool isSuspect = false, bool isWeapon = false)
        {
            int result = 0;
            try
            {
                Init();
                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");
                if (!isRoom && !isSuspect && !isWeapon)
                    throw new Exception("Entry is not designated with a card type");

                result = conn.Insert(new Card
                {
                    Name = name
                    ,
                    IsRoom = isRoom
                ,
                    IsSuspect = isSuspect
                ,
                    IsWeapon = isWeapon
                });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }
        public List<Card> GetAllCards(bool justRooms = false, bool justSuspects = false, bool JustWeapons = false)
        {
            try
            {
                Init();
                if (justRooms)
                    return conn.Table<Card>().Where(c => c.IsRoom == justRooms).ToList();
                else if (justSuspects)
                    return conn.Table<Card>().Where(c => c.IsSuspect == justSuspects).ToList();
                else if (JustWeapons)
                    return conn.Table<Card>().Where(c => c.IsWeapon == JustWeapons).ToList();
                else
                    return conn.Table<Card>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Card>();
        }

        public void RemoveCard(Card card)
        {
            Init();
            conn.Delete(card);
            StatusMessage = string.Format("Player {0} Removed ", card.Name);
        }
    }
}

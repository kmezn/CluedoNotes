using CluedoNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Query;

namespace CluedoNotes.Repos
{

    public class DBRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Card>();
            conn.CreateTable<Player>();
            conn.CreateTable<HeldCard>();

#if DEBUG
            AddNewPlayer("Kes");
            AddNewPlayer("Char");
            AddNewPlayer("Wendy");
            AddNewPlayer("Paul");
            AddNewPlayer("Luke");
            AddNewCard("Kitchen", isRoom: true);
            AddNewCard("Ballroom", isRoom: true);
            AddNewCard("Library", isRoom: true);
            AddNewCard("Crnl Mustard", isSuspect: true);
            AddNewCard("Proff Plum", isSuspect: true);
            AddNewCard("Miss Scarlett", isSuspect: true);
            AddNewCard("Gun", isWeapon: true);
            AddNewCard("Candlestick", isWeapon: true);
            AddNewCard("Rope", isWeapon: true);
#endif
        }

        public DBRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

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

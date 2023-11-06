using CluedoNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CluedoNotes
{
    
    public class PlayerRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Player>();
                 
        }

        public PlayerRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPlayer(string name)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Player { Name= name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<Player> GetAllPlayers()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                Init ();
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
    }
}

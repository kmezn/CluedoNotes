using CluedoNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

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
            conn.CreateTable<Cards>();
            conn.CreateTable<Player>();
            conn.CreateTable<Room>();
            conn.CreateTable<Suspect>();
            conn.CreateTable<Weapon>();
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
            // TODO: Init then retrieve a list of Person objects from the database into a list
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
            Init();
            conn.Update(player);
            StatusMessage = string.Format("Player {0} cards updated", player.Name);
            return player;
        }
        #endregion

        #region Suspect

        public void AddNewSuspect(string name)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Suspect { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }


        public List<Suspect> GetAllSuspects()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                Init();
                return conn.Table<Suspect>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Suspect>();
        }
        public void RemoveSuspect(Suspect victim)
        {
            Init();
            conn.Delete(victim);
            StatusMessage = string.Format("Suspect {0} Removed ", victim.Name);
        }
        #endregion

        #region Room

        public void AddNewRoom(string name)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Room { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }


        public List<Room> GetAllRooms()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                Init();
                return conn.Table<Room>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Room>();
        }
        public void RemoveRoom(Room room)
        {
            Init();
            conn.Delete(room);
            StatusMessage = string.Format("Room {0} Removed ", room.Name);
        }
        #endregion

        #region Weapon

        public void AddNewWeapon(string name)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Weapon { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }


        public List<Weapon> GetAllWeapons()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                Init();
                return conn.Table<Weapon>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Weapon>();
        }
        public void RemoveWeapon(Weapon weapon)
        {
            Init();
            conn.Delete(weapon);
            StatusMessage = string.Format("Suspect {0} Removed ", weapon.Name);
        }
        #endregion
    }
}

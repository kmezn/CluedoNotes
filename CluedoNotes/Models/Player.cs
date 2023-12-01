using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CluedoNotes.Models
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [MaxLength(255), Unique] 
        public string Name { get; set; }

        public Cards HeldCards { get; set; }
    }
}

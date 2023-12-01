using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedoNotes.Models
{

    [Table("Cards")]
    public class Cards
    {
        public List<Room> Rooms { get; set; }
        public List<Suspect> Suspects { get; set; }
        public List<Weapon> Weapons { get; set; }
    }
}

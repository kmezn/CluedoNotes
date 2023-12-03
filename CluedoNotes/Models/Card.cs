using SQLite;
using SQLiteNetExtensions.Attributes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CluedoNotes.Models
{

    [Table("Cards")]
    public class Card
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255), Unique]
        public string Name { get; set; }
        public bool IsRoom { get; set; }
        public bool IsSuspect {  get; set; }
        public bool IsWeapon { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<HeldCard> HeldCards { get; set; }

        //public bool IsConfirmed { get; set; }
        //public int PotentialCardEventNo { get; set; }
    }
}

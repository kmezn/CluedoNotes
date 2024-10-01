using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;


namespace CluedoNotes.Data
{
    public enum HasACard
    {
        unknown = 0,
        HasACard = 1,
        NoCard = 2
    }
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string Name { get; set; }

        [OneToMany(nameof(HeldCard), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<HeldCard> HeldCards { get; set; } = new List<HeldCard>();

        [NotMapped]
        public bool HasACard { get; set; }
        [NotMapped]
        public bool HasNoCard { get; set; }
    }
}

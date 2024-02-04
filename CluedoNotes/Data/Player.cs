using SQLite;
using SQLiteNetExtensions.Attributes;


namespace CluedoNotes.Data
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string Name { get; set; }

        [OneToMany(nameof(HeldCard), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<HeldCard> HeldCards { get; set; } = new List<HeldCard>();

    }
}

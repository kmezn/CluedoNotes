using SQLite;

namespace CluedoNotes.Data
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string Name { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<HeldCard> HeldCards { get; set; } = new List<HeldCard>();

    }
}

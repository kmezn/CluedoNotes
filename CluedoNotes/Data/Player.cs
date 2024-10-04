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





        // temp values as view model for passing back to DB with guesses/confirmed cards. 
        [NotMapped]
        public bool TmpHasACard { get; set; }
        [NotMapped]
        public bool TmpHasNoCard { get; set; }
        [NotMapped]
        public bool TmpIsConfirmed { get; set; } = false;
        [NotMapped]
        public bool TmpShownMyCard { get; set; } = false;
    }
}

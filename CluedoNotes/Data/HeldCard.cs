using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;

namespace CluedoNotes.Data
{

    public enum TickColour
    {
        Green = 0,
        Blue = 1,
        Red = 2,
        Yellow = 3,
        purple = 4,
    }

    public enum TickStyle
    {
        check = 0,
        x = 1,
        eye = 2,
        bookmark = 3,
        warning = 4,
        pencil = 5,
        minus = 6
    }
    public class HeldCard
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int EventId { get; set; }

        [ForeignKey(typeof(Card))]
        public int CardId {  get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public Card Card { get; set; }

        [ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public Player Player { get; set; }

        public bool IsConfirmed { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour TickColour { get; set; }
        // tick colour used for confirmation tick or eventId display on game notes page.
    }
}

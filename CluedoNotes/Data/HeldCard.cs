using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;

namespace CluedoNotes.Data
{
    public class HeldCard
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Card))]
        public int CardId {  get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public Card Card { get; set; }

        [ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public Player Player { get; set; }

        public bool IsConfirmed { get; set; }
        //public int PotentialCardEventNo { get; set; }
    }
}

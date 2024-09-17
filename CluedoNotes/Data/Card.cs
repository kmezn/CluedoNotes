using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CluedoNotes.Data;

public enum CardType
{
    Room = 0,
    Suspect = 1,
    Weapon = 2
}

public class Card
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [SQLite.MaxLength(255), Unique]
    public string Name { get; set; }

    public virtual int CardTypeId
    {
        get
        {
            return (int)this.CardType;
        }
        set
        {
            CardType = (CardType)value;
        }
    }
    [EnumDataType(typeof(CardType))]
    public CardType CardType { get; set; }

    [NotMapped]
    public bool Selected { get; set; }
}





//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CluedoNotes.Data
//{
//    internal class Card
//    {
//        //[PrimaryKey, AutoIncrement]
//        //public int Id { get; set; }
//        //[MaxLength(255), Unique]
//        //public string Name { get; set; }
//        //public bool IsRoom { get; set; }
//        //public bool IsSuspect {  get; set; }
//        //public bool IsWeapon { get; set; }

//        //[OneToMany(CascadeOperations = CascadeOperation.All)]
//        //public List<HeldCard> HeldCards { get; set; }
//    }
//}

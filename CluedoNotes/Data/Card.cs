using SQLite;
namespace CluedoNotes.Data;
public class Card
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [MaxLength(255), Unique]
    public string Name { get; set; }
    public bool IsRoom { get; set; }
    public bool IsSuspect { get; set; }
    public bool IsWeapon { get; set; }
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

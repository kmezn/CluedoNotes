using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CluedoNotes.Models
{

    [Table("Cards")]
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255), Required]
        public string Name { get; set; }
        public bool IsRoom { get; set; }
        public bool IsSuspect {  get; set; }
        public bool IsWeapon { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<HeldCard> HeldCards { get; set; }

        //public bool IsConfirmed { get; set; }
        //public int PotentialCardEventNo { get; set; }
    }
}

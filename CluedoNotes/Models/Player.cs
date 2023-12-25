using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CluedoNotes.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(255), Required] 
        public string Name { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<HeldCard> HeldCards { get; set; } = new List<HeldCard>();
    }
}

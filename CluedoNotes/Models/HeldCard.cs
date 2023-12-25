using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CluedoNotes.Models
{

    public class HeldCard
    {
        [Key]
        public int Id {  get; set; }
        //[ForeignKey(typeof(Card))]
        public int CardId {  get; set; }
        //[ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }
        public bool IsConfirmed { get; set; }
        //public int PotentialCardEventNo { get; set; }
    }
}

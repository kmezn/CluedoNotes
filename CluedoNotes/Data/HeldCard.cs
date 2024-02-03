using System.ComponentModel.DataAnnotations.Schema;

namespace CluedoNotes.Data
{
    internal class HeldCard
    {
        public int Id { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }

        [ForeignKey("Card")]
        public int PlayerId { get; set; }
        public bool IsConfirmed { get; set; }
        //public int PotentialCardEventNo { get; set; }
    }
}

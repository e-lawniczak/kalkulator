using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Model
{
    [Table("Operations")]
    public class HistoryEntry
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int HistoryEntryId { get; set; }
        public decimal? ValueA { get; set; }
        public decimal ValueB { get; set; }
        public decimal Result {  get; set; }

        [ForeignKey("Operators")]
        public int OperatorId { get; set; }
        public Operator Operator { get; set; }

    }

}
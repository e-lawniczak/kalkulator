using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Model
{
    [Table("Operators")]
    public class Operator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OperatorId { get; set; }
        public string Value { get; set; }

        public ICollection<HistoryEntry> historyEntries { get; set; }
    }
}
namespace Calculator.Model
{
    public class HistoryEntry
    {
        public int HistoryEntryId { get; set; }
        public Operator Operator { get; set; }
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
    }

}
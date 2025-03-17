
using Calculator.Model;
using Microsoft.EntityFrameworkCore;

namespace Calculator
{
    class HistoryDb
    {
        public HistoryDb()
        {
        }

        async string LoadHistory()
        {
            OperationHistoryContext db = new();
            var entries = db.Operations.OrderByDescending(o => o.HistoryEntryId).Take(10);


        }
    }
}
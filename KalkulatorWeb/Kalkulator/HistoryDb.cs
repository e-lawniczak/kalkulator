
using Calculator.Model;
using Microsoft.EntityFrameworkCore;
using static Calculator.Helpers;
namespace Calculator
{
    public class HistoryDb
    {
        private string[] operators = { "+", "-", "*", "/", "frac", "pow", "sqrt" };
        public HistoryDb()
        {
        }

        public async Task<string> LoadHistory()
        {
            using (OperationHistoryContext db = new())
            {
                var entries = await db.Operations.Include(o => o.Operator).OrderByDescending(o => o.HistoryEntryId).Take(10).ToListAsync();

                return string.Join("\n", entries.Select(e => $"{RemoveZeros(e.ValueA.ToString(), true)} {e.Operator.Value} {RemoveZeros(e.ValueB.ToString(), true)} = {RemoveZeros(e.Result.ToString(), true)}"));

            }
        }
        public async Task<int> SaveOperation(decimal? a, decimal b, string op, decimal result)
        {
            using (OperationHistoryContext db = new())
            {
                try
                {
                    var entry = new HistoryEntry
                    {
                        ValueA = a,
                        ValueB = b,
                        Result = result,
                        OperatorId = GetOpId(op),
                        //Operator = new Operator
                        //{
                        //    OperatorId = GetOpId(op),
                        //    Value = op,
                        //}
                    };
                    db.Add(entry);
                    return await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }
        public async Task<int> FillData()
        {
            using (OperationHistoryContext db = new())
            {
                var opCount = await db.Operators.CountAsync();
                if (opCount == 0)
                    foreach (var item in operators)
                    {
                        db.Add(new Operator
                        {
                            OperatorId = GetOpId(item),
                            Value = item
                        });
                    }

                return await db.SaveChangesAsync();
            }
        }


    }

}
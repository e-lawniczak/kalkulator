
using Calculator.Model;
using Microsoft.EntityFrameworkCore;

namespace Calculator
{
    public class HistoryDb
    {
        public HistoryDb()
        {
        }

        public async Task<string> LoadHistory()
        {
            using (OperationHistoryContext db = new())
            {
                var entries = await db.Operations.Include(o=>o.Operator).OrderByDescending(o => o.HistoryEntryId).Take(10).ToListAsync();

                return string.Join("\n", entries.Select(e => $"{e.ValueA} {e.Operator.Value} {e.ValueB} = {e.Result}"));

            }
        }
        public async Task<int> SaveOperation(decimal? a, decimal b, string op, decimal result)
        {
            using (OperationHistoryContext db = new())
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
        }

        private int GetOpId(string op)
        {
            return op switch
            {
                string when op == "+" => 1,
                string when op == "-" => 2,
                string when op == "*" => 3,
                string when op == "/" => 4,
                string when op == "frac" => 5,
                string when op == "pow" => 6,
                string when op == "sqrt" => 7,
                _ => throw new InvalidOperationException()

            };
        }
    }
    /*
                 migrationBuilder.InsertData(table: "Operators", column: "Value", value: "+");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "-");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "*");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "/");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "frac");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "pow");
            migrationBuilder.InsertData(table: "Operators", column: "Value", value: "sqrt");
     */
}
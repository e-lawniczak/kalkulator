using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Calculator
{
    public static class Helpers
    {
        public const char COMMA = ',';

        public static string RemoveZeros(string s, bool isHistory = false)
        {
            decimal val = 0;
            bool isdecimal = decimal.TryParse(s, out val);

            if (s == "0" || s == string.Empty || s == null) return s;
            if (s == COMMA.ToString()) return "0" + COMMA;
            if (s.IndexOf(COMMA) == 1 && s.StartsWith('0') ) return s;
            if (isHistory && val % 1 == 0) return s.Trim(['0', COMMA]);
            if (val % 1 == 0 ) return s.TrimStart(['0', COMMA]);
            return s.TrimStart('0');
        }
        public static int GetOpId(string op)
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
}

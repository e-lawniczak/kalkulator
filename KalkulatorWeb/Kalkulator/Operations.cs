using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Calculator.Helpers;
namespace Calculator
{
    public class Operations
    {
        private TextBox Display;
        public Operations(ref TextBox texBox) {
            Display = texBox;
        }
        public decimal Backspace()
        {
            if (Display.Text.Length <= 1)
            {
                Display.Text = "0";
                return 0;
            }
            Display.Text = RemoveZeros(Display.Text.Remove(Display.Text.Length - 1));
            bool NaN = true;
            decimal res = 0;
            NaN = !decimal.TryParse(Display.Text, out res);
            return NaN ? 0 : res; //returns 0 to _currentValue if text is not a number, or returns new value otherwise
        }

        public decimal Sqrt(decimal a)
        {
            return (decimal)Math.Sqrt((double)a);
        }

        public decimal Power(decimal a)
        {
            return (decimal)Math.Pow((double)a, 2);
        }

        public decimal FractionInverse(decimal a)
        {
            if (a != 0)
            {
                return 1 / a;
            }
            else
            {
                Display.Text = "Can't divide by 0";
                throw new NoZeroDivisionException();
            }
        }

        public decimal Subtract(decimal a, decimal b)
        {
            return a - b;
        }

        public decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }

        public decimal Divide(decimal a, decimal b)
        {
            if (a != 0)
            {
                return a / b;
            }
            else
            {
                Display.Text = "Can't divide by 0";
                throw new NoZeroDivisionException();
            }
        }

        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

    }
}

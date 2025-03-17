using Calculator;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private decimal _currentValue = 0;
    private decimal _previousValue = 0;
    private TextBlock _history;
    private string _operation = string.Empty;
    private char COMMA = ',';

    public MainWindow()
    {
        InitializeComponent();
        _history = (TextBlock?)FindName("History");
        LoadHistory();
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (_operation == "eq")
        {
            //clear screen if last operation was equals, type in fresh number
            Display.Text = "";
            _operation = string.Empty;
        }
        Button button = sender as Button;
        if (button != null)
        {
            string content = button.Content.ToString();
            string dataContent = (button.DataContext ?? string.Empty).ToString();
            if (content == "+" || content == "-" || content == "*" || content == "/")
            {
                _previousValue = _currentValue;
                _currentValue = 0;
                _operation = content;
                Display.Text = string.Empty;
            }
            else if (dataContent == "frac" || dataContent == "pow" || dataContent == "sqrt" || dataContent == "back")
            {
                _previousValue = _currentValue;
                _operation = dataContent;
                InPlaceOperation();
            }
            else
            {
                if (!(content == COMMA.ToString() && Display.Text.IndexOf(COMMA) > -1))
                {
                    Display.Text = RemoveZeros(Display.Text + content);
                    _currentValue = decimal.Parse(Display.Text);
                }
            }
        }
    }

    private void Equals_Click(object sender, RoutedEventArgs e)
    {
        if (!decimal.TryParse(Display.Text, out _)) return; // if current display is ending with comma, do nothing
        try
        {
            _currentValue = _operation switch
            {
                string o when o == "+" => Add(_previousValue, _currentValue),
                string o when o == "-" => Subtract(_previousValue, _currentValue),
                string o when o == "*" => Multiply(_previousValue, _currentValue),
                string o when o == "/" => Divide(_previousValue, _currentValue),
                _ => _currentValue,
            };
            Display.Text = RemoveZeros(_currentValue.ToString());
            SaveOperation();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _operation = "eq";
        }
    }
    private void InPlaceOperation()
    {
        try
        {
            _currentValue = _operation switch
            {
                string o when o == "frac" => FractionInverse(_currentValue),
                string o when o == "pow" => Power(_currentValue),
                string o when o == "sqrt" => Sqrt(_currentValue),
                string o when o == "back" => Backspace(),
                _ => throw new NotImplementedException(),
            };
            Display.Text = RemoveZeros(_currentValue.ToString());
            SaveOperation();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _operation = "eq";
        }
    }

    private void SaveOperation()
    {
        throw new NotImplementedException();
    }

    private void LoadHistory()
    {
        throw new NotImplementedException();
    }

    private string RemoveZeros(string s)
    {
        decimal val = 0;
        bool isdecimal = decimal.TryParse(s, out val);

        if (s == "0" || s == string.Empty || s == null) return s;
        if (s == COMMA.ToString()) return "0" + COMMA;
        if (s.IndexOf(COMMA) == 1 && s.StartsWith('0')) return s;
        if (val % 1 == 0) return s.TrimStart(['0', COMMA]);
        return s.TrimStart('0');
    }

    private decimal Backspace()
    {
        Display.Text = RemoveZeros(Display.Text.Remove(Display.Text.Length - 1));
        bool NaN = true;
        decimal res = 0;
        NaN = !decimal.TryParse(Display.Text, out res);
        return NaN ? 0 : res; //returns 0 to _currentValue if text is empty (not a number), or returns new value otherwise
    }

    private decimal Sqrt(decimal a)
    {
        return (decimal)Math.Sqrt((double)a);
    }

    private decimal Power(decimal a)
    {
        return (decimal)Math.Pow((double)a, 2);
    }

    private decimal FractionInverse(decimal a)
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

    private decimal Subtract(decimal a, decimal b)
    {
        return a - b;
    }

    private decimal Multiply(decimal a, decimal b)
    {
        return a * b;
    }

    private decimal Divide(decimal a, decimal b)
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

    private decimal Add(decimal a, decimal b)
    {
        return a + b;
    }

    private void On_KeyDown(object sender, KeyEventArgs e)
    {
        e.Handled = false;
    }
}
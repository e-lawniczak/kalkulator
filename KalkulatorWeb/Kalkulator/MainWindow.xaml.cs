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
using static Calculator.Helpers;
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
    private HistoryDb db;
    private Operations calc;

    public MainWindow()
    {
        db = new HistoryDb();
        InitializeComponent();
        calc = new Operations(ref Display);
        _history = (TextBlock)FindName("History");
        FillInitialData();
        FillHistory();
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
            var tmpPrev = _previousValue;
            var tmpCurrent = _currentValue;
            _currentValue = _operation switch
            {
                string o when o == "+" => calc.Add(_previousValue, _currentValue),
                string o when o == "-" => calc.Subtract(_previousValue, _currentValue),
                string o when o == "*" => calc.Multiply(_previousValue, _currentValue),
                string o when o == "/" => calc.Divide(_previousValue, _currentValue),
                _ => _currentValue,
            };
            _previousValue = tmpCurrent;
            CleanPrint();
            SaveOperation(tmpPrev);
            FillHistory();
            OnClickFinish();


        }
        catch (Exception)
        {
            throw;
        }

    }



    private void InPlaceOperation()
    {
        try
        {
            var tmpPrev = _previousValue;
            var tmpCurrent = _currentValue;

            _currentValue = _operation switch
            {
                string o when o == "frac" => calc.FractionInverse(_currentValue),
                string o when o == "pow" => calc.Power(_currentValue),
                string o when o == "sqrt" => calc.Sqrt(_currentValue),
                string o when o == "back" => calc.Backspace(),
                _ => throw new NotImplementedException(),
            };
            _previousValue = tmpCurrent;
            CleanPrint();

            if (_operation != "back")
            {
                SaveOperation();
                FillHistory();
                OnClickFinish();

            }
        }
        catch (Exception)
        {
            throw;
        }

    }

    private void OnClickFinish()
    {
        _operation = "eq";
    }

    private async void SaveOperation(decimal? tmpPrev = null)
    {
        if (!string.IsNullOrEmpty(_operation) && !_operation.Equals("eq"))
            await db.SaveOperation(tmpPrev, _previousValue, _operation, _currentValue);
    }

    private void CleanPrint()
    {
        Display.Text = RemoveZeros(_currentValue.ToString());

    }

    private async void FillHistory()
    {
        _history.Text = db.LoadHistory().Result;
    }
    private async void FillInitialData()
    {
        HistoryDb db = new();
        await db.FillData();
    }




}
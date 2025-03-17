
namespace Calculator
{
    [Serializable]
    internal class NoZeroDivisionException : Exception
    {
        public NoZeroDivisionException()
        {
        }

        public NoZeroDivisionException(string? message) : base(message)
        {
        }

        public NoZeroDivisionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
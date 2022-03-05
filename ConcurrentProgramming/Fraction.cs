namespace ConcurrentProgramming
{
    public class Fraction
    {
        private readonly int _dividend;
        private readonly int _divisor;

        public Fraction(int dividend, int divisor)
        {
            _dividend = dividend;
            _divisor = divisor;
        }

        public double Evaluate() => (double)_dividend / _divisor;
        public override string ToString() => $"{_dividend}/{_divisor}";
    }

    public class Program
    {
        static void Main()
        {
            System.Console.WriteLine("Simple fraction evaluator!");
        }
    }
}

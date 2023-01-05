namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Expected 1 parameter with file name. You provided none or more.");
                return;
            }
            var fileName = args[0];
            var calls = new CallLogReader().ReadCalls(fileName);
            var cost = new CostCalculator().CalculateCost(calls);
            Console.WriteLine($"Your cost is {cost}.");
        }
    }
}
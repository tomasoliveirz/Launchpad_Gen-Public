using Moongy.RD.Launchpad.CodeGenerator.Engine.Test;

class Program
{
    static async Task Main(string[] args)
    {
        await TestRunner.RunTestAsync();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
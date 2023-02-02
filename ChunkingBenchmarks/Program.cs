using BenchmarkDotNet.Running;

try
{
    BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
        .Run(args);

    return 0;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
    return 1;
}

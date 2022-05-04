using Tokens;

Console.WriteLine("Введите анализируемую строку. Для окончания ввода введите stop.\n");
string input = Console.ReadLine();
Analyzer analyzer = new Analyzer();

do
{
    try
    {
        TokenNode tree = analyzer.Analyze(input);
        double result = Analyzer.ComputeTree(tree);
        Console.WriteLine("Input: " + input + "\nTree: " + tree + "\nResult: " + result + "\n\nВведите строку: ");
        input = Console.ReadLine();
    } 
    catch (ArgumentException exc)
    {
        Console.WriteLine(exc.Message);
        input = Console.ReadLine();
    }
}
while (!input.Equals("stop"));

namespace ProgrammingApp;

internal class ProgrammingApp
{
    private static ProgrammingApp? _instance;

    private Program _loadedProgram;

    private ProgrammingApp() {}

    static void Main(string[] args)
    {
        ProgrammingApp.GetInstance().Run();
    }

    public void Run()
    {
        bool wantToExit = false;

        while (!wantToExit)
        {
            string[] input = Console.ReadLine().Split(' ');

            switch (input[0])
            {
                case "exit":
                    wantToExit = true;
                    break;
                case "load":
                    switch (input[1])
                    {
                        case "basic":
                            _loadedProgram = Program.BasicProgram;
                            break;
                        case "advanced":
                            _loadedProgram = Program.AdvancedProgram;
                            break;
                        case "expert":
                            _loadedProgram = Program.ExpertProgram;
                            break;
                        default:
                            Console.WriteLine("Invalid program name");
                            break;
                    }
                    break;
                case "run":
                    Board board = new();
                    _loadedProgram.Run(board);
                    Console.WriteLine($"End state {board}");
                    break;
            }
        }
    }

    public static ProgrammingApp GetInstance()
    {
        return _instance ??= new ProgrammingApp();
    }
}
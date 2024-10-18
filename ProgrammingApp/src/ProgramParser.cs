namespace ProgrammingApp
{
    internal class ProgramParser
    {
        private static StreamReader sr;
        private static string line;
        public static Program ParseFromTextFile(string fileName)
        {
            sr = new StreamReader(fileName);
            List<ICommand> commands = new List<ICommand>();
            line = sr.ReadLine();
            while (line != null)
            {
                string[] command = line.Split(' ');
                switch (command[0])
                {
                    case "Move":
                        commands.Add(new MoveCommand(int.Parse(command[1])));
                        break;
                    case "Turn":
                        commands.Add(new TurnCommand(command[1]));
                        break;
                    case "Repeat":
                        commands.AddRange(RepeatCommand(int.Parse(command[1]), int.Parse(command[2])));
                        break;
                    
                }
                line = sr.ReadLine();
            }
            return new Program(commands);
        }

        private static List<ICommand> RepeatCommand(int commandCount, int repeatAmount)
        {
            List<ICommand> commands = new List<ICommand>();
            for (int i = 0; i < commandCount; i++)
            {
                line = sr.ReadLine();
                string[] command = line.Split(' ');
                switch (command[0])
                {
                    case "Move":
                        commands.Add(new MoveCommand(int.Parse(command[1])));
                        break;
                    case "Turn":
                        commands.Add(new TurnCommand(command[1]));
                        break;
                    case "Repeat":
                        commands.AddRange(RepeatCommand(int.Parse(command[1]), int.Parse(command[2])));
                        break;
                }

            }
            List<ICommand> result = new List<ICommand>();
            for (int i = 0; i < repeatAmount; i++)
                result.AddRange(commands);
            return result;
        }
    }
}

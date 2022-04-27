using WebSocketSharp.Server;
class Program
{
    public bool Connected = false;
    public LSO LSO;
    const string _address = "ws://localhost";
    const ConsoleKey _resetKey = ConsoleKey.R;
    readonly Game _game;
    readonly Splitter _splitter;
    readonly WebSocketServer _server = new(_address);

    static void Main()
    {
        Console.Title = "TUNIC LiveSplit One AutoSplitter";
        Console.SetWindowSize(85, 6);
        Console.SetBufferSize(85, 6);

        string path = @$"{Directory.GetCurrentDirectory()}\\splits.txt";

        if (File.Exists(path))
            new Program(System.IO.File.ReadAllLines(path));
        else
        {
            Console.Clear();
            Console.WriteLine("splits.txt not found");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }

    Program(string[] splits)
    {
        var ui = new UI(_address, _resetKey.ToString());
        
        _game = new(ui);
        _splitter = new(splits, _game);

        _server.Start();
        _server.AddWebSocketService<LSO>("/", (LSO lso) => {
            lso.Program = this;
            lso.UI = ui;
        });

        Loop();
    }

    void Loop()
    {
        const ConsoleKey exitKey = ConsoleKey.Enter;
        ConsoleKey inputKey = default;
        
        do
        {
            _game.Check();

            if (Connected)
                foreach (string command in _splitter.GetCommands())
                    LSO.Send(command);

            if (!Console.KeyAvailable)
                continue;
            
            inputKey = Console.ReadKey(true).Key;

            if (Connected && inputKey == _resetKey)
                _splitter.ResetPressed = true;
        } while (inputKey != exitKey);

        _server.Stop();
    }
}
class Splitter
{
    public bool ResetPressed = false;
    readonly Game _game;
    bool _runStarted = false;
    Split _split;
    public Splitter(string[] splits, Game game)
    {
        _game = game;

        Split split = new(() => _game.Completed, isLast: true);
        var lastSplit = split;

        foreach (string name in splits.Reverse())
        {
            int id;
            Func<bool> passed = int.TryParse(name, out id) ?
                () => game.LoadedScene(id) :
                () => game.CrossedEvent(name);
            
            split = new(passed, split);
        }

        lastSplit.Next = _split = split;
    }

    public string[] GetCommands()
    {
        if (ResetPressed)
        {
            ResetPressed = _runStarted = false;

            return new [] { "reset" };
        }

        if (!_game.Hooked)
            return new string[0];

        if (!_runStarted)
        {
            if (!_game.Started)
                return new string[0];
            
            _runStarted = true;
            
            return new [] { "reset", "start", "initgametime" };
        }

        var igtCommand = $"setgametime {TimeSpan.FromSeconds(_game.Time)}";

        if (!_split.Passed())
            return new [] { igtCommand };

        if (_split.IsLast)
            _runStarted = false;

        _split = _split.Next;

        return new [] { igtCommand, "split" };
    }
}
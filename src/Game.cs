using MTK;
using MTK.MemUtils;
using MTK.MemUtils.Pointers;

class Game
{
    public bool Hooked = false;
    Pointer<float> _inGameTime;
    Pointer<bool> _gameComplete;
    Pointer<int> _lastScene;
    StringPointer _lastEvent;
    readonly MemoryManager _memory;
    public Game(UI ui)
    {
        _memory = new("TUNIC", "Secret Legend")
        {
            OnHook = () =>
            {
                InitPointers();
                
                Hooked = ui.GameHooked = true;
            },
            OnExit = () => Hooked = ui.GameHooked = false
        };
    }

    public void Check()
    {
        if (_memory.IsHooked)
            _memory.TickUp();
    }

    void InitPointers()
    {
        var data = new Pointer<IntPtr>(_memory, "GameAssembly.dll", 0x11FFCA0, 0xB8);
        
        _inGameTime = new(data, 0x8) { UpdateOnNullPointer = false };
        _gameComplete = new(data, 0x14) { UpdateOnNullPointer = false };
        _lastEvent = new(data, 0x18, 0x14) { StringType = StringType.UTF16Sized, UpdateOnNullPointer = false };
        _lastScene = new(data, 0x10) { UpdateOnNullPointer = false };
    }

    public bool Started
    {
        get => _inGameTime.Old == 0f && _inGameTime.Current > 0f && _inGameTime.Current < 5f;
    }

    public float Time
    {
        get => _inGameTime.Current;
    }

    public bool Completed
    {
        get => _gameComplete.Current;
    }

    public bool LoadedScene(int id)
    {
        return id == _lastScene.Current;
    }

    public bool CrossedEvent(string name)
    {
        return _lastEvent.Current?.StartsWith(name) ?? false;
    }
}
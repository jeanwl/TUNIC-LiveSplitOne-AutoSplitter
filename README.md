# TUNIC LiveSplit One AutoSplitter

Livesplit One AutoSplitter for the game TUNIC with customizable splits

![Preview](/preview.gif)

# Description

This program allows TUNIC autosplitting on LiveSplit One Browser, which is CSS customizable unlike original LiveSplit.

I suggest using [LiveSplit One OBS Layout](https://github.com/jeanwll/LiveSplitOne-OBS-Layout) which was made alongside this project.

# Usage

1. Edit splits.txt
    - Each line is a split condition, either an [area number](https://github.com/jeanwll/TUNIC-LiveSplitOne-AutoSplitter/blob/main/Areas.md) or an [event](https://github.com/jeanwll/TUNIC-LiveSplitOne-AutoSplitter/blob/main/Events.md)
    - Order them accordingly to your splits order

2. Start AutoSplitter.exe

3. Interact OBS browser source
    - Select Compare Against: "Game Time"
    - Connect to server "ws://localhost" (should be in clipboard)

___
Credit to [Ero](https://github.com/just-ero) for his [MTK Library](https://github.com/just-ero/MemoryToolKit) and overall guidance.

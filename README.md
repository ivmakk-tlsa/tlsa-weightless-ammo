# Weightless Ammo

A mod for [*The Last Stand: Aftermath*](https://www.nexusmods.com/thelaststandaftermath) that makes ammo weigh 0. Grab as much as you want and it never counts against your encumbrance. Only ammo changes, so everything else keeps its normal weight.

Good for loot goblins who scoop up every box, and for stocking up before a heavy fight where every bullet counts.

The mod zeroes the weight at its source, so both the encumbrance total and the inventory UI read 0.

## Install

1. Install [BepInEx 6 (IL2CPP)](https://www.nexusmods.com/thelaststandaftermath/mods/1) for The Last Stand: Aftermath. Start the game once so BepInEx finishes setup, then quit.
2. Extract this mod's zip into the game folder (the folder with the game .exe). The DLL lands in `BepInEx\plugins`. Full path examples:
   - Steam: `C:\Program Files (x86)\Steam\steamapps\common\The Last Stand Aftermath\BepInEx\plugins\WeightlessAmmo.dll`
   - Epic: `C:\Program Files\Epic Games\The Last Stand Aftermath\BepInEx\plugins\WeightlessAmmo.dll`
3. Start the game. Ammo now weighs 0 in your inventory.

Not working? Open `BepInEx\LogOutput.log` and look for the `Weightless Ammo loaded` line.

## Uninstall

Delete `WeightlessAmmo.dll` from the `BepInEx\plugins` folder.

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against the game's IL2CPP interop assemblies, so a working game install with BepInEx 6 set up is required. Those assemblies are game-derived and are not part of this repo.

```
dotnet build src/WeightlessAmmo.csproj -c Release
```

`Directory.Build.props` sets `GameDir` to the default Steam install path. If the game lives elsewhere, override it without editing the file: set a `GameDir` environment variable, or pass `-p:GameDir=...` on the build. The output DLL is at `src\bin\Release\WeightlessAmmo.dll`.

## Package

Add `-p:Package=true` to a Release build to also produce the ready-to-install zip at `dist\WeightlessAmmo-<version>.zip`, laid out as `BepInEx\plugins\WeightlessAmmo.dll` so a user extracts it at the game root. A plain build skips this step.

```
dotnet build src/WeightlessAmmo.csproj -c Release -p:Package=true
```

## License

Licensed under the GNU General Public License v3.0. Copyright (C) 2026 ivmakk. See [LICENSE](LICENSE).

You may reuse and modify this mod, but you must keep it open under the same license and give credit. Do not reupload it without credit.

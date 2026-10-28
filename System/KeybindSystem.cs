using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.UI;

namespace NohitterLeaderboard.System;

public class KeybindSystem : ModSystem
{
    public static ModKeybind ToggleLeaderboardKey { get; private set; }

    public override void Load()
    {
        ToggleLeaderboardKey = KeybindLoader.RegisterKeybind(Mod, "Toggle leaderboard", "L");
    }

    public override void Unload()
    {
        ToggleLeaderboardKey = null;
    }
}

public class KeybindPlayer : ModPlayer
{
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (KeybindSystem.ToggleLeaderboardKey.JustPressed)
        {
            ModContent.GetInstance<UISystem>().ToggleUI(out UIState uIState);

            if (uIState == null) return;
            
            ApiClient.SendRequest();
        }
    }
}
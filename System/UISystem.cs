using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using NohitterLeaderboard.UI;

namespace NohitterLeaderboard.System;

public class UISystem : ModSystem
{
    private UserInterface userInterface;
    private LeaderboardUI leaderboardUI;

    public override void Load()
    {
        if (!Main.dedServ)
        {
            leaderboardUI = LeaderboardUI.Instance;
            leaderboardUI.Activate();

            userInterface = new UserInterface();
        }
    }

    public override void UpdateUI(GameTime gameTime)
    {
        if (userInterface?.CurrentState != null)
        {
            userInterface.Update(gameTime);
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("NohitterLeaderboard: UI", () =>
            {
                if (userInterface?.CurrentState != null)
                {
                    userInterface.Draw(Main.spriteBatch, new GameTime());
                }
                return true;
            },
            InterfaceScaleType.UI));
        }
    }

    public void ToggleUI(out UIState uIState)
    {
        if (userInterface.CurrentState == null)
        {
            userInterface.SetState(leaderboardUI);
        }
        else
        {
            userInterface.SetState(null);
        }
        uIState = userInterface.CurrentState;
    }
}
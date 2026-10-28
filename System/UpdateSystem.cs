using NohitterLeaderboard.UI;
using Terraria.ModLoader;

namespace NohitterLeaderboard.System;

public class UpdateSystem : ModSystem
{
    public override void PostUpdateEverything()
    {
        if (ApiClient.IsResponseReceived)
        {
            LeaderboardUI.Instance.OnDeactivate();
            LeaderboardUI.Instance.OnActivate();
            ApiClient.IsResponseReceived = false;
        }
    }
}

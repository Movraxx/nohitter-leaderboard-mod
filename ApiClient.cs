using System;
using System.Collections.Generic;
using System.Net.Http;
using NohitterLeaderboard.Responces;
using Terraria;
using System.Text.Json;
using NohitterLeaderboard.UI;

namespace NohitterLeaderboard;

public class ApiClient
{
    public static bool IsResponseReceived { get; set; } = false;
    private static HttpClient httpClient;

    public static async void SendRequest()
    {
        try
        {
            httpClient = new();

            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5217/api/public/players/top");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            LeaderboardUI.Players = JsonSerializer.Deserialize<List<PlayerResponce>>(json);

            IsResponseReceived = true;
        }
        catch (Exception e)
        {
            Main.QueueMainThreadAction(() =>
            {
                Main.NewText(e.Message, new Microsoft.Xna.Framework.Color(255, 0, 0));
            });
        }
    }
}
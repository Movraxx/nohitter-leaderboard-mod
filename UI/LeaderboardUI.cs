using System.Collections.Generic;
using NohitterLeaderboard.Responces;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader.UI;

namespace NohitterLeaderboard.UI;

public class LeaderboardUI : UIState
{
    public static LeaderboardUI Instance { get; } = new LeaderboardUI();
    public static List<PlayerResponce> Players { get; set; } = [];

    private UIPanel mainPanel;
    private UIText titleText;
    private UIList uIList;
    private UIScrollbar scrollbar;
    private UIButton<char> updateButton;
    public override void OnActivate()
    {
        mainPanel = new();
        mainPanel.Width.Set(800f, 0f);
        mainPanel.Height.Set(900f, 0f);
        mainPanel.HAlign = 0.5f;
        mainPanel.VAlign = 0.5f;

        titleText = new("Nohitter Leaderboard", large: true);
        titleText.Width.Set(400f, 0f);
        titleText.Height.Set(100f, 0f);
        titleText.HAlign = 0.5f;
        titleText.VAlign = 0.02f;
        mainPanel.Append(titleText);

        uIList = [];
        uIList.Top.Set(80f, 0f);
        uIList.Width.Set(-30f, 1f);
        uIList.Height.Set(-80f, 1f);
        uIList.ListPadding = 10f;
        uIList.ManualSortMethod = list => {};
        mainPanel.Append(uIList);

        scrollbar = new();
        scrollbar.Top.Set(80f, 0f);
        scrollbar.Width.Set(25f, 0f);
        scrollbar.Height.Set(-80f, 1f);
        scrollbar.HAlign = 1f;
        mainPanel.Append(scrollbar);
        uIList.SetScrollbar(scrollbar);

        updateButton = new('⟳', large: true);
        updateButton.Top.Set(10f, 0f);
        updateButton.Left.Set(-45f, 1f);
        updateButton.Width.Set(45f, 0f);
        updateButton.Height.Set(45f, 0f);
        updateButton.HoverText = ' ';
        updateButton.OnLeftClick += OnClick;
        mainPanel.Append(updateButton);

        Append(mainPanel);
        PopulateTable();
    }

    public override void OnDeactivate()
    {
        RemoveAllChildren();
    }

    private void OnClick(UIMouseEvent evt, UIElement element) => ApiClient.SendRequest();

    private void PopulateTable()
    {
        uIList.Clear();

        for (int i = 0; i < Players.Count; i++)
        {
            UIPanel row = new();
            row.Width.Set(0f, 1f);
            row.Height.Set(40f, 0f);

            UIText rankText = new($"{i + 1}");
            rankText.Left.Set(8f, 0f);
            rankText.VAlign = 0.5f;
            row.Append(rankText);

            UIText nameText = new(Players[i].DisplayName);
            nameText.Left.Set(40f, 0f);
            nameText.VAlign = 0.5f;
            row.Append(nameText);

            UIText scoreText = new(Players[i].Score.ToString());
            scoreText.Left.Set(-20f, 0f);
            scoreText.HAlign = 1f;
            scoreText.VAlign = 0.5f;
            row.Append(scoreText);
        
            uIList.Add(row);
        }
    }
}
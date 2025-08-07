using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using ReSanctuary.Tabs;

namespace ReSanctuary.Windows;

public class MainWindow : Window {
    private Plugin plugin;
    private List<MainWindowTab> tabs;

    public MainWindow(Plugin plugin) : base("ReSanctuary") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(300, 300) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        Size = new Vector2(600, 400);
        SizeCondition = ImGuiCond.FirstUseEver;

        this.plugin = plugin;
        this.tabs = new List<MainWindowTab> {
            new GatheringTab(this.plugin),
            new WorkshopTab(this.plugin),
            new CreatureTab(this.plugin),
            new TodoTab(this.plugin),
            new SettingsTab(this.plugin),
            new AboutTab(this.plugin)
        };
    }

    public void Dispose() {
        this.tabs.Clear();
    }

    public override void Draw() {
        if (ImGui.BeginTabBar("##ReSanctuary_MainWindowTabs", ImGuiTabBarFlags.None)) {
            foreach (var tab in tabs) {
                if (ImGui.BeginTabItem(tab.Name)) {
                    tab.Draw();
                    ImGui.EndTabItem();
                }
            }

            ImGui.EndTabBar();
        }
    }
}

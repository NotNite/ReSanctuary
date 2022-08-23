using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Utility;
using ImGuiNET;
using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Windows;

public class MainWindow : Window, IDisposable {
    private Plugin Plugin;
    private List<GatheringItem> items;

    public MainWindow(Plugin plugin) : base("ReSanctuary") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        Plugin = plugin;
        items = Utils.GetSortedItems();
    }

    public void Dispose() { }

    private void DrawItemTab() {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit;

        if (ImGui.BeginTable("ReSanctuary_MainWindowTable", 3, tableFlags)) {
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Buttons");
            ImGui.TableHeadersRow();

            foreach (var item in items) {
                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                var icon = item.Icon;
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(icon.ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

                ImGui.TableSetColumnIndex(1);
                ImGui.Text(item.Name);

                ImGui.TableSetColumnIndex(2);
                if (ImGui.Button("Show on map##ReSanctuary_ShowOnMap_" + item.ItemID)) {
                    var teriTypeSheet = Plugin.DataManager.Excel.GetSheet<TerritoryType>();
                    var islandSanctuary = teriTypeSheet.First(x => x.Name == "h1m2");
                    var teri = islandSanctuary.RowId;

                    PluginLog.Log("radius: {radius}", item.Radius);

                    Utils.OpenGatheringMarker(teri, item.X, item.Y, item.Radius, item.Name);
                }
            }

            ImGui.EndTable();
        }
    }

    private void DrawAboutTab() {
        ImGui.Text("ReSanctuary, made by NotNite.");
        ImGui.Text("If you like my work, please consider supporting me financially via GitHub Sponsors!");
        
        if (ImGui.Button("View GitHub Page")) {
            Process.Start(new ProcessStartInfo {
                FileName = "https://github.com/NotNite/ReSanctuary",
                UseShellExecute = true
            });
        }
        
        ImGui.SameLine();
        
        if (ImGui.Button("Open GitHub Sponsors")) {
            Process.Start(new ProcessStartInfo {
                FileName = "https://notnite.com/givememoney",
                UseShellExecute = true
            });
        }
    }

    public override void Draw() {
        if (ImGui.BeginTabBar("##ReSanctuary_MainWindowTabs", ImGuiTabBarFlags.None)) {
            if (ImGui.BeginTabItem("Items")) {
                DrawItemTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("About")) {
                DrawAboutTab();
                ImGui.EndTabItem();
            }
        }
    }
}

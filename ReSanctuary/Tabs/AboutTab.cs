using System.Diagnostics;
using Dalamud.Bindings.ImGui;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs;

public class AboutTab : MainWindowTab {
    public AboutTab(Plugin plugin) : base(plugin, "About") { }

    public override void Draw() {
        ImGui.Text("ReSanctuary, made by NotNite.");
        ImGui.Text("If you like my work, please consider supporting me financially via GitHub Sponsors!");

        if (ImGui.Button("View GitHub Page"))
            Process.Start(new ProcessStartInfo {
                FileName = "https://github.com/NotNite/ReSanctuary",
                UseShellExecute = true
            });

        ImGui.SameLine();

        if (ImGui.Button("Open GitHub Sponsors"))
            Process.Start(new ProcessStartInfo {
                FileName = "https://notnite.com/givememoney",
                UseShellExecute = true
            });
    }
}

using ImGuiNET;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs; 

public class SettingsTab : MainWindowTab {
    public SettingsTab(Plugin plugin) : base(plugin, "Settings") { }

    public override void Draw() {
        var config = this.Plugin.Configuration;
        var lockWidget = config.LockWidget;
        if (ImGui.Checkbox("Lock widget", ref lockWidget)) {
            config.LockWidget = lockWidget;
            config.Save();
        }
    }
}

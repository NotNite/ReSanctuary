using System;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace ReSanctuary.Windows;

public class ConfigWindow : Window, IDisposable {
    private Configuration Configuration;

    private const ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse |
                                                 ImGuiWindowFlags.NoScrollbar |
                                                 ImGuiWindowFlags.NoScrollWithMouse;

    public ConfigWindow(Plugin plugin) : base("ReSanctuary Config", WindowFlags) {
        Size = new Vector2(300, 75) * ImGuiHelpers.GlobalScale;
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw() {
        var lockWidget = Configuration.LockWidget;
        if (ImGui.Checkbox("Lock widget", ref lockWidget)) {
            Configuration.LockWidget = lockWidget;
            Configuration.Save();
        }
    }
}

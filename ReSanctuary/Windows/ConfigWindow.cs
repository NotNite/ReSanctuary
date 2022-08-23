using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace ReSanctuary.Windows;

public class ConfigWindow : Window, IDisposable {
    private Configuration Configuration;

    private const ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse |
                                                 ImGuiWindowFlags.NoScrollbar |
                                                 ImGuiWindowFlags.NoScrollWithMouse;

    public ConfigWindow(Plugin plugin) : base("A Wonderful Configuration Window", WindowFlags) {
        Size = new Vector2(232, 75);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw() {
        // can't ref a property, so use a local copy
        var configValue = Configuration.SomePropertyToBeSavedAndWithADefault;
        if (ImGui.Checkbox("Random Config Bool", ref configValue)) {
            Configuration.SomePropertyToBeSavedAndWithADefault = configValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            Configuration.Save();
        }
    }
}

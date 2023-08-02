using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Windows;

public class WidgetWindow : Window {
    private readonly Plugin plugin;

    public WidgetWindow(Plugin plugin) : base("ReSanctuary Widget") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(150, 75) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.plugin = plugin;
    }

    public override void PreDraw() {
        if (this.plugin.Configuration.LockWidget) {
            Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove |
                    ImGuiWindowFlags.NoTitleBar;
            RespectCloseHotkey = false;
        } else {
            Flags = ImGuiWindowFlags.None;
            RespectCloseHotkey = true;
        }
    }

    private void DrawGarbage(int amount, Item item, int has) {
        var iconSize = ImGui.GetTextLineHeight() * 1.25f;
        var iconSizeVec = new Vector2(iconSize, iconSize);
        ImGui.Image(Utils.GetFromIconCache(item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

        ImGui.SameLine();

        if (has >= amount) ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
        if (ImGui.Selectable($"{item.Name} - {has}/{amount}")) {
            var mat = Utils.GetSortedGatheringItems().Find(x => x.ItemId == item.RowId);
            if (mat != null) {
                var teri = this.plugin.IslandSanctuary.RowId;
                Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
            }
        }

        if (has >= amount) ImGui.PopStyleColor();
    }

    public override void Draw() {
        var todoList = this.plugin.Configuration.TodoList;

        // i just got home from class and i am exhausted. here is some very bad unoptimized code\
        foreach (var (id, amount) in todoList) {
            var itemPouchItem = this.plugin.MJIItemPouchSheet.GetRow(id)!;
            var item = this.plugin.ItemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0))!;
            var has = Utils.GetStackSize(item.RowId);
            if (has < amount) DrawGarbage(amount, item, has);
        }

        foreach (var (id, amount) in todoList) {
            var itemPouchItem = this.plugin.MJIItemPouchSheet.GetRow(id)!;
            var item = this.plugin.ItemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0))!;
            var has = Utils.GetStackSize(item.RowId);
            if (has >= amount) DrawGarbage(amount, item, has);
        }
    }
}

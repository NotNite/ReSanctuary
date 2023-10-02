using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Utility;
using Dalamud.Logging;
using ImGuiNET;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs;

public class TodoTab : MainWindowTab {
    public TodoTab(Plugin plugin) : base(plugin, "Todo") { }

    public override void Draw() {
        var todoList = Plugin.Configuration.TodoList;

        if (ImGui.Button("Open Todo Widget")) this.Plugin.WidgetWindow.IsOpen = true;

        // ReSharper disable once InconsistentNaming
        foreach (var (id, _amount) in todoList) {
            var amount = _amount;

            var itemPouchRow = this.Plugin.MJIItemPouchSheet.GetRow(id)!;
            var itemId = itemPouchRow.ReadColumn<uint>(0);
            var item = this.Plugin.ItemSheet.GetRow(itemId)!;
            Plugin.PluginLog.Debug($"id: {id}, itemId: {itemId}, icon: {item.Icon}o");

            var iconSize = ImGui.GetTextLineHeight() * 1.25f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(Plugin.TextureProvider.GetIcon(item.Icon)!.ImGuiHandle,
                        iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.PushItemWidth(100 * ImGuiHelpers.GlobalScale);
            ImGui.SameLine();
            if (ImGui.InputInt($"##ReSanctuary_TodoList_{id}", ref amount, 1, 2,
                               ImGuiInputTextFlags.EnterReturnsTrue)) {
                if (amount > 0) {
                    todoList[id] = amount;
                } else {
                    todoList.Remove(id);
                }

                Plugin.Configuration.TodoList = todoList;
                Plugin.Configuration.Save();
            }

            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushFont(UiBuilder.IconFont);
            var trashIcon = FontAwesomeIcon.Trash.ToIconString();
            if (ImGui.Button(trashIcon + $"##ReSanctuary_TodoListTrash_{id}")) {
                todoList.Remove(id);
            }

            ImGui.PopFont();

            ImGui.SameLine();
            ImGui.Text(item.Name);
        }
    }
}

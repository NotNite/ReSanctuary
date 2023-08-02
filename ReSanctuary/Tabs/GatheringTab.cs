using System.Linq;
using System.Numerics;
using Dalamud.Logging;
using ImGuiNET;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs;

public class GatheringTab : MainWindowTab {
    private string filter = string.Empty;

    public GatheringTab(Plugin plugin) : base(plugin, "Gathering") { }

    public override void Draw() {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit;

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
        ImGui.InputText(string.Empty, ref filter, 256);

        if (ImGui.BeginTable("ReSanctuary_MainWindowTable", 4, tableFlags)) {
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Buttons");
            ImGui.TableSetupColumn("Required Tool");
            ImGui.TableHeadersRow();

            foreach (var item in this.Plugin.GatheringItems) {
                var reqToolString = item.RequiredTool != null ? item.RequiredTool.Name : "None";

                if (!item.Name.ToLower().Contains(this.filter.ToLower())
                    && !reqToolString.ToLower().Contains(this.filter.ToLower())) continue;

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(Utils.GetFromIconCache(item.Item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

                ImGui.TableSetColumnIndex(1);
                ImGui.Text(item.Name);

                ImGui.TableSetColumnIndex(2);
                if (ImGui.Button("Show on map##ReSanctuary_ShowOnMap_" + item.ItemId)) {
                    var teri = Plugin.IslandSanctuary.RowId;
                    PluginLog.Debug("radius: {radius}", item.Radius);
                    Utils.OpenGatheringMarker(teri, item.X, item.Y, item.Radius, item.Name, item.Item.Icon);
                }

                ImGui.SameLine();

                if (ImGui.Button("Add to todo list##ReSanctuary_GatheringAddTodo_" + item.Item)) {
                    var rowId = this.Plugin.MJIItemPouchSheet.First(x => {
                        var itemId = x.ReadColumn<uint>(0);
                        var pouchItem = this.Plugin.ItemSheet.GetRow(itemId);
                        if (itemId == 0 || pouchItem == null) return false;
                        return pouchItem.RowId == item.ItemId;
                    }).RowId;
                    
                    Utils.AddToTodoList(Plugin.Configuration, rowId);
                }

                ImGui.TableSetColumnIndex(3);
                ImGui.Text(reqToolString);
            }

            ImGui.EndTable();
        }
    }
}

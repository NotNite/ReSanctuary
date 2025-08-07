using System.Numerics;
using Dalamud.Interface.Textures;
using Dalamud.Bindings.ImGui;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs;

public class WorkshopTab : MainWindowTab {
    private string filter = string.Empty;
    private int selectedItem;

    public WorkshopTab(Plugin plugin) : base(plugin, "Workshop") { }

    public override void Draw() {
        var contentRegionAvail = ImGui.GetContentRegionAvail();

        {
            var imguiSucks = ImGui.CalcTextSize("Isleworks ImGui Sucks Dickballs Lmao").X;
            var childSize = contentRegionAvail with {X = imguiSucks};
            ImGui.BeginChild("ReSanctuary_WorkshopListSearchChild", childSize);

            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
            ImGui.InputText("##ReSanctuary_WorkshopListSearchInput", ref this.filter, 256);

            if (ImGui.BeginListBox("##ReSanctuary_WorkshopList", ImGui.GetContentRegionAvail())) {
                for (var i = 0; i < this.Plugin.WorkshopItems.Count; i++) {
                    var item = this.Plugin.WorkshopItems[i];

                    if (item.Name.ToLower().Contains(this.filter.ToLower())) {
                        var selected = i == this.selectedItem;

                        if (ImGui.Selectable(item.Name, selected)) this.selectedItem = i;
                        if (selected) ImGui.SetItemDefaultFocus();
                    }
                }

                ImGui.EndListBox();
            }

            ImGui.EndChild();
        }

        ImGui.SameLine();

        {
            ImGui.BeginChild("ReSanctuary_WorkshopListViewChild");

            var item = this.Plugin.WorkshopItems[this.selectedItem];

            var iconSize = ImGui.GetTextLineHeight() * 2f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(
                Plugin.TextureProvider.GetFromGameIcon(new GameIconLookup(item.Item.Icon)).GetWrapOrEmpty().Handle,
                iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.SameLine();

            ImGui.Text($"{item.Name}\nDuration: {item.CraftingTime} hours");

            if (ImGui.Button("Add to todo list##ReSanctuary_WorkshopAddTodo_" + item.ItemId)) {
                foreach (var (requiredMat, matCount) in item.Materials) {
                    Utils.AddToTodoList(Plugin.Configuration, requiredMat, matCount);
                }

                this.Plugin.WidgetWindow.IsOpen = true;
            }

            ImGui.Text("Materials:");
            foreach (var (requiredMat, matCount) in item.Materials) {
                var itemPouchRow = this.Plugin.MJIItemPouchSheet.GetRow(requiredMat)!;
                var itemPouchItem = itemPouchRow.Item.Value;
                var mat = this.Plugin.GatheringItems.Find(x => x.ItemId == itemPouchItem.RowId);

                var name = itemPouchItem.Name;
                var text = $"{name} x{matCount}";

                if (ImGui.TreeNode(text)) {
                    var matIconSize = ImGui.GetTextLineHeight() * 2;
                    var matIconSizeVec = new Vector2(matIconSize, matIconSize);

                    if (mat != null) {
                        ImGui.Image(
                            Plugin.TextureProvider.GetFromGameIcon(new GameIconLookup(mat.Item.Icon)).GetWrapOrEmpty()
                                  .Handle, matIconSizeVec,
                            Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.TextWrapped(
                            $"Required tool: {(mat.RequiredTool != null ? mat.RequiredTool.Value.Name : "None")}");

                        if (ImGui.Button("Show on map##ReSanctuary_WorkshopShowOnMap_" + mat.ItemId)) {
                            var teri = Plugin.IslandSanctuary.RowId;

                            Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name, mat.Item.Icon);
                        }
                    } else {
                        ImGui.Image(Plugin.TextureProvider.GetFromGameIcon(new GameIconLookup(itemPouchItem.Icon)).GetWrapOrEmpty().Handle, matIconSizeVec,
                                    Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        if (this.Plugin.CreatureItemDrops.ContainsKey(itemPouchItem.RowId)) {
                            var drops = string.Join(
                                ", ", Utils.FindDropOnCreatures(itemPouchItem.RowId, this.Plugin.CreatureItems));
                            ImGui.TextWrapped("Drops from: " + drops);
                        } else {
                            ImGui.TextWrapped("No data available :(");
                        }
                    }

                    ImGui.TreePop();
                }
            }

            ImGui.EndChild();
        }
    }
}

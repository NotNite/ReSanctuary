using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Windows;

public class MainWindow : Window, IDisposable {
    private Plugin Plugin;

    private List<GatheringItem> gatheringItems;
    private List<WorkshopItem> workshopItems;
    private List<Creature.CreatureItem> creatureItems;

    private Dictionary<uint, Weather> weatherList;

    private string gatheringSearchFilter = string.Empty;
    private string workshopSearchFilter = string.Empty;
    private int workshopSearchSelected;
    private string creatureSearchFilter = string.Empty;

    private ExcelSheet<MJIItemPouch> itemPouchSheet;

    public MainWindow(Plugin plugin) : base("ReSanctuary") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(300, 300) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        Size = new Vector2(600, 400);
        SizeCondition = ImGuiCond.FirstUseEver;

        Plugin = plugin;
        gatheringItems = Utils.GetSortedGatheringItems();
        workshopItems = Utils.GetSortedWorkshopItems();
        creatureItems = Utils.GetCreatureItems();

        weatherList = Utils.GetISWeathers();

        itemPouchSheet = Plugin.DataManager.Excel.GetSheet<MJIItemPouch>();
        
    }

    public void Dispose() { }

    private void DrawGatheringTab() {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit;

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
        ImGui.InputText(string.Empty, ref gatheringSearchFilter, 256);

        if (ImGui.BeginTable("ReSanctuary_MainWindowTable", 4, tableFlags)) {
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Buttons");
            ImGui.TableSetupColumn("Required Tool");
            ImGui.TableHeadersRow();

            foreach (var item in gatheringItems) {
                if (!item.Name.ToLower().Contains(gatheringSearchFilter.ToLower())) continue;

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(Utils.IconCache(item.Item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

                ImGui.TableSetColumnIndex(1);
                ImGui.Text(item.Name);

                ImGui.TableSetColumnIndex(2);
                if (ImGui.Button("Show on map##ReSanctuary_ShowOnMap_" + item.ItemID)) {
                    var teri = Plugin.islandSanctuary.RowId;

                    PluginLog.Debug("radius: {radius}", item.Radius);

                    Utils.OpenGatheringMarker(teri, item.X, item.Y, item.Radius, item.Name);
                }
                
                ImGui.SameLine();

                if (ImGui.Button("Add to todo list##ReSanctuary_GatheringAddTodo_" + item.ItemID)) {
                    var rowID = itemPouchSheet.First(x => {
                        var itemValue = x.Item.Value;
                        if (itemValue == null) return false;
                        return itemValue.RowId == item.ItemID;
                    }).RowId;
                    Utils.AddToTodoList(Plugin.Configuration, rowID);
                }

                ImGui.TableSetColumnIndex(3);
                ImGui.Text(item.RequiredTool != null ? item.RequiredTool.Name : "None");
            }

            ImGui.EndTable();
        }
    }

    private void DrawWorkshopTab() {
        var contentRegionAvail = ImGui.GetContentRegionAvail();

        {
            var imguiSucks = ImGui.CalcTextSize("Isleworks ImGui Sucks Dickballs Lmao").X;
            var childSize = contentRegionAvail with { X = imguiSucks };
            ImGui.BeginChild("ReSanctuary_WorkshopListSearchChild", childSize);

            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
            ImGui.InputText(string.Empty, ref workshopSearchFilter, 256);

            if (ImGui.BeginListBox("##ReSanctuary_WorkshopList", ImGui.GetContentRegionAvail())) {
                for (var i = 0; i < workshopItems.Count; i++) {
                    var item = workshopItems[i];

                    if (item.Name.ToLower().Contains(workshopSearchFilter.ToLower())) {
                        var selected = i == workshopSearchSelected;

                        if (ImGui.Selectable(item.Name, selected)) workshopSearchSelected = i;
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

            var item = workshopItems[workshopSearchSelected];

            var iconSize = ImGui.GetTextLineHeight() * 2f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(Utils.IconCache(item.Item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.SameLine();

            ImGui.Text($"{item.Name}\nDuration: {item.CraftingTime} hours");
            
            if (ImGui.Button("Add to todo list##ReSanctuary_WorkshopAddTodo_" + item.ItemID)) {
                foreach (var (requiredMat, matCount) in item.Materials) {
                    Utils.AddToTodoList(Plugin.Configuration, requiredMat, matCount);
                }
                
                Plugin.WindowSystem.GetWindow("ReSanctuary Widget").IsOpen = true;
            }

            ImGui.Text("Materials:");
            foreach (var (requiredMat, matCount) in item.Materials) {
                var itemPouchRow = itemPouchSheet.GetRow(requiredMat);
                var itemPouchItem = itemPouchRow.Item.Value;
                
                var mat = gatheringItems.Find(x => x.ItemID == itemPouchItem.RowId);

                var name = itemPouchItem.Name;
                var text = $"{name} x{matCount}";

                if (ImGui.TreeNode(text)) {
                    var matIconSize = ImGui.GetTextLineHeight() * 2;
                    var matIconSizeVec = new Vector2(matIconSize, matIconSize);

                    if (mat != null) {
                        ImGui.Image(Utils.IconCache(mat.Item.Icon).ImGuiHandle, matIconSizeVec, Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.Text($"Required tool: {(mat.RequiredTool != null ? mat.RequiredTool.Name : "None")}");

                        if (ImGui.Button("Show on map##ReSanctuary_WorkshopShowOnMap_" + mat.ItemID)) {
                            var teri = Plugin.islandSanctuary.RowId;

                            Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
                            Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
                        }
                    } else {
                        ImGui.Image(Utils.IconCache(itemPouchItem.Icon).ImGuiHandle, matIconSizeVec, Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.Text("No data available :(");
                    }

                    ImGui.TreePop();
                }
            }
            
            ImGui.EndChild();
        }
    }

    private void DrawCreatureTab()
    {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.NoKeepColumnsVisible;

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
        ImGui.InputText(string.Empty, ref creatureSearchFilter, 256);

        if (ImGui.BeginTable("ReSanctuary_MainWindowTable", 6, tableFlags))
        {
            ImGui.TableSetupColumn("Size/Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Posistion");
            ImGui.TableSetupColumn("Spawn Requirements");
            ImGui.TableSetupColumn("Guaranteed Leaving");
            ImGui.TableSetupColumn("Chance of Leaving");

            ImGui.TableHeadersRow();

            foreach (var item in creatureItems)
            {
                if (!item.Name.ToLower().Contains(creatureSearchFilter.ToLower())
                    && !item.Item1.Name.ToString().ToLower().Contains(creatureSearchFilter.ToLower())
                    && !item.Item2.Name.ToString().ToLower().Contains(creatureSearchFilter.ToLower())
                    && !weatherList[item.Weather].Name.ToString().ToLower().Contains(creatureSearchFilter.ToLower())
                    ) continue;

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                string sizetxt = "";
                switch (item.Size)
                {
                    case 1:
                        sizetxt = "[S]";
                        break;
                    case 2:
                        sizetxt = "[M]";
                        break;
                    case 3:
                        sizetxt = "[L]";
                        break;
                }
                ImGui.Text(sizetxt);
                ImGui.SameLine();
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(Utils.IconCache(item.IconID).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

                ImGui.TableSetColumnIndex(1);
                ImGui.Text(item.Name);

                ImGui.TableSetColumnIndex(2);
                ImGui.Text(item.IngameX.ToString("F1") + ", " + item.IngameY.ToString("F1"));
                ImGui.SameLine();
                ImGui.PushID("ReSanctuary_CreatureMap_" + (int)item.CreatureID);
                if (ImGuiComponents.IconButton(FontAwesomeIcon.MapMarkerAlt))
                {
                    var teri = Plugin.islandSanctuary.RowId;

                    PluginLog.Debug("radius: {radius}", item.Radius);

                    Utils.OpenGatheringMarker(teri, (int)item.MarkerX, (int)item.MarkerZ, item.Radius, item.Name);
                }
                ImGui.PopID();
                ImGui.TableSetColumnIndex(3);
                //spawn limits
                if (item.Weather != 0)
                {
                    var wiconSize = ImGui.GetTextLineHeight() * 1.25f;
                    var wiconSizeVec = new Vector2(wiconSize, wiconSize);
                    ImGui.Image(Utils.IconCache((uint)weatherList[item.Weather].Icon).ImGuiHandle, wiconSizeVec, Vector2.Zero, Vector2.One);
                    ImGui.SameLine();
                    ImGui.Text(weatherList[item.Weather].Name);
                }
                if (item.SpawnStart != 0 || item.SpawnEnd != 0)
                {
                    if (item.Weather !=0)
                    {
                        ImGui.SameLine();

                    }
                    ImGui.Text(Utils.Format24HourAsAmPm(item.SpawnStart) + "-" + Utils.Format24HourAsAmPm(item.SpawnEnd));        
                }
                ImGui.TableSetColumnIndex(4);
                //Item 1
                var i1iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var i1iconSizeVec = new Vector2(i1iconSize, i1iconSize);
                ImGui.Image(Utils.IconCache(item.Item1.Icon).ImGuiHandle, i1iconSizeVec, Vector2.Zero, Vector2.One);
                ImGui.SameLine();
                ImGui.Text(item.Item1ShortName);
                ImGui.SameLine();
                ImGui.PushID("ReSanctuary_CreatureItem_" + (int)item.CreatureID + "_" + (int)item.Item1ID);
                if (ImGuiComponents.IconButton(FontAwesomeIcon.ClipboardList))
                {
                    var rowID = itemPouchSheet.First(x => {
                        var itemValue = x.Item.Value;
                        if (itemValue == null) return false;
                        return itemValue.RowId == item.Item1ID;
                    }).RowId;
                    Utils.AddToTodoList(Plugin.Configuration, rowID);
                }
                ImGui.PopID();
                ImGui.TableSetColumnIndex(5);
                //Item 2
                var i2iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var i2iconSizeVec = new Vector2(i2iconSize, i2iconSize);
                ImGui.Image(Utils.IconCache(item.Item2.Icon).ImGuiHandle, i2iconSizeVec, Vector2.Zero, Vector2.One);
                ImGui.SameLine(0);
                ImGui.Text(item.Item2ShortName);
                ImGui.SameLine();
                ImGui.PushID("ReSanctuary_CreatureItem_" + (int)item.CreatureID + "_" + (int)item.Item2ID);
                if (ImGuiComponents.IconButton(FontAwesomeIcon.ClipboardList))
                {
                    var rowID = itemPouchSheet.First(x => {
                        var itemValue = x.Item.Value;
                        if (itemValue == null) return false;
                        return itemValue.RowId == item.Item2ID;
                    }).RowId;
                    Utils.AddToTodoList(Plugin.Configuration, rowID);
                }
                ImGui.PopID();


    }

            ImGui.EndTable();
        }


    }

    private void DrawTodoTab() {
        var todoList = Plugin.Configuration.TodoList;

        if (ImGui.Button("Open Todo Widget")) {
            Plugin.WindowSystem.GetWindow("ReSanctuary Widget").IsOpen = true;
        }

        foreach (var (id, amount) in todoList) {
            var item = itemPouchSheet.GetRow(id).Item.Value;
            var amnt = amount;

            var iconSize = ImGui.GetTextLineHeight() * 1.25f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(Utils.IconCache(item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.PushItemWidth(100 * ImGuiHelpers.GlobalScale);
            ImGui.SameLine();
            if (ImGui.InputInt($"##ReSanctuary_TodoList_{id}", ref amnt, 1, 2,
                    ImGuiInputTextFlags.EnterReturnsTrue)) {
                if (amnt > 0) {
                    todoList[id] = amnt;
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

    private void DrawAboutTab() {
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

    public override void Draw() {
        if (ImGui.BeginTabBar("##ReSanctuary_MainWindowTabs", ImGuiTabBarFlags.None)) {
            if (ImGui.BeginTabItem("Gathering")) {
                DrawGatheringTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Workshop")) {
                DrawWorkshopTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Creature"))
            {
                DrawCreatureTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Todo")) {
                DrawTodoTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("About")) {
                DrawAboutTab();
                ImGui.EndTabItem();
            }
        }
    }
}

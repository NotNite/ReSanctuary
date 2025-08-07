using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.Textures;
using Dalamud.Bindings.ImGui;
using Lumina.Excel.Sheets;
using ReSanctuary.Creature;
using ReSanctuary.Windows;

namespace ReSanctuary.Tabs;

public class CreatureTab : MainWindowTab {
    private string filter = string.Empty;

    public CreatureTab(Plugin plugin) : base(plugin, "Creatures") { }

    public override void Draw() {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit |
                         ImGuiTableFlags.NoKeepColumnsVisible;

        ImGui.Text("The data on this tab is crowdsourced and may not be accurate or complete.");

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
        ImGui.InputText(string.Empty, ref this.filter, 256);

        if (ImGui.BeginTable("ReSanctuary_MainWindowTable", 7, tableFlags)) {
            ImGui.TableSetupColumn("Size");
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Posistion");
            ImGui.TableSetupColumn("Spawn Requirements");
            ImGui.TableSetupColumn("Guaranteed Drop");
            ImGui.TableSetupColumn("Chance of Drop");

            ImGui.TableHeadersRow();

            foreach (var item in this.Plugin.CreatureItems) {
                var nameMatches = item.Name.ToLower().Contains(this.filter.ToLower());
                var oneMatches = item.Item1ShortName.ToLower().Contains(this.filter.ToLower());
                var twoMatches = item.Item2ShortName.ToLower().Contains(this.filter.ToLower());

                var weather = item.ExtraData?.Weather;
                var weatherMatches = false;
                if (weather != null && this.Plugin.WeatherList.TryGetValue(weather.Value, out var weatherData)) {
                    var name = weatherData.Name.ExtractText();
                    weatherMatches = name.ToLower().Contains(this.filter.ToLower());
                }

                if (!nameMatches && !oneMatches && !twoMatches && !weatherMatches) continue;

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                var size = item.Size switch {
                    1 => "[S]",
                    2 => "[M]",
                    3 => "[L]",
                    _ => "[?]"
                };
                ImGui.Text(size);

                ImGui.TableSetColumnIndex(1);
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(Plugin.TextureProvider.GetFromGameIcon(item.IconId).GetWrapOrEmpty().Handle,
                            iconSizeVec, Vector2.Zero,
                            Vector2.One);

                ImGui.TableSetColumnIndex(2);
                var itemName = item.ExtraData?.Name;
                if (itemName != null) {
                    ImGui.Text(itemName);
                } else {
                    ImGui.TextDisabled("???");
                }

                ImGui.TableSetColumnIndex(3);
                if (item.ExtraData != null && item.ExtraData.InGameX != 0 && item.ExtraData.InGameY != 0) {
                    ImGui.Text(item.ExtraData.InGameX.ToString("F1") + ", " + item.ExtraData.InGameY.ToString("F1"));
                    ImGui.SameLine();
                    ImGui.PushID("ReSanctuary_CreatureMap_" + (int) item.CreatureId);
                    if (ImGuiComponents.IconButton(FontAwesomeIcon.MapMarkerAlt)) {
                        var teri = Plugin.IslandSanctuary.RowId;

                        Plugin.PluginLog.Debug("radius: {radius}", item.ExtraData.Radius);

                        Utils.OpenGatheringMarker(teri, (int) item.MarkerX, (int) item.MarkerZ, item.ExtraData.Radius,
                                                  item.Name, item.IconId);
                    }
                    ImGui.PopID();
                } else {
                    ImGui.TextDisabled("???");
                }


                ImGui.TableSetColumnIndex(4);
                if (item.ExtraData != null) {
                    if (item.ExtraData.Weather != null) {
                        var weatherEntry = this.Plugin.WeatherList[item.ExtraData.Weather.Value];
                        var weatherSize = ImGui.GetTextLineHeight() * 1.25f;
                        var weatherSizeVec = new Vector2(weatherSize, weatherSize);
                        var weatherIcon = weatherEntry.Icon;
                        ImGui.Image(
                            Plugin.TextureProvider.GetFromGameIcon((uint) weatherIcon).GetWrapOrEmpty().Handle,
                            weatherSizeVec, Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.Text(weatherEntry.Name.ExtractText());
                    }

                    if (item.ExtraData.SpawnStart != null && item.ExtraData.SpawnEnd != null) {
                        if (item.ExtraData.Weather != null) ImGui.SameLine();

                        var start = Utils.Format24HourAsAmPm(item.ExtraData.SpawnStart.Value);
                        var end = Utils.Format24HourAsAmPm(item.ExtraData.SpawnEnd.Value);
                        ImGui.TextUnformatted(start + " - " + end);
                    }
                } else {
                    ImGui.TextDisabled("Unknown");
                }

                ImGui.TableSetColumnIndex(5);
                this.DrawItem(item, item.Item1);

                ImGui.TableSetColumnIndex(6);
                this.DrawItem(item, item.Item2);
            }

            ImGui.EndTable();
        }
    }

    private void DrawItem(CreatureItem creatureItem, Item item) {
        var iconSize = ImGui.GetTextLineHeight() * 1.5f;
        var iconSizeVec = new Vector2(iconSize, iconSize);

        var itemName = item.Name.ExtractText().Replace("Sanctuary ", "");

        ImGui.Image(
            Plugin.TextureProvider.GetFromGameIcon(new GameIconLookup(item.Icon)).GetWrapOrEmpty().Handle,
            iconSizeVec,
            Vector2.Zero, Vector2.One);
        ImGui.SameLine();

        ImGui.Text(itemName);
        ImGui.SameLine();

        ImGui.PushID("ReSanctuary_CreatureItem_" + (int) creatureItem.CreatureId + "_" + (int) item.RowId);
        if (ImGuiComponents.IconButton(FontAwesomeIcon.ClipboardList)) {
            var rowId = this.Plugin.MJIItemPouchSheet.First(x => x.Item.RowId == item.RowId).RowId;
            Utils.AddToTodoList(Plugin.Configuration, rowId);
        }

        ImGui.PopID();
    }
}

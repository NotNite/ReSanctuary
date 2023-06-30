using System;
using System.Collections.Generic;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiScene;
using Lumina.Excel.GeneratedSheets;
using ReSanctuary.Creature;
using MapType = FFXIVClientStructs.FFXIV.Client.UI.Agent.MapType;

namespace ReSanctuary;

public static class Utils {
    public static unsafe void OpenGatheringMarker(uint teri, int x, int y, int radius, string name, uint icon) {
        var agent = AgentMap.Instance();
        PluginLog.Debug("current teri/map: {currentTeri} {currentMap}", agent->CurrentTerritoryId, agent->CurrentMapId);
        if (teri != agent->CurrentTerritoryId) return;

        agent->AddGatheringTempMarker(x, y, radius, icon, 4u, name);
        agent->OpenMap(agent->CurrentMapId, teri, name, MapType.GatheringLog);
    }

    public static List<GatheringItem> GetSortedGatheringItems() {
        var gatheringSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringItem");
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();
        var keyItemSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIKeyItem");
        var gatheringToolSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringTool");

        var items = new List<GatheringItem>();
        var enumerator = gatheringSheet.GetEnumerator();
        enumerator.MoveNext(); // skip blank item?

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;

            var itemID = current.ReadColumn<uint>(0);
            var item = itemSheet.GetRow(itemID);

            Item? tool = null;
            var toolID = current.ReadColumn<byte>(2);
            if (toolID > 0) {
                var gatheringToolID = gatheringToolSheet.GetRow(toolID).ReadColumn<byte>(0);
                var toolRow = keyItemSheet.GetRow(gatheringToolID);
                var toolItemID = toolRow.ReadColumn<int>(0);
                tool = itemSheet.GetRow((uint)toolItemID);
            }

            var gi = new GatheringItem(current, item, tool);
            items.Add(gi);
        }

        items.Sort((x, y) => x.UIIndex.CompareTo(y.UIIndex));

        return items;
    }

    public static List<WorkshopItem> GetSortedWorkshopItems() {
        var craftingSheet = Plugin.DataManager.Excel.GetSheetRaw("MJICraftworksObject");
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();

        var items = new List<WorkshopItem>();
        var enumerator = craftingSheet.GetEnumerator();
        while (enumerator.MoveNext()) {
            var current = enumerator.Current;

            var itemID = current.ReadColumn<ushort>(0);
            if (itemID > 0) {
                var item = itemSheet.GetRow(itemID);
                var wi = new WorkshopItem(current, item);
                items.Add(wi);
            }
        }

        items.Sort((x, y) => x.UIIndex.CompareTo(y.UIIndex));

        return items;
    }

    public static List<CreatureItem> GetCreatureItems() {
        var creatureSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIAnimals");
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();

        var creatures = new List<CreatureItem>();
        var enumerator = creatureSheet.GetEnumerator();
        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            var creatureID = current.ReadColumn<uint>(0);
            if (creatureID > 0) {
                var ci = new CreatureItem();

                ci.CreatureID = creatureID;
                ci.IconID = (uint)current.ReadColumn<int>(6);
                ci.UIIndex = current.RowId;
                ci.Size = current.ReadColumn<byte>(1);
                ci.CreatureType = current.ReadColumn<byte>(2);
                ci.CreatureGroup = current.ReadColumn<byte>(3);
                ci.Item1ID = current.ReadColumn<uint>(4);
                ci.Item1 = itemSheet.GetRow(ci.Item1ID);
                ci.Item1ShortName = ci.Item1.Name.RawString.Replace("Sanctuary ", "");
                ci.Item2ID = current.ReadColumn<uint>(5);
                ci.Item2 = itemSheet.GetRow(ci.Item2ID);
                ci.Item2ShortName = ci.Item2.Name.RawString.Replace("Sanctuary ", "");

                CreatureExtraData? ed = CreatureData.GetCreatureExtraData(ci.CreatureID);
                if (ed != null) {
                    ci.Name = ed.Name;
                    ci.SpawnStart = ed.SpawnStart;
                    ci.SpawnEnd = ed.SpawnEnd;
                    ci.Weather = ed.Weather;
                    ci.IngameX = ed.IngameX;
                    ci.IngameY = ed.IngameY;
                    ci.Radius = ed.Radius;
                }
                else {
                    ci.Name = " . . . . ";
                    ci.SpawnStart = -1;
                    ci.SpawnEnd = -1;
                    ci.Weather = 0;
                    ci.IngameX = -1;
                    ci.IngameY = -1;
                    ci.Radius = 0;

                }

                ci.MarkerX = ConvertMapCoordToWorldCoordXZ((float)ci.IngameX, 100, -175);
                ci.MarkerZ = ConvertMapCoordToWorldCoordXZ((float)ci.IngameY, 100, 138);

                creatures.Add(ci);
            }
        }

        creatures.Sort((x, y) => x.UIIndex.CompareTo(y.UIIndex));

        return creatures;
    }

    public static Dictionary<uint, string> SeperateCreatureDrops(List<CreatureItem> creatures) {
        Dictionary<uint, string> drops = new Dictionary<uint, string>();
        foreach (CreatureItem item in creatures) {
            drops.TryAdd(item.Item1ID, item.Item1.Name);
            drops.TryAdd(item.Item2ID, item.Item2.Name);
        }

        return drops;
    }

    public static List<string> FindDropOnCreatures(uint itemid, List<CreatureItem> creatures) {
        List<string> droplist = new List<string>();
        foreach (CreatureItem item in creatures) {
            if (item.Item1ID == itemid || item.Item2ID == itemid) {
                droplist.Add(item.Name);
            }
        }

        return droplist;
    }

    public static Dictionary<uint, Weather> GetISWeathers() {
        Dictionary<uint, Weather> list = new Dictionary<uint, Weather>();
        List<uint> weathers = new List<uint> { 0, 1, 2, 3, 4, 7, 8 };

        var weatherSheet = Plugin.DataManager.Excel.GetSheet<Weather>();

        foreach (uint item in weathers) {
            var weatherRow = weatherSheet.GetRow(item);
            list.Add(item, weatherRow);
            IconCachePreload((uint)weatherRow.Icon);
        }

        return list;
    }

    public static void IconCachePreload(uint iconID) {
        if (!Plugin.IconCache.ContainsKey(iconID)) {
            //PluginLog.Debug("Add iconID " + iconID + " to IconCache");
            var icon = Plugin.DataManager.GetImGuiTextureIcon(iconID);
            Plugin.IconCache[iconID] = icon;
        }

        return;
    }

    public static TextureWrap IconCache(uint iconID) {
        // Ensure icon is loaded
        IconCachePreload(iconID);
        // Return icon
        return Plugin.IconCache[iconID];
    }


    public static void AddToTodoList(Configuration config, uint id, int amount = 1) {
        var todoList = config.TodoList;
        if (!todoList.ContainsKey(id)) todoList[id] = 0;
        todoList[id] += amount;

        config.TodoList = todoList;
        config.Save();
    }

    public static unsafe int GetStackSize(uint id) {
        return InventoryManager.Instance()->GetInventoryItemCount(id);
    }

    public static string Format24HourAsAmPm(int hour) {
        var ampm = "am";
        if (hour >= 12) {
            ampm = "pm";
            hour -= 12;
        }

        if (hour == 0) {
            hour = 12;
        }

        return hour + ampm;
    }

    //Copied from Dalamud.Utility.MapUtil
    public static float ConvertWorldCoordXZToMapCoord(float value, uint scale, int offset) {
        return 0.02f * offset + 2048f / scale + 0.02f * value + 1f;
    }

    public static float ConvertMapCoordToWorldCoordXZ(float value, uint scale, int offset) {
        return (value - 0.02f * offset - 2048f / scale - 1f) / 0.02f;
    }
}

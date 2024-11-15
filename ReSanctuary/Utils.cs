using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiScene;
using Lumina.Excel.Sheets;
using ReSanctuary.Creature;
using MapType = FFXIVClientStructs.FFXIV.Client.UI.Agent.MapType;

namespace ReSanctuary;

public static class Utils {
    private static readonly Dictionary<uint, TextureWrap> IconCache = new();

    public static unsafe void OpenGatheringMarker(uint teri, int x, int y, int radius, string name, uint icon) {
        var agent = AgentMap.Instance();
        Plugin.PluginLog.Debug("current teri/map: {currentTeri} {currentMap}", agent->CurrentTerritoryId,
                               agent->CurrentMapId);
        if (teri != agent->CurrentTerritoryId) return;

        agent->AddGatheringTempMarker(x, y, radius, iconId: icon, tooltip: name);
        agent->OpenMap(agent->CurrentMapId, teri, name, MapType.GatheringLog);
    }

    public static List<GatheringItem> GetSortedGatheringItems() {
        var gatheringSheet = Plugin.DataManager.Excel.GetSheet<MJIGatheringItem>();
        var keyItemSheet = Plugin.DataManager.Excel.GetSheet<MJIKeyItem>()!;
        var gatheringToolSheet = Plugin.DataManager.Excel.GetSheet<MJIGatheringTool>()!;

        var items = new List<GatheringItem>();
        using var enumerator = gatheringSheet.GetEnumerator();
        enumerator.MoveNext(); // skip blank item?

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;

            Item? tool = null;
            var toolId = current.Unknown0;
            if (toolId > 0) {
                var gatheringToolId = gatheringToolSheet.GetRow(toolId)!.Unknown0;
                var toolRow = keyItemSheet.GetRow(gatheringToolId)!;
                tool = toolRow.Item.Value;
            }

            var gi = new GatheringItem(current, tool);
            items.Add(gi);
        }

        items.Sort((x, y) => x.UiIndex.CompareTo(y.UiIndex));

        return items;
    }

    public static List<WorkshopItem> GetSortedWorkshopItems() {
        var craftingSheet = Plugin.DataManager.Excel.GetSheet<MJICraftworksObject>();
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>()!;

        var items = new List<WorkshopItem>();
        using var enumerator = craftingSheet.GetEnumerator();
        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            if (current.Item.RowId != 0) {
                var wi = new WorkshopItem(current);
                items.Add(wi);
            }
        }

        items.Sort((x, y) => x.UiIndex.CompareTo(y.UiIndex));

        return items;
    }

    public static List<CreatureItem> GetCreatureItems() {
        var creatureSheet = Plugin.DataManager.Excel.GetSheet<MJIAnimals>();
        var creatures = new List<CreatureItem>();
        using var enumerator = creatureSheet.GetEnumerator();

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            var creatureId = current.BNpcBase.RowId;
            if (creatureId > 0) {
                var ci = new CreatureItem(current);
                creatures.Add(ci);
            }
        }

        creatures.Sort((x, y) => x.UiIndex.CompareTo(y.UiIndex));

        return creatures;
    }

    public static Dictionary<uint, string> SeparateCreatureDrops(List<CreatureItem> creatures) {
        var drops = new Dictionary<uint, string>();

        foreach (var item in creatures) {
            drops.TryAdd(item.Item1.RowId, item.Item1.Name.ExtractText());
            drops.TryAdd(item.Item2.RowId, item.Item2.Name.ExtractText());
        }

        return drops;
    }

    public static List<string> FindDropOnCreatures(uint itemId, List<CreatureItem> creatures) {
        var dropList = new List<string>();

        foreach (var item in creatures) {
            if (item.Item2.RowId == itemId || item.Item2.RowId == itemId) dropList.Add(item.ExtraData?.Name ?? "???");
        }

        return dropList;
    }

    public static Dictionary<uint, Weather> GetSanctuaryWeathers() {
        var list = new Dictionary<uint, Weather>();
        var weathers = new List<uint> {0, 1, 2, 3, 4, 7, 8};
        var weatherSheet = Plugin.DataManager.Excel.GetSheet<Weather>()!;

        foreach (var item in weathers) {
            var weatherRow = weatherSheet.GetRow(item)!;
            list.Add(item, weatherRow);
        }

        return list;
    }

    public static void AddToTodoList(Configuration config, uint id, int amount = 1) {
        var todoList = config.TodoList;
        todoList.TryAdd(id, 0);
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

    // Copied from Dalamud.Utility.MapUtil
    public static float ConvertWorldCoordXzToMapCoord(float value, uint scale, int offset) {
        return (0.02f * offset) + (2048f / scale) + (0.02f * value) + 1f;
    }

    public static float ConvertMapCoordToWorldCoordXz(float value, uint scale, int offset) {
        return (value - (0.02f * offset) - (2048f / scale) - 1f) / 0.02f;
    }
}

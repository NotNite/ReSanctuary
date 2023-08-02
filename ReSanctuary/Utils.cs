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
    private static readonly Dictionary<uint, TextureWrap> IconCache = new();

    public static unsafe void OpenGatheringMarker(uint teri, int x, int y, int radius, string name, uint icon) {
        var agent = AgentMap.Instance();
        PluginLog.Debug("current teri/map: {currentTeri} {currentMap}", agent->CurrentTerritoryId, agent->CurrentMapId);
        if (teri != agent->CurrentTerritoryId) return;

        agent->AddGatheringTempMarker(x, y, radius, iconId: icon, tooltip: name);
        agent->OpenMap(agent->CurrentMapId, teri, name, MapType.GatheringLog);
    }

    public static List<GatheringItem> GetSortedGatheringItems() {
        var gatheringSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringItem")!;
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>()!;
        var keyItemSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIKeyItem")!;
        var gatheringToolSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringTool")!;

        var items = new List<GatheringItem>();
        using var enumerator = gatheringSheet.GetEnumerator();
        enumerator.MoveNext(); // skip blank item?

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;

            var itemId = current.ReadColumn<uint>(0);
            var item = itemSheet.GetRow(itemId)!;

            Item? tool = null;
            var toolId = current.ReadColumn<byte>(2);
            if (toolId > 0) {
                var gatheringToolId = gatheringToolSheet.GetRow(toolId)!.ReadColumn<byte>(0);
                var toolRow = keyItemSheet.GetRow(gatheringToolId)!;
                var toolItemId = toolRow.ReadColumn<int>(0);
                tool = itemSheet.GetRow((uint) toolItemId);
            }

            var gi = new GatheringItem(current, item, tool);
            items.Add(gi);
        }

        items.Sort((x, y) => x.UiIndex.CompareTo(y.UiIndex));

        return items;
    }

    public static List<WorkshopItem> GetSortedWorkshopItems() {
        var craftingSheet = Plugin.DataManager.Excel.GetSheetRaw("MJICraftworksObject")!;
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>()!;

        var items = new List<WorkshopItem>();
        using var enumerator = craftingSheet.GetEnumerator();
        while (enumerator.MoveNext()) {
            var current = enumerator.Current;

            var itemId = current.ReadColumn<ushort>(0);
            if (itemId > 0) {
                var item = itemSheet.GetRow(itemId)!;
                var wi = new WorkshopItem(current, item);
                items.Add(wi);
            }
        }

        items.Sort((x, y) => x.UiIndex.CompareTo(y.UiIndex));

        return items;
    }

    public static List<CreatureItem> GetCreatureItems() {
        var creatureSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIAnimals")!;
        var creatures = new List<CreatureItem>();
        using var enumerator = creatureSheet.GetEnumerator();

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            var creatureId = current.ReadColumn<uint>(0);
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
            drops.TryAdd(item.Item1.RowId, item.Item1.Name);
            drops.TryAdd(item.Item2.RowId, item.Item2.Name);
        }

        return drops;
    }

    public static List<string> FindDropOnCreatures(uint itemId, List<CreatureItem> creatures) {
        var dropList = new List<string>();

        foreach (var item in creatures) {
            if (item.Item1Id == itemId || item.Item2Id == itemId) dropList.Add(item.ExtraData?.Name ?? "???");
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
            IconCachePreload((uint) weatherRow.Icon);
        }

        return list;
    }

    public static void IconCachePreload(uint iconId) {
        if (!IconCache.ContainsKey(iconId)) {
            var icon = Plugin.DataManager.GetImGuiTextureIcon(iconId)!;
            IconCache[iconId] = icon;
        }
    }

    public static TextureWrap GetFromIconCache(uint iconId) {
        IconCachePreload(iconId);
        return IconCache[iconId];
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

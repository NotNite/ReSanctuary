using System.Collections.Generic;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Lumina.Excel.GeneratedSheets;
using MapType = FFXIVClientStructs.FFXIV.Client.UI.Agent.MapType;

namespace ReSanctuary;

public static class Utils {
    public static unsafe void OpenGatheringMarker(uint teri, int x, int y, int radius, string name) {
        var agent = AgentMap.Instance();
        PluginLog.Debug("current teri/map: {currentTeri} {currentMap}", agent->CurrentTerritoryId, agent->CurrentMapId);
        if (teri != agent->CurrentTerritoryId) return;

        agent->AddGatheringTempMarker(x, y, radius);
        agent->OpenMap(agent->CurrentMapId, teri, name, MapType.GatheringLog);
    }

    public static List<GatheringItem> GetSortedGatheringItems() {
        var gatheringSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringItem");
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();
        var keyItemSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIKeyItem");

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
                var toolRow = keyItemSheet.GetRow(toolID);
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
}

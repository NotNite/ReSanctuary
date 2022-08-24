using System.Collections.Generic;
using Dalamud.Logging;
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

    public static List<GatheringItem> GetSortedItems() {
        var gatheringSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIGatheringItem");
        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();

        var items = new List<GatheringItem>();
        var enumerator = gatheringSheet.GetEnumerator();
        enumerator.MoveNext(); // skip blank item?

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            
            var itemID = current.ReadColumn<uint>(0);
            var item = itemSheet.GetRow(itemID);
            var gi = new GatheringItem(current, item);
            items.Add(gi);
        }
        
        items.Sort((x, y) => x.UIIndex.CompareTo(y.UIIndex));

        return items;
    }
}

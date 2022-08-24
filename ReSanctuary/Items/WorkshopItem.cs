using System;
using System.Collections.Generic;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary;

public class WorkshopItem : BaseItem {
    public ushort CraftingTime;
    public ushort Value;
    public List<(ushort requiredMat, ushort matCount)> Materials = new();

    public WorkshopItem(RowParser workshopItem, Item item) {
        Name = item.Name;
        Item = item;
        ItemID = item.RowId;
        Icon = Plugin.DataManager.GetImGuiTextureIcon(item.Icon);
        RowID = workshopItem.RowId;
        UIIndex = (byte)workshopItem.RowId;

        CraftingTime = workshopItem.ReadColumn<ushort>(13);
        Value = workshopItem.ReadColumn<ushort>(14);

        for (var i = 4;; i += 2) {
            var requiredMat = workshopItem.ReadColumn<ushort>(i);
            var matCount = workshopItem.ReadColumn<ushort>(i + 1);

            if (matCount == 0) break;

            Materials.Add((requiredMat, matCount));
        }
    }
}

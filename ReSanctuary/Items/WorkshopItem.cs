using System.Collections.Generic;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary;

public class WorkshopItem : BaseItem {
    public readonly ushort CraftingTime;
    public readonly ushort Value;
    public readonly List<(ushort requiredMat, ushort matCount)> Materials = new();

    public WorkshopItem(RowParser workshopItem, Item item) : base(workshopItem, item, (byte) workshopItem.RowId) {
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

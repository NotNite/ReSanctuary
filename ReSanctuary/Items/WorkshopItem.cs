using System.Collections.Generic;
using System.Linq;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace ReSanctuary;

public class WorkshopItem : BaseItem {
    public readonly ushort CraftingTime;
    public readonly ushort Value;
    public readonly List<(ushort requiredMat, ushort matCount)> Materials = new();

    public WorkshopItem(MJICraftworksObject current) : base(current.Item.Value) {
        this.RowId = current.RowId;
        this.UiIndex = (byte) current.RowId;

        CraftingTime = current.CraftingTime;
        Value = current.Value;

        foreach (var (mat, count) in current.Material.Zip(current.Amount)) {
            if (count == 0) break;
            Materials.Add(((ushort) mat.RowId, count));
        }
    }
}

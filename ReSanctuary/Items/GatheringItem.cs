using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary;

public class GatheringItem : BaseItem {
    public readonly Item? RequiredTool;
    public readonly short X;
    public readonly short Y;
    public readonly ushort Radius;

    public GatheringItem(RowParser gatheringItem, Item item, Item? tool)
        : base(gatheringItem, item, gatheringItem.ReadColumn<byte>(1)) {
        RequiredTool = tool;
        X = gatheringItem.ReadColumn<short>(3);
        Y = gatheringItem.ReadColumn<short>(4);
        Radius = gatheringItem.ReadColumn<ushort>(5);
    }
}

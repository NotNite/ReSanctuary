using Lumina.Excel.Sheets;

namespace ReSanctuary;

public class GatheringItem : BaseItem {
    public readonly Item? RequiredTool;
    public readonly short X;
    public readonly short Y;
    public readonly ushort Radius;

    public GatheringItem(MJIGatheringItem current, Item? tool) : base(current.Item.Value) {
        this.RowId = current.RowId;
        this.UiIndex = current.Sort;
        this.RequiredTool = tool;
        this.X = current.X;
        this.Y = current.Y;
        this.Radius = current.Radius;
    }
}

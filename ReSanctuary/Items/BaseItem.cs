using Dalamud.Utility;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary;

public abstract class BaseItem {
    public readonly string Name;
    public readonly Item Item;
    public readonly uint ItemId;
    public readonly uint RowId;
    public readonly byte UiIndex;

    protected BaseItem(RowParser row, Item item, byte uiIndex) {
        this.Name = item.Name.ToDalamudString().TextValue;
        this.Item = item;
        this.ItemId = item.RowId;
        this.RowId = row.RowId;
        this.UiIndex = uiIndex;
    }
}

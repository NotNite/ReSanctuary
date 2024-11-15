using Lumina.Excel.Sheets;

namespace ReSanctuary;

public abstract class BaseItem(Item item) {
    public readonly string Name = item.Name.ExtractText();
    public readonly Item Item = item;
    public readonly uint ItemId = item.RowId;
    public uint RowId;
    public byte UiIndex;
}

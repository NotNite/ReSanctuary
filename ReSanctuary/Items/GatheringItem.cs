using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary;

public class GatheringItem : BaseItem {
    public Item? RequiredTool;
    public short X;
    public short Y;
    public ushort Radius;

    public GatheringItem(RowParser gatheringItem, Item item, Item? tool) {
        Name = item.Name;
        Item = item;
        ItemID = item.RowId;
        Icon = Plugin.DataManager.GetImGuiTextureIcon(item.Icon);
        RowID = gatheringItem.RowId;
        UIIndex = gatheringItem.ReadColumn<byte>(1);

        RequiredTool = tool;
        X = gatheringItem.ReadColumn<short>(3);
        Y = gatheringItem.ReadColumn<short>(4);
        Radius = gatheringItem.ReadColumn<ushort>(5);
    }
}

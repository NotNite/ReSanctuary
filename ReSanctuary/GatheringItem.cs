using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary; 

public class GatheringItem {
    public string Name;
    public Item Item;
    public uint ItemID;
    public TextureWrap Icon;
    
    public byte UIIndex;
    public byte RequiredTool;
    public short X;
    public short Y;
    public ushort Radius;

    public GatheringItem(RowParser gatheringItem, Item item) {
        Name = item.Name;
        Item = item;
        ItemID = item.RowId;
        Icon = Plugin.DataManager.GetImGuiTextureIcon(item.Icon);
        
        UIIndex = gatheringItem.ReadColumn<byte>(1);
        RequiredTool = gatheringItem.ReadColumn<byte>(2);
        X = gatheringItem.ReadColumn<short>(3);
        Y = gatheringItem.ReadColumn<short>(4);
        Radius = gatheringItem.ReadColumn<ushort>(5);
    }
}

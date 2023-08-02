using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Creature;

public record CreatureExtraData {
    public required string Name;
    public required int? SpawnStart;
    public required int? SpawnEnd;
    public required uint? Weather;
    public required double InGameX;
    public required double InGameY;
    public required ushort Radius;
}

public class CreatureItem {
    public readonly uint CreatureId;
    public readonly uint IconId;
    public readonly uint UiIndex;
    public readonly byte Size;

    public Item Item1;
    public uint Item1Id;
    public Item Item2;
    public uint Item2Id;

    public float MarkerX;
    public float MarkerZ;

    public CreatureExtraData? ExtraData;

    public string Name => this.ExtraData?.Name ?? "???";
    public string Item1ShortName => this.GetItemName(this.Item1);
    public string Item2ShortName => this.GetItemName(this.Item2);

    public CreatureItem(RowParser row) {
        this.CreatureId = row.ReadColumn<uint>(0);
        this.IconId = (uint) row.ReadColumn<int>(6);
        this.UiIndex = row.RowId;
        this.Size = row.ReadColumn<byte>(1);

        var itemSheet = Plugin.DataManager.Excel.GetSheet<Item>()!;
        this.Item1Id = row.ReadColumn<uint>(4);
        this.Item1 = itemSheet.GetRow(this.Item1Id)!;
        this.Item2Id = row.ReadColumn<uint>(5);
        this.Item2 = itemSheet.GetRow(this.Item2Id)!;

        this.ExtraData = CreatureData.GetCreatureExtraData(this.CreatureId);

        this.MarkerX = Utils.ConvertMapCoordToWorldCoordXz(
            (float) (this.ExtraData?.InGameX ?? -1),
            100,
            -175
        );

        this.MarkerZ = Utils.ConvertMapCoordToWorldCoordXz(
            (float) (this.ExtraData?.InGameY ?? -1),
            100,
            138
        );
    }

    private string GetItemName(Item? item)
        => item?.Name.RawString.Replace("Sanctuary ", "") ?? "???";
}

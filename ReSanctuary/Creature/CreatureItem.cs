using Lumina.Excel.Sheets;

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
    public Item Item2;

    public float MarkerX;
    public float MarkerZ;

    public CreatureExtraData? ExtraData;

    public string Name => this.ExtraData?.Name ?? "???";
    public string Item1ShortName => this.GetItemName(this.Item1);
    public string Item2ShortName => this.GetItemName(this.Item2);

    public CreatureItem(MJIAnimals current) {
        this.CreatureId = current.BNpcBase.RowId;
        this.IconId = (uint)current.Icon;
        this.UiIndex = current.RowId;
        this.Size = current.Size;

        this.Item1 = current.Reward[0].Value;
        this.Item2 = current.Reward[1].Value;

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
        => item?.Name.ExtractText().Replace("Sanctuary ", "") ?? "???";
}

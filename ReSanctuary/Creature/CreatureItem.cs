using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Creature;

public class CreatureExtraData {
    public string Name;
    public int SpawnStart;
    public int SpawnEnd;
    public uint Weather;
    public double IngameX;
    public double IngameY;
    public ushort Radius;
}

public class CreatureItem {
    public string Name;
    public uint CreatureID;
    public byte CreatureType;
    public byte CreatureGroup;
    public uint IconID;
    public uint RowID;
    public uint UIIndex;
    public int SpawnStart;
    public int SpawnEnd;
    public uint Weather;
    public byte Size;
    public Item? Item1;
    public uint Item1ID;
    public string Item1ShortName;
    public Item? Item2;
    public uint Item2ID;
    public string Item2ShortName;
    public double IngameX;
    public double IngameY;
    public float MarkerX;
    public float MarkerZ;
    public ushort Radius;
}

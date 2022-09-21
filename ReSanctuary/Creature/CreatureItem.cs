using System;
using System.Collections.Generic;
using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Creature;

public class CreatureExtraData
{
    public string Name;
    public int SpawnStart;
    public int SpawnEnd;
    public uint Weather;
    public double IngameX;
    public double IngameY;
    public ushort Radius;

}

public class CreatureItem
{
    public string Name;
    public uint CreatureID;
    public byte CreatureType;
    public byte CreatureGroup;
    public int IconID;
    public TextureWrap Icon;
    public uint RowID;
    public uint UIIndex;
    public int SpawnStart;
    public int SpawnEnd;
    public uint Weather;
    public byte Size;
    public Item? Item1;
    public uint Item1ID;
    public Item? Item2;
    public uint Item2ID;
    public double IngameX;
    public double IngameY;
    public int MarkerX;
    public int MarkerY;
    public ushort Radius;

}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;

namespace ReSanctuary.Creature
{
    internal class CreatureData
    {
        // TODO: Adjust to use the weather IDs the IS uses
        public const uint ISWEATHER_CLEAR = 1;
        public const uint ISWEATHER_FAIR = 2;
        public const uint ISWEATHER_CLOUDS = 3;
        public const uint ISWEATHER_RAIN = 7;
        public const uint ISWEATHER_SHOWERS = 8;
        public const uint ISWEATHER_FOG = 4;

        public static CreatureExtraData? GetCreatureExtraData(uint CreatureID)
        {
            CreatureExtraData? data = null;
            switch (CreatureID)
            {
                case 14906:
                    data = new CreatureExtraData
                    {
                        Name = "Lost Lamb",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 23,
                        Radius = 30
                    };
                    break;
                case 14907:
                    data = new CreatureExtraData
                    {
                        Name = "Ornery Karakul",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_FAIR,
                        IngameX = 20,
                        IngameY = 23,
                        Radius = 10
                    };
                    break;
                case 14908:
                    data = new CreatureExtraData
                    {
                        Name = "Opo-Opo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 26,
                        Radius = 30
                    };
                    break;
                case 14909:
                    data = new CreatureExtraData
                    {
                        Name = "Lemur",
                        SpawnStart = 6,
                        SpawnEnd = 9,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 26,
                        Radius = 10
                    };
                    break;
                case 14910:
                    data = new CreatureExtraData
                    {
                        Name = "Apkallu",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 19,
                        IngameY = 11,
                        Radius = 30
                    };
                    break;
                case 14911:
                    data = new CreatureExtraData
                    {
                        Name = "Apkallu of Paradise",
                        SpawnStart = 12,
                        SpawnEnd = 15,
                        Weather = 0,
                        IngameX = 19,
                        IngameY = 11,
                        Radius = 10
                    };
                    break;
                case 14912:
                    data = new CreatureExtraData
                    {
                        Name = "Ground Squirrel",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 15,
                        IngameY = 19,
                        Radius = 30
                    };
                    break;
                case 14913:
                    data = new CreatureExtraData
                    {
                        Name = "Star Marmot",
                        SpawnStart = 9,
                        SpawnEnd = 12,
                        Weather = 0,
                        IngameX = 15,
                        IngameY = 19,
                        Radius = 10
                    };
                    break;
                case 14914:
                    data = new CreatureExtraData
                    {
                        Name = "Coblyn",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 13,
                        Radius = 40
                    };
                    break;
                case 14915:
                    data = new CreatureExtraData
                    {
                        Name = "Yellow Coblyn",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_FOG,
                        IngameX = 27,
                        IngameY = 19,
                        Radius = 10
                    };
                    break;
                case 14916:
                    data = new CreatureExtraData
                    {
                        Name = "Wild Dodo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 16,
                        IngameY = 12,
                        Radius = 30
                    };
                    break;
                case 14917:
                    data = new CreatureExtraData
                    {
                        Name = "Dodo of Paradise",
                        SpawnStart = 15,
                        SpawnEnd = 18,
                        Weather = 0,
                        IngameX = 16,
                        IngameY = 12,
                        Radius = 10
                    };
                    break;
                case 14918:
                    data = new CreatureExtraData
                    {
                        Name = "Island Doe",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 21,
                        IngameY = 19,
                        Radius = 40
                    };
                    break;
                case 14919:
                    data = new CreatureExtraData
                    {
                        Name = "Island Stag",
                        SpawnStart = 18,
                        SpawnEnd = 21,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 19,
                        Radius = 10
                    };
                    break;
                case 14920:
                    data = new CreatureExtraData
                    {
                        Name = "Chocobo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 20,
                        IngameY = 21,
                        Radius = 30
                    };
                    break;
                case 14921:
                    data = new CreatureExtraData
                    {
                        Name = "Black Chocobo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_CLEAR,
                        IngameX = 13,
                        IngameY = 11,
                        Radius = 10
                    };
                    break;
                case 14922:
                    data = new CreatureExtraData
                    {
                        Name = "Glyptodon Pup",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 30,
                        IngameY = 11,
                        Radius = 40
                    };
                    break;
                case 14923:
                    data = new CreatureExtraData
                    {
                        Name = "Glyptodon",
                        SpawnStart = 0,
                        SpawnEnd = 3,
                        Weather = 0,
                        IngameX = 31,
                        IngameY = 11,
                        Radius = 10
                    };
                    break;
                case 14924:
                    data = new CreatureExtraData
                    {
                        Name = "Aurochs",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 12,
                        IngameY = 17,
                        Radius = 30
                    };
                    break;
                case 14925:
                    data = new CreatureExtraData
                    {
                        Name = "Grand Buffalo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_CLOUDS,
                        IngameX = 12,
                        IngameY = 17,
                        Radius = 10
                    };
                    break;
                case 14926:
                    data = new CreatureExtraData
                    {
                        Name = "Island Nanny",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 26,
                        IngameY = 24,
                        Radius = 40
                    };
                    break;
                case 14927:
                    data = new CreatureExtraData
                    {
                        Name = "Island Billy",
                        SpawnStart = 3,
                        SpawnEnd = 6,
                        Weather = 0,
                        IngameX = 26,
                        IngameY = 22,
                        Radius = 10
                    };
                    break;
                case 14928:
                    data = new CreatureExtraData
                    {
                        Name = "Blue Back",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 28,
                        IngameY = 28,
                        Radius = 30
                    };
                    break;
                case 14929:
                    data = new CreatureExtraData
                    {
                        Name = "Gold Back",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_RAIN,
                        IngameX = 31,
                        IngameY = 28,
                        Radius = 10
                    };
                    break;
                case 14930:
                    data = new CreatureExtraData
                    {
                        Name = "Twinklefleece",
                        SpawnStart = 18,
                        SpawnEnd = 21,
                        Weather = ISWEATHER_FOG,
                        IngameX = 22.1,
                        IngameY = 20.8,
                        Radius = 5
                    };
                    break;
                case 14931:
                    data = new CreatureExtraData
                    {
                        Name = "Beachcomb",
                        SpawnStart = 0,
                        SpawnEnd = 3,
                        Weather = ISWEATHER_RAIN,
                        IngameX = 17.8,
                        IngameY = 12.6,
                        Radius = 5
                    };
                    break;
                case 14932:
                    data = new CreatureExtraData
                    {
                        Name = "Paissa",
                        SpawnStart = 12,
                        SpawnEnd = 15,
                        Weather = ISWEATHER_FAIR,
                        IngameX = 24,
                        IngameY = 28,
                        Radius = 10
                    };
                    break;
                case 14933:
                    data = new CreatureExtraData
                    {
                        Name = "Alligator",
                        SpawnStart = 6,
                        SpawnEnd = 9,
                        Weather = ISWEATHER_SHOWERS,
                        IngameX = 17.7,
                        IngameY = 24,
                        Radius = 10
                    };
                    break;
                case 14934:
                    data = new CreatureExtraData
                    {
                        Name = "Goobbue",
                        SpawnStart = 9,
                        SpawnEnd = 12,
                        Weather = ISWEATHER_CLOUDS,
                        IngameX = 33,
                        IngameY = 16,
                        Radius = 10
                    };
                    break;

            }

            return data;

        }

    }
}

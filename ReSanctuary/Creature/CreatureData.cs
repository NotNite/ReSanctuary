using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;

namespace ReSanctuary.Creature
{
    internal class CreatureData
    {
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
                        IngameX = 20.6,
                        IngameY = 23.3,
                        Radius = 35
                    };
                    break;
                case 14907:
                    data = new CreatureExtraData
                    {
                        Name = "Ornery Karakul",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_FAIR,
                        IngameX = 20.6,
                        IngameY = 23.3,
                        Radius = 7
                    };
                    break;
                case 14908:
                    data = new CreatureExtraData
                    {
                        Name = "Opo-Opo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 20.2,
                        IngameY = 25.5,
                        Radius = 47
                    };
                    break;
                case 14909:
                    data = new CreatureExtraData
                    {
                        Name = "Lemur",
                        SpawnStart = 6,
                        SpawnEnd = 9,
                        Weather = 0,
                        IngameX = 20.3,
                        IngameY = 26.5,
                        Radius = 8
                    };
                    break;
                case 14910:
                    data = new CreatureExtraData
                    {
                        Name = "Apkallu",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 16.2,
                        IngameY = 11.9,
                        Radius = 140
                    };
                    break;
                case 14911:
                    data = new CreatureExtraData
                    {
                        Name = "Apkallu of Paradise",
                        SpawnStart = 12,
                        SpawnEnd = 15,
                        Weather = 0,
                        IngameX = 19.1,
                        IngameY = 11.8,
                        Radius = 7
                    };
                    break;
                case 14912:
                    data = new CreatureExtraData
                    {
                        Name = "Ground Squirrel",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 15.9,
                        IngameY = 18.6,
                        Radius = 46
                    };
                    break;
                case 14913:
                    data = new CreatureExtraData
                    {
                        Name = "Star Marmot",
                        SpawnStart = 9,
                        SpawnEnd = 12,
                        Weather = 0,
                        IngameX = 15.7,
                        IngameY = 19.5,
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
                        IngameX = 27.2,
                        IngameY = 18.9,
                        Radius = 123
                    };
                    break;
                case 14915:
                    data = new CreatureExtraData
                    {
                        Name = "Yellow Coblyn",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_FOG,
                        IngameX = 26.9,
                        IngameY = 19.4,
                        Radius = 8
                    };
                    break;
                case 14916:
                    data = new CreatureExtraData
                    {
                        Name = "Wild Dodo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 15.6,
                        IngameY = 14.1,
                        Radius = 100
                    };
                    break;
                case 14917:
                    data = new CreatureExtraData
                    {
                        Name = "Dodo of Paradise",
                        SpawnStart = 15,
                        SpawnEnd = 18,
                        Weather = 0,
                        IngameX = 16.6,
                        IngameY = 11.7,
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
                        IngameX = 20.7,
                        IngameY = 19.0,
                        Radius = 75
                    };
                    break;
                case 14919:
                    data = new CreatureExtraData
                    {
                        Name = "Island Stag",
                        SpawnStart = 18,
                        SpawnEnd = 21,
                        Weather = 0,
                        IngameX = 20.4,
                        IngameY = 20.2,
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
                        IngameX = 13.2,
                        IngameY = 13.8,
                        Radius = 75
                    };
                    break;
                case 14921:
                    data = new CreatureExtraData
                    {
                        Name = "Black Chocobo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_CLEAR,
                        IngameX = 13.1,
                        IngameY = 11.6,
                        Radius = 11
                    };
                    break;
                case 14922:
                    data = new CreatureExtraData
                    {
                        Name = "Glyptodon Pup",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 29.7,
                        IngameY = 10.6,
                        Radius =132
                    };
                    break;
                case 14923:
                    data = new CreatureExtraData
                    {
                        Name = "Glyptodon",
                        SpawnStart = 0,
                        SpawnEnd = 3,
                        Weather = 0,
                        IngameX = 31.9,
                        IngameY = 11.3,
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
                        IngameX = 13.1,
                        IngameY = 18.9,
                        Radius = 129
                    };
                    break;
                case 14925:
                    data = new CreatureExtraData
                    {
                        Name = "Grand Buffalo",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_CLOUDS,
                        IngameX = 12.3,
                        IngameY = 17.7,
                        Radius = 11
                    };
                    break;
                case 14926:
                    data = new CreatureExtraData
                    {
                        Name = "Island Nanny",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = 0,
                        IngameX = 25.1,
                        IngameY = 23.5,
                        Radius = 92
                    };
                    break;
                case 14927:
                    data = new CreatureExtraData
                    {
                        Name = "Island Billy",
                        SpawnStart = 3,
                        SpawnEnd = 6,
                        Weather = 0,
                        IngameX = 26.3,
                        IngameY = 22.9,
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
                        IngameX = 27.4,
                        IngameY = 29.5,
                        Radius = 116
                    };
                    break;
                case 14929:
                    data = new CreatureExtraData
                    {
                        Name = "Gold Back",
                        SpawnStart = 0,
                        SpawnEnd = 0,
                        Weather = ISWEATHER_RAIN,
                        IngameX = 31.2,
                        IngameY = 28.6,
                        Radius = 10
                    };
                    break;
                case 14930:
                    data = new CreatureExtraData
                    {
                        Name = "Twinklefleece",//ToCheck
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
                        IngameX = 18,
                        IngameY = 12.7,
                        Radius = 9
                    };
                    break;
                case 14932:
                    data = new CreatureExtraData
                    {
                        Name = "Paissa",
                        SpawnStart = 12,
                        SpawnEnd = 15,
                        Weather = ISWEATHER_FAIR,
                        IngameX = 24.9,
                        IngameY = 28.4,
                        Radius = 9
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
                        IngameY = 24.1,
                        Radius = 11
                    };
                    break;
                case 14934:
                    data = new CreatureExtraData
                    {
                        Name = "Goobbue",//ToCheck
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

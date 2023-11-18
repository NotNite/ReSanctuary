namespace ReSanctuary.Creature;

internal class CreatureData {
    public const uint WeatherClear = 1;
    public const uint WeatherFair = 2;
    public const uint WeatherClouds = 3;
    public const uint WeatherRain = 7;
    public const uint WeatherShowers = 8;
    public const uint WeatherFog = 4;

    public static CreatureExtraData? GetCreatureExtraData(uint creatureId) => creatureId switch {
        14906 => new CreatureExtraData {
            Name = "Lost Lamb",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 20.6,
            InGameY = 23.3,
            Radius = 35
        },
        14907 => new CreatureExtraData {
            Name = "Ornery Karakul",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherFair,
            InGameX = 20.6,
            InGameY = 23.3,
            Radius = 7
        },
        14908 => new CreatureExtraData {
            Name = "Opo-Opo",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 20.2,
            InGameY = 25.5,
            Radius = 47
        },
        14909 => new CreatureExtraData {
            Name = "Lemur",
            SpawnStart = 6,
            SpawnEnd = 9,
            Weather = null,
            InGameX = 20.3,
            InGameY = 26.5,
            Radius = 8
        },
        14910 => new CreatureExtraData {
            Name = "Apkallu",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 16.2,
            InGameY = 11.9,
            Radius = 140
        },
        14911 => new CreatureExtraData {
            Name = "Apkallu of Paradise",
            SpawnStart = 12,
            SpawnEnd = 15,
            Weather = null,
            InGameX = 19.1,
            InGameY = 11.8,
            Radius = 7
        },
        14912 => new CreatureExtraData {
            Name = "Ground Squirrel",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 15.9,
            InGameY = 18.6,
            Radius = 46
        },
        14913 => new CreatureExtraData {
            Name = "Star Marmot",
            SpawnStart = 9,
            SpawnEnd = 12,
            Weather = null,
            InGameX = 15.7,
            InGameY = 19.5,
            Radius = 10
        },
        14914 => new CreatureExtraData {
            Name = "Coblyn",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 27.2,
            InGameY = 18.9,
            Radius = 123
        },
        14915 => new CreatureExtraData {
            Name = "Yellow Coblyn",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherFog,
            InGameX = 26.9,
            InGameY = 19.4,
            Radius = 8
        },
        14916 => new CreatureExtraData {
            Name = "Wild Dodo",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 15.6,
            InGameY = 14.1,
            Radius = 100
        },
        14917 => new CreatureExtraData {
            Name = "Dodo of Paradise",
            SpawnStart = 15,
            SpawnEnd = 18,
            Weather = null,
            InGameX = 16.6,
            InGameY = 11.7,
            Radius = 10
        },
        14918 => new CreatureExtraData {
            Name = "Island Doe",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 20.7,
            InGameY = 19.0,
            Radius = 75
        },
        14919 => new CreatureExtraData {
            Name = "Island Stag",
            SpawnStart = 18,
            SpawnEnd = 21,
            Weather = null,
            InGameX = 20.4,
            InGameY = 20.2,
            Radius = 10
        },
        14920 => new CreatureExtraData {
            Name = "Chocobo",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 13.2,
            InGameY = 13.8,
            Radius = 75
        },
        14921 => new CreatureExtraData {
            Name = "Black Chocobo",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherClear,
            InGameX = 13.1,
            InGameY = 11.6,
            Radius = 11
        },
        14922 => new CreatureExtraData {
            Name = "Glyptodon Pup",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 29.7,
            InGameY = 10.6,
            Radius = 132
        },
        14923 => new CreatureExtraData {
            Name = "Glyptodon",
            SpawnStart = 0,
            SpawnEnd = 3,
            Weather = null,
            InGameX = 31.9,
            InGameY = 11.3,
            Radius = 10
        },
        14924 => new CreatureExtraData {
            Name = "Aurochs",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 13.1,
            InGameY = 18.9,
            Radius = 129
        },
        14925 => new CreatureExtraData {
            Name = "Grand Buffalo",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherClouds,
            InGameX = 12.3,
            InGameY = 17.7,
            Radius = 11
        },
        14926 => new CreatureExtraData {
            Name = "Island Nanny",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 25.1,
            InGameY = 23.5,
            Radius = 92
        },
        14927 => new CreatureExtraData {
            Name = "Island Billy",
            SpawnStart = 3,
            SpawnEnd = 6,
            Weather = null,
            InGameX = 26.3,
            InGameY = 22.9,
            Radius = 10
        },
        14928 => new CreatureExtraData {
            Name = "Blue Back",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 27.4,
            InGameY = 29.5,
            Radius = 116
        },
        14929 => new CreatureExtraData {
            Name = "Gold Back",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherRain,
            InGameX = 31.2,
            InGameY = 28.6,
            Radius = 10
        },
        14930 => new CreatureExtraData {
            Name = "Twinklefleece",
            SpawnStart = 18,
            SpawnEnd = 21,
            Weather = WeatherFog,
            InGameX = 22.15,
            InGameY = 20.8,
            Radius = 10
        },
        14931 => new CreatureExtraData {
            Name = "Beachcomb",
            SpawnStart = 0,
            SpawnEnd = 3,
            Weather = WeatherRain,
            InGameX = 18,
            InGameY = 12.7,
            Radius = 9
        },
        14932 => new CreatureExtraData {
            Name = "Paissa",
            SpawnStart = 12,
            SpawnEnd = 15,
            Weather = WeatherFair,
            InGameX = 24.9,
            InGameY = 28.4,
            Radius = 9
        },
        14933 => new CreatureExtraData {
            Name = "Alligator",
            SpawnStart = 6,
            SpawnEnd = 9,
            Weather = WeatherShowers,
            InGameX = 17.7,
            InGameY = 24.1,
            Radius = 11
        },
        14934 => new CreatureExtraData {
            Name = "Goobbue",
            SpawnStart = 9,
            SpawnEnd = 12,
            Weather = WeatherClouds,
            InGameX = 32.7,
            InGameY = 16.0,
            Radius = 11
        },
        14935 => new CreatureExtraData {
            Name = "Griffin",
            SpawnStart = 15,
            SpawnEnd = 18,
            Weather = WeatherClear,
            InGameX = 14.8,
            InGameY = 22.6,
            Radius = 12
        },
        16068 => new CreatureExtraData {
            Name = "Tiger of Paradise",
            SpawnStart = 18,
            SpawnEnd = 21,
            Weather = WeatherFair,
            InGameX = 14.9,
            InGameY = 14.0,
            Radius = 11
        },
        16069 => new CreatureExtraData {
            Name = "Morbol Seedling",
            SpawnStart = 3,
            SpawnEnd = 6,
            Weather = WeatherClouds,
            InGameX = 19.4,
            InGameY = 19.0,
            Radius = 10
        },
        14936 => new CreatureExtraData {
            Name = "Quartz Spriggan",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        14937 => new CreatureExtraData {
            Name = "Amethyst Spriggan",
            SpawnStart = 21,
            SpawnEnd = 0,
            Weather = null,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        14938 => new CreatureExtraData {
            Name = "Wild Boar",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = null,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        14939 => new CreatureExtraData {
            Name = "Boar of Paradise",
            SpawnStart = null,
            SpawnEnd = null,
            Weather = WeatherShowers,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        14940 => new CreatureExtraData {
            Name = "Weird Spriggan",
            SpawnStart = 0,
            SpawnEnd = 3,
            Weather = WeatherFog,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16340 => new CreatureExtraData {
            Name = "Funguar",
            SpawnStart = 15,
            SpawnEnd = 18,
            Weather = WeatherRain,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16341 => new CreatureExtraData {
            Name = "Alkonost",
            SpawnStart = 21,
            SpawnEnd = 0,
            Weather = WeatherClear,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16628 => new CreatureExtraData {
            Name = "Morbol",
            SpawnStart = 21,
            SpawnEnd = 0,
            Weather = WeatherShowers,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16629 => new CreatureExtraData {
            Name = "Adamantoise",
            SpawnStart = 12,
            SpawnEnd = 15,
            Weather = WeatherFog,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16630 => new CreatureExtraData {
            Name = "Pteranodon",
            SpawnStart = 9,
            SpawnEnd = 12,
            Weather = WeatherClear,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        16631 => new CreatureExtraData {
            Name = "Grand Doblyn",
            SpawnStart = 3,
            SpawnEnd = 6,
            Weather = WeatherFair,
            InGameX = 0,
            InGameY = 0,
            Radius = 20
        },
        _ => null
    };
}

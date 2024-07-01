using Dalamud.Data;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using ReSanctuary.Windows;
using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets;
using System.Linq;
using Dalamud.Plugin.Services;
using Lumina.Excel;
using ReSanctuary.Creature;

namespace ReSanctuary;

public sealed class Plugin : IDalamudPlugin {
    public string Name => "ReSanctuary";
    private const string CommandName = "/psanctuary";

    [PluginService] public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] public static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] public static IDataManager DataManager { get; private set; } = null!;
    [PluginService] public static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] public static IPluginLog PluginLog { get; private set; } = null!;

    public Configuration Configuration { get; private set; }

    public readonly WindowSystem WindowSystem = new("ReSanctuary");
    public readonly MainWindow MainWindow;
    public readonly WidgetWindow WidgetWindow;

    public readonly List<GatheringItem> GatheringItems;
    public readonly List<WorkshopItem> WorkshopItems;
    public readonly List<CreatureItem> CreatureItems;
    public readonly Dictionary<uint, string> CreatureItemDrops;
    public readonly Dictionary<uint, Weather> WeatherList;

    // ReSharper disable InconsistentNaming
    public readonly ExcelSheet<TerritoryType> TerritoryTypeSheet;
    public readonly ExcelSheet<Item> ItemSheet;
    public readonly RawExcelSheet MJIItemPouchSheet;
    // ReSharper restore InconsistentNaming

    public readonly TerritoryType IslandSanctuary;

    public Plugin() {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);

        this.MainWindow = new MainWindow(this);
        this.WidgetWindow = new WidgetWindow(this);
        this.WindowSystem.AddWindow(this.MainWindow);
        this.WindowSystem.AddWindow(this.WidgetWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
            HelpMessage = "Opens the main ReSanctuary interface."
        });

        PluginInterface.UiBuilder.Draw += this.DrawUi;
        PluginInterface.UiBuilder.OpenConfigUi += this.DrawConfigUi;
        PluginInterface.UiBuilder.OpenMainUi += this.DrawConfigUi;

        this.TerritoryTypeSheet = DataManager.GetExcelSheet<TerritoryType>()!;
        this.ItemSheet = DataManager.GetExcelSheet<Item>()!;
        this.MJIItemPouchSheet = DataManager.Excel.GetSheetRaw("MJIItemPouch")!;

        this.IslandSanctuary = this.TerritoryTypeSheet.First(x => x.Name == "h1m2");

        this.GatheringItems = Utils.GetSortedGatheringItems();
        this.WorkshopItems = Utils.GetSortedWorkshopItems();
        this.CreatureItems = Utils.GetCreatureItems();
        this.CreatureItemDrops = Utils.SeparateCreatureDrops(this.CreatureItems);
        this.WeatherList = Utils.GetSanctuaryWeathers();
    }

    public void Dispose() {
        WindowSystem.RemoveAllWindows();
        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args) {
        switch (args) {
            case "widget":
                this.WidgetWindow.IsOpen ^= true;
                break;

            default:
                this.MainWindow.IsOpen ^= true;
                break;
        }
    }

    private void DrawUi() {
        WindowSystem.Draw();
    }

    public void DrawConfigUi() {
        this.MainWindow.IsOpen = true;
    }
}

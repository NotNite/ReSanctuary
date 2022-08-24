using Dalamud.Data;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using ReSanctuary.Windows;

namespace ReSanctuary; 

public sealed class Plugin : IDalamudPlugin {
    public string Name => "ReSanctuary";
    private const string CommandName = "/psanctuary";

    [PluginService] public static DalamudPluginInterface PluginInterface { get; private set; }
    [PluginService] public static CommandManager CommandManager { get; private set; }
    [PluginService] public static DataManager DataManager { get; private set; }
    
    public Configuration Configuration { get; private set; }
    public WindowSystem WindowSystem = new("ReSanctuary");

    public Plugin() {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);
        
        WindowSystem.AddWindow(new ConfigWindow(this));
        WindowSystem.AddWindow(new MainWindow(this));

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
            HelpMessage = "Opens the main ReSanctuary interface."
        });

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
    }

    public void Dispose() {
        WindowSystem.RemoveAllWindows();
        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args) {
        WindowSystem.GetWindow("ReSanctuary").IsOpen = true;
    }

    private void DrawUI() {
        WindowSystem.Draw();
    }

    public void DrawConfigUI() {
        WindowSystem.GetWindow("ReSanctuary Config").IsOpen = true;
    }
}

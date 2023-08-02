namespace ReSanctuary.Windows; 

public abstract class MainWindowTab {
    protected readonly Plugin Plugin;
    public readonly string Name;

    protected MainWindowTab(Plugin plugin, string name) {
        this.Plugin = plugin;
        this.Name = name;
    }
    
    public abstract void Draw();
}

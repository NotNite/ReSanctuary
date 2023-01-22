using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;

namespace ReSanctuary;

[Serializable]
public class Configuration : IPluginConfiguration {
    public int Version { get; set; } = 1;

    public Dictionary<uint, int> TodoList { get; set; } = new();
    public Dictionary<uint, DateTime> TodoListCreature { get; set; } = new();
    public List<uint> CreatureFilterHide { get; set; } = new();
    public bool LockWidget { get; set; }
    


    // the below exist just to make saving less cumbersome
    [NonSerialized] private DalamudPluginInterface? PluginInterface;

    public void Initialize(DalamudPluginInterface pluginInterface) {
        PluginInterface = pluginInterface;
    }

    public void Save() {
        PluginInterface!.SavePluginConfig(this);
    }
}

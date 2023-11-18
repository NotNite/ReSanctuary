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

    [NonSerialized] private DalamudPluginInterface? pluginInterface;

    public void Initialize(DalamudPluginInterface pi) {
        this.pluginInterface = pi;
    }

    public void Save() {
        this.pluginInterface!.SavePluginConfig(this);
    }
}

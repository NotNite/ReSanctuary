using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Style;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ReSanctuary.Windows;

public class WidgetWindow : Window, IDisposable {
    private Plugin Plugin;
    
    // am i lazy for having these in two places? yes
    // do i care? no
    private RawExcelSheet itemPouchSheet;
    private ExcelSheet<Item> itemSheet;
    
    private Dictionary<uint, TextureWrap> todoTextureCache;

    public WidgetWindow(Plugin plugin) : base("ReSanctuary Widget") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(150, 75) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
   
        Plugin = plugin;
        
        itemPouchSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIItemPouch");
        itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();
        
        todoTextureCache = new();
    }

    public void Dispose() { }

    public override void PreDraw() {
        if (Plugin.Configuration.LockWidget) {
            Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar;
        } else {
            Flags = ImGuiWindowFlags.None;
        }
    }

    private void DrawGarbage(uint id, int amount, Item item, int has) {
        TextureWrap? icon;
        if (todoTextureCache.ContainsKey(id)) {
            icon = todoTextureCache[id]; 
        } else {
            icon = Plugin.DataManager.GetImGuiTextureIcon(item.Icon);
            todoTextureCache[id] = icon;
        }
            
        var iconSize = ImGui.GetTextLineHeight() * 1.25f;
        var iconSizeVec = new Vector2(iconSize, iconSize);
        ImGui.Image(icon.ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

        ImGui.SameLine();
        
        if (has >= amount) {
            ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
            ImGui.Text($"{item.Name} - {has}/{amount}");
            ImGui.PopStyleColor();
        } else {
            ImGui.Text($"{item.Name} - {has}/{amount}");
        }
    }

    public override void Draw() {
        var todoList = Plugin.Configuration.TodoList;
        
        // i just got home from class and i am exhausted. here is some very bad unoptimized code\
        foreach (var (id, amount) in todoList) {
            var itemPouchItem = itemPouchSheet.GetRow(id);
            var item = itemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0));
            var has = Utils.GetStackSize(item.RowId);
            
            if (has < amount) DrawGarbage(id, amount, item, has);
        }
        
        foreach (var (id, amount) in todoList) {
            var itemPouchItem = itemPouchSheet.GetRow(id);
            var item = itemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0));
            var has = Utils.GetStackSize(item.RowId);
            
            if (has >= amount) DrawGarbage(id, amount, item, has);
        }
    }
}

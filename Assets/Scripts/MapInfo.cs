using System.Collections.Generic;
using UnityEngine;

public static class MapInfo
{
    private static Dictionary<string, string> mapNames = new Dictionary<string, string>()
    {
        { "Map1", "Chapter 1: The Lobby" },
        { "Map2", "Chapter 2: Abandoned Office" },
        { "Map3", "Chapter 3: Electrical Station" },
        { "Map4", "Chapter 4: Pipe Dreams" },
        // Thêm các map khác ? ðây
    };

    private static Dictionary<string, string> mapTips = new Dictionary<string, string>()
    {
        { "Map1","Search for notes, symbols, or other clues that might guide you to the door leading to the exit."},

        { "Map2", "Carefully search every area to ensure you don't miss any sticky notes scattered throughout the map." },

        { "Map3", "Search for and attach the fuse to the switch to open the door. The fuses are scattered throughout the map, " +
            "possibly on shelves, in drawers, or under items. Carefully check every nook and cranny to ensure you find all the necessary fuses." },

        { "Map4", "Find your way through the underground piping system to reach the end and escape the dream to wake up. " +
            "Navigate through obstacles such as blocked drains, barriers, or debris in the pipes." },
        // Thêm các tips khác ? ðây
    };

    private static Dictionary<string, string> mapBackgrounds = new Dictionary<string, string>()
    {
        { "Map1", "Backgrounds/Chapter1" },
        { "Map2", "Backgrounds/Chapter2" },
        { "Map3", "Backgrounds/Chapter3" },
        { "Map4", "Backgrounds/Chapter4" },
        // Thêm các ðý?ng d?n t?i background image khác ? ðây
    };

    public static string GetMapName(string sceneName)
    {
        if (mapNames.ContainsKey(sceneName))
        {
            return mapNames[sceneName];
        }
        return "Unknown Map";
    }

    public static string GetMapTips(string sceneName)
    {
        if (mapTips.ContainsKey(sceneName))
        {
            return mapTips[sceneName];
        }
        return "No tips available for this map.";
    }

    public static string GetMapBackground(string sceneName)
    {
        if (mapBackgrounds.ContainsKey(sceneName))
        {
            return mapBackgrounds[sceneName];
        }
        return null;
    }
}

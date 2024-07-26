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
        // Thêm các map khác ? đây
    };

    private static Dictionary<string, string> mapTips = new Dictionary<string, string>()
    {
        { "Map1","<color=red>Tip:</color><br><br><line-height=170%>Search for notes, symbols, or other clues that might guide you to the door leading to the exit."},

        { "Map2", "<color=red>Tip:</color><br><br><line-height=170%>Carefully search every area to ensure you don't miss any sticky notes scattered throughout the map." },

        { "Map3", "<color=red>Tip:</color><br><br><line-height=170%>Search for and attach the fuse to the switch to open the door. The fuses are scattered throughout the map, " +
            "possibly on shelves, in drawers, or under items. Carefully check every nook and cranny to ensure you find all the necessary fuses." },

        { "Map4", "<color=red>Tip:</color><br><br><line-height=170%>Find your way through the underground piping system to reach the end and escape the dream to wake up. " +
            "Navigate through obstacles such as blocked drains, barriers, or debris in the pipes." },
        // Thêm các tips khác ? đây
    };

    private static Dictionary<string, string> mapBackgrounds = new Dictionary<string, string>()
    {
        { "Map1", "Backgrounds/Map1" },
        { "Map2", "Backgrounds/Map2" },
        { "Map3", "Backgrounds/Map3" },
        { "Map4", "Backgrounds/Map4" },
        // Thêm các đư?ng d?n t?i background image khác ? đây
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

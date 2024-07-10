using System.Collections.Generic;
using UnityEngine;

public static class MapInfo
{
    private static Dictionary<string, string> mapNames = new Dictionary<string, string>()
    {
        { "Chapter1", "Chapter 1: The Beginning" },
        { "Chapter2", "Chapter 2: The Adventure Continues" },
        // Thêm các map khác ? ðây
    };

    private static Dictionary<string, string> mapTips = new Dictionary<string, string>()
    {
        { "Chapter1", "Tip: Explore the area to find hidden items." },
        { "Chapter2", "Tip: Watch out for enemies around the corners." },
        // Thêm các tips khác ? ðây
    };

    private static Dictionary<string, string> mapBackgrounds = new Dictionary<string, string>()
    {
        { "Chapter1", "Backgrounds/Chapter1" },
        { "Chapter2", "Backgrounds/Chapter2" },
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

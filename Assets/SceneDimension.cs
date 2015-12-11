using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class SceneDimension
{
    public static List<string> playbackScene = new List<string>();
    
    public static void Back()
    {
        string backScene = playbackScene[0];
        playbackScene.Clear();
        Application.LoadLevel(backScene);        
    }
}

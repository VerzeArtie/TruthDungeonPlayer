using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace DungeonPlayer
{
    public static class SceneDimension
    {
        public static List<string> playbackScene = new List<string>();

        public static void Go(string src, string dst)
        {
            playbackScene.Add(src);
            Application.LoadLevel(dst);
        }

        public static void Replace(string dst)
        {
            Application.LoadLevel(dst);
        }
        public static void Back()
        {
            if (playbackScene.Count <= 0)
            {
                return;
            }

            string backScene = playbackScene[playbackScene.Count - 1];
            playbackScene.Remove(backScene);
            Application.LoadLevel(backScene);
        }

        public static void JumpToTitle()
        {
            playbackScene.Clear();
            Application.LoadLevel(Database.Title);
        }
    }
}
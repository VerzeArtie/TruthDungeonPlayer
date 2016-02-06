using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace DungeonPlayer
{
    public static class SceneDimension
    {
        public static List<string> playbackScene = new List<string>();

        public static void CallTruthSelectEquipment(string src, int equipType)
        {
            GroundOne.EquipType = equipType;
            SceneDimension.Go(src, Database.TruthSelectEquipment);
        }

        public static void CallTruthHomeTown(string src)
        {
            SceneDimension.Go(src, Database.TruthHomeTown);
        }

        public static void CallSaveLoad(string src, bool saveMode, bool afterBacktoTitle, bool title_LoadAndGo)
        {
            GroundOne.SaveMode = saveMode;
            GroundOne.AfterBacktoTitle = afterBacktoTitle;
            GroundOne.Title_LoadAndGo = title_LoadAndGo;
            SceneDimension.Go(src, Database.SaveLoad);
        }
        public static void CallTruthDungeon(string src)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            SceneDimension.Go(src, Database.TruthDungeon);
        }

        public static void CallTruthBattleEnemy(string src, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            GroundOne.Battle_DuelMode = duel;
            GroundOne.HiSpeedAnimation = hiSpeed;
            GroundOne.FinalBattle = final;
            GroundOne.LifeCountBattle = lifecount;
            GroundOne.BattleResult = GroundOne.battleResult.None;
            SceneDimension.Go(src, Database.TruthBattleEnemy);
        }

        public static void CallTruthStatusPlayer(string src)
        {
            GroundOne.LevelUp = false;
            GroundOne.UpPoint = 0;
            GroundOne.CumultiveLvUpValue = 0;
            SceneDimension.Go(src, Database.TruthStatusPlayer);
        }
        public static void CallTruthStatusPlayer(string src, ref bool leveUp, ref int upPoint, ref int cumultivaLvUpValue, Color color)
        {
            GroundOne.LevelUp = leveUp;
            GroundOne.UpPoint = upPoint;
            GroundOne.CumultiveLvUpValue = cumultivaLvUpValue;
            GroundOne.CurrentStatusView = color;
            leveUp = false;
            upPoint = 0;
            cumultivaLvUpValue = 0;
            SceneDimension.Go(src, Database.TruthStatusPlayer);
        }

        public static void CallTruthEquipmentShop(string src)
        {
            SceneDimension.Go(src, Database.TruthEquipmentShop);
        }

        public static void CallTruthBattleSetting(string src)
        {
            SceneDimension.Go(src, Database.TruthBattleSetting);
        }

        private static void Go(string src, string dst)
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

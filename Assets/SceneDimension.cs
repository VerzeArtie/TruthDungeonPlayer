using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public static class SceneDimension
    {
        public static List<string> playbackScene = new List<string>();

        public static void CallRequestFood(string src, MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            //scene.Filter.SetActive(true);
            Application.LoadLevelAdditive(Database.TruthRequestFood);
        }
        public static void CallSaveLoad(string src, bool SaveMode, bool AfterBacktoTitle, MotherForm scene)
        {
            GroundOne.SaveMode = SaveMode;
            GroundOne.AfterBacktoTitle = AfterBacktoTitle;
            GroundOne.ParentScene = scene;
            if (SaveMode)
            {
                scene.Filter.GetComponent<Image>().color = UnityColor.Salmon;
            }
            else
            {
                scene.Filter.GetComponent<Image>().color = UnityColor.Aqua;
            }
            scene.Filter.SetActive(true);
            Application.LoadLevelAdditive(Database.SaveLoad);
        }

        public static void JumpToTruthHomeTown(string src)
        {
            playbackScene.Clear();
            SceneDimension.Go(src, Database.TruthHomeTown);
        }

        public static void JumpToTruthDungeon(string src)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            playbackScene.Clear();
            SceneDimension.Go(src, Database.TruthDungeon);
        }

        public static void JumpToTitle()
        {
            playbackScene.Clear();
            Application.LoadLevel(Database.Title);
        }

        public static void CallTruthSelectEquipment(string src, int equipType)
        {
            GroundOne.EquipType = equipType;
            SceneDimension.Go(src, Database.TruthSelectEquipment);
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

        public static void CallTruthStatusPlayer(string src, MotherForm scene, bool onlySelectTrash, string itemName)
        {
            GroundOne.OnlySelectTrash = onlySelectTrash;
            GroundOne.CannotSelectTrash = itemName;
            GroundOne.LevelUp = false;
            GroundOne.UpPoint = 0;
            GroundOne.CumultiveLvUpValue = 0;
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthStatusPlayer);
        }
        public static void CallTruthStatusPlayer(string src)
        {
            GroundOne.OnlySelectTrash = false;
            GroundOne.LevelUp = false;
            GroundOne.UpPoint = 0;
            GroundOne.CumultiveLvUpValue = 0;
            GroundOne.ParentScene = null;
            SceneDimension.Go(src, Database.TruthStatusPlayer);
        }
        public static void CallTruthStatusPlayer(string src, ref bool leveUp, ref int upPoint, ref int cumultivaLvUpValue, Color color)
        {
            GroundOne.OnlySelectTrash = false;
            GroundOne.LevelUp = leveUp;
            GroundOne.UpPoint = upPoint;
            GroundOne.CumultiveLvUpValue = cumultivaLvUpValue;
            GroundOne.CurrentStatusView = color;
            GroundOne.ParentScene = null;
            leveUp = false;
            upPoint = 0;
            cumultivaLvUpValue = 0;
            SceneDimension.Go(src, Database.TruthStatusPlayer);
        }

        public static void CallTruthSkillSpellDesc(MotherForm parent, string playerName, string commandName)
        {
            GroundOne.ParentScene = parent;
            GroundOne.playerName = playerName;
            GroundOne.SpellSkillName = commandName;
            Application.LoadLevelAdditive(Database.TruthSkillSpellDesc);
        }

        public static void CallTruthEquipmentShop(string src)
        {
            SceneDimension.Go(src, Database.TruthEquipmentShop);
        }

        public static void CallTruthBattleSetting(string src)
        {
            GroundOne.CallBattleSettingFromBattleEnemy = false;
            GroundOne.ParentScene = null;
            SceneDimension.Go(src, Database.TruthBattleSetting);
        }
        public static void CallTruthBattleSetting(string src, TruthBattleEnemy scene)
        {
            GroundOne.CallBattleSettingFromBattleEnemy = true;
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthBattleSetting);
        }

        private static void Go(string src, string dst)
        {
            playbackScene.Add(src);
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

        public static void LoadGame()
        {
            playbackScene.Clear();
            playbackScene.Add(Database.Title);
        }
    }
}

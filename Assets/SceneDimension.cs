using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public static class SceneDimension
    {
        public static void JumpToTruthHomeTown()
        {
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.TruthHomeTown);
        }

        public static void JumpToTruthDungeon(bool gotoDownstair)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            GroundOne.GotoDownstair = gotoDownstair;
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.TruthDungeon);
        }

        public static void JumpToTitle()
        {
            GroundOne.ReInitializeGroundOne();
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.Title);
        }

        public static void CallTruthDuelPlayerStatus(MotherForm scene, string duelPlayerName)
        {
            GroundOne.ParentScene = scene;
            GroundOne.DuelPlayerName = duelPlayerName;
            Application.LoadLevelAdditive(Database.TruthDuelPlayerStatus);
        }

        public static void CallTruthDuelRule(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthDuelRule);
        }

        public static void CallTruthItemDesc(MotherForm scene, string itemNameTitle)
        {
            GroundOne.ParentScene = scene;
            GroundOne.ItemNameTitle = itemNameTitle;
            Application.LoadLevelAdditive(Database.TruthItemDesc);
        }

        public static void CallTruthDecision(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthDecision);
        }

        public static void CallDuelRule(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthDuelRule);
        }
        
        public static void CallRequestFood(string src, MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthRequestFood);
        }
        
        public static void CallItemBank(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthItemBank);
        }

        public static void CallPotionShop(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthPotionShop);
        }

        public static void CallSaveLoadWithSaveOnly()
        {
            GroundOne.SaveMode = true;
            GroundOne.SaveAndExit = true;
            Application.LoadLevelAdditive(Database.SaveLoad);
        }

        public static void CallSaveLoad(MotherForm scene, bool SaveMode, bool AfterBacktoTitle)
        {
            GroundOne.SaveMode = SaveMode;
            GroundOne.AfterBacktoTitle = AfterBacktoTitle;
            GroundOne.ParentScene = scene;
            GroundOne.SaveAndExit = false;
            scene.Filter.SetActive(true);
            Application.LoadLevelAdditive(Database.SaveLoad);
        }

        public static void CallTruthSelectEquipment(MotherForm scene, int equipType)
        {
            GroundOne.EquipType = equipType;
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthSelectEquipment);
        }

        public static void CallTruthBattleEnemy(MotherForm scene, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            GroundOne.DuelMode = duel;
            GroundOne.HiSpeedAnimation = hiSpeed;
            GroundOne.FinalBattle = final;
            GroundOne.LifeCountBattle = lifecount;
            GroundOne.BattleResult = GroundOne.battleResult.None;
            GroundOne.ParentScene = scene;
            GroundOne.StopDungeonMusic();
            System.Threading.Thread.Sleep(500);
            Application.LoadLevelAdditive(Database.TruthBattleEnemy);
        }

        public static void CallTruthStatusPlayer(MotherForm scene, bool onlySelectTrash, string itemName)
        {
            GroundOne.OnlySelectTrash = onlySelectTrash;
            GroundOne.CannotSelectTrash = itemName;
            GroundOne.LevelUp = false;
            GroundOne.UpPoint = 0;
            GroundOne.CumultiveLvUpValue = 0;
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthStatusPlayer);
        }
        public static void CallTruthStatusPlayer(MotherForm scene, ref bool leveUp, ref int upPoint, ref int cumultivaLvUpValue, Color color)
        {
            GroundOne.OnlySelectTrash = false;
            GroundOne.LevelUp = leveUp;
            GroundOne.UpPoint = upPoint;
            GroundOne.CumultiveLvUpValue = cumultivaLvUpValue;
            GroundOne.CurrentStatusView = color;
            GroundOne.ParentScene = scene;
            leveUp = false;
            upPoint = 0;
            cumultivaLvUpValue = 0;

            Application.LoadLevelAdditive(Database.TruthStatusPlayer);
        }

        public static void CallTruthSkillSpellDesc(MotherForm parent, string playerName, string commandName)
        {
            GroundOne.ParentScene = parent;
            GroundOne.playerName = playerName;
            GroundOne.SpellSkillName = commandName;
            Application.LoadLevelAdditive(Database.TruthSkillSpellDesc);
        }

        public static void CallTruthEquipmentShop(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthEquipmentShop);
        }

        public static void CallTruthBattleSetting(MotherForm scene)
        {
            GroundOne.ParentScene = scene;
            Application.LoadLevelAdditive(Database.TruthBattleSetting);
        }

        public static void Back(MotherForm scene)
        {
            Debug.Log("back name: " + scene.GetType().Name);
            string sceneName = scene.GetType().Name;
            if (GroundOne.ParentScene != null)
            {
                GroundOne.ParentScene.SceneBack();
            }
            Application.UnloadLevel(sceneName);
        }
    }
}

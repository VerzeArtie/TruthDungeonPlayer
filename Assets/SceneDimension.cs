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
            GroundOne.WE.DungeonPosX = 0;
            GroundOne.WE.DungeonPosY = 0;
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.TruthHomeTown);
            GroundOne.Parent.Clear();
        }

        public static void JumpToTruthDungeon(bool gotoDownstair)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            GroundOne.GotoDownstair = gotoDownstair;
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.TruthDungeon);
            GroundOne.Parent.Clear();
        }

        public static void JumpToTitle()
        {
            GroundOne.ReInitializeGroundOne(false);
            GroundOne.StopDungeonMusic();
            Application.LoadLevel(Database.Title);
            GroundOne.Parent.Clear();
        }

        public static void CallTruthChoiceStatue(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthChoiceStatue);
        }

        public static void CallTruthWill(MotherForm scene)
        {
            GroundOne.GodSeuqence = false;
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthWill);
        }

        public static void CallTruthAnswer(MotherForm scene)
        {
            GroundOne.GodSeuqence = false;
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthAnswer);
        }

        public static void CallTruthInputRequest(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthRequestInput);
        }

        public static void CallTruthDuelPlayerStatus(MotherForm scene, string duelPlayerName)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.DuelPlayerName = duelPlayerName;
            Application.LoadLevelAdditive(Database.TruthDuelPlayerStatus);
        }

        public static void CallTruthDuelRule(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthDuelRule);
        }

        public static void CallTruthItemDesc(MotherForm scene, string itemNameTitle)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.ItemNameTitle = itemNameTitle;
            Application.LoadLevelAdditive(Database.TruthItemDesc);
        }

        public static void CallTruthDecision(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthDecision);
        }

        public static void CallTruthDecision2(MotherForm scene, string message, string textTop, string textLeft, string textRight, string textBottom, bool permutation)
        {
            GroundOne.Decision2_Message = message;
            GroundOne.Decision2_TopText = textTop;
            GroundOne.Decision2_LeftText = textLeft;
            GroundOne.Decision2_RightText = textRight;
            GroundOne.Decision2_BottomText = textBottom;
            GroundOne.Decision2_SelectPermutation = permutation;
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthDecision2);
        }

        public static void CallDuelRule(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthDuelRule);
        }
        
        public static void CallRequestFood(string src, MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthRequestFood);
        }
        
        public static void CallItemBank(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthItemBank);
        }

        public static void CallPotionShop(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
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
            GroundOne.Parent.Add(scene);
            GroundOne.SaveAndExit = false;
            scene.Filter.SetActive(true);
            Application.LoadLevelAdditive(Database.SaveLoad);
        }

        public static void CallTruthSelectEquipment(MotherForm scene, int equipType, MainCharacter targetPlayer)
        {
            GroundOne.EquipType = equipType;
            GroundOne.TargetPlayer = targetPlayer;
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthSelectEquipment);
        }

        public static void CallTruthBattleEnemy(MotherForm scene, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            GroundOne.DuelMode = duel;
            GroundOne.HiSpeedAnimation = hiSpeed;
            GroundOne.FinalBattle = final;
            GroundOne.LifeCountBattle = lifecount;
            GroundOne.BattleResult = GroundOne.battleResult.None;
            GroundOne.Parent.Add(scene);
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
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthStatusPlayer);
        }
        public static void CallTruthStatusPlayer(MotherForm scene, ref bool leveUp, ref int upPoint, ref int cumultivaLvUpValue, Color color)
        {
            GroundOne.OnlySelectTrash = false;
            GroundOne.LevelUp = leveUp;
            GroundOne.UpPoint = upPoint;
            GroundOne.CumultiveLvUpValue = cumultivaLvUpValue;
            GroundOne.CurrentStatusColor = color;
            GroundOne.Parent.Add(scene);
            leveUp = false;
            upPoint = 0;
            cumultivaLvUpValue = 0;

            Application.LoadLevelAdditive(Database.TruthStatusPlayer);
        }

        public static void CallTruthSkillSpellDesc(MotherForm scene, string playerName, string commandName)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.playerName = playerName;
            GroundOne.SpellSkillName = commandName;
            Application.LoadLevelAdditive(Database.TruthSkillSpellDesc);
        }

        public static void CallTruthEquipmentShop(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthEquipmentShop);
        }

        public static void CallTruthBattleSetting(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            Application.LoadLevelAdditive(Database.TruthBattleSetting);
        }

        public static void Back(MotherForm scene)
        {
            Debug.Log("back name: " + scene.GetType().Name);
            string sceneName = scene.GetType().Name;
            if (GroundOne.Parent.Count > 0)
            {
                GroundOne.Parent[GroundOne.Parent.Count - 1].SceneBack();
                GroundOne.Parent.RemoveAt(GroundOne.Parent.Count - 1);
            }
            Application.UnloadLevel(sceneName);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DungeonPlayer
{
    public static class SceneDimension
    {
        public static void JumpToTruthHomeTown()
        {
            GroundOne.WE.DungeonPosX = 0;
            GroundOne.WE.DungeonPosY = 0;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadSceneAsync(Database.TruthHomeTown);
            GroundOne.Parent.Clear();
        }
        public static void JumpToSingleHomeTown()
        {
            GroundOne.WE.DungeonPosX = 0;
            GroundOne.WE.DungeonPosY = 0;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadSceneAsync(Database.SingleHomeTown);
            GroundOne.Parent.Clear();
        }
        public static void JumpToTruthDungeon(bool gotoDownstair)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            GroundOne.GotoDownstair = gotoDownstair;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadSceneAsync(Database.TruthDungeon);
            GroundOne.Parent.Clear();
        }

        public static void JumpToSingleDungeon(bool gotoDownstair)
        {
            GroundOne.WE.AlreadyShownEvent = false;
            GroundOne.GotoDownstair = gotoDownstair;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadSceneAsync(Database.SingleDungeon);
            GroundOne.Parent.Clear();
        }

        public static void JumpToTitle()
        {
            GroundOne.ReInitializeGroundOne(false);
            GroundOne.StopDungeonMusic();
            SceneManager.LoadSceneAsync(Database.Title);
            GroundOne.Parent.Clear();
        }

        public static void CallTutorial(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.Tutorial, LoadSceneMode.Additive);
        }

        public static void CallGameSetting( MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.GameSetting, LoadSceneMode.Additive);
        }

		public static void CallDungeonTicket(MotherForm scene)
		{
			GroundOne.Parent.Add (scene);
            SceneManager.LoadSceneAsync(Database.DungeonTicket, LoadSceneMode.Additive);
		}

        public static void CallTruthBattleEnemy(string sceneName, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            GroundOne.DuelMode = duel;
            GroundOne.HiSpeedAnimation = hiSpeed;
            GroundOne.FinalBattle = final;
            GroundOne.LifeCountBattle = lifecount;
            GroundOne.BattleResult = GroundOne.battleResult.None;
            GroundOne.SceneName = sceneName;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadScene(Database.TruthBattleEnemy);
            GroundOne.Parent.Clear();
        }
        public static void CallSingleBattleEnemy(string sceneName, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            GroundOne.DuelMode = duel;
            GroundOne.HiSpeedAnimation = hiSpeed;
            GroundOne.FinalBattle = final;
            GroundOne.LifeCountBattle = lifecount;
            GroundOne.BattleResult = GroundOne.battleResult.None;
            GroundOne.SceneName = sceneName;
            GroundOne.StopDungeonMusic();
            SceneManager.LoadScene(Database.SingleBattleEnemy);
            GroundOne.Parent.Clear();
        }

        public static void CallBackBattleEnemy()
        {
            string target = GroundOne.SceneName;
            GroundOne.SceneName = string.Empty;
            SceneManager.LoadScene(target);
        }
        public static void CallBackSingleBattleEnemy()
        {
            string target = GroundOne.SceneName;
            GroundOne.SceneName = string.Empty;
            SceneManager.LoadScene(target);
        }

        public static void CallTruthPlayBack(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthPlayback, LoadSceneMode.Additive);
        }

        public static void CallAchievement(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthAchievement, LoadSceneMode.Additive);
        }

        public static void CallTruthBookManual(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthInformation, LoadSceneMode.Additive);
        }

        public static void CallTruthSelectCharacter(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthSelectCharacter, LoadSceneMode.Additive);
        }

        public static void CallTruthChoiceStatue(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthChoiceStatue, LoadSceneMode.Additive);
        }

        public static void CallTruthWill(MotherForm scene)
        {
            GroundOne.GodSeuqence = false;
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthWill, LoadSceneMode.Additive);
        }

        public static void CallTruthAnswer(MotherForm scene)
        {
            GroundOne.GodSeuqence = false;
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthAnswer, LoadSceneMode.Additive);
        }

        public static void CallTruthInputRequest(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthRequestInput, LoadSceneMode.Additive);
        }

        public static void CallTruthDuelPlayerStatus(MotherForm scene, string duelPlayerName)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.DuelPlayerName = duelPlayerName;
            SceneManager.LoadSceneAsync(Database.TruthDuelPlayerStatus, LoadSceneMode.Additive);
        }

        public static void CallTruthDuelRule(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthDuelRule, LoadSceneMode.Additive);
        }

        public static void CallTruthItemDesc(MotherForm scene, string itemNameTitle)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.ItemNameTitle = itemNameTitle;
            SceneManager.LoadSceneAsync(Database.TruthItemDesc, LoadSceneMode.Additive);
        }

        public static void CallTruthDecision(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthDecision, LoadSceneMode.Additive);
        }

        public static void CallTruthDecision3(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthDecision3, LoadSceneMode.Additive);
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
            SceneManager.LoadSceneAsync(Database.TruthDecision2, LoadSceneMode.Additive);
        }

        public static void CallDuelRule(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthDuelRule, LoadSceneMode.Additive);
        }
        
        public static void CallRequestFood(string src, MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthRequestFood, LoadSceneMode.Additive);
        }
        
        public static void CallItemBank(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthItemBank, LoadSceneMode.Additive);
        }

        public static void CallPotionShop(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthPotionShop, LoadSceneMode.Additive);
        }

        public static void CallSaveLoadWithSaveOnly()
        {
            GroundOne.SaveMode = true;
            GroundOne.SaveAndExit = true;
            SceneManager.LoadSceneAsync(Database.SaveLoad, LoadSceneMode.Additive);
        }

        public static void CallSaveLoad(MotherForm scene, bool SaveMode, bool AfterBacktoTitle)
        {
            GroundOne.SaveMode = SaveMode;
            GroundOne.AfterBacktoTitle = AfterBacktoTitle;
            GroundOne.Parent.Add(scene);
            GroundOne.SaveAndExit = false;
            scene.Filter.SetActive(true);
            SceneManager.LoadSceneAsync(Database.SaveLoad, LoadSceneMode.Additive);
        }

        public static void CallTruthSelectEquipment(MotherForm scene, int equipType, MainCharacter targetPlayer)
        {
            GroundOne.EquipType = equipType;
            GroundOne.TargetPlayer = targetPlayer;
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthSelectEquipment, LoadSceneMode.Additive);
        }

        public static void CallTruthStatusPlayer(MotherForm scene, bool onlySelectTrash, string newItem, string itemName)
        {
            GroundOne.OnlySelectTrash = onlySelectTrash;
            GroundOne.OnlySelectTrashNewItem = newItem;
            GroundOne.CannotSelectTrash = itemName;
            GroundOne.LevelUpCharacter = GroundOne.MC.FullName;
            GroundOne.LevelUp = false;
            GroundOne.UpPoint = 0;
            GroundOne.CumultiveLvUpValue = 0;
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthStatusPlayer, LoadSceneMode.Additive);
        }
        public static void CallTruthStatusPlayer(MotherForm scene, ref bool leveUp, ref int upPoint, ref int cumultivaLvUpValue, string characterName)
        {
            GroundOne.OnlySelectTrash = false;
            GroundOne.OnlySelectTrashNewItem = string.Empty;
            GroundOne.CannotSelectTrash = string.Empty;
            GroundOne.LevelUp = leveUp;
            GroundOne.LevelUpCharacter = characterName;
            GroundOne.UpPoint = upPoint;
            GroundOne.CumultiveLvUpValue = cumultivaLvUpValue;
            GroundOne.Parent.Add(scene);
            leveUp = false;
            upPoint = 0;
            cumultivaLvUpValue = 0;

            SceneManager.LoadSceneAsync(Database.TruthStatusPlayer, LoadSceneMode.Additive);
        }

        public static void CallTruthSkillSpellDesc(MotherForm scene, string playerName, string commandName)
        {
            GroundOne.Parent.Add(scene);
            GroundOne.playerName = playerName;
            GroundOne.SpellSkillName = commandName;
            SceneManager.LoadSceneAsync(Database.TruthSkillSpellDesc, LoadSceneMode.Additive);
        }

        public static void CallTruthEquipmentShop(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthEquipmentShop, LoadSceneMode.Additive);
        }

        public static void CallTruthBattleSetting(MotherForm scene)
        {
            GroundOne.Parent.Add(scene);
            SceneManager.LoadSceneAsync(Database.TruthBattleSetting, LoadSceneMode.Additive);
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
            SceneManager.UnloadSceneAsync(sceneName);
        }
        
        public static void BackSuddenly(MotherForm scene)
        {
            SceneManager.UnloadSceneAsync(scene.GetType().Name);

            if (GroundOne.Parent.Count > 0)
            {
                MotherForm current = GroundOne.Parent[GroundOne.Parent.Count - 1];
                GroundOne.Parent.RemoveAt(GroundOne.Parent.Count - 1);
                current.SceneBack();
            }
        }
    }
}

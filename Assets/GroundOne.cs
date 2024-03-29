﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Reflection;
using DungeonPlayer;

namespace DungeonPlayer
{
    public static class GroundOne
    {
        public enum battleResult 
        {
            None, // 情報なし
            OK, // 勝ち
            Ignore, // 負け
            Retry, // 再戦
            Abort, // 逃げる
        }

        public enum GameLanguage
        {
            English = 1,
            Japanese = 2,
        }

        public static List<string> ObjectiveList = new List<string>(); // 目標

		public static bool IsConnect = false; // サーバー接続OKサイン
        public static List<string> playbackMessage = new List<string>(); // プレイバックメッセージテキスト

        private static GameObject objMC = null;
        private static GameObject objSC = null;
        private static GameObject objTC = null;
        private static GameObject objP1 = null;
        private static GameObject objP2 = null;
        private static GameObject objP3 = null;
        private static GameObject objWE = null;
        private static GameObject objWE2 = null;
        private static GameObject objWE3 = null;
        private static GameObject objSQL = null;

        public static MainCharacter MC = null;
        public static MainCharacter SC = null;
        public static MainCharacter TC = null;
        public static MainCharacter P1 = null;
        public static MainCharacter P2 = null;
        public static MainCharacter P3 = null;
        public static WorldEnvironment WE = null;
        public static TruthWorldEnvironment WE2 = null; // ゲームストーリー全体のワールド環境フラグ
        public static SingleWorldEnvironment WE3 = null; // シングルモードのワールド環境フラグ
        public static ControlSQL SQL = null;

        public static bool[] Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo2 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo3 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo4 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo5 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        public static GameObject sound = null; // サウンド音源
        public static AudioSource soundSource = null; // サウンドソース

        public static GameObject bgm = null; // BGM音源
        public static List<AudioSource> bgmSource = new List<AudioSource>(); // BGMソース
        private static List<String> BgmName = new List<string>();
        public static List<float> BgmLoopPoint = new List<float>();
        public static int BgmNumber = 0;

        public static int EnableBGM = 100; // ミュージック、デフォルトは100
        public static int EnableSoundEffect = 100; // 効果音、デフォルトは100
        public static int BattleSpeed = 3;
        public static int Difficulty = 2; // ゲーム難易度 デフォルトは２：普通
        public static GameLanguage Language = GameLanguage.English; // ゲームサポート言語
        public static bool SupportLog = true; // SQLサーバーに操作ログを残す　デフォルトはON

        public static bool AlreadyInitialize = false; // 既に一度InitializeGroundOneを呼んだかどうか

        public static bool TutorialMode = false; // チュートリアルモードを示すフラグ
        public static int TutorialLevel = 1; // チュートリアルのレベル

        // MotherForm
        public static List<MotherForm> Parent;
        public static string SceneName;

        // TruthBattleEnemy
        public static bool HiSpeedAnimation = false; // 通常ダメージアニメーションを早めるために使用
        public static bool FinalBattle = false; // 最終戦闘、スタックコマンドの動作を早めるために使用
        public static bool LifeCountBattle = false; // 最終戦闘でライフカウントを表現するために使用
        public static bool EnableBattleReward = true; // モンスター撃破後の報酬有無（デフォルトTrue、モンスター討伐ではFalse指定）
        public static bool CallFromMonsterQuest = false; // モンスター討伐から呼び出された戦闘シーンであることを示すフラグ
        public static battleResult BattleResult = battleResult.None;
        public static string enemyName1 = string.Empty;
        public static string enemyName2 = string.Empty;
        public static string enemyName3 = string.Empty;

        // TruthSelectEquipment
        public static int EquipType = 0; // 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        public static MainCharacter TargetPlayer = null;

        // TruthDungeon
        public static bool Player1Levelup = false;
        public static int Player1UpPoint = 0;
        public static int Player1CumultiveLvUpValue = 0;
        public static bool Player2Levelup = false;
        public static int Player2UpPoint = 0;
        public static int Player2CumultiveLvUpValue = 0;
        public static bool Player3Levelup = false;
        public static int Player3UpPoint = 0;
        public static int Player3CumultiveLvUpValue = 0;
        public static MainCharacter ShadowMC = null;
        public static MainCharacter ShadowSC = null;
        public static MainCharacter ShadowTC = null;
        public static WorldEnvironment shadowWE = null;
        public static TruthWorldEnvironment shadowWE2 = null;
        public static bool GotoDownstair = false;

        // TruthStatusPlayer
        //public static Color CurrentStatusColor = new Color(Database.COLOR_EIN_R, Database.COLOR_EIN_G, Database.COLOR_EIN_B);
        public static bool LevelUpRoutine = false; // レベルアップルーチン「他のキャラクターがレベルアップしている場合、ダンジョン画面に戻った時、継続してレベルアップチェックを行う」
        public static bool LevelUp = false; // レベルアップモード画面
        public static string LevelUpCharacter = string.Empty; // レベルアップ対象名
        public static int UpPoint = 0; // パラメタアップポイント
        public static int UpLifePointMC = 0; // Max Life Gain (view only)
        public static int UpManaPointMC = 0; // Max Mana Gain (view only)
        public static int UpLifePointSC = 0; // Max Life Gain (view only)
        public static int UpManaPointSC = 0; // Max Mana Gain (view only)
        public static int UpLifePointTC = 0; // Max Life Gain (view only)
        public static int UpManaPointTC = 0; // Max Mana Gain (view only)
        public static int CumultiveLvUpValue = 0; // レベルアップカウント累積値
        public static bool OnlySelectTrash = false; // 捨てる限定画面
        public static string OnlySelectTrashNewItem = string.Empty; // 捨てる限定画面で入手予定の新しいアイテム名
        public static string CannotSelectTrash = string.Empty; // 対象アイテムが重要品で捨てられない場合。
        public static bool DuelMode = false; // Duelモード
        public static string OpponentDuelist = string.Empty; // Duel対戦相手名
        public static bool OnlyUseItem = false; // 戦闘画面からアイテムを使用する時

        // TruthSkillSpellDesc
        public static string SpellSkillName = String.Empty;
        public static string playerName = String.Empty;

        // SaveLoad
        public static bool AfterBacktoTitle = false; // タイトル戻り直前のセーブモード
        public static bool SaveMode = false; // false:Load true:Save
        public static bool SaveAndExit = false; // true:RealWorldSave and exit
        public static string CurrentLoadFileName = String.Empty; // 現在ロード対象となっているファイル名

        // TruthHomeTown
        public static bool TruthHomeTown_NowExit = false;
        public static bool TruthHomeTown_DuelFailCount1 = false; // 現実世界、ラナDUEL戦で敗北した時１
        public static bool TruthHomeTown_DuelFailCount2 = false; // 現実世界、ラナDUEL戦で敗北した時２

        public static int DecisionSequence = 0;
        public static string DecisionMainMessage = string.Empty;
        public static string DecisionFirstMessage = string.Empty;
        public static string DecisionSecondMessage = string.Empty;
        public static string DecisionThirdMessage = string.Empty;
             
        // TruthDecision
        public static int DecisionChoice = 0;

        // TruthDecision2
        public static string Decision2_Message = string.Empty;
        public static string Decision2_TopText = string.Empty;
        public static string Decision2_LeftText = string.Empty;
        public static string Decision2_RightText = string.Empty;
        public static string Decision2_BottomText = string.Empty;
        public static TruthDecision2.AnswerType Decision2_Answer;
        public static bool Decision2_SelectPermutation = false;
        public static bool PermutationAnswer = false;

        // TruthInputRequest
        public static int InputValue = 0;

        // TruthAnswer
        public static bool GodSeuqence = false;
        
        // TruthItemDesc
        public static string ItemNameTitle = string.Empty;

        // TruthDuelPlayerStatus
        public static string DuelPlayerName;
        
        // TruthMonsterQuest
        public static int MQ_AreaNumber = 0;
        public static int MQ_StageNumber = 0;

        public static void ReInitializeGroundOne(bool FromGameLoad)
        {
            Debug.Log("ReInitializeGroundOne (S)");

            UnityEngine.Object.Destroy(MC);
            MC = null;
            UnityEngine.Object.Destroy(SC);
            SC = null;
            UnityEngine.Object.Destroy(TC);
            TC = null;
            UnityEngine.Object.Destroy(P1);
            P1 = null;
            UnityEngine.Object.Destroy(P2);
            P2 = null;
            UnityEngine.Object.Destroy(P3);
            P3 = null;
            UnityEngine.Object.Destroy(WE);
            WE = null;
            UnityEngine.Object.Destroy(WE2);
            WE2 = null;
            UnityEngine.Object.Destroy(WE3);
            WE3 = null;
            UnityEngine.Object.Destroy(SQL);
            SQL = null;
            UnityEngine.Object.Destroy(objMC);
            objMC = null;
            UnityEngine.Object.Destroy(objSC);
            objSC = null;
            UnityEngine.Object.Destroy(objTC);
            objTC = null;
            UnityEngine.Object.Destroy(objWE);
            objWE = null;
            UnityEngine.Object.Destroy(objWE2);
            objWE2 = null;
            UnityEngine.Object.Destroy(objWE3);
            objWE3 = null;
            UnityEngine.Object.Destroy(objSQL);
            objSQL = null;
            UnityEngine.Object.Destroy(ShadowMC);
            ShadowMC = null;
            UnityEngine.Object.Destroy(ShadowSC);
            ShadowSC = null;
            UnityEngine.Object.Destroy(ShadowTC);
            ShadowTC = null;
            UnityEngine.Object.Destroy(shadowWE);
            shadowWE = null;
            UnityEngine.Object.Destroy(shadowWE2);
            shadowWE2 = null;
            if (FromGameLoad == false)
            {
                Parent.Clear();
                UnityEngine.Object.Destroy(sound);
                UnityEngine.Object.Destroy(soundSource);
                sound = null;
                soundSource = null;
                UnityEngine.Object.Destroy(bgm);
                for (int ii = 0; ii < bgmSource.Count; ii++)
                {
                    //bgmSource[ii].clip.UnloadAudioData();
                    GameObject.Destroy(bgmSource[ii]);
                }
                bgm = null;
                bgmSource = null;
                BgmName.Clear();
                BgmName = null;
                BgmLoopPoint.Clear();
                BgmLoopPoint = null;
                BgmNumber = 0;
            }
            Truth_KnownTileInfo = null;
            AlreadyInitialize = false;
            TutorialMode = false;
            TutorialLevel = 1;
            InitializeGroundOne(FromGameLoad);
        }

        public static bool InitializeGroundOne(bool FromGameLoad)
        {
            Debug.Log("InitializeGroundOne start");

            if (AlreadyInitialize == false) { AlreadyInitialize = true; }
            else { Debug.Log("already initialize"); return false; }

            objMC = new GameObject("objMC");
            objSC = new GameObject("objSC");
            objTC = new GameObject("objTC");
            objP1 = new GameObject("objP1");
            objP2 = new GameObject("objP2");
            objP3 = new GameObject("objP3");
            objWE = new GameObject("objWE");
            objWE2 = new GameObject("objWE2");
            objWE3 = new GameObject("objWE3");
            objSQL = new GameObject("objSQL");

            if (FromGameLoad == false)
            {
                Parent = new List<MotherForm>();
                sound = new GameObject("sound");
                soundSource = sound.AddComponent<AudioSource>();
                bgm = new GameObject("bgm");
                bgmSource = new List<AudioSource>();
                bgmSource.Add(bgm.AddComponent<AudioSource>());
                BgmName = new List<string>();
                BgmLoopPoint = new List<float>();
            }

            Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            WE = objWE.AddComponent<WorldEnvironment>();
            WE.DungeonArea = 1;
            WE.AvailableFirstCharacter = true;
            WE2 = objWE2.AddComponent<TruthWorldEnvironment>();
            WE3 = objWE3.AddComponent<SingleWorldEnvironment>();

            MC = objMC.AddComponent<MainCharacter>();
            MC.AvailableMana = true;
            MC.AvailableSkill = true;
            SC = objSC.AddComponent<MainCharacter>();
            SC.AvailableMana = true;
            SC.AvailableSkill = true;
            TC = objTC.AddComponent<MainCharacter>();
            TC.AvailableMana = true;
            TC.AvailableSkill = true;
            P1 = objP1.AddComponent<MainCharacter>();
            P1.AvailableMana = true;
            P1.AvailableSkill = true;
            P2 = objP2.AddComponent<MainCharacter>();
            P2.AvailableMana = true;
            P2.AvailableSkill = true;
            P3 = objP3.AddComponent<MainCharacter>();
            P3.AvailableMana = true;
            P3.AvailableSkill = true;

            SQL = objSQL.AddComponent<ControlSQL>();
            SQL.SetupSql();

            // debug
            //P1.FirstName = "New Character 1";
            //P1.FullName = "New Character 1";
            //P1.Strength = 1;
            //P1.Agility = 2;
            //P1.Intelligence = 3;
            //P1.Stamina = 4;
            //P1.Mind = 5;
            //P1.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            //P1.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            //P1.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            //P1.MaxGain();
            //P1.BattleActionCommandList[0] = Database.ATTACK_EN;
            //P1.BattleActionCommandList[1] = Database.DEFENSE_EN;

            //P2.FirstName = "New Character 2";
            //P2.FullName = "New Character 2";
            //P2.Level = 2;
            //P2.Strength = 6;
            //P2.Agility = 7;
            //P2.Intelligence = 8;
            //P2.Stamina = 9;
            //P2.Mind = 10;
            //P2.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            //P2.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            //P2.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            //P2.MaxGain();
            //P2.BattleActionCommandList[0] = Database.ATTACK_EN;
            //P2.BattleActionCommandList[1] = Database.DEFENSE_EN;

            //P3.FirstName = "New Character 3";
            //P3.FullName = "New Character 3";
            //P3.Level = 3;
            //P3.Strength = 11;
            //P3.Agility = 12;
            //P3.Intelligence = 13;
            //P3.Stamina = 14;
            //P3.Mind = 15;
            //P3.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            //P3.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            //P3.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            //P3.MaxGain();
            //P3.BattleActionCommandList[0] = Database.ATTACK_EN;
            //P3.BattleActionCommandList[1] = Database.DEFENSE_EN;
            //WE.AvailablePotionshop = true;
            //WE.AvailableEquipShop = true;
            //WE.AvailableEquipShop2 = true;
            //WE.AvailableEquipShop3 = true;
            //WE.AvailableEquipShop4 = true;
            //WE.AvailablePotion2 = true;
            //WE.AvailablePotion3 = true;
            //if (!GroundOne.WE2.PotionAvailable_12 && (GroundOne.WE2.PotionMixtureDay_12 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_12))
            //GroundOne.WE2.PotionAvailable_12 = false;
            //GroundOne.WE2.PotionMixtureDay_12 = 1;

            //if (!GroundOne.WE2.EquipAvailable_11 && (GroundOne.WE2.EquipMixtureDay_11 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.EquipMixtureDay_11))
            //GroundOne.WE2.EquipMixtureDay_11 = 1;
            //GroundOne.WE.GameDay = 3;
            // if (!GroundOne.WE2.EquipAvailable_12 && (GroundOne.WE2.EquipMixtureDay_12 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.EquipMixtureDay_12))
            //GroundOne.WE2.EquipMixtureDay_12 = 1;
            //GroundOne.WE2.EquipMixtureDay_35 = 1;
            //GroundOne.WE2.EquipMixtureDay_46 = 1;

            //MC.Syutyu_Danzetsu = true;

            //WE.AvailableSecondCharacter = true;
            //WE.AvailableThirdCharacter = true;
            //WE.AvailableArchetypeCommand = true;

            //WE.AvailableDuelColosseum = true;
            //WE.AvailableItemBank = true;
            //WE.AvailableDuelMatch = true;
            //WE.MeetOlLandis = true;
            //WE.AlreadyRest = false;
            //WE.Truth_CommunicationFirstHomeTown = true;

            //WE.dungeonEvent226 = true;
            //WE.AvailableBattleSettingMenu = true;
            //WE.AvailableMixSpellSkill = true;
            //WE2.AvailableMixSpellSkill = true;
            //WE.AvailableInstantCommand = true;
            //WE.TruthCompleteArea1 = true;

            //MC.Level = 23;
            ////bool shinikiaVisible = (GroundOne.WE.AvailableBackGate && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            //WE.AvailableBackGate = true;
            //WE.AvailableFazilCastle = true;

            //GroundOne.WE2.AvailableMixSpellSkill = true;
            //GroundOne.WE2.AvailableArcheTypeCommand = true;
            ////GroundOne.WE.TruthCompleteArea2 && GroundOne.WE.TruthCommunicationCompArea2 && !GroundOne.WE.Truth_CommunicationThirdHomeTown
            ////WE.TruthCompleteArea2 = true;
            ////WE.TruthCommunicationCompArea2 = true;

            ////GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.TruthCommunicationCompArea1
            ////WE.TruthCompleteArea1 = true;
            ////WE.GameDay = 6;
            //WE.Truth_CommunicationFirstHomeTown = true;
            //WE.Truth_CommunicationHanna1 = true;
            //WE.AlreadyRest = true;
            ////GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.AvailableMixSpellSkill && GroundOne.MC.Level >= 21)
            ////WE.TruthCompleteArea1 = true;
            //WE.AvailableMixSpellSkill = false;
            //MC.Level = 1;

            //WE.AvailableInstantCommand = true;
            //GroundOne.WE.TruthCommunicationCompArea1 = true;
            //GroundOne.WE.Truth_CommunicationThirdHomeTown = true;

            //GroundOne.enemyName1 = Database.ENEMY_BOSS_BYSTANDER_EMPTINESS; // DUEL_EONE_FULNEA;
            //GroundOne.enemyName2 = string.Empty;// Database.ENEMY_SUN_FLOWER;
            //GroundOne.enemyName3 = string.Empty;// Database.ENEMY_SPEEDY_TAKA;
            //GroundOne.WE.AvailableSecondCharacter = true;
            ////GroundOne.WE.AvailableThirdCharacter = true;
            ////GroundOne.DuelMode = true;
            ////GroundOne.WE2.RealWorld = true;
            ////GroundOne.WE2.SeekerEvent506 = true;
            //GroundOne.WE.DungeonArea = 1;
            ////else if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationGanz1)
            ////else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            ////else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationGanz21)
            ////else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.AvailableMixSpellSkill && GroundOne.MC.Level >= 21)
            //WE.Truth_CommunicationGanz1 = true;
            //WE.Truth_CommunicationGanz21 = true;

            //// (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent602 && GroundOne.WE2.SeekerEvent603 && GroundOne.WE2.SeekerEvent604 && !GroundOne.WE2.SeekerEvent605)
            ////WE2.RealWorld = true;
            ////WE2.SeekerEvent602 = true;
            ////WE2.SeekerEvent603 = true;
            ////WE2.SeekerEvent604 = true;

            ////!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea1 && (!GroundOne.WE.Truth_CommunicationLana21 || !GroundOne.WE.Truth_CommunicationGanz21 || !GroundOne.WE.Truth_CommunicationHanna21 || !GroundOne.WE.Truth_CommunicationOl21))
            ////GroundOne.WE.TruthCompleteArea1 = true;
            //GroundOne.WE.Truth_CommunicationLana21 = true;
            //GroundOne.WE.Truth_CommunicationGanz21 = true;
            //GroundOne.WE.Truth_CommunicationHanna21 = true;
            //GroundOne.WE.Truth_CommunicationOl21 = true;

            //GroundOne.WE.AvailableEquipShop2 = true;
            //GroundOne.WE.AvailableEquipShop3 = true;
            //GroundOne.WE.AvailableEquipShop4 = true;

            //MC.FullName = Database.EIN_WOLENCE_FULL;
            //MC.FirstName = Database.EIN_WOLENCE;
            //MC.MainWeapon = new ItemBackPack(Database.POOR_TUKAIFURUSARETA_SWORD);
            //MC.SubWeapon = null;
            //MC.MainArmor = new ItemBackPack(Database.POOR_FESTERING_ARMOR);
            //MC.Accessory = new ItemBackPack(Database.COMMON_BLUE_PENDANT);
            //MC.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_PENDANT);
            //MC.CurrentLife = MC.MaxLife;
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_REVIVE_POTION_MINI));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_BASTARD_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_RED_PENDANT));
            //MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            //MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_TWEI_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_PENDANT));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_BRONZE_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_GUST_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.RARE_DARKNESS_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_MASTER_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.RARE_SWORD_OF_DIVIDE));
            //MC.AddBackPack(new ItemBackPack(Database.RARE_TYOU_KOU_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_MASEKI));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_PURPLE_MASEKI));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_VIKING_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_GREEN_MASEKI));
            //MC.AddBackPack(new ItemBackPack(Database.COMMON_INITIATE_SWORD));
            //MC.AddBackPack(new ItemBackPack(Database.RARE_AERO_BLADE));
            //MC.AddBackPack(new ItemBackPack(Database.EPIC_OVER_SHIFTING));

            //MC.DeadPlayer();
            //MC.Level = 5;
            //MC.Strength = 100;
            //MC.Agility = 500;
            //MC.Intelligence = 100;
            //MC.Stamina = 1000;
            //MC.CurrentLife = MC.MaxLife;
            //MC.CurrentMana = MC.MaxMana;
            //MC.CurrentSkillPoint = MC.CurrentSkillPoint;

            //MC.AvailableMana = true;
            //MC.AvailableSkill = true;
            //GroundOne.WE.AvailableMixSpellSkill = true;
            //MC.FreshHeal = true;
            //MC.Protection = true;
            //MC.HolyShock = true;
            //MC.SaintPower = true;
            //MC.Glory = true;
            //MC.Resurrection = true;
            //MC.CelestialNova = true;
            //MC.DarkBlast = true;
            //MC.ShadowPact = true;
            //MC.LifeTap = true;
            //MC.DevouringPlague = true;
            //MC.BlackContract = true;
            //MC.BloodyVengeance = true;
            //MC.Damnation = true;
            //MC.FireBall = true;
            //MC.FlameAura = true;
            //MC.HeatBoost = true;
            //MC.VolcanicWave = true;
            //MC.FlameStrike = true;
            //MC.ImmortalRave = true;
            //MC.LavaAnnihilation = true;
            //MC.IceNeedle = true;
            //MC.AbsorbWater = true;
            //MC.Cleansing = true;
            //MC.MirrorImage = true;
            //MC.FrozenLance = true;
            //MC.PromisedKnowledge = true;
            //MC.AbsoluteZero = true;
            //MC.WordOfPower = true;
            //MC.GaleWind = true;
            //MC.WordOfLife = true;
            //MC.WordOfFortune = true;
            //MC.AetherDrive = true;
            //MC.Genesis = true;
            //MC.EternalPresence = true;
            //MC.DispelMagic = true;
            //MC.RiseOfImage = true;
            //MC.Tranquility = true;
            //MC.Deflection = true;
            //MC.OneImmunity = true;
            //MC.WhiteOut = true;
            //MC.TimeStop = true;
            //MC.StraightSmash = true;
            //MC.DoubleSlash = true;
            //MC.CrushingBlow = true;
            //MC.SoulInfinity = true;
            //MC.CounterAttack = true;
            //MC.PurePurification = true;
            //MC.AntiStun = true;
            //MC.StanceOfDeath = true;
            //MC.StanceOfFlow = true;
            //MC.EnigmaSence = true;
            //MC.SilentRush = true;
            //MC.OboroImpact = true;
            //MC.StanceOfStanding = true;
            //MC.InnerInspiration = true;
            //MC.KineticSmash = true;
            //MC.Catastrophe = true;
            //MC.TruthVision = true;
            //MC.HighEmotionality = true;
            //MC.StanceOfEyes = true;
            //MC.PainfulInsanity = true;
            //MC.Negate = true;
            //MC.VoidExtraction = true;
            //MC.CarnageRush = true;
            //MC.NothingOfNothingness = true;
            //MC.PsychicTrance = true;
            //MC.BlindJustice = true;
            //MC.TranscendentWish = true;
            //MC.FlashBlaze = true;
            //MC.LightDetonator = true;
            //MC.AscendantMeteor = true;
            //MC.SkyShield = true;
            //MC.SacredHeal = true;
            //MC.EverDroplet = true;
            //MC.HolyBreaker = true;
            //MC.ExaltedField = true;
            //MC.HymnContract = true;
            //MC.StarLightning = true;
            //MC.AngelBreath = true;
            //MC.EndlessAnthem = true;
            //MC.BlackFire = true;
            //MC.BlazingField = true;
            //MC.DemonicIgnite = true;
            //MC.BlueBullet = true;
            //MC.DeepMirror = true;
            //MC.DeathDeny = true;
            //MC.WordOfMalice = true;
            //MC.AbyssEye = true;
            //MC.SinFortune = true;
            //MC.DarkenField = true;
            //MC.DoomBlade = true;
            //MC.EclipseEnd = true;
            //MC.FrozenAura = true;
            //MC.ChillBurn = true;
            //MC.ZetaExplosion = true;
            //MC.EnrageBlast = true;
            //MC.PiercingFlame = true;
            //MC.SigilOfHomura = true;
            //MC.Immolate = true;
            //MC.PhantasmalWind = true;
            //MC.RedDragonWill = true;
            //MC.WordOfAttitude = true;
            //MC.StaticBarrier = true;
            //MC.AusterityMatrix = true;
            //MC.VanishWave = true;
            //MC.VortexField = true;
            //MC.BlueDragonWill = true;
            //MC.SeventhMagic = true;
            //MC.ParadoxImage = true;
            //MC.WarpGate = true;
            //MC.NeutralSmash = true;
            //MC.StanceOfDouble = true;
            //MC.SwiftStep = true;
            //MC.VigorSense = true;
            //MC.CircleSlash = true;
            //MC.RisingAura = true;
            //MC.RumbleShout = true;
            //MC.OnslaughtHit = true;
            //MC.ColorlessMove = true;
            //MC.AscensionAura = true;
            //MC.FutureVision = true;
            //MC.UnknownShock = true;
            //MC.ReflexSpirit = true;
            //MC.FatalBlow = true;
            //MC.SharpGlare = true;
            //MC.ConcussiveHit = true;
            //MC.TrustSilence = true;
            //MC.MindKilling = true;
            //MC.SurpriseAttack = true;
            //MC.StanceOfMystic = true;
            //MC.PsychicWave = true;
            //MC.NourishSense = true;
            //MC.Recover = true;
            //MC.ImpulseHit = true;
            //MC.ViolentSlash = true;
            //MC.ONEAuthority = true;
            //MC.OuterInspiration = true;
            //MC.HardestParry = true;
            //MC.StanceOfSuddenness = true;
            //MC.SoulExecution = true;
            //MC.Syutyu_Danzetsu = true;

            //MC.Gold = 0;

            //MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            //MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            //MC.BattleActionCommandList[2] = Database.FIRE_BALL;
            //MC.BattleActionCommandList[3] = Database.STRAIGHT_SMASH;
            //MC.BattleActionCommandList[4] = Database.GALE_WIND;
            //MC.BattleActionCommandList[5] = Database.WARP_GATE;
            //MC.BattleActionCommandList[6] = Database.ARCHETYPE_EIN;
            //MC.BattleActionCommandList[7] = Database.RECOVER;
            //MC.BattleActionCommandList[8] = Database.STANCE_OF_SUDDENNESS;

            //SC.FullName = Database.RANA_AMILIA_FULL;
            //SC.FirstName = Database.RANA_AMILIA;
            //SC.MainWeapon = new ItemBackPack(Database.POOR_TUKAIFURUSARETA_SWORD);
            //SC.SubWeapon = null;
            //SC.MainArmor = new ItemBackPack(Database.POOR_FESTERING_ARMOR);
            //SC.Accessory = new ItemBackPack(Database.COMMON_BLUE_PENDANT);
            //SC.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_PENDANT);
            //SC.CurrentLife = SC.MaxLife;
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_REVIVE_POTION_MINI));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_BASTARD_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_RED_PENDANT));
            //SC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            //SC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_TWEI_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_PENDANT));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_BRONZE_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_GUST_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.RARE_DARKNESS_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_MASTER_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.RARE_SWORD_OF_DIVIDE));
            //SC.AddBackPack(new ItemBackPack(Database.RARE_TYOU_KOU_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_MASEKI));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_PURPLE_MASEKI));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_VIKING_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_GREEN_MASEKI));
            //SC.AddBackPack(new ItemBackPack(Database.COMMON_INITIATE_SWORD));
            //SC.AddBackPack(new ItemBackPack(Database.RARE_AERO_BLADE));
            //SC.AddBackPack(new ItemBackPack(Database.EPIC_OVER_SHIFTING));

            //SC.DeadPlayer();
            //SC.Level = 5;
            //SC.Strength = 100;
            //SC.Agility = 500;
            //SC.Intelligence = 100;
            //SC.Stamina = 1000;
            //SC.CurrentLife = SC.MaxLife;
            //SC.CurrentMana = SC.MaxMana;
            //SC.CurrentSkillPoint = SC.CurrentSkillPoint;

            //SC.AvailableMana = true;
            //SC.AvailableSkill = true;
            //GroundOne.WE.AvailableMixSpellSkill = true;
            //SC.FreshHeal = true;
            //SC.Protection = true;
            //SC.HolyShock = true;
            //SC.SaintPower = true;
            //SC.Glory = true;
            //SC.Resurrection = true;
            //SC.CelestialNova = true;
            //SC.DarkBlast = true;
            //SC.ShadowPact = true;
            //SC.LifeTap = true;
            //SC.DevouringPlague = true;
            //SC.BlackContract = true;
            //SC.BloodyVengeance = true;
            //SC.Damnation = true;
            //SC.FireBall = true;
            //SC.FlameAura = true;
            //SC.HeatBoost = true;
            //SC.VolcanicWave = true;
            //SC.FlameStrike = true;
            //SC.ImmortalRave = true;
            //SC.LavaAnnihilation = true;
            //SC.IceNeedle = true;
            //SC.AbsorbWater = true;
            //SC.Cleansing = true;
            //SC.MirrorImage = true;
            //SC.FrozenLance = true;
            //SC.PromisedKnowledge = true;
            //SC.AbsoluteZero = true;
            //SC.WordOfPower = true;
            //SC.GaleWind = true;
            //SC.WordOfLife = true;
            //SC.WordOfFortune = true;
            //SC.AetherDrive = true;
            //SC.Genesis = true;
            //SC.EternalPresence = true;
            //SC.DispelMagic = true;
            //SC.RiseOfImage = true;
            //SC.Tranquility = true;
            //SC.Deflection = true;
            //SC.OneImmunity = true;
            //SC.WhiteOut = true;
            //SC.TimeStop = true;
            //SC.StraightSmash = true;
            //SC.DoubleSlash = true;
            //SC.CrushingBlow = true;
            //SC.SoulInfinity = true;
            //SC.CounterAttack = true;
            //SC.PurePurification = true;
            //SC.AntiStun = true;
            //SC.StanceOfDeath = true;
            //SC.StanceOfFlow = true;
            //SC.EnigmaSence = true;
            //SC.SilentRush = true;
            //SC.OboroImpact = true;
            //SC.StanceOfStanding = true;
            //SC.InnerInspiration = true;
            //SC.KineticSmash = true;
            //SC.Catastrophe = true;
            //SC.TruthVision = true;
            //SC.HighEmotionality = true;
            //SC.StanceOfEyes = true;
            //SC.PainfulInsanity = true;
            //SC.Negate = true;
            //SC.VoidExtraction = true;
            //SC.CarnageRush = true;
            //SC.NothingOfNothingness = true;
            //SC.PsychicTrance = true;
            //SC.BlindJustice = true;
            //SC.TranscendentWish = true;
            //SC.FlashBlaze = true;
            //SC.LightDetonator = true;
            //SC.AscendantMeteor = true;
            //SC.SkyShield = true;
            //SC.SacredHeal = true;
            //SC.EverDroplet = true;
            //SC.HolyBreaker = true;
            //SC.ExaltedField = true;
            //SC.HymnContract = true;
            //SC.StarLightning = true;
            //SC.AngelBreath = true;
            //SC.EndlessAnthem = true;
            //SC.BlackFire = true;
            //SC.BlazingField = true;
            //SC.DemonicIgnite = true;
            //SC.BlueBullet = true;
            //SC.DeepMirror = true;
            //SC.DeathDeny = true;
            //SC.WordOfMalice = true;
            //SC.AbyssEye = true;
            //SC.SinFortune = true;
            //SC.DarkenField = true;
            //SC.DoomBlade = true;
            //SC.EclipseEnd = true;
            //SC.FrozenAura = true;
            //SC.ChillBurn = true;
            //SC.ZetaExplosion = true;
            //SC.EnrageBlast = true;
            //SC.PiercingFlame = true;
            //SC.SigilOfHomura = true;
            //SC.Immolate = true;
            //SC.PhantasmalWind = true;
            //SC.RedDragonWill = true;
            //SC.WordOfAttitude = true;
            //SC.StaticBarrier = true;
            //SC.AusterityMatrix = true;
            //SC.VanishWave = true;
            //SC.VortexField = true;
            //SC.BlueDragonWill = true;
            //SC.SeventhMagic = true;
            //SC.ParadoxImage = true;
            //SC.WarpGate = true;
            //SC.NeutralSmash = true;
            //SC.StanceOfDouble = true;
            //SC.SwiftStep = true;
            //SC.VigorSense = true;
            //SC.CircleSlash = true;
            //SC.RisingAura = true;
            //SC.RumbleShout = true;
            //SC.OnslaughtHit = true;
            //SC.ColorlessMove = true;
            //SC.AscensionAura = true;
            //SC.FutureVision = true;
            //SC.UnknownShock = true;
            //SC.ReflexSpirit = true;
            //SC.FatalBlow = true;
            //SC.SharpGlare = true;
            //SC.ConcussiveHit = true;
            //SC.TrustSilence = true;
            //SC.MindKilling = true;
            //SC.SurpriseAttack = true;
            //SC.StanceOfMystic = true;
            //SC.PsychicWave = true;
            //SC.NourishSense = true;
            //SC.Recover = true;
            //SC.ImpulseHit = true;
            //SC.ViolentSlash = true;
            //SC.ONEAuthority = true;
            //SC.OuterInspiration = true;
            //SC.HardestParry = true;
            //SC.StanceOfSuddenness = true;
            //SC.SoulExecution = true;

            //TC = objTC.AddComponent<MainCharacter>();
            //TC.FirstName = Database.OL_LANDIS;
            //TC.FullName = Database.OL_LANDIS_FULL;
            //TC.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
            //TC.Agility = Database.OL_LANDIS_FIRST_AGILITY;
            //TC.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
            //TC.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
            //TC.Mind = Database.OL_LANDIS_FIRST_MIND;
            //TC.Agility = 750;
            //TC.AvailableMana = true;
            //TC.AvailableSkill = true;
            //TC.BattleActionCommandList[0] = Database.ATTACK_EN;
            //TC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            //TC.BattleActionCommandList[2] = Database.STRAIGHT_SMASH;
            //TC.BattleActionCommandList[3] = Database.STAY_EN;
            //TC.BattleActionCommandList[4] = Database.STAY_EN;
            //TC.BattleActionCommandList[5] = Database.STAY_EN;
            //TC.BattleActionCommandList[6] = Database.STAY_EN;
            //TC.BattleActionCommandList[7] = Database.STAY_EN;
            //TC.BattleActionCommandList[8] = Database.STAY_EN;
            //TC.StraightSmash = true;
            //TC.FireBall = true;
            //TC.DarkBlast = true;
            //TC.DoubleSlash = true;
            //TC.ShadowPact = true;
            //TC.FlameAura = true;
            //TC.StanceOfStanding = true;
            //TC.DispelMagic = true;
            //TC.LifeTap = true;
            //TC.HeatBoost = true;
            //TC.Negate = true;
            //TC.BlackContract = true;
            //TC.InnerInspiration = true;
            //TC.RiseOfImage = true;
            //TC.Deflection = true;
            //TC.FlameStrike = true;
            //TC.Tranquility = true;
            //TC.VoidExtraction = true;
            //TC.BlackFire = true;
            //TC.Immolate = true;
            //TC.DarkenField = true;
            //TC.DevouringPlague = true;
            //TC.VolcanicWave = true;
            //TC.OneImmunity = true;
            //TC.CircleSlash = true;
            //TC.OuterInspiration = true;
            //TC.ColorlessMove = true;
            //TC.WordOfMalice = true;
            //TC.EnrageBlast = true;
            //TC.SwiftStep = true;
            //TC.Recover = true;
            //TC.SurpriseAttack = true;
            //TC.SeventhMagic = true;


            //TC.FireBall = true;
            //TC.LavaAnnihilation = true;
            //TC.DemonicIgnite = true;
            //TC.AusterityMatrix = true;
            //TC.StraightSmash = true;
            //TC.DoubleSlash = true;
            //TC.Negate = true;
            //TC.NothingOfNothingness = true;
            //TC.ZetaExplosion = true;
            //TC.ChillBurn = true;

            UnityEngine.Object.DontDestroyOnLoad(MC);
            UnityEngine.Object.DontDestroyOnLoad(SC);
            UnityEngine.Object.DontDestroyOnLoad(TC);
            UnityEngine.Object.DontDestroyOnLoad(P1);
            UnityEngine.Object.DontDestroyOnLoad(P2);
            UnityEngine.Object.DontDestroyOnLoad(P3);
            UnityEngine.Object.DontDestroyOnLoad(WE);
            UnityEngine.Object.DontDestroyOnLoad(WE2);
            UnityEngine.Object.DontDestroyOnLoad(WE3);
            UnityEngine.Object.DontDestroyOnLoad(sound);
            UnityEngine.Object.DontDestroyOnLoad(soundSource);
            UnityEngine.Object.DontDestroyOnLoad(bgm);
            UnityEngine.Object.DontDestroyOnLoad(bgmSource[0]);
            UnityEngine.Object.DontDestroyOnLoad(SQL);
            return true;
        }

        #region "BGM再生と効果音関連"
        public static void PlaySoundEffect(string soundName)
        {
            //if (GroundOne.EnableSoundEffect > 0.0f)
            {
                soundSource.clip = Resources.Load<AudioClip>(Database.BaseSoundFolder + soundName);
                soundSource.volume = (float)((float)GroundOne.EnableSoundEffect / 100.0f);
                soundSource.Play();
            }
        }

        public static void ChangeSoundEffectVolume(float vol)
        {
            soundSource.volume = vol;
        }

        public static void PlayDungeonMusic(string targetMusicName, float loopBegin)
        {
            PlayDungeonMusic(targetMusicName, string.Empty, loopBegin);
        }
        public static void PlayDungeonMusic(string targetMusicName, string targetMusicName2, float loopBegin)
        {
            StopDungeonMusic();

            bool detect = false;
            for (int ii = 0; ii < BgmName.Count; ii++)
            {
                if (targetMusicName == BgmName[ii])
                {
                    BgmNumber = ii;
                    detect = true;
                    break;
                }
            }

            if (detect == false)
            {
                BgmName.Add(targetMusicName);
                BgmLoopPoint.Add(loopBegin);
                bgmSource.Add(bgm.AddComponent<AudioSource>());
                BgmNumber = BgmName.Count - 1;
            }

            bgmSource[BgmNumber].Stop();
            bgmSource[BgmNumber].clip = Resources.Load<AudioClip>(Database.BaseMusicFolder + targetMusicName);
            bgmSource[BgmNumber].loop = false;
            bgmSource[BgmNumber].volume = (float)((float)GroundOne.EnableBGM / 100.0f);
            bgmSource[BgmNumber].time = 0;
            bgmSource[BgmNumber].Play();
        }

        public static void StopDungeonMusic()
        {
            if (bgmSource.Count > BgmNumber)
            {
                bgmSource[BgmNumber].Stop();
            }
        }

        public static void ChangeDungeonMusicVolume(float vol)
        {
            if (bgmSource.Count > BgmNumber)
            {
                bgmSource[BgmNumber].volume = vol;
            }
        }

        public static void TempStopDungeonMusic()
        {
            if (bgmSource.Count > BgmNumber)
            {
                bgmSource[BgmNumber].mute = true;
            }
        }

        public static void ResumeDungeonMusic()
        {
            if (bgmSource.Count > BgmNumber)
            {
                bgmSource[BgmNumber].mute = false;
            }
        }
        #endregion

    }
}

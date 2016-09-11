using System;
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

        public static string OwnerName = "YuasaTomonori"; // ゲーム接続オーナー名 [todo] ゲーム開始時点でオーナーに名前を入れてもらい、記憶する必要がある。
        public static string guid = "e9a30180-000e-4144-8130-c90c9f317c2f";
		public static ClientSocket CS = null; // サーバー接続ソケット
		public static bool IsConnect = false; // サーバー接続OKサイン
        public static List<string> playbackMessage = new List<string>(); // プレイバックメッセージテキスト

        private static GameObject objMC = null;
        private static GameObject objSC = null;
        private static GameObject objTC = null;
        private static GameObject objWE = null;
        private static GameObject objWE2 = null;
        private static GameObject objSQL = null;

        public static MainCharacter MC = null;
        public static MainCharacter SC = null;
        public static MainCharacter TC = null;
        public static WorldEnvironment WE = null;
        public static TruthWorldEnvironment WE2 = null; // ゲームストーリー全体のワールド環境フラグ
        public static ControlSQL SQL = null;

        public static bool[] Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo2 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo3 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo4 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo5 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        public static int BattleSpeed = 3;
        public static int Difficulty = 1; // ゲーム難易度 デフォルトは１：普通

        public static GameObject sound = null; // サウンド音源
        public static AudioSource soundSource = null; // サウンドソース

        public static GameObject bgm = null; // BGM音源
        public static AudioSource bgmSource = null; // BGMソース

        public static bool EnableBGM = false; // ミュージック、デフォルトはオン // debug
        public static bool EnableSoundEffect = false; // 効果音、デフォルトはオン // debug

        public static bool AlreadyInitialize = false; // 既に一度InitializeGroundOneを呼んだかどうか

        // MotherForm
        public static List<MotherForm> Parent;
        public static string SceneName;

        // TruthBattleEnemy
        public static bool HiSpeedAnimation = false; // スタック合戦をハイスピードにするかどうか
        public static bool FinalBattle = false; // 最終戦かどうか
        public static bool LifeCountBattle = false; // 最終戦（ライフカウント方式）かどうか
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
        public static Color CurrentStatusColor = new Color(Database.COLOR_EIN_R, Database.COLOR_EIN_G, Database.COLOR_EIN_B);
        public static bool LevelUp = false; // レベルアップモード画面
        public static int UpPoint = 0; // パラメタアップポイント
        public static int CumultiveLvUpValue = 0; // レベルアップカウント累積値
        public static bool OnlySelectTrash = false; // 捨てる限定画面
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

        // TruthHomeTown
        public static bool TruthHomeTown_NowExit = false;
        public static bool TruthHomeTown_DuelFailCount1 = false; // 現実世界、ラナDUEL戦で敗北した時１
        public static bool TruthHomeTown_DuelFailCount2 = false; // 現実世界、ラナDUEL戦で敗北した時２

        public static int DecisionSequence = 0;
        public static string DecisionMainMessage = string.Empty;
        public static string DecisionFirstMessage = string.Empty;
        public static string DecisionSecondMessage = string.Empty;
             
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
        
        public static void ReInitializeGroundOne(bool FromGameLoad)
        {
            Debug.Log("ReInitializeGroundOne (S)");

            GameObject.DestroyObject(MC);
            MC = null;
            GameObject.DestroyObject(SC);
            SC = null;
            GameObject.DestroyObject(TC);
            TC = null;
            GameObject.DestroyObject(WE);
            WE = null;
            GameObject.DestroyObject(WE2);
            WE2 = null;
            GameObject.DestroyObject(SQL);
            SQL = null;
            GameObject.DestroyObject(objMC);
            objMC = null;
            GameObject.DestroyObject(objSC);
            objSC = null;
            GameObject.DestroyObject(objTC);
            objTC = null;
            GameObject.DestroyObject(objWE);
            objWE = null;
            GameObject.DestroyObject(objWE2);
            objWE2 = null;
            GameObject.DestroyObject(objSQL);
            objSQL = null;
            GameObject.DestroyObject(ShadowMC);
            ShadowMC = null;
            GameObject.DestroyObject(ShadowSC);
            ShadowSC = null;
            GameObject.DestroyObject(ShadowTC);
            ShadowTC = null;
            GameObject.DestroyObject(shadowWE);
            shadowWE = null;
            GameObject.DestroyObject(shadowWE2);
            shadowWE2 = null;
            if (FromGameLoad == false)
            {
                Parent.Clear();
                GameObject.DestroyObject(sound);
                GameObject.DestroyObject(soundSource);
                sound = null;
                soundSource = null;
                GameObject.DestroyObject(bgm);
                GameObject.DestroyObject(bgmSource);
                bgm = null;
                bgmSource = null;
            }
            Truth_KnownTileInfo = null;
            AlreadyInitialize = false;
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
            objWE = new GameObject("objWE");
            objWE2 = new GameObject("objWE2");
            objSQL = new GameObject("objSQL");

            if (FromGameLoad == false)
            {
                Parent = new List<MotherForm>();
                sound = new GameObject("sound");
                soundSource = sound.AddComponent<AudioSource>();
                bgm = new GameObject("bgm");
                bgmSource = bgm.AddComponent<AudioSource>();
            }

            Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            WE = objWE.AddComponent<WorldEnvironment>();
            WE.DungeonArea = 1;
            WE.AvailableFirstCharacter = true;
            WE2 = objWE2.AddComponent<TruthWorldEnvironment>();

            MC = objMC.AddComponent<MainCharacter>();
            MC.AvailableMana = true;
            MC.AvailableSkill = true;
            SC = objSC.AddComponent<MainCharacter>();
            SC.AvailableMana = true;
            SC.AvailableSkill = true;
            TC = objTC.AddComponent<MainCharacter>();
            TC.AvailableMana = true;
            TC.AvailableSkill = true;

            SQL = objSQL.AddComponent<ControlSQL>();
            SQL.SetupSql();

            // debug
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
            UnityEngine.Object.DontDestroyOnLoad(WE);
            UnityEngine.Object.DontDestroyOnLoad(WE2);
            UnityEngine.Object.DontDestroyOnLoad(sound);
            UnityEngine.Object.DontDestroyOnLoad(soundSource);
            UnityEngine.Object.DontDestroyOnLoad(bgm);
            UnityEngine.Object.DontDestroyOnLoad(bgmSource);
            UnityEngine.Object.DontDestroyOnLoad(SQL);
            return true;
        }
		public static void InitializeNetworkConnection()
		{
			if (GroundOne.CS == null) {
				GroundOne.CS = new ClientSocket();
			}
//			GroundOne.CS.rcm += new ClientSocket.ReceiveClientMessage (ReceiveFromClientSocket);
			IPAddress ipAddress = IPAddress.Parse ("133.242.151.26");
			IPEndPoint serverEP = new IPEndPoint (ipAddress, 8001);
			GroundOne.IsConnect = CS.Connect (SaveData.GetName(), serverEP, 3000);
		}

        #region "BGM再生と効果音関連"
        public static void PlaySoundEffect(string soundName)
        {
            if (GroundOne.EnableSoundEffect)
            {
                soundSource.clip = Resources.Load<AudioClip>(Database.BaseSoundFolder + soundName);
                soundSource.Play();
            }
        }

        public static void PlayDungeonMusic(string targetMusicName, int loopBegin)
        {
            PlayDungeonMusic(targetMusicName, string.Empty, loopBegin);
        }
        public static void PlayDungeonMusic(string targetMusicName, string targetMusicName2, int loopBegin)
        {
            if (GroundOne.EnableBGM)
            {
                bgmSource.Stop();
                bgmSource.clip = Resources.Load<AudioClip>(Database.BaseMusicFolder + targetMusicName);
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }

        public static void StopDungeonMusic()
        {
            bgmSource.Stop();
        }

        public static void TempStopDungeonMusic()
        {
            bgmSource.mute = true;
        }

        public static void ResumeDungeonMusic()
        {
            bgmSource.mute = false;
        }
        #endregion

    }
}

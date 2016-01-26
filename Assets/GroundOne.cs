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
        public static List<Sprite> resourceList = null; // リソース画像データ

        public static GameObject objMC = new GameObject("objMC");
        public static GameObject objSC = new GameObject("objSC");
        public static GameObject objTC = new GameObject("objTC");
        public static GameObject objWE = new GameObject("objWE");
        public static GameObject objWE2 = new GameObject("objWE2");
        public static MainCharacter MC = null;
        public static MainCharacter SC = null;
        public static MainCharacter TC = null;
        public static WorldEnvironment WE = null;
        public static TruthWorldEnvironment WE2 = null; // ゲームストーリー全体のワールド環境フラグ

        public static bool[] Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo2 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo3 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo4 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        public static bool[] Truth_KnownTileInfo5 = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        public static bool NoFirstMusic = false;
        public static int BattleSpeed = 3;
        public static int Difficulty = 1; // ゲーム難易度 デフォルトは１：普通

//        public static TruthInformation information = null; // ヘルプ情報 // todo
//        public static XepherPlayer sound = null; // サウンド音源 // todo
        public static bool EnableBGM = true; // ミュージック、デフォルトはオン
        public static bool EnableSoundEffect = true; // 効果音、デフォルトはオン

        public static bool AlreadyInitialize = false; // 既に一度InitializeGroundOneを呼んだかどうか

        public static bool CallBattleSetting = false;

        // TruthEquipmentShop
        public static string titleName = "天下一品　ガンツの武具店";
        // TruthBattleEnemy
        public static battleResult BattleResult = battleResult.None;
        // TruthSelectEquipment
        public static int EquipType = 0; // 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        // TruthBattleEnemy
        public static string enemyName1 = string.Empty;
        public static string enemyName2 = string.Empty;
        public static string enemyName3 = string.Empty;
        // TruthStatusPlayer
        public static bool LevelUp = false; // レベルアップモード画面
        public static int UpPoint = 0; // パラメタアップポイント
        public static int CumultiveLvUpValue = 0; // レベルアップカウント累積値
        public static bool OnlySelectTrash = false; // 捨てる限定画面
        public static string CannotSelectTrash = string.Empty; // 対象アイテムが重要品で捨てられない場合。
        public static bool DuelMode = false; // Duelモード
        
        // SaveLoad
        public static bool SaveMode = false; // false:Load true:Save

        // TruthHomeTown
        public static bool TruthHomeTown_NowExit = false;

        // Title
        public static bool Title_LoadAndGo = false;

        public static bool InitializeGroundOne()
        {
            Debug.Log("InitializeGroundOne start");

            if (AlreadyInitialize == false) { AlreadyInitialize = true; }
            else { Debug.Log("already initialize"); return false; }

            GroundOne.resourceList = new List<Sprite>();
            GroundOne.resourceList.AddRange(Resources.LoadAll<Sprite>(""));
            GroundOne.Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            WE = objWE.AddComponent<WorldEnvironment>();
            WE.DungeonArea = 1;

            //WE.Truth_CommunicationFirstHomeTown = true;
            //WE.TruthCompleteArea1 = true;

            WE.AvailableFirstCharacter = true;
            //WE.AvailableSecondCharacter = true;
            //WE.AvailableThirdCharacter = true;
            WE.AvailableInstantCommand = true;
            GroundOne.enemyName1 = Database.ENEMY_HIYOWA_BEATLE;

            WE.AvailableMana = true;
            WE.AvailableSkill = true;

            //UpPoint = 2638;

            WE2 = objWE2.AddComponent<TruthWorldEnvironment>();
            //WE2.TruthRecollection1 = true;

            MC = objMC.AddComponent<MainCharacter>();
            MC.FirstName = Database.EIN_WOLENCE;
            MC.FullName = Database.EIN_WOLENCE_FULL;
            MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
            MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
            MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
            MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
            MC.Mind = Database.MAINPLAYER_FIRST_MIND;
            MC.CurrentLife = 10;
            MC.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
            MC.SubWeapon = new ItemBackPack(Database.POOR_HINSO_SHIELD);
            MC.MainArmor = new ItemBackPack(Database.COMMON_FINE_ARMOR);
            MC.Accessory = new ItemBackPack(Database.COMMON_RED_PENDANT);
            MC.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_PENDANT);
            MC.AddBackPack(new ItemBackPack(Database.RARE_TOOMI_BLUE_SUISYOU));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_BASTARD_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_RED_PENDANT));
            MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_TWEI_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_PENDANT));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_BRONZE_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_GUST_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.RARE_DARKNESS_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_MASTER_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.RARE_SWORD_OF_DIVIDE));
            MC.AddBackPack(new ItemBackPack(Database.RARE_TYOU_KOU_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_BLUE_MASEKI));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_PURPLE_MASEKI));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_VIKING_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_GREEN_MASEKI));
            MC.AddBackPack(new ItemBackPack(Database.COMMON_INITIATE_SWORD));
            MC.AddBackPack(new ItemBackPack(Database.RARE_AERO_BLADE));
            MC.AddBackPack(new ItemBackPack(Database.EPIC_OVER_SHIFTING));
            // debug
            MC.AvailableMana = true;
            MC.AvailableSkill = true;
            MC.FreshHeal = true;
            MC.Protection = true;
            MC.HolyShock = true;
            MC.SaintPower = true;
            MC.Glory = true;
            MC.Resurrection = true;
            MC.CelestialNova = true;
            MC.DarkBlast = true;
            MC.ShadowPact = true;
            MC.LifeTap = true;
            MC.DevouringPlague = true;
            MC.BlackContract = true;
            MC.BloodyVengeance = true;
            MC.Damnation = true;
            MC.FireBall = true;
            MC.FlameAura = true;
            MC.HeatBoost = true;
            MC.VolcanicWave = true;
            MC.FlameStrike = true;
            MC.ImmortalRave = true;
            MC.LavaAnnihilation = true;
            MC.IceNeedle = true;
            MC.AbsorbWater = true;
            MC.Cleansing = true;
            MC.MirrorImage = true;
            MC.FrozenLance = true;
            MC.PromisedKnowledge = true;
            MC.AbsoluteZero = true;
            MC.WordOfPower = true;
            MC.GaleWind = true;
            MC.WordOfLife = true;
            MC.WordOfFortune = true;
            MC.AetherDrive = true;
            MC.Genesis = true;
            MC.EternalPresence = true;
            MC.DispelMagic = true;
            MC.RiseOfImage = true;
            MC.Tranquility = true;
            MC.Deflection = true;
            MC.OneImmunity = true;
            MC.WhiteOut = true;
            MC.TimeStop = true;
            MC.StraightSmash = true;
            MC.DoubleSlash = true;
            MC.CrushingBlow = true;
            MC.SoulInfinity = true;
            MC.CounterAttack = true;
            MC.PurePurification = true;
            MC.AntiStun = true;
            MC.StanceOfDeath = true;
            MC.StanceOfFlow = true;
            MC.EnigmaSence = true;
            MC.SilentRush = true;
            MC.OboroImpact = true;
            MC.StanceOfStanding = true;
            MC.InnerInspiration = true;
            MC.KineticSmash = true;
            MC.Catastrophe = true;
            MC.TruthVision = true;
            MC.HighEmotionality = true;
            MC.StanceOfEyes = true;
            MC.PainfulInsanity = true;
            MC.Negate = true;
            MC.VoidExtraction = true;
            MC.CarnageRush = true;
            MC.NothingOfNothingness = true;
            MC.PsychicTrance = true;
            MC.BlindJustice = true;
            MC.TranscendentWish = true;
            MC.FlashBlaze = true;
            MC.LightDetonator = true;
            MC.AscendantMeteor = true;
            MC.SkyShield = true;
            MC.SacredHeal = true;
            MC.EverDroplet = true;
            MC.HolyBreaker = true;
            MC.ExaltedField = true;
            MC.HymnContract = true;
            MC.StarLightning = true;
            MC.AngelBreath = true;
            MC.EndlessAnthem = true;
            MC.BlackFire = true;
            MC.BlazingField = true;
            MC.DemonicIgnite = true;
            MC.BlueBullet = true;
            MC.DeepMirror = true;
            MC.DeathDeny = true;
            MC.WordOfMalice = true;
            MC.AbyssEye = true;
            MC.SinFortune = true;
            MC.DarkenField = true;
            MC.DoomBlade = true;
            MC.EclipseEnd = true;
            MC.FrozenAura = true;
            MC.ChillBurn = true;
            MC.ZetaExplosion = true;
            MC.EnrageBlast = true;
            MC.PiercingFlame = true;
            MC.SigilOfHomura = true;
            MC.Immolate = true;
            MC.PhantasmalWind = true;
            MC.RedDragonWill = true;
            MC.WordOfAttitude = true;
            MC.StaticBarrier = true;
            MC.AusterityMatrix = true;
            MC.VanishWave = true;
            MC.VortexField = true;
            MC.BlueDragonWill = true;
            MC.SeventhMagic = true;
            MC.ParadoxImage = true;
            MC.WarpGate = true;
            MC.NeutralSmash = true;
            MC.StanceOfDouble = true;
            MC.SwiftStep = true;
            MC.VigorSense = true;
            MC.CircleSlash = true;
            MC.RisingAura = true;
            MC.RumbleShout = true;
            MC.OnslaughtHit = true;
            MC.SmoothingMove = true;
            MC.AscensionAura = true;
            MC.FutureVision = true;
            MC.UnknownShock = true;
            MC.ReflexSpirit = true;
            MC.FatalBlow = true;
            MC.SharpGlare = true;
            MC.ConcussiveHit = true;
            MC.TrustSilence = true;
            MC.MindKilling = true;
            MC.SurpriseAttack = true;
            MC.StanceOfMystic = true;
            MC.PsychicWave = true;
            MC.NourishSense = true;
            MC.Recover = true;
            MC.ImpulseHit = true;
            MC.ViolentSlash = true;
            MC.ONEAuthority = true;
            MC.OuterInspiration = true;
            MC.HardestParry = true;
            MC.StanceOfSuddenness = true;
            MC.SoulExecution = true;
            
            MC.Gold = 5000;
            MC.Exp += 350;

            MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            MC.BattleActionCommandList[2] = Database.HEAT_BOOST;
            MC.BattleActionCommandList[3] = Database.FRESH_HEAL;
            MC.BattleActionCommandList[4] = Database.STRAIGHT_SMASH;
            MC.BattleActionCommandList[5] = Database.WORD_OF_LIFE;
            MC.BattleActionCommandList[6] = Database.LAVA_ANNIHILATION;
            MC.BattleActionCommandList[7] = Database.GALE_WIND;
            MC.BattleActionCommandList[8] = Database.STAY_EN;

            SC = objSC.AddComponent<MainCharacter>();
            SC.FirstName = Database.RANA_AMILIA;
            SC.FullName = Database.RANA_AMILIA_FULL;
            SC.AvailableMana = true;
            SC.AvailableSkill = true;
            SC.AddBackPack(new ItemBackPack(Database.COMMON_ELECTRO_ROD));
            SC.BattleActionCommandList[0] = Database.ATTACK_EN;
            SC.BattleActionCommandList[1] = Database.ICE_NEEDLE;
            SC.BattleActionCommandList[2] = Database.SHADOW_PACT;
            SC.BattleActionCommandList[3] = Database.COUNTER_ATTACK;
            SC.BattleActionCommandList[4] = Database.ENIGMA_SENSE;
            SC.BattleActionCommandList[5] = Database.STAY_EN;
            SC.BattleActionCommandList[6] = Database.STAY_EN;
            SC.BattleActionCommandList[7] = Database.STAY_EN;
            SC.BattleActionCommandList[8] = Database.STAY_EN;

            TC = objTC.AddComponent<MainCharacter>();
            TC.FirstName = Database.OL_LANDIS;
            TC.FullName = Database.OL_LANDIS_FULL;
            TC.AvailableMana = true;
            TC.AvailableSkill = true;
            TC.BattleActionCommandList[0] = Database.ATTACK_EN;
            TC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            TC.BattleActionCommandList[2] = Database.STAY_EN;
            TC.BattleActionCommandList[3] = Database.STAY_EN;
            TC.BattleActionCommandList[4] = Database.STAY_EN;
            TC.BattleActionCommandList[5] = Database.STAY_EN;
            TC.BattleActionCommandList[6] = Database.STAY_EN;
            TC.BattleActionCommandList[7] = Database.STAY_EN;
            TC.BattleActionCommandList[8] = Database.STAY_EN;
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
        public static void InitializeSoundData()
        {
//            try
//            {
//                if (GroundOne.sound == null)
//                {
//                    GroundOne.sound = new XepherPlayer();
//                }
//                GroundOne.sound.InitializeSoundList();
//            }
//            catch
//            {
//                GroundOne.EnableSoundEffect = false;
//                GroundOne.EnableBGM = false;
//                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
//            }
        }
        public static void PlaySoundEffect(string soundName)
        {
//            try
//            {
//                if (GroundOne.EnableSoundEffect)
//                {
//                    if (GroundOne.sound == null)
//                    {
//                        GroundOne.sound = new XepherPlayer();
//                    }
//                    GroundOne.sound.PlayMP3(soundName);
//                }
//            }
//            catch
//            {
//                GroundOne.EnableSoundEffect = false;
//                GroundOne.EnableBGM = false;
//                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
//            }
        }

        public static void PlayDungeonMusic(string targetMusicName, int loopBegin)
        {
//            PlayDungeonMusic(targetMusicName, string.Empty, loopBegin);
        }
        public static void PlayDungeonMusic(string targetMusicName, string targetMusicName2, int loopBegin)
        {
//            try
//            {
//                if (GroundOne.EnableBGM)
//                {
//                    if (GroundOne.sound == null)
//                    {
//                        GroundOne.sound = new XepherPlayer();
//                    }
//                    else
//                    {
//                        GroundOne.sound.StopMusic();
//                    }
//                    GroundOne.sound.PlayMusic(targetMusicName, targetMusicName2, loopBegin);
//                }
//            }
//            catch
//            {
//                GroundOne.EnableSoundEffect = false;
//                GroundOne.EnableBGM = false;
//                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
//            }
        }

        public static void StopDungeonMusic()
        {
//            try
//            {
//                if (GroundOne.EnableBGM)
//                {
//                    if (GroundOne.sound != null)
//                    {
//                        GroundOne.sound.StopMusic();
//                    }
//                }
//            }
//            catch
//            {
//                GroundOne.EnableSoundEffect = false;
//                GroundOne.EnableBGM = false;
//                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
//            }
        }

        public static void TempStopDungeonMusic()
        {
//            if (GroundOne.sound != null)
//            {
//                GroundOne.sound.UpdateMuteFlag(true);
//            }
//        }
//
//        public static void ResumeDungeonMusic()
//        {
//            if (GroundOne.sound != null)
//            {
//                GroundOne.sound.UpdateMuteFlag(false);
//            }
        }
        #endregion

    }
}

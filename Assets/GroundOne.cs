//using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

namespace DungeonPlayer {
    public static class GroundOne
    {
        public static string OwnerName = "YuasaTomonori"; // ゲーム接続オーナー名 [todo] ゲーム開始時点でオーナーに名前を入れてもらい、記憶する必要がある。
        public static string guid = "e9a30180-000e-4144-8130-c90c9f317c2f";
		public static ClientSocket CS = null; // サーバー接続ソケット
		public static bool IsConnect = false; // サーバー接続OKサイン
        public static List<string> playbackMessage = new List<string>(); // プレイバックメッセージテキスト
        public static List<Sprite> resourceList = null; // リソース画像データ

        public static MainCharacter MC = null;
        public static MainCharacter SC = null;
        public static MainCharacter TC = null;
        public static WorldEnvironment WE = null;

        public static bool[] Truth_KnownTileInfo = null;
        public static bool[] Truth_KnownTileInfo2 = null;
        public static bool[] Truth_KnownTileInfo3 = null;
        public static bool[] Truth_KnownTileInfo4 = null;
        public static bool[] Truth_KnownTileInfo5 = null;

        public static bool NoFirstMusic = false;
        public static int BattleSpeed = 3;
        public static int Difficulty = 1; // ゲーム難易度 デフォルトは１：普通

        public static TruthWorldEnvironment WE2 = null; // ゲームストーリー全体のワールド環境フラグ
//        public static TruthInformation information = null; // ヘルプ情報 // todo
//        public static XepherPlayer sound = null; // サウンド音源 // todo
        public static bool EnableBGM = true; // ミュージック、デフォルトはオン
        public static bool EnableSoundEffect = true; // 効果音、デフォルトはオン

        public static void InitializeGroundOne()
        {
            if (GroundOne.resourceList == null)
            {
                GroundOne.resourceList = new List<Sprite>();
                GroundOne.resourceList.AddRange(Resources.LoadAll<Sprite>(""));
            }
            if (GroundOne.Truth_KnownTileInfo == null)
            {
                GroundOne.Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            }

            if (GroundOne.WE == null)
            {
                GroundOne.WE = new WorldEnvironment();
                GroundOne.WE.DungeonArea = 1;
            }
            if (GroundOne.WE2 == null)
            {
                GroundOne.WE2 = new TruthWorldEnvironment();
            }

            if (GroundOne.MC == null)
            {
                GameObject obj = new GameObject("obj");
                GroundOne.MC = obj.AddComponent<MainCharacter>();
                // debug
                GroundOne.MC.AvailableMana = true;
                GroundOne.MC.FreshHeal = true;
                GroundOne.MC.Protection = true;
                GroundOne.MC.HolyShock = true;
                GroundOne.MC.SaintPower = true;
                GroundOne.MC.Glory = true;
                GroundOne.MC.Resurrection = true;
                GroundOne.MC.CelestialNova = true;
                GroundOne.MC.DarkBlast = true;
                GroundOne.MC.ShadowPact = true;
                GroundOne.MC.LifeTap = true;
                GroundOne.MC.DevouringPlague = true;
                GroundOne.MC.BlackContract = true;
                GroundOne.MC.BloodyVengeance = true;
                GroundOne.MC.Damnation = true;
                GroundOne.MC.FireBall = true;
                GroundOne.MC.FlameAura = true;
                GroundOne.MC.HeatBoost = true;
                GroundOne.MC.VolcanicWave = true;
                GroundOne.MC.FlameStrike = true;
                GroundOne.MC.ImmortalRave = true;
                GroundOne.MC.LavaAnnihilation = true;
                GroundOne.MC.IceNeedle = true;
                GroundOne.MC.AbsorbWater = true;
                GroundOne.MC.Cleansing = true;
                GroundOne.MC.MirrorImage = true;
                GroundOne.MC.FrozenLance = true;
                GroundOne.MC.PromisedKnowledge = true;
                GroundOne.MC.AbsoluteZero = true;
                GroundOne.MC.WordOfPower = true;
                GroundOne.MC.GaleWind = true;
                GroundOne.MC.WordOfLife = true;
                GroundOne.MC.WordOfFortune = true;
                GroundOne.MC.AetherDrive = true;
                GroundOne.MC.Genesis = true;
                GroundOne.MC.EternalPresence = true;
                GroundOne.MC.DispelMagic = true;
                GroundOne.MC.RiseOfImage = true;
                GroundOne.MC.Tranquility = true;
                GroundOne.MC.Deflection = true;
                GroundOne.MC.OneImmunity = true;
                GroundOne.MC.WhiteOut = true;
                GroundOne.MC.TimeStop = true;
                GroundOne.MC.StraightSmash = true;
                GroundOne.MC.DoubleSlash = true;
                GroundOne.MC.CrushingBlow = true;
                GroundOne.MC.SoulInfinity = true;
                GroundOne.MC.CounterAttack = true;
                GroundOne.MC.PurePurification = true;
                GroundOne.MC.AntiStun = true;
                GroundOne.MC.StanceOfDeath = true;
                GroundOne.MC.StanceOfFlow = true;
                GroundOne.MC.EnigmaSence = true;
                GroundOne.MC.SilentRush = true;
                GroundOne.MC.OboroImpact = true;
                GroundOne.MC.StanceOfStanding = true;
                GroundOne.MC.InnerInspiration = true;
                GroundOne.MC.KineticSmash = true;
                GroundOne.MC.Catastrophe = true;
                GroundOne.MC.TruthVision = true;
                GroundOne.MC.HighEmotionality = true;
                GroundOne.MC.StanceOfEyes = true;
                GroundOne.MC.PainfulInsanity = true;
                GroundOne.MC.Negate = true;
                GroundOne.MC.VoidExtraction = true;
                GroundOne.MC.CarnageRush = true;
                GroundOne.MC.NothingOfNothingness = true;
                GroundOne.MC.PsychicTrance = true;
                GroundOne.MC.BlindJustice = true;
                GroundOne.MC.TranscendentWish = true;
                GroundOne.MC.FlashBlaze = true;
                GroundOne.MC.LightDetonator = true;
                GroundOne.MC.AscendantMeteor = true;
                GroundOne.MC.SkyShield = true;
                GroundOne.MC.SacredHeal = true;
                GroundOne.MC.EverDroplet = true;
                GroundOne.MC.HolyBreaker = true;
                GroundOne.MC.ExaltedField = true;
                GroundOne.MC.HymnContract = true;
                GroundOne.MC.StarLightning = true;
                GroundOne.MC.AngelBreath = true;
                GroundOne.MC.EndlessAnthem = true;
                GroundOne.MC.BlackFire = true;
                GroundOne.MC.BlazingField = true;
                GroundOne.MC.DemonicIgnite = true;
                GroundOne.MC.BlueBullet = true;
                GroundOne.MC.DeepMirror = true;
                GroundOne.MC.DeathDeny = true;
                GroundOne.MC.WordOfMalice = true;
                GroundOne.MC.AbyssEye = true;
                GroundOne.MC.SinFortune = true;
                GroundOne.MC.DarkenField = true;
                GroundOne.MC.DoomBlade = true;
                GroundOne.MC.EclipseEnd = true;
                GroundOne.MC.FrozenAura = true;
                GroundOne.MC.ChillBurn = true;
                GroundOne.MC.ZetaExplosion = true;
                GroundOne.MC.EnrageBlast = true;
                GroundOne.MC.PiercingFlame = true;
                GroundOne.MC.SigilOfHomura = true;
                GroundOne.MC.Immolate = true;
                GroundOne.MC.PhantasmalWind = true;
                GroundOne.MC.RedDragonWill = true;
                GroundOne.MC.WordOfAttitude = true;
                GroundOne.MC.StaticBarrier = true;
                GroundOne.MC.AusterityMatrix = true;
                GroundOne.MC.VanishWave = true;
                GroundOne.MC.VortexField = true;
                GroundOne.MC.BlueDragonWill = true;
                GroundOne.MC.SeventhMagic = true;
                GroundOne.MC.ParadoxImage = true;
                GroundOne.MC.WarpGate = true;
                GroundOne.MC.NeutralSmash = true;
                GroundOne.MC.StanceOfDouble = true;
                GroundOne.MC.SwiftStep = true;
                GroundOne.MC.VigorSense = true;
                GroundOne.MC.CircleSlash = true;
                GroundOne.MC.RisingAura = true;
                GroundOne.MC.RumbleShout = true;
                GroundOne.MC.OnslaughtHit = true;
                GroundOne.MC.SmoothingMove = true;
                GroundOne.MC.AscensionAura = true;
                GroundOne.MC.FutureVision = true;
                GroundOne.MC.UnknownShock = true;
                GroundOne.MC.ReflexSpirit = true;
                GroundOne.MC.FatalBlow = true;
                GroundOne.MC.SharpGlare = true;
                GroundOne.MC.ConcussiveHit = true;
                GroundOne.MC.TrustSilence = true;
                GroundOne.MC.MindKilling = true;
                GroundOne.MC.SurpriseAttack = true;
                GroundOne.MC.StanceOfMystic = true;
                GroundOne.MC.PsychicWave = true;
                GroundOne.MC.NourishSense = true;
                GroundOne.MC.Recover = true;
                GroundOne.MC.ImpulseHit = true;
                GroundOne.MC.ViolentSlash = true;
                GroundOne.MC.ONEAuthority = true;
                GroundOne.MC.OuterInspiration = true;
                GroundOne.MC.HardestParry = true;
                GroundOne.MC.StanceOfSuddenness = true;
                GroundOne.MC.SoulExecution = true;



            }
            if (GroundOne.SC == null)
            {
                GroundOne.SC = new MainCharacter();
            }
            if (GroundOne.TC == null)
            {
                GroundOne.TC = new MainCharacter();
            }
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
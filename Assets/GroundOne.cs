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
                GroundOne.MC = new MainCharacter();
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
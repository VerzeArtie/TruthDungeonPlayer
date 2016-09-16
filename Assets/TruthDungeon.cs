using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class TruthDungeon : MotherForm
    {
        int nowReading = 0;
        List<string> nowMessage = new List<string>();
        List<MessagePack.ActionEvent> nowEvent = new List<MessagePack.ActionEvent>();

        private bool DungeonViewMode = false; // ダンジョンマップの全体を見たいときに使うフラグ
        private Vector2 DungeonViewModeMasterLocation = new Vector2(); // ダンジョン全体マップView表示時の元のViewの位置
        private Vector2 DungeonViewModeMasterPlayerLocation = new Vector2(); //  ダンジョン全体マップView表示時のプレイヤーの元のViewの位置
        private int MovementInterval = 0; // ダンジョンマップ全体を見ている時のインターバル

        // GUI
        public GameObject groupDayLabel;
        public Text dayLabel;
        public Text dungeonAreaLabel;
        public GameObject groupSystemMessage;
        public Text systemMessageText;
        public GameObject groupArrow;
        public GameObject back_playback;
        public Text[] playbackText;
        public GameObject GroupMenu;
        public GameObject btnStatus;
        public GameObject btnBattleSetting;
        public GameObject btnSave;
        public GameObject btnLoad;
        public GameObject btnExit;
        public GameObject GroupsubMenu;
        public GameObject HelpManual;
        public GameObject DungeonView;
        public GameObject PlayBack;
        public GameObject BlueOrbImage;
        public GameObject BlueOrbText;
        public GameObject PathfindingModeImage;
        public GameObject groupPlayerList;
        public GameObject FirstPlayerPanel;
        public GameObject SecondPlayerPanel;
        public GameObject ThirdPlayerPanel;
        public Text FirstPlayerName;
        public Text SecondPlayerName;
        public Text ThirdPlayerName;
        public Image currentLife1;
        public Image currentLife2;
        public Image currentLife3;
        public Image currentSkillPoint1;
        public Image currentSkillPoint2;
        public Image currentSkillPoint3;
        public Image currentManaPoint1;
        public Image currentManaPoint2;
        public Image currentManaPoint3;
        public Text currentLifeValue1;
        public Text currentLifeValue2;
        public Text currentLifeValue3;
        public Text currentSkillValue1;
        public Text currentSkillValue2;
        public Text currentSkillValue3;
        public Text currentManaValue1;
        public Text currentManaValue2;
        public Text currentManaValue3;

        // initialize data list
        List<GameObject> objList = new List<GameObject>();
        List<GameObject> objOther = new List<GameObject>();
        List<GameObject> objBlueWallTop = new List<GameObject>();
        List<GameObject> objBlueWallLeft = new List<GameObject>();
        List<GameObject> objBlueWallRight = new List<GameObject>();
        List<GameObject> objBlueWallBottom = new List<GameObject>();
        List<GameObject> unknownTile = new List<GameObject>();
        List<GameObject> objTreasureList = new List<GameObject>();
        List<int> objTreasureNum = new List<int>();

        public Text mainMessage;
        public Image back_vigilance;
        public Text labelVigilance;
        public Button btnOK;
        public Button btnYes;
        public Button btnNo;
        
        // from TruthDecision
        public GameObject noticePanel;
        public Text NoticeMessage;
        public Text FirstMessage;
        public Text SecondMessage;

        // 到達・未到達領域を示すためのタイル情報
        string[] tileInfo = null;
        string[] tileInfo2 = null;
        string[] tileInfo3 = null;
        string[] tileInfo4 = null;
        string[] tileInfo5 = null;

        private int battleSpeed;
        public int BattleSpeed
        {
            get { return battleSpeed; }
            set { battleSpeed = value; }
        }
        private int difficulty;
        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        GameObject Player = null;
        Vector3 viewPoint = new Vector3(); // ダンジョンビュー位置

        // 敵の強さを区分けするためのタイルカラー情報
        int[] tileColor = new int[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        // ダンジョンマップの基本タイル情報
        private GameObject prefab_TileElement = null;

        // ダンジョンマップ上にあるイベント要素情報
        bool[] treasureBoxTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] boardTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] upstairTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] downstairTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] mirrorTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] blueOrbTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] fountainTile = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        // ダンジョンマップ上にある条件付青壁の情報
        const int BLUE_WALL_NUM = 4;
        bool[] blueWallTop = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] blueWallLeft = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] blueWallRight = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        bool[] blueWallBottom = new bool[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        bool arrowDown = false; // add unity
        bool arrowUp = false; // add unity
        bool arrowLeft = false; // add unity
        bool arrowRight = false; // add unity
        bool keyDown = false;
        bool keyUp = false;
        bool keyLeft = false;
        bool keyRight = false;
        int MOVE_INTERVAL = 50;
        int interval = 0;

        bool nowEncountEnemy = false;
        bool execEncountEnemy = false;
        bool ignoreCreateShadow = false;

        bool firstAction = false;

        private DungeonPlayer.MessagePack.ActionEvent currentEvent; // MessagePackのevent情報

        private int stepCounter = 0; // 敵エンカウント率調整の値

        // 各種イベント発生時の変数群
        int nowAgilityRoomCounter = 0;

        bool nowDecisionFloor1OpenDoor = false; // １階ボス部屋を開く際のフラグ
        int nowDecisionFloor2EightAnswer = 0; // ２階TruthDecision2を呼び出すためのフラグ
        int failCounter = 0; // ２階、技の部屋で失敗した時のフラグ
        bool detectKeyUp = false; // ２階、技の部屋Ｃでキーを離した事を示すフラグ

        // ダンジョン２階の技の部屋、エリア２に関する記述
        int ShadowTileNumber = -1;
        int BeforeDirectionNumber = 0; // 1:左 2:上 3:下
        // ダンジョン２階の技の部屋、エリア４に関する記述
        int Area4_InnerTimerCount = 0;
        int Area4_ShadowTileNum = -1;

        bool nowDecisionFloor2OpenDoor = false; // ２階ボスの後の扉を開く際のフラグ
        bool nowDecisionFloor3OpenDoor = false; // ３階ボス前の扉を開く際のフラグ

        int nowIntelligenceRoom = 0; // ２階、知の部屋レバー数値をインプットするためのフラグ

        bool nowIntelligenceRoomGodSequence = false; // ２階、知の部屋、神々の試練を実行中

        int nowFloor4Area3LeverNumber = 0; // ４階、エリア３「事実」のレバー番号
        int nowFloor4Area3LeverNumber2 = 0; // ４階、エリア３「真実」のレバー番号

        // ３階無限回廊
        const int INFINITE_LOOP_MAX = 12; // 無限回廊の組み合わせ最大数
        int[] infinityLoopNumber = new int[INFINITE_LOOP_MAX]; // 無限解
        int[] playerLoopNumber = new int[INFINITE_LOOP_MAX]; // プレイヤー解

        bool nowMirrorRoomGodSequence = false; // ３階、鏡の部屋、神々の試練を実行中

        bool nowSelectCharacter = false; // ５階、メンバー選定中

        bool nowAutoMove = false; // 現実世界、自動移動中
        List<int> nowAutoMoveNumber = new List<int>();

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_FIRST_DUNGEON);
            if (Application.platform == RuntimePlatform.Android)
            {
                MOVE_INTERVAL = 1;
                this.groupArrow.SetActive(true);
            }
            else
            {
                MOVE_INTERVAL = 50;
                this.groupArrow.SetActive(false);
            }
            this.interval = MOVE_INTERVAL;
            this.MovementInterval = MOVE_INTERVAL;

            GroundOne.WE.SaveByDungeon = true;
            GroundOne.WE.AlreadyCommunicate = false;
            GroundOne.WE.AlreadyEquipShop = false;
            GroundOne.WE.alreadyCommunicateCahlhanz = false;
            GroundOne.WE.AlreadyRest = false;
            this.dayLabel.text = GroundOne.WE.GameDay.ToString() + "日目";
            this.dungeonAreaLabel.text = GroundOne.WE.DungeonArea.ToString() + "　階";

            btnBattleSetting.SetActive(GroundOne.WE.AvailableBattleSettingMenu);

            tileInfo = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo2 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo3 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo4 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo5 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];

            this.prefab_TileElement = new GameObject();
            this.prefab_TileElement.AddComponent<SpriteRenderer>();
            this.prefab_TileElement.transform.position = new Vector3(-99999, -99999, 0);

            this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.PLAYER_MARK);
            this.Player = Instantiate(this.prefab_TileElement, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            if (GroundOne.WE.DungeonArea == 0 || GroundOne.WE.DungeonArea == 1)
            {
                ReadDungeonTileFromXmlFile(@"DungeonMapping_T_1");
            }
            else if (GroundOne.WE.DungeonArea == 2)
            {
                ReadDungeonTileFromXmlFile(@"DungeonMapping_T_2");
            }
            else if (GroundOne.WE.DungeonArea == 3)
            {
                ReadDungeonTileFromXmlFile(@"DungeonMapping_T_3");
            }
            else if (GroundOne.WE.DungeonArea == 4)
            {
                ReadDungeonTileFromXmlFile(@"DungeonMapping_T_4");
            }
            else if (GroundOne.WE.DungeonArea == 5)
            {
                ReadDungeonTileFromXmlFile(@"DungeonMapping_T_5");
            }

            // 始めて開始する場合、あらかじめスタート地点を設定。
            if ((GroundOne.WE.DungeonPosX == 0) && (GroundOne.WE.DungeonPosY == 0))
            {
                switch (GroundOne.WE.DungeonArea)
                {
                    case 0:
                    case 1:
                        JumpToLocation(39, -14, true);
                        break;
                    case 2:
                        JumpToLocation(29, -19, true);
                        break;
                    case 3:
                        JumpToLocation(0, -19, true);
                        break;
                    case 4:
                        JumpToLocation(52, -18, true);
                        break;
                    case 5:
                        JumpToLocation(57, -2, true);
                        break;
                }
            }
            else
            {
                JumpToLocation(GroundOne.WE.DungeonPosX, GroundOne.WE.DungeonPosY, true);
            }
            UpdateUnknownTile();

            SetupPlayerStatus(true);
            UpdateMainMessage("", true);

            GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);

            #region "戦闘終了判定"
            #region "死亡時、再挑戦する場合、初めから戦闘画面を呼びなおす。"
            if (GroundOne.BattleResult == GroundOne.battleResult.Retry)
            {
                GroundOne.BattleResult = GroundOne.battleResult.None;
                CopyShadowToMain();
                this.ignoreCreateShadow = true;
                this.nowEncountEnemy = true;
            }
            #endregion
            #region "敗北して、ゲーム終了を選択した時"
            else if (GroundOne.BattleResult == GroundOne.battleResult.Ignore)
            {
                CopyShadowToMain();
                GroundOne.BattleResult = GroundOne.battleResult.None;
                if (GroundOne.enemyName1 == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BRILLIANT_SEA_PRINCE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SHELL_SWORD_KNIGHT)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEVIATHAN)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_HOWLING_SEIZER)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                    MessagePack.Message16018_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_OL_LANDIS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                    MessagePack.Message16045_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                    MessagePack.Message16070_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                    MessagePack.Message16071_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }

                // DUELモードは現実世界でDUEL戦闘となった時に再戦を判断させたいため、一旦ここでfalse返しとする。
                if (GroundOne.DuelMode)
                {
                    // after 続きのメッセージを実装先へと繋いでください。
                }
                else
                {
                    yesnoSystemMessage.text = Database.Message_SaveRequest1;
                    groupYesnoSystemMessage.SetActive(true);
                }
            }
            #endregion
            #region "逃げた時、経験値とゴールドは入らない。(つまり、何もしない）"
            else if (GroundOne.BattleResult == GroundOne.battleResult.Abort)
            {
                GroundOne.BattleResult = GroundOne.battleResult.None;
                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);

                if (GroundOne.enemyName1 == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BRILLIANT_SEA_PRINCE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SHELL_SWORD_KNIGHT)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEVIATHAN)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_HOWLING_SEIZER)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                    MessagePack.Message16018_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_OL_LANDIS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
                    MessagePack.Message16045_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                    MessagePack.Message16070_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, true);
                    MessagePack.Message16071_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
            }
            #endregion
            #region "戦闘に勝利した場合（通常ルート）"
            else if (GroundOne.BattleResult == GroundOne.battleResult.OK)
            {
                GroundOne.BattleResult = GroundOne.battleResult.None;

                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);

                // todo レベルアップできなくなるのでは？要確認
                // ボスに勝利した時、フラグ更新を行う。
                if (GroundOne.enemyName1 == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                {
                    GroundOne.WE.TruthCompleteSlayBoss1 = true;
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BRILLIANT_SEA_PRINCE)
                {
                    MessagePack.Message12044_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN)
                {
                    MessagePack.Message12045_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SHELL_SWORD_KNIGHT)
                {
                    MessagePack.Message12046_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    MessagePack.Message12047_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                {
                    MessagePack.Message12048_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEVIATHAN)
                {
                    GroundOne.WE.TruthCompleteSlayBoss2 = true;
                    MessagePack.Message12049_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_DRAGON_TINKOU_DEEPSEA)
                {
                    MessagePack.Message12066_4(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_HOWLING_SEIZER)
                {
                    GroundOne.WE.TruthCompleteSlayBoss3 = true;
                    MessagePack.Message13111_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_DRAGON_DESOLATOR_AZOLD)
                {
                    MessagePack.Message13122_4(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                {
                    MessagePack.Message14040_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                {
                    MessagePack.Message14066_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                {
                    MessagePack.Message14097_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    GroundOne.WE.TruthCompleteSlayBoss5 = true;
                    MessagePack.Message15006_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
                {
                    MessagePack.Message16018_Success(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_OL_LANDIS)
                {
                    MessagePack.Message16045_Success(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    MessagePack.Message16071(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    MessagePack.Message16072(ref nowMessage, ref nowEvent);
                    tapOK();
                }

                // 戦闘終了後、レベルアップがあるなら、ステータス画面を開く
                if (GroundOne.Player1Levelup && GroundOne.WE.AvailableFirstCharacter)
                {
                    SceneDimension.CallTruthStatusPlayer(this, ref GroundOne.Player1Levelup, ref GroundOne.Player1UpPoint, ref GroundOne.Player1CumultiveLvUpValue, GroundOne.MC.PlayerStatusColor);
                    return;
                }
                else if (GroundOne.Player2Levelup && GroundOne.WE.AvailableSecondCharacter)
                {
                    SceneDimension.CallTruthStatusPlayer(this, ref GroundOne.Player2Levelup, ref GroundOne.Player2UpPoint, ref GroundOne.Player2CumultiveLvUpValue, GroundOne.SC.PlayerStatusColor);
                    return;
                }
                else if (GroundOne.Player3Levelup && GroundOne.WE.AvailableThirdCharacter)
                {
                    SceneDimension.CallTruthStatusPlayer(this, ref GroundOne.Player3Levelup, ref GroundOne.Player3UpPoint, ref GroundOne.Player3CumultiveLvUpValue, GroundOne.TC.PlayerStatusColor);
                    return;
                }

                // after
                //  bool alreadyPlayBackMusic = false;
                //  if (GroundOne.WE.AvailableFirstCharacter)
                //  {
                //      this.MC = tempMC;
                //      this.GroundOne.MC.ReplaceBackPack(tempGroundOne.MC.GetBackPackInfo());
                //      if (GroundOne.MC.Level < Database.CHARACTER_MAX_LEVEL5)
                //      {
                //          GroundOne.MC.Exp += be.EC1.Exp;
                //      }
                //      GroundOne.MC.Gold += be.EC1.Gold;

                //      int levelUpPoint = 0;
                //      int cumultiveLvUpValue = 0;
                //      while (true)
                //      {
                //          if (GroundOne.MC.Exp >= GroundOne.MC.NextLevelBorder && GroundOne.MC.Level < Database.CHARACTER_MAX_LEVEL5)
                //          {
                //              levelUpPoint += GroundOne.MC.LevelUpPointTruth;
                //              GroundOne.MC.BaseLife += GroundOne.MC.LevelUpLifeTruth;
                //              GroundOne.MC.BaseMana += GroundOne.MC.LevelUpManaTruth;
                //              GroundOne.MC.Exp = GroundOne.MC.Exp - GroundOne.MC.NextLevelBorder;
                //              GroundOne.MC.Level += 1;
                //              cumultiveLvUpValue++;
                //          }
                //          else
                //          {
                //              break;
                //          }
                //      }

                //      if (cumultiveLvUpValue > 0)
                //      {
                //          GroundOne.PlaySoundEffect("LvUp");
                //          if (!alreadyPlayBackMusic)
                //          {
                //              alreadyPlayBackMusic = true;
                //              GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                //          }
                //          using (TruthStatusPlayer sp = new TruthStatusPlayer())
                //          {
                //              sp.WE = we;
                //              sp.MC = mc;
                //              sp.SC = sc;
                //              sp.TC = tc;
                //              sp.CurrentStatusView = GroundOne.MC.PlayerStatusColor;
                //              sp.LevelUp = true;
                //              sp.UpPoint = levelUpPoint;
                //              sp.CumultiveLvUpValue = cumultiveLvUpValue;
                //              sp.StartPosition = FormStartPosition.CenterParent;
                //              sp.ShowDialog();
                //          }

                //      }

                //      bool detect = false;
                //      string targetGetName = String.Empty;
                //      if (GroundOne.MC.MainWeapon != null)
                //      {
                //          if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO && GroundOne.WE2.PracticeSwordCount >= 5)
                //          {
                //              targetGetName = Database.POOR_PRACTICE_SWORD_1;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_1 && GroundOne.WE2.PracticeSwordCount >= 15)
                //          {
                //              targetGetName = Database.POOR_PRACTICE_SWORD_2;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_2 && GroundOne.WE2.PracticeSwordCount >= 30 && GroundOne.WE.CompleteSlayBoss2)
                //          {
                //              targetGetName = Database.COMMON_PRACTICE_SWORD_3;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_3 && GroundOne.WE2.PracticeSwordCount >= 50 && GroundOne.WE.CompleteSlayBoss2)
                //          {
                //              targetGetName = Database.COMMON_PRACTICE_SWORD_4;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_4 && GroundOne.WE2.PracticeSwordCount >= 75 && GroundOne.WE.CompleteSlayBoss3)
                //          {
                //              targetGetName = Database.RARE_PRACTICE_SWORD_5;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_5 && GroundOne.WE2.PracticeSwordCount >= 105 && GroundOne.WE.CompleteSlayBoss3)
                //          {
                //              targetGetName = Database.RARE_PRACTICE_SWORD_6;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_6 && GroundOne.WE2.PracticeSwordCount >= 140 && GroundOne.WE.CompleteSlayBoss4)
                //          {
                //              targetGetName = Database.EPIC_PRACTICE_SWORD_7;
                //          }
                //          else if (GroundOne.MC.MainWeapon.Name == Database.EPIC_PRACTICE_SWORD_7 && GroundOne.WE2.PracticeSwordCount >= 180 && GroundOne.WE.CompleteSlayBoss5)
                //          {
                //              targetGetName = Database.LEGENDARY_FELTUS;
                //          }

                //          if (targetGetName != String.Empty)
                //          {
                //              GroundOne.MC.MainWeapon = new ItemBackPack(targetGetName);
                //              detect = true;
                //          }
                //      }
                //      if ((GroundOne.MC.SubWeapon != null))
                //      {
                //          if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO && GroundOne.WE2.PracticeSwordCount >= 5)
                //          {
                //              targetGetName = Database.POOR_PRACTICE_SWORD_1;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_1 && GroundOne.WE2.PracticeSwordCount >= 20)
                //          {
                //              targetGetName = Database.POOR_PRACTICE_SWORD_2;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_2 && GroundOne.WE2.PracticeSwordCount >= 50 && GroundOne.WE.CompleteSlayBoss2)
                //          {
                //              targetGetName = Database.COMMON_PRACTICE_SWORD_3;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.COMMON_PRACTICE_SWORD_3 && GroundOne.WE2.PracticeSwordCount >= 100 && GroundOne.WE.CompleteSlayBoss2)
                //          {
                //              targetGetName = Database.COMMON_PRACTICE_SWORD_4;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.COMMON_PRACTICE_SWORD_4 && GroundOne.WE2.PracticeSwordCount >= 200 && GroundOne.WE.CompleteSlayBoss3)
                //          {
                //              targetGetName = Database.RARE_PRACTICE_SWORD_5;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.RARE_PRACTICE_SWORD_5 && GroundOne.WE2.PracticeSwordCount >= 400 && GroundOne.WE.CompleteSlayBoss3)
                //          {
                //              targetGetName = Database.RARE_PRACTICE_SWORD_6;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.RARE_PRACTICE_SWORD_6 && GroundOne.WE2.PracticeSwordCount >= 700 && GroundOne.WE.CompleteSlayBoss4)
                //          {
                //              targetGetName = Database.EPIC_PRACTICE_SWORD_7;
                //          }
                //          else if (GroundOne.MC.SubWeapon.Name == Database.EPIC_PRACTICE_SWORD_7 && GroundOne.WE2.PracticeSwordCount >= 1000 && GroundOne.WE.CompleteSlayBoss5)
                //          {
                //              targetGetName = Database.LEGENDARY_FELTUS;
                //          }

                //          if (targetGetName != String.Empty)
                //          {
                //              GroundOne.MC.SubWeapon = new ItemBackPack(targetGetName);
                //              detect = true;
                //          }
                //      }

                //      if (detect)
                //      {
                //          if (targetGetName == Database.POOR_PRACTICE_SWORD_1)
                //          {
                //              UpdateMainMessage("アイン：（この剣・・・）");

                //              UpdateMainMessage("アイン：（何か持つ感触が変わったな。以前より鋭くなった感じがする。）");

                //              UpdateMainMessage("アイン：（すげえ・・・ひょっとして成長する剣だったりするのか！？）");

                //              UpdateMainMessage("アイン：（サンキューガンツ伯父さん、ありがたく使わせてもらうぜ。）");
                //          }
                //          else if (targetGetName == Database.POOR_PRACTICE_SWORD_2)
                //          {
                //              UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                //              UpdateMainMessage("アイン：（でも、何だろうな・・・何か違う感じもするが・・・）");

                //              UpdateMainMessage("アイン：（まあ、良いか。このままガンガン使っていくぜ！）");
                //          }
                //          else if (targetGetName == Database.COMMON_PRACTICE_SWORD_3)
                //          {
                //              UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                //              UpdateMainMessage("アイン：（すげえぜ、この剣。最大値ばかりが上がって行くな。）");

                //              UpdateMainMessage("アイン：（使いこなせるかどうかだが・・・）");

                //              UpdateMainMessage("アイン：（まあ、この際だ。使えるだけ使ってみるとするか！）");
                //          }
                //          else if (targetGetName == Database.COMMON_PRACTICE_SWORD_4)
                //          {
                //              UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                //              UpdateMainMessage("アイン：（しかしどんどん値が伸びていくな・・・）");

                //              UpdateMainMessage("アイン：（今の俺でどこまで使いこなせるか、わかんねえけどな）");

                //              UpdateMainMessage("アイン：（まあ気にしててもしょうがねえ、ドンドン上げていくぜ！）");
                //          }
                //          else if (targetGetName == Database.RARE_PRACTICE_SWORD_5)
                //          {
                //              UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                //              UpdateMainMessage("アイン：（しかし、新しくなってるハズなんだが・・・");

                //              UpdateMainMessage("アイン：（何となく懐かしい感じもするんだよな）");

                //              UpdateMainMessage("アイン：（MAXまで上げきったら、ガンツ伯父さんにでも聞いてみるか）");
                //          }
                //          else if (targetGetName == Database.RARE_PRACTICE_SWORD_6)
                //          {
                //              UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                //              UpdateMainMessage("アイン：（練習用の剣なんて、大嘘もいいとこじゃねえか）");

                //              UpdateMainMessage("アイン：（・・・この、モヤモヤした感覚・・・）");

                //              UpdateMainMessage("アイン：（・・・　・・・まあ、上げてくか！）");
                //          }
                //          else if (targetGetName == Database.EPIC_PRACTICE_SWORD_7)
                //          {
                //              UpdateMainMessage("アイン：（剣レベルアップ・・・と言いたい所だが）");

                //              UpdateMainMessage("アイン：（そっか・・・何を忘れてたんだろうな、俺）");

                //              UpdateMainMessage("アイン：（ッハハハ・・・そりゃ、そうだよな。情けねえぜ）");

                //              UpdateMainMessage("アイン：（たぶん次でラストのレベルアップだ。やらせてもらうぜ）");
                //          }
                //          else if (targetGetName == Database.LEGENDARY_FELTUS)
                //          {
                //              // [コメント] 何か演出が欲しい。
                //              UpdateMainMessage("アイン：（神剣、フェルトゥーシュだったんだ、コレ・・・）");

                //              UpdateMainMessage("アイン：（そうさ・・・俺はコイツで・・・）");

                //              UpdateMainMessage("アイン：（いや、これを受け止めなければならないんだ、俺は）");

                //              UpdateMainMessage("アイン：（・・・今度こそ、心に決めたぜ）");

                //              UpdateMainMessage("アイン：（この剣からは逃げない）");

                //              UpdateMainMessage("アイン：（っしゃ！　それじゃ使うぜ、フェルトゥーシュを！）");
                //          }
                //          using (MessageDisplayWithIcon mdwi = new MessageDisplayWithIcon())
                //          {
                //              ItemBackPack item = new ItemBackPack(targetGetName);
                //              mdwi.Message = item.Name + "を入手した！";
                //              mdwi.Item = item;
                //              GroundOne.PlaySoundEffect(Database.SOUND_LVUP_FELTUS);
                //              mdwi.StartPosition = FormStartPosition.CenterParent;
                //              mdwi.ShowDialog();
                //          }
                //      }
                //  }



                //  if (GroundOne.WE.AvailableSecondCharacter)
                //  {
                //      this.SC = tempSC;
                //      this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                //      if (sc.Level < Database.CHARACTER_MAX_LEVEL5)
                //      {
                //          sc.Exp += be.EC1.Exp;
                //      }
                //      //SC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                //      int levelUpPoint = 0;
                //      int cumultiveLvUpValue = 0;
                //      while (true)
                //      {
                //          if (sc.Exp >= sc.NextLevelBorder && sc.Level < Database.CHARACTER_MAX_LEVEL5)
                //          {
                //              levelUpPoint += sc.LevelUpPointTruth;
                //              sc.BaseLife += sc.LevelUpLifeTruth;
                //              sc.BaseMana += sc.LevelUpManaTruth;
                //              sc.Exp = sc.Exp - sc.NextLevelBorder;
                //              sc.Level += 1;
                //              cumultiveLvUpValue++;
                //          }
                //          else
                //          {
                //              break;
                //          }
                //      }

                //      if (cumultiveLvUpValue > 0)
                //      {
                //          GroundOne.PlaySoundEffect("LvUp");
                //          if (!alreadyPlayBackMusic)
                //          {
                //              alreadyPlayBackMusic = true;
                //              GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                //          }
                //          using (TruthStatusPlayer sp = new TruthStatusPlayer())
                //          {
                //              sp.WE = we;
                //              sp.MC = mc;
                //              sp.SC = sc;
                //              sp.TC = tc;
                //              sp.CurrentStatusView = sc.PlayerStatusColor;
                //              sp.LevelUp = true;
                //              sp.UpPoint = levelUpPoint;
                //              sp.CumultiveLvUpValue = cumultiveLvUpValue;
                //              sp.StartPosition = FormStartPosition.CenterParent;
                //              sp.ShowDialog();
                //          }

                //      }
                //  }

                //  if (GroundOne.WE.AvailableThirdCharacter)
                //  {
                //      this.TC = tempTC;
                //      this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                //      if (tc.FullName == Database.OL_LANDIS_FULL)
                //      {
                //          if (tc.Level < Database.CHARACTER_MAX_LEVEL2)
                //          {
                //              tc.Exp += be.EC1.Exp;
                //          }
                //      }
                //      else if (tc.FullName == Database.VERZE_ARTIE_FULL)
                //      {
                //          if (tc.Level < Database.CHARACTER_MAX_LEVEL5)
                //          {
                //              tc.Exp += be.EC1.Exp;
                //          }
                //      }
                //      //TC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                //      int levelUpPoint = 0;
                //      int cumultiveLvUpValue = 0;
                //      while (true)
                //      {
                //          if (tc.Exp >= tc.NextLevelBorder && tc.Level < Database.CHARACTER_MAX_LEVEL5)
                //          {
                //              levelUpPoint += tc.LevelUpPointTruth;
                //              tc.BaseLife += tc.LevelUpLifeTruth;
                //              tc.BaseMana += tc.LevelUpManaTruth;
                //              tc.Exp = tc.Exp - tc.NextLevelBorder;
                //              tc.Level += 1;
                //              cumultiveLvUpValue++;
                //          }
                //          else
                //          {
                //              break;
                //          }
                //      }

                //      if (cumultiveLvUpValue > 0)
                //      {
                //          GroundOne.PlaySoundEffect("LvUp");
                //          if (!alreadyPlayBackMusic)
                //          {
                //              alreadyPlayBackMusic = true;
                //              GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                //          }
                //          using (TruthStatusPlayer sp = new TruthStatusPlayer())
                //          {
                //              sp.WE = we;
                //              sp.MC = mc;
                //              sp.SC = sc;
                //              sp.TC = tc;
                //              sp.CurrentStatusView = tc.PlayerStatusColor;
                //              sp.LevelUp = true;
                //              sp.UpPoint = levelUpPoint;
                //              sp.CumultiveLvUpValue = cumultiveLvUpValue;
                //              sp.StartPosition = FormStartPosition.CenterParent;
                //              sp.ShowDialog();
                //          }

                //      }
                //  }
                //  this.WE = tempWE;

                //  if (!alreadyPlayBackMusic && (ec1.Name != "五階の守護者：Bystander" && enemyName != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS && enemyName != Database.ENEMY_LAST_SINIKIA_KAHLHANZ && enemyName != Database.ENEMY_LAST_OL_LANDIS))
                //  {
                //      alreadyPlayBackMusic = true;
                //      GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                //  }
                //  SetupPlayerStatus();
                //  return true;
            }
            #endregion
            #endregion
        }
        
        private void ReadDungeonTileFromXmlFile(string xmlFileName)
        {
            #region "ダンジョンマップをXMLから読み込み"
            XmlDocument xml = new XmlDocument();
            TextAsset filename = (TextAsset)Resources.Load(xmlFileName);
            xml.LoadXml(filename.text);

            XmlNodeList currentList = xml.GetElementsByTagName("TileData");
            XmlNodeList childList = currentList[0].ChildNodes;
            Debug.Log("childlist count " + childList.Count.ToString() + "\r\n");

            #region "変数初期化と未探索タイル設置"
            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                treasureBoxTile[ii] = false;
                boardTile[ii] = false;
                upstairTile[ii] = false;
                downstairTile[ii] = false;
                mirrorTile[ii] = false;
                blueOrbTile[ii] = false;
                fountainTile[ii] = false;
                blueWallBottom[ii] = false;
                blueWallLeft[ii] = false;
                blueWallRight[ii] = false;
                blueWallTop[ii] = false;

                string current = Database.TILEINFO_10;
                // １階真実解
                if ((GroundOne.WE.DungeonArea == 1) && GroundOne.WE.dungeonEvent27 && !GroundOne.WE.TruthSpecialInfo1 && (ii == 29 * Database.TRUTH_DUNGEON_COLUMN + 47))
                {
                    current = Database.TILEINFO_10_2;
                }

                this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + current);
                this.unknownTile.Add(Instantiate(this.prefab_TileElement, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
            }
            #endregion
            #region "宝箱や看板などの設置"
            try
            {
                XmlNodeList currentList2 = xml.GetElementsByTagName("OtherData");
                XmlNodeList childList2 = currentList2[0].ChildNodes;

                string OTHER1 = "TreasureInfo";
                string OTHER2 = "BoardInfo";
                string OTHER3 = "UpstairInfo";
                string OTHER4 = "DownstairInfo";
                string OTHER5 = "BlueWallTopInfo";
                string OTHER6 = "BlueWallLeftInfo";
                string OTHER7 = "BlueWallRightInfo";
                string OTHER8 = "BlueWallBottomInfo";
                string OTHER9 = "MirrorInfo";
                string OTHER10 = "BlueOrbInfo";
                string OTHER11 = "FountainInfo";
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    if (childList2[ii].Name.Contains(OTHER1))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.TREASURE_BOX);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER1.Length, childList2[ii].Name.Length - OTHER1.Length));
                        int row = targetNumber / Database.TRUTH_DUNGEON_COLUMN;
                        int column = targetNumber % Database.TRUTH_DUNGEON_COLUMN;
                        GameObject current = Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject;
                        if (DetectOpenTreasure(new Vector3(column, -row, 0)))
                        {
                            current.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.TREASURE_BOX_OPEN);
                        }
                        this.objOther.Add(current);
                        this.objTreasureList.Add(current);
                        this.objTreasureNum.Add(targetNumber);
                    }
                    else if (childList2[ii].Name.Contains(OTHER2))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BOARD);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER2.Length, childList2[ii].Name.Length - OTHER2.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER3))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.UPSTAIR);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER3.Length, childList2[ii].Name.Length - OTHER3.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER4))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.DOWNSTAIR);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER4.Length, childList2[ii].Name.Length - OTHER4.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER9))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.MIRROR);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER9.Length, childList2[ii].Name.Length - OTHER9.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER10))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BLUEORB);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER10.Length, childList2[ii].Name.Length - OTHER10.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER11))
                    {
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FOUNTAIN);
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER11.Length, childList2[ii].Name.Length - OTHER11.Length));
                        this.objOther.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    if (childList2[ii].Name.Contains(OTHER5))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER5.Length, childList2[ii].Name.Length - OTHER5.Length));
                        blueWallTop[targetNumber] = true;
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BLUE_WALL_T);
                        this.objBlueWallTop.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                        this.objBlueWallTop[this.objBlueWallTop.Count - 1].name = "bluewall_" + targetNumber.ToString();

                        // １階
                        if (GroundOne.WE.DungeonArea == 1)
                        {
                            if (GroundOne.WE.dungeonEvent14KeyOpen && (targetNumber == 27 * Database.TRUTH_DUNGEON_COLUMN + 21))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent12KeyOpen && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 21))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 27 * Database.TRUTH_DUNGEON_COLUMN + 14))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 14))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent21KeyOpen && (targetNumber == 13 * Database.TRUTH_DUNGEON_COLUMN + 2))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent22KeyOpen && (targetNumber == 13 * Database.TRUTH_DUNGEON_COLUMN + 4))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                        }

                        // ２階
                        if (GroundOne.WE.DungeonArea == 2)
                        {
                            if ((GroundOne.WE.dungeonEvent205 && (targetNumber == 18 * Database.TRUTH_DUNGEON_COLUMN + 33)) ||
                                (GroundOne.WE.dungeonEvent205 && (targetNumber == 21 * Database.TRUTH_DUNGEON_COLUMN + 25)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 3 * Database.TRUTH_DUNGEON_COLUMN + 42)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 46)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 38)) ||
                                (GroundOne.WE.dungeonEvent224 && (targetNumber == 9 * Database.TRUTH_DUNGEON_COLUMN + 35)) ||
                                (GroundOne.WE.dungeonEvent250_SlayBoss && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 21)) ||
                                (GroundOne.WE.dungeonEvent251_SlayBoss && (targetNumber == 24 * Database.TRUTH_DUNGEON_COLUMN + 21)) ||
                                (GroundOne.WE.dungeonEvent253_SlayBoss && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 6)) ||
                                (GroundOne.WE.dungeonEvent254_SlayBoss && (targetNumber == 38 * Database.TRUTH_DUNGEON_COLUMN + 5)) ||
                                (GroundOne.WE.dungeonEvent255_SlayBoss && (targetNumber == 29 * Database.TRUTH_DUNGEON_COLUMN + 14)))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                        }

                        // ３階
                        if (GroundOne.WE.DungeonArea == 3)
                        {
                            // とくになし
                        }

                        // ４階
                        if (GroundOne.WE.DungeonArea == 4)
                        {
                            if ((GroundOne.WE.dungeonEvent402 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 45)) ||
                                (GroundOne.WE.dungeonEvent4_key1_1_open && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 43)) ||
                                (GroundOne.WE.dungeonEvent4_key1_2_open && (targetNumber == 6 * Database.TRUTH_DUNGEON_COLUMN + 45)) ||
                                (GroundOne.WE.dungeonEvent4_key1_4_open && (targetNumber == 4 * Database.TRUTH_DUNGEON_COLUMN + 31)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss1 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 47)) ||
                                (GroundOne.WE.dungeonEvent429 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 6)) ||
                                (GroundOne.WE.dungeonEvent431 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 10)) ||
                                (GroundOne.WE.dungeonEvent431 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent437 && (targetNumber == 14 * Database.TRUTH_DUNGEON_COLUMN + 16)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss2 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 20)) ||
                                (GroundOne.WE.dungeonEvent488 && (targetNumber == 32 * Database.TRUTH_DUNGEON_COLUMN + 48)))
                            {
                                blueWallTop[targetNumber] = false;
                            }
                        }

                        if (blueWallTop[targetNumber] == false)
                        {
                            this.objBlueWallTop[this.objBlueWallTop.Count - 1].SetActive(false);
                        }
                    }
                    if (childList2[ii].Name.Contains(OTHER6))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER6.Length, childList2[ii].Name.Length - OTHER6.Length));
                        blueWallLeft[targetNumber] = true;
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BLUE_WALL_L);
                        this.objBlueWallLeft.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                        this.objBlueWallLeft[this.objBlueWallLeft.Count-1].name = "bluewall_" + targetNumber.ToString();

                        // １階
                        if (GroundOne.WE.DungeonArea == 1)
                        {
                            if (GroundOne.WE.dungeonEvent11KeyOpen && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 23))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent13KeyOpen && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 23))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 13))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 13))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 10 * Database.TRUTH_DUNGEON_COLUMN + 16))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent23KeyOpen && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 6))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent20 && (targetNumber == 16 * Database.TRUTH_DUNGEON_COLUMN + 13))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent24KeyOpen && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 6))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent28KeyOpen && (targetNumber == 6 * Database.TRUTH_DUNGEON_COLUMN + 6))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                        }

                        // ２階
                        if (GroundOne.WE.DungeonArea == 2)
                        {
                            if ((GroundOne.WE.dungeonEvent205 && (targetNumber == 23 * Database.TRUTH_DUNGEON_COLUMN + 31)) ||
                                (GroundOne.WE.dungeonEvent205 && (targetNumber == 15 * Database.TRUTH_DUNGEON_COLUMN + 28)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 50)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 50)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 14 * Database.TRUTH_DUNGEON_COLUMN + 59)) ||
                                (GroundOne.WE.dungeonEvent230 && (targetNumber == 15 * Database.TRUTH_DUNGEON_COLUMN + 47)) ||
                                (GroundOne.WE.dungeonEvent230 && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 50)) ||
                                (GroundOne.WE.dungeonEvent252_SlayBoss && (targetNumber == 18 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent263_KeyOpen && (targetNumber == 26 * Database.TRUTH_DUNGEON_COLUMN + 17)))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                        }
                        // ３階
                        if (GroundOne.WE.DungeonArea == 3)
                        {
                            if ((GroundOne.WE.dungeonEvent305 && (targetNumber == 19 * Database.TRUTH_DUNGEON_COLUMN + 19)) ||
                                (GroundOne.WE.dungeonEvent319KeyOpen && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 55)))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                        }
                        // ４階
                        if (GroundOne.WE.DungeonArea == 4)
                        {
                            if ((GroundOne.WE.dungeonEvent401 && (targetNumber == 18 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE.dungeonEvent4_key1_3_open && (targetNumber == 4 * Database.TRUTH_DUNGEON_COLUMN + 36)) ||
                                (GroundOne.WE.dungeonEvent4_key1_5_open && (targetNumber == 12 * Database.TRUTH_DUNGEON_COLUMN + 34)) ||
                                (GroundOne.WE.dungeonEvent4_key1_6 && (targetNumber == 13 * Database.TRUTH_DUNGEON_COLUMN + 41)) ||
                                (GroundOne.WE.dungeonEvent4_key1_6_open && (targetNumber == 6 * Database.TRUTH_DUNGEON_COLUMN + 48)) ||
                                (GroundOne.WE.dungeonEvent4_key1_7_open && (targetNumber == 3 * Database.TRUTH_DUNGEON_COLUMN + 54)) ||
                                (GroundOne.WE.dungeonEvent4_key1_8_open && (targetNumber == 1 * Database.TRUTH_DUNGEON_COLUMN + 56)) ||
                                (GroundOne.WE.dungeonEvent4_key1_9_open && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 52)) ||
                                (GroundOne.WE.dungeonEvent426 && (targetNumber == 19 * Database.TRUTH_DUNGEON_COLUMN + 18)) ||
                                (GroundOne.WE.dungeonEvent427 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent433 && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 12)) ||
                                (GroundOne.WE.dungeonEvent435 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 12)) ||
                                (GroundOne.WE.dungeonEvent435 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 12)) ||
                                (GroundOne.WE.dungeonEvent442 && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 18)) ||
                                (GroundOne.WE.dungeonEvent473 && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 23)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss3 && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 23)) ||
                                (GroundOne.WE.dungeonEvent477 && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE.dungeonEvent481 && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 48)) ||
                                (GroundOne.WE.dungeonEvent483 && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 51)) ||
                                (GroundOne.WE.dungeonEvent483 && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 51)) ||
                                (GroundOne.WE.dungeonEvent485 && (targetNumber == 22 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE2.SeekerEvent1 && (targetNumber == 20 * Database.TRUTH_DUNGEON_COLUMN + 49)))
                            {
                                blueWallLeft[targetNumber] = false;
                            }
                        }

                        if (blueWallLeft[targetNumber] == false)
                        {
                            this.objBlueWallLeft[this.objBlueWallLeft.Count - 1].SetActive(false);
                        }
                    }
                    if (childList2[ii].Name.Contains(OTHER7))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER7.Length, childList2[ii].Name.Length - OTHER7.Length));
                        blueWallRight[targetNumber] = true;
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BLUE_WALL_R);
                        this.objBlueWallRight.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                        this.objBlueWallRight[this.objBlueWallRight.Count - 1].name = "bluewall_" + targetNumber.ToString();

                        // １階
                        if (GroundOne.WE.DungeonArea == 1)
                        {
                            if (GroundOne.WE.dungeonEvent11KeyOpen && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 22))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent13KeyOpen && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 22))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 12))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 12))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 10 * Database.TRUTH_DUNGEON_COLUMN + 15))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent23KeyOpen && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 5))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent20 && (targetNumber == 16 * Database.TRUTH_DUNGEON_COLUMN + 12))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent24KeyOpen && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 5))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent28KeyOpen && (targetNumber == 6 * Database.TRUTH_DUNGEON_COLUMN + 5))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                        }

                        // ２階
                        if (GroundOne.WE.DungeonArea == 2)
                        {
                            if ((GroundOne.WE.dungeonEvent205 && (targetNumber == 23 * Database.TRUTH_DUNGEON_COLUMN + 30)) ||
                                (GroundOne.WE.dungeonEvent205 && (targetNumber == 15 * Database.TRUTH_DUNGEON_COLUMN + 27)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 14 * Database.TRUTH_DUNGEON_COLUMN + 58)) ||
                                (GroundOne.WE.dungeonEvent230 && (targetNumber == 15 * Database.TRUTH_DUNGEON_COLUMN + 46)) ||
                                (GroundOne.WE.dungeonEvent230 && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 49)) ||
                                (GroundOne.WE.dungeonEvent252_SlayBoss && (targetNumber == 18 * Database.TRUTH_DUNGEON_COLUMN + 10)) ||
                                (GroundOne.WE.dungeonEvent263_KeyOpen && (targetNumber == 26 * Database.TRUTH_DUNGEON_COLUMN + 16)))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                        }
                        // ３階
                        if (GroundOne.WE.DungeonArea == 3)
                        {
                            if ((GroundOne.WE.dungeonEvent305 && (targetNumber == 19 * Database.TRUTH_DUNGEON_COLUMN + 18)) ||
                                (GroundOne.WE.dungeonEvent319KeyOpen && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 54)))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                        }
                        // ４階
                        if (GroundOne.WE.DungeonArea == 4)
                        {
                            if ((GroundOne.WE.dungeonEvent401 && (targetNumber == 18 * Database.TRUTH_DUNGEON_COLUMN + 48)) ||
                                (GroundOne.WE.dungeonEvent4_key1_3_open && (targetNumber == 4 * Database.TRUTH_DUNGEON_COLUMN + 35)) ||
                                (GroundOne.WE.dungeonEvent4_key1_5_open && (targetNumber == 12 * Database.TRUTH_DUNGEON_COLUMN + 33)) ||
                                (GroundOne.WE.dungeonEvent4_key1_6 && (targetNumber == 13 * Database.TRUTH_DUNGEON_COLUMN + 40)) ||
                                (GroundOne.WE.dungeonEvent4_key1_6_open && (targetNumber == 6 * Database.TRUTH_DUNGEON_COLUMN + 47)) ||
                                (GroundOne.WE.dungeonEvent4_key1_7_open && (targetNumber == 3 * Database.TRUTH_DUNGEON_COLUMN + 53)) ||
                                (GroundOne.WE.dungeonEvent4_key1_8_open && (targetNumber == 1 * Database.TRUTH_DUNGEON_COLUMN + 55)) ||
                                (GroundOne.WE.dungeonEvent4_key1_9_open && (targetNumber == 11 * Database.TRUTH_DUNGEON_COLUMN + 51)) ||
                                (GroundOne.WE.dungeonEvent426 && (targetNumber == 19 * Database.TRUTH_DUNGEON_COLUMN + 17)) ||
                                (GroundOne.WE.dungeonEvent427 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 10)) ||
                                (GroundOne.WE.dungeonEvent433 && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent435 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent435 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent442 && (targetNumber == 33 * Database.TRUTH_DUNGEON_COLUMN + 17)) ||
                                (GroundOne.WE.dungeonEvent473 && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 22)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss3 && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 22)) ||
                                (GroundOne.WE.dungeonEvent477 && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 48)) ||
                                (GroundOne.WE.dungeonEvent481 && (targetNumber == 39 * Database.TRUTH_DUNGEON_COLUMN + 47)) ||
                                (GroundOne.WE.dungeonEvent483 && (targetNumber == 35 * Database.TRUTH_DUNGEON_COLUMN + 50)) ||
                                (GroundOne.WE.dungeonEvent483 && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 50)) ||
                                (GroundOne.WE.dungeonEvent485 && (targetNumber == 22 * Database.TRUTH_DUNGEON_COLUMN + 48)) ||
                                (GroundOne.WE2.SeekerEvent1 && (targetNumber == 20 * Database.TRUTH_DUNGEON_COLUMN + 48)))
                            {
                                blueWallRight[targetNumber] = false;
                            }
                        }

                        if (blueWallRight[targetNumber] == false)
                        {
                            this.objBlueWallRight[this.objBlueWallRight.Count - 1].SetActive(false);
                        }
                    }
                    if (childList2[ii].Name.Contains(OTHER8))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER8.Length, childList2[ii].Name.Length - OTHER8.Length));
                        blueWallBottom[targetNumber] = true;
                        this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.BLUE_WALL_B);
                        this.objBlueWallBottom.Add(Instantiate(this.prefab_TileElement, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                        this.objBlueWallBottom[this.objBlueWallBottom.Count - 1].name = "bluewall_" + targetNumber.ToString();

                        // １階
                        if (GroundOne.WE.DungeonArea == 1)
                        {
                            if (GroundOne.WE.dungeonEvent14KeyOpen && (targetNumber == 26 * Database.TRUTH_DUNGEON_COLUMN + 21))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent12KeyOpen && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 21))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 26 * Database.TRUTH_DUNGEON_COLUMN + 14))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent16 && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 14))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent21KeyOpen && (targetNumber == 12 * Database.TRUTH_DUNGEON_COLUMN + 2))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                            if (GroundOne.WE.dungeonEvent22KeyOpen && (targetNumber == 12 * Database.TRUTH_DUNGEON_COLUMN + 4))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                        }

                        // ２階
                        if (GroundOne.WE.DungeonArea == 2)
                        {
                            if ((GroundOne.WE.dungeonEvent205 && (targetNumber == 17 * Database.TRUTH_DUNGEON_COLUMN + 33)) ||
                                (GroundOne.WE.dungeonEvent205 && (targetNumber == 20 * Database.TRUTH_DUNGEON_COLUMN + 25)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 2 * Database.TRUTH_DUNGEON_COLUMN + 42)) ||
                                (GroundOne.WE.dungeonEvent211 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 46)) ||
                                (GroundOne.WE.dungeonEvent219 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 38)) ||
                                (GroundOne.WE.dungeonEvent224 && (targetNumber == 8 * Database.TRUTH_DUNGEON_COLUMN + 35)) ||
                                (GroundOne.WE.dungeonEvent250_SlayBoss && (targetNumber == 34 * Database.TRUTH_DUNGEON_COLUMN + 21)) ||
                                (GroundOne.WE.dungeonEvent251_SlayBoss && (targetNumber == 23 * Database.TRUTH_DUNGEON_COLUMN + 21)) ||
                                (GroundOne.WE.dungeonEvent253_SlayBoss && (targetNumber == 27 * Database.TRUTH_DUNGEON_COLUMN + 6)) ||
                                (GroundOne.WE.dungeonEvent254_SlayBoss && (targetNumber == 37 * Database.TRUTH_DUNGEON_COLUMN + 5)) ||
                                (GroundOne.WE.dungeonEvent255_SlayBoss && (targetNumber == 28 * Database.TRUTH_DUNGEON_COLUMN + 14)))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                        }

                        // ３階
                        if (GroundOne.WE.DungeonArea == 3)
                        {
                            // とくになし
                        }

                        // ４階
                        if (GroundOne.WE.DungeonArea == 4)
                        {
                            if ((GroundOne.WE.dungeonEvent402 && (targetNumber == 16 * Database.TRUTH_DUNGEON_COLUMN + 45)) ||
                                (GroundOne.WE.dungeonEvent4_key1_1_open && (targetNumber == 10 * Database.TRUTH_DUNGEON_COLUMN + 43)) ||
                                (GroundOne.WE.dungeonEvent4_key1_2_open && (targetNumber == 5 * Database.TRUTH_DUNGEON_COLUMN + 45)) ||
                                (GroundOne.WE.dungeonEvent4_key1_4_open && (targetNumber == 3 * Database.TRUTH_DUNGEON_COLUMN + 31)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss1 && (targetNumber == 16 * Database.TRUTH_DUNGEON_COLUMN + 47)) ||
                                (GroundOne.WE.dungeonEvent429 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 6)) ||
                                (GroundOne.WE.dungeonEvent431 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 10)) ||
                                (GroundOne.WE.dungeonEvent431 && (targetNumber == 7 * Database.TRUTH_DUNGEON_COLUMN + 11)) ||
                                (GroundOne.WE.dungeonEvent437 && (targetNumber == 13 * Database.TRUTH_DUNGEON_COLUMN + 16)) ||
                                (GroundOne.WE.dungeonEvent4_SlayBoss2 && (targetNumber == 16 * Database.TRUTH_DUNGEON_COLUMN + 20)) ||
                                (GroundOne.WE.dungeonEvent488 && (targetNumber == 31 * Database.TRUTH_DUNGEON_COLUMN + 48)))
                            {
                                blueWallBottom[targetNumber] = false;
                            }
                        }

                        if (blueWallBottom[targetNumber] == false)
                        {
                            this.objBlueWallBottom[this.objBlueWallBottom.Count - 1].SetActive(false);
                        }
                    }
                }
            }
            catch { }
            #endregion
            #region "ダンジョンタイルをファイル名→プレハブ変換"
            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                this.tileColor[ii] = Convert.ToInt32(childList[ii * 2 + 1].InnerText);

                string current = Convert.ToString(childList[ii * 2 + 0].InnerText);
                // １階、終わりの部屋。心層の壁解除
                if (GroundOne.WE.dungeonEvent31 && ii == 29 * Database.TRUTH_DUNGEON_COLUMN + 50)
                {
                    current = Database.TILEINFO_21;
                }
                // ２階数字タイル
                if (GroundOne.WE.DungeonArea == 2)
                {
                    if (ii == 11 * Database.TRUTH_DUNGEON_COLUMN + 42)
                    {
                        current = Database.TILEINFO_32;
                    }
                    if (ii == 13 * Database.TRUTH_DUNGEON_COLUMN + 38)
                    {
                        current = Database.TILEINFO_33;
                    }
                    if (ii == 15 * Database.TRUTH_DUNGEON_COLUMN + 40)
                    {
                        current = Database.TILEINFO_34;
                    }
                    if (ii == 19 * Database.TRUTH_DUNGEON_COLUMN + 38)
                    {
                        current = Database.TILEINFO_35;
                    }
                    if (ii == 17 * Database.TRUTH_DUNGEON_COLUMN + 42)
                    {
                        current = Database.TILEINFO_36;
                    }
                    if (ii == 19 * Database.TRUTH_DUNGEON_COLUMN + 46)
                    {
                        current = Database.TILEINFO_37;
                    }
                    if (ii == 11 * Database.TRUTH_DUNGEON_COLUMN + 44)
                    {
                        current = Database.TILEINFO_38;
                    }
                    if (ii == 15 * Database.TRUTH_DUNGEON_COLUMN + 46)
                    {
                        current = Database.TILEINFO_39;
                    }
                }

                // ２階、心の部屋。心層の壁解除
                if (GroundOne.WE.DungeonArea == 2 && GroundOne.WE.dungeonEvent249)
                {
                    if ((ii == 5 * Database.TRUTH_DUNGEON_COLUMN + 28) ||
                        (ii == 6 * Database.TRUTH_DUNGEON_COLUMN + 28) ||
                        (ii == 7 * Database.TRUTH_DUNGEON_COLUMN + 28))
                    {
                        current = Database.TILEINFO_13;
                    }
                }
                // ２階、技の部屋、隠し通路の壁解除
                if (GroundOne.WE.DungeonArea == 2 && GroundOne.WE.dungeonEvent258)
                {
                    if (ii == 36 * Database.TRUTH_DUNGEON_COLUMN + 59)
                    {
                        current = Database.TILEINFO_21;
                    }
                }

                // ２階、ダンジョン進行フラグに応じて、真実イベントへの防壁を解除
                if (GroundOne.WE.DungeonArea == 2 && GroundOne.WE2.TruthAnswer2_OK)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 26 + 13)
                    {
                        current = Database.TILEINFO_13;
                    }
                }

                // ４階、ダンジョン進行フラグに応じて、真実イベントへの防壁を解除
                if (GroundOne.WE.DungeonArea == 4 && GroundOne.WE.dungeonEvent424)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 19 + 44)
                    {
                        current = Database.TILEINFO_13;
                    }
                }
                if (GroundOne.WE.DungeonArea == 4 && GroundOne.WE.dungeonEvent440)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 21 + 20)
                    {
                        current = Database.TILEINFO_13;
                    }
                }
                if (GroundOne.WE.DungeonArea == 4 && GroundOne.WE.dungeonEvent476)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 34 + 22)
                    {
                        current = Database.TILEINFO_13;
                    }
                }
                if (GroundOne.WE.DungeonArea == 4 && GroundOne.WE.dungeonEvent489)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 32 + 46)
                    {
                        current = Database.TILEINFO_13;
                    }
                }
                if (GroundOne.WE.DungeonArea == 4 && GroundOne.WE2.SeekerEvent1)
                {
                    if (ii == Database.TRUTH_DUNGEON_COLUMN * 21 + 46)
                    {
                        current = Database.TILEINFO_13;
                    }
                }

                this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + current);
                this.objList.Add(Instantiate(this.prefab_TileElement, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);

                // unknownTileとTruth_KnownTileInfoはネームが反対ですが、意味付けは同じ本質です。
                if ((GroundOne.WE.DungeonArea == 1) || (GroundOne.WE.DungeonArea == 0))
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo[ii]);
                    tileInfo[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 2)
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo2[ii]);
                    tileInfo2[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 3)
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo3[ii]);
                    tileInfo3[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 4)
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo4[ii]);
                    tileInfo4[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 5)
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo5[ii]);
                    tileInfo5[ii] = current;
                }

            }
            #endregion
            #endregion

        }

        public override void Update()
        {
            base.Update();

            if (!this.firstAction)
            {
                this.firstAction = true;
                ShownEvent();
                return;
            }

            if (this.nowAgilityRoomCounter > 0)
            {
                //Debug.Log("agilitycounter: " + nowAgilityRoomCounter);
                this.nowAgilityRoomCounter--;
                if (this.nowAgilityRoomCounter <= 0)
                {
                    agilityRoomTimer_Tick();
                }
            }

            #region "AutoMove"
            if (this.nowAutoMove)
            {
                if (this.nowAutoMoveNumber.Count > 0)
                {
                    if (this.nowAutoMoveNumber[0] <= 3)
                    {
                        AutoMove(this.nowAutoMoveNumber[0]);
                        System.Threading.Thread.Sleep(50);
                    }
                    else if (3 < this.nowAutoMoveNumber[0] && this.nowAutoMoveNumber[0] < 500)
                    {
                        int sleep = this.nowAutoMoveNumber[0] / 10;
                        sleep = sleep * 10;
                        int move = this.nowAutoMoveNumber[0] - sleep;

                        AutoMove(move);
                        System.Threading.Thread.Sleep(sleep);
                    }
                    else if (this.nowAutoMoveNumber[0] == 10001)
                    {
                        JumpToLocation(14, -36, true);
                        UpdateUnknownTileArea3_0_6();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10002)
                    {
                        JumpToLocation(12, -12, true);
                        UpdateUnknownTileArea3_0_14();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10003)
                    {
                        JumpToLocation(8, -28, true);
                        UpdateUnknownTileArea3_0_16();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10004)
                    {
                        JumpToLocation(1, -38, true);
                        UpdateUnknownTile();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10005)
                    {
                        JumpByMirror_1_End();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10006)
                    {
                        JumpByMirror_TurnBack();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10038)
                    {
                        JumpByMirror_2_38();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10039)
                    {
                        JumpByMirror_2_39();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10040)
                    {
                        JumpByMirror_2_40();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10041)
                    {
                        JumpByMirror_2_41();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10042)
                    {
                        JumpByMirror_2_42();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10043)
                    {
                        JumpByMirror_2_43();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10044)
                    {
                        JumpByMirror_2_44();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10045)
                    {
                        JumpByMirror_2_45();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10046)
                    {
                        JumpByMirror_2_46();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10047)
                    {
                        JumpByMirror_2_47();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10048)
                    {
                        JumpByMirror_2_48();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10049)
                    {
                        JumpByMirror_2_49();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10050)
                    {
                        JumpByMirror_2_50();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10051)
                    {
                        JumpByMirror_2_51();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10052)
                    {
                        JumpByMirror_2_52();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10053)
                    {
                        JumpByMirror_2_53();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10054)
                    {
                        JumpByMirror_2_54();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10055)
                    {
                        JumpByMirror_2_55();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10056)
                    {
                        JumpByMirror_2_56();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10057)
                    {
                        JumpByMirror_2_57();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10058)
                    {
                        JumpByMirror_2_58();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10059)
                    {
                        JumpByMirror_2_59();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10060)
                    {
                        JumpByMirror_2_60();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10061)
                    {
                        JumpByMirror_2_61();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10062)
                    {
                        JumpByMirror_2_62();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10063)
                    {
                        JumpByMirror_2_63();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10064)
                    {
                        JumpByMirror_2_64();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10065)
                    {
                        JumpByMirror_2_65();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10066)
                    {
                        JumpByMirror_2_66();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10067)
                    {
                        JumpByMirror_2_67();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10068)
                    {
                        JumpByMirror_2_68();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10069)
                    {
                        JumpByMirror_2_69();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10070)
                    {
                        JumpByMirror_2_70();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10071)
                    {
                        JumpByMirror_2_71();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10072)
                    {
                        JumpByMirror_2_72();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10073)
                    {
                        JumpByMirror_2_73();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10074)
                    {
                        JumpByMirror_2_74();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10075)
                    {
                        JumpByMirror_2_75();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10076)
                    {
                        JumpByMirror_2_76();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10077)
                    {
                        JumpByMirror_2_77();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10078)
                    {
                        JumpByMirror_2_78();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10079)
                    {
                        JumpByMirror_2_79();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10080)
                    {
                        JumpByMirror_2_80();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10081)
                    {
                        JumpByMirror_2_81();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10082)
                    {
                        JumpByMirror_2_82();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10083)
                    {
                        JumpByMirror_2_83();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10084)
                    {
                        JumpByMirror_2_84();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10101)
                    {
                        JumpByMirror_TruthWay1A();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10102)
                    {
                        JumpByMirror_TruthWay1B();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10103)
                    {
                        JumpByMirror_TruthWay1C();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10104)
                    {
                        JumpByMirror_TruthWay1D();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10201)
                    {
                        JumpByMirror_TruthWay2A();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10202)
                    {
                        JumpByMirror_TruthWay2B();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10203)
                    {
                        JumpByMirror_TruthWay2C();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10204)
                    {
                        JumpByMirror_TruthWay2D();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10301)
                    {
                        JumpByMirror_TruthWay3A();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10302)
                    {
                        JumpByMirror_TruthWay3B();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10303)
                    {
                        JumpByMirror_TruthWay3C();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10304)
                    {
                        JumpByMirror_TruthWay3D();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10401)
                    {
                        JumpByMirror_TruthWay4A();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10402)
                    {
                        JumpByMirror_TruthWay4B();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10403)
                    {
                        JumpByMirror_TruthWay4C();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10404)
                    {
                        JumpByMirror_TruthWay4D();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10501)
                    {
                        JumpByMirror_TruthWay5A();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10502)
                    {
                        JumpByMirror_TruthWay5B();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10503)
                    {
                        JumpByMirror_TruthWay5C();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10504)
                    {
                        JumpByMirror_TruthWay5D();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10505)
                    {
                        JumpByMirror_TruthWay5E();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10601)
                    {
                        JumpByMirror_Recollection3();
                    }
                    else if (this.nowAutoMoveNumber[0] == 10602)
                    {
                        JumpByMirror_ZeroWay();
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(this.nowAutoMoveNumber[0]);
                    }
                    this.nowAutoMoveNumber.RemoveAt(0);
                }
                else
                {
                    this.nowAutoMoveNumber.Clear();
                    this.nowAutoMove = false;
                    ShownEvent();
                }
                return;
            }
            #endregion

            if (this.nowEncountEnemy)
            {
                this.nowEncountEnemy = false;
                mainMessage.text = "アイン：敵と遭遇だ！";
                CancelKeyDownMovement();
                this.execEncountEnemy = true;
            }
            else if (this.execEncountEnemy)
            {
                this.execEncountEnemy = false;
                if (this.ignoreCreateShadow == false)
                {
                    CreateShadowData();
                }
                CancelKeyDownMovement();
                SceneDimension.CallTruthBattleEnemy(Database.TruthDungeon, false, false, false, false);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.UpArrow) ||
                    Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.LeftArrow) ||
                    Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.RightArrow) ||
                    Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.DownArrow) ||
                    (Application.platform == RuntimePlatform.Android && !this.arrowDown && !this.arrowUp && !this.arrowLeft && !this.arrowRight))
            {
                this.detectKeyUp = true; // ただし、iOS/Androidでこれはうまくいかない。
                CancelKeyDownMovement();
            }
            else if (this.Filter.activeInHierarchy == false)
            {
                if (Input.GetKeyDown(KeyCode.F2))
                {
                    DungeonView_Click();
                }
                else if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.UpArrow) || this.arrowUp)
                {
                    this.keyUp = true;
                    this.keyDown = false;
                    movementTimer_Tick();
                }
                else if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.DownArrow) || this.arrowDown)
                {
                    this.keyDown = true;
                    this.keyUp = false;
                    movementTimer_Tick();
                }
                if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.LeftArrow) || this.arrowLeft)
                {
                    this.keyLeft = true;
                    this.keyRight = false;
                    movementTimer_Tick();
                }
                if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.RightArrow) || this.arrowRight)
                {
                    this.keyRight = true;
                    this.keyLeft = false;
                    movementTimer_Tick();
                }
            }
            else if (this.btnOK.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    tapOK();
                }
            }
        }

        private void UpdateFloor2TruthAnswerStatus(ref string flag)
        {
            if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Top)
            {
                flag = GroundOne.Decision2_TopText;
            }
            else if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Left)
            {
                flag = GroundOne.Decision2_LeftText;
            }
            else if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Right)
            {
                flag = GroundOne.Decision2_RightText;
            }
            else if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Bottom)
            {
                flag = GroundOne.Decision2_BottomText;
            }
        }

        public override void SceneBack()
        {
            base.SceneBack();

            mainMessage.text = "";
            UpdateMainMessage("", true);
            SetupPlayerStatus(false);

            #region "小画面表示後の戻りイベント"
            if (this.nowDecisionFloor1OpenDoor)
            {
                this.nowDecisionFloor1OpenDoor = false;
                if (GroundOne.DecisionChoice == 1)
                {
                    MessagePack.Message10050_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else
                {
                    MessagePack.Message10050_3(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                return;
            }
            else if (this.nowDecisionFloor2OpenDoor)
            {
                this.nowDecisionFloor2OpenDoor = false;
                if (GroundOne.DecisionChoice == 1)
                {
                    MessagePack.Message12066_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else
                {
                    MessagePack.Message12066_3(ref nowMessage, ref nowEvent);
                    tapOK();
                }
            }
            else if (this.nowDecisionFloor3OpenDoor)
            {
                this.nowDecisionFloor3OpenDoor = false;
                if (GroundOne.DecisionChoice == 1)
                {
                    MessagePack.Message13121_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else
                {
                    MessagePack.Message13121_3(ref nowMessage, ref nowEvent);
                    tapOK();
                }
            }
            else if (this.nowIntelligenceRoom == 1)
            {
                this.nowIntelligenceRoom = 0;
                if (GroundOne.InputValue == 12569)
                {
                    GroundOne.WE.dungeonEvent216 = true;
                }
                else
                {
                    GroundOne.WE.dungeonEvent216 = false;
                }
                MessagePack.Message12008_4(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (this.nowIntelligenceRoom == 2)
            {
                this.nowIntelligenceRoom = 0;
                if (GroundOne.InputValue == 847)
                {
                    GroundOne.WE.dungeonEvent217 = true;
                }
                else
                {
                    GroundOne.WE.dungeonEvent217 = false;
                }
                MessagePack.Message12008_4(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (this.nowIntelligenceRoom == 3)
            {
                this.nowIntelligenceRoom = 0;
                if (GroundOne.InputValue == 30)
                {
                    GroundOne.WE.dungeonEvent218 = true;
                }
                else
                {
                    GroundOne.WE.dungeonEvent218 = false;
                }
                MessagePack.Message12008_4(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (this.nowIntelligenceRoomGodSequence)
            {
                this.nowIntelligenceRoomGodSequence = false;

                if (!GroundOne.GodSeuqence)
                {
                    MessagePack.Message12004_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else
                {
                    MessagePack.Message12004_Success(ref nowMessage, ref nowEvent);
                    tapOK();
                }
            }
            else if (this.nowMirrorRoomGodSequence)
            {
                this.nowMirrorRoomGodSequence = false;

                if (!GroundOne.GodSeuqence)
                {
                    MessagePack.Message13119_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else
                {
                    MessagePack.Message13119_Success(ref nowMessage, ref nowEvent);
                    tapOK();
                }
            }
            else if (this.nowDecisionFloor2EightAnswer == 1)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_1_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Top)
                {
                    GroundOne.WE2.TruthAnswer2_1 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_1 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 2)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_2_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Right)
                {
                    GroundOne.WE2.TruthAnswer2_2 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_2 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 3)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_3_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Left)
                {
                    GroundOne.WE2.TruthAnswer2_3 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_3 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 4)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_4_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Bottom)
                {
                    GroundOne.WE2.TruthAnswer2_4 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_4 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 5)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_5_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Top)
                {
                    GroundOne.WE2.TruthAnswer2_5 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_5 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, true);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 6)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_6_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Left)
                {
                    GroundOne.WE2.TruthAnswer2_6 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_6 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, true);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 7)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_7_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Right)
                {
                    GroundOne.WE2.TruthAnswer2_7 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_7 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 8)
            {
                this.nowDecisionFloor2EightAnswer = 0;

                string temp = string.Empty;
                UpdateFloor2TruthAnswerStatus(ref temp);
                GroundOne.WE2.TruthAnswer2_8_current = temp;

                if (GroundOne.Decision2_Answer == TruthDecision2.AnswerType.Bottom)
                {
                    GroundOne.WE2.TruthAnswer2_8 = true;
                }
                else
                {
                    GroundOne.WE2.TruthAnswer2_8 = false;
                }
                MessagePack.Message12063(ref nowMessage, ref nowEvent, false);
                tapOK();
            }
            else if (this.nowDecisionFloor2EightAnswer == 9)
            {
                Debug.Log("GroundOne.PermutationAnswer: " + GroundOne.PermutationAnswer);
                this.nowDecisionFloor2EightAnswer = 0;
                if (GroundOne.PermutationAnswer)
                {
                    this.failCounter = 0;
                    int tilenum = Method.GetTileNumber(Player.transform.position);
                    int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
                    int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;
                    bool fromStrengthRoom = false;

                    // 力の部屋、複合レバー３－１
                    // 力の部屋、複合レバー３－２
                    if ((row == 26 && column == 13) ||
                        (row == 26 && column == 15))
                    {
                        fromStrengthRoom = true;
                    }
                    MessagePack.Message12064_Success(ref nowMessage, ref nowEvent, fromStrengthRoom);
                    tapOK();
                }
                else
                {
                    this.failCounter++;
                    MessagePack.Message12064_Fail(ref nowMessage, ref nowEvent, failCounter);
                    tapOK();
                }
            }
            else if (this.nowSelectCharacter)
            {
                this.nowSelectCharacter = false;
                MessagePack.Message15007_2(ref nowMessage, ref nowEvent);
                tapOK();
            }
            #endregion
        }

        private void ShownEvent()
        {
            Debug.Log("ShownEvent (S): ");
            int tilenum = Method.GetTileNumber(Player.transform.position);
            int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
            int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;

            if (GroundOne.GotoDownstair)
            {
                GroundOne.GotoDownstair = false;
                if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.TruthCommunicationCompArea1)
                {
                    MessagePack.Message10051_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.TruthCommunicationCompArea2)
                {
                    MessagePack.Message12067_2(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
                else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.TruthCommunicationCompArea3)
                {
                    MessagePack.Message13122_3(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }
            }

            Debug.Log("StartSeeker: " + GroundOne.WE2.StartSeeker);
            if (GroundOne.WE2.StartSeeker)
            {
                if (!GroundOne.WE2.SeekerEvent1)
                {
                    GroundOne.WE2.SeekerEvent1 = true;
                    MessagePack.Message14143(ref this.nowMessage, ref this.nowEvent);
                    tapOK();
                }

                //　パーティ編成：アイン・ウォーレンス単独　　　　：ホームタウンで済
                //  閉ざされし【終わりの部屋】への到達　　　　　　：１階
                //　定められし【神々の試練】、＜迂回＞を選択　　　：２階
                //　【海と大地、そして天空】　完全詠唱　　　　　　：２階
                //　【愚者】と【賢者】の究極２択、　<破棄>を選択　：３階
                //　【虚無の鏡】、　<原点解>へと到達　　　　　　　：３階
                //　第四階層、無間地獄を踏破　　　　　　　　　　　：４階
                //　神剣フェルトゥーシュの入手　　　　　　　　　　：４階
                //　第四階層、第三の像を検知　　　　　　　　　　　：４階
                //　偶像の崩壊、そして真実世界へ　　　　　　　　　：最後
                #region "１階スタート"
                else if (!GroundOne.WE2.SeekerEvent701 && row == 14 && column == 39 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16000(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "終わりの部屋"
                else if (!GroundOne.WE2.SeekerEvent702 && row == 29 && column == 47 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16001(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "１階、警告看板"
                else if (!GroundOne.WE2.SeekerEvent703 && row == 3 && column == 37 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16002(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "１階、最後の扉通過"
                else if (!GroundOne.WE2.SeekerEvent704 && row == 6 && column == 5 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16003(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "１階ボス撃破"
                else if (!GroundOne.WE2.SeekerEvent705 && row == 4 && column == 11 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16004(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "１階から２階へ"
                else if (!GroundOne.WE2.SeekerEvent706 && row == 6 && column == 17 && GroundOne.WE2.RealDungeonArea == 1)
                {
                    MessagePack.Message16005(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "２階スタート"
                else if (!GroundOne.WE2.SeekerEvent801 && row == 19 && column == 29 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16006(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent802 && row == 19 && column == 25 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16006_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent803 && row == 15 && column == 29 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16006_3(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent804 && row == 19 && column == 33 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16006_4(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent805 && row == 23 && column == 29 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16006_5(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "知の部屋【絶対試練】"
                else if (!GroundOne.WE2.SeekerEvent806 && row == 8 && column == 49 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16007(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "【知】の部屋で、【心】関連の看板"
                else if (!GroundOne.WE2.SeekerEvent807 && row == 5 && column == 51 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16008(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "複合レバー【知】の部屋で、【心】関連"
                else if (!GroundOne.WE2.SeekerEvent808 && row == 4 && column == 51 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16009(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent809 && row == 6 && column == 51 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16009_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "【技】の部屋で、【知】関連の看板"
                else if (!GroundOne.WE2.SeekerEvent810 && row == 37 && column == 30 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16010(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "複合レバー【技】の部屋で、【知】関連"
                else if (!GroundOne.WE2.SeekerEvent811 && row == 37 && column == 29 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16011(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent812 && row == 37 && column == 31 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16011_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "【心】の部屋で、【力】関連の看板"
                else if (!GroundOne.WE2.SeekerEvent813 && row == 6 && column == 30 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16012(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "複合レバー【心】の部屋で、【力】関連"
                else if (!GroundOne.WE2.SeekerEvent814 && row == 5 && column == 30 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16013(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent815 && row == 7 && column == 30 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16013_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "ボス"
                else if (!GroundOne.WE2.SeekerEvent816 && row == 36 && column == 14 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16014(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "【力】の部屋で、【技】関連の看板"
                else if (!GroundOne.WE2.SeekerEvent817 && row == 26 && column == 14 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16015(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "複合レバー【力】の部屋で、【技】関連"
                else if (!GroundOne.WE2.SeekerEvent818 && row == 26 && column == 13 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16016(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent819 && row == 26 && column == 15 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16016_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "２階、回想録"
                else if (!GroundOne.WE2.SeekerEvent820 && row == 26 && column == 10 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16017(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "２階、カールハンツと会話／戦闘"
                else if (!GroundOne.WE2.SeekerEvent821 && row == 26 && column == 16 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16018(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "２階、カールハンツ会話後、戦闘敗北後の自由行動制限"
                else if (GroundOne.WE2.SeekerEvent821_fail && !GroundOne.WE2.SeekerEvent821)
                {
                    if (row == 28 && column == 14 && GroundOne.WE2.RealDungeonArea == 2)
                    {
                        MessagePack.Message16019(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                    else if (row == 26 && column == 12 && GroundOne.WE2.RealDungeonArea == 2)
                    {
                        MessagePack.Message16019_2(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                }
                #endregion
                #region "２階から３階へ"
                else if (row == 26 && column == 17 && GroundOne.WE2.RealDungeonArea == 2)
                {
                    MessagePack.Message16020(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "３階スタート"
                else if (!GroundOne.WE2.SeekerEvent901 && row == 19 && column == 0 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16021(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "誘導看板"
                else if ((!GroundOne.WE2.SeekerEvent902 && row == 19 && column == 9 && GroundOne.WE2.RealDungeonArea == 3) ||
                         (!GroundOne.WE2.SeekerEvent902 && row == 20 && column == 9 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16022(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡エリア１コンプリート"
                else if (!GroundOne.WE2.SeekerEvent903 && row == 35 && column == 1 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16023(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡エリア２開始の分析"
                else if (!GroundOne.WE2.SeekerEvent904 && row == 16 && column == 24 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16024(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡エリア２の進行"
                else if (!GroundOne.WE2.SeekerEvent905 && row == 16 && column == 24 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16025(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "真実の回想録3_2"
                else if (!GroundOne.WE2.SeekerEvent911 && row == 25 && column == 1 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16026(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "正解への到達時"
                else if (!GroundOne.WE2.SeekerEvent912 && row == 39 && column == 54 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16027(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "原点解到達までのあらすじ"
                else if (!GroundOne.WE2.SeekerEvent913 && row == 16 && column == 24 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16028(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "原点解への道と、真実の回想録3_3"
                else if (!GroundOne.WE2.SeekerEvent914 && row == 14 && column == 1 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16029(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "原点解到達"
                else if (!GroundOne.WE2.SeekerEvent915 && row == 12 && column == 37 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16030(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "原点解の看板Phase1"
                else if (!GroundOne.WE2.SeekerEvent916 && row == 7 && column == 37 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16031(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                // この後は、プレイヤーに操作させること
                #region "原点解の看板Phase2"
                else if (row == 7 && column == 37 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16032(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "聖者の選択"
                else if (!GroundOne.WE2.SeekerEvent917 && row == 5 && column == 36 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16033(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "愚者の選択"
                else if (!GroundOne.WE2.SeekerEvent918 && row == 5 && column == 38 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16034(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "戻るを選択"
                else if (!GroundOne.WE2.SeekerEvent919 && row == 12 && column == 37 && GroundOne.WE2.RealDungeonArea == 3 && GroundOne.WE2.SeekerEvent906)
                {
                    MessagePack.Message16035(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "ボス撃破後の無限回廊看板"
                else if (!GroundOne.WE2.SeekerEvent920 && row == 39 && column == 54 && GroundOne.WE2.RealDungeonArea == 3 && GroundOne.WE2.SeekerEvent909)
                {
                    MessagePack.Message16036(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "３Fハウリングシーザー撃破の破、自由行動制限"
                else if (GroundOne.WE2.SeekerEvent920 && row == 39 && column == 54)
                {
                    MessagePack.Message16037(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "無限回廊前の看板"
                else if (GroundOne.WE2.SeekerEvent920 && row == 39 && column == 56)
                {
                    MessagePack.Message16038(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "原点解入手後の正解階段ブロック"
                else if (GroundOne.WE2.SeekerEvent920 && row == 39 && column == 59)
                {
                    MessagePack.Message16039(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "無限回廊"
                else if (!GroundOne.WE2.SeekerEvent921 && row == 35 && column == 55 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 35 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 35 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 35 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 35 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 32 && column == 57 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if((!GroundOne.WE2.SeekerEvent921 && row == 32 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 32 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 32 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 32 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 29 && column == 58 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_3(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if((!GroundOne.WE2.SeekerEvent921 && row == 29 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 29 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 29 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 29 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 26 && column == 59 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_4(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if((!GroundOne.WE2.SeekerEvent921 && row == 26 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 26 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 26 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 26 && column == 58 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 23 && column == 58 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_5(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 23 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 23 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 23 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 23 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 20 && column == 56 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_6(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 20 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 20 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 20 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 20 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 17 && column == 57 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_7(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 17 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 17 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 17 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 17 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 14 && column == 55 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_8(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 14 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 14 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 14 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 14 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 11 && column == 58 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_9(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 11 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 11 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 11 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 11 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 8 && column == 56 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_10(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 8 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 8 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 8 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 8 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (!GroundOne.WE2.SeekerEvent921 && row == 5 && column == 59 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16040_11(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 5 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 5 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 5 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 5 && column == 58 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if ((!GroundOne.WE2.SeekerEvent921 && row == 2 && column == 55 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 2 && column == 56 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 2 && column == 57 && GroundOne.WE2.RealDungeonArea == 3) ||
                        (!GroundOne.WE2.SeekerEvent921 && row == 2 && column == 58 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16040_False(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "無限回廊から真実の回想録へ抜ける"
                else if (row == 2 && column == 59 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16041(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "真実の回想録3_4"
                else if (!GroundOne.WE2.SeekerEvent922 && row == 4 && column == 1 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16042(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "真実の回想録から右上最後の看板へ"
                else if (row == 8 && column == 1 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16043(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "最後の看板"
                else if (row == 1 && column == 55 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16044(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "３階、オル・ランディスと会話／戦闘"
                else if ((!GroundOne.WE2.SeekerEvent924 && row == 0 && column == 58 && GroundOne.WE2.RealDungeonArea == 3) ||
                         (!GroundOne.WE2.SeekerEvent924 && row == 1 && column == 59 && GroundOne.WE2.RealDungeonArea == 3))
                {
                    MessagePack.Message16045(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "３階から４階へ"
                else if (row == 0 && column == 59 && GroundOne.WE2.RealDungeonArea == 3)
                {
                    MessagePack.Message16046(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "無間地獄の看板"
                else if (row == 28 && column == 27 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16047(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "登り階段をブロック"
                else if (row == 28 && column == 31 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16048(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "宝箱１"
                else if (!GroundOne.WE2.SeekerEvent1002 && row == 25 && column == 27 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1002 = GetTreasure(Database.COMMON_GREEN_CRYSTAL);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱２"
                else if (!GroundOne.WE2.SeekerEvent1003 && row == 24 && column == 27 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1003 = GetTreasure(Database.RARE_MEEK_HIDENSYO);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱３"
                else if (!GroundOne.WE2.SeekerEvent1004 && row == 31 && column == 26 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1004 = GetTreasure(Database.COMMON_PLATINUM_RING_1);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱４"
                else if (!GroundOne.WE2.SeekerEvent1005 && row == 30 && column == 43 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1005 = GetTreasure(Database.POOR_MIGAWARI_DOOL);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱５"
                else if (!GroundOne.WE2.SeekerEvent1006 && row == 32 && column == 27 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1006 = GetTreasure(Database.COMMON_RED_CRYSTAL);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱６"
                else if (!GroundOne.WE2.SeekerEvent1007 && row == 28 && column == 44 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1007 = GetTreasure(Database.RARE_SHINING_AETHER);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱７"
                else if (!GroundOne.WE2.SeekerEvent1008 && row == 27 && column == 31 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1008 = GetTreasure(Database.EPIC_OVER_SHIFTING);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "宝箱８"
                else if (!GroundOne.WE2.SeekerEvent1009 && row == 28 && column == 38 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    GroundOne.WE2.SeekerEvent1009 = GetTreasure(Database.EPIC_GOLD_POTION);
                    UpdateFieldElement(this.Player.transform.position);
                }
                #endregion
                #region "鏡１"
                else if (row == 28 && column == 23 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16049(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡２"
                else if (row == 23 && column == 43 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16050(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡３"
                else if (row == 22 && column == 28 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16051(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡４"
                else if (row == 32 && column == 39 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16052(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡５"
                else if (row == 22 && column == 22 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16053(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡６"
                else if (row == 20 && column == 32 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16054(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡７"
                else if (row == 29 && column == 21 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16055(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡８"
                else if (row == 26 && column == 43 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16056(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡９"
                else if (row == 25 && column == 26 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16057(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１０"
                else if (row == 32 && column == 23 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16058(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１１"
                else if (row == 22 && column == 32 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16059(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１２"
                else if (row == 31 && column == 44 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16060(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１３"
                else if (row == 31 && column == 28 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16061(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１４"
                else if (row == 25 && column == 34 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16062(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "鏡１５"
                else if (row == 28 && column == 32 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16063(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "失敗Ｘ"
                else if ((row == 25 && column == 22 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 27 && column == 23 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 29 && column == 23 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 22 && column == 26 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 31 && column == 27 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 23 && column == 31 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 29 && column == 31 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 32 && column == 31 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 21 && column == 35 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 24 && column == 35 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 30 && column == 35 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 30 && column == 39 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 20 && column == 40 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 33 && column == 40 && GroundOne.WE2.RealDungeonArea == 4) ||
                         (row == 22 && column == 44 && GroundOne.WE2.RealDungeonArea == 4))
                {
                    MessagePack.Message16064(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (row == 26 && column == 32 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16064_2(ref nowMessage, ref nowEvent);
                }
                #endregion
                #region "鏡１６"
                else if (row == 26 && column == 34 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16065(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "ボス前の扉"
                else if (!GroundOne.WE2.SeekerEvent1011 && row == 19 && column == 52 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16066(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                else if (row == 19 && column == 53 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16066_2(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "フェルトゥーシュ入手＋第三偶像を破壊"
                else if (!GroundOne.WE2.SeekerEvent1012 && row == 19 && column == 59 && GroundOne.WE2.RealDungeonArea == 4)
                {
                    MessagePack.Message16067(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "最終戦の前で引き返す事をさせないための制御"
                else if (row == 16 && column == 30 && GroundOne.WE2.RealDungeonArea == 5)
                {
                    MessagePack.Message16068(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "回復の泉"
                else if (row == 14 && column == 31 && GroundOne.WE2.RealDungeonArea == 5)
                {
                    MessagePack.Message16069(ref nowMessage, ref nowEvent);
                    tapOK();
                }
                #endregion
                #region "真実世界"
                else if (row == 9 && ((column == 29) || (column == 30) || (column == 31)) && GroundOne.WE2.RealDungeonArea == 5)
                {
                    if (column == 29)
                    {
                        UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                        UpdateUnknownTile();
                    }
                    if (column == 31)
                    {
                        UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                        UpdateUnknownTile();
                    }

                    #region "ヴェルゼ最終戦１"
                    if (!GroundOne.WE2.SeekerEvent1101)
                    {
                        MessagePack.Message16070(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                    #endregion
                    #region "ヴェルゼ最終戦２【原罪】"
                    else if (!GroundOne.WE2.SeekerEvent1102)
                    {
                        MessagePack.Message16071(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                    #endregion
                    #region "ヴェルゼ戦闘終了後"
                    else if (!GroundOne.WE2.SeekerEvent1103)
                    {
                        MessagePack.Message16072(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                }
                #endregion
                #endregion
                #region "エンディングへ"
                else if (row == 0 && column == 30 && GroundOne.WE2.RealDungeonArea == 5)
                {
                    if (!GroundOne.WE2.SeekerEvent1104)
                    {
                        MessagePack.Message16073(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                    else
                    {
                        MessagePack.Message16074(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                }
                #endregion
            }
        }

        private void movementTimer_Tick()
        {
            if (this.interval < this.MovementInterval) { this.interval++; return; }
            else { this.interval = 0; }

            if (this.keyUp)
            {
                UpdatePlayersKeyEvents(0);
            }
            else if (this.keyRight)
            {
                UpdatePlayersKeyEvents(2);
            }
            else if (this.keyDown)
            {
                UpdatePlayersKeyEvents(3);
            }
            else if (this.keyLeft)
            {
                UpdatePlayersKeyEvents(1);
            }
        }
        public void CancelKeyDownMovement()
        {
            this.arrowUp = false;
            this.arrowDown = false;
            this.arrowLeft = false;
            this.arrowRight = false;
            this.keyUp = false;
            this.keyDown = false;
            this.keyLeft = false;
            this.keyRight = false;
            this.interval = MOVE_INTERVAL;
        }

        private void UpdateViewPoint(float x, float y)
        {
            GroundOne.WE.dungeonViewPointX = (int)x;
            GroundOne.WE.dungeonViewPointY = (int)y;
            this.viewPoint = new Vector3(x, y, Camera.main.transform.position.z);
            Camera.main.transform.position = this.viewPoint;
        }

        private void UpdatePlayerLocationInfo(float x, float y)
        {
            UpdatePlayerLocationInfo(x, y, false);
        }
        private void UpdatePlayerLocationInfo(float x, float y, bool noSound)
        {
            GroundOne.WE.DungeonPosX = (int)x;
            GroundOne.WE.DungeonPosY = (int)y;
            this.Player.transform.position = new Vector3(x, y, this.Player.transform.position.z);
            if (!noSound)
            {
                GroundOne.PlaySoundEffect(Database.SOUND_FOOT_STEP);
            }
        }

        private void JumpToLocation(int X, int Y, bool noSound)
        {
            int viewX = X; if (viewX < Database.CAMERA_BORDER_X_LEFT) viewX = Database.CAMERA_BORDER_X_LEFT; if (viewX > Database.CAMERA_BORDER_X_RIGHT) viewX = Database.CAMERA_BORDER_X_RIGHT;
            int viewY = Y; if (viewY > Database.CAMERA_BORDER_Y_TOP) viewY = Database.CAMERA_BORDER_Y_TOP; if (viewY < Database.CAMERA_BORDER_Y_BOTTOM) viewY = Database.CAMERA_BORDER_Y_BOTTOM;
            JumpToLocation(X, Y, viewX, viewY, noSound);
        }
        private void JumpToLocation(int X, int Y, int viewX, int viewY, bool noSound)
        {
            UpdatePlayerLocationInfo(X, Y, noSound);
            this.viewPoint = new Vector3(viewX + Database.CAMERA_WORLD_POINT_X, viewY + Database.CAMERA_WORLD_POINT_Y, Camera.main.transform.position.z);
            UpdateViewPoint(this.viewPoint.x, this.viewPoint.y);
            //Debug.Log("p:" + this.Player.transform.position.ToString() + " v:" + this.viewPoint.ToString());
        }

        private bool CheckWall(int direction) // 0:↑ 1:← 2:→ 3:↓
        {
            //debug.text += "CheckWall(S) ";
            int tilenum = Method.GetTileNumber(Player.transform.position);
            //debug.text += "tilenum " + tilenum.ToString() + " ";
            int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
            int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;

            string[] targetTileInfo = null;
            if (GroundOne.WE.DungeonArea == 1)
            {
                targetTileInfo = tileInfo;
            }
            else if (GroundOne.WE.DungeonArea == 2)
            {
                targetTileInfo = tileInfo2;
            }
            else if (GroundOne.WE.DungeonArea == 3)
            {
                targetTileInfo = tileInfo3;
            }
            else if (GroundOne.WE.DungeonArea == 4)
            {
                targetTileInfo = tileInfo4;
            }
            else if (GroundOne.WE.DungeonArea == 5)
            {
                targetTileInfo = tileInfo5;
            }

            // プレイヤーの位置に対応しているタイル情報を取得する。
            // タイル情報にある壁情報を取得して
            // 壁情報とプレイヤー動作方向に対して壁情報が一致する場合
            string WallHitMessage = "アイン：いてぇ！";
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd) { WallHitMessage = "アイン：・・・"; }
            #region "Wall判定"
            switch (targetTileInfo[Method.GetTileNumber(Player.transform.position)])
            {
                case Database.TILEINFO_13:
                    // とくになし
                    break;
                case Database.TILEINFO_24:
                    if (direction == 0)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_16:
                    if (direction == 1)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_21:
                    if (direction == 2)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_14:
                    if (direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_20:
                    if (direction == 2)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_42:
                    if (direction == 2)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_26:
                    if (direction == 0 || direction == 1)
                    {
                        // ４階、36、34、上方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 36 && column == 34 && direction == 0)
                        {
                            break;
                        }
                        // ４階、38、31、上方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 38 && column == 31 && direction == 0)
                        {
                            break;
                        }
                        else
                        {
                            WallHit(WallHitMessage);
                            return true;
                        }
                    }
                    break;
                case Database.TILEINFO_30:
                    if (direction == 0 || direction == 2)
                    {
                        // ４階、35、29、右方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 35 && column == 29 && direction == 2)
                        {
                            break;
                        }
                        else
                        {
                            WallHit(WallHitMessage);
                            return true;
                        }
                    }
                    break;
                case Database.TILEINFO_25:
                    if (direction == 0 || direction == 3)
                    {
                        // ４階、39、40、上方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 39 && column == 40 && direction == 0)
                        {
                            break;
                        }
                        // ４階、28、55、下方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 28 && column == 55 && direction == 3)
                        {
                            break;
                        }
                        else
                        {
                            WallHit(WallHitMessage);
                            return true;
                        }
                    }
                    break;
                case Database.TILEINFO_18:
                    if (direction == 1 || direction == 2)
                    {
                        // ４階、31、52、左方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 31 && column == 52 && direction == 1)
                        {
                            break;
                        }
                        // ４階、36、59、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 36 && column == 59 && direction == 1)
                        {
                            break;
                        }
                        // ４階、29、59、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 29 && column == 59 && direction == 1)
                        {
                            break;
                        }
                        // ４階、23、59、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 23 && column == 59 && direction == 1)
                        {
                            break;
                        }
                        else
                        {
                            WallHit(WallHitMessage);
                            return true;
                        }
                    }
                    break;
                case Database.TILEINFO_17:
                    if (direction == 1 || direction == 3)
                    {
                        // ４階、39、40、左方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 38 && column == 39 && direction == 1)
                        {
                            break;
                        }
                        // ４階、34、55、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 34 && column == 55 && direction == 1)
                        {
                            break;
                        }
                        // ４階、25、54、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 25 && column == 54 && direction == 1)
                        {
                            break;
                        }
                        // ４階、25、57、左方向を無視
                        else if (GroundOne.WE.DungeonArea == 4 && row == 25 && column == 57 && direction == 1)
                        {
                            break;
                        }
                        else
                        {
                            WallHit(WallHitMessage);
                            return true;
                        }
                    }
                    break;
                case Database.TILEINFO_22:
                    if (direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_28:
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_27:
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_31:
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_19:
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_29:
                    if (direction == 0 || direction == 1 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_6:
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_7:
                    if (direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_8:
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_5:
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_3:
                    if (direction == 0)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_2:
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_4:
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_32:
                    if (direction == 0)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_33:
                    if (direction == 1)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_34:
                    // とくになし
                    break;
                case Database.TILEINFO_35:
                    if (direction == 1 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_36:
                    // とくになし
                    break;
                case Database.TILEINFO_37:
                    if (direction == 2 || direction == 3)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_38:
                    if (direction == 0)
                    {
                        WallHit(WallHitMessage);
                        return true;
                    }
                    break;
                case Database.TILEINFO_39:
                    // とくになし
                    break;
            }
            #endregion
            UpdateMainMessage("", true, true);
            return false;
        }

        private void WallHit(string message)
        {
            CancelKeyDownMovement();
            GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
            this.mainMessage.text = message;
        }

        private bool CheckBlueWall(int direction) // 0:↑ 1:← 2:→ 3:↓
        {
            // プレイヤーの位置に対応している青壁情報を取得する。
            // 青壁情報を取得して、プレイヤー動作方向に対して青壁情報が一致する場合
            if (blueWallBottom[Method.GetTileNumber(Player.transform.position)])
            {
                if (direction == 3)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallLeft[Method.GetTileNumber(Player.transform.position)])
            {
                if (direction == 1)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallRight[Method.GetTileNumber(Player.transform.position)])
            {
                if (direction == 2)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallTop[Method.GetTileNumber(Player.transform.position)])
            {
                if (direction == 0)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
                    CancelKeyDownMovement();
                    return true;
                }
            }

            UpdateMainMessage("", true, true);
            return false;
        }
        private void UpdatePlayersKeyEvents(int direction)
        {
            mainMessage.text = "";
            // 通常動作モード
            if (!this.DungeonViewMode)
            {
                int moveX = 0;
                int moveY = 0;

                //    // [警告]：開発途中で戦闘終了後、イベント発生後などでキーダウンで効かない場合があった。押下しっぱなしだと進められるように仕様変更となるので、別の不具合が出た場合はまた再検討してください。
                //    // [警告]：後編でキーダウン動作仕様を変更した。戦闘エンカウントやメッセージ表示、ホームタウン戻り、壁当たりなど随所にCancelKeyDownMovementを導入して検討中。
                //    //keyDown = true;
                if (CheckWall(direction))
                {
                    keyDown = false;
                    keyUp = false;
                    keyLeft = false;
                    keyRight = false;
                    System.Threading.Thread.Sleep(200);
                    //debug.text += "check wall end.";
                    return;
                }

                if (CheckBlueWall(direction))
                {
                    System.Threading.Thread.Sleep(200);
                    return;
                }

                if (direction == 0) moveY = Database.DUNGEON_MOVE_LEN; // change unity
                else if (direction == 1) moveX = -Database.DUNGEON_MOVE_LEN;
                else if (direction == 2) moveX = Database.DUNGEON_MOVE_LEN;
                else if (direction == 3) moveY = -Database.DUNGEON_MOVE_LEN; // change unity

                JumpToLocation((int)this.Player.transform.position.x + moveX, (int)this.Player.transform.position.y + moveY, false);

                // EPICアイテムEPIC_ORB_GROW_GREENの効果
                for (int ii = 0; ii < 3; ii++)
                {
                    MainCharacter player = null;
                    Image targetLabel = null;
                    Text targetText = null;
                    if (ii == 0) { player = GroundOne.MC; targetLabel = currentSkillPoint1; targetText = currentSkillValue1; }
                    else if (ii == 1) { player = GroundOne.SC; targetLabel = currentSkillPoint2; targetText = currentSkillValue2; }
                    else if (ii == 2) { player = GroundOne.TC; targetLabel = currentSkillPoint3; targetText = currentSkillValue3; }
                    if (player != null &&
                        player.Accessory != null &&
                        player.Accessory.Name == Database.EPIC_ORB_GROW_GREEN)
                    {
                        player.CurrentSkillPoint++;
                        UpdateSkill(player, targetLabel, targetText);
                    }
                    if (player != null &&
                        player.Accessory2 != null &&
                        player.Accessory2.Name == Database.EPIC_ORB_GROW_GREEN)
                    {
                        player.CurrentSkillPoint++;
                        UpdateSkill(player, targetLabel, targetText);
                    }
                }

                // 移動時のタイル更新
                bool lowSpeed = UpdateUnknownTile();

                // イベント発生
                SearchSomeEvents();

                if (lowSpeed)
                {
                    this.MovementInterval = MOVE_INTERVAL;
                }
                else
                {
                    this.MovementInterval = MOVE_INTERVAL / 2;
                }
                Method.GetTileNumber(Player.transform.position);
            }
            // View動作モード
            else
            {
                int moveX = 0;
                int moveY = 0;

                if (direction == 0) moveY = Database.DUNGEON_MOVE_LEN;
                else if (direction == 1) moveX = -Database.DUNGEON_MOVE_LEN;
                else if (direction == 2) moveX = Database.DUNGEON_MOVE_LEN;
                else if (direction == 3) moveY = -Database.DUNGEON_MOVE_LEN;

                // 上端ダンジョン外を見せないようにする
                // 左端ダンジョン外を見せないようにする
                // 右端ダンジョン外を見せないようにする
                // 下端ダンジョン外を見せないようにする
                if ((direction == 0 && this.viewPoint.y >= Database.CAMERA_BORDER_Y_TOP + Database.CAMERA_WORLD_POINT_Y) ||
                    (direction == 1 && this.viewPoint.x <= Database.CAMERA_BORDER_X_LEFT + Database.CAMERA_WORLD_POINT_X) ||
                    (direction == 2 && this.viewPoint.x >= Database.CAMERA_BORDER_X_RIGHT + Database.CAMERA_WORLD_POINT_X) ||
                    (direction == 3 && this.viewPoint.y <= Database.CAMERA_BORDER_Y_BOTTOM + Database.CAMERA_WORLD_POINT_Y)
                    )
                {
                    return;
                }

                UpdateViewPoint(GroundOne.WE.dungeonViewPointX + moveX, GroundOne.WE.dungeonViewPointY + moveY);
            }
        }

        private void AutoMove(int direction)
        {
            int moveX = 0;
            int moveY = 0;

            if (direction == 0) moveY = Database.DUNGEON_MOVE_LEN; // change unity
            else if (direction == 1) moveX = -Database.DUNGEON_MOVE_LEN;
            else if (direction == 2) moveX = Database.DUNGEON_MOVE_LEN;
            else if (direction == 3) moveY = -Database.DUNGEON_MOVE_LEN; // change unity
            JumpToLocation((int)this.Player.transform.position.x + moveX, (int)this.Player.transform.position.y + moveY, false);
            UpdateUnknownTile();
        }

        private void SearchSomeEvents()
        {
            bool detectEvent = false;
            //using (OKRequest ok = new OKRequest())
            {
                //ok.StartPosition = FormStartPosition.Manual;
                //ok.Location = new Point(this.transform.position.x + 904, this.transform.position.y + 708);

                #region "１階"
                if (GroundOne.WE.DungeonArea == 1 && GroundOne.WE2.StartSeeker == false)
                {
                    for (int ii = 0; ii < 60; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            ExecSomeEvents(1, ii);
                            return;
                        }
                    }
                }
                #endregion
                #region "２階"
                else if (GroundOne.WE.DungeonArea == 2 && GroundOne.WE2.StartSeeker == false)
                {
                    for (int ii = 0; ii < 200; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            ExecSomeEvents(2, ii);
                            return;
                        }
                    }
                }
                #endregion
                #region "３階"
                else if (GroundOne.WE.DungeonArea == 3 && GroundOne.WE2.StartSeeker == false)
                {
                    for (int ii = 0; ii < 500; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            ExecSomeEvents(3, ii);
                            return;
                        }
                    }
                }
                #endregion
                #region "４階"
                else if (GroundOne.WE.DungeonArea == 4)
                {
                    for (int ii = 0; ii < 500; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            ExecSomeEvents(4, ii);
                            return;
                        }
                    }
                }
                #endregion
                #region "５階"
                else if (GroundOne.WE.DungeonArea == 5)
                {
                    for (int ii = 0; ii < 500; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            ExecSomeEvents(5, ii);
                            return;
                        }
                    }
                }
                #endregion
                #region "現実世界"
                if (GroundOne.WE2.StartSeeker)
                {
                    ShownEvent();
                }
                #endregion


                if (!detectEvent)
                {
                    EncountEnemy();
                }
            }
        }
        private bool UpdateUnknownTile()
        {
            bool newUpdate = false; // 新しくタイルが拓けた事を示すフラグ
            int currentPosNum = Method.GetTileNumber(this.Player.transform.position);
            string currentTileInfo = "";
            bool[] targetKnownTileInfo = null;
            if (GroundOne.WE.DungeonArea == 1)
            {
                currentTileInfo = tileInfo[currentPosNum];
                targetKnownTileInfo = GroundOne.Truth_KnownTileInfo;
            }
            else if (GroundOne.WE.DungeonArea == 2)
            {
                currentTileInfo = tileInfo2[currentPosNum];
                targetKnownTileInfo = GroundOne.Truth_KnownTileInfo2;
            }
            else if (GroundOne.WE.DungeonArea == 3)
            {
                currentTileInfo = tileInfo3[currentPosNum];
                targetKnownTileInfo = GroundOne.Truth_KnownTileInfo3;
            }
            else if (GroundOne.WE.DungeonArea == 4)
            {
                currentTileInfo = tileInfo4[currentPosNum];
                targetKnownTileInfo = GroundOne.Truth_KnownTileInfo4;
            }
            else if (GroundOne.WE.DungeonArea == 5)
            {
                currentTileInfo = tileInfo5[currentPosNum];
                targetKnownTileInfo = GroundOne.Truth_KnownTileInfo5;
            }

            if (unknownTile[currentPosNum].activeInHierarchy)
            {
                newUpdate = true;
            }
            unknownTile[currentPosNum].SetActive(false);
            targetKnownTileInfo[currentPosNum] = true;

            // 上の可視化
            if (currentPosNum >= Database.TRUTH_DUNGEON_COLUMN &&
                (currentTileInfo != Database.TILEINFO_24 &&
                 currentTileInfo != Database.TILEINFO_26 &&
                 currentTileInfo != Database.TILEINFO_30 &&
                 currentTileInfo != Database.TILEINFO_25 &&
                 currentTileInfo != Database.TILEINFO_28 &&
                 currentTileInfo != Database.TILEINFO_27 &&
                 currentTileInfo != Database.TILEINFO_31 &&
                 currentTileInfo != Database.TILEINFO_29 &&
                 currentTileInfo != Database.TILEINFO_8 &&
                 currentTileInfo != Database.TILEINFO_5 &&
                 currentTileInfo != Database.TILEINFO_4 &&
                 currentTileInfo != Database.TILEINFO_3 &&
                 currentTileInfo != Database.TILEINFO_32 &&
                 currentTileInfo != Database.TILEINFO_38 &&
                 blueWallTop[currentPosNum] == false))
            {
                if (unknownTile[currentPosNum - Database.TRUTH_DUNGEON_COLUMN].activeInHierarchy)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum - Database.TRUTH_DUNGEON_COLUMN].SetActive(false);
                targetKnownTileInfo[currentPosNum - Database.TRUTH_DUNGEON_COLUMN] = true;
            }

            // 左の可視化
            if (currentPosNum % Database.TRUTH_DUNGEON_COLUMN != 0 &&
                (currentTileInfo != Database.TILEINFO_16 &&
                 currentTileInfo != Database.TILEINFO_26 &&
                 currentTileInfo != Database.TILEINFO_18 &&
                 currentTileInfo != Database.TILEINFO_17 &&
                 currentTileInfo != Database.TILEINFO_28 &&
                 currentTileInfo != Database.TILEINFO_27 &&
                 currentTileInfo != Database.TILEINFO_19 &&
                 currentTileInfo != Database.TILEINFO_29 &&
                 currentTileInfo != Database.TILEINFO_6 &&
                 currentTileInfo != Database.TILEINFO_8 &&
                 currentTileInfo != Database.TILEINFO_4 &&
                 currentTileInfo != Database.TILEINFO_2 &&
                 currentTileInfo != Database.TILEINFO_20 &&
                 currentTileInfo != Database.TILEINFO_33 &&
                 currentTileInfo != Database.TILEINFO_35 &&
                 blueWallLeft[currentPosNum] == false))
            {
                if (unknownTile[currentPosNum - 1].activeInHierarchy)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum - 1].SetActive(false);
                targetKnownTileInfo[currentPosNum - 1] = true;
            }

            // 右の可視化
            if (currentPosNum % Database.TRUTH_DUNGEON_COLUMN != (Database.TRUTH_DUNGEON_COLUMN - 1) &&
                (currentTileInfo != Database.TILEINFO_21 &&
                 currentTileInfo != Database.TILEINFO_30 &&
                 currentTileInfo != Database.TILEINFO_18 &&
                 currentTileInfo != Database.TILEINFO_22 &&
                 currentTileInfo != Database.TILEINFO_28 &&
                 currentTileInfo != Database.TILEINFO_31 &&
                 currentTileInfo != Database.TILEINFO_19 &&
                 currentTileInfo != Database.TILEINFO_29 &&
                 currentTileInfo != Database.TILEINFO_6 &&
                 currentTileInfo != Database.TILEINFO_7 &&
                 currentTileInfo != Database.TILEINFO_8 &&
                 currentTileInfo != Database.TILEINFO_5 &&
                 currentTileInfo != Database.TILEINFO_2 &&
                 currentTileInfo != Database.TILEINFO_20 &&
                 currentTileInfo != Database.TILEINFO_23 &&
                 currentTileInfo != Database.TILEINFO_37 &&
                 blueWallRight[currentPosNum] == false))
            {
                if (unknownTile[currentPosNum + 1].activeInHierarchy)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum + 1].SetActive(false);
                targetKnownTileInfo[currentPosNum + 1] = true;
            }

            // 下の可視化
            if (currentPosNum < (Database.TRUTH_DUNGEON_COLUMN * (Database.TRUTH_DUNGEON_ROW - 1)) &&
                (currentTileInfo != Database.TILEINFO_14 &&
                 currentTileInfo != Database.TILEINFO_25 &&
                 currentTileInfo != Database.TILEINFO_17 &&
                 currentTileInfo != Database.TILEINFO_22 &&
                 currentTileInfo != Database.TILEINFO_27 &&
                 currentTileInfo != Database.TILEINFO_31 &&
                 currentTileInfo != Database.TILEINFO_19 &&
                 currentTileInfo != Database.TILEINFO_29 &&
                 currentTileInfo != Database.TILEINFO_6 &&
                 currentTileInfo != Database.TILEINFO_7 &&
                 currentTileInfo != Database.TILEINFO_5 &&
                 currentTileInfo != Database.TILEINFO_4 &&
                 currentTileInfo != Database.TILEINFO_2 &&
                 currentTileInfo != Database.TILEINFO_15 &&
                 currentTileInfo != Database.TILEINFO_42 &&
                 currentTileInfo != Database.TILEINFO_35 &&
                 currentTileInfo != Database.TILEINFO_37 &&
                 blueWallBottom[currentPosNum] == false))
            {
                if (unknownTile[currentPosNum + Database.TRUTH_DUNGEON_COLUMN].activeInHierarchy)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum + Database.TRUTH_DUNGEON_COLUMN].SetActive(false);
                targetKnownTileInfo[currentPosNum + Database.TRUTH_DUNGEON_COLUMN] = true;
            }
            //this.Update();

            return newUpdate;
        }

        private void EncountEnemy()
        {
            //return; // debug 敵を出さない状態

            if (GroundOne.WE2.SeekerEvent507)
            {
                // 最下層、パーティメンバー選定後、雑魚敵で稼ぐのを許可するため、スルー

                // 最下層、支配竜を倒した後は雑魚敵を出さない。
                if (GroundOne.WE2.SeekerEvent508)
                {
                    if (GroundOne.WE2.SeekerEvent1014)
                    {
                        // 最下層、真実世界の最後は雑魚的で稼ぐので、スルー
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (GroundOne.WE.dungeonEvent4_SlayBoss3 && !GroundOne.WE2.SeekerEvent925)
            {
                return;
            }

            System.Random rd = new System.Random(DateTime.Now.Millisecond * Environment.TickCount);
            int resultValue = rd.Next(1, 101);
            if (GroundOne.WE.CompleteSlayBoss5) resultValue = 100;

            if (labelVigilance.text == Database.TEXT_VIGILANCE_MODE)
            {
                stepCounter += 3;
            }
            else
            {
                stepCounter += 10;
            }
            int encountBorder = 0;
            if (labelVigilance.text == Database.TEXT_VIGILANCE_MODE)
            {
                if (GroundOne.WE.DungeonArea == 4)
                {
                    encountBorder = (int)(stepCounter / 20);
                }
                else
                {
                    encountBorder = (int)(stepCounter / 10);
                }
            }
            else
            {
                encountBorder = (int)(stepCounter / 5);
            }
            int lowLevelBorder = 1;
            if (GroundOne.WE2.RealWorld == false)
            {
                int[] factor = { Database.CHARACTER_MAX_LEVEL1, Database.CHARACTER_MAX_LEVEL2, Database.CHARACTER_MAX_LEVEL3, Database.CHARACTER_MAX_LEVEL4, Database.CHARACTER_MAX_LEVEL5 };
                lowLevelBorder = (factor[GroundOne.WE.DungeonArea - 1] - GroundOne.MC.Level) / 4 + 1;
            }
            else
            {
                lowLevelBorder = Database.CHARACTER_MAX_LEVEL6 - GroundOne.MC.Level + 1;
            }
            if (resultValue > encountBorder * lowLevelBorder)
            {
                return;
            }

            stepCounter = 0;
            string enemyName = "";
            string enemyName2 = "";
            string enemyName3 = "";
            string[] monsterName = null;
            string[] monsterName2 = null;
            int enemyLevel = tileColor[Method.GetTileNumber(this.Player.transform.position)];
            // １階は左上：エリア１、左下：エリア２、右上：エリア３、右下：エリア４
            if (GroundOne.WE.DungeonArea == 1)
            {
                if (enemyLevel == 1)
                {
                    monsterName = new string[6];
                    monsterName[0] = Database.ENEMY_HIYOWA_BEATLE;
                    monsterName[1] = Database.ENEMY_HENSYOKU_PLANT;
                    monsterName[2] = Database.ENEMY_GREEN_CHILD;
                    monsterName[3] = Database.ENEMY_TINY_MANTIS;
                    monsterName[4] = Database.ENEMY_KOUKAKU_WURM;
                    monsterName[5] = Database.ENEMY_MANDRAGORA;

                    if (GroundOne.MC.Level <= 1)
                    {
                        monsterName[2] = Database.ENEMY_HIYOWA_BEATLE;
                        monsterName[3] = Database.ENEMY_HENSYOKU_PLANT;
                        monsterName[4] = Database.ENEMY_HIYOWA_BEATLE;
                    }

                    if (GroundOne.MC.Level <= 5)
                    {
                        monsterName[5] = Database.ENEMY_HENSYOKU_PLANT;
                    }

                    monsterName2 = new string[6];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                    monsterName2[2] = monsterName[2];
                    monsterName2[3] = monsterName[3];
                    monsterName2[4] = monsterName[4];
                    monsterName2[5] = monsterName[5];
                }
                else if (enemyLevel == 2)
                {
                    monsterName = new string[5];
                    monsterName[0] = Database.ENEMY_SUN_FLOWER;
                    monsterName[1] = Database.ENEMY_RED_HOPPER;
                    monsterName[2] = Database.ENEMY_EARTH_SPIDER;
                    if (GroundOne.MC.Level <= 2)
                    {
                        monsterName[3] = monsterName[0];
                    }
                    else
                    {
                        monsterName[3] = Database.ENEMY_ALRAUNE;
                    }
                    if (GroundOne.MC.Level <= 4)
                    {
                        monsterName[4] = monsterName[1];
                    }
                    else
                    {
                        monsterName[4] = Database.ENEMY_POISON_MARY;
                    }
                    monsterName2 = new string[3];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                    monsterName2[2] = monsterName[2];
                }
                else if (enemyLevel == 3)
                {
                    monsterName = new string[5];
                    monsterName[0] = Database.ENEMY_SPEEDY_TAKA;
                    monsterName[1] = Database.ENEMY_ZASSYOKU_RABBIT;
                    if (GroundOne.MC.Level <= 2)
                    {
                        monsterName[2] = monsterName[0];
                    }
                    else
                    {
                        monsterName[2] = Database.ENEMY_WONDER_SEED;
                    }
                    if (GroundOne.MC.Level <= 2)
                    {
                        monsterName[3] = monsterName[1];
                    }
                    else
                    {
                        monsterName[3] = Database.ENEMY_FLANSIS_KNIGHT;
                    }
                    if (GroundOne.MC.Level <= 4)
                    {
                        monsterName[4] = monsterName[0];
                    }
                    else
                    {
                        monsterName[4] = Database.ENEMY_SHOTGUN_HYUI;
                    }
                    monsterName2 = new string[2];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                }
                else if (enemyLevel == 4)
                {
                    monsterName = new string[4];
                    monsterName[0] = Database.ENEMY_BRILLIANT_BUTTERFLY;
                    monsterName[1] = Database.ENEMY_WAR_WOLF;
                    if (GroundOne.MC.Level <= 2)
                    {
                        monsterName[2] = monsterName[0];
                    }
                    else
                    {
                        monsterName[2] = Database.ENEMY_BLOOD_MOSS;
                    }
                    if (GroundOne.MC.Level <= 4)
                    {
                        monsterName[3] = monsterName[0];
                    }
                    else
                    {
                        monsterName[3] = Database.ENEMY_MOSSGREEN_DADDY;
                    }
                    monsterName2 = new string[2];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                }
                enemyName = monsterName[AP.Math.RandomInteger(monsterName.Length)];
                enemyName2 = monsterName2[AP.Math.RandomInteger(monsterName2.Length)];
                enemyName3 = monsterName2[AP.Math.RandomInteger(monsterName2.Length)];
            }
            else if (GroundOne.WE.DungeonArea == 2)
            {
                if (enemyLevel == 1)
                {
                    monsterName = new string[4];
                    monsterName[0] = Database.ENEMY_DAGGER_FISH;
                    monsterName[1] = Database.ENEMY_SIPPU_FLYING_FISH;
                    monsterName[2] = Database.ENEMY_ORB_SHELLFISH;
                    monsterName[3] = Database.ENEMY_SPLASH_KURIONE;

                    monsterName2 = new string[3];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                    monsterName2[2] = monsterName[2];
                }
                else if (enemyLevel == 2)
                {
                    monsterName = new string[5];
                    monsterName[0] = Database.ENEMY_ROLLING_MAGURO;
                    monsterName[1] = Database.ENEMY_RANBOU_SEA_ARTINE;
                    monsterName[2] = Database.ENEMY_BLUE_SEA_WASI;
                    monsterName[3] = Database.ENEMY_GANGAME;
                    monsterName[4] = Database.ENEMY_BIGMOUSE_JOE;

                    monsterName2 = new string[3];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                    monsterName2[2] = monsterName[2];
                }
                else if (enemyLevel == 3)
                {
                    monsterName = new string[5];
                    monsterName[0] = Database.ENEMY_MOGURU_MANTA;
                    monsterName[1] = Database.ENEMY_FLOATING_GOLD_FISH;
                    monsterName[2] = Database.ENEMY_GOEI_HERMIT_CLUB;
                    monsterName[3] = Database.ENEMY_VANISHING_CORAL;
                    monsterName[4] = Database.ENEMY_CASSY_CANCER;

                    monsterName2 = new string[2];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                }
                else if (enemyLevel == 4)
                {
                    monsterName = new string[4];
                    monsterName[0] = Database.ENEMY_BLACK_STARFISH;
                    monsterName[1] = Database.ENEMY_RAINBOW_ANEMONE;
                    monsterName[2] = Database.ENEMY_EDGED_HIGH_SHARK;
                    monsterName[3] = Database.ENEMY_EIGHT_EIGHT;

                    monsterName2 = new string[2];
                    monsterName2[0] = monsterName[0];
                    monsterName2[1] = monsterName[1];
                }
                enemyName = monsterName[AP.Math.RandomInteger(monsterName.Length)];
                enemyName2 = monsterName2[AP.Math.RandomInteger(monsterName2.Length)];
                enemyName3 = monsterName2[AP.Math.RandomInteger(monsterName2.Length)];
            }
            else if (GroundOne.WE.DungeonArea == 3)
            {
                System.Random rand = new System.Random();
                if (enemyLevel == 1)
                {
                    int result = rand.Next(0, 4);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_WAR_MAMMOTH;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_SNOW_CAT;
                        enemyName2 = Database.ENEMY_SNOW_CAT;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_TOSSIN_ORC;
                        enemyName2 = Database.ENEMY_TOSSIN_ORC;
                        enemyName3 = String.Empty;
                    }
                    else
                    {
                        enemyName = Database.ENEMY_WINGED_COLD_FAIRY;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
                else if (enemyLevel == 2)
                {
                    int result = rand.Next(0, 5);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_BRUTAL_OGRE;
                        enemyName2 = Database.ENEMY_WINGED_COLD_FAIRY;
                        enemyName3 = Database.ENEMY_BRUTAL_OGRE;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_HYDRO_LIZARD;
                        enemyName2 = Database.ENEMY_HYDRO_LIZARD;
                        enemyName3 = Database.ENEMY_WINGED_COLD_FAIRY;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_PENGUIN_STAR;
                        enemyName2 = Database.ENEMY_PENGUIN_STAR;
                        enemyName3 = Database.ENEMY_PENGUIN_STAR;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_FEROCIOUS_RAGE_BEAR;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 4)
                    {
                        enemyName = Database.ENEMY_SWORD_TOOTH_TIGER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
                else if (enemyLevel == 3)
                {
                    int result = rand.Next(0, 5);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_WINTER_ORB;
                        enemyName2 = Database.ENEMY_WINGED_COLD_FAIRY;
                        enemyName3 = Database.ENEMY_WINGED_COLD_FAIRY;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI;
                        enemyName2 = Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_INTELLIGENCE_ARGONIAN;
                        enemyName2 = Database.ENEMY_PENGUIN_STAR;
                        enemyName3 = Database.ENEMY_PENGUIN_STAR;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_MAGIC_HYOU_RIFLE;
                        enemyName2 = Database.ENEMY_WINTER_ORB;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 4)
                    {
                        enemyName = Database.ENEMY_PURE_BLIZZARD_CRYSTAL;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
                else if (enemyLevel == 4)
                {
                    int result = rand.Next(0, 4);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                        enemyName2 = Database.ENEMY_FROST_HEART;
                        enemyName3 = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_FROST_HEART;
                        enemyName2 = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                        enemyName3 = Database.ENEMY_FROST_HEART;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_WIND_BREAKER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_TUNDRA_LONGHORN_DEER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
            }
            else if (GroundOne.WE.DungeonArea == 4)
            {
                System.Random rand = new System.Random();
                if (enemyLevel == 1)
                {
                    int result = rand.Next(0, 4);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_GENAN_HUNTER;
                        enemyName2 = Database.ENEMY_BEAST_MASTER;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_BEAST_MASTER;
                        enemyName2 = Database.ENEMY_ELDER_ASSASSIN;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_ELDER_ASSASSIN;
                        enemyName2 = Database.ENEMY_GENAN_HUNTER;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_FALLEN_SEEKER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
                else if (enemyLevel == 2)
                {
                    int result = rand.Next(0, 5);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_MASTER_LOAD;
                        enemyName2 = Database.ENEMY_DARK_MESSENGER;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_EXECUTIONER;
                        enemyName2 = Database.ENEMY_DARK_MESSENGER;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_EXECUTIONER;
                        enemyName2 = Database.ENEMY_MASTER_LOAD;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_BLACKFIRE_MASTER_BLADE;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 4)
                    {
                        enemyName = Database.ENEMY_SIN_THE_DARKELF;
                        enemyName2 = Database.ENEMY_EXECUTIONER;
                        enemyName3 = Database.ENEMY_DARK_MESSENGER;
                    }
                }
                else if (enemyLevel == 3)
                {
                    int result = rand.Next(0, 5);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_SUN_STRIDER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_ARC_DEMON;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_BALANCE_IDLE;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_GO_FLAME_SLASHER;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 4)
                    {
                        enemyName = Database.ENEMY_DEVIL_CHILDREN;
                        enemyName2 = Database.ENEMY_SUN_STRIDER;
                        enemyName3 = Database.ENEMY_ARC_DEMON;
                    }
                }
                else if (enemyLevel == 4)
                {
                    // エリア４でストーリー上はもう、モンスターを出せない。
                    return;
                }
                else if (enemyLevel == 6) // 現実世界の中央部
                {
                    int result = rand.Next(0, 4);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_PHOENIX;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_NINE_TAIL;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_JUDGEMENT;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_EMERALD_DRAGON;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
            }
            else if (GroundOne.WE.DungeonArea == 5)
            {
                System.Random rand = new System.Random();
                //if (enemyLevel == 6) // 現実世界の中央部
                {
                    int result = rand.Next(0, 4);
                    if (result == 0)
                    {
                        enemyName = Database.ENEMY_PHOENIX;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 1)
                    {
                        enemyName = Database.ENEMY_NINE_TAIL;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 2)
                    {
                        enemyName = Database.ENEMY_JUDGEMENT;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                    else if (result == 3)
                    {
                        enemyName = Database.ENEMY_EMERALD_DRAGON;
                        enemyName2 = String.Empty;
                        enemyName3 = String.Empty;
                    }
                }
            }

            // 敵２、敵３を生成するかどうかの判定用オブジェクトを生成
            GameObject enemyObj1 = new GameObject("enemy1");
            TruthEnemyCharacter ec1 = enemyObj1.AddComponent<TruthEnemyCharacter>();
            ec1.Initialize(enemyName);

            // 敵１はエントリー確定
            GroundOne.enemyName1 = enemyName;

            // １階初期パーティが１人の場合を考慮して以下の形式
            if ((GroundOne.WE.AvailableSecondCharacter && enemyName2 != String.Empty) &&
                (ec1.Rare == TruthEnemyCharacter.RareString.Black || ec1.Rare == TruthEnemyCharacter.RareString.Blue))
            {
                GroundOne.enemyName2 = enemyName2;
            }
            else
            {
                GroundOne.enemyName2 = String.Empty;
            }

            // ２階初期パーティが２人の場合を考慮して以下の形式
            if ((GroundOne.WE.AvailableThirdCharacter && enemyName3 != String.Empty) &&
                (ec1.Rare == TruthEnemyCharacter.RareString.Black) || (ec1.Rare == TruthEnemyCharacter.RareString.Blue))
            {
                GroundOne.enemyName3 = enemyName3;
            }
            else
            {
                GroundOne.enemyName3 = String.Empty;
            }

            // ２階、力の部屋以降、ボスが２人以上を考慮して以下の形式
            if (enemyName2 != String.Empty && ec1.Rare == TruthEnemyCharacter.RareString.Gold)
            {
                GroundOne.enemyName2 = enemyName2;
            }
            else
            {
                GroundOne.enemyName2 = String.Empty;
            }

            if (enemyName3 != String.Empty && ec1.Rare == TruthEnemyCharacter.RareString.Gold)
            {
                GroundOne.enemyName3 = enemyName3;
            }
            else
            {
                GroundOne.enemyName3 = String.Empty;
            }

            Destroy(ec1);
            
            this.nowEncountEnemy = true;
        }

        private bool CheckTriggeredEvent(int eventNum)
        {
            int tilenum = Method.GetTileNumber(Player.transform.position);
            int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
            int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;

            switch (GroundOne.WE.DungeonArea)
            {
                #region "１階"
                case 1:
                    // 看板１
                    if (row == 8 && column == 42 && eventNum == 0)
                    {
                        return true;
                    }
                    // 看板２
                    if (row == 3 && column == 37 && eventNum == 1)
                    {
                        return true;
                    }
                    // 看板３
                    if (row == 31 && column == 18 && eventNum == 2)
                    {
                        return true;
                    }
                    // 看板４
                    if (row == 21 && column == 54 && eventNum == 3)
                    {
                        return true;
                    }
                    // 看板５
                    if (row == 6 && column == 3 && eventNum == 4)
                    {
                        return true;
                    }
                    // 町への帰還
                    if (row == 14 && column == 39 && eventNum == 5)
                    {
                        return true;
                    }
                    // 宝箱１－１
                    if (row == 13 && column == 34 && !GroundOne.WE.TruthTreasure11 && eventNum == 6)
                    {
                        return true;
                    }
                    // 宝箱１－２
                    if (row == 21 && column == 53 && !GroundOne.WE.TruthTreasure12 && eventNum == 7)
                    {
                        return true;
                    }
                    // 宝箱１－３
                    if (row == 29 && column == 29 && !GroundOne.WE.TruthTreasure13 && eventNum == 8)
                    {
                        return true;
                    }
                    // 宝箱１－４
                    if (row == 8 && column == 33 && !GroundOne.WE.TruthTreasure14 && eventNum == 9)
                    {
                        return true;
                    }
                    // 宝箱１－５
                    if (row == 1 && column == 22 && !GroundOne.WE.TruthTreasure15 && eventNum == 10)
                    {
                        return true;
                    }
                    // ボス１
                    if (row == 4 && column == 11 && !GroundOne.WE.TruthCompleteSlayBoss1 && eventNum == 11)
                    {
                        return true;
                    }
                    // 大広間右下右扉
                    if (row == 33 && column == 22 && eventNum == 12)
                    {
                        return true;
                    }
                    // 大広間前、右下右の入り口
                    if (row == 33 && column == 24 && eventNum == 13)
                    {
                        return true;
                    }
                    // 大広間右下下扉
                    if (row == 34 && column == 21 && eventNum == 14)
                    {
                        return true;
                    }
                    // 大広間右下下の入り口
                    if (row == 36 && column == 21 && eventNum == 15)
                    {
                        return true;
                    }
                    // 大広間右上右扉
                    if (row == 28 && column == 22 && eventNum == 16)
                    {
                        return true;
                    }
                    // 大広間右上右の入り口
                    if (row == 28 && column == 24 && eventNum == 17)
                    {
                        return true;
                    }
                    // 大広間右上上扉
                    if (row == 27 && column == 21 && eventNum == 18)
                    {
                        return true;
                    }
                    // 大広間右上上の入り口
                    if (row == 25 && column == 21 && eventNum == 19)
                    {
                        return true;
                    }
                    // 中央一本道の扉右２
                    if (row == 10 && column == 17 && eventNum == 20)
                    {
                        return true;
                    }
                    // 中央一本道の扉右１
                    if (row == 10 && column == 16 && eventNum == 21)
                    {
                        return true;
                    }
                    // 宝箱２－１
                    if (row == 1 && column == 40 && !GroundOne.WE.TruthTreasure121 && eventNum == 22)
                    {
                        return true;
                    }
                    // 宝箱２－２
                    if (row == 1 && column == 53 && !GroundOne.WE.TruthTreasure122 && eventNum == 23)
                    {
                        return true;
                    }
                    // 宝箱２－３
                    if (row == 8 && column == 49 && !GroundOne.WE.TruthTreasure123 && eventNum == 24)
                    {
                        return true;
                    }
                    // 宝箱２－４
                    if (row == 12 && column == 24 && !GroundOne.WE.TruthTreasure124 && eventNum == 25)
                    {
                        return true;
                    }
                    // 宝箱２－５
                    if (row == 18 && column == 9 && !GroundOne.WE.TruthTreasure125 && eventNum == 26)
                    {
                        return true;
                    }
                    // 宝箱２－６
                    if (row == 20 && column == 45 && !GroundOne.WE.TruthTreasure126 && eventNum == 27)
                    {
                        return true;
                    }
                    // 宝箱２－７
                    if (row == 24 && column == 52 && !GroundOne.WE.TruthTreasure127 && eventNum == 28)
                    {
                        return true;
                    }
                    // 宝箱２－８
                    if (row == 26 && column == 52 && !GroundOne.WE.TruthTreasure128 && eventNum == 29)
                    {
                        return true;
                    }
                    // 宝箱２－９
                    if (row == 28 && column == 43 && !GroundOne.WE.TruthTreasure129 && eventNum == 30)
                    {
                        return true;
                    }
                    // 宝箱２－１０
                    if (row == 35 && column == 26 && !GroundOne.WE.TruthTreasure1210 && eventNum == 31)
                    {
                        return true;
                    }
                    // 宝箱２－１１
                    if (row == 35 && column == 34 && !GroundOne.WE.TruthTreasure1211 && eventNum == 32)
                    {
                        return true;
                    }
                    // 宝箱２－１２
                    if (row == 38 && column == 48 && !GroundOne.WE.TruthTreasure1212 && eventNum == 33)
                    {
                        return true;
                    }
                    // 宝箱３－１
                    if (row == 9 && column == 7 && !GroundOne.WE.TruthTreasure131 && eventNum == 34)
                    {
                        return true;
                    }
                    // 宝箱３－２
                    if (row == 18 && column == 1 && !GroundOne.WE.TruthTreasure132 && eventNum == 35)
                    {
                        return true;
                    }
                    // 宝箱３－３
                    if (row == 22 && column == 8 && !GroundOne.WE.TruthTreasure133 && eventNum == 36)
                    {
                        return true;
                    }
                    // 宝箱３－４
                    if (row == 36 && column == 12 && !GroundOne.WE.TruthTreasure134 && eventNum == 37)
                    {
                        return true;
                    }
                    // 宝箱４－１
                    if (row == 8 && column == 19 && !GroundOne.WE.TruthTreasure141 && eventNum == 38)
                    {
                        return true;
                    }
                    // 宝箱４－２
                    if (row == 16 && column == 8 && !GroundOne.WE.TruthTreasure142 && eventNum == 39)
                    {
                        return true;
                    }
                    // 中央一本道下　右２
                    if (row == 16 && column == 14 && eventNum == 40)
                    {
                        return true;
                    }
                    // 中央一本道下　右１
                    if (row == 16 && column == 13 && eventNum == 41)
                    {
                        return true;
                    }
                    // 小広間左下扉
                    if (row == 12 && column == 2 && eventNum == 42)
                    {
                        return true;
                    }
                    // 小広間左下入り口
                    if (row == 14 && column == 2 && eventNum == 43)
                    {
                        return true;
                    }
                    // 小広間下右扉
                    if (row == 12 && column == 4 && eventNum == 44)
                    {
                        return true;
                    }
                    // 小広間下右入り口
                    if (row == 14 && column == 4 && eventNum == 45)
                    {
                        return true;
                    }
                    // 小広間右下扉
                    if (row == 11 && column == 5 && eventNum == 46)
                    {
                        return true;
                    }
                    // 小広間右下入り口
                    if (row == 11 && column == 7 && eventNum == 47)
                    {
                        return true;
                    }
                    // 小広間右上扉
                    if (row == 2 && column == 5 && eventNum == 48)
                    {
                        return true;
                    }
                    // 小広間右上入り口
                    if (row == 2 && column == 7 && eventNum == 49)
                    {
                        return true;
                    }
                    // 小広間左上扉
                    if (row == 6 && column == 5 && eventNum == 50)
                    {
                        return true;
                    }
                    // ２階への階段
                    if (row == 6 && column == 17 && eventNum == 51)
                    {
                        return true;
                    }
                    // 大広間左上上扉
                    if (row == 27 && column == 14 && eventNum == 52)
                    {
                        return true;
                    }
                    // 大広間左上左扉
                    if (row == 28 && column == 13 && eventNum == 53)
                    {
                        return true;
                    }
                    // 大広間左下左扉
                    if (row == 33 && column == 13 && eventNum == 54)
                    {
                        return true;
                    }
                    // 大広間左下下扉
                    if (row == 34 && column == 14 && eventNum == 55)
                    {
                        return true;
                    }
                    // 真実解入り口
                    if (row == 29 && column == 50 && eventNum == 56)
                    {
                        return true;
                    }
                    // 真実解のイベント
                    if (row == 29 && column == 47 && eventNum == 57)
                    {
                        return true;
                    }
                    break;

                #endregion
                #region "２階"
                case 2:
                    // 上り階段
                    if (row == 19 && column == 29 && eventNum == 0)
                    {
                        return true;
                    }
                    // 中央４看板１
                    else if (row == 19 && column == 33 && eventNum == 1)
                    {
                        return true;
                    }
                    // 中央４看板２
                    else if (row == 23 && column == 29 && eventNum == 2)
                    {
                        return true;
                    }
                    // 中央４看板３
                    else if (row == 15 && column == 29 && eventNum == 3)
                    {
                        return true;
                    }
                    // 中央４看板４
                    else if (row == 19 && column == 25 && eventNum == 4)
                    {
                        return true;
                    }
                    // 知の部屋、看板１
                    else if (row == 8 && column == 49 && eventNum == 5)
                    {
                        return true;
                    }
                    // 知の部屋　ファースト：チェックポイント１
                    else if (row == 2 && column == 35 && eventNum == 8)
                    {
                        return true;
                    }
                    // 知の部屋　ファースト：チェックポイント２
                    else if (row == 20 && column == 57 && eventNum == 9)
                    {
                        return true;
                    }
                    // 知の部屋　ファースト：チェックポイント３
                    else if (row == 4 && column == 35 && eventNum == 10)
                    {
                        return true;
                    }
                    // 知の部屋　セカンド：チェックポイント４（上通路、中央部屋）
                    else if (row == 5 && column == 42 && eventNum == 11)
                    {
                        return true;
                    }
                    // 知の部屋　セカンド：チェックポイント５（左通路、右部屋）
                    else if (row == 5 && column == 46 && eventNum == 12)
                    {
                        return true;
                    }
                    // 知の部屋　セカンド：チェックポイント６（下通路、下部屋）
                    else if (row == 17 && column == 56 && eventNum == 13)
                    {
                        return true;
                    }
                    // 知の部屋　サード：チェックポイント７（右通路、中央部屋）
                    else if (row == 14 && column == 52 && eventNum == 14)
                    {
                        return true;
                    }
                    // 知の部屋　サード：チェックポイント８（左通路、左部屋）
                    else if (row == 5 && column == 38 && eventNum == 15)
                    {
                        return true;
                    }
                    // 知の部屋　サード：チェックポイント９（下通路、上部屋）
                    else if (row == 11 && column == 56 && eventNum == 16)
                    {
                        return true;
                    }
                    // 知の部屋、一本書き看板
                    else if (row == 15 && column == 42 && eventNum == 17)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル０－１
                    else if (row == 13 && column == 42 && eventNum == 18)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル１
                    else if (row == 11 && column == 42 && eventNum == 19)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル１－１
                    else if (row == 11 && column == 40 && eventNum == 20)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル１－２
                    else if (row == 11 && column == 38 && eventNum == 21)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル２
                    else if (row == 13 && column == 38 && eventNum == 22)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル２－１
                    else if (row == 13 && column == 40 && eventNum == 23)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル３
                    else if (row == 15 && column == 40 && eventNum == 24)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル３－１
                    else if (row == 15 && column == 38 && eventNum == 25)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル３－２
                    else if (row == 17 && column == 38 && eventNum == 26)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル４
                    else if (row == 19 && column == 38 && eventNum == 27)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル４－１
                    else if (row == 19 && column == 40 && eventNum == 28)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル４－２
                    else if (row == 17 && column == 40 && eventNum == 29)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル５
                    else if (row == 17 && column == 42 && eventNum == 30)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル５－１
                    else if (row == 19 && column == 42 && eventNum == 31)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル５－２
                    else if (row == 19 && column == 44 && eventNum == 32)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル６
                    else if (row == 19 && column == 46 && eventNum == 33)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル６－１
                    else if (row == 17 && column == 46 && eventNum == 34)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル６－２
                    else if (row == 17 && column == 44 && eventNum == 35)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル６－３
                    else if (row == 15 && column == 44 && eventNum == 36)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル６－４
                    else if (row == 13 && column == 44 && eventNum == 37)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル７
                    else if (row == 11 && column == 44 && eventNum == 38)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル７－１
                    else if (row == 11 && column == 46 && eventNum == 39)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル７－２
                    else if (row == 13 && column == 46 && eventNum == 40)
                    {
                        return true;
                    }
                    // 知の部屋、１本書き、タイル８
                    else if (row == 15 && column == 46 && eventNum == 41)
                    {
                        return true;
                    }
                    // 技の部屋、看板１
                    else if (row == 27 && column == 57 && eventNum == 42)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＡ
                    else if ((row == 26 && column == 56 && eventNum == 43) ||
                             (row == 26 && column == 55 && eventNum == 43) ||
                             (row == 26 && column == 54 && eventNum == 43) ||
                             (row == 26 && column == 53 && eventNum == 43) ||
                             (row == 26 && column == 52 && eventNum == 43) ||
                             (row == 26 && column == 51 && eventNum == 43) ||
                             (row == 26 && column == 50 && eventNum == 43) ||
                             (row == 26 && column == 49 && eventNum == 43) ||
                             (row == 26 && column == 48 && eventNum == 43) ||

                             (row == 27 && column == 56 && eventNum == 43) ||
                             (row == 27 && column == 55 && eventNum == 43) ||
                             (row == 27 && column == 54 && eventNum == 43) ||
                             (row == 27 && column == 53 && eventNum == 43) ||
                             (row == 27 && column == 52 && eventNum == 43) ||
                             (row == 27 && column == 51 && eventNum == 43) ||
                             (row == 27 && column == 50 && eventNum == 43) ||
                             (row == 27 && column == 49 && eventNum == 43) ||
                             (row == 27 && column == 48 && eventNum == 43) ||

                             (row == 28 && column == 56 && eventNum == 43) ||
                             (row == 28 && column == 55 && eventNum == 43) ||
                             (row == 28 && column == 54 && eventNum == 43) ||
                             (row == 28 && column == 53 && eventNum == 43) ||
                             (row == 28 && column == 52 && eventNum == 43) ||
                             (row == 28 && column == 51 && eventNum == 43) ||
                             (row == 28 && column == 50 && eventNum == 43) ||
                             (row == 28 && column == 49 && eventNum == 43) ||
                             (row == 28 && column == 48 && eventNum == 43))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＡクリア
                    else if ((row == 26 && column == 47 && eventNum == 44) ||
                             (row == 27 && column == 47 && eventNum == 44) ||
                             (row == 28 && column == 47 && eventNum == 44))
                    {
                        return true;
                    }
                    // 技の部屋、看板２
                    else if (row == 27 && column == 45 && eventNum == 45)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＢ
                    else if ((row == 26 && column == 44 && eventNum == 46) ||
                             (row == 26 && column == 43 && eventNum == 46) ||
                             (row == 26 && column == 42 && eventNum == 46) ||
                             (row == 26 && column == 41 && eventNum == 46) ||
                             (row == 26 && column == 40 && eventNum == 46) ||
                             (row == 26 && column == 39 && eventNum == 46) ||
                             (row == 26 && column == 38 && eventNum == 46) ||
                             (row == 26 && column == 37 && eventNum == 46) ||
                             (row == 26 && column == 36 && eventNum == 46) ||

                             (row == 27 && column == 44 && eventNum == 46) ||
                             (row == 27 && column == 43 && eventNum == 46) ||
                             (row == 27 && column == 42 && eventNum == 46) ||
                             (row == 27 && column == 41 && eventNum == 46) ||
                             (row == 27 && column == 40 && eventNum == 46) ||
                             (row == 27 && column == 39 && eventNum == 46) ||
                             (row == 27 && column == 38 && eventNum == 46) ||
                             (row == 27 && column == 37 && eventNum == 46) ||
                             (row == 27 && column == 36 && eventNum == 46) ||

                             (row == 28 && column == 44 && eventNum == 46) ||
                             (row == 28 && column == 43 && eventNum == 46) ||
                             (row == 28 && column == 42 && eventNum == 46) ||
                             (row == 28 && column == 41 && eventNum == 46) ||
                             (row == 28 && column == 40 && eventNum == 46) ||
                             (row == 28 && column == 39 && eventNum == 46) ||
                             (row == 28 && column == 38 && eventNum == 46) ||
                             (row == 28 && column == 37 && eventNum == 46) ||
                             (row == 28 && column == 36 && eventNum == 46))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＢクリア
                    else if ((row == 26 && column == 35 && eventNum == 47) ||
                             (row == 27 && column == 35 && eventNum == 47) ||
                             (row == 28 && column == 35 && eventNum == 47))
                    {
                        return true;
                    }
                    // 技の部屋、看板３
                    else if (row == 27 && column == 33 && eventNum == 48)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＣ
                    else if ((row == 26 && column == 32 && eventNum == 49) ||
                             (row == 26 && column == 31 && eventNum == 49) ||
                             (row == 26 && column == 30 && eventNum == 49) ||
                             (row == 26 && column == 29 && eventNum == 49) ||
                             (row == 26 && column == 28 && eventNum == 49) ||

                             (row == 27 && column == 32 && eventNum == 49) ||
                             (row == 27 && column == 31 && eventNum == 49) ||
                             (row == 27 && column == 30 && eventNum == 49) ||
                             (row == 27 && column == 29 && eventNum == 49) ||
                             (row == 27 && column == 28 && eventNum == 49) ||

                             (row == 28 && column == 32 && eventNum == 49) ||
                             (row == 28 && column == 31 && eventNum == 49) ||
                             (row == 28 && column == 30 && eventNum == 49) ||
                             (row == 28 && column == 29 && eventNum == 49) ||
                             (row == 28 && column == 28 && eventNum == 49) ||

                             (row == 29 && column == 30 && eventNum == 49) ||
                             (row == 29 && column == 29 && eventNum == 49) ||
                             (row == 29 && column == 28 && eventNum == 49) ||

                             (row == 30 && column == 30 && eventNum == 49) ||
                             (row == 30 && column == 29 && eventNum == 49) ||
                             (row == 30 && column == 28 && eventNum == 49) ||

                             (row == 31 && column == 30 && eventNum == 49) ||
                             (row == 31 && column == 29 && eventNum == 49) ||
                             (row == 31 && column == 28 && eventNum == 49) ||

                             (row == 32 && column == 30 && eventNum == 49) ||
                             (row == 32 && column == 29 && eventNum == 49) ||
                             (row == 32 && column == 28 && eventNum == 49) ||

                             (row == 33 && column == 32 && eventNum == 49) ||
                             (row == 33 && column == 31 && eventNum == 49) ||
                             (row == 33 && column == 30 && eventNum == 49) ||
                             (row == 33 && column == 29 && eventNum == 49) ||
                             (row == 33 && column == 28 && eventNum == 49) ||

                             (row == 34 && column == 32 && eventNum == 49) ||
                             (row == 34 && column == 31 && eventNum == 49) ||
                             (row == 34 && column == 30 && eventNum == 49) ||
                             (row == 34 && column == 29 && eventNum == 49) ||
                             (row == 34 && column == 28 && eventNum == 49) ||

                             (row == 35 && column == 32 && eventNum == 49) ||
                             (row == 35 && column == 31 && eventNum == 49) ||
                             (row == 35 && column == 30 && eventNum == 49) ||
                             (row == 35 && column == 29 && eventNum == 49) ||
                             (row == 35 && column == 28 && eventNum == 49))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＣクリア
                    else if ((row == 33 && column == 33 && eventNum == 50) ||
                             (row == 34 && column == 33 && eventNum == 50) ||
                             (row == 35 && column == 33 && eventNum == 50))
                    {
                        return true;
                    }
                    // 技の部屋、看板４
                    else if (row == 34 && column == 35 && eventNum == 51)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＤ
                    else if ((row == 33 && column == 36 && eventNum == 52) ||
                             (row == 33 && column == 37 && eventNum == 52) ||
                             (row == 33 && column == 38 && eventNum == 52) ||
                             (row == 33 && column == 39 && eventNum == 52) ||
                             (row == 33 && column == 40 && eventNum == 52) ||
                             (row == 33 && column == 41 && eventNum == 52) ||
                             (row == 33 && column == 42 && eventNum == 52) ||
                             (row == 33 && column == 43 && eventNum == 52) ||
                             (row == 33 && column == 44 && eventNum == 52) ||

                             (row == 34 && column == 36 && eventNum == 52) ||
                             (row == 34 && column == 37 && eventNum == 52) ||
                             (row == 34 && column == 38 && eventNum == 52) ||
                             (row == 34 && column == 39 && eventNum == 52) ||
                             (row == 34 && column == 40 && eventNum == 52) ||
                             (row == 34 && column == 41 && eventNum == 52) ||
                             (row == 34 && column == 42 && eventNum == 52) ||
                             (row == 34 && column == 43 && eventNum == 52) ||
                             (row == 34 && column == 44 && eventNum == 52) ||

                             (row == 35 && column == 36 && eventNum == 52) ||
                             (row == 35 && column == 37 && eventNum == 52) ||
                             (row == 35 && column == 38 && eventNum == 52) ||
                             (row == 35 && column == 39 && eventNum == 52) ||
                             (row == 35 && column == 40 && eventNum == 52) ||
                             (row == 35 && column == 41 && eventNum == 52) ||
                             (row == 35 && column == 42 && eventNum == 52) ||
                             (row == 35 && column == 43 && eventNum == 52) ||
                             (row == 35 && column == 44 && eventNum == 52))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＤクリア
                    else if ((row == 33 && column == 45 && eventNum == 53) ||
                             (row == 34 && column == 45 && eventNum == 53) ||
                             (row == 35 && column == 45 && eventNum == 53))
                    {
                        return true;
                    }
                    // 技の部屋、看板５
                    else if (row == 34 && column == 47 && eventNum == 54)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＥ
                    else if ((row == 34 && column == 48 && eventNum == 55))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＥクリア
                    else if ((row == 33 && column == 57 && eventNum == 56) ||
                             (row == 34 && column == 57 && eventNum == 56) ||
                             (row == 35 && column == 57 && eventNum == 56))
                    {
                        return true;
                    }
                    // 技の部屋、看板６
                    else if (row == 34 && column == 59 && eventNum == 57)
                    {
                        return true;
                    }
                    // 心の部屋、ヒント１
                    else if (row == 12 && column == 0 && eventNum == 58)
                    {
                        return true;
                    }
                    // 心の部屋、題材１
                    else if ((row == 0 && column == 0 && eventNum == 59) ||
                             (row == 1 && column == 0 && eventNum == 59) ||
                             (row == 2 && column == 0 && eventNum == 59) ||
                             (row == 3 && column == 0 && eventNum == 59) ||
                             (row == 4 && column == 0 && eventNum == 59))
                    {
                        return true;
                    }
                    // 心の部屋、題材２
                    else if ((row == 0 && column == 9 && eventNum == 60) ||
                             (row == 0 && column == 10 && eventNum == 60) ||
                             (row == 0 && column == 11 && eventNum == 60) ||
                             (row == 0 && column == 12 && eventNum == 60) ||
                             (row == 0 && column == 13 && eventNum == 60) ||
                             (row == 0 && column == 14 && eventNum == 60))
                    {
                        return true;
                    }
                    // 心の部屋、題材３
                    else if ((row == 0 && column == 28 && eventNum == 61))
                    {
                        return true;
                    }
                    // 心の部屋、題材４
                    else if ((row == 4 && column == 3 && eventNum == 62) ||
                             (row == 4 && column == 4 && eventNum == 62) ||
                             (row == 4 && column == 5 && eventNum == 62) ||
                             (row == 5 && column == 3 && eventNum == 62) ||
                             (row == 5 && column == 4 && eventNum == 62) ||
                             (row == 5 && column == 5 && eventNum == 62) ||
                             (row == 6 && column == 3 && eventNum == 62) ||
                             (row == 6 && column == 4 && eventNum == 62) ||
                             (row == 6 && column == 5 && eventNum == 62)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材５
                    else if ((row == 2 && column == 9 && eventNum == 63) ||
                             (row == 2 && column == 10 && eventNum == 63) ||
                             (row == 2 && column == 11 && eventNum == 63) ||
                             (row == 3 && column == 9 && eventNum == 63) ||
                             (row == 3 && column == 10 && eventNum == 63) ||
                             (row == 3 && column == 11 && eventNum == 63) ||
                             (row == 4 && column == 9 && eventNum == 63) ||
                             (row == 4 && column == 10 && eventNum == 63) ||
                             (row == 4 && column == 11 && eventNum == 63)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材６
                    else if ((row == 2 && column == 22 && eventNum == 64) ||
                             (row == 2 && column == 23 && eventNum == 64) ||
                             (row == 2 && column == 24 && eventNum == 64) ||
                             (row == 3 && column == 22 && eventNum == 64) ||
                             (row == 3 && column == 23 && eventNum == 64) ||
                             (row == 3 && column == 24 && eventNum == 64) ||
                             (row == 4 && column == 22 && eventNum == 64) ||
                             (row == 4 && column == 23 && eventNum == 64) ||
                             (row == 4 && column == 24 && eventNum == 64)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材７
                    else if ((row == 6 && column == 14 && eventNum == 65) ||
                             (row == 6 && column == 15 && eventNum == 65) ||
                             (row == 6 && column == 16 && eventNum == 65) ||
                             (row == 7 && column == 14 && eventNum == 65) ||
                             (row == 7 && column == 15 && eventNum == 65) ||
                             (row == 7 && column == 16 && eventNum == 65) ||
                             (row == 8 && column == 14 && eventNum == 65) ||
                             (row == 8 && column == 15 && eventNum == 65) ||
                             (row == 8 && column == 16 && eventNum == 65)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材８
                    else if ((row == 5 && column == 28 && eventNum == 66) ||
                             (row == 6 && column == 28 && eventNum == 66) ||
                             (row == 7 && column == 28 && eventNum == 66)
                             )
                    {
                        return true;
                    }
                    // 心の部屋、題材９
                    else if ((row == 8 && column == 20 && eventNum == 67) ||
                             (row == 8 && column == 21 && eventNum == 67) ||
                             (row == 8 && column == 22 && eventNum == 67) ||
                             (row == 8 && column == 23 && eventNum == 67) ||
                             (row == 8 && column == 24 && eventNum == 67) ||
                             (row == 8 && column == 25 && eventNum == 67) ||
                             (row == 9 && column == 20 && eventNum == 67) ||
                             (row == 9 && column == 21 && eventNum == 67) ||
                             (row == 9 && column == 22 && eventNum == 67) ||
                             (row == 9 && column == 23 && eventNum == 67) ||
                             (row == 9 && column == 24 && eventNum == 67) ||
                             (row == 9 && column == 25 && eventNum == 67) ||
                             (row == 10 && column == 20 && eventNum == 67) ||
                             (row == 10 && column == 21 && eventNum == 67) ||
                             (row == 10 && column == 22 && eventNum == 67) ||
                             (row == 10 && column == 23 && eventNum == 67) ||
                             (row == 10 && column == 24 && eventNum == 67) ||
                             (row == 10 && column == 25 && eventNum == 67)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材１０
                    else if ((row == 12 && column == 9 && eventNum == 68) ||
                             (row == 12 && column == 10 && eventNum == 68) ||
                             (row == 12 && column == 11 && eventNum == 68) ||
                             (row == 12 && column == 12 && eventNum == 68) ||
                             (row == 12 && column == 13 && eventNum == 68) ||
                             (row == 12 && column == 14 && eventNum == 68)
                        )
                    {
                        return true;
                    }
                    // 力の部屋、ボス１
                    else if (row == 37 && column == 22 && eventNum == 69)
                    {
                        return true;
                    }
                    // 力の部屋、ボス２
                    else if (row == 27 && column == 22 && eventNum == 70)
                    {
                        return true;
                    }
                    // 力の部屋、ボス３
                    else if (row == 19 && column == 16 && eventNum == 71)
                    {
                        return true;
                    }
                    // 力の部屋、ボス４
                    else if (row == 22 && column == 2 && eventNum == 72)
                    {
                        return true;
                    }
                    // 力の部屋、ボス５
                    else if (row == 31 && column == 3 && eventNum == 73)
                    {
                        return true;
                    }
                    // 力の部屋、ボス６
                    else if (row == 36 && column == 14 && eventNum == 74)
                    {
                        return true;
                    }
                    // 宝箱１
                    else if (row == 16 && column == 59 && !GroundOne.WE.TruthTreasure21 && eventNum == 75)
                    {
                        return true;
                    }
                    // 宝箱２
                    else if (row == 12 && column == 35 && !GroundOne.WE.TruthTreasure22 && eventNum == 76)
                    {
                        return true;
                    }
                    // 宝箱３
                    else if (row == 5 && column == 55 && !GroundOne.WE.TruthTreasure23 && eventNum == 77)
                    {
                        return true;
                    }
                    // 宝箱４
                    else if (row == 25 && column == 59 && !GroundOne.WE.TruthTreasure24 && eventNum == 78)
                    {
                        return true;
                    }
                    // 宝箱５
                    else if (row == 27 && column == 46 && !GroundOne.WE.TruthTreasure25 && eventNum == 79)
                    {
                        return true;
                    }
                    // 宝箱６
                    else if (row == 27 && column == 34 && !GroundOne.WE.TruthTreasure26 && eventNum == 80)
                    {
                        return true;
                    }
                    // 宝箱７
                    else if (row == 34 && column == 34 && !GroundOne.WE.TruthTreasure27 && eventNum == 81)
                    {
                        return true;
                    }
                    // 宝箱８
                    else if (row == 34 && column == 46 && !GroundOne.WE.TruthTreasure28 && eventNum == 82)
                    {
                        return true;
                    }
                    // 宝箱９
                    else if (row == 34 && column == 58 && !GroundOne.WE.TruthTreasure29 && eventNum == 83)
                    {
                        return true;
                    }
                    // 宝箱１０
                    else if (row == 39 && column == 31 && !GroundOne.WE.TruthTreasure210 && eventNum == 84)
                    {
                        return true;
                    }
                    // 宝箱１１
                    else if (row == 15 && column == 13 && !GroundOne.WE.TruthTreasure211 && eventNum == 85)
                    {
                        return true;
                    }
                    // 宝箱１２
                    else if (row == 6 && column == 29 && !GroundOne.WE.TruthTreasure212 && eventNum == 86)
                    {
                        return true;
                    }
                    // 宝箱１３
                    else if (row == 39 && column == 23 && !GroundOne.WE.TruthTreasure213 && eventNum == 87)
                    {
                        return true;
                    }
                    // 宝箱１４
                    else if (row == 31 && column == 22 && !GroundOne.WE.TruthTreasure214 && eventNum == 88)
                    {
                        return true;
                    }
                    // 宝箱１５
                    else if (row == 19 && column == 22 && !GroundOne.WE.TruthTreasure215 && eventNum == 89)
                    {
                        return true;
                    }
                    // 宝箱１６
                    else if (row == 19 && column == 4 && !GroundOne.WE.TruthTreasure216 && eventNum == 90)
                    {
                        return true;
                    }
                    // 宝箱１７
                    else if (row == 28 && column == 6 && !GroundOne.WE.TruthTreasure217 && eventNum == 91)
                    {
                        return true;
                    }
                    // 宝箱１８
                    else if (row == 39 && column == 10 && !GroundOne.WE.TruthTreasure218 && eventNum == 92)
                    {
                        return true;
                    }
                    // 知の部屋、複合レバーの看板１
                    else if (row == 5 && column == 51 && eventNum == 93)
                    {
                        return true;
                    }
                    // 知の部屋、複合レバー１－１
                    else if (row == 4 && column == 51 && eventNum == 94)
                    {
                        return true;
                    }
                    // 知の部屋、複合レバー１－２
                    else if (row == 6 && column == 51 && eventNum == 95)
                    {
                        return true;
                    }
                    // 技の部屋、複合レバーの看板１
                    else if (row == 37 && column == 30 && eventNum == 96)
                    {
                        return true;
                    }
                    // 技の部屋、複合レバー２－１
                    else if (row == 37 && column == 29 && eventNum == 97)
                    {
                        return true;
                    }
                    // 技の部屋、複合レバー２－２
                    else if (row == 37 && column == 31 && eventNum == 98)
                    {
                        return true;
                    }
                    // 力の部屋、複合レバーの看板１
                    else if (row == 26 && column == 14 && eventNum == 99)
                    {
                        return true;
                    }
                    // 力の部屋、複合レバー３－１
                    else if (row == 26 && column == 13 && eventNum == 100)
                    {
                        return true;
                    }
                    // 力の部屋、複合レバー３－２
                    else if (row == 26 && column == 15 && eventNum == 101)
                    {
                        return true;
                    }
                    // 心の部屋、複合レバーの看板１
                    else if (row == 6 && column == 30 && eventNum == 102)
                    {
                        return true;
                    }
                    // 心の部屋、複合レバー３－１
                    else if (row == 5 && column == 30 && eventNum == 103)
                    {
                        return true;
                    }
                    // 心の部屋、複合レバー３－２
                    else if (row == 7 && column == 30 && eventNum == 104)
                    {
                        return true;
                    }
                    // 技の部屋、隠し通路
                    else if (row == 37 && column == 59 && eventNum == 105)
                    {
                        return true;
                    }
                    // ３階への階段前
                    else if (row == 26 && column == 16 && eventNum == 106)
                    {
                        return true;
                    }
                    // ３階への階段
                    else if (row == 26 && column == 17 && eventNum == 107)
                    {
                        return true;
                    }
                    // 真実解のイベント２
                    else if (row == 26 && column == 10 && eventNum == 108)
                    {
                        return true;
                    }
                    // 技の部屋、ラストの部屋でダッシュ中戦闘発生を回避
                    else if ((row == 33 && column == 48 && eventNum == 109) ||
                             (row == 33 && column == 49 && eventNum == 109) ||
                             (row == 33 && column == 50 && eventNum == 109) ||
                             (row == 33 && column == 51 && eventNum == 109) ||
                             (row == 33 && column == 52 && eventNum == 109) ||
                             (row == 33 && column == 53 && eventNum == 109) ||
                             (row == 33 && column == 54 && eventNum == 109) ||
                             (row == 33 && column == 55 && eventNum == 109) ||
                             (row == 33 && column == 56 && eventNum == 109) ||

                             (row == 34 && column == 48 && eventNum == 109) ||
                             (row == 34 && column == 49 && eventNum == 109) ||
                             (row == 34 && column == 50 && eventNum == 109) ||
                             (row == 34 && column == 51 && eventNum == 109) ||
                             (row == 34 && column == 52 && eventNum == 109) ||
                             (row == 34 && column == 53 && eventNum == 109) ||
                             (row == 34 && column == 54 && eventNum == 109) ||
                             (row == 34 && column == 55 && eventNum == 109) ||
                             (row == 34 && column == 56 && eventNum == 109) ||

                             (row == 35 && column == 48 && eventNum == 109) ||
                             (row == 35 && column == 49 && eventNum == 109) ||
                             (row == 35 && column == 50 && eventNum == 109) ||
                             (row == 35 && column == 51 && eventNum == 109) ||
                             (row == 35 && column == 52 && eventNum == 109) ||
                             (row == 35 && column == 53 && eventNum == 109) ||
                             (row == 35 && column == 54 && eventNum == 109) ||
                             (row == 35 && column == 55 && eventNum == 109) ||
                             (row == 35 && column == 56 && eventNum == 109))
                    {
                        return true;
                    }
                    break;
                #endregion
                #region "３階"
                case 3:
                    // 上り階段
                    if (row == 19 && column == 0 && eventNum == 0)
                    {
                        return true;
                    }
                    // 始めの誘導
                    else if ((row == 19 && column == 3 && eventNum == 1) ||
                             (row == 20 && column == 3 && eventNum == 1))
                    {
                        return true;
                    }
                    // 始めの誘導縛り
                    else if (row == 18 && column == 3 && eventNum == 2)
                    {
                        return true;
                    }
                    else if (row == 21 && column == 3 && eventNum == 3)
                    {
                        return true;
                    }
                    else if (row == 19 && column == 9 && eventNum == 4)
                    {
                        return true;
                    }
                    else if (row == 20 && column == 9 && eventNum == 5)
                    {
                        return true;
                    }
                    // 鏡解説（１）
                    else if (row == 16 && column == 3 && eventNum == 6)
                    {
                        return true;
                    }
                    else if (row == 23 && column == 3 && eventNum == 7)
                    {
                        return true;
                    }
                    // 鏡ワープ１－１
                    else if (row == 1 && column == 4 && eventNum == 8)
                    {
                        return true;
                    }
                    else if (row == 4 && column == 4 && eventNum == 9)
                    {
                        return true;
                    }
                    else if (row == 7 && column == 4 && eventNum == 10)
                    {
                        return true;
                    }
                    else if (row == 10 && column == 4 && eventNum == 11)
                    {
                        return true;
                    }
                    else if (row == 13 && column == 4 && eventNum == 12)
                    {
                        return true;
                    }
                    else if (row == 16 && column == 4 && eventNum == 13)
                    {
                        return true;
                    }
                    else if (row == 23 && column == 4 && eventNum == 14)
                    {
                        return true;
                    }
                    else if (row == 26 && column == 4 && eventNum == 15)
                    {
                        return true;
                    }
                    else if (row == 29 && column == 4 && eventNum == 16)
                    {
                        return true;
                    }
                    else if (row == 32 && column == 4 && eventNum == 17)
                    {
                        return true;
                    }
                    else if (row == 35 && column == 4 && eventNum == 18)
                    {
                        return true;
                    }
                    else if (row == 38 && column == 4 && eventNum == 19)
                    {
                        return true;
                    }
                    // 鏡ワープ１－２
                    else if (row == 19 && column == 17 && eventNum == 20)
                    {
                        return true;
                    }
                    else if (row == 25 && column == 15 && eventNum == 21)
                    {
                        return true;
                    }
                    else if (row == 10 && column == 17 && eventNum == 22)
                    {
                        return true;
                    }
                    else if (row == 17 && column == 15 && eventNum == 23)
                    {
                        return true;
                    }
                    else if (row == 1 && column == 17 && eventNum == 24)
                    {
                        return true;
                    }
                    else if (row == 3 && column == 15 && eventNum == 25)
                    {
                        return true;
                    }
                    else if (row == 1 && column == 6 && eventNum == 26)
                    {
                        return true;
                    }
                    else if (row == 6 && column == 10 && eventNum == 27)
                    {
                        return true;
                    }
                    else if (row == 35 && column == 17 && eventNum == 28)
                    {
                        return true;
                    }
                    else if (row == 38 && column == 11 && eventNum == 29)
                    {
                        return true;
                    }
                    else if (row == 8 && column == 9 && eventNum == 30)
                    {
                        return true;
                    }
                    else if (row == 17 && column == 6 && eventNum == 31)
                    {
                        return true;
                    }
                    else if (row == 16 && column == 11 && eventNum == 32)
                    {
                        return true;
                    }
                    else if (row == 25 && column == 13 && eventNum == 33)
                    {
                        return true;
                    }
                    else if (row == 22 && column == 9 && eventNum == 34)
                    {
                        return true;
                    }
                    else if (row == 24 && column == 6 && eventNum == 35)
                    {
                        return true;
                    }
                    // 鏡ワープ１－３
                    else if (row == 27 && column == 12 && eventNum == 36)
                    {
                        return true;
                    }
                    else if (row == 30 && column == 18 && eventNum == 37)
                    {
                        return true;
                    }
                    else if (row == 5 && column == 15 && eventNum == 38)
                    {
                        return true;
                    }
                    else if (row == 8 && column == 17 && eventNum == 39)
                    {
                        return true;
                    }
                    else if (row == 9 && column == 11 && eventNum == 40)
                    {
                        return true;
                    }
                    else if (row == 14 && column == 13 && eventNum == 41)
                    {
                        return true;
                    }
                    else if (row == 36 && column == 6 && eventNum == 42)
                    {
                        return true;
                    }
                    else if (row == 38 && column == 9 && eventNum == 43)
                    {
                        return true;
                    }
                    // 鏡ワープ１－４
                    else if (row == 31 && column == 10 && eventNum == 44)
                    {
                        return true;
                    }
                    // 鏡ワープ１ーClear
                    else if (row == 31 && column == 1 && eventNum == 45)
                    {
                        return true;
                    }
                    // 鏡ワープ失敗
                    else if ((row == 18 && column == 0 && eventNum == 46) ||
                             (row == 21 && column == 2 && eventNum == 46) ||
                             (row == 21 && column == 4 && eventNum == 46) ||
                             (row == 33 && column == 6 && eventNum == 46) ||
                             (row == 31 && column == 6 && eventNum == 46) ||
                             (row == 26 && column == 6 && eventNum == 46) ||
                             (row == 26 && column == 10 && eventNum == 46) ||
                             (row == 0 && column == 11 && eventNum == 46) ||
                             (row == 0 && column == 13 && eventNum == 46) ||
                             (row == 20 && column == 18 && eventNum == 46) ||
                             (row == 33 && column == 17 && eventNum == 46) ||
                             (row == 39 && column == 17 && eventNum == 46) ||
                             (row == 18 && column == 19 && eventNum == 46) ||
                             (row == 20 && column == 19 && eventNum == 46))
                    {
                        return true;
                    }
                    // 真実解のイベント１
                    else if (row == 35 && column == 1 && eventNum == 47)
                    {
                        return true;
                    }
                    // 鏡ワープ２－１
                    else if (row == 14 && column == 22 && eventNum == 48) // 38
                    {
                        return true;
                    }
                    else if (row == 14 && column == 24 && eventNum == 49) // 39
                    {
                        return true;
                    }
                    else if (row == 14 && column == 26 && eventNum == 50) // 40
                    {
                        return true;
                    }
                    else if (row == 34 && column == 41 && eventNum == 51) // 41
                    {
                        return true;
                    }
                    else if (row == 34 && column == 43 && eventNum == 52) // 42
                    {
                        return true;
                    }
                    else if (row == 34 && column == 45 && eventNum == 53) // 43
                    {
                        return true;
                    }
                    else if (row == 9 && column == 45 && eventNum == 54) // 44
                    {
                        return true;
                    }
                    else if (row == 9 && column == 47 && eventNum == 55) // 45
                    {
                        return true;
                    }
                    else if (row == 32 && column == 21 && eventNum == 56) // 46
                    {
                        return true;
                    }
                    else if (row == 11 && column == 33 && eventNum == 57) // 47
                    {
                        return true;
                    }
                    else if (row == 13 && column == 32 && eventNum == 58) // 48
                    {
                        return true;
                    }
                    else if (row == 18 && column == 41 && eventNum == 59) // 49
                    {
                        return true;
                    }
                    else if (row == 22 && column == 25 && eventNum == 60) // 50
                    {
                        return true;
                    }
                    else if (row == 28 && column == 23 && eventNum == 61) // 51
                    {
                        return true;
                    }
                    else if (row == 9 && column == 53 && eventNum == 62) // 52
                    {
                        return true;
                    }
                    else if (row == 16 && column == 53 && eventNum == 63) // 53
                    {
                        return true;
                    }
                    else if (row == 34 && column == 31 && eventNum == 64) // 54
                    {
                        return true;
                    }
                    else if (row == 34 && column == 33 && eventNum == 65) // 55
                    {
                        return true;
                    }
                    else if (row == 1 && column == 27 && eventNum == 66) // 56
                    {
                        return true;
                    }
                    else if (row == 1 && column == 53 && eventNum == 67) // 57
                    {
                        return true;
                    }
                    else if (row == 3 && column == 34 && eventNum == 68) // 58
                    {
                        return true;
                    }
                    else if (row == 28 && column == 27 && eventNum == 69) // 59
                    {
                        return true;
                    }
                    else if (row == 28 && column == 29 && eventNum == 70) // 60
                    {
                        return true;
                    }
                    else if (row == 28 && column == 31 && eventNum == 71) // 61
                    {
                        return true;
                    }
                    else if (row == 13 && column == 39 && eventNum == 72) // 62
                    {
                        return true;
                    }
                    else if (row == 13 && column == 40 && eventNum == 73) // 63
                    {
                        return true;
                    }
                    else if (row == 30 && column == 38 && eventNum == 74) // 64
                    {
                        return true;
                    }
                    else if (row == 33 && column == 21 && eventNum == 75) // 65
                    {
                        return true;
                    }
                    else if (row == 23 && column == 39 && eventNum == 76) // 66
                    {
                        return true;
                    }
                    else if (row == 28 && column == 50 && eventNum == 77) // 67
                    {
                        return true;
                    }
                    else if (row == 28 && column == 52 && eventNum == 78) // 68
                    {
                        return true;
                    }
                    else if (row == 28 && column == 54 && eventNum == 79) // 69
                    {
                        return true;
                    }
                    else if (row == 11 && column == 21 && eventNum == 80) // 70
                    {
                        return true;
                    }
                    else if (row == 18 && column == 23 && eventNum == 81) // 71
                    {
                        return true;
                    }
                    else if (row == 27 && column == 40 && eventNum == 82) // 72
                    {
                        return true;
                    }
                    else if (row == 3 && column == 25 && eventNum == 83) // 73
                    {
                        return true;
                    }
                    else if (row == 3 && column == 27 && eventNum == 84) // 74
                    {
                        return true;
                    }
                    else if (row == 3 && column == 29 && eventNum == 85) // 75
                    {
                        return true;
                    }
                    else if (row == 11 && column == 39 && eventNum == 86) // 76
                    {
                        return true;
                    }
                    else if (row == 11 && column == 41 && eventNum == 87) // 77
                    {
                        return true;
                    }
                    else if (row == 16 && column == 34 && eventNum == 88) // 78
                    {
                        return true;
                    }
                    else if (row == 16 && column == 30 && eventNum == 89) // 79
                    {
                        return true;
                    }
                    else if (row == 18 && column == 48 && eventNum == 90) // 80
                    {
                        return true;
                    }
                    else if (row == 19 && column == 36 && eventNum == 91) // 81
                    {
                        return true;
                    }
                    else if (row == 21 && column == 35 && eventNum == 92) // 82
                    {
                        return true;
                    }
                    else if (row == 23 && column == 34 && eventNum == 93) // 83
                    {
                        return true;
                    }
                    else if (row == 25 && column == 33 && eventNum == 94) // 84
                    {
                        return true;
                    }
                    else if (row == 29 && column == 36 && eventNum == 95) // 85
                    {
                        return true;
                    }
                    else if (row == 9 && column == 50 && eventNum == 96) // 86
                    {
                        return true;
                    }
                    else if (row == 14 && column == 45 && eventNum == 97) // 87
                    {
                        return true;
                    }
                    else if (row == 30 && column == 27 && eventNum == 98) // 88
                    {
                        return true;
                    }
                    else if (row == 30 && column == 29 && eventNum == 99) // 89
                    {
                        return true;
                    }
                    else if (row == 30 && column == 31 && eventNum == 100) // 90
                    {
                        return true;
                    }
                    else if (row == 15 && column == 29 && eventNum == 101) // 91
                    {
                        return true;
                    }
                    else if (row == 11 && column == 34 && eventNum == 102) // 92
                    {
                        return true;
                    }
                    else if (row == 11 && column == 24 && eventNum == 103) // 93
                    {
                        return true;
                    }
                    else if (row == 3 && column == 24 && eventNum == 104) // 94
                    {
                        return true;
                    }
                    else if (row == 10 && column == 42 && eventNum == 105) // 95
                    {
                        return true;
                    }
                    else if (row == 34 && column == 49 && eventNum == 106) // 96
                    {
                        return true;
                    }
                    else if (row == 36 && column == 49 && eventNum == 107) // 97
                    {
                        return true;
                    }
                    else if (row == 38 && column == 49 && eventNum == 108) // 98
                    {
                        return true;
                    }
                    else if (row == 31 && column == 37 && eventNum == 109) // 99
                    {
                        return true;
                    }
                    else if (row == 33 && column == 37 && eventNum == 110) // 100
                    {
                        return true;
                    }
                    else if (row == 35 && column == 37 && eventNum == 111) // 101
                    {
                        return true;
                    }
                    else if (row == 12 && column == 41 && eventNum == 112) // 102
                    {
                        return true;
                    }
                    else if (row == 17 && column == 21 && eventNum == 113) // 103
                    {
                        return true;
                    }
                    else if (row == 23 && column == 24 && eventNum == 114) // 104
                    {
                        return true;
                    }
                    else if (row == 23 && column == 26 && eventNum == 115) // 105
                    {
                        return true;
                    }
                    else if (row == 23 && column == 28 && eventNum == 116) // 106
                    {
                        return true;
                    }
                    else if (row == 26 && column == 34 && eventNum == 117) // 107
                    {
                        return true;
                    }
                    else if (row == 28 && column == 34 && eventNum == 118) // 108
                    {
                        return true;
                    }
                    else if (row == 30 && column == 34 && eventNum == 119) // 109
                    {
                        return true;
                    }
                    else if (row == 32 && column == 34 && eventNum == 120) // 110
                    {
                        return true;
                    }
                    else if (row == 9 && column == 35 && eventNum == 121) // 111
                    {
                        return true;
                    }
                    else if (row == 20 && column == 50 && eventNum == 122) // 112
                    {
                        return true;
                    }
                    else if (row == 20 && column == 52 && eventNum == 123) // 113
                    {
                        return true;
                    }
                    else if (row == 24 && column == 31 && eventNum == 124) // 114
                    {
                        return true;
                    }
                    else if (row == 39 && column == 47 && eventNum == 125) // 115
                    {
                        return true;
                    }
                    else if (row == 10 && column == 54 && eventNum == 126) // 116
                    {
                        return true;
                    }
                    else if (row == 8 && column == 41 && eventNum == 127) // 117
                    {
                        return true;
                    }
                    else if (row == 5 && column == 46 && eventNum == 128) // 118
                    {
                        return true;
                    }
                    else if (row == 15 && column == 42 && eventNum == 129) // 119
                    {
                        return true;
                    }
                    else if (row == 22 && column == 48 && eventNum == 130) // 120
                    {
                        return true;
                    }
                    else if (row == 22 && column == 50 && eventNum == 131) // 121
                    {
                        return true;
                    }
                    else if (row == 22 && column == 52 && eventNum == 132) // 122
                    {
                        return true;
                    }
                    else if (row == 25 && column == 34 && eventNum == 133) // 123
                    {
                        return true;
                    }
                    else if (row == 17 && column == 26 && eventNum == 134) // 124
                    {
                        return true;
                    }
                    else if (row == 18 && column == 27 && eventNum == 135) // 125
                    {
                        return true;
                    }
                    else if (row == 19 && column == 28 && eventNum == 136) // 126
                    {
                        return true;
                    }
                    else if (row == 20 && column == 29 && eventNum == 137) // 127
                    {
                        return true;
                    }
                    else if (row == 17 && column == 42 && eventNum == 138) // 128
                    {
                        return true;
                    }
                    else if (row == 8 && column == 22 && eventNum == 139) // 129
                    {
                        return true;
                    }
                    else if (row == 6 && column == 22 && eventNum == 140) // 130
                    {
                        return true;
                    }
                    else if (row == 5 && column == 21 && eventNum == 141) // 131
                    {
                        return true;
                    }
                    else if (row == 24 && column == 44 && eventNum == 142) // 132
                    {
                        return true;
                    }
                    else if (row == 26 && column == 44 && eventNum == 143) // 133
                    {
                        return true;
                    }
                    else if (row == 24 && column == 53 && eventNum == 144) // 134
                    {
                        return true;
                    }
                    else if (row == 6 && column == 51 && eventNum == 145) // 135
                    {
                        return true;
                    }
                    else if (row == 6 && column == 52 && eventNum == 146) // 136
                    {
                        return true;
                    }
                    else if (row == 31 && column == 24 && eventNum == 147) // 137
                    {
                        return true;
                    }
                    else if (row == 26 && column == 22 && eventNum == 148) // 138
                    {
                        return true;
                    }
                    else if (row == 25 && column == 22 && eventNum == 149) // 139
                    {
                        return true;
                    }
                    else if (row == 21 && column == 24 && eventNum == 150) // 140
                    {
                        return true;
                    }
                    // ５Ｘ-１ルート
                    else if (row == 31 && column == 47 && eventNum == 151) // X1B
                    {
                        return true;
                    }
                    else if (row == 2 && column == 39 && eventNum == 152) // X1C
                    {
                        return true;
                    }
                    else if (row == 38 && column == 54 && eventNum == 153) // X1D
                    {
                        return true;
                    }
                    // ５Ｘ-２ルート
                    else if (row == 20 && column == 46 && eventNum == 154) // X2B
                    {
                        return true;
                    }
                    else if (row == 3 && column == 21 && eventNum == 155) // X2C
                    {
                        return true;
                    }
                    else if (row == 38 && column == 40 && eventNum == 156) // X2D
                    {
                        return true;
                    }
                    // ５Ｘ-３ルート
                    else if (row == 29 && column == 21 && eventNum == 157) // X3B
                    {
                        return true;
                    }
                    else if (row == 13 && column == 44 && eventNum == 158) // X3C
                    {
                        return true;
                    }
                    else if (row == 0 && column == 39 && eventNum == 159) // X2D
                    {
                        return true;
                    }
                    // ５Ｘ-４ルート
                    else if (row == 39 && column == 42 && eventNum == 160) // X4B
                    {
                        return true;
                    }
                    else if (row == 33 && column == 51 && eventNum == 161) // X4C
                    {
                        return true;
                    }
                    else if (row == 13 && column == 52 && eventNum == 162) // X4D
                    {
                        return true;
                    }
                    // ５Ｘ-５ルート
                    else if (row == 27 && column == 21 && eventNum == 163) // X5B
                    {
                        return true;
                    }
                    else if (row == 29 && column == 43 && eventNum == 164) // X5C
                    {
                        return true;
                    }
                    else if (row == 37 && column == 45 && eventNum == 165) // X5D
                    {
                        return true;
                    }
                    // ５Ｘ-Ｚ１
                    else if (row == 21 && column == 42 && eventNum == 166) // Z1
                    {
                        return true;
                    }
                    else if (row == 28 && column == 47 && eventNum == 167) // Z1
                    {
                        return true;
                    }
                    else if (row == 21 && column == 53 && eventNum == 168) // Z1
                    {
                        return true;
                    }
                    else if (row == 14 && column == 47 && eventNum == 169) // Z1
                    {
                        return true;
                    }
                    // ５Ｘ-Ｚ２
                    else if (row == 7 && column == 25 && eventNum == 170) // Z2
                    {
                        return true;
                    }
                    else if (row == 7 && column == 27 && eventNum == 171) // Z2
                    {
                        return true;
                    }
                    else if (row == 7 && column == 29 && eventNum == 172) // Z2
                    {
                        return true;
                    }
                    else if (row == 8 && column == 30 && eventNum == 173) // Z2
                    {
                        return true;
                    }
                    else if (row == 10 && column == 30 && eventNum == 174) // Z2
                    {
                        return true;
                    }
                    else if (row == 12 && column == 30 && eventNum == 175) // Z2
                    {
                        return true;
                    }
                    // ５Ｘ-Ｚ３
                    else if (row == 35 && column == 21 && eventNum == 176) // Z3
                    {
                        return true;
                    }
                    else if (row == 35 && column == 23 && eventNum == 177) // Z3
                    {
                        return true;
                    }
                    else if (row == 35 && column == 25 && eventNum == 178) // Z3
                    {
                        return true;
                    }
                    else if (row == 35 && column == 27 && eventNum == 179) // Z3
                    {
                        return true;
                    }
                    else if (row == 37 && column == 22 && eventNum == 180) // Z3
                    {
                        return true;
                    }
                    else if (row == 37 && column == 24 && eventNum == 181) // Z3
                    {
                        return true;
                    }
                    else if (row == 37 && column == 26 && eventNum == 182) // Z3
                    {
                        return true;
                    }
                    // ５Ｘ-Ｚ４
                    else if (row == 5 && column == 50 && eventNum == 183) // Z4
                    {
                        return true;
                    }
                    else if (row == 4 && column == 44 && eventNum == 184) // Z4
                    {
                        return true;
                    }
                    else if (row == 9 && column == 51 && eventNum == 185) // Z4
                    {
                        return true;
                    }
                    else if (row == 6 && column == 46 && eventNum == 186) // Z4
                    {
                        return true;
                    }
                    else if (row == 26 && column == 40 && eventNum == 187) // Z4
                    {
                        return true;
                    }
                    else if (row == 35 && column == 42 && eventNum == 188) // Z4
                    {
                        return true;
                    }
                    else if (row == 27 && column == 53 && eventNum == 189) // Z4
                    {
                        return true;
                    }
                    else if (row == 37 && column == 53 && eventNum == 190) // Z4
                    {
                        return true;
                    }
                    // Final、無限、１
                    else if (row == 35 && column == 55 && eventNum == 191) // 11
                    {
                        return true;
                    }
                    else if (row == 35 && column == 56 && eventNum == 192) // 12
                    {
                        return true;
                    }
                    else if (row == 35 && column == 57 && eventNum == 193) // 13
                    {
                        return true;
                    }
                    else if (row == 35 && column == 58 && eventNum == 194) // 14
                    {
                        return true;
                    }
                    else if (row == 35 && column == 59 && eventNum == 195) // 15
                    {
                        return true;
                    }
                    // Final、無限、２
                    else if (row == 32 && column == 55 && eventNum == 196) // 21
                    {
                        return true;
                    }
                    else if (row == 32 && column == 56 && eventNum == 197) // 22
                    {
                        return true;
                    }
                    else if (row == 32 && column == 57 && eventNum == 198) // 23
                    {
                        return true;
                    }
                    else if (row == 32 && column == 58 && eventNum == 199) // 24
                    {
                        return true;
                    }
                    else if (row == 32 && column == 59 && eventNum == 200) // 25
                    {
                        return true;
                    }
                    // Final、無限、３
                    else if (row == 29 && column == 55 && eventNum == 201) // 31
                    {
                        return true;
                    }
                    else if (row == 29 && column == 56 && eventNum == 202) // 32
                    {
                        return true;
                    }
                    else if (row == 29 && column == 57 && eventNum == 203) // 33
                    {
                        return true;
                    }
                    else if (row == 29 && column == 58 && eventNum == 204) // 34
                    {
                        return true;
                    }
                    else if (row == 29 && column == 59 && eventNum == 205) // 35
                    {
                        return true;
                    }
                    // Final、無限、４
                    else if (row == 26 && column == 55 && eventNum == 206) // 41
                    {
                        return true;
                    }
                    else if (row == 26 && column == 56 && eventNum == 207) // 42
                    {
                        return true;
                    }
                    else if (row == 26 && column == 57 && eventNum == 208) // 43
                    {
                        return true;
                    }
                    else if (row == 26 && column == 58 && eventNum == 209) // 44
                    {
                        return true;
                    }
                    else if (row == 26 && column == 59 && eventNum == 210) // 45
                    {
                        return true;
                    }
                    // Final、無限、５
                    else if (row == 23 && column == 55 && eventNum == 211) // 51
                    {
                        return true;
                    }
                    else if (row == 23 && column == 56 && eventNum == 212) // 52
                    {
                        return true;
                    }
                    else if (row == 23 && column == 57 && eventNum == 213) // 53
                    {
                        return true;
                    }
                    else if (row == 23 && column == 58 && eventNum == 214) // 54
                    {
                        return true;
                    }
                    else if (row == 23 && column == 59 && eventNum == 215) // 55
                    {
                        return true;
                    }
                    // Final、無限、６
                    else if (row == 20 && column == 55 && eventNum == 216) // 61
                    {
                        return true;
                    }
                    else if (row == 20 && column == 56 && eventNum == 217) // 62
                    {
                        return true;
                    }
                    else if (row == 20 && column == 57 && eventNum == 218) // 63
                    {
                        return true;
                    }
                    else if (row == 20 && column == 58 && eventNum == 219) // 64
                    {
                        return true;
                    }
                    else if (row == 20 && column == 59 && eventNum == 220) // 65
                    {
                        return true;
                    }
                    // Final、無限、７
                    else if (row == 17 && column == 55 && eventNum == 221) // 71
                    {
                        return true;
                    }
                    else if (row == 17 && column == 56 && eventNum == 222) // 72
                    {
                        return true;
                    }
                    else if (row == 17 && column == 57 && eventNum == 223) // 73
                    {
                        return true;
                    }
                    else if (row == 17 && column == 58 && eventNum == 224) // 74
                    {
                        return true;
                    }
                    else if (row == 17 && column == 59 && eventNum == 225) // 75
                    {
                        return true;
                    }
                    // Final、無限、８
                    else if (row == 14 && column == 55 && eventNum == 226) // 81
                    {
                        return true;
                    }
                    else if (row == 14 && column == 56 && eventNum == 227) // 82
                    {
                        return true;
                    }
                    else if (row == 14 && column == 57 && eventNum == 228) // 83
                    {
                        return true;
                    }
                    else if (row == 14 && column == 58 && eventNum == 229) // 84
                    {
                        return true;
                    }
                    else if (row == 14 && column == 59 && eventNum == 230) // 85
                    {
                        return true;
                    }
                    // Final、無限、９
                    else if (row == 11 && column == 55 && eventNum == 231) // 91
                    {
                        return true;
                    }
                    else if (row == 11 && column == 56 && eventNum == 232) // 92
                    {
                        return true;
                    }
                    else if (row == 11 && column == 57 && eventNum == 233) // 93
                    {
                        return true;
                    }
                    else if (row == 11 && column == 58 && eventNum == 234) // 94
                    {
                        return true;
                    }
                    else if (row == 11 && column == 59 && eventNum == 235) // 95
                    {
                        return true;
                    }
                    // Final、無限、１０
                    else if (row == 8 && column == 55 && eventNum == 236) // 101
                    {
                        return true;
                    }
                    else if (row == 8 && column == 56 && eventNum == 237) // 102
                    {
                        return true;
                    }
                    else if (row == 8 && column == 57 && eventNum == 238) // 103
                    {
                        return true;
                    }
                    else if (row == 8 && column == 58 && eventNum == 239) // 104
                    {
                        return true;
                    }
                    else if (row == 8 && column == 59 && eventNum == 240) // 105
                    {
                        return true;
                    }
                    // Final、無限、１１
                    else if (row == 5 && column == 55 && eventNum == 241) // 111
                    {
                        return true;
                    }
                    else if (row == 5 && column == 56 && eventNum == 242) // 112
                    {
                        return true;
                    }
                    else if (row == 5 && column == 57 && eventNum == 243) // 113
                    {
                        return true;
                    }
                    else if (row == 5 && column == 58 && eventNum == 244) // 114
                    {
                        return true;
                    }
                    else if (row == 5 && column == 59 && eventNum == 245) // 115
                    {
                        return true;
                    }
                    // Final、無限、１２
                    else if (row == 2 && column == 55 && eventNum == 246) // 121
                    {
                        return true;
                    }
                    else if (row == 2 && column == 56 && eventNum == 247) // 122
                    {
                        return true;
                    }
                    else if (row == 2 && column == 57 && eventNum == 248) // 123
                    {
                        return true;
                    }
                    else if (row == 2 && column == 58 && eventNum == 249) // 124
                    {
                        return true;
                    }
                    else if (row == 2 && column == 59 && eventNum == 250) // 125
                    {
                        return true;
                    }
                    // ボス
                    else if (row == 39 && column == 55 && eventNum == 251)
                    {
                        return true;
                    }
                    // 鏡エリア２の看板
                    else if (row == 16 && column == 24 && eventNum == 252)
                    {
                        return true;
                    }
                    // 宝箱
                    else if (row == 0 && column == 3 && !GroundOne.WE.TruthTreasure301 && eventNum == 253)
                    {
                        return true;
                    }
                    else if (row == 39 && column == 3 && !GroundOne.WE.TruthTreasure302 && eventNum == 254)
                    {
                        return true;
                    }
                    else if (row == 4 && column == 19 && !GroundOne.WE.TruthTreasure303 && eventNum == 255)
                    {
                        return true;
                    }
                    else if (row == 24 && column == 19 && !GroundOne.WE.TruthTreasure304 && eventNum == 256)
                    {
                        return true;
                    }
                    else if (row == 9 && column == 1 && !GroundOne.WE.TruthTreasure305 && eventNum == 257)
                    {
                        return true;
                    }
                    else if (row == 24 && column == 2 && !GroundOne.WE.TruthTreasure306 && eventNum == 258)
                    {
                        return true;
                    }
                    else if (row == 33 && column == 47 && !GroundOne.WE.TruthTreasure307 && eventNum == 259)
                    {
                        return true;
                    }
                    else if (row == 7 && column == 20 && !GroundOne.WE.TruthTreasure308 && eventNum == 260)
                    {
                        return true;
                    }
                    else if (row == 17 && column == 45 && !GroundOne.WE.TruthTreasure309 && eventNum == 261)
                    {
                        return true;
                    }
                    else if (row == 38 && column == 31 && !GroundOne.WE.TruthTreasure310 && eventNum == 262)
                    {
                        return true;
                    }
                    else if (row == 27 && column == 48 && !GroundOne.WE.TruthTreasure311 && eventNum == 263)
                    {
                        return true;
                    }
                    else if (row == 34 && column == 40 && !GroundOne.WE.TruthTreasure312 && eventNum == 264)
                    {
                        return true;
                    }
                    // area2 看板X1
                    else if (row == 21 && column == 47 && eventNum == 265)
                    {
                        return true;
                    }
                    // area2 看板X2
                    else if (row == 10 && column == 27 && eventNum == 266)
                    {
                        return true;
                    }
                    // area2 看板X3
                    else if (row == 37 && column == 20 && eventNum == 267)
                    {
                        return true;
                    }
                    // area2 看板X4
                    else if (row == 16 && column == 43 && eventNum == 268)
                    {
                        return true;
                    }
                    // area2 看板X5
                    else if (row == 19 && column == 37 && eventNum == 269)
                    {
                        return true;
                    }
                    // area2 看板X6
                    else if (row == 7 && column == 37 && eventNum == 270)
                    {
                        return true;
                    }
                    // area2 看板前の扉
                    else if (row == 39 && column == 54 && eventNum == 271)
                    {
                        return true;
                    }
                    // ４階への階段
                    else if (row == 39 && column == 59 && eventNum == 272)
                    {
                        return true;
                    }
                    // ボス横の看板
                    else if (row == 39 && column == 56 && eventNum == 273)
                    {
                        return true;
                    }
                    // 鏡エリア２－５帰り道
                    else if (row == 1 && column == 21 && eventNum == 274)
                    {
                        return true;
                    }
                    // 鏡エリア２－原点解帰り道
                    else if (row == 12 && column == 37 && eventNum == 275)
                    {
                        return true;
                    }
                    // 鏡エリア２－原点解、聖者
                    else if (row == 5 && column == 36 && eventNum == 276)
                    {
                        return true;
                    }
                    // 鏡エリア２－原点解、愚者
                    else if (row == 5 && column == 38 && eventNum == 277)
                    {
                        return true;
                    }
                    // ４階への階段（２）
                    else if (row == 0 && column == 59 && eventNum == 278)
                    {
                        return true;
                    }
                    // 無限回廊突破後の看板
                    else if (row == 1 && column == 55 && eventNum == 279)
                    {
                        return true;
                    }
                    // 真実解のイベント２
                    else if (row == 25 && column == 1 && eventNum == 280)
                    {
                        return true;
                    }
                    else if (row == 22 && column == 1 && eventNum == 281)
                    {
                        return true;
                    }
                    // 真実解のイベント３
                    else if (row == 14 && column == 1 && eventNum == 282)
                    {
                        return true;
                    }
                    else if (row == 17 && column == 1 && eventNum == 283)
                    {
                        return true;
                    }
                    // 真実解のイベント４
                    else if (row == 4 && column == 1 && eventNum == 284)
                    {
                        return true;
                    }
                    else if (row == 8 && column == 1 && eventNum == 285)
                    {
                        return true;
                    }
                    break;
                #endregion
                #region "４階"
                case 4:
                    #region "エリア１"
                    // 上り階段
                    if (row == 18 && column == 52 && eventNum == 0)
                    {
                        return true;
                    }
                    // 扉１
                    if (row == 18 && column == 49 && eventNum == 1)
                    {
                        return true;
                    }
                    // 看板１
                    if (row == 19 && column == 46 && eventNum == 2)
                    {
                        return true;
                    }
                    // 看板１-１
                    if (row == 12 && column == 44 && eventNum == 3)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-１）
                    if (row == 15 && column == 43 && !GroundOne.WE.TruthTreasure401 && eventNum == 4)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-２）
                    if (row == 12 && column == 42 && !GroundOne.WE.TruthTreasure402 && eventNum == 5)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-３）
                    if (row == 0 && column == 44 && !GroundOne.WE.TruthTreasure403 && eventNum == 6)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-４）
                    if (row == 7 && column == 29 && !GroundOne.WE.TruthTreasure404 && eventNum == 7)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-５）
                    if (row == 12 && column == 29 && !GroundOne.WE.TruthTreasure405 && eventNum == 8)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-６）
                    if (row == 3 && column == 46 && !GroundOne.WE.TruthTreasure406 && eventNum == 9)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-７）
                    if (row == 10 && column == 59 && !GroundOne.WE.TruthTreasure407 && eventNum == 10)
                    {
                        return true;
                    }
                    // 宝箱（エリア１-８）
                    if (row == 9 && column == 52 && !GroundOne.WE.TruthTreasure408 && eventNum == 11)
                    {
                        return true;
                    }
                    // 鍵（エリア１-１）
                    if (row == 16 && column == 46 && eventNum == 12)
                    {
                        return true;
                    }
                    // 鍵（エリア１-２）
                    if (row == 11 && column == 47 && eventNum == 13)
                    {
                        return true;
                    }
                    // 鍵（エリア１-３）
                    if (row == 8 && column == 37 && eventNum == 14)
                    {
                        return true;
                    }
                    // 鍵（エリア１-４）
                    if (row == 4 && column == 32 && eventNum == 15)
                    {
                        return true;
                    }
                    // 鍵（エリア１-５）
                    if (row == 10 && column == 30 && eventNum == 16)
                    {
                        return true;
                    }
                    // 鍵（エリア１-６）
                    if (row == 13 && column == 40 && eventNum == 17)
                    {
                        return true;
                    }
                    // 鍵（エリア１-７）
                    if (row == 1 && column == 51 && eventNum == 18)
                    {
                        return true;
                    }
                    // 鍵（エリア１-８）
                    if (row == 4 && column == 56 && eventNum == 19)
                    {
                        return true;
                    }
                    // 鍵（エリア１-９）
                    if (row == 16 && column == 54 && eventNum == 20)
                    {
                        return true;
                    }
                    // 扉（エリア１-１）
                    if (row == 11 && column == 43 && eventNum == 21)
                    {
                        return true;
                    }
                    // 扉（エリア１-２）
                    if (row == 6 && column == 45 && eventNum == 22)
                    {
                        return true;
                    }
                    // 扉（エリア１-３）
                    if (row == 4 && column == 36 && eventNum == 23)
                    {
                        return true;
                    }
                    // 扉（エリア１-４）
                    if (row == 3 && column == 31 && eventNum == 24)
                    {
                        return true;
                    }
                    // 扉（エリア１-５）
                    if (row == 12 && column == 34 && eventNum == 25)
                    {
                        return true;
                    }
                    // 扉（エリア１-６）
                    if (row == 6 && column == 47 && eventNum == 26)
                    {
                        return true;
                    }
                    // 扉（エリア１-７）
                    if (row == 3 && column == 53 && eventNum == 27)
                    {
                        return true;
                    }
                    // 扉（エリア１-８）
                    if (row == 1 && column == 56 && eventNum == 28)
                    {
                        return true;
                    }
                    // 扉（エリア１-９）
                    if (row == 11 && column == 51 && eventNum == 29)
                    {
                        return true;
                    }
                    // 真実の回想１-１
                    if (row == 5 && column == 42 && eventNum == 30)
                    {
                        return true;
                    }
                    // 真実の回想１-２
                    if (row == 16 && column == 37 && eventNum == 31)
                    {
                        return true;
                    }
                    // 真実の回想１-３
                    if (row == 13 && column == 57 && eventNum == 32)
                    {
                        return true;
                    }
                    // 真実の回想２-１
                    if (row == 4 && column == 5 && eventNum == 33)
                    {
                        return true;
                    }
                    // 真実の回想２-２
                    if (row == 5 && column == 14 && eventNum == 34)
                    {
                        return true;
                    }
                    // 真実の回想２-３
                    if (row == 15 && column == 14 && eventNum == 35)
                    {
                        return true;
                    }
                    // 真実の回想３-１
                    if (row == 28 && column == 3 && eventNum == 36)
                    {
                        return true;
                    }
                    // 真実の回想３-２
                    if (row == 37 && column == 25 && eventNum == 37)
                    {
                        return true;
                    }
                    // 真実の回想４-１
                    if (row == 36 && column == 54 && eventNum == 38)
                    {
                        return true;
                    }
                    // 真実の回想４-２
                    if (row == 25 && column == 50 && eventNum == 39)
                    {
                        return true;
                    }
                    // 看板１-２
                    if (row == 7 && column == 46 && eventNum == 40)
                    {
                        return true;
                    }
                    // 看板１-３
                    if (row == 3 && column == 37 && eventNum == 41)
                    {
                        return true;
                    }
                    // 看板１-４
                    if (row == 2 && column == 30 && eventNum == 42)
                    {
                        return true;
                    }
                    // 看板１-５
                    if (row == 11 && column == 35 && eventNum == 43)
                    {
                        return true;
                    }
                    // 看板１-６
                    if (row == 4 && column == 52 && eventNum == 44)
                    {
                        return true;
                    }
                    // 看板１-７
                    if (row == 2 && column == 57 && eventNum == 45)
                    {
                        return true;
                    }
                    // 看板１-８
                    if (row == 10 && column == 50 && eventNum == 46)
                    {
                        return true;
                    }
                    // エリア１ボス前
                    if (row == 14 && column == 47 && eventNum == 47)
                    {
                        return true;
                    }
                    // エリア１ボス
                    if (row == 16 && column == 47 && eventNum == 48)
                    {
                        return true;
                    }
                    // エリア１から２への通路
                    if (row == 19 && column == 44 && eventNum == 49)
                    {
                        return true;
                    }
                    // エリア２スタート
                    if (row == 19 && column == 23 && eventNum == 50)
                    {
                        return true;
                    }
                    #endregion
                    #region "エリア２"
                    // エリア２看板
                    if (row == 19 && column == 20 && eventNum == 51)
                    {
                        return true;
                    }
                    // エリア２-１
                    if (row == 8 && column == 11 && eventNum == 52)
                    {
                        return true;
                    }
                    // 鍵Ｘ-１
                    if (row == 6 && column == 1 && eventNum == 53)
                    {
                        return true;
                    }
                    // 鍵Ｘ-２
                    if (row == 10 && column == 10 && eventNum == 54)
                    {
                        return true;
                    }
                    // 鍵Ｘ-３
                    if (row == 15 && column == 1 && eventNum == 55)
                    {
                        return true;
                    }
                    // 鍵Ｘ-４
                    if (row == 18 && column == 8 && eventNum == 56)
                    {
                        return true;
                    }
                    // 鍵Ｘ-５
                    if (row == 23 && column == 18 && eventNum == 57)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１
                    if (row == 7 && column == 0 && !GroundOne.WE.TruthTreasure409 && eventNum == 58)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-２
                    if (row == 9 && column == 3 && !GroundOne.WE.TruthTreasure410 && eventNum == 59)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-３
                    if (row == 11 && column == 1 && !GroundOne.WE.TruthTreasure411 && eventNum == 60)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-４
                    if (row == 12 && column == 5 && !GroundOne.WE.TruthTreasure412 && eventNum == 61)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-５
                    if (row == 13 && column == 8 && !GroundOne.WE.TruthTreasure413 && eventNum == 62)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-６
                    if (row == 15 && column == 7 && !GroundOne.WE.TruthTreasure414 && eventNum == 63)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-７
                    if (row == 20 && column == 4 && !GroundOne.WE.TruthTreasure415 && eventNum == 64)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-８
                    if (row == 21 && column == 13 && !GroundOne.WE.TruthTreasure416 && eventNum == 65)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-９
                    if (row == 24 && column == 2 && !GroundOne.WE.TruthTreasure417 && eventNum == 66)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１０
                    if (row == 23 && column == 3 && !GroundOne.WE.TruthTreasure418 && eventNum == 67)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１１
                    if (row == 23 && column == 7 && !GroundOne.WE.TruthTreasure419 && eventNum == 68)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１２
                    if (row == 24 && column == 10 && !GroundOne.WE.TruthTreasure420 && eventNum == 69)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１３
                    if (row == 23 && column == 13 && !GroundOne.WE.TruthTreasure421 && eventNum == 70)
                    {
                        return true;
                    }
                    // 宝箱Ｘ-１４
                    if (row == 20 && column == 14 && !GroundOne.WE.TruthTreasure422 && eventNum == 71)
                    {
                        return true;
                    }
                    // 扉Ｘ１
                    if (row == 8 && column == 6 && eventNum == 72)
                    {
                        return true;
                    }
                    // 扉Ｘ２
                    if (row == 7 && column == 10 && eventNum == 73)
                    {
                        return true;
                    }
                    // 鍵Ｙ-１
                    if (row == 0 && column == 0 && eventNum == 74)
                    {
                        return true;
                    }
                    // 鍵Ｙ-２
                    if (row == 7 && column == 13 && eventNum == 75)
                    {
                        return true;
                    }
                    // 鍵Ｙ-３
                    if (row == 0 && column == 18 && eventNum == 76)
                    {
                        return true;
                    }
                    // 鍵Ｙ-４
                    if (row == 7 && column == 28 && eventNum == 77)
                    {
                        return true;
                    }
                    // 鍵Ｙ-５
                    if (row == 13 && column == 22 && eventNum == 78)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-１
                    if (row == 6 && column == 8 && !GroundOne.WE.TruthTreasure423 && eventNum == 79)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-２
                    if (row == 3 && column == 10 && !GroundOne.WE.TruthTreasure424 && eventNum == 80)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-３
                    if (row == 0 && column == 5 && !GroundOne.WE.TruthTreasure425 && eventNum == 81)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-４
                    if (row == 0 && column == 12 && !GroundOne.WE.TruthTreasure426 && eventNum == 82)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-５
                    if (row == 0 && column == 27 && !GroundOne.WE.TruthTreasure427 && eventNum == 83)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-６
                    if (row == 2 && column == 27 && !GroundOne.WE.TruthTreasure428 && eventNum == 84)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-７
                    if (row == 11 && column == 27 && !GroundOne.WE.TruthTreasure429 && eventNum == 85)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-８
                    if (row == 13 && column == 25 && !GroundOne.WE.TruthTreasure430 && eventNum == 86)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-９
                    if (row == 4 && column == 18 && !GroundOne.WE.TruthTreasure431 && eventNum == 87)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-１０
                    if (row == 2 && column == 23 && !GroundOne.WE.TruthTreasure432 && eventNum == 88)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-１１
                    if (row == 6 && column == 24 && !GroundOne.WE.TruthTreasure433 && eventNum == 89)
                    {
                        return true;
                    }
                    // 宝箱Ｙ-１２
                    if (row == 6 && column == 18 && !GroundOne.WE.TruthTreasure434 && eventNum == 90)
                    {
                        return true;
                    }
                    // 扉Ｙ１
                    if (row == 2 && column == 11 && eventNum == 91)
                    {
                        return true;
                    }
                    // 扉Ｙ２
                    if (row == 7 && column == 12 && eventNum == 92)
                    {
                        return true;
                    }
                    // 鍵Ｚ-１
                    if (row == 9 && column == 13 && eventNum == 93)
                    {
                        return true;
                    }
                    // 鍵Ｚ-２
                    if (row == 9 && column == 18 && eventNum == 94)
                    {
                        return true;
                    }
                    // 鍵Ｚ-３
                    if (row == 14 && column == 22 && eventNum == 95)
                    {
                        return true;
                    }
                    // 鍵Ｚ-４
                    if (row == 17 && column == 23 && eventNum == 96)
                    {
                        return true;
                    }
                    // 鍵Ｚ-５
                    if (row == 15 && column == 27 && eventNum == 97)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-１
                    if (row == 9 && column == 15 && !GroundOne.WE.TruthTreasure435 && eventNum == 98)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-２
                    if (row == 10 && column == 17 && !GroundOne.WE.TruthTreasure436 && eventNum == 99)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-３
                    if (row == 10 && column == 21 && !GroundOne.WE.TruthTreasure437 && eventNum == 100)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-４
                    if (row == 14 && column == 17 && !GroundOne.WE.TruthTreasure438 && eventNum == 101)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-５
                    if (row == 12 && column == 20 && !GroundOne.WE.TruthTreasure439 && eventNum == 102)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-６
                    if (row == 14 && column == 20 && !GroundOne.WE.TruthTreasure440 && eventNum == 103)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-７
                    if (row == 15 && column == 23 && !GroundOne.WE.TruthTreasure441 && eventNum == 104)
                    {
                        return true;
                    }
                    // 宝箱Ｚ-８
                    if (row == 17 && column == 27 && !GroundOne.WE.TruthTreasure442 && eventNum == 105)
                    {
                        return true;
                    }
                    // 扉Ｚ１
                    if (row == 13 && column == 16 && eventNum == 106)
                    {
                        return true;
                    }
                    // エリア２ボス前
                    if (row == 16 && column == 17 && eventNum == 107)
                    {
                        return true;
                    }
                    // エリア２ボス
                    if (row == 16 && column == 20 && eventNum == 108)
                    {
                        return true;
                    }
                    // エリア２から３への通路
                    if (row == 21 && column == 20 && eventNum == 109)
                    {
                        return true;
                    }
                    #endregion
                    #region "エリア３"
                    // エリア３スタート
                    if (row == 32 && column == 20 && eventNum == 110)
                    {
                        return true;
                    }
                    // エリア３看板
                    if (row == 34 && column == 20 && eventNum == 111)
                    {
                        return true;
                    }
                    // エリア３-１（始まり）
                    if (row == 27 && column == 16 && eventNum == 112)
                    {
                        return true;
                    }
                    // エリア３-１（看板１）
                    if (row == 26 && column == 16 && eventNum == 113)
                    {
                        return true;
                    }
                    // エリア３-１（事実１）
                    if (row == 27 && column == 9 && eventNum == 114)
                    {
                        return true;
                    }
                    // エリア３-１（事実２）
                    if (row == 28 && column == 11 && eventNum == 115)
                    {
                        return true;
                    }
                    // エリア３-１（事実３）
                    if (row == 29 && column == 14 && eventNum == 116)
                    {
                        return true;
                    }
                    // エリア３-１（事実４）
                    if (row == 31 && column == 6 && eventNum == 117)
                    {
                        return true;
                    }
                    // エリア３-１（事実５）
                    if (row == 31 && column == 8 && eventNum == 118)
                    {
                        return true;
                    }
                    // エリア３-１（事実６）
                    if (row == 33 && column == 8 && eventNum == 119)
                    {
                        return true;
                    }
                    // エリア３-１（事実７）
                    if (row == 33 && column == 10 && eventNum == 120)
                    {
                        return true;
                    }
                    // エリア３-１（事実８）
                    if (row == 33 && column == 11 && eventNum == 121)
                    {
                        return true;
                    }
                    // エリア３-１（事実９）
                    if (row == 34 && column == 14 && eventNum == 122)
                    {
                        return true;
                    }
                    // エリア３-１（事実１０）
                    if (row == 34 && column == 17 && eventNum == 123)
                    {
                        return true;
                    }
                    // エリア３-２への扉
                    if (row == 25 && column == 7 && eventNum == 124)
                    {
                        return true;
                    }
                    // エリア３-２開始
                    if (row == 32 && column == 2 && eventNum == 125)
                    {
                        return true;
                    }
                    // エリア３-２看板
                    if (row == 31 && column == 2 && eventNum == 126)
                    {
                        return true;
                    }
                    // エリア３-２（真実１）
                    if (row == 33 && column == 0 && eventNum == 127)
                    {
                        return true;
                    }
                    // エリア３-２（真実２）
                    if (row == 33 && column == 5 && eventNum == 128)
                    {
                        return true;
                    }
                    // エリア３-２（真実３）
                    if (row == 39 && column == 1 && eventNum == 129)
                    {
                        return true;
                    }
                    // エリア３-２（真実４）
                    if (row == 37 && column == 4 && eventNum == 130)
                    {
                        return true;
                    }
                    // エリア３-２（真実５）
                    if (row == 32 && column == 6 && eventNum == 131)
                    {
                        return true;
                    }
                    // エリア３-２（真実６）
                    if (row == 39 && column == 7 && eventNum == 132)
                    {
                        return true;
                    }
                    // エリア３-２（真実７）
                    if (row == 34 && column == 8 && eventNum == 133)
                    {
                        return true;
                    }
                    // エリア３-２（真実８）
                    if (row == 35 && column == 11 && eventNum == 134)
                    {
                        return true;
                    }
                    // エリア３-２（真実９）
                    if (row == 36 && column == 14 && eventNum == 135)
                    {
                        return true;
                    }
                    // エリア３-２（真実１０）
                    if (row == 37 && column == 18 && eventNum == 136)
                    {
                        return true;
                    }
                    // 宝箱エリア３-１
                    if (row == 29 && column == 7 && !GroundOne.WE.TruthTreasure443 && eventNum == 137)
                    {
                        return true;
                    }
                    // 宝箱エリア３-２
                    if (row == 35 && column == 10 && !GroundOne.WE.TruthTreasure444 && eventNum == 138)
                    {
                        return true;
                    }
                    // 宝箱エリア３-３
                    if (row == 33 && column == 14 && !GroundOne.WE.TruthTreasure445 && eventNum == 139)
                    {
                        return true;
                    }
                    // 宝箱エリア３-４
                    if (row == 36 && column == 3 && !GroundOne.WE.TruthTreasure446 && eventNum == 140)
                    {
                        return true;
                    }
                    // 宝箱エリア３-５
                    if (row == 34 && column == 5 && !GroundOne.WE.TruthTreasure447 && eventNum == 141)
                    {
                        return true;
                    }
                    // 宝箱エリア３-６
                    if (row == 39 && column == 10 && !GroundOne.WE.TruthTreasure448 && eventNum == 142)
                    {
                        return true;
                    }
                    // 宝箱エリア３-７
                    if (row == 36 && column == 12 && !GroundOne.WE.TruthTreasure449 && eventNum == 143)
                    {
                        return true;
                    }
                    // 宝箱エリア３-８
                    if (row == 39 && column == 14 && !GroundOne.WE.TruthTreasure450 && eventNum == 144)
                    {
                        return true;
                    }
                    // エリア３－２終了の扉
                    if (row == 39 && column == 22 && eventNum == 145)
                    {
                        return true;
                    }
                    // エリア３ボス前
                    if (row == 38 && column == 23 && eventNum == 146)
                    {
                        return true;
                    }
                    // エリア３ボス
                    if (row == 35 && column == 23 && eventNum == 147)
                    {
                        return true;
                    }
                    // エリア３から４への通路
                    if (row == 34 && column == 22 && eventNum == 148)
                    {
                        return true;
                    }

                    #endregion
                    #region "エリア４"
                    // エリア４スタート
                    if (row == 34 && column == 44 && eventNum == 149)
                    {
                        return true;
                    }
                    // エリア４看板
                    if (row == 34 && column == 46 && eventNum == 150)
                    {
                        return true;
                    }
                    // エリア４-１看板広間
                    if (row == 37 && column == 43 && eventNum == 151)
                    {
                        return true;
                    }
                    // エリア４-１看板
                    if (row == 37 && column == 42 && eventNum == 152)
                    {
                        return true;
                    }
                    // 鍵４-１
                    if (row == 35 && column == 40 && eventNum == 153)
                    {
                        return true;
                    }
                    // 鍵４-２
                    if (row == 36 && column == 33 && eventNum == 154)
                    {
                        return true;
                    }
                    // 鍵４-３
                    if (row == 37 && column == 31 && eventNum == 155)
                    {
                        return true;
                    }
                    // 鍵４-４
                    if (row == 37 && column == 39 && eventNum == 156)
                    {
                        return true;
                    }
                    // 鍵４-５
                    if (row == 37 && column == 35 && eventNum == 157)
                    {
                        return true;
                    }
                    // 宝箱４-１
                    if (row == 36 && column == 37 && !GroundOne.WE.TruthTreasure451 && eventNum == 158)
                    {
                        return true;
                    }
                    // 宝箱４-２
                    if (row == 36 && column == 29 && !GroundOne.WE.TruthTreasure452 && eventNum == 159)
                    {
                        return true;
                    }
                    // 宝箱４-３
                    if (row == 39 && column == 39 && !GroundOne.WE.TruthTreasure453 && eventNum == 160)
                    {
                        return true;
                    }
                    // 宝箱４-４
                    if (row == 39 && column == 41 && !GroundOne.WE.TruthTreasure454 && eventNum == 161)
                    {
                        return true;
                    }
                    // エリア４-１扉
                    if (row == 39 && column == 47 && eventNum == 162)
                    {
                        return true;
                    }
                    // エリア４-１終了
                    if (row == 35 && column == 51 && eventNum == 163)
                    {
                        return true;
                    }
                    // エリア４-２看板
                    if (row == 34 && column == 52 && eventNum == 164)
                    {
                        return true;
                    }
                    // 鍵４-２-１
                    if (row == 31 && column == 50 && eventNum == 165)
                    {
                        return true;
                    }
                    // 鍵４-２-２
                    if (row == 33 && column == 53 && eventNum == 166)
                    {
                        return true;
                    }
                    // 鍵４-２-３
                    if (row == 34 && column == 58 && eventNum == 167)
                    {
                        return true;
                    }
                    // 鍵４-２-４
                    if (row == 30 && column == 56 && eventNum == 168)
                    {
                        return true;
                    }
                    // 鍵４-２-５
                    if (row == 31 && column == 58 && eventNum == 169)
                    {
                        return true;
                    }
                    // 鍵４-２-６
                    if (row == 27 && column == 55 && eventNum == 170)
                    {
                        return true;
                    }
                    // 鍵４-２-７
                    if (row == 24 && column == 55 && eventNum == 171)
                    {
                        return true;
                    }
                    // 鍵４-２-８
                    if (row == 24 && column == 58 && eventNum == 172)
                    {
                        return true;
                    }
                    // 宝箱４-２-１
                    if (row == 32 && column == 55 && !GroundOne.WE.TruthTreasure455 && eventNum == 173)
                    {
                        return true;
                    }
                    // 宝箱４-２-２
                    if (row == 31 && column == 56 && !GroundOne.WE.TruthTreasure456 && eventNum == 174)
                    {
                        return true;
                    }
                    // 宝箱４-２-３
                    if (row == 28 && column == 59 && !GroundOne.WE.TruthTreasure457 && eventNum == 175)
                    {
                        return true;
                    }
                    // 宝箱４-２-４
                    if (row == 22 && column == 59 && !GroundOne.WE.TruthTreasure458 && eventNum == 176)
                    {
                        return true;
                    }
                    // 宝箱４-２-５
                    if (row == 21 && column == 50 && !GroundOne.WE.TruthTreasure459 && eventNum == 177)
                    {
                        return true;
                    }
                    // エリア４-２扉
                    if (row == 22 && column == 49 && eventNum == 178)
                    {
                        return true;
                    }
                    // エリア４-２終了
                    if (row == 31 && column == 48 && eventNum == 179)
                    {
                        return true;
                    }
                    // エリア４から１への通路
                    if (row == 32 && column == 46 && eventNum == 180)
                    {
                        return true;
                    }
                    // そして、現実世界へ
                    if (row == 31 && column == 46 && eventNum == 181)
                    {
                        return true;
                    }
                    // 究極の二択
                    if (row == 22 && column == 46 && eventNum == 182)
                    {
                        return true;
                    }
                    // 最下層への階段前の扉
                    if (row == 20 && column == 48 && eventNum == 183)
                    {
                        return true;
                    }
                    // 下り階段
                    if (row == 20 && column == 52 && eventNum == 184)
                    {
                        return true;
                    }
                    // 現実世界、移動ブロック
                    if (row == 23 && column == 46 && eventNum == 185)
                    {
                        return true;
                    }
                    if (row == 16 && column == 45 && eventNum == 186)
                    {
                        return true;
                    }
                    if (row == 17 && column == 47 && eventNum == 187)
                    {
                        return true;
                    }
                    if (row == 18 && column == 48 && eventNum == 188)
                    {
                        return true;
                    }
                    #endregion
                    #region "エリア３、ラナ消失後、戻りブロック"
                    if (row == 31 && column == 20 && eventNum == 189)
                    {
                        return true;
                    }
                    #endregion
                    break;
                #endregion
                #region "５階"
                case 5:
                    // 上り階段
                    if (row == 2 && column == 57 && eventNum == 0)
                    {
                        return true;
                    }
                    // 細い通路の始まり
                    if (row == 5 && column == 57 && eventNum == 1)
                    {
                        return true;
                    }
                    // 大通路の始まり
                    if (row == 29 && column == 57 && eventNum == 2)
                    {
                        return true;
                    }
                    // 大通路の中間１
                    if (((row == 30 && column == 44) ||
                        (row == 31 && column == 44) ||
                        (row == 32 && column == 44) ||
                        (row == 33 && column == 44) ||
                        (row == 34 && column == 44))
                        && eventNum == 3)
                    {
                        return true;
                    }
                    // 大通路の中間２
                    if (((row == 31 && column == 32) ||
                         (row == 32 && column == 32) ||
                         (row == 33 && column == 32))
                        && eventNum == 4)
                    {
                        return true;
                    }
                    // 大通路の終わり
                    if (row == 32 && column == 20 && eventNum == 5)
                    {
                        return true;
                    }
                    // ボス
                    if (row == 32 && column == 9 && eventNum == 6)
                    {
                        return true;
                    }
                    // ホログラムによるパーティ編成
                    if (row == 32 && column == 11 && eventNum == 7)
                    {
                        return true;
                    }
                    // 真実世界への入り口
                    if (row == 27 && column == 2 && eventNum == 8)
                    {
                        return true;
                    }
                    // 真実世界、開始前
                    if (row == 4 && column == 2 && eventNum == 9)
                    {
                        return true;
                    }
                    // 青水晶到達、真実世界へ
                    if (row == 2 && column == 2 && eventNum == 10)
                    {
                        return true;
                    }

                    break;
                #endregion
            }
            return false;
        }
        
        private bool GetTreasure(string targetItemName)
        {
            return GetTreasure(targetItemName, false);
        }
        private bool GetTreasure(string targetItemName, bool MustGetIt)
        {
            if (MustGetIt == false)
            {
                nowMessage.Add("アイン:よっしゃ！お宝だぜ！"); nowEvent.Add(MessagePack.ActionEvent.None);
            }
            ItemBackPack backpackData = new ItemBackPack(targetItemName);
            bool result1 = GroundOne.MC.AddBackPack(backpackData);
            if (result1)
            {
                Debug.Log("itemget 1");
                nowMessage.Add("『" + backpackData.Name + "を手に入れました』"); nowEvent.Add(MessagePack.ActionEvent.None);
                tapOK();
                return true;
            }
            if (GroundOne.WE.AvailableSecondCharacter)
            {
                bool result2 = GroundOne.SC.AddBackPack(backpackData);
                if (result2)
                {
                    Debug.Log("itemget 2");
                    nowMessage.Add("『" + backpackData.Name + "を手に入れました』"); nowEvent.Add(MessagePack.ActionEvent.None);
                    tapOK();
                    return true;
                }
            }
            if (GroundOne.WE.AvailableThirdCharacter)
            {
                bool result3 = GroundOne.TC.AddBackPack(backpackData);
                if (result3)
                {
                    Debug.Log("itemget 3");
                    nowMessage.Add("『" + backpackData.Name + "を手に入れました』"); nowEvent.Add(MessagePack.ActionEvent.None);
                    tapOK();
                    return true;
                }
            }
            nowMessage.Add("荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。"); nowEvent.Add(MessagePack.ActionEvent.None);
            tapOK();
            Debug.Log("itemget fail");
            return false;
        }

        private bool ExecSomeEvents(int area, int ii)
        {
            #region "１階"
            if (area == 1)
            {
                switch (ii)
                {
                    #region "始まりの看板"
                    case 0:
                        MessagePack.Message10000(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "近道の看板"
                    case 1:
                        MessagePack.Message10001(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間、看板"
                    case 2:
                        MessagePack.Message10002(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板、メンバー構成"
                    case 3:
                        MessagePack.Message10003(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実解を示す子部屋情報"
                    case 4:
                        MessagePack.Message10004(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "街へ戻る"
                    case 5:
                        MessagePack.MessageBackToTown(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱エリア１"
                    case 6:
                        GroundOne.WE.TruthTreasure11 = GetTreasure(Database.COMMON_SIMPLE_BRACELET);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 7:
                        GroundOne.WE.TruthTreasure12 = GetTreasure(Database.POOR_HARD_SHOES);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 8:
                        GroundOne.WE.TruthTreasure13 = GetTreasure(Database.COMMON_SEAL_OF_POSION);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 9:
                        GroundOne.WE.TruthTreasure14 = GetTreasure(Database.COMMON_GREEN_EGG_KAIGARA);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 10:
                        GroundOne.WE.TruthTreasure15 = GetTreasure(Database.COMMON_CHARM_OF_FIRE_ANGEL);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    #endregion
                    #region "ボス戦闘"
                    case 11:
                        MessagePack.Message10005(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鍵付き扉"
                    case 12:
                        MessagePack.Message10012(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス１"
                    case 13:
                        MessagePack.Message10013(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス２"
                    case 14:
                        MessagePack.Message10014(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス３"
                    case 15:
                        MessagePack.Message10015(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス４"
                    case 16:
                        MessagePack.Message10016(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス５"
                    case 17:
                        MessagePack.Message10017(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス６"
                    case 18:
                        MessagePack.Message10018(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス７"
                    case 19:
                        MessagePack.Message10019(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス８"
                    case 20:
                        MessagePack.Message10020(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大広間エントランス９"
                    case 21:
                        MessagePack.Message10021(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱エリア２"
                    case 22:
                        GroundOne.WE.TruthTreasure121 = GetTreasure(Database.COMMON_DREAM_POWDER);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 23:
                        GroundOne.WE.TruthTreasure122 = GetTreasure(Database.COMMON_VIKING_SWORD);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 24:
                        GroundOne.WE.TruthTreasure123 = GetTreasure(Database.COMMON_NEBARIITO_KUMO);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 25:
                        GroundOne.WE.TruthTreasure124 = GetTreasure(Database.COMMON_SUN_PRISM);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 26:
                        GroundOne.WE.TruthTreasure125 = GetTreasure(Database.COMMON_POISON_EKISU);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 27:
                        GroundOne.WE.TruthTreasure126 = GetTreasure(Database.COMMON_SOLID_CLAW);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 28:
                        GroundOne.WE.TruthTreasure127 = GetTreasure(Database.COMMON_GREEN_LEEF_CHARM);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 29:
                        GroundOne.WE.TruthTreasure128 = GetTreasure(Database.COMMON_WARRIOR_MEDAL);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 30:
                        GroundOne.WE.TruthTreasure129 = GetTreasure(Database.COMMON_PALADIN_MEDAL);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 31:
                        GroundOne.WE.TruthTreasure1210 = GetTreasure(Database.COMMON_KASHI_ROD);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 32:
                        GroundOne.WE.TruthTreasure1211 = GetTreasure(Database.RARE_TOTAL_HIYAKU_KASSEI);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 33:
                        GroundOne.WE.TruthTreasure1212 = GetTreasure(Database.RARE_ZEPHER_FETHER);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 34:
                        GroundOne.WE.TruthTreasure131 = GetTreasure(Database.COMMON_HAYATE_ORB);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 35:
                        GroundOne.WE.TruthTreasure132 = GetTreasure(Database.COMMON_BLUE_COPPER);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 36:
                        GroundOne.WE.TruthTreasure133 = GetTreasure(Database.COMMON_ORANGE_MATERIAL);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 37:
                        GroundOne.WE.TruthTreasure134 = GetTreasure(Database.RARE_LIFE_SWORD);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 38:
                        GroundOne.WE.TruthTreasure141 = GetTreasure(Database.RARE_PURE_WATER);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    case 39:
                        GroundOne.WE.TruthTreasure142 = GetTreasure(Database.RARE_PURE_GREEN_SILK_ROBE);
                        UpdateFieldElement(this.Player.transform.position);
                        break;
                    #endregion
                    #region "中通路の扉"
                    case 40:
                        MessagePack.Message10040(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "中通路の扉(反対１)"
                    case 41:
                        MessagePack.Message10041(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "中通路の扉(反対２)"
                    case 42:
                        MessagePack.Message10042(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "小広間への扉"
                    case 43:
                        MessagePack.Message10043(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 44:
                        MessagePack.Message10044(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 45:
                        MessagePack.Message10045(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 46:
                        MessagePack.Message10046(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 47:
                        MessagePack.Message10047(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 48:
                        MessagePack.Message10048(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 49:
                        MessagePack.Message10049(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "ボス前の扉"
                    case 50:
                        MessagePack.Message10050(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "２階への階段"
                    case 51:
                        MessagePack.Message10051(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 52:
                        MessagePack.Message10052(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 53:
                        MessagePack.Message10053(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 54:
                        MessagePack.Message10054(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉系統"
                    case 55:
                        MessagePack.Message10055(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "隠し小部屋の前"
                    case 56:
                        MessagePack.Message10056(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "記憶の回想"
                    case 57:
                        MessagePack.Message10057(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "何もないとき"
                    default:
                        MessagePack.MessageNotFound(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                }
            }
            #endregion
            #region "２階"
            else if (area == 2)
            {
                switch (ii)
                {
                    #region "１階へ戻る階段"
                    case 0:
                        MessagePack.Message12000(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "中央４看板"
                    case 1:
                        MessagePack.Message12001(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 2:
                        MessagePack.Message12001_2(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 3:
                        MessagePack.Message12001_3(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 4:
                        MessagePack.Message12001_4(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、メイン看板、ファースト：３回"
                    case 5:
                        if (!GroundOne.WE.dungeonEvent211)
                        {
                            MessagePack.Message12002(ref this.nowMessage, ref this.nowEvent);
                            tapOK();
                        }
                    #endregion
                    #region "知の部屋、メイン看板、セカンド：下３０上１２５６９左８４７"
                        else if (!GroundOne.WE.dungeonEvent215)
                        {
                            MessagePack.Message12003(ref this.nowMessage, ref this.nowEvent);
                            tapOK();
                        }
                        else if (!GroundOne.WE.dungeonEvent219)
                        {
                            MessagePack.Message12003_2(ref this.nowMessage, ref this.nowEvent);
                            tapOK();                            
                        }
                    #endregion
                    #region "知の部屋、メイン看板、サード：( >10 _6 <7 )  ( <11 ^3 )  ( _3 >7 )"
                        MessagePack.Message12004(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、フェーズ１"
                    case 8:
                        MessagePack.Message12005(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 9:
                        MessagePack.Message12006(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 10:
                        MessagePack.Message12007(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、フェーズ２"
                    case 11:
                        MessagePack.Message12008(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 12:
                        MessagePack.Message12008_2(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 13:
                        MessagePack.Message12008_3(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、フェーズ３"
                    case 14:
                        MessagePack.Message12009(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 15:
                        MessagePack.Message12009_2(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 16:
                        MessagePack.Message12009_3(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、正しき順序看板"
                    case 17:
                        MessagePack.Message12010(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、正しき順序フラグ"
                    case 18:
                        MessagePack.Message12011(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 19:
                        MessagePack.Message12011_2(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 20:
                        MessagePack.Message12011_3(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 21:
                        MessagePack.Message12011_4(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 22:
                        MessagePack.Message12011_5(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 23:
                        MessagePack.Message12011_6(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 24:
                        MessagePack.Message12011_7(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 25:
                        MessagePack.Message12011_8(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 26:
                        MessagePack.Message12011_9(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 27:
                        MessagePack.Message12011_10(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 28:
                        MessagePack.Message12011_11(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 29:
                        MessagePack.Message12011_12(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 30:
                        MessagePack.Message12011_13(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 31:
                        MessagePack.Message12011_14(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 32:
                        MessagePack.Message12011_15(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 33:
                        MessagePack.Message12011_16(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 34:
                        MessagePack.Message12011_17(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 35:
                        MessagePack.Message12011_18(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 36:
                        MessagePack.Message12011_19(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 37:
                        MessagePack.Message12011_20(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 38:
                        MessagePack.Message12011_21(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 39:
                        MessagePack.Message12011_22(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 40:
                        MessagePack.Message12011_23(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、正しき順序解答"
                    case 41:
                        MessagePack.Message12016(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＡ"
                    case 42:
                        MessagePack.Message12017(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 43:
                        MessagePack.Message12018(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 44:
                        MessagePack.Message12019(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＢ"
                    case 45:
                        MessagePack.Message12020(ref this.nowMessage, ref this.nowEvent, ref ShadowTileNumber, ref BeforeDirectionNumber);
                        tapOK();
                        return true;
                    case 46:
                        MessagePack.Message12021(ref this.nowMessage, ref this.nowEvent, this.Player, ref ShadowTileNumber, ref BeforeDirectionNumber);
                        tapOK();
                        return true;
                    case 47:
                        MessagePack.Message12022(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＣ"
                    case 48:
                        MessagePack.Message12023(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 49:
                        if (this.detectKeyUp)
                        {
                            this.detectKeyUp = false;
                            MessagePack.Message12024_Fail(ref this.nowMessage, ref this.nowEvent);
                        }
                        else
                        {
                            MessagePack.Message12024(ref this.nowMessage, ref this.nowEvent);
                        }
                        tapOK();
                        return true;
                    case 50:
                        MessagePack.Message12025(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＤ"
                    case 51:
                        MessagePack.Message12026(ref this.nowMessage, ref this.nowEvent, ref this.Area4_InnerTimerCount, ref this.Area4_ShadowTileNum);
                        tapOK();
                        return true;
                    case 52:
                        MessagePack.Message12027(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 53:
                        MessagePack.Message12028(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＥ"
                    case 54:
                        MessagePack.Message12029(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 55:
                        MessagePack.Message12030(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 56:
                        MessagePack.Message12031(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＦ（最後一個前）"
                    case 57:
                        MessagePack.Message12032(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、ヒント１"
                    case 58:
                        MessagePack.Message12033(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材１"
                    case 59:
                        MessagePack.Message12034(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材２"
                    case 60:
                        MessagePack.Message12035(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材３"
                    case 61:
                        MessagePack.Message12036(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材４"
                    case 62:
                        MessagePack.Message12037(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材５"
                    case 63:
                        MessagePack.Message12038(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材６"
                    case 64:
                        MessagePack.Message12039(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材７"
                    case 65:
                        MessagePack.Message12040(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材８"
                    case 66:
                        MessagePack.Message12041(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材９"
                    case 67:
                        MessagePack.Message12042(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、題材１０"
                    case 68:
                        MessagePack.Message12043(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス１"
                    case 69:
                        MessagePack.Message12044(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス２"
                    case 70:
                        MessagePack.Message12045(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス３"
                    case 71:
                        MessagePack.Message12046(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス４"
                    case 72:
                        MessagePack.Message12047(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス５"
                    case 73:
                        MessagePack.Message12048(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、ボス６"
                    case 74:
                        MessagePack.Message12049(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱"
                    case 75:
                        GroundOne.WE.TruthTreasure21 = GetTreasure(Database.COMMON_PUZZLE_BOX);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 76:
                        GroundOne.WE.TruthTreasure22 = GetTreasure(Database.COMMON_CHIENOWA_RING);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 77:
                        GroundOne.WE.TruthTreasure23 = GetTreasure(Database.RARE_MASTER_PIECE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 78:
                        GroundOne.WE.TruthTreasure24 = GetTreasure(Database.COMMON_TUMUJIKAZE_BOX);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 79:
                        GroundOne.WE.TruthTreasure25 = GetTreasure(Database.COMMON_ROCKET_DASH);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 80:
                        GroundOne.WE.TruthTreasure26 = GetTreasure(Database.COMMON_CLAW_OF_SPRING);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 81:
                        GroundOne.WE.TruthTreasure27 = GetTreasure(Database.COMMON_SOUKAI_DRINK_SS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 82:
                        GroundOne.WE.TruthTreasure28 = GetTreasure(Database.COMMON_BREEZE_CROSS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 83:
                        GroundOne.WE.TruthTreasure29 = GetTreasure(Database.COMMON_GUST_SWORD);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 84:
                        GroundOne.WE.TruthTreasure210 = GetTreasure(Database.RARE_PURE_GREEN_WATER);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 85:
                        GroundOne.WE.TruthTreasure211 = GetTreasure(Database.COMMON_BLANK_BOX);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 86:
                        GroundOne.WE.TruthTreasure212 = GetTreasure(Database.RARE_SPIRIT_OF_HEART);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 87:
                        GroundOne.WE.TruthTreasure213 = GetTreasure(Database.COMMON_FUSION_BOX);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 88:
                        GroundOne.WE.TruthTreasure214 = GetTreasure(Database.COMMON_WAR_DRUM);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 89:
                        GroundOne.WE.TruthTreasure215 = GetTreasure(Database.COMMON_KOBUSHI_OBJE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 90:
                        GroundOne.WE.TruthTreasure216 = GetTreasure(Database.COMMON_TIGER_BLADE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 91:
                        GroundOne.WE.TruthTreasure217 = GetTreasure(Database.COMMON_TUUKAI_DRINK_DD);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 92:
                        GroundOne.WE.TruthTreasure218 = GetTreasure(Database.RARE_ROD_OF_STRENGTH);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "知の部屋、複合レバーの看板１"
                    case 93:
                        MessagePack.Message12051(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、複合レバー１－１"
                    case 94:
                        MessagePack.Message12052(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "知の部屋、複合レバー１－２"
                    case 95:
                        MessagePack.Message12053(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、複合レバーの看板１"
                    case 96:
                        MessagePack.Message12054(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、複合レバー２－１"
                    case 97:
                        MessagePack.Message12055(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、複合レバー２－２"
                    case 98:
                        MessagePack.Message12056(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、複合レバーの看板１"
                    case 99:
                        MessagePack.Message12057(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、複合レバー３－１"
                    case 100:
                        MessagePack.Message12058(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "力の部屋、複合レバー３－２"
                    case 101:
                        MessagePack.Message12059(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、複合レバーの看板１"
                    case 102:
                        MessagePack.Message12060(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、複合レバー４－１"
                    case 103:
                        MessagePack.Message12061(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "心の部屋、複合レバー４－２"
                    case 104:
                        MessagePack.Message12062(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、隠し通路発見"
                    case 105:
                        MessagePack.Message12065(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "３階階段直前"
                    case 106:
                        MessagePack.Message12066(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "下り階段"
                    case 107:
                        MessagePack.Message12067(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "記憶の回想"
                    case 108:
                        MessagePack.Message12068(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "技の部屋、エリアＥ(戦闘回避)"
                    case 109:
                        // 何も記載してないが、これで戦闘発生を回避する
                        return true;
                    #endregion
                }
            }
            #endregion
            #region "３階"
            else if (area == 3)
            {
                switch (ii)
                {
                    #region "２階へ戻る階段"
                    case 0:
                        MessagePack.Message13000(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "入り口看板への誘導
                    case 1:
                        MessagePack.Message13001(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "始めの誘導縛り"
                    case 2:
                        MessagePack.Message13002(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 3:
                        MessagePack.Message13003(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "始まりの看板１"
                    case 4:
                        MessagePack.Message13004(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "始まりの看板２"
                    case 5:
                        MessagePack.Message13005(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡解説(1)"
                    case 6:
                    case 7:
                        MessagePack.Message13006(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡ワープ area 1"
                    case 8:
                        MessagePack.Message13007(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 9:
                        // 通路
                        MessagePack.Message13008(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 10:
                        MessagePack.Message13009(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 11:
                        MessagePack.Message13010(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 12:
                        MessagePack.Message13011(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 13:
                        // 通路
                        MessagePack.Message13012(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 14:
                        // 通路
                        MessagePack.Message13013(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 15:
                        // 通路（左隅)
                        MessagePack.Message13014(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 16:
                        MessagePack.Message13015(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 17:
                        MessagePack.Message13016(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 18:
                        MessagePack.Message13017(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 19:
                        MessagePack.Message13018(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 20:
                        // 通路
                        MessagePack.Message13019(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 21:
                        MessagePack.Message13020(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 22:
                        // 通路
                        MessagePack.Message13021(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 23:
                        MessagePack.Message13022(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 24:
                        MessagePack.Message13023(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 25:
                        MessagePack.Message13024(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 26:
                        MessagePack.Message13025(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 27:
                        MessagePack.Message13026(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 28:
                        MessagePack.Message13027(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 29:
                        // 通路（中央左の上）
                        MessagePack.Message13028(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 30:
                        // 通路（中央左の下）
                        MessagePack.Message13029(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 31:
                        MessagePack.Message13030(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 32:
                        // 通路
                        MessagePack.Message13031(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 33:
                        // 通路（左下）
                        MessagePack.Message13032(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 34:
                        MessagePack.Message13033(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 35:
                        MessagePack.Message13034(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 36:
                        MessagePack.Message13035(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 37:
                        MessagePack.Message13036(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 38:
                        MessagePack.Message13037(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 39:
                        MessagePack.Message13038(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 40:
                        MessagePack.Message13039(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 41:
                        MessagePack.Message13040(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 42:
                        // 通路
                        MessagePack.Message13041(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 43:
                        MessagePack.Message13042(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 44:
                        // 左小部屋１
                        MessagePack.Message13043(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 45: // clear
                        MessagePack.Message13044(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 46: // 失敗戻り
                        MessagePack.Message13045(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実解のイベント１【記憶の回想】"
                    case 47:
                        MessagePack.Message13046(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡ワープ area2"
                    case 48: // 38
                        MessagePack.Message13047(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 49: // 39
                        MessagePack.Message13048(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 50: // 40
                        MessagePack.Message13049(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 51: // 41
                        MessagePack.Message13050(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 52: // 42
                        MessagePack.Message13051(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 53: // 43
                        MessagePack.Message13052(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 54: // 44
                        MessagePack.Message13053(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 55: // 45
                        MessagePack.Message13054(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 56: // 46
                        MessagePack.Message13055(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 57: // 47
                        MessagePack.Message13056(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 58: // 48
                        MessagePack.Message13057(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 59: // 49
                        MessagePack.Message13058(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 60: // 50
                        MessagePack.Message13059(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 61: // 51
                        MessagePack.Message13060(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 62: // 52
                        MessagePack.Message13061(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 63: // 53
                        MessagePack.Message13062(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 64: // 54
                        MessagePack.Message13063(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 65: // 55
                        MessagePack.Message13064(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 66: // 56
                        MessagePack.Message13065(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 67: // 57
                        MessagePack.Message13066(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 68: // 58
                        MessagePack.Message13067(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 69: // 59
                        MessagePack.Message13068(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 70: // 60
                        MessagePack.Message13069(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 71: // 61
                        MessagePack.Message13070(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 72: // 62
                        MessagePack.Message13071(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 73: // 63
                        MessagePack.Message13072(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 74: // 64
                        MessagePack.Message13073(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 75: // 65
                        MessagePack.Message13074(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 76: // 66
                        MessagePack.Message13075(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 77: // 67
                        MessagePack.Message13076(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 78: // 68
                        MessagePack.Message13077(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 79: // 69
                        MessagePack.Message13078(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 80: // 70
                        MessagePack.Message13079(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 81: // 71
                        MessagePack.Message13080(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 82: // 72
                        MessagePack.Message13081(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 83: // 73
                        MessagePack.Message13082(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 84: // 74
                        MessagePack.Message13083(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 85: // 75
                        MessagePack.Message13084(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 86: // 76
                        MessagePack.Message13085(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 87: // 77
                        MessagePack.Message13086(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 88: // 78
                        MessagePack.Message13087(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 89: // 79
                        MessagePack.Message13088(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 90: // 80
                        MessagePack.Message13089(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 91: // 81
                        MessagePack.Message13090(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 92: // 82
                        MessagePack.Message13091(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 93: // 83
                        MessagePack.Message13092(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 94: // 84
                        MessagePack.Message13093(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    // ５段階目以降、ルート通過順序によって可変。
                    // 36 43
                    // 17 53
                    // 26 24
                    // 38 21
                    // 24 21

                    // １箇所グループ
                    case 95: // 85
                    case 96: // 86
                    case 97: // 87
                    case 105: // 95
                    case 112: // 102
                    case 113: // 103
                    case 121: // 111
                    case 124: // 114
                    case 125: // 115
                    case 133: // 123
                    case 138: // 128
                    case 144: // 134
                    // ２箇所グループ
                    case 122: // 112
                    case 123: // 113
                    case 142: // 132
                    case 143: // 133
                    case 145: // 135
                    case 146: // 136
                    // ３箇所グループ
                    case 98: // 88
                    case 99: // 89
                    case 100: // 90
                    case 106: // 96
                    case 107: // 97
                    case 108: // 98
                    case 109: // 99
                    case 110: // 100
                    case 111: // 101
                    case 114: // 104
                    case 115: // 105
                    case 116: // 106
                    case 130: // 120
                    case 131: // 121
                    case 132: // 122
                    case 139: // 129
                    case 140: // 130
                    case 141: // 131
                    // ４箇所グループ
                    case 101: // 91
                    case 102: // 92
                    case 103: // 93
                    case 104: // 94
                    case 117: // 107
                    case 118: // 108
                    case 119: // 109
                    case 120: // 110
                    case 126: // 116
                    case 127: // 117
                    case 128: // 118
                    case 129: // 119
                    case 134: // 124
                    case 135: // 125
                    case 136: // 126
                    case 137: // 127
                    case 147: // 137
                    case 148: // 138
                    case 149: // 139
                    case 150: // 140
                        MessagePack.Message13094(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 151: // X1B
                        MessagePack.Message13095(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 152: // X1C
                        MessagePack.Message13096(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 153: // X1D
                        MessagePack.Message13097(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 154: // X2B
                        MessagePack.Message13098(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 155: // X2C
                        MessagePack.Message13099(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 156: // X2D
                        MessagePack.Message13100(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 157: // X3B
                        MessagePack.Message13101(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 158: // X3C
                        MessagePack.Message13102(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 159: // X3D
                        MessagePack.Message13103(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 160: // X4B
                        MessagePack.Message13104(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 161: // X4C
                        MessagePack.Message13105(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 162: // X4D
                        MessagePack.Message13106(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 163: // X5B
                        MessagePack.Message13107(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 164: // X5C
                        MessagePack.Message13108(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 165: // X5D
                        MessagePack.Message13109(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    // Z1-Z4ルート
                    case 166:
                    case 167:
                    case 168:
                    case 169:
                        MessagePack.Message13110(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    // Z2ルート
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                    case 175:
                        MessagePack.Message13110_2(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    // Z3ルート
                    case 176:
                    case 177:
                    case 178:
                    case 179:
                    case 180:
                    case 181:
                    case 182:
                        MessagePack.Message13110_3(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    // Z4ルート
                    case 183:
                    case 184:
                    case 185:
                    case 186:
                    case 187:
                    case 188:
                    case 189:
                    case 190:
                        MessagePack.Message13110_4(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "ボス戦闘"
                    case 251:
                        MessagePack.Message13111(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡エリア２の始めの看板"
                    case 252:
                        MessagePack.Message13112(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱"
                    case 253:
                        GroundOne.WE.TruthTreasure301 = GetTreasure(Database.COMMON_ESSENCE_OF_EARTH);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 254:
                        GroundOne.WE.TruthTreasure302 = GetTreasure(Database.COMMON_KESSYOU_SEA_WATER_SALT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 255:
                        GroundOne.WE.TruthTreasure303 = GetTreasure(Database.COMMON_STAR_DUST_RING);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 256:
                        GroundOne.WE.TruthTreasure304 = GetTreasure(Database.COMMON_RED_ONION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 257:
                        GroundOne.WE.TruthTreasure305 = GetTreasure(Database.RARE_TAMATEBAKO_AKIDAMA);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 258:
                        GroundOne.WE.TruthTreasure306 = GetTreasure(Database.RARE_HARDEST_FIT_BOOTS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 259:
                        GroundOne.WE.TruthTreasure307 = GetTreasure(Database.COMMON_WATERY_GROBE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 260:
                        GroundOne.WE.TruthTreasure308 = GetTreasure(Database.COMMON_WHITE_POWDER);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 261:
                        GroundOne.WE.TruthTreasure309 = GetTreasure(Database.COMMON_SILENT_BOWL);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 262:
                        GroundOne.WE.TruthTreasure310 = GetTreasure(Database.RARE_SEAL_OF_ICE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 263:
                        GroundOne.WE.TruthTreasure311 = GetTreasure(Database.RARE_SWORD_OF_DIVIDE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 264:
                        GroundOne.WE.TruthTreasure312 = GetTreasure(Database.EPIC_OLD_TREE_MIKI_DANPEN);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "エリア２　看板X１～４"
                    case 265:
                        MessagePack.Message13113(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 266:
                        MessagePack.Message13114(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 267:
                        MessagePack.Message13115(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    case 268:
                        MessagePack.Message13116(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実解のイベント２【記憶の回想】"
                    case 280:
                        MessagePack.Message13117(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "記憶の回想２から一本道正解ルートへ鏡ワープ"
                    case 281:
                        MessagePack.Message13118(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "絶対試練"
                    case 269:
                        MessagePack.Message13119(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "原点解の看板"
                    case 270:
                        MessagePack.Message13120(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "ボス前の扉"
                    case 271:
                        MessagePack.Message13121(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "４階への階段"
                    case 272:
                        MessagePack.Message13122(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "無限回廊の看板"
                    case 273:
                        MessagePack.Message13123(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "無限回廊"
                    // Final、無限１～１２組み合わせ

                    case 191:
                    case 192:
                    case 193:
                    case 194:
                    case 195:
                        MessagePack.Message13124(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 196:
                    case 197:
                    case 198:
                    case 199:
                    case 200:
                        MessagePack.Message13125(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 201:
                    case 202:
                    case 203:
                    case 204:
                    case 205:
                        MessagePack.Message13126(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 206:
                    case 207:
                    case 208:
                    case 209:
                    case 210:
                        MessagePack.Message13127(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 211:
                    case 212:
                    case 213:
                    case 214:
                    case 215:
                        MessagePack.Message13128(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 216:
                    case 217:
                    case 218:
                    case 219:
                    case 220:
                        MessagePack.Message13129(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 221:
                    case 222:
                    case 223:
                    case 224:
                    case 225:
                        MessagePack.Message13130(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 226:
                    case 227:
                    case 228:
                    case 229:
                    case 230:
                        MessagePack.Message13131(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 231:
                    case 232:
                    case 233:
                    case 234:
                    case 235:
                        MessagePack.Message13132(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 236:
                    case 237:
                    case 238:
                    case 239:
                    case 240:
                        MessagePack.Message13133(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 241:
                    case 242:
                    case 243:
                    case 244:
                    case 245:
                        MessagePack.Message13134(ref this.nowMessage, ref this.nowEvent, ii);
                        tapOK();
                        return true;
                    case 246:
                    case 247:
                    case 248:
                    case 249:
                    case 250:
                        bool result = CheckInfiniteLoopResult();
                        MessagePack.Message13135(ref this.nowMessage, ref this.nowEvent, ii, result);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡エリア２－５、台座ルート入り口"
                    case 274:
                        MessagePack.Message13136(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鏡エリア３原点解の入口／出口"
                    case 275:
                        MessagePack.Message13139(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "聖者ルート（BADEND）"
                    case 276:
                        MessagePack.Message13140(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "愚者ルート（BADEND）"
                    case 277:
                        MessagePack.Message13141(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "４階への階段（２）"
                    case 278:
                        MessagePack.Message13142(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "無限回廊突破後の看板"
                    case 279:
                        MessagePack.Message13143(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実解のイベント３【記憶の回想】"
                    case 282:
                        MessagePack.Message13137(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "原点解発見"
                    case 283:
                        MessagePack.Message13138(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実解のイベント４【記憶の回想】"
                    case 284:
                        MessagePack.Message13144(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "無限回廊突破後の看板にて"
                    case 285:
                        MessagePack.Message13145(ref this.nowMessage, ref this.nowEvent);
                        tapOK();
                        return true;
                    #endregion
                }
            }
            #endregion
            #region "４階"
            else if (area == 4)
            {
                switch (ii)
                {
                    #region "３階へ戻る階段"
                    case 0:
                        MessagePack.Message14000(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "始まりの扉"
                    case 1:
                        MessagePack.Message14001(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１"
                    case 2:
                        MessagePack.Message14002(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-１"
                    case 3:
                        MessagePack.Message14003(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱（エリア１）"
                    case 4:
                        GroundOne.WE.TruthTreasure401 = GetTreasure(Database.COMMON_SOCIETY_SYMBOL);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 5:
                        GroundOne.WE.TruthTreasure402 = GetTreasure(Database.COMMON_BLACK_SALT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 6:
                        GroundOne.WE.TruthTreasure403 = GetTreasure(Database.RARE_ESSENCE_OF_DARK);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 7:
                        GroundOne.WE.TruthTreasure404 = GetTreasure(Database.COMMON_LIGHT_SERVANT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 8:
                        GroundOne.WE.TruthTreasure405 = GetTreasure(Database.COMMON_FEBL_ANIS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 9:
                        GroundOne.WE.TruthTreasure406 = GetTreasure(Database.RARE_ASTRAL_VOID_BLADE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 10:
                        GroundOne.WE.TruthTreasure407 = GetTreasure(Database.COMMON_SMORKY_HUNNY);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 11:
                        GroundOne.WE.TruthTreasure408 = GetTreasure(Database.COMMON_SUN_TARAGON);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "鍵（エリア１）"
                    case 12:
                        MessagePack.Message14004(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 13:
                        MessagePack.Message14005(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 14:
                        MessagePack.Message14006(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 15:
                        MessagePack.Message14007(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 16:
                        MessagePack.Message14008(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 17:
                        MessagePack.Message14009(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 18:
                        MessagePack.Message14010(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 19:
                        MessagePack.Message14011(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 20:
                        MessagePack.Message14012(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉１"
                    case 21:
                        MessagePack.Message14013(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉２"
                    case 22:
                        MessagePack.Message14014(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉３"
                    case 23:
                        MessagePack.Message14015(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉４"
                    case 24:
                        MessagePack.Message14016(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉５"
                    case 25:
                        MessagePack.Message14017(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉６"
                    case 26:
                        MessagePack.Message14018(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉７"
                    case 27:
                        MessagePack.Message14019(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉８"
                    case 28:
                        MessagePack.Message14020(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉９"
                    case 29:
                        MessagePack.Message14021(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想１"
                    case 30:
                        MessagePack.Message14022(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想２"
                    case 31:
                        MessagePack.Message14023(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想３"
                    case 32:
                        MessagePack.Message14024(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想４"
                    case 33:
                        MessagePack.Message14025(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想５"
                    case 34:
                        MessagePack.Message14026(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想６"
                    case 35:
                        MessagePack.Message14027(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想７"
                    case 36:
                        MessagePack.Message14028(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想８"
                    case 37:
                        MessagePack.Message14029(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想９"
                    case 38:
                        MessagePack.Message14030(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実の回想１０"
                    case 39:
                        MessagePack.Message14031(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-２"
                    case 40:
                        MessagePack.Message14032(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-３"
                    case 41:
                        MessagePack.Message14033(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-４"
                    case 42:
                        MessagePack.Message14034(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-５"
                    case 43:
                        MessagePack.Message14035(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-６"
                    case 44:
                        MessagePack.Message14036(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-７"
                    case 45:
                        MessagePack.Message14037(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "看板１-８"
                    case 46:
                        MessagePack.Message14038(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア１ボス前"
                    case 47:
                        MessagePack.Message14039(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア１ボス"
                    case 48:
                        MessagePack.Message14040(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア１から２への通路"
                    case 49:
                        MessagePack.Message14041(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２スタート"
                    case 50:
                        MessagePack.Message14042(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２看板"
                    case 51:
                        MessagePack.Message14043(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２-１（看板）"
                    case 52:
                        MessagePack.Message14044(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉の鍵（エリア２Ｘ）"
                    case 53:
                        MessagePack.Message14045(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 54:
                        MessagePack.Message14046(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 55:
                        MessagePack.Message14047(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 56:
                        MessagePack.Message14048(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 57:
                        MessagePack.Message14049(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱（エリア２Ｘ）"
                    case 58:
                        GroundOne.WE.TruthTreasure409 = GetTreasure(Database.COMMON_MUKEI_SAKAZUKI);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 59:
                        GroundOne.WE.TruthTreasure410 = GetTreasure(Database.COMMON_ELDER_PERSPECTIVE_GRASS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 60:
                        GroundOne.WE.TruthTreasure411 = GetTreasure(Database.RARE_SHADOW_BIBLE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 61:
                        GroundOne.WE.TruthTreasure412 = GetTreasure(Database.RARE_MIND_ILLUSION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 62:
                        GroundOne.WE.TruthTreasure413 = GetTreasure(Database.COMMON_ANGEL_DUST);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 63:
                        GroundOne.WE.TruthTreasure414 = GetTreasure(Database.GROWTH_LIQUID4_STRENGTH);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 64:
                        GroundOne.WE.TruthTreasure415 = GetTreasure(Database.COMMON_SILVER_RING_3);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 65:
                        GroundOne.WE.TruthTreasure416 = GetTreasure(Database.RARE_BLIND_NEEDLE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 66:
                        GroundOne.WE.TruthTreasure417 = GetTreasure(Database.COMMON_PURPLE_FLOAT_STONE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 67:
                        GroundOne.WE.TruthTreasure418 = GetTreasure(Database.COMMON_MURYOU_CROSS);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 68:
                        GroundOne.WE.TruthTreasure419 = GetTreasure(Database.COMMON_HUGE_BLUE_POTION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 69:
                        GroundOne.WE.TruthTreasure420 = GetTreasure(Database.COMMON_GREEN_FLOAT_STONE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 70:
                        GroundOne.WE.TruthTreasure421 = GetTreasure(Database.COMMON_SILVER_RING_7);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 71:
                        GroundOne.WE.TruthTreasure422 = GetTreasure(Database.RARE_ANGEL_CONTRACT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "扉Ｘ１(入口)"
                    case 72:
                        MessagePack.Message14050(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉Ｘ２(出口)"
                    case 73:
                        MessagePack.Message14051(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鍵（エリア２Ｙ）"
                    case 74:
                        MessagePack.Message14052(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 75:
                        MessagePack.Message14053(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 76:
                        MessagePack.Message14054(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 77:
                        MessagePack.Message14055(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 78:
                        MessagePack.Message14056(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱（エリア２Ｙ）"
                    case 79:
                        GroundOne.WE.TruthTreasure423 = GetTreasure(Database.COMMON_YELLOW_FLOAT_STONE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 80:
                        GroundOne.WE.TruthTreasure424 = GetTreasure(Database.RARE_DOMINATION_BRAVE_ARMOR);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 81:
                        GroundOne.WE.TruthTreasure425 = GetTreasure(Database.GROWTH_LIQUID4_AGILITY);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 82:
                        GroundOne.WE.TruthTreasure426 = GetTreasure(Database.COMMON_HUGE_RED_POTION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 83:
                        GroundOne.WE.TruthTreasure427 = GetTreasure(Database.COMMON_BULLET_KNUCKLE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 84:
                        GroundOne.WE.TruthTreasure428 = GetTreasure(Database.COMMON_SILVER_RING_4);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 85:
                        GroundOne.WE.TruthTreasure429 = GetTreasure(Database.RARE_SHINING_AETHER);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 86:
                        GroundOne.WE.TruthTreasure430 = GetTreasure(Database.RARE_COLORLESS_ANTIDOTE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 87:
                        GroundOne.WE.TruthTreasure431 = GetTreasure(Database.RARE_DEVIL_SUMMONER_TOME);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 88:
                        GroundOne.WE.TruthTreasure432 = GetTreasure(Database.COMMON_INITIATE_SWORD);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 89:
                        GroundOne.WE.TruthTreasure433 = GetTreasure(Database.COMMON_ROYAL_GUARD_RING);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 90:
                        GroundOne.WE.TruthTreasure434 = GetTreasure(Database.RARE_CORE_ESSENCE_CHANNEL);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "扉Ｙ１(入口)"
                    case 91:
                        MessagePack.Message14057(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "扉Ｙ２(出口)"
                    case 92:
                        MessagePack.Message14058(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "鍵（エリア２Ｚ）"
                    case 93:
                        MessagePack.Message14059(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 94:
                        MessagePack.Message14060(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 95:
                        MessagePack.Message14061(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 96:
                        MessagePack.Message14062(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 97:
                        MessagePack.Message14063(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱（エリア２Ｚ）"
                    case 98:
                        GroundOne.WE.TruthTreasure435 = GetTreasure(Database.RARE_ESSENCE_OF_ADAMANTINE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 99:
                        GroundOne.WE.TruthTreasure436 = GetTreasure(Database.COMMON_HUGE_GREEN_POTION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 100:
                        GroundOne.WE.TruthTreasure437 = GetTreasure(Database.RARE_DEMON_HORN);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 101:
                        GroundOne.WE.TruthTreasure438 = GetTreasure(Database.GROWTH_LIQUID4_INTELLIGENCE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 102:
                        GroundOne.WE.TruthTreasure439 = GetTreasure(Database.RARE_BLACK_ELIXIR);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 103:
                        GroundOne.WE.TruthTreasure440 = GetTreasure(Database.RARE_DARK_ANGEL_ROBE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 104:
                        GroundOne.WE.TruthTreasure441 = GetTreasure(Database.RARE_DOOMBRINGER_KAKERA);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 105:
                        GroundOne.WE.TruthTreasure442 = GetTreasure(Database.RARE_JOUKA_TANZOU);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "扉Ｚ１(入口)"
                    case 106:
                        MessagePack.Message14064(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２ボス前"
                    case 107:
                        MessagePack.Message14065(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２ボス"
                    case 108:
                        MessagePack.Message14066(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア２から３への通路"
                    case 109:
                        MessagePack.Message14067(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３スタート"
                    case 110:
                        MessagePack.Message14068(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３看板"
                    case 111:
                        MessagePack.Message14069(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３-１開始"
                    case 112:
                        MessagePack.Message14070(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３看板１"
                    case 113:
                        MessagePack.Message14071(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実１"
                    case 114:
                        MessagePack.Message14072(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実２"
                    case 115:
                        MessagePack.Message14073(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実３"
                    case 116:
                        MessagePack.Message14074(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実４"
                    case 117:
                        MessagePack.Message14075(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実５"
                    case 118:
                        MessagePack.Message14076(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実６"
                    case 119:
                        MessagePack.Message14077(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実７"
                    case 120:
                        MessagePack.Message14078(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実８"
                    case 121:
                        MessagePack.Message14079(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実９"
                    case 122:
                        MessagePack.Message14080(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、事実１０"
                    case 123:
                        MessagePack.Message14081(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３－２への扉"
                    case 124:
                        MessagePack.Message14082(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３-２開始"
                    case 125:
                        MessagePack.Message14083(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３-２看板"
                    case 126:
                        MessagePack.Message14084(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実１"
                    case 127:
                        MessagePack.Message14085(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実２"
                    case 128:
                        MessagePack.Message14086(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実３"
                    case 129:
                        MessagePack.Message14087(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実４"
                    case 130:
                        MessagePack.Message14088(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実５"
                    case 131:
                        MessagePack.Message14089(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実６"
                    case 132:
                        MessagePack.Message14090(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実７"
                    case 133:
                        MessagePack.Message14091(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実８"
                    case 134:
                        MessagePack.Message14092(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実９"
                    case 135:
                        MessagePack.Message14093(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実１０"
                    case 136:
                        MessagePack.Message14094(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱エリア３"
                    case 137:
                        GroundOne.WE.TruthTreasure443 = GetTreasure(Database.RARE_KYUUDOUSYA_HIDENSYO);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 138:
                        GroundOne.WE.TruthTreasure444 = GetTreasure(Database.GROWTH_LIQUID4_STAMINA);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 139:
                        GroundOne.WE.TruthTreasure445 = GetTreasure(Database.RARE_ESSENCE_OF_SHINE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 140:
                        GroundOne.WE.TruthTreasure446 = GetTreasure(Database.COMMON_ECHO_BEAST_MEAT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 141:
                        GroundOne.WE.TruthTreasure447 = GetTreasure(Database.RARE_BLACK_SEAL_IMPRESSION);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 142:
                        GroundOne.WE.TruthTreasure448 = GetTreasure(Database.RARE_MASTERBLADE_KAKERA);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 143:
                        GroundOne.WE.TruthTreasure449 = GetTreasure(Database.COMMON_CHAOS_TONGUE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 144:
                        GroundOne.WE.TruthTreasure450 = GetTreasure(Database.RARE_CHAOS_SIZUKU);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "エリア３-２終了の扉"
                    case 145:
                        MessagePack.Message14095(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３ボス前"
                    case 146:
                        MessagePack.Message14096(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３ボス"
                    case 147:
                        MessagePack.Message14097(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３から４への通路"
                    case 148:
                        MessagePack.Message14098(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４スタート"
                    case 149:
                        MessagePack.Message14099(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４看板"
                    case 150:
                        MessagePack.Message14100(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-１広間"
                    case 151:
                        MessagePack.Message14101(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-１看板"
                    case 152:
                        MessagePack.Message14102(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-１鍵
                    case 153:
                        MessagePack.Message14103(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 154:
                        MessagePack.Message14104(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 155:
                        MessagePack.Message14105(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 156:
                        MessagePack.Message14106(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 157:
                        MessagePack.Message14107(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱エリア４-１"
                    case 158:
                        GroundOne.WE.TruthTreasure451 = GetTreasure(Database.GROWTH_LIQUID4_MIND);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 159:
                        GroundOne.WE.TruthTreasure452 = GetTreasure(Database.RARE_VOID_HYMNSONIA);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 160:
                        GroundOne.WE.TruthTreasure453 = GetTreasure(Database.RARE_WHITE_DIAMOND_SHIELD);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 161:
                        GroundOne.WE.TruthTreasure454 = GetTreasure(Database.RARE_ARCHANGEL_CONTRACT);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "エリア４-１扉"
                    case 162:
                        MessagePack.Message14108(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-１終了"
                    case 163:
                        MessagePack.Message14109(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-２看板"
                    case 164:
                        MessagePack.Message14110(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-２鍵"
                    case 165:
                        MessagePack.Message14111(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 166:
                        MessagePack.Message14112(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 167:
                        MessagePack.Message14113(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 168:
                        MessagePack.Message14114(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 169:
                        MessagePack.Message14115(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 170:
                        MessagePack.Message14116(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 171:
                        MessagePack.Message14117(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 172:
                        MessagePack.Message14118(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "宝箱エリア４-２"
                    case 173:
                        GroundOne.WE.TruthTreasure455 = GetTreasure(Database.RARE_EVERMIND_SENSE);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 174:
                        GroundOne.WE.TruthTreasure456 = GetTreasure(Database.GROWTH_LIQUID4_MIND);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 175:
                        GroundOne.WE.TruthTreasure457 = GetTreasure(Database.RARE_DARKNESS_COIN);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 176:
                        GroundOne.WE.TruthTreasure458 = GetTreasure(Database.RARE_DANZAI_ANGEL_GOHU);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    case 177:
                        GroundOne.WE.TruthTreasure459 = GetTreasure(Database.EPIC_ETERNAL_HOMURA_RING);
                        UpdateFieldElement(this.Player.transform.position);
                        return true;
                    #endregion
                    #region "エリア４-２扉"
                    case 178:
                        MessagePack.Message14119(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４-２終了"
                    case 179:
                        MessagePack.Message14120(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア４から１への通路"
                    case 180:
                        MessagePack.Message14121(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "そして、現実世界へ"
                    case 181:
                        MessagePack.Message14122(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "究極の二択"
                    case 182:
                        MessagePack.Message14123(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "最下層への階段前の扉"
                    case 183:
                        MessagePack.Message14124(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "下り階段"
                    case 184:
                        MessagePack.Message14125(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "現実世界、移動ブロック"
                    case 185:
                        MessagePack.Message14126(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 186:
                        MessagePack.Message14127(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 187:
                        MessagePack.Message14128(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    case 188:
                        MessagePack.Message14129(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "エリア３、ラナ消失後、戻りブロック"
                    case 189:
                        MessagePack.Message14098_2(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                }
            }
            #endregion
            #region "５階"
            else if (area == 5)
            {
                switch (ii)
                {
                    #region "４階へ戻る階段"
                    case 0:
                        MessagePack.Message15000(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "細い通路の始まり"
                    case 1:
                        MessagePack.Message15001(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大通路の始まり"
                    case 2:
                        MessagePack.Message15002(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大通路の中間１"
                    case 3:
                        MessagePack.Message15003(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大通路の中間２"
                    case 4:
                        MessagePack.Message15004(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "大通路の終わり"
                    case 5:
                        MessagePack.Message15005(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "ボスと語り、そして戦闘へ"
                    case 6:
                        MessagePack.Message15006(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "ホログラムによるパーティ編成"
                    case 7:
                        MessagePack.Message15007(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実世界への入り口"
                    case 8:
                        MessagePack.Message15008(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実世界、開始直前"
                    case 9:
                        MessagePack.Message15009(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                    #region "真実世界、開始"
                    case 10:
                        MessagePack.Message15010(ref nowMessage, ref nowEvent);
                        tapOK();
                        return true;
                    #endregion
                }
            }
            #endregion
            return false;
        }

        private bool DetectOpenTreasure(Vector3 pos)
        {
            if ((((GroundOne.WE.TruthTreasure11 && pos.y == -13 && pos.x == 34) ||
                    (GroundOne.WE.TruthTreasure12 && pos.y == -21 && pos.x == 53) ||
                    (GroundOne.WE.TruthTreasure13 && (pos.y == -29 && pos.x == 29)) ||
                    (GroundOne.WE.TruthTreasure14 && (pos.y == -8 && pos.x == 33)) ||
                    (GroundOne.WE.TruthTreasure15 && (pos.y == -1 && pos.x == 22)) ||
                    (GroundOne.WE.TruthTreasure121 && (pos.y == -1 && pos.x == 40)) ||
                    (GroundOne.WE.TruthTreasure122 && (pos.y == -1 && pos.x == 53)) ||
                    (GroundOne.WE.TruthTreasure123 && (pos.y == -8 && pos.x == 49)) ||
                    (GroundOne.WE.TruthTreasure124 && (pos.y == -12 && pos.x == 24)) ||
                    (GroundOne.WE.TruthTreasure125 && (pos.y == -18 && pos.x == 9)) ||
                    (GroundOne.WE.TruthTreasure126 && (pos.y == -20 && pos.x == 45)) ||
                    (GroundOne.WE.TruthTreasure127 && (pos.y == -24 && pos.x == 52)) ||
                    (GroundOne.WE.TruthTreasure128 && (pos.y == -26 && pos.x == 52)) ||
                    (GroundOne.WE.TruthTreasure129 && (pos.y == -28 && pos.x == 43)) ||
                    (GroundOne.WE.TruthTreasure1210 && (pos.y == -35 && pos.x == 26)) ||
                    (GroundOne.WE.TruthTreasure1211 && (pos.y == -35 && pos.x == 34)) ||
                    (GroundOne.WE.TruthTreasure1212 && (pos.y == -38 && pos.x == 48)) ||
                    (GroundOne.WE.TruthTreasure131 && (pos.y == -9 && pos.x == 7)) ||
                    (GroundOne.WE.TruthTreasure132 && (pos.y == -18 && pos.x == 1)) ||
                    (GroundOne.WE.TruthTreasure133 && (pos.y == -22 && pos.x == 8)) ||
                    (GroundOne.WE.TruthTreasure134 && (pos.y == -36 && pos.x == 12)) ||
                    (GroundOne.WE.TruthTreasure141 && (pos.y == -8 && pos.x == 19)) ||
                    (GroundOne.WE.TruthTreasure142 && (pos.y == -16 && pos.x == 8))) && GroundOne.WE.DungeonArea == 1) ||
                (((GroundOne.WE.TruthTreasure21 && (pos.y == -16 && pos.x == 59)) ||
                    (GroundOne.WE.TruthTreasure22 && (pos.y == -12 && pos.x == 35)) ||
                    (GroundOne.WE.TruthTreasure23 && (pos.y == -5 && pos.x == 55)) ||
                    (GroundOne.WE.TruthTreasure24 && (pos.y == -25 && pos.x == 59)) ||
                    (GroundOne.WE.TruthTreasure25 && (pos.y == -27 && pos.x == 46)) ||
                    (GroundOne.WE.TruthTreasure26 && (pos.y == -27 && pos.x == 34)) ||
                    (GroundOne.WE.TruthTreasure27 && (pos.y == -34 && pos.x == 34)) ||
                    (GroundOne.WE.TruthTreasure28 && (pos.y == -34 && pos.x == 46)) ||
                    (GroundOne.WE.TruthTreasure29 && (pos.y == -34 && pos.x == 58)) ||
                    (GroundOne.WE.TruthTreasure210 && (pos.y == -39 && pos.x == 31)) ||
                    (GroundOne.WE.TruthTreasure211 && (pos.y == -15 && pos.x == 13)) ||
                    (GroundOne.WE.TruthTreasure212 && (pos.y == -6 && pos.x == 29)) ||
                    (GroundOne.WE.TruthTreasure213 && (pos.y == -39 && pos.x == 23)) ||
                    (GroundOne.WE.TruthTreasure214 && (pos.y == -31 && pos.x == 22)) ||
                    (GroundOne.WE.TruthTreasure215 && (pos.y == -19 && pos.x == 22)) ||
                    (GroundOne.WE.TruthTreasure216 && (pos.y == -19 && pos.x == 4)) ||
                    (GroundOne.WE.TruthTreasure217 && (pos.y == -28 && pos.x == 6)) ||
                    (GroundOne.WE.TruthTreasure218 && (pos.y == -39 && pos.x == 10))) && GroundOne.WE.DungeonArea == 2) ||
                (((GroundOne.WE.TruthTreasure301 && (pos.y == -0 && pos.x == 3)) ||
                    (GroundOne.WE.TruthTreasure302 && (pos.y == -39 && pos.x == 3)) ||
                    (GroundOne.WE.TruthTreasure303 && (pos.y == -4 && pos.x == 19)) ||
                    (GroundOne.WE.TruthTreasure304 && (pos.y == -24 && pos.x == 19)) ||
                    (GroundOne.WE.TruthTreasure305 && (pos.y == -9 && pos.x == 1)) ||
                    (GroundOne.WE.TruthTreasure306 && (pos.y == -24 && pos.x == 2)) ||
                    (GroundOne.WE.TruthTreasure307 && (pos.y == -33 && pos.x == 47)) ||
                    (GroundOne.WE.TruthTreasure308 && (pos.y == -7 && pos.x == 20)) ||
                    (GroundOne.WE.TruthTreasure309 && (pos.y == -17 && pos.x == 45)) ||
                    (GroundOne.WE.TruthTreasure310 && (pos.y == -38 && pos.x == 31)) ||
                    (GroundOne.WE.TruthTreasure311 && (pos.y == -27 && pos.x == 48)) ||
                    (GroundOne.WE.TruthTreasure312 && (pos.y == -34 && pos.x == 40))) && GroundOne.WE.DungeonArea == 3) ||
                (((GroundOne.WE.TruthTreasure401 && (pos.y == -15 && pos.x == 43)) ||
                    (GroundOne.WE.TruthTreasure402 && (pos.y == -12 && pos.x == 42)) ||
                    (GroundOne.WE.TruthTreasure403 && (pos.y == -0 && pos.x == 44)) ||
                    (GroundOne.WE.TruthTreasure404 && (pos.y == -7 && pos.x == 29)) ||
                    (GroundOne.WE.TruthTreasure405 && (pos.y == -12 && pos.x == 29)) ||
                    (GroundOne.WE.TruthTreasure406 && (pos.y == -3 && pos.x == 46)) ||
                    (GroundOne.WE.TruthTreasure407 && (pos.y == -10 && pos.x == 59)) ||
                    (GroundOne.WE.TruthTreasure408 && (pos.y == -9 && pos.x == 52)) ||
                    (GroundOne.WE.dungeonEvent4_key1_1 && (pos.y == -16 && pos.x == 46)) ||
                    (GroundOne.WE.dungeonEvent4_key1_2 && (pos.y == -11 && pos.x == 47)) ||
                    (GroundOne.WE.dungeonEvent4_key1_3 && (pos.y == -8 && pos.x == 37)) ||
                    (GroundOne.WE.dungeonEvent4_key1_4 && (pos.y == -4 && pos.x == 32)) ||
                    (GroundOne.WE.dungeonEvent4_key1_5 && (pos.y == -10 && pos.x == 30)) ||
                    (GroundOne.WE.dungeonEvent4_key1_6 && (pos.y == -13 && pos.x == 40)) ||
                    (GroundOne.WE.dungeonEvent4_key1_7 && (pos.y == -1 && pos.x == 51)) ||
                    (GroundOne.WE.dungeonEvent4_key1_8 && (pos.y == -4 && pos.x == 56)) ||
                    (GroundOne.WE.dungeonEvent4_key1_9 && (pos.y == -16 && pos.x == 54)) ||
                    (GroundOne.WE.TruthTreasure409 && (pos.y == -7 && pos.x == 0)) ||
                    (GroundOne.WE.TruthTreasure410 && (pos.y == -9 && pos.x == 3)) ||
                    (GroundOne.WE.TruthTreasure411 && (pos.y == -11 && pos.x == 1)) ||
                    (GroundOne.WE.TruthTreasure412 && (pos.y == -12 && pos.x == 5)) ||
                    (GroundOne.WE.TruthTreasure413 && (pos.y == -13 && pos.x == 8)) ||
                    (GroundOne.WE.TruthTreasure414 && (pos.y == -15 && pos.x == 7)) ||
                    (GroundOne.WE.TruthTreasure415 && (pos.y == -20 && pos.x == 4)) ||
                    (GroundOne.WE.TruthTreasure416 && (pos.y == -21 && pos.x == 13)) ||
                    (GroundOne.WE.TruthTreasure417 && (pos.y == -24 && pos.x == 2)) ||
                    (GroundOne.WE.TruthTreasure418 && (pos.y == -23 && pos.x == 3)) ||
                    (GroundOne.WE.TruthTreasure419 && (pos.y == -23 && pos.x == 7)) ||
                    (GroundOne.WE.TruthTreasure420 && (pos.y == -24 && pos.x == 10)) ||
                    (GroundOne.WE.TruthTreasure421 && (pos.y == -23 && pos.x == 13)) ||
                    (GroundOne.WE.TruthTreasure422 && (pos.y == -20 && pos.x == 14)) ||
                    (GroundOne.WE.dungeonEvent4_key2_1 && (pos.y == -6 && pos.x == 1)) ||
                    (GroundOne.WE.dungeonEvent4_key2_2 && (pos.y == -10 && pos.x == 10)) ||
                    (GroundOne.WE.dungeonEvent4_key2_3 && (pos.y == -15 && pos.x == 1)) ||
                    (GroundOne.WE.dungeonEvent4_key2_4 && (pos.y == -18 && pos.x == 8)) ||
                    (GroundOne.WE.dungeonEvent4_key2_5 && (pos.y == -23 && pos.x == 18)) ||
                    (GroundOne.WE.dungeonEvent4_key22_1 && (pos.y == -0 && pos.x == 0)) ||
                    (GroundOne.WE.dungeonEvent4_key22_2 && (pos.y == -7 && pos.x == 13)) ||
                    (GroundOne.WE.dungeonEvent4_key22_3 && (pos.y == -0 && pos.x == 18)) ||
                    (GroundOne.WE.dungeonEvent4_key22_4 && (pos.y == -7 && pos.x == 28)) ||
                    (GroundOne.WE.dungeonEvent4_key22_5 && (pos.y == -13 && pos.x == 22)) ||
                    (GroundOne.WE.dungeonEvent4_key23_1 && (pos.y == -9 && pos.x == 13)) ||
                    (GroundOne.WE.dungeonEvent4_key23_2 && (pos.y == -9 && pos.x == 18)) ||
                    (GroundOne.WE.dungeonEvent4_key23_3 && (pos.y == -14 && pos.x == 22)) ||
                    (GroundOne.WE.dungeonEvent4_key23_4 && (pos.y == -17 && pos.x == 23)) ||
                    (GroundOne.WE.dungeonEvent4_key23_5 && (pos.y == -15 && pos.x == 27)) ||
                    (GroundOne.WE.TruthTreasure423 && (pos.y == -6 && pos.x == 8)) ||
                    (GroundOne.WE.TruthTreasure424 && (pos.y == -3 && pos.x == 10)) ||
                    (GroundOne.WE.TruthTreasure425 && (pos.y == -0 && pos.x == 5)) ||
                    (GroundOne.WE.TruthTreasure426 && (pos.y == -0 && pos.x == 12)) ||
                    (GroundOne.WE.TruthTreasure427 && (pos.y == -0 && pos.x == 27)) ||
                    (GroundOne.WE.TruthTreasure428 && (pos.y == -2 && pos.x == 27)) ||
                    (GroundOne.WE.TruthTreasure429 && (pos.y == -11 && pos.x == 27)) ||
                    (GroundOne.WE.TruthTreasure430 && (pos.y == -13 && pos.x == 25)) ||
                    (GroundOne.WE.TruthTreasure431 && (pos.y == -4 && pos.x == 18)) ||
                    (GroundOne.WE.TruthTreasure432 && (pos.y == -2 && pos.x == 23)) ||
                    (GroundOne.WE.TruthTreasure433 && (pos.y == -6 && pos.x == 24)) ||
                    (GroundOne.WE.TruthTreasure434 && (pos.y == -6 && pos.x == 18)) ||
                    (GroundOne.WE.TruthTreasure435 && (pos.y == -9 && pos.x == 15)) ||
                    (GroundOne.WE.TruthTreasure436 && (pos.y == -10 && pos.x == 17)) ||
                    (GroundOne.WE.TruthTreasure437 && (pos.y == -10 && pos.x == 21)) ||
                    (GroundOne.WE.TruthTreasure438 && (pos.y == -14 && pos.x == 17)) ||
                    (GroundOne.WE.TruthTreasure439 && (pos.y == -12 && pos.x == 20)) ||
                    (GroundOne.WE.TruthTreasure440 && (pos.y == -14 && pos.x == 20)) ||
                    (GroundOne.WE.TruthTreasure441 && (pos.y == -15 && pos.x == 23)) ||
                    (GroundOne.WE.TruthTreasure442 && (pos.y == -17 && pos.x == 27)) ||
                    (GroundOne.WE.TruthTreasure443 && (pos.y == -29 && pos.x == 7)) ||
                    (GroundOne.WE.TruthTreasure444 && (pos.y == -35 && pos.x == 10)) ||
                    (GroundOne.WE.TruthTreasure445 && (pos.y == -33 && pos.x == 14)) ||
                    (GroundOne.WE.TruthTreasure446 && (pos.y == -36 && pos.x == 3)) ||
                    (GroundOne.WE.TruthTreasure447 && (pos.y == -34 && pos.x == 5)) ||
                    (GroundOne.WE.TruthTreasure448 && (pos.y == -39 && pos.x == 10)) ||
                    (GroundOne.WE.TruthTreasure449 && (pos.y == -36 && pos.x == 12)) ||
                    (GroundOne.WE.TruthTreasure450 && (pos.y == -39 && pos.x == 14)) ||
                    (GroundOne.WE.dungeonEvent4_key41_1 && (pos.y == -35 && pos.x == 40)) ||
                    (GroundOne.WE.dungeonEvent4_key41_2 && (pos.y == -36 && pos.x == 33)) ||
                    (GroundOne.WE.dungeonEvent4_key41_3 && (pos.y == -37 && pos.x == 31)) ||
                    (GroundOne.WE.dungeonEvent4_key41_4 && (pos.y == -37 && pos.x == 39)) ||
                    (GroundOne.WE.dungeonEvent4_key41_5 && (pos.y == -37 && pos.x == 35)) ||
                    (GroundOne.WE.TruthTreasure451 && (pos.y == -36 && pos.x == 37)) ||
                    (GroundOne.WE.TruthTreasure452 && (pos.y == -36 && pos.x == 29)) ||
                    (GroundOne.WE.TruthTreasure453 && (pos.y == -39 && pos.x == 39)) ||
                    (GroundOne.WE.TruthTreasure454 && (pos.y == -39 && pos.x == 41)) ||
                    (GroundOne.WE.dungeonEvent4_key42_1 && (pos.y == -31 && pos.x == 50)) ||
                    (GroundOne.WE.dungeonEvent4_key42_2 && (pos.y == -33 && pos.x == 53)) ||
                    (GroundOne.WE.dungeonEvent4_key42_3 && (pos.y == -34 && pos.x == 58)) ||
                    (GroundOne.WE.dungeonEvent4_key42_4 && (pos.y == -30 && pos.x == 56)) ||
                    (GroundOne.WE.dungeonEvent4_key42_5 && (pos.y == -31 && pos.x == 58)) ||
                    (GroundOne.WE.dungeonEvent4_key42_6 && (pos.y == -27 && pos.x == 55)) ||
                    (GroundOne.WE.dungeonEvent4_key42_7 && (pos.y == -24 && pos.x == 55)) ||
                    (GroundOne.WE.dungeonEvent4_key42_8 && (pos.y == -24 && pos.x == 58)) ||
                    (GroundOne.WE.TruthTreasure455 && (pos.y == -32 && pos.x == 55)) ||
                    (GroundOne.WE.TruthTreasure456 && (pos.y == -31 && pos.x == 56)) ||
                    (GroundOne.WE.TruthTreasure457 && (pos.y == -28 && pos.x == 59)) ||
                    (GroundOne.WE.TruthTreasure458 && (pos.y == -22 && pos.x == 59)) ||
                    (GroundOne.WE.TruthTreasure459 && (pos.y == -21 && pos.x == 50)))&& GroundOne.WE.DungeonArea == 4) ||
                (((GroundOne.WE2.SeekerEvent1002 && (pos.y == -25 && pos.x == 27)) ||
                    (GroundOne.WE2.SeekerEvent1003 && (pos.y == -24 && pos.x == 27)) ||
                    (GroundOne.WE2.SeekerEvent1004 && (pos.y == -31 && pos.x == 26)) ||
                    (GroundOne.WE2.SeekerEvent1005 && (pos.y == -30 && pos.x == 43)) ||
                    (GroundOne.WE2.SeekerEvent1006 && (pos.y == -32 && pos.x == 27)) ||
                    (GroundOne.WE2.SeekerEvent1007 && (pos.y == -28 && pos.x == 44)) ||
                    (GroundOne.WE2.SeekerEvent1008 && (pos.y == -27 && pos.x == 31)) ||
                    (GroundOne.WE2.SeekerEvent1009 && (pos.y == -28 && pos.x == 38))) && GroundOne.WE.DungeonArea == 4)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void UpdateFieldElement(Vector3 pos)
        {
            int number = Method.GetTileNumber(pos);
            for (int ii = 0; ii < this.objTreasureNum.Count; ii++)
            {
                if (this.objTreasureNum[ii] == number)
                {
                    if (DetectOpenTreasure(pos))
                    {
                        this.objTreasureList[ii].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.TREASURE_BOX_OPEN);
                    }
                    else
                    {
                        this.objTreasureList[ii].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.TREASURE_BOX);
                    }
                    break;
                }
            }
        }

        private void ReturnToNormal()
        {
            this.Background.GetComponent<Image>().color = Color.white;
            this.Background.GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.groupDayLabel.SetActive(true);
            this.groupPlayerList.SetActive(true);
            this.GroupsubMenu.SetActive(true);
            this.GroupMenu.SetActive(true);
        }

        private void TurnToBlack()
        {
            this.Background.GetComponent<Image>().color = Color.black;
            this.Background.GetComponent<Image>().sprite = null;
            this.groupDayLabel.SetActive(false);
            this.groupPlayerList.SetActive(false);
            this.GroupsubMenu.SetActive(false);
            this.GroupMenu.SetActive(false);
        }

        private void TurnToWhite()
        {
            this.Background.GetComponent<Image>().color = Color.white;
            this.Background.GetComponent<Image>().sprite = null;
            this.groupDayLabel.SetActive(false);
            this.groupPlayerList.SetActive(false);
            this.GroupsubMenu.SetActive(false);
            this.GroupMenu.SetActive(false);
        }

        private void CopyShadowToMain()
        {
            GroundOne.MC.MainWeapon = GroundOne.ShadowMC.MainWeapon;
            GroundOne.MC.SubWeapon = GroundOne.ShadowMC.SubWeapon;
            GroundOne.MC.MainArmor = GroundOne.ShadowMC.MainArmor;
            GroundOne.MC.Accessory = GroundOne.ShadowMC.Accessory;
            GroundOne.MC.Accessory2 = GroundOne.ShadowMC.Accessory2;

            GroundOne.SC.MainWeapon = GroundOne.ShadowSC.MainWeapon;
            GroundOne.SC.SubWeapon = GroundOne.ShadowSC.SubWeapon;
            GroundOne.SC.MainArmor = GroundOne.ShadowSC.MainArmor;
            GroundOne.SC.Accessory = GroundOne.ShadowSC.Accessory;
            GroundOne.SC.Accessory2 = GroundOne.ShadowSC.Accessory2;

            GroundOne.TC.MainWeapon = GroundOne.ShadowTC.MainWeapon;
            GroundOne.TC.SubWeapon = GroundOne.ShadowTC.SubWeapon;
            GroundOne.TC.MainArmor = GroundOne.ShadowTC.MainArmor;
            GroundOne.TC.Accessory = GroundOne.ShadowTC.Accessory;
            GroundOne.TC.Accessory2 = GroundOne.ShadowTC.Accessory2;

            // after 再戦時、ポーションのスタック数などを実際に減らしてみて、数が減ったままにならないかどうか確認。
            if (GroundOne.WE.AvailableFirstCharacter)
            {
                GroundOne.MC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowMC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.MC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            if (GroundOne.WE.AvailableSecondCharacter)
            {
                GroundOne.SC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowSC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.SC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            if (GroundOne.WE.AvailableThirdCharacter)
            {
                GroundOne.TC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowTC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.TC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null).ToString())), null);
                        pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null).ToString())), null);
                        pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null).ToString())), null);
                        pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null).ToString())), null);
                        pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            Type type2 = GroundOne.WE.GetType();
            foreach (PropertyInfo pi in type2.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (string)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
            }

            Type type3 = GroundOne.WE2.GetType();
            foreach (PropertyInfo pi in type3.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
            }
        }
        private void CreateShadowData()
        {
            GameObject shadowObjMC = new GameObject();
            GroundOne.ShadowMC = shadowObjMC.AddComponent<MainCharacter>();

            GameObject shadowObjSC = new GameObject();
            GroundOne.ShadowSC = shadowObjSC.AddComponent<MainCharacter>();

            GameObject shadowObjTC = new GameObject();
            GroundOne.ShadowTC = shadowObjTC.AddComponent<MainCharacter>();

            GroundOne.ShadowMC.MainWeapon = GroundOne.MC.MainWeapon;
            GroundOne.ShadowMC.SubWeapon = GroundOne.MC.SubWeapon;
            GroundOne.ShadowMC.MainArmor = GroundOne.MC.MainArmor;
            GroundOne.ShadowMC.Accessory = GroundOne.MC.Accessory;
            GroundOne.ShadowMC.Accessory2 = GroundOne.MC.Accessory2;
            GroundOne.ShadowMC.ReplaceBackPack(GroundOne.MC.GetBackPackInfo());

            GroundOne.ShadowSC.MainWeapon = GroundOne.SC.MainWeapon;
            GroundOne.ShadowSC.SubWeapon = GroundOne.SC.SubWeapon;
            GroundOne.ShadowSC.MainArmor = GroundOne.SC.MainArmor;
            GroundOne.ShadowSC.Accessory = GroundOne.SC.Accessory;
            GroundOne.ShadowSC.Accessory2 = GroundOne.SC.Accessory2;
            GroundOne.ShadowSC.ReplaceBackPack(GroundOne.SC.GetBackPackInfo());

            GroundOne.ShadowTC.MainWeapon = GroundOne.TC.MainWeapon;
            GroundOne.ShadowTC.SubWeapon = GroundOne.TC.SubWeapon;
            GroundOne.ShadowTC.MainArmor = GroundOne.TC.MainArmor;
            GroundOne.ShadowTC.Accessory = GroundOne.TC.Accessory;
            GroundOne.ShadowTC.Accessory2 = GroundOne.TC.Accessory2;
            GroundOne.ShadowTC.ReplaceBackPack(GroundOne.TC.GetBackPackInfo());

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.MC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowSC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.SC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowTC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.TC, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.MC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowSC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.SC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowTC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.TC, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            Type type2 = GroundOne.WE.GetType();
            foreach (PropertyInfo pi in type2.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (string)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
            }

            Type type3 = GroundOne.WE2.GetType();
            foreach (PropertyInfo pi in type3.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
            }
        }

        private void UpdateUnknownTileArea11()
        {
            for (int ii = 13; ii <= 22; ii++)
            {
                for (int jj = 27; jj <= 34; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea12()
        {
            for (int ii = 1; ii <= 5; ii++)
            {
                for (int jj = 1; jj <= 12; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea24()
        {
            for (int ii = 51; ii <= 53; ii++)
            {
                for (int jj = 13; jj <= 15; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea25()
        {
            for (int ii = 37; ii <= 39; ii++)
            {
                for (int jj = 4; jj <= 6; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea26()
        {
            for (int ii = 55; ii <= 57; ii++)
            {
                for (int jj = 10; jj <= 12; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea27()
        {
            for (int ii = 36; ii <= 44; ii++)
            {
                for (int jj = 33; jj <= 35; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea28()
        {
            for (int ii = 48; ii <= 56; ii++)
            {
                for (int jj = 33; jj <= 35; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea29()
        {
            for (int ii = 0; ii <= 28; ii++)
            {
                for (int jj = 0; jj <= 12; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea2_10()
        {
            for (int ii = 29; ii <= 30; ii++)
            {
                for (int jj = 5; jj <= 7; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_11()
        {
            for (int ii = 20; ii <= 23; ii++)
            {
                for (int jj = 35; jj <= 37; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_12()
        {
            for (int ii = 19; ii <= 23; ii++)
            {
                for (int jj = 24; jj <= 27; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_13()
        {
            for (int ii = 11; ii <= 16; ii++)
            {
                for (int jj = 18; jj <= 22; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_14()
        {
            for (int ii = 1; ii <= 7; ii++)
            {
                for (int jj = 22; jj <= 27; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_15()
        {
            for (int ii = 1; ii <= 8; ii++)
            {
                for (int jj = 31; jj <= 37; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea2_16()
        {
            for (int ii = 10; ii <= 18; ii++)
            {
                for (int jj = 29; jj <= 36; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo2[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea3_1()
        {
            for (int ii = 3; ii <= 9; ii++)
            {
                for (int jj = 19; jj <= 20; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
            for (int ii = 3; ii <= 3; ii++)
            {
                for (int jj = 1; jj <= 38; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea3_0_1()
        {
            UpdateUnknownTile_Rectangle(19, 15, 25, 17, true);
        }

        private void UpdateUnknownTileArea3_0_2()
        {
            UpdateUnknownTile_Rectangle(1, 15, 3, 17, true);
        }

        private void UpdateUnknownTileArea3_0_3()
        {
            UpdateUnknownTile_Rectangle(8, 6, 17, 9, true);
        }

        private void UpdateUnknownTileArea3_0_4()
        {
            UpdateUnknownTile_Rectangle(22, 6, 24, 9, true);
        }

        private void UpdateUnknownTileArea3_0_5()
        {
            UpdateUnknownTile_Rectangle(10, 15, 17, 17, true);
        }

        private void UpdateUnknownTileArea3_0_6()
        {
            UpdateUnknownTile_Rectangle(35, 11, 38, 17, true);
        }

        private void UpdateUnknownTileArea3_0_7()
        {
            UpdateUnknownTile_Rectangle(16, 11, 25, 13, true);
        }

        private void UpdateUnknownTileArea3_0_8()
        {
            UpdateUnknownTile_Rectangle(1, 6, 6, 10, true);
        }

        private void UpdateUnknownTileArea3_0_9()
        {
            UpdateUnknownTile_Rectangle(0, 12, 7, 13, true);
        }

        private void UpdateUnknownTileArea3_0_10()
        {
            UpdateUnknownTile_Rectangle(36, 6, 38, 9, true);
        }

        private void UpdateUnknownTileArea3_0_11()
        {
            UpdateUnknownTile_Rectangle(27, 12, 30, 18, true);
        }

        private void UpdateUnknownTileArea3_0_12()
        {
            UpdateUnknownTile_Rectangle(32, 12, 33, 17, true);
        }

        private void UpdateUnknownTileArea3_0_13()
        {
            UpdateUnknownTile_Rectangle(33, 6, 34, 9, true);
        }

        private void UpdateUnknownTileArea3_0_14()
        {
            UpdateUnknownTile_Rectangle(9, 11, 14, 13, true);
        }

        private void UpdateUnknownTileArea3_0_15()
        {
            UpdateUnknownTile_Rectangle(5, 15, 8, 17, true);
        }

        private void UpdateUnknownTileArea3_0_16()
        {
            UpdateUnknownTile_Rectangle(26, 6, 31, 10, true);
        }

        private void UpdateUnknownTileArea3_Area1()
        {
            UpdateUnknownTile_Rectangle(14, 21, 16, 27, true);
        }
        private void UpdateUnknownTileArea3_Area2()
        {
            UpdateUnknownTileArea3_One(29, 40, true);
            UpdateUnknownTileArea3_One(30, 40, true);
            UpdateUnknownTileArea3_One(31, 40, true);
            UpdateUnknownTile_Rectangle(31, 41, 34, 45, true);
        }
        private void UpdateUnknownTileArea3_Area3()
        {
            UpdateUnknownTileArea3_One(7, 33, true);
            UpdateUnknownTileArea3_One(8, 33, true);
            UpdateUnknownTileArea3_One(9, 33, true);
            UpdateUnknownTileArea3_One(10, 33, true);
            UpdateUnknownTileArea3_One(11, 33, true);
            UpdateUnknownTileArea3_One(10, 34, true);
            UpdateUnknownTileArea3_One(10, 35, true);
            UpdateUnknownTileArea3_One(11, 35, true);
            UpdateUnknownTileArea3_One(12, 35, true);
            UpdateUnknownTileArea3_One(13, 35, true);
            UpdateUnknownTileArea3_One(13, 34, true);
            UpdateUnknownTileArea3_One(13, 33, true);
            UpdateUnknownTileArea3_One(13, 32, true);
        }
        private void UpdateUnknownTileArea3_Area4()
        {
            UpdateUnknownTileArea3_One(23, 29, true);
            UpdateUnknownTileArea3_One(24, 29, true);
            UpdateUnknownTileArea3_One(25, 29, true);
            UpdateUnknownTile_Rectangle(26, 26, 28, 32, true);
        }
        private void UpdateUnknownTileArea3_Area5()
        {
            UpdateUnknownTileArea3_One(20, 31, true);
            UpdateUnknownTileArea3_One(20, 32, true);
            UpdateUnknownTileArea3_One(20, 33, true);
            UpdateUnknownTileArea3_One(20, 34, true);
            UpdateUnknownTileArea3_One(20, 35, true);
            UpdateUnknownTileArea3_One(20, 36, true);
            UpdateUnknownTileArea3_One(21, 36, true);
            UpdateUnknownTileArea3_One(22, 36, true);
            UpdateUnknownTileArea3_One(23, 36, true);
            UpdateUnknownTileArea3_One(24, 36, true);
            UpdateUnknownTileArea3_One(25, 36, true);
            UpdateUnknownTileArea3_One(26, 36, true);
            UpdateUnknownTileArea3_One(27, 36, true);
            UpdateUnknownTileArea3_One(28, 36, true);
            UpdateUnknownTileArea3_One(29, 36, true);
        }
        private void UpdateUnknownTileArea3_Area6()
        {
            UpdateUnknownTileArea3_One(4, 52, true);
            UpdateUnknownTileArea3_One(4, 51, true);
            UpdateUnknownTileArea3_One(4, 50, true);
            UpdateUnknownTileArea3_One(4, 49, true);
            UpdateUnknownTileArea3_One(5, 49, true);
            UpdateUnknownTileArea3_One(6, 49, true);
            UpdateUnknownTileArea3_One(7, 49, true);
            UpdateUnknownTileArea3_One(8, 49, true);
            UpdateUnknownTileArea3_One(9, 49, true);
            UpdateUnknownTileArea3_One(9, 50, true);
        }
        private void UpdateUnknownTileArea3_Area7()
        {
            UpdateUnknownTileArea3_One(12, 52, true);
            UpdateUnknownTileArea3_One(12, 51, true);
            UpdateUnknownTileArea3_One(12, 50, true);
            UpdateUnknownTileArea3_One(12, 49, true);
            UpdateUnknownTileArea3_One(12, 48, true);
            UpdateUnknownTileArea3_One(12, 47, true);
            UpdateUnknownTileArea3_One(12, 46, true);
            UpdateUnknownTileArea3_One(12, 45, true);
            UpdateUnknownTileArea3_One(13, 45, true);
            UpdateUnknownTileArea3_One(14, 45, true);
        }
        private void UpdateUnknownTileArea3_Area8()
        {
            UpdateUnknownTileArea3_One(24, 42, true);
            UpdateUnknownTileArea3_One(24, 41, true);
            UpdateUnknownTileArea3_One(24, 40, true);
            UpdateUnknownTileArea3_One(24, 39, true);
            UpdateUnknownTileArea3_One(24, 38, true);
            UpdateUnknownTileArea3_One(23, 38, true);
            UpdateUnknownTileArea3_One(22, 38, true);
            UpdateUnknownTileArea3_One(21, 38, true);
            UpdateUnknownTileArea3_One(20, 38, true);
            UpdateUnknownTileArea3_One(19, 38, true);
            UpdateUnknownTileArea3_One(18, 38, true);
            UpdateUnknownTileArea3_One(17, 38, true);
            UpdateUnknownTileArea3_One(16, 38, true);
            UpdateUnknownTile_Rectangle(13, 38, 15, 41, true);
        }
        private void UpdateUnknownTileArea3_Area9()
        {
            UpdateUnknownTileArea3_One(36, 29, true);
            UpdateUnknownTileArea3_One(35, 29, true);
            UpdateUnknownTileArea3_One(34, 29, true);
            UpdateUnknownTileArea3_One(33, 29, true);
            UpdateUnknownTile_Rectangle(30, 26, 32, 32, true);
        }
        private void UpdateUnknownTileArea3_Area10()
        {
            UpdateUnknownTileArea3_One(17, 35, true);
            UpdateUnknownTileArea3_One(16, 35, true);
            UpdateUnknownTileArea3_One(15, 35, true);
            UpdateUnknownTileArea3_One(15, 34, true);
            UpdateUnknownTileArea3_One(15, 33, true);
            UpdateUnknownTileArea3_One(16, 33, true);
            UpdateUnknownTileArea3_One(17, 33, true);
            UpdateUnknownTileArea3_One(17, 32, true);
            UpdateUnknownTileArea3_One(17, 31, true);
            UpdateUnknownTileArea3_One(16, 31, true);
            UpdateUnknownTileArea3_One(15, 31, true);
            UpdateUnknownTileArea3_One(14, 31, true);
            UpdateUnknownTileArea3_One(13, 31, true);
            UpdateUnknownTileArea3_One(12, 31, true);
            UpdateUnknownTileArea3_One(11, 31, true);
            UpdateUnknownTileArea3_One(10, 31, true);
            UpdateUnknownTileArea3_One(9, 31, true);
            UpdateUnknownTileArea3_One(8, 31, true);
            UpdateUnknownTileArea3_One(7, 31, true);
            UpdateUnknownTileArea3_One(7, 30, true);
            UpdateUnknownTileArea3_One(6, 30, true);
            UpdateUnknownTileArea3_One(6, 29, true);
            UpdateUnknownTileArea3_One(6, 28, true);
            UpdateUnknownTileArea3_One(6, 27, true);
            UpdateUnknownTileArea3_One(6, 26, true);
            UpdateUnknownTileArea3_One(6, 25, true);
            UpdateUnknownTileArea3_One(6, 24, true);
            UpdateUnknownTileArea3_One(7, 24, true);
            UpdateUnknownTileArea3_One(8, 24, true);
            UpdateUnknownTileArea3_One(9, 24, true);
            UpdateUnknownTileArea3_One(10, 24, true);
            UpdateUnknownTileArea3_One(11, 24, true);
            UpdateUnknownTileArea3_One(5, 24, true);
            UpdateUnknownTileArea3_One(4, 24, true);
            UpdateUnknownTileArea3_One(3, 24, true);
            UpdateUnknownTileArea3_One(12, 32, true);
            UpdateUnknownTileArea3_One(12, 33, true);
            UpdateUnknownTileArea3_One(12, 34, true);
            UpdateUnknownTileArea3_One(11, 34, true);
            UpdateUnknownTileArea3_One(17, 30, true);
            UpdateUnknownTileArea3_One(17, 29, true);
            UpdateUnknownTileArea3_One(16, 29, true);
            UpdateUnknownTileArea3_One(15, 29, true);
        }
        private void UpdateUnknownTileArea3_Area11()
        {
            UpdateUnknownTileArea3_One(28, 43, true);
            UpdateUnknownTileArea3_One(28, 44, true);
            UpdateUnknownTileArea3_One(28, 45, true);
            UpdateUnknownTileArea3_One(28, 46, true);
            UpdateUnknownTileArea3_One(27, 46, true);
            UpdateUnknownTileArea3_One(26, 46, true);
            UpdateUnknownTileArea3_One(25, 46, true);
            UpdateUnknownTileArea3_One(24, 46, true);
            UpdateUnknownTileArea3_One(23, 46, true);
            UpdateUnknownTileArea3_One(22, 46, true);
            UpdateUnknownTileArea3_One(22, 45, true);
            UpdateUnknownTileArea3_One(22, 44, true);
            UpdateUnknownTileArea3_One(22, 43, true);
            UpdateUnknownTileArea3_One(22, 42, true);
            UpdateUnknownTileArea3_One(22, 41, true);
            UpdateUnknownTileArea3_One(21, 41, true);
            UpdateUnknownTileArea3_One(20, 41, true);
            UpdateUnknownTileArea3_One(19, 41, true);
            UpdateUnknownTileArea3_One(18, 41, true);
        }
        private void UpdateUnknownTileArea3_Area12()
        {
            UpdateUnknownTileArea3_One(38, 38, true);
            UpdateUnknownTileArea3_One(38, 37, true);
            UpdateUnknownTileArea3_One(37, 37, true);
            UpdateUnknownTileArea3_One(37, 36, true);
            UpdateUnknownTileArea3_One(37, 35, true);
            UpdateUnknownTileArea3_One(37, 34, true);
            UpdateUnknownTileArea3_One(37, 33, true);
            UpdateUnknownTileArea3_One(37, 32, true);
            UpdateUnknownTileArea3_One(37, 31, true);
            UpdateUnknownTileArea3_One(37, 30, true);
            UpdateUnknownTileArea3_One(37, 29, true);
            UpdateUnknownTileArea3_One(37, 28, true);
            UpdateUnknownTileArea3_One(36, 28, true);
            UpdateUnknownTileArea3_One(35, 28, true);
            UpdateUnknownTileArea3_One(34, 28, true);
            UpdateUnknownTileArea3_One(33, 28, true);
            UpdateUnknownTileArea3_One(33, 27, true);
            UpdateUnknownTileArea3_One(33, 26, true);
            UpdateUnknownTileArea3_One(33, 25, true);
            UpdateUnknownTileArea3_One(33, 24, true);
            UpdateUnknownTileArea3_One(33, 23, true);
            UpdateUnknownTileArea3_One(33, 22, true);
            UpdateUnknownTileArea3_One(33, 21, true);
            UpdateUnknownTileArea3_One(36, 37, true);
            UpdateUnknownTileArea3_One(36, 38, true);
            UpdateUnknownTileArea3_One(35, 38, true);
            UpdateUnknownTileArea3_One(34, 38, true);
            UpdateUnknownTileArea3_One(33, 38, true);
            UpdateUnknownTileArea3_One(32, 38, true);
            UpdateUnknownTileArea3_One(31, 38, true);
            UpdateUnknownTileArea3_One(30, 38, true);
        }
        private void UpdateUnknownTileArea3_Area13()
        {
            UpdateUnknownTileArea3_One(3, 42, true);
            UpdateUnknownTileArea3_One(4, 42, true);
            UpdateUnknownTileArea3_One(5, 42, true);
            UpdateUnknownTileArea3_One(6, 42, true);
            UpdateUnknownTileArea3_One(7, 42, true);
            UpdateUnknownTileArea3_One(8, 42, true);
            UpdateUnknownTileArea3_One(9, 42, true);
            UpdateUnknownTileArea3_One(10, 42, true);
        }
        private void UpdateUnknownTileArea3_Area14()
        {
            UpdateUnknownTileArea3_One(38, 53, true);
            UpdateUnknownTileArea3_One(38, 52, true);
            UpdateUnknownTile_Rectangle(34, 49, 38, 51, true);
        }
        private void UpdateUnknownTileArea3_Area15()
        {
            UpdateUnknownTileArea3_One(22, 21, true);
            UpdateUnknownTileArea3_One(22, 22, true);
            UpdateUnknownTileArea3_One(22, 23, true);
            UpdateUnknownTileArea3_One(23, 23, true);
            UpdateUnknownTileArea3_One(24, 23, true);
            UpdateUnknownTileArea3_One(25, 23, true);
            UpdateUnknownTileArea3_One(26, 23, true);
            UpdateUnknownTileArea3_One(27, 23, true);
            UpdateUnknownTileArea3_One(28, 23, true);
            UpdateUnknownTileArea3_One(22, 24, true);
            UpdateUnknownTileArea3_One(22, 25, true);
        }
        private void UpdateUnknownTileArea3_Area16()
        {
            UpdateUnknownTileArea3_One(16, 40, true);
            UpdateUnknownTileArea3_One(16, 39, true);
            UpdateUnknownTileArea3_One(17, 39, true);
            UpdateUnknownTileArea3_One(18, 39, true);
            UpdateUnknownTileArea3_One(19, 39, true);
            UpdateUnknownTileArea3_One(20, 39, true);
            UpdateUnknownTileArea3_One(21, 39, true);
            UpdateUnknownTileArea3_One(22, 39, true);
            UpdateUnknownTileArea3_One(23, 39, true);
        }
        private void UpdateUnknownTileArea3_Area17()
        {
            UpdateUnknownTileArea3_One(33, 30, true);
            UpdateUnknownTileArea3_One(33, 31, true);
            UpdateUnknownTileArea3_One(33, 32, true);
            UpdateUnknownTileArea3_One(33, 33, true);
            UpdateUnknownTileArea3_One(33, 34, true);
            UpdateUnknownTile_Rectangle(31, 35, 35, 37, true);
        }
        private void UpdateUnknownTileArea3_Area18()
        {
            UpdateUnknownTileArea3_One(36, 53, true);
            UpdateUnknownTileArea3_One(35, 53, true);
            UpdateUnknownTileArea3_One(34, 53, true);
            UpdateUnknownTileArea3_One(33, 53, true);
            UpdateUnknownTileArea3_One(32, 53, true);
            UpdateUnknownTile_Rectangle(28, 50, 31, 54, true);
        }
        private void UpdateUnknownTileArea3_Area19()
        {
            UpdateUnknownTileArea3_One(4, 35, true);
            UpdateUnknownTileArea3_One(4, 36, true);
            UpdateUnknownTileArea3_One(4, 37, true);
            UpdateUnknownTileArea3_One(4, 38, true);
            UpdateUnknownTileArea3_One(4, 39, true);
            UpdateUnknownTileArea3_One(5, 39, true);
            UpdateUnknownTileArea3_One(6, 39, true);
            UpdateUnknownTileArea3_One(7, 39, true);
            UpdateUnknownTileArea3_One(8, 39, true);
            UpdateUnknownTileArea3_One(8, 38, true);
            UpdateUnknownTileArea3_One(9, 38, true);
            UpdateUnknownTileArea3_One(10, 38, true);
            UpdateUnknownTileArea3_One(11, 38, true);
            UpdateUnknownTileArea3_One(12, 38, true);
            UpdateUnknownTileArea3_One(12, 39, true);
            UpdateUnknownTileArea3_One(12, 40, true);
            UpdateUnknownTileArea3_One(12, 41, true);
        }
        private void UpdateUnknownTileArea3_Area20()
        {
            UpdateUnknownTileArea3_One(17, 23, true);
            UpdateUnknownTileArea3_One(17, 22, true);
            UpdateUnknownTileArea3_One(17, 21, true);
        }
        private void UpdateUnknownTileArea3_Area21()
        {
            UpdateUnknownTileArea3_One(29, 25, true);
            UpdateUnknownTileArea3_One(28, 25, true);
            UpdateUnknownTileArea3_One(27, 25, true);
            UpdateUnknownTileArea3_One(26, 25, true);
            UpdateUnknownTile_Rectangle(23, 24, 25, 28, true);
        }
        private void UpdateUnknownTileArea3_Area22()
        {
            UpdateUnknownTileArea3_One(4, 45, true);
            UpdateUnknownTileArea3_One(4, 46, true);
            UpdateUnknownTileArea3_One(4, 47, true);
            UpdateUnknownTileArea3_One(5, 47, true);
            UpdateUnknownTileArea3_One(6, 47, true);
            UpdateUnknownTile_Rectangle(7, 45, 9, 47, true);
        }
        private void UpdateUnknownTileArea3_Area23()
        {
            UpdateUnknownTileArea3_One(11, 45, true);
            UpdateUnknownTileArea3_One(11, 46, true);
            UpdateUnknownTileArea3_One(11, 47, true);
            UpdateUnknownTileArea3_One(11, 48, true);
            UpdateUnknownTileArea3_One(11, 49, true);
            UpdateUnknownTileArea3_One(11, 50, true);
            UpdateUnknownTileArea3_One(11, 51, true);
            UpdateUnknownTileArea3_One(11, 52, true);
            UpdateUnknownTileArea3_One(11, 53, true);
            UpdateUnknownTileArea3_One(12, 53, true);
            UpdateUnknownTileArea3_One(13, 53, true);
            UpdateUnknownTileArea3_One(14, 53, true);
            UpdateUnknownTileArea3_One(15, 53, true);
            UpdateUnknownTileArea3_One(16, 53, true);
            UpdateUnknownTileArea3_One(10, 53, true);
            UpdateUnknownTileArea3_One(9, 53, true);
        }
        private void UpdateUnknownTileArea3_Area24()
        {
            UpdateUnknownTileArea3_One(13, 30, true);
            UpdateUnknownTileArea3_One(13, 29, true);
            UpdateUnknownTileArea3_One(13, 28, true);
            UpdateUnknownTileArea3_One(13, 27, true);
            UpdateUnknownTileArea3_One(13, 26, true);
            UpdateUnknownTileArea3_One(13, 25, true);
            UpdateUnknownTileArea3_One(13, 24, true);
            UpdateUnknownTileArea3_One(13, 23, true);
            UpdateUnknownTileArea3_One(13, 22, true);
            UpdateUnknownTileArea3_One(13, 21, true);
            UpdateUnknownTileArea3_One(13, 20, true);
            UpdateUnknownTileArea3_One(14, 20, true);
            UpdateUnknownTileArea3_One(15, 20, true);
            UpdateUnknownTileArea3_One(16, 20, true);
            UpdateUnknownTileArea3_One(17, 20, true);
            UpdateUnknownTileArea3_One(18, 20, true);
            UpdateUnknownTileArea3_One(18, 21, true);
            UpdateUnknownTileArea3_One(18, 22, true);
            UpdateUnknownTileArea3_One(18, 23, true);
            UpdateUnknownTileArea3_One(12, 20, true);
            UpdateUnknownTileArea3_One(11, 20, true);
            UpdateUnknownTileArea3_One(11, 21, true);
        }
        private void UpdateUnknownTileArea3_Area25()
        {
            UpdateUnknownTileArea3_One(29, 26, true);
            UpdateUnknownTileArea3_One(29, 27, true);
            UpdateUnknownTileArea3_One(29, 28, true);
            UpdateUnknownTileArea3_One(29, 29, true);
            UpdateUnknownTileArea3_One(29, 30, true);
            UpdateUnknownTileArea3_One(29, 31, true);
            UpdateUnknownTileArea3_One(29, 32, true);
            UpdateUnknownTile_Rectangle(26, 33, 32, 34, true);
            UpdateUnknownTileArea3_One(29, 33, true);
            UpdateUnknownTileArea3_One(28, 33, true);
            UpdateUnknownTileArea3_One(27, 33, true);
            UpdateUnknownTileArea3_One(26, 33, true);
            UpdateUnknownTileArea3_One(30, 33, true);
            UpdateUnknownTileArea3_One(31, 33, true);
            UpdateUnknownTileArea3_One(32, 33, true);
            UpdateUnknownTileArea3_One(26, 34, true);
            UpdateUnknownTileArea3_One(28, 34, true);
            UpdateUnknownTileArea3_One(30, 34, true);
            UpdateUnknownTileArea3_One(32, 34, true);
        }
        private void UpdateUnknownTileArea3_Area26()
        {
            UpdateUnknownTileArea3_One(4, 30, true);
            UpdateUnknownTileArea3_One(4, 31, true);
            UpdateUnknownTileArea3_One(4, 32, true);
            UpdateUnknownTileArea3_One(5, 32, true);
            UpdateUnknownTileArea3_One(5, 33, true);
            UpdateUnknownTileArea3_One(6, 33, true);
            UpdateUnknownTileArea3_One(6, 34, true);
            UpdateUnknownTileArea3_One(7, 34, true);
            UpdateUnknownTileArea3_One(8, 34, true);
            UpdateUnknownTileArea3_One(9, 34, true);
            UpdateUnknownTileArea3_One(9, 35, true);
        }
        private void UpdateUnknownTileArea3_Area27()
        {
            UpdateUnknownTileArea3_One(30, 41, true);
            UpdateUnknownTileArea3_One(29, 41, true);
            UpdateUnknownTileArea3_One(28, 41, true);
            UpdateUnknownTileArea3_One(27, 41, true);
            UpdateUnknownTileArea3_One(27, 40, true);
        }
        private void UpdateUnknownTileArea3_Area28()
        {
            UpdateUnknownTileArea3_One(15, 51, true);
            UpdateUnknownTileArea3_One(16, 51, true);
            UpdateUnknownTileArea3_One(17, 51, true);
            UpdateUnknownTile_Rectangle(18, 49, 20, 53, true);
        }
        private void UpdateUnknownTileArea3_Area29()
        {
            UpdateUnknownTileArea3_One(36, 36, true);
            UpdateUnknownTileArea3_One(36, 35, true);
            UpdateUnknownTile_Rectangle(34, 30, 36, 34, true);
        }
        private void UpdateUnknownTileArea3_Area30()
        {
            UpdateUnknownTileArea3_One(11, 32, true);
            UpdateUnknownTileArea3_One(10, 32, true);
            UpdateUnknownTileArea3_One(9, 32, true);
            UpdateUnknownTileArea3_One(8, 32, true);
            UpdateUnknownTileArea3_One(7, 32, true);
            UpdateUnknownTileArea3_One(6, 32, true);
            UpdateUnknownTileArea3_One(6, 31, true);
            UpdateUnknownTileArea3_One(5, 31, true);
            UpdateUnknownTileArea3_One(5, 30, true);
            UpdateUnknownTile_Rectangle(3, 25, 5, 29, true);
        }
        private void UpdateUnknownTileArea3_Area31()
        {
            UpdateUnknownTileArea3_One(22, 31, true);
            UpdateUnknownTileArea3_One(22, 32, true);
            UpdateUnknownTileArea3_One(22, 33, true);
            UpdateUnknownTileArea3_One(22, 34, true);
            UpdateUnknownTileArea3_One(22, 35, true);
            UpdateUnknownTileArea3_One(23, 35, true);
            UpdateUnknownTileArea3_One(24, 35, true);
            UpdateUnknownTileArea3_One(24, 34, true);
            UpdateUnknownTileArea3_One(24, 33, true);
            UpdateUnknownTileArea3_One(24, 32, true);
            UpdateUnknownTileArea3_One(24, 31, true);
        }
        private void UpdateUnknownTileArea3_Area32()
        {
            UpdateUnknownTileArea3_One(33, 39, true);
            UpdateUnknownTileArea3_One(34, 39, true);
            UpdateUnknownTileArea3_One(35, 39, true);
            UpdateUnknownTileArea3_One(36, 39, true);
            UpdateUnknownTileArea3_One(36, 40, true);
            UpdateUnknownTileArea3_One(37, 40, true);
            UpdateUnknownTileArea3_One(37, 41, true);
            UpdateUnknownTileArea3_One(38, 41, true);
            UpdateUnknownTileArea3_One(38, 42, true);
            UpdateUnknownTileArea3_One(38, 43, true);
            UpdateUnknownTileArea3_One(39, 43, true);
            UpdateUnknownTileArea3_One(39, 44, true);
            UpdateUnknownTileArea3_One(39, 45, true);
            UpdateUnknownTileArea3_One(39, 46, true);
            UpdateUnknownTileArea3_One(39, 47, true);
        }
        private void UpdateUnknownTileArea3_Area33()
        {
            UpdateUnknownTileArea3_One(0, 41, true);
            UpdateUnknownTileArea3_One(0, 42, true);
            UpdateUnknownTileArea3_One(0, 43, true);
            UpdateUnknownTileArea3_One(0, 44, true);
            UpdateUnknownTileArea3_One(0, 45, true);
            UpdateUnknownTileArea3_One(0, 46, true);
            UpdateUnknownTileArea3_One(0, 47, true);
            UpdateUnknownTileArea3_One(0, 48, true);
            UpdateUnknownTileArea3_One(0, 49, true);
            UpdateUnknownTileArea3_One(0, 50, true);
            UpdateUnknownTileArea3_One(0, 51, true);
            UpdateUnknownTileArea3_One(0, 52, true);
            UpdateUnknownTileArea3_One(0, 53, true);
            UpdateUnknownTileArea3_One(0, 54, true);
            UpdateUnknownTileArea3_One(1, 54, true);
            UpdateUnknownTileArea3_One(2, 54, true);
            UpdateUnknownTileArea3_One(2, 53, true);
            UpdateUnknownTileArea3_One(2, 52, true);
            UpdateUnknownTileArea3_One(2, 51, true);
            UpdateUnknownTileArea3_One(2, 50, true);
            UpdateUnknownTileArea3_One(2, 49, true);
            UpdateUnknownTileArea3_One(2, 48, true);
            UpdateUnknownTileArea3_One(2, 47, true);
            UpdateUnknownTileArea3_One(2, 46, true);
            UpdateUnknownTileArea3_One(2, 45, true);
            UpdateUnknownTileArea3_One(2, 44, true);
            UpdateUnknownTileArea3_One(2, 43, true);
            UpdateUnknownTileArea3_One(3, 43, true);
            UpdateUnknownTileArea3_One(4, 43, true);
            UpdateUnknownTileArea3_One(5, 43, true);
            UpdateUnknownTileArea3_One(6, 43, true);
            UpdateUnknownTileArea3_One(7, 43, true);
            UpdateUnknownTileArea3_One(8, 43, true);
            UpdateUnknownTileArea3_One(9, 43, true);
            UpdateUnknownTileArea3_One(10, 43, true);
            UpdateUnknownTileArea3_One(11, 43, true);
            UpdateUnknownTileArea3_One(11, 42, true);
            UpdateUnknownTileArea3_One(12, 42, true);
            UpdateUnknownTileArea3_One(13, 42, true);
            UpdateUnknownTileArea3_One(14, 42, true);
            UpdateUnknownTileArea3_One(15, 42, true);
            UpdateUnknownTileArea3_One(5, 44, true);
            UpdateUnknownTileArea3_One(5, 45, true);
            UpdateUnknownTileArea3_One(5, 46, true);
            UpdateUnknownTileArea3_One(2, 42, true);
            UpdateUnknownTileArea3_One(2, 41, true);
            UpdateUnknownTileArea3_One(3, 41, true);
            UpdateUnknownTileArea3_One(4, 41, true);
            UpdateUnknownTileArea3_One(5, 41, true);
            UpdateUnknownTileArea3_One(6, 41, true);
            UpdateUnknownTileArea3_One(7, 41, true);
            UpdateUnknownTileArea3_One(8, 41, true);
            UpdateUnknownTileArea3_One(3, 54, true);
            UpdateUnknownTileArea3_One(4, 54, true);
            UpdateUnknownTileArea3_One(5, 54, true);
            UpdateUnknownTileArea3_One(6, 54, true);
            UpdateUnknownTileArea3_One(7, 54, true);
            UpdateUnknownTileArea3_One(8, 54, true);
            UpdateUnknownTileArea3_One(9, 54, true);
            UpdateUnknownTileArea3_One(10, 54, true);
        }
        private void UpdateUnknownTileArea3_Area34()
        {
            UpdateUnknownTileArea3_One(4, 40, true);
            UpdateUnknownTileArea3_One(5, 40, true);
            UpdateUnknownTileArea3_One(6, 40, true);
            UpdateUnknownTileArea3_One(7, 40, true);
            UpdateUnknownTileArea3_One(8, 40, true);
            UpdateUnknownTile_Rectangle(9, 39, 11, 41, true);
        }
        private void UpdateUnknownTileArea3_Area35()
        {
            UpdateUnknownTileArea3_One(11, 54, true);
            UpdateUnknownTileArea3_One(12, 54, true);
            UpdateUnknownTileArea3_One(13, 54, true);
            UpdateUnknownTileArea3_One(14, 54, true);
            UpdateUnknownTileArea3_One(15, 54, true);
            UpdateUnknownTileArea3_One(16, 54, true);
            UpdateUnknownTileArea3_One(17, 54, true);
            UpdateUnknownTileArea3_One(18, 54, true);
            UpdateUnknownTileArea3_One(19, 54, true);
            UpdateUnknownTileArea3_One(20, 54, true);
            UpdateUnknownTileArea3_One(21, 54, true);
            UpdateUnknownTileArea3_One(22, 54, true);
            UpdateUnknownTileArea3_One(23, 54, true);
            UpdateUnknownTileArea3_One(24, 54, true);
            UpdateUnknownTileArea3_One(25, 54, true);
            UpdateUnknownTileArea3_One(25, 53, true);
            UpdateUnknownTile_Rectangle(22, 48, 25, 52, true);
        }
        private void UpdateUnknownTileArea3_Area36()
        {
            UpdateUnknownTileArea3_One(26, 37, true);
            UpdateUnknownTileArea3_One(27, 37, true);
            UpdateUnknownTileArea3_One(28, 37, true);
            UpdateUnknownTileArea3_One(29, 37, true);
            UpdateUnknownTileArea3_One(30, 37, true);
            UpdateUnknownTileArea3_One(30, 36, true);
            UpdateUnknownTileArea3_One(30, 35, true);
            UpdateUnknownTileArea3_One(29, 35, true);
            UpdateUnknownTileArea3_One(28, 35, true);
            UpdateUnknownTileArea3_One(27, 35, true);
            UpdateUnknownTileArea3_One(26, 35, true);
            UpdateUnknownTileArea3_One(25, 35, true);
            UpdateUnknownTileArea3_One(25, 34, true);
        }
        private void UpdateUnknownTileArea3_Area37()
        {
            UpdateUnknownTileArea3_One(30, 21, true);
            UpdateUnknownTileArea3_One(30, 22, true);
            UpdateUnknownTileArea3_One(30, 23, true);
            UpdateUnknownTileArea3_One(30, 24, true);
            UpdateUnknownTileArea3_One(30, 25, true);
            UpdateUnknownTileArea3_One(31, 25, true);
            UpdateUnknownTileArea3_One(32, 25, true);
            UpdateUnknownTileArea3_One(32, 24, true);
            UpdateUnknownTileArea3_One(32, 23, true);
            UpdateUnknownTileArea3_One(32, 22, true);
            UpdateUnknownTileArea3_One(32, 21, true);
        }
        private void UpdateUnknownTileArea3_Area38()
        {
            UpdateUnknownTileArea3_One(0, 40, true);
            UpdateUnknownTileArea3_One(1, 40, true);
            UpdateUnknownTileArea3_One(2, 40, true);
            UpdateUnknownTileArea3_One(3, 40, true);
            UpdateUnknownTileArea3_One(3, 39, true);
            UpdateUnknownTileArea3_One(3, 38, true);
            UpdateUnknownTileArea3_One(3, 37, true);
            UpdateUnknownTileArea3_One(3, 36, true);
            UpdateUnknownTileArea3_One(3, 35, true);
            UpdateUnknownTileArea3_One(3, 34, true);
            UpdateUnknownTileArea3_One(1, 41, true);
            UpdateUnknownTileArea3_One(1, 42, true);
            UpdateUnknownTileArea3_One(1, 43, true);
            UpdateUnknownTileArea3_One(1, 44, true);
            UpdateUnknownTileArea3_One(1, 45, true);
            UpdateUnknownTileArea3_One(1, 46, true);
            UpdateUnknownTileArea3_One(1, 47, true);
            UpdateUnknownTileArea3_One(1, 48, true);
            UpdateUnknownTileArea3_One(1, 49, true);
            UpdateUnknownTileArea3_One(1, 50, true);
            UpdateUnknownTileArea3_One(1, 51, true);
            UpdateUnknownTileArea3_One(1, 52, true);
            UpdateUnknownTileArea3_One(1, 53, true);
            UpdateUnknownTileArea3_One(1, 39, true);
            UpdateUnknownTileArea3_One(1, 38, true);
            UpdateUnknownTileArea3_One(1, 37, true);
            UpdateUnknownTileArea3_One(1, 36, true);
            UpdateUnknownTileArea3_One(1, 35, true);
            UpdateUnknownTileArea3_One(1, 34, true);
            UpdateUnknownTileArea3_One(1, 33, true);
            UpdateUnknownTileArea3_One(1, 32, true);
            UpdateUnknownTileArea3_One(1, 31, true);
            UpdateUnknownTileArea3_One(1, 30, true);
            UpdateUnknownTileArea3_One(1, 29, true);
            UpdateUnknownTileArea3_One(1, 28, true);
            UpdateUnknownTileArea3_One(1, 27, true);
        }
        private void UpdateUnknownTileArea3_Area39()
        {
            UpdateUnknownTileArea3_One(16, 32, true);
            UpdateUnknownTileArea3_One(15, 32, true);
            UpdateUnknownTileArea3_One(14, 32, true);
            UpdateUnknownTileArea3_One(14, 33, true);
            UpdateUnknownTileArea3_One(14, 34, true);
            UpdateUnknownTileArea3_One(14, 35, true);
            UpdateUnknownTileArea3_One(14, 36, true);
            UpdateUnknownTileArea3_One(15, 36, true);
            UpdateUnknownTileArea3_One(16, 36, true);
            UpdateUnknownTileArea3_One(17, 36, true);
            UpdateUnknownTileArea3_One(18, 36, true);
            UpdateUnknownTileArea3_One(18, 35, true);
            UpdateUnknownTileArea3_One(18, 34, true);
            UpdateUnknownTileArea3_One(18, 33, true);
            UpdateUnknownTileArea3_One(18, 32, true);
            UpdateUnknownTileArea3_One(18, 31, true);
            UpdateUnknownTileArea3_One(18, 30, true);
            UpdateUnknownTileArea3_One(18, 29, true);
            UpdateUnknownTileArea3_One(18, 28, true);
            UpdateUnknownTileArea3_One(17, 28, true);
            UpdateUnknownTileArea3_One(16, 28, true);
            UpdateUnknownTileArea3_One(15, 28, true);
            UpdateUnknownTileArea3_One(14, 28, true);
            UpdateUnknownTileArea3_One(14, 29, true);
            UpdateUnknownTileArea3_One(14, 30, true);
            UpdateUnknownTileArea3_One(15, 30, true);
            UpdateUnknownTileArea3_One(16, 30, true);
            UpdateUnknownTileArea3_One(17, 34, true);
            UpdateUnknownTileArea3_One(16, 34, true);
        }
        private void UpdateUnknownTileArea3_Area40()
        {
            UpdateUnknownTileArea3_One(20, 20, true);
            UpdateUnknownTileArea3_One(20, 21, true);
            UpdateUnknownTileArea3_One(20, 22, true);
            UpdateUnknownTileArea3_One(20, 23, true);
            UpdateUnknownTileArea3_One(20, 24, true);
            UpdateUnknownTile_Rectangle(17, 25, 21, 27, true);
            UpdateUnknownTile_Rectangle(19, 28, 21, 29, true);
        }
        private void UpdateUnknownTileArea3_Area41()
        {
            UpdateUnknownTileArea3_One(20, 45, true);
            UpdateUnknownTileArea3_One(20, 44, true);
            UpdateUnknownTileArea3_One(20, 43, true);
            UpdateUnknownTileArea3_One(20, 42, true);
            UpdateUnknownTileArea3_One(19, 42, true);
            UpdateUnknownTileArea3_One(18, 42, true);
            UpdateUnknownTileArea3_One(17, 42, true);
        }
        private void UpdateUnknownTileArea3_Area42()
        {
            UpdateUnknownTileArea3_One(20, 48, true);
            UpdateUnknownTileArea3_One(19, 48, true);
            UpdateUnknownTileArea3_One(18, 48, true);
        }
        private void UpdateUnknownTileArea3_Area43()
        {
            UpdateUnknownTileArea3_One(9, 21, true);
            UpdateUnknownTile_Rectangle(5, 21, 9, 22, true);
        }
        private void UpdateUnknownTileArea3_Area44()
        {
            UpdateUnknownTileArea3_One(22, 26, true);
            UpdateUnknownTileArea3_One(22, 27, true);
            UpdateUnknownTileArea3_One(22, 28, true);
            UpdateUnknownTileArea3_One(22, 29, true);
            UpdateUnknownTileArea3_One(22, 30, true);
            UpdateUnknownTileArea3_One(23, 30, true);
            UpdateUnknownTileArea3_One(23, 31, true);
            UpdateUnknownTileArea3_One(23, 32, true);
            UpdateUnknownTileArea3_One(23, 33, true);
            UpdateUnknownTileArea3_One(23, 34, true);
            UpdateUnknownTileArea3_One(24, 30, true);
            UpdateUnknownTileArea3_One(25, 30, true);
            UpdateUnknownTileArea3_One(25, 31, true);
            UpdateUnknownTileArea3_One(25, 32, true);
            UpdateUnknownTileArea3_One(25, 33, true);
            UpdateUnknownTileArea3_One(21, 30, true);
            UpdateUnknownTileArea3_One(21, 31, true);
            UpdateUnknownTileArea3_One(21, 32, true);
            UpdateUnknownTileArea3_One(21, 33, true);
            UpdateUnknownTileArea3_One(21, 34, true);
            UpdateUnknownTileArea3_One(21, 35, true);
            UpdateUnknownTileArea3_One(20, 30, true);
            UpdateUnknownTileArea3_One(19, 30, true);
            UpdateUnknownTileArea3_One(19, 31, true);
            UpdateUnknownTileArea3_One(19, 32, true);
            UpdateUnknownTileArea3_One(19, 33, true);
            UpdateUnknownTileArea3_One(19, 34, true);
            UpdateUnknownTileArea3_One(19, 35, true);
            UpdateUnknownTileArea3_One(19, 36, true);
        }
        private void UpdateUnknownTileArea3_Area45()
        {
            UpdateUnknownTileArea3_One(28, 40, true);
            UpdateUnknownTileArea3_One(28, 39, true);
            UpdateUnknownTileArea3_One(27, 39, true);
            UpdateUnknownTileArea3_One(26, 39, true);
            UpdateUnknownTileArea3_One(25, 39, true);
            UpdateUnknownTileArea3_One(25, 40, true);
            UpdateUnknownTileArea3_One(25, 41, true);
            UpdateUnknownTileArea3_One(25, 42, true);
            UpdateUnknownTile_Rectangle(24, 43, 26, 44, true);
        }
        private void UpdateUnknownTileArea3_Area46()
        {
            UpdateUnknownTileArea3_One(22, 53, true);
            UpdateUnknownTileArea3_One(23, 53, true);
            UpdateUnknownTileArea3_One(24, 53, true);
        }
        private void UpdateUnknownTileArea3_Area47()
        {
            UpdateUnknownTileArea3_One(10, 52, true);
            UpdateUnknownTileArea3_One(9, 52, true);
            UpdateUnknownTile_Rectangle(6, 50, 8, 53, true);
        }
        private void UpdateUnknownTileArea3_Area48()
        {
            UpdateUnknownTileArea3_One(34, 27, true);
            UpdateUnknownTileArea3_One(34, 26, true);
            UpdateUnknownTileArea3_One(34, 25, true);
            UpdateUnknownTileArea3_One(34, 24, true);
            UpdateUnknownTileArea3_One(34, 23, true);
            UpdateUnknownTileArea3_One(34, 22, true);
            UpdateUnknownTileArea3_One(34, 21, true);
            UpdateUnknownTileArea3_One(34, 20, true);
            UpdateUnknownTileArea3_One(33, 20, true);
            UpdateUnknownTileArea3_One(32, 20, true);
            UpdateUnknownTileArea3_One(31, 20, true);
            UpdateUnknownTileArea3_One(30, 20, true);
            UpdateUnknownTileArea3_One(29, 20, true);
            UpdateUnknownTileArea3_One(28, 20, true);
            UpdateUnknownTileArea3_One(27, 20, true);
            UpdateUnknownTileArea3_One(26, 20, true);
            UpdateUnknownTileArea3_One(25, 20, true);
            UpdateUnknownTileArea3_One(24, 20, true);
            UpdateUnknownTileArea3_One(23, 20, true);
            UpdateUnknownTileArea3_One(22, 20, true);
            UpdateUnknownTileArea3_One(21, 20, true);
            UpdateUnknownTileArea3_One(21, 21, true);
            UpdateUnknownTileArea3_One(21, 22, true);
            UpdateUnknownTileArea3_One(21, 23, true);
            UpdateUnknownTileArea3_One(21, 24, true);
            UpdateUnknownTileArea3_One(23, 21, true);
            UpdateUnknownTileArea3_One(23, 22, true);
            UpdateUnknownTileArea3_One(24, 22, true);
            UpdateUnknownTileArea3_One(25, 22, true);
            UpdateUnknownTileArea3_One(28, 21, true);
            UpdateUnknownTileArea3_One(28, 22, true);
            UpdateUnknownTileArea3_One(27, 22, true);
            UpdateUnknownTileArea3_One(26, 22, true);
            UpdateUnknownTileArea3_One(31, 21, true);
            UpdateUnknownTileArea3_One(31, 22, true);
            UpdateUnknownTileArea3_One(31, 23, true);
            UpdateUnknownTileArea3_One(31, 24, true);
        }
        private void UpdateUnknownTileArea3_Area49()
        {
            UpdateUnknownTileArea3_One(36, 43, true);
            UpdateUnknownTileArea3_One(36, 44, true);
            UpdateUnknownTileArea3_One(36, 45, true);
            UpdateUnknownTileArea3_One(36, 46, true);
            UpdateUnknownTileArea3_One(36, 47, true);
            UpdateUnknownTileArea3_One(35, 47, true);
            UpdateUnknownTileArea3_One(34, 47, true);
            UpdateUnknownTileArea3_One(33, 47, true);
            UpdateUnknownTileArea3_One(32, 47, true);
            UpdateUnknownTileArea3_One(31, 47, true);
        }
        private void UpdateUnknownTileArea3_Area50()
        {
            UpdateUnknownTileArea3_One(2, 31, true);
            UpdateUnknownTileArea3_One(2, 32, true);
            UpdateUnknownTileArea3_One(2, 33, true);
            UpdateUnknownTileArea3_One(2, 34, true);
            UpdateUnknownTileArea3_One(2, 35, true);
            UpdateUnknownTileArea3_One(2, 36, true);
            UpdateUnknownTileArea3_One(2, 37, true);
            UpdateUnknownTileArea3_One(2, 38, true);
            UpdateUnknownTileArea3_One(2, 39, true);
        }
        private void UpdateUnknownTileArea3_Area51()
        {
            UpdateUnknownTileArea3_One(32, 54, true);
            UpdateUnknownTileArea3_One(33, 54, true);
            UpdateUnknownTileArea3_One(34, 54, true);
            UpdateUnknownTileArea3_One(35, 54, true);
            UpdateUnknownTileArea3_One(36, 54, true);
            UpdateUnknownTileArea3_One(37, 54, true);
            UpdateUnknownTileArea3_One(38, 54, true);
        }
        private void UpdateUnknownTileArea3_Area52()
        {
            UpdateUnknownTileArea3_One(21, 42, true);
            UpdateUnknownTileArea3_One(21, 43, true);
            UpdateUnknownTileArea3_One(21, 44, true);
            UpdateUnknownTileArea3_One(21, 45, true);
            UpdateUnknownTileArea3_One(21, 46, true);
            UpdateUnknownTileArea3_One(14, 47, true);
            UpdateUnknownTileArea3_One(15, 47, true);
            UpdateUnknownTileArea3_One(16, 47, true);
            UpdateUnknownTileArea3_One(17, 47, true);
            UpdateUnknownTileArea3_One(18, 47, true);
            UpdateUnknownTileArea3_One(19, 47, true);
            UpdateUnknownTileArea3_One(20, 47, true);
            UpdateUnknownTileArea3_One(21, 47, true);
            UpdateUnknownTileArea3_One(22, 47, true);
            UpdateUnknownTileArea3_One(23, 47, true);
            UpdateUnknownTileArea3_One(24, 47, true);
            UpdateUnknownTileArea3_One(25, 47, true);
            UpdateUnknownTileArea3_One(26, 47, true);
            UpdateUnknownTileArea3_One(27, 47, true);
            UpdateUnknownTileArea3_One(28, 47, true);
            UpdateUnknownTileArea3_One(21, 48, true);
            UpdateUnknownTileArea3_One(21, 49, true);
            UpdateUnknownTileArea3_One(21, 50, true);
            UpdateUnknownTileArea3_One(21, 51, true);
            UpdateUnknownTileArea3_One(21, 52, true);
            UpdateUnknownTileArea3_One(21, 53, true);
        }
        private void UpdateUnknownTileArea3_Area53()
        {
            UpdateUnknownTileArea3_One(17, 53, true);
            UpdateUnknownTileArea3_One(17, 52, true);
            UpdateUnknownTileArea3_One(16, 52, true);
            UpdateUnknownTileArea3_One(15, 52, true);
            UpdateUnknownTileArea3_One(14, 52, true);
            UpdateUnknownTileArea3_One(14, 51, true);
            UpdateUnknownTileArea3_One(14, 50, true);
            UpdateUnknownTileArea3_One(15, 50, true);
            UpdateUnknownTileArea3_One(16, 50, true);
            UpdateUnknownTileArea3_One(17, 50, true);
            UpdateUnknownTileArea3_One(17, 49, true);
            UpdateUnknownTileArea3_One(17, 48, true);
            UpdateUnknownTileArea3_One(16, 48, true);
            UpdateUnknownTileArea3_One(15, 48, true);
            UpdateUnknownTileArea3_One(14, 48, true);
            UpdateUnknownTileArea3_One(13, 48, true);
            UpdateUnknownTileArea3_One(13, 47, true);
            UpdateUnknownTileArea3_One(13, 46, true);
            UpdateUnknownTileArea3_One(14, 46, true);
            UpdateUnknownTileArea3_One(15, 46, true);
            UpdateUnknownTileArea3_One(16, 46, true);
            UpdateUnknownTileArea3_One(17, 46, true);
            UpdateUnknownTileArea3_One(18, 46, true);
            UpdateUnknownTileArea3_One(19, 46, true);
            UpdateUnknownTileArea3_One(20, 46, true);
        }
        private void UpdateUnknownTileArea3_Area54()
        {
            UpdateUnknownTileArea3_One(12, 21, true);
            UpdateUnknownTileArea3_One(12, 22, true);
            UpdateUnknownTileArea3_One(11, 22, true);
            UpdateUnknownTileArea3_One(10, 22, true);
            UpdateUnknownTileArea3_One(10, 21, true);
            UpdateUnknownTileArea3_One(10, 20, true);
            UpdateUnknownTileArea3_One(9, 20, true);
            UpdateUnknownTileArea3_One(8, 20, true);
            UpdateUnknownTileArea3_One(7, 20, true);
            UpdateUnknownTileArea3_One(6, 20, true);
            UpdateUnknownTileArea3_One(5, 20, true);
            UpdateUnknownTileArea3_One(4, 20, true);
            UpdateUnknownTileArea3_One(3, 20, true);
            UpdateUnknownTileArea3_One(3, 21, true);
        }
        private void UpdateUnknownTileArea3_Area55()
        {
            UpdateUnknownTileArea3_One(37, 38, true);
            UpdateUnknownTileArea3_One(37, 39, true);
            UpdateUnknownTileArea3_One(38, 39, true);
            UpdateUnknownTileArea3_One(38, 40, true);
        }
        private void UpdateUnknownTileArea3_Area56()
        {
            UpdateUnknownTileArea3_One(2, 25, true);
            UpdateUnknownTileArea3_One(2, 24, true);
            UpdateUnknownTileArea3_One(2, 23, true);
            UpdateUnknownTileArea3_One(3, 23, true);
            UpdateUnknownTileArea3_One(4, 23, true);
            UpdateUnknownTileArea3_One(5, 23, true);
            UpdateUnknownTileArea3_One(6, 23, true);
            UpdateUnknownTileArea3_One(7, 23, true);
            UpdateUnknownTileArea3_One(8, 23, true);
            UpdateUnknownTileArea3_One(9, 23, true);
            UpdateUnknownTileArea3_One(10, 23, true);
            UpdateUnknownTileArea3_One(11, 23, true);
            UpdateUnknownTileArea3_One(12, 23, true);
            UpdateUnknownTileArea3_One(12, 24, true);
            UpdateUnknownTile_Rectangle(7, 25, 12, 29, true);
            UpdateUnknownTileArea3_One(8, 30, true);
            UpdateUnknownTileArea3_One(9, 30, true);
            UpdateUnknownTileArea3_One(10, 30, true);
            UpdateUnknownTileArea3_One(11, 30, true);
            UpdateUnknownTileArea3_One(12, 30, true);
        }
        private void UpdateUnknownTileArea3_Area57()
        {
            UpdateUnknownTileArea3_One(26, 24, true);
            UpdateUnknownTileArea3_One(27, 24, true);
            UpdateUnknownTileArea3_One(28, 24, true);
            UpdateUnknownTileArea3_One(29, 24, true);
            UpdateUnknownTileArea3_One(29, 23, true);
            UpdateUnknownTileArea3_One(29, 22, true);
            UpdateUnknownTileArea3_One(29, 21, true);
        }
        private void UpdateUnknownTileArea3_Area58()
        {
            UpdateUnknownTileArea3_One(17, 43, true);
            UpdateUnknownTileArea3_One(18, 43, true);
            UpdateUnknownTileArea3_One(19, 43, true);
            UpdateUnknownTileArea3_One(19, 44, true);
            UpdateUnknownTileArea3_One(19, 45, true);
            UpdateUnknownTileArea3_One(18, 45, true);
            UpdateUnknownTileArea3_One(17, 45, true);
            UpdateUnknownTileArea3_One(16, 45, true);
            UpdateUnknownTileArea3_One(15, 45, true);
            UpdateUnknownTileArea3_One(15, 44, true);
            UpdateUnknownTileArea3_One(14, 44, true);
            UpdateUnknownTileArea3_One(13, 44, true);
        }
        private void UpdateUnknownTileArea3_Area59()
        {
            UpdateUnknownTileArea3_One(4, 21, true);
            UpdateUnknownTileArea3_One(4, 22, true);
            UpdateUnknownTileArea3_One(3, 22, true);
            UpdateUnknownTileArea3_One(2, 22, true);
            UpdateUnknownTileArea3_One(2, 21, true);
            UpdateUnknownTileArea3_One(2, 20, true);
            UpdateUnknownTileArea3_One(1, 20, true);
            UpdateUnknownTileArea3_One(0, 20, true);
            UpdateUnknownTileArea3_One(0, 21, true);
            UpdateUnknownTileArea3_One(0, 22, true);
            UpdateUnknownTileArea3_One(0, 23, true);
            UpdateUnknownTileArea3_One(0, 24, true);
            UpdateUnknownTileArea3_One(0, 25, true);
            UpdateUnknownTileArea3_One(0, 26, true);
            UpdateUnknownTileArea3_One(0, 27, true);
            UpdateUnknownTileArea3_One(0, 28, true);
            UpdateUnknownTileArea3_One(0, 29, true);
            UpdateUnknownTileArea3_One(0, 30, true);
            UpdateUnknownTileArea3_One(0, 31, true);
            UpdateUnknownTileArea3_One(0, 32, true);
            UpdateUnknownTileArea3_One(0, 33, true);
            UpdateUnknownTileArea3_One(0, 34, true);
            UpdateUnknownTileArea3_One(0, 35, true);
            UpdateUnknownTileArea3_One(0, 36, true);
            UpdateUnknownTileArea3_One(0, 37, true);
            UpdateUnknownTileArea3_One(0, 38, true);
            UpdateUnknownTileArea3_One(0, 39, true);
        }
        private void UpdateUnknownTileArea3_Area60()
        {
            UpdateUnknownTileArea3_One(39, 35, true);
            UpdateUnknownTileArea3_One(39, 34, true);
            UpdateUnknownTileArea3_One(39, 33, true);
            UpdateUnknownTileArea3_One(39, 32, true);
            UpdateUnknownTileArea3_One(39, 31, true);
            UpdateUnknownTileArea3_One(39, 30, true);
            UpdateUnknownTileArea3_One(39, 29, true);
            UpdateUnknownTileArea3_One(39, 28, true);
            UpdateUnknownTileArea3_One(39, 27, true);
            UpdateUnknownTileArea3_One(39, 26, true);
            UpdateUnknownTileArea3_One(39, 25, true);
            UpdateUnknownTileArea3_One(39, 24, true);
            UpdateUnknownTileArea3_One(39, 23, true);
            UpdateUnknownTileArea3_One(39, 22, true);
            UpdateUnknownTileArea3_One(39, 21, true);
            UpdateUnknownTileArea3_One(39, 20, true);
            UpdateUnknownTileArea3_One(38, 20, true);
            UpdateUnknownTile_Rectangle(35, 20, 37, 27, true);
        }
        private void UpdateUnknownTileArea3_Area61()
        {
            UpdateUnknownTileArea3_One(38, 21, true);
            UpdateUnknownTileArea3_One(38, 22, true);
            UpdateUnknownTileArea3_One(38, 23, true);
            UpdateUnknownTileArea3_One(38, 24, true);
            UpdateUnknownTileArea3_One(38, 25, true);
            UpdateUnknownTileArea3_One(38, 26, true);
            UpdateUnknownTileArea3_One(38, 27, true);
            UpdateUnknownTileArea3_One(38, 28, true);
            UpdateUnknownTileArea3_One(38, 29, true);
            UpdateUnknownTileArea3_One(38, 30, true);
            UpdateUnknownTileArea3_One(38, 31, true);
            UpdateUnknownTileArea3_One(38, 32, true);
            UpdateUnknownTileArea3_One(38, 33, true);
            UpdateUnknownTileArea3_One(38, 34, true);
            UpdateUnknownTileArea3_One(38, 35, true);
            UpdateUnknownTileArea3_One(38, 36, true);
            UpdateUnknownTileArea3_One(39, 36, true);
            UpdateUnknownTileArea3_One(39, 37, true);
            UpdateUnknownTileArea3_One(39, 38, true);
            UpdateUnknownTileArea3_One(39, 39, true);
            UpdateUnknownTileArea3_One(39, 40, true);
            UpdateUnknownTileArea3_One(39, 41, true);
            UpdateUnknownTileArea3_One(39, 42, true);
        }
        private void UpdateUnknownTileArea3_Area62()
        {
            UpdateUnknownTileArea3_One(33, 49, true);
            UpdateUnknownTileArea3_One(33, 50, true);
            UpdateUnknownTileArea3_One(33, 51, true);
        }
        private void UpdateUnknownTileArea3_Area63()
        {
            UpdateUnknownTileArea3_One(16, 49, true);
            UpdateUnknownTileArea3_One(15, 49, true);
            UpdateUnknownTileArea3_One(14, 49, true);
            UpdateUnknownTileArea3_One(13, 49, true);
            UpdateUnknownTileArea3_One(13, 50, true);
            UpdateUnknownTileArea3_One(13, 51, true);
            UpdateUnknownTileArea3_One(13, 52, true);
        }
        private void UpdateUnknownTileArea3_Area64()
        {
            UpdateUnknownTileArea3_One(18, 44, true);
            UpdateUnknownTileArea3_One(17, 44, true);
            UpdateUnknownTileArea3_One(16, 44, true);
            UpdateUnknownTileArea3_One(16, 43, true);
            UpdateUnknownTileArea3_One(16, 42, true);
            UpdateUnknownTileArea3_One(16, 41, true);
            UpdateUnknownTileArea3_One(17, 41, true);
            UpdateUnknownTileArea3_One(17, 40, true);
            UpdateUnknownTileArea3_One(18, 40, true);
            UpdateUnknownTileArea3_One(19, 40, true);
            UpdateUnknownTileArea3_One(20, 40, true);
            UpdateUnknownTileArea3_One(21, 40, true);
            UpdateUnknownTileArea3_One(22, 40, true);
            UpdateUnknownTileArea3_One(23, 40, true);
            UpdateUnknownTileArea3_One(23, 41, true);
            UpdateUnknownTileArea3_One(23, 42, true);
            UpdateUnknownTileArea3_One(23, 43, true);
            UpdateUnknownTileArea3_One(23, 44, true);
            UpdateUnknownTileArea3_One(23, 45, true);
            UpdateUnknownTileArea3_One(24, 45, true);
            UpdateUnknownTileArea3_One(25, 45, true);
            UpdateUnknownTileArea3_One(26, 45, true);
            UpdateUnknownTileArea3_One(27, 45, true);
            UpdateUnknownTileArea3_One(27, 44, true);
            UpdateUnknownTileArea3_One(27, 43, true);
            UpdateUnknownTileArea3_One(27, 42, true);
            UpdateUnknownTileArea3_One(28, 42, true);
            UpdateUnknownTileArea3_One(29, 42, true);
            UpdateUnknownTileArea3_One(30, 42, true);
            UpdateUnknownTileArea3_One(30, 43, true);
            UpdateUnknownTileArea3_One(30, 44, true);
            UpdateUnknownTileArea3_One(30, 45, true);
            UpdateUnknownTileArea3_One(30, 46, true);
            UpdateUnknownTileArea3_One(30, 47, true);
            UpdateUnknownTileArea3_One(30, 48, true);
            UpdateUnknownTileArea3_One(30, 49, true);
            UpdateUnknownTileArea3_One(31, 49, true);
            UpdateUnknownTileArea3_One(32, 49, true);
            UpdateUnknownTileArea3_One(32, 50, true);
            UpdateUnknownTileArea3_One(32, 51, true);
            UpdateUnknownTileArea3_One(32, 52, true);
            UpdateUnknownTileArea3_One(33, 52, true);
            UpdateUnknownTileArea3_One(34, 52, true);
            UpdateUnknownTileArea3_One(35, 52, true);
            UpdateUnknownTileArea3_One(36, 52, true);
            UpdateUnknownTileArea3_One(37, 52, true);
            UpdateUnknownTileArea3_One(37, 53, true);
            UpdateUnknownTileArea3_One(29, 49, true);
            UpdateUnknownTileArea3_One(28, 49, true);
            UpdateUnknownTileArea3_One(27, 49, true);
            UpdateUnknownTileArea3_One(27, 50, true);
            UpdateUnknownTileArea3_One(27, 51, true);
            UpdateUnknownTileArea3_One(27, 52, true);
            UpdateUnknownTileArea3_One(27, 53, true);
            UpdateUnknownTileArea3_One(31, 46, true);
            UpdateUnknownTileArea3_One(32, 46, true);
            UpdateUnknownTileArea3_One(33, 46, true);
            UpdateUnknownTileArea3_One(34, 46, true);
            UpdateUnknownTileArea3_One(35, 46, true);
            UpdateUnknownTileArea3_One(35, 45, true);
            UpdateUnknownTileArea3_One(35, 44, true);
            UpdateUnknownTileArea3_One(35, 43, true);
            UpdateUnknownTileArea3_One(35, 42, true);
            UpdateUnknownTileArea3_One(26, 42, true);
            UpdateUnknownTileArea3_One(26, 41, true);
            UpdateUnknownTileArea3_One(26, 40, true);
            UpdateUnknownTileArea3_One(15, 43, true);
            UpdateUnknownTileArea3_One(14, 43, true);
            UpdateUnknownTileArea3_One(13, 43, true);
            UpdateUnknownTileArea3_One(12, 43, true);
            UpdateUnknownTileArea3_One(12, 44, true);
            UpdateUnknownTileArea3_One(11, 44, true);
            UpdateUnknownTileArea3_One(10, 44, true);
            UpdateUnknownTileArea3_One(10, 45, true);
            UpdateUnknownTileArea3_One(10, 46, true);
            UpdateUnknownTileArea3_One(10, 47, true);
            UpdateUnknownTileArea3_One(10, 48, true);
            UpdateUnknownTileArea3_One(9, 48, true);
            UpdateUnknownTileArea3_One(8, 48, true);
            UpdateUnknownTileArea3_One(7, 48, true);
            UpdateUnknownTileArea3_One(6, 48, true);
            UpdateUnknownTileArea3_One(5, 48, true);
            UpdateUnknownTileArea3_One(4, 48, true);
            UpdateUnknownTileArea3_One(3, 48, true);
            UpdateUnknownTileArea3_One(3, 49, true);
            UpdateUnknownTileArea3_One(3, 50, true);
            UpdateUnknownTileArea3_One(3, 51, true);
            UpdateUnknownTileArea3_One(3, 52, true);
            UpdateUnknownTileArea3_One(3, 53, true);
            UpdateUnknownTileArea3_One(4, 53, true);
            UpdateUnknownTileArea3_One(5, 53, true);
            UpdateUnknownTileArea3_One(5, 52, true);
            UpdateUnknownTileArea3_One(5, 51, true);
            UpdateUnknownTileArea3_One(5, 50, true);
            UpdateUnknownTileArea3_One(3, 47, true);
            UpdateUnknownTileArea3_One(3, 46, true);
            UpdateUnknownTileArea3_One(3, 45, true);
            UpdateUnknownTileArea3_One(3, 44, true);
            UpdateUnknownTileArea3_One(4, 44, true);
            UpdateUnknownTileArea3_One(10, 49, true);
            UpdateUnknownTileArea3_One(10, 50, true);
            UpdateUnknownTileArea3_One(10, 51, true);
            UpdateUnknownTileArea3_One(9, 51, true);
            UpdateUnknownTileArea3_One(9, 44, true);
            UpdateUnknownTileArea3_One(8, 44, true);
            UpdateUnknownTileArea3_One(7, 44, true);
            UpdateUnknownTileArea3_One(6, 44, true);
            UpdateUnknownTileArea3_One(6, 45, true);
            UpdateUnknownTileArea3_One(6, 46, true);
        }
        private void UpdateUnknownTileArea3_Area65()
        {
            UpdateUnknownTileArea3_One(24, 21, true);
            UpdateUnknownTileArea3_One(25, 21, true);
            UpdateUnknownTileArea3_One(26, 21, true);
            UpdateUnknownTileArea3_One(27, 21, true);
        }
        private void UpdateUnknownTileArea3_Area66()
        {
            UpdateUnknownTileArea3_One(27, 54, true);
            UpdateUnknownTileArea3_One(26, 54, true);
            UpdateUnknownTileArea3_One(26, 53, true);
            UpdateUnknownTileArea3_One(26, 52, true);
            UpdateUnknownTileArea3_One(26, 51, true);
            UpdateUnknownTileArea3_One(26, 50, true);
            UpdateUnknownTileArea3_One(26, 49, true);
            UpdateUnknownTileArea3_One(26, 48, true);
            UpdateUnknownTileArea3_One(27, 48, true);
            UpdateUnknownTileArea3_One(28, 48, true);
            UpdateUnknownTileArea3_One(29, 48, true);
            UpdateUnknownTileArea3_One(29, 47, true);
            UpdateUnknownTileArea3_One(29, 46, true);
            UpdateUnknownTileArea3_One(29, 45, true);
            UpdateUnknownTileArea3_One(29, 44, true);
            UpdateUnknownTileArea3_One(29, 43, true);
        }
        private void UpdateUnknownTileArea3_Area67()
        {
            UpdateUnknownTileArea3_One(31, 48, true);
            UpdateUnknownTileArea3_One(32, 48, true);
            UpdateUnknownTileArea3_One(33, 48, true);
            UpdateUnknownTileArea3_One(34, 48, true);
            UpdateUnknownTileArea3_One(35, 48, true);
            UpdateUnknownTileArea3_One(36, 48, true);
            UpdateUnknownTileArea3_One(37, 48, true);
            UpdateUnknownTileArea3_One(37, 47, true);
            UpdateUnknownTileArea3_One(37, 46, true);
            UpdateUnknownTileArea3_One(37, 45, true);
        }
        //private void UpdateUnknownTileArea3_Area68() { } X5ルートは一歩ずつ進ませる事とする。

        private void UpdateUnknownTileArea3_Area69()
        {
            UpdateUnknownTileArea3_One(5, 36, true);
            UpdateUnknownTileArea3_One(6, 36, true);
            UpdateUnknownTileArea3_One(5, 38, true);
            UpdateUnknownTileArea3_One(6, 38, true);
        }

        private void JumpByMirror_1_End() { JumpToLocation(18, -19, true); UpdateUnknownTile(); }
        private void JumpByMirror_2_38() { JumpToLocation(40, -29, true); UpdateUnknownTileArea3_Area2(); }
        private void JumpByMirror_2_39() { JumpToLocation(45, -4, true); UpdateUnknownTileArea3_Area22(); }
        private void JumpByMirror_2_40() { JumpToLocation(21, -30, true); UpdateUnknownTileArea3_Area37(); }
        private void JumpByMirror_2_41() { JumpToLocation(33, -7, true); UpdateUnknownTileArea3_Area3(); }
        private void JumpByMirror_2_42() { JumpToLocation(43, -28, true); UpdateUnknownTileArea3_Area11(); }
        private void JumpByMirror_2_43() { JumpToLocation(21, -22, true); UpdateUnknownTileArea3_Area15(); }
        private void JumpByMirror_2_44() { JumpToLocation(45, -11, true); UpdateUnknownTileArea3_Area23(); }
        private void JumpByMirror_2_45() { JumpToLocation(36, -36, true); UpdateUnknownTileArea3_Area29(); }
        private void JumpByMirror_2_46() { JumpToLocation(40, 0, true); UpdateUnknownTileArea3_Area38(); }
        private void JumpByMirror_2_47() { JumpToLocation(29, -23, true); UpdateUnknownTileArea3_Area4(); }
        private void JumpByMirror_2_48() { JumpToLocation(42, -24, true); UpdateUnknownTileArea3_Area8(); }
        private void JumpByMirror_2_49() { JumpToLocation(38, -38, true); UpdateUnknownTileArea3_Area12(); }
        private void JumpByMirror_2_50() { JumpToLocation(40, -16, true); UpdateUnknownTileArea3_Area16(); }
        private void JumpByMirror_2_51() { JumpToLocation(53, -36, true); UpdateUnknownTileArea3_Area18(); }
        private void JumpByMirror_2_52() { JumpToLocation(30, -13, true); UpdateUnknownTileArea3_Area24(); }
        private void JumpByMirror_2_53() { JumpToLocation(41, -30, true); UpdateUnknownTileArea3_Area27(); }
        private void JumpByMirror_2_54() { JumpToLocation(32, -11, true); UpdateUnknownTileArea3_Area30(); }
        private void JumpByMirror_2_55() { JumpToLocation(40, -4, true); UpdateUnknownTileArea3_Area34(); }
        private void JumpByMirror_2_56() { JumpToLocation(32, -16, true); UpdateUnknownTileArea3_Area39(); }
        private void JumpByMirror_2_57() { JumpToLocation(48, -20, true); UpdateUnknownTileArea3_Area42(); }
        private void JumpByMirror_2_58() { JumpToLocation(26, -22, true); UpdateUnknownTileArea3_Area44(); }
        private void JumpByMirror_2_59() { JumpToLocation(31, -20, true); UpdateUnknownTileArea3_Area5(); }
        private void JumpByMirror_2_60() { JumpToLocation(52, -4, true); UpdateUnknownTileArea3_Area6(); }
        private void JumpByMirror_2_61() { JumpToLocation(52, -12, true); UpdateUnknownTileArea3_Area7(); }
        private void JumpByMirror_2_62() { JumpToLocation(29, -36, true); UpdateUnknownTileArea3_Area9(); }
        private void JumpByMirror_2_63() { JumpToLocation(35, -17, true); UpdateUnknownTileArea3_Area10(); }
        private void JumpByMirror_2_64() { JumpToLocation(42, -3, true); UpdateUnknownTileArea3_Area13(); }
        private void JumpByMirror_2_65() { JumpToLocation(53, -38, true); UpdateUnknownTileArea3_Area14(); }
        private void JumpByMirror_2_66() { JumpToLocation(30, -33, true); UpdateUnknownTileArea3_Area17(); }
        private void JumpByMirror_2_67() { JumpToLocation(35, -4, true); UpdateUnknownTileArea3_Area19(); }
        private void JumpByMirror_2_68() { JumpToLocation(23, -17, true); UpdateUnknownTileArea3_Area20(); }
        private void JumpByMirror_2_69() { JumpToLocation(25, -29, true); UpdateUnknownTileArea3_Area21(); }
        private void JumpByMirror_2_70() { JumpToLocation(26, -29, true); UpdateUnknownTileArea3_Area25(); }
        private void JumpByMirror_2_71() { JumpToLocation(30, -4, true); UpdateUnknownTileArea3_Area26(); }
        private void JumpByMirror_2_72() { JumpToLocation(51, -15, true); UpdateUnknownTileArea3_Area28(); }
        private void JumpByMirror_2_73() { JumpToLocation(31, -22, true); UpdateUnknownTileArea3_Area31(); }
        private void JumpByMirror_2_74() { JumpToLocation(39, -33, true); UpdateUnknownTileArea3_Area32(); }
        private void JumpByMirror_2_75() { JumpToLocation(41, 0, true); UpdateUnknownTileArea3_Area33(); }
        private void JumpByMirror_2_76() { JumpToLocation(54, -11, true); UpdateUnknownTileArea3_Area35(); }
        private void JumpByMirror_2_77() { JumpToLocation(37, -26, true); UpdateUnknownTileArea3_Area36(); }
        private void JumpByMirror_2_78() { JumpToLocation(20, -20, true); UpdateUnknownTileArea3_Area40(); }
        private void JumpByMirror_2_79() { JumpToLocation(45, -20, true); UpdateUnknownTileArea3_Area41(); }
        private void JumpByMirror_2_80() { JumpToLocation(21, -9, true); UpdateUnknownTileArea3_Area43(); }
        private void JumpByMirror_2_81() { JumpToLocation(40, -28, true); UpdateUnknownTileArea3_Area45(); }
        private void JumpByMirror_2_82() { JumpToLocation(53, -22, true); UpdateUnknownTileArea3_Area46(); }
        private void JumpByMirror_2_83() { JumpToLocation(52, -10, true); UpdateUnknownTileArea3_Area47(); }
        private void JumpByMirror_2_84() { JumpToLocation(27, -34, true); UpdateUnknownTileArea3_Area48(); } 
        private void JumpByMirror_TruthWay1A() { JumpToLocation(43, -36, true); UpdateUnknownTileArea3_Area49(); }
        private void JumpByMirror_TruthWay1B() { JumpToLocation(31, -2, true); UpdateUnknownTileArea3_Area50(); }
        private void JumpByMirror_TruthWay1C() { JumpToLocation(54, -32, true); UpdateUnknownTileArea3_Area51(); }
        private void JumpByMirror_TruthWay1D() { JumpToLocation(47, -22, true); UpdateUnknownTileArea3_Area52(); }
        private void JumpByMirror_TruthWay2A() { JumpToLocation(53, -17, true); UpdateUnknownTileArea3_Area53(); }
        private void JumpByMirror_TruthWay2B() { JumpToLocation(21, -12, true); UpdateUnknownTileArea3_Area54(); }
        private void JumpByMirror_TruthWay2C() { JumpToLocation(38, -37, true); UpdateUnknownTileArea3_Area55(); }
        private void JumpByMirror_TruthWay2D() { JumpToLocation(25, -2, true); UpdateUnknownTileArea3_Area56(); }
        private void JumpByMirror_TruthWay3A() { JumpToLocation(24, -26, true); UpdateUnknownTileArea3_Area57(); }
        private void JumpByMirror_TruthWay3B() { JumpToLocation(43, -17, true); UpdateUnknownTileArea3_Area58(); }
        private void JumpByMirror_TruthWay3C() { JumpToLocation(21, -4, true); UpdateUnknownTileArea3_Area59(); }
        private void JumpByMirror_TruthWay3D() { JumpToLocation(35, -39, true); UpdateUnknownTileArea3_Area60(); }
        private void JumpByMirror_TruthWay4A() { JumpToLocation(21, -38, true); UpdateUnknownTileArea3_Area61(); }
        private void JumpByMirror_TruthWay4B() { JumpToLocation(49, -33, true); UpdateUnknownTileArea3_Area62(); }
        private void JumpByMirror_TruthWay4C() { JumpToLocation(49, -16, true); UpdateUnknownTileArea3_Area63(); }
        private void JumpByMirror_TruthWay4D() { JumpToLocation(44, -18, true); UpdateUnknownTileArea3_Area64(); }
        private void JumpByMirror_TruthWay5A() { JumpToLocation(21, -24, true); UpdateUnknownTileArea3_Area65(); }
        private void JumpByMirror_TruthWay5B() { JumpToLocation(54, -27, true); UpdateUnknownTileArea3_Area66(); }
        private void JumpByMirror_TruthWay5C() { JumpToLocation(48, -31, true); UpdateUnknownTileArea3_Area67(); }
        private void JumpByMirror_TruthWay5D() { JumpToLocation(1, -29, true); UpdateUnknownTile(); }
        private void JumpByMirror_TruthWay5E() { JumpToLocation(21, -1, true); UpdateUnknownTile(); }

        private void JumpByMirror_TurnBack() { JumpToLocation(20, -19, true); UpdateUnknownTile(); }

        private void JumpByMirror_ZeroWay() { JumpToLocation(37, -12, true); UpdateUnknownTile(); }
        private void JumpByMirror_Recollection3() { JumpToLocation(1, -10, true); UpdateUnknownTile(); }
        private void JumpByMirror_Recollection4() { JumpToLocation(1, -1, true); UpdateUnknownTile(); }

        private void JumpByMirror_InfinityWay1() { JumpToLocation(57, -34, true); UpdateUnknownTileArea3_Last(32); }
        private void JumpByMirror_InfinityWay2() { JumpToLocation(57, -31, true); UpdateUnknownTileArea3_Last(29); }
        private void JumpByMirror_InfinityWay3() { JumpToLocation(57, -28, true); UpdateUnknownTileArea3_Last(26); }
        private void JumpByMirror_InfinityWay4() { JumpToLocation(57, -25, true); UpdateUnknownTileArea3_Last(23); }
        private void JumpByMirror_InfinityWay5() { JumpToLocation(57, -22, true); UpdateUnknownTileArea3_Last(20); }
        private void JumpByMirror_InfinityWay6() { JumpToLocation(57, -19, true); UpdateUnknownTileArea3_Last(17); }
        private void JumpByMirror_InfinityWay7() { JumpToLocation(57, -16, true); UpdateUnknownTileArea3_Last(14); }
        private void JumpByMirror_InfinityWay8() { JumpToLocation(57, -13, true); UpdateUnknownTileArea3_Last(11); }
        private void JumpByMirror_InfinityWay9() { JumpToLocation(57, -10, true); UpdateUnknownTileArea3_Last(8); }
        private void JumpByMirror_InfinityWay10() { JumpToLocation(57, -7, true); UpdateUnknownTileArea3_Last(5); }
        private void JumpByMirror_InfinityWay11() { JumpToLocation(57, -4, true); UpdateUnknownTileArea3_Last(2); }
        private void JumpByMirror_InfinityWayTurnBack() { JumpToLocation(56, -39, true); UpdateUnknownTile(); }
        private void JumpByMirror_InfinityWayLast() { JumpToLocation(55, -1, true); UpdateUnknownTileArea3_TruthLast(); }

        private void JumpByMirror_Hell1() { JumpToLocation(40, -22, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell2() { JumpToLocation(26, -20, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell3() { JumpToLocation(39, -31, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell4() { JumpToLocation(22, -28, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell5() { JumpToLocation(39, -21, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell6() { JumpToLocation(23, -31, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell7() { JumpToLocation(39, -26, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell8() { JumpToLocation(23, -23, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell9() { JumpToLocation(34, -33, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell10() { JumpToLocation(38, -22, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell11() { JumpToLocation(44, -25, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell12() { JumpToLocation(34, -31, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell13() { JumpToLocation(28, -25, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell14() { JumpToLocation(38, -25, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell15() { JumpToLocation(33, -27, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell16() { JumpToLocation(49, -19, true); UpdateUnknownTile(); }
        private void JumpByMirror_Hell16Back() { JumpToLocation(33, -26, true); UpdateUnknownTile(); }
        private void JumpByMirror_HellFail() { JumpToLocation(27, -28, true); UpdateUnknownTile(); }

        private void UpdateUnknownTileArea3_One(int Y, int X)
        {
            UpdateUnknownTileArea3_One(Y, X, false);
        }
        private void UpdateUnknownTileArea3_One(int Y, int X, bool disableInvalidate)
        {
            unknownTile[Y * Database.TRUTH_DUNGEON_COLUMN + X].SetActive(false);
            GroundOne.Truth_KnownTileInfo3[Y * Database.TRUTH_DUNGEON_COLUMN + X] = true;
            if (disableInvalidate == false)
            {
                //dungeonField.Invalidate();
                //this.Update();
            }
        }
        private void UpdateUnknownTile_Rectangle(int top, int left, int bottom, int right)
        {
            UpdateUnknownTile_Rectangle(top, left, bottom, right, false);
        }
        private void UpdateUnknownTile_Rectangle(int top, int left, int bottom, int right, bool disableInvalidate)
        {
            for (int ii = left; ii <= right; ii++)
            {
                for (int jj = top; jj <= bottom; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
            if (disableInvalidate == false)
            {
                //dungeonField.Invalidate();
                //this.Update();
            }
        }

        private void UpdateUnknownTileArea3_Last0()
        {
            for (int ii = 55; ii <= 59; ii++)
            {
                for (int jj = 35; jj <= 39; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea3_Last(int jjStart)
        {
            for (int ii = 55; ii <= 59; ii++)
            {
                for (int jj = jjStart; jj <= jjStart + 2; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea3_TruthLast()
        {
            for (int ii = 55; ii <= 59; ii++)
            {
                for (int jj = 0; jj <= 1; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo3[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }

        private void UpdateUnknownTileArea41()
        {
            for (int ii = 44; ii <= 48; ii++)
            {
                for (int jj = 17; jj <= 21; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo4[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea42()
        {
            for (int ii = 47; ii <= 47; ii++)
            {
                for (int jj = 14; jj <= 16; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    GroundOne.Truth_KnownTileInfo4[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        private void UpdateUnknownTileArea43()
        {
            UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 23, 43, 19, 19);
        }
        private void UpdateUnknownTileArea(bool[] tile, int x1, int x2, int y1, int y2)
        {
            for (int ii = x1; ii <= x2; ii++)
            {
                for (int jj = y1; jj <= y2; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false);
                    tile[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
        }
        
        private void MakeCorrectAnswer(int num)
        {
            Debug.Log("MakeCorrectAnswer(S)");
            // 95, 96, 97, 105, 112, 113, 121, 124, 125, 133, 138, 144
            // [122-123] [142-143] [145-146]
            // [98-100] [106-108] [109-111] [114-116] [130-132] [139-141]
            // [101-104] [117-120] [126-129] [134-137] [147-150]

            // グループ１なら、４，４，２
            // グループ２なら、４，４，１
            // グループ３なら、４，４
            // グループ４なら、４，３
            // と思ったが、よく考えるとX1A->B->C->D->戻るで一つづつ開いていく方が楽しいのでその方向で作り込む。
            // いやいや、Ｘルートから最初の地点へ戻った時にオープンする方が楽しそう。

            int totalOpenNum = 0;
            if (GroundOne.WE2.TruthWay95 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay96 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay97 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay98 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay99 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay100 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay101 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay102 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay103 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay104 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay105 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay106 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay107 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay108 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay109 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay110 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay111 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay112 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay113 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay114 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay115 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay116 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay117 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay118 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay119 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay120 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay121 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay122 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay123 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay124 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay125 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay126 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay127 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay128 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay129 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay130 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay131 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay132 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay133 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay134 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay135 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay136 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay137 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay138 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay139 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay140 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay141 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay142 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay143 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay144 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay145 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay146 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay147 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay148 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay149 == -1) totalOpenNum++;
            if (GroundOne.WE2.TruthWay150 == -1) totalOpenNum++;

            if (totalOpenNum >= 55) return;

            // 全体のオープンしていない数を数えておき、合計１１を開くようにしなければならない。
            // 初めのオープン場所はnumで指定されているため、それをそのまま開く。
            int ElevenCounter = 11;
            int[] group1 = { 95, 96, 97, 105, 112, 113, 121, 124, 125, 133, 138, 144 };
            foreach (int current in group1)
            {
                if (current == num)
                {
                    MakeWrongAnswerSub1(num); ElevenCounter -= 1;
                    Debug.Log("OpenWrongElevenAnswer call1: " + ElevenCounter);
                    OpenWrongElevenAnswer(ElevenCounter);
                    return;
                }
            }
            int[] group2 = new int[] { 122, 123, 142, 143, 145, 146 };
            foreach (int current in group2)
            {
                if (current == num)
                {
                    MakeWrongAnswerSub2(num); ElevenCounter -= 2;
                    Debug.Log("OpenWrongElevenAnswer call2: " + ElevenCounter);
                    OpenWrongElevenAnswer(ElevenCounter);
                    return;
                }
            }

            int[] group3 = new int[] { 98, 99, 100, 106, 107, 108, 109, 110, 111, 114, 115, 116, 130, 131, 132, 139, 140, 141 };
            foreach (int current in group3)
            {
                if (current == num)
                {
                    MakeWrongAnswerSub3(num); ElevenCounter -= 3;
                    Debug.Log("OpenWrongElevenAnswer call3: " + ElevenCounter);
                    OpenWrongElevenAnswer(ElevenCounter);
                    return;
                }
            }
            int[] group4 = new int[] { 101, 102, 103, 104, 117, 118, 119, 120, 126, 127, 128, 129, 134, 135, 136, 137, 147, 148, 149, 150 };
            foreach (int current in group4)
            {
                if (current == num)
                {
                    MakeWrongAnswerSub4(num); ElevenCounter -= 4;
                    Debug.Log("OpenWrongElevenAnswer call4: " + ElevenCounter);
                    OpenWrongElevenAnswer(ElevenCounter);
                    return;
                }
            }
        }

        private void OpenWrongElevenAnswer(int ElevenCounter)
        {
            Debug.Log("OpenWrongElevenAnswer(S): " + ElevenCounter);

            while (ElevenCounter > 0)
            {
                if (ElevenCounter >= 4)
                {
                    if (SearchArriveGroup4() > 0) { MakeWrongAnswerSub4(); ElevenCounter -= 4; }
                    else if (SearchArriveGroup3() > 0) { MakeWrongAnswerSub3(); ElevenCounter -= 3; }
                    else if (SearchArriveGroup2() > 0) { MakeWrongAnswerSub2(); ElevenCounter -= 2; }
                    else if (SearchArriveGroup1() > 0) { MakeWrongAnswerSub1(); ElevenCounter -= 1; }
                }
                else if (ElevenCounter == 3)
                {
                    if (SearchArriveGroup3() > 0) { MakeWrongAnswerSub3(); ElevenCounter -= 3; }
                    else if (SearchArriveGroup2() > 0) { MakeWrongAnswerSub2(); ElevenCounter -= 2; }
                    else if (SearchArriveGroup1() > 0) { MakeWrongAnswerSub1(); ElevenCounter -= 1; }
                }
                else if (ElevenCounter == 2)
                {
                    if (SearchArriveGroup2() > 0) { MakeWrongAnswerSub2(); ElevenCounter -= 2; }
                    else if (SearchArriveGroup1() > 0) { MakeWrongAnswerSub1(); ElevenCounter -= 1; }
                }
                else
                {
                    if (SearchArriveGroup1() > 0) { MakeWrongAnswerSub1(); ElevenCounter -= 1; }
                    else { return; } // 万が一にも通らないが、万が一のため。
                }
            }
        }

        private int SearchArriveGroup1()
        {
            int result = 0;
            if (GroundOne.WE2.TruthWay95 == 0) result++;
            if (GroundOne.WE2.TruthWay96 == 0) result++;
            if (GroundOne.WE2.TruthWay97 == 0) result++;
            if (GroundOne.WE2.TruthWay105 == 0) result++;
            if (GroundOne.WE2.TruthWay112 == 0) result++;
            if (GroundOne.WE2.TruthWay113 == 0) result++;
            if (GroundOne.WE2.TruthWay121 == 0) result++;
            if (GroundOne.WE2.TruthWay124 == 0) result++;
            if (GroundOne.WE2.TruthWay125 == 0) result++;
            if (GroundOne.WE2.TruthWay133 == 0) result++;
            if (GroundOne.WE2.TruthWay138 == 0) result++;
            if (GroundOne.WE2.TruthWay144 == 0) result++;

            return result;
        }
        private int SearchArriveGroup2()
        {
            int result = 0;
            if (GroundOne.WE2.TruthWay122 == 0) result++;
            if (GroundOne.WE2.TruthWay142 == 0) result++;
            if (GroundOne.WE2.TruthWay145 == 0) result++;

            return result;
        }
        private int SearchArriveGroup3()
        {
            int result = 0;
            if (GroundOne.WE2.TruthWay98 == 0) result++;
            if (GroundOne.WE2.TruthWay106 == 0) result++;
            if (GroundOne.WE2.TruthWay109 == 0) result++;
            if (GroundOne.WE2.TruthWay114 == 0) result++;
            if (GroundOne.WE2.TruthWay130 == 0) result++;
            if (GroundOne.WE2.TruthWay139 == 0) result++;

            return result;
        }
        private int SearchArriveGroup4()
        {
            int result = 0;
            if (GroundOne.WE2.TruthWay101 == 0) result++;
            if (GroundOne.WE2.TruthWay117 == 0) result++;
            if (GroundOne.WE2.TruthWay126 == 0) result++;
            if (GroundOne.WE2.TruthWay134 == 0) result++;
            if (GroundOne.WE2.TruthWay147 == 0) result++;

            return result;
        }

        private void MakeWrongAnswerSub1()
        {
            MakeWrongAnswerSub1(0);
        }
        private void MakeWrongAnswerSub1(int num)
        {
            Debug.Log("MakeWrongAnswerSub1(S) " + num);

            if (GroundOne.WE2.TruthWay95 == 0 && (num == 0 || num == 95))
            {
                Debug.Log("TruthWay95 now wrong!");
                GroundOne.WE2.TruthWay95 = -1; UpdateUnknownTileArea3_One(29, 36); UpdateUnknownTileArea3_Area5(); return;
            }
            if (GroundOne.WE2.TruthWay96 == 0 && (num == 0 || num == 96))
            {
                Debug.Log("TruthWay96 now wrong!");
                GroundOne.WE2.TruthWay96 = -1; UpdateUnknownTileArea3_One(9, 50); UpdateUnknownTileArea3_Area6(); return;
            }
            if (GroundOne.WE2.TruthWay97 == 0 && (num == 0 || num == 97))
            {
                Debug.Log("TruthWay97 now wrong!");
                GroundOne.WE2.TruthWay97 = -1; UpdateUnknownTileArea3_One(14, 45); UpdateUnknownTileArea3_Area7(); return;
            }
            if (GroundOne.WE2.TruthWay105 == 0 && (num == 0 || num == 105))
            {
                Debug.Log("TruthWay105 now wrong!");
                GroundOne.WE2.TruthWay105 = -1; UpdateUnknownTileArea3_One(10, 42); UpdateUnknownTileArea3_Area13(); return;
            }
            if (GroundOne.WE2.TruthWay112 == 0 && (num == 0 || num == 112))
            {
                Debug.Log("TruthWay112 now wrong!");
                GroundOne.WE2.TruthWay112 = -1; UpdateUnknownTileArea3_One(12, 41); UpdateUnknownTileArea3_Area19(); return;
            }
            if (GroundOne.WE2.TruthWay113 == 0 && (num == 0 || num == 113))
            {
                Debug.Log("TruthWay113 now wrong!");
                GroundOne.WE2.TruthWay113 = -1; UpdateUnknownTileArea3_One(17, 21); UpdateUnknownTileArea3_Area20(); return;
            }
            if (GroundOne.WE2.TruthWay121 == 0 && (num == 0 || num == 121))
            {
                Debug.Log("TruthWay121 now wrong!");
                GroundOne.WE2.TruthWay121 = -1; UpdateUnknownTileArea3_One(9, 35); UpdateUnknownTileArea3_Area26(); return;
            }
            if (GroundOne.WE2.TruthWay124 == 0 && (num == 0 || num == 124))
            {
                Debug.Log("TruthWay124 now wrong!");
                GroundOne.WE2.TruthWay124 = -1; UpdateUnknownTileArea3_One(24, 31); UpdateUnknownTileArea3_Area31(); return;
            }
            if (GroundOne.WE2.TruthWay125 == 0 && (num == 0 || num == 125))
            {
                Debug.Log("TruthWay125 now wrong!");
                GroundOne.WE2.TruthWay125 = -1; UpdateUnknownTileArea3_One(39, 47); UpdateUnknownTileArea3_Area32(); return;
            }
            if (GroundOne.WE2.TruthWay133 == 0 && (num == 0 || num == 133))
            {
                Debug.Log("TruthWay133 now wrong!");
                GroundOne.WE2.TruthWay133 = -1; UpdateUnknownTileArea3_One(25, 34); UpdateUnknownTileArea3_Area36(); return;
            }
            if (GroundOne.WE2.TruthWay138 == 0 && (num == 0 || num == 138))
            {
                Debug.Log("TruthWay138 now wrong!");
                GroundOne.WE2.TruthWay138 = -1; UpdateUnknownTileArea3_One(17, 42); UpdateUnknownTileArea3_Area41(); return;
            }
            if (GroundOne.WE2.TruthWay144 == 0 && (num == 0 || num == 144))
            {
                Debug.Log("TruthWay144 now wrong!");
                GroundOne.WE2.TruthWay144 = -1; UpdateUnknownTileArea3_One(24, 53); UpdateUnknownTileArea3_Area46(); return;
            }
        }

        private void MakeWrongAnswerSub2()
        {
            MakeWrongAnswerSub2(0);
        }
        private void MakeWrongAnswerSub2(int num)
        {
            Debug.Log("MakeWrongAnswerSub2(S) " + num);

            if (GroundOne.WE2.TruthWay122 == 0 && (num == 0 || num == 122 || num == 123))
            {
                Debug.Log("TruthWay122,123 now wrong!");
                GroundOne.WE2.TruthWay122 = -1; UpdateUnknownTileArea3_One(20, 50);
                GroundOne.WE2.TruthWay123 = -1; UpdateUnknownTileArea3_One(20, 52);
                UpdateUnknownTileArea3_Area28();
                return;
            }
            if (GroundOne.WE2.TruthWay142 == 0 && (num == 0 || num == 142 || num == 143))
            {
                Debug.Log("TruthWay142,143 now wrong!");
                GroundOne.WE2.TruthWay142 = -1; UpdateUnknownTileArea3_One(24, 44);
                GroundOne.WE2.TruthWay143 = -1; UpdateUnknownTileArea3_One(26, 44);
                UpdateUnknownTileArea3_Area45();
                return;
            }
            if (GroundOne.WE2.TruthWay145 == 0 && (num == 0 || num == 145 || num == 146))
            {
                Debug.Log("TruthWay145,146 now wrong!");
                GroundOne.WE2.TruthWay145 = -1; UpdateUnknownTileArea3_One(6, 51);
                GroundOne.WE2.TruthWay146 = -1; UpdateUnknownTileArea3_One(6, 52);
                UpdateUnknownTileArea3_Area47();
                return;
            }
        }

        private void MakeWrongAnswerSub3()
        {
            MakeWrongAnswerSub3(0);
        }
        private void MakeWrongAnswerSub3(int num)
        {
            Debug.Log("MakeWrongAnswerSub3(S) " + num);
            if (GroundOne.WE2.TruthWay98 == 0 && (num == 0 || num == 98 || num == 99 || num == 100))
            {
                Debug.Log("TruthWay98,99,100 now wrong!");
                GroundOne.WE2.TruthWay98 = -1; UpdateUnknownTileArea3_One(30, 27);
                GroundOne.WE2.TruthWay99 = -1; UpdateUnknownTileArea3_One(30, 29);
                GroundOne.WE2.TruthWay100 = -1; UpdateUnknownTileArea3_One(30, 31);
                UpdateUnknownTileArea3_Area9();
                return;
            }
            if (GroundOne.WE2.TruthWay106 == 0 && (num == 0 || num == 106 || num == 107 || num == 108))
            {
                Debug.Log("TruthWay106,107,108 now wrong!");
                GroundOne.WE2.TruthWay106 = -1; UpdateUnknownTileArea3_One(34, 49);
                GroundOne.WE2.TruthWay107 = -1; UpdateUnknownTileArea3_One(36, 49);
                GroundOne.WE2.TruthWay108 = -1; UpdateUnknownTileArea3_One(38, 49);
                UpdateUnknownTileArea3_Area14();
                return;
            }
            if (GroundOne.WE2.TruthWay109 == 0 && (num == 0 || num == 109 || num == 110 || num == 111))
            {
                Debug.Log("TruthWay109,110,111 now wrong!");
                GroundOne.WE2.TruthWay109 = -1; UpdateUnknownTileArea3_One(31, 37);
                GroundOne.WE2.TruthWay110 = -1; UpdateUnknownTileArea3_One(33, 37);
                GroundOne.WE2.TruthWay111 = -1; UpdateUnknownTileArea3_One(35, 37);
                UpdateUnknownTileArea3_Area17();
                return;
            }
            if (GroundOne.WE2.TruthWay114 == 0 && (num == 0 || num == 114 || num == 115 || num == 116))
            {
                Debug.Log("TruthWay114,115,116 now wrong!");
                GroundOne.WE2.TruthWay114 = -1; UpdateUnknownTileArea3_One(23, 24);
                GroundOne.WE2.TruthWay115 = -1; UpdateUnknownTileArea3_One(23, 26);
                GroundOne.WE2.TruthWay116 = -1; UpdateUnknownTileArea3_One(23, 28);
                UpdateUnknownTileArea3_Area21();
                return;
            }
            if (GroundOne.WE2.TruthWay130 == 0 && (num == 0 || num == 130 || num == 131 || num == 132))
            {
                Debug.Log("TruthWay130,131,132 now wrong!");
                GroundOne.WE2.TruthWay130 = -1; UpdateUnknownTileArea3_One(22, 48);
                GroundOne.WE2.TruthWay131 = -1; UpdateUnknownTileArea3_One(22, 50);
                GroundOne.WE2.TruthWay132 = -1; UpdateUnknownTileArea3_One(22, 52);
                UpdateUnknownTileArea3_Area35();
                return;
            }
            if (GroundOne.WE2.TruthWay139 == 0 && (num == 0 || num == 139 || num == 140 || num == 141))
            {
                Debug.Log("TruthWay139,140,141 now wrong!");
                GroundOne.WE2.TruthWay139 = -1; UpdateUnknownTileArea3_One(8, 22);
                GroundOne.WE2.TruthWay140 = -1; UpdateUnknownTileArea3_One(6, 22);
                GroundOne.WE2.TruthWay141 = -1; UpdateUnknownTileArea3_One(5, 21);
                UpdateUnknownTileArea3_Area43();
                return;
            }
        }

        private void MakeWrongAnswerSub4()
        {
            MakeWrongAnswerSub4(0);
        }
        private void MakeWrongAnswerSub4(int num)
        {
            Debug.Log("MakeWrongAnswerSub4(S) " + num);
            if (GroundOne.WE2.TruthWay101 == 0 && (num == 0 || num == 101 || num == 102 || num == 103 || num == 104))
            {
                Debug.Log("TruthWay101,102,103,104 now wrong!");
                GroundOne.WE2.TruthWay101 = -1; UpdateUnknownTileArea3_One(15, 29);
                GroundOne.WE2.TruthWay102 = -1; UpdateUnknownTileArea3_One(11, 34);
                GroundOne.WE2.TruthWay103 = -1; UpdateUnknownTileArea3_One(11, 24);
                GroundOne.WE2.TruthWay104 = -1; UpdateUnknownTileArea3_One(3, 24);
                UpdateUnknownTileArea3_Area10();
                return;
            }
            if (GroundOne.WE2.TruthWay117 == 0 && (num == 0 || num == 117 || num == 118 || num == 119 || num == 120))
            {
                Debug.Log("TruthWay117,118,119,120 now wrong!");
                GroundOne.WE2.TruthWay117 = -1; UpdateUnknownTileArea3_One(26, 34);
                GroundOne.WE2.TruthWay118 = -1; UpdateUnknownTileArea3_One(28, 34);
                GroundOne.WE2.TruthWay119 = -1; UpdateUnknownTileArea3_One(30, 34);
                GroundOne.WE2.TruthWay120 = -1; UpdateUnknownTileArea3_One(32, 34);
                UpdateUnknownTileArea3_Area25();
                return;
            }
            if (GroundOne.WE2.TruthWay126 == 0 && (num == 0 || num == 126 || num == 127 || num == 128 || num == 129))
            {
                Debug.Log("TruthWay126,127,128,129 now wrong!");
                GroundOne.WE2.TruthWay126 = -1; UpdateUnknownTileArea3_One(10, 54);
                GroundOne.WE2.TruthWay127 = -1; UpdateUnknownTileArea3_One(8, 41);
                GroundOne.WE2.TruthWay128 = -1; UpdateUnknownTileArea3_One(5, 46);
                GroundOne.WE2.TruthWay129 = -1; UpdateUnknownTileArea3_One(15, 42);
                UpdateUnknownTileArea3_Area33();
                return;
            }

            if (GroundOne.WE2.TruthWay134 == 0 && (num == 0 || num == 134 || num == 135 || num == 136 || num == 137))
            {
                Debug.Log("TruthWay134,135,136,137 now wrong!");
                GroundOne.WE2.TruthWay134 = -1; UpdateUnknownTileArea3_One(17, 26);
                GroundOne.WE2.TruthWay135 = -1; UpdateUnknownTileArea3_One(18, 27);
                GroundOne.WE2.TruthWay136 = -1; UpdateUnknownTileArea3_One(19, 28);
                GroundOne.WE2.TruthWay137 = -1; UpdateUnknownTileArea3_One(20, 29);
                UpdateUnknownTileArea3_Area40();
                return;
            }
            if (GroundOne.WE2.TruthWay147 == 0 && (num == 0 || num == 147 || num == 148 || num == 149 || num == 150))
            {
                Debug.Log("TruthWay147,148,149,150 now wrong!");
                GroundOne.WE2.TruthWay147 = -1; UpdateUnknownTileArea3_One(31, 24);
                GroundOne.WE2.TruthWay148 = -1; UpdateUnknownTileArea3_One(26, 22);
                GroundOne.WE2.TruthWay149 = -1; UpdateUnknownTileArea3_One(25, 22);
                GroundOne.WE2.TruthWay150 = -1; UpdateUnknownTileArea3_One(21, 24);
                UpdateUnknownTileArea3_Area48();
                return;
            }
        }
        
        private void UpdateLife(MainCharacter player, Image gauge, Text txt)
        {
            float dx = (float)player.CurrentLife / (float)player.MaxLife;
            txt.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            gauge.rectTransform.localScale = new Vector2(dx, 1.0f);
        }

        private void UpdateMana(MainCharacter player, Image gauge, Text txt)
        {
            float dx = (float)player.CurrentMana / (float)player.MaxMana;
            txt.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            gauge.rectTransform.localScale = new Vector2(dx, 1.0f);
        }

        private void UpdateSkill(MainCharacter player, Image gauge, Text txt)
        {
            float dx = (float)player.CurrentSkillPoint / (float)player.MaxSkillPoint;
            txt.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            gauge.rectTransform.localScale = new Vector2(dx, 1.0f);
        }
                
        private void SetupPlayerStatus()
        {
            SetupPlayerStatus(false);
        }
        private void SetupPlayerStatus(bool initialize)
        {
            if (GroundOne.WE.AvailableFirstCharacter)
            {
                FirstPlayerPanel.gameObject.SetActive(true);
                FirstPlayerPanel.GetComponent<Image>().color = GroundOne.MC.DungeonPanelColor;

                FirstPlayerName.text = GroundOne.MC.FullName;
                currentSkillPoint1.gameObject.SetActive(GroundOne.MC.AvailableSkill);
                currentSkillValue1.gameObject.SetActive(GroundOne.MC.AvailableSkill);
                currentManaPoint1.gameObject.SetActive(GroundOne.MC.AvailableMana);
                currentManaValue1.gameObject.SetActive(GroundOne.MC.AvailableMana);

                if (!GroundOne.MC.AvailableSkill && !GroundOne.MC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref FirstPlayerPanel, 2);
                }
                else if (GroundOne.MC.AvailableSkill && !GroundOne.MC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref FirstPlayerPanel, 1);
                }

                UpdateLife(GroundOne.MC, currentLife1, currentLifeValue1);
                UpdateSkill(GroundOne.MC, currentSkillPoint1, currentSkillValue1);
                UpdateMana(GroundOne.MC, currentManaPoint1, currentManaValue1);
            }
            else
            {
                //FirstPlayerPanel.gameObject.SetActive(false); // そうすると、レイアウトが崩れる。
                FirstPlayerPanel.GetComponent<Image>().color = Color.clear;
                FirstPlayerName.text = "";
                currentLife1.color = Color.clear;
                currentLifeValue1.text = "";
                currentManaPoint1.color = Color.clear;
                currentManaValue1.text = "";
                currentSkillPoint1.color = Color.clear;
                currentSkillValue1.text = "";
            }

            if (GroundOne.WE.AvailableSecondCharacter)
            {
                SecondPlayerPanel.gameObject.SetActive(true);
                SecondPlayerPanel.GetComponent<Image>().color = GroundOne.SC.DungeonPanelColor;

                SecondPlayerName.text = GroundOne.SC.FullName;
                currentSkillPoint2.gameObject.SetActive(GroundOne.SC.AvailableSkill);
                currentSkillValue2.gameObject.SetActive(GroundOne.SC.AvailableSkill);
                currentManaPoint2.gameObject.SetActive(GroundOne.SC.AvailableMana);
                currentManaValue2.gameObject.SetActive(GroundOne.SC.AvailableMana);

                if (!GroundOne.SC.AvailableSkill && !GroundOne.SC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref SecondPlayerPanel, 4);
                }
                else if (GroundOne.SC.AvailableSkill && !GroundOne.SC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref SecondPlayerPanel, 2);
                }

                UpdateLife(GroundOne.SC, currentLife2, currentLifeValue2);
                UpdateMana(GroundOne.SC, currentManaPoint2, currentManaValue2);
                UpdateSkill(GroundOne.SC, currentSkillPoint2, currentSkillValue2);
            }
            else
            {
                //SecondPlayerPanel.gameObject.SetActive(false); // そうすると、レイアウトが崩れる。
                SecondPlayerPanel.GetComponent<Image>().color = Color.clear;
                SecondPlayerName.text = "";
                currentLife2.color = Color.clear;
                currentLifeValue2.text = "";
                currentManaPoint2.color = Color.clear;
                currentManaValue2.text = "";
                currentSkillPoint2.color = Color.clear;
                currentSkillValue2.text = "";
            }

            if (GroundOne.WE.AvailableThirdCharacter)
            {
                ThirdPlayerPanel.gameObject.SetActive(true);
                ThirdPlayerPanel.GetComponent<Image>().color = GroundOne.TC.DungeonPanelColor;

                ThirdPlayerName.text = GroundOne.TC.FullName;
                currentSkillPoint3.gameObject.SetActive(GroundOne.TC.AvailableSkill);
                currentSkillValue3.gameObject.SetActive(GroundOne.TC.AvailableSkill);
                currentManaPoint3.gameObject.SetActive(GroundOne.TC.AvailableMana);
                currentManaValue3.gameObject.SetActive(GroundOne.TC.AvailableMana);

                if (!GroundOne.TC.AvailableSkill && !GroundOne.TC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref ThirdPlayerPanel, 4);
                }
                else if (GroundOne.TC.AvailableSkill && !GroundOne.TC.AvailableMana && initialize) // change unity
                {
                    Method.AddEmptyObj(ref ThirdPlayerPanel, 2);
                }

                UpdateLife(GroundOne.TC, currentLife3, currentLifeValue3);
                UpdateMana(GroundOne.TC, currentManaPoint3, currentManaValue3);
                UpdateSkill(GroundOne.TC, currentSkillPoint3, currentSkillValue3);
            }
            else
            {
                //ThirdPlayerPanel.gameObject.SetActive(false); // そうすると、レイアウトが崩れる。
                ThirdPlayerPanel.GetComponent<Image>().color = Color.clear;
                ThirdPlayerName.text = "";
                currentLife3.color = Color.clear;
                currentLifeValue3.text = "";
                currentManaPoint3.color = Color.clear;
                currentManaValue3.text = "";
                currentSkillPoint3.color = Color.clear;
                currentSkillValue3.text = "";
            }
        }

        private bool CheckInfiniteLoopResult()
        {
            for (int zz = 0; zz < INFINITE_LOOP_MAX; zz++)
            {
                if (this.infinityLoopNumber[zz] != this.playerLoopNumber[zz])
                {
                    return false;
                }
            }
            return true;
        }

        // numの意味  0:fail, 1:success, 2:originFix 
        private void MessageInfiniteLoopResult(int num)
        {
            string[] correct = new string[INFINITE_LOOP_MAX];
            for (int zz = 0; zz < INFINITE_LOOP_MAX; zz++)
            {
                if (this.infinityLoopNumber[zz] == this.playerLoopNumber[zz])
                {
                    correct[zz] = "○";
                }
                else
                {
                    correct[zz] = "×";
                }
            }

            if (num == 2)
            {
                string commentString = "　　　　『　原点を知りし者、　　向かうは　【生】【死】　』\r\n　　　　　　";
                mainMessage.text = commentString
                                    + Database.OriginNumber[0] + " - " + Database.OriginNumber[0] + " = ○    "
                                    + Database.OriginNumber[1] + " - " + Database.OriginNumber[1] + " = ○    "
                                    + Database.OriginNumber[2] + " - " + Database.OriginNumber[2] + " = ○    "
                                    + Database.OriginNumber[3] + " - " + Database.OriginNumber[3] + " = ○\r\n　　　　　　"
                                    + Database.OriginNumber[4] + " - " + Database.OriginNumber[4] + " = ○    "
                                    + Database.OriginNumber[5] + " - " + Database.OriginNumber[5] + " = ○    "
                                    + Database.OriginNumber[6] + " - " + Database.OriginNumber[6] + " = ○    "
                                    + Database.OriginNumber[7] + " - " + Database.OriginNumber[7] + " = ○\r\n　　　　　　"
                                    + Database.OriginNumber[8] + " - " + Database.OriginNumber[8] + " = ○    "
                                    + Database.OriginNumber[9] + " - " + Database.OriginNumber[9] + " = ○    "
                                    + Database.OriginNumber[10] + " - " + Database.OriginNumber[10] + " = ○    "
                                    + Database.OriginNumber[11] + " - " + Database.OriginNumber[11] + " = ○    ";
            }
            else if (num == 1)
            {
                string commentString = "　　　　『　原点を知りし者、　　向かうは　　　　【死】　』\r\n　　　　　　";
                if (GroundOne.WE.dungeonEvent328)
                {
                    commentString = "　　　　『　原点を知りし者、　　向かうは　【生】【死】　』\r\n　　　　　　";
                }
                mainMessage.text = commentString
                                    + this.infinityLoopNumber[0] + " - " + this.playerLoopNumber[0] + " = " + correct[0] + "    "
                                    + this.infinityLoopNumber[1] + " - " + this.playerLoopNumber[1] + " = " + correct[1] + "    "
                                    + this.infinityLoopNumber[2] + " - " + this.playerLoopNumber[2] + " = " + correct[2] + "    "
                                    + this.infinityLoopNumber[3] + " - " + this.playerLoopNumber[3] + " = " + correct[3] + "\r\n　　　　　　"
                                    + this.infinityLoopNumber[4] + " - " + this.playerLoopNumber[4] + " = " + correct[4] + "    "
                                    + this.infinityLoopNumber[5] + " - " + this.playerLoopNumber[5] + " = " + correct[5] + "    "
                                    + this.infinityLoopNumber[6] + " - " + this.playerLoopNumber[6] + " = " + correct[6] + "    "
                                    + this.infinityLoopNumber[7] + " - " + this.playerLoopNumber[7] + " = " + correct[7] + "\r\n　　　　　　"
                                    + this.infinityLoopNumber[8] + " - " + this.playerLoopNumber[8] + " = " + correct[8] + "    "
                                    + this.infinityLoopNumber[9] + " - " + this.playerLoopNumber[9] + " = " + correct[9] + "    "
                                    + this.infinityLoopNumber[10] + " - " + this.playerLoopNumber[10] + " = " + correct[10] + "    "
                                    + this.infinityLoopNumber[11] + " - " + this.playerLoopNumber[11] + " = " + correct[11] + "    ";
            }
            else
            {
                string commentString = "　　　　『　正解を導きし者、無限解の探求にて永遠に彷徨い、原点を知ること無く、回り続けるがよい　』\r\n　　　　　　";
                mainMessage.text = commentString
                                    + this.infinityLoopNumber[0] + " - " + this.playerLoopNumber[0] + " = " + correct[0] + "    "
                                    + this.infinityLoopNumber[1] + " - " + this.playerLoopNumber[1] + " = " + correct[1] + "    "
                                    + this.infinityLoopNumber[2] + " - " + this.playerLoopNumber[2] + " = " + correct[2] + "    "
                                    + this.infinityLoopNumber[3] + " - " + this.playerLoopNumber[3] + " = " + correct[3] + "\r\n　　　　　　"
                                    + this.infinityLoopNumber[4] + " - " + this.playerLoopNumber[4] + " = " + correct[4] + "    "
                                    + this.infinityLoopNumber[5] + " - " + this.playerLoopNumber[5] + " = " + correct[5] + "    "
                                    + this.infinityLoopNumber[6] + " - " + this.playerLoopNumber[6] + " = " + correct[6] + "    "
                                    + this.infinityLoopNumber[7] + " - " + this.playerLoopNumber[7] + " = " + correct[7] + "\r\n　　　　　　"
                                    + this.infinityLoopNumber[8] + " - " + this.playerLoopNumber[8] + " = " + correct[8] + "    "
                                    + this.infinityLoopNumber[9] + " - " + this.playerLoopNumber[9] + " = " + correct[9] + "    "
                                    + this.infinityLoopNumber[10] + " - " + this.playerLoopNumber[10] + " = " + correct[10] + "    "
                                    + this.infinityLoopNumber[11] + " - " + this.playerLoopNumber[11] + " = " + correct[11] + "    ";
            }
        }

        private void RefreshWater()
        {
            if (GroundOne.MC != null) { GroundOne.MC.ResurrectPlayer(GroundOne.MC.MaxLife); GroundOne.MC.MaxGain(); }
            if (GroundOne.SC != null) { GroundOne.SC.ResurrectPlayer(GroundOne.SC.MaxLife); GroundOne.SC.MaxGain(); }
            if (GroundOne.TC != null) { GroundOne.TC.ResurrectPlayer(GroundOne.TC.MaxLife); GroundOne.TC.MaxGain(); }
            SetupPlayerStatus();
        }

        public void MirrorWay(int wayLine, int anotherWayLine)
        {
            // 万が一、設定が無い場合は150, 138, 96, 130, 124で進める事にする。
            if (wayLine < 95 || wayLine > 150)
            {
                if (anotherWayLine == 1) { wayLine = 150; }
                else if (anotherWayLine == 2) { wayLine = 138; }
                else if (anotherWayLine == 3) { wayLine = 96; }
                else if (anotherWayLine == 4) { wayLine = 130; }
                else if (anotherWayLine == 5) { wayLine = 124; } // 正解
                else if (anotherWayLine == 6) { wayLine = 144; } // 原点解
                else { wayLine = 124; } // それ以外の万が一が来た場合はanotherwayline5と同じにする。
            }

            #region "鏡38"
            if (95 <= wayLine && wayLine <= 116)
            {
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10038); // JumpByMirror_2_38();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(2);
                if (95 <= wayLine && wayLine <= 104)
                {
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(10041); // JumpByMirror_2_41();
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    if (wayLine == 95 || wayLine == 96 || wayLine == 97)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10047); // JumpByMirror_2_47();
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        if (wayLine == 95)
                        {
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(10059); // JumpByMirror_2_59();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                        }
                        else if (wayLine == 96)
                        {
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(10060); // JumpByMirror_2_60();
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                        }
                        else if (wayLine == 97)
                        {
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(10061); // JumpByMirror_2_61();
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                        }
                    }
                    else if (wayLine == 98 || wayLine == 99 || wayLine == 100
                          || wayLine == 101 || wayLine == 102 || wayLine == 103 || wayLine == 104)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(10048); // JumpByMirror_2_48();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        if (wayLine == 98 || wayLine == 99 || wayLine == 100)
                        {
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10062); // JumpByMirror_2_62();
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            if (wayLine == 98) { this.nowAutoMoveNumber.Add(1); this.nowAutoMoveNumber.Add(1); }
                            else if (wayLine == 99) { /* なにもなし */ }
                            else if (wayLine == 100) { this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); }
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                        }
                        else if (wayLine == 101 || wayLine == 102 || wayLine == 103 || wayLine == 104)
                        {
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10063); // JumpByMirror_2_63();
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            if (wayLine == 101)
                            {
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                            }
                            else if (wayLine == 102 || wayLine == 103 || wayLine == 104)
                            {
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                if (wayLine == 102)
                                {
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(0);
                                }
                                else if (wayLine == 103 || wayLine == 104)
                                {
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    if (wayLine == 103)
                                    {
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                    }
                                    else// if (truthWay1 == 104) // 万が一を考えた場合の緊急回避として、ポイントミスはelseで始末するべきである
                                    {
                                        this.nowAutoMoveNumber.Add(0);
                                        this.nowAutoMoveNumber.Add(0);
                                        this.nowAutoMoveNumber.Add(0);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (105 <= wayLine && wayLine <= 108)
                {
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(10042); // JumpByMirror_2_42();
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(10049); // JumpByMirror_2_49();
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(0);
                    if (wayLine == 105)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10064); // JumpByMirror_2_64();
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (wayLine == 106 || wayLine == 107 || wayLine == 108)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(10065); // JumpByMirror_2_65();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        if (wayLine == 106) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 107) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 108) { /*何もしない*/ }
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                }
                else if (109 <= wayLine && wayLine <= 116)
                {
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(10043); // JumpByMirror_2_43();
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    if (109 <= wayLine && wayLine <= 111)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10050); // JumpByMirror_2_50();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10066); // JumpByMirror_2_66();
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        if (wayLine == 109) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 110) { /*何もしない*/ }
                        else if (wayLine == 111) { this.nowAutoMoveNumber.Add(3); this.nowAutoMoveNumber.Add(3); }
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (112 <= wayLine && wayLine <= 116)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10051); // JumpByMirror_2_51();
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        if (wayLine == 112)
                        {
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10067); // JumpByMirror_2_67();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                        }
                        else if (wayLine == 113)
                        {
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10068); // JumpByMirror_2_68();
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                        }
                        else if (wayLine == 114 || wayLine == 115 || wayLine == 116)
                        {
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10069); // JumpByMirror_2_69();
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            if (wayLine == 114) { this.nowAutoMoveNumber.Add(1); }
                            else if (wayLine == 115) { this.nowAutoMoveNumber.Add(2); }
                            else if (wayLine == 116) { this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); }
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                        }
                    }
                }
            }
            #endregion
            #region "鏡39"
            else if (117 <= wayLine && wayLine <= 133)
            {
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10039); // JumpByMirror_2_39();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                if (117 <= wayLine && wayLine <= 123)
                {
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(10044); // JumpByMirror_2_44();
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    if (117 <= wayLine && wayLine <= 121)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10052); // JumpByMirror_2_52();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        if (wayLine == 117 || wayLine == 118 || wayLine == 119 || wayLine == 120)
                        {
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(10070); // JumpByMirror_2_70();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            if (wayLine == 117) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                            else if (wayLine == 118) { this.nowAutoMoveNumber.Add(0); }
                            else if (wayLine == 119) { this.nowAutoMoveNumber.Add(3); }
                            else if (wayLine == 120) { this.nowAutoMoveNumber.Add(3); this.nowAutoMoveNumber.Add(3); this.nowAutoMoveNumber.Add(3); }
                            this.nowAutoMoveNumber.Add(2);
                        }
                        else if (wayLine == 121)
                        {
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(10071); // JumpByMirror_2_71();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                        }
                    }
                    else if (wayLine == 122 || wayLine == 123)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10053); // JumpByMirror_2_53();
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(10072); // JumpByMirror_2_72();
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        if (wayLine == 122) { this.nowAutoMoveNumber.Add(1); }
                        else if (wayLine == 123) { this.nowAutoMoveNumber.Add(2); }
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                }
                else if (124 <= wayLine && wayLine <= 133)
                {
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(10045); // JumpByMirror_2_45();
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    if (124 <= wayLine && wayLine <= 129)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10054); // JumpByMirror_2_54();
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        if (wayLine == 124)
                        {
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10073); // JumpByMirror_2_73();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                        }
                        else if (wayLine == 125)
                        {
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(1);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10074); // JumpByMirror_2_74();
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                        }
                        else if (wayLine == 126 || wayLine == 127 || wayLine == 128 || wayLine == 129)
                        {
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(10075); // JumpByMirror_2_75();
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(3);
                            this.nowAutoMoveNumber.Add(3);
                            if (wayLine == 126)
                            {
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                            }
                            else
                            {
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                if (wayLine == 127)
                                {
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                }
                                else
                                {
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                    if (wayLine == 128)
                                    {
                                        this.nowAutoMoveNumber.Add(2);
                                        this.nowAutoMoveNumber.Add(2);
                                        this.nowAutoMoveNumber.Add(2);
                                    }
                                    else
                                    {
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(1);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                        this.nowAutoMoveNumber.Add(3);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10055); // JumpByMirror_2_55();
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        if (130 <= wayLine && wayLine <= 133)
                        {
                            if (wayLine == 133)
                            {
                                this.nowAutoMoveNumber.Add(2);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(10077); // JumpByMirror_2_77();
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(1);
                            }
                            else if (130 <= wayLine && wayLine <= 132)
                            {
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(10076); // JumpByMirror_2_76();
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(3);
                                this.nowAutoMoveNumber.Add(1);
                                this.nowAutoMoveNumber.Add(1);
                                if (wayLine == 130)
                                {
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                }
                                else if (wayLine == 131)
                                {
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(1);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                }
                                else if (wayLine == 132)
                                {
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region "鏡40"
            else if (134 <= wayLine && wayLine <= 150)
            {
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10040); // JumpByMirror_2_40();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(10046); // JumpByMirror_2_46();
                this.nowAutoMoveNumber.Add(3);
                if (134 <= wayLine && wayLine <= 138)
                {
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(10056); // JumpByMirror_2_56();
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    if (wayLine == 134 || wayLine == 135 || wayLine == 136 || wayLine == 137)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10078); // JumpByMirror_2_78();
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        if (wayLine == 134) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 135) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(2); }
                        else if (wayLine == 136) { this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 137) { this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); this.nowAutoMoveNumber.Add(2); }
                    }
                    else if (wayLine == 138)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10079); // JumpByMirror_2_79();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                }
                else if (wayLine == 139 || wayLine == 140 || wayLine == 141)
                {
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(10057); // JumpByMirror_2_57();
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(0);
                    this.nowAutoMoveNumber.Add(10080); // JumpByMirror_2_80();
                    this.nowAutoMoveNumber.Add(0);
                    if (wayLine == 139) { this.nowAutoMoveNumber.Add(2); }
                    else if (wayLine == 140) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(2); }
                    else if (wayLine == 141) { this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); this.nowAutoMoveNumber.Add(0); }
                }
                else if (142 <= wayLine && wayLine <= 150)
                {
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(3);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(1);
                    this.nowAutoMoveNumber.Add(10058); // JumpByMirror_2_58();
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    this.nowAutoMoveNumber.Add(2);
                    if (wayLine == 142 || wayLine == 143)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10081); // JumpByMirror_2_81();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        if (wayLine == 142) { this.nowAutoMoveNumber.Add(0); }
                        else if (wayLine == 143) { this.nowAutoMoveNumber.Add(3); }
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (wayLine == 144)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10082); // JumpByMirror_2_82();
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (wayLine == 145 || wayLine == 146)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10083); // JumpByMirror_2_83();
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        if (wayLine == 145) { this.nowAutoMoveNumber.Add(1); }
                        else if (wayLine == 146) { /* 何もしない */ }
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (wayLine == 147 || wayLine == 148 || wayLine == 149 || wayLine == 150)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10084); // JumpByMirror_2_84();
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        if (wayLine == 147)
                        {
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                            this.nowAutoMoveNumber.Add(2);
                        }
                        else
                        {
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            this.nowAutoMoveNumber.Add(0);
                            if (wayLine == 148)
                            {
                                this.nowAutoMoveNumber.Add(2);
                                this.nowAutoMoveNumber.Add(2);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                            }
                            else
                            {
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                this.nowAutoMoveNumber.Add(0);
                                if (wayLine == 149)
                                {
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(3);
                                    this.nowAutoMoveNumber.Add(3);
                                }
                                else if (wayLine == 150)
                                {
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(0);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                    this.nowAutoMoveNumber.Add(2);
                                }
                            }
                        }

                    }
                }
            }
            #endregion
            this.nowAutoMove = true;
        }

        public void MirrorTruthWay(int wayPoint)
        {
            #region "鏡X1"
            if (wayPoint == 0)
            {
                this.nowAutoMoveNumber.Add(10101); // JumpByMirror_TruthWay1A();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10102); // JumpByMirror_TruthWay1B();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10103); // JumpByMirror_TruthWay1C();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(10104); // JumpByMirror_TruthWay1D();
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1000); // sleep
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                this.nowAutoMoveNumber.Add(500); // sleep
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
            }
            #endregion
            #region "鏡X2"
            else if (wayPoint == 1)
            {
                this.nowAutoMoveNumber.Add(10201); // JumpByMirror_TruthWay2A();
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(10202); // JumpByMirror_TruthWay2B();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10203); // JumpByMirror_TruthWay2C();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10204); // JumpByMirror_TruthWay2D();
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1000); // sleep
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                this.nowAutoMoveNumber.Add(500); // sleep
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
            }
            #endregion
            #region "鏡X3"
            else if (wayPoint == 2)
            {
                this.nowAutoMoveNumber.Add(10301); // JumpByMirror_TruthWay3A();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(10302); // JumpByMirror_TruthWay3B();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10303); // JumpByMirror_TruthWay3C();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10304); // JumpByMirror_TruthWay3D();
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1000); // sleep
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                this.nowAutoMoveNumber.Add(500); // sleep
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
            }
            #endregion
            #region "鏡X4"
            else if (wayPoint == 3)
            {
                this.nowAutoMoveNumber.Add(10401); // JumpByMirror_TruthWay4A();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10402); // JumpByMirror_TruthWay4B();
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10403); // JumpByMirror_TruthWay4C();
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10404); // JumpByMirror_TruthWay4D();
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1000); // sleep
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                this.nowAutoMoveNumber.Add(500); // sleep
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(2);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
            }
            #endregion
            #region "鏡X5"
            else if (wayPoint == 4)
            {
                this.nowAutoMoveNumber.Add(10501); // JumpByMirror_TruthWay5A();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(10502); // JumpByMirror_TruthWay5B();
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(10503); // JumpByMirror_TruthWay5C();
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(3);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(1);
                this.nowAutoMoveNumber.Add(10504); // JumpByMirror_TruthWay5D();
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(500); // sleep
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
                this.nowAutoMoveNumber.Add(0);
            }
            #endregion
            this.nowAutoMove = true;
        }

        public void MirrorLastWay()
        {
            this.nowAutoMoveNumber.Add(0);
            this.nowAutoMoveNumber.Add(0);
            this.nowAutoMoveNumber.Add(0);
            this.nowAutoMoveNumber.Add(10505); // JumpByMirror_TruthWay5E();
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(1000); // sleep
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(3);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMoveNumber.Add(2);
            this.nowAutoMove = true;
        }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOk)
        {
            UpdateMainMessage(message, ignoreOk, false, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOk, bool IgnoreCancelMove)
        {
            UpdateMainMessage(message, ignoreOk, IgnoreCancelMove, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOk, bool IgnoreCancelMove, bool blackImage)
        {
            if (IgnoreCancelMove == false)
            {
                CancelKeyDownMovement();
            }

            //this.nowMessage.Add(message); this.nowEvent.Add(MessagePack.ActionEvent.None);

            if (!ignoreOk)
            {
                tapOK();
                //    using (OKRequest ok = new OKRequest())
                //    {
                //        ok.StartPosition = FormStartPosition.Manual;
                //        ok.LayoutType = 1;
                //        ok.BlackImage = blackImage;
                //        ok.Location = new Point(this.transform.position.x + 647, this.transform.position.y + 703);
                //        ok.ShowDialog();
                //    }
            }
        }

        private bool BlockAction()
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && !GroundOne.WE2.SeekerEvent1)
            {
                mainMessage.text = "アイン：・・・　・・・";
                return true;
            }
            return false;
        }

        public void tapStatus()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_PLAYER_STATUS, "FromDungeon", String.Empty);
            if (BlockAction()) { return; }
            SceneDimension.CallTruthStatusPlayer(this, false, "");
        }
        public void tapBattleSetting()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLE_SETTING, "FromDungeon", String.Empty);
            if (BlockAction()) { return; }
            SceneDimension.CallTruthBattleSetting(this);
        }
        public void tapSave()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_SAVE_GAME, "FromDungeon", String.Empty);
            if (BlockAction()) { return; }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                this.Filter.GetComponent<Image>().color = Color.clear;
                this.Filter.SetActive(true);
                systemMessageText.text = "ここまでの記録は自動セーブされており、新しいセーブはできません。\nゲームを終わりたい場合は、ゲーム終了を押してください。";
                groupSystemMessage.SetActive(true);
                return;
            }

            this.Filter.GetComponent<Image>().color = Color.white;
            this.Filter.SetActive(true);
            SceneDimension.CallSaveLoad(this, true, false);
        }
        public void tapLoad()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_LOAD_GAME, "FromDungeon", String.Empty);
            if (BlockAction()) { return; }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                this.Filter.GetComponent<Image>().color = Color.clear;
                this.Filter.SetActive(true);
                systemMessageText.text = "現在ロードはできません。ここまでの記録は自動セーブされています。\nゲームを終わりたい場合は、ゲーム終了を押してください。";
                groupSystemMessage.SetActive(true);
                return;
            }
            SceneDimension.CallSaveLoad(this, false, false);
        }

        public void tapExit()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_EXIT_GAME, "FromDungeon", String.Empty);
            if (BlockAction()) { return; }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                Method.ExecSave(null, Database.WorldSaveNum, true); // add unity
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld();
                SceneDimension.JumpToTitle();
                return;
            }

            yesnoSystemMessage.text = Database.exitMessage1;
            groupYesnoSystemMessage.SetActive(true);
        }

        public override void BookManual_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_DESCRIPTION, String.Empty, String.Empty);
            if (BlockAction()) { return; }
            this.back_playback.SetActive(false);
            base.BookManual_Click();
        }

        public void DungeonView_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_VIEW_DUNGEON, String.Empty, String.Empty);
            if (BlockAction()) { return; }
            this.back_playback.SetActive(false);
            this.DungeonViewMode = !this.DungeonViewMode;
            if (this.DungeonViewMode)
            {
                this.DungeonViewModeMasterLocation = new Vector2(this.viewPoint.x, this.viewPoint.y);
                this.DungeonViewModeMasterPlayerLocation = new Vector2(this.Player.transform.position.x, this.Player.transform.position.y);

                this.GroupMenu.SetActive(false);
                this.groupPlayerList.SetActive(false);
                this.HelpManual.SetActive(false);
                this.PlayBack.SetActive(false);
                this.BlueOrbImage.SetActive(false);
                this.PathfindingModeImage.SetActive(false);
                this.MovementInterval = 0;
            }
            else
            {
                this.MovementInterval = MOVE_INTERVAL;

                this.viewPoint = new Vector2(this.DungeonViewModeMasterLocation.x, this.DungeonViewModeMasterLocation.y);
                this.Player.transform.position = new Vector2(this.DungeonViewModeMasterPlayerLocation.x, this.DungeonViewModeMasterPlayerLocation.y);

                this.GroupMenu.SetActive(true);
                this.groupPlayerList.SetActive(true);
                this.HelpManual.SetActive(true);
                this.PlayBack.SetActive(true);
                this.BlueOrbImage.SetActive(true);
                this.PathfindingModeImage.SetActive(true);
                UpdateViewPoint(this.DungeonViewModeMasterLocation.x, this.DungeonViewModeMasterLocation.y);
            }
        }

        public void PlayBack_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_PLAYBACK, String.Empty, String.Empty);
            if (BlockAction()) { return; }
            SceneDimension.CallTruthPlayBack(this);
            //if (!this.back_playback.activeInHierarchy)
            //{
            //    this.back_playback.SetActive(true);
            //    this.GroupMenu.SetActive(false);
            //    this.HelpManual.SetActive(false);
            //    this.DungeonView.SetActive(false);
            //    this.BlueOrbImage.SetActive(false);
            //    this.BlueOrbText.SetActive(false);
            //    this.PathfindingModeImage.SetActive(false);
            //    this.labelVigilance.gameObject.SetActive(false);
            //}
            //else
            //{
            //    this.back_playback.SetActive(false);
            //    this.GroupMenu.SetActive(true);
            //    this.HelpManual.SetActive(true);
            //    this.DungeonView.SetActive(true);
            //    this.BlueOrbImage.SetActive(true);
            //    this.PathfindingModeImage.SetActive(true);
            //}
        }

        public void BlueOrb_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BACKTO_TOWN, String.Empty, String.Empty);
            if (BlockAction()) { return; }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                mainMessage.text = "アイン：・・・　・・・";
                return;
            }
            else if (GroundOne.WE.dungeonEvent4_SlayBoss3)
            {
                mainMessage.text = "アイン：（ダメだ。ラナが囚われたままだ。助けるまではもう街へは帰らねえ）";
                return;
            }

            MessagePack.MessageBackToTown(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }

        public void PathfindingMode_Click()
        {
            if (BlockAction()) { return; }
            if (labelVigilance.text == Database.TEXT_VIGILANCE_MODE)
            {
                back_vigilance.sprite = Resources.Load<Sprite>(Database.FINDENEMY_MODE_RESOURCE);
                labelVigilance.text = Database.TEXT_FINDENEMY_MODE;
            }
            else
            {
                back_vigilance.sprite = Resources.Load<Sprite>(Database.VIGILANCE_MODE_RESOURCE);
                labelVigilance.text = Database.TEXT_VIGILANCE_MODE;
            }
        }

        public override void ExitYes()
        {
            base.ExitYes();

            if (yesnoSystemMessage.text == Database.Message_SaveRequest1)
            {
                SceneDimension.CallSaveLoad(this, true, true);
            }
            else if (yesnoSystemMessage.text == Database.Message_SaveRequest2)
            {
                SceneDimension.CallSaveLoad(this, true, true);
            }
            else if (yesnoSystemMessage.text == Database.Message_GotoSkipMirror)
            {
                JumpToLocation(22, -1, true);
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                mainMessage.text = "";
            }
            else if (yesnoSystemMessage.text == Database.Message_OriginOrNormal)
            {
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message13122_2_1(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_Floor4Area3Lever)
            {
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message14072_2(ref nowMessage, ref nowEvent, this.nowFloor4Area3LeverNumber);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_Floor4Area3Lever2)
            {
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message14085_2(ref nowMessage, ref nowEvent, this.nowFloor4Area3LeverNumber2);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_GotoUpstair)
            {
                if (GroundOne.WE.DungeonArea == 2)
                {
                    JumpToLocation(17, -6, true);
                    SetupDungeonMapping(1);
                }
                else if (GroundOne.WE.DungeonArea == 3)
                {
                    JumpToLocation(17, -26, true);
                    SetupDungeonMapping(2);
                }
                else if (GroundOne.WE.DungeonArea == 4)
                {
                    JumpToLocation(59, -39, true);
                    SetupDungeonMapping(3);
                }
                else if (GroundOne.WE.DungeonArea == 5)
                {
                    JumpToLocation(52, -20, true);
                    SetupDungeonMapping(4);
                }
            }
            else if (yesnoSystemMessage.text == Database.Message_GotoDownstair)
            {
                GoToDownStair();
            }
        }

        private void GoToDownStair()
        {
            if (GroundOne.WE.DungeonArea == 1)
            {
                GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_COMPLETE_FLOOR1);
                GroundOne.WE.TruthCompleteArea1 = true;
                JumpToLocation(29, -19, true);
                SetupDungeonMapping(2);
            }
            else if (GroundOne.WE.DungeonArea == 2)
            {
                GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_COMPLETE_FLOOR2);
                GroundOne.WE.TruthCompleteArea2 = true;
                JumpToLocation(0, -19, true);
                SetupDungeonMapping(3);
            }
            else if (GroundOne.WE.DungeonArea == 3)
            {
                GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_COMPLETE_FLOOR3);
                GroundOne.WE.TruthCompleteArea3 = true;
                JumpToLocation(52, -18, true);
                SetupDungeonMapping(4);
            }
            else if (GroundOne.WE.DungeonArea == 4)
            {
                GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_COMPLETE_FLOOR4);
                GroundOne.WE.TruthCompleteArea4 = true;
                JumpToLocation(57, -2, true);
                SetupDungeonMapping(5);
            }
        }

        private void GotoDownStairFourTwo()
        {
            GroundOne.WE.TruthCompleteArea4 = true;
            JumpToLocation(31, -28, true);
            SetupDungeonMapping(4);
        }

        private void GotoDownStairFiveTwo()
        {
            GroundOne.WE.TruthCompleteArea5 = true;
            JumpToLocation(30, -15, true);
            SetupDungeonMapping(5);
        }

        public override void ExitNo()
        {
            base.ExitNo();

            if (yesnoSystemMessage.text == Database.Message_SaveRequest1)
            {
                yesnoSystemMessage.text = Database.Message_SaveRequest2;
            }
            else if (yesnoSystemMessage.text == Database.Message_SaveRequest2)
            {
                SceneDimension.JumpToTitle();
            }
            else if (yesnoSystemMessage.text == Database.Message_OriginOrNormal)
            {
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message13122_2_2(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_Floor4Area3Lever)
            {
                this.nowFloor4Area3LeverNumber = 0;
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message14072_3(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_Floor4Area3Lever2)
            {
                this.nowFloor4Area3LeverNumber2 = 0;
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                MessagePack.Message14085_3(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (yesnoSystemMessage.text == Database.Message_GotoSkipMirror ||
                    yesnoSystemMessage.text == Database.Message_GotoDownstair ||
                    yesnoSystemMessage.text == Database.Message_GotoUpstair)
            {
                this.Filter.SetActive(false);
                groupYesnoSystemMessage.SetActive(false);
                mainMessage.text = "";
            }
        }

        public void tapOK()
        {
            bool HideFilterComplete = true;
            bool ForceSkipTapOK = false;

            if (this.nowReading < this.nowMessage.Count)
            {
                this.Filter.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                this.Filter.SetActive(true);
                this.btnOK.enabled = true;
                this.btnOK.gameObject.SetActive(true);

                this.currentEvent = this.nowEvent[this.nowReading];
                // メッセージ反映
                if (currentEvent == MessagePack.ActionEvent.None)
                {
                    mainMessage.text = "   " + this.nowMessage[this.nowReading];
                    GroundOne.playbackMessage.Add(this.nowMessage[this.nowReading]);
                }

                // 各イベント固有の処理
                if (currentEvent == MessagePack.ActionEvent.UpdateLocationTop)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateLocationBottom)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateLocationLeft)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateLocationRight)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTile)
                {
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.HomeTown)
                {
                    btnYes.enabled = true; btnYes.gameObject.SetActive(true);
                    btnNo.enabled = true; btnNo.gameObject.SetActive(true);

                    //using (YesNoRequest ynr = new YesNoRequest())
                    //{
                    //    ynr.StartPosition = FormStartPosition.CenterParent;
                    //    ynr.ShowDialog();
                    //    if (ynr.DialogResult == DialogResult.Yes)
                    //    {
                    //        CallHomeTown();
                    //        UpdateMainMessage("", true);
                    //    }
                    //    else
                    //    {
                    //        UpdateMainMessage("", true);
                    //    }
                    //}            
                }
                else if (currentEvent == MessagePack.ActionEvent.HomeTownGetItemFullCheck)
                {
                    Method.GetItemFullCheck(this, GroundOne.MC, this.nowMessage[this.nowReading]);
                    this.nowMessage[this.nowReading] = "";
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveTop)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveLeft)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveRight)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveBottom)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenTop)
                {
                    OpenTheDoor(0, this.Player.transform.position);

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);

                    OpenTheDoor(3, this.Player.transform.position);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenBottom)
                {
                    OpenTheDoor(3, this.Player.transform.position);

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN);

                    OpenTheDoor(0, this.Player.transform.position);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenLeft)
                {
                    OpenTheDoor(1, this.Player.transform.position);

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);

                    OpenTheDoor(2, this.Player.transform.position);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenRight)
                {
                    OpenTheDoor(2, this.Player.transform.position);

                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);

                    OpenTheDoor(1, this.Player.transform.position);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BigEntranceOpen)
                {
                    OpenTheDoor(3, new Vector3(14, -26, 0));
                    OpenTheDoor(0, new Vector3(14, -27, 0));

                    OpenTheDoor(0, new Vector3(14, -35, 0));
                    OpenTheDoor(3, new Vector3(14, -34, 0));

                    OpenTheDoor(1, new Vector3(13, -28, 0));
                    OpenTheDoor(2, new Vector3(12, -28, 0));

                    OpenTheDoor(1, new Vector3(13, -33, 0));
                    OpenTheDoor(2, new Vector3(12, -33, 0));

                    OpenTheDoor(1, new Vector3(16, -10, 0));
                    OpenTheDoor(2, new Vector3(15, -10, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.SmallEntranceOpen1)
                {
                    OpenTheDoor(0, this.Player.transform.position);

                    UpdateUnknownTileArea12();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);

                    OpenTheDoor(3, this.Player.transform.position);
                }
                else if (currentEvent == MessagePack.ActionEvent.SmallEntranceOpen2)
                {
                    OpenTheDoor(1, this.Player.transform.position);

                    UpdateUnknownTileArea12();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);

                    OpenTheDoor(2, this.Player.transform.position);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2CenterOpen)
                {
                    OpenTheDoor(3, new Vector3(33, -17, 0));
                    OpenTheDoor(0, new Vector3(33, -18, 0));

                    OpenTheDoor(3, new Vector3(25, -20, 0));
                    OpenTheDoor(0, new Vector3(25, -21, 0));

                    OpenTheDoor(2, new Vector3(30, -23, 0));
                    OpenTheDoor(1, new Vector3(31, -23, 0));

                    OpenTheDoor(2, new Vector3(27, -15, 0));
                    OpenTheDoor(1, new Vector3(28, -15, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomOpen1)
                {
                    OpenTheDoor(1, new Vector3(50, -17, 0));
                    OpenTheDoor(2, new Vector3(49, -17, 0));

                    OpenTheDoor(3, new Vector3(42, -2, 0));
                    OpenTheDoor(0, new Vector3(42, -3, 0));

                    OpenTheDoor(3, new Vector3(46, -7, 0));
                    OpenTheDoor(0, new Vector3(46, -8, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomRequestInput1)
                {
                    this.nowIntelligenceRoom = 1;
                    SceneDimension.CallTruthInputRequest(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomRequestInput2)
                {
                    this.nowIntelligenceRoom = 2;
                    SceneDimension.CallTruthInputRequest(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomRequestInput3)
                {
                    this.nowIntelligenceRoom = 3;
                    SceneDimension.CallTruthInputRequest(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomGodSequence)
                {
                    this.nowIntelligenceRoomGodSequence = true;
                    SceneDimension.CallTruthAnswer(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomOpen2)
                {
                    OpenTheDoor(3, new Vector3(38, -7, 0));
                    OpenTheDoor(0, new Vector3(38, -8, 0));

                    OpenTheDoor(1, new Vector3(50, -11, 0));
                    OpenTheDoor(2, new Vector3(49, -11, 0));

                    OpenTheDoor(1, new Vector3(59, -14, 0));
                    OpenTheDoor(2, new Vector3(58, -14, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.IntelligenceRoomOpen3)
                {
                    OpenTheDoor(1, new Vector3(47, -15, 0));
                    OpenTheDoor(2, new Vector3(46, -15, 0));

                    OpenTheDoor(1, new Vector3(50, -2, 0));
                    OpenTheDoor(2, new Vector3(49, -2, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen1)
                {
                    OpenTheDoor(3, new Vector3(21, -34, 0));
                    OpenTheDoor(0, new Vector3(21, -35, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen2)
                {
                    OpenTheDoor(3, new Vector3(21, -23, 0));
                    OpenTheDoor(0, new Vector3(21, -24, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen3)
                {
                    OpenTheDoor(2, new Vector3(10, -18, 0));
                    OpenTheDoor(1, new Vector3(11, -18, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen4)
                {
                    OpenTheDoor(3, new Vector3(6, -27, 0));
                    OpenTheDoor(0, new Vector3(6, -28, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen5)
                {
                    OpenTheDoor(3, new Vector3(5, -37, 0));
                    OpenTheDoor(0, new Vector3(5, -38, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.StrengthRoomOpen6)
                {
                    OpenTheDoor(3, new Vector3(14, -28, 0));
                    OpenTheDoor(0, new Vector3(14, -29, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer1)
                {
                    this.nowDecisionFloor2EightAnswer = 1;
                    SceneDimension.CallTruthDecision2(this, "歌い、木々が囁き始める", "鳥", "空", "樹", "霊", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer2)
                {
                    this.nowDecisionFloor2EightAnswer = 2;
                    SceneDimension.CallTruthDecision2(this, "青く照らし、地は新緑を謳歌する", "湖", "人", "天", "海", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer3)
                {
                    this.nowDecisionFloor2EightAnswer = 3;
                    SceneDimension.CallTruthDecision2(this, "流れ落ち、偉大なる海、天へと還り、無限循環", "生", "水", "死", "光", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer4)
                {
                    this.nowDecisionFloor2EightAnswer = 4;
                    SceneDimension.CallTruthDecision2(this, "あらゆる場所、可能な場を生めつくし、創元浄化", "天", "災", "灰", "火", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer5)
                {
                    this.nowDecisionFloor2EightAnswer = 5;
                    SceneDimension.CallTruthDecision2(this, "この世における絶対的な平等の象徴", "死", "母", "父", "命", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer6)
                {
                    this.nowDecisionFloor2EightAnswer = 6;
                    SceneDimension.CallTruthDecision2(this, "偉大なる母、厳格なる父より生み出されし", "源", "生", "滅", "諭", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer7)
                {
                    this.nowDecisionFloor2EightAnswer = 7;
                    SceneDimension.CallTruthDecision2(this, "誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在", "敵", "僧", "人", "神", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswer8)
                {
                    this.nowDecisionFloor2EightAnswer = 8;
                    SceneDimension.CallTruthDecision2(this, "神と人、鳥、木々、全生物における連続の理そこに見つけたり", "虚", "真", "心", "理", false);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2EightAnswerFinal)
                {
                    this.nowDecisionFloor2EightAnswer = 9;
                    SceneDimension.CallTruthDecision2(this, "４つのフロアを心得し者、その順列を示せ。", "知", "技", "力", "心", true);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2FinalRoomOpen)
                {
                    objList[26 * Database.TRUTH_DUNGEON_COLUMN + 13].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo2[26 * Database.TRUTH_DUNGEON_COLUMN + 13] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_11)
                {
                    UpdateUnknownTileArea2_11();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_12)
                {
                    UpdateUnknownTileArea2_12();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_13)
                {
                    UpdateUnknownTileArea2_13();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_14)
                {
                    UpdateUnknownTileArea2_14();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_15)
                {
                    UpdateUnknownTileArea2_15();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea2_16)
                {
                    UpdateUnknownTileArea2_16();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea24)
                {
                    UpdateUnknownTileArea24();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea25)
                {
                    UpdateUnknownTileArea25();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea26)
                {
                    UpdateUnknownTileArea26();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea27)
                {
                    UpdateUnknownTileArea27();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea28)
                {
                    UpdateUnknownTileArea28();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea29)
                {
                    UpdateUnknownTileArea29();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_1)
                {
                    UpdateUnknownTileArea3_1();
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor1MindRoomOpen1)
                {
                    objList[29 * Database.TRUTH_DUNGEON_COLUMN + 50].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_21);
                    tileInfo[29 * Database.TRUTH_DUNGEON_COLUMN + 50] = Database.TILEINFO_21;
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2MindRoomOpen1)
                {
                    objList[5 * Database.TRUTH_DUNGEON_COLUMN + 28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo2[5 * Database.TRUTH_DUNGEON_COLUMN + 28] = Database.TILEINFO_13;

                    objList[6 * Database.TRUTH_DUNGEON_COLUMN + 28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo2[6 * Database.TRUTH_DUNGEON_COLUMN + 28] = Database.TILEINFO_13;

                    objList[7 * Database.TRUTH_DUNGEON_COLUMN + 28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo2[7 * Database.TRUTH_DUNGEON_COLUMN + 28] = Database.TILEINFO_13;

                    UpdateUnknownTileArea2_10();

                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.ReturnToNormal)
                {
                    ReturnToNormal();
                }
                else if (currentEvent == MessagePack.ActionEvent.TurnToBlack)
                {
                    TurnToBlack();
                }
                else if (currentEvent == MessagePack.ActionEvent.TurnToWhite)
                {
                    TurnToWhite();
                }
                else if (currentEvent == MessagePack.ActionEvent.CenterBlueOpen)
                {
                    OpenTheDoor(1, new Vector3(13, -16, 0));
                    OpenTheDoor(2, new Vector3(12, -16, 0));
                }
                else if (currentEvent == MessagePack.ActionEvent.EncountBoss)
                {
                    GroundOne.enemyName1 = this.nowMessage[this.nowReading];
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                    if (this.nowMessage[this.nowReading] == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                    {
                        GroundOne.enemyName2 = Database.ENEMY_JELLY_EYE_DEEP_BLUE;
                    }
                    else if (this.nowMessage[this.nowReading] == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                    {
                        GroundOne.enemyName2 = Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU;
                        GroundOne.enemyName3 = Database.ENEMY_SEA_STAR_KNIGHT_AMARA;
                    }
                    CancelKeyDownMovement();
                    SceneDimension.CallTruthBattleEnemy(Database.TruthDungeon, false, false, false, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.StopMusic)
                {
                    GroundOne.StopDungeonMusic();
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic14)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic15)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic16)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.YesNoGotoDungeon)
                {
                    yesnoSystemMessage.text = Database.Message_GotoDownstair;
                    groupYesnoSystemMessage.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                }
                else if (currentEvent == MessagePack.ActionEvent.YesNoBacktoDungeon)
                {
                    yesnoSystemMessage.text = Database.Message_GotoUpstair;
                    groupYesnoSystemMessage.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                }
                else if (currentEvent == MessagePack.ActionEvent.GotoHomeTown)
                {
                    yesnoSystemMessage.text = Database.exitMessage3;
                    groupYesnoSystemMessage.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                }
                else if (currentEvent == MessagePack.ActionEvent.GotoHomeTownForce)
                {
                    CallHomeTown();
                }
                else if (currentEvent == MessagePack.ActionEvent.DecisionOpenDoor1)
                {
                    this.nowDecisionFloor1OpenDoor = true;
                    GroundOne.DecisionSequence = 0;
                    GroundOne.DecisionMainMessage = "【　扉を開けますか？　】";
                    GroundOne.DecisionFirstMessage = "扉を開ける。";
                    GroundOne.DecisionSecondMessage = "扉を開けず、他を探す。";
                    this.Filter.SetActive(true);
                    SceneDimension.CallTruthDecision(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonBadEnd)
                {
                    if (GroundOne.WE2.RealWorld)
                    {
                        Method.ExecSave(null, Database.WorldSaveNum, true);
                        SceneDimension.JumpToTitle();
                    }
                    else
                    {
                        SceneDimension.CallSaveLoad(this, true, true);
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonGetTreasure)
                {
                    // todo メッセージ進行中の宝箱ゲットはどう実装するか？
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateFieldElement)
                {
                    UpdateFieldElement(this.Player.transform.position);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomStart)
                {
                    this.nowAgilityRoomCounter = 30;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomStart2)
                {
                    if (!GroundOne.WE.dungeonEvent234_Fail1)
                    {
                        this.nowAgilityRoomCounter = 300;
                    }
                    else if (!GroundOne.WE.dungeonEvent234_Fail2)
                    {
                        this.nowAgilityRoomCounter = 500;
                    }
                    else if (!GroundOne.WE.dungeonEvent234_Fail3)
                    {
                        this.nowAgilityRoomCounter = 700;
                    }
                    else
                    {
                        this.nowAgilityRoomCounter = 1000;
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomStart3)
                {
                    this.detectKeyUp = false;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomUpdate3)
                {
                    this.nowAgilityRoomCounter = 75;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomNormal4)
                {
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_26);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    objList[33 * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);

                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);

                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_17);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                    objList[35 * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_14);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomUpdate4)
                {
                    this.nowAgilityRoomCounter = 1;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomStart5)
                {
                    if (GroundOne.WE.dungeonEvent237_Fail3)
                    {
                        this.nowAgilityRoomCounter = 520;
                    }
                    else if (GroundOne.WE.dungeonEvent237_Fail2)
                    {
                        this.nowAgilityRoomCounter = 490;
                    }
                    else if (GroundOne.WE.dungeonEvent237_Fail1)
                    {
                        this.nowAgilityRoomCounter = 460;
                    }
                    else
                    {
                        this.nowAgilityRoomCounter = 440;
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomStop)
                {
                    this.nowAgilityRoomCounter = 0;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomFail1)
                {
                    JumpToLocation(57, -27, true);
                    GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomFail2)
                {
                    JumpToLocation(45, -27, true);
                    GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);

                    this.ShadowTileNumber = -1;
                    this.BeforeDirectionNumber = 0;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomFail3)
                {
                    JumpToLocation(33, -27, true);
                    GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomFail4)
                {
                    JumpToLocation(35, -34, true);
                    GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilityRoomFail5)
                {
                    JumpToLocation(47, -34, true);
                    GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonAgilitySecretOpen)
                {
                    objList[36 * Database.TRUTH_DUNGEON_COLUMN + 59].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_21);
                }
                else if (currentEvent == MessagePack.ActionEvent.DecisionOpenDoor2)
                {
                    this.nowDecisionFloor2OpenDoor = true;
                    GroundOne.DecisionSequence = 0;
                    GroundOne.DecisionMainMessage = "【　扉を開けますか？　】";
                    GroundOne.DecisionFirstMessage = "扉を開ける。";
                    GroundOne.DecisionSecondMessage = "扉を開けず、他を探す。";
                    this.Filter.SetActive(true);
                    SceneDimension.CallTruthDecision(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.Floor2DownstairGateOpen)
                {
                    OpenTheDoor(2, new Vector3(16, -26, 0));
                    OpenTheDoor(1, new Vector3(17, -26, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation1)
                {
                    JumpToLocation(16, -22, true);
                    UpdateUnknownTileArea3_0_1();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation2)
                {
                    JumpToLocation(0, -33, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation3)
                {
                    JumpToLocation(16, -2, true);
                    UpdateUnknownTileArea3_0_2();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation4)
                {
                    JumpToLocation(8, -12, true);
                    UpdateUnknownTileArea3_0_3();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation5)
                {
                    JumpToLocation(8, -23, true);
                    UpdateUnknownTileArea3_0_4();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation6)
                {
                    JumpToLocation(11, -26, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation7)
                {
                    JumpToLocation(14, -4, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation8)
                {
                    JumpToLocation(2, -2, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation9)
                {
                    JumpToLocation(16, -13, true);
                    UpdateUnknownTileArea3_0_5();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation10)
                {
                    JumpToLocation(14, -36, true);
                    UpdateUnknownTileArea3_0_6();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation11)
                {
                    JumpToLocation(12, -20, true);
                    UpdateUnknownTileArea3_0_7();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation12)
                {
                    JumpToLocation(8, -4, true);
                    UpdateUnknownTileArea3_0_8();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation13)
                {
                    JumpToLocation(0, -13, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation14)
                {
                    JumpToLocation(13, -7, true);
                    UpdateUnknownTileArea3_0_9();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation15)
                {
                    JumpToLocation(10, -21, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation16)
                {
                    JumpToLocation(8, -37, true);
                    UpdateUnknownTileArea3_0_10();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation17)
                {
                    JumpToLocation(15, -28, true);
                    UpdateUnknownTileArea3_0_11();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation18)
                {
                    JumpToLocation(12, -32, true);
                    UpdateUnknownTileArea3_0_12();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation19)
                {
                    JumpToLocation(9, -33, true);
                    UpdateUnknownTileArea3_0_13();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation20)
                {
                    JumpToLocation(12, -7, true);
                    UpdateUnknownTileArea3_0_9();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation21)
                {
                    JumpToLocation(12, -12, true);
                    UpdateUnknownTileArea3_0_14();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation22)
                {
                    JumpToLocation(19, 0, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation23)
                {
                    JumpToLocation(19, -39, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation24)
                {
                    JumpToLocation(12, -33, true);
                    UpdateUnknownTileArea3_0_12();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation25)
                {
                    JumpToLocation(18, -12, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation26)
                {
                    JumpToLocation(2, -37, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation27)
                {
                    JumpToLocation(9, -34, true);
                    UpdateUnknownTileArea3_0_13();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation28)
                {
                    JumpToLocation(16, -7, true);
                    UpdateUnknownTileArea3_0_15();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation29)
                {
                    JumpToLocation(14, -37, true);
                    UpdateUnknownTileArea3_0_6();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation30)
                {
                    JumpToLocation(7, -23, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation31)
                {
                    JumpToLocation(4, -31, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation32)
                {
                    JumpToLocation(12, -21, true);
                    UpdateUnknownTileArea3_0_7();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation33)
                {
                    JumpToLocation(8, -28, true);
                    UpdateUnknownTileArea3_0_16();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation34)
                {
                    JumpToLocation(7, -37, true);
                    UpdateUnknownTileArea3_0_10();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation35)
                {
                    JumpToLocation(6, 0, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation36)
                {
                    JumpToLocation(7, -12, true);
                    UpdateUnknownTileArea3_0_3();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation37)
                {
                    JumpToLocation(1, -38, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationFail1)
                {
                    JumpToLocation(3, -19, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationArea1End)
                {
                    JumpToLocation(18, -19, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation38)
                {
                    JumpByMirror_2_38();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation39)
                {
                    JumpByMirror_2_39();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation40)
                {
                    JumpByMirror_2_40();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation41)
                {
                    JumpByMirror_2_41();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation42)
                {
                    JumpByMirror_2_42();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation43)
                {
                    JumpByMirror_2_43();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation44)
                {
                    JumpByMirror_2_44();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation45)
                {
                    JumpByMirror_2_45();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation46)
                {
                    JumpByMirror_2_46();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation47)
                {
                    JumpByMirror_2_47();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation48)
                {
                    JumpByMirror_2_48();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation49)
                {
                    JumpByMirror_2_49();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation50)
                {
                    JumpByMirror_2_50();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation51)
                {
                    JumpByMirror_2_51();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation52)
                {
                    JumpByMirror_2_52();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation53)
                {
                    JumpByMirror_2_53();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation54)
                {
                    JumpByMirror_2_54();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation55)
                {
                    JumpByMirror_2_55();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation56)
                {
                    JumpByMirror_2_56();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation57)
                {
                    JumpByMirror_2_57();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation58)
                {
                    JumpByMirror_2_58();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation59)
                {
                    JumpByMirror_2_59();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation60)
                {
                    JumpByMirror_2_60();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation61)
                {
                    JumpByMirror_2_61();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation62)
                {
                    JumpByMirror_2_62();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation63)
                {
                    JumpByMirror_2_63();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation64)
                {
                    JumpByMirror_2_64();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation65)
                {
                    JumpByMirror_2_65();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation66)
                {
                    JumpByMirror_2_66();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation67)
                {
                    JumpByMirror_2_67();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation68)
                {
                    JumpByMirror_2_68();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation69)
                {
                    JumpByMirror_2_69();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation70)
                {
                    JumpByMirror_2_70();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation71)
                {
                    JumpByMirror_2_71();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation72)
                {
                    JumpByMirror_2_72();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation73)
                {
                    JumpByMirror_2_73();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation74)
                {
                    JumpByMirror_2_74();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation75)
                {
                    JumpByMirror_2_75();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation76)
                {
                    JumpByMirror_2_76();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation77)
                {
                    JumpByMirror_2_77();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation78)
                {
                    JumpByMirror_2_78();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation79)
                {
                    JumpByMirror_2_79();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation80)
                {
                    JumpByMirror_2_80();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation81)
                {
                    JumpByMirror_2_81();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation82)
                {
                    JumpByMirror_2_82();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation83)
                {
                    JumpByMirror_2_83();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocation84)
                {
                    JumpByMirror_2_84();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonMakeCorrectAnswer)
                {
                    int num = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    MakeCorrectAnswer(num);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationTurnBack)
                {
                    JumpByMirror_TurnBack();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay1A)
                {
                    JumpByMirror_TruthWay1A();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay1B)
                {
                    JumpByMirror_TruthWay1B();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay1C)
                {
                    JumpByMirror_TruthWay1C();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay1D)
                {
                    JumpByMirror_TruthWay1D();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay2A)
                {
                    JumpByMirror_TruthWay2A();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay2B)
                {
                    JumpByMirror_TruthWay2B();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay2C)
                {
                    JumpByMirror_TruthWay2C();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay2D)
                {
                    JumpByMirror_TruthWay2D();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay3A)
                {
                    JumpByMirror_TruthWay3A();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay3B)
                {
                    JumpByMirror_TruthWay3B();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay3C)
                {
                    JumpByMirror_TruthWay3C();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay3D)
                {
                    JumpByMirror_TruthWay3D();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay4A)
                {
                    JumpByMirror_TruthWay4A();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay4B)
                {
                    JumpByMirror_TruthWay4B();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay4C)
                {
                    JumpByMirror_TruthWay4C();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay4D)
                {
                    JumpByMirror_TruthWay4D();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay5A)
                {
                    JumpByMirror_TruthWay5A();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay5B)
                {
                    JumpByMirror_TruthWay5B();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay5C)
                {
                    JumpByMirror_TruthWay5C();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay5D)
                {
                    JumpByMirror_TruthWay5D();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocatinTruthWay5E)
                {
                    JumpByMirror_TruthWay5E();
                    //UpdateUnknownTileArea3_Area68(); X5ルート最後は一歩ずつ進ませる事とする。
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonMirrorRoomGodSequence)
                {
                    this.nowMirrorRoomGodSequence = true;
                    SceneDimension.CallTruthWill(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonYesNoSkipMirror)
                {
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                    this.mainMessage.text = this.nowMessage[this.nowReading];
                    this.yesnoSystemMessage.text = Database.Message_GotoSkipMirror;
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DecisionOpenDoor3)
                {
                    this.nowDecisionFloor3OpenDoor = true;
                    GroundOne.DecisionSequence = 0;
                    GroundOne.DecisionMainMessage = "【　扉を開けますか？　】";
                    GroundOne.DecisionFirstMessage = "扉を開ける。";
                    GroundOne.DecisionSecondMessage = "扉を開けず、他を探す。";
                    this.Filter.SetActive(true);
                    SceneDimension.CallTruthDecision(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonYesNoOriginOrNormal)
                {
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                    this.mainMessage.text = this.nowMessage[this.nowReading];
                    this.yesnoSystemMessage.text = Database.Message_OriginOrNormal;
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationRecollection3)
                {
                    JumpToLocation(1, -10, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationRecollection4)
                {
                    JumpByMirror_Recollection4();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationZeroWay)
                {
                    JumpToLocation(37, -12, true);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonSetupOriginRandom)
                {
                    for (int zz = 0; zz < INFINITE_LOOP_MAX; zz++)
                    {
                        this.infinityLoopNumber[zz] = AP.Math.RandomInteger(5) + 1;
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonSetupOriginFix)
                {
                    for (int zz = 0; zz < INFINITE_LOOP_MAX; zz++)
                    {
                        this.infinityLoopNumber[zz] = Database.OriginNumber[zz];
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity1)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[0] = ii;
                    JumpByMirror_InfinityWay1();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity2)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[1] = ii;
                    JumpByMirror_InfinityWay2();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity3)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[2] = ii;
                    JumpByMirror_InfinityWay3();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity4)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[3] = ii;
                    JumpByMirror_InfinityWay4();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity5)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[4] = ii;
                    JumpByMirror_InfinityWay5();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity6)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[5] = ii;
                    JumpByMirror_InfinityWay6();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity7)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[6] = ii;
                    JumpByMirror_InfinityWay7();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity8)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[7] = ii;
                    JumpByMirror_InfinityWay8();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity9)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[8] = ii;
                    JumpByMirror_InfinityWay9();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity10)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[9] = ii;
                    JumpByMirror_InfinityWay10();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinity11)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[10] = ii;
                    JumpByMirror_InfinityWay11();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinityTurnBack)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[11] = ii;
                    JumpByMirror_InfinityWayTurnBack();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinityBadEndBack)
                {
                    JumpToLocation(56, -39, true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonJumpToLocationInfinityLast)
                {
                    int ii = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.playerLoopNumber[11] = ii;
                    JumpByMirror_InfinityWayLast();
                    ForceSkipTapOK = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area1)
                {
                    UpdateUnknownTileArea3_Area1();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area2)
                {
                    UpdateUnknownTileArea3_Area2();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area3)
                {
                    UpdateUnknownTileArea3_Area3();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area4)
                {
                    UpdateUnknownTileArea3_Area4();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area5)
                {
                    UpdateUnknownTileArea3_Area5();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area6)
                {
                    UpdateUnknownTileArea3_Area6();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area7)
                {
                    UpdateUnknownTileArea3_Area7();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area8)
                {
                    UpdateUnknownTileArea3_Area8();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area9)
                {
                    UpdateUnknownTileArea3_Area9();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area10)
                {
                    UpdateUnknownTileArea3_Area10();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area11)
                {
                    UpdateUnknownTileArea3_Area11();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area12)
                {
                    UpdateUnknownTileArea3_Area12();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area13)
                {
                    UpdateUnknownTileArea3_Area13();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area14)
                {
                    UpdateUnknownTileArea3_Area14();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area15)
                {
                    UpdateUnknownTileArea3_Area15();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area16)
                {
                    UpdateUnknownTileArea3_Area16();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area17)
                {
                    UpdateUnknownTileArea3_Area17();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area18)
                {
                    UpdateUnknownTileArea3_Area18();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area19)
                {
                    UpdateUnknownTileArea3_Area19();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area20)
                {
                    UpdateUnknownTileArea3_Area20();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area21)
                {
                    UpdateUnknownTileArea3_Area21();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area22)
                {
                    UpdateUnknownTileArea3_Area22();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area23)
                {
                    UpdateUnknownTileArea3_Area23();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area24)
                {
                    UpdateUnknownTileArea3_Area24();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area25)
                {
                    UpdateUnknownTileArea3_Area25();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area26)
                {
                    UpdateUnknownTileArea3_Area26();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area27)
                {
                    UpdateUnknownTileArea3_Area27();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area28)
                {
                    UpdateUnknownTileArea3_Area28();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area29)
                {
                    UpdateUnknownTileArea3_Area29();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area30)
                {
                    UpdateUnknownTileArea3_Area30();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area31)
                {
                    UpdateUnknownTileArea3_Area31();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area32)
                {
                    UpdateUnknownTileArea3_Area32();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area33)
                {
                    UpdateUnknownTileArea3_Area33();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area34)
                {
                    UpdateUnknownTileArea3_Area34();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area35)
                {
                    UpdateUnknownTileArea3_Area35();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area36)
                {
                    UpdateUnknownTileArea3_Area36();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area37)
                {
                    UpdateUnknownTileArea3_Area37();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area38)
                {
                    UpdateUnknownTileArea3_Area38();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area39)
                {
                    UpdateUnknownTileArea3_Area39();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area40)
                {
                    UpdateUnknownTileArea3_Area40();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area41)
                {
                    UpdateUnknownTileArea3_Area41();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area42)
                {
                    UpdateUnknownTileArea3_Area42();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area43)
                {
                    UpdateUnknownTileArea3_Area43();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area44)
                {
                    UpdateUnknownTileArea3_Area44();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area45)
                {
                    UpdateUnknownTileArea3_Area45();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area46)
                {
                    UpdateUnknownTileArea3_Area46();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area47)
                {
                    UpdateUnknownTileArea3_Area47();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area48)
                {
                    UpdateUnknownTileArea3_Area48();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Area69)
                {
                    UpdateUnknownTileArea3_Area69();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Last0)
                {
                    UpdateUnknownTileArea3_Last0();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_Last)
                {
                    int jjStart = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    UpdateUnknownTileArea3_Last(jjStart);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonMessageInfiniteLoopResult)
                {
                    int num = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    MessageInfiniteLoopResult(num);
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileArea3_TruthLast)
                {
                    UpdateUnknownTileArea3_TruthLast();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonRemovePartyTC)
                {
                    Method.RemoveParty(GroundOne.TC, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonRefreshWater)
                {
                    RefreshWater();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonSetupPlayerStatus)
                {
                    SetupPlayerStatus(false);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateA1)
                {
                    OpenTheDoor(3, new Vector3(45, -16, 0));
                    OpenTheDoor(0, new Vector3(45, -17, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateA2)
                {
                    OpenTheDoor(3, new Vector3(47, -16, 0));
                    OpenTheDoor(0, new Vector3(47, -17, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea41)
                {
                    UpdateUnknownTileArea41();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea42)
                {
                    UpdateUnknownTileArea42();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea43)
                {
                    UpdateUnknownTileArea43();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenWallA1)
                {
                    objList[19 * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo4[19 * Database.TRUTH_DUNGEON_COLUMN + 44] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea421)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 18, 22, 17, 21);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB1)
                {
                    OpenTheDoor(2, new Vector3(17, -19, 0));
                    OpenTheDoor(1, new Vector3(18, -19, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB2)
                {
                    OpenTheDoor(2, new Vector3(10, -8, 0));
                    OpenTheDoor(1, new Vector3(11, -8, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB3)
                {
                    OpenTheDoor(3, new Vector3(11, -7, 0));
                    OpenTheDoor(0, new Vector3(11, -8, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB4)
                {
                    OpenTheDoor(3, new Vector3(6, -7, 0));
                    OpenTheDoor(0, new Vector3(6, -8, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB5)
                {
                    OpenTheDoor(2, new Vector3(11, -2, 0));
                    OpenTheDoor(1, new Vector3(12, -2, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB6)
                {
                    OpenTheDoor(2, new Vector3(11, -8, 0));
                    OpenTheDoor(1, new Vector3(12, -8, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB7)
                {
                    OpenTheDoor(3, new Vector3(16, -13, 0));
                    OpenTheDoor(0, new Vector3(16, -14, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea422)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 17, 20, 16, 16);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateB8)
                {
                    OpenTheDoor(3, new Vector3(20, -16, 0));
                    OpenTheDoor(0, new Vector3(20, -17, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenWallB1)
                {
                    objList[21 * Database.TRUTH_DUNGEON_COLUMN + 20].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo4[21 * Database.TRUTH_DUNGEON_COLUMN + 20] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea423)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 20, 20, 22, 31);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea431)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 18, 22, 32, 36);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateC1)
                {
                    OpenTheDoor(2, new Vector3(17, -33, 0));
                    OpenTheDoor(1, new Vector3(18, -33, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonYesNoFloor4Area3Lever)
                {
                    this.nowFloor4Area3LeverNumber = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                    this.mainMessage.text = Database.Message_Floor4Area3Lever;
                    this.yesnoSystemMessage.text = Database.Message_Floor4Area3Lever;
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonYesNoFloor4Area3Lever2)
                {
                    this.nowFloor4Area3LeverNumber2 = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない。
                    this.mainMessage.text = Database.Message_Floor4Area3Lever2;
                    this.yesnoSystemMessage.text = Database.Message_Floor4Area3Lever2;
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateC3)
                {
                    OpenTheDoor(2, new Vector3(22, -39, 0));
                    OpenTheDoor(1, new Vector3(23, -39, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea432)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 23, 23, 35, 38);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateC4)
                {
                    OpenTheDoor(2, new Vector3(22, -35, 0));
                    OpenTheDoor(1, new Vector3(23, -35, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonRemovePartySC)
                {
                    Method.RemoveParty(GroundOne.SC, false);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonSystemMessage)
                {
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない
                    systemMessageText.text = this.nowMessage[this.nowReading];
                    groupSystemMessage.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenWallC1)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 22].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo4[34 * Database.TRUTH_DUNGEON_COLUMN + 22] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea433)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 23, 43, 34, 34);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea441)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 44, 48, 32, 36);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD1)
                {
                    OpenTheDoor(2, new Vector3(48, -34, 0));
                    OpenTheDoor(1, new Vector3(49, -34, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea442)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 41, 43, 35, 38);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD2)
                {
                    OpenTheDoor(2, new Vector3(47, -39, 0));
                    OpenTheDoor(1, new Vector3(48, -39, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD3)
                {
                    OpenTheDoor(2, new Vector3(50, -35, 0));
                    OpenTheDoor(1, new Vector3(51, -35, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD4)
                {
                    OpenTheDoor(2, new Vector3(50, -34, 0));
                    OpenTheDoor(1, new Vector3(51, -34, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD5)
                {
                    OpenTheDoor(2, new Vector3(48, -22, 0));
                    OpenTheDoor(1, new Vector3(49, -22, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateD6)
                {
                    OpenTheDoor(3, new Vector3(48, -31, 0));
                    OpenTheDoor(0, new Vector3(48, -32, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenWallD1)
                {
                    objList[32 * Database.TRUTH_DUNGEON_COLUMN + 46].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo4[32 * Database.TRUTH_DUNGEON_COLUMN + 46] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea443)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 46, 46, 21, 31);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4BlockWallD1)
                {
                    objList[31 * Database.TRUTH_DUNGEON_COLUMN + 46].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_19);
                    tileInfo4[31 * Database.TRUTH_DUNGEON_COLUMN + 46] = Database.TILEINFO_19;
                    objList[32 * Database.TRUTH_DUNGEON_COLUMN + 46].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_24);
                    tileInfo4[32 * Database.TRUTH_DUNGEON_COLUMN + 46] = Database.TILEINFO_24;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4InvalidateBlack)
                {
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        if ((ii == 22 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 23 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 24 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 25 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 26 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 27 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 28 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 29 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 30 * Database.TRUTH_DUNGEON_COLUMN + 46) ||
                            (ii == 31 * Database.TRUTH_DUNGEON_COLUMN + 46))
                        {
                            unknownTile[ii].SetActive(false);
                        }
                        else
                        {
                            objList[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                            unknownTile[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        }
                    }
                    for (int ii = 0; ii < this.objOther.Count; ii++)
                    {
                        this.objOther[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                    for (int ii = 0; ii < this.objBlueWallTop.Count; ii++)
                    {
                        this.objBlueWallTop[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                    for (int ii = 0; ii < this.objBlueWallBottom.Count; ii++)
                    {
                        this.objBlueWallBottom[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                    for (int ii = 0; ii < this.objBlueWallLeft.Count; ii++)
                    {
                        this.objBlueWallLeft[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                    for (int ii = 0; ii < this.objBlueWallRight.Count; ii++)
                    {
                        this.objBlueWallRight[ii].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonCallChoiceStatue)
                {
                    SceneDimension.CallTruthChoiceStatue(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonUpdateUnknownTileArea45)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 49, 52, 20, 20);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenGateE1)
                {
                    OpenTheDoor(2, new Vector3(48, -20, 0));
                    OpenTheDoor(1, new Vector3(49, -20, 0));
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor4OpenWallE1)
                {
                    objList[21 * Database.TRUTH_DUNGEON_COLUMN + 46].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_13);
                    tileInfo4[21 * Database.TRUTH_DUNGEON_COLUMN + 46] = Database.TILEINFO_13;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonGotoDungeonFive)
                {
                    GroundOne.WE.TruthCompleteArea4 = true;
                    JumpToLocation(57, -2, true);
                    SetupDungeonMapping(5);
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea1)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 44, 59, 29, 35);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea2)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 32, 43, 30, 34);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea3)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 20, 31, 31, 33);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea4)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 10, 19, 32, 32);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5TruthSelectCharacter)
                {
                    this.nowSelectCharacter = true;
                    SceneDimension.CallTruthSelectCharacter(this);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea5)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 0, 9, 27, 37);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonFloor5UnknownTileArea6)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 2, 2, 2, 3);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonGotoDownstair)
                {
                    GoToDownStair();
                }
                else if (currentEvent == MessagePack.ActionEvent.MirrorWay)
                {
                    int wayLine = GroundOne.WE2.TruthWay3_1;
                    int anotherWayLine = 1;
                    if (!GroundOne.WE2.SeekerEvent906) { wayLine = GroundOne.WE2.TruthWay3_1; anotherWayLine = 1; }
                    else if (!GroundOne.WE2.SeekerEvent907) { wayLine = GroundOne.WE2.TruthWay3_2; anotherWayLine = 2; }
                    else if (!GroundOne.WE2.SeekerEvent908) { wayLine = GroundOne.WE2.TruthWay3_3; anotherWayLine = 3; }
                    else if (!GroundOne.WE2.SeekerEvent909) { wayLine = GroundOne.WE2.TruthWay3_4; anotherWayLine = 4; }
                    else if (!GroundOne.WE2.SeekerEvent910) { wayLine = GroundOne.WE2.TruthWay3_5; anotherWayLine = 5; }
                    MirrorWay(wayLine, anotherWayLine);

                    int wayPoint = 0;
                    if (!GroundOne.WE2.SeekerEvent906) { wayPoint = 0; GroundOne.WE2.SeekerEvent906 = true; }
                    else if (!GroundOne.WE2.SeekerEvent907) { wayPoint = 1; GroundOne.WE2.SeekerEvent907 = true; }
                    else if (!GroundOne.WE2.SeekerEvent908) { wayPoint = 2; GroundOne.WE2.SeekerEvent908 = true; }
                    else if (!GroundOne.WE2.SeekerEvent909) { wayPoint = 3; GroundOne.WE2.SeekerEvent909 = true; }
                    else if (!GroundOne.WE2.SeekerEvent910) { wayPoint = 4; GroundOne.WE2.SeekerEvent910 = true; GroundOne.WE2.SeekerEvent905 = true; }

                    MirrorTruthWay(wayPoint);
                }
                else if (currentEvent == MessagePack.ActionEvent.MirrorLastWay)
                {
                    MirrorLastWay();
                }
                else if (currentEvent == MessagePack.ActionEvent.AutoMove)
                {
                    int number = Convert.ToInt32(this.nowMessage[this.nowReading]);
                    #region "AutoMove"
                    if (number == 1)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 2)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 3)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (number == 4)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 5)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 6)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 7)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 8)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 9)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (number == 10)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 11)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 12)
                    {
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 13)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (number == 14)
                    {
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);

                        // 技１
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(11);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        // 技２
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        // 技３
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        // 技４
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(302);
                        this.nowAutoMoveNumber.Add(272);
                        this.nowAutoMoveNumber.Add(242);
                        this.nowAutoMoveNumber.Add(212);
                        this.nowAutoMoveNumber.Add(182);
                        this.nowAutoMoveNumber.Add(152);
                        this.nowAutoMoveNumber.Add(122);
                        this.nowAutoMoveNumber.Add(92);
                        this.nowAutoMoveNumber.Add(62);
                        this.nowAutoMoveNumber.Add(32);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        // 技５
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(51);
                        this.nowAutoMoveNumber.Add(51);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(53);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(50);
                        this.nowAutoMoveNumber.Add(52);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        // 技看板最後
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        System.Threading.Thread.Sleep(500);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 15)
                    {
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 16)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 17)
                    {
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 18)
                    {
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 19)
                    {
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (number == 20)
                    {
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(22);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(23);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 21)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 22)
                    {
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 23)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 24)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                    }
                    else if (number == 25)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 26)
                    {
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 27)
                    {
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                    }
                    else if (number == 28)
                    {
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10001); // JumpByMirror_1_10
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10002); // JumpByMirror_1_21
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(1);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10003); // JumpByMirror_1_33
                        this.nowAutoMoveNumber.Add(2000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(10004); // JumpByMirror_1_37
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(200);
                        this.nowAutoMoveNumber.Add(200);
                        this.nowAutoMoveNumber.Add(200);
                    }
                    else if (number == 29)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(10005); // JumpByMirror_1_End();
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 30)
                    {
                        ShownEvent();
                    }
                    else if (number == 31)
                    {
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(20);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(21);
                        this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    else if (number == 32)
                    {
                        int zeroLine = 150; // 150とは鏡５６パターン内の最後の鏡である。万が一ロジック崩れがある場合は最後の鏡を原点解とする。
                        if (GroundOne.WE2.TruthWay95 == 0) { zeroLine = 95; }
                        else if (GroundOne.WE2.TruthWay96 == 0) { zeroLine = 96; }
                        else if (GroundOne.WE2.TruthWay97 == 0) { zeroLine = 97; }
                        else if (GroundOne.WE2.TruthWay98 == 0) { zeroLine = 98; }
                        else if (GroundOne.WE2.TruthWay99 == 0) { zeroLine = 99; }
                        else if (GroundOne.WE2.TruthWay100 == 0) { zeroLine = 100; }
                        else if (GroundOne.WE2.TruthWay101 == 0) { zeroLine = 101; }
                        else if (GroundOne.WE2.TruthWay102 == 0) { zeroLine = 102; }
                        else if (GroundOne.WE2.TruthWay103 == 0) { zeroLine = 103; }
                        else if (GroundOne.WE2.TruthWay104 == 0) { zeroLine = 104; }
                        else if (GroundOne.WE2.TruthWay105 == 0) { zeroLine = 105; }
                        else if (GroundOne.WE2.TruthWay106 == 0) { zeroLine = 106; }
                        else if (GroundOne.WE2.TruthWay107 == 0) { zeroLine = 107; }
                        else if (GroundOne.WE2.TruthWay108 == 0) { zeroLine = 108; }
                        else if (GroundOne.WE2.TruthWay109 == 0) { zeroLine = 109; }
                        else if (GroundOne.WE2.TruthWay110 == 0) { zeroLine = 110; }
                        else if (GroundOne.WE2.TruthWay111 == 0) { zeroLine = 111; }
                        else if (GroundOne.WE2.TruthWay112 == 0) { zeroLine = 112; }
                        else if (GroundOne.WE2.TruthWay113 == 0) { zeroLine = 113; }
                        else if (GroundOne.WE2.TruthWay114 == 0) { zeroLine = 114; }
                        else if (GroundOne.WE2.TruthWay115 == 0) { zeroLine = 115; }
                        else if (GroundOne.WE2.TruthWay116 == 0) { zeroLine = 116; }
                        else if (GroundOne.WE2.TruthWay117 == 0) { zeroLine = 117; }
                        else if (GroundOne.WE2.TruthWay118 == 0) { zeroLine = 118; }
                        else if (GroundOne.WE2.TruthWay119 == 0) { zeroLine = 119; }
                        else if (GroundOne.WE2.TruthWay120 == 0) { zeroLine = 120; }
                        else if (GroundOne.WE2.TruthWay121 == 0) { zeroLine = 121; }
                        else if (GroundOne.WE2.TruthWay122 == 0) { zeroLine = 122; }
                        else if (GroundOne.WE2.TruthWay123 == 0) { zeroLine = 123; }
                        else if (GroundOne.WE2.TruthWay124 == 0) { zeroLine = 124; }
                        else if (GroundOne.WE2.TruthWay125 == 0) { zeroLine = 125; }
                        else if (GroundOne.WE2.TruthWay126 == 0) { zeroLine = 126; }
                        else if (GroundOne.WE2.TruthWay127 == 0) { zeroLine = 127; }
                        else if (GroundOne.WE2.TruthWay128 == 0) { zeroLine = 128; }
                        else if (GroundOne.WE2.TruthWay129 == 0) { zeroLine = 129; }
                        else if (GroundOne.WE2.TruthWay130 == 0) { zeroLine = 130; }
                        else if (GroundOne.WE2.TruthWay131 == 0) { zeroLine = 131; }
                        else if (GroundOne.WE2.TruthWay132 == 0) { zeroLine = 132; }
                        else if (GroundOne.WE2.TruthWay133 == 0) { zeroLine = 133; }
                        else if (GroundOne.WE2.TruthWay134 == 0) { zeroLine = 134; }
                        else if (GroundOne.WE2.TruthWay135 == 0) { zeroLine = 135; }
                        else if (GroundOne.WE2.TruthWay136 == 0) { zeroLine = 136; }
                        else if (GroundOne.WE2.TruthWay137 == 0) { zeroLine = 137; }
                        else if (GroundOne.WE2.TruthWay138 == 0) { zeroLine = 138; }
                        else if (GroundOne.WE2.TruthWay139 == 0) { zeroLine = 139; }
                        else if (GroundOne.WE2.TruthWay140 == 0) { zeroLine = 140; }
                        else if (GroundOne.WE2.TruthWay141 == 0) { zeroLine = 141; }
                        else if (GroundOne.WE2.TruthWay142 == 0) { zeroLine = 142; }
                        else if (GroundOne.WE2.TruthWay143 == 0) { zeroLine = 143; }
                        else if (GroundOne.WE2.TruthWay144 == 0) { zeroLine = 144; }
                        else if (GroundOne.WE2.TruthWay145 == 0) { zeroLine = 145; }
                        else if (GroundOne.WE2.TruthWay146 == 0) { zeroLine = 146; }
                        else if (GroundOne.WE2.TruthWay147 == 0) { zeroLine = 147; }
                        else if (GroundOne.WE2.TruthWay148 == 0) { zeroLine = 148; }
                        else if (GroundOne.WE2.TruthWay149 == 0) { zeroLine = 149; }
                        else if (GroundOne.WE2.TruthWay150 == 0) { zeroLine = 150; }

                        MirrorWay(zeroLine, 6);
                        this.nowAutoMoveNumber.Add(10601); // JumpByMirror_Recollection3();
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                    }
                    else if (number == 33)
                    {
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(3);
                        this.nowAutoMoveNumber.Add(10602); // JumpByMirror_ZeroWay();
                    }
                    else if (number == 34)
                    {
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                    }
                    //else if (number == 35) { }
                    else if (number == 36)
                    {
                        this.nowAutoMoveNumber.Add(10006); // JumpByMirror_TurnBack();
                        this.nowAutoMoveNumber.Add(500); // sleep
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(2);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(0);
                        this.nowAutoMoveNumber.Add(1000); // sleep
                        MirrorWay(GroundOne.WE2.TruthWay3_5, 5);
                        MirrorTruthWay(4);
                        MirrorLastWay();
                    }
                    else if (number == 37)
                    {
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                        this.nowAutoMoveNumber.Add(300);
                    }
                    #endregion

                    this.nowAutoMove = true;
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonGotoDownstairFourTwo)
                {
                    GotoDownStairFourTwo();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror1)
                {
                    JumpByMirror_Hell1();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror2)
                {
                    JumpByMirror_Hell2();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror3)
                {
                    JumpByMirror_Hell3();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror4)
                {
                    JumpByMirror_Hell4();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror5)
                {
                    JumpByMirror_Hell5();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror6)
                {
                    JumpByMirror_Hell6();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror7)
                {
                    JumpByMirror_Hell7();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror8)
                {
                    JumpByMirror_Hell8();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror9)
                {
                    JumpByMirror_Hell9();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror10)
                {
                    JumpByMirror_Hell10();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror11)
                {
                    JumpByMirror_Hell11();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror12)
                {
                    JumpByMirror_Hell12();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror13)
                {
                    JumpByMirror_Hell13();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror14)
                {
                    JumpByMirror_Hell14();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror15)
                {
                    JumpByMirror_Hell15();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror16)
                {
                    JumpByMirror_Hell16();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirror16Back)
                {
                    JumpByMirror_Hell16Back();
                }
                else if (currentEvent == MessagePack.ActionEvent.JumpToLocationHellMirrorFail)
                {
                    JumpByMirror_HellFail();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileAreaHellLast)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo4, 53, 59, 18, 20);
                }
                else if (currentEvent == MessagePack.ActionEvent.DungeonGotoDownStairFiveTwo)
                {
                    GotoDownStairFiveTwo();
                }
                else if (currentEvent == MessagePack.ActionEvent.UpdateUnknownTileAreaTruthFinal)
                {
                    UpdateUnknownTileArea(GroundOne.Truth_KnownTileInfo5, 25, 35, 3, 9);
                }

                this.nowReading++;
                if (this.nowMessage[this.nowReading - 1] == "" || ForceSkipTapOK)
                {
                    tapOK();
                }
            }

            if (this.nowReading >= this.nowMessage.Count)
            {
                this.nowReading = 0;
                this.nowMessage.Clear();
                this.nowEvent.Clear();

                this.btnOK.enabled = false;
                this.btnOK.gameObject.SetActive(false);
                if (HideFilterComplete)
                {
                    this.Filter.GetComponent<Image>().color = Color.white;
                    this.Filter.SetActive(false);
                }
            }
        }

        private void agilityRoomTimer_Tick()
        {
            if (!GroundOne.WE.dungeonEvent233_Complete)
            {
                MessagePack.Message12019_fail(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE.dungeonEvent234_Complete)
            {
                MessagePack.Message12021_Fail(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE.dungeonEvent235_Complete)
            {
                MessagePack.Message12024_Fail(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (GroundOne.WE.dungeonEvent236 && !GroundOne.WE.dungeonEvent236_Complete)
            {
                // 黒タイルをまず描画
                for (int ii = 33; ii <= 35; ii++)
                {
                    int tempBase = 36;
                    if (ii == 34) { tempBase = 37; }

                    for (int jj = tempBase; jj <= 44; jj++)
                    {
                        this.objList[ii * Database.TRUTH_DUNGEON_COLUMN + jj].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_41);
                    }
                }

                // 踏み込み判定
                int CurrentTile = Method.GetTileNumber(this.Player.transform.position);
                int CurrentColumn = CurrentTile % Database.TRUTH_DUNGEON_COLUMN;
                int CurrentRow = CurrentTile / Database.TRUTH_DUNGEON_COLUMN;

                if ((CurrentRow != 34) ||
                     ((CurrentColumn == 37) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 36) && ((33 + (Area4_InnerTimerCount / 60) % 3) != 34)) ||
                     ((CurrentColumn == 38) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 37) && ((33 + (Area4_InnerTimerCount / 45) % 3) != 34)) ||
                     ((CurrentColumn == 39) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 38) && ((33 + (Area4_InnerTimerCount / 30) % 3) != 34)) ||
                     ((CurrentColumn == 40) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 39) && ((33 + (Area4_InnerTimerCount / 20) % 3) != 34)) ||
                     ((CurrentColumn == 41) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 40) && ((33 + (Area4_InnerTimerCount / 15) % 3) != 34)) ||
                     ((CurrentColumn == 42) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 41) && ((33 + (Area4_InnerTimerCount / 12) % 3) != 34)) ||
                     ((CurrentColumn == 43) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 42) && ((33 + (Area4_InnerTimerCount / 9) % 3) != 34)) ||
                     ((CurrentColumn == 44) && (this.Area4_ShadowTileNum == 34 * Database.TRUTH_DUNGEON_COLUMN + 43) && ((33 + (Area4_InnerTimerCount / 6) % 3) != 34))
                    )
                {
                    this.Area4_InnerTimerCount = 0;
                    this.Area4_ShadowTileNum = -1;

                    MessagePack.Message12027_Fail(ref nowMessage, ref nowEvent);
                    tapOK();
                    return;
                }

                // 正解タイル更新
                this.Area4_InnerTimerCount++;

                int x = 0;

                if (CurrentColumn == 36)
                {
                    if (this.Area4_InnerTimerCount > 60 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 60) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 37)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 45 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 45) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 38)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 30 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 30) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 39)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 20 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 20) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 40)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 15 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 15) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 41)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 12 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 12) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 42)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 9 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 9) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 43)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);

                    if (this.Area4_InnerTimerCount > 6 * 3) this.Area4_InnerTimerCount = 0;
                    x = 33 + (Area4_InnerTimerCount / 6) % 3;
                    objList[x * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }
                else if (CurrentColumn == 44)
                {
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 40].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 41].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 42].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 43].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                    objList[34 * Database.TRUTH_DUNGEON_COLUMN + 44].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.FloorFolder[GroundOne.WE.DungeonArea - 1] + Database.TILEINFO_40);
                }

                this.Area4_ShadowTileNum = Method.GetTileNumber(this.Player.transform.position);
                this.nowAgilityRoomCounter = 3;
            }
            else if (GroundOne.WE.dungeonEvent237 && !GroundOne.WE.dungeonEvent237_Complete)
            {
                MessagePack.Message12030_Fail(ref nowMessage, ref nowEvent);
                tapOK();
            }
        }

        private void OpenTheDoor(int direction, Vector3 pos)
        {
            int TileNumber = Method.GetTileNumber(pos);
            if (direction == 0)
            {
                this.blueWallTop[TileNumber] = false;
                for (int ii = 0; ii < this.objBlueWallTop.Count; ii++)
                {
                    if (this.objBlueWallTop[ii].name == "bluewall_" + TileNumber.ToString())
                    {
                        this.objBlueWallTop[ii].SetActive(false);
                        break;
                    }
                }
            }
            else if (direction == 1)
            {
                this.blueWallLeft[TileNumber] = false;
                for (int ii = 0; ii < this.objBlueWallLeft.Count; ii++)
                {
                    if (this.objBlueWallLeft[ii].name == "bluewall_" + TileNumber.ToString())
                    {
                        this.objBlueWallLeft[ii].SetActive(false);
                        break;
                    }
                }
            }
            else if (direction == 2)
            {
                this.blueWallRight[TileNumber] = false;
                for (int ii = 0; ii < this.objBlueWallRight.Count; ii++)
                {
                    if (this.objBlueWallRight[ii].name == "bluewall_" + TileNumber.ToString())
                    {
                        this.objBlueWallRight[ii].SetActive(false);
                        break;
                    }
                }
            }
            else if (direction == 3)
            {
                this.blueWallBottom[TileNumber] = false;
                for (int ii = 0; ii < this.objBlueWallBottom.Count; ii++)
                {
                    if (this.objBlueWallBottom[ii].name == "bluewall_" + TileNumber.ToString())
                    {
                        this.objBlueWallBottom[ii].SetActive(false);
                        break;
                    }
                }
            }
        }

        public void tapFirstMessage()
        {
            noticePanel.gameObject.SetActive(false);
            MessagePack.Message10050_2(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }
        public void tapSecondMessage()
        {
            noticePanel.gameObject.SetActive(false);
            MessagePack.Message10050_3(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }

        public void tapSystemMessageOK()
        {
            this.Filter.SetActive(false);
            groupSystemMessage.SetActive(false);
        }
        public void PointerDownArrowTop()
        {
            this.arrowUp = true;
            this.arrowDown = false;
        }
        public void PointerDownArrowBottom()
        {
            this.arrowUp = false;
            this.arrowDown = true;
        }
        public void PointerDownArrowLeft()
        {
            this.arrowLeft = true;
            this.arrowRight = false;
        }
        public void PointerDownArrowRight()
        {
            this.arrowLeft = false;
            this.arrowRight = true;
        }
        
        public void CallHomeTown()
        {
            CancelKeyDownMovement();

            GroundOne.BattleSpeed = this.battleSpeed;
            GroundOne.Difficulty = this.difficulty;
            SceneDimension.JumpToTruthHomeTown();
        }

        private void SetupDungeonMapping(int area)
        {
            GroundOne.WE.DungeonArea = area;
            this.dungeonAreaLabel.text = GroundOne.WE.DungeonArea.ToString() + "　階";
            this.dayLabel.text = GroundOne.WE.GameDay.ToString() + "日目";
            Application.UnloadLevel(Database.TruthDungeon);
            // 全情報クリア、全情報再読み込みを行わず、Sceneの再ロードで実行したい。
            SceneDimension.JumpToTruthDungeon(true);
        }
    }
}

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
    public class SingleDungeon : MotherForm
    {
        public GameObject canvasDungeon;
        public GameObject canvasBattle;

        #region "Dungeon"
        int nowReading = 0;
        List<string> nowMessage = new List<string>();
        List<MessagePack.ActionEvent> nowEvent = new List<MessagePack.ActionEvent>();

        private bool DungeonViewMode = false; // ダンジョンマップの全体を見たいときに使うフラグ
        private Vector2 DungeonViewModeMasterLocation = new Vector2(); // ダンジョン全体マップView表示時の元のViewの位置
        private Vector2 DungeonViewModeMasterPlayerLocation = new Vector2(); //  ダンジョン全体マップView表示時のプレイヤーの元のViewの位置
        private int MovementInterval = 0; // ダンジョンマップ全体を見ている時のインターバル

        // prefab
        public GameObject[] TILEINFO_1;
        public GameObject[] TILEINFO_2;
        public GameObject[] TILEINFO_3;
        public GameObject[] TILEINFO_4;
        public GameObject[] TILEINFO_5;
        public GameObject[] TILEINFO_6;
        public GameObject[] TILEINFO_7;
        public GameObject[] TILEINFO_8;
        public GameObject[] TILEINFO_9;
        public GameObject TILEINFO_9_2;
        public GameObject[] TILEINFO_10;
        public GameObject[] TILEINFO_10_2;
        public GameObject[] TILEINFO_11;
        public GameObject[] TILEINFO_12;
        public GameObject[] TILEINFO_13;
        public GameObject[] TILEINFO_14;
        public GameObject[] TILEINFO_15;
        public GameObject[] TILEINFO_16;
        public GameObject[] TILEINFO_17;
        public GameObject[] TILEINFO_18;
        public GameObject[] TILEINFO_19;
        public GameObject[] TILEINFO_20;
        public GameObject[] TILEINFO_21;
        public GameObject[] TILEINFO_22;
        public GameObject[] TILEINFO_23;
        public GameObject[] TILEINFO_24;
        public GameObject[] TILEINFO_25;
        public GameObject[] TILEINFO_26;
        public GameObject[] TILEINFO_27;
        public GameObject[] TILEINFO_28;
        public GameObject[] TILEINFO_29;
        public GameObject[] TILEINFO_30;
        public GameObject[] TILEINFO_31;
        public GameObject[] TILEINFO_32;
        public GameObject[] TILEINFO_33;
        public GameObject[] TILEINFO_34;
        public GameObject[] TILEINFO_35;
        public GameObject[] TILEINFO_36;
        public GameObject[] TILEINFO_37;
        public GameObject[] TILEINFO_38;
        public GameObject[] TILEINFO_39;
        public GameObject[] TILEINFO_40;
        public GameObject[] TILEINFO_41;
        public GameObject[] TILEINFO_42;
        public GameObject[] TILEINFO_43;
        public GameObject[] TILEINFO_44;
        public GameObject TILEINFO_45;
        public GameObject OBJ_FOUNTAIN;
        public GameObject OBJ_BLUEWALL_T;
        public GameObject OBJ_BLUEWALL_L;
        public GameObject OBJ_BLUEWALL_R;
        public GameObject OBJ_BLUEWALL_B;

        // GUI
        public GameObject closeFilter;
        public GameObject groupResult;
        public Text txtResult;
        public GameObject BlackoutFilter;
        public Text txtTitle;
        public GameObject groupDayLabel;
        public Text dayLabel;
        public Text dungeonAreaLabel;
        public GameObject groupSystemMessage;
        public Text systemMessageText;
        public GameObject groupArrow;
        public GameObject GroupMenu;
        public GameObject btnStatus;
        public GameObject btnBattleSetting;
        public GameObject btnSave;
        public GameObject btnLoad;
        public GameObject btnExit;
        public GameObject GroupsubMenu;
        public GameObject HelpManual;
        public GameObject DungeonView;
        public GameObject BlueOrbImage;
        public GameObject BlueOrbText;
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
        public Text txtMenuStatus;
        public Text txtMenuBattleSetting;
        public Text txtMenuSave;
        public Text txtMenuLoad;
        public Text txtMenuExit;
        public GameObject panelObjective;
        public GameObject panelObjectiveTitle;
        public Text txtObjectiveTitle;
        public GameObject panelGroupQuest;
        public List<GameObject> panelObjectiveTxt;
        public List<Text> txtObjective;

        // initialize data list
        List<GameObject> objList = new List<GameObject>();
        List<GameObject> unknownTile = new List<GameObject>();
        List<GameObject> objTreasure = new List<GameObject>();
        Dictionary<int, bool> treasureOpen = new Dictionary<int, bool>();
        List<GameObject> objUpstair = new List<GameObject>();
        List<GameObject> objDownstair = new List<GameObject>();
        List<GameObject> objBlueOrb = new List<GameObject>();
        List<GameObject> objFountain = new List<GameObject>();
        List<GameObject> objMonster = new List<GameObject>();

        public GameObject groupMainMessage;
        public Text mainMessage;
        public Button btnOK;
        public Button btnYes;
        public Button btnNo;

        // 到達・未到達領域を示すためのタイル情報
        string[] tileInfo = null;

        GameObject Player = null;
        Vector3 viewPoint = new Vector3(); // ダンジョンビュー位置

        // 敵の強さを区分けするためのタイルカラー情報
        int[] tileColor = new int[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        // ダンジョンマップの基本タイル情報
        private GameObject prefab_TileElement = null;

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

        private int stepCounter = 0; // 敵エンカウント率調整の値

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                txtMenuStatus.text = Database.GUI_MENU_STATUS;
                txtMenuBattleSetting.text = Database.GUI_MENU_BATTLESETTING;
                txtMenuSave.text = Database.GUI_MENU_SAVE;
                txtMenuLoad.text = Database.GUI_MENU_LOAD;
                txtMenuExit.text = Database.GUI_MENU_EXIT;
                txtObjectiveTitle.text = Database.GUI_OBJECTIVE;
            }

            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                MOVE_INTERVAL = 10;
                this.groupArrow.SetActive(true);
            }
            else
            {
                MOVE_INTERVAL = 40;
                this.groupArrow.SetActive(true);
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

            this.prefab_TileElement = new GameObject();
            this.prefab_TileElement.AddComponent<SpriteRenderer>();
            this.prefab_TileElement.transform.position = new Vector3(-99999, -99999, 0);
            this.prefab_TileElement.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.PLAYER_MARK);
            this.Player = Instantiate(this.prefab_TileElement, new Vector3(0, 0, -3), Quaternion.identity) as GameObject;

            ReadDungeonTileFromXmlFile(@"DungeonMapping_S_B_1"); // single-todo

            // 始めて開始する場合、あらかじめスタート地点を設定。
            if ((GroundOne.WE.DungeonPosX == 0) && (GroundOne.WE.DungeonPosY == 0))
            {
                switch (GroundOne.WE.DungeonArea)
                {
                    case 0:
                    case 1:
                        JumpToLocation(5, -20, true);
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
                Method.CopyShadowToMain();
                this.ignoreCreateShadow = true;
                this.nowEncountEnemy = true;
            }
            #endregion
            #region "敗北して、ゲーム終了を選択した時"
            else if (GroundOne.BattleResult == GroundOne.battleResult.Ignore)
            {
                Method.CopyShadowToMain();
                GroundOne.BattleResult = GroundOne.battleResult.None;
                if (GroundOne.enemyName1 == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, true);
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
            }
            #endregion
            #region "戦闘に勝利した場合（通常ルート）"
            else if (GroundOne.BattleResult == GroundOne.battleResult.OK)
            {
                GroundOne.BattleResult = GroundOne.battleResult.None;
                GroundOne.DuelMode = false;
                GroundOne.HiSpeedAnimation = false;
                GroundOne.FinalBattle = false;
                GroundOne.LifeCountBattle = false;

                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
            }
            #endregion
            #endregion
        }

        private GameObject SelectObjectFromString(string src)
        {
            int area = 0;
            if (GroundOne.WE.DungeonArea == 0) { area = 0; }
            else if (GroundOne.WE.DungeonArea == 1) { area = 0; }

            if (src == Database.TILEINFO_1) { return TILEINFO_1[area]; }
            if (src == Database.TILEINFO_2) { return TILEINFO_2[area]; }
            if (src == Database.TILEINFO_3) { return TILEINFO_3[area]; }
            if (src == Database.TILEINFO_4) { return TILEINFO_4[area]; }
            if (src == Database.TILEINFO_5) { return TILEINFO_5[area]; }
            if (src == Database.TILEINFO_6) { return TILEINFO_6[area]; }
            if (src == Database.TILEINFO_7) { return TILEINFO_7[area]; }
            if (src == Database.TILEINFO_8) { return TILEINFO_8[area]; }
            if (src == Database.TILEINFO_9) { return TILEINFO_9[area]; }
            if (src == Database.TILEINFO_10) { return TILEINFO_10[area]; }
            if (src == Database.TILEINFO_10_2) { return TILEINFO_10_2[area]; }
            if (src == Database.TILEINFO_11) { return TILEINFO_11[area]; }
            if (src == Database.TILEINFO_12) { return TILEINFO_12[area]; }
            if (src == Database.TILEINFO_13) { return TILEINFO_13[area]; }
            if (src == Database.TILEINFO_14) { return TILEINFO_14[area]; }
            if (src == Database.TILEINFO_15) { return TILEINFO_15[area]; }
            if (src == Database.TILEINFO_16) { return TILEINFO_16[area]; }
            if (src == Database.TILEINFO_17) { return TILEINFO_17[area]; }
            if (src == Database.TILEINFO_18) { return TILEINFO_18[area]; }
            if (src == Database.TILEINFO_19) { return TILEINFO_19[area]; }
            if (src == Database.TILEINFO_20) { return TILEINFO_20[area]; }
            if (src == Database.TILEINFO_21) { return TILEINFO_21[area]; }
            if (src == Database.TILEINFO_22) { return TILEINFO_22[area]; }
            if (src == Database.TILEINFO_23) { return TILEINFO_23[area]; }
            if (src == Database.TILEINFO_24) { return TILEINFO_24[area]; }
            if (src == Database.TILEINFO_25) { return TILEINFO_25[area]; }
            if (src == Database.TILEINFO_26) { return TILEINFO_26[area]; }
            if (src == Database.TILEINFO_27) { return TILEINFO_27[area]; }
            if (src == Database.TILEINFO_28) { return TILEINFO_28[area]; }
            if (src == Database.TILEINFO_29) { return TILEINFO_29[area]; }
            if (src == Database.TILEINFO_30) { return TILEINFO_30[area]; }
            if (src == Database.TILEINFO_31) { return TILEINFO_31[area]; }
            if (src == Database.TILEINFO_32) { return TILEINFO_32[area]; }
            if (src == Database.TILEINFO_33) { return TILEINFO_33[area]; }
            if (src == Database.TILEINFO_34) { return TILEINFO_34[area]; }
            if (src == Database.TILEINFO_35) { return TILEINFO_35[area]; }
            if (src == Database.TILEINFO_36) { return TILEINFO_36[area]; }
            if (src == Database.TILEINFO_37) { return TILEINFO_37[area]; }
            if (src == Database.TILEINFO_38) { return TILEINFO_38[area]; }
            if (src == Database.TILEINFO_39) { return TILEINFO_39[area]; }
            if (src == Database.TILEINFO_40) { return TILEINFO_40[area]; }
            if (src == Database.TILEINFO_41) { return TILEINFO_41[area]; }
            if (src == Database.TILEINFO_42) { return TILEINFO_42[area]; }
            if (src == Database.TILEINFO_43) { return TILEINFO_43[area]; }
            if (src == Database.TILEINFO_44) { return TILEINFO_44[area]; }
            if (src == Database.TILEINFO_45) { return TILEINFO_45; }
            if (src == Database.TREASURE_BOX) { return TILEINFO_9[area]; }
            if (src == Database.TREASURE_BOX_OPEN) { return TILEINFO_9_2; }
            if (src == Database.BOARD) { return TILEINFO_12[area]; }
            if (src == Database.UPSTAIR) { return TILEINFO_11[area]; }
            if (src == Database.DOWNSTAIR) { return TILEINFO_1[area]; }
            if (src == Database.MIRROR) { return TILEINFO_43[area]; }
            if (src == Database.BLUEORB) { return TILEINFO_44[area]; }
            if (src == Database.FOUNTAIN) { return OBJ_FOUNTAIN; }
            if (src == Database.BLUE_WALL_T) { return OBJ_BLUEWALL_T; }
            if (src == Database.BLUE_WALL_L) { return OBJ_BLUEWALL_L; }
            if (src == Database.BLUE_WALL_R) { return OBJ_BLUEWALL_R; }
            if (src == Database.BLUE_WALL_B) { return OBJ_BLUEWALL_B; }
            return null;
        }

        private void ReadDungeonTileFromXmlFile(string xmlFileName)
        {
            // 未探索タイル設置
            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                this.unknownTile.Add(Instantiate(SelectObjectFromString(Database.TILEINFO_10), new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), -2), Quaternion.identity) as GameObject);
            }

            #region "宝箱や看板などの設置"
            try
            {
                string OTHER1 = "TreasureInfo";
                string OTHER3 = "UpstairInfo";
                string OTHER4 = "DownstairInfo";
                string OTHER10 = "BlueOrbInfo";
                string OTHER11 = "FountainInfo";
                string OTHER12 = "EnemyInfo";
                string[] contents = Database.Content1_1; // single-todo

                for (int ii = 0; ii < contents.Length; ii++)
                {
                    if (contents[ii].Contains(OTHER1))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER1.Length, contents[ii].Length - OTHER1.Length));
                        this.objTreasure.Add(Instantiate(SelectObjectFromString(Database.TREASURE_BOX), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                        this.treasureOpen.Add(targetNumber, false);
                    }
                    else if (contents[ii].Contains(OTHER3))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER3.Length, contents[ii].Length - OTHER3.Length));
                        this.objUpstair.Add(Instantiate(SelectObjectFromString(Database.UPSTAIR), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                    }
                    else if (contents[ii].Contains(OTHER4))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER4.Length, contents[ii].Length - OTHER4.Length));
                        this.objDownstair.Add(Instantiate(SelectObjectFromString(Database.DOWNSTAIR), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                    }
                    else if (contents[ii].Contains(OTHER10))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER10.Length, contents[ii].Length - OTHER10.Length));
                        this.objBlueOrb.Add(Instantiate(SelectObjectFromString(Database.BLUEORB), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                    }
                    else if (contents[ii].Contains(OTHER11))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER11.Length, contents[ii].Length - OTHER11.Length));
                        this.objFountain.Add(Instantiate(SelectObjectFromString(Database.FOUNTAIN), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                    }
                    else if (contents[ii].Contains(OTHER12))
                    {
                        int targetNumber = Convert.ToInt32(contents[ii].Substring(OTHER12.Length, contents[ii].Length - OTHER12.Length));
                        this.objMonster.Add(Instantiate(SelectObjectFromString(Database.MARK_ENEMY), new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), -1), Quaternion.identity) as GameObject);
                    }
                }
            }
            catch { }
            #endregion
            #region "ダンジョンタイルをファイル名→プレハブ変換"
            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                this.tileColor[ii] = 1;
                string current = "Tile1";
                int quot = ii / Database.TRUTH_DUNGEON_COLUMN;

                if (GroundOne.WE.DungeonArea == 1)
                {
                    if (quot == 0) { current = Database.Tile1_1_00[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_00[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 1) { current = Database.Tile1_1_01[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_01[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 2) { current = Database.Tile1_1_02[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_02[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 3) { current = Database.Tile1_1_03[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_03[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 4) { current = Database.Tile1_1_04[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_04[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 5) { current = Database.Tile1_1_05[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_05[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 6) { current = Database.Tile1_1_06[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_06[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 7) { current = Database.Tile1_1_07[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_07[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 8) { current = Database.Tile1_1_08[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_08[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 9) { current = Database.Tile1_1_09[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_09[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 10) { current = Database.Tile1_1_10[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_10[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 11) { current = Database.Tile1_1_11[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_11[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 12) { current = Database.Tile1_1_12[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_12[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 13) { current = Database.Tile1_1_13[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_13[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 14) { current = Database.Tile1_1_14[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_14[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 15) { current = Database.Tile1_1_15[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_15[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 16) { current = Database.Tile1_1_16[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_16[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 17) { current = Database.Tile1_1_17[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_17[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 18) { current = Database.Tile1_1_18[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_18[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 19) { current = Database.Tile1_1_19[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_19[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 20) { current = Database.Tile1_1_20[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_20[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 21) { current = Database.Tile1_1_21[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_21[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 22) { current = Database.Tile1_1_22[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_22[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 23) { current = Database.Tile1_1_23[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_23[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 24) { current = Database.Tile1_1_24[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_24[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 25) { current = Database.Tile1_1_25[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_25[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 26) { current = Database.Tile1_1_26[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_26[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 27) { current = Database.Tile1_1_27[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_27[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 28) { current = Database.Tile1_1_28[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_28[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 29) { current = Database.Tile1_1_29[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_29[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 30) { current = Database.Tile1_1_30[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_30[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 31) { current = Database.Tile1_1_31[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_31[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 32) { current = Database.Tile1_1_32[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_32[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 33) { current = Database.Tile1_1_33[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_33[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 34) { current = Database.Tile1_1_34[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_34[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 35) { current = Database.Tile1_1_35[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_35[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 36) { current = Database.Tile1_1_36[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_36[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 37) { current = Database.Tile1_1_37[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_37[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 38) { current = Database.Tile1_1_38[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_38[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                    else if (quot == 39) { current = Database.Tile1_1_39[ii % Database.TRUTH_DUNGEON_COLUMN]; this.tileColor[ii] = Database.Area1_1_39[ii % Database.TRUTH_DUNGEON_COLUMN]; }
                }

                this.objList.Add(Instantiate(SelectObjectFromString(current), new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);

                // unknownTileとTruth_KnownTileInfoはネームが反対ですが、意味付けは同じ本質です。
                if ((GroundOne.WE.DungeonArea == 1) || (GroundOne.WE.DungeonArea == 0))
                {
                    unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo[ii]);
                    tileInfo[ii] = current;
                }
            }
            #endregion
        }

        public override void Update()
        {
            base.Update();

            if (canvasBattle.activeInHierarchy)
            {
                TempUpdate();
                return;
            }

            if (canvasDungeon.activeInHierarchy)
            {
                if (this.nowEncountEnemy)
                {
                    this.nowEncountEnemy = false;
                    mainMessage.text = "敵と遭遇！";
                    AppearMainMessage();
                    CancelKeyDownMovement();
                    this.execEncountEnemy = true;
                }
                else if (this.execEncountEnemy)
                {
                    this.execEncountEnemy = false;
                    PrepareCallTruthBattleEnemy();
                }
                else if (Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.UpArrow) ||
                        Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.LeftArrow) ||
                        Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.RightArrow) ||
                        Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.DownArrow))
                {
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
        }
        
        public override void SceneBack()
        {
            base.SceneBack();

            mainMessage.text = "";
            UpdateMainMessage("", true);
            SetupPlayerStatus(false);

            if (GroundOne.LevelUpRoutine)
            {
                GroundOne.LevelUpRoutine = false;
                return;
            }
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
        public void PointerUpArrow()
        {
            CancelKeyDownMovement();
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

            UpdatePlayerLocationInfo(X, Y, noSound);
            this.viewPoint = new Vector3(viewX + Database.CAMERA_WORLD_POINT_X, viewY + Database.CAMERA_WORLD_POINT_Y, Camera.main.transform.position.z);
            UpdateViewPoint(this.viewPoint.x, this.viewPoint.y);

            HarfAppearMainMessage();
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

            // プレイヤーの位置に対応しているタイル情報を取得する。
            // タイル情報にある壁情報を取得して
            // 壁情報とプレイヤー動作方向に対して壁情報が一致する場合
            string WallHitMessage = "";
            string current = targetTileInfo[Method.GetTileNumber(Player.transform.position)];
            if ((current == Database.TILEINFO_24 && direction == 0) ||
                (current == Database.TILEINFO_16 && direction == 1) ||
                (current == Database.TILEINFO_21 && direction == 2) ||
                (current == Database.TILEINFO_14 && direction == 3) ||
                (current == Database.TILEINFO_20 && direction == 2) ||
                (current == Database.TILEINFO_42 && direction == 2) ||
                (current == Database.TILEINFO_26 && (direction == 0 || direction == 1)) ||
                (current == Database.TILEINFO_30 && (direction == 0 || direction == 2)) ||
                (current == Database.TILEINFO_25 && (direction == 0 || direction == 3)) ||
                (current == Database.TILEINFO_18 && (direction == 1 || direction == 2)) ||
                (current == Database.TILEINFO_17 && (direction == 1 || direction == 3)) ||
                (current == Database.TILEINFO_22 && (direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_28 && (direction == 0 || direction == 1 || direction == 2)) ||
                (current == Database.TILEINFO_27 && (direction == 0 || direction == 1 || direction == 3)) ||
                (current == Database.TILEINFO_31 && (direction == 0 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_19 && (direction == 1 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_29 && (direction == 0 || direction == 1 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_6 && (direction == 1 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_7 && (direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_8 && (direction == 0 || direction == 1 || direction == 2)) ||
                (current == Database.TILEINFO_5 && (direction == 0 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_3 && (direction == 0)) ||
                (current == Database.TILEINFO_2 && (direction == 1 || direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_4 && (direction == 0 || direction == 1 || direction == 3)) ||
                (current == Database.TILEINFO_32 && (direction == 0)) ||
                (current == Database.TILEINFO_33 && (direction == 1)) ||
                (current == Database.TILEINFO_35 && (direction == 1 || direction == 3)) ||
                (current == Database.TILEINFO_37 && (direction == 2 || direction == 3)) ||
                (current == Database.TILEINFO_38 && (direction == 0))
                )
            {
                WallHit(WallHitMessage);
                return true;
            }

            UpdateMainMessage("", true, true);
            return false;
        }

        private void WallHit(string message)
        {
            CancelKeyDownMovement();
            GroundOne.PlaySoundEffect(Database.SOUND_WALL_HIT);
            this.mainMessage.text = message;
            AppearMainMessage();
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
                    //System.Threading.Thread.Sleep(100);
                    //debug.text += "check wall end.";
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
                    if (ii == 0) { player = GroundOne.P1; targetLabel = currentSkillPoint1; targetText = currentSkillValue1; }
                    else if (ii == 1) { player = GroundOne.P2; targetLabel = currentSkillPoint2; targetText = currentSkillValue2; }
                    else if (ii == 2) { player = GroundOne.P3; targetLabel = currentSkillPoint3; targetText = currentSkillValue3; }
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

        private void SearchSomeEvents()
        {
            int number = Method.GetTileNumber(Player.transform.localPosition);

            // 上り階段チェック
            for (int ii = 0; ii < objUpstair.Count; ii++)
            {
                if (number == Method.GetTileNumber(objUpstair[ii].transform.localPosition))
                {
                    // single-todo
                    mainMessage.text = "上り階段を発見";
                    SceneDimension.JumpToSingleHomeTown();
                    return;
                }
            }

            // 下り階段チェック
            for (int ii = 0; ii < objDownstair.Count; ii++)
            {
                if (number == Method.GetTileNumber(objDownstair[ii].transform.localPosition))
                {
                    // single-todo
                    mainMessage.text = "下り階段を発見";
                    txtResult.text = "";
                    groupResult.SetActive(true);
                    closeFilter.SetActive(true);
                    return;
                }
            }

            // 宝箱チェック
            for (int ii = 0; ii < objTreasure.Count; ii++)
            {
                if (number == Method.GetTileNumber(objTreasure[ii].transform.localPosition))
                {
                    if (treasureOpen[number] == false)
                    {
                        if (GetTreasure(Database.COMMON_FINE_SWORD)) // single-todo
                        {
                            treasureOpen[number] = true;
                            this.objTreasure[ii].GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(Database.TREASURE_BOX_OPEN);
                        }
                    }
                    return;
                }
            }

            // 青水晶フラグメント
            for (int ii = 0; ii < objBlueOrb.Count; ii++)
            {
                if (number == Method.GetTileNumber(objBlueOrb[ii].transform.localPosition))
                {
                    // single-todo
                    mainMessage.text = "フラグメントを取得しました。";
                    return;
                }
            }

            // 回復の泉チェック
            for (int ii = 0; ii < objFountain.Count; ii++)
            {
                if (number == Method.GetTileNumber(objFountain[ii].transform.localPosition))
                {
                    // single-todo
                    mainMessage.text = "回復した";
                    RefreshWater();
                    return;
                }                
            }

            // ボス存在チェック
            for (int ii = 0; ii < objMonster.Count; ii++)
            {
                if (number == Method.GetTileNumber(objMonster[ii].transform.localPosition))
                {
                    // single-todo
                    mainMessage.text = "ボスとの戦闘だ";
                    GroundOne.enemyName1 = Database.ENEMY_MANDRAGORA;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;

                    Method.CreateShadowData();
                    CancelKeyDownMovement();

                    GroundOne.DuelMode = false;
                    GroundOne.HiSpeedAnimation = false;
                    GroundOne.FinalBattle = false;
                    GroundOne.LifeCountBattle = false;
                    GroundOne.BattleResult = GroundOne.battleResult.None;
                    GroundOne.StopDungeonMusic();

                    TempStart();
                    TempUpdate();
                    canvasDungeon.SetActive(false);
                    canvasBattle.SetActive(true);
                    GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
                    //SceneDimension.CallTruthBattleEnemy(Database.TruthDungeon, false, false, false, false);
                    return;
                }
            }

            EncountEnemy();
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
                 currentTileInfo != Database.TILEINFO_38))
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
                 currentTileInfo != Database.TILEINFO_35))
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
                 currentTileInfo != Database.TILEINFO_37))
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
                 currentTileInfo != Database.TILEINFO_37))
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

        private void SetupEnemyName(string enemy1, string enemy2, string enemy3)
        {
            GroundOne.enemyName1 = enemy1;
            GroundOne.enemyName2 = enemy2;
            GroundOne.enemyName3 = enemy3;
        }

        private void EncountEnemy()
        {
            return;

            System.Random rd = new System.Random(DateTime.Now.Millisecond * Environment.TickCount);
            int resultValue = rd.Next(1, 101);
            if (GroundOne.WE.CompleteSlayBoss5) resultValue = 100;

            stepCounter += 2;
            int encountBorder = 0;
            encountBorder = (int)(stepCounter / 40);
            int lowLevelBorder = 1;
            if (GroundOne.WE2.RealWorld == false)
            {
                int[] factor = { Database.CHARACTER_MAX_LEVEL1, Database.CHARACTER_MAX_LEVEL2, Database.CHARACTER_MAX_LEVEL3, Database.CHARACTER_MAX_LEVEL4, Database.CHARACTER_MAX_LEVEL5 };
                lowLevelBorder = (factor[GroundOne.WE.DungeonArea - 1] - GroundOne.P1.Level) / 4 + 1;
            }
            else
            {
                lowLevelBorder = Database.CHARACTER_MAX_LEVEL6 - GroundOne.P1.Level + 1;
            }
            if (resultValue > encountBorder * lowLevelBorder)
            {
                return;
            }

            stepCounter = 0;
            int enemyLevel = tileColor[Method.GetTileNumber(this.Player.transform.position)];
            // １階は左上：エリア１、左下：エリア２、右上：エリア３、右下：エリア４
            if (GroundOne.WE.DungeonArea == 1)
            {
                if (enemyLevel == 1)
                {
                    string[] monsterName = { Database.ENEMY_HIYOWA_BEATLE, Database.ENEMY_HENSYOKU_PLANT,
                                             Database.ENEMY_GREEN_CHILD,   Database.ENEMY_TINY_MANTIS,
                                             Database.ENEMY_KOUKAKU_WURM,  Database.ENEMY_MANDRAGORA };

                    if (GroundOne.P1.Level <= 1)
                    {
                        SetupEnemyName(monsterName[AP.Math.RandomInteger(2)], String.Empty, String.Empty);
                    }
                    else
                    {
                        int result = AP.Math.RandomInteger(6);
                        if (result == 0)
                        {
                            SetupEnemyName(monsterName[0], monsterName[0], String.Empty);
                        }
                        else if (result == 1)
                        {
                            SetupEnemyName(monsterName[1], monsterName[1], String.Empty);
                        }
                        else if (result == 2)
                        {
                            SetupEnemyName(monsterName[2], monsterName[0], String.Empty);
                        }
                        else if (result == 3)
                        {
                            SetupEnemyName(monsterName[3], monsterName[1], String.Empty);
                        }
                        else if (result == 4)
                        {
                            if (GroundOne.P1.Level <= 3)
                            {
                                SetupEnemyName(monsterName[1], monsterName[0], String.Empty);
                            }
                            else
                            {
                                SetupEnemyName(monsterName[4], monsterName[0], String.Empty);
                            }
                        }
                        else if (result == 5)
                        {
                            if (GroundOne.P1.Level <= 5)
                            {
                                SetupEnemyName(monsterName[0], monsterName[1], String.Empty);
                            }
                            else
                            {
                                SetupEnemyName(monsterName[5], monsterName[1], String.Empty);
                            }
                        }
                    }
                }
            }

            // 敵２、敵３を生成するかどうかの判定用オブジェクトを生成
            GameObject enemyObj1 = new GameObject("enemy1");
            TruthEnemyCharacter ec1 = enemyObj1.AddComponent<TruthEnemyCharacter>();
            ec1.Initialize(GroundOne.enemyName1);

            if (GroundOne.WE.AvailableSecondCharacter == false)
            {
                GroundOne.enemyName2 = String.Empty;
            }
            if (GroundOne.WE.AvailableThirdCharacter == false)
            {
                GroundOne.enemyName3 = String.Empty;
            }

            Destroy(ec1);

            this.nowEncountEnemy = true;
        }

        private bool GetTreasure(string targetItemName)
        {
            nowMessage.Add("宝箱を発見！"); nowEvent.Add(MessagePack.ActionEvent.None);

            ItemBackPack backpackData = new ItemBackPack(targetItemName);
            bool result1 = GroundOne.P1.AddBackPack(backpackData);
            if (result1)
            {
                Debug.Log("itemget 1");
                nowMessage.Add("『" + backpackData.Name + "を手に入れました』"); nowEvent.Add(MessagePack.ActionEvent.None);
                tapOK();
                return true;
            }

            nowMessage.Add("荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。"); nowEvent.Add(MessagePack.ActionEvent.None);
            tapOK();
            Debug.Log("itemget fail");
            return false;
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
                FirstPlayerPanel.GetComponent<Image>().color = GroundOne.P1.DungeonPanelColor;

                FirstPlayerName.text = GroundOne.P1.FullName;
                currentSkillPoint1.gameObject.SetActive(GroundOne.P1.AvailableSkill);
                currentSkillValue1.gameObject.SetActive(GroundOne.P1.AvailableSkill);
                currentManaPoint1.gameObject.SetActive(GroundOne.P1.AvailableMana);
                currentManaValue1.gameObject.SetActive(GroundOne.P1.AvailableMana);

                currentLife1.color = Color.red;
                currentManaPoint1.color = UnityColor.ManaColor;
                currentSkillPoint1.color = UnityColor.SkillColor;

                UpdateLife(GroundOne.P1, currentLife1, currentLifeValue1);
                UpdateSkill(GroundOne.P1, currentSkillPoint1, currentSkillValue1);
                UpdateMana(GroundOne.P1, currentManaPoint1, currentManaValue1);
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
                SecondPlayerPanel.GetComponent<Image>().color = GroundOne.P2.DungeonPanelColor;

                SecondPlayerName.text = GroundOne.P2.FullName;
                currentSkillPoint2.gameObject.SetActive(GroundOne.P2.AvailableSkill);
                currentSkillValue2.gameObject.SetActive(GroundOne.P2.AvailableSkill);
                currentManaPoint2.gameObject.SetActive(GroundOne.P2.AvailableMana);
                currentManaValue2.gameObject.SetActive(GroundOne.P2.AvailableMana);

                currentLife2.color = Color.red;
                currentManaPoint2.color = UnityColor.ManaColor;
                currentSkillPoint2.color = UnityColor.SkillColor;

                UpdateLife(GroundOne.P2, currentLife2, currentLifeValue2);
                UpdateMana(GroundOne.P2, currentManaPoint2, currentManaValue2);
                UpdateSkill(GroundOne.P2, currentSkillPoint2, currentSkillValue2);
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
                ThirdPlayerPanel.GetComponent<Image>().color = GroundOne.P3.DungeonPanelColor;

                ThirdPlayerName.text = GroundOne.P3.FullName;
                currentSkillPoint3.gameObject.SetActive(GroundOne.P3.AvailableSkill);
                currentSkillValue3.gameObject.SetActive(GroundOne.P3.AvailableSkill);
                currentManaPoint3.gameObject.SetActive(GroundOne.P3.AvailableMana);
                currentManaValue3.gameObject.SetActive(GroundOne.P3.AvailableMana);

                currentLife3.color = Color.red;
                currentManaPoint3.color = UnityColor.ManaColor;
                currentSkillPoint3.color = UnityColor.SkillColor;

                UpdateLife(GroundOne.P3, currentLife3, currentLifeValue3);
                UpdateMana(GroundOne.P3, currentManaPoint3, currentManaValue3);
                UpdateSkill(GroundOne.P3, currentSkillPoint3, currentSkillValue3);
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

        private void RefreshWater()
        {
            if (GroundOne.P1 != null) { GroundOne.P1.ResurrectPlayer(GroundOne.P1.MaxLife); GroundOne.P1.MaxGain(); }
            if (GroundOne.P2 != null) { GroundOne.P2.ResurrectPlayer(GroundOne.P2.MaxLife); GroundOne.P2.MaxGain(); }
            if (GroundOne.P3 != null) { GroundOne.P3.ResurrectPlayer(GroundOne.P3.MaxLife); GroundOne.P3.MaxGain(); }
            SetupPlayerStatus();
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

            if (!ignoreOk)
            {
                tapOK();
            }
        }

        public void tapStatus()
        {
            SceneDimension.CallTruthStatusPlayer(this, false, string.Empty, string.Empty);
        }
        public void tapBattleSetting()
        {
            SceneDimension.CallTruthBattleSetting(this);
        }

        public override void BookManual_Click()
        {
            base.BookManual_Click();
        }

        public void DungeonView_Click()
        {
            this.DungeonViewMode = !this.DungeonViewMode;
            if (this.DungeonViewMode)
            {
                this.DungeonViewModeMasterLocation = new Vector2(this.viewPoint.x, this.viewPoint.y);
                this.DungeonViewModeMasterPlayerLocation = new Vector2(this.Player.transform.position.x, this.Player.transform.position.y);

                this.GroupMenu.SetActive(false);
                this.groupPlayerList.SetActive(false);
                this.HelpManual.SetActive(false);
                this.BlueOrbImage.SetActive(false);
                this.MovementInterval = 0;
            }
            else
            {
                this.MovementInterval = MOVE_INTERVAL;

                this.viewPoint = new Vector2(this.DungeonViewModeMasterLocation.x, this.DungeonViewModeMasterLocation.y);
                this.Player.transform.position = new Vector3(this.DungeonViewModeMasterPlayerLocation.x, this.DungeonViewModeMasterPlayerLocation.y, this.Player.transform.position.z);

                this.GroupMenu.SetActive(true);
                this.groupPlayerList.SetActive(true);
                this.HelpManual.SetActive(true);
                this.BlueOrbImage.SetActive(true);
                UpdateViewPoint(this.DungeonViewModeMasterLocation.x, this.DungeonViewModeMasterLocation.y);
            }
        }

        public void BlueOrb_Click()
        {
            MessagePack.MessageBackToTown(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }

        public void TapMainMessage()
        {
            UpdateTransparent(groupMainMessage, mainMessage);
        }

        protected void UpdateTransparent(GameObject obj, Text txt)
        {
            float current = groupMainMessage.GetComponent<Image>().color.a;
            float current2 = 1.0f;
            if (current == 1.0f)
            {
                current = 0.5f;
                current2 = 1.0f;
            }
            else if (current == 0.5f)
            {
                current = 0.0f;
                current2 = 0.0f;
            }
            else
            {
                current = 1.0f;
                current2 = 1.0f;
            }
            groupMainMessage.GetComponent<Image>().color = new Color(current, current, current, current);
            mainMessage.color = new Color(0, 0, 0, current2);
        }

        protected void AppearMainMessage()
        {
            groupMainMessage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            mainMessage.color = new Color(0, 0, 0, 1);
        }
        protected void HarfAppearMainMessage()
        {
            groupMainMessage.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            mainMessage.color = new Color(0, 0, 0, 1);
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

            if (this.nowReading < this.nowMessage.Count)
            {
                this.Filter.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                this.Filter.SetActive(true);
                this.btnOK.enabled = true;
                this.btnOK.gameObject.SetActive(true);

                MessagePack.ActionEvent currentEvent = this.nowEvent[this.nowReading];
                // メッセージ反映
                if (currentEvent == MessagePack.ActionEvent.None)
                {
                    mainMessage.text = "   " + this.nowMessage[this.nowReading];
                    AppearMainMessage();
                    GroundOne.playbackMessage.Add(this.nowMessage[this.nowReading]);
                }

                // 各イベント固有の処理
                if (currentEvent == MessagePack.ActionEvent.MoveTop)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, false);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveLeft)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveRight)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.MoveBottom)
                {
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, false);
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.StopMusic)
                {
                    GroundOne.StopDungeonMusic();
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic02)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic03)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic04)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic05)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM05, Database.BGM05LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic07)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM07, Database.BGM07LoopBegin);
                }
                else if (currentEvent == MessagePack.ActionEvent.PlayMusic14)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
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
                else if (currentEvent == MessagePack.ActionEvent.DungeonSystemMessage)
                {
                    this.Filter.SetActive(true);
                    HideFilterComplete = false; // フィルタを消さない
                    systemMessageText.text = this.nowMessage[this.nowReading];
                    groupSystemMessage.SetActive(true);
                }

                this.nowReading++;
                if (this.nowMessage[this.nowReading - 1] == "")
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

        private void PrepareCallTruthBattleEnemy()
        {
            if (this.ignoreCreateShadow == false)
            {
                Method.CreateShadowData();
            }
            CancelKeyDownMovement();

            SceneDimension.CallTruthBattleEnemy(Database.TruthDungeon, false, false, false, false);
        }

        public void TapClose()
        {
            SceneDimension.JumpToSingleHomeTown();
        }
        #endregion
        #region "Battle"
        public enum CriticalType
        {
            None,
            Random,
            Absolute,
        }

        bool ActionInstantMode = false;
        string instantActionCommandString = String.Empty;

        bool nowStackAnimation = false;
        int nowStackAnimationCounter = 0;

        bool nowAnimationMatrixTalk = false;
        int nowAnimationMatrixTalkCounter = 0;

        bool nowAnimationSandGlass = false;
        int nowAnimationSandGlassCounter = 0;

        bool nowAnimationFinal = false;
        int nowAnimationFinalCounter = 0;
        int nowAnimationFinalCounterShadow = 0;

        bool nowExecutionWarpGate = false;
        int nowExecutionWarpGateCounter = 0;
        MainCharacter nowExecutionWarpGatePlayer = null;
        MainCharacter nowExecutionWarpGateTarget = null;

        bool nowAnimation = false;
        int nowAnimationCounter = 0;
        List<MainCharacter> nowAnimationTarget = new List<MainCharacter>();
        List<int> nowAnimationDamage = new List<int>();
        List<Color> nowAnimationColor = new List<Color>();
        List<bool> nowAnimationAvoid = new List<bool>();
        List<String> nowAnimationCustomString = new List<string>();
        List<int> nowAnimationInterval = new List<int>();
        List<bool> nowAnimationCritical = new List<bool>();
        List<Text> nowAnimationText = new List<Text>();
        List<double> nowAnimationCurrentLife = new List<double>();
        bool NowSelectingTarget = false;
        MainCharacter currentTargetedPlayer = null;
        bool RunAwayFlag = false; // 戦闘逃げるで終了するためのフラグ
        bool BattleEndFlag = false; // 戦闘終了条件を満たし、バトル終了するためのフラグ
        bool tempStopFlag = false; // [戦闘停止」ボタンやESCキーで、戦闘を一旦停止させたい時に使うフラグ
        bool cannotRunAway = false; // 戦闘から逃げられるかどうかを示すフラグ
        bool NowStackInTheCommand = false; // スタックインザコマンドで一旦停止させたい時に使うフラグ
        List<MainCharacter> stackActivePlayer = new List<MainCharacter>();
        List<int> cumulativeCounter = new List<int>(); // スタックインザコマンドゲージ進行値
        bool NowTimeStop = false; // タイムストップ「全体」のフラグ

        private TruthImage[] pbBuffPlayer1;
        private TruthImage[] pbBuffPlayer2;
        private TruthImage[] pbBuffPlayer3;
        private TruthImage[] pbBuffEnemy1;
        private TruthImage[] pbBuffEnemy2;
        private TruthImage[] pbBuffEnemy3;

        // resource
        private Sprite[] imageSandglass;

        // GUI
        public GameObject groupMatrixDragonTalk;
        public Image back_MatrixDragonTalk;
        public Text MatrixDragonTalkText;
        public Image back_TutorialMessage;
        public Text TutorialMessageText;
        public Image back_Sandglass;
        public Text SandGlassText;
        public Image SandGlassImage;
        public Text TimeStopText;
        public Image back_FinalBattle;
        public Text FinalBattleText;
        public Text TimeSpeedLabel;
        public GameObject groupChooseCommand;
        public TruthImage[] FieldBuff;
        public GameObject groupFieldBuff;
        public GameObject groupParentBackpack;
        public GameObject[] back_Backpack;
        public Text[] backpack;
        public Text[] backpackStack;
        public Image[] backpackIcon;
        public GameObject groupBattleLog;

        public GameObject popupInfo;
        public Text CurrentInfo;
        public Text BattleStart;
        public Button[] ActionButton1;
        public Button[] ActionButton2;
        public Button[] ActionButton3;
        public Button[] ActionButtonE1;
        public Button[] ActionButtonE2;
        public Button[] ActionButtonE3;
        public Text txtBattleMessage;
        public GameObject back_labelBattleTurn;
        public Text labelBattleTurn;
        public Image pbSandglass;
        public Text lblTimerCount;
        public GameObject BattleMenuPanel;
        public Button BattleSettingButton;
        public Button UseItemButton;
        public Button RunAwayButton;

        public GameObject InstantModePanel;
        public Image UseItemGauge;
        public Text UseItemText;
        public GameObject groupPlayer1;
        public GameObject groupPlayer2;
        public GameObject groupPlayer3;
        public GameObject groupEnemy1;
        public GameObject groupEnemy2;
        public GameObject groupEnemy3;

        public GameObject back_nowStackAnimationName;
        public Text nowStackAnimationNameText;
        public GameObject back_nowStackAnimationBar;
        public Text nowStackAnimationBarText;
        public GameObject[] back_StackInTheCommandName;
        public Text[] StackInTheCommandNameText;
        public GameObject[] back_StackInTheCommandBar;
        public Text[] StackInTheCommandBarText;
        private int StackNumber = -1;

        public Image player1Arrow;
        public Text playerActionLabel1;
        public GameObject player1MainObjectBack;
        public Button player1MainObject;
        public Text player1Name;
        public Text player1FullName;
        public Text player1Life;
        public Image player1LifeMeter;
        public Text player1Mana;
        public Image player1ManaMeter;
        public Text player1Skill;
        public Image player1SkillMeter;
        public Text player1Instant;
        public Image player1InstantMeter;
        public Text player1SpecialInstant;
        public Image player1SpecialInstantMeter;
        public GameObject player1DamagePanel;
        public Text player1Damage;
        public Text player1Critical;
        public Image[] IsSorcery1;
        public GameObject player1Panel;
        public GameObject player1ActionPanel;
        public GameObject player1Field;

        public Image player2Arrow;
        public Text playerActionLabel2;
        public GameObject player2MainObjectBack;
        public Button player2MainObject;
        public Text player2Name;
        public Text player2FullName;
        public Text player2Life;
        public Image player2LifeMeter;
        public Text player2Mana;
        public Image player2ManaMeter;
        public Text player2Skill;
        public Image player2SkillMeter;
        public Text player2Instant;
        public Image player2InstantMeter;
        public Text player2SpecialInstant;
        public Image player2SpecialInstantMeter;
        public GameObject player2DamagePanel;
        public Text player2Damage;
        public Text player2Critical;
        public Image[] IsSorcery2;
        public GameObject player2Panel;
        public GameObject player2ActionPanel;
        public GameObject player2Field;

        public Image player3Arrow;
        public Text playerActionLabel3;
        public GameObject player3MainObjectBack;
        public Button player3MainObject;
        public Text player3Name;
        public Text player3FullName;
        public Text player3Life;
        public Image player3LifeMeter;
        public Text player3Mana;
        public Image player3ManaMeter;
        public Text player3Skill;
        public Image player3SkillMeter;
        public Text player3Instant;
        public Image player3InstantMeter;
        public Text player3SpecialInstant;
        public Image player3SpecialInstantMeter;
        public GameObject player3DamagePanel;
        public Text player3Damage;
        public Text player3Critical;
        public Image[] IsSorcery3;
        public GameObject player3Panel;
        public GameObject player3ActionPanel;
        public GameObject player3Field;

        public Image enemy1Arrow;
        public Image enemy1Shadow2;
        public Image enemy1Shadow3;
        public Text enemyActionLabel1;
        public GameObject enemy1MainObjectBack;
        public Button enemy1MainObject;
        public Text enemy1Name;
        public Text enemy1FullName;
        public Text enemy1Life;
        public Image enemy1LifeMeter;
        public Text enemy1Mana;
        public Image enemy1ManaMeter;
        public Text enemy1Skill;
        public Image enemy1SkillMeter;
        public Text enemy1Instant;
        public Image enemy1InstantMeter;
        public Text enemy1SpecialInstant;
        public Image enemy1SpecialInstantMeter;
        public GameObject enemy1DamagePanel;
        public Text enemy1Damage;
        public Text enemy1Critical;
        public Image[] IsSorceryE1;
        public GameObject enemy1Field;

        public Image enemy2Arrow;
        public Text enemyActionLabel2;
        public GameObject enemy2MainObjectBack;
        public Button enemy2MainObject;
        public Text enemy2Name;
        public Text enemy2FullName;
        public Text enemy2Life;
        public Image enemy2LifeMeter;
        public Text enemy2Mana;
        public Image enemy2ManaMeter;
        public Text enemy2Skill;
        public Image enemy2SkillMeter;
        public Text enemy2Instant;
        public Image enemy2InstantMeter;
        public Text enemy2SpecialInstant;
        public Image enemy2SpecialInstantMeter;
        public GameObject enemy2DamagePanel;
        public Text enemy2Damage;
        public Text enemy2Critical;
        public Image[] IsSorceryE2;
        public GameObject enemy2Field;

        public Image enemy3Arrow;
        public Text enemyActionLabel3;
        public GameObject enemy3MainObjectBack;
        public Button enemy3MainObject;
        public Text enemy3Name;
        public Text enemy3FullName;
        public Text enemy3Life;
        public Image enemy3LifeMeter;
        public Text enemy3Mana;
        public Image enemy3ManaMeter;
        public Text enemy3Skill;
        public Image enemy3SkillMeter;
        public Text enemy3Instant;
        public Image enemy3InstantMeter;
        public Text enemy3SpecialInstant;
        public Image enemy3SpecialInstantMeter;
        public GameObject enemy3DamagePanel;
        public Text enemy3Damage;
        public Text enemy3Critical;
        public Image[] IsSorceryE3;
        public GameObject enemy3Field;

        public GameObject BuffPanel1;
        public GameObject BuffPanel2;
        public GameObject BuffPanel3;
        public GameObject PanelBuffEnemy1;
        public GameObject PanelBuffEnemy2;
        public GameObject PanelBuffEnemy3;
        public GameObject treasurePanel;
        public Image treasureIcon;
        public Text treasureText;
        public GameObject ExpGoldPanel;
        public Text ExpGoldText;

        // internal
        private int BattleSpeed = 3;
        private int BattleTimeCounter = Database.BASE_TIMER_BAR_LENGTH;
        private int BattleTurnCount = 0;

        private bool gameStart = false;
        private bool endBattleForMatrixDragonEnd = false; // 支配竜会話終了時に戦闘終了させるフラグ

        private TruthEnemyCharacter ec1;
        private TruthEnemyCharacter ec2;
        private TruthEnemyCharacter ec3;

        private MainCharacter currentPlayer;

        private int activatePlayerNumber = 0;
        private List<MainCharacter> ActiveList = new List<MainCharacter>();

        private int MAX_ITEM_GAUGE = 500;
        private int currentItemGauge = 0;

        private string ChooseCommand = string.Empty;

        // Use this for initialization
        public void TempStart()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                BattleStart.text = Database.GUI_BATTLE_GO;
                labelBattleTurn.text = Database.GUI_BATTLE_TURN + " 1";
            }

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) { Database.BATTLE_CORE_SLEEP = 0; }
            else { Database.BATTLE_CORE_SLEEP = 1; }

            // 最終戦ヴェルゼのシャドウマークをデフォルトでは非表示にする。
            enemy1Shadow2.gameObject.SetActive(false);
            enemy1Shadow3.gameObject.SetActive(false);

            Texture2D textureSandGlass = Resources.Load<Texture2D>("SandGlassIcon");
            this.imageSandglass = new Sprite[8];
            int BASE_SIZE_X = 152;
            int BASE_SIZE_Y = 211;
            for (int locX = 0; locX < Database.TIMER_ICON_NUM; locX++)
            {
                this.imageSandglass[locX] = Sprite.Create(textureSandGlass, new Rect(BASE_SIZE_X * locX, 0, BASE_SIZE_X, BASE_SIZE_Y), new Vector2(0, 0));
            }
            this.pbSandglass.sprite = this.imageSandglass[0];

            pbBuffPlayer1 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer2 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer3 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy1 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy2 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy3 = new TruthImage[Database.BUFF_NUM];
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                SetupBuff(ref pbBuffPlayer1, BuffPanel1, ii);
                SetupBuff(ref pbBuffPlayer2, BuffPanel2, ii);
                SetupBuff(ref pbBuffPlayer3, BuffPanel3, ii);
                SetupBuff(ref pbBuffEnemy1, PanelBuffEnemy1, ii);
                SetupBuff(ref pbBuffEnemy2, PanelBuffEnemy2, ii);
                SetupBuff(ref pbBuffEnemy3, PanelBuffEnemy3, ii);
            }

            GroundOne.P1.CurrentCommand = Database.ATTACK_EN;
            GroundOne.P1.CurrentInstantPoint = 0;
            GroundOne.P1.MainFaceArrow = this.player1Arrow;
            GroundOne.P1.MainObjectButton = this.player1MainObject;
            GroundOne.P1.ActionLabel = this.playerActionLabel1;
            GroundOne.P1.labelName = this.player1Name;
            GroundOne.P1.labelCurrentLifePoint = this.player1Life;
            GroundOne.P1.meterCurrentLifePoint = this.player1LifeMeter;
            GroundOne.P1.labelCurrentManaPoint = this.player1Mana;
            GroundOne.P1.meterCurrentManaPoint = this.player1ManaMeter;
            GroundOne.P1.labelCurrentSkillPoint = this.player1Skill;
            GroundOne.P1.meterCurrentSkillPoint = this.player1SkillMeter;
            GroundOne.P1.labelCurrentInstantPoint = this.player1Instant;
            GroundOne.P1.meterCurrentInstantPoint = this.player1InstantMeter;
            GroundOne.P1.DamagePanel = this.player1DamagePanel;
            GroundOne.P1.DamageLabel = this.player1Damage;
            GroundOne.P1.CriticalLabel = this.player1Critical;
            GroundOne.P1.ActionButtonList.AddRange(this.ActionButton1);

            GroundOne.P2.CurrentCommand = Database.ATTACK_EN;
            GroundOne.P2.CurrentInstantPoint = 0;
            GroundOne.P2.MainFaceArrow = this.player2Arrow;
            GroundOne.P2.MainObjectButton = this.player2MainObject;
            GroundOne.P2.ActionLabel = this.playerActionLabel2;
            GroundOne.P2.labelName = this.player2Name;
            GroundOne.P2.labelCurrentLifePoint = this.player2Life;
            GroundOne.P2.meterCurrentLifePoint = this.player2LifeMeter;
            GroundOne.P2.labelCurrentManaPoint = this.player2Mana;
            GroundOne.P2.meterCurrentManaPoint = this.player2ManaMeter;
            GroundOne.P2.labelCurrentSkillPoint = this.player2Skill;
            GroundOne.P2.meterCurrentSkillPoint = this.player2SkillMeter;
            GroundOne.P2.labelCurrentInstantPoint = this.player2Instant;
            GroundOne.P2.meterCurrentInstantPoint = this.player2InstantMeter;
            GroundOne.P2.DamagePanel = this.player2DamagePanel;
            GroundOne.P2.DamageLabel = this.player2Damage;
            GroundOne.P2.CriticalLabel = this.player2Critical;
            GroundOne.P2.ActionButtonList.AddRange(this.ActionButton2);

            GroundOne.P3.CurrentCommand = Database.ATTACK_EN;
            GroundOne.P3.CurrentInstantPoint = 0;
            GroundOne.P3.MainFaceArrow = this.player3Arrow;
            GroundOne.P3.MainObjectButton = this.player3MainObject;
            GroundOne.P3.ActionLabel = this.playerActionLabel3;
            GroundOne.P3.labelName = this.player3Name;
            GroundOne.P3.labelCurrentLifePoint = this.player3Life;
            GroundOne.P3.meterCurrentLifePoint = this.player3LifeMeter;
            GroundOne.P3.labelCurrentManaPoint = this.player3Mana;
            GroundOne.P3.meterCurrentManaPoint = this.player3ManaMeter;
            GroundOne.P3.labelCurrentSkillPoint = this.player3Skill;
            GroundOne.P3.meterCurrentSkillPoint = this.player3SkillMeter;
            GroundOne.P3.labelCurrentInstantPoint = this.player3Instant;
            GroundOne.P3.meterCurrentInstantPoint = this.player3InstantMeter;
            GroundOne.P3.DamagePanel = this.player3DamagePanel;
            GroundOne.P3.DamageLabel = this.player3Damage;
            GroundOne.P3.CriticalLabel = player3Critical;
            GroundOne.P3.ActionButtonList.AddRange(this.ActionButton3);

            GameObject baseObj1 = new GameObject("enemyObj1");
            ec1 = baseObj1.AddComponent<TruthEnemyCharacter>();
            ec1.Initialize(GroundOne.enemyName1);
            ec1.CurrentCommand = Database.ATTACK_EN;
            ec1.CurrentInstantPoint = 0;
            ec1.MainFaceArrow = enemy1Arrow;
            ec1.MainObjectButton = enemy1MainObject;
            ec1.ActionLabel = enemyActionLabel1;
            ec1.labelName = enemy1Name;
            ec1.labelCurrentLifePoint = enemy1Life;
            ec1.meterCurrentLifePoint = enemy1LifeMeter;
            ec1.labelCurrentManaPoint = null;
            ec1.meterCurrentManaPoint = null;
            ec1.labelCurrentSkillPoint = null;
            ec1.meterCurrentSkillPoint = null;
            ec1.labelCurrentInstantPoint = enemy1Instant;
            ec1.meterCurrentInstantPoint = enemy1InstantMeter;
            ec1.labelCurrentSpecialInstant = enemy1SpecialInstant;
            ec1.meterCurrentSpecialInstant = enemy1SpecialInstantMeter;
            ec1.DamagePanel = enemy1DamagePanel;
            ec1.DamageLabel = enemy1Damage;
            ec1.CriticalLabel = enemy1Critical;

            GameObject baseObj2 = new GameObject("enemyObj2");
            ec2 = baseObj2.AddComponent<TruthEnemyCharacter>();
            ec2.Initialize(GroundOne.enemyName2);
            ec2.CurrentCommand = Database.ATTACK_EN;
            ec2.CurrentInstantPoint = 0;
            ec2.MainFaceArrow = enemy2Arrow;
            ec2.MainObjectButton = enemy2MainObject;
            ec2.ActionLabel = enemyActionLabel2;
            ec2.labelName = enemy2Name;
            ec2.labelCurrentLifePoint = enemy2Life;
            ec2.meterCurrentLifePoint = enemy2LifeMeter;
            ec2.labelCurrentManaPoint = null;
            ec2.meterCurrentManaPoint = null;
            ec2.labelCurrentSkillPoint = null;
            ec2.meterCurrentSkillPoint = null;
            ec2.labelCurrentInstantPoint = enemy2Instant;
            ec2.meterCurrentInstantPoint = enemy2InstantMeter;
            ec2.DamagePanel = enemy2DamagePanel;
            ec2.DamageLabel = enemy2Damage;
            ec2.CriticalLabel = enemy2Critical;

            GameObject baseObj3 = new GameObject("enemyObj3");
            ec3 = baseObj3.AddComponent<TruthEnemyCharacter>();
            ec3.Initialize(GroundOne.enemyName3);
            ec3.CurrentCommand = Database.PROTECTION;
            ec3.CurrentInstantPoint = 0;
            ec3.MainFaceArrow = enemy3Arrow;
            ec3.MainObjectButton = enemy3MainObject;
            ec3.ActionLabel = enemyActionLabel3;
            ec3.labelName = enemy3Name;
            ec3.labelCurrentLifePoint = enemy3Life;
            ec3.meterCurrentLifePoint = enemy3LifeMeter;
            ec3.labelCurrentManaPoint = null;
            ec3.meterCurrentManaPoint = null;
            ec3.labelCurrentSkillPoint = null;
            ec3.meterCurrentSkillPoint = null;
            ec3.labelCurrentInstantPoint = enemy3Instant;
            ec3.meterCurrentInstantPoint = enemy3InstantMeter;
            ec3.DamagePanel = enemy3DamagePanel;
            ec3.DamageLabel = enemy3Damage;
            ec3.CriticalLabel = enemy3Critical;

            SetupBuffElement(GroundOne.P1, ref pbBuffPlayer1);
            SetupBuffElement(GroundOne.P2, ref pbBuffPlayer2);
            SetupBuffElement(GroundOne.P3, ref pbBuffPlayer3);
            SetupBuffElement(ec1, ref pbBuffEnemy1);
            SetupBuffElement(ec2, ref pbBuffEnemy2);
            SetupBuffElement(ec3, ref pbBuffEnemy3);

            if (GroundOne.WE.AvailableFirstCharacter == false)
            {
                groupPlayer1.SetActive(false);
                player1Field.SetActive(false);
            }
            else
            {
                groupPlayer1.SetActive(true);
                player1Field.SetActive(true);
                ActivateSomeCharacter(GroundOne.P1, ec1, player1Name, player1FullName, player1Life, player1LifeMeter, player1Mana, player1ManaMeter, player1Skill, player1SkillMeter, player1Instant, player1InstantMeter, player1SpecialInstant, player1SpecialInstantMeter, ActionButton1, playerActionLabel1, BuffPanel1, player1Panel, player1ActionPanel, player1MainObjectBack, player1MainObject, GroundOne.P1.PlayerBattleTargetColor1, player1Arrow, null, null, player1Damage, player1Critical, ref pbBuffPlayer1, IsSorcery1);
            }
            if (GroundOne.WE.AvailableSecondCharacter == false || GroundOne.DuelMode)
            {
                groupPlayer2.SetActive(false);
                player2Field.SetActive(false);
            }
            else
            {
                groupPlayer2.SetActive(true);
                player2Field.SetActive(true);
                ActivateSomeCharacter(GroundOne.P2, ec1, player2Name, player2FullName, player2Life, player2LifeMeter, player2Mana, player2ManaMeter, player2Skill, player2SkillMeter, player2Instant, player2InstantMeter, player2SpecialInstant, player2SpecialInstantMeter, ActionButton2, playerActionLabel2, BuffPanel2, player2Panel, player2ActionPanel, player2MainObjectBack, player2MainObject, GroundOne.P2.PlayerBattleTargetColor1, player2Arrow, null, null, player2Damage, player2Critical, ref pbBuffPlayer2, IsSorcery2);
            }

            if (GroundOne.WE.AvailableThirdCharacter == false || GroundOne.DuelMode)
            {
                groupPlayer3.SetActive(false);
                player3Field.SetActive(false);
            }
            else
            {
                groupPlayer3.SetActive(true);
                player3Field.SetActive(true);
                ActivateSomeCharacter(GroundOne.P3, ec1, player3Name, player3FullName, player3Life, player3LifeMeter, player3Mana, player3ManaMeter, player3Skill, player3SkillMeter, player3Instant, player3InstantMeter, player3SpecialInstant, player3SpecialInstantMeter, ActionButton3, playerActionLabel3, BuffPanel3, player3Panel, player3ActionPanel, player3MainObjectBack, player3MainObject, GroundOne.P3.PlayerBattleTargetColor1, player3Arrow, null, null, player3Damage, player3Critical, ref pbBuffPlayer3, IsSorcery3);
            }

            if (GroundOne.enemyName1 == String.Empty)
            {
                groupEnemy1.SetActive(false);
                enemy1Field.SetActive(false);
                ec1 = null;
            }
            else
            {
                groupEnemy1.SetActive(true);
                enemy1Field.SetActive(true);
                ActivateSomeCharacter(ec1, GroundOne.P1, enemy1Name, enemy1FullName, enemy1Life, enemy1LifeMeter, enemy1Mana, enemy1ManaMeter, enemy1Skill, enemy1SkillMeter, enemy1Instant, enemy1InstantMeter, enemy1SpecialInstant, enemy1SpecialInstantMeter, ActionButtonE1, enemyActionLabel1, PanelBuffEnemy1, null, null, enemy1MainObjectBack, enemy1MainObject, new Color(87.0f / 255.0f, 0.0f, 16.0f / 255.0f), enemy1Arrow, enemy1Shadow2, enemy1Shadow3, enemy1Damage, enemy1Critical, ref pbBuffEnemy1, IsSorceryE1);
            }

            if (GroundOne.enemyName2 == String.Empty || GroundOne.DuelMode)
            {
                groupEnemy2.SetActive(false);
                enemy2Field.SetActive(false);
                ec2 = null;
            }
            else
            {
                groupEnemy2.SetActive(true);
                enemy2Field.SetActive(true);
                ActivateSomeCharacter(ec2, GroundOne.P1, enemy2Name, enemy2FullName, enemy2Life, enemy2LifeMeter, enemy2Mana, enemy2ManaMeter, enemy2Skill, enemy2SkillMeter, enemy2Instant, enemy2InstantMeter, enemy2SpecialInstant, enemy2SpecialInstantMeter, ActionButtonE2, enemyActionLabel2, PanelBuffEnemy2, null, null, enemy2MainObjectBack, enemy2MainObject, new Color(108.0f / 255.0f, 118.0f / 255.0f, 0.0f), enemy2Arrow, null, null, enemy2Damage, enemy2Critical, ref pbBuffEnemy2, IsSorceryE2);
            }

            if (GroundOne.enemyName3 == String.Empty || GroundOne.DuelMode)
            {
                groupEnemy3.SetActive(false);
                enemy3Field.SetActive(false);
                ec3 = null;
            }
            else
            {
                groupEnemy3.SetActive(true);
                enemy3Field.SetActive(true);
                ActivateSomeCharacter(ec3, GroundOne.P1, enemy3Name, enemy3FullName, enemy3Life, enemy3LifeMeter, enemy3Mana, enemy3ManaMeter, enemy3Skill, enemy3SkillMeter, enemy3Instant, enemy3InstantMeter, enemy2SpecialInstant, enemy3SpecialInstantMeter, ActionButtonE3, enemyActionLabel3, PanelBuffEnemy3, null, null, enemy3MainObjectBack, enemy3MainObject, new Color(69.0f / 255.0f, 99.0f / 255.0f, 129.0f / 255.0f), enemy3Arrow, null, null, enemy3Damage, enemy3Critical, ref pbBuffEnemy3, IsSorceryE3);
            }

            player1ActionPanel.SetActive(true);
            player1InstantMeter.gameObject.SetActive(true);
            player2ActionPanel.SetActive(false);
            //player2InstantMeter.gameObject.SetActive(false);
            player3ActionPanel.SetActive(false);
            //player3InstantMeter.gameObject.SetActive(false);

            for (int ii = 0; ii < this.ActiveList.Count; ii++)
            {
                UpdateLife(this.ActiveList[ii]);
                UpdateMana(this.ActiveList[ii]);
                UpdateSkillPoint(this.ActiveList[ii]);
                float widthScale = (float)(Screen.width) / (float)(Database.BASE_TIMER_BAR_LENGTH);
                Vector3 current = ActiveList[ii].MainFaceArrow.transform.position;
                ActiveList[ii].MainFaceArrow.transform.position = new Vector3((float)ActiveList[ii].BattleBarPos * widthScale - ActiveList[ii].MainFaceArrow.rectTransform.sizeDelta.x / 2.0f, current.y, current.z);
            }

            this.currentPlayer = GroundOne.P1;
            Method.UpdateBackPackLabel(this.currentPlayer, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);
            this.currentItemGauge = 0;
            UpdateUseItemGauge();
            //tapFirstChara ();

            if (ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                groupFieldBuff.SetActive(true);
            }
            else
            {
                groupFieldBuff.SetActive(false);
            }

            GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
        }

        // Update is called once per frame
        public void TempUpdate()
        {
            if (Database.BATTLE_CORE_SLEEP > 0)
            {
                System.Threading.Thread.Sleep(Database.BATTLE_CORE_SLEEP);
            }

            #region "進行停止"
            if (this.nowAnimationSandGlass)
            {
                ExecAnimationSandGlass();
                //Debug.Log("nowAnimationSandGlass is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimationFinal)
            {
                ExecAnimationFinalBattle();
                //Debug.Log("nowAnimationFinal is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimationMatrixTalk)
            {
                ExecAnimationMessageFadeOut();
                //Debug.Log("nowAnimationMatrixTalk is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimation)
            {
                ExecAnimation();
                //Debug.Log("nowAnimation is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowStackAnimation)
            {
                ExecStackAnimation();
                //Debug.Log("nowStackAnimation is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowExecutionWarpGate)
            {
                //ExecPlayWarpGate();
                //Debug.Log("nowExecutionWarpGate is true then return");
                return; // ワープゲート実行中は停止させる。
            }

            // バトル終了条件が満たされている場合、バトル終了とする。
            if (this.BattleEndFlag)
            {
                //Debug.Log("BattleEndFlag is true then return"); 
                BattleEndPhase();
            }

            if (UpdatePlayerDeadFlag()) { Debug.Log("UpdatePlayerDeadFlag is true then return"); return; }
            if (this.BattleEndFlag) { Debug.Log("endFlag is true then return"); return; } // 終了サインが出た場合、戦闘終了として待機する。
            if (this.gameStart == false) { return; } // 戦闘開始サインが無い状態では、待機する。
            if (this.endBattleForMatrixDragonEnd)
            {
                BattleEndPhase();
                return;
            } // 戦闘終了サインにより、戦闘を抜ける。
            #endregion

            #region "ゲージ位置"
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                UpdatePlayerMainFaceArrow(ActiveList[ii]);
            }
            #endregion

            //CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag())
            {
                return; // パーティ死亡確認で戦闘を抜ける。
            }

            #region "戦闘一旦停止フラグ"
            if (this.tempStopFlag) { return; } // 「戦闘停止」ボタンやESCキーで、一旦停止させる。
            if (GroundOne.DuelMode == false) // DUELモードの時、選択肢の選択中は一旦停止しない。
            {
                if (this.NowSelectingTarget) { return; } // インスタント行動対象選択時、一旦停止させる。
            }
            if (this.NowStackInTheCommand) { return; } // スタックインザコマンド発動中は停止させる。
            #endregion

            #region "ターン砂時計"
            if (this.NowTimeStop == false)
            {
                this.BattleTimeCounter++; // メイン戦闘タイマーカウント更新
                // Bystander専用
                int currentTimerCount = this.BattleTimeCounter;
                if (BattleTurnCount != 0)
                {
                    double currentTime = (Database.BASE_TIMER_BAR_LENGTH - (double)currentTimerCount) / (Database.BASE_TIMER_BAR_LENGTH) * 5.0f;
                    lblTimerCount.text = currentTime.ToString("0.00");
                }
                for (int ii = 0; ii < Database.TIMER_ICON_NUM; ii++)
                {
                    if (Database.BASE_TIMER_DIV * ii <= this.BattleTimeCounter && this.BattleTimeCounter < Database.BASE_TIMER_DIV * (ii + 1))
                    {
                        pbSandglass.sprite = this.imageSandglass[ii];
                        break;
                    }
                }
            }
            #endregion

            if (BattleTimeCounter >= Database.BASE_TIMER_BAR_LENGTH)
            {
                if (BattleTurnCount == 0)
                {
                    // ターン開始時（戦闘開始直後）
                    ExecPhaseElement(MethodType.Beginning, null);
                    // ターンを更新（１ターン始まり）
                    UpdateTurnEnd();
                }
                else
                {
                    // ターン更新直前にて、戦闘後の追加効果フェーズ
                    ExecPhaseElement(MethodType.AfterBattleEffect, null);

                    // ターンを更新
                    UpdateTurnEnd();

                    // ターン更新直後のクリーンナップ
                    ExecPhaseElement(MethodType.CleanUpStep, null);

                    // ターン更新後のアップキープ
                    ExecPhaseElement(MethodType.UpKeepStep, null);
                }
            }
            else
            {
                ExecPhaseElement(MethodType.CleanUpForBoss, null);
            }

            UpdateUseItemGauge();

            #region "各プレイヤーの戦闘フェーズ"
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (this.NowTimeStop && ActiveList[ii].CurrentTimeStop <= 0 && ActiveList[ii].FirstName != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    // 時間は飛ばされる
                }
                else if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii].BattleBarPos > Database.BASE_TIMER_BAR_LENGTH ||
                        ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH ||
                        ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH)
                    {
                        // 戦闘行動を実行前にポジションと意思決定フラグとカウンターアタックを解除
                        int arrowType = 0;
                        if (ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 1; }
                        else if (ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 2; }
                        UpdatePlayerPreCondition(ActiveList[ii], arrowType);

                        // 戦闘行動を実行
                        if (ExecPhaseElement(MethodType.PlayerAttackPhase, ActiveList[ii]) == false) break;

                        if (ActiveList[ii].CurrentSkillName == Database.STANCE_OF_FLOW && ActiveList[ii].PA == MainCharacter.PlayerAction.UseSkill)
                        {
                            ActiveList[ii].BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                        }

                        // 対象が行動不能な場合、ターゲットを切り替える。
                        //UpdatePlayerTarget(ActiveList[ii]);
                    }
                    else
                    {
                        // インスタント行動のタイマー更新
                        UpdatePlayerInstantPoint(ActiveList[ii]);

                        // 戦闘待機ポジション更新
                        UpdatePlayerGaugePosition(ActiveList[ii]);

                        // 戦闘実行内容の決定フェーズ（敵専用)
                        UpdatePlayerNextDecision(ActiveList[ii]);

                        // スタックインザコマンドの発動決定フェーズ（敵専用）
                        UpdatePlayerDoStackInTheCommand(ActiveList[ii]);
                    }
                }
            }
            #endregion

            //CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag())
            {
                return; // パーティ死亡確認で戦闘を抜ける。
            }
        }

        private void UpdatePlayerMainFaceArrow(MainCharacter player)
        {
            float widthScale = (float)(Screen.width) / (float)(Database.BASE_TIMER_BAR_LENGTH);
            Vector3 current = player.MainFaceArrow.transform.position;
            player.MainFaceArrow.transform.position = new Vector3((float)player.BattleBarPos * widthScale - player.MainFaceArrow.rectTransform.sizeDelta.x / 2.0f, current.y, current.z);

            // カオティックスキーマ限定
            if (player.ShadowFaceArrow2 != null)
            {
                if (player.CurrentChaoticSchema > 0)
                {
                    Vector3 current2 = player.ShadowFaceArrow2.transform.position;
                    player.ShadowFaceArrow2.transform.position = new Vector3((float)player.BattleBarPos2 * widthScale - player.ShadowFaceArrow2.rectTransform.sizeDelta.x / 2.0f, current2.y, current2.z);
                }
                else
                {
                    player.ShadowFaceArrow2.gameObject.SetActive(false);
                }
            }
            if (player.ShadowFaceArrow3 != null)
            {
                if (player.CurrentChaoticSchema > 0 && player.CurrentLifeCountValue <= 1)
                {
                    Vector3 current3 = player.ShadowFaceArrow3.transform.position;
                    player.ShadowFaceArrow3.transform.position = new Vector3((float)player.BattleBarPos3 * widthScale - player.ShadowFaceArrow3.rectTransform.sizeDelta.x / 2.0f, current3.y, current3.z);
                }
                else
                {
                    player.ShadowFaceArrow3.gameObject.SetActive(false);
                }
            }
        }


        void SelectPlayerArrow(MainCharacter player)
        {
            if (player.FirstName == Database.EIN_WOLENCE) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player1Arrow"); }
            else if (player.FirstName == Database.RANA_AMILIA) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player2Arrow"); ; }
            else if (player.FirstName == Database.OL_LANDIS) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player3Arrow"); ; }
            else if (player.FirstName == Database.VERZE_ARTIE) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player4Arrow"); ; }
            else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player5Arrow"); ; }
            else { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player3Arrow"); }
        }
        void SelectPlayerPanelColor(MainCharacter player)
        {
            if (player.ManaSkillPanel != null)
            {
                if (player.FirstName == Database.EIN_WOLENCE) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(0.0f / 255.0f, 183.0f / 255.0f, 239.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.RANA_AMILIA) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 196.0f / 255.0f, 251.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.OL_LANDIS) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 242.0f / 255.0f, 3.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.VERZE_ARTIE) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(120.0f / 255.0f, 120.0f / 255.0f, 120.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(81.0f / 255.0f, 7.0f / 255.0f, 255.0f / 255.0f, 100f / 255.0f); }
            }
            if (player.meterCurrentInstantPoint != null)
            {
                if (player.FirstName == Database.EIN_WOLENCE) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(0.0f / 255.0f, 198.0f / 255.0f, 190.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.RANA_AMILIA) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(248.0f / 255.0f, 124.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.OL_LANDIS) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(249.0f / 255.0f, 243.0f / 255.0f, 4.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.VERZE_ARTIE) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(124.0f / 255.0f, 4.0f / 255.0f, 233.0f / 255.0f, 255.0f / 255.0f); }
            }
        }

        protected virtual void ActivateSomeCharacter(MainCharacter player, MainCharacter target,
            Text charaName, Text fullName,
            Text life, Image lifeMeter,
            Text mana, Image manaMeter,
            Text skill, Image skillMeter,
            Text instant, Image instantMeter,
            Text specialInstant, Image specialInstantMeter,
            Button[] actionButton,
            Text actionLabel,
            GameObject buffPanel, GameObject ManaSkillPanel, GameObject ActionPanel,
            GameObject mainObjectBack, Button mainObject, Color mainColor, Image mainFaceArrow, Image shadowFaceArrow2, Image shadowFaceArrow3,
            Text damageLabel, Text criticalLabel,
            ref TruthImage[] buffList,
            Image[] sorceryMark
            )
        {
            player.RealTimeBattle = true;

            // 戦闘画面UIへの初期設定
            // MainCharacterクラス内容と戦闘画面UIの割り当て
            player.labelName = charaName;
            player.labelName.text = player.FullName;
            if (fullName != null)
            {
                player.labelFullName = fullName;
                player.labelFullName.text = player.FullName;
            }

            player.labelCurrentLifePoint = life;
            player.meterCurrentLifePoint = lifeMeter;
            player.labelCurrentManaPoint = mana;
            player.meterCurrentManaPoint = manaMeter;
            player.labelCurrentSkillPoint = skill;
            player.meterCurrentSkillPoint = skillMeter;
            player.labelCurrentInstantPoint = instant;
            player.meterCurrentInstantPoint = instantMeter;
            player.labelCurrentSpecialInstant = specialInstant;
            player.meterCurrentSpecialInstant = specialInstantMeter;

            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            }

            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            }

            player.CurrentInstantPoint = 0; // 後編追加 // 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.text = player.CurrentInstantPoint.ToString() + " / " + player.MaxInstantPoint.ToString();
            }

            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
            }

            if (actionButton != null)
            {
                player.ActionButtonList.Clear();
                for (int ii = 0; ii < actionButton.Length; ii++)
                {
                    player.ActionButtonList.Add(actionButton[ii]);
                }
            }

            if (sorceryMark != null)
            {
                player.IsSorceryMark.Clear();
                for (int ii = 0; ii < sorceryMark.Length; ii++)
                {
                    player.IsSorceryMark.Add(sorceryMark[ii]);
                }
            }

            player.TextBattleMessage = this.txtBattleMessage;

            player.ActionLabel = actionLabel;

            player.BuffPanel = buffPanel;
            player.BuffPanel.SetActive(true);
            player.ManaSkillPanel = ManaSkillPanel;
            player.ActionButtonPanel = ActionPanel;

            player.MainColor = mainColor;
            player.MainObjectBack = mainObjectBack;
            player.MainObjectButton = mainObject;
            if (mainObjectBack != null)
            {
                player.MainObjectBack.GetComponent<Image>().color = mainColor;
            }
            if (mainFaceArrow != null)
            {
                player.MainFaceArrow = mainFaceArrow;
            }
            player.MainFaceArrow = mainFaceArrow;
            player.ShadowFaceArrow2 = null;
            player.ShadowFaceArrow3 = null;

            if (player == GroundOne.P1 || player == GroundOne.P2 || player == GroundOne.P3)
            {
                SelectPlayerArrow(player);
            }
            SelectPlayerPanelColor(player);

            player.DamageLabel = damageLabel;
            player.CriticalLabel = criticalLabel;

            // 登録を反映
            player.BuffElement = buffList;

            // 各プレイヤーのターゲット選定
            player.Target = target;
            player.Target2 = player; // 味方選択はデフォルトでは自分自身としておく。
            if ((player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
            {
                player.Target2 = ec1;
            }

            // 各プレイヤーの初期行動を選定
            player.PA = MainCharacter.PlayerAction.NormalAttack;
            player.ReserveBattleCommand = Database.ATTACK_EN;
            //PlayerActionSet(player);

            // 各プレイヤーの戦闘バーの位置
            if (GroundOne.DuelMode)
            {
                player.BattleBarPos = 0;
            }
            else
            {
                System.Random rand = new System.Random(DateTime.Now.Millisecond * System.Environment.TickCount);
                player.BattleBarPos = rand.Next(100, 400);
                if (player.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                {
                    player.BattleBarPos = ec1.BattleBarPos + 250;
                    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                    }
                }

                if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                {
                    player.BattleBarPos = ec1.BattleBarPos + 150;
                    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                    }
                }
                if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                {
                    player.BattleBarPos = ec1.BattleBarPos + 300;
                    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                    }
                }
            }

            // 各プレイヤーの戦闘行動決定タイミングの設定（敵専用）
            player.DecisionTiming = 250;

            // 各プレイヤーを表示可能にする
            player.ActivateCharacter();

            // 各プレイヤーの戦闘への参加
            ActiveList.Add(player); // this.activatePlayerNumber, player); // change unity
            this.activatePlayerNumber++;

            // 各プレイヤーのスキル開放制限
            if (!player.AvailableSkill)
            {
                if (player.labelCurrentSkillPoint != null)
                {
                    player.labelCurrentSkillPoint.gameObject.SetActive(false);
                }
                if (player.meterCurrentSkillPoint != null)
                {
                    player.meterCurrentSkillPoint.gameObject.SetActive(false);
                }
            }

            // 各プレイヤーの魔法開放制限
            if (!player.AvailableMana)
            {
                if (player.labelCurrentManaPoint != null)
                {
                    player.labelCurrentManaPoint.gameObject.SetActive(false);
                }
                if (player.meterCurrentManaPoint != null)
                {
                    player.meterCurrentManaPoint.gameObject.SetActive(false);
                }
            }

            // 各プレイヤーのインスタントコマンドの開放制限
            if (!GroundOne.WE.AvailableInstantCommand)
            {
                if (player.meterCurrentInstantPoint != null)
                {
                    player.meterCurrentInstantPoint.gameObject.SetActive(false);
                }
                if (player.labelCurrentInstantPoint != null)
                {
                    player.labelCurrentInstantPoint.gameObject.SetActive(false);
                }
            }
            if ((player.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                (player.FirstName == Database.ENEMY_BRILLIANT_SEA_PRINCE))
            {
                player.labelCurrentInstantPoint.gameObject.SetActive(true);
            }
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).UseStackCommand == false && GroundOne.DuelMode == false)
                {
                    if (player.meterCurrentInstantPoint != null)
                    {
                        player.meterCurrentInstantPoint.gameObject.SetActive(false);
                    }
                    if (player.labelCurrentInstantPoint != null)
                    {
                        player.labelCurrentInstantPoint.gameObject.SetActive(false);
                    }
                }
            }

            // 味方側、魔法・スキルをセットアップ
            // プレイヤースキル・魔法習得に応じて、アクションボタンを登録
            UpdateBattleCommandSetting(player, actionButton, sorceryMark);

            // インスタントポイント０をGUIへ反映
            UpdateInstantPoint(player);

            // ボス戦の場合、ネームラベルやBUFFの表示場所を変更します。
            #region "敵側、名前の色と各ＵＩポジションを再配置"
            if (GroundOne.DuelMode)
            {
                if (player == ec1 && ec1.FullName == Database.ENEMY_LAST_SIN_VERZE_ARTIE ||
                    player == ec1 && ec1.FullName == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    UpdateSpecialInstantPoint(player);
                    player.meterCurrentSpecialInstant.gameObject.SetActive(true);
                }
            }

            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                {
                    player.labelName.color = UnityColor.DarkOrange;
                }
                // 支配竜
                else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Purple)
                {
                    Debug.Log("Shihairyuu layout");
                    this.cannotRunAway = true;
                }
            }
            #endregion

            // 死んでいる場合、グレー化する
            if (player.Dead)
            {
                player.DeadPlayer();
            }
        }

        private void UpdateBattleCommandSetting(MainCharacter player, Button[] actionButton, Image[] sorceryMark)
        {
            if (player == GroundOne.P1 || player == GroundOne.P2 || player == GroundOne.P3)
            {
                for (int ii = 0; ii < player.BattleActionCommandList.Length; ii++)
                {
                    Method.SetupActionButton(actionButton[ii].gameObject, sorceryMark[ii], player.BattleActionCommandList[ii]);
                }
            }
        }
        
        /// <summary>
        /// Unityのイベントトリガーを設定して、マウスイベント制御を設定、Buffパネルへのエントリーを設定
        /// </summary>
        /// <param name="pbBuff"></param>
        /// <param name="buffPanel"></param>
        /// <param name="ii"></param>
        void SetupBuff(ref TruthImage[] pbBuff, GameObject buffPanel, int ii)
        {
            GameObject baseObj = new GameObject("panelObj" + ii.ToString());

            pbBuff[ii] = baseObj.AddComponent<TruthImage>();


            pbBuff[ii].BuffMode = TruthImage.buffType.Small;
            pbBuff[ii].rectTransform.anchorMin = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.anchorMax = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.pivot = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.sizeDelta = new Vector2(40, 40);
            pbBuff[ii].rectTransform.anchoredPosition = new Vector2(Database.BUFFPANEL_BUFF_WIDTH, 0);
            pbBuff[ii].gameObject.SetActive(false);
            pbBuff[ii].transform.SetParent(buffPanel.transform, false);
        }

        private void SetupBuffElement(MainCharacter player, ref TruthImage[] buffList)
        {
            #region "BUFFリストを登録"
            int num = 0;
            player.pbProtection = buffList[num]; buffList[num].ImageName = Database.PROTECTION; num++;
            player.pbAbsorbWater = buffList[num]; buffList[num].ImageName = Database.ABSORB_WATER; num++;
            player.pbShadowPact = buffList[num]; buffList[num].ImageName = Database.SHADOW_PACT; num++;
            player.pbFlameAura = buffList[num]; buffList[num].ImageName = Database.FLAME_AURA; num++;
            player.pbHeatBoost = buffList[num]; buffList[num].ImageName = Database.HEAT_BOOST; num++;
            player.pbSaintPower = buffList[num]; buffList[num].ImageName = Database.SAINT_POWER; num++;
            player.pbWordOfLife = buffList[num]; buffList[num].ImageName = Database.WORD_OF_LIFE; num++;
            player.pbGlory = buffList[num]; buffList[num].ImageName = Database.GLORY; num++;
            player.pbVoidExtraction = buffList[num]; buffList[num].ImageName = Database.VOID_EXTRACTION; num++;
            player.pbOneImmunity = buffList[num]; buffList[num].ImageName = Database.ONE_IMMUNITY; num++;
            player.pbGaleWind = buffList[num]; buffList[num].ImageName = Database.GALE_WIND; num++;
            player.pbWordOfFortune = buffList[num]; buffList[num].ImageName = Database.WORD_OF_FORTUNE; num++;
            player.pbBloodyVengeance = buffList[num]; buffList[num].ImageName = Database.BLOODY_VENGEANCE; num++;
            player.pbRiseOfImage = buffList[num]; buffList[num].ImageName = Database.RISE_OF_IMAGE; num++;
            player.pbImmortalRave = buffList[num]; buffList[num].ImageName = Database.IMMORTAL_RAVE; num++;
            player.pbHighEmotionality = buffList[num]; buffList[num].ImageName = Database.HIGH_EMOTIONALITY; num++;
            player.pbBlackContract = buffList[num]; buffList[num].ImageName = Database.BLACK_CONTRACT; num++;
            player.pbAetherDrive = buffList[num]; buffList[num].ImageName = Database.AETHER_DRIVE; num++;
            player.pbEternalPresence = buffList[num]; buffList[num].ImageName = Database.ETERNAL_PRESENCE; num++;
            player.pbMirrorImage = buffList[num]; buffList[num].ImageName = Database.MIRROR_IMAGE; num++;
            player.pbDeflection = buffList[num]; buffList[num].ImageName = Database.DEFLECTION; num++;
            player.pbTruthVision = buffList[num]; buffList[num].ImageName = Database.TRUTH_VISION; num++;
            player.pbStanceOfFlow = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_FLOW; num++;
            player.pbPromisedKnowledge = buffList[num]; buffList[num].ImageName = Database.PROMISED_KNOWLEDGE; num++;
            player.pbStanceOfDeath = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DEATH; num++;
            player.pbAntiStun = buffList[num]; buffList[num].ImageName = Database.ANTI_STUN; num++;

            player.pbStanceOfEyes = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_EYES; num++;
            player.pbNegate = buffList[num]; buffList[num].ImageName = Database.NEGATE; num++;
            player.pbCounterAttack = buffList[num]; buffList[num].ImageName = Database.COUNTER_ATTACK; num++;
            player.pbStanceOfStanding = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_STANDING; num++;

            player.pbPainfulInsanity = buffList[num]; buffList[num].ImageName = Database.PAINFUL_INSANITY; num++;
            player.pbDamnation = buffList[num]; buffList[num].ImageName = Database.DAMNATION; num++;
            player.pbAbsoluteZero = buffList[num]; buffList[num].ImageName = Database.ABSOLUTE_ZERO; num++;
            player.pbNothingOfNothingness = buffList[num]; buffList[num].ImageName = Database.NOTHING_OF_NOTHINGNESS; num++;

            player.pbPoison = buffList[num]; buffList[num].ImageName = Database.EFFECT_POISON; num++;
            player.pbStun = buffList[num]; buffList[num].ImageName = Database.EFFECT_STUN; num++;
            player.pbSilence = buffList[num]; buffList[num].ImageName = Database.EFFECT_SILENCE; num++;
            player.pbParalyze = buffList[num]; buffList[num].ImageName = Database.EFFECT_PARALYZE; num++;
            player.pbFrozen = buffList[num]; buffList[num].ImageName = Database.EFFECT_FROZEN; num++;
            player.pbTemptation = buffList[num]; buffList[num].ImageName = Database.EFFECT_TEMPTATION; num++;
            player.pbNoResurrection = buffList[num]; buffList[num].ImageName = Database.EFFECT_NORESURRECTION; num++;
            player.pbSlow = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLOW; num++;
            player.pbBlind = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLIND; num++;
            player.pbSlip = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLIP; num++;
            player.pbNoGainLife = buffList[num]; buffList[num].ImageName = Database.EFFECT_NOGAIN_LIFE; num++;

            player.pbPhysicalAttackUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_UP; num++;
            player.pbPhysicalAttackDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_DOWN; num++;
            player.pbPhysicalDefenseUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_UP; num++;
            player.pbPhysicalDefenseDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_DOWN; num++;
            player.pbMagicAttackUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_UP; num++;
            player.pbMagicAttackDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_DOWN; num++;
            player.pbMagicDefenseUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_UP; num++;
            player.pbMagicDefenseDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_DOWN; num++;
            player.pbSpeedUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_UP; num++;
            player.pbSpeedDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_DOWN; num++;
            player.pbReactionUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_UP; num++;
            player.pbReactionDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_DOWN; num++;
            player.pbPotentialUp = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_UP; num++;
            player.pbPotentialDown = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_DOWN; num++;

            player.pbStrengthUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STRENGTH_UP; num++;
            player.pbAgilityUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_AGILITY_UP; num++;
            player.pbIntelligenceUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_INTELLIGENCE_UP; num++;
            player.pbStaminaUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STAMINA_UP; num++;
            player.pbMindUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_MIND_UP; num++;

            player.pbResistLightUp = buffList[num]; buffList[num].ImageName = Database.RESIST_LIGHT_UP; num++;
            player.pbResistShadowUp = buffList[num]; buffList[num].ImageName = Database.RESIST_SHADOW_UP; num++;
            player.pbResistFireUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FIRE_UP; num++;
            player.pbResistIceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_ICE_UP; num++;
            player.pbResistForceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FORCE_UP; num++;
            player.pbResistWillUp = buffList[num]; buffList[num].ImageName = Database.RESIST_WILL_UP; num++;

            player.pbResistStun = buffList[num]; buffList[num].ImageName = Database.RESIST_STUN; num++;
            player.pbResistSilence = buffList[num]; buffList[num].ImageName = Database.RESIST_SILENCE; num++;
            player.pbResistPoison = buffList[num]; buffList[num].ImageName = Database.RESIST_POISON; num++;
            player.pbResistTemptation = buffList[num]; buffList[num].ImageName = Database.RESIST_TEMPTATION; num++;
            player.pbResistFrozen = buffList[num]; buffList[num].ImageName = Database.RESIST_FROZEN; num++;
            player.pbResistParalyze = buffList[num]; buffList[num].ImageName = Database.RESIST_PARALYZE; num++;
            player.pbResistNoResurrection = buffList[num]; buffList[num].ImageName = Database.RESIST_NORESURRECTION; num++;
            player.pbResistSlow = buffList[num]; buffList[num].ImageName = Database.RESIST_SLOW; num++;
            player.pbResistBlind = buffList[num]; buffList[num].ImageName = Database.RESIST_BLIND; num++;
            player.pbResistSlip = buffList[num]; buffList[num].ImageName = Database.RESIST_SLIP; num++;

            player.pbPsychicTrance = buffList[num]; buffList[num].ImageName = Database.PSYCHIC_TRANCE; num++;
            player.pbBlindJustice = buffList[num]; buffList[num].ImageName = Database.BLIND_JUSTICE; num++;
            player.pbTranscendentWish = buffList[num]; buffList[num].ImageName = Database.TRANSCENDENT_WISH; num++;
            player.pbFlashBlaze = buffList[num]; buffList[num].ImageName = Database.FLASH_BLAZE; num++;
            player.pbSkyShield = buffList[num]; buffList[num].ImageName = Database.SKY_SHIELD; num++;
            player.pbEverDroplet = buffList[num]; buffList[num].ImageName = Database.EVER_DROPLET; num++;
            player.pbHolyBreaker = buffList[num]; buffList[num].ImageName = Database.HOLY_BREAKER; num++;
            player.pbStarLightning = buffList[num]; buffList[num].ImageName = Database.STAR_LIGHTNING; num++;
            player.pbBlackFire = buffList[num]; buffList[num].ImageName = Database.BLACK_FIRE; num++;
            player.pbWordOfMalice = buffList[num]; buffList[num].ImageName = Database.WORD_OF_MALICE; num++;
            player.pbDarkenField = buffList[num]; buffList[num].ImageName = Database.DARKEN_FIELD; num++;
            player.pbFrozenAura = buffList[num]; buffList[num].ImageName = Database.FROZEN_AURA; num++;
            player.pbEnrageBlast = buffList[num]; buffList[num].ImageName = Database.ENRAGE_BLAST; num++;
            player.pbImmolate = buffList[num]; buffList[num].ImageName = Database.IMMOLATE; num++;
            player.pbVanishWave = buffList[num]; buffList[num].ImageName = Database.VANISH_WAVE; num++;
            player.pbSeventhMagic = buffList[num]; buffList[num].ImageName = Database.SEVENTH_MAGIC; num++;
            player.pbStanceOfDouble = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DOUBLE; num++;
            player.pbSwiftStep = buffList[num]; buffList[num].ImageName = Database.SWIFT_STEP; num++;
            player.pbColorlessMove = buffList[num]; buffList[num].ImageName = Database.COLORLESS_MOVE; num++;
            player.pbFutureVision = buffList[num]; buffList[num].ImageName = Database.FUTURE_VISION; num++;
            player.pbReflexSpirit = buffList[num]; buffList[num].ImageName = Database.REFLEX_SPIRIT; num++;
            player.pbTrustSilence = buffList[num]; buffList[num].ImageName = Database.TRUST_SILENCE; num++;
            player.pbStanceOfMystic = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_MYSTIC; num++;
            player.pbPreStunning = buffList[num]; buffList[num].ImageName = Database.EFFECT_PRESTUNNING; num++;
            player.pbBlinded = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLINDED; num++;
            player.pbConcussiveHit = buffList[num]; buffList[num].ImageName = Database.CONCUSSIVE_HIT; num++;
            player.pbOnslaughtHit = buffList[num]; buffList[num].ImageName = Database.ONSLAUGHT_HIT; num++;
            player.pbImpulseHit = buffList[num]; buffList[num].ImageName = Database.IMPULSE_HIT; num++;
            player.pbExaltedField = buffList[num]; buffList[num].ImageName = Database.EXALTED_FIELD; num++;
            player.pbRisingAura = buffList[num]; buffList[num].ImageName = Database.RISING_AURA; num++;
            player.pbBlazingField = buffList[num]; buffList[num].ImageName = Database.BLAZING_FIELD; num++;
            player.pbPhantasmalWind = buffList[num]; buffList[num].ImageName = Database.PHANTASMAL_WIND; num++;
            player.pbParadoxImage = buffList[num]; buffList[num].ImageName = Database.PARADOX_IMAGE; num++;
            player.pbStaticBarrier = buffList[num]; buffList[num].ImageName = Database.STATIC_BARRIER; num++;
            player.pbAscensionAura = buffList[num]; buffList[num].ImageName = Database.ASCENSION_AURA; num++;
            player.pbNourishSense = buffList[num]; buffList[num].ImageName = Database.NOURISH_SENSE; num++;
            player.pbVigorSense = buffList[num]; buffList[num].ImageName = Database.VIGOR_SENSE; num++;
            player.pbOneAuthority = buffList[num]; buffList[num].ImageName = Database.ONE_AUTHORITY; num++;

            player.pbSyutyuDanzetsu = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_EIN; num++;
            player.pbJunkanSeiyaku = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_RANA; num++;

            player.pbHymnContract = buffList[num]; buffList[num].ImageName = Database.HYMN_CONTRACT; num++;
            player.pbSigilOfHomura = buffList[num]; buffList[num].ImageName = Database.SIGIL_OF_HOMURA; num++;
            player.pbAusterityMatrix = buffList[num]; buffList[num].ImageName = Database.AUSTERITY_MATRIX; num++;
            player.pbRedDragonWill = buffList[num]; buffList[num].ImageName = Database.RED_DRAGON_WILL; num++;
            player.pbBlueDragonWill = buffList[num]; buffList[num].ImageName = Database.BLUE_DRAGON_WILL; num++;
            player.pbEclipseEnd = buffList[num]; buffList[num].ImageName = Database.ECLIPSE_END; num++;
            player.pbTimeStop = buffList[num]; buffList[num].ImageName = Database.TIME_STOP; num++;
            player.pbSinFortune = buffList[num]; buffList[num].ImageName = Database.SIN_FORTUNE; num++;

            player.pbLightUp = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_UP; num++;
            player.pbLightDown = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_DOWN; num++;
            player.pbShadowUp = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_UP; num++;
            player.pbShadowDown = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_DOWN; num++;
            player.pbFireUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_UP; num++;
            player.pbFireDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_DOWN; num++;
            player.pbIceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_UP; num++;
            player.pbIceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_DOWN; num++;
            player.pbForceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_UP; num++;
            player.pbForceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_DOWN; num++;
            player.pbWillUp = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_UP; num++;
            player.pbWillDown = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_DOWN; num++;

            player.pbAfterReviveHalf = buffList[num]; buffList[num].ImageName = Database.BUFF_DANZAI_KAGO; num++;
            player.pbFireDamage2 = buffList[num]; buffList[num].ImageName = Database.BUFF_FIREDAMAGE2; num++;
            player.pbBlackMagic = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_MAGIC; num++;
            player.pbChaosDesperate = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOS_DESPERATE; num++;

            player.pbFeltus = buffList[num]; buffList[num].ImageName = Database.BUFF_FELTUS; num++;
            player.pbJuzaPhantasmal = buffList[num]; buffList[num].ImageName = Database.BUFF_JUZA_PHANTASMAL; num++;
            player.pbEternalFateRing = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_FATE_RING; num++;
            player.pbLightServant = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_SERVANT; num++;
            player.pbShadowServant = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_SERVANT; num++;
            player.pbAdilBlueBurn = buffList[num]; buffList[num].ImageName = Database.BUFF_ADIL_BLUE_BURN; num++;
            player.pbMazeCube = buffList[num]; buffList[num].ImageName = Database.BUFF_MAZE_CUBE; num++;
            player.pbShadowBible = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_BIBLE; num++;
            player.pbDetachmentOrb = buffList[num]; buffList[num].ImageName = Database.BUFF_DETACHMENT_ORB; num++;
            player.pbDevilSummonerTome = buffList[num]; buffList[num].ImageName = Database.BUFF_DEVIL_SUMMONER_TOME; num++;
            player.pbVoidHymnsonia = buffList[num]; buffList[num].ImageName = Database.BUFF_VOID_HYMNSONIA; num++;

            player.pbIchinaruHomura = buffList[num]; buffList[num].ImageName = Database.BUFF_ICHINARU_HOMURA; num++;
            player.pbAbyssFire = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_FIRE; num++;
            player.pbLightAndShadow = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_AND_SHADOW; num++;
            player.pbEternalDroplet = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_DROPLET; num++;
            player.pbAusterityMatrixOmega = buffList[num]; buffList[num].ImageName = Database.BUFF_AUSTERITY_MATRIX_OMEGA; num++;
            player.pbVoiceOfAbyss = buffList[num]; buffList[num].ImageName = Database.BUFF_VOICE_OF_ABYSS; num++;
            player.pbAbyssWill = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_WILL; num++;
            player.pbTheAbyssWall = buffList[num]; buffList[num].ImageName = Database.BUFF_THE_ABYSS_WALL; num++;

            player.pbSagePotionMini = buffList[num]; buffList[num].ImageName = Database.BUFF_SAGE_POTION_MINI; num++;
            player.pbGenseiTaima = buffList[num]; buffList[num].ImageName = Database.BUFF_GENSEI_TAIMA; num++;
            player.pbShiningAether = buffList[num]; buffList[num].ImageName = Database.BUFF_SHINING_AETHER; num++;
            player.pbBlackElixir = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_ELIXIR; num++;
            player.pbElementalSeal = buffList[num]; buffList[num].ImageName = Database.BUFF_ELEMENTAL_SEAL; num++;
            player.pbColorlessAntidote = buffList[num]; buffList[num].ImageName = Database.BUFF_COLORLESS_ANTIDOTE; num++;

            player.pbLifeCount = buffList[num]; buffList[num].ImageName = Database.BUFF_LIFE_COUNT; num++;
            player.pbChaoticSchema = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOTIC_SCHEMA; num++;
            #endregion
        }

        private void InstantAttackPhase(string BattleActionCommand)
        {
        }
        
        private void ExecActionMethod(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CommandName)
        {
            Debug.Log("ExecActionMethod start");
            // 1. 元核の場合、インスタント消費はせず、スタック情報も用いずにスタックを載せる。
            if ((CommandName == Database.ARCHETYPE_EIN) ||
                (CommandName == Database.ARCHETYPE_RANA) ||
                (CommandName == Database.ARCHETYPE_OL) ||
                (CommandName == Database.ARCHETYPE_VERZE)
                )
            {
                Debug.Log("ExecActionMethod 2");
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                Debug.Log("ExecActionMethod 2-2");
                this.NowStackInTheCommand = true;
            }
            else if (IsPlayerEnemy(player) && (((TruthEnemyCharacter)player).UseStackCommand))
            {
                Debug.Log("ExecActionMethod 3");
                if (UseInstantPoint(player) == false) { return; }
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                Debug.Log("ExecActionMethod 3-2");
                this.NowStackInTheCommand = true;
            }
            else
            {
                if ((GroundOne.DuelMode) ||
                    (this.NowStackInTheCommand))
                {
                    Debug.Log("ExecActionMethod 4");
                    if (UseInstantPoint(player) == false) { return; }
                    player.StackActivePlayer = player;
                    player.StackTarget = target;
                    player.StackPlayerAction = PA;
                    player.StackCommandString = CommandName;
                    player.StackActivation = true;
                    Debug.Log("ExecActionMethod 4-2");
                    this.NowStackInTheCommand = true;
                }
                else
                {
                    Debug.Log("ExecActionMethod 5");
                    if (UseInstantPoint(player) == false) { return; }
                    //PlayerAttackPhase(player, target, PA, CommandName, false, false, false);
                    //CompleteInstantAction();
                }
            }
        }

        protected bool PlayerAttackPhase(MainCharacter player, bool withoutCost, bool skipStanceDouble, bool mainPhase)
        {
            return true;
        }


        private bool UseInstantPoint(MainCharacter player)
        {
            if (player.CurrentInstantPoint <= 0)
            {
                // インスタントポイントが既に０の場合、何もしない
                return false;
            }

            player.CurrentInstantPoint = 0;
            UpdateInstantPoint(player);
            return true;
        }
        private void UseSpecialInstant(MainCharacter player)
        {
            player.CurrentSpecialInstant = 0;
            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
            }
        }
        private void UpdatePlayerInstantPoint(MainCharacter player)
        {
            if (player.CurrentFrozen > 0)
            {
                return;
            }
            if (player.CurrentStunning > 0)
            {
                return;
            }
            if (player.CurrentParalyze > 0)
            {
                return;
            }
            if (player.CurrentStarLightning > 0)
            {
                return;
            }

            if (player.CurrentInstantPoint < player.MaxInstantPoint)
            {
                player.CurrentInstantPoint += (int)PrimaryLogic.BattleResponseValue(player, GroundOne.DuelMode);
            }
            UpdateInstantPoint(player);

            if (player.CurrentSpecialInstant < player.MaxSpecialInstant)
            {
                player.CurrentSpecialInstant += PrimaryLogic.BattleResponseValue(player, GroundOne.DuelMode);
            }
            UpdateSpecialInstantPoint(player);
        }

        private void UpdatePlayerGaugePosition(MainCharacter player)
        {
            double movement = PrimaryLogic.BattleSpeedValue(player, GroundOne.DuelMode);

            player.BattleBarPos += movement;
            if (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH)
            {
                player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH + 1;
            }
        }

        private void UpdatePlayerNextDecision(MainCharacter player)
        {
            if (player == GroundOne.P1 || player == GroundOne.P2 || player == GroundOne.P3) return; // コンピューター専用ルーチンのため、プレイヤー側は何もしない。

            if (player.FirstName == Database.DUEL_OL_LANDIS) // オル・ランディスは常に戦術を変更可能とする。ヴェルゼなど主要人物は全て該当。
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P1, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
            }
            else if (player.FirstName == Database.VERZE_ARTIE_FULL || player.FirstName == Database.VERZE_ARTIE
                  || player.FirstName == Database.ENEMY_LAST_RANA_AMILIA
                  || player.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ
                  || player.FirstName == Database.ENEMY_LAST_OL_LANDIS
                  || player.FirstName == Database.ENEMY_LAST_VERZE_ARTIE
                  || player.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P1, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
            }
            else if ((player.FirstName == Database.DUEL_SHUVALTZ_FLORE) ||
                     (player.FirstName == Database.DUEL_SIN_OSCURETE))
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P1, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
            }
            else
            {
                if ((!player.ActionDecision && player.BattleBarPos > player.DecisionTiming) ||
                    ((TruthEnemyCharacter)player).FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    player.ActionDecision = true;

                    if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
                    {
                        if (GroundOne.P1 != null && !GroundOne.P1.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P1, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                        else if (GroundOne.P2 != null && !GroundOne.P2.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P2, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                        else if (GroundOne.P3 != null && !GroundOne.P3.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P3, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
                    {
                        if (GroundOne.WE.AvailableThirdCharacter && GroundOne.P3 != null && !GroundOne.P3.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P3, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                        else if (GroundOne.WE.AvailableSecondCharacter && GroundOne.P2 != null && !GroundOne.P2.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P2, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                        else if (GroundOne.P1 != null && !GroundOne.P1.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.P1, GroundOne.P1, GroundOne.P2, GroundOne.P3, ec1, ec2, ec3);
                        }
                    }
                }
            }
        }

        private void UpdatePlayerDoStackInTheCommand(MainCharacter player)
        {
            if (IsPlayerAlly(player)) { return; } // 味方プレイヤーは自動的に何らかの行動は行わない。
            if (this.NowStackInTheCommand) { return; } // スタック・イン・ザ・コマンド中はスルー
        }

        private void UpdatePlayerPreCondition(MainCharacter player, int arrowType)
        {
            if (arrowType == 0) { player.BattleBarPos = 0; }
            else if (arrowType == 1) { player.BattleBarPos2 = 0; }
            else if (arrowType == 2) { player.BattleBarPos3 = 0; }
            Vector3 current = player.MainFaceArrow.transform.position;
            player.MainFaceArrow.transform.position = new Vector3((float)player.BattleBarPos, current.y, current.z);

            player.ActionDecision = false;
            // player.CurrentCounterAttack = false; // 次のコマンドを実行したらカウンターが消滅してしまうのはゲーム性質上、おもしろくない。
        }


        private void ExpGoldDisplay(int exp, int gold)
        {
            ExpGoldText.text = ec1.Exp + "の経験値、 " + ec1.Gold + "Goldを獲得";
            ExpGoldPanel.SetActive(true);
        }

        private void MessageDisplayWithIcon(ItemBackPack item)
        {
            if (item != null)
            {
                treasureText.text = "【 " + item.Name + " 】を入手！";

                switch (item.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        treasurePanel.GetComponent<Image>().color = Color.gray;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        treasurePanel.GetComponent<Image>().color = UnityColor.CommonGreen;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        treasurePanel.GetComponent<Image>().color = UnityColor.DarkBlue;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        treasurePanel.GetComponent<Image>().color = UnityColor.Purple;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // 後編追加
                        treasurePanel.GetComponent<Image>().color = UnityColor.OrangeRed;
                        break;
                    default:
                        treasurePanel.GetComponent<Image>().color = Color.gray;
                        break;
                }

                Texture2D current = Resources.Load<Texture2D>("ItemIcon");
                int BASE_SIZE = 49;
                int locX = 0;
                int locY = 0;
                switch (item.Type)
                {
                    case ItemBackPack.ItemType.Weapon_TwoHand:
                        locX = 1; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Light:
                        locX = 2; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Heavy:
                    case ItemBackPack.ItemType.Weapon_Middle:
                        locX = 0; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Rod:
                        locX = 3; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Accessory:
                        locX = 0; locY = 3;
                        break;
                    case ItemBackPack.ItemType.Armor_Heavy:
                    case ItemBackPack.ItemType.Armor_Middle:
                        locX = 1; locY = 1;
                        break;
                    case ItemBackPack.ItemType.Armor_Light:
                        locX = 2; locY = 1;
                        break;
                    //case ItemBackPack.ItemType.Robe:
                    //    locX = 3; locY = 1;
                    //    break;
                    case ItemBackPack.ItemType.Material_Equip:
                    case ItemBackPack.ItemType.Material_Food:
                    case ItemBackPack.ItemType.Material_Potion:
                        locX = 0; locY = 0;
                        break;
                    case ItemBackPack.ItemType.Shield:
                        locX = 0; locY = 1;
                        break;
                    case ItemBackPack.ItemType.Use_Item:
                        locX = 2; locY = 3;
                        break;
                    case ItemBackPack.ItemType.Use_Potion:
                        locX = 1; locY = 0;
                        break;
                    case ItemBackPack.ItemType.Useless:
                        locX = 2; locY = 0;
                        break;
                    case ItemBackPack.ItemType.None:
                        locX = 2; locY = 0;
                        //pictureBox1.Visible = false;
                        break;
                    default:
                        locX = 2; locY = 0;
                        break;
                }
                treasureIcon.sprite = Sprite.Create(current, new Rect(BASE_SIZE * locX, BASE_SIZE * locY, BASE_SIZE, BASE_SIZE), new Vector2(0, 0));
            }
        }



        public enum MethodType
        {
            Beginning,
            AfterBattleEffect,
            // UpdateTurnEnd,
            CleanUpStep,
            UpKeepStep,
            //UpdateUseItemGauge,
            PlayerAttackPhase,
            // UpdatePlayerTarget,
            // UpdatePlayerInstantPoint,
            // UpdatePlayerGaugePosition,
            // UpdatePlayerNextDecision,
            // UpdatePlayerDoStackInTheCommand,
            // UpdatePlayerDeadFlag,
            CleanUpForBoss,
            TimeStopEnd,
        }
        private bool ExecPhaseElement(MethodType method, MainCharacter player)
        {
            switch (method)
            {
                case MethodType.Beginning:
                    Beginning();
                    break;
                case MethodType.AfterBattleEffect:
                    AfterBattleEffect();
                    break;
                case MethodType.CleanUpStep:
                    CleanUpStep();
                    break;
                case MethodType.UpKeepStep:
                    UpkeepStep();
                    break;
                case MethodType.PlayerAttackPhase:
                    Debug.Log("PAPhase: " + player.FirstName + " to " + player.Target.FirstName);
                    PlayerAttackPhase(player, false, false, true);
                    break;
                case MethodType.CleanUpForBoss:
                    CleanUpForBoss();
                    break;
                case MethodType.TimeStopEnd:
                    TimeStopEnd();
                    break;
            }
            if (UpdatePlayerDeadFlag()) return false;

            return true;
        }

        private void TimeStopEnd()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                for (int jj = 0; jj < ActiveList[ii].ActionCommandStackList.Count; jj++)
                {
                    ExecActionMethod(ActiveList[ii], ActiveList[ii].ActionCommandStackTarget[jj], TruthActionCommand.CheckPlayerActionFromString(ActiveList[ii].ActionCommandStackList[jj]), ActiveList[ii].ActionCommandStackList[jj]);
                }
                ActiveList[ii].ActionCommandStackList.Clear();
                ActiveList[ii].ActionCommandStackTarget.Clear();
            }

            this.Background.GetComponent<Image>().color = UnityColor.White;
            this.TimeStopText.gameObject.SetActive(false);
            this.labelBattleTurn.color = Color.black;
            this.TimeSpeedLabel.color = Color.black;
            this.lblTimerCount.color = Color.black;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                BackToNormalColor(ActiveList[ii]);
            }
        }

        private void GoToTimeStopColor(MainCharacter player)
        {
            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelCurrentLifePoint.color = UnityColor.Lightgreen;
            }
            else
            {
                player.labelCurrentLifePoint.color = Color.white;
            }
        }
        private void BackToNormalColor(MainCharacter player)
        {
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                {
                    player.labelName.color = UnityColor.DarkOrange;
                    player.labelCurrentInstantPoint.color = UnityColor.Gold;
                }
                else
                {
                    player.labelName.color = Color.black;
                }
            }
            else
            {
                player.labelName.color = Color.black;
            }

            player.ActionLabel.color = Color.black;
            player.CriticalLabel.color = Color.black;
            player.DamageLabel.color = Color.black;
            player.BuffPanel.GetComponent<Image>().color = UnityColor.GhostWhite;

            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelCurrentLifePoint.color = Color.green;
            }
            else
            {
                player.labelCurrentLifePoint.color = Color.black;
            }
        }

        private void CleanUpForBoss()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (IsPlayerEnemy(ActiveList[ii]))
                {
                    if ((((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss5) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.LastBoss) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss5))
                    {
                        ((TruthEnemyCharacter)ActiveList[ii]).CleanUpEffectForBoss();
                    }
                }
            }
        }

        private void UpkeepStep()
        {
        }

        private int CurrentTimeStop = 0; // [後編必須]タイムストップを後編専用で書き直してください。本フラグは不要です。
        private void CleanUpStep()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                ActiveList[ii].CleanUpEffect(false, false);
            }

            // 前編からの引継ぎには不足してるコーディング。書き直し必要。
            if (CurrentTimeStop > 0)
            {
                CurrentTimeStop--;
            }
        }

        private void AfterBattleEffect()
        {
        }

        private void Beginning()
        {
        }

        private void ItemEffect(MainCharacter player, ItemBackPack item, MethodType methodType)
        {
            if (player != null && player.Dead) { return; }

            // アイテム効果はAfterBattleEffectやUpkeepではターン終了時なので、全ての効果を発動してもよいが
            // Beginningフェーズでは、敵を対象とする効果が戦闘開始時に発動してはならない。
            // Beginningフェーズでは装備者本人に発動されるものだけが発動対象となる。
            if (item != null)
            {
            }
        }

        private void UpdateUseItemGauge()
        {
            // ゲージバー値を更新
            if (currentItemGauge < MAX_ITEM_GAUGE)
            {
                currentItemGauge++;
            }

            // ゲージバー表示更新
            float dx = (float)currentItemGauge / (float)MAX_ITEM_GAUGE;
            UseItemText.text = currentItemGauge.ToString();
            UseItemGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
        }

        private void UpdateTurnEnd(bool cancelCounterClear = false)
        {
            this.BattleTurnCount++;
            this.labelBattleTurn.text = "Turn " + BattleTurnCount.ToString();
            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                this.labelBattleTurn.text = Database.GUI_BATTLE_TURN + " " + BattleTurnCount.ToString();
            }
            if (cancelCounterClear == false) { this.BattleTimeCounter = 0; }
        }

        private void StackInTheCommandEnd()
        {
            if (this.StackNumber > 0)
            {
                this.stackActivePlayer.RemoveAt(this.stackActivePlayer.Count - 1);
                this.cumulativeCounter.RemoveAt(this.cumulativeCounter.Count - 1);
                this.StackNumber--;
            }
            else
            {
                this.StackNumber = -1;
                this.stackActivePlayer.Clear();
                this.cumulativeCounter.Clear();
                this.NowStackInTheCommand = false;
            }

        }

        private bool PlayerPartyDeathCheck()
        {
            // そのロジック、イマイチだが良しとする。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == ec1 ||
                        ActiveList[ii] == ec2 ||
                        ActiveList[ii] == ec3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool EnemyPartyDeathCheck()
        {
            // そのロジック、イマイチだが良しとする。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == GroundOne.P1 ||
                        ActiveList[ii] == GroundOne.P2 ||
                        ActiveList[ii] == GroundOne.P3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// カウンター行為が発動成功するかどうかを判定するメソッド
        /// </summary>
        /// <param name="player">対象元プレイヤー</param>
        /// <param name="target">対象先ターゲット</param>
        /// <param name="messageNumber">キャラクターセリフ番号</param>
        /// <returns>カウンター成功ならTrue、失敗ならFalse</returns>
        private bool JudgeSuccessOfCounter(MainCharacter player, MainCharacter target, int messageNumber, ref string factor)
        {
            if (player.PA == MainCharacter.PlayerAction.UseSpell && (TruthActionCommand.CannotBeCountered(player.CurrentSpellName)) ||
                     player.StackPlayerAction == MainCharacter.PlayerAction.UseSpell && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSpellName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.PA == MainCharacter.PlayerAction.UseSkill && (TruthActionCommand.CannotBeCountered(player.CurrentSkillName)) ||
                     player.StackPlayerAction == MainCharacter.PlayerAction.UseSkill && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSkillName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.PA == MainCharacter.PlayerAction.Archetype && (TruthActionCommand.CannotBeCountered(player.CurrentArchetypeName)) ||
                player.StackPlayerAction == MainCharacter.PlayerAction.Archetype && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentArchetypeName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + player.FirstName + "は無効化を無効にするオーラによって護られている！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = Database.NOTHING_OF_NOTHINGNESS;
                return false;
            }
            else if (player.CurrentHymnContract > 0)
            {
                UpdateBattleText(player.FirstName + "は天使の契約により保護されており、カウンターを無視した！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = Database.HYMN_CONTRACT;
                return false;
            }
            else
            {
                UpdateBattleText(target.GetCharacterSentence(messageNumber));
                AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_COUNTER);
                factor = String.Empty;
                return true;
            }
        }

        private bool IsPlayerEnemy(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == ec1) || (player == ec2) || (player == ec3))
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerAlly(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == GroundOne.P1) || (player == GroundOne.P2) || (player == GroundOne.P3))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// プレイヤーとターゲットはパーティメンバーか敵メンバーかを判別するメソッド
        /// </summary>
        /// <param name="player">対象元プレイヤー</param>
        /// <param name="target">対象先ターゲット</param>
        /// <returns>敵メンバーならTrue、味方メンバーならFalse</returns>
        private bool DetectOpponentParty(MainCharacter player, MainCharacter target)
        {
            // 可読性より、演算論理集約を重視した記述
            if (((player == ec1 || player == ec2 || player == ec3) &&
                 (target == GroundOne.P1 || target == GroundOne.P2 || target == GroundOne.P3))
                 ||
                 ((player == GroundOne.P1 || player == GroundOne.P2 || player == GroundOne.P3) &&
                  (target == ec1 || target == ec2 || target == ec3)))
            {
                return true;
            }
            return false;
        }

        private bool CheckBattlePlaying()
        {
            if (tempStopFlag)
            {
                return true;
            }
            return false;
        }

        private bool CheckInstantTarget(string BattleActionCommand)
        {
            if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
            {
                return true;
            }
            return false;
        }

        private bool CheckNotInstant(string BattleActionCommand)
        {
            // [警告] 武器、アクセサリは常にインスタントで良いのか？
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL2_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_LEFT_EN) return false;

            if (TruthActionCommand.GetTimingType(BattleActionCommand) != TruthActionCommand.TimingType.Instant)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 全滅しているかどうかを判定する。（敵・味方含む）
        /// </summary>
        /// <returns>true:全滅している
        ///          false:全滅していない</returns>
        private bool UpdatePlayerDeadFlag()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList[ii].CurrentLife <= 0)
                {
                    ActiveList[ii].DeadPlayer();
                }
            }

            if (PlayerPartyDeathCheck() || EnemyPartyDeathCheck())
            {
                this.BattleEndFlag = true;
                return true;
            }
            else
            {
                return false;
            }
        }
                        
        public void BattleStart_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_BATTLE_START, String.Empty, String.Empty);
            string NOW_BATTLE = "Now Battle...";
            string NOW_DUEL = "Now Duel...";
            string NOW_STOP = "Battle Stop";
            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                NOW_BATTLE = Database.GUI_BATTLE_RUN;
                NOW_DUEL = Database.GUI_BATTLE_RUNDUEL;
                NOW_STOP = Database.GUI_BATTLE_STOP;
            }

            if ((BattleStart.text == NOW_BATTLE) ||
                (BattleStart.text == NOW_DUEL))
            {
                if (GroundOne.DuelMode)
                {
                    // DUELでは途中一旦停止は出来ない事とする。
                }
                else
                {
                    BattleStart.text = NOW_STOP;
                    tempStopFlag = true;
                }
            }
            else
            {
                if (GroundOne.TutorialMode)
                {
                    back_TutorialMessage.gameObject.SetActive(false);
                }

                BattleStart.text = NOW_BATTLE;
                tempStopFlag = false;
                gameStart = true;
            }
        }

        private void UpdateBattleText(string text)
        {
            if (txtBattleMessage != null)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, text);
            }
        }

        public void tapBattleClose()
        {
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }

        public void tapPanel1()
        {
            this.currentPlayer = GroundOne.P1;
            GroundOne.P1.EnableGUI();
            GroundOne.P2.DisableGUI();
            GroundOne.P3.DisableGUI();
        }
        public void tapPanel2()
        {
            this.currentPlayer = GroundOne.P2;
            GroundOne.P1.DisableGUI();
            GroundOne.P2.EnableGUI();
            GroundOne.P3.DisableGUI();
        }
        public void tapPanel3()
        {
            this.currentPlayer = GroundOne.P3;
            GroundOne.P1.DisableGUI();
            GroundOne.P2.DisableGUI();
            GroundOne.P3.EnableGUI();
        }
        public void PointerDownPanel()
        {
            if (GroundOne.WE.AvailableInstantCommand)
            {
                if (this.ActionInstantMode == false)
                {
                    this.ActionInstantMode = true;
                    InstantModePanel.SetActive(true);
                    if (GroundOne.WE.AvailableFirstCharacter && GroundOne.P1 != null)
                    {
                        GroundOne.P1.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                        //GroundOne.P1.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                    }
                    if (GroundOne.WE.AvailableSecondCharacter && GroundOne.P2 != null)
                    {
                        GroundOne.P2.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                        //GroundOne.P2.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                    }
                    if (GroundOne.WE.AvailableThirdCharacter && GroundOne.P3 != null)
                    {
                        GroundOne.P3.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                        //GroundOne.P3.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                    }
                }
            }
        }
        public void PointerUpPanel()
        {
            if (this.ActionInstantMode)
            {
                this.ActionInstantMode = false;
                InstantModePanel.SetActive(false);
                if (GroundOne.WE.AvailableFirstCharacter && GroundOne.P1 != null)
                {
                    GroundOne.P1.ActionButtonPanel.GetComponent<Image>().color = new Color(GroundOne.P1.PlayerBattleColor.r,
                                                                                           GroundOne.P1.PlayerBattleColor.g,
                                                                                           GroundOne.P1.PlayerBattleColor.b, 1);
                    SelectPlayerPanelColor(GroundOne.P1);
                }
                if (GroundOne.WE.AvailableSecondCharacter && GroundOne.P2 != null)
                {
                    GroundOne.P2.ActionButtonPanel.GetComponent<Image>().color = new Color(GroundOne.P2.PlayerBattleColor.r,
                                                                                           GroundOne.P2.PlayerBattleColor.g,
                                                                                           GroundOne.P2.PlayerBattleColor.b, 1);
                    SelectPlayerPanelColor(GroundOne.P2);
                }
                if (GroundOne.WE.AvailableThirdCharacter && GroundOne.P3 != null)
                {
                    GroundOne.P3.ActionButtonPanel.GetComponent<Image>().color = new Color(GroundOne.P3.PlayerBattleColor.r,
                                                                                           GroundOne.P3.PlayerBattleColor.g,
                                                                                           GroundOne.P3.PlayerBattleColor.b, 1);
                    SelectPlayerPanelColor(GroundOne.P3);
                }
            }
        }

        public void tapFirstChara()
        {
            this.currentPlayer.Target2 = GroundOne.P1;
        }
        public void tapSecondChara()
        {
            this.currentPlayer.Target2 = GroundOne.P2;
        }
        public void tapThirdChara()
        {
            this.currentPlayer.Target2 = GroundOne.P3;
        }
        public void tapFirstCharaAction()
        {
            ChoiceFirstChara();
        }
        public void tapSecondCharaAction()
        {
            ChoiceSecondChara();
        }
        public void tapThirdCharaAction()
        {
            ChoiceThirdChara();
        }

        private void ChoiceFirstChara()
        {
            GroundOne.P1.ActionButtonPanel.SetActive(true);
            GroundOne.P2.ActionButtonPanel.SetActive(false);
            GroundOne.P3.ActionButtonPanel.SetActive(false);
            PointerDownPanel();
        }
        private void ChoiceSecondChara()
        {
            GroundOne.P1.ActionButtonPanel.SetActive(false);
            GroundOne.P2.ActionButtonPanel.SetActive(true);
            GroundOne.P3.ActionButtonPanel.SetActive(false);
            PointerDownPanel();
        }
        private void ChoiceThirdChara()
        {
            GroundOne.P1.ActionButtonPanel.SetActive(false);
            GroundOne.P2.ActionButtonPanel.SetActive(false);
            GroundOne.P3.ActionButtonPanel.SetActive(true);
            PointerDownPanel();
        }

        // 通常攻撃を抽象化したロジック。通常攻撃やストレートスマッシュは全てここに含まれる。
        private void AbstractMagicAttack(MainCharacter player, MainCharacter target, string command, double value)
        {
        }
        // 通常攻撃
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense, bool ignoreDoubleAttack)
        {
            return PlayerNormalAttack(player, target, magnification, 0, ignoreDefense, false, 0, 0, string.Empty, -1, ignoreDoubleAttack, CriticalType.Random);
        }
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, int crushingBlow, bool ignoreDefense, bool skipCounterPhase, double atkBase, int interval, string soundName, int textNumber, bool ignoreDoubleAttack, CriticalType critical)
        {
            return true;
        }

        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            return AbstractMagicDamage(player, target, interval, ref damage, magnification, soundName, messageNumber, magicType, ignoreTargetDefense, critical);
        }
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, ref double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            return true;
        }
        // 魔法攻撃
        private void PlayerMagicAttack(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.None, false, CriticalType.Random);
        }
        
        private void UpdateLife(MainCharacter player, double damage, bool plusValue, bool animationDamage, int interval, bool critical)
        {
            UpdateLife(player);

            if (animationDamage)
            {
                Color color = Color.black;
                if (plusValue)
                {
                    color = new Color(0, 0.7f, 0);
                    if (this.NowTimeStop)
                    {
                        color = UnityColor.Lightgreen;
                    }
                }
                else if (this.NowTimeStop)
                {
                    color = Color.white;
                }
                AnimationDamage(damage, player, interval, color, false, critical, String.Empty);
            }
        }
        private void UpdateLife(MainCharacter player)
        {
            float dx = (float)player.CurrentLife / (float)player.MaxLife;
            if (player.labelCurrentLifePoint != null)
            {
                player.labelCurrentLifePoint.text = player.CurrentLife.ToString();
            }
            if (player.meterCurrentLifePoint != null)
            {
                player.meterCurrentLifePoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }

            // 色付け
            if (player.labelCurrentLifePoint != null)
            {
                if (player.CurrentLife >= player.MaxLife)
                {
                    player.labelCurrentLifePoint.color = UnityColor.ForestGreen;
                    if (this.NowTimeStop)
                    {
                        player.labelCurrentLifePoint.color = UnityColor.Lightgreen;
                    }
                }
                else
                {
                    player.labelCurrentLifePoint.color = Color.black;
                    if (this.NowTimeStop)
                    {
                        player.labelCurrentLifePoint.color = Color.white;
                    }
                }
            }
        }

        private void UpdateMana(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            UpdateMana(player);
            if (animationDamage)
            {
                AnimationDamage(effectValue, player, interval, Color.blue, false, false, String.Empty);
            }
        }
        private void UpdateMana(MainCharacter player)
        {
            float dx = (float)player.CurrentMana / (float)player.MaxMana;
            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            }
            if (player.meterCurrentManaPoint != null)
            {
                player.meterCurrentManaPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }
        private void UpdateSkillPoint(MainCharacter player)
        {
            UpdateSkillPoint(player, 0, false, false, 0);
        }
        private void UpdateSkillPoint(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            float dx = (float)player.CurrentSkillPoint / (float)player.MaxSkillPoint;
            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            }
            if (player.meterCurrentSkillPoint != null)
            {
                player.meterCurrentSkillPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }

            if (animationDamage)
            {
                AnimationDamage(effectValue, player, interval, UnityColor.DarkGreen, false, false, String.Empty);
            }
        }
        private void UpdateInstantPoint(MainCharacter player)
        {
            float dx = (float)player.CurrentInstantPoint / (float)player.MaxInstantPoint;
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.text = player.CurrentInstantPoint.ToString();
            }
            if (player.meterCurrentInstantPoint != null)
            {
                player.meterCurrentInstantPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }
        private void UpdateSpecialInstantPoint(MainCharacter player)
        {
            float dx = (float)player.CurrentSpecialInstant / (float)player.MaxSpecialInstant;
            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = ((int)(player.CurrentSpecialInstant)).ToString();
            }
            if (player.meterCurrentSpecialInstant != null)
            {
                player.meterCurrentSpecialInstant.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }

        public void UseItemButton_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_USEITEM, String.Empty, String.Empty);
            groupParentBackpack.SetActive(!groupParentBackpack.activeInHierarchy);
        }

        public void BattleLog_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_BATTLE_LOG, String.Empty, String.Empty);
            groupBattleLog.SetActive(!groupBattleLog.activeInHierarchy);
        }

        public void RunAwayButton_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_RUNAWAY, String.Empty, String.Empty);
            if (this.cannotRunAway)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは今逃げられない状態にいる。\r\n");
                return;
            }

            if (GroundOne.DuelMode)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは降参を宣言した。\r\n");
                this.BattleEndFlag = true;
                this.RunAwayFlag = true;
                BattleEndPhase();
                //System.Threading.Thread.Sleep(200);
                return;
            }

            txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは逃げ出した。\r\n");
            this.BattleEndFlag = true;
            this.RunAwayFlag = true;
            BattleEndPhase();
            //System.Threading.Thread.Sleep(200);
        }

        public void GameOverYes_Click()
        {
            GroundOne.BattleResult = GroundOne.battleResult.Retry;
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }
        public void GameOverNo_Click()
        {
            GroundOne.BattleResult = GroundOne.battleResult.Ignore;
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }

        private void AnimationDamage(double damage, MainCharacter target, int interval, Color plusValue, bool avoid, bool critical, string customString)
        {
            Debug.Log("AnimationDamage start: " + this.nowAnimationCounter);
            target.DamageLabel.text = ((int)damage).ToString();
            this.nowAnimationTarget.Add(target);
            this.nowAnimationDamage.Add((int)damage);
            this.nowAnimationColor.Add(plusValue);
            this.nowAnimationAvoid.Add(avoid);
            this.nowAnimationCustomString.Add(customString);
            this.nowAnimationInterval.Add(interval);
            this.nowAnimationCritical.Add(critical);
            this.nowAnimationText.Add(target.DamageLabel);
            this.nowAnimationCurrentLife.Add(target.CurrentLife);
            this.nowAnimation = true;
        }

        Vector3 ExecAnimation_basePoint;
        Vector3 ExecAnimation_basePointCritical;
        private void ExecAnimation()
        {
            Text targetLabel = this.nowAnimationTarget[0].DamageLabel;
            Text targetCriticalLabel = this.nowAnimationTarget[0].CriticalLabel;

            targetLabel.color = this.nowAnimationColor[0];

            if (this.nowAnimationAvoid[0])
            {
                targetLabel.text = Database.MISS;
            }
            else if (this.nowAnimationCustomString[0] != string.Empty)
            {
                targetLabel.text = this.nowAnimationCustomString[0];
            }
            else
            {
                targetLabel.text = Convert.ToString(this.nowAnimationDamage[0]);
            }

            int[] waitTime = { 50, 45, 40, 35, 30, 25, 20, 15, 10 };

            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                waitTime[0] = 17; waitTime[1] = 15; waitTime[2] = 13;
                waitTime[3] = 11; waitTime[4] = 9; waitTime[5] = 7;
                waitTime[6] = 5; waitTime[7] = 3; waitTime[8] = 1;
            }
            int wait = waitTime[this.BattleSpeed - 1];

            if (GroundOne.HiSpeedAnimation) { wait = wait / 2; }
            if (this.nowAnimationInterval[0] > 0) wait = this.nowAnimationInterval[0];

            if (this.nowAnimationCounter <= 0)
            {
                if (this.nowAnimationCritical[0])
                {
                    targetLabel.fontSize = targetLabel.fontSize + 4;
                    targetCriticalLabel.text = "Critical";
                    targetCriticalLabel.gameObject.SetActive(true);
                }
                else
                {
                    targetCriticalLabel.text = "";
                    targetCriticalLabel.gameObject.SetActive(true);
                }

                this.nowAnimationTarget[0].DamagePanel.gameObject.SetActive(true);
                this.nowAnimationTarget[0].DamageLabel.gameObject.SetActive(true);

                ExecAnimation_basePoint = targetLabel.transform.position;
                ExecAnimation_basePointCritical = targetCriticalLabel.transform.position;

                // 現在ライフの表示をここで更新
                float dx = (float)nowAnimationCurrentLife[0] / (float)nowAnimationTarget[0].MaxLife;
                if (nowAnimationTarget[0].labelCurrentLifePoint != null)
                {
                    nowAnimationTarget[0].labelCurrentLifePoint.text = ((int)(nowAnimationCurrentLife[0])).ToString();
                }
                if (nowAnimationTarget[0].meterCurrentLifePoint != null)
                {
                    nowAnimationTarget[0].meterCurrentLifePoint.rectTransform.localScale = new Vector2(dx, 1.0f);
                }

                // 色付け
                if (nowAnimationTarget[0].labelCurrentLifePoint != null)
                {
                    if (nowAnimationCurrentLife[0] >= nowAnimationTarget[0].MaxLife)
                    {
                        nowAnimationTarget[0].labelCurrentLifePoint.color = UnityColor.ForestGreen;
                        if (this.NowTimeStop)
                        {
                            nowAnimationTarget[0].labelCurrentLifePoint.color = UnityColor.Lightgreen;
                        }
                    }
                    else
                    {
                        nowAnimationTarget[0].labelCurrentLifePoint.color = Color.black;
                        if (this.NowTimeStop)
                        {
                            nowAnimationTarget[0].labelCurrentLifePoint.color = Color.white;
                        }
                    }
                }
            }

            int movement = 1;
            if (this.nowAnimationCounter > 10) { movement = 0; }
            targetLabel.transform.position = new Vector3(targetLabel.transform.position.x + movement, targetLabel.transform.position.y, targetLabel.transform.position.z);
            targetCriticalLabel.transform.position = new Vector3(targetCriticalLabel.transform.position.x + movement, targetCriticalLabel.transform.position.y, targetCriticalLabel.transform.position.z);
            System.Threading.Thread.Sleep(5);

            this.nowAnimationCounter++;
            if (this.nowAnimationCounter > wait)
            {
                targetLabel.gameObject.SetActive(false);
                targetLabel.transform.position = ExecAnimation_basePoint;

                targetCriticalLabel.gameObject.SetActive(false);
                targetCriticalLabel.transform.position = ExecAnimation_basePointCritical;

                if (this.nowAnimationCritical[0])
                {
                    targetLabel.fontSize = targetLabel.fontSize - 4;
                }

                if (this.nowAnimationTarget.Count > 0)
                {
                    if (this.nowAnimationTarget.Count == 1)
                    {
                        UpdateLife(this.nowAnimationTarget[0]);
                    }

                    this.nowAnimationTarget.RemoveAt(0);
                    this.nowAnimationDamage.RemoveAt(0);
                    this.nowAnimationColor.RemoveAt(0);
                    this.nowAnimationAvoid.RemoveAt(0);
                    this.nowAnimationCustomString.RemoveAt(0);
                    this.nowAnimationInterval.RemoveAt(0);
                    this.nowAnimationCritical.RemoveAt(0);
                    this.nowAnimationText.RemoveAt(0);
                    this.nowAnimationCurrentLife.RemoveAt(0);
                    this.nowAnimationCounter = 0;
                }

                if (this.nowAnimationTarget.Count <= 0)
                {
                    this.nowAnimationTarget.Clear();
                    this.nowAnimationDamage.Clear();
                    this.nowAnimationColor.Clear();
                    this.nowAnimationAvoid.Clear();
                    this.nowAnimationCustomString.Clear();
                    this.nowAnimationInterval.Clear();
                    this.nowAnimationCritical.Clear();
                    this.nowAnimationText.Clear();
                    this.nowAnimationCurrentLife.Clear();
                    this.nowAnimationCounter = 0;
                    this.nowAnimation = false;
                }
            }
        }

        private void ExecStackAnimation()
        {
            int waitTime = 150;
            this.nowStackAnimationCounter++;
            if (this.nowStackAnimationCounter > waitTime)
            {
                this.back_nowStackAnimationBar.transform.localScale = new Vector2(0.0f, 1.0f);
                this.back_nowStackAnimationName.transform.localScale = new Vector2(0.0f, 1.0f);
                this.nowStackAnimation = false;
                this.nowStackAnimationCounter = 0;
                StackInTheCommandEnd();
            }
        }

        private void AnimationMessageFadeOut(string message)
        {
            MatrixDragonTalkText.color = Color.white;
            MatrixDragonTalkText.text = message;

            this.nowAnimationMatrixTalkCounter = 0;
            this.nowAnimationMatrixTalk = true;
        }
        private void ExecAnimationMessageFadeOut()
        {
            if (this.nowAnimationMatrixTalkCounter <= 0)
            {
                back_MatrixDragonTalk.gameObject.SetActive(true);
            }

            int waitTime = 258;
            System.Threading.Thread.Sleep(10);
            if (this.nowAnimationMatrixTalkCounter > 130)
            {
                this.MatrixDragonTalkText.color = new Color(this.MatrixDragonTalkText.color.r, this.MatrixDragonTalkText.color.g, this.MatrixDragonTalkText.color.b, this.MatrixDragonTalkText.color.a - (2.0f / 255.0f));
            }

            this.nowAnimationMatrixTalkCounter++;

            if (this.nowAnimationMatrixTalkCounter > waitTime)
            {
                back_MatrixDragonTalk.gameObject.SetActive(false);
                this.nowAnimationMatrixTalkCounter = 0;
                this.nowAnimationMatrixTalk = false;
            }
        }

        private void AnimationSandGlass()
        {
            this.nowAnimationSandGlassCounter = 0;
            this.nowAnimationSandGlass = true;
        }

        float angle = 0.0f;
        private void ExecAnimationSandGlass()
        {
            Text targetLabel = this.SandGlassText;

            if (this.nowAnimationSandGlassCounter <= 0)
            {
                back_Sandglass.gameObject.SetActive(true);
                targetLabel.text = (this.BattleTurnCount - 1).ToString();
                targetLabel.gameObject.SetActive(true);
                SandGlassImage.sprite = Resources.Load<Sprite>("AnimeSandGlass0");
                SandGlassImage.gameObject.SetActive(true);
            }

            int waitTime = 52;
            int startTime = 15;
            int moveLen = (Screen.width - 150) / 36;

            if (this.nowAnimationSandGlassCounter > startTime)
            {
                if (Application.platform == RuntimePlatform.Android ||
                    Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    // 何もしない
                }
                else
                {
                    System.Threading.Thread.Sleep(0);
                }
                //SandGlassImage.sprite = Resources.Load<Sprite>("AnimeSandGlass" + (this.nowAnimationSandGlassCounter-(startTime+1)).ToString());
                angle += 10;
                SandGlassImage.transform.rotation = Quaternion.Euler(0, 0, angle);
                SandGlassImage.transform.position = new Vector3(SandGlassImage.transform.position.x + moveLen, SandGlassImage.transform.position.y, SandGlassImage.transform.position.z);

                if (this.nowAnimationSandGlassCounter == 36)
                {
                    targetLabel.text = this.BattleTurnCount.ToString();
                }
            }

            this.nowAnimationSandGlassCounter++;

            if (this.nowAnimationSandGlassCounter > waitTime)
            {
                System.Threading.Thread.Sleep(500);
                this.angle = 0.0f;
                SandGlassImage.transform.rotation = new Quaternion(0, 0, 0, 0);
                SandGlassImage.transform.position = new Vector3(SandGlassImage.transform.position.x - moveLen * (waitTime - startTime), SandGlassImage.transform.position.y, SandGlassImage.transform.position.z);
                back_Sandglass.gameObject.SetActive(false);
                targetLabel.gameObject.SetActive(false);
                SandGlassImage.gameObject.SetActive(false);
                this.nowAnimationSandGlass = false;
                this.nowAnimationSandGlassCounter = 0;
            }
        }

        private void AnimationFinal(string message) { }
        private void ExecAnimationFinalBattle() { }
        private void BattleEndPhase() { }
        #endregion
    }
}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class TruthDungeon : MonoBehaviour
    {
        int nowReading = 0;
        List<string> nowMessage = new List<string>();
        List<DungeonPlayer.MessagePack.ActionEvent> nowEvent = new List<DungeonPlayer.MessagePack.ActionEvent>();

        // initialize data list
        List<GameObject> objList = new List<GameObject>();
        List<GameObject> objBlueWallTop = new List<GameObject>();
        List<GameObject> objBlueWallLeft = new List<GameObject>();
        List<GameObject> objBlueWallRight = new List<GameObject>();
        List<GameObject> objBlueWallBottom = new List<GameObject>();
        List<GameObject> unknownTile = new List<GameObject>();

        public GameObject prefab_BlueWall_T;
        public GameObject prefab_BlueWall_B;
        public GameObject prefab_BlueWall_L;
        public GameObject prefab_BlueWall_R;
        public GameObject prefabUnknownTile;
        public GameObject prefabDungeonTile;
        public GameObject prefab_TILEINFO_1;
        public GameObject prefab_TILEINFO_2;
        public GameObject prefab_TILEINFO_3;
        public GameObject prefab_TILEINFO_4;
        public GameObject prefab_TILEINFO_5;
        public GameObject prefab_TILEINFO_6;
        public GameObject prefab_TILEINFO_7;
        public GameObject prefab_TILEINFO_8;
        public GameObject prefab_TILEINFO_9;
        public GameObject prefab_TILEINFO_10;
        public GameObject prefab_TILEINFO_10_2;
        public GameObject prefab_TILEINFO_11;
        public GameObject prefab_TILEINFO_12;
        public GameObject prefab_TILEINFO_13;
        public GameObject prefab_TILEINFO_14;
        public GameObject prefab_TILEINFO_15;
        public GameObject prefab_TILEINFO_16;
        public GameObject prefab_TILEINFO_17;
        public GameObject prefab_TILEINFO_18;
        public GameObject prefab_TILEINFO_19;
        public GameObject prefab_TILEINFO_20;
        public GameObject prefab_TILEINFO_21;
        public GameObject prefab_TILEINFO_22;
        public GameObject prefab_TILEINFO_23;
        public GameObject prefab_TILEINFO_24;
        public GameObject prefab_TILEINFO_25;
        public GameObject prefab_TILEINFO_26;
        public GameObject prefab_TILEINFO_27;
        public GameObject prefab_TILEINFO_28;
        public GameObject prefab_TILEINFO_29;
        public GameObject prefab_TILEINFO_30;
        public GameObject prefab_TILEINFO_31;
        public GameObject prefab_TILEINFO_32;
        public GameObject prefab_TILEINFO_33;
        public GameObject prefab_TILEINFO_34;
        public GameObject prefab_TILEINFO_35;
        public GameObject prefab_TILEINFO_36;
        public GameObject prefab_TILEINFO_37;
        public GameObject prefab_TILEINFO_38;
        public GameObject prefab_TILEINFO_39;
        public GameObject prefab_TILEINFO_40;
        public GameObject prefab_TILEINFO_41;
        public GameObject prefab_TILEINFO_42;
        public GameObject prefab_TILEINFO_43;
        public GameObject prefab_TILEINFO_44;
        public GameObject prefab_FOUNTAIN;
        public GameObject prefabPlayer;
        public GameObject backgroundData;
        public Text mainMessage;
        public Text debug;
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
        //bool[] knownTileInfo = null;
        bool[] knownTileInfo2 = null;
        bool[] knownTileInfo3 = null;
        bool[] knownTileInfo4 = null;
        bool[] knownTileInfo5 = null;

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

        // ダンジョンマッピングデータを示すタイル情報
        GameObject[] dungeonTile = new GameObject[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];
        //GameObject[] unknownTile = new GameObject[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

        // 敵の強さを区分けするためのタイルカラー情報
        int[] tileColor = new int[Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW];

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

        // Use this for initialization
        void Start()
        {
            GroundOne.InitializeGroundOne();

            tileInfo = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo2 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo3 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo4 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
            tileInfo5 = new string[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];

            this.Player = Instantiate(this.prefabPlayer, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            ReadDungeonTileFromXmlFile(Application.dataPath + @"/DungeonMapping_T_1.xml");

            // 始めて開始する場合、あらかじめスタート地点を設定。
            if ((GroundOne.WE.DungeonPosX == 0) && (GroundOne.WE.DungeonPosY == 0))
            //    (GroundOne.WE.DungeonPosX == 1 + Database.DUNGEON_BASE_X + (Database.FIRST_POS % Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN) &&
            //    (GroundOne.WE.DungeonPosY == 1 + Database.DUNGEON_BASE_Y + (Database.FIRST_POS / Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN))
            {
                UpdatePlayerLocationInfo(39, -14, false);
                UpdateViewPoint(this.Player.transform.position.x, this.Player.transform.position.y);
            }
            else
            {
//              if (GroundOne.WE.Version <= 0)
                UpdatePlayerLocationInfo(GroundOne.WE.DungeonPosX, GroundOne.WE.DungeonPosY, false);
                UpdateViewPoint(GroundOne.WE.dungeonViewPointX, GroundOne.WE.dungeonViewPointY);
            }
            UpdateUnknownTile();

            //SetupDungeonMapping(GroundOne.WE.DungeonArea);
        }

        private void ReadDungeonTileFromXmlFile(string xmlFileName)
        {
            #region "ダンジョンマップをXMLから読み込み"
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFileName);

            XmlNodeList currentList = xml.GetElementsByTagName("TileData");
            XmlNodeList childList = currentList[0].ChildNodes;
            debug.text += "childlist count " + childList.Count.ToString() + "\r\n";

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
                this.unknownTile.Add(Instantiate(this.prefabUnknownTile, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
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
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER1.Length, childList2[ii].Name.Length - OTHER1.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_9, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER2))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER2.Length, childList2[ii].Name.Length - OTHER2.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_12, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER3))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER3.Length, childList2[ii].Name.Length - OTHER3.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_11, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER4))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER4.Length, childList2[ii].Name.Length - OTHER4.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_1, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER9))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER9.Length, childList2[ii].Name.Length - OTHER9.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_43, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER10))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER10.Length, childList2[ii].Name.Length - OTHER10.Length));
                        this.objList.Add(Instantiate(this.prefab_TILEINFO_44, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    else if (childList2[ii].Name.Contains(OTHER11))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER11.Length, childList2[ii].Name.Length - OTHER11.Length));
                        this.objList.Add(Instantiate(this.prefab_FOUNTAIN, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
                    }
                    if (childList2[ii].Name.Contains(OTHER5))
                    {
                        int targetNumber = Convert.ToInt32(childList2[ii].Name.Substring(OTHER5.Length, childList2[ii].Name.Length - OTHER5.Length));
                        blueWallTop[targetNumber] = true;
                        this.objBlueWallTop.Add(Instantiate(this.prefab_BlueWall_T, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
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
                        this.objBlueWallLeft.Add(Instantiate(this.prefab_BlueWall_L, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
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
                        this.objBlueWallRight.Add(Instantiate(this.prefab_BlueWall_R, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
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
                        this.objBlueWallBottom.Add(Instantiate(this.prefab_BlueWall_B, new Vector3(targetNumber % Database.TRUTH_DUNGEON_COLUMN, -(targetNumber / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject);
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
                if (current == Database.TILEINFO_1) { this.objList.Add(Instantiate(this.prefab_TILEINFO_1, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_1) { this.objList.Add(Instantiate(this.prefab_TILEINFO_1, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_2) { this.objList.Add(Instantiate(this.prefab_TILEINFO_2, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_3) { this.objList.Add(Instantiate(this.prefab_TILEINFO_3, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_4) { this.objList.Add(Instantiate(this.prefab_TILEINFO_4, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_5) { this.objList.Add(Instantiate(this.prefab_TILEINFO_5, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_6) { this.objList.Add(Instantiate(this.prefab_TILEINFO_6, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_7) { this.objList.Add(Instantiate(this.prefab_TILEINFO_7, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_8) { this.objList.Add(Instantiate(this.prefab_TILEINFO_8, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_9) { this.objList.Add(Instantiate(this.prefab_TILEINFO_9, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_10) { this.objList.Add(Instantiate(this.prefab_TILEINFO_10, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_11) { this.objList.Add(Instantiate(this.prefab_TILEINFO_11, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_12) { this.objList.Add(Instantiate(this.prefab_TILEINFO_12, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_13) { this.objList.Add(Instantiate(this.prefab_TILEINFO_13, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_14) { this.objList.Add(Instantiate(this.prefab_TILEINFO_14, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_15) { this.objList.Add(Instantiate(this.prefab_TILEINFO_15, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_16) { this.objList.Add(Instantiate(this.prefab_TILEINFO_16, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_17) { this.objList.Add(Instantiate(this.prefab_TILEINFO_17, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_18) { this.objList.Add(Instantiate(this.prefab_TILEINFO_18, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_19) { this.objList.Add(Instantiate(this.prefab_TILEINFO_19, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_20) { this.objList.Add(Instantiate(this.prefab_TILEINFO_20, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_21) { this.objList.Add(Instantiate(this.prefab_TILEINFO_21, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_22) { this.objList.Add(Instantiate(this.prefab_TILEINFO_22, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_23) { this.objList.Add(Instantiate(this.prefab_TILEINFO_23, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_24) { this.objList.Add(Instantiate(this.prefab_TILEINFO_24, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_25) { this.objList.Add(Instantiate(this.prefab_TILEINFO_25, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_26) { this.objList.Add(Instantiate(this.prefab_TILEINFO_26, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_27) { this.objList.Add(Instantiate(this.prefab_TILEINFO_27, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_28) { this.objList.Add(Instantiate(this.prefab_TILEINFO_28, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_29) { this.objList.Add(Instantiate(this.prefab_TILEINFO_29, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_30) { this.objList.Add(Instantiate(this.prefab_TILEINFO_30, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_31) { this.objList.Add(Instantiate(this.prefab_TILEINFO_31, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_32) { this.objList.Add(Instantiate(this.prefab_TILEINFO_32, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_33) { this.objList.Add(Instantiate(this.prefab_TILEINFO_33, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_34) { this.objList.Add(Instantiate(this.prefab_TILEINFO_34, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_35) { this.objList.Add(Instantiate(this.prefab_TILEINFO_35, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_36) { this.objList.Add(Instantiate(this.prefab_TILEINFO_36, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_37) { this.objList.Add(Instantiate(this.prefab_TILEINFO_37, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_38) { this.objList.Add(Instantiate(this.prefab_TILEINFO_38, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_39) { this.objList.Add(Instantiate(this.prefab_TILEINFO_39, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_40) { this.objList.Add(Instantiate(this.prefab_TILEINFO_40, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_41) { this.objList.Add(Instantiate(this.prefab_TILEINFO_41, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_42) { this.objList.Add(Instantiate(this.prefab_TILEINFO_42, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_43) { this.objList.Add(Instantiate(this.prefab_TILEINFO_43, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }
                else if (current == Database.TILEINFO_44) { this.objList.Add(Instantiate(this.prefab_TILEINFO_44, new Vector3((ii % Database.TRUTH_DUNGEON_COLUMN), -(ii / Database.TRUTH_DUNGEON_COLUMN), 0), Quaternion.identity) as GameObject); }

                unknownTile[ii].SetActive(!GroundOne.Truth_KnownTileInfo[ii]); // 反対ですが意味付けは同じ本質です。

                if ((GroundOne.WE.DungeonArea == 1) || (GroundOne.WE.DungeonArea == 0))
                {
                    tileInfo[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 2)
                {
                    tileInfo2[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 3)
                {
                    tileInfo3[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 4)
                {
                    tileInfo4[ii] = current;
                }
                else if (GroundOne.WE.DungeonArea == 5)
                {
                    tileInfo5[ii] = current;
                }

            }
            #endregion
            #endregion

        }

        bool keyDown = false;
        bool keyUp = false;
        bool keyLeft = false;
        bool keyRight = false;
        const int MOVE_INTERVAL = 15;
        int interval = MOVE_INTERVAL;
        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.UpArrow) ||
                Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.LeftArrow) ||
                Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.RightArrow) ||
                Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                //debug.text += "GetKeyUp ";
                CancelKeyDownMovement();
            }
            else if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.UpArrow))
            {
                this.keyUp = true;
                this.keyDown = false;
                movementTimer_Tick();
            }
            else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.LeftArrow))
            {
                this.keyLeft = true;
                this.keyRight = false;
                movementTimer_Tick();
            }
            else if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.RightArrow))
            {
                this.keyRight = true;
                this.keyLeft = false;
                movementTimer_Tick();
            }
            else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.DownArrow))
            {
                this.keyDown = true;
                this.keyUp = false;
                movementTimer_Tick();
            }

        }
        private void movementTimer_Tick()
        {
            if (this.interval < MOVE_INTERVAL) { this.interval++; return; }
            else { this.interval = 0; }

            //movementTimer.Interval = this.MovementInterval;
            if (this.keyUp)
            {
                UpdatePlayersKeyEvents(0);
            }
            else if (this.keyRight)
            {
                //debug.text += "keyRight(S) ";
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
        private void CancelKeyDownMovement()
        {
            this.keyUp = false;
            this.keyDown = false;
            this.keyLeft = false;
            this.keyRight = false;
            this.interval = MOVE_INTERVAL;
            //movementTimer.Enabled = false;
            //movementTimer.Stop();
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
            //debug.text += "LS:" + interval.ToString() + " ";
            //debug.text += x.ToString() + " " + y.ToString() + "\r\n";
            GroundOne.WE.DungeonPosX = (int)x;
            GroundOne.WE.DungeonPosY = (int)y;
            //this.Player.Visible = false;
            //debug.text += "call player move. ";
            this.Player.transform.position = new Vector3(x, y, this.Player.transform.position.z);
            //this.Player.Visible = true;
            //if (!noSound)
            //{
            //    //GroundOne.PlaySoundEffect("footstep.mp3");
            //}
            ////dungeonField.Invalidate();
        }

        private int GetTileNumber(Vector3 pos)
        {
            Vector3 adjustPos = new Vector3(pos.x, pos.y, pos.z);
            //debug.text += "srcPos: " + pos.ToString() + " adjustPos: " + adjustPos.ToString() + " ";
            //int number = (int)(((-viewPoint.x + adjustPos.x - 0)) / Database.DUNGEON_MOVE_LEN_U) % Database.TRUTH_DUNGEON_COLUMN + (int)(((-viewPoint.y + adjustPos.y - 0) / Database.DUNGEON_MOVE_LEN_U) * Database.TRUTH_DUNGEON_COLUMN);
            int number = (int)(adjustPos.x % Database.TRUTH_DUNGEON_COLUMN + (-adjustPos.y) * Database.TRUTH_DUNGEON_COLUMN);
            int row = number / Database.TRUTH_DUNGEON_COLUMN;
            int column = number % Database.TRUTH_DUNGEON_COLUMN;
            //label1.Text = "row: " + row.ToString() + "  column: " + column.ToString() + "  viewPoint.X: " + viewPoint.X.ToString() + "  viewPoint.Y: " + viewPoint.Y.ToString() + "  Player.transform.position.x: " + Player.transform.position.x.ToString() + "  Player.transform.position.y: " + Player.transform.position.y.ToString() + "  number: " + number.ToString();
            //label1.Update();
            return number;
        }
        private bool CheckWall(int direction) // 0:↑ 1:← 2:→ 3:↓
        {
            //debug.text += "CheckWall(S) ";
            int tilenum = GetTileNumber(Player.transform.position);
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
            //if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd) { WallHitMessage = "アイン：・・・"; }
            #region "Wall判定"
            switch (targetTileInfo[GetTileNumber(Player.transform.position)])
            {
                case "Tile1.bmp":
                    break;
                case "Tile1-WallT.bmp":
                    if (direction == 0)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallL.bmp":
                    if (direction == 1)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallR.bmp":
                    if (direction == 2)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallB.bmp":
                    if (direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallLR-DummyL.bmp":
                    if (direction == 2)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallRB-DummyB.bmp":
                    if (direction == 2)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallTL.bmp":
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
                            this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                            tapOK();
                            GroundOne.PlaySoundEffect("WallHit.mp3");
                            CancelKeyDownMovement();
                            return true;
                        }
                    }
                    break;
                case "Tile1-WallTR.bmp":
                    if (direction == 0 || direction == 2)
                    {
                        // ４階、35、29、右方向を無視
                        if (GroundOne.WE.DungeonArea == 4 && row == 35 && column == 29 && direction == 2)
                        {
                            break;
                        }
                        else
                        {
                            this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                            tapOK();
                            GroundOne.PlaySoundEffect("WallHit.mp3");
                            CancelKeyDownMovement();
                            return true;
                        }
                    }
                    break;
                case "Tile1-WallTB.bmp":
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
                            this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                            tapOK();
                            GroundOne.PlaySoundEffect("WallHit.mp3");
                            CancelKeyDownMovement();
                            return true;
                        }
                    }
                    break;
                case "Tile1-WallLR.bmp":
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
                            this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                            tapOK();
                            GroundOne.PlaySoundEffect("WallHit.mp3");
                            CancelKeyDownMovement();
                            return true;
                        }
                    }
                    break;
                case "Tile1-WallLB.bmp":
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
                            this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                            tapOK();
                            GroundOne.PlaySoundEffect("WallHit.mp3");
                            CancelKeyDownMovement();
                            return true;
                        }
                    }
                    break;
                case "Tile1-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Tile1-WallTLRB.bmp":
                    if (direction == 0 || direction == 1 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Upstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Upstair-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Upstair-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Downstair-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Downstair-WallT.bmp":
                    if (direction == 0)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Downstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
                case "Downstair-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        this.nowMessage.Add(WallHitMessage); this.nowEvent.Add(MessagePack.ActionEvent.None);
                        tapOK();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        CancelKeyDownMovement();
                        return true;
                    }
                    break;
            }
            #endregion
            UpdateMainMessage("", true, true);
            return false;
        }

        private bool CheckBlueWall(int direction) // 0:↑ 1:← 2:→ 3:↓
        {
            // プレイヤーの位置に対応している青壁情報を取得する。
            // 青壁情報を取得して、プレイヤー動作方向に対して青壁情報が一致する場合
            if (blueWallBottom[GetTileNumber(Player.transform.position)])
            {
                if (direction == 3)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect("WallHit.mp3");
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallLeft[GetTileNumber(Player.transform.position)])
            {
                if (direction == 1)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect("WallHit.mp3");
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallRight[GetTileNumber(Player.transform.position)])
            {
                if (direction == 2)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect("WallHit.mp3");
                    CancelKeyDownMovement();
                    return true;
                }
            }

            if (blueWallTop[GetTileNumber(Player.transform.position)])
            {
                if (direction == 0)
                {
                    this.nowMessage.Add("アイン：開かねぇ・・・"); this.nowEvent.Add(MessagePack.ActionEvent.None);
                    this.tapOK();
                    GroundOne.PlaySoundEffect("WallHit.mp3");
                    CancelKeyDownMovement();
                    return true;
                }
            }

            UpdateMainMessage("", true, true);
            return false;
        }
        Vector3 viewPoint = new Vector3();
        private void UpdatePlayersKeyEvents(int direction)
        {
            //debug.text += "UpdatePlayersKeyEvents(S) ";
            // 通常動作モード
            //if (!this.DungeonViewMode)
            //{
            float moveX = 0;
            float moveY = 0;

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

            //    int tilenum = GetTileNumber(Player.transform.position);
            //    int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
            //    int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;

            //debug.text += "UpdatePlayersKeyEvent " + direction.ToString() + " " + this.viewPoint.ToString()+" " + this.Player.transform.position.ToString() + "\r\n";
            // 上端ダンジョン外を見せないようにする
            // 右端ダンジョン外を見せないようにする
            // 左端ダンジョン外を見せないようにする
            // 下端ダンジョン外を見せないようにする
            // 上端近辺での↑移動はプレイヤー移動
            // 右端近辺での→移動はプレイヤー移動
            // 左端近辺での←移動はプレイヤー移動
            // 下端近辺での↓移動はプレイヤー移動
            //if ((direction == 0 && this.viewPoint.y <= 0) || // change unity
            //     (direction == 1 && this.viewPoint.x >= 0) ||
            //     (direction == 2 && this.viewPoint.x <= -Database.TRUTH_DUNGEON_COLUMN * Database.DUNGEON_MOVE_LEN_U / 2) || // change unity
            //     (direction == 3 && this.viewPoint.y <= Database.TRUTH_DUNGEON_ROW * Database.DUNGEON_MOVE_LEN_U / 2) || // change unity
            //     (direction == 0 && this.Player.transform.position.y >= -Database.DUNGEON_MOVE_LEN_U * 8) || // change unity
            //     (direction == 1 && this.Player.transform.position.x >= Database.DUNGEON_MOVE_LEN_U * 10) || // change unity
            //     (direction == 2 && this.Player.transform.position.x <= Database.DUNGEON_MOVE_LEN_U * 20) // change unity)
            //     (direction == 3 && this.Player.transform.position.y <= -Database.DUNGEON_MOVE_LEN_U * 12)) // change unity
            if (true)
            {
                UpdatePlayerLocationInfo(this.Player.transform.position.x + moveX, this.Player.transform.position.y + moveY, false);
                UpdateViewPoint(this.Player.transform.position.x, this.Player.transform.position.y);
            }
            else
            {
                //    UpdateViewPoint(viewPoint.x + moveX/10, viewPoint.y - moveY); // change unity
            }

            //    // EPICアイテムEPIC_ORB_GROW_GREENの効果
            //    for (int ii = 0; ii < 3; ii++)
            //    {
            //        MainCharacter player = null;
            //        Label targetLabel = null;
            //        if (ii == 0) { player = mc; targetLabel = currentSkillPoint1; }
            //        else if (ii == 1) { player = sc; targetLabel = currentSkillPoint2; }
            //        else if (ii == 2) { player = tc; targetLabel = currentSkillPoint3; }
            //        if (player != null)
            //        {
            //            if (player.Accessory != null)
            //            {
            //                if (player.Accessory.Name == Database.EPIC_ORB_GROW_GREEN)
            //                {
            //                    player.CurrentSkillPoint++;
            //                    targetLabel.Width = (int)((double)((double)player.CurrentSkillPoint / (double)player.MaxSkillPoint) * 100.0f);
            //                }
            //            }
            //            if (player.Accessory2 != null)
            //            {
            //                if (player.Accessory2.Name == Database.EPIC_ORB_GROW_GREEN)
            //                {
            //                    player.CurrentSkillPoint++;
            //                    targetLabel.Width = (int)((double)((double)player.CurrentSkillPoint / (double)player.MaxSkillPoint) * 100.0f);
            //                }
            //            }
            //        }
            //    }

            //    // 移動時のタイル更新
            bool lowSpeed = UpdateUnknownTile();
            //    //dungeonField.Invalidate();
            //    this.Update();

            // イベント発生
            SearchSomeEvents();

            //    if (lowSpeed)
            //    {
            //        //    System.Threading.Thread.Sleep(200);
            //        this.MovementInterval = 200;
            //    }
            //    else
            //    {
            //        //    System.Threading.Thread.Sleep(100);
            //        this.MovementInterval = 100;
            //    }
            //    GetTileNumber(Player.transform.position);
            //}
            //// View動作モード
            //else
            //{
            //    int moveX = 0;
            //    int moveY = 0;

            //    if (direction == 0) moveY = -Database.DUNGEON_MOVE_LEN;
            //    else if (direction == 1) moveX = -Database.DUNGEON_MOVE_LEN;
            //    else if (direction == 2) moveX = Database.DUNGEON_MOVE_LEN;
            //    else if (direction == 3) moveY = Database.DUNGEON_MOVE_LEN;

            //    int tilenum = GetTileNumber(Player.transform.position);
            //    int row = tilenum / Database.TRUTH_DUNGEON_COLUMN;
            //    int column = tilenum % Database.TRUTH_DUNGEON_COLUMN;

            //    // 上端ダンジョン外を見せないようにする
            //    // 右端ダンジョン外を見せないようにする
            //    // 左端ダンジョン外を見せないようにする
            //    // 下端ダンジョン外を見せないようにする
            //    if ((direction == 0 && this.viewPoint.Y >= 0) ||
            //         (direction == 1 && this.viewPoint.X >= 0) ||
            //         (direction == 2 && this.viewPoint.X <= -Database.TRUTH_DUNGEON_COLUMN * Database.DUNGEON_MOVE_LEN / 2) ||
            //         (direction == 3 && this.viewPoint.Y <= -Database.TRUTH_DUNGEON_ROW * Database.DUNGEON_MOVE_LEN / 2)
            //        )
            //    {
            //        return;
            //    }

            //    UpdateViewPoint(viewPoint.X - moveX, viewPoint.Y - moveY);

            //    if (direction == 0)
            //    {
            //        UpdatePlayerLocationInfo(this.Player.transform.position.x + moveX, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN, false);
            //    }
            //    else if (direction == 1)
            //    {
            //        UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
            //    }
            //    else if (direction == 2)
            //    {
            //        UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
            //    }
            //    else if (direction == 3)
            //    {
            //        UpdatePlayerLocationInfo(this.Player.transform.position.x + moveX, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN, false);
            //    }

            //    //dungeonField.Invalidate();
            //    this.Update();

            //    GetTileNumber(Player.transform.position);
            //}
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
                //#region "２階"
                //else if (GroundOne.WE.DungeonArea == 2 && GroundOne.WE2.StartSeeker == false)
                //{
                //    for (int ii = 0; ii < 200; ii++)
                //    {
                //        if (CheckTriggeredEvent(ii))
                //        {
                //            detectEvent = true;
                //            ExecSomeEvents(2, ii);
                //            return;
                //        }
                //    }
                //}
                //#endregion
                //#region "３階"
                //else if (GroundOne.WE.DungeonArea == 3 && GroundOne.WE2.StartSeeker == false)
                //{
                //    for (int ii = 0; ii < 500; ii++)
                //    {
                //        if (CheckTriggeredEvent(ii))
                //        {
                //            detectEvent = true;
                //            ExecSomeEvents(3, ii);
                //            return;
                //        }
                //    }
                //}
                //#endregion
                //#region "４階"
                //else if (GroundOne.WE.DungeonArea == 4)
                //{
                //    for (int ii = 0; ii < 500; ii++)
                //    {
                //        if (CheckTriggeredEvent(ii))
                //        {
                //            detectEvent = true;
                //            ExecSomeEvents(4, ii);
                //            return;
                //        }
                //    }
                //}
                //#endregion
                //#region "５階"
                //else if (GroundOne.WE.DungeonArea == 5)
                //{
                //    for (int ii = 0; ii < 500; ii++)
                //    {
                //        if (CheckTriggeredEvent(ii))
                //        {
                //            detectEvent = true;
                //            ExecSomeEvents(5, ii);
                //            return;
                //        }
                //    }
                //}
                //#endregion
                //#region "現実世界"
                //if (GroundOne.WE2.StartSeeker)
                //{
                //    ExecSomeEvent_ReadWorld();
                //}
                //#endregion


                if (!detectEvent)
                {
                    EncountEnemy();
                }
            }
        }
        private bool UpdateUnknownTile()
        {
            bool newUpdate = false; // 新しくタイルが拓けた事を示すフラグ
            int currentPosNum = GetTileNumber(this.Player.transform.position);
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
                (currentTileInfo != "Tile1-WallT.bmp" &&
                 currentTileInfo != "Tile1-WallTL.bmp" &&
                 currentTileInfo != "Tile1-WallTR.bmp" &&
                 currentTileInfo != "Tile1-WallTB.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallT.bmp" &&
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
                (currentTileInfo != "Tile1-WallL.bmp" &&
                 currentTileInfo != "Tile1-WallTL.bmp" &&
                 currentTileInfo != "Tile1-WallLR.bmp" &&
                 currentTileInfo != "Tile1-WallLB.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallLR-DummyL.bmp" &&
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
                (currentTileInfo != "Tile1-WallR.bmp" &&
                 currentTileInfo != "Tile1-WallTR.bmp" &&
                 currentTileInfo != "Tile1-WallLR.bmp" &&
                 currentTileInfo != "Tile1-WallRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallLR-DummyL.bmp" &&
                 currentTileInfo != "Tile1-WallR-DummyR.bmp" &&
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
                (currentTileInfo != "Tile1-WallB.bmp" &&
                 currentTileInfo != "Tile1-WallTB.bmp" &&
                 currentTileInfo != "Tile1-WallLB.bmp" &&
                 currentTileInfo != "Tile1-WallRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallRB.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallB-DummyB.bmp" &&
                 currentTileInfo != "Tile1-WallRB-DummyB.bmp" &&
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

        //private void UpdateUnknownTileArea11()
        //{
        //    for (int ii = 13; ii <= 22; ii++)
        //    {
        //        for (int jj = 27; jj <= 34; jj++)
        //        {
        //            unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].Visible = false;
        //            knownTileInfo[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
        //        }
        //    }
        //    //dungeonField.Invalidate();
        //    this.Update();
        //}

        private int stepCounter = 0; // 敵エンカウント率調整の値
        private void EncountEnemy()
        {
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
            debug.text += "R:" + resultValue.ToString();
            if (GroundOne.WE.CompleteSlayBoss5) resultValue = 100;

            if (labelVigilance.text == Database.TEXT_VIGILANCE_MODE)
            {
                stepCounter += 1;
            }
            else
            {
                stepCounter += 3;
            }
            int encountBorder = 0;
            if (labelVigilance.text == Database.TEXT_VIGILANCE_MODE)
            {
                if (GroundOne.WE.DungeonArea == 4)
                {
                    encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 20);
                }
                else
                {
                    encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 10);
                }
            }
            else
            {
                encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 5);
            }

            if (resultValue <= encountBorder)
            {
                stepCounter = 0;
                string enemyName = "";
                string enemyName2 = "";
                string enemyName3 = "";
                string[] monsterName = null;
                string[] monsterName2 = null;
                int enemyLevel = tileColor[GetTileNumber(this.Player.transform.position)];
                // １階は左上：エリア１、左下：エリア２、右上：エリア３、右下：エリア４
                if (GroundOne.WE.DungeonArea == 1)
                {
                    if (enemyLevel == 1)
                    {
                        monsterName = new string[4];
                        monsterName[0] = Database.ENEMY_KOUKAKU_WURM;
                        monsterName[1] = Database.ENEMY_HIYOWA_BEATLE;
                        monsterName[2] = Database.ENEMY_GREEN_CHILD;
                        
                        if (GroundOne.MC.Level <= 2)
                        {
                            monsterName[3] = monsterName[2];
                        }
                        else
                        {
                            monsterName[3] = Database.ENEMY_MANDRAGORA;
                        }
                        monsterName2 = new string[3];
                        monsterName2[0] = monsterName[0];
                        monsterName2[1] = monsterName[1];
                        monsterName2[2] = monsterName[2];
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

                this.Update();

                UpdateMainMessage("アイン：敵と遭遇だ！", true);

                EncountBattle(enemyName, enemyName2, enemyName3, false, false, false, false);

                UpdateMainMessage("", true);
            }
        }

        private bool CheckTriggeredEvent(int eventNum)
        {
            int tilenum = GetTileNumber(Player.transform.position);
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
                    // 知の部屋、複合レバーの看板１
                    else if (row == 5 && column == 51 && eventNum == 42)
                    {
                        return true;
                    }
                    // 知の部屋、複合レバー１－１
                    else if (row == 4 && column == 51 && eventNum == 43)
                    {
                        return true;
                    }
                    // 知の部屋、複合レバー１－２
                    else if (row == 6 && column == 51 && eventNum == 44)
                    {
                        return true;
                    }
                    // 技の部屋、看板１
                    else if (row == 27 && column == 57 && eventNum == 45)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＡ
                    else if ((row == 26 && column == 56 && eventNum == 46) ||
                             (row == 26 && column == 55 && eventNum == 46) ||
                             (row == 26 && column == 54 && eventNum == 46) ||
                             (row == 26 && column == 53 && eventNum == 46) ||
                             (row == 26 && column == 52 && eventNum == 46) ||
                             (row == 26 && column == 51 && eventNum == 46) ||
                             (row == 26 && column == 50 && eventNum == 46) ||
                             (row == 26 && column == 49 && eventNum == 46) ||
                             (row == 26 && column == 48 && eventNum == 46) ||

                             (row == 27 && column == 56 && eventNum == 46) ||
                             (row == 27 && column == 55 && eventNum == 46) ||
                             (row == 27 && column == 54 && eventNum == 46) ||
                             (row == 27 && column == 53 && eventNum == 46) ||
                             (row == 27 && column == 52 && eventNum == 46) ||
                             (row == 27 && column == 51 && eventNum == 46) ||
                             (row == 27 && column == 50 && eventNum == 46) ||
                             (row == 27 && column == 49 && eventNum == 46) ||
                             (row == 27 && column == 48 && eventNum == 46) ||

                             (row == 28 && column == 56 && eventNum == 46) ||
                             (row == 28 && column == 55 && eventNum == 46) ||
                             (row == 28 && column == 54 && eventNum == 46) ||
                             (row == 28 && column == 53 && eventNum == 46) ||
                             (row == 28 && column == 52 && eventNum == 46) ||
                             (row == 28 && column == 51 && eventNum == 46) ||
                             (row == 28 && column == 50 && eventNum == 46) ||
                             (row == 28 && column == 49 && eventNum == 46) ||
                             (row == 28 && column == 48 && eventNum == 46))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＡクリア
                    else if ((row == 26 && column == 47 && eventNum == 47) ||
                             (row == 27 && column == 47 && eventNum == 47) ||
                             (row == 28 && column == 47 && eventNum == 47))
                    {
                        return true;
                    }
                    // 技の部屋、看板２
                    else if (row == 27 && column == 45 && eventNum == 48)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＢ
                    else if ((row == 26 && column == 44 && eventNum == 49) ||
                             (row == 26 && column == 43 && eventNum == 49) ||
                             (row == 26 && column == 42 && eventNum == 49) ||
                             (row == 26 && column == 41 && eventNum == 49) ||
                             (row == 26 && column == 40 && eventNum == 49) ||
                             (row == 26 && column == 39 && eventNum == 49) ||
                             (row == 26 && column == 38 && eventNum == 49) ||
                             (row == 26 && column == 37 && eventNum == 49) ||
                             (row == 26 && column == 36 && eventNum == 49) ||

                             (row == 27 && column == 44 && eventNum == 49) ||
                             (row == 27 && column == 43 && eventNum == 49) ||
                             (row == 27 && column == 42 && eventNum == 49) ||
                             (row == 27 && column == 41 && eventNum == 49) ||
                             (row == 27 && column == 40 && eventNum == 49) ||
                             (row == 27 && column == 39 && eventNum == 49) ||
                             (row == 27 && column == 38 && eventNum == 49) ||
                             (row == 27 && column == 37 && eventNum == 49) ||
                             (row == 27 && column == 36 && eventNum == 49) ||

                             (row == 28 && column == 44 && eventNum == 49) ||
                             (row == 28 && column == 43 && eventNum == 49) ||
                             (row == 28 && column == 42 && eventNum == 49) ||
                             (row == 28 && column == 41 && eventNum == 49) ||
                             (row == 28 && column == 40 && eventNum == 49) ||
                             (row == 28 && column == 39 && eventNum == 49) ||
                             (row == 28 && column == 38 && eventNum == 49) ||
                             (row == 28 && column == 37 && eventNum == 49) ||
                             (row == 28 && column == 36 && eventNum == 49))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＢクリア
                    else if ((row == 26 && column == 35 && eventNum == 50) ||
                             (row == 27 && column == 35 && eventNum == 50) ||
                             (row == 28 && column == 35 && eventNum == 50))
                    {
                        return true;
                    }
                    // 技の部屋、看板３
                    else if (row == 27 && column == 33 && eventNum == 51)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＣ
                    else if ((row == 26 && column == 32 && eventNum == 52) ||
                             (row == 26 && column == 31 && eventNum == 52) ||
                             (row == 26 && column == 30 && eventNum == 52) ||
                             (row == 26 && column == 29 && eventNum == 52) ||
                             (row == 26 && column == 28 && eventNum == 52) ||

                             (row == 27 && column == 32 && eventNum == 52) ||
                             (row == 27 && column == 31 && eventNum == 52) ||
                             (row == 27 && column == 30 && eventNum == 52) ||
                             (row == 27 && column == 29 && eventNum == 52) ||
                             (row == 27 && column == 28 && eventNum == 52) ||

                             (row == 28 && column == 32 && eventNum == 52) ||
                             (row == 28 && column == 31 && eventNum == 52) ||
                             (row == 28 && column == 30 && eventNum == 52) ||
                             (row == 28 && column == 29 && eventNum == 52) ||
                             (row == 28 && column == 28 && eventNum == 52) ||

                             (row == 29 && column == 30 && eventNum == 52) ||
                             (row == 29 && column == 29 && eventNum == 52) ||
                             (row == 29 && column == 28 && eventNum == 52) ||

                             (row == 30 && column == 30 && eventNum == 52) ||
                             (row == 30 && column == 29 && eventNum == 52) ||
                             (row == 30 && column == 28 && eventNum == 52) ||

                             (row == 31 && column == 30 && eventNum == 52) ||
                             (row == 31 && column == 29 && eventNum == 52) ||
                             (row == 31 && column == 28 && eventNum == 52) ||

                             (row == 32 && column == 30 && eventNum == 52) ||
                             (row == 32 && column == 29 && eventNum == 52) ||
                             (row == 32 && column == 28 && eventNum == 52) ||

                             (row == 33 && column == 32 && eventNum == 52) ||
                             (row == 33 && column == 31 && eventNum == 52) ||
                             (row == 33 && column == 30 && eventNum == 52) ||
                             (row == 33 && column == 29 && eventNum == 52) ||
                             (row == 33 && column == 28 && eventNum == 52) ||

                             (row == 34 && column == 32 && eventNum == 52) ||
                             (row == 34 && column == 31 && eventNum == 52) ||
                             (row == 34 && column == 30 && eventNum == 52) ||
                             (row == 34 && column == 29 && eventNum == 52) ||
                             (row == 34 && column == 28 && eventNum == 52) ||

                             (row == 35 && column == 32 && eventNum == 52) ||
                             (row == 35 && column == 31 && eventNum == 52) ||
                             (row == 35 && column == 30 && eventNum == 52) ||
                             (row == 35 && column == 29 && eventNum == 52) ||
                             (row == 35 && column == 28 && eventNum == 52))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＣクリア
                    else if ((row == 33 && column == 33 && eventNum == 53) ||
                             (row == 34 && column == 33 && eventNum == 53) ||
                             (row == 35 && column == 33 && eventNum == 53))
                    {
                        return true;
                    }
                    // 技の部屋、看板４
                    else if (row == 34 && column == 35 && eventNum == 54)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＤ
                    else if ((row == 33 && column == 36 && eventNum == 55) ||
                             (row == 33 && column == 37 && eventNum == 55) ||
                             (row == 33 && column == 38 && eventNum == 55) ||
                             (row == 33 && column == 39 && eventNum == 55) ||
                             (row == 33 && column == 40 && eventNum == 55) ||
                             (row == 33 && column == 41 && eventNum == 55) ||
                             (row == 33 && column == 42 && eventNum == 55) ||
                             (row == 33 && column == 43 && eventNum == 55) ||
                             (row == 33 && column == 44 && eventNum == 55) ||

                             (row == 34 && column == 36 && eventNum == 55) ||
                             (row == 34 && column == 37 && eventNum == 55) ||
                             (row == 34 && column == 38 && eventNum == 55) ||
                             (row == 34 && column == 39 && eventNum == 55) ||
                             (row == 34 && column == 40 && eventNum == 55) ||
                             (row == 34 && column == 41 && eventNum == 55) ||
                             (row == 34 && column == 42 && eventNum == 55) ||
                             (row == 34 && column == 43 && eventNum == 55) ||
                             (row == 34 && column == 44 && eventNum == 55) ||

                             (row == 35 && column == 36 && eventNum == 55) ||
                             (row == 35 && column == 37 && eventNum == 55) ||
                             (row == 35 && column == 38 && eventNum == 55) ||
                             (row == 35 && column == 39 && eventNum == 55) ||
                             (row == 35 && column == 40 && eventNum == 55) ||
                             (row == 35 && column == 41 && eventNum == 55) ||
                             (row == 35 && column == 42 && eventNum == 55) ||
                             (row == 35 && column == 43 && eventNum == 55) ||
                             (row == 35 && column == 44 && eventNum == 55))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＤクリア
                    else if ((row == 33 && column == 45 && eventNum == 56) ||
                             (row == 34 && column == 45 && eventNum == 56) ||
                             (row == 35 && column == 45 && eventNum == 56))
                    {
                        return true;
                    }
                    // 技の部屋、看板５
                    else if (row == 34 && column == 47 && eventNum == 57)
                    {
                        return true;
                    }
                    // 技の部屋、ルームＥ
                    else if ((row == 34 && column == 48 && eventNum == 58))
                    {
                        return true;
                    }
                    // 技の部屋、ルームＥクリア
                    else if ((row == 33 && column == 57 && eventNum == 59) ||
                             (row == 34 && column == 57 && eventNum == 59) ||
                             (row == 35 && column == 57 && eventNum == 59))
                    {
                        return true;
                    }
                    // 技の部屋、看板６
                    else if (row == 34 && column == 59 && eventNum == 60)
                    {
                        return true;
                    }
                    // 心の部屋、ヒント１
                    else if (row == 12 && column == 0 && eventNum == 61)
                    {
                        return true;
                    }
                    // 心の部屋、題材１
                    else if ((row == 0 && column == 0 && eventNum == 62) ||
                             (row == 1 && column == 0 && eventNum == 62) ||
                             (row == 2 && column == 0 && eventNum == 62) ||
                             (row == 3 && column == 0 && eventNum == 62) ||
                             (row == 4 && column == 0 && eventNum == 62))
                    {
                        return true;
                    }
                    // 心の部屋、題材２
                    else if ((row == 0 && column == 9 && eventNum == 63) ||
                             (row == 0 && column == 10 && eventNum == 63) ||
                             (row == 0 && column == 11 && eventNum == 63) ||
                             (row == 0 && column == 12 && eventNum == 63) ||
                             (row == 0 && column == 13 && eventNum == 63) ||
                             (row == 0 && column == 14 && eventNum == 63))
                    {
                        return true;
                    }
                    // 心の部屋、題材３
                    else if ((row == 0 && column == 28 && eventNum == 64))
                    {
                        return true;
                    }
                    // 心の部屋、題材４
                    else if ((row == 4 && column == 3 && eventNum == 65) ||
                             (row == 4 && column == 4 && eventNum == 65) ||
                             (row == 4 && column == 5 && eventNum == 65) ||
                             (row == 5 && column == 3 && eventNum == 65) ||
                             (row == 5 && column == 4 && eventNum == 65) ||
                             (row == 5 && column == 5 && eventNum == 65) ||
                             (row == 6 && column == 3 && eventNum == 65) ||
                             (row == 6 && column == 4 && eventNum == 65) ||
                             (row == 6 && column == 5 && eventNum == 65)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材５
                    else if ((row == 2 && column == 9 && eventNum == 66) ||
                             (row == 2 && column == 10 && eventNum == 66) ||
                             (row == 2 && column == 11 && eventNum == 66) ||
                             (row == 3 && column == 9 && eventNum == 66) ||
                             (row == 3 && column == 10 && eventNum == 66) ||
                             (row == 3 && column == 11 && eventNum == 66) ||
                             (row == 4 && column == 9 && eventNum == 66) ||
                             (row == 4 && column == 10 && eventNum == 66) ||
                             (row == 4 && column == 11 && eventNum == 66)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材６
                    else if ((row == 2 && column == 22 && eventNum == 67) ||
                             (row == 2 && column == 23 && eventNum == 67) ||
                             (row == 2 && column == 24 && eventNum == 67) ||
                             (row == 3 && column == 22 && eventNum == 67) ||
                             (row == 3 && column == 23 && eventNum == 67) ||
                             (row == 3 && column == 24 && eventNum == 67) ||
                             (row == 4 && column == 22 && eventNum == 67) ||
                             (row == 4 && column == 23 && eventNum == 67) ||
                             (row == 4 && column == 24 && eventNum == 67)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材７
                    else if ((row == 6 && column == 14 && eventNum == 68) ||
                             (row == 6 && column == 15 && eventNum == 68) ||
                             (row == 6 && column == 16 && eventNum == 68) ||
                             (row == 7 && column == 14 && eventNum == 68) ||
                             (row == 7 && column == 15 && eventNum == 68) ||
                             (row == 7 && column == 16 && eventNum == 68) ||
                             (row == 8 && column == 14 && eventNum == 68) ||
                             (row == 8 && column == 15 && eventNum == 68) ||
                             (row == 8 && column == 16 && eventNum == 68)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材８
                    else if ((row == 5 && column == 28 && eventNum == 69) ||
                             (row == 6 && column == 28 && eventNum == 69) ||
                             (row == 7 && column == 28 && eventNum == 69)
                             )
                    {
                        return true;
                    }
                    // 心の部屋、題材９
                    else if ((row == 8 && column == 20 && eventNum == 70) ||
                             (row == 8 && column == 21 && eventNum == 70) ||
                             (row == 8 && column == 22 && eventNum == 70) ||
                             (row == 8 && column == 23 && eventNum == 70) ||
                             (row == 8 && column == 24 && eventNum == 70) ||
                             (row == 8 && column == 25 && eventNum == 70) ||
                             (row == 9 && column == 20 && eventNum == 70) ||
                             (row == 9 && column == 21 && eventNum == 70) ||
                             (row == 9 && column == 22 && eventNum == 70) ||
                             (row == 9 && column == 23 && eventNum == 70) ||
                             (row == 9 && column == 24 && eventNum == 70) ||
                             (row == 9 && column == 25 && eventNum == 70) ||
                             (row == 10 && column == 20 && eventNum == 70) ||
                             (row == 10 && column == 21 && eventNum == 70) ||
                             (row == 10 && column == 22 && eventNum == 70) ||
                             (row == 10 && column == 23 && eventNum == 70) ||
                             (row == 10 && column == 24 && eventNum == 70) ||
                             (row == 10 && column == 25 && eventNum == 70)
                        )
                    {
                        return true;
                    }
                    // 心の部屋、題材１０
                    else if ((row == 12 && column == 9 && eventNum == 71) ||
                             (row == 12 && column == 10 && eventNum == 71) ||
                             (row == 12 && column == 11 && eventNum == 71) ||
                             (row == 12 && column == 12 && eventNum == 71) ||
                             (row == 12 && column == 13 && eventNum == 71) ||
                             (row == 12 && column == 14 && eventNum == 71)
                        )
                    {
                        return true;
                    }
                    // 力の部屋、ボス１
                    else if (row == 37 && column == 22 && eventNum == 72)
                    {
                        return true;
                    }
                    // 力の部屋、ボス２
                    else if (row == 27 && column == 22 && eventNum == 73)
                    {
                        return true;
                    }
                    // 力の部屋、ボス３
                    else if (row == 19 && column == 16 && eventNum == 74)
                    {
                        return true;
                    }
                    // 力の部屋、ボス４
                    else if (row == 22 && column == 2 && eventNum == 75)
                    {
                        return true;
                    }
                    // 力の部屋、ボス５
                    else if (row == 31 && column == 3 && eventNum == 76)
                    {
                        return true;
                    }
                    // 力の部屋、ボス６
                    else if (row == 36 && column == 14 && eventNum == 77)
                    {
                        return true;
                    }
                    // 宝箱１
                    else if (row == 16 && column == 59 && !GroundOne.WE.TruthTreasure21 && eventNum == 78)
                    {
                        return true;
                    }
                    // 宝箱２
                    else if (row == 12 && column == 35 && !GroundOne.WE.TruthTreasure22 && eventNum == 79)
                    {
                        return true;
                    }
                    // 宝箱３
                    else if (row == 5 && column == 55 && !GroundOne.WE.TruthTreasure23 && eventNum == 80)
                    {
                        return true;
                    }
                    // 宝箱４
                    else if (row == 25 && column == 59 && !GroundOne.WE.TruthTreasure24 && eventNum == 81)
                    {
                        return true;
                    }
                    // 宝箱５
                    else if (row == 27 && column == 46 && !GroundOne.WE.TruthTreasure25 && eventNum == 82)
                    {
                        return true;
                    }
                    // 宝箱６
                    else if (row == 27 && column == 34 && !GroundOne.WE.TruthTreasure26 && eventNum == 83)
                    {
                        return true;
                    }
                    // 宝箱７
                    else if (row == 34 && column == 34 && !GroundOne.WE.TruthTreasure27 && eventNum == 84)
                    {
                        return true;
                    }
                    // 宝箱８
                    else if (row == 34 && column == 46 && !GroundOne.WE.TruthTreasure28 && eventNum == 85)
                    {
                        return true;
                    }
                    // 宝箱９
                    else if (row == 34 && column == 58 && !GroundOne.WE.TruthTreasure29 && eventNum == 86)
                    {
                        return true;
                    }
                    // 宝箱１０
                    else if (row == 39 && column == 31 && !GroundOne.WE.TruthTreasure210 && eventNum == 87)
                    {
                        return true;
                    }
                    // 宝箱１１
                    else if (row == 15 && column == 13 && !GroundOne.WE.TruthTreasure211 && eventNum == 88)
                    {
                        return true;
                    }
                    // 宝箱１２
                    else if (row == 6 && column == 29 && !GroundOne.WE.TruthTreasure212 && eventNum == 89)
                    {
                        return true;
                    }
                    // 宝箱１３
                    else if (row == 39 && column == 23 && !GroundOne.WE.TruthTreasure213 && eventNum == 90)
                    {
                        return true;
                    }
                    // 宝箱１４
                    else if (row == 31 && column == 22 && !GroundOne.WE.TruthTreasure214 && eventNum == 91)
                    {
                        return true;
                    }
                    // 宝箱１５
                    else if (row == 19 && column == 22 && !GroundOne.WE.TruthTreasure215 && eventNum == 92)
                    {
                        return true;
                    }
                    // 宝箱１６
                    else if (row == 19 && column == 4 && !GroundOne.WE.TruthTreasure216 && eventNum == 93)
                    {
                        return true;
                    }
                    // 宝箱１７
                    else if (row == 28 && column == 6 && !GroundOne.WE.TruthTreasure217 && eventNum == 94)
                    {
                        return true;
                    }
                    // 宝箱１８
                    else if (row == 39 && column == 10 && !GroundOne.WE.TruthTreasure218 && eventNum == 95)
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
                    if (row == 17 && column == 45 && eventNum == 186)
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

        private void MessageProgress(int ii)
        {

        }


        private bool GetTreasure(string targetItemName)
        {
            return GetTreasure(targetItemName, false);
        }
        private bool GetTreasure(string targetItemName, bool MustGetIt)
        {
            CancelKeyDownMovement();
            return true; // [todo] アイテム追加を実装してください。

            //using (OKRequest ok = new OKRequest())
            //{
            //    ok.StartPosition = FormStartPosition.Manual;
            //    ok.Location = new Point(this.transform.position.x + 904, this.transform.position.y + 708);
            //    if (MustGetIt == false)
            //    {
            //        UpdateMainMessage("アイン:よっしゃ！お宝だぜ！");
            //    }
            //    ItemBackPack backpackData = new ItemBackPack(targetItemName);
            //    // [警告]：芋プログラミングです。整備してください。
            //    bool result1 = GroundOne.MC.AddBackPack(backpackData);
            //    if (result1)
            //    {
            //        UpdateMainMessage("『" + backpackData.Name + "を手に入れました』");
            //        return true;
            //    }
            //    else
            //    {
            //        if (GroundOne.WE.AvailableSecondCharacter)
            //        {
            //            bool result2 = sc.AddBackPack(backpackData);
            //            if (result2)
            //            {
            //                UpdateMainMessage("『" + backpackData.Name + "を手に入れました』");
            //                return true;
            //            }
            //            else
            //            {
            //                if (GroundOne.WE.AvailableThirdCharacter)
            //                {
            //                    bool result3 = tc.AddBackPack(backpackData);
            //                    if (result3)
            //                    {
            //                        UpdateMainMessage("『" + backpackData.Name + "を手に入れました』");
            //                        return true;
            //                    }
            //                    else
            //                    {
            //                        UpdateMainMessage("荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。");
            //                        return false;
            //                    }
            //                }
            //                else
            //                {
            //                    UpdateMainMessage("荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。");
            //                    return false;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            UpdateMainMessage("荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。");
            //            return false;
            //        }
            //    }
            //}
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
                        break;
                    case 7:
                        GroundOne.WE.TruthTreasure12 = GetTreasure(Database.POOR_HARD_SHOES);
                        break;
                    case 8:
                        GroundOne.WE.TruthTreasure13 = GetTreasure(Database.COMMON_SEAL_OF_POSION);
                        break;
                    case 9:
                        GroundOne.WE.TruthTreasure14 = GetTreasure(Database.COMMON_GREEN_EGG_KAIGARA);
                        break;
                    case 10:
                        GroundOne.WE.TruthTreasure15 = GetTreasure(Database.COMMON_CHARM_OF_FIRE_ANGEL);
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
                        break;
                    case 23:
                        GroundOne.WE.TruthTreasure122 = GetTreasure(Database.COMMON_VIKING_SWORD);
                        break;
                    case 24:
                        GroundOne.WE.TruthTreasure123 = GetTreasure(Database.COMMON_NEBARIITO_KUMO);
                        break;
                    case 25:
                        GroundOne.WE.TruthTreasure124 = GetTreasure(Database.COMMON_SUN_PRISM);
                        break;
                    case 26:
                        GroundOne.WE.TruthTreasure125 = GetTreasure(Database.COMMON_POISON_EKISU);
                        break;
                    case 27:
                        GroundOne.WE.TruthTreasure126 = GetTreasure(Database.COMMON_SOLID_CLAW);
                        break;
                    case 28:
                        GroundOne.WE.TruthTreasure127 = GetTreasure(Database.COMMON_GREEN_LEEF_CHARM);
                        break;
                    case 29:
                        GroundOne.WE.TruthTreasure128 = GetTreasure(Database.COMMON_WARRIOR_MEDAL);
                        break;
                    case 30:
                        GroundOne.WE.TruthTreasure129 = GetTreasure(Database.COMMON_PALADIN_MEDAL);
                        break;
                    case 31:
                        GroundOne.WE.TruthTreasure1210 = GetTreasure(Database.COMMON_KASHI_ROD);
                        break;
                    case 32:
                        GroundOne.WE.TruthTreasure1211 = GetTreasure(Database.RARE_TOTAL_HIYAKU_KASSEI);
                        break;
                    case 33:
                        GroundOne.WE.TruthTreasure1212 = GetTreasure(Database.RARE_ZEPHER_FETHER);
                        break;
                    case 34:
                        GroundOne.WE.TruthTreasure131 = GetTreasure(Database.COMMON_HAYATE_ORB);
                        break;
                    case 35:
                        GroundOne.WE.TruthTreasure132 = GetTreasure(Database.COMMON_BLUE_COPPER);
                        break;
                    case 36:
                        GroundOne.WE.TruthTreasure133 = GetTreasure(Database.COMMON_ORANGE_MATERIAL);
                        break;
                    case 37:
                        GroundOne.WE.TruthTreasure134 = GetTreasure(Database.RARE_LIFE_SWORD);
                        break;
                    case 38:
                        GroundOne.WE.TruthTreasure141 = GetTreasure(Database.RARE_PURE_WATER);
                        break;
                    case 39:
                        GroundOne.WE.TruthTreasure142 = GetTreasure(Database.RARE_PURE_GREEN_SILK_ROBE);
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
            return false;
        }

        private void ReturnToNormal()
        {
            //this.BackColor = Color.RoyalBlue; // [todo] 背景を標準色に戻す
        }

        private void TurnToBlack()
        {
            //this.BackColor = Color.Black; // [todo] 背景を黒色に変更する。
        }

        private bool EncountBattle(string enemyName, string enemyName2, string enemyName3, bool duel, bool hiSpeed, bool final, bool lifecount)
        {
            CancelKeyDownMovement();

            GroundOne.StopDungeonMusic();

            bool endFlag = false;
            while (!endFlag)
            {
                System.Threading.Thread.Sleep(500);
                TruthBattleEnemy be = new TruthBattleEnemy();
                {

                    MainCharacter tempMC = new MainCharacter();
                    MainCharacter tempSC = new MainCharacter();
                    MainCharacter tempTC = new MainCharacter();
                    WorldEnvironment tempWE = new WorldEnvironment();
                    TruthWorldEnvironment tempWE2 = new TruthWorldEnvironment();

                    //tempGroundOne.MC.MainArmor = this.GroundOne.MC.MainArmor;
                    //tempGroundOne.MC.SubWeapon = this.GroundOne.MC.SubWeapon;
                    //tempGroundOne.MC.MainWeapon = this.GroundOne.MC.MainWeapon;
                    //tempGroundOne.MC.Accessory = this.GroundOne.MC.Accessory;
                    //tempGroundOne.MC.Accessory2 = this.GroundOne.MC.Accessory2;

                    //tempSC.MainArmor = this.SC.MainArmor;
                    //tempSC.SubWeapon = this.SC.SubWeapon;
                    //tempSC.MainWeapon = this.SC.MainWeapon;
                    //tempSC.Accessory = this.SC.Accessory;
                    //tempSC.Accessory2 = this.SC.Accessory2;

                    //tempTC.MainArmor = this.TC.MainArmor;
                    //tempTC.SubWeapon = this.TC.SubWeapon;
                    //tempTC.MainWeapon = this.TC.MainWeapon;
                    //tempTC.Accessory = this.TC.Accessory;
                    //tempTC.Accessory2 = this.TC.Accessory2;

                    //ItemBackPack[] tempBackPack = new ItemBackPack[this.GroundOne.MC.GetBackPackInfo().Length];
                    //tempBackPack = GroundOne.MC.GetBackPackInfo();
                    //be.MC = tempMC;
                    //for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //{
                    //    if (tempBackPack[ii] != null)
                    //    {
                    //        int stack = tempBackPack[ii].StackValue;
                    //        for (int jj = 0; jj < stack; jj++)
                    //        {
                    //            be.GroundOne.MC.AddBackPack(tempBackPack[ii]);
                    //        }
                    //    }
                    //}

                    //if (GroundOne.WE.AvailableSecondCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                    //    tempBackPack2 = sc.GetBackPackInfo();
                    //    be.SC = tempSC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack2[ii] != null)
                    //        {
                    //            int stack = tempBackPack2[ii].StackValue;
                    //            for (int jj = 0; jj < stack; jj++)
                    //            {
                    //                be.SC.AddBackPack(tempBackPack2[ii]);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    be.SC = null;
                    //}

                    //if (GroundOne.WE.AvailableThirdCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                    //    tempBackPack3 = tc.GetBackPackInfo();
                    //    be.TC = tempTC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack3[ii] != null)
                    //        {
                    //            int stack = tempBackPack3[ii].StackValue;
                    //            for (int jj = 0; jj < stack; jj++)
                    //            {
                    //                be.TC.AddBackPack(tempBackPack3[ii]);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    be.TC = null;
                    //}

                    //Type type = tempGroundOne.MC.GetType();
                    //foreach (PropertyInfo pi in type.GetProperties())
                    //{
                    //    // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                    //    if (pi.PropertyType == typeof(System.Int32))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                    //            pi.SetValue(tempSC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                    //            pi.SetValue(tempTC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.String))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (string)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                    //            pi.SetValue(tempSC, (string)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                    //            pi.SetValue(tempTC, (string)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.Boolean))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                    //            pi.SetValue(tempSC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                    //            pi.SetValue(tempTC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    // s 後編追加
                    //    else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (MainCharacter.PlayerStance)(Enum.Parse(typeof(MainCharacter.PlayerStance), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                    //            pi.SetValue(tempSC, (MainCharacter.PlayerStance)(Enum.Parse(typeof(MainCharacter.PlayerStance), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                    //            pi.SetValue(tempTC, (MainCharacter.PlayerStance)(Enum.Parse(typeof(MainCharacter.PlayerStance), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                    //        }
                    //        catch { }
                    //    }
                    //    // e 後編追加
                    //    // s 後編追加
                    //    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                    //            pi.SetValue(tempSC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                    //            pi.SetValue(tempTC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempMC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                    //            pi.SetValue(tempSC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                    //            pi.SetValue(tempTC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                    //        }
                    //        catch { }
                    //    }
                    //    // e 後編追加
                    //}


                    //Type type2 = tempGroundOne.WE.GetType();
                    //foreach (PropertyInfo pi in type2.GetProperties())
                    //{
                    //    // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                    //    if (pi.PropertyType == typeof(System.Int32))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.String))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE, (string)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.Boolean))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //}

                    //Type type3 = tempWE2.GetType();
                    //foreach (PropertyInfo pi in type3.GetProperties())
                    //{
                    //    // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                    //    if (pi.PropertyType == typeof(System.Int32))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.String))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //    else if (pi.PropertyType == typeof(System.Boolean))
                    //    {
                    //        try
                    //        {
                    //            pi.SetValue(tempWE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    //        }
                    //        catch { }
                    //    }
                    //}

                    //TruthEnemyCharacter ec1 = new TruthEnemyCharacter(enemyName);
                    //be.EC1 = ec1;
                    //// １階初期パーティが１人の場合を考慮して以下の形式
                    //if (GroundOne.WE.AvailableSecondCharacter && enemyName2 != String.Empty)
                    //{
                    //    if ((ec1.Rare == TruthEnemyCharacter.RareString.Black) ||
                    //        (ec1.Rare == TruthEnemyCharacter.RareString.Blue))
                    //    {
                    //        TruthEnemyCharacter ec2 = new TruthEnemyCharacter(enemyName2);
                    //        be.EC2 = ec2;
                    //    }
                    //}
                    //// ２階初期パーティが２人の場合を考慮して以下の形式
                    //if (GroundOne.WE.AvailableThirdCharacter && enemyName3 != String.Empty)
                    //{
                    //    if ((ec1.Rare == TruthEnemyCharacter.RareString.Black) ||
                    //        (ec1.Rare == TruthEnemyCharacter.RareString.Blue))
                    //    {
                    //        TruthEnemyCharacter ec3 = new TruthEnemyCharacter(enemyName3);
                    //        be.EC3 = ec3;
                    //    }
                    //}

                    //// ２階、力の部屋以降、ボスが２人以上を考慮して以下の形式
                    //if (enemyName2 != String.Empty)
                    //{
                    //    if (ec1.Rare == TruthEnemyCharacter.RareString.Gold)
                    //    {
                    //        TruthEnemyCharacter ec2 = new TruthEnemyCharacter(enemyName2);
                    //        be.EC2 = ec2;
                    //    }
                    //}
                    //if (enemyName3 != String.Empty)
                    //{
                    //    if (ec1.Rare == TruthEnemyCharacter.RareString.Gold)
                    //    {
                    //        TruthEnemyCharacter ec3 = new TruthEnemyCharacter(enemyName3);
                    //        be.EC3 = ec3;
                    //    }
                    //}

                    //be.WE = tempWE;
                    //be.StartPosition = FormStartPosition.CenterParent;
                    //be.IgnoreApplicationDoEvent = true;
                    //be.DuelMode = duel;
                    //be.HiSpeedAnimation = hiSpeed;
                    //be.FinalBattle = final;
                    //be.LifeCountBattle = lifecount;

                    //be.ShowDialog();
                    //SceneMove.TBE = be;
                    GroundOne.enemyName1 = enemyName;
                    GroundOne.enemyName2 = enemyName2;
                    GroundOne.enemyName3 = enemyName3;
                    SceneDimension.Go(Database.TruthDungeon, Database.TruthBattleEnemy);
                    endFlag = true;
                    //if (be.DialogResult == DialogResult.Retry)
                    //{
                    //    // 死亡時、再挑戦する場合、はじめから呼びなおす。
                    //    this.Update();
                    //    continue;
                    //}
                    //if (be.DialogResult == DialogResult.Abort)
                    //{
                    //    // 逃げた時、経験値とゴールドは入らない。
                    //    this.MC = tempMC;
                    //    this.GroundOne.MC.ReplaceBackPack(tempGroundOne.MC.GetBackPackInfo());
                    //    if (GroundOne.WE.AvailableSecondCharacter)
                    //    {
                    //        this.SC = tempSC;
                    //        this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                    //    }
                    //    if (GroundOne.WE.AvailableThirdCharacter)
                    //    {
                    //        this.TC = tempTC;
                    //        this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                    //    }
                    //    this.WE = tempWE;

                    //    GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                    //    SetupPlayerStatus();
                    //    return false;
                    //}
                    //else if (be.DialogResult == DialogResult.Ignore)
                    //{
                    //    using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                    //    {
                    //        if (ec1.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                    //        {
                    //            UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    //        }
                    //        if (ec1.Name == Database.ENEMY_BOSS_LEVIATHAN)
                    //        {
                    //            UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);
                    //        }
                    //        if (ec1.Name == Database.ENEMY_BOSS_HOWLING_SEIZER)
                    //        {
                    //            UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    //        }
                    //        if (ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                    //        {
                    //            UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    //        }
                    //        if (ec1.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                    //        {
                    //            UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    //        }
                    //        //if (ec1.Name == Database.ENEMY_LAST_VERZE_ARTIE ||
                    //        //    ec1.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                    //        //{
                    //        //    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);
                    //        //}

                    //        // DUELモードは現実世界でDUEL戦闘となった時に再戦を判断させたいため、一旦ここでfalse返しとする。
                    //        if (duel)
                    //        {
                    //            return false;
                    //        }

                    //        yerw.StartPosition = FormStartPosition.CenterParent;
                    //        yerw.MainMessage = "タイトルへ戻ります。今までのデータをセーブしますか？";
                    //        yerw.ShowDialog();
                    //        if (yerw.DialogResult == DialogResult.Yes)
                    //        {
                    //            using (ESCMenu esc = new ESCMenu())
                    //            {
                    //                esc.MC = this.MC;
                    //                esc.SC = this.SC;
                    //                esc.TC = this.TC;
                    //                esc.WE = this.WE;
                    //                esc.KnownTileInfo = this.knownTileInfo;
                    //                esc.KnownTileInfo2 = this.knownTileInfo2;
                    //                esc.KnownTileInfo3 = this.knownTileInfo3;
                    //                esc.KnownTileInfo4 = this.knownTileInfo4;
                    //                esc.KnownTileInfo5 = this.knownTileInfo5;
                    //                esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                    //                esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                    //                esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                    //                esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                    //                esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                    //                esc.StartPosition = FormStartPosition.CenterParent;
                    //                esc.OnlySave = true;
                    //                esc.TruthStory = true;
                    //                esc.ShowDialog();
                    //            }
                    //        }
                    //    }
                    //    endFlag = true;
                    //    this.Visible = false;
                    //}
                    //else
                    //{
                    //    bool alreadyPlayBackMusic = false;
                    //    if (GroundOne.WE.AvailableFirstCharacter)
                    //    {
                    //        this.MC = tempMC;
                    //        this.GroundOne.MC.ReplaceBackPack(tempGroundOne.MC.GetBackPackInfo());
                    //        if (GroundOne.MC.Level < Database.CHARACTER_MAX_LEVEL5)
                    //        {
                    //            GroundOne.MC.Exp += be.EC1.Exp;
                    //        }
                    //        GroundOne.MC.Gold += be.EC1.Gold;

                    //        int levelUpPoint = 0;
                    //        int cumultiveLvUpValue = 0;
                    //        while (true)
                    //        {
                    //            if (GroundOne.MC.Exp >= GroundOne.MC.NextLevelBorder && GroundOne.MC.Level < Database.CHARACTER_MAX_LEVEL5)
                    //            {
                    //                levelUpPoint += GroundOne.MC.LevelUpPointTruth;
                    //                GroundOne.MC.BaseLife += GroundOne.MC.LevelUpLifeTruth;
                    //                GroundOne.MC.BaseMana += GroundOne.MC.LevelUpManaTruth;
                    //                GroundOne.MC.Exp = GroundOne.MC.Exp - GroundOne.MC.NextLevelBorder;
                    //                GroundOne.MC.Level += 1;
                    //                cumultiveLvUpValue++;
                    //            }
                    //            else
                    //            {
                    //                break;
                    //            }
                    //        }

                    //        if (cumultiveLvUpValue > 0)
                    //        {
                    //            GroundOne.PlaySoundEffect("LvUp.mp3");
                    //            if (!alreadyPlayBackMusic)
                    //            {
                    //                alreadyPlayBackMusic = true;
                    //                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                    //            }
                    //            using (TruthStatusPlayer sp = new TruthStatusPlayer())
                    //            {
                    //                sp.WE = we;
                    //                sp.MC = mc;
                    //                sp.SC = sc;
                    //                sp.TC = tc;
                    //                sp.CurrentStatusView = GroundOne.MC.PlayerStatusColor;
                    //                sp.LevelUp = true;
                    //                sp.UpPoint = levelUpPoint;
                    //                sp.CumultiveLvUpValue = cumultiveLvUpValue;
                    //                sp.StartPosition = FormStartPosition.CenterParent;
                    //                sp.ShowDialog();
                    //            }

                    //            #region "アイン・レベルアップ習得表"
                    //            if ((GroundOne.MC.Level >= 3) && (!GroundOne.MC.StraightSmash)) { GroundOne.MC.AvailableSkill = true; GroundOne.MC.StraightSmash = true; ShowActiveSkillSpell(mc, Database.STRAIGHT_SMASH); GroundOne.MC.BattleActionCommand3 = Database.STRAIGHT_SMASH; }
                    //            if ((GroundOne.MC.Level >= 4) && (!GroundOne.MC.FreshHeal)) { GroundOne.MC.AvailableMana = true; GroundOne.MC.FreshHeal = true; ShowActiveSkillSpell(mc, Database.FRESH_HEAL); GroundOne.MC.BattleActionCommand4 = Database.FRESH_HEAL; }
                    //            if ((GroundOne.MC.Level >= 5) && (!GroundOne.MC.FireBall)) { GroundOne.MC.FireBall = true; ShowActiveSkillSpell(mc, Database.FIRE_BALL); GroundOne.MC.BattleActionCommand5 = Database.FIRE_BALL; }
                    //            if ((GroundOne.MC.Level >= 6) && (!GroundOne.MC.Protection)) { GroundOne.MC.Protection = true; ShowActiveSkillSpell(mc, Database.PROTECTION); GroundOne.MC.BattleActionCommand6 = Database.PROTECTION; }
                    //            if ((GroundOne.MC.Level >= 7) && (!GroundOne.MC.DoubleSlash)) { GroundOne.MC.DoubleSlash = true; ShowActiveSkillSpell(mc, Database.DOUBLE_SLASH); GroundOne.MC.BattleActionCommand7 = Database.DOUBLE_SLASH; }
                    //            if ((GroundOne.MC.Level >= 8) && (!GroundOne.MC.FlameAura)) { GroundOne.MC.FlameAura = true; ShowActiveSkillSpell(mc, Database.FLAME_AURA); GroundOne.MC.BattleActionCommand8 = Database.FLAME_AURA; }
                    //            if ((GroundOne.MC.Level >= 9) && (!GroundOne.MC.StanceOfStanding)) { GroundOne.MC.StanceOfStanding = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_STANDING); GroundOne.MC.BattleActionCommand9 = Database.STANCE_OF_STANDING; }
                    //            if ((GroundOne.MC.Level >= 10) && (!GroundOne.MC.WordOfPower)) { GroundOne.MC.WordOfPower = true; ShowActiveSkillSpell(mc, Database.WORD_OF_POWER); }
                    //            if ((GroundOne.MC.Level >= 11) && (!GroundOne.MC.HolyShock)) { GroundOne.MC.HolyShock = true; ShowActiveSkillSpell(mc, Database.HOLY_SHOCK); }
                    //            if ((GroundOne.MC.Level >= 12) && (!GroundOne.MC.TruthVision)) { GroundOne.MC.TruthVision = true; ShowActiveSkillSpell(mc, Database.TRUTH_VISION); }
                    //            if ((GroundOne.MC.Level >= 13) && (!GroundOne.MC.HeatBoost)) { GroundOne.MC.HeatBoost = true; ShowActiveSkillSpell(mc, Database.HEAT_BOOST); }
                    //            if ((GroundOne.MC.Level >= 14) && (!GroundOne.MC.SaintPower)) { GroundOne.MC.SaintPower = true; ShowActiveSkillSpell(mc, Database.SAINT_POWER); }
                    //            if ((GroundOne.MC.Level >= 15) && (!GroundOne.MC.GaleWind)) { GroundOne.MC.GaleWind = true; ShowActiveSkillSpell(mc, Database.GALE_WIND); }
                    //            if ((GroundOne.MC.Level >= 16) && (!GroundOne.MC.InnerInspiration)) { GroundOne.MC.InnerInspiration = true; ShowActiveSkillSpell(mc, Database.INNER_INSPIRATION); }
                    //            if ((GroundOne.MC.Level >= 17) && (!GroundOne.MC.WordOfLife)) { GroundOne.MC.WordOfLife = true; ShowActiveSkillSpell(mc, Database.WORD_OF_LIFE); }
                    //            if ((GroundOne.MC.Level >= 18) && (!GroundOne.MC.FlameStrike)) { GroundOne.MC.FlameStrike = true; ShowActiveSkillSpell(mc, Database.FLAME_STRIKE); }
                    //            if ((GroundOne.MC.Level >= 19) && (!GroundOne.MC.HighEmotionality)) { GroundOne.MC.HighEmotionality = true; ShowActiveSkillSpell(mc, Database.HIGH_EMOTIONALITY); }
                    //            if ((GroundOne.MC.Level >= 20) && (!GroundOne.MC.WordOfFortune)) { GroundOne.MC.WordOfFortune = true; ShowActiveSkillSpell(mc, Database.WORD_OF_FORTUNE); }
                    //            // [警告] ここで一気にレベルを挙げられると、複合魔法・スキルの習得に違和感が出てしまう。
                    //            // 複合魔法・スキルはガンツ武具屋のテレポート先、カール爵より習得するようにする。
                    //            if ((GroundOne.MC.Level >= 24) && (!GroundOne.MC.Glory)) { GroundOne.MC.Glory = true; ShowActiveSkillSpell(mc, Database.GLORY); }
                    //            if ((GroundOne.MC.Level >= 25) && (!GroundOne.MC.VolcanicWave)) { GroundOne.MC.VolcanicWave = true; ShowActiveSkillSpell(mc, Database.VOLCANIC_WAVE); }
                    //            if ((GroundOne.MC.Level >= 26) && (!GroundOne.MC.AetherDrive)) { GroundOne.MC.AetherDrive = true; ShowActiveSkillSpell(mc, Database.AETHER_DRIVE); }

                    //            if ((GroundOne.MC.Level >= 36) && (!GroundOne.MC.CrushingBlow)) { GroundOne.MC.CrushingBlow = true; ShowActiveSkillSpell(mc, Database.CRUSHING_BLOW); }
                    //            if ((GroundOne.MC.Level >= 37) && (!GroundOne.MC.KineticSmash)) { GroundOne.MC.KineticSmash = true; ShowActiveSkillSpell(mc, Database.KINETIC_SMASH); }
                    //            if ((GroundOne.MC.Level >= 38) && (!GroundOne.MC.StanceOfEyes)) { GroundOne.MC.StanceOfEyes = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_EYES); }
                    //            if ((GroundOne.MC.Level >= 39) && (!GroundOne.MC.Resurrection)) { GroundOne.MC.Resurrection = true; ShowActiveSkillSpell(mc, Database.RESURRECTION); }
                    //            if ((GroundOne.MC.Level >= 41) && (!GroundOne.MC.StaticBarrier)) { GroundOne.MC.StaticBarrier = true; ShowActiveSkillSpell(mc, Database.STATIC_BARRIER); }
                    //            if ((GroundOne.MC.Level >= 42) && (!GroundOne.MC.Genesis)) { GroundOne.MC.Genesis = true; ShowActiveSkillSpell(mc, Database.GENESIS); }
                    //            if ((GroundOne.MC.Level >= 43) && (!GroundOne.MC.LightDetonator)) { GroundOne.MC.LightDetonator = true; ShowActiveSkillSpell(mc, Database.LIGHT_DETONATOR); }
                    //            if ((GroundOne.MC.Level >= 44) && (!GroundOne.MC.ImmortalRave)) { GroundOne.MC.ImmortalRave = true; ShowActiveSkillSpell(mc, Database.IMMORTAL_RAVE); }
                    //            if ((GroundOne.MC.Level >= 45) && (!GroundOne.MC.ExaltedField)) { GroundOne.MC.ExaltedField = true; ShowActiveSkillSpell(mc, Database.EXALTED_FIELD); }
                    //            if ((GroundOne.MC.Level >= 46) && (!GroundOne.MC.PiercingFlame)) { GroundOne.MC.PiercingFlame = true; ShowActiveSkillSpell(mc, Database.PIERCING_FLAME); }
                    //            if ((GroundOne.MC.Level >= 47) && (!GroundOne.MC.SacredHeal)) { GroundOne.MC.SacredHeal = true; ShowActiveSkillSpell(mc, Database.SACRED_HEAL); }
                    //            if ((GroundOne.MC.Level >= 48) && (!GroundOne.MC.RisingAura)) { GroundOne.MC.RisingAura = true; ShowActiveSkillSpell(mc, Database.RISING_AURA); }
                    //            if ((GroundOne.MC.Level >= 49) && (!GroundOne.MC.ChillBurn)) { GroundOne.MC.ChillBurn = true; ShowActiveSkillSpell(mc, Database.CHILL_BURN); }
                    //            if ((GroundOne.MC.Level >= 50) && (!GroundOne.MC.SoulInfinity)) { GroundOne.MC.SoulInfinity = true; ShowActiveSkillSpell(mc, Database.SOUL_INFINITY); }

                    //            if ((GroundOne.MC.Level >= 51) && (!GroundOne.MC.HymnContract)) { GroundOne.MC.HymnContract = true; ShowActiveSkillSpell(mc, Database.HYMN_CONTRACT); }
                    //            if ((GroundOne.MC.Level >= 52) && (!GroundOne.MC.Catastrophe)) { GroundOne.MC.Catastrophe = true; ShowActiveSkillSpell(mc, Database.CATASTROPHE); }
                    //            if ((GroundOne.MC.Level >= 53) && (!GroundOne.MC.CelestialNova)) { GroundOne.MC.CelestialNova = true; ShowActiveSkillSpell(mc, Database.CELESTIAL_NOVA); }
                    //            if ((GroundOne.MC.Level >= 54) && (!GroundOne.MC.OnslaughtHit)) { GroundOne.MC.OnslaughtHit = true; ShowActiveSkillSpell(mc, Database.ONSLAUGHT_HIT); }
                    //            if ((GroundOne.MC.Level >= 55) && (!GroundOne.MC.PainfulInsanity)) { GroundOne.MC.PainfulInsanity = true; ShowActiveSkillSpell(mc, Database.PAINFUL_INSANITY); }
                    //            if ((GroundOne.MC.Level >= 56) && (!GroundOne.MC.LavaAnnihilation)) { GroundOne.MC.LavaAnnihilation = true; ShowActiveSkillSpell(mc, Database.LAVA_ANNIHILATION); }
                    //            if ((GroundOne.MC.Level >= 57) && (!GroundOne.MC.ConcussiveHit)) { GroundOne.MC.ConcussiveHit = true; ShowActiveSkillSpell(mc, Database.CONCUSSIVE_HIT); }
                    //            if ((GroundOne.MC.Level >= 58) && (!GroundOne.MC.EternalPresence)) { GroundOne.MC.EternalPresence = true; ShowActiveSkillSpell(mc, Database.ETERNAL_PRESENCE); }
                    //            if ((GroundOne.MC.Level >= 59) && (!GroundOne.MC.AusterityMatrix)) { GroundOne.MC.AusterityMatrix = true; ShowActiveSkillSpell(mc, Database.AUSTERITY_MATRIX); }
                    //            if ((GroundOne.MC.Level >= 60) && (!GroundOne.MC.SigilOfHomura)) { GroundOne.MC.SigilOfHomura = true; ShowActiveSkillSpell(mc, Database.SIGIL_OF_HOMURA); }

                    //            if ((GroundOne.MC.Level >= 61) && (!GroundOne.MC.EverDroplet)) { GroundOne.MC.EverDroplet = true; ShowActiveSkillSpell(mc, Database.EVER_DROPLET); }
                    //            if ((GroundOne.MC.Level >= 62) && (!GroundOne.MC.ONEAuthority)) { GroundOne.MC.ONEAuthority = true; ShowActiveSkillSpell(mc, Database.ONE_AUTHORITY); }
                    //            if ((GroundOne.MC.Level >= 63) && (!GroundOne.MC.AscendantMeteor)) { GroundOne.MC.AscendantMeteor = true; ShowActiveSkillSpell(mc, Database.ASCENDANT_METEOR); }
                    //            if ((GroundOne.MC.Level >= 64) && (!GroundOne.MC.FatalBlow)) { GroundOne.MC.FatalBlow = true; ShowActiveSkillSpell(mc, Database.FATAL_BLOW); }
                    //            if ((GroundOne.MC.Level >= 65) && (!GroundOne.MC.StanceOfDouble)) { GroundOne.MC.StanceOfDouble = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_DOUBLE); }
                    //            if ((GroundOne.MC.Level >= 66) && (!GroundOne.MC.ZetaExplosion)) { GroundOne.MC.ZetaExplosion = true; ShowActiveSkillSpell(mc, Database.ZETA_EXPLOSION); }
                    //            #endregion
                    //        }

                    //        bool detect = false;
                    //        string targetGetName = String.Empty;
                    //        if (GroundOne.MC.MainWeapon != null)
                    //        {
                    //            if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO && GroundOne.WE2.PracticeSwordCount >= 5)
                    //            {
                    //                targetGetName = Database.POOR_PRACTICE_SWORD_1;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_1 && GroundOne.WE2.PracticeSwordCount >= 15)
                    //            {
                    //                targetGetName = Database.POOR_PRACTICE_SWORD_2;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_2 && GroundOne.WE2.PracticeSwordCount >= 30 && GroundOne.WE.CompleteSlayBoss2)
                    //            {
                    //                targetGetName = Database.COMMON_PRACTICE_SWORD_3;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_3 && GroundOne.WE2.PracticeSwordCount >= 50 && GroundOne.WE.CompleteSlayBoss2)
                    //            {
                    //                targetGetName = Database.COMMON_PRACTICE_SWORD_4;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_4 && GroundOne.WE2.PracticeSwordCount >= 75 && GroundOne.WE.CompleteSlayBoss3)
                    //            {
                    //                targetGetName = Database.RARE_PRACTICE_SWORD_5;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_5 && GroundOne.WE2.PracticeSwordCount >= 105 && GroundOne.WE.CompleteSlayBoss3)
                    //            {
                    //                targetGetName = Database.RARE_PRACTICE_SWORD_6;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_6 && GroundOne.WE2.PracticeSwordCount >= 140 && GroundOne.WE.CompleteSlayBoss4)
                    //            {
                    //                targetGetName = Database.EPIC_PRACTICE_SWORD_7;
                    //            }
                    //            else if (GroundOne.MC.MainWeapon.Name == Database.EPIC_PRACTICE_SWORD_7 && GroundOne.WE2.PracticeSwordCount >= 180 && GroundOne.WE.CompleteSlayBoss5)
                    //            {
                    //                targetGetName = Database.LEGENDARY_FELTUS;
                    //            }

                    //            if (targetGetName != String.Empty)
                    //            {
                    //                GroundOne.MC.MainWeapon = new ItemBackPack(targetGetName);
                    //                detect = true;
                    //            }
                    //        }
                    //        if ((GroundOne.MC.SubWeapon != null))
                    //        {
                    //            if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO && GroundOne.WE2.PracticeSwordCount >= 5)
                    //            {
                    //                targetGetName = Database.POOR_PRACTICE_SWORD_1;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_1 && GroundOne.WE2.PracticeSwordCount >= 20)
                    //            {
                    //                targetGetName = Database.POOR_PRACTICE_SWORD_2;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_2 && GroundOne.WE2.PracticeSwordCount >= 50 && GroundOne.WE.CompleteSlayBoss2)
                    //            {
                    //                targetGetName = Database.COMMON_PRACTICE_SWORD_3;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.COMMON_PRACTICE_SWORD_3 && GroundOne.WE2.PracticeSwordCount >= 100 && GroundOne.WE.CompleteSlayBoss2)
                    //            {
                    //                targetGetName = Database.COMMON_PRACTICE_SWORD_4;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.COMMON_PRACTICE_SWORD_4 && GroundOne.WE2.PracticeSwordCount >= 200 && GroundOne.WE.CompleteSlayBoss3)
                    //            {
                    //                targetGetName = Database.RARE_PRACTICE_SWORD_5;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.RARE_PRACTICE_SWORD_5 && GroundOne.WE2.PracticeSwordCount >= 400 && GroundOne.WE.CompleteSlayBoss3)
                    //            {
                    //                targetGetName = Database.RARE_PRACTICE_SWORD_6;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.RARE_PRACTICE_SWORD_6 && GroundOne.WE2.PracticeSwordCount >= 700 && GroundOne.WE.CompleteSlayBoss4)
                    //            {
                    //                targetGetName = Database.EPIC_PRACTICE_SWORD_7;
                    //            }
                    //            else if (GroundOne.MC.SubWeapon.Name == Database.EPIC_PRACTICE_SWORD_7 && GroundOne.WE2.PracticeSwordCount >= 1000 && GroundOne.WE.CompleteSlayBoss5)
                    //            {
                    //                targetGetName = Database.LEGENDARY_FELTUS;
                    //            }

                    //            if (targetGetName != String.Empty)
                    //            {
                    //                GroundOne.MC.SubWeapon = new ItemBackPack(targetGetName);
                    //                detect = true;
                    //            }
                    //        }

                    //        if (detect)
                    //        {
                    //            if (targetGetName == Database.POOR_PRACTICE_SWORD_1)
                    //            {
                    //                UpdateMainMessage("アイン：（この剣・・・）");

                    //                UpdateMainMessage("アイン：（何か持つ感触が変わったな。以前より鋭くなった感じがする。）");

                    //                UpdateMainMessage("アイン：（すげえ・・・ひょっとして成長する剣だったりするのか！？）");

                    //                UpdateMainMessage("アイン：（サンキューガンツ伯父さん、ありがたく使わせてもらうぜ。）");
                    //            }
                    //            else if (targetGetName == Database.POOR_PRACTICE_SWORD_2)
                    //            {
                    //                UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                    //                UpdateMainMessage("アイン：（でも、何だろうな・・・何か違う感じもするが・・・）");

                    //                UpdateMainMessage("アイン：（まあ、良いか。このままガンガン使っていくぜ！）");
                    //            }
                    //            else if (targetGetName == Database.COMMON_PRACTICE_SWORD_3)
                    //            {
                    //                UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                    //                UpdateMainMessage("アイン：（すげえぜ、この剣。最大値ばかりが上がって行くな。）");

                    //                UpdateMainMessage("アイン：（使いこなせるかどうかだが・・・）");

                    //                UpdateMainMessage("アイン：（まあ、この際だ。使えるだけ使ってみるとするか！）");
                    //            }
                    //            else if (targetGetName == Database.COMMON_PRACTICE_SWORD_4)
                    //            {
                    //                UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                    //                UpdateMainMessage("アイン：（しかしどんどん値が伸びていくな・・・）");

                    //                UpdateMainMessage("アイン：（今の俺でどこまで使いこなせるか、わかんねえけどな）");

                    //                UpdateMainMessage("アイン：（まあ気にしててもしょうがねえ、ドンドン上げていくぜ！）");
                    //            }
                    //            else if (targetGetName == Database.RARE_PRACTICE_SWORD_5)
                    //            {
                    //                UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                    //                UpdateMainMessage("アイン：（しかし、新しくなってるハズなんだが・・・");

                    //                UpdateMainMessage("アイン：（何となく懐かしい感じもするんだよな）");

                    //                UpdateMainMessage("アイン：（MAXまで上げきったら、ガンツ伯父さんにでも聞いてみるか）");
                    //            }
                    //            else if (targetGetName == Database.RARE_PRACTICE_SWORD_6)
                    //            {
                    //                UpdateMainMessage("アイン：（っしゃ、来たぜ！剣レベルアップ！）");

                    //                UpdateMainMessage("アイン：（練習用の剣なんて、大嘘もいいとこじゃねえか）");

                    //                UpdateMainMessage("アイン：（・・・この、モヤモヤした感覚・・・）");

                    //                UpdateMainMessage("アイン：（・・・　・・・まあ、上げてくか！）");
                    //            }
                    //            else if (targetGetName == Database.EPIC_PRACTICE_SWORD_7)
                    //            {
                    //                UpdateMainMessage("アイン：（剣レベルアップ・・・と言いたい所だが）");

                    //                UpdateMainMessage("アイン：（そっか・・・何を忘れてたんだろうな、俺）");

                    //                UpdateMainMessage("アイン：（ッハハハ・・・そりゃ、そうだよな。情けねえぜ）");

                    //                UpdateMainMessage("アイン：（たぶん次でラストのレベルアップだ。やらせてもらうぜ）");
                    //            }
                    //            else if (targetGetName == Database.LEGENDARY_FELTUS)
                    //            {
                    //                // [コメント] 何か演出が欲しい。
                    //                UpdateMainMessage("アイン：（神剣、フェルトゥーシュだったんだ、コレ・・・）");

                    //                UpdateMainMessage("アイン：（そうさ・・・俺はコイツで・・・）");

                    //                UpdateMainMessage("アイン：（いや、これを受け止めなければならないんだ、俺は）");

                    //                UpdateMainMessage("アイン：（・・・今度こそ、心に決めたぜ）");

                    //                UpdateMainMessage("アイン：（この剣からは逃げない）");

                    //                UpdateMainMessage("アイン：（っしゃ！　それじゃ使うぜ、フェルトゥーシュを！）");
                    //            }
                    //            using (MessageDisplayWithIcon mdwi = new MessageDisplayWithIcon())
                    //            {
                    //                ItemBackPack item = new ItemBackPack(targetGetName);
                    //                mdwi.Message = item.Name + "を入手した！";
                    //                mdwi.Item = item;
                    //                GroundOne.PlaySoundEffect(Database.SOUND_LVUP_FELTUS);
                    //                mdwi.StartPosition = FormStartPosition.CenterParent;
                    //                mdwi.ShowDialog();
                    //            }
                    //        }
                    //    }



                    //    if (GroundOne.WE.AvailableSecondCharacter)
                    //    {
                    //        this.SC = tempSC;
                    //        this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                    //        if (sc.Level < Database.CHARACTER_MAX_LEVEL5)
                    //        {
                    //            sc.Exp += be.EC1.Exp;
                    //        }
                    //        //SC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                    //        int levelUpPoint = 0;
                    //        int cumultiveLvUpValue = 0;
                    //        while (true)
                    //        {
                    //            if (sc.Exp >= sc.NextLevelBorder && sc.Level < Database.CHARACTER_MAX_LEVEL5)
                    //            {
                    //                levelUpPoint += sc.LevelUpPointTruth;
                    //                sc.BaseLife += sc.LevelUpLifeTruth;
                    //                sc.BaseMana += sc.LevelUpManaTruth;
                    //                sc.Exp = sc.Exp - sc.NextLevelBorder;
                    //                sc.Level += 1;
                    //                cumultiveLvUpValue++;
                    //            }
                    //            else
                    //            {
                    //                break;
                    //            }
                    //        }

                    //        if (cumultiveLvUpValue > 0)
                    //        {
                    //            GroundOne.PlaySoundEffect("LvUp.mp3");
                    //            if (!alreadyPlayBackMusic)
                    //            {
                    //                alreadyPlayBackMusic = true;
                    //                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                    //            }
                    //            using (TruthStatusPlayer sp = new TruthStatusPlayer())
                    //            {
                    //                sp.WE = we;
                    //                sp.MC = mc;
                    //                sp.SC = sc;
                    //                sp.TC = tc;
                    //                sp.CurrentStatusView = sc.PlayerStatusColor;
                    //                sp.LevelUp = true;
                    //                sp.UpPoint = levelUpPoint;
                    //                sp.CumultiveLvUpValue = cumultiveLvUpValue;
                    //                sp.StartPosition = FormStartPosition.CenterParent;
                    //                sp.ShowDialog();
                    //            }
                    //            #region "ラナ・レベルアップ習得表"
                    //            if ((sc.Level >= 3) && (!sc.IceNeedle)) { sc.AvailableMana = true; sc.IceNeedle = true; ShowActiveSkillSpell(sc, Database.ICE_NEEDLE); sc.BattleActionCommand3 = Database.ICE_NEEDLE; }
                    //            if ((sc.Level >= 4) && (!sc.CounterAttack)) { sc.AvailableSkill = true; sc.CounterAttack = true; ShowActiveSkillSpell(sc, Database.COUNTER_ATTACK); sc.BattleActionCommand4 = Database.COUNTER_ATTACK; }
                    //            if ((sc.Level >= 5) && (!sc.DarkBlast)) { sc.DarkBlast = true; ShowActiveSkillSpell(sc, Database.DARK_BLAST); sc.BattleActionCommand5 = Database.DARK_BLAST; }
                    //            if ((sc.Level >= 6) && (!sc.AbsorbWater)) { sc.AbsorbWater = true; ShowActiveSkillSpell(sc, Database.ABSORB_WATER); sc.BattleActionCommand6 = Database.ABSORB_WATER; }
                    //            if ((sc.Level >= 7) && (!sc.StanceOfFlow)) { sc.StanceOfFlow = true; ShowActiveSkillSpell(sc, Database.STANCE_OF_FLOW); sc.BattleActionCommand7 = Database.STANCE_OF_FLOW; }
                    //            if ((sc.Level >= 8) && (!sc.ShadowPact)) { sc.ShadowPact = true; ShowActiveSkillSpell(sc, Database.SHADOW_PACT); sc.BattleActionCommand8 = Database.SHADOW_PACT; }
                    //            if ((sc.Level >= 9) && (!sc.DispelMagic)) { sc.DispelMagic = true; ShowActiveSkillSpell(sc, Database.DISPEL_MAGIC); sc.BattleActionCommand9 = Database.DISPEL_MAGIC; }
                    //            if ((sc.Level >= 10) && (!sc.LifeTap)) { sc.LifeTap = true; ShowActiveSkillSpell(sc, Database.LIFE_TAP); }
                    //            if ((sc.Level >= 11) && (!sc.PurePurification)) { sc.PurePurification = true; ShowActiveSkillSpell(sc, Database.PURE_PURIFICATION); }
                    //            if ((sc.Level >= 12) && (!sc.EnigmaSence)) { sc.EnigmaSence = true; ShowActiveSkillSpell(sc, Database.ENIGMA_SENSE); }
                    //            if ((sc.Level >= 13) && (!sc.BlackContract)) { sc.BlackContract = true; ShowActiveSkillSpell(sc, Database.BLACK_CONTRACT); }
                    //            if ((sc.Level >= 14) && (!sc.Cleansing)) { sc.Cleansing = true; ShowActiveSkillSpell(sc, Database.CLEANSING); }
                    //            if ((sc.Level >= 15) && (!sc.Negate)) { sc.Negate = true; ShowActiveSkillSpell(sc, Database.NEGATE); }
                    //            if ((sc.Level >= 16) && (!sc.FrozenLance)) { sc.FrozenLance = true; ShowActiveSkillSpell(sc, Database.FROZEN_LANCE); }
                    //            if ((sc.Level >= 17) && (!sc.RiseOfImage)) { sc.RiseOfImage = true; ShowActiveSkillSpell(sc, Database.RISE_OF_IMAGE); }
                    //            if ((sc.Level >= 18) && (!sc.Deflection)) { sc.Deflection = true; ShowActiveSkillSpell(sc, Database.DEFLECTION); }
                    //            if ((sc.Level >= 19) && (!sc.Tranquility)) { sc.Tranquility = true; ShowActiveSkillSpell(sc, Database.TRANQUILITY); }
                    //            if ((sc.Level >= 20) && (!sc.VoidExtraction)) { sc.VoidExtraction = true; ShowActiveSkillSpell(sc, Database.VOID_EXTRACTION); }
                    //            // [警告] ここで一気にレベルを挙げられると、複合魔法・スキルの習得に違和感が出てしまう。
                    //            if ((sc.Level >= 24) && (!sc.DevouringPlague)) { sc.DevouringPlague = true; ShowActiveSkillSpell(sc, Database.DEVOURING_PLAGUE); }
                    //            if ((sc.Level >= 25) && (!sc.MirrorImage)) { sc.MirrorImage = true; ShowActiveSkillSpell(sc, Database.MIRROR_IMAGE); }
                    //            if ((sc.Level >= 26) && (!sc.OneImmunity)) { sc.OneImmunity = true; ShowActiveSkillSpell(sc, Database.ONE_IMMUNITY); }

                    //            if ((sc.Level >= 36) && (!sc.AntiStun)) { sc.AntiStun = true; ShowActiveSkillSpell(sc, Database.ANTI_STUN); }
                    //            if ((sc.Level >= 37) && (!sc.SilentRush)) { sc.SilentRush = true; ShowActiveSkillSpell(sc, Database.SILENT_RUSH); }
                    //            if ((sc.Level >= 38) && (!sc.CarnageRush)) { sc.CarnageRush = true; ShowActiveSkillSpell(sc, Database.CARNAGE_RUSH); }
                    //            if ((sc.Level >= 39) && (!sc.BloodyVengeance)) { sc.BloodyVengeance = true; ShowActiveSkillSpell(sc, Database.BLOODY_VENGEANCE); }
                    //            if ((sc.Level >= 41) && (!sc.SacredHeal)) { sc.SacredHeal = true; ShowActiveSkillSpell(sc, Database.SACRED_HEAL); }
                    //            if ((sc.Level >= 42) && (!sc.WhiteOut)) { sc.WhiteOut = true; ShowActiveSkillSpell(sc, Database.WHITE_OUT); }
                    //            if ((sc.Level >= 43) && (!sc.DeepMirror)) { sc.DeepMirror = true; ShowActiveSkillSpell(sc, Database.DEEP_MIRROR); }
                    //            if ((sc.Level >= 44) && (!sc.PromisedKnowledge)) { sc.PromisedKnowledge = true; ShowActiveSkillSpell(sc, Database.PROMISED_KNOWLEDGE); }
                    //            if ((sc.Level >= 45) && (!sc.DoomBlade)) { sc.DoomBlade = true; ShowActiveSkillSpell(sc, Database.DOOM_BLADE); }
                    //            if ((sc.Level >= 46) && (!sc.VortexField)) { sc.VortexField = true; ShowActiveSkillSpell(sc, Database.VORTEX_FIELD); }
                    //            if ((sc.Level >= 47) && (!sc.AngelBreath)) { sc.AngelBreath = true; ShowActiveSkillSpell(sc, Database.ANGEL_BREATH); }
                    //            if ((sc.Level >= 48) && (!sc.UnknownShock)) { sc.UnknownShock = true; ShowActiveSkillSpell(sc, Database.UNKNOWN_SHOCK); }
                    //            if ((sc.Level >= 49) && (!sc.BlindJustice)) { sc.BlindJustice = true; ShowActiveSkillSpell(sc, Database.BLIND_JUSTICE); }
                    //            if ((sc.Level >= 50) && (!sc.StanceOfDeath)) { sc.StanceOfDeath = true; ShowActiveSkillSpell(sc, Database.STANCE_OF_DEATH); }

                    //            if ((sc.Level >= 51) && (!sc.EclipseEnd)) { sc.EclipseEnd = true; ShowActiveSkillSpell(sc, Database.ECLIPSE_END); }
                    //            if ((sc.Level >= 52) && (!sc.OboroImpact)) { sc.OboroImpact = true; ShowActiveSkillSpell(sc, Database.OBORO_IMPACT); }
                    //            if ((sc.Level >= 53) && (!sc.Damnation)) { sc.Damnation = true; ShowActiveSkillSpell(sc, Database.DAMNATION); }
                    //            if ((sc.Level >= 54) && (!sc.MindKilling)) { sc.MindKilling = true; ShowActiveSkillSpell(sc, Database.MIND_KILLING); }
                    //            if ((sc.Level >= 55) && (!sc.NothingOfNothingness)) { sc.NothingOfNothingness = true; ShowActiveSkillSpell(sc, Database.NOTHING_OF_NOTHINGNESS); }
                    //            if ((sc.Level >= 56) && (!sc.AbsoluteZero)) { sc.AbsoluteZero = true; ShowActiveSkillSpell(sc, Database.ABSOLUTE_ZERO); }
                    //            if ((sc.Level >= 57) && (!sc.NourishSense)) { sc.NourishSense = true; ShowActiveSkillSpell(sc, Database.NOURISH_SENSE); }
                    //            if ((sc.Level >= 58) && (!sc.TimeStop)) { sc.TimeStop = true; ShowActiveSkillSpell(sc, Database.TIME_STOP); }
                    //            if ((sc.Level >= 59) && (!sc.EverDroplet)) { sc.EverDroplet = true; ShowActiveSkillSpell(sc, Database.EVER_DROPLET); }
                    //            if ((sc.Level >= 60) && (!sc.BlueDragonWill)) { sc.BlueDragonWill = true; ShowActiveSkillSpell(sc, Database.BLUE_DRAGON_WILL); }

                    //            if ((sc.Level >= 61) && (!sc.EndlessAnthem)) { sc.EndlessAnthem = true; ShowActiveSkillSpell(sc, Database.ENDLESS_ANTHEM); }
                    //            if ((sc.Level >= 62) && (!sc.ImpulseHit)) { sc.ImpulseHit = true; ShowActiveSkillSpell(sc, Database.IMPULSE_HIT); }
                    //            if ((sc.Level >= 63) && (!sc.DeathDeny)) { sc.DeathDeny = true; ShowActiveSkillSpell(sc, Database.DEATH_DENY); }
                    //            if ((sc.Level >= 64) && (!sc.ConcussiveHit)) { sc.ConcussiveHit = true; ShowActiveSkillSpell(sc, Database.CONCUSSIVE_HIT); }
                    //            if ((sc.Level >= 65) && (!sc.SoulExecution)) { sc.SoulExecution = true; ShowActiveSkillSpell(sc, Database.SOUL_EXECUTION); }
                    //            if ((sc.Level >= 66) && (!sc.TranscendentWish)) { sc.TranscendentWish = true; ShowActiveSkillSpell(sc, Database.TRANSCENDENT_WISH); }
                    //            #endregion
                    //        }
                    //    }

                    //    if (GroundOne.WE.AvailableThirdCharacter)
                    //    {
                    //        this.TC = tempTC;
                    //        this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                    //        if (tc.FullName == Database.OL_LANDIS_FULL)
                    //        {
                    //            if (tc.Level < Database.CHARACTER_MAX_LEVEL2)
                    //            {
                    //                tc.Exp += be.EC1.Exp;
                    //            }
                    //        }
                    //        else if (tc.FullName == Database.VERZE_ARTIE_FULL)
                    //        {
                    //            if (tc.Level < Database.CHARACTER_MAX_LEVEL5)
                    //            {
                    //                tc.Exp += be.EC1.Exp;
                    //            }
                    //        }
                    //        //TC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                    //        int levelUpPoint = 0;
                    //        int cumultiveLvUpValue = 0;
                    //        while (true)
                    //        {
                    //            if (tc.Exp >= tc.NextLevelBorder && tc.Level < Database.CHARACTER_MAX_LEVEL5)
                    //            {
                    //                levelUpPoint += tc.LevelUpPointTruth;
                    //                tc.BaseLife += tc.LevelUpLifeTruth;
                    //                tc.BaseMana += tc.LevelUpManaTruth;
                    //                tc.Exp = tc.Exp - tc.NextLevelBorder;
                    //                tc.Level += 1;
                    //                cumultiveLvUpValue++;
                    //            }
                    //            else
                    //            {
                    //                break;
                    //            }
                    //        }

                    //        if (cumultiveLvUpValue > 0)
                    //        {
                    //            GroundOne.PlaySoundEffect("LvUp.mp3");
                    //            if (!alreadyPlayBackMusic)
                    //            {
                    //                alreadyPlayBackMusic = true;
                    //                GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                    //            }
                    //            using (TruthStatusPlayer sp = new TruthStatusPlayer())
                    //            {
                    //                sp.WE = we;
                    //                sp.MC = mc;
                    //                sp.SC = sc;
                    //                sp.TC = tc;
                    //                sp.CurrentStatusView = tc.PlayerStatusColor;
                    //                sp.LevelUp = true;
                    //                sp.UpPoint = levelUpPoint;
                    //                sp.CumultiveLvUpValue = cumultiveLvUpValue;
                    //                sp.StartPosition = FormStartPosition.CenterParent;
                    //                sp.ShowDialog();
                    //            }

                    //            // ランディスはレベル上限MAX35からスタートのため、習得はない。

                    //            #region "ヴェルゼ・レベルアップ習得表"

                    //            if ((tc.Level >= 36) && (!tc.StanceOfEyes)) { tc.StanceOfEyes = true; ShowActiveSkillSpell(tc, Database.STANCE_OF_EYES); }
                    //            if ((tc.Level >= 37) && (!tc.SwiftStep)) { tc.SwiftStep = true; ShowActiveSkillSpell(tc, Database.SWIFT_STEP); }
                    //            if ((tc.Level >= 38) && (!tc.Resurrection)) { tc.Resurrection = true; ShowActiveSkillSpell(tc, Database.RESURRECTION); }
                    //            if ((tc.Level >= 39) && (!tc.BlindJustice)) { tc.BlindJustice = true; ShowActiveSkillSpell(tc, Database.BLIND_JUSTICE); }
                    //            if ((tc.Level >= 41) && (!tc.Genesis)) { tc.Genesis = true; ShowActiveSkillSpell(tc, Database.GENESIS); }
                    //            if ((tc.Level >= 42) && (!tc.DeepMirror)) { tc.DeepMirror = true; ShowActiveSkillSpell(tc, Database.DEEP_MIRROR); }
                    //            if ((tc.Level >= 43) && (!tc.ImmortalRave)) { tc.ImmortalRave = true; ShowActiveSkillSpell(tc, Database.IMMORTAL_RAVE); }
                    //            if ((tc.Level >= 44) && (!tc.DoomBlade)) { tc.DoomBlade = true; ShowActiveSkillSpell(tc, Database.DOOM_BLADE); }
                    //            if ((tc.Level >= 45) && (!tc.CarnageRush)) { tc.CarnageRush = true; ShowActiveSkillSpell(tc, Database.CARNAGE_RUSH); }
                    //            if ((tc.Level >= 46) && (!tc.ChillBurn)) { tc.ChillBurn = true; ShowActiveSkillSpell(tc, Database.CHILL_BURN); }
                    //            if ((tc.Level >= 47) && (!tc.WhiteOut)) { tc.WhiteOut = true; ShowActiveSkillSpell(tc, Database.WHITE_OUT); }
                    //            if ((tc.Level >= 48) && (!tc.PhantasmalWind)) { tc.PhantasmalWind = true; ShowActiveSkillSpell(tc, Database.PHANTASMAL_WIND); }
                    //            if ((tc.Level >= 49) && (!tc.PainfulInsanity)) { tc.PainfulInsanity = true; ShowActiveSkillSpell(tc, Database.PAINFUL_INSANITY); }
                    //            if ((tc.Level >= 50) && (!tc.FatalBlow)) { tc.FatalBlow = true; ShowActiveSkillSpell(tc, Database.FATAL_BLOW); }

                    //            if ((tc.Level >= 51) && (!tc.StaticBarrier)) { tc.StaticBarrier = true; ShowActiveSkillSpell(tc, Database.STATIC_BARRIER); }
                    //            if ((tc.Level >= 52) && (!tc.StanceOfDeath)) { tc.StanceOfDeath = true; ShowActiveSkillSpell(tc, Database.STANCE_OF_DEATH); }
                    //            if ((tc.Level >= 53) && (!tc.EverDroplet)) { tc.EverDroplet = true; ShowActiveSkillSpell(tc, Database.EVER_DROPLET); }
                    //            if ((tc.Level >= 54) && (!tc.Catastrophe)) { tc.Catastrophe = true; ShowActiveSkillSpell(tc, Database.CATASTROPHE); }
                    //            if ((tc.Level >= 55) && (!tc.CelestialNova)) { tc.CelestialNova = true; ShowActiveSkillSpell(tc, Database.CELESTIAL_NOVA); }
                    //            if ((tc.Level >= 56) && (!tc.MindKilling)) { tc.MindKilling = true; ShowActiveSkillSpell(tc, Database.MIND_KILLING); }
                    //            if ((tc.Level >= 57) && (!tc.NothingOfNothingness)) { tc.NothingOfNothingness = true; ShowActiveSkillSpell(tc, Database.NOTHING_OF_NOTHINGNESS); }
                    //            if ((tc.Level >= 58) && (!tc.AbsoluteZero)) { tc.AbsoluteZero = true; ShowActiveSkillSpell(tc, Database.ABSOLUTE_ZERO); }
                    //            if ((tc.Level >= 59) && (!tc.AusterityMatrix)) { tc.AusterityMatrix = true; ShowActiveSkillSpell(tc, Database.AUSTERITY_MATRIX); }
                    //            if ((tc.Level >= 60) && (!tc.VigorSense)) { tc.VigorSense = true; ShowActiveSkillSpell(tc, Database.VIGOR_SENSE); }

                    //            if ((tc.Level >= 61) && (!tc.LavaAnnihilation)) { tc.LavaAnnihilation = true; ShowActiveSkillSpell(tc, Database.LAVA_ANNIHILATION); }
                    //            if ((tc.Level >= 62) && (!tc.EclipseEnd)) { tc.EclipseEnd = true; ShowActiveSkillSpell(tc, Database.ECLIPSE_END); }
                    //            if ((tc.Level >= 63) && (!tc.TimeStop)) { tc.TimeStop = true; ShowActiveSkillSpell(tc, Database.TIME_STOP); }
                    //            if ((tc.Level >= 64) && (!tc.SinFortune)) { tc.SinFortune = true; ShowActiveSkillSpell(tc, Database.SIN_FORTUNE); }
                    //            if ((tc.Level >= 65) && (!tc.DemonicIgnite)) { tc.DemonicIgnite = true; ShowActiveSkillSpell(tc, Database.DEMONIC_IGNITE); }
                    //            if ((tc.Level >= 66) && (!tc.StanceOfDouble)) { tc.StanceOfDouble = true; ShowActiveSkillSpell(tc, Database.STANCE_OF_DOUBLE); }
                    //            if ((tc.Level >= 67) && (!tc.WarpGate)) { tc.WarpGate = true; ShowActiveSkillSpell(tc, Database.WARP_GATE); }
                    //            if ((tc.Level >= 68) && (!tc.StanceOfMystic)) { tc.StanceOfMystic = true; ShowActiveSkillSpell(tc, Database.STANCE_OF_MYSTIC); }
                    //            if ((tc.Level >= 69) && (!tc.SoulExecution)) { tc.SoulExecution = true; ShowActiveSkillSpell(tc, Database.SOUL_EXECUTION); }
                    //            if ((tc.Level >= 70) && (!tc.ZetaExplosion)) { tc.ZetaExplosion = true; ShowActiveSkillSpell(tc, Database.ZETA_EXPLOSION); }
                    //            #endregion
                    //        }
                    //    }
                    //    this.WE = tempWE;

                    //    if (!alreadyPlayBackMusic && (ec1.Name != "五階の守護者：Bystander" && enemyName != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS && enemyName != Database.ENEMY_LAST_SINIKIA_KAHLHANZ && enemyName != Database.ENEMY_LAST_OL_LANDIS))
                    //    {
                    //        alreadyPlayBackMusic = true;
                    //        GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                    //    }
                    //    SetupPlayerStatus();
                    //    return true;
                    //}
                }
            }

            //this.DialogResult = DialogResult.Cancel;
            return false;
        }

        private void UpdateUnknownTileArea11()
        {
            for (int ii = 13; ii <= 22; ii++)
            {
                for (int jj = 27; jj <= 34; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false); // change unity
                    GroundOne.Truth_KnownTileInfo[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
            //dungeonField.Invalidate();
            //this.Update();
        }
        private void UpdateUnknownTileArea12()
        {
            for (int ii = 1; ii <= 5; ii++)
            {
                for (int jj = 1; jj <= 12; jj++)
                {
                    unknownTile[jj * Database.TRUTH_DUNGEON_COLUMN + ii].SetActive(false); // change unity
                    GroundOne.Truth_KnownTileInfo[jj * Database.TRUTH_DUNGEON_COLUMN + ii] = true;
                }
            }
            //dungeonField.Invalidate();
            //this.Update();
        }
        private void ShowActiveSkillSpell(MainCharacter mc, string p)
        {
            // todo
            //throw new NotImplementedException();
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

        public void tapStatus()
        {
            SceneDimension.playbackScene.Add("TruthDungeon");
            Application.LoadLevel("TruthStatusPlayer");
        }
        public void tapBattleSetting()
        {
        }
        public void tapSave()
        {
            //Camera camera = Camera.main;
            //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 10);
            //debug.text += camera.transform.position.ToString() + "\r\n";
        }
        public void tapLoad()
        {
            Camera camera = Camera.main;
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 10);
        }
        public void tapExit()
        {
            Application.LoadLevel("TruthHomeTown");
        }

        private DungeonPlayer.MessagePack.ActionEvent currentEvent;

        public void tapYes()
        {
            btnYes.enabled = false; btnYes.gameObject.SetActive(false);
            btnNo.enabled = false; btnNo.gameObject.SetActive(false);
            mainMessage.text = "";
            if (currentEvent == MessagePack.ActionEvent.HomeTown ||
                currentEvent == MessagePack.ActionEvent.GotoHomeTown)
            {
                CallHomeTown();
            }
            else if (currentEvent == MessagePack.ActionEvent.YesNoGotoDungeon2)
            {
                MessagePack.Message10051_2(ref this.nowMessage, ref this.nowEvent);
                tapOK();
            }
        }
        public void tapNo()
        {
            btnYes.enabled = false; btnYes.gameObject.SetActive(false);
            btnNo.enabled = false; btnNo.gameObject.SetActive(false);
            mainMessage.text = "";
        }
        public void tapOK()
        {
            debug.text += "tapOK ";
            if (this.nowReading < this.nowMessage.Count)
            {
                this.btnOK.enabled = true;
                this.btnOK.gameObject.SetActive(true);

                mainMessage.text = "   " + this.nowMessage[this.nowReading];
                this.currentEvent = this.nowEvent[this.nowReading];
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
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenTop)
                {
                    blueWallTop[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallTop.Count; ii++)
                    {
                        if (this.objBlueWallTop[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallTop[ii].SetActive(false);
                            break;
                        }
                    }

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y + Database.DUNGEON_MOVE_LEN);
                    blueWallBottom[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallBottom.Count; ii++)
                    {
                        if (this.objBlueWallBottom[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallBottom[ii].SetActive(false);
                            break;
                        }
                    }
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenBottom)
                {
                    blueWallBottom[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallBottom.Count; ii++)
                    {
                        if (this.objBlueWallBottom[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallBottom[ii].SetActive(false);
                            break;
                        }
                    }

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN);
                    blueWallTop[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallTop.Count; ii++)
                    {
                        if (this.objBlueWallTop[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallTop[ii].SetActive(false);
                            break;
                        }
                    }
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenLeft)
                {
                    blueWallLeft[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallLeft.Count; ii++)
                    {
                        if (this.objBlueWallLeft[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallLeft[ii].SetActive(false);
                            break;
                        }
                    }

                    UpdateUnknownTileArea11();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    blueWallRight[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallRight.Count; ii++)
                    {
                        if (this.objBlueWallRight[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallRight[ii].SetActive(false);
                            break;
                        }
                    }
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BlueOpenRight)
                {
                    blueWallRight[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallRight.Count; ii++)
                    {
                        if (this.objBlueWallRight[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallRight[ii].SetActive(false);
                            break;
                        }
                    } 
                    
                    UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    blueWallLeft[GetTileNumber(this.Player.transform.position)] = false;
                    for (int ii = 0; ii < this.objBlueWallLeft.Count; ii++)
                    {
                        if (this.objBlueWallLeft[ii].name == "bluewall_" + GetTileNumber(this.Player.transform.position).ToString())
                        {
                            this.objBlueWallLeft[ii].SetActive(false);
                            break;
                        }
                    }
                    UpdateUnknownTile();
                }
                else if (currentEvent == MessagePack.ActionEvent.BigEntranceOpen)
                {
                    blueWallBottom[26 * Database.TRUTH_DUNGEON_COLUMN + 14] = false;
                    blueWallTop[27 * Database.TRUTH_DUNGEON_COLUMN + 14] = false;

                    blueWallTop[35 * Database.TRUTH_DUNGEON_COLUMN + 14] = false;
                    blueWallBottom[34 * Database.TRUTH_DUNGEON_COLUMN + 14] = false;

                    blueWallLeft[28 * Database.TRUTH_DUNGEON_COLUMN + 13] = false;
                    blueWallRight[28 * Database.TRUTH_DUNGEON_COLUMN + 12] = false;

                    blueWallLeft[33 * Database.TRUTH_DUNGEON_COLUMN + 13] = false;
                    blueWallRight[33 * Database.TRUTH_DUNGEON_COLUMN + 12] = false;

                    blueWallLeft[10 * Database.TRUTH_DUNGEON_COLUMN + 16] = false;
                    blueWallRight[10 * Database.TRUTH_DUNGEON_COLUMN + 15] = false;

                    //dungeonField.Invalidate();
                }
                else if (currentEvent == MessagePack.ActionEvent.SmallEntranceOpen1)
                {
                    blueWallTop[GetTileNumber(this.Player.transform.position)] = false;
                    UpdateUnknownTileArea12();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x, this.Player.transform.position.y - Database.DUNGEON_MOVE_LEN);
                    blueWallBottom[GetTileNumber(this.Player.transform.position)] = false;
                    //dungeonField.Invalidate();

                }                     
                else if (currentEvent == MessagePack.ActionEvent.SmallEntranceOpen2)
                {
                    blueWallLeft[GetTileNumber(this.Player.transform.position)] = false;
                    UpdateUnknownTileArea12();
                    UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y);
                    blueWallRight[GetTileNumber(this.Player.transform.position)] = false;
                    //dungeonField.Invalidate();

                }
                else if (currentEvent == MessagePack.ActionEvent.CenterBlueOpen)
                {
                    blueWallLeft[16 * Database.TRUTH_DUNGEON_COLUMN + 13] = false;
                    blueWallRight[16 * Database.TRUTH_DUNGEON_COLUMN + 12] = false;
                    //dungeonField.Invalidate();

                }
                else if (currentEvent == MessagePack.ActionEvent.EncountFlansis)
                {
                    bool result = EncountBattle(Database.ENEMY_BOSS_KARAMITUKU_FLANSIS, String.Empty, String.Empty, false, false, false, false);
                    // todo loadlevelになり、画面が戻ってきた時、本画面のロードで以下の処理を行う。
                    if (!result)
                    {
                        UpdatePlayerLocationInfo(this.Player.transform.position.x - Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false);
                        //this.//dungeonField.Invalidate();
                        UpdateMainMessage("", true);
                    }
                    else
                    {
                        GroundOne.WE.TruthCompleteSlayBoss1 = true;
                    }
                    UpdateMainMessage("", true);
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
                else if (currentEvent == MessagePack.ActionEvent.YesNoGotoDungeon2)
                {
                    btnYes.enabled = true; btnYes.gameObject.SetActive(true);
                    btnNo.enabled = true; btnNo.gameObject.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.GotoHomeTown)
                {
                    btnYes.enabled = true; btnYes.gameObject.SetActive(true);
                    btnNo.enabled = true; btnNo.gameObject.SetActive(true);
                }
                else if (currentEvent == MessagePack.ActionEvent.GotoDungeon2)
                {
                    UpdateViewPoint(-Database.DUNGEON_MOVE_LEN * 15, -Database.DUNGEON_MOVE_LEN * 10);
                    UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * (29 - 15), Database.DUNGEON_MOVE_LEN * (19 - 10));
                    //SetupDungeonMapping(2); // todo
                    //dungeonField.Invalidate();
                }
                else if (currentEvent == MessagePack.ActionEvent.DecisionOpenDoor1)
                {
                    this.NoticeMessage.text = "　【　扉を開けますか？　】";
                    this.FirstMessage.text = "扉を開ける。";
                    this.SecondMessage.text = "扉を開けず、他を探す。";
                }
                   
                this.nowReading++;
            }

            if (this.nowReading >= this.nowMessage.Count)
            {
                this.nowReading = 0;
                this.nowMessage.Clear();
                this.nowEvent.Clear();

                this.btnOK.enabled = false;
                this.btnOK.gameObject.SetActive(false);
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
        public void CallbackHomeTown()
        {
            //this.mc = ht.MC;
            //this.sc = ht.SC;
            //this.tc = ht.TC;
            //this.we = ht.WE;

            //if (ht.DialogResult == DialogResult.Retry)
            //{
            //    this.Show();
            //    this.mc = ht.MC;
            //    this.sc = ht.SC;
            //    this.tc = ht.TC;
            //    this.we = ht.WE;
            //    this.knownTileInfo = ht.Truth_KnownTileInfo;
            //    this.knownTileInfo2 = ht.Truth_KnownTileInfo2;
            //    this.knownTileInfo3 = ht.Truth_KnownTileInfo3;
            //    this.knownTileInfo4 = ht.Truth_KnownTileInfo4;
            //    this.knownTileInfo5 = ht.Truth_KnownTileInfo5;
            //    SetupPlayerStatus();
            //    PreInitialize();
            //}
            //else if (ht.DialogResult == DialogResult.Cancel)
            //{
            //    this.Show();
            //    this.DialogResult = DialogResult.Cancel;
            //}
            //else
            //{
            //    // ダンジョンに戻ってきたため、フラグを立てる
            //    this.GroundOne.WE.SaveByDungeon = true;
            //    // ホームタウンから出てきたら、その日のコミュニケーションフラグを落とす
            //    this.GroundOne.WE.AlreadyCommunicate = false;
            //    this.GroundOne.WE.AlreadyEquipShop = false;
            //    this.GroundOne.WE.alreadyCommunicateCahlhanz = false;
            //    this.GroundOne.WE.AlreadyRest = false;
            //    this.dayLabel.Text = GroundOne.WE.GameDay.ToString() + "日目";
            //    this.dungeonAreaLabel.Text = GroundOne.WE.DungeonArea.ToString() + "　階";

            //    switch (ht.TargetDungeon)
            //    {
            //        case 1:
            //            UpdateViewPoint(-Database.DUNGEON_MOVE_LEN * 25, -Database.DUNGEON_MOVE_LEN * 5);
            //            UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * (39 - 25), Database.DUNGEON_MOVE_LEN * (14 - 5));
            //            break;
            //        case 2:
            //            UpdateViewPoint(-Database.DUNGEON_MOVE_LEN * 25, -Database.DUNGEON_MOVE_LEN * 5);
            //            UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * (29 - 25), Database.DUNGEON_MOVE_LEN * (19 - 5));
            //            break;
            //        case 3:
            //            UpdateViewPoint(-Database.DUNGEON_MOVE_LEN * 0, -Database.DUNGEON_MOVE_LEN * 10);
            //            UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * (0 - 0), Database.DUNGEON_MOVE_LEN * (19 - 10));
            //            break;
            //        case 4:
            //            JumpByNormal(18, 52);
            //            //UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * 6, Database.DUNGEON_MOVE_LEN * 19);
            //            break;
            //        case 5:
            //            UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * 20, Database.DUNGEON_MOVE_LEN * 0);
            //            break;
            //    }
            //    SetupDungeonMapping(ht.TargetDungeon);

            //    UpdateMainMessage("", true);
            //    this.Show();
            //    GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
            //    SetupPlayerStatus();
            //}

            //UpdateUnknownTile();
            ////dungeonField.Invalidate();
        }
        public void CallHomeTown() { CallHomeTown(false); }
        public void CallHomeTown(bool noFirstMusic)
        {
            CancelKeyDownMovement();

            stepCounter = 0;
            GroundOne.StopDungeonMusic();

            TruthHomeTown ht = new TruthHomeTown();

            // ホームタウンに入る前は、遠見の青水晶を使ってくる場合もあるため、スタート地点へ移動しておく事とする。
            //    UpdatePlayerLocationInfo(Database.DUNGEON_MOVE_LEN * 39, Database.DUNGEON_MOVE_LEN * 14); // [todo] Unityの新しい画面では、この操作は不要になる。
            //SetupDungeonMapping(1);

            GroundOne.Truth_KnownTileInfo2 = this.knownTileInfo2;
            GroundOne.Truth_KnownTileInfo3 = this.knownTileInfo3;
            GroundOne.Truth_KnownTileInfo4 = this.knownTileInfo4;
            GroundOne.Truth_KnownTileInfo5 = this.knownTileInfo5;
            GroundOne.NoFirstMusic = noFirstMusic;
            GroundOne.BattleSpeed = this.battleSpeed;
            GroundOne.Difficulty = this.difficulty;

            Application.LoadLevel("TruthHomeTown");

            // [todo] CallbackHomeTownはUnityのMainMenuから呼び出された時、必ず実施しなければならないメソッドである。
        }
    }
}

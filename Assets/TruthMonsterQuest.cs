using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

namespace DungeonPlayer
{
    public class TruthMonsterQuest : MotherForm
    {
        public Text txtTitle;
        public Text txtMainMessage;
        public List<GameObject> btnLevelList;
        public GameObject content;
        public List<GameObject> stageList;
        public List<GameObject> objMonsterIcon;
        public List<Text> txtMonster;
        public List<Text> txtRequire;
        public List<Text> txtDescription;
        public List<Image> imgReward1;
        public List<Text> txtReward1;
        public List<Image> imgBox1;
        public List<Image> imgReward2;
        public List<Text> txtReward2;
        public List<Image> imgBox2;
        public List<Image> imgReward3;
        public List<Text> txtReward3;
        public List<Image> imgBox3;
        public List<Image> imgStar;
        public GameObject groupBattleResult;
        public List<Text> txtRewardResult;
        public List<Image> imgRewardResult;
        public GameObject groupSystemMessage;
        public Text txtResultFailMessage;
        public GameObject objItemInfo;
        public Text txtItemDesc;

        private int areaNumber = 1;
        private int stageNumber = 0;

        public override void Start()
        {
            base.Start();
            Debug.Log("MonsterQuest Start");

            if (GroundOne.WE.AvailableMonsterQuest1 && GroundOne.WE.AvailableMonsterQuest2 == false)
            {
                btnLevelList[0].GetComponent<Button>().interactable = true;

                btnLevelList[1].GetComponent<Button>().interactable = false;
                btnLevelList[1].GetComponentInChildren<Text>().color = Color.white;
                btnLevelList[2].GetComponent<Button>().interactable = false;
                btnLevelList[2].GetComponentInChildren<Text>().color = Color.white;
                btnLevelList[3].GetComponent<Button>().interactable = false;
                btnLevelList[3].GetComponentInChildren<Text>().color = Color.white;
            }
            else if (GroundOne.WE.AvailableMonsterQuest2 && GroundOne.WE.AvailableMonsterQuest3 == false)
            {
                btnLevelList[0].GetComponent<Button>().interactable = true;
                btnLevelList[0].GetComponentInChildren<Text>().color = Color.black;
                btnLevelList[1].GetComponent<Button>().interactable = true;
                btnLevelList[1].GetComponentInChildren<Text>().color = Color.black;

                btnLevelList[2].GetComponent<Button>().interactable = false;
                btnLevelList[3].GetComponent<Button>().interactable = false;
            }
            else if (GroundOne.WE.AvailableMonsterQuest3 && GroundOne.WE.AvailableMonsterQuest4 == false)
            {
                btnLevelList[0].GetComponent<Button>().interactable = true;
                btnLevelList[1].GetComponent<Button>().interactable = true;
                btnLevelList[2].GetComponent<Button>().interactable = true;

                btnLevelList[3].GetComponent<Button>().interactable = false;
            }
            else if (GroundOne.WE.AvailableMonsterQuest4)
            {
                btnLevelList[0].GetComponent<Button>().interactable = true;
                btnLevelList[1].GetComponent<Button>().interactable = true;
                btnLevelList[2].GetComponent<Button>().interactable = true;
                btnLevelList[3].GetComponent<Button>().interactable = true;
            }

            btnLevelList[4].SetActive(false);

            GroundOne.battleResult result = GroundOne.BattleResult;
            GroundOne.BattleResult = GroundOne.battleResult.None;

            TapArea(0);
            TapStage(0);

            Debug.Log("MonsterQuest Start(E)");
        }
        private void UpdateLevelView(bool b1, bool b2, bool b3)
        {
            btnLevelList[0].SetActive(true);
            btnLevelList[1].GetComponent<Button>().interactable = false;
            btnLevelList[1].GetComponentInChildren<Text>().color = Color.white;
            btnLevelList[2].GetComponent<Button>().interactable = false;
            btnLevelList[2].GetComponentInChildren<Text>().color = Color.white;
            btnLevelList[3].GetComponent<Button>().interactable = false;
            btnLevelList[3].GetComponentInChildren<Text>().color = Color.white;

        }

        public override void Update()
        {
            base.Update();
        }

        public override void SceneBack()
        {
            base.SceneBack();
        }

        public void TapArea(int num)
        {
            Debug.Log("TapArea: " + num.ToString());
            stageNumber = 0; // エリア変更時は、ステージ番号は先頭に戻す。
            areaNumber = num;
            Color current = new Color();
            Color txtColor = new Color();
            string[] MQ_NAME = Database.MQ_FLOOR1_NAME;
            int[] MQ_REQUIRE = Database.MQ_FLOOR1_REQUIRE;
            string[] MQ_DESC = Database.MQ_FLOOR1_DESC;
            string[] MQ_ICON = Database.MQ_FLOOR1_ICON;
            string[,] MQ_REWARD = Database.MQ_FLOOR1_REWARD;

            if (num == 0)
            {
                current = new Color(125.0f / 255.0f, 249.0f / 255.0f, 151.0f / 255.0f);
                txtColor = new Color(0, 0, 0);
                MQ_NAME = Database.MQ_FLOOR1_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR1_REQUIRE;
                MQ_DESC = Database.MQ_FLOOR1_DESC;
                MQ_ICON = Database.MQ_FLOOR1_ICON;
                MQ_REWARD = Database.MQ_FLOOR1_REWARD;
            }
            else if (num == 1)
            {
                current = new Color(125.0f / 255.0f, 203.0f / 255.0f, 249.0f / 255.0f);
                txtColor = new Color(0, 0, 0);
                MQ_NAME = Database.MQ_FLOOR2_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR2_REQUIRE;
                MQ_DESC = Database.MQ_FLOOR2_DESC;
                MQ_ICON = Database.MQ_FLOOR2_ICON;
                MQ_REWARD = Database.MQ_FLOOR2_REWARD;
            }
            else if (num == 2)
            {
                current = new Color(38.0f / 255.0f, 34.0f / 255.0f, 144.0f / 255.0f);
                txtColor = new Color(1, 1, 1);
                MQ_NAME = Database.MQ_FLOOR3_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR3_REQUIRE;
                MQ_DESC = Database.MQ_FLOOR3_DESC;
                MQ_ICON = Database.MQ_FLOOR3_ICON;
                MQ_REWARD = Database.MQ_FLOOR3_REWARD;
            }
            else if (num == 3)
            {
                current = new Color(79.0f / 255.0f, 79.0f / 255.0f, 79.0f / 255.0f);
                txtColor = new Color(1, 1, 1);
                MQ_NAME = Database.MQ_FLOOR4_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR4_REQUIRE;
                MQ_DESC = Database.MQ_FLOOR4_DESC;
                MQ_ICON = Database.MQ_FLOOR4_ICON;
                MQ_REWARD = Database.MQ_FLOOR4_REWARD;
            }
            else if (num == 4)
            {
                current = new Color(114.0f / 255.0f, 32.0f / 255.0f, 32.0f / 255.0f);
                txtColor = new Color(1, 1, 1);
            }

            RectTransform rect = content.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0.0f, rect.sizeDelta.y);
            const int WIDTH = 700;
            const int MARGIN = 10;
            for (int ii = 0; ii < stageList.Count; ii++)
            {
                stageList[ii].GetComponent<Image>().color = current;
                txtMonster[ii].color = txtColor;
                txtRequire[ii].color = txtColor;

                if (ii < MQ_NAME.Length)
                {
                    txtMonster[ii].text = MQ_NAME[ii];
                }
                else
                {
                    txtMonster[ii].text = "";
                }

                if (ii < MQ_REQUIRE.Length && MQ_REQUIRE[ii] > 0)
                {
                    txtRequire[ii].text = "Require LV: " + MQ_REQUIRE[ii].ToString();
                    if (MQ_REQUIRE[ii] > GroundOne.MC.Level)
                    {
                        txtRequire[ii].color = Color.red;
                        txtRequire[ii].fontStyle = FontStyle.BoldAndItalic;
                        //stageList[ii].GetComponent<Button>().interactable = false;
                    }
                }
                else
                {
                    txtRequire[ii].text = "";
                }

                if (ii < MQ_DESC.Length)
                {
                    txtDescription[ii].text = MQ_DESC[ii];
                }
                else
                {
                    txtDescription[ii].text = "";
                }

                if (ii < MQ_ICON.Length && (MQ_ICON[ii] != String.Empty))
                {
                    objMonsterIcon[ii].GetComponent<Image>().sprite = Resources.Load<Sprite>(MQ_ICON[ii]);
                }
                else
                {
                    objMonsterIcon[ii].GetComponent<Image>().sprite = null;
                }

                if (ii < MQ_REWARD.Length)
                {
                    txtReward1[ii].text = MQ_REWARD[ii, 0];
                    if (txtReward1[ii].text == String.Empty) { imgReward1[ii].sprite = null; }
                    else { Method.UpdateItemImage(new ItemBackPack(txtReward1[ii].text), imgReward1[ii]); }

                    txtReward2[ii].text = MQ_REWARD[ii, 1];
                    if (txtReward2[ii].text == String.Empty) { imgReward2[ii].sprite = null; }
                    else { Method.UpdateItemImage(new ItemBackPack(txtReward2[ii].text), imgReward2[ii]); }
                    
                    txtReward3[ii].text = MQ_REWARD[ii, 2];
                    if (txtReward3[ii].text == String.Empty) { imgReward3[ii].sprite = null; }
                    else { Method.UpdateItemImage(new ItemBackPack(txtReward3[ii].text), imgReward3[ii]); }
                }

                // 未設定のものはグレーアウトする。
                if (txtMonster[ii].text == String.Empty)
                {
                    stageList[ii].GetComponent<Image>().color = Color.gray;
                    txtMonster[ii].text = "";
                    txtRequire[ii].text = "";
                    txtDescription[ii].text = "";
                    objMonsterIcon[ii].GetComponent<Image>().sprite = null;
                    imgStar[ii].GetComponent<Image>().sprite = null;
                    stageList[ii].SetActive(false);
                }
                else
                {
                    stageList[ii].SetActive(true);
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x + WIDTH + MARGIN, rect.sizeDelta.y);
                }
            }

            if (num == 0)
            {
                imgStar[0].gameObject.SetActive(GroundOne.WE.MQ_Complete1_1);
                imgStar[1].gameObject.SetActive(GroundOne.WE.MQ_Complete1_2);
                imgStar[2].gameObject.SetActive(GroundOne.WE.MQ_Complete1_3);
                imgStar[3].gameObject.SetActive(GroundOne.WE.MQ_Complete1_4);
                imgStar[4].gameObject.SetActive(GroundOne.WE.MQ_Complete1_5);
                imgStar[5].gameObject.SetActive(GroundOne.WE.MQ_Complete1_6);
                imgStar[6].gameObject.SetActive(GroundOne.WE.MQ_Complete1_7);
                imgStar[7].gameObject.SetActive(GroundOne.WE.MQ_Complete1_8);
                imgStar[8].gameObject.SetActive(GroundOne.WE.MQ_Complete1_9);
                imgStar[9].gameObject.SetActive(GroundOne.WE.MQ_Complete1_10);
                imgStar[10].gameObject.SetActive(GroundOne.WE.MQ_Complete1_11);
                imgStar[11].gameObject.SetActive(GroundOne.WE.MQ_Complete1_12);
                imgStar[12].gameObject.SetActive(GroundOne.WE.MQ_Complete1_13);

                int counter = 0;
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_1, GroundOne.WE.MQ_Reward1_1_1, GroundOne.WE.MQ_Reward1_1_2, GroundOne.WE.MQ_Reward1_1_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_2, GroundOne.WE.MQ_Reward1_2_1, GroundOne.WE.MQ_Reward1_2_2, GroundOne.WE.MQ_Reward1_2_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_3, GroundOne.WE.MQ_Reward1_3_1, GroundOne.WE.MQ_Reward1_3_2, GroundOne.WE.MQ_Reward1_3_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_4, GroundOne.WE.MQ_Reward1_4_1, GroundOne.WE.MQ_Reward1_4_2, GroundOne.WE.MQ_Reward1_4_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_5, GroundOne.WE.MQ_Reward1_5_1, GroundOne.WE.MQ_Reward1_5_2, GroundOne.WE.MQ_Reward1_5_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_6, GroundOne.WE.MQ_Reward1_6_1, GroundOne.WE.MQ_Reward1_6_2, GroundOne.WE.MQ_Reward1_6_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_7, GroundOne.WE.MQ_Reward1_7_1, GroundOne.WE.MQ_Reward1_7_2, GroundOne.WE.MQ_Reward1_7_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_8, GroundOne.WE.MQ_Reward1_8_1, GroundOne.WE.MQ_Reward1_8_2, GroundOne.WE.MQ_Reward1_8_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_9, GroundOne.WE.MQ_Reward1_9_1, GroundOne.WE.MQ_Reward1_9_2, GroundOne.WE.MQ_Reward1_9_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_10, GroundOne.WE.MQ_Reward1_10_1, GroundOne.WE.MQ_Reward1_10_2, GroundOne.WE.MQ_Reward1_10_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_11, GroundOne.WE.MQ_Reward1_11_1, GroundOne.WE.MQ_Reward1_11_2, GroundOne.WE.MQ_Reward1_11_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_12, GroundOne.WE.MQ_Reward1_12_1, GroundOne.WE.MQ_Reward1_12_2, GroundOne.WE.MQ_Reward1_12_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete1_13, GroundOne.WE.MQ_Reward1_13_1, GroundOne.WE.MQ_Reward1_13_2, GroundOne.WE.MQ_Reward1_13_3);
            }
            else if (num == 1)
            {
                imgStar[0].gameObject.SetActive(GroundOne.WE.MQ_Complete2_1);
                imgStar[1].gameObject.SetActive(GroundOne.WE.MQ_Complete2_2);
                imgStar[2].gameObject.SetActive(GroundOne.WE.MQ_Complete2_3);
                imgStar[3].gameObject.SetActive(GroundOne.WE.MQ_Complete2_4);
                imgStar[4].gameObject.SetActive(GroundOne.WE.MQ_Complete2_5);
                imgStar[5].gameObject.SetActive(GroundOne.WE.MQ_Complete2_6);
                imgStar[6].gameObject.SetActive(GroundOne.WE.MQ_Complete2_7);
                imgStar[7].gameObject.SetActive(GroundOne.WE.MQ_Complete2_8);
                imgStar[8].gameObject.SetActive(GroundOne.WE.MQ_Complete2_9);
                imgStar[9].gameObject.SetActive(GroundOne.WE.MQ_Complete2_10);
                imgStar[10].gameObject.SetActive(GroundOne.WE.MQ_Complete2_11);
                imgStar[11].gameObject.SetActive(GroundOne.WE.MQ_Complete2_12);

                int counter = 0;
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_1, GroundOne.WE.MQ_Reward2_1_1, GroundOne.WE.MQ_Reward2_1_2, GroundOne.WE.MQ_Reward2_1_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_2, GroundOne.WE.MQ_Reward2_2_1, GroundOne.WE.MQ_Reward2_2_2, GroundOne.WE.MQ_Reward2_2_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_3, GroundOne.WE.MQ_Reward2_3_1, GroundOne.WE.MQ_Reward2_3_2, GroundOne.WE.MQ_Reward2_3_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_4, GroundOne.WE.MQ_Reward2_4_1, GroundOne.WE.MQ_Reward2_4_2, GroundOne.WE.MQ_Reward2_4_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_5, GroundOne.WE.MQ_Reward2_5_1, GroundOne.WE.MQ_Reward2_5_2, GroundOne.WE.MQ_Reward2_5_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_6, GroundOne.WE.MQ_Reward2_6_1, GroundOne.WE.MQ_Reward2_6_2, GroundOne.WE.MQ_Reward2_6_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_7, GroundOne.WE.MQ_Reward2_7_1, GroundOne.WE.MQ_Reward2_7_2, GroundOne.WE.MQ_Reward2_7_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_8, GroundOne.WE.MQ_Reward2_8_1, GroundOne.WE.MQ_Reward2_8_2, GroundOne.WE.MQ_Reward2_8_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_9, GroundOne.WE.MQ_Reward2_9_1, GroundOne.WE.MQ_Reward2_9_2, GroundOne.WE.MQ_Reward2_9_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_10, GroundOne.WE.MQ_Reward2_10_1, GroundOne.WE.MQ_Reward2_10_2, GroundOne.WE.MQ_Reward2_10_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_11, GroundOne.WE.MQ_Reward2_11_1, GroundOne.WE.MQ_Reward2_11_2, GroundOne.WE.MQ_Reward2_11_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete2_12, GroundOne.WE.MQ_Reward2_12_1, GroundOne.WE.MQ_Reward2_12_2, GroundOne.WE.MQ_Reward2_12_3);
            }
            else if (num == 2)
            {
                imgStar[0].gameObject.SetActive(GroundOne.WE.MQ_Complete3_1);
                imgStar[1].gameObject.SetActive(GroundOne.WE.MQ_Complete3_2);
                imgStar[2].gameObject.SetActive(GroundOne.WE.MQ_Complete3_3);
                imgStar[3].gameObject.SetActive(GroundOne.WE.MQ_Complete3_4);
                imgStar[4].gameObject.SetActive(GroundOne.WE.MQ_Complete3_5);
                imgStar[5].gameObject.SetActive(GroundOne.WE.MQ_Complete3_6);
                imgStar[6].gameObject.SetActive(GroundOne.WE.MQ_Complete3_7);
                imgStar[7].gameObject.SetActive(GroundOne.WE.MQ_Complete3_8);
                imgStar[8].gameObject.SetActive(GroundOne.WE.MQ_Complete3_9);
                imgStar[9].gameObject.SetActive(GroundOne.WE.MQ_Complete3_10);
                imgStar[10].gameObject.SetActive(GroundOne.WE.MQ_Complete3_11);
                imgStar[11].gameObject.SetActive(GroundOne.WE.MQ_Complete3_12);

                int counter = 0;
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_1, GroundOne.WE.MQ_Reward3_1_1, GroundOne.WE.MQ_Reward3_1_2, GroundOne.WE.MQ_Reward3_1_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_2, GroundOne.WE.MQ_Reward3_2_1, GroundOne.WE.MQ_Reward3_2_2, GroundOne.WE.MQ_Reward3_2_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_3, GroundOne.WE.MQ_Reward3_3_1, GroundOne.WE.MQ_Reward3_3_2, GroundOne.WE.MQ_Reward3_3_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_4, GroundOne.WE.MQ_Reward3_4_1, GroundOne.WE.MQ_Reward3_4_2, GroundOne.WE.MQ_Reward3_4_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_5, GroundOne.WE.MQ_Reward3_5_1, GroundOne.WE.MQ_Reward3_5_2, GroundOne.WE.MQ_Reward3_5_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_6, GroundOne.WE.MQ_Reward3_6_1, GroundOne.WE.MQ_Reward3_6_2, GroundOne.WE.MQ_Reward3_6_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_7, GroundOne.WE.MQ_Reward3_7_1, GroundOne.WE.MQ_Reward3_7_2, GroundOne.WE.MQ_Reward3_7_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_8, GroundOne.WE.MQ_Reward3_8_1, GroundOne.WE.MQ_Reward3_8_2, GroundOne.WE.MQ_Reward3_8_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_9, GroundOne.WE.MQ_Reward3_9_1, GroundOne.WE.MQ_Reward3_9_2, GroundOne.WE.MQ_Reward3_9_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_10, GroundOne.WE.MQ_Reward3_10_1, GroundOne.WE.MQ_Reward3_10_2, GroundOne.WE.MQ_Reward3_10_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_11, GroundOne.WE.MQ_Reward3_11_1, GroundOne.WE.MQ_Reward3_11_2, GroundOne.WE.MQ_Reward3_11_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete3_12, GroundOne.WE.MQ_Reward3_12_1, GroundOne.WE.MQ_Reward3_12_2, GroundOne.WE.MQ_Reward3_12_3);
            }
            else if (num == 3)
            {
                imgStar[0].gameObject.SetActive(GroundOne.WE.MQ_Complete4_1);
                imgStar[1].gameObject.SetActive(GroundOne.WE.MQ_Complete4_2);
                imgStar[2].gameObject.SetActive(GroundOne.WE.MQ_Complete4_3);
                imgStar[3].gameObject.SetActive(GroundOne.WE.MQ_Complete4_4);
                imgStar[4].gameObject.SetActive(GroundOne.WE.MQ_Complete4_5);
                imgStar[5].gameObject.SetActive(GroundOne.WE.MQ_Complete4_6);
                imgStar[6].gameObject.SetActive(GroundOne.WE.MQ_Complete4_7);
                imgStar[7].gameObject.SetActive(GroundOne.WE.MQ_Complete4_8);
                imgStar[8].gameObject.SetActive(GroundOne.WE.MQ_Complete4_9);
                imgStar[9].gameObject.SetActive(GroundOne.WE.MQ_Complete4_10);
                imgStar[10].gameObject.SetActive(GroundOne.WE.MQ_Complete4_11);
                imgStar[11].gameObject.SetActive(GroundOne.WE.MQ_Complete4_12);

                int counter = 0;
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_1, GroundOne.WE.MQ_Reward4_1_1, GroundOne.WE.MQ_Reward4_1_2, GroundOne.WE.MQ_Reward4_1_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_2, GroundOne.WE.MQ_Reward4_2_1, GroundOne.WE.MQ_Reward4_2_2, GroundOne.WE.MQ_Reward4_2_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_3, GroundOne.WE.MQ_Reward4_3_1, GroundOne.WE.MQ_Reward4_3_2, GroundOne.WE.MQ_Reward4_3_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_4, GroundOne.WE.MQ_Reward4_4_1, GroundOne.WE.MQ_Reward4_4_2, GroundOne.WE.MQ_Reward4_4_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_5, GroundOne.WE.MQ_Reward4_5_1, GroundOne.WE.MQ_Reward4_5_2, GroundOne.WE.MQ_Reward4_5_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_6, GroundOne.WE.MQ_Reward4_6_1, GroundOne.WE.MQ_Reward4_6_2, GroundOne.WE.MQ_Reward4_6_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_7, GroundOne.WE.MQ_Reward4_7_1, GroundOne.WE.MQ_Reward4_7_2, GroundOne.WE.MQ_Reward4_7_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_8, GroundOne.WE.MQ_Reward4_8_1, GroundOne.WE.MQ_Reward4_8_2, GroundOne.WE.MQ_Reward4_8_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_9, GroundOne.WE.MQ_Reward4_9_1, GroundOne.WE.MQ_Reward4_9_2, GroundOne.WE.MQ_Reward4_9_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_10, GroundOne.WE.MQ_Reward4_10_1, GroundOne.WE.MQ_Reward4_10_2, GroundOne.WE.MQ_Reward4_10_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_11, GroundOne.WE.MQ_Reward4_11_1, GroundOne.WE.MQ_Reward4_11_2, GroundOne.WE.MQ_Reward4_11_3);
                GroupControlBoxView(ref counter, GroundOne.WE.MQ_Complete4_12, GroundOne.WE.MQ_Reward4_12_1, GroundOne.WE.MQ_Reward4_12_2, GroundOne.WE.MQ_Reward4_12_3);
            }
        }

        private void GroupControlBoxView(ref int counter, bool check1, bool check2_1, bool check2_2, bool check2_3)
        {
            ControlBoxView(txtReward1[counter], imgBox1[counter], check1, check2_1);
            ControlBoxView(txtReward2[counter], imgBox2[counter], check1, check2_2);
            ControlBoxView(txtReward3[counter], imgBox3[counter], check1, check2_3);
            counter++;
        }

        private void ControlBoxView(Text txtReward, Image imgBox, bool check1, bool check2)
        {
            if (check1)
            {
                if (txtReward != null && txtReward.text != String.Empty)
                {
                    imgBox.gameObject.SetActive(true);
                    UpdateBoxOpen(imgBox, check2);
                }
                else
                {
                    imgBox.gameObject.SetActive(false);
                }
            }
            else
            {
                imgBox.gameObject.SetActive(false);
            }
        }
        private void UpdateBoxOpen(Image box, bool check)
        {
            if (check)
            {
                box.sprite = Resources.Load<Sprite>(Database.TREASURE_BOX_OPEN);
            }
            else
            {
                box.sprite = Resources.Load<Sprite>(Database.TREASURE_BOX);
            }
        }

        public void TapStage(int num)
        {
            Debug.Log("TapStage: " + num.ToString());

            string[] MQ_NAME = Database.MQ_FLOOR1_NAME;
            int[] MQ_REQUIRE = Database.MQ_FLOOR1_REQUIRE;

            if (areaNumber == 0)
            {
                MQ_NAME = Database.MQ_FLOOR1_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR1_REQUIRE;
            }
            else if (areaNumber == 1)
            {
                MQ_NAME = Database.MQ_FLOOR2_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR2_REQUIRE;
            }
            else if (areaNumber == 2)
            {
                MQ_NAME = Database.MQ_FLOOR3_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR3_REQUIRE;
            }
            else if (areaNumber == 3)
            {
                MQ_NAME = Database.MQ_FLOOR4_NAME;
                MQ_REQUIRE = Database.MQ_FLOOR4_REQUIRE;
            }

            Debug.Log("areaNumber: " + areaNumber.ToString());
            Debug.Log("GroundOne.MC.Level: " + GroundOne.MC.Level.ToString());
            Debug.Log("MQ_REQUIRE[num]: " + MQ_REQUIRE[num].ToString());
            if (GroundOne.MC.Level >= MQ_REQUIRE[num])
            {
                txtMainMessage.text = "サンディ：" + MQ_NAME[num] + "の依頼を引き受けてもらえるだろうか！アイン・ウォーレンスよ！";
                stageNumber = num;
            }
            else
            {
                txtMainMessage.text = "サンディ：" + MQ_NAME[num] + "の必要レベルは" + MQ_REQUIRE[num].ToString() + "である！申し訳ないが、他を選んでいただきたい！";
                //stageNumber = num;
            }
        }

        public void TapChallenge()
        {
            // すでにクリアしているモンスター討伐は対象外。
            string message = @"サンディ：そのモンスター討伐はすでにクリアしておられる！他を選択されるがよい！";
            // 1F
            if (GroundOne.WE.MQ_Complete1_1 && areaNumber == 0 && stageNumber == 0) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_2 && areaNumber == 0 && stageNumber == 1) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_3 && areaNumber == 0 && stageNumber == 2) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_4 && areaNumber == 0 && stageNumber == 3) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_5 && areaNumber == 0 && stageNumber == 4) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_6 && areaNumber == 0 && stageNumber == 5) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_7 && areaNumber == 0 && stageNumber == 6) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_8 && areaNumber == 0 && stageNumber == 7) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_9 && areaNumber == 0 && stageNumber == 8) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_10 && areaNumber == 0 && stageNumber == 9) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_11 && areaNumber == 0 && stageNumber == 10) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_12 && areaNumber == 0 && stageNumber == 11) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete1_13 && areaNumber == 0 && stageNumber == 12) { txtMainMessage.text = message; return; }
            // 2F
            if (GroundOne.WE.MQ_Complete2_1 && areaNumber == 1 && stageNumber == 0) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_2 && areaNumber == 1 && stageNumber == 1) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_3 && areaNumber == 1 && stageNumber == 2) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_4 && areaNumber == 1 && stageNumber == 3) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_5 && areaNumber == 1 && stageNumber == 4) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_6 && areaNumber == 1 && stageNumber == 5) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_7 && areaNumber == 1 && stageNumber == 6) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_8 && areaNumber == 1 && stageNumber == 7) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_9 && areaNumber == 1 && stageNumber == 8) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_10 && areaNumber == 1 && stageNumber == 9) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_11 && areaNumber == 1 && stageNumber == 10) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete2_12 && areaNumber == 1 && stageNumber == 11) { txtMainMessage.text = message; return; }
            // 3F
            if (GroundOne.WE.MQ_Complete3_1 && areaNumber == 2 && stageNumber == 0) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_2 && areaNumber == 2 && stageNumber == 1) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_3 && areaNumber == 2 && stageNumber == 2) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_4 && areaNumber == 2 && stageNumber == 3) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_5 && areaNumber == 2 && stageNumber == 4) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_6 && areaNumber == 2 && stageNumber == 5) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_7 && areaNumber == 2 && stageNumber == 6) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_8 && areaNumber == 2 && stageNumber == 7) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_9 && areaNumber == 2 && stageNumber == 8) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_10 && areaNumber == 2 && stageNumber == 9) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_11 && areaNumber == 2 && stageNumber == 10) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete3_12 && areaNumber == 2 && stageNumber == 11) { txtMainMessage.text = message; return; }
            // 4F
            if (GroundOne.WE.MQ_Complete4_1 && areaNumber == 3 && stageNumber == 0) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_2 && areaNumber == 3 && stageNumber == 1) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_3 && areaNumber == 3 && stageNumber == 2) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_4 && areaNumber == 3 && stageNumber == 3) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_5 && areaNumber == 3 && stageNumber == 4) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_6 && areaNumber == 3 && stageNumber == 5) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_7 && areaNumber == 3 && stageNumber == 6) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_8 && areaNumber == 3 && stageNumber == 7) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_9 && areaNumber == 3 && stageNumber == 8) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_10 && areaNumber == 3 && stageNumber == 9) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_11 && areaNumber == 3 && stageNumber == 10) { txtMainMessage.text = message; return; }
            if (GroundOne.WE.MQ_Complete4_12 && areaNumber == 3 && stageNumber == 11) { txtMainMessage.text = message; return; }

            if (GroundOne.WE.AlreadyMonsterQuestComplete)
            {
                txtMainMessage.text = @"サンディ：モンスター討伐は本日すでに実施済みである！明日に備えて体調を整えるがよい！";
                return;
            }

            // 1F
            if (areaNumber == 0)
            {
                // 1-1
                if (stageNumber == 0)
                {
                    GroundOne.enemyName1 = Database.ENEMY_HIYOWA_BEATLE;
                    GroundOne.enemyName2 = Database.ENEMY_HENSYOKU_PLANT;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 1)
                {
                    GroundOne.enemyName1 = Database.ENEMY_TINY_MANTIS;
                    GroundOne.enemyName2 = Database.ENEMY_GREEN_CHILD;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 2)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MANDRAGORA;
                    GroundOne.enemyName2 = Database.ENEMY_KOUKAKU_WURM;
                    GroundOne.enemyName3 = String.Empty;
                }
                // 1-2
                else if (stageNumber == 3)
                {
                    GroundOne.enemyName1 = Database.ENEMY_RED_HOPPER;
                    GroundOne.enemyName2 = Database.ENEMY_RED_HOPPER;
                    GroundOne.enemyName3 = Database.ENEMY_SUN_FLOWER;
                }
                else if (stageNumber == 4)
                {
                    GroundOne.enemyName1 = Database.ENEMY_WILD_ANT;
                    GroundOne.enemyName2 = Database.ENEMY_WILD_ANT;
                    GroundOne.enemyName3 = Database.ENEMY_EARTH_SPIDER;
                }
                else if (stageNumber == 5)
                {
                    GroundOne.enemyName1 = Database.ENEMY_POISON_MARY;
                    GroundOne.enemyName2 = Database.ENEMY_WILD_ANT;
                    GroundOne.enemyName3 = Database.ENEMY_EARTH_SPIDER;
                }
                // 1-3
                else if (stageNumber == 6)
                {
                    GroundOne.enemyName1 = Database.ENEMY_SPEEDY_TAKA;
                    GroundOne.enemyName2 = Database.ENEMY_SPEEDY_TAKA;
                    GroundOne.enemyName3 = Database.ENEMY_ZASSYOKU_RABBIT;
                }
                else if (stageNumber == 7)
                {
                    GroundOne.enemyName1 = Database.ENEMY_WONDER_SEED;
                    GroundOne.enemyName2 = Database.ENEMY_ASH_CREEPER;
                    GroundOne.enemyName3 = Database.ENEMY_SPEEDY_TAKA;
                }
                else if (stageNumber == 8)
                {
                    GroundOne.enemyName1 = Database.ENEMY_FLANSIS_KNIGHT;
                    GroundOne.enemyName2 = Database.ENEMY_GIANT_SNAKE;
                    GroundOne.enemyName3 = Database.ENEMY_FLANSIS_KNIGHT;
                }
                else if (stageNumber == 9)
                {
                    GroundOne.enemyName1 = Database.ENEMY_SHOTGUN_HYUI;
                    GroundOne.enemyName2 = Database.ENEMY_GIANT_SNAKE;
                    GroundOne.enemyName3 = Database.ENEMY_WONDER_SEED;
                }
                // 1-4
                else if (stageNumber == 10)
                {
                    GroundOne.enemyName1 = Database.ENEMY_WAR_WOLF;
                    GroundOne.enemyName2 = Database.ENEMY_WAR_WOLF;
                    GroundOne.enemyName3 = Database.ENEMY_BRILLIANT_BUTTERFLY;
                }
                else if (stageNumber == 11)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MIST_ELEMENTAL;
                    GroundOne.enemyName2 = Database.ENEMY_WHISPER_DRYAD;
                    GroundOne.enemyName3 = Database.ENEMY_WAR_WOLF;
                }
                else if (stageNumber == 12)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MOSSGREEN_DADDY;
                    GroundOne.enemyName2 = Database.ENEMY_BLOOD_MOSS;
                    GroundOne.enemyName3 = Database.ENEMY_BRILLIANT_BUTTERFLY;
                }
            }
            // 2F
            else if (areaNumber == 1)
            {
                // 2-1
                if (stageNumber == 0)
                {
                    GroundOne.enemyName1 = Database.ENEMY_DAGGER_FISH;
                    GroundOne.enemyName2 = Database.ENEMY_DAGGER_FISH;
                    GroundOne.enemyName3 = Database.ENEMY_SIPPU_FLYING_FISH;
                }
                else if (stageNumber == 1)
                {
                    GroundOne.enemyName1 = Database.ENEMY_ORB_SHELLFISH;
                    GroundOne.enemyName2 = Database.ENEMY_SPLASH_KURIONE;
                    GroundOne.enemyName3 = Database.ENEMY_ORB_SHELLFISH;
                }
                else if (stageNumber == 2)
                {
                    GroundOne.enemyName1 = Database.ENEMY_TRANSPARENT_UMIUSHI;
                    GroundOne.enemyName2 = Database.ENEMY_SPLASH_KURIONE;
                    GroundOne.enemyName3 = Database.ENEMY_SIPPU_FLYING_FISH;
                }
                // 2-2
                else if (stageNumber == 3)
                {
                    GroundOne.enemyName1 = Database.ENEMY_RANBOU_SEA_ARTINE;
                    GroundOne.enemyName2 = Database.ENEMY_ROLLING_MAGURO;
                    GroundOne.enemyName3 = Database.ENEMY_ROLLING_MAGURO;
                }
                else if (stageNumber == 4)
                {
                    GroundOne.enemyName1 = Database.ENEMY_GANGAME;
                    GroundOne.enemyName2 = Database.ENEMY_BLUE_SEA_WASI;
                    GroundOne.enemyName3 = Database.ENEMY_BRIGHT_SQUID;
                }
                else if (stageNumber == 5)
                {
                    GroundOne.enemyName1 = Database.ENEMY_BIGMOUSE_JOE;
                    GroundOne.enemyName2 = Database.ENEMY_BRIGHT_SQUID;
                    GroundOne.enemyName3 = Database.ENEMY_RANBOU_SEA_ARTINE;
                }
                // 2-3
                else if (stageNumber == 6)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MOGURU_MANTA;
                    GroundOne.enemyName2 = Database.ENEMY_FLOATING_GOLD_FISH;
                    GroundOne.enemyName3 = Database.ENEMY_FLOATING_GOLD_FISH;
                }
                else if (stageNumber == 7)
                {
                    GroundOne.enemyName1 = Database.ENEMY_ABARE_SHARK;
                    GroundOne.enemyName2 = Database.ENEMY_GOEI_HERMIT_CLUB;
                    GroundOne.enemyName3 = Database.ENEMY_VANISHING_CORAL;
                }
                else if (stageNumber == 8)
                {
                    GroundOne.enemyName1 = Database.ENEMY_CASSY_CANCER;
                    GroundOne.enemyName2 = Database.ENEMY_FLOATING_GOLD_FISH;
                    GroundOne.enemyName3 = Database.ENEMY_GOEI_HERMIT_CLUB;
                }
                // 2-4
                else if (stageNumber == 9)
                {
                    GroundOne.enemyName1 = Database.ENEMY_RAINBOW_ANEMONE;
                    GroundOne.enemyName2 = Database.ENEMY_BLACK_STARFISH;
                    GroundOne.enemyName3 = Database.ENEMY_RAINBOW_ANEMONE;
                }
                else if (stageNumber == 10)
                {
                    GroundOne.enemyName1 = Database.ENEMY_EDGED_HIGH_SHARK;
                    GroundOne.enemyName2 = Database.ENEMY_MACHIBUSE_ANKOU;
                    GroundOne.enemyName3 = Database.ENEMY_RAINBOW_ANEMONE;
                }
                else if (stageNumber == 11)
                {
                    GroundOne.enemyName1 = Database.ENEMY_EIGHT_EIGHT;
                    GroundOne.enemyName2 = Database.ENEMY_EDGED_HIGH_SHARK;
                    GroundOne.enemyName3 = Database.ENEMY_EDGED_HIGH_SHARK;
                }
            }
            // 3F
            else if (areaNumber == 2)
            {
                // 3-1
                if (stageNumber == 0)
                {
                    GroundOne.enemyName1 = Database.ENEMY_TOSSIN_ORC;
                    GroundOne.enemyName2 = Database.ENEMY_SNOW_CAT;
                    GroundOne.enemyName3 = Database.ENEMY_SNOW_CAT;
                }
                else if (stageNumber == 1)
                {
                    GroundOne.enemyName1 = Database.ENEMY_WAR_MAMMOTH;
                    GroundOne.enemyName2 = Database.ENEMY_WINGED_COLD_FAIRY;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 2)
                {
                    GroundOne.enemyName1 = Database.ENEMY_FREEZING_GRIFFIN;
                    GroundOne.enemyName2 = Database.ENEMY_WINGED_COLD_FAIRY;
                    GroundOne.enemyName3 = Database.ENEMY_SNOW_CAT;
                }
                // 3-2
                else if (stageNumber == 3)
                {
                    GroundOne.enemyName1 = Database.ENEMY_BRUTAL_OGRE;
                    GroundOne.enemyName2 = Database.ENEMY_HYDRO_LIZARD;
                    GroundOne.enemyName3 = Database.ENEMY_HYDRO_LIZARD;
                }
                else if (stageNumber == 4)
                {
                    GroundOne.enemyName1 = Database.ENEMY_SWORD_TOOTH_TIGER;
                    GroundOne.enemyName2 = Database.ENEMY_ICEBERG_SPIRIT;
                    GroundOne.enemyName3 = Database.ENEMY_HYDRO_LIZARD;
                }
                else if (stageNumber == 5)
                {
                    GroundOne.enemyName1 = Database.ENEMY_FEROCIOUS_RAGE_BEAR;
                    GroundOne.enemyName2 = Database.ENEMY_PENGUIN_STAR;
                    GroundOne.enemyName3 = Database.ENEMY_PENGUIN_STAR;
                }
                // 3-3
                else if (stageNumber == 6)
                {
                    GroundOne.enemyName1 = Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI;
                    GroundOne.enemyName2 = Database.ENEMY_WINTER_ORB;
                    GroundOne.enemyName3 = Database.ENEMY_MAJESTIC_CENTAURUS;
                }
                else if (stageNumber == 7)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MAGIC_HYOU_RIFLE;
                    GroundOne.enemyName2 = Database.ENEMY_INTELLIGENCE_ARGONIAN;
                    GroundOne.enemyName3 = Database.ENEMY_INTELLIGENCE_ARGONIAN;
                }
                else if (stageNumber == 8)
                {
                    GroundOne.enemyName1 = Database.ENEMY_PURE_BLIZZARD_CRYSTAL;
                    GroundOne.enemyName2 = Database.ENEMY_INTELLIGENCE_ARGONIAN;
                    GroundOne.enemyName3 = Database.ENEMY_MAJESTIC_CENTAURUS;
                }
                // 3-4
                else if (stageNumber == 9)
                {
                    GroundOne.enemyName1 = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                    GroundOne.enemyName2 = Database.ENEMY_WHITENIGHT_GRIZZLY;
                    GroundOne.enemyName3 = Database.ENEMY_FROST_HEART;
                }
                else if (stageNumber == 10)
                {
                    GroundOne.enemyName1 = Database.ENEMY_WIND_BREAKER;
                    GroundOne.enemyName2 = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                    GroundOne.enemyName3 = Database.ENEMY_PURPLE_EYE_WARE_WOLF;
                }
                else if (stageNumber == 11)
                {
                    GroundOne.enemyName1 = Database.ENEMY_TUNDRA_LONGHORN_DEER;
                    GroundOne.enemyName2 = Database.ENEMY_WHITENIGHT_GRIZZLY;
                    GroundOne.enemyName3 = Database.ENEMY_WHITENIGHT_GRIZZLY;
                }
            }
            // 4F
            else if (areaNumber == 3)
            {
                // 4-1
                if (stageNumber == 0)
                {
                    GroundOne.enemyName1 = Database.ENEMY_GENAN_HUNTER;
                    GroundOne.enemyName2 = Database.ENEMY_BEAST_MASTER;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 1)
                {
                    GroundOne.enemyName1 = Database.ENEMY_FALLEN_SEEKER;
                    GroundOne.enemyName2 = Database.ENEMY_ELDER_ASSASSIN;
                    GroundOne.enemyName3 = Database.ENEMY_GENAN_HUNTER;
                }
                else if (stageNumber == 2)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MEPHISTO_RIGHTARM;
                    GroundOne.enemyName2 = Database.ENEMY_FALLEN_SEEKER;
                    GroundOne.enemyName3 = Database.ENEMY_ELDER_ASSASSIN;
                }
                // 4-2
                else if (stageNumber == 3)
                {
                    GroundOne.enemyName1 = Database.ENEMY_EXECUTIONER;
                    GroundOne.enemyName2 = Database.ENEMY_MARIONETTE_NEMESIS;
                    GroundOne.enemyName3 = Database.ENEMY_DARK_MESSENGER;
                }
                else if (stageNumber == 4)
                {
                    GroundOne.enemyName1 = Database.ENEMY_BLACKFIRE_MASTER_BLADE;
                    GroundOne.enemyName2 = Database.ENEMY_MASTER_LOAD;
                    GroundOne.enemyName3 = Database.ENEMY_DARK_MESSENGER;
                }
                else if (stageNumber == 5)
                {
                    GroundOne.enemyName1 = Database.ENEMY_SIN_THE_DARKELF;
                    GroundOne.enemyName2 = Database.ENEMY_BLACKFIRE_MASTER_BLADE;
                    GroundOne.enemyName3 = Database.ENEMY_MARIONETTE_NEMESIS;
                }
                // 4-3
                else if (stageNumber == 6)
                {
                    GroundOne.enemyName1 = Database.ENEMY_ARC_DEMON;
                    GroundOne.enemyName2 = Database.ENEMY_SUN_STRIDER;
                    GroundOne.enemyName3 = Database.ENEMY_ARC_DEMON;
                }
                else if (stageNumber == 7)
                {
                    GroundOne.enemyName1 = Database.ENEMY_GO_FLAME_SLASHER;
                    GroundOne.enemyName2 = Database.ENEMY_UNDEAD_WYVERN;
                    GroundOne.enemyName3 = Database.ENEMY_BALANCE_IDLE;
                }
                else if (stageNumber == 8)
                {
                    GroundOne.enemyName1 = Database.ENEMY_DEVIL_CHILDREN;
                    GroundOne.enemyName2 = Database.ENEMY_ARC_DEMON;
                    GroundOne.enemyName3 = Database.ENEMY_UNDEAD_WYVERN;
                }
                // 4-4
                else if (stageNumber == 9)
                {
                    GroundOne.enemyName1 = Database.ENEMY_HOWLING_HORROR;
                    GroundOne.enemyName2 = Database.ENEMY_PAIN_ANGEL;
                    GroundOne.enemyName3 = Database.ENEMY_CHAOS_WARDEN;
                }
                else if (stageNumber == 10)
                {
                    GroundOne.enemyName1 = Database.ENEMY_DREAD_KNIGHT;
                    GroundOne.enemyName2 = Database.ENEMY_DREAD_KNIGHT;
                    GroundOne.enemyName3 = Database.ENEMY_CHAOS_WARDEN;
                }
                else if (stageNumber == 11)
                {
                    GroundOne.enemyName1 = Database.ENEMY_DOOM_BRINGER;
                    GroundOne.enemyName2 = Database.ENEMY_DREAD_KNIGHT;
                    GroundOne.enemyName3 = Database.ENEMY_PAIN_ANGEL;
                }
            }
            // 5F
            else if (areaNumber == 4)
            {
                if (stageNumber == 0)
                {
                    GroundOne.enemyName1 = Database.ENEMY_PHOENIX;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 1)
                {
                    GroundOne.enemyName1 = Database.ENEMY_NINE_TAIL;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 2)
                {
                    GroundOne.enemyName1 = Database.ENEMY_MEPHISTOPHELES;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 3)
                {
                    GroundOne.enemyName1 = Database.ENEMY_JUDGEMENT;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                }
                else if (stageNumber == 4)
                {
                    GroundOne.enemyName1 = Database.ENEMY_EMERALD_DRAGON;
                    GroundOne.enemyName2 = String.Empty;
                    GroundOne.enemyName3 = String.Empty;
                }
            }

            // 全回復しておく
            if (GroundOne.MC != null)
            {
                GroundOne.SC.ResurrectPlayer(GroundOne.SC.MaxLife);
                GroundOne.MC.MaxGain();
            }
            if (GroundOne.SC != null)
            {
                GroundOne.SC.ResurrectPlayer(GroundOne.SC.MaxLife);
                GroundOne.SC.MaxGain();
            }
            if (GroundOne.TC != null)
            {
                GroundOne.TC.ResurrectPlayer(GroundOne.TC.MaxLife);
                GroundOne.TC.MaxGain();
            }

            // 戦闘開始
            SceneManager.UnloadSceneAsync(Database.TruthMonsterQuest);
            SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, false, false, false, false, false, true, this.areaNumber, this.stageNumber);
        }

        public void TapItemReward(int num)
        {
            Debug.Log("TapItemReward: " + num.ToString());

            int stageNumber = num / 3;
            int itemNumber = num % 3;
            string[,] MQ_REWARD = Database.MQ_FLOOR1_REWARD;
            if (areaNumber == 0)
            {
                MQ_REWARD = Database.MQ_FLOOR1_REWARD;
            }
            else if (areaNumber == 1)
            {
                MQ_REWARD = Database.MQ_FLOOR2_REWARD;
            }
            else if (areaNumber == 2)
            {
                MQ_REWARD = Database.MQ_FLOOR3_REWARD;
            }
            else if (areaNumber == 3)
            {
                MQ_REWARD = Database.MQ_FLOOR4_REWARD;
            }

            string targetItemName = MQ_REWARD[stageNumber, itemNumber];
            // 空文字やNULLアイテムは未登録のため、何もしない。
            if (targetItemName == null || targetItemName == String.Empty || targetItemName == "")
            {
                return;
            }

            if ((areaNumber == 0 && stageNumber == 0 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_1_1) ||
                (areaNumber == 0 && stageNumber == 0 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_1_1) ||
                (areaNumber == 0 && stageNumber == 0 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_1_1) || 
                (areaNumber == 0 && stageNumber == 1 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_2_1) ||
                (areaNumber == 0 && stageNumber == 1 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_2_2) ||
                (areaNumber == 0 && stageNumber == 1 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_2_3) ||
                (areaNumber == 0 && stageNumber == 2 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_3_1) ||
                (areaNumber == 0 && stageNumber == 2 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_3_2) ||
                (areaNumber == 0 && stageNumber == 2 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_3_3) ||
                (areaNumber == 0 && stageNumber == 3 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_4_1) ||
                (areaNumber == 0 && stageNumber == 3 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_4_2) ||
                (areaNumber == 0 && stageNumber == 3 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_4_3) ||
                (areaNumber == 0 && stageNumber == 4 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_5_1) ||
                (areaNumber == 0 && stageNumber == 4 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_5_2) ||
                (areaNumber == 0 && stageNumber == 4 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_5_3) ||
                (areaNumber == 0 && stageNumber == 5 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_6_1) ||
                (areaNumber == 0 && stageNumber == 5 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_6_2) ||
                (areaNumber == 0 && stageNumber == 5 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_6_3) ||
                (areaNumber == 0 && stageNumber == 6 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_7_1) ||
                (areaNumber == 0 && stageNumber == 6 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_7_2) ||
                (areaNumber == 0 && stageNumber == 6 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_7_3) ||
                (areaNumber == 0 && stageNumber == 7 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_8_1) ||
                (areaNumber == 0 && stageNumber == 7 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_8_2) ||
                (areaNumber == 0 && stageNumber == 7 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_8_3) ||
                (areaNumber == 0 && stageNumber == 8 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_9_1) ||
                (areaNumber == 0 && stageNumber == 8 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_9_2) ||
                (areaNumber == 0 && stageNumber == 8 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_9_3) ||
                (areaNumber == 0 && stageNumber == 9 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_10_1) ||
                (areaNumber == 0 && stageNumber == 9 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_10_2) ||
                (areaNumber == 0 && stageNumber == 9 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_10_3) ||
                (areaNumber == 0 && stageNumber == 10 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_11_1) ||
                (areaNumber == 0 && stageNumber == 10 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_11_2) ||
                (areaNumber == 0 && stageNumber == 10 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_11_3) ||
                (areaNumber == 0 && stageNumber == 11 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_12_1) ||
                (areaNumber == 0 && stageNumber == 11 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_12_2) ||
                (areaNumber == 0 && stageNumber == 11 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_12_3) ||
                (areaNumber == 0 && stageNumber == 12 && itemNumber == 0 && GroundOne.WE.MQ_Reward1_13_1) ||
                (areaNumber == 0 && stageNumber == 12 && itemNumber == 1 && GroundOne.WE.MQ_Reward1_13_2) ||
                (areaNumber == 0 && stageNumber == 12 && itemNumber == 2 && GroundOne.WE.MQ_Reward1_13_3) ||
                (areaNumber == 1 && stageNumber == 0 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_1_1) ||
                (areaNumber == 1 && stageNumber == 0 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_1_1) ||
                (areaNumber == 1 && stageNumber == 0 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_1_1) ||
                (areaNumber == 1 && stageNumber == 1 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_2_1) ||
                (areaNumber == 1 && stageNumber == 1 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_2_2) ||
                (areaNumber == 1 && stageNumber == 1 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_2_3) ||
                (areaNumber == 1 && stageNumber == 2 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_3_1) ||
                (areaNumber == 1 && stageNumber == 2 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_3_2) ||
                (areaNumber == 1 && stageNumber == 2 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_3_3) ||
                (areaNumber == 1 && stageNumber == 3 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_4_1) ||
                (areaNumber == 1 && stageNumber == 3 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_4_2) ||
                (areaNumber == 1 && stageNumber == 3 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_4_3) ||
                (areaNumber == 1 && stageNumber == 4 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_5_1) ||
                (areaNumber == 1 && stageNumber == 4 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_5_2) ||
                (areaNumber == 1 && stageNumber == 4 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_5_3) ||
                (areaNumber == 1 && stageNumber == 5 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_6_1) ||
                (areaNumber == 1 && stageNumber == 5 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_6_2) ||
                (areaNumber == 1 && stageNumber == 5 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_6_3) ||
                (areaNumber == 1 && stageNumber == 6 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_7_1) ||
                (areaNumber == 1 && stageNumber == 6 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_7_2) ||
                (areaNumber == 1 && stageNumber == 6 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_7_3) ||
                (areaNumber == 1 && stageNumber == 7 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_8_1) ||
                (areaNumber == 1 && stageNumber == 7 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_8_2) ||
                (areaNumber == 1 && stageNumber == 7 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_8_3) ||
                (areaNumber == 1 && stageNumber == 8 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_9_1) ||
                (areaNumber == 1 && stageNumber == 8 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_9_2) ||
                (areaNumber == 1 && stageNumber == 8 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_9_3) ||
                (areaNumber == 1 && stageNumber == 9 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_10_1) ||
                (areaNumber == 1 && stageNumber == 9 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_10_2) ||
                (areaNumber == 1 && stageNumber == 9 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_10_3) ||
                (areaNumber == 1 && stageNumber == 10 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_11_1) ||
                (areaNumber == 1 && stageNumber == 10 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_11_2) ||
                (areaNumber == 1 && stageNumber == 10 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_11_3) ||
                (areaNumber == 1 && stageNumber == 11 && itemNumber == 0 && GroundOne.WE.MQ_Reward2_12_1) ||
                (areaNumber == 1 && stageNumber == 11 && itemNumber == 1 && GroundOne.WE.MQ_Reward2_12_2) ||
                (areaNumber == 1 && stageNumber == 11 && itemNumber == 2 && GroundOne.WE.MQ_Reward2_12_3) ||
                (areaNumber == 2 && stageNumber == 0 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_1_1) ||
                (areaNumber == 2 && stageNumber == 0 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_1_1) ||
                (areaNumber == 2 && stageNumber == 0 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_1_1) ||
                (areaNumber == 2 && stageNumber == 1 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_2_1) ||
                (areaNumber == 2 && stageNumber == 1 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_2_2) ||
                (areaNumber == 2 && stageNumber == 1 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_2_3) ||
                (areaNumber == 2 && stageNumber == 2 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_3_1) ||
                (areaNumber == 2 && stageNumber == 2 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_3_2) ||
                (areaNumber == 2 && stageNumber == 2 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_3_3) ||
                (areaNumber == 2 && stageNumber == 3 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_4_1) ||
                (areaNumber == 2 && stageNumber == 3 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_4_2) ||
                (areaNumber == 2 && stageNumber == 3 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_4_3) ||
                (areaNumber == 2 && stageNumber == 4 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_5_1) ||
                (areaNumber == 2 && stageNumber == 4 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_5_2) ||
                (areaNumber == 2 && stageNumber == 4 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_5_3) ||
                (areaNumber == 2 && stageNumber == 5 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_6_1) ||
                (areaNumber == 2 && stageNumber == 5 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_6_2) ||
                (areaNumber == 2 && stageNumber == 5 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_6_3) ||
                (areaNumber == 2 && stageNumber == 6 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_7_1) ||
                (areaNumber == 2 && stageNumber == 6 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_7_2) ||
                (areaNumber == 2 && stageNumber == 6 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_7_3) ||
                (areaNumber == 2 && stageNumber == 7 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_8_1) ||
                (areaNumber == 2 && stageNumber == 7 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_8_2) ||
                (areaNumber == 2 && stageNumber == 7 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_8_3) ||
                (areaNumber == 2 && stageNumber == 8 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_9_1) ||
                (areaNumber == 2 && stageNumber == 8 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_9_2) ||
                (areaNumber == 2 && stageNumber == 8 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_9_3) ||
                (areaNumber == 2 && stageNumber == 9 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_10_1) ||
                (areaNumber == 2 && stageNumber == 9 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_10_2) ||
                (areaNumber == 2 && stageNumber == 9 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_10_3) ||
                (areaNumber == 2 && stageNumber == 10 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_11_1) ||
                (areaNumber == 2 && stageNumber == 10 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_11_2) ||
                (areaNumber == 2 && stageNumber == 10 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_11_3) ||
                (areaNumber == 2 && stageNumber == 11 && itemNumber == 0 && GroundOne.WE.MQ_Reward3_12_1) ||
                (areaNumber == 2 && stageNumber == 11 && itemNumber == 1 && GroundOne.WE.MQ_Reward3_12_2) ||
                (areaNumber == 2 && stageNumber == 11 && itemNumber == 2 && GroundOne.WE.MQ_Reward3_12_3) ||
                (areaNumber == 3 && stageNumber == 0 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_1_1) ||
                (areaNumber == 3 && stageNumber == 0 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_1_1) ||
                (areaNumber == 3 && stageNumber == 0 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_1_1) ||
                (areaNumber == 3 && stageNumber == 1 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_2_1) ||
                (areaNumber == 3 && stageNumber == 1 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_2_2) ||
                (areaNumber == 3 && stageNumber == 1 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_2_3) ||
                (areaNumber == 3 && stageNumber == 2 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_3_1) ||
                (areaNumber == 3 && stageNumber == 2 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_3_2) ||
                (areaNumber == 3 && stageNumber == 2 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_3_3) ||
                (areaNumber == 3 && stageNumber == 3 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_4_1) ||
                (areaNumber == 3 && stageNumber == 3 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_4_2) ||
                (areaNumber == 3 && stageNumber == 3 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_4_3) ||
                (areaNumber == 3 && stageNumber == 4 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_5_1) ||
                (areaNumber == 3 && stageNumber == 4 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_5_2) ||
                (areaNumber == 3 && stageNumber == 4 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_5_3) ||
                (areaNumber == 3 && stageNumber == 5 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_6_1) ||
                (areaNumber == 3 && stageNumber == 5 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_6_2) ||
                (areaNumber == 3 && stageNumber == 5 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_6_3) ||
                (areaNumber == 3 && stageNumber == 6 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_7_1) ||
                (areaNumber == 3 && stageNumber == 6 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_7_2) ||
                (areaNumber == 3 && stageNumber == 6 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_7_3) ||
                (areaNumber == 3 && stageNumber == 7 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_8_1) ||
                (areaNumber == 3 && stageNumber == 7 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_8_2) ||
                (areaNumber == 3 && stageNumber == 7 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_8_3) ||
                (areaNumber == 3 && stageNumber == 8 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_9_1) ||
                (areaNumber == 3 && stageNumber == 8 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_9_2) ||
                (areaNumber == 3 && stageNumber == 8 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_9_3) ||
                (areaNumber == 3 && stageNumber == 9 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_10_1) ||
                (areaNumber == 3 && stageNumber == 9 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_10_2) ||
                (areaNumber == 3 && stageNumber == 9 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_10_3) ||
                (areaNumber == 3 && stageNumber == 10 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_11_1) ||
                (areaNumber == 3 && stageNumber == 10 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_11_2) ||
                (areaNumber == 3 && stageNumber == 10 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_11_3) ||
                (areaNumber == 3 && stageNumber == 11 && itemNumber == 0 && GroundOne.WE.MQ_Reward4_12_1) ||
                (areaNumber == 3 && stageNumber == 11 && itemNumber == 1 && GroundOne.WE.MQ_Reward4_12_2) ||
                (areaNumber == 3 && stageNumber == 11 && itemNumber == 2 && GroundOne.WE.MQ_Reward4_12_3)
                )
            {
                txtMainMessage.text = @"サンディ：アイン・ウォーレンスよ！申し訳ないが、【 " + targetItemName + " 】は既に受け渡しが完了されている！";
                return;
            }

            ItemBackPack item = new ItemBackPack(targetItemName);
            bool result = GetNewItem(item);
            if (result == false)
            {
                txtMainMessage.text = @"サンディ：アイン・ウォーレンスよ！荷物がいっぱいのため、受け渡す事が出来ない！バックパックを整理してから参られよ！";
                return;
            }

            txtMainMessage.text = @"サンディ：アイン・ウォーレンスよ！" + targetItemName + "を受け取るが良い！";
            if (areaNumber == 0)
            {
                if (itemNumber == 0) { UpdateBoxOpen(imgBox3[stageNumber], true); }
                else if (itemNumber == 1) { UpdateBoxOpen(imgBox2[stageNumber], true); }
                else if (itemNumber == 2) { UpdateBoxOpen(imgBox3[stageNumber], true); }
                else { Debug.Log("Irregular routine: " + itemNumber.ToString()); }

                if (stageNumber == 0 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_1_1 = true; }
                if (stageNumber == 0 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_1_2 = true; }
                if (stageNumber == 0 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_1_3 = true; }
                if (stageNumber == 1 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_2_1 = true; }
                if (stageNumber == 1 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_2_2 = true; }
                if (stageNumber == 1 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_2_3 = true; }
                if (stageNumber == 2 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_3_1 = true; }
                if (stageNumber == 2 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_3_2 = true; }
                if (stageNumber == 2 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_3_3 = true; }
                if (stageNumber == 3 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_4_1 = true; }
                if (stageNumber == 3 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_4_2 = true; }
                if (stageNumber == 3 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_4_3 = true; }
                if (stageNumber == 4 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_5_1 = true; }
                if (stageNumber == 4 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_5_2 = true; }
                if (stageNumber == 4 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_5_3 = true; }
                if (stageNumber == 5 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_6_1 = true; }
                if (stageNumber == 5 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_6_2 = true; }
                if (stageNumber == 5 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_6_3 = true; }
                if (stageNumber == 6 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_7_1 = true; }
                if (stageNumber == 6 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_7_2 = true; }
                if (stageNumber == 6 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_7_3 = true; }
                if (stageNumber == 7 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_8_1 = true; }
                if (stageNumber == 7 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_8_2 = true; }
                if (stageNumber == 7 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_8_3 = true; }
                if (stageNumber == 8 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_9_1 = true; }
                if (stageNumber == 8 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_9_2 = true; }
                if (stageNumber == 8 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_9_3 = true; }
                if (stageNumber == 9 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_10_1 = true; }
                if (stageNumber == 9 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_10_2 = true; }
                if (stageNumber == 9 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_10_3 = true; }
                if (stageNumber == 10 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_11_1 = true; }
                if (stageNumber == 10 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_11_2 = true; }
                if (stageNumber == 10 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_11_3 = true; }
                if (stageNumber == 11 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_12_1 = true; }
                if (stageNumber == 11 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_12_2 = true; }
                if (stageNumber == 11 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_12_3 = true; }
                if (stageNumber == 12 && itemNumber == 0) { GroundOne.WE.MQ_Reward1_13_1 = true; }
                if (stageNumber == 12 && itemNumber == 1) { GroundOne.WE.MQ_Reward1_13_2 = true; }
                if (stageNumber == 12 && itemNumber == 2) { GroundOne.WE.MQ_Reward1_13_3 = true; }
            }
            else if (areaNumber == 1)
            {
                if (stageNumber == 0 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_1_1 = true; }
                if (stageNumber == 0 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_1_2 = true; }
                if (stageNumber == 0 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_1_3 = true; }
                if (stageNumber == 1 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_2_1 = true; }
                if (stageNumber == 1 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_2_2 = true; }
                if (stageNumber == 1 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_2_3 = true; }
                if (stageNumber == 2 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_3_1 = true; }
                if (stageNumber == 2 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_3_2 = true; }
                if (stageNumber == 2 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_3_3 = true; }
                if (stageNumber == 3 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_4_1 = true; }
                if (stageNumber == 3 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_4_2 = true; }
                if (stageNumber == 3 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_4_3 = true; }
                if (stageNumber == 4 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_5_1 = true; }
                if (stageNumber == 4 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_5_2 = true; }
                if (stageNumber == 4 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_5_3 = true; }
                if (stageNumber == 5 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_6_1 = true; }
                if (stageNumber == 5 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_6_2 = true; }
                if (stageNumber == 5 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_6_3 = true; }
                if (stageNumber == 6 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_7_1 = true; }
                if (stageNumber == 6 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_7_2 = true; }
                if (stageNumber == 6 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_7_3 = true; }
                if (stageNumber == 7 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_8_1 = true; }
                if (stageNumber == 7 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_8_2 = true; }
                if (stageNumber == 7 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_8_3 = true; }
                if (stageNumber == 8 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_9_1 = true; }
                if (stageNumber == 8 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_9_2 = true; }
                if (stageNumber == 8 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_9_3 = true; }
                if (stageNumber == 9 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_10_1 = true; }
                if (stageNumber == 9 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_10_2 = true; }
                if (stageNumber == 9 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_10_3 = true; }
                if (stageNumber == 10 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_11_1 = true; }
                if (stageNumber == 10 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_11_2 = true; }
                if (stageNumber == 10 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_11_3 = true; }
                if (stageNumber == 11 && itemNumber == 0) { GroundOne.WE.MQ_Reward2_12_1 = true; }
                if (stageNumber == 11 && itemNumber == 1) { GroundOne.WE.MQ_Reward2_12_2 = true; }
                if (stageNumber == 11 && itemNumber == 2) { GroundOne.WE.MQ_Reward2_12_3 = true; }
            }
            else if (areaNumber == 2)
            {
                if (stageNumber == 0 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_1_1 = true; }
                if (stageNumber == 0 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_1_2 = true; }
                if (stageNumber == 0 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_1_3 = true; }
                if (stageNumber == 1 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_2_1 = true; }
                if (stageNumber == 1 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_2_2 = true; }
                if (stageNumber == 1 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_2_3 = true; }
                if (stageNumber == 2 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_3_1 = true; }
                if (stageNumber == 2 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_3_2 = true; }
                if (stageNumber == 2 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_3_3 = true; }
                if (stageNumber == 3 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_4_1 = true; }
                if (stageNumber == 3 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_4_2 = true; }
                if (stageNumber == 3 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_4_3 = true; }
                if (stageNumber == 4 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_5_1 = true; }
                if (stageNumber == 4 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_5_2 = true; }
                if (stageNumber == 4 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_5_3 = true; }
                if (stageNumber == 5 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_6_1 = true; }
                if (stageNumber == 5 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_6_2 = true; }
                if (stageNumber == 5 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_6_3 = true; }
                if (stageNumber == 6 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_7_1 = true; }
                if (stageNumber == 6 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_7_2 = true; }
                if (stageNumber == 6 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_7_3 = true; }
                if (stageNumber == 7 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_8_1 = true; }
                if (stageNumber == 7 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_8_2 = true; }
                if (stageNumber == 7 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_8_3 = true; }
                if (stageNumber == 8 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_9_1 = true; }
                if (stageNumber == 8 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_9_2 = true; }
                if (stageNumber == 8 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_9_3 = true; }
                if (stageNumber == 9 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_10_1 = true; }
                if (stageNumber == 9 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_10_2 = true; }
                if (stageNumber == 9 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_10_3 = true; }
                if (stageNumber == 10 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_11_1 = true; }
                if (stageNumber == 10 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_11_2 = true; }
                if (stageNumber == 10 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_11_3 = true; }
                if (stageNumber == 11 && itemNumber == 0) { GroundOne.WE.MQ_Reward3_12_1 = true; }
                if (stageNumber == 11 && itemNumber == 1) { GroundOne.WE.MQ_Reward3_12_2 = true; }
                if (stageNumber == 11 && itemNumber == 2) { GroundOne.WE.MQ_Reward3_12_3 = true; }
            }
            else if (areaNumber == 3)
            {
                if (stageNumber == 0 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_1_1 = true; }
                if (stageNumber == 0 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_1_2 = true; }
                if (stageNumber == 0 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_1_3 = true; }
                if (stageNumber == 1 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_2_1 = true; }
                if (stageNumber == 1 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_2_2 = true; }
                if (stageNumber == 1 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_2_3 = true; }
                if (stageNumber == 2 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_3_1 = true; }
                if (stageNumber == 2 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_3_2 = true; }
                if (stageNumber == 2 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_3_3 = true; }
                if (stageNumber == 3 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_4_1 = true; }
                if (stageNumber == 3 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_4_2 = true; }
                if (stageNumber == 3 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_4_3 = true; }
                if (stageNumber == 4 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_5_1 = true; }
                if (stageNumber == 4 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_5_2 = true; }
                if (stageNumber == 4 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_5_3 = true; }
                if (stageNumber == 5 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_6_1 = true; }
                if (stageNumber == 5 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_6_2 = true; }
                if (stageNumber == 5 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_6_3 = true; }
                if (stageNumber == 6 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_7_1 = true; }
                if (stageNumber == 6 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_7_2 = true; }
                if (stageNumber == 6 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_7_3 = true; }
                if (stageNumber == 7 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_8_1 = true; }
                if (stageNumber == 7 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_8_2 = true; }
                if (stageNumber == 7 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_8_3 = true; }
                if (stageNumber == 8 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_9_1 = true; }
                if (stageNumber == 8 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_9_2 = true; }
                if (stageNumber == 8 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_9_3 = true; }
                if (stageNumber == 9 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_10_1 = true; }
                if (stageNumber == 9 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_10_2 = true; }
                if (stageNumber == 9 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_10_3 = true; }
                if (stageNumber == 10 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_11_1 = true; }
                if (stageNumber == 10 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_11_2 = true; }
                if (stageNumber == 10 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_11_3 = true; }
                if (stageNumber == 11 && itemNumber == 0) { GroundOne.WE.MQ_Reward4_12_1 = true; }
                if (stageNumber == 11 && itemNumber == 1) { GroundOne.WE.MQ_Reward4_12_2 = true; }
                if (stageNumber == 11 && itemNumber == 2) { GroundOne.WE.MQ_Reward4_12_3 = true; }
            }
            else if (areaNumber == 4)
            {
            }

            Debug.Log("TapItemReward(E)");
        }

        public void TapItemInfo(int num)
        {
            Debug.Log("TapItemReward: " + num.ToString());

            int stageNumber = num / 3;
            int itemNumber = num % 3;
            string[,] MQ_REWARD = Database.MQ_FLOOR1_REWARD;
            if (areaNumber == 0)
            {
                MQ_REWARD = Database.MQ_FLOOR1_REWARD;
            }
            else if (areaNumber == 1)
            {
                MQ_REWARD = Database.MQ_FLOOR2_REWARD;
            }
            else if (areaNumber == 2)
            {
                MQ_REWARD = Database.MQ_FLOOR3_REWARD;
            }
            else if (areaNumber == 3)
            {
                MQ_REWARD = Database.MQ_FLOOR4_REWARD;
            }

            string targetItemName = MQ_REWARD[stageNumber, itemNumber];
            // 空文字やNULLアイテムは未登録のため、何もしない。
            if (targetItemName == null || targetItemName == String.Empty || targetItemName == "")
            {
                return;
            }
            ItemBackPack item = new ItemBackPack(targetItemName);
            if (item == null) { return; }
            if (item.Name == String.Empty || item.Name == "") { return; }

            txtMainMessage.text = "報酬 ＜ " + item.Name + " ＞  " + item.Description;
        }

        public void TapFilterClose()
        {
            this.Filter.SetActive(false);
            this.groupSystemMessage.SetActive(false);
        }

        public void TapClose()
        {
            SceneDimension.Back(this);
        }
    }
}
﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace DungeonPlayer
{
    public partial class TruthItemBank : MotherForm
    {
        const int MAX_VIEW_PAGE = 10;
        const int MAX_PLAYER_LIST_PAGE = 2;

        // GUI
        public GameObject groupPlayerButton;
        public Button player1Button;
        public Button player2Button;
        public Button player3Button;
        public Button[] bankItemsList;
        public Text[] bankItemText;
        public Text[] bankItemStack;
        public Button[] playerItemList;
        public Text[] playerItemText;
        public Text[] playerItemStack;
        public Button[] back_BankListPage;
        public Text[] bankListPage;
        public Button[] back_PlayerListPage;
        public Text[] playerListPage;
        public Text mainMessage;

        string[] items = new string[Database.MAX_ITEM_BANK];
        int[] stacks = new int[Database.MAX_ITEM_BANK];

        int currentListPage = 1; // 初めページ番号が１、それをデフォルトとする。
        int p_currentListPage = 1;
        MainCharacter currentPlayer = null;
        Button back_currentPlayerItem = null;
        Text currentPlayerItem = null;
        Button back_currentBankItem = null;
        Text currentBankItem = null;

        int shadowPage1 = 1;
        int shadowPage2 = 1;
        int shadowPage3 = 1;

        public override void Start()
        {
            base.Start();

            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter) { groupPlayerButton.SetActive(false); }
            //if (!GroundOne.WE.AvailableThirdCharacter) player3Button.gameObject.SetActive(false); // after ３人目を見せるのかどうか、ストーリーと合わせて考える

            GroundOne.WE.LoadItemBankData(ref items, ref stacks);

            PageNumber1_Click(bankListPage[0]);

            player1Button_Click(player1Button);
            p_PageNumber1_Click(playerListPage[0]);
        }

        public override void Update()
        {
            base.Update();
        }

        private void PageNumber1_Click(Text sender)
        {
            this.currentListPage = Convert.ToInt32(sender.text);

            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (bankListPage[ii].Equals(sender))
                {
                    back_BankListPage[ii].GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    back_BankListPage[ii].GetComponent<Image>().color = UnityColor.LightYellow;
                }
            }
            if (this.currentBankItem != null)
            {
                this.back_currentBankItem.GetComponent<Image>().color = UnityColor.GhostWhite;
                this.back_currentBankItem = null;
                this.currentBankItem = null;
            }

            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                bankItemText[ii].text = items[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii];
                if (bankItemText[ii].text == null || bankItemText[ii].text == string.Empty || bankItemText[ii].text == "")
                {
                    bankItemStack[ii].text = "";
                }
                else
                {
                    bankItemStack[ii].text = "x" + stacks[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii].ToString();
                }
            }
        }

        private void p_PageNumber1_Click(Text sender)
        {
            this.p_currentListPage = Convert.ToInt32(sender.text);

            for (int ii = 0; ii < MAX_PLAYER_LIST_PAGE; ii++)
            {
                if (playerListPage[ii].Equals(sender))
                {
                    back_PlayerListPage[ii].GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    back_PlayerListPage[ii].GetComponent<Image>().color = UnityColor.LightYellow;
                }
            }

            if (this.currentPlayerItem != null)
            {
                if (this.currentPlayer == GroundOne.MC)
                {
                    this.back_currentPlayerItem.GetComponent<Image>().color = UnityColor.LightSkyBlue;
                }
                else if (this.currentPlayer == GroundOne.SC)
                {
                    this.back_currentPlayerItem.GetComponent<Image>().color = UnityColor.Pink;
                }
                else if (this.currentPlayer == GroundOne.TC)
                {
                    this.back_currentPlayerItem.GetComponent<Image>().color = UnityColor.Gold;
                }
                this.currentPlayerItem = null;
            }

            if (this.currentPlayer == GroundOne.MC)
            {
                shadowPage1 = this.p_currentListPage;
            }
            else if (this.currentPlayer == GroundOne.SC)
            {
                shadowPage2 = this.p_currentListPage;
            }
            else if (this.currentPlayer == GroundOne.TC)
            {
                shadowPage3 = this.p_currentListPage;
            }

            if (this.currentPlayer != null)
            {
                ItemBackPack[] info = this.currentPlayer.GetBackPackInfo();
                string[] items = new string[Database.MAX_BACKPACK_SIZE];
                int[] stacks = new int[Database.MAX_BACKPACK_SIZE];
                for (int ii = 0; ii < info.Length; ii++)
                {
                    if (info[ii] != null)
                    {
                        items[ii] = info[ii].Name;
                        stacks[ii] = info[ii].StackValue;
                    }
                }
                for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
                {
                    playerItemText[ii].text = items[(this.p_currentListPage - 1) * MAX_VIEW_PAGE + ii];
                    if (playerItemText[ii].text == null || playerItemText[ii].text == string.Empty || playerItemText[ii].text == "")
                    {
                        playerItemStack[ii].text = "";
                    }
                    else
                    {
                        playerItemStack[ii].text = "x" + stacks[(this.p_currentListPage - 1) * MAX_VIEW_PAGE + ii].ToString();
                    }
                }
            }
        }

        public void player1Button_Click(Button sender)
        {
            if (sender.GetComponent<Image>().color == Color.blue)
            {
                this.currentPlayer = GroundOne.MC;
                for (int ii = 0; ii < playerItemList.Length; ii++)
                {
                    playerItemList[ii].GetComponent<Image>().color = UnityColor.LightSkyBlue;
                }
                this.p_currentListPage = shadowPage1;
            }
            else if (sender.GetComponent<Image>().color == Color.red)
            {
                this.currentPlayer = GroundOne.SC;
                for (int ii = 0; ii < playerItemList.Length; ii++)
                {
                    playerItemList[ii].GetComponent<Image>().color = UnityColor.Pink;
                }
                this.p_currentListPage = shadowPage2;
            }
            else if (sender.GetComponent<Image>().color == Color.yellow)
            {
                this.currentPlayer = GroundOne.TC;
                for (int ii = 0; ii < playerItemList.Length; ii++)
                {
                    playerItemList[ii].GetComponent<Image>().color = UnityColor.Gold;
                }
                this.p_currentListPage = shadowPage3;
            }

            if (this.p_currentListPage == 1) p_PageNumber1_Click(this.playerListPage[0]);
            else if (this.p_currentListPage == 2) p_PageNumber1_Click(this.playerListPage[1]);
        }

        private void FromBackpackToPlayer_Click(object sender)
        {
            if (this.currentBankItem == null) return; // 空選択は何もしない。
            if (this.currentBankItem.text == String.Empty || this.currentBankItem.text == "" || this.currentBankItem.text == null) return;

            int stackValue = 0;
            int targetDeleteNum = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(bankItemsList[ii]))
                {
                    targetDeleteNum = (this.currentListPage - 1) * 10 + ii;
                    stackValue = stacks[targetDeleteNum];
                    break;
                }
            }

            int addedNumber = 0;
            bool result = this.currentPlayer.AddBackPack(new ItemBackPack(this.currentBankItem.text), stackValue, ref addedNumber);
            if (result == false)
            {
                mainMessage.text = currentPlayer.GetCharacterSentence(2034);
                return;
            }

            if (this.p_currentListPage == 1) p_PageNumber1_Click(playerListPage[0]);
            else if (this.p_currentListPage == 2) p_PageNumber1_Click(playerListPage[1]);

            this.currentBankItem.text = string.Empty;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(bankItemsList[ii]))
                {
                    bankItemStack[ii].text = "";
                }
            }
            items[targetDeleteNum] = string.Empty;
            stacks[targetDeleteNum] = 0;

            this.p_currentListPage = (addedNumber / MAX_VIEW_PAGE) + 1;
            p_PageNumber1_Click(playerListPage[this.p_currentListPage - 1]);
            PlayerItem_Click(playerItemText[addedNumber % MAX_VIEW_PAGE]);
        }

        private void FromPlayerToBackpack_Click(object sender)
        {
            if (this.currentPlayerItem == null) return; // 空選択は何もしない。
            if (this.currentPlayerItem.text == String.Empty || this.currentPlayerItem.text == "" || this.currentPlayerItem.text == null) return;
            if (TruthItemAttribute.CheckImportantItem(this.currentPlayerItem.text) != TruthItemAttribute.Transfer.Any)
            {
                mainMessage.text = this.currentPlayer.GetCharacterSentence(2033);
                return;
            }

            int stackValue = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentPlayerItem.Equals(playerItemList[ii]))
                {
                    stackValue = this.currentPlayer.CheckBackPackExist(new ItemBackPack(this.currentPlayerItem.text), (this.p_currentListPage - 1) * 10 + ii);
                    break;
                }
            }

            this.currentPlayer.DeleteBackPack(new ItemBackPack(this.currentPlayerItem.text));

            for (int ii = 0; ii < Database.MAX_ITEM_BANK; ii++)
            {
                if ((items[ii] == null) || (items[ii] == String.Empty) || (items[ii] == ""))
                {
                    items[ii] = this.currentPlayerItem.text;
                    stacks[ii] = stackValue;
                    this.currentPlayerItem.text = string.Empty;
                    for (int jj = 0; jj < MAX_VIEW_PAGE; jj++)
                    {
                        if (this.currentPlayerItem.Equals(playerItemList[jj]))
                        {
                            playerItemStack[jj].text = "";
                        }
                    }

                    currentListPage = (ii / MAX_VIEW_PAGE) + 1;
                    PageNumber1_Click(bankListPage[this.currentListPage - 1]);
                    BankItem_Click(bankItemText[ii % MAX_VIEW_PAGE]);
                    return; // 余計なFor回しはせず終了する。
                }
            }
        }

        public void PlayerItem_Click(Text sender)
        {
            this.currentPlayerItem = (Text)sender;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (sender.Equals(playerItemText[ii]))
                {
                    this.back_currentPlayerItem = playerItemList[ii];
                    playerItemList[ii].GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    if (this.currentPlayer == GroundOne.MC)
                    {
                        playerItemList[ii].GetComponent<Image>().color = UnityColor.LightSkyBlue;
                    }
                    else if (this.currentPlayer == GroundOne.SC)
                    {
                        playerItemList[ii].GetComponent<Image>().color = UnityColor.Pink;
                    }
                    else if (this.currentPlayer == GroundOne.TC)
                    {
                        playerItemList[ii].GetComponent<Image>().color = UnityColor.Gold;
                    }
                }
            }
        }

        public void BankItem_Click(Text sender)
        {
            this.currentBankItem = (Text)sender;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (sender.Equals(bankItemText[ii]))
                {
                    this.back_currentBankItem = bankItemsList[ii];
                    bankItemsList[ii].GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    bankItemsList[ii].GetComponent<Image>().color = UnityColor.GhostWhite;
                }
            }
        }

        public void tapClose()
        {
            GroundOne.WE.UpdateItemBankData(items, stacks);
            GroundOne.ParentScene.SceneBack();
            Application.UnloadLevel(Database.TruthItemBank);
        }
    }
}
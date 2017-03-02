using UnityEngine;
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
        public Text lblTitle;
        public Text lblWerehouse;
        public Text lblBackpack;
        public Text lblClose;

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

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblTitle.text = Database.GUI_HANNA_TITLE;
                lblWerehouse.text = Database.GUI_HANNA_ITEM;
                lblBackpack.text = Database.GUI_HANNA_BACKPACK;
                lblClose.text = Database.GUI_HANNA_CLOSE;
            }

            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter) { groupPlayerButton.SetActive(false); }
            //if (!GroundOne.WE.AvailableThirdCharacter) player3Button.gameObject.SetActive(false); // after ３人目を見せるのかどうか、ストーリーと合わせて考える

            GroundOne.WE.LoadItemBankData(ref items, ref stacks);

            BankPageNumber_Click(bankListPage[0]);
            PlayerButton_Click(player1Button);
            BackpackPageNumber_Click(playerListPage[0]);
        }

        public override void Update()
        {
            base.Update();
        }

        public void BankPageNumber_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_LEFTPAGE, sender.text, String.Empty);
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
                    Debug.Log("stacks: " + ii.ToString() + " " + stacks[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii]);
                    bankItemStack[ii].text = "x" + stacks[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii].ToString();
                }
            }
        }

        public void BackpackPageNumber_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_RIGHTPAGE, sender.text, String.Empty);
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

        public void PlayerButton_Click(Button sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_PLAYER, sender.name, String.Empty);
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

            if (this.p_currentListPage == 1) BackpackPageNumber_Click(this.playerListPage[0]);
            else if (this.p_currentListPage == 2) BackpackPageNumber_Click(this.playerListPage[1]);
        }

        public void FromBackpackToPlayer_Click()
        {
            if (this.currentBankItem == null) return; // 空選択は何もしない。
            if (this.currentBankItem.text == String.Empty || this.currentBankItem.text == "" || this.currentBankItem.text == null) return;

            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_LEFTTORIGHT, this.currentBankItem.text, String.Empty);

            int stackValue = 0;
            int targetDeleteNum = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(bankItemText[ii]))
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

            if (this.p_currentListPage == 1) BackpackPageNumber_Click(playerListPage[0]);
            else if (this.p_currentListPage == 2) BackpackPageNumber_Click(playerListPage[1]);

            this.currentBankItem.text = string.Empty;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(bankItemText[ii]))
                {
                    bankItemStack[ii].text = "";
                }
            }
            items[targetDeleteNum] = string.Empty;
            stacks[targetDeleteNum] = 0;

            this.p_currentListPage = (addedNumber / MAX_VIEW_PAGE) + 1;
            BackpackPageNumber_Click(playerListPage[this.p_currentListPage - 1]);
            PlayerItem_Click(playerItemText[addedNumber % MAX_VIEW_PAGE]);
        }

        public void FromPlayerToBackpack_Click()
        {
            if (this.currentPlayerItem == null) { return; } // 空選択は何もしない。
            if (this.currentPlayerItem.text == String.Empty || this.currentPlayerItem.text == "" || this.currentPlayerItem.text == null) return;

            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_RIGHTTOLEFT, this.currentPlayerItem.text, String.Empty);

            if (TruthItemAttribute.CheckImportantItem(this.currentPlayerItem.text) != TruthItemAttribute.Transfer.Any)
            {
                mainMessage.text = this.currentPlayer.GetCharacterSentence(2033);
                return;
            }

            int stackValue = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentPlayerItem.Equals(playerItemText[ii]))
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
                        if (this.currentPlayerItem.Equals(playerItemText[jj]))
                        {
                            playerItemStack[jj].text = "";
                        }
                    }

                    currentListPage = (ii / MAX_VIEW_PAGE) + 1;
                    BankPageNumber_Click(bankListPage[this.currentListPage - 1]);
                    BankItem_Click(bankItemText[ii % MAX_VIEW_PAGE]);
                    return; // 余計なFor回しはせず終了する。
                }
            }
        }

        public void PlayerItem_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_RIGHTITEM, sender.text, String.Empty);

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
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_LEFTITEM, sender.text, String.Empty);
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
            GroundOne.SQL.UpdateOwner(Database.LOG_ITEMBANK_CLOSE, String.Empty, String.Empty);
            GroundOne.WE.UpdateItemBankData(items, stacks);
            SceneDimension.Back(this);
        }
    }
}

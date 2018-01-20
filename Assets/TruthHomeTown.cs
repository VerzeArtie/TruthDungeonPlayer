using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;

namespace DungeonPlayer
{
    public class TruthHomeTown : MotherForm
    {
        public enum SupportType
        {
            Begin,
            FromDuelGate,
            FromDungeonGate,
        }
        int nowReading = 0;
        List<string> nowMessage = new List<string>();
        List<DungeonPlayer.MessagePack.ActionEvent> nowEvent = new List<DungeonPlayer.MessagePack.ActionEvent>();

        private int firstDay = 1;
        bool waitMessage = false;

        // GUI
        public Canvas parent;
        public Button btnOK;
        public GameObject systemMessagePanel;
        public Text systemMessage;
        public Camera cam;
        public GameObject groupRestInnMenu;
        public Button btnRestInn;
        public Button btnItemBank;
        public GameObject groupDuelSelect;
        public Text DuelMessageText;
        public Text honorText;
        public Text opponentDuelText_EN;
        public Text opponentDuelText_JP;
        public Text DuelResultText;
        public Button btnOpponentInfo;
        public Button btnCheckDuelRule;
        public Button btnDuelSelectClose;
        public GameObject groupTicketChoice;
        public Button[] selectTicketChoice;
        public GameObject groupSelectCastleMenu;
        public Button[] selectCastleMenu;
        public GameObject groupSelectGate;
        public Button[] selectGate;
        public GameObject groupSelectDungeon;
        public Button[] selectDungeon;
        public GameObject groupMenu;
        public Button buttonHanna;
        public Button buttonDungeon;
        public Button buttonRana;
        public Button buttonGanz;
        public Button buttonShinikia;
        public Button buttonPotion;
        public Button buttonDuel;
        public Button buttonStatus;
        public Button buttonBattleSetting;
        public Button buttonSave;
        public Button buttonLoad;
        public Button buttonExit;
        public GameObject panelObjective;
        public GameObject panelObjectiveTitle;
        public Text txtObjectiveTitle;
        public GameObject panelGroupQuest;
        public List<GameObject> panelObjectiveTxt;
        public List<Text> txtObjective;
        public Text dayLabel;
        public Image panelHide;
        public Image imgCharacter1;
        public Image imgCharacter2;
        public Image imgBackground;
	    public InputField nameField;
	    public Text inputName;
        public Text mainMessage;
        public Image panelMessage;
        public Image backgroundData;
        public GameObject panelEnding;
        public GameObject panelSubMenu;
        public Text txtMenuStatus;
        public Text txtMenuBattleSetting;
        public Text txtMenuSave;
        public Text txtMenuLoad;
        public Text txtMenuExit;

	    public static int serverPort = 8001;
	    private bool firstAction = false;
	    private string targetViewName = string.Empty;
        private bool nowHideing = false;
        public string currentRequestFood = string.Empty;

        bool forceSaveCall = false; // シナリオ進行上、強制セーブした後、”休息しました”を表示しするためのフラグ
        bool nowAfterRestMessage = false; // 宿屋で休息の後、”休息しました”を表示するためのフラグ
        private string MESSAGE_AUTOSAVE_EXIT = @"ここまでの記録は自動セーブとなります。次回起動は、ここから再開となります";

        bool fromGoDungeon = false;
        bool nowTalkingOlRandis = false;
        bool nowTalkingLanaAmiria = false;

        bool nowAnimationEnding = false;
        bool nowAnimationEnding_First = false;

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

            GroundOne.WE.SaveByDungeon = false;

            // after
            //GroundOne.CS = new ClientSocket();
            //GroundOne.InitializeNetworkConnection ();

            bool potionVisible = (GroundOne.WE.AvailablePotionshop && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonPotion.gameObject.SetActive(potionVisible);

            bool shinikiaVisible = (GroundOne.WE.AvailableBackGate && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonShinikia.gameObject.SetActive(shinikiaVisible);

            bool duelVisible = (GroundOne.WE.AvailableDuelColosseum && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonDuel.gameObject.SetActive(duelVisible);

            if (GroundOne.DuelMode && GroundOne.enemyName1 == Database.VERZE_ARTIE)
            {
                BlackOut(); 
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FIELD_OF_FIRSTPLACE);
            }
            else if (!GroundOne.WE.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }

            if (GroundOne.TruthHomeTown_NowExit)
            {
                base.yesnoSystemMessage.text = Database.exitMessage2;
                groupYesnoSystemMessage.SetActive(true);
            }

            buttonBattleSetting.gameObject.SetActive(GroundOne.WE.AvailableBattleSettingMenu);

            this.dayLabel.text = GroundOne.WE.GameDay.ToString() + "日目";
            if (GroundOne.WE.AlreadyRest)
            {
                this.firstDay = GroundOne.WE.GameDay - 1; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
                if (this.firstDay <= 0) this.firstDay = 1; // [警告] 後編初日のロジック崩れによる回避手段。あまり良い直し方ではありません。
            }
            else
            {
                this.firstDay = GroundOne.WE.GameDay; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
            }

            GroundOne.ObjectiveList.Clear();
            TruthObjective.GetObjectiveList();
            UpdateTxtObjective();

            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }
        
        string GetString(string msg, string protocolStr)
        {
            return msg.Substring(protocolStr.Length, msg.Length - protocolStr.Length);
        }
        
        List<Text> endingMessage = new List<Text>();
        List<Text> endingMessage2 = new List<Text>();
        List<Text> endingMessage3 = new List<Text>();
        private void ConstructEndingMessage(List<Text> messageList, Vector2 sizeDelta, TextAnchor txtAnchor, string text)
        {
            GameObject obj = new GameObject();
            Text element = obj.AddComponent<Text>();
            element.fontStyle = FontStyle.Normal;
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            element.font = ArialFont;
            element.transform.SetParent(parent.transform);
            element.rectTransform.localScale = new Vector3(1, 1, 1);
            element.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            element.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            element.rectTransform.sizeDelta = sizeDelta;
            element.color = Color.black;
            element.gameObject.SetActive(false);
            element.text = text;
            element.alignment = txtAnchor;
            element.transform.SetParent(panelEnding.transform);
            messageList.Add(element);
        }

        public void enemy_click(Text txtName)
        {
            GroundOne.enemyName1 = txtName.text;
            SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, false, false, false, false);
        }
        public void debug_click()
        {
            GroundOne.enemyName1 = Database.ENEMY_HIYOWA_BEATLE;
            SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, false, false, false, false);
        }

	    // Update is called once per frame
	    public override void Update () {
            base.Update();

            #region "エンディング"
            if (this.nowAnimationEnding)
            {
                if (this.panelEnding.GetComponent<Image>().color.a < 1.0f)
                {
                    Color current = this.panelEnding.GetComponent<Image>().color;
                    this.panelEnding.GetComponent<Image>().color = new Color(current.r, current.g, current.g, current.a + 0.125f);
                    System.Threading.Thread.Sleep(1000);
                    return;
                }

                if (!this.nowAnimationEnding_First)
                {
                    this.nowAnimationEnding_First = true;
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                    for (int ii = this.endingMessage.Count - 1; ii >= 0; ii--)
                    {
                        this.endingMessage[ii].transform.localPosition = new Vector3(-Screen.width / 4, -ii * 40 - Screen.height / 2 - 50, 0);
                        this.endingMessage[ii].gameObject.SetActive(true);
                    }
                    for (int ii = this.endingMessage2.Count - 1; ii >= 0; ii--)
                    {
                        this.endingMessage2[ii].transform.localPosition = new Vector3(Screen.width / 4, -ii * 140 - Screen.height / 2 - 50, 0);
                        this.endingMessage2[ii].gameObject.SetActive(true);
                    }
                    for (int ii = this.endingMessage3.Count - 1; ii >= 0; ii--)
                    {
                        this.endingMessage3[ii].color = new Color(0, 0, 0, 0);
                        this.endingMessage3[ii].transform.localPosition = new Vector3(-Screen.width / 4, 0, 0);
                        this.endingMessage3[ii].gameObject.SetActive(true);
                    }
                    return;
                }

                float move = 0.1f;
                float lastPosition = this.endingMessage[this.endingMessage.Count - 1].transform.position.y;
                if (lastPosition < -Screen.height / 4)
                {
                    move = 0.15f;
                }
                else if (-Screen.height / 4 <= lastPosition && lastPosition < 0)
                {
                    move = 0.3f;
                }
                else if (0 <= lastPosition && lastPosition <= Screen.height / 4)
                {
                    move = 0.5f;
                }
                else if (Screen.height / 4 <= lastPosition && lastPosition < Screen.height+100)
                {
                    move = 1.0f;
                }
                else
                {
                    move = 0;
                }
                if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                {
                    move = move * 2.5f;
                }

                for (int ii = 0; ii < this.endingMessage.Count; ii++)
                {
                    this.endingMessage[ii].transform.position = new Vector3(this.endingMessage[ii].transform.position.x, this.endingMessage[ii].transform.position.y + move, this.endingMessage[ii].transform.position.z);
                }
                for (int ii = 0; ii < this.endingMessage2.Count; ii++)
                {
                    this.endingMessage2[ii].transform.position = new Vector3(this.endingMessage2[ii].transform.position.x, this.endingMessage2[ii].transform.position.y + move, this.endingMessage2[ii].transform.position.z);
                }

                if (move == 0)
                {
                    float moveX = 0.1f;
                    float alpha = this.endingMessage3[0].color.a;
                    if (this.endingMessage3[0].transform.position.x < 400)
                    {
                        float lastPositionX = this.endingMessage3[this.endingMessage3.Count - 1].transform.position.x;
                        if (this.endingMessage3[0].color.a < 1.0f)
                        {
                            moveX = 0.2f;
                            this.endingMessage3[0].color = new Color(0, 0, 0, this.endingMessage3[0].color.a + 0.005f);
                        }
                    }
                    else
                    {
                        if (alpha > 0.5f)
                        {
                            moveX = 0.3f;
                        }
                        else if (alpha > 0.25f)
                        {
                            moveX = 0.5f;
                        }
                        else
                        {
                            moveX = 0.7f;
                        }
                        this.endingMessage3[0].color = new Color(0, 0, 0, this.endingMessage3[0].color.a - 0.0025f);

                        if (alpha <= 0.0f)
                        {
                            GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_ENDING);
                            this.nowAnimationEnding = true;
                            GroundOne.WE2.SeekerEnd = true;
                            GroundOne.WE.TruthCompleteArea5 = true;
                            GroundOne.WE.TruthCompleteArea5Day = GroundOne.WE.GameDay;
                            Method.AutoSaveRealWorld();
                            Method.AutoSaveTruthWorldEnvironment();
                            Method.ExecSave(null, Database.WorldSaveNum, true);
                            SceneDimension.JumpToTitle();
                        }
                    }
                    this.endingMessage3[0].transform.position = new Vector3(this.endingMessage3[0].transform.position.x + moveX, this.endingMessage3[0].transform.position.y, this.endingMessage3[0].transform.position.z);

                }

                return;
            }
            #endregion

            if (this.firstAction == false)
            {
                this.firstAction = true;
                ShownEvent();
            }
            if (this.panelMessage.gameObject.activeInHierarchy && btnOK.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    tapOK();
                }
            }
	    }

        private void ShownEvent()
        {
            #region "チュートリアル"
            if (GroundOne.TutorialMode)
            {
                buttonDuel.gameObject.SetActive(true);
                panelSubMenu.SetActive(false);
                return;
            }
            #endregion

            #region "エンディング"
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
            {
                MessagePack.Message20601(ref nowMessage, ref nowEvent);
                NormalTapOK();
                return;
            }
            #endregion

            #region "DUEL対戦結果"
            if (GroundOne.DuelMode)
            {
                Debug.Log("GroundOne.DuelMode");
                mainMessage.text = "";
                GroundOne.DuelMode = false;
                string opponentDuelist = GroundOne.OpponentDuelist;
                GroundOne.OpponentDuelist = string.Empty;
                bool duelWin = false;

                if (GroundOne.BattleResult == GroundOne.battleResult.OK)
                {
                    duelWin = true;
                }
                GroundOne.BattleResult = GroundOne.battleResult.None;
                Method.CopyShadowToMain();

                // DUEL対戦相手、および、勝敗結果に応じた処理
                if (GroundOne.enemyName1 == Database.DUEL_OL_LANDIS)
                {
                    MessagePack.Message80004_74(ref nowMessage, ref nowEvent, duelWin);
                    NormalTapOK();
                }
                else if (GroundOne.enemyName1 == Database.DUEL_SINIKIA_KAHLHANZ)
                {
                    MessagePack.Message70012_2(ref nowMessage, ref nowEvent, duelWin);
                    NormalTapOK();
                }
                else if (GroundOne.enemyName1 == Database.VERZE_ARTIE)
                {
                    MessagePack.Message70013_2(ref nowMessage, ref nowEvent, duelWin);
                    NormalTapOK();
                }
                else if (GroundOne.enemyName1 == Database.ENEMY_LAST_RANA_AMILIA)
                {
                    if (duelWin)
                    {
                        MessagePack.Message30004(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message30003(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else // 通常のDUEL闘技場の対戦相手
                {
                    MessagePack.Message89998_2(ref nowMessage, ref nowEvent, opponentDuelist, duelWin, this.fromGoDungeon);
                    NormalTapOK();
                }
                return;
            }
            #endregion

            #region "死亡しているものは自動的に復活させます。"
            if (GroundOne.MC != null)
            {
                if (GroundOne.MC.Dead)
                {
                    GroundOne.MC.Dead = false;
                    GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife / 2;
                    MessagePack.HomeTownResurrect(ref nowMessage, ref nowEvent, GroundOne.MC);
                    NormalTapOK();
                }
            }
            if (GroundOne.SC != null)
            {
                if (GroundOne.SC.Dead)
                {
                    GroundOne.SC.Dead = false;
                    GroundOne.SC.CurrentLife = GroundOne.SC.MaxLife / 2;
                    MessagePack.HomeTownResurrect(ref nowMessage, ref nowEvent, GroundOne.SC);
                }
            }
            if (GroundOne.TC != null)
            {
                if (GroundOne.TC.Dead)
                {
                    GroundOne.TC.Dead = false;
                    GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife / 2;
                    MessagePack.HomeTownResurrect(ref nowMessage, ref nowEvent, GroundOne.TC);
                }
            }
            #endregion

            if (GroundOne.WE.AlreadyShownEvent == false)
        	{
        		GroundOne.WE.AlreadyShownEvent = true;
        	}
        	else
        	{
                mainMessage.text = "アイン：さて、何すっかな";

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                return; // イベント発生は連続して発生させない
            }

            #region "後編初日"
            if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationFirstHomeTown)
            {
                MessagePack.Message20100(ref nowMessage, ref nowEvent);
                tapOK();
            }
            #endregion
            #region "看板「始まりの地」を見たとき"
            else if (this.firstDay >= 1 &&
                GroundOne.WE.BoardInfo10 &&
                GroundOne.WE.Truth_CommunicationJoinPartyLana == false &&
                GroundOne.WE.Truth_CommunicationNotJoinLana == false &&
                GroundOne.WE.AvailableSecondCharacter == false)
            {
            	MessagePack.Message20101(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "看板３を見る前でも、大広間に到達した時"
            else if ((GroundOne.WE.dungeonEvent11KeyOpen || GroundOne.WE.dungeonEvent12KeyOpen || GroundOne.WE.dungeonEvent13KeyOpen || GroundOne.WE.dungeonEvent14KeyOpen) &&
                GroundOne.WE.Truth_CommunicationJoinPartyLana == false && GroundOne.WE.AvailableSecondCharacter == false)
            {
            	MessagePack.Message20102(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "看板「メンバー構成で変化」を見たとき"
            else if ((GroundOne.WE.BoardInfo13) && GroundOne.WE.Truth_CommunicationJoinPartyLana == false && GroundOne.WE.AvailableSecondCharacter == false)
            {
            	MessagePack.Message20103(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "看板「くまなく探せ」を見たとき"
            else if (this.firstDay >= 1 && GroundOne.WE.BoardInfo14 &&
                     GroundOne.WE.Truth_CommunicationJoinPartyLana == false && GroundOne.WE.AvailableSecondCharacter == false)
            {
                MessagePack.Message20104(ref nowMessage, ref nowEvent);
                tapOK();
            }
            #endregion
            #region "1日目のダンジョン帰還後、ラナをパーティに加える"
            else if (this.firstDay >= 1 &&
                GroundOne.WE.Truth_CommunicationJoinPartyLana == false &&
                GroundOne.WE.Truth_CommunicationNotJoinLana == false &&
                GroundOne.WE.AvailableSecondCharacter == false)
            {
                MessagePack.Message20104_2(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "１階看板最後の情報を入手したとき"
            else if (this.firstDay >= 1 && !GroundOne.WE.Truth_Communication_Dungeon11 && GroundOne.WE.dungeonEvent27)
            {
                MessagePack.Message20105(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "１階制覇"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.TruthCommunicationCompArea1)
            {
                MessagePack.Message20106(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階初日"
            else if (GroundOne.WE.TruthCompleteArea1 && GroundOne.WE.TruthCommunicationCompArea1 && !GroundOne.WE.Truth_CommunicationSecondHomeTown)
            {
                GroundOne.WE.Truth_CommunicationSecondHomeTown = true;
                HometownCommunicationStart();
            }
            #endregion
            #region "２階、地の部屋、選択失敗"
            else if (GroundOne.WE.dungeonEvent206 && !GroundOne.WE.dungeonEvent207 && GroundOne.WE.dungeonEvent207FailEvent2)
            {
                MessagePack.Message20201(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階、神の試練クリア後"
            else if (GroundOne.WE2.TruthAnswerSuccess && GroundOne.WE.dungeonEvent224 && !GroundOne.WE.dungeonEvent225)
            {
                MessagePack.Message20202(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階制覇"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.TruthCommunicationCompArea2)
            {
                MessagePack.Message20203(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階初日"
            else if (GroundOne.WE.TruthCompleteArea2 && GroundOne.WE.TruthCommunicationCompArea2 && !GroundOne.WE.Truth_CommunicationThirdHomeTown)
            {
                GroundOne.WE.Truth_CommunicationThirdHomeTown = true;
                HometownCommunicationStart();
            }
            #endregion
            #region "３階、エリア１の鏡をクリア時"
            else if (GroundOne.WE.TruthCompleteArea1 && GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.TruthCompleteArea3 && GroundOne.WE.dungeonEvent305 && !GroundOne.WE.dungeonEvent306)
            {
                MessagePack.Message20301(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階、鏡エリア２－１をクリアした時"
            else if (GroundOne.WE.dungeonEvent314 && !GroundOne.WE.dungeonEvent314_2)
            {
                MessagePack.Message20302(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階、鏡エリア２－２をクリアした時"
            else if (GroundOne.WE.dungeonEvent315 && !GroundOne.WE.dungeonEvent315_2)
            {
                MessagePack.Message20303(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階、鏡エリア２－３をクリアした時"
            else if (GroundOne.WE.dungeonEvent316 && !GroundOne.WE.dungeonEvent316_2)
            {
                MessagePack.Message20304(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階、鏡エリア２－４をクリアした時"
            else if (GroundOne.WE.dungeonEvent317 && !GroundOne.WE.dungeonEvent317_2)
            {
                MessagePack.Message20305(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階、鏡エリア２－５をクリアした時"
            else if (GroundOne.WE.dungeonEvent312 && !GroundOne.WE.dungeonEvent312_2)
            {
                MessagePack.Message20306(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階制覇"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.TruthCommunicationCompArea3)
            {
                MessagePack.Message20307(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階初日"
            else if (GroundOne.WE.TruthCompleteArea3 && GroundOne.WE.TruthCommunicationCompArea3 && !GroundOne.WE.Truth_CommunicationFourthHomeTown)
            {
                GroundOne.WE.Truth_CommunicationFourthHomeTown = true;
                HometownCommunicationStart();
            }
            #endregion
            #region "現実世界"
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent511 && !GroundOne.WE2.SeekerEvent601)
            {
                MessagePack.Message20600(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            // ダンジョンから帰還後、必須イベントが無ければ、以下任意イベント            
            #region "DUEL闘技場開催"
            else if (this.firstDay >= 3 && !GroundOne.WE.AvailableDuelColosseum)
            {
                MessagePack.Message29000(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "DUEL闘技場、DUEL開始"
            else if (this.firstDay >= 4 && !GroundOne.WE.AvailableDuelMatch)
            {
                MessagePack.Message29001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "ESCメニュー：バトル設定"
            else if (!GroundOne.WE.AvailableBattleSettingMenu && GroundOne.MC.Level >= 4)
            {
                MessagePack.Message29002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            else
            {
                Debug.Log("all else, then no event");
                MessagePack.HomeTownNoEvent(ref nowMessage, ref nowEvent);
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                NormalTapOK();
            }
        }
            	
	    public void tapDungeon() {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "アイン：ここからダンジョンスタートだ。ここから先は本編でプレイしてみてくれ。";
                return;
            }

            if (!GroundOne.WE2.RealWorld && GroundOne.WE.GameDay <= 1 && (!GroundOne.WE.AlreadyCommunicate || !GroundOne.WE.Truth_CommunicationGanz1 || !GroundOne.WE.Truth_CommunicationHanna1 || !GroundOne.WE.Truth_CommunicationLana1))
            {
                MessagePack.Message30000(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea1 && (!GroundOne.WE.Truth_CommunicationLana21 || !GroundOne.WE.Truth_CommunicationGanz21 || !GroundOne.WE.Truth_CommunicationHanna21 || !GroundOne.WE.Truth_CommunicationOl21))
            {
                MessagePack.Message30000(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea2 && (!GroundOne.WE.Truth_CommunicationLana31 || !GroundOne.WE.Truth_CommunicationGanz31 || !GroundOne.WE.Truth_CommunicationHanna31 || !GroundOne.WE.AvailableItemBank || !GroundOne.WE.Truth_CommunicationOl31 || !GroundOne.WE.Truth_CommunicationSinikia31))
            {
                MessagePack.Message30000(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea3 && (!GroundOne.WE.Truth_CommunicationLana41 || !GroundOne.WE.Truth_CommunicationGanz41 || !GroundOne.WE.Truth_CommunicationHanna41 || !GroundOne.WE.Truth_CommunicationOl41 || !GroundOne.WE.Truth_CommunicationSinikia41))
            {
                MessagePack.Message30000(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (GroundOne.WE2.RealWorld && (!GroundOne.WE2.SeekerEvent602 || !GroundOne.WE2.SeekerEvent603 || !GroundOne.WE2.SeekerEvent604))
            {
                MessagePack.Message30000(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (!GroundOne.WE.AlreadyRest)
            {
                MessagePack.Message30001(ref nowMessage, ref nowEvent);
                tapOK();
            }
            else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent602 && GroundOne.WE2.SeekerEvent603 && GroundOne.WE2.SeekerEvent604 && !GroundOne.WE2.SeekerEvent605)
            {
                MessagePack.Message30002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else
            {
                if (GroundOne.WE2.RealWorld)
                {
                    if (GroundOne.WE2.SeekerEvent605 && !GroundOne.WE2.SeekerEvent706)
                    {
                        GroundOne.WE.DungeonArea = 1;
                        GroundOne.WE2.RealDungeonArea = 1;
                        Method.AutoSaveTruthWorldEnvironment();
                        Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                        SceneDimension.JumpToTruthDungeon(false); 
                    }
                    else if (GroundOne.WE2.SeekerEvent706 && !GroundOne.WE2.SeekerEvent822)
                    {
                        GroundOne.WE.DungeonArea = 2;
                        GroundOne.WE2.RealDungeonArea = 2;
                        Method.AutoSaveTruthWorldEnvironment();
                        Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                        SceneDimension.JumpToTruthDungeon(false);
                    }
                    else if (GroundOne.WE2.SeekerEvent822 && !GroundOne.WE2.SeekerEvent925)
                    {
                        GroundOne.WE.DungeonArea = 3;
                        GroundOne.WE2.RealDungeonArea = 3;
                        Method.AutoSaveTruthWorldEnvironment();
                        Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                        SceneDimension.JumpToTruthDungeon(false);
                    }
                    else if (GroundOne.WE2.SeekerEvent925 && !GroundOne.WE2.SeekerEvent1012)
                    {
                        GroundOne.WE.DungeonArea = 4;
                        GroundOne.WE2.RealDungeonArea = 4;
                        Method.AutoSaveTruthWorldEnvironment();
                        Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                        SceneDimension.JumpToTruthDungeon(false);
                    }
                    else
                    {
                        GroundOne.WE.DungeonArea = 5;
                        GroundOne.WE2.RealDungeonArea = 5;
                        Method.AutoSaveTruthWorldEnvironment();
                        Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                        SceneDimension.JumpToTruthDungeon(false);
                    }
                }
                else
                {
                    GroundOne.OpponentDuelist = WhoisDuelPlayer();
                    if (GroundOne.OpponentDuelist != string.Empty)
                    {
                        DuelSupportMessage(SupportType.FromDungeonGate, GroundOne.OpponentDuelist);

                        CallDuel(true);
                    }
                    else if (GroundOne.WE.TruthCompleteArea1 == false)
                    {
                        CallDungeon(1);
                    }
                    else
                    {
                        mainMessage.text = "アイン：さて、何階から始めるかな。";
                        selectDungeon[0].gameObject.SetActive(true);
                        selectDungeon[1].gameObject.SetActive(GroundOne.WE.TruthCompleteArea1);
                        selectDungeon[2].gameObject.SetActive(GroundOne.WE.TruthCompleteArea2);
                        selectDungeon[3].gameObject.SetActive(GroundOne.WE.TruthCompleteArea3);
                        selectDungeon[4].gameObject.SetActive(GroundOne.WE.TruthCompleteArea4);

                        groupSelectDungeon.SetActive(true);
                        panelHide.gameObject.SetActive(true);
                    }
                }
            }
	    }

        public void CallDungeon(int targetDungeon)
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_DUNGEON_GO, String.Empty, String.Empty);
            if (targetDungeon == 1)
            {
                GroundOne.WE.DungeonArea = 1;
                if (!GroundOne.WE.TruthCompleteArea1)
                {
                    mainMessage.text = "アイン：さて、１階を突破するぜ！";
                }
                else
                {
                    mainMessage.text = "アイン：もう１度、１階でも探索するか。";
                }
            }
            else if (targetDungeon == 2)
            {
                GroundOne.WE.DungeonArea = 2;
                if (!GroundOne.WE.CompleteArea2)
                {
                    mainMessage.text = "アイン：目指すは２階を制覇だな！";
                }
                else
                {
                    mainMessage.text = "アイン：もう１度、２階でも探索するか。";
                }
            }
            else if (targetDungeon == 3)
            {
                GroundOne.WE.DungeonArea = 3;
                if (!GroundOne.WE.CompleteArea3)
                {
                    mainMessage.text = "アイン：いよいよ３階、気を引き締めていくぜ！";
                }
                else
                {
                    mainMessage.text = "アイン：もう１度、３階でも探索するか。";
                }
            }
            else if (targetDungeon == 4)
            {
                GroundOne.WE.DungeonArea = 4;
                if (!GroundOne.WE.CompleteArea4)
                {
                    mainMessage.text = "アイン：４階制覇やってみせるぜ！";
                }
                else
                {
                    mainMessage.text = "アイン：もう１度、４階でも探索するか。";
                }
            }
            else if (targetDungeon == 5)
            {
                GroundOne.WE.DungeonArea = 5;
                if (!GroundOne.WE.CompleteArea5)
                {
                    mainMessage.text = "アイン：最下層制覇、やってみせる！";
                }
                else
                {
                    mainMessage.text = "アイン：もう１度、５階でも探索するか。";
                }
            }

            #region "ラナ、ガンツ、ハンナの一般会話完了はここで反映します。"
            //if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationLana1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana1 = true;
            //else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationLana2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana2 = true;
            //else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationLana3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana3 = true;
            //else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationLana4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana4 = true;
            //else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationLana5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana5 = true;
            //else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationLana6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana6 = true;
            //else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationLana7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana7 = true;
            //else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationLana8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana8 = true;
            //else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationLana9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana9 = true;
            //else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationLana10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana10 = true;

            //if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationHanna1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna1 = true;
            //else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationHanna2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna2 = true;
            //else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationHanna3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna3 = true;
            //else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationHanna4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna4 = true;
            //else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationHanna5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna5 = true;
            //else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationHanna6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna6 = true;
            //else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationHanna7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna7 = true;
            //else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationHanna8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna8 = true;
            //else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationHanna9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna9 = true;
            //else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationHanna10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna10 = true;

            //if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationGanz1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz1 = true;
            //else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationGanz2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz2 = true;
            //else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationGanz3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz3 = true;
            //else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationGanz4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz4 = true;
            //else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationGanz5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz5 = true;
            //else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationGanz6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz6 = true;
            //else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationGanz7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz7 = true;
            //else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationGanz8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz8 = true;
            //else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationGanz9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz9 = true;
            //else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationGanz10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz10 = true;
            #endregion

            SceneDimension.JumpToTruthDungeon(false);
        }

        public void tapCommunicationLana()
        {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ラナ：本編では気が向いたら、私に声をかけてね♪";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_TALK_LANA, String.Empty, String.Empty);
            if (GroundOne.WE.AlreadyCommunicate)
            {
                if (!GroundOne.WE.AlreadyRest)
                {
                    mainMessage.text = MessageFormatForLana(1001);
                }
                else
                {
                    mainMessage.text = MessageFormatForLana(1002);
                }
                return;
            }

            // todo ラナ・複合魔法・スキルの基礎習得
            #region "１日目"
            if (!GroundOne.WE.Truth_CommunicationLana1)
            {
                MessagePack.Message40000(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話１"
            else if (!GroundOne.WE.Truth_CommunicationLana2)
            {
                MessagePack.Message41001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話２"
            else if (!GroundOne.WE.Truth_CommunicationLana3)
            {
                MessagePack.Message41002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "ラナの薬品店完成"
            else if (!GroundOne.WE.Truth_CommunicationLana4)
            {
                MessagePack.Message40002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "戦闘：インスタントアクション"
            else if (!GroundOne.WE.Truth_CommunicationLana5)
            {
                MessagePack.Message29003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "スタンスの習得会話"
            else if (!GroundOne.WE.Truth_CommunicationLana6)
            {
                this.nowTalkingLanaAmiria = true;
                MessagePack.Message40003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話３"
            else if (!GroundOne.WE.Truth_CommunicationLana7)
            {
                MessagePack.Message41003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話４"
            else if (!GroundOne.WE.Truth_CommunicationLana8)
            {
                MessagePack.Message41004(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話５"
            else if (!GroundOne.WE.Truth_CommunicationLana9)
            {
                MessagePack.Message41005(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話６"
            else if (!GroundOne.WE.Truth_CommunicationLana10)
            {
                MessagePack.Message41006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "日常会話７"
            else if (!GroundOne.WE.Truth_CommunicationLana11)
            {
                MessagePack.Message41007(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                MessagePack.Message40004(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "絡みつくフランシスに遭遇済みの場合"
            else if (false) // after !we.Truth_CommunicationLana1_2 && Truth_KnownTileInfo[252] == true && !we.TruthCompleteSlayBoss1
            {
                MessagePack.Message40005(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationLana21)
            {
                MessagePack.Message40006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "複合魔法習得"
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 20 && GroundOne.MC.FlashBlaze && !GroundOne.WE.Truth_CommunicationLana22)
            {
                MessagePack.Message40010_1(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 21 && !GroundOne.SC.BlueBullet)
            {
                MessagePack.Message40010_2(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 22 && !GroundOne.SC.VanishWave)
            {
                MessagePack.Message40010_3(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 23 && !GroundOne.SC.DarkenField)
            {
                MessagePack.Message40010_4(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 27 && !GroundOne.SC.FutureVision)
            {
                MessagePack.Message40010_5(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 28 && !GroundOne.SC.Recover)
            {
                MessagePack.Message40010_6(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 29 && !GroundOne.SC.TrustSilence)
            {
                MessagePack.Message40010_7(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 30 && !GroundOne.SC.SkyShield)
            {
                MessagePack.Message40010_8(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 31 && !GroundOne.SC.StarLightning)
            {
                MessagePack.Message40010_9(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 32 && !GroundOne.SC.PsychicTrance)
            {
                MessagePack.Message40010_10(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 33 && !GroundOne.SC.PsychicWave)
            {
                this.nowTalkingLanaAmiria = true;
                MessagePack.Message40010_11(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 34 && !GroundOne.SC.SharpGlare)
            {
                MessagePack.Message40010_12(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE.AvailableMixSpellSkill && GroundOne.SC.Level >= 35 && !GroundOne.SC.StanceOfSuddenness)
            {
                MessagePack.Message40010_13(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationLana31)
            {
                MessagePack.Message40007(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationLana41)
            {
                MessagePack.Message40008(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "現実世界"
            else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent602)
            {
                MessagePack.Message40009(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "イベントが特に無い場合"
            else
            {
                MessagePack.Message49999(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            GroundOne.WE.AlreadyCommunicate = true;
        }

	    public void tapDuel() {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ランディス：気安くDUELボタンを押してんじゃねぇ、本編で遭遇したらブッ飛ばすからな。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_DUEL_ENTRANCE, String.Empty, String.Empty);
            GroundOne.OpponentDuelist = WhoisDuelPlayer();
            #region "Duel申請中"
            if (!GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                MessagePack.Message80001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディス遭遇"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                MessagePack.Message80002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationOl21)
            {
                MessagePack.Message80003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディスを仲間にするところ"
            else if (GroundOne.WE.dungeonEvent226 && !GroundOne.WE.Truth_CommunicationOl22)
            {
                this.nowTalkingOlRandis = true;
                MessagePack.Message80004(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationOl31)
            {
                MessagePack.Message80005(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationOl41)
            {
                MessagePack.Message80006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "条件に応じて、Duelを実施します。"
            else if (GroundOne.OpponentDuelist != string.Empty && GroundOne.WE.AlreadyRest)
            {
                GroundOne.WE.AlreadyDuelComplete = true;
                DuelSupportMessage(SupportType.FromDuelGate, GroundOne.OpponentDuelist);
                CallDuel(false);
            }
            #endregion
            #region "その他"
            else
            {
                MessagePack.Message89999(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
        }

        private void LoadDuelColosseum()
        {
            DuelResultText.text = "戦歴　" + GroundOne.WE2.DuelWin.ToString() + "  勝" + GroundOne.WE2.DuelLose.ToString() + "  敗";

            if (GroundOne.WE2.DuelWin >= 21)
            {
                honorText.text = Database.TITLE_HONOR_7;
            }
            else if (GroundOne.WE2.DuelWin >= 20)
            {
                honorText.text = Database.TITLE_HONOR_6;
            }
            else if (GroundOne.WE2.DuelWin >= 16)
            {
                honorText.text = Database.TITLE_HONOR_5;
            }
            else if (GroundOne.WE2.DuelWin >= 12)
            {
                honorText.text = Database.TITLE_HONOR_4;
            }
            else if (GroundOne.WE2.DuelWin >= 8)
            {
                honorText.text = Database.TITLE_HONOR_3;
            }
            else if (GroundOne.WE2.DuelWin >= 4)
            {
                honorText.text = Database.TITLE_HONOR_2;
            }
            else
            {
                honorText.text = Database.TITLE_HONOR_1;
            }

            DuelMessageText.text = "DUEL闘技場へようこそ。\n";
            if (WhoisDuelPlayerCheck() != string.Empty)
            {
                DuelMessageText.text += "アイン様の次の対戦相手は" + WhoisDuelPlayerCheck() + "を予定しております。";
            }
            else
            {
                DuelMessageText.text += "アイン様の次の対戦相手は現在設定されていません。";
            }
        }

        // 対戦相手のステータスを確認
        public void tapOpponentInfo()
        {
            this.Filter.SetActive(true);

            string duelPlayerName = WhoisDuelPlayerCheck();
            // mc.Level < XX を撤廃して、レベル超えていても戦えるようにした。
            // 7, 10, 13, 16, 19, 20, 23, 26, 29, 32, 35, 38, 41, 44, 47, 50, 52, 54, 56, 58, 60
            // 階層毎にDUEL相手を制御する処理も撤廃した。
            // && !we.TruthCompleteArea1
            Debug.Log("duelPlayerName: " + duelPlayerName);
            SceneDimension.CallTruthDuelPlayerStatus(this, duelPlayerName);
        }

        public void tapCheckDuelRule()
        {
            this.Filter.SetActive(true);
            SceneDimension.CallTruthDuelRule(this);
        }

        public void tapDuelClose()
        {
            HideAllChild();
        }

        public void tapBattleSetting()
        {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ラナ：バトルコマンドを設定する事が出来るわ。詳しくは別のチュートリアルで確認してね。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLE_SETTING, "FromHomeTown", String.Empty);
            SceneDimension.CallTruthBattleSetting(this);
        }

        public void tapSystemMessage()
        {
            if (this.nowReading <= 0 && this.nowMessage.Count <= 0)
            {
                HideAllChild();
            }
            else
            {
                tapOK();
            }
        }

        private void NormalTapOK()
        {
            this.nowHideing = true;
            tapOK();
        }
        public void tapOK()
        {
            bool ForceSkipTapOK = false;

            // autosave->exit専用
            if (this.systemMessage.text == this.MESSAGE_AUTOSAVE_EXIT)
            {
                SceneDimension.JumpToTitle();
                return;
            }

            if (this.nowReading < this.nowMessage.Count)
            {
                if (this.panelHide.isActiveAndEnabled == false && this.nowHideing)
                {
                    this.panelHide.gameObject.SetActive(true);
                }
                this.btnOK.enabled = true;
                this.btnOK.gameObject.SetActive(true);

                MessagePack.ActionEvent current = this.nowEvent[this.nowReading];
                // メッセージ反映
                if (current == MessagePack.ActionEvent.HomeTownMessageDisplay)
                {
                    systemMessage.text = this.nowMessage[this.nowReading];
                    systemMessagePanel.SetActive(true);

                    // メッセージの内容に応じてイベントを適宜こなす
                    if (this.nowMessage[this.nowReading] == Database.Message_DuelAvailable)
                    {
                        buttonDuel.gameObject.SetActive(true);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_BattleSettingAvailable)
                    {
                        GroundOne.WE.AvailableBattleSettingMenu = true;
                        buttonBattleSetting.gameObject.SetActive(true);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_GoToAnotherField)
                    {
                        panelObjective.SetActive(false);
                        buttonHanna.gameObject.SetActive(false);
                        buttonDungeon.gameObject.SetActive(false);
                        buttonRana.gameObject.SetActive(false);
                        buttonGanz.gameObject.SetActive(false);
                        buttonPotion.gameObject.SetActive(false);
                        buttonDuel.gameObject.SetActive(false);
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_GoToAnotherField_Back)
                    {
                        if (!GroundOne.WE.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        panelObjective.SetActive(true);
                        buttonHanna.gameObject.SetActive(true);
                        buttonDungeon.gameObject.SetActive(true);
                        buttonRana.gameObject.SetActive(true);
                        buttonGanz.gameObject.SetActive(true);
                        buttonPotion.gameObject.SetActive(true);
                        buttonDuel.gameObject.SetActive(true);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_GateAvailable)
                    {
                        buttonShinikia.gameObject.SetActive(true);
                        GroundOne.WE.AvailableBackGate = true;
                        GroundOne.WE.alreadyCommunicateCahlhanz = true; // カール爵に教えてもらったばかりのため、Trueを指定しておく。
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_PotionShopAvailable)
                    {
                        GroundOne.WE.AvailablePotionshop = true;
                        buttonPotion.gameObject.SetActive(true);
                    }
                }
                else if (current == MessagePack.ActionEvent.HomeTownYesNoMessageDisplay)
                {
                    yesnoSystemMessage.text = this.nowMessage[this.nowReading];
                    groupYesnoSystemMessage.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallDuel)
                {
                    systemMessagePanel.SetActive(false);
                    systemMessage.text = "";
                    BattleStart(this.nowMessage[this.nowReading]);
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpell ||
                         current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpellSC)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallDecision ||
                         current == MessagePack.ActionEvent.HomeTownCallDecision3)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.HomeTownGetItemFullCheck ||
                         current == MessagePack.ActionEvent.HomeTownRemoveItem)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.HomeTownAddNewCharacter)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.PlaySound)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.ObjectiveAdd)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.ObjectiveRemove)
                {
                    mainMessage.text = "";
                }
                else
                {
                    systemMessagePanel.SetActive(false);
                    systemMessage.text = "";
                    mainMessage.text = "   " + this.nowMessage[this.nowReading];
                    GroundOne.playbackMessage.Add(this.nowMessage[this.nowReading]);
                }

                // 各イベント固有の処理
                if (current == MessagePack.ActionEvent.HomeTownGetItemFullCheck)
                {
                    Method.GetItemFullCheck(this, GroundOne.MC, this.nowMessage[this.nowReading]);
                    this.nowMessage[this.nowReading] = "";
                }
                else if (current == MessagePack.ActionEvent.HomeTownRemoveItem)
                {
                    FindAndDeleteItem(Database.EPIC_OLD_TREE_MIKI_DANPEN);
                }
                else if (current == MessagePack.ActionEvent.HomeTownBlackOut)
                {
                    BlackOut();
                }
                else if (current == MessagePack.ActionEvent.HomeTownTurnToNormal)
                {
                    TurnToNormal();
                }
                else if (current == MessagePack.ActionEvent.HomeTownBackToTown)
                {
                    BackToTown();
                    ButtonVisibleControl(true);
                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.HomeTownButtonVisibleControl)
                {
                    ButtonVisibleControl(true);
                }
                else if (current == MessagePack.ActionEvent.StopMusic)
                {
                    GroundOne.StopDungeonMusic();
                }
                else if (current == MessagePack.ActionEvent.HomeTownMorning)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.imgBackground.sprite = Resources.Load<Sprite>(Database.BACKGROUND_MORNING);
                }
                else if (current == MessagePack.ActionEvent.HomeTownNight)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);
                    this.imgBackground.sprite = Resources.Load<Sprite>(Database.BACKGROUND_NIGHT);
                }
                else if (current == MessagePack.ActionEvent.HomeTownDuelSelect)
                {
                    LoadDuelColosseum();
                    this.groupDuelSelect.SetActive(true);
                    this.Filter.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowDuelRule)
                {
                    SceneDimension.CallDuelRule(this);
                    this.Filter.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallDecision)
                {
                    this.Filter.SetActive(true);
                    SceneDimension.CallTruthDecision(this);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallDecision3)
                {
                    this.Filter.SetActive(true);
                    SceneDimension.CallTruthDecision3(this);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallPotionShop)
                {
                    SceneDimension.CallPotionShop(this);
                }
                else if (current == MessagePack.ActionEvent.HomeTownFazilCastle)
                {
                    GoToFazilCastle();
                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.HomeTownFazilCastleMenu)
                {
                    this.groupSelectCastleMenu.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownTicketChoice)
                {
                    this.groupTicketChoice.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownGoToKahlhanz)
                {
                    GoToKahlhanz();
                }
                else if (current == MessagePack.ActionEvent.HomeTownGotoFirstPlace)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FIELD_OF_FIRSTPLACE);
                }
                else if (current == MessagePack.ActionEvent.HomeTownExecRestInn)
                {
                    ExecRestInn();
                }
                else if (current == MessagePack.ActionEvent.HomeTownExecItemBank)
                {
                    ExecItemBank();
                }
                else if (current == MessagePack.ActionEvent.CallSomeMessageWithAnimation)
                {
                    systemMessagePanel.gameObject.SetActive(true);
                    systemMessage.gameObject.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownButtonHidden)
                {
                    buttonDungeon.gameObject.SetActive(false);
                    buttonGanz.gameObject.SetActive(false);
                    buttonShinikia.gameObject.SetActive(false);
                    buttonHanna.gameObject.SetActive(false);
                    buttonRana.gameObject.SetActive(false);
                    buttonPotion.gameObject.SetActive(false);
                    buttonDuel.gameObject.SetActive(false);
                    dayLabel.gameObject.SetActive(false);
                    panelObjective.SetActive(false);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallSaveLoad)
                {
                    this.forceSaveCall = true;
                    SceneDimension.CallSaveLoad(this, true, false);
                }
                else if (current == MessagePack.ActionEvent.HomeTownAddNewCharacter)
                {
                    GroundOne.WE.AvailableThirdCharacter = true;
                    GroundOne.TC.FullName = "ヴェルゼ・アーティ";
                    GroundOne.TC.FirstName = "ヴェルゼ";
                    GroundOne.TC.Strength = Database.VERZE_ARTIE_SECOND_STRENGTH;
                    GroundOne.TC.Agility = Database.VERZE_ARTIE_SECOND_AGILITY;
                    GroundOne.TC.Intelligence = Database.VERZE_ARTIE_SECOND_INTELLIGENCE;
                    GroundOne.TC.Stamina = Database.VERZE_ARTIE_SECOND_STAMINA;
                    GroundOne.TC.Mind = Database.VERZE_ARTIE_SECOND_MIND;
                    GroundOne.TC.Level = 0;
                    GroundOne.TC.Exp = 0;
                    for (int ii = 0; ii < 35; ii++)
                    {
                        GroundOne.TC.BaseLife += GroundOne.TC.LevelUpLifeTruth;
                        GroundOne.TC.BaseMana += GroundOne.TC.LevelUpManaTruth;
                        GroundOne.TC.Level++;
                    }
                    GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife;
                    GroundOne.TC.BaseSkillPoint = 100;
                    GroundOne.TC.CurrentSkillPoint = 100;
                    GroundOne.TC.CurrentMana = GroundOne.TC.MaxMana;
                    GroundOne.TC.MainWeapon = new ItemBackPack(Database.RARE_WHITE_SILVER_SWORD_REPLICA);
                    GroundOne.TC.SubWeapon = null;
                    GroundOne.TC.MainArmor = new ItemBackPack(Database.RARE_BLACK_AERIAL_ARMOR_REPLICA);
                    GroundOne.TC.Accessory = new ItemBackPack(Database.RARE_HEAVENLY_SKY_WING_REPLICA);
                    GroundOne.TC.Accessory2 = null;
                    GroundOne.TC.BattleActionCommandList[0] = Database.NEUTRAL_SMASH;
                    GroundOne.TC.BattleActionCommandList[1] = Database.INNER_INSPIRATION;
                    GroundOne.TC.BattleActionCommandList[2] = Database.MIRROR_IMAGE;
                    GroundOne.TC.BattleActionCommandList[3] = Database.DEFLECTION;
                    GroundOne.TC.BattleActionCommandList[4] = Database.STANCE_OF_FLOW;
                    GroundOne.TC.BattleActionCommandList[5] = Database.GALE_WIND;
                    GroundOne.TC.BattleActionCommandList[6] = Database.STRAIGHT_SMASH;
                    GroundOne.TC.BattleActionCommandList[7] = Database.SURPRISE_ATTACK;
                    GroundOne.TC.BattleActionCommandList[8] = Database.NEGATE;
                    GroundOne.TC.AvailableMana = true;
                    GroundOne.TC.AvailableSkill = true;

                    GroundOne.TC.FireBall = true;
                    GroundOne.TC.StraightSmash = true;
                    GroundOne.TC.CounterAttack = true;
                    GroundOne.TC.FreshHeal = true;
                    GroundOne.TC.StanceOfFlow = true;
                    GroundOne.TC.DispelMagic = true;
                    GroundOne.TC.WordOfPower = true;
                    GroundOne.TC.EnigmaSence = true;
                    GroundOne.TC.BlackContract = true;
                    GroundOne.TC.Cleansing = true;
                    GroundOne.TC.GaleWind = true;
                    GroundOne.TC.Deflection = true;
                    GroundOne.TC.Negate = true;
                    GroundOne.TC.InnerInspiration = true;
                    GroundOne.TC.FrozenLance = true;
                    GroundOne.TC.Tranquility = true;
                    GroundOne.TC.WordOfFortune = true;
                    GroundOne.TC.SkyShield = true;
                    GroundOne.TC.NeutralSmash = true;
                    GroundOne.TC.Glory = true;
                    GroundOne.TC.BlackFire = true;
                    GroundOne.TC.SurpriseAttack = true;
                    GroundOne.TC.MirrorImage = true;
                    GroundOne.TC.WordOfMalice = true;
                    GroundOne.TC.StanceOfSuddenness = true;
                    GroundOne.TC.CrushingBlow = true;
                    GroundOne.TC.Immolate = true;
                    GroundOne.TC.AetherDrive = true;
                    GroundOne.TC.TrustSilence = true;
                    GroundOne.TC.WordOfAttitude = true;
                    GroundOne.TC.OneImmunity = true;
                    GroundOne.TC.AntiStun = true;
                    GroundOne.TC.FutureVision = true;
                }
                else if (current == MessagePack.ActionEvent.DungeonSeekerEvent601)
                {
                    GroundOne.WE2.SeekerEvent601 = true;
                    GroundOne.WE.AlreadyRest = true; // 朝起きたときからスタートとする。
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                }
                else if (current == MessagePack.ActionEvent.DungeonSeekerEvent602)
                {
                    GroundOne.WE2.SeekerEvent602 = true;
                    GroundOne.WE.AlreadyCommunicate = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                }
                else if (current == MessagePack.ActionEvent.DungeonSeekerEvent603)
                {
                    GroundOne.WE2.SeekerEvent603 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                }
                else if (current == MessagePack.ActionEvent.DungeonSeekerEvent604)
                {
                    GroundOne.WE2.SeekerEvent604 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                }
                else if (current == MessagePack.ActionEvent.DungeonSeekerEvent605)
                {
                    GroundOne.WE2.SeekerEvent605 = true;
                    GroundOne.WE.DungeonArea = 1;
                    GroundOne.WE2.RealDungeonArea = 1;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld();
                    SceneDimension.JumpToTruthDungeon(false);
                }
                else if (current == MessagePack.ActionEvent.HomeTownYesNoMessageDisplay)
                {
                    this.yesnoSystemMessage.text = this.nowMessage[this.nowReading];
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpell)
                {
                    ShowActiveSkillSpell(GroundOne.MC, this.nowMessage[this.nowReading]);
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpellSC)
                {
                    ShowActiveSkillSpell(GroundOne.SC, this.nowMessage[this.nowReading]);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic01)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic02)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic03)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic04)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic05)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM05, Database.BGM05LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic06)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM06, Database.BGM06LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic07)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM07, Database.BGM07LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic08)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM08, Database.BGM08LoopBegin);
                }
                //else if (current == MessagePack.ActionEvent.PlayMusic09)
                //{
                //    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);
                //}
                else if (current == MessagePack.ActionEvent.PlayMusic10)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic11)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic12)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic13)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic14)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic15)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic16)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic17)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM21, Database.BGM21LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic18)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM18, Database.BGM18LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic19)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM19, Database.BGM19LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlaySound)
                {
                    GroundOne.PlaySoundEffect(this.nowMessage[this.nowReading]);
                    ForceSkipTapOK = true;
                }
                else if (current == MessagePack.ActionEvent.ObjectiveAdd)
                {
                    systemMessage.text = "クエスト追加\r\n" + this.nowMessage[this.nowReading];
                    systemMessagePanel.SetActive(true);
                    GroundOne.PlaySoundEffect(Database.SOUND_OBJECTIVE_ADD);
                }
                else if (current == MessagePack.ActionEvent.ObjectiveRemove)
                {
                    systemMessage.text = this.nowMessage[this.nowReading] + "を完了しました！\r\n";
                    int exp = TruthObjective.GetObjectiveExp(nowMessage[nowReading]);
                    systemMessage.text += exp + "の経験値を獲得";
                    systemMessagePanel.SetActive(true);
                    if (GroundOne.MC != null && GroundOne.MC.Level < Method.GetMaxLevel())
                    {
                        GroundOne.MC.Exp += exp;
                    }
                    if (GroundOne.SC != null && GroundOne.SC.Level < Method.GetMaxLevel() && GroundOne.WE.AvailableSecondCharacter)
                    {
                        GroundOne.SC.Exp += exp;
                    }
                    if (GroundOne.TC != null && GroundOne.TC.Level < Method.GetMaxLevel() && GroundOne.WE.AvailableThirdCharacter)
                    {
                        GroundOne.TC.Exp += exp;
                    }
                    GroundOne.PlaySoundEffect(Database.SOUND_OBJECTIVE_COMP);
                }
                else if (current == MessagePack.ActionEvent.Ending)
                {
                    StartEnding();
                    return;
                }

                this.nowReading++;
                if (this.nowMessage[this.nowReading - 1] == "" || ForceSkipTapOK)
                {
                    tapOK();
                }
            }

            if (this.nowReading >= this.nowMessage.Count)
            {
                List<string> removeList = TruthObjective.RefreshObjectList();
                if (removeList.Count > 0)
                {
                    for (int ii = 0; ii < removeList.Count; ii++)
                    {
                        this.nowMessage.Add(removeList[ii]); this.nowEvent.Add(MessagePack.ActionEvent.ObjectiveRemove);
                    }

                    UpdateTxtObjective();
                    return;
                }
                List<string> list = TruthObjective.GetObjectiveList();
                // 新しく追加された文字列があれば、システムメッセージで表記する。
                if (list.Count > 0)
                {
                    for (int ii = 0; ii < list.Count; ii++)
                    {
                        this.nowMessage.Add(list[ii]); this.nowEvent.Add(MessagePack.ActionEvent.ObjectiveAdd);
                    }

                    UpdateTxtObjective();
                    return;
                }

                this.nowReading = 0;
                this.nowMessage.Clear();
                this.nowEvent.Clear();

                this.nowHideing = false;
                this.panelHide.gameObject.SetActive(false);
                this.btnOK.enabled = false;
                this.btnOK.gameObject.SetActive(false);

                UpdateTxtObjective();
            }
        }

        private void UpdateTxtObjective()
        {
            for (int ii = 0; ii < txtObjective.Count; ii++)
            {
                txtObjective[ii].text = String.Empty;
            }

            for (int ii = 0; ii < GroundOne.ObjectiveList.Count; ii++)
            {
                txtObjective[ii].text = GroundOne.ObjectiveList[ii];
            }
        }

        private void CallDuel(bool fromGoDungeon)
        {
            this.fromGoDungeon = fromGoDungeon;
            MessagePack.Message89998(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist);
            //NormalTapOK(); // ここでは不要
        }

        private void BattleStart(string playerName)
        {
            Method.CreateShadowData();

            GroundOne.enemyName1 = playerName;
            GroundOne.enemyName2 = string.Empty;
            GroundOne.enemyName3 = string.Empty;
            if (playerName == Database.ENEMY_LAST_RANA_AMILIA)
            {
                SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, true, true, false, false);
            }
            else
            {
                SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, true, false, false, false);
            }
        }

        private void BlackOut()
        {
            GroundOne.StopDungeonMusic();

            cam.backgroundColor = Color.black;
            groupMenu.gameObject.SetActive(false);
            buttonHanna.gameObject.SetActive(false);
            buttonDungeon.gameObject.SetActive(false);
            buttonRana.gameObject.SetActive(false);
            buttonGanz.gameObject.SetActive(false);
            buttonPotion.gameObject.SetActive(false);
            buttonShinikia.gameObject.SetActive(false);
            buttonDuel.gameObject.SetActive(false);
            dayLabel.gameObject.SetActive(false);
            panelObjective.SetActive(false);
            panelSubMenu.SetActive(false);
            this.imgBackground.gameObject.SetActive(false);
        }

        private void TurnToNormal()
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            } 
            cam.backgroundColor = Color.white;
            groupMenu.gameObject.SetActive(true);
            panelObjective.SetActive(true);
            buttonHanna.gameObject.SetActive(true);
            buttonDungeon.gameObject.SetActive(true);
            buttonRana.gameObject.SetActive(true);
            buttonGanz.gameObject.SetActive(true);
            bool potionVisible = (GroundOne.WE.AvailablePotionshop && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonPotion.gameObject.SetActive(potionVisible);

            bool shinikiaVisible = (GroundOne.WE.AvailableBackGate && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonShinikia.gameObject.SetActive(shinikiaVisible);

            bool duelVisible = (GroundOne.WE.AvailableDuelColosseum && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511);
            this.buttonDuel.gameObject.SetActive(duelVisible);

            dayLabel.gameObject.SetActive(true);
            panelSubMenu.SetActive(true);
            this.imgBackground.gameObject.SetActive(true);
        }

	    public void tapShop() {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ガンツ：準備中の札がお前さんには見えんのか。本編になってからまた来なさい。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_EQUIP_SHOP, String.Empty, String.Empty);
            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailableEquipShop2 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailableEquipShop3 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailableEquipShop4 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailableEquipShop5 = true; // 前編で既に周知のため、解説は不要。

            if (false)
            {
            }
            #region "１日目"
            else if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationGanz1)
            {
                MessagePack.Message50000(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                MessagePack.Message50001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationGanz21)
            {
                MessagePack.Message50002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "複合魔法・スキルを教えてもらうイベント"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.AvailableMixSpellSkill && GroundOne.MC.Level >= 21)
            {
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    GroundOne.WE.AlreadyEquipShop = true;

                    MessagePack.Message50003(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    SceneDimension.CallTruthEquipmentShop(this);
                    mainMessage.text = "";
                }
            }
            else if (GroundOne.WE.TruthCompleteArea1 && GroundOne.WE.AvailableMixSpellSkill && !GroundOne.MC.FlashBlaze)
            {
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    GroundOne.WE.AlreadyEquipShop = true;

                    MessagePack.Message50004(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    SceneDimension.CallTruthEquipmentShop(this);
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationGanz31)
            {
                GroundOne.WE.Truth_CommunicationGanz31 = true;

                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    GroundOne.WE.AlreadyEquipShop = true;

                    MessagePack.Message50005(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    SceneDimension.CallTruthEquipmentShop(this);
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "古代栄樹の幹の断片を所持し、売却する"
            else if (!GroundOne.WE.Truth_CommunicationGanz32 && FindItemInAllMember(Database.EPIC_OLD_TREE_MIKI_DANPEN))
            {
                MessagePack.Message50006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationGanz41)
            {
                MessagePack.Message50007(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "現実世界"
            else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent604)
            {
                MessagePack.Message50008(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            //else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent604)
            //{
            //    MessagePack.Message50009(ref nowMessage, ref nowEvent);
            //    NormalTapOK();
            //}
            #endregion
            #region "その他"
            else
            {
                SceneDimension.CallTruthEquipmentShop(this);
                mainMessage.text = "";
            }
            #endregion
	    }

        private bool FindItemInAllMember(string itemName)
        {
            bool detectFind = false;
            if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null && GroundOne.MC.FindBackPackItem(itemName))
            {
                detectFind = true;
            }
            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && GroundOne.SC.FindBackPackItem(itemName))
            {
                detectFind = true;
            }
            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && GroundOne.TC.FindBackPackItem(itemName))
            {
                detectFind = true;
            }
            return detectFind;
        }

        private void FindAndDeleteItem(string itemName)
        {
            if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null && GroundOne.MC.FindBackPackItem(itemName))
            {
                GroundOne.MC.DeleteBackPack(new ItemBackPack(Database.EPIC_OLD_TREE_MIKI_DANPEN));
            }
            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && GroundOne.SC.FindBackPackItem(itemName))
            {
                GroundOne.SC.DeleteBackPack(new ItemBackPack(Database.EPIC_OLD_TREE_MIKI_DANPEN));
            }
            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && GroundOne.TC.FindBackPackItem(itemName))
            {
                GroundOne.TC.DeleteBackPack(new ItemBackPack(Database.EPIC_OLD_TREE_MIKI_DANPEN));
            }
        }

        private string MessageFormatForLana(int num)
        {
            GameObject tempObj = new GameObject();
            MainCharacter currentPlayer = tempObj.AddComponent<MainCharacter>();
            currentPlayer.FirstName = "ラナ";
            switch (num)
            {
                case 1001:
                    if (!GroundOne.WE.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1003);
                    }

                case 1002:
                    if (!GroundOne.WE.AvailableSecondCharacter)
                {
                    return currentPlayer.GetCharacterSentence(num);
                }
                else
                {
                    return currentPlayer.GetCharacterSentence(1004);
                }
                default:
                    return currentPlayer.GetCharacterSentence(num);
            }
        }

        private void HometownCommunicationStart()
        {
            TurnToNormal();
            GroundOne.WE.Truth_CommunicationSecondHomeTown = true;
            GroundOne.WE.AlreadyRest = true;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            cam.backgroundColor = Color.white;
            ExecRestInn();
        }
        
        public void tapPotionShop()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_POTION_SHOP, String.Empty, String.Empty);
            this.Filter.SetActive(true);
            SceneDimension.CallPotionShop(this);
        }

        public void tapGate()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_TRANSPORT_GATE, String.Empty, String.Empty);
            #region "ファージル宮殿 or カールハンツ爵の訓練場を選択"
            if (GroundOne.WE.AvailableFazilCastle)
            {
                this.Filter.SetActive(true);
                this.groupSelectGate.SetActive(true);
            }
            #endregion
            else
            {
                CallKahlhanz();
            }
        }

        public void HideAllChild()
        {
            if (this.systemMessage.text == this.MESSAGE_AUTOSAVE_EXIT)
            {
                SceneDimension.JumpToTitle();
                return;
            }

            if (this.nowMessage.Count <= 0)
            {
                this.Filter.SetActive(false);
                this.panelHide.gameObject.SetActive(false);
                this.groupRestInnMenu.SetActive(false);
                this.groupSelectCastleMenu.SetActive(false);
                this.groupSelectDungeon.SetActive(false);
                this.groupSelectGate.SetActive(false);
                if (groupDuelSelect.activeInHierarchy)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                }
                this.groupDuelSelect.SetActive(false);
                this.groupTicketChoice.SetActive(false);
                this.groupYesnoSystemMessage.SetActive(false);
                this.systemMessagePanel.SetActive(false);
            }
        }

        public void CallFazilCastle()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_TRANSPORT_GATE, Database.LOG_CALL_FAZILCASTLE, String.Empty);
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            groupSelectGate.SetActive(false);
            if (!GroundOne.WE.AvailableOneDayItem && GroundOne.WE.AlreadyCommunicateFazilCastle)
            {
                mainMessage.text = "アイン：ファージル宮殿は、また今度行ってみよう。";
                return;
            }
            if (GroundOne.WE.AvailableOneDayItem && GroundOne.WE.AlreadyCommunicateFazilCastle && GroundOne.WE.AlreadyGetOneDayItem && GroundOne.WE.AlreadyGetMonsterHunt)
            {
                mainMessage.text = "アイン：ファージル宮殿は、また今度行ってみよう。";
                return;
            }

            #region "初めてのファージル宮殿"
            if (!GroundOne.WE.Truth_Communication_FC31)
            {
                MessagePack.Message70015(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "謁見開始"
            else if (!GroundOne.WE.Truth_Communication_FC32)
            {
                MessagePack.Message70016(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "お楽しみ抽選券"
            else if (!GroundOne.WE.AvailableOneDayItem)
            {
                MessagePack.Message70017(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２度目以降の通常入城"
            else
            {
                MessagePack.Message70018(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
        }

        public void CallTicket()
        {
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            this.groupSelectCastleMenu.SetActive(false);
            MessagePack.Message70019(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        public void CallMonsterBattle()
        {
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            this.groupSelectCastleMenu.SetActive(false);
            MessagePack.Message70020(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        public void CallBacktoHometown()
        {
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            this.groupSelectCastleMenu.SetActive(false);
            MessagePack.Message70022(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        public void CallCommunicationSandy()
        {
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            this.groupSelectCastleMenu.SetActive(false);
            MessagePack.Message70021(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        public void CallTicketEin()
        {
            this.groupTicketChoice.SetActive(false);
            MessagePack.Message70019_2(ref nowMessage, ref nowEvent, 1);
            NormalTapOK();
        }

        public void CallTicketLana()
        {
            this.groupTicketChoice.SetActive(false);
            MessagePack.Message70019_2(ref nowMessage, ref nowEvent, 2);
            NormalTapOK();
        }

        public void CallKahlhanz()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_TRANSPORT_GATE, Database.LOG_CALL_KAHLHANZ, String.Empty);
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            groupSelectGate.SetActive(false);

            if (GroundOne.WE.alreadyCommunicateCahlhanz)
            {
                mainMessage.text = "アイン：カールハンツ爵にはまた今度教えてもらうとしよう。";
                return;
            }
            #region "三階開始時"
            if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationSinikia31 && !GroundOne.WE.alreadyCommunicateCahlhanz)
            {
                if (!GroundOne.WE.Truth_CommunicationLana31)
                {
                    mainMessage.text = "アイン：いや・・・その前に、ラナにひとまず挨拶しておくか。";
                    return;
                }
                if (!GroundOne.WE.Truth_CommunicationOl31)
                {
                    mainMessage.text = "アイン：いや・・・その前に、師匠にひとまず挨拶しておくか。";
                    return;
                }

                MessagePack.Message70013(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "四階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationSinikia41 && !GroundOne.WE.alreadyCommunicateCahlhanz)
            {
                MessagePack.Message70014(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "カールハンツ爵の訓練場"
            else if (!GroundOne.WE.alreadyCommunicateCahlhanz)
            {
                GroundOne.WE.alreadyCommunicateCahlhanz = true;

                #region "エンレイジ・ブラスト"
                if ((GroundOne.MC.Level >= 22) && (!GroundOne.MC.EnrageBlast))
                {
                    MessagePack.Message70001(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "ホーリー・ブレイカー"
                else if ((GroundOne.MC.Level >= 23) && (!GroundOne.MC.HolyBreaker))
                {
                    MessagePack.Message70002(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "サークル・スラッシュ"
                else if ((GroundOne.MC.Level >= 27) && (!GroundOne.MC.CircleSlash))
                {
                    MessagePack.Message70003(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "バイオレント・スラッシュ"
                else if ((GroundOne.MC.Level >= 28) && (!GroundOne.MC.ViolentSlash))
                {
                    MessagePack.Message70004(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "ランブル・シャウト"
                else if ((GroundOne.MC.Level >= 29) && (!GroundOne.MC.RumbleShout))
                {
                    MessagePack.Message70005(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "ワード・オブ・アティチュード"
                else if ((GroundOne.MC.Level >= 30) && (!GroundOne.MC.WordOfAttitude))
                {
                    MessagePack.Message70006(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "スカイ・シールド"
                else if ((GroundOne.MC.Level >= 31) && (!GroundOne.MC.SkyShield))
                {
                    MessagePack.Message70007(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "フローズン・オーラ"
                else if ((GroundOne.MC.Level >= 32) && (!GroundOne.MC.FrozenAura))
                {
                    MessagePack.Message70008(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "シャープ・グレア"
                else if ((GroundOne.MC.Level >= 33) && (!GroundOne.MC.SharpGlare))
                {
                    MessagePack.Message70009(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "リフレックス・スピリット"
                else if ((GroundOne.MC.Level >= 34) && (!GroundOne.MC.ReflexSpirit))
                {
                    MessagePack.Message70010(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "ニュートラル・スマッシュ"
                else if ((GroundOne.MC.Level >= 35) && (!GroundOne.MC.NeutralSmash))
                {
                    MessagePack.Message70011(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                #region "【元核】習得"
                else if ((GroundOne.MC.Level >= 40) && (!GroundOne.WE.AvailableArchetypeCommand))
                {
                    MessagePack.Message70012(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                #endregion
                else
                {
                    MessagePack.Message79999(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
            }
            #endregion
            #region "その他"
            else
            {
                mainMessage.text = "アイン：カールハンツ爵にはまた今度教えてもらうとしよう。";
            }
            #endregion
        }

        private void GoToFazilCastle()
        {
            ButtonVisibleControl(false);
            //this.panelObjective.SetActive(false);
            this.buttonHanna.gameObject.SetActive(false);
            this.buttonDungeon.gameObject.SetActive(false);
            this.buttonRana.gameObject.SetActive(false);
            this.buttonGanz.gameObject.SetActive(false);
            this.buttonPotion.gameObject.SetActive(false);
            this.buttonDuel.gameObject.SetActive(false);
            this.buttonShinikia.gameObject.SetActive(false);
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);
        }

        private void GoToKahlhanz()
        {
            //this.panelObjective.SetActive(false);
            this.buttonHanna.gameObject.SetActive(false);
            this.buttonDungeon.gameObject.SetActive(false);
            this.buttonRana.gameObject.SetActive(false);
            this.buttonGanz.gameObject.SetActive(false);
            this.buttonPotion.gameObject.SetActive(false);
            this.buttonDuel.gameObject.SetActive(false);
            this.buttonShinikia.gameObject.SetActive(false);
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
        }

        private void BackToTown()
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }
            this.panelObjective.SetActive(true);
            this.buttonHanna.gameObject.SetActive(true);
            this.buttonDungeon.gameObject.SetActive(true);
            this.buttonRana.gameObject.SetActive(true);
            this.buttonGanz.gameObject.SetActive(true);
            this.buttonPotion.gameObject.SetActive(true);
            this.buttonDuel.gameObject.SetActive(true);
            this.buttonShinikia.gameObject.SetActive(true);
        }

        private void ButtonVisibleControl(bool visible)
        {
            this.groupMenu.SetActive(visible);
            this.dayLabel.gameObject.SetActive(visible);
            this.panelObjective.SetActive(visible);
            this.buttonHanna.gameObject.SetActive(visible);
            this.buttonDungeon.gameObject.SetActive(visible);
            this.buttonRana.gameObject.SetActive(visible);
            this.buttonGanz.gameObject.SetActive(visible);
            if (GroundOne.WE.AvailablePotionshop)
            {
                this.buttonPotion.gameObject.SetActive(visible);
            }
            if (GroundOne.WE.AvailableDuelColosseum)
            {
                this.buttonDuel.gameObject.SetActive(visible);
            }
            if (GroundOne.WE.AvailableBackGate)
            {
                this.buttonShinikia.gameObject.SetActive(visible);
            }
            this.groupTicketChoice.SetActive(false);
        }

	    public void tapInn() {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ハンナ：あら、予定外の客が来たわね。すまないけど、本編になってから来てちょうだい。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_INN, String.Empty, String.Empty);
            #region "一日目"
            if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationHanna1 && GroundOne.MC.Level >= 1)
            {
                MessagePack.Message60000(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                MessagePack.Message60001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationHanna21)
            {
                MessagePack.Message60002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationHanna31)
            {
                MessagePack.Message60004(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "荷物預け追加"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.AvailableItemBank && GroundOne.WE.Truth_CommunicationOl31)
            {
                MessagePack.Message60003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationHanna41)
            {
                MessagePack.Message60005(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "現実世界"
            else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent603)
            {
                MessagePack.Message60006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "その他"
            else
            {
                if (GroundOne.WE.AvailableItemBank)
                {
                    this.groupRestInnMenu.SetActive(true);
                    this.Filter.SetActive(true);
                }
                else
                {
                    CallRestInn();
                }
            }
            #endregion
        }

        public void CallRestInn()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_CALL_RESTINN, String.Empty, String.Empty);
            HideAllChild();
            if (!GroundOne.WE.AlreadyRest)
            {
                MessagePack.Message69999(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else
            {
                MessagePack.Message69994(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
        }

        public void CallItemBank()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_CALL_ITEMBANK, String.Empty, String.Empty);
            HideAllChild();
            MessagePack.Message69993(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        private void ExecItemBank()
        {
            this.Filter.SetActive(true);
            SceneDimension.CallItemBank(this);
        }

        private void ExecRestInn()
        {
            ExecRestInn(false);
        }
        private void ExecRestInn(bool noAction)
        {
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

            GroundOne.WE.AlreadyRest = true;
            // [警告]：オブジェクトの参照が全ての場合、クラスにメソッドを用意してそれをコールした方がいい。
            if (GroundOne.MC != null)
            {
                GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife;
                GroundOne.MC.CurrentSkillPoint = GroundOne.MC.MaxSkillPoint;
                GroundOne.MC.CurrentMana = GroundOne.MC.MaxMana;
                GroundOne.MC.AlreadyPlayArchetype = false;
            }
            if (GroundOne.SC != null)
            {
                GroundOne.SC.CurrentLife = GroundOne.SC.MaxLife;
                GroundOne.SC.CurrentSkillPoint = GroundOne.SC.MaxSkillPoint;
                GroundOne.SC.CurrentMana = GroundOne.SC.MaxMana;
                GroundOne.SC.AlreadyPlayArchetype = false;
            }
            if (GroundOne.TC != null)
            {
                GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife;
                GroundOne.TC.CurrentSkillPoint = GroundOne.TC.MaxSkillPoint;
                GroundOne.TC.CurrentMana = GroundOne.TC.MaxMana;
                GroundOne.TC.AlreadyPlayArchetype = false;
            }
            GroundOne.WE.AlreadyUseSyperSaintWater = false;
            GroundOne.WE.AlreadyUseRevivePotion = false;
            GroundOne.WE.AlreadyUsePureWater = false;
            GroundOne.WE.AlreadyGetOneDayItem = false;
            GroundOne.WE.AlreadyGetMonsterHunt = false;
            GroundOne.WE.AlreadyDuelComplete = false;

            GroundOne.WE.GameDay += 1;
            dayLabel.text = GroundOne.WE.GameDay.ToString() + "日目";

            GroundOne.WE.AlreadyCommunicateFazilCastle = false;

            if (noAction == false)
            {
                GroundOne.PlaySoundEffect(Database.SOUND_REST_INN);

                MessagePack.Message69995(ref nowMessage, ref nowEvent);
                if (nowMessage.Count <= 0)
                {
                    NormalTapOK();
                }

                GroundOne.OpponentDuelist = WhoisDuelPlayer();
                if (GroundOne.OpponentDuelist != string.Empty)
                {
                    DuelSupportMessage(SupportType.Begin, GroundOne.OpponentDuelist);
                }
            }
        }

        public void tapSave()
        {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ラナ：ゲームをセーブする画面を呼び出すわ。このチュートリアルではセーブ不要よ。";
                return;
            }
 
            //GroundOne.SQL.UpdateOwner(Database.LOG_SAVE_GAME, "FromHomeTown", String.Empty);
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                this.Filter.GetComponent<Image>().color = Color.clear;
                this.Filter.SetActive(true);
                systemMessage.text = "ここまでの記録をセーブしました。\nゲームを終わりたい場合は、ゲーム終了を押してください。";
                systemMessagePanel.SetActive(true);
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld();
                return;
            } 
            SceneDimension.CallSaveLoad(this, true, false);
        }
        public void tapLoad()
        {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ラナ：ゲームをセーブする画面を呼び出すわ。このチュートリアルではロード不要よ。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_LOAD_GAME, "FromHomeTown", String.Empty);
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                this.Filter.GetComponent<Image>().color = Color.clear;
                this.Filter.SetActive(true);
                systemMessage.text = "現在ロードはできません。ここまでの記録は自動セーブされています。\nゲームを終わりたい場合は、ゲーム終了を押してください。";
                systemMessagePanel.SetActive(true);
                return;
            }
            SceneDimension.CallSaveLoad(this, false, false);
        }

        public override void ExitYes()
        {
            base.ExitYes();
            if (yesnoSystemMessage.text == Database.Request_Inn)
            {
                groupYesnoSystemMessage.SetActive(false);
                SceneDimension.CallRequestFood(Database.TruthHomeTown, this);
            }
        }
        
        public override void ExitNo()
        {
            base.ExitNo();
            if (yesnoSystemMessage.text == Database.Request_Inn)
            {
                yesnoSystemMessage.text = "";
                this.groupYesnoSystemMessage.SetActive(false);
                this.Filter.SetActive(false);
                MessagePack.Message69996(ref nowMessage, ref nowEvent);
                NormalTapOK();
            } 
        }

        public override void SceneBack()
        {
            Debug.Log("SceneBack (S)");
            base.SceneBack();
            if (yesnoSystemMessage.text == Database.Request_Inn)
            {
                yesnoSystemMessage.text = "";
                MessagePack.Message69997(ref nowMessage, ref nowEvent, this.currentRequestFood);
                NormalTapOK();
            }
            else if (this.forceSaveCall)
            {
                this.forceSaveCall = false;
                HometownCommunicationStart();
            }
            else if (this.nowAfterRestMessage)
            {
                this.nowAfterRestMessage = false;
                ExecRestInn();
            }
            else if (this.nowTalkingOlRandis)
            {
                Debug.Log("nowtalkingolrandis: " + GroundOne.DecisionSequence.ToString());
                #region "オル・ランディス会話中"
                if (GroundOne.DecisionSequence == 0)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_2(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_11(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 2)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_3(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_4(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 4)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_5(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_10(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 5)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_6(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_7(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 7)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_8(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_9(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 1)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_12(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_43(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 12)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_13(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_28(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 13)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_14(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_21(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 14)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_15(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_18(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 15)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_16(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_17(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 18)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_19(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_20(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 21)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_22(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_25(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 22)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_23(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_24(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 25)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_26(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_27(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 28)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_29(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_36(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 29)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_30(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_33(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 30)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_31(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_32(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 33)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_34(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_35(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 36)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_37(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_40(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 37)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_38(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_39(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 40)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_41(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_42(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 43)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_44(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_59(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 44)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_45(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_52(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 45)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_46(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_49(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 46)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_47(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_48(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 49)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_50(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_51(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 52)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_53(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_56(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 53)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_54(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_55(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 56)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_57(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_58(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 59)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_60(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_67(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 60)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_61(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_64(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 61)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_62(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_63(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 64)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_65(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_66(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 67)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_68(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_71(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 68)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_69(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_70(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 71)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message80004_72(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message80004_73(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else
                {
                    Debug.Log("else...");
                    GroundOne.DecisionFirstMessage = "";
                    GroundOne.DecisionMainMessage = "";
                    GroundOne.DecisionSecondMessage = "";
                    GroundOne.DecisionThirdMessage = "";
                    GroundOne.DecisionSequence = 0;
                    GroundOne.DecisionChoice = 0;
                    this.nowTalkingOlRandis = false;
                }
                //if (!we.Truth_CommunicationOl22Progress1)
                //{
                //    GroundOne.StopDungeonMusic();
                //    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                //    return;
                //} // 正解してない場合、この時点で一旦設問終了

                //  bool secondQuestion = we.Truth_CommunicationOl22Progress2;


                //if (!GroundOne.WE.Truth_CommunicationOl22Progress2)
                //{
                //    GroundOne.StopDungeonMusic();
                //    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                //    return;
                //} // 正解してない場合、この時点で一旦設問終了
                #endregion
            }
            else if (this.nowTalkingLanaAmiria)
            {
                this.nowTalkingLanaAmiria = false;
                if (GroundOne.DecisionSequence == 0)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message40003_2(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message40003_3(ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else if (GroundOne.DecisionSequence == 1)
                {
                    if (GroundOne.DecisionChoice == 1)
                    {
                        MessagePack.Message40010_11_2(1, ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else if (GroundOne.DecisionChoice == 2)
                    {
                        MessagePack.Message40010_11_2(2, ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                    else
                    {
                        MessagePack.Message40010_11_2(3, ref nowMessage, ref nowEvent);
                        NormalTapOK();
                    }
                }
                else
                {
                    Debug.Log("else...");
                    GroundOne.DecisionFirstMessage = "";
                    GroundOne.DecisionMainMessage = "";
                    GroundOne.DecisionSecondMessage = "";
                    GroundOne.DecisionThirdMessage = "";
                    GroundOne.DecisionSequence = 0;
                    GroundOne.DecisionChoice = 0;
                    this.nowTalkingLanaAmiria = false;
                }
            }
            else
            {
                tapOK();
            }
        }

        public void tapExit()
        {
            if (GroundOne.TutorialMode)
            {
                SceneDimension.JumpToTitle();
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_EXIT_GAME, "FromHomeTown", String.Empty);
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                // 現実世界
                if (!GroundOne.WE2.AutoSaveInfo)
                {
                    this.systemMessage.text = this.MESSAGE_AUTOSAVE_EXIT;
                    this.systemMessagePanel.SetActive(true);
                    this.Filter.SetActive(true);
                    GroundOne.WE2.AutoSaveInfo = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                    return;
                }
                else
                {
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.ExecSave(null, Database.WorldSaveNum, true);
                    SceneDimension.JumpToTitle();
                    return;
                }
            }
            else
            {
                // 通常セーブ
                this.Filter.SetActive(true);
                this.groupYesnoSystemMessage.SetActive(true);
            }
        }

        public void tapBookManual()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_DESCRIPTION, String.Empty, String.Empty);
            SceneDimension.CallTruthBookManual(this);
        }

        public void tapPlayback()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_PLAYBACK, String.Empty, String.Empty);
            SceneDimension.CallTruthPlayBack(this);
        }

        public void tapObjective()
        {
            float current = panelObjective.GetComponent<Image>().color.a;
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
            panelObjective.GetComponent<Image>().color = new Color(current, current, current, current);
            panelObjectiveTitle.GetComponent<Image>().color = new Color(current, current, current, current);
            panelGroupQuest.GetComponent<Image>().color = new Color(current, current, current, current);
            txtObjectiveTitle.color = new Color(0, 0, 0, current2);
            for (int ii = 0; ii < txtObjective.Count; ii++)
            {
                txtObjective[ii].color = new Color(0, 0, 0, current2);
            }
        }

        public void tapAchievement()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_ACHIEVEMENT, String.Empty, String.Empty);
            SceneDimension.CallAchievement(this);
        }

        public void CallStatusPlayer()
        {
            if (GroundOne.TutorialMode)
            {
                mainMessage.text = "ラナ：キャラクターのステータス画面が確認できるわよ。詳しくは別のチュートリアルで確認することが出来るわ。";
                return;
            }

            //GroundOne.SQL.UpdateOwner(Database.LOG_PLAYER_STATUS, "FromHomeTown", String.Empty);
            SceneDimension.CallTruthStatusPlayer(this, ref GroundOne.Player1Levelup, ref GroundOne.Player1UpPoint, ref GroundOne.Player1CumultiveLvUpValue, GroundOne.MC.FullName);
        }

        private void ShowActiveSkillSpell(MainCharacter player, string commandName)
        {
            this.Filter.SetActive(true);
            SceneDimension.CallTruthSkillSpellDesc(this, player.FirstName, commandName);
        }
        
        private void DuelSupportMessage(SupportType type, string OpponentDuelist)
        {
            string KIINA = "受付嬢";
            if (GroundOne.WE.DuelWinZalge) KIINA = "キーナ";
            else KIINA = "受付嬢";

            int[] levelBorder = new int[22];
            levelBorder[0] = 4;
            levelBorder[1] = 7;
            levelBorder[2] = 10;
            levelBorder[3] = 13;
            levelBorder[4] = 16;
            levelBorder[5] = 20;
            levelBorder[6] = 23;
            levelBorder[7] = 26;
            levelBorder[8] = 29;
            levelBorder[9] = 32;
            levelBorder[10] = 35;
            levelBorder[11] = 38;
            levelBorder[12] = 41;
            levelBorder[13] = 44;
            levelBorder[14] = 47;
            levelBorder[15] = 50;
            levelBorder[16] = 52;
            levelBorder[17] = 54;
            levelBorder[18] = 56;
            levelBorder[19] = 58;
            levelBorder[20] = 60;
            levelBorder[21] = 999;

            #region "芋プログラミングだが、実効性重視で良しとする。"
            if (levelBorder[0] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch1)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90001(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90001_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[1] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch2)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90002(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90002_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[2] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch3)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90003(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90003_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[3] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch4)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90004(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90004_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[4] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch5)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90005(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90005_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[5] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch6)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90006(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90006_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[6] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch7)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90007(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90007_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[7] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch8)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90008(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90008_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[8] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch9)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90009(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90009_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[9] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch10)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90010(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90010_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[9] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch10)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90010(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90010_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[10] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch11)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90011(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90011_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[11] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch12)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90012(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90012_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[12] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch13)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90013(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90013_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[13] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch14)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90014(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90014_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[14] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch15)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90015(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90015_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[15] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch16)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90016(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90016_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[16] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch17)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90017(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90017_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[17] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch18)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90018(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90018_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[18] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch19)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90019(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90019_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[19] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch20)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90020(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90020_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            else if (levelBorder[20] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch21)
            {
                if (type == SupportType.Begin)
                {
                    MessagePack.Message90021(ref nowMessage, ref nowEvent, OpponentDuelist, KIINA);
                    // NormalTapOK(); // ここでは不要
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    MessagePack.Message90021_2(ref nowMessage, ref nowEvent, GroundOne.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            #endregion
        }

        private string WhoisDuelPlayerCheck()
        {
            string duelPlayerName = string.Empty;
            if (!GroundOne.WE.TruthDuelMatch1)
            {
                duelPlayerName = Database.DUEL_EONE_FULNEA;
            }
            else if (!GroundOne.WE.TruthDuelMatch2)
            {
                duelPlayerName = Database.DUEL_MAGI_ZELKIS;
            }
            else if (!GroundOne.WE.TruthDuelMatch3)
            {
                duelPlayerName = Database.DUEL_SELMOI_RO;
            }
            else if (!GroundOne.WE.TruthDuelMatch4)
            {
                duelPlayerName = Database.DUEL_KARTIN_MAI;
            }
            else if (!GroundOne.WE.TruthDuelMatch5)
            {
                duelPlayerName = Database.DUEL_JEDA_ARUS;
            }
            else if (!GroundOne.WE.TruthDuelMatch6)
            {
                duelPlayerName = Database.DUEL_SINIKIA_VEILHANZ;
            }
            else if (!GroundOne.WE.TruthDuelMatch7)
            {
                duelPlayerName = Database.DUEL_ADEL_BRIGANDY;
            }
            else if (!GroundOne.WE.TruthDuelMatch8)
            {
                duelPlayerName = Database.DUEL_LENE_COLTOS;
            }
            else if (!GroundOne.WE.TruthDuelMatch9)
            {
                duelPlayerName = Database.DUEL_SCOTY_ZALGE;
            }
            else if (!GroundOne.WE.TruthDuelMatch10)
            {
                duelPlayerName = Database.DUEL_PERMA_WARAMY;
            }
            else if (!GroundOne.WE.TruthDuelMatch11)
            {
                duelPlayerName = Database.DUEL_KILT_JORJU;
            }
            else if (!GroundOne.WE.TruthDuelMatch12)
            {
                duelPlayerName = Database.DUEL_BILLY_RAKI;
            }
            else if (!GroundOne.WE.TruthDuelMatch13)
            {
                duelPlayerName = Database.DUEL_ANNA_HAMILTON;
            }
            else if (!GroundOne.WE.TruthDuelMatch14)
            {
                duelPlayerName = Database.DUEL_CALMANS_OHN;
            }
            else if (!GroundOne.WE.TruthDuelMatch15)
            {
                duelPlayerName = Database.DUEL_SUN_YU;
            }
            else if (!GroundOne.WE.TruthDuelMatch16)
            {
                duelPlayerName = Database.DUEL_SHUVALTZ_FLORE;
            }
            else if (!GroundOne.WE.TruthDuelMatch17)
            {
                duelPlayerName = Database.DUEL_RVEL_ZELKIS;
            }
            else if (!GroundOne.WE.TruthDuelMatch18)
            {
                duelPlayerName = Database.DUEL_VAN_HEHGUSTEL;
            }
            else if (!GroundOne.WE.TruthDuelMatch19)
            {
                duelPlayerName = Database.DUEL_OHRYU_GENMA;
            }
            else if (!GroundOne.WE.TruthDuelMatch20)
            {
                duelPlayerName = Database.DUEL_LADA_MYSTORUS;
            }
            else if (!GroundOne.WE.TruthDuelMatch21)
            {
                duelPlayerName = Database.DUEL_SIN_OSCURETE;
            }
            return duelPlayerName;
        }
        private string WhoisDuelPlayer()
        {
            string OpponentDuelist = string.Empty;

            if (GroundOne.WE.AlreadyDuelComplete) return string.Empty;

            int[] levelBorder = new int[22];
            levelBorder[0] = 4;
            levelBorder[1] = 7;
            levelBorder[2] = 10;
            levelBorder[3] = 13;
            levelBorder[4] = 16;
            levelBorder[5] = 20;
            levelBorder[6] = 23;
            levelBorder[7] = 26;
            levelBorder[8] = 29;
            levelBorder[9] = 32;
            levelBorder[10] = 35;
            levelBorder[11] = 38;
            levelBorder[12] = 41;
            levelBorder[13] = 44;
            levelBorder[14] = 47;
            levelBorder[15] = 50;
            levelBorder[16] = 52;
            levelBorder[17] = 54;
            levelBorder[18] = 56;
            levelBorder[19] = 58;
            levelBorder[20] = 60;
            levelBorder[21] = 999;

            if (GroundOne.WE.AvailableDuelMatch && GroundOne.WE.MeetOlLandis)
            {
                // レベル上限に応じて対戦相手をスキップ移行するのを撤廃した。
                // GroundOne.MC.Level <= levelBorder[x] - 1
                // 階層毎にDUEL相手を制御する処理も撤廃した。
                // !GroundOne.WE.TruthCompleteAreaX
                if (levelBorder[0] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch1)
                {
                    OpponentDuelist = Database.DUEL_EONE_FULNEA;
                }
                else if (levelBorder[1] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch2)
                {
                    OpponentDuelist = Database.DUEL_MAGI_ZELKIS;
                }
                else if (levelBorder[2] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch3)
                {
                    OpponentDuelist = Database.DUEL_SELMOI_RO;
                }
                else if (levelBorder[3] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch4)
                {
                    OpponentDuelist = Database.DUEL_KARTIN_MAI;
                }
                else if (levelBorder[4] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch5)
                {
                    OpponentDuelist = Database.DUEL_JEDA_ARUS;
                }
                else if (levelBorder[5] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch6)
                {
                    OpponentDuelist = Database.DUEL_SINIKIA_VEILHANZ;
                }
                else if (levelBorder[6] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch7)
                {
                    OpponentDuelist = Database.DUEL_ADEL_BRIGANDY;
                }
                else if (levelBorder[7] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch8)
                {
                    OpponentDuelist = Database.DUEL_LENE_COLTOS;
                }
                else if (levelBorder[8] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch9)
                {
                    OpponentDuelist = Database.DUEL_SCOTY_ZALGE;
                }
                else if (levelBorder[9] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch10)
                {
                    OpponentDuelist = Database.DUEL_PERMA_WARAMY;
                }
                else if (levelBorder[10] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch11)
                {
                    OpponentDuelist = Database.DUEL_KILT_JORJU;
                }
                else if (levelBorder[11] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch12)
                {
                    OpponentDuelist = Database.DUEL_BILLY_RAKI;
                }
                else if (levelBorder[12] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch13)
                {
                    OpponentDuelist = Database.DUEL_ANNA_HAMILTON;
                }
                else if (levelBorder[13] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch14)
                {
                    OpponentDuelist = Database.DUEL_CALMANS_OHN;
                }
                else if (levelBorder[14] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch15)
                {
                    OpponentDuelist = Database.DUEL_SUN_YU;
                }
                else if (levelBorder[15] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch16)
                {
                    OpponentDuelist = Database.DUEL_SHUVALTZ_FLORE;
                }
                else if (levelBorder[16] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch17)
                {
                    OpponentDuelist = Database.DUEL_RVEL_ZELKIS;
                }
                else if (levelBorder[17] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch18)
                {
                    OpponentDuelist = Database.DUEL_VAN_HEHGUSTEL;
                }
                else if (levelBorder[18] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch19)
                {
                    OpponentDuelist = Database.DUEL_OHRYU_GENMA;
                }
                else if (levelBorder[19] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch20)
                {
                    OpponentDuelist = Database.DUEL_LADA_MYSTORUS;
                }
                else if (levelBorder[20] <= GroundOne.MC.Level && !GroundOne.WE.TruthDuelMatch21)
                {
                    OpponentDuelist = Database.DUEL_SIN_OSCURETE;
                }
            }
            else
            {
                OpponentDuelist = string.Empty;
            }

            return OpponentDuelist;
        }

        private void ChangeBackgroundData(string filename, float darkValue = 0)
        {
            Debug.Log("ChangeBackgroundData (S): " + filename);
            if (filename == null || filename == string.Empty || filename == "")
            {
                Debug.Log("filename == null || filename == string.Empty");
                this.backgroundData = null;
            }
            else
            {
                Sprite current = Resources.Load<Sprite>(filename);
                this.backgroundData.gameObject.SetActive(true);
                if (darkValue > 0)
                {
                }
                else
                {
                    Debug.Log("update backgrounddata Sprite current");
                    this.backgroundData.sprite = current;
                }
            }
        }

        private void StartEnding()
        {
            GroundOne.WE2.SeekerEndingRoll = true;

            nowMessage.Clear();
            nowEvent.Clear();
            MessagePack.Message20602(ref nowMessage, ref nowEvent);
            for (int ii = 0; ii < nowMessage.Count; ii++)
            {
                ConstructEndingMessage(this.endingMessage, new Vector2(Screen.width / 2, 60), TextAnchor.MiddleLeft, nowMessage[ii]);
            }

            nowMessage.Clear();
            nowEvent.Clear();
            MessagePack.Message20602_2(ref nowMessage, ref nowEvent);
            for (int ii = 0; ii < nowMessage.Count; ii++)
            {
                ConstructEndingMessage(this.endingMessage2, new Vector2(Screen.width / 2, 60), TextAnchor.MiddleCenter, nowMessage[ii]);
            }

            nowMessage.Clear();
            nowEvent.Clear();
            MessagePack.Message20602_3(ref nowMessage, ref nowEvent);
            for (int ii = 0; ii < nowMessage.Count; ii++)
            {
                ConstructEndingMessage(this.endingMessage3, new Vector2(Screen.width / 2, 60), TextAnchor.MiddleLeft, nowMessage[ii]);
            }

            nowMessage.Clear();
            nowEvent.Clear();

            this.panelMessage.gameObject.SetActive(false);
            this.panelEnding.SetActive(true);
            this.nowAnimationEnding = true;
        }
    }
}

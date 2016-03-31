using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

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
        public Button btnOK;
        public GameObject systemMessagePanel;
        public Text systemMessage;
        public Camera cam;
        public GameObject groupRestInnMenu;
        public Button btnRestInn;
        public Button btnItemBank;
        public GameObject groupDuelSelect;
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

	    public static int serverPort = 8001;
	    private bool firstAction = false;
	    private string targetViewName = string.Empty;
        private bool nowHideing = false;
        public string currentRequestFood = string.Empty;

        bool forceSaveCall = false; // シナリオ進行上、強制セーブした後、”休息しました”を表示しするためのフラグ
        bool nowRequestFood = false; // 宿屋で休息の後、”休息しました”を表示するためのフラグ
        private string MESSAGE_AUTOSAVE_EXIT = @"ここまでの記録は自動セーブとなります。次回起動は、ここから再開となります";

        bool nowDuel = false;
        string OpponentDuelist = string.Empty;
        bool fromGoDungeon = false;
        bool nowTalkingOlRandis = false;

	    // Use this for initialization
        public override void Start()
        {
            base.Start();

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

            if (!GroundOne.WE.AlreadyRest)
            {
                Debug.Log("evening");
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                Debug.Log("moring");
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

            if (GroundOne.BattleResult != GroundOne.battleResult.None)
            {
                // todo
                //MessagePack.Message70012_2(ref nowMessage, ref nowEvent, result);
                //MessagePack.Message70013_2(ref nowMessage, ref nowEvent, result);
            }
        }
        
        string GetString(string msg, string protocolStr)
        {
            return msg.Substring(protocolStr.Length, msg.Length - protocolStr.Length);
        }

	    private void ReceiveFromClientSocket(string msg)
	    {
		    if (msg.Contains(Protocol.ExistCharacter)) {
			    if (GetString(msg, Protocol.ExistCharacter) == "false")
			    {
				    SaveData.SetName (this.nameField.text);
				    byte[] bb = System.Text.Encoding.UTF8.GetBytes (Protocol.CreateCharacter + this.nameField.text);
				    GroundOne.CS.SendMessage(bb);
				    Application.LoadLevel(targetViewName);
			    }
		    }
	    }

	    // Update is called once per frame
	    public override void Update () {
            base.Update();

            if (this.firstAction == false)
            {
                this.firstAction = true;

                // DUEL結果に応じた処理
                // 再挑戦時、逃げた時、敗北した時
                if ((GroundOne.BattleResult == GroundOne.battleResult.Retry) ||
                    (GroundOne.BattleResult == GroundOne.battleResult.Abort) ||
                    (GroundOne.BattleResult == GroundOne.battleResult.Ignore))
                {
                    MessagePack.Message30003(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                // 勝利した時
                else if (GroundOne.BattleResult == GroundOne.battleResult.OK)
                {
                    MessagePack.Message30004(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    ShownEvent();
                }
            }
            if (this.panelMessage.gameObject.activeInHierarchy && btnOK.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    tapOK();
                }
            }
	    }

	    private void CallNext() {
		    if (GroundOne.IsConnect && SaveData.GetName () == "") {
			    byte[] bb = System.Text.Encoding.UTF8.GetBytes (Protocol.ExistCharacter + this.nameField.text);
			    GroundOne.CS.SendMessage (bb);
		    } else {
			    Application.LoadLevel (targetViewName);
		    }
	    }

        private void ShownEvent()
        {
            // 死亡しているものは自動的に復活させます。
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
                    NormalTapOK();
                }
            }
            if (GroundOne.TC != null)
            {
                if (GroundOne.TC.Dead)
                {
                    GroundOne.TC.Dead = false;
                    GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife / 2;
                    MessagePack.HomeTownResurrect(ref nowMessage, ref nowEvent, GroundOne.TC);
                    NormalTapOK();
                }
            }       
            
        	if (GroundOne.WE.AlreadyShownEvent == false)
        	{
        		GroundOne.WE.AlreadyShownEvent = true;
        	}
        	else
        	{
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
            #region "エンディング"
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
            {
                MessagePack.Message20601(ref nowMessage, ref nowEvent);
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
            #region "戦闘：インスタントアクション"
            else if (!GroundOne.WE.AvailableInstantCommand && GroundOne.MC.Level >= 6)
            {
            	MessagePack.Message29003(ref nowMessage, ref nowEvent);
            	NormalTapOK();
            }
            #endregion
            else
            {
                mainMessage.text = "アイン：さて、何すっかな";
            }
        }
            	
	    public void tapDungeon() {
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
            else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea2 && (!GroundOne.WE.Truth_CommunicationLana31 || !GroundOne.WE.Truth_CommunicationGanz31 || !GroundOne.WE.Truth_CommunicationHanna31 || !GroundOne.WE.Truth_CommunicationOl31 || !GroundOne.WE.Truth_CommunicationSinikia31))
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
                this.OpponentDuelist = WhoisDuelPlayer();
                if (this.OpponentDuelist != string.Empty)
                {
                    DuelSupportMessage(SupportType.FromDungeonGate, this.OpponentDuelist);

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

        public void CallDungeon(int targetDungeon)
        {
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
            if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationLana1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana1 = true;
            else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationLana2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana2 = true;
            else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationLana3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana3 = true;
            else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationLana4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana4 = true;
            else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationLana5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana5 = true;
            else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationLana6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana6 = true;
            else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationLana7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana7 = true;
            else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationLana8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana8 = true;
            else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationLana9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana9 = true;
            else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationLana10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationLana10 = true;

            if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationHanna1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna1 = true;
            else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationHanna2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna2 = true;
            else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationHanna3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna3 = true;
            else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationHanna4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna4 = true;
            else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationHanna5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna5 = true;
            else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationHanna6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna6 = true;
            else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationHanna7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna7 = true;
            else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationHanna8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna8 = true;
            else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationHanna9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna9 = true;
            else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationHanna10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationHanna10 = true;

            if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationGanz1 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz1 = true;
            else if (this.firstDay >= 2 && !GroundOne.WE.Truth_CommunicationGanz2 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz2 = true;
            else if (this.firstDay >= 3 && !GroundOne.WE.Truth_CommunicationGanz3 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz3 = true;
            else if (this.firstDay >= 4 && !GroundOne.WE.Truth_CommunicationGanz4 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz4 = true;
            else if (this.firstDay >= 5 && !GroundOne.WE.Truth_CommunicationGanz5 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz5 = true;
            else if (this.firstDay >= 6 && !GroundOne.WE.Truth_CommunicationGanz6 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz6 = true;
            else if (this.firstDay >= 7 && !GroundOne.WE.Truth_CommunicationGanz7 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz7 = true;
            else if (this.firstDay >= 8 && !GroundOne.WE.Truth_CommunicationGanz8 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz8 = true;
            else if (this.firstDay >= 9 && !GroundOne.WE.Truth_CommunicationGanz9 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz9 = true;
            else if (this.firstDay >= 10 && !GroundOne.WE.Truth_CommunicationGanz10 && GroundOne.MC.Level >= 1 && GroundOne.WE.AlreadyCommunicate) GroundOne.WE.Truth_CommunicationGanz10 = true;
            #endregion

            SceneDimension.JumpToTruthDungeon(Database.TruthHomeTown);
        }

        public void tapCommunicationRana()
        {
            if (GroundOne.WE.AlreadyCommunicate)
            {
                mainMessage.text = MessageFormatForLana(1002);
                return; 
            }
            if (!GroundOne.WE.Truth_CommunicationLana1)
            {
                MessagePack.Message40000(ref nowMessage, ref nowEvent);
                NormalTapOK();
                return;
            }
        }

	    public void tapDuel() {
            this.OpponentDuelist = WhoisDuelPlayer();
            #region "Duel申請中"
            if (!GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                Debug.Log("4");
                MessagePack.Message80001(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディス遭遇"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                Debug.Log("3");
                MessagePack.Message80002(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationOl21)
            {
                Debug.Log("2");
                MessagePack.Message80003(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "オル・ランディスを仲間にするところ"
            else if (GroundOne.WE.dungeonEvent226 && !GroundOne.WE.Truth_CommunicationOl22)
            {
                Debug.Log("1");
                this.nowTalkingOlRandis = true;
                MessagePack.Message80004(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationOl31)
            {
                Debug.Log("5");
                MessagePack.Message80005(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationOl41)
            {
                Debug.Log("6");
                MessagePack.Message80006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "条件に応じて、Duelを実施します。"
            else if (this.OpponentDuelist != string.Empty && GroundOne.WE.AlreadyRest)
            {
                Debug.Log("7");
                GroundOne.WE.AlreadyDuelComplete = true;
                DuelSupportMessage(SupportType.FromDuelGate, this.OpponentDuelist);
                CallDuel(false);
            }
            #endregion
            #region "その他"
            else
            {
                Debug.Log("8");
                MessagePack.Message89999(ref nowMessage, ref nowEvent);
                NormalTapOK();
                // todo
                // GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            }
            #endregion
        }

        public void tapBattleSetting()
        {
            SceneDimension.CallTruthBattleSetting(Database.TruthHomeTown);
        }

        private void NormalTapOK()
        {
            this.nowHideing = true;
            tapOK();
        }
        public void tapOK()
        {
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
                if (current == MessagePack.ActionEvent.HomeTownMessageDisplay)
                {
                    systemMessage.text = this.nowMessage[this.nowReading];
                    systemMessagePanel.SetActive(true);
                    if (this.nowMessage[this.nowReading] == Database.Message_DuelAvailable)
                    {
                        buttonDuel.gameObject.SetActive(true);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_BattleSettingAvailable)
                    {
                        buttonBattleSetting.gameObject.SetActive(true);
                    }
                    else if (this.nowMessage[this.nowReading] == Database.Message_GoToAnotherField)
                    {
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
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpell)
                {
                    mainMessage.text = "";
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallDecision)
                {
                    mainMessage.text = "";
                }
                else
                {
                    systemMessagePanel.SetActive(false);
                    systemMessage.text = "";
                    mainMessage.text = "   " + this.nowMessage[this.nowReading];
                }

                if (current == MessagePack.ActionEvent.HomeTownGetItemFullCheck)
                {
                    GetItemFullCheck(GroundOne.MC, this.nowMessage[this.nowReading]);
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
                    this.cam.backgroundColor = UnityColor.WhiteSmoke;
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
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallSaveLoad)
                {
                    this.forceSaveCall = true;
                    SceneDimension.CallSaveLoad(Database.TruthHomeTown, true, false, this);
                }
                else if (current == MessagePack.ActionEvent.HomeTownGotoRealDungeon)
                {
                    //this.targetDungeon = 1;
                    GroundOne.WE2.RealDungeonArea = 1;
                    GroundOne.WE2.SeekerEvent605 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
                    SceneDimension.JumpToTruthDungeon(Database.TruthHomeTown);
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
                    GroundOne.TC.MainArmor = new ItemBackPack(Database.RARE_BLACK_AERIAL_ARMOR_REPLICA);
                    GroundOne.TC.Accessory = new ItemBackPack(Database.RARE_HEAVENLY_SKY_WING_REPLICA);
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
                else if (current == MessagePack.ActionEvent.HomeTownYesNoMessageDisplay)
                {
                    this.yesnoSystemMessage.text = this.nowMessage[this.nowReading];
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpell)
                {
                    ShowActiveSkillSpell(GroundOne.MC, this.nowMessage[this.nowReading]);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic13)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic19)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM19, Database.BGM19LoopBegin);
                }
                this.nowReading++;
                if (this.nowMessage[this.nowReading-1] == "")
                {
                    tapOK();
                }
            }

            if (this.nowReading >= this.nowMessage.Count)
            {
                this.nowReading = 0;
                this.nowMessage.Clear();
                this.nowEvent.Clear();

                this.nowHideing = false;
                this.panelHide.gameObject.SetActive(false);
                this.btnOK.enabled = false;
                this.btnOK.gameObject.SetActive(false);
            }
        }


        private void CallDuel(bool fromGoDungeon)
        {
            this.fromGoDungeon = fromGoDungeon;
            MessagePack.Message89998(ref nowMessage, ref nowEvent, this.OpponentDuelist);
            //NormalTapOK(); // ここでは不要
        }

        private void BattleStart(string playerName)
        {
            this.nowDuel = true;
            GroundOne.enemyName1 = playerName;
            GroundOne.enemyName2 = string.Empty;
            GroundOne.enemyName3 = string.Empty;
            GroundOne.StopDungeonMusic();
            SceneDimension.CallTruthBattleEnemy(this, true, false, false, false);
        }

        private void BattleResult(bool duelWin)
        {
            MessagePack.Message89998_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, duelWin, this.fromGoDungeon);
            NormalTapOK();
        }

        private void BlackOut()
        {
            GroundOne.StopDungeonMusic();

            cam.backgroundColor = Color.black;
            groupMenu.gameObject.SetActive(false);
            dayLabel.gameObject.SetActive(false);
            buttonHanna.gameObject.SetActive(false);
            buttonDungeon.gameObject.SetActive(false);
            buttonRana.gameObject.SetActive(false);
            buttonGanz.gameObject.SetActive(false);
            this.imgBackground.gameObject.SetActive(false);
        }

        private void TurnToNormal()
        {
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            cam.backgroundColor = Color.white;
            groupMenu.gameObject.SetActive(true);
            buttonHanna.gameObject.SetActive(true);
            buttonDungeon.gameObject.SetActive(true);
            buttonRana.gameObject.SetActive(true);
            buttonGanz.gameObject.SetActive(true);
            dayLabel.gameObject.SetActive(true);
            this.imgBackground.gameObject.SetActive(true);
        }

	    public void tapShop() {
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
                    SceneDimension.CallTruthEquipmentShop(Database.TruthHomeTown);
                    mainMessage.text = "";
                }
            }
            else if (GroundOne.WE.TruthCompleteArea1 && GroundOne.WE.AvailableMixSpellSkill && !buttonShinikia.isActiveAndEnabled)
            {
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    GroundOne.WE.AlreadyEquipShop = true;

                    MessagePack.Message50004(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    SceneDimension.CallTruthEquipmentShop(Database.TruthHomeTown);
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
                    SceneDimension.CallTruthEquipmentShop(Database.TruthHomeTown);
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "４階開始時"
            else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationGanz41)
            {
                GroundOne.WE.Truth_CommunicationGanz41 = true;

                MessagePack.Message50006(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "現実世界"
            else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent604)
            {
                MessagePack.Message50007(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent604)
            {
                MessagePack.Message50008(ref nowMessage, ref nowEvent);
                NormalTapOK();
            }
            #endregion
            #region "その他"
            else
            {
                SceneDimension.CallTruthEquipmentShop(Database.TruthHomeTown);
                mainMessage.text = "";
            }
            #endregion
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
            this.Filter.SetActive(true);
            SceneDimension.CallPotionShop(this);
        }

        public void tapGate()
        {
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
                this.groupDuelSelect.SetActive(false);
                this.groupTicketChoice.SetActive(false);
                this.groupYesnoSystemMessage.SetActive(false);
            }
        }

        public void CallFazilCastle()
        {
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
            MessagePack.Message70019_2(ref nowMessage, ref nowEvent, 1);
            NormalTapOK();
        }

        public void CallTicketLana()
        {
            MessagePack.Message70019_2(ref nowMessage, ref nowEvent, 2);
            NormalTapOK();
        }

        public void CallKahlhanz()
        {
            this.Filter.SetActive(false);
            this.panelHide.gameObject.SetActive(false);
            groupSelectGate.SetActive(false);

            if (GroundOne.WE.alreadyCommunicateCahlhanz)
            {
                mainMessage.text = "アイン：カールハンツ爵にはまた今度教えてもらうとしよう。";
                return;
            }

            #region "カールハンツ爵の訓練場"
            if (!GroundOne.WE.alreadyCommunicateCahlhanz)
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
            #region "三階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationSinikia31 && !GroundOne.WE.alreadyCommunicateCahlhanz)
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
            #region "その他"
            else
            {
                mainMessage.text = "アイン：カールハンツ爵にはまた今度教えてもらうとしよう。";
            }
            #endregion
        }

        private void GoToFazilCastle()
        {
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
        }

	    public void tapInn() {
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
            #region "荷物預け追加"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.AvailableItemBank && GroundOne.WE.Truth_CommunicationOl31)
            {
                MessagePack.Message60003(ref nowMessage, ref nowEvent);
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

                this.OpponentDuelist = WhoisDuelPlayer();
                if (this.OpponentDuelist != string.Empty)
                {
                    DuelSupportMessage(SupportType.Begin, this.OpponentDuelist);
                }
            }
        }

        public void tapSave()
        {
            SceneDimension.CallSaveLoad(Database.TruthHomeTown, true, false, this);
        }
        public void tapLoad()
        {
            SceneDimension.CallSaveLoad(Database.TruthHomeTown, false, false, this);
        }

        public override void ExitYes()
        {
            base.ExitYes();
            if (yesnoSystemMessage.text == Database.exitMessage4)
            {
                groupYesnoSystemMessage.SetActive(false);
                SceneDimension.CallRequestFood(Database.TruthHomeTown, this);
            }
        }
        
        public override void ExitNo()
        {
            base.ExitNo();
            if (yesnoSystemMessage.text == Database.exitMessage4)
            {
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
            if (yesnoSystemMessage.text == Database.exitMessage4)
            {
                MessagePack.Message69997(ref nowMessage, ref nowEvent, this.currentRequestFood);
                NormalTapOK();
            }
            else if (this.forceSaveCall)
            {
                this.forceSaveCall = false;
                HometownCommunicationStart();
            }
            else if (this.nowRequestFood)
            {
                this.nowRequestFood = false;
                ExecRestInn();
            }
            else if (this.nowDuel && !this.nowTalkingOlRandis)
            {
                Debug.Log("SceneBack (nowDuel)");

                this.nowDuel = false;
                bool duelWin = false;
                if (GroundOne.BattleResult == GroundOne.battleResult.OK)
                {
                    Debug.Log("duelWin is true");
                    duelWin = true;
                }
                else
                {
                    Debug.Log("duelWin is still false...");
                }
                BattleResult(duelWin);
            }
            else if (this.nowTalkingOlRandis)
            {
                Debug.Log("nowtalkingolrandis: " + GroundOne.DecisionSequence.ToString());
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
                else if (GroundOne.DecisionSequence == 58)
                {
                    bool duelWin = false;
                    if (GroundOne.BattleResult == GroundOne.battleResult.OK)
                    {
                        duelWin = true;
                    }
                    MessagePack.Message80004_74(ref nowMessage, ref nowEvent, duelWin);
                    NormalTapOK();
                }
                else
                {
                    Debug.Log("else...");
                    GroundOne.DecisionFirstMessage = "";
                    GroundOne.DecisionMainMessage = "";
                    GroundOne.DecisionSecondMessage = "";
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

            }
            else
            {
                tapOK();
            }
        }

        public void tapDebug1()
        {
            BattleStart(Database.DUEL_ANNA_HAMILTON);
        }

        public void tapExit()
        {
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
                    Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
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

        public void CallStatusPlayer()
        {
            SceneDimension.CallTruthStatusPlayer(Database.TruthHomeTown, ref GroundOne.Player1Levelup, ref GroundOne.Player1UpPoint, ref GroundOne.Player1CumultiveLvUpValue, GroundOne.MC.PlayerStatusColor);
        }

        private void ShowActiveSkillSpell(MainCharacter player, string commandName)
        {
            this.Filter.SetActive(true);
            SceneDimension.CallTruthSkillSpellDesc(this, player.FirstName, commandName);
        }

        private void GetItemFullCheck(MainCharacter player, string itemName)
        {
            bool result = player.AddBackPack(new ItemBackPack(itemName));
            if (result) return;

            string cannotTrash = string.Empty;
            if (itemName == Database.RARE_EARRING_OF_LANA ||
                itemName == Database.RARE_TOOMI_BLUE_SUISYOU)
            {
                cannotTrash = itemName;
            }
            SceneDimension.CallTruthStatusPlayer(Database.TruthHomeTown, this, true, cannotTrash);
            mainMessage.text = "";
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
                    MessagePack.Message90001_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90002_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90003_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90004_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90005_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90006_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90007_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90008_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90009_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90010_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90010_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90011_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90012_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90013_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90014_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90015_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90016_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90017_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90018_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90019_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90020_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
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
                    MessagePack.Message90021_2(ref nowMessage, ref nowEvent, this.OpponentDuelist, type);
                    NormalTapOK();
                }
            }
            #endregion
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
                    OpponentDuelist = Database.DUEL_SINIKIA_VEILHANTU;
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
                if (darkValue > 0)
                {
                    // todo
                    //System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
                    //Image newImg = AdjustBrightness(current, darkValue);
                    //this.backgroundData = newImg;
                }
                else
                {
                    Debug.Log("update backgrounddata Sprite current");
                    this.backgroundData.sprite = current;
                }
            }
        }
        public static Image AdjustBrightness(Image img, float b)
        {
            // todo
            Image newImg = null;
            ////明るさを変更した画像の描画先となるImageオブジェクトを作成
            //Bitmap newImg = new Bitmap(img.Width, img.Height);
            ////newImgのGraphicsオブジェクトを取得
            //Graphics g = Graphics.FromImage(newImg);

            //float[][] colorMatrixElements = { 
            //    new float[] {1,    0,    0,    0, 0},
            //    new float[] {0,    1,    0,    0, 0},
            //    new float[] {0,    0,    1,    0, 0},
            //    new float[] {0,    0,    0,    1, 0},
            //    new float[] {b,    b,    b,    0, 1}};

            ////ColorMatrixオブジェクトの作成
            //System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);

            ////ImageAttributesオブジェクトの作成
            //System.Drawing.Imaging.ImageAttributes ia =
            //    new System.Drawing.Imaging.ImageAttributes();
            ////ColorMatrixを設定する
            //ia.SetColorMatrix(cm);

            ////ImageAttributesを使用して描画
            //g.DrawImage(img,
            //    new Rectangle(0, 0, img.Width, img.Height),
            //    0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            ////リソースを解放する
            //g.Dispose();

            return newImg;
        }
    }
}

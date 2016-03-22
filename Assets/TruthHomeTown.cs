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
        enum SupportType
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
                Debug.Log("shown else");
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
                // todo
                //string Opponent = WhoisDuelPlayer();
                //if (Opponent != String.Empty)
                //{
                //    DuelSupportMessage(SupportType.FromDungeonGate, Opponent);

                //    CallDuel(Opponent, true);
                //}
                //else { }

                if (GroundOne.WE.TruthCompleteArea1 == false)
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
            // todo
            string Opponent = string.Empty;
            //#region "４階開始時"
            //if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationOl41)
            //{
            //    GroundOne.WE.Truth_CommunicationOl41 = true;

            //    string detectSword = PracticeSwordLevel(mc);

            //    UpdateMainMessage("アイン：ふぅ・・・対戦相手の戦歴チェックっと・・・");

            //    UpdateMainMessage("アイン：・・・師匠、今頃何してるかな。");

            //    UpdateMainMessage("ラナ：あっ、アイン。こんな所に居たのね。");

            //    UpdateMainMessage("アイン：おお、ラナか。どうしたんだ？");

            //    UpdateMainMessage("ラナ：なんかね、闘技場の受付の人が、アインを探してたみたいよ。");

            //    UpdateMainMessage("アイン：そうか。じゃあちょっと受付まで行ってくる。サンキュー。");

            //    CallSomeMessageWithAnimation("－－－　アインは受付まで出向いた　－－－");

            //    UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

            //    UpdateMainMessage("アイン：よう、受付さん。俺に何か用でもあったのか？");

            //    UpdateMainMessage("　　【受付嬢：はい、新着の伝言が入っております。】");

            //    UpdateMainMessage("アイン：伝言！？そんな制度があるのか？");

            //    UpdateMainMessage("　　【受付嬢：はい、あります。】");

            //    UpdateMainMessage("アイン：ま、まあいいか。。。で、どんな内容なんだ？");

            //    UpdateMainMessage("　　【受付嬢：こちらに、識別ID<3-297761 Ol_Landis>の音声データが入ったチップがあります。】");

            //    UpdateMainMessage("アイン：へえ、小っさいチップだな。");

            //    UpdateMainMessage("アイン：って、識別IDが・・・ッグ・・・");

            //    UpdateMainMessage("　　【受付嬢：音声チップは手前から向かいにあります、あちらのデンデン君にセットしてお使いください。】");

            //    UpdateMainMessage("アイン：デンデン君？？");

            //    UpdateMainMessage("　　【受付嬢：詳しくは端末にある操作説明を読んでご利用ください。】");

            //    UpdateMainMessage("アイン：あ、ああ・・・");

            //    UpdateMainMessage("アイン：(しかし、こんなものがあるとは・・・）");

            //    UpdateMainMessage("アイン：確かコッチだな。");

            //    UpdateMainMessage("アイン：おし、これだな。えーとどれどれ・・・");

            //    UpdateMainMessage("アイン（音読）：「チップを装置横にある差し込み口に挿入し、PUSHスタートを押してください。」");

            //    UpdateMainMessage("アイン：これか・・・よし。");

            //    UpdateMainMessage("　　【【【　その瞬間。　アインの脳内に直接オル・ランディスの音声が伝わってきた！！　】】】");

            //    UpdateMainMessage("ランディス：《よぉ、ザコアイン》");

            //    UpdateMainMessage("アイン：うお！！びっくりするな。直接聞こえるのかよ、これ。");

            //    UpdateMainMessage("ランディス：《これを聞いてるって事は、ひとまず、四階へと進め始めたって事だな》");

            //    UpdateMainMessage("アイン：ま、まあ聞いてみるか・・・");

            //    UpdateMainMessage("ランディス：《いいか、よく聞け》");

            //    UpdateMainMessage("ランディス：《今から俺が言う事は、全て事実だ》");

            //    UpdateMainMessage("アイン：っえ？");

            //    UpdateMainMessage("ランディス：《てめぇが受け止めるかどうかに関しては、てめぇで決めろ》");

            //    UpdateMainMessage("　　【【【　アインはほんの少しだけ呼吸が止まった　】】】");

            //    UpdateMainMessage("ランディス：《今からてめぇに起こりうる事象を全て伝える》");

            //    UpdateMainMessage("ランディス：《まず、てめぇの横にいるアーティだが》");

            //    UpdateMainMessage("ランディス：《四階開始と同時に姿を消す》");

            //    UpdateMainMessage("　　【【【　それは　】】】");

            //    UpdateMainMessage("ランディス：《次に四階の内容だが》");

            //    UpdateMainMessage("ランディス：《あっさりと道筋通り進めるはずだ》");

            //    UpdateMainMessage("ランディス：《迷うポイントはほとんどねえ、手筋どおりだ》");

            //    UpdateMainMessage("　　【【【　心のどこか奥底で　】】】");

            //    if (detectSword == Database.LEGENDARY_FELTUS)
            //    {
            //        UpdateMainMessage("ランディス：《だがてめぇは、自分の知らない間に》");

            //        UpdateMainMessage("ランディス：《今てめぇが手にしているその神剣フェルトゥーシュを、いつの間にか見失う》");
            //    }
            //    else
            //    {
            //        UpdateMainMessage("ランディス：《だがてめぇは、そのまま進み続け》");

            //        UpdateMainMessage("ランディス：《神剣フェルトゥーシュを永遠に手にする機会を失う》");
            //    }

            //    UpdateMainMessage("　　【【【　既に認識していたかの様な冷たい感触　】】】");

            //    if (detectSword == Database.LEGENDARY_FELTUS)
            //    {
            //        UpdateMainMessage("ランディス：《神剣フェルトゥーシュを見失った状態で、ダンジョンを進み続け》");
            //    }
            //    else
            //    {
            //        UpdateMainMessage("ランディス：《神剣フェルトゥーシュを入手出来ていないままの状態で、ダンジョンを進み続け》");
            //    }

            //    UpdateMainMessage("ランディス：《四階、最後の試練【神の選択肢】に遭遇》");

            //    UpdateMainMessage("ランディス：《てめぇは、そこで・・・》");

            //    UpdateMainMessage("　　【【【　心の隅々にまで、真っ黒なインクが染み込むように　】】】");

            //    UpdateMainMessage("ランディス：《誤りを選択する》");

            //    UpdateMainMessage("ランディス：《【神の選択肢】ってのは、そういうもんだ》");

            //    UpdateMainMessage("ランディス：《その後てめぇは》");

            //    UpdateMainMessage("　　【【【　絶望という色彩が体中を覆った　】】】");

            //    UpdateMainMessage("ランディス：《絶望する》");

            //    UpdateMainMessage("ランディス：《最下層への到達は、終着点なんかじぇねえ》");

            //    UpdateMainMessage("ランディス：《終わりの始まり》");

            //    UpdateMainMessage("ランディス：《絶対に、最下層への道を見誤んじゃねえぞ》");

            //    UpdateMainMessage("ランディス：《いいか、わかったな》");

            //    UpdateMainMessage("アイン：・・・");

            //    UpdateMainMessage("アイン：（なんだ・・・これ・・・）");

            //    UpdateMainMessage("アイン：（師匠が、何故これから先に起こりうる事を知ってるんだ）");

            //    UpdateMainMessage("アイン：（い、いやいやいや。そもそも未来なんて分かるはずがねえんだが）");

            //    UpdateMainMessage("アイン：（でもあの言い方は・・・）");

            //    UpdateMainMessage("アイン：（真に迫る言い方、つまりブラフや威嚇じゃねえ）");

            //    UpdateMainMessage("アイン：（本当の事をキッチリ言う時に使う声だ）");

            //    UpdateMainMessage("アイン：（だとしたら・・・）");

            //    UpdateMainMessage("ラナ：ねえ、どうしたのよ？さっきから止まってるみたいだけど");

            //    UpdateMainMessage("アイン：どぅわあぁ！！！");

            //    UpdateMainMessage("アイン：ラナか。何だ脅かすなよ。");

            //    UpdateMainMessage("ラナ：普通に声をかけただけなのに、過剰にビビってんのはそっちじゃない。");

            //    UpdateMainMessage("アイン：そ、そうか・・・ッハハハ・・・");

            //    UpdateMainMessage("ラナ：デンデン君からは、良い情報は得られたの？");

            //    UpdateMainMessage("アイン：ああ、まあな。それなりに。");

            //    UpdateMainMessage("ラナ：ふうん、まあ詮索はしないけど。");

            //    UpdateMainMessage("アイン：おし、じゃあ頑張って行くとするか！");

            //    UpdateMainMessage("ラナ：まったく相変わらずゲンキンよね。闘技場で忘れ物とかしないでよ？");

            //    UpdateMainMessage("アイン：ああ、分かってるって。っさ、行くぜ！");
            //}
            //#endregion
            //#region "３階開始時"
            //else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationOl31)
            //{
            //    we.Truth_CommunicationOl31 = true;

            //    UpdateMainMessage("アイン：師匠、いるか？");

            //    UpdateMainMessage("ランディス：なんだ、ザコアイン。");

            //    UpdateMainMessage("アイン：３階に向けて、今から少し作戦タイムを・・・");

            //    UpdateMainMessage("ランディス：そぉだ、言い忘れてた事がある。");

            //    UpdateMainMessage("アイン：何だよ？");

            //    UpdateMainMessage("ランディス：オレは抜ける。");

            //    UpdateMainMessage("アイン：え？");

            //    UpdateMainMessage("ランディス：以上だ。");

            //    UpdateMainMessage("アイン：・・・えええぇぇぇ！？何でだよ！？");

            //    UpdateMainMessage("ランディス：急な用事だ。　テメェのお守りはココまでだ。");

            //    UpdateMainMessage("アイン：な、何だよ突然！？　用事って何だよ！");

            //    UpdateMainMessage("ランディス：っせぇ、黙れザコ。てめぇには関係ねえ。");

            //    UpdateMainMessage("アイン：くっそおぉぉ・・・マジかよ・・・");

            //    UpdateMainMessage("ランディス：荷物の件だが、【ハンナゆったり宿屋】に預けておいた。");

            //    UpdateMainMessage("ランディス：好きな時に荷物整備しとけ。");

            //    UpdateMainMessage("ランディス：以上だ。");

            //    CallSomeMessageWithAnimation("オル・ランディスはその場から立ち去っていった・・・");

            //    Method.RemoveParty(we, tc);

            //    UpdateMainMessage("アイン：・・・くそぉ、なんの前触れも無しかよ。");

            //    UpdateMainMessage("ラナ：でもランディスさんは、アインが来るのを一応待っていたワケよね。");

            //    UpdateMainMessage("アイン：ん？んん・・・まあ確かにそうなのかも。");

            //    UpdateMainMessage("ラナ：フフフ、何かオカシイわね。アイン結構気に入られてるんじゃないの♪");

            //    UpdateMainMessage("アイン：ウソつけ、あんなのテキトー快楽主義者だろ。。。");

            //    UpdateMainMessage("ラナ：で、ダンジョンはどうするわけ？");

            //    UpdateMainMessage("アイン：１人減る事で、ダンジョンのモンスター難易度も調整されるだろ。");

            //    UpdateMainMessage("アイン：いまさら止めてもどうかなるわけじゃないしな。続行だ。");

            //    UpdateMainMessage("ラナ：アインが続行なら、私も引き続きついて行くわよ♪");

            //    UpdateMainMessage("アイン：ああ、そうしてくれ。助かるぜ！");

            //    if (we.Truth_CommunicationLana31)
            //    {
            //        UpdateMainMessage("ラナ：ところで、転送装置からファージル宮殿に行ってみない？");

            //        UpdateMainMessage("アイン：おお、そうだったな！　じゃ、行くとするか！");
            //    }

            //    return;
            //}
            //#endregion
            //#region "Duel申請中"
            //if (!GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            //{
            //    if (!GroundOne.WE.AlreadyRest)
            //    {
            //        UpdateMainMessage("アイン：まだ、登録申請中みたいだ。明日まで待つとするか。", true);
            //    }
            //    else
            //    {
            //        UpdateMainMessage("アイン：受付さんよ。俺の登録申請はまだか？");

            //        UpdateMainMessage("　　【受付嬢：もうしばらくお待ちください。】");

            //        UpdateMainMessage("アイン：そっか、じゃあまたな。", true);
            //    }
            //    return;
            //}
            //#endregion
            //#region "オル・ランディス遭遇"
            //if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            //{
            //    GroundOne.StopDungeonMusic();
            //    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

            //    UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

            //    UpdateMainMessage("アイン：よお受付さん。今日はちょっと顔を出しただけなんだ。");

            //    UpdateMainMessage("アイン：いやいや、ッホント。特に用事はねえんだ。");

            //    UpdateMainMessage("アイン：邪魔したな、ッハッハッハ！");

            //    UpdateMainMessage("アイン：っじゃ、また今度な！");

            //    UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

            //    UpdateMainMessage("ランディス：よぉ、ザコアイン。");

            //    UpdateMainMessage("アイン：・・・人違いだ。俺はザコアインじゃねえ。");

            //    UpdateMainMessage("ランディス：ほぉ、じゃあ誰なんだ？");

            //    UpdateMainMessage("アイン：いや・・・");

            //    UpdateMainMessage("ランディス：『いや、いやいやいや。』　か。");

            //    UpdateMainMessage("ランディス：てめぇ。全っっっっっっ然成長してねえようだな。");

            //    UpdateMainMessage("アイン：いや・・・っちょ、待っ、っちょ、ッタンマ！");

            //    UpdateMainMessage("ランディス：はぁ？どうタンマなんだ？");

            //    UpdateMainMessage("アイン：え？え、いや、っか、かかってくるんじゃ");

            //    UpdateMainMessage("ランディス：前祝したいってワケか。よおおおおぉぉぉぉぉし！良い心構えだ。");

            //    UpdateMainMessage("アイン：いや、違っ、っちょっちょちょ！タンマタンマタンマ！！");

            //    GroundOne.StopDungeonMusic();

            //    UpdateMainMessage("ランディス：死んでこいやああぁぁぁぁぁ！！！");

            //    UpdateMainMessage("　　【　ドドドスドスドスドスドドドドドスドスドスドスドス　】");

            //    UpdateMainMessage("　　【　ドガガガガガガドガガガガドドガガガガガガガガ　】");

            //    UpdateMainMessage("　　【　ボボボボボボグッシャアアァァァァ・・・　】");

            //    CallSomeMessageWithAnimation("－－－　アイン気絶から、１時間が経過して　－－－");

            //    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            //    UpdateMainMessage("アイン：ったく、ムチャクチャだぜ、ホント。。。いっつつつ・・・");

            //    UpdateMainMessage("ランディス：てめぇ、ほんっとに成長してねえな。");

            //    UpdateMainMessage("アイン：いきなり突っかかってくるのが悪いんだろうが。");

            //    UpdateMainMessage("ランディス：いきなり突っかかってねえだろぉが。");

            //    UpdateMainMessage("アイン：いや、何かそのこう・・・グォワアアアアって来るなっつうの。");

            //    UpdateMainMessage("ランディス：シラネエな、んなの。");

            //    UpdateMainMessage("ランディス：てめぇが弱すぎる。それだけだ。");

            //    UpdateMainMessage("アイン：いや、まだＤＵＥＬする場所でも無い所で突っかかってくるなつうの。");

            //    UpdateMainMessage("ランディス：いつだったら良いんだ？");

            //    UpdateMainMessage("アイン：いや・・・いやいや、そうじゃなくて。");

            //    UpdateMainMessage("ランディス：またか。その　『いや、いやいやいや。』");

            //    UpdateMainMessage("アイン：いや、違う。そうじゃな・・");

            //    UpdateMainMessage("ランディス：てめぇ、ＤＵＥＬに参戦するそうだな。");

            //    UpdateMainMessage("アイン：え？ああ、参戦するさ。");

            //    UpdateMainMessage("ランディス：俺様からザコアインへ、一言送ってやろうと思ってな。");

            //    UpdateMainMessage("アイン：っな、何だよ？");

            //    UpdateMainMessage("　　＜オル・ランディスは自分の足元へ指先を向け・・・＞");

            //    UpdateMainMessage("ランディス：　　俺んトコまで、来てみせろ。　");

            //    UpdateMainMessage("アイン：・・・ああ・・・当然さ！");

            //    UpdateMainMessage("アイン：当然行ってやるさ！見てろよな！！");

            //    UpdateMainMessage("　　＜オル・ランディスは少し微笑むと・・・＞");

            //    UpdateMainMessage("ランディス：ッフ、まぁガンバレや。ザコアイン。");

            //    CallSomeMessageWithAnimation("オル・ランディスはその場から立ち去っていった・・・");

            //    UpdateMainMessage("アイン：っくそう。結局、殴られ損かよ・・・");

            //    UpdateMainMessage("ラナ：っあ、アイン。いたいた♪");

            //    UpdateMainMessage("アイン：ラナ。いつのまに来てたんだ？");

            //    UpdateMainMessage("ラナ：アインが気絶してた場面ぐらいからよ♪");

            //    UpdateMainMessage("アイン：俺が気絶してるトコ見られてたって事かよ。");

            //    UpdateMainMessage("ラナ：でも本当、あんなに食らってるのに、意外とアイン元気よね。");

            //    UpdateMainMessage("アイン：師匠は生命に危険を及ぼす急所攻撃はしねえタイプなんだ。");

            //    UpdateMainMessage("アイン：だから、大概が気絶、もしくは病院送りが関の山ってワケさ。");

            //    UpdateMainMessage("ラナ：病院送りになっちゃう人もいるのね。まあＤＵＥＬって言う以上しょうがないんだろうけど。");

            //    UpdateMainMessage("ラナ：あ、そうそう。今日から参戦可能になったんでしょ？");

            //    UpdateMainMessage("アイン：まあ、そうだな。せっかくだし、今日から対戦してみる所なんだが");

            //    UpdateMainMessage("ラナ：ＤＵＥＬにおける詳細ルールは、見てみた？");

            //    UpdateMainMessage("アイン：いや、まだだな。ラナは知ってるのか？");

            //    UpdateMainMessage("ラナ：ううん、知らないわよ。");

            //    UpdateMainMessage("ラナ：ＤＵＥＬ参戦者のみに通達されるみたいだし。私は登録してないからね。");

            //    UpdateMainMessage("アイン：まあ、受付に聞いてみるとするか。おーい、受付さん。");

            //    UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

            //    UpdateMainMessage("アイン：すまねえ、さっきは用事ねえって言ったんだが、ＤＵＥＬ詳細ルールっての見せてもらえるか？");

            //    UpdateMainMessage("　　【受付嬢：アイン様ですね、了解いたしました。】");

            //    UpdateMainMessage("　　＜受付係員は何か書かれている紙切れを１枚持ってきた。＞");

            //    UpdateMainMessage("　　【受付嬢：アイン様に関するＤＵＥＬ詳細ルールはこの通りです。ご参照ください。】");

            //    UpdateMainMessage("アイン：サンキュー！　この紙は、他の奴にも見せていいのか？");

            //    UpdateMainMessage("　　【受付嬢：構いません。】");

            //    UpdateMainMessage("アイン：そっか、わざわざありがとな。");

            //    UpdateMainMessage("アイン：ラナ、もらってきたぞ。じゃあ、見てみるか。");

            //    UpdateMainMessage("ラナ：うん、何て書いてある？");

            //    UpdateMainMessage("アイン：どれどれ・・・");

            //    using (TruthDuelRule tdr = new TruthDuelRule())
            //    {
            //        tdr.StartPosition = FormStartPosition.CenterParent;
            //        tdr.ShowDialog();
            //    }

            //    UpdateMainMessage("アイン：なるほどな。大体分かったぜ。");

            //    UpdateMainMessage("ラナ：一応装備やステータス値なんかは見せてもらえるわけね。");

            //    UpdateMainMessage("アイン：そうみたいだな。");

            //    UpdateMainMessage("ラナ：そのかわり、こっちも一緒だから、お互い手の内が少し分かっちゃうわね。");

            //    UpdateMainMessage("アイン：そうみたいだな。");

            //    UpdateMainMessage("ラナ：ライフ０になった時点で勝敗が決まる。ってことは単純に相手を倒せば良いのよね。");

            //    UpdateMainMessage("アイン：そうみたいだな。");

            //    UpdateMainMessage("ラナ：今日は何か一緒に食べてく？");

            //    UpdateMainMessage("アイン：そうみたいだな。");

            //    UpdateMainMessage("　　　『シャゴオォォン！！』（ラナのドラスティックキックがアインのミゾオチに炸裂）　　");

            //    UpdateMainMessage("アイン：っつうう・・・分かった、分かったって。");

            //    UpdateMainMessage("アイン：っまあ、ちょっと１回だけ対戦させてくれ。その後で、飯食べにいこうぜ。");

            //    UpdateMainMessage("ラナ：ん、じゃあ待ってるわね。やるからには、ちゃんと勝ってよね♪");

            //    UpdateMainMessage("アイン：ああ、任せとけって！ッハッハッハ！！");

            //    we.MeetOlLandis = true;
            //    return;
            //}
            //#endregion
            //#region "２階開始時"
            //else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationOl21)
            //{
            //    GroundOne.StopDungeonMusic();
            //    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

            //    UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

            //    UpdateMainMessage("アイン：よお受付さん。今日もちょっと顔を出しただけだ。");

            //    UpdateMainMessage("アイン：っじゃな！ッハッハッハ！");

            //    UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

            //    UpdateMainMessage("ランディス：よぉ。わざわざご苦労なこった。");

            //    UpdateMainMessage("アイン：きょ、今日は用事があって来た。");

            //    UpdateMainMessage("ランディス：ほぉ？");

            //    UpdateMainMessage("アイン：解いたぜ、１階。");

            //    UpdateMainMessage("ランディス：やるじゃねえか。大したもんだ。");

            //    UpdateMainMessage("アイン：このまま、進むぜ。");

            //    UpdateMainMessage("アイン：２階制覇も楽勝さ！ッハッハッハ！");

            //    UpdateMainMessage("ランディス：ッフ、まあがんばれや。ザコアイン。");

            //    UpdateMainMessage("アイン：待て、今日はそういう話をしに来たんじゃねえ。");

            //    UpdateMainMessage("アイン：師匠、お願いがあるんだ。聞いてくれるか？");

            //    UpdateMainMessage("ランディス：言ってみろ。");

            //    UpdateMainMessage("アイン：師匠もダンジョンへ一緒に来てくれないか？");

            //    UpdateMainMessage("ランディス：・・・");

            //    UpdateMainMessage("ランディス：・・・");

            //    UpdateMainMessage("アイン：・・・");

            //    UpdateMainMessage("ランディス：・・・");

            //    UpdateMainMessage("ランディス：・・・");

            //    UpdateMainMessage("ランディス：・・・");

            //    UpdateMainMessage("アイン：・・・");

            //    UpdateMainMessage("ランディス：駄目だな。");

            //    UpdateMainMessage("アイン：・・・そうか。");

            //    UpdateMainMessage("ランディス：少しは成長してるみてえじゃねえか。");

            //    UpdateMainMessage("アイン：・・・え？");

            //    UpdateMainMessage("ランディス：何でもねえ。オラ！！とっとと２階制覇してきやがれ！！！");

            //    UpdateMainMessage("アイン：え、っちょっオワワワワ！っちょちょちょ！！タンマタンマタンマ！！！");

            //    UpdateMainMessage("　　【　ズドッドドドドドッドドォォドドドド　】");

            //    UpdateMainMessage("　　【　メキボグッシャアァァァ・・・　】");

            //    CallSomeMessageWithAnimation("－－－　アイン気絶から、１時間が経過して　－－－");

            //    GroundOne.StopDungeonMusic();
            //    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            //    UpdateMainMessage("ラナ：っで、断られちゃったわけ？");

            //    UpdateMainMessage("アイン：そうみたいだな。イッツツツ・・・");

            //    UpdateMainMessage("ラナ：でも良く誘う気になれたわね？自殺行為じゃない？？");

            //    UpdateMainMessage("アイン：でもさ。師匠が入れば、神がかり的にパワーアップするだろ？");

            //    UpdateMainMessage("ラナ：うーん、まあランディスのお師匠さんが居てくれたら心強いわね。");

            //    UpdateMainMessage("アイン：でもこんな理由じゃ仲間に入ってくれないよな。");

            //    UpdateMainMessage("ラナ：そうよね・・・う～ん・・・");

            //    UpdateMainMessage("アイン：いやいや、良いんだ。行こうぜ２階。");

            //    UpdateMainMessage("ラナ：良いの？");

            //    UpdateMainMessage("アイン：ああ、今はこのまま進むしかねえ。");

            //    UpdateMainMessage("アイン：いずれ入ってくれるキッカケのようなモノを作ってみせるさ。");

            //    UpdateMainMessage("ラナ：っそうね。じゃ、２階制覇に向けて頑張りましょ♪");

            //    UpdateMainMessage("アイン：ああ！", true);

            //    we.Truth_CommunicationOl21 = true;
            //    return;
            //}
            //#endregion
            //#region "オル・ランディスを仲間にするところ"
            //else if (GroundOne.WE.dungeonEvent226 && !GroundOne.WE.Truth_CommunicationOl22)
            //{
            //    //we.Truth_CommunicationOl22 = true;
            //    if (!we.Truth_CommunicationOl22Fail)
            //    {
            //        GroundOne.StopDungeonMusic();
            //        GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

            //        UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

            //        UpdateMainMessage("アイン：よお受付さん！");

            //        UpdateMainMessage("アイン：いや～～、いやいやいや！ッハッハッハ！");

            //        UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

            //        UpdateMainMessage("ランディス：おい、受付相手に何いきなり笑ってやがる。");

            //        UpdateMainMessage("アイン：師匠、教えてくれ。");

            //        UpdateMainMessage("　　『ランディスは一瞬ラナへ視線を移し・・・』");

            //        UpdateMainMessage("ランディス：何が聞きたい？");

            //        UpdateMainMessage("アイン：このダンジョン。どうなってる？");

            //        UpdateMainMessage("ランディス：どうもこうもねえ。単なるダンジョンだ。");

            //        UpdateMainMessage("アイン：台座の試練、クリアしたぜ。");

            //        UpdateMainMessage("ランディス：やるじゃねえか。ザコアインにしちゃ大したもんだ。");

            //        UpdateMainMessage("アイン：知の部屋もクリアまであと一歩だ。");

            //        UpdateMainMessage("ランディス：てめぇ、何の話をしにきた？");

            //        UpdateMainMessage("　　【【【　アインはさらに背筋に戦慄を感じた。　】】】");

            //        UpdateMainMessage("アイン：た！　ッタイム！！");

            //        UpdateMainMessage("アイン：教えてくれ。師匠。");

            //        UpdateMainMessage("ランディス：何を知りてぇんだ？");
            //    }
            //    else
            //    {
            //        if (!we.Truth_CommunicationOl22Progress1)
            //        {
            //            UpdateMainMessage("アイン：師匠、教えてくれ！　頼むぜ！");

            //            UpdateMainMessage("ランディス：何を知りてぇんだ？");
            //        }
            //        else if (!we.Truth_CommunicationOl22Progress2)
            //        {
            //            UpdateMainMessage("アイン：師匠・・・頼む、もう１回だけチャンスを！");

            //            UpdateMainMessage("ランディス：っち・・・しょうがねえ。");
            //        }
            //        else
            //        {

            //            UpdateMainMessage("ランディス：どぉした。");

            //            UpdateMainMessage("アイン：っまだまだ！　もう一回DUELだ！！");

            //            UpdateMainMessage("ランディス：何度でもかかってこいや、ザコアイン。");
            //        }
            //    }

            //    using (TruthDecision td = new TruthDecision())
            //    {
            //        td.StartPosition = FormStartPosition.CenterParent;

            //        bool firstQuestion = we.Truth_CommunicationOl22Progress1;
            //        if (!firstQuestion)
            //        {
            //            GroundOne.StopDungeonMusic();
            //            GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

            //            td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            //            td.FirstMessage = "このダンジョン、どう解いていけば良い？";
            //            td.SecondMessage = "このダンジョン、どうすれば解けるんだ？";
            //            td.ShowDialog();
            //            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //            {
            //                UpdateMainMessage("アイン：このダンジョン、どう解いていけば良い？");

            //                UpdateMainMessage("ランディス：どうもこうもねえ。自分なりに解いてみろ。");

            //                UpdateMainMessage("アイン：台座の試練ってのがあったんだ。");

            //                UpdateMainMessage("ランディス：ほぉ。");

            //                UpdateMainMessage("アイン：そこでは、『神々の詩』を回答することになっていた。");

            //                UpdateMainMessage("ランディス：回答は？");

            //                UpdateMainMessage("アイン：出来たさ。");

            //                UpdateMainMessage("ランディス：で、それがどうした？");

            //                UpdateMainMessage("アイン：あれをどう捉えて良いのかが、わからねえ。");

            //                td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            //                td.FirstMessage = "師匠は『神々の詩』に関して、何か知らないか？";
            //                td.SecondMessage = "師匠の時も、ダンジョン攻略時、あんな台座が？";
            //                td.StartPosition = FormStartPosition.CenterParent;
            //                td.ShowDialog();
            //                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                {
            //                    UpdateMainMessage("アイン：師匠は『神々の詩』に関して、何か知らないか？");

            //                    UpdateMainMessage("ランディス：知らねぇな。");

            //                    UpdateMainMessage("アイン：頼む。何でも良いから知ってる事を");

            //                    UpdateMainMessage("ランディス：ゴチャゴチャとうるせぇ。帰れ。");
            //                    we.Truth_CommunicationOl22Fail = true;
            //                }
            //                else
            //                {
            //                    UpdateMainMessage("アイン：師匠の時も、ダンジョン攻略時、あんな台座が？");

            //                    UpdateMainMessage("ランディス：ああ。");

            //                    UpdateMainMessage("アイン：どんな内容だったんだ？");

            //                    UpdateMainMessage("ランディス：てめぇには関係ねえ。");

            //                    UpdateMainMessage("アイン：教えてくれても良いだろ？");

            //                    UpdateMainMessage("ランディス：言っても意味がねえ。");

            //                    td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            //                    td.FirstMessage = "意味がねえって・・・どういう意味だ？";
            //                    td.SecondMessage = "意味がねえかどうかは、聞かなきゃ分からないだろ？";
            //                    td.StartPosition = FormStartPosition.CenterParent;
            //                    td.ShowDialog();
            //                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        UpdateMainMessage("アイン：意味がねえって・・・どういう意味だ？");

            //                        UpdateMainMessage("ランディス：言葉通りだ。言った所で意味はねえ。");

            //                        UpdateMainMessage("アイン：俺には当てはまらない・・・って事か？");

            //                        UpdateMainMessage("ランディス：良く分かってるじゃねえか。");

            //                        td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            //                        td.FirstMessage = "つまり、台座は俺の【未来】に関係してるって事か？";
            //                        td.SecondMessage = "つまり、台座は俺の【過去】に関係してるって事か？";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            UpdateMainMessage("アイン：つまり、台座は俺の【未来】に関係してるって事か？");

            //                            UpdateMainMessage("ランディス：だからてめぇはザコアインだって言ってんだ。");

            //                            UpdateMainMessage("アイン：違うのかよ？頼むから、教えてくれよ？");

            //                            UpdateMainMessage("ランディス：ゴチャゴチャとうるせぇ。帰れ。");
            //                            we.Truth_CommunicationOl22Fail = true;
            //                        }
            //                        else
            //                        {
            //                            UpdateMainMessage("アイン：つまり、台座は俺の【過去】に関係してるって事か？");

            //                            UpdateMainMessage("ランディス：だとしたら、どうする。");

            //                            td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            //                            td.FirstMessage = "過去を基として、解を導き出せって事か？";
            //                            td.SecondMessage = "過去を紐解いて、正解を見つけろって事か？";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                GroundOne.StopDungeonMusic();

            //                                UpdateMainMessage("アイン：過去を基として、解を導き出せって事か？");

            //                                UpdateMainMessage("ランディス：さあな。");

            //                                UpdateMainMessage("アイン：どうなんだよ？");

            //                                UpdateMainMessage("ランディス：自分で考えろ。");

            //                                UpdateMainMessage("アイン：ああ・・・");

            //                                UpdateMainMessage("ランディス：ちったぁ、まともになってきたじゃねえか。");

            //                                UpdateMainMessage("アイン：・・・え？");

            //                                UpdateMainMessage("ランディス：今度は、俺から幾つか問う。");

            //                                UpdateMainMessage("ランディス：答えろ。");

            //                                UpdateMainMessage("アイン：あ、ああ！");

            //                                UpdateMainMessage("", true);

            //                                we.Truth_CommunicationOl22Progress1 = true;
            //                                firstQuestion = true;
            //                                // 正解
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("アイン：過去を紐解いて、正解を見つけろって事か？");

            //                                UpdateMainMessage("ランディス：正解なんてもんはねえ。");

            //                                UpdateMainMessage("アイン：じゃあ、過去が今回の台座の件とどう関係してるんだよ？");

            //                                UpdateMainMessage("ランディス：てめぇ、何を聞きにきた？");

            //                                UpdateMainMessage("アイン：っぐ・・・");

            //                                UpdateMainMessage("ランディス：話にならねえな。");

            //                                UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        UpdateMainMessage("アイン：意味がねえかどうかは、聞かなきゃ分からないだろ？");

            //                        UpdateMainMessage("ランディス：ッチ・・・話にならねぇ。");

            //                        UpdateMainMessage("アイン：っま、待ってくれ！！");

            //                        UpdateMainMessage("ランディス：意味がねぇもの、無理に聞いてどうなる？");

            //                        UpdateMainMessage("アイン：っぐ・・・");

            //                        UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");
            //                        we.Truth_CommunicationOl22Fail = true;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                UpdateMainMessage("アイン：このダンジョン、どうすれば解けるんだ？");

            //                UpdateMainMessage("ランディス：知らねぇな。自分で探せ。");
            //                we.Truth_CommunicationOl22Fail = true;
            //            }
            //        }

            //        if (!we.Truth_CommunicationOl22Progress1)
            //        {
            //            GroundOne.StopDungeonMusic();
            //            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            //            return;
            //        } // 正解してない場合、この時点で一旦設問終了


            //        bool secondQuestion = we.Truth_CommunicationOl22Progress2;

            //        if (!secondQuestion)
            //        {
            //            GroundOne.StopDungeonMusic();
            //            GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

            //            td.MainMessage = "　【　てめぇ、何で俺様の所に来る気になった？　】";
            //            td.FirstMessage = "ラナと相談した結果、師匠に聞こうって事で。";
            //            td.SecondMessage = "台座の回答をした後、不思議とそう感じたからだ。";
            //            td.ShowDialog();
            //            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //            {
            //                // Fail
            //                td.MainMessage = "　【　事実を聞いてんじゃねえ。てめぇはどうなんだ？　】";
            //                td.FirstMessage = "台座はクリアした。だが、妙なひっかかりを覚えた。";
            //                td.SecondMessage = "どうって・・・特にどうってわけじゃないが・・・";
            //                td.ShowDialog();
            //                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                {
            //                    // Fail
            //                    td.MainMessage = "　【　何が引っかかったのか、把握はしてんのか？　】";
            //                    td.FirstMessage = "把握はできてねえが、それなりの違和感は・・・";
            //                    td.SecondMessage = "いや・・・それが何なのかはわからねえ・・・";
            //                    td.ShowDialog();
            //                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　はっきりしねえな。ドッチなんだ？　】";
            //                        td.FirstMessage = "す、すまねえ・・・";
            //                        td.SecondMessage = "台座は解けた。でもまだ奥底が見えないんだ。";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
            //                            td.FirstMessage = "っそ、それは・・・";
            //                            td.SecondMessage = "ダンジョンで稼がなくちゃならねえ。それだけさ";
            //                            td.ShowDialog();
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　奥底ってのは何を指して言ってる？　】";
            //                            td.FirstMessage = "奥底ってのは、つまり・・・";
            //                            td.SecondMessage = "そんなの、俺にだってわからねえよ。";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　過去と台座の関係ぐらいは分かってんだろうな？　】";
            //                        td.FirstMessage = "もちろん、わかってるさ！";
            //                        td.SecondMessage = "ッグ・・・";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　じゃあ、言ってみろ。　】";
            //                            td.FirstMessage = "そ、それは・・・";
            //                            td.SecondMessage = "過去の出来事が台座での設問になる。そうだろ？";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　ダンジョンの攻略方法、把握度合いはどぉなんだ？　】";
            //                            td.FirstMessage = "それなりに、探索してるし分かってるつもりだぜ。";
            //                            td.SecondMessage = "す・・・少しぐらいなら・・・";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    // Fail
            //                    td.MainMessage = "　【　ダンジョンの攻略具合はどぉなんだ？　】";
            //                    td.FirstMessage = "順調だ。滞りなく進んでる。";
            //                    td.SecondMessage = "順調ってワケじゃねえ・・・";
            //                    td.ShowDialog();
            //                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　攻略の意味は分かってんだろうな？　】";
            //                        td.FirstMessage = "攻略の・・・意味？";
            //                        td.SecondMessage = "最下層へ進めるための謎を解く。そうだろ？";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　ダンジョンの攻略度合いさ。決まってるだろ？　】";
            //                            td.FirstMessage = "ああ、これなら楽勝だぜ。";
            //                            td.SecondMessage = "台座をクリアした所だが・・・";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　最下層？　謎？　何言ってんだテメェは。　】";
            //                            td.FirstMessage = "えっ？ち、違うのかよ？";
            //                            td.SecondMessage = "え？　っと・・・";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　台座のどこが気になった？言ってみろ。　】";
            //                        td.FirstMessage = "詩の内容は過去に聞いた事がある。しかし・・・";
            //                        td.SecondMessage = "看板の前に、突然出てきたって事ぐらいかな。";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
            //                            td.FirstMessage = "あ、ああ・・・";
            //                            td.SecondMessage = "過去との決別をするために！って事だろ？";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　それがどぉした？　】";
            //                            td.FirstMessage = "あ、っいや・・・";
            //                            td.SecondMessage = "・・・　・・・";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //Sucess
            //                td.MainMessage = "　【　台座をクリアした意味はわかってるか？　】";
            //                td.FirstMessage = "いや、まだ断片的な事しか、わからねえ。";
            //                td.SecondMessage = "ああ、当然分かっているさ！";
            //                td.ShowDialog();
            //                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                {
            //                    // Success
            //                    td.MainMessage = "　【　今後どうやって進めてくつもりだ？　】";
            //                    td.FirstMessage = "次への階段を探し出し、最下層を目指すまでさ。";
            //                    td.SecondMessage = "ダンジョン内をくまなく探索しながら進めるさ。";
            //                    td.ShowDialog();
            //                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　最下層まで行けたとして、どうするつもりだ？　】";
            //                        td.FirstMessage = "どうって・・・もっと強くなってやるさ。";
            //                        td.SecondMessage = "どうって・・・";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　強くなった後はどうするかを聞いてんだ。　】";
            //                            td.FirstMessage = "その後は・・・その・・・";
            //                            td.SecondMessage = "強ければ良いんだろ？それが師匠の教えじゃねえか。";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
            //                            td.FirstMessage = "っそ、それは・・・";
            //                            td.SecondMessage = "ダンジョン制覇そのものが目的さ、それ以上の意味は無い。";
            //                            td.ShowDialog();
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        // Success
            //                        td.MainMessage = "　【　探索して、ダンジョンの仕掛けは把握したのか？　】";
            //                        td.FirstMessage = "台座なら、ちゃんと見つけたぜ。クリアもした。";
            //                        td.SecondMessage = "台座の一部ぐらいしか・・・全体像はまだ何とも・・・";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　てめぇのお望みってのは、台座だったのかよ？　】";
            //                            td.FirstMessage = "メインの仕掛けを解いた。探索としては成功だろ？";
            //                            td.SecondMessage = "いや・・・そういうわけじゃ・・・";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }

            //                        }
            //                        else
            //                        {
            //                            // Success
            //                            td.MainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
            //                            td.FirstMessage = "腕を試したかった。それだけだ。";
            //                            td.SecondMessage = "・・・　・・・　";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                // Fail
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                GroundOne.StopDungeonMusic();

            //                                // Success
            //                                UpdateMainMessage("ランディス：・・・ほぉ。");
            //                                we.Truth_CommunicationOl22Progress2 = true;
            //                                secondQuestion = true;
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    // Fail
            //                    td.MainMessage = "　【　じゃあ、言ってみろ。　】";
            //                    td.FirstMessage = "台座は過去に関連してる。過去を基に解を導き出せばいい。";
            //                    td.SecondMessage = "あの詩がこのダンジョンにおける最大の鍵だ。";
            //                    td.ShowDialog();
            //                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　どぉ導き出されるってんだ？言ってみろ。　】";
            //                        td.FirstMessage = "過去の出来事を思い出しつつ、ダンジョン正解ルートを導き出す。";
            //                        td.SecondMessage = "ど、どうって・・・";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　正解だと？　てめぇ、俺から一体何を学んだ？　】";
            //                            td.FirstMessage = "っあ！い、いやいやいや！！";
            //                            td.SecondMessage = "隠さないでくれよ。正解ルートあるんだろ？";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　答えるか、答えないか。ハッキリしろ。　】";
            //                            td.FirstMessage = "す、すまねぇ・・・";
            //                            td.SecondMessage = "未来へとつながるキーワードを探せば良いんだ！";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        // Fail
            //                        td.MainMessage = "　【　最大の鍵。どこで使うんだ？　】";
            //                        td.FirstMessage = "ど、どこって・・・";
            //                        td.SecondMessage = "最下層だ。最後で使うんだろ、こういうのは。";
            //                        td.ShowDialog();
            //                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
            //                            td.FirstMessage = "最下層だ！";
            //                            td.SecondMessage = "一番最初だ！";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            // Fail
            //                            td.MainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
            //                            td.FirstMessage = "師匠の【炎神グローブ】みたいなヤツを俺も欲しいからさ。";
            //                            td.SecondMessage = "もちろん、最下層到達で師匠に並ぶためさ。";
            //                            td.ShowDialog();
            //                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                            else
            //                            {
            //                                UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
            //                                we.Truth_CommunicationOl22Fail = true;
            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //        if (!we.Truth_CommunicationOl22Progress2)
            //        {
            //            GroundOne.StopDungeonMusic();
            //            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            //            return;
            //        } // 正解してない場合、この時点で一旦設問終了

            //        GroundOne.StopDungeonMusic();
            //        GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

            //        if (!we.Truth_CommunicationOl22DuelFail)
            //        {
            //            UpdateMainMessage("アイン：師匠、おりいって頼みがある。");

            //            UpdateMainMessage("ランディス：何だ、言ってみろ。");

            //            UpdateMainMessage("アイン：師匠、このダンジョン一緒に来てくれ。頼むぜ！");

            //            UpdateMainMessage("ランディス：・・・");

            //            UpdateMainMessage("ランディス：ちっと腕見せてみろ。");

            //            UpdateMainMessage("アイン：え？");

            //            UpdateMainMessage("ランディス：３");

            //            UpdateMainMessage("ランディス：２");

            //            UpdateMainMessage("アイン：おわっ！マジかよ！？");

            //            UpdateMainMessage("ランディス：１");

            //            UpdateMainMessage("アイン：ック・・・来い！！");
            //        }

            //        bool result = BattleStart(Database.DUEL_OL_LANDIS, true);
            //        if (result)
            //        {
            //            // 勝った場合、次の会話へ
            //            GroundOne.WE2.WinOnceOlLandis = true;
            //        }
            //        else
            //        {
            //            if (we.Truth_CommunicationOl22DuelFailCount >= 3)
            //            {
            //                // 負けすぎなので、そのまま通す。ただし、WinOnceOlLandisはつけない。
            //            }
            //            else
            //            {
            //                // 負けた場合、強制リトライ
            //                UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");

            //                UpdateMainMessage("アイン：ッグ・・・");

            //                we.Truth_CommunicationOl22Fail = true;
            //                we.Truth_CommunicationOl22DuelFail = true;
            //                we.Truth_CommunicationOl22DuelFailCount++;
            //                return;
            //            }
            //        }

            //        GroundOne.StopDungeonMusic();
            //        GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            //        UpdateMainMessage("　　【受付嬢：そちらの方々！！今すぐ対戦を中止してください！！　】");

            //        UpdateMainMessage("アイン：ッウワ・・・ヤベ・・・");

            //        UpdateMainMessage("　　【受付嬢：闘技場内での勝手な対戦は、ルール厳禁となっております。　】");

            //        UpdateMainMessage("ランディス：ッチ、分かった分かったって、嬢ちゃん。");

            //        UpdateMainMessage("　　【受付嬢：今から、名前を読み上げます。　】");

            //        UpdateMainMessage("　　【受付嬢：オル・ランディス様　】");

            //        UpdateMainMessage("　　【受付嬢：アイン・ウォーレンス様　】");

            //        UpdateMainMessage("　　【受付嬢：読み上げられた者は、罰としてDUEL戦歴に１敗が加えられます。】");

            //        UpdateMainMessage("アイン：ッゲ！！マジかよ！？");

            //        UpdateMainMessage("ランディス：くだらんルールだな。");

            //        UpdateMainMessage("　　【受付嬢：ただし、アイン・ウォーレンス様はDUEL参加より" + we.GameDay.ToString() + "日以内のため、特例除外とします。】");

            //        UpdateMainMessage("アイン：助かったぜ・・・ッホ・・・");

            //        UpdateMainMessage("　　【受付嬢：合わせて、オル・ランディス様には累積罰として更に２敗が加えられます。】");

            //        UpdateMainMessage("ランディス：勝手に付けとけ。");

            //        UpdateMainMessage("　　【受付嬢：なお、今後も続けて行った場合、DUEL戦歴に累積的な敗北数が加算されます。】");

            //        UpdateMainMessage("　　【受付嬢：くれぐれも闘技場内での勝手なDUELはしないよう、お願いいたします。】");

            //        UpdateMainMessage("アイン：ああ、悪かったな、受付さん。次からは気をつけるよ。");

            //        UpdateMainMessage("ランディス：こいつらに気を使う必要はねぇ。ザコアイン。");

            //        UpdateMainMessage("アイン：何でだよ。運営側の受付さんだろ？別に良いじゃねえか。");

            //        UpdateMainMessage("ランディス：てめぇのそういうトコ・・・");

            //        UpdateMainMessage("アイン：受付さんだって人間だ。良いだろ？");

            //        UpdateMainMessage("ランディス：つくづく甘ちゃんだなテメェは・・・好きにしろ。");

            //        UpdateMainMessage("アイン：っふぅ・・・マジで疲れたぜ。");

            //        UpdateMainMessage("アイン：師匠とやるといつも全力だ・・・もう動けねえ・・・");

            //        UpdateMainMessage("ランディス：ダンジョン。");

            //        UpdateMainMessage("アイン：っえ？");

            //        UpdateMainMessage("ランディス：行ってやっても良い。");

            //        UpdateMainMessage("アイン：おおおお！！！　マジで！？　やった！！！");

            //        UpdateMainMessage("ランディス：条件がある。");

            //        UpdateMainMessage("アイン：あ、あぁ。教えてくれ。");

            //        UpdateMainMessage("ランディス：ラナを外せ。");

            //        UpdateMainMessage("アイン：・・・　えっ　・・・");

            //        UpdateMainMessage("ランディス：冗談だ。真に受けんなボケ。");

            //        UpdateMainMessage("アイン：っな・・・何だよ。つい考えちまったじゃねえか・・・");

            //        UpdateMainMessage("ランディス：ラナと少し距離を置け。");

            //        UpdateMainMessage("アイン：い、いやいや、何言ってるんだよ。");

            //        UpdateMainMessage("アイン：距離感とかいう話にならない程度の距離でしか・・・");

            //        UpdateMainMessage("ランディス：聞けっつってんだ、ボケ。");

            //        UpdateMainMessage("ランディス：このダンジョン、解きてぇんだろ？");

            //        UpdateMainMessage("アイン：あ、あぁ当然！　目指すは最下層到達だ！！");

            //        UpdateMainMessage("ランディス：だったら距離を置け。");

            //        UpdateMainMessage("アイン：それのどこがダンジョン攻略に・・・");

            //        UpdateMainMessage("ランディス：以上だ。");

            //        UpdateMainMessage("ランディス：パーティに入った以上、死ぬまで鍛えてやる。");

            //        UpdateMainMessage("ランディス：覚悟しとけや、ザコアイン。");

            //        UpdateMainMessage("アイン：あ、ああ！！");

            //        UpdateMainMessage("アイン：サンキューな！　師匠！！　");
            //        CallSomeMessageWithAnimation("【オル・ランディスがパーティに加わりました。】");

            //        we.AvailableThirdCharacter = true;
            //        we.Truth_CommunicationOl22 = true;

            //        // 「コメント」初回設計で後編３人目をヴェルゼアーティでセーブしてしまっているため、
            //        // ここで再設定しなければならなくなった。
            //        tc.FullName = "オル・ランディス";
            //        tc.Name = "ランディス";
            //        tc.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
            //        tc.Agility = Database.OL_LANDIS_FIRST_AGILITY;
            //        tc.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
            //        tc.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
            //        tc.Mind = Database.OL_LANDIS_FIRST_MIND;
            //        tc.Level = 35;
            //        tc.Exp = 0;
            //        tc.BaseLife = 2080;
            //        tc.CurrentLife = tc.MaxLife;
            //        tc.BaseSkillPoint = 100;
            //        tc.CurrentSkillPoint = 100;
            //        //td.TC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
            //        tc.BaseMana = 1290;
            //        tc.CurrentMana = tc.MaxMana;
            //        tc.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
            //        tc.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
            //        tc.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
            //        tc.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
            //        tc.BattleActionCommand1 = Database.ATTACK_EN;
            //        tc.BattleActionCommand2 = Database.DEFENSE_EN;
            //        tc.BattleActionCommand3 = Database.STRAIGHT_SMASH;
            //        tc.BattleActionCommand4 = Database.VOLCANIC_WAVE;
            //        tc.BattleActionCommand5 = Database.LIFE_TAP;
            //        tc.BattleActionCommand6 = Database.SEVENTH_MAGIC;
            //        tc.BattleActionCommand7 = Database.ONE_IMMUNITY;

            //        tc.AvailableMana = true;
            //        tc.AvailableSkill = true;
            //        tc.StraightSmash = true;
            //        tc.FireBall = true;
            //        tc.DarkBlast = true;
            //        tc.DoubleSlash = true;
            //        tc.ShadowPact = true;
            //        tc.FlameAura = true;
            //        tc.StanceOfStanding = true;
            //        tc.DispelMagic = true;
            //        tc.LifeTap = true;
            //        tc.HeatBoost = true;
            //        tc.Negate = true;
            //        tc.BlackContract = true;
            //        tc.InnerInspiration = true;
            //        tc.RiseOfImage = true;
            //        tc.Deflection = true;
            //        tc.FlameStrike = true;
            //        tc.Tranquility = true;
            //        tc.VoidExtraction = true;
            //        tc.BlackFire = true;
            //        tc.Immolate = true;
            //        tc.DarkenField = true;
            //        tc.DevouringPlague = true;
            //        tc.VolcanicWave = true;
            //        tc.OneImmunity = true;
            //        tc.CircleSlash = true;
            //        tc.OuterInspiration = true;
            //        tc.SmoothingMove = true;
            //        tc.WordOfMalice = true;
            //        tc.EnrageBlast = true;
            //        tc.SwiftStep = true;
            //        tc.Recover = true;
            //        tc.SurpriseAttack = true;
            //        tc.SeventhMagic = true;
            //    }

            //    GroundOne.StopDungeonMusic();
            //    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            //    return;

            //}
            //#endregion
            //#region "条件に応じて、Duelを実施します。"
            //else if (WhoisDuelPlayer() != string.Empty && GroundOne.WE.AlreadyRest)
            //{
            //    string Opponent = WhoisDuelPlayer();
            //    GroundOne.WE.AlreadyDuelComplete = true;
            //    DuelSupportMessage(SupportType.FromDuelGate, Opponent);
            //    CallDuel(Opponent, false);
            //    return;
            //}
            //#endregion
            #region "その他"
            if (true)
            {
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
                }
                else if (current == MessagePack.ActionEvent.HomeTownShowActiveSkillSpell)
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
                else if (current == MessagePack.ActionEvent.HomeTownCallDuel)
                {
                    BattleStart(nowMessage[this.nowReading]);
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

        private void BattleStart(string playerName)
        {
            GroundOne.enemyName1 = playerName;
            GroundOne.enemyName2 = string.Empty;
            GroundOne.enemyName3 = string.Empty;
            GroundOne.StopDungeonMusic();
            System.Threading.Thread.Sleep(500);
            SceneDimension.CallTruthBattleEnemy(Database.TruthHomeTown, true, false, false, false);
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
        
        public void tapShop2()
        {
            // todo
            mainMessage.text = "ラナ：ごめんなさい、まだ準備中なのよ。";

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
                else if ((GroundOne.MC.Level >= 40) && (!GroundOne.WE.availableArchetypeCommand))
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
            if (!GrouneOne.WE.AlreadyRest)
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
            MessagePack.Message69993(ref nowMessage, ref nowEvent);
            NormalTapOK();
        }

        private void ExecItemBank()
        {
            this.Filter.SetActive();
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

                if (WhoisDuelPlayer() != string.Empty)
                {
                    DuelSupportMessage(SupportType.Begin, WhoisDuelPlayer());
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
            else
            {
                tapOK();
            }
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

        // todo
        private void DuelSupportMessage(SupportType supportType, string p)
        {
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

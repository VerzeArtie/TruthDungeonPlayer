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
        public Button btnOK;
        bool waitMessage = false;

        public GameObject systemMessagePanel;
        public Text systemMessage;
        public Camera cam;
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

        bool forceSaveCall = false; // シナリオ進行上、強制セーブした後、”休息しました”を表示したいためのフラグ
        bool duelFailCount1 = false; // 現実世界、ラナDUEL戦で敗北した時１
        bool duelFailCount2 = false; // 現実世界、ラナDUEL戦で敗北した時２
 
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
                ShownEvent();
            }
            if (this.panelMessage.gameObject.activeInHierarchy && btnOK.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    tapOK();
                }
            }
            // after revive
            //if (this.firstAction == false) {
            //    this.btnOK.gameObject.SetActive(false);
            //    if (SaveData.GetName () != "") {
            //        //debug.text += "getName not null.\r\n";
            //        if (this.nameField != null && this.inputName != null) {
            //            this.nameField.text = SaveData.GetName();
            //            this.nameField.interactable = false;
            //            this.firstAction = true;
            //        }
            //        else
            //        {
            //            Debug.Log("namefield null...");
            //        }
            //    }
            //    else {
            //        this.firstAction = true;
            //    }
            //}
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

                #region "ダンジョン階層を選択"
                int targetDungeon = 1;

                // todo
                //if (we.TruthCompleteArea1)
                //{
                //    mainMessage.Text = "アイン：さて、何階から始めるかな。";
                //    mainMessage.Update();
                //    using (SelectDungeon sd = new SelectDungeon())
                //    {
                //        sd.StartPosition = FormStartPosition.Manual;
                //        sd.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                //        //if (we.CompleteArea5) sd.MaxSelectable = 5;
                //        if (we.TruthCompleteArea4) sd.MaxSelectable = 5;
                //        else if (we.TruthCompleteArea3) sd.MaxSelectable = 4;
                //        else if (we.TruthCompleteArea2) sd.MaxSelectable = 3;
                //        else if (we.TruthCompleteArea1) sd.MaxSelectable = 2;
                //        sd.ShowDialog();
                //        this.targetDungeon = sd.TargetDungeon;
                //    }
                //}

                if (targetDungeon == 1)
                {
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
                    if (!GroundOne.WE.CompleteArea5)
                    {
                        mainMessage.text = "アイン：最下層制覇、やってみせる！";
                    }
                    else
                    {
                        mainMessage.text = "アイン：もう１度、５階でも探索するか。";
                    }
                }
                #endregion

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
            mainMessage.text = "アイン：DUEL闘技場は閉まってる。他の所へ行こう。";
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
                else
                {
                    systemMessagePanel.SetActive(false);
                    systemMessage.text = "";
                    mainMessage.text = "   " + this.nowMessage[this.nowReading];
                }

                if (current == MessagePack.ActionEvent.HomeTownBlackOut)
                {
                    BlackOut();
                }
                else if (current == MessagePack.ActionEvent.HomeTownTurnToNormal)
                {
                    TurnToNormal();
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
                else if (current == MessagePack.ActionEvent.HomeTownFazilCastle)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);
                    this.imgBackground.sprite = Resources.Load<Sprite>(Database.BACKGROUND_FAZIL_CASTLE);
                }
                else if (current == MessagePack.ActionEvent.HomeTownCallRestInn)
                {
                    CallRestInn();
                }
                else if (current == MessagePack.ActionEvent.ResurrectHalfLife)
                {
                    // todo (target is still unknown)
                    GroundOne.MC.Dead = false;
                    GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife / 2;
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
                    // todo
                    BattleStart(nowMessage[this.nowReading], true);
                    //bool failCount1 = false;
                    //bool failCount2 = false;
                    //while (true)
                    //{
                    //    // todo
                    //    bool result = true;
                    //    //bool result = BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);

                    //    //if (failCount1 && failCount2)
                    //    //{
                    //    //    using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
                    //    //    {
                    //    //        ynrw.StartPosition = FormStartPosition.CenterParent;
                    //    //        ynrw.MainMessage = "戦闘をスキップし、勝利した状態からストーリーを進めますか？\r\n戦闘スキップによるペナルティはありません。";
                    //    //        ynrw.ShowDialog();
                    //    //        if (ynrw.DialogResult == DialogResult.Yes)
                    //    //        {
                    //    //            result = true;
                    //    //        }
                    //    //    }
                    //    //}
                }
                else if (current == MessagePack.ActionEvent.HomeTownYesNoMessageDisplay)
                {
                    this.yesnoSystemMessage.text = this.nowMessage[this.nowReading];
                    this.groupYesnoSystemMessage.SetActive(true);
                }
                else if (current == MessagePack.ActionEvent.PlayMusic13)
                {
                    // todo 他の画面、他の音楽も全て横展開が必要。Methodクラスで統一すべきである。
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

        private void BattleStart(string p1, bool p2)
        {
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
                    CallEquipmentShop();
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
                    CallEquipmentShop();
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
                    CallEquipmentShop();
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
                tapOK();
            }
            #endregion
            #region "その他"
            else
            {
                SceneDimension.CallTruthEquipmentShop(Database.TruthHomeTown);
                CallEquipmentShop();
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
            CallRestInn();
        }
        
        public void tapShop2()
        {
            mainMessage.text = "ラナ：ごめんなさい、まだ準備中なのよ。";

        }
        public void tapGate()
        {
            mainMessage.text = "アイン：まだゲートは開いてないみたいだな。";
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
                if (!GroundOne.WE.AlreadyRest)
                {
                    MessagePack.Message69999(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
                else
                {
                    if (GroundOne.WE.AvailableItemBank)
                    {
                        // todo
                        //using (SelectDungeon sd = new SelectDungeon())
                        //{
                        //    sd.StartPosition = FormStartPosition.Manual;
                        //    sd.Location = new Point(this.Location.X + 350, this.Location.Y + 550);
                        //    sd.MaxSelectable = 2;
                        //    sd.FirstName = "会話";
                        //    sd.SecondName = "倉庫";
                        //    sd.ShowDialog();
                        //    if (sd.TargetDungeon == -1)
                        //    {
                        //        return;
                        //    }
                        //    else if (sd.TargetDungeon == 1)
                        //    {
                        //        mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                        //    }
                        //    else
                        //    {
                        //        UpdateMainMessage("ハンナ：荷物倉庫かい？ホラ、コッチだよ。", true);
                        //        mainMessage.Update();
                        //        System.Threading.Thread.Sleep(1000);
                        //        CallItemBank();
                        //        UpdateMainMessage("ハンナ：また用があったら寄るんだね。", true);
                        //    }
                        //}
                    }
                    else
                    {
                        MessagePack.Message69994(ref nowMessage, ref nowEvent);
                        tapOK();
                    }
                }
            }
            #endregion
        }

        private void CallRestInn()
        {
            CallRestInn(false);
        }
        private void CallRestInn(bool noAction)
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

                if (nowMessage.Count > 0)
                {
                    MessagePack.Message69995(ref nowMessage, ref nowEvent);
                }
                else
                {
                    MessagePack.Message69995(ref nowMessage, ref nowEvent);
                    NormalTapOK();
                }
            }
            if (noAction == false)
            {
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
        }
        public void tapExit()
        {
            groupYesnoSystemMessage.SetActive(true);
            // todo
            //if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            //{
            //    if (!GroundOne.WE2.AutoSaveInfo)
            //    {
            //        using (TruthPlayerInformation TPI = new TruthPlayerInformation())
            //        {
            //            TPI.StartPosition = FormStartPosition.CenterParent;
            //            TPI.SetupMessage = "ここまでの記録は自動セーブとなります。次回起動は、ここから再開となります。";
            //            TPI.ShowDialog();
            //        }
            //        GroundOne.WE2.AutoSaveInfo = true;
            //        Method.AutoSaveTruthWorldEnvironment();
            //        Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, this.knownTileInfo, this.knownTileInfo2, this.knownTileInfo3, this.knownTileInfo4, this.knownTileInfo5, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            //        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //        return;
            //    }
            //    else
            //    {
            //        Method.AutoSaveTruthWorldEnvironment();
            //        Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, this.knownTileInfo, this.knownTileInfo2, this.knownTileInfo3, this.knownTileInfo4, this.knownTileInfo5, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            //        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //        return;
            //    }
            //}

            // todo
            //using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
            //{
            //    ynrw.StartPosition = FormStartPosition.CenterParent;
            //    ynrw.MainMessage = "セーブしていない場合、現在データは破棄されます。セーブしますか？";
            //    ynrw.ShowDialog();
            //    if (ynrw.DialogResult == DialogResult.Yes)
            //    {
            //        using (ESCMenu esc = new ESCMenu())
            //        {
            //            esc.MC = this.MC;
            //            esc.SC = this.SC;
            //            esc.TC = this.TC;
            //            esc.WE = this.WE;
            //            esc.KnownTileInfo = this.knownTileInfo;
            //            esc.KnownTileInfo2 = this.knownTileInfo2;
            //            esc.KnownTileInfo3 = this.knownTileInfo3;
            //            esc.KnownTileInfo4 = this.knownTileInfo4;
            //            esc.KnownTileInfo5 = this.knownTileInfo5;
            //            esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // 後編追加
            //            esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // 後編追加
            //            esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // 後編追加
            //            esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // 後編追加
            //            esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // 後編追加                        esc.StartPosition = FormStartPosition.CenterParent;
            //            esc.StartPosition = FormStartPosition.CenterParent;
            //            esc.OnlySave = true;
            //            esc.ShowDialog();
            //        }
            //    }

            //    ynrw.MainMessage = "タイトルへ戻りますか？";
            //    ynrw.ShowDialog();
            //    if (ynrw.DialogResult == DialogResult.Yes)
            //    {
            //        this.DialogResult = DialogResult.Cancel;
            //    }
            //}

            //SceneDimension.Back();
        }

        public void CallStatusPlayer()
        {
            SceneDimension.CallTruthStatusPlayer(Database.TruthHomeTown, ref GroundOne.Player1Levelup, ref GroundOne.Player1UpPoint, ref GroundOne.Player1CumultiveLvUpValue, GroundOne.MC.PlayerStatusColor);
        }
        // todo
        private string PracticeSwordLevel(MainCharacter mainCharacter)
        {
            return "";
        }

        // todo
        private void ShowActiveSkillSpell(MainCharacter mainCharacter, string p)
        {
        }

        // todo
        private void GetItemFullCheck(MainCharacter mainCharacter, string p)
        {
        }

        // todo
        private void CallEquipmentShop()
        {
        }

        // todo
        private void DuelSupportMessage(SupportType supportType, string p)
        {
        }

        // todo
        private string WhoisDuelPlayer()
        {
            return "";
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
        // todo
        private void CallSomeMessageWithAnimation(string p)
        {
        }
    }
}

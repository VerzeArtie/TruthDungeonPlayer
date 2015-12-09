using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DungeonPlayer
{
    public class TruthHomeTown : MonoBehaviour
    {
        private int firstDay = 1;
        private WorldEnvironment we;
        public Button ok;
        public Text okText;
        bool waitMessage = false;

        public Image panelHide;
        public Image imgCharacter1;
        public Image imgCharacter2;
	    public Text debug;
	    public Button btnDungeon;
	    public Button btnGo;
	    public Button btnEquip;
	    public Button btnJobClass;
	    public Button btnCommand;
	    public Button btnShop;
	    public Button btnInn;
	    public InputField nameField;
	    public Text inputName;
	    public Text guideText;
        public Text mainMessage;
        public Image panelMessage;

	    public static int serverPort = 8001;
	    private bool firstAction = false;
	    private string targetViewName = string.Empty;

	// Use this for initialization
	void Start () {
        this.we = new WorldEnvironment();
        GroundOne.CS = new ClientSocket();
		GroundOne.InitializeNetworkConnection ();
	}
	
	string GetString(string msg, string protocolStr)
	{
		return msg.Substring(protocolStr.Length, msg.Length - protocolStr.Length);
	}

	private void ReceiveFromClientSocket(string msg)
	{
		debug.text += msg + "\r\n";
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
	void Update () {
		if (this.firstAction == false) {
            this.ok.enabled = false;
            this.ok.gameObject.SetActive(false);
            this.okText.enabled = false;
			if (SaveData.GetName () != "") {
				//debug.text += "getName not null.\r\n";
				if (this.nameField != null && this.inputName != null) {
					debug.text += "nameField not null.--> [" + SaveData.GetName() + "]\r\n";
					this.guideText.text = "Character Name";
					this.nameField.text = SaveData.GetName();
					this.nameField.interactable = false;
					this.firstAction = true;
				}
				else
				{
					debug.text += "namefield null...";
				}
			}
			else {
				this.firstAction = true;
			}
		}
	}

	private void CallNext() {
		debug.text += "namefield: " + nameField.text + "\r\n";
		if (GroundOne.IsConnect && SaveData.GetName () == "") {
			byte[] bb = System.Text.Encoding.UTF8.GetBytes (Protocol.ExistCharacter + this.nameField.text);
			GroundOne.CS.SendMessage (bb);
		} else {
			debug.text += "loadlevel call\r\n";
			Application.LoadLevel (targetViewName);
			debug.text += "loadlevel call ok\r\n";
		}
	}

	public void tapDungeon() {
		targetViewName = "TruthDungeon";
		CallNext ();
	}
    public void tapCommunicationRana()
    {
        #region "１日目"
        if (this.firstDay >= 1 && !we.Truth_CommunicationLana1)
        {
            // if (!we.AlreadyRest) // 1日目はアインが起きたばかりなので、本フラグを未使用とします。
            if (!we.AlreadyCommunicate)
            {
                this.panelMessage.enabled = true;
                this.panelMessage.gameObject.SetActive(true);
                this.mainMessage.enabled = true;
                this.mainMessage.gameObject.SetActive(true);
                this.panelHide.enabled = true;
                this.panelHide.gameObject.SetActive(true);
                this.imgCharacter2.enabled = true;
                this.imgCharacter2.gameObject.SetActive(true);
                UpdateMainMessage("ラナ：っあら、意外と早いじゃない。");

                this.imgCharacter1.enabled = true;
                this.imgCharacter1.gameObject.SetActive(true);
                UpdateMainMessage("アイン：ああ、何だか寝覚めが良いんだ。今日も調子全快だぜ！");

                UpdateMainMessage("ラナ：バカな事言ってないで、ホラホラ、朝ごはんでも食べましょ。");

                UpdateMainMessage("アイン：ああ、そうだな！じゃあ、ハンナ叔母さんとこで食べようぜ。");

                //using (MessageDisplay md = new MessageDisplay())
                //{
                //    md.StartPosition = FormStartPosition.CenterParent;
                //    md.Message = "ハンナの宿屋（料理亭）にて・・・";
                //    md.ShowDialog();
                //}

                UpdateMainMessage("アイン：っさっすが、叔母さん！今日の飯もすげえ旨いよな！");

                UpdateMainMessage("ハンナ：アッハッハ、よく元気に食べるね。まだ沢山あるからね、どんどん食べな。");

                UpdateMainMessage("ラナ：アイン、少しは控えなさいよね。恥ずかしいったら。");

                UpdateMainMessage("アイン：ああ、控えるぜ。次からな！ッハッハッハ！！！");

                UpdateMainMessage("　　　『ッドス！』（ラナのサイレントブローがアインの横腹に炸裂）　　");

                UpdateMainMessage("アイン：うおおぉぉ・・・だから食ってる時にそれをやるなって・・・");

                UpdateMainMessage("アイン：・・・ッムグ・・・ごっそうさん！っでだ、ラナ。");

                UpdateMainMessage("ラナ：え？");

                UpdateMainMessage("アイン：オレはダンジョンへ向かうぜ。");

                UpdateMainMessage("アイン：そして、その最下層へオレは辿り付いてみせる！");

                UpdateMainMessage("ラナ：っちょ、何よいきなり唐突に。");

                UpdateMainMessage("ラナ：全然脈略が無いじゃない。何よ、本当にそんなトコ行きたいわけ？");

                //if (GroundOne.WE2.TruthBadEnd1)
                //{
                //    UpdateMainMessage("アイン：まあ本当に行きたいとか言われてもなあ・・・");

                //    UpdateMainMessage("アイン：金を稼いで収支を成り立たせるってのも当然なんだが、");

                //    UpdateMainMessage("アイン：伝説のFiveSeekerに追いつきたい気持ちもあるが・・・");

                //    UpdateMainMessage("アイン：それは別として、とにかく行かなくちゃならねえ。そんな気がするんだ。");

                //    UpdateMainMessage("ラナ：ふーん、何か曖昧な答えね。");

                //    UpdateMainMessage("ラナ：まあ、分かったわよ。っじゃあ、はいコレ♪");
                //}
                //else
                {
                    UpdateMainMessage("アイン：何言ってるんだ、ラナ。俺たちの稼ぎが何なのか忘れたのか？");

                    UpdateMainMessage("アイン：俺達の収支はダンジョンで成り立ってるだろ。金を稼がないとな。");

                    UpdateMainMessage("ラナ：うん、まあそれは分かってるつもりよ。でも何で最下層に行きたがるの？");

                    UpdateMainMessage("アイン：何でかって？そりゃあ決まってるだろ！");

                    UpdateMainMessage("アイン：伝説のFiveSeeker様達に追いつくためさ！！");

                    UpdateMainMessage("ラナ：アインって昔っからFiveSeeker様の事、大好きよね。はしゃいじゃって、ッフフフ。");

                    UpdateMainMessage("アイン：何がおかしい？FiveSeekerはすべての冒険者にとっての憧れの的だろう？目標にして当然だろ。");

                    UpdateMainMessage("ラナ：分かったわよ。っじゃあ、はいコレ♪");
                }


                //using (MessageDisplay md = new MessageDisplay())
                //{
                //    md.Message = "【遠見の青水晶】を手に入れました。";
                //    md.StartPosition = FormStartPosition.CenterParent;
                //    md.ShowDialog();
                //}

                //GetItemFullCheck(mc, Database.RARE_TOOMI_BLUE_SUISYOU);

                UpdateMainMessage("アイン：お、【遠見の青水晶】じゃねえか。助かるぜ！");

                UpdateMainMessage("ラナ：無くさないでよ？それ結構レア物で値段張るものなんだから。");

                UpdateMainMessage("アイン：ん？おう、任せておけって！ッハッハッハ！！");

                //UpdateMainMessage("アイン：っと、そうだ。忘れないうちに・・・");

                //UpdateMainMessage("アイン：・・・（ごそごそ）・・・");

                //UpdateMainMessage("ラナ：何探してるのよ？");

                //UpdateMainMessage("アイン：確かポケットに入れたはず・・・");

                //using (TruthDecision td = new TruthDecision())
                //{
                //    td.MainMessage = "　【　ラナにイヤリングを渡しますか？　】";
                //    td.FirstMessage = "ラナにイヤリングを渡す。";
                //    td.SecondMessage = "ラナにイヤリングを渡さず、ポケットにしまっておく。";
                //    td.StartPosition = FormStartPosition.CenterParent;
                //    td.ShowDialog();
                //    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        UpdateMainMessage("アイン：あったあった。ラナ、こいつを渡しておくぜ。");

                //        UpdateMainMessage("ラナ：これ、私のイヤリングじゃない。どこで拾ったのよ？");

                //        UpdateMainMessage("アイン：どこって、俺の部屋に落ちてたぞ。ラナが落としていったんだろ？");

                //        UpdateMainMessage("ラナ：・・・っええ！？そそそ、そんなワケ無いじゃない！！");

                //        UpdateMainMessage("アイン：なんでそんな慌ててんだよ。まあ返しておくぜ。ッホラ！");

                //        UpdateMainMessage("ラナ：っとと、・・・アリガト♪");

                //        UpdateMainMessage("アイン：お前は変な所で抜けてるからな、しっかり持ってろよな。");

                //        UpdateMainMessage("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！");

                //        mc.DeleteBackPack(new ItemBackPack("ラナのイヤリング"));
                //        we.Truth_GiveLanaEarring = true;
                //    }
                //    else
                //    {
                //        if (GroundOne.WE2.TruthBadEnd1)
                //        {
                //            UpdateMainMessage("アイン：（・・・このイヤリング・・・）");

                //            UpdateMainMessage("アイン：（これをもってると、何か思い出せそうなんだが・・・）");

                //            UpdateMainMessage("アイン：（ラナには悪いが、もう少し持っておこう・・・）");

                //            UpdateMainMessage("アイン：いや、何でもねえんだ。");

                //            UpdateMainMessage("ラナ：今、ポケットをゴソゴソしてたじゃないの？");

                //            UpdateMainMessage("アイン：い、いやいや。何でもねえ、ッハッハッハ！");

                //            UpdateMainMessage("ラナ：何よ、あからさまに怪しかったわよ？今のは・・・");

                //            UpdateMainMessage("アイン：いざ、ダンジョン！ッハッハッハ！");
                //        }
                //        else
                //        {
                //            UpdateMainMessage("アイン：おっかしいな・・・確かにポケットに入れたはずだが・・・");

                //            UpdateMainMessage("ラナ：何か探し物でもしてるの？");

                //            UpdateMainMessage("アイン：い、いやいや。何でもねえ、ッハッハッハ！");

                //            UpdateMainMessage("ラナ：何よ、怪しいわね・・・");

                //            UpdateMainMessage("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！");
                //        }
                //    }
                //}
                we.AlreadyCommunicate = true;
            }
            else
            {
                UpdateMainMessage(MessageFormatForLana(1002), true);
            }
            we.Truth_CommunicationLana1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
        }
        #endregion
    }
	public void tapEquip() {
        SceneDimension.playbackScene.Add("TruthHomeTown");
		targetViewName = "TruthStatusPlayer";
		CallNext ();
	}
	public void tapDuel() {
        mainMessage.text = "アイン：DUEL闘技場は閉まってる。他の所へ行こう。";
        //debug.text = "tap jobclass!\r\n";
        //targetViewName = "JobClassSetting";
		CallNext ();
	}
	    public void tapCommand() {
		    targetViewName = "BattleSetting";
		    CallNext ();
	    }
        public void tapOK()
        {
            UpdateMainMessage("");
            this.waitMessage = false;
            if (GroundOne.playbackMessage.Count <= 0)
            {
                this.okText.enabled = false;
                this.okText.gameObject.SetActive(false);
                this.ok.enabled = false;
                this.ok.gameObject.SetActive(false);

                this.panelHide.enabled = false;
                this.panelHide.gameObject.SetActive(false);
                this.imgCharacter1.enabled = false;
                this.imgCharacter1.gameObject.SetActive(false);
                this.imgCharacter2.enabled = false;
                this.imgCharacter2.gameObject.SetActive(false);
            }
        }
	    public void tapShop() {
		    targetViewName = "ItemShop";
		    CallNext ();
	    }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            if (message != "")
            {
                GroundOne.playbackMessage.Insert(0, message);
                //            GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.normal);
            }
            if (this.waitMessage == false && GroundOne.playbackMessage.Count > 0)
            {
                mainMessage.text = GroundOne.playbackMessage[GroundOne.playbackMessage.Count-1];
                //mainMessage.Update();
                GroundOne.playbackMessage.RemoveAt(GroundOne.playbackMessage.Count - 1);
                if (!ignoreOK)
                {
                    this.waitMessage = true;
                    this.okText.enabled = true;
                    this.okText.gameObject.SetActive(true);
                    this.ok.enabled = true;
                    this.ok.gameObject.SetActive(true);
//                    ok.ShowDialog();
                }
            }
        }

        private string MessageFormatForLana(int num)
        {
            MainCharacter currentPlayer = new MainCharacter();
            currentPlayer.Name = "ラナ";
            switch (num)
            {
                case 1001:
                    if (!we.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1003);
                    }

                case 1002:
                if (!we.AvailableSecondCharacter)
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

    public void tapShop2()
    {
        mainMessage.text = "ラナ：ごめんなさい、まだ準備中なのよ。";

    }
    public void tapGate()
    {
        mainMessage.text = "アイン：まだゲートは開いてないみたいだな。";
    }
	public void tapInn() {
		targetViewName = "RestInn";
		CallNext ();
	}
    public void tapExit()
    {
        Application.Quit();
    }
    }
}

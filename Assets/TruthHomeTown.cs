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
        enum SupportType
        {
            Begin,
            FromDuelGate,
            FromDungeonGate,
        }

        private int firstDay = 1;
        public Button ok;
        public Text okText;
        bool waitMessage = false;

        public Button buttonShinikia;
        public Text dayLabel;
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
        GroundOne.InitializeGroundOne();
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
        if (!GroundOne.WE2.RealWorld && GroundOne.WE.GameDay <= 1 && (!GroundOne.WE.AlreadyCommunicate || !GroundOne.WE.Truth_CommunicationGanz1 || !GroundOne.WE.Truth_CommunicationHanna1 || !GroundOne.WE.Truth_CommunicationLana1))
        {
            mainMessage.text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
        }
        else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea1 && (!GroundOne.WE.Truth_CommunicationLana21 || !GroundOne.WE.Truth_CommunicationGanz21 || !GroundOne.WE.Truth_CommunicationHanna21 || !GroundOne.WE.Truth_CommunicationOl21))
        {
            mainMessage.text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
        }
        else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea2 && (!GroundOne.WE.Truth_CommunicationLana31 || !GroundOne.WE.Truth_CommunicationGanz31 || !GroundOne.WE.Truth_CommunicationHanna31 || !GroundOne.WE.Truth_CommunicationOl31 || !GroundOne.WE.Truth_CommunicationSinikia31))
        {
            mainMessage.text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
        }
        else if (!GroundOne.WE2.RealWorld && GroundOne.WE.TruthCompleteArea3 && (!GroundOne.WE.Truth_CommunicationLana41 || !GroundOne.WE.Truth_CommunicationGanz41 || !GroundOne.WE.Truth_CommunicationHanna41 || !GroundOne.WE.Truth_CommunicationOl41 || !GroundOne.WE.Truth_CommunicationSinikia41))
        {
            mainMessage.text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
        }
        else if (GroundOne.WE2.RealWorld && (!GroundOne.WE2.SeekerEvent602 || !GroundOne.WE2.SeekerEvent603 || !GroundOne.WE2.SeekerEvent604))
        {
            mainMessage.text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
        }
        else if (!GroundOne.WE.AlreadyRest)
        {
            mainMessage.text = "アイン：今出てきたばかりだぜ？一休憩させてくれ。";
        }
        else if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent602 && GroundOne.WE2.SeekerEvent603 && GroundOne.WE2.SeekerEvent604 && !GroundOne.WE2.SeekerEvent605)
            {
                UpdateMainMessage("アイン：（・・・よし・・・行くか！）");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("ラナ：ちょっと、待ちなさいよ。");

                GroundOne.PlayDungeonMusic(Database.BGM19, Database.BGM19LoopBegin);

                UpdateMainMessage("アイン：・・・ラナか・・・");

                UpdateMainMessage("ラナ：今からダンジョンに向かう気よね？");

                UpdateMainMessage("アイン：ああ、そのつもりだ。");

                UpdateMainMessage("ラナ：・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("ラナ：【　真実の答え　】・・・見つかった？");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：ああ、見つかってる。");

                UpdateMainMessage("ラナ：今言ってみて、聞いてあげるから。");

                UpdateMainMessage("アイン：わかった。");

                UpdateMainMessage("アイン：【力は力にあらず、力は全てである。】");

                UpdateMainMessage("アイン：【負けられない勝負。　しかし心は満たず。】");

                UpdateMainMessage("アイン：【力のみに依存するな。心を対にせよ。】");

                UpdateMainMessage("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだ。");

                UpdateMainMessage("ラナ：そっか・・・ちゃんと覚えてたのね。");

                UpdateMainMessage("アイン：ああ。あの時は分からなかったが、今、ようやく分かり始めたんだ。");

                UpdateMainMessage("アイン：力だけじゃ限界がある、それだけじゃダメなんだ。");

                UpdateMainMessage("アイン：でもだからと言って、信念や想いだけを持ってても駄目だ。");

                UpdateMainMessage("アイン：両方とも併せもって初めて意味が出てくる。");

                UpdateMainMessage("アイン：そんな感じだ。");

                UpdateMainMessage("ラナ：そう・・・私にはよく分からないけど");

                UpdateMainMessage("ラナ：アインが感じた今の答えが真実なのね、きっと。");

                UpdateMainMessage("アイン：それを教えてくれたのが、この剣だ。");

                UpdateMainMessage("ラナ：その練習用の剣？　小さい頃母さんからもらったやつよね。");

                UpdateMainMessage("アイン：ああ、そうだ。");

                UpdateMainMessage("アイン：これが神剣フェルトゥーシュだと知るまでにはずいぶんと時間がかかった。");

                UpdateMainMessage("アイン：あの頃は、どうみても単なるナマクラの剣にしか見えなかったからな。");

                UpdateMainMessage("ラナ：・・・いつ頃から気づいてたのよ？");

                UpdateMainMessage("アイン：ボケ師匠ランディスに出くわした時だ。");

                UpdateMainMessage("ラナ：そうだったの。それからは、気づかない振りしてたの？");

                UpdateMainMessage("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。");

                UpdateMainMessage("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。");

                UpdateMainMessage("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？");

                UpdateMainMessage("アイン：師匠はどうもあの剣の特性に関して、もう一つ何か知ってるみたいだったんだ。");

                UpdateMainMessage("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。");

                UpdateMainMessage("アイン：心を燈して放たないと、威力は発揮されない。何かそんな話だった。");

                UpdateMainMessage("ラナ：心を燈して・・・って事は。");

                UpdateMainMessage("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。");

                UpdateMainMessage("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。");

                UpdateMainMessage("アイン：それが分かった時点で、俺の力に対する考えは変わった。");

                UpdateMainMessage("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。");

                UpdateMainMessage("ラナ：うん・・・");

                UpdateMainMessage("ラナ：アインって・・・凄いわね。");

                UpdateMainMessage("アイン：な、いやいや、凄くなんかねえって。");

                UpdateMainMessage("ラナ：ううん、そういう風に考えが行き届くのは凄いわよ。私じゃ考えもつかないもの。");

                UpdateMainMessage("アイン：いや、俺の勝手な解釈だからな。間違ってる可能性の方が高いぞ。");

                UpdateMainMessage("ラナ：ううん、解釈が間違ってるとかそういう話じゃないの。");

                UpdateMainMessage("ラナ：アインの雰囲気そのものが、凄く変わるのよ。");

                UpdateMainMessage("ラナ：凄く冷静で・・・的を得ていて・・・");

                UpdateMainMessage("ラナ：いつものアインじゃないみたい。");

                UpdateMainMessage("アイン：ま・・・");

                UpdateMainMessage("アイン：まあ、そういう側面もあるさ！　ッハッハッハ！");

                UpdateMainMessage("ラナ：良いのよ、無理して雰囲気変えなくて、ッフフ♪");

                UpdateMainMessage("アイン：わ、悪いな・・・");

                UpdateMainMessage("ラナ：ッフフ、良いって言ってるじゃないの♪");

                UpdateMainMessage("ラナ：でも、ついでに言わせてもらうわね。");

                UpdateMainMessage("アイン：な、なんだ？");

                UpdateMainMessage("ラナ：アイン、あんた私に手加減してるでしょ？");

                UpdateMainMessage("アイン：手加減？？　一体何の話だ。");

                UpdateMainMessage("ラナ：戦闘スタイルの事よ。");

                UpdateMainMessage("アイン：戦闘・・・スタイル？");

                UpdateMainMessage("ラナ：そうよ。私レベルが相手ならバレないとでも思ってたのかしら。");

                UpdateMainMessage("アイン：いや、俺は手加減なんてしてないぞ。気のせいじゃないのか？");

                UpdateMainMessage("ラナ：見たわよ、アンタが傭兵訓練所を卒業した後、コッソリ独自で練習している所。");

                UpdateMainMessage("ラナ：あんな動き・・・見た事もないスピードだったわ。");

                UpdateMainMessage("アイン：ま、待て。あれはだな・・・");

                UpdateMainMessage("ラナ：いいのよ。私じゃ正直、追いつけないレベルだった。");

                UpdateMainMessage("ラナ：動作切替タイミング、詠唱速度、剣を振るう速度。");

                UpdateMainMessage("ラナ：全てが別次元だったわ。");

                UpdateMainMessage("ラナ：どうして・・・私に見せてくれないのかしら？");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：すまねえ。");

                UpdateMainMessage("ラナ：謝らないでよ・・・どうなの？本当の所を教えてちょうだいよ。");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("ラナ：やっぱり・・・そういう事よね。");

                UpdateMainMessage("アイン：まて、そうじゃねえんだ！");

                UpdateMainMessage("アイン：俺が悪いのは本当だ。");

                UpdateMainMessage("アイン：ラナ、お前にだけはそういうとこを見せたくなかったんだ。");

                UpdateMainMessage("アイン：知られたく・・・なかったんだ。");

                UpdateMainMessage("アイン：お前がもし、俺のそういう側面を知ってしまえば・・・");

                UpdateMainMessage("アイン：俺の前から・・・居なくなるんじゃないかって・・・");

                UpdateMainMessage("ラナ：力量に差が出てきたら、私がアインから離れていく。そう考えたって事？");

                UpdateMainMessage("アイン：・・・ああ・・・");

                UpdateMainMessage("ラナ：・・・　・・・");

                UpdateMainMessage("ラナ：ッフ、ッフフ♪　なーに言ってんのかしら、バッカじゃないのアンタ！？");

                UpdateMainMessage("ラナ：力量なんて・・・そもそもアンタに私が追いつけるワケないでしょ！？");

                UpdateMainMessage("アイン：ラ、ラナ・・・");

                UpdateMainMessage("ラナ：何よそれ・・・失礼しちゃうわよホント。アンタの実力ってどんだけなのよ本当。");

                UpdateMainMessage("ラナ：隠すとか隠さないとか・・・くだらない事ばっかり考えて・・・");

                UpdateMainMessage("ラナ：隠さなきゃいけないレベルになっちゃってる、そう言いたいわけ！？");

                UpdateMainMessage("アイン：うっ・・・");

                UpdateMainMessage("ラナ：あの練習内容の異次元みたいなスピードから察するに、そういう事よね！？");

                UpdateMainMessage("ラナ：私なんかじゃ。。。絶対にあんなの出来っこないもん。。。");

                UpdateMainMessage("アイン：いや、今は出来なくとも・・・");

                UpdateMainMessage("ラナ：そんな風に気を使わないで。　私、自分の事は分かってるつもりだから。");

                UpdateMainMessage("ラナ：・・・　・・・");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("ラナ：ッフフ・・・おかしいわね。昔の小さい頃のアインってさ、すごく弱かったし。");

                UpdateMainMessage("ラナ：いっつも泣いてばっかり。で、私がいっつも守ってあげてたのに・・・");

                UpdateMainMessage("ラナ：いつの間にそんなに腕を上げちゃってたのかしら、信じられないわ本当。");

                UpdateMainMessage("アイン：ッハハ・・・あったな、そういやそんな事も・・・");

                UpdateMainMessage("ラナ：・・・　・・・");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("ラナ：いいわ、アイン。");

                UpdateMainMessage("ラナ：アインが私に対して、変に気を使ってた事は許してあげる。");

                UpdateMainMessage("アイン：わ・・・悪かったな、マジで。");

                UpdateMainMessage("アイン：これからは・・・そうだな、あまり気を使わずに・・・。");

                UpdateMainMessage("ラナ：あっ、そぉーーーだ！！！");

                UpdateMainMessage("アイン：うお！？なんだいきなり！？");

                UpdateMainMessage("ラナ：今、良い事思いついちゃった♪");

                UpdateMainMessage("ラナ：バカアイン、今から言うのは命令よ。ちゃんと聞きなさいよね。");

                UpdateMainMessage("アイン：な、何だ？");

                UpdateMainMessage("ラナ：私、今ここでアインにDUEL決闘を申し込むわ。");

                UpdateMainMessage("アイン：な！！！");

                UpdateMainMessage("ラナ：で、条件を一つ付け加えるわ。聞きなさい。");

                UpdateMainMessage("アイン：な、何だその条件ってのは？");

                UpdateMainMessage("ラナ：あんた今度こそ本当に、今この場で手加減せずに私に挑んでもらうわよ。");

                UpdateMainMessage("ラナ：それが絶対の条件よ。どう？");

                UpdateMainMessage("アイン：っぐ・・・");

                UpdateMainMessage("アイン：もし万が一手加減してたら・・・どうなる？");

                UpdateMainMessage("ラナ：その時は、私はアンタともうコンビは組まないわ。");

                UpdateMainMessage("ラナ：手加減されてまで一緒に居たくないから。");

                UpdateMainMessage("アイン：・・・分かった。");

                UpdateMainMessage("アイン：この一戦、絶対に手加減はしねえ。約束だ！");

                UpdateMainMessage("アイン：・・・あっ！ま、まてよ！？");

                UpdateMainMessage("アイン：万が一それで、俺が勝ってしまったらどうなるんだ？");

                UpdateMainMessage("アイン：やっぱり・・・その時も・・・");

                UpdateMainMessage("ラナ：・・・ップ");

                UpdateMainMessage("ラナ：ッフフフ、アーッハハハハ♪");

                UpdateMainMessage("ラナ：何そんな心配してんのよ、大丈夫よ♪");

                UpdateMainMessage("ラナ：手加減してない本気のアンタを見たいだけよ。");

                UpdateMainMessage("ラナ：アンタ基本的に勝って当然なんだから、またクダラナイ事考えないでよねホント♪");

                UpdateMainMessage("ラナ：（　どっちにしろ・・・本当に離れたりするわけ・・・　）");

                UpdateMainMessage("アイン：えっ・・・？");

                UpdateMainMessage("ラナ：ホーラホラホラホラ、じゃあ行くわよ。ちゃんと構えなさいよね♪");

                UpdateMainMessage("アイン：あ、ああ。ちょっと待ってくれな。");

                UpdateMainMessage("アイン：・・・よし、ＯＫだ。");

                UpdateMainMessage("ラナ：私も良いわよ♪");

                UpdateMainMessage("アイン：じゃあ、正真正銘の本気だ。手加減抜きで行くぜ！");

                UpdateMainMessage("ラナ：始めるわよ、３");

                UpdateMainMessage("アイン：２");

                UpdateMainMessage("ラナ：１");

                UpdateMainMessage("アイン：０！！");

                bool failCount1 = false;
                bool failCount2 = false;
                while (true)
                {
                    // todo
                    bool result = true;
                    //bool result = BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);

                    //if (failCount1 && failCount2)
                    //{
                    //    using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
                    //    {
                    //        ynrw.StartPosition = FormStartPosition.CenterParent;
                    //        ynrw.MainMessage = "戦闘をスキップし、勝利した状態からストーリーを進めますか？\r\n戦闘スキップによるペナルティはありません。";
                    //        ynrw.ShowDialog();
                    //        if (ynrw.DialogResult == DialogResult.Yes)
                    //        {
                    //            result = true;
                    //        }
                    //    }
                    //}

                    if (result)
                    {
                        // 勝ち
                        UpdateMainMessage("ラナ：ッキャ！！");

                        UpdateMainMessage("アイン：しまっっ！！　大丈夫か、ラナ！？");

                        UpdateMainMessage("ラナ：いっつつつ・・・大丈夫よ、少し打っただけだから。");

                        UpdateMainMessage("アイン：け、怪我とかしてねえか？大丈夫なのか？痛い所はないか！？");

                        UpdateMainMessage("ラナ：だーいじょーぶだって言ってるでしょーが。ホラホラ元気よ♪");

                        UpdateMainMessage("アイン：よ・・・良かった。本当に大丈夫だな？");

                        UpdateMainMessage("ラナ：しつこいわね。蹴りかえすわよ。");

                        UpdateMainMessage("アイン：わわわ、わかった。");

                        UpdateMainMessage("ラナ：で・・・手加減はしてないわよね？");

                        UpdateMainMessage("アイン：もちろんさ！　俺の得意戦術をそのまま使ったからな！");

                        UpdateMainMessage("ラナ：でも、まさかあんなタイミングから入れてくるとは思わなかったわ。");

                        UpdateMainMessage("ラナ：アインってさ、どこでそういうの覚えてきてるの？");

                        UpdateMainMessage("アイン：どこって言われてもな・・・師匠とやってるうちに自然と・・・かな。");

                        UpdateMainMessage("ラナ：ふ～ん・・・やっぱりランディスのお師匠さんが影響してるわけね。");

                        UpdateMainMessage("アイン：あとは・・・自分なりに、コソコソっとだな・・・");

                        UpdateMainMessage("アイン：他にはDUEL闘技場を観察してて、自分にはないトコを観察かな。");

                        UpdateMainMessage("アイン：傭兵訓練時代の基礎訓練項目もたまに読み返して反復練習はしてる。");

                        UpdateMainMessage("アイン：モンスター狩りの時も、普段使わない新戦術を取り入れてみたり。");

                        UpdateMainMessage("アイン：あとは・・・");

                        UpdateMainMessage("ラナ：あ～あ、もうイイ！　私の負け！！");

                        UpdateMainMessage("アイン：うわっ、すまねえ、悪かったって。");

                        UpdateMainMessage("ラナ：ううん、良いの。本気を見せてくれたんだし、スッキリしたわ♪");

                        UpdateMainMessage("アイン：ハッ・・・ハハハ・・・");

                        UpdateMainMessage("ラナ：ダンジョン、私を誘わないで一人でいくつもりなんでしょ？");

                        UpdateMainMessage("アイン：うっ・・・");

                        UpdateMainMessage("ラナ：バカアインは嘘作りが下手くそすぎなのよ。そんなのお見通しよ。");

                        UpdateMainMessage("アイン：ハハハ・・・まあ・・・");

                        UpdateMainMessage("アイン：嘘というか、正直パーティに誘うつもりはあった。");

                        UpdateMainMessage("アイン：これは本当だ。");

                        UpdateMainMessage("ラナ：・・・　・・・");

                        UpdateMainMessage("アイン：でも、それじゃ・・・駄目みたいなんだ。");

                        UpdateMainMessage("アイン：俺は・・・");

                        UpdateMainMessage("アイン：ここを抜けださなきゃならないんだ。");

                        UpdateMainMessage("ラナ：何とか・・・辿りつけそうなの？");

                        UpdateMainMessage("アイン：ああ、お前のイヤリングもホラ。");

                        UpdateMainMessage("ラナ：あっ・・・");

                        UpdateMainMessage("アイン：今は、俺が手にしたままの状態だ。");

                        UpdateMainMessage("アイン：・・・分かったんだ、どうしなければいけないか。");

                        UpdateMainMessage("ラナ：・・・");

                        UpdateMainMessage("ラナ：ありがと。こんな所まで頑張ってくれて。");

                        UpdateMainMessage("アイン：バカ言うな。俺自身の問題だ。");

                        UpdateMainMessage("アイン：絶対に何とかしてやる。任せろ。");

                        UpdateMainMessage("ラナ：うん、お願い。期待してるから♪");

                        UpdateMainMessage("アイン：じゃあな、行ってくるぜ！！");

                        break;
                    }
                    else
                    {
                        // todo
                        //using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                        {
                            UpdateMainMessage("アイン：ッグ・・！！");
                            if (!failCount1)
                            {
                                failCount1 = true;

                                UpdateMainMessage("ラナ：今ので当たるなんて、アインらしくないわね。");

                                UpdateMainMessage("アイン：ックソ・・・避けたつもりだったんだがな。");

                                UpdateMainMessage("ラナ：今のアイン・・・やっぱり動きが鈍ってるわよ。");

                                UpdateMainMessage("ラナ：見せてちょうだいよ、本当の動きを。");

                                UpdateMainMessage("アイン：あ、ああ。今度こそ！");
                            }
                            else if (!failCount2)
                            {
                                failCount2 = true;
                                UpdateMainMessage("ラナ：手加減が身体に染み込んでいるみたいね。動きが遅かったわよ。");

                                UpdateMainMessage("アイン：ラナ相手だと・・・動きが縮こまってるのか・・・");

                                UpdateMainMessage("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。");

                                UpdateMainMessage("アイン：ああ、今度こそ！");
                            }
                            else
                            {
                                UpdateMainMessage("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。");

                                UpdateMainMessage("アイン：っく、今度こそ！");
                            }
                        }
                    }
                }

                // todo
                //using (MessageDisplay md = new MessageDisplay())
                //{
                //    md.StartPosition = FormStartPosition.CenterParent;
                //    md.Message = "ダンジョンゲートの入り口にて";
                //    md.ShowDialog();
                //}

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("アイン：（・・・ダンジョンへ・・・俺は向かう・・・）");

                UpdateMainMessage("アイン：（ラナのイヤリングは手にしたままだ）");

                UpdateMainMessage("アイン：（俺はこれの意味を知っている）");

                UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                UpdateMainMessage("アイン：（行こう、ダンジョンへ）");

                // todo
                //this.targetDungeon = 1;
                //GroundOne.WE2.RealDungeonArea = 1;
                //GroundOne.WE2.SeekerEvent605 = true;
                //Method.AutoSaveTruthWorldEnvironment();
                //Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        else
        {
            // todo
            SceneDimension.Go(Database.TruthHomeTown, Database.TruthDungeon);
        }
	}
    public void tapCommunicationRana()
    {
        #region "１日目"
        if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationLana1)
        {
            // if (!we.AlreadyRest) // 1日目はアインが起きたばかりなので、本フラグを未使用とします。
            if (!GroundOne.WE.AlreadyCommunicate)
            {
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
                GroundOne.WE.AlreadyCommunicate = true;
            }
            else
            {
                UpdateMainMessage(MessageFormatForLana(1002), true);
            }
            GroundOne.WE.Truth_CommunicationLana1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
        }
        #endregion
    }

	public void tapDuel() {
        mainMessage.text = "アイン：DUEL闘技場は閉まってる。他の所へ行こう。";
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

                this.imgCharacter1.enabled = false;
                this.imgCharacter1.gameObject.SetActive(false);
                this.imgCharacter2.enabled = false;
                this.imgCharacter2.gameObject.SetActive(false);
            }
        }
	    public void tapShop() {
            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailableEquipShop2 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailableEquipShop3 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailableEquipShop4 = true; // 前編で既に周知のため、解説は不要。
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailableEquipShop5 = true; // 前編で既に周知のため、解説は不要。


            #region "現実世界"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("アイン：ガンツ叔父さん、いますかー？");

                UpdateMainMessage("ガンツ：アインか。よく来てくれた。");

                UpdateMainMessage("アイン：武具店、開いてますか？");

                UpdateMainMessage("ガンツ：ああ、開店しておるので見ていくと良い。");

                UpdateMainMessage("アイン：っしゃ！やったぜ！じゃあ早速見せてもらうとするぜ！！");

                UpdateMainMessage("ガンツ：好きなだけ見ていくと良い。");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("ガンツ：アインよ。これからダンジョンへ向かうのだな？");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。");

                // todo
                //using (MessageDisplay md = new MessageDisplay())
                //{
                //    md.StartPosition = FormStartPosition.CenterParent;
                //    md.Message = "ガンツは両眼を閉じた状態で、誰へともなく、空中へ語り始めた。";
                //    md.ShowDialog();
                //}

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：お前はものすごいモノを秘めている。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：お前は間違いなく打ちのめされる。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：途中、決してくじけてはならん。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：どうしてもしんどい時は一旦休みなさい。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：お前ならきっと叶えられるはずだ。");

                UpdateMainMessage("ガンツ：精進しなさい。アイン。");

                UpdateMainMessage("　　　　『ガンツは両目を開き、テーブルへ眼を戻した』");

                UpdateMainMessage("アイン：ッハイ！！");

                UpdateMainMessage("ガンツ：心を決めたようだな。良い雰囲気だ。");

                UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                UpdateMainMessage("アイン：ッハイ！！！");

                GroundOne.WE2.SeekerEvent604 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("ガンツ：アインよ、精進しなさい。", true);
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
            {
                if (!GroundOne.WE.MeetOlLandisBeforeGanz)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アインよ。");

                    UpdateMainMessage("アイン：は、はい。なんでしょう？");

                    UpdateMainMessage("ガンツ：オルが挨拶に来ておったぞ。");

                    UpdateMainMessage("アイン：は、ハハハ・・・そうでしたか。そいつは良かったですね。");

                    UpdateMainMessage("ガンツ：アインよ。");

                    UpdateMainMessage("アイン：は、はいハイ！");

                    UpdateMainMessage("ガンツ：精進しなさい。");

                    UpdateMainMessage("アイン：ハイ！！！じゃあ、これで失礼いたします。");

                    UpdateMainMessage("　　（ッバタン・・・）");

                    UpdateMainMessage("アイン：（はあ・・・先回りされてるじゃねえか・・・）");

                    UpdateMainMessage("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）", true);
                    GroundOne.WE.MeetOlLandisBeforeGanz = true;
                }
                else
                {
                    UpdateMainMessage("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）", true);
                }
                return;
            }
            #endregion
            #region "複合魔法・スキルを教えてもらうイベント"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.AvailableMixSpellSkill && GroundOne.MC.Level >= 21)
            {
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    GroundOne.WE.AlreadyEquipShop = true;

                    UpdateMainMessage("アイン：どうも、こんちわー");

                    UpdateMainMessage("ガンツ：アインよ、ちょっとこちらへ来なさい。");

                    UpdateMainMessage("アイン：ッゲ、何でしょう？");

                    UpdateMainMessage("ガンツ：心配はいらん。少しの間だけだ。");

                    UpdateMainMessage("アイン：はい。それじゃ・・・");

                    UpdateMainMessage("アイン：（あれ、この道って、ダンジョンゲートへ行くつもりか？）");

                    // todo
                    //using (MessageDisplay md = new MessageDisplay())
                    //{
                    //    md.StartPosition = FormStartPosition.CenterParent;
                    //    md.Message = "ダンジョンゲート裏の広場にて";
                    //    md.ShowDialog();
                    //}

                    UpdateMainMessage("ガンツ：着いたな。");

                    UpdateMainMessage("アイン：えっと、すいません質問なんですけど？");

                    UpdateMainMessage("ガンツ：なんだね。");

                    UpdateMainMessage("アイン：こんな裏広場まで来て一体何を？");

                    UpdateMainMessage("ガンツ：・・・アインよ、こちらへ来てみなさい。");

                    UpdateMainMessage("アイン：ん？これは・・・変な円紋が・・・");

                    UpdateMainMessage("ガンツ：空間転移装置、聞いた事ぐらいはあるだろう。");

                    UpdateMainMessage("アイン：マジっすか！？これが・・・へえええぇぇぇ！！");

                    UpdateMainMessage("アイン：え？　ってかどこかに行くつもりなんですか？");

                    UpdateMainMessage("　　ドン！！ （アインは突然突き飛ばされた）");

                    UpdateMainMessage("アイン：あ！っちょ！！　っちょちょ！！");

                    UpdateMainMessage("ガンツ：アインよ、精進なさい。");

                    UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                    // todo
                    //using (MessageDisplay md = new MessageDisplay())
                    //{
                    //    md.StartPosition = FormStartPosition.CenterParent;
                    //    md.Message = "アインは別の場所へと飛ばされてしまった。";
                    //    md.ShowDialog();
                    //}

                    // todo
                    //this.buttonHanna.Visible = false;
                    //this.buttonDungeon.Visible = false;
                    //this.buttonRana.Visible = false;
                    //this.buttonGanz.Visible = false;
                    //this.buttonPotion.Visible = false;
                    //this.buttonDuel.Visible = false;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
                    //this.Invalidate();

                    UpdateMainMessage("アイン：っいで！");

                    UpdateMainMessage("アイン：ッテテテ・・・体勢が悪かったせいか、放り出されちまった。");

                    UpdateMainMessage("アイン：ったく・・・行き先ぐらい教えてくれてもいいのに。");

                    UpdateMainMessage("？？？：貴君が、アイン・ウォーレンスか？");

                    UpdateMainMessage("アイン：え？あ、あぁそうだが・・・");

                    UpdateMainMessage("アイン：って、おわぁぁ！！");

                    UpdateMainMessage("　　　『相手の顔の右目がギョロっと動いている』");

                    UpdateMainMessage("アイン：ビックリしたなあ・・・擬眼かよ");

                    UpdateMainMessage("アイン：えっと・・・");

                    UpdateMainMessage("？？？：どれ、少し見せてもらうぞ。");

                    UpdateMainMessage("　　　『擬眼がギョロリと動きはじめた！』");

                    UpdateMainMessage("アイン：何かこええなあ・・・");

                    UpdateMainMessage("？？？：・・・　・・・　・・・");

                    UpdateMainMessage("？？？：スペル属性『聖  火  理』　それに");

                    UpdateMainMessage("？？？：スキル属性『動  剛  心眼』か。　なるほど。");

                    UpdateMainMessage("アイン：なっ！！");

                    UpdateMainMessage("？？？：大胆な攻撃スタイル、それに繊細な戦術をいくつか。");

                    UpdateMainMessage("？？？：挑発には意図的に挑む方だが、肝心な面はいつも冷静");

                    UpdateMainMessage("？？？：物理攻撃だけでなく、魔法にも長ける。全体的なオールラウンダー");

                    UpdateMainMessage("？？？：直感で『決まり』と判断すれば、一気に仕掛けるタイプ。");

                    UpdateMainMessage("？？？：ッフハハ、面白い。　ランディスもああ見えて教え好きだ。");

                    UpdateMainMessage("アイン：し、師匠を知ってるのか？");

                    UpdateMainMessage("　　　『擬眼が更にギョロリと動いた！』");

                    UpdateMainMessage("？？？：しかし、敵を全力で潰しにいかず、様子見の面が強い。");

                    UpdateMainMessage("？？？：一人で次々と倒そうとするより、チーム連携を考慮して動くタイプ。");

                    UpdateMainMessage("？？？：本来なら一人で出来る素質もあるが、表には決して見せない。");

                    UpdateMainMessage("？？？：なるほど、何か特定の事柄を意識しているな。");

                    UpdateMainMessage("？？？：この手抜き加減・・・驕りではなく、無意識的にかあるいは。");

                    UpdateMainMessage("？？？：たしかに、このままでは致命的な敗北は間逃れんな。");

                    UpdateMainMessage("アイン：い、いやいやいや・・・");

                    UpdateMainMessage("アイン：（ってか、さっきから妙にこの感覚・・・）");

                    UpdateMainMessage("　　【【【　アインは背筋に異常な恐怖感を覚えている。　】】】");

                    UpdateMainMessage("アイン：（あのボケ師匠じゃねえけど、ちょっと違った怖さがある・・・）");

                    UpdateMainMessage("アイン：（支配、制圧・・・そんな感じか）");

                    UpdateMainMessage("アイン：あんた、一体誰なんだよ？");

                    UpdateMainMessage("？？？：この距離感を一定に保つあたり、警戒心は貴君なりに最大というわけだな。");

                    UpdateMainMessage("？？？：だが我の射程、貴君の想像を遥かに超えている。");

                    UpdateMainMessage("アイン：っな！！！");

                    UpdateMainMessage("　　【【【　アインは膨大な汗を体中に感じた。　】】】");

                    UpdateMainMessage("アイン：（ッヤ、ヤベェ・・・何かヤベェ・・・！！！）");

                    UpdateMainMessage("アイン：って、ってか、そろそろ正体を教えろよ！？");

                    UpdateMainMessage("アイン：アンタ、敵じゃないんだろ！？");

                    UpdateMainMessage("？？？：この辺が頃合いか。逃げられても困る。");

                    UpdateMainMessage("？？？：我の名は『シニキア・カールハンツ』である。");

                    UpdateMainMessage("　　『アインは汗がスゥっと引いていくのを感じた。』");

                    UpdateMainMessage("アイン：（ッホ・・・なんだったんだ今のは・・・）");

                    UpdateMainMessage("アイン：・・・はじめまして、アインと言います。");

                    UpdateMainMessage("アイン：って、シニキアってまさか、伝説のFiveSeeker！！！");

                    UpdateMainMessage("カール：伝説のFiveSeekerなどという恥ずかしい通り名は止めてもらおう。");

                    UpdateMainMessage("カール：我はカールとでも呼べばよい。");

                    UpdateMainMessage("アイン：はい・・・えっと、じゃあのカールさん。");

                    UpdateMainMessage("アイン：ガンツ叔父さんは何でここへ俺を？");

                    UpdateMainMessage("カール：貴君を鍛えるよう言われている。");

                    UpdateMainMessage("アイン：俺をですか？");

                    UpdateMainMessage("カール：そうだ。");

                    UpdateMainMessage("カール：我の場合、鍛える方法は戦闘訓練ではない。");

                    UpdateMainMessage("カール：行動よりもまず理論。");

                    UpdateMainMessage("カール：貴君にはそれが欠けている。");

                    UpdateMainMessage("アイン：えっと・・・具体的には何を？");

                    UpdateMainMessage("カール：我の言う事、すべてを記憶せよ。");

                    UpdateMainMessage("アイン：記憶！？　暗記しろって事ですか！？");

                    UpdateMainMessage("カール：そうだ。では行くぞ。");

                    UpdateMainMessage("　　　『カールの講義が延々と小一時間続いたのち・・・』");

                    UpdateMainMessage("カール：複合魔法の基礎に関しては、以上だ。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("　　　ッバタ・・・（アインはその場で静かに落ちた）");

                    // todo
                    //using (MessageDisplay md = new MessageDisplay())
                    //{
                    //    md.StartPosition = FormStartPosition.CenterParent;
                    //    md.Message = "アインは複合魔法・スキルの基礎を習得した！";
                    //    md.ShowDialog();
                    //}
                    GroundOne.WE.AvailableMixSpellSkill = true;
                    GroundOne.WE2.AvailableMixSpellSkill = true;

                    UpdateMainMessage("カール：どうした。まだまだ先は長いぞ。");

                    UpdateMainMessage("アイン：無理・・・こういうのは駄目だ・・・");

                    UpdateMainMessage("アイン：なあ、ちょっとでも良いからよ。実践で教えてくれよ？");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：要は、聖と火を組み合わせるって事なんだろ？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：具体的に一回だけ見せてくれよ・・・");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：火と聖って相性が良いって事なんだろ？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：聖と闇は反対・・・みたいな？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：カール先生、一回だけ頼むぜ！");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：トホホ・・・");

                    UpdateMainMessage("カール：心配か？");

                    UpdateMainMessage("アイン：え？そりゃまあ、やってみた方が覚えるのも早いし");

                    UpdateMainMessage("カール：イメージの基本は、習得した知識から来る。");

                    UpdateMainMessage("カール：具現化の展開は、それぞれの知恵から派生して成る。");

                    UpdateMainMessage("アイン：ん、ま、まあなんとなくその辺は・・・");

                    UpdateMainMessage("カール：心配するな。貴君はすでに習得したも同然だ。");

                    UpdateMainMessage("アイン：え！？　そんな、１回も確認してないですけど？");

                    UpdateMainMessage("カール：誰に教えを乞うたと思っておる。我を愚弄するか？");

                    UpdateMainMessage("アイン：い、いやいや！そんなつもりじゃございません！！");

                    UpdateMainMessage("カール：まあよい。空間転移装置を復活させておいた。帰るが良い。");

                    UpdateMainMessage("アイン：ハイ・・・どうもありがとうございました。");

                    UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                    // todo
                    //using (MessageDisplay md = new MessageDisplay())
                    //{
                    //    md.StartPosition = FormStartPosition.CenterParent;
                    //    md.Message = "アインはダンジョンゲートの裏広場に戻ってきた";
                    //    md.ShowDialog();
                    //}

                    if (!GroundOne.WE.AlreadyRest)
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                    }
                    else
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    }
                    // todo
                    //this.buttonHanna.Visible = true;
                    //this.buttonDungeon.Visible = true;
                    //this.buttonRana.Visible = true;
                    //this.buttonGanz.Visible = true;
                    //this.buttonPotion.Visible = true;
                    //this.buttonDuel.Visible = true;


                    UpdateMainMessage("ガンツ：どうだったかね。");

                    UpdateMainMessage("アイン：どうも何も・・・すげえ疲れました。");

                    UpdateMainMessage("ガンツ：そうかね。では戻るとしよう。");

                    UpdateMainMessage("アイン：はい・・・");

                    UpdateMainMessage("アイン：（ガンツ叔父さんもこう見えて、強引だよな・・・）");

                    // todo
                    //using (MessageDisplay md = new MessageDisplay())
                    //{
                    //    md.StartPosition = FormStartPosition.CenterParent;
                    //    md.Message = "アイン達はガンツの武具屋まで戻ってきた";
                    //    md.ShowDialog();
                    //}

                    UpdateMainMessage("ガンツ：では、ワシはこれで。");

                    UpdateMainMessage("アイン：おじさん、ちょっと質問が");

                    UpdateMainMessage("ガンツ：何だね。");

                    UpdateMainMessage("アイン：あの転移された場所ってどこら辺なんですか？");

                    UpdateMainMessage("ガンツ：それはカール爵の希望により答えられん。");

                    UpdateMainMessage("アイン：そうなのか・・・いや、何か見たことある場所な気もしたんで");

                    UpdateMainMessage("ガンツ：なに、お主も良く知っておる場所よ。");

                    UpdateMainMessage("アイン：そうなんですか？　う～ん・・・");

                    UpdateMainMessage("アイン：まあ、良いや。おじさん、ありがとうございました！");

                    UpdateMainMessage("ガンツ：うむ、精進せいよ。");

                    UpdateMainMessage("　　　『ガンツは店の中へと戻っていった・・・』");

                    UpdateMainMessage("アイン：何かグダグダに疲れた気もするが・・・");

                    UpdateMainMessage("アイン：複合か・・・俺にも出来るようになるといいな");


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

                    if ((GroundOne.MC.Level >= 21) && (!GroundOne.MC.FlashBlaze))
                    {
                        UpdateMainMessage("アイン：どうも、こんちわー");

                        UpdateMainMessage("ガンツ：アインよ、ちょっとこちらへ。");

                        UpdateMainMessage("アイン：あ、はい何でしょう？");

                        UpdateMainMessage("ガンツ：以前から見て、また少し強くなったと見えるな。");

                        UpdateMainMessage("アイン：いやいや、それほどでもありませんが・・・");

                        UpdateMainMessage("ガンツ：カール爵の所へ行って来なさい。");

                        UpdateMainMessage("アイン：ッゲ、またですか！？");

                        UpdateMainMessage("ガンツ：何を言っておるアイン、お主なら複合系など容易いだろう。");

                        UpdateMainMessage("アイン：う～ん・・・あの人苦手なんだよなあ・・・");

                        UpdateMainMessage("ガンツ：アインよ、精進しに行ってきなさい。");

                        UpdateMainMessage("アイン：ハイ・・・");

                        // todo
                        //using (MessageDisplay md = new MessageDisplay())
                        //{
                        //    md.StartPosition = FormStartPosition.CenterParent;
                        //    md.Message = "ダンジョンゲート裏の広場にて";
                        //    md.ShowDialog();
                        //}

                        UpdateMainMessage("アイン：えっと、確かこの辺だったな・・・");

                        UpdateMainMessage("アイン：オッケー、発見発見っと！");

                        UpdateMainMessage("アイン：っしゃ、早速転送してもらおうか！");

                        UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                        // todo
                        //using (MessageDisplay md = new MessageDisplay())
                        //{
                        //    md.StartPosition = FormStartPosition.CenterParent;
                        //    md.Message = "アインは別の場所へと飛ばされてしまった。";
                        //    md.ShowDialog();
                        //}

                        //this.buttonHanna.Visible = false;
                        //this.buttonDungeon.Visible = false;
                        //this.buttonRana.Visible = false;
                        //this.buttonGanz.Visible = false;
                        //this.buttonPotion.Visible = false;
                        //this.buttonDuel.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                        UpdateMainMessage("アイン：えっと・・・カールハンツ爵はどこに・・・");

                        UpdateMainMessage("カール：ココだ。");

                        UpdateMainMessage("アイン：って、おわぁぁ！！");

                        UpdateMainMessage("　　　『カール爵は突然見たこともないファイアボールを放ってきた！』");

                        UpdateMainMessage("アイン：ッゲ！！！");

                        UpdateMainMessage("　　　『アインはとっさの判断で身をかわした』");

                        UpdateMainMessage("カール：ホラホラホラ！");

                        UpdateMainMessage("　　　『カール爵は次々と魔法の弾丸を撃ち込んできている！！』");

                        UpdateMainMessage("アイン：っおわ！！っちょっちょ！！");

                        UpdateMainMessage("　　　『アインは５発のファイアボールらしき弾丸を何とか回避しきった』");

                        UpdateMainMessage("アイン：ッタタ、タンマタンマ！！");

                        UpdateMainMessage("アイン：あのボケ師匠も大概だけど、あんたも無茶苦茶だないきなり・・・");

                        UpdateMainMessage("カール：ッフハハ、そうとは言えしっかりと回避してるようだが。");

                        UpdateMainMessage("アイン：そりゃ、こんなもん一回一回食らってたらキリが無いだろ。");

                        UpdateMainMessage("カール：転送装置先では、敵が待ち構えてる場合も多い。気を付けるのだな。");

                        UpdateMainMessage("アイン：（ググ・・・この人やっぱり敵なんじゃ・・・）");

                        UpdateMainMessage("アイン：というか、見たこと無い魔法だったが・・・");

                        UpdateMainMessage("アイン：ひょっとして今のが！？");

                        UpdateMainMessage("カール：聖と火の複合魔法「フラッシュ・ブレイズ」だ。");

                        UpdateMainMessage("カール：やってみるが良い。");

                        UpdateMainMessage("アイン：い、いきなりですか！？");

                        UpdateMainMessage("カール：先の教え、覚えておるだろう。");

                        UpdateMainMessage("カール：教えの通りにやると良い、貴君は習得済みであると言ったハズだ。");

                        UpdateMainMessage("アイン：そ・・・そうかなあ・・・じゃあ・・・");

                        UpdateMainMessage("　　　『アインは魔法詠唱の構えを始めた』");

                        UpdateMainMessage("アイン：（こう・・・火に明かりを添えるようにして・・・）");

                        UpdateMainMessage("　　　ッバシュ！！　　　");

                        UpdateMainMessage("アイン：ッゲ！！！");

                        UpdateMainMessage("カール：まだまだだが、ひとまず出せたようだな。");

                        UpdateMainMessage("アイン：っそ、そんな本当に１回目で・・・");

                        UpdateMainMessage("カール：驚いたか。");

                        UpdateMainMessage("アイン：す・・・スゲェぜ！！　これ！！！");

                        UpdateMainMessage("カール：直感と感性で習得してきた貴君にとっては、新鮮な感覚であろう。");

                        UpdateMainMessage("アイン：・・・あの講義のおかげですかね？");

                        UpdateMainMessage("カール：当然だ。ずいぶんと無礼な質問だな。");

                        UpdateMainMessage("アイン：いやいやいや！スンマセンでした！！");

                        UpdateMainMessage("カール：今回はここまでだな、また来ると良い。");

                        UpdateMainMessage("アイン：ホントどうもありがとうございました！");

                        GroundOne.MC.FlashBlaze = true;
                        ShowActiveSkillSpell(GroundOne.MC, Database.FLASH_BLAZE);

                        if (!GroundOne.WE.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        // todo
                        //this.buttonHanna.Visible = true;
                        //this.buttonDungeon.Visible = true;
                        //this.buttonRana.Visible = true;
                        //this.buttonGanz.Visible = true;
                        //this.buttonPotion.Visible = true;
                        //this.buttonDuel.Visible = true;


                        UpdateMainMessage("ガンツ：どうだったかね。");

                        UpdateMainMessage("アイン：・・・驚きました！！");

                        UpdateMainMessage("ガンツ：その様子、どうやら身に付けたようだね。");

                        UpdateMainMessage("アイン：これが驚きなんですよ！！");

                        UpdateMainMessage("アイン：始めから、クリーンに詠唱成功したんですよ！！");

                        UpdateMainMessage("ガンツ：よほど嬉しかったと見える。それほどかね？");

                        UpdateMainMessage("アイン：あんな体験は初めてでしたよ！！");

                        UpdateMainMessage("アイン：何せ、はじめっからですよ・・・はじめっから・・・");

                        UpdateMainMessage("ガンツ：アインよ、次からは好きなタイミングで彼の元へ訪れるがよい。");

                        UpdateMainMessage("アイン：あ、はい。また行ってみます！");

                        UpdateMainMessage("ガンツ：うむ、精進せいよ。");

                        // todo
                        //using (MessageDisplay md = new MessageDisplay())
                        //{
                        //    md.StartPosition = FormStartPosition.CenterParent;
                        //    md.Message = "アインは「ゲート裏　転送装置」へ行けるようになりました。";
                        //    md.ShowDialog();
                        //}
                        buttonShinikia.gameObject.SetActive(true);
                        GroundOne.WE.AvailableBackGate = true;
                        GroundOne.WE.alreadyCommunicateCahlhanz = true; // カール爵に教えてもらったばかりのため、Trueを指定しておく。
                    }


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

                UpdateMainMessage("アイン：こんちわー。");

                UpdateMainMessage("ガンツ：アインよ、相変わらず元気そうじゃの。");

                UpdateMainMessage("アイン：ッハハ・・・");

                UpdateMainMessage("ガンツ：４階へは、やはり進むつもりか。");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ガンツ：止めるつもりはなさそうだな。");

                UpdateMainMessage("アイン：ええ、なんとか制覇してみるつもりです。");

                UpdateMainMessage("ガンツ：ふむ、精進しなさい。");

                UpdateMainMessage("アイン：ハイ、それでは・・・");

                UpdateMainMessage("ガンツ：待ちなさい。");

                UpdateMainMessage("ガンツ：アインよ、剣を見せてくれんかね。");

                UpdateMainMessage("アイン：剣・・・？");

                UpdateMainMessage("ガンツ：練習用の剣を以前渡したであろう。");

                UpdateMainMessage("アイン：あ、ああ！　ちょっと待ってください。");

                UpdateMainMessage("アイン：ええと・・・これだ。ハイ、どうぞ");

                UpdateMainMessage("ガンツ：ふむ");

                UpdateMainMessage("ガンツ：・・・");

                string detectName = PracticeSwordLevel(GroundOne.MC);

                if (detectName == Database.POOR_PRACTICE_SWORD_ZERO)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：剣が成長しておらんようだな。");

                    UpdateMainMessage("アイン：え・・・？");

                    UpdateMainMessage("アイン：今、成長って言いました？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうだったんですか・・・");

                    UpdateMainMessage("ガンツ：焦る事はない。興味があれば、また使ってみなさい。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもすみませんでした！");

                    UpdateMainMessage("ガンツ：謝る必要はない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・そうだったのか・・・）");

                    UpdateMainMessage("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）");

                    UpdateMainMessage("アイン：（まあ、気が向いたら使ってみるか）");
                }
                else if ((detectName == Database.POOR_PRACTICE_SWORD_1) ||
                         (detectName == Database.POOR_PRACTICE_SWORD_2) ||
                         (detectName == Database.COMMON_PRACTICE_SWORD_3))
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：ほんの少しだけ、成長しておるようだな。");

                    UpdateMainMessage("アイン：ええ・・・何となくだけど、少しだけマシに振る舞えるようにはなりました。");

                    UpdateMainMessage("アイン：それより今、成長って言いました？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうだったんですか・・・");

                    UpdateMainMessage("ガンツ：焦る事はない。興味があれば、また使ってみなさい。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもすみませんでした！");

                    UpdateMainMessage("ガンツ：謝る必要はない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・そうだったのか・・・）");

                    UpdateMainMessage("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）");

                    UpdateMainMessage("アイン：（まあ、気が向いたら使ってみるか）");
                }
                else if ((detectName == Database.COMMON_PRACTICE_SWORD_4) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_5) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_6))
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：なかなか、成長してきておるようだな。");

                    UpdateMainMessage("アイン：ええ・・・しかし、この剣、不思議ですよね。");

                    UpdateMainMessage("アイン：使えば使うほど熟練度が上がるっていうか・・・");

                    UpdateMainMessage("アイン：使いようによって、どんどん攻撃ダメージが上がってきてる感じがするんですよ。");

                    UpdateMainMessage("ガンツ：お主の言うとおり。");

                    UpdateMainMessage("アイン：え？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうか。どうりで・・・");

                    UpdateMainMessage("ガンツ：この調子で、その剣を使いこなしてみなさい。");

                    UpdateMainMessage("ガンツ：アイン、お主はきっと強くなれる。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもありがとうございます！");

                    UpdateMainMessage("ガンツ：謝る必要はない、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・確かに威力がどんどん上がってきている・・・）");

                    UpdateMainMessage("アイン：（４階層、一気に使いこなせるように振舞ってみるか）");
                }
                else if (detectName == Database.EPIC_PRACTICE_SWORD_7)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：アインよ。お主はこの剣が、何であるかは理解しているか？");

                    UpdateMainMessage("アイン：理解・・・ですか？");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("アイン：いえ、多分理解までは・・・");

                    UpdateMainMessage("ガンツ：ふむ、良い心構えだ。");

                    UpdateMainMessage("ガンツ：アインよ、答えはもう目の前である感覚はあるかね？");

                    UpdateMainMessage("アイン：ええ・・・正直な所、もう何となくは・・・");

                    UpdateMainMessage("ガンツ：アインよ、お主はもう十分に強くなった。");

                    UpdateMainMessage("ガンツ：アインよ、常々、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、どうもありがとうございます。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣への・・・理解・・・）");

                    UpdateMainMessage("アイン：（あと一息な感じはしている・・・）");

                    UpdateMainMessage("アイン：（もう一超え頑張るとするか！）");
                }
                else if (detectName == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：よくぞここまで。見事だ。");

                    UpdateMainMessage("アイン：いえ、これは俺が単に逃げ続けていただけですから。");

                    UpdateMainMessage("ガンツ：そうではない。向かい続けてきた結果だ。卑下をする事はない。");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：フェルトゥーシュ、今お主は、その手に所持しておる。");

                    UpdateMainMessage("アイン：ええ、確かにこの手に。");

                    UpdateMainMessage("ガンツ：恐る事なく、進めるが良い。");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：決して");

                    UpdateMainMessage("ガンツ：決して、負けるな、アインよ。");

                    UpdateMainMessage("ガンツ：精進を怠らず、進めよ。アイン・ウォーレンス。");

                    UpdateMainMessage("アイン：分かりました！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（フェルトゥーシュにより俺は・・・）");

                    UpdateMainMessage("アイン：（分かってる、進むんだ）");

                    UpdateMainMessage("アイン：（俺は必ず、この手で）");

                    UpdateMainMessage("アイン：（決着を付けてみせる。）");
                }
            }
            #endregion
            #region "３階開始時"
            else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationGanz31)
            {
                GroundOne.WE.Truth_CommunicationGanz31 = true;

                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アインよ。いよいよ３階へと進むつもりか。");

                    UpdateMainMessage("アイン：はい、今日からスタートさせるつもりです。");

                    UpdateMainMessage("ガンツ：ふむ、ワシから言う事は特にない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：あっと、ハイ！ありがとうございました！！");

                    UpdateMainMessage("ガンツ：だが、一つ言っておかねばならん事がある。");

                    UpdateMainMessage("アイン：（ッゲ、特に無いと言ったのに・・・この展開は・・・）");

                    UpdateMainMessage("ガンツ：アインよ、どこへ向かう？");

                    UpdateMainMessage("アイン：どこって、ダンジョン３階です。");

                    UpdateMainMessage("ガンツ：バカの振りは不要。しっかりと答えなさい。");

                    UpdateMainMessage("アイン：う～ん、そう言われても・・・");

                    if (GroundOne.WE2.TruthRecollection1 && GroundOne.WE2.TruthRecollection2)
                    {
                        UpdateMainMessage("アイン：始まりの地へ。");

                        UpdateMainMessage("アイン：広大な草原と無限に拡がる大空。");

                        UpdateMainMessage("アイン：そこで俺は、ケリをつける。");

                        UpdateMainMessage("ガンツ：・・・・・・ふむ・・・・・・");

                        UpdateMainMessage("ガンツ：精進しなさい、アインよ。");

                        UpdateMainMessage("ガンツ：決して負けてはならん。よいな？");

                        UpdateMainMessage("アイン：ああ、任せてくれ。");

                        UpdateMainMessage("アイン：絶対に今度こそ。");

                        UpdateMainMessage("ガンツ：うむ、行ってきなさい。気をつけてな。");

                        UpdateMainMessage("アイン：ああ、了解！");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ダンジョン最下層だ。");

                        UpdateMainMessage("アイン：俺は絶対にこのダンジョンを制覇してみせる！");

                        UpdateMainMessage("ガンツ：ふむ、その勢い、忘れぬようにな。");

                        UpdateMainMessage("アイン：ガンツ叔父さんと話していると元気が出るよ、サンキュー。");

                        UpdateMainMessage("ガンツ：無理だけはせぬようにな、毎日をしっかり生きなさい。");

                        UpdateMainMessage("アイン：ああ、じゃあ行ってくるぜ！");
                    }
                    GroundOne.WE.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "２階開始時"
            else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationGanz21)
            {
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アイン。２階へ向かうようだな。");

                    UpdateMainMessage("アイン：あ、ええ。今日からそのつもりです。");

                    UpdateMainMessage("ガンツ：ならば、これでも持って行くと良いだろう。");

                    CallSomeMessageWithAnimation("アインは" + Database.POOR_PRACTICE_SWORD_ZERO + "を手に入れた。");

                    UpdateMainMessage("アイン：これは・・・練習用の剣？");

                    UpdateMainMessage("ガンツ：その武器には特殊な効果が封じ込められておる。");

                    UpdateMainMessage("アイン：そうなんですか？");

                    UpdateMainMessage("ガンツ：ワシなりに考えてみたが、アインよ。");

                    UpdateMainMessage("アイン：ハイ。");

                    UpdateMainMessage("ガンツ：・・・いや、お前なりに使ってみると良い。");

                    UpdateMainMessage("アイン：えっと、どういう事でしょうか？");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：あっと、ハイ！ありがとうございました！！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（どうみてもこれは単なる練習用の剣だが・・・）");

                    UpdateMainMessage("アイン：（・・・いや、そんなわけがねえよな）");

                    UpdateMainMessage("アイン：（っしゃ、せっかくなんだし、使ってみるとするか！）");

                    GetItemFullCheck(GroundOne.MC, Database.POOR_PRACTICE_SWORD_ZERO);

                    GroundOne.WE.Truth_CommunicationGanz21 = true;
                    GroundOne.WE.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "１日目"
            else if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationGanz1)
            {
                // if (!GroundOne.WE.AlreadyRest) //  1日目はアインが起きたばかりなので、本フラグを未使用とします。
                if (!GroundOne.WE.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：ガンツ叔父さん、いますかー？");

                    UpdateMainMessage("ガンツ：アインか。よく来てくれた。");

                    UpdateMainMessage("アイン：武具店、開いてますか？");

                    UpdateMainMessage("ガンツ：ああ、今月はヴァスタ爺からの物資配給が良くてな。開店しておるので見ていくと良い。");

                    UpdateMainMessage("アイン：っしゃ！やったぜ！じゃあ早速見せてもらうとするぜ！！");

                    GroundOne.WE.AlreadyEquipShop = true;
                    GroundOne.WE.AvailableEquipShop = true;
                    GroundOne.WE.Truth_CommunicationGanz1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。

                    CallEquipmentShop();
                    mainMessage.text = "";

                    UpdateMainMessage("アイン：叔父さん、また来るぜ。");

                    UpdateMainMessage("ガンツ：アインよ。これからダンジョンへ向かうのだな？");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。");

                    UpdateMainMessage("アイン：ッマジっすか！？ハハ、やったぜ！ありがとうございます！");

                    UpdateMainMessage("ガンツ：ダンジョンで殺したモンスターからは、役に立つ材料がいくつも採れる。");

                    UpdateMainMessage("アイン：ッハイ！");

                    UpdateMainMessage("ガンツ：モンスターより得られる部材、素材をワシの所へ持ってくると良い。");

                    UpdateMainMessage("アイン：ッハイ！！");

                    UpdateMainMessage("ガンツ：それら部材、素材を組み合わせ、ワシが腕によりをかけて新しい武具を作ろう。");

                    UpdateMainMessage("アイン：ッハイ！！！");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("ガンツ：では頼んだぞ。");

                    UpdateMainMessage("アイン：ッハイ！　ありがとうございました！！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（待てよ・・・これはひょっとして・・・）");

                    UpdateMainMessage("アイン：（最後結局「精進しなさい」しか言ってねえよな・・・）");

                    UpdateMainMessage("アイン：（でもまあ、ガンツ叔父さんの新しい武具生成か。楽しみだな。）");

                    UpdateMainMessage("アイン：（モンスターから得られた部材・素材はガンツ叔父さんのトコへ持っていくとするか。）");
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.text = "";
                }
            }
            #endregion
            #region "その他"
            else
            {
                SceneDimension.Go(Database.TruthHomeTown, Database.TruthEquipmentShop);
                CallEquipmentShop();
                mainMessage.text = "";
            }
            #endregion
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

    public void tapShop2()
    {
        mainMessage.text = "ラナ：ごめんなさい、まだ準備中なのよ。";

    }
    public void tapGate()
    {
        mainMessage.text = "アイン：まだゲートは開いてないみたいだな。";
    }
	public void tapInn() {
        #region "現実世界"
        if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent603)
        {
            UpdateMainMessage("アイン：叔母さん、いますか？");

            UpdateMainMessage("ハンナ：アインじゃないか。何の用だい？");

            UpdateMainMessage("アイン：いや、特に用ってわけじゃないんだが・・・");

            UpdateMainMessage("ハンナ：どうしたんだい、何か気になる事でもあるのかい。");

            UpdateMainMessage("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？");

            UpdateMainMessage("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？");

            UpdateMainMessage("アイン：どうやって、そんな美味い飯を作れるようになったんですか。");

            UpdateMainMessage("ハンナ：う～ん、どうと言われてもねえ。慣れみたいなもんさ。アッハハハ");

            UpdateMainMessage("アイン：ハハハ・・・");

            UpdateMainMessage("アイン：・・・　・・・　・・・");

            UpdateMainMessage("ハンナ：どうしたんだい、今からダンジョンへ向かうんじゃないのかい？");

            UpdateMainMessage("アイン：えっ。");

            UpdateMainMessage("ハンナ：悩んでるようだね。言ってみな。");

            UpdateMainMessage("アイン：・・・　・・・　・・・");

            UpdateMainMessage("アイン：いや、もう行かなくちゃ。");

            UpdateMainMessage("アイン：叔母さん、本当にどうもありがとう。");

            UpdateMainMessage("ハンナ：アッハハハ、変な子だね。あたしゃ何もしてないよ。");

            UpdateMainMessage("アイン：・・・いや、ありがとう。");

            UpdateMainMessage("アイン：じゃ、行ってくる。");

            UpdateMainMessage("ハンナ：ああ、行ってきなさい。体に気をつけるんだよ。");

            UpdateMainMessage("アイン：ああ");

            GroundOne.WE2.SeekerEvent603 = true;
            Method.AutoSaveTruthWorldEnvironment();
            Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
        }
        #endregion
        #region "オル・ランディス遭遇前後"
        else if (GroundOne.WE.AvailableDuelMatch && !GroundOne.WE.MeetOlLandis)
        {
            if (!GroundOne.WE.MeetOlLandisBeforeHanna)
            {
                UpdateMainMessage("アイン：ふううぅぅ・・・こんちわーっす・・・");

                UpdateMainMessage("ハンナ：あれま、どうしたんだい。らしくないため息なんか付いて。");

                UpdateMainMessage("アイン：いや、実はですね・・・");

                UpdateMainMessage("アイン：あのボケ師匠がＤＵＥＬ闘技場へ来てるみたいなんですよ・・・");

                UpdateMainMessage("ハンナ：そりゃ本当かい？良かったじゃないか。");

                UpdateMainMessage("アイン：はあああぁぁ・・・");

                UpdateMainMessage("ハンナ：ちょっとそこで待ってなさいな。");

                UpdateMainMessage("アイン：え？あ、はい。");

                CallSomeMessageWithAnimation("ハンナは厨房から何かを持ってきた");

                UpdateMainMessage("アイン：これは一体・・・なんですか？");

                UpdateMainMessage("ハンナ：キツ～い辛味スパイスを加えた、激辛カレーだよ、たんと食べな。");

                UpdateMainMessage("アイン：マジかよ・・・ッハッハッハ、悪いなおばちゃん。");

                UpdateMainMessage("アイン：そうだな、考えててもしょうがねえよな。じゃいただきますっと！");

                UpdateMainMessage("アイン：グオォォ！！！か、辛ええええぇ！！！");

                UpdateMainMessage("ハンナ：今のうちにキツいパンチをもらっておくんだね。アッハハハ。", true);
                GroundOne.WE.MeetOlLandisBeforeHanna = true;
            }
            else
            {
                UpdateMainMessage("ハンナ：っさあ、おとなしくＤＵＥＬ闘技場へ行ってくるんだね。", true);
            }
            return;
        }
        #endregion
        #region "４階開始時"
        else if (GroundOne.WE.TruthCompleteArea3 && !GroundOne.WE.Truth_CommunicationHanna41)
        {
            GroundOne.WE.Truth_CommunicationHanna41 = true;

            UpdateMainMessage("ハンナ：あら、どうしたんだい。");

            UpdateMainMessage("アイン：すまねえ、爽快ドリンクを一本もらえるかな。");

            UpdateMainMessage("ハンナ：はいよ。");

            UpdateMainMessage("アイン：おっ、サンキュー。");

            UpdateMainMessage("ハンナ：いよいよ、４階に進むのかい。");

            UpdateMainMessage("アイン：ええ、まあ・・・");

            UpdateMainMessage("ハンナ：アッハッハ、何をそんなに怖気づいてるんだい。");

            UpdateMainMessage("アイン：いや、怖気づいてるわけじゃないんだが・・・");

            UpdateMainMessage("アイン：何となくかな・・・ッハハ");

            UpdateMainMessage("ハンナ：そんな所、ラナちゃんには見せられないね。台無しだよ。");

            UpdateMainMessage("アイン：いやいやいや、なんでアイツが出てくるんですか。");

            UpdateMainMessage("ハンナ：おや、出てきちゃ悪いのかい？");

            UpdateMainMessage("アイン：いや、関係ねえ話かなと思って・・・");

            UpdateMainMessage("ハンナ：で、どうしたんだい？怖気づいたんじゃないとしたら");

            UpdateMainMessage("アイン：多分、迷ってるんですよ、俺。");

            UpdateMainMessage("アイン：ヒトゴトみたいに言ってるのもオカシイんですけど。");

            UpdateMainMessage("アイン：【迷いが拭えない】って言ったらいいのか・・・なんだろ。");

            UpdateMainMessage("アイン：今までのが水の泡になったら、って考えると、先に進めなくなるんですよ。");

            UpdateMainMessage("ハンナ：４階に今行きたくないんなら、１日伸ばしたらどうだい。");

            UpdateMainMessage("アイン：いや、行きたくないわけじゃないんですよ。");

            UpdateMainMessage("ハンナ：行くのが、怖いのかい？");

            UpdateMainMessage("アイン：いや、怖いわけでもなく・・・");

            UpdateMainMessage("アイン：・・・なんとなくですが・・・");

            UpdateMainMessage("アイン：【誤った】っていう感触が襲ってくる感じなんですよ。");

            UpdateMainMessage("アイン：進めば進むほど、その感覚が強くなる感じがして・・・");

            UpdateMainMessage("ハンナ：【誤った】というのは感覚の問題だよ。");

            UpdateMainMessage("ハンナ：世界から見れば、【誤った】【正しかった】は存在しない。");

            UpdateMainMessage("アイン：それに関しては、ボケ師匠から嫌というほど知らされてます、分かってるんです。");

            UpdateMainMessage("アイン：だからこれも理由としては違う気がしてて・・・");

            UpdateMainMessage("ハンナ：アイン、深く掘りすぎない事が肝心だよ。");

            UpdateMainMessage("ハンナ：あんたは昔からその独特なクセがあるみたいだからね。");

            UpdateMainMessage("アイン：クセか、ッハハハ・・・確かにそうかも。");

            UpdateMainMessage("ハンナ：【誤った】ことを感じたままの状態で、進めなさい。");

            UpdateMainMessage("ハンナ：【正しかった】で前提で進む心意気が把握できてるのなら");

            UpdateMainMessage("ハンナ：今の状態は【誤った】感を察知した上で進めるのも心構えは同じだとは思わないかい？");

            UpdateMainMessage("アイン：誤った感を察知した上で・・・");

            UpdateMainMessage("アイン：なるほど・・・なるほど、そうかもな！");

            UpdateMainMessage("アイン：そうだな！そうだ、そうだ！そうだよ！サンキュー！");

            UpdateMainMessage("アイン：いやあ、おばちゃんのトコ来ると本っっ当に助かるぜ！ッハッハッハ！");

            UpdateMainMessage("ハンナ：アッハハハ、そうかい。元気になれたんなら良いよ。");

            UpdateMainMessage("ハンナ：アンタが冷えると、隣のラナちゃんも冷え込んでくるからね。まあ、気をつけな。");

            UpdateMainMessage("アイン：いやいやいや、だからアイツは本っっ当関係ないでしょうが、まったく・・・");

            UpdateMainMessage("ハンナ：アッハハハハ、そういう事にしておくわ。");

            UpdateMainMessage("ハンナ：ッホラ、じゃあ頑張って行ってきなさい。");

            UpdateMainMessage("アイン：ああ、ありがと！　じゃな！");
        }
        #endregion
        #region "３階開始時"
        else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.Truth_CommunicationHanna31)
        {
            GroundOne.WE.Truth_CommunicationHanna31 = true;

            UpdateMainMessage("ハンナ：あら、どうしたんだい。");

            UpdateMainMessage("アイン：いや、紅茶一杯もらえるかな。");

            UpdateMainMessage("ハンナ：はいよ。");

            UpdateMainMessage("アイン：さあて、どうすっかな・・・ホント。");

            UpdateMainMessage("ハンナ：なんの話だい？");

            UpdateMainMessage("アイン：スキルアップの話さ。");

            UpdateMainMessage("アイン：俺はもう十分強くなった、そう思うか？叔母ちゃん。");

            UpdateMainMessage("ハンナ：アッハハハハ、もう十分強いんじゃないのかい？");

            UpdateMainMessage("アイン：んなわけねえよな・・・分かってて聞いてんだけどさ、ハハハ。");

            UpdateMainMessage("ハンナ：何迷ってるかは分からないけど、コレだけは言えるよ。");

            UpdateMainMessage("ハンナ：アイン、あんたは強い部類に入るわよ。");

            UpdateMainMessage("アイン：ええぇ・・・お世辞なんか良いですよ。");

            UpdateMainMessage("アイン：自分のウィークポイントなんか山ほどあるし、全然強くならないんですよ。");

            UpdateMainMessage("ハンナ：いいや、数多くの旅の人を見てきたアタシが言うんだから、間違いないよ。");

            UpdateMainMessage("アイン：い、いやいや、本当・・・");

            UpdateMainMessage("ハンナ：いやいや、あんたは本当に強いよ。");

            UpdateMainMessage("アイン：う～ん、本当ですか？");

            UpdateMainMessage("ハンナ：本当の本当ってもんさ、アッハハハハ。");

            UpdateMainMessage("アイン：ハハハ・・・ありがとな。叔母ちゃん。");

            UpdateMainMessage("アイン：もし、３階が解けたらさ。");

            UpdateMainMessage("ハンナ：なんだい。");

            UpdateMainMessage("アイン：またいろいろと教えてくれ。");

            UpdateMainMessage("ハンナ：何言ってんだい、アタシから教えられる事なんて無いよ。");

            UpdateMainMessage("ハンナ：まったく。　ッホラホラ、行く前から落ち着いてんじゃないわよ。");

            UpdateMainMessage("ハンナ：キッチリ３階をクリアしてくるんだね、行ってきな。");

            UpdateMainMessage("アイン：あ、ああ！　オーケー！");

            if (GroundOne.WE.Truth_CommunicationOl31)
            {
                UpdateMainMessage("ハンナ：あらやだ、そう言えば忘れていたわ、アイン。");

                UpdateMainMessage("アイン：ん？　何かあるのか？");

                UpdateMainMessage("ハンナ：アンタの師匠から預かってるわよ。荷物。");

                UpdateMainMessage("アイン：あ、ああ。そういや別れ際そんな事言ってたな。");

                UpdateMainMessage("アイン：オバチャン、荷物管理とかもやってるのか？");

                UpdateMainMessage("ハンナ：アッハハハハ、やってないわね。");

                UpdateMainMessage("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？");

                UpdateMainMessage("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。");

                UpdateMainMessage("アイン：っえ？あ、ああ・・・");
                GroundOne.WE.Truth_CommunicationHanna31_2 = true;
            }
        }
        #endregion
        #region "荷物預け追加"
        else if (GroundOne.WE.TruthCompleteArea2 && !GroundOne.WE.AvailableItemBank && GroundOne.WE.Truth_CommunicationOl31)
        {
            if (GroundOne.WE.Truth_CommunicationHanna31_2 == false)
            {
                UpdateMainMessage("ハンナ：あら、そう言えば、忘れてたわ。");

                UpdateMainMessage("アイン：ん？");

                UpdateMainMessage("ハンナ：アンタの師匠から預かってるわよ。荷物。");

                UpdateMainMessage("アイン：あ、ああ。そういや別れ際そんな事言ってたな。");

                UpdateMainMessage("アイン：オバチャン、荷物管理とかもやってるのか？");

                UpdateMainMessage("ハンナ：アッハハハハ、やってないわね。");

                UpdateMainMessage("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？");

                UpdateMainMessage("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。");

                UpdateMainMessage("アイン：っえ？あ、ああ・・・");
            }

            UpdateMainMessage("ハンナ：アイン、ほらこっちだよ。");

            UpdateMainMessage("アイン：あ、ああ。。。");

            UpdateMainMessage("アイン：（ホントだ。ちゃんと置いてってくれてたんだな・・・）");

            UpdateMainMessage("ハンナ：ああ見えて、照れ屋だからね。アンタの師匠は。");

            UpdateMainMessage("ハンナ：アンタに期待してるみたいだったよ。感謝しなさい、ッホラ！");

            UpdateMainMessage("アイン：あ、ああ、ああ・・・サンキューな、オバチャン。");

            UpdateMainMessage("ハンナ：アッハハハハ、アタシじゃなくて、お師匠さんに感謝しなさい。");

            UpdateMainMessage("アイン：ハハ・・・確かに。");

            UpdateMainMessage("アイン：しかし突然渡されてもな・・・");

            UpdateMainMessage("アイン：オバチャン、少しだけの間、保管しておいてもらえるか？");

            UpdateMainMessage("ハンナ：ああ、モチロンだよ。少しと言わずしばらくはずっと保管しといてあげるよ。");

            UpdateMainMessage("ハンナ：好きな時に持って行くんだね。");

            UpdateMainMessage("アイン：あと、俺のアイテムも出来れば・・・");

            UpdateMainMessage("ハンナ：モチロン構わないよ。預けたいモノは預けていきな。");

            UpdateMainMessage("アイン：いやあ、ホンット助かるぜ、サンキュー！");

            GroundOne.WE.AvailableItemBank = true;
            // todo
            //using (MessageDisplay md = new MessageDisplay())
            //{
            //    md.StartPosition = FormStartPosition.CenterParent;
            //    md.Message = "ハンナの宿屋で「荷物の預け・受け取り」が可能になりました！";
            //    md.ShowDialog();
            //}

            UpdateMainMessage("ハンナ：ただ、無限には受け取れないよ。こちらも倉庫は限られてるからね。");

            UpdateMainMessage("アイン：いやいや、少しだけでも。本当助かります。ありがとうございます！");

            UpdateMainMessage("ハンナ：後は、アンタの好きなように整備しな。任せたわよ。");

            UpdateMainMessage("アイン：ありがとうございました！使わせてもらいます！どうもです！！");
        }
        #endregion
        #region "２階開始時"
        else if (GroundOne.WE.TruthCompleteArea1 && !GroundOne.WE.Truth_CommunicationHanna21)
        {
            UpdateMainMessage("ハンナ：おや、アインじゃないか。どうしたんだい？");

            UpdateMainMessage("アイン：叔母ちゃん、エルモラの紅茶一杯ください。");

            UpdateMainMessage("ハンナ：あいよ。少し待ってるんだね。");

            UpdateMainMessage("ハンナ：はい、どうぞ召し上がりな。");

            UpdateMainMessage("アイン：サンキュー、叔母ちゃん。");

            UpdateMainMessage("アイン：ふう・・・");

            UpdateMainMessage("ハンナ：どうしたんだい。言ってごらん。");

            UpdateMainMessage("アイン：２階行ってくるぜ。");

            UpdateMainMessage("ハンナ：そうかい、頑張って来な。");

            UpdateMainMessage("アイン：ただ・・・");

            UpdateMainMessage("アイン：っつ・・・上手く言えないんだが・・・");

            UpdateMainMessage("ハンナ：上手く行ってる証拠と考えたらどうだい？");

            UpdateMainMessage("アイン：・・・っはい？");

            UpdateMainMessage("ハンナ：何も無い状態なら、そんな風にはならないよ。");

            UpdateMainMessage("ハンナ：何か想う所がある。違うかい？");

            UpdateMainMessage("アイン：っえ、ええ・・・まあそうです。");

            UpdateMainMessage("ハンナ：だったら、その通りに進んでみたらどうだい。");

            UpdateMainMessage("ハンナ：進まない限り、答えなんて見つかりっこないからね。");

            UpdateMainMessage("アイン：・・・そうか、なるほど・・・");

            UpdateMainMessage("アイン：叔母ちゃん、ありがとな。今度こそ、２階行ってくるぜ！");

            UpdateMainMessage("ハンナ：あいよ、行ってらっしゃい。");

            GroundOne.WE.Truth_CommunicationHanna21 = true;
        }
        #endregion
        #region "一日目"
        else if (this.firstDay >= 1 && !GroundOne.WE.Truth_CommunicationHanna1 && GroundOne.MC.Level >= 1)
        {
            UpdateMainMessage("アイン：叔母さん、いますか？");

            UpdateMainMessage("ハンナ：アインじゃないか。何の用だい？");

            UpdateMainMessage("アイン：いや、特に用ってわけじゃないんだが・・・");

            UpdateMainMessage("ハンナ：どうしたんだい、何か気になる事でもあるのかい。");

            UpdateMainMessage("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？");

            UpdateMainMessage("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？");

            UpdateMainMessage("アイン：どうやって、そんな美味い飯を作れるようになったんですか。");

            UpdateMainMessage("ハンナ：う～ん、どうと言われてもねえ。こればかりは経験を積むしかないよ。");

            UpdateMainMessage("アイン：経験・・・か。");

            UpdateMainMessage("ハンナ：アイン。ひとつ頼まれてくれないかい？");

            UpdateMainMessage("ハンナ：アインは今からダンジョンへ向かうんだね？");

            UpdateMainMessage("アイン：はい。");

            UpdateMainMessage("ハンナ：ダンジョンで得たアイテムで、食材になる物をワタシの所へ持ってきてくれないかね？");

            UpdateMainMessage("ハンナ：そうしたら、これまでよりもっと良い夕飯を出せるようになるからね。");

            UpdateMainMessage("アイン：マジっすか！？なら、喜んで持ってきますよ！任せておいてください！");

            UpdateMainMessage("ハンナ：アッハハハ、期待して待ってるよ。さあ、いってらっしゃいな。", true);

            GroundOne.WE.Truth_CommunicationHanna1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
            return;
        }
        #endregion
        #region "その他"
        else
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                UpdateMainMessage("アイン：おばちゃん。空いてる？");

                UpdateMainMessage("ハンナ：空いてるよ。泊まってくかい？");

                UpdateMainMessage("アイン：どうすっかな・・・泊まるか？");

                // todo
                //using (YesNoRequestMini yesno = new YesNoRequestMini())
                {
                    // todo
                    //yesno.Location = new Point(this.Location.X + 784, this.Location.Y + 708);
                    //yesno.Large = true;
                    //yesno.ShowDialog();
                    //if (yesno.DialogResult == DialogResult.Yes)
                    if (true)
                    {
                        UpdateMainMessage("ハンナ：はいよ、部屋は空いてるよ。ゆっくりと休みな。");

                        UpdateMainMessage("アイン：サンキュー、おばちゃん。");
                        
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        UpdateMainMessage("ハンナ：今日は何か食べていくかい？");

                        // todo
                        //using (TruthRequestFood trf = new TruthRequestFood())
                        {
                            // todo
                            //trf.StartPosition = FormStartPosition.CenterParent;
                            //trf.MC = this.mc;
                            //trf.SC = this.sc;
                            //trf.TC = this.tc;
                            //trf.WE = this.we;
                            //trf.ShowDialog();
                            //this.mc = trf.MC;
                            //this.sc = trf.SC;
                            //this.tc = trf.TC;
                            //this.we = trf.WE;
                            //if (trf.DialogResult == System.Windows.Forms.DialogResult.OK)
                            string tempSelect = "とんかつ定食";
                            {
                                UpdateMainMessage("アイン：おばちゃん、『" + tempSelect + "』を頼むぜ。"); // todo

                                UpdateMainMessage("ハンナ：『" + tempSelect + "』だね。少し待ってな。"); // todo

                                UpdateMainMessage("ハンナ：はいよ、お待たせ。たんと召し上がれ。");

                                UpdateMainMessage("　　【アインは十分な食事を取りました。】");

                                UpdateMainMessage("アイン：ふう～、食った食った・・・");

                                UpdateMainMessage("アイン：おばちゃん、ごちそうさま！");

                                UpdateMainMessage("ハンナ：あいよ、後は明日に備えてゆっくり休みな。");

                            }
                        }

                        CallRestInn();
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ごめん。まだ用があるんだ、後でくるよ。");

                        UpdateMainMessage("ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。");
                    }
                }
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
                    UpdateMainMessage("ハンナ：もう朝だよ。今日も頑張ってらっしゃい。");
                }
            }
        }
        #endregion
    }

    // todo
    private void CallRestInn()
    {
        CallRestInn(false);
    }
    private void CallRestInn(bool noAction)
    {
        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

        if (noAction == false)
        {
            GroundOne.PlaySoundEffect("RestInn.mp3");
            // todo
            //using (MessageDisplay md = new MessageDisplay())
            //{
            //    md.Message = "休息をとりました";
            //    md.StartPosition = FormStartPosition.CenterParent;
            //    md.ShowDialog();
            //}
            UpdateMainMessage("休息をとりました");
        }

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
            if (WhoisDuelPlayer() != string.Empty)
            {
                DuelSupportMessage(SupportType.Begin, WhoisDuelPlayer());
            }
        }
    }

    public void tapExit()
    {
        Application.Quit();
    }

    public void CallStatusPlayer()
    {
        SceneDimension.Go(Database.TruthHomeTown, Database.TruthStatusPlayer);
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
    // todo
    private void ChangeBackgroundData(string p)
    {
    }

    // todo
    private void CallSomeMessageWithAnimation(string p)
    {
    }
    }
}

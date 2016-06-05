using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;

public class TruthChoiceStatue : MotherForm {

    // gui
    private Text mainMessage;
    private Button btnOK;
    private Button buttonChoice2;
    private Button buttonChoice1;
    private Button buttonChoice3;
    private Button buttonChoice4;
    private Button buttonFact1;
    private Button buttonFact2;
    private Button buttonFact3;
    private Button buttonFact4;
    private Button buttonFact5;
    private Button buttonFact6;
    private Button buttonFact7;
    private Button buttonFact8;
    private Button buttonFact9;
    private Button buttonFact10;
    private Button buttonTruth1;
    private Button buttonTruth2;
    private Button buttonTruth3;
    private Button buttonTruth4;
    private Button buttonTruth5;
    private Button buttonTruth6;
    private Button buttonTruth7;
    private Button buttonTruth8;
    private Button buttonTruth9;
    private Button buttonTruth10;
    private Button buttonElemental6;
    private Button buttonElemental5;
    private Button buttonElemental4;
    private Button buttonElemental3;
    private Button buttonElemental2;
    private Button buttonElemental1;
    private Button buttonElemental7;
    private Button buttonElemental8;
    private Button buttonSong1;
    private Button buttonSong2;
    private Button buttonSong3;
    private Button buttonSong4;

    private bool flag0 = false;
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false;
    private bool flagA = false; // ファースト調査中、３か所とも調査済みでTrueになる
    private bool flag4 = false;
    private bool flag4_2 = false;
    private bool flag5 = false;
    private bool flag6 = false;
    private bool flag6_2 = false;
    private bool flag6_3 = false;
    private bool flagB = false; // ファースト選定、ラナの像で決定すればTrue、それ以外はBadEnd
    List<int> factOrder = new List<int>();
    private bool flagC = false; // 事実の順序を選定中、全選択完了でTrueになる
    private bool flagD = false; // 事実選定後、ラナの像で決定すればTrue、それ以外はBadEnd
    private bool flagE = false; // 真実選定前の調査、人間の像を選べばTrue、ラナの像で決定はBadEnd
    private bool flagF = false; // 真実の順序を選定中、全選択完了でTrueになる
    private bool flag7 = false; // 事実選定後、ラナの像で決定すればTrue、それ以外はBadEnd
    List<int> truthOrder = new List<int>();
    private bool flagG = false;
    List<int> elementalOrder = new List<int>();
    private bool flagH = false; // 神剣フェルトゥーシュ８つの光を選定中
    private bool flagI = false; // 神剣選定後、ラナの像を選べばTrue、それ以外はBadEnd
    List<int> songOrder = new List<int>();
    private bool flagJ = false; // 壁画の詩を選定中、全選択完了でTrueになる
    private bool flagK = false; // 壁画選定後、ラナの像を選べばTrue、それ以外はBadEnd

    int nowReading = 0;
    List<string> nowMessage = new List<string>();
    List<MessagePack.ActionEvent> nowEvent = new List<MessagePack.ActionEvent>();
    DungeonPlayer.MessagePack.ActionEvent currentEvent;

    public override void Start()
    {
        base.Start();

        buttonChoice1.enabled = false;
        buttonChoice2.enabled = false;
        buttonChoice3.enabled = false;
        buttonFact1.enabled = false;
        buttonFact2.enabled = false;
        buttonFact3.enabled = false;
        buttonFact4.enabled = false;
        buttonFact5.enabled = false;
        buttonFact6.enabled = false;
        buttonFact7.enabled = false;
        buttonFact8.enabled = false;
        buttonFact9.enabled = false;
        buttonFact10.enabled = false;
        buttonTruth1.enabled = false;
        buttonTruth2.enabled = false;
        buttonTruth3.enabled = false;
        buttonTruth4.enabled = false;
        buttonTruth5.enabled = false;
        buttonTruth6.enabled = false;
        buttonTruth7.enabled = false;
        buttonTruth8.enabled = false;
        buttonTruth9.enabled = false;
        buttonTruth10.enabled = false;
        buttonElemental1.enabled = false;
        buttonElemental2.enabled = false;
        buttonElemental3.enabled = false;
        buttonElemental4.enabled = false;
        buttonElemental5.enabled = false;
        buttonElemental6.enabled = false;
        buttonElemental7.enabled = false;
        buttonElemental8.enabled = false;
        buttonSong1.enabled = false;
        buttonSong2.enabled = false;
        buttonSong3.enabled = false;
        buttonSong4.enabled = false;
    }

    public override void Update()
    {
        base.Update();
    }
    
    public void tapOK()
    {
        bool HideFilterComplete = true;
        bool ForceSkipTapOK = false;

        if (this.nowReading < this.nowMessage.Count)
        {
            this.Filter.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            this.Filter.SetActive(true);
            this.btnOK.enabled = true;
            this.btnOK.gameObject.SetActive(true);

            this.currentEvent = this.nowEvent[this.nowReading];
            // メッセージ反映
            if (currentEvent == MessagePack.ActionEvent.None)
            {
                mainMessage.text = "   " + this.nowMessage[this.nowReading];
            }

            // 各イベント固有の処理
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueHuman)
            {
                this.buttonChoice1.enabled = true;
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueLana)
            {
                this.buttonChoice2.enabled = true;
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueDestroySword)
            {
                this.buttonChoice3.enabled = true;
            }

            this.nowReading++;
            if (this.nowMessage[this.nowReading - 1] == "" || ForceSkipTapOK)
            {
                tapOK();
            }
        }

        if (this.nowReading >= this.nowMessage.Count)
        {
            this.nowReading = 0;
            this.nowMessage.Clear();
            this.nowEvent.Clear();

            this.btnOK.enabled = false;
            this.btnOK.gameObject.SetActive(false);
            if (HideFilterComplete)
            {
                this.Filter.GetComponent<Image>().color = Color.white;
                this.Filter.SetActive(false);
            }
        }
    }

    private void NormalEnd_Correct()
    {
        MessagePack.Message14130(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    private void BadEnd_LanaStatue()
    {
        MessagePack.Message14131(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    private void BadEnd_HumanStatue()
    {
        MessagePack.Message14132(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    private void BadEnd_Surrender()
    {
        MessagePack.Message14133(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    private void ChangeChoice()
    {
        if (this.flag1 && this.flag2 && this.flag3)
        {
            this.buttonChoice1.enabled = false;
            this.buttonChoice2.enabled = false;
            this.buttonChoice3.enabled = false;
            this.flagA = true;

            MessagePack.Message14134(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }
    }

    // 人間の像
    private void buttonChoice1_Click()
    {
        MessagePack.Message14135(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    // ラナ・アミリアの像
    private void buttonChoice2_Click()
    {
        MessagePack.Message14136(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    // フェルトゥーシュの剣
    private void buttonChoice3_Click()
    {
        MessagePack.Message14137(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }

    // 歩を進める
    private void buttonChoice4_Click()
    {
        MessagePack.Message14138(ref this.nowMessage, ref this.nowEvent);
        tapOK();
    }


    private bool CheckFactOrder()
    {
        if (this.factOrder[0] != 1) { return false; }
        if (this.factOrder[1] != 2) { return false; }
        if (this.factOrder[2] != 3) { return false; }
        if (this.factOrder[3] != 4) { return false; }
        if (this.factOrder[4] != 5) { return false; }
        if (this.factOrder[5] != 6) { return false; }
        if (this.factOrder[6] != 7) { return false; }
        if (this.factOrder[7] != 8) { return false; }
        if (this.factOrder[8] != 9) { return false; }
        if (this.factOrder[9] != 10) { return false; }

        return true;
    }

    private bool CheckTruthOrder()
    {
        if (this.truthOrder[0] != 1) { return false; }
        if (this.truthOrder[1] != 2) { return false; }
        if (this.truthOrder[2] != 3) { return false; }
        if (this.truthOrder[3] != 4) { return false; }
        if (this.truthOrder[4] != 5) { return false; }
        if (this.truthOrder[5] != 6) { return false; }
        if (this.truthOrder[6] != 7) { return false; }
        if (this.truthOrder[7] != 8) { return false; }
        if (this.truthOrder[8] != 9) { return false; }
        if (this.truthOrder[9] != 10) { return false; }

        return true;
    }

    private bool CheckElementalOrder()
    {
        if (this.elementalOrder[0] != 1) { return false; }
        if (this.elementalOrder[1] != 2) { return false; }
        if (this.elementalOrder[2] != 3) { return false; }
        if (this.elementalOrder[3] != 4) { return false; }
        if (this.elementalOrder[4] != 5) { return false; }
        if (this.elementalOrder[5] != 6) { return false; }
        if (this.elementalOrder[6] != 7) { return false; }
        if (this.elementalOrder[7] != 8) { return false; }

        return true;
    }

    private bool CheckSongOrder()
    {
        if (this.songOrder[0] != 1) { return false; }
        if (this.songOrder[1] != 2) { return false; }
        if (this.songOrder[2] != 3) { return false; }
        if (this.songOrder[3] != 4) { return false; }

        return true;
    }


    private void buttonFact_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.enabled = false;
        if (btn.Equals(buttonFact1)) { this.factOrder.Add(1); }
        if (btn.Equals(buttonFact2)) { this.factOrder.Add(2); }
        if (btn.Equals(buttonFact3)) { this.factOrder.Add(3); }
        if (btn.Equals(buttonFact4)) { this.factOrder.Add(4); }
        if (btn.Equals(buttonFact5)) { this.factOrder.Add(5); }
        if (btn.Equals(buttonFact6)) { this.factOrder.Add(6); }
        if (btn.Equals(buttonFact7)) { this.factOrder.Add(7); }
        if (btn.Equals(buttonFact8)) { this.factOrder.Add(8); }
        if (btn.Equals(buttonFact9)) { this.factOrder.Add(9); }
        if (btn.Equals(buttonFact10)) { this.factOrder.Add(10); }

        if (this.factOrder.Count >= 10)
        {
            this.flagC = true;

            MessagePack.Message14139(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }
    }

    private void buttonTruth_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.enabled = false;
        if (btn.Equals(buttonTruth1)) { this.truthOrder.Add(1); }
        if (btn.Equals(buttonTruth2)) { this.truthOrder.Add(2); }
        if (btn.Equals(buttonTruth3)) { this.truthOrder.Add(3); }
        if (btn.Equals(buttonTruth4)) { this.truthOrder.Add(4); }
        if (btn.Equals(buttonTruth5)) { this.truthOrder.Add(5); }
        if (btn.Equals(buttonTruth6)) { this.truthOrder.Add(6); }
        if (btn.Equals(buttonTruth7)) { this.truthOrder.Add(7); }
        if (btn.Equals(buttonTruth8)) { this.truthOrder.Add(8); }
        if (btn.Equals(buttonTruth9)) { this.truthOrder.Add(9); }
        if (btn.Equals(buttonTruth10)) { this.truthOrder.Add(10); }

        if (this.truthOrder.Count >= 10)
        {
            this.flagF = true;

            MessagePack.Message14140(ref this.nowMessage, ref this.nowEvent);
            tapOK();
        }
    }

    private void buttonElemental_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.enabled = false;
        if (btn.Equals(buttonElemental1)) { this.elementalOrder.Add(1); }
        if (btn.Equals(buttonElemental2)) { this.elementalOrder.Add(2); }
        if (btn.Equals(buttonElemental3)) { this.elementalOrder.Add(3); }
        if (btn.Equals(buttonElemental4)) { this.elementalOrder.Add(4); }
        if (btn.Equals(buttonElemental5)) { this.elementalOrder.Add(5); }
        if (btn.Equals(buttonElemental6)) { this.elementalOrder.Add(6); }
        if (btn.Equals(buttonElemental7)) { this.elementalOrder.Add(7); }
        if (btn.Equals(buttonElemental8)) { this.elementalOrder.Add(8); }

        if (this.elementalOrder.Count >= 8)
        {
            this.flagH = true;
            if (CheckElementalOrder())
            {
                MessagePack.Message14141(ref this.nowMessage, ref this.nowEvent);
                tapOK();
            }
            else
            {
                MessagePack.Message14141_2(ref this.nowMessage, ref this.nowEvent);
                tapOK();
            }
        }
    }

    private void buttonSong_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.enabled = false;
        if (btn.Equals(buttonSong1)) { this.songOrder.Add(1); }
        if (btn.Equals(buttonSong2)) { this.songOrder.Add(2); }
        if (btn.Equals(buttonSong3)) { this.songOrder.Add(3); }
        if (btn.Equals(buttonSong4)) { this.songOrder.Add(4); }

        if (this.songOrder.Count >= 4)
        {
            this.flagJ = true;
            if (CheckSongOrder())
            {
                MessagePack.Message14142(ref this.nowMessage, ref this.nowEvent);
                tapOK();
            }
            else
            {
                MessagePack.Message14142_2(ref this.nowMessage, ref this.nowEvent);
                tapOK();
            }
        }
    }
}

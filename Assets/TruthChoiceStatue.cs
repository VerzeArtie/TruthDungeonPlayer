using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;

public class TruthChoiceStatue : MotherForm {

    // gui
    public GameObject groupFact;
    public GameObject groupTruth;
    public GameObject groupElement;
    public GameObject groupSong;
    public Text mainMessage;
    public Button btnOK;
    public Button buttonChoice1;
    public Button buttonChoice2;
    public Button buttonChoice3;
    public Button buttonChoice4;
    public Button buttonFact1;
    public Button buttonFact2;
    public Button buttonFact3;
    public Button buttonFact4;
    public Button buttonFact5;
    public Button buttonFact6;
    public Button buttonFact7;
    public Button buttonFact8;
    public Button buttonFact9;
    public Button buttonFact10;
    public Button buttonTruth1;
    public Button buttonTruth2;
    public Button buttonTruth3;
    public Button buttonTruth4;
    public Button buttonTruth5;
    public Button buttonTruth6;
    public Button buttonTruth7;
    public Button buttonTruth8;
    public Button buttonTruth9;
    public Button buttonTruth10;
    public Button buttonElemental1;
    public Button buttonElemental2;
    public Button buttonElemental3;
    public Button buttonElemental4;
    public Button buttonElemental5;
    public Button buttonElemental6;
    public Button buttonElemental7;
    public Button buttonElemental8;
    public Button buttonSong1;
    public Button buttonSong2;
    public Button buttonSong3;
    public Button buttonSong4;

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
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueHideWalkFront)
            {
                buttonChoice4.gameObject.SetActive(false);
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewHuman)
            {
                this.buttonChoice1.gameObject.SetActive(true);
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewLana)
            {
                this.buttonChoice2.gameObject.SetActive(true);
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewFeltus)
            {
                this.buttonChoice3.gameObject.SetActive(true);
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueChangeChoice)
            {
                ChangeChoice();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewTruth)
            {
                ViewButtonTruth();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewFact)
            {
                ViewButtonFact();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewElement)
            {
                ViewButtonElement();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonStatueViewSong)
            {
                ViewButtonSong();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonBadEnd)
            {
                Method.ExecSave(null, Database.WorldSaveNum, true);
                SceneDimension.JumpToTitle();
            }
            else if (currentEvent == MessagePack.ActionEvent.DungeonGoSeeker)
            {
                GroundOne.WE2.SelectFalseStatue = true;
                GroundOne.WE2.RealWorld = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld();
                Method.ExecSave(null, Database.WorldSaveNum, true);
                SceneDimension.CallSaveLoad(this, true, true);
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
                this.Filter.SetActive(false);
            }
        }
    }

    private void ChangeChoice()
    {
        if (this.flag1 && this.flag2 && this.flag3)
        {
            this.buttonChoice1.gameObject.SetActive(false);
            this.buttonChoice2.gameObject.SetActive(false);
            this.buttonChoice3.gameObject.SetActive(false);
            this.flagA = true;

            MessagePack.Message14134(ref this.nowMessage, ref this.nowEvent);
        }
    }

    private void ViewButtonTruth()
    {
        groupTruth.SetActive(true);
        buttonTruth1.gameObject.SetActive(true);
        buttonTruth2.gameObject.SetActive(true);
        buttonTruth3.gameObject.SetActive(true);
        buttonTruth4.gameObject.SetActive(true);
        buttonTruth5.gameObject.SetActive(true);
        buttonTruth6.gameObject.SetActive(true);
        buttonTruth7.gameObject.SetActive(true);
        buttonTruth8.gameObject.SetActive(true);
        buttonTruth9.gameObject.SetActive(true);
        buttonTruth10.gameObject.SetActive(true);
    }

    private void ViewButtonFact()
    {
        groupFact.SetActive(true);
        buttonFact1.gameObject.SetActive(true);
        buttonFact2.gameObject.SetActive(true);
        buttonFact3.gameObject.SetActive(true);
        buttonFact4.gameObject.SetActive(true);
        buttonFact5.gameObject.SetActive(true);
        buttonFact6.gameObject.SetActive(true);
        buttonFact7.gameObject.SetActive(true);
        buttonFact8.gameObject.SetActive(true);
        buttonFact9.gameObject.SetActive(true);
        buttonFact10.gameObject.SetActive(true);
    }
    private void ViewButtonElement()
    {
        groupElement.SetActive(true);
        buttonElemental1.gameObject.SetActive(true);
        buttonElemental2.gameObject.SetActive(true);
        buttonElemental3.gameObject.SetActive(true);
        buttonElemental4.gameObject.SetActive(true);
        buttonElemental5.gameObject.SetActive(true);
        buttonElemental6.gameObject.SetActive(true);
        buttonElemental7.gameObject.SetActive(true);
        buttonElemental8.gameObject.SetActive(true);
    }

    private void ViewButtonSong()
    {
        groupSong.SetActive(true);
        buttonSong1.gameObject.SetActive(true);
        buttonSong2.gameObject.SetActive(true);
        buttonSong3.gameObject.SetActive(true);
        buttonSong4.gameObject.SetActive(true);
    }

    // 人間の像
    public void buttonChoice1_Click()
    {
        MessagePack.Message14135(ref this.nowMessage, ref this.nowEvent, ref this.flagA, ref this.flag1, ref this.flagB, ref this.flag5, ref this.flagC, ref this.flagD, ref this.flagE, ref this.flagF, ref this.flagG, ref this.flagH, ref this.flagI, ref this.flagJ, ref this.flagK);
        tapOK();
    }

    // ラナ・アミリアの像
    public void buttonChoice2_Click()
    {
        MessagePack.Message14136(ref this.nowMessage, ref this.nowEvent, ref this.flagA, ref this.flag2, ref this.flagB, ref this.flag4, ref this.flag4_2, ref this.flagC, ref this.flagD, this.factOrder, ref this.flagE, ref this.flag7, ref this.flagF, ref this.flagG, this.truthOrder, ref this.flagH, ref this.flagI, ref this.flagJ, ref this.flagK);
        tapOK();
    }

    // フェルトゥーシュの剣
    public void buttonChoice3_Click()
    {
        MessagePack.Message14137(ref this.nowMessage, ref this.nowEvent, ref this.flagA, ref this.flag3, ref this.flagB, ref this.flag6, ref this.flag6_2, ref this.flag6_3, ref this.flagC, ref this.flagD, ref this.flagE, ref this.flagF, ref this.flagG, ref this.flagH, ref this.flagI, ref this.flagJ, ref this.flagK);
        tapOK();
    }

    // 歩を進める
    public void buttonChoice4_Click()
    {
        MessagePack.Message14138(ref this.nowMessage, ref this.nowEvent, ref this.flag0);
        tapOK();
    }
    
    // ラナ・アミリアの像(CheckFactOrder)はMessagePackにある
    // 人間の像(CheckTruthOrder)はMessagePackにある
    // 神剣フェルトゥーシュ
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

    // 神々の詩
    private bool CheckSongOrder()
    {
        if (this.songOrder[0] != 1) { return false; }
        if (this.songOrder[1] != 2) { return false; }
        if (this.songOrder[2] != 3) { return false; }
        if (this.songOrder[3] != 4) { return false; }

        return true;
    }


    public void buttonFact_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.interactable = false;
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

    public void buttonTruth_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.interactable = false;
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

    public void buttonElemental_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.interactable = false;
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

    public void buttonSong_Click(Button sender)
    {
        Button btn = (Button)sender;
        btn.interactable = false;
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

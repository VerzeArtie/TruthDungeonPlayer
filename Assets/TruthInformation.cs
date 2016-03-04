using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DungeonPlayer
{
    public class TruthInformation : MotherForm
    {
        Font defaultFont;
        int baseWidth = 0;
        int baseHeight = 0;
        int extWidth = 0;
        int extHeight = 0;
        int TL_LocX = 30;
        int TL_LocY = 110;
        int TL_Margin = 110;
        int TLE_Margin_X = 100;
        int TLE_Margin_Y = 40;
        float baseFontSize = 20.25F;
        float extFontSize = 14.00F;
        int ARCHETYPE_SIZE_X = 160;
        int ARCHETYPE_SIZE_Y = 80;
        int ARCHETYPE_LocX = 100;
        int ARCHETYPE_LocY = 170;

        // GUI
        public GameObject groupAttribute;
        public GameObject groupElement;
        public GameObject groupMixElement;
        public GameObject groupCommand;
        public GameObject groupCurrent;
        public Button[] AttributeButton;
        public Text[] AttributeButtonText;
        public Button[] ElementButton;
        public Text[] ElementButtonText;
        public Button[] MixElementButton;
        public Text[] MixElementButtonText;
        public Button[] CommandButton;
        public Text[] CommandButtonText;
        public Image CurrentImage;
        public Text CurrentLabel_JP;
        public Text CurrentLabel_EN;
        public Text CurrentCost;
        public Text CurrentTarget;
        public Text CurrentTiming;
        public GameObject back_CurrentDescription;
        public Text CurrentDescription;
        public GameObject backPanel;
        public Button CloseButton;

        // Use this for initialization
        public override void Start()
        {
            base.Start();
            // todo
            //defaultFont = CommandLabel_JP.Font;
            //baseWidth = AttributeButton1.Width;
            //baseHeight = AttributeButton2.Height;
            //extWidth = 100;
            //extHeight = 40;

            //if (GroundOne.WE2 != null && GroundOne.WE2.AvailableMixSpellSkill == false)
            //{
            //    MixSpellButton.gameObject.SetActive(false);
            //    MixSkillButton.gameObject.SetActive(false);
            //}
            //if (GroundOne.WE2 != null && GroundOne.WE2.AvailableArcheTypeCommand == false)
            //{
            //    ArcheTypeButton.gameObject.SetActive(false);
            //}
            //SpellButton_Click(null, null);
            button1_Click(ElementButtonText[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                tapClose();
            }
        }

        //private void SkillButton_Click(object sender)
        //{
        //    // まずボタンのサイズを決定
        //    AttributeButton1.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    AttributeButton2.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    AttributeButton3.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    AttributeButton4.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    AttributeButton5.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    AttributeButton6.Size = new System.Drawing.Size(baseWidth, baseHeight);
        //    // 次はレイアウト。つまり位置
        //    AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
        //    AttributeButton2.Location = new Point(TL_LocX, TL_LocY + TL_Margin);
        //    AttributeButton3.Location = new Point(TL_LocX + TL_Margin, TL_LocY);
        //    AttributeButton4.Location = new Point(TL_LocX + TL_Margin, TL_LocY + TL_Margin);
        //    AttributeButton5.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY);
        //    AttributeButton6.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY + TL_Margin);
        //    MixAttribute7.Location = new Point(-100, -100);
        //    MixAttribute8.Location = new Point(-100, -100);
        //    MixAttribute9.Location = new Point(-100, -100);
        //    MixAttribute10.Location = new Point(-100, -100);
        //    MixAttribute11.Location = new Point(-100, -100);
        //    MixAttribute12.Location = new Point(-100, -100);
        //    MixAttribute13.Location = new Point(-100, -100);
        //    MixAttribute14.Location = new Point(-100, -100);
        //    MixAttribute15.Location = new Point(-100, -100);
        //    // 不要なボタンは非表示（上）にして・・・
        //    MixAttribute7.gameObject.SetActive(false);
        //    MixAttribute8.gameObject.SetActive(false);
        //    MixAttribute9.gameObject.SetActive(false);
        //    MixAttribute10.gameObject.SetActive(false);
        //    MixAttribute11.gameObject.SetActive(false);
        //    MixAttribute12.gameObject.SetActive(false);
        //    MixAttribute13.gameObject.SetActive(false);
        //    MixAttribute14.gameObject.SetActive(false);
        //    MixAttribute15.gameObject.SetActive(false);
        //    // 必要なボタンは表示（上）
        //    AttributeButton1.gameObject.SetActive(true);
        //    AttributeButton2.gameObject.SetActive(true);
        //    AttributeButton3.gameObject.SetActive(true);
        //    AttributeButton4.gameObject.SetActive(true);
        //    AttributeButton5.gameObject.SetActive(true);
        //    AttributeButton6.gameObject.SetActive(true);
        //    // 必要なボタンは表示（下）させて・・・
        //    CommandButtonText[0].gameObject.SetActive(true);
        //    CommandButtonText[1].gameObject.SetActive(true);
        //    CommandButtonText[2].gameObject.SetActive(true);
        //    CommandButtonText[3].gameObject.SetActive(true);
        //    CommandButtonText[4].gameObject.SetActive(false);
        //    CommandButtonText[5].gameObject.SetActive(false);
        //    CommandButtonText[6].gameObject.SetActive(false);
        //    // 上ボタン、色更新
        //    AttributeButton1.BackColor = Color.Gold;
        //    AttributeButton2.BackColor = Color.DarkGray;
        //    AttributeButton3.BackColor = Color.OrangeRed;
        //    AttributeButton4.BackColor = Color.CornflowerBlue;
        //    AttributeButton5.BackColor = Color.LimeGreen;
        //    AttributeButton6.BackColor = Color.White;
        //    // 上ボタン、テキスト更新
        //    AttributeButton1.text = "動";
        //    AttributeButton1.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    AttributeButton2.text = "静";
        //    AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    AttributeButton3.text = "柔";
        //    AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    AttributeButton4.text = "剛";
        //    AttributeButton4.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    AttributeButton5.text = "心眼";
        //    AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    AttributeButton6.text = "無心";
        //    AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    // 最初の下項目のボタンを選択しておく！
        //    button1_Click(null, null);
        //    button7_Click(CommandButtonText[0], null);
        //}

        //private void button15_Click(object sender)
        //{
        //    // まずボタンのサイズを決定
        //    AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
        //    // 次はレイアウト。つまり位置
        //    AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
        //    AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
        //    AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
        //    AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
        //    AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
        //    AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
        //    MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
        //    MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
        //    MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
        //    // 不要なボタンは非表示（上）にして・・・
        //    // 必要なボタンは表示（上）
        //    AttributeButton1.gameObject.SetActive(true);
        //    AttributeButton2.gameObject.SetActive(true);
        //    AttributeButton3.gameObject.SetActive(true);
        //    AttributeButton4.gameObject.SetActive(true);
        //    AttributeButton5.gameObject.SetActive(true);
        //    AttributeButton6.gameObject.SetActive(true);
        //    MixAttribute7.gameObject.SetActive(true);
        //    MixAttribute8.gameObject.SetActive(true);
        //    MixAttribute9.gameObject.SetActive(true);
        //    MixAttribute10.gameObject.SetActive(true);
        //    MixAttribute11.gameObject.SetActive(true);
        //    MixAttribute12.gameObject.SetActive(true);
        //    MixAttribute13.gameObject.SetActive(true);
        //    MixAttribute14.gameObject.SetActive(true);
        //    MixAttribute15.gameObject.SetActive(true);
        //    // 必要なボタンは表示（下）させて・・・
        //    CommandButtonText[0].gameObject.SetActive(true);
        //    CommandButtonText[1].gameObject.SetActive(true);
        //    CommandButtonText[2].gameObject.SetActive(true);
        //    CommandButtonText[3].gameObject.SetActive(false);
        //    CommandButtonText[4].gameObject.SetActive(false);
        //    CommandButtonText[5].gameObject.SetActive(false);
        //    CommandButtonText[6].gameObject.SetActive(false);
        //    // 上ボタン、テキスト更新
        //    AttributeButton1.text = "聖/火";
        //    AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton2.text = "聖/理";
        //    AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton3.text = "火/理";
        //    AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton4.text = "闇/水";
        //    AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton5.text = "闇/空";
        //    AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton6.text = "水/空";
        //    AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute7.text = "聖/水";
        //    MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute8.text = "聖/空";
        //    MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute9.text = "火/空";
        //    MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute10.text = "闇/火";
        //    MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute11.text = "闇/理";
        //    MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute12.text = "水/理";
        //    MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute13.text = "聖/闇";
        //    MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute14.text = "火/水";
        //    MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute15.text = "理/空";
        //    MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    // ボタン配色を変更！
        //    AttributeButton1.BackColor = Color.Cyan;
        //    AttributeButton2.BackColor = Color.Cyan;
        //    AttributeButton3.BackColor = Color.Cyan;
        //    AttributeButton4.BackColor = Color.Cyan;
        //    AttributeButton5.BackColor = Color.Cyan;
        //    AttributeButton6.BackColor = Color.Cyan;
        //    MixAttribute7.BackColor = Color.Yellow;
        //    MixAttribute8.BackColor = Color.Yellow;
        //    MixAttribute9.BackColor = Color.Yellow;
        //    MixAttribute10.BackColor = Color.Yellow;
        //    MixAttribute11.BackColor = Color.Yellow;
        //    MixAttribute12.BackColor = Color.Yellow;
        //    MixAttribute13.BackColor = Color.Magenta;
        //    MixAttribute14.BackColor = Color.Magenta;
        //    MixAttribute15.BackColor = Color.Magenta;
        //    // 最初の下項目のボタンを選択しておく！
        //    button1_Click(null, null);
        //    button7_Click(CommandButtonText[0], null);
        //}

        //private void button16_Click(object sender)
        //{
        //    // まずボタンのサイズを決定
        //    AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
        //    AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
        //    // 次はレイアウト。つまり位置
        //    AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
        //    AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
        //    AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
        //    AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
        //    AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
        //    AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
        //    MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
        //    MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
        //    MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
        //    MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
        //    MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
        //    // 不要なボタンは非表示（上）にして・・・
        //    // 必要なボタンは表示（上）
        //    AttributeButton1.gameObject.SetActive(true);
        //    AttributeButton2.gameObject.SetActive(true);
        //    AttributeButton3.gameObject.SetActive(true);
        //    AttributeButton4.gameObject.SetActive(true);
        //    AttributeButton5.gameObject.SetActive(true);
        //    AttributeButton6.gameObject.SetActive(true);
        //    MixAttribute7.gameObject.SetActive(true);
        //    MixAttribute8.gameObject.SetActive(true);
        //    MixAttribute9.gameObject.SetActive(true);
        //    MixAttribute10.gameObject.SetActive(true);
        //    MixAttribute11.gameObject.SetActive(true);
        //    MixAttribute12.gameObject.SetActive(true);
        //    MixAttribute13.gameObject.SetActive(true);
        //    MixAttribute14.gameObject.SetActive(true);
        //    MixAttribute15.gameObject.SetActive(true);
        //    // 必要なボタンは表示（下）させて・・・
        //    CommandButtonText[0].gameObject.SetActive(true);
        //    CommandButtonText[1].gameObject.SetActive(true);
        //    CommandButtonText[2].gameObject.SetActive(false);
        //    CommandButtonText[3].gameObject.SetActive(false);
        //    CommandButtonText[4].gameObject.SetActive(false);
        //    CommandButtonText[5].gameObject.SetActive(false);
        //    CommandButtonText[6].gameObject.SetActive(false);
        //    // 上ボタン、テキスト更新
        //    AttributeButton1.text = "動/柔";
        //    AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton2.text = "動/心眼";
        //    AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton3.text = "柔/心眼";
        //    AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton4.text = "静/剛";
        //    AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton5.text = "静/無心";
        //    AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    AttributeButton6.text = "剛/無心";
        //    AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute7.text = "動/剛";
        //    MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute8.text = "動/無心";
        //    MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute9.text = "柔/無心";
        //    MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute10.text = "静/柔";
        //    MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute11.text = "静/心眼";
        //    MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute12.text = "剛/心眼";
        //    MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute13.text = "動/静";
        //    MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute14.text = "柔/剛";
        //    MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
        //    MixAttribute15.text = "心眼/無心";
        //    MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize - 2, FontStyle.Bold);
        //    // ボタン配色を変更！
        //    AttributeButton1.BackColor = Color.Cyan;
        //    AttributeButton2.BackColor = Color.Cyan;
        //    AttributeButton3.BackColor = Color.Cyan;
        //    AttributeButton4.BackColor = Color.Cyan;
        //    AttributeButton5.BackColor = Color.Cyan;
        //    AttributeButton6.BackColor = Color.Cyan;
        //    MixAttribute7.BackColor = Color.Yellow;
        //    MixAttribute8.BackColor = Color.Yellow;
        //    MixAttribute9.BackColor = Color.Yellow;
        //    MixAttribute10.BackColor = Color.Yellow;
        //    MixAttribute11.BackColor = Color.Yellow;
        //    MixAttribute12.BackColor = Color.Yellow;
        //    MixAttribute13.BackColor = Color.Magenta;
        //    MixAttribute14.BackColor = Color.Magenta;
        //    MixAttribute15.BackColor = Color.Magenta;
        //    // 最初の下項目のボタンを選択しておく！
        //    button1_Click(null, null);
        //    button7_Click(CommandButtonText[0], null);
        //}

        //private void Archetype_Click(object sender)
        //{
        //    // まずボタンのサイズを決定
        //    AttributeButton1.Size = new System.Drawing.Size(ARCHETYPE_SIZE_X, ARCHETYPE_SIZE_Y);
        //    // 次はレイアウト。つまり位置
        //    AttributeButton1.Location = new Point(ARCHETYPE_LocX, ARCHETYPE_LocY);
        //    // 不要なボタンは非表示（上）にして・・・
        //    AttributeButton2.Location = new Point(-100, -100);
        //    AttributeButton3.Location = new Point(-100, -100);
        //    AttributeButton4.Location = new Point(-100, -100);
        //    AttributeButton5.Location = new Point(-100, -100);
        //    AttributeButton6.Location = new Point(-100, -100);
        //    MixAttribute7.Location = new Point(-100, -100);
        //    MixAttribute8.Location = new Point(-100, -100);
        //    MixAttribute9.Location = new Point(-100, -100);
        //    MixAttribute10.Location = new Point(-100, -100);
        //    MixAttribute11.Location = new Point(-100, -100);
        //    MixAttribute12.Location = new Point(-100, -100);
        //    MixAttribute13.Location = new Point(-100, -100);
        //    MixAttribute14.Location = new Point(-100, -100);
        //    MixAttribute15.Location = new Point(-100, -100);
        //    AttributeButton2.gameObject.SetActive(false);
        //    AttributeButton3.gameObject.SetActive(false);
        //    AttributeButton4.gameObject.SetActive(false);
        //    AttributeButton5.gameObject.SetActive(false);
        //    AttributeButton6.gameObject.SetActive(false);
        //    MixAttribute7.gameObject.SetActive(false);
        //    MixAttribute8.gameObject.SetActive(false);
        //    MixAttribute9.gameObject.SetActive(false);
        //    MixAttribute10.gameObject.SetActive(false);
        //    MixAttribute11.gameObject.SetActive(false);
        //    MixAttribute12.gameObject.SetActive(false);
        //    MixAttribute13.gameObject.SetActive(false);
        //    MixAttribute14.gameObject.SetActive(false);
        //    MixAttribute15.gameObject.SetActive(false);
        //    // 必要なボタンは表示（上）
        //    // 必要なボタンは表示（下）させて・・・
        //    CommandButtonText[0].gameObject.SetActive(true);
        //    CommandButtonText[1].gameObject.SetActive(false);
        //    CommandButtonText[2].gameObject.SetActive(false);
        //    CommandButtonText[3].gameObject.SetActive(false);
        //    CommandButtonText[4].gameObject.SetActive(false);
        //    CommandButtonText[5].gameObject.SetActive(false);
        //    CommandButtonText[6].gameObject.SetActive(false);
        //    // 上ボタン、テキスト更新
        //    AttributeButton1.text = "元核";
        //    AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, baseFontSize, FontStyle.Bold);
        //    // ボタン配色を変更！
        //    AttributeButton1.BackColor = Color.SlateBlue;
        //    // 最初の下項目のボタンを選択しておく！
        //    button1_Click(null, null);
        //    button7_Click(CommandButtonText[0], null);
        //}

        private void button1_Click(Text sender)
        {
            Color targetColor = Color.white;
            if (sender.text == "聖")
            {
                CommandButtonText[0].text = Database.FRESH_HEAL;
                CommandButtonText[1].text = Database.PROTECTION;
                CommandButtonText[2].text = Database.HOLY_SHOCK;
                CommandButtonText[3].text = Database.SAINT_POWER;
                CommandButtonText[4].text = Database.GLORY;
                CommandButtonText[5].text = Database.RESURRECTION;
                CommandButtonText[6].text = Database.CELESTIAL_NOVA;
                targetColor = UnityColor.Gold;
            }
            else if (sender.text == "闇")
            {
                CommandButtonText[0].text = Database.DARK_BLAST;
                CommandButtonText[1].text = Database.SHADOW_PACT;
                CommandButtonText[2].text = Database.LIFE_TAP;
                CommandButtonText[3].text = Database.BLACK_CONTRACT;
                CommandButtonText[4].text = Database.DEVOURING_PLAGUE;
                CommandButtonText[5].text = Database.BLOODY_VENGEANCE;
                CommandButtonText[6].text = Database.DAMNATION;
                targetColor = UnityColor.Darkgray;
            }
            else if (sender.text == "火")
            {
                CommandButtonText[0].text = Database.FIRE_BALL;
                CommandButtonText[1].text = Database.FLAME_AURA;
                CommandButtonText[2].text = Database.HEAT_BOOST;
                CommandButtonText[3].text = Database.FLAME_STRIKE;
                CommandButtonText[4].text = Database.VOLCANIC_WAVE;
                CommandButtonText[5].text = Database.IMMORTAL_RAVE;
                CommandButtonText[6].text = Database.LAVA_ANNIHILATION;
                targetColor = UnityColor.OrangeRed;
            }
            else if (sender.text == "水")
            {
                CommandButtonText[0].text = Database.ICE_NEEDLE;
                CommandButtonText[1].text = Database.ABSORB_WATER;
                CommandButtonText[2].text = Database.CLEANSING;
                CommandButtonText[3].text = Database.FROZEN_LANCE;
                CommandButtonText[4].text = Database.MIRROR_IMAGE;
                CommandButtonText[5].text = Database.PROMISED_KNOWLEDGE;
                CommandButtonText[6].text = Database.ABSOLUTE_ZERO;
                targetColor = UnityColor.CornflowerBlue;
            }
            else if (sender.text == "理")
            {
                CommandButtonText[0].text = Database.WORD_OF_POWER;
                CommandButtonText[1].text = Database.GALE_WIND;
                CommandButtonText[2].text = Database.WORD_OF_LIFE;
                CommandButtonText[3].text = Database.WORD_OF_FORTUNE;
                CommandButtonText[4].text = Database.AETHER_DRIVE;
                CommandButtonText[5].text = Database.GENESIS;
                CommandButtonText[6].text = Database.ETERNAL_PRESENCE;
                targetColor = UnityColor.LimeGreen;
            }
            else if (sender.text == "空")
            {
                CommandButtonText[0].text = Database.DISPEL_MAGIC;
                CommandButtonText[1].text = Database.RISE_OF_IMAGE;
                CommandButtonText[2].text = Database.DEFLECTION;
                CommandButtonText[3].text = Database.TRANQUILITY;
                CommandButtonText[4].text = Database.ONE_IMMUNITY;
                CommandButtonText[5].text = Database.WHITE_OUT;
                CommandButtonText[6].text = Database.TIME_STOP;
                targetColor = UnityColor.White;
            }
            else if (sender.text == "動")
            {
                CommandButtonText[0].text = Database.STRAIGHT_SMASH;
                CommandButtonText[1].text = Database.DOUBLE_SLASH;
                CommandButtonText[2].text = Database.CRUSHING_BLOW;
                CommandButtonText[3].text = Database.SOUL_INFINITY;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.Gold;
            }
            else if (sender.text == "静")
            {
                CommandButtonText[0].text = Database.COUNTER_ATTACK;
                CommandButtonText[1].text = Database.PURE_PURIFICATION;
                CommandButtonText[2].text = Database.ANTI_STUN;
                CommandButtonText[3].text = Database.STANCE_OF_DEATH;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.Darkgray;
            }
            else if (sender.text == "柔")
            {
                CommandButtonText[0].text = Database.STANCE_OF_FLOW;
                CommandButtonText[1].text = Database.ENIGMA_SENSE;
                CommandButtonText[2].text = Database.SILENT_RUSH;
                CommandButtonText[3].text = Database.OBORO_IMPACT;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.OrangeRed;
            }
            else if (sender.text == "剛")
            {
                CommandButtonText[0].text = Database.STANCE_OF_STANDING;
                CommandButtonText[1].text = Database.INNER_INSPIRATION;
                CommandButtonText[2].text = Database.KINETIC_SMASH;
                CommandButtonText[3].text = Database.CATASTROPHE;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.OrangeRed;
            }
            else if (sender.text == "心眼")
            {
                CommandButtonText[0].text = Database.TRUTH_VISION;
                CommandButtonText[1].text = Database.HIGH_EMOTIONALITY;
                CommandButtonText[2].text = Database.STANCE_OF_EYES;
                CommandButtonText[3].text = Database.PAINFUL_INSANITY;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.LimeGreen;
            }
            else if (sender.text == "無心")
            {
                CommandButtonText[0].text = Database.NEGATE;
                CommandButtonText[1].text = Database.VOID_EXTRACTION;
                CommandButtonText[2].text = Database.CARNAGE_RUSH;
                CommandButtonText[3].text = Database.NOTHING_OF_NOTHINGNESS;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
                targetColor = UnityColor.White;
            }
            else if (sender.text == "聖/火")
            {
                CommandButtonText[0].text = Database.FLASH_BLAZE;
                CommandButtonText[1].text = Database.LIGHT_DETONATOR;
                CommandButtonText[2].text = Database.ASCENDANT_METEOR;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "聖/理")
            {
                CommandButtonText[0].text = Database.HOLY_BREAKER;
                CommandButtonText[1].text = Database.EXALTED_FIELD;
                CommandButtonText[2].text = Database.HYMN_CONTRACT;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "火/理")
            {
                CommandButtonText[0].text = Database.ENRAGE_BLAST;
                CommandButtonText[1].text = Database.PIERCING_FLAME;
                CommandButtonText[2].text = Database.SIGIL_OF_HOMURA;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "闇/水")
            {
                CommandButtonText[0].text = Database.BLUE_BULLET;
                CommandButtonText[1].text = Database.DEEP_MIRROR;
                CommandButtonText[2].text = Database.DEATH_DENY;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "闇/空")
            {
                CommandButtonText[0].text = Database.DARKEN_FIELD;
                CommandButtonText[1].text = Database.DOOM_BLADE;
                CommandButtonText[2].text = Database.ECLIPSE_END;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "水/空")
            {
                CommandButtonText[0].text = Database.VANISH_WAVE;
                CommandButtonText[1].text = Database.VORTEX_FIELD;
                CommandButtonText[2].text = Database.BLUE_DRAGON_WILL;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "聖/水")
            {
                CommandButtonText[0].text = Database.SKY_SHIELD;
                CommandButtonText[1].text = Database.SACRED_HEAL;
                CommandButtonText[2].text = Database.EVER_DROPLET;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "聖/空")
            {
                CommandButtonText[0].text = Database.STAR_LIGHTNING;
                CommandButtonText[1].text = Database.ANGEL_BREATH;
                CommandButtonText[2].text = Database.ENDLESS_ANTHEM;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "火/空")
            {
                CommandButtonText[0].text = Database.IMMOLATE;
                CommandButtonText[1].text = Database.PHANTASMAL_WIND;
                CommandButtonText[2].text = Database.RED_DRAGON_WILL;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "闇/火")
            {
                CommandButtonText[0].text = Database.BLACK_FIRE;
                CommandButtonText[1].text = Database.BLAZING_FIELD;
                CommandButtonText[2].text = Database.DEMONIC_IGNITE;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "闇/理")
            {
                CommandButtonText[0].text = Database.WORD_OF_MALICE;
                CommandButtonText[1].text = Database.ABYSS_EYE;
                CommandButtonText[2].text = Database.SIN_FORTUNE;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "水/理")
            {
                CommandButtonText[0].text = Database.WORD_OF_ATTITUDE;
                CommandButtonText[1].text = Database.STATIC_BARRIER;
                CommandButtonText[2].text = Database.AUSTERITY_MATRIX;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "聖/闇")
            {
                CommandButtonText[0].text = Database.PSYCHIC_TRANCE;
                CommandButtonText[1].text = Database.BLIND_JUSTICE;
                CommandButtonText[2].text = Database.TRANSCENDENT_WISH;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "火/水")
            {
                CommandButtonText[0].text = Database.FROZEN_AURA;
                CommandButtonText[1].text = Database.CHILL_BURN;
                CommandButtonText[2].text = Database.ZETA_EXPLOSION;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "理/空")
            {
                CommandButtonText[0].text = Database.SEVENTH_MAGIC;
                CommandButtonText[1].text = Database.PARADOX_IMAGE;
                CommandButtonText[2].text = Database.WARP_GATE;
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "動/柔")
            {
                CommandButtonText[0].text = Database.SWIFT_STEP;
                CommandButtonText[1].text = Database.VIGOR_SENSE;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "動/心眼")
            {
                CommandButtonText[0].text = Database.RUMBLE_SHOUT;
                CommandButtonText[1].text = Database.ONSLAUGHT_HIT;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "柔/心眼")
            {
                CommandButtonText[0].text = Database.PSYCHIC_WAVE;
                CommandButtonText[1].text = Database.NOURISH_SENSE;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "静/剛")
            {
                CommandButtonText[0].text = Database.REFLEX_SPIRIT;
                CommandButtonText[1].text = Database.FATAL_BLOW;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "静/無心")
            {
                CommandButtonText[0].text = Database.TRUST_SILENCE;
                CommandButtonText[1].text = Database.MIND_KILLING;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "剛/無心")
            {
                CommandButtonText[0].text = Database.OUTER_INSPIRATION;
                CommandButtonText[1].text = Database.HARDEST_PARRY;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "動/剛")
            {
                CommandButtonText[0].text = Database.CIRCLE_SLASH;
                CommandButtonText[1].text = Database.RISING_AURA;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "動/無心")
            {
                CommandButtonText[0].text = Database.SMOOTHING_MOVE;
                CommandButtonText[1].text = Database.ASCENSION_AURA;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "柔/無心")
            {
                CommandButtonText[0].text = Database.RECOVER;
                CommandButtonText[1].text = Database.IMPULSE_HIT;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "静/柔")
            {
                CommandButtonText[0].text = Database.FUTURE_VISION;
                CommandButtonText[1].text = Database.UNKNOWN_SHOCK;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "静/心眼")
            {
                CommandButtonText[0].text = Database.SHARP_GLARE;
                CommandButtonText[1].text = Database.CONCUSSIVE_HIT;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "剛/心眼")
            {
                CommandButtonText[0].text = Database.VIOLENT_SLASH;
                CommandButtonText[1].text = Database.ONE_AUTHORITY;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "動/静")
            {
                CommandButtonText[0].text = Database.NEUTRAL_SMASH;
                CommandButtonText[1].text = Database.STANCE_OF_DOUBLE;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "柔/剛")
            {
                CommandButtonText[0].text = Database.SURPRISE_ATTACK;
                CommandButtonText[1].text = Database.STANCE_OF_MYSTIC;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "心眼/無心")
            {
                CommandButtonText[0].text = Database.STANCE_OF_SUDDENNESS;
                CommandButtonText[1].text = Database.SOUL_EXECUTION;
                CommandButtonText[2].text = "";
                CommandButtonText[3].text = "";
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }
            else if (sender.text == "元核")
            {
                CommandButtonText[0].text = Database.ARCHETYPE_EIN_JP;
                CommandButtonText[1].text = "";// Database.ARCHETYPE_RANA_JP;
                CommandButtonText[2].text = "";// Database.ARCHETYPE_OL_JP;
                CommandButtonText[3].text = "";// Database.ARCHETYPE_VERZE_JP;
                CommandButtonText[4].text = "";
                CommandButtonText[5].text = "";
                CommandButtonText[6].text = "";
            }

            this.backPanel.GetComponent<Image>().color = targetColor;
            button7_Click(CommandButtonText[0]);
        }

        private void button7_Click(object sender)
        {
            if (((Text)sender).text == "？？？") return;

            string command = TruthActionCommand.ConvertToEnglish(((Text)sender).text);
            CurrentImage.sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + command);

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    CurrentTarget.text = "敵味方全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    CurrentTarget.text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    CurrentTarget.text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    CurrentTarget.text = "敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    CurrentTarget.text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    CurrentTarget.text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    CurrentTarget.text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    CurrentTarget.text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    CurrentTarget.text = "自分";
                    break;
            }

            CurrentLabel_JP.text = TruthActionCommand.ConvertToJapanese(command);
            CurrentLabel_EN.text = command;
            CurrentDescription.text = TruthActionCommand.GetDescription(command);
            CurrentCost.text = TruthActionCommand.GetCost(command).ToString();
            if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Instant)
            {
                CurrentTiming.text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Normal)
            {
                CurrentTiming.text = "ノーマル";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Sorcery)
            {
                CurrentTiming.text = "ソーサリー";
            }
        }

        // spell, skill, mixspell, mixskill, architect
        public void tapAttribute(Text sender)
        {

        }
        public void tapElement(Text sender)
        {
            // 不要なボタンは非表示（上）にして・・・
            groupMixElement.SetActive(false);
            // 必要なボタンは表示（上）
            groupElement.SetActive(true);
            // 必要なボタンは表示（下）させて・・・
            groupCommand.SetActive(true);
            //// 上ボタン、色更新
            //AttributeButton1.BackColor = Color.Gold;
            //AttributeButton2.BackColor = Color.DarkGray;
            //AttributeButton3.BackColor = Color.OrangeRed;
            //AttributeButton4.BackColor = Color.CornflowerBlue;
            //AttributeButton5.BackColor = Color.LimeGreen;
            //AttributeButton6.BackColor = Color.White;
            // 上ボタン、テキスト更新
            ElementButtonText[0].text = "聖";
            ElementButtonText[1].text = "火";
            ElementButtonText[2].text = "理";
            ElementButtonText[3].text = "闇";
            ElementButtonText[4].text = "水";
            ElementButtonText[5].text = "空";
            // 最初の下項目のボタンを選択しておく！
            button1_Click(sender);
            //button7_Click(CommandButtonText[0], null);
        }
        public void tapActionCommand(Text sender)
        {
            button7_Click(sender);
        }
        public void tapClose()
        {
            // todo
            Application.UnloadLevel(Database.TruthInformation);
        }
    }
}

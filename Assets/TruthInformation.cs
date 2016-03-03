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
        public Text label1;
        public Button SpellButton;
        public Button SkillButton;
        public Text label24;
        public Text label23;
        public Text label22;
        public Text label21;
        public Button AttributeButton1;
        public Button AttributeButton2;
        public Button AttributeButton3;
        public Button AttributeButton4;
        public Button AttributeButton5;
        public Button AttributeButton6;
        public Text label3;
        public Text CommandCost;
        public Text CommandLabel_JP;
        public Text CommandLabel_EN;
        public Text label7;
        public Text CommandTarget;
        public Image pictureBox1;
        public Button CommandButton1;
        public Button CommandButton2;
        public Button CommandButton3;
        public Button CommandButton4;
        public Button CommandButton5;
        public Button CommandButton6;
        public Button CommandButton7;
        public Text Description;
        public Button button14;
        public Button MixSpellButton;
        public Button MixSkillButton;
        public Button MixAttribute7;
        public Button MixAttribute8;
        public Button MixAttribute9;
        public Button MixAttribute10;
        public Button MixAttribute11;
        public Button MixAttribute12;
        public Button MixAttribute13;
        public Button MixAttribute14;
        public Button MixAttribute15;
        public Text label9;
        public Text CommandTiming;
        public Button ArcheTypeButton;

        // Use this for initialization
        public override void Start()
        {
            base.Start();
            defaultFont = CommandLabel_JP.Font;
            baseWidth = AttributeButton1.Width;
            baseHeight = AttributeButton2.Height;
            extWidth = 100;
            extHeight = 40;

            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableMixSpellSkill == false)
            {
                MixSpellButton.gameObject.SetActive(false);
                MixSkillButton.gameObject.SetActive(false);
            }
            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableArcheTypeCommand == false)
            {
                ArcheTypeButton.gameObject.SetActive(false);
            }
            SpellButton_Click(null, null);
            button1_Click(null, null);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                tapClose();
            }
        }

        private void SpellButton_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton2.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton3.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton4.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton5.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton6.Size = new System.Drawing.Size(baseWidth, baseHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX, TL_LocY + TL_Margin);
            AttributeButton3.Location = new Point(TL_LocX + TL_Margin, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX + TL_Margin, TL_LocY + TL_Margin);
            AttributeButton5.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY);
            AttributeButton6.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY + TL_Margin);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            // 不要なボタンは非表示（上）にして・・・
            MixAttribute7.gameObject.SetActive(false);
            MixAttribute8.gameObject.SetActive(false);
            MixAttribute9.gameObject.SetActive(false);
            MixAttribute10.gameObject.SetActive(false);
            MixAttribute11.gameObject.SetActive(false);
            MixAttribute12.gameObject.SetActive(false);
            MixAttribute13.gameObject.SetActive(false);
            MixAttribute14.gameObject.SetActive(false);
            MixAttribute15.gameObject.SetActive(false);
            // 必要なボタンは表示（上）
            AttributeButton1.gameObject.SetActive(true);
            AttributeButton2.gameObject.SetActive(true);
            AttributeButton3.gameObject.SetActive(true);
            AttributeButton4.gameObject.SetActive(true);
            AttributeButton5.gameObject.SetActive(true);
            AttributeButton6.gameObject.SetActive(true);
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.gameObject.SetActive(true);
            CommandButton2.gameObject.SetActive(true);
            CommandButton3.gameObject.SetActive(true);
            CommandButton4.gameObject.SetActive(true);
            CommandButton5.gameObject.SetActive(true);
            CommandButton6.gameObject.SetActive(true);
            CommandButton7.gameObject.SetActive(true);
            // 上ボタン、色更新
            AttributeButton1.BackColor = Color.Gold;
            AttributeButton2.BackColor = Color.DarkGray;
            AttributeButton3.BackColor = Color.OrangeRed;
            AttributeButton4.BackColor = Color.CornflowerBlue;
            AttributeButton5.BackColor = Color.LimeGreen;
            AttributeButton6.BackColor = Color.White;
            // 上ボタン、テキスト更新
            AttributeButton1.text = "聖";
            AttributeButton1.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton2.text = "闇";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton3.text = "火";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton4.text = "水";
            AttributeButton4.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton5.text = "理";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton6.text = "空";
            AttributeButton6.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void SkillButton_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton2.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton3.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton4.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton5.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton6.Size = new System.Drawing.Size(baseWidth, baseHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX, TL_LocY + TL_Margin);
            AttributeButton3.Location = new Point(TL_LocX + TL_Margin, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX + TL_Margin, TL_LocY + TL_Margin);
            AttributeButton5.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY);
            AttributeButton6.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY + TL_Margin);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            // 不要なボタンは非表示（上）にして・・・
            MixAttribute7.gameObject.SetActive(false);
            MixAttribute8.gameObject.SetActive(false);
            MixAttribute9.gameObject.SetActive(false);
            MixAttribute10.gameObject.SetActive(false);
            MixAttribute11.gameObject.SetActive(false);
            MixAttribute12.gameObject.SetActive(false);
            MixAttribute13.gameObject.SetActive(false);
            MixAttribute14.gameObject.SetActive(false);
            MixAttribute15.gameObject.SetActive(false);
            // 必要なボタンは表示（上）
            AttributeButton1.gameObject.SetActive(true);
            AttributeButton2.gameObject.SetActive(true);
            AttributeButton3.gameObject.SetActive(true);
            AttributeButton4.gameObject.SetActive(true);
            AttributeButton5.gameObject.SetActive(true);
            AttributeButton6.gameObject.SetActive(true);
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.gameObject.SetActive(true);
            CommandButton2.gameObject.SetActive(true);
            CommandButton3.gameObject.SetActive(true);
            CommandButton4.gameObject.SetActive(true);
            CommandButton5.gameObject.SetActive(false);
            CommandButton6.gameObject.SetActive(false);
            CommandButton7.gameObject.SetActive(false);
            // 上ボタン、色更新
            AttributeButton1.BackColor = Color.Gold;
            AttributeButton2.BackColor = Color.DarkGray;
            AttributeButton3.BackColor = Color.OrangeRed;
            AttributeButton4.BackColor = Color.CornflowerBlue;
            AttributeButton5.BackColor = Color.LimeGreen;
            AttributeButton6.BackColor = Color.White;
            // 上ボタン、テキスト更新
            AttributeButton1.text = "動";
            AttributeButton1.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton2.text = "静";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton3.text = "柔";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton4.text = "剛";
            AttributeButton4.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton5.text = "心眼";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton6.text = "無心";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
            AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
            AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
            AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
            MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
            // 不要なボタンは非表示（上）にして・・・
            // 必要なボタンは表示（上）
            AttributeButton1.gameObject.SetActive(true);
            AttributeButton2.gameObject.SetActive(true);
            AttributeButton3.gameObject.SetActive(true);
            AttributeButton4.gameObject.SetActive(true);
            AttributeButton5.gameObject.SetActive(true);
            AttributeButton6.gameObject.SetActive(true);
            MixAttribute7.gameObject.SetActive(true);
            MixAttribute8.gameObject.SetActive(true);
            MixAttribute9.gameObject.SetActive(true);
            MixAttribute10.gameObject.SetActive(true);
            MixAttribute11.gameObject.SetActive(true);
            MixAttribute12.gameObject.SetActive(true);
            MixAttribute13.gameObject.SetActive(true);
            MixAttribute14.gameObject.SetActive(true);
            MixAttribute15.gameObject.SetActive(true);
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.gameObject.SetActive(true);
            CommandButton2.gameObject.SetActive(true);
            CommandButton3.gameObject.SetActive(true);
            CommandButton4.gameObject.SetActive(false);
            CommandButton5.gameObject.SetActive(false);
            CommandButton6.gameObject.SetActive(false);
            CommandButton7.gameObject.SetActive(false);
            // 上ボタン、テキスト更新
            AttributeButton1.text = "聖/火";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton2.text = "聖/理";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton3.text = "火/理";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton4.text = "闇/水";
            AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton5.text = "闇/空";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton6.text = "水/空";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute7.text = "聖/水";
            MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute8.text = "聖/空";
            MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute9.text = "火/空";
            MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute10.text = "闇/火";
            MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute11.text = "闇/理";
            MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute12.text = "水/理";
            MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute13.text = "聖/闇";
            MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute14.text = "火/水";
            MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute15.text = "理/空";
            MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.Cyan;
            AttributeButton2.BackColor = Color.Cyan;
            AttributeButton3.BackColor = Color.Cyan;
            AttributeButton4.BackColor = Color.Cyan;
            AttributeButton5.BackColor = Color.Cyan;
            AttributeButton6.BackColor = Color.Cyan;
            MixAttribute7.BackColor = Color.Yellow;
            MixAttribute8.BackColor = Color.Yellow;
            MixAttribute9.BackColor = Color.Yellow;
            MixAttribute10.BackColor = Color.Yellow;
            MixAttribute11.BackColor = Color.Yellow;
            MixAttribute12.BackColor = Color.Yellow;
            MixAttribute13.BackColor = Color.Magenta;
            MixAttribute14.BackColor = Color.Magenta;
            MixAttribute15.BackColor = Color.Magenta;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
            AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
            AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
            AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
            MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
            // 不要なボタンは非表示（上）にして・・・
            // 必要なボタンは表示（上）
            AttributeButton1.gameObject.SetActive(true);
            AttributeButton2.gameObject.SetActive(true);
            AttributeButton3.gameObject.SetActive(true);
            AttributeButton4.gameObject.SetActive(true);
            AttributeButton5.gameObject.SetActive(true);
            AttributeButton6.gameObject.SetActive(true);
            MixAttribute7.gameObject.SetActive(true);
            MixAttribute8.gameObject.SetActive(true);
            MixAttribute9.gameObject.SetActive(true);
            MixAttribute10.gameObject.SetActive(true);
            MixAttribute11.gameObject.SetActive(true);
            MixAttribute12.gameObject.SetActive(true);
            MixAttribute13.gameObject.SetActive(true);
            MixAttribute14.gameObject.SetActive(true);
            MixAttribute15.gameObject.SetActive(true);
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.gameObject.SetActive(true);
            CommandButton2.gameObject.SetActive(true);
            CommandButton3.gameObject.SetActive(false);
            CommandButton4.gameObject.SetActive(false);
            CommandButton5.gameObject.SetActive(false);
            CommandButton6.gameObject.SetActive(false);
            CommandButton7.gameObject.SetActive(false);
            // 上ボタン、テキスト更新
            AttributeButton1.text = "動/柔";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton2.text = "動/心眼";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton3.text = "柔/心眼";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton4.text = "静/剛";
            AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton5.text = "静/無心";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton6.text = "剛/無心";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute7.text = "動/剛";
            MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute8.text = "動/無心";
            MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute9.text = "柔/無心";
            MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute10.text = "静/柔";
            MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute11.text = "静/心眼";
            MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute12.text = "剛/心眼";
            MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute13.text = "動/静";
            MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute14.text = "柔/剛";
            MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute15.text = "心眼/無心";
            MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize - 2, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.Cyan;
            AttributeButton2.BackColor = Color.Cyan;
            AttributeButton3.BackColor = Color.Cyan;
            AttributeButton4.BackColor = Color.Cyan;
            AttributeButton5.BackColor = Color.Cyan;
            AttributeButton6.BackColor = Color.Cyan;
            MixAttribute7.BackColor = Color.Yellow;
            MixAttribute8.BackColor = Color.Yellow;
            MixAttribute9.BackColor = Color.Yellow;
            MixAttribute10.BackColor = Color.Yellow;
            MixAttribute11.BackColor = Color.Yellow;
            MixAttribute12.BackColor = Color.Yellow;
            MixAttribute13.BackColor = Color.Magenta;
            MixAttribute14.BackColor = Color.Magenta;
            MixAttribute15.BackColor = Color.Magenta;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void Archetype_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(ARCHETYPE_SIZE_X, ARCHETYPE_SIZE_Y);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(ARCHETYPE_LocX, ARCHETYPE_LocY);
            // 不要なボタンは非表示（上）にして・・・
            AttributeButton2.Location = new Point(-100, -100);
            AttributeButton3.Location = new Point(-100, -100);
            AttributeButton4.Location = new Point(-100, -100);
            AttributeButton5.Location = new Point(-100, -100);
            AttributeButton6.Location = new Point(-100, -100);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            AttributeButton2.gameObject.SetActive(false);
            AttributeButton3.gameObject.SetActive(false);
            AttributeButton4.gameObject.SetActive(false);
            AttributeButton5.gameObject.SetActive(false);
            AttributeButton6.gameObject.SetActive(false);
            MixAttribute7.gameObject.SetActive(false);
            MixAttribute8.gameObject.SetActive(false);
            MixAttribute9.gameObject.SetActive(false);
            MixAttribute10.gameObject.SetActive(false);
            MixAttribute11.gameObject.SetActive(false);
            MixAttribute12.gameObject.SetActive(false);
            MixAttribute13.gameObject.SetActive(false);
            MixAttribute14.gameObject.SetActive(false);
            MixAttribute15.gameObject.SetActive(false);
            // 必要なボタンは表示（上）
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.gameObject.SetActive(true);
            CommandButton2.gameObject.SetActive(false);
            CommandButton3.gameObject.SetActive(false);
            CommandButton4.gameObject.SetActive(false);
            CommandButton5.gameObject.SetActive(false);
            CommandButton6.gameObject.SetActive(false);
            CommandButton7.gameObject.SetActive(false);
            // 上ボタン、テキスト更新
            AttributeButton1.text = "元核";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.SlateBlue;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.Gold;
            if (AttributeButton1.text == "聖")
            {
                CommandButton1.text = Database.FRESH_HEAL;
                CommandButton2.text = Database.PROTECTION;
                CommandButton3.text = Database.HOLY_SHOCK;
                CommandButton4.text = Database.SAINT_POWER;
                CommandButton5.text = Database.GLORY;
                CommandButton6.text = Database.RESURRECTION;
                CommandButton7.text = Database.CELESTIAL_NOVA;
            }
            else if (AttributeButton1.text == "動")
            {
                CommandButton1.text = Database.STRAIGHT_SMASH;
                CommandButton2.text = Database.DOUBLE_SLASH;
                CommandButton3.text = Database.CRUSHING_BLOW;
                CommandButton4.text = Database.SOUL_INFINITY;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton1.text == "聖/火")
            {
                CommandButton1.text = Database.FLASH_BLAZE;
                CommandButton2.text = Database.LIGHT_DETONATOR;
                CommandButton3.text = Database.ASCENDANT_METEOR;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton1.text == "動/柔")
            {
                CommandButton1.text = Database.SWIFT_STEP;
                CommandButton2.text = Database.VIGOR_SENSE;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton1.text == "元核")
            {
                CommandButton1.text = Database.ARCHETYPE_EIN_JP;
                CommandButton2.text = "";// Database.ARCHETYPE_RANA_JP;
                CommandButton3.text = "";// Database.ARCHETYPE_OL_JP;
                CommandButton4.text = "";// Database.ARCHETYPE_VERZE_JP;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.SlateBlue;
            }

            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.DarkGray;
            if (AttributeButton2.text == "闇")
            {
                CommandButton1.text = Database.DARK_BLAST;
                CommandButton2.text = Database.SHADOW_PACT;
                CommandButton3.text = Database.LIFE_TAP;
                CommandButton4.text = Database.BLACK_CONTRACT;
                CommandButton5.text = Database.DEVOURING_PLAGUE;
                CommandButton6.text = Database.BLOODY_VENGEANCE;
                CommandButton7.text = Database.DAMNATION;
            }
            else if (AttributeButton2.text == "静")
            {
                CommandButton1.text = Database.COUNTER_ATTACK;
                CommandButton2.text = Database.PURE_PURIFICATION;
                CommandButton3.text = Database.ANTI_STUN;
                CommandButton4.text = Database.STANCE_OF_DEATH;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton2.text == "聖/理")
            {
                CommandButton1.text = Database.HOLY_BREAKER;
                CommandButton2.text = Database.EXALTED_FIELD;
                CommandButton3.text = Database.HYMN_CONTRACT;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton2.text == "動/心眼")
            {
                CommandButton1.text = Database.RUMBLE_SHOUT;
                CommandButton2.text = Database.ONSLAUGHT_HIT;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.OrangeRed;
            if (AttributeButton3.text == "火")
            {
                CommandButton1.text = Database.FIRE_BALL;
                CommandButton2.text = Database.FLAME_AURA;
                CommandButton3.text = Database.HEAT_BOOST;
                CommandButton4.text = Database.FLAME_STRIKE;
                CommandButton5.text = Database.VOLCANIC_WAVE;
                CommandButton6.text = Database.IMMORTAL_RAVE;
                CommandButton7.text = Database.LAVA_ANNIHILATION;
            }
            else if (AttributeButton3.text == "柔")
            {
                CommandButton1.text = Database.STANCE_OF_FLOW;
                CommandButton2.text = Database.ENIGMA_SENSE;
                CommandButton3.text = Database.SILENT_RUSH;
                CommandButton4.text = Database.OBORO_IMPACT;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton3.text == "火/理")
            {
                CommandButton1.text = Database.ENRAGE_BLAST;
                CommandButton2.text = Database.PIERCING_FLAME;
                CommandButton3.text = Database.SIGIL_OF_HOMURA;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton3.text == "柔/心眼")
            {
                CommandButton1.text = Database.PSYCHIC_WAVE;
                CommandButton2.text = Database.NOURISH_SENSE;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.CornflowerBlue;
            if (AttributeButton4.text == "水")
            {
                CommandButton1.text = Database.ICE_NEEDLE;
                CommandButton2.text = Database.ABSORB_WATER;
                CommandButton3.text = Database.CLEANSING;
                CommandButton4.text = Database.FROZEN_LANCE;
                CommandButton5.text = Database.MIRROR_IMAGE;
                CommandButton6.text = Database.PROMISED_KNOWLEDGE;
                CommandButton7.text = Database.ABSOLUTE_ZERO;
            }
            else if (AttributeButton4.text == "剛")
            {
                CommandButton1.text = Database.STANCE_OF_STANDING;
                CommandButton2.text = Database.INNER_INSPIRATION;
                CommandButton3.text = Database.KINETIC_SMASH;
                CommandButton4.text = Database.CATASTROPHE;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton4.text == "闇/水")
            {
                CommandButton1.text = Database.BLUE_BULLET;
                CommandButton2.text = Database.DEEP_MIRROR;
                CommandButton3.text = Database.DEATH_DENY;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton4.text == "静/剛")
            {
                CommandButton1.text = Database.REFLEX_SPIRIT;
                CommandButton2.text = Database.FATAL_BLOW;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.LimeGreen;
            if (AttributeButton5.text == "理")
            {
                CommandButton1.text = Database.WORD_OF_POWER;
                CommandButton2.text = Database.GALE_WIND;
                CommandButton3.text = Database.WORD_OF_LIFE;
                CommandButton4.text = Database.WORD_OF_FORTUNE;
                CommandButton5.text = Database.AETHER_DRIVE;
                CommandButton6.text = Database.GENESIS;
                CommandButton7.text = Database.ETERNAL_PRESENCE;
            }
            else if (AttributeButton5.text == "心眼")
            {
                CommandButton1.text = Database.TRUTH_VISION;
                CommandButton2.text = Database.HIGH_EMOTIONALITY;
                CommandButton3.text = Database.STANCE_OF_EYES;
                CommandButton4.text = Database.PAINFUL_INSANITY;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton5.text == "闇/空")
            {
                CommandButton1.text = Database.DARKEN_FIELD;
                CommandButton2.text = Database.DOOM_BLADE;
                CommandButton3.text = Database.ECLIPSE_END;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton5.text == "静/無心")
            {
                CommandButton1.text = Database.TRUST_SILENCE;
                CommandButton2.text = Database.MIND_KILLING;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.White;
            if (AttributeButton6.text == "空")
            {
                CommandButton1.text = Database.DISPEL_MAGIC;
                CommandButton2.text = Database.RISE_OF_IMAGE;
                CommandButton3.text = Database.DEFLECTION;
                CommandButton4.text = Database.TRANQUILITY;
                CommandButton5.text = Database.ONE_IMMUNITY;
                CommandButton6.text = Database.WHITE_OUT;
                CommandButton7.text = Database.TIME_STOP;
            }
            else if (AttributeButton6.text == "無心")
            {
                CommandButton1.text = Database.NEGATE;
                CommandButton2.text = Database.VOID_EXTRACTION;
                CommandButton3.text = Database.CARNAGE_RUSH;
                CommandButton4.text = Database.NOTHING_OF_NOTHINGNESS;
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (AttributeButton6.text == "水/空")
            {
                CommandButton1.text = Database.VANISH_WAVE;
                CommandButton2.text = Database.VORTEX_FIELD;
                CommandButton3.text = Database.BLUE_DRAGON_WILL;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton6.text == "剛/無心")
            {
                CommandButton1.text = Database.OUTER_INSPIRATION;
                CommandButton2.text = Database.HARDEST_PARRY;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }


        private void button17_Click(object sender, EventArgs e)
        {
            if (MixAttribute7.text == "聖/水")
            {
                CommandButton1.text = Database.SKY_SHIELD;
                CommandButton2.text = Database.SACRED_HEAL;
                CommandButton3.text = Database.EVER_DROPLET;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute7.text == "動/剛")
            {
                CommandButton1.text = Database.CIRCLE_SLASH;
                CommandButton2.text = Database.RISING_AURA;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if (MixAttribute8.text == "聖/空")
            {
                CommandButton1.text = Database.STAR_LIGHTNING;
                CommandButton2.text = Database.ANGEL_BREATH;
                CommandButton3.text = Database.ENDLESS_ANTHEM;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute8.text == "動/無心")
            {
                CommandButton1.text = Database.SMOOTHING_MOVE;
                CommandButton2.text = Database.ASCENSION_AURA;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (MixAttribute9.text == "火/空")
            {
                CommandButton1.text = Database.IMMOLATE;
                CommandButton2.text = Database.PHANTASMAL_WIND;
                CommandButton3.text = Database.RED_DRAGON_WILL;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute9.text == "柔/無心")
            {
                CommandButton1.text = Database.RECOVER;
                CommandButton2.text = Database.IMPULSE_HIT;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (MixAttribute10.text == "闇/火")
            {
                CommandButton1.text = Database.BLACK_FIRE;
                CommandButton2.text = Database.BLAZING_FIELD;
                CommandButton3.text = Database.DEMONIC_IGNITE;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute10.text == "静/柔")
            {
                CommandButton1.text = Database.FUTURE_VISION;
                CommandButton2.text = Database.UNKNOWN_SHOCK;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (MixAttribute11.text == "闇/理")
            {
                CommandButton1.text = Database.WORD_OF_MALICE;
                CommandButton2.text = Database.ABYSS_EYE;
                CommandButton3.text = Database.SIN_FORTUNE;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute11.text == "静/心眼")
            {
                CommandButton1.text = Database.SHARP_GLARE;
                CommandButton2.text = Database.CONCUSSIVE_HIT;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (MixAttribute12.text == "水/理")
            {
                CommandButton1.text = Database.WORD_OF_ATTITUDE;
                CommandButton2.text = Database.STATIC_BARRIER;
                CommandButton3.text = Database.AUSTERITY_MATRIX;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute12.text == "剛/心眼")
            {
                CommandButton1.text = Database.VIOLENT_SLASH;
                CommandButton2.text = Database.ONE_AUTHORITY;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (MixAttribute13.text == "聖/闇")
            {
                CommandButton1.text = Database.PSYCHIC_TRANCE;
                CommandButton2.text = Database.BLIND_JUSTICE;
                CommandButton3.text = Database.TRANSCENDENT_WISH;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute13.text == "動/静")
            {
                CommandButton1.text = Database.NEUTRAL_SMASH;
                CommandButton2.text = Database.STANCE_OF_DOUBLE;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (MixAttribute14.text == "火/水")
            {
                CommandButton1.text = Database.FROZEN_AURA;
                CommandButton2.text = Database.CHILL_BURN;
                CommandButton3.text = Database.ZETA_EXPLOSION;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute14.text == "柔/剛")
            {
                CommandButton1.text = Database.SURPRISE_ATTACK;
                CommandButton2.text = Database.STANCE_OF_MYSTIC;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (MixAttribute15.text == "理/空")
            {
                CommandButton1.text = Database.SEVENTH_MAGIC;
                CommandButton2.text = Database.PARADOX_IMAGE;
                CommandButton3.text = Database.WARP_GATE;
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            else if (MixAttribute15.text == "心眼/無心")
            {
                CommandButton1.text = Database.STANCE_OF_SUDDENNESS;
                CommandButton2.text = Database.SOUL_EXECUTION;
                CommandButton3.text = "";
                CommandButton4.text = "";
                CommandButton5.text = "";
                CommandButton6.text = "";
                CommandButton7.text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (((Button)sender).text == "？？？") return;

            string ext = ".bmp";
            string command = TruthActionCommand.ConvertToEnglish(((Button)sender).Text);
            pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + command + ext);

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    CommandTarget.text = "敵味方全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    CommandTarget.text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    CommandTarget.text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    CommandTarget.text = "敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    CommandTarget.text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    CommandTarget.text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    CommandTarget.text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    CommandTarget.text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    CommandTarget.text = "自分";
                    break;
            }

            CommandLabel_JP.text = TruthActionCommand.ConvertToJapanese(command);
            CommandLabel_EN.text = command;
            Description.text = TruthActionCommand.GetDescription(command);
            CommandCost.text = TruthActionCommand.GetCost(command).ToString();
            if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Instant)
            {
                CommandTiming.text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Sorcery)
            {
                CommandTiming.text = "ソーサリー";
            }
        }

        public void tapClose()
        {
            // todo
            Application.UnloadLevel(Database.TruthInformation);
        }
    }
}

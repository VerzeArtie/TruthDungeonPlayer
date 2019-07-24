using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;
using System;

namespace DungeonPlayer
{
    public class TruthRequestFood : MotherForm
    {
        // GUI
        public GameObject groupFoodMenu;
        public GameObject groupSoulPoint;

        public GameObject groupNew;
        public Text txtNewTitle;
        public Text txtNewDescription;
        public Text txtCloseButton;
        public GameObject groupLevel;
        public Button[] LevelList;
        public Button[] FoodButtonList;
        public Text[] FoodTextList;
        public Text TitleText;
        public Text DescriptionText;
        public Text lblTitle;
        public Text lblClose;
        public List<Text> txtFoodValue;

        public string CurrentSelect { get; set; }

        private static string DESC_11 = DESC_11_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５\r\n  技\r\n  知\r\n  体＋５\r\n  心";
        private static string DESC_12 = DESC_12_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知\r\n  体＋５\r\n  心＋５";
        private static string DESC_13 = DESC_13_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋５\r\n  体\r\n  心＋５";
        private static string DESC_14 = DESC_14_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５\r\n  技＋５\r\n  知\r\n  体\r\n  心";
        private static string DESC_15 = DESC_15_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋５\r\n  知＋５\r\n  体\r\n  心";

        private static string DESC_21 = DESC_21_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋３０\r\n  知\r\n  体＋２０\r\n  心";
        private static string DESC_22 = DESC_22_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２０\r\n  技\r\n  知\r\n  体＋３０\r\n  心";
        private static string DESC_23 = DESC_23_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋２０\r\n  体\r\n  心＋３０";
        private static string DESC_24 = DESC_24_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋３０\r\n  技\r\n  知\r\n  体\r\n  心＋２０";
        private static string DESC_25 = DESC_25_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋３０\r\n  体＋２０\r\n  心";

        private static string DESC_31 = DESC_31_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋８０\r\n  体\r\n  心＋６０";
        private static string DESC_32 = DESC_32_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋８０\r\n  知＋６０\r\n  体＋６０\r\n  心";
        private static string DESC_33 = DESC_33_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋８０\r\n  技\r\n  知\r\n  体＋８０\r\n  心＋４０";
        private static string DESC_34 = DESC_34_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５０\r\n  技＋５０\r\n  知＋５０\r\n  体＋５０\r\n  心＋５０";
        private static string DESC_35 = DESC_35_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋１５０\r\n  体＋１００\r\n  心";

        private static string DESC_41 = DESC_41_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋２５０\r\n  知＋２５０\r\n  体\r\n  心＋２５０";
        private static string DESC_42 = DESC_42_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  技\r\n  知\r\n  体＋２５０\r\n  心＋２５０";
        private static string DESC_43 = DESC_43_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  知＋２５０\r\n  知\r\n  体＋２５０";
        private static string DESC_44 = DESC_44_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋２５０\r\n  知＋２５０\r\n  体＋２５０\r\n  心";
        private static string DESC_45 = DESC_45_MINI + "\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  技＋２５０\r\n  知\r\n  体\r\n  心＋２５０";

        private const string DESC_11_MINI = "か・・・辛い！！でもウマイ！！\r\n　実はハンナが客に応じて辛い配分を全調整してるとの事。";
        private const string DESC_12_MINI = "ほんのりとするオリーブの香りと、アッサリ味に仕立ててあるオニオン味のスープ。非常に好評のため定番メニューの一つとなっている。";
        private const string DESC_13_MINI = "味自体が非常に絶妙で美味しく、歯ごたえも非常に良い。問題はその見た目だが・・・。";
        private const string DESC_14_MINI = "ウサギ独特の臭みを無くし、肉の旨みは残してある。歯ごたえがかなりあるが、噛めばかむほど味が出る。";
        private const string DESC_15_MINI = "魚本来の味を引き出しており、かつ、煮物と非常にマッチしてる。";

        private const string DESC_21_MINI = "新鮮な魚介類の素材を細切りにして散りばめてあるグラタン。";
        private const string DESC_22_MINI = "魚介類独特の臭みを完全に除去し、質の高いテンプラに仕上げられている。大きさ／柔らかさ／食べごたえ共に申し分なく、腹いっぱい食べられる。";
        private const string DESC_23_MINI = "真実は闇の中にこそ潜む。味だけは保証されてるらしい・・・。";
        private const string DESC_24_MINI = "魚とは思えないような歯ごたえのあるジンギスカン。食べた後の後味は良く、何度でも食べたくなる味付け。";
        private const string DESC_25_MINI = "真っ赤なスパゲッティだが、実は全然辛く無いらしい。\r\n　素材の原色を駆使し、着色は一切行ってないそうだ。";

        private const string DESC_31_MINI = "カリっと天ぷら粉で焼き上げた野菜天ぷら。\r\n野菜であることを忘れてしまうぐらい、非常に香ばしい食感が残る。";
        private const string DESC_32_MINI = "固くて歯ごたえの悪いアザラシ肉を十分にほぐし、凍らせた後、焼き、塩をまぶした究極の一品。";
        private const string DESC_33_MINI = "冬の季節、急激な温度変化により身が引き締まったビーフを使用したカレーライス。臭みは一切感じない。";
        private const string DESC_34_MINI = "肉、魚、豆、味噌汁、ご飯、煎茶。全てが揃ったバランスの良い定食。\r\nハンナおばさん自慢の定食。";
        private const string DESC_35_MINI = "何という青さ・・・見ただけで凍えてしまいそうだ。\r\n　食べた時の口いっぱいに広がる感触は一級品のデザートそのものである。";

        private const string DESC_41_MINI = "真っ黒な色のスパゲッティ\r\n見た目がかなり不気味だが・・・スパイスの効いた香りがする。";
        private const string DESC_42_MINI = "ハンバーグの中に小さめに切り刻んだピーナッツが入っている\r\nフワフワとしたジューシーな肉とカリっとしたピーナッツが食欲をそそる。";
        private const string DESC_43_MINI = "表面に真っ赤なトウガラシがかけられているヒレステーキ。\r\nその裏には実はほんのりとハチミツが隠し味として入っており、食べた者には辛さと甘さが同時に響き渡る。";
        private const string DESC_44_MINI = "１番人気のトースト定食といえば、このオレンジトースト。\r\nふんだんに塗られたオレンジジャムとホワイトクリームを乗せたバカでかいトーストは男女問わず人気の一品である。";
        private const string DESC_45_MINI = "食物の匂いが全くしない闇の鍋\r\n　ハンナ叔母さん曰く、美味しいモノはちゃんと入っているとの事。それを信じて食べるしか選択肢は無い。";

        private int[] FOOD_11_VALUE = { 5, 0, 0, 5, 0 };
        private int[] FOOD_12_VALUE = { 0, 0, 0, 5, 5 };
        private int[] FOOD_13_VALUE = { 0, 0, 5, 0, 5 };
        private int[] FOOD_14_VALUE = { 5, 5, 0, 0, 0 };
        private int[] FOOD_15_VALUE = { 0, 5, 5, 0, 0 };
        private int[] FOOD_21_VALUE = { 0, 30, 0, 20, 0 };
        private int[] FOOD_22_VALUE = { 20, 0, 0, 30, 0 };
        private int[] FOOD_23_VALUE = { 0, 0, 20, 0, 30 };
        private int[] FOOD_24_VALUE = { 30, 0, 0, 0, 20 };
        private int[] FOOD_25_VALUE = { 0, 0, 30, 20, 0 };
        private int[] FOOD_31_VALUE = { 0, 0, 80, 0, 60 };
        private int[] FOOD_32_VALUE = { 0, 80, 60, 60, 0 };
        private int[] FOOD_33_VALUE = { 80, 0, 0, 80, 40 };
        private int[] FOOD_34_VALUE = { 50, 50, 50, 50, 50 };
        private int[] FOOD_35_VALUE = { 0, 0, 150, 100, 0 };
        private int[] FOOD_41_VALUE = { 0, 250, 250, 0, 250 };
        private int[] FOOD_42_VALUE = { 250, 0, 0, 250, 250 };
        private int[] FOOD_43_VALUE = { 250, 0, 250, 250, 0 };
        private int[] FOOD_44_VALUE = { 0, 250, 250, 250, 0 };
        private int[] FOOD_45_VALUE = { 250, 250, 0, 0, 250 };

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblTitle.text = Database.GUI_HANNA_INN;
                lblClose.text = Database.GUI_HANNA_ORDEROK;
            }

            // debug
            //GroundOne.WE.AvailableFood2 = true;
            //GroundOne.WE.AvailableFood3 = true;
            //GroundOne.WE.AvailableFood4 = true;
            //GroundOne.WE.AvailableFood5 = true;

            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailableFood2 = true;
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailableFood3 = true;
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailableFood4 = true;
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailableFood5 = true;

            CheckNewContents();
            
            button1_Click();

            InitializeSoulPointView();
            ConstructSoulPointView(GroundOne.MC);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TapSwitchView(int num)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (num == 0)
            {
                groupFoodMenu.SetActive(true);
                groupSoulPoint.SetActive(false);
            }
            else
            {
                groupFoodMenu.SetActive(false);
                groupSoulPoint.SetActive(true);
            }
        }

        public void TapNewClose()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            this.groupNew.SetActive(false);
            CheckNewContents();
        }

        private void CheckNewContents()
        {
            #region "１階"
            if (!GroundOne.WE2.FoodAvailable_11 && (GroundOne.WE2.FoodMixtureDay_11 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_11))
            {
                GroundOne.WE2.FoodAvailable_11 = true;
                this.txtNewTitle.text = Database.FOOD_INAGO_AND_TAMAGO;
                this.txtNewDescription.text = TruthRequestFood.DESC_13;
                this.txtCloseButton.text = "【 " + Database.FOOD_INAGO_AND_TAMAGO + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_12 && (GroundOne.WE2.FoodMixtureDay_12 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_12))
            {
                GroundOne.WE2.FoodAvailable_12 = true;
                this.txtNewTitle.text = Database.FOOD_USAGI;
                this.txtNewDescription.text = TruthRequestFood.DESC_14;
                this.txtCloseButton.text = "【 " + Database.FOOD_USAGI + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_13 && (GroundOne.WE2.FoodMixtureDay_13 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_13))
            {
                GroundOne.WE2.FoodAvailable_13 = true;
                this.txtNewTitle.text = Database.FOOD_SANMA;
                this.txtNewDescription.text = TruthRequestFood.DESC_15;
                this.txtCloseButton.text = "【 " + Database.FOOD_SANMA + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            #endregion
            #region "２階"
            else if (!GroundOne.WE2.FoodAvailable_21 && (GroundOne.WE2.FoodMixtureDay_21 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_21))
            {
                GroundOne.WE2.FoodAvailable_21 = true;
                this.txtNewTitle.text = Database.FOOD_SEA_TENPURA;
                this.txtNewDescription.text = TruthRequestFood.DESC_22;
                this.txtCloseButton.text = "【 " + Database.FOOD_SEA_TENPURA + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_22 && (GroundOne.WE2.FoodMixtureDay_22 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_22))
            {
                GroundOne.WE2.FoodAvailable_22 = true;
                this.txtNewTitle.text = Database.FOOD_TRUTH_YAMINABE_1;
                this.txtNewDescription.text = TruthRequestFood.DESC_23;
                this.txtCloseButton.text = "【 " + Database.FOOD_TRUTH_YAMINABE_1 + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_23 && (GroundOne.WE2.FoodMixtureDay_23 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_23))
            {
                GroundOne.WE2.FoodAvailable_23 = true;
                this.txtNewTitle.text = Database.FOOD_OSAKANA_ZINGISKAN;
                this.txtNewDescription.text = TruthRequestFood.DESC_24;
                this.txtCloseButton.text = "【 " + Database.FOOD_OSAKANA_ZINGISKAN + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_24 && (GroundOne.WE2.FoodMixtureDay_24 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_24))
            {
                GroundOne.WE2.FoodAvailable_24 = true;
                this.txtNewTitle.text = Database.FOOD_RED_HOT_SPAGHETTI;
                this.txtNewDescription.text = TruthRequestFood.DESC_25;
                this.txtCloseButton.text = "【 " + Database.FOOD_RED_HOT_SPAGHETTI + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            #endregion
            #region "３階"
            else if (!GroundOne.WE2.FoodAvailable_31 && (GroundOne.WE2.FoodMixtureDay_31 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_31))
            {
                GroundOne.WE2.FoodAvailable_31 = true;
                this.txtNewTitle.text = Database.FOOD_AZARASI_SHIOYAKI;
                this.txtNewDescription.text = TruthRequestFood.DESC_32;
                this.txtCloseButton.text = "【 " + Database.FOOD_AZARASI_SHIOYAKI + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_32 && (GroundOne.WE2.FoodMixtureDay_32 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_32))
            {
                GroundOne.WE2.FoodAvailable_32 = true;
                this.txtNewTitle.text = Database.FOOD_WINTER_BEEF_CURRY;
                this.txtNewDescription.text = TruthRequestFood.DESC_33;
                this.txtCloseButton.text = "【 " + Database.FOOD_WINTER_BEEF_CURRY + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_33 && (GroundOne.WE2.FoodMixtureDay_33 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_33))
            {
                GroundOne.WE2.FoodAvailable_33 = true;
                this.txtNewTitle.text = Database.FOOD_GATTURI_GOZEN;
                this.txtNewDescription.text = TruthRequestFood.DESC_34;
                this.txtCloseButton.text = "【 " + Database.FOOD_GATTURI_GOZEN + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_34 && (GroundOne.WE2.FoodMixtureDay_34 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_34))
            {
                GroundOne.WE2.FoodAvailable_34 = true;
                this.txtNewTitle.text = Database.FOOD_KOGOERU_DESSERT;
                this.txtNewDescription.text = TruthRequestFood.DESC_35;
                this.txtCloseButton.text = "【 " + Database.FOOD_KOGOERU_DESSERT + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            #endregion
            #region "４階"
            else if (!GroundOne.WE2.FoodAvailable_41 && (GroundOne.WE2.FoodMixtureDay_41 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_41))
            {
                GroundOne.WE2.FoodAvailable_41 = true;
                this.txtNewTitle.text = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                this.txtNewDescription.text = TruthRequestFood.DESC_42;
                this.txtCloseButton.text = "【 " + Database.FOOD_KOROKORO_PIENUS_HAMBURG + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_42 && (GroundOne.WE2.FoodMixtureDay_42 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_42))
            {
                GroundOne.WE2.FoodAvailable_42 = true;
                this.txtNewTitle.text = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                this.txtNewDescription.text = TruthRequestFood.DESC_43;
                this.txtCloseButton.text = "【 " + Database.FOOD_PIRIKARA_HATIMITSU_STEAK + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_43 && (GroundOne.WE2.FoodMixtureDay_43 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_43))
            {
                GroundOne.WE2.FoodAvailable_43 = true;
                this.txtNewTitle.text = Database.FOOD_HUNWARI_ORANGE_TOAST;
                this.txtNewDescription.text = TruthRequestFood.DESC_44;
                this.txtCloseButton.text = "【 " + Database.FOOD_HUNWARI_ORANGE_TOAST + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            else if (!GroundOne.WE2.FoodAvailable_44 && (GroundOne.WE2.FoodMixtureDay_44 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.FoodMixtureDay_44))
            {
                GroundOne.WE2.FoodAvailable_44 = true;
                this.txtNewTitle.text = Database.FOOD_TRUTH_YAMINABE_2;
                this.txtNewDescription.text = TruthRequestFood.DESC_45;
                this.txtCloseButton.text = "【 " + Database.FOOD_TRUTH_YAMINABE_2 + " 】が追加されました！";
                groupNew.SetActive(true);
            }
            #endregion

            if (!GroundOne.WE.AvailableFood2)
            {
                SetupAvailableList(1);

                groupLevel.SetActive(false); // [コメント]：最初は武具種類が増える傾向を見せない演出のため、VisibleはFalse
            }
            else if (GroundOne.WE.AvailableFood2 && !GroundOne.WE.AvailableFood3)
            {
                SetupAvailableList(2);

                groupLevel.SetActive(true);
                LevelList[2].gameObject.SetActive(false);
                LevelList[3].gameObject.SetActive(false);
                LevelList[4].gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableFood2 && GroundOne.WE.AvailableFood3 && !GroundOne.WE.AvailableFood4)
            {
                SetupAvailableList(3);

                groupLevel.SetActive(true);
                LevelList[3].gameObject.SetActive(false);
                LevelList[4].gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableFood2 && GroundOne.WE.AvailableFood3 && GroundOne.WE.AvailableFood4 && !GroundOne.WE.AvailableEquipShop5)
            {
                SetupAvailableList(4);

                groupLevel.SetActive(true);
                LevelList[4].gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableFood2 && GroundOne.WE.AvailableFood3 && GroundOne.WE.AvailableFood4 && GroundOne.WE.AvailableEquipShop5)
            {
                SetupAvailableList(5);
                groupLevel.SetActive(true);
            }
        }

        private void SetupAvailableList(int level)
        {
            const string FOOD_UNKNOWN = "？？？";

            if (level == 1)
            {
                FoodTextList[0].text = Database.FOOD_KATUCARRY;
                FoodTextList[1].text = Database.FOOD_OLIVE_AND_ONION;
                FoodTextList[2].text = Database.FOOD_INAGO_AND_TAMAGO;
                FoodTextList[3].text = Database.FOOD_USAGI;
                FoodTextList[4].text = Database.FOOD_SANMA;

                FoodButtonList[0].gameObject.SetActive(true);
                FoodButtonList[1].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_11) FoodButtonList[2].gameObject.SetActive(true); else FoodTextList[2].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_12) FoodButtonList[3].gameObject.SetActive(true); else FoodTextList[3].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_13) FoodButtonList[4].gameObject.SetActive(true); else FoodTextList[4].text = FOOD_UNKNOWN;
            }
            else if (level == 2)
            {
                FoodTextList[0].text = Database.FOOD_FISH_GURATAN;
                FoodTextList[1].text = Database.FOOD_SEA_TENPURA;
                FoodTextList[2].text = Database.FOOD_TRUTH_YAMINABE_1;
                FoodTextList[3].text = Database.FOOD_OSAKANA_ZINGISKAN;
                FoodTextList[4].text = Database.FOOD_RED_HOT_SPAGHETTI;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_21) FoodButtonList[1].gameObject.SetActive(true); else FoodTextList[1].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_22) FoodButtonList[2].gameObject.SetActive(true); else FoodTextList[2].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_23) FoodButtonList[3].gameObject.SetActive(true); else FoodTextList[3].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_24) FoodButtonList[4].gameObject.SetActive(true); else FoodTextList[4].text = FOOD_UNKNOWN;
            }
            else if (level == 3)
            {
                FoodTextList[0].text = Database.FOOD_HINYARI_YASAI;
                FoodTextList[1].text = Database.FOOD_AZARASI_SHIOYAKI;
                FoodTextList[2].text = Database.FOOD_WINTER_BEEF_CURRY;
                FoodTextList[3].text = Database.FOOD_GATTURI_GOZEN;
                FoodTextList[4].text = Database.FOOD_KOGOERU_DESSERT;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_31) FoodButtonList[1].gameObject.SetActive(true); else FoodTextList[1].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_32) FoodButtonList[2].gameObject.SetActive(true); else FoodTextList[2].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_33) FoodButtonList[3].gameObject.SetActive(true); else FoodTextList[3].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_34) FoodButtonList[4].gameObject.SetActive(true); else FoodTextList[4].text = FOOD_UNKNOWN;
            }
            else if (level == 4)
            {
                FoodTextList[0].text = Database.FOOD_BLACK_BUTTER_SPAGHETTI;
                FoodTextList[1].text = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                FoodTextList[2].text = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                FoodTextList[3].text = Database.FOOD_HUNWARI_ORANGE_TOAST;
                FoodTextList[4].text = Database.FOOD_TRUTH_YAMINABE_2;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_41) FoodButtonList[1].gameObject.SetActive(true); else FoodTextList[1].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_42) FoodButtonList[2].gameObject.SetActive(true); else FoodTextList[2].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_43) FoodButtonList[3].gameObject.SetActive(true); else FoodTextList[3].text = FOOD_UNKNOWN;
                if (GroundOne.WE2.FoodAvailable_44) FoodButtonList[4].gameObject.SetActive(true); else FoodTextList[4].text = FOOD_UNKNOWN;
            }
        }

        public void LevelButton_Click(Text sender)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            int number = 1;
            if (sender.text == "I") { number = 1; }
            else if (sender.text == "II") { number = 2; }
            else if (sender.text == "III") { number = 3; }
            else if (sender.text == "IV") { number = 4; }
            else if (sender.text == "V") { number = 5; }
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_NUMBER, number.ToString(), string.Empty);
            SetupAvailableList(number);
        }

        public void button1_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[0].text, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (FoodTextList[0].text == Database.FOOD_KATUCARRY)
            {
                DescriptionText.text = TruthRequestFood.DESC_11_MINI;
                UpdateFoodValue(FOOD_11_VALUE);
            }
            else if (FoodTextList[0].text == Database.FOOD_FISH_GURATAN)
            {
                DescriptionText.text = TruthRequestFood.DESC_21_MINI;
                UpdateFoodValue(FOOD_21_VALUE);
            }
            else if (FoodTextList[0].text == Database.FOOD_HINYARI_YASAI)
            {
                DescriptionText.text = TruthRequestFood.DESC_31_MINI;
                UpdateFoodValue(FOOD_31_VALUE);
            }
            else if (FoodTextList[0].text == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                DescriptionText.text = TruthRequestFood.DESC_41_MINI;
                UpdateFoodValue(FOOD_41_VALUE);
            }
            else
            {
                return;
            }
            TitleText.text = FoodTextList[0].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button2_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[1].text, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (FoodTextList[1].text == Database.FOOD_OLIVE_AND_ONION)
            {
                DescriptionText.text = TruthRequestFood.DESC_12_MINI;
                UpdateFoodValue(FOOD_12_VALUE);
            }
            else if (FoodTextList[1].text == Database.FOOD_SEA_TENPURA)
            {
                DescriptionText.text = TruthRequestFood.DESC_22_MINI;
                UpdateFoodValue(FOOD_22_VALUE);
            }
            else if (FoodTextList[1].text == Database.FOOD_AZARASI_SHIOYAKI)
            {
                DescriptionText.text = TruthRequestFood.DESC_32_MINI;
                UpdateFoodValue(FOOD_32_VALUE);
            }
            else if (FoodTextList[1].text == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                DescriptionText.text = TruthRequestFood.DESC_42_MINI;
                UpdateFoodValue(FOOD_42_VALUE);
            }
            else
            {
                return;
            }
            TitleText.text = FoodTextList[1].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button3_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[2].text, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (FoodTextList[2].text == Database.FOOD_INAGO_AND_TAMAGO)
            {
                DescriptionText.text = TruthRequestFood.DESC_13_MINI;
                UpdateFoodValue(FOOD_13_VALUE);
            }
            else if (FoodTextList[2].text == Database.FOOD_TRUTH_YAMINABE_1)
            {
                DescriptionText.text = TruthRequestFood.DESC_23_MINI;
                UpdateFoodValue(FOOD_23_VALUE);
            }
            else if (FoodTextList[2].text == Database.FOOD_WINTER_BEEF_CURRY)
            {
                DescriptionText.text = TruthRequestFood.DESC_33_MINI;
                UpdateFoodValue(FOOD_33_VALUE);
            }
            else if (FoodTextList[2].text == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                DescriptionText.text = TruthRequestFood.DESC_43_MINI;
                UpdateFoodValue(FOOD_43_VALUE);
            }
            else
            {
                return;
            }
            TitleText.text = FoodTextList[2].text;
            this.CurrentSelect = TitleText.text;
        }


        public void button4_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[3].text, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (FoodTextList[3].text == Database.FOOD_USAGI)
            {
                DescriptionText.text = TruthRequestFood.DESC_14_MINI;
                UpdateFoodValue(FOOD_14_VALUE);
            }
            else if (FoodTextList[3].text == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                DescriptionText.text = TruthRequestFood.DESC_24_MINI;
                UpdateFoodValue(FOOD_24_VALUE);
            }
            else if (FoodTextList[3].text == Database.FOOD_GATTURI_GOZEN)
            {
                DescriptionText.text = TruthRequestFood.DESC_34_MINI;
                UpdateFoodValue(FOOD_34_VALUE);
            }
            else if (FoodTextList[3].text == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                DescriptionText.text = TruthRequestFood.DESC_44_MINI;
                UpdateFoodValue(FOOD_44_VALUE);
            }
            else
            {
                return;
            }
            TitleText.text = FoodTextList[3].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button5_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[4].text, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (FoodTextList[4].text == Database.FOOD_SANMA)
            {
                DescriptionText.text = TruthRequestFood.DESC_15_MINI;
                UpdateFoodValue(FOOD_15_VALUE);
            }
            else if (FoodTextList[4].text == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                DescriptionText.text = TruthRequestFood.DESC_25_MINI;
                UpdateFoodValue(FOOD_15_VALUE);
            }
            else if (FoodTextList[4].text == Database.FOOD_KOGOERU_DESSERT)
            {
                DescriptionText.text = TruthRequestFood.DESC_35_MINI;
                UpdateFoodValue(FOOD_15_VALUE);
            }
            else if (FoodTextList[4].text == Database.FOOD_TRUTH_YAMINABE_2)
            {
                DescriptionText.text = TruthRequestFood.DESC_45_MINI;
                UpdateFoodValue(FOOD_15_VALUE);
            }
            else
            {
                return;
            }
            TitleText.text = FoodTextList[4].text;
            this.CurrentSelect = TitleText.text;
        }

        private void UpdateFoodValue(int[] foodValue)
        {
            for (int ii = 0; ii < txtFoodValue.Count; ii++)
            {
                txtFoodValue[ii].text = "+" + foodValue[ii].ToString();
            }
        }

        public void Cancel_Click()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            SceneDimension.Back(this);
        }

        public void Order_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_ORDER, this.CurrentSelect, string.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (this.CurrentSelect == Database.FOOD_KATUCARRY)
            {
                EatFood(FOOD_11_VALUE[0], FOOD_11_VALUE[1], FOOD_11_VALUE[2], FOOD_11_VALUE[3], FOOD_11_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_OLIVE_AND_ONION)
            {
                EatFood(FOOD_12_VALUE[0], FOOD_12_VALUE[1], FOOD_12_VALUE[2], FOOD_12_VALUE[3], FOOD_12_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_INAGO_AND_TAMAGO)
            {
                EatFood(FOOD_13_VALUE[0], FOOD_13_VALUE[1], FOOD_13_VALUE[2], FOOD_13_VALUE[3], FOOD_13_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_USAGI)
            {
                EatFood(FOOD_14_VALUE[0], FOOD_14_VALUE[1], FOOD_14_VALUE[2], FOOD_14_VALUE[3], FOOD_14_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_SANMA)
            {
                EatFood(FOOD_15_VALUE[0], FOOD_15_VALUE[1], FOOD_15_VALUE[2], FOOD_15_VALUE[3], FOOD_15_VALUE[4]);
            }
            // ２階
            else if (this.CurrentSelect == Database.FOOD_FISH_GURATAN)
            {
                EatFood(FOOD_21_VALUE[0], FOOD_21_VALUE[1], FOOD_21_VALUE[2], FOOD_21_VALUE[3], FOOD_21_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_SEA_TENPURA)
            {
                EatFood(FOOD_22_VALUE[0], FOOD_22_VALUE[1], FOOD_22_VALUE[2], FOOD_22_VALUE[3], FOOD_22_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_1)
            {
                EatFood(FOOD_23_VALUE[0], FOOD_23_VALUE[1], FOOD_23_VALUE[2], FOOD_23_VALUE[3], FOOD_23_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                EatFood(FOOD_24_VALUE[0], FOOD_24_VALUE[1], FOOD_24_VALUE[2], FOOD_24_VALUE[3], FOOD_24_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                EatFood(FOOD_25_VALUE[0], FOOD_25_VALUE[1], FOOD_25_VALUE[2], FOOD_25_VALUE[3], FOOD_25_VALUE[4]);
            }
            // ３階
            else if (this.CurrentSelect == Database.FOOD_HINYARI_YASAI)
            {
                EatFood(FOOD_31_VALUE[0], FOOD_31_VALUE[1], FOOD_31_VALUE[2], FOOD_31_VALUE[3], FOOD_31_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_AZARASI_SHIOYAKI)
            {
                EatFood(FOOD_32_VALUE[0], FOOD_32_VALUE[1], FOOD_32_VALUE[2], FOOD_32_VALUE[3], FOOD_32_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_WINTER_BEEF_CURRY)
            {
                EatFood(FOOD_33_VALUE[0], FOOD_33_VALUE[1], FOOD_33_VALUE[2], FOOD_33_VALUE[3], FOOD_33_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_GATTURI_GOZEN)
            {
                EatFood(FOOD_34_VALUE[0], FOOD_34_VALUE[1], FOOD_34_VALUE[2], FOOD_34_VALUE[3], FOOD_34_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_KOGOERU_DESSERT)
            {
                EatFood(FOOD_35_VALUE[0], FOOD_35_VALUE[1], FOOD_35_VALUE[2], FOOD_35_VALUE[3], FOOD_35_VALUE[4]);
            }
            // ４階
            else if (this.CurrentSelect == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                EatFood(FOOD_41_VALUE[0], FOOD_41_VALUE[1], FOOD_41_VALUE[2], FOOD_41_VALUE[3], FOOD_41_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                EatFood(FOOD_42_VALUE[0], FOOD_42_VALUE[1], FOOD_42_VALUE[2], FOOD_42_VALUE[3], FOOD_42_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                EatFood(FOOD_43_VALUE[0], FOOD_43_VALUE[1], FOOD_43_VALUE[2], FOOD_43_VALUE[3], FOOD_43_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                EatFood(FOOD_44_VALUE[0], FOOD_44_VALUE[1], FOOD_44_VALUE[2], FOOD_44_VALUE[3], FOOD_44_VALUE[4]);
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_2)
            {
                EatFood(FOOD_45_VALUE[0], FOOD_45_VALUE[1], FOOD_45_VALUE[2], FOOD_45_VALUE[3], FOOD_45_VALUE[4]);
            }

            ((TruthHomeTown)GroundOne.Parent[GroundOne.Parent.Count - 1]).currentRequestFood = this.CurrentSelect;
            SceneDimension.Back(this);
        }

        private void EatFood(int strUp, int aglUp, int intUp, int stmUp, int mindUp)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null && GroundOne.WE.AvailableFirstCharacter) group.Add(GroundOne.MC);
            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && GroundOne.WE.AvailableSecondCharacter) group.Add(GroundOne.SC);
            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && GroundOne.WE.AvailableThirdCharacter) group.Add(GroundOne.TC);
            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].BuffStrength_Food = strUp;
                group[ii].BuffAgility_Food = aglUp;
                group[ii].BuffIntelligence_Food = intUp;
                group[ii].BuffStamina_Food = stmUp;
                group[ii].BuffMind_Food = mindUp;
                group[ii].MaxGain();
            }
        }

        // ソウルポイントの割り振り
        public Text txtAvailableAttributes;
        public Text[] txtSoulValues;
        public Text[] txtSoulAttributeName;
        public Text[] txtSoulAttributeDesc;
        public Image[] backColors;
        public Button btnFirstChara;
        public Button btnSecondChara;
        public Button btnThirdChara;
        public GameObject btnCharacterGroup;
        public Text txtCurrentName;
        MainCharacter currentPlayer = null;

        private void InitializeSoulPointView()
        {
            this.currentPlayer = GroundOne.MC;
            if (GroundOne.MC != null) { btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor; }
            if (GroundOne.SC != null) { btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor; }
            if (GroundOne.TC != null) { btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor; }
            if (GroundOne.WE.AvailableSecondCharacter)
            {
                btnCharacterGroup.SetActive(true);
                btnSecondChara.gameObject.SetActive(true);

                if (GroundOne.WE.AvailableThirdCharacter)
                {
                    btnThirdChara.gameObject.SetActive(true);
                }
                else
                {
                    btnThirdChara.gameObject.SetActive(false);
                }
            }
            else
            {
                btnCharacterGroup.SetActive(false);
            }
        }
        private void ConstructSoulPointView(MainCharacter player)
        {
            string[] soulAttributeName = TruthActionCommand.GetSoulAttributeName();
            string[] soulAttributeDesc = TruthActionCommand.GetSoulAttributeDesc();
            double[] soulAttributeValue = TruthActionCommand.GetSoulAttributeValue();
            for (int ii = 0; ii < txtSoulAttributeName.Length; ii++)
            {
                txtSoulAttributeName[ii].text = soulAttributeName[ii];
                txtSoulAttributeDesc[ii].text = String.Format(soulAttributeDesc[ii], this.currentPlayer.CurrentSoulAttributes[ii] * soulAttributeValue[ii]);
                backColors[ii].color = this.currentPlayer.PlayerStatusColor;
            }

            for (int ii = 0; ii < txtSoulValues.Length; ii++)
            {
                txtSoulValues[ii].text = player.CurrentSoulAttributes[ii].ToString();
            }
            UpdateAvailablePoints();
            txtCurrentName.text = currentPlayer.FullName;
        }

        public void FirstChara_Click()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            this.currentPlayer = GroundOne.MC;
            ConstructSoulPointView(this.currentPlayer);
        }

        public void SecondChara_Click()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            this.currentPlayer = GroundOne.SC;
            ConstructSoulPointView(this.currentPlayer);
        }

        public void ThirdChara_Click()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            this.currentPlayer = GroundOne.TC;
            ConstructSoulPointView(this.currentPlayer);
        }
        private void UpdateSoulAttributeDesc(int number)
        {
            string[] soulAttributeDesc = TruthActionCommand.GetSoulAttributeDesc();
            double[] soulAttributeValue = TruthActionCommand.GetSoulAttributeValue();
            txtSoulAttributeDesc[number].text = String.Format(soulAttributeDesc[number], this.currentPlayer.CurrentSoulAttributes[number] * soulAttributeValue[number]);
        }
        public void TapFactorPlus(int number)
        {
            Debug.Log("CurrentSoulFragment: " + this.currentPlayer.CurrentSoulFragment.ToString());
            Debug.Log("this.currentPlayer.CurrentSoulAttributes[number]: " + this.currentPlayer.CurrentSoulAttributes[number].ToString());
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (this.currentPlayer.CurrentSoulFragment <= 0) { Debug.Log("return 0"); return; }
            if (this.currentPlayer.CurrentSoulAttributes[number] >= Database.MAX_SOUL_ATTRIBUTE) { Debug.Log("return 1"); return; }

            this.currentPlayer.CurrentSoulAttributes[number] += 1;
            Debug.Log("this.currentPlayer.CurrentSoulAttributes[number]: " + this.currentPlayer.CurrentSoulAttributes[number].ToString());
            txtSoulValues[number].text = this.currentPlayer.CurrentSoulAttributes[number].ToString();

            UpdateAvailablePoints();
            UpdateSoulAttributeDesc(number);
        }
        public void TapFactorMinus(int number)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (this.currentPlayer.CurrentSoulFragment >= this.currentPlayer.MaxSoulFragment) { return; }
            if (this.currentPlayer.CurrentSoulAttributes[number] <= 0) { return; }

            this.currentPlayer.CurrentSoulAttributes[number] -= 1;
            txtSoulValues[number].text = this.currentPlayer.CurrentSoulAttributes[number].ToString();

            UpdateAvailablePoints();
            UpdateSoulAttributeDesc(number);
        }

        private void UpdateAvailablePoints()
        {
            txtAvailableAttributes.text = "Available Soul Points: " + this.currentPlayer.CurrentSoulFragment.ToString();
        }
    }
}
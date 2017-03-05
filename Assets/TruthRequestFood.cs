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

        public string CurrentSelect { get; set; }

        private const string DESC_11 = "か・・・辛い！！でもウマイ！！\r\n　実はハンナが客に応じて辛い配分を全調整してるとの事。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５\r\n  技\r\n  知\r\n  体＋５\r\n  心";
        private const string DESC_12 = "ほんのりとするオリーブの香りと、アッサリ味に仕立ててあるオニオン味のスープ。非常に好評のため定番メニューの一つとなっている。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知\r\n  体＋５\r\n  心＋５";
        private const string DESC_13 = "味自体が非常に絶妙で美味しく、歯ごたえも非常に良い。問題はその見た目だが・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋５\r\n  体\r\n  心＋５";
        private const string DESC_14 = "ウサギ独特の臭みを無くし、肉の旨みは残してある。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５\r\n  技＋５\r\n  知\r\n  体\r\n  心";
        private const string DESC_15 = "魚本来の味を引き出しており、かつ、煮物と非常にマッチしてる。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋５\r\n  知＋５\r\n  体\r\n  心";

        private const string DESC_21 = "新鮮な魚介類の素材を細切りにして散りばめてあるグラタン。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋３０\r\n  知\r\n  体＋２０\r\n  心";
        private const string DESC_22 = "魚介類独特の臭みを完全に除去し、質の高いテンプラに仕上げられている。大きさ／柔らかさ／食べごたえ共に申し分なく、腹いっぱい食べられる。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２０\r\n  技\r\n  知\r\n  体＋３０\r\n  心";
        private const string DESC_23 = "真実は闇の中にこそ潜む。味だけは保証されてるらしい・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋２０\r\n  体\r\n  心＋３０";
        private const string DESC_24 = "魚とは思えないような歯ごたえのあるジンギスカン。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋３０\r\n  技\r\n  知\r\n  体\r\n  心＋２０";
        private const string DESC_25 = "真っ赤なスパゲッティだが、実は全然辛く無いらしい。\r\n　素材の原色を駆使し、着色は一切行ってないそうだ。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋３０\r\n  体＋２０\r\n  心";

        private const string DESC_31 = "カリっと天ぷら粉で焼き上げた野菜天ぷら。\r\n野菜であることを忘れてしまうぐらい、非常に香ばしい食感が残る。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋８０\r\n  体\r\n  心＋６０";
        private const string DESC_32 = "固くて歯ごたえの悪いアザラシ肉を十分にほぐし、凍らせた後、焼き、塩をまぶした究極の一品\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋８０\r\n  知＋６０\r\n  体＋６０\r\n  心";
        private const string DESC_33 = "冬の季節、急激な温度変化により身が引き締まったビーフを使用したカレーライス。臭みは一切感じない。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋８０\r\n  技\r\n  知\r\n  体＋８０\r\n  心＋４０";
        private const string DESC_34 = "肉、魚、豆、味噌汁、ご飯、煎茶。全てが揃ったバランスの良い定食。\r\nハンナおばさん自慢の定食。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋５０\r\n  技＋５０\r\n  知＋５０\r\n  体＋５０\r\n  心＋５０";
        private const string DESC_35 = "何という青さ・・・見ただけで凍えてしまいそうだ。\r\n　食べた時の口いっぱいに広がる感触は一級品のデザートそのものである。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技\r\n  知＋１５０\r\n  体＋１００\r\n  心";

        private const string DESC_41 = "真っ黒な色のスパゲッティ\r\n見た目がかなり不気味だが・・・スパイスの効いた香りがする。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋２５０\r\n  知＋２５０\r\n  体\r\n  心＋２５０";
        private const string DESC_42 = "ハンバーグの中に小さめに切り刻んだピーナッツが入っている\r\nフワフワとしたジューシーな肉とカリっとしたピーナッツが食欲をそそる。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  技\r\n  知\r\n  体＋２５０\r\n  心＋２５０";
        private const string DESC_43 = "表面に真っ赤なトウガラシがかけられているヒレステーキ。\r\nその裏には実はほんのりとハチミツが隠し味として入っており、食べた者には辛さと甘さが同時に響き渡る\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  知＋２５０\r\n  知\r\n  体＋２５０";
        private const string DESC_44 = "１番人気のトースト定食といえば、このオレンジトースト。\r\nふんだんに塗られたオレンジジャムとホワイトクリームを乗せたバカでかいトーストは男女問わず人気の一品である。\r\n\r\n食べた次の日は、以下の効果。\r\n  力\r\n  技＋２５０\r\n  知＋２５０\r\n  体＋２５０\r\n  心";
        private const string DESC_45 = "食物の匂いが全くしない闇の鍋\r\n　ハンナ叔母さん曰く、美味しいモノはちゃんと入っているとの事。それを信じて食べるしか選択肢は無い。\r\n\r\n食べた次の日は、以下の効果。\r\n  力＋２５０\r\n  技＋２５０\r\n  知\r\n  体\r\n  心＋２５０";

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
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TapNewClose()
        {
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
            if (level == 1)
            {
                FoodTextList[0].text = Database.FOOD_KATUCARRY;
                FoodTextList[1].text = Database.FOOD_OLIVE_AND_ONION;
                FoodTextList[2].text = Database.FOOD_INAGO_AND_TAMAGO;
                FoodTextList[3].text = Database.FOOD_USAGI;
                FoodTextList[4].text = Database.FOOD_SANMA;

                FoodButtonList[0].gameObject.SetActive(true);
                FoodButtonList[1].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_11) FoodButtonList[2].gameObject.SetActive(true); else FoodButtonList[2].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_12) FoodButtonList[3].gameObject.SetActive(true); else FoodButtonList[3].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_13) FoodButtonList[4].gameObject.SetActive(true); else FoodButtonList[4].gameObject.SetActive(false);
            }
            else if (level == 2)
            {
                FoodTextList[0].text = Database.FOOD_FISH_GURATAN;
                FoodTextList[1].text = Database.FOOD_SEA_TENPURA;
                FoodTextList[2].text = Database.FOOD_TRUTH_YAMINABE_1;
                FoodTextList[3].text = Database.FOOD_OSAKANA_ZINGISKAN;
                FoodTextList[4].text = Database.FOOD_RED_HOT_SPAGHETTI;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_21) FoodButtonList[1].gameObject.SetActive(true); else FoodButtonList[1].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_22) FoodButtonList[2].gameObject.SetActive(true); else FoodButtonList[2].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_23) FoodButtonList[3].gameObject.SetActive(true); else FoodButtonList[3].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_24) FoodButtonList[4].gameObject.SetActive(true); else FoodButtonList[4].gameObject.SetActive(false);
            }
            else if (level == 3)
            {
                FoodTextList[0].text = Database.FOOD_HINYARI_YASAI;
                FoodTextList[1].text = Database.FOOD_AZARASI_SHIOYAKI;
                FoodTextList[2].text = Database.FOOD_WINTER_BEEF_CURRY;
                FoodTextList[3].text = Database.FOOD_GATTURI_GOZEN;
                FoodTextList[4].text = Database.FOOD_KOGOERU_DESSERT;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_31) FoodButtonList[1].gameObject.SetActive(true); else FoodButtonList[1].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_32) FoodButtonList[2].gameObject.SetActive(true); else FoodButtonList[2].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_33) FoodButtonList[3].gameObject.SetActive(true); else FoodButtonList[3].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_34) FoodButtonList[4].gameObject.SetActive(true); else FoodButtonList[4].gameObject.SetActive(false);
            }
            else if (level == 4)
            {
                FoodTextList[0].text = Database.FOOD_BLACK_BUTTER_SPAGHETTI;
                FoodTextList[1].text = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                FoodTextList[2].text = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                FoodTextList[3].text = Database.FOOD_HUNWARI_ORANGE_TOAST;
                FoodTextList[4].text = Database.FOOD_TRUTH_YAMINABE_2;

                FoodButtonList[0].gameObject.SetActive(true);
                if (GroundOne.WE2.FoodAvailable_41) FoodButtonList[1].gameObject.SetActive(true); else FoodButtonList[1].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_42) FoodButtonList[2].gameObject.SetActive(true); else FoodButtonList[2].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_43) FoodButtonList[3].gameObject.SetActive(true); else FoodButtonList[3].gameObject.SetActive(false);
                if (GroundOne.WE2.FoodAvailable_44) FoodButtonList[4].gameObject.SetActive(true); else FoodButtonList[4].gameObject.SetActive(false);
            }
        }

        public void LevelButton_Click(Text sender)
        {
            int number = 1;
            if (sender.text == "I") { number = 1; }
            else if (sender.text == "II") { number = 2; }
            else if (sender.text == "III") { number = 3; }
            else if (sender.text == "IV") { number = 4; }
            else if (sender.text == "V") { number = 5; }
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_NUMBER, number.ToString(), string.Empty);
            SetupAvailableList(number);
        }


        public void button1_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[0].text, string.Empty);
            if (FoodTextList[0].text == Database.FOOD_KATUCARRY)
            {
                DescriptionText.text = TruthRequestFood.DESC_11;
            }
            else if (FoodTextList[0].text == Database.FOOD_FISH_GURATAN)
            {
                DescriptionText.text = TruthRequestFood.DESC_21;
            }
            else if (FoodTextList[0].text == Database.FOOD_HINYARI_YASAI)
            {
                DescriptionText.text = TruthRequestFood.DESC_31;
            }
            else if (FoodTextList[0].text == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                DescriptionText.text = TruthRequestFood.DESC_41;
            }
            TitleText.text = FoodTextList[0].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button2_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[1].text, string.Empty);
            if (FoodTextList[1].text == Database.FOOD_OLIVE_AND_ONION)
            {
                DescriptionText.text = TruthRequestFood.DESC_12;
            }
            else if (FoodTextList[1].text == Database.FOOD_SEA_TENPURA)
            {
                DescriptionText.text = TruthRequestFood.DESC_22;
            }
            else if (FoodTextList[1].text == Database.FOOD_AZARASI_SHIOYAKI)
            {
                DescriptionText.text = TruthRequestFood.DESC_32;
            }
            else if (FoodTextList[1].text == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                DescriptionText.text = TruthRequestFood.DESC_42;
            }
            TitleText.text = FoodTextList[1].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button3_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[2].text, string.Empty);
            if (FoodTextList[2].text == Database.FOOD_INAGO_AND_TAMAGO)
            {
                DescriptionText.text = TruthRequestFood.DESC_13;
            }
            else if (FoodTextList[2].text == Database.FOOD_TRUTH_YAMINABE_1)
            {
                DescriptionText.text = TruthRequestFood.DESC_23;
            }
            else if (FoodTextList[2].text == Database.FOOD_WINTER_BEEF_CURRY)
            {
                DescriptionText.text = TruthRequestFood.DESC_33;
            }
            else if (FoodTextList[2].text == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                DescriptionText.text = TruthRequestFood.DESC_43;
            }
            TitleText.text = FoodTextList[2].text;
            this.CurrentSelect = TitleText.text;
        }


        public void button4_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[3].text, string.Empty);
            if (FoodTextList[3].text == Database.FOOD_USAGI)
            {
                DescriptionText.text = TruthRequestFood.DESC_14;
            }
            else if (FoodTextList[3].text == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                DescriptionText.text = TruthRequestFood.DESC_24;
            }
            else if (FoodTextList[3].text == Database.FOOD_GATTURI_GOZEN)
            {
                DescriptionText.text = TruthRequestFood.DESC_34;
            }
            else if (FoodTextList[3].text == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                DescriptionText.text = TruthRequestFood.DESC_44;
            }
            TitleText.text = FoodTextList[3].text;
            this.CurrentSelect = TitleText.text;
        }

        public void button5_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_FOOD, FoodTextList[4].text, string.Empty);
            if (FoodTextList[4].text == Database.FOOD_SANMA)
            {
                DescriptionText.text = TruthRequestFood.DESC_15;
            }
            else if (FoodTextList[4].text == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                DescriptionText.text = TruthRequestFood.DESC_25;
            }
            else if (FoodTextList[4].text == Database.FOOD_KOGOERU_DESSERT)
            {
                DescriptionText.text = TruthRequestFood.DESC_35;
            }
            else if (FoodTextList[4].text == Database.FOOD_TRUTH_YAMINABE_2)
            {
                DescriptionText.text = TruthRequestFood.DESC_45;
            }
            TitleText.text = FoodTextList[4].text;
            this.CurrentSelect = TitleText.text;
        }

        public void Order_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_REQUESTFOOD_ORDER, this.CurrentSelect, string.Empty);
            if (this.CurrentSelect == Database.FOOD_KATUCARRY)
            {
                EatFood(5, 0, 0, 5, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_OLIVE_AND_ONION)
            {
                EatFood(0, 0, 0, 5, 5);
            }
            else if (this.CurrentSelect == Database.FOOD_INAGO_AND_TAMAGO)
            {
                EatFood(0, 0, 5, 0, 5);
            }
            else if (this.CurrentSelect == Database.FOOD_USAGI)
            {
                EatFood(5, 5, 0, 0, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_SANMA)
            {
                EatFood(0, 5, 5, 0, 0);
            }
            // ２階
            else if (this.CurrentSelect == Database.FOOD_FISH_GURATAN)
            {
                EatFood(0, 30, 0, 20, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_SEA_TENPURA)
            {
                EatFood(20, 0, 0, 30, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_1)
            {
                EatFood(0, 0, 20, 0, 30);
            }
            else if (this.CurrentSelect == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                EatFood(30, 0, 0, 0, 20);
            }
            else if (this.CurrentSelect == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                EatFood(0, 0, 30, 20, 0);
            }
            // ３階
            else if (this.CurrentSelect == Database.FOOD_HINYARI_YASAI)
            {
                EatFood(0, 0, 80, 0, 60);
            }
            else if (this.CurrentSelect == Database.FOOD_AZARASI_SHIOYAKI)
            {
                EatFood(0, 80, 60, 60, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_WINTER_BEEF_CURRY)
            {
                EatFood(80, 0, 0, 80, 40);
            }
            else if (this.CurrentSelect == Database.FOOD_GATTURI_GOZEN)
            {
                EatFood(50, 50, 50, 50, 50);
            }
            else if (this.CurrentSelect == Database.FOOD_KOGOERU_DESSERT)
            {
                EatFood(0, 0, 150, 100, 0);
            }
            // ４階
            else if (this.CurrentSelect == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                EatFood(0, 250, 250, 0, 250);
            }
            else if (this.CurrentSelect == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                EatFood(250, 0, 0, 250, 250);
            }
            else if (this.CurrentSelect == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                EatFood(250, 0, 250, 250, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                EatFood(0, 250, 250, 250, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_2)
            {
                EatFood(250, 250, 0, 0, 250);
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
    }
}
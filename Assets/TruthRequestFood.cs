using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public class TruthRequestFood : MotherForm
    {
        // GUI
        public GameObject groupLevel;
        public Button[] LevelList;
        public Button[] FoodButtonList;
        public Text[] FoodTextList;
        public Text DescriptionText;

        public string CurrentSelect { get; set; }

        // Use this for initialization
        public override void Start()
        {
            base.Start();
            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailableFood2 = true;
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailableFood3 = true;
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailableFood4 = true;
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailableFood5 = true;

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

            button1_Click();
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        private void SetupAvailableList(int level)
        {
            if (level == 1)
            {
                FoodTextList[0].text = Database.FOOD_BIFUKATU;
                FoodTextList[1].text = Database.FOOD_GEKIKARA_CURRY;
                FoodTextList[2].text = Database.FOOD_INAGO;
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

        public void LevelButton_Click(Button sender)
        {
            for (int ii = 0; ii < FoodButtonList.Length; ii++ )
            {
                if (sender == FoodButtonList[ii])
                {
                    SetupAvailableList(ii+1);
                    break;
                }
            }
        }


        public void button1_Click()
        {
            if (FoodTextList[0].text == Database.FOOD_BIFUKATU)
            {
                DescriptionText.text = "　『" + Database.FOOD_BIFUKATU + "』\r\n\r\nボリュームたっぷりで味も申し分が無いと好評。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋５\r\n 体＋５";
                this.CurrentSelect = Database.FOOD_BIFUKATU;
            }
            else if (FoodTextList[0].text == Database.FOOD_FISH_GURATAN)
            {
                DescriptionText.text = "　『" + Database.FOOD_FISH_GURATAN + "』\r\n\r\n新鮮な魚介類の素材を細切りにして散りばめてあるグラタン。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋３０\r\n 体＋２０";
                this.CurrentSelect = Database.FOOD_FISH_GURATAN;
            }
            else if (FoodTextList[0].text == Database.FOOD_HINYARI_YASAI)
            {
                DescriptionText.text = "　『" + Database.FOOD_HINYARI_YASAI + "』\r\n\r\nカリっと天ぷら粉で焼き上げた野菜天ぷら。\r\n野菜であることを忘れてしまうぐらい、非常に香ばしい食感が残る。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋８０\r\n 心＋６０";
                this.CurrentSelect = Database.FOOD_HINYARI_YASAI;
            }
            else if (FoodTextList[0].text == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                DescriptionText.text = "　『" + Database.FOOD_BLACK_BUTTER_SPAGHETTI + "』\r\n\r\n真っ黒な色のスパゲッティ\r\n見た目がかなり不気味だが・・・スパイスの効いた香りがする。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋２５０\r\n 知＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_BLACK_BUTTER_SPAGHETTI;
            }
        }

        public void button2_Click()
        {
            if (FoodTextList[1].text == Database.FOOD_GEKIKARA_CURRY)
            {
                DescriptionText.text = "　『" + Database.FOOD_GEKIKARA_CURRY + "』\r\n\r\nか・・・辛い！！でもウマイ！！\r\n　実はハンナが客に応じて辛い配分を全調整してるとの事。\r\n\r\n食べた次の日は、以下の効果。\r\n 体＋５\r\n 心＋５";
                this.CurrentSelect = Database.FOOD_GEKIKARA_CURRY;
            }
            else if (FoodTextList[1].text == Database.FOOD_SEA_TENPURA)
            {
                DescriptionText.text = "　『" + Database.FOOD_SEA_TENPURA + "』\r\n\r\nクチバシや舌の独特さを完全に除去し、質の高いテンプラに仕立ててある。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２０\r\n 体＋３０";
                this.CurrentSelect = Database.FOOD_SEA_TENPURA;
            }
            else if (FoodTextList[1].text == Database.FOOD_AZARASI_SHIOYAKI)
            {
                DescriptionText.text = "　『" + Database.FOOD_AZARASI_SHIOYAKI + "』\r\n\r\n固くて歯ごたえの悪いアザラシ肉を十分にほぐし、凍らせた後、焼き、塩をまぶした究極の一品\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋８０\r\n 知＋６０\r\n 体＋６０";
                this.CurrentSelect = Database.FOOD_AZARASI_SHIOYAKI;
            }
            else if (FoodTextList[1].text == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                DescriptionText.text = "　『" + Database.FOOD_KOROKORO_PIENUS_HAMBURG + "』\r\n\r\nハンバーグの中に小さめに切り刻んだピーナッツが入っている\r\nフワフワとしたジューシーな肉とカリっとしたピーナッツが食欲をそそる。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 体＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
            }
        }

        public void button3_Click()
        {
            if (FoodTextList[2].text == Database.FOOD_INAGO)
            {
                DescriptionText.text = "　『" + Database.FOOD_INAGO + "』\r\n\r\n味自体が非常に絶妙で、歯ごたえも非常に良い。問題はその見た目だが・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋５\r\n 心＋５";
                this.CurrentSelect = Database.FOOD_INAGO;
            }
            else if (FoodTextList[2].text == Database.FOOD_TRUTH_YAMINABE_1)
            {
                DescriptionText.text = "　『" + Database.FOOD_TRUTH_YAMINABE_1 + "』\r\n\r\n真実は闇の中にこそ潜む。味だけは保証されてるらしい・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋２０\r\n 心＋３０";
                this.CurrentSelect = Database.FOOD_TRUTH_YAMINABE_1;
            }
            else if (FoodTextList[2].text == Database.FOOD_WINTER_BEEF_CURRY)
            {
                DescriptionText.text = "　『" + Database.FOOD_WINTER_BEEF_CURRY + "』\r\n\r\n冬の季節、急激な温度変化により身が引き締まったビーフを使用したカレーライス。臭みは一切感じない。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋８０\r\n 技＋５０\r\n 体＋５０\r\n 心＋５０";
                this.CurrentSelect = Database.FOOD_WINTER_BEEF_CURRY;
            }
            else if (FoodTextList[2].text == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                DescriptionText.text = "　『" + Database.FOOD_PIRIKARA_HATIMITSU_STEAK + "』\r\n\r\n表面に真っ赤なトウガラシがかけられているヒレステーキ。\r\nその裏には実はほんのりとハチミツが隠し味として入っており、食べた者には辛さと甘さが同時に響き渡る\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 知＋２５０\r\n 体＋２５０";
                this.CurrentSelect = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
            }
        }


        public void button4_Click()
        {
            if (FoodTextList[3].text == Database.FOOD_USAGI)
            {
                DescriptionText.text = "　『" + Database.FOOD_USAGI + "』\r\n\r\nウサギ独特の臭みを無くし、肉の旨みは残してある。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋５\r\n 技＋５";
                this.CurrentSelect = Database.FOOD_USAGI;
            }
            else if (FoodTextList[3].text == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                DescriptionText.text = "　『" + Database.FOOD_OSAKANA_ZINGISKAN + "』\r\n\r\n魚とは思えないような歯ごたえのあるジンギスカン。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋３０\r\n 心＋２０";
                this.CurrentSelect = Database.FOOD_OSAKANA_ZINGISKAN;
            }
            else if (FoodTextList[3].text == Database.FOOD_GATTURI_GOZEN)
            {
                DescriptionText.text = "　『" + Database.FOOD_GATTURI_GOZEN + "』\r\n\r\n肉、魚、豆、味噌汁、ご飯、煎茶。全てが揃ったバランスの良い定食。\r\nハンナおばさん自慢の定食。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋６０\r\n 技＋６０\r\n 知＋６０\r\n 体＋６０\r\n 心＋６０";
                this.CurrentSelect = Database.FOOD_GATTURI_GOZEN;
            }
            else if (FoodTextList[3].text == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                DescriptionText.text = "　『" + Database.FOOD_HUNWARI_ORANGE_TOAST + "』\r\n\r\n朝１番のトースト定食といえば、このオレンジトースト。\r\nふんだんに塗られたオレンジジャムとホワイトクリームを乗せたバカでかいトーストは男女問わず人気の一品である。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋２５０\r\n 知＋２５０\r\n 体＋２５０";
                this.CurrentSelect = Database.FOOD_HUNWARI_ORANGE_TOAST;
            }
        }

        public void button5_Click()
        {
            if (FoodTextList[4].text == Database.FOOD_SANMA)
            {
                DescriptionText.text = "  『" + Database.FOOD_SANMA + "』\r\n\r\n魚本来の味を引き出しており、かつ、煮物と非常にマッチしてる。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋５\r\n 知＋５";
                this.CurrentSelect = Database.FOOD_SANMA;
            }
            else if (FoodTextList[4].text == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                DescriptionText.text = "  『" + Database.FOOD_RED_HOT_SPAGHETTI + "』\r\n真っ赤なスパゲッティだが、実は全然辛く無いらしい。\r\n　素材の原色を駆使し、着色は一切行ってないそうだ。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋３０\r\n 体＋２０";
                this.CurrentSelect = Database.FOOD_RED_HOT_SPAGHETTI;
            }
            else if (FoodTextList[4].text == Database.FOOD_KOGOERU_DESSERT)
            {
                DescriptionText.text = "  『" + Database.FOOD_KOGOERU_DESSERT + "』\r\n何という青さ・・・見ただけで凍えてしまいそうだ。\r\n　食べた時の口いっぱいに広がる感触は一級品のデザートそのものである。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋７０\r\n 知＋１００\r\n 心＋１２０";
                this.CurrentSelect = Database.FOOD_KOGOERU_DESSERT;
            }
            else if (FoodTextList[4].text == Database.FOOD_TRUTH_YAMINABE_2)
            {
                DescriptionText.text = "  『" + Database.FOOD_TRUTH_YAMINABE_2 + "』\r\n食物の匂いが全くしない闇の鍋\r\n　ハンナ叔母さん曰く、美味しいモノはちゃんと入っているとの事。それを信じて食べるしか選択肢は無い。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 技＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_TRUTH_YAMINABE_2;
            }
        }

        public void Order_Click()
        {
            if (this.CurrentSelect == Database.FOOD_BIFUKATU)
            {
                EatFood(5, 0, 0, 5, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_INAGO)
            {
                EatFood(0, 0, 5, 0, 5);
            }
            else if (this.CurrentSelect == Database.FOOD_USAGI)
            {
                EatFood(5, 5, 0, 0, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_GEKIKARA_CURRY)
            {
                EatFood(0, 0, 0, 5, 5);
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
                EatFood(80, 50, 0, 50, 50);
            }
            else if (this.CurrentSelect == Database.FOOD_GATTURI_GOZEN)
            {
                EatFood(60, 60, 60, 60, 60);
            }
            else if (this.CurrentSelect == Database.FOOD_KOGOERU_DESSERT)
            {
                EatFood(70, 0, 100, 0, 120);
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
            }
        }
    }
}
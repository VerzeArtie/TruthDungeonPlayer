using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DungeonPlayer
{
    public partial class TruthEquipmentShop : MotherForm
    {
        public GameObject filter;
        public GameObject groupYesNoMessage;
        public Text yesnoMessage;
        public Button btnLevel1;
        public Button btnLevel2;
        public Button btnLevel3;
        public Button btnLevel4;
        public Button btnLevel5;
        public Button btnChara1;
        public Button btnChara2;
        public Button btnChara3;
        public Text mainMessage;
        public Text labelTitle;
        public Text[] equipList;
        public Text[] costList;
        public Text[] backpackList;
        public Text[] backpackStack;
        public GameObject[] backEquip;
        public GameObject[] backCost;
        public GameObject[] back_backpackList;
        public GameObject[] back_backpackStack;
        public Text labelGold;
        public Button buttonBuy;
        public Button buttonSell;
        public Button buttonYes;
        public Button buttonNo;
        public Button buttonYes2;
        public Button buttonNo2;
        public GameObject backMainWeapon;
        public GameObject backSubWeapon;
        public GameObject backArmor;
        public GameObject backAccessory1;
        public GameObject backAccessory2;
        public Text txtMainWeapon;
        public Text txtSubWeapon;
        public Text txtArmor;
        public Text txtAccessory1;
        public Text txtAccessory2;

        private ItemBackPack currentSelectItem = null;
        private ItemBackPack currentSelectItem2 = null;
        private ItemBackPack currentSelectItem3 = null;

        protected int MAX_EQUIPLIST = 25; // 後編編集

        protected MainCharacter ganz;
        protected MainCharacter currentPlayer;

        public override void Start()
        {
            base.Start();

            GameObject objGanz = new GameObject("objGanz");
            ganz = objGanz.AddComponent<MainCharacter>();
            ganz.FirstName = "ガンツ";
            this.currentPlayer = GroundOne.MC;

            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
            {
                btnChara1.gameObject.SetActive(false); // [コメント]：最初はキャラクター増加を見せない演出のため、VisibleはFalse
                btnChara2.gameObject.SetActive(false);
                btnChara3.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
            {
                btnChara1.gameObject.SetActive(true);
                btnChara2.gameObject.SetActive(true);
                btnChara3.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableThirdCharacter)
            {
                btnChara1.gameObject.SetActive(true);
                btnChara2.gameObject.SetActive(true);
                btnChara3.gameObject.SetActive(false); // [コメント]：ストーリーの演出上、ヴェルゼはガンツの武具屋へ訪れる事はないため。
            }

            UpdateBackPackLabel(this.currentPlayer);

            if (/*GroundOne.WE.AvailableEquipShop && */!GroundOne.WE.AvailableEquipShop2)
            {
                SetupAvailableList(1);

                btnLevel1.gameObject.SetActive(false); // [コメント]：最初は武具種類が増える傾向を見せない演出のため、VisibleはFalse
                btnLevel2.gameObject.SetActive(false);
                btnLevel3.gameObject.SetActive(false);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableEquipShop && GroundOne.WE.AvailableEquipShop2 && !GroundOne.WE.AvailableEquipShop3)
            {
                SetupAvailableList(2);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(false);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableEquipShop && GroundOne.WE.AvailableEquipShop2 && GroundOne.WE.AvailableEquipShop3 && !GroundOne.WE.AvailableEquipShop4)
            {
                SetupAvailableList(3);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(true);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableEquipShop && GroundOne.WE.AvailableEquipShop2 && GroundOne.WE.AvailableEquipShop3 && GroundOne.WE.AvailableEquipShop4 && !GroundOne.WE.AvailableEquipShop5)
            {
                SetupAvailableList(4);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(true);
                btnLevel4.gameObject.SetActive(true);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailableEquipShop && GroundOne.WE.AvailableEquipShop2 && GroundOne.WE.AvailableEquipShop3 && GroundOne.WE.AvailableEquipShop4 && GroundOne.WE.AvailableEquipShop5)
            {
                SetupAvailableList(5);
                btnLevel1.gameObject.SetActive (true);
                btnLevel2.gameObject.SetActive (true);
                btnLevel3.gameObject.SetActive (true);
                btnLevel4.gameObject.SetActive (true);
                btnLevel5.gameObject.SetActive (true);
            }
            else
            {
            }

            OnLoadSetupFloorButton();
            OnLoadMessage(); // 後編編集
            this.labelGold.text = GroundOne.MC.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。

            SetupAvailableList(1);
            UpdateBackPackLabelInterface(GroundOne.MC);
            UpdateEquipment(GroundOne.MC);
            this.labelTitle.text = GroundOne.titleName;
            // keydown(EquipmentShop_KeyDown) // todo
            // keyup(TruthEquipmentShop_KeyUp) // todo
        }

        bool nowClose = false;
        bool execClose = false;
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                nowClose = true;
            }
            #region "close window"
            if (nowClose)
            {
                nowClose = false;
                Debug.Log("nowclose line");
                SetupMessageText(3009);
                execClose = true;
            }
            else if (execClose)
            {
                execClose = false;
                System.Threading.Thread.Sleep(1000);
                SceneDimension.Back();
            }
            #endregion
        }

        private void OnLoadSetupFloorButton()
        {
            // todo 派生先、TruthPotionShopで使われるメソッドである。
        }

        protected virtual void OnLoadMessage()
        {
            SetupMessageText(3000);
        }
        //bool IsShift = false;
        //protected void TruthEquipmentShop_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.ShiftKey)
        //    {
        //        this.IsShift = false;
        //    }
        //}
        //protected override void EquipmentShop_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        button1_Click(null, null);
        //    }
        //    else if (e.KeyCode == Keys.ShiftKey)
        //    {
        //        this.IsShift = true;
        //    }
        //}


        protected virtual void UpdateBackPackLabel(MainCharacter target) // 後編編集
        {
            //label11.Text = "バックパック（" + target.Name + ")"; // todo?

            UpdateBackPackLabelInterface(target);
        }
        protected void UpdateBackPackLabelInterface(MainCharacter target)
        {

            ItemBackPack[] temp = target.GetBackPackInfo();

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                int baseNumber = 0;

                if (temp[ii + baseNumber] != null)
                {
                    backpackList[ii].text = temp[ii + baseNumber].Name;
                    Method.UpdateRareColor(temp[ii + baseNumber], backpackList[ii], back_backpackList[ii]);
                    //backpackList[ii].Cursor = System.Windows.Forms.Cursors.Hand; // todo

                    backpackStack[ii].text = "x" + temp[ii + baseNumber].StackValue.ToString();

                }
                else
                {
                    backpackList[ii].text = "";
                    //backpackList[ii].Cursor = System.Windows.Forms.Cursors.Default; // todo

                    backpackStack[ii].text = "";
                    //backpackStack[ii].Cursor = System.Windows.Forms.Cursors.Default; // todo

                    back_backpackList[ii].gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

                }
            }
        }

        protected void UpdateEquipment(MainCharacter player)
        {
            if (player.MainWeapon != null)
            {
                txtMainWeapon.text = player.MainWeapon.Name;
            }
            else
            {
                txtMainWeapon.text = "";
            }
            Method.UpdateRareColor(player.MainWeapon, txtMainWeapon, backMainWeapon);

            if (player.SubWeapon != null)
            {
                txtSubWeapon.text = player.SubWeapon.Name;
            }
            else
            {
                txtSubWeapon.text = "";
            }
            Method.UpdateRareColor(player.SubWeapon, txtSubWeapon, backSubWeapon);

            if (player.MainArmor != null)
            {
                txtArmor.text = player.MainArmor.Name;
            }
            else
            {
                txtArmor.text = "";
            }
            Method.UpdateRareColor(player.MainArmor, txtArmor, backArmor);

            if (player.Accessory != null)
            {
                txtAccessory1.text = player.Accessory.Name;
            }
            else
            {
                txtAccessory1.text = "";
            }
            Method.UpdateRareColor(player.Accessory, txtAccessory1, backAccessory1);

            if (player.Accessory2 != null)
            {
                txtAccessory2.text = player.Accessory2.Name;
            }
            else
            {
                txtAccessory2.text = "";
            }
            Method.UpdateRareColor(player.Accessory2, txtAccessory2, backAccessory2);
        }

        // todo
        //protected override int SelectSellStackValue(object sender, EventArgs e, ItemBackPack backpackData, int ii)
        //{
        //    int exchangeValue = Convert.ToInt32(backpackStack[ii].Text.Remove(0, 1), 10);
        //    if (this.IsShift)
        //    {
        //        using (SelectValue sv = new SelectValue())
        //        {
        //            sv.StartPosition = FormStartPosition.Manual;
        //            sv.Location = new Point(this.Location.X + backpackStack[ii].Location.X, this.Location.Y + backpackStack[ii].Location.Y + 15);
        //            sv.MaxValue = exchangeValue;
        //            sv.ShowDialog();
        //            IsShift = false; // ShowDialog表示先で、Shiftキーは外された場合検知できないため、ココでリセット。
        //            if (sv.DialogResult == DialogResult.Cancel) return -1; // ESCキャンセルは中断とみなす。
        //            return sv.CurrentValue;
        //        }
        //    }
        //    else
        //    {
        //        return exchangeValue;
        //    }
        //}

        protected void SellBackPackItem(ItemBackPack backpackData, Text sender, int stack, int ii)
        {
            int MaxStack = Convert.ToInt32(backpackStack[ii].text.Remove(0, 1), 10);
            int updateWE2Value = 0;

            if (stack >= MaxStack)
            {
                this.currentPlayer.DeleteBackPack(backpackData);
                sender.text = "";
                //sender.Cursor = System.Windows.Forms.Cursors.Default; // todo
                backpackStack[ii].text = "";
                updateWE2Value = MaxStack;
            }
            else
            {
                this.currentPlayer.DeleteBackPack(backpackData, stack);
                backpackStack[ii].text = "x" + Convert.ToString(MaxStack - stack);
                updateWE2Value = MaxStack - stack;
            }

            // 素材売却情報を記憶する。
            #region "武具"
            #region "１階"
            if (backpackData.Name == Database.COMMON_WARM_NO_KOUKAKU)
            {
                GroundOne.WE2.EquipMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BEATLE_TOGATTA_TUNO)
            {
                GroundOne.WE2.EquipMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TAKA_FETHER)
            {
                GroundOne.WE2.EquipMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SUN_LEAF)
            {
                GroundOne.WE2.EquipMaterial_14 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_OOKAMI_FANG)
            {
                GroundOne.WE2.EquipMaterial_15 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TOGE_HAETA_SYOKUSYU)
            {
                GroundOne.WE2.EquipMaterial_16 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ORANGE_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_17 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_YELLOW_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_18 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_COPPER)
            {
                GroundOne.WE2.EquipMaterial_19 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RABBIT_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_110 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SPIDER_SILK)
            {
                GroundOne.WE2.EquipMaterial_111 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_WHITE_MAGATAMA)
            {
                GroundOne.WE2.EquipMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_MAGATAMA)
            {
                GroundOne.WE2.EquipMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WASI_BLUE_FEATHER)
            {
                GroundOne.WE2.EquipMaterial_23 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUEWHITE_SHARP_TOGE)
            {
                GroundOne.WE2.EquipMaterial_24 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GOTUGOTU_KARA)
            {
                GroundOne.WE2.EquipMaterial_25 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_SEKIKASSYOKU_HASAMI)
            {
                GroundOne.WE2.EquipMaterial_26 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_ARM)
            {
                GroundOne.WE2.EquipMaterial_27 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_AOSAME_KENSHI)
            {
                GroundOne.WE2.EquipMaterial_28 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KOUSITUKA_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_29 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_AOSAME_UROKO)
            {
                GroundOne.WE2.EquipMaterial_210 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_HALF_TRANSPARENT_ROCK_ASH)
            {
                GroundOne.WE2.EquipMaterial_211 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_SNOW_CAT_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_LIZARD_UROKO)
            {
                GroundOne.WE2.EquipMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GOTUGOTU_KONBOU)
            {
                GroundOne.WE2.EquipMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EMBLEM_OF_PENGUIN)
            {
                GroundOne.WE2.EquipMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ARGONIAN_PURPLE_UROKO)
            {
                GroundOne.WE2.EquipMaterial_35 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_BEAR_CLAW_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_36 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ESSENCE_OF_EARTH)
            {
                GroundOne.WE2.EquipMaterial_37 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WOLF_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_38 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_TUNDRA_DEER_HORN)
            {
                GroundOne.WE2.EquipMaterial_39 += updateWE2Value;
            }
            else if (backpackData.Name == Database.EPIC_OLD_TREE_MIKI_DANPEN)
            {
                GroundOne.WE2.EquipMaterial_310 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.COMMON_HUNTER_SEVEN_TOOL)
            {
                GroundOne.WE2.EquipMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BEAST_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EXECUTIONER_ROBE)
            {
                GroundOne.WE2.EquipMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ANGEL_SILK)
            {
                GroundOne.WE2.EquipMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SABI_BUGU)
            {
                GroundOne.WE2.EquipMaterial_45 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_DARK)
            {
                GroundOne.WE2.EquipMaterial_46 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SEEKER_HEAD)
            {
                GroundOne.WE2.EquipMaterial_47 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MASTERBLADE_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_48 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_FLAME)
            {
                GroundOne.WE2.EquipMaterial_49 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GREAT_JEWELCROWN)
            {
                GroundOne.WE2.EquipMaterial_410 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ONRYOU_HAKO)
            {
                GroundOne.WE2.EquipMaterial_411 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN)
            {
                GroundOne.WE2.EquipMaterial_412 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN_DOU)
            {
                GroundOne.WE2.EquipMaterial_413 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN_BOU)
            {
                GroundOne.WE2.EquipMaterial_414 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER_TUKA)
            {
                GroundOne.WE2.EquipMaterial_415 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_416 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER)
            {
                GroundOne.WE2.EquipMaterial_417 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOUKA_TANZOU)
            {
                GroundOne.WE2.EquipMaterial_418 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_ADAMANTINE)
            {
                GroundOne.WE2.EquipMaterial_419 += updateWE2Value;
            }

            #endregion
            #endregion
            #region "ポーション/強化薬"
            #region "１階"
            else if (backpackData.Name == Database.COMMON_GREEN_SIKISO)
            {
                GroundOne.WE2.PotionMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_POISON_EKISU)
            {
                GroundOne.WE2.PotionMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RED_HOUSI)
            {
                GroundOne.WE2.PotionMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MARY_KISS)
            {
                GroundOne.WE2.PotionMaterial_14 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ALRAUNE_KAHUN)
            {
                GroundOne.WE2.PotionMaterial_15 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_HYUI_SEED)
            {
                GroundOne.WE2.PotionMaterial_16 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_NEBARIITO_KUMO)
            {
                GroundOne.WE2.PotionMaterial_17 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_MANDORAGORA_ROOT)
            {
                GroundOne.WE2.PotionMaterial_18 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BRILLIANT_RINPUN)
            {
                GroundOne.WE2.PotionMaterial_19 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MOSSGREEN_EKISU)
            {
                GroundOne.WE2.PotionMaterial_110 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_DREAM_POWDER)
            {
                GroundOne.WE2.PotionMaterial_111 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_GANGAME_EGG)
            {
                GroundOne.WE2.PotionMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_NANAIRO_SYOKUSYU)
            {
                GroundOne.WE2.PotionMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EIGHTEIGHT_KUROSUMI)
            {
                GroundOne.WE2.PotionMaterial_23 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_FAIRY_POWDER)
            {
                GroundOne.WE2.PotionMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ESSENCE_OF_WIND)
            {
                GroundOne.WE2.PotionMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_FROZEN_HEART)
            {
                GroundOne.WE2.PotionMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SHARPNESS_TIGER_TOOTH)
            {
                GroundOne.WE2.PotionMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_PURE_CRYSTAL)
            {
                GroundOne.WE2.PotionMaterial_35 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.RARE_BLOOD_DAGGER_KAKERA)
            {
                GroundOne.WE2.PotionMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DEMON_HORN)
            {
                GroundOne.WE2.PotionMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_SHINE)
            {
                GroundOne.WE2.PotionMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_BLACK_SEAL_IMPRESSION)
            {
                GroundOne.WE2.PotionMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_CHAOS_SIZUKU)
            {
                GroundOne.WE2.PotionMaterial_45 += updateWE2Value;
            }
            #endregion
            #endregion
            #region "料理素材"
            #region "１階"
            else if (backpackData.Name == Database.COMMON_INAGO)
            {
                GroundOne.WE2.FoodMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RABBIT_MEAT)
            {
                GroundOne.WE2.FoodMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_PLANTNOID_SEED)
            {
                GroundOne.WE2.FoodMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GREEN_EGG_KAIGARA)
            {
                GroundOne.WE2.FoodMaterial_14 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_SEA_WASI_KUTIBASI)
            {
                GroundOne.WE2.FoodMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_TONGUE)
            {
                GroundOne.WE2.FoodMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_LEG)
            {
                GroundOne.WE2.FoodMaterial_23 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_DAGGERFISH_UROKO)
            {
                GroundOne.WE2.FoodMaterial_24 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_PURE_WHITE_BIGEYE)
            {
                GroundOne.WE2.FoodMaterial_25 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SIPPUU_HIRE)
            {
                GroundOne.WE2.FoodMaterial_26 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SOFT_BIG_HIRE)
            {
                GroundOne.WE2.FoodMaterial_27 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KURIONE_ZOUMOTU)
            {
                GroundOne.WE2.FoodMaterial_28 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EIGHTEIGHT_KYUUBAN)
            {
                GroundOne.WE2.FoodMaterial_29 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RENEW_AKAMI)
            {
                GroundOne.WE2.FoodMaterial_210 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_WHITE_AZARASHI_MEAT)
            {
                GroundOne.WE2.FoodMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KESSYOU_SEA_WATER_SALT)
            {
                GroundOne.WE2.FoodMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ORC_MOMONIKU)
            {
                GroundOne.WE2.FoodMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RED_ONION)
            {
                GroundOne.WE2.FoodMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BIG_HIZUME)
            {
                GroundOne.WE2.FoodMaterial_35 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WHITE_POWDER)
            {
                GroundOne.WE2.FoodMaterial_36 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_DANGAN_KAKERA)
            {
                GroundOne.WE2.FoodMaterial_37 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TOUMEI_SNOW_CRYSTAL)
            {
                GroundOne.WE2.FoodMaterial_38 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.COMMON_BLACK_SALT)
            {
                GroundOne.WE2.FoodMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_FEBL_ANIS)
            {
                GroundOne.WE2.FoodMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MASTERBLADE_FIRE)
            {
                GroundOne.WE2.FoodMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SMORKY_HUNNY)
            {
                GroundOne.WE2.FoodMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ANGEL_DUST)
            {
                GroundOne.WE2.FoodMaterial_45 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SUN_TARAGON)
            {
                GroundOne.WE2.FoodMaterial_46 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ECHO_BEAST_MEAT)
            {
                GroundOne.WE2.FoodMaterial_47 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_CHAOS_TONGUE)
            {
                GroundOne.WE2.FoodMaterial_48 += updateWE2Value;
            }
            #endregion
            #endregion

            // 獲得した素材が調合の条件を満たしている場合、調合日を記憶する。
            #region "武具"
            #region "１階"
            if (GroundOne.WE2.EquipMaterial_11 >= 1 && GroundOne.WE2.EquipMaterial_12 >= 1 && GroundOne.WE2.EquipMixtureDay_11 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_11 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_13 >= 1 && GroundOne.WE2.EquipMaterial_14 >= 1 && GroundOne.WE2.EquipMixtureDay_12 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_12 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_15 >= 1 && GroundOne.WE2.EquipMaterial_16 >= 1 && GroundOne.WE2.EquipMaterial_17 >= 1 && GroundOne.WE2.EquipMixtureDay_13 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_13 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_18 >= 1 && GroundOne.WE2.EquipMaterial_19 >= 1 && GroundOne.WE2.EquipMixtureDay_14 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_14 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_110 >= 1 && GroundOne.WE2.EquipMaterial_111 >= 1 && GroundOne.WE2.EquipMixtureDay_15 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_15 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.EquipMaterial_21 >= 1 && GroundOne.WE2.EquipMaterial_22 >= 1 && GroundOne.WE2.EquipMixtureDay_21 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_21 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_23 >= 1 && GroundOne.WE2.EquipMaterial_24 >= 1 && GroundOne.WE2.EquipMixtureDay_22 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_22 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_25 >= 1 && GroundOne.WE2.EquipMixtureDay_23 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_23 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_26 >= 1 && GroundOne.WE2.EquipMaterial_27 >= 1 && GroundOne.WE2.EquipMixtureDay_24 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_24 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_28 >= 1 && GroundOne.WE2.EquipMaterial_29 >= 1 && GroundOne.WE2.EquipMixtureDay_25 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_25 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_210 >= 1 && GroundOne.WE2.EquipMaterial_211 >= 1 && GroundOne.WE2.EquipMixtureDay_26 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_26 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.EquipMaterial_31 >= 1 && GroundOne.WE2.EquipMixtureDay_31 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_31 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_32 >= 1 && GroundOne.WE2.EquipMixtureDay_32 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_32 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_33 >= 1 && GroundOne.WE2.EquipMixtureDay_33 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_33 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_34 >= 1 && GroundOne.WE2.EquipMixtureDay_34 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_34 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_35 >= 1 && GroundOne.WE2.EquipMixtureDay_35 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_35 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_36 >= 1 &&
                GroundOne.WE2.EquipMaterial_37 >= 1 && GroundOne.WE2.EquipMixtureDay_36 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_36 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_38 >= 1 && GroundOne.WE2.EquipMixtureDay_37 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_37 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_39 >= 1 &&
                GroundOne.WE2.EquipMaterial_310 >= 1 && GroundOne.WE2.EquipMixtureDay_38 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_38 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.EquipMaterial_41 >= 1 && GroundOne.WE2.EquipMixtureDay_41 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_41 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_42 >= 1 && GroundOne.WE2.EquipMixtureDay_42 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_42 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_43 >= 1 &&
                GroundOne.WE2.EquipMaterial_44 >= 1 && GroundOne.WE2.EquipMixtureDay_43 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_43 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_45 >= 1 &&
                GroundOne.WE2.EquipMaterial_46 >= 1 && GroundOne.WE2.EquipMixtureDay_44 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_44 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_47 >= 1 &&
                GroundOne.WE2.EquipMaterial_48 >= 1 &&
                GroundOne.WE2.EquipMaterial_49 >= 1 && GroundOne.WE2.EquipMixtureDay_45 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_45 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_410 >= 1 &&
                GroundOne.WE2.EquipMaterial_411 >= 1 && GroundOne.WE2.EquipMixtureDay_46 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_46 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_412 >= 1 &&
                GroundOne.WE2.EquipMaterial_413 >= 1 &&
                GroundOne.WE2.EquipMaterial_414 >= 1 && GroundOne.WE2.EquipMixtureDay_47 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_47 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_415 >= 1 &&
                GroundOne.WE2.EquipMaterial_416 >= 1 && GroundOne.WE2.EquipMixtureDay_48 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_48 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_417 >= 1 &&
                GroundOne.WE2.EquipMaterial_418 >= 1 && GroundOne.WE2.EquipMixtureDay_49 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_49 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_418 >= 1 &&
                GroundOne.WE2.EquipMaterial_419 >= 1 && GroundOne.WE2.EquipMixtureDay_410 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_410 = GroundOne.WE.GameDay;
            }
            #endregion
            #endregion


            #region "ポーション/強化薬"
            #region "１階"
            if (GroundOne.WE2.PotionMaterial_11 >= 1 && GroundOne.WE2.PotionMaterial_12 >= 1 && GroundOne.WE2.PotionMixtureDay_11 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_11 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_13 >= 1 && GroundOne.WE2.PotionMaterial_14 >= 1 && GroundOne.WE2.PotionMixtureDay_12 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_12 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_15 >= 1 && GroundOne.WE2.PotionMaterial_16 >= 1 && GroundOne.WE2.PotionMaterial_17 >= 1 && GroundOne.WE2.PotionMixtureDay_13 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_13 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_18 >= 1 && GroundOne.WE2.PotionMaterial_19 >= 1 && GroundOne.WE2.PotionMixtureDay_14 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_14 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_110 >= 1 && GroundOne.WE2.PotionMaterial_111 >= 1 && GroundOne.WE2.PotionMixtureDay_15 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_15 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.PotionMaterial_21 >= 1 && GroundOne.WE2.PotionMixtureDay_21 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_21 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_22 >= 1 && GroundOne.WE2.PotionMixtureDay_22 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_22 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_23 >= 1 && GroundOne.WE2.PotionMixtureDay_23 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_23 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.PotionMaterial_31 >= 1 &&
                GroundOne.WE2.PotionMaterial_32 >= 1 && GroundOne.WE2.PotionMixtureDay_31 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_31 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_33 >= 1 &&
                GroundOne.WE2.PotionMaterial_34 >= 1 && GroundOne.WE2.PotionMixtureDay_32 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_32 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_35 >= 1 && GroundOne.WE2.PotionMixtureDay_33 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_33 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.PotionMaterial_41 >= 1 && GroundOne.WE2.PotionMixtureDay_41 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_41 = GroundOne.WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_42 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_42 >= 1 && GroundOne.WE2.PotionMixtureDay_43 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_43 = GroundOne.WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_44 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_43 >= 1 && GroundOne.WE2.PotionMixtureDay_45 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_45 = GroundOne.WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_46 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_44 >= 1 && GroundOne.WE2.PotionMixtureDay_47 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_47 = GroundOne.WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_48 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_45 >= 1 && GroundOne.WE2.PotionMixtureDay_49 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_49 = GroundOne.WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_410 = GroundOne.WE.GameDay;
            }
            #endregion
            #endregion

            #region "食品"
            #region "１階"
            if (GroundOne.WE2.FoodMaterial_11 >= 1 && GroundOne.WE2.FoodMixtureDay_11 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_11 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_12 >= 1 && GroundOne.WE2.FoodMixtureDay_12 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_12 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_13 >= 1 && GroundOne.WE2.FoodMaterial_14 >= 1 && GroundOne.WE2.FoodMixtureDay_13 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_13 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.FoodMaterial_21 >= 1 && GroundOne.WE2.FoodMaterial_22 >= 1 && GroundOne.WE2.FoodMixtureDay_21 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_21 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_23 >= 1 && GroundOne.WE2.FoodMaterial_24 >= 1 && GroundOne.WE2.FoodMaterial_25 >= 1 && GroundOne.WE2.FoodMixtureDay_22 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_22 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_26 >= 1 && GroundOne.WE2.FoodMaterial_27 >= 1 && GroundOne.WE2.FoodMixtureDay_23 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_23 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_28 >= 1 && GroundOne.WE2.FoodMaterial_29 >= 1 && GroundOne.WE2.FoodMaterial_210 >= 1 && GroundOne.WE2.FoodMixtureDay_24 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_24 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.FoodMaterial_31 >= 1 &&
                GroundOne.WE2.FoodMaterial_32 >= 1 && GroundOne.WE2.FoodMixtureDay_31 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_31 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_33 >= 1 &&
                GroundOne.WE2.FoodMaterial_34 >= 1 && GroundOne.WE2.FoodMixtureDay_32 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_32 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_35 >= 1 &&
                GroundOne.WE2.FoodMaterial_36 >= 1 && GroundOne.WE2.FoodMixtureDay_33 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_33 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_37 >= 1 &&
                GroundOne.WE2.FoodMaterial_38 >= 1 && GroundOne.WE2.FoodMixtureDay_34 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_34 = GroundOne.WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.FoodMaterial_41 >= 1 &&
                GroundOne.WE2.FoodMaterial_42 >= 1 && GroundOne.WE2.FoodMixtureDay_41 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_41 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_43 >= 1 &&
                GroundOne.WE2.FoodMaterial_44 >= 1 && GroundOne.WE2.FoodMixtureDay_42 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_42 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_45 >= 1 &&
                GroundOne.WE2.FoodMaterial_46 >= 1 && GroundOne.WE2.FoodMixtureDay_43 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_43 = GroundOne.WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_47 >= 1 &&
                GroundOne.WE2.FoodMaterial_48 >= 1 && GroundOne.WE2.FoodMixtureDay_44 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_44 = GroundOne.WE.GameDay;
            }
            #endregion
            #endregion
        }
        protected void SetupAvailableList(int level)
        {
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii].text = "";
                costList[ii].text = "";
            }

            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    int ii = 0;
                    item = new ItemBackPack(Database.COMMON_BRONZE_SWORD);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FIT_ARMOR);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LIGHT_SHIELD);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_SWORD_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;
                    
                    item = new ItemBackPack(Database.COMMON_BASTARD_SWORD_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_ARMOR_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_SHIELD_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LIGHT_CLAW_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_KASHI_ROD_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LETHER_CLOTHING_1);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_IRON_SWORD);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_KUSARI_KATABIRA);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.RARE_FLOWER_WAND);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SURVIVAL_CLAW);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SUPERIOR_CROSS);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;
                    
                    item = new ItemBackPack(Database.COMMON_BLACER_OF_SYOJIN);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_ZIAI_PENDANT);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_11)
                    {
                        item = new ItemBackPack(Database.COMMON_KOUKAKU_ARMOR);
                        equipList[ii].text = item.Name;
                        //equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                        //costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_12)
                    {
                        item = new ItemBackPack(Database.COMMON_SISSO_TUKEHANE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_13)
                    {
                        item = new ItemBackPack(Database.RARE_WAR_WOLF_BLADE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_14)
                    {
                        item = new ItemBackPack(Database.COMMON_BLUE_COPPER_ARMOR_KAI);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_15)
                    {
                        item = new ItemBackPack(Database.COMMON_RABBIT_SHOES);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;
                    break;

                case 2:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_SMART_SWORD_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_CLAW_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_RAUGE_SWORD_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_ROD_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_SHIELD_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_PLATE_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_CLOTHING_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_ROBE_2);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_STEEL_SWORD);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_BERSERKER_PLATE);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_MIX_HINOKI_ROD);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FACILITY_CLAW);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_BRIGHTNESS_ROBE);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.RARE_WILD_HEART_SPADE);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_21)
                    {
                        item = new ItemBackPack(Database.COMMON_WHITE_WAVE_RING);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_22)
                    {
                        item = new ItemBackPack(Database.COMMON_NEEDLE_FEATHER);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_23)
                    {
                        item = new ItemBackPack(Database.COMMON_KOUSHITU_ORB);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_24)
                    {
                        item = new ItemBackPack(Database.RARE_RED_ARM_BLADE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_25)
                    {
                        item = new ItemBackPack(Database.RARE_STRONG_SERPENT_CLAW);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_26)
                    {
                        item = new ItemBackPack(Database.RARE_STRONG_SERPENT_SHIELD);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    break;

                case 3:
                    item = new ItemBackPack(Database.COMMON_WINTERS_HORN);
                    equipList[0].text = item.Name;
                    Method.UpdateRareColor(item, equipList[0], backEquip[0]);

                    item = new ItemBackPack(Database.RARE_CHILL_BONE_SHIELD);
                    equipList[1].text = item.Name;
                    Method.UpdateRareColor(item, equipList[1], backEquip[1]);

                    if (GroundOne.WE2.EquipAvailable_31)
                    {
                        item = new ItemBackPack(Database.COMMON_SNOW_GUARD);
                        equipList[2].text = item.Name;
                        Method.UpdateRareColor(item, equipList[2], backEquip[2]);
                    }

                    if (GroundOne.WE2.EquipAvailable_32)
                    {
                        item = new ItemBackPack(Database.COMMON_LIZARDSCALE_ARMOR);
                        equipList[3].text = item.Name;
                        Method.UpdateRareColor(item, equipList[3], backEquip[3]);
                    }

                    if (GroundOne.WE2.EquipAvailable_33)
                    {
                        item = new ItemBackPack(Database.COMMON_STEEL_BLADE);
                        equipList[4].text = item.Name;
                        Method.UpdateRareColor(item, equipList[4], backEquip[4]);
                    }

                    if (GroundOne.WE2.EquipAvailable_34)
                    {
                        item = new ItemBackPack(Database.COMMON_PENGUIN_OF_PENGUIN);
                        equipList[5].text = item.Name;
                        Method.UpdateRareColor(item, equipList[5], backEquip[5]);
                    }

                    if (GroundOne.WE2.EquipAvailable_35)
                    {
                        item = new ItemBackPack(Database.COMMON_ARGNIAN_TUNIC);
                        equipList[6].text = item.Name;
                        Method.UpdateRareColor(item, equipList[6], backEquip[6]);
                    }

                    if (GroundOne.WE2.EquipAvailable_36)
                    {
                        item = new ItemBackPack(Database.RARE_SPLASH_BARE_CLAW);
                        equipList[7].text = item.Name;
                        Method.UpdateRareColor(item, equipList[7], backEquip[7]);
                    }

                    if (GroundOne.WE2.EquipAvailable_37)
                    {
                        item = new ItemBackPack(Database.COMMON_WOLF_BATTLE_CLOTH);
                        equipList[8].text = item.Name;
                        Method.UpdateRareColor(item, equipList[8], backEquip[8]);
                    }

                    if (GroundOne.WE2.EquipAvailable_38)
                    {
                        item = new ItemBackPack(Database.EPIC_GATO_HAWL_OF_GREAT);
                        equipList[9].text = item.Name;
                        Method.UpdateRareColor(item, equipList[9], backEquip[9]);
                    }
                    break;

                case 4:
                    item = new ItemBackPack(Database.RARE_SUPERIOR_CHOSEN_ROD);
                    equipList[0].text = item.Name;
                    Method.UpdateRareColor(item, equipList[0], backEquip[0]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_SWORD);
                    equipList[1].text = item.Name;
                    Method.UpdateRareColor(item, equipList[1], backEquip[1]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_ARMOR);
                    equipList[2].text = item.Name;
                    Method.UpdateRareColor(item, equipList[2], backEquip[2]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_SHIELD);
                    equipList[3].text = item.Name;
                    Method.UpdateRareColor(item, equipList[3], backEquip[3]);

                    item = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    equipList[4].text = item.Name;
                    Method.UpdateRareColor(item, equipList[4], backEquip[4]);

                    if (GroundOne.WE2.EquipAvailable_41)
                    {
                        item = new ItemBackPack(Database.RARE_HUNTERS_EYE);
                        equipList[5].text = item.Name;
                        Method.UpdateRareColor(item, equipList[5], backEquip[5]);
                    }
                    if (GroundOne.WE2.EquipAvailable_42)
                    {
                        item = new ItemBackPack(Database.RARE_ONEHUNDRED_BUTOUGI);
                        equipList[6].text = item.Name;
                        Method.UpdateRareColor(item, equipList[6], backEquip[6]);
                    }
                    if (GroundOne.WE2.EquipAvailable_43)
                    {
                        item = new ItemBackPack(Database.RARE_DARKANGEL_CROSS);
                        equipList[7].text = item.Name;
                        Method.UpdateRareColor(item, equipList[7], backEquip[7]);
                    }
                    if (GroundOne.WE2.EquipAvailable_44)
                    {
                        item = new ItemBackPack(Database.RARE_DEVIL_KILLER);
                        equipList[8].text = item.Name;
                        Method.UpdateRareColor(item, equipList[8], backEquip[8]);
                    }
                    if (GroundOne.WE2.EquipAvailable_45)
                    {
                        item = new ItemBackPack(Database.RARE_TRUERED_MASTER_BLADE);
                        equipList[9].text = item.Name;
                        Method.UpdateRareColor(item, equipList[9], backEquip[9]);
                    }
                    if (GroundOne.WE2.EquipAvailable_46)
                    {
                        item = new ItemBackPack(Database.RARE_VOID_HYMNSONIA);
                        equipList[10].text = item.Name;
                        Method.UpdateRareColor(item, equipList[10], backEquip[10]);
                    }
                    if (GroundOne.WE2.EquipAvailable_47)
                    {
                        item = new ItemBackPack(Database.RARE_SEAL_OF_BALANCE);
                        equipList[11].text = item.Name;
                        Method.UpdateRareColor(item, equipList[11], backEquip[11]);
                    }
                    if (GroundOne.WE2.EquipAvailable_48)
                    {
                        item = new ItemBackPack(Database.RARE_DOOMBRINGER);
                        equipList[12].text = item.Name;
                        Method.UpdateRareColor(item, equipList[12], backEquip[12]);
                    }
                    if (GroundOne.WE2.EquipAvailable_49)
                    {
                        item = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                        equipList[13].text = item.Name;
                        Method.UpdateRareColor(item, equipList[13], backEquip[13]);
                    }
                    
                    break;

                case 5:
                    item = new ItemBackPack(Database.COMMON_GORGEOUS_RED_POTION);
                    equipList[0].text = item.Name;
                    Method.UpdateRareColor(item, equipList[0], backEquip[0]);

                    item = new ItemBackPack(Database.COMMON_GORGEOUS_BLUE_POTION);
                    equipList[1].text = item.Name;
                    Method.UpdateRareColor(item, equipList[1], backEquip[1]);

                    item = new ItemBackPack(Database.COMMON_GORGEOUS_GREEN_POTION);
                    equipList[2].text = item.Name;
                    Method.UpdateRareColor(item, equipList[2], backEquip[2]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_1);
                    equipList[3].text = item.Name;
                    Method.UpdateRareColor(item, equipList[3], backEquip[3]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_2);
                    equipList[4].text = item.Name;
                    Method.UpdateRareColor(item, equipList[4], backEquip[4]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_3);
                    equipList[5].text = item.Name;
                    Method.UpdateRareColor(item, equipList[5], backEquip[5]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_4);
                    equipList[6].text = item.Name;
                    Method.UpdateRareColor(item, equipList[6], backEquip[6]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_5);
                    equipList[7].text = item.Name;
                    Method.UpdateRareColor(item, equipList[7], backEquip[7]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_6);
                    equipList[8].text = item.Name;
                    Method.UpdateRareColor(item, equipList[8], backEquip[8]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_7);
                    equipList[9].text = item.Name;
                    Method.UpdateRareColor(item, equipList[9], backEquip[9]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_8);
                    equipList[10].text = item.Name;
                    Method.UpdateRareColor(item, equipList[10], backEquip[10]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_9);
                    equipList[11].text = item.Name;
                    Method.UpdateRareColor(item, equipList[11], backEquip[11]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_10);
                    equipList[12].text = item.Name;
                    Method.UpdateRareColor(item, equipList[12], backEquip[12]);

                    item = new ItemBackPack(Database.RARE_ETHREAL_EDGE_SABRE);
                    equipList[13].text = item.Name;
                    Method.UpdateRareColor(item, equipList[13], backEquip[13]);

                    item = new ItemBackPack(Database.RARE_BLOODY_DIRTY_SCYTHE);
                    equipList[14].text = item.Name;
                    Method.UpdateRareColor(item, equipList[14], backEquip[14]);

                    item = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                    equipList[15].text = item.Name;
                    Method.UpdateRareColor(item, equipList[15], backEquip[15]);

                    item = new ItemBackPack(Database.RARE_WHITE_DIAMOND_SHIELD);
                    equipList[16].text = item.Name;
                    Method.UpdateRareColor(item, equipList[16], backEquip[16]);

                    break;
            }

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (equipList[ii].text != "")
                {
                    ItemBackPack temp4 = new ItemBackPack(equipList[ii].text);
                    costList[ii].text = temp4.Cost.ToString();
                }
                else
                {
                    costList[ii].text = "";
                    backEquip[ii].SetActive(false);
                    backCost[ii].SetActive(false);
                }
            }
        }

        // todo
        //protected virtual void OnEquipmentShop_Shown()
        //{
        //    #region "１階"
        //    if (!GroundOne.WE2.EquipAvailable_11 && (GroundOne.WE2.EquipMixtureDay_11 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_11))
        //    {
        //        GroundOne.WE2.EquipAvailable_11 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_KOUKAKU_ARMOR;
        //            TID.ItemNameTitle = Database.COMMON_KOUKAKU_ARMOR;
        //            TID.Description = "甲殻部を繋ぎ合わせた鎧に、魔法耐性を若干付与させた一品。防御力１１～１５。火耐性＋３０";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_12 && (GroundOne.WE2.EquipMixtureDay_12 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_12))
        //    {
        //        GroundOne.WE2.EquipAvailable_12 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_SISSO_TUKEHANE;
        //            TID.ItemNameTitle = Database.COMMON_SISSO_TUKEHANE;
        //            TID.Description = "毛皮に幾つかの白羽を埋め込んだアクセサリ。力＋３、技＋３、心＋３"; ;
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_13 && (GroundOne.WE2.EquipMixtureDay_13 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_13))
        //    {
        //        GroundOne.WE2.EquipAvailable_13 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_WAR_WOLF_BLADE;
        //            TID.ItemNameTitle = Database.RARE_WAR_WOLF_BLADE;
        //            TID.Description = "狼の牙を基素材とし、刺付き触手を加工した武器。攻撃力３２～４４"; ;
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_14 && (GroundOne.WE2.EquipMixtureDay_14 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_14))
        //    {
        //        GroundOne.WE2.EquipAvailable_14 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_BLUE_COPPER_ARMOR_KAI;
        //            TID.ItemNameTitle = Database.COMMON_BLUE_COPPER_ARMOR_KAI;
        //            TID.Description = "青銅の材質強度を落とさずに仕上げられた鎧。防御力１８～２５。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_15 && (GroundOne.WE2.EquipMixtureDay_15 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_15))
        //    {
        //        GroundOne.WE2.EquipAvailable_15 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_RABBIT_SHOES;
        //            TID.ItemNameTitle = Database.COMMON_RABBIT_SHOES;
        //            TID.Description = "ウサギの毛皮と質の良いスパイダーシルクを合成した出来たシューズ。技＋１２、体力＋１０";
        //            TID.ShowDialog();
        //        }
        //    }
        //    #endregion
        //    #region "２階"
        //    if (!GroundOne.WE2.EquipAvailable_21 && (GroundOne.WE2.EquipMixtureDay_21 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_21))
        //    {
        //        GroundOne.WE2.EquipAvailable_21 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_WHITE_WAVE_RING;
        //            TID.ItemNameTitle = Database.COMMON_WHITE_WAVE_RING;
        //            TID.Description = "勾玉からエッセンスを引き出し、リング化に成功。知＋８、体力＋７、心＋１０";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_22 && (GroundOne.WE2.EquipMixtureDay_22 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_22))
        //    {
        //        GroundOne.WE2.EquipAvailable_22 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_NEEDLE_FEATHER;
        //            TID.ItemNameTitle = Database.COMMON_NEEDLE_FEATHER;
        //            TID.Description = "鋭いトゲと幸運を呼ぶ青羽をうまく融合させたアクセサリ。力＋１０、技＋８、心＋９";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_23 && (GroundOne.WE2.EquipMixtureDay_23 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_23))
        //    {
        //        GroundOne.WE2.EquipAvailable_23 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_KOUSHITU_ORB;
        //            TID.ItemNameTitle = Database.COMMON_KOUSHITU_ORB;
        //            TID.Description = "幾つもの殻を溶解し、一つの胸当ての形状にした。防御力８～２５";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_24 && (GroundOne.WE2.EquipMixtureDay_24 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_24))
        //    {
        //        GroundOne.WE2.EquipAvailable_24 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_RED_ARM_BLADE;
        //            TID.ItemNameTitle = Database.RARE_RED_ARM_BLADE;
        //            TID.Description = "豪腕なジョーの腕を加工し、赤褐色でコーティングを施してある。攻撃力２７～９５";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_25 && (GroundOne.WE2.EquipMixtureDay_25 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_25))
        //    {
        //        GroundOne.WE2.EquipAvailable_25 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_STRONG_SERPENT_CLAW;
        //            TID.ItemNameTitle = Database.RARE_STRONG_SERPENT_CLAW;
        //            TID.Description = "強固な青鮫の剣歯を更に高質化させ、高熱で磨いだ爪。攻撃力１５～４６";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_26 && (GroundOne.WE2.EquipMixtureDay_26 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_26))
        //    {
        //        GroundOne.WE2.EquipAvailable_26 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_STRONG_SERPENT_SHIELD;
        //            TID.ItemNameTitle = Database.RARE_STRONG_SERPENT_SHIELD;
        //            TID.Description = "強固な青鮫の鱗を更に高質化させ、低温度化で固めた防具。防御力６～３５";
        //            TID.ShowDialog();
        //        }
        //    }
        //    #endregion
        //    #region "３階"
        //    if (!GroundOne.WE2.EquipAvailable_31 && (GroundOne.WE2.EquipMixtureDay_31 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_31))
        //    {
        //        GroundOne.WE2.EquipAvailable_31 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_SNOW_GUARD;
        //            TID.ItemNameTitle = Database.COMMON_SNOW_GUARD;
        //            TID.Description = "吹雪対策用に見えるが、アクセサリとしての上質さは装着した者のみが知る。体＋５０、心＋５０、水耐性１０００";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_32 && (GroundOne.WE2.EquipMixtureDay_32 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_32))
        //    {
        //        GroundOne.WE2.EquipAvailable_32 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_LIZARDSCALE_ARMOR;
        //            TID.ItemNameTitle = Database.COMMON_LIZARDSCALE_ARMOR;
        //            TID.Description = "リザードの鱗を細かく細分化し、鎧形状に仕立てなおしたもの。防御力１０５～１３０";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_33 && (GroundOne.WE2.EquipMixtureDay_33 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_33))
        //    {
        //        GroundOne.WE2.EquipAvailable_33 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_STEEL_BLADE;
        //            TID.ItemNameTitle = Database.COMMON_STEEL_BLADE;
        //            TID.Description = "強靭な素材のみ使用した鋼にガンツ直々の技が宿った剣！攻撃力２２５(+25）～２５５(+25)";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_34 && (GroundOne.WE2.EquipMixtureDay_34 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_34))
        //    {
        //        GroundOne.WE2.EquipAvailable_34 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_PENGUIN_OF_PENGUIN;
        //            TID.ItemNameTitle = Database.COMMON_PENGUIN_OF_PENGUIN;
        //            TID.Description = "ペンギンの気持ちが心なしか伝わってくる。力＋３０、技＋３０、知＋３０、体＋３０、心＋３０";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_35 && (GroundOne.WE2.EquipMixtureDay_35 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_35))
        //    {
        //        GroundOne.WE2.EquipAvailable_35 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_ARGNIAN_TUNIC;
        //            TID.ItemNameTitle = Database.COMMON_ARGNIAN_TUNIC;
        //            TID.Description = "アルゴニアンの素材は紫色のコーティングがあり安定した防御性が出やすい。防御力５５～６７";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_36 && (GroundOne.WE2.EquipMixtureDay_36 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_36))
        //    {
        //        GroundOne.WE2.EquipAvailable_36 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_SPLASH_BARE_CLAW;
        //            TID.ItemNameTitle = Database.RARE_SPLASH_BARE_CLAW;
        //            TID.Description = "ゴツゴツし砕け散ったクマの手素材をガンツが見事に武器化に成功！　攻撃力２６２～２７７";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_37 && (GroundOne.WE2.EquipMixtureDay_37 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_37))
        //    {
        //        GroundOne.WE2.EquipAvailable_37 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.COMMON_WOLF_BATTLE_CLOTH;
        //            TID.ItemNameTitle = Database.COMMON_WOLF_BATTLE_CLOTH;
        //            TID.Description = "野生ウルフのごわごわした質感を落とすことなく衣に仕立ててある。防御力７２～７９";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_38 && (GroundOne.WE2.EquipMixtureDay_38 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_38))
        //    {
        //        GroundOne.WE2.EquipAvailable_38 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.EPIC_GATO_HAWL_OF_GREAT;
        //            TID.ItemNameTitle = Database.EPIC_GATO_HAWL_OF_GREAT;
        //            TID.Description = "古代賢者ガトゥに仕えていた神鹿の紋章。沈黙・スタン・麻痺耐性。技＋８５、知＋３２５、魔力６６６～７７７、魔攻率＋２０％、潜力率＋２０％、闇耐性1500、火耐性1500";
        //            TID.ShowDialog();
        //        }
        //    }
        //    #endregion
        //    #region "４階"
        //    if (!GroundOne.WE2.EquipAvailable_41 && (GroundOne.WE2.EquipMixtureDay_41 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_41))
        //    {
        //        GroundOne.WE2.EquipAvailable_41 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_HUNTERS_EYE;
        //            TID.ItemNameTitle = Database.RARE_HUNTERS_EYE;
        //            TID.Description = "ハンター七つ道具を組み合わせて作成された擬眼。眼の動向の開き方に応じて様々なギミックが発動する。技＋３００、体＋３００、沈黙耐性、麻痺耐性、鈍化耐性、暗闇耐性";
        //            TID.Description += "\r\n【特殊能力】　以下のいずれかがランダムに発動する。敵全体に対して【鈍化】効果を与える / 味方全体のいずれかがトゥルス・ヴィジョンがかかっていない場合、トゥルス・ヴィジョンを発動する / 自分自身の物理攻撃力と戦闘速度をUPする / 敵単体の物理攻撃力と戦闘速度をDOWNさせる";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_42 && (GroundOne.WE2.EquipMixtureDay_42 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_42))
        //    {
        //        GroundOne.WE2.EquipAvailable_42 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_ONEHUNDRED_BUTOUGI;
        //            TID.ItemNameTitle = Database.RARE_ONEHUNDRED_BUTOUGI;
        //            TID.Description = "選りすぐりの獣皮を集約し、動きやすさ・重量感を重視したもの。防御力１６４～１７８、聖耐性15000、闇耐性15000、火耐性15000、水耐性15000";
        //            TID.Description += "\r\n【常備能力】　まれに物理/魔法による攻撃を回避する。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_43 && (GroundOne.WE2.EquipMixtureDay_43 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_43))
        //    {
        //        GroundOne.WE2.EquipAvailable_43 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_DARKANGEL_CROSS;
        //            TID.ItemNameTitle = Database.RARE_DARKANGEL_CROSS;
        //            TID.Description = "執行人のローブから高級なシルク素材を摘出し、天使のシルクと融合させて新たに創生した衣。防御力１９０～２３４、魔法防御４９２～６５２、聖耐性22000、闇耐性22000、毒耐性、誘惑耐性、鈍化耐性、暗闇耐性";
        //            TID.Description += "\r\n【常備能力】　聖魔法１０％強化、闇魔法１０％強化";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_44 && (GroundOne.WE2.EquipMixtureDay_44 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_44))
        //    {
        //        GroundOne.WE2.EquipAvailable_44 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_DEVIL_KILLER;
        //            TID.ItemNameTitle = Database.RARE_DEVIL_KILLER;
        //            TID.Description = "悪しき者を断つ剣。ガラクタから生成したとは思えないガンツ渾身の力作。攻撃力３６０～１８８５";
        //            TID.Description += "\r\n【常備能力】　稀に即死させる。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_45 && (GroundOne.WE2.EquipMixtureDay_45 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_45))
        //    {
        //        GroundOne.WE2.EquipAvailable_45 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_TRUERED_MASTER_BLADE;
        //            TID.ItemNameTitle = Database.RARE_TRUERED_MASTER_BLADE;
        //            TID.Description = "頭蓋骨を破砕した素材を柄に付け、剣の切っ先は常に火が宿る。攻撃力８００～８５０、魔力６５０～７００";
        //            TID.Description += "\r\n【常備能力】　物理攻撃がヒットする度に、稀にワード・オブ・パワーが追加効果で発動する。魔法攻撃がヒットする度に、稀にサイキック・ウェイブが追加効果で発動する。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_46 && (GroundOne.WE2.EquipMixtureDay_46 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_46))
        //    {
        //        GroundOne.WE2.EquipAvailable_46 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_VOID_HYMNSONIA;
        //            TID.ItemNameTitle = Database.RARE_VOID_HYMNSONIA;
        //            TID.Description = "豪華な財宝を与える事で怨霊を全て除去した箱。力＋５００、技＋５００、知＋５００、心－１０００、毒耐性、沈黙耐性、スタン耐性、麻痺耐性、凍結耐性、誘惑耐性、鈍化耐性、暗闇耐性、スリップ耐性";
        //            TID.Description += "\r\n【特殊能力】　本装備品：ヴォイド・ヒムソニアの【心】パラメタ-1000の特性を無効にする。戦闘終了までこの効果は継続する。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_47 && (GroundOne.WE2.EquipMixtureDay_47 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_47))
        //    {
        //        GroundOne.WE2.EquipAvailable_47 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_VOID_HYMNSONIA;
        //            TID.ItemNameTitle = Database.RARE_VOID_HYMNSONIA;
        //            TID.Description = "天秤の形状を再構築し、紋章の形状に変換することに成功。体＋５００、心＋５００、水耐性5000、空耐性5000";
        //            TID.Description += "\r\n【常備能力】　物理攻撃を受けた場合、マナが回復する。魔法攻撃を受けた場合、スキルポイントが回復する。DEBUFF属性が付与された場合、次のターンそのBUFFを解除する。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_48 && (GroundOne.WE2.EquipMixtureDay_48 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_48))
        //    {
        //        GroundOne.WE2.EquipAvailable_48 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.RARE_DOOMBRINGER;
        //            TID.ItemNameTitle = Database.RARE_DOOMBRINGER;
        //            TID.Description = "破滅した者へ永遠の安らぎをもたらすために作られた剣。攻撃力４７３～１４６９";
        //            TID.Description += "\r\n【常備能力】　理魔法＋１０％強化。戦闘開始時、ゲイル・ウィンドが自分自身にかかる。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_49 && (GroundOne.WE2.EquipMixtureDay_49 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_49))
        //    {
        //        GroundOne.WE2.EquipAvailable_49 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.EPIC_MEIKOU_DOOMBRINGER;
        //            TID.ItemNameTitle = Database.EPIC_MEIKOU_DOOMBRINGER;
        //            TID.Description = "穢れを取り払われた闇の剣。持ち主の意図に関わらず、剣が宿主を選ぶ。攻撃力１２００～２４００、魔力１２００～２４００、物攻率＋２５％、魔攻率＋２０％、戦速率＋１５％";
        //            TID.Description += "\r\n【常備能力】　理魔法＋１６％強化、聖魔法１６％強化。戦闘開始時、ゲイル・ウィンドが自分自身にかかる。戦闘開始時、ジェネシスの行動記憶に【ゲイル・ウィンド】がセットされる。";
        //            TID.ShowDialog();
        //        }
        //    }
        //    if (!GroundOne.WE2.EquipAvailable_410 && (GroundOne.WE2.EquipMixtureDay_410 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_410))
        //    {
        //        GroundOne.WE2.EquipAvailable_410 = true;
        //        using (TruthItemDesc TID = new TruthItemDesc())
        //        {
        //            TID.StartPosition = FormStartPosition.CenterParent;
        //            TID.ItemNameButton = Database.EPIC_ETERNAL_HOMURA_RING;
        //            TID.ItemNameTitle = Database.EPIC_ETERNAL_HOMURA_RING;
        //            TID.Description = "その焔火が潰える事は未だかつて起きた事がない。魔法攻撃１８５０～２０５０、知＋１２５０、スタン耐性、麻痺耐性、凍結耐性";
        //            TID.Description += "\r\n魔攻率＋３５％、魔防率３０％、聖耐性10000、闇耐性10000、火耐性75000、水耐性75000、理耐性10000、空耐性10000";
        //            TID.Description += "\r\n【常備能力】毎ターン、MPを回復する。";
        //            TID.Description += "\r\n【特殊能力】全MPを消費して、消費したMPの分だけ、無属性魔法ダメージを与える。";
        //            TID.ShowDialog();
        //        }
        //    }

        //    #endregion
        //}

        //protected override void EquipmentShop_Shown(object sender, System.EventArgs e)
        //{
        //    OnEquipmentShop_Shown();

        //    // ２重記述だが、ベストコードは後で良しとする。
        //    if (we.AvailableEquipShop && !we.AvailableEquipShop2)
        //    {
        //        SetupAvailableList(1);
        //    }
        //    else if (we.AvailableEquipShop && we.AvailableEquipShop2 && !we.AvailableEquipShop3)
        //    {
        //        SetupAvailableList(2);
        //    }
        //    else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && !we.AvailableEquipShop4)
        //    {
        //        SetupAvailableList(3);
        //    }
        //    else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && !we.AvailableEquipShop5)
        //    {
        //        SetupAvailableList(4);
        //    }
        //    else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && we.AvailableEquipShop5)
        //    {
        //        SetupAvailableList(5);
        //    }
        //    else
        //    {

        //    }
        //}

        //protected override void MessageExchange6(ItemBackPack backpackData, int stack, int ii)
        //{
        //    SetupMessageText(3007, backpackData.Name, ((backpackData.Cost / 2) * stack).ToString());
        //}

        //protected override void ConstructPopupInfo(PopUpMini popupInfo)
        //{
        //    popupInfo.CurrentInfo = currentPlayer.FullName + "\r\n";

        //    if (currentPlayer.MainWeapon == null)
        //    {
        //        popupInfo.CurrentInfo += "武器(メイン) " + "（なし）" + "\r\n";
        //    }
        //    else
        //    {
        //        popupInfo.CurrentInfo += "武器(メイン) " + currentPlayer.MainWeapon.Name + "\r\n";
        //    }
        //    if (currentPlayer.SubWeapon == null)
        //    {
        //        popupInfo.CurrentInfo += "武器(サブ) 　" + "（なし）" + "\r\n";
        //    }
        //    else
        //    {
        //        popupInfo.CurrentInfo += "武器(サブ)　 " + currentPlayer.SubWeapon.Name + "\r\n";
        //    }
        //    if (currentPlayer.MainArmor == null)
        //    {
        //        popupInfo.CurrentInfo += "防具　　　　 " + "（なし）" + "\r\n";
        //    }
        //    else
        //    {
        //        popupInfo.CurrentInfo += "防具　　　　 " + currentPlayer.MainArmor.Name + "\r\n";
        //    }
        //    if (currentPlayer.Accessory == null)
        //    {
        //        popupInfo.CurrentInfo += "装飾品１　　 " + "（なし）" + "\r\n";
        //    }
        //    else
        //    {
        //        popupInfo.CurrentInfo += "装飾品１　　 " + currentPlayer.Accessory.Name + "\r\n";
        //    }
        //    if (currentPlayer.Accessory2 == null)
        //    {
        //        popupInfo.CurrentInfo += "装飾品２　　 " + "（なし）" + "\r\n";
        //    }
        //    else
        //    {
        //        popupInfo.CurrentInfo += "装飾品２ 　　" + currentPlayer.Accessory2.Name + "\r\n";
        //    }
        //    popupInfo.CurrentInfo += "\r\n";
        //}

        public void Equip_Click(Text sender)
        {
            this.currentSelectItem3 = new ItemBackPack(sender.text);
            SelectSellItem(this.currentSelectItem3);
        }

        bool nowSellItem = false;
        public void Backpack_Click(Text sender)
        {
            this.nowSellItem = true;
            this.currentSelectItem2 = new ItemBackPack(sender.text);

            int stack = 1;
            if (currentSelectItem2.Name == "")
            {
                return;
            }
            if (currentSelectItem2.Cost <= 0)
            {
                MessageExchange5(); // 後編編集
                return;
            }
            // s 後編追加
            else if ((currentSelectItem2.Name == Database.LEGENDARY_FELTUS) ||
                        (currentSelectItem2.Name == Database.POOR_PRACTICE_SWORD_1) ||
                        (currentSelectItem2.Name == Database.POOR_PRACTICE_SWORD_2) ||
                        (currentSelectItem2.Name == Database.COMMON_PRACTICE_SWORD_3) ||
                        (currentSelectItem2.Name == Database.COMMON_PRACTICE_SWORD_4) ||
                        (currentSelectItem2.Name == Database.RARE_PRACTICE_SWORD_5) ||
                        (currentSelectItem2.Name == Database.RARE_PRACTICE_SWORD_6) ||
                        (currentSelectItem2.Name == Database.EPIC_PRACTICE_SWORD_7))
            {
                MessageExchange5();
                return;
            }
            // e 後編追加
            else
            {
                int number = 0;
                for (int ii = 0; ii < backpackList.Length; ii++)
                {
                    if (sender.Equals(backpackList[ii]))
                    {
                        number = ii;
                        break;
                    }
                }
                // s 後編編集
                stack = SelectSellStackValue(currentSelectItem2, number);
                if (stack == -1) return; // 複数量指定の時、ESCキャンセルはｰ1で抜けてくるので、即時Return

                MessageExchange6(currentSelectItem2, stack);
            }

            yesnoMessage.text = String.Format(ganz.GetCharacterSentence(3007), currentSelectItem2.Name, (currentSelectItem2.Cost / 2).ToString());
            groupYesNoMessage.SetActive(true);
            filter.SetActive(true);
        }
        public void EquipmentShop_Click(Text sender)
        {
            ItemBackPack backpackData = new ItemBackPack(((Text)sender).text);
            if (!GroundOne.WE.AvailableEquipShop5)
            {
                switch (backpackData.Name)
                {
                    case "ショートソード": // ガンツの武具屋販売（ダンジョン１階）
                        UpdateMainMessage("ガンツ：そいつは標準的なショートソードだね。買うかね？");
                        break;
                    case "洗練されたロングソード": // ガンツの武具屋販売（ダンジョン１階）
                        UpdateMainMessage("ガンツ：普通のロングソードだがヴァスタ爺が少し鍛えてある。買うかね？");
                        break;
                    case "冒険者用の鎖かたびら": // ガンツの武具屋販売（ダンジョン１階）
                        UpdateMainMessage("ガンツ：冒険者なら必需品といえる防御を誇る。買うかね？");
                        break;
                    case "青銅の鎧": // ガンツの武具屋販売（ダンジョン１階）
                        UpdateMainMessage("ガンツ：文句なしの良品質な一品だ。買うかね？");
                        break;
                    case "神剣  フェルトゥーシュ":
                        UpdateMainMessage("ガンツ：ヴァスタ爺の最高傑作だが、先客が買占めてしまったようだ。すまない。");
                        return;
                    case "些細なパワーリング": // ガンツの武具屋販売（ダンジョン１階）
                        UpdateMainMessage("ガンツ：目の付け所が良いな。買うかね？");
                        break;
                    case "紺碧のスターエムブレム": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("ガンツ：ハンナの思い付きを採用した一品だ。買うかね？");
                        break;
                    case "闘魂バンド": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("ガンツ：やる気を出すにはこのバンドが最適だ。買うかね？");
                        break;
                    case "ウェルニッケの腕輪": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("ウェルニッケ素材を使う事で体力の源を宿らせた腕輪だ。買うかね？");
                        break;
                    case "賢者の眼鏡": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("ヴァスタ爺が１日の思いつきで作ったユニークな眼鏡だ。買うかね？");
                        break;
                    case "ファルシオン": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("過去の文献を参考にして作り上げた剣だ。買うかね？");
                        break;
                    case "フィスト・クロス": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("打撃系同士の打ち合いに特化させた衣だ。買うかね？");
                        break;

                    case "青銅の剣": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("青銅の剣は重さと威力が良いバランスじゃよ。買うかね？");
                        break;
                    case "メタルフィスト": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("メタル製は若干重いものの、慣れれば扱いは良いはず。買うかね？");
                        break;
                    case "光沢のある鉄のプレート": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("鉄製のプレートにイエローマテリアルを幾つか埋め込んだ。買うかね？");
                        break;
                    case "シルクの武道衣": // ガンツの武具屋販売（ダンジョン２階）
                        UpdateMainMessage("シルク製だが、縫い目をキメ細かくしてあり頑丈なものになっておる。買うかね？");
                        break;

                    case "プラチナソード": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("プラチナ素材で精製した剣だ。シンプルじゃろ。買うかね？");
                        break;
                    case "アイアンクロー": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("鉄製の爪だ。シンプルじゃろ。買うかね？");
                        break;
                    case "シルバーアーマー": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("銀素材を少しずつ埋め込む事で耐久性をあげた鎧だ。買うかね？");
                        break;
                    case "獣皮製の舞踏衣": // ガンツの武具屋販売（ダンジョン３階）
                        UpdateMainMessage("ギルブロンド種族の皮を使って生成したものだ。買うかね？");
                        break;

                    case "ライトプラズマブレード": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("イエローとブルーマテリアルをふんだんに使ったブレードだ。買うかね？");
                        break;
                    case "イスリアルフィスト": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("ほぼ透明で、重さを感じさせないが威力は確かなものとした。買うかね？");
                        break;
                    case "プリズマティックアーマー": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("カラーマテリアルを幾つか合成して作成したものだ。買うかね？");
                        break;
                    case "極薄合金製の羽衣": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("この薄さの合金に仕立てるのは苦労させられた。買うかね？");
                        break;

                    case "七色プリズムバンド": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("カラーマテリアルを上手く組み合わせて作ったアクセサリだ。買うかね？");
                        break;
                    case "再生の紋章": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("ウェルニッケの素材を極小にして、埋め込んだものだ。買うかね？");
                        break;
                    case "シールオブアクア＆ファイア": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("少し一風変わっておるだろ。買うかね？");
                        break;
                    case "ドラゴンのベルト": // ガンツの武具屋販売（ダンジョン４階）
                        UpdateMainMessage("希少価値のあるドラゴン素材を使ったものだ。買うかね？");
                        break;


                    // 武具屋で以下のものは販売予定ありません。
                    case "小さい赤ポーション":
                    case "普通の赤ポーション":
                    case "大きな赤ポーション":
                    case "特大赤ポーション":
                    case "豪華な赤ポーション":
                    case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                    case "神聖水": // ２階アイテム

                    case "練習用の剣": // アイン初期装備
                    case "ナックル": // ラナ初期装備
                    case "白銀の剣（レプリカ）": // ヴェルゼ初期装備
                    case "シャムシール": // ３階アイテム
                    case "エスパダス": // ダンジョン４階のアイテム
                    case "ルナ・エグゼキュージョナー": // ダンジョン５階
                    case "蒼黒・氷大蛇の爪": // ダンジョン５階
                    case "ファージル・ジ・エスペランザ": // ダンジョン５階
                    case "双剣  ジュノセレステ":
                    case "極剣  ゼムルギアス":
                    case "クロノス・ロマティッド・ソード":

                    case "コート・オブ・プレート": // アイン初期装備
                    case "ライト・クロス": // ラナ初期装備
                    case "黒真空の鎧（レプリカ）": // ヴェルゼ初期装備
                    case "真鍮の鎧": // ２階アイテム
                    case "プレート・アーマー": // ３階アイテム
                    case "ラメラ・アーマー": // ３階アイテム
                    case "ブリガンダィン": // ダンジョン４階のアイテム
                    case "ロリカ・セグメンタータ": // ダンジョン４階のアイテム
                    case "アヴォイド・クロス": // ダンジョン４階のアイテム
                    case "ソード・オブ・ブルールージュ": // ダンジョン４階のアイテム
                    case "ヘパイストス・パナッサロイニ":

                    case "珊瑚のブレスレット": // ラナ初期装備
                    case "天空の翼（レプリカ）": // ヴェルゼ初期装備
                    case "炎授天使の護符": // １階アイテム
                    case "チャクラオーブ": // １階アイテム
                    case "鷹の刻印": // ２階アイテム
                    case "身かわしのマント": // ２階アイテム
                    case "ライオンハート": // ３階アイテム
                    case "オーガの腕章": // ３階アイテム
                    case "鋼鉄の石像": // ３階アイテム
                    case "ファラ様信仰のシール": // ３階アイテム
                    case "剣紋章ペンダント": // ラナレベルアップ時でもらえるアイテム
                    case "夢見の印章": // ダンジョン４階のアイテム
                    case "天使の契約書": // ダンジョン４階のアイテム
                    case "エルミ・ジョルジュ　ファージル王家の刻印":
                    case "ファラ・フローレ　天使のペンダント":
                    case "シニキア・カールハンツ　魔道デビルアイ":
                    case "オル・ランディス　炎神グローブ":
                    case "ヴェルゼ・アーティ　天空の翼":

                    case "ブルーマテリアル": // １階アイテム
                    case "レッドマテリアル": // ３階アイテム
                    case "グリーンマテリアル": // ダンジョン４階のアイテム
                    case "リーベストランクポーション":
                    case "リヴァイヴポーション":
                    case "アカシジアの実":
                    case "遠見の青水晶": // 初期ラナ会話イベントで入手アイテム
                    case "オーバーシフティング": // ダンジョン５階
                    case "レジェンド・レッドホース": // ダンジョン５階
                    case "ラナのイヤリング": // ダンジョン５階（ラナのイベント）
                    case "タイム・オブ・ルーセ": // ダンジョン５階の隠しアイテム
                    default:
                        VendorBuyMessage(backpackData); // 後編編集
                        break; // 後編編集
                }
            }
            else
            {
                if (backpackData.Name == "神剣  フェルトゥーシュ")
                {
                    UpdateMainMessage(this.currentPlayer.GetCharacterSentence(3010));
                    return;
                }

                UpdateMainMessage(String.Format(this.currentPlayer.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString()));
            }
            this.currentSelectItem = backpackData;
        }

        public void Buy_Click()
        {
            if (this.currentSelectItem == null)
            {
                mainMessage.text = "ガンツ：ふむ・・・まずは購入するアイテムを選択しなさい。";
                return;
            }

        }

        private void SelectSellItem(ItemBackPack currentItem)
        {
            if (currentItem.Name == "" || currentItem.Name == String.Empty)
            {
                return;
            }

            if (currentItem.Cost <= 0)
            {
                MessageExchange5(); // 後編編集
                return;
            }
            // s 後編追加
            else if ((currentItem.Name == Database.LEGENDARY_FELTUS) ||
                     (currentItem.Name == Database.POOR_PRACTICE_SWORD_1) ||
                     (currentItem.Name == Database.POOR_PRACTICE_SWORD_2) ||
                     (currentItem.Name == Database.COMMON_PRACTICE_SWORD_3) ||
                     (currentItem.Name == Database.COMMON_PRACTICE_SWORD_4) ||
                     (currentItem.Name == Database.RARE_PRACTICE_SWORD_5) ||
                     (currentItem.Name == Database.RARE_PRACTICE_SWORD_6) ||
                     (currentItem.Name == Database.EPIC_PRACTICE_SWORD_7))
            {
                MessageExchange5();
                return;
            }
            // e 後編追加
            else
            {
                int stack = 1;
                // s 後編編集
                //stack = SelectSellStackValue(sender, e, currentItem, ii);
                //if (stack == -1) return; // 複数量指定の時、ESCキャンセルはｰ1で抜けてくるので、即時Return

                MessageExchange6(currentItem, stack);
            }
        }

        private void SellItem(ItemBackPack currentItem, Text sender, int stack, int ii)
        {
            GroundOne.MC.Gold += stack * currentItem.Cost / 2; // 後編編集
            labelGold.text = GroundOne.MC.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
            SellBackPackItem(currentItem, sender, stack, ii);
            MessageExchange3(); // 後編編集
        }
        public void Sell2_Click()
        {
        }
        public void Sell3_Click()
        {
        }
        private void MessageExchange2()
        {
            SetupMessageText(3002);
        }

        // [コメント]：引数が無限に増える可能性がある場合、記述方法が何かありそうです。時間があれば探してください。
        protected void SetupMessageText(int number)
        {
            if (!GroundOne.WE.AvailableEquipShop5)
            {
                mainMessage.text = ganz.GetCharacterSentence(number);
            }
            else
            {
                mainMessage.text = this.currentPlayer.GetCharacterSentence(number);
            }
            yesnoMessage.text = mainMessage.text;
        }
        protected void SetupMessageText(int number, string arg1)
        {
            if (!GroundOne.WE.AvailableEquipShop5)
            {
                mainMessage.text = String.Format(ganz.GetCharacterSentence(number), arg1);
            }
            else
            {
                mainMessage.text = String.Format(this.currentPlayer.GetCharacterSentence(number), arg1);
            }
            yesnoMessage.text = mainMessage.text;
        }
        protected void SetupMessageText(int number, string arg1, string arg2)
        {
            if (!GroundOne.WE.AvailableEquipShop5)
            {
                mainMessage.text = String.Format(ganz.GetCharacterSentence(number), arg1, arg2);
            }
            else
            {
                mainMessage.text = String.Format(this.currentPlayer.GetCharacterSentence(number), arg1, arg2);
            }
            yesnoMessage.text = mainMessage.text;
        }
        private void MessageExchange5()
        {
            SetupMessageText(3006);
        }

        private void MessageExchange6(ItemBackPack backpackData, int stack)
        {
            SetupMessageText(3007, backpackData.Name, (backpackData.Cost / 2).ToString());
        }

        private void MessageExchange3()
        {
            SetupMessageText(3003);
        }

        private void MessageExchange4()
        {
            SetupMessageText(3005);
        }

        private void UpdateMainMessage(string p)
        {
            // todo
        }

        protected int SelectSellStackValue(ItemBackPack backpackData, int ii)
        {
            int exchangeValue = Convert.ToInt32(backpackStack[ii].text.Remove(0, 1), 10);
            //if (this.IsShift) // todo
            //{
            //    using (SelectValue sv = new SelectValue())
            //    {
            //        sv.StartPosition = FormStartPosition.Manual;
            //        sv.Location = new Point(this.Location.X + backpackStack[ii].Location.X, this.Location.Y + backpackStack[ii].Location.Y + 15);
            //        sv.MaxValue = exchangeValue;
            //        sv.ShowDialog();
            //        IsShift = false; // ShowDialog表示先で、Shiftキーは外された場合検知できないため、ココでリセット。
            //        if (sv.DialogResult == DialogResult.Cancel) return -1; // ESCキャンセルは中断とみなす。
            //        return sv.CurrentValue;
            //    }
            //}
            //else
            {
                return exchangeValue;
            }
        }

        private void VendorComplete()
        {
            nowCannotBuy = false;
            nowSellItem = false;
            filter.SetActive(false);
            groupYesNoMessage.SetActive(false);
        }

        bool nowCannotBuy = false;
        public void Yes_Click()
        {
            if (nowCannotBuy)
            {
                // 何もしない
                MessageExchange4();
            }
            else if (nowSellItem == false)
            {
                if (GroundOne.MC.Gold < this.currentSelectItem.Cost)
                {
                    SetupMessageText(3004, Convert.ToString((this.currentSelectItem.Cost - GroundOne.MC.Gold)));
                    nowCannotBuy = true;
                    return;
                }

                if (!currentPlayer.AddBackPack(currentSelectItem))
                {
                    MessageExchange2(); // 後編編集
                    return;
                }
                GroundOne.MC.Gold -= this.currentSelectItem.Cost;
                labelGold.text = GroundOne.MC.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                UpdateBackPackLabel(this.currentPlayer);
                MessageExchange3(); // 後編編集
                this.currentSelectItem = null;
            }
            else
            {
                SellItem(this.currentSelectItem2, this.backpackList[0], 1, 0);
                UpdateBackPackLabel(this.currentPlayer);

                this.currentSelectItem2 = null;
            }

            VendorComplete();
        }

        public void No_Click()
        {
            MessageExchange4();
            VendorComplete();
        }

        private void VendorBuyMessage(ItemBackPack backpackData)
        {
            //mainMessage.text = String.Format(ganz.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString()); // 後編編集
            yesnoMessage.text = String.Format(ganz.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString()); // change unity
            groupYesNoMessage.SetActive(true);
            filter.SetActive(true);
        }

        public void tapChara1()
        {
            this.currentPlayer = GroundOne.MC;
            UpdateBackPackLabel(this.currentPlayer);
        }
        public void tapChara2()
        {
            this.currentPlayer = GroundOne.SC;
            UpdateBackPackLabel(this.currentPlayer);
        }
        public void tapChara3()
        {
            this.currentPlayer = GroundOne.TC;
            UpdateBackPackLabel(this.currentPlayer);
        }
        public void tapExit()
        {
            nowClose = true;
        }
    }
}

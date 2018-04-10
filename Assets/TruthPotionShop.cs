using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class TruthPotionShop : TruthEquipmentShop
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnInitialize()
        {
            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblTitle.text = Database.GUI_EQUIPSHOP_TITLE_POTION;
                //lblItemList.text = Database.GUI_EQUIPSHOP_ITEM;
                //lblCost.text = Database.GUI_EQUIPSHOP_COST;
                //lblBackpack.text = Database.GUI_EQUIPSHOP_BACKPACK;
                //lblBackpackStack.text = Database.GUI_EQUIPSHOP_STACK;
                lblExchange.text = Database.GUI_EQUIPSHOP_TOSELL;
                lblBuyButton.text = Database.GUI_EQUIPSHOP_BUY_BUTTON;
                lblSellButton.text = Database.GUI_EQUIPSHOP_SELL_BUTTON;
                lblClose.text = Database.GUI_EQUIPSHOP_CLOSE;
            }

            GameObject objLana = new GameObject("objLana");
            vendor = objLana.AddComponent<MainCharacter>();
            vendor.FullName = "ラナ・アミリア";
            vendor.FirstName = "ラナ";

            // GUI
            //for (int ii = 0; ii < vendorItemList.Length; ii++)
            //{
            //    if (vendorItemList[ii] != null)
            //    {
            //        vendorItemList[ii].transform.localPosition = new Vector3(0, -140 * ii, 0);
            //    }
            //}
            //for (int ii = 0; ii < playerItemList.Length; ii++)
            //{
            //    if (playerItemList[ii] != null)
            //    {
            //        playerItemList[ii].transform.localPosition = new Vector3(0, -140 * ii, 0);
            //    }
            //}
            GroupSell.SetActive(false);

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

            // debug
            //GroundOne.WE.AvailablePotion2 = true;
            //GroundOne.WE.AvailablePotion3 = true;
            //GroundOne.WE.AvailablePotion4 = true;
            //GroundOne.WE.AvailablePotion5 = true;

            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailablePotion2 = true;
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailablePotion3 = true;
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailablePotion4 = true;
            //if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailablePotion5 = true;

            if (/*GroundOne.WE.AvailablePotionshop && */!GroundOne.WE.AvailablePotion2)
            {
                SetupAvailableList(1);

                btnLevel1.gameObject.SetActive(false); // [コメント]：最初は増える傾向を見せない演出のため、VisibleはFalse
                btnLevel2.gameObject.SetActive(false);
                btnLevel3.gameObject.SetActive(false);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && !GroundOne.WE.AvailablePotion3)
            {
                SetupAvailableList(2);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(false);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && !GroundOne.WE.AvailablePotion4)
            {
                SetupAvailableList(3);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(true);
                btnLevel4.gameObject.SetActive(false);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && GroundOne.WE.AvailablePotion4 && !GroundOne.WE.AvailablePotion5)
            {
                SetupAvailableList(4);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(true);
                btnLevel4.gameObject.SetActive(true);
                btnLevel5.gameObject.SetActive(false);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && GroundOne.WE.AvailablePotion4 && GroundOne.WE.AvailablePotion5)
            {
                SetupAvailableList(5);
                btnLevel1.gameObject.SetActive(true);
                btnLevel2.gameObject.SetActive(true);
                btnLevel3.gameObject.SetActive(true);
                btnLevel4.gameObject.SetActive(true);
                btnLevel5.gameObject.SetActive(true);
            }
            SetupAvailableListWithCurrentCase();

            OnLoadMessage(); // 後編編集
            this.labelGold.text = GroundOne.MC.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。

            UpdateBackPackLabelInterface(GroundOne.MC);
        }

        protected override void CheckAndCallTruthItemDesc()
        {
            #region "１階"
            if (!GroundOne.WE2.PotionAvailable_11 && (GroundOne.WE2.PotionMixtureDay_11 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_11))
            {
                GroundOne.WE2.PotionAvailable_11 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_NATURALIZE);
            }
            else if (!GroundOne.WE2.PotionAvailable_12 && (GroundOne.WE2.PotionMixtureDay_12 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_12))
            {
                GroundOne.WE2.PotionAvailable_12 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_RESIST_FIRE);
            }
            else if (!GroundOne.WE2.PotionAvailable_13 && (GroundOne.WE2.PotionMixtureDay_13 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_13))
            {
                GroundOne.WE2.PotionAvailable_13 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_MAGIC_SEAL);
            }
            else if (!GroundOne.WE2.PotionAvailable_14 && (GroundOne.WE2.PotionMixtureDay_14 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_14))
            {
                GroundOne.WE2.PotionAvailable_14 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_ATTACK_SEAL);
            }
            else if (!GroundOne.WE2.PotionAvailable_15 && (GroundOne.WE2.PotionMixtureDay_15 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_15))
            {
                GroundOne.WE2.PotionAvailable_15 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_CURE_BLIND);
            }
            else if (!GroundOne.WE2.PotionAvailable_16 && (GroundOne.WE2.PotionMixtureDay_16 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_16))
            {
                GroundOne.WE2.PotionAvailable_16 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_POTION_MOSSGREEN_DREAM);
            }
            else if (!GroundOne.WE2.PotionAvailable_17 && (GroundOne.WE2.PotionMixtureDay_17 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_17))
            {
                GroundOne.WE2.PotionAvailable_17 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_DRYAD_SAGE_POTION);
            }
            #endregion
            #region "２階"
            else if (!GroundOne.WE2.PotionAvailable_21 && (GroundOne.WE2.PotionMixtureDay_21 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_21))
            {
                GroundOne.WE2.PotionAvailable_21 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_OVER_GROWTH);
            }
            else if (!GroundOne.WE2.PotionAvailable_22 && (GroundOne.WE2.PotionMixtureDay_22 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_22))
            {
                GroundOne.WE2.PotionAvailable_22 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_RAINBOW_IMPACT);
            }
            else if (!GroundOne.WE2.PotionAvailable_23 && (GroundOne.WE2.PotionMixtureDay_23 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_23))
            {
                GroundOne.WE2.PotionAvailable_23 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_BLACK_GAST);
            }
            else if (!GroundOne.WE2.PotionAvailable_24 && (GroundOne.WE2.PotionMixtureDay_24 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_24))
            {
                GroundOne.WE2.PotionAvailable_24 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_SOUKAI_DRINK_SS);
            }
            else if (!GroundOne.WE2.PotionAvailable_25 && (GroundOne.WE2.PotionMixtureDay_25 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_25))
            {
                GroundOne.WE2.PotionAvailable_25 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_TUUKAI_DRINK_DD);
            }
            #endregion
            #region "３階"
            else if (!GroundOne.WE2.PotionAvailable_31 && (GroundOne.WE2.PotionMixtureDay_31 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_31))
            {
                GroundOne.WE2.PotionAvailable_31 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_FAIRY_BREATH);
            }

            else if (!GroundOne.WE2.PotionAvailable_32 && (GroundOne.WE2.PotionMixtureDay_32 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_32))
            {
                GroundOne.WE2.PotionAvailable_32 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_HEART_ACCELERATION);
            }
            else if (!GroundOne.WE2.PotionAvailable_33 && (GroundOne.WE2.PotionMixtureDay_33 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_33))
            {
                GroundOne.WE2.PotionAvailable_33 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_SAGE_POTION_MINI);
            }
            #endregion
            #region "４階"
            else if (!GroundOne.WE2.PotionAvailable_41 && (GroundOne.WE2.PotionMixtureDay_41 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_41))
            {
                GroundOne.WE2.PotionAvailable_41 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_POWER_SURGE);
            }
            else if (!GroundOne.WE2.PotionAvailable_42 && (GroundOne.WE2.PotionMixtureDay_42 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_42))
            {
                GroundOne.WE2.PotionAvailable_42 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ELEMENTAL_SEAL);
            }
            else if (!GroundOne.WE2.PotionAvailable_43 && (GroundOne.WE2.PotionMixtureDay_43 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_43))
            {
                GroundOne.WE2.PotionAvailable_43 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_GENSEI_MAGIC_BOTTLE);
            }
            else if (!GroundOne.WE2.PotionAvailable_44 && (GroundOne.WE2.PotionMixtureDay_44 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_44))
            {
                GroundOne.WE2.PotionAvailable_44 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_GENSEI_TAIMA_KUSURI);
            }
            else if (!GroundOne.WE2.PotionAvailable_45 && (GroundOne.WE2.PotionMixtureDay_45 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_45))
            {
                GroundOne.WE2.PotionAvailable_45 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_MIND_ILLUSION);
            }
            else if (!GroundOne.WE2.PotionAvailable_46 && (GroundOne.WE2.PotionMixtureDay_46 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_46))
            {
                GroundOne.WE2.PotionAvailable_46 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_SHINING_AETHER);
            }
            else if (!GroundOne.WE2.PotionAvailable_47 && (GroundOne.WE2.PotionMixtureDay_47 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_47))
            {
                GroundOne.WE2.PotionAvailable_47 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ZETTAI_STAMINAUP);
            }
            else if (!GroundOne.WE2.PotionAvailable_48 && (GroundOne.WE2.PotionMixtureDay_48 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_48))
            {
                GroundOne.WE2.PotionAvailable_48 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_BLACK_ELIXIR);
            }
            else if (!GroundOne.WE2.PotionAvailable_49 && (GroundOne.WE2.PotionMixtureDay_49 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_49))
            {
                GroundOne.WE2.PotionAvailable_49 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ZEPHER_BREATH);
            }
            else if (!GroundOne.WE2.PotionAvailable_410 && (GroundOne.WE2.PotionMixtureDay_410 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_410))
            {
                GroundOne.WE2.PotionAvailable_410 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_COLORLESS_ANTIDOTE);
            }
            #endregion
        }

        protected override void SetupAvailableListWithCurrentCase()
        {
            if (GroundOne.WE.AvailablePotionshop && !GroundOne.WE.AvailablePotion2)
            {
                SetupAvailableList(1);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && !GroundOne.WE.AvailablePotion3)
            {
                SetupAvailableList(2);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && !GroundOne.WE.AvailablePotion4)
            {
                SetupAvailableList(3);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && GroundOne.WE.AvailablePotion4 && !GroundOne.WE.AvailablePotion5)
            {
                SetupAvailableList(4);
            }
            else if (GroundOne.WE.AvailablePotionshop && GroundOne.WE.AvailablePotion2 && GroundOne.WE.AvailablePotion3 && GroundOne.WE.AvailablePotion4 && GroundOne.WE.AvailablePotion5)
            {
                SetupAvailableList(5);
            }
        }

        protected override void SetupAvailableList(int level)
        {
            Debug.Log("potion setupavailablelist");
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                vendorList[ii].text = "";
                vendorCostList[ii].text = "";
                vendorItemList[ii].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                vendorImageList[ii].sprite = null;
            }

            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    int ii = 0;
                    item = new ItemBackPack(Database.POOR_SMALL_RED_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.POOR_SMALL_BLUE_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.POOR_SMALL_GREEN_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.POOR_POTION_CURE_POISON);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_11)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_NATURALIZE);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_12)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_MAGIC_SEAL);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_13)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_ATTACK_SEAL);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_14)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_CURE_BLIND);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_15)
                    {
                        item = new ItemBackPack(Database.RARE_POTION_MOSSGREEN_DREAM);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;
                    break;

                case 2:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_NORMAL_BLUE_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_NORMAL_GREEN_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_RESIST_POISON);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_21)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_OVER_GROWTH);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_22)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_RAINBOW_IMPACT);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_23)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_BLACK_GAST);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;
                    break;

                case 3:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_LARGE_RED_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LARGE_BLUE_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LARGE_GREEN_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_31)
                    {
                        item = new ItemBackPack(Database.COMMON_FAIRY_BREATH);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_32)
                    {
                        item = new ItemBackPack(Database.COMMON_HEART_ACCELERATION);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_33)
                    {
                        item = new ItemBackPack(Database.RARE_SAGE_POTION_MINI);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;
                    break;

                case 4:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_HUGE_RED_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_HUGE_BLUE_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_HUGE_GREEN_POTION);
                    vendorList[ii].text = item.Name;
                    SetupItemLayout(item, ii);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_41)
                    {
                        item = new ItemBackPack(Database.RARE_POWER_SURGE);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_42)
                    {
                        item = new ItemBackPack(Database.RARE_ELEMENTAL_SEAL);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_43)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_MAGIC_BOTTLE);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_44)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_TAIMA_KUSURI);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_45)
                    {
                        item = new ItemBackPack(Database.RARE_MIND_ILLUSION);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_46)
                    {
                        item = new ItemBackPack(Database.RARE_SHINING_AETHER);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_47)
                    {
                        item = new ItemBackPack(Database.RARE_ZETTAI_STAMINAUP);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_48)
                    {
                        item = new ItemBackPack(Database.RARE_BLACK_ELIXIR);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_49)
                    {
                        item = new ItemBackPack(Database.RARE_ZEPHER_BREATH);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_410)
                    {
                        item = new ItemBackPack(Database.RARE_COLORLESS_ANTIDOTE);
                        vendorList[ii].text = item.Name;
                        SetupItemLayout(item, ii);
                    }
                    ii++;
                    break;
            }

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (vendorList[ii].text != "")
                {
                    ItemBackPack temp4 = new ItemBackPack(vendorList[ii].text);
                    vendorCostList[ii].text = temp4.Cost.ToString();
                    vendorList[ii].gameObject.SetActive(true);
                    vendorItemList[ii].SetActive(true);
                }
                else
                {
                    vendorList[ii].text = "";
                    vendorCostList[ii].text = "";
                    vendorItemList[ii].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    vendorImageList[ii].sprite = null;
                }
            }
        }

        protected override void OnLoadMessage()
        {
            mainMessage.text = this.vendor.GetCharacterSentence(3013);
            //yesnoMessage.text = this.vendor.GetCharacterSentence(3013);
        }

        protected override void MessageExchange1(ItemBackPack backpackData, MainCharacter player)
        {
            SetupMessageText(3016, Convert.ToString((backpackData.Cost - player.Gold)));
        }

        protected override void MessageExchange2()
        {
            SetupMessageText(3018);
        }

        protected override void MessageExchange3()
        {
            SetupMessageText(3017);
        }

        protected override void MessageExchange4()
        {
            SetupMessageText(3019);
        }

        protected override void MessageExchange5()
        {
            SetupMessageText(3020);
        }

        protected override void MessageExchange6(ItemBackPack backpackData, int stack)
        {
            SetupMessageText(3021, backpackData.Name, ((backpackData.Cost / 2) * stack).ToString());
        }

        protected override void MessageExchange7()
        {
            SetupMessageText(3014);
        }

        protected override void MessageExchange8(ItemBackPack backpackData)
        {
            DescriptionText2.text = String.Format(vendor.GetCharacterSentence(3015), backpackData.Name, backpackData.Cost.ToString());
        }            
    }
}

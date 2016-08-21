using UnityEngine;
using System.Collections;
using System;

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
            base.labelTitle.text = "ラナのランラン薬品店♪";
            GameObject objLana = new GameObject("objLana");
            vendor = objLana.AddComponent<MainCharacter>();
            vendor.FullName = "ラナ・アミリア";
            vendor.FirstName = "ラナ";

            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailablePotion2 = true;
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailablePotion3 = true;
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailablePotion4 = true;
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailablePotion5 = true;

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
            UpdateEquipment(GroundOne.MC);
        }

        protected override void CheckAndCallTruthItemDesc()
        {
            #region "１階"
            if (!GroundOne.WE2.PotionAvailable_11 && (GroundOne.WE2.PotionMixtureDay_11 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_11))
            {
                GroundOne.WE2.PotionAvailable_11 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_NATURALIZE);
            }
            if (!GroundOne.WE2.PotionAvailable_12 && (GroundOne.WE2.PotionMixtureDay_12 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_12))
            {
                GroundOne.WE2.PotionAvailable_12 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_RESIST_FIRE);
            }
            if (!GroundOne.WE2.PotionAvailable_13 && (GroundOne.WE2.PotionMixtureDay_13 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_13))
            {
                GroundOne.WE2.PotionAvailable_13 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_MAGIC_SEAL);
            }
            if (!GroundOne.WE2.PotionAvailable_14 && (GroundOne.WE2.PotionMixtureDay_14 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_14))
            {
                GroundOne.WE2.PotionAvailable_14 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_ATTACK_SEAL);
            }
            if (!GroundOne.WE2.PotionAvailable_15 && (GroundOne.WE2.PotionMixtureDay_15 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_15))
            {
                GroundOne.WE2.PotionAvailable_15 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_CURE_BLIND);
            }
            if (!GroundOne.WE2.PotionAvailable_16 && (GroundOne.WE2.PotionMixtureDay_16 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_16))
            {
                GroundOne.WE2.PotionAvailable_16 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_POTION_MOSSGREEN_DREAM);
            }
            if (!GroundOne.WE2.PotionAvailable_17 && (GroundOne.WE2.PotionMixtureDay_17 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_17))
            {
                GroundOne.WE2.PotionAvailable_17 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_DRYAD_SAGE_POTION);
            }
            #endregion
            #region "２階"
            if (!GroundOne.WE2.PotionAvailable_21 && (GroundOne.WE2.PotionMixtureDay_21 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_21))
            {
                GroundOne.WE2.PotionAvailable_21 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_OVER_GROWTH);
            }
            if (!GroundOne.WE2.PotionAvailable_22 && (GroundOne.WE2.PotionMixtureDay_22 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_22))
            {
                GroundOne.WE2.PotionAvailable_22 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_RAINBOW_IMPACT);
            }
            if (!GroundOne.WE2.PotionAvailable_23 && (GroundOne.WE2.PotionMixtureDay_23 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_23))
            {
                GroundOne.WE2.PotionAvailable_23 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_POTION_BLACK_GAST);
            }
            if (!GroundOne.WE2.PotionAvailable_24 && (GroundOne.WE2.PotionMixtureDay_24 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_24))
            {
                GroundOne.WE2.PotionAvailable_24 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_SOUKAI_DRINK_SS);
            }
            if (!GroundOne.WE2.PotionAvailable_25 && (GroundOne.WE2.PotionMixtureDay_25 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_25))
            {
                GroundOne.WE2.PotionAvailable_25 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_TUUKAI_DRINK_DD);
            }
            #endregion
            #region "３階"
            if (!GroundOne.WE2.PotionAvailable_31 && (GroundOne.WE2.PotionMixtureDay_31 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_31))
            {
                GroundOne.WE2.PotionAvailable_31 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_FAIRY_BREATH);
            }

            if (!GroundOne.WE2.PotionAvailable_32 && (GroundOne.WE2.PotionMixtureDay_32 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_32))
            {
                GroundOne.WE2.PotionAvailable_32 = true;
                SceneDimension.CallTruthItemDesc(this, Database.COMMON_HEART_ACCELERATION);
            }
            if (!GroundOne.WE2.PotionAvailable_33 && (GroundOne.WE2.PotionMixtureDay_33 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_33))
            {
                GroundOne.WE2.PotionAvailable_33 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_SAGE_POTION_MINI);
            }
            #endregion
            #region "４階"
            if (!GroundOne.WE2.PotionAvailable_41 && (GroundOne.WE2.PotionMixtureDay_41 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_41))
            {
                GroundOne.WE2.PotionAvailable_41 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_POWER_SURGE);
            }
            if (!GroundOne.WE2.PotionAvailable_42 && (GroundOne.WE2.PotionMixtureDay_42 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_42))
            {
                GroundOne.WE2.PotionAvailable_42 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ELEMENTAL_SEAL);
            }
            if (!GroundOne.WE2.PotionAvailable_43 && (GroundOne.WE2.PotionMixtureDay_43 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_43))
            {
                GroundOne.WE2.PotionAvailable_43 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_GENSEI_MAGIC_BOTTLE);
            }
            if (!GroundOne.WE2.PotionAvailable_44 && (GroundOne.WE2.PotionMixtureDay_44 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_44))
            {
                GroundOne.WE2.PotionAvailable_44 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_GENSEI_TAIMA_KUSURI);
            }
            if (!GroundOne.WE2.PotionAvailable_45 && (GroundOne.WE2.PotionMixtureDay_45 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_45))
            {
                GroundOne.WE2.PotionAvailable_45 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_MIND_ILLUSION);
            }
            if (!GroundOne.WE2.PotionAvailable_46 && (GroundOne.WE2.PotionMixtureDay_46 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_46))
            {
                GroundOne.WE2.PotionAvailable_46 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_SHINING_AETHER);
            }
            if (!GroundOne.WE2.PotionAvailable_47 && (GroundOne.WE2.PotionMixtureDay_47 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_47))
            {
                GroundOne.WE2.PotionAvailable_47 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ZETTAI_STAMINAUP);
            }
            if (!GroundOne.WE2.PotionAvailable_48 && (GroundOne.WE2.PotionMixtureDay_48 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_48))
            {
                GroundOne.WE2.PotionAvailable_48 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_BLACK_ELIXIR);
            }
            if (!GroundOne.WE2.PotionAvailable_49 && (GroundOne.WE2.PotionMixtureDay_49 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_49))
            {
                GroundOne.WE2.PotionAvailable_49 = true;
                SceneDimension.CallTruthItemDesc(this, Database.RARE_ZEPHER_BREATH);
            }
            if (!GroundOne.WE2.PotionAvailable_410 && (GroundOne.WE2.PotionMixtureDay_410 != 0) && (GroundOne.WE.GameDay > GroundOne.WE2.PotionMixtureDay_410))
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
                equipList[ii].text = "";
                costList[ii].text = "";
            }

            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    int ii = 0;
                    item = new ItemBackPack(Database.POOR_SMALL_RED_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.POOR_SMALL_BLUE_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.POOR_SMALL_GREEN_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.POOR_POTION_CURE_POISON);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_11)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_NATURALIZE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_12)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_MAGIC_SEAL);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_13)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_ATTACK_SEAL);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_14)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_CURE_BLIND);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_15)
                    {
                        item = new ItemBackPack(Database.RARE_POTION_MOSSGREEN_DREAM);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;
                    break;

                case 2:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_NORMAL_BLUE_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_NORMAL_GREEN_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_RESIST_POISON);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_21)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_OVER_GROWTH);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_22)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_RAINBOW_IMPACT);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_23)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_BLACK_GAST);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;
                    break;

                case 3:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_LARGE_RED_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LARGE_BLUE_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LARGE_GREEN_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_31)
                    {
                        item = new ItemBackPack(Database.COMMON_FAIRY_BREATH);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_32)
                    {
                        item = new ItemBackPack(Database.COMMON_HEART_ACCELERATION);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_33)
                    {
                        item = new ItemBackPack(Database.RARE_SAGE_POTION_MINI);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;
                    break;

                case 4:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_HUGE_RED_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_HUGE_BLUE_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_HUGE_GREEN_POTION);
                    equipList[ii].text = item.Name;
                    Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_41)
                    {
                        item = new ItemBackPack(Database.RARE_POWER_SURGE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_42)
                    {
                        item = new ItemBackPack(Database.RARE_ELEMENTAL_SEAL);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_43)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_MAGIC_BOTTLE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_44)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_TAIMA_KUSURI);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_45)
                    {
                        item = new ItemBackPack(Database.RARE_MIND_ILLUSION);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_46)
                    {
                        item = new ItemBackPack(Database.RARE_SHINING_AETHER);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_47)
                    {
                        item = new ItemBackPack(Database.RARE_ZETTAI_STAMINAUP);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_48)
                    {
                        item = new ItemBackPack(Database.RARE_BLACK_ELIXIR);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_49)
                    {
                        item = new ItemBackPack(Database.RARE_ZEPHER_BREATH);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.PotionAvailable_410)
                    {
                        item = new ItemBackPack(Database.RARE_COLORLESS_ANTIDOTE);
                        equipList[ii].text = item.Name;
                        Method.UpdateRareColor(item, equipList[ii], backEquip[ii]);
                    }
                    ii++;
                    break;
            }

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (equipList[ii].text != "")
                {
                    ItemBackPack temp4 = new ItemBackPack(equipList[ii].text);
                    costList[ii].text = temp4.Cost.ToString();
                    backEquip[ii].SetActive(true);
                    backCost[ii].SetActive(true);
                }
                else
                {
                    costList[ii].text = "";
                    backEquip[ii].SetActive(false);
                    backCost[ii].SetActive(false);
                }
            }
        }

        protected override void OnLoadMessage()
        {
            mainMessage.text = this.vendor.GetCharacterSentence(3013);
            yesnoMessage.text = this.vendor.GetCharacterSentence(3013);
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
            yesnoMessage.text = String.Format(vendor.GetCharacterSentence(3015), backpackData.Name, backpackData.Cost.ToString());
        }            
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace DungeonPlayer
{     
    public class SingleHomeTown : MotherForm
    {
        public GameObject groupDungeon;
        public GameObject groupInn;
        public GameObject groupShop;
        public GameObject groupDuel;
        public GameObject groupCastle;
        public GameObject groupStatus;
        public GameObject groupParty;
        public Text txtName;
        public Text txtLevel;
        public Text txtExperience;
        public Image imgExpGauge;
        public Text life;
        public Image imgLifeGauge;
        public Text mana;
        public Image imgManaGauge;
        public Text skill;
        public Image imgSkillGauge;
        public Text totalStrength;
        public Text totalAgility;
        public Text totalIntelligence;
        public Text totalStamina;
        public Text totalMind;
        public GameObject back_weapon;
        public GameObject back_subWeapon;
        public GameObject back_armor;
        public GameObject back_accessory;
        public GameObject back_accessory2;
        public Text weapon;
        public Image imgMainWeapon;
        public Text subWeapon;
        public Image imgSubWeapon;
        public Text armor;
        public Image imgArmor;
        public Text accessory;
        public Image imgAccessory1;
        public Text accessory2;
        public Image imgAccessory2;
        public Text mainMessage;
        public Text[] partyMemberLevel;
        public Text[] partyMemberName;
        public Text[] partyMemberLife;
        public Text[] partyMemberMana;
        public Text[] partyMemberSkill;
        public Text[] partyMemberStrength;
        public Text[] partyMemberAgility;
        public Text[] partyMemberIntelligence;
        public Text[] partyMemberStamina;
        public Text[] partyMemberMind;

        private enum CoreType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }
        public override void Start()
        {
            base.Start();

            GroundOne.WE.AlreadyRest = true;

            if (GroundOne.WE.AlreadyRest)
            {
                base.Background.GetComponent<Image>().color = Color.white;
            }
            else
            {
                base.Background.GetComponent<Image>().color = new Color(120.0f/255.0f, 100.0f/255.0f, 77.0f/255.0f, 1.0f);
            }

            SettingCharacterData(GroundOne.P1, 0);
            SettingCharacterData(GroundOne.P2, 1);
            SettingCharacterData(GroundOne.P3, 2);
        }
        public override void Update()
        {
            base.Update();
        }

        public void TapDungeon()
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                mainMessage.text = "サンディ：夕方以降の活動は禁止されている！宿屋でお休みになられるがよい！";
                return;
            }

            GroupView(true, false, false, false, false, false, false);
        }
        public void TapInn()
        {
            if (GroundOne.WE.AlreadyRest)
            {
                mainMessage.text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                return;
            }
            GroupView(false, true, false, false, false, false, false);
        }
        public void TapShop()
        {
            GroupView(false, false, true, false, false, false, false);
        }
        public void TapDuel()
        {
            GroupView(false, false, false, true, false, false, false);
        }
        public void TapCastle()
        {
            GroupView(false, false, false, false, true, false, false);
        }
        public void TapStatus()
        {
            GroupView(false, false, false, false, false, true, false);
        }
        public void TapParty()
        {
            GroupView(false, false, false, false, false, false, true);
        }
        public void TapNone()
        {
            GroupView(false, false, false, false, false, false, false);                
        }

        private void GroupView(bool g1, bool g2, bool g3, bool g4, bool g5, bool g6, bool g7)
        {
            groupDungeon.SetActive(g1);
            groupInn.SetActive(g2);
            groupShop.SetActive(g3);
            groupDuel.SetActive(g4);
            groupCastle.SetActive(g5);
            groupStatus.SetActive(g6);
            groupParty.SetActive(g7);
        }

        public void TapGo()
        {
            GroundOne.WE.DungeonArea = 1;
            SceneDimension.JumpToSingleDungeon(false);
        }

        public void TapRestInn()
        {
            GroundOne.WE.AlreadyRest = true;
            GroundOne.P1.MaxGain();
            GroundOne.P2.MaxGain();
            GroundOne.P3.MaxGain();
        }

        private void SettingCharacterData(MainCharacter player, int num)
        {
            this.txtName.text = player.FullName;
            this.partyMemberName[num].text = player.FullName;
            this.txtLevel.text = player.Level.ToString();
            this.partyMemberLevel[num].text = player.Level.ToString();
            if (player.Level < Method.GetMaxLevel())
            {
                this.txtExperience.text = player.Exp.ToString() + " / " + player.NextLevelBorder.ToString();
                float dx = (float)player.Exp / (float)player.NextLevelBorder;
                imgExpGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
            else
            {
                this.txtExperience.text = "-----" + " / " + "-----";
                imgExpGauge.rectTransform.localScale = new Vector2(0.0f, 1.0f);
            }


            SettingCoreParameter(CoreType.Strength, player.Strength, player.BuffStrength_Accessory, player.BuffStrength_Food, this.totalStrength, this.partyMemberStrength[num]);
            SettingCoreParameter(CoreType.Agility, player.Agility, player.BuffAgility_Accessory, player.BuffAgility_Food, this.totalAgility, this.partyMemberAgility[num]);
            SettingCoreParameter(CoreType.Intelligence, player.Intelligence, player.BuffIntelligence_Accessory, player.BuffIntelligence_Food, this.totalIntelligence, this.partyMemberIntelligence[num]);
            SettingCoreParameter(CoreType.Stamina, player.Stamina, player.BuffStamina_Accessory, player.BuffStamina_Food, this.totalStamina, this.partyMemberStamina[num]);
            SettingCoreParameter(CoreType.Mind, player.Mind, player.BuffMind_Accessory, player.BuffMind_Food, this.totalMind, this.partyMemberMind[num]);

            this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.partyMemberLife[num].text = player.MaxLife.ToString();
            float dxLife = (float)player.CurrentLife / (float)player.MaxLife;
            imgLifeGauge.rectTransform.localScale = new Vector2(dxLife, 1.0f);

            mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            this.partyMemberMana[num].text = player.MaxMana.ToString();
            float dxMana = (float)player.CurrentMana / (float)player.MaxMana;
            imgManaGauge.rectTransform.localScale = new Vector2(dxMana, 1.0f);

            if (player.CurrentSkillPoint > player.MaxSkillPoint)
            {
                player.CurrentSkillPoint = player.MaxSkillPoint;
            }
            skill.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            this.partyMemberSkill[num].text = player.MaxSkillPoint.ToString();
            float dxSkill = (float)player.CurrentSkillPoint / (float)player.MaxSkillPoint;
            imgSkillGauge.rectTransform.localScale = new Vector2(dxSkill, 1.0f);

            this.weapon.text = "";
            this.imgMainWeapon.sprite = null;
            this.subWeapon.text = "";
            this.imgSubWeapon.sprite = null;
            this.armor.text = "";
            this.imgArmor.sprite = null;
            this.accessory.text = "";
            this.imgAccessory1.sprite = null;
            this.accessory2.text = "";
            this.imgAccessory2.sprite = null;
            if (player.MainWeapon != null)
            {
                this.weapon.text = player.MainWeapon.Name;
                Method.UpdateItemImage(player.MainWeapon, this.imgMainWeapon);
            }
            else
            {
                this.weapon.text = "";
            }
            Method.UpdateRareColor(player.MainWeapon, weapon, back_weapon, null);

            if (player.SubWeapon != null)
            {
                this.subWeapon.text = player.SubWeapon.Name;
                Method.UpdateItemImage(player.SubWeapon, this.imgSubWeapon);
            }
            else
            {
                this.subWeapon.text = "";
            }
            Method.UpdateRareColor(player.SubWeapon, subWeapon, back_subWeapon, null);

            if (player.MainArmor != null)
            {
                this.armor.text = player.MainArmor.Name;
                Method.UpdateItemImage(player.MainArmor, this.imgArmor);
            }
            else
            {
                this.armor.text = "";
            }
            Method.UpdateRareColor(player.MainArmor, armor, back_armor, null);

            if (player.Accessory != null)
            {
                this.accessory.text = player.Accessory.Name;
                Method.UpdateItemImage(player.Accessory, this.imgAccessory1);
            }
            else
            {
                this.accessory.text = "";
            }
            Method.UpdateRareColor(player.Accessory, accessory, back_accessory, null);

            if (player.Accessory2 != null)
            {
                this.accessory2.text = player.Accessory2.Name;
                Method.UpdateItemImage(player.Accessory2, this.imgAccessory2);
            }
            else
            {
                this.accessory2.text = "";
            }
            Method.UpdateRareColor(player.Accessory2, accessory2, back_accessory2, null);

            // single-delete
            //Method.UpdateBackPackLabel(player, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);
            //UpdateSpellSkillLabel(player);
            //UpdateResistStatus(player);
        }


        private void SettingCoreParameter(CoreType coreType, int basicValue, int addAccessoryValue, int addFoodValue, Text txtTotal, Text txtTotal2)
        {
            int totalValue = 0;

            totalValue = basicValue;
            //txtBasic.text = basicValue.ToString();

            //txtBuff.text = "+" + addAccessoryValue.ToString();
            totalValue += addAccessoryValue;

            //txtFood.text = "+" + addFoodValue.ToString();
            totalValue += addFoodValue;

            txtTotal.text = "" + totalValue.ToString();
            txtTotal2.text = totalValue.ToString();
        }
    }
}
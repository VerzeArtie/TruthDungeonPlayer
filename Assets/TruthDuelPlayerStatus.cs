using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DungeonPlayer
{
    public class TruthDuelPlayerStatus : MotherForm
    {

        public GameObject[] back_Backpack;
        public Text[] backpack;
        public Text[] backpackStack;
        public Image[] backpackIcon;
        public Text txtName;
        public Text txtLevel;
        public Text life;
        public Text mana;
        public Text skill;
        public Text strength;
        public Text addStrength;
        public Text addStrengthFood;
        public Text totalStrength;
        public Text agility;
        public Text addAgility;
        public Text addAgilityFood;
        public Text totalAgility;
        public Text intelligence;
        public Text addIntelligence;
        public Text addIntelligenceFood;
        public Text totalIntelligence;
        public Text stamina;
        public Text addStamina;
        public Text addStaminaFood;
        public Text totalStamina;
        public Text mind;
        public Text addMind;
        public Text addMindFood;
        public Text totalMind;
        public Text weapon;
        public Text subWeapon;
        public Text armor;
        public Text accessory;
        public Text accessory2;
        public GameObject back_weapon;
        public GameObject back_subWeapon;
        public GameObject back_armor;
        public GameObject back_accessory;
        public GameObject back_accessory2;
        public Text mainMessage;

        public override void Start()
        {
            base.Start();

            GameObject baseObj = new GameObject("DuelPlayer");
            TruthEnemyCharacter player = baseObj.AddComponent<TruthEnemyCharacter>();
            player.Initialize(GroundOne.DuelPlayerName);

            SettingCharacterData(player);

            ItemBackPack[] backpackData = player.GetBackPackInfo();
            Method.UpdateBackPackLabel(player, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);
        }

        public override void Update()
        {
            base.Update();
        }

        private void SettingCharacterData(MainCharacter chara)
        {
            this.txtName.text = chara.FullName;
            this.txtLevel.text = chara.Level.ToString();

            SettingCoreParameter(CoreType.Strength, chara.Strength, chara.BuffStrength_Accessory, chara.BuffStrength_Food, this.strength, this.addStrength, this.addStrengthFood, this.totalStrength);
            SettingCoreParameter(CoreType.Intelligence, chara.Intelligence, chara.BuffIntelligence_Accessory, chara.BuffIntelligence_Food, this.intelligence, this.addIntelligence, this.addIntelligenceFood, this.totalIntelligence);
            SettingCoreParameter(CoreType.Agility, chara.Agility, chara.BuffAgility_Accessory, chara.BuffAgility_Food, this.agility, this.addAgility, this.addAgilityFood, this.totalAgility);
            SettingCoreParameter(CoreType.Stamina, chara.Stamina, chara.BuffStamina_Accessory, chara.BuffStamina_Food, this.stamina, this.addStamina, this.addStaminaFood, this.totalStamina);
            SettingCoreParameter(CoreType.Mind, chara.Mind, chara.BuffMind_Accessory, chara.BuffMind_Food, this.mind, this.addMind, this.addMindFood, this.totalMind);

            this.life.text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

            if (chara.AvailableSkill)
            {
                if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
                {
                    chara.CurrentSkillPoint = chara.MaxSkillPoint;
                }
                skill.text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
            }

            if (chara.AvailableMana)
            {
                mana.text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
            }

            this.weapon.text = "";
            this.subWeapon.text = "";
            this.armor.text = "";
            this.accessory.text = "";
            this.accessory2.text = "";
            if (chara.MainWeapon != null)
            {
                this.weapon.text = chara.MainWeapon.Name;
            }
            else
            {
                this.weapon.text = "";
            }
            Method.UpdateRareColor(chara.MainWeapon, weapon, back_weapon);

            if (chara.SubWeapon != null)
            {
                this.subWeapon.text = chara.SubWeapon.Name;
            }
            else
            {
                this.subWeapon.text = "";
            }
            Method.UpdateRareColor(chara.SubWeapon, subWeapon, back_subWeapon);

            if (chara.MainArmor != null)
            {
                this.armor.text = chara.MainArmor.Name;
            }
            else
            {
                this.armor.text = "";
            }
            Method.UpdateRareColor(chara.MainArmor, armor, back_armor);

            if (chara.Accessory != null)
            {
                this.accessory.text = chara.Accessory.Name;
            }
            else
            {
                this.accessory.text = "";
            }
            Method.UpdateRareColor(chara.Accessory, accessory, back_accessory);

            if (chara.Accessory2 != null)
            {
                this.accessory2.text = chara.Accessory2.Name;
            }
            else
            {
                this.accessory2.text = "";
            }
            Method.UpdateRareColor(chara.Accessory2, accessory2, back_accessory2);
        }

        private enum CoreType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }

        private void SettingCoreParameter(CoreType coreType, int basicValue, int addAccessoryValue, int addFoodValue, Text txtBasic, Text txtBuff, Text txtFood, Text txtTotal)
        {
            int totalValue = 0;

            totalValue = basicValue;
            txtBasic.text = basicValue.ToString();

            txtBuff.text = "+" + addAccessoryValue.ToString();
            totalValue += addAccessoryValue;

            txtFood.text = "+" + addFoodValue.ToString();
            totalValue += addFoodValue;

            txtTotal.text = "= " + totalValue.ToString();
        }
        public void tapExit()
        {
            SceneDimension.Back(this);
        }

        public void StatusPlayer_MouseEnter(Text sender)
        {
            if (sender.text == "")
            {
                mainMessage.text = "";
            }
            else
            {
                ItemBackPack temp = new ItemBackPack(sender.text);
                mainMessage.text = temp.Description;
            }
        }

    }
}
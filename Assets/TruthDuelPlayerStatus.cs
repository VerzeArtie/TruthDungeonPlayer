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
        public Text agility;
        public Text addAgility;
        public Text intelligence;
        public Text addIntelligence;
        public Text stamina;
        public Text addStamina;
        public Text mind;
        public Text addMind;
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
            Debug.Log("SettingCharacterData (S)");
            this.txtName.text = chara.FullName;
            this.txtLevel.text = chara.Level.ToString();

            this.strength.text = chara.Strength.ToString();
            if (chara.BuffStrength_Accessory == 0) this.addStrength.text = "";
            else this.addStrength.text = " + " + chara.BuffStrength_Accessory.ToString();

            this.agility.text = chara.Agility.ToString();
            if (chara.BuffAgility_Accessory == 0) this.addAgility.text = "";
            else this.addAgility.text = " + " + chara.BuffAgility_Accessory.ToString();

            this.intelligence.text = chara.Intelligence.ToString();
            if (chara.BuffIntelligence_Accessory == 0) this.addIntelligence.text = "";
            else this.addIntelligence.text = " + " + chara.BuffIntelligence_Accessory.ToString();

            this.stamina.text = chara.Stamina.ToString();
            if (chara.BuffStamina_Accessory == 0) this.addStamina.text = "";
            else this.addStamina.text = " + " + chara.BuffStamina_Accessory.ToString();

            this.mind.text = chara.Mind.ToString();
            if (chara.BuffMind_Accessory == 0) this.addMind.text = "";
            else this.addMind.text = " + " + chara.BuffMind_Accessory.ToString();
            
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
            Debug.Log("SettingCharacterData (E)");
        }
        
        public void tapExit()
        {
            GroundOne.ParentScene.SceneBack();
            Application.UnloadLevel(Database.TruthDuelPlayerStatus);
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
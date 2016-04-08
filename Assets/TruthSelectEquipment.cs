using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DungeonPlayer
{
    public class TruthSelectEquipment : MotherForm
    {
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
        public Text PhysicalAttack;
        public Text PhysicalDefense;
        public Text MagicAttack;
        public Text MagicDefense;
        public Text BattleSpeed;
        public Text BattleResponse;
        public Text Potential;
            
        public Text[] equip;
        public GameObject[] back_equip;
        public Text mainMessage;
        
        private MainCharacter targetPlayer;
        private MainCharacter shadow = new MainCharacter();
        public string SelectValue { get; set; }
        private int MAX_LEN = 10;
        int baseNumber = 0;
        // Use this for initialization
        public override void Start()
        {
            base.Start();

            targetPlayer = GroundOne.MC;
            ItemBackPack[] temp = GroundOne.MC.GetBackPackInfo();
            List<ItemBackPack> currentList = new List<ItemBackPack>();
            for (int ii = 0; ii < temp.Length; ii++)
            {
                if (temp[ii] == null)
                    continue;

                if (CheckEquipmentType(targetPlayer, temp[ii], GroundOne.EquipType))
                {
                    currentList.Add(temp[ii]);
                }
            }

            for (int ii = 0; ii < MAX_LEN; ii++)
            {
                if (ii < currentList.Count)
                {
                    if (equip[ii] != null)
                    {
                        equip[ii].text = currentList[ii].Name;
                        Method.UpdateRareColor(currentList[ii], equip[ii], back_equip[ii]);
                    }
                    else
                    {
                        equip[ii].text = "";
                        Method.UpdateRareColor(null, equip[ii], back_equip[ii]);
                    }
                }
                else
                {
                    equip[ii].text = "";
                    Method.UpdateRareColor(null, equip[ii], back_equip[ii]);
                }
            }

            shadow.MainWeapon = this.targetPlayer.MainWeapon;
            shadow.SubWeapon = this.targetPlayer.SubWeapon;
            shadow.MainArmor = this.targetPlayer.MainArmor;
            shadow.Accessory = this.targetPlayer.Accessory;
            shadow.Accessory2 = this.targetPlayer.Accessory2;

            // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
            Type type = this.targetPlayer.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(shadow, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.targetPlayer, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(shadow, (string)(type.GetProperty(pi.Name).GetValue(this.targetPlayer, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(shadow, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.targetPlayer, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                {
                    try
                    {
                        pi.SetValue(shadow, (MainCharacter.PlayerStance)(Enum.Parse(typeof(MainCharacter.PlayerStance), type.GetProperty(pi.Name).GetValue(this.targetPlayer, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(shadow, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.targetPlayer, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(shadow, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.targetPlayer, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            RefreshPartyMembersBattleStatus(targetPlayer);

            RefreshPartyMemberBaseParameter(targetPlayer);
        }

        // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private bool CheckEquipmentType(MainCharacter player, ItemBackPack item, int equipType)
        {
            if (equipType == 0)
            {
                if ((player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    return true;
                }
                else if ((player == GroundOne.SC) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
                            (player == GroundOne.SC) && (item.Type == ItemBackPack.ItemType.Weapon_Rod))
                {
                    return true;
                }
                else if ((player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
                {
                    return true;
                }
            }
            else if (equipType == 1)
            {
                if ((player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
                else if ((player == GroundOne.SC) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
                {
                    return true;
                }
                else if ((player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
            }
            else if (equipType == 2)
            {
                if ((player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
                    (player == GroundOne.MC) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
                {
                    return true;
                }
                else if ((player == GroundOne.SC) && (item.Type == ItemBackPack.ItemType.Armor_Light))
                {
                    return true;
                }
                else if ((player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Armor_Light) ||
                            (player == GroundOne.TC) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
                {
                    return true;
                }
            }
            else if (equipType == 3)
            {
                if (item.Type == ItemBackPack.ItemType.Accessory)
                {
                    if ((player == GroundOne.MC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.MC) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
                    {
                        return true;
                    }
                    if ((player == GroundOne.SC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.SC) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
                    {
                        return true;
                    }
                    if ((player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
                        (player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
                    {
                        return true;
                    }
                }
            }
            else if (equipType == 4)
            {
                if (item.Type == ItemBackPack.ItemType.Accessory)
                {
                    if ((player == GroundOne.MC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.MC) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
                    {
                        return true;
                    }
                    if ((player == GroundOne.SC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.SC) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
                    {
                        return true;
                    }
                    if ((player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
                        (player == GroundOne.TC) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private void RefreshPartyMemberBaseParameter(MainCharacter player)
        {
            this.strength.text = player.Strength.ToString();
            if (shadow.BuffStrength_Accessory > this.targetPlayer.BuffStrength_Accessory) { this.addStrength.text = "<color=blue> + " + player.BuffStrength_Accessory.ToString() + "</color>"; }
            else if (shadow.BuffStrength_Accessory < this.targetPlayer.BuffStrength_Accessory) { this.addStrength.text = "<color=red> + " + player.BuffStrength_Accessory.ToString() + "</color>"; }
            else { this.addStrength.text = "<color=black> + " + player.BuffStrength_Accessory.ToString() + "</color>"; }


            this.agility.text = player.Agility.ToString();
            if (shadow.BuffAgility_Accessory > this.targetPlayer.BuffAgility_Accessory) { this.addAgility.text = "<color=blue> + " + player.BuffAgility_Accessory.ToString() + "</color>"; }
            else if (shadow.BuffAgility_Accessory < this.targetPlayer.BuffAgility_Accessory) { this.addAgility.text = "<color=red> + " + player.BuffAgility_Accessory.ToString() + "</color>"; }
            else { this.addAgility.text = "<color=black> + " + player.BuffAgility_Accessory.ToString() + "</color>"; }
                
            this.intelligence.text = player.Intelligence.ToString();

            if (shadow.BuffIntelligence_Accessory > this.targetPlayer.BuffIntelligence_Accessory) { this.addIntelligence.text = "<color=blue> + " + player.BuffIntelligence_Accessory.ToString() + "</color>"; }
            else if (shadow.BuffIntelligence_Accessory < this.targetPlayer.BuffIntelligence_Accessory) { this.addIntelligence.text = "<color=red> + " + player.BuffIntelligence_Accessory.ToString() + "</color>"; }
            else { this.addIntelligence.text = "<color=black> + " + player.BuffIntelligence_Accessory.ToString() + "</color>"; }

            this.stamina.text = player.Stamina.ToString();
            if (shadow.BuffStamina_Accessory > this.targetPlayer.BuffStamina_Accessory) { this.addStamina.text = "<color=blue> + " + player.BuffStamina_Accessory.ToString() + "</color>"; }
            else if (shadow.BuffStamina_Accessory < this.targetPlayer.BuffStamina_Accessory) { this.addStamina.text = "<color=red> + " + player.BuffStamina_Accessory.ToString() + "</color>"; }
            else { this.addStamina.text = "<color=black> + " + player.BuffStamina_Accessory.ToString() + "</color>"; }

            this.mind.text = player.Mind.ToString();

            if (shadow.BuffMind_Accessory > this.targetPlayer.BuffMind_Accessory) { this.addMind.text = "<color=blue> + " + player.BuffMind_Accessory.ToString() + "</color>"; }
            else if (shadow.BuffMind_Accessory < this.targetPlayer.BuffMind_Accessory) { this.addMind.text = "<color=red> + " + player.BuffMind_Accessory.ToString() + "</color>"; }
            else { this.addMind.text = "<color=black> + " + player.BuffMind_Accessory.ToString() + "</color>"; }
        }

        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            bool MainBlade = false;
            bool SubBlade = false;
            bool DoubleBlade = false;
            double temp1 = 0;
            double temp2 = 0;

            if (shadow.MainWeapon != null)
            {
                if ((shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                {
                    MainBlade = true;
                    if (shadow.SubWeapon != null)
                    {
                        if ((shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                            (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                            (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                        {
                            SubBlade = true;
                            DoubleBlade = true;
                        }
                    }

                }
                if ((shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    MainBlade = true;
                    SubBlade = false;
                }
                if (shadow.MainWeapon.Name == "") // メイン武器が無い場合も含む。
                {
                    MainBlade = true;
                    SubBlade = false;
                }
            }
            else
            {
                if (shadow.SubWeapon != null)
                {
                    if ((shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                        (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        SubBlade = true;
                    }
                }
            }

            // 物理攻撃 (最小) - (最大)
            if (true)
            {
                temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
                temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) { PhysicalAttack.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) { PhysicalAttack.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
                PhysicalAttack.text += " - ";
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) { PhysicalAttack.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) { PhysicalAttack.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }
            }
            else if (MainBlade == false && SubBlade == true)
            {
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
                PhysicalAttack.text += " - ";
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }
            }
            if (DoubleBlade)
            {
                // ２刀流の場合、物理攻撃 (最小) - (最大)
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                PhysicalAttack.text += "\r\n";
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=blue>" + temp1.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=red>" + temp1.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text += "<color=black>" + temp1.ToString("F2") + "</color>"; }
                PhysicalAttack.text += " - ";
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) { PhysicalAttack.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
                else { PhysicalAttack.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }
            }

            // 物理防御 (最小) - (最大)
            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) { PhysicalDefense.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) { PhysicalDefense.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { PhysicalDefense.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
            PhysicalDefense.text += " - ";
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) { PhysicalDefense.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) { PhysicalDefense.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
            else { PhysicalDefense.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }

            // 魔法攻撃 (最小) - (最大)
            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) { MagicAttack.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) { MagicAttack.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { MagicAttack.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
            MagicAttack.text += " - ";
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) { MagicAttack.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) { MagicAttack.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
            else { MagicAttack.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }

            // 魔法防御 (最小) - (最大)
            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) { MagicDefense.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) { MagicDefense.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { MagicDefense.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
            MagicDefense.text += " - ";
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) { MagicDefense.text += "<color=blue>" + temp2.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) { MagicDefense.text += "<color=red>" + temp2.ToString("F2") + "</color>"; }
            else { MagicDefense.text += "<color=black>" + temp2.ToString("F2") + "</color>"; }

            // 戦闘速度
            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            if (PrimaryLogic.BattleSpeedValue(shadow, false) > PrimaryLogic.BattleSpeedValue(this.targetPlayer, false)) { BattleSpeed.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.BattleSpeedValue(shadow, false) < PrimaryLogic.BattleSpeedValue(this.targetPlayer, false)) { BattleSpeed.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { BattleSpeed.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }

            // 戦闘反応
            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            if (PrimaryLogic.BattleResponseValue(shadow, false) > PrimaryLogic.BattleResponseValue(this.targetPlayer, false)) { BattleResponse.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.BattleResponseValue(shadow, false) < PrimaryLogic.BattleResponseValue(this.targetPlayer, false)) { BattleResponse.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { BattleResponse.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }

            // 潜在能力
            temp1 = PrimaryLogic.PotentialValue(player, false);
            if (PrimaryLogic.PotentialValue(shadow, false) > PrimaryLogic.PotentialValue(this.targetPlayer, false)) { Potential.text = "<color=blue>" + temp1.ToString("F2") + "</color>"; }
            else if (PrimaryLogic.PotentialValue(shadow, false) < PrimaryLogic.PotentialValue(this.targetPlayer, false)) { Potential.text = "<color=red>" + temp1.ToString("F2") + "</color>"; }
            else { Potential.text = "<color=black>" + temp1.ToString("F2") + "</color>"; }
        }

        private void ViewBackPack(int number)
        {
            ItemBackPack[] temp = GroundOne.MC.GetBackPackInfo();
            List<ItemBackPack> currentList = new List<ItemBackPack>();
            for (int ii = 0; ii < temp.Length; ii++)
            {
                if (temp[ii] == null)
                    continue;

                if (CheckEquipmentType(targetPlayer, temp[ii], GroundOne.EquipType))
                {
                    currentList.Add(temp[ii]);
                }
            }
            for (int ii = 0; ii < MAX_LEN; ii++)
            {
                if ((ii + number * MAX_LEN) < currentList.Count)
                {
                    if (currentList[ii + number * MAX_LEN] != null)
                    {
                        equip[ii].text = currentList[ii + number * MAX_LEN].Name;
                        Method.UpdateRareColor(currentList[ii + number * MAX_LEN], equip[ii], back_equip[ii]);
                    }
                    else
                    {
                        equip[ii].text = "";
                        Method.UpdateRareColor(null, equip[ii], back_equip[ii]);
                    }
                }
                else
                {
                    equip[ii].text = "";
                    Method.UpdateRareColor(null, equip[ii], back_equip[ii]);
                }
            }
        }

        public void tapNumber(Text sender)
        {
            ViewBackPack(Convert.ToInt32(sender.text) - 1);
        }

        public void tapEquip(Text sender)
        {
            if ((sender.text != string.Empty) ||
                (sender.text != ""))
            {
                this.SelectValue = sender.text;
            }
        }
        public void tapOK()
        {
            ItemBackPack exchangeItem = new ItemBackPack(SelectValue);
            ItemBackPack tempItem = null;
            if (GroundOne.EquipType == 0)
            {
                tempItem = targetPlayer.MainWeapon;
                targetPlayer.MainWeapon = exchangeItem;
                if ((exchangeItem.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                    (exchangeItem.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    if (targetPlayer.SubWeapon != null)
                    {
                        if (targetPlayer.SubWeapon.Name != "")
                        {
                            targetPlayer.AddBackPack(targetPlayer.SubWeapon);
                        }
                        targetPlayer.SubWeapon = null;
                    }
                }
            }
            else if (GroundOne.EquipType == 1)
            {
                tempItem = targetPlayer.SubWeapon;
                targetPlayer.SubWeapon = exchangeItem;
                if (targetPlayer.MainWeapon != null)
                {
                    if (targetPlayer.MainWeapon.Name != "")
                    {
                        if ((targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                            (targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                        {
                            targetPlayer.AddBackPack(targetPlayer.MainWeapon);
                            targetPlayer.MainWeapon = null;
                        }
                    }
                }
            }
            else if (GroundOne.EquipType == 2)
            {
                tempItem = targetPlayer.MainArmor;
                targetPlayer.MainArmor = exchangeItem;
            }
            else if (GroundOne.EquipType == 3)
            {
                tempItem = targetPlayer.Accessory;
                targetPlayer.Accessory = exchangeItem;
            }
            else if (GroundOne.EquipType == 4)
            {
                tempItem = targetPlayer.Accessory2;
                targetPlayer.Accessory2 = exchangeItem;
            }
            if (exchangeItem != null)
            {
                if (exchangeItem.Name != "")
                {
                    targetPlayer.DeleteBackPack(exchangeItem);
                }
            }
            if (tempItem != null)
            {
                if (tempItem.Name != "" && tempItem.Name != String.Empty)
                {
                    targetPlayer.AddBackPack(tempItem);
                }
            }
            SceneDimension.Back(this);
        }
        public void tapCancel()
        {
            SceneDimension.Back(this);
        }
        public void tapDropEquip()
        {
            ItemBackPack[] tempBackPack = this.targetPlayer.GetBackPackInfo();
            int count = 0;
            if (tempBackPack != null)
            {
                for (int ii = 0; ii < tempBackPack.Length; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        if ((tempBackPack[ii].Name != String.Empty) && (tempBackPack[ii].Name != ""))
                        {
                            count++;
                        }
                    }
                }
                if (count >= Database.MAX_BACKPACK_SIZE)
                {
                    mainMessage.text = this.targetPlayer.GetCharacterSentence(2029);
                    return;
                }
            }
            if (GroundOne.EquipType == 0)
            {
                GroundOne.MC.AddBackPack(GroundOne.MC.MainWeapon);
                GroundOne.MC.MainWeapon = null;
            }
            else if (GroundOne.EquipType == 1)
            {
                GroundOne.MC.AddBackPack(GroundOne.MC.SubWeapon);
                GroundOne.MC.SubWeapon = null;
            }
            else if (GroundOne.EquipType == 2)
            {
                GroundOne.MC.AddBackPack(GroundOne.MC.MainArmor);
                GroundOne.MC.MainArmor = null;
            }
            else if (GroundOne.EquipType == 3)
            {
                GroundOne.MC.AddBackPack(GroundOne.MC.Accessory);
                GroundOne.MC.Accessory = null;
            }
            else if (GroundOne.EquipType == 4)
            {
                GroundOne.MC.AddBackPack(GroundOne.MC.Accessory2);
                GroundOne.MC.Accessory2 = null;
            }

            SceneDimension.Back(this);
        }


        public void target1_MouseEnter(Text sender)
        {
            string target = sender.text;
            if (target == null)
            {
                return;
            }
            if ((target == string.Empty) ||
                (target == ""))
            {
                return;
            }

            ItemBackPack temp = new ItemBackPack(sender.text);
            mainMessage.text = temp.Description;

            if (GroundOne.EquipType == 0)
            {
                shadow.MainWeapon = temp;
            }
            else if (GroundOne.EquipType == 1)
            {
                shadow.SubWeapon = temp;
            }
            else if (GroundOne.EquipType == 2)
            {
                shadow.MainArmor = temp;
            }
            else if (GroundOne.EquipType == 3)
            {
                shadow.Accessory = temp;
            }
            else if (GroundOne.EquipType == 4)
            {
                shadow.Accessory2 = temp;
            }

            RefreshPartyMemberBaseParameter(shadow);
            RefreshPartyMembersBattleStatus(shadow);
        }

        public void target1_MouseLeave()
        {
            if (GroundOne.EquipType == 0)
            {
                shadow.MainWeapon = this.targetPlayer.MainWeapon;
            }
            else if (GroundOne.EquipType == 1)
            {
                shadow.SubWeapon = this.targetPlayer.SubWeapon;
            }
            else if (GroundOne.EquipType == 2)
            {
                shadow.MainArmor = this.targetPlayer.MainArmor;
            }
            else if (GroundOne.EquipType == 3)
            {
                shadow.Accessory = this.targetPlayer.Accessory;
            }
            else if (GroundOne.EquipType == 4)
            {
                shadow.Accessory2 = this.targetPlayer.Accessory2;
            }

            mainMessage.text = "";
            RefreshPartyMemberBaseParameter(this.targetPlayer);
            RefreshPartyMembersBattleStatus(this.targetPlayer);
        }

    }
}

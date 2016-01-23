using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DungeonPlayer
{
    public class TruthSelectEquipment : MonoBehaviour
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
        
        public int EquipType { get; set; } // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private MainCharacter targetPlayer;
        private MainCharacter shadow = new MainCharacter();
        public string SelectValue { get; set; }
        private int MAX_LEN = 10;
        int baseNumber = 0;
        // Use this for initialization
        void Start()
        {
            GroundOne.InitializeGroundOne();
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
            this.addStrength.text = " + " + player.BuffStrength_Accessory.ToString();

            this.agility.text = player.Agility.ToString();
            this.addAgility.text = " + " + player.BuffAgility_Accessory.ToString();

            this.intelligence.text = player.Intelligence.ToString();
            this.addIntelligence.text = " + " + player.BuffIntelligence_Accessory.ToString();

            this.stamina.text = player.Stamina.ToString();
            this.addStamina.text = " + " + player.BuffStamina_Accessory.ToString();

            this.mind.text = player.Mind.ToString();
            this.addMind.text = " + " + player.BuffMind_Accessory.ToString();

            Color downColor = Color.red;
            Color upColor = Color.blue;
            Color normalColor = Color.black;
            if (shadow.BuffStrength_Accessory > this.targetPlayer.BuffStrength_Accessory) this.addStrength.color = upColor;
            else if (shadow.BuffStrength_Accessory < this.targetPlayer.BuffStrength_Accessory) this.addStrength.color = downColor;
            else this.addStrength.color = normalColor;

            if (shadow.BuffAgility_Accessory > this.targetPlayer.BuffAgility_Accessory) this.addAgility.color = upColor;
            else if (shadow.BuffAgility_Accessory < this.targetPlayer.BuffAgility_Accessory) this.addAgility.color = downColor;
            else this.addAgility.color = normalColor;

            if (shadow.BuffIntelligence_Accessory > this.targetPlayer.BuffIntelligence_Accessory) this.addIntelligence.color = upColor;
            else if (shadow.BuffIntelligence_Accessory < this.targetPlayer.BuffIntelligence_Accessory) this.addIntelligence.color = downColor;
            else this.addIntelligence.color = normalColor;

            if (shadow.BuffStamina_Accessory > this.targetPlayer.BuffStamina_Accessory) this.addStamina.color = upColor;
            else if (shadow.BuffStamina_Accessory < this.targetPlayer.BuffStamina_Accessory) this.addStamina.color = downColor;
            else this.addStamina.color = normalColor;

            if (shadow.BuffMind_Accessory > this.targetPlayer.BuffMind_Accessory) this.addMind.color = upColor;
            else if (shadow.BuffMind_Accessory < this.targetPlayer.BuffMind_Accessory) this.addMind.color = downColor;
            else this.addMind.color = normalColor;
        }

        //Point basePhysicalLocation;
        //Point basePhysicalLocationBar;
        //Point basePhysicalLocationMax;
        //Point basePhysicalLocation2;
        //Point basePhysicalLocation2Bar;
        //Point basePhysicalLocation2Max;
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

            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            PhysicalAttack.text = temp1.ToString("F2");
            PhysicalAttack.text += " - " + temp2.ToString("F2");

            if (MainBlade == false && SubBlade == true)
            {
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                PhysicalAttack.text = temp1.ToString("F2");
                PhysicalAttack.text += " - " + temp2.ToString("F2");
            }

            if (DoubleBlade)
            {
                // ２刀流の場合
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, MainCharacter.PlayerStance.None, false);
                PhysicalAttack.text += "\r\n" + temp1.ToString("F2");
                PhysicalAttack.text += " - " + temp2.ToString("F2");
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            PhysicalDefense.text = temp1.ToString("F2");
            PhysicalDefense.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            MagicAttack.text = temp1.ToString("F2");
            MagicAttack.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            MagicDefense.text = temp1.ToString("F2");
            MagicDefense.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            BattleSpeed.text = temp1.ToString("F2");

            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            BattleResponse.text = temp1.ToString("F2");

            temp1 = PrimaryLogic.PotentialValue(player, false);
            Potential.text = temp1.ToString("F2");

            Color downColor = Color.red;
            Color upColor = Color.blue;
            Color normalColor = Color.black;

            if (MainBlade)
            {
                //MessageBox.Show("mainblade only");
                // 物理攻撃（最小）
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
                // todo unityでMaxとMinの区別をなくしたが本当にいいか？
                // 物理攻撃（最大）
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
            }

            if (MainBlade == false && SubBlade == true)
            {
                // 物理攻撃（最小）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
                // todo unityでMaxとMinの区別をなくしたが本当にいいか？
                // 物理攻撃（最大）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
            }

            if (DoubleBlade)
            {
                // todo unityで片手両手の区別をなくしたが本当にいいか？
                // 物理攻撃２（最小）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
                // todo unityでMaxとMinの区別をなくしたが本当にいいか？
                // 物理攻撃２（最大）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false)) this.PhysicalAttack.color = downColor;
                else this.PhysicalAttack.color = normalColor;
            }

            // 魔法攻撃（最小）
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.color = upColor;
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.color = downColor;
            else this.MagicAttack.color = normalColor;
            // todo unityでMaxとMinの区別をなくしたが本当にいいか？
            // 魔法攻撃（最大）
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.color = upColor;
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.targetPlayer, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.color = downColor;
            else this.MagicAttack.color = normalColor;

            // 物理防御（最小）
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) this.PhysicalDefense.color = upColor;
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) this.PhysicalDefense.color = downColor;
            else this.PhysicalDefense.color = normalColor;
            // todo unityでMaxとMinの区別をなくしたが本当にいいか？
            // 物理防御（最大）
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) this.PhysicalDefense.color = upColor;
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.PhysicalDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) this.PhysicalDefense.color = downColor;
            else this.PhysicalDefense.color = normalColor;

            // 魔法防御（最小）
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) this.MagicDefense.color = upColor;
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Min, false)) this.MagicDefense.color = downColor;
            else this.MagicDefense.color = normalColor;
            // todo unityでMaxとMinの区別をなくしたが本当にいいか？
            // 魔法防御（最大）
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) this.MagicDefense.color = upColor;
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.MagicDefenseValue(this.targetPlayer, PrimaryLogic.NeedType.Max, false)) this.MagicDefense.color = downColor;
            else this.MagicDefense.color = normalColor;
            // 戦闘速度
            if (PrimaryLogic.BattleSpeedValue(shadow, false) > PrimaryLogic.BattleSpeedValue(this.targetPlayer, false)) this.BattleSpeed.color = upColor;
            else if (PrimaryLogic.BattleSpeedValue(shadow, false) < PrimaryLogic.BattleSpeedValue(this.targetPlayer, false)) this.BattleSpeed.color = downColor;
            else this.BattleSpeed.color = normalColor;
            // 戦闘反応
            if (PrimaryLogic.BattleResponseValue(shadow, false) > PrimaryLogic.BattleResponseValue(this.targetPlayer, false)) this.BattleResponse.color = upColor;
            else if (PrimaryLogic.BattleResponseValue(shadow, false) < PrimaryLogic.BattleResponseValue(this.targetPlayer, false)) this.BattleResponse.color = downColor;
            else this.BattleResponse.color = normalColor;
            // 潜在能力
            if (PrimaryLogic.PotentialValue(shadow, false) > PrimaryLogic.PotentialValue(this.targetPlayer, false)) this.Potential.color = upColor;
            else if (PrimaryLogic.PotentialValue(shadow, false) < PrimaryLogic.PotentialValue(this.targetPlayer, false)) this.Potential.color = downColor;
            else this.Potential.color = normalColor;
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
                //this.DialogResult = System.Windows.Forms.DialogResult.OK; // todo
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
            SceneDimension.Back();
        }
        public void tapCancel()
        {
            SceneDimension.Back();
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

            SceneDimension.Back();
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

            if (this.EquipType == 0)
            {
                shadow.MainWeapon = temp;
            }
            else if (this.EquipType == 1)
            {
                shadow.SubWeapon = temp;
            }
            else if (this.EquipType == 2)
            {
                shadow.MainArmor = temp;
            }
            else if (this.EquipType == 3)
            {
                shadow.Accessory = temp;
            }
            else if (this.EquipType == 4)
            {
                shadow.Accessory2 = temp;
            }

            RefreshPartyMemberBaseParameter(shadow);
            RefreshPartyMembersBattleStatus(shadow);
        }

        public void target1_MouseLeave()
        {
            if (this.EquipType == 0)
            {
                shadow.MainWeapon = this.targetPlayer.MainWeapon;
            }
            else if (this.EquipType == 1)
            {
                shadow.SubWeapon = this.targetPlayer.SubWeapon;
            }
            else if (this.EquipType == 2)
            {
                shadow.MainArmor = this.targetPlayer.MainArmor;
            }
            else if (this.EquipType == 3)
            {
                shadow.Accessory = this.targetPlayer.Accessory;
            }
            else if (this.EquipType == 4)
            {
                shadow.Accessory2 = this.targetPlayer.Accessory2;
            }

            mainMessage.text = "";
            RefreshPartyMemberBaseParameter(this.targetPlayer);
            RefreshPartyMembersBattleStatus(this.targetPlayer);
        }

    }
}

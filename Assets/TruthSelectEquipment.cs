using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public class TruthSelectEquipment : MonoBehaviour
    {
        public Text[] equip;
        public GameObject[] back_equip;
        private MainCharacter targetPlayer;
        public string SelectValue { get; set; }
        public Text mainMessage;
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
                if (ii < currentList.Count )
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
    }
}

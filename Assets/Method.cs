﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class Method
    {
        public enum NewItemCategory
        {
            Battle,
            Lottery,
        }

        public static string ConvertToEnglishItem(string itemName)
        {
            if (GroundOne.Language == GroundOne.GameLanguage.English)
            {
                return ItemBackPack.ConvertToEnglish(itemName);
            }
            else
            {
                return itemName;
            }
        }

        public static string ConvertToOriginItem(string sender)
        {
            if (GroundOne.Language == GroundOne.GameLanguage.English)
            {
                return ItemBackPack.ConvertToOrigin(sender);
            }
            else
            {
                return sender;
            }
        }

        // キャラクターシャドウデータをキャラクター本体データに戻す。
        public static void CopyShadowToMain()
        {
            GroundOne.MC.MainWeapon = GroundOne.ShadowMC.MainWeapon;
            GroundOne.MC.SubWeapon = GroundOne.ShadowMC.SubWeapon;
            GroundOne.MC.MainArmor = GroundOne.ShadowMC.MainArmor;
            GroundOne.MC.Accessory = GroundOne.ShadowMC.Accessory;
            GroundOne.MC.Accessory2 = GroundOne.ShadowMC.Accessory2;

            GroundOne.SC.MainWeapon = GroundOne.ShadowSC.MainWeapon;
            GroundOne.SC.SubWeapon = GroundOne.ShadowSC.SubWeapon;
            GroundOne.SC.MainArmor = GroundOne.ShadowSC.MainArmor;
            GroundOne.SC.Accessory = GroundOne.ShadowSC.Accessory;
            GroundOne.SC.Accessory2 = GroundOne.ShadowSC.Accessory2;

            GroundOne.TC.MainWeapon = GroundOne.ShadowTC.MainWeapon;
            GroundOne.TC.SubWeapon = GroundOne.ShadowTC.SubWeapon;
            GroundOne.TC.MainArmor = GroundOne.ShadowTC.MainArmor;
            GroundOne.TC.Accessory = GroundOne.ShadowTC.Accessory;
            GroundOne.TC.Accessory2 = GroundOne.ShadowTC.Accessory2;

            if (GroundOne.WE.AvailableFirstCharacter)
            {
                GroundOne.MC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowMC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.MC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            if (GroundOne.WE.AvailableSecondCharacter)
            {
                GroundOne.SC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowSC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.SC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            if (GroundOne.WE.AvailableThirdCharacter)
            {
                GroundOne.TC.DeleteBackPackAll();
                ItemBackPack[] tempBackPack = GroundOne.ShadowTC.GetBackPackInfo();
                for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        int stack = tempBackPack[ii].StackValue;
                        for (int jj = 0; jj < stack; jj++)
                        {
                            GroundOne.TC.AddBackPack(tempBackPack[ii]);
                        }
                    }
                }
            }

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null)), null);
                        pi.SetValue(GroundOne.SC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null)), null);
                        pi.SetValue(GroundOne.TC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null).ToString())), null);
                        pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null).ToString())), null);
                        pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowMC, null).ToString())), null);
                        pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowSC, null).ToString())), null);
                        pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.ShadowTC, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            Type type2 = GroundOne.WE.GetType();
            foreach (PropertyInfo pi in type2.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (string)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(GroundOne.shadowWE, null)), null);
                    }
                    catch { }
                }
            }

            Type type3 = GroundOne.WE2.GetType();
            foreach (PropertyInfo pi in type3.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.shadowWE2, null)), null);
                    }
                    catch { }
                }
            }
        }

        // キャラクター本体データからキャラクターシャドウデータを生成する。
        public static void CreateShadowData()
        {
            GameObject shadowObjMC = new GameObject();
            GroundOne.ShadowMC = shadowObjMC.AddComponent<MainCharacter>();

            GameObject shadowObjSC = new GameObject();
            GroundOne.ShadowSC = shadowObjSC.AddComponent<MainCharacter>();

            GameObject shadowObjTC = new GameObject();
            GroundOne.ShadowTC = shadowObjTC.AddComponent<MainCharacter>();

            GroundOne.ShadowMC.MainWeapon = GroundOne.MC.MainWeapon;
            GroundOne.ShadowMC.SubWeapon = GroundOne.MC.SubWeapon;
            GroundOne.ShadowMC.MainArmor = GroundOne.MC.MainArmor;
            GroundOne.ShadowMC.Accessory = GroundOne.MC.Accessory;
            GroundOne.ShadowMC.Accessory2 = GroundOne.MC.Accessory2;
            GroundOne.ShadowMC.ReplaceBackPack(GroundOne.MC.GetBackPackInfo());

            GroundOne.ShadowSC.MainWeapon = GroundOne.SC.MainWeapon;
            GroundOne.ShadowSC.SubWeapon = GroundOne.SC.SubWeapon;
            GroundOne.ShadowSC.MainArmor = GroundOne.SC.MainArmor;
            GroundOne.ShadowSC.Accessory = GroundOne.SC.Accessory;
            GroundOne.ShadowSC.Accessory2 = GroundOne.SC.Accessory2;
            GroundOne.ShadowSC.ReplaceBackPack(GroundOne.SC.GetBackPackInfo());

            GroundOne.ShadowTC.MainWeapon = GroundOne.TC.MainWeapon;
            GroundOne.ShadowTC.SubWeapon = GroundOne.TC.SubWeapon;
            GroundOne.ShadowTC.MainArmor = GroundOne.TC.MainArmor;
            GroundOne.ShadowTC.Accessory = GroundOne.TC.Accessory;
            GroundOne.ShadowTC.Accessory2 = GroundOne.TC.Accessory2;
            GroundOne.ShadowTC.ReplaceBackPack(GroundOne.TC.GetBackPackInfo());

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (System.Int32)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (string)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.MC, null)), null);
                        pi.SetValue(GroundOne.ShadowSC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.SC, null)), null);
                        pi.SetValue(GroundOne.ShadowTC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(GroundOne.TC, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.MC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowSC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.SC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowTC, (MainCharacter.AdditionalSpellType)(Enum.Parse(typeof(MainCharacter.AdditionalSpellType), type.GetProperty(pi.Name).GetValue(GroundOne.TC, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(GroundOne.ShadowMC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.MC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowSC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.SC, null).ToString())), null);
                        pi.SetValue(GroundOne.ShadowTC, (MainCharacter.AdditionalSkillType)(Enum.Parse(typeof(MainCharacter.AdditionalSkillType), type.GetProperty(pi.Name).GetValue(GroundOne.TC, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            Type type2 = GroundOne.WE.GetType();
            foreach (PropertyInfo pi in type2.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (string)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(GroundOne.WE, null)), null);
                    }
                    catch { }
                }
            }

            Type type3 = GroundOne.WE2.GetType();
            foreach (PropertyInfo pi in type3.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.shadowWE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                    }
                    catch { }
                }
            }
        }
        
        public static int GetMaxLevel()
        {
            if (GroundOne.WE.TruthCompleteArea1 == false) { return Database.CHARACTER_MAX_LEVEL1; }
            if (GroundOne.WE.TruthCompleteArea2 == false) { return Database.CHARACTER_MAX_LEVEL2; }
            if (GroundOne.WE.TruthCompleteArea3 == false) { return Database.CHARACTER_MAX_LEVEL3; }
            if (GroundOne.WE.TruthCompleteArea4 == false) { return Database.CHARACTER_MAX_LEVEL4; }
            if (GroundOne.WE.TruthCompleteArea5 == false) { return Database.CHARACTER_MAX_LEVEL5; }

            return Database.CHARACTER_MAX_LEVEL6;
        }

        public static string DuelistConvertToEnglish(string duelist)
        {
            if (duelist == Database.DUEL_EONE_FULNEA)
            {
                return Database.DUEL_EONE_FULNEA_DB;
            }
            else if (duelist == Database.DUEL_MAGI_ZELKIS)
            {
                return Database.DUEL_MAGI_ZELKIS_DB;
            }
            else if (duelist == Database.DUEL_SELMOI_RO)
            {
                return Database.DUEL_SELMOI_RO_DB;
            }
            else if (duelist == Database.DUEL_KARTIN_MAI)
            {
                return Database.DUEL_KARTIN_MAI_DB;
            }
            else if (duelist == Database.DUEL_JEDA_ARUS)
            {
                return Database.DUEL_JEDA_ARUS_DB;
            }
            else if (duelist == Database.DUEL_SINIKIA_VEILHANZ)
            {
                return Database.DUEL_SINIKIA_VEILHANZ_DB;
            }
            else if (duelist == Database.DUEL_ADEL_BRIGANDY)
            {
                return Database.DUEL_ADEL_BRIGANDY_DB;
            }
            else if (duelist == Database.DUEL_LENE_COLTOS)
            {
                return Database.DUEL_LENE_COLTOS_DB;                    
            }
            else if (duelist == Database.DUEL_SCOTY_ZALGE)
            {
                return Database.DUEL_SCOTY_ZALGE_DB;
            }
            else if (duelist == Database.DUEL_PERMA_WARAMY)
            {
                return Database.DUEL_PERMA_WARAMY_DB;
            }
            else if (duelist == Database.DUEL_KILT_JORJU)
            {
                return Database.DUEL_KILT_JORJU_DB;
            }
            else if (duelist == Database.DUEL_BILLY_RAKI)
            {
                return Database.DUEL_BILLY_RAKI_DB;
            }
            else if (duelist == Database.DUEL_ANNA_HAMILTON)
            {
                return Database.DUEL_ANNA_HAMILTON_DB;
            }
            else if (duelist == Database.DUEL_CALMANS_OHN)
            {
                return Database.DUEL_CALMANS_OHN_DB;
            }
            else if (duelist == Database.DUEL_SUN_YU)
            {
                return Database.DUEL_SUN_YU_DB;
            }
            else if (duelist == Database.DUEL_SHUVALTZ_FLORE)
            {
                return Database.DUEL_SHUVALTZ_FLORE_DB;
            }
            else if (duelist == Database.DUEL_RVEL_ZELKIS)
            {
                return Database.DUEL_RVEL_ZELKIS_DB;
            }
            else if (duelist == Database.DUEL_VAN_HEHGUSTEL)
            {
                return Database.DUEL_VAN_HEHGUSTEL_DB;
            }
            else if (duelist == Database.DUEL_OHRYU_GENMA)
            {
                return Database.DUEL_OHRYU_GENMA_DB;
            }
            else if (duelist == Database.DUEL_LADA_MYSTORUS)
            {
                return Database.DUEL_LADA_MYSTORUS_DB;
            }
            else if (duelist == Database.DUEL_SIN_OSCURETE)
            {
                return Database.DUEL_SIN_OSCURETE_DB;
            }
            else
            {
                return duelist; // 何も判定できなかった場合は、そのまま値を返す
            }
        }

        public static void UseItem(List<MainCharacter> allyList, MainCharacter player, string itemName, int currentNumber, Text mainMessage)
        {
            ItemBackPack backpackData = new ItemBackPack(itemName);

            switch (backpackData.Name)
            {
                case Database.POOR_SMALL_RED_POTION:
                case Database.COMMON_NORMAL_RED_POTION:
                case Database.COMMON_LARGE_RED_POTION:
                case Database.COMMON_HUGE_RED_POTION:
                case Database.COMMON_GORGEOUS_RED_POTION:
                case Database.RARE_PERFECT_RED_POTION:
                case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                    int effect = backpackData.UseIt();
                    if (player.CurrentNourishSense > 0)
                    {
                        effect = (int)((double)effect * PrimaryLogic.NourishSenseValue(player));
                    }
                    player.CurrentLife += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    break;

                case Database.POOR_SMALL_BLUE_POTION:
                case Database.COMMON_NORMAL_BLUE_POTION:
                case Database.COMMON_LARGE_BLUE_POTION:
                case Database.COMMON_HUGE_BLUE_POTION:
                case Database.COMMON_GORGEOUS_BLUE_POTION:
                case Database.RARE_PERFECT_BLUE_POTION:
                    effect = backpackData.UseIt();
                    player.CurrentMana += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    break;

                case Database.POOR_SMALL_GREEN_POTION:
                case Database.COMMON_NORMAL_GREEN_POTION:
                case Database.COMMON_LARGE_GREEN_POTION:
                case Database.COMMON_HUGE_GREEN_POTION:
                case Database.COMMON_GORGEOUS_GREEN_POTION:
                case Database.RARE_PERFECT_GREEN_POTION:
                    effect = backpackData.UseIt();
                    player.CurrentSkillPoint += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    break;

                case Database.COMMON_REVIVE_POTION_MINI:
                    for (int ii = 0; ii < allyList.Count; ii++)
                    {
                        if (allyList[ii].Dead)
                        {
                            player.DeleteBackPack(backpackData, 1, currentNumber);
                            allyList[ii].ResurrectPlayer(1);
                            mainMessage.text = allyList[ii].GetCharacterSentence(2016);
                            break;
                        }                            
                    }
                    break;

                case Database.COMMON_POTION_RESIST_FIRE:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentResistFireUp = Database.INFINITY;
                    player.CurrentResistFireUpValue = 50;
                    player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", Database.INFINITY);
                    break;

                case Database.COMMON_POTION_MAGIC_SEAL:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyMagicAttack = 1.05f;
                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                    break;

                case Database.COMMON_POTION_ATTACK_SEAL:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyPhysicalAttack = 1.05f;
                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                    break;

                case Database.POOR_POTION_CURE_POISON:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentPoison = 0;
                    player.CurrentPoisonValue = 0;
                    player.DeBuff(player.pbPoison);
                    break;

                case Database.COMMON_POTION_NATURALIZE:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentPoison = 0;
                    player.CurrentPoisonValue = 0;
                    player.DeBuff(player.pbPoison);
                    player.CurrentSlow = 0;
                    player.DeBuff(player.pbSlow);
                    break;

                case Database.COMMON_POTION_CURE_BLIND:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentBlind = 0;
                    player.DeBuff(player.pbBlind);
                    break;

                case Database.RARE_POTION_MOSSGREEN_DREAM:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveSlow();
                    player.RemovePoison();
                    player.RemoveBlind();
                    break;

                case Database.RARE_DRYAD_SAGE_POTION:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyBattleSpeed = 1.05f;
                    player.AmplifyBattleResponse = 1.05f;
                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                    player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + "BuffReactionUp", Database.INFINITY);
                    break;

                case Database.COMMON_RESIST_POISON:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentPoison = 0;
                    player.CurrentPoisonValue = 0;
                    player.DeBuff(player.pbPoison);
                    player.ResistPoison = true;
                    player.ActivateBuff(player.pbResistPoison, Database.BaseResourceFolder + "ResistPoison", Database.INFINITY);
                    break;

                case Database.COMMON_POTION_OVER_GROWTH:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentStaminaUp = Database.INFINITY;
                    player.CurrentStaminaUpValue = 100; // スタミナUPは内部処理で10倍されてるため、ここでは1000/10で100
                    player.ActivateBuff(player.pbStaminaUp, Database.BaseResourceFolder + "BuffStaminaUp", Database.INFINITY);
                    break;

                case Database.COMMON_POTION_RAINBOW_IMPACT:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemovePhysicalAttackDown();
                    player.RemoveMagicAttackDown();
                    break;

                case Database.COMMON_POTION_BLACK_GAST:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyMagicAttack = 1.07f;
                    player.AmplifyPhysicalAttack = 1.07f;
                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                    break;

                case Database.COMMON_SOUKAI_DRINK_SS:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyMagicAttack = 1.07f;
                    player.AmplifyBattleSpeed = 1.07f;
                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                    break;

                case Database.COMMON_TUUKAI_DRINK_DD:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.AmplifyPhysicalAttack = 1.07f;
                    player.AmplifyBattleSpeed = 1.07f;
                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                    break;

                case Database.COMMON_FAIRY_BREATH:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveSilence();
                    player.ResistSilence = true;
                    player.ActivateBuff(player.pbResistSilence, Database.BaseResourceFolder + "ResistSilence", Database.INFINITY);
                    break;

                case Database.COMMON_HEART_ACCELERATION:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveParalyze();
                    player.ResistParalyze = true;
                    player.ActivateBuff(player.pbResistParalyze, Database.BaseResourceFolder + "ResistParalyze", Database.INFINITY);
                    break;

                case Database.RARE_SAGE_POTION_MINI:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveDebuffEffect();
                    player.RemoveDebuffParam();
                    player.RemoveDebuffSpell();
                    player.RemoveDebuffSkill();
                    player.CurrentSagePotionMini = Database.INFINITY;
                    player.CurrentNoResurrection = Database.INFINITY;
                    player.ActivateBuff(player.pbNoResurrection, Database.BaseResourceFolder + "NoResurrection", Database.INFINITY);
                    break;

                case Database.RARE_POWER_SURGE:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.BuffUpStrength(600);
                    player.BuffUpStamina(400);
                    player.BuffUpAmplifyPhysicalAttack(1.20f);
                    break;

                case Database.RARE_ZEPHER_BREATH:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.BuffUpAgility(600);
                    player.BuffUpIntelligence(400);
                    player.BuffUpAmplifyBattleSpeed(1.20f);
                    break;

                case Database.RARE_GENSEI_MAGIC_BOTTLE:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.BuffUpIntelligence(600);
                    player.BuffUpMind(400);
                    player.BuffUpAmplifyMagicAttack(1.20f);
                    break;

                case Database.RARE_ZETTAI_STAMINAUP:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.BuffUpStrength(200);
                    player.BuffUpIntelligence(200);
                    player.BuffUpStamina(600);
                    player.BuffUpAmplifyPhysicalDefence(1.10f);
                    player.BuffUpAmplifyMagicDefense(1.10f);
                    break;

                case Database.RARE_MIND_ILLUSION:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.BuffUpStrength(100);
                    player.BuffUpAgility(100);
                    player.BuffUpIntelligence(100);
                    player.BuffUpStamina(100);
                    player.BuffUpMind(600);
                    player.BuffUpAmplifyPotential(1.20f);
                    break;

                case Database.RARE_GENSEI_TAIMA_KUSURI:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentGenseiTaima = Database.INFINITY;
                    player.ActivateBuff(player.pbGenseiTaima, Database.BaseResourceFolder + Database.ITEMCOMMAND_GENSEI_TAIMA, Database.INFINITY);
                    break;

                case Database.RARE_SHINING_AETHER:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentShiningAether = 2; // 次のターンまで有効
                    player.ActivateBuff(player.pbShiningAether, Database.BaseResourceFolder + Database.ITEMCOMMAND_SHINING_AETHER, 2);
                    break;

                case Database.RARE_BLACK_ELIXIR:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.CurrentBlackElixir = Database.INFINITY;
                    player.CurrentBlackElixirValue = player.MaxLife / 2;
                    player.CurrentLife += player.CurrentBlackElixirValue;
                    player.ActivateBuff(player.pbBlackElixir, Database.BaseResourceFolder + Database.ITEMCOMMAND_BLACK_ELIXIR, Database.INFINITY);
                    break;

                case Database.RARE_ELEMENTAL_SEAL:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveDebuffEffect();
                    player.CurrentElementalSeal = Database.INFINITY;
                    player.ActivateBuff(player.pbElementalSeal, Database.BaseResourceFolder + Database.ITEMCOMMAND_ELEMENTAL_SEAL, Database.INFINITY);
                    break;

                case Database.RARE_COLORLESS_ANTIDOTE:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    player.RemoveDebuffParam();
                    player.CurrentColorlessAntidote = Database.INFINITY;
                    player.ActivateBuff(player.pbColorlessAntidote, Database.BaseResourceFolder + Database.ITEMCOMMAND_COLORLESS_ANTIDOTE, Database.INFINITY);
                    break;

                case Database.RARE_TOTAL_HIYAKU_KASSEI:
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    int maxValue = Math.Max(player.Strength,
                                    Math.Max(player.Agility,
                                            player.Intelligence));
                    if (maxValue == player.Strength)
                    {
                        player.BuffStrength_Hiyaku_Kassei = maxValue;
                    }
                    else if (maxValue == player.Agility)
                    {
                        player.BuffAgility_Hiyaku_Kassei = maxValue;
                    }
                    else if (maxValue == player.Intelligence)
                    {
                        player.BuffIntelligence_Hiyaku_Kassei = maxValue;
                    }
                    // todo [ ActivateBuffはなくてもよい？ ]
                    break;

                case Database.RARE_SINSEISUI:
                    if (!GroundOne.WE.AlreadyUseSyperSaintWater)
                    {
                        GroundOne.WE.AlreadyUseSyperSaintWater = true;
                        player.CurrentLife += (int)((double)player.MaxLife * 0.3F);
                        player.CurrentMana += (int)((double)player.MaxMana * 0.3F);
                        player.CurrentSkillPoint += (int)((double)player.MaxSkillPoint * 0.3F);
                        mainMessage.text = player.GetCharacterSentence(2009);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2010);
                    }
                    break;

                case Database.RARE_PURE_WATER:
                    if (!GroundOne.WE.AlreadyUsePureWater)
                    {
                        GroundOne.WE.AlreadyUsePureWater = true;
                        player.CurrentLife = (int)((double)player.MaxLife);
                        mainMessage.text = player.GetCharacterSentence(2027);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2028);
                    }
                    break;

                case Database.RARE_REVIVE_POTION:
                    if (!GroundOne.WE.AlreadyUseRevivePotion)
                    {
                        for (int ii = 0; ii < allyList.Count; ii++)
                        {
                            if (allyList[ii].Dead)
                            {
                                player.DeleteBackPack(backpackData, 1, currentNumber);

                                GroundOne.WE.AlreadyUseRevivePotion = true;
                                allyList[ii].ResurrectPlayer(allyList[ii].MaxLife / 2);
                                mainMessage.text = allyList[ii].GetCharacterSentence(2016);
                                break;
                            }
                        }
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2010);
                    }
                    break;

                default:
                    mainMessage.text = player.GetCharacterSentence(2032);
                    break;
            }
        }

        public static MainCharacter GetCurrentPlayer(Color baseColor)
        {
        Debug.Log("baseColor: " + baseColor);
            MainCharacter player = null;
            if (GroundOne.MC == null) { Debug.Log("status MC is null...?"); }
            if (GroundOne.SC == null) { Debug.Log("status SC is null...?"); }
            if (GroundOne.TC == null) { Debug.Log("status TC is null...?"); }
            Debug.Log("MC.color" + GroundOne.MC.PlayerStatusColor);
            Debug.Log("SC.color" + GroundOne.SC.PlayerStatusColor);
            Debug.Log("TC.color" + GroundOne.TC.PlayerStatusColor);

            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.MC;
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.SC;
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.TC;
            }
            else
            {
                if (GroundOne.MC == null) { Debug.Log("fatal sequence..."); }
            }
            return player;
        }

        public static int GetTileNumber(Vector3 pos)
        {
            Vector3 adjustPos = new Vector3(pos.x, pos.y, pos.z);
            int number = (int)(adjustPos.x % Database.TRUTH_DUNGEON_COLUMN + (-adjustPos.y) * Database.TRUTH_DUNGEON_COLUMN);
            int row = number / Database.TRUTH_DUNGEON_COLUMN;
            int column = number % Database.TRUTH_DUNGEON_COLUMN;
            return number;
        }

        public static void GetItemFullCheck(MotherForm scene, MainCharacter player, string itemName)
        {
            bool result = player.AddBackPack(new ItemBackPack(itemName));
            if (result) return;

            string cannotTrash = string.Empty;
            if (TruthItemAttribute.CheckImportantItem(itemName) == TruthItemAttribute.Transfer.Cannot_Trash)
            {
                cannotTrash = itemName;
            }
            SceneDimension.CallTruthStatusPlayer(scene, true, itemName, cannotTrash);
        }

        public static void UpdateItemImage(ItemBackPack item, Image src)
        {
            Texture2D current = Resources.Load<Texture2D>("ItemIcon");
            int BASE_SIZE = 49;
            int locX = 0;
            int locY = 0;
            if ((item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                (item.Type == ItemBackPack.ItemType.Weapon_Middle))
            {
                locX = 0; locY = 2;
            }
            else if (item.Type == ItemBackPack.ItemType.Weapon_TwoHand)
            {
                locX = 1; locY = 2;
            }
            else if (item.Type == ItemBackPack.ItemType.Weapon_Light)
            {
                locX = 2; locY = 2;
            }
            else if (item.Type == ItemBackPack.ItemType.Weapon_Rod)
            {
                locX = 3; locY = 2;
            }
            else if (item.Type == ItemBackPack.ItemType.Shield)
            {
                locX = 0; locY = 1;
            }
            else if ((item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
                        (item.Type == ItemBackPack.ItemType.Armor_Middle))
            {
                locX = 1; locY = 1;
            }
            else if ((item.Type == ItemBackPack.ItemType.Armor_Light))
            {
                locX = 2; locY = 1;
            }
            //else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Robe))
            //{
            //    locX = 3; locY = 1;
            //}
            else if ((item.Type == ItemBackPack.ItemType.Material_Equip) ||
                        (item.Type == ItemBackPack.ItemType.Material_Food) ||
                        (item.Type == ItemBackPack.ItemType.Material_Potion))
            {
                locX = 0; locY = 0;
            }
            else if (item.Type == ItemBackPack.ItemType.Use_Potion)
            {
                locX = 1; locY = 0;
            }
            else if (item.Type == ItemBackPack.ItemType.Useless)
            {
                locX = 2; locY = 0;
            }
            else if (item.Type == ItemBackPack.ItemType.Accessory)
            {
                locX = 0; locY = 3;
            }
            else if (item.Type == ItemBackPack.ItemType.Use_BlueOrb)
            {
                locX = 1; locY = 3;
            }
            else if (item.Type == ItemBackPack.ItemType.Use_Item)
            {
                locX = 2; locY = 3;
            }
            else
            {
                locX = 2; locY = 0; // same Useless
            }
            src.sprite = Sprite.Create(current, new Rect(BASE_SIZE * locX, BASE_SIZE * locY, BASE_SIZE, BASE_SIZE), new Vector2(0, 0));
        }

        public static void UpdateBackPackLabel(MainCharacter target, GameObject[] back_Backpack, Text[] backpack, Text[] backpackStack, Image[] backpackIcon)
        {
            ItemBackPack[] backpackData = target.GetBackPackInfo();
            for (int currentNumber = 0; currentNumber < backpackData.Length; currentNumber++)
            {
                if (backpackData[currentNumber] == null)
                {
                    if (currentNumber < backpack.Length ) { backpack[currentNumber].text = ""; }
                    if (currentNumber < backpackStack.Length) { backpackStack[currentNumber].text = ""; }
                    if (currentNumber < backpackIcon.Length) { backpackIcon[currentNumber].sprite = null; }
                    if (currentNumber < backpack.Length) { Method.UpdateRareColor(null, backpack[currentNumber], back_Backpack[currentNumber], null); }
                    //back_Backpack[currentNumber].SetActive(false);
                }
                else
                {
                    back_Backpack[currentNumber].SetActive(true);
                    backpack[currentNumber].text = backpackData[currentNumber].Name;
                    Method.UpdateRareColor(backpackData[currentNumber], backpack[currentNumber], back_Backpack[currentNumber], null);
                    backpackStack[currentNumber].text = "x" + backpackData[currentNumber].StackValue.ToString();

                    Texture2D current = Resources.Load<Texture2D>("ItemIcon");
                    int BASE_SIZE = 49;
                    int locX = 0;
                    int locY = 0;
                    if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        locX = 0; locY = 2;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_TwoHand)
                    {
                        locX = 1; locY = 2;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Light)
                    {
                        locX = 2; locY = 2;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Rod)
                    {
                        locX = 3; locY = 2;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Shield)
                    {
                        locX = 0; locY = 1;
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Heavy) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Middle))
                    {
                        locX = 1; locY = 1;
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Light))
                    {
                        locX = 2; locY = 1;
                    }
                    //else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Robe))
                    //{
                    //    locX = 3; locY = 1;
                    //}
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Equip) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Food) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Potion))
                    {
                        locX = 0; locY = 0;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Potion)
                    {
                        locX = 1; locY = 0;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Useless)
                    {
                        locX = 2; locY = 0;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Accessory)
                    {
                        locX = 0; locY = 3;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_BlueOrb)
                    {
                        locX = 1; locY = 3;
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Item)
                    {
                        locX = 2; locY = 3;
                    }
                    else
                    {
                        locX = 2; locY = 0; // same Useless
                    }
                    backpackIcon[currentNumber].sprite = Sprite.Create(current, new Rect(BASE_SIZE * locX, BASE_SIZE * locY, BASE_SIZE, BASE_SIZE), new Vector2(0, 0));
                }
            }
        }

        // 親グループに空のオブジェクトを追加する(レイアウト調整専用)
        public static void AddEmptyObj(ref GameObject parentGroup, int number)
        {
            for (int ii = 0; ii < number; ii++)
            {
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(parentGroup.transform);
            }
        }
        
        // panel(gameobject)の色をレアに応じて変更
        public static void UpdateRareColor(ItemBackPack item, Text target1, GameObject target2, Text target3)
        {
            if (item == null)
            {
                if (target1 != null) { target1.color = Color.white; }
                if (target2 != null) { target2.gameObject.GetComponent<Image>().color = Color.white; }
                return;
            }

            switch (item.Rare)
            {
                case ItemBackPack.RareLevel.Poor:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = Color.gray;
                    if (target3 != null) { target3.color = Color.white; }
                    break;
                case ItemBackPack.RareLevel.Common:
                    target1.color = Color.black;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.CommonGreen;
                    if (target3 != null) { target3.color = Color.black; }
                    break;
                case ItemBackPack.RareLevel.Rare:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.DarkBlue;
                    if (target3 != null) { target3.color = Color.white; }
                    break;
                case ItemBackPack.RareLevel.Epic:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.EpicPurple;
                    if (target3 != null) { target3.color = Color.white; }
                    break;
                case ItemBackPack.RareLevel.Legendary: // 後編追加
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.OrangeRed;
                    if (target3 != null) { target3.color = Color.white; }
                    break;
            }
        }

        // ソーサリー／ノーマル／インスタントのミニアイコンをセットする
        public static void SetupActionButton(GameObject actionButton, Image sorceryMark, string actionCommand)
        {
            if (actionCommand != null && actionCommand != "" && actionCommand != string.Empty)
            {
                //Debug.Log("not equal empty : " + actionCommand);

                actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(actionCommand);
                actionButton.name = actionCommand;
                if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Sorcery)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.SorceryIcon);
                }
                else if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Normal)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
                }
                else if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Instant)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.InstantIcon);
                }
                else
                {
                    actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.STAY_EN);
                    actionButton.name = Database.STAY_EN;
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
                }
            }
            else
            {
                actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.STAY_EN);
                actionButton.name = Database.STAY_EN;
                sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
            }
        }

        public static void MakeDirectory()
        {
            if (System.IO.File.Exists(Method.PathForRootFile(Database.WE2_FILE)) == false)
            {
                Method.AutoSaveTruthWorldEnvironment();
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // なし
                string directory = Method.PathForSaveFile();
                if (System.IO.Directory.Exists(directory) == false)
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                string directory = Method.PathForSaveFile();
                if (System.IO.Directory.Exists(directory) == false)
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
            else
            {
                if (System.IO.Directory.Exists(Database.BaseSaveFolder) == false)
                {
                    System.IO.Directory.CreateDirectory(Database.BaseSaveFolder);
                }
            }
        }

        // GroundOne.WE2をリロード
        public static string PathForSaveFile()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //return Application.persistentDataPath.Substring(0, Application.persistentDataPath.LastIndexOf('/')); // after (ios)
                return Path.Combine(Application.persistentDataPath, "save");
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return Path.Combine(Application.persistentDataPath, "save");
            }
            else
            {
                return Database.BaseSaveFolder;
            }

        }

        public static string pathForDocumentsFile(string filename)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
                //path = path.Substring(0, path.LastIndexOf('/'));
                //return Path.Combine(Path.Combine(path, "Documents"), filename);
                return Path.Combine(PathForSaveFile(), filename);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return Path.Combine(PathForSaveFile(), filename);
            }
            else
            {
                return Database.BaseSaveFolder + filename;
            }
        }

        public static string PathForRootFile(string filename)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //return filename;
                return Path.Combine(Application.persistentDataPath, filename);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return Path.Combine(Application.persistentDataPath, filename);
            }
            else
            {
                return filename;
            }
        }

        public static void ReloadTruthWorldEnvironment()
        {
            XmlDocument xml2 = new XmlDocument();
            xml2.Load(PathForRootFile(Database.WE2_FILE));
            Type typeWE2 = GroundOne.WE2.GetType();
            foreach (PropertyInfo pi in typeWE2.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, Convert.ToInt32(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, (xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE2, Convert.ToBoolean(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
            }
        }

        public static void ExecSave(Text sender, string targetFileName, bool forceSave)
        {
            DateTime now = DateTime.Now;

            foreach (string overwriteData in System.IO.Directory.GetFiles(PathForSaveFile(), "*.xml"))
            {
                if (overwriteData.Contains(targetFileName))
                {
                    if (forceSave == false) // if 後編追加
                    {
                        Debug.Log("cannot overwrite savefile, because mode is not force save.");
                        //this.currentSaveText = sender;
                        //this.currentTargetFileName = targetFileName;
                        //this.yesnoSystemMessage.text = this.MESSAGE_OVERWRITE;
                        //this.groupYesnoSystemMessage.SetActive(true);
                        //this.Filter.SetActive(true);
                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(overwriteData); // 後編追加
                    }
                }
            }

            targetFileName += now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") + now.Hour.ToString("D2") + now.Minute.ToString("D2") + now.Second.ToString("D2") + GroundOne.WE.GameDay.ToString("D3");
            if (GroundOne.WE.CompleteArea5 || GroundOne.WE.TruthCompleteArea5) // 後編編集
            {
                targetFileName += Convert.ToString(6);
            }
            else if (GroundOne.WE.CompleteArea4 || GroundOne.WE.TruthCompleteArea4) // 後編編集
            {
                targetFileName += Convert.ToString(5);
            }
            else if (GroundOne.WE.CompleteArea3 || GroundOne.WE.TruthCompleteArea3) // 後編編集
            {
                targetFileName += Convert.ToString(4);
            }
            else if (GroundOne.WE.CompleteArea2 || GroundOne.WE.TruthCompleteArea2) // 後編編集
            {
                targetFileName += Convert.ToString(3);
            }
            else if (GroundOne.WE.CompleteArea1 || GroundOne.WE.TruthCompleteArea1) // 後編編集
            {
                targetFileName += Convert.ToString(2);
            }
            else
            {
                targetFileName += Convert.ToString(1);
            }
            targetFileName += ".xml";

            XmlTextWriter xmlWriter = new XmlTextWriter(pathForDocumentsFile(targetFileName), Encoding.UTF8);
            try
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteStartElement("Body");
                xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter.WriteElementString("Version", Database.VERSION.ToString());
                xmlWriter.WriteWhitespace("\r\n");

                // メインプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_MAINPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                // [昇華]：本記載テクニックを横展開してください。
                Type type = GroundOne.MC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.MC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.MC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (GroundOne.MC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.MC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.MC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.MC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (GroundOne.MC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.MC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.MC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.MC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.MC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.MC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (GroundOne.MC != null)
                {
                    ItemBackPack[] backpackInfo = GroundOne.MC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // セカンドプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_SECONDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = GroundOne.SC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.SC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.SC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加   
                }

                // プレイヤー装備
                if (GroundOne.SC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.SC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.SC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.SC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }                    // e 後編追加
                if (GroundOne.SC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.SC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.SC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.SC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.SC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.SC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (GroundOne.SC != null)
                {
                    ItemBackPack[] backpackInfo = GroundOne.SC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");


                // サードプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_THIRDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = GroundOne.TC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.TC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.TC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (GroundOne.TC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.TC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.TC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.TC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (GroundOne.TC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.TC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.TC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.TC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.TC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.TC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (GroundOne.TC != null)
                {
                    ItemBackPack[] backpackInfo = GroundOne.TC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter.WriteStartElement("WorldEnvironment");
                xmlWriter.WriteWhitespace("\r\n");
                if (GroundOne.WE != null)
                {
                    Type typeWE = GroundOne.WE.GetType();
                    foreach (PropertyInfo pi in typeWE.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE, null))).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE, null)));
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE, null)).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");


                // ダンジョン１階の制覇情報
                // [警告]：作業落とし込みで終わるものの拡張性を考慮した設計に直してください。
                // after revive
                //if (this.knownTileInfo != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonOneInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileOne" + ii, this.knownTileInfo[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo2 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonTwoInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileTwo" + ii, this.knownTileInfo2[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo3 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonThreeInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileThree" + ii, this.knownTileInfo3[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo4 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonFourInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileFour" + ii, this.knownTileInfo4[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo5 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonFiveInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileFive" + ii, this.knownTileInfo5[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                // s 後編追加
                if (GroundOne.Truth_KnownTileInfo != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonOneInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileOne" + ii, GroundOne.Truth_KnownTileInfo[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo2 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonTwoInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileTwo" + ii, GroundOne.Truth_KnownTileInfo2[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo3 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonThreeInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileThree" + ii, GroundOne.Truth_KnownTileInfo3[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo4 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFourInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFour" + ii, GroundOne.Truth_KnownTileInfo4[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo5 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFiveInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFive" + ii, GroundOne.Truth_KnownTileInfo5[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndDocument();
            }
            finally
            {
                xmlWriter.Close();

                //if ((Text)sender != null) // if 後編追加
                //{
                //    ((Text)sender).text = DateTime.Now.ToString() + "\r\n経過日数：" + GroundOne.WE.GameDay.ToString("D3") + "日 ";
                //    if (GroundOne.WE.CompleteArea5 || GroundOne.WE.TruthCompleteArea5) // 後編編集
                //    {
                //        ((Text)sender).text += archiveAreaString + archiveAreaString3;
                //    }
                //    else if (GroundOne.WE.CompleteArea4 || GroundOne.WE.TruthCompleteArea4) // 後編編集
                //    {
                //        ((Text)sender).text += archiveAreaString + "5" + archiveAreaString2;
                //    }
                //    else if (GroundOne.WE.CompleteArea3 || GroundOne.WE.TruthCompleteArea3) // 後編編集
                //    {
                //        ((Text)sender).text += archiveAreaString + "4" + archiveAreaString2;
                //    }
                //    else if (GroundOne.WE.CompleteArea2 || GroundOne.WE.TruthCompleteArea2) // 後編編集
                //    {
                //        ((Text)sender).text += archiveAreaString + "3" + archiveAreaString2;
                //    }
                //    else if (GroundOne.WE.CompleteArea1 || GroundOne.WE.TruthCompleteArea1) // 後編編集
                //    {
                //        ((Text)sender).text += archiveAreaString + "2" + archiveAreaString2;
                //    }
                //    else
                //    {
                //        ((Text)sender).text += archiveAreaString + "1" + archiveAreaString2;
                //    }

                //    if (!((Text)sender).Equals(buttonText[0])) back_button[0].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[1])) back_button[1].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[2])) back_button[2].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[3])) back_button[3].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[4])) back_button[4].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[5])) back_button[5].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[6])) back_button[6].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[7])) back_button[7].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[8])) back_button[8].GetComponent<Image>().sprite = null;
                //    if (!((Text)sender).Equals(buttonText[9])) back_button[9].GetComponent<Image>().sprite = null;
                //    for (int ii = 0; ii < buttonText.Length; ii++)
                //    {
                //        if (sender.Equals(buttonText[ii]))
                //        {
                //            back_button[ii].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.SAVELOAD_NEW);
                //        }
                //    }
                //}

                Method.AutoSaveTruthWorldEnvironment();

                //if (!forceSave) // if 後編追加
                //{
                //    this.systemMessage.text = this.MESSAGE_2;
                //    this.back_SystemMessage.SetActive(true);
                //    this.Filter.SetActive(true);
                //    this.autoKillTimer = 0;
                //    this.nowAutoKill = true;
                //}
            }
        }

        public static void ExecLoad(Text sender, string targetFileName, bool forceLoad)
        {
            GroundOne.ReInitializeGroundOne(true);

            XmlDocument xml = new XmlDocument();
            DateTime now = DateTime.Now;
            string yearData = String.Empty;
            string monthData = String.Empty;
            string dayData = String.Empty;
            string hourData = String.Empty;
            string minuteData = String.Empty;
            string secondData = String.Empty;
            string gamedayData = String.Empty;
            string completeareaData = String.Empty;

            Debug.Log("ExecLoad 1 " + DateTime.Now + "." + DateTime.Now.Minute);
            //if (((Text)sender) != null)
            //{
            //    yearData = ((Text)sender).text.Substring(0, 4);
            //    monthData = ((Text)sender).text.Substring(5, 2);
            //    dayData = ((Text)sender).text.Substring(8, 2);
            //    hourData = ((Text)sender).text.Substring(11, 2);
            //    minuteData = ((Text)sender).text.Substring(14, 2);
            //    secondData = ((Text)sender).text.Substring(17, 2);
            //    gamedayData = ((Text)sender).text.Substring(this.gameDayString.Length + 19, 3);
            //    completeareaData = ((Text)sender).text.Substring(this.gameDayString.Length + this.gameDayString2.Length + this.archiveAreaString.Length + 22, 1);

            //    if (completeareaData == "制")
            //    {
            //        this.systemMessage.text = MESSAGE_1;
            //        this.back_SystemMessage.SetActive(true);
            //        this.Filter.SetActive(true);
            //        return;
            //    }
            //    targetFileName += yearData + monthData + dayData + hourData + minuteData + secondData + gamedayData + completeareaData + ".xml";
            //}
            //else
            {
                foreach (string currentFile in System.IO.Directory.GetFiles(Method.PathForSaveFile(), "*.xml"))
                {
                    if (currentFile.Contains(Database.WorldSaveNum))
                    {
                        targetFileName = System.IO.Path.GetFileName(currentFile);
                        break;
                    }
                }
            }

            xml.Load(Method.pathForDocumentsFile(targetFileName));
            GroundOne.CurrentLoadFileName = targetFileName;
            Debug.Log("ExecLoad 2 " + DateTime.Now + "." + DateTime.Now.Minute);

            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("SubWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainArmor");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.Accessory = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory2");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加
            Debug.Log("ExecLoad 3 " + DateTime.Now + "." + DateTime.Now.Minute);

            //for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            //{
            //    XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
            //    if (temp.Count <= 0)
            //    {
            //    }
            //    else
            //    {
            //        foreach (XmlNode node in temp)
            //        {
            //            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
            //            {
            //                GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
            //            {
            //                GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
            //            {
            //                GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //        }
            //    }
            //}

            // s 後編編集

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
                foreach (XmlNode node in temp)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            // e 後編編集
            Debug.Log("ExecLoad 4 " + DateTime.Now + "." + DateTime.Now.Minute);

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try { pi.SetValue(GroundOne.MC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // e 後編追加
            }
            Debug.Log("ExecLoad 5 " + DateTime.Now + "." + DateTime.Now.Minute);

            Type typeWE = GroundOne.WE.GetType();
            Debug.Log("ExecLoad 6 " + DateTime.Now + "." + DateTime.Now.Minute);


            PropertyInfo[] tempWE = typeWE.GetProperties();
            Debug.Log(tempWE.Length.ToString());

            foreach (PropertyInfo pi in tempWE)
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText, null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText), null);
                    }
                    catch { }
                }
            }
            Debug.Log("ExecLoad 7 " + DateTime.Now + "." + DateTime.Now.Minute);

            // after revive
            //try // 後編追加 // [警告]：前編での読み込みバグが無く、かつ、後編では絶対に使わないことを前提とした記述。
            //{
            //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
            //    {
            //        knownTileInfo[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileOne" + ii.ToString()))[0].InnerText);
            //        knownTileInfo2[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileTwo" + ii.ToString()))[0].InnerText);
            //        knownTileInfo3[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileThree" + ii.ToString()))[0].InnerText);
            //        knownTileInfo4[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFour" + ii.ToString()))[0].InnerText);
            //        knownTileInfo5[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFive" + ii.ToString()))[0].InnerText);
            //    }
            //}
            //catch { }

            Method.ReloadTruthWorldEnvironment();
            Debug.Log("ExecLoad 75 " + DateTime.Now + "." + DateTime.Now.Minute);

            Method.LoadKnownTileInfo();
            Debug.Log("ExecLoad 8-1 " + DateTime.Now + "." + DateTime.Now.Minute);

            //if (forceLoad == false)
            //{
            //    this.systemMessage.text = "ゲームデータの読み込みが完了しました。";
            //    this.back_SystemMessage.SetActive(true);
            //    this.autoKillTimer = 0;
            //    this.nowAutoKill = true;
            //}

            Debug.Log("ExecLoad end");
        }

        public static void LoadKnownTileInfo()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Method.pathForDocumentsFile(GroundOne.CurrentLoadFileName));

            //Debug.Log("LoadKnownTile: S1: " + DateTime.Now + DateTime.Now.Millisecond);
            XmlNodeList listA = null;
            listA = xml.DocumentElement.ChildNodes;

            if (listA != null)
            {
                //Debug.Log("listA.count: " + listA.Count);
                for (int ii = 0; ii < listA.Count; ii++)
                {
                    //Debug.Log("listA[ii].Name" + listA[ii].Name);

                    if (listA[ii].Name == "TruthDungeonOneInfo")
                    {
                        XmlNodeList inner = listA[ii].ChildNodes;
                        //Debug.Log("inner.count: " + inner.Count);
                        for (int jj = 0; jj < inner.Count; jj++)
                        {
                            GroundOne.Truth_KnownTileInfo[jj] = Convert.ToBoolean(inner[jj].InnerText);
                        }
                    }
                    else  if (listA[ii].Name == "TruthDungeonTwoInfo")
                    {
                        XmlNodeList inner = listA[ii].ChildNodes;
                       // Debug.Log("inner.count: " + inner.Count);
                        for (int jj = 0; jj < inner.Count; jj++)
                        {
                            GroundOne.Truth_KnownTileInfo2[jj] = Convert.ToBoolean(inner[jj].InnerText);
                        }
                    }
                    else if (listA[ii].Name == "TruthDungeonThreeInfo")
                    {
                        XmlNodeList inner = listA[ii].ChildNodes;
                        //Debug.Log("inner.count: " + inner.Count);
                        for (int jj = 0; jj < inner.Count; jj++)
                        {
                            GroundOne.Truth_KnownTileInfo3[jj] = Convert.ToBoolean(inner[jj].InnerText);
                        }
                    }
                    else if (listA[ii].Name == "TruthDungeonFourInfo")
                    {
                        XmlNodeList inner = listA[ii].ChildNodes;
                        //Debug.Log("inner.count: " + inner.Count);
                        for (int jj = 0; jj < inner.Count; jj++)
                        {
                            GroundOne.Truth_KnownTileInfo4[jj] = Convert.ToBoolean(inner[jj].InnerText);
                        }
                    }
                    else if (listA[ii].Name == "TruthDungeonFiveInfo")
                    {
                        XmlNodeList inner = listA[ii].ChildNodes;
                        //Debug.Log("inner.count: " + inner.Count);
                        for (int jj = 0; jj < inner.Count; jj++)
                        {
                            GroundOne.Truth_KnownTileInfo5[jj] = Convert.ToBoolean(inner[jj].InnerText);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("listA null...");
            }
        }

        // 通常セーブ、現実世界の自動セーブ、タイトルSeekerモードの自動セーブを結合
        public static void AutoSaveTruthWorldEnvironment()
        {
            XmlTextWriter xmlWriter2 = new XmlTextWriter(Method.PathForRootFile(Database.WE2_FILE), Encoding.UTF8);
            try
            {
                xmlWriter2.WriteStartDocument();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteStartElement("Body");
                xmlWriter2.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter2.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter2.WriteStartElement("TruthWorldEnvironment");
                xmlWriter2.WriteWhitespace("\r\n");
                if (GroundOne.WE2 != null)
                {
                    Type typeWE2 = GroundOne.WE2.GetType();
                    foreach (PropertyInfo pi in typeWE2.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE2, null))).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter2.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE2, null)));
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE2, null)).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");
                xmlWriter2.WriteEndDocument();
            }
            finally
            {
                xmlWriter2.Close();
            }
        }
        public static void AutoSaveSingleWorldEnvironment()
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(Method.PathForRootFile(Database.WE3_FILE), Encoding.UTF8);
            try
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteStartElement("Body");
                xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter.WriteStartElement("SingleWorldEnvironment");
                xmlWriter.WriteWhitespace("\r\n");
                if (GroundOne.WE3 != null)
                {
                    Type typeWE = GroundOne.WE3.GetType();
                    foreach (PropertyInfo pi in typeWE.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE3, null))).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE3, null)));
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE3, null)).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndDocument();
            }
            finally
            {
                xmlWriter.Close();
            }
        }
        // 現実世界の自動セーブ
        public static void AutoSaveRealWorld()
        {
            SceneDimension.CallSaveLoadWithSaveOnly();
        }
        public static void AutoSaveRealWorld(MainCharacter MC, MainCharacter SC, MainCharacter TC, WorldEnvironment WE, bool[] knownTileInfo, bool[] knownTileInfo2, bool[] knownTileInfo3, bool[] knownTileInfo4, bool[] knownTileInfo5, bool[] Truth_KnownTileInfo, bool[] Truth_KnownTileInfo2, bool[] Truth_KnownTileInfo3, bool[] Truth_KnownTileInfo4, bool[] Truth_KnownTileInfo5)
        {
            SceneDimension.CallSaveLoadWithSaveOnly();
        }

        // 街でオル・ランディスが外れる、４階最初でヴェルゼが外れる、４階エリア３でラナが外れるのを統合
        public static void RemoveParty(MainCharacter player, bool initializeBank)
        {
            if (GroundOne.WE.AvailableThirdCharacter)
            {
                GroundOne.WE.AvailableThirdCharacter = false;
                // オルとヴェルゼは再度復帰する予定はないため、ここで一旦ソウルポイントをクリアする。
                for (int ii = 0; ii < player.CurrentSoulAttributes.Length; ii++)
                {
                    player.CurrentSoulAttributes[ii] = 0;
                }
            }
            else if (GroundOne.WE.AvailableSecondCharacter)
            {
                GroundOne.WE.AvailableSecondCharacter = false;
                // ラナの場合、再度復帰する予定があるため、ここで一旦ソウルポイントはクリアしない。
                //for (int ii = 0; ii < player.CurrentSoulAttributes.Length; ii++)
                //{
                //    player.CurrentSoulAttributes[ii] = 0;
                //}
            }

            string[] itemBank = new string[Database.MAX_ITEM_BANK];
            int[] itemBankStack = new int[Database.MAX_ITEM_BANK];
            int current = 0;

            if (initializeBank)
            {
                GroundOne.WE.InitializeItemBankData();
            }

            string[] beforeItem = new string[Database.MAX_ITEM_BANK];
            int[] beforeStack = new int[Database.MAX_ITEM_BANK];
            GroundOne.WE.LoadItemBankData(ref beforeItem, ref beforeStack);
            for (int ii = 0; ii < beforeItem.Length; ii++)
            {
                if (beforeItem[ii] == String.Empty || beforeItem[ii] == "" || beforeItem[ii] == null)
                {
                    // 空っぽの場合、何も追加しない。
                }
                else
                {
                    itemBank[current] = beforeItem[ii];
                    itemBankStack[current] = beforeStack[ii];
                    current++;
                }
            }

            if (player.MainWeapon != null)
            {
                if ((player.MainWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                    (player.MainWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.SubWeapon != null)
            {
                if ((player.SubWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                    (player.SubWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.SubWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.SubWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.MainArmor != null)
            {
                if ((player.MainArmor.Name != Database.COMMON_AURA_ARMOR) &&
                    (player.MainArmor.Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainArmor.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory != null)
            {
                if ((player.Accessory.Name != Database.COMMON_FATE_RING) &&
                    (player.Accessory.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory2 != null)
            {
                if ((player.Accessory2.Name != Database.COMMON_FATE_RING) &&
                    (player.Accessory2.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory2.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory2.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            ItemBackPack[] backpackInfo = player.GetBackPackInfo();
            for (int ii = 0; ii < backpackInfo.Length; ii++)
            {
                if (backpackInfo[ii] != null)
                {
                    if ((backpackInfo[ii].Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                        (backpackInfo[ii].Name != Database.COMMON_AURA_ARMOR) &&
                        (backpackInfo[ii].Name != Database.COMMON_FATE_RING) &&
                        (backpackInfo[ii].Name != Database.COMMON_LOYAL_RING) &&
                        (backpackInfo[ii].Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA))
                    {
                        itemBank[current] = backpackInfo[ii].Name;
                        itemBankStack[current] = backpackInfo[ii].StackValue;
                        current++;
                    }
                }
            }
            GroundOne.WE.UpdateItemBankData(itemBank, itemBankStack);
        }

        // 戦闘終了後のアイテムゲット、ファージル宮殿お楽しみ抽選券のアイテムゲットを統合
        public static string GetNewItem(NewItemCategory category, MainCharacter mc, TruthEnemyCharacter ec1 = null, int dungeonArea = 0)
        {
            string targetItemName = String.Empty;
            int debugCounter1 = 0;
            int debugCounter2 = 0;
            int debugCounter3 = 0;
            int debugCounter4 = 0;
            int debugCounter5 = 0;
            int debugCounter6 = 0;
            int debugCounter7 = 0;

            int debugCounter8 = 0;

            for (int zzz = 0; zzz < 1; zzz++)
            {
                System.Threading.Thread.Sleep(1);

                // ドロップアイテムを出現させる
                System.Random rd = new System.Random(Environment.TickCount * DateTime.Now.Millisecond);
                int param1 = 1000; // 素材
                int param2 = 600; // 武具POOR
                int param3 = 350; // 武具COMMON
                int param4 = 50; // 武具RARE
                int param5 = 20; // パラメタUP
                int param6 = 10; // EPIC
                int param7 = 200; // ハズレ

                // param1 は固定でいくこと
                // param2 + param3 + param4 は1000とすること
                // param7はBlack以外は0とすること
                if (ec1 != null)
                {
                    switch (ec1.Rare)
                    {
                        case TruthEnemyCharacter.RareString.Black:
                            param1 = 1000;
                            param2 = 600;
                            param3 = 350;
                            param4 = 50;
                            param5 = 20;
                            param6 = 10 + GroundOne.WE2.KillingEnemy; // EPICを少し出しやすくする味付け
                            param7 = 200;
                            break;
                        case TruthEnemyCharacter.RareString.Blue:
                            param1 = 1000;
                            param2 = 100;
                            param3 = 700;
                            param4 = 200;
                            param5 = 60;
                            param6 = 20 + GroundOne.WE2.KillingEnemy * 3; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Red:
                            param1 = 1000;
                            param2 = 0;
                            param3 = 500;
                            param4 = 500;
                            param5 = 120;
                            param6 = 40 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Gold: // 階層ボスは固定ドロップアイテムだが、通常ボスはランダム扱い
                            param1 = 0; // ボスレベルで素材は無い事とする。
                            param2 = 0;
                            param3 = 600;
                            param4 = 1200;
                            param5 = 400;
                            param6 = 80 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Purple: // 支配竜は固定ドロップアイテム
                            break;
                    }

                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51))
                    {
                        param1 = 0;
                        param2 = 0;
                        param3 = 500;
                        param4 = 400;
                        param5 = 300;
                        param6 = 15 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                        param7 = 0;
                    }                    
                }
                else if (category == NewItemCategory.Lottery)
                {
                    param1 = 0; // 抽選券、モンスター素材ではない。
                    param2 = 0; // 抽選券、POORは無しとする
                    param3 = 600;
                    param4 = 240;
                    param5 = 100;
                    param6 = 7;
                    param7 = 0; // 抽選券、ハズレは無しとする
                    debugCounter8++;
                }

                int randomValue = rd.Next(1, param1 + param2 + param3 + param4 + param5 + param6 + param7 + 1);
                int randomValue2 = rd.Next(1, 1 + param1 + param2 + param3 + param4);
                int randomValue21 = rd.Next(1, 19);
                int randomValue22 = rd.Next(1, 11);
                int randomValue3 = rd.Next(1, 17);
                int randomValue32 = rd.Next(1, 26);
                int randomValue4 = rd.Next(1, 6);
                int randomValue42 = rd.Next(1, 9);
                int randomValue5 = rd.Next(1, 6);
                int randomValue6 = rd.Next(1, 3);
                int randomValue7 = rd.Next(1, 101);

                #region "エリア毎のアイテム総数に応じた値を設定"
                // 1階は上述宣言時の値そのもの
                if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                    (category == NewItemCategory.Lottery && dungeonArea == 1))
                {
                    randomValue21 = rd.Next(1, 19);
                    randomValue22 = rd.Next(1, 11);
                    randomValue3 = rd.Next(1, 17);
                    randomValue32 = rd.Next(1, 26);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 9);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 3);
                    randomValue7 = rd.Next(1, 101);
                }
                // 2階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 2))
                {
                    randomValue21 = rd.Next(1, 10);
                    randomValue22 = rd.Next(1, 10);
                    randomValue3 = rd.Next(1, 18);
                    randomValue32 = rd.Next(1, 28);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 16);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 3階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 3))
                {
                    randomValue21 = rd.Next(1, 5);
                    randomValue22 = rd.Next(1, 5);
                    randomValue3 = rd.Next(1, 14);
                    randomValue32 = rd.Next(1, 32);
                    randomValue4 = rd.Next(1, 7);
                    randomValue42 = rd.Next(1, 24);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 4階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 4))
                {
                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 23);
                    randomValue32 = rd.Next(1, 29);
                    randomValue4 = rd.Next(1, 11);
                    randomValue42 = rd.Next(1, 27);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = rd.Next(1, 101);
                }
                // 現実世界４層ラスト
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51))
                {
                    param1 = 0;
                    param2 = 0;

                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 16);
                    randomValue32 = 0;
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = 0;
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = 0;
                }
                #endregion

                #region "モンスター毎の素材ドロップ"
                if (1 <= randomValue && randomValue <= param1) // 44.84 %
                {
                    int DropItemNumber = 0;
                    for (int ii = 0; ii < ec1.DropItem.Length; ii++)
                    {
                        if (ec1.DropItem[ii] != String.Empty)
                        {
                            DropItemNumber++;
                        }
                    }
                    if (DropItemNumber <= 0) // 素材登録が無い場合、ハズレ
                    {
                        targetItemName = String.Empty;
                    }
                    else
                    {
                        int randomValue1 = AP.Math.RandomInteger(DropItemNumber);
                        targetItemName = ec1.DropItem[randomValue1];
                    }

                    debugCounter1++;
                }
                #endregion
                #region "ダンジョンエリア毎の汎用装備品"
                else if (param1 < randomValue && randomValue <= (param1 + param2 + param3 + param4)) // 44.84%
                {
                    if (1 <= randomValue2 && randomValue2 <= param2) // Poor 60.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HINJAKU_ARMRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_USUYOGORETA_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_NON_BRIGHT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_KUKEI_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SUTERARESHI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_ARIFURETA_STATUE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_NON_ADJUST_BELT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIMPLE_EARRING;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_KATAKUZURESHITA_FINGERRING;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_IROASETA_CHOKER;
                                    break;
                                case 11:
                                    targetItemName = Database.POOR_YOREYORE_MANTLE;
                                    break;
                                case 12:
                                    targetItemName = Database.POOR_NON_HINSEI_CROWN;
                                    break;
                                case 13:
                                    targetItemName = Database.POOR_TUKAIFURUSARETA_SWORD;
                                    break;
                                case 14:
                                    targetItemName = Database.POOR_TUKAINIKUI_LONGSWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.POOR_GATAGAKITERU_ARMOR;
                                    break;
                                case 16:
                                    targetItemName = Database.POOR_FESTERING_ARMOR;
                                    break;
                                case 17:
                                    targetItemName = Database.POOR_HINSO_SHIELD;
                                    break;
                                case 18:
                                    targetItemName = Database.POOR_MUDANIOOKII_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_OLD_USELESS_ROD;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_KISSAKI_MARUI_TUME;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BATTLE_HUMUKI_BUTOUGI;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SIZE_AWANAI_ROBE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NO_CONCEPT_RING;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_HIGHCOLOR_MANTLE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_EIGHT_PENDANT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_GOJASU_BELT;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EGARA_HUMEI_EMBLEM;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_HAYATOTIRI_ORB;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HUANTEI_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_DEPRESS_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_DAMAGED_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SHIMETSUKE_BELT;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NOGENKEI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_MAGICLIGHT_FIRE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_MAGICLIGHT_ICE;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_MAGICLIGHT_SHADOW;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_MAGICLIGHT_LIGHT;
                                    break;

                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_CURSE_EARRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_CURSE_BOOTS;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BLOODY_STATUE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_FALLEN_MANTLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SIHAIRYU_SIKOTU;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_OLD_TREE_KAREHA;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_GALEWIND_KONSEKI;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIN_CRYSTAL_KAKERA;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EVERMIND_ZANSHI;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_DIRTY_ANGEL_CONTRACT;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_FROZEN;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_PARALYZE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_STUN;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_MIGAWARI_DOOL;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_ONE_DROPLET_KESSYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_MOMENTARY_FLASH_LIGHT;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SUN_YUME_KAKERA;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            targetItemName = String.Empty;
                        }
                        else if (ec1.Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                            ec1.Area == TruthEnemyCharacter.MonsterArea.Area44)
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        debugCounter2++;
                    }
                    // ダンジョンエリア毎のコモン汎用装備品
                    else if (param2 < randomValue2 && randomValue2 <= (param2 + param3)) // Common 35.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_PENDANT;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_PENDANT;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_PENDANT;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_PENDANT;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_PENDANT;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SISSO_ARMRING;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_FINE_FEATHER;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_KIREINA_ORB;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_FIT_BANGLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PRISM_EMBLEM;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_FINE_SWORD;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_TWEI_SWORD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_FINE_ARMOR;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GOTHIC_PLATE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_FINE_SHIELD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_GRIPPING_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_COPPER_RING_TORA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_COPPER_RING_IRUKA;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_COPPER_RING_UMA;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMA;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_COPPER_RING_HAYABUSA;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_COPPER_RING_TAKO;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_COPPER_RING_USAGI;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMO;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_COPPER_RING_SHIKA;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_COPPER_RING_ZOU;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_AMULET;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_AMULET;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_AMULET;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_AMULET;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_AMULET;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SHARP_CLAW;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_LIGHT_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_WOOD_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_SHORT_SWORD;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_BASTARD_SWORD;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_LETHER_CLOTHING;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_COTTON_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_COPPER_ARMOR;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_HEAVY_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_IRON_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CHARM;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CHARM;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CHARM;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CHARM;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CHARM;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_THREE_COLOR_COMPASS;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SANGO_CROWN;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SMOOTHER_BOOTS;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SHIOKAZE_MANTLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SMART_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SMART_CLAW;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_SMART_ROD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_SMART_SHIELD;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_RAUGE_SWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_SMART_CLOTHING;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SMART_ROBE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_SMART_PLATE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_BRONZE_RING_KIBA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BRONZE_RING_SASU;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_BRONZE_RING_KU;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_BRONZE_RING_NAGURI;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_BRONZE_RING_TOBI;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_BRONZE_RING_KARAMU;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_BRONZE_RING_HANERU;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BRONZE_RING_TORU;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_BRONZE_RING_MIRU;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_BRONZE_RING_KATAI;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_KOKUIN;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_KOKUIN;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_KOKUIN;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_KOKUIN;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_KOKUIN;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SISSEI_MANTLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_KAISEI_EMBLEM;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SAZANAMI_EARRING;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_AMEODORI_STATUE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_SMASH_BLADE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_POWERED_BUSTER;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_STONE_CLAW;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_DENDOU_ROD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_SERPENT_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SWIFT_CROSS;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_CHIFFON_ROBE;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_PURE_BRONZE_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_STONE;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_STONE;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_STONE;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_STONE;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_STONE;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_EXCELLENT_SWORD;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_EXCELLENT_KNUCKLE;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_EXCELLENT_ROD;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_EXCELLENT_BUSTER;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_EXCELLENT_ARMOR;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_EXCELLENT_CROSS;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_EXCELLENT_ROBE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_EXCELLENT_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_STEEL_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_STEEL_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_STEEL_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_STEEL_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_STEEL_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_STEEL_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_STEEL_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_STEEL_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_STEEL_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_STEEL_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_MASEKI;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_MASEKI;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_MASEKI;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_MASEKI;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_MASEKI;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_DESCENED_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_FALSET_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SEKIGAN_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_ROCK_BUSTER;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_COLD_STEEL_PLATE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_AIR_HARE_CROSS;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_FLOATING_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_SNOW_CRYSTAL_SHIELD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_WING_STEP_FEATHER;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SNOW_FAIRY_BREATH;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_STASIS_RING;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_SIHAIRYU_KIBA;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_OLD_TREE_JUSHI;
                                    break;
                                case 29:
                                    targetItemName = Database.COMMON_GALEWIND_KIZUATO;
                                    break;
                                case 30:
                                    targetItemName = Database.COMMON_SIN_CRYSTAL_QUATZ;
                                    break;
                                case 31:
                                    targetItemName = Database.COMMON_EVERMIND_OMEN;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_MEDALLION;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_MEDALLION;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_MEDALLION;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_MEDALLION;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_MEDALLION;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SOCIETY_SYMBOL;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_FEATHER_BRACELET;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BIRD_SONG_LUTE;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_MAZE_CUBE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_LIGHT_SERVANT;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SHADOW_SERVANT;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_ROYAL_GUARD_RING;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_ELEMENTAL_GUARD_RING;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_HAYATE_GUARD_RING;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_MASTER_SWORD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MASTER_KNUCKLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_MASTER_ROD;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_MASTER_AXE;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_MASTER_ARMOR;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_MASTER_CROSS;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_MASTER_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_MASTER_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_SILVER_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_SILVER_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_SILVER_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_SILVER_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_SILVER_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SILVER_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SILVER_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SILVER_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SILVER_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_FLOAT_STONE;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_FLOAT_STONE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_FLOAT_STONE;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_FLOAT_STONE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_FLOAT_STONE;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MUKEI_SAKAZUKI;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_RAINBOW_TUBE;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_ELDER_PERSPECTIVE_GRASS;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_DEVIL_SEALED_VASE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_FLOATING_WHITE_BALL;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_INITIATE_SWORD;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_BULLET_KNUCKLE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_KENTOUSI_SWORD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_ELECTRO_ROD;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_FORTIFY_SCALE;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_MURYOU_CROSS;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_COLORLESS_ROBE;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_LOGISTIC_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CRYSTAL;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CRYSTAL;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CRYSTAL;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CRYSTAL;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CRYSTAL;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_PLATINUM_RING_1;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_PLATINUM_RING_2;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_PLATINUM_RING_3;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_PLATINUM_RING_4;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PLATINUM_RING_5;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_PLATINUM_RING_6;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_PLATINUM_RING_7;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PLATINUM_RING_8;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_PLATINUM_RING_9;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_PLATINUM_RING_10;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter3++;
                    }
                    // ダンジョンエリア毎のレア汎用装備品
                    else if ((param2 + param3) < randomValue2 && randomValue2 <= (param2 + param3 + param4)) // Rare 5.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_JOUSITU_BLUE_POWERRING;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_KOUJOUSINYADORU_RED_ORB;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_MAGICIANS_MANTLE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BEATRUSH_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_AERO_BLADE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SINTYUU_RING_KUROHEBI;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SINTYUU_RING_HAKUTYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_SINTYUU_RING_AKAHYOU;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_ICE_SWORD;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RISING_KNUCKLE;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_AUTUMN_ROD;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SUN_BRAVE_ARMOR;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_ESMERALDA_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_WRATH_SERVEL_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_BLUE_LIGHTNING;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DIRGE_ROBE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_DUNSID_PLATE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_BURNING_CLAYMORE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_KONSHIN;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SYUNSOKU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_JUKURYO;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SOUGEN;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_YUUDAI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_MEIUN_BOX;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_WILL_HOLY_HAT;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EMBLEM_BLUESTAR;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SEAL_OF_DEATH;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DARKNESS_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_BLUE_RED_ROD;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SHARKSKIN_ARMOR;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_RED_THUNDER_ROBE;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_BLACK_MAGICIAN_CROSS;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_BLUE_SKY_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_MENTALIZED_FORCE_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_ADERKER_FALSE_ROD;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_BLACK_ICE_SWORD;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_CLAYMORE_ZUKS;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_DRAGONSCALE_ARMOR;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_LIGHT_BLIZZARD_ROBE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_FROZEN_LAVA;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_WHITE_TIGER_ANGEL_GOHU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_SOLID;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_VAPOR;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_ERASTIC;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_TORAREITION;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SYUURENSYA_KUROOBI;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_SHIHANDAI_KUROOBI;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SYUUDOUSOU_KUROOBI;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_KUGYOUSYA_KUROOBI;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_TEARS_END;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SKY_COLD_BOOTS;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_EARTH_BREAKERS_SIGIL;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_AERIAL_VORTEX;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_LIVING_GROWTH_SEED;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_SHARPNEL_SPIN_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_BLUE_LIGHT_MOON_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_BLIZZARD_SNOW_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHAERING_BONE_CRUSHER;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_SCALE_BLUERAGE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_BLUE_REFLECT_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_SLIDE_THROUGH_SHIELD;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_ELEMENTAL_STAR_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SPELL_COMPASS;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SHADOW_BIBLE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DETACHMENT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BLIND_NEEDLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_CORE_ESSENCE_CHANNEL;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_ASTRAL_VOID_BLADE;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_VERDANT_SONIC_CLAW;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_PRISONER_BREAKING_AXE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_INVISIBLE_STATE_ROD;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DOMINATION_BRAVE_ARMOR;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SEAL_OF_ASSASSINATION;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_EMBLEM_OF_VALKYRIE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_EMBLEM_OF_HADES;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_SIHAIRYU_KATAUDE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_OLD_TREE_SINKI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_GALEWIND_IBUKI;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SIN_CRYSTAL_SOLID;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EVERMIND_SENSE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_DEVIL_SUMMONER_TOME;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_ANGEL_CONTRACT;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_ARCHANGEL_CONTRACT;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_DARKNESS_COIN;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_SOUSUI_HIDENSYO;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_MEEK_HIDENSYO;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_JUKUTATUSYA_HIDENSYO;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_KYUUDOUSYA_HIDENSYO;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_DANZAI_ANGEL_GOHU;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_ETHREAL_EDGE_SABRE;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHINGETUEN_CLAW;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_BLOODY_DIRTY_SCYTHE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_ALL_ELEMENTAL_ROD;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_BLOOD_BLAZER_CROSS;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_DARK_ANGEL_ROBE;
                                    break;
                                case 24:
                                    targetItemName = Database.RARE_MAJEST_HAZZARD_SHIELD;
                                    break;
                                case 25:
                                    targetItemName = Database.RARE_WHITE_DIAMOND_SHIELD;
                                    break;
                                case 26:
                                    targetItemName = Database.RARE_VAPOR_SOLID_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID5_MIND;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter4++;
                    }
                }
                #endregion
                #region "ダンジョン階層依存のパワーアップアイテム"
                else if ((param1 + param2 + param3 + param4) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5)) // Rare Use Item 0.90%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID2_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID3_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID4_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID5_MIND;
                                break;
                        }
                    }
                    #endregion
                    debugCounter5++;
                }
                #endregion
                #region "ダンジョン階層依存の高級装備品"
                else if ((param1 + param2 + param3 + param4 + param5) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6)) // EPIC 0.45%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 10)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_RING_OF_OSCURETE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MERGIZD_SOL_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 27)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID2_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_GARVANDI_ADILORB;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MAXCARN_X_BUSTER;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_JUZA_ARESTINE_SLICER;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 45)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID3_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_SHEZL_MYSTIC_FORTUNE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_MERGIZD_DAV_AGITATED_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 55)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID4_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                    break;
                                case 4:
                                    targetItemName = Database.EPIC_JUZA_THE_PHANTASMAL_CLAW;
                                    break;
                                case 5:
                                    targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        // 低レベル制限はかけない。
                        switch (randomValue6)
                        {
                            case 1:
                                targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                break;
                            case 2:
                                targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                break;
                            case 3:
                                targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                break;
                            case 4:
                                targetItemName = Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2;
                                break;
                            case 5:
                                targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                break;
                        }
                    }
                    #endregion
                    debugCounter6++;
                }
                #endregion
                #region "ハズレ"
                else if ((param1 + param2 + param3 + param4 + param5 + param6) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6 + param7)) // ハズレ 8.97 %
                {
                    targetItemName = String.Empty;
                    debugCounter7++;
                }
                else // 万が一規定外の値はハズレ
                {
                    targetItemName = String.Empty;
                }
                #endregion

                #region "ハズレは、不用品をランダムドロップ"
                if (targetItemName == string.Empty)
                {
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL2;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL3;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL4;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL5;
                        }
                    }
                }
                #endregion
            }

            //MessageBox.Show(debugCounter1.ToString() + "(" + Convert.ToString((double)(((double)debugCounter1 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter2.ToString() + "(" + Convert.ToString((double)(((double)debugCounter2 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter3.ToString() + "(" + Convert.ToString((double)(((double)debugCounter3 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter4.ToString() + "(" + Convert.ToString((double)(((double)debugCounter4 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter5.ToString() + "(" + Convert.ToString((double)(((double)debugCounter5 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter6.ToString() + "(" + Convert.ToString((double)(((double)debugCounter6 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter7.ToString() + "(" + Convert.ToString((double)(((double)debugCounter7 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter8.ToString() + "\r\n");

            #region "ボス撃破、固定ドロップアイテム"
            if ((category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_LEVIATHAN) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_HOWLING_SEIZER) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1))
            {
                targetItemName = ec1.DropItem[0];
            }
            #endregion

            return targetItemName;
        }

        /// <summary>
        ///　セーブデータフラグを内部変数の配列に取り込む
        /// </summary>
        public static void GetRewardData(ref bool[,] rewardComplete, ref bool[, ,] rewardData)
        {
            int num = 0; int area = 0;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_1; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_2; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_3; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_4; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_5; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_6; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_7; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_8; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_9; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_10; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_11; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_12; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete1_13; num++;
            num = 0; area = 1;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_1; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_2; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_3; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_4; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_5; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_6; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_7; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_8; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_9; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_10; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_11; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete2_12; num++;
            num = 0; area = 2;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_1; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_2; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_3; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_4; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_5; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_6; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_7; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_8; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_9; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_10; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_11; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete3_12; num++;
            num = 0; area = 3;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_1; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_2; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_3; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_4; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_5; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_6; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_7; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_8; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_9; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_10; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_11; num++;
            rewardComplete[area, num] = GroundOne.WE.MQ_Complete4_12; num++;

            num = 0; area = 0;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_1_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_1_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_1_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_2_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_2_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_2_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_3_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_3_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_3_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_4_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_4_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_4_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_5_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_5_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_5_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_6_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_6_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_6_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_7_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_7_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_7_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_8_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_8_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_8_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_9_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_9_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_9_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_10_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_10_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_10_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_11_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_11_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_11_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_12_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_12_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_12_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_13_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_13_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward1_13_3; num++;
            num = 0; area = 1;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_1_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_1_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_1_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_2_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_2_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_2_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_3_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_3_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_3_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_4_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_4_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_4_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_5_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_5_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_5_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_6_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_6_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_6_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_7_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_7_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_7_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_8_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_8_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_8_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_9_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_9_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_9_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_10_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_10_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_10_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_11_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_11_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_11_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_12_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_12_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward2_12_3; num++;
            num = 0; area = 2;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_1_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_1_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_1_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_2_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_2_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_2_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_3_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_3_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_3_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_4_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_4_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_4_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_5_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_5_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_5_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_6_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_6_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_6_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_7_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_7_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_7_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_8_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_8_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_8_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_9_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_9_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_9_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_10_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_10_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_10_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_11_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_11_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_11_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_12_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_12_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward3_12_3; num++;
            num = 0; area = 3;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_1_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_1_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_1_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_2_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_2_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_2_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_3_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_3_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_3_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_4_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_4_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_4_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_5_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_5_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_5_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_6_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_6_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_6_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_7_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_7_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_7_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_8_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_8_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_8_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_9_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_9_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_9_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_10_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_10_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_10_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_11_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_11_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_11_3; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_12_1; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_12_2; num++;
            rewardData[area, num / 3, num % 3] = GroundOne.WE.MQ_Reward4_12_3; num++;
        }

        /// <summary>
        ///　内部変数の配列をセーブデータフラグに反映する。
        /// </summary>
        public static void SetRewardData(bool[,] rewardComplete, bool[, ,] rewardData)
        {
            int num = 0; int area = 0;
            GroundOne.WE.MQ_Complete1_1 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_2 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_3 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_4 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_5 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_6 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_7 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_8 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_9 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_10 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_11 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_12 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete1_13 = rewardComplete[area, num]; num++;
            num = 0; area = 1;
            GroundOne.WE.MQ_Complete2_1 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_2 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_3 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_4 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_5 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_6 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_7 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_8 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_9 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_10 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_11 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete2_12 = rewardComplete[area, num]; num++;
            num = 0; area = 2;
            GroundOne.WE.MQ_Complete3_1 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_2 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_3 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_4 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_5 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_6 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_7 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_8 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_9 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_10 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_11 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete3_12 = rewardComplete[area, num]; num++;
            num = 0; area = 3;
            GroundOne.WE.MQ_Complete4_1 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_2 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_3 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_4 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_5 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_6 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_7 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_8 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_9 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_10 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_11 = rewardComplete[area, num]; num++;
            GroundOne.WE.MQ_Complete4_12 = rewardComplete[area, num]; num++;

            num = 0; area = 0;
            GroundOne.WE.MQ_Reward1_1_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_1_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_1_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_2_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_2_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_2_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_3_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_3_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_3_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_4_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_4_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_4_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_5_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_5_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_5_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_6_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_6_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_6_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_7_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_7_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_7_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_8_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_8_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_8_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_9_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_9_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_9_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_10_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_10_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_10_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_11_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_11_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_11_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_12_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_12_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_12_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_13_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_13_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward1_13_3 = rewardData[area, num / 3, num % 3]; num++;
            num = 0; area = 1;
            GroundOne.WE.MQ_Reward2_1_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_1_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_1_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_2_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_2_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_2_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_3_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_3_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_3_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_4_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_4_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_4_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_5_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_5_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_5_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_6_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_6_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_6_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_7_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_7_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_7_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_8_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_8_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_8_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_9_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_9_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_9_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_10_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_10_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_10_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_11_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_11_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_11_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_12_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_12_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward2_12_3 = rewardData[area, num / 3, num % 3]; num++;
            num = 0; area = 2;
            GroundOne.WE.MQ_Reward3_1_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_1_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_1_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_2_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_2_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_2_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_3_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_3_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_3_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_4_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_4_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_4_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_5_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_5_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_5_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_6_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_6_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_6_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_7_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_7_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_7_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_8_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_8_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_8_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_9_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_9_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_9_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_10_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_10_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_10_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_11_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_11_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_11_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_12_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_12_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward3_12_3 = rewardData[area, num / 3, num % 3]; num++;
            num = 0; area = 3;
            GroundOne.WE.MQ_Reward4_1_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_1_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_1_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_2_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_2_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_2_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_3_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_3_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_3_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_4_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_4_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_4_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_5_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_5_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_5_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_6_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_6_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_6_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_7_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_7_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_7_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_8_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_8_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_8_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_9_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_9_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_9_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_10_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_10_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_10_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_11_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_11_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_11_3 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_12_1 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_12_2 = rewardData[area, num / 3, num % 3]; num++;
            GroundOne.WE.MQ_Reward4_12_3 = rewardData[area, num / 3, num % 3]; num++;
        }
    }
}

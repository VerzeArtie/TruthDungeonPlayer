using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DungeonPlayer
{
    public class ItemBackPack
    {

        public enum ItemType
        {
            None,
            Weapon_Light,
            Weapon_Middle,
            Weapon_Heavy,
            Weapon_TwoHand,
            Weapon_Rod,
            Armor_Light,
            Armor_Middle,
            Armor_Heavy,
            Shield,
            Accessory,
            Material_Equip,
            Material_Potion,
            Material_Food,
            Use_Potion,
            Use_Any,
            Useless,
        }

        public enum RareLevel
        {
            Poor,
            Common,
            Rare,
            Epic,
            Legendary,
        }

        public ItemBackPack(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int MinValue { get; set; } // todo: but delete it
        public int MaxValue { get; set; } // todo: bnt delete it
        public int MagicMinValue { get; set; } // todo: but delete it
        public int MagicMaxValue { get; set; } // todo: but delete it
        public int Cost { get; set; }
        public ItemType Type { get; set; }
        public RareLevel Rare { get; set; }
        public int PhysicalAttackMinValue { get; set; }
        public int PhysicalAttackMaxValue { get; set; }
        public int PhysicalDefenseMinValue { get; set; }
        public int PhysicalDefenseMaxValue { get; set; }
        public int MagicAttackMinValue { get; set; }
        public int MagicAttackMaxValue { get; set; }
        public int MagicDefenseMinValue { get; set; }
        public int MagicDefenseMaxValue { get; set; }
        public int BuffUpStrength { get; set; }
        public int BuffUpAgility { get; set; }
        public int BuffUpIntelligence { get; set; }
        public int BuffUpStamina { get; set; }
        public int BuffUpMind { get; set; }
        public int ResistLight { get; set; }
        public int ResistShadow { get; set; }
        public int ResistFire { get; set; }
        public int ResistIce { get; set; }
        public int ResistForce { get; set; }
        public int ResistWill { get; set; }
        public bool ResistStun { get; set; }
        public bool ResistSilence { get; set; }
        public bool ResistPoison { get; set; }
        public bool ResistTemptation { get; set; }
        public bool ResistFrozen { get; set; }
        public bool ResistParalyze { get; set; }
        public bool ResistSlow { get; set; }
        public bool ResistBlind { get; set; }
        public bool ResistSlip { get; set; }
        public bool ResistNoResurrection { get; set; }
        public double AmplifyPhysicalAttack { get; set; }
        public double AmplifyPhysicalDefense { get; set; }
        public double AmplifyMagicAttack { get; set; }
        public double AmplifyMagicDefense { get; set; }
        public double AmplifyBattleSpeed { get; set; }
        public double AmplifyBattleResponse { get; set; }
        public double AmplifyPotential { get; set; }
        public double AmplifyLight { get; set; }
        public double AmplifyShadow { get; set; }
        public double AmplifyFire { get; set; }
        public double AmplifyIce { get; set; }
        public double AmplifyForce { get; set; }
        public double AmplifyWill { get; set; }
        public double ManaCostReduction = 0; // 後編追加(魔法消費軽減)
        public double ManaCostReductionLight = 0; // 後編追加
        public double ManaCostReductionShadow = 0; // 後編追加
        public double ManaCostReductionFire = 0; // 後編追加
        public double ManaCostReductionIce = 0; // 後編追加
        public double ManaCostReductionForce = 0; // 後編追加
        public double ManaCostReductionWill = 0; // 後編追加
        public double SkillCostReduction = 0; // 後編追加（スキル消費軽減）
        public double SkillCostReductionActive = 0; // 後編追加
        public double SkillCostReductionPassive = 0; // 後編追加
        public double SkillCostReductionSoft = 0; // 後編追加
        public double SkillCostReductionHard = 0; // 後編追加
        public double SkillCostReductionTruth = 0; // 後編追加
        public double SkillCostReductionVoid = 0; // 後編追加

        public double EffectValue1 { get; set; } // 後編追加(最大スキルポイント増加)
        public bool SwitchStatus1 { get; set; }
        public string Information { get; set; }
        public bool UseSpecialAbility { get; set; }
        public bool AfterBroken { get; set; }
        public bool EffectStatus { get; set; }
        public bool OnlyOnce { get; set; }
        public string ImprintCommand { get; set; }

        // s 後編追加
        public int StackValue
        {
            get { return stackValue; }
            set { stackValue = value; }
        }
        public int LimitValue
        {
            get { return limitValue; }
            set { limitValue = value; }
        }
        // e 後編追加
        // s 後編追加
        protected int stackValue = 1; // 生成した時点で１つのオブジェクトがあるため、明示的に１を宣言
        protected int limitValue = Database.MAX_ITEM_STACK_SIZE; // オブジェクトがスタックできる最大数

        // [comment] アイテム消耗品より、RARE_EPICだった場合はスタック１とすること。
        //           装備品はRARE_EPICと同等で気にしなくて良い。
        public const int USING_ITEM_STACK_SIZE = 5;
        public const int RARE_EPIC_ITEM_STACK_SIZE = 1;
        public const int EQUIP_ITEM_STACK_SIZE = 1;
        public const int MATERIAL_ITEM_STACK_SIZE = 10;
        public const int OTHER_ITEM_STACK_SIZE = 10;
        // e 後編追加


        public static IComparer SortItemBackPackUsed()
        {
            return (IComparer)new ItemBackPackSortUsed();
        }
        public static IComparer SortItemBackPackAccessory()
        {
            return (IComparer)new ItemBackPackSortAccessory();
        }
        public static IComparer SortItemBackPackWeapon()
        {
            return (IComparer)new ItemBackPackSortWeapon();
        }
        public static IComparer SortItemBackPackArmor()
        {
            return (IComparer)new ItemBackPackSortArmor();
        }
        public static IComparer SortItemBackPackName()
        {
            return (IComparer)new ItemBackPackSortName();
        }
        public static IComparer SortItemBackPackRare()
        {
            return (IComparer)new ItemBackPackSortRare();
        }

        public void CleanUpStatus()
        {
            EffectStatus = false;
        }
    }


    // ソートは指定されたTypeをトップへ。
    // それ以外はUsed,Accessory、Weapon、Armorの順序で並べる。
    // 名前ソートの場合、上記ルールを無視して名前順とする。
    // Rareソートの場合、Rare順で、同一Rareの場合名前順とする。
    public class ItemBackPackSortUsed : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortAccessory : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortWeapon : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortArmor : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortName : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortRare : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Rare.CompareTo(c2.Rare) == 0)
            {
                return c1.Name.CompareTo(c2.Name);
            }
            else
            {
                return c2.Rare.CompareTo(c1.Rare);
            }
        }
    }
}
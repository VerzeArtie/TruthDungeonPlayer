using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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

        public enum Equipable
        {
            All,
            Ein,
            Lana,
            Verze,
            Ol, // 後編追加
            Kahl, // 後編追加
        }
        public enum RareLevel
        {
            Poor,
            Common,
            Rare,
            Epic,
            Legendary,
        }

        public ItemBackPack(string createName)
        {
            this.Name = createName;

            switch (createName)
            {
                case Database.COMMON_FINE_SWORD: // １階：エリア１：ランダムドロップ
                    description = "そつなく使える剣。攻撃力５～８";
                    minValue = 5;
                    maxValue = 8;
                    cost = 560;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
            }
        }

        protected string name = string.Empty;
        protected string description = string.Empty;
        protected int minValue = 0;
        protected int maxValue = 0;
        protected int cost = 0;
        protected ItemType type = ItemType.None;
        protected Equipable equipablePerson = Equipable.All;
        protected RareLevel rareLevel = RareLevel.Poor;
        protected int buffUpStrength = 0;
        protected int buffUpAgility = 0;
        protected int buffUpIntelligence = 0;
        protected int buffUpStamina = 0;
        protected int buffUpMind = 0;
        protected double amplifyPhysicalAttack = 0.0f; // 後編追加
        protected double amplifyPhysicalDefense = 0.0f; // 後編追加
        protected double amplifyMagicAttack = 0.0f; // 後編追加
        protected double amplifyMagicDefense = 0.0f; // 後編追加
        protected double amplifyBattleSpeed = 0.0f; // 後編追加
        protected double amplifyBattleResponse = 0.0f; // 後編追加
        protected double amplifyPotential = 0.0f; // 後編追加
        protected double amplifyLight = 0.0f; // 後編追加
        protected double amplifyShadow = 0.0f; // 後編追加
        protected double amplifyFire = 0.0f; // 後編追加
        protected double amplifyIce = 0.0f; // 後編追加
        protected double amplifyForce = 0.0f; // 後編追加
        protected double amplifyWill = 0.0f; // 後編追加

        protected double effectValue1 = 0; // 後編追加(最大スキルポイント増加)
        protected double manaCostReduction = 0; // 後編追加(魔法消費軽減)
        protected double manaCostReductionLight = 0; // 後編追加
        protected double manaCostReductionShadow = 0; // 後編追加
        protected double manaCostReductionFire = 0; // 後編追加
        protected double manaCostReductionIce = 0; // 後編追加
        protected double manaCostReductionForce = 0; // 後編追加
        protected double manaCostReductionWill = 0; // 後編追加
        protected double skillCostReduction = 0; // 後編追加（スキル消費軽減）
        protected double skillCostReductionActive = 0; // 後編追加
        protected double skillCostReductionPassive = 0; // 後編追加
        protected double skillCostReductionSoft = 0; // 後編追加
        protected double skillCostReductionHard = 0; // 後編追加
        protected double skillCostReductionTruth = 0; // 後編追加
        protected double skillCostReductionVoid = 0; // 後編追加

        protected bool switchStatus1 = false; // 後編追加（メイズ・キューブの物理/魔法の対象切り替えにつくった値)

        protected string information = string.Empty;
        protected bool useSpecialAbility = false;
        protected bool afterBroken = false; // ジャンク・タリスマン発動時、戦闘終了後にアイテム破棄するために用意したフラグ
        protected bool onlyOnce = false; // デタッチメント・オーブにより、戦闘中に一度しか発動できないために用意したフラグ
        protected string imprintCommand = string.Empty; // 悪魔封じの壺により、キャンセル対象魔法の名前を覚えるために用意した
        protected bool effectStatus = false; // 玉手箱『秋玉』により、死亡時一度だけ蘇生するために用意したフラグ

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        public int MagicMinValue { get; set; } // 後編追加
        public int MagicMaxValue { get; set; } // 後編追加

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public ItemType Type
        {
            get { return type; }
            set { type = value; }
        }
        public Equipable EquipablePerson
        {
            get { return equipablePerson; }
            set { equipablePerson = value; }
        }
        public RareLevel Rare
        {
            get { return rareLevel; }
            set { rareLevel = value; }
        }
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
        public int BuffUpStrength
        {
            get { return buffUpStrength; }
            set { buffUpStrength = value; }
        }
        public int BuffUpAgility
        {
            get { return buffUpAgility; }
            set { buffUpAgility = value; }
        }
        public int BuffUpIntelligence
        {
            get { return buffUpIntelligence; }
            set { buffUpIntelligence = value; }
        }
        public int BuffUpStamina
        {
            get { return buffUpStamina; }
            set { buffUpStamina = value; }
        }
        public int BuffUpMind
        {
            get { return buffUpMind; }
            set { buffUpMind = value; }
        }
        // s 後編追加
        public double AmplifyPhysicalAttack
        {
            get { return amplifyPhysicalAttack; }
            set { amplifyPhysicalAttack = value; }
        }
        public double AmplifyPhysicalDefense
        {
            get { return amplifyPhysicalDefense; }
            set { amplifyPhysicalDefense = value; }
        }
        public double AmplifyMagicAttack
        {
            get { return amplifyMagicAttack; }
            set { amplifyMagicAttack = value; }
        }
        public double AmplifyMagicDefense
        {
            get { return amplifyMagicDefense; }
            set { amplifyMagicDefense = value; }
        }
        public double AmplifyBattleSpeed
        {
            get { return amplifyBattleSpeed; }
            set { amplifyBattleSpeed = value; }
        }
        public double AmplifyBattleResponse
        {
            get { return amplifyBattleResponse; }
            set { amplifyBattleResponse = value; }
        }
        public double AmplifyPotential
        {
            get { return amplifyPotential; }
            set { amplifyPotential = value; }
        }
        public double AmplifyLight
        {
            get { return amplifyLight; }
            set { amplifyLight = value; }
        }
        public double AmplifyShadow
        {
            get { return amplifyShadow; }
            set { amplifyShadow = value; }
        }
        public double AmplifyFire
        {
            get { return amplifyFire; }
            set { amplifyFire = value; }
        }
        public double AmplifyIce
        {
            get { return amplifyIce; }
            set { amplifyIce = value; }
        }
        public double AmplifyForce
        {
            get { return amplifyForce; }
            set { amplifyForce = value; }
        }
        public double AmplifyWill
        {
            get { return amplifyWill; }
            set { amplifyWill = value; }
        }
        public double EffectValue1
        {
            get { return effectValue1; }
            set { effectValue1 = value; }
        }
        public double ManaCostReduction
        {
            get { return manaCostReduction; }
            set { manaCostReduction = value; }
        }

        public double ManaCostReductionLight
        {
            get { return manaCostReductionLight; }
            set { manaCostReductionLight = value; }
        }
        public double ManaCostReductionShadow
        {
            get { return manaCostReductionShadow; }
            set { manaCostReductionShadow = value; }
        }
        public double ManaCostReductionFire
        {
            get { return manaCostReductionFire; }
            set { manaCostReductionFire = value; }
        }
        public double ManaCostReductionIce
        {
            get { return manaCostReductionIce; }
            set { manaCostReductionIce = value; }
        }
        public double ManaCostReductionForce
        {
            get { return manaCostReductionForce; }
            set { manaCostReductionForce = value; }
        }
        public double ManaCostReductionWill
        {
            get { return manaCostReductionWill; }
            set { manaCostReductionWill = value; }
        }

        public double SkillCostReduction
        {
            get { return skillCostReduction; }
            set { skillCostReduction = value; }
        }
        public double SkillCostReductionActive
        {
            get { return skillCostReductionActive; }
            set { skillCostReductionActive = value; }
        }
        public double SkillCostReductionPassive
        {
            get { return skillCostReductionPassive; }
            set { skillCostReductionPassive = value; }
        }
        public double SkillCostReductionSoft
        {
            get { return skillCostReductionSoft; }
            set { skillCostReductionSoft = value; }
        }
        public double SkillCostReductionHard
        {
            get { return skillCostReductionHard; }
            set { skillCostReductionHard = value; }
        }
        public double SkillCostReductionTruth
        {
            get { return skillCostReductionTruth; }
            set { skillCostReductionTruth = value; }
        }
        public double SkillCostReductionVoid
        {
            get { return skillCostReductionVoid; }
            set { skillCostReductionVoid = value; }
        }

        public bool SwitchStatus1
        {
            get { return switchStatus1; }
            set { switchStatus1 = value; }
        }
        // e 後編追加

        public string Information
        {
            get { return information; }
            set { information = value; }
        }
        // s 後編追加
        public int ResistFire { get; set; }
        public int ResistIce { get; set; }
        public int ResistLight { get; set; }
        public int ResistShadow { get; set; }
        public int ResistForce { get; set; }
        public int ResistWill { get; set; }
        // e 後編追加
        public bool UseSpecialAbility
        {
            get { return useSpecialAbility; }
            set { useSpecialAbility = value; }
        }
        // s 後編追加
        public bool AfterBroken
        {
            get { return afterBroken; }
            set { afterBroken = value; }
        }
        public bool EffectStatus
        {
            get { return effectStatus; }
            set { effectStatus = value; }
        }
        public bool OnlyOnce
        {
            get { return onlyOnce; }
            set { onlyOnce = value; }
        }
        public string ImprintCommand
        {
            get { return imprintCommand; }
            set { imprintCommand = value; }
        }
        // e 後編追加

        // s 後編追加
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
        // e 後編追加

        public int UseIt()
        {
            System.Random rd = new System.Random(DateTime.Now.Millisecond);
            return rd.Next(minValue, maxValue + 1);
        }

        protected void AdditionalDescription(ItemType s_type)
        {
            this.type = s_type;
            if (s_type == ItemType.Material_Equip)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_EQUIP_MATERIAL);
            }
            else if (s_type == ItemType.Material_Food)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_FOOD_MATERIAL);
            }
            else if (s_type == ItemType.Material_Potion)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_POTION_MATERIAL);
            }
            else if (s_type == ItemType.Useless || type == ItemType.None)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_SELL_ONLY);
            }
        }
        public int PhysicalAttackMinValue { get; set; }
        public int PhysicalAttackMaxValue { get; set; }
        public int PhysicalDefenseMinValue { get; set; }
        public int PhysicalDefenseMaxValue { get; set; }
        public int MagicAttackMinValue { get; set; }
        public int MagicAttackMaxValue { get; set; }
        public int MagicDefenseMinValue { get; set; }
        public int MagicDefenseMaxValue { get; set; }

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
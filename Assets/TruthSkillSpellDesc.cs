using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class TruthSkillSpellDesc : MotherForm
    {
        public Image commandImage;
        public Text commandName;
        public Text commandName_En;
        public Text commandCost;
        public Text commandTarget;
        public Text commandTiming;
        public Text commandAttribute1;
        public Text commandAttribute2;
        public Text commandDescription;
        public Image pbWeapon;
        public Image pbStrength;
        public Image pbAgility;
        public Image pbIntelligence;
        public Image pbStamina;
        public Image pbMind;
        public Text mainMessage;
        public override void Start()
        {
            base.Start();

            this.commandName.text = TruthActionCommand.ConvertToJapanese(GroundOne.SpellSkillName);
            this.commandName_En.text = GroundOne.SpellSkillName;
            commandImage.sprite = Resources.Load<Sprite>(GroundOne.SpellSkillName);
            commandCost.text = TruthActionCommand.GetCost(GroundOne.SpellSkillName).ToString();
            switch (TruthActionCommand.GetTargetType(GroundOne.SpellSkillName))
            {
                case TruthActionCommand.TargetType.AllMember:
                    commandTarget.text = "敵味方全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    commandTarget.text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    commandTarget.text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    commandTarget.text = "敵単体／味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    commandTarget.text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    commandTarget.text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    commandTarget.text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    commandTarget.text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    commandTarget.text = "自分";
                    break;
            }

            // 内在　Immanence Spirit
            // 外在  
            string ARCHTYPE_PHISICAL = "開放型";
            string ARCHTYPE_INNER_MENTAL = "内在型";

            string SKILL_ATTRIBUTE_TEXT = "スキル属性";
            string SPELL_ATTRIBUTE_TEXT = "スペル属性";

            string ATTRIBUTE_LIGHT = "--- 聖 ---";
            string ATTRIBUTE_SHADOW = "--- 闇 ---";
            string ATTRIBUTE_FIRE = "--- 火 ---";
            string ATTRIBUTE_ICE = "--- 水 ---";
            string ATTRIBUTE_FORCE = "--- 理 ---";
            string ATTRIBUTE_WILL = "--- 空 ---";

            string ATTRIBUTE_LIGHT_SHADOW = " --- 聖/闇 ---";
            string ATTRIBUTE_LIGHT_FIRE = " --- 聖/火 ---";
            string ATTRIBUTE_LIGHT_ICE = " --- 聖/水 ---";
            string ATTRIBUTE_LIGHT_FORCE = " --- 聖/理 ---";
            string ATTRIBUTE_LIGHT_WILL = " --- 聖/空 ---";
            string ATTRIBUTE_SHADOW_FIRE = " --- 闇/火 ---";
            string ATTRIBUTE_SHADOW_ICE = " --- 闇/水 ---";
            string ATTRIBUTE_SHADOW_FORCE = " --- 闇/理 ---";
            string ATTRIBUTE_SHADOW_WILL = " --- 闇/空 ---";
            string ATTRIBUTE_FIRE_ICE = " --- 火/水 ---";
            string ATTRIBUTE_FIRE_FORCE = " --- 火/理 ---";
            string ATTRIBUTE_FIRE_WILL = " --- 火/空 ---";
            string ATTRIBUTE_ICE_FORCE = " --- 水/理 ---";
            string ATTRIBUTE_ICE_WILL = " --- 水/空 ---";
            string ATTRIBUTE_FORCE_WILL = " --- 理/空 ---";

            string ATTRIBUTE_ACTIVE = "--- 動 ---";
            string ATTRIBUTE_PASSIVE = "--- 静 ---";
            string ATTRIBUTE_SOFT = "--- 柔 ---";
            string ATTRIBUTE_HARD = "--- 剛 ---";
            string ATTRIBUTE_TRUTH = "--- 心眼 ---";
            string ATTRIBUTE_VOID = "--- 無心 ---";

            string ATTRIBUTE_ACTIVE_PASSIVE = "--- 動/静 ---";
            string ATTRIBUTE_ACTIVE_SOFT = "--- 動/柔 ---";
            string ATTRIBUTE_ACTIVE_HARD = "--- 動/剛 ---";
            string ATTRIBUTE_ACTIVE_TRUTH = "--- 動/心眼 ---";
            string ATTRIBUTE_ACTIVE_VOID = "--- 動/無心 ---";
            string ATTRIBUTE_PASSIVE_SOFT = "--- 静/柔 ---";
            string ATTRIBUTE_PASSIVE_HARD = "--- 静/剛 ---";
            string ATTRIBUTE_PASSIVE_TRUTH = "--- 静/心眼 ---";
            string ATTRIBUTE_PASSIVE_VOID = "--- 静/無心 ---";
            string ATTRIBUTE_SOFT_HARD = "--- 柔/剛 ---";
            string ATTRIBUTE_SOFT_TRUTH = "--- 柔/心眼 ---";
            string ATTRIBUTE_SOFT_VOID = "--- 柔/無心 ---";
            string ATTRIBUTE_HARD_TRUTH = "--- 剛/心眼 ---";
            string ATTRIBUTE_HARD_VOID = "--- 剛/無心 ---";
            string ATTRIBUTE_TRUTH_VOID = "--- 心眼/無心 ---";

            string ATTRIBUTE_NONE = "--- 無属性 ---";

            if (TruthActionCommand.GetTimingType(GroundOne.SpellSkillName) == TruthActionCommand.TimingType.Instant)
            {
                commandTiming.text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(GroundOne.SpellSkillName) == TruthActionCommand.TimingType.Normal)
            {
                commandTiming.text = "ノーマル";

            }
            else if (TruthActionCommand.GetTimingType(GroundOne.SpellSkillName) == TruthActionCommand.TimingType.Sorcery)
            {
                commandTiming.text = "ソーサリー";
            }
            commandDescription.text = TruthActionCommand.GetDescription(GroundOne.SpellSkillName);

            if (TruthActionCommand.CheckPlayerActionFromString(GroundOne.SpellSkillName) == PlayerAction.UseSpell)
            {
                commandAttribute1.text = SPELL_ATTRIBUTE_TEXT;
                switch (TruthActionCommand.GetMagicType(GroundOne.SpellSkillName))
                {
                    case TruthActionCommand.MagicType.Light:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT;
                        break;
                    case TruthActionCommand.MagicType.Shadow:
                        commandAttribute2.Text = ATTRIBUTE_SHADOW;
                        break;
                    case TruthActionCommand.MagicType.Fire:
                        commandAttribute2.Text = ATTRIBUTE_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Ice:
                        commandAttribute2.Text = ATTRIBUTE_ICE;
                        break;
                    case TruthActionCommand.MagicType.Force:
                        commandAttribute2.Text = ATTRIBUTE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Will:
                        commandAttribute2.Text = ATTRIBUTE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Light_Shadow:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT_SHADOW;
                        break;
                    case TruthActionCommand.MagicType.Light_Fire:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Light_Ice:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT_ICE;
                        break;
                    case TruthActionCommand.MagicType.Light_Force:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Light_Will:
                        commandAttribute2.Text = ATTRIBUTE_LIGHT_WILL;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Fire:
                        commandAttribute2.Text = ATTRIBUTE_SHADOW_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Ice:
                        commandAttribute2.Text = ATTRIBUTE_SHADOW_ICE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Force:
                        commandAttribute2.Text = ATTRIBUTE_SHADOW_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Will:
                        commandAttribute2.Text = ATTRIBUTE_SHADOW_WILL;
                        break;
                    case TruthActionCommand.MagicType.Fire_Ice:
                        commandAttribute2.Text = ATTRIBUTE_FIRE_ICE;
                        break;
                    case TruthActionCommand.MagicType.Fire_Force:
                        commandAttribute2.Text = ATTRIBUTE_FIRE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Fire_Will:
                        commandAttribute2.Text = ATTRIBUTE_FIRE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Ice_Force:
                        commandAttribute2.Text = ATTRIBUTE_ICE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Ice_Will:
                        commandAttribute2.Text = ATTRIBUTE_ICE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Force_Will:
                        commandAttribute2.Text = ATTRIBUTE_FORCE_WILL;
                        break;
                    default:
                        commandAttribute2.Text = ATTRIBUTE_NONE;
                        break;
                }
            }
            else
            {
                commandAttribute1.text = SKILL_ATTRIBUTE_TEXT;
                switch (TruthActionCommand.GetSkillType(GroundOne.SpellSkillName))
                {
                    case TruthActionCommand.SkillType.Active:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE;
                        break;
                    case TruthActionCommand.SkillType.Passive:
                        commandAttribute2.Text = ATTRIBUTE_PASSIVE;
                        break;
                    case TruthActionCommand.SkillType.Soft:
                        commandAttribute2.Text = ATTRIBUTE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Hard:
                        commandAttribute2.Text = ATTRIBUTE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Truth:
                        commandAttribute2.Text = ATTRIBUTE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Void:
                        commandAttribute2.Text = ATTRIBUTE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Active_Passive:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE_PASSIVE;
                        break;
                    case TruthActionCommand.SkillType.Active_Soft:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Active_Hard:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Active_Truth:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Active_Void:
                        commandAttribute2.Text = ATTRIBUTE_ACTIVE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Passive_Soft:
                        commandAttribute2.Text = ATTRIBUTE_PASSIVE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Passive_Hard:
                        commandAttribute2.Text = ATTRIBUTE_PASSIVE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Passive_Truth:
                        commandAttribute2.Text = ATTRIBUTE_PASSIVE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Passive_Void:
                        commandAttribute2.Text = ATTRIBUTE_PASSIVE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Soft_Hard:
                        commandAttribute2.Text = ATTRIBUTE_SOFT_HARD;
                        break;
                    case TruthActionCommand.SkillType.Soft_Truth:
                        commandAttribute2.Text = ATTRIBUTE_SOFT_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Soft_Void:
                        commandAttribute2.Text = ATTRIBUTE_SOFT_VOID;
                        break;
                    case TruthActionCommand.SkillType.Hard_Truth:
                        commandAttribute2.Text = ATTRIBUTE_HARD_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Hard_Void:
                        commandAttribute2.Text = ATTRIBUTE_HARD_VOID;
                        break;
                    case TruthActionCommand.SkillType.Truth_Void:
                        commandAttribute2.Text = ATTRIBUTE_TRUTH_VOID;
                        break;
                    default:
                        commandAttribute2.Text = ATTRIBUTE_NONE;
                        break;
                }
            }

            mainMessage.text = GroundOne.playerName + "は" + TruthActionCommand.ConvertToJapanese(GroundOne.SpellSkillName) + "を習得した";

            // todo 影響因子の表示内容が全て網羅出来ているとは思えない。要見直し
            if (TruthActionCommand.IsDamage(GroundOne.SpellSkillName))
            {
                if (TruthActionCommand.CheckPlayerActionFromString(GroundOne.SpellSkillName) == PlayerAction.UseSpell)
                {
                    if (GroundOne.SpellSkillName == Database.WORD_OF_POWER)
                    {
                        SetupEffectFactor(false, true, false, false, false, false);
                    }
                    else
                    {
                        SetupEffectFactor(false, false, false, true, false, false);
                    }
                }
                else
                {
                    if (GroundOne.SpellSkillName == Database.PSYCHIC_WAVE)
                    {
                        SetupEffectFactor(false, false, false, true, false, false);
                    }
                    else
                    {
                        SetupEffectFactor(false, true, false, false, false, false);
                    }
                }
            }

            switch (GroundOne.SpellSkillName)
            {
                case Database.STRAIGHT_SMASH:
                    SetupEffectFactor(false, true, true, false, false, false);
                    break;
                case Database.INNER_INSPIRATION:
                    SetupEffectFactor(false, false, false, false, false, mind);
                    break;
                case Database.WORD_OF_LIFE:
                    SetupEffectFactor(false, false, false, false, false, mind);
                    break;
                case Database.ENIGMA_SENSE:
                    SetupEffectFactor(false, true, true, true, false, false);
                    break;
                case Database.BLACK_CONTRACT:
                    SetupEffectFactor(false, false, false, false, false, mind);
                    break;
            }
        }

        private SetupEffectFactor(bool weapon, bool strength, bool agility, bool intelligence, bool stamina, bool mind)
        {
            if (weapon) { pbWeapon.sprite = Resources.Load<Sprite>(Database.WeaponIcon); }
            if (strength) { pbStrength.sprite = Resources.Load<Sprite>(Database.StrengthIcon); }
            if (agility) { pbAgility.sprite = Resources.Load<Sprite>(Database.AgilityhIcon); }
            if (intelligence) { pbIntelligence.sprite = Resources.Load<Sprite>(Database.IntelligenceIcon); }
            if (stamina) { pbStamina.sprite = Resources.Load<Sprite>(Database.StaminaIcon); }
            if (mind) { pbMind.sprite = Resources.Load<Sprite>(Database.MindIcon); }
        }

        public void Close_Clock()
        {
            SceneDimension.Back();
        }
    }
}

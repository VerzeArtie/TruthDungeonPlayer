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

            pbWeapon.sprite = Resources.Load<Sprite>(Database.WeaponIcon);
            pbStrength.sprite = Resources.Load<Sprite>(Database.StrengthIcon);
            pbAgility.sprite = Resources.Load<Sprite>(Database.AgilityhIcon);
            pbIntelligence.sprite = Resources.Load<Sprite>(Database.IntelligenceIcon);
            pbStamina.sprite = Resources.Load<Sprite>(Database.StaminaIcon);
            pbMind.sprite = Resources.Load<Sprite>(Database.MindIcon);

            this.commandName.text = TruthActionCommand.ConvertToJapanese(GroundOne.SpellSkillName);
            this.commandName_En.text = GroundOne.SpellSkillName;
            commandImage.sprite = Resources.Load<Sprite>(GroundOne.SpellSkillName);
            commandCost.text = TruthActionCommand.GetCost(GroundOne.SpellSkillName).ToString();
            switch (TruthActionCommand.GetTargetType(GroundOne.SpellSkillName))
            {
                case TruthActionCommand.TargetType.AllMember:
                    commandTarget.text = "敵味方\r\n全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    commandTarget.text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    commandTarget.text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    commandTarget.text = "敵単体\r\n味方単体";
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

        }

        void Update()
        {

        }
    }
}

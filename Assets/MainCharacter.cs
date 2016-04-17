using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonPlayer
{
    public class MainCharacter : MonoBehaviour
    {
        public enum PlayerAction
        {
            None = 0, // 待機（なにもしない）
            NormalAttack = 1, // 通常攻撃
            UseSkill = 2, // スキル使用
            RunAway = 3, // 逃げる
            UseItem = 4, // アイテム使用
            UseSpell = 5, // 魔法使用
            Defense = 6, // 防御
            SpecialSkill = 7, // 後編追加 敵専用スキル
            Charge = 8, // 後編追加 杖でためる
            Archetype = 9, // 後編追加 元核使用
        }

        public enum PlayerStance
        {
            None = 0,
            FrontOffence = 1, // アイン、オル、ラナ、ヴェルゼ
            FrontDefense = 2, // アイン、オル
            BackOffence = 3, // ラナ、ヴェルゼ
            BackSupport = 4, // アイン、ラナ、オル、ヴェルゼ
            AllRounder = 5, // ヴェルゼ専用
        }

        public enum AdditionalSpellType
        {
            None, // アイン、複合魔法属性を未選択
            Type1, // 例：アイン「水」を追加選択
            Type2, // 例：アイン「闇」を追加選択
        }

        public enum AdditionalSkillType
        {
            None, // アイン、複合スキル属性を未選択
            Type1, // 例：アイン「静」を追加選択
            Type2, // 例：アイン「柔」を追加選択
        }

        // basic parameter
        public string FirstName = string.Empty;
        protected string fullName = string.Empty;
        protected int baseStrength = 1;
        protected int baseAgility = 1;
        protected int baseIntelligence = 1;
        protected int baseStamina = 1;
        protected int baseMind = 1;

        protected int baseResistFire = 0; // 後編追加
        protected int baseResistIce = 0; // 後編追加
        protected int baseResistLight = 0; // 後編追加
        protected int baseResistShadow = 0; // 後編追加
        protected int baseResistForce = 0; // 後編追加
        protected int baseResistWill = 0; // 後編追加

        protected int level = 1;
        protected int experience = 0;
        protected int baseLife = 50;
        protected int currentLife = 50;
        protected int baseSkillPoint = 100;
        protected int currentSkillPoint = 100;
        protected int baseInstantPoint = 1000; // 後編追加
        protected int baseSpecialInstant = 20000; // 後編追加
        protected double currentInstantPoint = 0; // 後編追加// 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
        protected double currentSpecialInstant = 0; // 後編追加
        protected int gold = 0;
        protected PlayerStance stance = PlayerStance.None; // 後編追加
        protected AdditionalSpellType additionSpellType = AdditionalSpellType.None; // 後編追加
        protected AdditionalSkillType additionSkillType = AdditionalSkillType.None; // 後編追加

        protected int baseMana = 80;
        protected int currentMana = 80;
        protected bool availableSkill = false;
        protected bool availableMana = false;
        protected bool availableArchitect = false;

        // core parameter
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        public int Strength
        {
            get { return baseStrength; }
            set { baseStrength = value; }
        }
        public int Agility
        {
            get { return baseAgility; }
            set { baseAgility = value; }
        }
        public int Intelligence
        {
            get { return baseIntelligence; }
            set { baseIntelligence = value; }
        }
        public int Stamina
        {
            get { return baseStamina; }
            set { baseStamina = value; }
        }
        public int Mind
        {
            get
            {
                if (((this.accessory != null) && (this.accessory.Name == Database.RARE_VOID_HYMNSONIA)) ||
                    ((this.accessory2 != null) && (this.accessory2.Name == Database.RARE_VOID_HYMNSONIA)))
                {
                    return 1;
                }
                else
                {
                    return baseMind;
                }
            }
            set { baseMind = value; }
        }
        protected string currentArchetypeName = string.Empty; // 後編追加
        public string CurrentArchetypeName
        {
            get { return currentArchetypeName; }
            set { currentArchetypeName = value; }
        }

        public int BaseLife
        {
            get { return baseLife; }
            set { baseLife = value; }
        }
        public int MaxLife
        {
            get
            {
                int result = baseLife;
                result += TotalStamina * 10;
                result += CurrentBlackElixirValue;
                return result;
            }
        }
        public int BaseMana
        {
            get { return baseMana; }
            set { baseMana = value; }
        }
        public int MaxMana
        {
            get { return baseMana + TotalIntelligence * 10; } // 後編編集
        }
        public int BaseSkillPoint
        {
            get { return baseSkillPoint; }
            set { baseSkillPoint = value; }
        }
        public int MaxSkillPoint
        {
            get
            {
                int result = baseSkillPoint;
                if (this.accessory != null)
                {
                    result += (int)this.accessory.EffectValue1;
                }
                if (this.accessory2 != null)
                {
                    result += (int)this.accessory2.EffectValue1;
                }

                if (this.currentSkillPoint > result) this.currentSkillPoint = result;
                return result; 
            }
        }
        public int BaseInstantPoint
        {
            get { return baseInstantPoint; }
            set { baseInstantPoint = value; }
        }
        public int MaxInstantPoint
        {
            get
            {
                if (baseInstantPoint < 1000) baseInstantPoint = 1000;
                return baseInstantPoint;
            }
        }

        
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Exp
        {
            get { return experience; }
            set 
            {
                if (value <= 0)
                {
                    experience = 0;
                }
                else
                {
                    experience = value;
                }
            }
        }
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public int CurrentLife
        {
            get { return currentLife; }
            set
            {
                if (value >= MaxLife)
                {
                    value = MaxLife;
                }
                if (value <= 0)
                {
                    value = 0;
                }
                currentLife = value;
            }
        }
        public int CurrentMana
        {
            get { return currentMana; }
            set
            {
                if (value >= MaxMana)
                {
                    value = MaxMana;
                }
                if (value <= 0)
                {
                    value = 0;
                }
                currentMana = value;
            }
        }
        public int CurrentSkillPoint
        {
            get { return currentSkillPoint; }
            set
            {
                if (value >= MaxSkillPoint)
                {
                    value = MaxSkillPoint;
                }
                currentSkillPoint = value;
            }
        }
        // s 後編追加
        public double CurrentInstantPoint
        {
            get { return currentInstantPoint; }
            set
            {
                if (value >= MaxInstantPoint)
                {
                    value = MaxInstantPoint;
                }
                currentInstantPoint = value;
            }
        }
        public double CurrentSpecialInstant
        {
            get { return currentSpecialInstant; }
            set
            {
                if (value >= MaxSpecialInstant)
                {
                    value = MaxSpecialInstant;
                }
                currentSpecialInstant = value;
            }
        }
        public int BaseSpecialInstant
        {
            get { return baseSpecialInstant; }
            set { baseSpecialInstant = value; }
        }
        public int MaxSpecialInstant
        {
            get
            {
                if (baseSpecialInstant < 1) baseSpecialInstant = 1;
                return baseSpecialInstant;
            }
        }        
        public JobClass.Job Job { get; set; } // add unity

        public int TotalResistLight
        {
            get
            {
                int result = baseResistLight;
                if (this.CurrentResistLightUp > 0) result += CurrentResistLightUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistLight;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistLight;
                if (this.MainArmor != null) result += this.MainArmor.ResistLight;
                if (this.Accessory != null) result += this.Accessory.ResistLight;
                if (this.Accessory2 != null) result += this.Accessory2.ResistLight;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistShadow
        {
            get
            {
                int result = baseResistShadow;
                if (this.CurrentResistShadowUp > 0) result += CurrentResistShadowUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistShadow;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistShadow;
                if (this.MainArmor != null) result += this.MainArmor.ResistShadow;
                if (this.Accessory != null) result += this.Accessory.ResistShadow;
                if (this.Accessory2 != null) result += this.Accessory2.ResistShadow;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistFire
        {
            get
            {
                int result = baseResistFire;
                if (this.CurrentResistFireUp > 0) result += CurrentResistFireUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistFire;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistFire;
                if (this.MainArmor != null) result += this.MainArmor.ResistFire;
                if (this.Accessory != null) result += this.Accessory.ResistFire;
                if (this.Accessory2 != null) result += this.Accessory2.ResistFire;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistIce
        {
            get
            {
                int result = baseResistIce;
                if (this.CurrentResistIceUp > 0) result += CurrentResistIceUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistIce;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistIce;
                if (this.MainArmor != null) result += this.MainArmor.ResistIce;
                if (this.Accessory != null) result += this.Accessory.ResistIce;
                if (this.Accessory2 != null) result += this.Accessory2.ResistIce;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistForce
        {
            get
            {
                int result = baseResistForce;
                if (this.CurrentResistForceUp > 0) result += CurrentResistForceUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistForce;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistForce;
                if (this.MainArmor != null) result += this.MainArmor.ResistForce;
                if (this.Accessory != null) result += this.Accessory.ResistForce;
                if (this.Accessory2 != null) result += this.Accessory2.ResistForce;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistWill
        {
            get
            {
                int result = baseResistWill;
                if (this.CurrentResistWillUp > 0) result += CurrentResistWillUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistWill;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistWill;
                if (this.MainArmor != null) result += this.MainArmor.ResistWill;
                if (this.Accessory != null) result += this.Accessory.ResistWill;
                if (this.Accessory2 != null) result += this.Accessory2.ResistWill;
                if (result <= 0) result = 0;
                return result;
            }
        }

        // buff up(+)
        protected int buffStrength_Food = 0;
        protected int buffAgility_Food = 0;
        protected int buffIntelligence_Food = 0;
        protected int buffStamina_Food = 0;
        protected int buffMind_Food = 0;

        public int BuffStrength_Food
        {
            get { return buffStrength_Food; }
            set { buffStrength_Food = value; }
        }
        public int BuffAgility_Food
        {
            get { return buffAgility_Food; }
            set { buffAgility_Food = value; }
        }
        public int BuffIntelligence_Food
        {
            get { return buffIntelligence_Food; }
            set { buffIntelligence_Food = value; }
        }
        public int BuffStamina_Food
        {
            get { return buffStamina_Food; }
            set { buffStamina_Food = value; }
        }
        public int BuffMind_Food
        {
            get { return buffMind_Food; }
            set { buffMind_Food = value; }
        }
        // 負の影響効果（ボス専用、対象行動されると失敗しスタンになる）
        protected int currentPreStunning = 0;
        // 負の影響効果
        protected int currentStunning = 0;
        protected int currentSilence = 0;
        protected int currentPoison = 0;
        protected int currentTemptation = 0;
        protected int currentFrozen = 0;
        protected int currentParalyze = 0;
        protected int currentNoResurrection = 0; // 後編編集（スペル修正）
        protected int currentSlow = 0; // 後編追加
        protected int currentBlind = 0; // 後編追加
        protected int currentSlip = 0; // 後編追加
        protected int currentNoGainLife = 0; // 後編追加
        // 正の影響効果
        protected int currentBlinded = 0; // 後編追加
        protected int currentSpeedBoost = 0; // 後編追加 (一時的にスピードUPさせる靴の効果を期待したもの）
        protected int currentChargeCount = 0; // 後編追加
        protected int currentPhysicalChargeCount = 0; // 後編追加

        // 負の影響効果の重ねがけ効果
        protected int currentPoisonValue = 0; // 後編追加
        protected int currentConcussiveHitValue = 0; // 後編追加
        protected int currentOnslaughtHitValue = 0; // 後編追加
        protected int currentImpulseHitValue = 0; // 後編追加

        // 正の影響効果の重ねがけ効果
        protected int currentSkyShieldValue = 0; // 後編追加
        protected int currentStaticBarrierValue = 0; // 後編追加
        protected int currentStanceOfMysticValue = 0; // 後編追加

        // 武器特有の重ねがけ効果
        protected int currentFeltusValue = 0; // 後編追加
        protected int currentJuzaPhantasmalValue = 0; // 後編追加
        protected int currentEternalFateRingValue = 0; // 後編追加
        protected int currentLightServantValue = 0; // 後編追加
        protected int currentShadowServantValue = 0; // 後編追加
        protected int currentAdilBlueBurnValue = 0; // 後編追加
        protected int currentMazeCubeValue = 0; // 後編追加

        // 消耗品特有の重ねがけ効果
        protected int currentBlackElixirValue = 0; // 後編追加

        // 最終戦闘のライフカウント
        protected int currentLifeCountValue = 0; // 後編追加

        // 負の影響効果（ボス専用、対象行動されると失敗しスタンになる）
        public int CurrentPreStunning = 0;
        // 負の影響効果
        public int CurrentStunning = 0;
        public int CurrentSilence = 0;
        public int CurrentPoison = 0;
        public int CurrentTemptation = 0;
        public int CurrentFrozen = 0;
        public int CurrentParalyze = 0;
        public int CurrentNoResurrection = 0; // 後編編集（スペル修正）
        public int CurrentSlow = 0; // 後編追加
        public int CurrentBlind = 0; // 後編追加
        public int CurrentSlip = 0; // 後編追加
        public int CurrentNoGainLife = 0; // 後編追加
        // 正の影響効果
        public int CurrentBlinded = 0; // 後編追加
        public int CurrentSpeedBoost = 0; // 後編追加 (一時的にスピードUPさせる靴の効果を期待したもの）
        public int CurrentChargeCount = 0; // 後編追加
        public int CurrentPhysicalChargeCount = 0; // 後編追加

        public int ResistFire { get; set; }
        public int ResistIce { get; set; }
        public int ResistLight { get; set; }
        public int ResistShadow { get; set; }
        public int ResistForce { get; set; }
        public int ResistWill { get; set; }

        // Debuff耐性
        protected bool battleResistStun = false; // 後編追加
        protected bool battleResistSilence = false; // 後編追加
        protected bool battleResistPoison = false; // 後編追加
        protected bool battleResistTemptation = false; // 後編追加
        protected bool battleResistFrozen = false; // 後編追加
        protected bool battleResistParalyze = false; // 後編追加
        protected bool battleResistSlow = false; // 後編追加
        protected bool battleResistBlind = false; // 後編追加
        protected bool battleResistSlip = false; // 後編追加
        protected bool battleResistNoResurrection = false; // 後編追加

        public bool CheckResistPhysicalAttackDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistPhysicalDefenseDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistMagicAttackDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistMagicDefenseDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistBattleSpeedDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistBattleResponseDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistPotentialDown
        {
            get
            {
                if (this.CurrentColoressAntidote > 0) return true;
                return false;
            }
        }

        public bool ResistStun
        {
            get { return battleResistStun; }
            set { battleResistStun = value; }
        }
        public bool ResistSilence
        {
            get { return battleResistSilence; }
            set { battleResistSilence = value; }
        }
        public bool ResistPoison
        {
            get { return battleResistPoison; }
            set { battleResistPoison = value; }
        }
        public bool ResistTemptation
        {
            get { return battleResistTemptation; }
            set { battleResistTemptation = value; }
        }
        public bool ResistFrozen
        {
            get { return battleResistFrozen; }
            set { battleResistFrozen = value; }
        }
        public bool ResistParalyze
        {
            get { return battleResistParalyze; }
            set { battleResistParalyze = value; }
        }
        public bool ResistNoResurrection
        {
            get { return battleResistNoResurrection; }
            set { battleResistNoResurrection = value; }
        }
        public bool ResistSlow
        {
            get { return battleResistSlow; }
            set { battleResistSlow = value; }
        }
        public bool ResistBlind
        {
            get { return battleResistBlind; }
            set { battleResistBlind = value; }
        }
        public bool ResistSlip
        {
            get { return battleResistSlip; }
            set { battleResistSlip = value; }
        }

        // s 後編追加
        // 以下、ボス戦など敵が汎用的にパワーアップしようとして作成したもの
        public int CurrentPhysicalAttackUp = 0;
        public int CurrentPhysicalAttackUpValue = 0;
        public int CurrentPhysicalAttackDown = 0;
        public int CurrentPhysicalAttackDownValue = 0;

        public int CurrentPhysicalDefenseUp = 0;
        public int CurrentPhysicalDefenseUpValue = 0;
        public int CurrentPhysicalDefenseDown = 0;
        public int CurrentPhysicalDefenseDownValue = 0;

        public int CurrentMagicAttackUp = 0;
        public int CurrentMagicAttackUpValue = 0;
        public int CurrentMagicAttackDown = 0;
        public int CurrentMagicAttackDownValue = 0;

        public int CurrentMagicDefenseUp = 0;
        public int CurrentMagicDefenseUpValue = 0;
        public int CurrentMagicDefenseDown = 0;
        public int CurrentMagicDefenseDownValue = 0;

        public int CurrentSpeedUp = 0;
        public int CurrentSpeedUpValue = 0;
        public int CurrentSpeedDown = 0;
        public int CurrentSpeedDownValue = 0;

        public int CurrentReactionUp = 0;
        public int CurrentReactionUpValue = 0;
        public int CurrentReactionDown = 0;
        public int CurrentReactionDownValue = 0;

        public int CurrentPotentialUp = 0;
        public int CurrentPotentialUpValue = 0;
        public int CurrentPotentialDown = 0;
        public int CurrentPotentialDownValue = 0;

        public int CurrentStrengthUp = 0;
        public int CurrentStrengthUpValue = 0;

        public int CurrentAgilityUp = 0;
        public int CurrentAgilityUpValue = 0;

        public int CurrentIntelligenceUp = 0;
        public int CurrentIntelligenceUpValue = 0;

        public int CurrentStaminaUp = 0;
        public int CurrentStaminaUpValue = 0;

        public int CurrentMindUp = 0;
        public int CurrentMindUpValue = 0;

        public int CurrentLightUp = 0;
        public int CurrentLightUpValue = 0;
        public int CurrentLightDown = 0;
        public int CurrentLightDownValue = 0;

        public int CurrentShadowUp = 0;
        public int CurrentShadowUpValue = 0;
        public int CurrentShadowDown = 0;
        public int CurrentShadowDownValue = 0;

        public int CurrentFireUp = 0;
        public int CurrentFireUpValue = 0;
        public int CurrentFireDown = 0;
        public int CurrentFireDownValue = 0;

        public int CurrentIceUp = 0;
        public int CurrentIceUpValue = 0;
        public int CurrentIceDown = 0;
        public int CurrentIceDownValue = 0;

        public int CurrentForceUp = 0;
        public int CurrentForceUpValue = 0;
        public int CurrentForceDown = 0;
        public int CurrentForceDownValue = 0;

        public int CurrentWillUp = 0;
        public int CurrentWillUpValue = 0;
        public int CurrentWillDown = 0;
        public int CurrentWillDownValue = 0;

        public int CurrentResistLightUp = 0;
        public int CurrentResistLightUpValue = 0;

        public int CurrentResistShadowUp = 0;
        public int CurrentResistShadowUpValue = 0;

        public int CurrentResistFireUp = 0;
        public int CurrentResistFireUpValue = 0;

        public int CurrentResistIceUp = 0;
        public int CurrentResistIceUpValue = 0;

        public int CurrentResistForceUp = 0;
        public int CurrentResistForceUpValue = 0;

        public int CurrentResistWillUp = 0;
        public int CurrentResistWillUpValue = 0;

        public int CurrentAfterReviveHalf = 0; // エグゼキューショナーの死亡直後半分ライフ回復で蘇生
        public int CurrentFireDamage2 = 0; // 業・フレイムスラッシャーの火追加効果ダメージ
        public int CurrentBlackMagic = 0; // デビル・チルドレンの2回魔法BUFF
        public int CurrentChaosDesperate = 0; // カオス・ワーデンの死亡予告
        public int CurrentChaosDesperateValue = 0; // カオス・ワーデンの死亡予告カウント値
        public int CurrentIchinaruHomura = 0; // レギィンアーゼの壱なる焔
        public int CurrentAbyssFire = 0; // レギィンアーゼのアビス・ファイア
        public int CurrentLightAndShadow = 0; // レギィンアーゼのライト・アンド・シャドウ
        public int CurrentEternalDroplet = 0; // レギィンアーゼのエターナルドロップレット
        public int CurrentAusterityMatrixOmega = 0; // レギィンアーゼのアウステリティ・マトリクス・Ω
        public int CurrentVoiceOfAbyss = 0; // レギィンアーゼのヴォイス・オブ・アビス
        public int CurrentAbyssWill = 0; // レギィンアーゼのアビスの意志
        public int CurrentAbyssWillValue = 0; // レギィンアーゼのアビスの意志の累積値
        public int CurrentTheAbyssWall = 0; // レギィンアーゼのアビス防壁

        public int PoolLifeConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった
        public int PoolManaConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった
        public int PoolSkillConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった

        // 以下、複合魔法FlashBlazeから追加ダメージを当てようとして考えたもの
        public int CurrentFlashBlazeCount = 0; // 後編追加
        public int CurrentFlashBlazeFactor = 0; // 後編追加


        public double AmplifyPhysicalAttack = 0.0f;
        public double AmplifyMagicAttack = 0.0f;
        public double AmplifyPhysicalDefense = 0.0f;
        public double AmplifyMagicDefense = 0.0f;
        public double AmplifyBattleSpeed = 0.0f;
        public double AmplifyBattleResponse = 0.0f;
        public double AmplifyPotential = 0.0f;

        protected string reserveBattleCommand = string.Empty; // 後編追加 ターゲットを指定する場合、一時的にバトルコマンドを記憶する
        public string ReserveBattleCommand
        {
            get { return reserveBattleCommand; }
            set { reserveBattleCommand = value; }
        }
        // 動
        // 静
        public int CurrentCounterAttack = 0; // 後編編集
        public int CurrentStanceOfStanding = 0; // 後編編集
        public int CurrentAntiStun = 0;
        public int CurrentStanceOfDeath = 0;
        // 柔
        public int CurrentStanceOfFlow = 0;
        // 剛
        // 心眼
        public int CurrentTruthVision = 0;
        public int CurrentHighEmotionality = 0;
        public int CurrentStanceOfEyes = 0;  // 後編編集
        public int CurrentPainfulInsanity = 0;
        // 無心
        public int CurrentNegate = 0; // 後編編集
        public int CurrentVoidExtraction = 0;
        public int CurrentNothingOfNothingness = 0;
        // 動＋静
        public int CurrentStanceOfDouble = 0;
        // 動＋柔
        public int CurrentSwiftStep = 0;
        public int CurrentVigorSense = 0;
        // 動＋剛
        public int CurrentRisingAura = 0;
        // 動＋心眼
        public int CurrentOnslaughtHit = 0;
        // 動＋無心
        public int CurrentSmoothingMove = 0;
        public int CurrentAscensionAura = 0;
        // 静＋柔
        public int CurrentFutureVision = 0;
        // 静＋剛
        public int CurrentReflexSpirit = 0;
        // 静＋心眼
        public int CurrentConcussiveHit = 0;
        // 静＋無心
        public int CurrentTrustSilence = 0;
        // 柔＋剛
        public int CurrentStanceOfMystic = 0;
        // 柔＋心眼
        public int CurrentNourishSense = 0;
        // 柔＋無心
        public int CurrentImpulseHit = 0;
        // 剛＋心眼
        public int CurrentOneAuthority = 0;
        // 剛＋無心
        public bool CurrentHardestParry = false;
        // 心眼＋無心
        public bool CurrentStanceOfSuddenness = false;

        // 聖
        public int CurrentProtection = 0;
        public int CurrentSaintPower = 0;
        public int CurrentGlory = 0;
        // 闇
        public int CurrentShadowPact = 0;
        public int CurrentBlackContract = 0;
        public int CurrentBloodyVengeance = 0;
        public int CurrentDamnation = 0;
        public int CurrentDamnationFactor = 0;
        // 火
        public int CurrentFlameAura = 0;
        public int CurrentHeatBoost = 0;
        public int CurrentImmortalRave = 0;
        // 水
        public int CurrentAbsorbWater = 0;
        public int CurrentMirrorImage = 0;
        public int CurrentPromisedKnowledge = 0;
        public int CurrentAbsoluteZero = 0;
        // 理
        public int CurrentGaleWind = 0;
        public int CurrentWordOfLife = 0;
        public int CurrentWordOfFortune = 0;
        public int CurrentAetherDrive = 0;
        public int CurrentEternalPresence = 0;
        // 空
        public int CurrentRiseOfImage = 0;
        public int CurrentDeflection = 0;
        public int CurrentOneImmunity = 0;
        public int CurrentTimeStop = 0; // 後編追加
        public bool CurrentTimeStopImmediate = false; // 後編追加

        // s 後編追加
        // 聖＋闇
        public int CurrentPsychicTrance = 0;
        public int CurrentBlindJustice = 0;
        public int CurrentTranscendentWish = 0;
        // 聖＋水
        public int CurrentSkyShield = 0;
        public int CurrentEverDroplet = 0;
        // 聖＋理
        public int CurrentHolyBreaker = 0;
        public int CurrentExaltedField = 0;
        public int CurrentHymnContract = 0;
        // 聖＋空
        public int CurrentStarLightning = 0;
        // 闇＋火
        public int CurrentBlackFire = 0;
        public int CurrentBlazingField = 0;
        public int CurrentBlazingFieldFactor = 0;
        // 闇＋水
        public bool CurrentDeepMirror = false;
        // 闇＋理
        public int CurrentWordOfMalice = 0;
        public int CurrentSinFortune = 0;
        // 闇＋空
        public int CurrentDarkenField = 0;
        public int CurrentEclipseEnd = 0;
        // 火＋水
        public int CurrentFrozenAura = 0;
        // 火＋理
        public int CurrentEnrageBlast = 0;
        public int CurrentSigilOfHomura = 0;
        // 火＋空
        public int CurrentImmolate = 0;
        public int CurrentPhantasmalWind = 0;
        public int CurrentRedDragonWill = 0;
        // 水＋理
        public int CurrentStaticBarrier = 0;
        public int CurrentAusterityMatrix = 0;
        // 水＋空
        public int CurrentBlueDragonWill = 0;
        // 理＋空
        public int CurrentSeventhMagic = 0;
        public int CurrentParadoxImage = 0;
        // e 後編追加

        // 集中と断絶
        public int CurrentSyutyu_Danzetsu = 0;
        public int CurrentJunkan_Seiyaku = 0;

        // 武器特有のBUFF
        public int CurrentFeltus = 0;
        public int CurrentJuzaPhantasmal = 0;
        public int CurrentLightServant = 0;
        public int CurrentShadowServant = 0;
        public int CurrentAdilBlueBurn = 0;
        public int CurrentMazeCube = 0;
        public int CurrentShadowBible = 0;
        public int CurrentDetachmentOrb = 0;
        public int CurrentDevilSummonerTome = 0;
        public int CurrentVoidHymnsonia = 0;

        // 消耗品特有のBUFF
        public int CurrentSagePotionMini = 0;
        public int CurrentGenseiTaima = 0;
        public int CurrentShiningAether = 0;
        public int CurrentBlackElixir = 0;
        public int CurrentElementalSeal = 0;
        public int CurrentColoressAntidote = 0;

        // 最終戦ライフカウント
        public int CurrentLifeCount = 0;

        // 最終戦ヴェルゼのカオティックスキーマ
        public int CurrentChaoticSchema = 0;

        // 各魔法・スキル・アクセサリによるパラメタＵＰ
        protected int buffStrength_BloodyVengeance = 0;
        protected int buffAgility_HeatBoost = 0;
        protected int buffIntelligence_PromisedKnowledge = 0;
        protected int buffStamina_Unknown = 0; // [将来]：StaminaのBUFFUPスペルを構築
        protected int buffMind_RiseOfImage = 0;

        protected int buffStrength_VoidExtraction = 0;
        protected int buffAgility_VoidExtraction = 0;
        protected int buffIntelligence_VoidExtraction = 0;
        protected int buffStamina_VoidExtraction = 0; // [将来]：StaminaのBuffUPスキルを構築
        protected int buffMind_VoidExtraction = 0;

        protected int buffStrength_HighEmotionality = 0;
        protected int buffAgility_HighEmotionality = 0;
        protected int buffIntelligence_HighEmotionality = 0;
        protected int buffStamina_HighEmotionality = 0; // [将来]：StaminaのBuffUPスキルを構築
        protected int buffMind_HighEmotionality = 0;

        // s 後編追加
        protected int buffStrength_TranscendentWish = 0;
        protected int buffAgility_TranscendentWish = 0;
        protected int buffIntelligence_TranscendentWish = 0;
        protected int buffStamina_TranscendentWish = 0;
        protected int buffMind_TranscendentWish = 0;
        // e 後編追加

        // s 後編追加
        protected int buffStrength_Hiyaku_Kassei = 0;
        protected int buffAgility_Hiyaku_Kassei = 0;
        protected int buffIntelligence_Hiyaku_Kassei = 0;
        protected int buffStamina_Hiyaku_Kassei = 0;
        protected int buffMind_Hiyaku_Kassei = 0;
        // e 後編追加


        public int BuffStrength_BloodyVengeance
        {
            get { return buffStrength_BloodyVengeance; }
            set { buffStrength_BloodyVengeance = value; }
        }
        public int BuffAgility_HeatBoost
        {
            get { return buffAgility_HeatBoost; }
            set { buffAgility_HeatBoost = value; }
        }
        public int BuffIntelligence_PromisedKnowledge
        {
            get { return buffIntelligence_PromisedKnowledge; }
            set { buffIntelligence_PromisedKnowledge = value; }
        }
        public int BuffStamina_Unknown
        {
            get { return buffStamina_Unknown; }
            set { buffStamina_Unknown = value; }
        }
        public int BuffMind_RiseOfImage
        {
            get { return buffMind_RiseOfImage; }
            set { buffMind_RiseOfImage = value; }
        }

        public int BuffStrength_VoidExtraction
        {
            get { return buffStrength_VoidExtraction; }
            set { buffStrength_VoidExtraction = value; }
        }
        public int BuffAgility_VoidExtraction
        {
            get { return buffAgility_VoidExtraction; }
            set { buffAgility_VoidExtraction = value; }
        }
        public int BuffIntelligence_VoidExtraction
        {
            get { return buffIntelligence_VoidExtraction; }
            set { buffIntelligence_VoidExtraction = value; }
        }
        public int BuffStamina_VoidExtraction
        {
            get { return buffStamina_VoidExtraction; }
            set { buffStamina_VoidExtraction = value; }
        }
        public int BuffMind_VoidExtraction
        {
            get { return buffMind_VoidExtraction; }
            set { buffMind_VoidExtraction = value; }
        }

        public int BuffStrength_HighEmotionality
        {
            get { return buffStrength_HighEmotionality; }
            set { buffStrength_HighEmotionality = value; }
        }
        public int BuffAgility_HighEmotionality
        {
            get { return buffAgility_HighEmotionality; }
            set { buffAgility_HighEmotionality = value; }
        }
        public int BuffIntelligence_HighEmotionality
        {
            get { return buffIntelligence_HighEmotionality; }
            set { buffIntelligence_HighEmotionality = value; }
        }
        public int BuffStamina_HighEmotionality
        {
            get { return buffStamina_HighEmotionality; }
            set { buffStamina_HighEmotionality = value; }
        }
        public int BuffMind_HighEmotionality
        {
            get { return buffMind_HighEmotionality; }
            set { buffMind_HighEmotionality = value; }
        }

        // s 後編追加
        public int BuffStrength_TranscendentWish
        {
            get { return buffStrength_TranscendentWish; }
            set { buffStrength_TranscendentWish = value; }
        }
        public int BuffAgility_TranscendentWish
        {
            get { return buffAgility_TranscendentWish; }
            set { buffAgility_TranscendentWish = value; }
        }
        public int BuffIntelligence_TranscendentWish
        {
            get { return buffIntelligence_TranscendentWish; }
            set { buffIntelligence_TranscendentWish = value; }
        }
        public int BuffStamina_TranscendentWish
        {
            get { return buffStamina_TranscendentWish; }
            set { buffStamina_TranscendentWish = value; }
        }
        public int BuffMind_TranscendentWish
        {
            get { return buffMind_TranscendentWish; }
            set { buffMind_TranscendentWish = value; }
        }

        public int BuffStrength_Hiyaku_Kassei
        {
            get { return buffStrength_Hiyaku_Kassei; }
            set { buffStrength_Hiyaku_Kassei = value; }
        }
        public int BuffAgility_Hiyaku_Kassei
        {
            get { return buffAgility_Hiyaku_Kassei; }
            set { buffAgility_Hiyaku_Kassei = value; }
        }
        public int BuffIntelligence_Hiyaku_Kassei
        {
            get { return buffIntelligence_Hiyaku_Kassei; }
            set { buffIntelligence_Hiyaku_Kassei = value; }
        }
        public int BuffStamina_Hiyaku_Kassei
        {
            get { return buffStamina_Hiyaku_Kassei; }
            set { buffStamina_Hiyaku_Kassei = value; }
        }
        public int BuffMind_Hiyaku_Kassei
        {
            get { return buffMind_Hiyaku_Kassei; }
            set { buffMind_Hiyaku_Kassei = value; }
        }
        public ItemBackPack iw = null;
        public ItemBackPack sw = null;
        public ItemBackPack ia = null;
        public ItemBackPack accessory = null;
        public ItemBackPack accessory2 = null;

        protected int buffStrength_MainWeapon = 0;
        protected int buffAgility_MainWeapon = 0;
        protected int buffIntelligence_MainWeapon = 0;
        protected int buffStamina_MainWeapon = 0;
        protected int buffMind_MainWeapon = 0;

        protected int buffStrength_SubWeapon = 0;
        protected int buffAgility_SubWeapon = 0;
        protected int buffIntelligence_SubWeapon = 0;
        protected int buffStamina_SubWeapon = 0;
        protected int buffMind_SubWeapon = 0;

        protected int buffStrength_Armor = 0;
        protected int buffAgility_Armor = 0;
        protected int buffIntelligence_Armor = 0;
        protected int buffStamina_Armor = 0;
        protected int buffMind_Armor = 0;

        protected int buffStrength_Accessory = 0;
        protected int buffAgility_Accessory = 0;
        protected int buffIntelligence_Accessory = 0;
        protected int buffStamina_Accessory = 0;
        protected int buffMind_Accessory = 0;

        protected int buffStrength_Accessory2 = 0;
        protected int buffAgility_Accessory2 = 0;
        protected int buffIntelligence_Accessory2 = 0;
        protected int buffStamina_Accessory2 = 0;
        protected int buffMind_Accessory2 = 0;


        public int BuffStrength_Accessory
        {
            get { return buffStrength_MainWeapon + buffStrength_SubWeapon + buffStrength_Armor + buffStrength_Accessory + buffStrength_Accessory2; } // c 後編追加
        }
        public int BuffAgility_Accessory
        {
            get { return buffAgility_MainWeapon + buffAgility_SubWeapon + buffAgility_Armor + buffAgility_Accessory + buffAgility_Accessory2; } // c 後編追加
        }
        public int BuffIntelligence_Accessory
        {
            get { return buffIntelligence_MainWeapon + buffIntelligence_SubWeapon + buffIntelligence_Armor + buffIntelligence_Accessory + buffIntelligence_Accessory2; } // c 後編追加
        }
        public int BuffStamina_Accessory
        {
            get { return buffStamina_MainWeapon + buffStamina_SubWeapon + buffStamina_Armor + buffStamina_Accessory + buffStamina_Accessory2; } // c 後編追加
        }
        public int BuffMind_Accessory
        {
            get { return buffMind_MainWeapon + buffMind_SubWeapon + buffMind_Armor + buffMind_Accessory + buffMind_Accessory2; } // c 後編追加
        }
        // 負の影響効果の重ねがけ効果
        public int CurrentPoisonValue = 0; // 後編追加
        public int CurrentConcussiveHitValue = 0; // 後編追加
        public int CurrentOnslaughtHitValue = 0; // 後編追加
        public int CurrentImpulseHitValue = 0; // 後編追加

        // 正の影響効果の重ねがけ効果
        public int CurrentSkyShieldValue = 0; // 後編追加
        public int CurrentStaticBarrierValue = 0; // 後編追加
        public int CurrentStanceOfMysticValue = 0; // 後編追加

        // 武器特有の重ねがけ効果
        public int CurrentFeltusValue = 0; // 後編追加
        public int CurrentJuzaPhantasmalValue = 0; // 後編追加
        public int CurrentLightServantValue = 0; // 後編追加
        public int CurrentShadowServantValue = 0; // 後編追加
        public int CurrentAdilBlueBurnValue = 0; // 後編追加
        public int CurrentMazeCubeValue = 0; // 後編追加

        // 消耗品特有の重ねがけ効果
        public int CurrentBlackElixirValue = 0; // 後編追加

        // 最終戦闘のライフカウント
        public int CurrentLifeCountValue = 0; // 後編追加

        public bool AvailableSkill = false;
        public bool AvailableMana = false;
        public bool AvailableArchitect = false;

        public PlayerAction PA = PlayerAction.None;
        public PlayerStance Stance = PlayerStance.None;
        protected string currentUsingItem = string.Empty;
        protected string currentSkillName = string.Empty;
        protected string currentSpellName = string.Empty;
        public string CurrentUsingItem
        {
            get { return currentUsingItem; }
            set { currentUsingItem = value; }
        }
        public string CurrentSkillName
        {
            get { return currentSkillName; }
            set { currentSkillName = value; }
        }
        public string CurrentSpellName
        {
            get { return currentSpellName; }
            set { currentSpellName = value; }
        }
        protected bool realTimeBattle = false; // 後編追加

        protected bool stackActivation = false; // 後編追加
        protected MainCharacter stackActivePlayer = null; // スタックインザコマンド用のアクティブプレイヤー格納庫
        protected MainCharacter stackTarget = null; // スタックインザコマンド用の対象プレイヤー格納庫
        protected PlayerAction stackPlayerAction = PlayerAction.None; // スタックインザコマンド用のプレイヤーアクション格納庫
        protected string stackCommandString = string.Empty; // スタックインザコマンド用のコマンド文字列格納庫

        protected List<string> actionCommandStackList = new List<string>(); // Bystanderがタイムストップ時、詠唱を累積させるために用いるアクションコマンドリスト
        public List<string> ActionCommandStackList
        {
            get { return actionCommandStackList; }
            set { actionCommandStackList = value; }
        }
        protected List<MainCharacter> actionCommandStackTarget = new List<MainCharacter>();
        public List<MainCharacter> ActionCommandStackTarget
        {
            get { return actionCommandStackTarget; }
            set { actionCommandStackTarget = value; }
        }

        public GameObject BuffPanel = null;
        public int BuffNumber = 0;
        public TruthImage[] BuffElement = null; // 「警告」：後編ではこれでBUFF並びを整列する。最終的には個別BUFFのTruthImageは全て不要になる。
        //public List<Image> BuffElement;
        public Text TextBattleMessage = null;


        public bool RealTimeBattle
        {
            get { return realTimeBattle; }
            set { realTimeBattle = value; }
        }
        public bool StackActivation
        {
            get { return stackActivation; }
            set { stackActivation = value; }
        }
        public MainCharacter StackActivePlayer
        {
            get { return stackActivePlayer; }
            set { stackActivePlayer = value; }
        }
        public MainCharacter StackTarget
        {
            get { return stackTarget; }
            set { stackTarget = value; }
        }
        public PlayerAction StackPlayerAction
        {
            get { return stackPlayerAction; }
            set { stackPlayerAction = value; }
        }
        public string StackCommandString
        {
            get { return stackCommandString; }
            set { stackCommandString = value; }
        }
        public double BattleBarPos = 0; // 後編追加
        public double BattleBarPos2 = 0; // 後編追加
        public double BattleBarPos3 = 0; // 後編追加
        public Image MainFaceArrow = null; // 後編追加
        public Image ShadowFaceArrow2 = null; // 後編追加
        public Image ShadowFaceArrow3 = null; // 後編追加

        protected bool dead = false;
        protected bool deadSignForTranscendentWish = false; // 後編追加

        protected bool actionDecision = false; // 敵がアクションを決定したかどうかを示すフラグ
        protected int decisionTiming = 0; // 敵がアクションを決定するタイミング

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public bool DeadSignForTranscendentWish
        {
            get { return deadSignForTranscendentWish; }
            set { deadSignForTranscendentWish = value; }
        }

        public bool ActionDecision
        {
            get { return actionDecision; }
            set { actionDecision = value; }
        }

        public int DecisionTiming
        {
            get { return decisionTiming; }
            set { decisionTiming = value; }
        }

        public ItemBackPack MainWeapon
        {
            get { return iw; }
            set
            {
                iw = value;
                // s 後編編集
                if (iw != null)
                {
                    this.buffStrength_MainWeapon = iw.BuffUpStrength;
                    this.buffAgility_MainWeapon = iw.BuffUpAgility;
                    this.buffIntelligence_MainWeapon = iw.BuffUpIntelligence;
                    this.buffStamina_MainWeapon = iw.BuffUpStamina;
                    this.buffMind_MainWeapon = iw.BuffUpMind;
                }
                else
                {
                    this.buffStrength_MainWeapon = 0;
                    this.buffAgility_MainWeapon = 0;
                    this.buffIntelligence_MainWeapon = 0;
                    this.buffStamina_MainWeapon = 0;
                    this.buffMind_MainWeapon = 0;
                }
                if (this.CurrentLife > this.MaxLife) this.CurrentLife = this.MaxLife;
                // e 後編編集
            }
        }
        // s 後編追加
        public ItemBackPack SubWeapon
        {
            get { return sw; }
            set
            {
                sw = value;
                // s 後編編集
                if (sw != null)
                {
                    this.buffStrength_SubWeapon = sw.BuffUpStrength;
                    this.buffAgility_SubWeapon = sw.BuffUpAgility;
                    this.buffIntelligence_SubWeapon = sw.BuffUpIntelligence;
                    this.buffStamina_SubWeapon = sw.BuffUpStamina;
                    this.buffMind_SubWeapon = sw.BuffUpMind;
                }
                else
                {
                    this.buffStrength_SubWeapon = 0;
                    this.buffAgility_SubWeapon = 0;
                    this.buffIntelligence_SubWeapon = 0;
                    this.buffStamina_SubWeapon = 0;
                    this.buffMind_SubWeapon = 0;
                }
                if (this.CurrentLife > this.MaxLife) this.CurrentLife = this.MaxLife;
                // e 後編編集
            }
        }
        // e 後編追加
        public ItemBackPack MainArmor
        {
            get { return ia; }
            set
            {
                ia = value;
                // s 後編編集
                if (ia != null)
                {
                    this.buffStrength_Armor = ia.BuffUpStrength;
                    this.buffAgility_Armor = ia.BuffUpAgility;
                    this.buffIntelligence_Armor = ia.BuffUpIntelligence;
                    this.buffStamina_Armor = ia.BuffUpStamina;
                    this.buffMind_Armor = ia.BuffUpMind;
                }
                else
                {
                    this.buffStrength_Armor = 0;
                    this.buffAgility_Armor = 0;
                    this.buffIntelligence_Armor = 0;
                    this.buffStamina_Armor = 0;
                    this.buffMind_Armor = 0;
                }
                if (this.CurrentLife > this.MaxLife) this.CurrentLife = this.MaxLife;
                // e 後編編集
            }
        }
        public ItemBackPack Accessory
        {
            get { return accessory; }
            set
            {
                accessory = value;
                if (accessory != null)
                {
                    // s 後編編集
                    this.buffStrength_Accessory = accessory.BuffUpStrength;
                    this.buffAgility_Accessory = accessory.BuffUpAgility;
                    this.buffIntelligence_Accessory = accessory.BuffUpIntelligence;
                    this.buffStamina_Accessory = accessory.BuffUpStamina;
                    this.buffMind_Accessory = accessory.BuffUpMind;
                    // e 後編編集
                }
                else
                {
                    this.buffStrength_Accessory = 0;
                    this.buffAgility_Accessory = 0;
                    this.buffIntelligence_Accessory = 0;
                    this.buffStamina_Accessory = 0;
                    this.buffMind_Accessory = 0;
                }
                if (this.CurrentLife > this.MaxLife) this.CurrentLife = this.MaxLife;
            }
        }
        // s 後編追加
        public ItemBackPack Accessory2
        {
            get { return accessory2; }
            set
            {
                accessory2 = value;
                if (accessory2 != null)
                {
                    this.buffStrength_Accessory2 = accessory2.BuffUpStrength;
                    this.buffAgility_Accessory2 = accessory2.BuffUpAgility;
                    this.buffIntelligence_Accessory2 = accessory2.BuffUpIntelligence;
                    this.buffStamina_Accessory2 = accessory2.BuffUpStamina;
                    this.buffMind_Accessory2 = accessory2.BuffUpMind;
                }
                else
                {
                    this.buffStrength_Accessory2 = 0;
                    this.buffAgility_Accessory2 = 0;
                    this.buffIntelligence_Accessory2 = 0;
                    this.buffStamina_Accessory2 = 0;
                    this.buffMind_Accessory2 = 0;
                }
                if (this.CurrentLife > this.MaxLife) this.CurrentLife = this.MaxLife;
            }
        }


        // -buff list
        public TruthImage pbPoison = null;
        public TruthImage pbPreStunning = null;
        public TruthImage pbStun = null;
        public TruthImage pbSilence = null;
        public TruthImage pbTemptation = null;
        public TruthImage pbFrozen = null;
        public TruthImage pbParalyze = null;
        public TruthImage pbNoResurrection = null;
        public TruthImage pbSlow = null; // 後編追加
        public TruthImage pbBlind = null; // 後編追加
        public TruthImage pbSlip = null; // 後編追加
        public TruthImage pbNoGainLife = null; // 後編追加
        public TruthImage pbBlinded = null; // 後編追加

        public TruthImage pbPhysicalAttackUp = null;
        public TruthImage pbPhysicalAttackDown = null;
        public TruthImage pbPhysicalDefenseUp = null;
        public TruthImage pbPhysicalDefenseDown = null;
        public TruthImage pbMagicAttackUp = null;
        public TruthImage pbMagicAttackDown = null;
        public TruthImage pbMagicDefenseUp = null;
        public TruthImage pbMagicDefenseDown = null;
        public TruthImage pbSpeedUp = null;
        public TruthImage pbSpeedDown = null;
        public TruthImage pbReactionUp = null;
        public TruthImage pbReactionDown = null;
        public TruthImage pbPotentialUp = null;
        public TruthImage pbPotentialDown = null;

        public TruthImage pbStrengthUp = null;
        public TruthImage pbAgilityUp = null;
        public TruthImage pbIntelligenceUp = null;
        public TruthImage pbStaminaUp = null;
        public TruthImage pbMindUp = null;

        public TruthImage pbLightUp = null;
        public TruthImage pbLightDown = null;
        public TruthImage pbShadowUp = null;
        public TruthImage pbShadowDown = null;
        public TruthImage pbFireUp = null;
        public TruthImage pbFireDown = null;
        public TruthImage pbIceUp = null;
        public TruthImage pbIceDown = null;
        public TruthImage pbForceUp = null;
        public TruthImage pbForceDown = null;
        public TruthImage pbWillUp = null;
        public TruthImage pbWillDown = null;

        public TruthImage pbResistLightUp = null;
        public TruthImage pbResistShadowUp = null;
        public TruthImage pbResistFireUp = null;
        public TruthImage pbResistIceUp = null;
        public TruthImage pbResistForceUp = null;
        public TruthImage pbResistWillUp = null;

        public TruthImage pbResistStun = null;
        public TruthImage pbResistSilence = null;
        public TruthImage pbResistPoison = null;
        public TruthImage pbResistTemptation = null;
        public TruthImage pbResistFrozen = null;
        public TruthImage pbResistParalyze = null;
        public TruthImage pbResistSlow = null;
        public TruthImage pbResistBlind = null;
        public TruthImage pbResistSlip = null;
        public TruthImage pbResistNoResurrection = null;

        public TruthImage pbAfterReviveHalf = null;
        public TruthImage pbFireDamage2 = null;
        public TruthImage pbBlackMagic = null;
        public TruthImage pbChaosDesperate = null;
        public TruthImage pbIchinaruHomura = null;
        public TruthImage pbAbyssFire = null;
        public TruthImage pbLightAndShadow = null;
        public TruthImage pbEternalDroplet = null;
        public TruthImage pbAusterityMatrixOmega = null;
        public TruthImage pbVoiceOfAbyss = null;
        public TruthImage pbAbyssWill = null;
        public TruthImage pbTheAbyssWall = null;

        // 基本スペル／スキル
        public TruthImage pbAbsorbWater = null;
        public TruthImage pbProtection = null;
        public TruthImage pbSaintPower = null;
        public TruthImage pbShadowPact = null;
        public TruthImage pbWordOfLife = null;
        public TruthImage pbGlory = null;
        public TruthImage pbFlameAura = null;
        public TruthImage pbOneImmunity = null;
        public TruthImage pbTimeStop = null;
        public TruthImage pbGaleWind = null;
        public TruthImage pbWordOfFortune = null;
        public TruthImage pbHeatBoost = null;
        public TruthImage pbBloodyVengeance = null;
        public TruthImage pbRiseOfImage = null;
        public TruthImage pbImmortalRave = null;
        public TruthImage pbBlackContract = null;
        public TruthImage pbAetherDrive = null;
        public TruthImage pbEternalPresence = null;
        public TruthImage pbMirrorImage = null;
        public TruthImage pbDeflection = null;
        public TruthImage pbPainfulInsanity = null;
        public TruthImage pbDamnation = null;
        public TruthImage pbPsychicTrance = null;
        public TruthImage pbBlindJustice = null;
        public TruthImage pbTranscendentWish = null;
        public TruthImage pbCounterAttack = null;
        public TruthImage pbStanceOfStanding = null;
        public TruthImage pbTruthVision = null;
        public TruthImage pbStanceOfFlow = null;
        public TruthImage pbAbsoluteZero = null;
        public TruthImage pbPromisedKnowledge = null;
        public TruthImage pbHighEmotionality = null;
        public TruthImage pbStanceOfEyes = null;
        public TruthImage pbVoidExtraction = null;
        public TruthImage pbNegate = null;
        public TruthImage pbAntiStun = null;
        public TruthImage pbStanceOfDeath = null;
        public TruthImage pbNothingOfNothingness = null;
        // 複合
        public TruthImage pbFlashBlaze = null;
        public TruthImage pbSkyShield = null;
        public TruthImage pbStaticBarrier = null;
        public TruthImage pbAusterityMatrix = null;
        public TruthImage pbEverDroplet = null;
        public TruthImage pbFrozenAura = null;
        public TruthImage pbStarLightning = null;
        public TruthImage pbWordOfMalice = null;
        public TruthImage pbSinFortune = null;
        public TruthImage pbBlackFire = null;
        public TruthImage pbBlazingField = null;
        public TruthImage pbEnrageBlast = null;
        public TruthImage pbSigilOfHomura = null;
        public TruthImage pbImmolate = null;
        public TruthImage pbVanishWave = null;
        public TruthImage pbHolyBreaker = null;
        public TruthImage pbHymnContract = null;
        public TruthImage pbDarkenField = null;
        public TruthImage pbEclipseEnd = null;
        public TruthImage pbExaltedField = null;
        public TruthImage pbOneAuthority = null;
        public TruthImage pbRisingAura = null;
        public TruthImage pbAscensionAura = null;
        public TruthImage pbSeventhMagic = null;
        public TruthImage pbPhantasmalWind = null;
        public TruthImage pbRedDragonWill = null;
        public TruthImage pbBlueDragonWill = null;
        public TruthImage pbParadoxImage = null;
        public TruthImage pbStanceOfDouble = null;
        public TruthImage pbSwiftStep = null;
        public TruthImage pbVigorSense = null;
        public TruthImage pbOnslaughtHit = null;
        public TruthImage pbSmoothingMove = null;
        public TruthImage pbFutureVision = null;
        public TruthImage pbReflexSpirit = null;
        public TruthImage pbConcussiveHit = null;
        public TruthImage pbTrustSilence = null;
        public TruthImage pbStanceOfMystic = null;
        public TruthImage pbNourishSense = null;
        public TruthImage pbImpulseHit = null;
        // 武器特有BUFF
        public TruthImage pbFeltus = null;
        public TruthImage pbJuzaPhantasmal = null;
        public TruthImage pbEternalFateRing = null;
        public TruthImage pbLightServant = null;
        public TruthImage pbShadowServant = null;
        public TruthImage pbAdilBlueBurn = null;
        public TruthImage pbMazeCube = null;
        public TruthImage pbShadowBible = null;
        public TruthImage pbDetachmentOrb = null;
        public TruthImage pbDevilSummonerTome = null;
        public TruthImage pbVoidHymnsonia = null;
        // 消耗品特有BUFF
        public TruthImage pbSagePotionMini = null;
        public TruthImage pbGenseiTaima = null;
        public TruthImage pbShiningAether = null;
        public TruthImage pbBlackElixir = null;
        public TruthImage pbElementalSeal = null;
        public TruthImage pbColoressAntidote = null;
        // 最終戦ライフカウント
        public TruthImage pbLifeCount = null;
        // ヴェルゼ最終戦カオティックスキーマ
        public TruthImage pbChaoticSchema = null;
        // 集中と断絶
        public TruthImage pbSyutyuDanzetsu = null;
        // 循環と誓約
        public TruthImage pbJunkanSeiyaku = null;

        #region "spell"
        // 聖 Light
        public bool freshHeal = false; // ライフ回復
        public bool FreshHeal
        {
            get { return freshHeal; }
            set { freshHeal = value; }
        }
        public bool protection = false; // 物理防御UP
        public bool Protection
        {
            get { return protection; }
            set { protection = value; }
        }
        public bool holyshock = false; // ダメージ
        public bool HolyShock
        {
            get { return holyshock; }
            set { holyshock = value; }
        }
        public bool saintpower = false; // 物理攻撃UP
        public bool SaintPower
        {
            get { return saintpower; }
            set { saintpower = value; }
        }
        public bool glory = false; // 次の３ターン、ライフ回復と攻撃を行う
        public bool Glory
        {
            get { return glory; }
            set { glory = value; }
        }
        public bool resurrection = false; // 死んだ仲間を復活
        public bool Resurrection
        {
            get { return resurrection; }
            set { resurrection = value; }
        }
        public bool celestialnova = false; // 敵全体ダメージ、または、味方全体ライフ回復
        public bool CelestialNova
        {
            get { return celestialnova; }
            set { celestialnova = value; }
        }

        // 闇 Shadow
        public bool darkblast = false; // 魔法攻撃
        public bool DarkBlast
        {
            get { return darkblast; }
            set { darkblast = value; }
        }
        public bool shadowpact = false; // 魔法攻撃UP
        public bool ShadowPact
        {
            get { return shadowpact; }
            set { shadowpact = value; }
        }
        public bool lifeTap = false; // マナ値、スキル値を消費してライフ回復
        public bool LifeTap
        {
            get { return lifeTap; }
            set { lifeTap = value; }
        }
        public bool blackContract = false; // 武器最大ダメージUP
        public bool BlackContract
        {
            get { return blackContract; }
            set { blackContract = value; }
        }
        public bool devouringPlague = false; // 敵にダメージ＋自分ライフ回復
        public bool DevouringPlague
        {
            get { return devouringPlague; }
            set { devouringPlague = value; }
        }
        public bool bloodyvengeance = false; // 力と体UP
        public bool BloodyVengeance
        {
            get { return bloodyvengeance; }
            set { bloodyvengeance = value; }
        }
        public bool damnation = false; // 毎ターン、ダメージ
        public bool Damnation
        {
            get { return damnation; }
            set { damnation = value; }
        }

        // 火 Fire
        public bool fireball = false; // 魔法攻撃
        public bool FireBall
        {
            get { return fireball; }
            set { fireball = value; }
        }
        public bool flameaura = false; // 通常攻撃付随、追加ダメージ
        public bool FlameAura
        {
            get { return flameaura; }
            set { flameaura = value; }
        }
        public bool heatboost = false; // 技量UP
        public bool HeatBoost
        {
            get { return heatboost; }
            set { heatboost = value; }
        }
        public bool flamestrike = false; // 中ダメージ
        public bool FlameStrike
        {
            get { return flamestrike; }
            set { flamestrike = value; }
        }
        public bool volcanicwave = false; // 大ダメージ
        public bool VolcanicWave
        {
            get { return volcanicwave; }
            set { volcanicwave = value; }
        }
        public bool immortalrave = false; // 次の３ターン、火攻撃スペル（コスト０）＋直接攻撃を行う
        public bool ImmortalRave
        {
            get { return immortalrave; }
            set { immortalrave = value; }
        }
        public bool lavaannihilation = false; // 全体ダメージ
        public bool LavaAnnihilation
        {
            get { return lavaannihilation; }
            set { lavaannihilation = value; }
        }

        // 水 Ice
        public bool iceneedle = false; // ダメージ
        public bool IceNeedle
        {
            get { return iceneedle; }
            set { iceneedle = value; }
        }
        public bool absorbwater = false; // 魔法防御UP
        public bool AbsorbWater
        {
            get { return absorbwater; }
            set { absorbwater = value; }
        }
        public bool cleansing = false; // 味方一人に対して、負の影響を解除する
        public bool Cleansing
        {
            get { return cleansing; }
            set { cleansing = value; }
        }
        public bool frozenlance = false; // 中ダメージ
        public bool FrozenLance
        {
            get { return frozenlance; }
            set { frozenlance = value; }
        }
        public bool mirrorimage = false; // 魔法攻撃を反射（AbsoluteZeroとWordOfPowerは反射できない）
        public bool MirrorImage
        {
            get { return mirrorimage; }
            set { mirrorimage = value; }
        }
        public bool promisedknowledge = false; // 知力UP
        public bool PromisedKnowledge
        {
            get { return promisedknowledge; }
            set { promisedknowledge = value; }
        }
        public bool absolutezero = false; // 次の３ターン、敵はライフ回復不可、反射付与を無効化、スペル詠唱不可（Tranquilityは対象外）、防御不可
        public bool AbsoluteZero
        {
            get { return absolutezero; }
            set { absolutezero = value; }
        }

        // 理 Force
        public bool wordofpower = false; // 物理ダメージ（魔法ダメージではない）
        public bool WordOfPower
        {
            get { return wordofpower; }
            set { wordofpower = value; }
        }
        public bool galewind = false; // 次のターン、攻撃またはスペル詠唱が連続２回になる
        public bool GaleWind
        {
            get { return galewind; }
            set { galewind = value; }
        }
        public bool wordoflife = false; // 毎ターンライフ回復
        public bool WordOfLife
        {
            get { return wordoflife; }
            set { wordoflife = value; }
        }
        public bool wordoffortune = false; // 次ターン、100%クリティカル
        public bool WordOfFortune
        {
            get { return wordoffortune; }
            set { wordoffortune = value; }
        }
        public bool aetherdrive = false; // 次の３ターン、敵からの物理ダメージ半減、味方の物理ダメージ２倍
        public bool AetherDrive
        {
            get { return aetherdrive; }
            set { aetherdrive = value; }
        }
        public bool genesis = false; // 前回とった行動と同じ行動を行う。スペル、スキルコストは０。ただしキャンセル対象になった後は効果が続かない。
        public bool Genesis
        {
            get { return genesis; }
            set { genesis = value; }
        }
        public bool eternalpresence = false; // 物理攻撃、物理防御、魔法攻撃、魔法防御UP
        public bool EternalPresence
        {
            get { return eternalpresence; }
            set { eternalpresence = value; }
        }

        // 空 Will
        public bool dispelmagic = false; // 付与された魔法を除去
        public bool DispelMagic
        {
            get { return dispelmagic; }
            set { dispelmagic = value; }
        }
        public bool riseofimage = false; // 心UP
        public bool RiseOfImage
        {
            get { return riseofimage; }
            set { riseofimage = value; }
        }
        public bool deflection = false; // 物理攻撃を反射
        public bool Deflection
        {
            get { return deflection; }
            set { deflection = value; }
        }
        public bool tranquility = false; // Glory、BlackContract、ImmortalRave、AbsoluteZero、AetherDrive、Deflection、MirrorImageを無効化
        public bool Tranquility
        {
            get { return tranquility; }
            set { tranquility = value; }
        }
        public bool oneimmunity = false; // 次のターン、防御している間は、全ダメージ無効化。
        public bool OneImmunity
        {
            get { return oneimmunity; }
            set { oneimmunity = value; }
        }
        public bool whiteout = false; // 大ダメージ
        public bool WhiteOut
        {
            get { return whiteout; }
            set { whiteout = value; }
        }
        public bool timestop = false; // 次のターン、相手のターンを飛ばす
        public bool TimeStop
        {
            get { return timestop; }
            set { timestop = value; }
        }

        // s 後編追加
        // 聖＋闇（完全逆）
        public bool psychicTrance = false;
        public bool PsychicTrance
        {
            get { return psychicTrance; }
            set { psychicTrance = value; }
        }
        public bool blindJustice = false;
        public bool BlindJustice
        {
            get { return blindJustice; }
            set { blindJustice = value; }
        }
        public bool transcendentWish = false;
        public bool TranscendentWish
        {
            get { return transcendentWish; }
            set { transcendentWish = value; }
        }

        // 聖＋火
        public bool flashBlaze = false; // 魔法ダメージ＋追加魔法ダメージ
        public bool FlashBlaze
        {
            get { return flashBlaze; }
            set { flashBlaze = value; }
        }
        public bool lightDetonator = false;
        public bool LightDetonator
        {
            get { return lightDetonator; }
            set { lightDetonator = value; }
        }
        public bool ascendantMeteor = false;
        public bool AscendantMeteor
        {
            get { return ascendantMeteor; }
            set { ascendantMeteor = value; }
        }

        // 聖＋水
        public bool skyShield = false;
        public bool SkyShield
        {
            get { return skyShield; }
            set { skyShield = value; }
        }
        public bool sacredHeal = false;
        public bool SacredHeal
        {
            get { return sacredHeal; }
            set { sacredHeal = value; }
        }
        public bool everDroplet = false;
        public bool EverDroplet
        {
            get { return everDroplet; }
            set { everDroplet = value; }
        }

        // 聖＋理
        public bool holyBreaker = false;
        public bool HolyBreaker
        {
            get { return holyBreaker; }
            set { holyBreaker = value; }
        }
        public bool exaltedField = false;
        public bool ExaltedField
        {
            get { return exaltedField; }
            set { exaltedField = value; }
        }
        public bool hymnContract = false;
        public bool HymnContract
        {
            get { return hymnContract; }
            set { hymnContract = value; }
        }

        // 聖＋空
        public bool starLightning = false;
        public bool StarLightning
        {
            get { return starLightning; }
            set { starLightning = value; }
        }
        public bool angelBreath = false;
        public bool AngelBreath
        {
            get { return angelBreath; }
            set { angelBreath = value; }
        }
        public bool endlessAnthem = false;
        public bool EndlessAnthem
        {
            get { return endlessAnthem; }
            set { endlessAnthem = value; }
        }

        // 闇＋火
        public bool blackFire = false;
        public bool BlackFire
        {
            get { return blackFire; }
            set { blackFire = value; }
        }
        public bool blazingField = false;
        public bool BlazingField
        {
            get { return blazingField; }
            set { blazingField = value; }
        }
        public bool demonicIgnite = false;
        public bool DemonicIgnite
        {
            get { return demonicIgnite; }
            set { demonicIgnite = value; }
        }

        // 闇＋水
        public bool blueBullet = false;
        public bool BlueBullet
        {
            get { return blueBullet; }
            set { blueBullet = value; }
        }
        public bool deepMirror = false;
        public bool DeepMirror
        {
            get { return deepMirror; }
            set { deepMirror = value; }
        }
        public bool deathDeny = false;
        public bool DeathDeny
        {
            get { return deathDeny; }
            set { deathDeny = value; }
        }

        // 闇＋理
        public bool wordofMalice = false;
        public bool WordOfMalice
        {
            get { return wordofMalice; }
            set { wordofMalice = value; }
        }
        public bool abyssEye = false;
        public bool AbyssEye
        {
            get { return abyssEye; }
            set { abyssEye = value; }
        }
        public bool sinFortune = false;
        public bool SinFortune
        {
            get { return sinFortune; }
            set { sinFortune = value; }
        }

        // 闇＋空        
        public bool darkenField = false;
        public bool DarkenField
        {
            get { return darkenField; }
            set { darkenField = value; }
        }
        public bool doomBlade = false;
        public bool DoomBlade
        {
            get { return doomBlade; }
            set { doomBlade = value; }
        }
        public bool eclipseEnd = false;
        public bool EclipseEnd
        {
            get { return eclipseEnd; }
            set { eclipseEnd = value; }
        }

        // 火＋水（完全逆）
        public bool frozenAura = false;
        public bool FrozenAura
        {
            get { return frozenAura; }
            set { frozenAura = value; }
        }
        public bool chillBurn = false;
        public bool ChillBurn
        {
            get { return chillBurn; }
            set { chillBurn = value; }
        }
        public bool zetaExplosion = false;
        public bool ZetaExplosion
        {
            get { return zetaExplosion; }
            set { zetaExplosion = value; }
        }

        // 火＋理
        public bool enrageBlast = false;
        public bool EnrageBlast
        {
            get { return enrageBlast; }
            set { enrageBlast = value; }
        }
        public bool piercingFlame = false;
        public bool PiercingFlame
        {
            get { return piercingFlame; }
            set { piercingFlame = value; }
        }
        public bool sigilofHomura = false;
        public bool SigilOfHomura
        {
            get { return sigilofHomura; }
            set { sigilofHomura = value; }
        }

        // 火＋空
        public bool immolate = false;
        public bool Immolate
        {
            get { return immolate; }
            set { immolate = value; }
        }
        public bool phantasmalWind = false;
        public bool PhantasmalWind
        {
            get { return phantasmalWind; }
            set { phantasmalWind = value; }
        }
        public bool redDragonWill = false;
        public bool RedDragonWill
        {
            get { return redDragonWill; }
            set { redDragonWill = value; }
        }

        // 水＋理
        public bool wordofAttitude = false;
        public bool WordOfAttitude
        {
            get { return wordofAttitude; }
            set { wordofAttitude = value; }
        }
        public bool staticBarrier = false;
        public bool StaticBarrier
        {
            get { return staticBarrier; }
            set { staticBarrier = value; }
        }
        public bool austerityMatrix = false;
        public bool AusterityMatrix
        {
            get { return austerityMatrix; }
            set { austerityMatrix = value; }
        }

        // 水＋空
        public bool vanishWave = false;
        public bool VanishWave
        {
            get { return vanishWave; }
            set { vanishWave = value; }
        }
        public bool vortexField = false;
        public bool VortexField
        {
            get { return vortexField; }
            set { vortexField = value; }
        }
        public bool blueDragonWill = false;
        public bool BlueDragonWill
        {
            get { return blueDragonWill; }
            set { blueDragonWill = value; }
        }

        // 空＋理(完全逆)
        public bool seventhMagic = false;
        public bool SeventhMagic
        {
            get { return seventhMagic; }
            set { seventhMagic = value; }
        }
        public bool paradoxImage = false;
        public bool ParadoxImage
        {
            get { return paradoxImage; }
            set { paradoxImage = value; }
        }
        public bool warpGate = false;
        public bool WarpGate
        {
            get { return warpGate; }
            set { warpGate = value; }
        }
        // e 後編追加
        #endregion

        #region "スキル"
        // 動
        public bool straightSmash = false; // 力＋技による直接攻撃
        public bool StraightSmash
        {
            get { return straightSmash; }
            set { straightSmash = value; }
        }
        public bool doubleSlash = false; // 2回攻撃
        public bool DoubleSlash
        {
            get { return doubleSlash; }
            set { doubleSlash = value; }
        }
        public bool crushingBlow = false; // 直接攻撃＋１ターンの間スタン効果
        public bool CrushingBlow
        {
            get { return crushingBlow; }
            set { crushingBlow = value; }
        }
        public bool soulInfinity = false; // 力＋技＋知による直接攻撃
        public bool SoulInfinity
        {
            get { return soulInfinity; }
            set { soulInfinity = value; }
        }

        // 静
        public bool counterAttack = false; // 直接攻撃が来た場合、それを無効化して、直接攻撃を返す
        public bool CounterAttack
        {
            get { return counterAttack; }
            set { counterAttack = value; }
        }
        public bool purePurification = false; // 味方一人に対して、負の影響を解除する
        public bool PurePurification
        {
            get { return purePurification; }
            set { purePurification = value; }
        }
        public bool antiStun = false; // スタンする攻撃を食らった場合、スタンしない
        public bool AntiStun
        {
            get { return antiStun; }
            set { antiStun = value; }
        }
        public bool stanceOfDeath = false; // 一撃死を食らう効果が当たった場合、それを回避する
        public bool StanceOfDeath
        {
            get { return stanceOfDeath; }
            set { stanceOfDeath = value; }
        }

        // 柔
        public bool stanceOfFlow = false; // 次の３ターン、必ず後攻を取る。
        public bool StanceOfFlow
        {
            get { return stanceOfFlow; }
            set { stanceOfFlow = value; }
        }
        public bool enigmaSense = false; // 力・技・知いずれの値の中で一番高い値を基準として直接攻撃
        public bool EnigmaSence
        {
            get { return enigmaSense; }
            set { enigmaSense = value; }
        }
        public bool silentRush = false; // ３回攻撃
        public bool SilentRush
        {
            get { return silentRush; }
            set { silentRush = value; }
        }
        public bool oboroImpact = false; // 力×心＋技×心＋知×心によるダメージ
        public bool OboroImpact
        {
            get { return oboroImpact; }
            set { oboroImpact = value; }
        }

        // 剛
        public bool stanceOfStanding = false; // 防御の構えをしたまま通常攻撃
        public bool StanceOfStanding
        {
            get { return stanceOfStanding; }
            set { stanceOfStanding = value; }
        }
        public bool innerInspiration = false; // スキルポイントを回復
        public bool InnerInspiration
        {
            get { return innerInspiration; }
            set { innerInspiration = value; }
        }
        public bool kineticSmash = false; // 力＋知による直接攻撃
        public bool KineticSmash
        {
            get { return kineticSmash; }
            set { kineticSmash = value; }
        }
        public bool catastrophe = false; // 全スキルポイントを使用して大ダメージ
        public bool Catastrophe
        {
            get { return catastrophe; }
            set { catastrophe = value; }
        }

        // 心眼
        public bool truthVision = false; // 敵のパラメタUP状態を無視したステータスを対象とする
        public bool TruthVision
        {
            get { return truthVision; }
            set { truthVision = value; }
        }
        public bool highEmotionality = false; // 次の３ターン、全パラメタUP
        public bool HighEmotionality
        {
            get { return highEmotionality; }
            set { highEmotionality = value; }
        }
        public bool stanceOfEyes = false; // 相手の行動をキャンセルた上で攻撃する
        public bool StanceOfEyes
        {
            get { return stanceOfEyes; }
            set { stanceOfEyes = value; }
        }
        public bool painfulInsanity = false; // 毎ターン、心値による永続ダメージ
        public bool PainfulInsanity
        {
            get { return painfulInsanity; }
            set { painfulInsanity = value; }
        }

        // 無心
        public bool negate = false; // 相手のスペル詠唱をキャンセルする
        public bool Negate
        {
            get { return negate; }
            set { negate = value; }
        }
        public bool voidExtraction = false; // 最も高いパラメタを２倍に引き上げる
        public bool VoidExtraction
        {
            get { return voidExtraction; }
            set { voidExtraction = value; }
        }
        public bool carnageRush = false; // ５連続の無属性攻撃（属性がない）
        public bool CarnageRush
        {
            get { return carnageRush; }
            set { carnageRush = value; }
        }
        public bool nothingOfNothingness = false; // DispelMagicとTranquilityを無効化する。Negate、StanceOfEyes、CounterAttackを無効化する。
        public bool NothingOfNothingness
        {
            get { return nothingOfNothingness; }
            set { nothingOfNothingness = value; }
        }

        // s 後編追加
        // 動＋静 （完全逆）
        public bool neutralSmash = false;
        public bool NeutralSmash
        {
            get { return neutralSmash; }
            set { neutralSmash = value; }
        }
        public bool stanceofDouble = false;
        public bool StanceOfDouble
        {
            get { return stanceofDouble; }
            set { stanceofDouble = value; }
        }

        // 動＋柔
        public bool swiftStep = false;
        public bool SwiftStep
        {
            get { return swiftStep; }
            set { swiftStep = value; }
        }
        public bool vigorSense = false;
        public bool VigorSense
        {
            get { return vigorSense; }
            set { vigorSense = value; }
        }

        // 動＋剛
        public bool circleSlash = false;
        public bool CircleSlash
        {
            get { return circleSlash; }
            set { circleSlash = value; }
        }
        public bool risingAura = false;
        public bool RisingAura
        {
            get { return risingAura; }
            set { risingAura = value; }
        }

        // 動＋心眼
        public bool rumbleShout = false;
        public bool RumbleShout
        {
            get { return rumbleShout; }
            set { rumbleShout = value; }
        }
        public bool onslaughtHit = false;
        public bool OnslaughtHit
        {
            get { return onslaughtHit; }
            set { onslaughtHit = value; }
        }

        // 動＋無心
        public bool smoothingMove = false;
        public bool SmoothingMove
        {
            get { return smoothingMove; }
            set { smoothingMove = value; }
        }
        public bool ascensionAura = false;
        public bool AscensionAura
        {
            get { return ascensionAura; }
            set { ascensionAura = value; }
        }

        // 静＋柔
        public bool futureVision = false;
        public bool FutureVision
        {
            get { return futureVision; }
            set { futureVision = value; }
        }
        public bool unknownShock = false;
        public bool UnknownShock
        {
            get { return unknownShock; }
            set { unknownShock = value; }
        }

        // 静＋剛
        public bool reflexSpirit = false;
        public bool ReflexSpirit
        {
            get { return reflexSpirit; }
            set { reflexSpirit = value; }
        }
        public bool fatalBlow = false;
        public bool FatalBlow
        {
            get { return fatalBlow; }
            set { fatalBlow = value; }
        }

        // 静＋心眼
        public bool sharpGlare = false;
        public bool SharpGlare
        {
            get { return sharpGlare; }
            set { sharpGlare = value; }
        }
        public bool concussiveHit = false;
        public bool ConcussiveHit
        {
            get { return concussiveHit; }
            set { concussiveHit = value; }
        }

        // 静＋無心
        public bool trustSilence = false;
        public bool TrustSilence
        {
            get { return trustSilence; }
            set { trustSilence = value; }
        }
        public bool mindKilling = false;
        public bool MindKilling
        {
            get { return mindKilling; }
            set { mindKilling = value; }
        }

        // 柔＋剛 （完全逆）
        public bool surpriseAttack = false;
        public bool SurpriseAttack
        {
            get { return surpriseAttack; }
            set { surpriseAttack = value; }
        }
        public bool impulseHit = false;
        public bool ImpulseHit
        {
            get { return impulseHit; }
            set { impulseHit = value; }
        }

        // 柔＋心眼
        public bool psychicWave = false;
        public bool PsychicWave
        {
            get { return psychicWave; }
            set { psychicWave = value; }
        }
        public bool nourishSense = false;
        public bool NourishSense
        {
            get { return nourishSense; }
            set { nourishSense = value; }
        }

        // 柔＋無心
        public bool recover = false;
        public bool Recover
        {
            get { return recover; }
            set { recover = value; }
        }
        public bool stanceofMystic = false;
        public bool StanceOfMystic
        {
            get { return stanceofMystic; }
            set { stanceofMystic = value; }
        }

        // 剛＋心眼
        public bool violentSlash = false;
        public bool ViolentSlash
        {
            get { return violentSlash; }
            set { violentSlash = value; }
        }
        public bool oneAuthority = false;
        public bool ONEAuthority
        {
            get { return oneAuthority; }
            set { oneAuthority = value; }
        }

        // 剛＋無心
        public bool outerInspiration = false;
        public bool OuterInspiration
        {
            get { return outerInspiration; }
            set { outerInspiration = value; }
        }
        public bool hardestParry = false;
        public bool HardestParry
        {
            get { return hardestParry; }
            set { hardestParry = value; }
        }

        // 心眼＋無心 （完全逆）
        public bool stanceofSuddenness = false;
        public bool StanceOfSuddenness
        {
            get { return stanceofSuddenness; }
            set { stanceofSuddenness = value; }
        }
        public bool soulExecution = false;
        public bool SoulExecution
        {
            get { return soulExecution; }
            set { soulExecution = value; }
        }
        // e 後編追加
        #endregion

        #region "元核"
        protected bool syutyu_danzetsu = false;
        public bool Syutyu_Danzetsu
        {
            get { return syutyu_danzetsu; }
            set { syutyu_danzetsu = value; }
        }

        protected bool junkan_seiyaku = false;
        public bool Junkan_Seiyaku
        {
            get { return junkan_seiyaku; }
            set { junkan_seiyaku = value; }
        }

        protected bool ora_ora_oraaa = false;
        public bool Ora_Ora_Oraaa
        {
            get { return ora_ora_oraaa; }
            set { ora_ora_oraaa = value; }
        }

        protected bool shinzitsu_hakai = false;
        public bool Shinzitsu_Hakai
        {
            get { return shinzitsu_hakai; }
            set { shinzitsu_hakai = value; }
        }
        #endregion

        public string CurrentCommand { get; set; }

        //	public int CurrentProtection { get; set; }
        //	public int CurrentShadowPact { get; set; }
        //	public int CurrentFlameAura { get; set; }
        //	public int CurrentWordOfLife { get; set; }
        //	public int CurrentDeflection { get; set; }
        //	public int CurrentTruthVision { get; set; }

        // battle command list (manual)
       	public string[] BattleActionCommandList = new string[Database.BATTLE_COMMAND_MAX];
        public string BattleActionCommand1
        {
            get { return BattleActionCommandList[0]; }
            set { BattleActionCommandList[0] = value; }
        }
        public string BattleActionCommand2
        {
            get { return BattleActionCommandList[1]; }
            set { BattleActionCommandList[1] = value; }
        }
        public string BattleActionCommand3
        {
            get { return BattleActionCommandList[2]; }
            set { BattleActionCommandList[2] = value; }
        }
        public string BattleActionCommand4
        {
            get { return BattleActionCommandList[3]; }
            set { BattleActionCommandList[3] = value; }
        }
        public string BattleActionCommand5
        {
            get { return BattleActionCommandList[4]; }
            set { BattleActionCommandList[4] = value; }
        }
        public string BattleActionCommand6
        {
            get { return BattleActionCommandList[5]; }
            set { BattleActionCommandList[5] = value; }
        }
        public string BattleActionCommand7
        {
            get { return BattleActionCommandList[6]; }
            set { BattleActionCommandList[6] = value; }
        }
        public string BattleActionCommand8
        {
            get { return BattleActionCommandList[7]; }
            set { BattleActionCommandList[7] = value; }
        }
        public string BattleActionCommand9
        {
            get { return BattleActionCommandList[8]; }
            set { BattleActionCommandList[8] = value; }
        }

        protected bool nowExecActionFlag = false; // 後編追加　現在自分が行動実行中であることを示すフラグ
        public bool NowExecActionFlag
        {
            get { return nowExecActionFlag; }
            set { nowExecActionFlag = value; }
        }
        // +BUFF
        //	public int CurrentFlameAura = 0;
        //	public int CurrentProtection = 0;
        //	public int CurrentShadowPact = 0;
        //	public int CurrentWordOfLife = 0;
        //	public int CurrentDeflection = 0;
        //	public int CurrentStanceOfStanding = 0;
        //	public int CurrentStanceOfFlow = 0;
        //	public int CurrentTruthVision = 0;
        //	public int CurrentVoidExtraction = 0;
        //	public int CurrentNothingOfNothingness = 0;

        // -BUFF
        //	public int CurrentPoison = 0;
        //	public int CurrentPoisonValue = 0;


        // basic parameter up
        //	public int CurrentStrengthUp { get; set; }
        //	public int CurrentStrengthUpValue { get; set; }
        //	public int CurrentAgilityUp { get; set; }
        //	public int CurrentAgilityUpValue { get; set; }
        //	public int CurrentIntelligenceUp { get; set; }
        //	public int CurrentIntelligenceUpValue { get; set; }
        //	public int CurrentStaminaUp { get; set; }
        //	public int CurrentStaminaUpValue { get; set; }
        //	public int CurrentMindUp { get; set; }
        //	public int CurrentMindUpValue { get; set; }

        // gui
        public GameObject mainPanel = null;
        public GameObject MainObjectBack = null;
        public Button MainObjectButton = null;
        public Color MainColor = new Color();

        public Text labelName = null;
        public Text labelFullName = null;
        public Text ActionLabel = null;

        public Text labelCurrentLifePoint = null;
        public Image meterCurrentLifePoint = null;

        public Text labelCurrentManaPoint = null;
        public Image meterCurrentManaPoint = null;

        public Text labelCurrentSkillPoint = null;
        public Image meterCurrentSkillPoint = null;

        public Text labelCurrentInstantPoint = null;
        public Image meterCurrentInstantPoint = null;

        public Text labelCurrentSpecialInstant = null;
        public Image meterCurrentSpecialInstant = null;

        public GameObject DamagePanel = null;
        public Text DamageLabel = null;
        public Text CriticalLabel = null;

        public Color DungeonPanelColor
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_EIN_R, Database.COLOR_EIN_G, Database.COLOR_EIN_B, 0.8f); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_RANA_R, Database.COLOR_RANA_G, Database.COLOR_RANA_B, 0.8f); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_OL_R, Database.COLOR_OL_G, Database.COLOR_OL_B, 0.8f); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_KAHL_R, Database.COLOR_KAHL_G, Database.COLOR_KAHL_B, 0.8f); }
                else { return new Color(Database.COLOR_VERZE_R, Database.COLOR_VERZE_G, Database.COLOR_VERZE_B, 0.8f); }
            }
        }

        public Color PlayerStatusColor
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_EIN_R, Database.COLOR_EIN_G, Database.COLOR_EIN_B); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_RANA_R, Database.COLOR_RANA_G, Database.COLOR_RANA_B); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_OL_R, Database.COLOR_OL_G, Database.COLOR_OL_B); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_KAHL_R, Database.COLOR_KAHL_G, Database.COLOR_KAHL_B); }
                else { return new Color(Database.COLOR_VERZE_R, Database.COLOR_VERZE_G, Database.COLOR_VERZE_B); }
            }
        }
        public Color PlayerColor
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_BOX_EIN_R, Database.COLOR_BOX_EIN_G, Database.COLOR_BOX_EIN_B); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_RANA_R, Database.COLOR_RANA_G, Database.COLOR_RANA_B); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_BOX_OL_R, Database.COLOR_BOX_OL_G, Database.COLOR_BOX_OL_B); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_BOX_KAHL_R, Database.COLOR_BOX_KAHL_G, Database.COLOR_BOX_KAHL_B); }
                else { return new Color(Database.COLOR_BOX_VERZE_R, Database.COLOR_BOX_VERZE_G, Database.COLOR_BOX_VERZE_B); }
            }
        }

        public Color PlayerBattleColor
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_BATTLE_EIN_R, Database.COLOR_BATTLE_EIN_G, Database.COLOR_BATTLE_EIN_B); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_BATTLE_RANA_R, Database.COLOR_BATTLE_RANA_G, Database.COLOR_BATTLE_RANA_B); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_BATTLE_OL_R, Database.COLOR_BATTLE_OL_G, Database.COLOR_BATTLE_OL_B); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_BATTLE_KAHL_R, Database.COLOR_BATTLE_KAHL_G, Database.COLOR_BATTLE_KAHL_B); }
                else { return new Color(Database.COLOR_BATTLE_VERZE_R, Database.COLOR_BATTLE_VERZE_G, Database.COLOR_BATTLE_VERZE_B); }
            }
        }
        public Color PlayerBattleTargetColor1
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_BATTLE_TARGET1_EIN_R, Database.COLOR_BATTLE_TARGET1_EIN_G, Database.COLOR_BATTLE_TARGET1_EIN_B); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_BATTLE_TARGET1_RANA_R, Database.COLOR_BATTLE_TARGET1_RANA_G, Database.COLOR_BATTLE_TARGET1_RANA_B); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_BATTLE_TARGET1_OL_R, Database.COLOR_BATTLE_TARGET1_OL_G, Database.COLOR_BATTLE_TARGET1_OL_B); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_BATTLE_TARGET1_KAHL_R, Database.COLOR_BATTLE_TARGET1_KAHL_G, Database.COLOR_BATTLE_TARGET1_KAHL_B); }
                else { return new Color(Database.COLOR_BATTLE_TARGET1_VERZE_R, Database.COLOR_BATTLE_TARGET1_VERZE_G, Database.COLOR_BATTLE_TARGET1_VERZE_B); }
            }
        }
        public Color PlayerBattleTargetColor2
        {
            get
            {
                if (this.FirstName == Database.EIN_WOLENCE) { return new Color(Database.COLOR_BATTLE_TARGET2_EIN_R, Database.COLOR_BATTLE_TARGET2_EIN_G, Database.COLOR_BATTLE_TARGET2_EIN_B); }
                else if (this.FirstName == Database.RANA_AMILIA) { return new Color(Database.COLOR_BATTLE_TARGET2_RANA_R, Database.COLOR_BATTLE_TARGET2_RANA_G, Database.COLOR_BATTLE_TARGET2_RANA_B); }
                else if (this.FirstName == Database.OL_LANDIS) { return new Color(Database.COLOR_BATTLE_TARGET2_OL_R, Database.COLOR_BATTLE_TARGET2_OL_G, Database.COLOR_BATTLE_TARGET2_OL_B); }
                else if (this.FirstName == Database.SINIKIA_KAHLHANZ) { return new Color(Database.COLOR_BATTLE_TARGET2_KAHL_R, Database.COLOR_BATTLE_TARGET2_KAHL_G, Database.COLOR_BATTLE_TARGET2_KAHL_B); }
                else { return new Color(Database.COLOR_BATTLE_TARGET2_VERZE_R, Database.COLOR_BATTLE_TARGET2_VERZE_G, Database.COLOR_BATTLE_TARGET2_VERZE_B); }
            }
        }
        // gui action button list
        public List<Button> ActionButtonList = new List<Button>();
        public List<Text> ActionKeyNum = new List<Text>();
        public List<Image> IsSorceryMark = new List<Image>();

        // battle target
        public MainCharacter Target = null; // 敵ターゲット
        public MainCharacter Target2 = null; // 味方ターゲット

        // Genesis / StanceOfDouble
        protected PlayerAction beforePA = PlayerAction.None;
        protected string beforeUsingItem = string.Empty;
        protected string beforeSkillName = string.Empty;
        protected string beforeSpellName = string.Empty;
        protected string beforeArchetypeName = string.Empty; // 後編追加
        protected MainCharacter beforeTarget = null;
        protected MainCharacter beforeTarget2 = null; // 後編追加
        protected bool alreadyPlayArchetype = false; // 後編追加

        public PlayerAction BeforePA
        {
            get { return beforePA; }
            set { beforePA = value; } // 後編追加
        }
        public string BeforeUsingItem
        {
            get { return beforeUsingItem; }
            set { beforeUsingItem = value; } // 後編追加
        }
        public string BeforeSkillName
        {
            get { return beforeSkillName; }
            set { beforeSkillName = value; } // 後編追加
        }
        public string BeforeSpellName
        {
            get { return beforeSpellName; }
            set { beforeSpellName = value; } // 後編追加
        }
        // s 後編追加
        public string BeforeArchetypeName
        {
            get { return beforeArchetypeName; }
            set { beforeArchetypeName = value; }
        }
        // e 後編追加
        public MainCharacter BeforeTarget
        {
            get { return beforeTarget; }
            set { beforeTarget = value; }
        }
        // s 後編追加
        public MainCharacter BeforeTarget2
        {
            get { return beforeTarget2; }
            set { beforeTarget2 = value; }
        }
        public bool AlreadyPlayArchetype
        {
            get { return alreadyPlayArchetype; }
            set { alreadyPlayArchetype = value; }
        }

        public void EnableGUI()
        {
            //if (labelInstant != null) { this.labelInstant.enabled = true; }
            //if (labelCurrentInstantPoint != null) { labelCurrentInstantPoint.enabled = true; }
            //if (ActionButton1 != null) { ActionButton1.enabled = true; ActionButton1.image.enabled = true; }
            //if (ActionButton2 != null) { ActionButton2.enabled = true; ActionButton2.image.enabled = true; }
            //if (ActionButton3 != null) { ActionButton3.enabled = true; ActionButton3.image.enabled = true; }
            //if (ActionButton4 != null) { ActionButton4.enabled = true; ActionButton4.image.enabled = true; }
            //if (ActionButton5 != null) { ActionButton5.enabled = true; ActionButton5.image.enabled = true; }
            //if (ActionButton6 != null) { ActionButton6.enabled = true; ActionButton6.image.enabled = true; }
        }
        public void DisableGUI()
        {
            //if (labelInstant != null) { this.labelInstant.enabled = false; }
            //if (labelCurrentInstantPoint != null) { labelCurrentInstantPoint.enabled = false; }
            //if (ActionButton1 != null) { ActionButton1.enabled = false; ActionButton1.image.enabled = false; }
            //if (ActionButton2 != null) { ActionButton2.enabled = false; ActionButton2.image.enabled = false; }
            //if (ActionButton3 != null) { ActionButton3.enabled = false; ActionButton3.image.enabled = false; }
            //if (ActionButton4 != null) { ActionButton4.enabled = false; ActionButton4.image.enabled = false; }
            //if (ActionButton5 != null) { ActionButton5.enabled = false; ActionButton5.image.enabled = false; }
            //if (ActionButton6 != null) { ActionButton6.enabled = false; ActionButton6.image.enabled = false; }
        }

        public void ActivatePoison(Sprite img, int effectTime)
        {
            CurrentPoison = effectTime;
            CurrentPoisonValue++;
            if (pbPoison != null) { pbPoison.sprite = img; pbPoison.enabled = true; }
        }

        public void ActivateFlameAura(Sprite img, int effectTime)
        {
            CurrentFlameAura = effectTime;
            if (pbFlameAura != null) { pbFlameAura.sprite = img; pbFlameAura.enabled = true; }
        }

        public void ActivateProtection(Sprite img, int effectTime)
        {
            CurrentProtection = effectTime;
            if (pbProtection != null) { pbProtection.sprite = img; pbProtection.enabled = true; }
        }

        public void ActivateShadowPact(Sprite img, int effectTime)
        {
            CurrentShadowPact = effectTime;
            if (pbShadowPact != null) { pbShadowPact.sprite = img; pbShadowPact.enabled = true; }
        }

        public void ActivateWordOfLife(Sprite img, int effectTime)
        {
            CurrentWordOfLife = effectTime;
            if (pbWordOfLife != null) { pbWordOfLife.sprite = img; pbWordOfLife.enabled = true; }
        }

        public void ActivateDeflection(Sprite img, int effectTime)
        {
            CurrentDeflection = effectTime;
            if (pbDeflection != null) { pbDeflection.sprite = img; pbDeflection.enabled = true; }
        }

        public void ActivateTruthVision(Sprite img, int effectTime)
        {
            CurrentTruthVision = effectTime;
            if (pbTruthVision != null) { pbTruthVision.sprite = img; pbTruthVision.enabled = true; }
        }


        // basic parameter
        // Totalはベース値＋戦闘中UP＋常在型BUFFUPの合計値
        public int TotalStrength
        {
            get
            {
                return this.baseStrength + 
                    this.buffStrength_BloodyVengeance +
                    this.buffStrength_HighEmotionality +
                    this.buffStrength_VoidExtraction +
                    this.buffStrength_TranscendentWish + // 後編追加
                    this.buffStrength_Hiyaku_Kassei + // c 後編追加
                    this.buffStrength_MainWeapon + // 後編追加
                    this.buffStrength_SubWeapon + // 後編追加
                    this.buffStrength_Armor +  // 後編追加
                    this.buffStrength_Accessory +
                    this.buffStrength_Accessory2 + // 後編追加
                    this.buffStrength_Food + // 後編追加
                    this.CurrentStrengthUpValue; // 後編追加    
            } // c 後編追加
        }
        public int TotalAgility
        {
            get
            {
                return this.baseAgility + 
                	this.buffAgility_HeatBoost +
                	this.buffAgility_HighEmotionality +
                	this.buffAgility_VoidExtraction +
                	this.buffAgility_TranscendentWish + // 後編追加
                	this.buffAgility_Hiyaku_Kassei + // c 後編追加
                	this.buffAgility_MainWeapon + // 後編追加
                	this.buffAgility_SubWeapon + // 後編追加
                	this.buffAgility_Armor + // 後編追加
                	this.buffAgility_Accessory +
                	this.buffAgility_Accessory2 + // 後編追加
                	this.buffAgility_Food + // 後編追加
                	this.CurrentAgilityUpValue; // c 後編追加
            }
        }
        public int TotalIntelligence
        {
            get { return this.baseIntelligence +
                    this.buffIntelligence_PromisedKnowledge +
                    this.buffIntelligence_HighEmotionality +
                    this.buffIntelligence_VoidExtraction +
                    this.buffIntelligence_TranscendentWish + // 後編追加
                    this.buffIntelligence_Hiyaku_Kassei + // c 後編追加
                    this.buffIntelligence_MainWeapon + // 後編追加
                    this.buffIntelligence_SubWeapon + // 後編追加
                    this.buffIntelligence_Armor + // 後編追加
                    this.buffIntelligence_Accessory +
                    this.buffIntelligence_Accessory2 + // 後編追加
                    this.buffIntelligence_Food + // 後編追加
                    this.CurrentIntelligenceUpValue; // 後編追加
            }
        }
        public int TotalStamina
        {
            get { return this.baseStamina +
                    this.buffStamina_Unknown +
                    this.buffStamina_HighEmotionality +
                    this.buffStamina_VoidExtraction +
                    this.buffStamina_TranscendentWish + // 後編追加
                    this.buffStamina_Hiyaku_Kassei + // c 後編追加
                    this.buffStamina_MainWeapon + // 後編追加
                    this.buffStamina_SubWeapon + // 後編追加
                    this.buffStamina_Armor + // 後編追加
                    this.buffStamina_Accessory +
                    this.buffStamina_Accessory2 + // 後編追加
                    this.buffStamina_Food + // 後編追加
                    this.CurrentStaminaUpValue; // 後編追加
            }
        }
        public int TotalMind
        {
            get
            {
                return this.baseMind +
                    this.buffMind_RiseOfImage +
                    this.buffMind_HighEmotionality +
                    this.buffMind_VoidExtraction +
                    this.buffMind_TranscendentWish + // 後編追加
                    this.buffMind_Hiyaku_Kassei + // c 後編追加
                    this.buffMind_MainWeapon + // 後編追加
                    this.buffMind_SubWeapon + // 後編追加
                    this.buffMind_Armor + // 後編追加
                    this.buffMind_Accessory +
                    this.buffMind_Accessory2 + // 後編追加
                    this.buffMind_Food + // 後編追加
                    this.CurrentMindUpValue + // c 後編追加
                    this.CurrentFeltusValue * (int)(PrimaryLogic.FeltusValue(this)); // 後編追加
            }
        }

        // Standardはベース値＋常在型BUFFUP＋食事常在BUFFUPの合計値
        public int StandardStrength
        {
            get { return this.baseStrength + this.buffStrength_MainWeapon + this.buffStrength_Accessory + this.buffStrength_Accessory2 + this.buffStrength_Food; } // c 後編追加
        }
        public int StandardAgility
        {
            get { return this.baseAgility + this.buffAgility_MainWeapon + this.buffAgility_Accessory + this.buffAgility_Accessory2 + this.buffAgility_Food; } // c 後編追加
        }
        public int StandardIntelligence
        {
            get { return this.baseIntelligence + this.buffIntelligence_MainWeapon + this.buffIntelligence_Accessory + this.buffIntelligence_Accessory2 + this.buffIntelligence_Food; } // c 後編追加
        }
        public int StandardStamina
        {
            get { return this.baseStamina + this.buffStamina_MainWeapon + this.buffStamina_Accessory + this.buffStamina_Accessory2 + this.buffStamina_Food; } // c 後編追加
        }
        public int StandardMind
        {
            get { return this.baseMind + this.buffMind_MainWeapon + this.buffMind_Accessory + this.buffMind_Accessory2 + this.buffMind_Food; } // c 後編追加
        }



        public void ActivateCharacter()
        {
            if (this.labelName != null) this.labelName.gameObject.SetActive(true);
            if (this.labelCurrentLifePoint != null) this.labelCurrentLifePoint.gameObject.SetActive(true);
            if (this.labelCurrentSkillPoint != null) { this.labelCurrentSkillPoint.gameObject.SetActive(true); }
            if (this.labelCurrentManaPoint != null) { this.labelCurrentManaPoint.gameObject.SetActive(true); }
            if (this.labelCurrentInstantPoint != null) { this.labelCurrentInstantPoint.gameObject.SetActive(true); }
            if (this.labelCurrentSpecialInstant != null) { this.labelCurrentSpecialInstant.gameObject.SetActive(true); }
            for (int ii = 0; ii < this.ActionButtonList.Count; ii++)
            {
                if (this.ActionButtonList[ii] != null) this.ActionButtonList[ii].gameObject.SetActive(true);
            }
            if (this.ActionLabel != null) this.ActionLabel.gameObject.SetActive(true);
            if (this.MainObjectButton != null) this.MainObjectButton.gameObject.SetActive(true);
            //if (this.pbBuff1 != null) this.pbBuff1.Visible = true;
            //if (this.pbBuff2 != null) this.pbBuff2.Visible = true;
            //if (this.pbBuff3 != null) this.pbBuff3.Visible = true;
        }
        public void DisactiveCharacter()
        {
            if (this.labelName != null) this.labelName.gameObject.SetActive(false);
            if (this.labelCurrentLifePoint != null) this.labelCurrentLifePoint.gameObject.SetActive(false);
            if (this.labelCurrentSkillPoint != null) this.labelCurrentSkillPoint.gameObject.SetActive(false);
            if (this.labelCurrentManaPoint != null) this.labelCurrentManaPoint.gameObject.SetActive(false);
            if (this.labelCurrentInstantPoint != null) this.labelCurrentInstantPoint.gameObject.SetActive(false);
            if (this.labelCurrentSpecialInstant != null) this.labelCurrentSpecialInstant.gameObject.SetActive(false);
            for (int ii = 0; ii < this.ActionButtonList.Count; ii++)
            {
                if (this.ActionButtonList[ii] != null) this.ActionButtonList[ii].gameObject.SetActive(false);
            }
            if (this.ActionLabel != null) this.ActionLabel.gameObject.SetActive(false);
            if (this.MainObjectButton != null) this.MainObjectButton.gameObject.SetActive(false);
            //if (this.pbBuff1 != null) this.pbBuff1.Visible = false;
            //if (this.pbBuff2 != null) this.pbBuff2.Visible = false;
            //if (this.pbBuff3 != null) this.pbBuff3.Visible = false;
        }

        public void DeadPlayer()
        {
            this.Dead = true;
            this.DeadSignForTranscendentWish = false;

            if (this.MainObjectButton != null)
            {
                // 【要検討】リザレクション対象にするため、生死に関わらず、対象とする事を許可
                //this.MainObjectButton.Enabled = false; // delete
                this.MainObjectButton.GetComponent<Image>().color = UnityColor.DarkSlateGray;
            }

            if (this.labelName != null) this.labelName.color = Color.red;
            if (this.labelCurrentLifePoint != null) this.labelCurrentLifePoint.color = Color.red;
        }

        public void ResurrectPlayer(int life)
        {
            if (this.CurrentNoResurrection <= 0)
            {
                this.CurrentLife = life;
                this.Dead = false;
                if (this.MainObjectButton != null)
                {
                    this.MainObjectButton.gameObject.SetActive(true);
                    this.MainObjectButton.GetComponent<Image>().color = this.MainColor;
                }
                if (this.labelName != null) { this.labelName.color = Color.black; }
                if (this.labelCurrentLifePoint != null) { this.labelCurrentLifePoint.color = Color.black; this.labelCurrentLifePoint.text = CurrentLife.ToString(); }
            }
        }

        public string GetCharacterSentence(int sentenceNumber)
        {
            // 0 ~ 999 戦闘中
            // 10000 ~ 99999 戦闘中(後編追加)
            // 1000 ~ 1999 ホームタウン会話
            // 2000 ~ 2999 ステータス表示時
            // 3000 ~ 3999 ガンツ武具店会話
            // 4000 ~ 4999 後編戦闘の戦闘コマンド設定時

            #region "アイン"
            if (this.FirstName == "アイン")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "：しまった！スキルポイントが足りねぇ！\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "：行くぜ！オラァ！！\r\n";
                    case 2: // Double Slash 1
                        return this.FirstName + "：ほらよ！\r\n";
                    case 3: // Double Slash 2
                        return this.FirstName + "：もういっちょ！\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "：【心眼奥義】狂乱の痛みを食らい続けな！\r\n";
                    case 5: // empty skill
                        return this.FirstName + "：しまった。スキルを選択してねえ！！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "：ってぇな！！クソ！\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + ": 全然・・・動きが見えねえ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "：ッグ、ガハアァァ！！！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 回復したぜ。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "：この炎をくらえ！FireBall！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "：焼け落ちろ！FlameStrike！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "：灼熱の業火！くらえ！VolcanicWave！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：オラァ、クリティカルヒット！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "：燃えろ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "：戦闘中は使えねえぞコレ。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "：・・・？？ よく分からないアイテムだ。何もおきねえ！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "：マナが足りねぇ！\r\n";
                    case 18: // Protection
                        return this.FirstName + "：聖神の加護、Protection！物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：水女神の加護・・・AbsorbWater！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：力こそ全て、SaintPower！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：闇との契約・・・ShadowPact！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：闇の使者よ力を示せ・・・BloodyVengeance！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "：聖なる鉄槌、HolyShock！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：常に栄光あれ。Glory！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：聖なる波動、CelestialNova！ {0} 回復したぜ。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "：聖なる裁きを食らえ、CelestialNova！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "：黒の波動を食らえ、DarkBlast！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "：炎授天使、召還！くらえ！LavaAnnihilation！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.FirstName + "：炎授天使、召還！くらえ！LavaAnnihilation！\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "：体力を食らい尽くすぜ。DevouringPlague！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "：氷の刃を食らえ、IceNeedle！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "：凍りの槍でぶち抜かれろ、FrozenLance！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "：大海原よ！その力を見せ付けろ、TidalWave！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "：この衝撃波でも食らえ、WordOfPower！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "：全ての感覚から抹殺せしめろ、WhiteOut！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：悪魔取引だ、BlackContract！ " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：炎のシンボルよ来たれ、FlameAura！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：朽ち果てろ、Damnation！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：炎授天使よ、炎の勢い借りるぜ、HeatBoost！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：オラオラオラァいくぜ、ImmortalRave！ " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：同時に行くぜ、GaleWind！ もう一人の" + this.FirstName + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.FirstName + "：自然と共にあれ、WordOfLife！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "：強き未来を示せ、WordOfFortune！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.FirstName + "：空想の物理力を示せ、AetherDrive！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "：法則と原理を呼び起こすぜ、EternalPresence！ " + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.FirstName + "：空間障壁、OneImmunity！ " + this.FirstName + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.FirstName + "：全ては時空干渉から・・・TimeStop！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.FirstName + "：ウザってぇ効果は消えて無くなれ、DispelMagic！ \r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "：時空の支配者より新たなるイメージを借りるぜ・・・RiseOfImage！ 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.FirstName + "：しまった。空詠唱だ！！\r\n";
                    case 51: // Inner Inspiration
                        return this.FirstName + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.FirstName + "：偉大なる聖の力。奇跡起こすぜ、Resurrection！！\r\n";
                    case 53: // Resurrection 2
                        return this.FirstName + "：しまった！対象を間違えたじゃねえか！！\r\n";
                    case 54: // Resurrection 3
                        return this.FirstName + "：しまった！死んでねぇぞ！！\r\n";
                    case 55: // Resurrection 4
                        return this.FirstName + "：おいおい、自分にやっても意味ねえからな・・・\r\n";
                    case 56: // Stance Of Standing
                        return this.FirstName + "：この体制のまま・・・っしゃ、攻撃だ！\r\n";
                    case 57: // Mirror Image
                        return this.FirstName + "：魔法をはね返すぜ、MirrorImage！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "：おっしゃ、はね返せ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "：おっしゃ、はね返せ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：バカな！？\r\n";
                    case 60: // Deflection
                        return this.FirstName + "：物理攻撃をはね返すぜ、Deflection！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "：おっしゃ、はね返せ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "：おっしゃ、はね返せ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：ッグ、グアァ！\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "：本質を見切ってやるぜ、TruthVision！　" + this.FirstName + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.FirstName + "：俺はこれ苦手だけどな・・・StanceOfFlow！　" + this.FirstName + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "：オラオラ、連発ラッシュいくぜ、CarnageRush！ ほらよ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "もういっちょ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "オラオラァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "まだまだまぁ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "食らええぇ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "：ガツンと一撃止めて見せるぜ、CrushingBlow！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.FirstName + "：これでも・・・食らいな！\r\n";
                    case 72: // Enigma Sence
                        return this.FirstName + "：力以外のチカラっての見せてやるぜ、EnigmaSennce！\r\n";
                    case 73: // Soul Infinity
                        return this.FirstName + "：俺の全てを注ぎ込むぜ！オラアアァァ！！ SoulInfinity！！！\r\n";
                    case 74: // Kinetic Smash
                        return this.FirstName + "：剣による攻撃、最大限にやってやるぜ！オラァ、KineticSmash！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "：・・・ックソ！詠唱失敗だ！！\r\n";
                    case 77: // Cleansing
                        return this.FirstName + "：妙な効果は全部これで洗い流すぜ、Cleansing！\r\n";
                    case 78: // Pure Purification
                        return this.FirstName + "：精神さえしっかり保てば直せるはずだ、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.FirstName + "：最大限界値、さらに超えるぜ。VoidExtraction！" + this.FirstName + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.FirstName + "：ほらよ、これで少しは目が醒めるだろ。\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "：氷付けにしてやるぜ！　AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "：知識も一つの力だぜ、PromiesdKnowledge！　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "：うっとおしい効果だな。消えちまいな、Tranquility！\r\n";
                    case 85: // High Emotionality 1
                        return this.FirstName + "：俺の最大能力はこんなものじゃねえ、ウオオォォ、HighEmotionality！\r\n";
                    case 86: // High Emotionality 2
                        return this.FirstName + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "：だ・・・だめだ、全く動けねえ！　スキル使用ミスったぜ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "：くそ・・・体が・・・防御できねえ！ \r\n";
                    case 89: // Silent Rush 1
                        return this.FirstName + "：この３連打、受けてみろ、SilentRush！ ほらよ！ {0}ダメージ・・・   ";
                    case 90: // Silent Rush 2
                        return "もういっちょ！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "３発目だ！おらぁ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.FirstName + "：スタン効果は俺には効かねえぜ、AntiStun！ " + this.FirstName + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.FirstName + "：ってぇ・・・だが、スタンしないぜ。\r\n";
                    case 95: // Stance Of Death
                        return this.FirstName + "：俺は耐えて見せるぜ、StanceOfDeath！ " + this.FirstName + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.FirstName + "：朧、刻むぜ【究極奥義】Oboro Impact！！\r\n";
                    case 97: // Oboro Impact 2
                        return this.FirstName + "：オラアアァァァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.FirstName + "：芯から破壊してやるぜ【究極奥義】Catastrophe！\r\n";
                    case 99: // Catastrophe 2
                        return this.FirstName + "：食らいやがれぇ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.FirstName + "：その行動、見切ってみせるぜ、 StanceOfEyes！ " + this.FirstName + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.FirstName + "：っしゃ、ソコだ！！　" + this.FirstName + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.FirstName + "：駄目だ・・・全然見切れねえ・・・　" + this.FirstName + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.FirstName + "：スペル詠唱なんかさせるかよ、Negate！　" + this.FirstName + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.FirstName + "：お見通しだぜ！" + this.FirstName + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.FirstName + "：っくそ、全然詠唱タイミングが・・・" + this.FirstName + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "：もうゼロに還したりさせねえぜ、【究極奥義】NothingOfNothingness！ " + this.FirstName + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.FirstName + "：行動原理の起源、刻み込んであるぜ、Genesis！  " + this.FirstName + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.FirstName + "：っくそ・・・調子が思わしくねえ、悪いがCleansingは出来ねえ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "：その構えはもう見飽きたぜ！\r\n";
                    case 111: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復したぜ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるぜ。一日１回だけだな。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.FirstName + "：おっしゃ、見切った！カウンターだ！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.FirstName + "：ックソ、どうなってんだ。見切れねえ！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "：防御なんてお見通しだぜ！貫通しろ！！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：っしゃ！クリティカルだぜ！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.FirstName + "：っしゃ！これで復活だぜ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "：っくそ・・・ライフが回復できねえ・・・\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.FirstName + "：っくそ・・・マナが回復できねえ・・・\r\n";
                    case 122: // 「ためる」行動時
                        return this.FirstName + "：魔力、蓄えさせてもらったぜ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.FirstName + "：魔力の蓄積はこれが上限みたいだな。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.FirstName + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.FirstName + "：アイテムゲージが溜まってないとアイテムは使えないぜ。\r\n";
                    case 126: // FlashBlase
                        return this.FirstName + "：この閃光でも食らいな、FlashBlaze！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.FirstName + "：っくそ、まだインスタント値が足りねぇ！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.FirstName + "：だめだ、これはインスタントで発動できねえ。\r\n";
                    case 129: // PsychicTrance
                        return this.FirstName + "：多少の犠牲を払ってでも魔法攻撃力、更に強化するぜ！PsychicTrance！\r\n";
                    case 130: // BlindJustice
                        return this.FirstName + "：打ち合い覚悟だ。物理攻撃、更に強化するぜ！BlindJustice！\r\n";
                    case 131: // TranscendentWish
                        return this.FirstName + "：この一瞬でケリをつける、TranscendentWish！\r\n";
                    case 132: // LightDetonator
                        return this.FirstName + "：そのフィールド全体、もらった！ LightDetonator！\r\n";
                    case 133: // AscendantMeteor
                        return this.FirstName + "：食らえ！怒涛の１０連撃、AscendantMeteor！\r\n";
                    case 134: // SkyShield
                        return this.FirstName + "：空と聖より加護を得るぜ、SkyShield！\r\n";
                    case 135: // SacredHeal
                        return this.FirstName + "：全員回復っと！　SacredHeal！\r\n";
                    case 136: // EverDroplet
                        return this.FirstName + "：マナの循環もらったぜ、EverDroplet！\r\n";
                    case 137: // FrozenAura
                        return this.FirstName + "：氷の魔法剣を発動するぜ、FrozenAura！\r\n";
                    case 138: // ChillBurn
                        return this.FirstName + "：凍りつけ！ ChillBurn！\r\n";
                    case 139: // ZetaExplosion
                        return this.FirstName + "：究極！ ZetaExplosion！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.FirstName + "：凍りつきな！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.FirstName + "：コレで気絶しな、StarLightning！\r\n";
                    case 142: // WordOfMalice
                        return this.FirstName + "：動きを鈍くさせるぜ、WordOfMalice！\r\n";
                    case 143: // BlackFire
                        return this.FirstName + "：魔法防御落としてやるぜ、BlackFire！\r\n";
                    case 144: // EnrageBlast
                        return this.FirstName + "：ブチかますぜ、EnrageBlast！\r\n";
                    case 145: // Immolate
                        return this.FirstName + "：物理防御落としてやるぜ、Immolate！\r\n";
                    case 146: // VanishWave
                        return this.FirstName + "：黙らせてやるぜ、VanishWave！\r\n";
                    case 147: // WordOfAttitude
                        return this.FirstName + "：インスタント回復させるぜ、WordOfAttitude！\r\n";
                    case 148: // HolyBreaker
                        return this.FirstName + "：これでダメージレースを優位にしてみせる。HolyBreaker！\r\n";
                    case 149: // DarkenField
                        return this.FirstName + "：防御力をガタ落ちにしてやるぜ、DarkenField！\r\n";
                    case 150: // SeventhMagic
                        return this.FirstName + "：今、全てを覆すぜ、SeventhMagic！\r\n";
                    case 151: // BlueBullet
                        return this.FirstName + "：氷の弾丸を連射するぜ、BlueBullet！\r\n";
                    case 152: // NeutralSmash
                        return this.FirstName + "：っしゃ、NeutralSmash！\r\n";
                    case 153: // SwiftStep
                        return this.FirstName + "：速度値上げてくぜ、SwiftStep！\r\n";
                    case 154: // CircleSlash
                        return this.FirstName + "：コレでも食らいな、CircleSlash！\r\n";
                    case 155: // RumbleShout
                        return this.FirstName + "：どこ見てる、コッチだ！\r\n";
                    case 156: // SmoothingMove
                        return this.FirstName + "：スライド式戦闘法、SmoothingMove！\r\n";
                    case 157: // FutureVision
                        return this.FirstName + "：次のターン、見切ったぜ。FutureVision！\r\n";
                    case 158: // ReflexSpirit
                        return this.FirstName + "：スタンや麻痺はゴメンだぜ、ReflexSpirit！\r\n";
                    case 159: // SharpGlare
                        return this.FirstName + "：黙らせてやるぜ、SharpGlare！\r\n";
                    case 160: // TrustSilence
                        return this.FirstName + "：沈黙や誘惑にかかってられるか、TrustSilence！\r\n";
                    case 161: // SurpriseAttack
                        return this.FirstName + "：全員蹴散らしてやるぜ、SurpriseAttack！\r\n";
                    case 162: // PsychicWave
                        return this.FirstName + "：この技は止められやしねえぜ、PsychicWave！\r\n";
                    case 163: // Recover
                        return this.FirstName + "：しっかりしろ！Recover！\r\n";
                    case 164: // ViolentSlash
                        return this.FirstName + "：見切れねえだろコイツは、ViolentSlash！\r\n";
                    case 165: // OuterInspiration
                        return this.FirstName + "：ポテンシャル、引き戻すぜ、OuterInspiration！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.FirstName + "：っしゃ、ソレだ！StanceOfSuddenness！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.FirstName + "：ダメだ、こいつはインスタント対象専用だ。\r\n";
                    case 168: // StanceOfMystic
                        return this.FirstName + "：もうダメージは喰らわねえ、StanceOfMystic！\r\n";
                    case 169: // HardestParry
                        return this.FirstName + "：瞬間で捉えてみせる。HardestParry！\r\n";
                    case 170: // ConcussiveHit
                        return this.FirstName + "：これでも食らいな、コンカッシヴ・ヒット！\r\n";
                    case 171: // Onslaught hit
                        return this.FirstName + "：これでも食らいな、オンスロート・ヒット！\r\n";
                    case 172: // Impulse hit
                        return this.FirstName + "：これでも食らいな、インパルス・ヒット！\r\n";
                    case 173: // Fatal Blow
                        return this.FirstName + "：砕け散れ、フェイタル・ブロー！\r\n";
                    case 174: // Exalted Field
                        return this.FirstName + "：賛美の場を構築するぜ、イグザルティッド・フィールド！\r\n";
                    case 175: // Rising Aura
                        return this.FirstName + "：ガンガン行くぜ、ライジング・オーラ！\r\n";
                    case 176: // Ascension Aura
                        return this.FirstName + "：どんどんぶちかますぜ、アセンション・オーラ！\r\n";
                    case 177: // Angel Breath
                        return this.FirstName + "：っしゃ、全員の状態を戻すぜ、エンジェル・ブレス！\r\n";
                    case 178: // Blazing Field
                        return this.FirstName + "：燃やし尽くすぜ、ブレイジング・フィールド！\r\n";
                    case 179: // Deep Mirror
                        return this.FirstName + "：その手は通さないぜ、ディープ・ミラー！\r\n";
                    case 180: // Abyss Eye
                        return this.FirstName + "：深淵の眼、アビス・アイ！\r\n";
                    case 181: // Doom Blade
                        return this.FirstName + "：精神まで切り刻むぜ、ドゥーム・ブレイド！\r\n";
                    case 182: // Piercing Flame
                        return this.FirstName + "：っしゃ、これでも食らいな！ピアッシング・フレイム！\r\n";
                    case 183: // Phantasmal Wind
                        return this.FirstName + "：反応上げていくぜ、ファンタズマル・ウィンド！\r\n";
                    case 184: // Paradox Image
                        return this.FirstName + "：・・・パラドックス・イメージ！\r\n";
                    case 185: // Vortex Field
                        return this.FirstName + "：この渦の中に埋もれさせてやるぜ、ヴォルテックス・フィールド！\r\n";
                    case 186: // Static Barrier
                        return this.FirstName + "：水と理より加護を得るぜ、スタティック・バリア！\r\n";
                    case 187: // Unknown Shock
                        return this.FirstName + "：盲目にしてやるぜ、アンノウン・ショック！\r\n";
                    case 188: // SoulExecution
                        return this.FirstName + "：奥義　ソウル・エグゼキューション！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.FirstName + "：ッラ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.FirstName + "：ラァ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.FirstName + "：ッシャ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.FirstName + "：ッティ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.FirstName + "：トォ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.FirstName + "：ッフ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.FirstName + "：ッハ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.FirstName + "：ッハァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.FirstName + "：オラァ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.FirstName + "：終わりだ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.FirstName + "：さらに回復していくぜ、ノリッシュ・センス！\r\n";
                    case 200: // Mind Killing
                        return this.FirstName + "：精神切り刻むぜ、マインド・キリング！\r\n";
                    case 201: // Vigor Sense
                        return this.FirstName + "：反応値上げていくぜ、ヴィゴー・センス！\r\n";
                    case 202: // ONE Authority
                        return this.FirstName + "：おっしゃ、全員上げていこうぜ！ワン・オーソリティ！\r\n";
                    case 203: // 集中と断絶
                        return this.FirstName + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.FirstName + "：【元核】は今日もう使っちまったぜ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.FirstName + "：【元核】はインスタントタイミング限定だな。\r\n";
                    case 206: // Sigil Of Homura
                        return this.FirstName + "：焔の威力、思い知れ！シギル・オブ・ホムラ！\r\n";
                    case 207: // Austerity Matrix
                        return this.FirstName + "：支配封じさせてもらうぜ、アゥステリティ・マトリクス！\r\n";
                    case 208: // Red Dragon Will
                        return this.FirstName + "：火の竜よ、俺に更なる力を！レッド・ドラゴン・ウィル！\r\n";
                    case 209: // Blue Dragon Will
                        return this.FirstName + "：水の竜よ、俺に更なる力を！ブルー・ドラゴン・ウィル！\r\n";
                    case 210: // Eclipse End
                        return this.FirstName + "：究極の全抹消、エクリプス・エンド！\r\n";
                    case 211: // Sin Fortune
                        return this.FirstName + "：次で決めるぜ、シン・フォーチュン！\r\n";
                    case 212: // AfterReviveHalf
                        return this.FirstName + "：死耐性（ハーフ）を付けるぜ！\r\n";
                    case 213: // Demonic Ignite
                        return this.FirstName + "：黒の焔をその身で受けろ！デーモニック・イグナイト！\r\n";
                    case 214: // Death Deny
                        return this.FirstName + "：死者を完全なる状態で蘇生させるぜ、デス・ディナイ！\r\n";
                    case 215: // Stance of Double
                        return this.FirstName + "：究極の行動原理、スタンス・オブ・ダブル！  " + this.FirstName + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.FirstName + "：まだだ・・・まだ、負けられねえんだ！！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.FirstName + "：・・・ッグ・・・っく・・・\r\n";
                    case 218: // インスタント不足
                        return this.FirstName + "：まだインスタントが足りねぇ・・・\r\n";
                        
                    case 2001: // ポーションまたは魔法による回復時
                        return this.FirstName + "：よし、{0} 回復したぜ。";
                    case 2002: // レベルアップ終了催促
                        return this.FirstName + "：レベルアップしてからにしようぜ。";
                    case 2003: // 荷物を減らせる催促
                        return this.FirstName + "：{0}、少し荷物を減らしておけよ。アイテムが渡せねえぞ。";
                    case 2004: // 装備判断
                        return this.FirstName + "：よし、装備しとくか？";
                    case 2005: // 装備完了
                        return this.FirstName + "：オーケー、装備完了！";
                    case 2006: // 遠見の青水晶を使用
                        return this.FirstName + "：いったん、町へ戻るか？";
                    case 2007: // 売却専用品に対する一言
                        return this.FirstName + "：売却専用品だな。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.FirstName + "：マナ不足だな。";
                    case 2009: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復したぜ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるぜ。一日１回だけだな。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.FirstName + "：戦闘中専用品だな。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.FirstName + "：すでに町の中だ。使うだけ無駄だな。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.FirstName + "：おいおい、さすがにこれを捨てたら駄目だろ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.FirstName + "：スキル不足だな。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.FirstName + "：おっと、それは捨ててもらっちゃ困るぜ。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.FirstName + "：っしゃ、復活だぜ！サンキュー！";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.FirstName + "：{0}はまだ生きてるじゃねえか。使用する必要はないぜ。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.FirstName + "：俺自身に使っても意味ないだろ。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.FirstName + "：俺には装備出来ないようだ。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.FirstName + "：いや、今コレは使うつもりはねえ";
                    case 2021: // アイテム捨てるの催促
                        return this.FirstName + "：バックパックの整理を先にしようぜ。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.FirstName + "：凄えぜ・・・身体のパラメタが再構築されていく・・・";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.FirstName + "：オーバーシフティングによる割り振りを早いトコやってしまおうぜ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.FirstName + "：【{0}】パラメタが {1} 上昇したぜ！";
                    case 2025:
                        return this.FirstName + "：両手武器を持ってる場合、サブは装備できねえぜ。";
                    case 2026:
                        return this.FirstName + "：武器（メイン）にまず何か装備しようぜ。";
                    case 2027: // 清透水使用時
                        return this.FirstName + "：ライフが100%回復したぜ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるぜ。一日１回だけだな。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.FirstName + "：バックパックがいっぱいだ。装備外す前にバックパック整理しようぜ。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.FirstName + "：{0}を捨てて新しいアイテムを入手するか？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.FirstName + "：戦闘中だし、アイテム使用に集中しようぜ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.FirstName + "：このアイテムは戦闘中に使用は出来ないぜ。";
                    case 2033: // 預けられない場合
                        return this.FirstName + "：これを預けておくわけにはいかねえよな。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.FirstName + "：おっと荷物がいっぱいだぜ。";
                    case 2035: // Sacred Heal
                        return this.FirstName + "：おし、回復したぜ。";
                    case 2036: // オーバーシフティング割り振り完了
                        return this.FirstName + "：っしゃ、再割り振り完了！";
                    case 2037: // １人のため、アイテムをわたす相手が居ない場合
                        return this.FirstName + "：今は俺一人だ、わたす相手はいないぜ。";

                    case 3000: // 店に入った時の台詞
                        return this.FirstName + "：本当に誰も見張り役が居ないんだな。";
                    case 3001: // 支払い要求時
                        return this.FirstName + "：おし、この {0} を買うぜ。 {1} Gold払えば良いんだな？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.FirstName + "：しまった、持ち物がいっぱいだ。手持ちのアイテムを整理してからだな。";
                    case 3003: // 購入完了時
                        return this.FirstName + "：よっしゃ、売買成立！・・・だよな？";
                    case 3004: // Gold不足で購入できない場合
                        return this.FirstName + "：クソ、Goldがまだ{0}足りねえ・・・";
                    case 3005: // 購入せずキャンセルした場合
                        return this.FirstName + "：他のアイテムでも見てみるか";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.FirstName + "：これは手放すわけにはいかねえな。";
                    case 3007: // アイテム売却時
                        return this.FirstName + "：相手がいねえが・・・{0}を置いて、{1}Gold・・・いただくぜ？";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.FirstName + "：コレはちょっと売るのは心がひけるが・・・{0}Goldいただくぜ？";
                    case 3009: // 武具店を出る時
                        return this.FirstName + "：ガンツ叔父さん、見張り人ぐらい付けるといいのにな・・・";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.FirstName + "：元々売ってるわけがねえんだよな・・・";
                    case 3011: // 装備可能なものが購入された時
                        return this.FirstName + "：っしゃ、ここで装備していくぜ？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.FirstName + "：今装備しているは、{0}だ。{1}Goldぐらいで買い取ってもらえるんじゃねえか？";

                    case 4001: // 通常攻撃を選択
                        return this.FirstName + "：っしゃ、攻撃だ！\r\n";
                    case 4002: // 防御を選択
                        return this.FirstName + "：防御姿勢で構えておくか。\r\n";
                    case 4003: // 待機を選択
                        return this.FirstName + "：何もせず待機と行くか・・・\r\n";
                    case 4004: // フレッシュヒールを選択
                        return this.FirstName + "：ここはフレッシュヒールで回復だ。\r\n";
                    case 4005: // プロテクションを選択
                        return this.FirstName + "：防御力上げるぜ、プロテクションだ。\r\n";
                    case 4006: // ファイア・ボールを選択
                        return this.FirstName + "：ファイアボールだ！\r\n";
                    case 4007: // フレイム・オーラを選択
                        return this.FirstName + "：ここは、フレイムを付けておくぜ。\r\n";
                    case 4008: // ストレート・スマッシュを選択
                        return this.FirstName + "：いくぜ！ストレートスマッシュ！\r\n";
                    case 4009: // ダブル・スマッシュを選択
                        return this.FirstName + "：２連撃いくぜ、ダブルスマッシュ！\r\n";
                    case 4010: // スタンス・オブ・スタンディングを選択
                        return this.FirstName + "：守って攻める、スタンディングの構えだ。\r\n";
                    case 4011: // アイス・ニードルを選択
                        return this.FirstName + "：アイスニードルでいくぜ！\r\n";
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.FirstName + "：" + this.ActionLabel.text + "でいくぜ。\r\n";

                    case 4072:
                        return this.FirstName + "：これは敵にかけられないぜ。\r\n";
                    case 4073:
                        return this.FirstName + "：これは敵にかけられないぜ。\r\n";
                    case 4074:
                        return this.FirstName + "：これは味方にかけたくはねえ。\r\n";
                    case 4075:
                        return this.FirstName + "：これは味方にかけたくはねえ。\r\n";
                    case 4076:
                        return this.FirstName + "：味方に攻撃は仕掛けられないぜ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.FirstName + "：パワーをためるぜ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.FirstName + "：メイン武器の効果を発動させるぜ。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.FirstName + "：サブ武器の効果を発動させるぜ。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.FirstName + "：アクセサリ１の効果を発動させるぜ。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.FirstName + "：アクセサリ２の効果を発動させるぜ。\r\n";

                    case 4082: // FlashBlaze
                        return this.FirstName + "：フラッシュブレイズいくぜ！\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.FirstName + "：これでも食らいな！エアロ・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.FirstName + "：{0} 回復っと！ \r\n";
                    case 5003:
                        return this.FirstName + "：{0}マナ回復っと！\r\n";
                    case 5004:
                        return this.FirstName + "：凍りつけ！アイシクル・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.FirstName + "：砕けろ！エレクトロ・ブロー！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.FirstName + "：これでも食らいな！ブルー・ライトニング！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.FirstName + "：これでも食らいな！バーニング・クレイモア！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.FirstName + "：この蒼の炎でも食らうんだな！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.FirstName + "：{0}スキルポイント回復っと！\r\n";
                    case 5010:
                        return this.FirstName + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ラナ"
            else if (this.FirstName == "ラナ")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "：っちょっと、スキルポイントが足りないじゃない！\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "：いくわよ。\r\n";
                    case 2: // Double Slash 1
                        return this.FirstName + "：ハイッ！\r\n";
                    case 3: // Double Slash 2
                        return this.FirstName + "：セイッ！\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "：【心眼奥義】終わらない痛み、受け続けるが良いわ。\r\n";
                    case 5: // empty skill
                        return this.FirstName + "：っちょっと、スキル選択してないじゃない！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "：痛っ！　辛いわね・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + ": 駄目・・・私じゃ全然追えないわ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "：っよ、避け切れない！ッキャアアァ！！！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 回復ね。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "：炎の玉、FireBallよ。 {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "：炎の槍、FlameStrikeよ。 {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "：真紅の業火、VolcanicWaveよ。 {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：クリティカルヒットよ！ {0} へ {1} のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "：フレイム！ {0}の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "：戦闘中に使っても意味ないのよね。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "：ホント、使えないアイテムね。何も起きないわ。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "：あ、マナ不足だったわ。\r\n";
                    case 18: // Protection
                        return this.FirstName + "：聖なる神による加護、Protectionよ。物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：水の女神による加護・・・AbsorbWaterよ。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：聖なる神よ我に力を、SaintPower。 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：闇との契約、ShadowPactよ。 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：闇の使者による加護・・・BloodyVengeanceよ。 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "：ハンマーでドカンっとね♪ HolyShock！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：栄光の光を照らせ、Gloryよ。 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：聖なる光で包み込むわ、CelestialNovaよ。 {0} 回復ね。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "：聖なる裁きの光、CelestialNovaよ。 {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "：黒い波動、耐えられるかしら、DarkBlastよ。 {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "：これが私の炎授天使様よ、LavaAnnihilation！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.FirstName + "：これが私の炎授天使様よ、LavaAnnihilation！\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "：体力を頂くとするわ、DevouringPlagueよ。 {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "：それっ、氷の針よ♪ IceNeedle。 {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "：凍結された槍、受けてみなさい、FrozenLanceよ。 {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "：大地をも飲み込む壮大なる唸り、TidalWaveよ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "：言葉にも力はあるわ、WordOfPowerよ。 {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "：五感の全てが消え去る瞬間、WhiteOutよ。 {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：悪魔からの確約された力、BlackContractよ。 " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：エンチャント炎って便利ね、FlameAuraよ。 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：闇の深遠、逃れられないわよ、Damnation！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：炎授天使より加護を得る、HeatBoostよ。 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：炎術演舞、ImmortalRaveよ！ " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：真実の幻影、GaleWindよ。 もう一人の" + this.FirstName + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.FirstName + "：永久なる自然の恩恵、WordOfLifeよ。 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "：強き理、未来、確定のWordOfFortuneよ。 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.FirstName + "：創造上の物理使わせてもらうわ、AetherDrive！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "：新しき創造と原理構築、EternalPresenceよ。 " + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.FirstName + "：空間の壁ってとこかしら♪ OneImmunityよ。 " + this.FirstName + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.FirstName + "：時空断裂、認識自体を飛ばすわよ、TimeStop！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.FirstName + "：永続的効果は無に帰るべきね、DispelMagicよ。 \r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "：時空の支配者より加護を得る、RiseOfImageよ。 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.FirstName + "：っちょっと、空詠唱じゃないの！\r\n";
                    case 51: // Inner Inspiration
                        return this.FirstName + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.FirstName + "：聖の力は奇跡すら起こせるのよ、Resurrection！！\r\n";
                    case 53: // Resurrection 2
                        return this.FirstName + "：っちょっと、対象間違えちゃったじゃない！！\r\n";
                    case 54: // Resurrection 3
                        return this.FirstName + "：あ、生きてた・・・ゴメンね♪\r\n";
                    case 55: // Resurrection 4
                        return this.FirstName + "：自分にやっても効果ないのよね・・・\r\n";
                    case 56: // Stance Of Standing
                        return this.FirstName + "：防御体制を維持したまま・・・攻撃よ！\r\n";
                    case 57: // Mirror Image
                        return this.FirstName + "：純蒼の女神より魔法反射の加護を得る、MirrorImageよ。{0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "：来たわ、はね返すのよ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "：来たわ、はね返すのよ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：っう、ウソ！？\r\n";
                    case 60: // Deflection
                        return this.FirstName + "：純白の使者より物理反射の加護を得る、Deflectionよ。 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "：来たわ、はね返すのよ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "：来たわ、はね返すのよ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：キャアアッ！！\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "：本質さえ見えれば怖くないわ、TruthVisionよ。　" + this.FirstName + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.FirstName + "：後手必勝、必ず会得してみせるわ、StanceOfFlowよ。　" + this.FirstName + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "：この連続コンボ、耐えられるかしら？CarnageRushよ。 ッハイ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ッセイ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "ヤァァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "ッフ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "ハアアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "：この一撃で動きを止めて見せるわ。CrushingBlowよ。  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.FirstName + "：ッフフ、これでも食らいなさい。\r\n";
                    case 72: // Enigma Sence
                        return this.FirstName + "：力の源は人によって違うのよ、EnigmaSence！\r\n";
                    case 73: // Soul Infinity
                        return this.FirstName + "：私の能力を全て使って叩き込むわ。ッハアアァァァ・・・SoulInfinity！！\r\n";
                    case 74: // Kinetic Smash
                        return this.FirstName + "：私の拳、最大限の運動性を引き出してみせるわ、KineticSmashよ！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "：・・・ッダメ！詠唱できなかったわ！！\r\n";
                    case 77: // Cleansing
                        return this.FirstName + "：純蒼の女神による浄化、Cleansingよ。\r\n";
                    case 78: // Pure Purification
                        return this.FirstName + "：精神潜在からの浄化、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.FirstName + "：最大の能力、最大限に引き出すわ、VoidExtractionよ。" + this.FirstName + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.FirstName + "：っそれ、これで気付けになるわよ。\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "：氷の女神より絶対零度を受けなさい。AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "：雪の女神よ、聡明なる知恵を授けたまえ、PromiesdKnowledgeよ。　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "：その効果さえ、消えてしまうがいいわ！ Tranquilityよ！\r\n";
                    case 85: // High Emotionality 1
                        return this.FirstName + "：私の潜在能力はこんなものじゃないわ、HighEmotionalityよ！\r\n";
                    case 86: // High Emotionality 2
                        return this.FirstName + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "：全然・・・動けない・・・、スキル使用できないわ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "：防御の・・・構えが取れないわ！ \r\n";
                    case 89: // Silent Rush 1
                        return this.FirstName + "：動きを最小限に抑えた連続攻撃、SilentRushよ。　ッハイ！ {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "ッセイ！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "ヤアァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.FirstName + "：この構えでスタンを防ぐわ、AntiStunよ。 " + this.FirstName + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.FirstName + "：っく、痛いわね。でもスタンは避けたわ。\r\n";
                    case 95: // Stance Of Death
                        return this.FirstName + "：そう簡単にやられないわよ、StanceOfDeath！ " + this.FirstName + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.FirstName + "：朧の容、【究極奥義】Oboro Impactよ！\r\n";
                    case 97: // Oboro Impact 2
                        return this.FirstName + "：ッハアァァ・・・ッセイ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.FirstName + "：身体の根幹へと伝わる・・・【究極奥義】Catastropheよ！\r\n";
                    case 99: // Catastrophe 2
                        return this.FirstName + "：ッセエエエエェ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.FirstName + "：行動には必ずモーションがあるはず、 StanceOfEyesよ。 " + this.FirstName + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.FirstName + "：っそれね！！　" + this.FirstName + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.FirstName + "：っうそ・・・動いた形跡が無いわ・・・　" + this.FirstName + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.FirstName + "：詠唱なんかさせないわ、Negateよ。" + this.FirstName + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.FirstName + "：詠唱するとこ、見つけたわ！" + this.FirstName + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.FirstName + "：っうそ・・・詠唱タイミングがわからない・・・" + this.FirstName + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "：これが、完全なる否定魔法【究極奥義】NothingOfNothingnessよ！ " + this.FirstName + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.FirstName + "：全ての行動をもう一度呼び起こすわ、Genesisよ。  " + this.FirstName + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.FirstName + "：っう・・・駄目、調子が悪くてCleansingは出来そうにないわ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "：甘いわね、その構えは既にお見通しよ。\r\n";
                    case 111: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復したわよ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるわ。一日１回だけのようね。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.FirstName + "：その動きもらったわ、カウンターよ。\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.FirstName + "：駄目・・・見切れないわ。\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "：防御の構えは無駄よ！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：クリティカルヒットよ！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.FirstName + "：このポーションで復活よ。ッハイ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "：ライフが・・・回復できないわ・・・\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.FirstName + "：マナが・・・回復できないわ・・・\r\n";
                    case 122: // 「ためる」行動時
                        return this.FirstName + "：魔力、蓄えさせてもらったわよ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.FirstName + "：これ以上、魔力は蓄えられないみたいね。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.FirstName + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.FirstName + "：アイテムゲージが溜まってないわね。アイテムはまだ使えないわよ。\r\n";
                    case 126: // FlashBlase
                        return this.FirstName + "：聖なる炎で焼いてあげるわ、FlashBlazeよ。 {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.FirstName + "：っあ！インスタント値足りないじゃない！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.FirstName + "：インスタントタイミングじゃ発動できないわ。\r\n";
                    case 129: // PsychicTrance
                        return this.FirstName + "：少し怖いけど・・・魔法攻撃力強化よ、PsychicTrance。\r\n";
                    case 130: // BlindJustice
                        return this.FirstName + "：この魔法危険だけど・・・物理攻撃強化よ、BlindJustice。\r\n";
                    case 131: // TranscendentWish
                        return this.FirstName + "：お願い、ケリを付けさせてちょうだい、TranscendentWishよ\r\n";
                    case 132: // LightDetonator
                        return this.FirstName + "：ここね、フィールド展開！ LightDetonatorよ\r\n";
                    case 133: // AscendantMeteor
                        return this.FirstName + "：アッハハハ、死ねば良いわ。　AscendantMeteorよ\r\n";
                    case 134: // SkyShield
                        return this.FirstName + "：空と聖から加護を受けるわ、SkyShieldよ\r\n";
                    case 135: // SacredHeal
                        return this.FirstName + "：全員回復させるわ、SacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.FirstName + "：これでマナ枯渇の心配は不要ね、EverDropletよ\r\n";
                    case 137: // FrozenAura
                        return this.FirstName + "：氷属性を付与するわ、FrozenAura。\r\n";
                    case 138: // ChillBurn
                        return this.FirstName + "：凍結してちょうだい、ChillBurn。\r\n";
                    case 139: // ZetaExplosion
                        return this.FirstName + "：古の禁呪、ZetaExplosionよ！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.FirstName + "：凍って！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.FirstName + "：お願い。気絶して、StarLightningよ！\r\n";
                    case 142: // WordOfMalice
                        return this.FirstName + "：動きを鈍化させてあげるわ、WordOfMalice。\r\n";
                    case 143: // BlackFire
                        return this.FirstName + "：魔法防御を劣化させてあげるわ、BlackFire。\r\n";
                    case 144: // EnrageBlast
                        return this.FirstName + "：打ち上げ花火♪　EnrageBlast♪　\r\n";
                    case 145: // Immolate
                        return this.FirstName + "：物理防御を劣化させてあげるわ、Immolate。\r\n";
                    case 146: // VanishWave
                        return this.FirstName + "：少し黙っててちょうだい、VanishWave！\r\n";
                    case 147: // WordOfAttitude
                        return this.FirstName + "：インスタント回復させるわ、WordOfAttitudeよ。\r\n";
                    case 148: // HolyBreaker
                        return this.FirstName + "：ダメージの引き換えをあげるわ、HolyBreaker！\r\n";
                    case 149: // DarkenField
                        return this.FirstName + "：防御力を全面ダウンさせてあげる、DarkenField！\r\n";
                    case 150: // SeventhMagic
                        return this.FirstName + "：原点の反転を行うわ、SeventhMagic！\r\n";
                    case 151: // BlueBullet
                        return this.FirstName + "：氷魔法を連射するわ、BlueBulletよ。\r\n";
                    case 152: // NeutralSmash
                        return this.FirstName + "：NeutralSmashよ、ハイ！\r\n";
                    case 153: // SwiftStep
                        return this.FirstName + "：戦闘の速度を上げるわよ、SwiftStep。\r\n";
                    case 154: // CircleSlash
                        return this.FirstName + "：みんな邪魔よ、CircleSlash！\r\n";
                    case 155: // RumbleShout
                        return this.FirstName + "：どこ見てんのよ！コッチよ！\r\n";
                    case 156: // SmoothingMove
                        return this.FirstName + "：流れるように攻められるわ、SmoothingMove！\r\n";
                    case 157: // FutureVision
                        return this.FirstName + "：次のターン、何もさせないわよ、FutureVision！\r\n";
                    case 158: // ReflexSpirit
                        return this.FirstName + "：スタン系は絶対回避するわ、ReflexSpiritよ。\r\n";
                    case 159: // SharpGlare
                        return this.FirstName + "：少し黙っててちょうだい、SharpGlareよ。\r\n";
                    case 160: // TrustSilence
                        return this.FirstName + "：沈黙や誘惑とか面倒だしね、TrustSilenceよ。\r\n";
                    case 161: // SurpriseAttack
                        return this.FirstName + "：全員吹っ飛ばしてあげるわ、SurpriseAttackよ！\r\n";
                    case 162: // PsychicWave
                        return this.FirstName + "：この技は止められるかしら、PsychicWaveよ。\r\n";
                    case 163: // Recover
                        return this.FirstName + "：しっかりしてよね、Recoverよ。\r\n";
                    case 164: // ViolentSlash
                        return this.FirstName + "：ッフフ、見きれるかしら、ViolentSlashよ！\r\n";
                    case 165: // OuterInspiration
                        return this.FirstName + "：ステータスを元通りに、OuterInspiration！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.FirstName + "：ッココね！StanceOfSuddenness！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.FirstName + "：これはインスタント対象専用よね。\r\n";
                    case 168: // StanceOfMystic
                        return this.FirstName + "：当てようとしても、もう無駄よ。StanceOfMystic！\r\n";
                    case 169: // HardestParry
                        return this.FirstName + "：その攻撃、捉えてみせるわ。HardestParryよ。\r\n";
                    case 170: // ConcussiveHit
                        return this.FirstName + "：食らいなさい、ConcussiveHit！\r\n";
                    case 171: // Onslaught hit
                        return this.FirstName + "：食らいなさい、OnslaughtHit！\r\n";
                    case 172: // Impulse hit
                        return this.FirstName + "：食らいなさい、ImpulseHit！\r\n";
                    case 173: // Fatal Blow
                        return this.FirstName + "：これで終わりよ、FatalBlow！\r\n";
                    case 174: // Exalted Field
                        return this.FirstName + "：賛美の声を集うわ、ExaltedField！\r\n";
                    case 175: // Rising Aura
                        return this.FirstName + "：物理攻撃、上げて行くわよ。RisingAura！\r\n";
                    case 176: // Ascension Aura
                        return this.FirstName + "：魔法攻撃、上げて行くわよ。AscensionAura！\r\n";
                    case 177: // Angel Breath
                        return this.FirstName + "：みんな頑張って、元の状態に。　AngelBreath\r\n";
                    case 178: // Blazing Field
                        return this.FirstName + "：この炎で燃やしてあげるわ、BlazingField！\r\n";
                    case 179: // Deep Mirror
                        return this.FirstName + "：それは通さないわよ、DeepMirror！\r\n";
                    case 180: // Abyss Eye
                        return this.FirstName + "：深淵の眼から逃れられないわよ、AbyssEye！\r\n";
                    case 181: // Doom Blade
                        return this.FirstName + "：精神力もろともいただくわ、DoomBlade！\r\n";
                    case 182: // Piercing Flame
                        return this.FirstName + "：貫通の火を打ち込んであげるわ、PiercingFlame！\r\n";
                    case 183: // Phantasmal Wind
                        return this.FirstName + "：反応力を上げるわ、PhantasmalWindよ。\r\n";
                    case 184: // Paradox Image
                        return this.FirstName + "：潜在能力を引き出すわ、ParadoxImageよ。\r\n";
                    case 185: // Vortex Field
                        return this.FirstName + "：これで皆を鈍足にするわね、VortexField。\r\n";
                    case 186: // Static Barrier
                        return this.FirstName + "：水と理から加護を受けるわ、StaticBarrierよ\r\n";
                    case 187: // Unknown Shock
                        return this.FirstName + "：真っ暗な中で戦うといいわ、UnknownShockよ\r\n";
                    case 188: // SoulExecution
                        return this.FirstName + "：行くわよ、奥義　SoulExecusion！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.FirstName + "：ッセィ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.FirstName + "：ッハ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.FirstName + "：ッハイ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.FirstName + "：ッフ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.FirstName + "：ッセイ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.FirstName + "：ッハ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.FirstName + "：ッフ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.FirstName + "：ッハアァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.FirstName + "：セエェェィ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.FirstName + "：決めるわ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.FirstName + "：回復量をあげていくわ、NourishSenseよ。\r\n";
                    case 200: // Mind Killing
                        return this.FirstName + "：精神を切り刻んであげるわ、MindKilling！\r\n";
                    case 201: // Vigor Sense
                        return this.FirstName + "：反応値上げるわよ、VigorSense！\r\n";
                    case 202: // ONE Authority
                        return this.FirstName + "：みんな、上げていくわよ。OneAuthority！\r\n";
                    case 203: // 集中と断絶
                        return this.FirstName + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.FirstName + "：【元核】は今日もう使ってしまってるわ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.FirstName + "：【元核】はインスタントタイミングで使用するものよ。\r\n";
                    case 206: // Sigil Of Homura
                        return this.FirstName + "：焔の印を受けなさい、SigilOfHomura！\r\n";
                    case 207: // Austerity Matrix
                        return this.FirstName + "：支配力を切らせてもらうわ、AusterityMatrixよ。\r\n";
                    case 208: // Red Dragon Will
                        return this.FirstName + "：火竜よ、私に力を与えよ、RedDragonWill！\r\n";
                    case 209: // Blue Dragon Will
                        return this.FirstName + "：水竜よ、私に力を与えよ、BlueDragonWill！\r\n";
                    case 210: // Eclipse End
                        return this.FirstName + "：全てを抹消せし無を今ここに、EclipseEnd！\r\n";
                    case 211: // Sin Fortune
                        return this.FirstName + "：次のヒットで決めてみせるわ、SinFortuneよ。\r\n";
                    case 212: // AfterReviveHalf
                        return this.FirstName + "：死耐性（ハーフ）を付与するわね。\r\n";
                    case 213: // Demonic Ignite
                        return this.FirstName + "：黒炎から逃れられないわよ、DemonicIgnite！\r\n";
                    case 214: // Death Deny
                        return this.FirstName + "：死者を完全に復活させるわよ、DeathDeny！\r\n";
                    case 215: // Stance of Double
                        return this.FirstName + "：究極行動原理、StanceOfDouble！  " + this.FirstName + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.FirstName + "：うっ・・・まだよ・・・まだ倒れるわけにはいかないわ！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.FirstName + "：・・・ご・・・ごめん・・・な・・さ・・・\r\n";
                    case 218: // インスタント不足
                        return this.FirstName + "：まだインスタント足りないわね・・・\r\n";

                    case 1001: // Home Town 1 コミュニケーション済で、休む前のアイン一人を対象
                        return this.FirstName + "：今日はもう休んで、明日に備えたら？";
                    case 1002: // Home Town 2 コミュニケーション済で、休んだ後のアイン一人を対象
                        return this.FirstName + "：ホラホラ、とっとと行って来い♪";
                    case 1003: // Home Town 1 コミュニケーション済で、休む前のアイン・ラナ２人を対象
                        return this.FirstName + "：じゃ、私は一旦戻るとするわね。明日に備えて休みましょ。";
                    case 1004: // Home Town 2 コミュニケーション済で、休んだ後のアイン・ラナ２人を対象
                        return this.FirstName + "：準備が出来たら、とっとと行くわよ♪";

                    case 2001: // ポーション回復時
                        return this.FirstName + "：{0} 回復したわよ。";
                    case 2002: // レベルアップ終了催促
                        return this.FirstName + "：レベルアップが先ね。";
                    case 2003: // 荷物を減らせる催促
                        return this.FirstName + "：{0}、荷物を減らしておいたら？アイテムが渡せないわよ。";
                    case 2004: // 装備判断
                        return this.FirstName + "：じゃ、装備しようか？";
                    case 2005: // 装備完了
                        return this.FirstName + "：装備完了♪";
                    case 2006: // 遠見の青水晶を使用
                        return this.FirstName + "：町に戻るとしますか♪";
                    case 2007: // 売却専用品に対する一言
                        return this.FirstName + "：売却専用の品物ね。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.FirstName + "：マナが不足してるわね。";
                    case 2009: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復したわよ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるわ。一日１回だけのようね。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.FirstName + "：戦闘中専用品のようね。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.FirstName + "：今は町の中にいるから使っても意味ないわよ。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.FirstName + "：これを捨てたら私達、酷い目に会うわよ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.FirstName + "：スキルが不足してるわね。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.FirstName + "：あ、ちょっと、それは捨てないでよ。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.FirstName + "：よし、復活できたわ。ホントありがと♪";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.FirstName + "：{0}はまだ死んではいないわ。使用する必要はなさそうね。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.FirstName + "：私はまだ生きてるわよ。失礼ね。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.FirstName + "：私じゃ装備できないみたいね。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.FirstName + "：ごめんなさい、今はもうこれは使わないつもりだから。";
                    case 2021: // アイテム捨てるの催促
                        return this.FirstName + "：バックパックの整理が先よね。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.FirstName + "：凄いわ・・・身体能力が再構築されていくのを感じるわ。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.FirstName + "：オーバーシフティングの割り振りを先にしましょ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.FirstName + "：【{0}】パラメタが {1} 上昇したわ♪";
                    case 2025:
                        return this.FirstName + "：今は両手武器を装備しているわ。サブは装備できないわね。";
                    case 2026:
                        return this.FirstName + "：武器（メイン）に何か装備してからにしましょ。";
                    case 2027: // 清透水使用時
                        return this.FirstName + "：ライフが100%回復したわよ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるわ。一日１回だけのようね。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.FirstName + "：バックパックがいっぱいみたいだわ。装備は外せないわね。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.FirstName + "：{0}を捨てるわね？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.FirstName + "：今は戦闘中よ。他のパラメタなんか探ってる余裕は無いわ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.FirstName + "：このアイテムは戦闘中に使用は出来ないみたいね。";
                    case 2033: // 預けられない場合
                        return this.FirstName + "：う～ん、ちょっとこれは手元から外せないわね。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.FirstName + "：う～ん、荷物はもういっぱいみたいね。";
                    case 2035: // Sacred Heal
                        return this.FirstName + "：うん、全員回復したわよ。";
                    case 2036: // オーバーシフティング割り振り完了
                        return this.FirstName + "：再割り振り完了よ♪";
                    case 2037: // １人のため、アイテムをわたす相手が居ない場合
                        return this.FirstName + "：私一人だから、わたす事はできないわね。";

                    case 3000: // 店に入った時の台詞
                        return this.FirstName + "：ホンット誰もいないわね。";
                    case 3001: // 支払い要求時
                        return this.FirstName + "：私、この{0}が欲しいわ。 {1}Gold置いてくわよ？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.FirstName + "：持ち物がいっぱいのようね。少しアイテム整理するわ。";
                    case 3003: // 購入完了時
                        return this.FirstName + "：これで売買成立してるわよね？ガンツ叔父さん。";
                    case 3004: // Gold不足で購入できない場合
                        return this.FirstName + "：Goldが{0}足りないわね・・・";
                    case 3005: // 購入せずキャンセルした場合
                        return this.FirstName + "：他のアイテムも見て回りましょ。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.FirstName + "：このアイテムは手放せないわね。";
                    case 3007: // アイテム売却時
                        return this.FirstName + "：{0}を置いてくわよ、{1}Goldもらって良いはずよね？";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.FirstName + "：ちょっとコレせっかく私が作ったものよ。まあ、{0}Goldぐらいはもらえそうだけど？";
                    case 3009: // 武具店を出る時
                        return this.FirstName + "：ガンツ叔父さん、時空のルーツ見つかるといいわね。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.FirstName + "：この剣は元々売り切れって話じゃないわよね・・・";
                    case 3011: // 装備可能なものが購入された時
                        return this.FirstName + "：ここで装備していくわよ。良いわよね？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.FirstName + "：っとと、今装備してる{0}も売っておきたいわね。{1}Goldぐらいよね、ガンツ叔父さん？";

                    case 3013: // 店の担当者としてお迎えの挨拶
                        return this.FirstName + "：どうぞお買い求めください♪";
                    case 3014: // 店の担当者としてお別れの挨拶
                        return this.FirstName + "：また、おいでくださいませ♪";
                    case 3015: // 店の担当者としてお買い上げをヒアリングするとき
                        return this.FirstName + "：{0}ですね。{1}Goldですが、お買い上げになりますか？";
                    case 3016: // 店の担当者として、Goldが不足してるときの台詞
                        return this.FirstName + "：すいませんが、後{0}Goldだけ不足しております。";
                    case 3017: // 店の担当者として、買い手が購入決定・完了したとき
                        return this.FirstName + "：ありがとうございました♪";
                    case 3018: // 買い手の持ち物がいっぱいである事をお伝えするとき
                        return this.FirstName + "：あの、すいませんが荷物がいっぱいのようです。";
                    case 3019: // 買い手が購入せずキャンセルされた場合
                        return this.FirstName + "：他にも良ければ、見て行ってくださいませ♪";
                    case 3020: // 買い手が売却不可能なものを提示してきた場合
                        return this.FirstName + "：すいませんが、その品物は買取りができません。";
                    case 3021: // 買い手が売却可能なものを提示してきた場合
                        return this.FirstName + "：{0}ですね。{1}Goldでの買取らせていただきましょうか？";

                    case 4001: // 通常攻撃を選択
                        return this.FirstName + "：普通に攻撃ね。\r\n";
                    case 4002: // 防御を選択
                        return this.FirstName + "：危ないときは防御かな。\r\n";
                    case 4003: // 待機を選択
                        return this.FirstName + "：待機で次に備えるわよ。\r\n";
                    case 4004: // フレッシュヒールを選択
                        return this.FirstName + "：フレッシュヒールで行こうかしら。\r\n";
                    case 4005: // プロテクションを選択
                        return this.FirstName + "：防御力ＵＰ、プロテクションよ。\r\n";
                    case 4006: // ファイア・ボールを選択
                        return this.FirstName + "：ファイアボールを撃ち込むわよ。\r\n";
                    case 4007: // フレイム・オーラを選択
                        return this.FirstName + "：フレイム属性攻撃の準備ね。\r\n";
                    case 4008: // ストレート・スマッシュを選択
                        return this.FirstName + "：次、ストレートスマッシュ行くわよ。\r\n";
                    case 4009: // ダブル・スマッシュを選択
                        return this.FirstName + "：２回攻撃、ダブルスマッシュ行くわよ。\r\n";
                    case 4010: // スタンス・オブ・スタンディングを選択
                        return this.FirstName + "：スタンディングの構え。守って攻めるわよ。\r\n";
                    case 4011: // アイス・ニードルを選択
                        return this.FirstName + "：アイスニードルを撃ち込むわよ。\r\n";
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.FirstName + "：" + this.ActionLabel.text + "にするわね。\r\n";
                    case 4072:
                        return this.FirstName + "：敵に放ちたくないわね。\r\n";
                    case 4073:
                        return this.FirstName + "：敵に放ちたくないわね。\r\n";
                    case 4074:
                        return this.FirstName + "：味方に放つわけには行かないわ。\r\n";
                    case 4075:
                        return this.FirstName + "：味方に放つわけには行かないわ。\r\n";
                    case 4076:
                        return this.FirstName + "：味方に攻撃するわけには行かないわ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.FirstName + "：魔力をためるわ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.FirstName + "：メイン武器の効果を発動させるわね。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.FirstName + "：サブ武器の効果を発動させるわね。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.FirstName + "：アクセサリ１の効果を発動させるわね。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.FirstName + "：アクセサリ２の効果を発動させるわね。\r\n";

                    case 4082: // FlashBlaze
                        return this.FirstName + "：フラッシュブレイズで行こうかしら。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.FirstName + "：風で切り裂くわよ、エアロ・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.FirstName + "：{0} 回復よ \r\n";
                    case 5003:
                        return this.FirstName + "：{0} マナ回復よ \r\n";
                    case 5004:
                        return this.FirstName + "：凍りつくが良いわ！アイシクル・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.FirstName + "：行くわよ！エレクトロ・ブロー！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.FirstName + "：いくわよ！ブルー・ライトニング！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.FirstName + "：いくわよ！バーニング・クレイモア！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.FirstName + "：赤蒼授からの炎、食らうわがいいわ！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.FirstName + "：{0} スキルポイント回復よ \r\n";
                    case 5010:
                        return this.FirstName + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ヴェルゼ"
            else if (this.FirstName == "ヴェルゼ" || this.FirstName == "ヴェルゼ・アーティ")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "：スキルポイントが足りませんね。\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "：ッハアアァァァァァ！！！\r\n";
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.FirstName + "：ひとつ\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.FirstName + "：ふたつ\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "：【心眼奥義】あなたには分からないでしょう、無限の苦しみ。\r\n";
                    case 5: // empty skill
                        return this.FirstName + "：スキル選択ミスですね。\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "：さて、これは・・・なかなか。\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + ": このボクでさえ・・・まったく見えません・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "：こ・・・この剣、強すぎ・・る・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 回復です。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "：軽くどうでしょう、FireBallです。 {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "：こんな赤いものはいかがでしょう、FlameStrikeです。 {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "：業火ぐらい大した事ないでしょう？ VolcanicWaveです。 {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：クリティカルヒットです。ッハアアァァァ！！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "：火の粉です。 {0}の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "：これは戦闘中では使えませんね。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "：ッチ、役立たずアイテムが。　こんなもの使えねえ！！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "：マナが不足していますね。\r\n";
                    case 18: // Protection
                        return this.FirstName + "：聖の防御円、Protectionです。物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：水の防御円、AbsorbWaterです。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：聖の攻撃円、SaintPowerです。 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：ククッ、闇からの力、ShadowPactです。 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：闇の使者がボクに力をくれる。BloodyVengeanceです。 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "：鉄槌なんていかがですか、HolyShockです。 {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：誰でも光輝く時代があったんですよ。Gloryです。 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：光による癒し、CelestialNovaです。 {0} 回復です。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "：ッハッハハハ！裁き食らえやあぁぁぁ！　CelestialNova！！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "：ククク、闇の波動こそ全て、DarkBlastです。 {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "：炎授天使ぐらい造作も無い、LavaAnnihilationです。 {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.FirstName + "：炎授天使ぐらい造作も無い、LavaAnnihilationです。\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "：体力吸収だ、死ね死ね死ねぇ！DevouringPlague！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "：無数の氷でも食らってください、IceNeedleです。 {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "：どうです、氷の槍で串刺しってのは？　FrozenLanceです。 {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "：水に呑まれて死んじまえよおおぉぉ！！！TidalWave！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "：あなたこの魔法止めれませんよ、WordOfPowerです。 {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "：五・・・五感があああぁぁぁぁぁ！　WhiteOut！！！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：悪魔、なんて美しい強さだ・・・、BlackContractです。 " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：炎をで丸焦げにしてやるぜええぇぇ！！FlameAura！！！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：闇闇闇闇闇闇闇闇闇・・・絶望・・・Damnation。 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：炎授天使の加速円、HeatBoostです。 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：炎術の舞なんて、簡単すぎる・・・ImmortalRaveです。 " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：全盛期ボクは常にこの状態だった、GaleWindです。 もう一人の" + this.FirstName + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.FirstName + "：ックク、自然の力いただくぜええぇ！WordOfLife！！！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "：全盛期ボクはこんな魔法不要でした、WordOfFortuneです。 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.FirstName + "：空想物理なんて大したものじゃありませんよ？AetherDriveです。 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "：創造と原理なんて大げさ過ぎる、普通ですよ。EternalPresence。 " + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.FirstName + "：２回行動できたとしたら最強だと思いません？ OneImmunityです。 " + this.FirstName + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.FirstName + "：アッハハハハハ！時空の歪に消えちまいなぁ！TimeStop！！！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.FirstName + "：元々はカラッポのくせに、ッククク。DispelMagicです。\r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "：時空の支配者の上昇円、RiseOfImageです。 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.FirstName + "：空詠唱ですね。\r\n";
                    case 51: // Inner Inspiration
                        return this.FirstName + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.FirstName + "：奇跡？このぐらいの聖スペル普通でしょう、Resurrectionです。\r\n";
                    case 53: // Resurrection 2
                        return this.FirstName + "：てめえなんか対象にするわけねえだろおおぉ！！\r\n";
                    case 54: // Resurrection 3
                        return this.FirstName + "：生きてましたね。すいませんでした。\r\n";
                    case 55: // Resurrection 4
                        return this.FirstName + "：ボク自身は生きてるのか？というブラックジョークです、意味はありません。\r\n";
                    case 56: // Stance Of Standing
                        return this.FirstName + "：防御兼攻撃とは、こうやるんですよ。\r\n";
                    case 57: // Mirror Image
                        return this.FirstName + "：女神より蒼の魔法反射円、MirrorImageです。{0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "：ックク、掛かったな！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "：ックク、掛かったな！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：し、しまったボクとした事がぁぁ！\r\n";
                    case 60: // Deflection
                        return this.FirstName + "：白の者より物理反射円、Deflectionです。 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "：ックク、掛かったな！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "：ックク、掛かったな！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：しまった、ボクとしたことがあぁ！\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "：本質？あるわけないでしょう、TruthVisionです。　" + this.FirstName + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.FirstName + "：相手の動き、封殺してみせましょう。StanceOfFlowです。　" + this.FirstName + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "：技があれば容易いコンボでしょう、CarnageRushです。 ひとつ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ふたつ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "みっつ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "よっつ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "そして最後です。ハアアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "：少し寝ててください。CrushingBlowです。  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.FirstName + "：こんなアイテムがあるんですよ、いかがでしょう。\r\n";
                    case 72: // Enigma Sence
                        return this.FirstName + "：力は見せ方次第だと思いませんか？EnigmaSennceです\r\n";
                    case 73: // Soul Infinity
                        return this.FirstName + "：これがボクの全ての能力を注ぎ込んだパワーです。SoulInfinity！\r\n";
                    case 74: // Kinetic Smash
                        return this.FirstName + "：物理運動に沿った最大限の攻撃とはこうですかね。KineticSmashです。\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "：・・・っこのタイミングで・・・！？　詠唱ミスです。\r\n";
                    case 77: // Cleansing
                        return this.FirstName + "：浄化というより、元々何も無いんですよ。Cleansingです。\r\n";
                    case 78: // Pure Purification
                        return this.FirstName + "：精神の洗い直しから治せる事もあるんです、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.FirstName + "：ボクに限界なんて無いんですよ、VoidExtractionです。" + this.FirstName + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.FirstName + "：アカシジアの実です。良い気付けになるでしょう。\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "：絶対零度で凍り付けえええぇぇぇぇ。AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "：知恵と知識の組み合わせ最強だと思いません？ PromiesdKnowledgeです。　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "：平穏と無、どちらも同じなんでしょうね、Tranquilityです。\r\n";
                    case 85: // High Emotionality 1
                        return this.FirstName + "：ッハアアアアアアアァァァァ！！！　HighEmotionality！\r\n";
                    case 86: // High Emotionality 2
                        return this.FirstName + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "：・・・っく、思うように動けません・・・、スキル使用ミスです。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "：っく・・・防御が・・・思うようにできません！ \r\n";
                    case 89: // Silent Rush 1
                        return this.FirstName + "：貴方は姿すら捉えられないでしょうね、SilentRushです。　一つ {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "ふたつ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "そして、みっつめです。ハアァァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.FirstName + "：スタン効果はボクには効きませんよ。AntiStunです。 " + this.FirstName + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.FirstName + "：言ったはずです。ボクにスタンは効かない。\r\n";
                    case 95: // Stance Of Death
                        return this.FirstName + "：死・・・破滅の言葉ですよ、ッククク、StanceOfDeath！ " + this.FirstName + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.FirstName + "：見えますか？この容が、【究極奥義】Oboro Impactです。\r\n";
                    case 97: // Oboro Impact 2
                        return this.FirstName + "：ッハアアァァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.FirstName + "：全てを破壊・・・破壊してあげましょう【究極奥義】Catastropheです\r\n";
                    case 99: // Catastrophe 2
                        return this.FirstName + "：ッハアアァァァアアア！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.FirstName + "：無駄です。全て見切ってさしあげましょう、 StanceOfEyesです。 " + this.FirstName + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.FirstName + "：ソコのモーションですね、　" + this.FirstName + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.FirstName + "：っばかな！？動きがまったく読めない！　" + this.FirstName + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.FirstName + "：詠唱とは必ず隙があります。Negateです。" + this.FirstName + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.FirstName + "：ッハハハ！詠唱動作が見え見えですよ！" + this.FirstName + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.FirstName + "：っばかな！・・・何時の間に詠唱を！？" + this.FirstName + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "：ッハハハハハ！！真なるゼロ【究極奥義】NothingOfNothingness！ " + this.FirstName + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.FirstName + "：これが超然たる起源、Genesisです。  " + this.FirstName + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.FirstName + "：っす、すいません・・・調子が悪くてCleansingが発動できません。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "：ッハハハハ！そんな構え、見え見えですよ！\r\n";
                    case 111: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復です。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう水は残ってないようです。一日待たないと駄目ですね。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.FirstName + "：甘いですね、カウンターです。\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.FirstName + "：ッバ、バカな！このボクが見切れないなんて！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "：防御出来る・・・とでも思いましたか？\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：クリティカルヒットです。\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.FirstName + "：リヴァイヴポーションです。これで復活ですね。\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "：この凍てつく寒さ・・・ライフ回復できません。\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.FirstName + "：この凍てつく寒さ・・・マナ回復できません。\r\n";
                    case 122: // 「ためる」行動時
                        return this.FirstName + "：ッハハハ、こんな簡単に魔力を蓄えさせてもらえるとは。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.FirstName + "：魔力は十分蓄えてあります。もう十分でしょう。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.FirstName + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.FirstName + "：アイテムゲージが溜まってない間、アイテムは使えませんね。\r\n";
                    case 126: // FlashBlase
                        return this.FirstName + "：これは痛いでしょうね、FlashBlazeです。 {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.FirstName + "：ボクとしたことが・・・インスタント値が足りません\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.FirstName + "：インスタントのタイミングで発動したい所ですが。\r\n";
                    case 129: // PsychicTrance
                        return this.FirstName + "：ッハハハ、更に魔法攻撃強化、PsychicTrance。\r\n";
                    case 130: // BlindJustice
                        return this.FirstName + "：ッククク、更に物理攻撃強化、BlindJustice。\r\n";
                    case 131: // TranscendentWish
                        return this.FirstName + "：死など恐れませんよ、TranscendentWish。\r\n";
                    case 132: // LightDetonator
                        return this.FirstName + "：そこに行くと分かってました、LightDetonator。\r\n";
                    case 133: // AscendantMeteor
                        return this.FirstName + "焼け焦げるが良い、AscendantMeteor\r\n";
                    case 134: // SkyShield
                        return this.FirstName + "：空聖の加護、SkyShield\r\n";
                    case 135: // SacredHeal
                        return this.FirstName + "：全員回復です、SacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.FirstName + "：ッククク、これを許すとは愚かな、EverDroplet\r\n";
                    case 137: // FrozenAura
                        return this.FirstName + "：氷属性を付与、FrozenAuraです。\r\n";
                    case 138: // ChillBurn
                        return this.FirstName + "：ChillBurnで凍結してください。\r\n";
                    case 139: // ZetaExplosion
                        return this.FirstName + "：よみがえれ、Zeta！　ッハハ・・・ッハハハハ！！！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.FirstName + "：凍ってください。 {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.FirstName + "：ほんの一瞬です、StarLightning。\r\n";
                    case 142: // WordOfMalice
                        return this.FirstName + "：ハハハ、さらに遅くなりますよ、WordOfMalice。\r\n";
                    case 143: // BlackFire
                        return this.FirstName + "：魔法防御低下、BlackFire。\r\n";
                    case 144: // EnrageBlast
                        return this.FirstName + "：さて、焼かれてもらいましょう、EnrageBlast。\r\n";
                    case 145: // Immolate
                        return this.FirstName + "：物理防御低下、Immolate。\r\n";
                    case 146: // VanishWave
                        return this.FirstName + "：黙っていてくれませんか、VanishWave。\r\n";
                    case 147: // WordOfAttitude
                        return this.FirstName + "：卑怯と呼んでもらって構いませんよ、WordOfAttitude。\r\n";
                    case 148: // HolyBreaker
                        return this.FirstName + "：ダメージの差を付けましょう、HolyBreaker。\r\n";
                    case 149: // DarkenField
                        return this.FirstName + "：防御力全面低下、DarkenField。\r\n";
                    case 150: // SeventhMagic
                        return this.FirstName + "：原則を覆すとしましょう、SeventhMagic。\r\n";
                    case 151: // BlueBullet
                        return this.FirstName + "：氷の飛礫、BlueBulletです。\r\n";
                    case 152: // NeutralSmash
                        return this.FirstName + "：NeutralSmash、ッハァ！\r\n";
                    case 153: // SwiftStep
                        return this.FirstName + "：速度上げさせてもらいます、SwiftStep。\r\n";
                    case 154: // CircleSlash
                        return this.FirstName + "：邪魔ですねどいてください、CircleSlash。\r\n";
                    case 155: // RumbleShout
                        return this.FirstName + "：どこを見てるんですか？コチラです。\r\n";
                    case 156: // SmoothingMove
                        return this.FirstName + "：ッククク、ほぼ無限コンボです、SmoothingMove。\r\n";
                    case 157: // FutureVision
                        return this.FirstName + "：ボクが見きれないハズがない、FutureVision。\r\n";
                    case 158: // ReflexSpirit
                        return this.FirstName + "：スタン系は事前回避に限ります、ReflexSpirit。\r\n";
                    case 159: // SharpGlare
                        return this.FirstName + "：黙っていてくれませんか、SharpGlare。\r\n";
                    case 160: // TrustSilence
                        return this.FirstName + "：沈黙などボクには無縁ですね、TrustSilence。\r\n";
                    case 161: // SurpriseAttack
                        return this.FirstName + "ッハハハハ、これで気絶しちまいなぁ！SurpriseAttack！！\r\n";
                    case 162: // PsychicWave
                        return this.FirstName + "：ッハハハ、止められないでしょう、PsychicWave。\r\n";
                    case 163: // Recover
                        return this.FirstName + "：しっかりしてください、Recoverです。\r\n";
                    case 164: // ViolentSlash
                        return this.FirstName + "：トドメェェェ！！　ViolentSlash！！！\r\n";
                    case 165: // OuterInspiration
                        return this.FirstName + "：さて、これで元通りですね、OuterInspiration。\r\n";
                    case 166: // StanceOfSuddenness
                        return this.FirstName + "：ッハハハハ！StanceOfSuddenness！ッハッハハハハ！！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.FirstName + "：対象のインスタントコマンドが無いですね。\r\n";
                    case 168: // StanceOfMystic
                        return this.FirstName + "：見切ったつもりでしょうが、甘いです。StanceOfMystic。\r\n";
                    case 169: // HardestParry
                        return this.FirstName + "：その攻撃、瞬間で回避してみせましょう。HardestParryです。\r\n";
                    case 170: // ConcussiveHit
                        return this.FirstName + "：防御率DOWNです、ConcussiveHit。\r\n";
                    case 171: // Onslaught hit
                        return this.FirstName + "：攻撃率DOWNです、OnslaughtHit。\r\n";
                    case 172: // Impulse hit
                        return this.FirstName + "：速度率DOWNです、ImpulseHit。\r\n";
                    case 173: // Fatal Blow
                        return this.FirstName + "：さて、死んでもらいましょう、FatalBlow。\r\n";
                    case 174: // Exalted Field
                        return this.FirstName + "：この場にて更なる増強をかけます、ExaltedField。\r\n";
                    case 175: // Rising Aura
                        return this.FirstName + "：更に攻撃を増しますよ、RisingAuraです。\r\n";
                    case 176: // Ascension Aura
                        return this.FirstName + "：更に魔法攻撃を上げましょうか、AscensionAuraです。\r\n";
                    case 177: // Angel Breath
                        return this.FirstName + "：天使の加護による状態異常回復を、AngelBreathです。\r\n";
                    case 178: // Blazing Field
                        return this.FirstName + "：すぐに燃やし尽くしてあげますよ、BlazingField。\r\n";
                    case 179: // Deep Mirror
                        return this.FirstName + "：それが通るとは思ってないでしょう、DeepMirrorです。\r\n";
                    case 180: // Abyss Eye
                        return this.FirstName + "：この眼を見た時が最後です、AbyssEye。\r\n";
                    case 181: // Doom Blade
                        return this.FirstName + "：マナを断ち切らせてもらいます、DoomBlade。\r\n";
                    case 182: // Piercing Flame
                        return this.FirstName + "：ここは貫通の火を使いましょう、PiercingFlame。\r\n";
                    case 183: // Phantasmal Wind
                        return this.FirstName + "：さらに上げていきます、PhantasmalWindです。\r\n";
                    case 184: // Paradox Image
                        return this.FirstName + "：潜在の源、ParadoxImageです。\r\n";
                    case 185: // Vortex Field
                        return this.FirstName + "：皆さん鈍足になってください、VortexFieldです。\r\n";
                    case 186: // Static Barrier
                        return this.FirstName + "：水理の加護、StaticBarrier\r\n";
                    case 187: // Unknown Shock
                        return this.FirstName + "：あなたが暗闇で戦えるとは思えませんけどね、UnknownShockです。\r\n";
                    case 188: // SoulExecution
                        return this.FirstName + "：ックク・・・SoulExecusion。\r\n";
                    case 189: // SoulExecution hit 01
                        return this.FirstName + "：トゥ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.FirstName + "：シッ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.FirstName + "：ツェ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.FirstName + "：セィ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.FirstName + "：スゥ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.FirstName + "：フッ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.FirstName + "：ドゥ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.FirstName + "：セイ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.FirstName + "：ハアアァァ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.FirstName + "：トドメです！ハアァァァ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.FirstName + "：回復量を上げさせてもらいましょう、NourishSenseです。\r\n";
                    case 200: // Mind Killing
                        return this.FirstName + "：精神から攻めさせてもらいましょう、MindKilling。\r\n";
                    case 201: // Vigor Sense
                        return this.FirstName + "：反応値を更に上げさせてもらいます、VigorSenseです。\r\n";
                    case 202: // ONE Authority
                        return this.FirstName + "：全員上げていきましょうか、OneAuthority。\r\n";
                    case 203: // 集中と断絶
                        return this.FirstName + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.FirstName + "：すみませんが【元核】は、今日すでに発動済みです。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.FirstName + "：【元核】はインスタントタイミング限定です。\r\n";
                    case 206: // Sigil Of Homura
                        return this.FirstName + "：これが決まったら、ほぼ終わりですね。SigilOfHomuraです。\r\n";
                    case 207: // Austerity Matrix
                        return this.FirstName + "：支配力、変えさせてもらいます、AusterityMatrix。\r\n";
                    case 208: // Red Dragon Will
                        return this.FirstName + "：火竜よ、ボクに仕えよ、RedDragonWill。\r\n";
                    case 209: // Blue Dragon Will
                        return this.FirstName + "：水竜よ、ボクに仕えよ、BlueDragonWill。\r\n";
                    case 210: // Eclipse End
                        return this.FirstName + "：ックク、これで全てが無駄となりますね、EclipseEnd。\r\n";
                    case 211: // Sin Fortune
                        return this.FirstName + "：次のクリティカル、覚悟してください、SinFortuneです。\r\n";
                    case 212: // AfterReviveHalf
                        return this.FirstName + "：死耐性（ハーフ）を付与させてもらいます。\r\n";
                    case 213: // Demonic Ignite
                        return this.FirstName + "：これでほぼ詰みですね、DemonicIgniteです。\r\n";
                    case 214: // Death Deny
                        return this.FirstName + "：冒涜ではなく完全なる蘇りです、DeathDeny。\r\n";
                    case 215: // Stance of Double
                        return this.FirstName + "：これぞ究極原理、StanceOfDoubleです。  " + this.FirstName + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.FirstName + "：神具よ！我に永遠の生命を！！ッハアアアァァァ！！！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.FirstName + "：ッグ・・・セ・・・セフィ・・・ネ・・・\r\n";
                    case 218: // インスタント不足
                        return this.FirstName + "：インスタントが不足していますね。\r\n";

                    case 2001: // ポーション回復時
                        return this.FirstName + "：{0} 回復です。";
                    case 2002: // レベルアップ終了催促
                        return this.FirstName + "：レベルアップを先にしてください。";
                    case 2003: // 荷物を減らせる催促
                        return this.FirstName + "：{0}、荷物いっぱいですね？アイテムを減らしてからまた言ってください。";
                    case 2004: // 装備判断
                        return this.FirstName + "：装備してもいいですか？";
                    case 2005: // 装備完了
                        return this.FirstName + "：装備完了です。";
                    case 2006: // 遠見の青水晶を使用
                        return this.FirstName + "：いったん、町に戻りましょうか？";
                    case 2007: // 売却専用品に対する一言
                        return this.FirstName + "：売却専用の品物です。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.FirstName + "：マナが不足していますね。";
                    case 2009: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復です。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう水は残ってないようです。一日待たないと駄目ですね。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.FirstName + "：戦闘中専用品です。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.FirstName + "：町の中から使っても意味はありませんね。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.FirstName + "：これはさすがに捨てられません。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.FirstName + "：スキルが不足していますね。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.FirstName + "：すいません、それはちょっと捨てないでください。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.FirstName + "：おかげで復活できました。ありがとうございます。";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.FirstName + "：{0}は生きてないのでは？というブラックジョークです。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.FirstName + "：もしボクが死んでいたら使う行為自体できないはず。面白いジョークですね。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.FirstName + "：これはボクでは装備できませんね。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.FirstName + "：すいません、今の状況でこのアイテムを使う必要はなさそうです。";
                    case 2021: // アイテム捨てるの催促
                        return this.FirstName + "：先にバックパックの整理をしませんか？";
                    case 2022: // オーバーシフティング使用開始時
                        return this.FirstName + "：こ、これは・・・身体能力が再構築されるとは・・・素晴らしいですね。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.FirstName + "：オーバーシフティングによる割り振りが終わってからにしましょう。";
                    case 2024: // 成長リキッドを使用した時
                        return this.FirstName + "：【{0}】パラメタ、 {1} 上昇しましたね。";
                    case 2025:
                        return this.FirstName + "：両手武器を装備していますからね。サブは装備できません。";
                    case 2026:
                        return this.FirstName + "：武器（メイン）をはじめに何か装備しないといけませんね。";
                    case 2027: // 清透水使用時
                        return this.FirstName + "：ライフが100%回復しましたね。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.FirstName + "：もう水は残ってないゆです。一日待たないと駄目ですね。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.FirstName + "：バックパックを空けましょう。今のままでは装備は外せませんね。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.FirstName + "：{0}を捨てましょう。いいですね？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.FirstName + "：戦闘中です。使用するアイテムを早く決めませんか？";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.FirstName + "：このアイテムは戦闘中に使用は出来ませんね。";
                    case 2033: // 預けられない場合
                        return this.FirstName + "：これを預けるのは得策ではありませんね。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.FirstName + "：いえ、これ以上は持って行く必要はありませんね。";
                    case 2035: // Sacred Heal
                        return this.FirstName + "：全員回復しましたね。";
                    case 2036: // オーバーシフティング割り振り完了
                        return this.FirstName + "：再割り振り、完了ですね。";
                    case 2037: // １人のため、アイテムをわたす相手が居ない場合
                        return this.FirstName + "：ボク自身から、ボクにわたす・・・面白いジョークですね。";

                    case 3000: // 店に入った時の台詞
                        return this.FirstName + "：無防備な状態ですね・・・";
                    case 3001: // 支払い要求時
                        return this.FirstName + "：{0}を購入しましょう。 {1}Gold置きましょう。";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.FirstName + "：アイテムがいっぱいのようです。少し整理させてください。";
                    case 3003: // 購入完了時
                        return this.FirstName + "：これで・・・売買成立とさせてください。";
                    case 3004: // Gold不足で購入できない場合
                        return this.FirstName + "：Goldがあと{0}足りないようです。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.FirstName + "：他を見させてください。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.FirstName + "：すいません、このアイテムは売却できません。";
                    case 3007: // アイテム売却時
                        return this.FirstName + "：{0}を売却します。つまり、{1}Gold頂いても良いハズですね。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.FirstName + "：コレはラナさんの作ったアクセサリですね。{0}Goldで本当に売るのでしょうか？";
                    case 3009: // 武具店を出る時
                        return this.FirstName + "：また・・・いつか帰ってきてください。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.FirstName + "：この剣は・・・確かに売り切れですね。";
                    case 3011: // 装備可能なものが購入された時
                        return this.FirstName + "：この場で装備しても良いでしょうか？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.FirstName + "：現在は、{0}を装備しています。鑑定は不得意ですが{1}Goldぐらいで売れるでしょう。";

                    case 4001: // 通常攻撃を選択
                    case 4002: // 防御を選択
                    case 4003: // 待機を選択
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.FirstName + "：" + this.ActionLabel.text + "でいきます。\r\n";
                    case 4072:
                        return this.FirstName + "：敵を対象にするわけにはいきませんね。\r\n";
                    case 4073:
                        return this.FirstName + "：敵を対象にするわけにはいきませんね。\r\n";
                    case 4074:
                        return this.FirstName + "：味方を対象にはできませんね。\r\n";
                    case 4075:
                        return this.FirstName + "：味方を対象にはできませんね。\r\n";
                    case 4076:
                        return this.FirstName + "：味方に攻撃をするつもりはありません。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.FirstName + "：魔力をため込みます。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.FirstName + "：メイン武器の効果を発動させます。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.FirstName + "：サブ武器の効果を発動させます。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.FirstName + "：アクセサリ１の効果を発動させます。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.FirstName + "：アクセサリ２の効果を発動させます。\r\n";

                    case 4082: // FlashBlaze
                        return this.FirstName + "：ここはフラッシュブレイズですね。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.FirstName + "：エアロ・スラッシュです。ハアアァ！！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.FirstName + "：{0} 回復です \r\n";
                    case 5003:
                        return this.FirstName + "：{0} マナ回復です \r\n";
                    case 5004:
                        return this.FirstName + "：アイシクル・スラッシュ、行きます！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.FirstName + "：エレクトロ・ブローです、ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.FirstName + "：ブルー・ライトニングです。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.FirstName + "：バーニング・クレイモアです。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.FirstName + "：赤蒼授からの炎です。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.FirstName + "：{0} スキルポイント回復です \r\n";
                    case 5010:
                        return this.FirstName + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ランディス"
            if (this.FirstName == "ランディス")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "：ッチ・・・スキルがねぇ\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "：ッラァ！！\r\n";
                    case 2: // Double Slash 1
                        return this.FirstName + "：ッラ！\r\n";
                    case 3: // Double Slash 2
                        return this.FirstName + "：ッドラァ！\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "：精神勝負で負ける気はしねえ、朽ちろや。\r\n";
                    case 5: // empty skill
                        return this.FirstName + "：ッチ・・・スキル未定とは・・・\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "：っきかねえなぁ・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + "：やるじゃねぇか・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "：ッケ・・・ドジったか・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 回復しといたぞ。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "：ファイア！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "：フレスト！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "：ヴォルカニィ！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：ッシャオラ、来た来たぁ！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "：オラヨ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "：コイツは戦闘中使えねぇ。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "：ッチィ！使えねぇアイテムだな！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "：ッケ・・・もうマナはねぇ\r\n";
                    case 18: // Protection
                        return this.FirstName + "：プロッツ！ 物理防御上げとくぞ。\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：ァヴソーヴ！ 魔法防御上げとくぞ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：セイント！ 物理攻撃上げとくぞ。\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：シャドウ！ 魔法攻撃上げとくぞ。r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：ヴェンジェ！ 【力】を {0} 上げとくぞ。\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "：ショック！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：グローリ！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：ノヴァ！ {0} 回復しといたぞ。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "：ノヴァ！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "：ブラスト！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "：ダーッハッハッハ！燃えちまいなぁ！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.FirstName + "：ダーッハッハッハ！燃えちまいなぁ！\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "：デヴォプラァ！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "：ニードル！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "：ランス！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "：ウェイヴ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "：ワーパワー！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "：ホワイアウト！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：ッカッカッカ、コイツで撃ち放題だ。 " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：炎でぶった斬る。　 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：ソコで死んでろぉ、ダムネーション！　　死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：ブースト！ 【技】を {0} 上げとくぞ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：ラアアァァァァ！レイヴ！！ " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：ゲイル！　 もう一人の" + this.FirstName + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.FirstName + "：ワーライフ！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "：ワーフォーチュ！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.FirstName + "：ドライヴ！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "：ッゼンス！ " + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.FirstName + "：ワンイム！ " + this.FirstName + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.FirstName + "：ストップ！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.FirstName + "：ディスペル！ \r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "：ライズ！ 【心】を {0} 上げとくぞ。\r\n";
                    case 50: // 空詠唱
                        return this.FirstName + "：ッカ・・・詠唱してねぇぞ・・・\r\n";
                    case 51: // Inner Inspiration
                        return this.FirstName + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.FirstName + "：リザレク！\r\n";
                    case 53: // Resurrection 2
                        return this.FirstName + "：ヴォケが！対象ちげぇ！！\r\n";
                    case 54: // Resurrection 3
                        return this.FirstName + "：ヴォケが！死んでねぇ！！\r\n";
                    case 55: // Resurrection 4
                        return this.FirstName + "：ヴォケも大概にしろ。\r\n";
                    case 56: // Stance Of Standing
                        return this.FirstName + "：ッフン！\r\n";
                    case 57: // Mirror Image
                        return this.FirstName + "：ミライメ！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "：返すぞオラァ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "：返すぞオラァ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：ッケ、クソが\r\n";
                    case 60: // Deflection
                        return this.FirstName + "：デフレク！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "：返すぞオラァ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "：返すぞオラァ！ 【しかし、強力な威力ではね返せない！】" + this.FirstName + "：ッケ、クソが\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "：トルゥス！　" + this.FirstName + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.FirstName + "：スタンフロウ！　" + this.FirstName + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "：オラァ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ッラァ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "ッラアァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "ッラアァァ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "ウォラアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "：クラッシュ！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.FirstName + "：コイツでも飲んでな。\r\n";
                    case 72: // Enigma Sence
                        return this.FirstName + "：エニグマ！\r\n";
                    case 73: // Soul Infinity
                        return this.FirstName + "：ブッッッッタ斬る、ラァァァ！！！\r\n";
                    case 74: // Kinetic Smash
                        return this.FirstName + "：スマァッシュ！！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "：・・・ッケ、詠唱妨害か、くだらねぇ。\r\n";
                    case 77: // Cleansing
                        return this.FirstName + "：クリーン！\r\n";
                    case 78: // Pure Purification
                        return this.FirstName + "：ピュリファイ！\r\n";
                    case 79: // Void Extraction
                        return this.FirstName + "：ヴォイデクス！" + this.FirstName + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.FirstName + "：少しは目を醒ませ。\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "：アブスゼロ！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "：プロナレ！　【知】を {0} 上げとくぞ。\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "：トランキィ！\r\n";
                    case 85: // High Emotionality 1
                        return this.FirstName + "：ハイエモ！　来たきたぁ！！\r\n";
                    case 86: // High Emotionality 2
                        return this.FirstName + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "：ッケ・・・使えねぇなぁ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "：ッカ・・・防御できねぇとはな。 \r\n";
                    case 89: // Silent Rush 1
                        return this.FirstName + "：ラッシュ！！ {0}ダメージ・・・   ";
                    case 90: // Silent Rush 2
                        return "ッフン！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "ッラアァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.FirstName + "：アンスタ！ " + this.FirstName + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.FirstName + "：スタン回避ぐらいとぉぜんだ。\r\n";
                    case 95: // Stance Of Death
                        return this.FirstName + "：スタンデス！ " + this.FirstName + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.FirstName + "：オボロ行くぞ・・・ッフウウウゥゥゥゥ・・・\r\n";
                    case 97: // Oboro Impact 2
                        return this.FirstName + "：ウオォォラアアァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.FirstName + "：悪いがコッパミジンだ、カタスト！\r\n";
                    case 99: // Catastrophe 2
                        return this.FirstName + "：死んでこいやああぁぁ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.FirstName + "：スタンアイ！ " + this.FirstName + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.FirstName + "：おせぇな！！　" + this.FirstName + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.FirstName + "：ッケ・・・　" + this.FirstName + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.FirstName + "：ニゲイト！　" + this.FirstName + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.FirstName + "：おせえぇ！" + this.FirstName + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.FirstName + "：ッカ・・・" + this.FirstName + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "：ナッシング！ " + this.FirstName + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.FirstName + "：ジェネ！  " + this.FirstName + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.FirstName + "：クリーンジングミスだ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "：無駄だな。\r\n";
                    case 111: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復しといたぞ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：一日１回だ。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.FirstName + "：おせぇ！カウンター！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.FirstName + "：ッカ・・・ッスったか・・・\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "：無駄だな。\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：ッシャラァ！クリティカル！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.FirstName + "：復活させるぞ。\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "：ッケ・・・回復も無駄だったな\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.FirstName + "：ッカ・・・マナ回復も無駄だったな\r\n";
                    case 122: // 「ためる」行動時
                        return this.FirstName + "：魔力、蓄えるぞ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.FirstName + "：蓄積上限だ。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.FirstName + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.FirstName + "：アイテムゲージがまだだ。\r\n";
                    case 126: // FlashBlase
                        return this.FirstName + "：ブレイズ！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.FirstName + "：ヴォケが、インスタント足りねぇだろぉが！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.FirstName + "：コイツはインスタントだ。\r\n";
                    case 129: // PsychicTrance
                        return this.FirstName + "：サイトラ！　魔法攻撃強化だ。\r\n";
                    case 130: // BlindJustice
                        return this.FirstName + "：ジャスティス！　物理攻撃強化だ。\r\n";
                    case 131: // TranscendentWish
                        return this.FirstName + "：トラッセン！　\r\n";
                    case 132: // LightDetonator
                        return this.FirstName + "：デトネイト！\r\n";
                    case 133: // AscendantMeteor
                        return this.FirstName + "：ッラアアァァ！！　メテオ！！\r\n";
                    case 134: // SkyShield
                        return this.FirstName + "：シールド！\r\n";
                    case 135: // SacredHeal
                        return this.FirstName + "：サークレッド！\r\n";
                    case 136: // EverDroplet
                        return this.FirstName + "：エヴァドロー！\r\n";
                    case 137: // FrozenAura
                        return this.FirstName + "：アイスオーラ！\r\n";
                    case 138: // ChillBurn
                        return this.FirstName + "：食らえ、ッバーン！\r\n";
                    case 139: // ZetaExplosion
                        return this.FirstName + "：ゼータ！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.FirstName + "：凍れや！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.FirstName + "：ライトニング！\r\n";
                    case 142: // WordOfMalice
                        return this.FirstName + "：ワーマリス！\r\n";
                    case 143: // BlackFire
                        return this.FirstName + "：ブラックファイア！\r\n";
                    case 144: // EnrageBlast
                        return this.FirstName + "：エンレイジ！\r\n";
                    case 145: // Immolate
                        return this.FirstName + "：イモレ！\r\n";
                    case 146: // VanishWave
                        return this.FirstName + "：ヴァニッシュ！\r\n";
                    case 147: // WordOfAttitude
                        return this.FirstName + "：ワーアッティ！\r\n";
                    case 148: // HolyBreaker
                        return this.FirstName + "：ブレイカー！\r\n";
                    case 149: // DarkenField
                        return this.FirstName + "：ダーケン！\r\n";
                    case 150: // SeventhMagic
                        return this.FirstName + "：セヴェンス！\r\n";
                    case 151: // BlueBullet
                        return this.FirstName + "：ブルバレ！\r\n";
                    case 152: // NeutralSmash
                        return this.FirstName + "：ッシャァ！スマッシュ！\r\n";
                    case 153: // SwiftStep
                        return this.FirstName + "：スウィフト！\r\n";
                    case 154: // CircleSlash
                        return this.FirstName + "：サークル！\r\n";
                    case 155: // RumbleShout
                        return this.FirstName + "：コッチだ、オラァ！\r\n";
                    case 156: // SmoothingMove
                        return this.FirstName + "：スムージン！\r\n";
                    case 157: // FutureVision
                        return this.FirstName + "：フューチャーヴィ！\r\n";
                    case 158: // ReflexSpirit
                        return this.FirstName + "：リフレ！\r\n";
                    case 159: // SharpGlare
                        return this.FirstName + "：シャーグレ！\r\n";
                    case 160: // TrustSilence
                        return this.FirstName + "：トラッサイレン！\r\n";
                    case 161: // SurpriseAttack
                        return this.FirstName + "：ップライズ！\r\n";
                    case 162: // PsychicWave
                        return this.FirstName + "：サイキック！\r\n";
                    case 163: // Recover
                        return this.FirstName + "：リカバ！\r\n";
                    case 164: // ViolentSlash
                        return this.FirstName + "：ッヴァイオレン！\r\n";
                    case 165: // OuterInspiration
                        return this.FirstName + "：アウスピ！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.FirstName + "：ッサドン！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.FirstName + "：コイツはインスタント対象専用だ。\r\n";
                    case 168: // StanceOfMystic
                        return this.FirstName + "：ミスティッ！\r\n";
                    case 169: // HardestParry
                        return this.FirstName + "：ッカ、面倒くせぇ、ハードパリィ！\r\n";
                    case 170: // ConcussiveHit
                        return this.FirstName + "：カッシヴ！\r\n";
                    case 171: // Onslaught hit
                        return this.FirstName + "：オンッスロ！";
                    case 172: // Impulse hit
                        return this.FirstName + "：パルス！";
                    case 173: // Fatal Blow
                        return this.FirstName + "：フェタル！";
                    case 174: // Exalted Field
                        return this.FirstName + "：ッハァ、悪ぃな！　イグッザルツ！\r\n";
                    case 175: // Rising Aura
                        return this.FirstName + "：ライジンッ！\r\n";
                    case 176: // Ascension Aura
                        return this.FirstName + "：セッション！\r\n";
                    case 177: // Angel Breath
                        return this.FirstName + "：ッカ、面倒くせえ、ブレス！\r\n";
                    case 178: // Blazing Field
                        return this.FirstName + "：レイジン・フィー！\r\n";
                    case 179: // Deep Mirror
                        return this.FirstName + "：ディプミラ！\r\n";
                    case 180: // Abyss Eye
                        return this.FirstName + "：アビッスァ！\r\n";
                    case 181: // Doom Blade
                        return this.FirstName + "：ドゥーム・レイ！\r\n";
                    case 182: // Piercing Flame
                        return this.FirstName + "：ピアッ・フレイ！\r\n";
                    case 183: // Phantasmal Wind
                        return this.FirstName + "：ファンタズマ！\r\n";
                    case 184: // Paradox Image
                        return this.FirstName + "：ドックス！\r\n";
                    case 185: // Vortex Field
                        return this.FirstName + "：テックス・フィール！\r\n";
                    case 186: // Static Barrier
                        return this.FirstName + "：スタック・バリア！\r\n";
                    case 187: // Unknown Shock
                        return this.FirstName + "：アン・ショック！\r\n";
                    case 188: // SoulExecution
                        return this.FirstName + "：ッハーーッハッハッハァ！　ソウル・エグゼッ！！！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.FirstName + "：ラァ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.FirstName + "：ッラァ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.FirstName + "：オラァ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.FirstName + "：オラアァ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.FirstName + "：ッラ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.FirstName + "：ッララ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.FirstName + "：ッララアァァ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.FirstName + "：オラララアアァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.FirstName + "：オラオラオラ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.FirstName + "：食らえやあ！ラアアァァ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.FirstName + "：ノッセンス！\r\n";
                    case 200: // Mind Killing
                        return this.FirstName + "：マイン・ッキル！\r\n";
                    case 201: // Vigor Sense
                        return this.FirstName + "：ヴィゴー！\r\n";
                    case 202: // ONE Authority
                        return this.FirstName + "：ワン・オース！\r\n";
                    case 203: // 集中と断絶
                        return this.FirstName + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.FirstName + "：ッケ、そんな何度も発動してらんねぇぜ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.FirstName + "：んなの、いつでもいいだろ。\r\n";
                    case 206: // Sigil Of Homura
                        return this.FirstName + "：シギィル！\r\n";
                    case 207: // Austerity Matrix
                        return this.FirstName + "：アゥス・トゥリックス！\r\n";
                    case 208: // Red Dragon Will
                        return this.FirstName + "：レッドラァ！\r\n";
                    case 209: // Blue Dragon Will
                        return this.FirstName + "：ブルードラァ！\r\n";
                    case 210: // Eclipse End
                        return this.FirstName + "：イクス・エンッ！\r\n";
                    case 211: // Sin Fortune
                        return this.FirstName + "：ッシン！\r\n";
                    case 212: // AfterReviveHalf
                        return this.FirstName + "：デッド・レジ・ハーフ！\r\n";
                    case 213: // Demonic Ignite
                        return this.FirstName + "：ディーモッ！\r\n";
                    case 214: // Death Deny
                        return this.FirstName + "：ッディナ！\r\n";
                    case 215: // Stance of Double
                        return this.FirstName + "：スタンッダブル！  " + this.FirstName + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.FirstName + "：ッハ、この程度でくたばると思うなよ。\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.FirstName + "：・・・　ッ・・・\r\n";
                    case 218: // インスタント不足
                        return this.FirstName + "：っち・・・インスタントが足りねぇ\r\n";

                    case 2001: // ポーションまたは魔法による回復時
                        return this.FirstName + "：{0} 回復しといたぞ。";
                    case 2002: // レベルアップ終了催促
                        return this.FirstName + "：レベルアップさせろ。";
                    case 2003: // 荷物を減らせる催促
                        return this.FirstName + "：{0}、荷物を減らせ。アイテムを渡させろ。";
                    case 2004: // 装備判断
                        return this.FirstName + "：装備だな？";
                    case 2005: // 装備完了
                        return this.FirstName + "：装備したぞ。";
                    case 2006: // 遠見の青水晶を使用
                        return this.FirstName + "：町戻るか？";
                    case 2007: // 売却専用品に対する一言
                        return this.FirstName + "：売却専用品だ。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.FirstName + "：マナ不足だ。";
                    case 2009: // 神聖水使用時
                        return this.FirstName + "：ライフ・スキル・マナが30%ずつ回復しといたぞ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてる。一日１回だ。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.FirstName + "：戦闘中専用品だ。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.FirstName + "：町ん中じゃ無駄だ。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.FirstName + "：コイツは捨てれねぇ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.FirstName + "：スキル不足だ。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.FirstName + "：ッオイ、ソイツは捨てるな。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.FirstName + "：復活！";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.FirstName + "：{0}にコイツは不要だ。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.FirstName + "：ヴォケも大概にしろ。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.FirstName + "：んなもん、装備させんな。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.FirstName + "：今はそんな時じゃねぇ。";
                    case 2021: // アイテム捨てるの催促
                        return this.FirstName + "：バックパック整備ぐらいしろ。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.FirstName + "：パラメタ再構築させてもらうぞ。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.FirstName + "：オーバーシフティング割り振りが先にさせろ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.FirstName + "：【{0}】パラメタ {1} 上昇。";
                    case 2025:
                        return this.FirstName + "：両手武器なんでな、サブは装備できねえ。";
                    case 2026:
                        return this.FirstName + "：武器（メイン）にまず装備だ。";
                    case 2027: // 清透水使用時
                        return this.FirstName + "：ライフ100%回復しといたぞ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.FirstName + "：もう枯れてしまってるな。一日１回だ。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.FirstName + "：バックパック満杯だ。装備外す前に整備しとけ。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.FirstName + "：{0}捨ててこのアイテム、入手か？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.FirstName + "：戦闘中だ。アイテム使用に集中しろ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.FirstName + "：このアイテムは戦闘中は使えねぇ。";
                    case 2033: // 預けられない場合
                        return this.FirstName + "：おい、何で預けなきゃなんねぇんだよ。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.FirstName + "：これ以上は面倒くせぇ。";
                    case 2035: // Sacred Heal
                        return this.FirstName + "：全員回復したぞ。";
                    case 2036: // オーバーシフティング割り振り完了
                        return this.FirstName + "：面倒くせえ割り振りだったな。";
                    case 2037: // １人のため、アイテムをわたす相手が居ない場合
                        return this.FirstName + "：相手いねぇだろうが、ボケが。";

                    case 3000: // 店に入った時の台詞
                        return this.FirstName + "：ジジィ・・・誰もいねえのはやべぇだろが・・・。";
                    case 3001: // 支払い要求時
                        return this.FirstName + "： {0} を買う。 {1} Gold払うぞ？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.FirstName + "：持ち物がいっぱいじゃねぇか。アイテム整備ぐらいしとけ。";
                    case 3003: // 購入完了時
                        return this.FirstName + "：ジジィ、売買成立だ。";
                    case 3004: // Gold不足で購入できない場合
                        return this.FirstName + "：Goldが{0}足りねえな。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.FirstName + "：他のアイテムを見せろ。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.FirstName + "：これを売る気はねえ。";
                    case 3007: // アイテム売却時
                        return this.FirstName + "：{0}で売るぜ。{1}Goldもらってくぞ、ジジィ。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.FirstName + "：マジで売るのか、{0}Goldだが？";
                    case 3009: // 武具店を出る時
                        return this.FirstName + "：ジジィ、さすがに見張り付けろ・・・";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.FirstName + "：ッカ・・・ザコアインが気付くかどうかだな。";
                    case 3011: // 装備可能なものが購入された時
                        return this.FirstName + "：装備するぞ？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.FirstName + "：今の装備は、{0}だ。あのジジィなら{1}Goldで買い取りってトコだ。";

                    case 4001: // 通常攻撃を選択
                        return this.FirstName + "：攻めるぜ。\r\n";
                    case 4002: // 防御を選択
                        return this.FirstName + "：防御させろ。\r\n";
                    case 4003: // 待機を選択
                        return this.FirstName + "：何もしねぇぞ。\r\n";
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                    case 4082: // FlashBlaze
                        return this.FirstName + "：" + this.ActionLabel.text + "だ。\r\n";

                    case 4072:
                        return this.FirstName + "：敵にかけれねぇ。\r\n";
                    case 4073:
                        return this.FirstName + "：敵にかけれねぇ。\r\n";
                    case 4074:
                        return this.FirstName + "：味方にはかけれねぇ。\r\n";
                    case 4075:
                        return this.FirstName + "：味方にはかけれねぇ。\r\n";
                    case 4076:
                        return this.FirstName + "：味方に攻撃はできねぇ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.FirstName + "：ためるぞ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.FirstName + "：メイン発動させるぞ。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.FirstName + "：サブ発動させるぞ。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.FirstName + "：アクセ１発動させるぞ。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.FirstName + "：アクセ２発動させるぞ。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.FirstName + "：エアロ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.FirstName + "：{0} 回復しといたぞ。 \r\n";
                    case 5003:
                        return this.FirstName + "：{0} マナ回復しといたぞ。\r\n";
                    case 5004:
                        return this.FirstName + "：アイシクル！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.FirstName + "：エレクトロ！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.FirstName + "：ブルーライト！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.FirstName + "：バーニン！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.FirstName + "：ブルーファイア！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.FirstName + "：{0} スキルポイント回復しといたぞ。\r\n";
                    case 5010:
                        return this.FirstName + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "カールハンツ"
            if (this.FirstName == Database.SINIKIA_KAHLHANZ)
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "：ッフ・・・スキル不足か。\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "：ストレート・スマッシュを食らえぃ！\r\n";
                    case 2: // Double Slash 1
                        return this.FirstName + "：っむぅん！！\r\n";
                    case 3: // Double Slash 2
                        return this.FirstName + "：っむおおぉ！\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "：【心眼奥義】ペインフル・インサニティを食らえぃ！\r\n";
                    case 5: // empty skill
                        return this.FirstName + "：ッフ・・・スキルの選択を忘れておったわ。\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "：ッ・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + ": ッ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "：ッ！・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 回復させてもらったぞ。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "：食らえぃ！ファイヤー・ボール！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "：っむぅん！フレーム・ストライク！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "：ぬうおぉ！ボルカニック・ウェーブ！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：おおおぉ、クリティカルダメージ！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "：踊れ炎よ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "：ッフ・・・戦闘中に使うはずがなかろう。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "：ッフ・・・使えんアイテムだったな。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "：ッフ・・・マナ不足とは。\r\n";
                    case 18: // Protection
                        return this.FirstName + "：聖の加護、プロテクション！物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：水の加護、アブソーブ・ウオーター！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：聖なる力、セイント・パワー！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：闇の契約、シャドー・パクト！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：復讐の力、ブラッデー・ベンジアンス！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "：聖なる衝撃、ホーリー・ショック！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：栄光、グローリー！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：生命の輝き、セレステアル・ノバ！ {0} 回復させてもらったぞ。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "：生命への裁き、セレステアル・ノバ！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "：闇の衝撃、ダーク・ブラスト！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "：炎の天使よ！焼き払えぃ！ラバ・アニヒレーション！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.FirstName + "：炎の天使よ！焼き払えぃ！ラバ・アニヒレーション！\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "：命を吸収せしめよ！デボーリング・プラグー！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "：氷の刃、アイス・ニードル！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "：氷結槍、フローズン・ランス！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "：っむうおぉ！タイダル・ウェイブ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "：力の衝波、ワード・オブ・パワー！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "：無に帰せよ、ホワイト・アウト！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：契約の力を見よ、ブラック・コントラクト！ " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：おおぉ炎よ！フレイム・オーラ！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：滅びなり、ダムネーション！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：っむぅん！ヒート・ブースト！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：炎に宿りし力！イモータル・レイブ！ " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：我が分身、現れよ！ゲール・ウインド！ もう一人の" + this.FirstName + "が現れた。\r\n";



                }
            }
            #endregion
            #region "ガンツ"
            else if (this.FirstName == "ガンツ")
            {
                switch (sentenceNumber)
                {
                    case 3000: // 店に入った時の台詞
                        return this.FirstName + "：ゆっくり見ていくがいい。";
                    case 3001: // 支払い要求時
                        return this.FirstName + "：{0}は{1}Goldだ。買うかね？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.FirstName + "：おや、荷物がいっぱいだよ。手持ちを減らしてからまた来なさい。";
                    case 3003: // 購入完了時
                        return this.FirstName + "：はいよ、まいどあり。";
                    case 3004: // Gold不足で購入できない場合
                        return this.FirstName + "：すまない、こちらも商売でな。後{0}必要だ。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.FirstName + "：他のも見ていくがいい。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.FirstName + "：すまないが、それは買い取れん。";
                    case 3007: // アイテム売却時
                        return this.FirstName + "：ふむ、{0}だな。{1}Goldで買い取ろうか。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.FirstName + "：ふむ・・・良い出来具合のアクセサリだ。{0}Goldだが、本当に買い取って良いのか？";
                    case 3009: // 武具店を出る時
                        return this.FirstName + "：またいつでも寄りなさい。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return "";
                    case 3011: // 装備可能なものが購入された時
                        return this.FirstName + "：ふむ、今ここで装備していくかね？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.FirstName + "：現在の装備品を売却するかね？{0}は{1}Goldで買い取ろう。";
                    case 3013: // 両手持ち装備をした後、サブ武器を売却せず、手元に残そうとして、バックパックがいっぱいの時
                        return this.FirstName + "：荷物がいっぱいのようだな。{0}はハンナの宿屋倉庫に後で送っておこう。";
                }
            }
            #endregion
            #region "三階の守護者：Minflore"
            else if (this.FirstName == "三階の守護者：Minflore")
            {
                switch (sentenceNumber)
                {
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.FirstName + "：ッハ！\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.FirstName + "：ッヤァ！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：ライフ回復させてもらうわ、FreshHeal。{0} 回復。\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：あなたのが弱点、見切ったわ！クリティカル！！ {0} へ {1}のダメージ\r\n";
                    case 18: // Protection
                        return this.FirstName + "：聖なる神よ、われに与えよ、Protection。 物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：水の女神よ、われに与えよ、AbsorbWater。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：力が全てを覆すわ、SaintPower。物理攻撃力：ＵＰ\r\n";
                    case 24: // Glory
                        return this.FirstName + "：光を照らし、栄光を私に！Glory！　 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "：これでも食らいなさい、CrushingBlow。  {0} へ {1} のダメージ。\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：ここでクリティカルヒットよ！\r\n";
                }
            }
            #endregion
            #region "四階の守護者：Altomo"
            else if (this.FirstName == "四階の守護者：Altomo")
            {
                switch (sentenceNumber)
                {
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "：当たれ！！クリティカル！！ {0} へ {1} のダメージ\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "：次は必殺だ、さあ死んでもらおうか！ 決死のオーラが湧き上がった。\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "：パラメタＵＰなどとこざかしいな！　対象のパラメタＵＰを無視するようになった。\r\n";
                    case 70: // Scatter Shot (Crushing Blow)
                        return this.FirstName + "：邪魔だ、寝てろ。　ScatterShot！ {0} へ {1}のダメージ。\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "{0} は魔法詠唱ができなくなった！\r\n";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "：そんな構えぐらいお見通しだ！　効くわけがないだろう！！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "：防御など無駄な行為だ！　甘いな！！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：っふん、クリティカルだ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "：っふん、もともとライフ回復などと、邪推するはずなかろう！！\r\n";
                }
            }
            #endregion
            #region "五階の守護者：Bystander"
            else if (this.FirstName == "五階の守護者：Bystander")
            {
                switch (sentenceNumber)
                {
                    case 18: // Protection
                        return this.FirstName + "：『聖魔法』  －　『Protection』　物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "：『水魔法』  －　『AbsorbWater』　魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "：『聖魔法』  －　『SaintPower』　物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "：『闇魔法』  －　『ShadowPact』　魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "：『闇魔法』  －　『BloodyVengeance』　力パラメタが {0} ＵＰ\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "：『火魔法』  －　『HeatBoost』　技パラメタが {0} ＵＰ。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "：『理魔法』  －　『EternalPresence』　" + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "：『空魔法』  －　『RiseOfImage』　心パラメタが {0} ＵＰ\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "：『水魔法』  －　『PromisedKnowledge』　知パラメタが {0} ＵＰ\r\n";

                    case 48: // Dispel Magic
                        return this.FirstName + "：『空魔法』  －　『DispelMagic』\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "：『空魔法』  －　『Tranquility』\r\n";
                    case 37: // Damnation
                        return this.FirstName + "：『闇魔法』　－　『Damnation』\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "：『闇魔法』　－　『BlackContract』\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "：『水魔法』　－　『AbsoluteZero』　対象はライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可になる。\r\n";

                    case 43: // Aether Drive
                        return this.FirstName + "：『AetherDrive』  周囲全体に空想物理力がみなぎる。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "：『ImmortalRave』 " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "：『GaleWind』  もう一人のBystanderが現れた。\r\n";
                    case 24: // Glory
                        return this.FirstName + "：『Glory』  直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return "『FlameAura』が襲いかかる！ {0} ダメージ\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "：『CarnageRush』 『壱』 {0}ダメージ   \r\n";
                    case 66: // Carnage Rush 2
                        return this.FirstName + "：『CarnageRush』 『弐』 {0}ダメージ   \r\n";
                    case 67: // Carnage Rush 3
                        return this.FirstName + "：『CarnageRush』 『参』 {0}ダメージ   \r\n";
                    case 68: // Carnage Rush 4
                        return this.FirstName + "：『CarnageRush』 『四』 {0}ダメージ   \r\n";
                    case 69: // Carnage Rush 5
                        return this.FirstName + "：『CarnageRush』 『終』 {0}のダメージ\r\n";
                    case 53: // 対象不適切
                        return this.FirstName + "：既に『対象』は『消滅』している。\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "：{0} 『回復』\r\n";

                    case 57: // Mirror Image
                        return this.FirstName + "『水魔法』　－　『MirrorImage』 {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "は魔法をはじき返そうとした。 {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "は魔法をはじき返そうとした。 【しかし、強力な威力ではね返せない！】\r\n";
                    case 60: // Deflection
                        return this.FirstName + "『空魔法』　－　『AbsoluteZero』 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "は物理ダメージをはじき返そうとした！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "は物理ダメージをはじき返そうとした！ 【しかし、強力な威力ではね返せない！】\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";

                    case 27: // Dark Blast
                        return "『DarkBlast』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 23: // Holy Shock
                        return "『HolyShock』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 30: // Ice Needle
                        return "『IceNeedle』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 10: // Fire Ball
                        return "『FireBall』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 33: // Word of Power
                        return "『WordOfPower』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return "『FlameStrike』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return "『FrozenLance』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return "『VolcanicWave』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return "『WhiteOut』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return "『LavaAnnihilation』が襲いかかる！ {0} へ {1} のダメージ\r\n";

                    case 41: // Word of Life
                        return this.FirstName + "：『WordOfLife』　大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "：『CelestialNova』　{0} 『回復』\r\n";

                    case 36: // Flame Aura詠唱
                        return this.FirstName + "：『火魔法』  －　『FlameAura』 直接攻撃に炎の追加効果が付与される。\r\n";

                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "：『無心スキル』  －　『NothingOfNothingness』　無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";

                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "：『クリティカルヒット』\r\n";
                }
            }
            #endregion
            #region "不特定多数"
            else
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.FirstName + "のスキルポイントが足りない！\r\n";
                    case 1: // Straight Smash
                        return this.FirstName + "はストレート・スマッシュを繰り出した。\r\n";
                    case 2: // Double Slash 1
                        return this.FirstName + "  １回目の攻撃！\r\n";
                    case 3: // Double Slash 2
                        return this.FirstName + "  ２回目の攻撃！\r\n";
                    case 4: // Painful Insanity
                        return this.FirstName + "は【心眼奥義】PainfulInsanityを繰り出した！\r\n";
                    case 5: // empty skill
                        return this.FirstName + "はスキル選択し忘れていた！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.FirstName + "は必死に堪えている・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.FirstName + "は必死に堪えている・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.FirstName + "は必死に堪えている・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.FirstName + "は{0} 回復した。\r\n";
                    case 10: // Fire Ball
                        return this.FirstName + "はFireBallを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.FirstName + "はFlameStrikeを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.FirstName + "はVolcanicWaveを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.FirstName + "からのクリティカルヒット！ {0} へ {1} のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.FirstName + "のFlameAuraによる {0} の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.FirstName + "はやみくもにアイテムを使った。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.FirstName + "はやみくもにアイテムを使った。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.FirstName + "のマナが足りない！\r\n";
                    case 18: // Protection
                        return this.FirstName + "はProtectionを唱えた！ 物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.FirstName + "はAbsorbWaterを唱えた！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.FirstName + "はSaintPowerを唱えた！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.FirstName + "はShadowPactを唱えた！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.FirstName + "はBloodyVengeanceを唱えた！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.FirstName + "はHolyShockを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.FirstName + "はGloryを唱えた！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.FirstName + "はCelestialNovaを唱えた！ {0} 回復。\r\n";
                    case 26: // CelestialNova 2
                        return this.FirstName + "はCelestialNovaを唱えた！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.FirstName + "はDarkBlastを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.FirstName + "はLavaAnnihilationを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 29: // Devouring Plague
                        return this.FirstName + "はDevouringPlagueを唱えた！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.FirstName + "はIceNeedleを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.FirstName + "はFrozenLanceを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.FirstName + "はTidalWaveを唱えた！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.FirstName + "はWordOfPowerを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.FirstName + "はWhiteOutを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.FirstName + "はBlackContractを唱えた！ " + this.FirstName + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.FirstName + "はFlameAuraを唱えた！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.FirstName + "はDamnationを唱えた！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.FirstName + "はHeatBoostを唱えた！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.FirstName + "はImmortalRaveを唱えた！ " + this.FirstName + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.FirstName + "はGaleWindを唱えた！ もう一人の" + this.FirstName + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.FirstName + "はWordOfLifeを唱えた！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.FirstName + "はWordOfFortuneを唱えた！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.FirstName + "はAetherDriveを唱えた！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.FirstName + "はEternalPresenceを唱えた！ " + this.FirstName + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.FirstName + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.FirstName + "はOneImmunityを唱えた！ " + this.FirstName + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.FirstName + "はTimeStopを唱えた！ 相手の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.FirstName + "はDispelMagicを唱えた！ \r\n";
                    case 49: // Rise of Image
                        return this.FirstName + "はRiseOfImageを唱えた！ 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.FirstName + "は詠唱に失敗した！\r\n";
                    case 51: // Inner Inspiration
                        return this.FirstName + "はInnerInspirationを繰り出した！ {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.FirstName + "はResurrectionを唱えた！！\r\n";
                    case 53: // Resurrection 2
                        return this.FirstName + "は対象を間違えていた！！\r\n";
                    case 54: // Resurrection 3
                        return this.FirstName + "は対象を間違えていた！！\r\n";
                    case 55: // Resurrection 4
                        return this.FirstName + "は対象を間違えていた！！\r\n";
                    case 56: // Stance Of Standing
                        return this.FirstName + "はStanceOfStandingを繰り出した！\r\n";
                    case 57: // Mirror Image
                        return this.FirstName + "はMirrorImageを唱えた！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.FirstName + "は魔法をはじき返そうとした。 {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.FirstName + "は魔法をはじき返そうとした。 【しかし、強力な威力ではね返せない！】\r\n";
                    case 60: // Deflection
                        return this.FirstName + "はDeflectionを唱えた！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.FirstName + "は物理ダメージをはじき返そうとした！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.FirstName + "は物理ダメージをはじき返そうとした！ 【しかし、強力な威力ではね返せない！】\r\n";
                    case 63: // Truth Vision
                        return this.FirstName + "はTruthVisionを繰り出した！ " + this.FirstName + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.FirstName + "はStanceOfFlowを繰り出した！ " + this.FirstName + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.FirstName + "はCarnageRushを繰り出した！ １発目{0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return " ２発目{0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return " ３発目{0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return " ４発目{0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return " ５発目{0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.FirstName + "はCrushingBlowを繰り出した！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.FirstName + "はアイテムを使用してきた！\r\n";
                    case 72: // Enigma Sence
                        return this.FirstName + "はEnigmaSenceを繰り出した！\r\n";
                    case 73: // Soul Infinity
                        return this.FirstName + "はSoulInfinityを繰り出した！\r\n";
                    case 74: // Kinetic Smash
                        return this.FirstName + "はKineticSmashを繰り出した！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.FirstName + "は魔法詠唱に失敗した！！\r\n";
                    case 77: // Cleansing
                        return this.FirstName + "はCleansingを唱えた！\r\n";
                    case 78: // Pure Purification
                        return this.FirstName + "はPurePurificationを繰り出した！\r\n";
                    case 79: // Void Extraction
                        return this.FirstName + "はVoidExtractionを繰り出した！ " + this.FirstName + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.FirstName + "はアイテムを使用してきた！\r\n";
                    case 81: // Absolute Zero
                        return this.FirstName + "はAbsoluteZeroを唱えた！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.FirstName + "はPromiesdKnowledgeを唱えた！ 知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.FirstName + "はTranquilityを唱えた！\r\n";
                    case 85: // High Emotionality 1
                        return this.FirstName + "はHighEmotionalityを繰り出した！\r\n";
                    case 86: // High Emotionality 2
                        return this.FirstName + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.FirstName + "は絶対零度効果により、スキルの使用に失敗した！\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.FirstName + "は絶対零度効果により、防御できないままでいる！ \r\n";
                    case 89: // Silent Rush 1
                        return this.FirstName + "はSilentRushを繰り出した！ １発目 {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "２発目 {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "３発目 {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.FirstName + "はAntiStunを繰り出した！ " + this.FirstName + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.FirstName + "はAntiStun効果によりスタンを回避した。\r\n";
                    case 95: // Stance Of Death
                        return this.FirstName + "はStanceOfDeathを繰り出した！ " + this.FirstName + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.FirstName + "は【究極奥義】OboroImpactを繰り出した！\r\n";
                    case 97: // Oboro Impact 2
                        return "{0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.FirstName + "は【究極奥義】Catastropheを繰り出した！\r\n";
                    case 99: // Catastrophe 2
                        return this.FirstName + " {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.FirstName + "はStanceOfEyesを繰り出した！ " + this.FirstName + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.FirstName + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.FirstName + "は相手のモーションを見切ろうとしたが、相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.FirstName + "はNegateを繰り出した！ " + this.FirstName + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.FirstName + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.FirstName + "は相手のスペル詠唱を弾こうとしたが、相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.FirstName + "は【究極奥義】NothingOfNothingnessを繰り出した！ " + this.FirstName + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.FirstName + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.FirstName + "はGenesisを唱えた！ " + this.FirstName + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.FirstName + "は調子が悪いため、Cleansingの詠唱に失敗した。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.FirstName + "はCounterAttackの構えを無視した。\r\n";
                    case 111: // 神聖水使用時
                        return this.FirstName + "はアイテムを使用してきた！\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.FirstName + "が使ったアイテムは効果がなかった！\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.FirstName + "はカウンターアタックを繰り出した！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.FirstName + "はカウンターできなかった！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.FirstName + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.FirstName + "は防御を無視して攻撃してきた！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.FirstName + "からのクリティカルヒット！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.FirstName + "はリヴァイヴポーションを使用した！";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.FirstName + "は絶対零度効果によりライフ回復できない！\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.FirstName + "は凍てつく寒さによりマナ回復ができない。\r\n";
                    case 122: // 「ためる」行動時
                        return this.FirstName + "は魔力を蓄えた。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.FirstName + "は魔力を蓄えようとしたが、これ以上蓄えられないでいる。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.FirstName + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.FirstName + "はアイテムゲージが溜まってない間、アイテムを使えないでいる。\r\n";
                    case 126: // FlashBlase
                        return this.FirstName + "：はFlashBlazeを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.FirstName + "のインスタント値が足りない！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.FirstName + "はインスタントタイミングで発動できないでいる。\r\n";
                    case 129: // PsychicTrance
                        return this.FirstName + "はPsychicTranceを唱えた！\r\n";
                    case 130: // BlindJustice
                        return this.FirstName + "はBlindJusticeを唱えた！\r\n";
                    case 131: // TranscendentWish
                        return this.FirstName + "はTranscendentWishを唱えた！\r\n";
                    case 132: // LightDetonator
                        return this.FirstName + "はLightDetonatorを唱えた！\r\n";
                    case 133: // AscendantMeteor
                        return this.FirstName + "はAscendantMeteorを唱えた！\r\n";
                    case 134: // SkyShield
                        return this.FirstName + "はSkyShieldを唱えた！\r\n";
                    case 135: // SacredHeal
                        return this.FirstName + "はSacredHealを唱えた！\r\n";
                    case 136: // EverDroplet
                        return this.FirstName + "はEverDropletを唱えた！\r\n";
                    case 137: // FrozenAura
                        return this.FirstName + "はFrozenAuraを唱えた！\r\n";
                    case 138: // ChillBurn
                        return this.FirstName + "はChillBurnを唱えた！\r\n";
                    case 139: // ZetaExplosion
                        return this.FirstName + "はZetaExplosionを唱えた！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.FirstName + "のFrozenAuraによる {0} の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.FirstName + "はStarLightningを唱えた！\r\n";
                    case 142: // WordOfMalice
                        return this.FirstName + "はWordOfMaliceを唱えた！\r\n";
                    case 143: // BlackFire
                        return this.FirstName + "はBlackFireを唱えた！\r\n";
                    case 144: // EnrageBlast
                        return this.FirstName + "はEnrageBlastを唱えた！\r\n";
                    case 145: // Immolate
                        return this.FirstName + "はImmolateを唱えた！\r\n";
                    case 146: // VanishWave
                        return this.FirstName + "はVanishWaveを唱えた！\r\n";
                    case 147: // WordOfAttitude
                        return this.FirstName + "はWordOfAttitudeを唱えた！\r\n";
                    case 148: // HolyBreaker
                        return this.FirstName + "はHolyBreakerを唱えた！\r\n";
                    case 149: // DarkenField
                        return this.FirstName + "はDarkenFieldを唱えた！\r\n";
                    case 150: // SeventhMagic
                        return this.FirstName + "はSeventhMagicを唱えた！\r\n";
                    case 151: // BlueBullet
                        return this.FirstName + "はBlueBulletを唱えた！\r\n";
                    case 152: // NeutralSmash
                        return this.FirstName + "はNeutralSmashを繰り出した！\r\n";
                    case 153: // SwiftStep
                        return this.FirstName + "はSwiftStepを繰り出した！\r\n";
                    case 154: // CircleSlash
                        return this.FirstName + "はCircleSlashを繰り出した！\r\n";
                    case 155: // RumbleShout
                        return this.FirstName + "はRumbleShoutを繰り出した！\r\n";
                    case 156: // SmoothingMove
                        return this.FirstName + "はSmoothingMoveを繰り出した！\r\n";
                    case 157: // FutureVision
                        return this.FirstName + "はFutureVisionを繰り出した！\r\n";
                    case 158: // ReflexSpirit
                        return this.FirstName + "はReflexSpiritを繰り出した！\r\n";
                    case 159: // SharpGlare
                        return this.FirstName + "はSharpGlareを繰り出した！\r\n";
                    case 160: // TrustSilence
                        return this.FirstName + "はTrustSilenceを繰り出した！\r\n";
                    case 161: // SurpriseAttack
                        return this.FirstName + "はSurpriseAttackを繰り出した！\r\n";
                    case 162: // PsychicWave
                        return this.FirstName + "はPsychicWaveを繰り出した！\r\n";
                    case 163: // Recover
                        return this.FirstName + "はRecoverを繰り出した！\r\n";
                    case 164: // ViolentSlash
                        return this.FirstName + "はViolentSlashを繰り出した！\r\n";
                    case 165: // OuterInspiration
                        return this.FirstName + "はOuterInspirationを繰り出した！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.FirstName + "はStanceOfSuddennessを繰り出した！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.FirstName + "は対象のインスタントコマンドが無く戸惑っている。\r\n";
                    case 168: // StanceOfMystic
                        return this.FirstName + "はStanceOfMysticを繰り出した！\r\n";
                    case 169: // HardestParry
                        return this.FirstName + "はHardestParryを繰り出した！\r\n";
                    case 170: // ConcussiveHit
                        return this.FirstName + "はConcussiveHitを繰り出した！\r\n";
                    case 171: // Onslaught hit
                        return this.FirstName + "はOnslaughtHitを繰り出した！\r\n";
                    case 172: // Impulse hit
                        return this.FirstName + "はImpulseHitを繰り出した！\r\n";
                    case 173: // Fatal Blow
                        return this.FirstName + "はFatalBlowを繰り出した！\r\n";
                    case 174: // Exalted Field
                        return this.FirstName + "はExaltedFieldを唱えた！\r\n";
                    case 175: // Rising Aura
                        return this.FirstName + "はRisingAuraを繰り出した！\r\n";
                    case 176: // Ascension Aura
                        return this.FirstName + "はAscensioAuraを繰り出した！\r\n";
                    case 177: // Angel Breath
                        return this.FirstName + "はAngelBreathを唱えた！\r\n";
                    case 178: // Blazing Field
                        return this.FirstName + "はBlazingFieldを唱えた！\r\n";
                    case 179: // Deep Mirror
                        return this.FirstName + "はDeepMirrorを唱えた！\r\n";
                    case 180: // Abyss Eye
                        return this.FirstName + "はAbyssEyeを唱えた！\r\n";
                    case 181: // Doom Blade
                        return this.FirstName + "はDoomBladeを繰り出した！\r\n";
                    case 182: // Piercing Flame
                        return this.FirstName + "はPiercingFlameを唱えた！\r\n";
                    case 183: // Phantasmal Wind
                        return this.FirstName + "はPhantasmalWindを唱えた！\r\n";
                    case 184: // Paradox Image
                        return this.FirstName + "はParadoxImageを唱えた！\r\n";
                    case 185: // Vortex Field
                        return this.FirstName + "はVortexFieldを唱えた！\r\n";
                    case 186: // Static Barrier
                        return this.FirstName + "はStaticBarrierを唱えた！\r\n";
                    case 187: // Unknown Shock
                        return this.FirstName + "はUnknownShockを繰り出した！\r\n";
                    case 188: // SoulExecution
                        return this.FirstName + "は【究極奥義】SoulExecutionを繰り出した！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.FirstName + "１発目！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.FirstName + "２発目！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.FirstName + "３発目！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.FirstName + "４発目！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.FirstName + "５発目！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.FirstName + "６発目！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.FirstName + "７発目！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.FirstName + "８発目！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.FirstName + "９発目！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.FirstName + "１０発目！\r\n";
                    case 199: // Nourish Sense
                        return this.FirstName + "はNourishSenseを繰り出した！\r\n";
                    case 200: // Mind Killing
                        return this.FirstName + "はMindKillingを繰り出した！\r\n";
                    case 201: // Vigor Sense
                        return this.FirstName + "はVigorSenseを繰り出した！\r\n";
                    case 202: // ONE Authority
                        return this.FirstName + "はOneAuthorityを繰り出した！\r\n";
                    case 203: // 集中と断絶
                        return this.FirstName + "は【集中と断絶】を発動した！\r\n";
                    case 204: // 【元核】発動済み
                        return this.FirstName + "は既に【元核】を使用しており、これ以上発動が行えなかった。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.FirstName + "の【元核】はインスタントタイミング限定であり、通常行動に選択できなかった。\r\n";
                    case 206: // Sigil Of Homura
                        return this.FirstName + "はSigilOfHomuraを唱えた！\r\n";
                    case 207: // Austerity Matrix
                        return this.FirstName + "はAusterityMatrixを唱えた！\r\n";
                    case 208: // Red Dragon Will
                        return this.FirstName + "はRedDragonWillを唱えた！\r\n";
                    case 209: // Blue Dragon Will
                        return this.FirstName + "はBlueDragonWillを唱えた！\r\n";
                    case 210: // Eclipse End
                        return this.FirstName + "はEclipseEndを唱えた！\r\n";
                    case 211: // Sin Fortune
                        return this.FirstName + "はSinFortuneを唱えた！\r\n";
                    case 212: // AfterReviveHalf
                        return this.FirstName + "に死耐性（ハーフ）が付与された！\r\n";
                    case 213: // DemonicIgnite
                        return this.FirstName + "はDemonicIgniteを唱えた！\r\n";
                    case 214: // Death Deny
                        return this.FirstName + "はDeathDenyを唱えた！\r\n";
                    case 215: // Stance of Double
                        return this.FirstName + "はStanceOfDoubleを放った！　" + this.FirstName + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.FirstName + "の生命が削りとられる代わりに、その場で生き残った！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.FirstName + "の生命は完全に消滅させらた・・・\r\n";
                    case 218: // インスタント不足
                        return this.FirstName + "はインスタント値が不足している。\r\n";

                    case 4001: // 通常攻撃を選択
                    case 4002: // 防御を選択
                    case 4003: // 待機を選択
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.FirstName + "は" + this.ActionLabel.text + "を選択した。\r\n";
                    case 4072:
                        return this.FirstName + "は敵を対象にするのをためらっている。\r\n";
                    case 4073:
                        return this.FirstName + "は敵を対象にするのをためらっている。\r\n";
                    case 4074:
                        return this.FirstName + "は味方を対象にするのをためらっている。\r\n";
                    case 4075:
                        return this.FirstName + "は味方を対象にするのをためらっている。\r\n";
                    case 4076:
                        return this.FirstName + "は味方に攻撃するのをためらっている。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.FirstName + "は魔力をため込み始めた。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.FirstName + "はメイン武器の効果発動を選択した。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.FirstName + "はサブ武器の効果発動を選択した。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.FirstName + "はアクセサリ１の効果発動を選択した。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.FirstName + "はアクセサリ２の効果発動を選択した。\r\n";

                    case 4082: // FlashBlaze
                        return this.FirstName + "はフラッシュブレイズを選択した。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.FirstName + "はエアロ・スラッシュを放った。 {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.FirstName + "は{0}ライフ回復した。 \r\n";
                    case 5003:
                        return this.FirstName + "は{0}マナ回復した \r\n";
                    case 5004:
                        return this.FirstName + "はアイシクル・スラッシュを放った。 {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.FirstName + "はエレクトロ・ブローを放った。 {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.FirstName + "はブルー・ライトニングを放った。 {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.FirstName + "はバーニング・クレイモアを放った。 {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.FirstName + "は赤蒼授の杖から蒼の炎を放った。 {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.FirstName + "は{0}スキルポイント回復した。\r\n";
                    case 5010:
                        return this.FirstName + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion

            return "";
        }


        public void ActivateBuff(TruthImage imageData, string imageName, int count)
        {
            if (imageData.sprite == null)
            {
                imageData.sprite = Resources.Load<Sprite>(imageName);
                imageData.rectTransform.anchoredPosition = new Vector3(-1 * (Database.BUFFPANEL_OFFSET_X + Database.BUFFPANEL_BUFF_WIDTH) * (this.BuffNumber), 0);
                imageData.Count = count;
                imageData.gameObject.SetActive(true);
                this.BuffNumber++;
            }
        }

        public void ChangeBuffImage(TruthImage imageData, string imageName)
        {
            if (imageData.sprite != null)
            {
                imageData.sprite = Resources.Load<Sprite>(imageName);
            }
        }
        public void DeBuff(TruthImage imageData)
        {
            if (imageData.sprite != null)
            {
                RemoveOneBuff(imageData);
                this.BuffNumber--;
            }
        }

        private void RemoveOneBuff(TruthImage imageBox)
        {
            Vector3 tempPoint = new Vector3(imageBox.transform.position.x - Database.BUFFPANEL_BUFF_WIDTH, 0);
            imageBox.Count = 0;
            imageBox.Cumulative = 0;
            imageBox.sprite = null;
            imageBox.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0);
            imageBox.gameObject.SetActive(false);
            //imageBox.Update();

            MoveNextBuff(tempPoint);
        }

        private void MoveNextBuff(Vector3 tempPoint)
        {
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                if (tempPoint == this.BuffElement[ii].transform.position)
                {
                    this.BuffElement[ii].transform.position = new Vector3(this.BuffElement[ii].transform.position.x + Database.BUFFPANEL_BUFF_WIDTH, 0);
                    Vector3 tempPointB = new Vector3(tempPoint.x - Database.BUFFPANEL_BUFF_WIDTH, 0);
                    MoveNextBuff(tempPointB);
                    break;
                }
            }

        }
        protected ItemBackPack[] backpack;
        #region "バックパック制御関連"
        public MainCharacter()
        {
            backpack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
        }

        // s 後編編集
        /// <summary>
        /// バックパックにアイテムを追加します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns>TRUE:追加完了、FALSE:満杯のため追加できない</returns>
        public bool AddBackPack(ItemBackPack item)
        {
            return AddBackPack(item, 1);
        }
        public bool AddBackPack(ItemBackPack item, int addValue)
        {
            int dummyValue = 0;
            return AddBackPack(item, addValue, ref dummyValue);
        }
        public bool AddBackPack(ItemBackPack item, int addValue, ref int addedNumber)
        {
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                // まだ持っていない場合、１つ目として生成する。
                if (this.backpack[ii] == null)
                {
                    // いや、次を探索すると同名アイテムを持っているかもしれないので、まず検索する。
                    for (int jj = ii + 1; jj < Database.MAX_BACKPACK_SIZE; jj++) // 後編編集
                    {
                        if (CheckBackPackExist(item, jj) > 0)
                        {
                            // スタック上限以上の場合、別のアイテムとして追加する。
                            if (this.backpack[jj].StackValue >= item.LimitValue)
                            {
                                // 次のアイテムリストへスルー
                                break;
                            }
                            else
                            {
                                // スタック上限を超えていなくても、多数追加で上限を超えてしまう場合
                                if (this.backpack[jj].StackValue + addValue > item.LimitValue)
                                {
                                    // 次のアイテムリストへスルー
                                    break;
                                }
                                else
                                {
                                    this.backpack[jj].StackValue += addValue;
                                    addedNumber = jj;
                                    return true;
                                }
                            }
                        }
                    }

                    // やはり探索しても無かったので、そのまま追加する。
                    this.backpack[ii] = item;
                    this.backpack[ii].StackValue = addValue;
                    addedNumber = ii;
                    return true;
                }
                else
                {
                    // 既に持っている場合、スタック量を増やす。
                    if (this.backpack[ii].Name == item.Name)
                    {
                        // スタック上限以上の場合、別のアイテムとして追加する。
                        if (this.backpack[ii].StackValue >= item.LimitValue)
                        {
                            // 次のアイテムリストへスルー
                        }
                        else
                        {
                            // スタック上限を超えていなくても、多数追加で上限を超えてしまう場合
                            if (this.backpack[ii].StackValue + addValue > item.LimitValue)
                            {
                                // 次のアイテムリストへスルー
                            }
                            else
                            {
                                this.backpack[ii].StackValue += addValue;
                                addedNumber = ii;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// バックパックのアイテムを削除します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item)
        {
            return DeleteBackPack(item, 0);
        }
        /// <summary>
        /// バックパックのアイテムを指定した数だけ削除します。
        /// </summary>
        /// <param name="item"></param>
        /// <param name="deleteValue">削除する数 ０：全て削除、正値：指定数だけ削除</param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item, int deleteValue)
        {
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                if (this.backpack[ii] != null)
                {
                    if (this.backpack[ii].Name == item.Name)
                    {
                        if (deleteValue <= 0)
                        {
                            this.backpack[ii] = null;
                            break;
                        }
                        else
                        {
                            // スタック量が正値の場合、指定されたスタック量を減らす。
                            this.backpack[ii].StackValue -= deleteValue;
                            if (this.backpack[ii].StackValue <= 0) // 結果的にスタック量が０になった場合はオブジェクトを消す。
                            {
                                this.backpack[ii] = null;
                            }
                            break;
                        }
                    }
                }
            }
            return true;
        }
        public bool DeleteBackPack(ItemBackPack item, int deleteValue, int ii)
        {
            if (this.backpack[ii] != null)
            {
                if (this.backpack[ii].Name == item.Name)
                {
                    // スタック量が１以下の場合、生成されているオブジェクトを消す。
                    if (this.backpack[ii].StackValue <= 1)
                    {
                        this.backpack[ii] = null;
                    }
                    else
                    {
                        // スタック量が１より大きい場合、指定されたスタック量を減らす。
                        this.backpack[ii].StackValue -= deleteValue;
                        if (this.backpack[ii].StackValue <= 0) // 結果的にスタック量が０になった場合はオブジェクトを消す。
                        {
                            this.backpack[ii] = null;
                        }
                    }
                }
            }
            return true;
        }

        public void DeleteBackPackAll()
        {
            this.backpack = null;
            this.backpack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
        }
        /// <summary>
        /// バックパックに対象のアイテムが含まれている数を示します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CheckBackPackExist(ItemBackPack item, int ii)
        {
            if (this.backpack[ii] != null)
            {
                if (this.backpack[ii].Name == item.Name)
                {
                    return this.backpack[ii].StackValue;
                }
            }
            return 0;
        }

        /// <summary>
        /// アイテム内容を全面的に入れ替えます。
        /// </summary>
        /// <param name="item"></param>
        public void ReplaceBackPack(ItemBackPack[] item)
        {
            this.backpack = null;
            this.backpack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
            this.backpack = item;
        }

        /// <summary>
        /// バックパックの内容を一括で全て取得します。
        /// </summary>
        /// <returns></returns>
        public ItemBackPack[] GetBackPackInfo()
        {
            return backpack;
        }
        // e 後編編集

        public ItemBackPack[] SortByUsed()
        {
            return SortBySomeType(0);
        }

        public ItemBackPack[] SortByAccessory()
        {
            return SortBySomeType(1);
        }

        public ItemBackPack[] SortByWeapon()
        {
            return SortBySomeType(2);
        }

        public ItemBackPack[] SortByArmor()
        {
            return SortBySomeType(3);
        }

        public ItemBackPack[] SortByName()
        {
            return SortBySomeType(4);
        }

        public ItemBackPack[] SortByRare()
        {
            return SortBySomeType(5);
        }

        private ItemBackPack[] SortBySomeType(int type)
        {
            ItemBackPack[] newBackPack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
            int jj = 0;

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                if (backpack[ii] != null)
                {
                    newBackPack[jj] = backpack[ii];
                    jj++;
                }
            }

            if (type == 0)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackUsed());
            }
            else if (type == 1)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackAccessory());
            }
            else if (type == 2)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackWeapon());
            }
            else if (type == 3)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackArmor());
            }
            else if (type == 4)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackName());
            }
            else if (type == 5)
            {
                System.Array.Sort((System.Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackRare());
            }
            this.backpack = newBackPack;
            return newBackPack;
        }
        #endregion

        public int NextLevelBorder
        {
            get
            {
                int nextValue = 0;
                switch (this.level)
                {
                    case 1:
                        nextValue = 400;
                        break;
                    case 2:
                        nextValue = 900;
                        break;
                    case 3:
                        nextValue = 1400;
                        break;
                    case 4:
                        nextValue = 2100;
                        break;
                    case 5:
                        nextValue = 2800;
                        break;
                    case 6:
                        nextValue = 3600;
                        break;
                    case 7:
                        nextValue = 4500;
                        break;
                    case 8:
                        nextValue = 5400;
                        break;
                    case 9:
                        nextValue = 6500;
                        break;
                    case 10:
                        nextValue = 7600;
                        break;
                    case 11:
                        nextValue = 8700;
                        break;
                    case 12:
                        nextValue = 9800;
                        break;
                    case 13:
                        nextValue = 11000;
                        break;
                    case 14:
                        nextValue = 12300;
                        break;
                    case 15:
                        nextValue = 13600;
                        break;
                    case 16:
                        nextValue = 15000;
                        break;
                    case 17:
                        nextValue = 16400;
                        break;
                    case 18:
                        nextValue = 17800;
                        break;
                    case 19:
                        nextValue = 19300;
                        break;
                    case 20:
                        nextValue = 20800;
                        break;
                    case 21:
                        nextValue = 22400;
                        break;
                    case 22:
                        nextValue = 24000;
                        break;
                    case 23:
                        nextValue = 25500;
                        break;
                    case 24:
                        nextValue = 27200;
                        break;
                    case 25:
                        nextValue = 28900;
                        break;
                    case 26:
                        nextValue = 30500;
                        break;
                    case 27:
                        nextValue = 32200;
                        break;
                    case 28:
                        nextValue = 33900;
                        break;
                    case 29:
                        nextValue = 36300;
                        break;
                    case 30:
                        nextValue = 38800;
                        break;
                    case 31:
                        nextValue = 41600;
                        break;
                    case 32:
                        nextValue = 44600;
                        break;
                    case 33:
                        nextValue = 48000;
                        break;
                    case 34:
                        nextValue = 51400;
                        break;
                    case 35:
                        nextValue = 55000;
                        break;
                    case 36:
                        nextValue = 58700;
                        break;
                    case 37:
                        nextValue = 62400;
                        break;
                    case 38:
                        nextValue = 66200;
                        break;
                    case 39:
                        nextValue = 70200;
                        break;
                    case 40:
                        nextValue = 74300;
                        break;
                    // s 後編追加
                    case 41:
                        nextValue = 78500;
                        break;
                    case 42:
                        nextValue = 82800;
                        break;
                    case 43:
                        nextValue = 87100;
                        break;
                    case 44:
                        nextValue = 91600;
                        break;
                    case 45:
                        nextValue = 96300;
                        break;
                    case 46:
                        nextValue = 101000;
                        break;
                    case 47:
                        nextValue = 105800;
                        break;
                    case 48:
                        nextValue = 110700;
                        break;
                    case 49:
                        nextValue = 115700;
                        break;
                    case 50:
                        nextValue = 120900;
                        break;
                    case 51:
                        nextValue = 126100;
                        break;
                    case 52:
                        nextValue = 131500;
                        break;
                    case 53:
                        nextValue = 137000;
                        break;
                    case 54:
                        nextValue = 142500;
                        break;
                    case 55:
                        nextValue = 148200;
                        break;
                    case 56:
                        nextValue = 154000;
                        break;
                    case 57:
                        nextValue = 159900;
                        break;
                    case 58:
                        nextValue = 165800;
                        break;
                    case 59:
                        nextValue = 172000;
                        break;
                    case 60:
                        nextValue = 290000;
                        break;
                    case 61:
                        nextValue = 317000;
                        break;
                    case 62:
                        nextValue = 349000;
                        break;
                    case 63:
                        nextValue = 386000;
                        break;
                    case 64:
                        nextValue = 428000;
                        break;
                    case 65:
                        nextValue = 475000;
                        break;
                    case 66:
                        nextValue = 527000;
                        break;
                    case 67:
                        nextValue = 585000;
                        break;
                    case 68:
                        nextValue = 648000;
                        break;
                    case 69:
                        nextValue = 717000;
                        break;
                    case 70:
                        nextValue = 1523800;
                        break;
                    // e 後編追加
                }

                if (this.level < 35)
                {
                    return nextValue / 4; // ハードモードの場合、２
                }
                else
                {
                    return nextValue / 2; // ハードモードの場合、１
                }
            }
        }
        
        public int LevelUpPointTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 5;
                        break;
                    case 2:
                        upPoint = 5;
                        break;
                    case 3:
                        upPoint = 5;
                        break;
                    case 4:
                        upPoint = 5;
                        break;
                    case 5:
                        upPoint = 5;
                        break;
                    case 6:
                        upPoint = 6;
                        break;
                    case 7:
                        upPoint = 6;
                        break;
                    case 8:
                        upPoint = 6;
                        break;
                    case 9:
                        upPoint = 7;
                        break;
                    case 10:
                        upPoint = 8;
                        break;
                    case 11:
                        upPoint = 8;
                        break;
                    case 12:
                        upPoint = 8;
                        break;
                    case 13:
                        upPoint = 8;
                        break;
                    case 14:
                        upPoint = 8;
                        break;
                    case 15:
                        upPoint = 9;
                        break;
                    case 16:
                        upPoint = 9;
                        break;
                    case 17:
                        upPoint = 9;
                        break;
                    case 18:
                        upPoint = 10;
                        break;
                    case 19:
                        upPoint = 10;
                        break;

                    case 20:
                        upPoint = 12;
                        break;
                    case 21:
                        upPoint = 12;
                        break;
                    case 22:
                        upPoint = 12;
                        break;
                    case 23:
                        upPoint = 13;
                        break;
                    case 24:
                        upPoint = 14;
                        break;
                    case 25:
                        upPoint = 15;
                        break;
                    case 26:
                        upPoint = 16;
                        break;
                    case 27:
                        upPoint = 17;
                        break;
                    case 28:
                        upPoint = 19;
                        break;
                    case 29:
                        upPoint = 21;
                        break;

                    case 30:
                        upPoint = 25;
                        break;
                    case 31:
                        upPoint = 28;
                        break;
                    case 32:
                        upPoint = 31;
                        break;
                    case 33:
                        upPoint = 34;
                        break;
                    case 34:
                        upPoint = 37;
                        break;
                    case 35:
                        upPoint = 40;
                        break;
                    case 36:
                        upPoint = 43;
                        break;
                    case 37:
                        upPoint = 47;
                        break;
                    case 38:
                        upPoint = 51;
                        break;
                    case 39:
                        upPoint = 55;
                        break;

                    case 40:
                        upPoint = 61;
                        break;
                    case 41:
                        upPoint = 65;
                        break;
                    case 42:
                        upPoint = 69;
                        break;
                    case 43:
                        upPoint = 73;
                        break;
                    case 44:
                        upPoint = 77;
                        break;
                    case 45:
                        upPoint = 81;
                        break;
                    case 46:
                        upPoint = 86;
                        break;
                    case 47:
                        upPoint = 91;
                        break;
                    case 48:
                        upPoint = 96;
                        break;
                    case 49:
                        upPoint = 101;
                        break;

                    case 50:
                        upPoint = 109;
                        break;
                    case 51:
                        upPoint = 115;
                        break;
                    case 52:
                        upPoint = 121;
                        break;
                    case 53:
                        upPoint = 127;
                        break;
                    case 54:
                        upPoint = 133;
                        break;
                    case 55:
                        upPoint = 140;
                        break;
                    case 56:
                        upPoint = 147;
                        break;
                    case 57:
                        upPoint = 154;
                        break;
                    case 58:
                        upPoint = 162;
                        break;
                    case 59:
                        upPoint = 170;
                        break;

                    case 60:
                        upPoint = 180;
                        break;
                    case 61:
                        upPoint = 188;
                        break;
                    case 62:
                        upPoint = 196;
                        break;
                    case 63:
                        upPoint = 204;
                        break;
                    case 64:
                        upPoint = 212;
                        break;
                    case 65:
                        upPoint = 221;
                        break;
                    case 66:
                        upPoint = 230;
                        break;
                    case 67:
                        upPoint = 239;
                        break;
                    case 68:
                        upPoint = 248;
                        break;
                    case 69:
                        upPoint = 258;
                        break;
                }

                return upPoint;
            }
        }

        public int LevelUpLifeTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 20;
                        break;
                    case 2:
                        upPoint = 20;
                        break;
                    case 3:
                        upPoint = 20;
                        break;
                    case 4:
                        upPoint = 20;
                        break;
                    case 5:
                        upPoint = 20;
                        break;
                    case 6:
                        upPoint = 20;
                        break;
                    case 7:
                        upPoint = 20;
                        break;
                    case 8:
                        upPoint = 20;
                        break;
                    case 9:
                        upPoint = 20;
                        break;
                    case 10:
                        upPoint = 20;
                        break;
                    case 11:
                        upPoint = 20;
                        break;
                    case 12:
                        upPoint = 20;
                        break;
                    case 13:
                        upPoint = 20;
                        break;
                    case 14:
                        upPoint = 20;
                        break;
                    case 15:
                        upPoint = 20;
                        break;
                    case 16:
                        upPoint = 20;
                        break;
                    case 17:
                        upPoint = 20;
                        break;
                    case 18:
                        upPoint = 20;
                        break;
                    case 19:
                        upPoint = 20;
                        break;

                    case 20:
                        upPoint = 30;
                        break;
                    case 21:
                        upPoint = 40;
                        break;
                    case 22:
                        upPoint = 50;
                        break;
                    case 23:
                        upPoint = 60;
                        break;
                    case 24:
                        upPoint = 70;
                        break;
                    case 25:
                        upPoint = 80;
                        break;
                    case 26:
                        upPoint = 90;
                        break;
                    case 27:
                        upPoint = 100;
                        break;
                    case 28:
                        upPoint = 110;
                        break;
                    case 29:
                        upPoint = 120;
                        break;

                    case 30:
                        upPoint = 140;
                        break;
                    case 31:
                        upPoint = 160;
                        break;
                    case 32:
                        upPoint = 180;
                        break;
                    case 33:
                        upPoint = 200;
                        break;
                    case 34:
                        upPoint = 220;
                        break;
                    case 35:
                        upPoint = 240;
                        break;
                    case 36:
                        upPoint = 260;
                        break;
                    case 37:
                        upPoint = 280;
                        break;
                    case 38:
                        upPoint = 300;
                        break;
                    case 39:
                        upPoint = 320;
                        break;

                    case 40:
                        upPoint = 360;
                        break;
                    case 41:
                        upPoint = 400;
                        break;
                    case 42:
                        upPoint = 440;
                        break;
                    case 43:
                        upPoint = 480;
                        break;
                    case 44:
                        upPoint = 520;
                        break;
                    case 45:
                        upPoint = 560;
                        break;
                    case 46:
                        upPoint = 600;
                        break;
                    case 47:
                        upPoint = 640;
                        break;
                    case 48:
                        upPoint = 680;
                        break;
                    case 49:
                        upPoint = 720;
                        break;

                    case 50:
                        upPoint = 800;
                        break;
                    case 51:
                        upPoint = 880;
                        break;
                    case 52:
                        upPoint = 960;
                        break;
                    case 53:
                        upPoint = 1040;
                        break;
                    case 54:
                        upPoint = 1120;
                        break;
                    case 55:
                        upPoint = 1200;
                        break;
                    case 56:
                        upPoint = 1280;
                        break;
                    case 57:
                        upPoint = 1360;
                        break;
                    case 58:
                        upPoint = 1440;
                        break;
                    case 59:
                        upPoint = 1520;
                        break;

                    case 60:
                        upPoint = 1670;
                        break;
                    case 61:
                        upPoint = 1820;
                        break;
                    case 62:
                        upPoint = 1980;
                        break;
                    case 63:
                        upPoint = 2140;
                        break;
                    case 64:
                        upPoint = 2320;
                        break;
                    case 65:
                        upPoint = 2500;
                        break;
                    case 66:
                        upPoint = 2700;
                        break;
                    case 67:
                        upPoint = 2900;
                        break;
                    case 68:
                        upPoint = 3130;
                        break;
                    case 69:
                        upPoint = 3360;
                        break;
                }

                return upPoint;
            }
        }

        public int LevelUpManaTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 0;
                        break;
                    case 2:
                        upPoint = 15;
                        break;
                    case 3:
                        upPoint = 15;
                        break;
                    case 4:
                        upPoint = 15;
                        break;
                    case 5:
                        upPoint = 15;
                        break;
                    case 6:
                        upPoint = 15;
                        break;
                    case 7:
                        upPoint = 15;
                        break;
                    case 8:
                        upPoint = 15;
                        break;
                    case 9:
                        upPoint = 15;
                        break;
                    case 10:
                        upPoint = 15;
                        break;
                    case 11:
                        upPoint = 15;
                        break;
                    case 12:
                        upPoint = 15;
                        break;
                    case 13:
                        upPoint = 15;
                        break;
                    case 14:
                        upPoint = 15;
                        break;
                    case 15:
                        upPoint = 15;
                        break;
                    case 16:
                        upPoint = 15;
                        break;
                    case 17:
                        upPoint = 15;
                        break;
                    case 18:
                        upPoint = 15;
                        break;
                    case 19:
                        upPoint = 15;
                        break;

                    case 20:
                        upPoint = 20;
                        break;
                    case 21:
                        upPoint = 25;
                        break;
                    case 22:
                        upPoint = 30;
                        break;
                    case 23:
                        upPoint = 35;
                        break;
                    case 24:
                        upPoint = 40;
                        break;
                    case 25:
                        upPoint = 45;
                        break;
                    case 26:
                        upPoint = 50;
                        break;
                    case 27:
                        upPoint = 55;
                        break;
                    case 28:
                        upPoint = 60;
                        break;
                    case 29:
                        upPoint = 65;
                        break;

                    case 30:
                        upPoint = 80;
                        break;
                    case 31:
                        upPoint = 95;
                        break;
                    case 32:
                        upPoint = 110;
                        break;
                    case 33:
                        upPoint = 125;
                        break;
                    case 34:
                        upPoint = 140;
                        break;
                    case 35:
                        upPoint = 155;
                        break;
                    case 36:
                        upPoint = 170;
                        break;
                    case 37:
                        upPoint = 185;
                        break;
                    case 38:
                        upPoint = 200;
                        break;
                    case 39:
                        upPoint = 215;
                        break;

                    case 40:
                        upPoint = 245;
                        break;
                    case 41:
                        upPoint = 275;
                        break;
                    case 42:
                        upPoint = 305;
                        break;
                    case 43:
                        upPoint = 335;
                        break;
                    case 44:
                        upPoint = 365;
                        break;
                    case 45:
                        upPoint = 395;
                        break;
                    case 46:
                        upPoint = 425;
                        break;
                    case 47:
                        upPoint = 455;
                        break;
                    case 48:
                        upPoint = 485;
                        break;
                    case 49:
                        upPoint = 515;
                        break;

                    case 50:
                        upPoint = 565;
                        break;
                    case 51:
                        upPoint = 615;
                        break;
                    case 52:
                        upPoint = 665;
                        break;
                    case 53:
                        upPoint = 715;
                        break;
                    case 54:
                        upPoint = 765;
                        break;
                    case 55:
                        upPoint = 815;
                        break;
                    case 56:
                        upPoint = 865;
                        break;
                    case 57:
                        upPoint = 915;
                        break;
                    case 58:
                        upPoint = 965;
                        break;
                    case 59:
                        upPoint = 1015;
                        break;

                    case 60:
                        upPoint = 1105;
                        break;
                    case 61:
                        upPoint = 1195;
                        break;
                    case 62:
                        upPoint = 1295;
                        break;
                    case 63:
                        upPoint = 1395;
                        break;
                    case 64:
                        upPoint = 1510;
                        break;
                    case 65:
                        upPoint = 1625;
                        break;
                    case 66:
                        upPoint = 1760;
                        break;
                    case 67:
                        upPoint = 1895;
                        break;
                    case 68:
                        upPoint = 2065;
                        break;
                    case 69:
                        upPoint = 2240;
                        break;
                }

                return upPoint;
            }
        }

        public int CurrentEternalFateRingValue
        {
            get { return currentEternalFateRingValue; }
            set { if (value <= 10) { currentEternalFateRingValue = value; } }
        }
        protected int currentEternalFateRing;
        public int CurrentEternalFateRing
        {
            get { return this.currentEternalFateRing; }
            set { this.currentEternalFateRing = value; }
        }

        public bool CheckResistStun
        {
            get
            {
                if (this.battleResistStun) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistStun)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistStun)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistStun)) return true;
                if ((this.Accessory != null) && (Accessory.ResistStun)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistStun)) return true;
                return false;
            }
        }
        public bool CheckResistSilence
        {
            get
            {
                if (this.battleResistSilence) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSilence)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSilence)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSilence)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSilence)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSilence)) return true;
                return false;
            }
        }
        public bool CheckResistPoison
        {
            get
            {
                if (this.battleResistPoison) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistPoison)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistPoison)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistPoison)) return true;
                if ((this.Accessory != null) && (Accessory.ResistPoison)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistPoison)) return true;
                return false;
            }
        }
        public bool CheckResistTemptation
        {
            get
            {
                if (this.battleResistTemptation) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistTemptation)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistTemptation)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistTemptation)) return true;
                if ((this.Accessory != null) && (Accessory.ResistTemptation)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistTemptation)) return true;
                return false;
            }
        }
        public bool CheckResistFrozen
        {
            get
            {
                if (this.battleResistFrozen) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistFrozen)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistFrozen)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistFrozen)) return true;
                if ((this.Accessory != null) && (Accessory.ResistFrozen)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistFrozen)) return true;
                return false;
            }
        }
        public bool CheckResistParalyze
        {
            get
            {
                if (this.battleResistParalyze) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistParalyze)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistParalyze)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistParalyze)) return true;
                if ((this.Accessory != null) && (Accessory.ResistParalyze)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistParalyze)) return true;
                return false;
            }
        }
        public bool CheckResistSlow
        {
            get
            {
                if (this.battleResistSlow) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSlow)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSlow)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSlow)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSlow)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSlow)) return true;
                return false;
            }
        }
        public bool CheckResistBlind
        {
            get
            {
                if (this.battleResistBlind) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistBlind)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistBlind)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistBlind)) return true;
                if ((this.Accessory != null) && (Accessory.ResistBlind)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistBlind)) return true;
                return false;
            }
        }
        public bool CheckResistSlip
        {
            get
            {
                if (this.battleResistSlip) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                if (this.CurrentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSlip)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSlip)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSlip)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSlip)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSlip)) return true;
                return false;
            }
        }
        public bool CheckResistNoResurrection
        {
            get
            {
                if (this.battleResistNoResurrection) return true;
                if (this.CurrentSagePotionMini > 0) return true;
                //if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistNoResurrection)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistNoResurrection)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistNoResurrection)) return true;
                if ((this.Accessory != null) && (Accessory.ResistNoResurrection)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistNoResurrection)) return true;
                return false;
            }
        }


        /// <summary>
        /// 正のスキル効果をリムーブ
        /// </summary>
        public void RemoveBuffSkill()
        {
            // 基本スキル
            this.RemoveAntiStun();
            this.RemoveStanceOfDeath();
            this.RemoveTruthVision();
            this.RemovePainfulInsanity(); // ダメージ系統だが対象は自分自身なのでUP扱い
            this.RemoveVoidExtraction();
            this.RemoveNothingOfNothingness();
            // 複合スキル
            this.RemoveRisingAura();
            this.RemoveAscensionAura();
            this.RemoveReflexSpirit();
            this.RemoveTrustSilence();
            this.RemoveStanceOfMystic();
            this.RemoveNourishSense();
        }

        /// <summary>
        /// 負のスキル効果をリムーブ
        /// </summary>
        public void RemoveDebuffSkill()
        {
            // 基本スキル
            // 複合スキル
            this.RemoveOnslaughtHit();
            this.RemoveConcussiveHit();
            this.RemoveImpulseHit();
        }

        /// <summary>
        /// 正の魔法効果をリムーブ
        /// </summary>
        public void RemoveBuffSpell()
        {
            // 基本スペル
            this.RemoveProtection();
            this.RemoveSaintPower();
            this.RemoveAbsorbWater();
            this.RemoveShadowPact();
            this.RemoveEternalPresence();
            this.RemoveBloodyVengeance();
            this.RemoveHeatBoost();
            this.RemovePromisedKnowledge();
            this.RemoveRiseOfImage();
            this.RemoveWordOfLife();
            this.RemoveFlameAura();
            // 複合スペル
            this.RemovePsychicTrance();
            this.RemoveBlindJustice();
            this.RemoveSkyShield();
            this.RemoveEverDroplet();
            this.RemoveHolyBreaker();
            this.RemoveExaltedField();
            this.RemoveFrozenAura();
            this.RemovePhantasmalWind();
            this.RemoveRedDragonWill();
            this.RemoveStaticBarrier();
            this.RemoveBlueDragonWill();
            this.RemoveSeventhMagic();
            this.RemoveParadoxImage();
        }

        /// <summary>
        /// 負の魔法効果をリムーブ
        /// </summary>
        public void RemoveDebuffSpell()
        {
            // 基本スペル
            this.RemoveDamnation();
            this.RemoveAbsoluteZero();
            // 複合スペル
            this.RemoveFlashBlaze();
            this.RemoveStarLightning();
            this.RemoveBlackFire();
            this.RemoveBlazingField();
            this.RemoveDemonicIgnite();
            this.RemoveWordOfMalice();
            this.RemoveDarkenField();
            this.RemoveChillBurn();
            this.RemoveEnrageBlast();
            //this.RemoveSigilOfHomura();
            this.RemoveImmolate();
            //this.RemoveAusterityMatrix();
            this.RemoveVanishWave();
            this.RemoveVortexField();
        }

        /// <summary>
        /// 正のパラメタ効果をリムーブ
        /// </summary>
        public void RemoveBuffParam()
        {
            this.RemovePhysicalAttackUp();
            this.RemovePhysicalDefenseUp();
            this.RemoveMagicAttackUp();
            this.RemoveMagicDefenseUp();
            this.RemoveSpeedUp();
            this.RemoveReactionUp();
            this.RemovePotentialUp();
        }


        /// <summary>
        /// 負のパラメタ効果をリムーブ
        /// </summary>
        public void RemoveDebuffParam()
        {
            this.RemovePhysicalAttackDown();
            this.RemovePhysicalDefenseDown();
            this.RemoveMagicAttackDown();
            this.RemoveMagicDefenseDown();
            this.RemoveSpeedDown();
            this.RemoveReactionDown();
            this.RemovePotentialDown();
        }

        /// <summary>
        /// 正の影響効果をリムーブ
        /// </summary>
        public void RemoveBuffEffect()
        {
            this.RemoveBlinded();
            this.RemoveSpeedBoost();
            this.RemoveChargeCount();
            this.RemovePhysicalChargeCount();
        }

        /// <summary>
        /// 負の影響効果をリムーブ
        /// </summary>
        public void RemoveDebuffEffect()
        {
            this.RemovePreStunning();
            this.RemoveStun();
            this.RemoveSilence();
            this.RemovePoison();
            this.RemoveTemptation();
            this.RemoveFrozen();
            this.RemoveParalyze();
            this.RemoveNoResurrection();
            this.RemoveSlow();
            this.RemoveBlind();
            this.RemoveSlip();
        }

        private void UpdateBattleText(string text)
        {
            this.TextBattleMessage.text = this.TextBattleMessage.text.Insert(0, text);
        }
        /// <summary>
        /// 力パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpStrength(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.FirstName + "は【力】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentStrengthUp = turn;
            this.CurrentStrengthUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStrengthUp, Database.BaseResourceFolder + Database.BUFF_STRENGTH_UP, turn);
        }

        /// <summary>
        /// 技パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpAgility(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.FirstName + "は【技】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentAgilityUp = turn;
            this.CurrentAgilityUpValue = (int)effectValue;
            this.ActivateBuff(this.pbAgilityUp, Database.BaseResourceFolder + Database.BUFF_AGILITY_UP, turn);
        }

        /// <summary>
        /// 知パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpIntelligence(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.FirstName + "は【知】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentIntelligenceUp = turn;
            this.CurrentIntelligenceUpValue = (int)effectValue;
            this.ActivateBuff(this.pbIntelligenceUp, Database.BaseResourceFolder + Database.BUFF_INTELLIGENCE_UP, turn);
        }

        /// <summary>
        /// 体パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpStamina(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.FirstName + "は【体】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentStaminaUp = turn;
            this.CurrentStaminaUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStaminaUp, Database.BaseResourceFolder + Database.BUFF_STAMINA_UP, turn);
        }

        /// <summary>
        /// 心パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpMind(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.FirstName + "は【心】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentMindUp = turn;
            this.CurrentMindUpValue = (int)effectValue;
            this.ActivateBuff(this.pbMindUp, Database.BaseResourceFolder + Database.BUFF_MIND_UP, turn);
        }

        public void BuffUpAmplifyPhysicalAttack(double effectValue, int turn = 999)
        {
            this.AmplifyPhysicalAttack = effectValue;
            this.ActivateBuff(this.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, turn);
        }

        public void BuffUpAmplifyPhysicalDefence(double effectValue, int turn = 999)
        {
            this.AmplifyPhysicalDefense = effectValue;
            this.ActivateBuff(this.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, turn);
        }

        public void BuffUpAmplifyMagicAttack(double effectValue, int turn = 999)
        {
            this.AmplifyMagicAttack = effectValue;
            this.ActivateBuff(this.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, turn);
        }

        public void BuffUpAmplifyMagicDefense(double effectValue, int turn = 999)
        {
            this.AmplifyMagicDefense = effectValue;
            this.ActivateBuff(this.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, turn);
        }

        public void BuffUpAmplifyBattleSpeed(double effectValue, int turn = 999)
        {
            this.AmplifyBattleSpeed = effectValue;
            this.ActivateBuff(this.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, turn);
        }

        public void BuffUpAmplifyBattleResponse(double effectValue, int turn = 999)
        {
            this.AmplifyBattleResponse = effectValue;
            this.ActivateBuff(this.pbReactionUp, Database.BaseResourceFolder + Database.BUFF_REACTION_UP, turn);
        }

        public void BuffUpAmplifyPotential(double effectValue, int turn = 999)
        {
            this.AmplifyPotential = effectValue;
            this.ActivateBuff(this.pbPotentialUp, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_UP, turn);
        }
        // s 後編追加（以下、前編とメソッドが、かぶらないようにしたい。もしかぶると、Debuffメソッドで例外エラーとなるためそれを避けたい）
        public void RemoveProtection()
        {
            this.CurrentProtection = 0;
            this.DeBuff(this.pbProtection);
        }
        public void RemoveSaintPower()
        {
            this.CurrentSaintPower = 0;
            this.DeBuff(this.pbSaintPower);
        }
        public void RemoveGlory()
        {
            this.CurrentGlory = 0;
            this.DeBuff(this.pbGlory);
        }
        public void RemoveShadowPact()
        {
            this.CurrentShadowPact = 0;
            this.DeBuff(this.pbShadowPact);
        }
        public void RemoveBlackContract()
        {
            this.CurrentBlackContract = 0;
            this.DeBuff(this.pbBlackContract);
        }
        public void RemoveDamnation()
        {
            this.CurrentDamnation = 0;
            this.DeBuff(this.pbDamnation);
        }
        public void RemoveFlameAura()
        {
            this.CurrentFlameAura = 0;
            this.DeBuff(this.pbFlameAura);
        }
        public void RemoveImmortalRave()
        {
            this.CurrentImmortalRave = 0;
            this.DeBuff(this.pbImmortalRave);
        }
        public void RemoveAbsorbWater()
        {
            this.CurrentAbsorbWater = 0;
            this.DeBuff(this.pbAbsorbWater);
        }
        public void RemoveMirrorImage()
        {
            this.CurrentMirrorImage = 0;
            this.DeBuff(this.pbMirrorImage);
        }
        public void RemoveAbsoluteZero()
        {
            this.CurrentAbsoluteZero = 0;
            this.DeBuff(this.pbAbsoluteZero);
        }
        public void RemoveGaleWind()
        {
            this.CurrentGaleWind = 0;
            this.DeBuff(this.pbGaleWind);
        }
        public void RemoveWordOfLife()
        {
            this.CurrentWordOfLife = 0;
            this.DeBuff(this.pbWordOfLife);
        }
        public void RemoveWordOfFortune()
        {
            this.CurrentWordOfFortune = 0;
            this.DeBuff(this.pbWordOfFortune);
        }
        public void RemoveAetherDrive()
        {
            this.CurrentAetherDrive = 0;
            this.DeBuff(this.pbAetherDrive);
        }
        public void RemoveEternalPresence()
        {
            this.CurrentEternalPresence = 0;
            this.DeBuff(this.pbEternalPresence);
        }
        public void RemoveDeflection()
        {
            this.CurrentDeflection = 0;
            this.DeBuff(this.pbDeflection);
        }
        public void RemoveOneImmunity()
        {
            this.CurrentOneImmunity = 0;
            this.DeBuff(this.pbOneImmunity);
        }
        public void RemoveTimeStop()
        {
            this.CurrentTimeStop = 0;
            this.CurrentTimeStopImmediate = false;
            this.DeBuff(this.pbTimeStop);
        }
        public void RemoveBloodyVengeance()
        {
            this.CurrentBloodyVengeance = 0;
            this.BuffStrength_BloodyVengeance = 0;
            this.DeBuff(this.pbBloodyVengeance);
        }
        public void RemoveHeatBoost()
        {
            this.CurrentHeatBoost = 0;
            this.BuffAgility_HeatBoost = 0;
            this.DeBuff(this.pbHeatBoost);
        }
        public void RemovePromisedKnowledge()
        {
            this.CurrentPromisedKnowledge = 0;
            this.BuffIntelligence_PromisedKnowledge = 0;
            this.DeBuff(this.pbPromisedKnowledge);
        }
        public void RemoveRiseOfImage()
        {
            this.CurrentRiseOfImage = 0;
            this.BuffMind_RiseOfImage = 0;
            this.DeBuff(this.pbRiseOfImage);
        }
        public void RemovePsychicTrance()
        {
            this.CurrentPsychicTrance = 0;
            this.DeBuff(this.pbPsychicTrance);
        }
        public void RemoveBlindJustice()
        {
            this.CurrentBlindJustice = 0;
            this.DeBuff(this.pbBlindJustice);
        }
        public void RemoveTranscendentWish()
        {
            this.CurrentTranscendentWish = 0;
            this.BuffStrength_TranscendentWish = 0;
            this.BuffAgility_TranscendentWish = 0;
            this.BuffIntelligence_TranscendentWish = 0;
            this.BuffStamina_TranscendentWish = 0;
            this.BuffMind_TranscendentWish = 0;
            this.DeBuff(this.pbTranscendentWish);
        }
        public void RemoveFlashBlaze()
        {
            this.CurrentFlashBlazeCount = 0;
            this.DeBuff(this.pbFlashBlaze);
        }
        public void RemoveSkyShield()
        {
            this.CurrentSkyShield = 0;
            this.CurrentSkyShieldValue = 0;
            this.DeBuff(this.pbSkyShield);
        }
        public void RemoveEverDroplet()
        {
            this.CurrentEverDroplet = 0;
            this.DeBuff(this.pbEverDroplet);
        }
        public void RemoveHolyBreaker()
        {
            this.CurrentHolyBreaker = 0;
            this.DeBuff(this.pbHolyBreaker);
        }
        public void RemoveExaltedField()
        {
            this.CurrentExaltedField = 0;
            this.DeBuff(this.pbExaltedField);
        }
        public void RemoveHymnContract()
        {
            this.CurrentHymnContract = 0;
            this.DeBuff(this.pbHymnContract);
        }
        public void RemoveStarLightning()
        {
            this.CurrentStarLightning = 0;
            this.DeBuff(this.pbStarLightning);
        }
        public void RemoveEndlessAnthem()
        {
            //this.CurrentEndlessAnthem = 0;
            //this.DeBuff(this.pbEndlessAnthem);
        }
        public void RemoveBlackFire()
        {
            this.CurrentBlackFire = 0;
            this.DeBuff(this.pbBlackFire);
        }
        public void RemoveBlazingField()
        {
            this.CurrentBlazingField = 0;
            this.CurrentBlazingFieldFactor = 0;
            this.DeBuff(this.pbBlazingField);
        }
        public void RemoveDemonicIgnite()
        {
            //this.CurrentDemonicIgnite = 0;
            //this.DeBuff(this.pbDemonicIgnite);
        }
        public void RemoveWordOfMalice()
        {
            this.CurrentWordOfMalice = 0;
            this.DeBuff(this.pbWordOfMalice);
        }
        public void RemoveSinFortune()
        {
            this.CurrentSinFortune = 0;
            this.DeBuff(this.pbSinFortune);
        }
        public void RemoveDarkenField()
        {
            this.CurrentDarkenField = 0;
            this.DeBuff(this.pbDarkenField);
        }
        public void RemoveEclipseEnd()
        {
            this.CurrentEclipseEnd = 0;
            this.DeBuff(this.pbEclipseEnd);
        }
        public void RemoveFrozenAura()
        {
            this.CurrentFrozenAura = 0;
            this.DeBuff(this.pbFrozenAura);
        }
        public void RemoveChillBurn()
        {
            //this.CurrentChillBurn = 0;
            //this.DeBuff(this.pbChillBurn);
        }
        public void RemoveEnrageBlast()
        {
            this.CurrentEnrageBlast = 0;
            this.DeBuff(this.pbEnrageBlast);
        }
        //public void RemoveSigilOfHomura()
        //{
        //    this.CurrentSigilOfHomura = 0;
        //    this.DeBuff(this.pbSigilOfHomura);
        //}
        public void RemoveImmolate()
        {
            this.CurrentImmolate = 0;
            this.DeBuff(this.pbImmolate);
        }
        public void RemovePhantasmalWind()
        {
            this.CurrentPhantasmalWind = 0;
            this.DeBuff(this.pbPhantasmalWind);
        }
        public void RemoveRedDragonWill()
        {
            this.CurrentRedDragonWill = 0;
            this.DeBuff(this.pbRedDragonWill);
        }
        public void RemoveStaticBarrier()
        {
            this.CurrentStaticBarrier = 0;
            this.CurrentStaticBarrierValue = 0;
            this.DeBuff(this.pbStaticBarrier);
        }
        public void RemoveAusterityMatrix()
        {
            this.CurrentAusterityMatrix = 0;
            this.DeBuff(this.pbAusterityMatrix);
        }
        public void RemoveVanishWave()
        {
            //this.CurrentVanishWave = 0;
            //this.DeBuff(this.pbVanishWave);
        }
        public void RemoveVortexField()
        {
            //this.CurrentVortexField = 0;
            //this.DeBuff(this.pbVortexField);
        }
        public void RemoveBlueDragonWill()
        {
            this.CurrentBlueDragonWill = 0;
            this.DeBuff(this.pbBlueDragonWill);
        }
        public void RemoveSeventhMagic()
        {
            this.CurrentSeventhMagic = 0;
            this.DeBuff(this.pbSeventhMagic);
        }
        public void RemoveParadoxImage()
        {
            this.CurrentParadoxImage = 0;
            this.DeBuff(this.pbParadoxImage);
        }

        public void RemoveAntiStun()
        {
            this.CurrentAntiStun = 0;
            this.DeBuff(this.pbAntiStun);
        }
        public void RemoveStanceOfDeath()
        {
            this.CurrentStanceOfDeath = 0;
            this.DeBuff(this.pbStanceOfDeath);
        }
        public void RemoveStanceOfFlow()
        {
            this.CurrentStanceOfFlow = 0;
            this.DeBuff(this.pbStanceOfFlow);
        }
        public void RemoveTruthVision()
        {
            this.CurrentTruthVision = 0;
            this.DeBuff(this.pbTruthVision);
        }
        public void RemoveHighEmotionality()
        {
            this.CurrentHighEmotionality = 0;
            this.BuffStrength_HighEmotionality = 0;
            this.BuffAgility_HighEmotionality = 0;
            this.BuffIntelligence_HighEmotionality = 0;
            this.BuffStamina_HighEmotionality = 0;
            this.BuffMind_HighEmotionality = 0;
            this.DeBuff(this.pbHighEmotionality);
        }
        // s 後編追加
        public void RemoveCounterAttack()
        {
            this.CurrentCounterAttack = 0;
            this.DeBuff(this.pbCounterAttack);
        }
        public void RemoveStanceOfEyes()
        {
            this.CurrentStanceOfEyes = 0;
            this.DeBuff(this.pbStanceOfEyes);
        }
        public void RemoveNegate()
        {
            this.CurrentNegate = 0;
            this.DeBuff(this.pbNegate);
        }
        public void RemoveStanceOfStanding()
        {
            this.CurrentStanceOfStanding = 0;
            this.DeBuff(this.pbStanceOfStanding);
        }
        // e 後編追加
        public void RemovePainfulInsanity()
        {
            this.CurrentPainfulInsanity = 0;
            this.DeBuff(this.pbPainfulInsanity);
        }
        public void RemoveVoidExtraction()
        {
            this.CurrentVoidExtraction = 0;
            this.BuffStrength_VoidExtraction = 0;
            this.BuffAgility_VoidExtraction = 0;
            this.BuffIntelligence_VoidExtraction = 0;
            this.BuffStamina_VoidExtraction = 0;
            this.BuffMind_VoidExtraction = 0;
            this.DeBuff(this.pbVoidExtraction);
        }
        public void RemoveNothingOfNothingness()
        {
            this.CurrentNothingOfNothingness = 0;
            this.DeBuff(this.pbNothingOfNothingness);
        }
        public void RemoveStanceOfDouble()
        {
            this.CurrentStanceOfDouble = 0;
            this.DeBuff(this.pbStanceOfDouble);
        }
        public void RemoveSwiftStep()
        {
            this.CurrentSwiftStep = 0;
            this.DeBuff(this.pbSwiftStep);
        }
        public void RemoveVigorSense()
        {
            this.CurrentVigorSense = 0;
            this.DeBuff(this.pbVigorSense);
        }
        public void RemoveRisingAura()
        {
            this.CurrentRisingAura = 0;
            this.DeBuff(this.pbRisingAura);
        }
        public void RemoveOnslaughtHit()
        {
            this.CurrentOnslaughtHit = 0;
            this.CurrentOnslaughtHitValue = 0;
            this.DeBuff(this.pbOnslaughtHit);
        }
        public void RemoveSmoothingMove()
        {
            this.CurrentSmoothingMove = 0;
            this.DeBuff(this.pbSmoothingMove);
        }
        public void RemoveAscensionAura()
        {
            this.CurrentAscensionAura = 0;
            this.DeBuff(this.pbAscensionAura);
        }
        public void RemoveFutureVision()
        {
            this.CurrentFutureVision = 0;
            this.DeBuff(this.pbFutureVision);
        }
        public void RemoveReflexSpirit()
        {
            this.CurrentReflexSpirit = 0;
            this.DeBuff(this.pbReflexSpirit);
        }
        public void RemoveConcussiveHit()
        {
            this.CurrentConcussiveHit = 0;
            this.CurrentConcussiveHitValue = 0;
            this.DeBuff(this.pbConcussiveHit);
        }
        public void RemoveTrustSilence()
        {
            this.CurrentTrustSilence = 0;
            this.DeBuff(this.pbTrustSilence);
        }
        public void RemoveStanceOfMystic()
        {
            this.CurrentStanceOfMystic = 0;
            this.CurrentStanceOfMysticValue = 0;
            this.DeBuff(this.pbStanceOfMystic);
        }
        public void RemoveNourishSense()
        {
            this.CurrentNourishSense = 0;
            this.DeBuff(this.pbNourishSense);
        }
        public void RemoveImpulseHit()
        {
            this.CurrentImpulseHit = 0;
            this.CurrentImpulseHitValue = 0;
            this.DeBuff(this.pbImpulseHit);
        }
        public void RemoveOneAuthority()
        {
            this.CurrentOneAuthority = 0;
            this.DeBuff(this.pbOneAuthority);
        }

        public void RemovePhysicalAttackUp()
        {
            CurrentPhysicalAttackUp = 0;
            CurrentPhysicalAttackUpValue = 0;
            DeBuff(pbPhysicalAttackUp);
        }
        public void RemovePhysicalAttackDown()
        {
            CurrentPhysicalAttackDown = 0;
            CurrentPhysicalAttackDownValue = 0;
            DeBuff(pbPhysicalAttackDown);
        }
        public void RemovePhysicalDefenseUp()
        {
            CurrentPhysicalDefenseUp = 0;
            CurrentPhysicalDefenseUpValue = 0;
            DeBuff(pbPhysicalDefenseUp);
        }
        public void RemovePhysicalDefenseDown()
        {
            CurrentPhysicalDefenseDown = 0;
            CurrentPhysicalDefenseDownValue = 0;
            DeBuff(pbPhysicalDefenseDown);
        }

        public void RemoveMagicAttackUp()
        {
            CurrentMagicAttackUp = 0;
            CurrentMagicAttackUpValue = 0;
            DeBuff(pbMagicAttackUp);
        }
        public void RemoveMagicAttackDown()
        {
            CurrentMagicAttackDown = 0;
            CurrentMagicAttackDownValue = 0;
            DeBuff(pbMagicAttackDown);
        }

        public void RemoveMagicDefenseUp()
        {
            CurrentMagicDefenseUp = 0;
            CurrentMagicDefenseUpValue = 0;
            DeBuff(pbMagicDefenseUp);
        }
        public void RemoveMagicDefenseDown()
        {
            CurrentMagicDefenseDown = 0;
            CurrentMagicDefenseDownValue = 0;
            DeBuff(pbMagicDefenseDown);
        }

        public void RemoveSpeedUp()
        {
            CurrentSpeedUp = 0;
            CurrentSpeedUpValue = 0;
            DeBuff(pbSpeedUp);
        }
        public void RemoveSpeedDown()
        {
            CurrentSpeedDown = 0;
            CurrentSpeedDownValue = 0;
            DeBuff(pbSpeedDown);
        }

        public void RemoveReactionUp()
        {
            CurrentReactionUp = 0;
            CurrentReactionUpValue = 0;
            DeBuff(pbReactionUp);
        }
        public void RemoveReactionDown()
        {
            CurrentReactionDown = 0;
            CurrentReactionDownValue = 0;
            DeBuff(pbReactionDown);
        }

        public void RemovePotentialUp()
        {
            CurrentPotentialUp = 0;
            CurrentPotentialUpValue = 0;
            DeBuff(pbPotentialUp);
        }
        public void RemovePotentialDown()
        {
            CurrentPotentialDown = 0;
            CurrentPotentialDownValue = 0;
            DeBuff(pbPotentialDown);
        }

        public void RemoveStrengthUp()
        {
            CurrentStrengthUp = 0;
            CurrentStrengthUpValue = 0;
            DeBuff(pbStrengthUp);
        }

        public void RemoveAgilityUp()
        {
            CurrentAgilityUp = 0;
            CurrentAgilityUpValue = 0;
            DeBuff(pbAgilityUp);
        }

        public void RemoveIntelligenceUp()
        {
            CurrentIntelligenceUp = 0;
            CurrentIntelligenceUpValue = 0;
            DeBuff(pbIntelligenceUp);
        }

        public void RemoveStaminaUp()
        {
            CurrentStaminaUp = 0;
            CurrentStaminaUpValue = 0;
            DeBuff(pbStaminaUp);
        }

        public void RemoveMindUp()
        {
            CurrentMindUp = 0;
            CurrentMindUpValue = 0;
            DeBuff(pbMindUp);
        }

        public void RemoveLightUp()
        {
            CurrentLightUp = 0;
            CurrentLightUpValue = 0;
            DeBuff(pbLightUp);
        }
        public void RemoveLightDown()
        {
            CurrentLightDown = 0;
            CurrentLightDownValue = 0;
            DeBuff(pbLightDown);
        }
        public void RemoveShadowUp()
        {
            CurrentShadowUp = 0;
            CurrentShadowUpValue = 0;
            DeBuff(pbShadowUp);
        }
        public void RemoveShadowDown()
        {
            CurrentShadowDown = 0;
            CurrentShadowDownValue = 0;
            DeBuff(pbShadowDown);
        }
        public void RemoveFireUp()
        {
            CurrentFireUp = 0;
            CurrentFireUpValue = 0;
            DeBuff(pbFireUp);
        }
        public void RemoveFireDown()
        {
            CurrentFireDown = 0;
            CurrentFireDownValue = 0;
            DeBuff(pbFireDown);
        }
        public void RemoveIceUp()
        {
            CurrentIceUp = 0;
            CurrentIceUpValue = 0;
            DeBuff(pbIceUp);
        }
        public void RemoveIceDown()
        {
            CurrentIceDown = 0;
            CurrentIceDownValue = 0;
            DeBuff(pbIceDown);
        }
        public void RemoveForceUp()
        {
            CurrentForceUp = 0;
            CurrentForceUpValue = 0;
            DeBuff(pbForceUp);
        }
        public void RemoveForceDown()
        {
            CurrentForceDown = 0;
            CurrentForceDownValue = 0;
            DeBuff(pbForceDown);
        }
        public void RemoveWillUp()
        {
            CurrentWillUp = 0;
            CurrentWillUpValue = 0;
            DeBuff(pbWillUp);
        }
        public void RemoveWillDown()
        {
            CurrentWillDown = 0;
            CurrentWillDownValue = 0;
            DeBuff(pbWillDown);
        }
        public void RemoveResistLightUp()
        {
            CurrentResistLightUp = 0;
            CurrentResistLightUpValue = 0;
            DeBuff(pbResistLightUp);
        }

        public void RemoveResistShadowUp()
        {
            CurrentResistShadowUp = 0;
            CurrentResistShadowUpValue = 0;
            DeBuff(pbResistShadowUp);
        }

        public void RemoveResistFireUp()
        {
            CurrentResistFireUp = 0;
            CurrentResistFireUpValue = 0;
            DeBuff(pbResistFireUp);
        }

        public void RemoveResistIceUp()
        {
            CurrentResistIceUp = 0;
            CurrentResistIceUpValue = 0;
            DeBuff(pbResistIceUp);
        }

        public void RemoveResistForceUp()
        {
            CurrentResistForceUp = 0;
            CurrentResistForceUpValue = 0;
            DeBuff(pbResistForceUp);
        }

        public void RemoveResistWillUp()
        {
            CurrentResistWillUp = 0;
            CurrentResistWillUpValue = 0;
            DeBuff(pbResistWillUp);
        }

        public void RemoveAfterReviveHalf()
        {
            CurrentAfterReviveHalf = 0;
            DeBuff(pbAfterReviveHalf);
        }
        public void RemoveFireDamage2()
        {
            CurrentFireDamage2 = 0;
            DeBuff(pbFireDamage2);
        }
        public void RemoveBlackMagic()
        {
            CurrentBlackMagic = 0;
            DeBuff(pbBlackMagic);
        }
        public void RemoveChaosDesperate()
        {
            CurrentChaosDesperate = 0;
            CurrentChaosDesperateValue = 0;
            DeBuff(pbChaosDesperate);
        }
        public void RemoveIchinaruHomura()
        {
            CurrentIchinaruHomura = 0;
            DeBuff(pbIchinaruHomura);
        }

        public void RemoveAbyssFire()
        {
            CurrentAbyssFire = 0;
            DeBuff(pbAbyssFire);
        }

        public void RemoveLightAndShadow()
        {
            CurrentLightAndShadow = 0;
            DeBuff(pbLightAndShadow);
        }

        public void RemoveEternalDroplet()
        {
            CurrentEternalDroplet = 0;
            DeBuff(pbEternalDroplet);
        }

        public void RemoveAusterityMatrixOmega()
        {
            CurrentAusterityMatrixOmega = 0;
            DeBuff(pbAusterityMatrixOmega);
        }

        public void RemoveVoiceOfAbyss()
        {
            CurrentVoiceOfAbyss = 0;
            DeBuff(pbVoiceOfAbyss);
        }

        public void RemoveAbyssWill()
        {
            CurrentAbyssWill = 0;
            CurrentAbyssWillValue = 0;
            DeBuff(pbAbyssWill);
        }

        public void RemoveTheAbyssWall()
        {
            CurrentTheAbyssWall = 0;
            DeBuff(pbTheAbyssWall);
        }

        public void RemovePreStunning()
        {
            this.CurrentPreStunning = 0;
            this.DeBuff(this.pbPreStunning);
        }
        public void RemoveStun()
        {
            this.CurrentStunning = 0;
            this.DeBuff(this.pbStun);
        }
        public void RemoveSilence()
        {
            this.CurrentSilence = 0;
            this.DeBuff(this.pbSilence);
        }
        public void RemovePoison()
        {
            this.CurrentPoison = 0;
            this.CurrentPoisonValue = 0;
            this.DeBuff(this.pbPoison);
        }
        public void RemoveTemptation()
        {
            this.CurrentTemptation = 0;
            this.DeBuff(this.pbTemptation);
        }
        public void RemoveFrozen()
        {
            this.CurrentFrozen = 0;
            this.DeBuff(this.pbFrozen);
        }
        public void RemoveParalyze()
        {
            this.CurrentParalyze = 0;
            this.DeBuff(this.pbParalyze);
        }
        public void RemoveNoResurrection()
        {
            this.CurrentNoResurrection = 0;
            this.DeBuff(this.pbNoResurrection);
        }
        public void RemoveSlow()
        {
            this.CurrentSlow = 0;
            this.DeBuff(this.pbSlow);
        }
        public void RemoveBlind()
        {
            this.CurrentBlind = 0;
            this.DeBuff(this.pbBlind);
        }
        public void RemoveSlip()
        {
            this.CurrentSlip = 0;
            this.DeBuff(this.pbSlip);
        }
        public void RemoveNoGainLife()
        {
            this.CurrentNoGainLife = 0;
            this.DeBuff(this.pbNoGainLife);
        }
        public void RemoveBlinded()
        {
            this.CurrentBlinded = 0;
            this.DeBuff(this.pbBlinded);
        }
        public void RemoveSpeedBoost()
        {
            currentSpeedBoost = 0;
        }

        public void RemoveChargeCount()
        {
            currentChargeCount = 0;
        }

        public void RemovePhysicalChargeCount()
        {
            currentPhysicalChargeCount = 0;
        }

        // 武器特有
        public void RemoveFeltus()
        {
            this.CurrentFeltus = 0;
            this.CurrentFeltusValue = 0;
            this.DeBuff(this.pbFeltus);
        }
        public void RemoveJuzaPhantasmal()
        {
            this.CurrentJuzaPhantasmal = 0;
            this.CurrentJuzaPhantasmalValue = 0;
            this.DeBuff(this.pbJuzaPhantasmal);
        }
        public void RemoveEternalFateRing()
        {
            this.CurrentEternalDroplet = 0;
            this.CurrentEternalFateRingValue = 0;
            this.DeBuff(this.pbEternalFateRing);
        }
        public void RemoveLightServant()
        {
            this.CurrentLightServant = 0;
            this.CurrentLightServantValue = 0;
            this.DeBuff(this.pbLightServant);
        }
        public void RemoveShadowServant()
        {
            this.CurrentShadowServant = 0;
            this.CurrentShadowServantValue = 0;
            this.DeBuff(this.pbShadowServant);
        }
        public void RemoveAdilBlueBurn()
        {
            this.CurrentAdilBlueBurn = 0;
            this.CurrentAdilBlueBurnValue = 0;
            this.DeBuff(this.pbAdilBlueBurn);
        }
        public void RemoveMazeCube()
        {
            this.CurrentMazeCube = 0;
            this.CurrentMazeCubeValue = 0;
            this.DeBuff(this.pbMazeCube);
        }
        public void RemoveShadowBible()
        {
            this.CurrentShadowBible = 0;
            this.DeBuff(this.pbShadowBible);
        }
        public void RemoveDetachmentOrb()
        {
            this.CurrentDetachmentOrb = 0;
            this.DeBuff(this.pbDetachmentOrb);
        }
        public void RemoveDevilSummonerTome()
        {
            this.CurrentDevilSummonerTome = 0;
            this.DeBuff(this.pbDevilSummonerTome);
        }
        public void RemoveVoidHymnsonia()
        {
            this.CurrentVoidHymnsonia = 0;
            this.DeBuff(this.pbVoidHymnsonia);
        }

        public void RemoveSagePotionMini()
        {
            this.CurrentSagePotionMini = 0;
            this.DeBuff(this.pbSagePotionMini);
        }
        public void RemoveGenseiTaima()
        {
            this.CurrentGenseiTaima = 0;
            this.DeBuff(this.pbGenseiTaima);
        }
        public void RemoveShiningAether()
        {
            this.CurrentShiningAether = 0;
            this.DeBuff(this.pbShiningAether);
        }
        public void RemoveBlackElixir()
        {
            this.CurrentBlackElixir = 0;
            this.CurrentBlackElixirValue = 0;
            this.DeBuff(this.pbBlackElixir);
        }
        public void RemoveElementalSeal()
        {
            this.CurrentElementalSeal = 0;
            this.DeBuff(this.pbElementalSeal);
        }
        public void RemoveColoressAntidote()
        {
            this.CurrentColoressAntidote = 0;
            this.DeBuff(this.pbColoressAntidote);
        }
        public void RemoveLifeCount()
        {
            this.CurrentLife = 0;
            this.CurrentLifeCount = 0;
            this.DeBuff(this.pbLifeCount);
        }
        public void RemoveChaoticSchema()
        {
            this.CurrentChaoticSchema = 0;
            this.DeBuff(this.pbChaoticSchema);
        }
        // e 後編追加


        public void RecoverStunning()
        {
            CurrentStunning = 0;
            this.DeBuff(this.pbStun);
        }
        public void RecoverParalyze()
        {
            CurrentParalyze = 0;
            this.DeBuff(this.pbParalyze);
        }
        public void RecoverFrozen()
        {
            CurrentFrozen = 0;
            this.DeBuff(this.pbFrozen);
        }
        public void RecoverPoison()
        {
            CurrentPoison = 0;
            currentPoisonValue = 0; // 後編追加
            this.DeBuff(this.pbPoison);
        }
        public void RecoverSlow()
        {
            CurrentSlow = 0;
            this.DeBuff(this.pbSlow);
        }
        public void RecoverBlind()
        {
            CurrentBlind = 0;
            this.DeBuff(this.pbBlind);
        }
        public void RecoverSilence()
        {
            CurrentSilence = 0;
            this.DeBuff(this.pbSilence);
        }
        public void RecoverSlip()
        {
            CurrentSlip = 0;
            this.DeBuff(this.pbSlip);
        }
        public void RecoverTemptation()
        {
            currentTemptation = 0;
            this.DeBuff(this.pbTemptation);
        }
        public void RecoverPreStunning()
        {
            currentPreStunning = 0;
            this.DeBuff(this.pbPreStunning);
        }
        public void RecoverNoResurrection()
        {
            CurrentNoResurrection = 0;
            this.DeBuff(this.pbNoResurrection);
        }


        public delegate void RemoveBuff();
        public void AbstractChangeStatus(string bmpName, int value, TruthImage pbData, RemoveBuff remove, int count)
        {
            if (value <= 0)
            {
                remove();
            }
            else if ((value == 1) && (pbData.sprite == null))
            {
                this.ActivateBuff(pbData, Database.BaseResourceFolder + bmpName + ".bmp", count);
            }
        }
        public void ChangePoisonStatus(int count)
        {
            pbPoison.Cumulative = currentPoisonValue;
            AbstractChangeStatus("Poison", this.CurrentPoisonValue, pbPoison, this.RemovePoison, count);
        }
        public void ChangeConcussiveHitStatus(int count)
        {
            pbConcussiveHit.Cumulative = currentConcussiveHitValue;
            AbstractChangeStatus(Database.CONCUSSIVE_HIT, this.CurrentConcussiveHitValue, pbConcussiveHit, this.RemoveConcussiveHit, count);
        }
        public void ChangeOnslaughtHitStatus(int count)
        {
            pbOnslaughtHit.Cumulative = currentOnslaughtHitValue;
            AbstractChangeStatus(Database.ONSLAUGHT_HIT, this.CurrentOnslaughtHitValue, pbOnslaughtHit, this.RemoveOnslaughtHit, count);
        }
        public void ChangeImpulseHitStatus(int count)
        {
            pbImpulseHit.Cumulative = currentImpulseHitValue;
            AbstractChangeStatus(Database.IMPULSE_HIT, this.CurrentImpulseHitValue, pbImpulseHit, this.RemoveImpulseHit, count);
        }
        public void ChangeSkyShieldStatus(int count)
        {
            pbSkyShield.Cumulative = currentSkyShieldValue;
            AbstractChangeStatus(Database.SKY_SHIELD, this.CurrentSkyShieldValue, pbSkyShield, this.RemoveSkyShield, count);
        }
        public void ChangeStaticBarrierStatus(int count)
        {
            pbStaticBarrier.Cumulative = currentStaticBarrierValue;
            AbstractChangeStatus(Database.STATIC_BARRIER, this.CurrentStaticBarrierValue, pbStaticBarrier, this.RemoveStaticBarrier, count);
        }
        public void ChangeStanceOfMysticStatus(int count)
        {
            pbStanceOfMystic.Cumulative = currentStanceOfMysticValue;
            AbstractChangeStatus(Database.STANCE_OF_MYSTIC, this.CurrentStanceOfMysticValue, pbStanceOfMystic, this.RemoveStanceOfMystic, count);
        }
        public void ChangeFeltusStatus(int count)
        {
            pbFeltus.Cumulative = currentFeltusValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_FELTUS, this.currentFeltusValue, pbFeltus, this.RemoveFeltus, count);
        }
        public void ChangeJuzaPhantasmalStatus(int count)
        {
            pbJuzaPhantasmal.Cumulative = currentJuzaPhantasmalValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_JUZA_PHANTASMAL, this.CurrentJuzaPhantasmalValue, pbJuzaPhantasmal, this.RemoveJuzaPhantasmal, count);
        }
        public void ChangeEternalFateRingStatus(int count)
        {
            pbEternalFateRing.Cumulative = currentEternalFateRingValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_ETERNAL_FATE, this.currentEternalFateRingValue, pbEternalFateRing, this.RemoveEternalFateRing, count);
        }
        public void ChangeLightServantStatus(int count)
        {
            pbLightServant.Cumulative = currentLightServantValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_LIGHT_SERVANT, this.CurrentLightServantValue, pbLightServant, this.RemoveLightServant, count);
        }
        public void ChangeShadowServantStatus(int count)
        {
            pbShadowServant.Cumulative = currentShadowServantValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_SHADOW_SERVANT, this.CurrentShadowServantValue, pbShadowServant, this.RemoveShadowServant, count);
        }
        public void ChangeAdilBlueBurnStatus(int count)
        {
            pbAdilBlueBurn.Cumulative = currentAdilBlueBurnValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN, this.CurrentAdilBlueBurnValue, pbAdilBlueBurn, this.RemoveAdilBlueBurn, count);
        }
        public void ChangeMazeCubeStatus(int count)
        {
            pbMazeCube.Cumulative = currentMazeCubeValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_MAZE_CUBE, this.CurrentMazeCubeValue, pbMazeCube, this.RemoveMazeCube, count);
        }
        public void ChangeLifeCountStatus(int count)
        {
            pbLifeCount.Cumulative = currentLifeCountValue;
            AbstractChangeStatus(Database.LIFE_COUNT, this.CurrentLifeCountValue, pbLifeCount, this.RemoveLifeCount, count);
        }


        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase)
        {
            int dummy1 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref dummy1);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1)
        {
            int value2 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2)
        {
            int value3 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3)
        {
            int value4 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4)
        {
            int value5 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4, ref value5);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4, ref int value5)
        {
            bool flag1 = false;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4, ref value5, ref flag1);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4, ref int value5, ref bool flag1)
        {
            if (countBase > 0)
            {
                Debug.Log("countBase: " + countBase.ToString());
                countBase--;
                if (picture != null)
                {
                    picture.Count--;
                }
                if (countBase <= 0)
                {
                    flag1 = true; // [警告]:deadSignForTranscendentWishをtrueにするために採用したフラグだが、他の展開を考えた時、デフォルトtrue指定するようになるので留意必須。
                    value1 = 0;
                    value2 = 0;
                    value3 = 0;
                    value4 = 0;
                    value5 = 0;
                    if (picture != null)
                    {
                        Debug.Log("call RemoveOneBuff");
                        RemoveOneBuff(picture);
                        this.BuffNumber--;
                    }
                }
            }
        }

        protected void AbstractCountUpBuff(TruthImage picture, ref int countBase)
        {
            if (countBase > 0)
            {
                countBase++;
                if (picture != null)
                {
                    picture.Count++;
                }
            }
        }

        public void UpdateGenesisCommand(PlayerAction curPA, string spell, string skill, string item, string arche)
        {
            this.beforePA = curPA;
            if (this.beforePA == PlayerAction.UseSpell)
            {
                this.beforeSpellName = spell;
                this.beforeSkillName = string.Empty;
                this.beforeUsingItem = string.Empty;
                this.beforeArchetypeName = string.Empty;
            }
            if (this.beforePA == PlayerAction.UseSkill)
            {
                this.beforeSpellName = string.Empty;
                this.beforeSkillName = skill;
                this.beforeUsingItem = string.Empty;
                this.beforeArchetypeName = string.Empty;
            }
            if (this.beforePA == PlayerAction.UseItem)
            {
                this.beforeSpellName = string.Empty;
                this.beforeSkillName = string.Empty;
                this.beforeUsingItem = item;
                this.beforeArchetypeName = string.Empty;
            }
            if (this.beforePA == PlayerAction.Archetype)
            {
                this.beforeSpellName = string.Empty;
                this.beforeSkillName = string.Empty;
                this.beforeUsingItem = string.Empty;
                this.beforeArchetypeName = arche;
            }
            this.beforeTarget = Target;
            this.beforeTarget2 = Target2; // 後編追加
            // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
        }
        // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
        public void CleanUpEffect()
        {
            CleanUpEffect(true, true);
        }
        public void CleanUpEffect(bool ClearActionInfo, bool ClearBeforeAction)
        {
            if (PA == PlayerAction.UseSkill && currentSkillName == Database.GENESIS)
            {
                // Genesisスキルは前回の情報をクリアしないことでそれを使い続けます。
            }
            else if (PA == PlayerAction.UseSkill && currentSkillName == Database.STANCE_OF_DOUBLE)
            {
                // StanceOfDoubleスキルは前回の情報をクリアしないことでそれを使い続けます。
            }
            else
            {
                if (ClearBeforeAction == true)
                {
                    this.beforePA = PA;
                    this.beforeUsingItem = currentUsingItem;
                    this.beforeSkillName = currentSkillName;
                    this.beforeSpellName = currentSpellName;
                    this.beforeArchetypeName = currentArchetypeName; // 後編追加
                    this.beforeTarget = this.Target;
                    this.beforeTarget2 = this.Target2; // 後編追加
                    // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
                }
            }

            if (ClearActionInfo)
            {
                CurrentUsingItem = string.Empty;
                CurrentSkillName = string.Empty;
                CurrentSpellName = string.Empty;
                CurrentArchetypeName = string.Empty; // 後編追加
                PA = PlayerAction.None;
                this.Target = null;
                this.Target2 = null; // 後編追加
            }

            // 循環と誓約がかかっている場合、カウントダウンしない。
            if (this.CurrentJunkan_Seiyaku <= 0)
            {
                // change start 後編編集（抽象化メソッドを使って一行で表す様、修正）
                // 動
                // 静
                if (this.realTimeBattle == false) // 後編追加、リアルタイムでは、ターン終了で解除せず、別のフェーズで解除する。
                {
                    CurrentCounterAttack = 0; // 後編編集
                }
                // s 後編追加
                else
                {
                    AbstractCountDownBuff(pbCounterAttack, ref CurrentCounterAttack);
                }
                // e 後編追加

                AbstractCountDownBuff(pbAntiStun, ref CurrentAntiStun);
                AbstractCountDownBuff(pbStanceOfDeath, ref CurrentStanceOfDeath);

                // 柔
                AbstractCountDownBuff(pbStanceOfFlow, ref CurrentStanceOfFlow);
                // 剛
                AbstractCountDownBuff(pbStanceOfStanding, ref CurrentStanceOfStanding); // 後編編集
                // 心眼
                AbstractCountDownBuff(pbTruthVision, ref CurrentTruthVision);
                AbstractCountDownBuff(pbHighEmotionality, ref CurrentHighEmotionality, ref buffStrength_HighEmotionality, ref buffAgility_HighEmotionality, ref buffIntelligence_HighEmotionality, ref buffStamina_HighEmotionality, ref buffMind_HighEmotionality);
                AbstractCountDownBuff(pbStanceOfEyes, ref CurrentStanceOfEyes); // 後編編集
                AbstractCountDownBuff(pbPainfulInsanity, ref CurrentPainfulInsanity);
                // 無心
                AbstractCountDownBuff(pbNegate, ref CurrentNegate); // 後編編集
                AbstractCountDownBuff(pbVoidExtraction, ref CurrentVoidExtraction, ref buffStrength_VoidExtraction, ref buffAgility_VoidExtraction, ref buffIntelligence_VoidExtraction, ref buffStamina_VoidExtraction, ref buffMind_VoidExtraction);
                AbstractCountDownBuff(pbNothingOfNothingness, ref CurrentNothingOfNothingness);

                // 聖
                AbstractCountDownBuff(pbProtection, ref CurrentProtection);
                AbstractCountDownBuff(pbSaintPower, ref CurrentSaintPower);
                AbstractCountDownBuff(pbGlory, ref CurrentGlory);
                // 闇
                AbstractCountDownBuff(pbShadowPact, ref CurrentShadowPact);
                AbstractCountDownBuff(pbBlackContract, ref CurrentBlackContract);
                AbstractCountDownBuff(pbBloodyVengeance, ref CurrentBloodyVengeance, ref buffStrength_BloodyVengeance);
                AbstractCountDownBuff(pbDamnation, ref CurrentDamnation);
                // 火
                AbstractCountDownBuff(pbFlameAura, ref CurrentFlameAura);
                AbstractCountDownBuff(pbHeatBoost, ref CurrentHeatBoost, ref buffAgility_HeatBoost);
                AbstractCountDownBuff(pbImmortalRave, ref CurrentImmortalRave);
                // 水
                AbstractCountDownBuff(pbAbsorbWater, ref CurrentAbsorbWater);
                AbstractCountDownBuff(pbMirrorImage, ref CurrentMirrorImage);
                AbstractCountDownBuff(pbPromisedKnowledge, ref CurrentPromisedKnowledge, ref buffIntelligence_PromisedKnowledge);
                AbstractCountDownBuff(pbAbsoluteZero, ref CurrentAbsoluteZero);
                // 理
                AbstractCountDownBuff(pbGaleWind, ref CurrentGaleWind);
                AbstractCountDownBuff(pbWordOfLife, ref CurrentWordOfLife);
                AbstractCountDownBuff(pbWordOfFortune, ref CurrentWordOfFortune);
                AbstractCountDownBuff(pbAetherDrive, ref CurrentAetherDrive);
                AbstractCountDownBuff(pbEternalPresence, ref CurrentEternalPresence);
                // 空
                AbstractCountDownBuff(pbRiseOfImage, ref CurrentRiseOfImage, ref buffMind_RiseOfImage);
                AbstractCountDownBuff(pbDeflection, ref CurrentDeflection);
                AbstractCountDownBuff(pbOneImmunity, ref CurrentOneImmunity);
                AbstractCountDownBuff(pbTimeStop, ref CurrentTimeStop);

                // 聖＋闇
                AbstractCountDownBuff(pbPsychicTrance, ref CurrentPsychicTrance);
                AbstractCountDownBuff(pbBlindJustice, ref CurrentBlindJustice);
                AbstractCountDownBuff(pbTranscendentWish, ref CurrentTranscendentWish, ref buffStrength_TranscendentWish, ref buffAgility_TranscendentWish, ref buffIntelligence_TranscendentWish, ref buffStamina_TranscendentWish, ref buffMind_TranscendentWish, ref deadSignForTranscendentWish);
                // 聖＋火
                AbstractCountDownBuff(pbFlashBlaze, ref CurrentFlashBlazeCount);
                // 聖＋水
                AbstractCountDownBuff(pbSkyShield, ref CurrentSkyShield, ref currentSkyShieldValue);
                AbstractCountDownBuff(pbEverDroplet, ref CurrentEverDroplet);
                // 聖＋理
                AbstractCountDownBuff(pbHolyBreaker, ref CurrentHolyBreaker);
                AbstractCountDownBuff(pbExaltedField, ref CurrentExaltedField);
                AbstractCountDownBuff(pbHymnContract, ref CurrentHymnContract);
                // 聖＋空
                AbstractCountDownBuff(pbStarLightning, ref CurrentStarLightning);
                // 闇＋火
                AbstractCountDownBuff(pbBlackFire, ref CurrentBlackFire);
                AbstractCountDownBuff(pbBlazingField, ref CurrentBlazingField, ref CurrentBlazingFieldFactor);
                // 闇＋水
                CurrentDeepMirror = false;
                // 闇＋理
                AbstractCountDownBuff(pbWordOfMalice, ref CurrentWordOfMalice);
                AbstractCountDownBuff(pbSinFortune, ref CurrentSinFortune);
                // 闇＋空
                AbstractCountDownBuff(pbDarkenField, ref CurrentDarkenField);
                AbstractCountDownBuff(pbEclipseEnd, ref CurrentEclipseEnd);
                // 火＋水
                AbstractCountDownBuff(pbFrozenAura, ref CurrentFrozenAura);
                // 火＋理
                AbstractCountDownBuff(pbEnrageBlast, ref CurrentEnrageBlast);
                AbstractCountDownBuff(pbSigilOfHomura, ref CurrentSigilOfHomura);
                // 火＋空
                AbstractCountDownBuff(pbImmolate, ref CurrentImmolate);
                AbstractCountDownBuff(pbPhantasmalWind, ref CurrentPhantasmalWind);
                AbstractCountDownBuff(pbRedDragonWill, ref CurrentRedDragonWill);
                // 水＋理
                AbstractCountDownBuff(pbStaticBarrier, ref CurrentStaticBarrier, ref currentStaticBarrierValue);
                AbstractCountDownBuff(pbAusterityMatrix, ref CurrentAusterityMatrix);
                // 水＋空
                AbstractCountDownBuff(pbBlueDragonWill, ref CurrentBlueDragonWill);
                // 理＋空
                AbstractCountDownBuff(pbSeventhMagic, ref CurrentSeventhMagic);
                AbstractCountDownBuff(pbParadoxImage, ref CurrentParadoxImage);
                // 動＋静
                AbstractCountDownBuff(pbStanceOfDouble, ref CurrentStanceOfDouble);
                // 動＋柔
                AbstractCountDownBuff(pbSwiftStep, ref CurrentSwiftStep);
                AbstractCountDownBuff(pbVigorSense, ref CurrentVigorSense);
                // 動＋剛
                AbstractCountDownBuff(pbRisingAura, ref CurrentRisingAura);
                // 動＋心眼
                AbstractCountDownBuff(pbOnslaughtHit, ref CurrentOnslaughtHit, ref CurrentOnslaughtHitValue);
                // 動＋無心
                AbstractCountDownBuff(pbSmoothingMove, ref CurrentSmoothingMove);
                AbstractCountDownBuff(pbAscensionAura, ref CurrentAscensionAura);
                // 静＋柔
                AbstractCountDownBuff(pbFutureVision, ref CurrentFutureVision);
                // 静＋剛
                AbstractCountDownBuff(pbReflexSpirit, ref CurrentReflexSpirit);
                // 静＋心眼
                AbstractCountDownBuff(pbConcussiveHit, ref CurrentConcussiveHit, ref CurrentConcussiveHitValue);
                // 静＋無心
                AbstractCountDownBuff(pbTrustSilence, ref CurrentTrustSilence);
                // 柔＋剛
                AbstractCountDownBuff(pbStanceOfMystic, ref CurrentStanceOfMystic, ref CurrentStanceOfMysticValue);
                // 柔＋心眼
                AbstractCountDownBuff(pbNourishSense, ref CurrentNourishSense);
                // 柔＋無心
                AbstractCountDownBuff(pbImpulseHit, ref CurrentImpulseHit, ref CurrentImpulseHitValue);
                // 剛＋心眼
                AbstractCountDownBuff(pbOneAuthority, ref CurrentOneAuthority);
                // 剛＋無心
                CurrentHardestParry = false;
                // 心眼＋無心
                CurrentStanceOfSuddenness = false;

                // 武器特有
                AbstractCountDownBuff(pbFeltus, ref CurrentFeltus, ref CurrentFeltusValue);
                AbstractCountDownBuff(pbJuzaPhantasmal, ref CurrentJuzaPhantasmal, ref CurrentJuzaPhantasmalValue);
                AbstractCountDownBuff(pbEternalFateRing, ref currentEternalFateRing, ref currentEternalFateRingValue);
                AbstractCountDownBuff(pbLightServant, ref CurrentLightServant, ref CurrentLightServantValue);
                AbstractCountDownBuff(pbShadowServant, ref CurrentShadowServant, ref CurrentShadowServantValue);
                AbstractCountDownBuff(pbAdilBlueBurn, ref CurrentAdilBlueBurn, ref CurrentAdilBlueBurnValue);
                AbstractCountDownBuff(pbMazeCube, ref CurrentMazeCube, ref CurrentMazeCubeValue);
                AbstractCountDownBuff(pbShadowBible, ref CurrentShadowBible);
                AbstractCountDownBuff(pbDetachmentOrb, ref CurrentDetachmentOrb);
                AbstractCountDownBuff(pbDevilSummonerTome, ref CurrentDevilSummonerTome);
                AbstractCountDownBuff(pbVoidHymnsonia, ref CurrentVoidHymnsonia);
                AbstractCountDownBuff(pbSagePotionMini, ref CurrentSagePotionMini);
                AbstractCountDownBuff(pbGenseiTaima, ref CurrentGenseiTaima);
                AbstractCountDownBuff(pbShiningAether, ref CurrentShiningAether);
                AbstractCountDownBuff(pbBlackElixir, ref CurrentBlackElixir, ref CurrentBlackElixirValue);
                AbstractCountDownBuff(pbElementalSeal, ref CurrentElementalSeal);
                AbstractCountDownBuff(pbColoressAntidote, ref CurrentColoressAntidote);

                // 最終戦ライフカウント
                //AbstractCountDownBuff(pbLifeCount, ref CurrentLifeCount, ref CurrentLifeCountValue); // ターンのクリーンナップでカウントダウンはしない
                AbstractCountDownBuff(pbChaoticSchema, ref CurrentChaoticSchema);

                // 正の影響効果
                AbstractCountDownBuff(pbBlinded, ref CurrentBlinded);
                // SpeedBoostはBattleEnemyフォーム側でマイナスさせます。
                // CurrentChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                // CurrentPhysicalChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                AbstractCountDownBuff(pbPhysicalAttackUp, ref CurrentPhysicalAttackUp, ref CurrentPhysicalAttackUpValue);
                AbstractCountDownBuff(pbPhysicalDefenseUp, ref CurrentPhysicalDefenseUp, ref CurrentPhysicalDefenseUpValue);
                AbstractCountDownBuff(pbMagicAttackUp, ref CurrentMagicAttackUp, ref CurrentMagicAttackUpValue);
                AbstractCountDownBuff(pbMagicDefenseUp, ref CurrentMagicDefenseUp, ref CurrentMagicDefenseUpValue);
                AbstractCountDownBuff(pbSpeedUp, ref CurrentSpeedUp, ref CurrentSpeedUpValue);
                AbstractCountDownBuff(pbReactionUp, ref CurrentReactionUp, ref CurrentReactionUpValue);
                AbstractCountDownBuff(pbPotentialUp, ref CurrentPotentialUp, ref CurrentPotentialUpValue);

                AbstractCountDownBuff(pbStrengthUp, ref CurrentStrengthUp, ref CurrentStrengthUpValue);
                AbstractCountDownBuff(pbAgilityUp, ref CurrentAgilityUp, ref CurrentAgilityUpValue);
                AbstractCountDownBuff(pbIntelligenceUp, ref CurrentIntelligenceUp, ref CurrentIntelligenceUpValue);
                AbstractCountDownBuff(pbStaminaUp, ref CurrentStaminaUp, ref CurrentStaminaUpValue);
                AbstractCountDownBuff(pbMindUp, ref CurrentMindUp, ref CurrentMindUpValue);

                AbstractCountDownBuff(pbLightUp, ref CurrentLightUp, ref CurrentLightUpValue);
                AbstractCountDownBuff(pbShadowUp, ref CurrentShadowUp, ref CurrentShadowUpValue);
                AbstractCountDownBuff(pbFireUp, ref CurrentFireUp, ref CurrentFireUpValue);
                AbstractCountDownBuff(pbIceUp, ref CurrentIceUp, ref CurrentIceUpValue);
                AbstractCountDownBuff(pbForceUp, ref CurrentForceUp, ref CurrentForceUpValue);
                AbstractCountDownBuff(pbWillUp, ref CurrentWillUp, ref CurrentWillUpValue);

                AbstractCountDownBuff(pbResistLightUp, ref CurrentResistLightUp, ref CurrentResistLightUpValue);
                AbstractCountDownBuff(pbResistShadowUp, ref CurrentResistShadowUp, ref CurrentResistShadowUpValue);
                AbstractCountDownBuff(pbResistFireUp, ref CurrentResistFireUp, ref CurrentResistFireUpValue);
                AbstractCountDownBuff(pbResistIceUp, ref CurrentResistIceUp, ref CurrentResistIceUpValue);
                AbstractCountDownBuff(pbResistForceUp, ref CurrentResistForceUp, ref CurrentResistForceUpValue);
                AbstractCountDownBuff(pbResistWillUp, ref CurrentResistWillUp, ref CurrentResistWillUpValue);

                // 集中と断絶
                AbstractCountDownBuff(pbSyutyuDanzetsu, ref CurrentSyutyu_Danzetsu);
            }

            // 循環と誓約(コレ自身は、【循環と誓約】効果対象外）
            AbstractCountDownBuff(pbJunkanSeiyaku, ref CurrentJunkan_Seiyaku);

            // 負の影響効果(【循環と誓約】効果対象外）
            AbstractCountDownBuff(pbPreStunning, ref CurrentPreStunning);
            AbstractCountDownBuff(pbStun, ref CurrentStunning);
            AbstractCountDownBuff(pbSilence, ref CurrentSilence);
            AbstractCountDownBuff(pbPoison, ref CurrentPoison, ref CurrentPoisonValue);
            AbstractCountDownBuff(pbTemptation, ref CurrentTemptation);
            AbstractCountDownBuff(pbFrozen, ref CurrentFrozen);
            AbstractCountDownBuff(pbParalyze, ref CurrentParalyze);
            AbstractCountDownBuff(pbNoResurrection, ref CurrentNoResurrection);
            // s 後編追加
            AbstractCountDownBuff(pbSlip, ref CurrentSlip);
            AbstractCountDownBuff(pbSlow, ref CurrentSlow);
            AbstractCountDownBuff(pbNoGainLife, ref CurrentNoGainLife);
            AbstractCountDownBuff(pbBlind, ref CurrentBlind);

            AbstractCountDownBuff(pbPhysicalAttackDown, ref CurrentPhysicalAttackDown, ref CurrentPhysicalAttackDownValue);
            AbstractCountDownBuff(pbPhysicalDefenseDown, ref CurrentPhysicalDefenseDown, ref CurrentPhysicalDefenseDownValue);
            AbstractCountDownBuff(pbMagicAttackDown, ref CurrentMagicAttackDown, ref CurrentMagicAttackDownValue);
            AbstractCountDownBuff(pbMagicDefenseDown, ref CurrentMagicDefenseDown, ref CurrentMagicDefenseDownValue);
            AbstractCountDownBuff(pbSpeedDown, ref CurrentSpeedDown, ref CurrentSpeedDownValue);
            AbstractCountDownBuff(pbReactionDown, ref CurrentReactionDown, ref CurrentReactionDownValue);
            AbstractCountDownBuff(pbPotentialDown, ref CurrentPotentialDown, ref CurrentPotentialDownValue);

            // pbStrengthDown系が存在しない

            AbstractCountDownBuff(pbLightDown, ref CurrentLightDown, ref CurrentLightDownValue);
            AbstractCountDownBuff(pbShadowDown, ref CurrentShadowDown, ref CurrentShadowDownValue);
            AbstractCountDownBuff(pbFireDown, ref CurrentFireDown, ref CurrentFireDownValue);
            AbstractCountDownBuff(pbIceDown, ref CurrentIceDown, ref CurrentIceDownValue);
            AbstractCountDownBuff(pbForceDown, ref CurrentForceDown, ref CurrentForceDownValue);
            AbstractCountDownBuff(pbWillDown, ref CurrentWillDown, ref CurrentWillDownValue);

            // pbResistLightDown系が存在しない

            AbstractCountDownBuff(pbAfterReviveHalf, ref CurrentAfterReviveHalf);

            AbstractCountDownBuff(pbFireDamage2, ref CurrentFireDamage2);

            AbstractCountDownBuff(pbBlackMagic, ref CurrentBlackMagic);

            //AbstractCountDownBuff(pbChaosDesperate, ref CurrentChaosDesperate, ref CurrentChaosDesperateValue);
            if (CurrentChaosDesperate > 0)
            {
                CurrentChaosDesperate--;
                CurrentChaosDesperateValue--;
                if (CurrentChaosDesperate <= 0 || CurrentChaosDesperateValue <= 0)
                {
                    //CurrentChaosDesperate = 0; 外部クラスで判定材料にするため、あえてコメントアウト
                    CurrentChaosDesperateValue = 0;
                    if (pbChaosDesperate != null)
                    {
                        RemoveOneBuff(pbChaosDesperate);
                        this.BuffNumber--;
                    }
                }
            }

            AbstractCountDownBuff(pbIchinaruHomura, ref CurrentIchinaruHomura);
            AbstractCountDownBuff(pbAbyssFire, ref CurrentAbyssFire);
            AbstractCountDownBuff(pbLightAndShadow, ref CurrentLightAndShadow);
            AbstractCountDownBuff(pbEternalDroplet, ref CurrentEternalDroplet);
            AbstractCountDownBuff(pbAusterityMatrixOmega, ref CurrentAusterityMatrixOmega);
            AbstractCountDownBuff(pbVoiceOfAbyss, ref CurrentVoiceOfAbyss);
            AbstractCountDownBuff(pbAbyssWill, ref CurrentAbyssWill, ref CurrentAbyssWillValue);
            AbstractCountDownBuff(pbTheAbyssWall, ref CurrentTheAbyssWall);
            // e 後編追加

            PoolLifeConsumption = 0;
            PoolManaConsumption = 0;
            PoolSkillConsumption = 0;

            // 後編
            // s 後編「コメント」以下は、戦闘中永続であり、カウントダウンを含めない。
            //amplifyPhysicalAttack;
            //amplifyPhysicalDefense;
            //amplifyMagicAttack;
            //amplifyMagicDefense;
            //amplifyBattleSpeed;
            //amplifyBattleResponse;
            //amplifyPotential;
        }

        // s 後編追加
        // ボス負の影響自動リカバー
        protected int autoRecoverStunning = 0;
        protected int autoRecoverSilence = 0;
        protected int autoRecoverPoison = 0;
        protected int autoRecoverTemptation = 0;
        protected int autoRecoverFrozen = 0;
        protected int autoRecoverParalyze = 0;
        protected int autoRecoverNoResurrection = 0;
        protected int autoRecoverSlow = 0;
        protected int autoRecoverBlind = 0;
        protected int autoRecoverSlip = 0;
        protected int autoRecoverNoGainLife = 0;
        // e 後編追加

        public void CleanUpEffectForBoss()
        {
            if (CurrentStunning > 0)
            {
                autoRecoverStunning++;
            }
            if (CurrentSilence > 0)
            {
                autoRecoverSilence++;
            }
            if (CurrentPoison > 0)
            {
                autoRecoverPoison++;
            }
            if (CurrentTemptation > 0)
            {
                autoRecoverTemptation++;
            }
            if (CurrentFrozen > 0)
            {
                autoRecoverFrozen++;
            }
            if (CurrentParalyze > 0)
            {
                autoRecoverParalyze++;
            }
            if (CurrentNoResurrection > 0)
            {
                autoRecoverNoResurrection++;
            }
            if (CurrentSlow > 0)
            {
                autoRecoverSlow++;
            }
            if (CurrentBlind > 0)
            {
                autoRecoverBlind++;
            }
            if (CurrentSlip > 0)
            {
                autoRecoverSlip++;
            }
            if (CurrentNoGainLife > 0)
            {
                autoRecoverNoGainLife++;
            }

            if (autoRecoverStunning >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverStunning = 0;
                CurrentStunning--;
                if (CurrentStunning <= 0)
                {
                    if (pbStun != null)
                    {
                        RemoveOneBuff(pbStun);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSilence >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSilence = 0;
                CurrentSilence--;
                if (CurrentSilence <= 0)
                {
                    if (pbSilence != null)
                    {
                        RemoveOneBuff(pbSilence);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverPoison >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverPoison = 0;
                CurrentPoison--;
                if (CurrentPoison <= 0)
                {
                    if (pbPoison != null)
                    {
                        RemoveOneBuff(pbPoison);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverTemptation >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverTemptation = 0;
                CurrentTemptation--;
                if (CurrentTemptation <= 0)
                {
                    if (pbTemptation != null)
                    {
                        RemoveOneBuff(pbTemptation);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverFrozen >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverFrozen = 0;
                CurrentFrozen--;
                if (CurrentFrozen <= 0)
                {
                    if (pbFrozen != null)
                    {
                        RemoveOneBuff(pbFrozen);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverParalyze >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverParalyze = 0;
                CurrentParalyze--;
                if (CurrentParalyze <= 0)
                {
                    if (pbParalyze != null)
                    {
                        RemoveOneBuff(pbParalyze);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverNoResurrection >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverNoResurrection = 0;
                CurrentNoResurrection--;
                if (CurrentNoResurrection <= 0)
                {
                    if (pbNoResurrection != null)
                    {
                        RemoveOneBuff(pbNoResurrection);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSlow >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSlow = 0;
                CurrentSlow--;
                if (CurrentSlow <= 0)
                {
                    if (pbSlow != null)
                    {
                        RemoveOneBuff(pbSlow);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverBlind >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverBlind = 0;
                CurrentBlind--;
                if (CurrentBlind <= 0)
                {
                    if (pbBlind != null)
                    {
                        RemoveOneBuff(pbBlind);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSlip >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSlip = 0;
                CurrentSlip--;
                if (CurrentSlip <= 0)
                {
                    if (pbSlip != null)
                    {
                        RemoveOneBuff(pbSlip);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverNoGainLife >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverNoGainLife = 0;
                CurrentNoGainLife--;
                if (CurrentNoGainLife <= 0)
                {
                    if (pbNoGainLife != null)
                    {
                        RemoveOneBuff(pbNoGainLife);
                        this.BuffNumber--;
                    }
                }
            }

            if (this.CurrentTimeStopImmediate)
            {
                AbstractCountDownBuff(this.pbTimeStop, ref this.CurrentTimeStop);
            }

        }
        internal void CleanUpBattleEnd(ref string brokenName)
        {
            this.beforePA = PlayerAction.None;
            this.beforeUsingItem = String.Empty;
            this.beforeSkillName = String.Empty;
            this.beforeSpellName = String.Empty;
            this.beforeArchetypeName = String.Empty; // 後編追加
            this.beforeTarget = null;
            this.beforeTarget2 = null; // 後編追加
            // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
            PA = PlayerAction.None;
            CurrentUsingItem = String.Empty;
            CurrentSkillName = String.Empty;
            CurrentSpellName = String.Empty;
            CurrentArchetypeName = String.Empty; // 後編追加
            Target = null;
            Target2 = null; // 後編追加

            // ボス負の影響自動リカバー
            autoRecoverStunning = 0;
            autoRecoverSilence = 0;
            autoRecoverPoison = 0;
            autoRecoverTemptation = 0;
            autoRecoverFrozen = 0;
            autoRecoverParalyze = 0;
            autoRecoverNoResurrection = 0;
            autoRecoverSlow = 0;
            autoRecoverBlind = 0;
            autoRecoverSlip = 0;

            // 動
            // 静
            CurrentCounterAttack = 0; // 後編編集
            CurrentAntiStun = 0;
            CurrentStanceOfDeath = 0;
            // 柔
            CurrentStanceOfFlow = 0;
            // 剛
            CurrentStanceOfStanding = 0; // 後編編集
            // 心眼
            CurrentTruthVision = 0;
            CurrentHighEmotionality = 0;
            CurrentStanceOfEyes = 0; // 後編編集
            CurrentPainfulInsanity = 0;
            // 無心
            CurrentNegate = 0; // 後編編集
            CurrentVoidExtraction = 0;
            CurrentNothingOfNothingness = 0;

            // 聖
            CurrentProtection = 0;
            CurrentSaintPower = 0;
            CurrentGlory = 0;
            // 闇
            CurrentShadowPact = 0;
            CurrentBlackContract = 0;
            CurrentBloodyVengeance = 0;
            CurrentDamnation = 0;
            // 火
            CurrentFlameAura = 0;
            CurrentHeatBoost = 0;
            CurrentImmortalRave = 0;
            // 水
            CurrentAbsorbWater = 0;
            CurrentMirrorImage = 0;
            CurrentAbsoluteZero = 0;
            CurrentPromisedKnowledge = 0;
            // 理
            CurrentGaleWind = 0;
            CurrentWordOfLife = 0;
            CurrentWordOfFortune = 0;
            CurrentAetherDrive = 0;
            CurrentEternalPresence = 0;
            // 空
            CurrentRiseOfImage = 0;
            CurrentOneImmunity = 0;
            CurrentDeflection = 0;
            CurrentTimeStop = 0;
            CurrentTimeStopImmediate = false;

            // s 後編追加
            // 聖＋闇
            CurrentPsychicTrance = 0;
            CurrentBlindJustice = 0;
            CurrentTranscendentWish = 0;
            // 聖＋火
            CurrentFlashBlazeCount = 0;
            // 聖＋水
            CurrentSkyShield = 0;
            CurrentSkyShieldValue = 0;
            CurrentEverDroplet = 0;
            // 聖＋理
            CurrentHolyBreaker = 0;
            CurrentExaltedField = 0;
            CurrentHymnContract = 0;
            // 聖＋空
            CurrentStarLightning = 0;
            // 闇＋火
            CurrentBlackFire = 0;
            CurrentBlazingField = 0;
            CurrentBlazingFieldFactor = 0;
            // 闇＋水
            CurrentDeepMirror = false;
            // 闇＋理
            CurrentWordOfMalice = 0;
            CurrentSinFortune = 0;
            // 闇＋空
            CurrentDarkenField = 0;
            CurrentEclipseEnd = 0;
            // 火＋水
            CurrentFrozenAura = 0;
            // 火＋理
            CurrentEnrageBlast = 0;
            CurrentSigilOfHomura = 0;
            // 火＋空
            CurrentImmolate = 0;
            CurrentPhantasmalWind = 0;
            CurrentRedDragonWill = 0;
            // 水＋理
            CurrentStaticBarrier = 0;
            CurrentStaticBarrierValue = 0;
            CurrentAusterityMatrix = 0;
            // 水＋空
            CurrentBlueDragonWill = 0;
            // 理＋空
            CurrentSeventhMagic = 0;
            CurrentParadoxImage = 0;

            // 動＋静
            CurrentStanceOfDouble = 0;
            // 動＋柔
            CurrentSwiftStep = 0;
            CurrentVigorSense = 0;
            // 動＋剛
            CurrentRisingAura = 0;
            // 動＋心眼
            CurrentOnslaughtHit = 0;
            CurrentOnslaughtHitValue = 0;
            // 動＋無心
            CurrentSmoothingMove = 0;
            CurrentAscensionAura = 0;
            // 静＋柔
            CurrentFutureVision = 0;
            // 静＋剛
            CurrentReflexSpirit = 0;
            // 静＋心眼
            CurrentConcussiveHit = 0;
            CurrentConcussiveHitValue = 0;
            // 静＋無心
            CurrentTrustSilence = 0;
            // 柔＋剛
            CurrentStanceOfMystic = 0;
            CurrentStanceOfMysticValue = 0;
            // 柔＋心眼
            CurrentNourishSense = 0;
            // 柔＋無心
            CurrentImpulseHit = 0;
            CurrentImpulseHitValue = 0;
            // 剛＋心眼
            CurrentOneAuthority = 0;
            // 剛＋無心
            CurrentHardestParry = false;
            // 心眼＋無心
            CurrentStanceOfSuddenness = false;

            // 武器特有
            CurrentFeltus = 0;
            CurrentFeltusValue = 0;
            CurrentJuzaPhantasmal = 0;
            CurrentJuzaPhantasmalValue = 0;
            CurrentEternalFateRing = 0;
            CurrentEternalFateRingValue = 0;
            CurrentLightServant = 0;
            CurrentLightServantValue = 0;
            CurrentShadowServant = 0;
            CurrentShadowServantValue = 0;
            CurrentAdilBlueBurn = 0;
            CurrentAdilBlueBurnValue = 0;
            CurrentMazeCube = 0;
            CurrentMazeCubeValue = 0;
            CurrentShadowBible = 0;
            CurrentDetachmentOrb = 0;
            CurrentDevilSummonerTome = 0;
            CurrentVoidHymnsonia = 0;

            // 消耗品特有
            CurrentSagePotionMini = 0;
            CurrentGenseiTaima = 0;
            CurrentShiningAether = 0;
            CurrentBlackElixir = 0;
            CurrentBlackElixirValue = 0;
            CurrentElementalSeal = 0;
            CurrentColoressAntidote = 0;
            
            // 最終戦ライフカウント
            CurrentLifeCount = 0;
            CurrentLifeCountValue = 0;

            // ヴェルゼ最終戦カオティックスキーマ
            CurrentChaoticSchema = 0;

            // 集中と断絶
            CurrentSyutyu_Danzetsu = 0;
            CurrentJunkan_Seiyaku = 0;
            // e 後編追加

            // 動
            // 静
            if (pbCounterAttack != null) { pbCounterAttack.sprite = null; pbCounterAttack.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbAntiStun != null) { pbAntiStun.sprite = null; pbAntiStun.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfDeath != null) { pbStanceOfDeath.sprite = null; pbStanceOfDeath.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔
            if (pbStanceOfFlow != null) { pbStanceOfFlow.sprite = null; pbStanceOfFlow.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛
            if (pbStanceOfStanding != null) { pbStanceOfStanding.sprite = null; pbStanceOfStanding.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            // 心眼
            if (pbTruthVision != null) { pbTruthVision.sprite = null; pbTruthVision.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHighEmotionality != null) { pbHighEmotionality.sprite = null; pbHighEmotionality.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfEyes != null) { pbStanceOfEyes.sprite = null; pbStanceOfEyes.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbPainfulInsanity != null) { pbPainfulInsanity.sprite = null; pbPainfulInsanity.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 無心
            if (pbNegate != null) { pbNegate.sprite = null; pbNegate.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbVoidExtraction != null) { pbVoidExtraction.sprite = null; pbVoidExtraction.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNothingOfNothingness != null) { pbNothingOfNothingness.sprite = null; pbNothingOfNothingness.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 聖
            if (pbProtection != null) { pbProtection.sprite = null; pbProtection.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSaintPower != null) { pbSaintPower.sprite = null; pbSaintPower.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGlory != null) { pbGlory.sprite = null; pbGlory.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇
            if (pbShadowPact != null) { pbShadowPact.sprite = null; pbShadowPact.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackContract != null) { pbBlackContract.sprite = null; pbBlackContract.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBloodyVengeance != null) { pbBloodyVengeance.sprite = null; pbBloodyVengeance.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDamnation != null) { pbDamnation.sprite = null; pbDamnation.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火
            if (pbFlameAura != null) { pbFlameAura.sprite = null; pbFlameAura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHeatBoost != null) { pbHeatBoost.sprite = null; pbHeatBoost.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbImmortalRave != null) { pbImmortalRave.sprite = null; pbImmortalRave.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 水
            if (pbAbsorbWater != null) { pbAbsorbWater.sprite = null; pbAbsorbWater.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMirrorImage != null) { pbMirrorImage.sprite = null; pbMirrorImage.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbsoluteZero != null) { pbAbsoluteZero.sprite = null; pbAbsoluteZero.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPromisedKnowledge != null) { pbPromisedKnowledge.sprite = null; pbPromisedKnowledge.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 理
            if (pbGaleWind != null) { pbGaleWind.sprite = null; pbGaleWind.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfLife != null) { pbWordOfLife.sprite = null; pbWordOfLife.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfFortune != null) { pbWordOfFortune.sprite = null; pbWordOfFortune.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAetherDrive != null) { pbAetherDrive.sprite = null; pbAetherDrive.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalPresence != null) { pbEternalPresence.sprite = null; pbEternalPresence.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 空
            if (pbRiseOfImage != null) { pbRiseOfImage.sprite = null; pbRiseOfImage.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbOneImmunity != null) { pbOneImmunity.sprite = null; pbOneImmunity.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDeflection != null) { pbDeflection.sprite = null; pbDeflection.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTimeStop != null) { pbTimeStop.sprite = null; pbTimeStop.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // s 後編追加
            // 聖＋闇
            if (pbPsychicTrance != null) { pbPsychicTrance.sprite = null; pbPsychicTrance.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlindJustice != null) { pbBlindJustice.sprite = null; pbBlindJustice.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTranscendentWish != null) { pbTranscendentWish.sprite = null; pbTranscendentWish.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋火
            if (pbFlashBlaze != null) { pbFlashBlaze.sprite = null; pbFlashBlaze.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            // 聖＋水
            if (pbSkyShield != null) { pbSkyShield.sprite = null; pbSkyShield.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEverDroplet != null) { pbEverDroplet.sprite = null; pbEverDroplet.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋理
            if (pbHolyBreaker != null) { pbHolyBreaker.sprite = null; pbHolyBreaker.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbExaltedField != null) { pbExaltedField.sprite = null; pbExaltedField.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHymnContract != null) { pbHymnContract.sprite = null; pbHymnContract.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋空
            if (pbStarLightning != null) { pbStarLightning.sprite = null; pbStarLightning.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋火
            if (pbBlackFire != null) { pbBlackFire.sprite = null; pbBlackFire.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlazingField != null) { pbBlazingField.sprite = null; pbBlazingField.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋水
            // 闇＋理
            if (pbWordOfMalice != null) { pbWordOfMalice.sprite = null; pbWordOfMalice.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSinFortune != null) { pbSinFortune.sprite = null; pbSinFortune.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋空
            if (pbDarkenField != null) { pbDarkenField.sprite = null; pbDarkenField.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEclipseEnd != null) { pbEclipseEnd.sprite = null; pbEclipseEnd.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋水
            if (pbFrozenAura != null) { pbFrozenAura.sprite = null; pbFrozenAura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋理
            if (pbEnrageBlast != null) { pbEnrageBlast.sprite = null; pbEnrageBlast.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSigilOfHomura != null) { pbSigilOfHomura.sprite = null; pbSigilOfHomura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋空
            if (pbImmolate != null) { pbImmolate.sprite = null; pbImmolate.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhantasmalWind != null) { pbPhantasmalWind.sprite = null; pbPhantasmalWind.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbRedDragonWill != null) { pbRedDragonWill.sprite = null; pbRedDragonWill.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 水＋理
            if (pbStaticBarrier != null) { pbStaticBarrier.sprite = null; pbStaticBarrier.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAusterityMatrix != null) { pbAusterityMatrix.sprite = null; pbAusterityMatrix.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 水＋空
            if (pbVanishWave != null) { pbVanishWave.sprite = null; pbVanishWave.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlueDragonWill != null) { pbBlueDragonWill.sprite = null; pbBlueDragonWill.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 理＋空
            if (pbSeventhMagic != null) { pbSeventhMagic.sprite = null; pbSeventhMagic.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParadoxImage != null) { pbParadoxImage.sprite = null; pbParadoxImage.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 動＋静
            if (pbStanceOfDouble != null) { pbStanceOfDouble.sprite = null; pbStanceOfDouble.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋柔
            if (pbSwiftStep != null) { pbSwiftStep.sprite = null; pbSwiftStep.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVigorSense != null) { pbVigorSense.sprite = null; pbVigorSense.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋剛
            if (pbRisingAura != null) { pbRisingAura.sprite = null; pbRisingAura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋心眼
            if (pbOnslaughtHit != null) { pbOnslaughtHit.sprite = null; pbOnslaughtHit.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋無心
            if (pbSmoothingMove != null) { pbSmoothingMove.sprite = null; pbSmoothingMove.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAscensionAura != null) { pbAscensionAura.sprite = null; pbAscensionAura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋柔
            if (pbFutureVision != null) { pbFutureVision.sprite = null; pbFutureVision.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋剛
            if (pbReflexSpirit != null) { pbReflexSpirit.sprite = null; pbReflexSpirit.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋心眼
            if (pbConcussiveHit != null) { pbConcussiveHit.sprite = null; pbConcussiveHit.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋無心
            if (pbTrustSilence != null) { pbTrustSilence.sprite = null; pbTrustSilence.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋剛
            if (pbStanceOfMystic != null) { pbStanceOfMystic.sprite = null; pbStanceOfMystic.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋心眼
            if (pbNourishSense != null) { pbNourishSense.sprite = null; pbNourishSense.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋無心
            if (pbImpulseHit != null) { pbImpulseHit.sprite = null; pbImpulseHit.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛＋心眼
            if (pbOneAuthority != null) { pbOneAuthority.sprite = null; pbOneAuthority.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛＋無心
            // 心眼＋無心

            // 武器特有BUFF
            if (pbFeltus != null) { pbFeltus.sprite = null; pbFeltus.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbJuzaPhantasmal != null) { pbJuzaPhantasmal.sprite = null; pbJuzaPhantasmal.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalFateRing != null) { pbEternalFateRing.sprite = null; pbEternalFateRing.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightServant != null) { pbLightServant.sprite = null; pbLightServant.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowServant != null) { pbShadowServant.sprite = null; pbShadowServant.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAdilBlueBurn != null) { pbAdilBlueBurn.sprite = null; pbAdilBlueBurn.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMazeCube != null) { pbMazeCube.sprite = null; pbMazeCube.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowBible != null) { pbShadowBible.sprite = null; pbShadowBible.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDetachmentOrb != null) { pbDetachmentOrb.sprite = null; pbDetachmentOrb.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDevilSummonerTome != null) { pbDevilSummonerTome.sprite = null; pbDevilSummonerTome.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVoidHymnsonia != null) { pbVoidHymnsonia.sprite = null; pbVoidHymnsonia.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 消耗品特有
            if (pbSagePotionMini != null) { pbSagePotionMini.sprite = null; pbSagePotionMini.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGenseiTaima != null) { pbGenseiTaima.sprite = null; pbGenseiTaima.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShiningAether != null) { pbShiningAether.sprite = null; pbShiningAether.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackElixir != null) { pbBlackElixir.sprite = null; pbBlackElixir.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbElementalSeal != null) { pbElementalSeal.sprite = null; pbElementalSeal.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbColoressAntidote != null) { pbColoressAntidote.sprite = null; pbColoressAntidote.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 集中と断絶
            if (pbSyutyuDanzetsu != null) { pbSyutyuDanzetsu.sprite = null; pbSyutyuDanzetsu.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 循環と誓約
            if (pbJunkanSeiyaku != null) { pbJunkanSeiyaku.sprite = null; pbJunkanSeiyaku.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 最終戦ライフカウント
            if (pbLifeCount != null) { pbLifeCount.sprite = null; pbLifeCount.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // ヴェルゼ最終戦カオティックスキーマ
            if (pbChaoticSchema != null) { pbChaoticSchema.sprite = null; pbChaoticSchema.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e 後編追加

            // 負の影響効果
            CurrentPreStunning = 0;
            CurrentStunning = 0;
            CurrentSilence = 0;
            CurrentPoison = 0;
            CurrentTemptation = 0;
            CurrentFrozen = 0;
            CurrentParalyze = 0;
            CurrentNoResurrection = 0;
            CurrentSlow = 0; // 後編追加
            CurrentBlind = 0; // 後編追加
            CurrentSlip = 0; // 後編追加
            CurrentNoGainLife = 0; // 後編追加
            CurrentBlinded = 0; // 後編追加

            // s 後編追加
            battleResistStun = false;
            battleResistSilence = false;
            battleResistPoison = false;
            battleResistTemptation = false;
            battleResistFrozen = false;
            battleResistParalyze = false;
            battleResistNoResurrection = false;
            battleResistSlow = false;
            battleResistBlind = false;
            battleResistSlip = false;
            // e 後編追加

            CurrentPoisonValue = 0; // 後編追加

            DeadSignForTranscendentWish = false; // 後編追加

            // 正の影響効果
            CurrentSpeedBoost = 0; // 後編追加
            CurrentBlinded = 0; // 後編追加
            CurrentChargeCount = 0; // 後編追加
            CurrentPhysicalChargeCount = 0; // 後編追加

            // s 後編追加
            CurrentPhysicalAttackUp = 0;
            CurrentPhysicalAttackUpValue = 0;
            CurrentPhysicalAttackDown = 0;
            CurrentPhysicalAttackDownValue = 0;

            CurrentPhysicalDefenseUp = 0;
            CurrentPhysicalDefenseUpValue = 0;
            CurrentPhysicalDefenseDown = 0;
            CurrentPhysicalDefenseDownValue = 0;

            CurrentMagicDefenseUp = 0;
            CurrentMagicDefenseUpValue = 0;
            CurrentMagicDefenseDown = 0;
            CurrentMagicDefenseDownValue = 0;

            CurrentMagicAttackUp = 0;
            CurrentMagicAttackUpValue = 0;
            CurrentMagicAttackDown = 0;
            CurrentMagicAttackDownValue = 0;

            CurrentSpeedUp = 0;
            CurrentSpeedUpValue = 0;
            CurrentSpeedDown = 0;
            CurrentSpeedDownValue = 0;

            CurrentReactionUp = 0;
            CurrentReactionUpValue = 0;
            CurrentReactionDown = 0;
            CurrentReactionDownValue = 0;

            CurrentPotentialUp = 0;
            CurrentPotentialUpValue = 0;
            CurrentPotentialDown = 0;
            CurrentPotentialDownValue = 0;

            CurrentStrengthUp = 0; // 後編追加
            CurrentStrengthUpValue = 0; // 後編追加

            CurrentAgilityUp = 0; // 後編追加
            CurrentAgilityUpValue = 0; // 後編追加

            CurrentIntelligenceUp = 0; // 後編追加
            CurrentIntelligenceUpValue = 0; // 後編追加

            CurrentStaminaUp = 0; // 後編追加
            CurrentStaminaUpValue = 0; // 後編追加

            CurrentMindUp = 0; // 後編追加
            CurrentMindUpValue = 0; // 後編追加

            CurrentLightUp = 0;
            CurrentLightUpValue = 0;
            CurrentLightDown = 0;
            CurrentLightDownValue = 0;

            CurrentShadowUp = 0;
            CurrentShadowUpValue = 0;
            CurrentShadowDown = 0;
            CurrentShadowDownValue = 0;

            CurrentFireUp = 0;
            CurrentFireUpValue = 0;
            CurrentFireDown = 0;
            CurrentFireDownValue = 0;

            CurrentIceUp = 0;
            CurrentIceUpValue = 0;
            CurrentIceDown = 0;
            CurrentIceDownValue = 0;

            CurrentForceUp = 0;
            CurrentForceUpValue = 0;
            CurrentForceDown = 0;
            CurrentForceDownValue = 0;

            CurrentWillUp = 0;
            CurrentWillUpValue = 0;
            CurrentWillDown = 0;
            CurrentWillDownValue = 0;
            // e 後編追加

            // s 後編追加
            CurrentResistLightUp = 0;
            CurrentResistLightUpValue = 0;

            CurrentResistShadowUp = 0;
            CurrentResistShadowUpValue = 0;

            CurrentResistFireUp = 0;
            CurrentResistFireUpValue = 0;

            CurrentResistIceUp = 0;
            CurrentResistIceUpValue = 0;

            CurrentResistForceUp = 0;
            CurrentResistForceUpValue = 0;

            CurrentResistWillUp = 0;
            CurrentResistWillUpValue = 0;

            CurrentAfterReviveHalf = 0;
            CurrentFireDamage2 = 0;
            CurrentBlackMagic = 0;

            CurrentChaosDesperate = 0;
            CurrentChaosDesperateValue = 0;

            CurrentIchinaruHomura = 0;
            CurrentAbyssFire = 0;
            CurrentLightAndShadow = 0;
            CurrentEternalDroplet = 0;
            CurrentAusterityMatrixOmega = 0;
            CurrentVoiceOfAbyss = 0;
            CurrentAbyssWill = 0;
            CurrentAbyssWillValue = 0;
            CurrentTheAbyssWall = 0;

            PoolLifeConsumption = 0;
            PoolManaConsumption = 0;
            PoolSkillConsumption = 0;
            // e 後編追加

            if (pbPreStunning != null) { pbPreStunning.sprite = null; pbPreStunning.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStun != null) { pbStun.sprite = null; pbStun.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSilence != null) { pbSilence.sprite = null; pbSilence.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPoison != null) { pbPoison.sprite = null; pbPoison.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTemptation != null) { pbTemptation.sprite = null; pbTemptation.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFrozen != null) { pbFrozen.sprite = null; pbFrozen.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParalyze != null) { pbParalyze.sprite = null; pbParalyze.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNoResurrection != null) { pbNoResurrection.sprite = null; pbNoResurrection.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSlow != null) { pbSlow.sprite = null; pbSlow.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbBlind != null) { pbBlind.sprite = null; pbBlind.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbSlip != null) { pbSlip.sprite = null; pbSlip.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbNoGainLife != null) { pbNoGainLife.sprite = null; pbNoGainLife.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加

            // s 後編追加
            if (pbPhysicalAttackUp != null) { pbPhysicalAttackUp.sprite = null; pbPhysicalAttackUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhysicalAttackDown != null) { pbPhysicalAttackDown.sprite = null; pbPhysicalAttackDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhysicalDefenseUp != null) { pbPhysicalDefenseUp.sprite = null; pbPhysicalDefenseUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhysicalDefenseDown != null) { pbPhysicalDefenseDown.sprite = null; pbPhysicalDefenseDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMagicAttackUp != null) { pbMagicAttackUp.sprite = null; pbMagicAttackUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMagicAttackDown != null) { pbMagicAttackDown.sprite = null; pbMagicAttackDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMagicDefenseUp != null) { pbMagicDefenseUp.sprite = null; pbMagicDefenseUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMagicDefenseDown != null) { pbMagicDefenseDown.sprite = null; pbMagicDefenseDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSpeedUp != null) { pbSpeedUp.sprite = null; pbSpeedUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSpeedDown != null) { pbSpeedDown.sprite = null; pbSpeedDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbReactionUp != null) { pbReactionUp.sprite = null; pbReactionUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbReactionDown != null) { pbReactionDown.sprite = null; pbReactionDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPotentialUp != null) { pbPotentialUp.sprite = null; pbPotentialUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPotentialDown != null) { pbPotentialDown.sprite = null; pbPotentialDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbStrengthUp != null) { pbStrengthUp.sprite = null; pbStrengthUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAgilityUp != null) { pbAgilityUp.sprite = null; pbAgilityUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIntelligenceUp != null) { pbIntelligenceUp.sprite = null; pbIntelligenceUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStaminaUp != null) { pbStaminaUp.sprite = null; pbStaminaUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMindUp != null) { pbMindUp.sprite = null; pbMindUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbLightUp != null) { pbLightUp.sprite = null; pbLightUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightDown != null) { pbLightDown.sprite = null; pbLightDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowUp != null) { pbShadowUp.sprite = null; pbShadowUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowDown != null) { pbShadowDown.sprite = null; pbShadowDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireUp != null) { pbFireUp.sprite = null; pbFireUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireDown != null) { pbFireDown.sprite = null; pbFireDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIceUp != null) { pbIceUp.sprite = null; pbIceUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIceDown != null) { pbIceDown.sprite = null; pbIceDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbForceUp != null) { pbForceUp.sprite = null; pbForceUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbForceDown != null) { pbForceDown.sprite = null; pbForceDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWillUp != null) { pbWillUp.sprite = null; pbWillUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWillDown != null) { pbWillDown.sprite = null; pbWillDown.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbResistLightUp != null) { pbResistLightUp.sprite = null; pbResistLightUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistShadowUp != null) { pbResistShadowUp.sprite = null; pbResistShadowUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistFireUp != null) { pbResistFireUp.sprite = null; pbResistFireUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistIceUp != null) { pbResistIceUp.sprite = null; pbResistIceUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistForceUp != null) { pbResistForceUp.sprite = null; pbResistForceUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistWillUp != null) { pbResistWillUp.sprite = null; pbResistWillUp.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbAfterReviveHalf != null) { pbAfterReviveHalf.sprite = null; pbAfterReviveHalf.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireDamage2 != null) { pbFireDamage2.sprite = null; pbFireDamage2.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackMagic != null) { pbBlackMagic.sprite = null; pbBlackMagic.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbChaosDesperate != null) { pbChaosDesperate.sprite = null; pbChaosDesperate.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIchinaruHomura != null) { pbIchinaruHomura.sprite = null; pbIchinaruHomura.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbyssFire != null) { pbAbyssFire.sprite = null; pbAbyssFire.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightAndShadow != null) { pbLightAndShadow.sprite = null; pbLightAndShadow.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalDroplet != null) { pbEternalDroplet.sprite = null; pbEternalDroplet.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAusterityMatrixOmega != null) { pbAusterityMatrixOmega.sprite = null; pbAusterityMatrixOmega.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVoiceOfAbyss != null) { pbVoiceOfAbyss.sprite = null; pbVoiceOfAbyss.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbyssWill != null) { pbAbyssWill.sprite = null; pbAbyssWill.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTheAbyssWall != null) { pbTheAbyssWall.sprite = null; pbTheAbyssWall.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbResistStun != null) { pbResistStun.sprite = null; pbResistStun.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSilence != null) { pbResistSilence.sprite = null; pbResistSilence.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistPoison != null) { pbResistPoison.sprite = null; pbResistPoison.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistTemptation != null) { pbResistTemptation.sprite = null; pbResistTemptation.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistFrozen != null) { pbResistFrozen.sprite = null; pbResistFrozen.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistParalyze != null) { pbResistParalyze.sprite = null; pbResistParalyze.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistNoResurrection != null) { pbResistNoResurrection.sprite = null; pbResistNoResurrection.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSlow != null) { pbResistSlow.sprite = null; pbResistSlow.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistBlind != null) { pbResistBlind.sprite = null; pbResistBlind.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSlip != null) { pbResistSlip.sprite = null; pbResistSlip.transform.position = new Vector3(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e 後編追加

            // BUFFUP効果を解除
            BuffStrength_BloodyVengeance = 0;
            BuffAgility_HeatBoost = 0;
            BuffIntelligence_PromisedKnowledge = 0;
            BuffStamina_Unknown = 0;
            BuffMind_RiseOfImage = 0;

            BuffStrength_HighEmotionality = 0;
            BuffAgility_HighEmotionality = 0;
            BuffIntelligence_HighEmotionality = 0;
            BuffStamina_HighEmotionality = 0;
            BuffMind_HighEmotionality = 0;

            BuffStrength_VoidExtraction = 0;
            BuffAgility_VoidExtraction = 0;
            BuffIntelligence_VoidExtraction = 0;
            BuffStamina_VoidExtraction = 0;
            BuffMind_VoidExtraction = 0;

            // s 後編追加
            BuffStrength_TranscendentWish = 0;
            BuffAgility_TranscendentWish = 0;
            BuffIntelligence_TranscendentWish = 0;
            BuffStamina_TranscendentWish = 0;
            BuffMind_TranscendentWish = 0;

            BuffStrength_Hiyaku_Kassei = 0;
            BuffAgility_Hiyaku_Kassei = 0;
            BuffIntelligence_Hiyaku_Kassei = 0;
            BuffStamina_Hiyaku_Kassei = 0;
            BuffMind_Hiyaku_Kassei = 0;
            // e 後編追加

            // [食事効果は戦闘終了後も継続される] 後編追加
            // buffStrength_Food = 0;
            // buffAgility_Food = 0;
            // buffIntelligence_Food = 0;
            // buffStamina_Food = 0;
            // buffMind_Food = 0;

            actionDecision = false; // 後編追加
            decisionTiming = 0; // 後編追加
            currentInstantPoint = 0; // 後編追加 // 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
            realTimeBattle = false; // 後編追加
            stackActivation = false; // 後編追加
            stackActivePlayer = null; // 後編追加
            stackTarget = null; // 後編追加
            stackPlayerAction = PlayerAction.None; // 後編追加
            stackCommandString = string.Empty; // 後編追加
            //shadowStackActivePlayer = null; // 後編追加
            //shadowStackTarget = null; // 後編追加
            //shadowStackPlayerAction = PlayerAction.None; // 後編追加
            //shadowStackCommandString = String.Empty; // 後編追加
            BuffNumber = 0; // 後編追加

            AmplifyPhysicalAttack = 0.0f; // 後編追加
            AmplifyPhysicalDefense = 0.0f; // 後編追加
            AmplifyMagicAttack = 0.0f; // 後編追加
            AmplifyMagicDefense = 0.0f; // 後編追加
            AmplifyBattleSpeed = 0.0f; // 後編追加
            AmplifyBattleResponse = 0.0f; // 後編追加
            AmplifyPotential = 0.0f; // 後編追加

            currentLifeCountValue = 0; // 後編追加

            reserveBattleCommand = String.Empty; // 後編追加
            nowExecActionFlag = false; // 後編追加

            if ((this.MainWeapon != null) && (this.MainWeapon.AfterBroken)) { brokenName = this.MainWeapon.Name; this.MainWeapon = null; }
            if ((this.SubWeapon != null) && (this.SubWeapon.AfterBroken)) { brokenName = this.SubWeapon.Name; this.SubWeapon = null; }
            if ((this.MainArmor != null) && (this.MainArmor.AfterBroken)) { brokenName = this.MainArmor.Name; this.MainArmor = null; }
            if ((this.Accessory != null) && (this.Accessory.AfterBroken)) { brokenName = this.Accessory.Name; this.Accessory = null; }
            if ((this.Accessory2 != null) && (this.Accessory2.AfterBroken)) { brokenName = this.Accessory2.Name; this.Accessory2 = null; }

            if (this.MainWeapon != null) { this.MainWeapon.CleanUpStatus(); }
            if (this.SubWeapon != null) { this.SubWeapon.CleanUpStatus(); }
            if (this.MainArmor != null) { this.MainArmor.CleanUpStatus(); }
            if (this.Accessory != null) { this.Accessory.CleanUpStatus(); }
            if (this.Accessory2 != null) { this.Accessory2.CleanUpStatus(); }
        }

        internal double AmplifyMagicByEquipment(double damage, TruthActionCommand.MagicType type)
        {
            List<ItemBackPack> equipList = new List<ItemBackPack>();
            if (this.MainWeapon != null) { equipList.Add(this.MainWeapon); }
            if (this.SubWeapon != null) { equipList.Add(this.SubWeapon); }
            if (this.MainArmor != null) { equipList.Add(this.MainArmor); }
            if (this.Accessory != null) { equipList.Add(this.Accessory); }
            if (this.Accessory2 != null) { equipList.Add(this.Accessory2); }

            if (TruthActionCommand.IsLight(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyLight > 0) { damage = damage * equipList[ii].AmplifyLight; }
                }
            }
            if (TruthActionCommand.IsShadow(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyShadow > 0) { damage = damage * equipList[ii].AmplifyShadow; }
                }
            }
            if (TruthActionCommand.IsFire(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyFire > 0) { damage = damage * equipList[ii].AmplifyFire; }
                }
            }
            if (TruthActionCommand.IsIce(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyIce > 0) { damage = damage * equipList[ii].AmplifyIce; }
                }
            }
            if (TruthActionCommand.IsForce(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyForce > 0) { damage = damage * equipList[ii].AmplifyForce; }
                }
            }
            if (TruthActionCommand.IsWill(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyWill > 0) { damage = damage * equipList[ii].AmplifyWill; }
                }
            }

            return damage;
        }

        internal void BuffCountUp()
        {
            // 静
            AbstractCountUpBuff(pbCounterAttack, ref CurrentCounterAttack); // 後編追加
            AbstractCountUpBuff(pbAntiStun, ref CurrentAntiStun);
            AbstractCountUpBuff(pbStanceOfDeath, ref CurrentStanceOfDeath);
            // 柔
            AbstractCountUpBuff(pbStanceOfFlow, ref CurrentStanceOfFlow);
            // 剛
            AbstractCountUpBuff(pbStanceOfStanding, ref CurrentStanceOfStanding); // 後編追加
            // 心眼
            AbstractCountUpBuff(pbTruthVision, ref CurrentTruthVision);
            AbstractCountUpBuff(pbHighEmotionality, ref CurrentHighEmotionality);
            AbstractCountUpBuff(pbStanceOfEyes, ref CurrentStanceOfEyes); // 後編編集
            AbstractCountUpBuff(pbPainfulInsanity, ref CurrentPainfulInsanity);
            // 無心
            AbstractCountUpBuff(pbNegate, ref CurrentNegate); // 後編追加
            AbstractCountUpBuff(pbVoidExtraction, ref CurrentVoidExtraction);
            AbstractCountUpBuff(pbNothingOfNothingness, ref CurrentNothingOfNothingness);

            // 聖
            AbstractCountUpBuff(pbProtection, ref CurrentProtection);
            AbstractCountUpBuff(pbSaintPower, ref CurrentSaintPower);
            AbstractCountUpBuff(pbGlory, ref CurrentGlory);
            // 闇
            AbstractCountUpBuff(pbShadowPact, ref CurrentShadowPact);
            AbstractCountUpBuff(pbBlackContract, ref CurrentBlackContract);
            AbstractCountUpBuff(pbBloodyVengeance, ref CurrentBloodyVengeance);
            //AbstractCountUpBuff(pbDamnation, ref CurrentDamnation); // 負の影響
            // 火
            AbstractCountUpBuff(pbFlameAura, ref CurrentFlameAura);
            AbstractCountUpBuff(pbHeatBoost, ref CurrentHeatBoost);
            AbstractCountUpBuff(pbImmortalRave, ref CurrentImmortalRave);
            // 水
            AbstractCountUpBuff(pbAbsorbWater, ref CurrentAbsorbWater);
            AbstractCountUpBuff(pbMirrorImage, ref CurrentMirrorImage);
            AbstractCountUpBuff(pbPromisedKnowledge, ref CurrentPromisedKnowledge);
            //AbstractCountUpBuff(pbAbsoluteZero, ref CurrentAbsoluteZero); // 負の影響
            // 理
            AbstractCountUpBuff(pbGaleWind, ref CurrentGaleWind);
            AbstractCountUpBuff(pbWordOfLife, ref CurrentWordOfLife);
            AbstractCountUpBuff(pbWordOfFortune, ref CurrentWordOfFortune);
            AbstractCountUpBuff(pbAetherDrive, ref CurrentAetherDrive);
            AbstractCountUpBuff(pbEternalPresence, ref CurrentEternalPresence);
            // 空
            AbstractCountUpBuff(pbRiseOfImage, ref CurrentRiseOfImage);
            AbstractCountUpBuff(pbDeflection, ref CurrentDeflection);
            AbstractCountUpBuff(pbOneImmunity, ref CurrentOneImmunity);
            AbstractCountUpBuff(pbTimeStop, ref CurrentTimeStop);

            // 聖＋闇
            AbstractCountUpBuff(pbPsychicTrance, ref CurrentPsychicTrance);
            AbstractCountUpBuff(pbBlindJustice, ref CurrentBlindJustice);
            AbstractCountUpBuff(pbTranscendentWish, ref CurrentTranscendentWish);
            // 聖＋火
            //AbstractCountUpBuff(pbFlashBlaze, ref CurrentFlashBlazeCount); // 負の影響
            // 聖＋水
            AbstractCountUpBuff(pbSkyShield, ref CurrentSkyShield);
            AbstractCountUpBuff(pbEverDroplet, ref CurrentEverDroplet);
            // 聖＋理
            AbstractCountUpBuff(pbHolyBreaker, ref CurrentHolyBreaker);
            AbstractCountUpBuff(pbExaltedField, ref CurrentExaltedField);
            AbstractCountUpBuff(pbHymnContract, ref CurrentHymnContract);
            // 聖＋空
            //AbstractCountUpBuff(pbStarLightning, ref CurrentStarLightning); // 負の影響
            // 闇＋火
            //AbstractCountUpBuff(pbBlackFire, ref CurrentBlackFire); // 負の影響
            //AbstractCountUpBuff(pbBlazingField, ref CurrentBlazingField); // 負の影響
            // 闇＋水
            // 闇＋理
            //AbstractCountUpBuff(pbWordOfMalice, ref CurrentWordOfMalice); // 負の影響
            AbstractCountUpBuff(pbSinFortune, ref CurrentSinFortune);
            // 闇＋空
            //AbstractCountUpBuff(pbDarkenField, ref CurrentDarkenField); // 負の影響
            AbstractCountUpBuff(pbEclipseEnd, ref CurrentEclipseEnd);
            // 火＋水
            AbstractCountUpBuff(pbFrozenAura, ref CurrentFrozenAura);
            // 火＋理
            //AbstractCountUpBuff(pbEnrageBlast, ref CurrentEnrageBlast); // 負の影響
            //AbstractCountUpBuff(pbSigilOfHomura, ref CurrentSigilOfHomura); // 負の影響
            // 火＋空
            //AbstractCountUpBuff(pbImmolate, ref CurrentImmolate); // 負の影響
            AbstractCountUpBuff(pbPhantasmalWind, ref CurrentPhantasmalWind);
            AbstractCountUpBuff(pbRedDragonWill, ref CurrentRedDragonWill);
            // 水＋理
            AbstractCountUpBuff(pbStaticBarrier, ref CurrentStaticBarrier);
            //AbstractCountUpBuff(pbAusterityMatrix, ref CurrentAusterityMatrix); // 負の影響
            // 水＋空
            AbstractCountUpBuff(pbBlueDragonWill, ref CurrentBlueDragonWill);
            // 理＋空
            AbstractCountUpBuff(pbSeventhMagic, ref CurrentSeventhMagic);
            AbstractCountUpBuff(pbParadoxImage, ref CurrentParadoxImage);
            // 動＋静
            AbstractCountUpBuff(pbStanceOfDouble, ref CurrentStanceOfDouble);
            // 動＋柔
            AbstractCountUpBuff(pbSwiftStep, ref CurrentSwiftStep);
            AbstractCountUpBuff(pbVigorSense, ref CurrentVigorSense);
            // 動＋剛
            AbstractCountUpBuff(pbRisingAura, ref CurrentRisingAura);
            // 動＋心眼
            //AbstractCountUpBuff(pbOnslaughtHit, ref CurrentOnslaughtHit); // 負の影響
            // 動＋無心
            AbstractCountUpBuff(pbSmoothingMove, ref CurrentSmoothingMove);
            AbstractCountUpBuff(pbAscensionAura, ref CurrentAscensionAura);
            // 静＋柔
            AbstractCountUpBuff(pbFutureVision, ref CurrentFutureVision);
            // 静＋剛
            AbstractCountUpBuff(pbReflexSpirit, ref CurrentReflexSpirit);
            // 静＋心眼
            //AbstractCountUpBuff(pbConcussiveHit, ref CurrentConcussiveHit); // 負の影響
            // 静＋無心
            AbstractCountUpBuff(pbTrustSilence, ref CurrentTrustSilence);
            // 柔＋剛
            AbstractCountUpBuff(pbStanceOfMystic, ref CurrentStanceOfMystic);
            // 柔＋心眼
            AbstractCountUpBuff(pbNourishSense, ref CurrentNourishSense);
            // 柔＋無心
            //AbstractCountUpBuff(pbImpulseHit, ref CurrentImpulseHit); // 負の影響
            // 剛＋心眼
            AbstractCountUpBuff(pbOneAuthority, ref CurrentOneAuthority);
            // 剛＋無心
            // 心眼＋無心

            // 武器特有 // 武器まで影響を及ぼさない
            //AbstractCountUpBuff(pbFeltus, ref CurrentFeltus);
            //AbstractCountUpBuff(pbJuzaPhantasmal, ref CurrentJuzaPhantasmal);
            //AbstractCountUpBuff(pbEternalFateRing, ref CurrentEternalFateRing);
            //AbstractCountUpBuff(pbLightServant, ref CurrentLightServant);
            //AbstractCountUpBuff(pbShadowServant, ref CurrentShadowServant);
            //AbstractCountUpBuff(pbAdilBlueBurn, ref CurrentAdilBlueBurn);
            //AbstractCountUpBuff(pbMazeCube, ref CurrentMazeCube);
            //AbstractCountUpBuff(pbShadowBible, ref CurrentShadowBible);
            //AbstractCountUpBuff(pbDetachmentOrb, ref CurrentDetachmentOrb);
            //AbstractCountUpBuff(pbDevilSummonerTome, ref CurrentDevilSummonerTome);
            //AbstractCountUpBuff(pbVoidHymnsonia, ref CurrentVoidHymnsonia);
            //AbstractCountUpBuff(pbSagePotionMini, ref CurrentSagePotionMini);
            //AbstractCountUpBuff(pbGenseiTaima, ref CurrentGenseiTaima);
            //AbstractCountUpBuff(pbShiningAether, ref CurrentShiningAether);
            //AbstractCountUpBuff(pbBlackElixir, ref CurrentBlackElixir);
            //AbstractCountUpBuff(pbElementalSeal, ref CurrentElementalSeal);
            //AbstractCountUpBuff(pbColoressAntidote, ref CurrentColoressAntidote);

            // 最終戦ライフカウント
            //AbstractCountUpBuff(pbLifeCount, ref CurrentLifeCountValue); カウントアップする対象ではないため、コメントアウト
            //AbstractCountUpBuff(pbChaoticSchema, ref CurrentChaoticSchema);  カウントアップする対象ではないため、コメントアウト
            
            // 正の影響効果
            AbstractCountUpBuff(pbBlinded, ref CurrentBlinded);
            // SpeedBoostはBattleEnemyフォーム側でマイナスさせます。
            // CurrentChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
            // CurrentPhysicalChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
            AbstractCountUpBuff(pbPhysicalAttackUp, ref CurrentPhysicalAttackUp);
            AbstractCountUpBuff(pbPhysicalDefenseUp, ref CurrentPhysicalDefenseUp);
            AbstractCountUpBuff(pbMagicAttackUp, ref CurrentMagicAttackUp);
            AbstractCountUpBuff(pbMagicDefenseUp, ref CurrentMagicDefenseUp);
            AbstractCountUpBuff(pbSpeedUp, ref CurrentSpeedUp);
            AbstractCountUpBuff(pbReactionUp, ref CurrentReactionUp);
            AbstractCountUpBuff(pbPotentialUp, ref CurrentPotentialUp);

            AbstractCountUpBuff(pbStrengthUp, ref CurrentStrengthUp);
            AbstractCountUpBuff(pbAgilityUp, ref CurrentAgilityUp);
            AbstractCountUpBuff(pbIntelligenceUp, ref CurrentIntelligenceUp);
            AbstractCountUpBuff(pbStaminaUp, ref CurrentStaminaUp);
            AbstractCountUpBuff(pbMindUp, ref CurrentMindUp);

            AbstractCountUpBuff(pbLightUp, ref CurrentLightUp);
            AbstractCountUpBuff(pbShadowUp, ref CurrentShadowUp);
            AbstractCountUpBuff(pbFireUp, ref CurrentFireUp);
            AbstractCountUpBuff(pbIceUp, ref CurrentIceUp);
            AbstractCountUpBuff(pbForceUp, ref CurrentForceUp);
            AbstractCountUpBuff(pbWillUp, ref CurrentWillUp);

            AbstractCountUpBuff(pbResistLightUp, ref CurrentResistLightUp);
            AbstractCountUpBuff(pbResistShadowUp, ref CurrentResistShadowUp);
            AbstractCountUpBuff(pbResistFireUp, ref CurrentResistFireUp);
            AbstractCountUpBuff(pbResistIceUp, ref CurrentResistIceUp);
            AbstractCountUpBuff(pbResistForceUp, ref CurrentResistForceUp);
            AbstractCountUpBuff(pbResistWillUp, ref CurrentResistWillUp);

            // 集中と断絶
            AbstractCountUpBuff(pbSyutyuDanzetsu, ref CurrentSyutyu_Danzetsu);

            // 循環と誓約(コレ自身は、【循環と誓約】効果対象外）
            AbstractCountUpBuff(pbJunkanSeiyaku, ref CurrentJunkan_Seiyaku);

            // 負の影響効果(【循環と誓約】効果対象外）
            //AbstractCountUpBuff(pbPreStunning, ref CurrentPreStunning);
            //AbstractCountUpBuff(pbStun, ref CurrentStunning);
            //AbstractCountUpBuff(pbSilence, ref CurrentSilence);
            //AbstractCountUpBuff(pbPoison, ref CurrentPoison);
            //AbstractCountUpBuff(pbTemptation, ref CurrentTemptation);
            //AbstractCountUpBuff(pbFrozen, ref CurrentFrozen);
            //AbstractCountUpBuff(pbParalyze, ref CurrentParalyze);
            //AbstractCountUpBuff(pbNoResurrection, ref CurrentNoResurrection);
            //AbstractCountUpBuff(pbSlip, ref CurrentSlip);
            //AbstractCountUpBuff(pbSlow, ref CurrentSlow);
            //AbstractCountUpBuff(pbNoGainLife, ref CurrentNoGainLife);
            //AbstractCountUpBuff(pbBlind, ref CurrentBlind);

            //AbstractCountUpBuff(pbPhysicalAttackDown, ref CurrentPhysicalAttackDown);
            //AbstractCountUpBuff(pbPhysicalDefenseDown, ref CurrentPhysicalDefenseDown);
            //AbstractCountUpBuff(pbMagicAttackDown, ref CurrentMagicAttackDown);
            //AbstractCountUpBuff(pbMagicDefenseDown, ref CurrentMagicDefenseDown);
            //AbstractCountUpBuff(pbSpeedDown, ref CurrentSpeedDown);
            //AbstractCountUpBuff(pbReactionDown, ref CurrentReactionDown);
            //AbstractCountUpBuff(pbPotentialDown, ref CurrentPotentialDown);

            // pbStrengthDown系が存在しない

            //AbstractCountUpBuff(pbLightDown, ref CurrentLightDown);
            //AbstractCountUpBuff(pbShadowDown, ref CurrentShadowDown);
            //AbstractCountUpBuff(pbFireDown, ref CurrentFireDown);
            //AbstractCountUpBuff(pbIceDown, ref CurrentIceDown);
            //AbstractCountUpBuff(pbForceDown, ref CurrentForceDown);
            //AbstractCountUpBuff(pbWillDown, ref CurrentWillDown);

            // pbResistLightDown系が存在しない

            //AbstractCountUpBuff(pbAfterReviveHalf, ref CurrentAfterReviveHalf);

            //AbstractCountUpBuff(pbFireDamage2, ref CurrentFireDamage2);

            //AbstractCountUpBuff(pbBlackMagic, ref CurrentBlackMagic);

            //AbstractCountUpBuff(pbChaosDesperate, ref CurrentChaosDesperate);

            //AbstractCountUpBuff(pbIchinaruHomura, ref CurrentIchinaruHomura);
            //AbstractCountUpBuff(pbAbyssFire, ref CurrentAbyssFire);
            //AbstractCountUpBuff(pbLightAndShadow, ref CurrentLightAndShadow);
            //AbstractCountUpBuff(pbEternalDroplet, ref CurrentEternalDroplet);
            //AbstractCountUpBuff(pbAusterityMatrixOmega, ref CurrentAusterityMatrixOmega);
            //AbstractCountUpBuff(pbVoiceOfAbyss, ref CurrentVoiceOfAbyss);
            //AbstractCountUpBuff(pbAbyssWill, ref CurrentAbyssWill);
            //AbstractCountUpBuff(pbTheAbyssWall, ref CurrentTheAbyssWall);
        }
    }
}

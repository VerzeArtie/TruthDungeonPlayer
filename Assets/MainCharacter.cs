using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
        protected string fullName = string.Empty;
        protected string name = string.Empty;
        protected int baseStrength = 5;
        protected int baseAgility = 3;
        protected int baseIntelligence = 2;
        protected int baseStamina = 4;
        protected int baseMind = 3;

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
        public string Name
        {
            get { return name; }
            set { name = value; }
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
        public JobClass.Job Job { get; set; }

        // buff up(+)
        public int BuffStrength_Food = 0;
        public int BuffAgility_Food = 0;
        public int BuffIntelligence_Food = 0;
        public int BuffStamina_Food = 0;
        public int BuffMind_Food = 0;

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
        protected bool alreadyPlayArchetype = false; // 後編追加
        public bool AlreadyPlayArchetype
        {
            get { return alreadyPlayArchetype; }
            set { alreadyPlayArchetype = value; }
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
        public int BuffStrength_BloodyVengeance = 0;
        public int BuffAgility_HeatBoost = 0;
        public int BuffIntelligence_PromisedKnowledge = 0;
        public int BuffStamina_Unknown = 0; // [将来]：StaminaのBUFFUPスペルを構築
        public int BuffMind_RiseOfImage = 0;

        public int BuffStrength_VoidExtraction = 0;
        public int BuffAgility_VoidExtraction = 0;
        public int BuffIntelligence_VoidExtraction = 0;
        public int BuffStamina_VoidExtraction = 0; // [将来]：StaminaのBuffUPスキルを構築
        public int BuffMind_VoidExtraction = 0;

        public int BuffStrength_HighEmotionality = 0;
        public int BuffAgility_HighEmotionality = 0;
        public int BuffIntelligence_HighEmotionality = 0;
        public int BuffStamina_HighEmotionality = 0; // [将来]：StaminaのBuffUPスキルを構築
        public int BuffMind_HighEmotionality = 0;

        // s 後編追加
        public int BuffStrength_TranscendentWish = 0;
        public int BuffAgility_TranscendentWish = 0;
        public int BuffIntelligence_TranscendentWish = 0;
        public int BuffStamina_TranscendentWish = 0;
        public int BuffMind_TranscendentWish = 0;
        // e 後編追加

        // s 後編追加
        public int BuffStrength_Hiyaku_Kassei = 0;
        public int BuffAgility_Hiyaku_Kassei = 0;
        public int BuffIntelligence_Hiyaku_Kassei = 0;
        public int BuffStamina_Hiyaku_Kassei = 0;
        public int BuffMind_Hiyaku_Kassei = 0;
        // e 後編追加

        public ItemBackPack iw = null;
        public ItemBackPack sw = null;
        public ItemBackPack ia = null;
        public ItemBackPack accessory = null;
        public ItemBackPack accessory2 = null;

        public int buffStrength_MainWeapon = 0;
        public int buffAgility_MainWeapon = 0;
        public int buffIntelligence_MainWeapon = 0;
        public int buffStamina_MainWeapon = 0;
        public int buffMind_MainWeapon = 0;

        public int buffStrength_SubWeapon = 0;
        public int buffAgility_SubWeapon = 0;
        public int buffIntelligence_SubWeapon = 0;
        public int buffStamina_SubWeapon = 0;
        public int buffMind_SubWeapon = 0;

        public int buffStrength_Armor = 0;
        public int buffAgility_Armor = 0;
        public int buffIntelligence_Armor = 0;
        public int buffStamina_Armor = 0;
        public int buffMind_Armor = 0;

        public int buffStrength_Accessory = 0;
        public int buffAgility_Accessory = 0;
        public int buffIntelligence_Accessory = 0;
        public int buffStamina_Accessory = 0;
        public int buffMind_Accessory = 0;

        public int buffStrength_Accessory2 = 0;
        public int buffAgility_Accessory2 = 0;
        public int buffIntelligence_Accessory2 = 0;
        public int buffStamina_Accessory2 = 0;
        public int buffMind_Accessory2 = 0;

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
        public GameObject BuffPanel = null;
        public int BuffNumber = 0;
        public TruthImage[] BuffElement = null; // 「警告」：後編ではこれでBUFF並びを整列する。最終的には個別BUFFのTruthImageは全て不要になる。
        //public List<Image> BuffElement;


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
        public Image pbTargetTarget = null; // todo (ターゲットのターゲットを矢印ではなく画像表示で表現できないか？)

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

        public double MagicAttackValue { get; set; }

        public string CurrentCommand { get; set; }

        //	public int CurrentProtection { get; set; }
        //	public int CurrentShadowPact { get; set; }
        //	public int CurrentFlameAura { get; set; }
        //	public int CurrentWordOfLife { get; set; }
        //	public int CurrentDeflection { get; set; }
        //	public int CurrentTruthVision { get; set; }

        // base command (automatic)
        public Button btnBaseCommand = null;

        // battle command list (manual)
       	public string[] BattleActionCommandList = new string[Database.BATTLE_COMMAND_MAX];

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
        public Button MainObjectButton = null;
        public Text labelName = null;
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


        public Text DamageLabel = null;
        public Text CriticalLabel = null;

        // gui action button list
        public List<Button> ActionButtonList = new List<Button>();
        public Button ActionButton1 = null;
        public Button ActionButton2 = null;
        public Button ActionButton3 = null;
        public Button ActionButton4 = null;
        public Button ActionButton5 = null;
        public Button ActionButton6 = null;
        public Button ActionButton7 = null;
        public Button ActionButton8 = null;
        public Button ActionButton9 = null;

        // battle target
        public MainCharacter Target = null; // 敵ターゲット
        public MainCharacter Target2 = null; // 味方ターゲット


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
                return this.Strength;
                // todo
                //			this.buffStrength_BloodyVengeance +
                //				this.buffStrength_HighEmotionality +
                //					this.buffStrength_VoidExtraction + 
                //					this.buffStrength_TranscendentWish + // 後編追加
                //					this.buffStrength_Hiyaku_Kassei + // c 後編追加
                //					this.buffStrength_MainWeapon + // 後編追加
                //					this.buffStrength_SubWeapon + // 後編追加
                //					this.buffStrength_Armor +  // 後編追加
                //					this.buffStrength_Accessory +
                //					this.buffStrength_Accessory2 + // 後編追加
                //					this.buffStrength_Food + // 後編追加
                //					this.CurrentStrengthUpValue; // 後編追加    
            } // c 後編追加
        }
        public int TotalAgility
        {
            get
            {
                return this.Agility;
                // todo
                //			this.buffAgility_HeatBoost +
                //				this.buffAgility_HighEmotionality +
                //					this.buffAgility_VoidExtraction +
                //					this.buffAgility_TranscendentWish + // 後編追加
                //					this.buffAgility_Hiyaku_Kassei + // c 後編追加
                //					this.buffAgility_MainWeapon + // 後編追加
                //					this.buffAgility_SubWeapon + // 後編追加
                //					this.buffAgility_Armor + // 後編追加
                //					this.buffAgility_Accessory +
                //					this.buffAgility_Accessory2 + // 後編追加
                //					this.buffAgility_Food + // 後編追加
                //					this.CurrentAgilityUpValue; } // c 後編追加
            }
        }
        public int TotalIntelligence
        {
            get { return this.Intelligence; }
            // todo
            //			this.buffIntelligence_PromisedKnowledge +
            //				this.buffIntelligence_HighEmotionality +
            //					this.buffIntelligence_VoidExtraction +
            //					this.buffIntelligence_TranscendentWish + // 後編追加
            //					this.buffIntelligence_Hiyaku_Kassei + // c 後編追加
            //					this.buffIntelligence_MainWeapon + // 後編追加
            //					this.buffIntelligence_SubWeapon + // 後編追加
            //					this.buffIntelligence_Armor + // 後編追加
            //					this.buffIntelligence_Accessory +
            //					this.buffIntelligence_Accessory2 + // 後編追加
            //					this.buffIntelligence_Food + // 後編追加
            //					this.CurrentIntelligenceUpValue; } // c 後編追加
        }
        public int TotalStamina
        {
            get { return this.Stamina; }
            // todo
            //			this.buffStamina_Unknown +
            //				this.buffStamina_HighEmotionality +
            //					this.buffStamina_VoidExtraction +
            //					this.buffStamina_TranscendentWish + // 後編追加
            //					this.buffStamina_Hiyaku_Kassei + // c 後編追加
            //					this.buffStamina_MainWeapon + // 後編追加
            //					this.buffStamina_SubWeapon + // 後編追加
            //					this.buffStamina_Armor + // 後編追加
            //					this.buffStamina_Accessory +
            //					this.buffStamina_Accessory2 + // 後編追加
            //					this.buffStamina_Food + // 後編追加
            //					this.CurrentStaminaUpValue; } // c 後編追加
        }
        public int TotalMind
        {
            get
            {
                return this.Mind;
                // todo
                //				this.buffMind_RiseOfImage +
                //					this.buffMind_HighEmotionality +
                //					this.buffMind_VoidExtraction +
                //					this.buffMind_TranscendentWish + // 後編追加
                //					this.buffMind_Hiyaku_Kassei + // c 後編追加
                //					this.buffMind_MainWeapon + // 後編追加
                //					this.buffMind_SubWeapon + // 後編追加
                //					this.buffMind_Armor + // 後編追加
                //					this.buffMind_Accessory +
                //					this.buffMind_Accessory2 + // 後編追加
                //					this.buffMind_Food + // 後編追加
                //					this.CurrentMindUpValue + // c 後編追加
                //					this.CurrentFeltusValue * (int)(PrimaryLogic.FeltusValue(this)); // 後編追加
            }
        }

        // Standardはベース値＋常在型BUFFUP＋食事常在BUFFUPの合計値
        public int StandardStrength
        {
            get { return this.baseStrength + this.buffStrength_MainWeapon + this.buffStrength_Accessory + this.buffStrength_Accessory2 + this.BuffStrength_Food; } // c 後編追加
        }
        public int StandardAgility
        {
            get { return this.baseAgility + this.buffAgility_MainWeapon + this.buffAgility_Accessory + this.buffAgility_Accessory2 + this.BuffAgility_Food; } // c 後編追加
        }
        public int StandardIntelligence
        {
            get { return this.baseIntelligence + this.buffIntelligence_MainWeapon + this.buffIntelligence_Accessory + this.buffIntelligence_Accessory2 + this.BuffIntelligence_Food; } // c 後編追加
        }
        public int StandardStamina
        {
            get { return this.baseStamina + this.buffStamina_MainWeapon + this.buffStamina_Accessory + this.buffStamina_Accessory2 + this.BuffStamina_Food; } // c 後編追加
        }
        public int StandardMind
        {
            get { return this.baseMind + this.buffMind_MainWeapon + this.buffMind_Accessory + this.buffMind_Accessory2 + this.BuffMind_Food; } // c 後編追加
        }



        public void ActivateCharacter()
        {
            if (this.labelName != null) this.labelName.gameObject.SetActive(true);
            if (this.labelCurrentLifePoint != null) this.labelCurrentLifePoint.gameObject.SetActive(true);
            if (this.labelCurrentSkillPoint != null) { this.labelCurrentSkillPoint.gameObject.SetActive(true); }
            if (this.labelCurrentManaPoint != null) { this.labelCurrentManaPoint.gameObject.SetActive(true); }
            if (this.labelCurrentInstantPoint != null) { this.labelCurrentInstantPoint.gameObject.SetActive(true); }
            if (this.labelCurrentSpecialInstant != null) { this.labelCurrentSpecialInstant.gameObject.SetActive(true); }
            if (this.ActionButton1 != null) this.ActionButton1.gameObject.SetActive(true);
            if (this.ActionButton2 != null) this.ActionButton2.gameObject.SetActive(true);
            if (this.ActionButton3 != null) this.ActionButton3.gameObject.SetActive(true);
            if (this.ActionButton4 != null) this.ActionButton4.gameObject.SetActive(true);
            if (this.ActionButton5 != null) this.ActionButton5.gameObject.SetActive(true);
            if (this.ActionButton6 != null) this.ActionButton6.gameObject.SetActive(true);
            if (this.ActionButton7 != null) this.ActionButton7.gameObject.SetActive(true);
            if (this.ActionButton8 != null) this.ActionButton8.gameObject.SetActive(true);
            if (this.ActionButton9 != null) this.ActionButton9.gameObject.SetActive(true);
            if (this.ActionLabel != null) this.ActionLabel.gameObject.SetActive(true);
            if (this.MainObjectButton != null) this.MainObjectButton.gameObject.SetActive(true);
            if (this.pbTargetTarget != null) this.pbTargetTarget.gameObject.SetActive(true);
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
            if (this.ActionButton1 != null) this.ActionButton1.gameObject.SetActive(false);
            if (this.ActionButton2 != null) this.ActionButton2.gameObject.SetActive(false);
            if (this.ActionButton3 != null) this.ActionButton3.gameObject.SetActive(false);
            if (this.ActionButton4 != null) this.ActionButton4.gameObject.SetActive(false);
            if (this.ActionButton5 != null) this.ActionButton5.gameObject.SetActive(false);
            if (this.ActionButton6 != null) this.ActionButton6.gameObject.SetActive(false);
            if (this.ActionButton7 != null) this.ActionButton7.gameObject.SetActive(false);
            if (this.ActionButton8 != null) this.ActionButton8.gameObject.SetActive(false);
            if (this.ActionButton9 != null) this.ActionButton9.gameObject.SetActive(false);
            if (this.ActionLabel != null) this.ActionLabel.gameObject.SetActive(false);
            if (this.MainObjectButton != null) this.MainObjectButton.gameObject.SetActive(false);
            if (this.pbTargetTarget != null) this.pbTargetTarget.gameObject.SetActive(false);
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
                // todo
                //			this.MainObjectButton.BackColor = Color.DarkSlateGray;
            }
            // todo
            //		if (this.pbTargetTarget != null)
            //		{
            //			this.pbTargetTarget.BackColor = Color.DarkSlateGray;
            //		}
            if (this.labelName != null) this.labelName.color = Color.red;
            if (this.labelCurrentLifePoint != null) this.labelCurrentLifePoint.color = Color.red;
        }

        public void ResurrectPlayer(int life)
        {
            if (this.CurrentNoResurrection <= 0)
            {
                this.CurrentLife = life;
                this.Dead = false;
                // todo
                //			if (this.MainObjectButton != null)
                //			{
                //				this.MainObjectButton.Enabled = true;
                //				this.MainObjectButton.BackColor = this.MainColor;
                //			}
                //			if (this.pbTargetTarget != null)
                //			{
                //				this.pbTargetTarget.BackColor = this.MainColor;
                //			}
                if (this.labelName != null) { this.labelName.color = Color.black; }
                if (this.labelCurrentLifePoint != null) { this.labelCurrentLifePoint.color = Color.black; this.labelCurrentLifePoint.text = CurrentLife.ToString(); }
            }
        }

        public string GetCharacterSentence(int sentenceNumber)
        {
            if (this.Name == "ラナ")
            {
                switch (sentenceNumber)
                {
                    case 1001: // Home Town 1 コミュニケーション済で、休む前のアイン一人を対象
                        return this.Name + "：今日はもう休んで、明日に備えたら？";
                    case 1002: // Home Town 2 コミュニケーション済で、休んだ後のアイン一人を対象
                        return this.Name + "：ホラホラ、とっとと行って来い♪";
                    case 1003: // Home Town 1 コミュニケーション済で、休む前のアイン・ラナ２人を対象
                        return this.Name + "：じゃ、私は一旦戻るとするわね。明日に備えて休みましょ。";
                    case 1004: // Home Town 2 コミュニケーション済で、休んだ後のアイン・ラナ２人を対象
                        return this.Name + "：準備が出来たら、とっとと行くわよ♪";
                }
            }
            #region "ガンツ"
            else if (this.name == "ガンツ")
            {
                switch (sentenceNumber)
                {
                    case 3000: // 店に入った時の台詞
                        return this.name + "：ゆっくり見ていくがいい。";
                    case 3001: // 支払い要求時
                        return this.name + "：{0}は{1}Goldだ。買うかね？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：おや、荷物がいっぱいだよ。手持ちを減らしてからまた来なさい。";
                    case 3003: // 購入完了時
                        return this.name + "：はいよ、まいどあり。";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：すまない、こちらも商売でな。後{0}Gold必要だ。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他のも見ていくがいい。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：すまないが、それは買い取れん。";
                    case 3007: // アイテム売却時
                        return this.name + "：ふむ、{0}だな。{1}Goldで買い取ろうか。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：ふむ・・・良い出来具合のアクセサリだ。{0}Goldだが、本当に買い取って良いのか？";
                    case 3009: // 武具店を出る時
                        return this.name + "：またいつでも寄りなさい。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return "";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：ふむ、今ここで装備していくかね？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：現在の装備品を売却するかね？{0}は{1}Goldで買い取ろう。";
                    case 3013: // 両手持ち装備をした後、サブ武器を売却せず、手元に残そうとして、バックパックがいっぱいの時
                        return this.name + "：荷物がいっぱいのようだな。{0}はハンナの宿屋倉庫に後で送っておこう。";
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
                int nextValue = 300;
                int recursion = 0;
                for (int ii = 0; ii < this.Level; ii++)
                {
                    nextValue += recursion;
                    recursion = 300 + (100 * ii);
                }
                return nextValue;
            }
        }


        public int CurrentEternalFateRingValue
        {
            get { return currentEternalFateRingValue; }
            set { if (value <= 10) { currentEternalFateRingValue = value; } }
        }
        public int CurrentEternalFateRing { get; set; }

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
            // todo
            //else
            //{
            //    pbData.Invalidate();
            //}
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
                    //picture.Invalidate();
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
                // todo
                //if (ClearBeforeAction == true)
                //{
                //    this.beforePA = PA;
                //    this.beforeUsingItem = currentUsingItem;
                //    this.beforeSkillName = currentSkillName;
                //    this.beforeSpellName = currentSpellName;
                //    this.beforeArchetypeName = currentArchetypeName; // 後編追加
                //    this.beforeTarget = target;
                //    this.beforeTarget2 = target2; // 後編追加
                //    // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
                //}
            }

            // todo
            //if (ClearActionInfo)
            //{
            //    CurrentUsingItem = string.Empty;
            //    CurrentSkillName = string.Empty;
            //    CurrentSpellName = string.Empty;
            //    CurrentArchetypeName = string.Empty; // 後編追加
            //    PA = PlayerAction.None;
            //    target = null;
            //    target2 = null; // 後編追加
            //}

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
                    // todo
                    //AbstractCountDownBuff(pbCounterAttack, ref currentCounterAttack);
                }
                // e 後編追加

                // todo
                //    AbstractCountDownBuff(pbAntiStun, ref currentAntiStun);
                //    AbstractCountDownBuff(pbStanceOfDeath, ref currentStanceOfDeath);

                //    // 柔
                //    AbstractCountDownBuff(pbStanceOfFlow, ref currentStanceOfFlow);
                //    // 剛
                //    AbstractCountDownBuff(pbStanceOfStanding, ref currentStanceOfStanding); // 後編編集
                //    // 心眼
                //    AbstractCountDownBuff(pbTruthVision, ref currentTruthVision);
                //    AbstractCountDownBuff(pbHighEmotionality, ref currentHighEmotionality, ref buffStrength_HighEmotionality, ref buffAgility_HighEmotionality, ref buffIntelligence_HighEmotionality, ref buffStamina_HighEmotionality, ref buffMind_HighEmotionality);
                //    AbstractCountDownBuff(pbStanceOfEyes, ref currentStanceOfEyes); // 後編編集
                //    AbstractCountDownBuff(pbPainfulInsanity, ref currentPainfulInsanity);
                //    // 無心
                //    AbstractCountDownBuff(pbNegate, ref currentNegate); // 後編編集
                //    AbstractCountDownBuff(pbVoidExtraction, ref currentVoidExtraction, ref buffStrength_VoidExtraction, ref buffAgility_VoidExtraction, ref buffIntelligence_VoidExtraction, ref buffStamina_VoidExtraction, ref buffMind_VoidExtraction);
                //    AbstractCountDownBuff(pbNothingOfNothingness, ref currentNothingOfNothingness);

                //    // 聖
                //    AbstractCountDownBuff(pbProtection, ref currentProtection);
                //    AbstractCountDownBuff(pbSaintPower, ref currentSaintPower);
                //    AbstractCountDownBuff(pbGlory, ref currentGlory);
                //    // 闇
                //    AbstractCountDownBuff(pbShadowPact, ref currentShadowPact);
                //    AbstractCountDownBuff(pbBlackContract, ref currentBlackContract);
                //    AbstractCountDownBuff(pbBloodyVengeance, ref currentBloodyVengeance, ref buffStrength_BloodyVengeance);
                //    AbstractCountDownBuff(pbDamnation, ref currentDamnation);                
                //    // 火
                //    AbstractCountDownBuff(pbFlameAura, ref currentFlameAura);
                //    AbstractCountDownBuff(pbHeatBoost, ref currentHeatBoost, ref buffAgility_HeatBoost);
                //    AbstractCountDownBuff(pbImmortalRave, ref currentImmortalRave);
                //    // 水
                //    AbstractCountDownBuff(pbAbsorbWater, ref currentAbsorbWater);
                //    AbstractCountDownBuff(pbMirrorImage, ref currentMirrorImage);
                //    AbstractCountDownBuff(pbPromisedKnowledge, ref currentPromisedKnowledge, ref buffIntelligence_PromisedKnowledge);
                //    AbstractCountDownBuff(pbAbsoluteZero, ref currentAbsoluteZero);
                //    // 理
                    AbstractCountDownBuff(pbGaleWind, ref CurrentGaleWind);
                //    AbstractCountDownBuff(pbWordOfLife, ref currentWordOfLife);
                //    AbstractCountDownBuff(pbWordOfFortune, ref currentWordOfFortune);
                //    AbstractCountDownBuff(pbAetherDrive, ref currentAetherDrive);
                //    AbstractCountDownBuff(pbEternalPresence, ref currentEternalPresence);
                //    // 空
                //    AbstractCountDownBuff(pbRiseOfImage, ref currentRiseOfImage, ref buffMind_RiseOfImage);
                //    AbstractCountDownBuff(pbDeflection, ref currentDeflection);
                //    AbstractCountDownBuff(pbOneImmunity, ref currentOneImmunity);
                //    AbstractCountDownBuff(pbTimeStop, ref currentTimeStop);

                //    // 聖＋闇
                //    AbstractCountDownBuff(pbPsychicTrance, ref currentPsychicTrance);
                //    AbstractCountDownBuff(pbBlindJustice, ref currentBlindJustice);
                //    AbstractCountDownBuff(pbTranscendentWish, ref currentTranscendentWish, ref buffStrength_TranscendentWish, ref buffAgility_TranscendentWish, ref buffIntelligence_TranscendentWish, ref buffStamina_TranscendentWish, ref buffMind_TranscendentWish, ref deadSignForTranscendentWish);
                //    // 聖＋火
                //    AbstractCountDownBuff(pbFlashBlaze, ref currentFlashBlazeCount);
                //    // 聖＋水
                //    AbstractCountDownBuff(pbSkyShield, ref currentSkyShield, ref currentSkyShieldValue);
                //    AbstractCountDownBuff(pbEverDroplet, ref currentEverDroplet);
                //    // 聖＋理
                //    AbstractCountDownBuff(pbHolyBreaker, ref currentHolyBreaker);
                //    AbstractCountDownBuff(pbExaltedField, ref currentExaltedField);
                //    AbstractCountDownBuff(pbHymnContract, ref currentHymnContract);
                //    // 聖＋空
                //    AbstractCountDownBuff(pbStarLightning, ref currentStarLightning);
                //    // 闇＋火
                //    AbstractCountDownBuff(pbBlackFire, ref currentBlackFire);
                //    AbstractCountDownBuff(pbBlazingField, ref currentBlazingField, ref currentBlazingFieldFactor);
                //    // 闇＋水
                //    currentDeepMirror = false;
                //    // 闇＋理
                //    AbstractCountDownBuff(pbWordOfMalice, ref currentWordOfMalice);
                //    AbstractCountDownBuff(pbSinFortune, ref currentSinFortune);
                //    // 闇＋空
                //    AbstractCountDownBuff(pbDarkenField, ref currentDarkenField);
                //    AbstractCountDownBuff(pbEclipseEnd, ref currentEclipseEnd);
                //    // 火＋水
                //    AbstractCountDownBuff(pbFrozenAura, ref currentFrozenAura);
                //    // 火＋理
                //    AbstractCountDownBuff(pbEnrageBlast, ref currentEnrageBlast);
                //    AbstractCountDownBuff(pbSigilOfHomura, ref currentSigilOfHomura);
                //    // 火＋空
                //    AbstractCountDownBuff(pbImmolate, ref currentImmolate);
                //    AbstractCountDownBuff(pbPhantasmalWind, ref currentPhantasmalWind);
                //    AbstractCountDownBuff(pbRedDragonWill, ref currentRedDragonWill);
                //    // 水＋理
                //    AbstractCountDownBuff(pbStaticBarrier, ref currentStaticBarrier, ref currentStaticBarrierValue);
                //    AbstractCountDownBuff(pbAusterityMatrix, ref currentAusterityMatrix);
                //    // 水＋空
                //    AbstractCountDownBuff(pbBlueDragonWill, ref currentBlueDragonWill);
                //    // 理＋空
                //    AbstractCountDownBuff(pbSeventhMagic, ref currentSeventhMagic);
                //    AbstractCountDownBuff(pbParadoxImage, ref currentParadoxImage);
                //    // 動＋静
                //    AbstractCountDownBuff(pbStanceOfDouble, ref currentStanceOfDouble);
                //    // 動＋柔
                //    AbstractCountDownBuff(pbSwiftStep, ref currentSwiftStep);
                //    AbstractCountDownBuff(pbVigorSense, ref currentVigorSense);
                //    // 動＋剛
                //    AbstractCountDownBuff(pbRisingAura, ref currentRisingAura);
                //    // 動＋心眼
                //    AbstractCountDownBuff(pbOnslaughtHit, ref currentOnslaughtHit, ref currentOnslaughtHitValue);
                //    // 動＋無心
                //    AbstractCountDownBuff(pbSmoothingMove, ref currentSmoothingMove);
                //    AbstractCountDownBuff(pbAscensionAura, ref currentAscensionAura);
                //    // 静＋柔
                //    AbstractCountDownBuff(pbFutureVision, ref currentFutureVision);
                //    // 静＋剛
                //    AbstractCountDownBuff(pbReflexSpirit, ref currentReflexSpirit);
                //    // 静＋心眼
                //    AbstractCountDownBuff(pbConcussiveHit, ref currentConcussiveHit, ref currentConcussiveHitValue);
                //    // 静＋無心
                //    AbstractCountDownBuff(pbTrustSilence, ref currentTrustSilence);
                //    // 柔＋剛
                //    AbstractCountDownBuff(pbStanceOfMystic, ref currentStanceOfMystic, ref currentStanceOfMysticValue);
                //    // 柔＋心眼
                //    AbstractCountDownBuff(pbNourishSense, ref currentNourishSense);
                //    // 柔＋無心
                //    AbstractCountDownBuff(pbImpulseHit, ref currentImpulseHit, ref currentImpulseHitValue);
                //    // 剛＋心眼
                //    AbstractCountDownBuff(pbOneAuthority, ref currentOneAuthority);
                //    // 剛＋無心
                //    currentHardestParry = false;
                //    // 心眼＋無心
                //    currentStanceOfSuddenness = false;

                //    // 武器特有
                //    AbstractCountDownBuff(pbFeltus, ref currentFeltus, ref currentFeltusValue);
                //    AbstractCountDownBuff(pbJuzaPhantasmal, ref currentJuzaPhantasmal, ref currentJuzaPhantasmalValue);
                //    AbstractCountDownBuff(pbEternalFateRing, ref currentEternalFateRing, ref currentEternalFateRingValue);
                //    AbstractCountDownBuff(pbLightServant, ref currentLightServant, ref currentLightServantValue);
                //    AbstractCountDownBuff(pbShadowServant, ref currentShadowServant, ref currentShadowServantValue);
                //    AbstractCountDownBuff(pbAdilBlueBurn, ref currentAdilBlueBurn, ref currentAdilBlueBurnValue);
                //    AbstractCountDownBuff(pbMazeCube, ref currentMazeCube, ref currentMazeCubeValue);
                //    AbstractCountDownBuff(pbShadowBible, ref currentShadowBible);
                //    AbstractCountDownBuff(pbDetachmentOrb, ref currentDetachmentOrb);
                //    AbstractCountDownBuff(pbDevilSummonerTome, ref currentDevilSummonerTome);
                //    AbstractCountDownBuff(pbVoidHymnsonia, ref currentVoidHymnsonia);
                //    AbstractCountDownBuff(pbSagePotionMini, ref currentSagePotionMini);
                //    AbstractCountDownBuff(pbGenseiTaima, ref currentGenseiTaima);
                //    AbstractCountDownBuff(pbShiningAether, ref currentShiningAether);
                //    AbstractCountDownBuff(pbBlackElixir, ref currentBlackElixir, ref currentBlackElixirValue);
                //    AbstractCountDownBuff(pbElementalSeal, ref currentElementalSeal);
                //    AbstractCountDownBuff(pbColoressAntidote, ref currentColoressAntidote);

                //    // 最終戦ライフカウント
                //    //AbstractCountDownBuff(pbLifeCount, ref currentLifeCount, ref currentLifeCountValue); // ターンのクリーンナップでカウントダウンはしない
                //    AbstractCountDownBuff(pbChaoticSchema, ref currentChaoticSchema);

                //    // 正の影響効果
                //    AbstractCountDownBuff(pbBlinded, ref currentBlinded);
                //    // SpeedBoostはBattleEnemyフォーム側でマイナスさせます。
                //    // CurrentChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                //    // CurrentPhysicalChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                //    AbstractCountDownBuff(pbPhysicalAttackUp, ref currentPhysicalAttackUp, ref currentPhysicalAttackUpValue);
                //    AbstractCountDownBuff(pbPhysicalDefenseUp, ref currentPhysicalDefenseUp, ref currentPhysicalDefenseUpValue);
                //    AbstractCountDownBuff(pbMagicAttackUp, ref currentMagicAttackUp, ref currentMagicAttackUpValue);
                //    AbstractCountDownBuff(pbMagicDefenseUp, ref currentMagicDefenseUp, ref currentMagicDefenseUpValue);
                //    AbstractCountDownBuff(pbSpeedUp, ref currentSpeedUp, ref currentSpeedUpValue);
                //    AbstractCountDownBuff(pbReactionUp, ref currentReactionUp, ref currentReactionUpValue);
                //    AbstractCountDownBuff(pbPotentialUp, ref currentPotentialUp, ref currentPotentialUpValue);

                //    AbstractCountDownBuff(pbStrengthUp, ref currentStrengthUp, ref currentStrengthUpValue);
                //    AbstractCountDownBuff(pbAgilityUp, ref currentAgilityUp, ref currentAgilityUpValue);
                //    AbstractCountDownBuff(pbIntelligenceUp, ref currentIntelligenceUp, ref currentIntelligenceUpValue);
                //    AbstractCountDownBuff(pbStaminaUp, ref currentStaminaUp, ref currentStaminaUpValue);
                //    AbstractCountDownBuff(pbMindUp, ref currentMindUp, ref currentMindUpValue);

                //    AbstractCountDownBuff(pbLightUp, ref currentLightUp, ref currentLightUpValue);
                //    AbstractCountDownBuff(pbShadowUp, ref currentShadowUp, ref currentShadowUpValue);
                //    AbstractCountDownBuff(pbFireUp, ref currentFireUp, ref currentFireUpValue);
                //    AbstractCountDownBuff(pbIceUp, ref currentIceUp, ref currentIceUpValue);
                //    AbstractCountDownBuff(pbForceUp, ref currentForceUp, ref currentForceUpValue);
                //    AbstractCountDownBuff(pbWillUp, ref currentWillUp, ref currentWillUpValue);

                //    AbstractCountDownBuff(pbResistLightUp, ref currentResistLightUp, ref currentResistLightUpValue);
                //    AbstractCountDownBuff(pbResistShadowUp, ref currentResistShadowUp, ref currentResistShadowUpValue);
                //    AbstractCountDownBuff(pbResistFireUp, ref currentResistFireUp, ref currentResistFireUpValue);
                //    AbstractCountDownBuff(pbResistIceUp, ref currentResistIceUp, ref currentResistIceUpValue);
                //    AbstractCountDownBuff(pbResistForceUp, ref currentResistForceUp, ref currentResistForceUpValue);
                //    AbstractCountDownBuff(pbResistWillUp, ref currentResistWillUp, ref currentResistWillUpValue);

                //    // 集中と断絶
                //    AbstractCountDownBuff(pbSyutyuDanzetsu, ref currentSyutyu_Danzetsu);               
                //}

                //// 循環と誓約(コレ自身は、【循環と誓約】効果対象外）
                //AbstractCountDownBuff(pbJunkanSeiyaku, ref currentJunkan_Seiyaku);

                //// 負の影響効果(【循環と誓約】効果対象外）
                //AbstractCountDownBuff(pbPreStunning, ref currentPreStunning);
                //AbstractCountDownBuff(pbStun, ref currentStunning);
                //AbstractCountDownBuff(pbSilence, ref currentSilence);
                //AbstractCountDownBuff(pbPoison, ref currentPoison, ref currentPoisonValue);
                //AbstractCountDownBuff(pbTemptation, ref currentTemptation);
                //AbstractCountDownBuff(pbFrozen, ref currentFrozen);
                //AbstractCountDownBuff(pbParalyze, ref currentParalyze);
                //AbstractCountDownBuff(pbNoResurrection, ref currentNoResurrection);
                //// s 後編追加
                //AbstractCountDownBuff(pbSlip, ref currentSlip);
                //AbstractCountDownBuff(pbSlow, ref currentSlow);
                //AbstractCountDownBuff(pbNoGainLife, ref currentNoGainLife);
                //AbstractCountDownBuff(pbBlind, ref currentBlind);

                //AbstractCountDownBuff(pbPhysicalAttackDown, ref currentPhysicalAttackDown, ref currentPhysicalAttackDownValue);
                //AbstractCountDownBuff(pbPhysicalDefenseDown, ref currentPhysicalDefenseDown, ref currentPhysicalDefenseDownValue);
                //AbstractCountDownBuff(pbMagicAttackDown, ref currentMagicAttackDown, ref currentMagicAttackDownValue);
                //AbstractCountDownBuff(pbMagicDefenseDown, ref currentMagicDefenseDown, ref currentMagicDefenseDownValue);
                //AbstractCountDownBuff(pbSpeedDown, ref currentSpeedDown, ref currentSpeedDownValue);
                //AbstractCountDownBuff(pbReactionDown, ref currentReactionDown, ref currentReactionDownValue);
                //AbstractCountDownBuff(pbPotentialDown, ref currentPotentialDown, ref currentPotentialDownValue);

                //// pbStrengthDown系が存在しない

                //AbstractCountDownBuff(pbLightDown, ref currentLightDown, ref currentLightDownValue);
                //AbstractCountDownBuff(pbShadowDown, ref currentShadowDown, ref currentShadowDownValue);
                //AbstractCountDownBuff(pbFireDown, ref currentFireDown, ref currentFireDownValue);
                //AbstractCountDownBuff(pbIceDown, ref currentIceDown, ref currentIceDownValue);
                //AbstractCountDownBuff(pbForceDown, ref currentForceDown, ref currentForceDownValue);
                //AbstractCountDownBuff(pbWillDown, ref currentWillDown, ref currentWillDownValue);

                //// pbResistLightDown系が存在しない

                //AbstractCountDownBuff(pbAfterReviveHalf, ref currentAfterReviveHalf);

                //AbstractCountDownBuff(pbFireDamage2, ref currentFireDamage2);

                //AbstractCountDownBuff(pbBlackMagic, ref currentBlackMagic);

                ////AbstractCountDownBuff(pbChaosDesperate, ref currentChaosDesperate, ref currentChaosDesperateValue);
                //if (currentChaosDesperate > 0)
                //{
                //    currentChaosDesperate--;
                //    currentChaosDesperateValue--;
                //    if (currentChaosDesperate <= 0 || currentChaosDesperateValue <= 0)
                //    {
                //        //currentChaosDesperate = 0; 外部クラスで判定材料にするため、あえてコメントアウト
                //        currentChaosDesperateValue = 0;
                //        if (pbChaosDesperate != null)
                //        {
                //            RemoveOneBuff(pbChaosDesperate);
                //            this.BuffNumber--;
                //        }
                //    }
                //}

                //AbstractCountDownBuff(pbIchinaruHomura, ref currentIchinaruHomura);
                //AbstractCountDownBuff(pbAbyssFire, ref currentAbyssFire);
                //AbstractCountDownBuff(pbLightAndShadow, ref currentLightAndShadow);
                //AbstractCountDownBuff(pbEternalDroplet, ref currentEternalDroplet);
                //AbstractCountDownBuff(pbAusterityMatrixOmega, ref currentAusterityMatrixOmega);
                //AbstractCountDownBuff(pbVoiceOfAbyss, ref currentVoiceOfAbyss);
                //AbstractCountDownBuff(pbAbyssWill, ref currentAbyssWill, ref currentAbyssWillValue);
                //AbstractCountDownBuff(pbTheAbyssWall, ref currentTheAbyssWall);
                //// e 後編追加

                //poolLifeConsumption = 0;
                //poolManaConsumption = 0;
                //poolSkillConsumption = 0;

                //// 後編
                //// s 後編「コメント」以下は、戦闘中永続であり、カウントダウンを含めない。
                ////amplifyPhysicalAttack;
                ////amplifyPhysicalDefense;
                ////amplifyMagicAttack;
                ////amplifyMagicDefense;
                ////amplifyBattleSpeed;
                ////amplifyBattleResponse;
                ////amplifyPotential;
            }
        }
    }
}
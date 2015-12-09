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
        public string FullName { get; set; }
        protected int baseStrength = 5;
        protected int baseAgility = 3;
        protected int baseIntelligence = 2;
        protected int baseStamina = 4;
        protected int baseMind = 3;

        // core parameter
        public string Name { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Stamina { get; set; }
        public int Mind { get; set; }
        public int CurrentLife { get; set; }
        public int CurrentMana { get; set; }
        public int CurrentSkillPoint { get; set; }
        public int CurrentInstantPoint { get; set; }
        protected string currentArchetypeName = string.Empty; // 後編追加
        public string CurrentArchetypeName
        {
            get { return currentArchetypeName; }
            set { currentArchetypeName = value; }
        }
        public int MaxLife
        {
            get { return 50 + this.TotalStamina * 10; }
        }
        public int MaxMana
        {
            get { return 30 + this.TotalIntelligence * 10; }
        }
        public int MaxSkillPoint
        {
            get { return 100; }
        }
        public int MaxInstantPoint
        {
            get { return 300; }
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
       	public List<string> BattleActionCommandList = new List<string>();

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

        // -BUFF resist
        public bool battleResistStun { get; set; }
        public bool battleResistSilence { get; set; }
        public bool battleResistPoison { get; set; }
        public bool battleResistTemptation { get; set; }
        public bool battleResistFrozen { get; set; }
        public bool battleResistParalyze { get; set; }
        public bool battleResistSlow { get; set; }
        public bool battleResistBlind { get; set; }
        public bool battleResistSlip { get; set; }
        public bool battleResistNoResurrection { get; set; }

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
        protected int baseSpecialInstant = 20000; // 後編追加
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
        public double currentSpecialInstant = 0;
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
        public void RemovePoison()
        {
            CurrentPoison = 0;
            CurrentPoisonValue = 0;
            if (pbPoison != null) { pbPoison.enabled = false; }
        }

        public void ActivateFlameAura(Sprite img, int effectTime)
        {
            CurrentFlameAura = effectTime;
            if (pbFlameAura != null) { pbFlameAura.sprite = img; pbFlameAura.enabled = true; }
        }
        public void RemoveFlameAura()
        {
            CurrentFlameAura = 0;
            if (pbFlameAura != null) { pbFlameAura.enabled = false; }
        }

        public void ActivateProtection(Sprite img, int effectTime)
        {
            CurrentProtection = effectTime;
            if (pbProtection != null) { pbProtection.sprite = img; pbProtection.enabled = true; }
        }
        public void RemoveProtection()
        {
            CurrentProtection = 0;
            if (pbProtection != null) { pbProtection.enabled = false; }
        }

        public void ActivateShadowPact(Sprite img, int effectTime)
        {
            CurrentShadowPact = effectTime;
            if (pbShadowPact != null) { pbShadowPact.sprite = img; pbShadowPact.enabled = true; }
        }
        public void RemoveShadowPact()
        {
            CurrentShadowPact = 0;
            if (pbShadowPact != null) { pbShadowPact.enabled = false; }
        }

        public void ActivateWordOfLife(Sprite img, int effectTime)
        {
            CurrentWordOfLife = effectTime;
            if (pbWordOfLife != null) { pbWordOfLife.sprite = img; pbWordOfLife.enabled = true; }
        }
        public void RemoveWordOfLife()
        {
            CurrentWordOfLife = 0;
            if (pbWordOfLife != null) { pbWordOfLife.enabled = false; }
        }

        public void ActivateDeflection(Sprite img, int effectTime)
        {
            CurrentDeflection = effectTime;
            if (pbDeflection != null) { pbDeflection.sprite = img; pbDeflection.enabled = true; }
        }
        public void RemoveDeflection()
        {
            CurrentDeflection = 0;
            if (pbDeflection != null) { pbDeflection.enabled = false; }
        }

        public void ActivateTruthVision(Sprite img, int effectTime)
        {
            CurrentTruthVision = effectTime;
            if (pbTruthVision != null) { pbTruthVision.sprite = img; pbTruthVision.enabled = true; }
        }
        public void RemoveTruthVision()
        {
            CurrentTruthVision = 0;
            if (pbTruthVision != null) { pbTruthVision.enabled = false; }
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
                //			this.MainObjectButton.BackColor = Color.DarkSlateGray;
            }
            //		if (this.pbTargetTarget != null)
            //		{
            //			this.pbTargetTarget.BackColor = Color.DarkSlateGray;
            //		}
            //		if (this.labelName != null) this.labelName.ForeColor = System.Drawing.Color.Red;
            //		if (this.labelLife != null) this.labelLife.ForeColor = System.Drawing.Color.Red;
        }

        public void ResurrectPlayer(int life)
        {
            if (this.CurrentNoResurrection <= 0)
            {
                this.CurrentLife = life;
                this.Dead = false;
                //			if (this.MainObjectButton != null)
                //			{
                //				this.MainObjectButton.Enabled = true;
                //				this.MainObjectButton.BackColor = this.MainColor;
                //			}
                //			if (this.pbTargetTarget != null)
                //			{
                //				this.pbTargetTarget.BackColor = this.MainColor;
                //			}
                //			if (this.labelName != null) { this.labelName.ForeColor = System.Drawing.Color.Black; }
                //			if (this.labelLife != null) { this.labelLife.ForeColor = System.Drawing.Color.Black; this.labelLife.Text = this.currentLife.ToString(); }
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

        internal void ChangeSkyShieldStatus(int p)
        {
            // todo
            throw new System.NotImplementedException();
        }

        internal void ChangeStaticBarrierStatus(int p)
        {
            // todo
            throw new System.NotImplementedException();
        }

        internal void ChangeConcussiveHitStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeOnslaughtHitStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeImpulseHitStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeStanceOfMysticStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeFeltusStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeJuzaPhantasmalStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        public int CurrentEternalFateRingValue
        {
            get { return currentEternalFateRingValue; }
            set { if (value <= 10) { currentEternalFateRingValue = value; } }
        }
        public int CurrentEternalFateRing { get; set; }

        internal void ChangeEternalFateRingStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeLightServantStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeShadowServantStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeAdilBlueBurnStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeMazeCubeStatus(int p)
        {
            throw new System.NotImplementedException();
        }

        internal void ChangeLifeCountStatus(int p)
        {
            throw new System.NotImplementedException();
        }
    }


}
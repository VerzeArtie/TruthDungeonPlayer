using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;

public class TruthSelectCharacter : MotherForm
{
    public Text mainMessage;
    public Button btnClose;
    public Text txtClose;
    public Text txtName;
    public Text txtLevel;
    public Text txtExperience;
    public GameObject groupBtnLifeManaSkill;
    public GameObject groupTxtLifeManaSkill;
    public GameObject btnLife;
    public GameObject btnMana;
    public GameObject btnSkill;
    public Text life;
    public Text mana;
    public Text skill;
    public Button btnStrength;
    public Button btnAgility;
    public Button btnIntelligence;
    public Button btnStamina;
    public Button btnMind;
    public Text strength;
    public Text addStrength;
    public Text addStrengthFood;
    public Text totalStrength;
    public Text agility;
    public Text addAgility;
    public Text addAgilityFood;
    public Text totalAgility;
    public Text intelligence;
    public Text addIntelligence;
    public Text addIntelligenceFood;
    public Text totalIntelligence;
    public Text stamina;
    public Text addStamina;
    public Text addStaminaFood;
    public Text totalStamina;
    public Text mind;
    public Text addMind;
    public Text addMindFood;
    public Text totalMind;
    public Button plus1;
    public Button plus10;
    public Button plus100;
    public Button plus1000;
    public Button btnUpReset;
    public Text lblRemain;
    public Text txtPhysicalAttack;
    public Text txtPhysicalDefense;
    public Text txtMagicAttack;
    public Text txtMagicDefense;
    public Text txtBattleSpeed;
    public Text txtBattleResponse;
    public Text txtPotential;
    public Text weapon;
    public Text subWeapon;
    public Text armor;
    public Text accessory;
    public Text accessory2;
    public GameObject back_weapon;
    public GameObject back_subWeapon;
    public GameObject back_armor;
    public GameObject back_accessory;
    public GameObject back_accessory2;

    public Button btnPlayer1;
    public Button btnPlayer2;
    public Button btnPlayer3;
    public GameObject back_selected1;
    public Text selected1;
    public GameObject back_selected2;
    public Text selected2;
    public Button choice;
    public Button btnFix;

    public Text lblName;
    public Text lblLevel;
    public Text lblExp;
    public Text lblLife;
    public Text lblMana;
    public Text lblSkill;
    public Text lblMainWeapon;
    public Text lblSubWeapon;
    public Text lblArmor;
    public Text lblAccessory1;
    public Text lblAccessory2;
    public Text lblPhysicalAttack;
    public Text lblPhysicalDefense;
    public Text lblMagicAttack;
    public Text lblMagicDefense;
    public Text lblBattleSpeed;
    public Text lblBattleReaction;
    public Text lblPotential;
    public Text lblReset;
    public Text lblChoice;
    public Text lblFix;

    bool usingOvershifting = false;
    bool usingOvershiftingFirstSleep = false;
    bool usingOvershiftingSecondSleep = false;
    bool usingToomiBlueSuisyou = false;

    public List<MainCharacter> playerList = new List<MainCharacter>();
    private MainCharacter currentPlayer = null;
    public const int MAX_ADD = 2;
    private int remainSC = 0;
    private int addStrSC = 0;
    private int addAglSC = 0;
    private int addIntSC = 0;
    private int addStmSC = 0;
    private int addMndSC = 0;
    private int remainTC = 0;
    private int addStrTC = 0;
    private int addAglTC = 0;
    private int addIntTC = 0;
    private int addStmTC = 0;
    private int addMndTC = 0;
    private bool choiceSC = false;
    private bool choiceTC = false;

    private MainCharacter b_tc = null;
    private MainCharacter b_fc = null;

    private enum CoreType
    {
        Strength,
        Agility,
        Intelligence,
        Stamina,
        Mind
    }

    public override void Start()
    {
        base.Start();

        if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
        {
            lblName.text = Database.GUI_S_BASIC_NAME;
            lblLevel.text = Database.GUI_S_BASIC_LEVEL;
            lblExp.text = Database.GUI_S_BASIC_EXP;
            lblLife.text = Database.GUI_S_BASIC_LIFE;
            lblMana.text = Database.GUI_S_BASIC_MANA;
            lblSkill.text = Database.GUI_S_BASIC_SKILL;
            lblMainWeapon.text = Database.GUI_S_BASIC_MAIN;
            lblSubWeapon.text = Database.GUI_S_BASIC_SUB;
            lblArmor.text = Database.GUI_S_BASIC_ARMOR;
            lblAccessory1.text = Database.GUI_S_BASIC_ACCESSORY1;
            lblAccessory2.text = Database.GUI_S_BASIC_ACCESSORY2;
            lblPhysicalAttack.text = Database.GUI_S_BASIC_PATK;
            lblPhysicalDefense.text = Database.GUI_S_BASIC_PDEF;
            lblMagicAttack.text = Database.GUI_S_BASIC_MATK;
            lblMagicDefense.text = Database.GUI_S_BASIC_MDEF;
            lblBattleSpeed.text = Database.GUI_S_BASIC_BSPD;
            lblBattleReaction.text = Database.GUI_S_BASIC_BRCT;
            lblPotential.text = Database.GUI_S_BASIC_PTCL;
            lblReset.text = Database.GUI_SELECT_C_RESET;
            lblChoice.text = Database.GUI_SELECT_C_CHOICE;
            lblFix.text = Database.GUI_SELECT_C_FIX;
        }

        // debug 
        //GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
        //GroundOne.MC.FirstName = Database.EIN_WOLENCE;
        //GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;
        //GroundOne.SC.FirstName = Database.RANA_AMILIA;

        this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);

        SettingCharacterData(player);
        RefreshPartyMembersBattleStatus(player);

        btnMana.SetActive(GroundOne.MC.AvailableMana);
        mana.gameObject.SetActive(GroundOne.MC.AvailableMana);

        btnSkill.SetActive(GroundOne.MC.AvailableSkill);
        skill.gameObject.SetActive(GroundOne.MC.AvailableSkill);

        mainMessage.text = "";

        // 1人目
        if (GroundOne.SC.FullName == Database.RANA_AMILIA_FULL)
        {
            // データ引き継ぎを行う
        }
        else
        {
            // [警告] 本来は一旦データ引き継ぎを行った後、別メンバーが来た場合、ラナ・アミリアの更新後のデータをここで読み込まなければならない。
            //this.sc = new MainCharacter();
            //this.sc.FullName = Database.RANA_AMILIA_FULL;
            //this.sc.Name = Database.RANA_AMILIA;
            //this.sc.Strength = Database.OL_LANDIS_STRENGTH_2;
            //this.sc.Agility = Database.OL_LANDIS_AGILITY_2;
            //this.sc.Intelligence = Database.OL_LANDIS_INTELLIGENCE_2;
            //this.sc.Stamina = Database.OL_LANDIS_STAMINA_2;
            //this.sc.Mind = Database.OL_LANDIS_MIND_2;
            //this.sc.Level = 0;
            //this.sc.Exp = 0;
        }

        int baseLevel = GroundOne.SC.Level;
        if (baseLevel < 65)
        {
            for (int ii = baseLevel; ii < 65; ii++)
            {
                GroundOne.SC.BaseLife += GroundOne.SC.LevelUpLifeTruth;
                GroundOne.SC.BaseMana += GroundOne.SC.LevelUpManaTruth;
                this.remainSC += GroundOne.SC.LevelUpPointTruth;
                GroundOne.SC.Level++;
            }

            GroundOne.SC.Strength += this.remainSC / 5;
            GroundOne.SC.Agility += this.remainSC / 5;
            GroundOne.SC.Intelligence += this.remainSC / 5;
            GroundOne.SC.Stamina += this.remainSC / 5;
            GroundOne.SC.Mind += this.remainSC / 5;
            GroundOne.SC.Mind += this.remainSC % 5;
            this.remainSC = 0;
        }
        GroundOne.SC.AvailableMana = true;
        GroundOne.SC.AvailableSkill = true;

        GroundOne.SC.CurrentLife = GroundOne.SC.MaxLife;
        GroundOne.SC.CurrentSkillPoint = GroundOne.SC.MaxSkillPoint;
        GroundOne.SC.CurrentMana = GroundOne.SC.MaxMana;

        GroundOne.SC.DarkBlast = true;
        GroundOne.SC.ShadowPact = true;
        GroundOne.SC.LifeTap = true;
        GroundOne.SC.BlackContract = true;
        GroundOne.SC.DevouringPlague = true;
        GroundOne.SC.BloodyVengeance = true;
        GroundOne.SC.Damnation = true;
        GroundOne.SC.IceNeedle = true;
        GroundOne.SC.AbsorbWater = true;
        GroundOne.SC.Cleansing = true;
        GroundOne.SC.FrozenLance = true;
        GroundOne.SC.MirrorImage = true;
        GroundOne.SC.PromisedKnowledge = true;
        GroundOne.SC.AbsoluteZero = true;
        GroundOne.SC.DispelMagic = true;
        GroundOne.SC.RiseOfImage = true;
        GroundOne.SC.Deflection = true;
        GroundOne.SC.Tranquility = true;
        GroundOne.SC.OneImmunity = true;
        GroundOne.SC.WhiteOut = true;
        GroundOne.SC.TimeStop = true;

        GroundOne.SC.CounterAttack = true;
        GroundOne.SC.PurePurification = true;
        GroundOne.SC.AntiStun = true;
        GroundOne.SC.StanceOfDeath = true;
        GroundOne.SC.StanceOfFlow = true;
        GroundOne.SC.EnigmaSence = true;
        GroundOne.SC.SilentRush = true;
        GroundOne.SC.OboroImpact = true;
        GroundOne.SC.Negate = true;
        GroundOne.SC.VoidExtraction = true;
        GroundOne.SC.CarnageRush = true;
        GroundOne.SC.NothingOfNothingness = true;

        GroundOne.SC.BlueBullet = true;
        GroundOne.SC.DeepMirror = true;
        GroundOne.SC.DeathDeny = true;
        GroundOne.SC.VanishWave = true;
        GroundOne.SC.VortexField = true;
        GroundOne.SC.BlueDragonWill = true;
        GroundOne.SC.DarkenField = true;
        GroundOne.SC.DoomBlade = true;
        GroundOne.SC.EclipseEnd = true;
        GroundOne.SC.SkyShield = true;
        GroundOne.SC.SacredHeal = true;
        GroundOne.SC.EverDroplet = true;
        GroundOne.SC.StarLightning = true;
        GroundOne.SC.AngelBreath = true;
        GroundOne.SC.EndlessAnthem = true;
        GroundOne.SC.PsychicTrance = true;
        GroundOne.SC.BlindJustice = true;
        GroundOne.SC.TranscendentWish = true;

        GroundOne.SC.FutureVision = true;
        GroundOne.SC.UnknownShock = true;
        GroundOne.SC.Recover = true;
        GroundOne.SC.ImpulseHit = true;
        GroundOne.SC.TrustSilence = true;
        GroundOne.SC.MindKilling = true;
        GroundOne.SC.PsychicWave = true;
        GroundOne.SC.NourishSense = true;
        GroundOne.SC.SharpGlare = true;
        GroundOne.SC.ConcussiveHit = true;
        GroundOne.SC.StanceOfSuddenness = true;
        GroundOne.SC.SoulExecution = true;
        
        SelectOrAdd(GroundOne.SC);

        // 2人目
        GameObject.DestroyObject(GroundOne.TC);
        GroundOne.TC = null;
        GroundOne.TC = (new GameObject("objTC")).AddComponent<MainCharacter>();
        GroundOne.TC.FullName = Database.OL_LANDIS_FULL;
        GroundOne.TC.FirstName = Database.OL_LANDIS;
        GroundOne.TC.Strength = Database.OL_LANDIS_STRENGTH_2;
        GroundOne.TC.Agility = Database.OL_LANDIS_AGILITY_2;
        GroundOne.TC.Intelligence = Database.OL_LANDIS_INTELLIGENCE_2;
        GroundOne.TC.Stamina = Database.OL_LANDIS_STAMINA_2;
        GroundOne.TC.Mind = Database.OL_LANDIS_MIND_2;
        GroundOne.TC.Level = 0;
        GroundOne.TC.Exp = 0;
        for (int ii = 0; ii < 65; ii++)
        {
            GroundOne.TC.BaseLife += GroundOne.TC.LevelUpLifeTruth;
            GroundOne.TC.BaseMana += GroundOne.TC.LevelUpManaTruth;
            GroundOne.TC.Level++;
        }

        GroundOne.TC.MainWeapon = new ItemBackPack(Database.LEGENDARY_GOD_FIRE_GLOVE);
        GroundOne.TC.MainArmor = new ItemBackPack(Database.EPIC_AURA_ARMOR_OMEGA);
        GroundOne.TC.Accessory = new ItemBackPack(Database.EPIC_FATE_RING_OMEGA);
        GroundOne.TC.Accessory2 = new ItemBackPack(Database.EPIC_LOYAL_RING_OMEGA);
        GroundOne.TC.BattleActionCommand1 = Database.DEFENSE_EN;
        GroundOne.TC.BattleActionCommand2 = Database.ONE_IMMUNITY;
        GroundOne.TC.BattleActionCommand3 = Database.BLACK_CONTRACT;
        GroundOne.TC.BattleActionCommand4 = Database.CARNAGE_RUSH;
        GroundOne.TC.BattleActionCommand5 = Database.DEMONIC_IGNITE;
        GroundOne.TC.BattleActionCommand6 = Database.HARDEST_PARRY;
        GroundOne.TC.BattleActionCommand7 = Database.IMPULSE_HIT;
        GroundOne.TC.BattleActionCommand8 = Database.DISPEL_MAGIC;
        GroundOne.TC.BattleActionCommand9 = Database.SEVENTH_MAGIC;

        GroundOne.TC.AvailableMana = true;
        GroundOne.TC.AvailableSkill = true;

        GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife;
        GroundOne.TC.CurrentSkillPoint = GroundOne.TC.MaxSkillPoint;
        GroundOne.TC.CurrentMana = GroundOne.TC.MaxMana;

        GroundOne.TC.FireBall = true;
        GroundOne.TC.FlameAura = true;
        GroundOne.TC.HeatBoost = true;
        GroundOne.TC.FlameStrike = true;
        GroundOne.TC.VolcanicWave = true;
        GroundOne.TC.ImmortalRave = true;
        GroundOne.TC.LavaAnnihilation = true;
        GroundOne.TC.DarkBlast = true;
        GroundOne.TC.ShadowPact = true;
        GroundOne.TC.LifeTap = true;
        GroundOne.TC.BlackContract = true;
        GroundOne.TC.DevouringPlague = true;
        GroundOne.TC.BloodyVengeance = true;
        GroundOne.TC.Damnation = true;
        GroundOne.TC.DispelMagic = true;
        GroundOne.TC.RiseOfImage = true;
        GroundOne.TC.Deflection = true;
        GroundOne.TC.Tranquility = true;
        GroundOne.TC.OneImmunity = true;
        GroundOne.TC.WhiteOut = true;
        GroundOne.TC.TimeStop = true;

        GroundOne.TC.StraightSmash = true;
        GroundOne.TC.DoubleSlash = true;
        GroundOne.TC.CrushingBlow = true;
        GroundOne.TC.SoulInfinity = true;
        GroundOne.TC.CounterAttack = true;
        GroundOne.TC.PurePurification = true;
        GroundOne.TC.AntiStun = true;
        GroundOne.TC.StanceOfDeath = true;
        GroundOne.TC.StanceOfFlow = true;
        GroundOne.TC.EnigmaSence = true;
        GroundOne.TC.SilentRush = true;
        GroundOne.TC.OboroImpact = true;
        GroundOne.TC.StanceOfStanding = true;
        GroundOne.TC.InnerInspiration = true;
        GroundOne.TC.KineticSmash = true;
        GroundOne.TC.Catastrophe = true;
        GroundOne.TC.TruthVision = true;
        GroundOne.TC.HighEmotionality = true;
        GroundOne.TC.StanceOfEyes = true;
        GroundOne.TC.PainfulInsanity = true;
        GroundOne.TC.Negate = true;
        GroundOne.TC.VoidExtraction = true;
        GroundOne.TC.CarnageRush = true;
        GroundOne.TC.NothingOfNothingness = true;

        GroundOne.TC.BlackFire = true;
        GroundOne.TC.BlazingField = true;
        GroundOne.TC.DemonicIgnite = true;
        GroundOne.TC.Immolate = true;
        GroundOne.TC.PhantasmalWind = true;
        GroundOne.TC.RedDragonWill = true;
        GroundOne.TC.DarkenField = true;
        GroundOne.TC.DoomBlade = true;
        GroundOne.TC.EclipseEnd = true;
        GroundOne.TC.WordOfMalice = true;
        GroundOne.TC.AbyssEye = true;
        GroundOne.TC.SinFortune = true;
        GroundOne.TC.EnrageBlast = true;
        GroundOne.TC.PiercingFlame = true;
        GroundOne.TC.SigilOfHomura = true;
        GroundOne.TC.SeventhMagic = true;
        GroundOne.TC.ParadoxImage = true;
        GroundOne.TC.WarpGate = true;

        GroundOne.TC.NeutralSmash = true;
        GroundOne.TC.StanceOfDouble = true;
        GroundOne.TC.SwiftStep = true;
        GroundOne.TC.VigorSense = true;
        GroundOne.TC.CircleSlash = true;
        GroundOne.TC.RisingAura = true;
        GroundOne.TC.RumbleShout = true;
        GroundOne.TC.OnslaughtHit = true;
        GroundOne.TC.ColorlessMove = true;
        GroundOne.TC.AscensionAura = true;
        GroundOne.TC.FutureVision = true;
        GroundOne.TC.UnknownShock = true;
        GroundOne.TC.ReflexSpirit = true;
        GroundOne.TC.FatalBlow = true;
        GroundOne.TC.SharpGlare = true;
        GroundOne.TC.ConcussiveHit = true;
        GroundOne.TC.TrustSilence = true;
        GroundOne.TC.MindKilling = true;
        GroundOne.TC.SurpriseAttack = true;
        GroundOne.TC.StanceOfMystic = true;
        GroundOne.TC.PsychicWave = true;
        GroundOne.TC.NourishSense = true;
        GroundOne.TC.Recover = true;
        GroundOne.TC.ImpulseHit = true;
        GroundOne.TC.ViolentSlash = true;
        GroundOne.TC.ONEAuthority = true;
        GroundOne.TC.OuterInspiration = true;
        GroundOne.TC.HardestParry = true;
        GroundOne.TC.StanceOfSuddenness = true;
        GroundOne.TC.SoulExecution = true;
        //if (GroundOne.TC.MainWeapon != null) { if (GroundOne.TC.MainWeapon.Name == Database.POOR_GOD_FIRE_GLOVE_REPLICA) { GroundOne.TC.MainWeapon = new ItemBackPack(Database.LEGENDARY_GOD_FIRE_GLOVE); } }

        // 3人目
        this.b_fc = (new GameObject("obj_bFC")).AddComponent<MainCharacter>();
        this.b_fc.FullName = Database.SINIKIA_KAHLHANZ_FULL;
        this.b_fc.FirstName = Database.SINIKIA_KAHLHANZ;
        this.b_fc.Strength = Database.SINIKIA_KAHLHANTZ_STRENGTH_2;
        this.b_fc.Agility = Database.SINIKIA_KAHLHANTZ_AGILITY_2;
        this.b_fc.Intelligence = Database.SINIKIA_KAHLHANTZ_INTELLIGENCE_2;
        this.b_fc.Stamina = Database.SINIKIA_KAHLHANTZ_STAMINA_2;
        this.b_fc.Mind = Database.SINIKIA_KAHLHANTZ_MIND_2;
        this.b_fc.Level = 0;
        this.b_fc.Exp = 0;
        for (int ii = 0; ii < 65; ii++)
        {
            this.b_fc.BaseLife += this.b_fc.LevelUpLifeTruth;
            this.b_fc.BaseMana += this.b_fc.LevelUpManaTruth;
            this.b_fc.Level++;
        }

        this.b_fc.MainWeapon = new ItemBackPack(Database.EPIC_DARKMAGIC_DEVIL_EYE_2);
        this.b_fc.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE_2);
        this.b_fc.Accessory = new ItemBackPack(Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2);
        this.b_fc.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2);
        this.b_fc.BattleActionCommand1 = Database.PIERCING_FLAME;
        this.b_fc.BattleActionCommand2 = Database.CELESTIAL_NOVA;
        this.b_fc.BattleActionCommand3 = Database.SACRED_HEAL;
        this.b_fc.BattleActionCommand4 = Database.GENESIS;
        this.b_fc.BattleActionCommand5 = Database.PROMISED_KNOWLEDGE;
        this.b_fc.BattleActionCommand6 = Database.PSYCHIC_TRANCE;
        this.b_fc.BattleActionCommand7 = Database.DISPEL_MAGIC;
        this.b_fc.BattleActionCommand8 = Database.ONE_IMMUNITY;
        this.b_fc.BattleActionCommand9 = Database.RESURRECTION;
        this.b_fc.AvailableMana = true;
        this.b_fc.AvailableSkill = true;

        this.b_fc.CurrentLife = this.b_fc.MaxLife;
        this.b_fc.BaseSkillPoint = 100;
        this.b_fc.CurrentSkillPoint = this.b_fc.MaxSkillPoint;
        this.b_fc.CurrentMana = this.b_fc.MaxMana;

        this.b_fc.FreshHeal = true;
        this.b_fc.Protection = true;
        this.b_fc.HolyShock = true;
        this.b_fc.SaintPower = true;
        this.b_fc.Glory = true;
        this.b_fc.Resurrection = true;
        this.b_fc.CelestialNova = true;
        this.b_fc.DarkBlast = true;
        this.b_fc.ShadowPact = true;
        this.b_fc.LifeTap = true;
        this.b_fc.BlackContract = true;
        this.b_fc.DevouringPlague = true;
        this.b_fc.BloodyVengeance = true;
        this.b_fc.Damnation = true;
        this.b_fc.FireBall = true;
        this.b_fc.FlameAura = true;
        this.b_fc.HeatBoost = true;
        this.b_fc.FlameStrike = true;
        this.b_fc.VolcanicWave = true;
        this.b_fc.ImmortalRave = true;
        this.b_fc.LavaAnnihilation = true;
        this.b_fc.IceNeedle = true;
        this.b_fc.AbsorbWater = true;
        this.b_fc.Cleansing = true;
        this.b_fc.FrozenLance = true;
        this.b_fc.MirrorImage = true;
        this.b_fc.PromisedKnowledge = true;
        this.b_fc.AbsoluteZero = true;
        this.b_fc.WordOfPower = true;
        this.b_fc.GaleWind = true;
        this.b_fc.WordOfLife = true;
        this.b_fc.WordOfFortune = true;
        this.b_fc.AetherDrive = true;
        this.b_fc.Genesis = true;
        this.b_fc.EternalPresence = true;
        this.b_fc.DispelMagic = true;
        this.b_fc.RiseOfImage = true;
        this.b_fc.Deflection = true;
        this.b_fc.Tranquility = true;
        this.b_fc.OneImmunity = true;
        this.b_fc.WhiteOut = true;
        this.b_fc.TimeStop = true;

        this.b_fc.CounterAttack = true;
        this.b_fc.StanceOfFlow = true;
        this.b_fc.Negate = true;

        this.b_fc.PsychicTrance = true;
        this.b_fc.BlindJustice = true;
        this.b_fc.TranscendentWish = true;
        this.b_fc.FlashBlaze = true;
        this.b_fc.LightDetonator = true;
        this.b_fc.AscendantMeteor = true;
        this.b_fc.SkyShield = true;
        this.b_fc.SacredHeal = true;
        this.b_fc.EverDroplet = true;
        this.b_fc.HolyBreaker = true;
        this.b_fc.ExaltedField = true;
        this.b_fc.HymnContract = true;
        this.b_fc.StarLightning = true;
        this.b_fc.AngelBreath = true;
        this.b_fc.EndlessAnthem = true;
        this.b_fc.BlackFire = true;
        this.b_fc.BlazingField = true;
        this.b_fc.DemonicIgnite = true;
        this.b_fc.BlueBullet = true;
        this.b_fc.DeepMirror = true;
        this.b_fc.DeathDeny = true;
        this.b_fc.WordOfMalice = true;
        this.b_fc.AbyssEye = true;
        this.b_fc.SinFortune = true;
        this.b_fc.DarkenField = true;
        this.b_fc.DoomBlade = true;
        this.b_fc.EclipseEnd = true;
        this.b_fc.FrozenAura = true;
        this.b_fc.ChillBurn = true;
        this.b_fc.ZetaExplosion = true;
        this.b_fc.EnrageBlast = true;
        this.b_fc.PiercingFlame = true;
        this.b_fc.SigilOfHomura = true;
        this.b_fc.Immolate = true;
        this.b_fc.PhantasmalWind = true;
        this.b_fc.RedDragonWill = true;
        this.b_fc.WordOfAttitude = true;
        this.b_fc.StaticBarrier = true;
        this.b_fc.AusterityMatrix = true;
        this.b_fc.VanishWave = true;
        this.b_fc.VortexField = true;
        this.b_fc.BlueDragonWill = true;
        this.b_fc.SeventhMagic = true;
        this.b_fc.ParadoxImage = true;
        this.b_fc.WarpGate = true;

        // 4人目
        this.b_tc = (new GameObject("obj_bTC")).AddComponent<MainCharacter>();
        this.b_tc.FullName = Database.VERZE_ARTIE_FULL;
        this.b_tc.FirstName = Database.VERZE_ARTIE;
        this.b_tc.Strength = Database.VERZE_ARTIE_STRENGTH_3;
        this.b_tc.Agility = Database.VERZE_ARTIE_AGILITY_3;
        this.b_tc.Intelligence = Database.VERZE_ARTIE_INTELLIGENCE_3;
        this.b_tc.Stamina = Database.VERZE_ARTIE_STAMINA_3;
        this.b_tc.Mind = Database.VERZE_ARTIE_MIND_3;
        this.b_tc.Level = 0;
        this.b_tc.Exp = 0;
        for (int ii = 0; ii < 65; ii++)
        {
            this.b_tc.BaseLife += this.b_tc.LevelUpLifeTruth;
            this.b_tc.BaseMana += this.b_tc.LevelUpManaTruth;
            this.b_tc.Level++;
        }
        this.b_tc.MainWeapon = new ItemBackPack(Database.EPIC_WHITE_SILVER_SWORD_REPLICA);
        this.b_tc.MainArmor = new ItemBackPack(Database.EPIC_BLACK_AERIAL_ARMOR_REPLICA);
        this.b_tc.Accessory = new ItemBackPack(Database.EPIC_HEAVENLY_SKY_WING_REPLICA);
        this.b_tc.BattleActionCommand1 = Database.NEUTRAL_SMASH;
        this.b_tc.BattleActionCommand2 = Database.INNER_INSPIRATION;
        this.b_tc.BattleActionCommand3 = Database.MIRROR_IMAGE;
        this.b_tc.BattleActionCommand4 = Database.DEFLECTION;
        this.b_tc.BattleActionCommand5 = Database.STANCE_OF_FLOW;
        this.b_tc.BattleActionCommand6 = Database.GALE_WIND;
        this.b_tc.BattleActionCommand7 = Database.STRAIGHT_SMASH;
        this.b_tc.BattleActionCommand8 = Database.SURPRISE_ATTACK;
        this.b_tc.BattleActionCommand9 = Database.NEGATE;
        this.b_tc.AvailableMana = true;
        this.b_tc.AvailableSkill = true;

        this.b_tc.CurrentLife = this.b_tc.MaxLife;
        this.b_tc.BaseSkillPoint = 100;
        this.b_tc.CurrentSkillPoint = 100;
        this.b_tc.CurrentMana = this.b_tc.MaxMana;

        this.b_tc.FireBall = true;
        this.b_tc.StraightSmash = true;
        this.b_tc.CounterAttack = true;
        this.b_tc.FreshHeal = true;
        this.b_tc.StanceOfFlow = true;
        this.b_tc.DispelMagic = true;
        this.b_tc.WordOfPower = true;
        this.b_tc.EnigmaSence = true;
        this.b_tc.BlackContract = true;
        this.b_tc.Cleansing = true;
        this.b_tc.GaleWind = true;
        this.b_tc.Deflection = true;
        this.b_tc.Negate = true;
        this.b_tc.InnerInspiration = true;
        this.b_tc.FrozenLance = true;
        this.b_tc.Tranquility = true;
        this.b_tc.WordOfFortune = true;
        this.b_tc.SkyShield = true;
        this.b_tc.NeutralSmash = true;
        this.b_tc.Glory = true;
        this.b_tc.BlackFire = true;
        this.b_tc.SurpriseAttack = true;
        this.b_tc.MirrorImage = true;
        this.b_tc.WordOfMalice = true;
        this.b_tc.StanceOfSuddenness = true;
        this.b_tc.CrushingBlow = true;
        this.b_tc.Immolate = true;
        this.b_tc.AetherDrive = true;
        this.b_tc.TrustSilence = true;
        this.b_tc.WordOfAttitude = true;
        this.b_tc.OneImmunity = true;
        this.b_tc.AntiStun = true;
        this.b_tc.FutureVision = true;
        this.b_tc.StanceOfEyes = true;
        this.b_tc.SwiftStep = true;
        this.b_tc.Resurrection = true;
        this.b_tc.BlindJustice = true;
        this.b_tc.Genesis = true;
        this.b_tc.DeepMirror = true;
        this.b_tc.ImmortalRave = true;
        this.b_tc.DoomBlade = true;
        this.b_tc.CarnageRush = true;
        this.b_tc.ChillBurn = true;
        this.b_tc.WhiteOut = true;
        this.b_tc.PhantasmalWind = true;
        this.b_tc.PainfulInsanity = true;
        this.b_tc.FatalBlow = true;
        this.b_tc.StaticBarrier = true;
        this.b_tc.StanceOfDeath = true;
        this.b_tc.EverDroplet = true;
        this.b_tc.Catastrophe = true;
        this.b_tc.CelestialNova = true;
        this.b_tc.MindKilling = true;
        this.b_tc.NothingOfNothingness = true;
        this.b_tc.AbsoluteZero = true;
        this.b_tc.AusterityMatrix = true;
        this.b_tc.VigorSense = true;
        this.b_tc.LavaAnnihilation = true;
        this.b_tc.EclipseEnd = true;
        this.b_tc.TimeStop = true;
        this.b_tc.SinFortune = true;
        this.b_tc.DemonicIgnite = true;
        // 以下は65以降
        //this.tc.StanceOfDouble = true;
        //this.tc.WarpGate = true;
        //this.tc.StanceOfMystic = true;
        //this.tc.SoulExecution = true;
        //this.tc.ZetaExplosion = true;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void SceneBack()
    {
        base.SceneBack();

        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
        SettingCharacterData(player);
        RefreshPartyMembersBattleStatus(player);
    }

    public void btnReset_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        this.playerList.Clear();
        selected1.text = "";
        selected2.text = "";
        back_selected1.GetComponent<Image>().color = UnityColor.Cornsilk;
        back_selected2.GetComponent<Image>().color = UnityColor.Cornsilk;
        btnPlayer1.enabled = true;
        btnPlayer2.enabled = true;
        btnPlayer3.enabled = true;
        choice.enabled = true;
        btnFix.enabled = false;
        this.choiceSC = false;
        this.choiceTC = false;
        UpdateBtnUpReset();
    }

    public void choice_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        SelectOrAdd(this.currentPlayer);
    }

    public void btnFix_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        GroundOne.WE.AvailableSecondCharacter = true;
        GroundOne.WE.AvailableThirdCharacter = true;
        GroundOne.SC = playerList[0];
        DontDestroyOnLoad(GroundOne.SC);
        GroundOne.TC = playerList[1];
        DontDestroyOnLoad(GroundOne.TC);
        SceneDimension.Back(this);
    }

    private void CheckMaxAdd()
    {
        if (this.playerList.Count >= MAX_ADD)
        {
            btnPlayer1.enabled = false;
            btnPlayer2.enabled = false;
            btnPlayer3.enabled = false;
            choice.enabled = false;
            btnFix.enabled = true;
        }
    }

    private void PlayerAdd(string name)
    {
        Text current = selected1;
        GameObject back_current = back_selected1;
        this.playerList.Add(this.currentPlayer);
        if (this.playerList.Count == 1)
        {
            current = selected1;
            back_current = back_selected1;
        }
        else if (this.playerList.Count == 2)
        {
            current = selected2;
            back_current = back_selected2;
        }
        if (this.currentPlayer == null) { Debug.Log("currentplayer is null..."); }

        current.text = this.currentPlayer.FullName;
        if (currentPlayer.FirstName == Database.RANA_AMILIA)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            this.btnUpReset.gameObject.SetActive(false);
        }
        else if (currentPlayer.FirstName == Database.OL_LANDIS)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            this.btnUpReset.gameObject.SetActive(false);
        }
        else if (currentPlayer.FirstName == Database.VERZE_ARTIE)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
        }
        else if (currentPlayer.FirstName == Database.SINIKIA_KAHLHANZ)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
        }
        CheckMaxAdd();
    }

    private void UpdateBtnUpReset()
    {
        btnUpReset.gameObject.SetActive(false);
        //if (this.currentPlayer == null) { return; }

        //if (this.currentPlayer.Equals(GroundOne.SC))
        //{
        //    btnUpReset.gameObject.SetActive(!this.choiceSC);
        //}
        //else if (this.currentPlayer.Equals(GroundOne.TC))
        //{
        //    btnUpReset.gameObject.SetActive(!this.choiceTC);
        //}
        //else
        //{
        //    btnUpReset.gameObject.SetActive(false);
        //}
    }

    private void SelectOrAdd(MainCharacter player)
    {
        if ((this.currentPlayer == null) ||
            (this.currentPlayer.FullName != player.FullName))
        {
            this.currentPlayer = player;
            this.Background.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
        }
        else
        {
            if (this.playerList.Contains(player))
            {
                // 何もしない
            }
            else
            {
                if (player.Equals(GroundOne.SC))
                {
                    if (this.remainSC > 0)
                    {
                        mainMessage.text = "アイン：パラメタを先に割り振ろう";// this.sc.FullName + "のパラメタ割り振りを完了してください。";
                    }
                    else
                    {
                        this.choiceSC = true;
                        PlayerAdd(player.FullName);
                    }
                }
                else if (player.Equals(GroundOne.TC))
                {
                    if (this.remainTC > 0)
                    {
                        mainMessage.text = "アイン：パラメタを先に割り振ろう"; // this.tc.FullName + "のパラメタ割り振りを完了してください。";
                    }
                    else
                    {
                        this.choiceTC = true;
                        PlayerAdd(player.FullName);
                    }
                }
                else
                {
                    PlayerAdd(player.FullName);
                }
            }
        }
        UpdateBtnUpReset();
    }

    private void SettingCoreParameter(CoreType coreType, int basicValue, int addAccessoryValue, int addFoodValue, Text txtBasic, Text txtBuff, Text txtFood, Text txtTotal)
    {
        int totalValue = 0;

        totalValue = basicValue;
        txtBasic.text = basicValue.ToString();

        txtBuff.text = "+" + addAccessoryValue.ToString();
        totalValue += addAccessoryValue;

        txtFood.text = "+" + addFoodValue.ToString();
        totalValue += addFoodValue;

        txtTotal.text = "= " + totalValue.ToString();
    }

    private void SettingCharacterData(MainCharacter chara)
    {
        this.txtName.text = chara.FullName;
        this.txtLevel.text = chara.Level.ToString();
        if (chara.Level < Database.CHARACTER_MAX_LEVEL5)
        {
            this.txtExperience.text = chara.Exp.ToString() + " / " + chara.NextLevelBorder.ToString();
        }
        else
        {
            this.txtExperience.text = "-----" + " / " + "-----";
        }


        SettingCoreParameter(CoreType.Strength, chara.Strength, chara.BuffStrength_Accessory, chara.BuffStrength_Food, this.strength, this.addStrength, this.addStrengthFood, this.totalStrength);
        SettingCoreParameter(CoreType.Intelligence, chara.Intelligence, chara.BuffIntelligence_Accessory, chara.BuffIntelligence_Food, this.intelligence, this.addIntelligence, this.addIntelligenceFood, this.totalIntelligence);
        SettingCoreParameter(CoreType.Agility, chara.Agility, chara.BuffAgility_Accessory, chara.BuffAgility_Food, this.agility, this.addAgility, this.addAgilityFood, this.totalAgility);
        SettingCoreParameter(CoreType.Stamina, chara.Stamina, chara.BuffStamina_Accessory, chara.BuffStamina_Food, this.stamina, this.addStamina, this.addStaminaFood, this.totalStamina);
        SettingCoreParameter(CoreType.Mind, chara.Mind, chara.BuffMind_Accessory, chara.BuffMind_Food, this.mind, this.addMind, this.addMindFood, this.totalMind);

        //    plus1.gameObject.SetActive(false);
        //    plus10.gameObject.SetActive(false);
        //    plus100.gameObject.SetActive(false);
        //    plus1000.gameObject.SetActive(false);
        //    btnUpReset.gameObject.SetActive(false);
        //    lblRemain.gameObject.SetActive(false);
        //if (chara.Equals(GroundOne.SC) || chara.Equals(GroundOne.TC))
        //{
        //    plus1.gameObject.SetActive(true);
        //    plus10.gameObject.SetActive(true);
        //    plus100.gameObject.SetActive(true);
        //    plus1000.gameObject.SetActive(true);
        //    btnUpReset.gameObject.SetActive(true);
        //    lblRemain.gameObject.SetActive(true);
        //    if (chara.Equals(GroundOne.SC))
        //    {
        //        lblRemain.text = "残り　" + this.remainSC.ToString();
        //    }
        //    else
        //    {
        //        lblRemain.text = "残り　" + this.remainTC.ToString();
        //    }
        //}
        //else
        //{
        //    plus1.gameObject.SetActive(false);
        //    plus10.gameObject.SetActive(false);
        //    plus100.gameObject.SetActive(false);
        //    plus1000.gameObject.SetActive(false);
        //    btnUpReset.gameObject.SetActive(false);
        //    lblRemain.gameObject.SetActive(false);
        //}
        //UpdateBtnUpReset();

        this.life.text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

        if (chara.AvailableSkill)
        {
            if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
            {
                chara.CurrentSkillPoint = chara.MaxSkillPoint;
            }
            skill.text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
        }

        if (chara.AvailableMana)
        {
            mana.text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
        }

        this.weapon.text = "";
        this.subWeapon.text = "";
        this.armor.text = "";
        this.accessory.text = "";
        this.accessory2.text = "";
        if (chara.MainWeapon != null)
        {
            this.weapon.text = chara.MainWeapon.Name;
        }
        else
        {
            this.weapon.text = "";
        }
        Method.UpdateRareColor(chara.MainWeapon, weapon, back_weapon, null);

        if (chara.SubWeapon != null)
        {
            this.subWeapon.text = chara.SubWeapon.Name;
        }
        else
        {
            this.subWeapon.text = "";
        }
        Method.UpdateRareColor(chara.SubWeapon, subWeapon, back_subWeapon, null);

        if (chara.MainArmor != null)
        {
            this.armor.text = chara.MainArmor.Name;
        }
        else
        {
            this.armor.text = "";
        }
        Method.UpdateRareColor(chara.MainArmor, armor, back_armor, null);

        if (chara.Accessory != null)
        {
            this.accessory.text = chara.Accessory.Name;
        }
        else
        {
            this.accessory.text = "";
        }
        Method.UpdateRareColor(chara.Accessory, accessory, back_accessory, null);

        if (chara.Accessory2 != null)
        {
            this.accessory2.text = chara.Accessory2.Name;
        }
        else
        {
            this.accessory2.text = "";
        }
        Method.UpdateRareColor(chara.Accessory2, accessory2, back_accessory2, null);
    }

    private void RefreshPartyMembersBattleStatus(MainCharacter player)
    {
        double temp1 = 0;
        double temp2 = 0;
        temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false);
        temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false);
        txtPhysicalAttack.text = temp1.ToString("F2");
        txtPhysicalAttack.text += " - " + temp2.ToString("F2");

        temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, false);
        temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, false);
        if (temp1 > 0)
        {
            txtPhysicalAttack.text += "\r\n" + temp1.ToString("F2");
            txtPhysicalAttack.text += " - " + temp2.ToString("F2");
        }

        temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
        temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
        txtPhysicalDefense.text = temp1.ToString("F2");
        txtPhysicalDefense.text += " - " + temp2.ToString("F2");

        temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, false);
        temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, false);
        txtMagicAttack.text = temp1.ToString("F2");
        txtMagicAttack.text += " - " + temp2.ToString("F2");

        temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
        temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
        txtMagicDefense.text = temp1.ToString("F2");
        txtMagicDefense.text += " - " + temp2.ToString("F2");

        temp1 = PrimaryLogic.BattleSpeedValue(player, false);
        txtBattleSpeed.text = temp1.ToString("F2");

        temp1 = PrimaryLogic.BattleResponseValue(player, false);
        txtBattleResponse.text = temp1.ToString("F2");

        temp1 = PrimaryLogic.PotentialValue(player, false);
        txtPotential.text = temp1.ToString("F2");
    }

    public void FirstChara_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
        SelectOrAdd(GroundOne.SC);
    }

    public void SecondChara_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
        SelectOrAdd(GroundOne.TC);
    }

    public void ThirdChara_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
        SelectOrAdd(this.b_fc);
    }

    upType number = upType.Strength;

    enum upType
    {
        Strength,
        Agility,
        Intelligence,
        Stamina,
        Mind
    }

    private void CheckUpPoint()
    {
        GroundOne.UpPoint--;
        if (GroundOne.UpPoint <= 0)
        {
            mainMessage.text = "ポイント割り振り完了！";
            txtClose.text = "完了";
            btnClose.gameObject.SetActive(true);
        }
        else
        {
            mainMessage.text = "あと" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
        }
    }
    public void buttonStrength_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Strength++;
                strength.text = GroundOne.MC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Strength++;
                strength.text = GroundOne.SC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Strength++;
                strength.text = GroundOne.TC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }


    public void buttonAgility_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Agility++;
                agility.text = GroundOne.MC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Agility++;
                agility.text = GroundOne.SC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Agility++;
                agility.text = GroundOne.TC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    public void buttonIntelligence_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Intelligence++;
                intelligence.text = GroundOne.MC.Intelligence.ToString();
                if (GroundOne.MC.AvailableMana)
                {
                    this.mana.text = GroundOne.MC.CurrentMana.ToString() + " / " + GroundOne.MC.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Intelligence++;
                intelligence.text = GroundOne.SC.Intelligence.ToString();
                if (GroundOne.SC.AvailableMana)
                {
                    this.mana.text = GroundOne.SC.CurrentMana.ToString() + " / " + GroundOne.SC.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Intelligence++;
                intelligence.text = GroundOne.TC.Intelligence.ToString();
                if (GroundOne.TC.AvailableMana)
                {
                    this.mana.text = GroundOne.TC.CurrentMana.ToString() + " / " + GroundOne.TC.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    public void buttonStamina_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Stamina++;
                stamina.text = GroundOne.MC.Stamina.ToString();
                this.life.text = GroundOne.MC.CurrentLife.ToString() + " / " + GroundOne.MC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Stamina++;
                stamina.text = GroundOne.SC.Stamina.ToString();
                this.life.text = GroundOne.SC.CurrentLife.ToString() + " / " + GroundOne.SC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Stamina++;
                stamina.text = GroundOne.TC.Stamina.ToString();
                this.life.text = GroundOne.TC.CurrentLife.ToString() + " / " + GroundOne.TC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    public void buttonMind_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Mind++;
                mind.text = GroundOne.MC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Mind++;
                mind.text = GroundOne.SC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Mind++;
                mind.text = GroundOne.TC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    private void GoLevelUpPoint(upType type, int plus, ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
    {
        if (remain > 0 && remain >= plus)
        {
            remain -= plus;
            if (type == upType.Strength)
            {
                addStr += plus;
                player.Strength += plus;
                strength.text = player.Strength.ToString();
            }
            else if (type == upType.Agility)
            {
                addAgl += plus;
                player.Agility += plus;
                agility.text = player.Agility.ToString();
            }
            else if (type == upType.Intelligence)
            {
                addInt += plus;
                player.Intelligence += plus;
                intelligence.text = player.Intelligence.ToString();
            }
            else if (type == upType.Stamina)
            {
                addStm += plus;
                player.Stamina += plus;
                stamina.text = player.Stamina.ToString();
            }
            else if (type == upType.Mind)
            {
                addMnd += plus;
                player.Mind += plus;
                mind.text = player.Mind.ToString();
            }
            RefreshPartyMembersBattleStatus(player);
            lblRemain.text = "残り " + remain.ToString();
        }
    }

    public void plus1_Click(Text sender)
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
        int plus = 0;
        if (sender.text == "+1") { plus = 1; }
        else if (sender.text == "+10") { plus = 10; }
        else if (sender.text == "+100") { plus = 100; }
        else if (sender.text == "+1000") { plus = 1000; }

        GoLevelUpPoint(this.number, plus, ref player, ref GroundOne.UpPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
        if (GroundOne.UpPoint <= 0)
        {
            plus1.gameObject.SetActive(false);
            plus10.gameObject.SetActive(false);
            plus100.gameObject.SetActive(false);
            plus1000.gameObject.SetActive(false);
            btnUpReset.gameObject.SetActive(false);
            lblRemain.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(true);
            mainMessage.text = player.GetCharacterSentence(2036);
            player.CurrentLife = player.MaxLife;
            player.CurrentSkillPoint = player.MaxSkillPoint;
            player.CurrentMana = player.MaxMana;
            SettingCharacterData(player);
        }
    }

    private void ResetParameter(ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
    {
        remain += addStr; player.Strength -= addStr; addStr = 0;
        remain += addAgl; player.Agility -= addAgl; addAgl = 0;
        remain += addInt; player.Intelligence -= addInt; addInt = 0;
        remain += addStm; player.Stamina -= addStm; addStm = 0;
        remain += addMnd; player.Mind -= addMnd; addMnd = 0;
        lblRemain.text = "残り　" + remain.ToString();
    }

    public void btnUpReset_Click()
    {
        GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
        ResetParameter(ref player, ref GroundOne.UpPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
        SettingCharacterData(player);
        RefreshPartyMembersBattleStatus(player);
    }


    public void Item_MouseEnter(Text sender)
    {
        if (sender.text == "")
        {
            mainMessage.text = "";
        }
        else
        {
            ItemBackPack temp = new ItemBackPack(sender.text);
            mainMessage.text = temp.Description;
        }
    }
}
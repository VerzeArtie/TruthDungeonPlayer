using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;
using System.IO;
using System.Text;

namespace DungeonPlayer
{
    public class Title : MotherForm
    {
        public Camera cam;
        public Text titleText;
        public Text subtitleText;
        public GameObject GroupSystemMessage;
        public Text SystemMessageText;
        public GameObject GroupMenu;
        public GameObject buttonGamestart;
        public GameObject buttonSeeker;
        public GameObject buttonLoad;
        public GameObject buttonConfig;
        public GameObject buttonExit;
        public GameObject groupAccount;
        public Text account;

        // debug
        public Text DebugStrength;
        public Text DebugAgility;
        public Text DebugIntelligence;
        public Text DebugStamina;
        public Text DebugMind;
        public Toggle toggle2;
        public Toggle toggle3;
        public Toggle toggleB2;
        public Toggle toggleB3;
        public Toggle toggleDuel;

        public override void Start()
        {
            base.Start();
            if (GroundOne.EnableBGM)
            {
                GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin); // 後編追加    
            }

            // 初期開始時、ファイルが無い場合準備しておく。
            Method.MakeDirectory();

            // GroundOne.WE2はゲーム全体のセーブデータであり、ここで読み込んでおく。
            Method.ReloadTruthWorldEnvironment();

            // 現実世界突入でSeekerモードを表示
            if (GroundOne.WE2.RealWorld)
            {
                buttonSeeker.SetActive(true);
                this.cam.backgroundColor = Color.black;
                this.titleText.color = Color.white;
                this.subtitleText.color = Color.white;
            }

            if (GroundOne.WE2.Account != null && GroundOne.WE2.Account != String.Empty)
            {
                groupAccount.SetActive(false);
                GroupMenu.SetActive(true);
            }
            else
            {
                groupAccount.SetActive(true);
                GroupMenu.SetActive(false);
            }
        }
        
        public void enemy_click(Text txtName)
        {
            GroundOne.WE.AvailableMixSpellSkill = true;
            GroundOne.WE2.AvailableMixSpellSkill = true;
            GroundOne.WE.AvailableInstantCommand = true;
                
            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Strength = Convert.ToInt32(DebugStrength.text);
            GroundOne.MC.Agility = Convert.ToInt32(DebugAgility.text);
            GroundOne.MC.Intelligence = Convert.ToInt32(DebugIntelligence.text);
            GroundOne.MC.Stamina = Convert.ToInt32(DebugStamina.text);
            GroundOne.MC.Mind = Convert.ToInt32(DebugMind.text);
            GroundOne.MC.Dead = false;

            GroundOne.MC.FreshHeal = true;
            GroundOne.MC.Protection = true;
            GroundOne.MC.HolyShock = true;
            GroundOne.MC.SaintPower = true;
            GroundOne.MC.Glory = true;
            GroundOne.MC.Resurrection = true;
            GroundOne.MC.CelestialNova = true;
            GroundOne.MC.DarkBlast = true;
            GroundOne.MC.ShadowPact = true;
            GroundOne.MC.LifeTap = true;
            GroundOne.MC.DevouringPlague = true;
            GroundOne.MC.BlackContract = true;
            GroundOne.MC.BloodyVengeance = true;
            GroundOne.MC.Damnation = true;
            GroundOne.MC.FireBall = true;
            GroundOne.MC.FlameAura = true;
            GroundOne.MC.HeatBoost = true;
            GroundOne.MC.VolcanicWave = true;
            GroundOne.MC.FlameStrike = true;
            GroundOne.MC.ImmortalRave = true;
            GroundOne.MC.LavaAnnihilation = true;
            GroundOne.MC.IceNeedle = true;
            GroundOne.MC.AbsorbWater = true;
            GroundOne.MC.Cleansing = true;
            GroundOne.MC.MirrorImage = true;
            GroundOne.MC.FrozenLance = true;
            GroundOne.MC.PromisedKnowledge = true;
            GroundOne.MC.AbsoluteZero = true;
            GroundOne.MC.WordOfPower = true;
            GroundOne.MC.GaleWind = true;
            GroundOne.MC.WordOfLife = true;
            GroundOne.MC.WordOfFortune = true;
            GroundOne.MC.AetherDrive = true;
            GroundOne.MC.Genesis = true;
            GroundOne.MC.EternalPresence = true;
            GroundOne.MC.DispelMagic = true;
            GroundOne.MC.RiseOfImage = true;
            GroundOne.MC.Tranquility = true;
            GroundOne.MC.Deflection = true;
            GroundOne.MC.OneImmunity = true;
            GroundOne.MC.WhiteOut = true;
            GroundOne.MC.TimeStop = true;
            GroundOne.MC.StraightSmash = true;
            GroundOne.MC.DoubleSlash = true;
            GroundOne.MC.CrushingBlow = true;
            GroundOne.MC.SoulInfinity = true;
            GroundOne.MC.CounterAttack = true;
            GroundOne.MC.PurePurification = true;
            GroundOne.MC.AntiStun = true;
            GroundOne.MC.StanceOfDeath = true;
            GroundOne.MC.StanceOfFlow = true;
            GroundOne.MC.EnigmaSence = true;
            GroundOne.MC.SilentRush = true;
            GroundOne.MC.OboroImpact = true;
            GroundOne.MC.StanceOfStanding = true;
            GroundOne.MC.InnerInspiration = true;
            GroundOne.MC.KineticSmash = true;
            GroundOne.MC.Catastrophe = true;
            GroundOne.MC.TruthVision = true;
            GroundOne.MC.HighEmotionality = true;
            GroundOne.MC.StanceOfEyes = true;
            GroundOne.MC.PainfulInsanity = true;
            GroundOne.MC.Negate = true;
            GroundOne.MC.VoidExtraction = true;
            GroundOne.MC.CarnageRush = true;
            GroundOne.MC.NothingOfNothingness = true;
            GroundOne.MC.PsychicTrance = true;
            GroundOne.MC.BlindJustice = true;
            GroundOne.MC.TranscendentWish = true;
            GroundOne.MC.FlashBlaze = true;
            GroundOne.MC.LightDetonator = true;
            GroundOne.MC.AscendantMeteor = true;
            GroundOne.MC.SkyShield = true;
            GroundOne.MC.SacredHeal = true;
            GroundOne.MC.EverDroplet = true;
            GroundOne.MC.HolyBreaker = true;
            GroundOne.MC.ExaltedField = true;
            GroundOne.MC.HymnContract = true;
            GroundOne.MC.StarLightning = true;
            GroundOne.MC.AngelBreath = true;
            GroundOne.MC.EndlessAnthem = true;
            GroundOne.MC.BlackFire = true;
            GroundOne.MC.BlazingField = true;
            GroundOne.MC.DemonicIgnite = true;
            GroundOne.MC.BlueBullet = true;
            GroundOne.MC.DeepMirror = true;
            GroundOne.MC.DeathDeny = true;
            GroundOne.MC.WordOfMalice = true;
            GroundOne.MC.AbyssEye = true;
            GroundOne.MC.SinFortune = true;
            GroundOne.MC.DarkenField = true;
            GroundOne.MC.DoomBlade = true;
            GroundOne.MC.EclipseEnd = true;
            GroundOne.MC.FrozenAura = true;
            GroundOne.MC.ChillBurn = true;
            GroundOne.MC.ZetaExplosion = true;
            GroundOne.MC.EnrageBlast = true;
            GroundOne.MC.PiercingFlame = true;
            GroundOne.MC.SigilOfHomura = true;
            GroundOne.MC.Immolate = true;
            GroundOne.MC.PhantasmalWind = true;
            GroundOne.MC.RedDragonWill = true;
            GroundOne.MC.WordOfAttitude = true;
            GroundOne.MC.StaticBarrier = true;
            GroundOne.MC.AusterityMatrix = true;
            GroundOne.MC.VanishWave = true;
            GroundOne.MC.VortexField = true;
            GroundOne.MC.BlueDragonWill = true;
            GroundOne.MC.SeventhMagic = true;
            GroundOne.MC.ParadoxImage = true;
            GroundOne.MC.WarpGate = true;
            GroundOne.MC.NeutralSmash = true;
            GroundOne.MC.StanceOfDouble = true;
            GroundOne.MC.SwiftStep = true;
            GroundOne.MC.VigorSense = true;
            GroundOne.MC.CircleSlash = true;
            GroundOne.MC.RisingAura = true;
            GroundOne.MC.RumbleShout = true;
            GroundOne.MC.OnslaughtHit = true;
            GroundOne.MC.ColorlessMove = true;
            GroundOne.MC.AscensionAura = true;
            GroundOne.MC.FutureVision = true;
            GroundOne.MC.UnknownShock = true;
            GroundOne.MC.ReflexSpirit = true;
            GroundOne.MC.FatalBlow = true;
            GroundOne.MC.SharpGlare = true;
            GroundOne.MC.ConcussiveHit = true;
            GroundOne.MC.TrustSilence = true;
            GroundOne.MC.MindKilling = true;
            GroundOne.MC.SurpriseAttack = true;
            GroundOne.MC.StanceOfMystic = true;
            GroundOne.MC.PsychicWave = true;
            GroundOne.MC.NourishSense = true;
            GroundOne.MC.Recover = true;
            GroundOne.MC.ImpulseHit = true;
            GroundOne.MC.ViolentSlash = true;
            GroundOne.MC.ONEAuthority = true;
            GroundOne.MC.OuterInspiration = true;
            GroundOne.MC.HardestParry = true;
            GroundOne.MC.StanceOfSuddenness = true;
            GroundOne.MC.SoulExecution = true; 
            GroundOne.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            GroundOne.MC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            GroundOne.MC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            GroundOne.MC.MaxGain();
            GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

            if (toggleB2.isOn)
            {
                GroundOne.WE.AvailableSecondCharacter = true;
                GroundOne.SC.FirstName = Database.RANA_AMILIA;
                GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;
                GroundOne.SC.Strength = Convert.ToInt32(DebugStrength.text);
                GroundOne.SC.Agility = Convert.ToInt32(DebugAgility.text);
                GroundOne.SC.Intelligence = Convert.ToInt32(DebugIntelligence.text);
                GroundOne.SC.Stamina = Convert.ToInt32(DebugStamina.text);
                GroundOne.SC.Mind = Convert.ToInt32(DebugMind.text);
                GroundOne.SC.Dead = false;

                GroundOne.WE.AvailableMixSpellSkill = true;
                GroundOne.SC.FreshHeal = true;
                GroundOne.SC.Protection = true;
                GroundOne.SC.HolyShock = true;
                GroundOne.SC.SaintPower = true;
                GroundOne.SC.Glory = true;
                GroundOne.SC.Resurrection = true;
                GroundOne.SC.CelestialNova = true;
                GroundOne.SC.DarkBlast = true;
                GroundOne.SC.ShadowPact = true;
                GroundOne.SC.LifeTap = true;
                GroundOne.SC.DevouringPlague = true;
                GroundOne.SC.BlackContract = true;
                GroundOne.SC.BloodyVengeance = true;
                GroundOne.SC.Damnation = true;
                GroundOne.SC.FireBall = true;
                GroundOne.SC.FlameAura = true;
                GroundOne.SC.HeatBoost = true;
                GroundOne.SC.VolcanicWave = true;
                GroundOne.SC.FlameStrike = true;
                GroundOne.SC.ImmortalRave = true;
                GroundOne.SC.LavaAnnihilation = true;
                GroundOne.SC.IceNeedle = true;
                GroundOne.SC.AbsorbWater = true;
                GroundOne.SC.Cleansing = true;
                GroundOne.SC.MirrorImage = true;
                GroundOne.SC.FrozenLance = true;
                GroundOne.SC.PromisedKnowledge = true;
                GroundOne.SC.AbsoluteZero = true;
                GroundOne.SC.WordOfPower = true;
                GroundOne.SC.GaleWind = true;
                GroundOne.SC.WordOfLife = true;
                GroundOne.SC.WordOfFortune = true;
                GroundOne.SC.AetherDrive = true;
                GroundOne.SC.Genesis = true;
                GroundOne.SC.EternalPresence = true;
                GroundOne.SC.DispelMagic = true;
                GroundOne.SC.RiseOfImage = true;
                GroundOne.SC.Tranquility = true;
                GroundOne.SC.Deflection = true;
                GroundOne.SC.OneImmunity = true;
                GroundOne.SC.WhiteOut = true;
                GroundOne.SC.TimeStop = true;
                GroundOne.SC.StraightSmash = true;
                GroundOne.SC.DoubleSlash = true;
                GroundOne.SC.CrushingBlow = true;
                GroundOne.SC.SoulInfinity = true;
                GroundOne.SC.CounterAttack = true;
                GroundOne.SC.PurePurification = true;
                GroundOne.SC.AntiStun = true;
                GroundOne.SC.StanceOfDeath = true;
                GroundOne.SC.StanceOfFlow = true;
                GroundOne.SC.EnigmaSence = true;
                GroundOne.SC.SilentRush = true;
                GroundOne.SC.OboroImpact = true;
                GroundOne.SC.StanceOfStanding = true;
                GroundOne.SC.InnerInspiration = true;
                GroundOne.SC.KineticSmash = true;
                GroundOne.SC.Catastrophe = true;
                GroundOne.SC.TruthVision = true;
                GroundOne.SC.HighEmotionality = true;
                GroundOne.SC.StanceOfEyes = true;
                GroundOne.SC.PainfulInsanity = true;
                GroundOne.SC.Negate = true;
                GroundOne.SC.VoidExtraction = true;
                GroundOne.SC.CarnageRush = true;
                GroundOne.SC.NothingOfNothingness = true;
                GroundOne.SC.PsychicTrance = true;
                GroundOne.SC.BlindJustice = true;
                GroundOne.SC.TranscendentWish = true;
                GroundOne.SC.FlashBlaze = true;
                GroundOne.SC.LightDetonator = true;
                GroundOne.SC.AscendantMeteor = true;
                GroundOne.SC.SkyShield = true;
                GroundOne.SC.SacredHeal = true;
                GroundOne.SC.EverDroplet = true;
                GroundOne.SC.HolyBreaker = true;
                GroundOne.SC.ExaltedField = true;
                GroundOne.SC.HymnContract = true;
                GroundOne.SC.StarLightning = true;
                GroundOne.SC.AngelBreath = true;
                GroundOne.SC.EndlessAnthem = true;
                GroundOne.SC.BlackFire = true;
                GroundOne.SC.BlazingField = true;
                GroundOne.SC.DemonicIgnite = true;
                GroundOne.SC.BlueBullet = true;
                GroundOne.SC.DeepMirror = true;
                GroundOne.SC.DeathDeny = true;
                GroundOne.SC.WordOfMalice = true;
                GroundOne.SC.AbyssEye = true;
                GroundOne.SC.SinFortune = true;
                GroundOne.SC.DarkenField = true;
                GroundOne.SC.DoomBlade = true;
                GroundOne.SC.EclipseEnd = true;
                GroundOne.SC.FrozenAura = true;
                GroundOne.SC.ChillBurn = true;
                GroundOne.SC.ZetaExplosion = true;
                GroundOne.SC.EnrageBlast = true;
                GroundOne.SC.PiercingFlame = true;
                GroundOne.SC.SigilOfHomura = true;
                GroundOne.SC.Immolate = true;
                GroundOne.SC.PhantasmalWind = true;
                GroundOne.SC.RedDragonWill = true;
                GroundOne.SC.WordOfAttitude = true;
                GroundOne.SC.StaticBarrier = true;
                GroundOne.SC.AusterityMatrix = true;
                GroundOne.SC.VanishWave = true;
                GroundOne.SC.VortexField = true;
                GroundOne.SC.BlueDragonWill = true;
                GroundOne.SC.SeventhMagic = true;
                GroundOne.SC.ParadoxImage = true;
                GroundOne.SC.WarpGate = true;
                GroundOne.SC.NeutralSmash = true;
                GroundOne.SC.StanceOfDouble = true;
                GroundOne.SC.SwiftStep = true;
                GroundOne.SC.VigorSense = true;
                GroundOne.SC.CircleSlash = true;
                GroundOne.SC.RisingAura = true;
                GroundOne.SC.RumbleShout = true;
                GroundOne.SC.OnslaughtHit = true;
                GroundOne.SC.ColorlessMove = true;
                GroundOne.SC.AscensionAura = true;
                GroundOne.SC.FutureVision = true;
                GroundOne.SC.UnknownShock = true;
                GroundOne.SC.ReflexSpirit = true;
                GroundOne.SC.FatalBlow = true;
                GroundOne.SC.SharpGlare = true;
                GroundOne.SC.ConcussiveHit = true;
                GroundOne.SC.TrustSilence = true;
                GroundOne.SC.MindKilling = true;
                GroundOne.SC.SurpriseAttack = true;
                GroundOne.SC.StanceOfMystic = true;
                GroundOne.SC.PsychicWave = true;
                GroundOne.SC.NourishSense = true;
                GroundOne.SC.Recover = true;
                GroundOne.SC.ImpulseHit = true;
                GroundOne.SC.ViolentSlash = true;
                GroundOne.SC.ONEAuthority = true;
                GroundOne.SC.OuterInspiration = true;
                GroundOne.SC.HardestParry = true;
                GroundOne.SC.StanceOfSuddenness = true;
                GroundOne.SC.SoulExecution = true;
                GroundOne.SC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                GroundOne.SC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
                GroundOne.SC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
                GroundOne.SC.MaxGain();
                GroundOne.SC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.SC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            }

            if (toggleB3.isOn)
            {
                GroundOne.WE.AvailableThirdCharacter = true;
                GroundOne.TC.FirstName = Database.OL_LANDIS;
                GroundOne.TC.FullName = Database.OL_LANDIS_FULL;
                GroundOne.TC.Strength = Convert.ToInt32(DebugStrength.text);
                GroundOne.TC.Agility = Convert.ToInt32(DebugAgility.text);
                GroundOne.TC.Intelligence = Convert.ToInt32(DebugIntelligence.text);
                GroundOne.TC.Stamina = Convert.ToInt32(DebugStamina.text);
                GroundOne.TC.Mind = Convert.ToInt32(DebugMind.text);
                GroundOne.TC.Dead = false;

                GroundOne.WE.AvailableMixSpellSkill = true;
                GroundOne.TC.FreshHeal = true;
                GroundOne.TC.Protection = true;
                GroundOne.TC.HolyShock = true;
                GroundOne.TC.SaintPower = true;
                GroundOne.TC.Glory = true;
                GroundOne.TC.Resurrection = true;
                GroundOne.TC.CelestialNova = true;
                GroundOne.TC.DarkBlast = true;
                GroundOne.TC.ShadowPact = true;
                GroundOne.TC.LifeTap = true;
                GroundOne.TC.DevouringPlague = true;
                GroundOne.TC.BlackContract = true;
                GroundOne.TC.BloodyVengeance = true;
                GroundOne.TC.Damnation = true;
                GroundOne.TC.FireBall = true;
                GroundOne.TC.FlameAura = true;
                GroundOne.TC.HeatBoost = true;
                GroundOne.TC.VolcanicWave = true;
                GroundOne.TC.FlameStrike = true;
                GroundOne.TC.ImmortalRave = true;
                GroundOne.TC.LavaAnnihilation = true;
                GroundOne.TC.IceNeedle = true;
                GroundOne.TC.AbsorbWater = true;
                GroundOne.TC.Cleansing = true;
                GroundOne.TC.MirrorImage = true;
                GroundOne.TC.FrozenLance = true;
                GroundOne.TC.PromisedKnowledge = true;
                GroundOne.TC.AbsoluteZero = true;
                GroundOne.TC.WordOfPower = true;
                GroundOne.TC.GaleWind = true;
                GroundOne.TC.WordOfLife = true;
                GroundOne.TC.WordOfFortune = true;
                GroundOne.TC.AetherDrive = true;
                GroundOne.TC.Genesis = true;
                GroundOne.TC.EternalPresence = true;
                GroundOne.TC.DispelMagic = true;
                GroundOne.TC.RiseOfImage = true;
                GroundOne.TC.Tranquility = true;
                GroundOne.TC.Deflection = true;
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
                GroundOne.TC.PsychicTrance = true;
                GroundOne.TC.BlindJustice = true;
                GroundOne.TC.TranscendentWish = true;
                GroundOne.TC.FlashBlaze = true;
                GroundOne.TC.LightDetonator = true;
                GroundOne.TC.AscendantMeteor = true;
                GroundOne.TC.SkyShield = true;
                GroundOne.TC.SacredHeal = true;
                GroundOne.TC.EverDroplet = true;
                GroundOne.TC.HolyBreaker = true;
                GroundOne.TC.ExaltedField = true;
                GroundOne.TC.HymnContract = true;
                GroundOne.TC.StarLightning = true;
                GroundOne.TC.AngelBreath = true;
                GroundOne.TC.EndlessAnthem = true;
                GroundOne.TC.BlackFire = true;
                GroundOne.TC.BlazingField = true;
                GroundOne.TC.DemonicIgnite = true;
                GroundOne.TC.BlueBullet = true;
                GroundOne.TC.DeepMirror = true;
                GroundOne.TC.DeathDeny = true;
                GroundOne.TC.WordOfMalice = true;
                GroundOne.TC.AbyssEye = true;
                GroundOne.TC.SinFortune = true;
                GroundOne.TC.DarkenField = true;
                GroundOne.TC.DoomBlade = true;
                GroundOne.TC.EclipseEnd = true;
                GroundOne.TC.FrozenAura = true;
                GroundOne.TC.ChillBurn = true;
                GroundOne.TC.ZetaExplosion = true;
                GroundOne.TC.EnrageBlast = true;
                GroundOne.TC.PiercingFlame = true;
                GroundOne.TC.SigilOfHomura = true;
                GroundOne.TC.Immolate = true;
                GroundOne.TC.PhantasmalWind = true;
                GroundOne.TC.RedDragonWill = true;
                GroundOne.TC.WordOfAttitude = true;
                GroundOne.TC.StaticBarrier = true;
                GroundOne.TC.AusterityMatrix = true;
                GroundOne.TC.VanishWave = true;
                GroundOne.TC.VortexField = true;
                GroundOne.TC.BlueDragonWill = true;
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
                GroundOne.TC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                GroundOne.TC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
                GroundOne.TC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
                GroundOne.TC.MaxGain();
                GroundOne.TC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.TC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            }

            GroundOne.enemyName1 = txtName.text;
            if (toggle2.isOn)
            {
                GroundOne.enemyName2 = Database.DUEL_DUMMY_SUBURI;
            }
            if (toggle3.isOn)
            {
                GroundOne.enemyName3 = txtName.text;
            }
            SceneDimension.CallTruthBattleEnemy(Database.Title, toggleDuel.isOn, false, false, false);
        }

        public void tapExit()
        {
            GroupSystemMessage.SetActive(false);
        }

        public void tapAccountOK(Text account)
        {
            GroundOne.SQL.CreateOwner(account.text);
            GroundOne.WE2.Account = account.text;
            Method.AutoSaveTruthWorldEnvironment();
            groupAccount.SetActive(false);
            GroupMenu.SetActive(true);
        }

        public void GameStart_Click()
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                SystemMessageText.text = "アイン・ウォーレンスが並行世界へ突入している事により、新しく始める事はできません。";
                GroupSystemMessage.SetActive(true);
                return;
            }

            GroundOne.SQL.UpdateOwner(Database.LOG_GAME_START, String.Empty, String.Empty);
            GroundOne.SQL.UpdateArchivement(Database.ARCHIVEMENT_FIRST_TRY);
            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
            GroundOne.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
            GroundOne.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
            GroundOne.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
            GroundOne.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
            GroundOne.MC.AvailableMana = true;
            GroundOne.MC.AvailableSkill = true;
            GroundOne.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            GroundOne.MC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            GroundOne.MC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            GroundOne.MC.MaxGain();
            GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

            GroundOne.SC.FirstName = Database.RANA_AMILIA;
            GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;
            GroundOne.SC.Strength = Database.SECONDPLAYER_FIRST_STRENGTH;
            GroundOne.SC.Intelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
            GroundOne.SC.Agility = Database.SECONDPLAYER_FIRST_AGILITY;
            GroundOne.SC.Stamina = Database.SECONDPLAYER_FIRST_STAMINA;
            GroundOne.SC.Mind = Database.SECONDPLAYER_FIRST_MIND;
            GroundOne.SC.AvailableMana = true;
            GroundOne.SC.AvailableSkill = true;
            GroundOne.SC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_KNUCKLE);
            GroundOne.SC.MainArmor = new ItemBackPack(Database.POOR_LIGHT_CROSS);
            GroundOne.SC.Accessory = new ItemBackPack(Database.COMMON_SANGO_BRESLET);
            GroundOne.SC.MaxGain();
            GroundOne.SC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.SC.BattleActionCommandList[1] = Database.DEFENSE_EN;

            SceneDimension.JumpToTruthHomeTown();
        }

        public void Load_Click()
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                SystemMessageText.text = "アイン・ウォーレンスが並行世界へ突入している事により、ロードを行う事はできません。";
                GroupSystemMessage.SetActive(true);
                return;
            }
            GroundOne.SQL.UpdateOwner(Database.LOG_LOAD_GAME, String.Empty, String.Empty);                
            this.Filter.SetActive(true);
            SceneDimension.CallSaveLoad(this, false, false);
        }

        public void Config_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_CONFIG, String.Empty, String.Empty);
        }

        public void Exit_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_EXIT_GAME, String.Empty, String.Empty);
            Application.Quit();
        }

        public void Seeker_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_START_SEEKER, String.Empty, String.Empty);
            GroundOne.WE2.StartSeeker = true;
            Method.AutoSaveTruthWorldEnvironment();

            GroundOne.StopDungeonMusic();
            Method.ExecLoad(null, Database.WorldSaveNum, true);
            if (GroundOne.WE.SaveByDungeon)
            {
                SceneDimension.JumpToTruthDungeon(false);
            }
            else
            {
                SceneDimension.JumpToTruthHomeTown();
            }
        }
    }
}
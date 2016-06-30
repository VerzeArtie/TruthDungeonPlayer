using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class Title : MotherForm
    {
        public Camera cam;
        public Text titleText;
        public Text subtitleText;
        public GameObject GroupSystemMessage;
        public Text SystemMessageText;
        public GameObject buttonGamestart;
        public GameObject buttonSeeker;
        public GameObject buttonLoad;
        public GameObject buttonConfig;
        public GameObject buttonExit;
        public override void Start()
        {
            base.Start();
            if (GroundOne.EnableBGM)
            {
                GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin); // 後編追加    
            }

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
        }

        // debug
        public Text DebugStrength;
        public Text DebugAgility;
        public Text DebugIntelligence;
        public Text DebugStamina;
        public Text DebugMind;
        public void enemy_click(Text txtName)
        {
            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Strength = Convert.ToInt32(DebugStrength.text);
            GroundOne.MC.Agility = Convert.ToInt32(DebugAgility.text);
            GroundOne.MC.Intelligence = Convert.ToInt32(DebugIntelligence.text);
            GroundOne.MC.Stamina = Convert.ToInt32(DebugStamina.text);
            GroundOne.MC.Mind = Convert.ToInt32(DebugMind.text);
            GroundOne.MC.Dead = false;

            GroundOne.WE.AvailableMixSpellSkill = true;
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
            GroundOne.MC.SmoothingMove = true;
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
            GroundOne.enemyName1 = txtName.text;
            SceneDimension.CallTruthBattleEnemy(Database.Title, false, false, false, false);
        }

        public void tapExit()
        {
            GroupSystemMessage.SetActive(false);
        }

        public void GameStart_Click()
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                SystemMessageText.text = "アイン・ウォーレンスが並行世界へ突入している事により、新しく始める事はできません。";
                GroupSystemMessage.SetActive(true);
                return;
            }

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
            this.Filter.SetActive(true);
            SceneDimension.CallSaveLoad(this, false, false);
        }

        public void Config_Click()
        {
        }

        public void Exit_Click()
        {
            Application.Quit();
        }

        public void Seeker_Click()
        {
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

        public void PointerEnter()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_LIFE_TAP);
        }
    }
}
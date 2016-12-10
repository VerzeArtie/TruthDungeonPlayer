using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace DungeonPlayer
{
    public class Tutorial : MotherForm
    {
        public bool toggle2 = false;
        public bool toggle3 = false;
        public bool toggleB2 = false;
        public bool toggleB3 = false;
        public bool toggleDuel = false;
        public Text description = null;
        public Button buttonHomeTown = null;

        private int selectNumber = 0;

        public override void Start()
        {
            Home_Click();
        }

        public override void Update()
        {
        }
        
        public void Home_Click()
        {
            Debug.Log("Home_Click");
            this.selectNumber = 0;

            this.description.text = "ホームタウンでの基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "メインメニュー\r\n";
            this.description.text += "  【ステータス】　　　　　　　キャラクターのステータス画面を表示します。\r\n";
            this.description.text += "  【バトル設定】　　　　　　　戦闘コマンドの設定を行う画面を表示します。\r\n";
            this.description.text += "  【セーブ】　　　　　　　　　ゲームをセーブします。\r\n";
            this.description.text += "  【ロード】　　　　　　　　　ゲームをロードします。\r\n";
            this.description.text += "  【ゲーム終了】　　　　　　　本ゲームを終了します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "イベントメニュー\r\n";
            this.description.text += "  【DungeonPlayer!】　　　　　ダンジョンを開始します。\r\n";
            this.description.text += "  【幼なじみのラナと会話】　　ラナと会話を行います。\r\n";
            this.description.text += "  【天下一品ガンツの武具店】　武具の売買を行います。\r\n";
            this.description.text += "  【ハンナのゆったり宿屋】　　宿屋に宿泊します。\r\n";
        }

        public void Dungeon_Click()
        {
            Debug.Log("Dungeon_Click");
            this.selectNumber = 1;

            this.description.text = "ダンジョンの基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "基本操作\r\n";
            this.description.text += "　画面上部をタップ　　        上へ移動します。\r\n";
            this.description.text += "　画面左部をタップ　　        左へ移動します。\r\n";
            this.description.text += "　画面右部をタップ　　        右へ移動します。\r\n";
            this.description.text += "　画面下部をタップ　　        下へ移動します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "サブメニュー\r\n";
            this.description.text += "  【ブック】アイコン　　　    本ゲームで登場する戦闘コマンドの説明を表示します。\r\n";
            this.description.text += "  【トーク】アイコン　　　    本編の会話内容を履歴表示します。\r\n";
            this.description.text += "  【マップ】アイコン　　　    ダンジョンを表示モードに切り替えます。\r\n";
            this.description.text += "  【 水晶 】アイコン　　　    ダンジョンからホームタウンへ帰還します。\r\n";
            this.description.text += "  【 索敵 】アイコン　　　    モンスターを索敵するモードに切り替えます。\r\n";
            this.description.text += "  \r\n";
            this.description.text += "メインメニュー\r\n";
            this.description.text += "  （ホームタウンのメニューと同等です。)\r\n";
        }

        public void Battle_Click()
        {
            Debug.Log("Battle_Click");
            this.selectNumber = 2;

            this.description.text = "戦闘時の基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "アクションコマンド\r\n";
            this.description.text += "　【戦闘コマンド】リスト　　　戦闘コマンドから行動させるアクションを決定します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "メニュー\r\n";
            this.description.text += "　【アイテム】アイコン　　　　アイテムを使用します。\r\n";
            this.description.text += "　【バトル設定】アイコン　　　戦闘コマンドを再設定します。\r\n";
            this.description.text += "　【逃げる】アイコン　　　　　戦闘から離脱します。\r\n";
            this.description.text += "　【一時停止】アイコン　　　　戦闘を一時停止させます。\r\n";
            this.description.text += "\r\n";
            this.description.text += "その他\r\n";
            this.description.text += "　【戦闘速度】ゲージバー　　　戦闘速度を変更します。\r\n";
            this.description.text += "　【戦闘履歴】ウィンドウ　　　戦闘イベントの履歴を確認できます。\r\n";
        }

        public void Status_Click()
        {
            Debug.Log("Status_Click");
            this.selectNumber = 3;

            this.description.text = "キャラクターステータス画面での基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "タブ一覧\r\n";
            this.description.text += "　【ステータス】　　　　　　　キャラクターのコアパラメタ、装備、戦闘値を表示します。\r\n";
            this.description.text += "　【バックパック】　　　　　　現在保持しているアイテムリストを表示します。\r\n";
            this.description.text += "　【スペル】　　　　　　　　　非戦闘時に使用可能な魔法を表示します。\r\n";
            this.description.text += "　【レジスト】　　　　　　　　キャラクターの各種耐性を表示します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "レベルアップ時\r\n";
            this.description.text += "　【力】アイコンをタップ　　　【力】パラメタをUPさせます。\r\n";
            this.description.text += "　【技】アイコンをタップ　　　【技】パラメタをUPさせます。\r\n";
            this.description.text += "　【知】アイコンをタップ　　　【知】パラメタをUPさせます。\r\n";
            this.description.text += "　【体】アイコンをタップ　　　【体】パラメタをUPさせます。\r\n";
            this.description.text += "　【心】アイコンをタップ　　　【心】パラメタをUPさせます。\r\n";
        }

        public void Duel_Click()
        {
            Debug.Log("Duel_Click");
            this.selectNumber = 5;

            this.description.text = "DUEL戦闘時における基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "　DUEL戦においては基本的には通常戦闘と同様に楽しめます。\r\n";
            this.description.text += "　ただし通常戦闘と比べ、いくつかルールの違いがあります。\r\n";
            this.description.text += "　通常とは少し違った感覚でDUEL戦を楽しんでください。\r\n";
            this.description.text += "\r\n";
            this.description.text += "【基本ルール】\r\n";
            this.description.text += "　・必ず１ｖｓ１での戦闘となります。\r\n";
            this.description.text += "　・行動ゲージバーは、一番初め（左部）から開始となります。\r\n";
            this.description.text += "　・DUEL開始後、【一時停止】が選べなくなります。\r\n";
            this.description.text += "　・インスタントで行動を行う際、＜行動スタック＞が画面中央に入ります。\r\n";
            this.description.text += "　・＜行動スタック＞が積まれている間、更に＜行動スタック＞を乗せる事ができます。\r\n";
            this.description.text += "　　一定時間経過した後、一番最後の＜行動スタック＞が一番初めに実行されます。\r\n";
            this.description.text += "\r\n";
            this.description.text += "【Tips】\r\n";
            this.description.text += "　インスタント行動の中には、＜行動スタック＞を打ち消すコマンドも存在します。\r\n";
            this.description.text += "　対戦相手の行動特性をよく見極めて、タイミングよく行動しましょう。\r\n";
        }

        public void Level_Click(int level)
        {
            Debug.Log("Level_Click");
        }

        public void GoTutorial_Click()
        {
            GroundOne.TutorialMode = true;

            if (this.selectNumber == 0)
            {
                GroundOne.WE.AlreadyRest = true;
                SceneDimension.JumpToTruthHomeTown();
            }
            else if (this.selectNumber == 1)
            {
                SceneDimension.JumpToTruthDungeon(false);
            }
            else if (this.selectNumber == 2)
            {
                enemy_click();
            }
            else if (this.selectNumber == 3)
            {
                bool dummy1 = false;
                int dummy2 = 0;
                int dummy3 = 0;
                SceneDimension.CallTruthStatusPlayer(this, ref dummy1, ref dummy2, ref dummy3, GroundOne.MC.PlayerStatusColor);
            }
            else if (this.selectNumber == 4)
            {
            }
            else if (this.selectNumber == 5)
            {
            }
        }
    
        public void Close_Click()
        {
            GroundOne.TutorialMode = false;
            GroundOne.ReInitializeGroundOne(false);
            SceneDimension.Back(this);
        }

        public void enemy_click()
        {
            GroundOne.WE.AvailableMixSpellSkill = true;
            GroundOne.WE2.AvailableMixSpellSkill = true;
            GroundOne.WE.AvailableInstantCommand = true;

            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Level = 1;
            GroundOne.MC.Strength = 2;
            GroundOne.MC.Agility = 5;
            GroundOne.MC.Intelligence = 1;
            GroundOne.MC.Stamina = 2;
            GroundOne.MC.Mind = 2;
            GroundOne.MC.Dead = false;
            //GroundOne.MC.Accessory = new ItemBackPack(Database.EPIC_ADILRING_OF_BLUE_BURN);
            GroundOne.MC.Level = 0;
            GroundOne.MC.BaseLife = 0;
            GroundOne.MC.BaseMana = 0;
            for (int ii = 0; ii < GroundOne.MC.Level; ii++)
            {
                GroundOne.MC.BaseLife += GroundOne.MC.LevelUpLifeTruth;
                GroundOne.MC.BaseMana += GroundOne.MC.LevelUpManaTruth;
                GroundOne.MC.Level++;
            }

            GroundOne.MC.FreshHeal = true;

            GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
            GroundOne.MC.SubWeapon = null;
            GroundOne.MC.MainArmor = null;
            GroundOne.MC.MaxGain();
            GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

            if (toggleB2)
            {
                GroundOne.WE.AvailableSecondCharacter = true;
                GroundOne.SC.FirstName = Database.RANA_AMILIA;
                GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;
                GroundOne.SC.Level = 6;
                GroundOne.SC.Strength = 8;
                GroundOne.SC.Agility = 7;
                GroundOne.SC.Intelligence = 15;
                GroundOne.SC.Stamina = 6;
                GroundOne.SC.Mind = 5;
                GroundOne.SC.Dead = false;
                for (int ii = 0; ii < GroundOne.SC.Level; ii++)
                {
                    GroundOne.SC.BaseLife += GroundOne.SC.LevelUpLifeTruth;
                    GroundOne.SC.BaseMana += GroundOne.SC.LevelUpManaTruth;
                    GroundOne.SC.Level++;
                }
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

            if (toggleB3)
            {
                GroundOne.WE.AvailableThirdCharacter = true;
                GroundOne.TC.FirstName = Database.OL_LANDIS;
                GroundOne.TC.FullName = Database.OL_LANDIS_FULL;
                GroundOne.TC.Level = 7;
                GroundOne.TC.Strength = 16;
                GroundOne.TC.Agility = 12;
                GroundOne.TC.Intelligence = 6;
                GroundOne.TC.Stamina = 11;
                GroundOne.TC.Mind = 5;
                GroundOne.TC.Dead = false;
                for (int ii = 0; ii < GroundOne.TC.Level; ii++)
                {
                    GroundOne.TC.BaseLife += GroundOne.TC.LevelUpLifeTruth;
                    GroundOne.TC.BaseMana += GroundOne.TC.LevelUpManaTruth;
                    GroundOne.TC.Level++;
                }

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

            GroundOne.enemyName1 = Database.ENEMY_TINY_MANTIS;
            GroundOne.enemyName2 = String.Empty;
            GroundOne.enemyName3 = String.Empty;

            if (toggle2)
            {
                GroundOne.enemyName2 = Database.ENEMY_GREEN_CHILD;
            }
            if (toggle3)
            {
                GroundOne.enemyName3 = Database.ENEMY_TINY_MANTIS;
            }
            SceneDimension.CallTruthBattleEnemy(Database.Title, toggleDuel, false, false, false);
        }

    }
}
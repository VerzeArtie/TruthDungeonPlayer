using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MonoBehaviour
    {
        public enum CriticalType
        {
            None,
            Random,
            Absolute,
        }

        string instantActionCommandString = String.Empty;
        MainCharacter tempTargetForInstant = null;
        MainCharacter tempTargetForTarget2 = null;
        MainCharacter tempTargetForTarget = null;

        bool NowSelectingTarget = false;
        MainCharacter currentTargetedPlayer = null;
        bool tempStopFlag = false; // [戦闘停止」ボタンやESCキーで、戦闘を一旦停止させたい時に使うフラグ
        bool endFlag = false; // メイン戦闘のループを抜ける時に使うフラグ
        bool cannotRunAway = false; // 戦闘から逃げられるかどうかを示すフラグ
        public bool DuelMode { get; set; }
        bool NowStackInTheCommand = false; // スタックインザコマンドで一旦停止させたい時に使うフラグ
        bool NowTimeStop = false; // タイムストップ「全体」のフラグ
        public bool HiSpeedAnimation { get; set; } // 通常ダメージアニメーションを早めるために使用
        public bool FinalBattle { get; set; } // 最終戦闘、スタックコマンドの動作を早めるために使用
        public bool LifeCountBattle { get; set; } // 最終戦闘でライフカウントを表現するために使用

        private TruthImage[] pbBuffPlayer1;
        private TruthImage[] pbBuffPlayer2;
        private TruthImage[] pbBuffPlayer3;
        private TruthImage[] pbBuffEnemy1;
        private TruthImage[] pbBuffEnemy2;
        private TruthImage[] pbBuffEnemy3;


        // resource
        public Sprite[] imageSandglass;

        public Sprite imgAttack;
        public Sprite imgDefense;
        public Sprite imgFireBall;
        public Sprite imgStraightSmash;
        public Sprite imgFlameAura;
        public Sprite imgProtection;
        public Sprite imgShadowPact;
        public Sprite imgWordOfLife;
        public Sprite imgDeflection;
        public Sprite imgTruthVision;
        public Sprite imgPoison;

        // GUI
        public Image debugimage;

        public GameObject popupInfo;
        public Text CurrentInfo;
        public Text BattleStart;
        public Text debugMessage;
        public Button debugButton;
        public Button[] ActionButton1;
        public Button[] ActionButton2;
        public Button[] ActionButton3;
        public Text txtBattleMessage;
        public Text labelBattleTurn;
        public Image pbSandglass;
        public Text lblTimerCount;
        public GameObject BattleMenuPanel;
        public Button BattleSettingButton;
        public Button UseItemButton;
        public Button RunAwayButton;

        public Image player1Arrow;
        public Text playerActionLabel1;
        public Button buttonTargetPlayer1;
        public Text player1Name;
        public Text player1Life;
        public Image player1LifeMeter;
        public Image player1ManaMeter;
        public Image player1SkillMeter;
        public Text player1Instant;
        public Image player1InstantMeter;
        public Text player1Damage;
        public Text player1Critical;
        public Image imgShadowPact1;
        public Image imgWordOfLife1;
        public Image imgPoison1;
        public Image[] IsSorcery1;

        public Image player2Arrow;
        public Text playerActionLabel2;
        public Button buttonTargetPlayer2;
        public Text player2Name;
        public Text player2Life;
        public Image player2LifeMeter;
        public Image player2ManaMeter;
        public Image player2SkillMeter;
        public Text player2Instant;
        public Image player2InstantMeter;
        public Text player2Damage;
        public Text player2Critical;
        public Image imgShadowPact2;
        public Image imgWordOfLife2;
        public Image imgPoison2;
        public Image[] IsSorcery2;

        public Image player3Arrow;
        public Text playerActionLabel3;
        public Button buttonTargetPlayer3;
        public Text player3Name;
        public Text player3Life;
        public Image player3LifeMeter;
        public Image player3ManaMeter;
        public Image player3SkillMeter;
        public Text player3Instant;
        public Image player3InstantMeter;
        public Text player3Damage;
        public Text player3Critical;
        public Image imgShadowPact3;
        public Image imgWordOfLife3;
        public Image imgPoison3;
        public Image[] IsSorcery3;

        public Image enemy1Arrow;
        public Text enemyActionLabel1;
        public Button buttonTargetEnemy1;
        public Text enemy1Name;
        public Text enemy1Life;
        public Image enemy1LifeMeter;
        //	public Image enemy1ManaMeter;
        //	public Image enemy1SkillMeter;
        //	public Text enemy1Instant;
        //	public Image enemy1InstantMeter;
        public Text enemy1Damage;
        public Text enemy1Critical;
        public Image imgShadowPactE1;
        public Image imgWordOfLifeE1;
        public Image imgPoisonE1;
        //public Image[] IsSorceryE1;

        public Image enemy2Arrow;
        public Text enemyActionLabel2;
        public Button buttonTargetEnemy2;
        public Text enemy2Name;
        public Text enemy2Life;
        public Image enemy2LifeMeter;
        //	public Image enemy2ManaMeter;
        //	public Image enemy2SkillMeter;
        //	public Text enemy2Instant;
        //	public Image enemy2InstantMeter;
        public Text enemy2Damage;
        public Text enemy2Critical;
        public Image imgShadowPactE2;
        public Image imgWordOfLifeE2;
        public Image imgPoisonE2;
        //public Image[] IsSorceryE2;

        public Image enemy3Arrow;
        public Text enemyActionLabel3;
        public Button buttonTargetEnemy3;
        public Text enemy3Name;
        public Text enemy3Life;
        public Image enemy3LifeMeter;
        //	public Image enemy3ManaMeter;
        //	public Image enemy3SkillMeter;
        //	public Text enemy3Instant;
        //	public Image enemy3InstantMeter;
        public Text enemy3Damage;
        public Text enemy3Critical;
        //public Image[] IsSorceryE3;

        public GameObject BuffPanel1;
        public GameObject BuffPanel2;
        public GameObject BuffPanel3;
        public GameObject PanelBuffEnemy1;
        public GameObject PanelBuffEnemy2;
        public GameObject PanelBuffEnemy3;

        // internal
        const int TIME_TURN = 320;
        int BattleTimeCounter = Database.BASE_TIMER_BAR_LENGTH / 2;
        int BattleTurnCount = 0;

        bool gameStart = false;

        TruthEnemyCharacter ec1;
        TruthEnemyCharacter ec2;
        TruthEnemyCharacter ec3;
        List<MainCharacter> playerList = new List<MainCharacter>();
        List<MainCharacter> enemyList = new List<MainCharacter>();

        MainCharacter currentPlayer;

        ClientSocket CS;
        public bool firstAction = false;

        int activatePlayerNumber = 0;
        List<MainCharacter> ActiveList = new List<MainCharacter>();
        private static System.Random rand = new System.Random(DateTime.Now.Millisecond * System.Environment.TickCount);

        void ActivateSomeCharacter(MainCharacter player, MainCharacter target,
            Text charaName, Text life, Text backSkillPoint, Text currentSkillPoint, Text backManaPoint, Text currentManaPoint, Text currentInstantPoint, Text currentSpecialInstant,
            Button[] actionButton,
            Text actionLabel,
            GameObject buffPanel, // Panel
            Button mainObject, Color mainColor, Image targetTarget, Image mainFaceArrow, Image shadowFaceArrow2, Image shadowFaceArrow3, // todo Bitmap -> Image ?mainFaceArrow,shadowFaceArrow2,shadowFaceArrow3
            Text damageLabel, Text criticalLabel,
            TruthImage[] buffList,
            Text keyNum1, Text keyNum2, Text keyNum3, Text keyNum4, Text keyNum5, Text keyNum6, Text keyNum7, Text keyNum8, Text keyNum9,
            Image[] sorceryMark
            )
        {
            if (true) // player != null) // todo null指定はできない
            {
                player.RealTimeBattle = true;

                // 戦闘画面UIへの初期設定
                // MainCharacterクラス内容と戦闘画面UIの割り当て
                player.labelName = charaName;
                player.labelName.text = player.Name;

                player.labelCurrentLifePoint = life;
                UpdateLife(player);

                // todo 元ソースから持ってくるモノがある。

                player.ActionLabel = actionLabel;

                // todo 元ソースから持ってくるモノがある。


                // BUFFリストを登録                
                int num = 0;
                player.pbProtection = buffList[num]; buffList[num].ImageName = Database.PROTECTION; num++;
                player.pbAbsorbWater = buffList[num]; buffList[num].ImageName = Database.ABSORB_WATER; num++;
                player.pbShadowPact = buffList[num]; buffList[num].ImageName = Database.SHADOW_PACT; num++;
                player.pbFlameAura = buffList[num]; buffList[num].ImageName = Database.FLAME_AURA; num++;
                player.pbHeatBoost = buffList[num]; buffList[num].ImageName = Database.HEAT_BOOST; num++;
                player.pbSaintPower = buffList[num]; buffList[num].ImageName = Database.SAINT_POWER; num++;
                player.pbWordOfLife = buffList[num]; buffList[num].ImageName = Database.WORD_OF_LIFE; num++;
                player.pbGlory = buffList[num]; buffList[num].ImageName = Database.GLORY; num++;
                player.pbVoidExtraction = buffList[num]; buffList[num].ImageName = Database.VOID_EXTRACTION; num++;
                player.pbOneImmunity = buffList[num]; buffList[num].ImageName = Database.ONE_IMMUNITY; num++;
                player.pbGaleWind = buffList[num]; buffList[num].ImageName = Database.GALE_WIND; num++;
                player.pbWordOfFortune = buffList[num]; buffList[num].ImageName = Database.WORD_OF_FORTUNE; num++;
                player.pbBloodyVengeance = buffList[num]; buffList[num].ImageName = Database.BLOODY_VENGEANCE; num++;
                player.pbRiseOfImage = buffList[num]; buffList[num].ImageName = Database.RISE_OF_IMAGE; num++;
                player.pbImmortalRave = buffList[num]; buffList[num].ImageName = Database.IMMORTAL_RAVE; num++;
                player.pbHighEmotionality = buffList[num]; buffList[num].ImageName = Database.HIGH_EMOTIONALITY; num++;
                player.pbBlackContract = buffList[num]; buffList[num].ImageName = Database.BLACK_CONTRACT; num++;
                player.pbAetherDrive = buffList[num]; buffList[num].ImageName = Database.AETHER_DRIVE; num++;
                player.pbEternalPresence = buffList[num]; buffList[num].ImageName = Database.ETERNAL_PRESENCE; num++;
                player.pbMirrorImage = buffList[num]; buffList[num].ImageName = Database.MIRROR_IMAGE; num++;
                player.pbDeflection = buffList[num]; buffList[num].ImageName = Database.DEFLECTION; num++;
                player.pbTruthVision = buffList[num]; buffList[num].ImageName = Database.TRUTH_VISION; num++;
                player.pbStanceOfFlow = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_FLOW; num++;
                player.pbPromisedKnowledge = buffList[num]; buffList[num].ImageName = Database.PROMISED_KNOWLEDGE; num++;
                player.pbStanceOfDeath = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DEATH; num++;
                player.pbAntiStun = buffList[num]; buffList[num].ImageName = Database.ANTI_STUN; num++;

                player.pbStanceOfEyes = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_EYES; num++;
                player.pbNegate = buffList[num]; buffList[num].ImageName = Database.NEGATE; num++;
                player.pbCounterAttack = buffList[num]; buffList[num].ImageName = Database.COUNTER_ATTACK; num++;
                player.pbStanceOfStanding = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_STANDING; num++;

                player.pbPainfulInsanity = buffList[num]; buffList[num].ImageName = Database.PAINFUL_INSANITY; num++;
                player.pbDamnation = buffList[num]; buffList[num].ImageName = Database.DAMNATION; num++;
                player.pbAbsoluteZero = buffList[num]; buffList[num].ImageName = Database.ABSOLUTE_ZERO; num++;
                player.pbNothingOfNothingness = buffList[num]; buffList[num].ImageName = Database.NOTHING_OF_NOTHINGNESS; num++;

                player.pbPoison = buffList[num]; buffList[num].ImageName = Database.EFFECT_POISON; num++;
                player.pbStun = buffList[num]; buffList[num].ImageName = Database.EFFECT_STUN; num++;
                player.pbSilence = buffList[num]; buffList[num].ImageName = Database.EFFECT_SILENCE; num++;
                player.pbParalyze = buffList[num]; buffList[num].ImageName = Database.EFFECT_PARALYZE; num++;
                player.pbFrozen = buffList[num]; buffList[num].ImageName = Database.EFFECT_FROZEN; num++;
                player.pbTemptation = buffList[num]; buffList[num].ImageName = Database.EFFECT_TEMPTATION; num++;
                player.pbNoResurrection = buffList[num]; buffList[num].ImageName = Database.EFFECT_NORESURRECTION; num++;
                player.pbSlow = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLOW; num++;
                player.pbBlind = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLIND; num++;
                player.pbSlip = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLIP; num++;
                player.pbNoGainLife = buffList[num]; buffList[num].ImageName = Database.EFFECT_NOGAIN_LIFE; num++;

                //player.pbBuff1 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // 未使用
                //player.pbBuff2 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // 未使用
                //player.pbBuff3 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // 未使用

                player.pbPhysicalAttackUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_UP; num++;
                player.pbPhysicalAttackDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_DOWN; num++;
                player.pbPhysicalDefenseUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_UP; num++;
                player.pbPhysicalDefenseDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_DOWN; num++;
                player.pbMagicAttackUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_UP; num++;
                player.pbMagicAttackDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_DOWN; num++;
                player.pbMagicDefenseUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_UP; num++;
                player.pbMagicDefenseDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_DOWN; num++;
                player.pbSpeedUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_UP; num++;
                player.pbSpeedDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_DOWN; num++;
                player.pbReactionUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_UP; num++;
                player.pbReactionDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_DOWN; num++;
                player.pbPotentialUp = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_UP; num++;
                player.pbPotentialDown = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_DOWN; num++;

                player.pbStrengthUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STRENGTH_UP; num++;
                player.pbAgilityUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_AGILITY_UP; num++;
                player.pbIntelligenceUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_INTELLIGENCE_UP; num++;
                player.pbStaminaUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STAMINA_UP; num++;
                player.pbMindUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_MIND_UP; num++;

                player.pbResistLightUp = buffList[num]; buffList[num].ImageName = Database.RESIST_LIGHT_UP; num++;
                player.pbResistShadowUp = buffList[num]; buffList[num].ImageName = Database.RESIST_SHADOW_UP; num++;
                player.pbResistFireUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FIRE_UP; num++;
                player.pbResistIceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_ICE_UP; num++;
                player.pbResistForceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FORCE_UP; num++;
                player.pbResistWillUp = buffList[num]; buffList[num].ImageName = Database.RESIST_WILL_UP; num++;

                player.pbResistStun = buffList[num]; buffList[num].ImageName = Database.RESIST_STUN; num++;
                player.pbResistSilence = buffList[num]; buffList[num].ImageName = Database.RESIST_SILENCE; num++;
                player.pbResistPoison = buffList[num]; buffList[num].ImageName = Database.RESIST_POISON; num++;
                player.pbResistTemptation = buffList[num]; buffList[num].ImageName = Database.RESIST_TEMPTATION; num++;
                player.pbResistFrozen = buffList[num]; buffList[num].ImageName = Database.RESIST_FROZEN; num++;
                player.pbResistParalyze = buffList[num]; buffList[num].ImageName = Database.RESIST_PARALYZE; num++;
                player.pbResistNoResurrection = buffList[num]; buffList[num].ImageName = Database.RESIST_NORESURRECTION; num++;
                player.pbResistSlow = buffList[num]; buffList[num].ImageName = Database.RESIST_SLOW; num++;
                player.pbResistBlind = buffList[num]; buffList[num].ImageName = Database.RESIST_BLIND; num++;
                player.pbResistSlip = buffList[num]; buffList[num].ImageName = Database.RESIST_SLIP; num++;

                player.pbPsychicTrance = buffList[num]; buffList[num].ImageName = Database.PSYCHIC_TRANCE; num++;
                player.pbBlindJustice = buffList[num]; buffList[num].ImageName = Database.BLIND_JUSTICE; num++;
                player.pbTranscendentWish = buffList[num]; buffList[num].ImageName = Database.TRANSCENDENT_WISH; num++;
                player.pbFlashBlaze = buffList[num]; buffList[num].ImageName = Database.FLASH_BLAZE; num++;
                player.pbSkyShield = buffList[num]; buffList[num].ImageName = Database.SKY_SHIELD; num++;
                player.pbEverDroplet = buffList[num]; buffList[num].ImageName = Database.EVER_DROPLET; num++;
                player.pbHolyBreaker = buffList[num]; buffList[num].ImageName = Database.HOLY_BREAKER; num++;
                player.pbStarLightning = buffList[num]; buffList[num].ImageName = Database.STAR_LIGHTNING; num++;
                player.pbBlackFire = buffList[num]; buffList[num].ImageName = Database.BLACK_FIRE; num++;
                player.pbWordOfMalice = buffList[num]; buffList[num].ImageName = Database.WORD_OF_MALICE; num++;
                player.pbDarkenField = buffList[num]; buffList[num].ImageName = Database.DARKEN_FIELD; num++;
                player.pbFrozenAura = buffList[num]; buffList[num].ImageName = Database.FROZEN_AURA; num++;
                player.pbEnrageBlast = buffList[num]; buffList[num].ImageName = Database.ENRAGE_BLAST; num++;
                player.pbImmolate = buffList[num]; buffList[num].ImageName = Database.IMMOLATE; num++;
                player.pbVanishWave = buffList[num]; buffList[num].ImageName = Database.VANISH_WAVE; num++;
                player.pbSeventhMagic = buffList[num]; buffList[num].ImageName = Database.SEVENTH_MAGIC; num++;
                player.pbStanceOfDouble = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DOUBLE; num++;
                player.pbSwiftStep = buffList[num]; buffList[num].ImageName = Database.SWIFT_STEP; num++;
                player.pbSmoothingMove = buffList[num]; buffList[num].ImageName = Database.SMOOTHING_MOVE; num++;
                player.pbFutureVision = buffList[num]; buffList[num].ImageName = Database.FUTURE_VISION; num++;
                player.pbReflexSpirit = buffList[num]; buffList[num].ImageName = Database.REFLEX_SPIRIT; num++;
                player.pbTrustSilence = buffList[num]; buffList[num].ImageName = Database.TRUST_SILENCE; num++;
                player.pbStanceOfMystic = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_MYSTIC; num++;
                player.pbPreStunning = buffList[num]; buffList[num].ImageName = Database.EFFECT_PRESTUNNING; num++;
                player.pbBlinded = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLINDED; num++;
                player.pbConcussiveHit = buffList[num]; buffList[num].ImageName = Database.CONCUSSIVE_HIT; num++;
                player.pbOnslaughtHit = buffList[num]; buffList[num].ImageName = Database.ONSLAUGHT_HIT; num++;
                player.pbImpulseHit = buffList[num]; buffList[num].ImageName = Database.IMPULSE_HIT; num++;
                player.pbExaltedField = buffList[num]; buffList[num].ImageName = Database.EXALTED_FIELD; num++;
                player.pbRisingAura = buffList[num]; buffList[num].ImageName = Database.RISING_AURA; num++;
                player.pbBlazingField = buffList[num]; buffList[num].ImageName = Database.BLAZING_FIELD; num++;
                player.pbPhantasmalWind = buffList[num]; buffList[num].ImageName = Database.PHANTASMAL_WIND; num++;
                player.pbParadoxImage = buffList[num]; buffList[num].ImageName = Database.PARADOX_IMAGE; num++;
                player.pbStaticBarrier = buffList[num]; buffList[num].ImageName = Database.STATIC_BARRIER; num++;
                player.pbAscensionAura = buffList[num]; buffList[num].ImageName = Database.ASCENSION_AURA; num++;
                player.pbNourishSense = buffList[num]; buffList[num].ImageName = Database.NOURISH_SENSE; num++;
                player.pbVigorSense = buffList[num]; buffList[num].ImageName = Database.VIGOR_SENSE; num++;
                player.pbOneAuthority = buffList[num]; buffList[num].ImageName = Database.ONE_AUTHORITY; num++;

                player.pbSyutyuDanzetsu = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_EIN; num++;
                player.pbJunkanSeiyaku = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_RANA; num++;

                player.pbHymnContract = buffList[num]; buffList[num].ImageName = Database.HYMN_CONTRACT; num++;
                player.pbSigilOfHomura = buffList[num]; buffList[num].ImageName = Database.SIGIL_OF_HOMURA; num++;
                player.pbAusterityMatrix = buffList[num]; buffList[num].ImageName = Database.AUSTERITY_MATRIX; num++;
                player.pbRedDragonWill = buffList[num]; buffList[num].ImageName = Database.RED_DRAGON_WILL; num++;
                player.pbBlueDragonWill = buffList[num]; buffList[num].ImageName = Database.BLUE_DRAGON_WILL; num++;
                player.pbEclipseEnd = buffList[num]; buffList[num].ImageName = Database.ECLIPSE_END; num++;
                player.pbTimeStop = buffList[num]; buffList[num].ImageName = Database.TIME_STOP; num++;
                player.pbSinFortune = buffList[num]; buffList[num].ImageName = Database.SIN_FORTUNE; num++;

                player.pbLightUp = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_UP; num++;
                player.pbLightDown = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_DOWN; num++;
                player.pbShadowUp = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_UP; num++;
                player.pbShadowDown = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_DOWN; num++;
                player.pbFireUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_UP; num++;
                player.pbFireDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_DOWN; num++;
                player.pbIceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_UP; num++;
                player.pbIceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_DOWN; num++;
                player.pbForceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_UP; num++;
                player.pbForceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_DOWN; num++;
                player.pbWillUp = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_UP; num++;
                player.pbWillDown = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_DOWN; num++;

                player.pbAfterReviveHalf = buffList[num]; buffList[num].ImageName = Database.BUFF_DANZAI_KAGO; num++;
                player.pbFireDamage2 = buffList[num]; buffList[num].ImageName = Database.BUFF_FIREDAMAGE2; num++;
                player.pbBlackMagic = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_MAGIC; num++;
                player.pbChaosDesperate = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOS_DESPERATE; num++;

                player.pbFeltus = buffList[num]; buffList[num].ImageName = Database.BUFF_FELTUS; num++;
                player.pbJuzaPhantasmal = buffList[num]; buffList[num].ImageName = Database.BUFF_JUZA_PHANTASMAL; num++;
                player.pbEternalFateRing = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_FATE_RING; num++;
                player.pbLightServant = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_SERVANT; num++;
                player.pbShadowServant = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_SERVANT; num++;
                player.pbAdilBlueBurn = buffList[num]; buffList[num].ImageName = Database.BUFF_ADIL_BLUE_BURN; num++;
                player.pbMazeCube = buffList[num]; buffList[num].ImageName = Database.BUFF_MAZE_CUBE; num++;
                player.pbShadowBible = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_BIBLE; num++;
                player.pbDetachmentOrb = buffList[num]; buffList[num].ImageName = Database.BUFF_DETACHMENT_ORB; num++;
                player.pbDevilSummonerTome = buffList[num]; buffList[num].ImageName = Database.BUFF_DEVIL_SUMMONER_TOME; num++;
                player.pbVoidHymnsonia = buffList[num]; buffList[num].ImageName = Database.BUFF_VOID_HYMNSONIA; num++;

                player.pbIchinaruHomura = buffList[num]; buffList[num].ImageName = Database.BUFF_ICHINARU_HOMURA; num++;
                player.pbAbyssFire = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_FIRE; num++;
                player.pbLightAndShadow = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_AND_SHADOW; num++;
                player.pbEternalDroplet = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_DROPLET; num++;
                player.pbAusterityMatrixOmega = buffList[num]; buffList[num].ImageName = Database.BUFF_AUSTERITY_MATRIX_OMEGA; num++;
                player.pbVoiceOfAbyss = buffList[num]; buffList[num].ImageName = Database.BUFF_VOICE_OF_ABYSS; num++;
                player.pbAbyssWill = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_WILL; num++;
                player.pbTheAbyssWall = buffList[num]; buffList[num].ImageName = Database.BUFF_THE_ABYSS_WALL; num++;

                player.pbSagePotionMini = buffList[num]; buffList[num].ImageName = Database.BUFF_SAGE_POTION_MINI; num++;
                player.pbGenseiTaima = buffList[num]; buffList[num].ImageName = Database.BUFF_GENSEI_TAIMA; num++;
                player.pbShiningAether = buffList[num]; buffList[num].ImageName = Database.BUFF_SHINING_AETHER; num++;
                player.pbBlackElixir = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_ELIXIR; num++;
                player.pbElementalSeal = buffList[num]; buffList[num].ImageName = Database.BUFF_ELEMENTAL_SEAL; num++;
                player.pbColoressAntidote = buffList[num]; buffList[num].ImageName = Database.BUFF_COLORESS_ANTIDOTE; num++;

                player.pbLifeCount = buffList[num]; buffList[num].ImageName = Database.BUFF_LIFE_COUNT; num++;
                player.pbChaoticSchema = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOTIC_SCHEMA; num++;

                // 登録を反映
                player.BuffElement = buffList;

                // 各プレイヤーのターゲット選定
                player.Target = target;
                player.Target2 = player; // 味方選択はデフォルトでは自分自身としておく。
                if ((player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                    (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
                {
                    player.Target2 = ec1;
                }

                // 各プレイヤーの初期行動を選定
                player.PA = MainCharacter.PlayerAction.NormalAttack;
                player.ReserveBattleCommand = Database.ATTACK_EN;
                PlayerActionSet(player);

                // 各プレイヤーの戦闘バーの位置
                if (DuelMode)
                {
                    player.BattleBarPos = 0;
                }
                else
                {
                    player.BattleBarPos = rand.Next(100, 400);
                    if (player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 250;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }

                    if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 150;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }
                    if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 300;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }
                }

                // 各プレイヤーの戦闘行動決定タイミングの設定（敵専用）
                player.DecisionTiming = 250;

                // 各プレイヤーを表示可能にする
                player.ActivateCharacter();

                // 各プレイヤーの戦闘への参加
                ActiveList.Add(player); // this.activatePlayerNumber, player); // change unity
                this.activatePlayerNumber++;

                // 各プレイヤーのスキル開放制限
                if (!player.AvailableSkill)
                {
                    if (player.labelCurrentSkillPoint != null)
                    {
                        player.labelCurrentSkillPoint.gameObject.SetActive(false);
                    }
                }

                // 各プレイヤーの魔法開放制限
                if (!player.AvailableMana)
                {
                    if (player.labelCurrentManaPoint != null)
                    {
                        player.labelCurrentManaPoint.gameObject.SetActive(false);
                    }
                }

                // 各プレイヤーのインスタントコマンドの開放制限
                if (!GroundOne.WE.AvailableInstantCommand)
                {
                    if (player.labelCurrentInstantPoint != null)
                    {
                        player.labelCurrentInstantPoint.gameObject.SetActive(false);
                    }
                }
                if ((player.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                    (player.Name == Database.ENEMY_BRILLIANT_SEA_PRINCE))
                {
                    player.labelCurrentInstantPoint.gameObject.SetActive(true);
                }

                // 味方側、魔法・スキルをセットアップ
                // プレイヤースキル・魔法習得に応じて、アクションボタンを登録
                UpdateBattleCommandSetting(player, actionButton, sorceryMark);

                #region "敵側、名前の色と各ＵＩポジションを再配置"
                //if (player == ec1 || player == ec2 || player == ec3)
                //{
                //    if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Blue)
                //    {
                //        player.labelName.ForeColor = Color.Blue;
                //    }
                //    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Red)
                //    {
                //        player.labelName.ForeColor = Color.Red;

                //        if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                //        {
                //            player.labelName.Location = new Point(496, 175);
                //            player.ActionLabel.Location = new Point(503, 212);
                //            player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 15);
                //            player.labelCurrentInstantPoint.Location = new Point(460, 235);
                //        }
                //        if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                //        {
                //            player.labelName.Location = new Point(496, 260);
                //            player.ActionLabel.Location = new Point(503, 300);
                //            player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 15);
                //            player.labelCurrentInstantPoint.Location = new Point(460, 320);
                //        }

                //    }
                //    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                //    {
                //        player.labelName.ForeColor = Color.DarkOrange;
                //        player.labelCurrentInstantPoint.BackColor = Color.Gold;

                //        // 640x480時代
                //        // ボス戦の場合、ネームラベルやBUFFの表示場所を変更します。

                //        //player.labelName.ForeColor = Color.DarkOrange;
                //        //player.labelCurrentInstantPoint.BackColor = Color.Gold;

                //        //if (player.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                //        //{
                //        //    player.labelName.Text = "【１階の守護者】\r\n\r\n絡みつくフランシス";
                //        //}
                //        //if (player.Name == Database.ENEMY_BOSS_LEVIATHAN)
                //        //{
                //        //    player.labelName.Text = "【２階の守護者】\r\n\r\n大海蛇リヴィアサン";
                //        //}

                //        //player.MainObjectButton.Location = new Point(400, 182);
                //        //player.labelLife.Location = new Point(510, 186);
                //        //player.CriticalLabel.Location = new Point(393, 190);
                //        //player.DamageLabel.Location = new Point(393, 213);
                //        //player.ActionLabel.Location = new Point(503, 223);
                //        //player.labelName.Location = new Point(430, 115);
                //        //player.labelName.Size = new System.Drawing.Size(200, 100);
                //        //player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 14, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));
                //        //player.labelCurrentInstantPoint.Location = new Point(400, 250);
                //        //player.labelCurrentInstantPoint.Size = new System.Drawing.Size(200, 30);
                //        //player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                //        //player.BuffPanel.Location = new Point(381, 300);

                //        //if (player.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                //        //{
                //        //    player.labelName.Location = new Point(430, 70);
                //        //    player.labelLife.Location = new Point(514, 87);
                //        //    player.MainObjectButton.Location = new Point(400, 89);
                //        //    player.CriticalLabel.Location = new Point(393, 99);
                //        //    player.DamageLabel.Location = new Point(393, 109);
                //        //    player.ActionLabel.Location = new Point(503, 116);
                //        //    player.labelCurrentInstantPoint.Location = new Point(400, 139);
                //        //    player.BuffPanel.Location = new Point(390, 172); 
                //        //}
                //        //if (player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                //        //{
                //        //    player.labelName.Location = new Point(430, 207);
                //        //    player.labelLife.Location = new Point(514, 228);
                //        //    player.MainObjectButton.Location = new Point(400, 230);
                //        //    player.CriticalLabel.Location = new Point(393, 240);
                //        //    player.DamageLabel.Location = new Point(393, 250);
                //        //    player.ActionLabel.Location = new Point(503, 257);
                //        //    player.labelCurrentInstantPoint.Location = new Point(400, 280);
                //        //    player.BuffPanel.Location = new Point(390, 310); 
                //        //}

                //        //if (player.Name == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                //        //{
                //        //    player.labelName.Location = new Point(496, 80);
                //        //    player.ActionLabel.Location = new Point(503, 128);
                //        //    player.MainObjectButton.Location = new Point(400, 97);
                //        //    player.CriticalLabel.Location = new Point(393, 102);
                //        //    player.DamageLabel.Location = new Point(393, 125);
                //        //    player.labelLife.Location = new Point(514, 102);
                //        //    player.labelName.Size = new System.Drawing.Size(200, 100);
                //        //    player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 14, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));
                //        //    player.labelCurrentInstantPoint.Location = new Point(460, 145);
                //        //    player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 20);
                //        //    player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                //        //    player.BuffPanel.Location = new Point(377, 66);
                //        //}

                //        // 1024 x 768
                //        player.MainObjectButton.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_MAIN_OBJ_LOC_Y);
                //        player.labelLife.Location = new Point(TruthLayout.BOSS_STATUS_LOC_X, TruthLayout.BOSS_LIFE_LABEL_LOC_Y);
                //        player.labelName.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_NAME_LABEL_LOC_Y);
                //        player.ActionLabel.Location = new Point(TruthLayout.BOSS_STATUS_LOC_X, TruthLayout.BOSS_ACTION_LABEL_LOC_Y);
                //        player.CriticalLabel.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_CRITICAL_LABEL_LOC_Y);
                //        player.DamageLabel.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_DAMAGE_LABEL_LOC_Y);
                //        player.labelCurrentInstantPoint.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_INSTANT_LABEL_LOC_Y);
                //        player.BuffPanel.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_BUFF_LOC_Y);
                //        player.labelCurrentInstantPoint.Size = new System.Drawing.Size(TruthLayout.BOSS_INSTANT_LABEL_WIDTH, TruthLayout.BOSS_INSTANT_LABEL_HEIGHT);
                //        player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                //        player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));

                //        if (player.Name == Database.ENEMY_BOSS_HOWLING_SEIZER)
                //        {
                //            player.labelName.Text = "【三階の守護者】\r\n\r\n恐鳴主ハウリング・シーザー";
                //        }
                //        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                //        {
                //            player.labelCurrentManaPoint.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_MANA_LABEL_LOC_Y);
                //            player.labelCurrentManaPoint.Size = new Size(TruthLayout.BOSS_MANA_LABEL_WIDTH, TruthLayout.BOSS_MANA_LABEL_HEIGHT);
                //            player.labelCurrentManaPoint.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                //        }
                //        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                //        {
                //            player.labelName.Text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【瘴気】";
                //        }
                //        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                //        {
                //            player.labelName.Text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【無音】";
                //        }
                //        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                //        {
                //            player.labelName.Text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【深淵】";
                //        }
                //        else if (player.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                //        {
                //            player.labelName.Text = "【五階の守護者】\r\n\r\n支　配　竜";
                //            //player.labelCurrentSkillPoint.Visible = false;
                //            //player.labelCurrentManaPoint.Visible = false;
                //        }
                //    }
                //    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Purple)
                //    {
                //        player.labelName.ForeColor = Color.Purple;
                //        player.labelName.Visible = false;
                //        pbMatrixDragon.Visible = true;
                //        pbMatrixDragon.Size = new System.Drawing.Size(250, 100);
                //        pbMatrixDragon.SizeMode = PictureBoxSizeMode.StretchImage;
                //        if (player.Name == Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD)
                //        {
                //            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_BRIYARD);
                //        }
                //        else if (player.Name == Database.ENEMY_DRAGON_TINKOU_DEEPSEA)
                //        {
                //            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_DEEPSEA);
                //        }
                //        else if (player.Name == Database.ENEMY_DRAGON_DESOLATOR_AZOLD)
                //        {
                //            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_AZOLD);
                //        }
                //        else if (player.Name == Database.ENEMY_DRAGON_IDEA_CAGE_ZEED)
                //        {
                //            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_ZEED);
                //        }
                //        else if (player.Name == Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA)
                //        {
                //            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_ETULA);
                //        }
                //        pbMatrixDragon.Location = new Point(700, 150);
                //        player.labelName.ForeColor = Color.DarkOrange;
                //        this.cannotRunAway = true;
                //    }
                //    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Legendary)
                //    {
                //        player.BuffPanel.Location = new Point(663, 80);

                //        player.labelCurrentSkillPoint.Location = new Point(700, 270);
                //        player.labelCurrentSkillPoint.Size = new Size(300, 30);
                //        player.labelCurrentSkillPoint.Font = new Font(player.labelCurrentSkillPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                //        player.labelCurrentManaPoint.Location = new Point(700, 300);
                //        player.labelCurrentManaPoint.Size = new Size(300, 30);
                //        player.labelCurrentManaPoint.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                //        player.labelCurrentInstantPoint.Location = new Point(700, 330);
                //        player.labelCurrentInstantPoint.Size = new Size(300, 30);
                //        player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                //        player.labelName.ForeColor = Color.OrangeRed;
                //        player.labelName.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.LAST_BOSS_NAME_LABEL_LOC_Y);
                //        player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));

                //        if (player.labelCurrentSpecialInstant != null)
                //        {
                //            player.labelCurrentSpecialInstant.Location = new Point(700, 460); // 【警告】なぜ３６０ではレイアウトずれてしまうのか？
                //            player.labelCurrentSpecialInstant.Size = new Size(300, 30);
                //            player.labelCurrentSpecialInstant.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                //        }
                //    }
                //}
                #endregion

                // 敵側、初期BUFFをセットアップ
                if (player == ec1 && player.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    player.CurrentResistFireUp = Database.INFINITY;
                    player.CurrentResistFireUpValue = 2000;
                    player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp.bmp", Database.INFINITY);
                }
                if (player == ec2 && player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                {
                    player.CurrentResistIceUp = Database.INFINITY;
                    player.CurrentResistIceUpValue = 2000;
                    player.ActivateBuff(player.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp.bmp", Database.INFINITY);
                }

                // 死んでいる場合、グレー化する
                if (player.Dead)
                {
                    player.DeadPlayer();
                }
            }
        }

        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // 基本行動
        const int MIX_ACTION_NUM = 45; // [警告] 暫定、本来Databaseに記載するべき
        const int MIX_ACTION_NUM_2 = 30; // [警告]暫定、本来Databaseに記載するべき
        const int ARCHETYPE_NUM = 1; // アーキタイプ
        private void UpdateBattleCommandSetting(MainCharacter player, Button[] actionButton, Image[] sorceryMark)
        {
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                for (int ii = 0; ii < player.BattleActionCommandList.Length; ii++)
                {
                    if (player.BattleActionCommandList[ii] != "")
                    {
                        actionButton[ii].image.sprite = Resources.Load<Sprite>(player.BattleActionCommandList[ii]);
                        actionButton[ii].name = player.BattleActionCommandList[ii];
                        if (TruthActionCommand.GetTimingType(player.BattleActionCommandList[ii]) == TruthActionCommand.TimingType.Sorcery)
                        {
                            sorceryMark[ii].sprite = Resources.Load<Sprite>("sorcery_mark");
                        }
                        else
                        {
                            sorceryMark[ii].sprite = Resources.Load<Sprite>("instant_mark");
                        }
                    }
                }
            }
        }
        void PointerEnter(TruthImage currentImage)
        {
            Debug.Log("PointerEnter");
            // todo
            Vector3 current = Input.mousePosition;
            current.x -= 5;
            current.y += 5;
            popupInfo.transform.position = current;
            popupInfo.SetActive(true);
            CurrentInfo.text = currentImage.ImageName;
            CurrentInfo.text += "\r\n" + TruthActionCommand.GetDescription(currentImage.ImageName);
            // todo
            //Panel currentPanel = (Panel)(((PictureBox)sender).Parent);
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if (currentPanel.Equals(ActiveList[ii].BuffPanel))
            //    {
            //        //((Panel)(((PictureBox)sender).Parent)).Size = new Size(26 * ActiveList[ii].BuffNumber, 26);
            //        ((Panel)(((PictureBox)sender).Parent)).BringToFront();
            //        break;
            //    }
            //}

            //popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + ((Panel)((TruthImage)sender).Parent).Location.X + e.X + 5,
            //    this.Location.Y + ((PictureBox)sender).Location.Y + ((Panel)((TruthImage)sender).Parent).Location.Y + e.Y - 18);
            //popupInfo.PopupColor = Color.Black;
        }
        void PointerExit()
        {
            Debug.Log("PointerExit");
            popupInfo.SetActive(false);
            CurrentInfo.text = "";
        }

        public void PointerMove()
        {
            Debug.Log("PointerMove");
        }

        /// <summary>
        /// Unityのイベントトリガーを設定して、マウスイベント制御を設定、Buffパネルへのエントリーを設定
        /// </summary>
        /// <param name="pbBuff"></param>
        /// <param name="buffPanel"></param>
        /// <param name="ii"></param>
        void SetupBuff(TruthImage[] pbBuff, GameObject buffPanel, int ii)
        {
            GameObject baseObj = new GameObject("object");
            EventTrigger trigger = baseObj.AddComponent<EventTrigger>();
            
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((x) => PointerExit());
            trigger.triggers.Add(entry2);

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.Move; // todo MoveではMouseMoveにならない。
            entry3.callback.AddListener((x) => PointerMove());
            trigger.triggers.Add(entry3);

            pbBuff[ii] = baseObj.AddComponent<TruthImage>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((x) => PointerEnter(pbBuff[ii]));
            trigger.triggers.Add(entry);
            
            pbBuff[ii].name = "buff" + ii; // change unity
            pbBuff[ii].BuffMode = TruthImage.buffType.Small;
            pbBuff[ii].rectTransform.anchorMin = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.anchorMax = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.pivot = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.sizeDelta = new Vector2(40, 40);
            pbBuff[ii].rectTransform.anchoredPosition = new Vector2(Database.BUFFPANEL_BUFF_WIDTH, 0);
            pbBuff[ii].gameObject.SetActive(false);
            pbBuff[ii].transform.SetParent(buffPanel.transform, false);
        }
        private static bool created = false;
        void Awake()
        {
            Debug.Log("TruthBattleEnemy Awake");
            if (!created)
            {
                DontDestroyOnLoad(this.gameObject);
                created = true;
            }
            else
            {
                Destroy(this.gameObject);
                GroundOne.CallBattleSettingAwake = true;
            }

            GroundOne.InitializeGroundOne();
        }

        // Use this for initialization
        void Start()
        {
            pbBuffPlayer1 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer2 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer3 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy1 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy2 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy3 = new TruthImage[Database.BUFF_NUM];
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                SetupBuff( pbBuffPlayer1, BuffPanel1, ii);
                SetupBuff( pbBuffPlayer2, BuffPanel2, ii);
                SetupBuff( pbBuffPlayer3, BuffPanel3, ii);
                SetupBuff( pbBuffEnemy1, PanelBuffEnemy1, ii);
                SetupBuff( pbBuffEnemy2, PanelBuffEnemy2, ii);
                SetupBuff( pbBuffEnemy3, PanelBuffEnemy3, ii);                
            }
            GameObject baseObj = new GameObject("object");
            GroundOne.MC.Name = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Strength = 5;
            GroundOne.MC.Agility = 3;
            GroundOne.MC.Intelligence = 2;
            GroundOne.MC.Stamina = 7;
            GroundOne.MC.Mind = 3;
            GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife;
            GroundOne.MC.CurrentMana = GroundOne.MC.MaxMana;
            GroundOne.MC.CurrentSkillPoint = GroundOne.MC.MaxSkillPoint;
            GroundOne.MC.MagicAttackValue = 20;
            GroundOne.MC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.MC.CurrentInstantPoint = 0;
            GroundOne.MC.MainFaceArrow = this.player1Arrow;
            GroundOne.MC.MainObjectButton = this.buttonTargetPlayer1;
            GroundOne.MC.ActionLabel = this.playerActionLabel1;
            GroundOne.MC.labelName = this.player1Name;
            GroundOne.MC.labelCurrentLifePoint = this.player1Life;
            GroundOne.MC.meterCurrentLifePoint = this.player1LifeMeter;
            GroundOne.MC.labelCurrentManaPoint = null;
            GroundOne.MC.meterCurrentManaPoint = this.player1ManaMeter;
            GroundOne.MC.labelCurrentSkillPoint = null;
            GroundOne.MC.meterCurrentSkillPoint = this.player1SkillMeter;
            GroundOne.MC.labelCurrentInstantPoint = this.player1Instant;
            GroundOne.MC.meterCurrentInstantPoint = this.player1InstantMeter;
            GroundOne.MC.DamageLabel = this.player1Damage;
            GroundOne.MC.CriticalLabel = this.player1Critical;
            GroundOne.MC.btnBaseCommand = this.buttonTargetPlayer1;
            GroundOne.MC.ActionButtonList.AddRange(this.ActionButton1);
            this.playerList.Add(GroundOne.MC);

            GroundOne.SC.Name = Database.RANA_AMILIA_FULL;
            GroundOne.SC.Strength = 2;
            GroundOne.SC.Agility = 4;
            GroundOne.SC.Intelligence = 5;
            GroundOne.SC.Stamina = 5;
            GroundOne.SC.Mind = 3;
            GroundOne.SC.CurrentLife = GroundOne.SC.MaxLife;
            GroundOne.SC.CurrentMana = GroundOne.SC.MaxMana;
            GroundOne.SC.CurrentSkillPoint = GroundOne.SC.MaxSkillPoint;
            GroundOne.SC.MagicAttackValue = 50;
            GroundOne.SC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.SC.CurrentInstantPoint = 0;
            GroundOne.SC.MainFaceArrow = this.player2Arrow;
            GroundOne.SC.MainObjectButton = this.buttonTargetPlayer2;
            GroundOne.SC.ActionLabel = this.playerActionLabel2;
            GroundOne.SC.labelName = this.player2Name;
            GroundOne.SC.labelCurrentLifePoint = this.player2Life;
            GroundOne.SC.meterCurrentLifePoint = this.player2LifeMeter;
            GroundOne.SC.labelCurrentManaPoint = null;
            GroundOne.SC.meterCurrentManaPoint = this.player2ManaMeter;
            GroundOne.SC.labelCurrentSkillPoint = null;
            GroundOne.SC.meterCurrentSkillPoint = this.player2SkillMeter;
            GroundOne.SC.labelCurrentInstantPoint = this.player2Instant;
            GroundOne.SC.meterCurrentInstantPoint = this.player2InstantMeter;
            GroundOne.SC.DamageLabel = this.player2Damage;
            GroundOne.SC.CriticalLabel = this.player2Critical;
            GroundOne.SC.btnBaseCommand = this.buttonTargetPlayer2;
            GroundOne.SC.ActionButtonList.AddRange(this.ActionButton2);
            this.playerList.Add(GroundOne.SC);

            GroundOne.TC.Name = Database.OL_LANDIS_FULL;
            GroundOne.TC.Strength = 3;
            GroundOne.TC.Agility = 5;
            GroundOne.TC.Intelligence = 4;
            GroundOne.TC.Stamina = 3;
            GroundOne.TC.Mind = 5;
            GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife;
            GroundOne.TC.CurrentMana = GroundOne.TC.MaxMana;
            GroundOne.TC.CurrentSkillPoint = GroundOne.TC.MaxSkillPoint;
            GroundOne.TC.MagicAttackValue = 30;
            GroundOne.TC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.TC.CurrentInstantPoint = 0;
            GroundOne.TC.MainFaceArrow = this.player3Arrow;
            GroundOne.TC.MainObjectButton = this.buttonTargetPlayer3;
            GroundOne.TC.ActionLabel = this.playerActionLabel3;
            GroundOne.TC.labelName = this.player3Name;
            GroundOne.TC.labelCurrentLifePoint = this.player3Life;
            GroundOne.TC.meterCurrentLifePoint = this.player3LifeMeter;
            GroundOne.TC.labelCurrentManaPoint = null;
            GroundOne.TC.meterCurrentManaPoint = this.player3ManaMeter;
            GroundOne.TC.labelCurrentSkillPoint = null;
            GroundOne.TC.meterCurrentSkillPoint = this.player3SkillMeter;
            GroundOne.TC.labelCurrentInstantPoint = player3Instant;
            GroundOne.TC.meterCurrentInstantPoint = this.player3InstantMeter;
            GroundOne.TC.DamageLabel = player3Damage;
            GroundOne.TC.CriticalLabel = player3Critical;
            GroundOne.TC.btnBaseCommand = this.buttonTargetPlayer3;
            GroundOne.TC.ActionButtonList.AddRange(this.ActionButton3);
            this.playerList.Add(GroundOne.TC);

            this.ec1 = baseObj.AddComponent<TruthEnemyCharacter>();
            this.ec1.Initialize(Database.ENEMY_KOUKAKU_WURM);
            this.ec1.CurrentMana = this.ec1.MaxMana;
            this.ec1.MagicAttackValue = 1;
            this.ec1.CurrentCommand = Database.ATTACK_EN;
            this.ec1.CurrentInstantPoint = 0;
            this.ec1.MainFaceArrow = this.enemy1Arrow;
            this.ec1.MainObjectButton = this.buttonTargetEnemy1;
            this.ec1.ActionLabel = this.enemyActionLabel1;
            this.ec1.labelName = this.enemy1Name;
            this.ec1.labelCurrentLifePoint = this.enemy1Life;
            this.ec1.meterCurrentLifePoint = this.enemy1LifeMeter;
            this.ec1.labelCurrentManaPoint = null;
            this.ec1.meterCurrentManaPoint = null;
            this.ec1.labelCurrentSkillPoint = null;
            this.ec1.meterCurrentSkillPoint = null;
            this.ec1.labelCurrentInstantPoint = null;
            this.ec1.meterCurrentInstantPoint = null;
            this.ec1.DamageLabel = enemy1Damage;
            this.ec1.CriticalLabel = enemy1Critical;
            this.enemyList.Add(this.ec1);

            this.ec2 = baseObj.AddComponent<TruthEnemyCharacter>();
            this.ec2.Initialize(Database.ENEMY_HIYOWA_BEATLE);
            this.ec2.CurrentMana = this.ec2.MaxMana;
            this.ec2.MagicAttackValue = 1;
            this.ec2.CurrentCommand = Database.ATTACK_EN;
            this.ec2.CurrentInstantPoint = 0;
            this.ec2.MainFaceArrow = this.enemy2Arrow;
            this.ec2.MainObjectButton = this.buttonTargetEnemy2;
            this.ec2.ActionLabel = this.enemyActionLabel2;
            this.ec2.labelName = this.enemy2Name;
            this.ec2.labelCurrentLifePoint = this.enemy2Life;
            this.ec2.meterCurrentLifePoint = this.enemy2LifeMeter;
            this.ec2.labelCurrentManaPoint = null;
            this.ec2.meterCurrentManaPoint = null;
            this.ec2.labelCurrentSkillPoint = null;
            this.ec2.meterCurrentSkillPoint = null;
            this.ec2.labelCurrentInstantPoint = null;
            this.ec2.meterCurrentInstantPoint = null;
            this.ec2.DamageLabel = enemy2Damage;
            this.ec2.CriticalLabel = enemy2Critical;
            this.enemyList.Add(this.ec2);

            this.ec3 = baseObj.AddComponent<TruthEnemyCharacter>();
            this.ec3.Initialize(Database.ENEMY_GREEN_CHILD);
            this.ec3.CurrentMana = this.ec3.MaxMana;
            this.ec3.MagicAttackValue = 1;
            this.ec3.CurrentCommand = Database.PROTECTION;
            this.ec3.CurrentInstantPoint = 0;
            this.ec3.MainFaceArrow = this.enemy3Arrow;
            this.ec3.MainObjectButton = this.buttonTargetEnemy3;
            this.ec3.ActionLabel = this.enemyActionLabel3;
            this.ec3.labelName = this.enemy3Name;
            this.ec3.labelCurrentLifePoint = this.enemy3Life;
            this.ec3.meterCurrentLifePoint = this.enemy3LifeMeter;
            this.ec3.labelCurrentManaPoint = null;
            this.ec3.meterCurrentManaPoint = null;
            this.ec3.labelCurrentSkillPoint = null;
            this.ec3.meterCurrentSkillPoint = null;
            this.ec3.labelCurrentInstantPoint = null;
            this.ec3.meterCurrentInstantPoint = null;
            this.ec3.DamageLabel = enemy3Damage;
            this.ec3.CriticalLabel = enemy3Critical;
            this.enemyList.Add(this.ec3);

            // todo 色々とまだコンポーネント登録しなければならない

//            ActivateSomeCharacter(mc, ec1, nameLabel1, lifeLabel1, null, currentSkillPoint1, null, currentManaPoint1, currentInstantPoint1, null, ActionButton11, ActionButton12, ActionButton13, ActionButton14, ActionButton15, ActionButton16, ActionButton17, ActionButton18, ActionButton19, playerActionLabel1, BuffPanel1, buttonTargetPlayer1, mc.PlayerBattleColor, pbPlayerTargetTarget1, SelectPlayerArrow(mc), null, null, labelDamage1, labelCritical1, pbBuffPlayer1, keyNum1_1, keyNum1_2, keyNum1_3, keyNum1_4, keyNum1_5, keyNum1_6, keyNum1_7, keyNum1_8, keyNum1_9, IsSorcery11, IsSorcery12, IsSorcery13, IsSorcery14, IsSorcery15, IsSorcery16, IsSorcery17, IsSorcery18, IsSorcery19);
            ActivateSomeCharacter(GroundOne.MC, ec1, player1Name, player1Life, null, null, null, null, player1Instant, null, ActionButton1, playerActionLabel1, BuffPanel1, buttonTargetPlayer1, new Color(Database.COLOR_BATTLE_TARGET1_EIN_R, Database.COLOR_BATTLE_TARGET1_EIN_G, Database.COLOR_BATTLE_TARGET1_EIN_B), null, null, null, null, null, null, pbBuffPlayer1, null, null, null, null, null, null, null, null, null, IsSorcery1);
            ActivateSomeCharacter(GroundOne.SC, ec1, player2Name, player2Life, null, null, null, null, player2Instant, null, ActionButton2, playerActionLabel2, BuffPanel2, buttonTargetPlayer2, new Color(Database.COLOR_BATTLE_TARGET1_RANA_R, Database.COLOR_BATTLE_TARGET1_RANA_G, Database.COLOR_BATTLE_TARGET1_RANA_B), null, null, null, null, null, null, pbBuffPlayer2, null, null, null, null, null, null, null, null, null, IsSorcery2);
            ActivateSomeCharacter(GroundOne.TC, ec1, player3Name, player3Life, null, null, null, null, player3Instant, null, ActionButton3, playerActionLabel3, BuffPanel3, buttonTargetPlayer3, new Color(Database.COLOR_BATTLE_TARGET1_OL_R, Database.COLOR_BATTLE_TARGET1_OL_G, Database.COLOR_BATTLE_TARGET1_OL_B), null, null, null, null, null, null, pbBuffPlayer3, null, null, null, null, null, null, null, null, null, IsSorcery3);
//            ActivateSomeCharacter(ec1, mc, enemyNameLabel1, lblLifeEnemy1, null, currentEnemySkillPoint1, null, currentEnemyManaPoint1, currentEnemyInstantPoint1, specialInstant, null, null, null, null, null, null, null, null, null, enemyActionLabel1, PanelBuffEnemy1, buttonTargetEnemy1, Color.DarkRed, pbEnemyTargetTarget1, bmpEnemy1, bmpShadowEnemy1_2, bmpShadowEnemy1_3, labelEnemyDamage1, labelEnemyCritical1, pbBuffEnemy1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //ActivateSomeCharacter(ec1, mc, enemy1Name, enemy1Life, enemyActionLabel1, pbBuffEnemy1);
            ActivateSomeCharacter(ec1, GroundOne.MC, enemy1Name, enemy1Life, null, null, null, null, null, null, null, enemyActionLabel1, PanelBuffEnemy1, null, new Color(50, 100, 150), null, null, null, null, null, null, pbBuffEnemy1, null, null, null, null, null, null, null, null, null, null);
            ActivateSomeCharacter(ec2, GroundOne.MC, enemy2Name, enemy2Life, null, null, null, null, null, null, null, enemyActionLabel2, PanelBuffEnemy2, null, new Color(150, 50, 100), null, null, null, null, null, null, pbBuffEnemy2, null, null, null, null, null, null, null, null, null, null);
            ActivateSomeCharacter(ec3, GroundOne.MC, enemy3Name, enemy3Life, null, null, null, null, null, null, null, enemyActionLabel3, PanelBuffEnemy3, null, new Color(100, 150, 50), null, null, null, null, null, null, pbBuffEnemy3, null, null, null, null, null, null, null, null, null, null);

            // debug
            ec3.PA = MainCharacter.PlayerAction.UseSpell;
            ec3.ReserveBattleCommand = Database.PROTECTION;
            PlayerActionSet(ec3);

            for (int ii = 0; ii < this.ActiveList.Count; ii++)
            {
                UpdateLife(this.ActiveList[ii]);
                UpdateMana(this.ActiveList[ii]);
                UpdateSkillPoint(this.ActiveList[ii]);
            }

            this.currentPlayer = GroundOne.MC;
            //tapFirstChara ();
        }

        bool isEscDown = false;
        // Update is called once per frame
        void Update()
        {
            #region "SceneBack Refresh Logic"
            if (GroundOne.CallBattleSetting && GroundOne.CallBattleSettingAwake)
            {
                Debug.Log("CallBattleSetting true, then reflesh");
                GroundOne.CallBattleSetting = false;
                GroundOne.CallBattleSettingAwake = false;

                UpdateBattleCommandSetting(GroundOne.MC, ActionButton1, IsSorcery1);
                //UpdateBattleCommandSetting(mc, mc.ActionButton1, mc.ActionButton2, mc.ActionButton3, mc.ActionButton4, mc.ActionButton5, mc.ActionButton6, mc.ActionButton7, mc.ActionButton8, mc.ActionButton9,
                //                               mc.IsSorceryMark1, mc.IsSorceryMark2, mc.IsSorceryMark3, mc.IsSorceryMark4, mc.IsSorceryMark5, mc.IsSorceryMark6, mc.IsSorceryMark7, mc.IsSorceryMark8, mc.IsSorceryMark9);
                //if (we.AvailableSecondCharacter && this.DuelMode == false)
                //{
                //    UpdateBattleCommandSetting(sc, sc.ActionButton1, sc.ActionButton2, sc.ActionButton3, sc.ActionButton4, sc.ActionButton5, sc.ActionButton6, sc.ActionButton7, sc.ActionButton8, sc.ActionButton9,
                //                                   sc.IsSorceryMark1, sc.IsSorceryMark2, sc.IsSorceryMark3, sc.IsSorceryMark4, sc.IsSorceryMark5, sc.IsSorceryMark6, sc.IsSorceryMark7, sc.IsSorceryMark8, sc.IsSorceryMark9);
                //}
                //if (we.AvailableThirdCharacter && this.DuelMode == false)
                //{
                //    UpdateBattleCommandSetting(tc, tc.ActionButton1, tc.ActionButton2, tc.ActionButton3, tc.ActionButton4, tc.ActionButton5, tc.ActionButton6, tc.ActionButton7, tc.ActionButton8, tc.ActionButton9,
                //                                   tc.IsSorceryMark1, tc.IsSorceryMark2, tc.IsSorceryMark3, tc.IsSorceryMark4, tc.IsSorceryMark5, tc.IsSorceryMark6, tc.IsSorceryMark7, tc.IsSorceryMark8, tc.IsSorceryMark9);
                //}
            }
            #endregion

            #region "キー制御"
            bool detectShift = false;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                detectShift = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // todo
                if (this.ActionButton1[0].gameObject.activeInHierarchy)
                {
                    ActionCommand(detectShift, GroundOne.MC, GroundOne.MC.BattleActionCommandList[0]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (this.isEscDown == false)
                {
                    this.isEscDown = true;
                    if (this.NowSelectingTarget)
                    {
                        CompleteInstantAction();
                    }
                    else
                    {
                        if (this.DuelMode == false)
                        {
                            if (BattleStart.text == "戦闘中・・・")
                            {
                                BattleStart.text = "戦闘停止";
                                tempStopFlag = true;
                                this.BattleMenuPanel.SetActive(true);
                            }
                            else
                            {
                                BattleStart.text = "戦闘中・・・";
                                tempStopFlag = false;
                                this.BattleMenuPanel.SetActive(false);
                            }
                        }
                        else
                        {
                            if (this.NowStackInTheCommand == false)
                            {
                                this.BattleMenuPanel.SetActive(!this.BattleMenuPanel.activeInHierarchy);
                            }
                        }
                    }
                }                
            }
            #endregion

            #region "ゲージ位置"
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                float widthScale = (float)Screen.width / (float)Database.BASE_TIMER_BAR_LENGTH;
                Vector3 current = ActiveList[ii].MainFaceArrow.transform.position;
                ActiveList[ii].MainFaceArrow.transform.position = new Vector3((float)ActiveList[ii].BattleBarPos * widthScale, current.y, current.z);
            }
            #endregion

            if (this.endFlag) { return; } // 終了サインが出た場合、戦闘終了として待機する。
            if (this.gameStart == false) { return; } // 戦闘開始サインが無い状態では、待機する。

            CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag())
            {
                this.endFlag = true;
                return;
            }

            // todo
            #region "タイムストップチェック"
            //bool tempTimeStop = false;
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if ((ActiveList[ii].CurrentTimeStop > 0))
            //    {
            //        this.NowTimeStop = true;
            //        tempTimeStop = true;
            //        break;
            //    }
            //}

            //if (tempTimeStop == false)
            //{
            //    this.NowTimeStop = false;
            //}
            //if ((this.NowTimeStop == true) && (this.BackColor == Color.GhostWhite))
            //{
            //    this.BackColor = Color.Black;
            //    this.labelBattleTurn.ForeColor = Color.White;
            //    this.TimeSpeedLabel.ForeColor = Color.White;
            //    this.lblTimerCount.ForeColor = Color.White;
            //    for (int ii = 0; ii < ActiveList.Count; ii++)
            //    {
            //        ActiveList[ii].labelName.ForeColor = Color.White;
            //        ActiveList[ii].ActionLabel.ForeColor = Color.White;
            //        ActiveList[ii].CriticalLabel.ForeColor = Color.White;
            //        ActiveList[ii].DamageLabel.ForeColor = Color.White;
            //        GoToTimeStopColor(ActiveList[ii]);
            //        ActiveList[ii].BuffPanel.BackColor = Color.Black;
            //    }
            //}
            //if ((this.NowTimeStop == false) && (this.BackColor == Color.Black))
            //{
            //    ExecPhaseElement(MethodType.TimeStopEnd, null);
            //}
            #endregion

            #region "戦闘一旦停止フラグ"
            if (this.tempStopFlag) { return; } // 「戦闘停止」ボタンやESCキーで、一旦停止させる。
            if (this.DuelMode == false) // DUELモードの時、選択肢の選択中は一旦停止しない。
            {
                if (this.NowSelectingTarget) { return; } // インスタント行動対象選択時、一旦停止させる。
            }
            if (this.NowStackInTheCommand) { return; } // スタックインザコマンド発動中は停止させる。
            #endregion

            this.BattleTimeCounter++; // メイン戦闘タイマーカウント更新
            #region "Bystander専用"
            int currentTimerCount = this.BattleTimeCounter;
            if (BattleTurnCount != 0)
            {
                double currentTime = (Database.BASE_TIMER_BAR_LENGTH / 2.0f - (double)currentTimerCount) / (Database.BASE_TIMER_BAR_LENGTH / 2.0f) * 300.0f / 100.0f;
                lblTimerCount.text = currentTime.ToString("0.00");
            }
            const int DivNum = 32;
            for (int ii = 0; ii < 8; ii++)
            {
                if (DivNum * ii <= this.BattleTimeCounter && this.BattleTimeCounter < DivNum * (ii + 1))
                {
                    pbSandglass.sprite = this.imageSandglass[ii];
                    break;
                }
            }
            #endregion

            if (BattleTimeCounter >= Database.BASE_TIMER_BAR_LENGTH / 2)
            {
                if (BattleTurnCount == 0)
                {
                    // ターン開始時（戦闘開始直後）
                    ExecPhaseElement(MethodType.Beginning, null);
                    // ターンを更新（１ターン始まり）
                    UpdateTurnEnd();
                }
                else
                {
                    // ターン更新直前にて、戦闘後の追加効果フェーズ
                    ExecPhaseElement(MethodType.AfterBattleEffect, null);

                    // ターンを更新
                    UpdateTurnEnd();

                    // ターン更新直後のクリーンナップ
                    ExecPhaseElement(MethodType.CleanUpStep, null);

                    // ターン更新後のアップキープ
                    ExecPhaseElement(MethodType.UpKeepStep, null);
                }
            }
            else
            {
                ExecPhaseElement(MethodType.CleanUpForBoss, null);
            }

            UpdateUseItemGauge();

            #region "各プレイヤーの戦闘フェーズ"
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (this.NowTimeStop && ActiveList[ii].CurrentTimeStop <= 0 && ActiveList[ii].Name != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    // 時間は飛ばされる
                }
                else if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii].BattleBarPos > Database.BASE_TIMER_BAR_LENGTH ||
                        ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH ||
                        ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH)
                    {
                        // 戦闘行動を実行前にポジションと意思決定フラグとカウンターアタックを解除
                        int arrowType = 0;
                        if (ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 1; }
                        else if (ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 2; }
                        UpdatePlayerPreCondition(ActiveList[ii], arrowType);

                        // 戦闘行動を実行
                        if (ExecPhaseElement(MethodType.PlayerAttackPhase, ActiveList[ii]) == false) break;

                        if (ActiveList[ii].CurrentSkillName == Database.STANCE_OF_FLOW && ActiveList[ii].PA == MainCharacter.PlayerAction.UseSkill)
                        {
                            ActiveList[ii].BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                        }

                        // 対象が行動不能な場合、ターゲットを切り替える。
                        UpdatePlayerTarget(ActiveList[ii]);
                    }
                    else
                    {
                        // インスタント行動のタイマー更新
                        UpdatePlayerInstantPoint(ActiveList[ii]);

                        // 戦闘待機ポジション更新
                        UpdatePlayerGaugePosition(ActiveList[ii]);

                        // 戦闘実行内容の決定フェーズ（敵専用)
                        UpdatePlayerNextDecision(ActiveList[ii]);

                        // スタックインザコマンドの発動決定フェーズ（敵専用）
                        UpdatePlayerDoStackInTheCommand(ActiveList[ii]);
                    }
                }
            }
            #endregion

            //CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag()) { this.endFlag = true; }// パーティ死亡確認で戦闘を抜ける。
            //if (this.endBattleForMatrixDragonEnd) break; // 戦闘終了サインにより、戦闘を抜ける。

            //pbPlayer1.Invalidate();

            // BattleEndPhase // todo
            if (GroundOne.MC.CurrentLife <= 0 && GroundOne.SC.CurrentLife <= 0 && GroundOne.TC.CurrentLife <= 0)
            {
                if (this.txtBattleMessage != null)
                {
                    this.txtBattleMessage.text = "You lose..." + this.endFlag.ToString();
                    this.txtBattleMessage.enabled = true;
                }
                this.endFlag = true;
            }
            else if (this.ec1.CurrentLife <= 0 && this.ec2.CurrentLife <= 0 && this.ec3.CurrentLife <= 0)
            {
                if (this.txtBattleMessage != null)
                {
                    this.txtBattleMessage.text = "YOU WIN !!";
                    this.txtBattleMessage.enabled = true;
                }
                this.endFlag = true;
            }

            // battle logic
            for (int ii = 0; ii < this.playerList.Count; ii++)
            {
                if (this.playerList[ii].CurrentInstantPoint < this.playerList[ii].MaxInstantPoint)
                {
                    this.playerList[ii].CurrentInstantPoint++;
                    UpdateInstantPoint(this.playerList[ii], this.playerList[ii].labelCurrentInstantPoint, this.playerList[ii].meterCurrentInstantPoint);
                }
            }
        }

        private void InstantAttackPhase(string BattleActionCommand)
        {
            // 敵対象・味方対象・自分対象、単一敵、複数敵、単一味方、複数味方、状況によってＩＦ文を使い分ける。
            if (this.NowSelectingTarget == false)
            {
                // 魔法・スキルは呼び出し元の名称がそのまま使えるが、武器能力は武器名によって異なるため、以下の分岐。
                if (BattleActionCommand == Database.WEAPON_SPECIAL_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.MainWeapon.Name);
                }
                else if (BattleActionCommand == Database.WEAPON_SPECIAL_LEFT_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.SubWeapon.Name);
                }
                else if (BattleActionCommand == Database.ACCESSORY_SPECIAL_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.Accessory.Name);
                }
                else if (BattleActionCommand == Database.ACCESSORY_SPECIAL2_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.Accessory2.Name);
                }
                else
                {
                    InstantAttackSelect(BattleActionCommand);
                }
            }
            else
            {
                MainCharacter memoTarget = null;
                if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.AllyOrEnemy)
                {
                    return; // 敵味方選択中に自動選択は行えないため、何もしない。
                }
                else if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                {
                    memoTarget = this.currentTargetedPlayer;
                }
                else if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                {
                    memoTarget = this.currentTargetedPlayer.Target;
                }
                // 以下特定ターゲットは無いため、実装不要。
                //else if (ActionCommandAttribute.IsOwnTarget(this.tempActionLabel))
                //{
                //}
                //else if (ActionCommandAttribute.IsAll(this.tempActionLabel))
                //{
                //}

                ExecActionMethod(this.currentTargetedPlayer, memoTarget, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
            }
        }

        private void InstantAttackSelect(string BattleActionCommand)
        {
            // 自分自身が対象の場合、パーティ構成に関係なく、直接自分自身へ
            if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Own)
            {
                ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
            }
            else
            {
                // 味方１人の場合
                if ((GroundOne.WE.AvailableSecondCharacter == false) && (GroundOne.WE.AvailableThirdCharacter == false) ||
                    (this.DuelMode))
                {
                    // 敵が１人の場合
                    if ((ec2 == null) && (ec3 == null))
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ec1, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            //this.Invalidate();
                        }
                        else // 何も書いてないが、アイテム使用を前提として設計されている
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                    // 敵が２人以上（複数）の場合
                    else
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            //this.Invalidate();
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // 何も書いてないが、アイテム使用を前提として設計されている
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                }
                // 味方２人以上（複数）の場合
                else
                {
                    // 敵が１人の場合
                    if ((ec2 == null) && (ec3 == null))
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            //this.Invalidate();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ec1, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // 何も書いてないが、アイテム使用を前提として設計されている
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                    // 敵が２人以上（複数）の場合
                    else
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            //this.Invalidate();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            //this.Invalidate();
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // 何も書いてないが、アイテム使用を前提として設計されている
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                }
            }
        }


        private void ExecActionMethod(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CommandName)
        {
            // 1. 元核の場合、インスタント消費はせず、スタック情報も用いずにスタックを載せる。
            if ((CommandName == Database.ARCHETYPE_EIN) ||
                (CommandName == Database.ARCHETYPE_RANA) ||
                (CommandName == Database.ARCHETYPE_OL) ||
                (CommandName == Database.ARCHETYPE_VERZE)
                )
            {
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                this.NowStackInTheCommand = true;
            }
            else if (IsPlayerEnemy(player) && (((TruthEnemyCharacter)player).UseStackCommand))
            {
                if (UseInstantPoint(player) == false) { return; }
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                this.NowStackInTheCommand = true;
            }
            else
            {
                if ((this.DuelMode) ||
                    (this.NowStackInTheCommand))
                {
                    if (UseInstantPoint(player) == false) { return; }
                    player.StackActivePlayer = player;
                    player.StackTarget = target;
                    player.StackPlayerAction = PA;
                    player.StackCommandString = CommandName;
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
                else
                {
                    if (UseInstantPoint(player) == false) { return; }
                    PlayerAttackPhase(player, target, PA, CommandName, false, false, false);
                    CompleteInstantAction();
                }
            }
        }

        private void CompleteInstantAction()
        {
            //this.currentTargetedPlayer.CurrentInstantPoint = 0; // 元々コメントアウトされていた
            this.instantActionCommandString = String.Empty;
            this.tempTargetForInstant = null;
            this.tempTargetForTarget = null;
            this.tempTargetForTarget2 = null;
            this.NowSelectingTarget = false;
            //this.Invalidate();
        }

        private bool UseInstantPoint(MainCharacter player)
        {
            if (player.CurrentInstantPoint <= 0)
            {
                // インスタントポイントが既に０の場合、何もしない
                return false;
            }

            player.CurrentInstantPoint = 0;
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.text = player.CurrentInstantPoint.ToString();
            }
            return true;
        }

        private void UpdatePlayerInstantPoint(MainCharacter player)
        {
            if (player.CurrentFrozen > 0)
            {
                return;
            }
            if (player.CurrentStunning > 0)
            {
                return;
            }
            if (player.CurrentParalyze > 0)
            {
                return;
            }
            if (player.CurrentStarLightning > 0)
            {
                return;
            }

            if (player.labelCurrentInstantPoint != null)
            {
                if (player.CurrentInstantPoint < player.MaxInstantPoint)
                {
                    player.CurrentInstantPoint += (int)PrimaryLogic.BattleResponseValue(player, this.DuelMode);
                }
                player.labelCurrentInstantPoint.text = ((int)player.CurrentInstantPoint).ToString();
            }

            if (player.labelCurrentSpecialInstant != null)
            {
                if (player.CurrentSpecialInstant < player.MaxSpecialInstant)
                {
                    player.CurrentSpecialInstant += PrimaryLogic.BattleResponseValue(player, this.DuelMode);
                }
                player.labelCurrentSpecialInstant.text = ((int)player.CurrentSpecialInstant).ToString() + " / " + player.MaxSpecialInstant;
            }
        }

        private void UpdatePlayerGaugePosition(MainCharacter player)
        {
            if (player.CurrentFrozen > 0)
            {
                return;
            }
            if (player.CurrentStunning > 0)
            {
                return;
            }
            if (player.CurrentParalyze > 0)
            {
                return;
            }
            if (player.CurrentStarLightning > 0)
            {
                return;
            }
            double movement = PrimaryLogic.BattleSpeedValue(player, this.DuelMode);
            if (player.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                //TruthEnemyCharacter player2 = ((TruthEnemyCharacter)player); // todo 対象が無い？
                movement = movement + Math.Log((double)(1 + this.BattleTurnCount)) / 3;
            }
            if (player.CurrentSlow > 0)
            {
                movement = movement * 2.0f / 3.0f;
            }
            if (player.CurrentSpeedBoost > 0)
            {
                player.CurrentSpeedBoost--;
                movement = movement + 2;
            }
            if (player.CurrentSwiftStep > 0)
            {
                movement = movement * 1.3f;
            }
            if (player.CurrentSmoothingMove > 0)
            {
                movement = movement * 2.0f;
            }
            if (player.CurrentJuzaPhantasmal > 0)
            {
                movement = movement * PrimaryLogic.JuzaPhantasmalValue(player);
            }
            player.BattleBarPos += movement;
            if (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH)
            {
                player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH + 1;
            }
            // 最終戦カオティックスキーマ
            if (player.CurrentChaoticSchema > 0)
            {
                player.BattleBarPos2 += movement;
                if (player.BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH)
                {
                    player.BattleBarPos2 = Database.BASE_TIMER_BAR_LENGTH + 1;
                }

                if (player.CurrentLifeCountValue <= 1)
                {
                    player.BattleBarPos3 += movement;
                    if (player.BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos3 = Database.BASE_TIMER_BAR_LENGTH + 1;
                    }
                }
            }

            // todo
            // StanceOfFlow特有のポジション更新
            //if ((player.CurrentStanceOfFlow > 0) && (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH))
            //{
            //    if (this.StayOn_StanceOfFlow == false && this.BreakOn_StanceOfFlow == false)
            //    {
            //        this.StayOn_StanceOfFlow = true;
            //        player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
            //    }
            //    else
            //    {
            //        if (this.BreakOn_StanceOfFlow == false)
            //        {
            //            player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
            //        }
            //        else
            //        {
            //            this.StayOn_StanceOfFlow = false;
            //            this.BreakOn_StanceOfFlow = false;
            //        }
            //    }
            //}


        }

        private void UpdatePlayerNextDecision(MainCharacter player)
        {
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC) return; // コンピューター専用ルーチンのため、プレイヤー側は何もしない。

            if (player.Name == Database.DUEL_OL_LANDIS) // オル・ランディスは常に戦術を変更可能とする。ヴェルゼなど主要人物は全て該当。
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else if (player.Name == Database.VERZE_ARTIE_FULL || player.Name == Database.VERZE_ARTIE
                  || player.Name == Database.ENEMY_LAST_RANA_AMILIA
                  || player.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ
                  || player.Name == Database.ENEMY_LAST_OL_LANDIS
                  || player.Name == Database.ENEMY_LAST_VERZE_ARTIE
                  || player.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else if ((player.Name == Database.DUEL_SHUVALTZ_FLORE) ||
                     (player.Name == Database.DUEL_SIN_OSCURETE))
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else
            {
                if ((!player.ActionDecision && player.BattleBarPos > player.DecisionTiming) ||
                    ((TruthEnemyCharacter)player).Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    player.ActionDecision = true;

                    if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
                    {
                        if (GroundOne.MC != null && !GroundOne.MC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.SC != null && !GroundOne.SC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.SC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.TC != null && !GroundOne.TC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.TC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
                    {
                        if (GroundOne.TC != null && !GroundOne.TC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.TC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.SC != null && !GroundOne.SC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.SC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.MC != null && !GroundOne.MC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                    }
                    //player.ActionLabel.Update();
                }
            }
        }

        private void UpdatePlayerDoStackInTheCommand(MainCharacter mainCharacter)
        {
            // todo
            //throw new System.NotImplementedException();
        }

        private void UpdatePlayerPreCondition(MainCharacter player, int arrowType)
        {
            // todo
            // StanceOfFlow特有記述
            //if (this.StayOn_StanceOfFlow)
            //{
            //    this.BreakOn_StanceOfFlow = true;
            //}

            if (arrowType == 0) { player.BattleBarPos = 0; }
            else if (arrowType == 1) { player.BattleBarPos2 = 0; }
            else if (arrowType == 2) { player.BattleBarPos3 = 0; }
            Vector3 current = player.MainFaceArrow.transform.position;
            player.MainFaceArrow.transform.position = new Vector3((float)player.BattleBarPos, current.y, current.z);

            // todo
            //player.ActionDecision = false;

            // player.CurrentCounterAttack = false; // 次のコマンドを実行したらカウンターが消滅してしまうのはゲーム性質上、おもしろくない。
        }


        /// <summary>
        /// "[後編必須]ただし、パーティ編成が可能にすることを想定すると、このままではいけないはず。"
        /// </summary>
        /// <param name="mainCharacter"></param>
        private void UpdatePlayerTarget(MainCharacter mainCharacter)
        {
            for (int ii = 0; ii < this.enemyList.Count; ii++)
            {
                // enemy A.I
                if (GroundOne.MC.CurrentLife > 0)
                {
                    this.enemyList[ii].Target = GroundOne.MC;
                }
                else if (GroundOne.SC.CurrentLife > 0)
                {
                    this.enemyList[ii].Target = GroundOne.SC;
                }
                else if (GroundOne.TC.CurrentLife > 0)
                {
                    this.enemyList[ii].Target = GroundOne.TC;
                }
            }

            // todo
            //if (mainCharacter == ec1 || mainCharacter == ec2 || mainCharacter == ec3)
            //{
            //    if (this.DuelMode)
            //    {
            //        // Duelモードの場合、なにもしない。
            //    }
            //    else if (we.AvailableSecondCharacter == false && we.AvailableThirdCharacter == false)
            //    {
            //        // 味方一人の場合、なにもしない。
            //    }
            //    else if (we.AvailableSecondCharacter == true && we.AvailableThirdCharacter == false)
            //    {
            //        // 味方二人の場合、死んでないほうへ切り替える。
            //        if (mc != null && mc.Dead) mainCharacter.Target = sc;
            //        else if (sc != null & sc.Dead) mainCharacter.Target = mc;
            //    }
            //    else
            //    {
            //        List<MainCharacter> group = new List<MainCharacter>();
            //        if (mc != null && !mc.Dead) { group.Add(mc); }
            //        if (sc != null && !sc.Dead) { group.Add(sc); }
            //        if (tc != null && !tc.Dead) { group.Add(tc); }
            //        if (((TruthEnemyCharacter)mainCharacter).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
            //        {
            //            mainCharacter.Target = group[0];
            //        }
            //        else
            //        {
            //            mainCharacter.Target = group[group.Count - 1];
            //        }
            //    }

            //    // 敵側の場合、プレイヤー側へ行動完了後の行動指針を待機にしたことを伝えるため。
            //    if (mainCharacter.FullName == Database.DUEL_CALMANS_OHN)
            //    {
            //        mainCharacter.PA = PlayerAction.Defense;
            //        mainCharacter.ActionLabel.Text = Database.DEFENSE_JP;
            //    }
            //    else
            //    {
            //        mainCharacter.ActionLabel.Text = Database.STAY_JP;
            //    }
            //}
            //else
            //{
            //    if (ec2 == null && ec3 == null)
            //    {
            //        // 敵一人の場合、なにもしない。
            //    }
            //    else if (ec2 != null && ec3 == null)
            //    {
            //        // 敵二人の場合、死んでないほうへ切り替える。
            //        if (ec1 != null && ec1.Dead) mainCharacter.Target = ec2;
            //        else if (ec2 != null && ec2.Dead) mainCharacter.Target = ec1;
            //    }
            //    else
            //    {
            //        List<MainCharacter> group = new List<MainCharacter>();
            //        if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
            //        if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
            //        if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            //        mainCharacter.Target = group[0];
            //    }
            //}
        }

        public enum MethodType
        {
            Beginning,
            AfterBattleEffect,
            // UpdateTurnEnd,
            CleanUpStep,
            UpKeepStep,
            //UpdateUseItemGauge,
            PlayerAttackPhase,
            // UpdatePlayerTarget,
            // UpdatePlayerInstantPoint,
            // UpdatePlayerGaugePosition,
            // UpdatePlayerNextDecision,
            // UpdatePlayerDoStackInTheCommand,
            // UpdatePlayerDeadFlag,
            CleanUpForBoss,
            TimeStopEnd,
        }
        private bool ExecPhaseElement(MethodType method, MainCharacter player)
        {
            switch (method)
            {
                case MethodType.Beginning:
                    Beginning();
                    break;
                case MethodType.AfterBattleEffect:
                    AfterBattleEffect();
                    break;
                case MethodType.CleanUpStep:
                    CleanUpStep();
                    break;
                case MethodType.UpKeepStep:
                    UpkeepStep();
                    break;
                case MethodType.PlayerAttackPhase:
                    PlayerAttackPhase(player, false, false, true);
                    break;
                case MethodType.CleanUpForBoss:
                    CleanUpForBoss();
                    break;
                case MethodType.TimeStopEnd:
                    TimeStopEnd();
                    break;
            }
            if (UpdatePlayerDeadFlag()) return false;

            CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag()) return false;
            return true;
        }

        private void TimeStopEnd()
        {
            // todo
            // throw new System.NotImplementedException();
        }

        private void CleanUpForBoss()
        {
            // todo
            // throw new System.NotImplementedException();
        }



        private void UpkeepStep()
        {
            // todo
            // throw new System.NotImplementedException();
        }

        private void CleanUpStep()
        {
            // todo
            // throw new System.NotImplementedException();
        }

        private void AfterBattleEffect()
        {
            for (int ii = 0; ii < this.ActiveList.Count; ii++)
            {
                if (this.ActiveList[ii].CurrentWordOfLife > 0)
                {
                    this.ActiveList[ii].CurrentWordOfLife--;
                    double value = 32;
                    this.ActiveList[ii].CurrentLife += (int)value;
                    if (this.ActiveList[ii].CurrentLife > this.ActiveList[ii].MaxLife) { this.ActiveList[ii].CurrentLife = this.ActiveList[ii].MaxLife; }
                    UpdateLife(this.ActiveList[ii]);
                    UpdateMessage(this.ActiveList[ii].labelName.text + " 回復 " + ((int)value).ToString() + "\n");
                }
                if (this.ActiveList[ii].CurrentPoison > 0)
                {
                    this.ActiveList[ii].CurrentPoison--;
                    double value = 20;
                    this.ActiveList[ii].CurrentLife -= (int)value;
                    if (this.ActiveList[ii].CurrentLife < 0) { this.ActiveList[ii].CurrentLife = 0; }
                    UpdateLife(this.ActiveList[ii]);
                    UpdateMessage(this.ActiveList[ii].labelName.text + " 毒 " + ((int)value).ToString() + "\n");
                }
            }

            // todo
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if (!ActiveList[ii].Dead)
            //    {
            //        if (ActiveList[ii].CurrentEternalDroplet > 0)
            //        {
            //            double effectValue = PrimaryLogic.EternalDropletValue_A(ActiveList[ii]);
            //            if (ActiveList[ii].CurrentNourishSense > 0)
            //            {
            //                effectValue = effectValue * 1.3f;
            //            }
            //            effectValue = GainIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText("永遠を示す理が、" + ActiveList[ii].Name + "へ生命力を注ぎ込んでいる。" + ((int)effectValue).ToString() + "ライフ回復\r\n");
            //            ActiveList[ii].CurrentLife += (int)(effectValue);
            //            UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);

            //            double effectValue2 = PrimaryLogic.EternalDropletValue_B(ActiveList[ii]);
            //            effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
            //            UpdateBattleText(((int)effectValue2).ToString() + "マナ回復\r\n");
            //            ActiveList[ii].CurrentMana += (int)effectValue;
            //            UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
            //        }

            //        if (ActiveList[ii].CurrentWordOfLife > 0)
            //        {
            //            double effectValue = PrimaryLogic.WordOfLifeValue(ActiveList[ii], this.DuelMode);
            //            if (ActiveList[ii].CurrentNourishSense > 0)
            //            {
            //                effectValue = effectValue * 1.3f;
            //            }
            //            effectValue = GainIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText("大自然から" + ActiveList[ii].Name + "へ力強い脈動が行き渡る。" + ((int)effectValue).ToString() + "ライフ回復\r\n");
            //            ActiveList[ii].CurrentLife += (int)(effectValue);
            //            UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);
            //        }

            //        if (ActiveList[ii].CurrentEverDroplet > 0 && ActiveList[ii].Dead == false)
            //        {
            //            double effectValue = PrimaryLogic.EverDropletValue(ActiveList[ii]);
            //            effectValue = GainIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText("生命の根源から" + ActiveList[ii].Name + "へ無限のイメージが行き渡る。" + ((int)effectValue).ToString() + "マナ回復\r\n");
            //            ActiveList[ii].CurrentMana += (int)effectValue;
            //            UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
            //        }

            //        if (ActiveList[ii].CurrentBlackContract > 0 && !ActiveList[ii].Dead)
            //        {
            //            double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
            //            effectValue = DamageIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText(ActiveList[ii].Name + "は悪魔への代償を支払う。" + ((int)effectValue).ToString() + "ライフが削り取られる。\r\n");
            //            LifeDamage(effectValue, ActiveList[ii]);
            //        }

            //        if (ActiveList[ii].CurrentHymnContract > 0 && !ActiveList[ii].Dead)
            //        {
            //            double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
            //            effectValue = DamageIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText(ActiveList[ii].Name + "は天使との締結により、魂の代金を支払う。" + ((int)effectValue).ToString() + "ライフが削り取られる。\r\n");
            //            LifeDamage(effectValue, ActiveList[ii]);
            //        }

            //        if (ActiveList[ii].CurrentDamnation > 0 && !ActiveList[ii].Dead)
            //        {
            //            double effectValue = PrimaryLogic.DamnationValue(ActiveList[ii]);
            //            effectValue = DamageIsZero(effectValue, ActiveList[ii]);
            //            UpdateBattleText("黒が" + ActiveList[ii].Name + "の存在している空間を歪ませてくる。" + ((int)effectValue).ToString() + "のダメージ\r\n");
            //            LifeDamage(effectValue, ActiveList[ii]);
            //        }

            //        if ((ActiveList[ii].Accessory != null) && (ActiveList[ii].Accessory.Name == Database.COMMON_MUKEI_SAKAZUKI))
            //        {
            //            if (ActiveList[ii].PoolLifeConsumption > 0)
            //            {
            //                double effectValue = (double)(ActiveList[ii].PoolLifeConsumption) / 2.0F;
            //                double effectValue2 = (double)(ActiveList[ii].PoolManaConsumption) / 2.0F;
            //                double effectValue3 = (double)(ActiveList[ii].PoolSkillConsumption) / 2.0F;
            //                effectValue = GainIsZero(effectValue, ActiveList[ii]);
            //                effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
            //                effectValue3 = GainIsZero(effectValue3, ActiveList[ii]);
            //                UpdateBattleText(Database.COMMON_MUKEI_SAKAZUKI + "から" + ActiveList[ii].Name + "へ生命の水が湧き出てくる。\r\n");
            //                UpdateBattleText(ActiveList[ii].Name + "のライフが" + ((int)effectValue).ToString() + "回復、マナが" + ((int)effectValue2).ToString() + "回復、スキルポイントが" + ((int)effectValue3).ToString() + "回復\r\n");
            //                ActiveList[ii].CurrentLife += (int)effectValue;
            //                ActiveList[ii].CurrentMana += (int)effectValue2;
            //                ActiveList[ii].CurrentSkillPoint += (int)effectValue3;
            //                UpdateLife(ActiveList[ii], (double)effectValue, true, true, 0, false);
            //                UpdateMana(ActiveList[ii], (double)effectValue2, true, true, 0);
            //                UpdateSkillPoint(ActiveList[ii], (double)effectValue3, true, true, 0);
            //            }
            //        }
            //    }
            //}
        }

        private void Beginning()
        {
            // todo
            // throw new System.NotImplementedException();
        }

        private void UpdateUseItemGauge()
        {
            // todo
            //throw new System.NotImplementedException();
        }
        
        private void UpdateTurnEnd(bool cancelCounterClear = false)
        {
            this.BattleTurnCount++;
            this.labelBattleTurn.text = "ターン " + BattleTurnCount.ToString();
            if (cancelCounterClear == false) { this.BattleTimeCounter = 0; }
        }

        private void CheckStackInTheCommand()
        {
            // todo
            //throw new System.NotImplementedException();
        }


        private void RefreshActionIcon(MainCharacter player)
        {
            //string SelectOn = "_On";

            // todo
            //if (player.BattleActionCommand1 != "") player.ActionButton1.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand1 + fileExt);
            //if (player.BattleActionCommand2 != "") player.ActionButton2.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand2 + fileExt);
            //if (player.BattleActionCommand3 != "") player.ActionButton3.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand3 + fileExt);
            //if (player.BattleActionCommand4 != "") player.ActionButton4.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand4 + fileExt);
            //if (player.BattleActionCommand5 != "") player.ActionButton5.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand5 + fileExt);
            //if (player.BattleActionCommand6 != "") player.ActionButton6.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand6 + fileExt);
            //if (player.BattleActionCommand7 != "") player.ActionButton7.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand7 + fileExt);
            //if (player.BattleActionCommand8 != "") player.ActionButton8.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand8 + fileExt);
            //if (player.BattleActionCommand9 != "") player.ActionButton9.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand9 + fileExt);
        }
        private bool PlayerPartyDeathCheck()
        {
            // そのロジック、イマイチだが良しとする。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == ec1 ||
                        ActiveList[ii] == ec2 ||
                        ActiveList[ii] == ec3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool EnemyPartyDeathCheck()
        {
            // そのロジック、イマイチだが良しとする。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == GroundOne.MC ||
                        ActiveList[ii] == GroundOne.SC ||
                        ActiveList[ii] == GroundOne.TC)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsPlayerEnemy(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == ec1) || (player == ec2) || (player == ec3))
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerAlly(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == GroundOne.MC) || (player == GroundOne.SC) || (player == GroundOne.TC))
            {
                return true;
            }
            return false;
        }
        private bool CheckBattlePlaying()
        {
            if (tempStopFlag)
            {
                return true;
            }
            return false;
        }

        private bool CheckInstantTarget(string BattleActionCommand)
        {
            if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
            {
                return true;
            }
            return false;
        }

        private bool CheckNotInstant(string BattleActionCommand)
        {
            // [警告] 武器、アクセサリは常にインスタントで良いのか？
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL2_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_LEFT_EN) return false;

            if (TruthActionCommand.GetTimingType(BattleActionCommand) != TruthActionCommand.TimingType.Instant)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 全滅しているかどうかを判定する。（敵・味方含む）
        /// </summary>
        /// <returns>true:全滅している
        ///          false:全滅していない</returns>
        private bool UpdatePlayerDeadFlag()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList[ii].CurrentLife <= 0)
                {
                    // todo
                    //if ((ActiveList[ii].Name == Database.ENEMY_BOSS_LEGIN_ARZE_3) &&
                    //    (!((TruthEnemyCharacter)ActiveList[ii]).DetectDeath))
                    //{
                    //    ((TruthEnemyCharacter)ActiveList[ii]).DetectDeath = true;
                    //    UpdateBattleText(ActiveList[ii].Name + "は死の至る刹那、深淵の防壁を作りだした！！\r\n");
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "深淵の防壁");
                    //    ActiveList[ii].CurrentLife = 1;
                    //    UpdateLife(ActiveList[ii]);
                    //    ActiveList[ii].CurrentTheAbyssWall = Database.INFINITY;
                    //    ActiveList[ii].ActivateBuff(ActiveList[ii].pbTheAbyssWall, Database.BaseResourceFolder + Database.THE_ABYSS_WALL + ".bmp", Database.INFINITY);
                    //}
                    //else if (ActiveList[ii].CurrentGenseiTaima > 0)
                    //{
                    //    ActiveList[ii].RemoveGenseiTaima();
                    //    UpdateBattleText(ActiveList[ii].Name + "に対して退魔の効果が発動し、致死の狭間で生き残った！！\r\n");
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "復活");
                    //    ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife / 2;
                    //    UpdateLife(ActiveList[ii]);
                    //}
                    //else if (ActiveList[ii].CurrentStanceOfDeath > 0)
                    //{
                    //    ActiveList[ii].RemoveStanceOfDeath();
                    //    UpdateBattleText(ActiveList[ii].Name + "は致死の狭間で生き残った！！\r\n");
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "復活");
                    //    ActiveList[ii].CurrentLife = 1;
                    //    UpdateLife(ActiveList[ii]);
                    //}
                    //else if (ActiveList[ii].CurrentShadowBible > 0)
                    //{
                    //    ActiveList[ii].RemoveShadowBible();
                    //    UpdateBattleText(ActiveList[ii].Name + "は致死の狭間でみなぎる生命力を感じ取った！！\r\n");
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "復活");
                    //    ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife;
                    //    UpdateLife(ActiveList[ii]);
                    //    NowNoResurrection(ActiveList[ii], ActiveList[ii], 999);
                    //}
                    //else if (ActiveList[ii].CurrentAfterReviveHalf > 0)
                    //{
                    //    ActiveList[ii].RemoveAfterReviveHalf();
                    //    UpdateBattleText(ActiveList[ii].Name + "は致死の狭間で生き残った！！\r\n");
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "復活");
                    //    ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife / 2.0f);
                    //    UpdateLife(ActiveList[ii]);
                    //}
                    //else if (ActiveList[ii].CurrentLifeCount > 0)
                    //{
                    //    ActiveList[ii].CurrentLifeCountValue--;
                    //    UpdateBattleText(ActiveList[ii].Name + "の生命力が１つ削られた！！\r\n");
                    //    if (ActiveList[ii].CurrentLifeCountValue <= 0)
                    //    {
                    //        UpdateBattleText(ActiveList[ii].GetCharacterSentence(217));
                    //        ActiveList[ii].RemoveLifeCount();
                    //        ActiveList[ii].DeadPlayer();
                    //    }
                    //    else
                    //    {
                    //        UpdateBattleText(ActiveList[ii].GetCharacterSentence(216));
                    //        System.Threading.Thread.Sleep(1000);
                    //        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "生命復活");
                    //        ActiveList[ii].RemoveDebuffEffect();
                    //        ActiveList[ii].RemoveDebuffParam();
                    //        ActiveList[ii].RemoveDebuffSkill();
                    //        ActiveList[ii].RemoveDebuffSpell();
                    //        ActiveList[ii].ChangeLifeCountStatus(ActiveList[ii].CurrentLifeCountValue);
                    //        ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife);
                    //        UpdateLife(ActiveList[ii]);
                    //        ActiveList[ii].labelLife.Update();
                    //        System.Threading.Thread.Sleep(1000);
                    //    }
                    //}
                    //else if (CheckResurrectWithItem(ActiveList[ii], Database.RARE_TAMATEBAKO_AKIDAMA))
                    //{
                    //    UpdateBattleText(Database.RARE_TAMATEBAKO_AKIDAMA + "が淡く光り始めた！\r\n", 500);
                    //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "復活");

                    //    UpdateBattleText(ActiveList[ii].Name + "は致死の狭間で生き残った！！\r\n");
                    //    ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife * 0.1f);
                    //    UpdateLife(ActiveList[ii]);
                    //}
                    //else
                    //{
                        ActiveList[ii].DeadPlayer();
                    //}
                }

                // todo
                // TranscendentWishの効果が解除された時即死する条件を追加。
                //if (ActiveList[ii].DeadSignForTranscendentWish)
                //{
                //    UpdateBattleText(ActiveList[ii].Name + "のTranscendentWishの効果が切れた！生命の源が失われていく・・・\r\n");
                //    UpdateLife(ActiveList[ii], ActiveList[ii].CurrentLife, false, true, 0, false);
                //    ActiveList[ii].CurrentLife = 0;
                //    UpdateLife(ActiveList[ii], 0, false, false, 0, false);
                //    ActiveList[ii].DeadPlayer();
                //    System.Threading.Thread.Sleep(1000);
                //}

                // todo
                //CheckChaosDesperate(ActiveList[ii]);
            }

            if (PlayerPartyDeathCheck() || EnemyPartyDeathCheck())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ActionCommand( bool detectShift, MainCharacter player, string BattleActionCommand)
        {
            // いずれかのプレイヤーが行動実行中である間、割り込みはできない。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList[ii].NowExecActionFlag)
                {
                    if (BattleActionCommand == Database.DEFENSE_EN)
                    {
                        // 防御だけは即時適用を可能とする。
                    }
                    else
                    {
                        return;
                    }
                }

                if (IsPlayerEnemy(ActiveList[ii]))
                {
                    if (ActiveList[ii].CurrentTimeStop > 0)
                    {
                        UpdateBattleText("時間停止中のため、行動できない！！\r\n");
                        return;
                    }
                }
            }

            if (player != null)
            {
                UpdatePlayerDeadFlag();
                if (player.Dead)
                {
                    return;
                }
            }
            if (player.CurrentFrozen > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }
            if (player.CurrentStunning > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }
            if (player.CurrentParalyze > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }

            if (//((e != null) && (e.Button == System.Windows.Forms.MouseButtons.Right)) || // todo もし画面UIで操作できる何かがあれば
                 (detectShift) //(e2 != null) && (e2.Shift)) // todo 右クリックやShiftは今はないため、常にfalse
                )
            {
                if (CheckBattlePlaying()) return;
                if ((player.CurrentInstantPoint < player.MaxInstantPoint) &&
                    (BattleActionCommand != Database.ARCHETYPE_EIN) &&
                    (BattleActionCommand != Database.ARCHETYPE_RANA) &&
                    (BattleActionCommand != Database.ARCHETYPE_OL) &&
                    (BattleActionCommand != Database.ARCHETYPE_VERZE))
                {
                    return;
                }
                if (GroundOne.WE.AvailableInstantCommand == false) return;
                if ((BattleActionCommand == Database.ARCHETYPE_EIN) || // 元核は一日一度だけである
                    (BattleActionCommand == Database.ARCHETYPE_RANA) ||
                    (BattleActionCommand == Database.ARCHETYPE_OL) ||
                    (BattleActionCommand == Database.ARCHETYPE_VERZE))
                {
                    // シャイニング・エーテル効果がある時は、一回だけ追加発動可能である。
                    if (player.CurrentShiningAether > 0)
                    {
                        // 追加発動可能のため、スルー
                    }
                    // 元核は一日一度だけである
                    else if (player.AlreadyPlayArchetype)
                    {
                        UpdateBattleText(player.GetCharacterSentence(204));
                        return;
                    }
                }
                if (CheckNotInstant(BattleActionCommand)) // インスタントではない場合、発動できない。
                {
                    UpdateBattleText(player.GetCharacterSentence(128));
                    return;
                }
                if (CheckInstantTarget(BattleActionCommand)) // インスタント対象の場合
                {
                    if (this.NowStackInTheCommand)
                    {
                        // スタック・イン・ザ・コマンド中はインスタント対象として発動するため、ここではスルー
                        // ただし、事前にコスト消費チェックが入る。
                        if (player.CurrentSkillPoint < TruthActionCommand.Cost(BattleActionCommand, player) &&
                            TruthActionCommand.GetAttribute(BattleActionCommand) == TruthActionCommand.Attribute.Skill)
                        {
                            // todo EffectCheckDarknessCoin
                            //if (EffectCheckDarknessCoin(player))
                            //{
                                // 代償を支払ったため、スルー
                            //}
                            //else if (player.CurrentBlackContract > 0) // todo (back to else if)
                            if (player.CurrentBlackContract > 0)
                            {
                                // ブラック・コントラクト時はスルー
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(0));
                                return;
                            }
                        }
                        else
                        {
                            player.CurrentSkillPoint -= TruthActionCommand.Cost(BattleActionCommand, player);
                            UpdateSkillPoint(player);
                        }
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(167)); // インスタント対象の場合、発動できない。
                        return;
                    }
                }

                this.currentTargetedPlayer = player;
                InstantAttackPhase(BattleActionCommand);
            }
            else if (//((e != null) && (e.Button == System.Windows.Forms.MouseButtons.Left)) || // todo  もし画面UIで操作できる何かがあれば
                      (!detectShift)/*(e2 != null) && (!e2.Shift))*/
                    )
            {
                if (CheckInstantTarget(BattleActionCommand)) // インスタント対象の場合、発動できない。
                {
                    UpdateBattleText(player.GetCharacterSentence(167));
                    return;
                }
                if ((BattleActionCommand == Database.ARCHETYPE_EIN) || // 元核はインスタント発動専用である。
                    (BattleActionCommand == Database.ARCHETYPE_RANA) ||
                    (BattleActionCommand == Database.ARCHETYPE_OL) ||
                    (BattleActionCommand == Database.ARCHETYPE_VERZE))
                {
                    UpdateBattleText(player.GetCharacterSentence(205));
                    return;
                }

                this.currentTargetedPlayer = player;
                this.currentTargetedPlayer.ReserveBattleCommand = BattleActionCommand;

                //if (ActionCommandAttribute.IsOwnTarget(BattleActionCommand))
                if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Own)
                {
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                    // 自分自身が対象の場合、指定対象選択は不要
                    this.currentTargetedPlayer.Target2 = this.currentTargetedPlayer;
                    this.currentTargetedPlayer.ReserveBattleCommand = string.Empty;
                }
                else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
                {
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                    // 敵全員、味方全員、敵味方全員が対象の場合、何かをターゲットしなおす事はしない。
                    this.currentTargetedPlayer.ReserveBattleCommand = string.Empty;
                }
                else
                {
                    this.NowSelectingTarget = true;
                    //this.Invalidate();
                }
            }
        }


        public void buttonTargetPlayer_Click(Button sender)
        {
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) { this.tempTargetForInstant = ActiveList[ii]; }
                    }

                    if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.Archetype) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.NormalAttack))
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4076));
                        return;
                    }
                    ExecActionMethod(this.currentTargetedPlayer, this.tempTargetForInstant, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                }
                else
                {
                    MainCharacter memoTarget = null;
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) { memoTarget = ActiveList[ii]; }
                    }

                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }

                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.currentTargetedPlayer.Target2 = memoTarget;
                    }
                    else
                    {
                        this.currentTargetedPlayer.Target = memoTarget;
                    }
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                this.NowSelectingTarget = false;
                //this.Invalidate();
            }
        }

        public void buttonTargetEnemy_Click(Button sender)
        {
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) { this.tempTargetForInstant = ActiveList[ii]; }
                    }

                    if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == MainCharacter.PlayerAction.Archetype) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    ExecActionMethod(this.currentTargetedPlayer, this.tempTargetForInstant, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                }
                else
                {
                    MainCharacter memoTarget = null;
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) { memoTarget = ActiveList[ii]; }
                    }

                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }

                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.currentTargetedPlayer.Target2 = memoTarget;
                    }
                    else
                    {
                        this.currentTargetedPlayer.Target = memoTarget;
                    }
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                this.NowSelectingTarget = false;
                //this.Invalidate();
            }
        }


        public void BattleStart_Click()
        {
            if (false) // th == null)
            {
                // timerBattleStart.Stop(); // todo Ready...の表示に関連する
                if (this.DuelMode)
                {
                    BattleStart.text = "DUEL中・・・";
                    //BattleStart.Enabled = false; // todo
                }
                else
                {
                    BattleStart.text = "戦闘中・・・";
                }
                //this.BattleMenuPanel.Visible = false; // todo ただし本当にチェンジする必要があるか？
                //th = new Thread(new System.Threading.ThreadStart(BattleLoop));
                //th.IsBackground = true;
                //th.Start();
            }
            else
            {
                if ((BattleStart.text == "戦闘中・・・") ||
                    (BattleStart.text == "DUEL中・・・"))
                {
                    if (this.DuelMode)
                    {
                        // DUELでは途中一旦停止は出来ない事とする。
                    }
                    else
                    {
                        BattleStart.text = "戦闘停止";
                        //this.BattleMenuPanel.Visible = true; // todo ただし本当にチェンジする必要があるか？
                    }
                    tempStopFlag = true;
                }
                else
                {
                    // timerBattleStart.Stop(); // todo Ready...の表示に関連する
                    BattleStart.text = "戦闘中・・・";
                    tempStopFlag = false;
                    gameStart = true;
                    //this.BattleMenuPanel.Visible = false; // todo ただし本当にチェンジする必要があるか？
                }
            }
        }


        private void UpdateBattleText(string text)
        {
            if (debugMessage != null)
            {
                string temp = debugMessage.text;
                debugMessage.text = "";
                debugMessage.text = text + temp;
            }

        }
        private void UpdateBattleText(string text, int sleepTime)
        {
            if (debugMessage != null)
            {
                string temp = debugMessage.text;
                debugMessage.text = "";
                debugMessage.text = text + temp;
            }
            this.Update();

            if (sleepTime > 0)
            {
                System.Threading.Thread.Sleep(sleepTime);
            }
        }
        private void UpdateMessage(string txt)
        {
            if (debugMessage != null)
            {
                string temp = debugMessage.text;
                debugMessage.text = "";
                debugMessage.text = txt + temp;
            }
        }

        private MainCharacter WhoTarget(MainCharacter player, string command)
        {
            MainCharacter target = null;
            TruthActionCommand.TargetType type = TruthActionCommand.GetTargetType(command);
            if (type == TruthActionCommand.TargetType.Ally)
            {
                target = this.currentPlayer.Target2;
            }
            else if (type == TruthActionCommand.TargetType.Enemy)
            {
                target = this.currentPlayer.Target;
            }
            else if (type == TruthActionCommand.TargetType.Own)
            {
                target = this.currentPlayer;
            }
            else if (type == TruthActionCommand.TargetType.InstantTarget)
            {
                return null;
            }
            return target;
        }
        public void tapActionButton(Button obj)
        {
            string command = obj.name;
            //buttonTargetPlayer1.image.sprite = obj.image.sprite;
            txtBattleMessage.text = command;

            ActionCommand(false, GroundOne.MC, obj.name);

            //PlayerInstantCommand(this.currentPlayer, WhoTarget(this.currentPlayer, command), command);
        }
        public void tapActionButton2(Button obj)
        {
            string command = obj.name;
            buttonTargetPlayer2.image.sprite = obj.image.sprite;
            txtBattleMessage.text = command;

            ActionCommand(false, GroundOne.SC, obj.name);

            //PlayerInstantCommand(this.currentPlayer, WhoTarget(this.currentPlayer, command), command);
        }
        public void tapActionButton3(Button obj)
        {
            string command = obj.name;
            buttonTargetPlayer3.image.sprite = obj.image.sprite;
            txtBattleMessage.text = command;

            ActionCommand(false, GroundOne.TC, obj.name);

            //PlayerInstantCommand(this.currentPlayer, WhoTarget(this.currentPlayer, command), command);
        }

        public void tapBattleSetting()
        {
            Debug.Log("tapBattleSetting");
            GroundOne.CallBattleSetting = true;
            SceneDimension.Go(Database.TruthBattleEnemy, Database.TruthBattleSetting);
        }
        public void tapPanel1()
        {
            this.currentPlayer = GroundOne.MC;
            GroundOne.MC.EnableGUI();
            GroundOne.SC.DisableGUI();
            GroundOne.TC.DisableGUI();
        }
        public void tapPanel2()
        {
            this.currentPlayer = GroundOne.SC;
            GroundOne.MC.DisableGUI();
            GroundOne.SC.EnableGUI();
            GroundOne.TC.DisableGUI();
        }
        public void tapPanel3()
        {
            this.currentPlayer = GroundOne.TC;
            GroundOne.MC.DisableGUI();
            GroundOne.SC.DisableGUI();
            GroundOne.TC.EnableGUI();
        }

        public void tapFirstChara()
        {
            this.currentPlayer.Target2 = GroundOne.MC;
        }
        public void tapSecondChara()
        {
            this.currentPlayer.Target2 = GroundOne.SC;
        }
        public void tapThirdChara()
        {
            this.currentPlayer.Target2 = GroundOne.TC;
        }
        private void ChangeBaseAction(MainCharacter player)
        {
            // future function
        }
        public void tapFirstCharaAction()
        {
            ChangeBaseAction(GroundOne.MC);
        }
        public void tapSecondCharaAction()
        {
            ChangeBaseAction(GroundOne.SC);
        }
        public void tapThirdCharaAction()
        {
            ChangeBaseAction(GroundOne.TC);
        }

        // 通常攻撃を抽象化したロジック。通常攻撃やストレートスマッシュは全てここに含まれる。
        private void AbstractPhysicalAttack(MainCharacter player, MainCharacter target, string command, double value)
        {
            if (target.CurrentProtection > 0) { value = value * 0.7f; }
            target.CurrentLife -= (int)value;
            if (target.CurrentLife < 0) { target.CurrentLife = 0; }
            UpdateLife(target);
            UpdateMessage(player.labelName.text + " " + command + " " + ((int)value).ToString() + " \n");
        }
        private void AbstractMagicAttack(MainCharacter player, MainCharacter target, string command, double value)
        {
            if (player.CurrentShadowPact > 0) { value = value * 1.3f; }
            target.CurrentLife -= (int)value;
            if (target.CurrentLife < 0) { target.CurrentLife = 0; }
            UpdateLife(target);
            UpdateMessage(player.labelName.text + " " + command + " " + ((int)value).ToString() + " \n");
        }
        // 通常攻撃
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense, bool ignoreDoubleAttack)
        {
            return PlayerNormalAttack(player, target, magnification, 0, ignoreDefense, false, 0, 0, string.Empty, -1, ignoreDoubleAttack, CriticalType.Random);
        }
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, int crushingBlow, bool ignoreDefense, bool skipCounterPhase, double atkBase, int interval, string soundName, int textNumber, bool ignoreDoubleAttack, CriticalType critical)
        {
            //double value = player.TotalStrength; 
            //AbstractPhysicalAttack(player, target, Database.ATTACK_EN, value);

             // todo
            //if (skipCounterPhase == false)
            //{
            //    if (CheckCounterAttack(player))
            //    {
            //        PlayerNormalAttack(target, player, 0, false, false);
            //        return;
            //    }
            //}

            for (int ii = 0; ii < 2; ii++) // サブウェポンによる2回攻撃を考慮
            {
                // 攻撃ミス判定する前にGlory効果（Gloryは自身対象なので、適用対象ＯＫ仕様は前編時代と同じ）
                // Gloryによる効果
                if (player.CurrentGlory > 0)
                {
                    MainCharacter memoTarget = player.Target;
                    player.Target = player;
                    PlayerSpellFreshHeal(player, player);
                    player.Target = memoTarget;
                }

                // todo
                // ミス判定
                //if (CheckDodge(player, target, false))
                //{
                //    // 回避された場合、ダメージは発生しない。デフレクション判定なども同様。
                //    // 一番下にサブウェポンによる二回攻撃判定があるので、それは別とする。
                //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                //}
                //else if (CheckBlindMiss(player, target))
                //{
                //    // 暗闇により攻撃を外した場合ダメージは発生しない。デフレクション判定なども同様。
                //    // 一番下にサブウェポンによる二回攻撃判定があるので、それは別とする。
                //    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                //}
                //else
                {
                    double damage = 0;
                    // ダメージ加算
                    if (atkBase == 0)
                    {
                        if (ii == 0)
                        {
                            damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, DungeonPlayer.MainCharacter.PlayerStance.FrontOffence, PrimaryLogic.SpellSkillType.Standard, this.DuelMode);
                        }
                        else
                        {
                            damage = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0, 0, 0, 1.0F, DungeonPlayer.MainCharacter.PlayerStance.FrontOffence, this.DuelMode);
                        }
                    }
                    else
                    {
                        damage = atkBase;
                    }
                    if (magnification > 0)
                    {
                        damage = damage * magnification;
                    }
                    if (player.CurrentSaintPower > 0)
                    {
                        damage = damage * 1.5F;
                    }
                    if (player.CurrentEternalPresence > 0)
                    {
                        damage = damage * 1.3F;
                    }
                    if (player.CurrentAetherDrive > 0)
                    {
                        damage = damage * 2.0F;
                    }
                    if (player.CurrentBlindJustice > 0)
                    {
                        damage = damage * 1.7F;
                    }
                    if (player.CurrentRisingAura > 0)
                    {
                        damage = damage * 1.4F;
                    }
                    if (player.CurrentMazeCube > 0)
                    {
                        damage = damage * PrimaryLogic.MazeCubeValue(player);
                    }
                    if (player.CurrentEternalFateRing > 0)
                    {
                        damage = damage * PrimaryLogic.EternalFateRingValue(player);
                    }

                    // ダメージ軽減
                    damage -= PrimaryLogic.PhysicalDefenseValue(target, PrimaryLogic.NeedType.Random, this.DuelMode);
                    if (damage <= 0.0f) damage = 0.0f;

                    if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = (int)((float)damage / 1.2F);
                    }
                    if (target.CurrentEternalPresence > 0)
                    {
                        damage = damage * 0.8F;
                    }
                    if (target.CurrentExaltedField > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = damage / 1.4F;
                    }
                    if (target.CurrentAetherDrive > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = damage * 0.5f;
                    }

                    if (ignoreDefense == false)
                    {
                        if (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                        {
                            if ((target.CurrentAbsoluteZero > 0) ||
                                (target.CurrentFrozen > 0) ||
                                (target.CurrentParalyze > 0) ||
                                (target.CurrentStunning > 0))
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                if (target.SubWeapon != null)
                                {
                                    if (target.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                                    {
                                        damage = damage / 4.0f;
                                    }
                                    else
                                    {
                                        damage = damage / 3.0f;
                                    }
                                }
                                else
                                {
                                    damage = damage / 3.0f;
                                }
                            }
                        }
                        // ワン・イムーニティによる軽減
                        if (target.CurrentOneImmunity > 0 && (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                        {
                            if (target.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                damage = 0;
                            }
                        }
                    }

                    // クリティカル判定
                    bool detectCritical = false;
                    if (critical == CriticalType.Random) detectCritical = PrimaryLogic.CriticalDetect(player);
                    if (critical == CriticalType.None) detectCritical = false;
                    if (critical == CriticalType.Absolute) detectCritical = true;
                    if (crushingBlow > 0) detectCritical = false;
                    if (IsPlayerEnemy(player))
                    {
                        if (((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area11 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area12 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area13 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area14 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area21 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area22 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area23 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area24 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area31 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area32 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area33 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area34 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area41 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area42 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area44)
                        //((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area51) 最後の雑魚ぐらいはクリティカル通常判定で。
                        {
                            if (AP.Math.RandomInteger(2) != 0) // 雑魚クリティカルは二分の一に機会を減らす
                            {
                                detectCritical = false;
                            }
                        }
                    }
                    if (detectCritical)
                    {
                        damage = damage * PrimaryLogic.CriticalDamageValue(player, this.DuelMode);
                        if (player.CurrentSinFortune > 0)
                        {
                            damage = damage * PrimaryLogic.SinFortuneValue(player);
                            player.RemoveSinFortune();
                        }
                    }

                    // 効果音の再生
                    if (soundName != string.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    else
                    {
                        if (player == ec1 || player == ec2 || player == ec3)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                        }
                        else
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_SWORD_SLASH1);
                        }
                    }

                    // todo
                    // デフレクション効果はクリティカル値も反映させる
                    // デフレクションによる物理攻撃反射
                    //if (skipCounterPhase)
                    //{
                    //    if (target.CurrentDeflection > 0)
                    //    {
                    //        UpdateBattleText(target.GetCharacterSentence(62));
                    //        this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_DEFLECTION);
                    //        target.CurrentDeflection = 0;
                    //        target.pbDeflection.Image = null;
                    //        target.pbDeflection.Update();
                    //    }
                    //}
                    //else
                    //{
                    //    if (target.CurrentDeflection > 0)
                    //    {
                    //        damage = DamageIsZero(damage, player);
                    //        LifeDamage(damage, player);
                    //        target.CurrentDeflection = 0;
                    //        target.pbDeflection.Image = null;
                    //        target.pbDeflection.Update();
                    //        return true;
                    //    }
                    //}

                    // StaticBarrierによる効果
                    if (target.CurrentStaticBarrier > 0)
                    {
                        target.CurrentStaticBarrierValue--;
                        target.ChangeStaticBarrierStatus(target.CurrentStaticBarrierValue);
                        damage = damage * 0.5f;
                    }

                    // todo
                    // StanceOfMysticによる効果
                    //if (target.CurrentStanceOfMysticValue > 0)
                    //{
                    //    target.CurrentStanceOfMysticValue--;
                    //    target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMysticValue);
                    //    damage = 0;
                    //    LifeDamage(damage, target, interval, false);
                    //    return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
                    //}
                    //// HardestParryによる効果
                    //if (target.CurrentHardestParry)
                    //{
                    //    target.CurrentHardestParry = false;
                    //    damage = 0;
                    //    LifeDamage(damage, target, interval, false);
                    //    return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
                    //}

                    // todo
                    // ダメージ０変換
                    //damage = DamageIsZero(damage, target);

                    //// スケール・オブ・ブルーレイジによる効果
                    //if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_SCALE_BLUERAGE))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.ScaleOfBlueRageValue(player))
                    //    {
                    //        this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                    //        damage = 0;
                    //    }
                    //}
                    //// スライド・スルー・シールドによる効果
                    //if ((target.SubWeapon != null) && (target.SubWeapon.Name == Database.RARE_SLIDE_THROUGH_SHIELD))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.SlideThroughShieldValue(player))
                    //    {
                    //        this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                    //        damage = 0;
                    //    }
                    //}

                    // メッセージ更新
                    if (detectCritical)
                    {
                        UpdateBattleText(player.GetCharacterSentence(117));
                    }
                    if (soundName == Database.SOUND_STRAIGHT_SMASH)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(124), target.Name, (int)damage), interval);
                    }
                    else if (textNumber != -1)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(textNumber), target.Name, (int)damage), interval);
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(115), target.Name, (int)damage), interval);
                    }

                    // ライフを更新
                    LifeDamage(damage, target, interval, detectCritical);

                    // todo
                    // アビス・ファイアによる効果
                    //if (player.CurrentAbyssFire > 0)
                    //{
                    //    double effectValue = PrimaryLogic.AbyssFireValue(target); // ダメージ発生源はレギィンアーゼ
                    //    LifeDamage(effectValue, player, interval, detectCritical);
                    //    UpdateBattleText(String.Format(player.GetCharacterSentence(120), player.Name, ((int)effectValue).ToString()), interval);
                    //}

                    //// シェズル・ミラージュ・ランサーの場合、ダブルヒット扱いとする。
                    //if (((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)) ||
                    //    ((ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)))
                    //{
                    //    LifeDamage(damage, target, interval, detectCritical);
                    //}

                    //// 対象者のシール・オブ・バランスによる効果
                    //if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_BALANCE))
                    //{
                    //    PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    //}
                    //if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_BALANCE))
                    //{
                    //    PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    //}

                    //// 集中と断絶効果がある場合、途切れさす
                    //if (player.CurrentSyutyu_Danzetsu > 0)
                    //{
                    //    player.CurrentSyutyu_Danzetsu = 0;
                    //    player.DeBuff(player.pbSyutyuDanzetsu);
                    //}

                    //// HolyBreakerによるダメージ反射
                    //if (target.CurrentHolyBreaker > 0)
                    //{
                    //    LifeDamage(damage, player);
                    //}

                    //// 黒氷刀による効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_BLACK_ICE_SWORD) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_BLACK_ICE_SWORD))
                    //{
                    //    double effectValue = PrimaryLogic.BlackIceSwordValue(player);
                    //    effectValue = GainIsZero(effectValue, player);
                    //    player.CurrentMana += (int)effectValue;
                    //    UpdateMana(player, (int)effectValue, true, true, 0);
                    //}
                    //// メンタライズド・フォース・クローによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW))
                    //{
                    //    double effectValue = PrimaryLogic.MentalizedForceClawValue(player);
                    //    effectValue = GainIsZero(effectValue, player);
                    //    player.CurrentSkillPoint += (int)effectValue;
                    //    UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    //}
                    //// クレイモア・オブ・ザックスによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_CLAYMORE_ZUKS))
                    //{
                    //    double effectValue = PrimaryLogic.ClaymoreZuksValue(player);
                    //    effectValue = GainIsZero(effectValue, player);
                    //    player.CurrentLife += (int)effectValue;
                    //    UpdateLife(player, (int)effectValue, true, true, 0, false);
                    //}
                    //// ソード・オブ・ディバイドによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_SWORD_OF_DIVIDE) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_SWORD_OF_DIVIDE))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.SwordOfDivideValue_A(player))
                    //    {
                    //        double effectValue = PrimaryLogic.SwordOfDivideValue(target);
                    //        effectValue = DamageIsZero(effectValue, target);
                    //        LifeDamage(effectValue, target);
                    //    }
                    //}
                    //// 真紅炎・マスターブレイドによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.SinkouenMasterBladeValue_A(player))
                    //    {
                    //        // ワード・オブ・パワーを発動
                    //        PlayerSpellWordOfPower(player, target, 0, 0);
                    //    }
                    //}
                    //// デビルキラーによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_DEVIL_KILLER) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_DEVIL_KILLER))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.DevilKillerValue(player))
                    //    {
                    //        PlayerDeath(player, target);
                    //    }
                    //}

                    //// ジュザ・ファンタズマル・クローによる効果
                    //if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW) ||
                    //    (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_JUZA_PHANTASMAL);
                    //}

                    //// エターナル・フェイトリングによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_ETERNAL_FATE);
                    //}
                    //if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_ETERNAL_FATE);
                    //}

                    //// エターナル・ロイヤルリングによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    //{
                    //    double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                    //    effectValue = GainIsZero(effectValue, player);
                    //    player.CurrentSkillPoint += (int)effectValue;
                    //    UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    //}
                    //if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    //{
                    //    double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                    //    effectValue = GainIsZero(effectValue, player);
                    //    player.CurrentSkillPoint += (int)effectValue;
                    //    UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    //}

                    //// ライト・サーヴァントによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_LIGHT_SERVANT))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    //}
                    //if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_LIGHT_SERVANT))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    //}

                    //// シャドウ・サーヴァントによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_SHADOW_SERVANT))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    //}
                    //if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_SHADOW_SERVANT))
                    //{
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    //}

                    //// メイズ・キューブによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory.SwitchStatus1 == false))
                    //{
                    //    player.Accessory.SwitchStatus1 = true;
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
                    //}
                    //if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory2.SwitchStatus1 == false))
                    //{
                    //    player.Accessory2.SwitchStatus1 = true;
                    //    PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
                    //}

                    //// エムブレム・オブ・ヴァルキリーによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_VALKYRIE) ||
                    //    (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_VALKYRIE))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfValkyrieValue(player))
                    //    {
                    //        NowStunning(player, target, (int)PrimaryLogic.EmblemOfValkyrieValue_A(player));
                    //    }
                    //}
                    //// エムブレム・オブ・ハデスによる効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_HADES) ||
                    //    (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_HADES))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfHades(player))
                    //    {
                    //        PlayerDeath(player, target);
                    //    }
                    //}
                    //// 氷絶零の宝珠による効果
                    //if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_ORB_SILENT_COLD_ICE) ||
                    //    (player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_ORB_SILENT_COLD_ICE))
                    //{
                    //    if (AP.Math.RandomInteger(100) < PrimaryLogic.SilentColdIceValue(player))
                    //    {
                    //        NowFrozen(player, target, (int)PrimaryLogic.SilentColdIceValue_A(player));
                    //        target.RemoveBuffSpell();
                    //    }
                    //}

                    //// CrushingBlowによる気絶
                    //if (crushingBlow > 0)
                    //{
                    //    UpdateBattleText(String.Format(player.GetCharacterSentence(70), target.Name, (int)damage));
                    //    if (target.CurrentAntiStun > 0)
                    //    {
                    //        target.RemoveAntiStun();
                    //        UpdateBattleText(target.GetCharacterSentence(94));
                    //    }
                    //    else
                    //    {
                    //        if ((target.Accessory != null) && (target.Accessory.Name == "鋼鉄の石像"))
                    //        {
                    //            System.Random rd3 = new System.Random(DateTime.Now.Millisecond * Environment.TickCount);
                    //            if (rd3.Next(1, 101) <= target.Accessory.MinValue)
                    //            {
                    //                UpdateBattleText(target.Name + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                    //                UpdateBattleText(target.Name + "はスタン状態に陥らなかった。\r\n");
                    //            }
                    //            else
                    //            {
                    //                NowStunning(player, target, crushingBlow);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            NowStunning(player, target, crushingBlow);
                    //        }
                    //    }
                    //}

                    //// FlameAuraによる追加攻撃
                    //if (player.CurrentFlameAura > 0)
                    //{
                    //    double additional = PrimaryLogic.FlameAuraValue(player, this.DuelMode);
                    //    if (ignoreDefense == false)
                    //    {
                    //        if (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    //        {
                    //            if (target.CurrentAbsoluteZero > 0)
                    //            {
                    //                UpdateBattleText(target.GetCharacterSentence(88));
                    //            }
                    //            else
                    //            {
                    //                additional = (int)((float)additional / 3.0F);
                    //            }
                    //        }
                    //        if (target.CurrentOneImmunity > 0 && (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    //        {
                    //            if (target.CurrentAbsoluteZero > 0)
                    //            {
                    //                UpdateBattleText(target.GetCharacterSentence(88));
                    //            }
                    //            else
                    //            {
                    //                additional = 0;
                    //            }
                    //        }
                    //    }

                    //    additional = DamageIsZero(additional, target);
                    //    LifeDamage(additional, target, interval);
                    //    UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                    //}
                    //// FrozenAuraによる追加攻撃
                    //if (player.CurrentFrozenAura > 0)
                    //{
                    //    double additional = PrimaryLogic.FrozenAuraValue(player, this.DuelMode);
                    //    if (ignoreDefense == false)
                    //    {
                    //        if (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    //        {
                    //            if (target.CurrentAbsoluteZero > 0)
                    //            {
                    //                UpdateBattleText(target.GetCharacterSentence(88));
                    //            }
                    //            else
                    //            {
                    //                additional = (int)((float)additional / 3.0F);
                    //            }
                    //        }
                    //        if (target.CurrentOneImmunity > 0 && (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    //        {
                    //            if (target.CurrentAbsoluteZero > 0)
                    //            {
                    //                UpdateBattleText(target.GetCharacterSentence(88));
                    //            }
                    //            else
                    //            {
                    //                additional = 0;
                    //            }
                    //        }
                    //    }
                    //    additional = DamageIsZero(additional, target);
                    //    LifeDamage(additional, target, interval);
                    //    UpdateBattleText(String.Format(player.GetCharacterSentence(140), additional.ToString()));
                    //}

                    // ImmortalRaveによる追加攻撃
                    if (player.CurrentImmortalRave == 3)
                    {
                        PlayerSpellFireBall(player, target, 0, 0);
                    }
                    else if (player.CurrentImmortalRave == 2)
                    {
                        PlayerSpellFlameStrike(player, target, 0, 0);
                    }
                    else if (player.CurrentImmortalRave == 1)
                    {
                        PlayerSpellVolcanicWave(player, target, 0, 0);
                    }
                }

                // サブウェポンがある場合、二回攻撃となる。
                if (player.SubWeapon == null)
                {
                    return true;
                }
                if (player.SubWeapon.Name == "")
                {
                    return true;
                }
                if ((player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Rod || player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand || player.SubWeapon.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
                // スキルや特殊攻撃からの場合、サブウェポン2回攻撃を強制的に実施しない場合
                if (ignoreDoubleAttack)
                {
                    return true;
                }
            }
            return true;
        }
        // 魔法攻撃
        private void PlayerMagicAttack(MainCharacter player, MainCharacter target)
        {
            double value = player.MagicAttackValue;
            AbstractMagicAttack(player, target, "Magical", value);
        }


        // フレイムオーラ
        private void PlayerSpellFlameAura(MainCharacter player, MainCharacter target)
        {
            target.ActivateFlameAura(imgFlameAura, 99999);
        }
        // アイスニードル
        private void PlayerSpellIceNeedle(MainCharacter player, MainCharacter target, int p1, int p2)
        {
            double value = 30.0 + player.Intelligence * 1.9f;
            AbstractMagicAttack(player, target, Database.ICE_NEEDLE, value);
            // 戦闘速度Down
        }
        // クリーンジング
        private void PlayerSpellCleansing(MainCharacter player, MainCharacter target)
        {
            // -BUFF除去
            target.RemovePoison();
        }
        // フレッシュヒール
        private void PlayerSpellFreshHeal(MainCharacter player, MainCharacter target)
        {
            if (player.CurrentMana < Database.FRESH_HEAL_COST)
            {
                UpdateMessage(player.labelName.text + " Not enough mana \n");
                return;
            }
            player.CurrentMana -= Database.FRESH_HEAL_COST;
            UpdateMana(player);

            double value = 30.0 + player.Intelligence * 2.0f;
            target.CurrentLife += (int)value;
            if (target.CurrentLife > target.MaxLife) { target.CurrentLife = target.MaxLife; }
            UpdateLife(target);
            UpdateMessage(" target2 name " + player.Target2.Name); // = " + player.Target2.ToString()); }
            UpdateMessage(player.labelName.text + " " + Database.FRESH_HEAL + " to " + player.Target2.Name + " " + value);
        }
        // ダークブラスト
        private void PlayerSpellDarkBlast(MainCharacter player, MainCharacter target)
        {
            double value = 30.0 + player.Intelligence * 1.7f;
            AbstractMagicAttack(player, target, Database.DARK_BLAST, value);
            // 魔法攻撃Down
        }

        // ワードオブパワー
        private void PlayerSpellWordOfPower(MainCharacter player, MainCharacter target)
        {
            double value = 30.0 + player.Strength * 1.6f;
            AbstractMagicAttack(player, target, Database.WORD_OF_POWER, value);
        }
        // ワードオブライフ
        private void PlayerSpellWordOfLife(MainCharacter player, MainCharacter target)
        {
            target.ActivateWordOfLife(imgWordOfLife, 99999);
        }
        // ディスペルマジック
        private void PlayerSpellDispelMagic(MainCharacter player, MainCharacter target)
        {
            //target...
        }
        // デフレクション
        private void PlayerSpellDeflection(MainCharacter player, MainCharacter target)
        {
            target.ActivateDeflection(imgDeflection, 99999);
        }
        // ストレートスマッシュ
        private void PlayerSkillStraightSmash(MainCharacter player, MainCharacter target)
        {
            double value = 30.0 + player.Strength * 1.0f + player.Agility * 1.0f;
            AbstractPhysicalAttack(player, target, Database.STRAIGHT_SMASH, value);
        }

        // スタンスオブフロー
        private void PlayerSkillStanceOfFlow(MainCharacter player, MainCharacter target)
        {
            // 必ず後攻
        }
        // スタンスオブスタンディング
        private void PlayerSkillStanceOfStanding(MainCharacter player, MainCharacter target)
        {
        }
        // トゥルスビジョン
        private void PlayerSkillTruthVision(MainCharacter player, MainCharacter target)
        {
            target.ActivateTruthVision(imgTruthVision, 99999);
        }
        // ニゲイト
        private void PlayerSkillNegate(MainCharacter player, MainCharacter target)
        {
            // negate is stack-in-the-command only
        }

        private void UpdateLife(MainCharacter player, double damage, bool plusValue, bool animationDamage, int interval, bool critical)
        {
            UpdateLife(player);
            // todo
            //if (player.labelLife != null)
            //{
            //    player.labelLife.Text = player.CurrentLife.ToString();
            //    if (player.CurrentLife >= player.MaxLife)
            //    {
            //        player.labelLife.ForeColor = Color.Green;
            //        if (this.NowTimeStop)
            //        {
            //            player.labelLife.ForeColor = Color.LightGreen;
            //        }
            //    }
            //    else
            //    {
            //        player.labelLife.ForeColor = Color.Black;
            //        if (this.NowTimeStop)
            //        {
            //            player.labelLife.ForeColor = Color.White;
            //        }
            //    }
            //    player.labelLife.Update();
            //}
            //if (animationDamage)
            //{
            //    Color color = Color.Black;
            //    if (plusValue)
            //    {
            //        color = Color.Green;
            //        if (this.NowTimeStop)
            //        {
            //            color = Color.LightGreen;
            //        }
            //    }
            //    else if (this.NowTimeStop)
            //    {
            //        color = Color.White;
            //    }
            //    this.Invoke(new _AnimationDamage(AnimationDamage), damage, player, interval, color, false, critical, String.Empty);
            //}
        }
        private void UpdateLife(MainCharacter player)
        {
            float dx = (float)player.CurrentLife / (float)player.MaxLife;
            if (player.labelCurrentLifePoint != null)
            {
                player.labelCurrentLifePoint.text = player.CurrentLife.ToString();
            }
            if (player.meterCurrentLifePoint != null)
            {
                player.meterCurrentLifePoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }

        private void UpdateMana(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            UpdateMana(player);
            // todo
            //if (player.labelCurrentManaPoint != null)
            //{
            //    player.labelCurrentManaPoint.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            //    player.labelCurrentManaPoint.Update();
            //}
            //if (animationDamage)
            //{
            //    this.Invoke(new _AnimationDamage(AnimationDamage), effectValue, player, interval, Color.Blue, false, false, String.Empty);
            //}
        }
        private void UpdateMana(MainCharacter player)
        {
            float dx = (float)player.CurrentMana / (float)player.MaxMana;
            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.text = player.CurrentMana.ToString();
            }
            if (player.meterCurrentManaPoint != null)
            {
                player.meterCurrentManaPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }
        private void UpdateSkillPoint(MainCharacter player)
        {
            float dx = (float)player.CurrentSkillPoint / (float)player.MaxSkillPoint;
            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.text = player.CurrentSkillPoint.ToString();
            }
            if (player.meterCurrentSkillPoint != null)
            {
                player.meterCurrentSkillPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }

        private void UpdateInstantPoint(MainCharacter player, Text text, Image meter)
        {
            float dx = (float)player.CurrentInstantPoint / (float)player.MaxInstantPoint;
            if (text != null)
            {
                text.text = player.CurrentInstantPoint.ToString();
            }
            if (meter != null)
            {
                meter.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }

        private void PlayerInstantCommand(MainCharacter player, MainCharacter target, string command)
        {
            // プレイヤーがタップして実行するアクションはインスタントポイントを必要とする。
            if (player.CurrentInstantPoint < player.MaxInstantPoint)
            {
                return;
            }
            player.CurrentInstantPoint = 0;

            PlayerExecCommand(player, target, command);
        }
        private void PlayerExecCommand(MainCharacter player, MainCharacter target, string command)
        {
            if (command == Database.ATTACK_EN)
            {
                PlayerNormalAttack(player, target, 0, false, false);
            }
            else if (command == Database.FRESH_HEAL)
            {
                PlayerSpellFreshHeal(player, target);
            }
            else if (command == Database.PROTECTION)
            {
                PlayerSpellProtection(player, target);
            }
            else if (command == Database.DARK_BLAST)
            {
                PlayerSpellDarkBlast(player, target);
            }
            else if (command == Database.SHADOW_PACT)
            {
                PlayerSpellShadowPact(player, target);
            }
            else if (command == Database.FIRE_BALL)
            {
                PlayerSpellFireBall(player, target, 0, 0);
            }
            else if (command == Database.FLAME_AURA)
            {
                PlayerSpellFlameAura(player, target);
            }
            else if (command == Database.ICE_NEEDLE)
            {
                PlayerSpellIceNeedle(player, target, 0, 0);
            }
            else if (command == Database.CLEANSING)
            {
                PlayerSpellCleansing(player, target);
            }
            else if (command == Database.WORD_OF_POWER)
            {
                PlayerSpellWordOfPower(player, target);
            }
            else if (command == Database.WORD_OF_LIFE)
            {
                PlayerSpellWordOfLife(player, target);
            }
            else if (command == Database.DISPEL_MAGIC)
            {
                PlayerSpellDispelMagic(player, target);
            }
            else if (command == Database.DEFLECTION)
            {
                PlayerSpellDeflection(player, target);
            }
            else if (command == Database.STRAIGHT_SMASH)
            {
                PlayerSkillStraightSmash(player, target);
            }
            else if (command == Database.COUNTER_ATTACK)
            {
                PlayerSkillCounterAttack(player, target);
            }
            else if (command == Database.STANCE_OF_FLOW)
            {
                PlayerSkillStanceOfFlow(player, player);
            }
            else if (command == Database.STANCE_OF_STANDING)
            {
                PlayerSkillStanceOfStanding(player, target);
            }
            else if (command == Database.TRUTH_VISION)
            {
                PlayerSkillTruthVision(player, target);
            }
            else if (command == Database.NEGATE)
            {
                PlayerSkillNegate(player, target);
            }
        }

        private void PlayerSkillCounterAttack(MainCharacter player, MainCharacter target)
        {
            //throw new System.NotImplementedException();// todo
        }

        private void UseItemButton_Click()
        {
            // todo バックパック画面を開いて、消耗品アイテムを使用する。
            //if (UseItemGauge.Width < 600)
            //{
            //    if (mc.Dead == false)
            //    {
            //        UpdateBattleText(mc.GetCharacterSentence(125));
            //    }
            //    else if (we.AvailableSecondCharacter && sc.Dead == false)
            //    {
            //        UpdateBattleText(sc.GetCharacterSentence(125));
            //    }
            //    else if (we.AvailableThirdCharacter && tc.Dead == false)
            //    {
            //        UpdateBattleText(tc.GetCharacterSentence(125));
            //    }
            //    return;
            //}

            //using (TruthStatusPlayer TSP = new TruthStatusPlayer())
            //{
            //    TSP.WE = we;
            //    TSP.MC = mc;
            //    TSP.SC = sc;
            //    TSP.TC = tc;
            //    TSP.StartPosition = FormStartPosition.CenterParent;
            //    TSP.OnlyUseItem = true;
            //    TSP.DuelMode = this.DuelMode;
            //    if (mc.Dead == false)
            //    {
            //        TSP.CurrentStatusView = Color.LightSkyBlue;
            //    }
            //    else if (we.AvailableSecondCharacter && sc.Dead == false)
            //    {
            //        TSP.CurrentStatusView = Color.Pink;
            //    }
            //    else if (we.AvailableThirdCharacter && tc.Dead == false)
            //    {
            //        TSP.CurrentStatusView = Color.Gold;
            //    }
            //    TSP.ShowDialog();

            //    if (TSP.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    {
            //        if (we.AvailableFirstCharacter)
            //        {
            //            mc = TSP.MC;
            //            UpdateLife(mc, 0, false, false, 0, false);
            //            UpdateSkillPoint(mc, 0, false, false, 0);
            //            UpdateMana(mc, 0, false, false, 0);
            //        }
            //        if (we.AvailableSecondCharacter && this.DuelMode == false)
            //        {
            //            sc = TSP.SC;
            //            UpdateLife(sc, 0, false, false, 0, false);
            //            UpdateSkillPoint(sc, 0, false, false, 0);
            //            UpdateMana(sc, 0, false, false, 0);
            //        }
            //        if (we.AvailableThirdCharacter && this.DuelMode == false)
            //        {
            //            tc = TSP.TC;
            //            UpdateLife(tc, 0, false, false, 0, false);
            //            UpdateSkillPoint(tc, 0, false, false, 0);
            //            UpdateMana(tc, 0, false, false, 0);
            //        }

            //        UseItemGauge.Width = 0;
            //    }
            //}
        }
        private void RunAwayButton_Click()
        {
            if (this.cannotRunAway)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは今逃げられない状態に居る。\r\n");
                return;
            }

            // todo
            //if (th != null)
            //{
            //    tempStopFlag = true;
            //    endFlag = true;
            //}
            //else
            //{
            //    if (this.DuelMode)
            //    {
            //        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "アインは降参を宣言した。\r\n");
            //    }
            //    else
            //    {
            //        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "アインは逃げ出した。\r\n");
            //    }

            //    txtBattleMessage.Update();
            //    System.Threading.Thread.Sleep(1000);
            string backScene = SceneDimension.playbackScene[0];
            SceneDimension.playbackScene.Clear();
            Application.LoadLevel(backScene);
            //}
        }

        public void OnMouseEnterImage(Button sender)
        {
        }

        delegate void _AnimationDamage(double damage, MainCharacter target, int interval, Color plusValue, bool avoid, bool critical, string customString);

        private void AnimationDamage(double damage, MainCharacter target, int interval, Color plusValue, bool avoid, bool critical, string customString)
        {
            // todo
        }
            
    }
}

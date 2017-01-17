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
    public partial class TruthBattleEnemy : MotherForm
    {
        public enum CriticalType
        {
            None,
            Random,
            Absolute,
        }

        bool ActionInstantMode = false;
        string instantActionCommandString = String.Empty;

        bool nowStackAnimation = false;
        int nowStackAnimationCounter = 0;

        bool nowAnimationMatrixTalk = false;
        int nowAnimationMatrixTalkCounter = 0;

        bool nowAnimationSandGlass = false;
        int nowAnimationSandGlassCounter = 0;

        bool nowAnimationFinal = false;
        int nowAnimationFinalCounter = 0;

        bool nowExecutionWarpGate = false;
        int nowExecutionWarpGateCounter = 0;
        MainCharacter nowExecutionWarpGatePlayer = null;
        MainCharacter nowExecutionWarpGateTarget = null;

        bool nowAnimation = false;
        int nowAnimationCounter = 0;
        List<MainCharacter> nowAnimationTarget = new List<MainCharacter>();
        List<int> nowAnimationDamage = new List<int>();
        List<Color> nowAnimationColor = new List<Color>();
        List<bool> nowAnimationAvoid = new List<bool>();
        List<String> nowAnimationCustomString = new List<string>();
        List<int> nowAnimationInterval = new List<int>();
        List<bool> nowAnimationCritical = new List<bool>();
        List<Text> nowAnimationText = new List<Text>();
        bool NowSelectingTarget = false;
        MainCharacter currentTargetedPlayer = null;
        bool RunAwayFlag = false; // 戦闘逃げるで終了するためのフラグ
        bool BattleEndFlag = false; // 戦闘終了条件を満たし、バトル終了するためのフラグ
        bool tempStopFlag = false; // [戦闘停止」ボタンやESCキーで、戦闘を一旦停止させたい時に使うフラグ
        bool cannotRunAway = false; // 戦闘から逃げられるかどうかを示すフラグ
        bool NowStackInTheCommand = false; // スタックインザコマンドで一旦停止させたい時に使うフラグ
        List<MainCharacter> stackActivePlayer = new List<MainCharacter>();
        List<int> cumulativeCounter = new List<int>(); // スタックインザコマンドゲージ進行値
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
        private Sprite[] imageSandglass;
        
        // GUI
        public Text debug1;
        public Text debug2;
        public Text debug3;
        public Text debug4;
        public Text debug5;
        public Text debug21;
        public Text debug22;
        public Text debug23;
        public Text debug24;
        public Text debug25;
        public Text debug26;
        public Text debug27;
        public Text debug31;
        public Text debug41;
        public Text debug42;
        public Text debug43;
        public Text debug51;
        public Text debugB1;
        public Text debugB2;
        public Text debugB3;
        public Text debugB4;
        public Text debugB5;
        public Text debugB21;
        public Text debugB22;
        public Text debugB23;
        public Text debugB24;
        public Text debugB25;
        public Text debugB26;
        public Text debugB27;
        public Text debugB31;
        public Text debugB41;
        public Text debugB42;
        public Text debugB43;
        public Text debugB51;
        public GameObject groupMatrixDragonTalk;
        public Image back_MatrixDragonTalk;
        public Text MatrixDragonTalkText;
        public Image back_TutorialMessage;
        public Text TutorialMessageText;
        public Image back_Sandglass;
        public Text SandGlassText;
        public Image SandGlassImage;
        public Text TimeStopText;
        public Image back_FinalBattle;
        public Text FinalBattleText;
        public Text TimeSpeedLabel;
        public GameObject groupChooseCommand;
        public Camera cam;
        public TruthImage[] FieldBuff;
        public GameObject groupFieldBuff;
        public GameObject groupParentBackpack;
        public GameObject[] back_Backpack;
        public Text[] backpack;
        public Text[] backpackStack;
        public Image[] backpackIcon;

        public GameObject popupInfo;
        public Text CurrentInfo;
        public Text BattleStart;
        public Button[] ActionButton1;
        public Button[] ActionButton2;
        public Button[] ActionButton3;
        public Button[] ActionButtonE1;
        public Button[] ActionButtonE2;
        public Button[] ActionButtonE3;
        public Text txtBattleMessage;
        public GameObject back_labelBattleTurn;
        public Text labelBattleTurn;
        public Image pbSandglass;
        public Text lblTimerCount;
        public GameObject BattleMenuPanel;
        public Button BattleSettingButton;
        public Button UseItemButton;
        public Button RunAwayButton;

        public GameObject InstantModePanel;
        public Image UseItemGauge;
        public Text UseItemText;
        public GameObject groupPlayer1;
        public GameObject groupPlayer2;
        public GameObject groupPlayer3;
        public GameObject groupEnemy1;
        public GameObject groupEnemy2;
        public GameObject groupEnemy3;

        public GameObject back_nowStackAnimationName;
        public Text nowStackAnimationNameText;
        public GameObject back_nowStackAnimationBar;
        public Text nowStackAnimationBarText;
        public GameObject[] back_StackInTheCommandName;
        public Text[] StackInTheCommandNameText;
        public GameObject[] back_StackInTheCommandBar;
        public Text[] StackInTheCommandBarText;
        private int StackNumber = -1;

        public Image player1Arrow;
        public Text playerActionLabel1;
        public GameObject player1MainObjectBack;
        public Button player1MainObject;
        public Text player1Name;
        public Text player1FullName;
        public Text player1Life;
        public Image player1LifeMeter;
        public Text player1Mana;
        public Image player1ManaMeter;
        public Text player1Skill;
        public Image player1SkillMeter;
        public Text player1Instant;
        public Image player1InstantMeter;
        public Text player1SpecialInstant;
        public Image player1SpecialInstantMeter;
        public GameObject player1DamagePanel;
        public Text player1Damage;
        public Text player1Critical;
        public Text[] KeyNum1;
        public Image[] IsSorcery1;
        public GameObject player1Panel;
        public GameObject player1ActionPanel;

        public Image player2Arrow;
        public Text playerActionLabel2;
        public GameObject player2MainObjectBack;
        public Button player2MainObject;
        public Text player2Name;
        public Text player2FullName;
        public Text player2Life;
        public Image player2LifeMeter;
        public Text player2Mana;
        public Image player2ManaMeter;
        public Text player2Skill;
        public Image player2SkillMeter;
        public Text player2Instant;
        public Image player2InstantMeter;
        public Text player2SpecialInstant;
        public Image player2SpecialInstantMeter;
        public GameObject player2DamagePanel;
        public Text player2Damage;
        public Text player2Critical;
        public Text[] KeyNum2;
        public Image[] IsSorcery2;
        public GameObject player2Panel;
        public GameObject player2ActionPanel;

        public Image player3Arrow;
        public Text playerActionLabel3;
        public GameObject player3MainObjectBack;
        public Button player3MainObject;
        public Text player3Name;
        public Text player3FullName;
        public Text player3Life;
        public Image player3LifeMeter;
        public Text player3Mana;
        public Image player3ManaMeter;
        public Text player3Skill;
        public Image player3SkillMeter;
        public Text player3Instant;
        public Image player3InstantMeter;
        public Text player3SpecialInstant;
        public Image player3SpecialInstantMeter;
        public GameObject player3DamagePanel;
        public Text player3Damage;
        public Text player3Critical;
        public Text[] KeyNum3;
        public Image[] IsSorcery3;
        public GameObject player3Panel;
        public GameObject player3ActionPanel;

        public Image enemy1Arrow;
        public Text enemyActionLabel1;
        public GameObject enemy1MainObjectBack;
        public Button enemy1MainObject;
        public Text enemy1Name;
        public Text enemy1FullName;
        public Text enemy1Life;
        public Image enemy1LifeMeter;
        public Text enemy1Mana;
        public Image enemy1ManaMeter;
        public Text enemy1Skill;
        public Image enemy1SkillMeter;
        public Text enemy1Instant;
        public Image enemy1InstantMeter;
        public Text enemy1SpecialInstant;
        public Image enemy1SpecialInstantMeter;
        public GameObject enemy1DamagePanel;
        public Text enemy1Damage;
        public Text enemy1Critical;
        public Text[] KeyNumE1;
        public Image[] IsSorceryE1;

        public Image enemy2Arrow;
        public Text enemyActionLabel2;
        public GameObject enemy2MainObjectBack;
        public Button enemy2MainObject;
        public Text enemy2Name;
        public Text enemy2FullName;
        public Text enemy2Life;
        public Image enemy2LifeMeter;
        public Text enemy2Mana;
        public Image enemy2ManaMeter;
        public Text enemy2Skill;
        public Image enemy2SkillMeter;
        public Text enemy2Instant;
        public Image enemy2InstantMeter;
        public Text enemy2SpecialInstant;
        public Image enemy2SpecialInstantMeter;
        public GameObject enemy2DamagePanel;
        public Text enemy2Damage;
        public Text enemy2Critical;
        public Text[] KeyNumE2;
        public Image[] IsSorceryE2;

        public Image enemy3Arrow;
        public Text enemyActionLabel3;
        public GameObject enemy3MainObjectBack;
        public Button enemy3MainObject;
        public Text enemy3Name;
        public Text enemy3FullName;
        public Text enemy3Life;
        public Image enemy3LifeMeter;
        public Text enemy3Mana;
        public Image enemy3ManaMeter;
        public Text enemy3Skill;
        public Image enemy3SkillMeter;
        public Text enemy3Instant;
        public Image enemy3InstantMeter;
        public Text enemy3SpecialInstant;
        public Image enemy3SpecialInstantMeter;
        public GameObject enemy3DamagePanel;
        public Text enemy3Damage;
        public Text enemy3Critical;
        public Text[] KeyNumE3;
        public Image[] IsSorceryE3;

        public GameObject BuffPanel1;
        public GameObject BuffPanel2;
        public GameObject BuffPanel3;
        public GameObject PanelBuffEnemy1;
        public GameObject PanelBuffEnemy2;
        public GameObject PanelBuffEnemy3;
        public GameObject treasurePanel;
        public Image treasureIcon;
        public Text treasureText;
        public GameObject ExpGoldPanel;
        public Text ExpGoldText;

        // internal
        int BattleTimeCounter = Database.BASE_TIMER_BAR_LENGTH;
        int BattleTurnCount = 0;

        bool gameStart = false;
        bool execBattleEndPhase = false; // BattleEndPhaseが２重コールされるのを防ぐ
        bool endBattleForMatrixDragonEnd = false; // 支配竜会話終了時に戦闘終了させるフラグ

        TruthEnemyCharacter ec1;
        TruthEnemyCharacter ec2;
        TruthEnemyCharacter ec3;

        MainCharacter currentPlayer;

        public bool firstAction = false;

        int activatePlayerNumber = 0;
        List<MainCharacter> ActiveList = new List<MainCharacter>();
        private static System.Random rand = new System.Random(DateTime.Now.Millisecond * System.Environment.TickCount);


        int MAX_ITEM_GAUGE = 1000;
        int currentItemGauge = 0;

        bool StayOn_StanceOfFlow = false;
        bool BreakOn_StanceOfFlow = false;

        string ChooseCommand = string.Empty;

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            Texture2D textureSandGlass = Resources.Load<Texture2D>("SandGlassIcon");
            this.imageSandglass = new Sprite[8];
            int BASE_SIZE_X = 152;
            int BASE_SIZE_Y = 211;
            for (int locX = 0; locX < Database.TIMER_ICON_NUM; locX++)
            {
                this.imageSandglass[locX] = Sprite.Create(textureSandGlass, new Rect(BASE_SIZE_X * locX, 0, BASE_SIZE_X, BASE_SIZE_Y), new Vector2(0, 0));
            }
            this.pbSandglass.sprite = this.imageSandglass[0];

            if (GroundOne.TutorialMode)
            {
                if (GroundOne.TutorialLevel == 1)
                {
                    TutorialMessageText.text  = "　　　右下の【戦闘開始】ボタンを押してください。戦闘中は各プレイヤーのゲージが右へ進みます。\r\n";
                    TutorialMessageText.text += "　　　プレイヤーの位置が右端に来た時、メイン行動が行われます。\r\n";
                    TutorialMessageText.text += "　　　戦闘を一旦停止させたい場合は、右下の【戦闘停止】ボタンを押してください。\r\n";
                    TutorialMessageText.text += "　　　また、戦闘速度は右下のスライドバーで調整できます。";
                }
                else if (GroundOne.TutorialLevel == 2)
                {
                    TutorialMessageText.text  = "　　　戦闘コマンドに「攻撃」と「防御」の２つがあります。\r\n";
                    TutorialMessageText.text += "　　　「防御」を選択することでダメージを軽減できます。\r\n";
                    TutorialMessageText.text += "　　　「攻撃」を選択した後、敵本体をタップする事で、メイン行動時に攻撃が繰り出されます。\r\n";
                    TutorialMessageText.text += "　　　ただし「防御」選択状態では、メイン行動に攻撃が繰り出されなくなります。\r\n";
                    TutorialMessageText.text += "　　　タイミングよく「攻撃」を選択するようにしましょう。";
                }
                else if (GroundOne.TutorialLevel == 3)
                {
                    TutorialMessageText.text  = "　　　パーティ戦では戦闘コマンドを上手く組み立てる必要があります。\r\n";
                    TutorialMessageText.text += "　　　１番目のアインは「防御」か「フレッシュ・ヒール」を選択、\r\n";
                    TutorialMessageText.text += "　　　２番目のラナは「アイス・ニードル」を選択しておくと効率よく戦えます。\r\n";
                    TutorialMessageText.text += "　　　「フレッシュ・ヒール」の選択対象はアインかラナを選択してください。\r\n";
                    TutorialMessageText.text += "　　　「アイス・ニードル」は敵を選択してください。";
                }
                else if (GroundOne.TutorialLevel == 4)
                {
                    TutorialMessageText.text  = "　　　戦闘コマンドは、インスタントタイミングで使えるものがあります。\r\n";
                    TutorialMessageText.text += "　　　戦闘コマンドの下のインスタントゲージを確認し、ゲージが溜まるのを待ちます。\r\n";
                    TutorialMessageText.text += "　　　ゲージが溜まったら、左下のキャラクターエリアをプッシュした状態にしてください。\r\n";
                    TutorialMessageText.text += "　　　その状態で「プロテクション」などをタップし、アインを選択します。\r\n";
                    TutorialMessageText.text += "　　　メイン行動ではないタイミングで即座にアクションを行うことが出来るようになります。\r\n";
                }
                else if (GroundOne.TutorialLevel == 5)
                {
                    TutorialMessageText.text = "　　　[ DUEL ]戦は、通常戦闘と比べいくつか違いがあります。ここではその違いを把握しておきましょう。\r\n";
                    TutorialMessageText.text += "　　　・各プレイヤーのゲージバーは、必ず一番初め（左部）から開始となります。\r\n";
                    TutorialMessageText.text += "　　　・DUEL開始後は【一時停止】が選べなくなり、コマンド変更中においても戦闘が進行し続けます。\r\n";
                    TutorialMessageText.text += "　　　・インスタント行動は、＜行動スタック＞として画面中央に入ります。\r\n";
                    TutorialMessageText.text += "　　　・＜行動スタック＞が積まれている間、更に＜行動スタック＞を乗せる事ができます。\r\n";
                    TutorialMessageText.text += "　　　・＜行動スタック＞ゲージが完了すると、実際の行動が行われるようになります。\r\n";
                }
                back_TutorialMessage.gameObject.SetActive(true);
            }

            pbBuffPlayer1 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer2 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer3 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy1 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy2 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy3 = new TruthImage[Database.BUFF_NUM];
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                SetupBuff(ref pbBuffPlayer1, BuffPanel1, ii);
                SetupBuff(ref pbBuffPlayer2, BuffPanel2, ii);
                SetupBuff(ref pbBuffPlayer3, BuffPanel3, ii);
                SetupBuff(ref pbBuffEnemy1, PanelBuffEnemy1, ii);
                SetupBuff(ref pbBuffEnemy2, PanelBuffEnemy2, ii);
                SetupBuff(ref pbBuffEnemy3, PanelBuffEnemy3, ii);
            }

            GroundOne.MC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.MC.CurrentInstantPoint = 0;
            GroundOne.MC.MainFaceArrow = this.player1Arrow;
            GroundOne.MC.MainObjectButton = this.player1MainObject;
            GroundOne.MC.ActionLabel = this.playerActionLabel1;
            GroundOne.MC.labelName = this.player1Name;
            GroundOne.MC.labelCurrentLifePoint = this.player1Life;
            GroundOne.MC.meterCurrentLifePoint = this.player1LifeMeter;
            GroundOne.MC.labelCurrentManaPoint = this.player1Mana;
            GroundOne.MC.meterCurrentManaPoint = this.player1ManaMeter;
            GroundOne.MC.labelCurrentSkillPoint = this.player1Skill;
            GroundOne.MC.meterCurrentSkillPoint = this.player1SkillMeter;
            GroundOne.MC.labelCurrentInstantPoint = this.player1Instant;
            GroundOne.MC.meterCurrentInstantPoint = this.player1InstantMeter;
            GroundOne.MC.DamagePanel = this.player1DamagePanel;
            GroundOne.MC.DamageLabel = this.player1Damage;
            GroundOne.MC.CriticalLabel = this.player1Critical;
            GroundOne.MC.ActionButtonList.AddRange(this.ActionButton1);

            GroundOne.SC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.SC.CurrentInstantPoint = 0;
            GroundOne.SC.MainFaceArrow = this.player2Arrow;
            GroundOne.SC.MainObjectButton = this.player2MainObject;
            GroundOne.SC.ActionLabel = this.playerActionLabel2;
            GroundOne.SC.labelName = this.player2Name;
            GroundOne.SC.labelCurrentLifePoint = this.player2Life;
            GroundOne.SC.meterCurrentLifePoint = this.player2LifeMeter;
            GroundOne.SC.labelCurrentManaPoint = this.player2Mana;
            GroundOne.SC.meterCurrentManaPoint = this.player2ManaMeter;
            GroundOne.SC.labelCurrentSkillPoint = this.player2Skill;
            GroundOne.SC.meterCurrentSkillPoint = this.player2SkillMeter;
            GroundOne.SC.labelCurrentInstantPoint = this.player2Instant;
            GroundOne.SC.meterCurrentInstantPoint = this.player2InstantMeter;
            GroundOne.SC.DamagePanel = this.player2DamagePanel;
            GroundOne.SC.DamageLabel = this.player2Damage;
            GroundOne.SC.CriticalLabel = this.player2Critical;
            GroundOne.SC.ActionButtonList.AddRange(this.ActionButton2);

            GroundOne.TC.CurrentCommand = Database.ATTACK_EN;
            GroundOne.TC.CurrentInstantPoint = 0;
            GroundOne.TC.MainFaceArrow = this.player3Arrow;
            GroundOne.TC.MainObjectButton = this.player3MainObject;
            GroundOne.TC.ActionLabel = this.playerActionLabel3;
            GroundOne.TC.labelName = this.player3Name;
            GroundOne.TC.labelCurrentLifePoint = this.player3Life;
            GroundOne.TC.meterCurrentLifePoint = this.player3LifeMeter;
            GroundOne.TC.labelCurrentManaPoint = this.player3Mana;
            GroundOne.TC.meterCurrentManaPoint = this.player3ManaMeter;
            GroundOne.TC.labelCurrentSkillPoint = this.player3Skill;
            GroundOne.TC.meterCurrentSkillPoint = this.player3SkillMeter;
            GroundOne.TC.labelCurrentInstantPoint = this.player3Instant;
            GroundOne.TC.meterCurrentInstantPoint = this.player3InstantMeter;
            GroundOne.TC.DamagePanel = this.player3DamagePanel;
            GroundOne.TC.DamageLabel = this.player3Damage;
            GroundOne.TC.CriticalLabel = player3Critical;
            GroundOne.TC.ActionButtonList.AddRange(this.ActionButton3);

            GameObject baseObj1 = new GameObject("enemyObj1");
            ec1 = baseObj1.AddComponent<TruthEnemyCharacter>();
            ec1.Initialize(GroundOne.enemyName1);
            ec1.CurrentCommand = Database.ATTACK_EN;
            ec1.CurrentInstantPoint = 0;
            ec1.MainFaceArrow = enemy1Arrow;
            ec1.MainObjectButton = enemy1MainObject;
            ec1.ActionLabel = enemyActionLabel1;
            ec1.labelName = enemy1Name;
            ec1.labelCurrentLifePoint = enemy1Life;
            ec1.meterCurrentLifePoint = enemy1LifeMeter;
            ec1.labelCurrentManaPoint = null;
            ec1.meterCurrentManaPoint = null;
            ec1.labelCurrentSkillPoint = null;
            ec1.meterCurrentSkillPoint = null;
            ec1.labelCurrentInstantPoint = enemy1Instant;
            ec1.meterCurrentInstantPoint = enemy1InstantMeter;
            ec1.labelCurrentSpecialInstant = enemy1SpecialInstant;
            ec1.meterCurrentSpecialInstant = enemy1SpecialInstantMeter;
            ec1.DamagePanel = enemy1DamagePanel;
            ec1.DamageLabel = enemy1Damage;
            ec1.CriticalLabel = enemy1Critical;

            GameObject baseObj2 = new GameObject("enemyObj2");
            ec2 = baseObj2.AddComponent<TruthEnemyCharacter>();
            ec2.Initialize(GroundOne.enemyName2);
            ec2.CurrentCommand = Database.ATTACK_EN;
            ec2.CurrentInstantPoint = 0;
            ec2.MainFaceArrow = enemy2Arrow;
            ec2.MainObjectButton = enemy2MainObject;
            ec2.ActionLabel = enemyActionLabel2;
            ec2.labelName = enemy2Name;
            ec2.labelCurrentLifePoint = enemy2Life;
            ec2.meterCurrentLifePoint = enemy2LifeMeter;
            ec2.labelCurrentManaPoint = null;
            ec2.meterCurrentManaPoint = null;
            ec2.labelCurrentSkillPoint = null;
            ec2.meterCurrentSkillPoint = null;
            ec2.labelCurrentInstantPoint = enemy2Instant;
            ec2.meterCurrentInstantPoint = enemy2InstantMeter;
            ec2.DamagePanel = enemy2DamagePanel;
            ec2.DamageLabel = enemy2Damage;
            ec2.CriticalLabel = enemy2Critical;

            GameObject baseObj3 = new GameObject("enemyObj3");
            ec3 = baseObj3.AddComponent<TruthEnemyCharacter>();
            ec3.Initialize(GroundOne.enemyName3);
            ec3.CurrentCommand = Database.PROTECTION;
            ec3.CurrentInstantPoint = 0;
            ec3.MainFaceArrow = enemy3Arrow;
            ec3.MainObjectButton = enemy3MainObject;
            ec3.ActionLabel = enemyActionLabel3;
            ec3.labelName = enemy3Name;
            ec3.labelCurrentLifePoint = enemy3Life;
            ec3.meterCurrentLifePoint = enemy3LifeMeter;
            ec3.labelCurrentManaPoint = null;
            ec3.meterCurrentManaPoint = null;
            ec3.labelCurrentSkillPoint = null;
            ec3.meterCurrentSkillPoint = null;
            ec3.labelCurrentInstantPoint = enemy3Instant;
            ec3.meterCurrentInstantPoint = enemy3InstantMeter;
            ec3.DamagePanel = enemy3DamagePanel;
            ec3.DamageLabel = enemy3Damage;
            ec3.CriticalLabel = enemy3Critical;

            SetupBuffElement(GroundOne.MC, ref pbBuffPlayer1);
            SetupBuffElement(GroundOne.SC, ref pbBuffPlayer2);
            SetupBuffElement(GroundOne.TC, ref pbBuffPlayer3);
            SetupBuffElement(ec1, ref pbBuffEnemy1);
            SetupBuffElement(ec2, ref pbBuffEnemy2);
            SetupBuffElement(ec3, ref pbBuffEnemy3);

            if (GroundOne.WE.AvailableFirstCharacter == false)
            {
                groupPlayer1.SetActive(false);
            }
            else
            {
                groupPlayer1.SetActive(true);
                ActivateSomeCharacter(GroundOne.MC, ec1, player1Name, player1FullName, player1Life, player1LifeMeter, player1Mana, player1ManaMeter, player1Skill, player1SkillMeter, player1Instant, player1InstantMeter, player1SpecialInstant, player1SpecialInstantMeter, ActionButton1, playerActionLabel1, BuffPanel1, player1Panel, player1ActionPanel, player1MainObjectBack, player1MainObject, GroundOne.MC.PlayerBattleTargetColor1, player1Arrow, null, null, player1Damage, player1Critical, ref pbBuffPlayer1, KeyNum1, IsSorcery1);
            }
            if (GroundOne.WE.AvailableSecondCharacter == false || GroundOne.DuelMode)
            {
                groupPlayer2.SetActive(false);
            }
            else
            {
                groupPlayer2.SetActive(true);
                ActivateSomeCharacter(GroundOne.SC, ec1, player2Name, player2FullName, player2Life, player2LifeMeter, player2Mana, player2ManaMeter, player2Skill, player2SkillMeter, player2Instant, player2InstantMeter, player2SpecialInstant, player2SpecialInstantMeter, ActionButton2, playerActionLabel2, BuffPanel2, player2Panel, player2ActionPanel, player2MainObjectBack, player2MainObject, GroundOne.SC.PlayerBattleTargetColor1, player2Arrow, null, null, player2Damage, player2Critical, ref pbBuffPlayer2, KeyNum2, IsSorcery2);
            }

            if (GroundOne.WE.AvailableThirdCharacter == false || GroundOne.DuelMode)
            {
                groupPlayer3.SetActive(false);
            }
            else
            {
                groupPlayer3.SetActive(true);
                ActivateSomeCharacter(GroundOne.TC, ec1, player3Name, player3FullName, player3Life, player3LifeMeter, player3Mana, player3ManaMeter, player3Skill, player3SkillMeter, player3Instant, player3InstantMeter, player3SpecialInstant, player3SpecialInstantMeter, ActionButton3, playerActionLabel3, BuffPanel3, player3Panel, player3ActionPanel, player3MainObjectBack, player3MainObject, GroundOne.TC.PlayerBattleTargetColor1, player3Arrow, null, null, player3Damage, player3Critical, ref pbBuffPlayer3, KeyNum3, IsSorcery3);
            }

            if (GroundOne.enemyName1 == String.Empty)
            {
                groupEnemy1.SetActive(false);
                ec1 = null;
            }
            else
            {
                groupEnemy1.SetActive(true);
                ActivateSomeCharacter(ec1, GroundOne.MC, enemy1Name, enemy1FullName, enemy1Life, enemy1LifeMeter, enemy1Mana, enemy1ManaMeter, enemy1Skill, enemy1SkillMeter, enemy1Instant, enemy1InstantMeter, enemy1SpecialInstant, enemy1SpecialInstantMeter, ActionButtonE1, enemyActionLabel1, PanelBuffEnemy1, null, null, enemy1MainObjectBack, enemy1MainObject, new Color(87.0f / 255.0f, 0.0f, 16.0f / 255.0f), enemy1Arrow, null, null, enemy1Damage, enemy1Critical, ref pbBuffEnemy1, KeyNumE1, IsSorceryE1);
            }

            if (GroundOne.enemyName2 == String.Empty || GroundOne.DuelMode)
            {
                groupEnemy2.SetActive(false);
                ec2 = null;
            }
            else
            {
                groupEnemy2.SetActive(true);
                ActivateSomeCharacter(ec2, GroundOne.MC, enemy2Name, enemy2FullName, enemy2Life, enemy2LifeMeter, enemy2Mana, enemy2ManaMeter, enemy2Skill, enemy2SkillMeter, enemy2Instant, enemy2InstantMeter, enemy2SpecialInstant, enemy2SpecialInstantMeter, ActionButtonE2, enemyActionLabel2, PanelBuffEnemy2, null, null, enemy2MainObjectBack, enemy2MainObject, new Color(108.0f / 255.0f, 118.0f / 255.0f, 0.0f), enemy2Arrow, null, null, enemy2Damage, enemy2Critical, ref pbBuffEnemy2, KeyNumE2, IsSorceryE2);
            }

            if (GroundOne.enemyName3 == String.Empty || GroundOne.DuelMode)
            {
                groupEnemy3.SetActive(false);
                ec3 = null;
            }
            else
            {
                groupEnemy3.SetActive(true);
                ActivateSomeCharacter(ec3, GroundOne.MC, enemy3Name, enemy3FullName, enemy3Life, enemy3LifeMeter, enemy3Mana, enemy3ManaMeter, enemy3Skill, enemy3SkillMeter, enemy3Instant, enemy3InstantMeter, enemy2SpecialInstant, enemy3SpecialInstantMeter, ActionButtonE3, enemyActionLabel3, PanelBuffEnemy3, null, null, enemy3MainObjectBack, enemy3MainObject, new Color(69.0f / 255.0f, 99.0f / 255.0f, 129.0f / 255.0f), enemy3Arrow, null, null, enemy3Damage, enemy3Critical, ref pbBuffEnemy3, KeyNumE3, IsSorceryE3);
            }

            for (int ii = 0; ii < this.ActiveList.Count; ii++)
            {
                UpdateLife(this.ActiveList[ii]);
                UpdateMana(this.ActiveList[ii]);
                UpdateSkillPoint(this.ActiveList[ii]);
                float widthScale = (float)(Screen.width) / (float)(Database.BASE_TIMER_BAR_LENGTH);
                Vector3 current = ActiveList[ii].MainFaceArrow.transform.position;
                ActiveList[ii].MainFaceArrow.transform.position = new Vector3((float)ActiveList[ii].BattleBarPos * widthScale - ActiveList[ii].MainFaceArrow.rectTransform.sizeDelta.x/2.0f, current.y, current.z);
            }

            this.currentPlayer = GroundOne.MC;
            Method.UpdateBackPackLabel(this.currentPlayer, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);
            this.currentItemGauge = 0;
            UpdateUseItemGauge();
            //tapFirstChara ();

            if (ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                groupFieldBuff.SetActive(true);
            }
            else
            {
                groupFieldBuff.SetActive(false);
            }

            if (ec1 != null)
            {
                // ヴェルゼ最終戦闘２
                if (ec1.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM23, Database.BGM23LoopBegin);
                }
                // ヴェルゼ最終戦闘
                else if (ec1.FirstName == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM21, Database.BGM21LoopBegin);
                }
                // DUEL最終戦
                else if (ec1.FirstName == Database.ENEMY_LAST_RANA_AMILIA ||
                         ec1.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                         ec1.FirstName == Database.ENEMY_LAST_OL_LANDIS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM21, Database.BGM21LoopBegin);
                }
                // ボス
                else if ((ec1.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                    (ec1.FirstName == Database.ENEMY_BRILLIANT_SEA_PRINCE) ||
                    (ec1.FirstName == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN) ||
                    (ec1.FirstName == Database.ENEMY_JELLY_EYE_BRIGHT_RED) ||
                    (ec1.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE) ||
                    (ec1.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                    (ec1.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA) ||
                    (ec1.FirstName == Database.ENEMY_SEA_STAR_ORIGIN_KING) ||
                    (ec1.FirstName == Database.ENEMY_BOSS_LEVIATHAN) ||
                    (ec1.FirstName == Database.ENEMY_BOSS_HOWLING_SEIZER) ||
                    (ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1) ||
                    (ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_2) ||
                    (ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                    )
                {
                    GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);
                }
                // 初期DUELオル・ランディス
                else if (ec1.FirstName == Database.DUEL_OL_LANDIS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM17, Database.BGM17LoopBegin);
                }
                // 支配竜達の呼び声
                else if (ec1.Rare == TruthEnemyCharacter.RareString.Purple)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM18, Database.BGM18LoopBegin);
                }
                // 最終ボス：支配竜
                else if (ec1.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM05, Database.BGM05LoopBegin);
                }
                // 通常バトル
                else
                {
                    GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
                }
            }
            // 万が一、敵の情報が取得できない場合、通常バトル
            else
            {
                GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
            }
        }

        public override void SceneBack()
        {
            base.SceneBack();

            UpdateBattleCommandSetting(GroundOne.MC, ActionButton1, IsSorcery1);
            if (GroundOne.WE.AvailableSecondCharacter)
            {
                UpdateBattleCommandSetting(GroundOne.SC, ActionButton2, IsSorcery2);
            }
            if (GroundOne.WE.AvailableThirdCharacter)
            {
                UpdateBattleCommandSetting(GroundOne.TC, ActionButton3, IsSorcery3);
            }
        }

        public void ExecChooseCommand(Text sender)
        {
            // after (command listを戦闘画面開始に表示し、特定のコマンドを選択させる）
            //string chooseCommand = String.Empty;
            //    chooseCommand = tcc.ChooseCommand;
            //}
            //ActiveList[ii].Accessory.ImprintCommand = chooseCommand;
        }

        bool isEscDown = false;
        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            // debug
            //if (GroundOne.MC != null)
            //{
            //    this.debug1.text = GroundOne.MC.TotalStrength.ToString();
            //    this.debug2.text = GroundOne.MC.TotalAgility.ToString();
            //    this.debug3.text = GroundOne.MC.TotalIntelligence.ToString();
            //    this.debug4.text = GroundOne.MC.TotalStamina.ToString();
            //    this.debug5.text = GroundOne.MC.TotalMind.ToString();
            //    this.debug21.text = PrimaryLogic.PhysicalAttackValue(GroundOne.MC, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false).ToString("F2");
            //    this.debug22.text = PrimaryLogic.PhysicalDefenseValue(GroundOne.MC, PrimaryLogic.NeedType.Min, false).ToString("F2");
            //    this.debug23.text = PrimaryLogic.MagicAttackValue(GroundOne.MC, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, PrimaryLogic.SpellSkillType.Standard, false, false).ToString("F2");
            //    this.debug24.text = PrimaryLogic.MagicDefenseValue(GroundOne.MC, PrimaryLogic.NeedType.Min, false).ToString("F2");
            //    this.debug25.text = PrimaryLogic.BattleSpeedValue(GroundOne.MC, false).ToString("F2");
            //    this.debug26.text = PrimaryLogic.BattleResponseValue(GroundOne.MC, false).ToString("F2");
            //    this.debug27.text = PrimaryLogic.PotentialValue(GroundOne.MC, false).ToString("F2");
            //    this.debug31.text = GroundOne.MC.Target.FirstName;
            //    //this.debug41.text = GroundOne.MC.MaxLife.ToString();
            //    //this.debug42.text = GroundOne.MC.MaxMana.ToString();
            //    //this.debug43.text = GroundOne.MC.MaxInstantPoint.ToString();
            //    this.debug41.text = GroundOne.MC.CurrentLife.ToString();
            //    this.debug42.text = GroundOne.MC.CurrentMana.ToString();
            //    this.debug43.text = GroundOne.MC.CurrentSkillPoint.ToString();
            //    this.debug51.text = GroundOne.MC.Gold.ToString();
            //}
            //if (ec1 != null)
            //{
            //    this.debugB1.text = ec1.TotalStrength.ToString();
            //    this.debugB2.text = ec1.TotalAgility.ToString();
            //    this.debugB3.text = ec1.TotalIntelligence.ToString();
            //    this.debugB4.text = ec1.TotalStamina.ToString();
            //    this.debugB5.text = ec1.TotalMind.ToString();
            //    this.debugB21.text = PrimaryLogic.PhysicalAttackValue(ec1, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false).ToString("F2");
            //    this.debugB22.text = PrimaryLogic.PhysicalDefenseValue(ec1, PrimaryLogic.NeedType.Min, false).ToString("F2");
            //    this.debugB23.text = PrimaryLogic.MagicAttackValue(ec1, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, PrimaryLogic.SpellSkillType.Standard, false, false).ToString("F2");
            //    this.debugB24.text = PrimaryLogic.MagicDefenseValue(ec1, PrimaryLogic.NeedType.Min, false).ToString("F2");
            //    this.debugB25.text = PrimaryLogic.BattleSpeedValue(ec1, false).ToString("F2");
            //    this.debugB26.text = PrimaryLogic.BattleResponseValue(ec1, false).ToString("F2");
            //    this.debugB27.text = PrimaryLogic.PotentialValue(ec1, false).ToString("F2");
            //    this.debugB31.text = ec1.Target.FirstName;
            //    //this.debugB41.text = ec1.MaxLife.ToString();
            //    //this.debugB42.text = ec1.MaxMana.ToString();
            //    //this.debugB43.text = ec1.MaxInstantPoint.ToString();
            //    this.debugB41.text = ec1.CurrentLife.ToString();
            //    this.debugB42.text = ec1.CurrentMana.ToString();
            //    this.debugB43.text = ec1.CurrentSkillPoint.ToString();
            //    this.debugB51.text = ec1.Gold.ToString();
            //}

            System.Threading.Thread.Sleep(Database.BATTLE_CORE_SLEEP);
            #region "キー制御"
            bool detectShift = false;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                detectShift = true;
            }

            KeyCode[] keyCodeData1 = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
            for (int ii = 0; ii < keyCodeData1.Length; ii++)
            {
                if (Input.GetKeyDown(keyCodeData1[ii]) &&
                    (this.ActionButton1[ii].gameObject.activeInHierarchy))
                {
                    ActionCommand(detectShift, GroundOne.MC, GroundOne.MC.BattleActionCommandList[ii]);
                }
            }

            KeyCode[] keyCodeData2 = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O };
            for (int ii = 0; ii < keyCodeData2.Length; ii++)
            {
                if (Input.GetKeyDown(keyCodeData2[ii]) &&
                    (this.ActionButton2[ii].gameObject.activeInHierarchy))
                {
                    ActionCommand(detectShift, GroundOne.SC, GroundOne.SC.BattleActionCommandList[ii]);
                }
            }

            KeyCode[] keyCodeData3 = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L };
            for (int ii = 0; ii < keyCodeData3.Length; ii++)
            {
                if (Input.GetKeyDown(keyCodeData3[ii]) &&
                    (this.ActionButton3[ii].gameObject.activeInHierarchy))
                {
                    ActionCommand(detectShift, GroundOne.TC, GroundOne.TC.BattleActionCommandList[ii]);
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
                        if (GroundOne.DuelMode == false)
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

            #region "進行停止"
            if (this.nowAnimationSandGlass)
            {
                ExecAnimationSandGlass();
                //Debug.Log("nowAnimationSandGlass is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimation)
            {
                ExecAnimation();
                //Debug.Log("nowAnimation is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowStackAnimation)
            {
                ExecStackAnimation();
                Debug.Log("nowStackAnimation is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimationMatrixTalk)
            {
                ExecAnimationMessageFadeOut();
                Debug.Log("nowAnimationMatrixTalk is true then return");
                return; // アニメーション表示中は停止させる。
            }
            if (this.nowAnimationFinal)
            {
                ExecAnimationFinalBattle();
                Debug.Log("nowAnimationFinal is true then return");
                return; // アニメーション表示中は停止させる。
            }

            if (this.nowExecutionWarpGate)
            {
                ExecPlayWarpGate();
                Debug.Log("nowExecutionWarpGate is true then return");
                return; // ワープゲート実行中は停止させる。
            }

            // バトル終了条件が満たされている場合、バトル終了とする。
            if (this.BattleEndFlag) 
            {
                Debug.Log("BattleEndFlag is true then return"); 
                BattleEndPhase(); 
            }

            if (UpdatePlayerDeadFlag()) { Debug.Log("UpdatePlayerDeadFlag is true then return"); return; }
            if (this.BattleEndFlag) { Debug.Log("endFlag is true then return"); return; } // 終了サインが出た場合、戦闘終了として待機する。
            if (this.gameStart == false) { return; } // 戦闘開始サインが無い状態では、待機する。
            if (this.endBattleForMatrixDragonEnd)
            {
                BattleEndPhase();
                return;
            } // 戦闘終了サインにより、戦闘を抜ける。
            #endregion

            #region "ゲージ位置"
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                UpdatePlayerMainFaceArrow(ActiveList[ii]);
            }
            #endregion

            CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag())
            {
                return; // パーティ死亡確認で戦闘を抜ける。
            }

            #region "タイムストップチェック"
            bool tempTimeStop = false;
            int tempTimeStopCounter = 0;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if ((ActiveList[ii].CurrentTimeStop > 0))
                {
                    ActiveList[ii].CurrentTimeStopValue--;
                    tempTimeStopCounter = ActiveList[ii].CurrentTimeStopValue;
                    if (ActiveList[ii].CurrentTimeStopValue <= 0)
                    {
                        ActiveList[ii].RemoveTimeStop();
                    }
                    else
                    {
                        this.NowTimeStop = true;
                        tempTimeStop = true;
                        break;
                    }
                }
            }

            if (tempTimeStop == false)
            {
                this.NowTimeStop = false;
            }
            if ((this.NowTimeStop == true) && (this.Background.GetComponent<Image>().color == Color.white))
            {
                this.Background.GetComponent<Image>().color = Color.black;
                this.labelBattleTurn.color = Color.white;
                this.TimeSpeedLabel.color = Color.white;
                this.lblTimerCount.color = Color.white;
                if (ec1.FirstName != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    this.TimeStopText.gameObject.SetActive(true);
                }
                for (int ii = 0; ii < ActiveList.Count; ii++)
                {
                    ActiveList[ii].labelName.color = Color.white;
                    ActiveList[ii].ActionLabel.color = Color.white;
                    ActiveList[ii].CriticalLabel.color = Color.white;
                    ActiveList[ii].DamageLabel.color = Color.white;
                    GoToTimeStopColor(ActiveList[ii]);
                    ActiveList[ii].BuffPanel.GetComponent<Image>().color = Color.black;
                }
            }
            if ((this.NowTimeStop == false) && (this.Background.GetComponent<Image>().color == Color.black))
            {
                ExecPhaseElement(MethodType.TimeStopEnd, null);
            }
            #endregion

            #region "戦闘一旦停止フラグ"
            if (this.tempStopFlag) { return; } // 「戦闘停止」ボタンやESCキーで、一旦停止させる。
            if (GroundOne.DuelMode == false) // DUELモードの時、選択肢の選択中は一旦停止しない。
            {
                if (this.NowSelectingTarget) { return; } // インスタント行動対象選択時、一旦停止させる。
            }
            if (this.NowStackInTheCommand) { return; } // スタックインザコマンド発動中は停止させる。
            #endregion

            #region "ターン砂時計"
            if (this.NowTimeStop == false)
            {
                this.BattleTimeCounter++; // メイン戦闘タイマーカウント更新
                // Bystander専用
                int currentTimerCount = this.BattleTimeCounter;
                if (BattleTurnCount != 0)
                {
                    double currentTime = (Database.BASE_TIMER_BAR_LENGTH - (double)currentTimerCount) / (Database.BASE_TIMER_BAR_LENGTH) * 5.0f;
                    lblTimerCount.text = currentTime.ToString("0.00");
                }
                for (int ii = 0; ii < Database.TIMER_ICON_NUM; ii++)
                {
                    if (Database.BASE_TIMER_DIV * ii <= this.BattleTimeCounter && this.BattleTimeCounter < Database.BASE_TIMER_DIV * (ii + 1))
                    {
                        pbSandglass.sprite = this.imageSandglass[ii];
                        break;
                    }
                }
            }
            else
            {
                this.TimeStopText.text = tempTimeStopCounter.ToString();
            }
            #endregion

            if (BattleTimeCounter >= Database.BASE_TIMER_BAR_LENGTH)
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
                if (this.NowTimeStop && ActiveList[ii].CurrentTimeStop <= 0 && ActiveList[ii].FirstName != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
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

            CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag())
            {
                return; // パーティ死亡確認で戦闘を抜ける。
            }
        }

        private void UpdatePlayerMainFaceArrow(MainCharacter player)
        {
            float widthScale = (float)(Screen.width) / (float)(Database.BASE_TIMER_BAR_LENGTH);
            Vector3 current = player.MainFaceArrow.transform.position;
            player.MainFaceArrow.transform.position = new Vector3((float)player.BattleBarPos * widthScale - player.MainFaceArrow.rectTransform.sizeDelta.x/2.0f, current.y, current.z);
        }


        void SelectPlayerArrow(MainCharacter player)
        {
            if (player.FirstName == Database.EIN_WOLENCE) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player1Arrow"); }
            else if (player.FirstName == Database.RANA_AMILIA) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player2Arrow"); ; }
            else if (player.FirstName == Database.OL_LANDIS) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player3Arrow"); ; }
            else if (player.FirstName == Database.VERZE_ARTIE) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player4Arrow"); ; }
            else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player5Arrow"); ; }
            else { player.MainFaceArrow.sprite = Resources.Load<Sprite>("Player1Arrow"); }
        }
        void SelectPlayerPanelColor(MainCharacter player)
        {
            if (player.ManaSkillPanel != null)
            {
                if (player.FirstName == Database.EIN_WOLENCE) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(0.0f / 255.0f, 183.0f / 255.0f, 239.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.RANA_AMILIA) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 196.0f / 255.0f, 251.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.OL_LANDIS) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 242.0f / 255.0f, 3.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.VERZE_ARTIE) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(120.0f / 255.0f, 120.0f / 255.0f, 120.0f / 255.0f, 100f / 255.0f); }
                else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.ManaSkillPanel.GetComponent<Image>().color = new Color(81.0f / 255.0f, 7.0f / 255.0f, 255.0f / 255.0f, 100f / 255.0f); }
            }
            if (player.meterCurrentInstantPoint != null)
            {
                if (player.FirstName == Database.EIN_WOLENCE) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(0.0f / 255.0f, 198.0f / 255.0f, 190.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.RANA_AMILIA) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(248.0f / 255.0f, 124.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.OL_LANDIS) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(249.0f / 255.0f, 243.0f / 255.0f, 4.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.VERZE_ARTIE) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 255.0f / 255.0f); }
                else if (player.FirstName == Database.SINIKIA_KAHLHANZ) { player.meterCurrentInstantPoint.GetComponent<Image>().color = new Color(124.0f / 255.0f, 4.0f / 255.0f, 233.0f / 255.0f, 255.0f / 255.0f); }
            }
        }

        void ActivateSomeCharacter(MainCharacter player, MainCharacter target,
            Text charaName, Text fullName, 
            Text life, Image lifeMeter, 
            Text mana, Image manaMeter,
            Text skill, Image skillMeter,
            Text instant, Image instantMeter,
            Text specialInstant, Image specialInstantMeter,
            Button[] actionButton,
            Text actionLabel,
            GameObject buffPanel, GameObject ManaSkillPanel, GameObject ActionPanel,
            GameObject mainObjectBack, Button mainObject, Color mainColor, Image mainFaceArrow, Image shadowFaceArrow2, Image shadowFaceArrow3,
            Text damageLabel, Text criticalLabel,
            ref TruthImage[] buffList,
            Text[] keyNum,
            Image[] sorceryMark
            )
        {
            player.RealTimeBattle = true;

            // 戦闘画面UIへの初期設定
            // MainCharacterクラス内容と戦闘画面UIの割り当て
            player.labelName = charaName;
            player.labelName.text = player.FirstName;
            if (fullName != null)
            {
                player.labelFullName = fullName;
                player.labelFullName.text = player.FullName;
            }

            player.labelCurrentLifePoint = life;
            player.meterCurrentLifePoint = lifeMeter;
            player.labelCurrentManaPoint = mana;
            player.meterCurrentManaPoint = manaMeter;
            player.labelCurrentSkillPoint = skill;
            player.meterCurrentSkillPoint = skillMeter;
            player.labelCurrentInstantPoint = instant;
            player.meterCurrentInstantPoint = instantMeter;
            player.labelCurrentSpecialInstant = specialInstant;
            player.meterCurrentSpecialInstant = specialInstantMeter;

            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            }

            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            }

            player.CurrentInstantPoint = 0; // 後編追加 // 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.text = player.CurrentInstantPoint.ToString() + " / " + player.MaxInstantPoint.ToString();
            }

            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
            }

            if (actionButton != null)
            {
                player.ActionButtonList.Clear();
                for (int ii = 0; ii < actionButton.Length; ii++)
                {
                    player.ActionButtonList.Add(actionButton[ii]);
                }
            }

            if (keyNum != null)
            {
                player.ActionKeyNum.Clear();
                for (int ii = 0; ii < keyNum.Length; ii++)
                {
                    player.ActionKeyNum.Add(keyNum[ii]);
                }              
            }

            if (sorceryMark != null)
            {
                player.IsSorceryMark.Clear();
                for (int ii = 0; ii < sorceryMark.Length; ii++)
                {
                    player.IsSorceryMark.Add(sorceryMark[ii]);
                }
            }

            player.TextBattleMessage = this.txtBattleMessage;

            player.ActionLabel = actionLabel;

            player.BuffPanel = buffPanel;
            player.BuffPanel.SetActive(true);
            player.ManaSkillPanel = ManaSkillPanel;
            player.ActionButtonPanel = ActionPanel;

            player.MainColor = mainColor;
            player.MainObjectBack = mainObjectBack;
            player.MainObjectButton = mainObject;
            if (mainObjectBack != null)
            {
                player.MainObjectBack.GetComponent<Image>().color = mainColor;
            }
            if (mainFaceArrow != null)
            {
                player.MainFaceArrow = mainFaceArrow;
            }
            player.MainFaceArrow = mainFaceArrow;
            if (player.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE) { player.ShadowFaceArrow2 = shadowFaceArrow2; player.ShadowFaceArrow3 = shadowFaceArrow3; } // 最終戦ヴェルゼのみ、分身の技を使う。

            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                SelectPlayerArrow(player);
            }
            SelectPlayerPanelColor(player);

            player.DamageLabel = damageLabel;
            player.CriticalLabel = criticalLabel;

            // 登録を反映
            player.BuffElement = buffList;

            // 各プレイヤーのターゲット選定
            player.Target = target;
            player.Target2 = player; // 味方選択はデフォルトでは自分自身としておく。
            if ((player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
            {
                player.Target2 = ec1;
            }

            // 各プレイヤーの初期行動を選定
            player.PA = MainCharacter.PlayerAction.NormalAttack;
            player.ReserveBattleCommand = Database.ATTACK_EN;
            PlayerActionSet(player);

            // 各プレイヤーの戦闘バーの位置
            if (GroundOne.DuelMode)
            {
                player.BattleBarPos = 0;
            }
            else
            {
                player.BattleBarPos = rand.Next(100, 400);
                if (player.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                {
                    player.BattleBarPos = ec1.BattleBarPos + 250;
                    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                    }
                }

                if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                {
                    player.BattleBarPos = ec1.BattleBarPos + 150;
                    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                    }
                }
                if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
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
                if (player.meterCurrentSkillPoint != null)
                {
                    player.meterCurrentSkillPoint.gameObject.SetActive(false);
                }
            }

            // 各プレイヤーの魔法開放制限
            if (!player.AvailableMana)
            {
                if (player.labelCurrentManaPoint != null)
                {
                    player.labelCurrentManaPoint.gameObject.SetActive(false);
                }
                if (player.meterCurrentManaPoint != null)
                {
                    player.meterCurrentManaPoint.gameObject.SetActive(false);
                }
            }

            // 各プレイヤーのインスタントコマンドの開放制限
            if (!GroundOne.WE.AvailableInstantCommand)
            {
                if (player.meterCurrentInstantPoint != null)
                {
                    player.meterCurrentInstantPoint.gameObject.SetActive(false);
                }
                if (player.labelCurrentInstantPoint != null)
                {
                    player.labelCurrentInstantPoint.gameObject.SetActive(false);
                }
            }
            if ((player.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                (player.FirstName == Database.ENEMY_BRILLIANT_SEA_PRINCE))
            {
                player.labelCurrentInstantPoint.gameObject.SetActive(true);
            }
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).UseStackCommand == false && GroundOne.DuelMode == false)
                {
                    if (player.meterCurrentInstantPoint != null)
                    {
                        player.meterCurrentInstantPoint.gameObject.SetActive(false);
                    }
                    if (player.labelCurrentInstantPoint != null)
                    {
                        player.labelCurrentInstantPoint.gameObject.SetActive(false);
                    }
                }
            }

            // 味方側、魔法・スキルをセットアップ
            // プレイヤースキル・魔法習得に応じて、アクションボタンを登録
            UpdateBattleCommandSetting(player, actionButton, sorceryMark);

            // ボス戦の場合、ネームラベルやBUFFの表示場所を変更します。
            #region "敵側、名前の色と各ＵＩポジションを再配置"
            if (GroundOne.DuelMode)
            {
                if (player == ec1 || player == ec2 || player == ec3)
                {
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(640, 170, 0);
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 50);
                    player.labelCurrentLifePoint.fontSize = 36;
                    player.labelCurrentLifePoint.alignment = TextAnchor.MiddleLeft;
                    player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(640, 220, 0);
                    player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(560, 45);
                    player.labelName.fontSize = 30;
                    player.labelName.alignment = TextAnchor.MiddleCenter;
                    player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(0, 24, 0);
                    player.CriticalLabel.fontSize = 26;
                    player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(0, -26, 0);
                    player.DamageLabel.fontSize = 22;
                    player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(130, 60, 0);
                    player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(635, 120, 0);
                    player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 30);
                    player.ActionLabel.fontSize = 20;
                    player.ActionLabel.alignment = TextAnchor.MiddleLeft;
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(80, 140, 0);
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(560, 10, 0);
                    player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(640, 70, 0);
                    player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 60);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(640, 25, 0);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 30);
                }
                else
                {
                    player.labelName.text = player.FullName;
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(-640, 170, 0);
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 50);
                    player.labelCurrentLifePoint.fontSize = 36;
                    player.labelCurrentLifePoint.alignment = TextAnchor.MiddleRight;
                    player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(-640, 220, 0);
                    player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(560, 45);
                    player.labelName.fontSize = 30;
                    player.labelName.alignment = TextAnchor.MiddleCenter;
                    player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(0, 24, 0);
                    player.CriticalLabel.fontSize = 26;
                    player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(0, -26, 0);
                    player.DamageLabel.fontSize = 22;
                    player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(-130, 60, 0);
                    player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(-635, 120, 0);
                    player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 30);
                    player.ActionLabel.fontSize = 20;
                    player.ActionLabel.alignment = TextAnchor.MiddleRight;
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(-80, 140, 0);
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(560, 10, 0);
                    player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(-640, 70, 0);
                    player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 60);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(-640, 25, 0);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 30);
                }
            }

            else if (player == ec1 || player == ec2 || player == ec3)
            {
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
                if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                {
                    if (player.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                    {
                        player.labelName.text = "【一階の守護者】\r\n\r\n絡みつくフランシス";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_LEVIATHAN)
                    {
                        player.labelName.text = "【二階の守護者】\r\n\r\n大海蛇リヴィアサン";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_HOWLING_SEIZER)
                    {
                        player.labelName.text = "【三階の守護者】\r\n\r\n恐鳴主ハウリング・シーザー";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                    {
                        player.labelName.text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【瘴気】";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                    {
                        player.labelName.text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【無音】";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                    {
                        player.labelName.text = "【四階の守護者】\r\n\r\n闇焔レギィン・アーゼ【深淵】";
                    }
                    else if (player.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                    {
                        player.labelName.text = "支　配　竜";
                        Font font = (Font)(Resources.Load<Font>(Database.FONT_KOUZAN_MOUHITSU));
                        player.labelName.font = font;
                        //player.labelCurrentSkillPoint.Visible = false;
                        //player.labelCurrentManaPoint.Visible = false;
                    }
                    player.labelName.color = UnityColor.DarkOrange;

                    if (player.FirstName == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                    {
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(185, 235, 0);
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 50);
                        player.labelCurrentLifePoint.fontSize = 42;
                        player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(620, 255, 0);
                        player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 220);
                        player.labelName.fontSize = 28;
                        player.labelName.alignment = TextAnchor.MiddleCenter;
                        player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, 20, 0);
                        player.CriticalLabel.fontSize = 38;
                        player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, -40, 0);
                        player.DamageLabel.fontSize = 34;
                        player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(234, 235, 0);
                        player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                        player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(600, 215, 0);
                        player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
                        player.ActionLabel.fontSize = 22;
                        player.ActionLabel.alignment = TextAnchor.MiddleLeft;
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(50, 190, 0);
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(550, 10, 0);
                        player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(600, 165, 0);
                        player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(600, 135, 0);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 20);
                        player.labelCurrentInstantPoint.color = UnityColor.Gold;

                    }
                    else if (player.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                    {
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(185, 75, 0);
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 50);
                        player.labelCurrentLifePoint.fontSize = 42;
                        player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(620, 100, 0);
                        player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 220);
                        player.labelName.fontSize = 28;
                        player.labelName.alignment = TextAnchor.MiddleCenter;
                        player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, 20, 0);
                        player.CriticalLabel.fontSize = 38;
                        player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, -40, 0);
                        player.DamageLabel.fontSize = 34;
                        player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(234, 75, 0);
                        player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                        player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(600, 65, 0);
                        player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
                        player.ActionLabel.fontSize = 22;
                        player.ActionLabel.alignment = TextAnchor.MiddleLeft;
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(50, 35, 0);
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(550, 10, 0);
                        player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(600, 10, 0);
                        player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(600, -20, 0);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 20);
                        player.labelCurrentInstantPoint.color = UnityColor.Gold;
                    }
                    else if (player.FirstName == Database.ENEMY_SEA_STAR_ORIGIN_KING ||
                             player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU ||
                             player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                    {
                        // レイアウト変更はしない。
                    }
                    else
                    {
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(280, 220, 0);
                        player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(230, 50);
                        player.labelCurrentLifePoint.fontSize = 42;
                        player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(620, 170, 0);
                        player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 220);
                        player.labelName.fontSize = 28;
                        player.labelName.alignment = TextAnchor.MiddleCenter;
                        player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, 20, 0);
                        player.CriticalLabel.fontSize = 38;
                        player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, -40, 0);
                        player.DamageLabel.fontSize = 34;
                        player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(165, 130, 0);
                        player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                        player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(600, 90, 0);
                        player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
                        player.ActionLabel.fontSize = 24;
                        player.ActionLabel.alignment = TextAnchor.MiddleLeft;
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(50, 55, 0);
                        player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(550, 10, 0);
                        player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(600, 30, 0);
                        player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(600, -10, 0);
                        player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                        player.labelCurrentInstantPoint.color = UnityColor.Gold;

                        if (player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1 || 
                            player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_2 ||
                            player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                        {
                            player.meterCurrentManaPoint.gameObject.SetActive(true);
                            player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 38);
                            player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(50, 70, 0);
                        }
                        else if (player.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                        {
                            player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(260, 180, 0);
                            player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(145, 110, 0);
                            player.labelName.fontSize = 70;
                            player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(620, 163, 0);
                            player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 143);
                        }
                    }
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


            //        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
            //        {
            //            player.labelCurrentManaPoint.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_MANA_LABEL_LOC_Y);
            //            player.labelCurrentManaPoint.Size = new Size(TruthLayout.BOSS_MANA_LABEL_WIDTH, TruthLayout.BOSS_MANA_LABEL_HEIGHT);
            //            player.labelCurrentManaPoint.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
            //        }
                }
                else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Purple)
                {
                    player.labelName.color = UnityColor.Purple;
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(280, 220, 0);
                    player.labelCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector2(230, 50);
                    player.labelCurrentLifePoint.fontSize = 42;
                    player.labelName.GetComponent<RectTransform>().localPosition = new Vector3(620, 170, 0);
                    player.labelName.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 220);
                    player.labelName.fontSize = 28;
                    player.labelName.alignment = TextAnchor.MiddleCenter;
                    player.CriticalLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, 20, 0);
                    player.CriticalLabel.fontSize = 38;
                    player.DamageLabel.GetComponent<RectTransform>().localPosition = new Vector3(30, -40, 0);
                    player.DamageLabel.fontSize = 34;
                    player.MainObjectBack.GetComponent<RectTransform>().localPosition = new Vector3(165, 130, 0);
                    player.MainObjectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    player.ActionLabel.GetComponent<RectTransform>().localPosition = new Vector3(600, 90, 0);
                    player.ActionLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
                    player.ActionLabel.fontSize = 24;
                    player.ActionLabel.alignment = TextAnchor.MiddleLeft;
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().localPosition = new Vector3(50, 55, 0);
                    player.meterCurrentLifePoint.GetComponent<RectTransform>().sizeDelta = new Vector3(550, 10, 0);
                    player.BuffPanel.GetComponent<RectTransform>().localPosition = new Vector3(600, 30, 0);
                    player.BuffPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().localPosition = new Vector3(600, -10, 0);
                    player.meterCurrentInstantPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 40);
                    player.labelCurrentInstantPoint.color = UnityColor.Gold;

                    //        player.labelName.Visible = false;
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
                            this.cannotRunAway = true;
                }
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
            }
            #endregion

            // 敵側、初期BUFFをセットアップ
            if (player == ec1 && player.FirstName == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
            {
                player.CurrentResistFireUp = Database.INFINITY;
                player.CurrentResistFireUpValue = 2000;
                player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", Database.INFINITY);
            }
            if (player == ec2 && player.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
            {
                player.CurrentResistIceUp = Database.INFINITY;
                player.CurrentResistIceUpValue = 2000;
                player.ActivateBuff(player.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp", Database.INFINITY);
            }

            // 死んでいる場合、グレー化する
            if (player.Dead)
            {
                player.DeadPlayer();
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
                    Method.SetupActionButton(actionButton[ii].gameObject, sorceryMark[ii], player.BattleActionCommandList[ii]);
                }
            }
        }

        void PointerEnter(TruthImage currentImage)
        {
            Vector3 current = Input.mousePosition;
            current.x -= 5;
            current.y += 5;
            popupInfo.transform.position = current;
            popupInfo.SetActive(true);
            CurrentInfo.text = currentImage.ImageName;
            CurrentInfo.text += "\r\n" + TruthActionCommand.GetDescriptionMini(currentImage.ImageName);
        }
        void PointerExit()
        {
            popupInfo.SetActive(false);
            CurrentInfo.text = "";
        }

        public void PointerMove()
        {
        }

        /// <summary>
        /// Unityのイベントトリガーを設定して、マウスイベント制御を設定、Buffパネルへのエントリーを設定
        /// </summary>
        /// <param name="pbBuff"></param>
        /// <param name="buffPanel"></param>
        /// <param name="ii"></param>
        void SetupBuff(ref TruthImage[] pbBuff, GameObject buffPanel, int ii)
        {
            GameObject baseObj = new GameObject("panelObj" + ii.ToString());
            //EventTrigger trigger = baseObj.AddComponent<EventTrigger>();
            
            //EventTrigger.Entry entry2 = new EventTrigger.Entry();
            //entry2.eventID = EventTriggerType.PointerExit;
            //entry2.callback.AddListener((x) => PointerExit());
            //trigger.triggers.Add(entry2);

            //EventTrigger.Entry entry3 = new EventTrigger.Entry();
            //entry3.eventID = EventTriggerType.Move;
            //entry3.callback.AddListener((x) => PointerMove());
            //trigger.triggers.Add(entry3);

            pbBuff[ii] = baseObj.AddComponent<TruthImage>();

            //EventTrigger.Entry entry = new EventTrigger.Entry();
            //entry.eventID = EventTriggerType.PointerEnter;
            //entry.callback.AddListener((x) => PointerEnter(pbBuff[ii]));
            //trigger.triggers.Add(entry);
            
            //pbBuff[ii].name = "buff" + ii; // change unity
            pbBuff[ii].BuffMode = TruthImage.buffType.Small;
            pbBuff[ii].rectTransform.anchorMin = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.anchorMax = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.pivot = new Vector2(1.0f, 0.5f);
            pbBuff[ii].rectTransform.sizeDelta = new Vector2(40, 40);
            pbBuff[ii].rectTransform.anchoredPosition = new Vector2(Database.BUFFPANEL_BUFF_WIDTH, 0);
            pbBuff[ii].gameObject.SetActive(false);
            pbBuff[ii].transform.SetParent(buffPanel.transform, false);
        }

        private void SetupBuffElement(MainCharacter player, ref TruthImage[] buffList)
        {
            #region "BUFFリストを登録"
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
            player.pbColorlessMove = buffList[num]; buffList[num].ImageName = Database.COLORLESS_MOVE; num++;
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
            player.pbColorlessAntidote = buffList[num]; buffList[num].ImageName = Database.BUFF_COLORLESS_ANTIDOTE; num++;

            player.pbLifeCount = buffList[num]; buffList[num].ImageName = Database.BUFF_LIFE_COUNT; num++;
            player.pbChaoticSchema = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOTIC_SCHEMA; num++;
            #endregion
        }

        private void InstantAttackPhase(string BattleActionCommand)
        {
            Debug.Log("InstantAttackPhase start");

            // 敵対象・味方対象・自分対象、単一敵、複数敵、単一味方、複数味方、状況によってＩＦ文を使い分ける。
            if (this.NowSelectingTarget == false)
            {
                Debug.Log("this.NowSelectingTarget false");

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
                Debug.Log("this.NowSelectingTarget else(true)");

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
                    (GroundOne.DuelMode))
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
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            NowSelectingTargetON();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.stackActivePlayer[this.stackActivePlayer.Count - 1], TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            NowSelectingTargetON();
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
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            NowSelectingTargetON();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.stackActivePlayer[this.stackActivePlayer.Count - 1], TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
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
                        if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally) ||
                            (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            NowSelectingTargetON();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ec1, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.stackActivePlayer[this.stackActivePlayer.Count - 1], TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
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
                        if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally) ||
                            (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy) ||
                            (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            NowSelectingTargetON();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.stackActivePlayer[this.stackActivePlayer.Count - 1], TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
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
            Debug.Log("ExecActionMethod start");
            // 1. 元核の場合、インスタント消費はせず、スタック情報も用いずにスタックを載せる。
            if ((CommandName == Database.ARCHETYPE_EIN) ||
                (CommandName == Database.ARCHETYPE_RANA) ||
                (CommandName == Database.ARCHETYPE_OL) ||
                (CommandName == Database.ARCHETYPE_VERZE)
                )
            {
                Debug.Log("ExecActionMethod 2");
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                Debug.Log("ExecActionMethod 2-2");
                this.NowStackInTheCommand = true;
            }
            else if (IsPlayerEnemy(player) && (((TruthEnemyCharacter)player).UseStackCommand))
            {
                Debug.Log("ExecActionMethod 3");
                if (UseInstantPoint(player) == false) { return; }
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                Debug.Log("ExecActionMethod 3-2");
                this.NowStackInTheCommand = true;
            }
            else
            {
                if ((GroundOne.DuelMode) ||
                    (this.NowStackInTheCommand))
                {
                    Debug.Log("ExecActionMethod 4");
                    if (UseInstantPoint(player) == false) { return; }
                    player.StackActivePlayer = player;
                    player.StackTarget = target;
                    player.StackPlayerAction = PA;
                    player.StackCommandString = CommandName;
                    player.StackActivation = true;
                    Debug.Log("ExecActionMethod 4-2");
                    this.NowStackInTheCommand = true;
                }
                else
                {
                    Debug.Log("ExecActionMethod 5");
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
            NowSelectingTargetOFF();
        }

        private bool UseInstantPoint(MainCharacter player)
        {
            if (player.CurrentInstantPoint <= 0)
            {
                // インスタントポイントが既に０の場合、何もしない
                return false;
            }

            player.CurrentInstantPoint = 0;
            UpdateInstantPoint(player);
            return true;
        }
        private void UseSpecialInstant(MainCharacter player)
        {
            player.CurrentSpecialInstant = 0;
            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
            }
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

            if (player.CurrentInstantPoint < player.MaxInstantPoint)
            {
                player.CurrentInstantPoint += (int)PrimaryLogic.BattleResponseValue(player, GroundOne.DuelMode);
            }
            UpdateInstantPoint(player);

            if (player.CurrentSpecialInstant < player.MaxSpecialInstant)
            {
                player.CurrentSpecialInstant += PrimaryLogic.BattleResponseValue(player, GroundOne.DuelMode);
            }
            UpdateSpecialInstantPoint(player);
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
            double movement = PrimaryLogic.BattleSpeedValue(player, GroundOne.DuelMode);
            if (player.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
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

            // StanceOfFlow特有のポジション更新
            if ((player.CurrentStanceOfFlow > 0) && (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH))
            {
                if (this.StayOn_StanceOfFlow == false && this.BreakOn_StanceOfFlow == false)
                {
                    this.StayOn_StanceOfFlow = true;
                    player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                }
                else
                {
                    if (this.BreakOn_StanceOfFlow == false)
                    {
                        player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                    }
                    else
                    {
                        this.StayOn_StanceOfFlow = false;
                        this.BreakOn_StanceOfFlow = false;
                    }
                }
            }


        }

        private void UpdatePlayerNextDecision(MainCharacter player)
        {
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC) return; // コンピューター専用ルーチンのため、プレイヤー側は何もしない。

            if (player.FirstName == Database.DUEL_OL_LANDIS) // オル・ランディスは常に戦術を変更可能とする。ヴェルゼなど主要人物は全て該当。
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else if (player.FirstName == Database.VERZE_ARTIE_FULL || player.FirstName == Database.VERZE_ARTIE
                  || player.FirstName == Database.ENEMY_LAST_RANA_AMILIA
                  || player.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ
                  || player.FirstName == Database.ENEMY_LAST_OL_LANDIS
                  || player.FirstName == Database.ENEMY_LAST_VERZE_ARTIE
                  || player.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else if ((player.FirstName == Database.DUEL_SHUVALTZ_FLORE) ||
                     (player.FirstName == Database.DUEL_SIN_OSCURETE))
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
            }
            else
            {
                if ((!player.ActionDecision && player.BattleBarPos > player.DecisionTiming) ||
                    ((TruthEnemyCharacter)player).FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
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
                        if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && !GroundOne.TC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.TC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && !GroundOne.SC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.SC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                        else if (GroundOne.MC != null && !GroundOne.MC.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(GroundOne.MC, GroundOne.MC, GroundOne.SC, GroundOne.TC, ec1, ec2, ec3);
                        }
                    }
                }
            }
        }

        private void UpdatePlayerDoStackInTheCommand(MainCharacter player)
        {
            if (IsPlayerAlly(player)) { return; } // 味方プレイヤーは自動的に何らかの行動は行わない。
            if (this.NowStackInTheCommand) { return; } // スタック・イン・ザ・コマンド中はスルー

            #region "セルモイ・ロウ"
            if (player.FirstName == Database.DUEL_SELMOI_RO)
            {
                bool existItem = false;
                ItemBackPack[] tempItem = player.GetBackPackInfo();
                foreach (ItemBackPack value in tempItem)
                {
                    if (value != null)
                    {
                        if (value.Name == Database.COMMON_FROZEN_BALL)
                        {
                            existItem = true;
                        }
                    }
                }

                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 50 < player.BattleBarPos && player.BattleBarPos < 100)
                {
                    if (existItem)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseItem;
                        player.StackCommandString = Database.COMMON_FROZEN_BALL;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else if (player.CurrentAbsorbWater <= 0)
                    {
                        if (player.CurrentAbsorbWater <= 0)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = player;
                            player.StackTarget = player;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                            player.StackCommandString = Database.ABSORB_WATER;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = player;
                            player.StackTarget = GroundOne.MC;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                            player.StackCommandString = Database.WORD_OF_POWER;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                    else
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_POWER;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "カーティン・マイ"
            else if (player.FirstName == Database.DUEL_KARTIN_MAI)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.CurrentHeatBoost <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = player;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.HEAT_BOOST;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else if (player.CurrentShadowPact <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = player;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.SHADOW_PACT;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.FIRE_BALL;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "ジェダ・アルス"
            else if (player.FirstName == Database.DUEL_JEDA_ARUS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = player;
                    player.StackTarget = GroundOne.MC;
                    player.StackPlayerAction = MainCharacter.PlayerAction.UseItem;
                    player.StackCommandString = Database.RARE_AERO_BLADE;
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "シニキア・ヴェイルハンツ"
            else if (player.FirstName == Database.DUEL_SINIKIA_VEILHANZ)
            {
                if (player.CurrentLife < player.MaxLife / 3)
                {
                    if (player.CurrentInstantPoint >= player.MaxInstantPoint && 400 < player.BattleBarPos && player.BattleBarPos < 450)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.LIFE_TAP;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "【１階】絡みつくフランシス"
            else if (player.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (GroundOne.MC != null && !GroundOne.MC.Dead) player.StackTarget = GroundOne.MC;
                    else if (GroundOne.SC != null && !GroundOne.SC.Dead) player.StackTarget = GroundOne.SC;
                    //player.StackTarget = null; // 「警告」null指定がスタックインザコマンドで「全体」を表現しようとしているが、思いつきの新仕様である。全体仕様のどこに関わってくるか考察してください。
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "キル・スピニングランサー";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "オル・ランディス（初DUEL)
            else if (player.FirstName == Database.DUEL_OL_LANDIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((GroundOne.MC.CurrentLife < 500) && (GroundOne.MC.CurrentParalyze <= 0) && (ec1.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST))
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                        player.StackCommandString = Database.SURPRISE_ATTACK;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        if ((GroundOne.MC.CurrentBlackFire <= 0) && (ec1.CurrentMana >= Database.BLACK_FIRE_COST))
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = GroundOne.MC;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                            player.StackCommandString = Database.BLACK_FIRE;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else if ((GroundOne.MC.CurrentImmolate <= 0) && (ec1.CurrentMana >= Database.IMMOLATE_COST))
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = GroundOne.MC;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                            player.StackCommandString = Database.IMMOLATE;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                }
                // ちょっと強すぎるので、封印か。
                //    if ((GroundOne.MC.CurrentLife < GroundOne.MC.MaxLife / 2) && (ec1.CurrentSkillPoint >= Database.CRUSHING_BLOW_COST))
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = GroundOne.MC;
                //        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                //        player.StackCommandString = Database.CRUSHING_BLOW;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //    else if (ec1.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = GroundOne.MC;
                //        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                //        player.StackCommandString = Database.STRAIGHT_SMASH;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //    else 
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = ec1;
                //        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                //        player.StackCommandString = Database.INNER_INSPIRATION;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }                    
                //}
            }
            #endregion
            #region "輝ける海の王子"
            else if (player.FirstName == Database.ENEMY_BRILLIANT_SEA_PRINCE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null; // 「警告」null指定がスタックインザコマンドで「全体」を表現しようとしているが、思いつきの新仕様である。全体仕様のどこに関わってくるか考察してください。
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "アイソニック・ウェイヴ";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "源星・珊瑚の女王"
            else if (player.FirstName == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (player.CurrentPhysicalDefenseUp <= 0)
                    {
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                        player.StackCommandString = "アンダートの詠唱";
                    }
                    else if (player.CurrentMirrorImage <= 0)
                    {
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                        player.StackCommandString = "サルマンの詠唱";
                    }
                    else
                    {
                        if (AP.Math.RandomInteger(2) == 0)
                        {
                            player.StackTarget = null;
                            player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                            player.StackCommandString = "エレメンタル・スプラッシュ";

                        }
                        else
                        {
                            player.StackTarget = ec1;
                            player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                            player.StackCommandString = "生命の龍水";
                        }
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "ジュエル・ナイト"
            else if (player.FirstName == Database.ENEMY_SHELL_SWORD_KNIGHT)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (GroundOne.MC != null && !GroundOne.MC.Dead) { player.StackTarget = GroundOne.MC; }
                    else if (GroundOne.SC != null && !GroundOne.SC.Dead) { player.StackTarget = GroundOne.SC; }
                    else if (GroundOne.TC != null && !GroundOne.TC.Dead) { player.StackTarget = GroundOne.TC; }
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "ジュエル・ブレイク";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "ジェリーアイ・赤"
            else if (player.FirstName == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (GroundOne.MC != null && !GroundOne.MC.Dead) { player.StackTarget = GroundOne.MC; }
                    else if (GroundOne.SC != null && !GroundOne.SC.Dead) { player.StackTarget = GroundOne.SC; }
                    else if (GroundOne.TC != null && !GroundOne.TC.Dead) { player.StackTarget = GroundOne.TC; }
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "溶岩の一撃";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "ジェリーアイ・青"
            else if (player.FirstName == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec2;
                    if (GroundOne.MC != null && !GroundOne.MC.Dead) { player.StackTarget = GroundOne.MC; }
                    else if (GroundOne.SC != null && !GroundOne.SC.Dead) { player.StackTarget = GroundOne.SC; }
                    else if (GroundOne.TC != null && !GroundOne.TC.Dead) { player.StackTarget = GroundOne.TC; }
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "凍雹の一撃";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "海星騎士エーギル"
            else if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec2;

                    if (GroundOne.SC != null && !GroundOne.SC.Dead) { player.StackTarget = GroundOne.SC; }
                    else if (GroundOne.TC != null && !GroundOne.TC.Dead) { player.StackTarget = GroundOne.TC; }
                    else if (GroundOne.MC != null && !GroundOne.MC.Dead) { player.StackTarget = GroundOne.MC; }
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "スター・ダスト";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "海星騎士アマラ"
            else if (player.FirstName == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec3;

                    if (GroundOne.TC != null && !GroundOne.TC.Dead) { player.StackTarget = GroundOne.TC; }
                    else if (GroundOne.SC != null && !GroundOne.SC.Dead) { player.StackTarget = GroundOne.SC; }
                    else if (GroundOne.MC != null && !GroundOne.MC.Dead) { player.StackTarget = GroundOne.MC; }
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "スター・フォール";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "海星源の王"
            else if (player.FirstName == Database.ENEMY_SEA_STAR_ORIGIN_KING)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "海星源の授印";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "アデル・ブリガンディ"
            else if (player.FirstName == Database.DUEL_ADEL_BRIGANDY)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.CurrentLife < player.MaxLife)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.FRESH_HEAL;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "レネ・コルトス"
            else if (player.FirstName == Database.DUEL_LENE_COLTOS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.FRESH_HEAL;
                    }
                    else
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseItem;
                        player.StackCommandString = Database.RARE_BLUE_LIGHTNING;
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "スコーティ・ザルゲ"
            else if (player.FirstName == Database.DUEL_SCOTY_ZALGE)
            {
            }
            #endregion
            #region "ペルマ・ワラミィ"
            else if (player.FirstName == Database.DUEL_PERMA_WARAMY)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && player.BattleBarPos < 50)
                {
                    UseInstantPoint(player);
                    if (player.CurrentLife <= player.MaxLife * 2 / 3)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.FRESH_HEAL;
                    }
                    else if (player.CurrentWordOfLife <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_LIFE;
                    }
                    else if (GroundOne.MC.CurrentEnrageBlast <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.ENRAGE_BLAST;
                    }
                    else if (player.CurrentFlameAura <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.FLAME_AURA;
                    }
                    else if (player.CurrentStrengthUp <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseItem;
                        player.StackCommandString = Database.RARE_BURNING_CLAYMORE;
                    }
                    else
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_POWER;
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "キルト・ジョルジュ"
            else if (player.FirstName == Database.DUEL_KILT_JORJU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (GroundOne.MC.CurrentLife <= 1200)
                    {
                        if ((player.CurrentWordOfFortune <= 0))
                        {
                            if (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH - 50)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = player;
                                player.StackTarget = player;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.WORD_OF_FORTUNE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                        }
                        else
                        {
                            if (player.CurrentSkillPoint > Database.STRAIGHT_SMASH_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = GroundOne.MC;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                player.StackCommandString = Database.STRAIGHT_SMASH;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else if (player.CurrentMana > Database.BLUE_BULLET_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = GroundOne.MC;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                        }
                    }
                    else
                    {
                        if (player.CurrentSkillPoint > Database.STRAIGHT_SMASH_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = GroundOne.MC;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                            player.StackCommandString = Database.STRAIGHT_SMASH;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else if (player.CurrentMana > Database.BLUE_BULLET_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = GroundOne.MC;
                            player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                            player.StackCommandString = Database.BLUE_BULLET;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                }
            }
            #endregion
            #region "【２階】大海蛇リヴィアサン"
            else if (player.FirstName == Database.ENEMY_BOSS_LEVIATHAN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    player.StackCommandString = "タイダル・ウェイブ";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "ヴェルゼ・アーティ(初DUEL）"
            else if (player.FirstName == Database.VERZE_ARTIE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((player.CurrentStanceOfMystic <= 0) && (player.CurrentSkillPoint >= Database.STANCE_OF_MYSTIC_COST))
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.STANCE_OF_MYSTIC);
                    }
                    else if (GroundOne.MC.CurrentHolyBreaker > 0)
                    {
                        if (player.CurrentSkillPoint > Database.PSYCHIC_WAVE_COST)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.PSYCHIC_WAVE);
                        }
                        else
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                    }
                    else if (player.CurrentGaleWind > 0)
                    {
                        if (player.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                        }
                        else
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (player.CurrentCounterAttack <= 0 && player.CurrentDeflection <= 0)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if (player.CurrentSkyShieldValue <= 0 && player.CurrentMirrorImage <= 0)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.MIRROR_IMAGE);
                    }
                    else if (player.CurrentSkillPoint < player.MaxSkillPoint)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                }
            }
            #endregion
            #region "ビリー・ラキ"
            else if (player.FirstName == Database.DUEL_BILLY_RAKI)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((player.CurrentSkillPoint > Database.DOUBLE_SLASH_COST) && (player.CurrentSkillPoint < Database.CARNAGE_RUSH_COST))
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = GroundOne.MC;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                        player.StackCommandString = Database.DOUBLE_SLASH;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }                
            }
            #endregion
            #region "アンナ・ハミルトン"
            else if (player.FirstName == Database.DUEL_ANNA_HAMILTON)
            {
                // とくになし
            }
            #endregion
            #region "カルマンズ・オーン"
            else if (player.FirstName == Database.DUEL_CALMANS_OHN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.CurrentWordOfLife <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_LIFE;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                        {
                            if (player.CurrentGaleWind > 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = GroundOne.MC;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = GroundOne.MC;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.DEVOURING_PLAGUE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }

                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                        }
                        else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                        {
                            if (player.CurrentPromisedKnowledge <= 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = ec1;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.PROMISED_KNOWLEDGE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = GroundOne.MC;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }

                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                        }
                        else
                        {
                            if (GroundOne.MC.CurrentLife <= GroundOne.MC.MaxLife / 2)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = ec1;
                                player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                player.StackCommandString = Database.GALE_WIND;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                if (player.CurrentGaleWind > 0)
                                {
                                    UseInstantPoint(player);
                                    player.StackActivePlayer = ec1;
                                    player.StackTarget = GroundOne.MC;
                                    player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                    player.StackCommandString = Database.BLUE_BULLET;
                                    player.StackActivation = true;
                                    this.NowStackInTheCommand = true;
                                }
                                else
                                {
                                    UseInstantPoint(player);
                                    player.StackActivePlayer = ec1;
                                    player.StackTarget = GroundOne.MC;
                                    player.StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                    player.StackCommandString = Database.DEVOURING_PLAGUE;
                                    player.StackActivation = true;
                                    this.NowStackInTheCommand = true;
                                }
                            }
                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                        }
                    }
                }
            }
            #endregion
            #region "サン・ユウ"
            else if (player.FirstName == Database.DUEL_SUN_YU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (GroundOne.MC.CurrentAetherDrive > 0)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.TRANQUILITY);
                    }
                    else if (GroundOne.MC.CurrentSaintPower > 0)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                    }
                }
            }
            #endregion
            #region "シュヴァルツェ・フローレ"
            else if (player.FirstName == Database.DUEL_SHUVALTZ_FLORE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if (player.CurrentFlameAura <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if (player.CurrentProtection <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PROTECTION);
                        }
                        else if (player.CurrentAbsorbWater <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.ABSORB_WATER);
                        }
                        else if (player.CurrentSaintPower <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.SAINT_POWER);
                        }
                        else if (player.CurrentFrozenAura <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.FROZEN_AURA);
                        }
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }
                    else
                    {
                        if (50 < GroundOne.MC.BattleBarPos && GroundOne.MC.BattleBarPos < 75)
                        {
                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                        }
                    }
                }
            }
            #endregion
            #region "シニキア・カールハンツ(DUEL)"
            else if (player.FirstName == Database.DUEL_SINIKIA_KAHLHANZ)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint) &&
                    (GroundOne.MC.CurrentInstantPoint < GroundOne.MC.MaxInstantPoint))
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if ((GroundOne.MC.CurrentFrozen <= 0) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (player.CurrentHeatBoost <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.HEAT_BOOST);
                        }
                        else if (player.CurrentPsychicTrance <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PSYCHIC_TRANCE);
                        }
                        else if (GroundOne.MC.CurrentWordOfMalice <= 0)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.WORD_OF_MALICE);
                        }
                        else if (GroundOne.MC.CurrentBlackFire <= 0)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.PIERCING_FLAME);
                        }
                        //((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    //else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    //{
                    //    ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    //    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    //}
                    //else
                    //{
                    //    if (50 < GroundOne.MC.BattleBarPos && GroundOne.MC.BattleBarPos < 75)
                    //    {
                    //        ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                    //    }
                    //}
                }
            }
            #endregion
            #region "【３階】ハウリング・シーザー"
            else if (player.FirstName == Database.ENEMY_BOSS_HOWLING_SEIZER)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    ExecActionMethod(player, null, MainCharacter.PlayerAction.SpecialSkill, "アース・コールド・シェイク");
                }
            }
            #endregion
            #region "ルベル・ゼルキス"
            else if (player.FirstName == Database.DUEL_RVEL_ZELKIS)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    if (GroundOne.MC.CurrentFrozen > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    else if (player.CurrentPromisedKnowledge > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }

                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if ((GroundOne.MC.CurrentFrozen <= 0) &&
                            (player.CurrentMana >= Database.CHILL_BURN_COST) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if ((player.CurrentPromisedKnowledge <= 0) &&
                                 (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 2)
                    {
                        if ((GroundOne.MC.CurrentFrozen <= 0) &&
                            (player.CurrentMana >= Database.CHILL_BURN_COST) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (player.CurrentBlackContract <= 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                        }
                        else if (player.CurrentSkillPoint >= Database.CARNAGE_RUSH_COST)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                    }
                }
            }
            #endregion
            #region "ヴァン・ヘーグステル"
            else if (player.FirstName == Database.DUEL_VAN_HEHGUSTEL)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    // 戦術の判断
                    if (player.CurrentLightServant > 0 && player.CurrentLightServantValue >= 3)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    if (player.CurrentFlameAura > 0 && player.CurrentFrozenAura > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }

                    if (player.CurrentLightServant > 0 && player.CurrentLightServantValue >= 3 && player.CurrentLife < player.MaxLife / 2)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseItem, Database.COMMON_LIGHT_SERVANT);
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if ((player.CurrentFlameAura <= 0) &&
                            (player.CurrentMana >= Database.FLAME_AURA_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((player.CurrentFrozenAura <= 0) &&
                                 (player.CurrentMana >= Database.FROZEN_AURA_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.FROZEN_AURA);
                        }
                    }
                    else
                    {
                        if ((GroundOne.MC.CurrentLife < GroundOne.MC.MaxLife * 2 / 3) &&
                            (player.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST) &&
                            (GroundOne.MC.CurrentParalyze <= 0) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeParalyze == false))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.SURPRISE_ATTACK);
                        }
                        else
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                }
            }
            #endregion
            #region "オウリュウ・ゲンマ"
            else if (player.FirstName == Database.DUEL_OHRYU_GENMA)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    if (player.CurrentSkillPoint < Database.CARNAGE_RUSH_COST)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    else if ((player.CurrentSwiftStep <= 0) &&
                        (player.CurrentSkillPoint >= Database.SWIFT_STEP_COST))
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.SWIFT_STEP);
                    }
                    else if ((player.CurrentVoidExtraction <= 0) &&
                             (player.CurrentSkillPoint >= Database.VOID_EXTRACTION_COST))
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                    }
                    else if ((player.CurrentSkillPoint >= Database.WORD_OF_POWER_COST))
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                }
            }
            #endregion
            #region "ラダ・ミストゥルス"
            else if (player.FirstName == Database.DUEL_LADA_MYSTORUS)
            {
            }
            #endregion
            #region "シン・オスキュレーテ"
            else if (player.FirstName == Database.DUEL_SIN_OSCURETE)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint) &&
                    (GroundOne.MC.CurrentInstantPoint < 500) &&
                    (GroundOne.MC.CurrentMana > 0))
                {
                    if ((player.CurrentAntiStun <= 0) &&
                        (player.CurrentSkillPoint >= Database.ANTI_STUN_COST))
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.ANTI_STUN);
                    }
                    else if ((player.CurrentOneImmunity <= 0) &&
                             (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    else if (player.CurrentSkillPoint <= 30)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    // この時点では強すぎるため、コメントアウト
                    //if ((player.CurrentTimeStop <= 0) &&
                    //    (player.CurrentMana >= Database.TIME_STOP_COST) &&
                    //    (player.CurrentWordOfLife <= 0))
                    //{
                    //    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.TIME_STOP);
                    //}
                }
            }
            #endregion
            #region "【４階】レギィンアーゼ"
            else if (player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE ||
                     player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1 ||
                     player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_2 ||
                     player.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    ExecActionMethod(player, null, MainCharacter.PlayerAction.SpecialSkill, "虚無の鼓動");
                }
            }
            #endregion
            #region "ラナ・アミリア(DUEL)"
            else if (player.FirstName == Database.ENEMY_LAST_RANA_AMILIA)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (!((TruthEnemyCharacter)player).StillNotAction1 &&
                        player.CurrentMana >= Database.TIME_STOP_COST)
                    {
                        ((TruthEnemyCharacter)player).StillNotAction1 = true;
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.TIME_STOP);
                    }
                    else
                    {
                        if ((player.CurrentOneImmunity <= 0) &&
                            (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                        }
                        else if ((player.CurrentVoidExtraction <= 0) && (player.CurrentSkillPoint >= Database.VOID_EXTRACTION_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                        }
                        else if ((player.CurrentPromisedKnowledge <= 0) && (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                        else if ((GroundOne.MC.CurrentImpulseHitValue < 3) && (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((GroundOne.MC.CurrentMana >= GroundOne.MC.MaxMana / 5) && (player.CurrentMana >= Database.DOOM_BLADE_COST))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.DOOM_BLADE);
                        }
                        else
                        {
                            switch (AP.Math.RandomInteger(2))
                            {
                                case 0:
                                    if (player.CurrentMana >= Database.WHITE_OUT_COST)
                                    {
                                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.WHITE_OUT);
                                    }
                                    break;
                                case 1:
                                    if (player.CurrentSkillPoint >= Database.ENIGMA_SENSE_COST)
                                    {
                                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion
            #region "シニキア・カールハンツ(DUEL2)"
            else if (player.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.BattleBarPos <= 250)
                    {
                        if ((player.CurrentOneImmunity <= 0) &&
                            (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                        }
                        //else if ((player.CurrentLife <= player.MaxLife / 2) && (player.CurrentMana >= Database.LIFE_TAP_COST))
                        //{
                        //    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.LIFE_TAP);
                        //}
                        //else if ((player.CurrentPhantasmalWind <= 0) && (player.CurrentMana >= Database.PHANTASMAL_WIND_COST))
                        //{
                        //    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PHANTASMAL_WIND);
                        //}
                        //else if ((player.CurrentMana >= Database.WARP_GATE_COST) && (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH / 2))
                        //{
                        //    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.WARP_GATE);
                        //}   
                        else if (player.MaxLife * 0.0f <= player.CurrentLife && player.CurrentLife < player.MaxLife * 0.8f && player.CurrentMana >= Database.LIFE_TAP_COST)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else if ((GroundOne.MC.CurrentSigilOfHomura <= 0) && (player.CurrentMana >= Database.SIGIL_OF_HOMURA_COST))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                        }
                        else if ((player.CurrentRedDragonWill <= 0) && (player.CurrentMana >= Database.RED_DRAGON_WILL_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                        }
                        //else if ((GroundOne.MC.CurrentEnrageBlast <= 0) && (player.CurrentMana >= Database.ENRAGE_BLAST_COST))
                        //{
                        //    ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                        //}

                        //else if ((player.CurrentPromisedKnowledge <= 0) && (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        //{
                        //    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        //}
                        //else if ((GroundOne.MC.CurrentImpulseHitValue < 3) && (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST))
                        //{
                        //    ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        //}
                        //else if ((GroundOne.MC.CurrentMana >= GroundOne.MC.MaxMana / 5) && (player.CurrentMana >= Database.DOOM_BLADE_COST))
                        //{
                        //    ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.DOOM_BLADE);
                        //}
                        //else
                        //{
                        //    switch (AP.Math.RandomInteger(2))
                        //    {
                        //        case 0:
                        //            if (player.CurrentMana >= Database.WHITE_OUT_COST)
                        //            {
                        //                ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.WHITE_OUT);
                        //            }
                        //            break;
                        //        case 1:
                        //            if (player.CurrentSkillPoint >= Database.ENIGMA_SENSE_COST)
                        //            {
                        //                ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                        //            }
                        //            break;
                        //    }
                        //}
                    }
                }
            }
            #endregion
            #region "オル・ランディス(DUEL2)"
            else if (player.FirstName == Database.ENEMY_LAST_OL_LANDIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (!((TruthEnemyCharacter)player).OpponentUseInstantPoint)
                    {
                        // 対戦相手がインスタント消費してない場合、何もしない
                    }
                    else
                    {
                        if (player.BeforeSkillName == Database.SOUL_EXECUTION)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.GENESIS);
                        }
                        else if ((player.CurrentLife < player.MaxLife / 2) && (GroundOne.MC.CurrentParalyze <= 0) && ((TruthEnemyCharacter)player).DetectCannotBeParalyze == false && player.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.SURPRISE_ATTACK);
                        }
                        else if ((player.CurrentLife < player.MaxLife / 2) && (player.CurrentMana >= Database.CELESTIAL_NOVA_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else if ((player.CurrentMana >= Database.DISPEL_MAGIC_COST) &&
                                 ((GroundOne.MC.CurrentSaintPower > 0) || (GroundOne.MC.CurrentHeatBoost > 0) || (GroundOne.MC.CurrentFlameAura > 0) || (GroundOne.MC.CurrentHolyBreaker > 0)))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                        }
                        else if ((player.CurrentMana >= Database.TRANQUILITY_COST) &&
                                ((GroundOne.MC.CurrentGaleWind > 0) || (GroundOne.MC.CurrentWordOfFortune > 0)))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.TRANQUILITY);
                        }
                        else if ((player.CurrentBlackContract <= 0) && (player.CurrentMana >= Database.BLACK_CONTRACT_COST))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                        }
                        else if ((player.CurrentFlameAura <= 0) && ((player.CurrentBlackContract > 0) || (player.CurrentMana >= Database.FLAME_AURA_COST)))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((player.CurrentGaleWind <= 0) && ((player.CurrentBlackContract > 0) || (player.CurrentMana >= Database.GALE_WIND_COST)))
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.GALE_WIND);
                        }
                        else if ((GroundOne.MC.CurrentImpulseHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST)))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((GroundOne.MC.CurrentOnslaughtHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                        }
                        else if ((GroundOne.MC.CurrentConcussiveHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                        }
                    }
                }
            }
            #endregion
            #region "ヴェルゼ・アーティ最終戦(DUEL)"
            else if (player.FirstName == Database.ENEMY_LAST_VERZE_ARTIE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if (player.CurrentFlameAura > 0 && player.CurrentFrozenAura > 0 && player.CurrentGaleWind > 0)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 9)
                    {
                        if (player.BeforeSpellName == Database.ZETA_EXPLOSION)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.GENESIS);
                        }
                    }
                }
            }
            #endregion
            #region "【原罪】ヴェルゼ・アーティ最終戦２(DUEL)"
            else if (player.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                if (player.CurrentSpecialInstant >= player.MaxSpecialInstant)
                {
                    UseSpecialInstant(player);
                    if (player.CurrentLifeCountValue == 3)
                    {
                        if (player.CurrentEclipseEnd > 0)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                        }
                        else
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_INVISIBLE_HUNDRED_CUTTER);
                        }
                    }
                    else if (player.CurrentLifeCountValue == 2)
                    {
                        ExecActionMethod(player, player, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                    }
                    else if (player.CurrentLifeCountValue == 1)
                    {
                        ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_ZERO_INNOCENT_SIN);
                    }
                    else if (player.CurrentLifeCountValue <= 0)
                    {
                        if (player.CurrentLife <= player.MaxLife * 0.5f)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_SEFINE_PAINFUL_HYMNUS);
                        }
                        else
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                        }
                    }
                }
                else if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.BattleBarPos <= 200)
                    {
                        if (player.CurrentGaleWind <= 0 && player.CurrentMana >= Database.GALE_WIND_COST)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.GALE_WIND);
                        }
                        else if (player.MaxLife * 0.4f <= player.CurrentLife && player.CurrentLife < player.MaxLife * 0.5f && player.CurrentMana >= Database.CELESTIAL_NOVA_COST)
                        {
                            ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (320 < player.BattleBarPos && player.BattleBarPos <= 350 && AP.Math.RandomInteger(40) == 0)
                    {
                        if (player.CurrentMana >= Database.WARP_GATE_COST)
                        {
                            ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.UseSpell, Database.WARP_GATE);
                        }
                    }
                }
                else if (player.CurrentFrozen > 0 || player.CurrentParalyze > 0 || player.CurrentStunning > 0)
                {
                    ExecActionMethod(player, player, MainCharacter.PlayerAction.UseSkill, Database.RECOVER);
                }
            }
            #endregion
            #region "支配竜"
            else if (player.FirstName == Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD ||
                     player.FirstName == Database.ENEMY_DRAGON_TINKOU_DEEPSEA ||
                     player.FirstName == Database.ENEMY_DRAGON_DESOLATOR_AZOLD ||
                     player.FirstName == Database.ENEMY_DRAGON_IDEA_CAGE_ZEED ||
                     player.FirstName == Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA)
            {
                if (((TruthEnemyCharacter)player).AI_TacticsNumber == 5)
                {
                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 6;
                    ExecActionMethod(player, GroundOne.MC, MainCharacter.PlayerAction.SpecialSkill, "形成消失");
                }
            }
            #endregion
            #region "Bystander Emptiness"
            else if (player.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                TruthEnemyCharacter current = (TruthEnemyCharacter)player;
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = MainCharacter.PlayerAction.SpecialSkill;
                    int rand = AP.Math.RandomInteger(4);
                    if (rand == 0) { player.StackTarget = current.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC); player.StackCommandString = "キル・スピニングランサー"; }
                    else if (rand == 1) { player.StackCommandString = "アース・コールド・シェイク"; }
                    else if (rand == 2) { player.StackCommandString = "タイダル・ウェイブ"; }
                    else if (rand == 3) { player.StackCommandString = "虚無の鼓動"; }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "ダミー素振り君"
            else if (player.FirstName == Database.DUEL_DUMMY_SUBURI)
            {
                //if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                //{
                //    //if (player.CurrentTimeStop > 0)
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = GroundOne.MC;
                //        player.StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                //        player.StackCommandString = Database.STRAIGHT_SMASH;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //}
            }
            #endregion
        }

        private void UpdatePlayerPreCondition(MainCharacter player, int arrowType)
        {
            // StanceOfFlow特有記述
            if (this.StayOn_StanceOfFlow)
            {
                this.BreakOn_StanceOfFlow = true;
            }

            if (arrowType == 0) { player.BattleBarPos = 0; }
            else if (arrowType == 1) { player.BattleBarPos2 = 0; }
            else if (arrowType == 2) { player.BattleBarPos3 = 0; }
            Vector3 current = player.MainFaceArrow.transform.position;
            player.MainFaceArrow.transform.position = new Vector3((float)player.BattleBarPos, current.y, current.z);

            player.ActionDecision = false;
            // player.CurrentCounterAttack = false; // 次のコマンドを実行したらカウンターが消滅してしまうのはゲーム性質上、おもしろくない。
        }


        /// <summary>
        /// "[後編必須]ただし、パーティ編成が可能にすることを想定すると、このままではいけないはず。"
        /// </summary>
        /// <param name="player"></param>
        private void UpdatePlayerTarget(MainCharacter player)
        {
            if (player == ec1 || player == ec2 || player == ec3)
            {
                if (player.Target.Dead == false) { return; } // ターゲットが死亡してない場合は変更しない。

                if (GroundOne.DuelMode)
                {
                    // Duelモードの場合、なにもしない。
                }
                else if (GroundOne.WE.AvailableSecondCharacter == false && GroundOne.WE.AvailableThirdCharacter == false)
                {
                    // 味方一人の場合、なにもしない。
                }
                else if (GroundOne.WE.AvailableSecondCharacter == true && GroundOne.WE.AvailableThirdCharacter == false)
                {
                    // 味方二人の場合、死んでないほうへ切り替える。
                    if (GroundOne.MC != null && GroundOne.MC.Dead) player.Target = GroundOne.SC;
                    else if (GroundOne.SC != null & GroundOne.SC.Dead) player.Target = GroundOne.MC;
                }
                else
                {
                    List<MainCharacter> group = new List<MainCharacter>();
                    SetupAllyGroup(ref group);
                    if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
                    {
                        player.Target = group[0];
                    }
                    else if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
                    {
                        player.Target = group[group.Count - 1];
                    }
                }

                // 敵側の場合、プレイヤー側へ行動完了後の行動指針を待機にしたことを伝えるため。
                if (player.FullName == Database.DUEL_CALMANS_OHN)
                {
                    player.PA = MainCharacter.PlayerAction.Defense;
                    player.ActionLabel.text = Database.DEFENSE_JP;
                }
                else
                {
                    player.ActionLabel.text = Database.STAY_JP;
                }
            }
            else
            {
                if (ec2 == null && ec3 == null)
                {
                    // 敵一人の場合、なにもしない。
                }
                else if (ec2 != null && ec3 == null)
                {
                    // 敵二人の場合、死んでないほうへ切り替える。
                    if (ec1 != null && ec1.Dead) player.Target = ec2;
                    else if (ec2 != null && ec2.Dead) player.Target = ec1;
                }
                else
                {
                    if (player.Target.Dead == false) { return; }
                    List<MainCharacter> group = new List<MainCharacter>();
                    SetupEnemyGroup(ref group);
                    player.Target = group[0];
                }
            }
        }

        private void ExpGoldDisplay(int exp, int gold)
        {
            ExpGoldText.text = ec1.Exp + "の経験値、 " + ec1.Gold + "Goldを獲得";
            ExpGoldPanel.SetActive(true);
        }

        private void MessageDisplayWithIcon(ItemBackPack item)
        {
            if (item != null)
            {
                treasureText.text = "【 " + item.Name + " 】を入手！";

                switch (item.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        treasurePanel.GetComponent<Image>().color = Color.gray;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        treasurePanel.GetComponent<Image>().color = UnityColor.CommonGreen;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        treasurePanel.GetComponent<Image>().color = UnityColor.DarkBlue;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        treasurePanel.GetComponent<Image>().color = UnityColor.Purple;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // 後編追加
                        treasurePanel.GetComponent<Image>().color = UnityColor.OrangeRed;
                        break;
                    default:
                        treasurePanel.GetComponent<Image>().color = Color.gray;
                        break;
                }

                Texture2D current = Resources.Load<Texture2D>("ItemIcon");
                int BASE_SIZE = 49;
                int locX = 0;
                int locY = 0;
                switch (item.Type)
                {
                    case ItemBackPack.ItemType.Weapon_TwoHand:
                        locX = 1; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Light:
                        locX = 2; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Heavy:
                    case ItemBackPack.ItemType.Weapon_Middle:
                        locX = 0; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Weapon_Rod:
                        locX = 3; locY = 2;
                        break;
                    case ItemBackPack.ItemType.Accessory:
                        locX = 0; locY = 3;
                        break;
                    case ItemBackPack.ItemType.Armor_Heavy:
                    case ItemBackPack.ItemType.Armor_Middle:
                        locX = 1; locY = 1;
                        break;
                    case ItemBackPack.ItemType.Armor_Light:
                        locX = 2; locY = 1;
                        break;
                    //case ItemBackPack.ItemType.Robe:
                    //    locX = 3; locY = 1;
                    //    break;
                    case ItemBackPack.ItemType.Material_Equip:
                    case ItemBackPack.ItemType.Material_Food:
                    case ItemBackPack.ItemType.Material_Potion:
                        locX = 0; locY = 0;
                        break;
                    case ItemBackPack.ItemType.Shield:
                        locX = 0; locY = 1;
                        break;
                    case ItemBackPack.ItemType.Use_Item:
                        locX = 2; locY = 3;
                        break;
                    case ItemBackPack.ItemType.Use_Potion:
                        locX = 1; locY = 0;
                        break;
                    case ItemBackPack.ItemType.Useless:
                        locX = 2; locY = 0;
                        break;
                    case ItemBackPack.ItemType.None:
                        locX = 2; locY = 0;
                        //pictureBox1.Visible = false;
                        break;
                    default:
                        locX = 2; locY = 0;
                        break;
                }
                treasureIcon.sprite = Sprite.Create(current, new Rect(BASE_SIZE * locX, BASE_SIZE * locY, BASE_SIZE, BASE_SIZE), new Vector2(0, 0));
            }
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
                    Debug.Log("PAPhase: " + player.FirstName + " to " + player.Target.FirstName);
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
            bool tempStop = false;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                for (int jj = 0; jj < ActiveList[ii].ActionCommandStackList.Count; jj++)
                {
                    if (ActiveList[ii].FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                    {
                        if (tempStop == false)
                        {
                            tempStop = true;
                            //this.back_labelBattleTurn.GetComponent<Image>().color = Color.white;
                            this.labelBattleTurn.color = Color.black;
                            System.Threading.Thread.Sleep(1000);
                            //this.back_labelBattleTurn.GetComponent<Image>().color = UnityColor.GhostWhite;
                            this.labelBattleTurn.color = Color.black;
                        }
                        PlayerAttackPhase(ActiveList[ii], ActiveList[ii].ActionCommandStackTarget[jj], TruthActionCommand.CheckPlayerActionFromString(ActiveList[ii].ActionCommandStackList[jj]), ActiveList[ii].ActionCommandStackList[jj], true, false, false);
                    }
                    else
                    {
                        ExecActionMethod(ActiveList[ii], ActiveList[ii].ActionCommandStackTarget[jj], TruthActionCommand.CheckPlayerActionFromString(ActiveList[ii].ActionCommandStackList[jj]), ActiveList[ii].ActionCommandStackList[jj]);
                    }
                }
                ActiveList[ii].ActionCommandStackList.Clear();
                ActiveList[ii].ActionCommandStackTarget.Clear();
            }

            this.Background.GetComponent<Image>().color = UnityColor.White;
            this.TimeStopText.gameObject.SetActive(false);
            this.labelBattleTurn.color = Color.black;
            this.TimeSpeedLabel.color = Color.black;
            this.lblTimerCount.color = Color.black;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                BackToNormalColor(ActiveList[ii]);
            }
        }

        private void GoToTimeStopColor(MainCharacter player)
        {
            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelCurrentLifePoint.color = UnityColor.Lightgreen;
            }
            else
            {
                player.labelCurrentLifePoint.color = Color.white;
            }
        }
        private void BackToNormalColor(MainCharacter player)
        {
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                {
                    player.labelName.color = UnityColor.DarkOrange;
                    player.labelCurrentInstantPoint.color = UnityColor.Gold;
                }
                else
                {
                    player.labelName.color = Color.black;
                }
            }
            else
            {
                player.labelName.color = Color.black;
            }

            player.ActionLabel.color = Color.black;
            player.CriticalLabel.color = Color.black;
            player.DamageLabel.color = Color.black;
            player.BuffPanel.GetComponent<Image>().color = UnityColor.GhostWhite;

            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelCurrentLifePoint.color = Color.green;
            }
            else
            {
                player.labelCurrentLifePoint.color = Color.black;
            }
        }

        private void CleanUpForBoss()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (IsPlayerEnemy(ActiveList[ii]))
                {
                    if ((((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss5) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.LastBoss) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss5))
                    {
                        ((TruthEnemyCharacter)ActiveList[ii]).CleanUpEffectForBoss();
                    }
                }
            }
        }

        private void UpkeepStep()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                // 装備品特殊効果
                ItemEffect(ActiveList[ii], ActiveList[ii].MainWeapon, MethodType.UpKeepStep);
                ItemEffect(ActiveList[ii], ActiveList[ii].SubWeapon, MethodType.UpKeepStep);
                ItemEffect(ActiveList[ii], ActiveList[ii].MainArmor, MethodType.UpKeepStep);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory, MethodType.UpKeepStep);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory2, MethodType.UpKeepStep);

                // OneAuthorityの効果
                if (ActiveList[ii].CurrentAbsoluteZero > 0)
                {
                }
                else
                {
                    if (ActiveList[ii].CurrentOneAuthority > 0)
                    {
                        ActiveList[ii].CurrentSkillPoint += (int)PrimaryLogic.OneAuthorityValue(ActiveList[ii], GroundOne.DuelMode);
                    }
                    // 各プレイヤーのスキル値を回復
                    ActiveList[ii].CurrentSkillPoint++;
                    UpdateSkillPoint(ActiveList[ii]);
                }

                // ペインフル・インサニティ効果
                if (ActiveList[ii].CurrentPainfulInsanity > 0 && ActiveList[ii].Dead == false)
                {
                    List<MainCharacter> group = new List<MainCharacter>();
                    if (IsPlayerAlly(ActiveList[ii]))
                    {
                        SetupEnemyGroup(ref group);
                    }
                    else
                    {
                        SetupAllyGroup(ref group);
                    }

                    for (int jj = 0; jj < group.Count; jj++)
                    {
                        double effectValue = PrimaryLogic.PainfulInsanityValue(ActiveList[ii], GroundOne.DuelMode);
                        effectValue = DamageIsZero(effectValue, group[jj], false);
                        UpdateBattleText(ActiveList[ii].FirstName + "は" + group[jj].FirstName + "の心へ直接的なダメージを発生させている。" + ((int)effectValue).ToString() + "のダメージ\r\n");
                        LifeDamage(effectValue, group[jj]);
                    }
                }

                // 毒効果
                if (ActiveList[ii].CurrentPoison > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.PoisonValue(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "は猛毒の効果により、ライフを削られていく。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
                // スリップ効果
                if (ActiveList[ii].CurrentSlip > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.SlipValue(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "の傷口はひどく、ライフが削られていく。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
                // フラッシュ・ブレイズ効果
                if (ActiveList[ii].CurrentFlashBlazeCount > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = ActiveList[ii].CurrentFlashBlazeFactor;
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "に閃光の炎が降り注ぐ。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                    ActiveList[ii].RemoveFlashBlaze();
                }
                // エンレイジ・ブラスト効果
                if (ActiveList[ii].CurrentEnrageBlast > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = ActiveList[ii].CurrentEnrageBlastFactor;
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ火の粉が降り注ぐ。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
                // ブレイジング・フィールド効果
                if (ActiveList[ii].CurrentBlazingField > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = ActiveList[ii].CurrentBlazingFieldFactor;
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ猛火が降り注ぐ。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
                // 炎ダメージ２効果
                if (ActiveList[ii].CurrentFireDamage2 > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.FireDamage2Value(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ猛火が降り注ぐ。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
                // 壱なる焔効果
                if (ActiveList[ii].CurrentIchinaruHomura > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.IchinaruHomuraValue(ec1); // ダメージ発生源はレギィンアーゼ
                    effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                    UpdateBattleText(ActiveList[ii].FirstName + "に焔火が降り注ぐ。\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].FirstName + "へ" + ((int)effectValue).ToString() + "のダメージ\r\n");
                }
            }

            // Bystanderはアップキープをメイン行動の主軸とする。
            if ((ec1 != null) && (ec1.FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS))
            {
                int totalNum = 0;
                if (FieldBuff[0].Count <= 0) totalNum++;
                if (FieldBuff[1].Count <= 0) totalNum++;
                if (FieldBuff[2].Count <= 0) totalNum++;
                if (FieldBuff[3].Count <= 0) totalNum++;
                if (FieldBuff[4].Count <= 0) totalNum++;
                //if (FieldBuff[5].Count <= 0) totalNum++;
                int choice = AP.Math.RandomInteger(totalNum);
                if (FieldBuff[0].Count > 0 && choice >= 0) { choice++; }
                if (FieldBuff[1].Count > 0 && choice >= 1) { choice++; }
                if (FieldBuff[2].Count > 0 && choice >= 2) { choice++; }
                if (FieldBuff[3].Count > 0 && choice >= 3) { choice++; }
                if (FieldBuff[4].Count > 0 && choice >= 4) { choice++; }
                if (FieldBuff[5].Count > 0 && choice >= 5) { choice++; }

                switch (choice)
                {
                    case 0:
                        ec1.ChoiceTimeSequenceBuff(0, FieldBuff, this.BattleTurnCount);
                        FieldBuff[0].ImageName = Database.BUFF_TIME_SEQUENCE_1;
                        FieldBuff[0].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_zougou");
                        break;
                    case 1:
                        ec1.ChoiceTimeSequenceBuff(1, FieldBuff, this.BattleTurnCount);
                        FieldBuff[1].ImageName = Database.BUFF_TIME_SEQUENCE_2;
                        FieldBuff[1].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_reikuu");
                        break;
                    case 2:
                        ec1.ChoiceTimeSequenceBuff(2, FieldBuff, this.BattleTurnCount);
                        FieldBuff[2].ImageName = Database.BUFF_TIME_SEQUENCE_3;
                        FieldBuff[2].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_seiei");
                        break;
                    case 3:
                        ec1.ChoiceTimeSequenceBuff(3, FieldBuff, this.BattleTurnCount);
                        FieldBuff[3].ImageName = Database.BUFF_TIME_SEQUENCE_4;
                        FieldBuff[3].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_zekken");
                        break;
                    case 4:
                        ec1.ChoiceTimeSequenceBuff(4, FieldBuff, this.BattleTurnCount);
                        FieldBuff[4].ImageName = Database.BUFF_TIME_SEQUENCE_5;
                        FieldBuff[4].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_ryokuei");
                        break;
                    case 5:
                        FieldBuff[5].Count = 10;
                        FieldBuff[5].ImageName = Database.BUFF_TIME_SEQUENCE_6;
                        FieldBuff[5].sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "bys_syuen");
                        break;
                    default:
                        break;
                }
            }
        }
        
        protected void TimeStopAlly(MainCharacter player)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            SetupAllyGroup(ref group);

            foreach (MainCharacter current in group)
            {
                PlayerSpellTimeStop(player, current, true);
            }
        }

        private int CurrentTimeStop = 0; // [後編必須]タイムストップを後編専用で書き直してください。本フラグは不要です。
        private void CleanUpStep()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                ActiveList[ii].CleanUpEffect(false, false);
                if (ActiveList[ii].FirstName == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    // 憎業「攻撃１」
                    if (this.FieldBuff[0].AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);

                        player.Pattern1++;

                        if (player.Pattern1 > 0) { player.ActionCommandStackList.Add(Database.BLAZING_FIELD); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 1) { player.ActionCommandStackList.Add(Database.SIGIL_OF_HOMURA); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 2) { player.ActionCommandStackList.Add(Database.IMMOLATE); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 3) { player.ActionCommandStackList.Add(Database.WORD_OF_MALICE); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 4) { player.ActionCommandStackList.Add(Database.PIERCING_FLAME); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 5) { player.ActionCommandStackList.Add(Database.DEMONIC_IGNITE); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 6) { player.ActionCommandStackList.Add(Database.DOOM_BLADE); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern1 > 7) { player.ActionCommandStackList.Add(Database.LAVA_ANNIHILATION); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // 零空「ディスペル」
                    if (this.FieldBuff[1].AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern2++;

                        if (player.Pattern2 > 0) { player.ActionCommandStackList.Add(Database.ABSOLUTE_ZERO); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 0) { player.ActionCommandStackList.Add(Database.DAMNATION); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 1) { player.ActionCommandStackList.Add(Database.AUSTERITY_MATRIX); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 2) { player.ActionCommandStackList.Add(Database.BLACK_CONTRACT); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 3) { player.ActionCommandStackList.Add(Database.TRANQUILITY); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 4) { player.ActionCommandStackList.Add(Database.HYMN_CONTRACT); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 5) { player.ActionCommandStackList.Add(Database.DISPEL_MAGIC); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        if (player.Pattern2 > 6) { player.ActionCommandStackList.Add(Database.TRANSCENDENT_WISH); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // 盛栄「防御」
                    if (this.FieldBuff[2].AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern3++;

                        if (player.Pattern3 > 0) { player.ActionCommandStackList.Add(Database.PROTECTION); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 1) { player.ActionCommandStackList.Add(Database.MIRROR_IMAGE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 2) { player.ActionCommandStackList.Add(Database.DEFLECTION); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 3) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 4) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 5) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 6) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 7) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 8) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 9) { player.ActionCommandStackList.Add(Database.HOLY_BREAKER); player.ActionCommandStackTarget.Add(player); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // 絶剣「攻撃２」
                    if (this.FieldBuff[3].AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern4++;

                        if (player.Pattern4 <= 2) { player.ActionCommandStackList.Add(Database.STRAIGHT_SMASH); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        else if (player.Pattern4 <= 4) { player.ActionCommandStackList.Add(Database.DOUBLE_SLASH); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        else if (player.Pattern4 <= 8) { player.ActionCommandStackList.Add(Database.SILENT_RUSH); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        else if (player.Pattern4 <= 16) { player.ActionCommandStackList.Add(Database.CARNAGE_RUSH); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }
                        else if (player.Pattern4 <= 32) { player.ActionCommandStackList.Add(Database.SOUL_EXECUTION); player.ActionCommandStackTarget.Add(player.Targetting(GroundOne.MC, GroundOne.SC, GroundOne.TC)); }

                        if (player.Pattern4 > 0) { player.ActionCommandStackList.Add(Database.SAINT_POWER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 1) { player.ActionCommandStackList.Add(Database.FLAME_AURA); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 2) { player.ActionCommandStackList.Add(Database.FROZEN_AURA); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 3) { player.ActionCommandStackList.Add(Database.GALE_WIND); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 4) { player.ActionCommandStackList.Add(Database.WORD_OF_FORTUNE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 5) { player.ActionCommandStackList.Add(Database.SIN_FORTUNE); player.ActionCommandStackTarget.Add(player); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // 緑永「回復」
                    if (this.FieldBuff[4].AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern5++;

                        if (player.Pattern5 > 0) { player.ActionCommandStackList.Add(Database.NOURISH_SENSE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 0) { player.ActionCommandStackList.Add(Database.FRESH_HEAL); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 1) { player.ActionCommandStackList.Add(Database.WORD_OF_LIFE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 2) { player.ActionCommandStackList.Add(Database.SACRED_HEAL); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 3) { player.ActionCommandStackList.Add(Database.CELESTIAL_NOVA); player.ActionCommandStackTarget.Add(player); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                //    // 終焉
                //    if (FieldBuff6.AbstractCountDownBuff())
                //    {
                //        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                //        player.Pattern6++;
                //        if (player.Pattern6 > 0) { player.ActionCommandStackList.Add(Database.ZETA_EXPLOSION); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }

                //        TimeStopAlly(ActiveList[ii]);
                //        //if (this.actionCommandStackList.Count == 0)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SHADOW_PACT);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 1)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 2)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ETERNAL_PRESENCE);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 3)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PSYCHIC_TRANCE);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 4)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 5)
                //        //{
                //        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLUE_DRAGON_WILL);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 6)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 7)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.TRANQUILITY);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 8)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.AUSTERITY_MATRIX);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 9)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ABSOLUTE_ZERO);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 10)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                //        //}
                //        //else if (this.actionCommandStackList.Count == 11)
                //        //{
                //        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);
                //        //}

                //        TimeStopAlly(ActiveList[ii]);
                //    }
                }
            }

            // 前編からの引継ぎには不足してるコーディング。書き直し必要。
            if (CurrentTimeStop > 0)
            {
                CurrentTimeStop--;
            }
        }

        private void AfterBattleEffect()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii].CurrentEternalDroplet > 0)
                    {
                        double effectValue = PrimaryLogic.EternalDropletValue_A(ActiveList[ii]);
                        if (ActiveList[ii].CurrentNourishSense > 0)
                        {
                            effectValue = effectValue * PrimaryLogic.NourishSenseValue(ActiveList[ii]);
                        }
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("永遠を示す理が、" + ActiveList[ii].FirstName + "へ生命力を注ぎ込んでいる。" + ((int)effectValue).ToString() + "ライフ回復\r\n");
                        ActiveList[ii].CurrentLife += (int)(effectValue);
                        UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);

                        double effectValue2 = PrimaryLogic.EternalDropletValue_B(ActiveList[ii]);
                        effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
                        UpdateBattleText(((int)effectValue2).ToString() + "マナ回復\r\n");
                        ActiveList[ii].CurrentMana += (int)effectValue;
                        UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
                    }

                    if (ActiveList[ii].CurrentWordOfLife > 0)
                    {
                        double effectValue = PrimaryLogic.WordOfLifeValue(ActiveList[ii], GroundOne.DuelMode);
                        if (ActiveList[ii].CurrentNourishSense > 0)
                        {
                            effectValue = effectValue * PrimaryLogic.NourishSenseValue(ActiveList[ii]);
                        }
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("大自然から" + ActiveList[ii].FirstName + "へ力強い脈動が行き渡る。" + ((int)effectValue).ToString() + "ライフ回復\r\n");
                        ActiveList[ii].CurrentLife += (int)(effectValue);
                        UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);
                    }

                    if (ActiveList[ii].CurrentEverDroplet > 0 && ActiveList[ii].Dead == false)
                    {
                        double effectValue = PrimaryLogic.EverDropletValue(ActiveList[ii]);
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("生命の根源から" + ActiveList[ii].FirstName + "へ無限のイメージが行き渡る。" + ((int)effectValue).ToString() + "マナ回復\r\n");
                        ActiveList[ii].CurrentMana += (int)effectValue;
                        UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
                    }

                    if (ActiveList[ii].CurrentBlackContract > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
                        effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                        UpdateBattleText(ActiveList[ii].FirstName + "は悪魔への代償を支払う。" + ((int)effectValue).ToString() + "ライフが削り取られる。\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if (ActiveList[ii].CurrentHymnContract > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
                        effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                        UpdateBattleText(ActiveList[ii].FirstName + "は天使との締結により、魂の代金を支払う。" + ((int)effectValue).ToString() + "ライフが削り取られる。\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if (ActiveList[ii].CurrentDamnation > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = PrimaryLogic.DamnationValue(ActiveList[ii]);
                        effectValue = DamageIsZero(effectValue, ActiveList[ii], false);
                        UpdateBattleText("黒が" + ActiveList[ii].FirstName + "の存在している空間を歪ませてくる。" + ((int)effectValue).ToString() + "のダメージ\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if ((ActiveList[ii].Accessory != null) && (ActiveList[ii].Accessory.Name == Database.COMMON_MUKEI_SAKAZUKI))
                    {
                        if (ActiveList[ii].PoolLifeConsumption > 0)
                        {
                            double effectValue = (double)(ActiveList[ii].PoolLifeConsumption) / 2.0F;
                            double effectValue2 = (double)(ActiveList[ii].PoolManaConsumption) / 2.0F;
                            double effectValue3 = (double)(ActiveList[ii].PoolSkillConsumption) / 2.0F;
                            effectValue = GainIsZero(effectValue, ActiveList[ii]);
                            effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
                            effectValue3 = GainIsZero(effectValue3, ActiveList[ii]);
                            UpdateBattleText(Database.COMMON_MUKEI_SAKAZUKI + "から" + ActiveList[ii].FirstName + "へ生命の水が湧き出てくる。\r\n");
                            UpdateBattleText(ActiveList[ii].FirstName + "のライフが" + ((int)effectValue).ToString() + "回復、マナが" + ((int)effectValue2).ToString() + "回復、スキルポイントが" + ((int)effectValue3).ToString() + "回復\r\n");
                            ActiveList[ii].CurrentLife += (int)effectValue;
                            ActiveList[ii].CurrentMana += (int)effectValue2;
                            ActiveList[ii].CurrentSkillPoint += (int)effectValue3;
                            UpdateLife(ActiveList[ii], (double)effectValue, true, true, 0, false);
                            UpdateMana(ActiveList[ii], (double)effectValue2, true, true, 0);
                            UpdateSkillPoint(ActiveList[ii], (double)effectValue3, true, true, 0);
                        }
                    }
                }
            }
        }

        private void Beginning()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (this.LifeCountBattle)
                {
                    PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], Database.LIFE_COUNT);
                }
                // 装備品特殊効果
                ItemEffect(ActiveList[ii], ActiveList[ii].MainWeapon, MethodType.Beginning);
                ItemEffect(ActiveList[ii], ActiveList[ii].MainArmor, MethodType.Beginning);
                ItemEffect(ActiveList[ii], ActiveList[ii].SubWeapon, MethodType.Beginning);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory, MethodType.Beginning);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory2, MethodType.Beginning);
                if (ActiveList[ii].MainWeapon != null)
                {
                    if (ActiveList[ii].MainWeapon.Name == Database.RARE_DOOMBRINGER)
                    {
                        if (ActiveList[ii].CurrentGaleWind <= 0)
                        {
                            PlayerSpellGaleWind(ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].MainWeapon.Name == Database.EPIC_MEIKOU_DOOMBRINGER)
                    {
                        if (ActiveList[ii].CurrentGaleWind <= 0)
                        {
                            PlayerSpellGaleWind(ActiveList[ii]);
                        }
                        ActiveList[ii].BeforePA = MainCharacter.PlayerAction.UseSpell;
                        ActiveList[ii].BeforeUsingItem = String.Empty;
                        ActiveList[ii].BeforeSkillName = String.Empty;
                        ActiveList[ii].BeforeSpellName = Database.GALE_WIND;
                        ActiveList[ii].BeforeTarget = ActiveList[ii];
                        //ActiveList[ii].BeforeTarget2 = null; // 記憶要素はない
                    }
                }

                if (ActiveList[ii].Accessory != null)
                {
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                    {
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.COMMON_DEVIL_SEALED_VASE)
                    {
                        this.groupChooseCommand.SetActive(true);
                        this.Filter.SetActive(true);
                    }
                    if (ActiveList[ii].Accessory.Name == Database.RARE_VOID_HYMNSONIA)
                    {
                        PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], Database.ITEMCOMMAND_VOID_HYMNSONIA);
                    }
                }
                if (ActiveList[ii].Accessory2 != null)
                {
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                    {
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.COMMON_DEVIL_SEALED_VASE)
                    {
                        this.groupChooseCommand.SetActive(true);
                        this.Filter.SetActive(true);
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.RARE_VOID_HYMNSONIA)
                    {
                        PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], Database.ITEMCOMMAND_VOID_HYMNSONIA);
                    }
                }
            }
        }

        private void ItemEffect(MainCharacter player, ItemBackPack item, MethodType methodType)
        {
            if (player != null && player.Dead) { return; }

            // アイテム効果はAfterBattleEffectやUpkeepではターン終了時なので、全ての効果を発動してもよいが
            // Beginningフェーズでは、敵を対象とする効果が戦闘開始時に発動してはならない。
            // Beginningフェーズでは装備者本人に発動されるものだけが発動対象となる。
            if (item != null)
            {
                if (item.Name == Database.EPIC_ORB_GROW_GREEN)
                {
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.EverGrowGreenValue(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                {
                    if (player.CurrentRiseOfImage <= 0)
                    {
                        PlayerSpellRiseOfImage(player, player);
                    }
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                {
                    if (player.CurrentWordOfLife <= 0)
                    {
                        PlayerSpellWordOfLife(player, player);
                    }
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_SWORD)
                {
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.ShuvalzFloreSwordValue(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_SHIELD)
                {
                    PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.ShuvalzFloreShieldValue(player), 0, String.Empty, 5002);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ARMOR)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.ShuvalzFloreArmorValue(player), 0, String.Empty, 5003);
                }
                if (item.Name == Database.EPIC_SHEZL_MYSTIC_FORTUNE)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.ShezlMysticFortuneValue(player), 0, String.Empty, 5003);
                }
                if (item.Name == Database.EPIC_EZEKRIEL_ARMOR_SIGIL)
                {
                    PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_A(player), 0, String.Empty, 5002);
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_B(player), 0, String.Empty, 5003);
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_C(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.RARE_ANGEL_CONTRACT)
                {
                    if (player.CurrentPreStunning > 0 || player.CurrentStunning > 0 || player.CurrentSilence > 0 ||
                        player.CurrentPoison > 0 || player.CurrentTemptation > 0 || player.CurrentFrozen > 0 ||
                        player.CurrentParalyze > 0 || player.CurrentSlow > 0 || player.CurrentBlind > 0)
                    {
                        UpdateBattleText(player.FirstName + "が装備している天使の契約書が光り輝いた！\r\n", 1000);
                        player.RemovePreStunning();
                        player.RemoveStun();
                        player.RemoveSilence();
                        player.RemovePoison();
                        player.RemoveTemptation();
                        player.RemoveFrozen();
                        player.RemoveParalyze();
                        player.RemoveSlow();
                        player.RemoveBlind();
                        UpdateBattleText(player.FirstName + "にかかっている負の影響が全て解除された。\r\n");
                    }
                }
                if (item.Name == Database.RARE_ARCHANGEL_CONTRACT)
                {
                    if (player.CurrentPhysicalAttackDown > 0 || player.CurrentPhysicalDefenseDown > 0 ||
                        player.CurrentMagicAttackDown > 0 || player.CurrentMagicDefenseDown > 0 ||
                        player.CurrentSpeedDown > 0 || player.CurrentReactionDown > 0 || player.CurrentPotentialDown > 0)
                    {
                        UpdateBattleText(player.FirstName + "が装備している大天使の契約書が光り輝いた！\r\n", 1000);
                        player.RemovePhysicalAttackDown();
                        player.RemovePhysicalDefenseDown();
                        player.RemoveMagicAttackDown();
                        player.RemoveMagicDefenseDown();
                        player.RemoveSpeedDown();
                        player.RemoveReactionDown();
                        player.RemovePotentialDown();
                        UpdateBattleText(player.FirstName + "の能力低下状態が解除された！");
                    }
                }

                if (item.Name == Database.COMMON_ELDER_PERSPECTIVE_GRASS && methodType != MethodType.Beginning)
                {
                    if (player.Target != null)
                    {
                        BuffDownBattleSpeed(player.Target, 1.0F);
                        BuffDownBattleReaction(player.Target, 1.0F);
                    }
                }

                if (item.Name == Database.RARE_DEVIL_SUMMONER_TOME && methodType != MethodType.Beginning)
                {
                    if (player.Target != null)
                    {
                        double effectValue = PrimaryLogic.DevilSummonerTomeValue(player, GroundOne.DuelMode);
                        AbstractMagicDamage(player, player.Target, 0, effectValue, 0, Database.SOUND_FIREBALL, 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random);
                    }
                }

                if (item.Name == Database.EPIC_ETERNAL_HOMURA_RING)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.EternalHomuraRingValue_A(player), 0, String.Empty, 5003);
                }
            }
        }

        private void UpdateUseItemGauge()
        {
            // ゲージバー値を更新
            if (currentItemGauge < MAX_ITEM_GAUGE)
            {
                currentItemGauge++;
            }

            // ゲージバー表示更新
            float dx = (float)currentItemGauge / (float)MAX_ITEM_GAUGE;
            UseItemText.text = currentItemGauge.ToString();
            UseItemGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
        }
        
        private void UpdateTurnEnd(bool cancelCounterClear = false)
        {
            this.BattleTurnCount++;
            this.labelBattleTurn.text = "ターン " + BattleTurnCount.ToString();
            if (cancelCounterClear == false) { this.BattleTimeCounter = 0; }
        }
        
        private void StackInTheCommandEnd()
        {
            if (this.StackNumber > 0)
            {
                this.stackActivePlayer.RemoveAt(this.stackActivePlayer.Count - 1);
                this.cumulativeCounter.RemoveAt(this.cumulativeCounter.Count - 1);
                this.StackNumber--;
            }
            else
            {
                this.StackNumber = -1;
                this.stackActivePlayer.Clear();
                this.cumulativeCounter.Clear();
                this.NowStackInTheCommand = false;
                CompleteInstantAction();
            }

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


        /// <summary>
        /// カウンター行為が発動成功するかどうかを判定するメソッド
        /// </summary>
        /// <param name="player">対象元プレイヤー</param>
        /// <param name="target">対象先ターゲット</param>
        /// <param name="messageNumber">キャラクターセリフ番号</param>
        /// <returns>カウンター成功ならTrue、失敗ならFalse</returns>
        private bool JudgeSuccessOfCounter(MainCharacter player, MainCharacter target, int messageNumber, ref string factor)
        {
            if (player.PA == MainCharacter.PlayerAction.UseSpell && (TruthActionCommand.CannotBeCountered(player.CurrentSpellName)) ||
                     player.StackPlayerAction == MainCharacter.PlayerAction.UseSpell && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSpellName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.PA == MainCharacter.PlayerAction.UseSkill && (TruthActionCommand.CannotBeCountered(player.CurrentSkillName)) ||
                     player.StackPlayerAction == MainCharacter.PlayerAction.UseSkill && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSkillName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.PA == MainCharacter.PlayerAction.Archetype && (TruthActionCommand.CannotBeCountered(player.CurrentArchetypeName)) ||
                player.StackPlayerAction == MainCharacter.PlayerAction.Archetype && (TruthActionCommand.CannotBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentArchetypeName + "はカウンター出来ない！！！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = String.Empty;
                return false;
            }
            else if (player.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + player.FirstName + "は無効化を無効にするオーラによって護られている！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = Database.NOTHING_OF_NOTHINGNESS;
                return false;
            }
            else if (player.CurrentHymnContract > 0)
            {
                UpdateBattleText(player.FirstName + "は天使の契約により保護されており、カウンターを無視した！\r\n");
                AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_COUNTER);
                factor = Database.HYMN_CONTRACT;
                return false;
            }
            else
            {
                UpdateBattleText(target.GetCharacterSentence(messageNumber));
                AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_COUNTER);
                factor = String.Empty;
                return true;
            }
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

        /// <summary>
        /// プレイヤーとターゲットはパーティメンバーか敵メンバーかを判別するメソッド
        /// </summary>
        /// <param name="player">対象元プレイヤー</param>
        /// <param name="target">対象先ターゲット</param>
        /// <returns>敵メンバーならTrue、味方メンバーならFalse</returns>
        private bool DetectOpponentParty(MainCharacter player, MainCharacter target)
        {
            // 可読性より、演算論理集約を重視した記述
            if (((player == ec1 || player == ec2 || player == ec3) &&
                 (target == GroundOne.MC || target == GroundOne.SC || target == GroundOne.TC))
                 ||
                 ((player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC) &&
                  (target == ec1 || target == ec2 || target == ec3)))
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
                    if ((ActiveList[ii].FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3) &&
                        (!((TruthEnemyCharacter)ActiveList[ii]).DetectDeath))
                    {
                        ((TruthEnemyCharacter)ActiveList[ii]).DetectDeath = true;
                        UpdateBattleText(ActiveList[ii].FirstName + "は死の至る刹那、深淵の防壁を作りだした！！\r\n");
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "深淵の防壁");
                        ActiveList[ii].CurrentLife = 1;
                        UpdateLife(ActiveList[ii]);
                        ActiveList[ii].CurrentTheAbyssWall = Database.INFINITY;
                        ActiveList[ii].ActivateBuff(ActiveList[ii].pbTheAbyssWall, Database.BaseResourceFolder + Database.THE_ABYSS_WALL, Database.INFINITY);
                    }
                    else if (ActiveList[ii].CurrentGenseiTaima > 0)
                    {
                        ActiveList[ii].RemoveGenseiTaima();
                        UpdateBattleText(ActiveList[ii].FirstName + "に対して退魔の効果が発動し、致死の狭間で生き残った！！\r\n");
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "致死回避！");
                        ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife / 2;
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentStanceOfDeath > 0)
                    {
                        ActiveList[ii].RemoveStanceOfDeath();
                        UpdateBattleText(ActiveList[ii].FirstName + "は致死の狭間で生き残った！！\r\n");
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, false, false, "致死回避！");
                        ActiveList[ii].CurrentLife = 1;
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentShadowBible > 0)
                    {
                        ActiveList[ii].RemoveShadowBible();
                        UpdateBattleText(ActiveList[ii].FirstName + "は致死の狭間でみなぎる生命力を感じ取った！！\r\n");
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "致死回避！");
                        ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife;
                        UpdateLife(ActiveList[ii]);
                        NowNoResurrection(ActiveList[ii], ActiveList[ii], 999);
                    }
                    else if (ActiveList[ii].CurrentAfterReviveHalf > 0)
                    {
                        ActiveList[ii].RemoveAfterReviveHalf();
                        UpdateBattleText(ActiveList[ii].FirstName + "は致死の狭間で生き残った！！\r\n");
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "致死回避！");
                        ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife / 2.0f);
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentLifeCount > 0)
                    {
                        ActiveList[ii].CurrentLifeCountValue--;
                        UpdateBattleText(ActiveList[ii].FirstName + "の生命力が１つ削られた！！\r\n");
                        if (ActiveList[ii].CurrentLifeCountValue <= 0)
                        {
                            UpdateBattleText(ActiveList[ii].GetCharacterSentence(217));
                            ActiveList[ii].RemoveLifeCount();
                            ActiveList[ii].DeadPlayer();
                        }
                        else
                        {
                            UpdateBattleText(ActiveList[ii].GetCharacterSentence(216));
                            System.Threading.Thread.Sleep(1000);
                            AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "生命復活");
                            ActiveList[ii].RemoveDebuffEffect();
                            ActiveList[ii].RemoveDebuffParam();
                            ActiveList[ii].RemoveDebuffSkill();
                            ActiveList[ii].RemoveDebuffSpell();
                            ActiveList[ii].ChangeLifeCountStatus(ActiveList[ii].CurrentLifeCount);
                            ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife);
                            UpdateLife(ActiveList[ii]);
                            //ActiveList[ii].labelLife.Update();
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    else if (CheckResurrectWithItem(ActiveList[ii], Database.RARE_TAMATEBAKO_AKIDAMA))
                    {
                        UpdateBattleText(Database.RARE_TAMATEBAKO_AKIDAMA + "が淡く光り始めた！\r\n", 500);
                        AnimationDamage(0, ActiveList[ii], 0, Color.black, true, false, "復活");

                        UpdateBattleText(ActiveList[ii].FirstName + "は致死の狭間で生き残った！！\r\n");
                        ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife * 0.1f);
                        UpdateLife(ActiveList[ii]);
                    }
                    else
                    {
                        ActiveList[ii].DeadPlayer();
                    }
                }

                // TranscendentWishの効果が解除された時即死する条件を追加。
                if (ActiveList[ii].DeadSignForTranscendentWish)
                {
                    UpdateBattleText(ActiveList[ii].FirstName + "のTranscendentWishの効果が切れた！生命の源が失われていく・・・\r\n");
                    UpdateLife(ActiveList[ii], ActiveList[ii].CurrentLife, false, true, 0, false);
                    ActiveList[ii].CurrentLife = 0;
                    UpdateLife(ActiveList[ii], 0, false, false, 0, false);
                    ActiveList[ii].DeadPlayer();
                    System.Threading.Thread.Sleep(1000);
                }

                CheckChaosDesperate(ActiveList[ii]);
            }

            if (PlayerPartyDeathCheck() || EnemyPartyDeathCheck())
            {
                this.BattleEndFlag = true;
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

            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ||
                this.ActionInstantMode)
            {
                Debug.Log("Right Click Action");
                if (CheckBattlePlaying())
                {
                    return;
                }
                if ((player.CurrentInstantPoint < player.MaxInstantPoint) &&
                    (BattleActionCommand != Database.ARCHETYPE_EIN) &&
                    (BattleActionCommand != Database.ARCHETYPE_RANA) &&
                    (BattleActionCommand != Database.ARCHETYPE_OL) &&
                    (BattleActionCommand != Database.ARCHETYPE_VERZE))
                {
                    UpdateBattleText(player.GetCharacterSentence(218));
                    return;
                }

                if (GroundOne.WE.AvailableInstantCommand == false)
                {
                    return;
                }

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
                    Debug.Log("CheckNotInstant");
                    UpdateBattleText(player.GetCharacterSentence(128));
                    return;
                }

                if (CheckInstantTarget(BattleActionCommand)) // インスタント対象の場合
                {
                    Debug.Log("CheckInstantTarget");

                    if (this.NowStackInTheCommand)
                    {
                        // スタック・イン・ザ・コマンド中はインスタント対象として発動するため、ここではスルー
                        // ただし、事前にコスト消費チェックが入る。
                        if (player.CurrentSkillPoint < TruthActionCommand.Cost(BattleActionCommand, player) &&
                            TruthActionCommand.GetAttribute(BattleActionCommand) == TruthActionCommand.Attribute.Skill)
                        {
                            if (EffectCheckDarknessCoin(player))
                            {
                                // 代償を支払ったため、スルー
                            }
                            else if (player.CurrentBlackContract > 0)
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
                Debug.Log("call InstantAttackPhase");
                InstantAttackPhase(BattleActionCommand);
            }
            else if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftShift) == false || Input.GetKey(KeyCode.LeftShift) == false)
            {
                Debug.Log("command normal select: " + BattleActionCommand);
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
                    // 自分自身が対象の場合、指定対象選択は不要
                    this.currentTargetedPlayer.Target2 = this.currentTargetedPlayer;
                    this.currentTargetedPlayer.ReserveBattleCommand = string.Empty;
                }
                else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
                {
                    PlayerActionSet(this.currentTargetedPlayer);
                    // 敵全員、味方全員、敵味方全員が対象の場合、何かをターゲットしなおす事はしない。
                    this.currentTargetedPlayer.ReserveBattleCommand = string.Empty;
                }
                else
                {
                    NowSelectingTargetON();
                }
            }
        }

        private void NowSelectingTargetON()
        {
            this.NowSelectingTarget = true;
            this.Background.GetComponent<Image>().color = UnityColor.Silver;
        }
        private void NowSelectingTargetOFF()
        {
            this.NowSelectingTarget = false;
            this.Background.GetComponent<Image>().color = Color.white;
        }

        public void buttonTargetPlayer_Click(Button sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_PLAYER, sender.name, String.Empty);
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
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

                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) 
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ActiveList[ii], TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                            break;
                        }
                    }
                }
                else
                {
                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }

                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton))
                        {
                            if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                                (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                            {
                                this.currentTargetedPlayer.Target2 = ActiveList[ii];
                            }
                            else
                            {
                                this.currentTargetedPlayer.Target = ActiveList[ii];
                            }
                            PlayerActionSet(this.currentTargetedPlayer);
                            break;
                        }
                    }
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                NowSelectingTargetOFF();
            }
        }

        public void buttonTargetEnemy_Click(Button sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_ENEMY, sender.name, String.Empty);
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
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
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton)) 
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ActiveList[ii], TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                            break;
                        }
                    }
                }
                else
                {
                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }

                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((Button)sender).Equals(ActiveList[ii].MainObjectButton))
                        {
                            if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                                (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                            {
                                this.currentTargetedPlayer.Target2 = ActiveList[ii];
                            }
                            else
                            {
                                this.currentTargetedPlayer.Target = ActiveList[ii];
                            }
                            PlayerActionSet(this.currentTargetedPlayer);
                            break;
                        }
                    }
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                NowSelectingTargetOFF();
            }
        }


        public void BattleStart_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_BATTLE_START, String.Empty, String.Empty);
            const string NOW_BATTLE = "戦闘中・・・";
            const string NOW_DUEL = "DUEL中・・・";
            const string NOW_STOP = "戦闘停止";

            if ((BattleStart.text == NOW_BATTLE) ||
                (BattleStart.text == NOW_DUEL))
            {
                if (GroundOne.DuelMode)
                {
                    // DUELでは途中一旦停止は出来ない事とする。
                }
                else
                {
                    BattleStart.text = NOW_STOP;
                    tempStopFlag = true;
                }
            }
            else
            {
                if (GroundOne.TutorialMode)
                {
                    back_TutorialMessage.gameObject.SetActive(false);
                }

                BattleStart.text = NOW_BATTLE;
                tempStopFlag = false;
                gameStart = true;
            }
        }

        private void UpdateBattleText(string text)
        {
            if (txtBattleMessage != null)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, text);
            }
        }
        private void UpdateBattleText(string text, int sleepTime)
        {
            if (txtBattleMessage != null)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, text);
            }

            if (sleepTime > 0)
            {
                System.Threading.Thread.Sleep(sleepTime);
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

        public void tapBattleClose()
        {
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }

        public void tapActionButton(Button obj)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_COMMAND1, obj.name, String.Empty);
            ActionCommand(false, GroundOne.MC, obj.name);
        }
        public void tapActionButton2(Button obj)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_COMMAND1, obj.name, String.Empty);
            ActionCommand(false, GroundOne.SC, obj.name);
        }
        public void tapActionButton3(Button obj)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_COMMAND1, obj.name, String.Empty);
            ActionCommand(false, GroundOne.TC, obj.name);
        }

        public void tapBattleSetting()
        {
            SceneDimension.CallTruthBattleSetting(this);
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
        public void PointerDownPanel()
        {
            if (this.ActionInstantMode == false)
            {
                this.ActionInstantMode = true;
                InstantModePanel.SetActive(true);
                if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null)
                {
                    GroundOne.MC.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                    GroundOne.MC.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                }
                if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null)
                {
                    GroundOne.SC.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                    GroundOne.SC.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                }
                if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null)
                {
                    GroundOne.TC.ActionButtonPanel.GetComponent<Image>().color = Color.black;
                    GroundOne.TC.ManaSkillPanel.GetComponent<Image>().color = Color.black;
                }
            }
        }
        public void PointerUpPanel()
        {
            if (this.ActionInstantMode)
            {
                this.ActionInstantMode = false;
                InstantModePanel.SetActive(false);
                if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null)
                {
                    GroundOne.MC.ActionButtonPanel.GetComponent<Image>().color = Color.white;
                    SelectPlayerPanelColor(GroundOne.MC);
                }
                if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null)
                {
                    GroundOne.SC.ActionButtonPanel.GetComponent<Image>().color = Color.white;
                    SelectPlayerPanelColor(GroundOne.SC);
                }
                if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null)
                {
                    GroundOne.TC.ActionButtonPanel.GetComponent<Image>().color = Color.white;
                    SelectPlayerPanelColor(GroundOne.TC);
                }
            }
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

        public void UseItem_Click(Text sender)
        {
            MainCharacter player = this.currentPlayer;
            if (player.Dead)
            {
                txtBattleMessage.text = "【" + player.FirstName + "は死んでしまっているため、アイテムが使えない。】";
                return;
            }

            // バックパック画面を開いて、消耗品アイテムを使用する。
            Debug.Log("ItemGauge: " + UseItemGauge.rectTransform.localScale.x);
            if (UseItemGauge.rectTransform.localScale.x < 1.0f)
            {
                if (GroundOne.MC.Dead == false)
                {
                    UpdateBattleText(GroundOne.MC.GetCharacterSentence(125));
                }
                else if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC.Dead == false)
                {
                    UpdateBattleText(GroundOne.SC.GetCharacterSentence(125));
                }
                else if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC.Dead == false)
                {
                    UpdateBattleText(GroundOne.TC.GetCharacterSentence(125));
                }
                return;
            }

            int currentNumber = 0;
            for (int ii = 0; ii < backpack.Length; ii++)
            {
                if (backpack[ii].Equals(sender))
                {
                    currentNumber = ii;
                    Debug.Log("currentNumber is " + currentNumber.ToString());
                    break;
                }
            }

            Method.UseItem(player, sender.text, currentNumber, txtBattleMessage);
            UpdateLife(player);
            UpdateMana(player);
            UpdateSkillPoint(player);
            UpdateInstantPoint(player);
            Method.UpdateBackPackLabel(player, back_Backpack, backpack, backpackStack, backpackIcon);

            this.currentItemGauge = 0;
            UpdateUseItemGauge();
        }

        // 通常攻撃を抽象化したロジック。通常攻撃やストレートスマッシュは全てここに含まれる。
        private void AbstractMagicAttack(MainCharacter player, MainCharacter target, string command, double value)
        {
            if (player.CurrentShadowPact > 0) { value = value * 1.3f; }
            target.CurrentLife -= (int)value;
            if (target.CurrentLife < 0) { target.CurrentLife = 0; }
            UpdateLife(target);
            UpdateBattleText(player.labelName.text + " " + command + " " + ((int)value).ToString() + " \n");
        }
        // 通常攻撃
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense, bool ignoreDoubleAttack)
        {
            return PlayerNormalAttack(player, target, magnification, 0, ignoreDefense, false, 0, 0, string.Empty, -1, ignoreDoubleAttack, CriticalType.Random);
        }
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, int crushingBlow, bool ignoreDefense, bool skipCounterPhase, double atkBase, int interval, string soundName, int textNumber, bool ignoreDoubleAttack, CriticalType critical)
        {
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

                // ミス判定
                if (CheckDodge(player, target, false))
                {
                    // 回避された場合、ダメージは発生しない。デフレクション判定なども同様。
                    // 一番下にサブウェポンによる二回攻撃判定があるので、それは別とする。
                    AnimationDamage(0, target, 0, Color.black, true, false, String.Empty);
                }
                else if (CheckBlindMiss(player, target))
                {
                    // 暗闇により攻撃を外した場合ダメージは発生しない。デフレクション判定なども同様。
                    // 一番下にサブウェポンによる二回攻撃判定があるので、それは別とする。
                    AnimationDamage(0, target, 0, Color.black, true, false, String.Empty);
                }
                else
                {
                    double damage = 0;
                    // ダメージ加算
                    if (atkBase == 0)
                    {
                        if (ii == 0)
                        {
                            damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode);
                        }
                        else
                        {
                            damage = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0, 0, 0, 1.0F, GroundOne.DuelMode);
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
                    damage -= PrimaryLogic.PhysicalDefenseValue(target, PrimaryLogic.NeedType.Random, GroundOne.DuelMode);
                    if (damage <= 0.0f) damage = 0.0f;

                    if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = (int)((float)damage / 1.5F);
                    }
                    if (target.CurrentEternalPresence > 0 && player.CurrentTruthVision <= 0)
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
                        damage = damage * PrimaryLogic.CriticalDamageValue(player, GroundOne.DuelMode);
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

                    // StanceOfMysticによる効果
                    if (target.CurrentStanceOfMysticValue > 0) // && ignoreTargetDefense == false) ダメージ置き換えはignoreTargetDefenseの対象外でダメージを０にする。
                    {
                        target.CurrentStanceOfMysticValue--;
                        target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMystic);
                        damage = 0;
                        AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_AVOID);
                        return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
                    }

                    // HardestParryによる効果
                    if (target.CurrentHardestParry) // && ignoreTargetDefense == false) ダメージ置き換えはignoreTargetDefenseの対象外でダメージを０にする。
                    {
                        target.CurrentHardestParry = false;
                        damage = 0;
                        AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_AVOID);
                        return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
                    }

                    // デフレクション効果はクリティカル値も反映させる
                    // デフレクションによる物理攻撃反射
                    if (skipCounterPhase)
                    {
                        if (target.CurrentDeflection > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(62));
                            AnimationDamage(0, target, 0, Color.black, true, false, Database.FAIL_DEFLECTION);
                            target.RemoveDeflection();
                        }
                    }
                    else
                    {
                        if (target.CurrentDeflection > 0)
                        {
                            AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_DEFLECTION);
                            damage = DamageIsZero(damage, player, ignoreDefense);
                            LifeDamage(damage, player);
                            target.RemoveDeflection();
                            return true;
                        }
                    }

                    // StaticBarrierによる効果
                    if (target.CurrentStaticBarrier > 0 && ignoreDefense == false)
                    {
                        target.CurrentStaticBarrierValue--;
                        target.ChangeStaticBarrierStatus(target.CurrentStaticBarrier);
                        damage = damage * 0.5f;
                    }
                    
                    // ダメージ０変換
                    damage = DamageIsZero(damage, target, ignoreDefense);

                    // スケール・オブ・ブルーレイジによる効果
                    if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_SCALE_BLUERAGE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.ScaleOfBlueRageValue(player))
                        {
                            AnimationDamage(0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                            damage = 0;
                        }
                    }
                    // スライド・スルー・シールドによる効果
                    if ((target.SubWeapon != null) && (target.SubWeapon.Name == Database.RARE_SLIDE_THROUGH_SHIELD))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SlideThroughShieldValue(player))
                        {
                            AnimationDamage(0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                            damage = 0;
                        }
                    }

                    // メッセージ更新
                    if (detectCritical)
                    {
                        UpdateBattleText(player.GetCharacterSentence(117));
                    }
                    if (soundName == Database.SOUND_STRAIGHT_SMASH)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(124), target.FirstName, (int)damage), interval);
                    }
                    else if (textNumber != -1)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(textNumber), target.FirstName, (int)damage), interval);
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(115), target.FirstName, (int)damage), interval);
                    }

                    // ライフを更新
                    Debug.Log("damage: " + player.FirstName + " -> " + target.FirstName + " " + ((int)damage).ToString());
                    LifeDamage(damage, target, interval, detectCritical);

                    // アビス・ファイアによる効果
                    if (player.CurrentAbyssFire > 0)
                    {
                        double effectValue = PrimaryLogic.AbyssFireValue(target); // ダメージ発生源はレギィンアーゼ
                        effectValue = DamageIsZero(effectValue, player, false);
                        LifeDamage(effectValue, player, interval, detectCritical);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(120), player.FirstName, ((int)effectValue).ToString()), interval);
                    }

                    // シェズル・ミラージュ・ランサーの場合、ダブルヒット扱いとする。
                    if (((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)) ||
                        ((ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)))
                    {
                        LifeDamage(damage, target, interval, detectCritical);
                    }

                    // 対象者のシール・オブ・バランスによる効果
                    if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_BALANCE))
                    {
                        PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    }
                    if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_BALANCE))
                    {
                        PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    }

                    // 集中と断絶効果がある場合、途切れさす
                    if (player.CurrentSyutyu_Danzetsu > 0)
                    {
                        player.CurrentSyutyu_Danzetsu = 0;
                        player.DeBuff(player.pbSyutyuDanzetsu);
                    }

                    // HolyBreakerによるダメージ反射
                    if (target.CurrentHolyBreaker > 0)
                    {
                        LifeDamage(damage, player);
                    }

                    // 黒氷刀による効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_BLACK_ICE_SWORD) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_BLACK_ICE_SWORD))
                    {
                        double effectValue = PrimaryLogic.BlackIceSwordValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentMana += (int)effectValue;
                        UpdateMana(player, (int)effectValue, true, true, 0);
                    }

                    // メンタライズド・フォース・クローによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW))
                    {
                        double effectValue = PrimaryLogic.MentalizedForceClawValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }

                    // クレイモア・オブ・ザックスによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_CLAYMORE_ZUKS))
                    {
                        double effectValue = PrimaryLogic.ClaymoreZuksValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentLife += (int)effectValue;
                        UpdateLife(player, (int)effectValue, true, true, 0, false);
                    }

                    // ソード・オブ・ディバイドによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_SWORD_OF_DIVIDE) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_SWORD_OF_DIVIDE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SwordOfDivideValue_A(player))
                        {
                            double effectValue = PrimaryLogic.SwordOfDivideValue(target);
                            effectValue = DamageIsZero(effectValue, target, false);
                            LifeDamage(effectValue, target);
                        }
                    }
                    // 真紅炎・マスターブレイドによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SinkouenMasterBladeValue_A(player))
                        {
                            // ワード・オブ・パワーを発動
                            PlayerSpellWordOfPower(player, target, 0, 0);
                        }
                    }
                    // デビルキラーによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_DEVIL_KILLER) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_DEVIL_KILLER))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.DevilKillerValue(player))
                        {
                            PlayerDeath(player, target);
                        }
                    }

                    // ジュザ・ファンタズマル・クローによる効果
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_JUZA_PHANTASMAL);
                    }

                    // エターナル・フェイトリングによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_ETERNAL_FATE);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_ETERNAL_FATE);
                    }

                    // エターナル・ロイヤルリングによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }

                    // ライト・サーヴァントによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_LIGHT_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_LIGHT_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    }

                    // シャドウ・サーヴァントによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_SHADOW_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_SHADOW_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    }

                    // メイズ・キューブによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory.SwitchStatus1 == false))
                    {
                        player.Accessory.SwitchStatus1 = true;
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_MAZE_CUBE);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory2.SwitchStatus1 == false))
                    {
                        player.Accessory2.SwitchStatus1 = true;
                        PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_MAZE_CUBE);
                    }

                    // エムブレム・オブ・ヴァルキリーによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_VALKYRIE) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_VALKYRIE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfValkyrieValue(player))
                        {
                            NowStunning(player, target, (int)PrimaryLogic.EmblemOfValkyrieValue_A(player), false);
                        }
                    }
                    // エムブレム・オブ・ハデスによる効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_HADES) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_HADES))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfHades(player))
                        {
                            PlayerDeath(player, target);
                        }
                    }
                    // 氷絶零の宝珠による効果
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_ORB_SILENT_COLD_ICE) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_ORB_SILENT_COLD_ICE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SilentColdIceValue(player))
                        {
                            NowFrozen(player, target, (int)PrimaryLogic.SilentColdIceValue_A(player));
                            target.RemoveBuffSpell();
                        }
                    }

                    // CrushingBlowによる気絶
                    if (crushingBlow > 0)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(70), target.FirstName, (int)damage));
                        if (target.CurrentAntiStun > 0)
                        {
                            target.RemoveAntiStun();
                            UpdateBattleText(target.GetCharacterSentence(94));
                        }
                        else
                        {
                            if ((target.Accessory != null) && (target.Accessory.Name == "鋼鉄の石像"))
                            {
                                System.Random rd3 = new System.Random(DateTime.Now.Millisecond * Environment.TickCount);
                                if (rd3.Next(1, 101) <= target.Accessory.MinValue)
                                {
                                    UpdateBattleText(target.FirstName + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                                    UpdateBattleText(target.FirstName + "はスタン状態に陥らなかった。\r\n");
                                }
                                else
                                {
                                    NowStunning(player, target, crushingBlow, false);
                                }
                            }
                            else
                            {
                                NowStunning(player, target, crushingBlow, false);
                            }
                        }
                    }

                    // FlameAuraによる追加攻撃
                    if (player.CurrentFlameAura > 0)
                    {
                        double additional = PrimaryLogic.FlameAuraValue(player, GroundOne.DuelMode);
                        if (ignoreDefense == false)
                        {
                            if (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = (int)((float)additional / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = 0;
                                }
                            }
                        }
                        additional = DamageIsZero(additional, target, false);
                        LifeDamage(additional, target, interval);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                    }

                    // FrozenAuraによる追加攻撃
                    if (player.CurrentFrozenAura > 0)
                    {
                        double additional = PrimaryLogic.FrozenAuraValue(player, GroundOne.DuelMode);
                        if (ignoreDefense == false)
                        {
                            if (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = (int)((float)additional / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = 0;
                                }
                            }
                        }
                        additional = DamageIsZero(additional, target, false);
                        LifeDamage(additional, target, interval);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(140), additional.ToString()));
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

        
        /// <summary>
        /// 魔法ダメージのロジック
        /// </summary>
        /// <param name="player">対象元</param>
        /// <param name="target">対象相手</param>
        /// <param name="interval">発動後のインターバル</param>
        /// <param name="damage">ダメージ</param> // ref参照　DevouringPlagueの参照元で回復量に逆算したものを使用するため
        /// <param name="magnification">増減倍率、０の場合は増減しない</param>
        /// <param name="soundName">効果音ファイル名</param>
        /// <param name="messageNumber">魔法ダメージメッセージ</param>
        /// <param name="magicType">魔法属性</param>
        /// <param name="ignoreTargetDefense">対象の防御を無視する場合、True</param>
        /// <param name="critical">クリティカル有効フラグ</param>
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            return AbstractMagicDamage(player, target, interval, ref damage, magnification, soundName, messageNumber, magicType, ignoreTargetDefense, critical);
        }
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, ref double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            if (CheckDodge(player, target))
            {
                AnimationDamage(0, target, 0, Color.black, true, false, String.Empty);
                return false;
            }
            if (CheckBlindMiss(player, target))
            {
                AnimationDamage(0, target, 0, Color.black, true, false, String.Empty);
                return false;
            }

            if (CheckSilence(player))
            {
                AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_SPELL);
                return false;
            }


            // ダメージ加算
            if (damage == 0)
            {
                damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
            }
            // ダメージ「×」増幅
            if (magnification > 0)
            {
                damage = damage * magnification;
            }
            if (player.CurrentShadowPact > 0)
            {
                damage = damage * 1.5F;
            }
            if (player.CurrentEternalPresence > 0)
            {
                damage = damage * 1.3F;
            }
            if (player.CurrentPsychicTrance > 0)
            {
                damage = damage * 1.7F;
            }
            if (player.CurrentAscensionAura > 0)
            {
                damage = damage * 1.4F;
            }
            if (player.CurrentMazeCube > 0)
            {
                damage = damage * PrimaryLogic.MazeCubeValue(player);
            }

            damage = player.AmplifyMagicByEquipment(damage, magicType);

            if (player.CurrentRedDragonWill > 0)
            {
                if ((magicType == TruthActionCommand.MagicType.Fire) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                    (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                    (magicType == TruthActionCommand.MagicType.Shadow_Fire))
                {
                    damage = damage * 1.5F;
                }
            }
            if (player.CurrentBlueDragonWill > 0)
            {
                if ((magicType == TruthActionCommand.MagicType.Ice) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Ice_Force) ||
                    (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                    (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Shadow_Ice))
                {
                    damage = damage * 1.5F;
                }
            }

            // ダメージ「＋」追加
            if ((magicType == TruthActionCommand.MagicType.Light) ||
                (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                (magicType == TruthActionCommand.MagicType.Light_Force) ||
                (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                (magicType == TruthActionCommand.MagicType.Light_Shadow) ||
                (magicType == TruthActionCommand.MagicType.Light_Will))
            {
                damage += player.CurrentLightUpValue;
                damage -= player.CurrentLightDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Shadow) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Fire) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Force) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Ice) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Shadow))
            {
                damage += player.CurrentShadowUpValue;
                damage -= player.CurrentShadowDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Fire) ||
                (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Fire))
            {
                damage += player.CurrentFireUpValue;
                damage -= player.CurrentFireDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Ice) ||
                (magicType == TruthActionCommand.MagicType.Ice_Force) ||
                (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Ice) ||
                (magicType == TruthActionCommand.MagicType.Fire_Ice))
            {
                damage += player.CurrentIceUpValue;
                damage -= player.CurrentIceDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Force) ||
                (magicType == TruthActionCommand.MagicType.Force_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Force) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Force) ||
                (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                (magicType == TruthActionCommand.MagicType.Ice_Force))
            {
                damage += player.CurrentForceUpValue;
                damage -= player.CurrentForceDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Will) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Will) ||
                (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                (magicType == TruthActionCommand.MagicType.Force_Will))
            {
                damage += player.CurrentWillUpValue;
                damage -= player.CurrentWillDownValue;
            }

            // ダメージ軽減
            if (magicType == TruthActionCommand.MagicType.Force || ignoreTargetDefense)
            {
                // ワード魔法はダメージ吸収できない。
            }
            else
            {
                damage -= PrimaryLogic.MagicDefenseValue(target, PrimaryLogic.NeedType.Random, GroundOne.DuelMode);
                if (damage <= 0.0f) damage = 0.0f;

                if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
                {
                    damage = damage / 1.5F;
                }
                if (target.CurrentEternalPresence > 0 && player.CurrentTruthVision <= 0)
                {
                    damage = damage * 0.8F;
                }
                if (target.CurrentExaltedField > 0 && player.CurrentTruthVision <= 0)
                {
                    damage = damage / 1.4F;
                }

                if (target.PA == MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
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
                if (target.CurrentOneImmunity > 0 && (target.PA == MainCharacter.PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
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

                if (magicType == TruthActionCommand.MagicType.Fire)
                {
                    if (target.CurrentSigilOfHomura > 0)
                    {
                        // SigilOfHomuraが有効の場合、火のダメージ軽減は行われない。
                    }
                    else
                    {
                        if (target.ResistFire > 0)
                        {
                            damage -= target.ResistFire;
                        }
                        if (target.CurrentResistFireUp > 0)
                        {
                            damage -= target.CurrentResistFireUpValue;
                        }

                        if (target.MainWeapon != null)
                        {
                            damage -= target.MainWeapon.ResistFire;
                        }
                        if (target.SubWeapon != null)
                        {
                            damage -= target.SubWeapon.ResistFire;
                        }
                        if (target.MainArmor != null)
                        {
                            damage -= target.MainArmor.ResistFire;
                        }
                        if (target.Accessory != null)
                        {
                            // 旧仕様のため、この値で耐性値を引く。
                            if (target.Accessory.Name == Database.COMMON_CHARM_OF_FIRE_ANGEL)
                            {
                                damage -= target.Accessory.MinValue;
                            }
                            else
                            {
                                damage -= target.Accessory.ResistFire;
                            }
                            if (target.Accessory.Name == Database.RARE_SEAL_AQUA_FIRE)
                            {
                                damage = damage * (100.0F - (float)target.Accessory.MinValue) / 100.0F;
                            }
                        }
                        if (target.Accessory2 != null)
                        {
                            // 旧仕様のため、この値で耐性値を引く。
                            if (target.Accessory2.Name == Database.COMMON_CHARM_OF_FIRE_ANGEL)
                            {
                                damage -= target.Accessory2.MinValue;
                            }
                            else
                            {
                                damage -= target.Accessory2.ResistFire;
                            }

                            if (target.Accessory2.Name == Database.RARE_SEAL_AQUA_FIRE)
                            {
                                damage = damage * (100.0F - (float)target.Accessory2.MinValue) / 100.0F;
                            }

                        }
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Ice)
                {
                    if (target.ResistIce > 0)
                    {
                        damage -= target.ResistIce;
                    }
                    if (target.CurrentResistIceUp > 0)
                    {
                        damage -= target.CurrentResistIceUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistIce;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistIce;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistIce;
                    }
                    if (target.Accessory != null)
                    {
                        // 旧仕様のため、この値で耐性値を引く。
                        if (target.Accessory.Name == Database.RARE_SEAL_AQUA_FIRE)
                        {
                            damage = damage * (100.0F - (float)target.Accessory.MinValue) / 100.0F;
                        }
                        else
                        {
                            damage -= target.Accessory.ResistIce;
                        }
                    }
                    if (target.Accessory2 != null)
                    {
                        // 旧仕様のため、この値で耐性値を引く。
                        if (target.Accessory2.Name == Database.RARE_SEAL_AQUA_FIRE)
                        {
                            damage = damage * (100.0F - (float)target.Accessory2.MinValue) / 100.0F;
                        }
                        else
                        {
                            damage -= target.Accessory2.ResistIce;
                        }
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Light)
                {
                    if (target.ResistLight > 0)
                    {
                        damage -= target.ResistLight;
                    }
                    if (target.CurrentResistLightUp > 0)
                    {
                        damage -= target.CurrentResistLightUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistLight;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistLight;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistLight;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistLight;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistLight;
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Shadow)
                {
                    if (target.ResistShadow > 0)
                    {
                        damage -= target.ResistShadow;
                    }
                    if (target.CurrentResistShadowUp > 0)
                    {
                        damage -= target.CurrentResistShadowUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistShadow;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistShadow;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistShadow;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistShadow;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistShadow;
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Force)
                {
                    if (target.ResistForce > 0)
                    {
                        damage -= target.ResistForce;
                    }
                    if (target.CurrentResistForceUp > 0)
                    {
                        damage -= target.CurrentResistForceUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistForce;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistForce;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistForce;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistForce;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistForce;
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Will)
                {
                    if (target.ResistWill > 0)
                    {
                        damage -= target.ResistWill;
                    }
                    if (target.CurrentResistWillUp > 0)
                    {
                        damage -= target.CurrentResistWillUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistWill;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistWill;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistWill;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistWill;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistWill;
                    }
                    if (damage <= 0) damage = 0;
                }
                // [警告]複合魔法はどう扱っていくのか？
            }


            // 後編からスペルクリティカルを採用
            bool detectCritical = false;
            if (critical == CriticalType.Random) detectCritical = PrimaryLogic.CriticalDetect(player);
            if (critical == CriticalType.None) detectCritical = false;
            if (critical == CriticalType.Absolute) detectCritical = true;
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
                damage = damage * PrimaryLogic.CriticalDamageValue(player, GroundOne.DuelMode);
                if (player.CurrentSinFortune > 0)
                {
                    damage = damage * PrimaryLogic.SinFortuneValue(player);
                    player.RemoveSinFortune();
                }
            }

            // 効果音の再生
            if (soundName != String.Empty)
            {
                GroundOne.PlaySoundEffect(soundName);
            }

            // StanceOfMysticによる効果
            if (target.CurrentStanceOfMysticValue > 0) // && ignoreTargetDefense == false) ダメージ置き換えはignoreTargetDefenseの対象外でダメージを０にする。
            {
                target.CurrentStanceOfMysticValue--;
                target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMystic);
                damage = 0;
                AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_AVOID);
                return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
            }

            // HardestParryによる効果
            if (target.CurrentHardestParry) // && ignoreTargetDefense == false) ダメージ置き換えはignoreTargetDefenseの対象外でダメージを０にする。
            {
                target.CurrentHardestParry = false;
                damage = 0;
                AnimationDamage(0, target, 0, Color.black, false, false, Database.SUCCESS_AVOID);
                return false; // 呼び出し元で追加効果をスキップさせるためのfalse返し
            }

            // MirrorImageによる効果
            if (magicType == TruthActionCommand.MagicType.Force)
            {
                // ワード魔法は反射できない。
            }
            else
            {
                if (target.CurrentMirrorImage > 0)
                {
                    AnimationDamage(0, target, 0, Color.black, false, false, "反射");

                    damage = DamageIsZero(damage, player, ignoreTargetDefense);
                    LifeDamage(damage, player);
                    UpdateBattleText(String.Format(target.GetCharacterSentence(58), ((int)damage).ToString(), player.FirstName), 1000);

                    target.CurrentMirrorImage = 0;
                    target.DeBuff(target.pbMirrorImage);
                    return true;
                }
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave > 0)
            {
                UpdateBattleText("炎のダンシングソードが敵へめがけて炎を発射！\r\n");
                double immortalRaveValue = PrimaryLogic.ImmmortalRaveValue(player, GroundOne.DuelMode);
                immortalRaveValue = DamageIsZero(immortalRaveValue, player, ignoreTargetDefense);
                LifeDamage(immortalRaveValue, target, interval, detectCritical);
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), target.FirstName, ((int)damage).ToString()), interval);
            }

            // SkyShieldによる効果
            if (target.CurrentSkyShieldValue > 0 && ignoreTargetDefense == false)
            {
                target.CurrentSkyShieldValue--;
                target.ChangeSkyShieldStatus(target.CurrentSkyShield);
                damage = 0;
            }
            // StaticBarrierの効果
            if (target.CurrentStaticBarrier > 0 && ignoreTargetDefense == false)
            {
                target.CurrentStaticBarrierValue--;
                target.ChangeStaticBarrierStatus(target.CurrentStaticBarrier);
                damage = damage * 0.5f;
            }
            // ダメージ０変換
            damage = DamageIsZero(damage, target, ignoreTargetDefense);

            // ブルー・リフレクト・ローブによる効果
            if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_BLUE_REFLECT_ROBE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.BlueReflectRobeValue(player))
                {
                    AnimationDamage(0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                    damage = 0;
                }
            }
            // スライド・スルー・シールドによる効果
            if ((target.SubWeapon != null) && (target.SubWeapon.Name == Database.RARE_SLIDE_THROUGH_SHIELD))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.SlideThroughShieldValue(player))
                {
                    AnimationDamage(0, target, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
                    damage = 0;
                }
            }

            // メッセージ更新
            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }

            // ライフを更新
            LifeDamage(damage, target, interval, detectCritical);
            if (soundName == Database.SOUND_DEVOURING_PLAGUE)
            {
                UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)damage).ToString()), interval);
            }
            else
            {
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), target.FirstName, ((int)damage).ToString()), interval);
            }

            // アビス・ファイアによる効果
            if (player.CurrentAbyssFire > 0)
            {
                double effectValue = PrimaryLogic.AbyssFireValue(ec1); // ダメージ発生源はレギィンアーゼ
                LifeDamage(effectValue, player, interval);
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), player.FirstName, ((int)effectValue).ToString()), interval);
            }

            // 対象者のシール・オブ・バランスによる効果
            if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_BALANCE))
            {
                PlayerAbstractSkillGain(target, target, 0, PrimaryLogic.RainbowTubeValue_B(target, GroundOne.DuelMode), 0, Database.SOUND_FRESH_HEAL, 5009);
            }
            if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_BALANCE))
            {
                PlayerAbstractSkillGain(target, target, 0, PrimaryLogic.RainbowTubeValue_B(target, GroundOne.DuelMode), 0, Database.SOUND_FRESH_HEAL, 5009);
            }

            // 集中と断絶効果がある場合、途切れさす
            if (player.CurrentSyutyu_Danzetsu > 0)
            {
                player.CurrentSyutyu_Danzetsu = 0;
                player.DeBuff(player.pbSyutyuDanzetsu);
            }

            // SigilOfHomuraによる追加効果
            if (target.CurrentSigilOfHomura > 0)
            {
                UpdateBattleText("焔の印が赤く輝く！\r\n");
                LifeDamage(damage, target, interval, detectCritical);
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), target.FirstName, ((int)damage).ToString()), interval);
            }

            // アダーカー・フォルス・ロッドによる効果
            if ((player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_ADERKER_FALSE_ROD))
            {
                double effectValue = PrimaryLogic.AderkerFalseRodValue(player);
                player.CurrentInstantPoint += (int)effectValue;
                UpdateInstantPoint(player);
            }
            // 真紅炎・マスターブレイドによる効果
            if ((player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE) ||
                (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.SinkouenMasterBladeValue_A(player))
                {
                    // サイキック・ウェイブを発動
                    PlayerSkillPsychicWave(player, target);
                }
            }

            // ライト・サーヴァントによる効果
            if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_LIGHT_SERVANT))
            {
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_LIGHT_SERVANT);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_LIGHT_SERVANT))
            {
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_LIGHT_SERVANT);
            }

            // シャドウ・サーヴァントによる効果
            if ((player.Accessory != null) && (player.Accessory.Name == Database.BUFF_SHADOW_SERVANT))
            {
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_SHADOW_SERVANT);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.BUFF_SHADOW_SERVANT))
            {
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_SHADOW_SERVANT);
            }

            // メイズ・キューブによる効果
            if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory.SwitchStatus1 == true))
            {
                player.Accessory.SwitchStatus1 = false;
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_MAZE_CUBE);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory2.SwitchStatus1 == true))
            {
                player.Accessory2.SwitchStatus1 = false;
                PlayerBuffAbstract(player, player, Database.ITEMCOMMAND_MAZE_CUBE);
            }

            // エムブレム・オブ・ヴァルキリーによる効果
            if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_VALKYRIE) ||
                (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_VALKYRIE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfValkyrieValue(player))
                {
                    NowStunning(player, target, (int)PrimaryLogic.EmblemOfValkyrieValue_A(player), false);
                }
            }
            // エムブレム・オブ・ハデスによる効果
            if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_HADES) ||
                (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_HADES))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfHades(player))
                {
                    PlayerDeath(player, target);
                }
            }
            return true;
        }
        // 魔法攻撃
        private void PlayerMagicAttack(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.None, false, CriticalType.Random);
        }


        // 味方対象
        /// <summary>
        /// 回復コマンドの抽象化
        /// </summary>
        /// <param name="player">対象元</param>
        /// <param name="target">対象相手</param>
        /// <param name="interval">発動後のインターバル</param>
        /// <param name="damage">ダメージ</param>
        /// <param name="magnification">増減倍率、０の場合は増減しない</param>
        /// <param name="soundName">効果音ファイル名</param>
        /// <param name="messageNumber">魔法ダメージメッセージ</param>
        private void PlayerAbstractLifeGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target.CurrentNourishSense > 0)
            {
                effectValue = effectValue * PrimaryLogic.NourishSenseValue(player);
            }

            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("しかし" + target.FirstName + "は死んでしまっているため効果が無かった！\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(119));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        effectValue = GainIsZero(effectValue, target);
                        target.CurrentLife += (int)effectValue;
                        UpdateLife(target, effectValue, true, true, 0, false);
                        if (messageNumber == 0)
                        {
                            UpdateBattleText(target.FirstName + "は" + ((int)effectValue).ToString() + "回復した。\r\n");
                        }
                        else
                        {
                            UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                        }
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(119));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    effectValue = GainIsZero(effectValue, player);
                    player.CurrentLife += (int)effectValue;
                    UpdateLife(player, effectValue, true, true, 0, false);
                    if (messageNumber == 0)
                    {
                        UpdateBattleText(player.FirstName + "は" + ((int)effectValue).ToString() + "回復した。\r\n");
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// マナ回復コマンドの抽象化
        /// </summary>
        /// <param name="player">対象元</param>
        /// <param name="target">対象相手</param>
        /// <param name="interval">発動後のインターバル</param>
        /// <param name="damage">ダメージ</param>
        /// <param name="magnification">増減倍率、０の場合は増減しない</param>
        /// <param name="soundName">効果音ファイル名</param>
        /// <param name="messageNumber">魔法ダメージメッセージ</param>
        private void PlayerAbstractManaGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("しかし" + target.FirstName + "は死んでしまっているため効果が無かった！\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(121));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        target.CurrentMana += (int)effectValue;
                        UpdateMana(target, effectValue, true, true, 0);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(121));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    player.CurrentMana += (int)effectValue;
                    UpdateMana(player, effectValue, true, true, 0);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                }
            }
        }

        /// <summary>
        /// スキル回復コマンドの抽象化
        /// </summary>
        /// <param name="player">対象元</param>
        /// <param name="target">対象相手</param>
        /// <param name="interval">発動後のインターバル</param>
        /// <param name="damage">ダメージ</param>
        /// <param name="magnification">増減倍率、０の場合は増減しない</param>
        /// <param name="soundName">効果音ファイル名</param>
        /// <param name="messageNumber">魔法ダメージメッセージ</param>
        private void PlayerAbstractSkillGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("しかし" + target.FirstName + "は死んでしまっているため効果が無かった！\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(121));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        target.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(target, effectValue, true, true, 0);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(121));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    player.CurrentSkillPoint += (int)effectValue;
                    UpdateSkillPoint(player, effectValue, true, true, 0);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                }
            }
        }

        private void UpdateLife(MainCharacter player, double damage, bool plusValue, bool animationDamage, int interval, bool critical)
        {
            UpdateLife(player);

            if (animationDamage)
            {
                Color color = Color.black;
                if (plusValue)
                {
                    color = new Color(0, 0.7f, 0);
                    if (this.NowTimeStop)
                    {
                        color = UnityColor.Lightgreen;
                    }
                }
                else if (this.NowTimeStop)
                {
                    color = Color.white;
                }
                AnimationDamage(damage, player, interval, color, false, critical, String.Empty);
            }
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

            // 色付け
            if (player.labelCurrentLifePoint != null)
            {
                if (player.CurrentLife >= player.MaxLife)
                {
                    player.labelCurrentLifePoint.color = UnityColor.ForestGreen;
                    if (this.NowTimeStop)
                    {
                        player.labelCurrentLifePoint.color = UnityColor.Lightgreen;
                    }
                }
                else
                {
                    player.labelCurrentLifePoint.color = Color.black;
                    if (this.NowTimeStop)
                    {
                        player.labelCurrentLifePoint.color = Color.white;
                    }
                }
            }
        }

        private void UpdateMana(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            UpdateMana(player);
            if (animationDamage)
            {
                AnimationDamage(effectValue, player, interval, Color.blue, false, false, String.Empty);
            }
        }
        private void UpdateMana(MainCharacter player)
        {
            float dx = (float)player.CurrentMana / (float)player.MaxMana;
            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            }
            if (player.meterCurrentManaPoint != null)
            {
                player.meterCurrentManaPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }
        private void UpdateSkillPoint(MainCharacter player)
        {
            UpdateSkillPoint(player, 0, false, false, 0);
        }
        private void UpdateSkillPoint(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            float dx = (float)player.CurrentSkillPoint / (float)player.MaxSkillPoint;
            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            }
            if (player.meterCurrentSkillPoint != null)
            {
                player.meterCurrentSkillPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }

            if (animationDamage)
            {
                AnimationDamage(effectValue, player, interval, UnityColor.DarkGreen, false, false, String.Empty);
            }
        }
        private void UpdateInstantPoint(MainCharacter player)
        {
            float dx = (float)player.CurrentInstantPoint / (float)player.MaxInstantPoint;
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.text = player.CurrentInstantPoint.ToString();
            }
            if (player.meterCurrentInstantPoint != null)
            {
                player.meterCurrentInstantPoint.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
        }
        private void UpdateSpecialInstantPoint(MainCharacter player)
        {
            float dx = (float)player.CurrentSpecialInstant / (float)player.MaxSpecialInstant;
            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.text = player.CurrentSpecialInstant.ToString();
            }
            if (player.meterCurrentSpecialInstant != null)
            {
                player.meterCurrentSpecialInstant.rectTransform.localScale = new Vector2(dx, 1.0f);
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
                PlayerSpellDarkBlast(player, target, 0, 0);
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
                PlayerSpellWordOfPower(player, target, 0, 0);
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
                PlayerSkillStraightSmash(player, target, 0, false);
            }
            else if (command == Database.COUNTER_ATTACK)
            {
                PlayerSkillCounterAttack(player, target);
            }
            else if (command == Database.STANCE_OF_FLOW)
            {
                PlayerSkillStanceOfFlow(player);
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

        public void UseItemButton_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_USEITEM, String.Empty, String.Empty);
            groupParentBackpack.SetActive(!groupParentBackpack.activeInHierarchy);
        }
        public void RunAwayButton_Click()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_BATTLEENEMY_RUNAWAY, String.Empty, String.Empty);
            if (this.cannotRunAway)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは今逃げられない状態にいる。\r\n");
                return;
            }

            if (GroundOne.DuelMode)
            {
                txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは降参を宣言した。\r\n");
                this.BattleEndFlag = true;
                this.RunAwayFlag = true;
                BattleEndPhase();
                System.Threading.Thread.Sleep(200);
                return;
            }

            // 主人公が逃げられない状態を演出したいが、プレイヤーとしてはすぐ逃げられるようにしておいてほしい。
            //if (GroundOne.MC.CurrentStunning > 0 || GroundOne.MC.CurrentFrozen > 0 || GroundOne.MC.CurrentParalyze > 0)
            //{
            //    UpdateBattleText("アインは今逃げられない状態に居る。\r\n");
            //    return;
            //}

            // debug
            //if (((GroundOne.MC.Level - this.ec1.Level <= -5) && AP.Math.RandomInteger(100) < 95) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == -4) && AP.Math.RandomInteger(100) < 90) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == -3) && AP.Math.RandomInteger(100) < 80) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == -2) && AP.Math.RandomInteger(100) < 70) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == -1) && AP.Math.RandomInteger(100) < 70) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == 0) && AP.Math.RandomInteger(100) < 50) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == 1) && AP.Math.RandomInteger(100) < 40) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == 2) && AP.Math.RandomInteger(100) < 30) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == 3) && AP.Math.RandomInteger(100) < 20) ||
            //    ((GroundOne.MC.Level - this.ec1.Level == 4) && AP.Math.RandomInteger(100) < 10) ||
            //    ((GroundOne.MC.Level - this.ec1.Level >= 5) && AP.Math.RandomInteger(100) < 5)
            //    )
            //{
            //    UpdateBattleText("逃げようとして、敵に追いつかれた！　パーティ全員にスタン効果が発生！\r\n");
            //    List<MainCharacter> group = new List<MainCharacter>();
            //    SetupAllyGroup(ref group);
            //    for (int ii = 0; ii < group.Count; ii++)
            //    {
            //        NowStunning(group[ii], group[ii], 1, true);
            //    }
            //    return;
            //}

            txtBattleMessage.text = txtBattleMessage.text.Insert(0, "アインは逃げ出した。\r\n");
            this.BattleEndFlag = true;
            this.RunAwayFlag = true;
            BattleEndPhase();
            System.Threading.Thread.Sleep(200);
        }

        public void GameOverYes_Click()
        {
            GroundOne.BattleResult = GroundOne.battleResult.Retry;
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }
        public void GameOverNo_Click()
        {
            GroundOne.BattleResult = GroundOne.battleResult.Ignore;
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }

        public void OnMouseEnterImage(Button sender)
        {
        }

        private void AnimationDamage(double damage, MainCharacter target, int interval, Color plusValue, bool avoid, bool critical, string customString)
        {
            Debug.Log("AnimationDamage start: " + this.nowAnimationCounter);
            target.DamageLabel.text = ((int)damage).ToString();
            this.nowAnimationTarget.Add(target);
            this.nowAnimationDamage.Add((int)damage);
            this.nowAnimationColor.Add(plusValue);
            this.nowAnimationAvoid.Add(avoid);
            this.nowAnimationCustomString.Add(customString);
            this.nowAnimationInterval.Add(interval);
            this.nowAnimationCritical.Add(critical);
            this.nowAnimationText.Add(target.DamageLabel);
            this.nowAnimation = true;
        }

        Vector3 ExecAnimation_basePoint;
        Vector3 ExecAnimation_basePointCritical;
        private void ExecAnimation()
        {
            Text targetLabel = this.nowAnimationTarget[0].DamageLabel;
            Text targetCriticalLabel = this.nowAnimationTarget[0].CriticalLabel;

            targetLabel.color = this.nowAnimationColor[0];

            if (this.nowAnimationAvoid[0])
            {
                targetLabel.text = Database.MISS;
            }
            else if (this.nowAnimationCustomString[0] != string.Empty)
            {
                targetLabel.text = this.nowAnimationCustomString[0];
            }
            else
            {
                targetLabel.text = Convert.ToString(this.nowAnimationDamage[0]);
            }
            
            int[] waitTime = {150, 90, 60, 40, 20};

            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                waitTime[0] = 60; waitTime[1] = 36; waitTime[2] = 24; waitTime[3] = 16; waitTime[4] = 8;
            }
            int wait = waitTime[2];
            if (Database.BATTLE_CORE_SLEEP == 40) wait = waitTime[0];
            else if (Database.BATTLE_CORE_SLEEP == 20) wait = waitTime[1];
            else if (Database.BATTLE_CORE_SLEEP == 10) wait = waitTime[2];
            else if (Database.BATTLE_CORE_SLEEP == 5) wait = waitTime[3];
            else if (Database.BATTLE_CORE_SLEEP == 2) wait = waitTime[4];

            if (this.HiSpeedAnimation) { wait = wait / 2; }
            if (this.nowAnimationInterval[0] > 0) wait = this.nowAnimationInterval[0];

            if (this.nowAnimationCounter <= 0)
            {
                if (this.nowAnimationCritical[0])
                {
                    targetLabel.fontSize = targetLabel.fontSize + 4;
                    targetCriticalLabel.text = "Critical";
                    targetCriticalLabel.gameObject.SetActive(true);
                }
                else
                {
                    targetCriticalLabel.text = "";
                    targetCriticalLabel.gameObject.SetActive(true);
                }

                this.nowAnimationTarget[0].DamagePanel.gameObject.SetActive(true);
                this.nowAnimationTarget[0].DamageLabel.gameObject.SetActive(true);

                ExecAnimation_basePoint = targetLabel.transform.position;
                ExecAnimation_basePointCritical = targetCriticalLabel.transform.position;
            }

            int movement = 1;
            if (this.nowAnimationCounter > 10) { movement = 0; }
            targetLabel.transform.position = new Vector3(targetLabel.transform.position.x + movement, targetLabel.transform.position.y, targetLabel.transform.position.z);
            targetCriticalLabel.transform.position = new Vector3(targetCriticalLabel.transform.position.x + movement, targetCriticalLabel.transform.position.y, targetCriticalLabel.transform.position.z);
            System.Threading.Thread.Sleep(5);

            this.nowAnimationCounter++;
            if (this.nowAnimationCounter > wait)
            {
                targetLabel.gameObject.SetActive(false);
                targetLabel.transform.position = ExecAnimation_basePoint;

                targetCriticalLabel.gameObject.SetActive(false);
                targetCriticalLabel.transform.position = ExecAnimation_basePointCritical;

                if (this.nowAnimationCritical[0])
                {
                    targetLabel.fontSize = targetLabel.fontSize - 4;
                }

                if (this.nowAnimationTarget.Count > 0)
                {
                    this.nowAnimationTarget.RemoveAt(0);
                    this.nowAnimationDamage.RemoveAt(0);
                    this.nowAnimationColor.RemoveAt(0);
                    this.nowAnimationAvoid.RemoveAt(0);
                    this.nowAnimationCustomString.RemoveAt(0);
                    this.nowAnimationInterval.RemoveAt(0);
                    this.nowAnimationCritical.RemoveAt(0);
                    this.nowAnimationText.RemoveAt(0);
                    this.nowAnimationCounter = 0;
                }

                if (this.nowAnimationTarget.Count <= 0)
                {
                    this.nowAnimationTarget.Clear();
                    this.nowAnimationDamage.Clear();
                    this.nowAnimationColor.Clear();
                    this.nowAnimationAvoid.Clear();
                    this.nowAnimationCustomString.Clear();
                    this.nowAnimationInterval.Clear();
                    this.nowAnimationCritical.Clear();
                    this.nowAnimationText.Clear();
                    this.nowAnimationCounter = 0;
                    this.nowAnimation = false;
                }
            }
        }

        private void ExecStackAnimation()
        {
            int waitTime = 150;
            this.nowStackAnimationCounter++;
            if (this.nowStackAnimationCounter > waitTime)
            {
                this.back_nowStackAnimationBar.transform.localScale = new Vector2(0.0f, 1.0f);
                this.back_nowStackAnimationName.transform.localScale = new Vector2(0.0f, 1.0f);
                this.nowStackAnimation = false;
                this.nowStackAnimationCounter = 0;
                StackInTheCommandEnd();
            }
        }

        private void AnimationMessageFadeOut(string message)
        {
            MatrixDragonTalkText.color = Color.white;
            MatrixDragonTalkText.text = message;

            this.nowAnimationMatrixTalkCounter = 0;
            this.nowAnimationMatrixTalk = true;
        }
        private void ExecAnimationMessageFadeOut()
        {
            if (this.nowAnimationMatrixTalkCounter <= 0)
            {
                back_MatrixDragonTalk.gameObject.SetActive(true);
            }

            int waitTime = 258; 
            System.Threading.Thread.Sleep(10);
            if (this.nowAnimationMatrixTalkCounter > 130)
            {
                this.MatrixDragonTalkText.color = new Color(this.MatrixDragonTalkText.color.r, this.MatrixDragonTalkText.color.g, this.MatrixDragonTalkText.color.b, this.MatrixDragonTalkText.color.a-(2.0f/255.0f));
            }

            this.nowAnimationMatrixTalkCounter++;

            if (this.nowAnimationMatrixTalkCounter > waitTime)
            {
                back_MatrixDragonTalk.gameObject.SetActive(false);
                this.nowAnimationMatrixTalkCounter = 0;
                this.nowAnimationMatrixTalk = false;
            }
        }
        
        private void AnimationSandGlass()
        {
            this.nowAnimationSandGlassCounter = 0;
            this.nowAnimationSandGlass = true;
        }

        float angle = 0.0f;
        private void ExecAnimationSandGlass()
        {
            Text targetLabel = this.SandGlassText;

            if (this.nowAnimationSandGlassCounter <= 0)
            {
                back_Sandglass.gameObject.SetActive(true);
                targetLabel.text = (this.BattleTurnCount - 1).ToString();
                targetLabel.gameObject.SetActive(true);
                SandGlassImage.sprite = Resources.Load<Sprite>("AnimeSandGlass0");
                SandGlassImage.gameObject.SetActive(true);
            }

            int waitTime = 52;
            int startTime = 15;
            int moveLen = (Screen.width - 150) / 36;

            if (this.nowAnimationSandGlassCounter > startTime)
            {
                if (Application.platform == RuntimePlatform.Android ||
                    Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    // 何もしない
                }
                else
                {
                    System.Threading.Thread.Sleep(0);
                }
                //SandGlassImage.sprite = Resources.Load<Sprite>("AnimeSandGlass" + (this.nowAnimationSandGlassCounter-(startTime+1)).ToString());
                angle += 10;
                SandGlassImage.transform.rotation = Quaternion.Euler(0, 0, angle);
                SandGlassImage.transform.position = new Vector3(SandGlassImage.transform.position.x + moveLen, SandGlassImage.transform.position.y, SandGlassImage.transform.position.z);

                if (this.nowAnimationSandGlassCounter == 36)
                {
                    targetLabel.text = this.BattleTurnCount.ToString();
                }
            }

            this.nowAnimationSandGlassCounter++;

            if (this.nowAnimationSandGlassCounter > waitTime)
            {
                System.Threading.Thread.Sleep(500);
                this.angle = 0.0f;
                SandGlassImage.transform.rotation = new Quaternion(0, 0, 0, 0);
                SandGlassImage.transform.position = new Vector3(SandGlassImage.transform.position.x - moveLen * (waitTime - startTime), SandGlassImage.transform.position.y, SandGlassImage.transform.position.z);
                back_Sandglass.gameObject.SetActive(false);
                targetLabel.gameObject.SetActive(false);
                SandGlassImage.gameObject.SetActive(false);
                this.nowAnimationSandGlass = false;
                this.nowAnimationSandGlassCounter = 0;
            }
        }

        private void AnimationFinal(string message)
        {
            this.FinalBattleText.text = message;
            this.nowAnimationFinalCounter = 0;
            this.nowAnimationFinal = true;
        }

        public int debugcounter = 0;
        private void ExecAnimationFinalBattle()
        {
            Debug.Log("this.counter: " + this.nowAnimationFinalCounter.ToString());
            Text targetLabel = this.FinalBattleText;

            if (this.nowAnimationFinalCounter <= 0)
            {
                Debug.Log("screen width: " + Screen.width.ToString());
                targetLabel.transform.position = new Vector3(targetLabel.transform.position.x + Screen.width, targetLabel.transform.position.y, targetLabel.transform.position.z);
                back_FinalBattle.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                targetLabel.gameObject.SetActive(true);
                back_FinalBattle.gameObject.SetActive(true);
            }

            int waitTime = 181;

            if (this.nowAnimationFinalCounter < 33)
            {
                this.debugcounter += 50;
                targetLabel.transform.position = new Vector3(targetLabel.transform.position.x - 50, targetLabel.transform.position.y, targetLabel.transform.position.z);
            }
            else if (this.nowAnimationFinalCounter < 150)
            {
                this.debugcounter += 1;
                targetLabel.transform.position = new Vector3(targetLabel.transform.position.x - 1, targetLabel.transform.position.y, targetLabel.transform.position.z);
            }
            else
            {
                this.debugcounter += 40;
                targetLabel.transform.position = new Vector3(targetLabel.transform.position.x - 40, targetLabel.transform.position.y, targetLabel.transform.position.z);
                this.back_FinalBattle.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f - (((float)this.nowAnimationFinalCounter - 150) / 30.0f), 1.0f);
            }

            this.nowAnimationFinalCounter++;

            if (this.nowAnimationFinalCounter > waitTime)
            {
                Debug.Log("debugcounter: " + this.debugcounter.ToString());
                System.Threading.Thread.Sleep(500);
                targetLabel.transform.position = new Vector3(targetLabel.transform.position.x + 3047 - Screen.width, targetLabel.transform.position.y, targetLabel.transform.position.z);
                targetLabel.gameObject.SetActive(false);
                back_FinalBattle.gameObject.SetActive(false);
                this.nowAnimationFinal = false;
                this.nowAnimationFinalCounter = 0;
            }
        }
        
        private bool CheckDodge(MainCharacter player, MainCharacter target)
        {
            return CheckDodge(player, target, false);
        }

        private bool CheckDodge(MainCharacter player, MainCharacter target, bool ignoreDodge)
        {
            if (target == null) { return false; }
            if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_ONEHUNDRED_BUTOUGI) &&
                (target.CurrentStunning <= 0) &&
                (target.CurrentFrozen <= 0) &&
                (target.CurrentParalyze <= 0) &&
                (target.CurrentTemptation <= 0))
            {
                if (AP.Math.RandomInteger(100) <= PrimaryLogic.OneHundredButougiValue(target))
                {
                    UpdateBattleText(target.FirstName + "は" + target.MainArmor.Name + "の効果で素早く身をかわした！\r\n");
                    return true;
                }
            }

            if ((target.Accessory != null) && (target.Accessory.Name == "身かわしのマント") &&
                (target.CurrentStunning <= 0) &&
                (target.CurrentFrozen <= 0) &&
                (target.CurrentParalyze <= 0) &&
                (target.CurrentTemptation <= 0))
            {
                if (AP.Math.RandomInteger(100) <= target.Accessory.MinValue)
                {
                    UpdateBattleText(target.FirstName + "は" + target.Accessory.Name + "の効果で素早く身をかわした！\r\n");
                    return true;
                }
            }

            if ((target.Accessory2 != null) && (target.Accessory2.Name == "身かわしのマント") &&
                    (target.CurrentStunning <= 0) &&
                    (target.CurrentFrozen <= 0) &&
                    (target.CurrentParalyze <= 0) &&
                    (target.CurrentTemptation <= 0))
            {
                if (AP.Math.RandomInteger(100) <= target.Accessory2.MinValue)
                {
                    UpdateBattleText(target.FirstName + "は" + target.Accessory2.Name + "の効果で素早く身をかわした！\r\n");
                    return true;
                }
            }

            if (target.CurrentBlinded > 0)
            {
                UpdateBattleText(target.FirstName + "は退避状態により、難なく身をかわした！\r\n");
                return true;
            }

            return false;
        }

        private bool CheckBlindMiss(MainCharacter player, MainCharacter target)
        {
            if (player.CurrentBlind > 0)
            {
                int randomValue = AP.Math.RandomInteger(1000);
                if (randomValue <= 500)
                {
                    UpdateBattleText(player.FirstName + "は攻撃を外してしまった！\r\n");
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 沈黙状態で詠唱不可能かどうかを判定するチェックメソッド
        /// </summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckSilence(MainCharacter player)
        {
            if (player.CurrentSilence > 0)
            {
                AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_SPELL);
                return true;
            }
            return false;
        }

        private bool CheckCancelSpell(MainCharacter player, string currentSpellName)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            SetupAllyGroup(ref group);
            SetupEnemyGroup(ref group);

            for (int ii = 0; ii < group.Count; ii++)
            {
                if ((group[ii].Accessory != null) && (group[ii].Accessory.Name == Database.COMMON_DEVIL_SEALED_VASE) && (group[ii].Accessory.ImprintCommand == currentSpellName) ||
                    (group[ii].Accessory2 != null) && (group[ii].Accessory2.Name == Database.COMMON_DEVIL_SEALED_VASE) && (group[ii].Accessory2.ImprintCommand == currentSpellName))
                {
                    AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_SPELL);
                    return true;
                }
            }
            return false;
        }


        /// <summary>インスタント行動カウンターFutureVisionのチェックメソッド</summary> 
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckFutureVision(MainCharacter player)
        {
            return false;
        }
        /// <summary>
        /// インスタント行動カウンターStanceOfSuddennessのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckStanceOfSuddenness(MainCharacter player)
        {
            return false;
        }

        /// <summary>
        /// 非ダメージ系インスタント行動カウンターDeepMirrorのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckDeepMirror(MainCharacter player)
        {
            return false;
        }

        /// <summary>ダメージ系インスタントカウンターStanceOfMysticのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckStanceOfMystic(MainCharacter player)
        {
            return false;
        }

        /// <summary>魔法・スキルカウンターHymnContractのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckHymnContract(MainCharacter player)
        {
            return false;
        }

        /// <summary>
        /// 物理攻撃カウンターCounterAttackのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckCounterAttack(MainCharacter player, string CurrentSkillName)
        {
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if (DetectOpponentParty(player, ActiveList[ii]))
            //    {
            //        if (ActiveList[ii].CurrentCounterAttack > 0)
            //        {
            //            if (TruthActionCommand.IsDamage(CurrentSkillName))
            //            {
            //                ActiveList[ii].RemoveCounterAttack();
            //                if (JudgeSuccessOfCounter(player, ActiveList[ii], 113))
            //                {
            //                    // [警告] カウンター判定内でダメージを与えるのはいかがなものか・・・
            //                    // しかし、ここに入れておく。
            //                    PlayerNormalAttack(ActiveList[ii], player, 0, false, false);
            //                    return true; // カウンター成功
            //                }
            //            }
            //        }
            //    }
            //}
            return false;
        }

        /// <summary>
        /// 魔法・スキルカウンターStanceOfEyesのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckStanceOfEyes(MainCharacter player)
        {
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if (DetectOpponentParty(player, ActiveList[ii]))
            //    {
            //        if (ActiveList[ii].CurrentStanceOfEyes > 0)
            //        {
            //            ActiveList[ii].RemoveStanceOfEyes(); // カウンター成功／失敗に限らず、一度チェックが入ったら解消されるものとする（１ターンで何度も発動するのは強すぎたため）
            //            if (JudgeSuccessOfCounter(player, ActiveList[ii], 101))
            //            {
            //                return true; // カウンター成功
            //            }
            //        }
            //    }
            //}
            return false;
        }

        /// <summary>
        /// スペルカウンターNegateのチェックメソッド</summary>
        /// <returns>False：カウンター無しでスルー　true：カウンター判定あり</returns>
        private bool CheckNegateCounter(MainCharacter player)
        {
            //for (int ii = 0; ii < ActiveList.Count; ii++)
            //{
            //    if (DetectOpponentParty(player, ActiveList[ii]))
            //    {
            //        if (ActiveList[ii].CurrentNegate > 0)
            //        {
            //            ActiveList[ii].RemoveNegate(); // カウンター成功／失敗に限らず、一度チェックが入ったら解消されるものとする（１ターンで何度も発動するのは強すぎたため）
            //            if (JudgeSuccessOfCounter(player, ActiveList[ii], 104))
            //            {
            //                return true; // カウンター成功
            //            }
            //        }
            //    }
            //}
            return false;
        }

        private bool CheckResurrectWithItem(MainCharacter target, string itemName)
        {
            if ((target.MainWeapon != null) && (target.MainWeapon.Name == itemName) && (target.MainWeapon.EffectStatus == false))
            {
                target.MainWeapon.EffectStatus = true;
                return true;
            }
            else if ((target.SubWeapon != null) && (target.SubWeapon.Name == itemName) && (target.SubWeapon.EffectStatus == false))
            {
                target.SubWeapon.EffectStatus = true;
                return true;
            }
            else if ((target.MainArmor != null) && (target.MainArmor.Name == itemName) && (target.MainArmor.EffectStatus == false))
            {
                target.MainArmor.EffectStatus = true;
                return true;
            }
            else if ((target.Accessory != null) && (target.Accessory.Name == itemName) && (target.Accessory.EffectStatus == false))
            {
                target.Accessory.EffectStatus = true;
                return true;
            }
            else if ((target.Accessory2 != null) && (target.Accessory2.Name == itemName) && (target.Accessory2.EffectStatus == false))
            {
                target.Accessory2.EffectStatus = true;
                return true;
            }

            return false;
        }

        private void BattleEndPhase()
        {
            Debug.Log("BattleEndPhase (S)");
            if (this.execBattleEndPhase == false)
            {
                this.execBattleEndPhase = true;
            }
            else
            {
                Debug.Log("BattleEndPhase already done...");
                return;
            }

            // 全てのBUFF効果を解除する。
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                string brokenName = String.Empty;
                ActiveList[ii].CleanUpBattleEnd(ref brokenName);
                if (brokenName != String.Empty)
                {
                    // 破損したアイテム名を出しても良いが、名前が長すぎる場合、読めないので、アイテム名表示は不要と判断。
                    AnimationDamage(0, ActiveList[ii], 200, Color.red, false, false, Database.BROKEN_ITEM);
                }
            }

            // チュートリアルでは何もせず終了する。
            if (GroundOne.TutorialMode)
            {
                GroundOne.BattleResult = GroundOne.battleResult.OK;
            }
            // 支配竜会話終了時、通常終了とみなす。
            else if (this.endBattleForMatrixDragonEnd)
            {
                Debug.Log("endBattleForMatrixDragonEnd true then exit ok");
                GroundOne.BattleResult = GroundOne.battleResult.OK;
            }
            // [警告]万が一、相打ちの場合、プレイヤーの負けとみなす
            else if (EnemyPartyDeathCheck())
            {
                if (GroundOne.DuelMode)
                {
                    UpdateBattleText("アインはDUELに敗れた！\r\n");
                    System.Threading.Thread.Sleep(200);
                    GroundOne.BattleResult = GroundOne.battleResult.Ignore;
                }
                else
                {
                    UpdateBattleText("全滅しました・・・もう一度この戦闘をやり直しますか？\r\n");
                    yesnoSystemMessage.text = "全滅しました・・・もう一度この戦闘をやり直しますか？";
                    groupYesnoSystemMessage.SetActive(true);
                    return; // scenebackさせない
                }
            }
            else if (this.RunAwayFlag)
            {
                if (!GroundOne.WE.AvailableSecondCharacter)
                {
                    if (GroundOne.DuelMode)
                    {
                        UpdateBattleText("アインは降参を宣言した。\r\n");
                    }
                    else
                    {
                        UpdateBattleText("アインは逃げ出した。\r\n");
                    }
                }
                else
                {
                    if (GroundOne.DuelMode)
                    {
                        UpdateBattleText("アインは降参を宣言した。\r\n");
                    }
                    else
                    {
                        UpdateBattleText("アイン達は逃げ出した。\r\n");
                    }
                }
                System.Threading.Thread.Sleep(200);
                GroundOne.BattleResult = GroundOne.battleResult.Abort;
            }
            else
            {
                GroundOne.BattleResult = GroundOne.battleResult.OK;
                if (GroundOne.DuelMode == false)
                {
                    UpdateBattleText("敵を倒した！　" + ec1.Exp + "の経験値を得た。\r\n");
                    ExpGoldDisplay(ec1.Exp, ec1.Gold);
                }

                if (ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_3 && GroundOne.MC.Dead)
                {
                    GroundOne.MC.ResurrectPlayer(1);
                }
                System.Threading.Thread.Sleep(100);

                // 敵撃墜カウントを数える。
                GroundOne.WE2.KillingEnemy++;

                // 練習用の剣カウントを数える。
                if (GroundOne.MC != null)
                {
                    if ((GroundOne.MC.MainWeapon != null) && (GroundOne.MC.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO) ||
                        (GroundOne.MC.SubWeapon != null) && (GroundOne.MC.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO))
                    {
                        GroundOne.WE2.PracticeSwordCount++;
                    }
                }

                if (GroundOne.DuelMode == false)
                {
                    GetExpAndGold();

                    string targetItemName = Method.GetNewItem(Method.NewItemCategory.Battle, GroundOne.MC, ec1, GroundOne.WE.DungeonArea);
                    Debug.Log("targetItemName: " + targetItemName);
                    if (targetItemName != string.Empty)
                    {
                        this.GettingNewItem = new ItemBackPack(targetItemName);
                        if (this.GettingNewItem.Rare == ItemBackPack.RareLevel.Epic)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_GET_EPIC_ITEM);
                        }
                        else if (this.GettingNewItem.Rare == ItemBackPack.RareLevel.Rare)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_GET_RARE_ITEM);
                        }
                        MessageDisplayWithIcon(new ItemBackPack(targetItemName));
                        this.Filter.SetActive(true);
                        treasurePanel.SetActive(true);

                        if (GetNewItem(this.GettingNewItem))
                        {
                            // バックパックが空いてて入手可能な場合、ここでは何もしない。
                            return; // scenebackさせない
                        }
                        else
                        {
                            // バックパックがいっぱいの場合ステータス画面で不要アイテムを捨てさせます。
                            SceneDimension.CallTruthStatusPlayer(this, true, string.Empty);
                            return; // scenebackさせない

                        }
                    }
                }
            }

            Debug.Log("BattleResult: " + GroundOne.BattleResult.ToString());
            GroundOne.StopDungeonMusic();
            SceneDimension.CallBackBattleEnemy();
        }

        private void GetExpAndGold()
        {
            if (GroundOne.WE.AvailableFirstCharacter)
            {
                // ダンジョン階層のクリア状況に応じてMAXレベル制限を設ける。
                if (GroundOne.MC != null && GroundOne.MC.Level < Method.GetMaxLevel())
                {
                    GroundOne.MC.Exp += ec1.Exp;
                }
                GroundOne.MC.Gold += ec1.Gold;

                int levelUpPoint = 0;
                int cumultiveLvUpValue = 0;
                while (true)
                {
                    if (GroundOne.MC.Exp >= GroundOne.MC.NextLevelBorder && GroundOne.MC.Level < Method.GetMaxLevel())
                    {
                        levelUpPoint += GroundOne.MC.LevelUpPointTruth;
                        GroundOne.MC.BaseLife += GroundOne.MC.LevelUpLifeTruth;
                        GroundOne.MC.BaseMana += GroundOne.MC.LevelUpManaTruth;
                        GroundOne.MC.Exp = GroundOne.MC.Exp - GroundOne.MC.NextLevelBorder;
                        GroundOne.MC.Level += 1;
                        cumultiveLvUpValue++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (cumultiveLvUpValue > 0)
                {
                    GroundOne.Player1Levelup = true;
                    GroundOne.Player1CumultiveLvUpValue = cumultiveLvUpValue;
                    GroundOne.Player1UpPoint = levelUpPoint;
                }
            }

            if (GroundOne.WE.AvailableSecondCharacter)
            {
                // ダンジョン階層のクリア状況に応じてMAXレベル制限を設ける。
                if (GroundOne.SC != null && GroundOne.SC.Level < Method.GetMaxLevel())
                {
                    GroundOne.SC.Exp += ec1.Exp;
                }

                int levelUpPoint = 0;
                int cumultiveLvUpValue = 0;
                while (true)
                {
                    if (GroundOne.SC.Exp >= GroundOne.SC.NextLevelBorder && GroundOne.SC.Level < Method.GetMaxLevel())
                    {
                        levelUpPoint += GroundOne.SC.LevelUpPointTruth;
                        GroundOne.SC.BaseLife += GroundOne.SC.LevelUpLifeTruth;
                        GroundOne.SC.BaseMana += GroundOne.SC.LevelUpManaTruth;
                        GroundOne.SC.Exp = GroundOne.SC.Exp - GroundOne.SC.NextLevelBorder;
                        GroundOne.SC.Level += 1;
                        cumultiveLvUpValue++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (cumultiveLvUpValue > 0)
                {
                    GroundOne.Player2Levelup = true;
                    GroundOne.Player2CumultiveLvUpValue = cumultiveLvUpValue;
                    GroundOne.Player2UpPoint = levelUpPoint;
                }
            }

            if (GroundOne.WE.AvailableThirdCharacter)
            {
                // ダンジョン階層のクリア状況に応じてMAXレベル制限を設ける。
                if (GroundOne.TC != null && GroundOne.TC.Level < Method.GetMaxLevel())
                {
                    GroundOne.TC.Exp += ec1.Exp;
                }

                int levelUpPoint = 0;
                int cumultiveLvUpValue = 0;
                while (true)
                {
                    if (GroundOne.TC.Exp >= GroundOne.TC.NextLevelBorder && GroundOne.TC.Level < Method.GetMaxLevel())
                    {
                        levelUpPoint += GroundOne.TC.LevelUpPointTruth;
                        GroundOne.TC.BaseLife += GroundOne.TC.LevelUpLifeTruth;
                        GroundOne.TC.BaseMana += GroundOne.TC.LevelUpManaTruth;
                        GroundOne.TC.Exp = GroundOne.TC.Exp - GroundOne.TC.NextLevelBorder;
                        GroundOne.TC.Level += 1;
                        cumultiveLvUpValue++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (cumultiveLvUpValue > 0)
                {
                    GroundOne.Player3Levelup = true;
                    GroundOne.Player3CumultiveLvUpValue = cumultiveLvUpValue;
                    GroundOne.Player3UpPoint = levelUpPoint;
                }
            }
        }


        public void battleSpeedBar_Scroll(Slider sender)
        {
            switch ((int)(sender.value))
            {
                case 1:
                    Database.BATTLE_CORE_SLEEP = 40;
                    TimeSpeedLabel.text = "時間速度 x0.25";
                    break;
                case 2:
                    Database.BATTLE_CORE_SLEEP = 30;
                    TimeSpeedLabel.text = "時間速度 x0.37";
                    break;
                case 3:
                    Database.BATTLE_CORE_SLEEP = 20;
                    TimeSpeedLabel.text = "時間速度 x0.50";
                    break;
                case 4:
                    Database.BATTLE_CORE_SLEEP = 15;
                    TimeSpeedLabel.text = "時間速度 x0.75";
                    break;
                case 5:
                    Database.BATTLE_CORE_SLEEP = 10;
                    TimeSpeedLabel.text = "時間速度 x1.00";
                    break;
                case 6:
                    Database.BATTLE_CORE_SLEEP = 8;
                    TimeSpeedLabel.text = "時間速度 x1.50";
                    break;
                case 7:
                    Database.BATTLE_CORE_SLEEP = 5;
                    TimeSpeedLabel.text = "時間速度 x2.00";
                    break;
                case 8:
                    Database.BATTLE_CORE_SLEEP = 3;
                    TimeSpeedLabel.text = "時間速度 x3.00";
                    break;
                case 9:
                    Database.BATTLE_CORE_SLEEP = 1;
                    TimeSpeedLabel.text = "時間速度 x4.00";
                    break;
                default:
                    Database.BATTLE_CORE_SLEEP = 10;
                    TimeSpeedLabel.text = "時間速度 x1.00";
                    break;
            }
        }
        /// <summary>
        /// 物理攻撃上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPhysicalAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【物理攻撃】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
            player.CurrentPhysicalAttackUp = turn;
            player.CurrentPhysicalAttackUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, turn);
        }

        /// <summary>
        /// 物理攻撃減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPhysicalAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPhysicalAttackDown)
            {
                UpdateBattleText(player.FirstName + "は、物理攻撃DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ROYAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ROYAL_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた物理攻撃DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【物理攻撃】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃DOWN");
            player.CurrentPhysicalAttackDown = turn;
            player.CurrentPhysicalAttackDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalAttackDown, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_DOWN, turn);
        }

        /// <summary>
        /// 魔法攻撃上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpMagicAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【魔法攻撃】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃UP");
            player.CurrentMagicAttackUp = turn;
            player.CurrentMagicAttackUpValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, turn);
        }

        /// <summary>
        /// 魔法攻撃減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownMagicAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistMagicAttackDown)
            {
                UpdateBattleText(player.FirstName + "は、魔法攻撃DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ELEMENTAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ELEMENTAL_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた魔法攻撃DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【魔法攻撃】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃DOWN");
            player.CurrentMagicAttackDown = turn;
            player.CurrentMagicAttackDownValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicAttackDown, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_DOWN, turn);
        }

        /// <summary>
        /// 物理防御上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPhysicalDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【物理防御】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "物理防御UP");
            player.CurrentPhysicalDefenseUp = turn;
            player.CurrentPhysicalDefenseUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, turn);
        }

        /// <summary>
        /// 物理防御減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPhysicalDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPhysicalDefenseDown)
            {
                UpdateBattleText(player.FirstName + "は、物理防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ROYAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ROYAL_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた物理防御DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【物理防御】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "物理防御DOWN");
            player.CurrentPhysicalDefenseDown = turn;
            player.CurrentPhysicalDefenseDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalDefenseDown, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_DOWN, turn);
        }

        /// <summary>
        /// 魔法防御上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpMagicDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【魔法防御】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "魔法防御UP");
            player.CurrentMagicDefenseUp = turn;
            player.CurrentMagicDefenseUpValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, turn);
        }

        /// <summary>
        /// 魔法防御減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownMagicDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistMagicDefenseDown)
            {
                UpdateBattleText(player.FirstName + "は、魔法防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ELEMENTAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ELEMENTAL_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた魔法防御DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【魔法防御】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "魔法防御DOWN");
            player.CurrentMagicDefenseDown = turn;
            player.CurrentMagicDefenseDownValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicDefenseDown, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_DOWN, turn);
        }

        /// <summary>
        /// 戦闘速度上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpBattleSpeed(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【戦闘速度】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "戦闘速度UP");
            player.CurrentSpeedUp = turn;
            player.CurrentSpeedUpValue = (int)effectValue;
            player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, turn);
        }

        /// <summary>
        /// 戦闘速度減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownBattleSpeed(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistBattleSpeedDown)
            {
                UpdateBattleText(player.FirstName + "は、戦闘防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_HAYATE_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_HAYATE_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた戦闘速度DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【戦闘速度】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "戦闘速度DOWN");
            player.CurrentSpeedDown = turn;
            player.CurrentSpeedDownValue = (int)effectValue;
            player.ActivateBuff(player.pbSpeedDown, Database.BaseResourceFolder + Database.BUFF_SPEED_DOWN, turn);
        }

        /// <summary>
        /// 戦闘反応上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpBattleReaction(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【戦闘反応】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "戦闘反応UP");
            player.CurrentReactionUp = turn;
            player.CurrentReactionUpValue = (int)effectValue;
            player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + Database.BUFF_REACTION_UP, turn);
        }

        /// <summary>
        /// 戦闘反応減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownBattleReaction(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistBattleResponseDown)
            {
                UpdateBattleText(player.FirstName + "は、戦闘反応DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_HAYATE_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_HAYATE_GUARD_RING)))
            {
                UpdateBattleText(player.FirstName + "にかけられた戦闘反応DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.FirstName + "は【戦闘反応】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "戦闘反応DOWN");
            player.CurrentReactionDown = turn;
            player.CurrentReactionDownValue = (int)effectValue;
            player.ActivateBuff(player.pbReactionDown, Database.BaseResourceFolder + Database.BUFF_REACTION_DOWN, turn);
        }

        /// <summary>
        /// 潜在能力上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPotential(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.FirstName + "は【潜在能力】が" + effectValue.ToString() + "上昇\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "潜在能力UP");
            player.CurrentPotentialUp = turn;
            player.CurrentPotentialUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPotentialUp, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_UP, turn);
        }

        /// <summary>
        /// 潜在能力減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPotential(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPotentialDown)
            {
                UpdateBattleText(player.FirstName + "は、潜在能力DOWN効果を受けなかった！\r\n");
                return;
            }
            UpdateBattleText(player.FirstName + "は【潜在能力】が" + effectValue.ToString() + "減少\r\n");
            AnimationDamage(0, player, 0, Color.black, false, false, "潜在能力DOWN");
            player.CurrentPotentialDown = turn;
            player.CurrentPotentialDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPotentialDown, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_DOWN, turn);
        }
    }
}

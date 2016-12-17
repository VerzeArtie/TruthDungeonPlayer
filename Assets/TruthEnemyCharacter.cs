using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DungeonPlayer
{
    public partial class TruthEnemyCharacter : MainCharacter
    {
        public enum TargetLogic
        {
            Front,
            Back,
        }

        public enum RareString
        {
            Legendary,
            Purple,
            Gold,
            Red,
            Blue,
            Black
        }

        public enum ArmorType
        {
            Normal,
            Regist_Physical,
            Regist_Magic,
            Regist_Both
        }

        public enum MonsterArea
        {
            Area11,
            Area12,
            Area13,
            Area14,
            Boss1,
            TruthBoss1,
            Area21,
            Area22,
            Area23,
            Area24,
            Boss21,
            Boss22,
            Boss23,
            Boss24,
            Boss25,
            Boss2,
            TruthBoss2,
            Area31,
            Area32,
            Area33,
            Area34,
            Boss3,
            TruthBoss3,
            Area41,
            Area42,
            Area43,
            Area44,
            Boss4,
            TruthBoss4,
            Area51,
            Boss5,
            TruthBoss5,
            Area46,
            Duel,
            LastBoss,
        }

        protected TargetLogic _initialTarget = TargetLogic.Front;
        public TargetLogic InitialTarget
        {
            get { return _initialTarget; }
            set { _initialTarget = value; }
        } // 先頭開始時のターゲット選定

        public bool UseStackCommand { get; set; } // 敵がボスとしてスタックコマンドを使うかどうかのフラグ（Falseなら直接行動的）
        public bool DetectCannotBeStun { get; set; } // 敵がスタン耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSilence { get; set; } // 敵が沈黙耐性があるかどうかを知るフラグ
        public bool DetectCannotBePoison { get; set; } // 敵が猛毒耐性があるかどうかを知るフラグ
        public bool DetectCannotBeTemptation { get; set; } // 敵が誘惑耐性があるかどうかを知るフラグ
        public bool DetectCannotBeFrozen { get; set; } // 敵が凍結耐性があるかどうかを知るフラグ
        public bool DetectCannotBeParalyze { get; set; } // 敵が麻痺耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSlow { get; set; } // 敵が鈍化耐性があるかどうかを知るフラグ
        public bool DetectCannotBeBlind { get; set; } // 敵が暗闇耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSlip { get; set; } // 敵がスリップ耐性があるかどうかを知るフラグ
        public bool DetectCannotBeNoResurrection { get; set; } // 敵が蘇生不可耐性があるかどうかを知るフラグ
        public bool DetectCannotBeNoGainLife { get; set; } // 敵がライフ回復不可耐性があるかどうかを知るフラグ
        public bool DetectDeath { get; set; } // 敵が自分が一旦死亡した事を知るフラグ（レギィンアーゼLv3専用）

        public int Pattern1 { get; set; } // Bystander戦術１
        public int Pattern2 { get; set; } // Bystander戦術２
        public int Pattern3 { get; set; } // Bystander戦術３
        public int Pattern4 { get; set; } // Bystander戦術４
        public int Pattern5 { get; set; } // Bystander戦術５
        public int Pattern6 { get; set; } // Bystander戦術６
        public int TimeCumulative { get; set; } // Bystander戦術時間累積カウント

        public bool StillNotAction1 { get; set; } // 一度も任意のコマンドを発動していない場合を示すフラグ、DUEL最終戦ラナとの戦闘でつけたフラグ
        public bool StillNotAction2 { get; set; } // 一度も任意のコマンドを発動していない場合を示すフラグ、DUEL最終戦ラナとの戦闘でつけたフラグ
        public bool OpponentUseInstantPoint { get; set; } // 対戦相手が一度も任意の【インスタントorソーサリー】行動をしてない事を示すフラグ、DUEL最終戦オルとの戦闘

        public int AI_TacticsNumber { get; set; } // 戦術を切り替えるために使用

        public bool BossBeforeStay { get; set; }
        public MonsterArea Area { get; set; }
        public string Description { get; set; }
        public RareString Rare { get; set; }
        public ArmorType Armor { get; set; }
        public string[] DropItem { get; set; }

        public static int MAX_DROPITEM_SIZE = 10;

        public bool AddDropItem(string dropitem)
        {
            for (int ii = 0; ii < MAX_DROPITEM_SIZE; ii++)
            {
                if (this.DropItem[ii] == string.Empty)
                {
                    this.DropItem[ii] = dropitem;
                    return true;
                }
            }
            return false;
        }

        public MainCharacter Targetting(MainCharacter mc, MainCharacter sc, MainCharacter tc)
        {
            if (this.InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
            {
                if (tc != null && tc.Dead == false) { return tc; }
                else if (sc != null && sc.Dead == false) { return sc; }
                else if (mc != null && mc.Dead == false) { return mc; }
            }
            else if (this.InitialTarget == TargetLogic.Front)
            {
                if (mc != null && mc.Dead == false) { return mc; }
                else if (sc != null && sc.Dead == false) { return sc; }
                else if (tc != null && tc.Dead == false) { return tc; }
            }

            return mc;
        }

        private bool SearchItem(string ItemName)
        {
            ItemBackPack[] tempItem = this.GetBackPackInfo();
            foreach (ItemBackPack value in tempItem)
            {
                if (value != null)
                {
                    if (value.Name == ItemName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void NextAttackDecision(MainCharacter target,
                                        MainCharacter mc,
                                        MainCharacter sc,
                                        MainCharacter tc,
                                        TruthEnemyCharacter ec1,
                                        TruthEnemyCharacter ec2,
                                        TruthEnemyCharacter ec3)
        {
            System.Random rd = new System.Random(Environment.TickCount * DateTime.Now.Millisecond);
            switch (this.FirstName)
            {
                #region "１階"
                case Database.ENEMY_HIYOWA_BEATLE:
                    SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                    break;

                case Database.ENEMY_HENSYOKU_PLANT:
                    SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                    break;

                case Database.ENEMY_GREEN_CHILD:
                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.MAGIC_ATTACK);
                    break;

                case Database.ENEMY_TINY_MANTIS:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "振り上げるカマ");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_KOUKAKU_WURM:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.TOSSIN);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_MANDRAGORA:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "超音波");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                            break;
                    }
                    break;

                case Database.ENEMY_SUN_FLOWER:
                    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                    break;

                case Database.ENEMY_RED_HOPPER:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_EARTH_SPIDER:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                        case 1:
                            if (target.CurrentSlow <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "蜘蛛の糸");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WILD_ANT:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "かみつき");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_ALRAUNE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "怪しげな花弁");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DARK_BLAST);
                            break;
                        case 2:
                            if (target.CurrentPoison <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "猛毒の花粉");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DARK_BLAST);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_POISON_MARY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "毒胞子");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "幻覚胞子");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DEVOURING_PLAGUE);
                            break;
                    }
                    break;

                case Database.ENEMY_ZASSYOKU_RABBIT:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.TOSSIN);
                            break;
                        case 1:
                            if (this.CurrentStrengthUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, Database.BUFFUP_STRENGTH);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.TOSSIN);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_SPEEDY_TAKA:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_ASH_CREEPER:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                        case 1:
                            if (target.CurrentSlow <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "まとわりつく");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_GIANT_SNAKE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                        case 1:
                            if (target.CurrentPoison <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "毒かみつき");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WONDER_SEED:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "棘殻ローリング");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ニードル・スピア");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                    }
                    break;

                case Database.ENEMY_FLANSIS_KNIGHT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "なぎ払い");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ファイア・ランス");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_SHOTGUN_HYUI:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヒューイ弾丸");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ホウセンの種");
                            break;
                        case 2:
                            if (this.CurrentStrengthUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, Database.BUFFUP_STRENGTH);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヒューイ弾丸");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WAR_WOLF:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.TOSSIN);
                            break;
                    }
                    break;

                case Database.ENEMY_BRILLIANT_BUTTERFLY:
                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.MAGIC_ATTACK);
                    break;

                case Database.ENEMY_MIST_ELEMENTAL:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                            break;
                        case 1:
                            if (target.CurrentBlind <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ホワイトミスト");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WHISPER_DRYAD:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                            break;
                        case 1:
                            if (target.CurrentPoisonValue <= 2)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "猛毒のバラ");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BLOOD_MOSS:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "赤い胞子");
                            break;
                    }
                    break;

                case Database.ENEMY_MOSSGREEN_DADDY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                            break;
                        case 1:
                            if (this.CurrentShadowPact <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SHADOW_PACT);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "エンタングル");
                            break;

                    }
                    break;

                case Database.ENEMY_BOSS_KARAMITUKU_FLANSIS:
                    int rand = AP.Math.RandomInteger(5);
                    if (this.BossBeforeStay)
                    {
                        rand = 3;
                    }
                    // Tac0：始まりは毒胞子による猛毒と暗闇から始める。Tac1へ移行。
                    // Tac1：レッドローズブラスト、または連槍突進、またはファイアビューネを行う。Tac2へ移行。
                    // Tac2：絡み蔦は相手がSlowではない場合、行う。そうでない場合、毒胞子を続けて放つ。Tac1へ移行。
                    // 　　　なお、毒カウンターは累積する。毒カウンターが増加するたび、猛毒効果ダメージは上昇する。
                    // キル・スピニングランサーはインスタント行動がたまったときにスタックコマンドで発動する。
                    if ((mc != null) && (mc.CurrentPoison <= 0) && (sc != null) && (sc.CurrentPoison <= 0))
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "黒の毒胞子");
                    }
                    else
                    {
                        if (this.AI_TacticsNumber == 0)
                        {
                            switch (AP.Math.RandomInteger(3))
                            {
                                case 0:
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, "レッドローズブラスト");
                                    break;
                                case 1:
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, "連槍突進");
                                    break;
                                case 2:
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ファイアビューネ");
                                    break;
                            }

                            this.AI_TacticsNumber = 1;
                        }
                        else
                        {
                            if ((mc != null) && (mc.CurrentSlow <= 0) && (sc != null) && (sc.CurrentSlow <= 0))
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "絡み蔦");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "黒の毒胞子");
                            }

                            this.AI_TacticsNumber = 0;
                        }
                    }
                    break;
                #endregion
                #region "２階"
                case Database.ENEMY_DAGGER_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "乱れ噛みつき");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_SIPPU_FLYING_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            break;
                    }
                    break;

                case Database.ENEMY_ORB_SHELLFISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FROZEN_LANCE);
                            break;
                        case 1:
                            if (this.currentChargeCount <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.Charge, Database.TAMERU_EN);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FROZEN_LANCE);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_SPLASH_KURIONE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "透明な光");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "共鳴波");
                            break;
                    }
                    break;

                case Database.ENEMY_TRANSPARENT_UMIUSHI:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FROZEN_LANCE);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "吸い取り");
                            break;
                        case 2:
                            if (this.CurrentMagicDefenseUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "透明化");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FROZEN_LANCE);
                            }
                            break;

                    }
                    break;

                case Database.ENEMY_ROLLING_MAGURO:
                    if (this.Target.FirstName == GroundOne.MC.FirstName)
                    {
                        if ((GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && GroundOne.SC.Dead) &&
                            (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && GroundOne.TC.Dead))
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ローリング突進");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "捕獲選定");
                        }
                    }
                    else
                    {
                        SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "ローリング突進");
                    }
                    break;

                case Database.ENEMY_RANBOU_SEA_ARTINE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "トゲの放射");
                            break;
                        case 1:
                            if (this.CurrentPhysicalAttackUp <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "表面膨張");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "トゲの放射");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BLUE_SEA_WASI:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "金切り声");
                            break;
                    }
                    break;

                case Database.ENEMY_BRIGHT_SQUID:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.HOLY_SHOCK);
                            break;
                        case 1:
                            if (target.CurrentBlind <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "フラッシュ");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.HOLY_SHOCK);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_GANGAME:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "地響き");
                            break;
                        case 1:
                            if (this.CurrentPhysicalDefenseUp <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "タートル・シェル");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "がぶりつき");
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "がぶりつき");
                            break;
                    }
                    break;

                case Database.ENEMY_BIGMOUSE_JOE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (this.CurrentAgilityUp <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "伸張する舌");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "トリプル・パンチ");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "トリプル・パンチ");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "異常な奇声");
                            break;
                    }
                    break;

                case Database.ENEMY_MOGURU_MANTA:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "流水の渦巻き");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "流水の突壁");
                            break;
                    }
                    break;

                case Database.ENEMY_FLOATING_GOLD_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "鉄砲泡");
                            break;
                        case 1:
                            if (this.CurrentSpeedUp <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "水面跳躍");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "鉄砲泡");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_GOEI_HERMIT_CLUB:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "豪腕ハサミ");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "突進バサミ");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ダブル・ハサミ");
                            break;
                    }
                    break;

                case Database.ENEMY_ABARE_SHARK:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "のこぎり歯");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "食い散らかし");
                            break;
                    }
                    break;

                case Database.ENEMY_VANISHING_CORAL:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "コーラル・サウンド");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "バニッシング・エフェクト");
                            break;
                        case 2:
                            if (this.currentLife <= this.MaxLife / 5)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ラスト・バウンド");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "コーラル・サウンド");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_CASSY_CANCER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ベタつく緑泡");
                            break;
                        case 1:
                            if (this.CurrentPhysicalDefenseUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "甲殻増強");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "キャンサー・ブロー");
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "キャンサー・ブロー");
                            break;
                    }
                    break;
                    
                case Database.ENEMY_BLACK_STARFISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                            break;
                        case 1:
                            if (target.CurrentBlackFire <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLACK_FIRE);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_RAINBOW_ANEMONE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.VANISH_WAVE);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.PSYCHIC_WAVE);
                            break;
                    }
                    break;

                case Database.ENEMY_MACHIBUSE_ANKOU:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "飛びかかり");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "かぶりつき");
                            break;
                    }
                    break;

                case Database.ENEMY_EDGED_HIGH_SHARK:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "猛突撃");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "貪欲な咬みつき");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴァイオレンス・テール");
                            break;
                    }
                    break;

                case Database.ENEMY_EIGHT_EIGHT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "【八】はがい絞め");
                            break;
                        case 1:
                            if (this.CurrentMagicAttackUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ブチ巻く黒墨");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "黒墨ミサイル");
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "大渦巻き");
                            break;
                    }
                    break;

                // ２階、力の部屋ボス１
                case Database.ENEMY_BRILLIANT_SEA_PRINCE:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if ((this.CurrentPhysicalDefenseUp <= 0 && this.CurrentMagicDefenseUp <= 0 && this.CurrentSpeedUp <= 0) &&
                                (this.CurrentStrengthUp <= 0 && this.CurrentIntelligenceUp <= 0 && this.CurrentMindUp <= 0))
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "シースライドウォータ");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                        case 1:
                            if ((this.CurrentStrengthUp <= 0 && this.CurrentIntelligenceUp <= 0 && this.CurrentMindUp <= 0) &&
                                (this.CurrentPhysicalDefenseUp <= 0 && this.CurrentMagicDefenseUp <= 0 && this.CurrentSpeedUp <= 0))
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "勇敢な雄叫び");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                        case 2:
                            if (this.CurrentStrengthUp > 0 || this.CurrentIntelligenceUp > 0 || this.CurrentMindUp > 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "グングニル・スラッシュ");
                            }
                            else if (this.CurrentPhysicalDefenseUp > 0 || this.CurrentMagicDefenseUp > 0 || this.CurrentSpeedUp > 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "グングニルの閃光");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                    }
                    break;

                // ２階、力の部屋ボス２
                case Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN:
                    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FROZEN_LANCE);
                    break;

                // ２階、力の部屋ボス３
                case Database.ENEMY_SHELL_SWORD_KNIGHT:
                    rand = AP.Math.RandomInteger(4);
                    if (this.CurrentWordOfFortune > 0)
                    {
                        rand = 0;
                    }
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "シー・ストライプ");
                            break;
                        case 1:
                            if (this.CurrentWordOfFortune <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ブリンク・シェル");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "シー・ストライプ");
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "深海の渦");
                            break;
                        case 3:
                            if (this.CurrentPhysicalAttackUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "海星源への忠誠");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "シー・ストライプ");
                            }
                            break;
                    }
                    break;

                // ２階、力の部屋ボス４－１
                case Database.ENEMY_JELLY_EYE_BRIGHT_RED:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "燃え盛る炎弾丸");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ファイア・ウォール");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ブレイジング・ストーム");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "フラッシュ・バーン");
                            break;
                    }
                    break;

                // ２階、力の部屋ボス４－２
                case Database.ENEMY_JELLY_EYE_DEEP_BLUE:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "凍てつく氷弾丸");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ウォータ・バブル");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ウォーター・スラッシュ");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ハルシネイト・アイ");
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５－１
                case Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU:
                    rand = AP.Math.RandomInteger(2);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スターソード『煌』");
                            break;

                        case 1:
                            if (this.Target2 != null && !this.Target2.Dead && this.Target2.CurrentPhysicalDefenseUp <= 0)
                            {
                                SetupActionCommand(this, this.Target2, PlayerAction.SpecialSkill, "エーギル・フィールド");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スターソード『煌』");
                            }
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５ー２
                case Database.ENEMY_SEA_STAR_KNIGHT_AMARA:
                    rand = AP.Math.RandomInteger(2);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スターソード『艶』");
                            break;

                        case 1:
                            if (this.Target2 != null && !this.Target2.Dead && this.Target2.CurrentPhysicalDefenseUp <= 0)
                            {
                                SetupActionCommand(this, this.Target2, PlayerAction.SpecialSkill, "アマラ・フィールド");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スターソード『艶』");
                            }
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５－３
                case Database.ENEMY_SEA_STAR_ORIGIN_KING:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (ec2 != null && !ec2.Dead && ec2.CurrentProtection <= 0)
                            {
                                this.Target2 = ec2;
                                SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.PROTECTION);
                            }
                            else if (ec3 != null && !ec3.Dead && ec3.CurrentProtection <= 0)
                            {
                                this.Target2 = ec3;
                                SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.PROTECTION);
                            }
                            else if (this.CurrentProtection <= 0)
                            {
                                this.Target2 = this;
                                SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.PROTECTION);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.HOLY_SHOCK);
                            }
                            break;
                        case 1:
                            if (ec2 != null && !ec2.Dead && ec2.CurrentSaintPower <= 0)
                            {
                                this.Target2 = ec2;
                                SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.SAINT_POWER);
                            }
                            else if (ec3 != null && !ec3.Dead && ec3.CurrentSaintPower <= 0)
                            {
                                this.Target2 = ec3;
                                SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.SAINT_POWER);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.HOLY_SHOCK);
                            }
                            break;

                        case 2:
                            if (ec2 != null && !ec2.Dead && ec3 != null && !ec3.dead)
                            {
                                if ((ec2.CurrentLife >= ec2.MaxLife) && (ec3.CurrentLife >= ec3.MaxLife))
                                {
                                    this.Target2 = this;
                                }
                                else if ((ec2.CurrentLife < ec2.MaxLife) && (ec3.CurrentLife >= ec3.MaxLife))
                                {
                                    this.Target2 = ec2;
                                }
                                else if ((ec2.CurrentLife >= ec2.MaxLife) && (ec3.CurrentLife < ec3.MaxLife))
                                {
                                    this.Target2 = ec3;
                                }
                                else if (ec2.CurrentLife < ec3.CurrentLife)
                                {
                                    this.Target2 = ec2;
                                }
                                else
                                {
                                    this.Target2 = ec3;
                                }
                            }
                            else if (ec2 != null && ec2.dead && ec3 != null && !ec3.dead)
                            {
                                this.Target2 = ec3;
                            }
                            else if (ec2 != null && !ec2.dead && ec3 != null && ec3.dead)
                            {
                                this.Target2 = ec2;
                            }
                            else
                            {
                                this.Target2 = this;
                            }
                            SetupActionCommand(this, this.Target2, PlayerAction.UseSpell, Database.FRESH_HEAL);
                            break;
                    }
                    break;

                // ２階、力の部屋ボス６
                case Database.ENEMY_BOSS_LEVIATHAN:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            if ((this.CurrentLife <= this.MaxLife / 2) && (this.CurrentPhysicalAttackUp <= 0))
                            {
                                this.Target2 = this;
                                SetupActionCommand(this, this.Target2, PlayerAction.SpecialSkill, "海王の咆哮");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "バースト・クラウド");
                            }
                            break;

                        case 1:
                            if ((this.CurrentLife <= this.MaxLife / 2) && (this.CurrentPhysicalAttackUp <= 0))
                            {
                                this.Target2 = this;
                                SetupActionCommand(this, this.Target2, PlayerAction.SpecialSkill, "海王の咆哮");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "大激衝");
                            }
                            break;

                        case 2:
                            if ((this.CurrentLife <= this.MaxLife / 2) && (this.CurrentPhysicalAttackUp <= 0))
                            {
                                this.Target2 = this;
                                SetupActionCommand(this, this.Target2, PlayerAction.SpecialSkill, "海王の咆哮");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "サージェティック・バインド");
                            }
                            break;

                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "大激衝");
                            break;
                    }
                    break;

                // １～５階、支配竜
                case Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD:
                case Database.ENEMY_DRAGON_TINKOU_DEEPSEA:
                case Database.ENEMY_DRAGON_DESOLATOR_AZOLD:
                    if (this.AI_TacticsNumber < 5)
                    {
                        SetupActionCommand(this, this, PlayerAction.SpecialSkill, "無音の呼び声");
                    }
                    else
                    {
                        SetupActionCommand(this, this, PlayerAction.SpecialSkill, "形成消失");
                    }
                    break;
                #endregion
                #region "３階"
                case Database.ENEMY_TOSSIN_ORC:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "突貫");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "暴走");
                            break;
                    }
                    break;

                case Database.ENEMY_SNOW_CAT:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, Database.RENZOKU_ATTACK);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "凍りつく吹雪");
                            break;
                    }
                    break;

                case Database.ENEMY_WAR_MAMMOTH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "蹂躙");
                            break;
                        case 1:
                            if (this.CurrentPhysicalChargeCount <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ためる");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "蹂躙");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WINGED_COLD_FAIRY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "プチ・ブリザード");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "凍結玉");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ウィンター・ソング");
                            break;
                    }
                    break;

                case Database.ENEMY_FREEZING_GRIFFIN:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (target.CurrentFrozen <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アイス・ウィンド");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "直滑降");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "直滑降");
                            break;
                        case 2:
                            if (this.CurrentSpeedUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "雄叫び");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "直滑降");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BRUTAL_OGRE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ぶん投げる");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "氷の儀式");
                            break;
                    }
                    break;

                case Database.ENEMY_HYDRO_LIZARD:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "リザード・スラッシュ");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アイシクル・ブレード");
                            break;
                    }
                    break;

                case Database.ENEMY_PENGUIN_STAR:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ペンギンの輝き！");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ペンギンアタック！");
                            break;
                    }
                    break;

                case Database.ENEMY_ICEBERG_SPIRIT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "エコーヴォイス");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "巨大な氷針の嵐");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                            break;
                    }
                    break;

                case Database.ENEMY_SWORD_TOOTH_TIGER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "サーヴェルクロー");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "目くらまし");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "連速三段");
                            break;
                    }
                    break;

                case Database.ENEMY_FEROCIOUS_RAGE_BEAR:
                    switch (AP.Math.RandomInteger(4))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "四歯戦速");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "自己増強");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "漸波動");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "食いちぎり");
                            break;
                    }
                    break;

                case Database.ENEMY_WINTER_ORB:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "氷の結晶術");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "冷気の射出");
                            break;
                    }
                    break;

                case Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI:
                    string COMMAND_1 = "津波の呼び声";
                    string COMMAND_2 = "平穏の呼び声";
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this == ec2)
                            {
                                if (ec1.ActionLabel.text == COMMAND_1)
                                {
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_2);
                                }
                                else
                                {
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_1);
                                }
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_1);
                            }
                            break;
                        case 1:
                            if (this == ec2)
                            {
                                if (ec1.ActionLabel.text == COMMAND_2)
                                {
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_1);
                                }
                                else
                                {
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_2);
                                }
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, COMMAND_2);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_MAJESTIC_CENTAURUS:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (target.CurrentFrozen <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アイスランス");
                            }
                            else if (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                        case 1:
                            if (target.CurrentPhysicalDefenseDown <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "圧力ある雄叫び");
                            }
                            else if (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                        case 2:
                            if (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_INTELLIGENCE_ARGONIAN:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.FROZEN_AURA);
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "打突");
                            break;
                    }
                    break;

                case Database.ENEMY_MAGIC_HYOU_RIFLE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "雹弾乱射");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ＳＰＬＡＳＨ！");
                            break;
                        case 2:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "マジックバリア");
                            break;
                    }
                    break;

                case Database.ENEMY_PURE_BLIZZARD_CRYSTAL:
                    switch (AP.Math.RandomInteger(4))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ブリザード");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "零式");
                            break;
                        case 2:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "蒼授の気配");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "絶・スピニングランサー");
                            break;
                    }
                    break;

                case Database.ENEMY_PURPLE_EYE_WARE_WOLF:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this.CurrentStrengthUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "バトルクライ");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "キリング・スラッシュ");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "キリング・スラッシュ");
                            break;
                    }
                    break;

                case Database.ENEMY_FROST_HEART:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this.CurrentMagicAttackUpValue <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "冷気圧縮");
                            }
                            else if (this.currentLife > 1)
                            {
                                List<MainCharacter> group = new List<MainCharacter>();
                                if (mc != null && !mc.Dead) { group.Add(mc); }
                                if (sc != null && !sc.Dead) { group.Add(sc); }
                                if (tc != null && !tc.Dead) { group.Add(tc); }
                                int randomValue = AP.Math.RandomInteger(group.Count);
                                this.Target = group[randomValue];
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "大暴発");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                            }
                            break;

                        case 1:
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                            break;
                    }
                    break;

                case Database.ENEMY_WHITENIGHT_GRIZZLY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "三本爪");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ぶんまわし");
                            break;
                        case 2:
                            if (this.CurrentPhysicalAttackUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "一発気合");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ぶんまわし");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_WIND_BREAKER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (this.CurrentGaleWind <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ソード・オブ・ウィンド");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "断空");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "断空");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アイス・トルネード");
                            break;
                    }
                    break;

                case Database.ENEMY_TUNDRA_LONGHORN_DEER:
                    List<MainCharacter> group_deer = new List<MainCharacter>();
                    if (mc != null && !mc.Dead) { group_deer.Add(mc); }
                    if (sc != null && !sc.Dead) { group_deer.Add(sc); }
                    if (tc != null && !tc.Dead) { group_deer.Add(tc); }
                    int randomValue_deer = AP.Math.RandomInteger(group_deer.Count);
                    this.Target = group_deer[randomValue_deer];
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (group_deer[randomValue_deer].CurrentReactionDown <= 0)
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "トキのコエ");
                            }
                            else
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "レッドスノーホーン");
                            }
                            break;
                        case 1:
                            if (group_deer[randomValue_deer].CurrentPhysicalAttackDown <= 0)
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "氷雪化現象");
                            }
                            else
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "レッドスノーホーン");
                            }
                            break;
                        case 2:
                            if (group_deer[randomValue_deer].CurrentMagicDefenseDown <= 0)
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "無音音響の和");
                            }
                            else
                            {
                                SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "レッドスノーホーン");
                            }
                            break;
                    }
                    break;


                // ３階、ボス
                case Database.ENEMY_BOSS_HOWLING_SEIZER:
                    rand = AP.Math.RandomInteger(5);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "もぎとり");
                            break;

                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ブンまわし");
                            break;

                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "異常音響");
                            break;

                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "凍らせる視線");
                            break;

                        case 4:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "破裂する雄叫び");
                            break;
                    }
                    break;
                #endregion
                #region "４階"
                case Database.ENEMY_GENAN_HUNTER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "バインド・ウィップ");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アジテイト・アロー");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デッドリー・ショット");
                            break;
                    }
                    break;

                case Database.ENEMY_BEAST_MASTER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "タイガー・ブロウ");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "圧死の咆哮");
                            break;
                        case 2:
                            if (this.CurrentPhysicalAttackUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "ピューマ・ライジング");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "タイガー・ブロウ");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_ELDER_ASSASSIN:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "フェイタル・ニードル");
                            break;
                        case 1:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "気配抹消");
                            break;
                        case 2:
                            if (target.CurrentPhysicalDefenseDown <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ウロボロスの一撃");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "神速の連撃");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_FALLEN_SEEKER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.CurrentShadowUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "汚れし悪魔契約");
                            }
                            else if (this.CurrentLightUp <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "純潔の聖天使契約");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ホーリー・バレット");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ホーリー・バレット");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "セイント・スラッシュ");
                            break;
                    }
                    break;

                case Database.ENEMY_MEPHISTO_RIGHTARM:
                    switch (AP.Math.RandomInteger(4))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "見えざる暗黒呪文");
                            break;
                        case 1:
                            if (target.CurrentEnrageBlast <= 0 || target.CurrentBlazingField <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "煉獄の禁術");
                            }
                            else
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "地獄の鼓動");
                            }
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デスサイン");
                            break;
                        case 3:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "地獄の鼓動");
                            break;
                    }
                    break;

                case Database.ENEMY_DARK_MESSENGER:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if ((mc != null && mc.CurrentNoResurrection <= 0) ||
                            (sc != null && sc.CurrentNoResurrection <= 0) ||
                            (tc != null && tc.CurrentNoResurrection <= 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "黒龍のささやき");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "チューズン・サクリファイ");
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "チューズン・サクリファイ");
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "死への背徳");
                    }
                    break;

                case Database.ENEMY_MASTER_LOAD:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "スペリオル・フィールド");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ダーク・エリミネイト");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "振動剣");
                            break;
                    }
                    break;

                case Database.ENEMY_EXECUTIONER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.CurrentAfterReviveHalf <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "断罪の加護");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デス・ストライク");
                            }
                            break;
                        case 1:
                            if (target.CurrentAbsoluteZero <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "魂への凍結");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デス・ストライク");
                            }
                            break;
                        case 2:
                            if (target.CurrentDamnation <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "死滅のひと振り");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デス・ストライク");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_MARIONETTE_NEMESIS:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (this.currentParalyze <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "呪いの糸");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴェイパー・ドライブ");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴェイパー・ドライブ");
                            break;
                        case 2:
                            if (target.CurrentSpeedDown <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ホラー・ビジョン");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴェイパー・ドライブ");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BLACKFIRE_MASTER_BLADE:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "螺旋黒炎");
                    }
                    else
                    {
                        rand = AP.Math.RandomInteger(2);
                        switch (rand)
                        {
                            case 0:
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "乱奏連撃");
                                break;
                            case 1:
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ブルー・エクスプロード");
                                break;
                        }
                    }
                    break;

                case Database.ENEMY_SIN_THE_DARKELF:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ネイチャー・エンゼンブル");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "シャープネル・ニードル");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "メギド・ブレイズ");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アーケン・デストラクション");
                            break;
                    }
                    break;

                case Database.ENEMY_SUN_STRIDER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "太陽の滅印");
                            break;
                        case 1:
                            if (target.CurrentDamnation <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ブラック・フレア");
                            }
                            else
                            {
                                if (this.CurrentSaintPower <= 0)
                                {
                                    SetupActionCommand(this, this, PlayerAction.SpecialSkill, "サテライト・エナジー");
                                }
                                else
                                {
                                    SetupActionCommand(this, target, PlayerAction.SpecialSkill, "サテライト・ソード");
                                }
                            }
                            break;
                        case 2:
                            if (this.CurrentSaintPower <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "サテライト・エナジー");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "サテライト・ソード");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_ARC_DEMON:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.CurrentPhysicalAttackUp <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "デビル・プロミス");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ギガント・スレイヤー");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "呪怨殺");
                            break;
                        case 2:
                            List<MainCharacter> group = new List<MainCharacter>();
                            if (mc != null && !mc.Dead) { group.Add(mc); }
                            if (sc != null && !sc.Dead) { group.Add(sc); }
                            if (tc != null && !tc.Dead) { group.Add(tc); }
                            MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                            if (current.CurrentPhysicalAttackDown <= 0)
                            {
                                SetupActionCommand(this, current, PlayerAction.SpecialSkill, "深淵の理");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ギガント・スレイヤー");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BALANCE_IDLE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "全ては灰に");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "生命の輝き");
                            break;
                        case 2:
                            if (this.CurrentAfterReviveHalf <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "オーン・プリゼンス");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "レヴェルの唄");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_UNDEAD_WYVERN:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (target.CurrentStunning <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スカル・クラッシュ");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ボーン・トルネード");
                            }
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "死への囁き");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ボーン・トルネード");
                            break;
                    }
                    break;

                case Database.ENEMY_GO_FLAME_SLASHER:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "煉獄弾");
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((mc != null && !mc.Dead && mc.CurrentFireDamage2 <= 0) ||
                            (sc != null && !sc.Dead && sc.CurrentFireDamage2 <= 0) ||
                            (tc != null && !tc.Dead && tc.CurrentFireDamage2 <= 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "禍の炎");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ジ・エンド");
                        }
                    }
                    break;

                case Database.ENEMY_DEVIL_CHILDREN:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.CurrentBlackMagic <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "暗黒の詠唱術");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "クロマティック・バレット");
                        }
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "無音の真空波");
                    }
                    else if (AI_TacticsNumber == 2)
                    {
                        if (this.CurrentLife <= this.MaxLife / 2)
                        {
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "異常再生");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "超高温熱波動");
                        }
                    }
                    else if (AI_TacticsNumber == 3)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "クロマティック・バレット");
                    }
                    break;

                case Database.ENEMY_HOWLING_HORROR:
                    if (AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "スペクター・ヴォイス");
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "無慈悲な叫び声");
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ダーク・シミュラクラム");
                    }
                    break;

                case Database.ENEMY_PAIN_ANGEL:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "フェイブリオル・ランス");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "安らかな死別");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ダンシング・ソード");
                            break;
                    }
                    break;

                case Database.ENEMY_CHAOS_WARDEN:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "カオス・デスペラート");
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "マリア・ダンセル");
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "調律の破壊");
                    }
                    break;

                case Database.ENEMY_DREAD_KNIGHT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "暗黒の槍");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "大地の裂け目");
                            break;
                        case 2:
                            if (this.currentLife <= this.MaxLife / 2)
                            {
                                SetupActionCommand(this, this, PlayerAction.SpecialSkill, "不死への渇望");
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.SpecialSkill, "暗黒の槍");
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_DOOM_BRINGER:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ディレンジド・アート");
                            break;
                        case 1:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヘル・サークル");
                            break;
                        case 2:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "無垢のひと振り");
                            break;
                        case 3:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ハーシュ・カッター");
                            break;
                    }
                    break;

                // ４階、ボス
                case Database.ENEMY_BOSS_LEGIN_ARZE:
                case Database.ENEMY_BOSS_LEGIN_ARZE_1:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (target.CurrentAusterityMatrixOmega <= 0)
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アウステリティ・マトリクス・Ω");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((mc != null) && (!mc.Dead) && (mc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = mc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = sc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }

                    this.AI_TacticsNumber++;
                    if (this.AI_TacticsNumber > 2) { this.AI_TacticsNumber = 0; }
                    break;

                case Database.ENEMY_BOSS_LEGIN_ARZE_2:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (target.CurrentAusterityMatrixOmega <= 0)
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アウステリティ・マトリクス・Ω");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((mc != null) && (!mc.Dead) && (mc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = mc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = sc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 3)
                    {
                        if (this.CurrentVoiceOfAbyss <= 0)
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴォイス・オブ・アビス");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }

                    this.AI_TacticsNumber++;
                    if (this.AI_TacticsNumber > 3) { this.AI_TacticsNumber = 0; }
                    break;

                case Database.ENEMY_BOSS_LEGIN_ARZE_3:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (target.CurrentAusterityMatrixOmega <= 0)
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アウステリティ・マトリクス・Ω");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentAbyssFire <= 0))
                        {
                            this.Target = sc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "アビス・ファイア");
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = mc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentIchinaruHomura <= 0))
                        {
                            this.Target = sc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "壱なる焔");
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentAbyssFire <= 0))
                        {
                            this.Target = mc;
                            SetupActionCommand(this, this.Target, PlayerAction.SpecialSkill, "アビス・ファイア");
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 3)
                    {
                        if ((this.CurrentVoiceOfAbyss <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ヴォイス・オブ・アビス");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }
                    else if (this.AI_TacticsNumber == 4)
                    {
                        //if ((this.CurrentLightAndShadow <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        //{
                        //    this.PA = PlayerAction.SpecialSkill;
                        //    this.ActionLabel.text = "ライト・アンド・シャドウ";
                        //    this.Target = this;
                        //}
                        if (this.CurrentEternalDroplet <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.SpecialSkill, "エターナル・ドロップレット");
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "アビスの意志");
                        }
                    }

                    this.AI_TacticsNumber++;
                    if (this.AI_TacticsNumber > 4) { this.AI_TacticsNumber = 0; }
                    break;
                #endregion
                #region "５階"
                case Database.ENEMY_BOSS_BYSTANDER_EMPTINESS:
                    SetupActionCommand(this, this, PlayerAction.SpecialSkill, "時間律の支配");
                    break;

                case Database.ENEMY_PHOENIX:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "戦慄の金切り声");
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "焼き尽くす煉獄炎");
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        SetupActionCommand(this, this, PlayerAction.SpecialSkill, "輝ける生命");
                        this.AI_TacticsNumber = 0;
                    }
                    break;
                    
                case Database.ENEMY_NINE_TAIL:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ベジェ・テイル・アタック");
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "喰らいつき");
                        this.AI_TacticsNumber++;
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "隕石を呼ぶ声");
                        this.AI_TacticsNumber = 0;
                    }
                    break;

                case Database.ENEMY_JUDGEMENT:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "聖者の裁き");
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "福音");
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        SetupActionCommand(this, this, PlayerAction.SpecialSkill, "解放の賛歌");
                        this.AI_TacticsNumber = 0;
                    }
                    break;

                case Database.ENEMY_EMERALD_DRAGON:
                    if (this.AI_TacticsNumber == 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "圧死の視線"); // ライフ１、蘇生不可
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "イル・メギド・ブレス"); // 全員、最大ライフ－１のダメージ
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        SetupActionCommand(this, target, PlayerAction.SpecialSkill, "炎と氷の爆発");
                        this.AI_TacticsNumber = 0;
                    }

                    break;
                #endregion
                #region "真実世界"
                case Database.ENEMY_LAST_RANA_AMILIA:
                    // 相手が行動直前なら防御姿勢
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 70)
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    else if ((this.CurrentCounterAttack <= 0) && (this.CurrentSkillPoint >= Database.COUNTER_ATTACK_COST) && (target.CurrentSkillName == Database.CRUSHING_BLOW))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if ((this.CurrentNegate <= 0) && (this.CurrentSkillPoint >= Database.NEGATE_COST) && (target.CurrentSpellName == Database.CHILL_BURN))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                    }
                    else if ((this.CurrentOneImmunity <= 0) && (this.CurrentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    //else if ((this.CurrentNothingOfNothingness <= 0) && (this.CurrentSkillPoint >= Database.NOTHING_OF_NOTHINGNESS_COST) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NOTHING_OF_NOTHINGNESS);
                    //}
                    else if ((!this.StillNotAction2) && (this.CurrentEclipseEnd <= 0) && (this.CurrentMana >= Database.ECLIPSE_END_COST) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ECLIPSE_END);
                        this.StillNotAction2 = true;
                    }
                    else if ((this.CurrentVoidExtraction <= 0) && (this.CurrentSkillPoint >= Database.VOID_EXTRACTION_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                    }
                    else if ((this.CurrentPromisedKnowledge <= 0) && (this.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    }
                    else if ((this.CurrentFutureVision <= 0) && (this.CurrentSkillPoint >= Database.FUTURE_VISION_COST) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.FUTURE_VISION);
                    }
                    else if ((target.CurrentAbsoluteZero <= 0) && (this.CurrentMana >= Database.ABSOLUTE_ZERO_COST) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ABSOLUTE_ZERO);
                    }
                    else if ((target.CurrentImpulseHitValue < 3) && (this.CurrentSkillPoint >= Database.IMPULSE_HIT_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                    }
                    else if ((target.CurrentMana >= target.MaxMana / 5) && (this.CurrentMana >= Database.DOOM_BLADE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DOOM_BLADE);
                    }
                    else if ((this.CurrentMana >= Database.WHITE_OUT_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WHITE_OUT);
                    }
                    else if ((this.CurrentSkillPoint >= Database.ENIGMA_SENSE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                    }
                    else
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_SINIKIA_KAHLHANZ:
                    if (this.CurrentEclipseEnd > 0) { StillNotAction1 = true; }

                    // 相手が行動直前なら防御姿勢
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 50)
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    else if ((this.CurrentEclipseEnd <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint) && (this.CurrentMana >= Database.ECLIPSE_END_COST) && this.StillNotAction1 == false)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ECLIPSE_END);
                    }
                    else if ((this.CurrentCounterAttack <= 0) && (this.CurrentSkillPoint >= Database.COUNTER_ATTACK_COST) && (target.CurrentSkillName == Database.CRUSHING_BLOW))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if ((this.CurrentNegate <= 0) && (this.CurrentSkillPoint >= Database.NEGATE_COST) && (target.CurrentSpellName == Database.CHILL_BURN))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                    }
                    else if ((target.CurrentAusterityMatrix <= 0) && (this.CurrentMana >= Database.AUSTERITY_MATRIX_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.AUSTERITY_MATRIX);
                    }
                    //else if ((this.CurrentOneImmunity <= 0) && (this.CurrentMana >= Database.ONE_IMMUNITY_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    //}
                    else if ((target.CurrentNoGainLife <= 0) && (this.CurrentMana >= Database.DEMONIC_IGNITE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DEMONIC_IGNITE);
                    }
                    //else if ((this.CurrentPromisedKnowledge <= 0) && (this.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    //}
                    //else if ((target.CurrentSigilOfHomura <= 0) && (this.CurrentMana <= Database.SIGIL_OF_HOMURA_COST))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                    //}
                    //else if ((this.CurrentRedDragonWill <= 0) && (this.CurrentMana >= Database.RED_DRAGON_WILL_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                    //}
                    //else if ((target.CurrentEnrageBlast <= 0) && (this.CurrentMana >= Database.ENRAGE_BLAST_COST))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                    //}
                    //else if ((this.CurrentGaleWind <= 0) && (this.CurrentMana >= Database.GALE_WIND_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.GALE_WIND);
                    //}
                    else if (this.CurrentMana >= Database.PIERCING_FLAME_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                    }
                    //else if ((this.CurrentMana >= this.MaxMana / 2) && (target.CurrentLife <= target.MaxLife) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ASCENDANT_METEOR);
                    //}
                    else
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_OL_LANDIS:
                    // 相手が行動直前時は防御を選択
                    int RESPONSE_BORDER_OL = 20;
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER_OL)
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    else if (!this.OpponentUseInstantPoint)
                    {
                        if (this.CurrentSkillPoint <= 5)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                        else if ((this.CurrentLife <= this.MaxLife * 3 / 5) && (this.CurrentMana >= Database.CELESTIAL_NOVA_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else if ((this.CurrentCounterAttack <= 0) && (this.CurrentSkillPoint >= Database.COUNTER_ATTACK_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                        }
                        else if ((this.CurrentNegate <= 0) && (this.CurrentSkillPoint >= Database.NEGATE_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                        }
                        else if ((this.CurrentMana >= Database.DISPEL_MAGIC_COST) &&
                                 (target.CurrentSaintPower > 0 || target.CurrentFlameAura > 0 || target.CurrentHeatBoost > 0 || target.CurrentHolyBreaker > 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                        }
                        else if ((this.CurrentMana >= Database.TRANQUILITY_COST) &&
                                 (target.CurrentGaleWind > 0 || target.CurrentWordOfFortune > 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.TRANQUILITY);
                        }
                        else if ((this.CurrentBlackContract <= 0) && (this.CurrentMana >= Database.BLACK_CONTRACT_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                        }
                        else if ((this.CurrentFlameAura <= 0) && ((this.CurrentBlackContract > 0) || (this.CurrentMana >= Database.FLAME_AURA_COST)))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((this.CurrentPhantasmalWind <= 0) && ((this.CurrentBlackContract > 0) || (this.CurrentMana >= Database.PHANTASMAL_WIND_COST)))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PHANTASMAL_WIND);
                        }
                        else if ((target.CurrentImpulseHitValue < 2) && ((this.CurrentBlackContract > 0) || (this.CurrentSkillPoint >= Database.IMPULSE_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((target.CurrentOnslaughtHitValue < 2) && ((this.CurrentBlackContract > 0) || (this.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                        }
                        else if ((target.CurrentConcussiveHitValue < 2) && ((this.CurrentBlackContract > 0) || (this.CurrentSkillPoint >= Database.CONCUSSIVE_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                        }
                        else if ((this.CurrentBlackContract > 0) || (this.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.DOUBLE_SLASH);
                        }
                        else
                        {
                            this.PA = PlayerAction.Defense;
                            this.ActionLabel.text = Database.DEFENSE_JP;
                        }
                    }
                    else
                    {
                        if (this.beforeSkillName != Database.CARNAGE_RUSH)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.GENESIS);
                        }
                    }
                    break;
                case Database.ENEMY_LAST_VERZE_ARTIE:
                    // pattern
                    if (this.CurrentFutureVision > 0)
                    {
                        this.AI_TacticsNumber = 9;
                    }
                    else if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 15 && TruthActionCommand.IsDamage(target.ActionLabel.text))
                    {
                        this.AI_TacticsNumber = 10;
                    }
                    else if (((this.CurrentLife < this.MaxLife / 2) && (target.CurrentLife > target.MaxLife / 2)) ||
                             (this.AI_TacticsNumber == 4 && target.CurrentLife > target.MaxLife / 2))
                    {
                        this.AI_TacticsNumber = 4;
                    }
                    else if ((this.beforePA == PlayerAction.UseSpell && this.beforeSpellName != Database.ONE_IMMUNITY) ||
                             (this.beforePA == PlayerAction.UseSkill && (this.beforeSkillName != Database.STANCE_OF_MYSTIC &&
                                                                         this.beforeSkillName != Database.NEGATE &&
                                                                         this.beforeSkillName != Database.COUNTER_ATTACK &&
                                                                         this.beforeSkillName != Database.FUTURE_VISION)))
                    {
                        if (this.CurrentOneImmunity <= 0 || this.CurrentStanceOfMysticValue < 3 || this.CurrentNegate <= 0 || this.CurrentCounterAttack <= 0 || this.CurrentFutureVision <= 0)
                        {
                            this.AI_TacticsNumber = 0;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }
                    else if (target.PA == PlayerAction.Defense)
                    {
                        this.AI_TacticsNumber = 8;
                    }
                    else if ((target.PA == PlayerAction.UseSpell && TruthActionCommand.IsDamage(target.CurrentSpellName)) ||
                             (target.PA == PlayerAction.UseSkill && TruthActionCommand.IsDamage(target.CurrentSkillName)) ||
                             (target.PA == PlayerAction.NormalAttack))
                    {
                        this.AI_TacticsNumber = 1;
                    }
                    else if ((target.PA == PlayerAction.UseSpell && TruthActionCommand.GetBuffType(target.CurrentSpellName) == TruthActionCommand.BuffType.Up) ||
                             (target.PA == PlayerAction.UseSkill && TruthActionCommand.GetBuffType(target.CurrentSkillName) == TruthActionCommand.BuffType.Up))
                    {
                        if (target.CurrentAusterityMatrix <= 0 || this.CurrentNothingOfNothingness <= 0)
                        {
                            this.AI_TacticsNumber = 3;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }
                    else
                    {
                        if (this.CurrentEverDroplet <= 0 || this.CurrentWordOfLife <= 0)
                        {
                            this.AI_TacticsNumber = 7;
                        }
                        else if (this.DetectCannotBeFrozen == false || this.DetectCannotBeParalyze == false || this.DetectCannotBeStun == false)
                        {
                            this.AI_TacticsNumber = 2;
                        }
                        else if (target.CurrentEnrageBlast <= 0 || target.CurrentBlazingField <= 0 || target.CurrentDamnation <= 0 || this.CurrentPainfulInsanity <= 0)
                        {
                            this.AI_TacticsNumber = 6;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }

                    // tac
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (this.CurrentOneImmunity <= 0)
                        {
                            SetupActionWisely(this, this, Database.ONE_IMMUNITY);
                        }
                        else if (this.CurrentStanceOfDeath <= 0)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_DEATH);
                        }
                        else if (this.CurrentStanceOfMysticValue < 3)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_MYSTIC);
                        }
                        else if (this.CurrentCounterAttack <= 0)
                        {
                            SetupActionWisely(this, this, Database.COUNTER_ATTACK);
                        }
                        else if (this.CurrentNegate <= 0)
                        {
                            SetupActionWisely(this, this, Database.NEGATE);
                        }
                        else if ((this.CurrentFutureVision <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.FUTURE_VISION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if (this.CurrentFlameAura <= 0)
                        {
                            SetupActionWisely(this, this, Database.FLAME_AURA);
                        }
                        else if (this.CurrentFrozenAura <= 0)
                        {
                            SetupActionWisely(this, this, Database.FROZEN_AURA);
                        }
                        else if (this.CurrentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else
                        {
                            SetupActionWisely(this, this, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((target.CurrentFrozen <= 0) && (this.DetectCannotBeFrozen == false))
                        {
                            SetupActionWisely(this, target, Database.CHILL_BURN);
                        }
                        else if ((target.CurrentParalyze <= 0) && (this.DetectCannotBeParalyze == false))
                        {
                            SetupActionWisely(this, target, Database.SURPRISE_ATTACK);
                        }
                        else if ((target.CurrentStunning <= 0) && (this.DetectCannotBeStun == false) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, target, Database.CRUSHING_BLOW);
                        }
                    }
                    else if (this.AI_TacticsNumber == 3)
                    {
                        if ((this.CurrentEclipseEnd <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.ECLIPSE_END);
                        }
                        else if ((this.CurrentHymnContract <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.HYMN_CONTRACT);
                        }
                        else if ((this.CurrentAusterityMatrix <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, target, Database.AUSTERITY_MATRIX);
                        }
                        else if ((this.CurrentNothingOfNothingness <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.NOTHING_OF_NOTHINGNESS);
                        }
                    }
                    else if (this.AI_TacticsNumber == 4)
                    {
                        if (this.CurrentLife < this.MaxLife / 4)
                        {
                            SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                        }
                        else if (this.CurrentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else if (this.CurrentWordOfFortune <= 0)
                        {
                            SetupActionWisely(this, this, Database.WORD_OF_FORTUNE);
                        }
                        else if (this.CurrentLife < this.MaxLife / 2)
                        {
                            SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                        }
                        else if (this.CurrentLife >= this.MaxLife / 2)
                        {
                            SetupActionWisely(this, target, Database.CELESTIAL_NOVA);
                        }
                    }
                    else if (this.AI_TacticsNumber == 5)
                    {
                        if (this.CurrentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else
                        {
                            SetupActionWisely(this, this, Database.INNER_INSPIRATION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 6)
                    {
                        if (this.CurrentEnrageBlast <= 0)
                        {
                            SetupActionWisely(this, this, Database.ENRAGE_BLAST);
                        }
                        else if (target.CurrentBlazingField <= 0)
                        {
                            SetupActionWisely(this, target, Database.BLAZING_FIELD);
                        }
                        else if (target.CurrentDamnation <= 0)
                        {
                            SetupActionWisely(this, target, Database.DAMNATION);
                        }
                        else if ((this.CurrentPainfulInsanity <= 0) && (this.CurrentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.PAINFUL_INSANITY);
                        }
                    }
                    else if (this.AI_TacticsNumber == 7)
                    {
                        if (this.CurrentEverDroplet <= 0)
                        {
                            SetupActionWisely(this, this, Database.EVER_DROPLET);
                        }
                        else if (this.CurrentWordOfLife <= 0)
                        {
                            SetupActionWisely(this, this, Database.WORD_OF_LIFE);
                        }
                    }
                    else if (this.AI_TacticsNumber == 8)
                    {
                        if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.PSYCHIC_WAVE);
                        }
                        else if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.PIERCING_FLAME);
                        }
                        else if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.WORD_OF_POWER);
                        }
                    }
                    else if (this.AI_TacticsNumber == 9)
                    {
                        // action check
                        if (this.CurrentTimeStop > 0)
                        {
                            this.StillNotAction1 = true;
                        }
                        if (this.CurrentStanceOfDouble > 0)
                        {
                            this.StillNotAction2 = true;
                        }

                        // tac
                        if (this.CurrentTimeStop <= 0 && this.StillNotAction1 == false)
                        {
                            SetupActionWisely(this, this, Database.TIME_STOP);
                        }
                        else if (this.CurrentBlackContract <= 0)
                        {
                            SetupActionWisely(this, this, Database.BLACK_CONTRACT);
                        }
                        else if (target.CurrentSigilOfHomura <= 0)
                        {
                            SetupActionWisely(this, target, Database.SIGIL_OF_HOMURA);
                        }
                        else if (this.CurrentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else if (this.beforeSpellName != Database.ZETA_EXPLOSION)
                        {
                            SetupActionWisely(this, target, Database.ZETA_EXPLOSION);
                        }
                        else if (this.beforeSpellName == Database.ZETA_EXPLOSION && this.CurrentStanceOfDouble <= 0)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_DOUBLE);
                        }
                        else
                        {
                            SetupActionWisely(this, target, Database.ZETA_EXPLOSION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 10)
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_SIN_VERZE_ARTIE:
                    int random1 = AP.Math.RandomInteger(4);
                    int RESPONSE_BORDER = 10;

                    // 相手が行動直前時は防御を選択
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        this.PA = PlayerAction.Defense;
                        this.ActionLabel.text = Database.DEFENSE_JP;
                    }
                    // ライフが減ってきた場合は回復
                    else if (this.CurrentLife < this.MaxLife / 3)
                    {
                        SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                    }
                    // 特定のBUFFが付いている場合、それを強化するためのシーケンスを選択する。
                    else if (this.CurrentFlameAura > 0 && this.CurrentFrozenAura <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.FROZEN_AURA);
                    }
                    else if (this.CurrentFrozenAura > 0 && this.CurrentFlameAura <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.FLAME_AURA);
                    }
                    else if (this.CurrentWordOfLife > 0 && this.CurrentEverDroplet <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.EVER_DROPLET);
                    }
                    else if (this.CurrentEverDroplet > 0 && this.CurrentWordOfLife <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.WORD_OF_LIFE);
                    }
                    else if (this.CurrentDeflection > 0 && this.CurrentMirrorImage <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.MIRROR_IMAGE);
                    }
                    else if (this.CurrentMirrorImage > 0 && this.CurrentDeflection <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.DEFLECTION);
                    }
                    else if (target.CurrentBlueDragonWill > 0 && this.CurrentRedDragonWill <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.RED_DRAGON_WILL);
                    }
                    else if (this.CurrentRedDragonWill > 0 && target.CurrentBlueDragonWill <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.BLUE_DRAGON_WILL);
                    }
                    else if (this.CurrentTruthVision > 0 && this.CurrentPainfulInsanity <= 0)
                    {
                        SetupActionWisely(this, this, Database.PAINFUL_INSANITY);
                    }
                    else if (this.CurrentPainfulInsanity > 0 && this.CurrentTruthVision <= 0)
                    {
                        SetupActionWisely(this, this, Database.TRUTH_VISION);
                    }
                    // 生命力カウンター（２）依存戦術
                    else if (this.CurrentFutureVision <= 0 && this.CurrentInstantPoint >= this.MaxInstantPoint && random1 == 0 && this.CurrentLifeCountValue == 2)
                    {
                        SetupActionWisely(this, target, Database.FUTURE_VISION);
                    }
                    // ワープゲート用に常にランダム
                    else
                    {
                        List<string> commands = new List<string>(TruthActionCommand.GetActionList(this));
                        // 基本動作または弱コマンドはリストから除外
                        commands.Remove(Database.ATTACK_EN);
                        commands.Remove(Database.DEFENSE_EN);
                        commands.Remove(Database.STAY_EN);
                        commands.Remove(Database.WEAPON_SPECIAL_EN);
                        commands.Remove(Database.WEAPON_SPECIAL_LEFT_EN);
                        commands.Remove(Database.TAMERU_EN);
                        commands.Remove(Database.ACCESSORY_SPECIAL_EN);
                        commands.Remove(Database.ACCESSORY_SPECIAL2_EN);
                        commands.Remove(Database.FRESH_HEAL);
                        commands.Remove(Database.RESURRECTION);
                        commands.Remove(Database.LIFE_TAP);
                        commands.Remove(Database.DARK_BLAST);
                        commands.Remove(Database.FIRE_BALL);
                        commands.Remove(Database.FLAME_STRIKE);
                        commands.Remove(Database.ICE_NEEDLE);
                        commands.Remove(Database.STANCE_OF_FLOW);
                        commands.Remove(Database.TRANSCENDENT_WISH); // 自分が死亡してしまうので除外。
                        commands.Remove(Database.DEATH_DENY);
                        commands.Remove(Database.WORD_OF_ATTITUDE);
                        commands.Remove(Database.SEVENTH_MAGIC); // ヴェルゼの場合、特に意味はないため除外。
                        commands.Remove(Database.WARP_GATE);
                        commands.Remove(Database.ANTI_STUN); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.NEUTRAL_SMASH);
                        commands.Remove(Database.CIRCLE_SLASH);
                        commands.Remove(Database.RUMBLE_SHOUT);
                        commands.Remove(Database.REFLEX_SPIRIT); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.TRUST_SILENCE); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.RECOVER);
                        commands.Remove(Database.HARDEST_PARRY);
                        commands.Remove(Database.STANCE_OF_SUDDENNESS);
                        commands.Remove(Database.ENDLESS_ANTHEM);
                        commands.Remove(Database.GENESIS);
                        // BUFFが既に付与されている場合は除外
                        if (this.CurrentBloodyVengeance > 0) { commands.Remove(Database.BLOODY_VENGEANCE); }
                        if (this.CurrentHeatBoost > 0) { commands.Remove(Database.HEAT_BOOST); }
                        if (this.CurrentPromisedKnowledge > 0) { commands.Remove(Database.PROMISED_KNOWLEDGE); }
                        if (this.CurrentRiseOfImage > 0) { commands.Remove(Database.RISE_OF_IMAGE); }
                        if (this.CurrentProtection > 0) { commands.Remove(Database.PROTECTION); }
                        if (this.CurrentSaintPower > 0) { commands.Remove(Database.SAINT_POWER); }
                        if (this.CurrentGlory > 0) { commands.Remove(Database.GLORY); }
                        if (this.CurrentShadowPact > 0) { commands.Remove(Database.SHADOW_PACT); }
                        if (this.CurrentBlackContract > 0) { commands.Remove(Database.BLACK_CONTRACT); }
                        if (this.CurrentFlameAura > 0) { commands.Remove(Database.FLAME_AURA); }
                        if (this.CurrentImmortalRave > 0) { commands.Remove(Database.IMMORTAL_RAVE); }
                        if (this.CurrentAbsorbWater > 0) { commands.Remove(Database.ABSORB_WATER); }
                        if (this.CurrentMirrorImage > 0) { commands.Remove(Database.MIRROR_IMAGE); }
                        if (this.CurrentGaleWind > 0) { commands.Remove(Database.GALE_WIND); }
                        if (this.CurrentWordOfLife > 0) { commands.Remove(Database.WORD_OF_LIFE); }
                        if (this.CurrentWordOfFortune > 0) { commands.Remove(Database.WORD_OF_FORTUNE); }
                        if (this.CurrentAetherDrive > 0) { commands.Remove(Database.AETHER_DRIVE); }
                        if (this.CurrentEternalPresence > 0) { commands.Remove(Database.ETERNAL_PRESENCE); }
                        if (this.CurrentDeflection > 0) { commands.Remove(Database.DEFLECTION); }
                        if (this.CurrentOneImmunity > 0) { commands.Remove(Database.ONE_IMMUNITY); }
                        if (this.CurrentPsychicTrance > 0) { commands.Remove(Database.PSYCHIC_TRANCE); }
                        if (this.CurrentBlindJustice > 0) { commands.Remove(Database.BLIND_JUSTICE); }
                        //if (this.CurrentTranscendentWish > 0) { commands.Remove(Database.TRANSCENDENT_WISH); } // 既に除外されている。
                        if (this.CurrentSkyShieldValue >= 3) { commands.Remove(Database.SKY_SHIELD); }
                        if (this.CurrentEverDroplet > 0) { commands.Remove(Database.EVER_DROPLET); }
                        if (this.CurrentHolyBreaker > 0) { commands.Remove(Database.HOLY_BREAKER); }
                        if (this.CurrentExaltedField > 0) { commands.Remove(Database.EXALTED_FIELD); }
                        if (this.CurrentHymnContract > 0) { commands.Remove(Database.HYMN_CONTRACT); }
                        if (this.CurrentSinFortune > 0) { commands.Remove(Database.SIN_FORTUNE); }
                        if (this.CurrentEclipseEnd > 0) { commands.Remove(Database.ECLIPSE_END); }
                        if (this.CurrentFrozenAura > 0) { commands.Remove(Database.FROZEN_AURA); }
                        if (this.CurrentPhantasmalWind > 0) { commands.Remove(Database.PHANTASMAL_WIND); }
                        if (this.CurrentRedDragonWill > 0) { commands.Remove(Database.RED_DRAGON_WILL); }
                        if (this.CurrentStaticBarrierValue >= 3) { commands.Remove(Database.STATIC_BARRIER); }
                        if (this.CurrentBlueDragonWill > 0) { commands.Remove(Database.BLUE_DRAGON_WILL); }
                        //if (this.CurrentSeventhMagic > 0) { commands.Remove(Database.SEVENTH_MAGIC); } // 既に除外されている。
                        if (this.CurrentParadoxImage > 0) { commands.Remove(Database.PARADOX_IMAGE); }
                        if (this.CurrentCounterAttack > 0) { commands.Remove(Database.COUNTER_ATTACK); }
                        //if (this.CurrentAntiStun > 0) { commands.Remove(Database.ANTI_STUN); } // 既に除外されている。
                        if (this.CurrentStanceOfDeath > 0) { commands.Remove(Database.STANCE_OF_DEATH); }
                        if (this.CurrentStanceOfFlow > 0) { commands.Remove(Database.STANCE_OF_FLOW); }
                        if (this.CurrentStanceOfStanding > 0) { commands.Remove(Database.STANCE_OF_STANDING); }
                        if (this.CurrentTruthVision > 0) { commands.Remove(Database.TRUTH_VISION); }
                        if (this.CurrentHighEmotionality > 0) { commands.Remove(Database.HIGH_EMOTIONALITY); }
                        if (this.CurrentStanceOfEyes > 0) { commands.Remove(Database.STANCE_OF_EYES); }
                        if (this.CurrentPainfulInsanity > 0) { commands.Remove(Database.PAINFUL_INSANITY); }
                        if (this.CurrentNegate > 0) { commands.Remove(Database.NEGATE); }
                        if (this.CurrentVoidExtraction > 0) { commands.Remove(Database.VOID_EXTRACTION); }
                        if (this.CurrentNothingOfNothingness > 0) { commands.Remove(Database.NOTHING_OF_NOTHINGNESS); }
                        if (this.CurrentStanceOfDouble > 0) { commands.Remove(Database.STANCE_OF_DOUBLE); }
                        if (this.CurrentSwiftStep > 0) { commands.Remove(Database.SWIFT_STEP); }
                        if (this.CurrentVigorSense > 0) { commands.Remove(Database.VIGOR_SENSE); }
                        if (this.CurrentRisingAura > 0) { commands.Remove(Database.RISING_AURA); }
                        if (this.CurrentColorlessMove > 0) { commands.Remove(Database.COLORLESS_MOVE); }
                        if (this.CurrentAscensionAura > 0) { commands.Remove(Database.ASCENSION_AURA); }
                        if (this.CurrentFutureVision > 0) { commands.Remove(Database.FUTURE_VISION); }
                        //if (this.CurrentReflexSpirit > 0) { commands.Remove(Database.REFLEX_SPIRIT); } // 既に除外されている。
                        //if (this.CurrentTrustSilence > 0) { commands.Remove(Database.TRUST_SILENCE); } // 既に除外されている。
                        if (this.CurrentStanceOfMysticValue >= 3) { commands.Remove(Database.STANCE_OF_MYSTIC); }
                        if (this.CurrentNourishSense > 0) { commands.Remove(Database.NOURISH_SENSE); }
                        if (this.CurrentOneAuthority > 0) { commands.Remove(Database.ONE_AUTHORITY); }
                        // 相手に負BUFFが既に付与されている場合は除外
                        if (target.CurrentDamnation > 0) { commands.Remove(Database.DAMNATION); }
                        if (target.CurrentAbsoluteZero > 0) { commands.Remove(Database.ABSOLUTE_ZERO); }
                        if (target.CurrentFlashBlazeCount > 0) { commands.Remove(Database.FLASH_BLAZE); }
                        if (target.CurrentStarLightning > 0) { commands.Remove(Database.STAR_LIGHTNING); }
                        if (target.CurrentBlackFire > 0) { commands.Remove(Database.BLACK_FIRE); }
                        if (target.CurrentBlazingField > 0) { commands.Remove(Database.BLAZING_FIELD); }
                        if (target.CurrentNoGainLife > 0) { commands.Remove(Database.DEMONIC_IGNITE); }
                        if (target.CurrentWordOfMalice > 0) { commands.Remove(Database.WORD_OF_MALICE); }
                        if (target.CurrentDarkenField > 0) { commands.Remove(Database.DARKEN_FIELD); }
                        if (target.CurrentFrozen > 0) { commands.Remove(Database.CHILL_BURN); }
                        if (target.CurrentEnrageBlast > 0) { commands.Remove(Database.ENRAGE_BLAST); }
                        if (target.CurrentSigilOfHomura > 0) { commands.Remove(Database.SIGIL_OF_HOMURA); }
                        if (target.CurrentImmolate > 0) { commands.Remove(Database.IMMOLATE); }
                        if (target.CurrentAusterityMatrix > 0) { commands.Remove(Database.AUSTERITY_MATRIX); }
                        if (target.CurrentSilence > 0) { commands.Remove(Database.VANISH_WAVE); }
                        if (target.CurrentStunning > 0) { commands.Remove(Database.CRUSHING_BLOW); }
                        if (target.CurrentOnslaughtHitValue >= 3) { commands.Remove(Database.ONSLAUGHT_HIT); }
                        if (target.CurrentBlind > 0) { commands.Remove(Database.UNKNOWN_SHOCK); }
                        if (target.CurrentConcussiveHitValue >= 3) { commands.Remove(Database.CONCUSSIVE_HIT); }
                        if (target.CurrentParalyze > 0) { commands.Remove(Database.SURPRISE_ATTACK); }
                        if (target.CurrentImpulseHitValue >= 3) { commands.Remove(Database.IMPULSE_HIT); }
                        // ディスペルマジック対象が相手にかかってない場合は除外
                        if (target.CurrentProtection <= 0 && target.CurrentSaintPower <= 0 && target.CurrentAbsorbWater <= 0 && target.CurrentShadowPact <= 0
                            && target.CurrentEternalPresence <= 0 && target.CurrentBloodyVengeance <= 0 && target.CurrentHeatBoost <= 0 && target.CurrentPromisedKnowledge <= 0
                            && target.CurrentRiseOfImage <= 0 && target.CurrentWordOfLife <= 0 && target.CurrentFlameAura <= 0
                            && target.CurrentPsychicTrance <= 0 && target.CurrentBlindJustice <= 0 && target.CurrentSkyShield <= 0
                            && target.CurrentEverDroplet <= 0 && target.CurrentHolyBreaker <= 0 && target.CurrentExaltedField <= 0
                            && target.CurrentFrozenAura <= 0 && target.CurrentPhantasmalWind <= 0 && target.CurrentRedDragonWill <= 0
                            && target.CurrentStaticBarrier <= 0 && target.CurrentBlueDragonWill <= 0 && target.CurrentSeventhMagic <= 0
                            && target.CurrentParadoxImage <= 0)
                        {
                            commands.Remove(Database.DISPEL_MAGIC);
                        }
                        // トランキリティ対象が相手にかかってない場合は除外
                        if (target.CurrentGlory <= 0 && target.CurrentGaleWind <= 0 && target.CurrentWordOfFortune <= 0 && target.CurrentBlackContract <= 0
                            && target.CurrentHymnContract <= 0 && target.CurrentImmortalRave <= 0 && target.CurrentAbsoluteZero <= 0 && target.CurrentAetherDrive <= 0
                            && target.CurrentOneImmunity <= 0 && target.CurrentHighEmotionality <= 0)
                        {
                            commands.Remove(Database.TRANQUILITY);
                        }
                        // ダメージ系統が当たる場合の除外リスト(IsDamageでは自分回復可能なCelestialNova、FlameAuraによる追加ダメージの分も入ってしまうので個別記述とする）
                        if (this.CurrentEclipseEnd > 0)
                        {
                            commands.Remove(Database.HOLY_SHOCK);
                            commands.Remove(Database.DEVOURING_PLAGUE);
                            commands.Remove(Database.VOLCANIC_WAVE);
                            commands.Remove(Database.LAVA_ANNIHILATION);
                            commands.Remove(Database.FROZEN_LANCE);
                            commands.Remove(Database.WORD_OF_POWER);
                            commands.Remove(Database.FLASH_BLAZE);
                            commands.Remove(Database.LIGHT_DETONATOR);
                            commands.Remove(Database.ASCENDANT_METEOR);
                            commands.Remove(Database.STAR_LIGHTNING);
                            commands.Remove(Database.BLACK_FIRE);
                            commands.Remove(Database.DEMONIC_IGNITE);
                            commands.Remove(Database.BLUE_BULLET);
                            commands.Remove(Database.WORD_OF_MALICE);
                            commands.Remove(Database.ABYSS_EYE);
                            commands.Remove(Database.DOOM_BLADE);
                            commands.Remove(Database.CHILL_BURN);
                            commands.Remove(Database.ZETA_EXPLOSION);
                            commands.Remove(Database.ENRAGE_BLAST);
                            commands.Remove(Database.PIERCING_FLAME);
                            commands.Remove(Database.IMMOLATE);
                            commands.Remove(Database.VANISH_WAVE);
                            commands.Remove(Database.STRAIGHT_SMASH);
                            commands.Remove(Database.DOUBLE_SLASH);
                            commands.Remove(Database.CRUSHING_BLOW);
                            commands.Remove(Database.SOUL_INFINITY);
                            commands.Remove(Database.ENIGMA_SENSE);
                            commands.Remove(Database.SILENT_RUSH);
                            commands.Remove(Database.OBORO_IMPACT);
                            commands.Remove(Database.STANCE_OF_STANDING);
                            commands.Remove(Database.KINETIC_SMASH);
                            commands.Remove(Database.CATASTROPHE);
                            commands.Remove(Database.CARNAGE_RUSH);
                            commands.Remove(Database.STANCE_OF_DOUBLE); // もし、前回行動がダメージ系だったら、ここで発動するとカッコ悪いのでやらない。
                            commands.Remove(Database.UNKNOWN_SHOCK);
                            commands.Remove(Database.FATAL_BLOW);
                            commands.Remove(Database.SHARP_GLARE);
                            commands.Remove(Database.CONCUSSIVE_HIT);
                            commands.Remove(Database.MIND_KILLING);
                            commands.Remove(Database.SURPRISE_ATTACK);
                            commands.Remove(Database.PSYCHIC_WAVE);
                            commands.Remove(Database.IMPULSE_HIT);
                            commands.Remove(Database.VIOLENT_SLASH);
                            commands.Remove(Database.SOUL_EXECUTION);
                        }

                        int randomNumber2 = AP.Math.RandomInteger(commands.Count);
                        this.PA = TruthActionCommand.CheckPlayerActionFromString(commands[randomNumber2]);
                        SetupActionWisely(this, target, commands[randomNumber2]);
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(commands[randomNumber2]);
                    }
                    //                    else if (this.CurrentInstantPoint >= this.MaxInstantPoint && random1 == 1)
                    //                    {
                    //                        SetupActionWisely(this, target, Database.STRAIGHT_SMASH, Database.STRAIGHT_SMASH_COST);
                    ////                        SetupActionWisely(this, target, Database.ZETA_EXPLOSION, Database.ZETA_EXPLOSION_COST);
                    //                    }
                    //                    else if (this.CurrentInstantPoint >= this.MaxInstantPoint && random1 == 2)
                    //                    {
                    //                        SetupActionWisely(this, target, Database.ABSOLUTE_ZERO, Database.ABSOLUTE_ZERO_COST);
                    //                    }
                    //                    else if (this.CurrentInstantPoint >= this.MaxInstantPoint && random1 == 3)
                    //                    {
                    //                        SetupActionWisely(this, target, Database.NOTHING_OF_NOTHINGNESS, Database.NOTHING_OF_NOTHINGNESS_COST);
                    //                    }
                    //                    else
                    //                    {
                    //                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                    //                    }
                    break;
                #endregion
                #region "Duel対戦相手（１階）"
                case Database.DUEL_EONE_FULNEA:
                    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ICE_NEEDLE);
                    break;

                case Database.DUEL_MAGI_ZELKIS:
                    if ((this.CurrentLife < this.MaxLife / 2) && (SearchItem(Database.COMMON_NORMAL_RED_POTION)))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseItem, Database.COMMON_NORMAL_RED_POTION);
                    }
                    else if (this.CurrentFlameAura <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.FLAME_AURA);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                    }
                    break;

                case Database.DUEL_SELMOI_RO:
                    if ((this.CurrentLife < this.MaxLife / 2) && (SearchItem(Database.COMMON_NORMAL_RED_POTION)))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseItem, Database.COMMON_NORMAL_RED_POTION);
                    }
                    else if (this.CurrentStanceOfStanding <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.STANCE_OF_STANDING);

                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                    break;

                case Database.DUEL_KARTIN_MAI:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.UseSkill;
                            this.CurrentSkillName = Database.ENIGMA_SENSE;
                            this.ActionLabel.text = Database.ENIGMA_SENSE_JP;
                            this.Target = target;
                            break;
                        case 1:
                            if (this.CurrentLife >= this.MaxLife)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.CurrentSpellName = Database.FIRE_BALL;
                                this.ActionLabel.text = Database.FIRE_BALL_JP;
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.CurrentSpellName = Database.LIFE_TAP;
                                this.ActionLabel.text = Database.LIFE_TAP_JP;
                                this.Target = this;
                            }
                            break;
                        case 2:
                            this.PA = PlayerAction.UseSpell;
                            this.CurrentSpellName = Database.DEVOURING_PLAGUE;
                            this.ActionLabel.text = Database.DEVOURING_PLAGUE_JP;
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.DUEL_JEDA_ARUS:
                    if (this.CurrentSkillPoint < 25)
                    {
                        if (SearchItem(Database.RARE_PURE_GREEN_WATER))
                        {
                            this.PA = PlayerAction.UseItem;
                            this.CurrentUsingItem = Database.RARE_PURE_GREEN_WATER;
                            this.ActionLabel.text = Database.RARE_PURE_GREEN_WATER;
                            this.Target = this;
                        }
                        else
                        {
                            this.PA = PlayerAction.UseSkill;
                            this.CurrentSkillName = Database.DOUBLE_SLASH;
                            this.ActionLabel.text = Database.DOUBLE_SLASH_JP;
                            this.Target = target;
                        }
                    }
                    else
                    {
                        switch (AP.Math.RandomInteger(2))
                        {
                            case 0:
                                if (this.CurrentLife < this.MaxLife * 0.5F && SearchItem(Database.RARE_PURE_WATER))
                                {
                                    this.PA = PlayerAction.UseItem;
                                    this.CurrentUsingItem = Database.RARE_PURE_WATER;
                                    this.ActionLabel.text = Database.RARE_PURE_WATER;
                                    this.Target = this;
                                }
                                else
                                {
                                    this.PA = PlayerAction.UseSkill;
                                    this.CurrentSkillName = Database.DOUBLE_SLASH;
                                    this.ActionLabel.text = Database.DOUBLE_SLASH_JP;
                                    this.Target = target;
                                }
                                break;
                            case 1:
                                if (this.CurrentSaintPower <= 0)
                                {
                                    this.PA = PlayerAction.UseSpell;
                                    this.CurrentSpellName = Database.SAINT_POWER;
                                    this.ActionLabel.text = Database.SAINT_POWER_JP;
                                    this.Target = this;
                                }
                                else
                                {
                                    this.PA = PlayerAction.UseSkill;
                                    this.CurrentSkillName = Database.DOUBLE_SLASH;
                                    this.ActionLabel.text = Database.DOUBLE_SLASH_JP;
                                    this.Target = target;
                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                    break;

                case Database.DUEL_SINIKIA_VEILHANZ:
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            if (target.PA == PlayerAction.Defense)
                            {
                                if (this.CurrentShadowPact <= 0)
                                {
                                    this.PA = PlayerAction.UseSpell;
                                    this.CurrentSpellName = Database.SHADOW_PACT;
                                    this.ActionLabel.text = Database.SHADOW_PACT_JP;
                                    this.Target = this;
                                }
                                else if (this.CurrentChargeCount <= 0)
                                {
                                    this.PA = PlayerAction.Charge;
                                    this.ActionLabel.text = Database.TAMERU_JP;
                                    this.Target = this;
                                }
                                else
                                {
                                    this.PA = PlayerAction.UseSpell;
                                    this.CurrentSpellName = Database.HOLY_SHOCK;
                                    this.ActionLabel.text = Database.HOLY_SHOCK_JP;
                                    this.Target = target;
                                }
                            }
                            else
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.CurrentSpellName = Database.HOLY_SHOCK;
                                this.ActionLabel.text = Database.HOLY_SHOCK_JP;
                                this.Target = target;
                            }
                            break;
                    }
                    break;

                case Database.DUEL_OL_LANDIS:
                    if ((this.Target.CurrentParalyze > 0) && (this.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST))
                    {
                        this.PA = PlayerAction.UseSkill;
                        this.CurrentSkillName = Database.STRAIGHT_SMASH;
                        this.ActionLabel.text = Database.STRAIGHT_SMASH_JP;
                        this.Target = target;
                    }
                    else
                    {
                        if (this.CurrentMana >= Database.VOLCANIC_WAVE_COST)
                        {
                            this.PA = PlayerAction.UseSpell;
                            this.CurrentSpellName = Database.VOLCANIC_WAVE;
                            this.ActionLabel.text = Database.VOLCANIC_WAVE_JP;
                            this.Target = target;
                        }
                        else if (this.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                        {
                            this.PA = PlayerAction.UseSkill;
                            this.CurrentSkillName = Database.STRAIGHT_SMASH;
                            this.ActionLabel.text = Database.STRAIGHT_SMASH_JP;
                            this.Target = this;
                        }
                    }
                    break;

                case Database.VERZE_ARTIE_FULL:
                case Database.VERZE_ARTIE:
                    if (this.CurrentStanceOfFlow <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.STANCE_OF_FLOW);
                    }
                    else if (this.CurrentCounterAttack <= 0)
                    {
                        if (this.CurrentSkillPoint < Database.COUNTER_ATTACK_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                        }
                    }
                    else if (this.CurrentMirrorImage <= 0)
                    {
                        if (this.CurrentMana < Database.MIRROR_IMAGE_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                        else
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.MIRROR_IMAGE);
                        }
                    }
                    else if (this.CurrentMana < Database.GALE_WIND_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    }
                    else if (this.CurrentGaleWind <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.GALE_WIND);
                    }
                    else if (this.CurrentSkillPoint < Database.STRAIGHT_SMASH_COST)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                    }
                    break;

                case Database.DUEL_ADEL_BRIGANDY:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.CurrentHeatBoost <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.HEAT_BOOST);
                        }
                        else if (this.CurrentHighEmotionality <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.HIGH_EMOTIONALITY);
                        }
                        else if (this.CurrentGaleWind <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.GALE_WIND);
                        }
                        AI_TacticsNumber = 1;
                    }
                    else
                    {
                        if (this.CurrentSkillPoint < Database.ENIGMA_SENSE_COST)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                        }
                        AI_TacticsNumber = 0;
                    }
                    break;

                case Database.DUEL_LENE_COLTOS:
                    if (this.CurrentLife <= this.MaxLife / 2 && AI_TacticsNumber == 0)
                    {
                        AI_TacticsNumber = 1;
                    }
                    else if (this.CurrentLife >= this.MaxLife && AI_TacticsNumber == 1)
                    {
                        AI_TacticsNumber = 0;
                    }

                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            if (AI_TacticsNumber == 1)
                            {
                                SetupActionCommand(this, this, PlayerAction.UseSpell, Database.FRESH_HEAL);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseItem, Database.RARE_BLUE_LIGHTNING);
                            }
                            break;
                    }
                    break;

                case Database.DUEL_SCOTY_ZALGE:
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            SetupActionCommand(this, target, PlayerAction.SpecialSkill, "ザルゲ・スラッシュ");
                            break;
                    }
                    break;

                case Database.DUEL_PERMA_WARAMY:
                    if (this.CurrentHolyBreaker <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.HOLY_BREAKER);
                    }
                    else
                    {
                        if (this.CurrentLife >= this.MaxLife)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FLAME_STRIKE);
                        }
                        else
                        {
                            SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                        }
                    }
                    break;

                case Database.DUEL_KILT_JORJU:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.CurrentSkillPoint > Database.STRAIGHT_SMASH_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                        }
                        AI_TacticsNumber = 1;
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        if (this.CurrentMana > Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                        }
                        AI_TacticsNumber = 0;
                    }
                    break;

                case Database.DUEL_BILLY_RAKI:
                    if (this.CurrentSkillPoint > Database.CARNAGE_RUSH_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                    }
                    else if (this.CurrentMana > Database.WORD_OF_POWER_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                    }
                    break;

                case Database.DUEL_ANNA_HAMILTON:
                    if (this.CurrentWordOfLife <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.WORD_OF_LIFE);
                    }
                    else
                    {
                        if (AI_TacticsNumber == 0)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.VOLCANIC_WAVE);
                            AI_TacticsNumber = 1;
                        }
                        else if (AI_TacticsNumber == 1)
                        {
                            if (this.CurrentSkillPoint > Database.CRUSHING_BLOW_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CRUSHING_BLOW);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.VOLCANIC_WAVE);
                            }
                            AI_TacticsNumber = 0;
                        }
                    }
                    break;

                case Database.DUEL_CALMANS_OHN:
                    SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                    break;

                case Database.DUEL_SUN_YU:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.CurrentLife < this.MaxLife / 2)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SACRED_HEAL);
                        }
                        else
                        {
                            if (target.CurrentDarkenField <= 0)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DARKEN_FIELD);
                            }
                            else if (this.CurrentSaintPower <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SAINT_POWER);
                            }
                            else if (this.CurrentBloodyVengeance <= 0)
                            {
                                SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLOODY_VENGEANCE);
                            }
                            else
                            {
                                SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                            }
                        }
                        AI_TacticsNumber = 1;
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        if (target.CurrentConcussiveHit <= 0 || target.CurrentConcussiveHitValue < 3)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                        }
                        AI_TacticsNumber = 2;
                    }
                    else
                    {
                        if (this.CurrentSkillPoint > Database.SILENT_RUSH_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.SILENT_RUSH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                        }
                        AI_TacticsNumber = 0;
                    }
                    break;

                case Database.DUEL_SHUVALTZ_FLORE:
                    if (this.CurrentAetherDrive > 0 && this.CurrentWordOfFortune <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.WORD_OF_FORTUNE);
                    }
                    else if (this.CurrentInstantPoint >= this.MaxInstantPoint && this.CurrentExaltedField <= 0 && this.CurrentMana > Database.EXALTED_FIELD_COST)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.EXALTED_FIELD);
                    }
                    else if (this.CurrentInstantPoint >= this.MaxInstantPoint && this.CurrentRisingAura <= 0 && this.CurrentSkillPoint > Database.RISING_AURA_COST)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.RISING_AURA);
                    }
                    else if (this.CurrentAetherDrive > 0 && this.CurrentWordOfFortune > 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    }
                    else
                    {
                        SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                    }
                    break;

                case Database.DUEL_SINIKIA_KAHLHANZ:
                    if (this.currentMana >= Database.PIERCING_FLAME_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                    }
                    else if (this.CurrentBlackContract <= 0 && this.currentMana >= Database.BLACK_CONTRACT_COST)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.NormalAttack, Database.ATTACK_EN);
                    }
                    break;

                case Database.DUEL_RVEL_ZELKIS:
                    bool existRedPotion = false;
                    bool existBluePotion = false;
                    bool existGreenPotion = false;
                    ItemBackPack[] tempItem_2 = this.GetBackPackInfo();
                    foreach (ItemBackPack value in tempItem_2)
                    {
                        if (value != null)
                        {
                            if (value.Name == Database.COMMON_HUGE_RED_POTION)
                            {
                                existRedPotion = true;
                            }
                            if (value.Name == Database.COMMON_HUGE_BLUE_POTION)
                            {
                                existBluePotion = true;
                            }
                            if (value.Name == Database.COMMON_HUGE_GREEN_POTION)
                            {
                                existGreenPotion = true;
                            }
                        }
                    }

                    // 判断の基準
                    if (mc.CurrentFrozen > 0)
                    {
                        this.AI_TacticsNumber = 1;
                    }
                    else if (this.CurrentPromisedKnowledge > 0)
                    {
                        this.AI_TacticsNumber = 2;
                    }

                    // アイテム使用の基準
                    if ((this.CurrentLife < this.MaxLife / 2) &&
                        (existRedPotion))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseItem, Database.COMMON_HUGE_RED_POTION);
                    }
                    else if ((this.CurrentMana < Database.CHILL_BURN_COST) &&
                        (existBluePotion))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseItem, Database.COMMON_HUGE_BLUE_POTION);
                    }
                    else if ((this.CurrentSkillPoint < Database.CARNAGE_RUSH_COST) &&
                        (existGreenPotion))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseItem, Database.COMMON_HUGE_GREEN_POTION);
                    }
                    else if (this.AI_TacticsNumber == 0)
                    {
                        if ((this.CurrentMana >= Database.CHILL_BURN_COST) &&
                            (target.CurrentFrozen <= 0) &&
                            (this.DetectCannotBeFrozen == false))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (this.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST &&
                                 this.CurrentPromisedKnowledge <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        } // todo (not blue bullet?)
                        else if (this.CurrentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if (this.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST &&
                            this.CurrentPromisedKnowledge <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                        else if (this.CurrentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if (this.CurrentSeventhMagic <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SEVENTH_MAGIC);
                        }
                        else if (this.CurrentSkillPoint >= Database.CARNAGE_RUSH_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else if (this.CurrentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    break;

                case Database.DUEL_VAN_HEHGUSTEL:
                    if (this.CurrentChargeCount <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.Charge, Database.TAMERU_EN);
                    }
                    else if (this.CurrentMana >= Database.PIERCING_FLAME_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                    }
                    else
                    {
                        SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                    }
                    break;

                case Database.DUEL_OHRYU_GENMA:
                    if ((this.CurrentOneAuthority <= 0) &&
                        (this.CurrentSkillPoint <= 30))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.ONE_AUTHORITY);
                    }
                    else
                    {
                        // 全てかかっている場合、次のAIへ
                        if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 3;
                        }
                        // ２つ満たしている場合、残り１つのAIを選択
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            this.AI_TacticsNumber = 2;
                        }
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 1;
                        }
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 0;
                        }
                        // １つ満たしている場合、残り２つのAIをいずれか選択
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 0;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 1;
                            }
                        }
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 0;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 2;
                            }
                        }
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 1;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 2;
                            }
                        }
                        // 全て満たしてない場合、ランダム３
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            this.AI_TacticsNumber = AP.Math.RandomInteger(3);
                        }

                        // AIに応じて、スキル変更
                        if (this.AI_TacticsNumber == 0)
                        {
                            if ((this.CurrentSkillPoint >= Database.IMPULSE_HIT_COST) &&
                                (target.CurrentImpulseHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 1)
                        {
                            if ((this.CurrentSkillPoint >= Database.CONCUSSIVE_HIT_COST) &&
                                (target.CurrentConcussiveHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 2)
                        {
                            if ((this.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST) &&
                                (target.CurrentOnslaughtHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 3)
                        {
                            if (this.CurrentSkillPoint >= Database.CARNAGE_RUSH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                            }
                            else if (this.CurrentSkillPoint >= Database.VIOLENT_SLASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.VIOLENT_SLASH);
                            }
                            else if (this.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.DOUBLE_SLASH);
                            }
                            else if (this.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                            }
                        }
                    }
                    break;

                case Database.DUEL_LADA_MYSTORUS:
                    if ((target.CurrentDamnation <= 0) &&
                        (this.CurrentMana >= Database.DAMNATION_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DAMNATION);
                    }
                    else if ((this.CurrentOneImmunity <= 0) &&
                             (this.CurrentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    else if ((target.CurrentEnrageBlast <= 0) &&
                             (this.CurrentMana >= Database.ENRAGE_BLAST_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                    }
                    else if ((target.CurrentBlazingField <= 0) &&
                             (this.CurrentMana >= Database.BLAZING_FIELD_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLAZING_FIELD);
                    }
                    else if ((this.CurrentSkyShieldValue < 3) &&
                             (target.CurrentMana >= Database.SKY_SHIELD_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SKY_SHIELD);
                    }
                    else
                    {
                        SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                    }
                    break;
                case Database.DUEL_SIN_OSCURETE:
                    if ((target.CurrentMana <= 0) && (this.CurrentMana >= Database.ZETA_EXPLOSION_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);
                    }
                    else if (this.BattleBarPos <= 400)
                    {
                        SetupActionCommand(this, this, PlayerAction.Defense, Database.DEFENSE_EN);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                    //if (this.CurrentMana >= Database.ONE_IMMUNITY_COST &&
                    //    this.CurrentOneImmunity <= 0)
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    //}

                    //else if (this.AI_TacticsNumber == 0)
                    //{
                    //    if (this.CurrentMana >= Database.DOOM_BLADE_COST && target.CurrentMana > 0)
                    //    {
                    //        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DOOM_BLADE);
                    //    }
                    //}
                    //else if (this.AI_TacticsNumber == 1)
                    //{
                    //    if ((this.CurrentHeatBoost <= 0) &&
                    //        (this.CurrentMana >= Database.HEAT_BOOST_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.HEAT_BOOST);
                    //    }
                    //    else if ((this.CurrentWordOfLife <= 0) &&
                    //             (this.CurrentMana >= Database.WORD_OF_LIFE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.WORD_OF_LIFE);
                    //    }
                    //    else if ((this.CurrentSaintPower <= 0) &&
                    //             (this.CurrentMana >= Database.SAINT_POWER_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SAINT_POWER);
                    //    }
                    //    else if ((this.CurrentProtection <= 0) &&
                    //             (this.CurrentMana >= Database.PROTECTION_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROTECTION);
                    //    }
                    //    else if ((this.CurrentPromisedKnowledge <= 0) &&
                    //             (this.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    //    }
                    //    else if ((this.CurrentBloodyVengeance <= 0) &&
                    //             (this.CurrentMana >= Database.BLOODY_VENGEANCE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLOODY_VENGEANCE);
                    //    }
                    //    else if ((this.CurrentRiseOfImage <= 0) &&
                    //             (this.CurrentMana >= Database.RISE_OF_IMAGE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RISE_OF_IMAGE);
                    //    }
                    //    else if ((this.CurrentShadowPact <= 0) &&
                    //             (this.CurrentMana >= Database.SHADOW_PACT_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SHADOW_PACT);
                    //    }
                    //    else if ((this.CurrentAbsorbWater <= 0) &&
                    //             (this.CurrentMana >= Database.ABSORB_WATER_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ABSORB_WATER);
                    //    }
                    //    else if ((this.CurrentEternalPresence <= 0) &&
                    //             (this.CurrentMana >= Database.ETERNAL_PRESENCE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ETERNAL_PRESENCE);
                    //    }
                    //}
                    //else
                    //{
                    //    if ((this.CurrentMana >= Database.ZETA_EXPLOSION_COST))
                    //    {
                    //        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);
                    //    }
                    //}
                    break;
                #endregion
                #region "ダミー素振り"
                case Database.DUEL_DUMMY_SUBURI:
                    if (this.CurrentProtection <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROTECTION);
                    }
                    else if (this.CurrentExaltedField <= 0)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.EXALTED_FIELD);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.FIRE_BALL);
                    }
                    return;

                    this.PA = PlayerAction.UseSkill;
                    this.CurrentSkillName = Database.NEGATE;
                    this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSkillName);
                    this.Target = target;
                    return;

                    this.PA = PlayerAction.UseSpell;
                    this.CurrentSpellName = Database.TIME_STOP;
                    this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                    this.Target = this;
                    return;

                    this.PA = PlayerAction.SpecialSkill;
                    this.ActionLabel.text = "BUFF!";
                    this.Target = target;
                    return;

                    if (this.CurrentVigorSense <= 0)
                    {
                        this.PA = PlayerAction.UseSkill;
                        this.CurrentSkillName = Database.VIGOR_SENSE;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSkillName);
                        this.Target = this;
                    }
                    else if (this.CurrentRisingAura <= 0)
                    {
                        this.PA = PlayerAction.UseSkill;
                        this.CurrentSkillName = Database.RISING_AURA;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSkillName);
                        this.Target = this;
                    }
                    else if (this.CurrentAbsorbWater <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.ABSORB_WATER;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentSaintPower <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.SAINT_POWER;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentProtection <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.PROTECTION;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentBloodyVengeance <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.BLOODY_VENGEANCE;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentPromisedKnowledge <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.PROMISED_KNOWLEDGE;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentHeatBoost <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.HEAT_BOOST;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    return;

                    if (target.CurrentNoResurrection <= 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        //this.ActionLabel.text = "スタン";
                        //this.ActionLabel.text = "沈黙";
                        //this.ActionLabel.text = "猛毒";
                        //this.ActionLabel.text = "誘惑";
                        //this.ActionLabel.text = "凍結";
                        //this.ActionLabel.text = "麻痺";
                        //this.ActionLabel.text = "鈍化";
                        //this.ActionLabel.text = "暗闇";
                        //this.ActionLabel.text = "スリップ";
                        this.ActionLabel.text = "蘇生不可";
                        this.Target = target;
                    }
                    else if (this.CurrentAbsorbWater <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.ABSORB_WATER;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentSaintPower <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.SAINT_POWER;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentProtection <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.PROTECTION;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentBloodyVengeance <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.BLOODY_VENGEANCE;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentPromisedKnowledge <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.PROMISED_KNOWLEDGE;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else if (this.CurrentHeatBoost <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.HEAT_BOOST;
                        this.ActionLabel.text = TruthActionCommand.ConvertToJapanese(this.CurrentSpellName);
                        this.Target = this;
                    }
                    else
                    {
                        this.PA = PlayerAction.None;
                        this.ActionLabel.text = Database.STAY_JP;
                        this.Target = this;
                    }
                    return;
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 20:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FLAME_STRIKE;
                            this.ActionLabel.text = Database.FLAME_STRIKE_JP;
                            this.Target = target;
                            break;
                        case 19:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.DEFLECTION;
                            this.ActionLabel.text = Database.DEFLECTION_JP;
                            this.Target = this;
                            break;
                        case 18:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.text = "弱体化「魔法防御」";
                            this.Target = target;
                            break;
                        case 17:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.text = "弱体化「魔法攻撃」";
                            this.Target = target;
                            break;
                        case 16:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.text = "弱体化「物理攻撃」";
                            this.Target = target;
                            break;
                        case 15:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.COUNTER_ATTACK;
                            this.ActionLabel.text = Database.COUNTER_ATTACK_JP;
                            this.Target = target;
                            break;
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.text = "スタン";
                            this.ActionLabel.text = "沈黙";
                            this.ActionLabel.text = "猛毒";
                            this.ActionLabel.text = "誘惑";
                            this.ActionLabel.text = "凍結";
                            this.ActionLabel.text = "麻痺";
                            this.ActionLabel.text = "鈍化";
                            this.ActionLabel.text = "暗闇";
                            this.ActionLabel.text = "スリップ";
                            this.ActionLabel.text = "蘇生不可";
                            this.Target = target;
                            break;
                        case 9:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.CRUSHING_BLOW;
                            this.ActionLabel.text = Database.CRUSHING_BLOW_JP;
                            this.Target = target;
                            break;
                        case 8:
                            this.PA = PlayerAction.None;
                            break;
                        case 7:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.WORD_OF_MALICE;
                            this.ActionLabel.text = Database.WORD_OF_MALICE_JP;
                            this.Target = target;
                            break;
                        case 6:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STRAIGHT_SMASH;
                            this.ActionLabel.text = Database.STRAIGHT_SMASH_JP;
                            this.Target = target;
                            break;
                        case 5:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STANCE_OF_EYES;
                            this.ActionLabel.text = Database.STANCE_OF_EYES_JP;
                            this.Target = target;
                            break;
                        case 4:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.NEGATE;
                            this.ActionLabel.text = Database.NEGATE_JP;
                            this.Target = target;
                            break;
                        case 3:
                            this.PA = PlayerAction.Defense;
                            break;
                        case 2:
                            this.PA = PlayerAction.NormalAttack;
                            this.ActionLabel.text = Database.ATTACK_JP;
                            this.Target = target;
                            break;
                    }
                    #endregion
                    break;
            }
        }


        public void Initialize(string createName)
        {
            Debug.Log("TruthEnemyCharacter(S) " + createName);
            // Expリスト設定
            double maxExp = 140000;
            double[] factorExp = { 1.07, 1.20, 1.20, 1.20, 1.50, 0.80, 1.07, 1.08, 1.20, 1.20, 1.50, 0.80, 1.08, 1.10, 1.10, 1.10, 1.20, 1.50, 0.80, 1.05, 1.20, 1.20, 1.20, 1.50, 2.00,
                                   0.60, 1.03, 1.03, 1.10, 1.30, 0.75, 1.03, 1.03, 1.10, 1.10, 1.30, 0.75, 1.03, 1.05, 1.05, 1.05, 1.30, 0.70, 1.03, 1.10, 1.10, 1.30, 1.40, 1.04, 1.04, 1.04, 1.00, 1.04, 1.00, 1.00, 2.00,
                                   0.60, 1.03, 1.03, 1.10, 1.30, 0.75, 1.03, 1.03, 1.10, 1.10, 1.30, 0.75, 1.03, 1.05, 1.05, 1.05, 1.30, 0.70, 1.03, 1.10, 1.10, 1.30, 2.00, 
                                   0.60, 1.03, 1.03, 1.10, 1.30, 0.75, 1.03, 1.03, 1.10, 1.10, 1.40, 0.75, 1.03, 1.05, 1.05, 1.05, 1.40, 0.70, 1.03, 1.10, 1.10, 1.40, 2.00, 1.00, 1.00,
                                   0.50, 1.00, 1.00, 1.00, 1.00,
                                   1.00,
                                   1.00 };
            double[] listExp = new double[factorExp.Length];
            double current = maxExp;
            for (int ii = 0; ii < factorExp.Length; ii++)
            {
                listExp[factorExp.Length - 1 - ii] = current / factorExp[factorExp.Length - 1 - ii];
                current = listExp[factorExp.Length - 1 - ii];
            }
            for (int ii = 0; ii < factorExp.Length; ii++)
            {
                listExp[ii] = Math.Round(listExp[ii]);
            }
            // Goldリスト設定
            double maxGold = 200000;
            double[] factorGold = { 1.10, 1.20, 1.20, 1.20, 1.50, 0.80, 1.10, 1.10, 1.20, 1.20, 1.50, 0.80, 1.10, 1.10, 1.20, 1.20, 1.20, 1.50, 0.80, 1.10, 1.20, 1.20, 1.20, 1.50, 3.00,
                                    0.45, 1.05, 1.05, 1.10, 1.30, 0.80, 1.05, 1.05, 1.10, 1.10, 1.30, 0.80, 1.05, 1.10, 1.10, 1.10, 1.30, 0.80, 1.05, 1.10, 1.10, 1.30, 1.50, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 3.00,
                                    0.40, 1.05, 1.05, 1.10, 1.30, 0.80, 1.05, 1.05, 1.10, 1.10, 1.30, 0.80, 1.05, 1.10, 1.10, 1.10, 1.30, 0.80, 1.02, 1.05, 1.05, 1.30, 2.50,
                                    0.43, 1.02, 1.02, 1.05, 1.30, 0.80, 1.02, 1.02, 1.05, 1.05, 1.30, 0.80, 1.02, 1.05, 1.05, 1.05, 1.30, 0.80, 1.02, 1.05, 1.05, 1.20, 1.50, 1.00, 1.00,
                                    0.50, 1.00, 1.00, 1.00, 1.00, 2.00,
                                    1.00,
                                    1.00,
                                  };
            double[] listGold = new double[factorGold.Length];
            double currentGold = maxGold;
            for (int ii = 0; ii < factorGold.Length; ii++)
            {
                listGold[factorGold.Length - 1 - ii] = currentGold / factorGold[factorGold.Length - 1 - ii];
                currentGold = listGold[factorGold.Length - 1 - ii];
            }

            for (int ii = 0; ii < factorGold.Length; ii++)
            {
                listGold[ii] = Math.Round(listGold[ii]);
            }


            this.DropItem = new string[MAX_DROPITEM_SIZE];
            for (int ii = 0; ii < MAX_DROPITEM_SIZE; ii++)
            {
                this.DropItem[ii] = String.Empty;
            }

            // 敵はスキル・魔法が使えなくなっていても、DUELでは使える風に見せかけるため、常にTRUEとします。
            this.AvailableMana = true;
            this.AvailableSkill = true;

            this.FirstName = createName;
            switch (createName)
            {
                #region "ダンジョン１階"
                #region "エリア１"
                case Database.ENEMY_HIYOWA_BEATLE:
                    SetupParameterMonster(1, 8, 1, 1, 1, 2, (int)(listExp[0]), (int)(listGold[0]), 5);
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_BEATLE_TOGATTA_TUNO;
                    break;
                case Database.ENEMY_HENSYOKU_PLANT:
                    SetupParameterMonster(1, 7, 1, 1, 1, 2, (int)(listExp[1]), (int)(listGold[1]), 8);
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_HENSYOKU_KUKI;
                    break;
                case Database.ENEMY_GREEN_CHILD:
                    SetupParameterMonster(2, 7, 4, 12, 3, 3, (int)(listExp[2]), (int)(listGold[2]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_GREEN_SIKISO;
                    break;
                case Database.ENEMY_TINY_MANTIS:
                    SetupParameterMonster(3, 14, 6, 5, 4, 3, (int)(listExp[3]), (int)(listGold[3]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_MANTIS_TAIEKI;
                    break;
                case Database.ENEMY_KOUKAKU_WURM:
                    SetupParameterMonster(4, 18, 7, 6, 5, 3, (int)(listExp[4]), (int)(listGold[4]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_WARM_NO_KOUKAKU;
                    break;
                case Database.ENEMY_MANDRAGORA:
                    SetupParameterMonster(5, 10, 12, 25, 18, 7, (int)(listExp[5]), (int)(listGold[5]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = Database.COMMON_MANDORAGORA_ROOT;
                    break;
                #endregion

                #region "エリア２"
                case Database.ENEMY_SUN_FLOWER:
                    SetupParameterMonster(6, 8, 10, 20, 12, 4, (int)(listExp[6]), (int)(listGold[6]));
                    this.baseResistFire = 30;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.COMMON_SUN_LEAF;
                    break;
                case Database.ENEMY_RED_HOPPER:
                    SetupParameterMonster(7, 20, 16, 9, 13, 4, (int)(listExp[7]), (int)(listGold[7]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Regist_Physical;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.COMMON_INAGO;
                    break;
                case Database.ENEMY_EARTH_SPIDER:
                    SetupParameterMonster(8, 25, 11, 11, 15, 5, (int)(listExp[8]), (int)(listGold[8]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.COMMON_SPIDER_SILK;
                    break;
                case Database.ENEMY_WILD_ANT:
                    SetupParameterMonster(9, 35, 15, 13, 30, 5, (int)(listExp[9]), (int)(listGold[9]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.COMMON_ANT_ESSENCE;
                    this.DropItem[1] = Database.COMMON_YELLOW_MATERIAL;
                    break;
                case Database.ENEMY_ALRAUNE:
                    SetupParameterMonster(10, 18, 17, 40, 25, 6, (int)(listExp[10]), (int)(listGold[10]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.COMMON_ALRAUNE_KAHUN;
                    break;
                case Database.ENEMY_POISON_MARY:
                    SetupParameterMonster(12, 20, 20, 50, 45, 12, (int)(listExp[11]), (int)(listGold[11]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = Database.RARE_MARY_KISS;
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_ZASSYOKU_RABBIT:
                    SetupParameterMonster(13, 40, 18, 20, 40, 7, (int)(listExp[12]), (int)(listGold[12]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_RABBIT_KEGAWA;
                    this.DropItem[1] = Database.COMMON_RABBIT_MEAT;
                    break;
                case Database.ENEMY_SPEEDY_TAKA:
                    SetupParameterMonster(14, 30, 30, 22, 33, 7, (int)(listExp[13]), (int)(listGold[13]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_TAKA_FETHER;
                    break;
                case Database.ENEMY_ASH_CREEPER:
                    SetupParameterMonster(15, 25, 22, 45, 45, 8, (int)(listExp[14]), (int)(listGold[14]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_ASH_EGG;
                    break;
                case Database.ENEMY_GIANT_SNAKE:
                    SetupParameterMonster(16, 55, 28, 30, 50, 8, (int)(listExp[15]), (int)(listGold[15]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_SNEAK_UROKO;
                    break;
                case Database.ENEMY_WONDER_SEED:
                    SetupParameterMonster(17, 60, 32, 35, 55, 9, (int)(listExp[16]), (int)(listGold[16]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_PLANTNOID_SEED;
                    break;
                case Database.ENEMY_FLANSIS_KNIGHT:
                    SetupParameterMonster(18, 65, 40, 40, 65, 9, (int)(listExp[17]), (int)(listGold[17]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.COMMON_TOGE_HAETA_SYOKUSYU;
                    break;
                case Database.ENEMY_SHOTGUN_HYUI:
                    SetupParameterMonster(20, 90, 70, 42, 81, 16, (int)(listExp[18]), (int)(listGold[18]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = Database.RARE_HYUI_SEED;
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_WAR_WOLF:
                    SetupParameterMonster(21, 75, 50, 50, 75, 10, (int)(listExp[19]), (int)(listGold[19]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.COMMON_OOKAMI_FANG;
                    break;
                case Database.ENEMY_BRILLIANT_BUTTERFLY:
                    SetupParameterMonster(22, 50, 53, 80, 60, 10, (int)(listExp[20]), (int)(listGold[20]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.COMMON_BRILLIANT_RINPUN;
                    break;
                case Database.ENEMY_MIST_ELEMENTAL:
                    SetupParameterMonster(23, 90, 60, 55, 80, 11, (int)(listExp[21]), (int)(listGold[21]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.COMMON_MIST_CRYSTAL;
                    break;
                case Database.ENEMY_WHISPER_DRYAD:
                    SetupParameterMonster(24, 55, 65, 95, 70, 11, (int)(listExp[22]), (int)(listGold[22]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.COMMON_DRYAD_RINPUN;
                    break;
                case Database.ENEMY_BLOOD_MOSS:
                    SetupParameterMonster(25, 100, 70, 60, 66, 11, (int)(listExp[23]), (int)(listGold[23]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.COMMON_RED_HOUSI;
                    break;
                case Database.ENEMY_MOSSGREEN_DADDY:
                    SetupParameterMonster(27, 65, 75, 110, 82, 17, (int)(listExp[24]), (int)(listGold[24]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = Database.RARE_MOSSGREEN_EKISU;
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_KARAMITUKU_FLANSIS:
                    SetupParameterMonster(30, 200, 100, 200, 602, 45, (int)(listExp[25]), (int)(listGold[25]));
                    this.baseInstantPoint = 3000;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss1;
                    this.DropItem[0] = Database.EPIC_ORB_GROW_GREEN;
                    break;
                #endregion
                #endregion
                #region "ダンジョン２階"
                #region "エリア１"
                case Database.ENEMY_DAGGER_FISH:
                    SetupParameterMonster(30, 160, 130, 90, 100, 20, (int)(listExp[26]), (int)(listGold[26]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_DAGGERFISH_UROKO;
                    break;

                case Database.ENEMY_SIPPU_FLYING_FISH:
                    SetupParameterMonster(31, 140, 180, 90, 110, 20, (int)(listExp[27]), (int)(listGold[27]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_SIPPUU_HIRE;
                    break;

                case Database.ENEMY_ORB_SHELLFISH:
                    SetupParameterMonster(32, 90, 140, 180, 120, 20, (int)(listExp[28]), (int)(listGold[28]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_WHITE_MAGATAMA;
                    this.DropItem[1] = Database.COMMON_BLUE_MAGATAMA;
                    break;

                case Database.ENEMY_SPLASH_KURIONE:
                    SetupParameterMonster(33, 110, 155, 190, 150, 22, (int)(listExp[29]), (int)(listGold[29]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_BLUEWHITE_SHARP_TOGE;
                    this.DropItem[1] = Database.COMMON_KURIONE_ZOUMOTU;
                    break;

                case Database.ENEMY_TRANSPARENT_UMIUSHI:
                    SetupParameterMonster(35, 120, 180, 230, 220, 30, (int)(listExp[30]), (int)(listGold[30]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.RARE_TRANSPARENT_POWDER;
                    break;
                #endregion

                #region "エリア２"
                case Database.ENEMY_ROLLING_MAGURO:
                    SetupParameterMonster(36, 210, 165, 130, 200, 25, (int)(listExp[31]), (int)(listGold[31]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_RENEW_AKAMI;
                    break;

                case Database.ENEMY_RANBOU_SEA_ARTINE:
                    SetupParameterMonster(37, 220, 175, 135, 260, 25, (int)(listExp[32]), (int)(listGold[32]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_SEA_WASI_KUTIBASI;
                    break;

                case Database.ENEMY_BLUE_SEA_WASI:
                    SetupParameterMonster(38, 190, 250, 140, 230, 25, (int)(listExp[33]), (int)(listGold[33]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_WASI_BLUE_FEATHER;
                    break;

                case Database.ENEMY_BRIGHT_SQUID:
                    SetupParameterMonster(40, 140, 190, 250, 300, 28, (int)(listExp[34]), (int)(listGold[34]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_BRIGHT_GESO;
                    break;

                case Database.ENEMY_GANGAME:
                    SetupParameterMonster(41, 260, 200, 150, 350, 28, (int)(listExp[35]), (int)(listGold[35]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_GANGAME_EGG;
                    break;

                case Database.ENEMY_BIGMOUSE_JOE:
                    SetupParameterMonster(43, 320, 240, 160, 560, 40, (int)(listExp[36]), (int)(listGold[36]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.RARE_JOE_ARM;
                    this.DropItem[1] = Database.RARE_JOE_LEG;
                    this.DropItem[2] = Database.RARE_JOE_TONGUE;
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_MOGURU_MANTA:
                    SetupParameterMonster(44, 150, 220, 300, 500, 32, (int)(listExp[37]), (int)(listGold[37]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_SOFT_BIG_HIRE;
                    break;

                case Database.ENEMY_FLOATING_GOLD_FISH:
                    SetupParameterMonster(45, 280, 280, 160, 550, 32, (int)(listExp[38]), (int)(listGold[38]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_PURE_WHITE_BIGEYE;
                    break;

                case Database.ENEMY_GOEI_HERMIT_CLUB:
                    SetupParameterMonster(47, 350, 280, 170, 650, 36, (int)(listExp[39]), (int)(listGold[39]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_GOTUGOTU_KARA;
                    break;

                case Database.ENEMY_ABARE_SHARK:
                    SetupParameterMonster(48, 360, 290, 180, 750, 36, (int)(listExp[40]), (int)(listGold[40]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_SAME_NANKOTSU;
                    break;

                case Database.ENEMY_VANISHING_CORAL:
                    SetupParameterMonster(49, 180, 300, 370, 700, 36, (int)(listExp[41]), (int)(listGold[41]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_HALF_TRANSPARENT_ROCK_ASH;
                    break;

                case Database.ENEMY_CASSY_CANCER:
                    SetupParameterMonster(51, 450, 350, 250, 950, 56, (int)(listExp[42]), (int)(listGold[42]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.RARE_SEKIKASSYOKU_HASAMI;
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_BLACK_STARFISH:
                    SetupParameterMonster(52, 200, 320, 390, 800, 40, (int)(listExp[43]), (int)(listGold[43]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_KOUSITUKA_MATERIAL;
                    break;

                case Database.ENEMY_RAINBOW_ANEMONE:
                    SetupParameterMonster(53, 210, 330, 400, 860, 40, (int)(listExp[44]), (int)(listGold[44]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_NANAIRO_SYOKUSYU;
                    break;

                case Database.ENEMY_MACHIBUSE_ANKOU:
                    SetupParameterMonster(55, 450, 360, 220, 1000, 45, (int)(listExp[45]), (int)(listGold[45]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_PUREWHITE_KIMO;
                    break;

                case Database.ENEMY_EDGED_HIGH_SHARK:
                    SetupParameterMonster(56, 470, 370, 230, 1060, 45, (int)(listExp[46]), (int)(listGold[46]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_AOSAME_KENSHI;
                    this.DropItem[1] = Database.COMMON_AOSAME_UROKO;
                    break;

                case Database.ENEMY_EIGHT_EIGHT:
                    SetupParameterMonster(58, 250, 430, 550, 1300, 62, (int)(listExp[47]), (int)(listGold[47]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_EIGHTEIGHT_KUROSUMI;
                    this.DropItem[1] = Database.COMMON_EIGHTEIGHT_KYUUBAN;
                    break;
                #endregion

                #region "力の部屋：ボス"
                case Database.ENEMY_BRILLIANT_SEA_PRINCE:
                    SetupParameterMonster(65, 700, 550, 700, 2872, 100, (int)(listExp[48]), (int)(listGold[48]));
                    this.baseInstantPoint = 2400;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss21;
                    break;

                case Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN:
                    SetupParameterMonster(66, 300, 500, 800, 3070, 100, (int)(listExp[49]), (int)(listGold[49]));
                    this.baseInstantPoint = 1800;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss22;
                    break;

                case Database.ENEMY_SHELL_SWORD_KNIGHT:
                    SetupParameterMonster(67, 900, 650, 300, 3368, 100, (int)(listExp[50]), (int)(listGold[50]));
                    this.baseInstantPoint = 3900;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss23;
                    break;

                case Database.ENEMY_JELLY_EYE_BRIGHT_RED:
                    SetupParameterMonster(68, 300, 550, 800, 3166, 100, (int)(listExp[51]), (int)(listGold[51]));
                    this.baseInstantPoint = 2200;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss24;
                    //this.baseResistFire = 2000; // これだと画面上で分からないため、BattleEnemy側の初期化でサポート
                    break;

                case Database.ENEMY_JELLY_EYE_DEEP_BLUE:
                    SetupParameterMonster(68, 300, 550, 800, 3166, 100, (int)(listExp[52]), (int)(listGold[52]));
                    this.baseInstantPoint = 2700;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss24;
                    //this.baseResistIce = 2000; // これだと画面上で分からないため、BattleEnemy側の初期化でサポート
                    break;


                case Database.ENEMY_SEA_STAR_ORIGIN_KING:
                    SetupParameterMonster(75, 600, 450, 600, 3852, 100, (int)(listExp[53]), (int)(listGold[53]));
                    this.baseInstantPoint = 12000;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                case Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU:
                    SetupParameterMonster(70, 900, 650, 300, 2662, 100, (int)(listExp[54]), (int)(listGold[54]));
                    this.baseInstantPoint = 5400;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                case Database.ENEMY_SEA_STAR_KNIGHT_AMARA:
                    SetupParameterMonster(70, 900, 680, 300, 2662, 100, (int)(listExp[55]), (int)(listGold[55]));
                    this.baseInstantPoint = 3600;
                    this.UseStackCommand = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                #endregion

                #region "ボス"

                case Database.ENEMY_BOSS_LEVIATHAN:
                    SetupParameterMonster(80, 850, 750, 750, 8142, 130, (int)(listExp[56]), (int)(listGold[56]));
                    this.baseInstantPoint = 55000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss2;
                    this.DropItem[0] = Database.EPIC_ORB_GROUNDSEA_STAR;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "３階"
                #region "エリア１"
                case Database.ENEMY_TOSSIN_ORC:
                    SetupParameterMonster(70, 650, 500, 350, 2462, 50, (int)(listExp[57]), (int)(listGold[57]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_ORC_MOMONIKU;
                    break;

                case Database.ENEMY_SNOW_CAT:
                    SetupParameterMonster(71, 550, 600, 360, 2260, 50, (int)(listExp[58]), (int)(listGold[58]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_SNOW_CAT_KEGAWA;
                    break;

                case Database.ENEMY_WAR_MAMMOTH:
                    SetupParameterMonster(72, 670, 540, 370, 3058, 50, (int)(listExp[59]), (int)(listGold[59]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_BIG_HIZUME;
                    break;

                case Database.ENEMY_WINGED_COLD_FAIRY:
                    SetupParameterMonster(74, 400, 570, 730, 3154, 60, (int)(listExp[60]), (int)(listGold[60]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_FAIRY_POWDER;
                    break;

                case Database.ENEMY_FREEZING_GRIFFIN:
                    SetupParameterMonster(75, 850, 600, 420, 3352, 60, (int)(listExp[61]), (int)(listGold[61]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.RARE_GRIFFIN_WHITE_FEATHER;
                    break;

                #endregion
                #region "エリア２"
                case Database.ENEMY_BRUTAL_OGRE:
                    SetupParameterMonster(76, 800, 580, 440, 3650, 70, (int)(listExp[62]), (int)(listGold[62]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_GOTUGOTU_KONBOU;
                    break;

                case Database.ENEMY_HYDRO_LIZARD:
                    SetupParameterMonster(77, 830, 600, 450, 4048, 70, (int)(listExp[63]), (int)(listGold[63]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_LIZARD_UROKO;
                    break;

                case Database.ENEMY_PENGUIN_STAR:
                    SetupParameterMonster(78, 720, 720, 720, 4346, 70, (int)(listExp[64]), (int)(listGold[64]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_EMBLEM_OF_PENGUIN;
                    break;

                case Database.ENEMY_ICEBERG_SPIRIT:
                    SetupParameterMonster(80, 470, 640, 920, 4742, 80, (int)(listExp[65]), (int)(listGold[65]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_KINKIN_ICE;
                    break;

                case Database.ENEMY_SWORD_TOOTH_TIGER:
                    SetupParameterMonster(81, 950, 660, 480, 5140, 80, (int)(listExp[66]), (int)(listGold[66]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_SHARPNESS_TIGER_TOOTH;
                    break;

                case Database.ENEMY_FEROCIOUS_RAGE_BEAR:
                    SetupParameterMonster(83, 1300, 750, 600, 6536, 150, (int)(listExp[67]), (int)(listGold[67]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.RARE_BEAR_CLAW_KAKERA;
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_WINTER_ORB:
                    SetupParameterMonster(84, 520, 680, 1000, 5834, 90, (int)(listExp[68]), (int)(listGold[68]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_TOUMEI_SNOW_CRYSTAL;
                    break;

                case Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI:
                    SetupParameterMonster(85, 530, 700, 1030, 6032, 90, (int)(listExp[69]), (int)(listGold[69]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_WHITE_AZARASHI_MEAT;
                    break;

                case Database.ENEMY_MAJESTIC_CENTAURUS:
                    SetupParameterMonster(87, 1080, 750, 550, 6528, 100, (int)(listExp[70]), (int)(listGold[70]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_CENTAURUS_LEATHER;
                    break;

                case Database.ENEMY_INTELLIGENCE_ARGONIAN:
                    SetupParameterMonster(88, 950, 770, 950, 6726, 100, (int)(listExp[71]), (int)(listGold[71]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_ARGONIAN_PURPLE_UROKO;
                    break;

                case Database.ENEMY_MAGIC_HYOU_RIFLE:
                    SetupParameterMonster(89, 580, 790, 1120, 7024, 100, (int)(listExp[72]), (int)(listGold[72]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_BLUE_DANGAN_KAKERA;
                    break;

                case Database.ENEMY_PURE_BLIZZARD_CRYSTAL:
                    SetupParameterMonster(91, 700, 860, 1600, 9420, 180, (int)(listExp[73]), (int)(listGold[73]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.RARE_PURE_CRYSTAL;
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_PURPLE_EYE_WARE_WOLF:
                    SetupParameterMonster(92, 1200, 800, 600, 7818, 110, (int)(listExp[74]), (int)(listGold[74]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_WOLF_KEGAWA;
                    break;

                case Database.ENEMY_FROST_HEART:
                    SetupParameterMonster(94, 630, 820, 1230, 7614, 110, (int)(listExp[75]), (int)(listGold[75]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_FROZEN_HEART;
                    break;

                case Database.ENEMY_WHITENIGHT_GRIZZLY:
                    SetupParameterMonster(96, 1300, 860, 680, 9010, 120, (int)(listExp[76]), (int)(listGold[76]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_CLAW_HEART;
                    break;

                case Database.ENEMY_WIND_BREAKER:
                    SetupParameterMonster(97, 1100, 880, 1100, 9308, 120, (int)(listExp[77]), (int)(listGold[77]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_ESSENCE_OF_WIND;
                    break;

                case Database.ENEMY_TUNDRA_LONGHORN_DEER:
                    SetupParameterMonster(100, 800, 920, 2000, 11802, 200, (int)(listExp[78]), (int)(listGold[78]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.RARE_TUNDRA_DEER_HORN;
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_HOWLING_SEIZER:
                    SetupParameterMonster(120, 4000, 1200, 1500, 64762, 500, (int)(listExp[79]), (int)(listGold[79]));
                    this.baseInstantPoint = 15000;
                    this.ResistStun = true;
                    this.ResistParalyze = true;
                    this.ResistFrozen = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss3;
                    this.DropItem[0] = Database.EPIC_ORB_SILENT_COLD_ICE;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "４階"
                #region "エリア１"
                case Database.ENEMY_GENAN_HUNTER:
                    SetupParameterMonster(101, 1400, 920, 700, 9500, 150, (int)(listExp[80]), (int)(listGold[80]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_HUNTER_SEVEN_TOOL;
                    this.InitialTarget = TargetLogic.Back;
                    break;

                case Database.ENEMY_BEAST_MASTER:
                    SetupParameterMonster(102, 1430, 950, 720, 11798, 150, (int)(listExp[81]), (int)(listGold[81]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_BEAST_KEGAWA;
                    break;

                case Database.ENEMY_ELDER_ASSASSIN:
                    SetupParameterMonster(103, 1500, 1460, 750, 10796, 150, (int)(listExp[82]), (int)(listGold[82]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.RARE_BLOOD_DAGGER_KAKERA;
                    this.InitialTarget = TargetLogic.Back;
                    break;

                case Database.ENEMY_FALLEN_SEEKER:
                    SetupParameterMonster(105, 800, 1000, 1600, 12792, 170, (int)(listExp[83]), (int)(listGold[83]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_SABI_BUGU;
                    break;

                case Database.ENEMY_MEPHISTO_RIGHTARM:
                    SetupParameterMonster(107, 1100, 1100, 2500, 19788, 260, (int)(listExp[84]), (int)(listGold[84]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.RARE_MEPHISTO_BLACKLIGHT;
                    break;

                #endregion
                #region "エリア２"
                case Database.ENEMY_DARK_MESSENGER:
                    SetupParameterMonster(108, 850, 1050, 1700, 16786, 180, (int)(listExp[85]), (int)(listGold[85]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_SEEKER_HEAD;
                    break;

                case Database.ENEMY_MASTER_LOAD:
                    SetupParameterMonster(109, 860, 1080, 1730, 20784, 180, (int)(listExp[86]), (int)(listGold[86]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_DARK;
                    break;

                case Database.ENEMY_EXECUTIONER:
                    SetupParameterMonster(110, 1760, 1110, 870, 23782, 180, (int)(listExp[87]), (int)(listGold[87]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_EXECUTIONER_ROBE;
                    break;

                case Database.ENEMY_MARIONETTE_NEMESIS:
                    SetupParameterMonster(112, 1900, 1170, 900, 28778, 200, (int)(listExp[88]), (int)(listGold[88]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_NEMESIS_ESSENCE;
                    break;

                case Database.ENEMY_BLACKFIRE_MASTER_BLADE:
                    SetupParameterMonster(113, 1950, 1200, 920, 31776, 200, (int)(listExp[89]), (int)(listGold[89]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.RARE_MASTERBLADE_KAKERA;
                    this.DropItem[1] = Database.RARE_MASTERBLADE_FIRE;
                    break;

                case Database.ENEMY_SIN_THE_DARKELF:
                    SetupParameterMonster(115, 1600, 1300, 3200, 37772, 300, (int)(listExp[90]), (int)(listGold[90]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_GREAT_JEWELCROWN;
                    break;
                #endregion
                #region "エリア３"
                case Database.ENEMY_SUN_STRIDER:
                    SetupParameterMonster(116, 2000, 1250, 1000, 34770, 220, (int)(listExp[91]), (int)(listGold[91]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_SHINE;
                    break;

                case Database.ENEMY_ARC_DEMON:
                    SetupParameterMonster(117, 2050, 1280, 1020, 36768, 220, (int)(listExp[92]), (int)(listGold[92]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_DEMON_HORN;
                    break;

                case Database.ENEMY_BALANCE_IDLE:
                    SetupParameterMonster(119, 1800, 1380, 1800, 41764, 240, (int)(listExp[93]), (int)(listGold[93]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.COMMON_KUMITATE_TENBIN;
                    this.DropItem[1] = Database.COMMON_KUMITATE_TENBIN_BOU;
                    this.DropItem[2] = Database.COMMON_KUMITATE_TENBIN_DOU;
                    break;

                case Database.ENEMY_UNDEAD_WYVERN:
                    SetupParameterMonster(120, 1050, 1410, 2200, 43762, 240, (int)(listExp[94]), (int)(listGold[94]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.COMMON_WYVERN_BONE;
                    break;

                case Database.ENEMY_GO_FLAME_SLASHER:
                    SetupParameterMonster(121, 2250, 1440, 1070, 45760, 240, (int)(listExp[95]), (int)(listGold[95]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_FLAME;
                    break;

                case Database.ENEMY_DEVIL_CHILDREN:
                    SetupParameterMonster(123, 2800, 1600, 2800, 53756, 340, (int)(listExp[96]), (int)(listGold[96]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_BLACK_SEAL_IMPRESSION;
                    break;
                #endregion
                #region "エリア４"
                case Database.ENEMY_HOWLING_HORROR:
                    SetupParameterMonster(124, 1120, 1500, 2300, 50754, 260, (int)(listExp[97]), (int)(listGold[97]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.COMMON_ONRYOU_HAKO;
                    break;

                case Database.ENEMY_PAIN_ANGEL:
                    SetupParameterMonster(125, 2350, 1530, 1150, 52752, 260, (int)(listExp[98]), (int)(listGold[98]));
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_ANGEL_SILK;
                    break;

                case Database.ENEMY_CHAOS_WARDEN:
                    SetupParameterMonster(127, 1200, 1580, 2500, 56748, 280, (int)(listExp[99]), (int)(listGold[99]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_CHAOS_SIZUKU;
                    break;

                case Database.ENEMY_DREAD_KNIGHT:
                    SetupParameterMonster(128, 2550, 1610, 1250, 58746, 280, (int)(listExp[100]), (int)(listGold[100]));
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_DREAD_EXTRACT;
                    break;

                case Database.ENEMY_DOOM_BRINGER:
                    SetupParameterMonster(130, 3200, 1800, 2000, 71742, 380, (int)(listExp[101]), (int)(listGold[101]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_DOOMBRINGER_KAKERA;
                    this.DropItem[1] = Database.RARE_DOOMBRINGER_TUKA;
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_LEGIN_ARZE_1:
                case Database.ENEMY_BOSS_LEGIN_ARZE_2:
                case Database.ENEMY_BOSS_LEGIN_ARZE_3:
                    //this.name = Database.ENEMY_BOSS_LEGIN_ARZE;
                    if (createName == Database.ENEMY_BOSS_LEGIN_ARZE_1) { SetupParameterMonster(150, 1, 2500, 3000, 294702, 450, (int)(listExp[102]), (int)(listGold[102])); }
                    if (createName == Database.ENEMY_BOSS_LEGIN_ARZE_2) { SetupParameterMonster(150, 1, 2500, 3000, 336702, 450, (int)(listExp[103]), (int)(listGold[103])); }
                    if (createName == Database.ENEMY_BOSS_LEGIN_ARZE_3) { SetupParameterMonster(150, 1, 2500, 3000, 381702, 450, (int)(listExp[104]), (int)(listGold[104])); }
                    this.baseMana = 2720000;
                    this.baseInstantPoint = 20000;
                    this.ResistStun = true;
                    this.ResistParalyze = true;
                    this.ResistFrozen = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss4;
                    //this.DropItem[0] = Database.EPIC_ORB_DESTRUCT_FIRE;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "５階"
                case Database.ENEMY_PHOENIX:
                    SetupParameterMonster(160, 2500, 2000, 3500, 79682, 500, (int)(listExp[105]), (int)(listGold[105]));
                    this.baseResistFire = 30000;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_NINE_TAIL:
                    SetupParameterMonster(160, 3500, 2150, 2500, 83682, 500, (int)(listExp[106]), (int)(listGold[106]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_MEPHISTOPHELES:
                    SetupParameterMonster(160, 3300, 3000, 2500, 87682, 500, (int)(listExp[107]), (int)(listGold[107]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_JUDGEMENT:
                    SetupParameterMonster(160, 4000, 2300, 4000, 91682, 500, (int)(listExp[108]), (int)(listGold[108]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_EMERALD_DRAGON:
                    SetupParameterMonster(160, 2500, 2450, 5000, 95682, 500, (int)(listExp[109]), (int)(listGold[109]));
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_BOSS_BYSTANDER_EMPTINESS:
                    this.firstName = Database.ENEMY_BOSS_BYSTANDER_EMPTINESS;
                    SetupParameterMonster(200, 3000, 4200, 3000, 999999, 500, (int)(listExp[110]), 0);
                    this.baseLife = 9;
                    this.baseInstantPoint = 8000;
                    this.UseStackCommand = true;
                    this.ResistBlind = true;
                    this.ResistFrozen = true;
                    this.ResistNoResurrection = true;
                    this.ResistParalyze = true;
                    this.ResistPoison = true;
                    this.ResistSilence = true;
                    this.ResistSlip = true;
                    this.ResistSlow = true;
                    this.ResistStun = true;
                    this.ResistTemptation = true;
                    this.gold = 0;
                    this.experience = 0;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss5;
                    break;
                #endregion

                #region "真実世界"
                case Database.ENEMY_LAST_RANA_AMILIA:
                    this.fullName = Database.ENEMY_LAST_RANA_AMILIA;
                    SetupParameter(70, 650, 900, 1730, 1500, 270);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Legendary;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_JUZA_THE_PHANTASMAL_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.COMMON_PLATINUM_RING_1);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PLATINUM_RING_10);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_SINIKIA_KAHLHANZ:
                    this.fullName = Database.ENEMY_LAST_SINIKIA_KAHLHANZ;
                    SetupParameter(70, 1, 1000, 1850, 1899, 300);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Legendary;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD_1);
                    this.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE_2);
                    this.Accessory = new ItemBackPack(Database.RARE_DANZAI_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_OL_LANDIS:
                    this.fullName = Database.ENEMY_LAST_OL_LANDIS;
                    SetupParameter(70, 1850, 1000, 1, 1899, 300);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Legendary;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_JUZA_THE_PHANTASMAL_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_TYOU_KOU_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.EPIC_EZEKRIEL_ARMOR_SIGIL);
                    this.Accessory = new ItemBackPack(Database.RARE_DANZAI_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_CRYSTAL);
                    this.Area = MonsterArea.Duel;
                    this.OpponentUseInstantPoint = true;
                    break;

                case Database.ENEMY_LAST_VERZE_ARTIE:
                    this.fullName = Database.ENEMY_LAST_VERZE_ARTIE;
                    SetupParameter(70, 1, 2500, 1, 2547, 300);
                    this.experience = 0;
                    this.gold = 0;
                    this.baseSpecialInstant = 15000;
                    this.Rare = RareString.Legendary;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_SIN_VERZE_ARTIE:
                    this.fullName = Database.ENEMY_LAST_SIN_VERZE_ARTIE;
                    this.FirstName = Database.ENEMY_LAST_SIN_VERZE_ARTIE;
                    SetupParameter(80, 3500, 3000, 4000, 8600, 2200);
                    this.experience = 0;
                    this.gold = 0;
                    this.baseSpecialInstant = 12000;
                    this.Rare = RareString.Legendary;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_TAU_WHITE_SILVER_SWORD);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_SEFINE_HYMNUS_RING);
                    this.Area = MonsterArea.LastBoss;
                    break;


                #endregion

                #region "DUEL闘技場"
                case Database.DUEL_EONE_FULNEA:
                    this.fullName = Database.DUEL_EONE_FULNEA;
                    SetupParameter(4, 2, 3, 19, 12, 3);
                    SetupFoodParameter(0, 0, 5, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_WHITE_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_BLUE_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_KIREINA_ORB);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_MAGI_ZELKIS:
                    this.fullName = Database.DUEL_MAGI_ZELKIS;
                    SetupParameter(7, 24, 5, 3, 15, 4);
                    SetupFoodParameter(5, 0, 0, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_ZELKIS_SWORD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_ZELKIS_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_RED_PENDANT);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_RED_PENDANT);
                    this.backpack = new ItemBackPack[1];
                    this.backpack[0] = new ItemBackPack(Database.POOR_SMALL_RED_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SELMOI_RO:
                    this.fullName = Database.DUEL_SELMOI_RO;
                    this.firstName = Database.DUEL_SELMOI_RO;
                    SetupParameter(10, 30, 10, 2, 20, 5);
                    SetupFoodParameter(5, 0, 0, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_FINE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_PRISM_EMBLEM);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PRISM_EMBLEM);
                    this.backpack = new ItemBackPack[2];
                    this.backpack[0] = new ItemBackPack(Database.COMMON_FROZEN_BALL);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_KARTIN_MAI:
                    this.fullName = Database.DUEL_KARTIN_MAI;
                    SetupParameter(13, 1, 27, 35, 23, 6);
                    SetupFoodParameter(0, 0, 5, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_KASHI_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_COTTON_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_JEDA_ARUS:
                    this.fullName = Database.DUEL_JEDA_ARUS;
                    SetupParameter(16, 48, 20, 7, 35, 8);
                    SetupFoodParameter(5, 0, 0, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_AERO_BLADE);
                    this.SubWeapon = new ItemBackPack(Database.RARE_AERO_BLADE);
                    this.MainArmor = new ItemBackPack(Database.RARE_SUN_BRAVE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SINTYUU_RING_AKAHYOU);
                    this.backpack = new ItemBackPack[2];
                    this.backpack[0] = new ItemBackPack(Database.RARE_PURE_WATER);
                    this.backpack[1] = new ItemBackPack(Database.RARE_PURE_GREEN_WATER);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SINIKIA_VEILHANZ:
                    this.fullName = Database.DUEL_SINIKIA_VEILHANZ;
                    SetupParameter(20, 2, 30, 63, 45, 14);
                    SetupFoodParameter(0, 0, 5, 5, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_DEVIL_EYE_ROD);
                    this.MainArmor = new ItemBackPack(Database.RARE_MAGICIANS_MANTLE);
                    this.Accessory = new ItemBackPack(Database.COMMON_PURPLE_AMULET);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PURPLE_AMULET);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_ADEL_BRIGANDY:
                    this.fullName = Database.DUEL_ADEL_BRIGANDY;
                    SetupParameter(23, 80, 43, 5, 50, 12);
                    SetupFoodParameter(0, 30, 0, 20, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_ICE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.RARE_SUN_BRAVE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_LENE_COLTOS:
                    this.fullName = Database.DUEL_LENE_COLTOS;
                    SetupParameter(26, 5, 55, 100, 73, 15);
                    SetupFoodParameter(0, 0, 30, 20, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHTNING);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SMART_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_GREEN_CHARM);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_CHARM);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SCOTY_ZALGE:
                    this.fullName = Database.DUEL_SCOTY_ZALGE;
                    SetupParameter(29, 90, 130, 2, 80, 3);
                    SetupFoodParameter(20, 0, 0, 30, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_ZALGE_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_ZALGE_CLAW);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SERPENT_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SEAL_OF_DEATH);
                    this.Accessory2 = new ItemBackPack(Database.RARE_EMBLEM_BLUESTAR);
                    this.backpack[0] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.backpack[2] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_PERMA_WARAMY:
                    this.fullName = Database.DUEL_PERMA_WARAMY;
                    SetupParameter(32, 19, 65, 120, 160, 25);
                    SetupFoodParameter(0, 0, 20, 30, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BURNING_CLAYMORE);
                    this.MainArmor = new ItemBackPack(Database.RARE_RED_THUNDER_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_RED_KOKUIN);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_RED_KOKUIN);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_KILT_JORJU:
                    this.fullName = Database.DUEL_KILT_JORJU;
                    SetupParameter(35, 120, 120, 120, 100, 40);
                    SetupFoodParameter(0, 0, 0, 50, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SMASH_BLADE);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_PURE_BRONZE_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SERPENT_ARMOR);
                    this.Accessory = new ItemBackPack(Database.EPIC_FAZIL_ORB_1);
                    this.Accessory2 = new ItemBackPack(Database.EPIC_FAZIL_ORB_2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_BILLY_RAKI:
                    this.fullName = Database.DUEL_BILLY_RAKI;
                    SetupParameter(38, 220, 105, 1, 285, 30);
                    SetupFoodParameter(60, 0, 0, 80, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_MENTALIZED_FORCE_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_MENTALIZED_FORCE_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_SCALE_BLUERAGE);
                    this.Accessory = new ItemBackPack(Database.RARE_EARTH_BREAKERS_SIGIL);
                    this.Accessory2 = new ItemBackPack(Database.RARE_LIVING_GROWTH_SEED);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_ANNA_HAMILTON:
                    this.fullName = Database.DUEL_ANNA_HAMILTON;
                    SetupParameter(41, 10, 120, 345, 314, 33);
                    SetupFoodParameter(60, 0, 0, 80, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SEKIGAN_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_FLOATING_ROBE);
                    this.Accessory = new ItemBackPack(Database.RARE_TEARS_END);
                    this.Accessory2 = new ItemBackPack(Database.RARE_LIVING_GROWTH_SEED);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_CALMANS_OHN:
                    this.fullName = Database.DUEL_CALMANS_OHN;
                    SetupParameter(44, 44, 136, 400, 425, 36);
                    SetupFoodParameter(0, 0, 60, 80, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_SHAERING_BONE_CRUSHER);
                    this.MainArmor = new ItemBackPack(Database.RARE_DRAGONSCALE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_WHITE_TIGER_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.RARE_AERIAL_VORTEX);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SUN_YU:
                    this.fullName = Database.DUEL_SUN_YU;
                    SetupParameter(47, 450, 200, 80, 529, 40);
                    SetupFoodParameter(60, 0, 60, 80, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHT_MOON_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHT_MOON_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_SCALE_BLUERAGE);
                    this.Accessory = new ItemBackPack(Database.RARE_WHITE_TIGER_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SHIHANDAI_KUROOBI);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SHUVALTZ_FLORE:
                    this.fullName = Database.DUEL_SHUVALTZ_FLORE;
                    SetupParameter(50, 10, 300, 630, 555, 110);
                    SetupFoodParameter(0, 0, 100, 150, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_SWORD);
                    this.SubWeapon = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1);
                    this.Accessory2 = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_RVEL_ZELKIS:
                    this.fullName = Database.DUEL_RVEL_ZELKIS;
                    SetupParameter(52, 800, 340, 21, 620, 60);
                    SetupFoodParameter(250, 0, 0, 500, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SWORD_OF_RVEL);
                    this.MainArmor = new ItemBackPack(Database.COMMON_ARMOR_OF_RVEL);
                    this.Accessory = new ItemBackPack(Database.COMMON_PURPLE_MEDALLION);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PURPLE_MEDALLION);
                    this.backpack = new ItemBackPack[3];
                    this.backpack[0] = new ItemBackPack(Database.COMMON_HUGE_RED_POTION);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_HUGE_BLUE_POTION);
                    this.backpack[2] = new ItemBackPack(Database.COMMON_HUGE_GREEN_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_VAN_HEHGUSTEL:
                    this.fullName = Database.DUEL_VAN_HEHGUSTEL;
                    SetupParameter(54, 15, 400, 874, 747, 65);
                    SetupFoodParameter(0, 250, 0, 500, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_INVISIBLE_STATE_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_MASTER_CROSS);
                    this.Accessory = new ItemBackPack(Database.COMMON_LIGHT_SERVANT);
                    this.Accessory2 = new ItemBackPack(Database.RARE_CORE_ESSENCE_CHANNEL);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_OHRYU_GENMA:
                    this.fullName = Database.DUEL_OHRYU_GENMA;
                    SetupParameter(56, 1096, 422, 10, 790, 70);
                    SetupFoodParameter(250, 0, 0, 500, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_SHINGETUEN_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_SHINGETUEN_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_BLOOD_BLAZER_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_SOUSUI_HIDENSYO);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_FLOATING_WHITE_BALL);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_LADA_MYSTORUS:
                    this.fullName = Database.DUEL_LADA_MYSTORUS;
                    SetupParameter(58, 1, 480, 1223, 870, 130);
                    SetupFoodParameter(0, 250, 0, 500, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_LADA_ACHROMATIC_ORB);
                    this.SubWeapon = new ItemBackPack(Database.RARE_SLIDE_THROUGH_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_OLD_TREE_SINKI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_OLD_TREE_SINKI);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SIN_OSCURETE:
                    this.fullName = Database.DUEL_SIN_OSCURETE;
                    SetupParameter(60, 1400, 520, 10, 924, 200);
                    SetupFoodParameter(250, 0, 0, 500, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_ANGEL_CONTRACT);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SEAL_OF_BALANCE);
                    this.Area = MonsterArea.Duel;
                    break;

                #endregion
                    
                #region "オル・ランディス仲間にする前"
                case Database.DUEL_OL_LANDIS:
                    this.fullName = Database.DUEL_OL_LANDIS;
                    SetupParameter(35, Database.OL_LANDIS_FIRST_STRENGTH, Database.OL_LANDIS_FIRST_AGILITY, Database.OL_LANDIS_FIRST_INTELLIGENCE, Database.OL_LANDIS_FIRST_STAMINA, Database.OL_LANDIS_FIRST_MIND);
                    SetupFoodParameter(0, 0, 0, 50, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
                    this.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "ヴェルゼ・アーティDUELその１"
                case Database.VERZE_ARTIE:
                    this.fullName = Database.VERZE_ARTIE_FULL;
                    SetupParameter(50, 30, 1205, 30, 240, 100);
                    SetupFoodParameter(0, 0, 0, 200, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_TAU_WHITE_SILVER_SWORD_0);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR_0);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING_0);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "シニキア・カールハンツ、元核習得前"
                case Database.DUEL_SINIKIA_KAHLHANZ:
                    this.fullName = Database.DUEL_SINIKIA_KAHLHANZ;
                    SetupParameter(50, 5, 320, 670, 500, 110);
                    SetupFoodParameter(0, 0, 0, 200, 0);
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_DARKMAGIC_DEVIL_EYE_REPLICA);
                    this.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "Matrix Dragon"
                case Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD:
                    this.baseStrength = 68590;
                    this.baseAgility = 1;//4241;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 77610;
                    this.baseStamina = 40650;
                    this.baseMind = 2150;
                    this.baseLife = 34359122;
                    this.experience = 0;//15000000;
                    this.level = 251;
                    this.gold = 15000000;
                    this.Area = MonsterArea.TruthBoss1;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_TINKOU_DEEPSEA:
                    this.baseStrength = 35510;
                    this.baseAgility = 100;//2566;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 91210;
                    this.baseStamina = 61120;
                    this.baseMind = 2150;
                    this.baseLife = 41226289;
                    this.experience = 0;//15000000;
                    this.level = 252;
                    this.gold = 15000000;
                    this.Area = MonsterArea.TruthBoss2;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_DESOLATOR_AZOLD:
                    this.baseStrength = 99930;
                    this.baseAgility = 500;//4543;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 11250;
                    this.baseStamina = 44510;
                    this.baseMind = 3190;
                    this.experience = 0;//15000000;
                    this.baseLife = 49938705;
                    this.baseInstantPoint = 100000;
                    this.level = 253;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss3;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_IDEA_CAGE_ZEED:
                    this.baseStrength = 21260;
                    this.baseAgility = 1000; // 会話専用のため、スピードを減らす。
                    this.baseIntelligence = 92370;
                    this.baseStamina = 81100;
                    this.baseMind = 3950;
                    this.experience = 0;
                    this.baseLife = 53910687;
                    this.baseInstantPoint = 100000;
                    this.level = 254;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss4;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA:
                    this.baseStrength = 78772;
                    this.baseAgility = 1000; // 会話専用のため、スピードを減らす。
                    this.baseIntelligence = 56910;
                    this.baseStamina = 92500;
                    this.baseMind = 5850;
                    this.experience = 0;
                    this.baseLife = 68119820;
                    this.baseInstantPoint = 100000;
                    this.level = 255;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss5;
                    this.UseStackCommand = true;
                    break;
                #endregion

                #region "ダミー素振り君"
                case Database.DUEL_DUMMY_SUBURI:
                    this.baseStrength = 100;
                    this.baseAgility = 111;
                    this.baseIntelligence = 1000;
                    this.baseStamina = 9999;
                    this.baseMind = 2;
                    this.experience = 0;
                    this.baseLife = 9990009;
                    this.baseMana = 999999;
                    this.ResistFire = 0;
                    this.Rare = RareString.Black;
                    this.level = 1;
                    this.gold = 0;
                    this.Area = MonsterArea.Area31;
                    break;
                #endregion
                default:
                    break;
            }

            if (this.baseLife == 0)
            {
                this.baseLife = (this.level - 1) * 20;
            }
            if (this.baseMana == 0)
            {
                this.baseMana = 80 + (this.level - 1) * 15;
            }

            // after
            //float powerValue = 1.0f;
            //if (GroundOne.Difficulty == 1) { powerValue = 0.75f; }
            //else if (GroundOne.Difficulty == 2) { powerValue = 1.00f; }
            //else { powerValue = 1.25f; }
            //this.baseStrength = ((int)((float)this.baseStrength * powerValue)); if (this.baseStrength <= 1) this.baseStrength = 1;
            //this.baseAgility = ((int)((float)this.baseAgility * powerValue)); if (this.baseAgility <= 1) this.baseAgility = 1;
            //this.baseIntelligence = ((int)((float)this.baseIntelligence * powerValue)); if (this.baseIntelligence <= 1) this.baseIntelligence = 1;
            //this.baseStamina = ((int)((float)this.baseStamina * powerValue)); if (this.baseStamina <= 1) this.baseStamina = 1;
            //this.baseMind = ((int)((float)this.baseMind * powerValue)); if (this.baseMind <= 1) this.baseMind = 1;
            //this.baseLife = ((int)((float)this.baseLife * powerValue)); if (this.baseLife <= 1) this.baseLife = 1;
            //this.baseMana = ((int)((float)this.baseMana * powerValue)); if (this.baseMana <= 1) this.baseMana = 1;

            // c 後編編集
            // 後編からは敵もDUELで装備を持つようになります。
            if (this.MainWeapon == null)
            {
                this.MainWeapon = new ItemBackPack("");
            }
            if (this.SubWeapon == null)
            {
                this.SubWeapon = new ItemBackPack(""); // 後編追加
            }
            if (this.MainArmor == null)
            {
                this.MainArmor = new ItemBackPack("");
            }
            if (this.Accessory == null)
            {
                this.Accessory = new ItemBackPack("");
            }
            if (this.Accessory2 == null)
            {
                this.Accessory2 = new ItemBackPack(""); // 後編追加
            }
            // c 後編編集

            this.currentLife = this.MaxLife; // c 後編編集
            this.currentMana = this.MaxMana; // c 後編編集
        }

        private void SetupParameterMonster(int lvl, int strength, int agility, int intelligence, int stamina, int mind, int exp, int gold, int baselife = 0, int basemana = 0)
        {
            this.baseStrength = strength;
            this.baseAgility = agility;
            this.baseIntelligence = intelligence;
            this.baseStamina = stamina;
            this.baseMind = mind;
            this.level = lvl;
            this.baseLife = baselife;
            this.baseMana = basemana;
            this.experience = exp;
            this.gold = gold;
        }

        private void SetupParameter(int lvl, int strength, int agility, int intelligence, int stamina, int mind)
        {
            this.baseStrength = strength;
            this.baseAgility = agility;
            this.baseIntelligence = intelligence;
            this.baseStamina = stamina;
            this.baseMind = mind;
            this.level = 0;
            for (int ii = 0; ii < lvl; ii++)
            {
                this.baseLife += this.LevelUpLifeTruth;
                this.baseMana += this.LevelUpManaTruth;
                this.level++;
            }
        }
        private void SetupFoodParameter(int strength, int agility, int intelligence, int stamina, int mind)
        {
            this.BuffStrength_Food = strength;
            this.BuffAgility_Food = agility;
            this.BuffIntelligence_Food = intelligence;
            this.BuffStamina_Food = stamina;
            this.BuffMind_Food = mind;
        }

        public new void CleanUpEffectForBoss()
        {
            base.CleanUpEffectForBoss();
        }

        public void ChoiceTimeSequenceBuff(int number, TruthImage[] list, int currentTurn)
        {
            this.TimeCumulative++;
            double max = Math.Max(Math.Max(Math.Max(Math.Max(list[0].Count, list[1].Count), list[2].Count), list[3].Count), list[4].Count);
            // １つも無い場合、つまり一番初めは２からスタート
            if (max <= 0)
            {
                list[number].Count = 2;
            }
            // ２つ目以降
            else
            {
                int choice = 0;
                // 万が一最大より低い場合はそのまま埋め合わせる。
                if (this.TimeCumulative < max)
                {
                    choice = (int)max;
                }
                // 最大値と同じだった場合は、１大きい状態でセット
                else if (this.TimeCumulative == max)
                {
                    choice = (int)max + 1;
                }
                // 最大値より大きい場合、（普段はココ）
                else // this.TimeCumulative > max
                {
                    // 最大値＋１と同じなのであれば、そのままセット（最大値と同じだった場合は、１大きい状態でセットと同じ）
                    if (this.TimeCumulative <= (int)max + 1)
                    {
                        choice = this.TimeCumulative;
                    }
                    // でなければ、最大値より２以上大きい事となる。飛び飛び数値にせず最大より１大きい状態としてセットしたい
                    else
                    {
                        choice = (int)max + 1;
                    }
                }

                // １０ターン毎に最大値歯車の稼働数を上げたいため、最大値を大きくする
                if ((currentTurn >= 10 && choice == 3) ||
                    (currentTurn >= 20 && choice == 4) ||
                    (currentTurn >= 30 && choice == 5) ||
                    (currentTurn >= 40 && choice == 6))
                {
                    choice++;
                }

                list[number].Count = choice;
            }
        }

        private void SetupActionWisely(MainCharacter player, MainCharacter target, string commandString)
        {
            PlayerAction pa = TruthActionCommand.CheckPlayerActionFromString(commandString);
            if (player.CurrentBlackContract <= 0)
            {
                if (pa == PlayerAction.UseSkill && player.CurrentSkillPoint < TruthActionCommand.Cost(commandString, player))
                {
                    if (player.CurrentGaleWind <= 0 && player.CurrentMana >= Database.GALE_WIND_COST)
                    {
                        target = player;
                        pa = PlayerAction.UseSpell;
                        commandString = Database.GALE_WIND;
                    }
                    else
                    {
                        target = player;
                        pa = PlayerAction.UseSkill;
                        commandString = Database.INNER_INSPIRATION;
                    }
                }
                else if (pa == PlayerAction.UseSpell && player.CurrentMana < TruthActionCommand.Cost(commandString, player))
                {
                    if (player.CurrentMana >= Database.BLACK_CONTRACT_COST)
                    {
                        target = player;
                        pa = PlayerAction.UseSpell;
                        commandString = Database.BLACK_CONTRACT;
                    }
                    else
                    {
                        target = player;
                        pa = PlayerAction.Defense;
                        commandString = Database.DEFENSE_EN;
                    }
                }
            }

            SetupActionCommand(player, target, pa, commandString);
        }

        private void SetupActionCommand(MainCharacter player, MainCharacter target, PlayerAction pa, string commandString)
        {
            player.PA = pa;
            player.Target = target;
            if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.UseSpell)
            {
                player.CurrentSpellName = commandString;
            }
            else if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.UseSkill)
            {
                player.CurrentSkillName = commandString;
            }
            else if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.Archetype)
            {
                player.CurrentArchetypeName = commandString;
            }
            else
            {
                player.CurrentUsingItem = commandString;
            }
            player.ActionLabel.text = TruthActionCommand.ConvertToJapanese(commandString);

            if (pa == PlayerAction.SpecialSkill)
            {
                player.MainObjectButton.image.sprite = Resources.Load<Sprite>(Database.ATTACK_EN);
            }
            else
            {
                player.MainObjectButton.image.sprite = Resources.Load<Sprite>(commandString);
            }
        }
    }
}

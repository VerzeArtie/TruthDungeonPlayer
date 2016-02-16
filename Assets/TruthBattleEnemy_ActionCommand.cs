﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        #region "スペル"
        #region "聖"

        /// <summary>
        /// ホーリー・ショックーのメソッド
        /// </summary>
        private void PlayerSpellHolyShock(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.HolyShockValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "HolyShock", 23, TruthActionCommand.MagicType.Light, false, CriticalType.Random);
        }
        #endregion
        #region "闇"
        /// <summary>
        /// ダーク・ブラストのメソッド
        /// </summary>
        private void PlayerSpellDarkBlast(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.DarkBlastValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "DarkBlast", 27, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random);
        }

        /// <summary>
        /// デヴォーリング・プラグーのメソッド
        /// </summary>
        /// <param name="player"></param>
        /// <param name="mainCharacter"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void PlayerSpellDevouringPlague(MainCharacter player, MainCharacter target, int interval, int magnification)
        {
            double damage = PrimaryLogic.DevouringPlagueValue(player, GroundOne.DuelMode);
            if (AbstractMagicDamage(player, target, interval, ref damage, magnification, "DevouringPlague", 29, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random))
            {
                if (player.CurrentNourishSense > 0)
                {
                    damage = damage * 1.3f;
                }
                damage = GainIsZero(damage, player);
                player.CurrentLife += (int)damage;
                UpdateLife(player, damage, true, true, 0, false);
            }
        }
        #endregion
        #region "火"
        /// <summary>
        /// ファイア・ボールのメソッド
        /// </summary>
        private void PlayerSpellFireBall(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FireBallValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FireBall", 10, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// フレイム・ストライクのメソッド
        /// </summary>
        private void PlayerSpellFlameStrike(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FlameStrikeValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FlameStrike", 11, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// ヴォルカニック・ウェイヴのメソッド
        /// </summary>
        private void PlayerSpellVolcanicWave(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.VolcanicWaveValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "VolcanicWave", 12, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }
        /// <summary>
        /// ラヴァ・アニヒレーションのメソッド
        /// </summary>
        private void PlayerSpellLavaAnnihilation(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(10028));
            GroundOne.PlaySoundEffect("LavaAnnihilation");
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.LavaAnnihilationValue(player, GroundOne.DuelMode), 0.0f, String.Empty, 0, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
            }
        }

        #endregion
        #region "水"
        /// <summary>
        /// アイス・ニードルのメソッド
        /// </summary>
        private void PlayerSpellIceNeedle(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.IceNeedleValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "IceNeedle", 30, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
        }

        /// <summary>
        /// フローズン・ランスのメソッド
        /// </summary>
        private void PlayerSpellFrozenLance(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FrozenLanceValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FrozenLance", 31, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
        }
        #endregion
        #region "理"
        /// <summary>
        /// ワード・オブ・パワーのメソッド
        /// </summary>
        private void PlayerSpellWordOfPower(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.WordOfPowerValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "WordOfPower", 33, TruthActionCommand.MagicType.Force, true, CriticalType.Random);
        }
        #endregion
        #region "空"
        #endregion
        #region "[聖　闇]"
        #endregion
        #region "[聖　火]"
        #endregion
        #region "[聖　水]"
        #endregion
        #region "[聖　理]"
        #endregion
        #region "[聖　空]"
        #endregion
        #region "[闇　火]"
        #endregion
        #region "[闇　水]"
        #endregion
        #region "[闇　理]"
        #endregion
        #region "[闇　空]"
        #endregion
        #region "[火　水]"
        #endregion
        #region "[火　理]"
        #endregion
        #region "[火　空]"
        #endregion
        #region "[水　理]"
        #endregion
        #region "[水　空]"
        #endregion
        #region "[理　空]"
        #endregion

        // 敵対象







        /// <summary>
        /// ホワイト・アウトのメソッド
        /// </summary>
        private void PlayerSpellWhiteOut(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.WhiteOutValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "WhiteOut", 34, TruthActionCommand.MagicType.Will, false, CriticalType.Random);
        }

        /// <summary>
        /// フラッシュ・ブレイズのメソッド
        /// </summary>
        private void PlayerSpellFlashBlaze(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FlashBlazeValue(player, GroundOne.DuelMode);
            if (AbstractMagicDamage(player, target, interval, ref damage, magnification, "HolyShock", 120, TruthActionCommand.MagicType.Light, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "FlashBlaze_Buff");
            }
        }

        /// <summary>
        /// セレスティアル・ノヴァのメソッド
        /// </summary>
        private void PlayerSpellCelestialNova(MainCharacter player, MainCharacter target)
        {
            if (DetectOpponentParty(player, target))
            {
                AbstractMagicDamage(player, target, 0, PrimaryLogic.CelestialNovaValue_A(player, GroundOne.DuelMode), 0, "CelestialNova", 26, TruthActionCommand.MagicType.Light, false, CriticalType.Random);
            }
            else
            {
                double lifeGain = PrimaryLogic.CelestialNovaValue_B(player, GroundOne.DuelMode);
                if (player.CurrentNourishSense > 0)
                {
                    lifeGain = lifeGain * 1.3f;
                }
                PlayerAbstractLifeGain(player, target, 0, lifeGain, 0, Database.SOUND_CELESTIAL_NOVA, 25);
            }
        }

        /// <summary>
        /// ライト・デトネイターのメソッド
        /// </summary>
        private void PlayerSpellLightDetonator(MainCharacter player, MainCharacter target, int p, int p_2)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.LightDetonatorValue(player, GroundOne.DuelMode), 0, "FlameStrike", 132, TruthActionCommand.MagicType.Light_Fire, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// ゼータ・エクスプロージョンのメソッド
        /// </summary>
        private void PlayerSpellZetaExplosion(MainCharacter player, MainCharacter target)
        {
            AbstractMagicDamage(player, target, 0, PrimaryLogic.ZetaExplosionValue(player, GroundOne.DuelMode), 0, Database.SOUND_ZETA_EXPLOSION, 139, TruthActionCommand.MagicType.Fire_Ice, true, CriticalType.Random);
        }

        /// <summary>
        /// チル・バーンのメソッド
        /// </summary>
        private void PlayerSpellChillBurn(MainCharacter player, MainCharacter target)
        {
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.ChillBurnValue(player, GroundOne.DuelMode), 0, "IceNeedle", 138, TruthActionCommand.MagicType.Fire_Ice, false, CriticalType.Random))
            {
                NowFrozen(player, target, 2);
            }
        }

        /// <summary>
        /// ピアッシング・フレイムのメソッド
        /// </summary>
        private void PlayerSpellPiercingFlame(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(182));
            AbstractMagicDamage(player, target, 0, PrimaryLogic.PiercingFlameValue(player, GroundOne.DuelMode), 0, Database.SOUND_PIERCING_FLAME, 138, TruthActionCommand.MagicType.Fire_Force, true, CriticalType.Random);
        }

        /// <summary>
        /// ブルー・バレットのメソッド
        /// </summary>
        private void PlayerSpellBlueBullet(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            UpdateBattleText(player.GetCharacterSentence(151));
            for (int ii = 0; ii < 3; ii++)
            {
                AbstractMagicDamage(player, target, interval, PrimaryLogic.BlueBulletValue(player, GroundOne.DuelMode), magnification, "FrozenLance", 120, TruthActionCommand.MagicType.Shadow_Ice, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// アセンダント・メテオのメソッド
        /// </summary>
        private void PlayerSpellAscendantMeteor(MainCharacter player, MainCharacter target, int p, int p_2)
        {
            UpdateBattleText(player.GetCharacterSentence(133));
            for (int ii = 0; ii < 10; ii++)
            {
                AbstractMagicDamage(player, target, 15, PrimaryLogic.AscendantMeteorValue(player, GroundOne.DuelMode), 0, "FireBall", 120, TruthActionCommand.MagicType.Light_Fire, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// スター・ライトニングのメソッド
        /// </summary>
        private void PlayerSpellStarLightning(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(141));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.StarLightningValue(player, GroundOne.DuelMode), 0, "FlameStrike", 120, TruthActionCommand.MagicType.Light_Will, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 1, "StarLightning");
            }
        }

        /// <summary>
        /// ブラック・ファイアのメソッド
        /// </summary>
        private void PlayerSpellBlackFire(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(143));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.BlackFireValue(player, GroundOne.DuelMode), 0, "DarkBlast", 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "BlackFire");
            }
        }

        /// <summary>
        /// ワード・オブ・マリスのメソッド
        /// </summary>
        private void PlayerSpellWordOfMalice(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(142));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.WordOfMaliceValue(player, GroundOne.DuelMode), 0, "WhiteOut", 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "WordOfMalice");
            }
        }

        /// <summary>
        /// エンレイジ・ブラストのメソッド
        /// </summary>
        private void PlayerSpellEnrageBlast(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(144));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                if (AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.EnrageBlastValue(player, GroundOne.DuelMode), 0, "FlameStrike", 120, TruthActionCommand.MagicType.Fire_Force, false, CriticalType.Random))
                {
                    PlayerBuffAbstract(player, group[ii], 999, "EnrageBlast");
                }
            }
        }

        /// <summary>
        /// イモレイトのメソッド
        /// </summary>
        private void PlayerSpellImmolate(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(145));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.ImmolateValue(player, GroundOne.DuelMode), 0, "FireBall", 120, TruthActionCommand.MagicType.Fire_Will, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "Immolate");
            }
        }

        /// <summary>
        /// ヴァニッシュ・ウェイヴのメソッド
        /// </summary>
        private void PlayerSpellVanishWave(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(146));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.VanishWaveValue(player, GroundOne.DuelMode), 0, "WhiteOut", 120, TruthActionCommand.MagicType.Ice_Will, false, CriticalType.Random))
            {
                //PlayerBuffAbstract(player, target, 999, "VanishWave");
                NowSilence(player, target, 3);
            }
        }

        /// <summary>
        /// アビス・アイのメソッド
        /// </summary>
        private void PlayerSpellAbyssEye(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(180));
            int random = AP.Math.RandomInteger(100);
            if (target.GetType() == typeof(TruthEnemyCharacter))
            {
                TruthEnemyCharacter tTarget = (TruthEnemyCharacter)target;
                if ((random <= 70) &&
                    (tTarget.Area != TruthEnemyCharacter.MonsterArea.Duel) &&
                    ((tTarget.Rare == TruthEnemyCharacter.RareString.Black) ||
                     (tTarget.Rare == TruthEnemyCharacter.RareString.Blue) ||
                     (tTarget.Rare == TruthEnemyCharacter.RareString.Red)
                    )
                   )
                {
                    GroundOne.PlaySoundEffect(Database.SOUND_ABYSS_EYE);
                    PlayerDeath(player, target);
                }
                else
                {
                    AbstractMagicDamage(player, target, 0, PrimaryLogic.AbyssEyeValue(player, GroundOne.DuelMode), 0, Database.SOUND_ABYSS_EYE, 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random);
                }
            }
            else
            {
                AbstractMagicDamage(player, target, 0, PrimaryLogic.AbyssEyeValue(player, GroundOne.DuelMode), 0, Database.SOUND_ABYSS_EYE, 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// ドゥーム・ブレイドのメソッド
        /// </summary>
        private void PlayerSpellDoomBlade(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(181));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.DoomBladeValue(player, GroundOne.DuelMode), 0, Database.SOUND_DOOM_BLADE, 120, TruthActionCommand.MagicType.Shadow_Will, false, CriticalType.Random))
            {
                double damage = PrimaryLogic.DoomBlade_A_Value(player, GroundOne.DuelMode);
                target.CurrentMana -= (int)damage;
                UpdateMana(target, damage, false, true, 0);
            }
        }

        /// <summary>
        /// マインド・キリングのメソッド
        /// </summary>
        private void PlayerSkillMindKilling(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(200));
            double effectValue = PrimaryLogic.MindKillingValue(player, GroundOne.DuelMode);
            target.CurrentMana -= (int)effectValue;
            UpdateMana(target, effectValue, false, true, 0);
        }


        /// <summary>
        /// ジェネシス
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        private void PlayerSpellGenesis(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_GENESIS);
            UpdateBattleText(player.GetCharacterSentence(108));
            ExecBeforeAttackPhase(player, false);
        }

        private void ExecBeforeAttackPhase(MainCharacter player, bool skipStanceDouble)
        {
            if (player.BeforePA == MainCharacter.PlayerAction.None)
            {
                PlayerNormalAttack(player, player.Target, 0.0f, false, false);
                return;
            }

            MainCharacter.PlayerAction shadowPA = player.PA;
            string shadowItem = player.CurrentUsingItem;
            string shadowSpell = player.CurrentSpellName;
            string shadowSkill = player.CurrentSkillName;
            string shadowArche = player.CurrentArchetypeName;
            MainCharacter shadowTarget = player.Target;
            MainCharacter shadowTarget2 = player.Target2;
            MainCharacter.PlayerAction shadowBeforePA = player.BeforePA;
            string shadowBeforeItem = player.BeforeUsingItem;
            string shadowBeforeSpell = player.BeforeSpellName;
            string shadowBeforeskill = player.BeforeSkillName;
            string shadowBeforeArche = player.BeforeArchetypeName;
            MainCharacter shadowBeforeTarget = player.BeforeTarget;
            MainCharacter shadowBeforeTarget2 = player.BeforeTarget2;

            player.PA = player.BeforePA;
            player.CurrentUsingItem = player.BeforeUsingItem;
            player.CurrentSkillName = player.BeforeSkillName;
            player.CurrentSpellName = player.BeforeSpellName;
            player.CurrentArchetypeName = player.BeforeArchetypeName;
            player.Target = player.BeforeTarget;
            player.Target2 = player.BeforeTarget2;

            PlayerAttackPhase(player, true, skipStanceDouble, false);

            player.PA = shadowPA;
            player.CurrentUsingItem = shadowItem;
            player.CurrentSkillName = shadowSkill;
            player.CurrentSpellName = shadowSpell;
            player.CurrentArchetypeName = shadowArche;
            player.Target = shadowTarget;
            player.Target2 = shadowTarget2;
            player.BeforePA = shadowBeforePA;
            player.BeforeUsingItem = shadowBeforeItem;
            player.BeforeSkillName = shadowBeforeskill;
            player.BeforeSpellName = shadowBeforeSpell;
            player.BeforeTarget = shadowBeforeTarget;
            player.BeforeTarget2 = shadowBeforeTarget2;
        }

        /// <summary>
        /// デーモニック・イグナイトのメソッド
        /// </summary>
        private void PlayerSpellDemonicIgnite(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(213));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.DemonicIgniteValue(player, GroundOne.DuelMode), 0, Database.SOUND_DEMONIC_IGNITE, 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random))
            {
                NowNoGainLife(target, 1);
            }
        }

        /// <summary>
        /// エンドレス・アンセムのメソッド
        /// </summary>
        private void PlayerSpellEndlessAnthem(MainCharacter player, MainCharacter target)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }
            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].BuffCountUp();
            }
            GroundOne.PlaySoundEffect(Database.SOUND_ENDLESS_ANTHEM);
        }

        /// <summary>
        /// ワープ・ゲートのメソッド
        /// </summary>
        private void PlayerSpellWarpGate(MainCharacter player, MainCharacter target)
        {
            // todo
            // ゲージを進める。進めた結果、行動フェーズを超えた場合、コスト無しで行動を行い、超えた分だけさらにゲージを進める。
            //GroundOne.PlaySoundEffect(Database.SOUND_WARP_GATE);
            //for (int ii = 0; ii < (int)PrimaryLogic.WarpGateValue(player); ii++)
            //{
            //    player.BattleBarPos++;
            //    if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
            //    {
            //        PlayerAttackPhase(player, true, false, false);
            //        player.BattleBarPos = 0;
            //    }
            //    pbPlayer1.Invalidate();
            //    this.Update();
            //    System.Threading.Thread.Sleep(1);
            //}
        }


        /// <summary>
        /// フレッシュ・ヒールのメソッド
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellFreshHeal(MainCharacter player, MainCharacter target)
        {
            double lifeGain = PrimaryLogic.FreshHealValue(player, GroundOne.DuelMode);
            if (player.CurrentNourishSense > 0)
            {
                lifeGain = lifeGain * 1.3f;
            }
            PlayerAbstractLifeGain(player, player, 0, lifeGain, 0, Database.SOUND_FRESH_HEAL, 9);
        }

        /// <summary>
        /// ライフ・タップのメソッド
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellLifeTap(MainCharacter player, MainCharacter target)
        {
            double lifeGain = PrimaryLogic.LifeTapValue(player, GroundOne.DuelMode);
            if (player.CurrentNourishSense > 0)
            {
                lifeGain = lifeGain * 1.3f;
            }
            PlayerAbstractLifeGain(player, target, 0, lifeGain, 0, Database.SOUND_LIFE_TAP, 9);
        }

        /// <summary>
        /// サークレッド・ヒールのメソッド
        /// </summary>
        private void PlayerSpellSacredHeal(MainCharacter player, MainCharacter target)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerAbstractLifeGain(player, group[ii], 0, PrimaryLogic.SacredHealValue(player, GroundOne.DuelMode), 0, "CelestialNova", 135);
            }
        }

        /// <summary>
        /// クリーンジングのメソッド
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellCleansing(MainCharacter player, MainCharacter target)
        {
            if (player.CurrentStunning > 0 || player.CurrentSilence > 0 || player.CurrentPoison > 0 || player.CurrentParalyze > 0 || player.CurrentTemptation > 0 || player.CurrentFrozen > 0)
            {
                UpdateBattleText(player.GetCharacterSentence(109));
                return;
            }

            if (target != ec1 || target != ec2 || target != ec3)
            {
                UpdateBattleText(player.GetCharacterSentence(77));
                target.RemovePreStunning();
                target.RemoveStun();
                target.RemoveSilence();    
                target.RemovePoison();
                target.RemoveTemptation();
                target.RemoveFrozen();
                target.RemoveParalyze();
                target.RemoveSlow();
                target.RemoveBlind();
                target.RemoveSlip();
                //target.RemoveNoResurrection(); // 復活不可は負の影響という定義には当てはまらない。
                //target.RemoveNoGainLife(); // ライフ回復不可は負の影響という定義には当てはまらない。
                GroundOne.PlaySoundEffect("Cleansing");
                UpdateBattleText(target.FirstName + "にかかっている負の影響が全て取り払われた。\r\n");
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        /// <summary>
        /// エンジェル・ブレスのメソッド
        /// </summary>
        private void PlayerSpellAngelBreath(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(177));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].CurrentPreStunning = 0;
                group[ii].DeBuff(group[ii].pbPreStunning);
                group[ii].CurrentStunning = 0;
                group[ii].DeBuff(group[ii].pbStun);
                group[ii].CurrentSilence = 0;
                group[ii].DeBuff(group[ii].pbSilence);
                group[ii].CurrentPoison = 0;
                group[ii].CurrentPoisonValue = 0;
                group[ii].DeBuff(group[ii].pbPoison);
                group[ii].CurrentTemptation = 0;
                group[ii].DeBuff(group[ii].pbTemptation);
                group[ii].CurrentFrozen = 0;
                group[ii].DeBuff(group[ii].pbFrozen);
                group[ii].CurrentParalyze = 0;
                group[ii].DeBuff(group[ii].pbParalyze);
                group[ii].CurrentSlow = 0;
                group[ii].DeBuff(group[ii].pbSlow);
                group[ii].CurrentBlind = 0;
                group[ii].DeBuff(group[ii].pbBlind);
                group[ii].CurrentSlip = 0;
                group[ii].DeBuff(group[ii].pbSlip);
                //group[ii].CurrentNoResurrection = 0; // 復活不可は負の影響という定義には当てはまらない。
                //group[ii].DeBuff(group[ii].pbNoResurrection);
                GroundOne.PlaySoundEffect("Cleansing");
                UpdateBattleText(group[ii].FirstName + "にかかっている負の影響が全て取り払われた。\r\n");
            }
        }

        /// <summary>
        /// ピュア・プリファイケーションのメソッド
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSkillPurePurification(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(78));
            player.CurrentPreStunning = 0;
            player.DeBuff(player.pbPreStunning);
            player.CurrentStunning = 0;
            player.DeBuff(player.pbStun);
            player.CurrentSilence = 0;
            player.DeBuff(player.pbSilence);
            player.CurrentPoison = 0;
            player.CurrentPoisonValue = 0;
            player.DeBuff(player.pbPoison);
            player.CurrentTemptation = 0;
            player.DeBuff(player.pbTemptation);
            player.CurrentFrozen = 0;
            player.DeBuff(player.pbFrozen);
            player.CurrentParalyze = 0;
            player.DeBuff(player.pbParalyze);
            player.CurrentSlow = 0;
            player.DeBuff(player.pbSlow);
            player.CurrentBlind = 0;
            player.DeBuff(player.pbBlind);
            player.CurrentSlip = 0;
            player.DeBuff(player.pbSlip);
            //player.CurrentNoResurrection = 0; // 復活不可は負の影響という定義には当てはまらない。
            //player.DeBuff(player.pbNoResurrection);
            //player.CurrentNoGainLife = 0; // 復活不可は負の影響という定義には当てはまらない。
            //player.DeBuff(player.pbNoGainLife);
            GroundOne.PlaySoundEffect("Cleansing");
            UpdateBattleText(player.FirstName + "にかかっている負の影響が全て取り払われた。\r\n");
        }

        /// <summary>
        /// ディスペル・マジックのメソッド
        /// </summary>
        private void PlayerSpellDispelMagic(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(48), 1000);

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + target.FirstName + "は無効化を無効にするオーラによって護られている！\r\n");
                return;
            }

            target.RemoveBuffSpell();
            GroundOne.PlaySoundEffect("DispelMagic");
            UpdateBattleText(target.FirstName + "の能力ＵＰ型効果を全て打ち消した！\r\n");
        }

        private void RemoveBuffAll(MainCharacter target)
        {
            RemoveBuffSpell(target);
            RemoveBuffSkill(target);
            RemoveEffect(target);
        }

        private void RemoveEffect(MainCharacter target)
        {
            target.RemoveDebuffEffect();
            target.RemoveBuffEffect();

            target.RemoveBuffParam();
            target.RemoveDebuffParam();

            target.RemoveStrengthUp();
            target.RemoveAgilityUp();
            target.RemoveIntelligenceUp();
            target.RemoveStaminaUp();
            target.RemoveMindUp();

            target.RemoveResistLightUp();
            target.RemoveResistShadowUp();
            target.RemoveResistFireUp();
            target.RemoveResistIceUp();
            target.RemoveResistForceUp();
            target.RemoveResistWillUp();
        }

        private void RemoveBuffSpell(MainCharacter target)
        {
            target.RemoveBuffSpell();
            RemoveTemporaryUpSpell(target);
            RemoveBuffDownSpell(target);
        }

        private void RemoveBuffSkill(MainCharacter target)
        {
            RemoveBuffUpSkill(target);
            RemoveTemporaryUpSkill(target);
            RemoveBuffDownSkill(target);
        }

        private void RemoveTemporaryUpSpell(MainCharacter target)
        {
            // 基本スペル
            target.RemoveGlory();
            target.RemoveBlackContract();
            target.RemoveImmortalRave();
            target.RemoveMirrorImage();
            target.RemoveGaleWind();
            target.RemoveWordOfFortune();
            target.RemoveAetherDrive();
            target.RemoveDeflection();
            target.RemoveOneImmunity();
            target.RemoveTimeStop();
            // 複合スペル
            //target.RemoveTranscendentWish();
            target.RemoveHymnContract();
            target.RemoveEndlessAnthem();
            target.RemoveSinFortune();
            //target.RemoveEclipseEnd();
        }
        private void RemoveBuffDownSpell(MainCharacter target)
        {
            target.RemoveDebuffSpell();
        }
        private void RemoveBuffUpSkill(MainCharacter target)
        {
            target.RemoveBuffSkill();
        }
        private void RemoveTemporaryUpSkill(MainCharacter target)
        {
            // 基本スキル
            target.RemoveStanceOfFlow();
            target.RemoveHighEmotionality();
            // 複合スキル
            target.RemoveStanceOfDouble();
            target.RemoveSwiftStep();
            target.RemoveVigorSense();
            target.RemoveSmoothingMove();
            target.RemoveFutureVision();
            target.RemoveOneAuthority();
        }
        private void RemoveBuffDownSkill(MainCharacter target)
        {
            target.RemoveDebuffSkill();
        }

        private void PlayerSpellTranquility(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(84));

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + target.FirstName + "は無効化を無効にするオーラによって護られている！\r\n");
                return;
            }

            target.RemoveGlory();
            // GaleWindとFortuneがDispel不可能なのは実践上強すぎたため、Dispel対象とする。
            target.RemoveGaleWind();
            target.RemoveWordOfFortune();
            target.RemoveBlackContract();
            target.RemoveHymnContract();
            target.RemoveImmortalRave();
            //target.RemoveAbsoluteZero(); // Tranquilityは負の影響効果を消し去るスペルではない。
            target.RemoveAetherDrive();
            target.RemoveOneImmunity();
            target.RemoveHighEmotionality();
            target.RemoveStanceOfFlow();
            // 複合スキル
            target.RemoveStanceOfDouble();
            target.RemoveSwiftStep();
            target.RemoveVigorSense();
            target.RemoveSmoothingMove();
            target.RemoveFutureVision();
            target.RemoveOneAuthority();

            GroundOne.PlaySoundEffect("Tranquility");
            UpdateBattleText(target.FirstName + "の一定時間付与効果を全て打ち消した！\r\n");
        }

        /// <summary>
        /// ワード・オブ・アティチュードのメソッド
        /// </summary>
        private void PlayerSpellWordOfAttitude(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(147));
            target.CurrentInstantPoint = target.MaxInstantPoint;
            UpdateInstantPoint(target);
        }


        // 自分対象の魔法
        // グローリーのメソッド
        private void PlayerSpellGlory(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Glory");
            PlayerBuffAbstract(player, player, 4, "Glory");
        }

        // ブラック・コントラクトのメソッド
        private void PlayerSpellBlackContract(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("BlackContract");
            PlayerBuffAbstract(player, target, 4, "BlackContract");
        }

        // イモータル・レイブのメソッド
        private void PlayerSpellImmortalRave(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("ImmortalRave");
            PlayerBuffAbstract(player, player, 4, "ImmortalRave");
        }

        // ゲイル・ウィンドのメソッド
        private void PlayerSpellGaleWind(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("GaleWind");
            if (player.CurrentGaleWind <= 0)
            {
                PlayerBuffAbstract(player, player, 2, "GaleWind");
            }
            else
            {
                // 後編、ヴェルゼがＸ回行動を取るための仕組み。
                PlayerBuffAbstract(player, player, player.CurrentGaleWind + 1, "GaleWind");
            }
        }

        // エーテル・ドライブのメソッド
        private void PlayerSpellAetherDrive(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("AetherDrive");
            PlayerBuffAbstract(player, player, 4, "AetherDrive");
        }

        // ワン・イムーニティのメソッド
        private void PlayerSpellOneImmunity(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("OneImmunity");
            PlayerBuffAbstract(player, player, 4, "OneImmunity");
        }

        // スタンス・オブ・フローのメソッド
        private void PlayerSkillStanceOfFlow(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("StanceOfFlow");
            PlayerBuffAbstract(player, player, 3, "StanceOfFlow");
        }

        //　トルゥス・ヴィジョンのメソッド
        private void PlayerSkillTruthVision(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("TruthVision");
            PlayerBuffAbstract(player, target, 999, "TruthVision");
        }

        // プロテクションのメソッド
        private void PlayerSpellProtection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Protection");
            PlayerBuffAbstract(player, target, 999, "Protection");
        }

        /// <summary>
        /// アブソーブ・ウォーターのメソッド
        /// </summary>
        private void PlayerSpellAbsorbWater(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("AbsorbWater");
            PlayerBuffAbstract(player, target, 999, "AbsorbWater");
        }

        private void PlayerSpellSaintPower(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("SaintPower");
            PlayerBuffAbstract(player, target, 999, "SaintPower");
        }

        private void PlayerSpellShadowPact(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("ShadowPact");
            PlayerBuffAbstract(player, target, 999, "ShadowPact");
        }

        private void PlayerSpellBloodyVengeance(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("BloodyVengeance");
            PlayerBuffAbstract(player, target, 999, "BloodyVengeance");
        }

        private void PlayerSpellHeatBoost(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("HeatBoost");
            PlayerBuffAbstract(player, target, 999, "HeatBoost");
        }

        private void PlayerSpellRiseOfImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage");
            PlayerBuffAbstract(player, target, 999, "RiseOfImage");
        }

        protected void PlayerSpellPromisedKnowledge(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("PromisedKnowledge");
            PlayerBuffAbstract(player, target, 999, "PromisedKnowledge");
        }

        private void PlayerSpellFlameAura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("FlameAura");
            PlayerBuffAbstract(player, target, 999, "FlameAura");
        }

        private void PlayerSpellWordOfLife(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfLife");
            PlayerBuffAbstract(player, target, 999, "WordOfLife");
        }

        private void PlayerSpellWordOfFortune(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfFortune");
            PlayerBuffAbstract(player, target, 2, "WordOfFortune"); // １ターン継続のためには、初期値は１＋１
        }

        private void PlayerSpellMirrorImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("MirrorImage");
            PlayerBuffAbstract(player, target, 999, "MirrorImage");
        }

        private void PlayerSpellDeflection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Deflection");
            PlayerBuffAbstract(player, target, 999, "Deflection");
        }

        private void PlayerSpellSigilOfHomura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SIGIL_OF_HOMURA);
            PlayerBuffAbstract(player, target, 999, "SigilOfHomura");
        }

        /// <summary>
        /// ファンタズマル・ウィンドのメソッド
        /// </summary>
        private void PlayerSpellPhantasmalWind(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_PHANTASMAL_WIND);
            PlayerBuffAbstract(player, target, 999, "PhantasmalWind");
        }

        /// <summary>
        /// パラドックス・イメージのメソッド
        /// </summary>
        private void PlayerSpellParadoxImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_PARADOX_IMAGE);
            PlayerBuffAbstract(player, target, 999, "ParadoxImage");
        }

        private void PlayerSpellHymnContract(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_HYMN_CONTRACT);
            PlayerBuffAbstract(player, target, 4, "HymnContract");
        }

        /// <summary>
        /// サイキック・トランスのメソッド
        /// </summary>
        private void PlayerSpellPsychicTrance(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage");
            PlayerBuffAbstract(player, target, 999, "PsychicTrance");
        }

        /// <summary>
        /// ブラインド・ジャスティスのメソッド
        /// </summary>
        private void PlayerSpellBlindJustice(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage");
            PlayerBuffAbstract(player, target, 999, "BlindJustice");
        }

        /// <summary>
        /// トランッセンデント・ウィッシュのメソッド
        /// </summary>
        private void PlayerSpellTranscendentWish(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage");
            PlayerBuffAbstract(player, target, 4, "TranscendentWish"); // ３ターン継続のためには、初期値は３＋１
        }

        /// <summary>
        /// フローズン・オーラのメソッド
        /// </summary>
        private void PlayerSpellFrozenAura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("IceNeedle");
            PlayerBuffAbstract(player, target, 999, "FrozenAura");
        }

        /// <summary>
        /// スカイ・シールドのメソッド
        /// </summary>
        private void PlayerSpellSkyShield(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory");
            PlayerBuffAbstract(player, target, 999, "SkyShield");
        }

        /// <summary>
        /// スタティック・バリアのメソッド1
        /// </summary>
        private void PlayerSpellStaticBarrier(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_STATIC_BARRIER);
            PlayerBuffAbstract(player, target, 999, "StaticBarrier");
        }

        /// <summary>
        /// エヴァー・ドロップレットのメソッド
        /// </summary>
        private void PlayerSpellEverDroplet(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory");
            PlayerBuffAbstract(player, target, 999, "EverDroplet");
        }

        /// <summary>
        /// ホーリー・ブレイカーのメソッド
        /// </summary>
        private void PlayerSpellHolyBreaker(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory");
            PlayerBuffAbstract(player, target, 999, "HolyBreaker");
        }

        /// <summary>
        /// アウステリティ・マトリクスのメソッド
        /// </summary>
        private void PlayerSpellAusterityMatrix(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_AUSTERITY_MATRIX);
            target.RemoveBuffSpell();
            PlayerBuffAbstract(player, target, 999, "AusterityMatrix");
        }

        /// <summary>
        /// レッド・ドラゴン・ウィルのメソッド
        /// </summary>
        private void PlayerSpellRedDragonWill(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_RED_DRAGON_WILL);
            PlayerBuffAbstract(player, target, 999, "RedDragonWill");
        }

        /// <summary>
        /// ブルー・ドラゴン・ウィルのメソッド
        /// </summary>
        private void PlayerSpellBlueDragonWill(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_BLUE_DRAGON_WILL);
            PlayerBuffAbstract(player, target, 999, "BlueDragonWill");
        }

        /// <summary>
        /// シン・フォーチュンのメソッド
        /// </summary>
        private void PlayerSpellSinFortune(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SIN_FORTUNE);
            PlayerBuffAbstract(player, target, 999, "SinFortune");
        }

        /// <summary>
        /// ノリッシュ・センスのメソッド
        /// </summary>
        private void PlayerSkillNourishSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(199));
            GroundOne.PlaySoundEffect(Database.SOUND_NOURISH_SENSE);
            PlayerBuffAbstract(player, target, 999, "NourishSense");
        }

        /// <summary>
        /// エターナル・プリゼンスのメソッド
        /// </summary>
        private void PlayerSpellEternalPresence(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("EternalPresence");
            PlayerBuffAbstract(player, target, 999, "EternalPresence");
        }

        /// <summary>
        /// ダムネーションのメソッド
        /// </summary>
        private void PlayerSpellDamnation(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Damnation");
            PlayerBuffAbstract(player, target, 999, "Damnation");
        }

        /// <summary>
        /// アブソリュート・ゼロのメソッド
        /// </summary>
        private void PlayerSpellAbsoluteZero(MainCharacter player, MainCharacter target)
        {
            int effectTurn = 4;
            if (player.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE) { effectTurn = 1; } // todo FirstNameか？
            GroundOne.PlaySoundEffect("AbsoluteZero");
            if (player.Target.CurrentAbsoluteZero <= 0) // 強力無比な魔法のため、継続ターンの連続更新は出来なくしている。
            {
                PlayerBuffAbstract(player, target, effectTurn, "AbsoluteZero");
            }
        }

        /// <summary>
        /// スタンス・オブ・ダブルのメソッド
        /// </summary>
        private void PlayerSkillStanceOfDouble(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(215));
            GroundOne.PlaySoundEffect(Database.SOUND_STANCE_OF_DOUBLE);
            PlayerBuffAbstract(player, target, 1, "StanceOfDouble");
        }

        /// <summary>
        /// タイムストップのメソッド
        /// </summary>
        private void PlayerSpellTimeStop(MainCharacter player, MainCharacter target, bool immidiateEnd = false)
        {
            GroundOne.PlaySoundEffect("TimeStop");
            if (player.CurrentTimeStop <= 0) // 強力無比な魔法のため、継続ターンの連続更新は出来なくしている。
            {
                if (immidiateEnd)
                {
                    player.CurrentTimeStopImmediate = true;
                }
                PlayerBuffAbstract(player, player, 1, "TimeStop"); // １ターン継続
            }
        }

        /// <summary>
        /// ダーケン・フィールドのメソッド
        /// </summary>
        private void PlayerSpellDarkenField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(149));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("DarkBlast");
                PlayerBuffAbstract(player, group[ii], 999, "DarkenField");
            }
        }

        /// <summary>
        /// ブレイジング・フィールドのメソッド
        /// </summary>
        private void PlayerSpellBlazingField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(178));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("FlameStrike");
                PlayerBuffAbstract(player, group[ii], 999, "BlazingField");
            }
        }

        /// <summary>
        /// イグザルティッド・フィールド
        /// </summary>
        private void PlayerSpellExaltedField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(174));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("Protection");
                PlayerBuffAbstract(player, group[ii], 999, "ExaltedField");
            }
        }

        /// <summary>
        /// ワン・オーソリティのメソッド
        /// </summary>
        private void PlayerSkillOneAuthority(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(202));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("SaintPower");
                PlayerBuffAbstract(player, group[ii], 4, "ONEAuthority");
            }
        }

        /// <summary>
        /// ヴォルテックス・フィールド
        /// </summary>
        private void PlayerSpellVortexField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(185));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect(Database.SOUND_VORTEX_FIELD);
                NowSlow(player, group[ii], 4);
            }
        }

        /// <summary>
        /// ライジング・オーラのメソッド
        /// </summary>
        private void PlayerSkillRisingAura(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(175));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("HighEmotionality");
                PlayerBuffAbstract(player, group[ii], 999, "RisingAura");
            }
        }

        /// <summary>
        /// アセンション・オーラのメソッド
        /// </summary>
        private void PlayerSkillAscensionAura(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(176));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("HighEmotionality");
                PlayerBuffAbstract(player, group[ii], 999, "AscensionAura");
            }
        }

        /// <summary>
        /// エクリプス・エンドのメソッド
        /// </summary>
        private void PlayerSpellEclipseEnd(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(210));
            GroundOne.PlaySoundEffect(Database.SOUND_ECLIPSE_END);
            List<MainCharacter> group = new List<MainCharacter>();
            if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
            if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
            if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
            if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
            if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }

            for (int ii = 0; ii < group.Count; ii++)
            {
                RemoveBuffAll(group[ii]);
                PlayerBuffAbstract(player, group[ii], 2, "EclipseEnd");
            }
        }

        private void PlayerSkillFatalBlow(MainCharacter player, MainCharacter target)
        {
            bool notOneKill = false;
            if (target.GetType() == typeof(TruthEnemyCharacter))
            {
                if (((TruthEnemyCharacter)target).Area == TruthEnemyCharacter.MonsterArea.Duel) { notOneKill = true; }
                if (((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Purple) { notOneKill = true; }
                if (((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Gold) { notOneKill = true; }
            }

            if (notOneKill)
            {
                // 100%クリティカルヒット
                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 0, Database.SOUND_KINETIC_SMASH, 173, true, CriticalType.Absolute);
            }
            else
            {
                int rand = AP.Math.RandomInteger(3);
                if (rand <= 0)
                {
                    PlayerDeath(player, target);
                }
                else
                {
                    // 100%クリティカルヒット
                    PlayerNormalAttack(player, target, 0, 0, false, false, 0, 0, Database.SOUND_KINETIC_SMASH, 173, true, CriticalType.Absolute);
                }
            }
        }

        /// <summary>
        /// セブンス・マジックのメソッド
        /// </summary>
        private void PlayerSpellSeventhMagic(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("TruthVision");
            PlayerBuffAbstract(player, target, 999, "SeventhMagic");
        }

        /// <summary>
        /// スタンス・オブ・ミスティックのメソッド
        /// </summary>
        private void PlayerSkillStanceOfMystic(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("GaleWind");
            PlayerBuffAbstract(player, target, 999, "StanceOfMystic");
        }

        bool CannotResurrect = false;
        /// <summary>
        /// リザレクションのメソッド
        /// </summary>
        private void PlayerSpellResurrection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Resurrection");
            UpdateBattleText(player.GetCharacterSentence(52), 3000);

            ResurrectionLogic(player, target, Database.RESURRECTION);

        }

        /// <summary>
        /// デス・ディナイのメソッド
        /// </summary>
        private void PlayerSpellDeathDeny(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_DEATH_DENY);
            UpdateBattleText(player.GetCharacterSentence(214), 3000);

            ResurrectionLogic(player, target, Database.DEATH_DENY);
            NowNoResurrection(player, target, 999);
        }

        private void ResurrectionLogic(MainCharacter player, MainCharacter target, string command)
        {
            if (target != null)
            {
                if (DetectOpponentParty(player, target))
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
                else
                {
                    if (target == player)
                    {
                        UpdateBattleText(player.GetCharacterSentence(55));
                    }
                    else if (!target.Dead)
                    {
                        UpdateBattleText(player.GetCharacterSentence(54));
                    }
                    else if (this.CannotResurrect)
                    {
                        UpdateBattleText("しかし、完全絶対時間律【終焉】の効果により復活ができない！\r\n");
                    }
                    else if (target.CurrentNoResurrection > 0)
                    {
                        UpdateBattleText("しかし、" + target.FirstName + "は復活できなかった！");
                    }
                    else
                    {
                        if (command == Database.DEATH_DENY)
                        {
                            target.ResurrectPlayer((int)PrimaryLogic.DeathDenyValue(target));
                            target.CurrentMana = target.MaxMana;
                            UpdateMana(target, target.MaxMana, true, false, 0);
                            target.CurrentSkillPoint = target.MaxSkillPoint;
                            UpdateSkillPoint(target, target.MaxMana, true, false, 0);
                        }
                        else
                        {
                            target.ResurrectPlayer((int)PrimaryLogic.ResurrectionValue(target));
                        }
                        this.Update();
                        UpdateBattleText(target.FirstName + "は復活した！\r\n");
                    }
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        /// <summary>
        /// ストレートスマッシュのメソッド
        /// </summary>
        private void PlayerSkillStraightSmash(MainCharacter player, MainCharacter target, int interval, bool ignoreDefense)
        {
            UpdateBattleText(player.GetCharacterSentence(1));
            double damage = PrimaryLogic.StraightSmashValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, ignoreDefense, false, damage, interval, Database.SOUND_STRAIGHT_SMASH, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// サイレント・ラッシュのメソッド
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void PlayerSkillSilentRush(MainCharacter player, MainCharacter target)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                string soundName = "Hit01";
                int interval = 30;
                int sentence = 89; if (ii == 1) sentence = 90; if (ii == 2) sentence = 91;

                PlayerNormalAttack(player, target, 0, 0, false, false, 0, interval, soundName, sentence, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// カルネージラッシュのメソッド
        /// </summary>
        private void PlayerSkillCarnageRush(MainCharacter player, MainCharacter target)
        {
            for (int ii = 0; ii < 5; ii++)
            {
                string soundName = "Hit01"; if (ii == 4) soundName = "KineticSmash";
                int interval = 30; if (ii == 1 || ii == 2 || ii == 3) { interval = 8; } if (ii == 4) { interval = 30; }
                int sentence = 65; if (ii == 1) sentence = 66; if (ii == 2) sentence = 67; if (ii == 3) sentence = 68; if (ii == 4) sentence = 69;

                PlayerNormalAttack(player, target, 0, 0, false, false, 0, interval, soundName, sentence, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// ソウル・エグゼキューション
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        private void PlayerSkillSoulExecution(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(188), 1000);
            bool alreadyTruthVision = false;
            if (player.CurrentTruthVision <= 0)
            {
                PlayerSkillTruthVision(player, player);
            }
            else
            {
                alreadyTruthVision = true;
            }

            for (int ii = 0; ii < 10; ii++)
            {
                string soundName = "Hit01"; if (ii == 9) soundName = "Catastrophe";
                int[] interval = { 10, 9, 8, 7, 6, 5, 4, 3, 50, 0 };
                int[] sentence = { 189, 190, 191, 192, 193, 194, 195, 196, 197, 198 };
                double[] damageMag = { 1.0f, 1.1f, 1.2f, 1.3f, 1.5f, 1.7f, 1.9f, 2.2f, 2.5f, 3.0f }; // ; damageMag += ii * 0.2f; if (ii == 9) damageMag += 3.0f;
                PlayerNormalAttack(player, target, damageMag[ii], 0, false, false, 0, interval[ii], soundName, sentence[ii], false, CriticalType.Random);
            }

            if (alreadyTruthVision == false)
            {
                player.CurrentTruthVision = 0;
                player.DeBuff(player.pbTruthVision);
            }
        }


        /// <summary>
        /// エニグマ・センスのメソッド
        /// </summary>
        private void PlayerSkillEnigmaSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(72));
            double atkBase = PrimaryLogic.EnigmaSenseValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, atkBase, 0, string.Empty, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// クラッシング・ブローのメソッド
        /// </summary>
        private void PlayerSkillCrushingBlow(MainCharacter player)
        {
            PlayerNormalAttack(player, player.Target, 0, 2, false, false, 0, 0, Database.SOUND_CRUSHING_BLOW, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// キネティック・スマッシュのメソッド
        /// </summary>
        private void PlayerSkillKineticSmash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(74));
            double damage = PrimaryLogic.KineticSmashValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_KINETIC_SMASH, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// ソウル・インフィニティのメソッド
        /// </summary>
        private void PlayerSkillSoulInfinity(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(73));
            double damage = PrimaryLogic.SoulInfinityValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_SOUL_INFINITY, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// カタストロフィのメソッド
        /// </summary>
        private void PlayerSkillCatastrophe(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(98), 1000);
            double damage = PrimaryLogic.CatastropheValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_CATASTROPHE, 99, true, CriticalType.Random);
        }

        /// <summary>
        /// 朧・インパクトのメソッド
        /// </summary>
        private void PlayerSkillOboroImpact(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(96), 1000);
            double damage = PrimaryLogic.OboroImpactValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_OBORO_IMPACT, 97, true, CriticalType.Random);
        }


        /// <summary>
        /// インナー・インスピレーションのメソッド
        /// </summary>
        private void PlayerSkillInnerInspiration(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("InnerInspiration");
            double effectValue = PrimaryLogic.InnerInspirationValue(player);
            effectValue = GainIsZero(effectValue, player);
            UpdateBattleText(String.Format(player.GetCharacterSentence(51), Convert.ToString((int)effectValue)));
            player.CurrentSkillPoint += (int)effectValue;
            UpdateSkillPoint(player, effectValue, true, true, 0);
        }

        /// <summary>
        /// スタンス・オブ・スタンディングのメソッド
        /// </summary>
        private void PlayerSkillStanceOfStanding(MainCharacter player, MainCharacter target)
        {
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, player, 1, Database.STANCE_OF_STANDING + "");
            }
            UpdateBattleText(player.GetCharacterSentence(56));
        }
        /// <summary>
        /// ニュートラル・スマッシュのメソッド
        /// </summary>
        private void PlayerSkillNeutralSmash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(152));
            PlayerNormalAttack(player, target, 0, false, false);
        }
        /// <summary>
        /// スウィフト・ステップのメソッド
        /// </summary>
        private void PlayerSkillSwiftStep(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(153));
            GroundOne.PlaySoundEffect("StanceOfFlow");
            PlayerBuffAbstract(player, target, 3, "SwiftStep");
        }
        /// <summary>
        /// ヴィゴー・センスのメソッド
        /// </summary>
        private void PlayerSkillVigorSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(201));
            GroundOne.PlaySoundEffect("StanceOfFlow");
            PlayerBuffAbstract(player, target, 3, "VigorSense");
        }

        /// <summary>
        /// サークル・スラッシュのメソッド
        /// </summary>
        private void PlayerSkillCircleSlash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(154));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == GroundOne.MC || player == GroundOne.SC || player == GroundOne.TC)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }
            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerNormalAttack(player, group[ii], 0, false, false);
            }
        }
        /// <summary>
        /// ランブル・シャウトのメソッド
        /// </summary>
        private void PlayerSkillRumbleShout(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(155));
            target.Target = player;
            target.StackTarget = player;
        }
        /// <summary>
        /// スムージング・ムーヴのメソッド
        /// </summary>
        private void PlayerSkillSmoothingMove(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(156));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                GroundOne.PlaySoundEffect("AeroBlade");
                PlayerBuffAbstract(player, player, 1, "SmoothingMove");
            }
        }
        /// <summary>
        /// フューチャー・ヴィジョンのメソッド
        /// </summary>
        private void PlayerSkillFutureVision(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(157));
            GroundOne.PlaySoundEffect("Tranquility");
            PlayerBuffAbstract(player, player, 2, "FutureVision");
        }
        /// <summary>
        /// リフレックス・スピリットのメソッド
        /// </summary>
        private void PlayerSkillReflexSpirit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(158));
            GroundOne.PlaySoundEffect("Tranquility");
            PlayerBuffAbstract(player, player, 999, "ReflexSpirit");
        }
        /// <summary>
        /// シャープ・グレアのメソッド
        /// </summary>
        private void PlayerSkillSharpGlare(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(159));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                GroundOne.PlaySoundEffect("RisingKnuckle");
                NowSilence(player, target, 3);
            }
        }
        /// <summary>
        /// アンノウン・ショックのメソッド
        /// </summary>
        private void PlayerSkillUnknownShock(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(187));

            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                if (PlayerNormalAttack(player, group[ii], 0, false, false))
                {
                    GroundOne.PlaySoundEffect("RisingKnuckle");
                    NowBlind(player, group[ii], 3);
                }
            }
        }

        /// <summary>
        /// トラスト・サイレンスのメソッド
        /// </summary>
        private void PlayerSkillTrustSilence(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(160));
            GroundOne.PlaySoundEffect("Tranquility");
            PlayerBuffAbstract(player, player, 999, "TrustSilence");
        }
        /// <summary>
        /// サプライズ・アタックのメソッド
        /// </summary>
        private void PlayerSkillSurpriseAttack(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(161));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                bool result = NowParalyze(player, target, 1);
                if (result == false)
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeParalyze = true;
                }
            }
        }
        /// <summary>
        /// サイキック・ウェイヴのメソッド
        /// </summary>
        private void PlayerSkillPsychicWave(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(162));

            double damage = PrimaryLogic.PsychicWaveValue(player, GroundOne.DuelMode);
            AbstractMagicDamage(player, target, 0, damage, 0, "WordOfPower", -1, TruthActionCommand.MagicType.None, true, CriticalType.Random);
        }
        /// <summary>
        /// リカバーのメソッド
        /// </summary>
        private void PlayerSkillRecover(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(163));
            target.RecoverStunning();
            target.RecoverParalyze();
            target.RecoverFrozen();
        }
        /// <summary>
        /// バイオレント・スラッシュのメソッド
        /// </summary>
        private void PlayerSkillViolentSlash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(164));
            double damage = PrimaryLogic.ViolentSlashValue(player, GroundOne.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, true, false, damage, 0, Database.SOUND_KINETIC_SMASH, -1, false, CriticalType.Random);
        }
        /// <summary>
        /// アウター・インスピレーションのメソッド
        /// </summary>
        private void PlayerSkillOuterInspiration(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfLife");
            UpdateBattleText(player.GetCharacterSentence(165));
            target.RemovePhysicalAttackDown();
            target.RemovePhysicalDefenseDown();
            target.RemoveMagicAttackDown();
            target.RemoveMagicDefenseDown();
            target.RemoveSpeedDown();
            target.RemoveReactionDown();
            target.RemovePotentialDown();
            UpdateBattleText(target.FirstName + "の能力低下状態が解除された！");
        }

        /// <summary>
        /// ディープ・ミラーのメソッド
        /// </summary>
        private void PlayerSpellDeepMirror(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(179));
            player.CurrentDeepMirror = true;
        }

        /// <summary>
        /// スタンス・オブ・サッドネスのメソッド
        /// </summary>
        private void PlayerSkillStanceOfSuddenness(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(166));
            player.CurrentStanceOfSuddenness = true;
        }

        /// <summary>
        /// ハーデスト・パリィのメソッド
        /// </summary>
        private void PlayerSkillHardestParry(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(169));
            player.CurrentHardestParry = true;
        }

        /// <summary>
        /// コンカシッヴ・ヒットのメソッド
        /// </summary>
        private void PlayerSkillConcussiveHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(170));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "ConcussiveHit");
            }
        }

        /// <summary>
        /// オンスロート・ヒットのメソッド
        /// </summary>
        private void PlayerSkillOnslaughtHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(171));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "OnslaughtHit");
            }
        }

        /// <summary>
        /// インパルス・ヒットのメソッド
        /// </summary>
        private void PlayerSkillImpulseHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(172));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "ImpulseHit");
            }
        }

        /// <summary>
        /// ハイ・エモーショナリティのメソッド
        /// </summary>
        private void PlayerSkillHighEmotionality(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("HighEmotionality");
            player.Target = player;
            PlayerBuffAbstract(player, player, 4, "HighEmotionality");
        }

        /// <summary>
        /// スタンス・オブ・デスのメソッド
        /// </summary>
        private void PlayerSkillStanceOfDeath(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("StanceOfDeath");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "StanceOfDeath");
        }

        /// <summary>
        /// アンチ・スタンのメソッド
        /// </summary>
        private void PlayerSkillAntiStun(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("AntiStun");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "AntiStun");
        }

        /// <summary>
        /// ペインフル・インサニティ
        /// </summary>
        private void PlayerSkillPainfulInsanity(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("PainfulInsanity");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "PainfulInsanity");
        }

        /// <summary>
        /// ナッシング・オブ・ナッシングネスのメソッド
        /// </summary>
        private void PlayerSkillNothingOfNothingness(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("NothingOfNothingness");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "NothingOfNothingness");
        }

        #endregion

        #region "スキル"
        #region "動"
        #endregion
        #region "静"
        #endregion
        #region "柔"
        #endregion
        #region "剛"
        #endregion
        #region "心眼"
        #endregion
        #region "無心"

        private void PlayerSkillVoidExtraction(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("VoidExtraction");
            int effectValue = Math.Max(player.Strength, Math.Max(player.Agility, Math.Max(player.Intelligence, player.Mind)));
            string maxParameter = String.Empty;
            if (effectValue == player.Strength)
            {
                if ((effectValue - player.BuffStrength_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = Database.INFINITY;
                    player.ActivateBuff(player.pbVoidExtraction, Database.BaseResourceFolder + Database.VOID_EXTRACTION, Database.INFINITY);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "力", (effectValue - player.BuffStrength_VoidExtraction)));
                    player.BuffStrength_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else if (effectValue == player.Agility)
            {
                if ((effectValue - player.BuffAgility_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = Database.INFINITY;
                    player.ActivateBuff(player.pbVoidExtraction, Database.BaseResourceFolder + Database.VOID_EXTRACTION, Database.INFINITY);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "技", (effectValue - player.BuffAgility_VoidExtraction)));
                    player.BuffAgility_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else if (effectValue == player.Intelligence)
            {
                if ((effectValue - player.BuffIntelligence_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = Database.INFINITY;
                    player.ActivateBuff(player.pbVoidExtraction, Database.BaseResourceFolder + Database.VOID_EXTRACTION, Database.INFINITY);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "知", (effectValue - player.BuffIntelligence_VoidExtraction)));
                    player.BuffIntelligence_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else
            {
                if ((effectValue - player.Mind) > 0)
                {
                    player.CurrentVoidExtraction = Database.INFINITY;
                    player.ActivateBuff(player.pbVoidExtraction, Database.BaseResourceFolder + Database.VOID_EXTRACTION, Database.INFINITY);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "心", (effectValue - player.BuffMind_VoidExtraction)));
                    player.BuffMind_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
        }
        #endregion
        #region "動　静"
        #endregion
        #region "動　柔"
        #endregion
        #region "動　剛"
        #endregion
        #region "動　心眼"
        #endregion
        #region "動　無心"
        #endregion
        #region "静　柔"
        #endregion
        #region "静　剛"
        #endregion
        #region "静　心眼"
        #endregion
        #region "静　無心"
        #endregion
        #region "柔　剛"
        #endregion
        #region "柔　心眼"
        #endregion
        #region "柔　無心"
        #endregion
        #region "剛　心眼"
        #endregion
        #region "剛　無心"
        #endregion
        #region "心眼　無心"
        #endregion

        private void PlayerSkillDoubleSlash(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense)
        {
            Debug.Log("playerskilldoubleslash start");
            // todo 全体的に作り直しでは？
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.FirstName + "が" + target.FirstName + "へ" + Database.ATTACK_JP + "を仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.FirstName + "は無効化オーラによって護られている！\r\n");
                        target.RemoveStanceOfEyes();
                        target.RemoveNegate();
                        target.RemoveCounterAttack();
                    }
                    else
                    {
                        UpdateBattleText(player.FirstName + "が" + target.FirstName + "へ" + Database.ATTACK_JP + "を仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, 0, false, false);
                        return;
                    }
                }
            }

            UpdateBattleText(player.GetCharacterSentence(2));
            PlayerNormalAttack(player, target, 0, false, false);
            UpdateBattleText(player.GetCharacterSentence(3), 100);
            PlayerNormalAttack(player, target, 0, false, false);
        }


        private void PlayerSkillCounterAttack(MainCharacter player, MainCharacter target)
        {
            PlayerBuffAbstract(player, player, Database.INFINITY, Database.COUNTER_ATTACK + "");
            UpdateBattleText(player.FirstName + "はカウンターの構えをとっている。\r\n");
        }

        private void PlayerSkillNegate(MainCharacter player, MainCharacter target)
        {
            PlayerBuffAbstract(player, player, Database.INFINITY, Database.NEGATE + "");
            UpdateBattleText(player.FirstName + "はニゲイトの構えをとっている。\r\n");
        }

        private void PlayerSkillStanceOfEyes(MainCharacter player)
        {
            PlayerBuffAbstract(player, player, 1, Database.STANCE_OF_EYES + "");
            UpdateBattleText(player.FirstName + "はスタンス・オブ・アイズの構えをとっている。\r\n");
        }
        #endregion

        #region "元核"

        /// <summary>
        /// 元核：集中と断絶のメソッド
        /// </summary>
        private void PlayerArchetypeSyutyuDanzetsu(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SYUTYU_DANZETSU);
            PlayerBuffAbstract(player, player, 999, "SYUTYU-DANZETSU");
            player.AlreadyPlayArchetype = true;
        }
        /// <summary>
        /// 元核：循環と誓約のメソッド
        /// </summary>
        private void PlayerArchetypeJunkanSeiyaku(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_JUNKAN_SEIYAKU);
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerBuffAbstract(player, group[ii], (int)PrimaryLogic.JunkanSeiyakuValue(player), "JUNKAN-SEIYAKU");
            }
            player.AlreadyPlayArchetype = true;
        }

        private void PlayerArchetypeOraOraOraaa(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_ORA_ORA_ORAAA);
            int num = (int)PrimaryLogic.OraOraOraaaValue(player);
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
            }
            for (int ii = 0; ii < num; ii++)
            {
                string soundName = "Hit01"; if (ii == num - 1) soundName = "KineticSmash";
                int interval = 7; if (ii == num - 1) { interval = 60; }

                PlayerNormalAttack(player, group[AP.Math.RandomInteger(group.Count)], 0, 0, false, false, 0, interval, soundName, 115, false, CriticalType.Random);
            }
        }
        #endregion

    }
}
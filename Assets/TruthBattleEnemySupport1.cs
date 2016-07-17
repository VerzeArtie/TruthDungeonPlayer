using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DungeonPlayer;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        // 特定のターゲットを指定していない場合、敵・味方・自分を区別して、自動的にターゲットを選ぶもの
        private bool PlayerAttackPhase(MainCharacter player, bool withoutCost, bool skipStanceDouble, bool mainPhase)
        {
            bool _withoutCost = false;
            if (player.CurrentBlackContract > 0)
            {
                _withoutCost = true;
            }
            if (withoutCost)
            {
                _withoutCost = true;
            }

            if ((player.PA == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(player.CurrentSpellName) == TruthActionCommand.TargetType.Ally)
            {
                // 【要検討】リザレクション対象にするため、生死に関わらず、対象とする事を許可
                PlayerAttackPhase0(player, player.Target2, player.PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
                return true;

                // 初めからコメントアウト
                //if (player.Target2.Dead)
                //{
                //    return false;
                //}
                //else
                //{
                //    PlayerAttackPhase(player, player.Target2, player.PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, withoutCost);
                //    return true;
                //}
            }
            else if (((player.PA == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(player.CurrentSkillName) == TruthActionCommand.TargetType.Ally) ||
                     ((player.PA == MainCharacter.PlayerAction.Archetype) && TruthActionCommand.GetTargetType(player.CurrentArchetypeName) == TruthActionCommand.TargetType.Ally))
            {
                if ((player != null) && (player.Target2 != null) && (player.Target2.Dead))
                {
                    return false;
                }
                else
                {
                    PlayerAttackPhase0(player, player.Target2, player.PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
                    return true;
                }
            }
            else if (((player.PA == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(player.CurrentSpellName) == TruthActionCommand.TargetType.Own) ||
                     ((player.PA == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(player.CurrentSkillName) == TruthActionCommand.TargetType.Own) ||
                     ((player.PA == MainCharacter.PlayerAction.Archetype) && TruthActionCommand.GetTargetType(player.CurrentArchetypeName) == TruthActionCommand.TargetType.Own))
            {
                PlayerAttackPhase0(player, player, player.PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
                return true;
            }
            else // PlayerAction.NormalAttackとそれ以外を一つにまとめて記述。
            {
                if ((player != null) && (player.Target != null) && (player.Target.Dead))
                {
                    return false;
                }
                else
                {
                    PlayerAttackPhase0(player, player.Target, player.PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
                    return true;
                }
            }
        }
        // ターゲット、アクションタイプ（魔法・スキル・アイテム）、そして使用する名前が決まっている場合
        private void PlayerAttackPhase(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CommandName, bool withoutCost, bool skipStanceDouble, bool mainPhase)
        {
            bool _withoutCost = false;
            if (player.CurrentBlackContract > 0)
            {
                _withoutCost = true;
            }
            if (withoutCost)
            {
                _withoutCost = true;
            }

            if (PA == MainCharacter.PlayerAction.UseSpell)
            {
                PlayerAttackPhase0(player, target, PA, CommandName, String.Empty, String.Empty, String.Empty, _withoutCost, skipStanceDouble, mainPhase);
            }
            else if (PA == MainCharacter.PlayerAction.UseSkill)
            {
                PlayerAttackPhase0(player, target, PA, String.Empty, CommandName, String.Empty, String.Empty, _withoutCost, skipStanceDouble, mainPhase);
            }
            else if (PA == MainCharacter.PlayerAction.UseItem)
            {
                PlayerAttackPhase0(player, target, PA, String.Empty, String.Empty, CommandName, String.Empty, _withoutCost, skipStanceDouble, mainPhase);
            }
            else if (PA == MainCharacter.PlayerAction.Archetype)
            {
                PlayerAttackPhase0(player, target, PA, String.Empty, String.Empty, String.Empty, CommandName, _withoutCost, skipStanceDouble, mainPhase);
            }
            else if (PA == MainCharacter.PlayerAction.NormalAttack)
            {
                PlayerAttackPhase0(player, target, PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
            }
            else
            {
                PlayerAttackPhase0(player, target, PA, player.CurrentSpellName, player.CurrentSkillName, player.CurrentUsingItem, player.CurrentArchetypeName, _withoutCost, skipStanceDouble, mainPhase);
            }
        }

        private void PlayerAttackPhase0(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CurrentSpellName, String CurrentSkillName, String CurrentUsingItem, String CurrentArchetypeName, bool withoutCost, bool skipStanceDouble, bool mainPhase)
        {
            // try-catch-finally制御でも良いが、速度性能重視とする。
            player.NowExecActionFlag = true;

            PlayerAttackPhase1(player, target, PA, CurrentSpellName, CurrentSkillName, CurrentUsingItem, CurrentArchetypeName, withoutCost, skipStanceDouble, mainPhase);

            // GaleWindがかかっている場合、コスト０で２回行動
            if (player.CurrentGaleWind >= 1)
            {
                PlayerAttackPhase1(player, target, PA, CurrentSpellName, CurrentSkillName, CurrentUsingItem, CurrentArchetypeName, true, skipStanceDouble, false); // GaleWindによる２回目行動はメイン行動ではない
            }

            player.NowExecActionFlag = false;
        }
        private void PlayerAttackPhase1(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CurrentSpellName, String CurrentSkillName, String CurrentUsingItem, String CurrentArchetypeName, bool withoutCost, bool skipStanceDouble, bool mainPhase)
        {
            // StanceOfDoubleがかかっており、かつ、StanceOfDouble経由ではない場合、前回の行動をおこなう。
            if (player.CurrentStanceOfDouble >= 1 && !skipStanceDouble)
            {
                ExecBeforeAttackPhase(player, true);
            }
            PlayerAttackPhase2(player, target, PA, CurrentSpellName, CurrentSkillName, CurrentUsingItem, CurrentArchetypeName, withoutCost, mainPhase);
        }

        // [警告] 本メソッドを直接呼んだ場合、GaleWindおよびStanceOfDoubleは適用されない。
        private void PlayerAttackPhase2(MainCharacter player, MainCharacter target, MainCharacter.PlayerAction PA, String CurrentSpellName, String CurrentSkillName, String CurrentUsingItem, String CurrentArchetypeName, bool withoutCost, bool mainPhase)
        {
            Debug.Log(PA.ToString() + " " + CurrentSpellName + " " + CurrentSkillName + " " + CurrentUsingItem);
            string fileExt = ".bmp";
            double effectValue = 1.0F;

            // 行動の成功・失敗を問わず、アクションコマンド自体は記憶する。
            if ((PA == MainCharacter.PlayerAction.UseSpell && CurrentSpellName == Database.GENESIS) ||
                (PA == MainCharacter.PlayerAction.UseSkill && CurrentSkillName == Database.STANCE_OF_DOUBLE))
            {
                // Genesis、StanceOfDouble自体が行動の場合、それは記憶しない
            }
            else
            {
                player.UpdateGenesisCommand(PA, CurrentSpellName, CurrentSkillName, CurrentUsingItem, CurrentArchetypeName);
            }

            // 行動実行直前にトリガードイベントで発動されるアクション
            if (player.CurrentTemptation > 0)
            {
                // 「誘惑」敵にダメージを与える要因がある行動ができなくなる。
                if (PA == MainCharacter.PlayerAction.UseSpell && TruthActionCommand.IsTemptationEffect(CurrentSpellName))
                {
                    UpdateBattleText(player.FirstName + "は誘惑にかられており、敵にダメージを与える気になれないでいる。\r\n");
                    return;
                }
                if (PA == MainCharacter.PlayerAction.UseSkill && TruthActionCommand.IsTemptationEffect(CurrentSkillName))
                {
                    UpdateBattleText(player.FirstName + "は誘惑にかられており、敵にダメージを与える気になれないでいる。\r\n");
                    return;
                }
                if (PA == MainCharacter.PlayerAction.Archetype && TruthActionCommand.IsTemptationEffect(CurrentArchetypeName))
                {
                    // 元核は「誘惑」の効果を超えて発動するため、ここでは何もしない。
                }
                if (PA == MainCharacter.PlayerAction.NormalAttack)
                {
                    UpdateBattleText(player.FirstName + "は誘惑にかられており、敵にダメージを与える気になれないでいる。\r\n");
                    return;
                }
            }

            if (player.CurrentPreStunning > 0)
            {
                if (player.PA == MainCharacter.PlayerAction.Defense)
                {
                    // 防御は恐怖スタンの対象外
                }
                else if (player.PA == MainCharacter.PlayerAction.Charge)
                {
                    // ためるは恐怖スタンの対象外
                }
                else if (player.PA == MainCharacter.PlayerAction.None)
                {
                    // 待機は恐怖スタンの対象外
                }
                else if ((player.PA == MainCharacter.PlayerAction.UseSpell) &&
                         ((TruthActionCommand.GetTargetType(CurrentSpellName) == TruthActionCommand.TargetType.AllMember) ||
                          (TruthActionCommand.GetTargetType(CurrentSpellName) == TruthActionCommand.TargetType.Ally) ||
                          (TruthActionCommand.GetTargetType(CurrentSpellName) == TruthActionCommand.TargetType.AllyGroup) ||
                          (TruthActionCommand.GetTargetType(CurrentSpellName) == TruthActionCommand.TargetType.InstantTarget) ||
                          (TruthActionCommand.GetTargetType(CurrentSpellName) == TruthActionCommand.TargetType.Own))
                        )
                {
                    // 自分、味方、味方全体、全員対象は恐怖スタンの対象外
                }
                else if ((player.PA == MainCharacter.PlayerAction.UseSkill) &&
                         ((TruthActionCommand.GetTargetType(CurrentSkillName) == TruthActionCommand.TargetType.AllMember) ||
                          (TruthActionCommand.GetTargetType(CurrentSkillName) == TruthActionCommand.TargetType.Ally) ||
                          (TruthActionCommand.GetTargetType(CurrentSkillName) == TruthActionCommand.TargetType.AllyGroup) ||
                          (TruthActionCommand.GetTargetType(CurrentSkillName) == TruthActionCommand.TargetType.InstantTarget) ||
                          (TruthActionCommand.GetTargetType(CurrentSkillName) == TruthActionCommand.TargetType.Own))
                        )
                {
                    // 自分、味方、味方全体、全員対象は恐怖スタンの対象外
                }
                else if (player.PA == MainCharacter.PlayerAction.Archetype)
                {
                    // 元核は対象外
                }
                else if (target != null)
                {
                    if (target == ec1 || target == ec2 || target == ec3)
                    {
                        AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_ACTION);
                        player.CurrentPreStunning = 0;
                        player.DeBuff(player.pbPreStunning);
                        NowStunning(player, player, 2, false);
                        return;
                    }
                }
            }

            // 任意の行動を行うたびに、神の蓄積カウンターが１つ自分にBUFFとして蓄積する。
            if ((player.MainWeapon != null) && (player.MainWeapon.Name == Database.LEGENDARY_FELTUS) && (PA != MainCharacter.PlayerAction.Defense))
            {
                PlayerBuffAbstract(player, player, Database.INFINITY, Database.ITEMCOMMAND_FELTUS);
            }
            if ((player.SubWeapon != null) && (player.SubWeapon.Name == Database.LEGENDARY_FELTUS) && (PA != MainCharacter.PlayerAction.Defense))
            {
                PlayerBuffAbstract(player, player, Database.INFINITY, Database.ITEMCOMMAND_FELTUS);
            }

            // 任意の行動を行うたびに、蒼の蓄積カウンターが１つ自分にBUFFとして蓄積する。
            if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_ADILRING_OF_BLUE_BURN) && (PA != MainCharacter.PlayerAction.Defense))
            {
                PlayerBuffAbstract(player, player, Database.INFINITY, Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_ADILRING_OF_BLUE_BURN) && (PA != MainCharacter.PlayerAction.Defense))
            {
                PlayerBuffAbstract(player, player, Database.INFINITY, Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN);
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave > 0)
            {
                double immortalRaveValue = PrimaryLogic.ImmmortalRaveValue(player, GroundOne.DuelMode);
                List<MainCharacter> group = new List<MainCharacter>();
                if (IsPlayerAlly(player))
                {
                    SetupEnemyGroup(ref group);
                }
                else
                {
                    SetupAllyGroup(ref group);
                }
                AbstractMagicDamage(player, group[AP.Math.RandomInteger(group.Count - 1)], 0, ref immortalRaveValue, 0, "VolcanicWave", 0, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
            }

            // 任意の行動が敵から自分に行われた場合、まれに回避する。
            if ((IsPlayerAlly(player) && IsPlayerEnemy(target)) ||
                (IsPlayerEnemy(player) && IsPlayerAlly(target)))
            {
                if (((target.Accessory != null) && (target.Accessory.Name == Database.COMMON_FLOATING_WHITE_BALL)) ||
                    ((target.Accessory2 != null) && (target.Accessory2.Name == Database.COMMON_FLOATING_WHITE_BALL)))
                {
                    if (AP.Math.RandomInteger(100) < PrimaryLogic.FloatingWhiteBallValue(player))
                    {
                        UpdateBattleText(target.FirstName + "が装着している" + Database.COMMON_FLOATING_WHITE_BALL + "が突如オーラを放ち、回避行動を" + target.FirstName + "にとらせた！\r\n");
                        AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_ACTION);
                        return;
                    }
                }
            }

            // コマンド実行開始
            switch (PA)
            {
                case MainCharacter.PlayerAction.NormalAttack:
                    if (!target.Dead)
                    {
                        if (CheckCounterAttack(player, Database.ATTACK_EN)) { return; }
                        PlayerNormalAttack(player, target, 0.0f, false, false);
                        player.CurrentPhysicalChargeCount = 0;
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(120));
                    }
                    break;

                case MainCharacter.PlayerAction.Defense:
                    // 防御は何もしない。
                    break;

                case MainCharacter.PlayerAction.Charge:
                    // 敵側はRodを持つ必要なく、ためるが可能とする。
                    if (player == ec1 || player == ec2 || player == ec3)
                    {
                        if (player.CurrentChargeCount <= 0)
                        {
                            UpdateBattleText(player.GetCharacterSentence(122));
                            player.CurrentChargeCount++;
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(123));
                        }
                    }
                    else
                    {
                        // 味方側はRodを持つ事で、ためるが可能となる。
                        if (player.MainWeapon != null)
                        {
                            if (player.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod)
                            {
                                if (player.CurrentChargeCount <= 0)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(122));
                                    player.CurrentChargeCount++;
                                }
                                else
                                {
                                    UpdateBattleText(player.GetCharacterSentence(123));
                                }
                            }
                        }
                    }
                    break;

                #region "スペル"
                case MainCharacter.PlayerAction.UseSpell:
                    PreExecPlaySpell(player, target, withoutCost, mainPhase, CurrentSpellName);
                    if (((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_ORB_GROUNDSEA_STAR)) ||
                        ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_ORB_GROUNDSEA_STAR)))
                    {
                        if (AP.Math.RandomInteger(100) <= PrimaryLogic.GroundSeaStarValue(player))
                        {
                            PreExecPlaySpell(player, target, withoutCost, false, CurrentSpellName);
                        }
                    }
                    break;
                #endregion

                #region "スキル"
                case MainCharacter.PlayerAction.UseSkill:
                    PreExecPlaySkill(player, target, withoutCost, mainPhase, CurrentSkillName);
                    break;
                #endregion

                #region "元核"
                case MainCharacter.PlayerAction.Archetype:
                    PreExecPlayArchetype(player, target, withoutCost, CurrentArchetypeName);
                    break;
                #endregion

                #region "アイテム使用"
                case MainCharacter.PlayerAction.UseItem:
                    effectValue = 0.0f;
                    List<MainCharacter> groupAlly = new List<MainCharacter>();
                    List<MainCharacter> groupEnemy = new List<MainCharacter>();
                    SetupAllyGroup(ref groupAlly);
                    SetupEnemyGroup(ref groupEnemy);

                    switch (CurrentUsingItem)
                    {
                        case Database.POOR_SMALL_RED_POTION:
                        case Database.COMMON_NORMAL_RED_POTION:
                        case Database.COMMON_LARGE_RED_POTION:
                        case Database.COMMON_HUGE_RED_POTION:
                        case Database.COMMON_GORGEOUS_RED_POTION:
                            ItemBackPack item;
                            if (CurrentUsingItem != string.Empty)
                            {
                                item = new ItemBackPack(CurrentUsingItem);
                            }
                            else
                            {
                                item = new ItemBackPack(player.CurrentUsingItem);
                            }

                            int effect = item.UseIt(); // [警告]：汎用性の高いメソッド名だが、実質ポーション専用になっています。
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。" + effect.ToString() + " ライフ回復\r\n");
                                if (player.CurrentNourishSense > 0)
                                {
                                    effect = (int)((double)effect * PrimaryLogic.NourishSenseValue(player));
                                }
                                effect = (int)GainIsZero(effect, player);
                                player.CurrentLife += effect;
                                UpdateLife(player, effect, true, true, 0, false);
                            }
                            player.DeleteBackPack(item);
                            break;

                        case Database.POOR_SMALL_BLUE_POTION:
                        case Database.COMMON_NORMAL_BLUE_POTION:
                        case Database.COMMON_LARGE_BLUE_POTION:
                        case Database.COMMON_HUGE_BLUE_POTION:
                        case Database.COMMON_GORGEOUS_BLUE_POTION:
                            if (CurrentUsingItem != string.Empty)
                            {
                                item = new ItemBackPack(CurrentUsingItem);
                            }
                            else
                            {
                                item = new ItemBackPack(player.CurrentUsingItem);
                            }

                            effect = item.UseIt(); // [警告]：汎用性の高いメソッド名だが、実質ポーション専用になっています。
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。" + effect.ToString() + " マナ回復\r\n");
                                player.CurrentMana += effect;
                                UpdateMana(player);
                            }
                            player.DeleteBackPack(item);
                            break;

                        case Database.POOR_SMALL_GREEN_POTION:
                        case Database.COMMON_NORMAL_GREEN_POTION:
                        case Database.COMMON_LARGE_GREEN_POTION:
                        case Database.COMMON_HUGE_GREEN_POTION:
                        case Database.COMMON_GORGEOUS_GREEN_POTION:
                            if (CurrentUsingItem != string.Empty)
                            {
                                item = new ItemBackPack(CurrentUsingItem);
                            }
                            else
                            {
                                item = new ItemBackPack(player.CurrentUsingItem);
                            }

                            effect = item.UseIt(); // [警告]：汎用性の高いメソッド名だが、実質ポーション専用になっています。
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。" + effect.ToString() + " スキルポイント回復\r\n");
                                player.CurrentSkillPoint += effect;
                                UpdateSkillPoint(player);
                            }
                            player.DeleteBackPack(item);
                            break;

                        case Database.RARE_PURE_WATER:
                            item = new ItemBackPack(player.CurrentUsingItem);
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。" + player.MaxLife.ToString() + " ライフ回復\r\n");
                                player.CurrentLife = player.MaxLife;
                                UpdateLife(player, Convert.ToDouble(player.MaxLife), true, true, 0, false);
                            }
                            player.DeleteBackPack(item);
                            break;
                        case Database.RARE_PURE_GREEN_WATER:
                            item = new ItemBackPack(player.CurrentUsingItem);
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。" + player.MaxSkillPoint.ToString() + " スキルポイント回復\r\n");
                                player.CurrentSkillPoint = player.MaxSkillPoint;
                                UpdateSkillPoint(player, Convert.ToDouble(player.MaxSkillPoint), true, true, 0);
                            }
                            player.DeleteBackPack(item);
                            break;
                        case Database.EPIC_GOLD_POTION:
                            item = new ItemBackPack(player.CurrentUsingItem);
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.FirstName + "は" + item.Name + "を使った。ライフ／マナ／スキルポイントが全回復した！\r\n");
                                player.CurrentLife = player.MaxLife;
                                UpdateLife(player, Convert.ToDouble(player.MaxLife), true, true, 0, false);
                                player.CurrentMana = player.MaxMana;
                                UpdateMana(player, Convert.ToDouble(player.MaxMana), true, true, 0);
                                player.CurrentSkillPoint = player.MaxSkillPoint;
                                UpdateSkillPoint(player, Convert.ToDouble(player.MaxSkillPoint), true, true, 0);
                            }
                            player.DeleteBackPack(item);
                            break;

                        case Database.RARE_AERO_BLADE:
                            effectValue = PrimaryLogic.AeroBladeValue(player, GroundOne.DuelMode);
                            AbstractMagicDamage(player, target, 0, ref effectValue, 0, "AeroBlade", 5001, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            break;
                        case Database.RARE_LIFE_SWORD:
                            effectValue = PrimaryLogic.LifeSwordValue(player, GroundOne.DuelMode);
                            PlayerAbstractLifeGain(player, target, 0, effectValue, 0, "FreshHeal", 5002);
                            break;
                        case Database.RARE_AUTUMN_ROD:
                            effectValue = PrimaryLogic.AutumnRodValue(player);
                            PlayerAbstractManaGain(player, player, 0, effectValue, 0, "FreshHeal", 5003);
                            break;
                        case Database.RARE_FLOWER_WAND:
                            effectValue = PrimaryLogic.FlowerWandValue(player);
                            PlayerAbstractManaGain(player, player, 0, effectValue, 0, "FreshHeal", 5003);
                            break;
                        case Database.COMMON_HAYATE_ORB:
                            GroundOne.PlaySoundEffect("HeatBoost");
                            player.CurrentSpeedBoost = (int)PrimaryLogic.HayateOrbValue(player);
                            break;
                        case Database.RARE_ICE_SWORD:
                            GroundOne.PlaySoundEffect("IceNeedle");
                            effectValue = PrimaryLogic.IceSwordValue(player, GroundOne.DuelMode);
                            AbstractMagicDamage(player, target, 0, ref effectValue, 0, "IceNeedle", 5004, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                            break;
                        case Database.RARE_RISING_KNUCKLE:
                            GroundOne.PlaySoundEffect("RisingKnuckle");
                            effectValue = PrimaryLogic.RisingKnuckleValue(player, GroundOne.DuelMode);
                            AbstractMagicDamage(player, target, 0, ref effectValue, 0, "RisingKnuckle", 5005, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            break;
                        case Database.COMMON_FROZEN_BALL:
                            UpdateBattleText(player.FirstName + "は" + Database.COMMON_FROZEN_BALL + "を放ってきた！\r\n");
                            System.Threading.Thread.Sleep(1000);
                            GroundOne.PlaySoundEffect("IceNeedle");
                            NowFrozen(player, target, 3);
                            item = new ItemBackPack(CurrentUsingItem);
                            player.DeleteBackPack(item);
                            break;
                        case Database.RARE_BLUE_LIGHTNING:
                            for (int ii = 0; ii < 2; ii++)
                            {
                                effectValue = PrimaryLogic.BlueLightningValue(player, GroundOne.DuelMode);
                                AbstractMagicDamage(player, target, 0, ref effectValue, 0, "blueLightning", 5006, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            }
                            break;
                        case Database.RARE_BURNING_CLAYMORE:
                            effectValue = PrimaryLogic.BurningClaymoreValue(player);
                            player.CurrentStrengthUp = Database.INFINITY;
                            player.CurrentStrengthUpValue = (int)PrimaryLogic.BurningClaymoreValue(player);
                            player.ActivateBuff(player.pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                            break;
                        case Database.COMMON_CHIENOWA_RING:
                            effectValue = PrimaryLogic.ChienowaRingValue(player);
                            AnimationDamage(0, target, 0, Color.blue, false, false, Database.RESIST_ICE_UP);
                            player.CurrentResistIceUp = Database.INFINITY;
                            player.CurrentResistIceUpValue = (int)effectValue;
                            player.ActivateBuff(player.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp", Database.INFINITY);
                            break;
                        case Database.COMMON_ROCKET_DASH:
                            GroundOne.PlaySoundEffect("HeatBoost");
                            for (int ii = 0; ii < (int)PrimaryLogic.RocketDashValue(player); ii++)
                            {
                                player.BattleBarPos--;
                                if (player.BattleBarPos <= 0)
                                {
                                    player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                                }
                                this.Update();
                                System.Threading.Thread.Sleep(1);
                            }
                            break;
                        case Database.COMMON_WAR_DRUM:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                AnimationDamage(0, groupAlly[ii], 0, Color.black, false, false, Database.PHYSICAL_ATTACK_UP);
                                groupAlly[ii].CurrentStrengthUp = Database.INFINITY;
                                groupAlly[ii].CurrentStrengthUpValue = (int)PrimaryLogic.WarDrumValue(player);
                                groupAlly[ii].ActivateBuff(groupAlly[ii].pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                            }
                            break;
                        case Database.RARE_ROD_OF_STRENGTH:
                            player.CurrentStrengthUp = Database.INFINITY;
                            player.CurrentStrengthUpValue = (int)PrimaryLogic.RodOfStrengthValue(player);
                            player.ActivateBuff(player.pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                            break;
                        case Database.RARE_WRATH_SERVEL_CLAW:
                            effectValue = PrimaryLogic.WrathServelClawValue(player, GroundOne.DuelMode);
                            AbstractMagicDamage(player, target, 0, ref effectValue, 0, "blueLightning", 5006, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            break;
                        case Database.RARE_BLUE_RED_ROD:
                            effectValue = PrimaryLogic.BlueRedRodValue(player, GroundOne.DuelMode);
                            if (AbstractMagicDamage(player, target, 0, ref effectValue, 0, "FireBall", 5008, TruthActionCommand.MagicType.Fire, false, CriticalType.Random))
                            {
                                effectValue = PrimaryLogic.BlueRedRodValue_A(player);
                                PlayerAbstractManaGain(player, player, 0, effectValue, 0, "FreshHeal", 5003);
                            }
                            break;
                        case Database.RARE_MEIUN_BOX:
                            int randomValue = AP.Math.RandomInteger(3);
                            if (randomValue == 0)
                            {
                                PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.MeiunBoxValue_A(player), 0, "FreshHeal", 5002);
                            }
                            else if (randomValue == 1)
                            {
                                PlayerAbstractManaGain(player, player, 0, PrimaryLogic.MeiunBoxValue_B(player), 0, "FreshHeal", 5003);
                            }
                            else if (randomValue == 2)
                            {
                                PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.MeiunBoxValue_C(player), 0, "FreshHeal", 5009);
                            }
                            break;
                        case Database.RARE_SYUURENSYA_KUROOBI:
                            PreExecPlaySkill(player, target, false, false, Database.INNER_INSPIRATION);
                            break;
                        case Database.RARE_SHIHANDAI_KUROOBI:
                            PreExecPlaySkill(player, target, false, false, Database.STANCE_OF_EYES);
                            break;
                        case Database.RARE_SYUUDOUSOU_KUROOBI:
                            PreExecPlaySkill(player, target, false, false, Database.PURE_PURIFICATION);
                            break;
                        case Database.RARE_KUGYOUSYA_KUROOBI:
                            PreExecPlaySkill(player, target, false, false, Database.NEGATE);
                            break;
                        case Database.COMMON_STAR_DUST_RING:
                            PreExecPlaySpell(player, target, false, false, Database.WORD_OF_POWER);
                            break;
                        case Database.RARE_FROZEN_LAVA:
                            if (AP.Math.RandomInteger(2) <= 0)
                            {
                                PreExecPlaySpell(player, target, false, false, Database.FLAME_STRIKE);
                            }
                            else
                            {
                                PreExecPlaySpell(player, target, false, false, Database.FROZEN_LANCE);
                            }
                            break;
                        case Database.RARE_SHARPNEL_SPIN_BLADE:
                            player.AmplifyBattleSpeed = 1.1f;
                            player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, Database.INFINITY);
                            break;
                        case Database.RARE_BLUE_LIGHT_MOON_CLAW:
                            player.AmplifyPhysicalAttack = 1.1f;
                            player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, Database.INFINITY);
                            break;
                        case Database.RARE_SHAERING_BONE_CRUSHER:
                            player.AmplifyPotential = 1.1f;
                            player.ActivateBuff(player.pbPotentialUp, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_UP, Database.INFINITY);
                            break;
                        case Database.RARE_BLIZZARD_SNOW_ROD:
                            player.AmplifyMagicAttack = 1.1f;
                            player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, Database.INFINITY);
                            break;
                        case Database.COMMON_WINTERS_HORN:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                AnimationDamage(0, groupAlly[ii], 0, Color.black, false, false, Database.MAGIC_ATTACK_UP);
                                groupAlly[ii].CurrentIntelligenceUp = Database.INFINITY;
                                groupAlly[ii].CurrentIntelligenceUpValue = (int)PrimaryLogic.WintersHornValue(player);
                                groupAlly[ii].ActivateBuff(groupAlly[ii].pbIntelligenceUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                            }
                            break;
                        case Database.RARE_TEARS_END:
                            PreExecPlaySpell(player, target, false, false, Database.RISE_OF_IMAGE);
                            break;
                        case Database.RARE_SKY_COLD_BOOTS:
                            PreExecPlaySpell(player, target, false, false, Database.HEAT_BOOST);
                            break;
                        case Database.RARE_EARTH_BREAKERS_SIGIL:
                            PreExecPlaySpell(player, target, false, false, Database.BLOODY_VENGEANCE);
                            break;
                        case Database.RARE_AERIAL_VORTEX:
                            PreExecPlaySpell(player, target, false, false, Database.PROMISED_KNOWLEDGE);
                            break;
                        case Database.RARE_LIVING_GROWTH_SEED:
                            PreExecPlaySpell(player, target, false, false, Database.WORD_OF_LIFE);
                            break;
                        case Database.COMMON_LIGHT_SERVANT:
                            effectValue = PrimaryLogic.LightServantValue(player);
                            if (IsPlayerEnemy(player))
                            {
                                for (int ii = 0; ii < groupEnemy.Count; ii++)
                                {
                                    PlayerAbstractLifeGain(player, groupEnemy[ii], 0, effectValue, 0, Database.SOUND_FRESH_HEAL, 5002);
                                }
                            }
                            else
                            {
                                for (int ii = 0; ii < groupAlly.Count; ii++)
                                {
                                    PlayerAbstractLifeGain(player, groupAlly[ii], 0, effectValue, 0, Database.SOUND_FRESH_HEAL, 5002);
                                }
                            }
                            player.RemoveLightServant();
                            break;
                        case Database.COMMON_SHADOW_SERVANT:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                UpdateBattleText(groupAlly[ii].FirstName + "のデバフ効果が解除された。\r\n");
                                groupAlly[ii].RemovePhysicalAttackDown();
                                groupAlly[ii].RemovePhysicalDefenseDown();
                                groupAlly[ii].RemoveMagicAttackDown();
                                groupAlly[ii].RemoveMagicDefenseDown();
                                groupAlly[ii].RemoveSpeedDown();
                                groupAlly[ii].RemoveReactionDown();
                                groupAlly[ii].RemovePotentialDown();
                                groupAlly[ii].RemoveLightDown();
                                groupAlly[ii].RemoveShadowDown();
                                groupAlly[ii].RemoveFireDown();
                                groupAlly[ii].RemoveIceDown();
                                groupAlly[ii].RemoveForceDown();
                                groupAlly[ii].RemoveWillDown();

                                // DEBUFF効果というカテゴリに負の影響効果も含まれるのかどうかがハッキリしない。仕様再検討必要だが
                                // これが含まれていないと、「ユーザー観点として使えないアイテム」の烙印が押されるので、
                                // 使えるアイテムとして含めることとしたい
                                groupAlly[ii].RemovePreStunning();
                                groupAlly[ii].RemoveStun();
                                groupAlly[ii].RemoveSilence();
                                groupAlly[ii].RemovePoison();
                                groupAlly[ii].RemoveTemptation();
                                groupAlly[ii].RemoveFrozen();
                                groupAlly[ii].RemoveParalyze();
                                groupAlly[ii].RemoveNoResurrection();
                                groupAlly[ii].RemoveSlow();
                                groupAlly[ii].RemoveBlind();
                                groupAlly[ii].RemoveSlip();
                            }
                            player.RemoveShadowServant();
                            break;
                        case Database.EPIC_ADILRING_OF_BLUE_BURN:
                            effectValue = PrimaryLogic.AdilBlueBurnValue(player);
                            AbstractMagicDamage(player, target, 0, ref effectValue, 0, "blueLightning", 5010, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            break;
                        case Database.RARE_SOUSUI_HIDENSYO:
                            PreExecPlaySkill(player, target, false, false, Database.VIOLENT_SLASH);
                            break;
                        case Database.RARE_MEEK_HIDENSYO:
                            PreExecPlaySkill(player, target, false, false, Database.RECOVER);
                            break;
                        case Database.RARE_JUKUTATUSYA_HIDENSYO:
                            PreExecPlaySkill(player, target, false, false, Database.SWIFT_STEP);
                            break;
                        case Database.RARE_KYUUDOUSYA_HIDENSYO:
                            PreExecPlaySkill(player, player, false, false, Database.FUTURE_VISION);
                            break;
                        case Database.COMMON_SOCIETY_SYMBOL:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                effectValue = PrimaryLogic.SocietySymbolValue(player);
                                BuffUpPhysicalAttack(groupAlly[ii], effectValue);
                                BuffUpPhysicalDefense(groupAlly[ii], effectValue);
                            }
                            break;
                        case Database.COMMON_SILVER_FEATHER_BRACELET:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                effectValue = PrimaryLogic.SilverFeatherBraceletValue(player);
                                BuffUpMagicAttack(groupAlly[ii], effectValue);
                                BuffUpMagicDefense(groupAlly[ii], effectValue);
                            }
                            break;
                        case Database.COMMON_BIRD_SONG_LUTE:
                            for (int ii = 0; ii < groupAlly.Count; ii++)
                            {
                                effectValue = PrimaryLogic.BirdSongLuteValue(player);
                                groupAlly[ii].BuffUpMind(effectValue);
                            }
                            break;
                        case Database.RARE_SPELL_COMPASS:
                            if (player.CurrentShadowPact <= 0)
                            {
                                PreExecPlaySpell(player, player, false, false, Database.SHADOW_PACT);
                            }
                            else if (player.CurrentPromisedKnowledge <= 0)
                            {
                                PreExecPlaySpell(player, player, false, false, Database.PROMISED_KNOWLEDGE);
                            }
                            else if (player.CurrentPsychicTrance <= 0)
                            {
                                PreExecPlaySpell(player, player, false, false, Database.PSYCHIC_TRANCE);
                            }
                            break;
                        case Database.RARE_BLIND_NEEDLE:
                            target.RemovePreStunning();
                            target.RemoveStun();
                            target.RemoveSilence();
                            target.RemovePoison();
                            target.RemoveTemptation();
                            target.RemoveFrozen();
                            target.RemoveParalyze();
                            target.RemoveNoResurrection();
                            target.RemoveSlow();
                            target.RemoveBlind();
                            target.RemoveSlip();
                            break;
                        case Database.RARE_CORE_ESSENCE_CHANNEL:
                            UpdateBattleText("活性の脈動が" + player.FirstName + "に響き渡る\r\n");
                            PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.CoreEssenceChannelValue_A(player), 0, "FreshHeal", 5002);
                            PlayerAbstractManaGain(player, player, 0, PrimaryLogic.CoreEssenceChannelValue_B(player), 0, "FreshHeal", 5003);
                            PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.CoreEssenceChannelValue_C(player), 0, "FreshHeal", 5009);
                            break;

                        case Database.RARE_HUNTERS_EYE:
                            switch (AP.Math.RandomInteger(4))
                            {
                                case 0:
                                    for (int ii = 0; ii < groupEnemy.Count; ii++)
                                    {
                                        NowSlow(player, groupEnemy[ii], 4);
                                    }
                                    break;
                                case 1:
                                    for (int ii = 0; ii < groupAlly.Count; ii++)
                                    {
                                        if (groupAlly[ii].CurrentTruthVision <= 0)
                                        {
                                            PreExecPlaySpell(player, groupAlly[ii], true, false, Database.TRUTH_VISION);
                                        }
                                    }
                                    break;
                                case 2:
                                    BuffUpPhysicalAttack(player, PrimaryLogic.HuntersEyeValue_A(player));
                                    BuffUpBattleSpeed(player, PrimaryLogic.HuntersEyeValue_B(player));
                                    break;
                                case 3:
                                    BuffDownPhysicalAttack(target, PrimaryLogic.HuntersEyeValue_C(player));
                                    BuffDownBattleSpeed(target, PrimaryLogic.HuntersEyeValue_D(player));
                                    break;
                            }
                            break;

                        case Database.COMMON_RAINBOW_TUBE:
                            switch (AP.Math.RandomInteger(4))
                            {
                                case 0:
                                    for (int ii = 0; ii < groupEnemy.Count; ii++)
                                    {
                                        effectValue = PrimaryLogic.RainbowTubeValue_A(player, GroundOne.DuelMode);
                                        AbstractMagicDamage(player, groupEnemy[ii], 0, effectValue, 0, Database.SOUND_VOLCANICWAVE, 12, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                    }
                                    break;
                                case 1:
                                    for (int ii = 0; ii < groupAlly.Count; ii++)
                                    {
                                        effectValue = PrimaryLogic.RainbowTubeValue_B(player, GroundOne.DuelMode);
                                        PlayerAbstractLifeGain(player, groupAlly[ii], 0, PrimaryLogic.SacredHealValue(player, GroundOne.DuelMode), 0, Database.SOUND_CELESTIAL_NOVA, 135);
                                    }
                                    break;
                                case 2:
                                    for (int ii = 0; ii < groupEnemy.Count; ii++)
                                    {
                                        NowStunning(player, groupEnemy[ii], 2, false);
                                    }
                                    break;
                                case 3:
                                    for (int ii = 0; ii < groupAlly.Count; ii++)
                                    {
                                        groupAlly[ii].RemovePhysicalAttackDown();
                                        groupAlly[ii].RemovePhysicalDefenseDown();
                                        groupAlly[ii].RemoveMagicAttackDown();
                                        groupAlly[ii].RemoveMagicDefenseDown();
                                        groupAlly[ii].RemoveSpeedDown();
                                        groupAlly[ii].RemoveReactionDown();
                                        groupAlly[ii].RemovePotentialDown();
                                        groupAlly[ii].RemoveLightDown();
                                        groupAlly[ii].RemoveShadowDown();
                                        groupAlly[ii].RemoveFireDown();
                                        groupAlly[ii].RemoveIceDown();
                                        groupAlly[ii].RemoveForceDown();
                                        groupAlly[ii].RemoveWillDown();

                                        // DEBUFF効果というカテゴリに負の影響効果も含まれるのかどうかがハッキリしない。仕様再検討必要だが
                                        // これが含まれていないと、「ユーザー観点として使えないアイテム」の烙印が押されるので、
                                        // 使えるアイテムとして含めることとしたい
                                        groupAlly[ii].RemovePreStunning();
                                        groupAlly[ii].RemoveStun();
                                        groupAlly[ii].RemoveSilence();
                                        groupAlly[ii].RemovePoison();
                                        groupAlly[ii].RemoveTemptation();
                                        groupAlly[ii].RemoveFrozen();
                                        groupAlly[ii].RemoveParalyze();
                                        groupAlly[ii].RemoveNoResurrection();
                                        groupAlly[ii].RemoveSlow();
                                        groupAlly[ii].RemoveBlind();
                                        groupAlly[ii].RemoveSlip();
                                    }
                                    break;
                            }
                            break;

                        case Database.RARE_DETACHMENT_ORB:
                            if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_DETACHMENT_ORB) && (player.Accessory.OnlyOnce == false))
                            {
                                player.Accessory.OnlyOnce = true;
                                for (int ii = 0; ii < groupAlly.Count; ii++)
                                {
                                    PlayerBuffAbstract(player, groupAlly[ii], 2, Database.ITEMCOMMAND_DETACHMENT_ORB);
                                }
                            }
                            else if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_DETACHMENT_ORB) && (player.Accessory2.OnlyOnce == false))
                            {
                                player.Accessory2.OnlyOnce = true;
                                for (int ii = 0; ii < groupAlly.Count; ii++)
                                {
                                    PlayerBuffAbstract(player, groupAlly[ii], 2, Database.ITEMCOMMAND_DETACHMENT_ORB);
                                }
                            }
                            else
                            {
                                UpdateBattleText(Database.RARE_DETACHMENT_ORB + "は既に使用されており、効力が発揮されなかった！\r\n");
                            }
                            break;

                        case Database.RARE_DEVIL_SUMMONER_TOME:
                            PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_DEVIL_SUMMONER_TOME);
                            break;

                        case Database.RARE_VOID_HYMNSONIA:
                            UpdateBattleText(player.FirstName + "は、空虚な歌声を必死に取り払い、戦闘に集中した！\r\n");
                            player.RemoveVoidHymnsonia();
                            break;

                        case Database.EPIC_ETERNAL_HOMURA_RING:
                            UpdateBattleText(player.FirstName + "が装着している" + Database.EPIC_ETERNAL_HOMURA_RING + "が強烈な蒼白い閃光を放った！\r\n");
                            effectValue = player.CurrentMana;
                            player.CurrentMana = 0;
                            UpdateMana(player, (int)effectValue, false, false, 0);
                            AbstractMagicDamage(player, target, 0, PrimaryLogic.EternalHomuraRingValue(player, GroundOne.DuelMode), 0, Database.SOUND_LAVA_ANNIHILATION, 0, TruthActionCommand.MagicType.None, false, CriticalType.Random);
                            break;
                    }
                    break;
                #endregion

                #region "敵専用スキル"
                case MainCharacter.PlayerAction.SpecialSkill:
                    if (!player.Target.Dead)
                    {
                        switch (player.FirstName)
                        {
                            // 「警告」抽象化していくべきではないだろうか？
                            #region "１階の敵"
                            case Database.ENEMY_KOUKAKU_WURM:
                                UpdateBattleText(player.FirstName + "は硬い甲殻部分を丸めて突進してきた！\r\n");
                                PlayerNormalAttack(player, target, 2.0f, false, true);
                                break;

                            case Database.ENEMY_HIYOWA_BEATLE:
                                effectValue = 4.0F;
                                UpdateBattleText(player.FirstName + "は尖った角を掲げ上げた！ 【力】" + effectValue.ToString() + "上昇\r\n");
                                player.CurrentStrengthUp = Database.INFINITY;
                                player.CurrentStrengthUpValue = (int)effectValue;
                                player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                                break;

                            case Database.ENEMY_GREEN_CHILD:
                                UpdateBattleText(player.FirstName + "はグリーン・スプラッシュを唱えた！\r\n");
                                PlayerMagicAttack(player, target, 0, 2.0f);
                                break;

                            case Database.ENEMY_MANDRAGORA:
                                UpdateBattleText(player.FirstName + "は奇妙な悲鳴を上げてきた！\r\n");
                                PlayerMagicAttack(player, target, 0, 3.0f);
                                break;

                            case Database.ENEMY_RED_HOPPER:
                                UpdateBattleText(player.FirstName + "の群れは一斉に攻撃を仕掛けてきた！\r\n");
                                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 20, string.Empty, -1, true, CriticalType.None);
                                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 20, string.Empty, -1, true, CriticalType.None);
                                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 20, string.Empty, -1, true, CriticalType.None);
                                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 20, string.Empty, -1, true, CriticalType.None);
                                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 20, string.Empty, -1, true, CriticalType.None);
                                break;

                            case Database.ENEMY_EARTH_SPIDER:
                                UpdateBattleText(player.FirstName + "は蜘蛛の糸を絡み付かせてきた！\r\n");
                                NowSlow(player, target, 2);
                                break;

                            case Database.ENEMY_ALRAUNE:
                                if (player.ActionLabel.text == "怪しげな花弁")
                                {
                                    UpdateBattleText(player.FirstName + "は怪しげな花弁をかざしてきた！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    NowTemptation(player, target, 3);
                                }
                                else if (player.ActionLabel.text == "猛毒の花粉")
                                {
                                    UpdateBattleText(player.FirstName + "は猛毒の花粉をまき散らしてきた！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    NowPoison(player, target, 4, false);
                                }
                                break;

                            case Database.ENEMY_POISON_MARY:
                                if (player.ActionLabel.text == "毒胞子")
                                {
                                    UpdateBattleText(player.FirstName + "は猛毒の花粉をまき散らしてきた！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    NowPoison(player, target, 4, false);
                                }
                                else if (player.ActionLabel.text == "幻覚胞子")
                                {
                                    UpdateBattleText(player.FirstName + "は幻覚胞子を飛ばしてきた！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    NowBlind(player, target, 4);
                                }
                                break;

                            case Database.ENEMY_SPEEDY_TAKA:
                                UpdateBattleText(player.FirstName + "は２回連続で攻撃を繰り出してきた！\r\n");
                                PlayerNormalAttack(player, target, 0, false, true);
                                System.Threading.Thread.Sleep(100);
                                PlayerNormalAttack(player, target, 0, false, true);
                                System.Threading.Thread.Sleep(100);
                                break;
                            case Database.ENEMY_ZASSYOKU_RABBIT:
                                if (player.ActionLabel.text == Database.BUFFUP_STRENGTH)
                                {
                                    effectValue = 30.0F;
                                    UpdateBattleText(player.FirstName + "は体全体を奮い立たせた！ 【力】" + effectValue.ToString() + "上昇\r\n");
                                    player.CurrentStrengthUp = Database.INFINITY;
                                    player.CurrentStrengthUpValue = (int)effectValue;
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                                }
                                else
                                {
                                    UpdateBattleText(player.FirstName + "は勢い良く突進してきた！\r\n");
                                    PlayerNormalAttack(player, target, 3.0f, false, true);
                                }
                                break;

                            case Database.ENEMY_WONDER_SEED:
                                if (player.ActionLabel.text == "棘殻ローリング")
                                {
                                    UpdateBattleText(player.FirstName + "は棘のある外殻を丸めて体当たりしてきた！\r\n");
                                    if (GroundOne.MC.Dead == false)
                                    {
                                        PlayerNormalAttack(player, GroundOne.MC, 0, false, true);
                                        System.Threading.Thread.Sleep(100);
                                    }
                                    if (GroundOne.SC != null)
                                    {
                                        if (GroundOne.SC.Dead == false)
                                        {
                                            PlayerNormalAttack(player, GroundOne.SC, 0, false, true);
                                            System.Threading.Thread.Sleep(100);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ニードル・スピア")
                                {
                                    UpdateBattleText(player.FirstName + "はニードル・スピアを放ってきた！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, false, true))
                                    {
                                        NowSlow(player, target, 2);
                                    }
                                }
                                else if (player.ActionLabel.text == "連続攻撃")
                                {
                                    UpdateBattleText(player.FirstName + "は２回連続で攻撃を繰り出してきた！\r\n");
                                    PlayerNormalAttack(player, target, 0, false, true);
                                    System.Threading.Thread.Sleep(100);
                                    PlayerNormalAttack(player, target, 0, false, true);
                                    System.Threading.Thread.Sleep(100);
                                }
                                break;
                            case Database.ENEMY_FLANSIS_KNIGHT:
                                if (player.ActionLabel.text == "なぎ払い")
                                {
                                    UpdateBattleText(player.FirstName + "は細長い蔦槍を一気に横になぎ払ってきた！\r\n");
                                    if (GroundOne.MC.Dead == false)
                                    {
                                        PlayerNormalAttack(player, GroundOne.MC, 0, false, true);
                                        System.Threading.Thread.Sleep(100);
                                    }
                                    if (GroundOne.SC != null)
                                    {
                                        if (GroundOne.SC.Dead == false)
                                        {
                                            PlayerNormalAttack(player, GroundOne.SC, 0, false, true);
                                            System.Threading.Thread.Sleep(100);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ファイア・ランス")
                                {
                                    UpdateBattleText(player.FirstName + "は炎を燈した蔦槍を投げつけてきた！\r\n");
                                    PlayerNormalAttack(player, target, 0, false, true);
                                    System.Threading.Thread.Sleep(100);
                                    double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
                                    AbstractMagicDamage(player, target, 0, ref damage, 0, "FlameStrike", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                    System.Threading.Thread.Sleep(100);
                                }
                                break;

                            case Database.ENEMY_SHOTGUN_HYUI:
                                if (player.ActionLabel.text == "ヒューイ弾丸")
                                {
                                    UpdateBattleText(player.FirstName + "は高速の弾丸を撒き散らしてきた！\r\n");

                                    for (int ii = 0; ii < 5; ii++)
                                    {
                                        int tempRandom = AP.Math.RandomInteger(2);
                                        if (tempRandom == 0)
                                        {
                                            PlayerNormalAttack(player, GroundOne.MC, 0, false, true);
                                        }
                                        else
                                        {
                                            PlayerNormalAttack(player, GroundOne.SC, 0, false, true);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ホウセンの種")
                                {
                                    UpdateBattleText(player.FirstName + "はホウセンの種を周囲へ撒き散らした！\r\n");
                                    if (GroundOne.MC.Dead == false)
                                    {
                                        if (PlayerNormalAttack(player, GroundOne.MC, 0, false, true))
                                        {
                                            NowSlow(player, GroundOne.MC, 2);
                                        }
                                        System.Threading.Thread.Sleep(100);
                                    }
                                    if (GroundOne.SC != null)
                                    {
                                        if (GroundOne.SC.Dead == false)
                                        {
                                            if (PlayerNormalAttack(player, GroundOne.SC, 0, false, true))
                                            {
                                                NowSlow(player, GroundOne.SC, 2);
                                            }
                                            System.Threading.Thread.Sleep(100);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == Database.BUFFUP_STRENGTH)
                                {
                                    effectValue = 30.0F;
                                    UpdateBattleText(player.FirstName + "は自己発芽を活性させ始めた！ 【力】" + effectValue.ToString() + "上昇\r\n");
                                    player.CurrentStrengthUp = Database.INFINITY;
                                    player.CurrentStrengthUpValue = (int)effectValue;
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                                }
                                break;

                            case Database.ENEMY_BRILLIANT_BUTTERFLY:
                                UpdateBattleText(player.FirstName + "はフラッシュ・ウィンドを唱えた！\r\n");
                                PlayerMagicAttack(player, target, 0, 2.0f);
                                break;
                            case Database.ENEMY_WAR_WOLF:
                                UpdateBattleText(player.FirstName + "は鋭い勢いで突進してきた！\r\n");
                                PlayerNormalAttack(player, target, 3.0f, false, true);
                                break;

                            case Database.ENEMY_BLOOD_MOSS:
                                UpdateBattleText(player.FirstName + "は赤い胞子をばら撒いてきた！\r\n");
                                if (GroundOne.MC.Dead == false)
                                {
                                    PlayerNormalAttack(player, GroundOne.MC, 0, false, true);
                                    System.Threading.Thread.Sleep(100);
                                }
                                if (GroundOne.SC != null)
                                {
                                    if (GroundOne.SC.Dead == false)
                                    {
                                        PlayerNormalAttack(player, GroundOne.SC, 0, false, true);
                                        System.Threading.Thread.Sleep(100);
                                    }
                                }
                                break;

                            case Database.ENEMY_MOSSGREEN_DADDY:
                                UpdateBattleText(player.FirstName + "は根の蔦を地上から一気に仕掛けてきた！\r\n");
                                if (GroundOne.MC.Dead == false)
                                {
                                    if (PlayerNormalAttack(player, GroundOne.MC, 0, false, true))
                                    {
                                        NowSlow(player, GroundOne.MC, 2);
                                    }
                                    System.Threading.Thread.Sleep(100);
                                }
                                if (GroundOne.SC != null)
                                {
                                    if (GroundOne.SC.Dead == false)
                                    {
                                        if (PlayerNormalAttack(player, GroundOne.SC, 0, false, true))
                                        {
                                            NowSlow(player, GroundOne.SC, 2);
                                        }
                                        System.Threading.Thread.Sleep(100);
                                    }
                                }

                                break;

                            case Database.ENEMY_BOSS_KARAMITUKU_FLANSIS:
                                if (player.StackCommandString == "キル・スピニングランサー")
                                {
                                    ((TruthEnemyCharacter)player).BossBeforeStay = false;
                                    UpdateBattleText(player.FirstName + "は巨大なイバラの槍を形成し始めた！\r\n");
                                    UpdateBattleText(player.FirstName + ":ッシャアアアァァァ！！！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    UpdateBattleText(ec1.FirstName + "「奥義　キル・スピニングランサー」発動！！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    double damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode);
                                    PlayerNormalAttack(player, target, 8.0f, 0, false, true, 0, 0, string.Empty, -1, true, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "絡み蔦")
                                {
                                    UpdateBattleText(player.FirstName + "は絡み蔦を忍ばせてきた！！\r\n");
                                    if (GroundOne.MC.Dead == false)
                                    {
                                        if (PlayerNormalAttack(player, GroundOne.MC, 0, false, true))
                                        {
                                            NowSlow(player, GroundOne.MC, 3);
                                        }
                                    }
                                    if (GroundOne.SC != null)
                                    {
                                        if (GroundOne.SC.Dead == false)
                                        {
                                            if (PlayerNormalAttack(player, GroundOne.SC, 0, false, true))
                                            {
                                                NowSlow(player, GroundOne.SC, 3);
                                            }
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "黒の毒胞子")
                                {
                                    UpdateBattleText(player.FirstName + "は黒色の毒胞子をばらまいてきた！！\r\n");
                                    if (GroundOne.MC.Dead == false)
                                    {
                                        NowPoison(player, GroundOne.MC, 999, true);
                                        NowBlind(player, GroundOne.MC, 4);
                                    }
                                    if (GroundOne.SC != null)
                                    {
                                        if (GroundOne.SC.Dead == false)
                                        {
                                            NowPoison(player, GroundOne.SC, 999, true);
                                            NowBlind(player, GroundOne.SC, 4);
                                        }
                                    }

                                }
                                else if (player.ActionLabel.text == "レッドローズブラスト")
                                {
                                    UpdateBattleText(player.FirstName + "の一輪の赤花より火炎が放たれる！！\r\n");
                                    double damage = PrimaryLogic.FireBallValue(player, false);
                                    AbstractMagicDamage(player, target, 0, ref damage, 1.5f, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                }
                                else if ((player.StackCommandString == "ファイアビューネ") ||
                                         (player.ActionLabel.text == "ファイアビューネ"))
                                {
                                    UpdateBattleText(player.FirstName + "は複数の蔦の先に炎を宿らせて放ってきた！！\r\n");

                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 2;

                                        int tempRandom = AP.Math.RandomInteger(2);
                                        if (tempRandom == 0)
                                        {
                                            if (GroundOne.MC.Dead == false)
                                            {
                                                AbstractMagicDamage(player, GroundOne.MC, 10, ref damage, 0, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                            }
                                            else
                                            {
                                                AbstractMagicDamage(player, GroundOne.SC, 10, ref damage, 0, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                            }
                                        }
                                        else
                                        {
                                            if (GroundOne.WE.AvailableSecondCharacter)
                                            {
                                                if (GroundOne.SC.Dead == false)
                                                {
                                                    AbstractMagicDamage(player, GroundOne.SC, 10, ref damage, 0, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                                }
                                                else
                                                {
                                                    AbstractMagicDamage(player, GroundOne.MC, 10, ref damage, 0, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                                }
                                            }
                                            else
                                            {
                                                AbstractMagicDamage(player, GroundOne.MC, 10, ref damage, 0, "FireBall", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                            }
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "連槍突進")
                                {
                                    UpdateBattleText(player.FirstName + "は複数の蔦槍を構えて突進してきた！\r\n");
                                    PlayerNormalAttack(player, target, 0, false, true);
                                    System.Threading.Thread.Sleep(70);
                                    PlayerNormalAttack(player, target, 0, false, true);
                                    System.Threading.Thread.Sleep(70);
                                    PlayerNormalAttack(player, target, 0, false, true);
                                }

                                break;
                            #endregion
                            #region "２階の敵"
                            case Database.ENEMY_DAGGER_FISH:
                                UpdateBattleText(player.FirstName + "は見境なく噛み付いてきた！\r\n");
                                PlayerRandomTargetPhysicalDamage(player, 6, 20, 0);
                                break;

                            case Database.ENEMY_SIPPU_FLYING_FISH:
                                UpdateBattleText(player.FirstName + "は２回連続で攻撃を繰り出してきた！\r\n");
                                PlayerNormalAttack(player, target, 0, false, true);
                                System.Threading.Thread.Sleep(100);
                                PlayerNormalAttack(player, target, 0, false, true);
                                System.Threading.Thread.Sleep(100);
                                break;

                            case Database.ENEMY_SPLASH_KURIONE:
                                if (player.ActionLabel.text == "透明な光")
                                {
                                    UpdateBattleText(player.FirstName + "は目に見えない光を発生させてきた！\r\n");
                                    if (AbstractMagicDamage(player, target, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 0, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.Light, false, CriticalType.Random))
                                    {
                                        NowBlind(player, target, 5);
                                    }
                                }
                                else if (player.ActionLabel.text == "共鳴波")
                                {
                                    UpdateBattleText(player.FirstName + "は耳に聞こえない音波を発してきた！\r\n");
                                    if (AbstractMagicDamage(player, target, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 0, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.Will, false, CriticalType.Random))
                                    {
                                        NowSilence(player, target, 5);
                                    }
                                }
                                break;

                            case Database.ENEMY_ROLLING_MAGURO:
                                if (player.ActionLabel.text == "捕獲選定")
                                {
                                    UpdateBattleText(player.FirstName + "はローリング突進するターゲットを選び始めた！\r\n");
                                    if (GroundOne.SC != null && !GroundOne.SC.Dead)
                                    {
                                        player.Target = GroundOne.SC;
                                    }
                                    else if (GroundOne.TC != null && !GroundOne.TC.Dead)
                                    {
                                        player.Target = GroundOne.TC;
                                    }
                                    else
                                    {
                                        player.Target = GroundOne.MC;
                                    }
                                }
                                else if (player.ActionLabel.text == "ローリング突進")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に猛回転しながら突進してきた！\r\n");
                                    PlayerNormalAttack(player, target, 2.0F, true, false);
                                }
                                break;

                            case Database.ENEMY_RANBOU_SEA_ARTINE:
                                if (player.ActionLabel.text == "トゲの放射")
                                {
                                    UpdateBattleText(player.FirstName + "は大きなトゲを射出してきた！\r\n");
                                    PlayerRandomTargetPhysicalDamage(player, 6, 20, 0);
                                }
                                else if (player.ActionLabel.text == "表面膨張")
                                {
                                    UpdateBattleText(player.FirstName + "は鱗の表面を膨張させた！\r\n");
                                    // 物理攻撃UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
                                    effectValue = 250.0F;
                                    player.CurrentPhysicalAttackUp = 3;
                                    player.CurrentPhysicalAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", 3);
                                }
                                break;

                            case Database.ENEMY_BLUE_SEA_WASI:
                                if (player.ActionLabel.text == "金切り声")
                                {
                                    UpdateBattleText(player.FirstName + "はキリキリした声を全体へ発生させてきた！\r\n");
                                    // 全体ダメージ
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, Database.SOUND_MAGIC_ATTACK, TruthActionCommand.MagicType.Will);
                                }
                                break;

                            case Database.ENEMY_GANGAME:
                                if (player.ActionLabel.text == "地響き")
                                {
                                    UpdateBattleText(player.FirstName + "は大きく地面を鳴らしてきた！\r\n");
                                    // 全体ダメージ
                                    PlayerPhysicalAttackAllEnemy(player, PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode) * 2.0F, Database.SOUND_ENEMY_ATTACK1);
                                    // 全体鈍化
                                    PlayerSlowAllEnemy(player, 3);
                                }
                                else if (player.ActionLabel.text == "タートル・シェル")
                                {
                                    UpdateBattleText(player.FirstName + "は首を引っ込めて、厚い甲羅を強化してきた！\r\n");
                                    // 物理防御UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理防御UP");
                                    effectValue = 300.0F;
                                    player.CurrentPhysicalDefenseUp = 3;
                                    player.CurrentPhysicalDefenseUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + "BuffPhysicalDefenseUp", 3);
                                }
                                else if (player.ActionLabel.text == "がぶりつき")
                                {
                                    UpdateBattleText(player.FirstName + "は首を伸ばして、がぶりついてきた！\r\n");
                                    PlayerNormalAttack(player, target, 3.0F, false, false);
                                }
                                break;

                            case Database.ENEMY_BIGMOUSE_JOE:
                                if (player.ActionLabel.text == "伸張する舌")
                                {
                                    UpdateBattleText(player.FirstName + "は舌をベロリと伸ばしてきた。\r\n");
                                    // 技UP
                                    effectValue = 300.0F;

                                    AnimationDamage(0, player, 0, Color.black, false, false, "技UP");
                                    player.CurrentAgilityUp = 3;
                                    player.CurrentAgilityUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbAgilityUp, Database.BaseResourceFolder + "BuffAgilityUp", 3);

                                    UpdateBattleText(player.FirstName + "は【技】が" + effectValue.ToString() + "上昇\r\n");
                                }
                                else if (player.ActionLabel.text == "トリプル・パンチ")
                                {
                                    UpdateBattleText(player.FirstName + "のトリプル・パンチが炸裂！\r\n");
                                    // ３回攻撃
                                    PlayerNormalAttack(player, target, 1.5F, false, false);
                                    PlayerNormalAttack(player, target, 2.0F, false, false);
                                    PlayerNormalAttack(player, target, 3.0F, false, false);
                                }
                                else if (player.ActionLabel.text == "異常な奇声")
                                {
                                    UpdateBattleText(player.FirstName + "は異常なまでの奇声を発してきた！\r\n");

                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        // 単体スタン
                                        NowStunning(player, target, 1, false);
                                    }
                                }
                                break;

                            case Database.ENEMY_MOGURU_MANTA:
                                if (player.ActionLabel.text == "流水の渦巻き")
                                {
                                    UpdateBattleText(player.FirstName + "は水の渦巻きを発生させてきた！\r\n");
                                    AbstractMagicDamage(player, target, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 4.0F, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "流水の突壁")
                                {
                                    UpdateBattleText(player.FirstName + "は水の障壁を発生させてきた！\r\n");
                                    PlayerMirrorImageAllAlly(player);
                                }
                                break;

                            case Database.ENEMY_FLOATING_GOLD_FISH:
                                if (player.ActionLabel.text == "鉄砲泡")
                                {
                                    UpdateBattleText(player.FirstName + "は口を鉄砲型にして高速の泡を吹いてきた！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, false, false))
                                    {
                                        NowBlind(player, target, 3);
                                    }
                                }
                                else if (player.ActionLabel.text == "水面跳躍")
                                {
                                    UpdateBattleText(player.FirstName + "は踊るような跳びはね方をしてきた！\r\n");
                                    // スピードUP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "戦闘速度UP");
                                    player.CurrentSpeedUp = 2;
                                    player.CurrentSpeedUpValue = (int)250;
                                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, 2);
                                }
                                break;

                            case Database.ENEMY_GOEI_HERMIT_CLUB:
                                if (player.ActionLabel.text == "豪腕ハサミ")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に強引にハサミを向けてきた！\r\n");
                                    PlayerNormalAttack(player, target, 2.0F, true, false);
                                }
                                else if (player.ActionLabel.text == "突進バサミ")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に突進しながらハサミで突いてきた！\r\n");
                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        // 単体スタン
                                        NowStunning(player, target, 1, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "ダブル・ハサミ")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に両方のハサミで攻撃してきた！\r\n");
                                    PlayerNormalAttack(player, target, 1.5F, false, false);
                                    PlayerNormalAttack(player, target, 1.5F, false, false);
                                }
                                break;

                            case Database.ENEMY_VANISHING_CORAL:
                                if (player.ActionLabel.text == "コーラル・サウンド")
                                {
                                    UpdateBattleText(player.FirstName + "は独特のサウンドを発生させてきた！\r\n");
                                    AbstractMagicDamage(player, target, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 4.0F, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "バニッシング・エフェクト")
                                {
                                    UpdateBattleText(player.FirstName + "は自分の姿を透明化させてきた！\r\n");
                                    // 物理防御UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理防御UP");
                                    effectValue = 9999.9F;
                                    player.CurrentPhysicalDefenseUp = 1;
                                    player.CurrentPhysicalDefenseUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, 1);
                                    // 魔法防御UP
                                    AnimationDamage(0, player.Target2, 0, Color.black, false, false, "魔法防御UP");
                                    player.Target2.CurrentMagicDefenseUp = 1;
                                    player.Target2.CurrentMagicDefenseUpValue = 9999;
                                    player.Target2.ActivateBuff(player.Target2.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, 1);

                                }
                                else if (player.ActionLabel.text == "ラスト・バウンド")
                                {
                                    UpdateBattleText(player.FirstName + "は奇妙な体制から突然爆発してきた！\r\n");
                                    PlayerMagicAttack(player, target, 0, 20.0F);
                                }
                                break;

                            case Database.ENEMY_CASSY_CANCER:
                                if (player.ActionLabel.text == "ベタつく緑泡")
                                {
                                    UpdateBattleText(player.FirstName + "はベタつく緑色の泡を周囲に吐き出してきた。\r\n");
                                    PlayerAllBlind(player);
                                }
                                else if (player.ActionLabel.text == "甲殻増強")
                                {
                                    UpdateBattleText(player.FirstName + "は自らの甲殻を増強してきた！\r\n");
                                    // 物理防御UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理防御UP");
                                    effectValue = 9999.9F;
                                    player.CurrentPhysicalDefenseUp = 10;
                                    player.CurrentPhysicalDefenseUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, 10);
                                }
                                else if (player.ActionLabel.text == "キャンサー・ブロー")
                                {
                                    UpdateBattleText(player.FirstName + "のキャンサー・ブローが炸裂！\r\n");
                                    PlayerNormalAttack(player, target, 5.0F, false, false);
                                }
                                break;

                            case Database.ENEMY_EDGED_HIGH_SHARK:
                                if (player.ActionLabel.text == "猛突撃")
                                {
                                    UpdateBattleText(player.FirstName + "は見境なく激しい突進をしてきた！\r\n");
                                    PlayerRandomTargetPhysicalDamage(player, 1, 0, 5.0F);
                                }
                                else if (player.ActionLabel.text == "貪欲な咬みつき")
                                {
                                    UpdateBattleText(player.FirstName + "はそこら中を対象にして噛み付いてきた！\r\n");
                                    PlayerRandomTargetPhysicalDamage(player, 10, 20, 1.2F);
                                }
                                else if (player.ActionLabel.text == "ヴァイオレンス・テール")
                                {
                                    UpdateBattleText(player.FirstName + "はバカでかい尾を使って、周囲に当たり散らしてきた！\r\n");
                                    PlayerPhysicalAttackAllEnemy(player, 3.0F * PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 1.0F, 0.0F, 0.0F, 0.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode), Database.SOUND_ENEMY_ATTACK1);
                                }
                                break;

                            case Database.ENEMY_EIGHT_EIGHT:
                                if (player.ActionLabel.text == "【八】はがい絞め")
                                {
                                    UpdateBattleText(player.FirstName + "は８つの足をグルリと回し、絡ませてきた！\r\n");
                                    NowBlind(player, target, 2);
                                    NowSlow(player, target, 4);
                                    NowStunning(player, target, 2, false);

                                    PlayerNormalAttack(player, target, 0.0f, false, false);
                                }
                                else if (player.ActionLabel.text == "ブチ巻く黒墨")
                                {
                                    UpdateBattleText(player.FirstName + "は真っ黒い墨を自分自身にブチ巻けた\r\n");
                                    effectValue = 250.0F;
                                    AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃UP");
                                    player.CurrentMagicAttackUp = Database.INFINITY;
                                    player.CurrentMagicAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, Database.INFINITY);
                                }
                                else if (player.ActionLabel.text == "黒墨ミサイル")
                                {
                                    UpdateBattleText(player.FirstName + "は墨をミサイル形状化させて放ってきた！\r\n");
                                    AbstractMagicDamage(player, target, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 0.0F, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "大渦巻き")
                                {
                                    UpdateBattleText(player.FirstName + "は巨大な渦巻きを周囲に発生させてきた！\r\n");
                                    PlayerPhysicalAttackAllEnemy(player, 3.0F * PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode), Database.SOUND_ENEMY_ATTACK1);
                                }
                                break;


                            case Database.ENEMY_BRILLIANT_SEA_PRINCE:
                                if (player.ActionLabel.text == "シースライドウォータ")
                                {
                                    UpdateBattleText(player.FirstName + "はシースライドウォータを発動した！\r\n");
                                    effectValue = 200.0f;

                                    // 魔法攻撃UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃UP");
                                    player.CurrentMagicAttackUp = 3;
                                    player.CurrentMagicAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, 3);
                                    // スピードUP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "戦闘速度UP");
                                    player.CurrentSpeedUp = 3;
                                    player.CurrentSpeedUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, 3);

                                    UpdateBattleText(player.FirstName + "は【魔法攻撃】【戦闘速度】が" + effectValue.ToString() + "上昇\r\n");
                                }
                                else if (player.ActionLabel.text == "勇敢な雄叫び")
                                {
                                    UpdateBattleText(player.FirstName + "は勇敢な雄叫びを上げた！\r\n");

                                    effectValue = 160.0F;

                                    AnimationDamage(0, player, 0, Color.black, false, false, "力UP");
                                    player.CurrentStrengthUp = 3;
                                    player.CurrentStrengthUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", 3);

                                    AnimationDamage(0, player, 0, Color.black, false, false, "心UP");
                                    player.CurrentMindUp = 3;
                                    player.CurrentMindUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbMindUp, Database.BaseResourceFolder + "BuffMindUp", 3);

                                    UpdateBattleText(player.FirstName + "は【力】と【心】が" + effectValue.ToString() + "上昇\r\n");
                                }
                                else if ((player.StackCommandString == "アイソニック・ウェイヴ") ||
                                         (player.ActionLabel.text == "アイソニック・ウェイヴ"))
                                {
                                    UpdateBattleText(player.FirstName + "はアイソニック・ウェイヴを放ってきた！\r\n");
                                    // 全体ダメージ
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, Database.SOUND_MAGIC_ATTACK, TruthActionCommand.MagicType.Ice);
                                }
                                else if (player.ActionLabel.text == "グングニル・スラッシュ")
                                {
                                    UpdateBattleText(player.FirstName + "：食らえ、グングニルの力！　ッヤアァァァ！！\r\n");
                                    // 物理攻撃
                                    double damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode);
                                    PlayerNormalAttack(player, target, 10.0f, 0, false, true, 0, 0, string.Empty, -1, true, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "グングニルの閃光")
                                {
                                    UpdateBattleText(player.FirstName + "：光輝け、グングニル！　ッハアァァァ！！\r\n");

                                    // 魔法攻撃
                                    double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
                                    PlayerMagicAttack(player, target, 0, 10.0f);
                                }
                                break;

                            case Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN:
                                if (player.StackCommandString == "エレメンタル・スプラッシュ")
                                {
                                    UpdateBattleText(player.FirstName + "は宙にウォータエレメンタルを飛ばした！\r\n");
                                    // 全体ダメージ
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, Database.SOUND_MAGIC_ATTACK, TruthActionCommand.MagicType.Ice);
                                }
                                else if ((player.ActionLabel.text == "生命の龍水") ||
                                         (player.StackCommandString == "生命の龍水"))
                                {
                                    UpdateBattleText(player.FirstName + "は生命の龍水を体中に浴びた！\r\n");
                                    // ライフ回復
                                    effectValue = player.Intelligence * 4;
                                    PlayerAbstractLifeGain(player, player, 0, effectValue, 0, "FreshHeal", 5002);
                                }
                                else if ((player.ActionLabel.text == "サルマンの詠唱") ||
                                         (player.StackCommandString == "サルマンの詠唱"))
                                {
                                    UpdateBattleText(player.FirstName + "はサルマン神に対する誓いの詠唱を謡った！\r\n");
                                    PlayerSpellAbsorbWater(player, player);
                                    PlayerSpellMirrorImage(player, player);
                                }
                                else if ((player.ActionLabel.text == "アンダートの詠唱") ||
                                         (player.StackCommandString == "アンダートの詠唱"))
                                {
                                    UpdateBattleText(player.FirstName + "はアンダート神に対する誓いの詠唱を謡った！\r\n");
                                    // 物理防御UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理防御UP");
                                    effectValue = 9999.9F;
                                    player.CurrentPhysicalDefenseUp = 3;
                                    player.CurrentPhysicalDefenseUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + "BuffPhysicalDefenseUp", 3);
                                }
                                break;

                            case Database.ENEMY_SHELL_SWORD_KNIGHT:
                                if (player.StackCommandString == "ジュエル・ブレイク")
                                {
                                    UpdateBattleText(player.FirstName + "はジュエル・ブレイクを放った！\r\n");

                                    if (PlayerNormalAttack(player, player.StackTarget, 2.0F, true, false))
                                    {
                                        PlayerSpellDispelMagic(player, player.StackTarget);
                                    }
                                }
                                else if (player.ActionLabel.text == "ブリンク・シェル")
                                {
                                    UpdateBattleText(player.FirstName + "はブリンク・シェルを発動した！\r\n");
                                    // 次のターン、100必殺
                                    PlayerSpellWordOfFortune(player, player);
                                }
                                else if (player.ActionLabel.text == "シー・ストライプ")
                                {
                                    if (PlayerNormalAttack(player, target, 1.0F, false, false))
                                    {
                                        player.CurrentSpeedBoost = 75;
                                    }
                                }
                                else if (player.ActionLabel.text == "深海の渦")
                                {
                                    UpdateBattleText(player.FirstName + "は深海の渦を発生させた！\r\n");
                                    // 全体鈍化
                                    PlayerSlowAllEnemy(player, 10);
                                }
                                else if (player.ActionLabel.text == "海星源への忠誠")
                                {
                                    UpdateBattleText(player.FirstName + "：海星源。永遠なる王のため！！\r\n");
                                    // 物理攻撃UP
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
                                    effectValue = 550.0F;
                                    player.CurrentPhysicalAttackUp = 3;
                                    player.CurrentPhysicalAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", 3);
                                }
                                break;

                            case Database.ENEMY_JELLY_EYE_BRIGHT_RED:
                                if (player.StackCommandString == "溶岩の一撃")
                                {
                                    AbstractMagicDamage(player, player.StackTarget, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 1.5F, "FlameStrike", 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "燃え盛る炎弾丸")
                                {
                                    UpdateBattleText(player.FirstName + "は燃え盛る炎の弾丸を吐いてきた！\r\n");

                                    PlayerRandomTargetDamage(player, 5, "FireBall", TruthActionCommand.MagicType.Fire);
                                }
                                else if (player.ActionLabel.text == "ブレイジング・ストーム")
                                {
                                    UpdateBattleText(player.FirstName + "の眼が大きくなる！ブレイジング・ストームを放射してきた！\r\n");

                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, "FlameStrike", TruthActionCommand.MagicType.Fire);
                                }
                                else if (player.ActionLabel.text == "ファイア・ウォール")
                                {
                                    // 蒼目の方に火炎耐性を付ける。
                                    UpdateBattleText(player.FirstName + "はファイア・ウォールを発生させた！\r\n");

                                    if (ec2 != null && !ec2.Dead)
                                    {
                                        ec2.CurrentResistFireUp = 10;
                                        ec2.CurrentResistFireUpValue = 2000;
                                        ec2.ActivateBuff(ec2.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", 10);
                                    }
                                    else if (ec1 != null && !ec1.Dead)
                                    {
                                        ec1.CurrentResistFireUp = 10;
                                        ec1.CurrentResistFireUpValue = 2000;
                                        ec1.ActivateBuff(ec1.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", 10);
                                    }
                                }
                                else if (player.ActionLabel.text == "フラッシュ・バーン")
                                {
                                    UpdateBattleText(player.FirstName + "の目が一瞬、白い閃光を放ってきた！\r\n");

                                    PlayerAllSilence(player);
                                }
                                break;

                            case Database.ENEMY_JELLY_EYE_DEEP_BLUE:
                                if (player.StackCommandString == "凍雹の一撃")
                                {
                                    AbstractMagicDamage(player, player.StackTarget, 0, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode), 1.5F, "FrozenLance", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "凍てつく氷弾丸")
                                {
                                    UpdateBattleText(player.FirstName + "は凍てつく氷の弾丸を放ってきた！\r\n");

                                    PlayerRandomTargetDamage(player, 5, "FrozenLance", TruthActionCommand.MagicType.Ice);
                                }
                                else if (player.ActionLabel.text == "ウォーター・スラッシュ")
                                {
                                    UpdateBattleText(player.FirstName + "の眼が大きくなる！ウォーター・スラッシュを発生させてきた！\r\n");

                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, "FrozenLance", TruthActionCommand.MagicType.Ice);
                                }
                                else if (player.ActionLabel.text == "ウォータ・バブル")
                                {
                                    //　赤目の方に水耐性を付ける。
                                    UpdateBattleText(player.FirstName + "はウォータ・バブルを発生させた！\r\n");

                                    if (ec1 != null && !ec1.Dead)
                                    {
                                        ec1.CurrentResistIceUp = 10;
                                        ec1.CurrentResistIceUpValue = 2000;
                                        ec1.ActivateBuff(ec1.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp", 10);
                                    }
                                    else if (ec2 != null && !ec2.Dead)
                                    {
                                        ec2.CurrentResistIceUp = 10;
                                        ec2.CurrentResistIceUpValue = 2000;
                                        ec2.ActivateBuff(ec2.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp", 10);
                                    }

                                }
                                else if (player.ActionLabel.text == "ハルシネイト・アイ")
                                {
                                    UpdateBattleText(player.FirstName + "の目が鋭く凝視してきた！\r\n");

                                    PlayerAllStun(player);
                                }

                                break;


                            case Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU:
                                if (player.StackCommandString == "スター・ダスト")
                                {
                                    UpdateBattleText(player.FirstName + "のスターソードが" + player.StackTarget.FirstName + "へ無数の星屑を落とす！\r\n");

                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        NowBlind(player, player.StackTarget, 7);
                                    }

                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        NowStunning(player, player.StackTarget, 1, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "スターソード『煌』")
                                {
                                    UpdateBattleText(player.FirstName + "はスターソード『煌』を振りかざしてきた！\r\n");
                                    PlayerNormalAttack(player, target, 3.0F, false, false);
                                }
                                else if (player.ActionLabel.text == "エーギル・フィールド")
                                {
                                    UpdateBattleText(player.FirstName + "はスターソード『煌』を地面に差し込んだ！\r\n");

                                    AnimationDamage(0, player.Target2, 0, Color.black, false, false, "物理防御UP");
                                    player.Target2.CurrentPhysicalDefenseUp = 4;
                                    player.Target2.CurrentPhysicalDefenseUpValue = 3000;
                                    player.Target2.ActivateBuff(player.Target2.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, 4);
                                }
                                break;

                            case Database.ENEMY_SEA_STAR_KNIGHT_AMARA:
                                if (player.StackCommandString == "スター・フォール")
                                {
                                    UpdateBattleText(player.FirstName + "のスターソードが" + player.StackTarget.FirstName + "へ無数の星屑を落とす！\r\n");
                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        NowSilence(player, player.StackTarget, 5);
                                    }

                                    if (PlayerNormalAttack(player, player.StackTarget, 0, false, false))
                                    {
                                        NowStunning(player, player.StackTarget, 1, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "スターソード『艶』")
                                {
                                    UpdateBattleText(player.FirstName + "はスターソード『艶』を振りかざしてきた！\r\n");
                                    PlayerNormalAttack(player, target, 3.0F, false, false);
                                }
                                else if (player.ActionLabel.text == "アマラ・フィールド")
                                {
                                    UpdateBattleText(player.FirstName + "はスターソード『艶』を地面に差し込んだ！\r\n");

                                    AnimationDamage(0, player.Target2, 0, Color.black, false, false, "魔法防御UP");
                                    player.Target2.CurrentMagicDefenseUp = 4;
                                    player.Target2.CurrentMagicDefenseUpValue = 3000;
                                    player.Target2.ActivateBuff(player.Target2.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, 4);
                                }
                                break;

                            case Database.ENEMY_SEA_STAR_ORIGIN_KING:
                                if (player.StackCommandString == "海星源の授印")
                                {
                                    UpdateBattleText(player.FirstName + "は海星源の場全体へ大きな授印を展開しはじめた！\r\n");

                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerSpellProtection(player, group[ii]);
                                        PlayerSpellSaintPower(player, group[ii]);
                                        PlayerSpellDeflection(player, group[ii]);
                                        AnimationDamage(0, group[ii], 0, Color.black, false, false, "授印");
                                    }
                                }
                                break;

                            case Database.ENEMY_BOSS_LEVIATHAN:
                                if (player.StackCommandString == "タイダル・ウェイブ")
                                {
                                    // 全体ダメージ
                                    UpdateBattleText(player.FirstName + "は体全体を大きくうならせ、大きな津波を発生させてきた！\r\n");
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 3.0F, "FrozenLance", TruthActionCommand.MagicType.Ice);
                                    break;
                                }
                                else if (player.ActionLabel.text == "バースト・クラウド")
                                {
                                    // ランダムダメージ
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 2;

                                        int tempRandom = AP.Math.RandomInteger(group.Count);
                                        AbstractMagicDamage(player, group[tempRandom], 10, ref damage, 0, "FrozenLance", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                    }
                                }
                                else if (player.ActionLabel.text == "海王の咆哮")
                                {
                                    // パワーアップ
                                    effectValue = 500.0F;
                                    UpdateBattleText(player.FirstName + "は【魔法攻撃】が" + effectValue.ToString() + "上昇\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃UP");
                                    player.CurrentMagicAttackUp = Database.INFINITY;
                                    player.CurrentMagicAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, Database.INFINITY);

                                    effectValue = 500.0F;
                                    UpdateBattleText(player.FirstName + "は【物理攻撃】が" + effectValue.ToString() + "上昇\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
                                    player.CurrentPhysicalAttackUp = Database.INFINITY;
                                    player.CurrentPhysicalAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, Database.INFINITY);

                                    effectValue = 2500.0F;
                                    UpdateBattleText(player.FirstName + "は【戦闘反応】が" + effectValue.ToString() + "上昇\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "戦闘反応UP");
                                    player.CurrentReactionUp = Database.INFINITY;
                                    player.CurrentReactionUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + Database.BUFF_REACTION_UP, Database.INFINITY);

                                }
                                else if (player.ActionLabel.text == "サージェティック・バインド")
                                {
                                    // 巻きつきによるスタン＋出血ダメージ攻撃を行う。
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    int tempRandom = AP.Math.RandomInteger(group.Count);

                                    if (PlayerNormalAttack(player, group[tempRandom], 0, false, false))
                                    {
                                        NowSlip(player, group[tempRandom], 10);
                                        NowStunning(player, group[tempRandom], 1, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "大激衝")
                                {
                                    // 一体に大ダメージ
                                    PlayerNormalAttack(player, target, 5.0F, false, false);
                                }
                                break;

                            // ２階：支配竜
                            case Database.ENEMY_DRAGON_TINKOU_DEEPSEA:
                                if (player.ActionLabel.text == "形成消失" || player.StackCommandString == "形成消失")
                                {
                                    txtBattleMessage.text = txtBattleMessage.text.Insert(0, "【沈降せし者】ディープシーは、その場より消えさった。\r\n");
                                    //System.Threading.Thread.Sleep(2000);
                                    endBattleForMatrixDragonEnd = true;
                                    Debug.Log("endBattleForMatrixDragonEnd is now true ok");
                                }
                                else if (player.ActionLabel.text == "無音の呼び声")
                                {
                                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                                    {
                                        string message = "    【ディープシーの声】\r\n    希望を、さすれば絶望の中にて生を得る。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                                    {
                                        string message = "    【ディープシーの声】\r\n    希望を、さすれば虚無より、空間を生み出す。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 2)
                                    {
                                        string message = "    【ディープシーの声】\r\n    希望を、さすれば自己消失より、存在を見出す。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 3)
                                    {
                                        string message = "    【ディープシーの声】\r\n    希望とは、完全に沈降した世界より生まれるもの。\r\n    これを認識せよ、アイン・ウォーレンス。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 4)
                                    {
                                        string message = "    【ディープシーの声】\r\n    さすれば、完全なる希望が得られるであろう。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                }
                                break;

                            // ２階：DUEL対戦相手
                            case Database.DUEL_SCOTY_ZALGE:
                                if (player.ActionLabel.text == "ザルゲ・スラッシュ")
                                {
                                    if (PlayerNormalAttack(player, target, 0, false, false))
                                    {
                                        NowPoison(player, target, 999, true);
                                    }
                                }
                                break;
                            #endregion
                            #region "３階"
                            // ３階雑魚
                            case Database.ENEMY_TOSSIN_ORC:
                                if (player.ActionLabel.text == "突貫")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に向けて突貫してきた！\r\n");
                                    PlayerNormalAttack(player, target, 2.0F, true, false);
                                }
                                else if (player.ActionLabel.text == "暴走")
                                {
                                    UpdateBattleText(player.FirstName + "の暴走！　見境なく攻撃をしてきた！\r\n");
                                    PlayerRandomTargetPhysicalDamage(player, 4, 0, 0.0F);
                                }
                                break;
                            case Database.ENEMY_SNOW_CAT:
                                if (player.ActionLabel.text == "連続攻撃")
                                {
                                    UpdateBattleText(player.FirstName + "の連続攻撃！\r\n");
                                    PlayerNormalAttack(player, target, 0, false, false);
                                    PlayerNormalAttack(player, target, 0, false, false);
                                }
                                else if (player.ActionLabel.text == "凍りつく吹雪")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "へ凍りつく吹雪を繰り出してきた！\r\n");
                                    PlayerMagicAttack(player, target, 0, 0.0F);
                                    NowFrozen(player, target, 1);
                                    NowSlow(player, target, 2);
                                }
                                break;
                            case Database.ENEMY_WAR_MAMMOTH:
                                if (player.ActionLabel.text == "ためる") // ためるは魔法専用だが、ここではマンモス専用で物理攻撃２倍としたい。
                                {
                                    UpdateBattleText(player.FirstName + "はその場で足踏みをしている。\r\n");
                                    player.CurrentPhysicalChargeCount++;
                                }
                                else if (player.ActionLabel.text == "蹂躙")
                                {
                                    UpdateBattleText(player.FirstName + "は場全体を蹂躙してきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerNormalAttack(player, group[ii], player.CurrentPhysicalChargeCount, true, false);
                                    }
                                    player.CurrentPhysicalChargeCount = 0;
                                }
                                break;
                            case Database.ENEMY_WINGED_COLD_FAIRY:
                                if (player.ActionLabel.text == "プチ・ブリザード")
                                {
                                    UpdateBattleText(player.FirstName + "：凍えちゃえ、プチ・ブリザード！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerMagicAttack(player, group[ii], 0, 0);
                                        NowFrozen(player, group[ii], 1);
                                    }
                                }
                                else if (player.ActionLabel.text == "凍結玉")
                                {
                                    UpdateBattleText(player.FirstName + "：動けないようにしてアゲル。　凍結玉！\r\n");
                                    PlayerMagicAttack(player, target, 0, 0);
                                    NowFrozen(player, target, 2);
                                }
                                else if (player.ActionLabel.text == "ウィンター・ソング")
                                {
                                    UpdateBattleText(player.FirstName + "：ステキでしょ・・・ウィンターソング。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        NowFrozen(player, group[ii], 3);
                                    }
                                }
                                break;

                            case Database.ENEMY_BRUTAL_OGRE:
                                if (player.ActionLabel.text == "ぶん投げる")
                                {
                                    UpdateBattleText(player.FirstName + "は棍棒をぶん投げてきた！\r\n");
                                    PlayerNormalAttack(player, target, 0, false, false);
                                }
                                else if (player.ActionLabel.text == "氷の儀式")
                                {
                                    UpdateBattleText(player.FirstName + "は氷の儀式を行った。氷属性の攻撃が棍棒に追加付与された！\r\n");
                                    GroundOne.PlaySoundEffect("IceNeedle");
                                    target.CurrentFrozenAura = Database.INFINITY;
                                    target.ActivateBuff(target.pbFrozenAura, Database.BaseResourceFolder + "FrozenAura", Database.INFINITY);
                                }
                                break;
                            case Database.ENEMY_HYDRO_LIZARD:
                                if (player.ActionLabel.text == "リザード・スラッシュ")
                                {
                                    PlayerNormalAttack(player, player.Target, 0, 1, false, false, 0, 0, Database.SOUND_CRUSHING_BLOW, -1, true, CriticalType.Random);
                                }
                                else if (player.ActionLabel.text == "アイシクル・ブレード")
                                {
                                    UpdateBattleText(player.FirstName + "はブレードを氷化させて太刀を放ってきた！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, false, false))
                                    {
                                        NowFrozen(player, target, 1);
                                    }
                                }
                                break;
                            case Database.ENEMY_PENGUIN_STAR:
                                if (player.ActionLabel.text == "ペンギンの輝き！")
                                {
                                    UpdateBattleText(player.FirstName + "は堂々の構えで凛としたオーラを放ち始めた！\r\n");
                                    player.CurrentStrengthUp = Database.INFINITY;
                                    player.CurrentStrengthUpValue = 300;
                                    player.CurrentAgilityUp = Database.INFINITY;
                                    player.CurrentAgilityUpValue = 300;
                                    player.CurrentIntelligenceUp = Database.INFINITY;
                                    player.CurrentIntelligenceUpValue = 300;
                                    player.CurrentStaminaUp = Database.INFINITY;
                                    player.CurrentStaminaUpValue = 300;
                                    player.CurrentMindUp = Database.INFINITY;
                                    player.CurrentMindUpValue = 300;
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffAgilityUp", Database.INFINITY);
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffIntelligenceUp", Database.INFINITY);
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffStaminaUp", Database.INFINITY);
                                    player.ActivateBuff(((TruthEnemyCharacter)player).pbStrengthUp, Database.BaseResourceFolder + "BuffMindUp", Database.INFINITY);
                                }
                                else if (player.ActionLabel.text == "ペンギンアタック！")
                                {
                                    UpdateBattleText(player.FirstName + "は勇猛果敢に正面から突っ込んできた！\r\n");
                                    PlayerNormalAttack(player, target, 0, true, false);
                                }
                                break;
                            case Database.ENEMY_SWORD_TOOTH_TIGER:
                                if (player.ActionLabel.text == "サーヴェルクロー")
                                {
                                    UpdateBattleText(player.FirstName + "の鋭い剣形状の爪が襲いかかってくる！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, false, false))
                                    {
                                        NowStunning(player, target, 2, false);
                                        NowSlip(player, target, 10);
                                    }
                                }
                                else if (player.ActionLabel.text == "目くらまし")
                                {
                                    UpdateBattleText(player.FirstName + "は目くらましを繰り出してきた！\r\n");
                                    NowBlinded(player, 3);
                                }
                                else if (player.ActionLabel.text == "連速三段")
                                {
                                    UpdateBattleText(player.FirstName + "は目にも止まらぬ速さで連続攻撃を繰り出してきた！\r\n");
                                    for (int ii = 0; ii < 3; ii++)
                                    {
                                        PlayerNormalAttack(player, target, 0, false, false);
                                    }
                                }
                                break;
                            case Database.ENEMY_FEROCIOUS_RAGE_BEAR:
                                if (player.ActionLabel.text == "四歯戦速")
                                {
                                    UpdateBattleText(player.FirstName + "は異常な速さで連続攻撃を繰り出してきた！\r\n");
                                    for (int ii = 0; ii < 4; ii++)
                                    {
                                        PlayerNormalAttack(player, target, 0, false, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "自己増強")
                                {
                                    UpdateBattleText(player.FirstName + "は手足の先を地面に食い込ませ、力を入れ始めた！\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
                                    effectValue = 750.0F;
                                    player.CurrentPhysicalAttackUp = 4;
                                    player.CurrentPhysicalAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", 4);
                                }
                                else if (player.ActionLabel.text == "漸波動")
                                {
                                    UpdateBattleText(player.FirstName + "は轟音のような衝撃を放ってきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        if (PlayerNormalAttack(player, group[ii], 0, false, false))
                                        {
                                            NowStunning(player, group[ii], 2, false);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "食いちぎり")
                                {
                                    UpdateBattleText(player.FirstName + "は" + target.FirstName + "に向け激しく噛み千切ってきた！\r\n");
                                    PlayerLifeHalfMax(player, target);
                                }
                                break;

                            case Database.ENEMY_WINTER_ORB:
                                if (player.ActionLabel.text == "氷の結晶術")
                                {
                                    UpdateBattleText(player.FirstName + "は氷を更に結晶化させ、高位魔法の序術を放った！\r\n");
                                    double damage = PrimaryLogic.IceNeedleValue(player, false);
                                    if (AbstractMagicDamage(player, target, 0, ref damage, 2.0f, "IceNeedle", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random))
                                    {
                                        NowFrozen(player, target, 1);
                                    }
                                }
                                else if (player.ActionLabel.text == "冷気の射出")
                                {
                                    UpdateBattleText(player.FirstName + "は凍える冷気を周囲全体に射出した！\r\n");
                                    for (int ii = 0; ii < 5; ii++)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 3;
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        int tempdata = AP.Math.RandomInteger(group.Count);
                                        if (AbstractMagicDamage(player, group[tempdata], 30, ref damage, 0, "IceNeedle", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random))
                                        {
                                            NowFrozen(player, group[tempdata], 1);
                                        }
                                    }
                                }
                                break;
                            case Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI:
                                if (player.ActionLabel.text == "津波の呼び声")
                                {
                                    UpdateBattleText(player.FirstName + "は周囲全体の場へ津波が襲来するよう呼び声を放った！\r\n");
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 2.0F, "FrozenLance", TruthActionCommand.MagicType.Ice);
                                }
                                else if (player.ActionLabel.text == "平穏の呼び声")
                                {
                                    UpdateBattleText(player.FirstName + "は周囲全体の場が和むような呼び声を放った！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        NowParalyze(player, group[ii], 2);
                                    }
                                }
                                break;
                            case Database.ENEMY_INTELLIGENCE_ARGONIAN:
                                if (player.ActionLabel.text == "打突")
                                {
                                    UpdateBattleText(player.FirstName + "は剣の切っ先を鋭く" + target.FirstName + "へ向け、突いてきた！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, true, false))
                                    {
                                        NowParalyze(player, target, 1);
                                    }
                                }
                                break;
                            case Database.ENEMY_MAGIC_HYOU_RIFLE:
                                if (player.ActionLabel.text == "雹弾乱射")
                                {
                                    UpdateBattleText(player.FirstName + "は銃口を特定に定めず、乱射してきた！\r\n");
                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 3;
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        int tempdata = AP.Math.RandomInteger(group.Count);
                                        AbstractMagicDamage(player, group[tempdata], 10, ref damage, 0, "IceNeedle", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                    }
                                }
                                else if (player.ActionLabel.text == "ＳＰＬＡＳＨ！")
                                {
                                    UpdateBattleText(player.FirstName + "：ＳＰＬＡａａａａａａａａａａＳＨ！！\r\n");
                                    for (int ii = 0; ii < 20; ii++)
                                    {
                                        string sound = String.Empty;
                                        if (ii == 0) { sound = "IceNeedle"; }

                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 0.5f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 3;
                                        AbstractMagicDamage(player, target, 10, ref damage, 0, sound, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                    }
                                }
                                else if (player.ActionLabel.text == "マジックバリア")
                                {
                                    UpdateBattleText(player.FirstName + "は周囲全体に濃い青色のフィールドを展開した！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        group[ii].CurrentSkyShield = 999;
                                        group[ii].CurrentSkyShieldValue++;
                                        group[ii].ChangeSkyShieldStatus(group[ii].CurrentSkyShield);
                                    }
                                }
                                break;
                            case Database.ENEMY_PURE_BLIZZARD_CRYSTAL:
                                if (player.ActionLabel.text == "ブリザード")
                                {
                                    UpdateBattleText(player.FirstName + "：【クダケチレ　オール　ブリザード】\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < 16; ii++)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 0.8f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
                                        string sound = String.Empty;
                                        if (ii == 0) { sound = "FrozenLance"; }

                                        int randomValue = AP.Math.RandomInteger(group.Count);
                                        AbstractMagicDamage(player, group[randomValue], 2 * (ii + 1), ref damage, 0, sound, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                    }
                                }
                                else if (player.ActionLabel.text == "零式")
                                {
                                    UpdateBattleText(player.FirstName + "：【ハテテシマエ　クウゼツホウ　ゼロシキ】\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 3.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
                                    int randomValue = AP.Math.RandomInteger(group.Count);
                                    if (AbstractMagicDamage(player, group[randomValue], 0, ref damage, 1.0f, "IceNeedle", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random))
                                    {
                                        NowFrozen(player, group[randomValue], 3);
                                    }
                                }
                                else if (player.ActionLabel.text == "蒼授の気配")
                                {
                                    UpdateBattleText(player.FirstName + "：【ワレコソガ　テンシ　ソウジュ　ノ　シンノツカイテ】\r\n");
                                    PlayerSpellPromisedKnowledge(player, player);
                                }
                                else if (player.ActionLabel.text == "絶・スピニングランサー")
                                {
                                    UpdateBattleText(player.FirstName + "：【ケシトベ　ゼツ　スピニングランサー】\r\n");
                                    if (target.CurrentLife <= 1) // ライフが既に１の場合は即死させる。
                                    {
                                        PlayerDeath(player, target);
                                    }
                                    else
                                    {
                                        PlayerLifeOne(player, target);
                                    }
                                }
                                break;

                            case Database.ENEMY_PURPLE_EYE_WARE_WOLF:
                                if (player.ActionLabel.text == "バトルクライ")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        effectValue = 2500.0F;

                                        AnimationDamage(0, group[ii], 0, Color.black, false, false, "力UP");
                                        group[ii].CurrentStrengthUp = 3;
                                        group[ii].CurrentStrengthUpValue = (int)effectValue;
                                        group[ii].ActivateBuff(group[ii].pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", 3);
                                    }
                                }
                                else if (player.ActionLabel.text == "キリング・スラッシュ")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerNormalAttack(player, group[ii], 0, 0, false, false, 0, 10, string.Empty, -1, false, CriticalType.None);
                                        PlayerNormalAttack(player, group[ii], 0, 0, false, false, 0, 10, string.Empty, -1, false, CriticalType.None);
                                    }
                                }
                                break;
                            case Database.ENEMY_FROST_HEART:
                                if (player.ActionLabel.text == "冷気圧縮")
                                {
                                    UpdateBattleText(player.FirstName + "の発光状態の青さが増していく！\r\n");
                                    effectValue = 10000.0F;
                                    AnimationDamage(0, player, 0, Color.black, false, false, "魔法攻撃UP");
                                    player.CurrentMagicAttackUp = Database.INFINITY;
                                    player.CurrentMagicAttackUpValue += (int)effectValue;
                                    player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, Database.INFINITY);
                                }
                                else if (player.ActionLabel.text == "自爆")
                                {
                                    UpdateBattleText(player.FirstName + "の発光状態が急激に青から白へと変わっていく！！\r\n");
                                    if (AbstractMagicDamage(player, target, 0, 0, 1.0f, "IceNeedle", 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random))
                                    {
                                        double damage = player.CurrentLife;
                                        player.CurrentLife = 0;
                                        UpdateLife(player, damage, false, false, 0, false);
                                        player.DeadPlayer();
                                    }
                                }
                                break;
                            case Database.ENEMY_WIND_BREAKER:
                                if (player.ActionLabel.text == "ソード・オブ・ウィンド")
                                {
                                    UpdateBattleText(player.FirstName + "：コオオォォォォオォォォォォ.....\r\n");
                                    PlayerSpellFlameAura(player, player);
                                    PlayerSpellFrozenAura(player, player);
                                    PlayerSpellGaleWind(player);
                                }
                                else if (player.ActionLabel.text == "断空")
                                {
                                    UpdateBattleText(player.FirstName + "：フシュウウゥゥゥゥウウウゥゥ！！\r\n");
                                    PlayerNormalAttack(player, target, 10.0f, 0, false, false, 0, 0, "KineticSmash", -1, false, CriticalType.None);
                                }
                                else if (player.ActionLabel.text == "アイス・トルネード")
                                {
                                    UpdateBattleText(player.FirstName + "：ッゴアアァァアァァァァアア！！！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count * 3; ii++)
                                    {
                                        int randomValue = AP.Math.RandomInteger(group.Count);
                                        PlayerNormalAttack(player, group[randomValue], 0, 0, false, false, 0, 10, string.Empty, -1, false, CriticalType.None);
                                    }
                                }
                                break;
                            case Database.ENEMY_TUNDRA_LONGHORN_DEER:
                                if (player.ActionLabel.text == "トキのコエ")
                                {
                                    UpdateBattleText(player.FirstName + "は澄み切った発声を一定のリズムを刻む波長を発してきた！\r\n");
                                    NowSlow(player, target, 10);
                                    NowTemptation(player, target, 2);
                                    BuffDownBattleReaction(target, 10000);
                                }
                                else if (player.ActionLabel.text == "氷雪化現象")
                                {
                                    UpdateBattleText(player.FirstName + "の目の奥が青白く輝いた！！\r\n");
                                    NowBlind(player, target, 10);
                                    NowFrozen(player, target, 2);
                                    effectValue = 3000.0F;
                                    BuffDownPhysicalAttack(target, effectValue);
                                }
                                else if (player.ActionLabel.text == "無音音響の和")
                                {
                                    UpdateBattleText(player.FirstName + "は発声を行わず、口の形だけを言葉として表してきた。\r\n");
                                    NowSilence(player, target, 10);
                                    NowParalyze(player, target, 2);
                                    BuffDownMagicDefense(target, 10000);
                                }
                                else if (player.ActionLabel.text == "レッドスノーホーン")
                                {
                                    UpdateBattleText(player.FirstName + "は真っ赤な角をこちらに向け、奇妙な呻き声を発しながら高熱の火の玉を吹き出してきた！！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, 0, 2.0f, "LavaAnnihilation", 0, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                    }
                                }
                                break;

                            // ３階ボス
                            case Database.ENEMY_BOSS_HOWLING_SEIZER:
                                if (player.StackCommandString == "アース・コールド・シェイク")
                                {
                                    // 全体：ダメージ＋スタン＋凍結＋麻痺
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    foreach (MainCharacter current in group)
                                    {
                                        if (PlayerNormalAttack(player, current, 2.0F, 0, false, false, 0, 0, string.Empty, -1, false, CriticalType.None))
                                        {
                                            int rand = AP.Math.RandomInteger(3);
                                            switch (rand)
                                            {
                                                case 0:
                                                    NowParalyze(player, current, 2);
                                                    break;
                                                case 1:
                                                    NowStunning(player, current, 2, false);
                                                    break;
                                                case 2:
                                                    NowFrozen(player, current, 2);
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "もぎとり")
                                {
                                    // ライフを０にして即死させるか、ライフ１にする。
                                    UpdateBattleText(player.FirstName + "は巨大な両手をこちらへ向け、胴体をもぎとりにきた！\r\n");
                                    if (target.CurrentLife <= 1) // ライフが既に１の場合は即死させる。
                                    {
                                        PlayerDeath(player, target);
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
                                            PlayerLifeOne(player, target);
                                        }
                                    }
                                    break;
                                }
                                else if (player.ActionLabel.text == "ブンまわし")
                                {
                                    UpdateBattleText(player.FirstName + "は大きい尻尾を部屋中全体に対して振り回してきた！！\r\n");

                                    // 全員：麻痺＋物理ダメージ
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    foreach (MainCharacter current in group)
                                    {
                                        if (PlayerNormalAttack(player, current, 1.5F, 0, false, false, 0, 0, string.Empty, -1, false, CriticalType.None))
                                        {
                                            if (GroundOne.Difficulty <= 1)
                                            {
                                                // 麻痺はたまにしか与えない。
                                                if (AP.Math.RandomInteger(4) == 0)
                                                {
                                                    NowParalyze(player, current, 2);
                                                }
                                            }
                                            else
                                            {
                                                NowParalyze(player, current, 2);
                                            }
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "異常音響")
                                {
                                    // 沈黙＋魔法攻撃DOWN＋魔法防御DOWN
                                    if (GroundOne.Difficulty <= 1)
                                    {
                                        NowSilence(player, target, 2);
                                    }
                                    else
                                    {
                                        NowSilence(player, target, 5);
                                    }

                                    if (GroundOne.Difficulty <= 1)
                                    {
                                        BuffDownMagicAttack(target, 4000, 3);
                                    }
                                    else
                                    {
                                        BuffDownMagicAttack(target, 4000);
                                    }

                                    if (GroundOne.Difficulty > 1)
                                    {
                                        BuffDownMagicDefense(target, 2000);
                                    }
                                }
                                else if (player.ActionLabel.text == "凍らせる視線")
                                {
                                    // 凍結＋戦闘反応DOWN＋戦闘速度DOWN
                                    if (GroundOne.Difficulty <= 1)
                                    {
                                        NowFrozen(player, target, 2);
                                    }
                                    else
                                    {
                                        NowFrozen(player, target, 5);
                                    }

                                    if (GroundOne.Difficulty <= 1)
                                    {
                                        BuffDownBattleSpeed(target, 500, 3);
                                    }
                                    else
                                    {
                                        BuffDownBattleSpeed(target, 500);
                                    }

                                    if (GroundOne.Difficulty > 1)
                                    {
                                        BuffDownBattleReaction(target, 1000);
                                    }
                                }
                                else if (player.ActionLabel.text == "破裂する雄叫び")
                                {
                                    // 全体：次のターン、敵を対象とする行動は失敗する。その後、自分自身がスタンする。
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    foreach (MainCharacter current in group)
                                    {
                                        NowPreStunning(current, 2);
                                    }
                                }
                                break;

                            // ３階：支配竜
                            case Database.ENEMY_DRAGON_DESOLATOR_AZOLD:
                                //MessageBox.Show(player.StackCommandString);
                                if (player.ActionLabel.text == "形成消失" || player.StackCommandString == "形成消失")
                                {
                                    txtBattleMessage.text = txtBattleMessage.text.Insert(0, "【凍てつく者】アゾルドは、その場より消えさった。\r\n");
                                    System.Threading.Thread.Sleep(2000);
                                    endBattleForMatrixDragonEnd = true;
                                }
                                else if (player.ActionLabel.text == "無音の呼び声")
                                {
                                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                                    {
                                        string message = "    【アゾルドの声】\r\n    成長、それは歩を進めようとした者にのみ、認識されるもの。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                                    {
                                        string message = "    【アゾルドの声】\r\n    成長、それは歩を進めている間の者には、認識できないもの。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 2)
                                    {
                                        string message = "    【アゾルドの声】\r\n    成長、それは歩を進めてきた者にのみ、認識されるもの。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 3)
                                    {
                                        string message = "    【アゾルドの声】\r\n    成長、それは最終的な生成形態として凍結されるため。\r\n    これを認識せよ、アイン・ウォーレンス。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 4)
                                    {
                                        string message = "    【アゾルドの声】\r\n    さすれば、完全なる成長が得られるであろう。";
                                        AnimationMessageFadeOut(message);
                                        ((TruthEnemyCharacter)player).AI_TacticsNumber++;
                                    }
                                }
                                break;
                            #endregion
                            #region "４階"
                            case Database.ENEMY_GENAN_HUNTER:
                                if (player.ActionLabel.text == "バインド・ウィップ")
                                {
                                    UpdateBattleText(player.FirstName + "は茨のムチをサークル状にし、" + target.FirstName + "の足元に向けて放ってきた！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, true, false))
                                    {
                                        NowParalyze(player, target, 2);
                                        NowPoison(player, target, 999, true);
                                    }
                                }
                                else if (player.ActionLabel.text == "アジテイト・アロー")
                                {
                                    UpdateBattleText(player.FirstName + "のアジテイト・アローが炸裂し、" + target.FirstName + "は扇動されてしまった！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, true, false))
                                    {
                                        UpdateBattleText(target.FirstName + "の行動は、通常の攻撃に戻されてしまった。");
                                        target.PA = MainCharacter.PlayerAction.NormalAttack;
                                        target.Target = player;
                                        target.ActionLabel.text = Database.ATTACK_JP;
                                        target.MainObjectButton.image.sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.ATTACK_EN);
                                    }
                                }
                                else if (player.ActionLabel.text == "デッドリー・ショット")
                                {
                                    // ライフを０にして即死させるか、ライフ１か、半分に減らすか、にする。
                                    UpdateBattleText(player.FirstName + "は全神経を集中させ、身体を貫通する炎の矢を放ってきた！\r\n");
                                    if (target.CurrentLife <= 1) // ライフが既に１の場合は即死させる。
                                    {
                                        PlayerDeath(player, target);
                                    }
                                    else
                                    {
                                        if (GroundOne.Difficulty <= 1)
                                        {
                                            PlayerLifeHalfCurrent(player, target);
                                        }
                                        else
                                        {
                                            int rand = AP.Math.RandomInteger(4);
                                            if (0 <= rand && rand <= 2)
                                            {
                                                PlayerLifeHalfCurrent(player, target);
                                            }
                                            else
                                            {
                                                PlayerLifeOne(player, target);
                                            }
                                        }
                                    }
                                }
                                break;

                            case Database.ENEMY_BEAST_MASTER:
                                if (player.ActionLabel.text == "タイガー・ブロウ")
                                {
                                    UpdateBattleText(player.FirstName + "のタイガー・ブロウが炸裂！！\r\n");
                                    if (PlayerNormalAttack(player, target, 4, 0, false, false, 0, 0, string.Empty, -1, false, CriticalType.None))
                                    {
                                        NowStunning(player, target, 2, false);
                                    }

                                    player.CurrentPhysicalAttackUp = 0;
                                    player.CurrentPhysicalAttackUpValue = 0;
                                    player.DeBuff(player.pbPhysicalAttackUp);
                                    player.CurrentSpeedUp = 0;
                                    player.CurrentSpeedUpValue = 0;
                                    player.DeBuff(player.pbSpeedUp);
                                }
                                else if (player.ActionLabel.text == "圧死の咆哮")
                                {
                                    UpdateBattleText(player.FirstName + "は、エリア全体を圧迫するかのような雄叫びを上げた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    foreach (MainCharacter current in group)
                                    {
                                        int rand = AP.Math.RandomInteger(11);
                                        if (rand == 0) { NowBlind(player, current, 999); }
                                        else if (rand == 1) { NowFrozen(player, current, 2); }
                                        else if (rand == 2) { NowNoResurrection(player, current, 999); }
                                        else if (rand == 3) { NowParalyze(player, current, 2); }
                                        else if (rand == 4) { NowPoison(player, current, 999, true); }
                                        else if (rand == 5) { NowPreStunning(current, 2); }
                                        else if (rand == 6) { NowSilence(player, current, 999); }
                                        else if (rand == 7) { NowSlip(player, current, 999); }
                                        else if (rand == 8) { NowSlow(player, current, 2); }
                                        else if (rand == 9) { NowStunning(player, current, 2, false); }
                                        else if (rand == 10) { NowTemptation(player, current, 2); }
                                    }
                                }
                                else if (player.ActionLabel.text == "ピューマ・ライジング")
                                {
                                    effectValue = 2000.0F;
                                    UpdateBattleText(player.FirstName + "は【物理攻撃】が" + effectValue.ToString() + "上昇\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "物理攻撃UP");
                                    player.CurrentPhysicalAttackUp = 2;
                                    player.CurrentPhysicalAttackUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, 2);

                                    effectValue = 1200.0F;
                                    UpdateBattleText(player.FirstName + "は【戦闘速度】が" + effectValue.ToString() + "上昇\r\n");
                                    AnimationDamage(0, player, 0, Color.black, false, false, "戦闘速度UP");
                                    player.CurrentSpeedUp = 2;
                                    player.CurrentSpeedUpValue = (int)effectValue;
                                    player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, 2);
                                }
                                break;

                            case Database.ENEMY_ELDER_ASSASSIN:
                                if (player.ActionLabel.text == "フェイタル・ニードル")
                                {
                                    UpdateBattleText(player.FirstName + "は目視で確認できないほどの極小の針を" + target.FirstName + "に向けて放った！\r\n");
                                    NowPoison(player, target, 999, true);
                                    PlayerLifeHalfCurrent(player, target);
                                }
                                else if (player.ActionLabel.text == "気配抹消")
                                {
                                    UpdateBattleText(player.FirstName + "は気配を消滅させ、闇と同化した\r\n");
                                    PlayerSkillStanceOfMystic(player, player);
                                }
                                else if (player.ActionLabel.text == "ウロボロスの一撃")
                                {
                                    UpdateBattleText(player.FirstName + "は奇妙な紋様を手で描いて" + target.FirstName + "に向かって一撃を放った！\r\n");
                                    if (PlayerNormalAttack(player, target, 0, false, false))
                                    {
                                        if (GroundOne.Difficulty > 1)
                                        {
                                            BuffDownPhysicalAttack(target, 1500.0F, 3);
                                            BuffDownPhysicalDefense(target, 1500.0F, 3);
                                        }
                                        BuffDownPotential(target, 1500.0F, 3);
                                    }
                                }
                                else if (player.ActionLabel.text == "神速の連撃")
                                {
                                    UpdateBattleText(player.FirstName + "はクナイを３本同時に手元にセットし、" + target.FirstName + "に向かってきた！\r\n");
                                    for (int ii = 0; ii < 3; ii++)
                                    {
                                        PlayerSkillStraightSmash(player, target, 30, false);
                                    }
                                }
                                break;

                            case Database.ENEMY_FALLEN_SEEKER:
                                if (player.ActionLabel.text == "汚れし悪魔契約")
                                {
                                    UpdateBattleText(player.FirstName + "はアーク・デーモンを召喚し、【呪闇】の契約を刻み始めた！\r\n");
                                    player.CurrentShadowUp = Database.INFINITY;
                                    player.CurrentShadowUpValue = 4500;
                                    player.ActivateBuff(player.pbShadowUp, Database.BaseResourceFolder + Database.BUFF_SHADOW_UP + fileExt, Database.INFINITY);
                                    AnimationDamage(0, player, 0, Color.black, false, false, "闇属性UP");
                                }
                                else if (player.ActionLabel.text == "純潔の聖天使契約")
                                {
                                    UpdateBattleText(player.FirstName + "はアーク・エンジェルを召喚し、【呪聖】の契約を刻み始めた！\r\n");
                                    player.CurrentLightUp = Database.INFINITY;
                                    player.CurrentLightUpValue = 7500;
                                    player.ActivateBuff(player.pbLightUp, Database.BaseResourceFolder + Database.BUFF_LIGHT_UP + fileExt, Database.INFINITY);
                                    AnimationDamage(0, player, 0, Color.black, false, false, "聖属性UP");
                                }
                                else if (player.ActionLabel.text == "ホーリー・バレット")
                                {
                                    UpdateBattleText(player.FirstName + "はホーリー・バレットを詠唱した！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    foreach (MainCharacter current in group)
                                    {
                                        double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode);
                                        if (AbstractMagicDamage(player, current, 0, ref damage, 0, Database.SOUND_CELESTIAL_NOVA, 120, TruthActionCommand.MagicType.Light_Shadow, false, CriticalType.Random))
                                        {
                                            PlayerAbstractLifeGain(player, player, 0, damage, 0, "", 0);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "セイント・スラッシュ")
                                {
                                    UpdateBattleText(player.FirstName + "は聖なる輝きを秘めた剣で、突貫してきた！\r\n");
                                    PlayerNormalAttack(player, target, 2.0f, 0, false, false, 0, 0, String.Empty, -1, false, CriticalType.Absolute);
                                    AbstractMagicDamage(player, target, 0, 0, 2.0f, Database.SOUND_CELESTIAL_NOVA, 120, TruthActionCommand.MagicType.Light_Shadow, false, CriticalType.Absolute);
                                }
                                break;

                            case Database.ENEMY_MASTER_LOAD:
                                if (player.ActionLabel.text == "スペリオル・フィールド")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupEnemyGroup(ref group);
                                    foreach (MainCharacter current in group)
                                    {
                                        if (current.CurrentMagicAttackUp <= 0)
                                        {
                                            BuffUpMagicAttack(current, 500.0F, 10);
                                        }
                                        else if (current.CurrentPhysicalAttackUp <= 0)
                                        {
                                            BuffUpPhysicalAttack(current, 500.0F, 10);
                                        }
                                        else if (current.CurrentSpeedUp <= 0)
                                        {
                                            BuffUpBattleSpeed(current, 500.0F, 10);
                                        }
                                        else if (current.CurrentPotentialUp <= 0)
                                        {
                                            BuffUpPotential(current, 500.0F, 10);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ダーク・エリミネイト")
                                {
                                    double damage = PrimaryLogic.DarkBlastValue(player, false);
                                    if (AbstractMagicDamage(player, target, 0, ref damage, 2.0F, Database.SOUND_DARK_BLAST, 120, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random))
                                    {
                                        BuffDownMagicDefense(target, 500);
                                    }
                                }
                                else if (player.ActionLabel.text == "振動剣")
                                {
                                    double damage = PrimaryLogic.DarkBlastValue(player, false);
                                    if (AbstractMagicDamage(player, target, 0, ref damage, 2.0F, Database.SOUND_RISINGKNUCKLE, 120, TruthActionCommand.MagicType.Force, false, CriticalType.Random))
                                    {
                                        BuffDownPhysicalDefense(target, 500.0F);
                                    }
                                }
                                break;

                            case Database.ENEMY_EXECUTIONER:
                                if (player.ActionLabel.text == "断罪の加護")
                                {
                                    UpdateBattleText(player.FirstName + "は剣を天へとすくい上げるモーションを取り、断罪を誓った！\r\n");
                                    GroundOne.PlaySoundEffect(Database.SOUND_GLORY);
                                    PlayerBuffAbstract(player, player, 999, Database.AFTER_REVIVE_HALF + fileExt);
                                }
                                else if (player.ActionLabel.text == "魂への凍結")
                                {
                                    UpdateBattleText(player.FirstName + "は剣を" + target.FirstName + "の魂に突き立てるように切っ先を向けた！！\r\n");
                                    GroundOne.PlaySoundEffect(Database.SOUND_ABSOLUTE_ZERO);
                                    if (player.Target.CurrentAbsoluteZero <= 0) // 強力無比な魔法のため、継続ターンの連続更新は出来なくしている。
                                    {
                                        PlayerBuffAbstract(player, target, 2, Database.ABSOLUTE_ZERO + fileExt);
                                    }
                                }
                                else if (player.ActionLabel.text == "死滅のひと振り")
                                {
                                    UpdateBattleText(player.FirstName + "は音もなく、剣を縦にゆるりと振り落とした。\r\n");
                                    if (PlayerNormalAttack(player, target, 2.0F, false, false))
                                    {
                                        GroundOne.PlaySoundEffect(Database.SOUND_DAMNATION);
                                        PlayerBuffAbstract(player, target, 999, Database.DAMNATION + fileExt);
                                    }
                                }
                                else if (player.ActionLabel.text == "デス・ストライク")
                                {
                                    UpdateBattleText(player.FirstName + "の強烈無比な剣技が" + target.FirstName + "に炸裂した！\r\n");
                                    if (PlayerNormalAttack(player, target, 3.0F, false, false))
                                    {
                                        if (AP.Math.RandomInteger(3) == 0)
                                        {
                                            AnimationDamage(0, target, 0, Color.black, false, false, "即死");
                                            PlayerDeath(player, target);
                                        }
                                    }
                                }
                                break;

                            case Database.ENEMY_DARK_MESSENGER:
                                if (player.ActionLabel.text == "黒龍のささやき")
                                {
                                    UpdateBattleText(player.FirstName + "は黒龍より禁断の闇技を授かり、アイン達へ向けて呪術を放った。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    foreach (MainCharacter current in group)
                                    {
                                        NowNoResurrection(player, current, 999);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "チューズン・サクリファイ")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>(); // TruthEnemyCharacter
                                    SetupEnemyGroup(ref group);
                                    foreach (TruthEnemyCharacter current in group)
                                    {
                                        if (current != player)
                                        {
                                            UpdateBattleText(player.FirstName + "は" + current.FirstName + "の魂を根こそぎエネルギーに変換し、それを放出してきた！\r\n");
                                            int damage = current.CurrentLife;
                                            PlayerDeath(player, current);
                                            AbstractMagicDamage(player, target, 0, damage, 0, Database.SOUND_CHOSEN_SACRIFY, 120, TruthActionCommand.MagicType.Shadow, false, CriticalType.None);
                                            break;
                                        }
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                                }
                                else if (player.ActionLabel.text == "死への背徳")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>(); // TruthEnemyCharacter
                                    SetupEnemyGroup(ref group);
                                    foreach (TruthEnemyCharacter current in group)
                                    {
                                        UpdateBattleText(player.FirstName + "は" + current.FirstName + "の魂に黒龍の生命エネルギーを呪変換を行った！\r\n");
                                        AnimationDamage(0, current, 0, Color.black, false, false, "復活");
                                        current.ResurrectPlayer(current.MaxLife);
                                        break;
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                                }
                                break;

                            case Database.ENEMY_BLACKFIRE_MASTER_BLADE:
                                if (player.ActionLabel.text == "螺旋黒炎")
                                {
                                    UpdateBattleText(player.FirstName + "は刀の切っ先を素早く螺旋状に描き、黒い炎を噴出してきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    foreach (MainCharacter current in group)
                                    {
                                        if (AbstractMagicDamage(player, current, 0, PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 4.0f, 0.0f, 0.0f, 0.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode), 0, "FlameStrike", 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random))
                                        {
                                            current.CurrentBlackFire = Database.INFINITY;
                                            current.ActivateBuff(current.pbBlackFire, Database.BaseResourceFolder + Database.BLACK_FIRE + fileExt, Database.INFINITY);
                                        }
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "乱奏連撃")
                                {
                                    UpdateBattleText(player.FirstName + "は狂気じみた旋律を奏でつつ、乱雑に刀を振り回してきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int i = 0; i < 10; i++)
                                    {
                                        PlayerNormalAttack(player, group[AP.Math.RandomInteger(group.Count - 1)], 3.0f, 0, false, false, 0, 2, "", -1, false, CriticalType.Random);
                                    }
                                }
                                else if (player.ActionLabel.text == "ブルー・エクスプロード")
                                {
                                    UpdateBattleText(player.FirstName + "の炎の色は、青白く輝き、" + target.FirstName + "に蒼く輝くファイア・ボールが放たれた！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    AbstractMagicDamage(player, group[AP.Math.RandomInteger(group.Count - 1)], 0, 0, 10.0f, Database.SOUND_KOKUEN_BLUE_EXPLODE, 120, TruthActionCommand.MagicType.Fire_Ice, true, CriticalType.None);
                                }
                                break;

                            case Database.ENEMY_SIN_THE_DARKELF:
                                if (player.ActionLabel.text == "ネイチャー・エンゼンブル")
                                {
                                    UpdateBattleText(player.FirstName + "の辺りは淡い緑色に球体に包まれた！\r\n");
                                    PlayerBuffAbstract(player, player, 999, "NourishSense");
                                    PlayerBuffAbstract(player, player, 999, "WordOfLife");
                                    PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.SacredHealValue(player, false), 0, "CelestialNova", 0);
                                }
                                else if (player.ActionLabel.text == "シャープネル・ニードル")
                                {
                                    UpdateBattleText(player.FirstName + "は空間にずぶとい針形状の氷を創生してきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, 0, 5.0f, Database.SOUND_SHARPNEL_NEEDLE, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.None);
                                    }
                                }
                                else if (player.ActionLabel.text == "メギド・ブレイズ")
                                {
                                    UpdateBattleText(player.FirstName + "は見たことも無い紋様を描き、炎の渦を空間に自然発生させてきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, 0, 5.0f, Database.SOUND_MEGID_BLAZE, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.None);
                                    }
                                }
                                else if (player.ActionLabel.text == "アーケン・デストラクション")
                                {
                                    UpdateBattleText(player.FirstName + "は青紫のエネルギー体を創り出し、一気にそれを放出してきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        GroundOne.PlaySoundEffect(Database.SOUND_ARCANE_DESTRUCTION);
                                        PlayerLifeHalfCurrent(player, group[ii]);
                                    }
                                }
                                break;

                            case Database.ENEMY_SUN_STRIDER:
                                if (player.ActionLabel.text == "太陽の滅印")
                                {
                                    UpdateBattleText(player.FirstName + "：太陽を滅ぼすが如く！　太陽の滅印をくらえ！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    PlayerLifeOne(player, current);
                                    NowSlip(player, current, 999);
                                }
                                else if (player.ActionLabel.text == "ブラック・フレア")
                                {
                                    UpdateBattleText(player.FirstName + "：太陽の影より出てし熱き炎よ、焼き尽くせ！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    GroundOne.PlaySoundEffect(Database.SOUND_BLACK_FLARE);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    PlayerBuffAbstract(player, current, 999, Database.DAMNATION + fileExt);
                                    PlayerBuffAbstract(player, current, 999, Database.ENRAGE_BLAST + fileExt);
                                }
                                else if (player.ActionLabel.text == "サテライト・エナジー")
                                {
                                    UpdateBattleText(player.FirstName + "：天より授かりし力よ、今ここに！！\r\n");
                                    GroundOne.PlaySoundEffect(Database.SOUND_GLORY);
                                    PlayerBuffAbstract(player, player, 999, Database.HOLY_BREAKER + fileExt);
                                    PlayerBuffAbstract(player, player, 999, Database.SAINT_POWER + fileExt);
                                    PlayerBuffAbstract(player, player, 999, Database.PSYCHIC_TRANCE + fileExt);
                                }
                                else if (player.ActionLabel.text == "サテライト・ソード")
                                {
                                    UpdateBattleText(player.FirstName + "：我が剣、喰らうがよい！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    PlayerNormalAttack(player, current, 2.0F, false, false);
                                    PlayerNormalAttack(player, current, 3.0F, false, false);
                                    PlayerNormalAttack(player, current, 4.0F, false, false);
                                }
                                break;

                            case Database.ENEMY_ARC_DEMON:
                                if (player.ActionLabel.text == "デビル・プロミス")
                                {
                                    UpdateBattleText(player.FirstName + "：チカラをシラヌモノ、ゼツボウセヨ\r\n");
                                    BuffUpPhysicalAttack(player, 3000.0F);
                                    BuffUpMagicAttack(player, 3000.0F);
                                    BuffUpPhysicalDefense(player, 700.0F);
                                    BuffUpMagicDefense(player, 700.0F);
                                    BuffUpBattleSpeed(player, 500.0F);
                                    BuffUpBattleReaction(player, 500.0F);
                                    BuffUpPotential(player, 100.0F);
                                }
                                else if (player.ActionLabel.text == "呪怨殺")
                                {
                                    UpdateBattleText(player.FirstName + "：シせよ、オロカなるモノ\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    PlayerDeath(player, current);
                                }
                                else if (player.ActionLabel.text == "深淵の理")
                                {
                                    UpdateBattleText(player.FirstName + "：ヤミへのコトワリ、シンエンへシズメ\r\n");
                                    BuffDownPhysicalAttack(target, 2500.0F);
                                    BuffDownMagicAttack(target, 2500.0F);
                                    BuffDownPhysicalDefense(target, 600.0F);
                                    BuffDownMagicDefense(target, 600.0F);
                                    BuffDownBattleSpeed(target, 500.0F);
                                    BuffDownBattleReaction(target, 350.0F);
                                    BuffDownPotential(target, 2000.0F);
                                }
                                else if (player.ActionLabel.text == "ギガント・スレイヤー")
                                {
                                    UpdateBattleText(player.FirstName + "：このイチゲキにて、クチハテよ\r\n");
                                    PlayerNormalAttack(player, target, 10.0F, 2, false, false, 0, 0, Database.SOUND_LAVA_ANNIHILATION, -1, false, CriticalType.None);
                                }
                                break;

                            case Database.ENEMY_BALANCE_IDLE:
                                if (player.ActionLabel.text == "全ては灰に")
                                {
                                    UpdateBattleText(player.FirstName + "：衰え朽ちなさい。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerLifeDown(player, group[ii], group[ii].CurrentLife * AP.Math.RandomReal());
                                    }
                                }
                                else if (player.ActionLabel.text == "生命の輝き")
                                {
                                    UpdateBattleText(player.FirstName + "：この輝きを受け取りなさい。\r\n");
                                    double damage = player.CurrentLife / player.MaxLife;
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, group[ii].MaxLife * (1.0f - damage), 0, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.None, false, CriticalType.None);
                                    }
                                    PlayerAbstractLifeGain(player, player, 0, player.MaxLife * 0.1F, 0, Database.SOUND_FRESH_HEAL, 9);
                                }
                                else if (player.ActionLabel.text == "オーン・プリゼンス")
                                {
                                    UpdateBattleText(player.FirstName + "：永久に果てることなく、永らえなさい。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        BuffUpPhysicalDefense(group[ii], 4000);
                                        BuffUpMagicDefense(group[ii], 5000);
                                        GroundOne.PlaySoundEffect(Database.SOUND_GLORY);
                                        PlayerBuffAbstract(player, group[ii], 999, "AfterReviveHalf" + fileExt);
                                        player.CurrentStanceOfDeath = Database.INFINITY;
                                        group[ii].ActivateBuff(group[ii].pbStanceOfDeath, Database.BaseResourceFolder + Database.STANCE_OF_DEATH + fileExt, Database.INFINITY);
                                    }
                                }
                                else if (player.ActionLabel.text == "レヴェルの唄")
                                {
                                    UpdateBattleText(player.FirstName + "：苦しみを抱え込みなさい。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    SetupEnemyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        GroundOne.PlaySoundEffect(Database.SOUND_DAMNATION);
                                        NowSlip(player, group[ii], 999);
                                        NowPoison(player, group[ii], 999, true);
                                        PlayerBuffAbstract(player, target, 999, Database.DAMNATION + fileExt);
                                    }
                                }
                                break;

                            case Database.ENEMY_GO_FLAME_SLASHER:
                                if (player.ActionLabel.text == "煉獄弾")
                                {
                                    UpdateBattleText(player.FirstName + "：ダラララララアァ！！！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                        double magnify = 1.0f;
                                        if (current.CurrentFireDamage2 > 0) { magnify = 2.0f; }
                                        AbstractMagicDamage(player, current, 10, 0, magnify, Database.SOUND_FIREBALL, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "禍の炎")
                                {
                                    UpdateBattleText(player.FirstName + "：シュゴオオオォォォ！！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    //if (GroundOne.MC != null && !GroundOne.MC.Dead && GroundOne.MC.CurrentFireDamage2 <= 0) { group.Add(GroundOne.MC); }
                                    //if (GroundOne.SC != null && !GroundOne.SC.Dead && GroundOne.SC.CurrentFireDamage2 <= 0) { group.Add(GroundOne.SC); }
                                    //if (GroundOne.TC != null && !GroundOne.TC.Dead && GroundOne.TC.CurrentFireDamage2 <= 0) { group.Add(GroundOne.TC); }
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    if (AbstractMagicDamage(player, current, 0, 0, 5.0f, Database.SOUND_FLAME_STRIKE, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.None))
                                    {
                                        current.CurrentFireDamage2 = Database.INFINITY;
                                        current.ActivateBuff(current.pbFireDamage2, Database.BaseResourceFolder + Database.FIRE_DAMAGE_2 + fileExt, Database.INFINITY);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                                }
                                else if (player.ActionLabel.text == "ジ・エンド")
                                {
                                    UpdateBattleText(player.FirstName + "：フシャアアアアァァ！！！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.LavaAnnihilationValue(player, false), 0.0f, Database.SOUND_LAVA_ANNIHILATION, 0, TruthActionCommand.MagicType.Fire, false, CriticalType.None);
                                    }
                                }
                                break;

                            case Database.ENEMY_DEVIL_CHILDREN:
                                if (player.ActionLabel.text == "暗黒の詠唱術")
                                {
                                    UpdateBattleText(player.FirstName + "：ックク、ブラックマジックだ。\r\n");
                                    player.CurrentBlackMagic = Database.INFINITY;
                                    player.ActivateBuff(player.pbBlackMagic, Database.BaseResourceFolder + Database.BLACK_MAGIC + fileExt, Database.INFINITY);
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "無音の真空波")
                                {
                                    UpdateBattleText(player.FirstName + "：真空波だ。そのままジッとしてろ。\r\n");
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        GroundOne.PlaySoundEffect(Database.SOUND_ABSOLUTE_ZERO);
                                        PlayerBuffAbstract(player, group[ii], 2, Database.ABSOLUTE_ZERO + fileExt);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                                }
                                else if (player.ActionLabel.text == "異常再生")
                                {
                                    UpdateBattleText(player.FirstName + "：この再生能力、ニンゲンは欲しがるだろうな。\r\n");
                                    PlayerAbstractLifeGain(player, player, 0, player.MaxLife, 0, Database.SOUND_CELESTIAL_NOVA, 9);
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 3;
                                }
                                else if (player.ActionLabel.text == "超高温熱波動")
                                {
                                    UpdateBattleText(player.FirstName + "：灼け落ちろ、超高温熱波動だ。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];

                                    if (AbstractMagicDamage(player, current, 0, PrimaryLogic.LavaAnnihilationValue(player, false), 0.0f, Database.SOUND_LAVA_ANNIHILATION, 0, TruthActionCommand.MagicType.Fire, false, CriticalType.None))
                                    {
                                        NowNoResurrection(player, current, 999);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 3;
                                }
                                else if (player.ActionLabel.text == "クロマティック・バレット")
                                {
                                    UpdateBattleText(player.FirstName + "：ッハッハハハ、喰らえ喰らえ。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < 4; ii++)
                                    {
                                        MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                        int rand = AP.Math.RandomInteger(6);
                                        if (rand == 0)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_LAVA_ANNIHILATION, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                        }
                                        else if (rand == 1)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_FROZENLANCE, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                        }
                                        else if (rand == 2)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_WHITEOUT, 120, TruthActionCommand.MagicType.Will, false, CriticalType.Random);
                                        }
                                        else if (rand == 3)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_DAMNATION, 120, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random);
                                        }
                                        else if (rand == 4)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_CELESTIAL_NOVA, 120, TruthActionCommand.MagicType.Light, false, CriticalType.Random);
                                        }
                                        else if (rand == 5)
                                        {
                                            AbstractMagicDamage(player, current, 15, 0, 1.5f, Database.SOUND_WORD_OF_POWER, 120, TruthActionCommand.MagicType.Force, false, CriticalType.Random);
                                        }
                                    }

                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                break;

                            case Database.ENEMY_HOWLING_HORROR:
                                if (player.ActionLabel.text == "スペクター・ヴォイス")
                                {
                                    UpdateBattleText(player.FirstName + "のスペクター・ヴォイスが発動した！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        GroundOne.PlaySoundEffect(Database.SOUND_ABSOLUTE_ZERO);
                                        NowSilence(player, group[ii], 2);
                                        NowPreStunning(group[ii], 2);
                                        NowBlind(player, group[ii], 2);
                                        NowSlip(player, group[ii], 2);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "無慈悲な叫び声")
                                {
                                    UpdateBattleText(player.FirstName + "から無慈悲な叫び声が発せられた！" + target.FirstName + "の心に直接ダメージが発生される。\r\n");
                                    AbstractMagicDamage(player, target, 0, PrimaryLogic.VoiceOfNoMercy(target), 0, Database.SOUND_DAMNATION, 120, TruthActionCommand.MagicType.Shadow, false, CriticalType.None);
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                                }
                                else if (player.ActionLabel.text == "ダーク・シミュラクラム")
                                {
                                    UpdateBattleText(player.FirstName + "不気味なドス黒い物体を排出し、それを放ってきた。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    for (int ii = 0; ii < 5; ii++)
                                    {
                                        NowPoison(player, current, 999, true);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                                }
                                break;

                            case Database.ENEMY_PAIN_ANGEL:
                                if (player.ActionLabel.text == "フェイブリオル・ランス")
                                {
                                    UpdateBattleText(player.FirstName + "：苦しみを刻み込みなさい、フェイブリオル・ランス！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    if (PlayerNormalAttack(player, current, 4.0F, 0, false, false, 0, 0, Database.SOUND_KINETIC_SMASH, -1, false, CriticalType.Absolute))
                                    {
                                        NowStunning(player, current, 3, false);
                                    }
                                }
                                else if (player.ActionLabel.text == "安らかな死別")
                                {
                                    UpdateBattleText(player.FirstName + "：苦しむ事はないのよ、安らかなる死別を与えましょう。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    PlayerDeath(player, current);
                                }
                                else if (player.ActionLabel.text == "ダンシング・ソード")
                                {
                                    UpdateBattleText(player.FirstName + "：この剣舞を受けてみなさい、ダンシング・ソード！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    PlayerNormalAttack(player, group[AP.Math.RandomInteger(group.Count - 1)], 3.0f, 0, false, false, 0, 5, Database.SOUND_STRAIGHT_SMASH, 120, false, CriticalType.None);
                                    AbstractMagicDamage(player, group[AP.Math.RandomInteger(group.Count - 1)], 0, 0, 3.0f, Database.SOUND_FLAME_STRIKE, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
                                    PlayerNormalAttack(player, target, 3.0f, 0, false, false, 0, 5, Database.SOUND_STRAIGHT_SMASH, 120, false, CriticalType.None);
                                    AbstractMagicDamage(player, group[AP.Math.RandomInteger(group.Count - 1)], 0, 0, 3.0f, Database.SOUND_ICENEEDLE, 120, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
                                    PlayerNormalAttack(player, target, 3.0f, 0, false, false, 0, 5, Database.SOUND_STRAIGHT_SMASH, 120, false, CriticalType.None);
                                    AbstractMagicDamage(player, group[AP.Math.RandomInteger(group.Count - 1)], 0, 0, 3.0f, Database.SOUND_DAMNATION, 120, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random);
                                }
                                break;

                            case Database.ENEMY_CHAOS_WARDEN:
                                if (player.ActionLabel.text == "カオス・デスペラート")
                                {
                                    UpdateBattleText(player.FirstName + "は終了のサインを示す演舞を踊ってみせた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        group[ii].CurrentChaosDesperate = Database.INFINITY;
                                        group[ii].CurrentChaosDesperateValue = 10;
                                        group[ii].ActivateBuff(group[ii].pbChaosDesperate, Database.BaseResourceFolder + Database.CHAOS_DESPERATE + fileExt, Database.INFINITY);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                                }
                                else if (player.ActionLabel.text == "マリア・ダンセル")
                                {
                                    UpdateBattleText(player.FirstName + "は不気味で美しい妖艶な演舞を踊ってみせた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        NowTemptation(player, group[ii], 5);
                                        NowNoResurrection(player, group[ii], 999);
                                    }
                                    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                                }
                                else if (player.ActionLabel.text == "調律の破壊")
                                {
                                    UpdateBattleText(player.FirstName + "は何も無い空間に対して破壊を行う演舞を踊ってみせた！\r\n");
                                    GroundOne.PlaySoundEffect(Database.SOUND_TIME_STOP);
                                    CleanUpStep();

                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        CheckChaosDesperate(group[ii]);
                                    }
                                }
                                break;

                            case Database.ENEMY_DOOM_BRINGER:
                                if (player.ActionLabel.text == "ディレンジド・アート")
                                {
                                    UpdateBattleText(player.FirstName + "はえも言われぬ不気味かつ狂気的なオーラをまとい降りかかってきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < 2; ii++)
                                    {
                                        for (int jj = 0; jj < group.Count; jj++)
                                        {
                                            PlayerLifeHalfCurrent(player, group[jj]);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ヘル・サークル")
                                {
                                    UpdateBattleText(player.FirstName + "は地獄の円を部屋全体に描いてきた！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        NowSlow(player, group[ii], 5);
                                        NowSlip(player, group[ii], 999);
                                        NowBlind(player, group[ii], 999);
                                    }
                                }
                                else if (player.ActionLabel.text == "無垢のひと振り")
                                {
                                    UpdateBattleText(player.FirstName + "はスっとどこへともなく振りかざしてきた。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                    NowNoResurrection(player, target, 999);
                                    PlayerDeath(player, current);
                                }
                                else if (player.ActionLabel.text == "ハーシュ・カッター")
                                {
                                    UpdateBattleText(player.FirstName + "は部屋中をかき乱すように剣をぶん回してきた！！\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                                        PlayerNormalAttack(player, current, 2.0F, 0, false, false, 0, 5, String.Empty, -1, false, CriticalType.Random);
                                    }
                                }
                                break;

                            case Database.ENEMY_BOSS_LEGIN_ARZE:
                            case Database.ENEMY_BOSS_LEGIN_ARZE_1:
                            case Database.ENEMY_BOSS_LEGIN_ARZE_2:
                            case Database.ENEMY_BOSS_LEGIN_ARZE_3:
                                if (player.StackCommandString == "虚無の鼓動")
                                {
                                    UpdateBattleText(player.FirstName + "：全て実像は等しく虚構である。虚無の理を受け入れよ。\r\n");
                                    if (player.CurrentMana < Database.COST_THE_ABYSS_WALL)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_THE_ABYSS_WALL;
                                        UpdateMana(player);
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        SetupEnemyGroup(ref group);
                                        for (int ii = 0; ii < group.Count; ii++)
                                        {
                                            PlayerLifeDown(player, group[ii], group[ii].CurrentLife * 0.5f);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "アビスの意志")
                                {
                                    UpdateBattleText(player.FirstName + "：アビスは存在にあらず、意志そのもの。アビスとは理そのもの。\r\n");
                                    if (player.CurrentMana < Database.COST_ABYSS_WILL)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_ABYSS_WILL;
                                        UpdateMana(player);
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        SetupEnemyGroup(ref group);
                                        for (int ii = 0; ii < group.Count; ii++)
                                        {
                                            group[ii].CurrentAbyssWill = Database.INFINITY;
                                            group[ii].CurrentAbyssWillValue++;
                                            group[ii].ActivateBuff(group[ii].pbAbyssWill, Database.BaseResourceFolder + Database.ABYSS_WILL + fileExt, Database.INFINITY);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "壱なる焔")
                                {
                                    UpdateBattleText(player.FirstName + "：永久の浄化印、焔の理を知るがよい。\r\n");
                                    if (player.CurrentMana < Database.COST_ICHINARU_HOMURA)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_ICHINARU_HOMURA;
                                        UpdateMana(player);
                                        target.CurrentIchinaruHomura = Database.INFINITY;
                                        target.ActivateBuff(target.pbIchinaruHomura, Database.BaseResourceFolder + Database.ICHINARU_HOMURA + fileExt, Database.INFINITY);
                                    }
                                }
                                else if (player.ActionLabel.text == "アビス・ファイア")
                                {
                                    UpdateBattleText(player.FirstName + "：深淵の呪怨印、アビスの理を知るがよい。\r\n");
                                    if (player.CurrentMana < Database.COST_ABYSS_FIRE)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_ABYSS_FIRE;
                                        UpdateMana(player);
                                        target.CurrentAbyssFire = Database.INFINITY;
                                        target.ActivateBuff(target.pbAbyssFire, Database.BaseResourceFolder + Database.ABYSS_FIRE + fileExt, Database.INFINITY);
                                    }
                                }
                                else if (player.ActionLabel.text == "ライト・アンド・シャドウ")
                                {
                                    UpdateBattleText(player.FirstName + "：秩序と混沌によりて、聖と闇の本質融合を見るがよい。\r\n");
                                    if (player.CurrentMana < Database.COST_LIGHT_AND_SHADOW)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_LIGHT_AND_SHADOW;
                                        UpdateMana(player);
                                        player.CurrentLightAndShadow = 2;
                                        player.ActivateBuff(player.pbLightAndShadow, Database.BaseResourceFolder + Database.LIGHT_AND_SHADOW + fileExt, 2);
                                    }
                                }
                                else if (player.ActionLabel.text == "エターナル・ドロップレット")
                                {
                                    UpdateBattleText(player.FirstName + "：理に制約など存在しない。理は永遠そのものである。\r\n");
                                    if (player.CurrentMana < Database.COST_ETERNAL_DROPLET)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_ETERNAL_DROPLET;
                                        UpdateMana(player);
                                        player.CurrentEternalDroplet = 4;
                                        player.ActivateBuff(player.pbEternalDroplet, Database.BaseResourceFolder + Database.ETERNAL_DROPLET + fileExt, 4);
                                    }
                                }
                                else if (player.ActionLabel.text == "アウステリティ・マトリクス・Ω")
                                {
                                    UpdateBattleText(player.FirstName + "：厳粛なる支配の理を受け止めるがよい。\r\n");
                                    if (player.CurrentMana < Database.COST_AUSTERITY_MATRIX_OMEGA)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_AUSTERITY_MATRIX_OMEGA;
                                        UpdateMana(player);
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        SetupEnemyGroup(ref group);
                                        for (int ii = 0; ii < group.Count; ii++)
                                        {
                                            group[ii].RemoveBuffSpell();
                                            group[ii].CurrentAusterityMatrixOmega = 3;
                                            group[ii].ActivateBuff(group[ii].pbAusterityMatrixOmega, Database.BaseResourceFolder + Database.AUSTERITY_MATRIX_OMEGA + fileExt, 3);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "ヴォイス・オブ・アビス")
                                {
                                    UpdateBattleText(player.FirstName + "：深淵からの呼び声は、真実の理、コレを受け止めよ。\r\n");
                                    if (player.CurrentMana < Database.COST_VOICE_OF_ABYSS)
                                    {
                                        MissNotEnoughMana(player);
                                    }
                                    else
                                    {
                                        player.CurrentMana -= Database.COST_VOICE_OF_ABYSS;
                                        UpdateMana(player);
                                        List<MainCharacter> group = new List<MainCharacter>();
                                        SetupAllyGroup(ref group);
                                        // 【警告】敵だけ対象外なのは卑怯かもしれないので、要調整となる。
                                        //SetupEnemyGroup(ref group);
                                        for (int ii = 0; ii < group.Count; ii++)
                                        {
                                            group[ii].CurrentVoiceOfAbyss = 2;
                                            group[ii].ActivateBuff(group[ii].pbVoiceOfAbyss, Database.BaseResourceFolder + Database.VOICE_OF_ABYSS + fileExt, 2);
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region "５階"
                            case Database.ENEMY_PHOENIX:
                                if (player.ActionLabel.text == "戦慄の金切り声")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        if (AbstractMagicDamage(player, group[ii], 0, 0, 0, Database.SOUND_FLAME_STRIKE, 120, TruthActionCommand.MagicType.Fire_Ice, false, CriticalType.None))
                                        {
                                            NowPreStunning(group[ii], 1);
                                            NowStunning(player, group[ii], 1, false);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "焼き尽くす煉獄炎")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        AbstractMagicDamage(player, group[ii], 0, 0, 0, Database.LAVA_ANNIHILATION, 120, TruthActionCommand.MagicType.Fire, false, CriticalType.None);
                                    }
                                }
                                else if (player.ActionLabel.text == "輝ける生命")
                                {
                                    int current = player.CurrentMagicAttackUpValue;
                                    current += 2000;
                                    BuffUpMagicAttack(player, current, Database.INFINITY);
                                }
                                break;
                            case Database.ENEMY_JUDGEMENT:
                                if (player.ActionLabel.text == "聖者の裁き")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        if (AbstractMagicDamage(player, group[ii], 0, 0, 0, Database.SOUND_CELESTIAL_NOVA, 120, TruthActionCommand.MagicType.Light, false, CriticalType.None))
                                        {
                                            PlayerSpellWordOfPower(player, group[ii], 0, 0);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "福音")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    GroundOne.PlaySoundEffect("RiseOfImage");
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        double damage = AP.Math.RandomInteger(100) / 100.0f;
                                        PlayerLifeDown(player, group[ii], damage);
                                        NowNoGainLife(group[ii], Database.INFINITY);
                                    }
                                }
                                else if (player.ActionLabel.text == "解放の賛歌")
                                {
                                    PlayerSpellSaintPower(player, player);
                                    PlayerSpellShadowPact(player, player);
                                    PlayerSpellPsychicTrance(player, player);
                                    PlayerSpellBlindJustice(player, player);
                                }
                                break;
                            case Database.ENEMY_NINE_TAIL:
                                if (player.ActionLabel.text == "ベジェ・テイル・アタック")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        if (PlayerNormalAttack(player, group[ii], 2.0f, false, false))
                                        {
                                            NowBlind(player, group[ii], 1);
                                            NowSlip(player, group[ii], Database.INFINITY);
                                        }
                                    }
                                }
                                else if (player.ActionLabel.text == "喰らいつき")
                                {
                                    for (int ii = 0; ii < 3; ii++)
                                    {
                                        PlayerLifeHalfCurrent(player, target, 0);
                                    }
                                    NowSlip(player, target, 3);
                                }
                                else if (player.ActionLabel.text == "隕石を呼ぶ声")
                                {
                                    for (int ii = 0; ii < 10; ii++)
                                    {
                                        AbstractMagicDamage(player, target, 15, PrimaryLogic.AscendantMeteorValue(player, GroundOne.DuelMode), 0.3f, "FireBall", 120, TruthActionCommand.MagicType.Light_Fire, false, CriticalType.Random);
                                    }
                                }
                                break;
                            case Database.ENEMY_EMERALD_DRAGON:
                                if (player.ActionLabel.text == "圧死の視線")
                                {
                                    PlayerLifeOne(player, target);
                                    NowNoResurrection(player, target, Database.INFINITY);
                                }
                                else if (player.ActionLabel.text == "イル・メギド・ブレス")
                                {
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerLifeHalfMax(player, group[ii]);
                                    }
                                }
                                else if (player.ActionLabel.text == "炎と氷の爆発")
                                {
                                    AbstractMagicDamage(player, target, 0, PrimaryLogic.ZetaExplosionValue(player, GroundOne.DuelMode), 0, Database.SOUND_ZETA_EXPLOSION, 139, TruthActionCommand.MagicType.Fire_Ice, false, CriticalType.None);
                                }
                                break;
                            #endregion
                            #region "５階の守護者：Bystander the Emptiness"
                            case Database.ENEMY_BOSS_BYSTANDER_EMPTINESS:
                                if (player.StackCommandString == "キル・スピニングランサー")
                                {
                                    ((TruthEnemyCharacter)player).BossBeforeStay = false;
                                    UpdateBattleText(player.FirstName + "は巨大な無双の槍を形成し始めた！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    UpdateBattleText(ec1.FirstName + "「奥義　キル・スピニングランサー」発動！！\r\n");
                                    System.Threading.Thread.Sleep(500);
                                    double damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode);
                                    PlayerNormalAttack(player, target, 12.0f, 0, false, true, 0, 0, string.Empty, -1, true, CriticalType.Absolute);

                                }
                                else if (player.StackCommandString == "タイダル・ウェイブ")
                                {
                                    // 全体ダメージ
                                    UpdateBattleText(player.FirstName + "は、大きな津波を発生させてきた！\r\n");
                                    PlayerMagicAttackAllEnemy(player, PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 5.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) * 3.0F, "FrozenLance", TruthActionCommand.MagicType.Ice);
                                    break;
                                }
                                else if (player.StackCommandString == "アース・コールド・シェイク")
                                {
                                    // 全体：ダメージ＋スタン＋凍結＋麻痺
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);

                                    foreach (MainCharacter current in group)
                                    {
                                        if (PlayerNormalAttack(player, current, 10.0F, 0, false, false, 0, 0, string.Empty, -1, false, CriticalType.None))
                                        {
                                            int rand = AP.Math.RandomInteger(3);
                                            switch (rand)
                                            {
                                                case 0:
                                                    NowParalyze(player, current, 2);
                                                    break;
                                                case 1:
                                                    NowStunning(player, current, 2, false);
                                                    break;
                                                case 2:
                                                    NowFrozen(player, current, 2);
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (player.StackCommandString == "虚無の鼓動")
                                {
                                    UpdateBattleText(player.FirstName + "：全て実像は等しく虚構である。虚無の理を受け入れよ。\r\n");
                                    List<MainCharacter> group = new List<MainCharacter>();
                                    SetupAllyGroup(ref group);
                                    double amplify = BattleTurnCount / 999.0f;
                                    if (amplify > 1.0f) { amplify = 1.0f; }
                                    for (int ii = 0; ii < group.Count; ii++)
                                    {
                                        PlayerLifeDown(player, group[ii], group[ii].CurrentLife * 0.5f * (1 + amplify));
                                    }
                                }
                                else if (player.StackCommandString == "完全絶対時間律【終焉】")
                                {
                                    // LavaAnnihilation級の究極ダメージ＋BUFFUP＋敵へDEBUFF
                                    player.ActionCommandStackList.Add(player.CurrentSpellName);
                                    player.ActionCommandStackTarget.Add(target);
                                }
                                else if (player.ActionLabel.text == "時間律の支配")
                                {
                                    UpdateBattleText(player.FirstName + "は微動だにせず、玉座に存在し続けている。時の刻のみが急速に進み続ける！\r\n");
                                    AnimationSandGlass();
                                    CleanUpStep();
                                    UpdateTurnEnd(true);
                                    UpkeepStep();
                                }
                                break;
                            #endregion
                            #region "最終戦【原罪】ヴェルゼ・アーティ"
                            case Database.ENEMY_LAST_SIN_VERZE_ARTIE:
                                if (player.ActionLabel.text == Database.FINAL_INVISIBLE_HUNDRED_CUTTER ||
                                    player.StackCommandString == Database.FINAL_INVISIBLE_HUNDRED_CUTTER)
                                {
                                    // 連続攻撃
                                    UpdateBattleText(player.FirstName + ":アイン君、これが最後です　【瘴技】インヴィジヴル・ハンドレッド・カッター！！！\r\n");
                                    if (withoutCost == false)
                                    {
                                        AnimationFinal("瘴技：Invisible Hundred Cutter");
                                        for (int ii = 0; ii < 15; ii++)
                                        {
                                            System.Threading.Thread.Sleep(1);
                                            PlayerNormalAttack(player, target, 0, 0, false, true, 0, 3, String.Empty, -1, false, CriticalType.Random);
                                            for (int jj = 0; jj < 10; jj++)
                                            {
                                                System.Threading.Thread.Sleep(30 / (ii + 1));
                                                player.BattleBarPos = (Database.BASE_TIMER_BAR_LENGTH / 10) * (jj + 1);
                                            }
                                        }
                                        PlayerNormalAttack(player, target, 2.0f, 0, false, true, 0, 100, String.Empty, -1, false, CriticalType.Random);
                                        player.BattleBarPos = 0;
                                    }
                                }
                                else if (player.ActionLabel.text == Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA ||
                                         player.StackCommandString == Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA)
                                {
                                    // 分身２体生成（生命カウンターが残り１つなら、３体生成)
                                    UpdateBattleText(player.FirstName + ":ックク、これは読み切れないでしょう 【叡技】ラダリュンテ・カオティック・スキーマ！！\r\n");
                                    if (withoutCost == false)
                                    {
                                        AnimationFinal("叡技：Ladarynte Chaotic Schema");
                                        PlayerBuffAbstract(player, player, 1, Database.CHAOTIC_SCHEMA);
                                        player.BattleBarPos = 0;
                                        if (player.CurrentLifeCountValue <= 1)
                                        {
                                            player.BattleBarPos2 = 167;
                                            player.BattleBarPos3 = 334;
                                        }
                                        else
                                        {
                                            player.BattleBarPos2 = 250;
                                        }
                                    }
                                }
                                // after Espelantieの内容を決定する事
                                //else if (player.ActionLabel.text == Database.FINAL_ADEST_ESPELANTIE)
                                //{
                                //    // 
                                //    UpdateBattleText(player.FirstName + ":アイン君はなす術もない、ッハハハハ！　神技：Adest Espelantie！！\r\n");
                                //    if (withoutCost == false)
                                //    {
                                //        this.Invoke(new _AnimationFinal1(AnimationFinal1), "神技：Adest Espelantie");
                                //    }
                                //}
                                else if (player.ActionLabel.text == Database.FINAL_SEFINE_PAINFUL_HYMNUS ||
                                         player.StackCommandString == Database.FINAL_SEFINE_PAINFUL_HYMNUS)
                                {
                                    // 全回復
                                    UpdateBattleText(player.FirstName + ":今ここでセフィにボクのすべてを捧げる【永技：Sefine・Painful・Hymnus】！！\r\n");
                                    if (withoutCost == false)
                                    {
                                        AnimationFinal("永技：Sefine Painful Hymnus");
                                        double lifegain = player.MaxLife - player.CurrentLife;
                                        player.CurrentLife = player.MaxLife;
                                        UpdateLife(player, lifegain, true, true, 0, false);
                                        double managain = player.MaxMana - player.CurrentMana;
                                        player.CurrentMana = player.MaxMana;
                                        UpdateMana(player, managain, true, true, 0);
                                        double skillgain = player.MaxSkillPoint - player.CurrentSkillPoint;
                                        player.CurrentSkillPoint = player.MaxSkillPoint;
                                        UpdateSkillPoint(player, skillgain, true, true, 0);
                                    }
                                }
                                else if (player.ActionLabel.text == Database.FINAL_ZERO_INNOCENT_SIN ||
                                         player.StackCommandString == Database.FINAL_ZERO_INNOCENT_SIN)
                                {
                                    // ライフダウン１０回
                                    UpdateBattleText(player.FirstName + ":【絶技】ゼロ・イノセント・シン・・・これで終わりです。\r\n");
                                    if (withoutCost == false)
                                    {
                                        AnimationFinal("絶技：Zero Innocent Sin");
                                        Color[] colors = { UnityColor.WhiteSmoke, UnityColor.Gainsboro, UnityColor.Silver, UnityColor.Gray, UnityColor.SaddleBrown, UnityColor.DarkRed, UnityColor.Firebrick, UnityColor.Crimson, UnityColor.MediumVioletRed, UnityColor.Red };
                                        for (int ii = 0; ii < 10; ii++)
                                        {
                                            int sleepCount = 301 - (ii * 30);
                                            this.cam.backgroundColor = colors[ii];
                                            System.Threading.Thread.Sleep(sleepCount);
                                            PlayerLifeHalfCurrent(player, target, 10);
                                        }
                                        PlayerLifeHalfCurrent(player, target, 100);
                                        this.cam.backgroundColor = UnityColor.GhostWhite;
                                    }
                                }

                                break;
                            #endregion
                            #region "ダミー素振り君"
                            case Database.DUEL_DUMMY_SUBURI:
                                if (player.ActionLabel.text == "DOWN!")
                                {
                                    BuffDownPhysicalAttack(target, 1000, Database.INFINITY);
                                    BuffDownPhysicalDefense(target, 1000, Database.INFINITY);
                                    BuffDownMagicAttack(target, 1000, Database.INFINITY);
                                    BuffDownMagicDefense(target, 1000, Database.INFINITY);
                                    BuffDownBattleSpeed(target, 1000, Database.INFINITY);
                                    BuffDownBattleReaction(target, 1000, Database.INFINITY);
                                    BuffDownPotential(target, 1000, Database.INFINITY);
                                    //                        BuffDownPotential(target, 500, 2);

                                }
                                else if (player.ActionLabel.text == "BUFF!")
                                {
                                    //NowPreStunning(target, 999);
                                    NowStunning(player, target, 999, true);
                                    NowSilence(player, target, 999);
                                    //NowPoison(player, target, 999, true);
                                    //NowTemptation(player, target, 999);
                                    //NowParalyze(player, target, 999);
                                    //NowNoResurrection(player, target, 999);
                                    //NowSlow(player, target, 999);
                                    //NowBlind(player, target, 999);
                                    //NowSlip(player, target, 999);

                                    //NowBlinded(player, 999);
                                    //player.CurrentSpeedBoost = 100;

                                    //player.CurrentPhysicalAttackUp = Database.INFINITY;
                                    //player.CurrentPhysicalAttackUpValue = 1000;
                                    //player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);

                                    //player.CurrentPhysicalDefenseUp = Database.INFINITY;
                                    //player.CurrentPhysicalDefenseUpValue = 1000;
                                    //player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + "BuffPhysicalDefenseUp", Database.INFINITY);

                                    //player.CurrentMagicAttackUp = Database.INFINITY;
                                    //player.CurrentMagicAttackUpValue = 1000;
                                    //player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);

                                    //player.CurrentMagicDefenseUp = Database.INFINITY;
                                    //player.CurrentMagicDefenseUpValue = 1000;
                                    //player.ActivateBuff(player.pbMagicDefenseUp, Database.BaseResourceFolder + "BuffMagicDefenseUp", Database.INFINITY);

                                    //player.CurrentSpeedUp = Database.INFINITY;
                                    //player.CurrentSpeedUpValue = 1000;
                                    //player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);

                                    //player.CurrentReactionUp = Database.INFINITY;
                                    //player.CurrentReactionUpValue = 1000;
                                    //player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + "BuffReactionUp", Database.INFINITY);

                                    //player.CurrentPotentialUp = Database.INFINITY;
                                    //player.CurrentPotentialUpValue = 1000;
                                    //player.ActivateBuff(player.pbPotentialUp, Database.BaseResourceFolder + "BuffPotentialUp", Database.INFINITY);

                                    //BuffUpPotential(target, 1000);

                                    //BuffDownPhysicalDefense(target, 1000);

                                    //BuffDownMagicAttack(target, 1000);

                                    //BuffDownMagicDefense(target, 1000);

                                    //BuffDownBattleSpeed(target, 1000);

                                    //BuffDownBattleReaction(target, 1000);

                                    //BuffDownPotential(target, 1000);

                                    //player.CurrentStrengthUp = Database.INFINITY;
                                    //player.CurrentStrengthUpValue = 1000;
                                    //player.CurrentAgilityUp = Database.INFINITY;
                                    //player.CurrentAgilityUpValue = 1000;
                                    //player.CurrentIntelligenceUp = Database.INFINITY;
                                    //player.CurrentIntelligenceUpValue = 1000;
                                    //player.CurrentStaminaUp = Database.INFINITY;
                                    //player.CurrentStaminaUpValue = 1000;
                                    //player.CurrentMindUp = Database.INFINITY;
                                    //player.CurrentMindUpValue = 1000;
                                    //player.ActivateBuff(player.pbStrengthUp, Database.BaseResourceFolder + "BuffStrengthUp", Database.INFINITY);
                                    //player.ActivateBuff(player.pbAgilityUp, Database.BaseResourceFolder + "BuffAgilityUp", Database.INFINITY);
                                    //player.ActivateBuff(player.pbIntelligenceUp, Database.BaseResourceFolder + "BuffIntelligenceUp", Database.INFINITY);
                                    //player.ActivateBuff(player.pbStaminaUp, Database.BaseResourceFolder + "BuffStaminaUp", Database.INFINITY);
                                    //player.ActivateBuff(player.pbMindUp, Database.BaseResourceFolder + "BuffMindUp", Database.INFINITY);

                                    //player.CurrentResistFireUp = Database.INFINITY;
                                    //player.CurrentResistFireUpValue = 1000;
                                    //player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", Database.INFINITY);

                                    //player.CurrentResistIceUp = Database.INFINITY;
                                    //player.CurrentResistIceUpValue = 1000;
                                    //player.ActivateBuff(player.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp", Database.INFINITY);
                                }
            //                    else if (player.ActionLabel.text == "弱体化「潜在能力」")
            //                    {
            //                        BuffDownPotential(target, 500, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「戦闘反応」")
            //                    {
            //                        BuffDownBattleReaction(target, 500, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「戦闘速度」")
            //                    {
            //                        BuffDownBattleSpeed(target, 500, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「魔法防御」")
            //                    {
            //                        BuffDownMagicDefense(target, 500, 10);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「物理防御」")
            //                    {
            //                        BuffDownPhysicalDefense(target, 500, 10);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「魔法攻撃」")
            //                    {
            //                        BuffDownMagicAttack(target, 500, 10);
            //                    }
            //                    else if (player.ActionLabel.text == "弱体化「物理攻撃」")
            //                    {
            //                        BuffDownPhysicalAttack(target, 2000, 10);
            //                    }
            //                    else if (player.ActionLabel.text == "凍結")
            //                    {
            //                        NowFrozen(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "麻痺")
            //                    {
            //                        NowParalyze(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "沈黙")
            //                    {
            //                        NowSilence(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "暗闇")
            //                    {
            //                        NowBlind(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "誘惑")
            //                    {
            //                        NowTemptation(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "鈍化")
            //                    {
            //                        NowSlow(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "スタン")
            //                    {
            //                        NowStunning(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "猛毒")
            //                    {
            //                        NowPoison(player, target, 2, false);
            //                    }
            //                    else if (player.ActionLabel.text == "累積猛毒")
            //                    {
            //                        NowPoison(player, target, 999, true);
            //                    }
            //                    else if (player.ActionLabel.text == "スリップ")
            //                    {
            //                        NowSlip(player, target, 2);
            //                    }
            //                    else if (player.ActionLabel.text == "復活不可")
            //                    {
            //                        NowNoResurrection(player, target, 2);
            //                    }
                                break;
                            #endregion
                        }
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(120));
                    }
                    break;
                #endregion
            }
        }

        /////////////////////////////////////////////////////////////////////////
        // プレイヤーアクションセット                                          //
        /////////////////////////////////////////////////////////////////////////
        private void PlayerActionSet(MainCharacter player)
        {
            string commandName = player.ReserveBattleCommand;

            player.PA = TruthActionCommand.CheckPlayerActionFromString(commandName);
            player.MainObjectButton.image.sprite = Resources.Load<Sprite>(commandName);
            player.ActionLabel.text = TruthActionCommand.ConvertToJapanese(commandName);

            if (commandName == Database.ATTACK_EN)
            {
                UpdateBattleText(player.GetCharacterSentence(4001));
            }
            else if (commandName == Database.DEFENSE_EN)
            {
                UpdateBattleText(player.GetCharacterSentence(4002));
            }
            else if (commandName == Database.STAY_EN)
            {
                UpdateBattleText(player.GetCharacterSentence(4003));
            }
            else
            {
                if (player.PA == MainCharacter.PlayerAction.UseSpell)
                {
                    player.CurrentSpellName = commandName;
                }
                else if (player.PA == MainCharacter.PlayerAction.UseSkill)
                {
                    player.CurrentSkillName = commandName;
                }
                else if (player.PA == MainCharacter.PlayerAction.Archetype)
                {
                    player.CurrentArchetypeName = commandName;
                }
                else // ソレ以外はアイテム
                {
                    if (commandName == Database.ACCESSORY_SPECIAL_EN)
                    {
                        if (player.Accessory != null)
                        {
                            player.CurrentUsingItem = player.Accessory.Name;
                        }
                    }
                    else if (commandName == Database.ACCESSORY_SPECIAL2_EN)
                    {
                        if (player.Accessory2 != null)
                        {
                            player.CurrentUsingItem = player.Accessory2.Name;
                        }
                    }
                    else if (commandName == Database.WEAPON_SPECIAL_EN)
                    {
                        if (player.MainWeapon != null)
                        {
                            player.CurrentUsingItem = player.MainWeapon.Name;
                        }
                    }
                    else if (commandName == Database.WEAPON_SPECIAL_LEFT_EN)
                    {
                        if (player.SubWeapon != null)
                        {
                            player.CurrentUsingItem = player.SubWeapon.Name;
                        }
                    }
                }
                UpdateBattleText(player.GetCharacterSentence(4071));
            }
        }

        private void PreExecPlayArchetype(MainCharacter player, MainCharacter target, bool withoutCost, string CurrentArchetypeName)
        {
            // ところで、Archetypeに消費コストは存在しないため、そのまま元核を発動する。
            ExecPlayArchetype(player, target, CurrentArchetypeName);
        }

        private void ExecPlayArchetype(MainCharacter player, MainCharacter target, string CurrentArchetypeName)
        {
            // 元核は無条件で、カウンター対象外とする。
            if (CurrentArchetypeName == Database.ARCHETYPE_EIN)
            {
                PlayerArchetypeSyutyuDanzetsu(player, target);
            }
            else if (CurrentArchetypeName == Database.ARCHETYPE_RANA)
            {
                PlayerArchetypeJunkanSeiyaku(player);
            }
            else if (CurrentArchetypeName == Database.ARCHETYPE_OL)
            {
                PlayerArchetypeOraOraOraaa(player);
            }
            else if (CurrentArchetypeName == Database.ARCHETYPE_VERZE)
            {
                PlayerArchetypeShinzitsuHakai(player);
            }
            player.RemoveShiningAether();
        }

        private void PreExecPlaySpell(MainCharacter player, MainCharacter target, bool withoutCost, bool mainPhase, string CurrentSpellName)
        {
            Debug.Log("Call PreExecPlaySpell start");
            if ((!withoutCost) && (player.CurrentMana < TruthActionCommand.Cost(CurrentSpellName, player)))
            {
                Debug.Log("MissNotEnoughMana 2 " + player.CurrentMana.ToString() + " " + TruthActionCommand.Cost(CurrentSpellName, player));
                MissNotEnoughMana(player);
            }
            else if ((TruthActionCommand.GetTimingType(CurrentSpellName) == TruthActionCommand.TimingType.Sorcery) &&
                     (mainPhase) &&
                     (player.CurrentInstantPoint < player.MaxInstantPoint))
            {
                Debug.Log("MissNotEnoughInstant 3");
                MissNotEnoughInstant(player);
            }
            else if (player.CurrentAbsoluteZero > 0)
            {
                Debug.Log("MissSpellAbsoluteZero 4");
                MissSpellAbsoluteZero(player);
            }
            else
            {
                if (!withoutCost)
                {
                    player.CurrentMana -= TruthActionCommand.Cost(CurrentSpellName, player);
                    UpdateMana(player);

                    if (TruthActionCommand.GetTimingType(CurrentSpellName) == TruthActionCommand.TimingType.Sorcery)
                    {
                        player.CurrentInstantPoint = 0;
                        UseInstantPoint(player);
                    }
                }
                Debug.Log("Call ExecPlaySpell: " + CurrentSpellName);
                ExecPlaySpell(player, target, CurrentSpellName);
            }
        }

        private bool EffectCheckDarknessCoin(MainCharacter player)
        {
            if (((player.Accessory != null) && (player.Accessory.Name == Database.RARE_DARKNESS_COIN)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_DARKNESS_COIN)))
            {
                // ライフを消費して、発動
                double effectValue = PrimaryLogic.DarknessCoinValue(player);
                UpdateBattleText(player.FirstName + "が装着している" + Database.RARE_DARKNESS_COIN + "が禍々しいオーラを放つ！\r\n");
                UpdateBattleText(player.FirstName + "のライフが" + ((int)effectValue).ToString() + "削り取られ、スキル発動が強要された！\r\n");
                LifeDamage(effectValue, player);
                return true;
            }
            return false;
        }
        private void PreExecPlaySkill(MainCharacter player, MainCharacter target, bool withoutCost, bool mainPhase, string commandName)
        {
            if ((!withoutCost) && (player.CurrentSkillPoint < TruthActionCommand.Cost(commandName, player)))
            {
                if (EffectCheckDarknessCoin(player))
                {
                    // 代償を支払ったため、スルー
                }
                else
                {
                    MissNotEnoughSkill(player);
                }
            }
            else if ((TruthActionCommand.GetTimingType(commandName) == TruthActionCommand.TimingType.Sorcery) &&
                     (mainPhase) &&
                     (player.CurrentInstantPoint < player.MaxInstantPoint))
            {
                MissNotEnoughInstant(player);
            }
            else if (player.CurrentAbsoluteZero > 0)
            {
                MissSkillAbsoluteZero(player);
            }
            else
            {
                if (!withoutCost)
                {
                    player.CurrentSkillPoint -= TruthActionCommand.Cost(commandName, player);
                    UpdateSkillPoint(player);

                    if (TruthActionCommand.GetTimingType(commandName) == TruthActionCommand.TimingType.Sorcery)
                    {
                        player.CurrentInstantPoint = 0;
                        UseInstantPoint(player);
                    }
                }
                Debug.Log("Call ExecPlaySkill: " + commandName);
                ExecPlaySkill(player, target, commandName);
            }
        }

        private void ExecPlaySpell(MainCharacter player, MainCharacter target, string CurrentSpellName)
        {
            if (CheckStanceOfEyes(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckNegateCounter(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckDeepMirror(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckHymnContract(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckFutureVision(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckStanceOfMystic(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckStanceOfSuddenness(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckSilence(player)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }
            if (CheckCancelSpell(player, CurrentSpellName)) { if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); } return; }

            // 聖 //
            if (CurrentSpellName == Database.FRESH_HEAL)
            {
                PlayerSpellFreshHeal(player, target);
            }
            else if (CurrentSpellName == Database.PROTECTION)
            {
                PlayerSpellProtection(player, target);
            }
            else if (CurrentSpellName == Database.HOLY_SHOCK)
            {
                PlayerSpellHolyShock(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.SAINT_POWER)
            {
                PlayerSpellSaintPower(player, target);
            }
            else if (CurrentSpellName == Database.GLORY)
            {
                PlayerSpellGlory(player);
            }
            else if (CurrentSpellName == Database.RESURRECTION)
            {
                PlayerSpellResurrection(player, target);
            }
            else if (CurrentSpellName == Database.CELESTIAL_NOVA)
            {
                PlayerSpellCelestialNova(player, target);
            }
            // 闇 //
            else if (CurrentSpellName == Database.DARK_BLAST)
            {
                PlayerSpellDarkBlast(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.SHADOW_PACT)
            {
                PlayerSpellShadowPact(player, target);
            }
            else if (CurrentSpellName == Database.LIFE_TAP)
            {
                PlayerSpellLifeTap(player, target);
            }
            else if (CurrentSpellName == Database.BLACK_CONTRACT)
            {
                PlayerSpellBlackContract(player, target);
            }
            else if (CurrentSpellName == Database.DEVOURING_PLAGUE)
            {
                PlayerSpellDevouringPlague(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.BLOODY_VENGEANCE)
            {
                PlayerSpellBloodyVengeance(player, target);
            }
            else if (CurrentSpellName == Database.DAMNATION)
            {
                PlayerSpellDamnation(player, target);
            }
            // 火 //
            else if (CurrentSpellName == Database.FIRE_BALL)
            {
                PlayerSpellFireBall(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.FLAME_AURA)
            {
                PlayerSpellFlameAura(player, target);
            }
            else if (CurrentSpellName == Database.HEAT_BOOST)
            {
                PlayerSpellHeatBoost(player, target);
            }
            else if (CurrentSpellName == Database.FLAME_STRIKE)
            {
                PlayerSpellFlameStrike(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.VOLCANIC_WAVE)
            {
                PlayerSpellVolcanicWave(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.IMMORTAL_RAVE)
            {
                PlayerSpellImmortalRave(player);
            }
            else if (CurrentSpellName == Database.LAVA_ANNIHILATION)
            {
                PlayerSpellLavaAnnihilation(player, target);
            }
            // 水 //
            else if (CurrentSpellName == Database.ICE_NEEDLE)
            {
                PlayerSpellIceNeedle(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.ABSORB_WATER)
            {
                PlayerSpellAbsorbWater(player, target);
            }
            else if (CurrentSpellName == Database.CLEANSING)
            {
                PlayerSpellCleansing(player, target);
            }
            else if (CurrentSpellName == Database.FROZEN_LANCE)
            {
                PlayerSpellFrozenLance(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.MIRROR_IMAGE)
            {
                PlayerSpellMirrorImage(player, target);
            }
            else if (CurrentSpellName == Database.PROMISED_KNOWLEDGE)
            {
                PlayerSpellPromisedKnowledge(player, target);
            }
            else if (CurrentSpellName == Database.ABSOLUTE_ZERO)
            {
                PlayerSpellAbsoluteZero(player, target);
            }
            // 理 //
            else if (CurrentSpellName == Database.WORD_OF_POWER)
            {
                PlayerSpellWordOfPower(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.GALE_WIND)
            {
                PlayerSpellGaleWind(player);
            }
            else if (CurrentSpellName == Database.WORD_OF_LIFE)
            {
                PlayerSpellWordOfLife(player, target);
            }
            else if (CurrentSpellName == Database.WORD_OF_FORTUNE)
            {
                PlayerSpellWordOfFortune(player, target);
            }
            else if (CurrentSpellName == Database.AETHER_DRIVE)
            {
                PlayerSpellAetherDrive(player);
            }
            else if (CurrentSpellName == Database.GENESIS)
            {
                PlayerSpellGenesis(player, player.BeforeTarget);
            }
            else if (CurrentSpellName == Database.ETERNAL_PRESENCE)
            {
                PlayerSpellEternalPresence(player, target);
            }
            // 空
            else if (CurrentSpellName == Database.DISPEL_MAGIC)
            {
                PlayerSpellDispelMagic(player, target);
            }
            else if (CurrentSpellName == Database.RISE_OF_IMAGE)
            {
                PlayerSpellRiseOfImage(player, target);
            }
            else if (CurrentSpellName == Database.DEFLECTION)
            {
                PlayerSpellDeflection(player, target);
            }
            else if (CurrentSpellName == Database.TRANQUILITY)
            {
                PlayerSpellTranquility(player, target);
            }
            else if (CurrentSpellName == Database.ONE_IMMUNITY)
            {
                PlayerSpellOneImmunity(player);
            }
            else if (CurrentSpellName == Database.WHITE_OUT)
            {
                PlayerSpellWhiteOut(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.TIME_STOP)
            {
                PlayerSpellTimeStop(player, target);
            }
            // 聖＋闇 //
            else if (CurrentSpellName == Database.PSYCHIC_TRANCE)
            {
                PlayerSpellPsychicTrance(player, target);
            }
            else if (CurrentSpellName == Database.BLIND_JUSTICE)
            {
                PlayerSpellBlindJustice(player, target);
            }
            else if (CurrentSpellName == Database.TRANSCENDENT_WISH)
            {
                PlayerSpellTranscendentWish(player, target);
            }
            // 聖＋火 //
            else if (CurrentSpellName == Database.FLASH_BLAZE)
            {
                PlayerSpellFlashBlaze(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.LIGHT_DETONATOR)
            {
                PlayerSpellLightDetonator(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.ASCENDANT_METEOR)
            {
                PlayerSpellAscendantMeteor(player, target, 0, 0);
                // こちらでマナ全消費
                player.CurrentMana = 0;
                UpdateMana(player);
            }
            // 聖＋水 //
            else if (CurrentSpellName == Database.SKY_SHIELD)
            {
                PlayerSpellSkyShield(player, target);
            }
            else if (CurrentSpellName == Database.SACRED_HEAL)
            {
                PlayerSpellSacredHeal(player, target);
            }
            else if (CurrentSpellName == Database.EVER_DROPLET)
            {
                PlayerSpellEverDroplet(player, target);
            }
            // 聖＋理
            else if (CurrentSpellName == Database.HOLY_BREAKER)
            {
                PlayerSpellHolyBreaker(player, target);
            }
            else if (CurrentSpellName == Database.EXALTED_FIELD)
            {
                PlayerSpellExaltedField(player, target);
            }
            else if (CurrentSpellName == Database.HYMN_CONTRACT)
            {
                PlayerSpellHymnContract(player, target);
            }
            // 聖＋空
            else if (CurrentSpellName == Database.STAR_LIGHTNING)
            {
                PlayerSpellStarLightning(player, target);
            }
            else if (CurrentSpellName == Database.ANGEL_BREATH)
            {
                PlayerSpellAngelBreath(player, target);
            }
            else if (CurrentSpellName == Database.ENDLESS_ANTHEM)
            {
                PlayerSpellEndlessAnthem(player, target);
            }
            // 闇＋火
            else if (CurrentSpellName == Database.BLACK_FIRE)
            {
                PlayerSpellBlackFire(player, target);
            }
            else if (CurrentSpellName == Database.BLAZING_FIELD)
            {
                PlayerSpellBlazingField(player, target);
            }
            else if (CurrentSpellName == Database.DEMONIC_IGNITE)
            {
                PlayerSpellDemonicIgnite(player, target);
            }
            // 闇＋水
            else if (CurrentSpellName == Database.BLUE_BULLET)
            {
                PlayerSpellBlueBullet(player, target, 0, 0);
            }
            else if (CurrentSpellName == Database.DEEP_MIRROR)
            {
                PlayerSpellDeepMirror(player, target);
            }
            else if (CurrentSpellName == Database.DEATH_DENY)
            {
                PlayerSpellDeathDeny(player, target);
            }
            // 闇＋理
            else if (CurrentSpellName == Database.WORD_OF_MALICE)
            {
                PlayerSpellWordOfMalice(player, target);
            }
            else if (CurrentSpellName == Database.ABYSS_EYE)
            {
                PlayerSpellAbyssEye(player, target);
            }
            else if (CurrentSpellName == Database.SIN_FORTUNE)
            {
                PlayerSpellSinFortune(player, target);
            }
            // 闇＋空
            else if (CurrentSpellName == Database.DARKEN_FIELD)
            {
                PlayerSpellDarkenField(player, target);
            }
            else if (CurrentSpellName == Database.DOOM_BLADE)
            {
                PlayerSpellDoomBlade(player, target);
            }
            else if (CurrentSpellName == Database.ECLIPSE_END)
            {
                PlayerSpellEclipseEnd(player, target);
            }
            // 火＋水
            else if (CurrentSpellName == Database.FROZEN_AURA)
            {
                PlayerSpellFrozenAura(player, target);
            }
            else if (CurrentSpellName == Database.CHILL_BURN)
            {
                PlayerSpellChillBurn(player, target);
            }
            else if (CurrentSpellName == Database.ZETA_EXPLOSION)
            {
                PlayerSpellZetaExplosion(player, target);
            }
            // 火＋理
            else if (CurrentSpellName == Database.ENRAGE_BLAST)
            {
                PlayerSpellEnrageBlast(player, target);
            }
            else if (CurrentSpellName == Database.PIERCING_FLAME)
            {
                PlayerSpellPiercingFlame(player, target);
            }
            else if (CurrentSpellName == Database.SIGIL_OF_HOMURA)
            {
                PlayerSpellSigilOfHomura(player, target);
            }
            // 火＋空
            else if (CurrentSpellName == Database.IMMOLATE)
            {
                PlayerSpellImmolate(player, target);
            }
            else if (CurrentSpellName == Database.PHANTASMAL_WIND)
            {
                PlayerSpellPhantasmalWind(player, target);
            }
            else if (CurrentSpellName == Database.RED_DRAGON_WILL)
            {
                PlayerSpellRedDragonWill(player, target);
            }
            // 水＋理
            else if (CurrentSpellName == Database.WORD_OF_ATTITUDE)
            {
                PlayerSpellWordOfAttitude(player, target);
            }
            else if (CurrentSpellName == Database.STATIC_BARRIER)
            {
                PlayerSpellStaticBarrier(player, target);
            }
            else if (CurrentSpellName == Database.AUSTERITY_MATRIX)
            {
                PlayerSpellAusterityMatrix(player, target);
            }
            // 水＋空
            else if (CurrentSpellName == Database.VANISH_WAVE)
            {
                PlayerSpellVanishWave(player, target);
            }
            else if (CurrentSpellName == Database.VORTEX_FIELD)
            {
                PlayerSpellVortexField(player, target);
            }
            else if (CurrentSpellName == Database.BLUE_DRAGON_WILL)
            {
                PlayerSpellBlueDragonWill(player, target);
            }
            // 理＋空（完全逆)
            else if (CurrentSpellName == Database.SEVENTH_MAGIC)
            {
                PlayerSpellSeventhMagic(player, target);
            }
            else if (CurrentSpellName == Database.PARADOX_IMAGE)
            {
                PlayerSpellParadoxImage(player, target);
            }
            else if (CurrentSpellName == Database.WARP_GATE)
            {
                PlayerSpellWarpGate(player, target);
            }

            if (TruthActionCommand.IsDamage(CurrentSpellName)) { UpdateCurrentChargeCount(player); }
        }

        private void ExecPlaySkill(MainCharacter player, MainCharacter target, string CurrentSkillName)
        {
            if (CheckStanceOfEyes(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckCounterAttack(player, CurrentSkillName)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckDeepMirror(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckHymnContract(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckFutureVision(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckStanceOfMystic(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }
            if (CheckStanceOfSuddenness(player)) { if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); } return; }

            // 動 //
            if (CurrentSkillName == Database.STRAIGHT_SMASH)
            {
                PlayerSkillStraightSmash(player, target, 0, false);
            }
            else if (CurrentSkillName == Database.DOUBLE_SLASH)
            {
                PlayerSkillDoubleSlash(player, target, 0, false);
            }
            else if (CurrentSkillName == Database.CRUSHING_BLOW)
            {
                PlayerSkillCrushingBlow(player);
            }
            else if (CurrentSkillName == Database.SOUL_INFINITY)
            {
                PlayerSkillSoulInfinity(player, target);
            }
            // 静 //
            else if (CurrentSkillName == Database.COUNTER_ATTACK)
            {
                PlayerSkillCounterAttack(player, target);
            }
            else if (CurrentSkillName == Database.PURE_PURIFICATION)
            {
                PlayerSkillPurePurification(player, target);
            }
            else if (CurrentSkillName == Database.ANTI_STUN)
            {
                PlayerSkillAntiStun(player, target);
            }
            else if (CurrentSkillName == Database.STANCE_OF_DEATH)
            {
                PlayerSkillStanceOfDeath(player);
            }
            // 柔 //
            else if (CurrentSkillName == Database.STANCE_OF_FLOW)
            {
                PlayerSkillStanceOfFlow(player);
            }
            else if (CurrentSkillName == Database.ENIGMA_SENSE)
            {
                PlayerSkillEnigmaSense(player, target);
            }
            else if (CurrentSkillName == Database.SILENT_RUSH)
            {
                PlayerSkillSilentRush(player, target);
            }
            else if (CurrentSkillName == Database.OBORO_IMPACT)
            {
                PlayerSkillOboroImpact(player, target);
            }
            // 剛 //
            else if (CurrentSkillName == Database.STANCE_OF_STANDING)
            {
                PlayerSkillStanceOfStanding(player, target);
            }
            else if (CurrentSkillName == Database.INNER_INSPIRATION)
            {
                PlayerSkillInnerInspiration(player);
            }
            else if (CurrentSkillName == Database.KINETIC_SMASH)
            {
                PlayerSkillKineticSmash(player, target);
            }
            else if (CurrentSkillName == Database.CATASTROPHE)
            {
                PlayerSkillCatastrophe(player, target);
                // こちらでスキル全消費
                player.CurrentSkillPoint = 0;
                UpdateSkillPoint(player);
            }
            // 心眼 //
            else if (CurrentSkillName == Database.TRUTH_VISION)
            {
                PlayerSkillTruthVision(player, target);
            }
            else if (CurrentSkillName == Database.HIGH_EMOTIONALITY)
            {
                PlayerSkillHighEmotionality(player);
            }
            else if (CurrentSkillName == Database.STANCE_OF_EYES)
            {
                PlayerSkillStanceOfEyes(player);
            }
            else if (CurrentSkillName == Database.PAINFUL_INSANITY)
            {
                PlayerSkillPainfulInsanity(player);
            }
            // 無心 //
            else if (CurrentSkillName == Database.NEGATE)
            {
                PlayerSkillNegate(player, target);
            }
            else if (CurrentSkillName == Database.VOID_EXTRACTION)
            {
                PlayerSkillVoidExtraction(player);
            }
            else if (CurrentSkillName == Database.CARNAGE_RUSH)
            {
                PlayerSkillCarnageRush(player, target);
            }
            else if (CurrentSkillName == Database.NOTHING_OF_NOTHINGNESS)
            {
                PlayerSkillNothingOfNothingness(player);
            }
            // 動＋静（完全逆）
            else if (CurrentSkillName == Database.NEUTRAL_SMASH)
            {
                PlayerSkillNeutralSmash(player, target);
            }
            else if (CurrentSkillName == Database.STANCE_OF_DOUBLE)
            {
                PlayerSkillStanceOfDouble(player, target);
            }
            // 動＋柔
            else if (CurrentSkillName == Database.SWIFT_STEP)
            {
                PlayerSkillSwiftStep(player, target);
            }
            else if (CurrentSkillName == Database.VIGOR_SENSE)
            {
                PlayerSkillVigorSense(player, target);
            }
            // 動＋剛
            else if (CurrentSkillName == Database.CIRCLE_SLASH)
            {
                PlayerSkillCircleSlash(player, target);
            }
            else if (CurrentSkillName == Database.RISING_AURA)
            {
                PlayerSkillRisingAura(player, target);
            }
            // 動＋心眼
            else if (CurrentSkillName == Database.RUMBLE_SHOUT)
            {
                PlayerSkillRumbleShout(player, target);
            }
            else if (CurrentSkillName == Database.ONSLAUGHT_HIT)
            {
                PlayerSkillOnslaughtHit(player, target);
            }
            // 動＋無心
            else if (CurrentSkillName == Database.SMOOTHING_MOVE)
            {
                PlayerSkillSmoothingMove(player, target);
            }
            else if (CurrentSkillName == Database.ASCENSION_AURA)
            {
                PlayerSkillAscensionAura(player, target);
            }
            // 静＋柔
            else if (CurrentSkillName == Database.FUTURE_VISION)
            {
                PlayerSkillFutureVision(player, target);
            }
            else if (CurrentSkillName == Database.UNKNOWN_SHOCK)
            {
                PlayerSkillUnknownShock(player, target);
            }
            // 静＋剛
            else if (CurrentSkillName == Database.REFLEX_SPIRIT)
            {
                PlayerSkillReflexSpirit(player, target);
            }
            else if (CurrentSkillName == Database.FATAL_BLOW)
            {
                PlayerSkillFatalBlow(player, target);
            }
            // 静＋心眼
            else if (CurrentSkillName == Database.SHARP_GLARE)
            {
                PlayerSkillSharpGlare(player, target);
            }
            else if (CurrentSkillName == Database.CONCUSSIVE_HIT)
            {
                PlayerSkillConcussiveHit(player, target);
            }
            // 静＋無心
            else if (CurrentSkillName == Database.TRUST_SILENCE)
            {
                PlayerSkillTrustSilence(player, target);
            }
            else if (CurrentSkillName == Database.MIND_KILLING)
            {
                PlayerSkillMindKilling(player, target);
            }
            // 柔＋剛（完全逆）
            else if (CurrentSkillName == Database.SURPRISE_ATTACK)
            {
                PlayerSkillSurpriseAttack(player, target);
            }
            else if (CurrentSkillName == Database.STANCE_OF_MYSTIC)
            {
                PlayerSkillStanceOfMystic(player, target);
            }
            // 柔＋心眼
            else if (CurrentSkillName == Database.PSYCHIC_WAVE)
            {
                PlayerSkillPsychicWave(player, target);
            }
            else if (CurrentSkillName == Database.NOURISH_SENSE)
            {
                PlayerSkillNourishSense(player, target);
            }
            // 柔＋無心
            else if (CurrentSkillName == Database.RECOVER)
            {
                PlayerSkillRecover(player, target);
            }
            else if (CurrentSkillName == Database.IMPULSE_HIT)
            {
                PlayerSkillImpulseHit(player, target);
            }
            // 剛＋心眼
            else if (CurrentSkillName == Database.VIOLENT_SLASH)
            {
                PlayerSkillViolentSlash(player, target);
            }
            else if (CurrentSkillName == Database.ONE_AUTHORITY)
            {
                PlayerSkillOneAuthority(player, target);
            }
            // 剛＋無心
            else if (CurrentSkillName == Database.OUTER_INSPIRATION)
            {
                PlayerSkillOuterInspiration(player, target);
            }
            else if (CurrentSkillName == Database.HARDEST_PARRY)
            {
                PlayerSkillHardestParry(player, target);
            }
            // 心眼＋無心（完全逆）
            else if (CurrentSkillName == Database.STANCE_OF_SUDDENNESS)
            {
                PlayerSkillStanceOfSuddenness(player, target);
            }
            else if (CurrentSkillName == Database.SOUL_EXECUTION)
            {
                PlayerSkillSoulExecution(player, target);
            }
            
            //if (TruthActionCommand.IsDamage(CurrentSkillName)) { UpdateCurrentPhysicalChargeCount(player); }
        }

        private void UpdateCurrentChargeCount(MainCharacter player)
        {
            if (player.CurrentChargeCount > 0)
            {
                player.CurrentChargeCount--;
            }
        }

        private void UpdateCurrentPhysicalChargeCount(MainCharacter player)
        {
            if (player.CurrentPhysicalChargeCount > 0)
            {
                player.CurrentPhysicalChargeCount--;
            }
        }

        private void MissNotEnoughMana(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(17));
            AnimationDamage(0, player, 0, Color.black, false, false, "マナ不足");
        }

        private void MissNotEnoughSkill(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(0));
            AnimationDamage(0, player, 0, Color.black, false, false, "スキル不足");
        }
        private void MissNotEnoughInstant(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(127));
            AnimationDamage(0, player, 0, Color.black, false, false, "インスタント不足");
        }
        private void MissSpellAbsoluteZero(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(76));
            AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_SPELL);
        }
        private void MissSkillAbsoluteZero(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(87));
            AnimationDamage(0, player, 0, Color.black, false, false, Database.MISS_SKILL);
        }
        
        private void PlayerMirrorImageAllAlly(MainCharacter player)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            SetupEnemyGroup(ref group);

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("MirrorImage");
                PlayerBuffAbstract(player, group[ii], 999, "MirrorImage");
            }
        }


        /// <summary>
        /// ライフダウン
        /// </summary>
        /// <param name="player">プレーヤー</param>
        /// <param name="target">ターゲット</param>
        /// <param name="effectValue">減少倍率（0.0から1.0で指定（1.0で即死）</param>
        private void PlayerLifeDown(MainCharacter player, MainCharacter target, double effectValue)
        {
            if (target.CurrentBlackElixir > 0)
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_LIFE_DOWN);
                return;
            }

            LifeDamage(effectValue, target);
        }

        private void PlayerLifeHalfMax(MainCharacter player, MainCharacter target)
        {
            if (target.CurrentBlackElixir > 0)
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_LIFE_DOWN);
                return;
            }

            double damage = (double)target.MaxLife / 2.0F;
            LifeDamage(damage, target);
        }

        private void PlayerLifeHalfCurrent(MainCharacter player, MainCharacter target, int interval = 0)
        {
            if (target.CurrentBlackElixir > 0)
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_LIFE_DOWN);
                return;
            }

            double damage = 0;
            if (target.CurrentLife <= 1)
            {
                damage = 1;
            }
            else
            {
                damage = (double)target.CurrentLife / 2.0F;
            }
            LifeDamage(damage, target, interval);
        }

        private void PlayerLifeOne(MainCharacter player, MainCharacter target)
        {
            if (target.CurrentBlackElixir > 0)
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_LIFE_DOWN);
                return;
            }

            double damage = target.CurrentLife - 1;
            target.CurrentLife = 1;
            UpdateLife(target, damage, false, true, 0, false);
        }

        private void PlayerDeath(MainCharacter player, MainCharacter target)
        {
            // 敵がボス系統の場合は発動しない事とする。
            if (target.GetType() == typeof(TruthEnemyCharacter))
            {
                if (((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Purple ||
                    ((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Gold ||
                    ((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Legendary)
                {
                    // ボスが自動耐性の場合、何も表示せずスルーする。
                    return;
                }
            }
            // 即死耐性のアクセサリ
            if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_ASSASSINATION))
            {
                UpdateBattleText(target + "が装備している" + target.Accessory + "が輝きだし、即死を防いだ！\r\n");
                AnimationDamage(0, target, 0, Color.black, false, false, "即死耐性");
                return;
            }
            if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_ASSASSINATION))
            {
                UpdateBattleText(target + "が装備している" + target.Accessory2 + "が輝きだし、即死を防いだ！\r\n");
                AnimationDamage(0, target, 0, Color.black, false, false, "即死耐性");
                return;
            }
            // 耐性効果
            if (target.CurrentGenseiTaima > 0)
            {
                UpdateBattleText(target + "の退魔効果が発動し、即死を防いだ！\r\n");
                AnimationDamage(0, target, 0, Color.black, false, false, "即死耐性");
                target.RemoveGenseiTaima();
                return;
            }

            double damage = target.CurrentLife;
            target.CurrentLife = 0;
            UpdateLife(target, damage, false, true, 0, false);
            //target.DeadPlayer();
        }

        private void NowPreStunning(MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_FEAR);
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_PRESTUNNING);
                target.CurrentPreStunning = effectTime;
                target.ActivateBuff(target.pbPreStunning, Database.BaseResourceFolder + "PreStunning", effectTime);
                UpdateBattleText(target.FirstName + "は恐怖に駆られた。\r\n");
            }
        }

        private void NowStunning(MainCharacter player, MainCharacter target, int effectTime, bool forceStun)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0 && !forceStun)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_STUN);
            }
            else if ((target.CheckResistStun || CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT) || CheckResistWithItem(target, Database.POOR_JUNK_TARISMAN_STUN)) && !forceStun)
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_STUN);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeStun = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_STUN);
                if (target.CurrentStunning <= 0) // 重ねがけは出来ない仕様
                {
                    target.CurrentStunning = effectTime;
                }
                target.ActivateBuff(target.pbStun, Database.BaseResourceFolder + "Stunning", target.CurrentStunning);
                UpdateBattleText(target.FirstName + "は気絶した。\r\n");
            }
        }

        private void NowSilence(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SILENCE);
            }
            else if ((target.CheckResistSilence) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SILENCE);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeSilence = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_SILENCE);
                target.CurrentSilence = effectTime;
                target.ActivateBuff(target.pbSilence, Database.BaseResourceFolder + "Silence", effectTime);
                UpdateBattleText(target.FirstName + "は沈黙にかかった。\r\n");
            }
        }

        private void NowPoison(MainCharacter player, MainCharacter target, int effectValue, bool cumulative)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_POISON);
            }
            else if ((target.CheckResistPoison) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_POISON);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBePoison = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_POISON);
                target.CurrentPoison = effectValue;
                if (cumulative)
                {
                    target.CurrentPoisonValue++;
                    target.ChangePoisonStatus(effectValue);
                }
                else
                {
                    target.CurrentPoisonValue = 1;
                    target.ActivateBuff(target.pbPoison, Database.BaseResourceFolder + "Poison", effectValue);
                }
                UpdateBattleText(target.FirstName + "は猛毒におかされた。\r\n");
            }
        }

        private void NowTemptation(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_TEMPTATION);
            }
            else if ((target.CheckResistTemptation) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_TEMPTATION);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeTemptation = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_TEMPTATION);
                target.CurrentTemptation = effectTime;
                target.ActivateBuff(target.pbTemptation, Database.BaseResourceFolder + "Temptation", effectTime);
                UpdateBattleText(target.FirstName + "は誘惑にかかった。\r\n");
            }
        }

        private bool NowFrozen(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_FROZEN);
            }
            else if ((target.CheckResistFrozen) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)) ||
                     (CheckResistWithItem(target, Database.POOR_JUNK_TARISMAN_FROZEN)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_FROZEN);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeFrozen = true;
                }
                return false;
            }

            if (target.CurrentFrozen <= 0) // 重ねがけは出来ない仕様
            {
                target.CurrentFrozen = effectTime;
            }
            AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_FROZEN);
            target.ActivateBuff(target.pbFrozen, Database.BaseResourceFolder + "Frozen", effectTime);
            UpdateBattleText(target.FirstName + "は凍結した。\r\n");
            return true;
        }

        private bool NowParalyze(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_PARALYZE);
            }
            else if ((target.CheckResistParalyze) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)) ||
                     (CheckResistWithItem(target, Database.POOR_JUNK_TARISMAN_PARALYZE)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_PARALYZE);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeParalyze = true;
                }
                return false;
            }
            else
            {
                if (target.CurrentParalyze <= 0) // 重ねがけは出来ない仕様
                {
                    target.CurrentParalyze = effectTime;
                }
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_PARALYZE);
                target.ActivateBuff(target.pbParalyze, Database.BaseResourceFolder + "Paralyze", effectTime);
                UpdateBattleText(target.FirstName + "は麻痺した。\r\n");
            }
            return true;
        }

        private void NowSlow(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SLOW);
            }
            else if ((target.CheckResistSlow) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SLOW);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeSlow = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_SLOW);
                target.CurrentSlow = effectTime;
                target.ActivateBuff(target.pbSlow, Database.BaseResourceFolder + "Slow", effectTime);
                UpdateBattleText(target.FirstName + "は動きが鈍くなった。\r\n");
            }
        }

        private void NowBlind(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_BLIND);
            }
            else if ((target.CheckResistBlind) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_BLIND);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeBlind = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_BLIND);
                target.CurrentBlind = effectTime;
                target.ActivateBuff(target.pbBlind, Database.BaseResourceFolder + "Blind", effectTime);
                UpdateBattleText(target.FirstName + "は暗闇にかかった。\r\n");
            }
        }

        private void NowSlip(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if (target.CurrentAntiStun > 0)
            {
                target.RemoveAntiStun();
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SLIP);
            }
            else if ((target.CheckResistSlip) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_SLIP);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeSlip = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_SLIP);
                target.CurrentSlip = effectTime;
                target.ActivateBuff(target.pbSlip, Database.BaseResourceFolder + "Slip", effectTime);
                UpdateBattleText(target.FirstName + "はひどい傷を負った。\r\n");
            }
        }

        private void NowNoResurrection(MainCharacter player, MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else if ((target.CheckResistNoResurrection) ||
                     (CheckResistWithItem(target, Database.POOR_DIRTY_ANGEL_CONTRACT)))
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.RESIST_NORESURRECTION);
                if (player.GetType() == typeof(TruthEnemyCharacter))
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeNoResurrection = true;
                }
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_NORESURRECTION);
                target.CurrentNoResurrection = effectTime;
                target.ActivateBuff(target.pbNoResurrection, Database.BaseResourceFolder + "NoResurrection", effectTime);
                UpdateBattleText(target.FirstName + "はリザレクションによる復活が不可能になった。\r\n");
            }
        }

        private void NowNoGainLife(MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_NOGAIN_LIFE);
                target.CurrentNoGainLife = effectTime;
                target.ActivateBuff(target.pbNoGainLife, Database.BaseResourceFolder + "NoGainLife", effectTime);
                UpdateBattleText(target.FirstName + "はライフ回復が不可能になった。\r\n");
            }
        }

        private bool CheckResistWithItem(MainCharacter target, string itemName)
        {
            if ((target.MainWeapon != null) && (target.MainWeapon.Name == itemName))
            {
                target.MainWeapon.AfterBroken = true;
                return true;
            }
            else if ((target.SubWeapon != null) && (target.SubWeapon.Name == itemName))
            {
                target.SubWeapon.AfterBroken = true;
                return true;
            }
            else if ((target.MainArmor != null) && (target.MainArmor.Name == itemName))
            {
                target.MainArmor.AfterBroken = true;
                return true;
            }
            else if ((target.Accessory != null) && (target.Accessory.Name == itemName))
            {
                target.Accessory.AfterBroken = true;
                return true;
            }
            else if ((target.Accessory2 != null) && (target.Accessory2.Name == itemName))
            {
                target.Accessory2.AfterBroken = true;
                return true;
            }

            return false;
        }


        private void NowBlinded(MainCharacter target, int effectTime)
        {
            if (target.Dead)
            {
                // 何もしない
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_BLINDED);
                target.CurrentBlinded = effectTime;
                target.ActivateBuff(target.pbBlinded, Database.BaseResourceFolder + "Blinded", effectTime);
                UpdateBattleText(target.FirstName + "は退避状態に入った。\r\n");
            }
        }

        private void PlayerAllBlind(MainCharacter player)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                NowBlind(player, GroundOne.MC, 2);
            }

            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                NowBlind(player, GroundOne.SC, 2);
            }

            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                NowBlind(player, GroundOne.TC, 2);
            }
        }

        private void PlayerAllSilence(MainCharacter player)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                NowSilence(player, GroundOne.MC, 1);
            }

            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                NowSilence(player, GroundOne.SC, 1);
            }

            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                NowSilence(player, GroundOne.TC, 1);
            }
        }

        private void PlayerAllStun(MainCharacter player)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                NowStunning(player, GroundOne.MC, 1, false);
            }
            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                NowStunning(player, GroundOne.SC, 1, false);
            }
            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                NowStunning(player, GroundOne.TC, 1, false);
            }
        }

        private void PlayerRandomTargetPhysicalDamage(MainCharacter player, int attackNum, int speed, double magnification)
        {
            for (int ii = 0; ii < attackNum; ii++)
            {
                double effectValue = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, GroundOne.DuelMode) / 2;

                List<MainCharacter> group = new List<MainCharacter>();
                SetupAllyGroup(ref group);

                int tempdata = AP.Math.RandomInteger(group.Count);
                PlayerNormalAttack(player, group[tempdata], magnification, 0, false, false, 0, speed, String.Empty, -1, false, CriticalType.Random);
            }
        }

        private void PlayerRandomTargetDamage(MainCharacter player, int attackNum, string soundName, TruthActionCommand.MagicType type)
        {
            for (int ii = 0; ii < attackNum; ii++)
            {
                double effectValue = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, GroundOne.DuelMode) / 3;

                List<MainCharacter> group = new List<MainCharacter>();
                SetupAllyGroup(ref group);

                int tempdata = AP.Math.RandomInteger(group.Count);
                AbstractMagicDamage(player, group[tempdata], 30, ref effectValue, 0, soundName, 120, type, false, CriticalType.Random);
            }
        }

        private void PlayerSlowAllEnemy(MainCharacter player, int effectValue)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                GroundOne.MC.CurrentSlow = effectValue;
                GroundOne.MC.ActivateBuff(GroundOne.MC.pbSlow, Database.BaseResourceFolder + "Slow", effectValue);
                UpdateBattleText(GroundOne.MC.FirstName + "は動きが鈍くなった。\r\n");
            }
            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                GroundOne.SC.CurrentSlow = effectValue;
                GroundOne.SC.ActivateBuff(GroundOne.SC.pbSlow, Database.BaseResourceFolder + "Slow", effectValue);
                UpdateBattleText(GroundOne.SC.FirstName + "は動きが鈍くなった。\r\n");
            }
            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                GroundOne.TC.CurrentSlow = effectValue;
                GroundOne.TC.ActivateBuff(GroundOne.TC.pbSlow, Database.BaseResourceFolder + "Slow", effectValue);
                UpdateBattleText(GroundOne.TC.FirstName + "は動きが鈍くなった。\r\n");
            }
        }

        private void PlayerPhysicalAttackAllEnemy(MainCharacter player, double damage, string soundName)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                PlayerNormalAttack(player, GroundOne.MC, 0.0f, 0, false, false, damage, 0, String.Empty, -1, false, CriticalType.Random);
            }

            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                PlayerNormalAttack(player, GroundOne.SC, 0.0f, 0, false, false, damage, 0, String.Empty, -1, false, CriticalType.Random);
            }

            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                PlayerNormalAttack(player, GroundOne.TC, 0.0f, 0, false, false, damage, 0, String.Empty, -1, false, CriticalType.Random);
            }
        }
        
        private void PlayerMagicAttackAllEnemy(MainCharacter player, double damage, string soundName, TruthActionCommand.MagicType type)
        {
            if (GroundOne.MC != null && !GroundOne.MC.Dead)
            {
                AbstractMagicDamage(player, GroundOne.MC, 0, damage, 0, soundName, 120, type, false, CriticalType.Random);
            }

            if (GroundOne.SC != null && !GroundOne.SC.Dead)
            {
                AbstractMagicDamage(player, GroundOne.SC, 0, damage, 0, soundName, 120, type, false, CriticalType.Random);
            }

            if (GroundOne.TC != null && !GroundOne.TC.Dead)
            {
                AbstractMagicDamage(player, GroundOne.TC, 0, damage, 0, soundName, 120, type, false, CriticalType.Random);
            }
        }
    }
}

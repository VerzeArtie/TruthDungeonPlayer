﻿using UnityEngine;
using System.Collections;
using System;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        // [情報]：全てのＢＵＦＦ＿ＵＰ魔法は、ここへ集約されるようにしてください。
        // [警告]：ここに集約されている情報は味方プレイヤーのみを対象としています。敵味方区別無くいけるようにしてください。
        protected void PlayerBuffAbstract(MainCharacter player, MainCharacter target, string spellName, int effectTime = 0)
        {
            Debug.Log("playerbuffabstract start");
            string fileExt = "";

            if (effectTime == 0)
            {
                effectTime = TruthActionCommand.IsBuffTurn(spellName);
            }

            int effectValue = 0;
            if (target.CurrentAusterityMatrix > 0 || target.CurrentAusterityMatrixOmega > 0)
            {
                if (TruthActionCommand.GetBuffType(spellName) == TruthActionCommand.BuffType.Up)
                {
                    UpdateBattleText(target.FirstName + "はAusterityMatrixに支配されており、BUFFを付与できなかった！！\r\n");
                    AnimationDamage(0, target, 0, Color.black, false, false, Database.EFFECT_CANNOT_BUFF);
                    return;
                }
            }

            if (spellName == Database.TIME_STOP ||
                spellName == Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN ||
                spellName == Database.ITEMCOMMAND_BLACK_ELIXIR ||
                spellName == Database.ITEMCOMMAND_COLORLESS_ANTIDOTE ||
                spellName == Database.ITEMCOMMAND_DETACHMENT_ORB ||
                spellName == Database.ITEMCOMMAND_DEVIL_SUMMONER_TOME ||
                spellName == Database.ITEMCOMMAND_ELEMENTAL_SEAL ||
                spellName == Database.ITEMCOMMAND_ETERNAL_FATE ||
                spellName == Database.ITEMCOMMAND_FELTUS ||
                spellName == Database.ITEMCOMMAND_GENSEI_TAIMA ||
                spellName == Database.ITEMCOMMAND_JUZA_PHANTASMAL ||
                spellName == Database.ITEMCOMMAND_LIGHT_SERVANT ||
                spellName == Database.ITEMCOMMAND_MAZE_CUBE ||
                spellName == Database.ITEMCOMMAND_SHADOW_SERVANT ||
                spellName == Database.ITEMCOMMAND_SHINING_AETHER ||
                spellName == Database.ITEMCOMMAND_VOID_HYMNSONIA)
            {
                // タイムストップやアイテムコマンドのBUFF効果文字を出す必要はない。
            }
            else if (TruthActionCommand.GetTargetType(spellName) == TruthActionCommand.TargetType.Own)
            {
                AnimationDamage(0, player, 0, Color.black, false, false, Database.BUFF_EFFECT);
            }
            else
            {
                AnimationDamage(0, target, 0, Color.black, false, false, Database.BUFF_EFFECT);
            }

            switch (spellName)
            {
                case Database.DAMNATION:
                    target.CurrentDamnation = effectTime;
                    target.ActivateBuff(target.pbDamnation, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(37));
                    break;

                case Database.ABSOLUTE_ZERO:
                    target.CurrentAbsoluteZero = effectTime;
                    target.ActivateBuff(target.pbAbsoluteZero, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(81));
                    break;

                case Database.STANCE_OF_FLOW:
                    target.CurrentStanceOfFlow = effectTime;
                    target.ActivateBuff(target.pbStanceOfFlow, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(64));
                    break;

                case Database.ONE_IMMUNITY:
                    target.CurrentOneImmunity = effectTime;
                    target.ActivateBuff(target.pbOneImmunity, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(46));
                    break;

                case Database.GALE_WIND:
                    target.CurrentGaleWind = effectTime;
                    target.ActivateBuff(target.pbGaleWind, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(40));
                    break;

                case Database.AETHER_DRIVE:
                    target.CurrentAetherDrive = effectTime;
                    target.ActivateBuff(target.pbAetherDrive, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(43));
                    break;

                case Database.IMMORTAL_RAVE:
                    target.CurrentImmortalRave = effectTime;
                    target.ActivateBuff(target.pbImmortalRave, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(39));
                    break;

                case Database.BLACK_CONTRACT:
                    target.CurrentBlackContract = effectTime;
                    target.ActivateBuff(target.pbBlackContract, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(35));
                    break;

                case Database.GLORY:
                    target.CurrentGlory = effectTime;
                    target.ActivateBuff(target.pbGlory, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(24));
                    break;

                case Database.PROTECTION:
                    target.CurrentProtection = effectTime;
                    target.ActivateBuff(target.pbProtection, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(18));
                    break;

                case Database.ABSORB_WATER:
                    target.CurrentAbsorbWater = effectTime;
                    target.ActivateBuff(target.pbAbsorbWater, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(19));
                    break;

                case Database.SAINT_POWER:
                    target.CurrentSaintPower = effectTime;
                    target.ActivateBuff(target.pbSaintPower, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(20));
                    break;

                case Database.SHADOW_PACT:
                    target.CurrentShadowPact = effectTime;
                    target.ActivateBuff(target.pbShadowPact, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(21));
                    break;

                case Database.BLOODY_VENGEANCE:
                    effectValue = player.StandardIntelligence / 2;
                    if (effectValue <= 0) { effectValue = 1; }
                    if ((effectValue - target.BuffStrength_BloodyVengeance) > 0)
                    {
                        target.CurrentBloodyVengeance = effectTime;
                        target.ActivateBuff(target.pbBloodyVengeance, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(string.Format(player.GetCharacterSentence(22), Convert.ToString(effectValue - target.BuffStrength_BloodyVengeance)));
                        target.BuffStrength_BloodyVengeance += effectValue;
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.HEAT_BOOST:
                    effectValue = player.StandardIntelligence / 2;
                    if (effectValue <= 0) { effectValue = 1; }
                    if ((effectValue - target.BuffAgility_HeatBoost) > 0)
                    {
                        target.CurrentHeatBoost = effectTime;
                        target.ActivateBuff(target.pbHeatBoost, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(string.Format(player.GetCharacterSentence(38), Convert.ToString(effectValue - target.BuffAgility_HeatBoost)));
                        target.BuffAgility_HeatBoost += effectValue;
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.FLAME_AURA:
                    target.CurrentFlameAura = effectTime;
                    target.ActivateBuff(target.pbFlameAura, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(36));
                    break;

                case Database.PROMISED_KNOWLEDGE:
                    effectValue = player.StandardIntelligence / 2;
                    if (effectValue <= 0) { effectValue = 1; }
                    if ((effectValue - target.BuffIntelligence_PromisedKnowledge) > 0)
                    {
                        target.CurrentPromisedKnowledge = effectTime;
                        target.ActivateBuff(target.pbPromisedKnowledge, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(string.Format(player.GetCharacterSentence(83), Convert.ToString(effectValue - target.BuffIntelligence_PromisedKnowledge)));
                        target.BuffIntelligence_PromisedKnowledge += effectValue;
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.RISE_OF_IMAGE:
                    effectValue = player.StandardIntelligence / 2;
                    if (effectValue <= 0) { effectValue = 1; }
                    if ((effectValue - target.BuffMind_RiseOfImage) > 0)
                    {
                        target.CurrentRiseOfImage = effectTime;
                        target.ActivateBuff(target.pbRiseOfImage, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(string.Format(player.GetCharacterSentence(49), Convert.ToString(effectValue - target.BuffMind_RiseOfImage)));
                        target.BuffMind_RiseOfImage += effectValue;
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.WORD_OF_LIFE:
                    target.CurrentWordOfLife = effectTime;
                    target.ActivateBuff(target.pbWordOfLife, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(41));
                    break;

                case Database.WORD_OF_FORTUNE:
                    target.CurrentWordOfFortune = effectTime;
                    target.ActivateBuff(target.pbWordOfFortune, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(42));
                    break;

                case Database.ETERNAL_PRESENCE:
                    target.CurrentEternalPresence = effectTime;
                    target.ActivateBuff(target.pbEternalPresence, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(44));
                    UpdateBattleText(player.GetCharacterSentence(45));
                    break;

                case Database.MIRROR_IMAGE:
                    target.CurrentMirrorImage = effectTime;
                    target.ActivateBuff(target.pbMirrorImage, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(57), player.Target.FirstName));
                    break;

                case Database.DEFLECTION:
                    target.CurrentDeflection = effectTime;
                    target.ActivateBuff(target.pbDeflection, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(60), target.FirstName));
                    break;

                case Database.PSYCHIC_TRANCE:
                    target.CurrentPsychicTrance = effectTime;
                    target.ActivateBuff(target.pbPsychicTrance, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(129), target.FirstName));
                    break;

                case Database.BLIND_JUSTICE:
                    target.CurrentBlindJustice = effectTime;
                    target.ActivateBuff(target.pbBlindJustice, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(130), target.FirstName));
                    break;

                case Database.TRANSCENDENT_WISH:
                    if (target.CurrentTranscendentWish <= 0)
                    {
                        target.CurrentTranscendentWish = effectTime;
                        target.ActivateBuff(target.pbTranscendentWish, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(131), target.FirstName));

                        target.BuffStrength_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Strength));
                        target.BuffAgility_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Agility));
                        target.BuffIntelligence_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Intelligence));
                        target.BuffStamina_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Stamina));
                        target.BuffMind_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Mind));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.SKY_SHIELD:
                    target.CurrentSkyShield = effectTime;
                    target.CurrentSkyShieldValue = 3;
                    target.ActivateBuff(target.pbSkyShield, Database.SKY_SHIELD, effectTime);
                    target.ChangeSkyShieldStatus(target.CurrentSkyShield);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(134), target.FirstName));
                    break;

                case Database.STATIC_BARRIER:
                    target.CurrentStaticBarrier = effectTime;
                    target.CurrentStaticBarrierValue = 3;
                    target.ActivateBuff(target.pbStaticBarrier, Database.STATIC_BARRIER, effectTime);
                    target.ChangeStaticBarrierStatus(target.CurrentStaticBarrier);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(186), target.FirstName));
                    break;

                case Database.EVER_DROPLET:
                    target.CurrentEverDroplet = effectTime;
                    target.ActivateBuff(target.pbEverDroplet, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(136), target.FirstName));
                    break;

                case Database.FROZEN_AURA:
                    target.CurrentFrozenAura = effectTime;
                    target.ActivateBuff(target.pbFrozenAura, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(137), target.FirstName));
                    break;


                case Database.PAINFUL_INSANITY:
                    player.CurrentPainfulInsanity = effectTime;
                    player.ActivateBuff(player.pbPainfulInsanity, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(4));
                    break;

                case Database.FLASH_BLAZE_BUFF:
                    target.CurrentFlashBlazeCount = effectTime;
                    target.CurrentFlashBlazeFactor = (int)PrimaryLogic.FlashBlaze_A_Value(player, GroundOne.DuelMode);
                    target.ActivateBuff(target.pbFlashBlaze, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.STAR_LIGHTNING:
                    if (target.CurrentStarLightning <= 0) // スタン効果なので、累積させない。
                    {
                        target.CurrentStarLightning = effectTime;
                    }
                    target.ActivateBuff(target.pbStarLightning, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(target.FirstName + "は気絶した。\r\n");
                    break;

                case Database.WORD_OF_MALICE:
                    target.CurrentWordOfMalice = effectTime;
                    target.ActivateBuff(target.pbWordOfMalice, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.SIN_FORTUNE:
                    target.CurrentSinFortune = effectTime;
                    target.ActivateBuff(target.pbSinFortune, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(211), target.FirstName));
                    break;

                case Database.BLACK_FIRE:
                    target.CurrentBlackFire = effectTime;
                    target.ActivateBuff(target.pbBlackFire, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.ENRAGE_BLAST:
                    target.CurrentEnrageBlast = effectTime;
                    target.CurrentEnrageBlastFactor = (int)PrimaryLogic.EnrageBlast_A_Value(player, GroundOne.DuelMode);
                    target.ActivateBuff(target.pbEnrageBlast, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.SIGIL_OF_HOMURA:
                    target.CurrentSigilOfHomura = effectTime;
                    target.ActivateBuff(target.pbSigilOfHomura, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(206)));
                    break;

                case Database.IMMOLATE:
                    target.CurrentImmolate = effectTime;
                    target.ActivateBuff(target.pbImmolate, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.HOLY_BREAKER:
                    target.CurrentHolyBreaker = effectTime;
                    target.ActivateBuff(target.pbHolyBreaker, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(148));
                    break;

                case Database.HYMN_CONTRACT:
                    target.CurrentHymnContract = effectTime;
                    target.ActivateBuff(target.pbHymnContract, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.DARKEN_FIELD:
                    target.CurrentDarkenField = effectTime;
                    target.ActivateBuff(target.pbDarkenField, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.ECLIPSE_END:
                    target.CurrentEclipseEnd = effectTime;
                    target.ActivateBuff(target.pbEclipseEnd, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.BLAZING_FIELD:
                    target.CurrentBlazingField = effectTime;
                    target.CurrentBlazingFieldFactor = (int)PrimaryLogic.BlazingField_A_Value(player, GroundOne.DuelMode);
                    target.ActivateBuff(target.pbBlazingField, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.EXALTED_FIELD:
                    target.CurrentExaltedField = effectTime;
                    target.ActivateBuff(target.pbExaltedField, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.ONE_AUTHORITY:
                    target.CurrentOneAuthority = effectTime;
                    target.ActivateBuff(target.pbOneAuthority, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.RISING_AURA:
                    target.CurrentRisingAura = effectTime;
                    target.ActivateBuff(target.pbRisingAura, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.ASCENSION_AURA:
                    target.CurrentAscensionAura = effectTime;
                    target.ActivateBuff(target.pbAscensionAura, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.SEVENTH_MAGIC:
                    target.CurrentSeventhMagic = effectTime;
                    target.ActivateBuff(target.pbSeventhMagic, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(150));
                    break;

                case Database.PHANTASMAL_WIND:
                    target.CurrentPhantasmalWind = effectTime;
                    target.ActivateBuff(target.pbPhantasmalWind, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(183));
                    break;

                case Database.PARADOX_IMAGE:
                    target.CurrentParadoxImage = effectTime;
                    target.ActivateBuff(target.pbParadoxImage, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(184));
                    break;

                case Database.AUSTERITY_MATRIX:
                    target.CurrentAusterityMatrix = effectTime;
                    target.ActivateBuff(target.pbAusterityMatrix, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(207));
                    break;

                case Database.RED_DRAGON_WILL:
                    target.CurrentRedDragonWill = effectTime;
                    target.ActivateBuff(target.pbRedDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(208));
                    break;

                case Database.BLUE_DRAGON_WILL:
                    target.CurrentBlueDragonWill = effectTime;
                    target.ActivateBuff(target.pbBlueDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(209));
                    break;

                case Database.NOURISH_SENSE:
                    target.CurrentNourishSense = effectTime;
                    target.ActivateBuff(target.pbNourishSense, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.STANCE_OF_DOUBLE:
                    target.CurrentStanceOfDouble = effectTime;
                    target.ActivateBuff(target.pbStanceOfDouble, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.SWIFT_STEP:
                    target.CurrentSwiftStep = effectTime;
                    target.ActivateBuff(target.pbSwiftStep, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.VIGOR_SENSE:
                    target.CurrentVigorSense = effectTime;
                    target.ActivateBuff(target.pbVigorSense, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.COLORLESS_MOVE:
                    target.CurrentColorlessMove = effectTime;
                    target.ActivateBuff(target.pbColorlessMove, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.FUTURE_VISION:
                    target.CurrentFutureVision = effectTime;
                    target.ActivateBuff(target.pbFutureVision, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.REFLEX_SPIRIT:
                    target.CurrentReflexSpirit = effectTime;
                    target.ActivateBuff(target.pbReflexSpirit, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.CONCUSSIVE_HIT:
                    target.CurrentConcussiveHit = effectTime;
                    target.CurrentConcussiveHitValue++;
                    target.ChangeConcussiveHitStatus(target.CurrentConcussiveHit);
                    break;

                case Database.ONSLAUGHT_HIT:
                    target.CurrentOnslaughtHit = effectTime;
                    target.CurrentOnslaughtHitValue++;
                    target.ChangeOnslaughtHitStatus(target.CurrentOnslaughtHit);
                    break;

                case Database.IMPULSE_HIT:
                    target.CurrentImpulseHit = effectTime;
                    target.CurrentImpulseHitValue++;
                    target.ChangeImpulseHitStatus(target.CurrentImpulseHit);
                    break;

                case Database.ARCHETYPE_RANA:
                    if (target.CurrentJunkan_Seiyaku <= 0)
                    {
                        target.CurrentJunkan_Seiyaku = effectTime;
                        target.ActivateBuff(target.pbJunkanSeiyaku, Database.BaseResourceFolder + spellName, effectTime);
                    }
                    else
                    {
                        UpdateBattleText(target.GetCharacterSentence(92));
                    }
                    break;

                // 自分自身が対象
                case Database.STANCE_OF_MYSTIC:
                    target.CurrentStanceOfMystic = effectTime;
                    target.CurrentStanceOfMysticValue = 3;
                    target.ActivateBuff(target.pbStanceOfMystic, Database.STANCE_OF_MYSTIC, effectTime);
                    target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMystic);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(168), target.FirstName));
                    break;

                case Database.TRUTH_VISION:
                    player.CurrentTruthVision = effectTime;
                    target.ActivateBuff(target.pbTruthVision, Database.BaseResourceFolder + spellName, effectTime);
                    UpdateBattleText(player.GetCharacterSentence(63));
                    break;

                case Database.HIGH_EMOTIONALITY:
                    if (player.CurrentHighEmotionality <= 0)
                    {
                        //player.BuffStrength_HighEmotionality = player.Strength / 3;
                        //player.BuffAgility_HighEmotionality = player.Agility / 3;
                        //player.BuffIntelligence_HighEmotionality = player.Intelligence / 3;
                        double plusValue = (double)player.Stamina * 0.2f;
                        player.BuffStamina_HighEmotionality = (int)plusValue;
                        //player.BuffMind_HighEmotionality = player.Mind / 3;

                        PlayerAbstractLifeGain(player, player, 0, (int)plusValue, 0, String.Empty, 5002); // plusValue * 10を撤廃(2018/03/13)

                        player.CurrentHighEmotionality = effectTime;
                        target.ActivateBuff(target.pbHighEmotionality, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(85));
                        UpdateBattleText(player.GetCharacterSentence(86));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(82));
                    }
                    break;

                case Database.NEGATE:
                    player.CurrentNegate = effectTime;
                    player.ActivateBuff(player.pbNegate, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.STANCE_OF_EYES:
                    player.CurrentStanceOfEyes = effectTime;
                    player.ActivateBuff(player.pbStanceOfEyes, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.COUNTER_ATTACK:
                    player.CurrentCounterAttack = effectTime;
                    player.ActivateBuff(player.pbCounterAttack, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.STANCE_OF_STANDING:
                    player.CurrentStanceOfStanding = effectTime;
                    player.ActivateBuff(player.pbStanceOfStanding, Database.BaseResourceFolder + spellName, effectTime);
                    break;

                case Database.ANTI_STUN:
                    if (target.CurrentAntiStun <= 0)
                    {
                        target.CurrentAntiStun = effectTime;
                        target.ActivateBuff(target.pbAntiStun, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(93));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(92));
                    }
                    break;

                case Database.STANCE_OF_DEATH:
                    if (player.CurrentStanceOfDeath <= 0)
                    {
                        player.CurrentStanceOfDeath = effectTime;
                        target.ActivateBuff(target.pbStanceOfDeath, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(95));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(92));
                    }
                    break;

                case Database.NOTHING_OF_NOTHINGNESS:
                    if (player.CurrentNothingOfNothingness <= 0)
                    {
                        player.CurrentNothingOfNothingness = effectTime;
                        player.ActivateBuff(player.pbNothingOfNothingness, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(106));
                        UpdateBattleText(player.GetCharacterSentence(107));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(92));
                    }
                    break;

                case Database.ARCHETYPE_EIN:
                    if (player.CurrentSyutyu_Danzetsu <= 0)
                    {
                        player.CurrentSyutyu_Danzetsu = effectTime;
                        player.ActivateBuff(player.pbSyutyuDanzetsu, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(203));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(92));
                    }
                    break;

                case Database.TIME_STOP:
                    if (player.CurrentTimeStop <= 0)
                    {
                        player.CurrentTimeStop = effectTime;
                        player.CurrentTimeStopValue = 300; // todo
                        player.ActivateBuff(player.pbTimeStop, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(47));
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(92));
                    }
                    break;

                case Database.AFTER_REVIVE_HALF:
                    if (player.CurrentAfterReviveHalf <= 0)
                    {
                        player.CurrentAfterReviveHalf = effectTime;
                        player.ActivateBuff(player.pbAfterReviveHalf, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(212));
                    }
                    break;

                // アイテム効果
                case Database.ITEMCOMMAND_FELTUS:
                    player.CurrentFeltus = effectTime;
                    player.CurrentFeltusValue++;
                    player.ChangeFeltusStatus(player.CurrentFeltus);
                    UpdateBattleText(target.FirstName + "に、【神】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_JUZA_PHANTASMAL:
                    player.CurrentJuzaPhantasmal = effectTime;
                    player.CurrentJuzaPhantasmalValue++;
                    player.ChangeJuzaPhantasmalStatus(player.CurrentJuzaPhantasmal);
                    UpdateBattleText(target.FirstName + "に、【颯】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_ETERNAL_FATE:
                    player.CurrentEternalFateRing = effectTime;
                    player.CurrentEternalFateRingValue++;
                    player.ChangeEternalFateRingStatus(player.CurrentEternalFateRing);
                    UpdateBattleText(target.FirstName + "に、【轟】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_LIGHT_SERVANT:
                    player.CurrentLightServant = effectTime;
                    player.CurrentLightServantValue++;
                    player.ChangeLightServantStatus(player.CurrentLightServant);
                    UpdateBattleText(player.FirstName + "に、【聖】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_SHADOW_SERVANT:
                    player.CurrentShadowServant = effectTime;
                    player.CurrentShadowServantValue++;
                    player.ChangeShadowServantStatus(player.CurrentShadowServant);
                    UpdateBattleText(player.FirstName + "に、【闇】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN:
                    player.CurrentAdilBlueBurn = effectTime;
                    player.CurrentAdilBlueBurnValue++;
                    player.ChangeAdilBlueBurnStatus(player.CurrentAdilBlueBurn);
                    UpdateBattleText(target.FirstName + "に、【蒼】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_MAZE_CUBE:
                    player.CurrentMazeCube = effectTime;
                    player.CurrentMazeCubeValue++;
                    player.ChangeMazeCubeStatus(player.CurrentMazeCube);
                    UpdateBattleText(player.FirstName + "に、【迷】の蓄積カウンターが乗った！\r\n");
                    break;

                case Database.ITEMCOMMAND_DETACHMENT_ORB:
                    player.CurrentDetachmentOrb = effectTime;
                    player.ActivateBuff(player.pbDetachmentOrb, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    UpdateBattleText(player.FirstName + "に、全ダメージ無効の乖離フィールドが展開された！\r\n");
                    break;

                case Database.ITEMCOMMAND_DEVIL_SUMMONER_TOME:
                    player.CurrentDevilSummonerTome = effectTime;
                    player.ActivateBuff(player.pbDevilSummonerTome, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    UpdateBattleText(player.FirstName + "はアークデーモンを召喚した！\r\n");
                    break;

                case Database.ITEMCOMMAND_VOID_HYMNSONIA:
                    player.CurrentVoidHymnsonia = effectTime;
                    player.ActivateBuff(player.pbVoidHymnsonia, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    UpdateBattleText(player.FirstName + "は空虚な歌声に心を奪われた状態となった！\r\n");
                    break;

                case Database.ITEMCOMMAND_GENSEI_TAIMA:
                    player.CurrentGenseiTaima = effectTime;
                    player.ActivateBuff(player.pbGenseiTaima, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    UpdateBattleText(player.FirstName + "は退魔の薬を使用する事で、即死に対する恐怖感を振り払った！\r\n");
                    break;

                case Database.LIFE_COUNT:
                    player.CurrentLifeCount = effectTime;
                    player.CurrentLifeCountValue = 1;
                    player.pbLifeCount.CumulativeAlign = TruthImage.CumulativeTextAlign.Center;
                    player.ActivateBuff(player.pbLifeCount, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    player.CurrentLifeCountValue = 3;
                    player.ChangeLifeCountStatus(player.CurrentLifeCount);
                    UpdateBattleText(player.FirstName + "に生命力カウンターが３つ発生した！\r\n");
                    break;

                case Database.CHAOTIC_SCHEMA:
                    player.CurrentChaoticSchema = effectTime;
                    player.ActivateBuff(player.pbChaoticSchema, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                    break;

                default:
                    break;
            }
        }
    }
}
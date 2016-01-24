using UnityEngine;
using System.Collections;
using System;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        /// <summary>
        /// 魔法ダメージのロジック
        /// </summary>
        /// <param name="player">対象元</param>
        /// <param name="target">対象相手</param>
        /// <param name="command">コマンド名</param>
        /// <param name="interval">発動後のインターバル</param>
        /// <param name="damage">ダメージ</param> // ref参照　DevouringPlagueの参照元で回復量に逆算したものを使用するため
        /// <param name="magnification">増減倍率、０の場合は増減しない</param>
        /// <param name="soundName">効果音ファイル名</param>
        /// <param name="messageNumber">魔法ダメージメッセージ</param>
        /// <param name="magicType">魔法属性</param>
        /// <param name="ignoreTargetDefense">対象の防御を無視する場合、True</param>
        /// <param name="critical">クリティカル有効フラグ</param>
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target, string command,
            int interval, double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            return AbstractMagicDamage(player, target, command, interval, ref damage, magnification, soundName, messageNumber, magicType, ignoreTargetDefense, critical);
        }
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target, string command,
            int interval, ref double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            if (player.CurrentShadowPact > 0) { damage = damage * 1.3f; }
            target.CurrentLife -= (int)damage;
            if (target.CurrentLife < 0) { target.CurrentLife = 0; }
            UpdateLife(target);
            UpdateMessage(player.labelName.text + " to " + target.labelName.text + " " + command + " " + ((int)damage).ToString() + " \n");

            return true;
        }


        // [情報]：全てのＢＵＦＦ＿ＵＰ魔法は、ここへ集約されるようにしてください。
        // [警告]：ここに集約されている情報は味方プレイヤーのみを対象としています。敵味方区別無くいけるようにしてください。
        private void PlayerBuffAbstract(MainCharacter player, MainCharacter target, int effectTime, string spellName)
        {
            string fileExt = "";
            // if (target != null) // todo [!= null problem]
            //{
                int effectValue = 0;
                if (target.CurrentAusterityMatrix > 0 || target.CurrentAusterityMatrixOmega > 0)
                {
                    string spellNameWithoutExt = spellName.Substring(0, spellName.Length - 4);
                    if (TruthActionCommand.GetBuffType(spellNameWithoutExt) == TruthActionCommand.BuffType.Up)
                    {
                        UpdateBattleText(target.FirstName + "はAusterityMatrixに支配されており、BUFFを付与できなかった！！\r\n");
                        //this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.black, true, false, Database.EFFECT_CANNOT_BUFF); // todo
                        return;
                    }
                }

                switch (spellName)
                {
                    case "Damnation":
                        target.CurrentDamnation = effectTime;
                        target.ActivateBuff(target.pbDamnation, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(37));
                        break;
                    case "AbsoluteZero":
                        target.CurrentAbsoluteZero = effectTime;
                        target.ActivateBuff(target.pbAbsoluteZero, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(81));
                        break;
                    case "StanceOfFlow":
                        target.CurrentStanceOfFlow = effectTime;
                        target.ActivateBuff(target.pbStanceOfFlow, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(64));
                        break;
                    case "OneImmunity":
                        target.CurrentOneImmunity = effectTime;
                        target.ActivateBuff(target.pbOneImmunity, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(46));
                        break;
                    case "GaleWind":
                        target.CurrentGaleWind = effectTime;
                        target.ActivateBuff(target.pbGaleWind, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(40));
                        break;
                    case "AetherDrive":
                        target.CurrentAetherDrive = effectTime;
                        target.ActivateBuff(target.pbAetherDrive, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(43));
                        break;
                    case "ImmortalRave":
                        target.CurrentImmortalRave = effectTime;
                        target.ActivateBuff(target.pbImmortalRave, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(39));
                        break;
                    case "BlackContract":
                        target.CurrentBlackContract = effectTime;
                        target.ActivateBuff(target.pbBlackContract, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(35));
                        break;
                    case "Glory":
                        target.CurrentGlory = effectTime;
                        target.ActivateBuff(target.pbGlory, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(24));
                        break;
                    case "Protection":
                        target.CurrentProtection = effectTime;
                        target.ActivateBuff(target.pbProtection, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(18));
                        break;
                    case "AbsorbWater":
                        target.CurrentAbsorbWater = effectTime;
                        target.ActivateBuff(target.pbAbsorbWater, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(19));
                        break;
                    case "SaintPower":
                        target.CurrentSaintPower = effectTime;
                        target.ActivateBuff(target.pbSaintPower, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(20));
                        break;
                    case "ShadowPact":
                        target.CurrentShadowPact = effectTime;
                        target.ActivateBuff(target.pbShadowPact, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(21));
                        break;
                    case "BloodyVengeance":
                        effectValue = player.StandardIntelligence / 2;
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

                    case "HeatBoost":
                        effectValue = player.StandardIntelligence / 2;
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

                    case "FlameAura":
                        target.CurrentFlameAura = effectTime;
                        target.ActivateBuff(target.pbFlameAura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(36));
                        break;

                    case "PromisedKnowledge":
                        effectValue = player.StandardIntelligence / 2;
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

                    case "RiseOfImage":
                        effectValue = player.StandardIntelligence / 2;
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

                    case "WordOfLife":
                        target.CurrentWordOfLife = effectTime;
                        target.ActivateBuff(target.pbWordOfLife, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(41));
                        break;

                    case "WordOfFortune":
                        target.CurrentWordOfFortune = effectTime;
                        target.ActivateBuff(target.pbWordOfFortune, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(42));
                        break;

                    case "EternalPresence":
                        target.CurrentEternalPresence = effectTime;
                        target.ActivateBuff(target.pbEternalPresence, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(44), 1000);
                        UpdateBattleText(player.GetCharacterSentence(45));
                        break;

                    case "MirrorImage":
                        target.CurrentMirrorImage = effectTime;
                        target.ActivateBuff(target.pbMirrorImage, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(57), player.Target.FirstName));
                        break;

                    case "Deflection":
                        target.CurrentDeflection = effectTime;
                        target.ActivateBuff(target.pbDeflection, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(60), target.FirstName));
                        break;

                    case "PsychicTrance":
                        target.CurrentPsychicTrance = effectTime;
                        target.ActivateBuff(target.pbPsychicTrance, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(129), target.FirstName));
                        break;
                    case "BlindJustice":
                        target.CurrentBlindJustice = effectTime;
                        target.ActivateBuff(target.pbBlindJustice, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(130), target.FirstName));
                        break;
                    case "TranscendentWish":
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
                    case "SkyShield":
                        target.CurrentSkyShield = effectTime;
                        target.CurrentSkyShieldValue++;
                        target.ChangeSkyShieldStatus(target.CurrentSkyShieldValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(134), target.FirstName));
                        break;
                    case "StaticBarrier":
                        target.CurrentStaticBarrier = effectTime;
                        target.CurrentStaticBarrierValue++;
                        target.ChangeStaticBarrierStatus(target.CurrentStaticBarrierValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(186), target.FirstName));
                        break;
                    case "EverDroplet":
                        target.CurrentEverDroplet = effectTime;
                        target.ActivateBuff(target.pbEverDroplet, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(136), target.FirstName));
                        break;
                    case "FrozenAura":
                        target.CurrentFrozenAura = effectTime;
                        target.ActivateBuff(target.pbFrozenAura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(137), target.FirstName));
                        break;

                    //case "Damnation":
                    //    player.Target.CurrentDamnation = effectTime;
                    //    player.Target.pbDamnation.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                    //    player.Target.pbDamnation.Update();
                    //    UpdateBattleText(player.GetCharacterSentence(37));
                    //    break;

                    case "PainfulInsanity":
                        player.CurrentPainfulInsanity = effectTime;
                        player.ActivateBuff(player.pbPainfulInsanity, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(4));
                        break;

                    case "FlashBlaze_Buff":
                        target.CurrentFlashBlazeCount = effectTime;
                        target.ActivateBuff(target.pbFlashBlaze, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "StarLightning":
                        if (target.CurrentStarLightning <= 0) // スタン効果なので、累積させない。
                        {
                            target.CurrentStarLightning = effectTime;
                        }
                        target.ActivateBuff(target.pbStarLightning, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(target.FirstName + "は気絶した。\r\n");
                        break;
                    case "WordOfMalice":
                        target.CurrentWordOfMalice = effectTime;
                        target.ActivateBuff(target.pbWordOfMalice, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SinFortune":
                        target.CurrentSinFortune = effectTime;
                        target.ActivateBuff(target.pbSinFortune, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(211), target.FirstName));
                        break;
                    case "BlackFire":
                        target.CurrentBlackFire = effectTime;
                        target.ActivateBuff(target.pbBlackFire, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "EnrageBlast":
                        target.CurrentEnrageBlast = effectTime;
                        target.ActivateBuff(target.pbEnrageBlast, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SigilOfHomura":
                        target.CurrentSigilOfHomura = effectTime;
                        target.ActivateBuff(target.pbSigilOfHomura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(206)));
                        break;
                    case "Immolate":
                        target.CurrentImmolate = effectTime;
                        target.ActivateBuff(target.pbImmolate, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "HolyBreaker":
                        target.CurrentHolyBreaker = effectTime;
                        target.ActivateBuff(target.pbHolyBreaker, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(148));
                        break;
                    case "HymnContract":
                        target.CurrentHymnContract = effectTime;
                        target.ActivateBuff(target.pbHymnContract, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "DarkenField":
                        target.CurrentDarkenField = effectTime;
                        target.ActivateBuff(target.pbDarkenField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "EclipseEnd":
                        target.CurrentEclipseEnd = effectTime;
                        target.ActivateBuff(target.pbEclipseEnd, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "BlazingField":
                        target.CurrentBlazingField = effectTime;
                        target.CurrentBlazingFieldFactor = player.TotalIntelligence;
                        target.ActivateBuff(target.pbBlazingField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ExaltedField":
                        target.CurrentExaltedField = effectTime;
                        target.ActivateBuff(target.pbExaltedField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ONEAuthority":
                        target.CurrentOneAuthority = effectTime;
                        target.ActivateBuff(target.pbOneAuthority, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "RisingAura":
                        target.CurrentRisingAura = effectTime;
                        target.ActivateBuff(target.pbRisingAura, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "AscensionAura":
                        target.CurrentAscensionAura = effectTime;
                        target.ActivateBuff(target.pbAscensionAura, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "SeventhMagic":
                        target.CurrentSeventhMagic = effectTime;
                        target.ActivateBuff(target.pbSeventhMagic, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(150));
                        break;

                    case "PhantasmalWind":
                        target.CurrentPhantasmalWind = effectTime;
                        target.ActivateBuff(target.pbPhantasmalWind, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(183));
                        break;
                    case "ParadoxImage":
                        target.CurrentParadoxImage = effectTime;
                        target.ActivateBuff(target.pbParadoxImage, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(184));
                        break;

                    case "AusterityMatrix":
                        target.CurrentAusterityMatrix = effectTime;
                        target.ActivateBuff(target.pbAusterityMatrix, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(207));
                        break;

                    case "RedDragonWill":
                        target.CurrentRedDragonWill = effectTime;
                        target.ActivateBuff(target.pbRedDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(208));
                        break;

                    case "BlueDragonWill":
                        target.CurrentBlueDragonWill = effectTime;
                        target.ActivateBuff(target.pbBlueDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(209));
                        break;

                    case "NourishSense":
                        target.CurrentNourishSense = effectTime;
                        target.ActivateBuff(target.pbNourishSense, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfDouble":
                        target.CurrentStanceOfDouble = effectTime;
                        target.ActivateBuff(target.pbStanceOfDouble, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SwiftStep":
                        target.CurrentSwiftStep = effectTime;
                        target.ActivateBuff(target.pbSwiftStep, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "VigorSense":
                        target.CurrentVigorSense = effectTime;
                        target.ActivateBuff(target.pbVigorSense, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SmoothingMove":
                        target.CurrentSmoothingMove = effectTime;
                        target.ActivateBuff(target.pbSmoothingMove, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "FutureVision":
                        target.CurrentFutureVision = effectTime;
                        target.ActivateBuff(target.pbFutureVision, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ReflexSpirit":
                        target.CurrentReflexSpirit = effectTime;
                        target.ActivateBuff(target.pbReflexSpirit, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "TrustSilence":
                        target.CurrentTrustSilence = effectTime;
                        target.ActivateBuff(target.pbTrustSilence, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "ConcussiveHit":
                        target.CurrentConcussiveHit = effectTime;
                        target.CurrentConcussiveHitValue++;
                        target.ChangeConcussiveHitStatus(target.CurrentConcussiveHitValue);
                        break;
                    case "OnslaughtHit":
                        target.CurrentOnslaughtHit = effectTime;
                        target.CurrentOnslaughtHitValue++;
                        target.ChangeOnslaughtHitStatus(target.CurrentOnslaughtHitValue);
                        break;
                    case "ImpulseHit":
                        target.CurrentImpulseHit = effectTime;
                        target.CurrentImpulseHitValue++;
                        target.ChangeImpulseHitStatus(target.CurrentImpulseHitValue);
                        break;
                    case "JUNKAN-SEIYAKU":
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
                    case "StanceOfMystic":
                        target.CurrentStanceOfMystic = effectTime;
                        target.CurrentStanceOfMysticValue++;
                        target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMysticValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(168), target.FirstName));
                        break;
                    case "TruthVision":
                        player.CurrentTruthVision = effectTime;
                        target.ActivateBuff(target.pbTruthVision, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(63));
                        break;

                    case "HighEmotionality":
                        if (player.CurrentHighEmotionality <= 0)
                        {
                            player.BuffStrength_HighEmotionality = player.Strength / 3;
                            player.BuffAgility_HighEmotionality = player.Agility / 3;
                            player.BuffIntelligence_HighEmotionality = player.Intelligence / 3;
                            //player.BuffStamina_HighEmotionality = player.Stamina / 3;
                            player.BuffMind_HighEmotionality = player.Mind / 3;

                            player.CurrentHighEmotionality = effectTime;
                            target.ActivateBuff(target.pbHighEmotionality, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(85), 1000);
                            UpdateBattleText(player.GetCharacterSentence(86));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "Negate":
                        player.CurrentNegate = effectTime;
                        player.ActivateBuff(player.pbNegate, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfEyes":
                        player.CurrentStanceOfEyes = effectTime;
                        player.ActivateBuff(player.pbStanceOfEyes, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "CounterAttack":
                        player.CurrentCounterAttack = effectTime;
                        player.ActivateBuff(player.pbCounterAttack, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfStanding":
                        player.CurrentStanceOfStanding = effectTime;
                        player.ActivateBuff(player.pbStanceOfStanding, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "AntiStun":
                        if (player.CurrentAntiStun <= 0)
                        {
                            player.CurrentAntiStun = effectTime;
                            target.ActivateBuff(target.pbAntiStun, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(93));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "StanceOfDeath":
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

                    case "NothingOfNothingness":
                        if (player.CurrentNothingOfNothingness <= 0)
                        {
                            player.CurrentNothingOfNothingness = effectTime;
                            player.ActivateBuff(player.pbNothingOfNothingness, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(106), 1000);
                            UpdateBattleText(player.GetCharacterSentence(107));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "SYUTYU-DANZETSU":
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

                    case "TimeStop":
                        if (player.CurrentTimeStop <= 0)
                        {
                            player.CurrentTimeStop = effectTime;
                            player.ActivateBuff(player.pbTimeStop, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(47));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "AfterReviveHalf":
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
                        player.ChangeFeltusStatus(player.CurrentFeltusValue);
                        UpdateBattleText(target.FirstName + "に、【神】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_JUZA_PHANTASMAL:
                        player.CurrentJuzaPhantasmal = effectTime;
                        player.CurrentJuzaPhantasmalValue++;
                        player.ChangeJuzaPhantasmalStatus(player.CurrentJuzaPhantasmalValue);
                        UpdateBattleText(target.FirstName + "に、【颯】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_ETERNAL_FATE:
                        player.CurrentEternalFateRing = effectTime;
                        player.CurrentEternalFateRingValue++;
                        player.ChangeEternalFateRingStatus(player.CurrentEternalFateRingValue);
                        UpdateBattleText(target.FirstName + "に、【轟】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_LIGHT_SERVANT:
                        player.CurrentLightServant = effectTime;
                        player.CurrentLightServantValue++;
                        player.ChangeLightServantStatus(player.CurrentLightServantValue);
                        UpdateBattleText(player.FirstName + "に、【聖】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_SHADOW_SERVANT:
                        player.CurrentShadowServant = effectTime;
                        player.CurrentShadowServantValue++;
                        player.ChangeShadowServantStatus(player.CurrentShadowServantValue);
                        UpdateBattleText(player.FirstName + "に、【闇】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN:
                        player.CurrentAdilBlueBurn = effectTime;
                        player.CurrentAdilBlueBurnValue++;
                        player.ChangeAdilBlueBurnStatus(player.CurrentAdilBlueBurnValue);
                        UpdateBattleText(target.FirstName + "に、【蒼】の蓄積カウンターが乗った！\r\n");
                        break;

                    case Database.ITEMCOMMAND_MAZE_CUBE:
                        player.CurrentMazeCube = effectTime;
                        player.CurrentMazeCubeValue++;
                        player.ChangeMazeCubeStatus(player.CurrentMazeCubeValue);
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
                        player.ChangeLifeCountStatus(player.CurrentLifeCountValue);
                        UpdateBattleText(player.FirstName + "に生命力カウンターが３つ発生した！\r\n");
                        break;

                    case Database.CHAOTIC_SCHEMA:
                        player.CurrentChaoticSchema = effectTime;
                        player.ActivateBuff(player.pbChaoticSchema, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        break;

                    default:
                        break;
                }
            // [todo]
            //}
            //else
            //{
            //    UpdateBattleText(player.GetCharacterSentence(53));
            //}
        }

        /// <summary>
        /// ファイア・ボールのメソッド
        /// </summary>
        private void PlayerSpellFireBall(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FireBallValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, Database.FIRE_BALL, interval, ref damage, magnification, "FireBall.mp3", 10, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// シャドウ・パクトのメソッド
        /// </summary>
        private void PlayerSpellShadowPact(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("ShadowPact.mp3");
            PlayerBuffAbstract(player, target, 999, "ShadowPact");
        }

        /// <summary>
        /// プロテクションのメソッド
        /// </summary>
        private void PlayerSpellProtection(MainCharacter player, MainCharacter target)
        {
            Debug.Log("playerspellprotection start");
            GroundOne.PlaySoundEffect("Protection.mp3");
            PlayerBuffAbstract(player, target, 999, "Protection");
            Debug.Log("playerspellprotection end");
        }

        /// <summary>
        /// ヒート・ブースト
        /// </summary>
        private void PlayerSpellHeatBoost(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("HeatBoost.mp3");
            PlayerBuffAbstract(player, target, 999, "HeatBoost");
        }
    }
}
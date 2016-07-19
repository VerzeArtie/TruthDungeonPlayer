using UnityEngine;
using UnityEngine.UI;
using System;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        private int FAIL_ACTION_WAIT = 150;
        private int failActionTimer = 0;
        private void CheckStackInTheCommand()
        {
            if (this.NowStackInTheCommand)
            {
                #region "カウンター失敗時のアニメーション"
                if (failActionTimer > 0)
                {
                    this.failActionTimer--;
                    return;
                }
                else
                {
                    back_nowStackAnimationName.transform.localScale = new Vector2(0.0f, 1.0f);
                    back_nowStackAnimationBar.transform.localScale = new Vector2(0.0f, 1.0f);
                }
                #endregion

                // UnityではUpdateから連続的にコールされてアニメーションを行うため、ここではUnityロジックでスタックインザコマンドを実装した。
                for (int ii = 0; ii < this.ActiveList.Count; ii++)
                {
                    if (this.ActiveList[ii].StackActivation)
                    {
                        Debug.Log("StackActivation start: " + this.ActiveList[ii].StackCommandString);
                        this.ActiveList[ii].StackActivation = false;

                        // インスタント対象の場合、ここでターゲットを記載する（メインメソッドのターゲット指定では指定できない）
                        if (this.ActiveList[ii].StackCommandString == Database.DEEP_MIRROR)
                        {
                            this.ActiveList[ii].StackTarget = this.stackActivePlayer[this.StackNumber];
                        }

                        // スタック行動を追加登録する。
                        this.stackActivePlayer.Add(this.ActiveList[ii]);
                        string actionCommand = this.ActiveList[ii].StackCommandString;
                        this.StackNumber++;
                        this.cumulativeCounter.Add(0);
                        StackInTheCommandNameText[this.StackNumber].text = actionCommand;
                        StackInTheCommandNameText[this.StackNumber].text += "    " + this.ActiveList[ii].FirstName + " --> ";
                        if (this.ActiveList[ii].StackTarget != null)
                        {
                            StackInTheCommandNameText[this.StackNumber].text += "  " + this.ActiveList[ii].StackTarget.FirstName;
                        }
                        else
                        {
                            StackInTheCommandNameText[this.StackNumber].text += "  " + "全体"; // 「警告」絡みつくフランシスのファイアビューネが発端となっている。全体考察してください。
                        }

                        #region "カラーの設定"
                        if (actionCommand == Database.ARCHETYPE_EIN ||
                            actionCommand == Database.RECOVER)
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = Color.black;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = Color.black;
                            StackInTheCommandNameText[this.StackNumber].color = Color.white;
                            StackInTheCommandBarText[this.StackNumber].color = Color.white;
                        }
                        else if (this.ActiveList[ii] == GroundOne.MC || this.ActiveList[ii] == GroundOne.SC || this.ActiveList[ii] == GroundOne.TC)
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = this.ActiveList[ii].PlayerBattleColor;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = this.ActiveList[ii].PlayerBattleColor;
                            StackInTheCommandNameText[this.StackNumber].color = Color.black;
                            StackInTheCommandBarText[this.StackNumber].color = Color.black;
                        }
                        else
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = Color.red;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = Color.red;
                            StackInTheCommandNameText[this.StackNumber].color = Color.white;
                            StackInTheCommandBarText[this.StackNumber].color = Color.white;
                        }

                        StackInTheCommandBarText[this.StackNumber].text = Database.TIMEUP_FIRST_RESPONSE.ToString();
                        if (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == MainCharacter.PlayerAction.Archetype)
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = Color.black;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = Color.black;
                            StackInTheCommandNameText[this.StackNumber].color = Color.white;
                            StackInTheCommandBarText[this.StackNumber].color = Color.white;
                        }
                        else if (this.ActiveList[ii] == GroundOne.MC || this.ActiveList[ii] == GroundOne.SC || this.ActiveList[ii] == GroundOne.TC)
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = this.ActiveList[ii].PlayerBattleColor;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = this.ActiveList[ii].PlayerBattleColor;
                            StackInTheCommandNameText[this.StackNumber].color = Color.black;
                            StackInTheCommandBarText[this.StackNumber].color = Color.black;
                        }
                        else
                        {
                            back_StackInTheCommandName[this.StackNumber].GetComponent<Image>().color = Color.red;
                            back_StackInTheCommandBar[this.StackNumber].GetComponent<Image>().color = Color.red;
                            StackInTheCommandNameText[this.StackNumber].color = Color.white;
                            StackInTheCommandBarText[this.StackNumber].color = Color.white;
                        }
                        #endregion

                        this.back_StackInTheCommandName[this.StackNumber].transform.localScale = new Vector2(1.0f, 1.0f);
                        this.back_StackInTheCommandBar[this.StackNumber].transform.localScale = new Vector2(1.0f, 1.0f);


                        #region "スタックが即座に発動するコマンド"
                        // スタンス・オブ・サッドネス
                        if (this.ActiveList[ii].StackCommandString == Database.STANCE_OF_SUDDENNESS)
                        {
                            ActiveList[ii].CurrentStanceOfSuddenness = false;
                            ExecStackIn();
                            nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber - 1].FirstName + "の" + this.stackActivePlayer[this.StackNumber - 1].StackCommandString;
                            nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "のStanceOfSuddenness)";
                            ExecStackOut(true);
                            StackInTheCommandEnd();
                            ExecStackOut(true);
                            return;
                        }
                        // リカバー
                        if (this.ActiveList[ii].StackCommandString == Database.RECOVER)
                        {
                            ExecStackIn();
                            nowStackAnimationNameText.text = this.ActiveList[ii].FirstName + "の" + this.ActiveList[ii].StackCommandString;
                            nowStackAnimationBarText.text = "【リカバー】発動！　スタン／麻痺／凍結を解除　";
                            PlayerSkillRecover(this.ActiveList[ii], this.ActiveList[ii]);
                            ExecStackOut(true);
                            return;
                        }
                        // 防御姿勢
                        if (this.ActiveList[ii].StackCommandString == Database.DEFENSE_EN)
                        {
                            ExecStackIn();
                            nowStackAnimationNameText.text = this.ActiveList[ii].FullName;
                            nowStackAnimationBarText.text = "【防御】の姿勢";
                            ActiveList[ii].PA = MainCharacter.PlayerAction.Defense;
                            ExecStackOut(true);
                            return;
                        }
                        // 元核
                        if (TruthActionCommand.CheckPlayerActionFromString(this.ActiveList[ii].StackCommandString) == MainCharacter.PlayerAction.Archetype)
                        {
                            ExecStackIn();
                            if (this.ActiveList[ii].StackCommandString == Database.ARCHETYPE_EIN)
                            {
                                nowStackAnimationNameText.text = this.ActiveList[ii].FullName;
                                nowStackAnimationBarText.text = "【元核】　【集中と断絶】　【発動！】";
                                PlayerArchetypeSyutyuDanzetsu(this.ActiveList[ii], this.ActiveList[ii]);
                            }
                            else if (this.ActiveList[ii].StackCommandString == Database.ARCHETYPE_RANA)
                            {
                                nowStackAnimationNameText.text = this.ActiveList[ii].FullName;
                                nowStackAnimationBarText.text = "【元核】　【循環の誓約】　【発動！】";
                                PlayerArchetypeJunkanSeiyaku(this.ActiveList[ii]);
                            }
                            else if (this.ActiveList[ii].StackCommandString == Database.ARCHETYPE_OL)
                            {
                                nowStackAnimationNameText.text = this.ActiveList[ii].FullName;
                                nowStackAnimationBarText.text = "【元核】　【オラオラオラァ！】　【発動！】";
                                PlayerArchetypeOraOraOraaa(this.ActiveList[ii]);
                            }
                            else if (this.ActiveList[ii].StackCommandString == Database.ARCHETYPE_VERZE)
                            {
                                nowStackAnimationNameText.text = this.ActiveList[ii].FullName;
                                nowStackAnimationBarText.text = "【元核】　【真実の破壊】　【発動！】";
                                PlayerArchetypeSyutyuDanzetsu(this.ActiveList[ii], this.ActiveList[ii]);
                            }
                            ExecStackOut(true);
                            return;
                        }
                        #endregion
                    }
                }

                if (this.cumulativeCounter[this.StackNumber] < Database.TIMEUP_FIRST_RESPONSE)
                {
                    MainCharacter activePlayer = this.stackActivePlayer[this.StackNumber];
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        string actionCommand = this.stackActivePlayer[this.StackNumber].StackCommandString;
                        MainCharacter.PlayerAction pa = this.stackActivePlayer[this.StackNumber].StackPlayerAction;

                        if (this.NowTimeStop && ActiveList[ii].CurrentTimeStop <= 0)
                        {
                            // 時間は飛ばされる
                            continue;
                        }

                        #region "対戦相手が特定のカウンター系をいれこんでいる場合、打ち消しが実施される。"
                        if (DetectOpponentParty(activePlayer, ActiveList[ii]))
                        {
                            if (ActiveList[ii].CurrentNegate > 0)
                            {
                                ActiveList[ii].RemoveNegate(); // カウンター成功／失敗に限らず、一度チェックが入ったら解消されるものとする（１ターンで何度も発動するのは強すぎたため）
                                string factor = String.Empty;
                                // 魔法系統の場合のみ発動する。
                                if (TruthActionCommand.GetAttribute(actionCommand) == TruthActionCommand.Attribute.Spell)
                                {
                                    if (JudgeSuccessOfCounter(this.stackActivePlayer[this.StackNumber], ActiveList[ii], 104, ref factor))
                                    {
                                        back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
                                        back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
                                        back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
                                        nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
                                        nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "の" + Database.NEGATE + ")";
                                        ExecStackOut(true);
                                        return;
                                    }
                                    else
                                    {
                                        CounterFail(actionCommand, factor);
                                    }
                                }
                            }

                            if (ActiveList[ii].CurrentCounterAttack > 0)
                            {
                                ActiveList[ii].RemoveCounterAttack();
                                string factor = String.Empty;
                                // スキル系統かつ、ダメージ系統の場合のみ発動する。
                                if (TruthActionCommand.GetAttribute(actionCommand) == TruthActionCommand.Attribute.NormalAttack ||
                                    TruthActionCommand.GetAttribute(actionCommand) == TruthActionCommand.Attribute.Skill)
                                {
                                    if (TruthActionCommand.IsDamage(actionCommand) == true)
                                    {
                                        if (JudgeSuccessOfCounter(this.stackActivePlayer[this.StackNumber], ActiveList[ii], 113, ref factor))
                                        {
                                            back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
                                            back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
                                            back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
                                            nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
                                            nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "の" + Database.COUNTER_ATTACK + ")";
                                            ExecStackOut(true);
                                        }
                                        else
                                        {
                                            CounterFail(actionCommand, factor);
                                        }
                                        return;
                                    }
                                }
                            }

                            if (ActiveList[ii].CurrentStanceOfEyes > 0)
                            {
                                ActiveList[ii].RemoveStanceOfEyes();
                                string factor = String.Empty;
                                // 任意の行動に対して発動する。
                                if (JudgeSuccessOfCounter(this.stackActivePlayer[this.StackNumber], ActiveList[ii], 101, ref factor))
                                {
                                    back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
                                    back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
                                    back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
                                    nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
                                    nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "の" + Database.STANCE_OF_EYES + ")";
                                    ExecStackOut(true);
                                }
                                else
                                {
                                    CounterFail(actionCommand, factor);
                                }
                                return;
                            }

                            if (ActiveList[ii].CurrentFutureVision > 0)
                            {
                                //ActiveList[ii].RemoveFutureVision(); // 外れない仕様で進めている。強すぎる場合は他と同様外れる仕様にする。
                                string factor = String.Empty;
                                // 任意の行動に対して発動する。
                                if (JudgeSuccessOfCounter(this.stackActivePlayer[this.StackNumber], ActiveList[ii], 219, ref factor))
                                {
                                    back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
                                    back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
                                    back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
                                    nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
                                    nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "の" + Database.FUTURE_VISION + ")";
                                    ExecStackOut(true);
                                }
                                else
                                {
                                    CounterFail(actionCommand, factor);
                                }
                                return;
                            }
                            if (ActiveList[ii].CurrentDeepMirror == true)
                            {
                                ActiveList[ii].CurrentDeepMirror = false;
                                string factor = String.Empty;
                                // 非ダメージ系統の場合のみ発動する。
                                if (TruthActionCommand.IsDamage(actionCommand) == false)
                                {
                                    if (JudgeSuccessOfCounter(this.stackActivePlayer[this.StackNumber], ActiveList[ii], 220, ref factor))
                                    {
                                        back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
                                        back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
                                        back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
                                        nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
                                        nowStackAnimationBarText.text = "失敗！(要因：" + ActiveList[ii].FirstName + "の" + Database.DEEP_MIRROR + ")";
                                        ExecStackOut(true);
                                    }
                                    else
                                    {
                                        CounterFail(actionCommand, factor);
                                    }
                                    return;
                                }
                            }
                        }
                        #endregion

                        #region "各プレイヤーのスタック割り込み"
                        if (this.cumulativeCounter[this.StackNumber] > Database.TIMEUP_COMPUTER_INTERRUPT && CanAction(ActiveList[ii]))
                        {
                            #region "シニキア・ヴェイルハンツ"
                            if (ActiveList[ii].FirstName == Database.DUEL_SINIKIA_VEILHANTU)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.DOUBLE_SLASH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.STRAIGHT_SMASH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CRUSHING_BLOW)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.ENIGMA_SENSE)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.SILENT_RUSH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.OBORO_IMPACT)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.KINETIC_SMASH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CATASTROPHE)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CARNAGE_RUSH))
                                        )
                                    {
                                        UpdateBattleText(ec1.FirstName + "：『カウンターアタック』を発動だ。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.FIRE_BALL)) ||
                                                ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.FLAME_STRIKE)) ||
                                                ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.FRESH_HEAL)) ||
                                                ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.HOLY_SHOCK)) ||
                                                ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.WORD_OF_POWER)) ||
                                                ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.GALE_WIND))
                                        )
                                    {
                                        UpdateBattleText(ec1.FirstName + "：『ニゲイト』を発動だ。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.NEGATE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "オル・ランディス"
                            else if (ActiveList[ii].FirstName == Database.DUEL_OL_LANDIS)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：あめぇんだよ、ザコアイン！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    // 強すぎるせいかコメントアウトされている。
                                    //if (ActiveList[ii].CurrentSaintPower <= 0)
                                    //{
                                    //    UpdateBattleText(ec1.FirstName + "：パワーアップだ！！\r\n");
                                    //    ActiveList[ii].CurrentInstantPoint = 0;
                                    //    ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                    //    ActiveList[ii].StackTarget = ActiveList[ii];
                                    //    ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                    //    ActiveList[ii].StackCommandString = Database.SAINT_POWER;
                                    //    ActiveList[ii].StackActivation = true;
                                    //}
                                    //else if (ActiveList[ii].CurrentGlory <= 0)
                                    //{
                                    //    if ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.STRAIGHT_SMASH) ||
                                    //        (pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.DOUBLE_SLASH) ||
                                    //        (pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.ENIGMA_SENSE))
                                    //    {
                                    //        UpdateBattleText(ec1.FirstName + "：あめぇんだよ、ザコアイン！\r\n");
                                    //        ActiveList[ii].CurrentInstantPoint = 0;
                                    //        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                    //        ActiveList[ii].StackTarget = ActiveList[ii];
                                    //        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                    //        ActiveList[ii].StackCommandString = Database.GLORY;
                                    //        ActiveList[ii].StackActivation = true;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    UpdateBattleText(ec1.FirstName + "：食らえや！！\r\n");
                                    //    ActiveList[ii].CurrentInstantPoint = 0;
                                    //    ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                    //    ActiveList[ii].StackTarget = GroundOne.MC;
                                    //    ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                    //    ActiveList[ii].StackCommandString = Database.STRAIGHT_SMASH;
                                    //    ActiveList[ii].StackActivation = true;
                                    //}
                                }
                            }
                            #endregion
                            #region "スコーティ・ザルゲ"
                            else if (ActiveList[ii].FirstName == Database.DUEL_SCOTY_ZALGE)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if ((pa == MainCharacter.PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand)))
                                    {
                                        bool existItem = false;
                                        ItemBackPack[] tempItem = ActiveList[ii].GetBackPackInfo();
                                        foreach (ItemBackPack value in tempItem)
                                        {
                                            if (value != null)
                                            {
                                                if (value.Name == Database.COMMON_NORMAL_RED_POTION)
                                                {
                                                    existItem = true;
                                                }
                                            }
                                        }

                                        if ((ec1.CurrentLife <= ec1.MaxLife / 2) &&
                                            (existItem))
                                        {
                                            UpdateBattleText(ec1.FirstName + "：ヒャハハハ、待ってましたぁ！ライフ回復ってかぁ！？\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseItem;
                                            ActiveList[ii].StackCommandString = Database.COMMON_NORMAL_RED_POTION;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                        else
                                        {
                                            UpdateBattleText(ec1.FirstName + "：『カウンターアタック』ってかぁ！？ッヒャハハハ！\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                            ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                    else
                                    {
                                        if (GroundOne.MC.CurrentDarkenField <= 0)
                                        {
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.WORD_OF_MALICE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                        else
                                        {
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.IMMOLATE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "キルト・ジョルジュ"
                            else if (ActiveList[ii].FirstName == Database.DUEL_KILT_JORJU)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御させてもらう！\r\n", ii);
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ"
                            else if ((ActiveList[ii].FirstName == Database.VERZE_ARTIE) && IsPlayerEnemy(ActiveList[ii]))
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (ActiveList[ii].CurrentCounterAttack <= 0))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：すみませんが、カウンターさせてもらいます。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)) && ((ActiveList[ii].CurrentMirrorImage <= 0)))
                                    {
                                        if (actionCommand == Database.WORD_OF_POWER)
                                        {
                                        }
                                        else
                                        {
                                            UpdateBattleText(ec1.FirstName + "：それを喰らうわけには行きませんね、ミラーイメージです。\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.MIRROR_IMAGE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "アンナ・ハミルトン"
                            else if (ActiveList[ii].FirstName == Database.DUEL_ANNA_HAMILTON)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：いやあああぁぁぁ！！　インスタント攻撃なんてしないでよ！！！\r\n", ii);
                                }
                                else if (TruthActionCommand.IsDamage(actionCommand))
                                {
                                    if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                    {
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.ONE_IMMUNITY;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "サン・ユウ"
                            if (ActiveList[ii].FirstName == Database.DUEL_SUN_YU)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand))))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：ッフン、喰らってたまるか、そんなもの！！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "シニキア・カールハンツ"
                            else if (ActiveList[ii].FirstName == Database.DUEL_SINIKIA_KAHLHANZ)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (ActiveList[ii].CurrentCounterAttack <= 0))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：ッフ、カウンターアタック。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)) && ((ActiveList[ii].CurrentMirrorImage <= 0)))
                                    {
                                        if (actionCommand == Database.WORD_OF_POWER)
                                        {
                                        }
                                        else
                                        {
                                            UpdateBattleText(ec1.FirstName + "：ッフ、ミラーイメージ。\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.MIRROR_IMAGE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ルベル・ゼルキス"
                            else if (ActiveList[ii].FirstName == Database.DUEL_RVEL_ZELKIS)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if (((pa == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (ActiveList[ii].CurrentCounterAttack <= 0))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：食らってなるものか、カウンターをさせてもらおう。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)))
                                    {
                                        if (actionCommand == Database.WORD_OF_POWER)
                                        {
                                        }
                                        else
                                        {
                                            UpdateBattleText(ec1.FirstName + "：食らってなるものか、ニゲイトをさせてもらおう。\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                            ActiveList[ii].StackCommandString = Database.NEGATE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ヴァン・ヘーグステル"
                            else if (ActiveList[ii].FirstName == Database.DUEL_VAN_HEHGUSTEL)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御だな。\r\n", ii);
                                }
                            }
                            #endregion
                            #region "オウリュウ・ゲンマ"
                            else if (ActiveList[ii].FirstName == Database.DUEL_OHRYU_GENMA)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御じゃのお。\r\n", ii);
                                }
                            }
                            #endregion
                            #region "ラダ・ミストゥルス"
                            else if (ActiveList[ii].FirstName == Database.DUEL_LADA_MYSTORUS)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：フン、ここは防御だな。\r\n", ii);
                                }
                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if (TruthActionCommand.IsDamage(actionCommand) == false)
                                    {
                                        UpdateBattleText(ec1.FirstName + "：では、こちらも動き出させてもらう。\r\n");
                                        if (ActiveList[ii].CurrentPromisedKnowledge <= 0)
                                        {
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.PROMISED_KNOWLEDGE;
                                        }
                                        else if (ActiveList[ii].CurrentWordOfLife <= 0)
                                        {
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = ActiveList[ii];
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.WORD_OF_LIFE;
                                        }
                                        else
                                        {
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.FLASH_BLAZE;
                                        }
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "シン・オスキュレーテ"
                            else if (ActiveList[ii].FirstName == Database.DUEL_SIN_OSCURETE)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御ですかね。\r\n", ii);
                                }

                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if (TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        if (pa == MainCharacter.PlayerAction.UseSpell && GroundOne.MC.CurrentSilence <= 0)
                                        {
                                            UpdateBattleText(ec1.FirstName + "：沈黙してもらいます。\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                            ActiveList[ii].StackCommandString = Database.VANISH_WAVE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && TruthActionCommand.IsHeal(actionCommand)))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：カウンターしておきましょう。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.NEGATE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else
                                    {
                                        UpdateBattleText(ec1.FirstName + "：レスポンスします。\r\n");
                                        if (((TruthEnemyCharacter)ActiveList[ii]).AI_TacticsNumber == 0)
                                        {
                                            if (ActiveList[ii].CurrentPromisedKnowledge <= 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = ActiveList[ii];
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.PROMISED_KNOWLEDGE;
                                            }
                                            else if (ActiveList[ii].CurrentRiseOfImage <= 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = ActiveList[ii];
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.RISE_OF_IMAGE;
                                            }
                                            else if (ActiveList[ii].CurrentBloodyVengeance <= 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = ActiveList[ii];
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.BLOODY_VENGEANCE;
                                            }
                                            else if (ActiveList[ii].CurrentHeatBoost <= 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = ActiveList[ii];
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.HEAT_BOOST;
                                            }
                                            else if (GroundOne.MC.CurrentMana > 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.DOOM_BLADE;
                                            }
                                            else
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.WORD_OF_POWER;
                                            }
                                        }
                                        else
                                        {
                                            if (GroundOne.MC.CurrentWordOfMalice <= 0) // 戦闘反応
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.WORD_OF_MALICE;
                                            }
                                            else if (GroundOne.MC.CurrentImmolate <= 0) // 物理防御
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.IMMOLATE;
                                            }
                                            else if (GroundOne.MC.CurrentBlackFire <= 0) // 魔法防御
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.BLACK_FIRE;
                                            }
                                            else if (GroundOne.MC.CurrentMana > 0)
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.DOOM_BLADE;
                                            }
                                            else
                                            {
                                                ActiveList[ii].CurrentInstantPoint = 0;
                                                ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                                ActiveList[ii].StackTarget = GroundOne.MC;
                                                ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                                ActiveList[ii].StackCommandString = Database.WORD_OF_POWER;
                                            }
                                        }
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "ラナ・アミリア（DUEL)"
                            else if (ActiveList[ii].FirstName == Database.ENEMY_LAST_RANA_AMILIA)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御の体制を取るわ。\r\n", ii);
                                }

                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if (actionCommand == Database.CHILL_BURN)
                                    {
                                        if (ActiveList[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                        {
                                            UpdateBattleText(ec1.FirstName + "：止められるわけにはいかないわ、Negate！\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                            ActiveList[ii].StackCommandString = Database.NEGATE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                    else if ((ActiveList[ii].CurrentOneImmunity <= 0) && (ActiveList[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                    {
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.ONE_IMMUNITY;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (pa == MainCharacter.PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：それもカウンターさせてもらうわ、DeepMirrorよ！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.DEEP_MIRROR;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (pa == MainCharacter.PlayerAction.UseSpell)
                                    {
                                        UpdateBattleText(ec1.FirstName + "：沈黙させるわ、VanishWave！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.VANISH_WAVE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "シニキア・カールハンツ(DUEL2)"
                            else if (ActiveList[ii].FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御させてもらおう。\r\n", ii);
                                }

                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if ((pa == MainCharacter.PlayerAction.UseSpell) && (ActiveList[ii].CurrentSkillPoint >= Database.NEGATE_COST) && (actionCommand != Database.WORD_OF_POWER))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：それは止めさせてもらおう、Negateだ。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.NEGATE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if ((pa == MainCharacter.PlayerAction.UseSpell) && (ActiveList[ii].CurrentMana >= Database.VANISH_WAVE_COST))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：沈黙させてもらうぞ、VanishWaveだ。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.VANISH_WAVE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if ((ActiveList[ii].CurrentOneImmunity <= 0) && (ActiveList[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                    {
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.ONE_IMMUNITY;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (pa == MainCharacter.PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：ッフ、DeepMirrorだ。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.DEEP_MIRROR;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "オル・ランディス（DUEL2)"
                            else if (ActiveList[ii].FirstName == Database.ENEMY_LAST_OL_LANDIS)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：ッケ、防御だ。\r\n", ii);
                                }

                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if ((pa == MainCharacter.PlayerAction.UseSpell) && (ActiveList[ii].CurrentSkillPoint >= Database.HARDEST_PARRY_COST) && (actionCommand == Database.WORD_OF_POWER))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：ケッ、パリィ発動！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.HARDEST_PARRY;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (TruthActionCommand.IsHeal(actionCommand) && (ActiveList[ii].CurrentMana >= Database.DEMONIC_IGNITE_COST))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：させっかよ、ディーモ！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.DEMONIC_IGNITE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (ActiveList[ii].CurrentBlackContract > 0)
                                    {
                                        // スタンス・オブ・サッドネスは即時発動なので、スタック乗せがどうなるか
                                    }
                                    else if ((pa == MainCharacter.PlayerAction.UseSpell) && (ActiveList[ii].CurrentSkillPoint >= Database.NEGATE_COST))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：あめぇ、ニゲイト！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.NEGATE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if ((pa == MainCharacter.PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand)) && (ActiveList[ii].CurrentSkillPoint >= Database.COUNTER_ATTACK_COST))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：あめぇ、カウンター！\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ（最終戦）
                            else if (ActiveList[ii].FirstName == Database.ENEMY_LAST_VERZE_ARTIE)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御させてもらいましょう。\r\n", ii);
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ（最終戦２）
                            else if (ActiveList[ii].FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(ActiveList, ec1.FirstName + "：防御させてもらいましょう。\r\n", ii);
                                }

                                if ((ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint))
                                {
                                    if (pa == MainCharacter.PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：甘いですね、DeepMirror。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.DEEP_MIRROR;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (actionCommand == Database.CHILL_BURN)
                                    {
                                        if (ActiveList[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                        {
                                            UpdateBattleText(ec1.FirstName + "：ックク、Negateです。\r\n");
                                            ActiveList[ii].CurrentInstantPoint = 0;
                                            ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                            ActiveList[ii].StackTarget = GroundOne.MC;
                                            ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                            ActiveList[ii].StackCommandString = Database.NEGATE;
                                            ActiveList[ii].StackActivation = true;
                                        }
                                    }
                                    else if (actionCommand == Database.PIERCING_FLAME && !ec1.DetectCannotBeSilence)
                                    {
                                        UpdateBattleText(ec1.FirstName + "：少し黙っていてもらいましょう、VanishWave。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.VANISH_WAVE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (actionCommand == Database.PIERCING_FLAME && ActiveList[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                    {
                                        UpdateBattleText(ec1.FirstName + "：ックク、Negateです。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.NEGATE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if ((ActiveList[ii].CurrentOneImmunity <= 0) && (ActiveList[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                    {
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = ActiveList[ii];
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.ONE_IMMUNITY;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (JudgeSelectDefense(activePlayer, actionCommand))
                                    {
                                        ExecDefense(ActiveList, ec1.FirstName + "：利きませんよ、それは。\r\n", ii);
                                    }
                                    else if (pa == MainCharacter.PlayerAction.UseSpell)
                                    {
                                        UpdateBattleText(ec1.FirstName + "：少し黙っていてもらいましょう、VanishWave。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.VANISH_WAVE;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                            #region "ダミー素振り君"
                            else if (ActiveList[ii].FirstName == Database.DUEL_DUMMY_SUBURI)
                            {
                                if (ActiveList[ii].CurrentInstantPoint >= ActiveList[ii].MaxInstantPoint)
                                {
                                    if ((pa == MainCharacter.PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：『カウンターアタック』。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if ((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.PROMISED_KNOWLEDGE))
                                    {
                                        UpdateBattleText(ec1.FirstName + "：『ディープ・ミラー』。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        ActiveList[ii].StackCommandString = Database.DEEP_MIRROR;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSpell) && (actionCommand == Database.WORD_OF_POWER)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.PSYCHIC_WAVE)))
                                    {
                                        //UpdateBattleText(ec1.FirstName + "：『ディープ・ミラー』。\r\n");
                                        //ActiveList[ii].CurrentInstantPoint = 0;
                                        //ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        //ActiveList[ii].StackTarget = GroundOne.MC;
                                        //ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSpell;
                                        //ActiveList[ii].StackCommandString = Database.DEEP_MIRROR;
                                        //ActiveList[ii].StackActivation = true;

                                        UpdateBattleText(ec1.FirstName + "：『スタンスオブ・アイズ』。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.STANCE_OF_EYES;
                                        ActiveList[ii].StackActivation = true;

                                        //UpdateBattleText(ec1.FirstName + "：『カウンターアタック』。\r\n");
                                        //ActiveList[ii].CurrentInstantPoint = 0;
                                        //ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        //ActiveList[ii].StackTarget = GroundOne.MC;
                                        //ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        //ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        //ActiveList[ii].StackActivation = true;

                                        //UpdateBattleText(ec1.FirstName + "：『ニゲイト』。\r\n");
                                        //ActiveList[ii].CurrentInstantPoint = 0;
                                        //ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        //ActiveList[ii].StackTarget = GroundOne.MC;
                                        //ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        //ActiveList[ii].StackCommandString = Database.NEGATE;
                                        //ActiveList[ii].StackActivation = true;
                                    }
                                    else if (((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.PSYCHIC_WAVE)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.STRAIGHT_SMASH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CRUSHING_BLOW)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.ENIGMA_SENSE)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.SILENT_RUSH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.OBORO_IMPACT)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.KINETIC_SMASH)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CATASTROPHE)) ||
                                        ((pa == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.CARNAGE_RUSH))
                                        )
                                    {
                                        UpdateBattleText(ec1.FirstName + "：『カウンターアタック』。\r\n");
                                        ActiveList[ii].CurrentInstantPoint = 0;
                                        ActiveList[ii].StackActivePlayer = ActiveList[ii];
                                        ActiveList[ii].StackTarget = GroundOne.MC;
                                        ActiveList[ii].StackPlayerAction = MainCharacter.PlayerAction.UseSkill;
                                        ActiveList[ii].StackCommandString = Database.COUNTER_ATTACK;
                                        ActiveList[ii].StackActivation = true;
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }

                    #region "タイムウェイト更新"
                    int cumulativeCounter = 0;
                    if (activePlayer.FirstName == Database.VERZE_ARTIE ||
                        activePlayer.FirstName == Database.ENEMY_LAST_VERZE_ARTIE ||
                        activePlayer.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                    {
                        if (activePlayer.StackCommandString == Database.NEUTRAL_SMASH)
                        {
                            cumulativeCounter = 9;
                        }
                        else
                        {
                            cumulativeCounter = 7;
                        }
                    }
                    else if (activePlayer.FirstName == Database.ENEMY_LAST_RANA_AMILIA ||
                                activePlayer.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                                activePlayer.FirstName == Database.ENEMY_LAST_OL_LANDIS
                        )
                    {
                        cumulativeCounter = 5;
                    }
                    else if (this.FinalBattle)
                    {
                        cumulativeCounter = 7;
                    }
                    else if (this.HiSpeedAnimation)
                    {
                        cumulativeCounter = 5;
                    }
                    else
                    {
                        cumulativeCounter = 3;
                    }
                    this.cumulativeCounter[this.StackNumber] += cumulativeCounter;
                    float dx = (float)(Database.TIMEUP_FIRST_RESPONSE - this.cumulativeCounter[this.StackNumber]) / (float)(Database.TIMEUP_FIRST_RESPONSE);
                    back_StackInTheCommandBar[this.StackNumber].transform.localScale = new Vector2(dx, 1.0f);
                    StackInTheCommandBarText[this.StackNumber].text = (Database.TIMEUP_FIRST_RESPONSE - this.cumulativeCounter[this.StackNumber]).ToString();
                    #endregion
                }
                else
                {
                    if (this.stackActivePlayer[this.StackNumber].Dead == false)
                    {
                        PlayerAttackPhase(this.stackActivePlayer[this.StackNumber], this.stackActivePlayer[this.StackNumber].StackTarget, this.stackActivePlayer[this.StackNumber].StackPlayerAction, this.stackActivePlayer[this.StackNumber].StackCommandString, false, false, false);
                    }
                    UpdatePlayerDeadFlag(); // 死亡判定・全滅判定更新

                    ExecStackOut(false);
                    StackInTheCommandEnd();
                }
            }
        }

        private void CounterFail(string actionCommand, string factor)
        {
            back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
            back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
            back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
            nowStackAnimationNameText.text = this.stackActivePlayer[this.StackNumber].FirstName + "の" + this.stackActivePlayer[this.StackNumber].StackCommandString;
            if (factor == String.Empty)
            {
                nowStackAnimationBarText.text = actionCommand + "はカウンターできない！";
            }
            else
            {
                nowStackAnimationBarText.text = factor + "によりカウンター無効化！";
            }
            this.failActionTimer = FAIL_ACTION_WAIT;
        }
    }
}
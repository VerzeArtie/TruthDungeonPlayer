using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using UnityEngine.UI;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        /// <summary>
        /// スタック・イン・ザ・コマンド。（再帰メソッド）
        /// </summary>
        /// <param name="player">プレイヤー全員</param>
        public void StackInTheCommand(List<MainCharacter> player, MainCharacter activePlayer, MainCharacter target, MainCharacter.PlayerAction pa, string actionCommand)
        //        public void StackInTheCommand(SortedList<int, MainCharacter> player, MainCharacter activePlayer, MainCharacter target, MainCharacter.PlayerAction pa, string actionCommand)
        {
            int TimeUp = 1200;
            int TimeUpFirstResponse = 600;
            int cumulativeCounter = 0;

            // 【警告】最終DUELフェーズのみ、進行速度を早くするが、本来は戦闘反応値に依存して早くするべきである。
            if (activePlayer.FirstName == Database.ENEMY_LAST_RANA_AMILIA ||
                activePlayer.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                activePlayer.FirstName == Database.ENEMY_LAST_OL_LANDIS
                )
            {
                TimeUp = 800; TimeUpFirstResponse = 200;
            }
            else if (activePlayer.FirstName == Database.ENEMY_LAST_VERZE_ARTIE ||
                activePlayer.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                TimeUp = 600; TimeUpFirstResponse = 0;
            }

            while (true)
            {
                System.Threading.Thread.Sleep(1);
                for (int ii = 0; ii < player.Count; ii++)
                {
                    // todo
                    #region "他のプレイヤーがコマンドスタックを載せてきた場合"
                    //if ((player[ii].StackActivation))
                    //{
                    //    player[ii].StackActivation = false;

                    //    #region "StanceOfSuddnessによるカウンター"
                    //    if (player[ii].StackCommandString == Database.STANCE_OF_SUDDENNESS)
                    //    {
                    //        player[ii].CurrentStanceOfSuddenness = false;
                    //        activePlayer.StackCommandString = String.Empty;
                    //        activePlayer.StackPlayerAction = MainCharacter.PlayerAction.None;
                    //        activePlayer.StackTarget = null;
                    //        activePlayer.StackActivePlayer = null;
                    //        activePlayer.StackActivation = false;
                    //        StackInThecommandNameText.ForeColor = System.Drawing.Color.White;
                    //        StackInThecommandNameText.BackColor = System.Drawing.Color.Black;
                    //        StackInThecommandNameText.Width = Database.TIMEUP_FIRST_RESPONSE;
                    //        StackInThecommandNameText.Text = "失敗！(要因：" + player[ii].FirstName + "のStanceOfSuddenness)";
                    //        StackInThecommandNameText.Update();
                    //        System.Threading.Thread.Sleep(1000);
                    //        StackNameLabel.Width = 0;
                    //        StackNameLabel.Update();
                    //        StackInThecommandNameText.Width = 0;
                    //        StackInThecommandNameText.Update();
                    //        return;
                    //    }
                    //    #endregion

                    //    // インスタント対象の場合、ここでターゲットを記載する（メインメソッドのターゲット指定では指定できない）
                    //    if (player[ii].StackCommandString == Database.DEEP_MIRROR)
                    //    {
                    //        player[ii].StackTarget = activePlayer;
                    //    }

                    //    StackInTheCommand(player, player[ii], player[ii].StackTarget, player[ii].StackPlayerAction, player[ii].StackCommandString);

                    //    int temp = 0;
                    //    if (cumulativeCounter > TimeUpFirstResponse)
                    //    {
                    //        temp = (cumulativeCounter - TimeUpFirstResponse) / (TimeUp / Database.TIMEUP_FIRST_RESPONSE);
                    //    }

                    //    // RumbleShoutにより、第ニパラメタをtargetからactivePlayer.StackTargetに変更。概念の見直しは必要と考えられる。
                    //    UpdateLabelInfo(activePlayer, activePlayer.StackTarget, Database.TIMEUP_FIRST_RESPONSE - temp, actionCommand);
                    //}
                    #endregion
                }

                #region "通常 or 即時元核奥義 or 即時リカバー"
                if ((cumulativeCounter >= TimeUp + TimeUpFirstResponse) ||
                    (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == MainCharacter.PlayerAction.Archetype) ||
                    ((TruthActionCommand.CheckPlayerActionFromString(actionCommand) == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.RECOVER)))
                {
                    if (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == MainCharacter.PlayerAction.Archetype)
                    {
                        if (actionCommand == Database.ARCHETYPE_EIN)
                        {
                            StackInTheCommandNameText[this.StackNumber].text = "【元核】　【集中と断絶】　【発動！】";
                        }
                        else if (actionCommand == Database.ARCHETYPE_RANA)
                        {
                            StackInTheCommandNameText[this.StackNumber].text = "【元核】　【循環の誓約】　【発動！】";
                        }
                        else if (actionCommand == Database.ARCHETYPE_OL)
                        {
                            StackInTheCommandNameText[this.StackNumber].text = "【元核】　【オラオラオラァ！】　【発動！】";
                        }
                        else if (actionCommand == Database.ARCHETYPE_VERZE)
                        {
                            StackInTheCommandNameText[this.StackNumber].text = "【元核】　【真実の破壊】　【発動！】";
                        }
                        // todo
                        //UpdateLabelInfo(activePlayer, target, Database.TIMEUP_FIRST_RESPONSE, actionCommand);
                        System.Threading.Thread.Sleep(2000);
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(actionCommand) == MainCharacter.PlayerAction.UseSkill) && (actionCommand == Database.RECOVER))
                    {
                        StackInTheCommandNameText[this.StackNumber].text = "【リカバー】発動！";
                        // todo
                        //UpdateLabelInfo(activePlayer, target, Database.TIMEUP_FIRST_RESPONSE, actionCommand);
                        System.Threading.Thread.Sleep(1000);
                    }

                    if (activePlayer.Dead == false)
                    {
                        PlayerAttackPhase(activePlayer, target, pa, actionCommand, false, false, false);
                    }
                    UpdatePlayerDeadFlag(); // 死亡判定・全滅判定更新
                    break;
                }
                #endregion

                #region "スタックコマンド待機時間累積更新"
                // 【警告】最終DUELフェーズのみ、進行速度を早くするが、本来は戦闘反応値に依存して早くするべきである。
                if (activePlayer.FirstName == Database.VERZE_ARTIE ||
                    activePlayer.FirstName == Database.ENEMY_LAST_VERZE_ARTIE ||
                    activePlayer.FirstName == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    if (actionCommand == Database.NEUTRAL_SMASH)
                    {
                        cumulativeCounter += 5;
                    }
                    else
                    {
                        cumulativeCounter += 2;
                    }
                }
                else if (activePlayer.FirstName == Database.ENEMY_LAST_RANA_AMILIA ||
                            activePlayer.FirstName == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                            activePlayer.FirstName == Database.ENEMY_LAST_OL_LANDIS
                    )
                {
                    cumulativeCounter += 2;
                }
                else if (this.FinalBattle)
                {
                    cumulativeCounter += 3;
                }
                else if (this.HiSpeedAnimation)
                {
                    cumulativeCounter += 2;
                }
                else
                {
                    cumulativeCounter++;
                }
                #endregion

                int temp2 = 0;
                if (cumulativeCounter > TimeUpFirstResponse)
                {
                    temp2 = (cumulativeCounter - TimeUpFirstResponse) / (TimeUp / Database.TIMEUP_FIRST_RESPONSE);
                }
                back_StackInTheCommandBar[this.StackNumber].transform.localScale = new Vector2(((Database.TIMEUP_FIRST_RESPONSE - temp2) / Database.TIMEUP_FIRST_RESPONSE), 1.0f);
                StackInTheCommandBarText[this.StackNumber].text = Convert.ToString(Database.TIMEUP_FIRST_RESPONSE - temp2);
            }
            activePlayer.StackCommandString = String.Empty;
            activePlayer.StackPlayerAction = MainCharacter.PlayerAction.None;
            activePlayer.StackTarget = null;
            activePlayer.StackActivePlayer = null;
            activePlayer.StackActivation = false;

            StackInTheCommandNameText[this.StackNumber].transform.localScale = new Vector2(0.0f, 1.0f);
            StackInTheCommandBarText[this.StackNumber].transform.localScale = new Vector2(0.0f, 1.0f);
        }

    }
}

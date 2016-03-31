using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        public bool JudgeSelectDefense(MainCharacter activePlayer, string actionCommand)
        {
            if (activePlayer != ec1 && ec1.PA != MainCharacter.PlayerAction.Defense && ec1.StackActivation == false && (TruthActionCommand.IsDamage(actionCommand)))
            {
                return true;
            }
            return false;
        }

        public bool CanAction(MainCharacter activePlayer)
        {
            if (activePlayer.CurrentStunning > 0 || activePlayer.CurrentFrozen > 0 || activePlayer.CurrentParalyze > 0)
            {
                return false;
            }
            return true;
        }

        public void ExecStackIn()
        {
            back_nowStackAnimationName.transform.localScale = new Vector2(1.0f, 1.0f);
            back_nowStackAnimationBar.transform.localScale = new Vector2(1.0f, 1.0f);
            back_nowStackAnimationBar.GetComponent<Image>().color = Color.black;
            nowStackAnimationBarText.color = Color.white;
            back_nowStackAnimationName.GetComponent<Image>().color = Color.black;
            nowStackAnimationNameText.color = Color.white;
        }

        public void ExecStackOut(bool nowStackAnimation)
        {
            back_StackInTheCommandBar[this.StackNumber].transform.localScale = new Vector2(0.0f, 1.0f);
            back_StackInTheCommandName[this.StackNumber].transform.localScale = new Vector2(0.0f, 1.0f);
            this.stackActivePlayer[this.StackNumber].StackCommandString = String.Empty;
            this.stackActivePlayer[this.StackNumber].StackPlayerAction = MainCharacter.PlayerAction.None;
            this.stackActivePlayer[this.StackNumber].StackTarget = null;
            this.stackActivePlayer[this.StackNumber].StackActivePlayer = null;
            this.stackActivePlayer[this.StackNumber].StackActivation = false;
            this.cumulativeCounter[this.StackNumber] = Database.TIMEUP_FIRST_RESPONSE;
            this.nowStackAnimation = nowStackAnimation;
        }

        public void ExecStanceOfSuddenness(List<MainCharacter> player, string message, int ii)
        {
            //player[ii].CurrentStanceOfSuddenness = false;
            //activePlayer.StackCommandString = String.Empty;
            //activePlayer.StackPlayerAction = PlayerAction.None;
            //activePlayer.StackTarget = null;
            //activePlayer.StackActivePlayer = null;
            //activePlayer.StackActivation = false;
            //StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
            //StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
            //StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
            //StackInTheCommandLabel.Text = "失敗！(要因：" + player[ii].Name + "のStanceOfSuddenness)";
            //StackInTheCommandLabel.Update();
            //System.Threading.Thread.Sleep(1000);
            //StackNameLabel.Width = 0;
            //StackNameLabel.Update();
            //StackInTheCommandLabel.Width = 0;
            //StackInTheCommandLabel.Update();
            //return;
        }

        public void ExecDefense(List<MainCharacter> player, string message, int ii)
        {
            // todo
        //    UpdateBattleText(message);
        //    player[ii].PA = MainCharacter.PlayerAction.Defense;
        //    player[ii].ActionLabel.text = Database.DEFENSE_JP;
        //    Color tempBack = StackInTheCommandLabel.BackColor;
        //    Color tempFore = StackInTheCommandLabel.ForeColor;
        //    StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
        //    StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
        //    StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
        //    StackInTheCommandLabel.Text = player[ii].FirstName + "防御の姿勢";
        //    StackInTheCommandLabel.Update();
        //    System.Threading.Thread.Sleep(1000);
        //    StackInTheCommandLabel.Width = 0;
        //    StackInTheCommandLabel.Update();
        //    StackInTheCommandLabel.ForeColor = tempFore;
        //    StackInTheCommandLabel.BackColor = tempBack;
        //    //player[ii].StackActivation = true; // 防御姿勢はスタックアクティベーションに入らず、即時適用となる。
        }
    }
}

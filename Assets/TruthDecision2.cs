using UnityEngine;
using System.Collections;
using DungeonPlayer;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class TruthDecision2 : MotherForm
    {
        public enum AnswerType
        {
            Top,
            Left,
            Right,
            Bottom
        }

        // GUI
        public GameObject grpNormal;
        public GameObject back_buttonTop;
        public GameObject back_buttonLeft;
        public GameObject back_buttonRight;
        public GameObject back_buttonBottom;
        public Text buttonTop;
        public Text buttonLeft;
        public Text buttonRight;
        public Text buttonBottom;
        public GameObject grpPermutation;
        public GameObject back_button1;
        public GameObject back_button2;
        public GameObject back_button3;
        public GameObject back_button4;
        public Text button1;
        public Text button2;
        public Text button3;
        public Text button4;
        public Text mainMessage;

        private bool Permutation1 = false;
        private bool Permutation2 = false;
        private bool Permutation3 = false;
        private bool Permutation4 = false;

        public override void Start()
        {
            base.Start();
            mainMessage.text = GroundOne.Decision2_Message;
            buttonTop.text = GroundOne.Decision2_TopText;
            buttonLeft.text = GroundOne.Decision2_LeftText;
            buttonRight.text = GroundOne.Decision2_RightText;
            buttonBottom.text = GroundOne.Decision2_BottomText;
            button1.text = GroundOne.Decision2_TopText;
            button2.text = GroundOne.Decision2_LeftText;
            button3.text = GroundOne.Decision2_RightText;
            button4.text = GroundOne.Decision2_BottomText;

            if (GroundOne.Decision2_SelectPermutation)
            {
                grpNormal.SetActive(false);
                grpPermutation.SetActive(true);
            }
            else
            {
                grpNormal.SetActive(true);
                grpPermutation.SetActive(false);
            }
        }

        public override void Update()
        {
            base.Update();
        }
        
        public void buttonTop_Click()
        {
            if (GroundOne.Decision2_SelectPermutation)
            {
                if (back_button1.GetComponent<Image>().color != Color.red)
                {
                    back_button1.GetComponent<Image>().color = Color.red;
                    this.Permutation1 = true;
                    CheckPermutationEnd();
                }
            }
            else
            {
                GroundOne.Decision2_Answer = AnswerType.Top;
                TapExit();
            }
        }

        public void buttonLeft_Click()
        {
            if (GroundOne.Decision2_SelectPermutation)
            {
                if (back_button2.GetComponent<Image>().color != Color.red)
                {
                    back_button2.GetComponent<Image>().color = Color.red;
                    if (Permutation1)
                    {
                        Permutation2 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                GroundOne.Decision2_Answer = AnswerType.Left;
                TapExit();
            }
        }

        public void buttonRight_Click()
        {
            if (GroundOne.Decision2_SelectPermutation)
            {
                if (back_button3.GetComponent<Image>().color != Color.red)
                {
                    back_button3.GetComponent<Image>().color = Color.red;
                    if (Permutation2)
                    {
                        Permutation3 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                GroundOne.Decision2_Answer = AnswerType.Right;
                TapExit();
            }
        }

        public void buttonBottom_Click()
        {
            if (GroundOne.Decision2_SelectPermutation)
            {
                if (back_button4.GetComponent<Image>().color != Color.red)
                {
                    back_button4.GetComponent<Image>().color = Color.red;
                    if (Permutation3)
                    {
                        Permutation4 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                GroundOne.Decision2_Answer = AnswerType.Bottom;
                TapExit();
            }
        }

        private void CheckPermutationEnd()
        {
            if (back_button1.GetComponent<Image>().color == Color.red &&
                back_button2.GetComponent<Image>().color == Color.red &&
                back_button3.GetComponent<Image>().color == Color.red &&
                back_button4.GetComponent<Image>().color == Color.red)
            {
                if (Permutation1 && Permutation2 && Permutation3 && Permutation4)
                {
                    Debug.Log("TruthDecision2: permutaion ok");
                    GroundOne.PermutationAnswer = true;
                    TapExit();
                }
                else
                {
                    Debug.Log("TruthDecision2: permutaion ng");
                    GroundOne.PermutationAnswer = false;
                    TapExit();
                }
            }
        }

        //private void button2_MouseEnter(Button sender)
        //{
        //    FocusBackColorChange(sender);
        //}
        //private void button1_Enter(Button sender)
        //{
        //    FocusBackColorChange(sender);
        //}
        //private void FocusBackColorChange(Object sender)
        //{
        //    if (this.SelectPermutation)
        //    {
        //        if (((Button)sender).GetComponent<Image>().color == Color.red)
        //        {
        //            return;
        //        }
        //    }

        //    foreach (Object btn in this.Controls)
        //    {
        //        if (btn.GetType() == typeof(Button))
        //        {
        //            if (((Button)btn).Equals(sender))
        //            {
        //                ((Button)sender).BackColor = Color.NavajoWhite;
        //            }
        //            else
        //            {
        //                if (((Button)btn).BackColor != Color.Red)
        //                {
        //                    ((Button)btn).BackColor = Color.AliceBlue;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void button2_MouseLeave(Button sender)
        //{
        //    FocurLeaveBackColorChange(sender);
        //}
        //private void button1_Leave(Button sender)
        //{
        //    FocurLeaveBackColorChange(sender);
        //}
        //private void FocurLeaveBackColorChange(Button sender)
        //{
        //    if (this.SelectPermutation)
        //    {
        //        return;
        //    }

        //    sender.GetComponent<Image>().color = UnityColor.Aliceblue;
        //}

        private void TapExit()
        {
            SceneDimension.Back(this);
        }
    }
}
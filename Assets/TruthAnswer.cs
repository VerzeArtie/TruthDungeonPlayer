using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class TruthAnswer : MotherForm
    {
        public Button[] buttonList;
        public Text[] textList;
        private int selectCounter = 0;
        bool failFlag = false;

        public override void Start()
        {
            base.Start();
            for (int ii = 0; ii < buttonList.Length; ii++)
            {
                Debug.Log(buttonList[ii]);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public void Button_Click(Button sender)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            Debug.Log(sender);
            if (sender.Equals(buttonList[this.selectCounter]) == false)
            {
                Debug.Log("detect fail sequence...");
                failFlag = true;
            }
            else
            {
                Debug.Log("ok");
            }
            sender.enabled = false;

            selectCounter++;
            if (selectCounter >= 5)
            {
                this.Background.GetComponent<Image>().color = Color.red;
                //for (int ii = 0; ii < buttonList.Length; ii++)
                //{
                //    buttonList[ii].GetComponent<Image>().color = Color.red;
                //    textList[ii].color = Color.white;
                //}
            }
            if (selectCounter >= 10)
            {
                this.Background.GetComponent<Image>().color = UnityColor.DarkSlateBlue;
                //for (int ii = 0; ii < buttonList.Length; ii++)
                //{
                //    buttonList[ii].GetComponent<Image>().color = UnityColor.DarkSlateBlue;
                //    textList[ii].color = Color.white;
                //}
            }
            if (selectCounter >= 13)
            {
                this.Background.GetComponent<Image>().color = Color.white;
                //for (int ii = 0; ii < buttonList.Length; ii++)
                //{
                //    buttonList[ii].GetComponent<Image>().color = Color.white;
                //    textList[ii].color = Color.black;
                //}
            }

            if (selectCounter >= buttonList.Length)
            {
                GroundOne.GodSeuqence = !failFlag; // 一度もミスしていなければ成功
                SceneDimension.Back(this);
            }
        }
    }
}
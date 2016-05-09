using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class TruthAnswer : MotherForm
    {
        public Button[] buttonList;
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

            selectCounter++;
        }
    }
}
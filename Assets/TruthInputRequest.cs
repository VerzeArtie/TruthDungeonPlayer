using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public class TruthInputRequest : MotherForm
    {
        public Text[] inputData;
        private List<int> numberList = null;

        public override void Start()
        {
            base.Start();
            this.numberList = new List<int>();
        }

        public override void Update()
        {
            base.Update();
        }

        public void AddNumber(Text txt)
        {
            if (this.numberList.Count >= inputData.Length) { return; }

            this.numberList.Insert(0, Convert.ToInt32(txt.text));

            for (int ii = 0; ii < this.numberList.Count; ii++)
            {
                inputData[ii].text = this.numberList[ii].ToString();
            }
        }

        public void tapReset()
        {
            this.numberList.Clear();
            for (int ii = 0; ii < inputData.Length; ii++)
            {
                inputData[ii].text = "";
            }
        }

        public void tapExit()
        {
            int inputValue = 0;
            for (int ii = 0; ii < inputData.Length; ii++)
            {
                if (inputData[ii].text != string.Empty)
                {
                    inputValue += Convert.ToInt32(inputData[ii].text) * (int)(Math.Pow(10, ii));
                }
            }

            GroundOne.InputValue = inputValue;
            Debug.Log("InputValue is " + GroundOne.InputValue);

            SceneDimension.Back(this);
        }
    }
}
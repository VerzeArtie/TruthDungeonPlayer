using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;

public class TruthWill : MotherForm {

    public Button[] buttonList;
    public Text[] textList;
    private int selectCounter = 0;
    bool failFlag = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Button_Click(Button sender)
    {
        if (sender.Equals(buttonList[this.selectCounter]) == false)
        {
            failFlag = true;
        }
        sender.enabled = false;

        selectCounter++;
        if (selectCounter >= 4)
        {
            this.Background.GetComponent<Image>().color = UnityColor.YellowGreen;
            //for (int ii = 0; ii < buttonList.Length; ii++)
            //{
            //    buttonList[ii].GetComponent<Image>().color = UnityColor.YellowGreen;
            //    textList[ii].color = Color.black;
            //}
        }
        if (selectCounter >= 9)
        {
            this.Background.GetComponent<Image>().color = UnityColor.MediumPurple;
            //for (int ii = 0; ii < buttonList.Length; ii++)
            //{
            //    buttonList[ii].GetComponent<Image>().color = UnityColor.MediumPurple;
            //    textList[ii].color = Color.white;
            //}
        }
        if (selectCounter >= 14)
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
            GroundOne.GodSeuqence = !failFlag; // １度もミスしていなければ成功
            SceneDimension.Back(this);
        }
    }
}

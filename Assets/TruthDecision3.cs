using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class TruthDecision3 : MotherForm
    {
        public Text MainMessage;
        public Text FirstMessage;
        public Text SecondMessage;
        public Text ThirdMessage;

        public override void Start()
        {
            base.Start();
            MainMessage.text = GroundOne.DecisionMainMessage;
            FirstMessage.text = "１　　" + GroundOne.DecisionFirstMessage;
            SecondMessage.text = "２　　" + GroundOne.DecisionSecondMessage;
            ThirdMessage.text = "３　　" + GroundOne.DecisionThirdMessage;
        }

        // 1
        public void button1_Click()
        {
            GroundOne.DecisionChoice = 1;
            SceneDimension.Back(this);
        }

        // 2
        public void button2_Click()
        {
            GroundOne.DecisionChoice = 2;
            SceneDimension.Back(this);
        }

        // 3
        public void button3_Click()
        {
            GroundOne.DecisionChoice = 3;
            SceneDimension.Back(this);
        }
    }
}

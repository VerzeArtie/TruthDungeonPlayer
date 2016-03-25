using UnityEngine;
using UnityEngine.UI;
using System;

namespace DungeonPlayer
{
    public class TruthDecision : MotherForm
    {
        public Text MainMessage;
        public Text FirstMessage;
        public Text SecondMessage;

        public override void Start()
        {
            base.Start();
            MainMessage.text = GroundOne.DecisionMainMessage;
            FirstMessage.text = "１　　" + GroundOne.DecisionFirstMessage;
            SecondMessage.text = "２　　" + GroundOne.DecisionSecondMessage;
        }
        
        // 1
        public void button1_Click()
        {
            GroundOne.DecisionChoice = 1;
            GroundOne.ParentScene.SceneBack();
            Application.UnloadLevel(Database.TruthDecision);
        }

        // 2
        public void button2_Click()
        {
            GroundOne.DecisionChoice = 2;
            GroundOne.ParentScene.SceneBack();
            Application.UnloadLevel(Database.TruthDecision);
        }
    }
}

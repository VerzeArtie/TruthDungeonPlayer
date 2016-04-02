using UnityEngine;
using System.Collections;

namespace DungeonPlayer
{
    public class TruthPotionShop : MotherForm
    {
        public override void Start()
        {
            base.Start();
            // todo

            if (GroundOne.WE.TruthCompleteArea1) GroundOne.WE.AvailablePotion2 = true;
            if (GroundOne.WE.TruthCompleteArea2) GroundOne.WE.AvailablePotion3 = true;
            if (GroundOne.WE.TruthCompleteArea3) GroundOne.WE.AvailablePotion4 = true;
            if (GroundOne.WE.TruthCompleteArea4) GroundOne.WE.AvailablePotion5 = true;
        }

        public override void Update()
        {
            base.Update();
        }

    }
}
using UnityEngine;
using System.Collections;

namespace DungeonPlayer
{
    public class TruthDuelRule : MotherForm
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }
       
        public void tapExit()
        {
            SceneDimension.Back(this);
        }
    }
}
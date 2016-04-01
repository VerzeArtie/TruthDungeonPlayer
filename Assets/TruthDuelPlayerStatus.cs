using UnityEngine;
using System.Collections;

namespace DungeonPlayer
{
    public class TruthDuelPlayerStatus : MotherForm
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
            GroundOne.ParentScene.SceneBack();
            Application.UnloadLevel(Database.TruthDuelPlayerStatus);
        }
    }
}
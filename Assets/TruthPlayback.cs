using UnityEngine;
using System.Collections;

//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;

namespace DungeonPlayer
{
    public partial class TruthPlayback : MotherForm
    {
        public GameObject[] nodeList;

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public void tapClose()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_PLAYBACK_CLOSE, string.Empty, string.Empty);

            SceneDimension.Back(this);
        }
    }	
}

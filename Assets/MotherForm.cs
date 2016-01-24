using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class MotherForm : MonoBehaviour
    {
        public virtual void Start()
        {
            if (GroundOne.InitializeGroundOne())
            {
                DontDestroyOnLoad(GroundOne.MC);
                DontDestroyOnLoad(GroundOne.SC);
                DontDestroyOnLoad(GroundOne.TC);
                DontDestroyOnLoad(GroundOne.WE);
                DontDestroyOnLoad(GroundOne.WE2);
            }
        }
    }
}

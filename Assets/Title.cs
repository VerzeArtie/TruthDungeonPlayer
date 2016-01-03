using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;
using System.Collections;

namespace DungeonPlayer
{
    public class Title : MonoBehaviour
    {
        void Start()
        {
            GroundOne.InitializeGroundOne();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DontDestroyOnLoad(GroundOne.MC);
                DontDestroyOnLoad(GroundOne.SC);
                DontDestroyOnLoad(GroundOne.TC);
                DontDestroyOnLoad(GroundOne.WE);
                DontDestroyOnLoad(GroundOne.WE2);
                SceneDimension.Go(Database.Title, Database.TruthHomeTown);
            }
        }
    }
}
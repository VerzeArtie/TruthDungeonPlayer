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
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneDimension.Go(Database.Title, Database.TruthHomeTown);
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;
using DungeonPlayer;

namespace DungeonPlayer
{
    public class Title : MotherForm
    {
        public Camera cam;
        public override void Start()
        {
            base.Start();

        }
        // Update is called once per frame
        void Update()
        {
        }


        public void GameStart_Click()
        {
            SceneDimension.CallTruthHomeTown(Database.Title);
        }

        public void Load_Click()
        {
            this.Filter.SetActive(true);
            SceneDimension.CallSaveLoad(Database.Title, false, false, this);
        }

        public void Config_Click()
        {

        }

        public void Exit_Click()
        {
            Application.Quit();
        }

        public void Seeker_Click()
        {

        }
    }
}
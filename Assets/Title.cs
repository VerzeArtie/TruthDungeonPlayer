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
    public class Title : MotherForm
    {
        bool ExecFirstGo = false;
        public override void Start()
        {
            base.Start();

            if (GroundOne.Title_LoadAndGo)
            {
                GroundOne.Title_LoadAndGo = false;
                this.ExecFirstGo = true;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (this.ExecFirstGo)
            {
                this.ExecFirstGo = false;
                SceneDimension.Go(Database.Title, Database.TruthHomeTown); // todo ÉçÅ[ÉhêÊÇÕà·Ç§

            }
        }

        public void GameStart_Click()
        {
            SceneDimension.Go(Database.Title, Database.TruthHomeTown);
        }

        public void Load_Click()
        {
            GroundOne.SaveMode = false;
            GroundOne.Title_LoadAndGo = true;
            SceneDimension.Go(Database.Title, Database.SaveLoad);
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
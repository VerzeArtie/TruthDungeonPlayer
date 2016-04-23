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
        public Text titleText;
        public override void Start()
        {
            base.Start();
            if (GroundOne.EnableBGM)
            {
                GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin); // Œã•Ò’Ç‰Á    
            }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SelectFalseStatue)
            {
                SettingRealWorldButtonLayout();
            }
        }

        private void SettingRealWorldButtonLayout()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void GameStart_Click()
        {
            GroundOne.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            GroundOne.MC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            GroundOne.MC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);

            SceneDimension.JumpToTruthHomeTown();
        }

        public void Load_Click()
        {
            this.Filter.SetActive(true);
            SceneDimension.CallSaveLoad(this, false, false);
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

        public void PointerEnter()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_LIFE_TAP);
        }
    }
}
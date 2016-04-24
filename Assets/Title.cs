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

        public void GameStart_Click()
        {
            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
            GroundOne.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
            GroundOne.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
            GroundOne.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
            GroundOne.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
            GroundOne.MC.AvailableMana = true;
            GroundOne.MC.AvailableSkill = true;
            GroundOne.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
            GroundOne.MC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
            GroundOne.MC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
            GroundOne.MC.MaxGain();
            GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

            GroundOne.SC.FirstName = Database.RANA_AMILIA;
            GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;
            GroundOne.SC.Strength = Database.SECONDPLAYER_FIRST_STRENGTH;
            GroundOne.SC.Intelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
            GroundOne.SC.Agility = Database.SECONDPLAYER_FIRST_AGILITY;
            GroundOne.SC.Stamina = Database.SECONDPLAYER_FIRST_STAMINA;
            GroundOne.SC.Mind = Database.SECONDPLAYER_FIRST_MIND;
            GroundOne.SC.AvailableMana = true;
            GroundOne.SC.AvailableSkill = true;
            GroundOne.SC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_KNUCKLE);
            GroundOne.SC.MainArmor = new ItemBackPack(Database.POOR_LIGHT_CROSS);
            GroundOne.SC.Accessory = new ItemBackPack(Database.COMMON_SANGO_BRESLET);
            GroundOne.SC.MaxGain();
            GroundOne.SC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.SC.BattleActionCommandList[1] = Database.DEFENSE_EN;

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
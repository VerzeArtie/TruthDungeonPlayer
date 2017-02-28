﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Xml;
using UnityEngine.Purchasing;

namespace DungeonPlayer
{
    public class GameSetting : MotherForm
    {
        public Slider BGMSlider = null;
        public Slider SoundSlider = null;
        public Slider DifficultySilder = null;
        public Toggle ToggleSupportLog = null;
        public Text TextAccount = null;
        public Button LanguageEngligh = null;
        public Button LanguageJapanese = null;

        public override void Start()
        {
            base.Start();

            this.BGMSlider.value = (float)((float)GroundOne.EnableBGM);
            this.SoundSlider.value = (float)((float)GroundOne.EnableSoundEffect);
            //this.battleSpeedBar.Value = this.battleSpeed;
            this.DifficultySilder.value = GroundOne.Difficulty;
            this.ToggleSupportLog.isOn = GroundOne.SupportLog;
            if (GroundOne.Language == GroundOne.GameLanguage.English)
            {
                LanguageEngligh.Select();
            }
            else
            {
                LanguageJapanese.Select();
            }
            this.TextAccount.text = "AccountID: " + GroundOne.WE2.Account;

        }
        
        public void ChangeBGMVolume(Slider sender)
        {
            GroundOne.EnableBGM = (int)sender.value;
            GroundOne.ChangeDungeonMusicVolume((float)(sender.value / 100.0f));
        }

        public void ChangeSoundEffectVolume(Slider sender)
        {
            GroundOne.EnableSoundEffect = (int)sender.value;
            GroundOne.ChangeSoundEffectVolume((float)(sender.value / 100.0f));
        }

        public void ChangeDifficulty(Slider sender)
        {
            GroundOne.Difficulty = (int)sender.value;
        }

        public void ChangeSupportLog(Toggle toggle)
        {
            GroundOne.SupportLog = toggle.isOn;
        }

        public void ChangeLanguage(int number)
        {
            if (number == 2)
            {
                GroundOne.Language = GroundOne.GameLanguage.English;
            }
            else
            {
                GroundOne.Language = GroundOne.GameLanguage.Japanese;
            }
        }

        public void tapClose()
        {
            try
            {
                //this.battleSpeed = battleSpeedBar.Value;
                //this.difficulty = difficultyBar.Value;

                XmlTextWriter xmlWriter = new XmlTextWriter(Database.GameSettingFileName, System.Text.Encoding.UTF8);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteStartElement("Body");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("EnableBGM", GroundOne.EnableBGM.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("EnableSoundEffect", GroundOne.EnableSoundEffect.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                //xmlWriter.WriteElementString("BattleSpeed", this.battleSpeed.ToString());
                //xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("Difficulty", GroundOne.Difficulty.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("SupportLog", GroundOne.SupportLog.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteElementString("GameLanguage", GroundOne.Language.ToString());
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch { }

            SceneDimension.Back(this);
        }
    }
}
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Xml;
using System.Reflection;
using System.Text;
using System.IO;

namespace DungeonPlayer
{
    public class SaveLoad : MotherForm
    {
        public GameObject back_SystemMessage;
        public Text systemMessage;
        public Text titleLabel;
        public Button[] back_button;
        public Text[] buttonText;
        public Button[] buttonPage;
        public Text lblClose;

        private string gameDayString = "\r\n経過日数：";
        private string gameDayString2 = @"日 ";
        private string archiveAreaString = @"到達階層：";
        private string archiveAreaString2 = @"階";
        private string archiveAreaString3 = @"制覇";

        private string MESSAGE_1 = @"DungeonPlayerクリアデータです。本編ではロードできません。";
        private string MESSAGE_2 = @"保存が完了しました。";
        private string MESSAGE_OVERWRITE = @"既にデータが存在します。上書きしてセーブしますか？";
        private string MESSAGE_NOWLOADING = @"しばらくお待ちください...";

        private bool nowAutoKill = false;
        private int autoKillTimer = 0;

        private Text currentSaveText = null;
        private string currentTargetFileName = string.Empty;

        private int pageNumber = 1;
        private string[] filenameList = null;
        private DateTime newDateTime = new DateTime(1, 1, 1, 0, 0, 0);
        private DateTime newDateTimeAuto = new DateTime(1, 1, 1, 0, 0, 0);

        public Image pbSandglass;
        private Sprite imageSandglass;
        private CurrentPhase currentPhase = CurrentPhase.None;
        private Text txtSender;
        private bool forceSave = false;

        private enum CurrentPhase
        {
            None,
            Save,
            Load,
            Complete,
        }

        // Use this for initialization
        public override void Start()
        {
            base.Start();
            Debug.Log("saveload start");

            if (GroundOne.SaveAndExit)
            {
                RealWorldSave();
                SceneDimension.Back(this);
                return;
            }


            this.Background.GetComponent<Image>().color = UnityColor.Aqua;
            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                titleLabel.text = Database.GUI_SAVELOAD_LOAD;
                lblClose.text = Database.GUI_S_BASIC_CLOSE;
            }
            else
            {
                titleLabel.text = "LOAD";
            }
            if (GroundOne.SaveMode)
            {
                if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
                {
                    titleLabel.text = Database.GUI_SAVELOAD_SAVE;
                }
                else
                {
                    titleLabel.text = "SAVE";
                }
                this.Background.GetComponent<Image>().color = UnityColor.Salmon;
            }

            Method.MakeDirectory();

            this.filenameList = System.IO.Directory.GetFiles(Method.PathForSaveFile(), "*.xml");

            // 一番新しいファイルのナンバーを記憶する。
            int newNumber = 0;
            for (int ii = 0; ii < filenameList.Length; ii++)
            {
                string targetString = System.IO.Path.GetFileName(filenameList[ii]);
                if (Convert.ToInt32(targetString.Substring(0, 3)) >= Database.WORLD_SAVE_NUM) { continue; } // オートセーブは検査対象外

                string DateTimeString = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2);
                DateTime targetDateTime = DateTime.Parse(DateTimeString);

                // 手動セーブと自動セーブの二つを記憶する。
                int currentNumber = Convert.ToInt32(targetString.Substring(0, 3));
                if (targetDateTime > newDateTime && currentNumber <= 200)
                {
                    newDateTime = targetDateTime;
                    newNumber = currentNumber;
                }
                if (targetDateTime > newDateTimeAuto && currentNumber > 200)
                {
                    newDateTimeAuto = targetDateTime;
                    //newNumber = currentNumber; // 自動セーブはページ自動移動の範囲に含めない。
                }
            }

            PageMove((newNumber - 1) / buttonText.Length + 1);

            if (GroundOne.SaveMode)
            {
                buttonPage[20].enabled = false;
                buttonPage[20].gameObject.SetActive(false);
            }

            int BASE_SIZE_X = 152;
            int BASE_SIZE_Y = 211;
            this.imageSandglass = Sprite.Create(Resources.Load<Texture2D>("SandGlassIcon"), new Rect(0, 0, BASE_SIZE_X, BASE_SIZE_Y), new Vector2(0, 0));
            this.pbSandglass.sprite = this.imageSandglass;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if (this.currentPhase == CurrentPhase.None)
            {
                // no action
            }
            else if (this.currentPhase == CurrentPhase.Save || this.currentPhase == CurrentPhase.Load)
            {
                ExecSaveLoad();
            }
            else
            {
                if (GroundOne.SaveMode)
                {
                    this.systemMessage.text = MESSAGE_2;
                }
                else
                {
                    this.systemMessage.text = "ゲームデータの読み込みが完了しました。";
                }
                this.pbSandglass.gameObject.SetActive(false);
            }

            if (this.nowAutoKill)
            {
                this.autoKillTimer++;
                if (this.autoKillTimer == 200)
                {
                    tapExit();
                }
            }
        }

        private void PageMove(int pageNum)
        {
            this.pageNumber = pageNum;
            for (int ii = 0; ii < buttonText.Length; ii++)
            {
                buttonText[ii].text = "";
                back_button[ii].GetComponent<Image>().sprite = null;
            }
            for (int ii = 0; ii < buttonPage.Length; ii++)
            {
                buttonPage[ii].GetComponent<Image>().sprite = null;
            }
            Debug.Log("buttonpage: " + this.pageNumber);
            buttonPage[this.pageNumber - 1].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "SaveLoadNewPageNum");


            for (int ii = 0; ii < filenameList.Length; ii++)
            {
                string filename = filenameList[ii];
                //Debug.Log("filename: " + filename);
                Text targetButton = null;
                string targetString = System.IO.Path.GetFileName(filename);
                for (int jj = 0; jj < buttonText.Length; jj++)
                {
                    if (targetString.Contains(((jj + 1) + ((this.pageNumber - 1) * buttonText.Length)).ToString("D3") + "_"))
                    {
                        targetButton = buttonText[jj];
                        targetButton.text = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2) + this.gameDayString + targetString.Substring(18, 3) + this.gameDayString2 + archiveAreaString;

                        string DateTimeString = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2);
                        DateTime targetDateTime = DateTime.Parse(DateTimeString);
                        if (targetDateTime.Equals(this.newDateTime))
                        {
                            back_button[jj].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.SAVELOAD_NEW);
                        }
                        if (targetDateTime.Equals(this.newDateTimeAuto))
                        {
                            back_button[jj].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.SAVELOAD_NEW_AUTO);
                        }


                        if (targetString.Substring(21, 1) == "6")
                        {
                            targetButton.text += this.archiveAreaString3;
                        }
                        else
                        {
                            targetButton.text += targetString.Substring(21, 1) + this.archiveAreaString2;
                        }

                        if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SelectFalseStatue)
                        {
                            targetButton.text = "";
                        }
                    }
                }
            }
        }
        
        public void TapSelectNumber(Text txtNumber)
        {
            Debug.Log("txtNumber: " + txtNumber.text.ToString());
            //GroundOne.SQL.UpdateOwner(Database.LOG_SAVELOAD_PAGE, txtNumber.text, String.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            if (txtNumber.text == "A")
            {
                PageMove(Database.AUTOSAVE_PAGE_NUM);
            }
            else
            {
                PageMove(Convert.ToInt32(txtNumber.text));
            }
        }

        public void tapButton(Text sender)
        {
            Debug.Log(sender.text);
            //GroundOne.SQL.UpdateOwner(Database.LOG_SAVELOAD_NUMBER, sender.text, String.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);

            this.txtSender = sender;
            this.systemMessage.text = MESSAGE_NOWLOADING;
            this.pbSandglass.sprite = this.imageSandglass;

            if (GroundOne.SaveMode)
            {
                string targetFileName = String.Empty;
                for (int ii = 0; ii < buttonText.Length; ii++)
                {
                    if (this.txtSender.Equals(buttonText[ii]))
                    {
                        targetFileName = ((ii + 1) + ((this.pageNumber - 1) * buttonText.Length)).ToString("D3") + "_";
                        break;
                    }
                }
                bool result = TryExecSave(this.txtSender, targetFileName);
                if (result == false)
                {
                    return;
                }
            }

            this.back_SystemMessage.SetActive(true);
            StartCoroutine(WaitOnly());
        }

        private IEnumerator WaitOnly()
        {
            yield return new WaitForEndOfFrame();

            if (GroundOne.SaveMode)
            {
                this.currentPhase = CurrentPhase.Save;
            }
            else
            {
                this.currentPhase = CurrentPhase.Load;
            }

            yield return null;
        }

        private void ExecSaveLoad()
        {
            Debug.Log("ExecSaveLoad(S)");
            //
            // セーブ！！！
            //
            if (GroundOne.SaveMode)
            {
                string targetFileName = String.Empty;
                for (int ii = 0; ii < buttonText.Length; ii++)
                {
                    if (this.txtSender.Equals(buttonText[ii]))
                    {
                        targetFileName = ((ii + 1) + ((this.pageNumber - 1) * buttonText.Length)).ToString("D3") + "_";
                        break;
                    }
                }
                ExecSave(this.txtSender, targetFileName, this.forceSave); // 後編移動
                this.forceSave = false;
            }
            //
            // ロード！！！
            //
            else
            {
                if ((this.txtSender).text == String.Empty) { return; }

                string targetFileName = String.Empty;
                for (int ii = 0; ii < buttonText.Length; ii++)
                {
                    if (this.txtSender.Equals(buttonText[ii]))
                    {
                        targetFileName = (ii + 1 + ((this.pageNumber - 1) * buttonText.Length)).ToString("D3") + "_";
                        break;
                    }
                }
                ExecLoad(this.txtSender, targetFileName, false); // 後編移動
            }
            this.currentPhase = CurrentPhase.Complete;
        }

        public override void ExitYes()
        {
            base.ExitYes();
            if (this.yesnoSystemMessage.text == this.MESSAGE_OVERWRITE)
            {
                HideAllChild();
                this.systemMessage.text = MESSAGE_NOWLOADING;
                this.pbSandglass.sprite = this.imageSandglass;
                this.back_SystemMessage.SetActive(true);

                this.forceSave = true;
                StartCoroutine(WaitOnly());
            }
        }

        public override void ExitNo()
        {
            base.ExitNo();
            if (this.yesnoSystemMessage.text == this.MESSAGE_OVERWRITE)
            {
                HideAllChild();
                this.currentSaveText = null;
                this.currentTargetFileName = string.Empty;
            }
        }

        public void RealWorldSave()
        {
            ExecSave(null, Database.WorldSaveNum, true);
        }

        private bool TryExecSave(Text sender, string targetFileName)
        {
            foreach (string overwriteData in System.IO.Directory.GetFiles(Method.PathForSaveFile(), "*.xml"))
            {
                if (overwriteData.Contains(targetFileName))
                {
                    this.currentSaveText = sender;
                    this.currentTargetFileName = targetFileName;
                    this.yesnoSystemMessage.text = this.MESSAGE_OVERWRITE;
                    this.groupYesnoSystemMessage.SetActive(true);
                    this.Filter.SetActive(true);
                    return false;
                }
            }
            return true;
        }

        private void ExecSave(Text sender, string targetFileName, bool forceSave)
        {
            DateTime now = DateTime.Now;

            foreach (string overwriteData in System.IO.Directory.GetFiles(Method.PathForSaveFile(), "*.xml"))
            {
                if (overwriteData.Contains(targetFileName))
                {
                    if (forceSave == false) // if 後編追加
                    {
                        this.currentSaveText = sender;
                        this.currentTargetFileName = targetFileName;
                        this.yesnoSystemMessage.text = this.MESSAGE_OVERWRITE;
                        this.groupYesnoSystemMessage.SetActive(true);
                        this.Filter.SetActive(true);
                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(overwriteData); // 後編追加
                    }
                }
            }

            targetFileName += now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") + now.Hour.ToString("D2") + now.Minute.ToString("D2") + now.Second.ToString("D2") + GroundOne.WE.GameDay.ToString("D3");
            if (GroundOne.WE.CompleteArea5 || GroundOne.WE.TruthCompleteArea5) // 後編編集
            {
                targetFileName += Convert.ToString(6);
            }
            else if (GroundOne.WE.CompleteArea4 || GroundOne.WE.TruthCompleteArea4) // 後編編集
            {
                targetFileName += Convert.ToString(5);
            }
            else if (GroundOne.WE.CompleteArea3 || GroundOne.WE.TruthCompleteArea3) // 後編編集
            {
                targetFileName += Convert.ToString(4);
            }
            else if (GroundOne.WE.CompleteArea2 || GroundOne.WE.TruthCompleteArea2) // 後編編集
            {
                targetFileName += Convert.ToString(3);
            }
            else if (GroundOne.WE.CompleteArea1 || GroundOne.WE.TruthCompleteArea1) // 後編編集
            {
                targetFileName += Convert.ToString(2);
            }
            else
            {
                targetFileName += Convert.ToString(1);
            }
            targetFileName += ".xml";

            XmlTextWriter xmlWriter = new XmlTextWriter(Method.pathForDocumentsFile(targetFileName), Encoding.UTF8);
            try
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteStartElement("Body");
                xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter.WriteElementString("Version", Database.VERSION.ToString());
                xmlWriter.WriteWhitespace("\r\n");

                // メインプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_MAINPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                // [昇華]：本記載テクニックを横展開してください。
                Type type = GroundOne.MC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.MC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.MC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (GroundOne.MC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.MC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.MC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.MC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (GroundOne.MC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.MC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.MC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.MC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.MC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.MC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                if (GroundOne.MC != null)
                {
                    // バックパック
                    ItemBackPack[] backpackInfo = GroundOne.MC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }

                    // ソウル・ポイント
                    int[] soulAttributes = GroundOne.MC.CurrentSoulAttributes;
                    for (int ii = 0; ii < soulAttributes.Length; ii++)
                    {
                        xmlWriter.WriteElementString("SoulAttribute" + ii.ToString(), soulAttributes[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // セカンドプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_SECONDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = GroundOne.SC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.SC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.SC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加   
                }

                // プレイヤー装備
                if (GroundOne.SC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.SC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.SC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.SC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }                    // e 後編追加
                if (GroundOne.SC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.SC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.SC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.SC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.SC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.SC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                if (GroundOne.SC != null)
                {
                    // バックパック
                    ItemBackPack[] backpackInfo = GroundOne.SC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }

                    // ソウル・ポイント
                    int[] soulAttributes = GroundOne.SC.CurrentSoulAttributes;
                    for (int ii = 0; ii < soulAttributes.Length; ii++)
                    {
                        xmlWriter.WriteElementString("SoulAttribute" + ii.ToString(), soulAttributes[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");


                // サードプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_THIRDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = GroundOne.TC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.TC, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.TC, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSpellType)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.AdditionalSkillType)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (GroundOne.TC.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", GroundOne.TC.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.TC.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", GroundOne.TC.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (GroundOne.TC.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", GroundOne.TC.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (GroundOne.TC.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", GroundOne.TC.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (GroundOne.TC.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", GroundOne.TC.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                if (GroundOne.TC != null)
                {
                    // バックパック
                    ItemBackPack[] backpackInfo = GroundOne.TC.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }

                    // ソウル・ポイント
                    int[] soulAttributes = GroundOne.TC.CurrentSoulAttributes;
                    for (int ii = 0; ii < soulAttributes.Length; ii++)
                    {
                        xmlWriter.WriteElementString("SoulAttribute" + ii.ToString(), soulAttributes[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter.WriteStartElement("WorldEnvironment");
                xmlWriter.WriteWhitespace("\r\n");
                if (GroundOne.WE != null)
                {
                    Type typeWE = GroundOne.WE.GetType();
                    foreach (PropertyInfo pi in typeWE.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE, null))).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE, null)));
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE, null)).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");

                // s 後編追加
                if (GroundOne.Truth_KnownTileInfo != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonOneInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileOne" + ii, GroundOne.Truth_KnownTileInfo[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo2 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonTwoInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileTwo" + ii, GroundOne.Truth_KnownTileInfo2[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo3 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonThreeInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileThree" + ii, GroundOne.Truth_KnownTileInfo3[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo4 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFourInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFour" + ii, GroundOne.Truth_KnownTileInfo4[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (GroundOne.Truth_KnownTileInfo5 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFiveInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFive" + ii, GroundOne.Truth_KnownTileInfo5[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndDocument();
            }
            finally
            {
                xmlWriter.Close();

                if ((Text)sender != null) // if 後編追加
                {
                    ((Text)sender).text = DateTime.Now.ToString() + "\r\n経過日数：" + GroundOne.WE.GameDay.ToString("D3") + "日 ";
                    if (GroundOne.WE.CompleteArea5 || GroundOne.WE.TruthCompleteArea5) // 後編編集
                    {
                        ((Text)sender).text += archiveAreaString + archiveAreaString3;
                    }
                    else if (GroundOne.WE.CompleteArea4 || GroundOne.WE.TruthCompleteArea4) // 後編編集
                    {
                        ((Text)sender).text += archiveAreaString + "5" + archiveAreaString2;
                    }
                    else if (GroundOne.WE.CompleteArea3 || GroundOne.WE.TruthCompleteArea3) // 後編編集
                    {
                        ((Text)sender).text += archiveAreaString + "4" + archiveAreaString2;
                    }
                    else if (GroundOne.WE.CompleteArea2 || GroundOne.WE.TruthCompleteArea2) // 後編編集
                    {
                        ((Text)sender).text += archiveAreaString + "3" + archiveAreaString2;
                    }
                    else if (GroundOne.WE.CompleteArea1 || GroundOne.WE.TruthCompleteArea1) // 後編編集
                    {
                        ((Text)sender).text += archiveAreaString + "2" + archiveAreaString2;
                    }
                    else
                    {
                        ((Text)sender).text += archiveAreaString + "1" + archiveAreaString2;
                    }

                    if (!((Text)sender).Equals(buttonText[0])) back_button[0].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[1])) back_button[1].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[2])) back_button[2].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[3])) back_button[3].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[4])) back_button[4].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[5])) back_button[5].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[6])) back_button[6].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[7])) back_button[7].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[8])) back_button[8].GetComponent<Image>().sprite = null;
                    if (!((Text)sender).Equals(buttonText[9])) back_button[9].GetComponent<Image>().sprite = null;
                    for (int ii = 0; ii < buttonText.Length; ii++)
                    {
                        if (sender.Equals(buttonText[ii]))
                        {
                            back_button[ii].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + Database.SAVELOAD_NEW);
                        }
                    }
                }

                // s 後編追加
                Method.AutoSaveTruthWorldEnvironment();
                // e 後編追加

                if (!forceSave) // if 後編追加
                {
                    this.systemMessage.text = this.MESSAGE_2;
                    this.back_SystemMessage.SetActive(true);
                    this.Filter.SetActive(true);
                    this.autoKillTimer = 0;
                    this.nowAutoKill = true;
                }
            }

            // セーブデータをサーバーへ転送する。
            try
            {
                Debug.Log("Call UpdateSaveData");
                using (FileStream fs = new FileStream(Method.pathForDocumentsFile(targetFileName), FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] save_current = br.ReadBytes((int)fs.Length);
                        using (FileStream fs2 = new FileStream(Method.PathForRootFile(Database.WE2_FILE), FileMode.Open))
                        {
                            using (BinaryReader br2 = new BinaryReader(fs2))
                            {
                                byte[] save_we2 = br2.ReadBytes((int)fs2.Length);
                                GroundOne.SQL.UpdaeSaveData(save_current, save_we2, sender.text, this.pageNumber.ToString());
                            }
                        }
                    }
                }
                
                Debug.Log("Call UpdateSaveData ok");
            }
            catch (Exception ex)
            {
                Debug.Log("ExecSave error: " + ex.ToString());
            }

        }

        private void ExecLoad(Text sender, string targetFileName, bool forceLoad)
        {
            if (this.nowAutoKill) { return; }
            Debug.Log("ExecLoad 0 " + DateTime.Now);

            GroundOne.ReInitializeGroundOne(true);

            XmlDocument xml = new XmlDocument();
            DateTime now = DateTime.Now;
            string yearData = String.Empty;
            string monthData = String.Empty;
            string dayData = String.Empty;
            string hourData = String.Empty;
            string minuteData = String.Empty;
            string secondData = String.Empty;
            string gamedayData = String.Empty;
            string completeareaData = String.Empty;

            Debug.Log("ExecLoad 1 " + DateTime.Now);
            if (((Text)sender) != null)
            {
                yearData = ((Text)sender).text.Substring(0, 4);
                monthData = ((Text)sender).text.Substring(5, 2);
                dayData = ((Text)sender).text.Substring(8, 2);
                hourData = ((Text)sender).text.Substring(11, 2);
                minuteData = ((Text)sender).text.Substring(14, 2);
                secondData = ((Text)sender).text.Substring(17, 2);
                gamedayData = ((Text)sender).text.Substring(this.gameDayString.Length + 19, 3);
                completeareaData = ((Text)sender).text.Substring(this.gameDayString.Length + this.gameDayString2.Length + this.archiveAreaString.Length + 22, 1);

                if (completeareaData == "制")
                {
                    this.systemMessage.text = MESSAGE_1;
                    this.back_SystemMessage.SetActive(true);
                    this.Filter.SetActive(true);
                    return;
                }
                targetFileName += yearData + monthData + dayData + hourData + minuteData + secondData + gamedayData + completeareaData + ".xml";
            }
            else
            {
                foreach (string currentFile in System.IO.Directory.GetFiles(Method.PathForSaveFile(), "*.xml"))
                {
                    if (currentFile.Contains(Database.WorldSaveNum))
                    {
                        targetFileName = System.IO.Path.GetFileName(currentFile);
                        break;
                    }
                }
            }

            xml.Load(Method.pathForDocumentsFile(targetFileName));
            GroundOne.CurrentLoadFileName = targetFileName;
            Debug.Log("ExecLoad 2 " + DateTime.Now);

            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("SubWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainArmor");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.Accessory = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory2");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加
            Debug.Log("ExecLoad 3 " + DateTime.Now);

            // s 後編編集
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {

                XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
                foreach (XmlNode node in temp)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            // e 後編編集
            for (int ii = 0; ii < Database.SOUL_ATTRIBUTE_NUM; ii++)
            {
                XmlNodeList temp = xml.GetElementsByTagName("SoulAttribute" + ii.ToString());
                foreach (XmlNode node in temp)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        GroundOne.MC.CurrentSoulAttributes[ii] = Convert.ToInt32(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        GroundOne.SC.CurrentSoulAttributes[ii] = Convert.ToInt32(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        GroundOne.TC.CurrentSoulAttributes[ii] = Convert.ToInt32(node.InnerText);
                    }
                }
            }

            Debug.Log("ExecLoad 4 " + DateTime.Now);

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try { pi.SetValue(GroundOne.MC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText, null); }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.SC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                    try { pi.SetValue(GroundOne.TC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSpellType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSpellType)Enum.Parse(typeof(MainCharacter.AdditionalSpellType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(MainCharacter.AdditionalSkillType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.MC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.SC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.TC, (MainCharacter.AdditionalSkillType)Enum.Parse(typeof(MainCharacter.AdditionalSkillType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // e 後編追加
            }
            Debug.Log("ExecLoad 5 " + DateTime.Now);

            Type typeWE = GroundOne.WE.GetType();
            Debug.Log("ExecLoad 6 " + DateTime.Now);


            PropertyInfo[] tempWE = typeWE.GetProperties();
            Debug.Log(tempWE.Length.ToString());

            foreach (PropertyInfo pi in tempWE)
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText, null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(GroundOne.WE, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/WorldEnvironment/" + (pi.Name)).InnerText), null);
                    }
                    catch { }
                }
            }
            Debug.Log("ExecLoad 7 " + DateTime.Now);

            // after revive
            //try // 後編追加 // [警告]：前編での読み込みバグが無く、かつ、後編では絶対に使わないことを前提とした記述。
            //{
            //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
            //    {
            //        knownTileInfo[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileOne" + ii.ToString()))[0].InnerText);
            //        knownTileInfo2[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileTwo" + ii.ToString()))[0].InnerText);
            //        knownTileInfo3[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileThree" + ii.ToString()))[0].InnerText);
            //        knownTileInfo4[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFour" + ii.ToString()))[0].InnerText);
            //        knownTileInfo5[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFive" + ii.ToString()))[0].InnerText);
            //    }
            //}
            //catch { }

            // [必須] 最終的には全階層分のデータを一括取得するようになるので、このFor分割は不要となる。
            //string temp1 = DateTime.Now.ToString() + "  " + DateTime.Now.Millisecond.ToString();

            Method.ReloadTruthWorldEnvironment();
            Debug.Log("ExecLoad 75 " + DateTime.Now + DateTime.Now.Millisecond);

            // ここでは、ダンジョンタイルデータのロードを行う。
            Method.LoadKnownTileInfo();

            Debug.Log(DateTime.Now.ToString());
            Debug.Log("ExecLoad 8-1 " + DateTime.Now + DateTime.Now.Millisecond);

            if (forceLoad == false)
            {
                this.systemMessage.text = "ゲームデータの読み込みが完了しました。";
                this.back_SystemMessage.SetActive(true);
                this.autoKillTimer = 0;
                this.nowAutoKill = true;
            }
            Debug.Log("ExecLoad end");
        }
        // move-out(e) 後編追加
        
        public void HideAllChild()
        {
            this.groupYesnoSystemMessage.SetActive(false);
            this.back_SystemMessage.SetActive(false);
            this.Filter.SetActive(false);
            this.currentPhase = CurrentPhase.None;
            this.systemMessage.text = "";
        }

        public void tapExit()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_SAVELOAD_CLOSE, String.Empty, String.Empty);
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            if (this.systemMessage.text == this.MESSAGE_1 || this.systemMessage.text == this.MESSAGE_2)
            {
                HideAllChild();
            }
            else if (this.nowAutoKill)
            {
                if (GroundOne.AfterBacktoTitle)
                {
                    SceneDimension.JumpToTitle();
                }
                else if (GroundOne.Parent.Count > 0)
                {
                    GroundOne.Parent[GroundOne.Parent.Count - 1].NextScene();
                }
            }
            else if (GroundOne.AfterBacktoTitle)
            {
                SceneDimension.JumpToTitle();
            }
            else
            {
                SceneDimension.Back(this);
            }
        }
    }
}

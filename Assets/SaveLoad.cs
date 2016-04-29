using UnityEngine;
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

        private string gameDayString = "\r\n経過日数：";
        private string gameDayString2 = @"日 ";
        private string archiveAreaString = @"到達階層：";
        private string archiveAreaString2 = @"階";
        private string archiveAreaString3 = @"制覇";

        private string MESSAGE_1 = @"DungeonPlayerクリアデータです。本編ではロードできません。";
        private string MESSAGE_2 = @"保存が完了しました。";
        private string MESSAGE_OVERWRITE = @"既にデータが存在します。上書きしてセーブしますか？";

        private bool nowAutoKill = false;
        private int autoKillTimer = 0;

        private Text currentSaveText = null;
        private string currentTargetFileName = string.Empty;

        private int pageNumber = 1;
        private string[] filenameList = null;
        private DateTime newDateTime = new DateTime(1, 1, 1, 0, 0, 0);

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.SaveAndExit)
            {
                RealWorldSave();
                SceneDimension.Back(this);
                return;
            }

            this.Background.GetComponent<Image>().color = UnityColor.Aqua;
            if (GroundOne.SaveMode)
            {
                titleLabel.text = "SAVE";
                this.Background.GetComponent<Image>().color = UnityColor.Salmon;
            }

            MakeDirectory();

            this.filenameList = System.IO.Directory.GetFiles(GetDirectoryName(), "*.xml");

            // 一番新しいファイルのナンバーを記憶する。
            for (int ii = 0; ii < filenameList.Length; ii++)
            {
                string targetString = System.IO.Path.GetFileName(filenameList[ii]);
                string DateTimeString = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2);
                DateTime targetDateTime = DateTime.Parse(DateTimeString);
                if (targetDateTime > newDateTime)
                {
                    newDateTime = targetDateTime;
                }

            }
            PageMove(1);
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
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

            for (int ii = 0; ii < filenameList.Length; ii++)
            {
                string filename = filenameList[ii];
                Debug.Log("filename: " + filename);
                Text targetButton = null;
                string targetString = System.IO.Path.GetFileName(filename);
                for (int jj = 0; jj < buttonText.Length; jj++)
                {
                    if (targetString.Contains(((jj + 1) + ((this.pageNumber - 1) * 10)).ToString("D3") + "_"))
                    {
                        targetButton = buttonText[jj];
                        targetButton.text = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2) + this.gameDayString + targetString.Substring(18, 3) + this.gameDayString2 + archiveAreaString;

                        string DateTimeString = targetString.Substring(4, 4) + "/" + targetString.Substring(8, 2) + "/" + targetString.Substring(10, 2) + " " + targetString.Substring(12, 2) + ":" + targetString.Substring(14, 2) + ":" + targetString.Substring(16, 2);
                        DateTime targetDateTime = DateTime.Parse(DateTimeString);
                        if (targetDateTime.Equals(this.newDateTime))
                        {
                            back_button[jj].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "SaveLoadNew2");
                        }

                        if (targetString.Substring(21, 1) == "6")
                        {
                            targetButton.text += this.archiveAreaString3;
                        }
                        else
                        {
                            targetButton.text += targetString.Substring(21, 1) + this.archiveAreaString2;
                        }

                        if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
                        {
                            targetButton.text = "";
                        }
                    }
                }
            }
        }

        private string GetDirectoryName()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Application.persistentDataPath.Substring(0, Application.persistentDataPath.LastIndexOf('/')); // after (ios)
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return Application.persistentDataPath.Substring(0, Application.persistentDataPath.LastIndexOf('/'));
            }
            else
            {
                return Database.BaseSaveFolder;
            }

        }
        private void MakeDirectory()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // なし
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                // なし
            }
            else
            {
                if (System.IO.Directory.Exists(Database.BaseSaveFolder) == false)
                {
                    System.IO.Directory.CreateDirectory(Database.BaseSaveFolder);
                }
            }
        }

        private string pathForDocumentsFile(string filename)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(Path.Combine(path, "Documents"), filename);
            }

            else if (Application.platform == RuntimePlatform.Android)
            {
                string path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, filename);
            }

            else
            {
                return Database.BaseSaveFolder + filename;
            }
        }

        public void TapSelectNumber(Text txtNumber)
        {
            Debug.Log("txtNumber: " + txtNumber.text.ToString());
            PageMove(Convert.ToInt32(txtNumber.text));
        }

        public void tapButton(Text sender)
        {
            Debug.Log(sender.text);
            //
            // セーブ！！！
            //
            if (GroundOne.SaveMode)
            {
                string targetFileName = String.Empty;
                for (int ii = 0; ii < buttonText.Length; ii++)
                {
                    if (sender.Equals(buttonText[ii]))
                    {
                        targetFileName = ((ii + 1) + ((this.pageNumber - 1) * 10)).ToString("D3") + "_";
                        break;
                    }
                }
                ExecSave((Text)sender, targetFileName, false); // 後編移動
            }
            //
            // ロード！！！
            //
            else
            {
                if (((Text)sender).text == String.Empty) return;

                string targetFileName = String.Empty;
                for (int ii = 0; ii < buttonText.Length; ii++)
                {
                    if (sender.Equals(buttonText[ii]))
                    {
                        targetFileName = (ii + 1 + ((this.pageNumber - 1) * 10)).ToString("D3") + "_";
                        break;
                    }
                }
                ExecLoad((Text)sender, targetFileName, false); // 後編移動
            }
        }

        public override void ExitYes()
        {
            base.ExitYes();
            if (this.yesnoSystemMessage.text == this.MESSAGE_OVERWRITE)
            {
                HideAllChild();
                ExecSave(this.currentSaveText, this.currentTargetFileName, true);
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
            ExecSave(null, "999_", true);
        }
        public void RealWorldLoad()
        {
            ExecLoad(null, "999_", true);
        }

        private void ExecSave(Text sender, string targetFileName, bool forceSave)
        {
            DateTime now = DateTime.Now;

            foreach (string overwriteData in System.IO.Directory.GetFiles(GetDirectoryName(), "*.xml"))
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

            XmlTextWriter xmlWriter = new XmlTextWriter(pathForDocumentsFile(targetFileName), Encoding.UTF8);
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
                    else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.PlayerStance)pi.GetValue(GroundOne.MC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
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

                // バックパック
                if (GroundOne.MC != null)
                {
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
                    else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.PlayerStance)pi.GetValue(GroundOne.SC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
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

                // バックパック
                if (GroundOne.SC != null)
                {
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
                    else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((MainCharacter.PlayerStance)pi.GetValue(GroundOne.TC, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
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

                // バックパック
                if (GroundOne.TC != null)
                {
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


                // ダンジョン１階の制覇情報
                // [警告]：作業落とし込みで終わるものの拡張性を考慮した設計に直してください。
                // after revive
                //if (this.knownTileInfo != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonOneInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileOne" + ii, this.knownTileInfo[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo2 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonTwoInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileTwo" + ii, this.knownTileInfo2[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo3 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonThreeInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileThree" + ii, this.knownTileInfo3[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo4 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonFourInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileFour" + ii, this.knownTileInfo4[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

                //if (this.knownTileInfo5 != null) // 後編追加
                //{
                //    xmlWriter.WriteStartElement("DungeonFiveInfo");
                //    xmlWriter.WriteWhitespace("\r\n");
                //    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                //    {
                //        xmlWriter.WriteElementString("tileFive" + ii, this.knownTileInfo5[ii].ToString());
                //        xmlWriter.WriteWhitespace("\r\n");
                //    }
                //    xmlWriter.WriteEndElement();
                //    xmlWriter.WriteWhitespace("\r\n");
                //}

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
                            back_button[ii].GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + "SaveLoadNew2");
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
                foreach (string currentFile in System.IO.Directory.GetFiles(GetDirectoryName(), "*.xml"))
                {
                    if (currentFile.Contains("999_"))
                    {
                        targetFileName = System.IO.Path.GetFileName(currentFile);
                        break;
                    }
                }
            }

            xml.Load(pathForDocumentsFile(targetFileName));
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

            //for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            //{
            //    XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
            //    if (temp.Count <= 0)
            //    {
            //    }
            //    else
            //    {
            //        foreach (XmlNode node in temp)
            //        {
            //            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
            //            {
            //                GroundOne.MC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
            //            {
            //                GroundOne.SC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
            //            {
            //                GroundOne.TC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //        }
            //    }
            //}

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
            Debug.Log("ExecLoad 4 " + DateTime.Now);

            Type type = GroundOne.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                    try { pi.SetValue(GroundOne.SC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                    try { pi.SetValue(GroundOne.TC, Convert.ToInt32(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try { pi.SetValue(GroundOne.MC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText, null); } catch {}
                    try { pi.SetValue(GroundOne.SC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText, null); } catch {}
                    try { pi.SetValue(GroundOne.TC, xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText, null); } catch {}
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try { pi.SetValue(GroundOne.MC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_MAINPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                    try { pi.SetValue(GroundOne.SC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_SECONDPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                    try { pi.SetValue(GroundOne.TC, Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/" + Database.NODE_THIRDPLAYERSTATUS + "/" + pi.Name).InnerText), null); } catch {}
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(MainCharacter.PlayerStance))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.MC, (MainCharacter.PlayerStance)Enum.Parse(typeof(MainCharacter.PlayerStance), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.SC, (MainCharacter.PlayerStance)Enum.Parse(typeof(MainCharacter.PlayerStance), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(GroundOne.TC, (MainCharacter.PlayerStance)Enum.Parse(typeof(MainCharacter.PlayerStance), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // e 後編追加
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

            XmlDocument xml2 = new XmlDocument();
            try
            {
                xml2.Load(pathForDocumentsFile(Database.WE2_FILE));
                Type typeWE2 = GroundOne.WE2.GetType();
                foreach (PropertyInfo pi in typeWE2.GetProperties())
                {
                    // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, Convert.ToInt32(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, (xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, Convert.ToBoolean(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                }
                Debug.Log("ExecLoad 9 " + DateTime.Now); ;

                XmlNodeList list1 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonOneInfo");
                XmlNodeList list2 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonTwoInfo");
                XmlNodeList list3 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonThreeInfo");
                XmlNodeList list4 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonFourInfo");
                XmlNodeList list5 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonFiveInfo");
                Debug.Log("ExecLoad 75: " + list1.Count.ToString() + " " + GroundOne.Truth_KnownTileInfo.Length.ToString());
                Debug.Log(DateTime.Now.ToString());
            }
            catch {}

            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                GroundOne.Truth_KnownTileInfo[ii] = Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/TruthDungeonOneInfo/truthTileOne" + ii.ToString()).InnerText, null);
            }
            for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            {
                GroundOne.Truth_KnownTileInfo2[ii] = Convert.ToBoolean(xml.DocumentElement.SelectSingleNode(@"/Body/TruthDungeonTwoInfo/truthTileTwo" + ii.ToString()).InnerText, null);
            }
            Debug.Log(DateTime.Now.ToString());
            Debug.Log("ExecLoad 8-1 " + DateTime.Now);

            // after (もう少し早くならないか？
            //for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            //{
            //    string temp = xml.GetElementsByTagName("truthTileTwo" + ii.ToString())[0].InnerText;
            //    GroundOne.Truth_KnownTileInfo2[ii] = Convert.ToBoolean(temp);
            //}
            //Debug.Log(DateTime.Now.ToString());
            //Debug.Log("ExecLoad 8-2");

            //for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            //{
            //    string temp = xml.GetElementsByTagName("truthTileThree" + ii.ToString())[0].InnerText;
            //    GroundOne.Truth_KnownTileInfo3[ii] = Convert.ToBoolean(temp);
            //}
            //Debug.Log(DateTime.Now.ToString());
            //Debug.Log("ExecLoad 8-3");

            //for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            //{
            //    string temp = xml.GetElementsByTagName("truthTileFour" + ii.ToString())[0].InnerText;
            //    GroundOne.Truth_KnownTileInfo4[ii] = Convert.ToBoolean(temp);
            //}
            //Debug.Log(DateTime.Now.ToString());
            //Debug.Log("ExecLoad 8-4");

            //for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
            //{
            //    string temp = xml.GetElementsByTagName("truthTileFive" + ii.ToString())[0].InnerText;
            //    GroundOne.Truth_KnownTileInfo5[ii] = Convert.ToBoolean(temp);
            //}
            //Debug.Log(DateTime.Now.ToString());
            //Debug.Log("ExecLoad 8-5"); 
            
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
            this.systemMessage.text = "";
        }

        public void tapExit()
        {
            Debug.Log("tapExit (S)");

            if (this.systemMessage.text == this.MESSAGE_1 || this.systemMessage.text == this.MESSAGE_2)
            {
                HideAllChild();
            }
            else if (this.nowAutoKill)
            {
                Debug.Log("tapExit auto kill: " + GroundOne.Parent.Count);
                if (GroundOne.Parent.Count > 0)
                {
                    GroundOne.Parent[GroundOne.Parent.Count - 1].NextScene();
                }
            }
            else
            {
                if (GroundOne.AfterBacktoTitle)
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
}

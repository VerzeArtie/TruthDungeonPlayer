using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class Method
    {
        public enum NewItemCategory
        {
            Battle,
            Lottery,
        }

        // panel(gameobject)の色をレアに応じて変更
        public static void UpdateRareColor(ItemBackPack item, Text target1, GameObject target2)
        {
            if (item == null)
            {
                target1.color = Color.white;
                target2.gameObject.GetComponent<Image>().color = Color.white;
                return;
            }

            switch (item.Rare)
            {
                case ItemBackPack.RareLevel.Poor:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = Color.gray;
                    break;
                case ItemBackPack.RareLevel.Common:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = new Color(0, 0.7f, 0);
                    break;
                case ItemBackPack.RareLevel.Rare:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.DarkBlue;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.Purple;
                    break;
                case ItemBackPack.RareLevel.Legendary: // 後編追加
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.Orangered;
                    break;
            }
        }

        // 通常セーブ、現実世界の自動セーブ、タイトルSeekerモードの自動セーブを結合
        public static void AutoSaveTruthWorldEnvironment()
        {
            XmlTextWriter xmlWriter2 = new XmlTextWriter(Database.WE2_FILE, Encoding.UTF8);
            try
            {
                xmlWriter2.WriteStartDocument();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteStartElement("Body");
                xmlWriter2.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter2.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter2.WriteStartElement("TruthWorldEnvironment");
                xmlWriter2.WriteWhitespace("\r\n");
                if (GroundOne.WE2 != null)
                {
                    Type typeWE2 = GroundOne.WE2.GetType();
                    foreach (PropertyInfo pi in typeWE2.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE2, null))).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter2.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE2, null)));
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE2, null)).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");
                xmlWriter2.WriteEndDocument();
            }
            finally
            {
                xmlWriter2.Close();
            }
        }
        // 現実世界の自動セーブ
        public static void AutoSaveRealWorld(MainCharacter MC, MainCharacter SC, MainCharacter TC, WorldEnvironment WE, bool[] knownTileInfo, bool[] knownTileInfo2, bool[] knownTileInfo3, bool[] knownTileInfo4, bool[] knownTileInfo5, bool[] Truth_KnownTileInfo, bool[] Truth_KnownTileInfo2, bool[] Truth_KnownTileInfo3, bool[] Truth_KnownTileInfo4, bool[] Truth_KnownTileInfo5)
        {
            // todo
        }


        // 戦闘終了後のアイテムゲット、ファージル宮殿お楽しみ抽選券のアイテムゲットを統合
        public static string GetNewItem(NewItemCategory category, MainCharacter mc, TruthEnemyCharacter ec1 = null, int dungeonArea = 0)
        {
            return ""; // todo
        }
    }
}

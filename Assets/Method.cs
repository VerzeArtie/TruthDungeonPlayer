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

        public static void UseItem(MainCharacter player, string itemName, int currentNumber, Text mainMessage)
        {
            ItemBackPack backpackData = new ItemBackPack(itemName);

            switch (backpackData.Name)
            {
                case Database.POOR_SMALL_RED_POTION:
                case Database.COMMON_NORMAL_RED_POTION:
                case Database.COMMON_LARGE_RED_POTION:
                case Database.COMMON_HUGE_RED_POTION:
                case Database.COMMON_GORGEOUS_RED_POTION:
                case Database.RARE_PERFECT_RED_POTION:
                case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                    int effect = backpackData.UseIt();
                    if (player.CurrentNourishSense > 0)
                    {
                        effect = (int)((double)effect * 1.3f);
                    }
                    player.CurrentLife += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    break;
            }
        }

        public static MainCharacter GetCurrentPlayer(Color baseColor)
        {
            MainCharacter player = null;
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.MC;
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.SC;
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == baseColor)
            {
                player = GroundOne.TC;
            }
            else
            {
                if (GroundOne.MC == null) { Debug.Log("fatal sequence..."); }
            }
            return player;
        }

        public static int GetTileNumber(Vector3 pos)
        {
            Vector3 adjustPos = new Vector3(pos.x, pos.y, pos.z);
            int number = (int)(adjustPos.x % Database.TRUTH_DUNGEON_COLUMN + (-adjustPos.y) * Database.TRUTH_DUNGEON_COLUMN);
            int row = number / Database.TRUTH_DUNGEON_COLUMN;
            int column = number % Database.TRUTH_DUNGEON_COLUMN;
            return number;
        }

        public static void GetItemFullCheck(MotherForm scene, MainCharacter player, string itemName)
        {
            bool result = player.AddBackPack(new ItemBackPack(itemName));
            if (result) return;

            string cannotTrash = string.Empty;
            if (itemName == Database.RARE_EARRING_OF_LANA ||
                itemName == Database.RARE_TOOMI_BLUE_SUISYOU)
            {
                cannotTrash = itemName;
            }
            SceneDimension.CallTruthStatusPlayer(scene, true, cannotTrash);
        }

        public static void UpdateBackPackLabel(MainCharacter target, GameObject[] back_Backpack, Text[] backpack, Text[] backpackStack, Image[] backpackIcon)
        {
            ItemBackPack[] backpackData = target.GetBackPackInfo();
            for (int currentNumber = 0; currentNumber < backpackData.Length; currentNumber++)
            {
                if (backpackData[currentNumber] == null)
                {
                    if (currentNumber < backpack.Length ) { backpack[currentNumber].text = ""; }
                    if (currentNumber < backpackStack.Length) { backpackStack[currentNumber].text = ""; }
                    if (currentNumber < backpackIcon.Length) { backpackIcon[currentNumber].sprite = null; }
                    if (currentNumber < backpack.Length) { Method.UpdateRareColor(null, backpack[currentNumber], back_Backpack[currentNumber]); }
                    //back_Backpack[currentNumber].SetActive(false);
                }
                else
                {
                    back_Backpack[currentNumber].SetActive(true);
                    backpack[currentNumber].text = backpackData[currentNumber].Name;
                    Method.UpdateRareColor(backpackData[currentNumber], backpack[currentNumber], back_Backpack[currentNumber]);
                    backpackStack[currentNumber].text = "x" + backpackData[currentNumber].StackValue.ToString();
                    if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Weapon");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_TwoHand)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("TwoHand");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Light)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Knuckle");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Rod)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Rod");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Shield)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Shield");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Heavy) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Middle))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Armor");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Light))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("LightArmor");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Accessory)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Accessory");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Equip) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Food) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Potion))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Material1");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Potion)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Potion");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Any)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Useless");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_BlueOrb)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("BlueOrb");
                    }
                    else
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Useless");
                    }
                }
            }
        }

        // 親グループに空のオブジェクトを追加する(レイアウト調整専用)
        public static void AddEmptyObj(ref GameObject parentGroup, int number)
        {
            for (int ii = 0; ii < number; ii++)
            {
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(parentGroup.transform);
            }
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
                    target2.gameObject.GetComponent<Image>().color = UnityColor.CommonGreen;
                    break;
                case ItemBackPack.RareLevel.Rare:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.DarkBlue;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.EpicPurple;
                    break;
                case ItemBackPack.RareLevel.Legendary: // 後編追加
                    target1.color = Color.white;
                    target2.gameObject.GetComponent<Image>().color = UnityColor.OrangeRed;
                    break;
            }
        }

        // ソーサリー／ノーマル／インスタントのミニアイコンをセットする
        public static void SetupActionButton(GameObject actionButton, Image sorceryMark, string actionCommand)
        {
            if (actionCommand != null && actionCommand != "" && actionCommand != string.Empty)
            {
                Debug.Log("not equal empty : " + actionCommand);

                actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(actionCommand);
                actionButton.name = actionCommand;
                if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Sorcery)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.SorceryIcon);
                }
                else if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Normal)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
                }
                else if (TruthActionCommand.GetTimingType(actionCommand) == TruthActionCommand.TimingType.Instant)
                {
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.InstantIcon);
                }
                else
                {
                    actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.STAY_EN);
                    actionButton.name = Database.STAY_EN;
                    sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
                }
            }
            else
            {
                Debug.Log("empty then sprite null");
                actionButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(Database.STAY_EN);
                actionButton.name = Database.STAY_EN;
                sorceryMark.sprite = Resources.Load<Sprite>(Database.NormalIcon);
            }
        }

        // GroundOne.WE2をリロード
        private static string pathForRootFile(string filename)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return filename;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return filename;
            }
            else
            {
                return filename;
            }
        }
        public static void ReloadTruthWorldEnvironment()
        {
            XmlDocument xml2 = new XmlDocument();
            xml2.Load(pathForRootFile(Database.WE2_FILE));
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
        public static void AutoSaveRealWorld()
        {
            SceneDimension.CallSaveLoadWithSaveOnly();
        }
        public static void AutoSaveRealWorld(MainCharacter MC, MainCharacter SC, MainCharacter TC, WorldEnvironment WE, bool[] knownTileInfo, bool[] knownTileInfo2, bool[] knownTileInfo3, bool[] knownTileInfo4, bool[] knownTileInfo5, bool[] Truth_KnownTileInfo, bool[] Truth_KnownTileInfo2, bool[] Truth_KnownTileInfo3, bool[] Truth_KnownTileInfo4, bool[] Truth_KnownTileInfo5)
        {
            SceneDimension.CallSaveLoadWithSaveOnly();
        }

        // 街でオル・ランディスが外れる、４階最初でヴェルゼが外れる、４階エリア３でラナが外れるのを統合
        public static void RemoveParty(MainCharacter player, bool initializeBank)
        {
            if (GroundOne.WE.AvailableThirdCharacter)
            {
                GroundOne.WE.AvailableThirdCharacter = false;
            }
            else if (GroundOne.WE.AvailableSecondCharacter)
            {
                GroundOne.WE.AvailableSecondCharacter = false;
            }

            string[] itemBank = new string[Database.MAX_ITEM_BANK];
            int[] itemBankStack = new int[Database.MAX_ITEM_BANK];
            int current = 0;

            if (initializeBank)
            {
                GroundOne.WE.InitializeItemBankData();
            }

            string[] beforeItem = new string[Database.MAX_ITEM_BANK];
            int[] beforeStack = new int[Database.MAX_ITEM_BANK];
            GroundOne.WE.LoadItemBankData(ref beforeItem, ref beforeStack);
            for (int ii = 0; ii < beforeItem.Length; ii++)
            {
                if (beforeItem[ii] == String.Empty || beforeItem[ii] == "" || beforeItem[ii] == null)
                {
                    // 空っぽの場合、何も追加しない。
                }
                else
                {
                    itemBank[current] = beforeItem[ii];
                    itemBankStack[current] = beforeStack[ii];
                    current++;
                }
            }

            if (player.MainWeapon != null)
            {
                if ((player.MainWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                    (player.MainWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.SubWeapon != null)
            {
                if ((player.SubWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                    (player.SubWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.SubWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.SubWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.MainArmor != null)
            {
                if ((player.MainArmor.Name != Database.COMMON_AURA_ARMOR) &&
                    (player.MainArmor.Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainArmor.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory != null)
            {
                if ((player.Accessory.Name != Database.COMMON_FATE_RING) &&
                    (player.Accessory.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory2 != null)
            {
                if ((player.Accessory2.Name != Database.COMMON_FATE_RING) &&
                    (player.Accessory2.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory2.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory2.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            ItemBackPack[] backpackInfo = player.GetBackPackInfo();
            for (int ii = 0; ii < backpackInfo.Length; ii++)
            {
                if (backpackInfo[ii] != null)
                {
                    if ((backpackInfo[ii].Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                        (backpackInfo[ii].Name != Database.COMMON_AURA_ARMOR) &&
                        (backpackInfo[ii].Name != Database.COMMON_FATE_RING) &&
                        (backpackInfo[ii].Name != Database.COMMON_LOYAL_RING) &&
                        (backpackInfo[ii].Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA))
                    {
                        itemBank[current] = backpackInfo[ii].Name;
                        itemBankStack[current] = backpackInfo[ii].StackValue;
                        current++;
                    }
                }
            }
            GroundOne.WE.UpdateItemBankData(itemBank, itemBankStack);
        }

        // 戦闘終了後のアイテムゲット、ファージル宮殿お楽しみ抽選券のアイテムゲットを統合
        public static string GetNewItem(NewItemCategory category, MainCharacter mc, TruthEnemyCharacter ec1 = null, int dungeonArea = 0)
        {
            string targetItemName = String.Empty;
            int debugCounter1 = 0;
            int debugCounter2 = 0;
            int debugCounter3 = 0;
            int debugCounter4 = 0;
            int debugCounter5 = 0;
            int debugCounter6 = 0;
            int debugCounter7 = 0;

            int debugCounter8 = 0;

            for (int zzz = 0; zzz < 1; zzz++)
            {
                System.Threading.Thread.Sleep(1);

                // ドロップアイテムを出現させる
                System.Random rd = new System.Random(Environment.TickCount * DateTime.Now.Millisecond);
                int param1 = 1000; // 素材
                int param2 = 600; // 武具POOR
                int param3 = 350; // 武具COMMON
                int param4 = 50; // 武具RARE
                int param5 = 20; // パラメタUP
                int param6 = 10; // EPIC
                int param7 = 200; // ハズレ

                // param1 は固定でいくこと
                // param2 + param3 + param4 は1000とすること
                // param7はBlack以外は0とすること
                if (ec1 != null)
                {
                    switch (ec1.Rare)
                    {
                        case TruthEnemyCharacter.RareString.Black:
                            param1 = 1000;
                            param2 = 600;
                            param3 = 350;
                            param4 = 50;
                            param5 = 20;
                            param6 = 10 + GroundOne.WE2.KillingEnemy; // EPICを少し出しやすくする味付け
                            param7 = 200;
                            break;
                        case TruthEnemyCharacter.RareString.Blue:
                            param1 = 1000;
                            param2 = 100;
                            param3 = 700;
                            param4 = 200;
                            param5 = 60;
                            param6 = 20 + GroundOne.WE2.KillingEnemy * 3; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Red:
                            param1 = 1000;
                            param2 = 0;
                            param3 = 500;
                            param4 = 500;
                            param5 = 120;
                            param6 = 40 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Gold: // 階層ボスは固定ドロップアイテムだが、通常ボスはランダム扱い
                            param1 = 0; // ボスレベルで素材は無い事とする。
                            param2 = 0;
                            param3 = 600;
                            param4 = 1200;
                            param5 = 400;
                            param6 = 80 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Purple: // 支配竜は固定ドロップアイテム
                            break;
                    }
                }
                else if (category == NewItemCategory.Lottery)
                {
                    param1 = 0; // 抽選券、モンスター素材ではない。
                    param2 = 0; // 抽選券、POORは無しとする
                    param3 = 600;
                    param4 = 240;
                    param5 = 100;
                    param6 = 7;
                    param7 = 0; // 抽選券、ハズレは無しとする
                    debugCounter8++;
                }

                int randomValue = rd.Next(1, param1 + param2 + param3 + param4 + param5 + param6 + param7 + 1);
                int randomValue2 = rd.Next(1, 1 + param1 + param2 + param3 + param4);
                int randomValue21 = rd.Next(1, 19);
                int randomValue22 = rd.Next(1, 11);
                int randomValue3 = rd.Next(1, 17);
                int randomValue32 = rd.Next(1, 26);
                int randomValue4 = rd.Next(1, 6);
                int randomValue42 = rd.Next(1, 9);
                int randomValue5 = rd.Next(1, 6);
                int randomValue6 = rd.Next(1, 3);
                int randomValue7 = rd.Next(1, 101);

                #region "エリア毎のアイテム総数に応じた値を設定"
                // 1階は上述宣言時の値そのもの
                if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                    (category == NewItemCategory.Lottery && dungeonArea == 1))
                {
                    randomValue21 = rd.Next(1, 19);
                    randomValue22 = rd.Next(1, 11);
                    randomValue3 = rd.Next(1, 17);
                    randomValue32 = rd.Next(1, 26);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 9);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 3);
                    randomValue7 = rd.Next(1, 101);
                }
                // 2階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 2))
                {
                    randomValue21 = rd.Next(1, 10);
                    randomValue22 = rd.Next(1, 10);
                    randomValue3 = rd.Next(1, 18);
                    randomValue32 = rd.Next(1, 28);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 16);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 3階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 3))
                {
                    randomValue21 = rd.Next(1, 5);
                    randomValue22 = rd.Next(1, 5);
                    randomValue3 = rd.Next(1, 14);
                    randomValue32 = rd.Next(1, 32);
                    randomValue4 = rd.Next(1, 7);
                    randomValue42 = rd.Next(1, 24);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 4階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 4))
                {
                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 23);
                    randomValue32 = rd.Next(1, 29);
                    randomValue4 = rd.Next(1, 11);
                    randomValue42 = rd.Next(1, 27);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = rd.Next(1, 101);
                }
                // 現実世界４層ラスト
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51))
                {
                    param1 = 0;
                    param2 = 0;

                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 16);
                    randomValue32 = 0;
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = 0;
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = 0;
                }
                #endregion

                #region "モンスター毎の素材ドロップ"
                if (1 <= randomValue && randomValue <= param1) // 44.84 %
                {
                    int DropItemNumber = 0;
                    for (int ii = 0; ii < ec1.DropItem.Length; ii++)
                    {
                        if (ec1.DropItem[ii] != String.Empty)
                        {
                            DropItemNumber++;
                        }
                    }
                    if (DropItemNumber <= 0) // 素材登録が無い場合、ハズレ
                    {
                        targetItemName = String.Empty;
                    }
                    else
                    {
                        int randomValue1 = AP.Math.RandomInteger(DropItemNumber);
                        targetItemName = ec1.DropItem[randomValue1];
                    }

                    debugCounter1++;
                }
                #endregion
                #region "ダンジョンエリア毎の汎用装備品"
                else if (param1 < randomValue && randomValue <= (param1 + param2 + param3 + param4)) // 44.84%
                {
                    if (1 <= randomValue2 && randomValue2 <= param2) // Poor 60.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HINJAKU_ARMRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_USUYOGORETA_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_NON_BRIGHT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_KUKEI_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SUTERARESHI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_ARIFURETA_STATUE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_NON_ADJUST_BELT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIMPLE_EARRING;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_KATAKUZURESHITA_FINGERRING;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_IROASETA_CHOKER;
                                    break;
                                case 11:
                                    targetItemName = Database.POOR_YOREYORE_MANTLE;
                                    break;
                                case 12:
                                    targetItemName = Database.POOR_NON_HINSEI_CROWN;
                                    break;
                                case 13:
                                    targetItemName = Database.POOR_TUKAIFURUSARETA_SWORD;
                                    break;
                                case 14:
                                    targetItemName = Database.POOR_TUKAINIKUI_LONGSWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.POOR_GATAGAKITERU_ARMOR;
                                    break;
                                case 16:
                                    targetItemName = Database.POOR_FESTERING_ARMOR;
                                    break;
                                case 17:
                                    targetItemName = Database.POOR_HINSO_SHIELD;
                                    break;
                                case 18:
                                    targetItemName = Database.POOR_MUDANIOOKII_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_OLD_USELESS_ROD;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_KISSAKI_MARUI_TUME;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BATTLE_HUMUKI_BUTOUGI;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SIZE_AWANAI_ROBE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NO_CONCEPT_RING;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_HIGHCOLOR_MANTLE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_EIGHT_PENDANT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_GOJASU_BELT;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EGARA_HUMEI_EMBLEM;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_HAYATOTIRI_ORB;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HUANTEI_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_DEPRESS_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_DAMAGED_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SHIMETSUKE_BELT;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NOGENKEI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_MAGICLIGHT_FIRE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_MAGICLIGHT_ICE;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_MAGICLIGHT_SHADOW;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_MAGICLIGHT_LIGHT;
                                    break;

                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_CURSE_EARRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_CURSE_BOOTS;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BLOODY_STATUE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_FALLEN_MANTLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SIHAIRYU_SIKOTU;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_OLD_TREE_KAREHA;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_GALEWIND_KONSEKI;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIN_CRYSTAL_KAKERA;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EVERMIND_ZANSHI;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_DIRTY_ANGEL_CONTRACT;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_FROZEN;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_PARALYZE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_STUN;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_MIGAWARI_DOOL;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_ONE_DROPLET_KESSYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_MOMENTARY_FLASH_LIGHT;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SUN_YUME_KAKERA;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            targetItemName = String.Empty;
                        }
                        else if (ec1.Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                            ec1.Area == TruthEnemyCharacter.MonsterArea.Area44)
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        debugCounter2++;
                    }
                    // ダンジョンエリア毎のコモン汎用装備品
                    else if (param2 < randomValue2 && randomValue2 <= (param2 + param3)) // Common 35.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_PENDANT;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_PENDANT;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_PENDANT;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_PENDANT;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_PENDANT;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SISSO_ARMRING;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_FINE_FEATHER;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_KIREINA_ORB;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_FIT_BANGLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PRISM_EMBLEM;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_FINE_SWORD;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_TWEI_SWORD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_FINE_ARMOR;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GOTHIC_PLATE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_FINE_SHIELD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_GRIPPING_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_COPPER_RING_TORA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_COPPER_RING_IRUKA;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_COPPER_RING_UMA;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMA;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_COPPER_RING_HAYABUSA;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_COPPER_RING_TAKO;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_COPPER_RING_USAGI;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMO;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_COPPER_RING_SHIKA;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_COPPER_RING_ZOU;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_AMULET;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_AMULET;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_AMULET;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_AMULET;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_AMULET;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SHARP_CLAW;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_LIGHT_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_WOOD_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_SHORT_SWORD;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_BASTARD_SWORD;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_LETHER_CLOTHING;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_COTTON_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_COPPER_ARMOR;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_HEAVY_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_IRON_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CHARM;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CHARM;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CHARM;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CHARM;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CHARM;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_THREE_COLOR_COMPASS;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SANGO_CROWN;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SMOOTHER_BOOTS;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SHIOKAZE_MANTLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SMART_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SMART_CLAW;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_SMART_ROD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_SMART_SHIELD;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_RAUGE_SWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_SMART_CLOTHING;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SMART_ROBE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_SMART_PLATE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_BRONZE_RING_KIBA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BRONZE_RING_SASU;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_BRONZE_RING_KU;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_BRONZE_RING_NAGURI;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_BRONZE_RING_TOBI;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_BRONZE_RING_KARAMU;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_BRONZE_RING_HANERU;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BRONZE_RING_TORU;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_BRONZE_RING_MIRU;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_BRONZE_RING_KATAI;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_KOKUIN;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_KOKUIN;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_KOKUIN;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_KOKUIN;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_KOKUIN;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SISSEI_MANTLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_KAISEI_EMBLEM;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SAZANAMI_EARRING;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_AMEODORI_STATUE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_SMASH_BLADE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_POWERED_BUSTER;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_STONE_CLAW;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_DENDOU_ROD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_SERPENT_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SWIFT_CROSS;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_CHIFFON_ROBE;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_PURE_BRONZE_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_STONE;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_STONE;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_STONE;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_STONE;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_STONE;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_EXCELLENT_SWORD;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_EXCELLENT_KNUCKLE;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_EXCELLENT_ROD;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_EXCELLENT_BUSTER;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_EXCELLENT_ARMOR;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_EXCELLENT_CROSS;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_EXCELLENT_ROBE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_EXCELLENT_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_STEEL_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_STEEL_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_STEEL_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_STEEL_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_STEEL_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_STEEL_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_STEEL_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_STEEL_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_STEEL_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_STEEL_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_MASEKI;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_MASEKI;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_MASEKI;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_MASEKI;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_MASEKI;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_DESCENED_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_FALSET_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SEKIGAN_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_ROCK_BUSTER;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_COLD_STEEL_PLATE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_AIR_HARE_CROSS;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_FLOATING_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_SNOW_CRYSTAL_SHIELD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_WING_STEP_FEATHER;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SNOW_FAIRY_BREATH;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_STASIS_RING;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_SIHAIRYU_KIBA;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_OLD_TREE_JUSHI;
                                    break;
                                case 29:
                                    targetItemName = Database.COMMON_GALEWIND_KIZUATO;
                                    break;
                                case 30:
                                    targetItemName = Database.COMMON_SIN_CRYSTAL_QUATZ;
                                    break;
                                case 31:
                                    targetItemName = Database.COMMON_EVERMIND_OMEN;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_MEDALLION;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_MEDALLION;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_MEDALLION;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_MEDALLION;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_MEDALLION;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SOCIETY_SYMBOL;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_FEATHER_BRACELET;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BIRD_SONG_LUTE;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_MAZE_CUBE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_LIGHT_SERVANT;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SHADOW_SERVANT;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_ROYAL_GUARD_RING;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_ELEMENTAL_GUARD_RING;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_HAYATE_GUARD_RING;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_MASTER_SWORD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MASTER_KNUCKLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_MASTER_ROD;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_MASTER_AXE;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_MASTER_ARMOR;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_MASTER_CROSS;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_MASTER_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_MASTER_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_SILVER_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_SILVER_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_SILVER_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_SILVER_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_SILVER_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SILVER_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SILVER_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SILVER_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SILVER_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_FLOAT_STONE;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_FLOAT_STONE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_FLOAT_STONE;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_FLOAT_STONE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_FLOAT_STONE;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MUKEI_SAKAZUKI;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_RAINBOW_TUBE;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_ELDER_PERSPECTIVE_GRASS;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_DEVIL_SEALED_VASE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_FLOATING_WHITE_BALL;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_INITIATE_SWORD;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_BULLET_KNUCKLE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_KENTOUSI_SWORD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_ELECTRO_ROD;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_FORTIFY_SCALE;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_MURYOU_CROSS;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_COLORLESS_ROBE;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_LOGISTIC_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CRYSTAL;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CRYSTAL;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CRYSTAL;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CRYSTAL;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CRYSTAL;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_PLATINUM_RING_1;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_PLATINUM_RING_2;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_PLATINUM_RING_3;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_PLATINUM_RING_4;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PLATINUM_RING_5;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_PLATINUM_RING_6;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_PLATINUM_RING_7;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PLATINUM_RING_8;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_PLATINUM_RING_9;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_PLATINUM_RING_10;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter3++;
                    }
                    // ダンジョンエリア毎のレア汎用装備品
                    else if ((param2 + param3) < randomValue2 && randomValue2 <= (param2 + param3 + param4)) // Rare 5.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_JOUSITU_BLUE_POWERRING;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_KOUJOUSINYADORU_RED_ORB;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_MAGICIANS_MANTLE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BEATRUSH_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_AERO_BLADE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SINTYUU_RING_KUROHEBI;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SINTYUU_RING_HAKUTYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_SINTYUU_RING_AKAHYOU;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_ICE_SWORD;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RISING_KNUCKLE;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_AUTUMN_ROD;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SUN_BRAVE_ARMOR;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_ESMERALDA_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_WRATH_SERVEL_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_BLUE_LIGHTNING;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DIRGE_ROBE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_DUNSID_PLATE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_BURNING_CLAYMORE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_KONSHIN;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SYUNSOKU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_JUKURYO;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SOUGEN;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_YUUDAI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_MEIUN_BOX;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_WILL_HOLY_HAT;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EMBLEM_BLUESTAR;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SEAL_OF_DEATH;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DARKNESS_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_BLUE_RED_ROD;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SHARKSKIN_ARMOR;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_RED_THUNDER_ROBE;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_BLACK_MAGICIAN_CROSS;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_BLUE_SKY_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_MENTALIZED_FORCE_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_ADERKER_FALSE_ROD;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_BLACK_ICE_SWORD;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_CLAYMORE_ZUKS;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_DRAGONSCALE_ARMOR;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_LIGHT_BLIZZARD_ROBE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_FROZEN_LAVA;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_WHITE_TIGER_ANGEL_GOHU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_SOLID;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_VAPOR;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_ERASTIC;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_TORAREITION;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SYUURENSYA_KUROOBI;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_SHIHANDAI_KUROOBI;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SYUUDOUSOU_KUROOBI;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_KUGYOUSYA_KUROOBI;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_TEARS_END;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SKY_COLD_BOOTS;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_EARTH_BREAKERS_SIGIL;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_AERIAL_VORTEX;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_LIVING_GROWTH_SEED;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_SHARPNEL_SPIN_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_BLUE_LIGHT_MOON_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_BLIZZARD_SNOW_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHAERING_BONE_CRUSHER;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_SCALE_BLUERAGE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_BLUE_REFLECT_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_SLIDE_THROUGH_SHIELD;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_ELEMENTAL_STAR_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SPELL_COMPASS;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SHADOW_BIBLE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DETACHMENT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BLIND_NEEDLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_CORE_ESSENCE_CHANNEL;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_ASTRAL_VOID_BLADE;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_VERDANT_SONIC_CLAW;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_PRISONER_BREAKING_AXE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_INVISIBLE_STATE_ROD;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DOMINATION_BRAVE_ARMOR;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SEAL_OF_ASSASSINATION;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_EMBLEM_OF_VALKYRIE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_EMBLEM_OF_HADES;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_SIHAIRYU_KATAUDE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_OLD_TREE_SINKI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_GALEWIND_IBUKI;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SIN_CRYSTAL_SOLID;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EVERMIND_SENSE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_DEVIL_SUMMONER_TOME;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_ANGEL_CONTRACT;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_ARCHANGEL_CONTRACT;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_DARKNESS_COIN;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_SOUSUI_HIDENSYO;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_MEEK_HIDENSYO;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_JUKUTATUSYA_HIDENSYO;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_KYUUDOUSYA_HIDENSYO;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_DANZAI_ANGEL_GOHU;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_ETHREAL_EDGE_SABRE;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHINGETUEN_CLAW;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_BLOODY_DIRTY_SCYTHE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_ALL_ELEMENTAL_ROD;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_BLOOD_BLAZER_CROSS;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_DARK_ANGEL_ROBE;
                                    break;
                                case 24:
                                    targetItemName = Database.RARE_MAJEST_HAZZARD_SHIELD;
                                    break;
                                case 25:
                                    targetItemName = Database.RARE_WHITE_DIAMOND_SHIELD;
                                    break;
                                case 26:
                                    targetItemName = Database.RARE_VAPOR_SOLID_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID5_MIND;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter4++;
                    }
                }
                #endregion
                #region "ダンジョン階層依存のパワーアップアイテム"
                else if ((param1 + param2 + param3 + param4) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5)) // Rare Use Item 0.90%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID2_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID3_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID4_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID5_MIND;
                                break;
                        }
                    }
                    #endregion
                    debugCounter5++;
                }
                #endregion
                #region "ダンジョン階層依存の高級装備品"
                else if ((param1 + param2 + param3 + param4 + param5) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6)) // EPIC 0.45%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 10)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_RING_OF_OSCURETE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MERGIZD_SOL_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 27)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID2_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_GARVANDI_ADILORB;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MAXCARN_X_BUSTER;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_JUZA_ARESTINE_SLICER;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 45)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID3_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_SHEZL_MYSTIC_FORTUNE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_MERGIZD_DAV_AGITATED_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 55)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID4_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                    break;
                                case 4:
                                    targetItemName = Database.EPIC_JUZA_THE_PHANTASMAL_CLAW;
                                    break;
                                case 5:
                                    targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        // 低レベル制限はかけない。
                        switch (randomValue6)
                        {
                            case 1:
                                targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                break;
                            case 2:
                                targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                break;
                            case 3:
                                targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                break;
                            case 4:
                                targetItemName = Database.EPIC_JUZA_THE_PHANTASMAL_CLAW;
                                break;
                            case 5:
                                targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                break;
                        }
                    }
                    #endregion
                    debugCounter6++;
                }
                #endregion
                #region "ハズレ"
                else if ((param1 + param2 + param3 + param4 + param5 + param6) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6 + param7)) // ハズレ 8.97 %
                {
                    targetItemName = String.Empty;
                    debugCounter7++;
                }
                else // 万が一規定外の値はハズレ
                {
                    targetItemName = String.Empty;
                }
                #endregion

                #region "ハズレは、不用品をランダムドロップ"
                if (targetItemName == string.Empty)
                {
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL2;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL3;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL4;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL5;
                        }
                    }
                }
                #endregion
            }

            //MessageBox.Show(debugCounter1.ToString() + "(" + Convert.ToString((double)(((double)debugCounter1 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter2.ToString() + "(" + Convert.ToString((double)(((double)debugCounter2 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter3.ToString() + "(" + Convert.ToString((double)(((double)debugCounter3 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter4.ToString() + "(" + Convert.ToString((double)(((double)debugCounter4 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter5.ToString() + "(" + Convert.ToString((double)(((double)debugCounter5 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter6.ToString() + "(" + Convert.ToString((double)(((double)debugCounter6 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter7.ToString() + "(" + Convert.ToString((double)(((double)debugCounter7 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter8.ToString() + "\r\n");

            #region "ボス撃破、固定ドロップアイテム"
            if ((category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_LEVIATHAN) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_HOWLING_SEIZER) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.FirstName == Database.ENEMY_BOSS_LEGIN_ARZE_1))
            {
                targetItemName = ec1.DropItem[0];
            }
            #endregion

            return targetItemName;
        }
    }
}

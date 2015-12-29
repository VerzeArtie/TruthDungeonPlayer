using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class Method
    {
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
            // todo
        }

        // 現実世界の自動セーブ
        public static void AutoSaveRealWorld(MainCharacter MC, MainCharacter SC, MainCharacter TC, WorldEnvironment WE, bool[] knownTileInfo, bool[] knownTileInfo2, bool[] knownTileInfo3, bool[] knownTileInfo4, bool[] knownTileInfo5, bool[] Truth_KnownTileInfo, bool[] Truth_KnownTileInfo2, bool[] Truth_KnownTileInfo3, bool[] Truth_KnownTileInfo4, bool[] Truth_KnownTileInfo5)
        {
            // todo
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DungeonPlayer
{
    /// <summary>
    /// シングルモードで使用する記憶クラス
    /// </summary>
    public class SingleWorldEnvironment : MonoBehaviour
    {
        public int Gold { get; set; }
        public int ObsidianStone { get; set; }
        public int SoulEssence { get; set; }

        public string P1_Name { get; set; }
        public int P1_Level { get; set; }
        public int P1_Strength { get; set; }
        public int P1_Agility { get; set; }
        public int P1_Intelligence { get; set; }
        public int P1_Stamina { get; set; }
        public int P1_Mind { get; set; }
        public string P1_MainWeapon { get; set; }
        public string P1_SubWeapon { get; set; }
        public string P1_Armor { get; set; }
        public string P1_Accessory1 { get; set; }
        public string P1_Accessory2 { get; set; }

        public bool CompleteSingleA_1_1 { get; set; }
        public bool CompleteSingleA_1_2 { get; set; }
        public bool CompleteSingleA_1_3 { get; set; }
        public bool CompleteSingleA_1_4 { get; set; }
        public bool CompleteSingleA_1_5 { get; set; }
        public bool CompleteSingleB_1_1 { get; set; }
        public bool CompleteSingleB_1_2 { get; set; }
        public bool CompleteSingleB_1_3 { get; set; }
        public bool CompleteSingleB_1_4 { get; set; }
        public bool CompleteSingleB_1_5 { get; set; }
        public bool CompleteSingleC_1_1 { get; set; }
        public bool CompleteSingleC_1_2 { get; set; }
        public bool CompleteSingleC_1_3 { get; set; }
        public bool CompleteSingleC_1_4 { get; set; }
        public bool CompleteSingleC_1_5 { get; set; }
        public bool CompleteSingleD_1_1 { get; set; }
        public bool CompleteSingleD_1_2 { get; set; }
        public bool CompleteSingleD_1_3 { get; set; }
        public bool CompleteSingleD_1_4 { get; set; }
        public bool CompleteSingleD_1_5 { get; set; }
        public bool CompleteSingleE_1_1 { get; set; }
        public bool CompleteSingleE_1_2 { get; set; }
        public bool CompleteSingleE_1_3 { get; set; }
        public bool CompleteSingleE_1_4 { get; set; }
        public bool CompleteSingleE_1_5 { get; set; }

    }
}

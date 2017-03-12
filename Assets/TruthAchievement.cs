using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class TruthAchievement : MotherForm
    {
        public GameObject[] objList;
        public Text[] txtList;
        void Start()
        {
            for (int ii = 0; ii < objList.Length; ii++)
            {
                if (objList[ii] != null)
                {
                    objList[ii].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
                }
                if (txtList[ii] != null)
                {
                    txtList[ii].color = new Color(0, 0, 0, 0.8f);
                }
            }
            // (0, 81, 255)
            // (255, 50, 50)
            // (0, 146, 255)
            // (107, 17, 133)
        }
        void Update()
        {
        }
    }
}
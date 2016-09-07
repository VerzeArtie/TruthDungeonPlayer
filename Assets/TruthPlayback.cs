﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DungeonPlayer
{
    public partial class TruthPlayback : MotherForm
    {
        public GameObject node;
        public GameObject content;

        public override void Start()
        {
            base.Start();

            RectTransform rect = content.GetComponent<RectTransform>();
            const int HEIGHT = 110;
            const int MARGIN = 10;
            for (int ii = 0; ii < 50; ii++)
            {
                GameObject item = GameObject.Instantiate(node);
                item.transform.SetParent(content.transform, false);
                item.GetComponentInChildren<Text>().text = "debug " + ii.ToString();
                item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y - HEIGHT * (ii + 1), item.transform.localPosition.z);

                // 個数に応じて、コンテンツ長さを延長する。
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + HEIGHT);
            }
            // 最後に余白を追加しておく。
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + MARGIN);
        }

        public override void Update()
        {
            base.Update();
        }

        public void tapClose()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_PLAYBACK_CLOSE, string.Empty, string.Empty);

            SceneDimension.Back(this);
        }
    }	
}

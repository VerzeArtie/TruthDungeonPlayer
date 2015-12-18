using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public class TruthImage : Image
    {
        public enum buffType
        {
            None,
            Small,
            Large,
        }
        public enum CumulativeTextAlign
        {
            TopRight,
            Center,
        }

        protected buffType buffMode = buffType.None;
        public buffType BuffMode
        {
            get { return buffMode; }
            set
            {
                buffMode = value;
                if (value == buffType.Large)
                {
                    //                this.fixedRect = new Rectangle(0, 0, 50, 50);
                    //                this.fixedPointCount = new PointF(4, 47);
                }
            }
        }

        protected string imageName = string.Empty;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        protected int count = 0;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        protected int cumulative = 0;
        public int Cumulative
        {
            get { return cumulative; }
            set { if (value >= 0) { cumulative = value; } }
        }
        protected CumulativeTextAlign align = CumulativeTextAlign.TopRight;
        public CumulativeTextAlign CumulativeAlign
        {
            get { return align; }
            set { align = value; }
        }

        public void OnMouseEnterImage()
        {

        }

        private Text txtCounter;
        void Start()
        {
            GameObject baseObj = new GameObject("object");

            txtCounter = baseObj.AddComponent<Text>();

            txtCounter.name = "counter";
            //counter.BuffMode = TruthImage.buffType.Small;
            txtCounter.rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
            txtCounter.rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
            txtCounter.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            //counter.rectTransform.localPosition = new Vector2(0, 0);
            //counter.rectTransform.position = new Vector2(0, 0);
            txtCounter.alignment = TextAnchor.UpperCenter;
            txtCounter.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            txtCounter.color = Color.blue;
            txtCounter.text = this.count.ToString();
            txtCounter.gameObject.SetActive(true);
            txtCounter.transform.SetParent(this.transform, false);
            txtCounter.rectTransform.anchoredPosition = new Vector2(0, -30);
        }
        void Update()
        {
            if (0 < this.count && this.count <= 99)
            {
                txtCounter.text = this.count.ToString();
            }
            else
            {
                txtCounter.text = "";
            }

            if (cumulative > 0)
            {
                //                if (this.align == CumulativeTextAlign.TopRight)
                //                {
                //
                //                    if (cumulative >= 10)
                //                    {
                //                        e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative_2);
                //                    }
                //                    else
                //                    {
                //                        e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative);
                //                    }
                //                }
                //                else
                //                {
                //                    e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulativeCenter);
                //                }

            }

        }
        // todo
        //    Rectangle fixedRect = new Rectangle(0, 0, 25, 25);
        //    PointF fixedPointCount = new PointF(4, 22);
        //    PointF fixedPointCumulative = new PointF(12, -3);
        //    PointF fixedPointCumulative_2 = new PointF(4, -3);
        //    PointF fixedPointCumulativeCenter = new PointF(6, 4);
        //    Font fixedFontCount = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Point, (byte)(128));
        //    Font fixedFontCumulative = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte)(128));
        //    protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        //    {
        //        if (buffMode == buffType.None)
        //        {
        //            base.OnPaint(pe);
        //        }
        //        else
        //        {
        //            System.Drawing.Graphics e = pe.Graphics;
        //            if (this.Image != null)
        //            {
        //                e.DrawImage(this.Image, this.fixedRect);
        //            }
        //
        //            if (0 < count && count <= 99)
        //            {
        //                e.DrawString(count.ToString(), fixedFontCount, Brushes.DarkGreen, fixedPointCount);
        //            }
        //
        //            if (cumulative > 0)
        //            {
        //                if (this.align == CumulativeTextAlign.TopRight)
        //                {
        //
        //                    if (cumulative >= 10)
        //                    {
        //                        e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative_2);
        //                    }
        //                    else
        //                    {
        //                        e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative);
        //                    }
        //                }
        //                else
        //                {
        //                    e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulativeCenter);
        //                }
        //
        //            }
        //        }
        //    }

        public bool AbstractCountDownBuff()
        {
            if (this.Count > 0)
            {
                this.Count--;
                //this.Invalidate();
                if (this.Count <= 0)
                {
                    this.sprite = null;
                    this.Update();
                    return true; // BUFF効果が切れた事を通達する。
                }
            }
            return false;
        }
    }
}
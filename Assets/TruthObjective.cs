using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonPlayer
{
    public static class TruthObjective
    {
        private static void AddEvent(string value, ref List<string> list)
        {
            if (GroundOne.ObjectiveList.Contains(value) == false)
            {
                GroundOne.ObjectiveList.Add(value);
                list.Add(value);
            }
        }
        private static void Remove(string value, ref List<string> list)
        {
            if (GroundOne.ObjectiveList.Contains(value))
            {
                list.Add(value);
            }
            GroundOne.ObjectiveList.Remove(value);
        }

        public static List<string> GetObjectiveList()
        {
            List<string> list = new List<string>();
            #region "第一階層"
            // ホームタウンで始まり
            if (GroundOne.WE.Truth_CommunicationFirstHomeTown)
            {
                if (!GroundOne.WE.Truth_CommunicationLana1)
                {
                    AddEvent(Objective1001, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationGanz1)
                {
                    AddEvent(Objective1002, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationHanna1)
                {
                    AddEvent(Objective1003, ref list);
                }
            }

            // コミュニケーションを取る
            if (GroundOne.WE.Truth_CommunicationLana1 &&
                GroundOne.WE.Truth_CommunicationGanz1 &&
                GroundOne.WE.Truth_CommunicationHanna1)
            {
                if (!GroundOne.WE.BoardInfo10 &&
                    !GroundOne.WE.BoardInfo11 &&
                    !GroundOne.WE.BoardInfo12 &&
                    !GroundOne.WE.BoardInfo13 &&
                    !GroundOne.WE.BoardInfo14)
                {
                    AddEvent(Objective1004, ref list);
                }
            }

            // ダンジョン第一階層スタート
            if (GroundOne.WE.BoardInfo10 ||
                GroundOne.WE.BoardInfo11 || 
                GroundOne.WE.BoardInfo12 ||
                GroundOne.WE.BoardInfo13 ||
                GroundOne.WE.BoardInfo14 ||
                GroundOne.WE.dungeonEvent00)
            {
                if (GroundOne.WE.AvailableSecondCharacter == false &&
                    GroundOne.WE.Truth_CommunicationJoinPartyLana == false)
                {
                    AddEvent(Objective1005, ref list);
                }
            }

            // GroundOne.WE.BoardInfo11; // 特になし

            // 大広間、４つの入り口
            if (GroundOne.WE.BoardInfo12)
            {
                if (GroundOne.WE.dungeonEvent16 == false)
                {
                    AddEvent(Objective1006, ref list);
                    if (GroundOne.WE.dungeonEvent11KeyOpen == false)
                    {
                        AddEvent(Objective1006_1, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent12KeyOpen == false)
                    {
                        AddEvent(Objective1006_2, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent13KeyOpen == false)
                    {
                        AddEvent(Objective1006_3, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent14KeyOpen == false)
                    {
                        AddEvent(Objective1006_4, ref list);
                    }
                }
            }

            // GroundOne.WE.BoardInfo13; // 特になし
            // GroundOne.WE.dungeonEvent15 // 特になし

            // 小広間への探索開始
            if (GroundOne.WE.dungeonEvent16)
            {
                if (GroundOne.WE.dungeonEvent21KeyOpen == false &&
                    GroundOne.WE.dungeonEvent22KeyOpen == false &&
                    GroundOne.WE.dungeonEvent23KeyOpen == false &&
                    GroundOne.WE.dungeonEvent24KeyOpen == false)
                {
                    AddEvent(Objective1007, ref list);
                }
            }
            // GroundOne.WE.dungeonEvent16_1NotOpen; // 特になし
            // GroundOne.WE.dungeonEvent16_2NotOpen; // 特になし
            // GroundOne.WE.dungeonEvent16_3NotOpen; // 特になし
            // GroundOne.WE.dungeonEvent16_4NotOpen; // 特になし
            // GroundOne.WE.dungeonEvent17; // 特になし
            // GroundOne.WE.dungeonEvent18; // 特になし
            // GroundOne.WE.dungeonEvent19; // 特になし
            // GroundOne.WE.dungeonEvent20; // 特になし

            // 小広間到達してから、看板を確認
            if (GroundOne.WE.dungeonEvent21KeyOpen ||
                GroundOne.WE.dungeonEvent22KeyOpen ||
                GroundOne.WE.dungeonEvent23KeyOpen ||
                GroundOne.WE.dungeonEvent24KeyOpen)
            {
                if (GroundOne.WE.BoardInfo14 == false)
                {
                    AddEvent(Objective1008, ref list);
                }
            }

            // 小広間の扉を確認する
            if (GroundOne.WE.BoardInfo14)
            {
                if (GroundOne.WE.dungeonEvent21KeyOpen == false)
                {
                    AddEvent(Objective1009_1, ref list);
                }
                if (GroundOne.WE.dungeonEvent22KeyOpen == false)
                {
                    AddEvent(Objective1009_2, ref list);
                }
                if (GroundOne.WE.dungeonEvent25 == false)
                {
                    AddEvent(Objective1009_3, ref list);
                }
                if (GroundOne.WE.dungeonEvent23KeyOpen == false)
                {
                    AddEvent(Objective1009_4, ref list);
                }
                if (GroundOne.WE.dungeonEvent24KeyOpen == false)
                {
                    AddEvent(Objective1009_5, ref list);
                }
            }

            // ボス前の侵入への扉をチェック
            if (GroundOne.WE.dungeonEvent25)
            {
                if (GroundOne.WE.dungeonEvent28KeyOpen == false)
                {
                    AddEvent(Objective1010, ref list);
                }
            }

            // ボス前の侵入への扉を解放し、ボス戦
            if (GroundOne.WE.dungeonEvent28KeyOpen)
            {
                if (GroundOne.WE.TruthCompleteSlayBoss1 == false)
                {
                    AddEvent(Objective1011, ref list);
                }
            }

            // ボス戦を撃破後、第二階層へ行く
            if (GroundOne.WE.TruthCompleteSlayBoss1)
            {
                if (GroundOne.WE.TruthCompleteArea1 == false)
                {
                    AddEvent(Objective1012, ref list);
                }
            }

            // dungeonEvent26

            // 小広間の扉を全て開けた場合、かつ、ボス戦前の扉を開けず、かつ看板を再確認した場合
            if (GroundOne.WE.dungeonEvent21KeyOpen &&
                GroundOne.WE.dungeonEvent22KeyOpen &&
                GroundOne.WE.dungeonEvent23KeyOpen &&
                GroundOne.WE.dungeonEvent24KeyOpen &&
                GroundOne.WE.dungeonEvent28KeyOpen == false &&
                GroundOne.WE.dungeonEvent27)
            {
                if (GroundOne.WE.dungeonEvent31 == false)
                {
                    AddEvent(Objective1013, ref list);
                }
            }

            // dungeonEvent29
            // dungeonEvent30
            #endregion
            #region "第二階層"
            if (GroundOne.WE.TruthCompleteArea1)
            {
                if (!GroundOne.WE.Truth_CommunicationLana21)
                {
                    AddEvent(Objective2001, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationGanz21)
                {
                    AddEvent(Objective2002, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationHanna21)
                {
                    AddEvent(Objective2003, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationOl21)
                {
                    AddEvent(Objective2004, ref list);
                }
            }
            if (GroundOne.WE.Truth_CommunicationLana21 &&
                GroundOne.WE.Truth_CommunicationGanz21 &&
                GroundOne.WE.Truth_CommunicationHanna21 &&
                GroundOne.WE.Truth_CommunicationOl21)
            {
                if (GroundOne.WE.dungeonEvent201 == false ||
                    GroundOne.WE.dungeonEvent202 == false ||
                    GroundOne.WE.dungeonEvent203 == false ||
                    GroundOne.WE.dungeonEvent204 == false)
                {
                    AddEvent(Objective2005, ref list);
                }
            }
            #endregion
            return list;
        }

        public static List<string> RefreshObjectList()
        {
            List<string> list = new List<string>();
            #region "第一階層"
            if (GroundOne.WE.Truth_CommunicationLana1)
            {
                Remove(Objective1001, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationGanz1)
            {
                Remove(Objective1002, ref list);
            } 
            if (GroundOne.WE.Truth_CommunicationHanna1)
            {
                Remove(Objective1003, ref list);
            }
            if (GroundOne.WE.BoardInfo10 || GroundOne.WE.BoardInfo11 || GroundOne.WE.BoardInfo12 || GroundOne.WE.BoardInfo13 || GroundOne.WE.BoardInfo14)
            {
                Remove(Objective1004, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationJoinPartyLana)
            {
                Remove(Objective1005, ref list);
            }
            if (GroundOne.WE.dungeonEvent11KeyOpen)
            {
                Remove(Objective1006_1, ref list);
            }
            if (GroundOne.WE.dungeonEvent12KeyOpen)
            {
                Remove(Objective1006_2, ref list);
            }
            if (GroundOne.WE.dungeonEvent13KeyOpen)
            {
                Remove(Objective1006_3, ref list);
            }
            if (GroundOne.WE.dungeonEvent14KeyOpen)
            {
                Remove(Objective1006_4, ref list);
            }
            if (GroundOne.WE.dungeonEvent16)
            {
                Remove(Objective1006, ref list);
                Remove(Objective1006_1, ref list);
                Remove(Objective1006_2, ref list);
                Remove(Objective1006_3, ref list);
                Remove(Objective1006_4, ref list);
            }
            if (GroundOne.WE.dungeonEvent21KeyOpen ||
                GroundOne.WE.dungeonEvent22KeyOpen ||
                GroundOne.WE.dungeonEvent23KeyOpen ||
                GroundOne.WE.dungeonEvent24KeyOpen)
            {
                Remove(Objective1007, ref list);
            }
            if (GroundOne.WE.BoardInfo14)
            {
                Remove(Objective1008, ref list);
            }
            if (GroundOne.WE.dungeonEvent21KeyOpen)
            {
                Remove(Objective1009_1, ref list);
            }
            if (GroundOne.WE.dungeonEvent22KeyOpen)
            {
                Remove(Objective1009_2, ref list);
            }
            if (GroundOne.WE.dungeonEvent25)
            {
                Remove(Objective1009_3, ref list);
            }
            if (GroundOne.WE.dungeonEvent23KeyOpen)
            {
                Remove(Objective1009_4, ref list);
            }
            if (GroundOne.WE.dungeonEvent24KeyOpen)
            {
                Remove(Objective1009_5, ref list);
            }
            if (GroundOne.WE.dungeonEvent28KeyOpen)
            {
                Remove(Objective1010, ref list);
            }
            if (GroundOne.WE.TruthCompleteSlayBoss1)
            {
                Remove(Objective1011, ref list);
            }
            if (GroundOne.WE.TruthCompleteArea1)
            {
                Remove(Objective1012, ref list);
            }
            if (GroundOne.WE.dungeonEvent31)
            {
                Remove(Objective1013, ref list);
            }
            #endregion
            #region "第二階層"
            if (GroundOne.WE.Truth_CommunicationLana21)
            {
                Remove(Objective2001, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationGanz21)
            {
                Remove(Objective2002, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationHanna21)
            {
                Remove(Objective2003, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationOl21)
            {
                Remove(Objective2004, ref list);
            }
            if (GroundOne.WE.dungeonEvent201 &&
                GroundOne.WE.dungeonEvent202 &&
                GroundOne.WE.dungeonEvent203 &&
                GroundOne.WE.dungeonEvent204)
            {
                Remove(Objective2005, ref list);
            }
            #endregion
            return list;
        }

        #region "第一階層"
        public const string Objective1001 = "ホームタウン：[幼なじみのラナと会話]";
        public const string Objective1002 = "ホームタウン：[天下一品　ガンツの武具店]";
        public const string Objective1003 = "ホームタウン：[ハンナのゆったり宿屋]";
        public const string Objective1004 = "ダンジョン：[第一階層を探索する]";
        public const string Objective1005 = "ホームタウン：[ラナと会話する]";
        public const string Objective1006 = "第一階層：[中央大広間] 入口扉を４つ解除]";
        public const string Objective1006_1 = "第一階層：[中央大広間] 扉１を解除";
        public const string Objective1006_2 = "第一階層：[中央大広間] 扉２を解除";
        public const string Objective1006_3 = "第一階層：[中央大広間] 扉３を解除";
        public const string Objective1006_4 = "第一階層：[中央大広間] 扉４を解除";
        public const string Objective1007 = "第一階層：[小広間へのルートを探索する]";
        public const string Objective1008 = "第一階層：[小広間] 看板を確認";
        public const string Objective1009_1 = "第一階層：[小広間] 扉１を確認";
        public const string Objective1009_2 = "第一階層：[小広間] 扉２を確認";
        public const string Objective1009_3 = "第一階層：[小広間] 扉３を確認";
        public const string Objective1009_4 = "第一階層：[小広間] 扉４を確認";
        public const string Objective1009_5 = "第一階層：[小広間] 扉５を確認";
        public const string Objective1010 = "第一階層：[小広間] 侵入への扉を開く]";
        public const string Objective1011 = "第一階層：[ボス戦] 絡みつくフランシス";
        public const string Objective1012 = "第一階層：[第二階層への到達]";
        public const string Objective1013 = "第一階層：[【始まりの地】への到達]";
        #endregion
        #region "第二階層"
        public const string Objective2001 = "ホームタウン：[ラナと会話]";
        public const string Objective2002 = "ホームタウン：[ガンツと会話]";
        public const string Objective2003 = "ホームタウン：[ハンナと会話]";
        public const string Objective2004 = "ホームタウン：[オル・ランディスと会話]";
        public const string Objective2005 = "ダンジョン：[第二階層を探索する]";
        #endregion

        public static int GetObjectiveExp(string src)
        {
            if (src == Objective1001) { return 15; }
            if (src == Objective1002) { return 20; }
            if (src == Objective1003) { return 25; }
            if (src == Objective1004) { return 30; }
            if (src == Objective1005) { return 30; }
            if (src == Objective1006) { return 500; }
            if (src == Objective1006_1) { return 100; }
            if (src == Objective1006_2) { return 100; }
            if (src == Objective1006_3) { return 100; }
            if (src == Objective1006_4) { return 100; }
            if (src == Objective1007) { return 300; }
            if (src == Objective1008) { return  100; }
            if (src == Objective1009_1) { return 400; }
            if (src == Objective1009_2) { return 400; }
            if (src == Objective1009_3) { return 400; }
            if (src == Objective1009_4) { return 400; }
            if (src == Objective1009_5) { return 400; }
            if (src == Objective1010) { return 300; }
            if (src == Objective1011) { return 1200; }
            if (src == Objective1012) { return 1500; }
            return 0;
        }

        public const int Objective1001_E = 20;
        public const int Objective1002_E = 20;
        public const int Objective1003_E = 20;
        public const int Objective1004_E = 30;
        public const int Objective1005_E = 30;
        public const int Objective1006_E = 30;
        public const int Objective1006_1_E = 30;
        public const int Objective1006_2_E = 30;
        public const int Objective1006_3_E = 30;
        public const int Objective1006_4_E = 30;
        public const int Objective1007_E = 30;
        public const int Objective1008_E = 30;
        public const int Objective1009_1_E = 30;
        public const int Objective1009_2_E = 30;
        public const int Objective1009_3_E = 30;
        public const int Objective1009_4_E = 30;
        public const int Objective1009_5_E = 30;
        public const int Objective1010_E = 30;
        public const int Objective1011_E = 30;
        public const int Objective1012_E = 30;
        public const int Objective1013_E = 30;
    }
}

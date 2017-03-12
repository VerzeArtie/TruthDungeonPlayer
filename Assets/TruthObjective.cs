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
        private static void Remove(string value)
        {
            GroundOne.ObjectiveList.Remove(value);
        }

        public static List<string> GetObjectiveList()
        {
            List<string> list = new List<string>();
            if (GroundOne.WE.Truth_CommunicationFirstHomeTown)
            {
                if (!GroundOne.WE.Truth_CommunicationLana1)
                {
                    AddEvent(Objective00001, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationGanz1)
                {
                    AddEvent(Objective00002, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationHanna1)
                {
                    AddEvent(Objective00003, ref list);
                }
            }

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
                    AddEvent(Objective00004, ref list);
                }
            }

            if (GroundOne.WE.BoardInfo10 || GroundOne.WE.BoardInfo11 || GroundOne.WE.BoardInfo12 || GroundOne.WE.BoardInfo13 || GroundOne.WE.BoardInfo14 || GroundOne.WE.dungeonEvent00)
            {
                if (GroundOne.WE.AvailableSecondCharacter == false &&
                    GroundOne.WE.Truth_CommunicationJoinPartyLana == false)
                {
                    AddEvent(Objective00005, ref list);
                }
            }

            // GroundOne.WE.BoardInfo11; // 特になし

            if (GroundOne.WE.BoardInfo12)
            {
                if (GroundOne.WE.dungeonEvent16 == false)
                {
                    AddEvent(Objective00006, ref list);
                    if (GroundOne.WE.dungeonEvent11KeyOpen == false)
                    {
                        AddEvent(Objective00006_1, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent12KeyOpen == false)
                    {
                        AddEvent(Objective00006_2, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent13KeyOpen == false)
                    {
                        AddEvent(Objective00006_3, ref list);
                    }
                    if (GroundOne.WE.dungeonEvent14KeyOpen == false)
                    {
                        AddEvent(Objective00006_4, ref list);
                    }
                }
            }

            // GroundOne.WE.BoardInfo13; // 特になし

            // GroundOne.WE.dungeonEvent15 // 特になし
            if (GroundOne.WE.dungeonEvent16)
            {
                if (GroundOne.WE.dungeonEvent21KeyOpen == false &&
                    GroundOne.WE.dungeonEvent22KeyOpen == false &&
                    GroundOne.WE.dungeonEvent23KeyOpen == false &&
                    GroundOne.WE.dungeonEvent24KeyOpen == false)
                {
                    AddEvent(Objective00007, ref list);
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

            if (GroundOne.WE.dungeonEvent21KeyOpen || GroundOne.WE.dungeonEvent22KeyOpen || GroundOne.WE.dungeonEvent23KeyOpen || GroundOne.WE.dungeonEvent24KeyOpen)
            {
                if (GroundOne.WE.BoardInfo14 == false)
                {
                    AddEvent(Objective00008, ref list);
                }
            }

            if (GroundOne.WE.BoardInfo14)
            {
                if (GroundOne.WE.dungeonEvent21KeyOpen == false)
                {
                    AddEvent(Objective00009_1, ref list);
                }
                if (GroundOne.WE.dungeonEvent22KeyOpen == false)
                {
                    AddEvent(Objective00009_2, ref list);
                }
                if (GroundOne.WE.dungeonEvent25 == false)
                {
                    AddEvent(Objective00009_3, ref list);
                }
                if (GroundOne.WE.dungeonEvent23KeyOpen == false)
                {
                    AddEvent(Objective00009_4, ref list);
                }
                if (GroundOne.WE.dungeonEvent24KeyOpen == false)
                {
                    AddEvent(Objective00009_5, ref list);
                }
            }

            if (GroundOne.WE.dungeonEvent25)
            {
                if (GroundOne.WE.dungeonEvent28KeyOpen == false)
                {
                    AddEvent(Objective00010, ref list);
                }
            }

            if (GroundOne.WE.dungeonEvent28KeyOpen)
            {
                if (GroundOne.WE.TruthCompleteSlayBoss1 == false)
                {
                    AddEvent(Objective00011, ref list);
                }
            }

            if (GroundOne.WE.TruthCompleteSlayBoss1)
            {
                if (GroundOne.WE.TruthCompleteArea1 == false)
                {
                    AddEvent(Objective00012, ref list);
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
                    AddEvent(Objective00013, ref list);
                }
            }

            // dungeonEvent29
            // dungeonEvent30


            return list;
        }

        public static void RefreshObjectList()
        {
            if (GroundOne.WE.Truth_CommunicationLana1)
            {
                Remove(Objective00001);
            }
            if (GroundOne.WE.Truth_CommunicationGanz1)
            {
                Remove(Objective00002);
            } 
            if (GroundOne.WE.Truth_CommunicationHanna1)
            {
                Remove(Objective00003);
            }
            if (GroundOne.WE.BoardInfo10 || GroundOne.WE.BoardInfo11 || GroundOne.WE.BoardInfo12 || GroundOne.WE.BoardInfo13 || GroundOne.WE.BoardInfo14)
            {
                Remove(Objective00004);
            }
            if (GroundOne.WE.Truth_CommunicationJoinPartyLana)
            {
                Remove(Objective00005);
            }
            if (GroundOne.WE.dungeonEvent11KeyOpen)
            {
                Remove(Objective00006_1);
            }
            if (GroundOne.WE.dungeonEvent12KeyOpen)
            {
                Remove(Objective00006_2);
            }
            if (GroundOne.WE.dungeonEvent13KeyOpen)
            {
                Remove(Objective00006_3);
            }
            if (GroundOne.WE.dungeonEvent14KeyOpen)
            {
                Remove(Objective00006_4);
            }
            if (GroundOne.WE.dungeonEvent16)
            {
                Remove(Objective00006);
                Remove(Objective00006_1);
                Remove(Objective00006_2);
                Remove(Objective00006_3);
                Remove(Objective00006_4);
            }
            if (GroundOne.WE.dungeonEvent21KeyOpen ||
                GroundOne.WE.dungeonEvent22KeyOpen ||
                GroundOne.WE.dungeonEvent23KeyOpen ||
                GroundOne.WE.dungeonEvent24KeyOpen)
            {
                Remove(Objective00007);
            }
            if (GroundOne.WE.BoardInfo14)
            {
                Remove(Objective00008);
            }
            if (GroundOne.WE.dungeonEvent21KeyOpen)
            {
                Remove(Objective00009_1);
            }
            if (GroundOne.WE.dungeonEvent22KeyOpen)
            {
                Remove(Objective00009_2);
            }
            if (GroundOne.WE.dungeonEvent25)
            {
                Remove(Objective00009_3);
            }
            if (GroundOne.WE.dungeonEvent23KeyOpen)
            {
                Remove(Objective00009_4);
            }
            if (GroundOne.WE.dungeonEvent24KeyOpen)
            {
                Remove(Objective00009_5);
            }
            if (GroundOne.WE.dungeonEvent28KeyOpen)
            {
                Remove(Objective00010);
            }
            if (GroundOne.WE.TruthCompleteSlayBoss1)
            {
                Remove(Objective00011);
            }
            if (GroundOne.WE.TruthCompleteArea1)
            {
                Remove(Objective00012);
            }
            if (GroundOne.WE.dungeonEvent31)
            {
                Remove(Objective00013);
            }
        }

        public const string Objective00001 = "ホームタウン：[幼なじみのラナと会話]";
        public const string Objective00002 = "ホームタウン：[天下一品　ガンツの武具店]";
        public const string Objective00003 = "ホームタウン：[ハンナのゆったり宿屋]";
        public const string Objective00004 = "ダンジョン：[第一階層を探索する]";
        public const string Objective00005 = "ホームタウン：[ラナと会話する]";
        public const string Objective00006 = "第一階層：[中央の大広間] 入り口側の扉を４つ開く]";
        public const string Objective00006_1 = "第一階層：[中央の大広間] 扉１を解除";
        public const string Objective00006_2 = "第一階層：[中央の大広間] 扉２を解除";
        public const string Objective00006_3 = "第一階層：[中央の大広間] 扉３を解除";
        public const string Objective00006_4 = "第一階層：[中央の大広間] 扉４を解除";
        public const string Objective00007 = "第一階層：[小広間へのルートを探索する]";
        public const string Objective00008 = "第一階層：[小広間] 看板を確認";
        public const string Objective00009_1 = "第一階層：[小広間] 扉１を確認";
        public const string Objective00009_2 = "第一階層：[小広間] 扉２を確認";
        public const string Objective00009_3 = "第一階層：[小広間] 扉３を確認";
        public const string Objective00009_4 = "第一階層：[小広間] 扉４を確認";
        public const string Objective00009_5 = "第一階層：[小広間] 扉５を確認";
        public const string Objective00010 = "第一階層：[小広間] 侵入への扉を開く]";
        public const string Objective00011 = "第一階層：[ボス戦] 絡みつくフランシス";
        public const string Objective00012 = "第一階層：[第二階層への到達]";
        public const string Objective00013 = "第一階層：[【始まりの地】を探索する]";
    }
}

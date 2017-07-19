using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
            if (GroundOne.WE.dungeonEvent201 && GroundOne.WE.dungeonEvent202 && GroundOne.WE.dungeonEvent203 && GroundOne.WE.dungeonEvent204)
            {
                if (GroundOne.WE.dungeonEvent224 == false)
                {
                    AddEvent(Objective2006, ref list);
                }
                if (GroundOne.WE.dungeonEvent257 == false)
                {
                    AddEvent(Objective2007, ref list);
                }
                if (GroundOne.WE.dungeonEvent249 == false)
                {
                    AddEvent(Objective2008, ref list);
                }
                if (GroundOne.WE.dungeonEvent255_SlayBoss == false)
                {
                    AddEvent(Objective2009, ref list);
                }
            }
            // 力の部屋
            if (GroundOne.WE.dungeonEvent250)
            {
                if (GroundOne.WE.dungeonEvent250_SlayBoss == false)
                {
                    AddEvent(Objective2010, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent251)
            {
                if (GroundOne.WE.dungeonEvent251_SlayBoss == false)
                {
                    AddEvent(Objective2011, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent252)
            {
                if (GroundOne.WE.dungeonEvent252_SlayBoss == false)
                {
                    AddEvent(Objective2012, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent253)
            {
                if (GroundOne.WE.dungeonEvent253_SlayBoss == false)
                {
                    AddEvent(Objective2013, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent254)
            {
                if (GroundOne.WE.dungeonEvent254_SlayBoss == false)
                {
                    AddEvent(Objective2014, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent255)
            {
                if (GroundOne.WE.dungeonEvent255_SlayBoss == false)
                {
                    AddEvent(Objective2015, ref list);
                }
            }
            // 技の部屋
            if (GroundOne.WE.dungeonEvent233)
            {
                if (GroundOne.WE.dungeonEvent233_Complete == false)
                {
                    AddEvent(Objective2016, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent234)
            {
                if (GroundOne.WE.dungeonEvent234_Complete == false)
                {
                    AddEvent(Objective2017, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent235)
            {
                if (GroundOne.WE.dungeonEvent235_Complete == false)
                {
                    AddEvent(Objective2018, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent236)
            {
                if (GroundOne.WE.dungeonEvent236_Complete == false)
                {
                    AddEvent(Objective2019, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent237)
            {
                if (GroundOne.WE.dungeonEvent237_Complete == false)
                {
                    AddEvent(Objective2020, ref list);
                }
            }
            // 知の部屋
            if (GroundOne.WE.dungeonEvent206)
            {
                if (GroundOne.WE.dungeonEvent208 == false)
                {
                    AddEvent(Objective2021, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent208)
            {
                if (GroundOne.WE.dungeonEvent210 == false)
                {
                    AddEvent(Objective2022, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent210)
            {
                if (GroundOne.WE.dungeonEvent211 == false)
                {
                    AddEvent(Objective2023, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent211)
            {
                if (GroundOne.WE.dungeonEvent212 == false || GroundOne.WE.dungeonEvent213 == false || GroundOne.WE.dungeonEvent214 == false)
                {
                    AddEvent(Objective2024, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent212 && GroundOne.WE.dungeonEvent213 && GroundOne.WE.dungeonEvent214 && GroundOne.WE.dungeonEvent215)
            {
                if (GroundOne.WE.dungeonEvent219 == false)
                {
                    AddEvent(Objective2025, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent219)
            {
                if (GroundOne.WE.dungeonEvent221 == false || GroundOne.WE.dungeonEvent222 == false || GroundOne.WE.dungeonEvent223 == false)
                {
                    AddEvent(Objective2026, ref list);
                }
            }
            if (GroundOne.WE.dungeonEvent220)
            {
                if (GroundOne.WE.dungeonEvent224 == false)
                {
                    if (GroundOne.WE2.TruthAnswerFail == false)
                    {
                        AddEvent(Objective2027, ref list);
                    }
                }
            }
            if (GroundOne.WE.dungeonEvent215 && GroundOne.WE.dungeonEvent219 && GroundOne.WE.dungeonEvent220 && GroundOne.WE2.TruthAnswerFail)
            {
                if (GroundOne.WE.dungeonEvent224 == false)
                {
                    AddEvent(Objective2028, ref list);
                }
            }

            // ボス戦を撃破後、第三階層へ行く
            if (GroundOne.WE.TruthCompleteSlayBoss2)
            {
                if (GroundOne.WE.TruthCommunicationCompArea2 == false)
                {
                    AddEvent(Objective2029, ref list);
                }
            }
            #endregion
            #region "第三階層"
            if (GroundOne.WE.TruthCompleteArea2)
            {
                if (!GroundOne.WE.Truth_CommunicationLana31)
                {
                    AddEvent(Objective3001, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationGanz31)
                {
                    AddEvent(Objective3002, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationHanna31)
                {
                    AddEvent(Objective3003, ref list);
                }
                if (!GroundOne.WE.Truth_CommunicationOl31)
                {
                    AddEvent(Objective3004, ref list);
                }
                if (GroundOne.WE.Truth_CommunicationLana31)
                {
                    if (!GroundOne.WE.Truth_CommunicationSinikia31)
                    {
                        AddEvent(Objective3005, ref list);
                    }
                }
                if (GroundOne.WE.Truth_CommunicationOl31)
                {
                    if (!GroundOne.WE.AvailableItemBank)
                    {
                        AddEvent(Objective3006, ref list);
                    }
                }
            }
            // 第3階層開始
            if (GroundOne.WE.Truth_CommunicationLana31 &&
                GroundOne.WE.Truth_CommunicationGanz31 &&
                GroundOne.WE.Truth_CommunicationHanna31 &&
                GroundOne.WE.Truth_CommunicationOl31 &&
                GroundOne.WE.Truth_CommunicationSinikia31 &&
                GroundOne.WE.AvailableItemBank)
            {
                if (GroundOne.WE.dungeonEvent301 == false)
                {
                    AddEvent(Objective3007, ref list);
                }
            }
            // 鏡の間の前の2つの看板を読む
            if (GroundOne.WE.dungeonEvent301)
            {
                if (GroundOne.WE.dungeonEvent302_1 == false)
                {
                    AddEvent(Objective3008, ref list);
                }
                if (GroundOne.WE.dungeonEvent302_2 == false)
                {
                    AddEvent(Objective3009, ref list);
                }
            }

            // 鏡の間【探求】を走破する
            if (GroundOne.WE.dungeonEvent302_3)
            {
                if (GroundOne.WE.dungeonEvent305 == false)
                {
                    AddEvent(Objective3010, ref list);
                }
            }

            // 鏡の間【探求】の先へと進む
            if (GroundOne.WE.dungeonEvent305)
            {
                if (GroundOne.WE.dungeonEvent326 == false)
                {
                    AddEvent(Objective3011, ref list);
                }
            }
            // 鏡の間【迷宮】　第一の看板を読む。
            if (GroundOne.WE.dungeonEvent308)
            {
                if (GroundOne.WE.dungeonEvent314 == false)
                {
                    AddEvent(Objective3012, ref list);
                }
            }
            // 鏡の間【迷宮】　第二の看板を読む。
            if (GroundOne.WE.dungeonEvent309)
            {
                if (GroundOne.WE.dungeonEvent315 == false)
                {
                    AddEvent(Objective3013, ref list);
                }
            }
            // 鏡の間【迷宮】　第三の看板を読む。
            if (GroundOne.WE.dungeonEvent310)
            {
                if (GroundOne.WE.dungeonEvent316 == false)
                {
                    AddEvent(Objective3014, ref list);
                }
            }
            // 鏡の間【迷宮】　第四の看板を読む。
            if (GroundOne.WE.dungeonEvent311)
            {
                if (GroundOne.WE.dungeonEvent317 == false)
                {
                    AddEvent(Objective3015, ref list);
                }
            }
            // 鏡の間【迷宮】を走破する
            if (GroundOne.WE.dungeonEvent326)
            {
                if (GroundOne.WE.dungeonEvent318 == false)
                {
                    AddEvent(Objective3016, ref list);
                }
            }
            // 絶対試練
            if (GroundOne.WE.dungeonEvent318)
            {
                if (GroundOne.WE.dungeonEvent312 == false)
                {
                    if (GroundOne.WE2.TruthWillFail == false)
                    {
                        AddEvent(Objective3017, ref list);
                    }
                    else
                    {
                        AddEvent(Objective3017_2, ref list);
                    }
                }
            }
            // 絶対試練の先へ進む
            if (GroundOne.WE.dungeonEvent312)
            {
                if (GroundOne.WE.dungeonEvent319 == false)
                {
                    AddEvent(Objective3018, ref list);
                }
            }
            // ボス
            if (GroundOne.WE.dungeonEvent319)
            {
                if (GroundOne.WE.dungeonEvent3_SlayBoss == false)
                {
                    AddEvent(Objective3019, ref list);
                }
            }
            // 第四階層到達
            if (GroundOne.WE.dungeonEvent3_SlayBoss)
            {
                if (GroundOne.WE.TruthCommunicationCompArea3 == false)
                {
                    AddEvent(Objective3020, ref list);
                }
            }
            #endregion
            #region "第四階層"
            if (GroundOne.WE.TruthCompleteArea3)
            {
                if (GroundOne.WE.Truth_CommunicationLana41 == false)
                {
                    AddEvent(Objective4001, ref list);
                }
                if (GroundOne.WE.Truth_CommunicationGanz41 == false)
                {
                    AddEvent(Objective4002, ref list);
                }
                if (GroundOne.WE.Truth_CommunicationHanna41 == false)
                {
                    AddEvent(Objective4003, ref list);
                }
                if (GroundOne.WE.Truth_CommunicationOl41 == false)
                {
                    AddEvent(Objective4004, ref list);
                }
                if (GroundOne.WE.Truth_CommunicationSinikia41 == false)
                {
                    AddEvent(Objective4005, ref list);
                }
            }
            // 第4階層開始
            if (GroundOne.WE.Truth_CommunicationLana41 &&
                GroundOne.WE.Truth_CommunicationGanz41 &&
                GroundOne.WE.Truth_CommunicationHanna41 &&
                GroundOne.WE.Truth_CommunicationOl41 &&
                GroundOne.WE.Truth_CommunicationSinikia41)
            {
                if (GroundOne.WE.dungeonEvent401 == false)
                {
                    AddEvent(Objective4006, ref list);
                }
            }
            // 第一の間、看板を確認
            if (GroundOne.WE.dungeonEvent401)
            {
                if (GroundOne.WE.dungeonEvent402 == false)
                {
                    AddEvent(Objective4007, ref list);
                }
            }
            // 第一の間　探索を進める
            if (GroundOne.WE.dungeonEvent402)
            {
                if (GroundOne.WE.dungeonEvent403 == false)
                {
                    AddEvent(Objective4008, ref list);
                }
            }
            // 第一の間　エスミリア草原区域、森の細道
            if (GroundOne.WE.dungeonEvent403)
            {
                if (GroundOne.WE.dungeonEvent406 == false)
                {
                    AddEvent(Objective4009, ref list);
                }
            }
            // 第一の間　森の細道では一本道、ダンジョン迷宮では分岐が存在する
            if (GroundOne.WE.dungeonEvent406)
            {
                if (GroundOne.WE.dungeonEvent409 == false)
                {
                    AddEvent(Objective4010, ref list);
                }
            }
            // 第一の間　森で失いしは、フェルトゥーシュの剣
            if (GroundOne.WE.dungeonEvent409)
            {
                if (GroundOne.WE.dungeonEvent411 == false)
                {
                    AddEvent(Objective4011, ref list);
                }
            }
            // 第一の間　森は剣と共に、真実を覆い隠す
            if (GroundOne.WE.dungeonEvent411)
            {
                if (GroundOne.WE.dungeonEvent413 == false)
                {
                    AddEvent(Objective4012, ref list);
                }
            }
            // 第一の間　隠された真実への記憶は、森の奥底に眠る
            if (GroundOne.WE.dungeonEvent413)
            {
                if (GroundOne.WE.dungeonEvent416 == false)
                {
                    AddEvent(Objective4013, ref list);
                }
            }
            // 第一の間　呼び覚まされし、DUELの旋律
            if (GroundOne.WE.dungeonEvent416)
            {
                if (GroundOne.WE.dungeonEvent418 == false)
                {
                    AddEvent(Objective4014, ref list);
                }
            }
            // 第一の間　その特性、神々の遺産を超えるものなり
            if (GroundOne.WE.dungeonEvent418)
            {
                if (GroundOne.WE.dungeonEvent420 == false)
                {
                    AddEvent(Objective4015, ref list);
                }
            }
            // 第一の間　古来より存在せしフェルトゥーシュ、絶対的な死と破滅を運命とする
            if (GroundOne.WE.dungeonEvent420)
            {
                if (GroundOne.WE.dungeonEvent422 == false)
                {
                    AddEvent(Objective4016, ref list);
                }
            }
            // 第一の間　【 瘴気 】
            if (GroundOne.WE.dungeonEvent422)
            {
                if (GroundOne.WE.dungeonEvent423 == false)
                {
                    AddEvent(Objective4017, ref list);
                }
            }
            // 第一の間　ボス戦　闇焔レギィン・アーゼ【瘴気】
            if (GroundOne.WE.dungeonEvent423)
            {
                if (GroundOne.WE.dungeonEvent4_SlayBoss1 == false)
                {
                    AddEvent(Objective4018, ref list);
                }
            }
            // 第一の間　部屋を確認する
            if (GroundOne.WE.dungeonEvent4_SlayBoss1)
            {
                if (GroundOne.WE.dungeonEvent424 == false)
                {
                    AddEvent(Objective4019, ref list);
                }
            }
            // 第二の間　看板を確認する
            if (GroundOne.WE.dungeonEvent424)
            {
                if (GroundOne.WE.dungeonEvent426 == false)
                {
                    AddEvent(Objective4020, ref list);
                }
            }
            // 第二の間　探索を進める
            if (GroundOne.WE.dungeonEvent426)
            {
                if (GroundOne.WE.dungeonEvent427 == false)
                {
                    AddEvent(Objective4021, ref list);
                }
            }
            // 第二の間　宝物庫にて、鍵を拾い集めよ
            if (GroundOne.WE.dungeonEvent427)
            {
                if (GroundOne.WE.dungeonEvent431 == false)
                {
                    AddEvent(Objective4022, ref list);
                }
            }
            // 第二の間　生と死にまつわる、事実の再結合
            if (GroundOne.WE.dungeonEvent431)
            {
                if (GroundOne.WE.dungeonEvent435 == false)
                {
                    AddEvent(Objective4023, ref list);
                }
            }
            // 第二の間　必然性と偶発性に起因する、真実の再統合
            if (GroundOne.WE.dungeonEvent435)
            {
                if (GroundOne.WE.dungeonEvent437 == false)
                {
                    AddEvent(Objective4024, ref list);
                }
            }
            // 第二の間　【 無音 】
            if (GroundOne.WE.dungeonEvent437)
            {
                if (GroundOne.WE.dungeonEvent439 == false)
                {
                    AddEvent(Objective4025, ref list);
                }
            }
            // 第二の間　ボス戦　闇焔レギィン・アーゼ【無音】
            if (GroundOne.WE.dungeonEvent439)
            {
                if (GroundOne.WE.dungeonEvent4_SlayBoss2 == false)
                {
                    AddEvent(Objective4026, ref list);
                }
            }
            // 第二の間　部屋を確認する
            if (GroundOne.WE.dungeonEvent4_SlayBoss2)
            {
                if (GroundOne.WE.dungeonEvent440 == false)
                {
                    AddEvent(Objective4027, ref list);
                }
            }
            // 第三の間　看板を確認する
            if (GroundOne.WE.dungeonEvent440)
            {
                if (GroundOne.WE.dungeonEvent442 == false)
                {
                    AddEvent(Objective4028, ref list);
                }
            }
            // 第三の間　探索を進める
            if (GroundOne.WE.dungeonEvent442)
            {
                if (GroundOne.WE.dungeonEvent443 == false)
                {
                    AddEvent(Objective4029, ref list);
                }
            }
            // 第三の間　事象と時間を紡ぎし者にのみ、次なる道は拓かれる
            if (GroundOne.WE.dungeonEvent443)
            {
                if (GroundOne.WE.dungeonEvent460 == false)
                {
                    AddEvent(Objective4030, ref list);
                }
            }
            // 第三の間　真実と事実は非なるもの　真実への追及が事実を覆す
            if (GroundOne.WE.dungeonEvent460)
            {
                if (GroundOne.WE.dungeonEvent473 == false)
                {
                    AddEvent(Objective4031, ref list);
                }
            }
            // 第三の間　【 深淵 】
            if (GroundOne.WE.dungeonEvent473)
            {
                if (GroundOne.WE.dungeonEvent475 == false)
                {
                    AddEvent(Objective4032, ref list);
                }
            }
            // 第三の間　ボス戦　闇焔レギィン・アーゼ【深淵】
            if (GroundOne.WE.dungeonEvent475)
            {
                if (GroundOne.WE.dungeonEvent4_SlayBoss3 == false)
                {
                    AddEvent(Objective4033, ref list);
                }
            }
            // 第三の間　部屋を確認する
            if (GroundOne.WE.dungeonEvent4_SlayBoss3)
            {
                if (GroundOne.WE.dungeonEvent476 == false)
                {
                    AddEvent(Objective4034, ref list);
                }
            }
            // 第四の間　看板を確認する
            if (GroundOne.WE.dungeonEvent476)
            {
                if (GroundOne.WE.dungeonEvent477 == false)
                {
                    AddEvent(Objective4035, ref list);
                }
            }
            // 第四の間　探索を進める
            if (GroundOne.WE.dungeonEvent477)
            {
                if (GroundOne.WE.dungeonEvent479 == false)
                {
                    AddEvent(Objective4036, ref list);
                }
            }
            // 第四の間　見捨てられし狭間
            if (GroundOne.WE.dungeonEvent479)
            {
                if (GroundOne.WE.dungeonEvent484 == false)
                {
                    AddEvent(Objective4037, ref list);
                }
            }
            // 第四の間　捨て去られた空間
            if (GroundOne.WE.dungeonEvent484)
            {
                if (GroundOne.WE.dungeonEvent488 == false)
                {
                    AddEvent(Objective4038, ref list);
                }
            }
            // 第四の間　探索を進める　【　　】
            if (GroundOne.WE.dungeonEvent488)
            {
                if (GroundOne.WE.dungeonEvent490 == false)
                {
                    AddEvent(Objective4039, ref list);
                }
            }
            // 第四の間　[　　　　]　【　　　　　　 　　 】
            if (GroundOne.WE.dungeonEvent490)
            {
                if (GroundOne.WE2.RealWorld == false)
                {
                    AddEvent(Objective4040, ref list);
                }
            }

            // ここから先は、WE2をベースとしたものになります。
            //// 第四の間　歩を進める
            //if (GroundOne.WE2.RealWorld)
            //{
            //    if (GroundOne.WE2.SeekerEvent2 == false)
            //    {
            //        AddEvent(Objective4041, ref list);
            //    }
            //}
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
            if (GroundOne.WE.dungeonEvent224)
            {
                Remove(Objective2006, ref list);
            }
            if (GroundOne.WE.dungeonEvent257)
            {
                Remove(Objective2007, ref list);
            }
            if (GroundOne.WE.dungeonEvent249)
            {
                Remove(Objective2008, ref list);
            }
            if (GroundOne.WE.dungeonEvent255_SlayBoss)
            {
                Remove(Objective2009, ref list);
            }
            if (GroundOne.WE.dungeonEvent250_SlayBoss)
            {
                Remove(Objective2010, ref list);
            }
            if (GroundOne.WE.dungeonEvent251_SlayBoss)
            {
                Remove(Objective2011, ref list);
            }
            if (GroundOne.WE.dungeonEvent252_SlayBoss)
            {
                Remove(Objective2012, ref list);
            }
            if (GroundOne.WE.dungeonEvent253_SlayBoss)
            {
                Remove(Objective2013, ref list);
            }
            if (GroundOne.WE.dungeonEvent254_SlayBoss)
            {
                Remove(Objective2014, ref list);
            }
            if (GroundOne.WE.dungeonEvent255_SlayBoss)
            {
                Remove(Objective2015, ref list);
            }
            if (GroundOne.WE.dungeonEvent233_Complete)
            {
                Remove(Objective2016, ref list);
            }
            if (GroundOne.WE.dungeonEvent234_Complete)
            {
                Remove(Objective2017, ref list);
            }
            if (GroundOne.WE.dungeonEvent235_Complete)
            {
                Remove(Objective2018, ref list);
            }
            if (GroundOne.WE.dungeonEvent236_Complete)
            {
                Remove(Objective2019, ref list);
            }
            if (GroundOne.WE.dungeonEvent237_Complete)
            {
                Remove(Objective2020, ref list);
            }
            if (GroundOne.WE.dungeonEvent208)
            {
                Remove(Objective2021, ref list);
            }
            if (GroundOne.WE.dungeonEvent210)
            {
                Remove(Objective2022, ref list);
            }
            if (GroundOne.WE.dungeonEvent211)
            {
                Remove(Objective2023, ref list);
            }
            if (GroundOne.WE.dungeonEvent212 && GroundOne.WE.dungeonEvent213 && GroundOne.WE.dungeonEvent214)
            {
                Remove(Objective2024, ref list);
            }
            if (GroundOne.WE.dungeonEvent219)
            {
                Remove(Objective2025, ref list);
            }
            if (GroundOne.WE.dungeonEvent221 && GroundOne.WE.dungeonEvent222 && GroundOne.WE.dungeonEvent223)
            {
                Remove(Objective2026, ref list);
            }
            if (GroundOne.WE.dungeonEvent224)
            {
                Remove(Objective2027, ref list);
            }
            if (GroundOne.WE.dungeonEvent224)
            {
                Remove(Objective2028, ref list);
            }
            if (GroundOne.WE.TruthCommunicationCompArea2)
            {
                Remove(Objective2029, ref list);
            }
            #endregion
            #region "第三階層"
            if (GroundOne.WE.Truth_CommunicationLana31)
            {
                Remove(Objective3001, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationGanz31)
            {
                Remove(Objective3002, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationHanna31)
            {
                Remove(Objective3003, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationOl31)
            {
                Remove(Objective3004, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationSinikia31)
            {
                Remove(Objective3005, ref list);
            }
            if (GroundOne.WE.AvailableItemBank)
            {
                Remove(Objective3006, ref list);
            }
            if (GroundOne.WE.dungeonEvent301)
            {
                Remove(Objective3007, ref list);
            }
            if (GroundOne.WE.dungeonEvent302_1)
            {
                Remove(Objective3008, ref list);
            }
            if (GroundOne.WE.dungeonEvent302_2)
            {
                Remove(Objective3009, ref list);
            }
            if (GroundOne.WE.dungeonEvent305)
            {
                Remove(Objective3010, ref list);
            }
            if (GroundOne.WE.dungeonEvent326)
            {
                Remove(Objective3011, ref list);
            }
            if (GroundOne.WE.dungeonEvent314)
            {
                Remove(Objective3012, ref list);
            }
            if (GroundOne.WE.dungeonEvent315)
            {
                Remove(Objective3013, ref list);
            }
            if (GroundOne.WE.dungeonEvent316)
            {
                Remove(Objective3014, ref list);
            }
            if (GroundOne.WE.dungeonEvent317)
            {
                Remove(Objective3015, ref list);
            }
            if (GroundOne.WE.dungeonEvent318)
            {
                Remove(Objective3016, ref list);
            }
            if (GroundOne.WE.dungeonEvent312)
            {
                Remove(Objective3017, ref list);
                Remove(Objective3017_2, ref list);
            }
            if (GroundOne.WE.dungeonEvent319)
            {
                Remove(Objective3018, ref list);
            }
            if (GroundOne.WE.dungeonEvent3_SlayBoss)
            {
                Remove(Objective3019, ref list);
            }
            if (GroundOne.WE.TruthCommunicationCompArea3)
            {
                Remove(Objective3020, ref list);
            }
            #endregion
            #region "第四階層"
            if (GroundOne.WE.Truth_CommunicationLana41)
            {
                Remove(Objective4001, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationGanz41)
            {
                Remove(Objective4002, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationHanna41)
            {
                Remove(Objective4003, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationOl41)
            {
                Remove(Objective4004, ref list);
            }
            if (GroundOne.WE.Truth_CommunicationSinikia41)
            {
                Remove(Objective4005, ref list);
            }
            if (GroundOne.WE.dungeonEvent401)
            {
                Remove(Objective4006, ref list);
            }
            if (GroundOne.WE.dungeonEvent402)
            {
                Remove(Objective4007, ref list);
            }
            if (GroundOne.WE.dungeonEvent403)
            {
                Remove(Objective4008, ref list);
            }
            if (GroundOne.WE.dungeonEvent406)
            {
                Remove(Objective4009, ref list);
            }
            if (GroundOne.WE.dungeonEvent409)
            {
                Remove(Objective4010, ref list);
            }
            if (GroundOne.WE.dungeonEvent411)
            {
                Remove(Objective4011, ref list);
            }
            if (GroundOne.WE.dungeonEvent413)
            {
                Remove(Objective4012, ref list);
            }
            if (GroundOne.WE.dungeonEvent416)
            {
                Remove(Objective4013, ref list);
            }
            if (GroundOne.WE.dungeonEvent418)
            {
                Remove(Objective4014, ref list);
            }
            if (GroundOne.WE.dungeonEvent420)
            {
                Remove(Objective4015, ref list);
            }
            if (GroundOne.WE.dungeonEvent422)
            {
                Remove(Objective4016, ref list);
            }
            if (GroundOne.WE.dungeonEvent423)
            {
                Remove(Objective4017, ref list);
            }
            if (GroundOne.WE.dungeonEvent4_SlayBoss1)
            {
                Remove(Objective4018, ref list);
            }
            if (GroundOne.WE.dungeonEvent424)
            {
                Remove(Objective4019, ref list);
            }
            if (GroundOne.WE.dungeonEvent426)
            {
                Remove(Objective4020, ref list);
            }
            if (GroundOne.WE.dungeonEvent427)
            {
                Remove(Objective4021, ref list);
            }
            if (GroundOne.WE.dungeonEvent431)
            {
                Remove(Objective4022, ref list);
            }
            if (GroundOne.WE.dungeonEvent435)
            {
                Remove(Objective4023, ref list);
            }
            if (GroundOne.WE.dungeonEvent437)
            {
                Remove(Objective4024, ref list);
            }
            if (GroundOne.WE.dungeonEvent439)
            {
                Remove(Objective4025, ref list);
            }
            if (GroundOne.WE.dungeonEvent4_SlayBoss2)
            {
                Remove(Objective4026, ref list);
            }
            if (GroundOne.WE.dungeonEvent440)
            {
                Remove(Objective4027, ref list);
            }
            if (GroundOne.WE.dungeonEvent442)
            {
                Remove(Objective4028, ref list);
            }
            if (GroundOne.WE.dungeonEvent443 )
            {
                Remove(Objective4029, ref list);
            }
            if (GroundOne.WE.dungeonEvent460)
            {
                Remove(Objective4030, ref list);
            }
            if (GroundOne.WE.dungeonEvent473)
            {
                Remove(Objective4031, ref list);
            }
            if (GroundOne.WE.dungeonEvent475)
            {
                Remove(Objective4032, ref list);
            }
            if (GroundOne.WE.dungeonEvent4_SlayBoss3)
            {
                Remove(Objective4033, ref list);
            }
            if (GroundOne.WE.dungeonEvent476)
            {
                Remove(Objective4034, ref list);
            }
            if (GroundOne.WE.dungeonEvent477)
            {
                Remove(Objective4035, ref list);
            }
            if (GroundOne.WE.dungeonEvent479)
            {
                Remove(Objective4036, ref list);
            }
            if (GroundOne.WE.dungeonEvent484)
            {
                Remove(Objective4037, ref list);
            }
            if (GroundOne.WE.dungeonEvent488)
            {
                Remove(Objective4038, ref list);
            }
            if (GroundOne.WE.dungeonEvent490)
            {
                Remove(Objective4039, ref list);
            }
            if (GroundOne.WE2.RealWorld)
            {
                Remove(Objective4040, ref list);
            }
            #endregion
            return list;
        }

        // RefreshObjectListでクエスト完了判定をする際、同一の文字列があると終わってないクエストが
        // 完了とみなされてしまうため、Objectiveの文字列はすべて異なる文字にする必要がある。
        #region "第一階層"
        public const string Objective1001 = "ホームタウン：[幼なじみのラナと会話]";
        public const string Objective1002 = "ホームタウン：[天下一品　ガンツの武具店]";
        public const string Objective1003 = "ホームタウン：[ハンナのゆったり宿屋]";
        public const string Objective1004 = "ダンジョン：[第一階層を探索開始]";
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
        public const string Objective2001 = "ホームタウン：[ラナと会話(階層２)]";
        public const string Objective2002 = "ホームタウン：[ガンツと会話(階層２)]";
        public const string Objective2003 = "ホームタウン：[ハンナと会話(階層２)]";
        public const string Objective2004 = "ホームタウン：[オル・ランディスと会話(階層２)]";
        public const string Objective2005 = "ダンジョン：[第二階層を探索開始]";
        public const string Objective2006 = "第二階層：[知の部屋]を解く";
        public const string Objective2007 = "第二階層：[技の部屋]を解く";
        public const string Objective2008 = "第二階層：[心の部屋]を解く";
        public const string Objective2009 = "第二階層：[力の部屋]を解く";
        public const string Objective2010 = "第二階層：[ボス戦] 輝ける海の王子";
        public const string Objective2011 = "第二階層：[ボス戦] 源星・珊瑚の女王";
        public const string Objective2012 = "第二階層：[ボス戦] シェル・ザ・ソードナイト";
        public const string Objective2013 = "第二階層：[ボス戦] ジェリーアイ【熱光】／【流冷】";
        public const string Objective2014 = "第二階層：[ボス戦] 海星源の王";
        public const string Objective2015 = "第二階層：[ボス戦] 大海蛇リヴィアサン";
        public const string Objective2016 = "第二階層：[技の部屋]　俊足歩行";
        public const string Objective2017 = "第二階層：[技の部屋]　即断即決";
        public const string Objective2018 = "第二階層：[技の部屋]　無感呼吸";
        public const string Objective2019 = "第二階層：[技の部屋]　浮上遊歩";
        public const string Objective2020 = "第二階層：[技の部屋]　縦横無尽";
        public const string Objective2021 = "第二階層：[知の部屋]　『上』";
        public const string Objective2022 = "第二階層：[知の部屋]　『上』→『下』";
        public const string Objective2023 = "第二階層：[知の部屋]　『上』→『下』→『左』";
        public const string Objective2024 = "第二階層：[知の部屋]　開かれた３つの部屋";
        public const string Objective2025 = "第二階層：[知の部屋]　『下３０　上１２５６９　左８４７』";
        public const string Objective2026 = "第二階層：[知の部屋]　開かれた３つの部屋（PART2）";
        public const string Objective2027 = "第二階層：[知の部屋]　『  ( >10 _6 <7 )  ( <11 ^3 )  ( _3 >7 )  』";
        public const string Objective2028 = "第二階層：[知の部屋]　『絶対試練：汝、答えを示せ』";
        public const string Objective2029 = "第二階層：[第三階層への到達]";
        //public const string Objective2030 = "第二階層：[への到達]";
        #endregion
        #region "第三階層"
        public const string Objective3001 = "ホームタウン：[ラナと会話(階層３)]";
        public const string Objective3002 = "ホームタウン：[ガンツと会話(階層３)]";
        public const string Objective3003 = "ホームタウン：[ハンナと会話(階層３)]";
        public const string Objective3004 = "ホームタウン：[オル・ランディスと会話(階層３)]";
        public const string Objective3005 = "ホームタウン：[ゲート裏　転送装置へ向かう]";
        public const string Objective3006 = "ホームタウン：[ハンナの荷物小屋]";
        public const string Objective3007 = "ダンジョン：[第三階層を探索開始]";
        public const string Objective3008 = "第三階層：[入口の看板を探索１]";
        public const string Objective3009 = "第三階層：[入口の看板を探索２]";
        public const string Objective3010 = "第三階層：[鏡の間【探求】を走破する]";
        public const string Objective3011 = "第三階層：[鏡の間【探求】の先へ進む]";
        public const string Objective3012 = "第三階層：[鏡の間【迷宮】　第一の選択を決定する]";
        public const string Objective3013 = "第三階層：[鏡の間【迷宮】　第二の選択を決定する]";
        public const string Objective3014 = "第三階層：[鏡の間【迷宮】　第三の選択を決定する]";
        public const string Objective3015 = "第三階層：[鏡の間【迷宮】　第四の選択を決定する]";
        public const string Objective3016 = "第三階層：[鏡の間【迷宮】を走破する]";
        public const string Objective3017 = "第三階層：[鏡の間【迷宮】の先へ進む』";
        public const string Objective3017_2 = "第三階層：『絶対試練：汝、答えを示せ』";
        public const string Objective3018 = "第三階層：[鏡の間『絶対試練』の先へ進む]";
        public const string Objective3019 = "第三階層：[ボス戦] 恐鳴主ハウリング・シーザー";
        public const string Objective3020 = "第三階層：[第四階層への到達]";
        //public const string Objective3021 = "第三階層：[鏡の間【創成】を走破する]";
        #endregion
        #region "第四階層"
        public const string Objective4001 = "ホームタウン：[ラナと会話(階層４)]";
        public const string Objective4002 = "ホームタウン：[ガンツと会話(階層４)]";
        public const string Objective4003 = "ホームタウン：[ハンナと会話(階層４)]";
        public const string Objective4004 = "ホームタウン：[オル・ランディスと会話(階層４)]";
        public const string Objective4005 = "ホームタウン：[カールハンツと会話(階層４)]";
        public const string Objective4006 = "ダンジョン：[第四階層を探索開始]";
        public const string Objective4007 = "第四階層：[第一の間]　看板を確認する";
        public const string Objective4008 = "第四階層：[第一の間]　探索を進める";
        public const string Objective4009 = "第四階層：[第一の間]　探索を進める(エスミリア草原区域、森の細道)";
        public const string Objective4010 = "第四階層：[第一の間]　探索を進める(森の細道では一本道、ダンジョン迷宮では分岐が存在する)";
        public const string Objective4011 = "第四階層：[第一の間]　探索を進める(森で失いしは、フェルトゥーシュの剣)";
        public const string Objective4012 = "第四階層：[第一の間]　探索を進める(森は剣と共に、真実を覆い隠す)";
        public const string Objective4013 = "第四階層：[第一の間]　探索を進める(隠された真実への記憶は、森の奥底に眠る)";
        public const string Objective4014 = "第四階層：[第一の間]　探索を進める(呼び覚まされし、DUELの旋律)";
        public const string Objective4015 = "第四階層：[第一の間]　探索を進める(その特性、神々の遺産を超えるものなり)";
        public const string Objective4016 = "第四階層：[第一の間]　探索を進める(古来より存在せしフェルトゥーシュ、絶対的な死と破滅を運命とする)";
        public const string Objective4017 = "第四階層：[第一の間]　探索を進める【 瘴気 】";
        public const string Objective4018 = "第四階層：[ボス戦] 闇焔レギィン・アーゼ【瘴気】";
        public const string Objective4019 = "第四階層：[第一の間]　部屋を確認する";
        public const string Objective4020 = "第四階層：[第二の間]　看板を確認する";
        public const string Objective4021 = "第四階層：[第二の間]　探索を進める";
        public const string Objective4022 = "第四階層：[第二の間]　探索を進める(宝物庫にて、鍵を拾い集めよ)";
        public const string Objective4023 = "第四階層：[第二の間]　探索を進める(生と死にまつわる、事実の再結合)";
        public const string Objective4024 = "第四階層：[第二の間]　探索を進める(必然性と偶発性に起因する、真実の再統合)";
        public const string Objective4025 = "第四階層：[第二の間]　探索を進める【 無音 】";
        public const string Objective4026 = "第四階層：[ボス戦] 闇焔レギィン・アーゼ【無音】";
        public const string Objective4027 = "第四階層：[第二の間]　部屋を確認する";
        public const string Objective4028 = "第四階層：[第三の間]　看板を確認する";
        public const string Objective4029 = "第四階層：[第三の間]　探索を進める";
        public const string Objective4030 = "第四階層：[第三の間]　探索を進める(事象と時間を紡ぎし者にのみ、次なる道は拓かれる)";
        public const string Objective4031 = "第四階層：[第三の間]　探索を進める(真実と事実は非なるもの　真実への追及が事実を覆す)";
        public const string Objective4032 = "第四階層：[第三の間]　探索を進める【 深淵 】";
        public const string Objective4033 = "第四階層：[ボス戦] 闇焔レギィン・アーゼ【深淵】";
        public const string Objective4034 = "第四階層：[第三の間]　部屋を確認する";
        public const string Objective4035 = "第四階層：[第四の間]　看板を確認する";
        public const string Objective4036 = "第四階層：[第四の間]　探索を進める";
        public const string Objective4037 = "第四階層：[第四の間]　探索を進める(見捨てられし狭間)";
        public const string Objective4038 = "第四階層：[第四の間]　探索を進める(捨て去られた空間)";
        public const string Objective4039 = "第四階層：[第四の間]　探索を進める【 　　 】";
        public const string Objective4040 = "第四階層：[　　　　]　【　　　　　　 　　 】";
        public const string Objective4041 = "第四階層：[歩を進める]";
        public const string Objective4042 = "第四階層：[第五階層への到達]";
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
            if (src == Objective1013) { return 1800; }
            if (src == Objective2001) { return 1500; }
            if (src == Objective2002) { return 1500; }
            if (src == Objective2003) { return 1500; }
            if (src == Objective2004) { return 1500; }
            if (src == Objective2005) { return 2000; }
            if (src == Objective2006) { return 8000; }
            if (src == Objective2007) { return 8000; }
            if (src == Objective2008) { return 8000; }
            if (src == Objective2009) { return 8000; }
            if (src == Objective2010) { return 4000; }
            if (src == Objective2011) { return 6000; }
            if (src == Objective2012) { return 7000; }
            if (src == Objective2013) { return 10000; }
            if (src == Objective2014) { return 12000; }
            if (src == Objective2015) { return 15000; }
            if (src == Objective2016) { return 4000; }
            if (src == Objective2017) { return 6000; }
            if (src == Objective2018) { return 8000; }
            if (src == Objective2019) { return 10000; }
            if (src == Objective2020) { return 12000; }
            if (src == Objective2021) { return 4000; }
            if (src == Objective2022) { return 5000; }
            if (src == Objective2023) { return 6000; }
            if (src == Objective2024) { return 7000; }
            if (src == Objective2025) { return 8000; }
            if (src == Objective2026) { return 9000; }
            if (src == Objective2027) { return 10000; }
            if (src == Objective2028) { return 12000; }
            if (src == Objective2029) { return 15000; }
            if (src == Objective3001) { return  3000; }
            if (src == Objective3002) { return  3000; }
            if (src == Objective3003) { return  3000; }
            if (src == Objective3004) { return  3000; }
            if (src == Objective3005) { return  3000; }
            if (src == Objective3006) { return  3000; }
            if (src == Objective3007) { return  5000; }
            if (src == Objective3008) { return  5000; }
            if (src == Objective3009) { return  5000; }
            if (src == Objective3010) { return 20000; }
            if (src == Objective3011) { return 22000; }
            if (src == Objective3012) { return 24000; }
            if (src == Objective3013) { return 26000; }
            if (src == Objective3014) { return 28000; }
            if (src == Objective3015) { return 30000; }
            if (src == Objective3016) { return 32000; }
            if (src == Objective3017) { return 34000; }
            if (src == Objective3017_2) { return 34000; }
            if (src == Objective3018) { return 35000; }
            if (src == Objective3019) { return 50000; }
            if (src == Objective3020) { return 55000; }
            if (src == Objective4001) { return 25000; }
            if (src == Objective4002) { return 25000; }
            if (src == Objective4003) { return 25000; }
            if (src == Objective4004) { return 25000; }
            if (src == Objective4005) { return 25000; }
            if (src == Objective4006) { return 27000; }
            if (src == Objective4007) { return 27000; }
            if (src == Objective4008) { return 27000; }
            if (src == Objective4009) { return 30000; }
            if (src == Objective4010) { return 31000; }
            if (src == Objective4011) { return 32000; }
            if (src == Objective4012) { return 33000; }
            if (src == Objective4013) { return 34000; }
            if (src == Objective4014) { return 35000; }
            if (src == Objective4015) { return 36000; }
            if (src == Objective4016) { return 37000; }
            if (src == Objective4017) { return 38000; }
            if (src == Objective4018) { return 80000; }
            if (src == Objective4019) { return 40000; }
            if (src == Objective4020) { return 40000; }
            if (src == Objective4021) { return 40000; }
            if (src == Objective4022) { return 45000; }
            if (src == Objective4023) { return 46000; }
            if (src == Objective4024) { return 47000; }
            if (src == Objective4025) { return 48000; }
            if (src == Objective4026) { return 100000; }
            if (src == Objective4027) { return 50000; }
            if (src == Objective4028) { return 50000; }
            if (src == Objective4029) { return 55000; }
            if (src == Objective4030) { return 60000; }
            if (src == Objective4031) { return 64000; }
            if (src == Objective4032) { return 68000; }
            if (src == Objective4033) { return 120000; }
            if (src == Objective4034) { return 70000; }
            if (src == Objective4035) { return 70000; }
            if (src == Objective4036) { return 70000; }
            if (src == Objective4037) { return 80000; }
            if (src == Objective4038) { return 85000; }
            if (src == Objective4039) { return 90000; }
            if (src == Objective4040) { return 0; }
            return 0;
        }

    }
}

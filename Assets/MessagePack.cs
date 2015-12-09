﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonPlayer;

namespace DungeonPlayer
{
    public static class MessagePack
    {
        public enum ActionEvent
        {
            None,
            UpdateLocationTop,
            UpdateLocationLeft,
            UpdateLocationRight,
            UpdateLocationBottom,
            HomeTown,
            TurnToBlack,
            ReturnToNormal,
            BlueOpenTop,
            BlueOpenLeft,
            BlueOpenRight,
            BlueOpenBottom,
            BigEntranceOpen,
            SmallEntranceOpen1,
            SmallEntranceOpen2,
            CenterBlueOpen,
            UpdateUnknownTile,
            EncountFlansis,
            StopMusic,
            PlayMusic14,
            PlayMusic15,
            PlayMusic16,
            YesNoGotoDungeon2,
            GotoHomeTown,
            GotoDungeon2,
            DecisionOpenDoor1,
        }

        public static void MessageBackToTown(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ユングの町に戻るか？"); eventList.Add(ActionEvent.HomeTown);
        }

        public static void MessageNotFound(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ん？特に何もなかったと思うが。"); eventList.Add(ActionEvent.None);
        }
        public static void Message10000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.BoardInfo10)
            {
                messageList.Add("アイン：看板があるな・・・なになに？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『始まりの地、見落とすべからず』"); eventList.Add(ActionEvent.None);
                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("アイン：始まりの地って、ここのダンジョン１階の事か？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：そうなんじゃない？最初が肝心って意味じゃないかしら。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：見落とす・・・べからず？　べからずってどういう意味だよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：「見落としてはいけません」って意味でしょ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何だ、普通の看板だな。特にコレといった特徴もねえし。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：くまなくダンジョンを探索してみましょ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：了解了解！"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：始まりの地って、ここのダンジョン１階の事か？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っと、見落とすべからず？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：しまった・・・「べからず」ってどういう意味だよ。くそ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、いいか。後でラナにでも聞いてみるか。"); eventList.Add(ActionEvent.None);
                }
                GroundOne.WE.BoardInfo10 = true;
            }
            else
            {
                messageList.Add("　　　　『始まりの地、見落とすべからず。』"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message10001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.BoardInfo11)
            {
                messageList.Add("アイン：こんなトコにも看板か。・・・ええっと？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『近道にこそ、危険が潜む』"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("ラナ：進むの止めといた方が良いんじゃない？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何でだよ？近道だろ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：危険って書いてあるじゃない。あからさまな罠でしょ。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.MC.Level < 10)
                    {
                        messageList.Add("アイン：危険って書いてあるだけだろ？大丈夫だって。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：何で大丈夫なのよ！？引き返しなさいよ！！！"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：だあぁぁ、分かったって！引き返すって！っな？"); eventList.Add(ActionEvent.None);

                        // UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：（駄目だ・・・ラナが許してくれそうにもねえ・・・）"); eventList.Add(ActionEvent.UpdateLocationRight);

                        messageList.Add("アイン：（もう少し、レベルアップするか何かしないとな・・・）"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：でもな俺もLV" + GroundOne.MC.Level + "なワケだし、大丈夫だろ？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：そうかしら・・・そんな簡単に進めるとは思えないけど。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まあ明らかに危ないと感じたら引き返す。それで良いだろ？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：ん～、まあ良いわ。分かったわよ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：っしゃ、じゃあ進もうぜ！"); eventList.Add(ActionEvent.None);

                        GroundOne.WE.dungeonEvent29 = true;
                    }
                    GroundOne.WE.dungeonEvent30 = true;
                }
                else
                {
                    messageList.Add("アイン：なるほどな。まあ確かに近道ってのは危険がつきものだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：って事は、ひょっとしたら・・・すげえルートがあるかもしれねえな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナが居たら絶対ひき止められそうだが、行ってみるか！"); eventList.Add(ActionEvent.None);
                }

                GroundOne.WE.BoardInfo11 = true;
            }
            else
            {
                if (GroundOne.WE.dungeonEvent29 == false)
                {
                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("　　　　『近道にこそ、危険が潜む。』"); eventList.Add(ActionEvent.None);

                        if (GroundOne.WE.dungeonEvent30 == false)
                        {
                            messageList.Add("ラナ：進むの止めといた方が良いんじゃない？"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：何でだよ？近道だろ？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：危険って書いてあるじゃない。あからさまな罠でしょ。"); eventList.Add(ActionEvent.None);

                            if (GroundOne.MC.Level < 10)
                            {
                                messageList.Add("アイン：危険って書いてあるだけだろ？大丈夫だって。"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：何で大丈夫なのよ！？引き返しなさいよ！！！"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：だあぁぁ、分かったって！引き返すって！っな？"); eventList.Add(ActionEvent.None);

                                //UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：（駄目だ・・・ラナが許してくれそうにもねえ・・・）"); eventList.Add(ActionEvent.UpdateLocationRight);

                                messageList.Add("アイン：（もう少し、レベルアップするか何かしないとな・・・）"); eventList.Add(ActionEvent.None);
                            }
                            else
                            {
                                messageList.Add("アイン：でもな俺もLV" + GroundOne.MC.Level + "なワケだし、大丈夫だろ？"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：そうかしら・・・そんな簡単に進めるとは思えないけど。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：まあ明らかに危ないと感じたら引き返す。それで良いだろ？"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：ん～、まあ良いわ。分かったわよ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：っしゃ、じゃあ進もうぜ！"); eventList.Add(ActionEvent.None);

                                GroundOne.WE.dungeonEvent29 = true;
                            }
                            GroundOne.WE.dungeonEvent30 = true;
                        }
                        else
                        {
                            if (GroundOne.MC.Level < 8)
                            {
                                messageList.Add("ラナ：・・・ちょっと！？"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：分かった、分かった・・・分かりました、ラナ様・・・"); eventList.Add(ActionEvent.None);

                                //UpdatePlayerLocationInfo(this.Player.transform.position.x + Database.DUNGEON_MOVE_LEN, this.Player.transform.position.y, false); eventList.Add(ActionEvent.None);

                                messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationRight);
                            }
                            else
                            {
                                messageList.Add("アイン：ラナ、俺もLV" + GroundOne.MC.Level + "に到達してる。もう、大丈夫だろ？"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：そうかしら・・・そんな簡単に進めるとは思えないけど。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：まあ明らかに危ないと感じたら引き返す。それで良いだろ？"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：ん～、まあ良いわ。分かったわよ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：っしゃ、じゃあ進もうぜ！"); eventList.Add(ActionEvent.None);

                                GroundOne.WE.dungeonEvent29 = true;
                            }
                        }
                    }
                    else
                    {
                        messageList.Add("　　　　『近道にこそ、危険が潜む。』"); eventList.Add(ActionEvent.None);
                    }
                }
                else
                {
                    messageList.Add("　　　　『近道にこそ、危険が潜む。』"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.BoardInfo12)
            {
                messageList.Add("アイン：おっ、看板発見っと！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『入り口側の扉をすべて開けよ。さすれば、道は開けるであろう。』"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("ラナ：結構扉が並んでるわね、ズラリと。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：各角に２個ずつ扉があるわね。合計８箇所ってとこかしら。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：入り口側っていう切り口も少し気になるわね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：このダンジョン。妙に迷路っぽいだろ。方向が掴み難くいんだよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：少しは自分でマッピングしていったらどうなのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：面倒くせえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：まったく、そういうとこ本当治らないわね・・・はあぁぁぁ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：まあ良いわ。進む方向とマッピングは私がやるから。アインは戦闘の前衛をお願いね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：サンキュー。恩にきるぜ！"); eventList.Add(ActionEvent.None);

                }
                else
                {

                    messageList.Add("アイン：へえ、結構な扉の数だな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：各角に２箇所ずつ、合計８箇所って事か。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：入り口側ってのが少し気にかかるが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ考えててもしょうがねえ。探索するしかないな。"); eventList.Add(ActionEvent.None);
                }

                GroundOne.WE.BoardInfo12 = true;
            }
            else
            {
                if (GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.dungeonEvent15)
                {
                    messageList.Add("アイン：何かよくわかんねえよな。この看板。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：入り口側・・・入り口側・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：入り口側って、ひょっとして、右側４つを指しているんじゃないかしら？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：マジかよ！？何でそうなるんだよ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：単純な理屈よ。単にスタート地点がこの大広間より右側にある、それだけよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ソレだ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ちょっと・・・違うかも知れないわよ？ひょっとしたら反対側の４つかも知れないし。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：良いってイイって！それでいこうぜ！右側４箇所だな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っちょ、本当に適当よ。間違ってても知らないわよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：了解！了解！ッハッハッハ！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent15 = true;
                }
                else
                {
                    messageList.Add("　　　　『入り口側の扉をすべて開けよ。さすれば、道は開けるであろう。』"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.BoardInfo13)
            {
                messageList.Add("アイン：看板が立ててあるな。なになに？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『ダンジョンはメンバー構成によって変化する。』"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("アイン：しかしだな、ラナが加わって何か変わったんだろうか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アインが入った時と比べて、何か変化してる？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いいや、特に変わりは無いぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：本当に変化してるのかどうか、分からないわね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダンジョンの全構図が見えてれば、分かるんだろうが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：さすがに今の時点じゃ分からねえな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：まあ、もう少し他を探索してみましょ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：ん？そうなのか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、今は俺一人だから変わりようがねえけどな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：でも、面白そうだな。今度ラナでも誘ってみるか。"); eventList.Add(ActionEvent.None);
                }

                GroundOne.WE.BoardInfo13 = true;
            }
            else
            {
                messageList.Add("　　　　『ダンジョンはメンバー構成によって変化する。』"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message10004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent22KeyOpen && GroundOne.WE.dungeonEvent23KeyOpen && GroundOne.WE.dungeonEvent24KeyOpen && !GroundOne.WE.dungeonEvent27 && !GroundOne.WE.dungeonEvent28KeyOpen)
            {
                messageList.Add("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】"); eventList.Add(ActionEvent.TurnToBlack);

                messageList.Add("　　　　『真実解１　　＜始まりの地にて＞　　地点【４７　２９】』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おいおい・・・なんだよ、これ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ちょっと待って、マップ見てみるわ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：・・・４７　２９は多分座標ポイントね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：その数字からすると・・・右下のホラ、この辺よきっと。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：やべ・・・頭が・・・ッグ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っちょ、アイン大丈夫！？汗びっしょりじゃないの！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグ・・・だ・・・大丈夫だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：激しい頭痛がするだけだ。心配すんなって、大丈夫だから。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そんな汗びっしょりでどこが大丈夫なのよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・すまねえ。一旦戻ろう。遠見の青水晶だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：最初からそう言いなさいよ。じゃあ戻るわよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：遠見の青水晶を使うわ。ッハイ！"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.ReturnToNormal);

                GroundOne.WE.dungeonEvent27 = true;

                messageList.Add(""); eventList.Add(ActionEvent.HomeTown);
            }
            else
            {
                if (!GroundOne.WE.BoardInfo14)
                {
                    if (!GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("アイン：こんなとこにも看板があるな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("　　　　『くまなく、探したか？』"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：なんだこりゃ。くまなく・・・探したか？？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：やべ、わかんねえ単語だな。。。「くまなく」って何だよ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まあいい。ラナに聞いてみるとするか。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：こんなとこにも看板があるな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("　　　　『くまなく、探したか？』"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：くまなく・・・熊無く・・・"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：隅々まで余すことなく、って意味よ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：サンキュー。隅々まで探したかどうか・・・か。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：う～ん・・・いまいち分かり辛い内容だな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：「ダンジョン内をくまなく探せ。」って意味でしょ？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：あんまり深く考えない方が得策よ♪"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ああ・・・まあそうだな・・・。"); eventList.Add(ActionEvent.None);
                    }

                    GroundOne.WE.BoardInfo14 = true;
                }
                else
                {
                    if (GroundOne.WE.dungeonEvent27)
                    {
                        messageList.Add("　　　　『真実解１　　＜始まりの地にて＞　　地点【４７　２９】』"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("　　　　『くまなく、探したか？』"); eventList.Add(ActionEvent.None);
                    }
                }
            }
        }

        public static void Message10005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ボスとの戦闘だ！気を引き締めていくぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.EncountFlansis);
        }
        public static void Message10012(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent11KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent11NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent11NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10013(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent11KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen)
                {
                    messageList.Add("アイン：っと、何だこりゃ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何かの扉みたいが見えるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：特に鍵もかかってないみたいね。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵はどうもかかってなさそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けてみるとするか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenLeft);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：わぁ、大きい広間ね。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：なんとなく見とれてしまうわね。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ああ、そうだな・・・"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まあ探索を続けようぜ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);
                    }

                    GroundOne.WE.dungeonEvent11KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenLeft);

                    if (GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.BigEntranceOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：おお！見ろよラナ！　向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：これで先に進めるってわけね♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：いや、ホント。俺だけで進められなかったぜコレ。本当にサンキューな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：何言ってるのよ。まだ終わったわけじゃないんだから。ホラホラ、先に進みましょ♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：了解！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：おお！向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：っしゃ、さっそく向こう側の方へ行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent16 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    else if ((GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent11KeyOpen = true;
                }
            }
        }

        public static void Message10014(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent12KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent12NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent12NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10015(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent12KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen)
                {
                    messageList.Add("アイン：っと、何だこりゃ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何かの扉みたいが見えるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：特に鍵もかかってないみたいね。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵はどうもかかってなさそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けてみるとするか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenTop);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent12KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenTop);

                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.BigEntranceOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：おお！見ろよラナ！　向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：これで先に進めるってわけね♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：いや、ホント。俺だけで進められなかったぜコレ。本当にサンキューな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：何言ってるのよ。まだ終わったわけじゃないんだから。ホラホラ、先に進みましょ♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：了解！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：おお！向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：っしゃ、さっそく向こう側の方へ行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent16 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent12KeyOpen = true;
                }
            }
        }

        public static void Message10016(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent13KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent13NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent13NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10017(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent13KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen)
                {
                    messageList.Add("アイン：っと、何だこりゃ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何かの扉みたいが見えるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：特に鍵もかかってないみたいね。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵はどうもかかってなさそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けてみるとするか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』");

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenLeft);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent13KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenLeft);


                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.BigEntranceOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：おお！見ろよラナ！　向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：これで先に進めるってわけね♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：いや、ホント。俺だけで進められなかったぜコレ。本当にサンキューな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：何言ってるのよ。まだ終わったわけじゃないんだから。ホラホラ、先に進みましょ♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：了解！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：おお！向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：っしゃ、さっそく向こう側の方へ行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent16 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent14KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent13KeyOpen = true;
                }
            }
        }

        public static void Message10018(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent14KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent14NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent14NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10019(ref List<string> messageList, ref List<ActionEvent> eventList)
        {

            if (!GroundOne.WE.dungeonEvent14KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen)
                {
                    messageList.Add("アイン：っと、何だこりゃ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何かの扉みたいが見えるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationBottom);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：特に鍵もかかってないみたいね。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵はどうもかかってなさそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けてみるとするか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenBottom);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent14KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationBottom);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenBottom);

                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.BigEntranceOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：おお！見ろよラナ！　向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：これで先に進めるってわけね♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：いや、ホント。俺だけで進められなかったぜコレ。本当にサンキューな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：何言ってるのよ。まだ終わったわけじゃないんだから。ホラホラ、先に進みましょ♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：了解！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：おお！向こう側の扉が一気に開いたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：っしゃ、さっそく向こう側の方へ行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent16 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen) ||
                             (GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    else if ((GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && !GroundOne.WE.dungeonEvent13KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent11KeyOpen && !GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen))
                    {
                        messageList.Add("アイン：おし、これで２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ他のルートが残ってそうだな。引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent14KeyOpen = true;
                }
            }
        }

        public static void Message10020(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent16)
            {
                if (!GroundOne.WE.dungeonEvent17)
                {
                    messageList.Add("アイン：おっと、何だこりゃ。扉か？");

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateUnknownTile);

                    messageList.Add("アイン：くそっ、開かねえな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：今のところ、通れないみたいだし、他のルートを探すとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent17 = true;
                }
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent18)
                {
                    if (!GroundOne.WE.dungeonEvent17)
                    {
                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：ん？何か妙に綺麗な造りをした通路だな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：まるで扉でもあったかのような場所に見えるな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：アイン、良く分かるわね。確かに、ここはもともと扉があったみたいよ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：ラナこそ良く分かったな、そんな事？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：この壁の隙間よ。扉がぴったり収まってるわ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：なるほど、最初は閉じてたが、いつのまにか開いたって事か。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：まあいい。開いてるんだし、さっそく進もうぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：まったく何の考えも無しに突っ込んで、知らないわよ？"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：大丈夫大丈夫！ッハッハッハ！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：ん？何か妙に綺麗な造りをした通路だな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：まるで扉でもあったかのような場所に見えるな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：・・・なるほど。両脇に扉が押し込まれてるな。そういうことだったのか。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：まあいい。幸い扉は開いてる。ガンガン進むとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent18 = true;

                    }
                    else
                    {
                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：おぉ、いつの間にか扉が開いてるじゃねえか！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：でも気をつけてよね。こういう所って大概危ないんだから。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：そんな危ない所、あるわけねえだろ？さっさと行くぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：まったく何の考えも無しに突っ込んで、知らないわよ？"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：大丈夫大丈夫！ッハッハッハ！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：おし、思ったとおり。この扉も開くってわけだな。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：ラナが居たら『何の考えも無しに』とか言いそうだが・・・。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：まあこの扉仕掛け自体が本命だろ。このまま突っ切るぜ！"); eventList.Add(ActionEvent.None);
                        }
                        GroundOne.WE.dungeonEvent18 = true;
                    }
                }
            }
        }

        public static void Message10021(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent16)
            {
                messageList.Add("アイン：扉は閉じたままだな。"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message10040(ref List<string> messageList, ref List<ActionEvent> eventList)
        {

            if (!GroundOne.WE.dungeonEvent20)
            {
                if (!GroundOne.WE.dungeonEvent19)
                {
                    messageList.Add("アイン：おっ、ここも扉ってワケか。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateUnknownTile);

                    messageList.Add("アイン：やっぱりココも開かないようだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：開く方法も見つからねえし、やはり他のルートを探すか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent19 = true;
                }
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent26)
                {
                    if (!GroundOne.WE.dungeonEvent19)
                    {
                        if (!GroundOne.WE.dungeonEvent17)
                        {
                            messageList.Add("アイン：ここも、何か妙に綺麗な造りだな。"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("ラナ：この壁の隙間にもホラ。扉が収まってるわ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：本当だな。さっきの音はここのだったのかも知れないな。"); eventList.Add(ActionEvent.None);
                            }
                            else
                            {
                                messageList.Add("アイン：まるで扉でもあったかのような場所だが・・・"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：おっ、やっぱりな。両サイドにピッタリと扉がはまっているぜ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：きっとここも扉で閉まってたんだろうな。"); eventList.Add(ActionEvent.None);
                            }

                            messageList.Add("アイン：さてと・・・このマップからして・・・"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：きっとお宝だ！！　マチガイない！！"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("ラナ：何でそうなるのよ？"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：俺の勘がそう言ってるんだ。まちがいねえぜ！"); eventList.Add(ActionEvent.None);
                            }

                            messageList.Add("アイン：さあ、進むぜ！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("ラナ：大丈夫なのかしら、ホント・・・"); eventList.Add(ActionEvent.None);
                            }

                            GroundOne.WE.dungeonEvent26 = true;
                        }
                        else
                        {
                            messageList.Add("アイン：ん？何か妙に綺麗な造りをした通路だな。"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("アイン：まるで扉でもあったかのような場所に見えるな。"); eventList.Add(ActionEvent.None);

                                messageList.Add("ラナ：アイン、良く分かるわね。確かに、ここはもともと扉があったみたいよ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：ってことは、さっきの音はここのだったのかも知れないな。"); eventList.Add(ActionEvent.None);
                            }
                            else
                            {
                                messageList.Add("アイン：まるで扉でもあったかのような場所だが・・・"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：おっ、やっぱりな。両サイドにピッタリと扉がはまっているぜ。"); eventList.Add(ActionEvent.None);

                                messageList.Add("アイン：きっとここも扉で閉まってたんだろうな。"); eventList.Add(ActionEvent.None);
                            }

                            messageList.Add("アイン：さてと・・・このマップからして・・・"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：きっとお宝だ！！　マチガイない！！"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("ラナ：何でそうなるのよ？");

                                messageList.Add("アイン：俺の勘がそう言ってるんだ。まちがいねえぜ！"); eventList.Add(ActionEvent.None);
                            }

                            messageList.Add("アイン：さあ、進むぜ！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

                            if (GroundOne.WE.AvailableSecondCharacter)
                            {
                                messageList.Add("ラナ：大丈夫なのかしら、ホント・・・"); eventList.Add(ActionEvent.None);
                            }

                            GroundOne.WE.dungeonEvent26 = true;
                        }
                    }
                    else
                    {
                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：っしゃ！予想通り、ここの扉が開いてるぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：よく気づいたわね。こんな所。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：ああ、大広間の仕掛けがヒントになっていたのさ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：あの大広間、４箇所開くと次の扉が開いただろ？"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：だから小広間も扉を開いて行けば、いずれどこか開くってワケさ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：バカのくせにこういう所はホント勘が良いわね。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：っさてと！これは絶対にお宝だ！！おっ宝お宝！！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：あんまりはしゃぎすぎて、変なトラップにはまらないでよね？"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：了解了解！　待ってろよ、お宝！！"); eventList.Add(ActionEvent.None);

                        }
                        else
                        {
                            messageList.Add("アイン：よしきた！ここの扉も開くと思ってたぜ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：大広間が例の仕掛けだからな。類似系パターンが適用されてたってワケだ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：よし・・・って事は・・・"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：お宝に違いねえ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：待ってろよ、お宝！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent26 = true;
                    }
                }
            }
        }

        public static void Message10041(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent20)
            {
                messageList.Add("アイン：扉は閉じたままだな。"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message10042(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent11KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent21NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent21NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10043(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent21KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent22KeyOpen && !GroundOne.WE.dungeonEvent23KeyOpen)
                {
                    messageList.Add("アイン：っお・・・扉だな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうするの？調べてみる？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ああ、少しだけ調べてみるとするか！"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen1);

                    messageList.Add("アイン：へえ、少し狭めの広間に出たな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・ん？"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうかしたの？"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：・・・気のせいか、少し強い気配を感じる。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：バカアインもそういう気配察知は大したものよね。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：バカは余計だ。まあ、気をつけて探索を続けようぜ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：少し気をつけた方が良さそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    GroundOne.WE.dungeonEvent21KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：オッケー。小広間に繋がる扉発見！"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen1);

                    if (GroundOne.WE.dungeonEvent22KeyOpen && GroundOne.WE.dungeonEvent23KeyOpen)
                    {
                        if (GroundOne.WE.dungeonEvent28KeyOpen)
                        {
                            messageList.Add("アイン：小広間、４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.CenterBlueOpen);
                        
                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：っお！何か遠くで音がしなかったか！？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：うん、確かになんか聞こえたわね。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：どっかの扉が開いたんだろ。行ってみようぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：ッフフ、はしゃいじゃって。じゃあ、行ってみましょ♪"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：っしゃ！何かの音がしたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：おそらくどこかの扉が開いたんだろ。行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent20 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent22KeyOpen && !GroundOne.WE.dungeonEvent23KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent22KeyOpen && GroundOne.WE.dungeonEvent23KeyOpen))
                    {
                        if (GroundOne.WE.dungeonEvent28KeyOpen)
                        {
                            messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：小広間、２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }

                        messageList.Add("アイン：まだ特に何も起きてねえが・・・引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent21KeyOpen = true;
                }
            }
        }

        public static void Message10044(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent22KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent22NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent22NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10045(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent22KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent21KeyOpen && !GroundOne.WE.dungeonEvent23KeyOpen)
                {
                    messageList.Add("アイン：っお・・・扉だな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうするの？調べてみる？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ああ、少しだけ調べてみるとするか！"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen1);

                    messageList.Add("アイン：へえ、少し狭めの広間に出たな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・ん？"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうかしたの？"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：・・・気のせいか、少し強い気配を感じる。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：バカアインもそういう気配察知は大したものよね。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：バカは余計だ。まあ、気をつけて探索を続けようぜ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：少し気をつけた方が良さそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    GroundOne.WE.dungeonEvent22KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：オッケー。小広間に繋がる扉発見！"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen1);

                    if (GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent23KeyOpen)
                    {
                        if (GroundOne.WE.dungeonEvent28KeyOpen)
                        {
                            messageList.Add("アイン：小広間、４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.CenterBlueOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：っお！何か遠くで音がしなかったか！？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：うん、確かになんか聞こえたわね。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：どっかの扉が開いたんだろ。行ってみようぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：ッフフ、はしゃいじゃって。じゃあ、行ってみましょ♪"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：っしゃ！何かの音がしたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：おそらくどこかの扉が開いたんだろ。行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent20 = true;
                    }
                    else if ((GroundOne.WE.dungeonEvent21KeyOpen && !GroundOne.WE.dungeonEvent23KeyOpen) ||
                             (!GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent23KeyOpen))
                    {
                        if (GroundOne.WE.dungeonEvent28KeyOpen)
                        {
                            messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：小広間、２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);
                        }

                        messageList.Add("アイン：まだ特に何も起きてねえが・・・引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent22KeyOpen = true;
                }
            }
        }

        public static void Message10046(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent24KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent24NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent24NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10047(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent24KeyOpen)
            {
                if (GroundOne.WE.dungeonEvent28KeyOpen)
                {
                    messageList.Add("アイン：よし、いよいよこれで小広間の扉も５つ目だ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：よし、いよいよこれで小広間の扉も４つ目だ。"); eventList.Add(ActionEvent.None);
                }

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("ラナ：小広間って確か５つ扉があったわよね？"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.dungeonEvent28KeyOpen)
                    {
                        messageList.Add("アイン：ああ、ここが５つ目って事になるな。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：ああ、これが４つ目って事になるな。"); eventList.Add(ActionEvent.None);
                    }
                }
                else
                {
                    if (GroundOne.WE.dungeonEvent28KeyOpen)
                    {
                        messageList.Add("アイン：（ココが最後の５つ目って考えるのは少し変だが・・・）"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：（俺の読みが正しければ、・・・これが先に開くべき扉だ)"); eventList.Add(ActionEvent.None);
                    }
                }

                messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("ラナ：トラップとかも特に無さそうよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何か期待してしまうな・・・それじゃ開けるぜ！"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：っしゃ・・・開けるぜ！"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen2);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    if (GroundOne.WE.dungeonEvent28KeyOpen)
                    {
                        messageList.Add("アイン：特に何も無いみたいだな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：・・・う～ん。宝箱が１個あったじゃない。そういう事じゃない？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まあ、そうかも知れないな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：まあ、あんまり深く考えずに進めましょ♪"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：あぁ、了解だ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：特に何も無いみたいだな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：・・・そうでも無いみたいよ。ホラ、あの看板。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：看板？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：ここからだと良く見えないけど、少し文章が変わってるように見えるわ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：本当かよ！？早速見に行こうぜ！"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：うん、行ってみましょ。"); eventList.Add(ActionEvent.None);
                    }
                }
                else
                {
                    if (GroundOne.WE.dungeonEvent28KeyOpen)
                    {
                        messageList.Add("アイン：そうそう、何も起きないか、さすがに。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：（宝箱・・・だけか）"); eventList.Add(ActionEvent.None);


                        messageList.Add("アイン：（まあ、あんまり深く考えててもしょうがねえよな）"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：さて、次へ進めるとするか。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：そうそう、何も起きないか、さすがに。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ん？あの看板・・・"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ここからだと良く見えないが、何か文章が変わってるように見えるな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：早速見に行ってみるとするか！"); eventList.Add(ActionEvent.None);
                    }
                }

                GroundOne.WE.dungeonEvent24KeyOpen = true;
            }
        }

        public static void Message10048(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent23KeyOpen)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent23NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent23NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10049(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent23KeyOpen)
            {
                if (!GroundOne.WE.dungeonEvent21KeyOpen && !GroundOne.WE.dungeonEvent22KeyOpen)
                {
                    messageList.Add("アイン：っお・・・扉だな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうするの？調べてみる？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ああ、少しだけ調べてみるとするか！"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen2);

                    messageList.Add("アイン：へえ、少し狭めの広間に出たな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・ん？"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：どうかしたの？"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：・・・気のせいか、少し強い気配を感じる。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：バカアインもそういう気配察知は大したものよね。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：バカは余計だ。まあ、気をつけて探索を続けようぜ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：少し気をつけた方が良さそうだな。"); eventList.Add(ActionEvent.None);
                    }

                    GroundOne.WE.dungeonEvent23KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：オッケー。小広間に繋がる扉発見！"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen2);

                    if (GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent22KeyOpen)
                    {
                        messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.CenterBlueOpen);

                        if (GroundOne.WE.AvailableSecondCharacter)
                        {
                            messageList.Add("アイン：っお！何か遠くで音がしなかったか！？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：うん、確かになんか聞こえたわね。"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：どっかの扉が開いたんだろ。行ってみようぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：ッフフ、はしゃいじゃって。じゃあ、行ってみましょ♪"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            messageList.Add("アイン：っしゃ！何かの音がしたぜ！"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：おそらくどこかの扉が開いたんだろ。行ってみるとするか！"); eventList.Add(ActionEvent.None);
                        }

                        GroundOne.WE.dungeonEvent20 = true;
                    }
                    else if ((!GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent22KeyOpen) ||
                             (GroundOne.WE.dungeonEvent21KeyOpen && !GroundOne.WE.dungeonEvent22KeyOpen))
                    {
                        messageList.Add("アイン：小広間、２つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：まだ特に何も起きてねえが・・・引き続き探索と行くか！"); eventList.Add(ActionEvent.None);
                    }
                    GroundOne.WE.dungeonEvent23KeyOpen = true;
                }
            }
        }

        public static void Message10050_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：よし、開けるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.BlueOpenRight);

            if (GroundOne.WE.AvailableSecondCharacter == false)
            {
                messageList.Add("アイン：（ピリピリと殺気を感じるな。コイツはやばいぜ）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（１階ボスだな。この感触、間違いねえ。危ないと思ったらすぐ引き返すか）"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ラナ：特に、変な所は無さそうね。普通の通路と同じね。");

                messageList.Add("アイン：待て、ラナ。下手に進むな。強い殺気を感じる！");

                messageList.Add("ラナ：そう？そろそろボスって所かしら。気をつけて進みましょ。");

                messageList.Add("アイン：ああ、気を引き締めていこうぜ。");
            }

            GroundOne.WE.dungeonEvent28KeyOpen = true;

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic14);
        }

        public static void Message10050_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.AvailableSecondCharacter == false)
            {
                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（やはりおかしい。進んでは行けない気がする。）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（このダンジョンの構成、どうも引っかかる）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（もう少し他を探索するか）"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ちょっと、本当にどうしたのよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ、もう少し他を探索しようぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アインがそう言うんだったら、別に止めはしないわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このダンジョンの構成、どうも引っかかるんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナもう少しだけ探索させてくれ。悪ぃな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ううん、別に。他に何か無いか、探して見ましょ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            GroundOne.WE.dungeonEvent25 = true;

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic14);
        }

        public static void Message10050(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent25)
            {
                if (GroundOne.WE.AvailableSecondCharacter == false)
                {
                    messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                    messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                    messageList.Add("アイン：・・・扉があるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（どうもおかしい、ただならぬ殺気を感じるが）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（ひょっとして１階のボスじゃねえか？この先・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（・・・どうする・・・開けるか・・・開けないべきか・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.DecisionOpenDoor1);
                }
                else
                {
                    messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                    messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                    messageList.Add("アイン：扉・・・か。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：鍵はかかってねえみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：どうしたのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・いや。何でもねえが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナ、お前どう思う？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：何がよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：この扉、トラップとか仕掛けてあると思うか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：今調べてみてるけど、特に無さそうよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そうか・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：一体どうしちゃったのよ。怖気づいたわけ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何かこう・・・ダンジョンの構成がわからねえんだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ダンジョンの構成？どこがわからないのよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや・・・上手く表現できねえが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：納得がいかねえ。そんな感じだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：気にしすぎなんじゃないの？らしくないわね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ、そうかもな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（駄目だ。ラナはこういう所、昔っから鈍感だからな。）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（・・・どうする・・・開けるか・・・開けないべきか・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.DecisionOpenDoor1);
                }
            }
            else if (!GroundOne.WE.dungeonEvent28KeyOpen)
            {
                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.DecisionOpenDoor1);
            }
        }
        public static void Message10051(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：下り階段発見！さっそく降りるとするか？"); eventList.Add(ActionEvent.YesNoGotoDungeon2);
        }

        public static void Message10051_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.GotoDungeon2);

            if (!GroundOne.WE.TruthCompleteArea1)
            {
                messageList.Add("アイン：おし、１階制覇した事だし、一度ユングの町へ戻るとするか。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTown);
            }
        }

        public static void Message10052(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent16)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent16_1NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent16_1NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10053(ref List<string> messageList, ref List<ActionEvent> eventList)
        {

            if (GroundOne.WE.dungeonEvent16)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent16_2NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent16_2NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10054(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent16)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent16_3NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent16_3NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10055(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent16)
            {
                // すでにオープンなら、何も発生させない。
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent16_4NotOpen)
                {
                    messageList.Add("アイン：ん？この扉、鍵がかかってるみたいだな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：鍵穴とかも特に無さそうよ。"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        messageList.Add("アイン：鍵穴も無いみてえだし・・・"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：まあ、しょうがねえ。他のルートを探してみるとするか。"); eventList.Add(ActionEvent.None);
                    GroundOne.WE.dungeonEvent16_4NotOpen = true;
                }
                else
                {
                    messageList.Add("アイン：この扉は開かないな。他のルートでも探そう。"); eventList.Add(ActionEvent.None);
                }
            }
        }

        public static void Message10056(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.Truth_Communication_Dungeon11 && !GroundOne.WE.dungeonEvent31)
            {
                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("アイン：着いたぜ。ここだろ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アイン、相変わらず汗びっしょりね。大丈夫なの？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：大丈夫だ。変な頭痛は相変わらずだが、倒れるほどじゃねえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っさ、行くぞ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っえ、えぇ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：着いた。ここだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（あれから、変な頭痛が取れねえな・・・イツツ・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（こんな所で立ち止まってたら駄目だ）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っよし、行くぜ。"); eventList.Add(ActionEvent.None);
                }

                GroundOne.WE.dungeonEvent31 = true;

            }

        }
        public static void Message10057(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE2.TruthRecollection1 == false)
            {
                GroundOne.WE2.TruthRecollection1 = true;

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.TurnToBlack);

                messageList.Add("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　力こそが全てである ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic15);

                messageList.Add("　　？？？：これは小さい頃から何度も読んでるっての。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　？？？：そんなだから、てめぇはザコアインのままなんだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：俺はザコアインじゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：師匠だって、いつも力押しばっかりじゃねえか。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：あぁ、そうだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：じゃあ、力で押す以外に無いって事じゃないか。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：次、読め。ボケアイン"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　力は力にあらず、力は全てである　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：ここも知ってるって。結局は力って事を言いたいんだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：っち・・・今のザコアインじゃ、まだ早過ぎか。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：最後の文面も、一応読んどけ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　負けられない勝負。　しかし心は満たず。　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　力のみに依存するな。心を対にせよ。　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：・・・読んだぜ？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：もう行くぞ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：何だよ。教えてくれよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：ザコにはザコのままがお似合いだって事さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：俺はザコじゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：とにかく、俺はダンジョンへ行く。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：止めとけ。てめぇには無理だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：何でそう言い切れるんだよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：俺もずいぶんと強くなった。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：今じゃ、DUELでもそれなりの戦歴が残せているし。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：魔物討伐や護衛なんかもかなりハイレベルな内容もやってのけるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：そういう話してんじゃねえ。無理なもんは無理だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ラナ：アイン・・・ダンジョン行くの、止めておいたら？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：ラナ、お前は師匠がいるといっつも師匠の味方だな。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ラナ：そういうわけじゃないけど・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：俺は行く。決めてたんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：っち・・・おい、ちょっと待て。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：何だよ。俺は行くぜ？とめても無駄だからな。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：行く前に一つやってけ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：何をやるんだよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：まさか師匠とDUELで腕試しってわけか。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：違ぇよ。俺様とじゃなくてだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ランディス：そこの小娘と勝負してみろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：っな！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ラナ：わ、わわわ私？？？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：勝敗はどうでもいい。いっぺんココでやってけ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　アイン：っぐ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：どぉした。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：ああ！やってやるさ！ラナ、準備は良いか？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：うわ、っちょっとぐらい待ってよね。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：・・・えーっと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：こんなもんかな。じゃ良いわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：よぉし、じゃあ始め！！"); eventList.Add(ActionEvent.None);

                messageList.Add("    【・・・　・・・　・・・】"); eventList.Add(ActionEvent.None);

                messageList.Add("    【・・・　・・・】"); eventList.Add(ActionEvent.None);

                messageList.Add("    【・・・】"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：勝負あり！"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：いっつつつ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：勝者！ラナ・アミリア！"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：やったわ♪　私のエレメンタルブローが決まったみたいね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：もうちょっとタイミングが合ってれば俺の勝ちだったんだけどな。。。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：っち・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：師匠すまねえ。ギリギリ負けちまったみたいだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：てめぇ、治ってねえな。その性格。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：え？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：自覚、してねえんだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：自覚って何の話だよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：分かんねぇなら、それまでだ。好きにしろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：・・・じゃ、もう良いか？俺は行くぜ、ダンジョンへ！"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：ちょっとアイン・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：あぁ。行って来い。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：え、良いんですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：あぁ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：ラナ、お前も一緒にダンジョン来るか？"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：え・・・う～ん・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    ラナ：ちょっと、考えさせてよ。っね？"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：あ、ああ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜オル・ランディスは少し微笑むと・・・＞"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：ッフ、まぁガンバレや。ザコアイン。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：師匠すまねえ。頑張ってダンジョン制覇してくるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    ランディス：厳しくなったら、引き返せよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("    アイン：あ、あぁ。了解了解！"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add("　　　【アインに対する激しい激痛は少しずつ引いていった。】"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.ReturnToNormal);

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    messageList.Add("ラナ：アイン・・・大丈夫？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ん？おお・・・おぉ、大丈夫だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：随分と壁に向かって止まってたみたいだけど。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ、何て事はねえ。大丈夫だぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：汗・・・引いてるわね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そうか？そういや、変な頭痛も無くなったみたいだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：・・・見たのね？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あれは、おそらく過去の記憶だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・っよし！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：じゃあ、１階のボスを倒しにでも行くか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っちょ、何でいきなりそうなるのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ良いじゃねえか。詳しいことは後だ。ボスを倒してしまおうぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アインって、たまに無理やりよね・・・まあ分かったわ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：すまねえな、いろいろと。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：別に良いわよ。納得が行ってなかったんでしょ？今まで。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、いやいや、そういうわけじゃねえが。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：イイって言ってるじゃない。ホラ、とっとと倒しちゃいましょ♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ！ボス戦、行こうぜ！"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：（・・・頭の頭痛が消えている・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（真実解ってのはおそらく・・・過去の記憶だ）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（だとすれば・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ！１階ボス、目指すとするか！"); eventList.Add(ActionEvent.None);
                }

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic14);
            }
        }
            
    }
}

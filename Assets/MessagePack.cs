using System;
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
            PlayMusic01,
            PlayMusic02,
            PlayMusic03,
            PlayMusic11,
            PlayMusic14,
            PlayMusic15,
            PlayMusic16,
            YesNoGotoDungeon2,
            GotoHomeTown,
            GotoDungeon2,
            DecisionOpenDoor1,
            HomeTownBlackOut,
            HomeTownTurnToNormal,
            HomeTownNight,
            HomeTownCallRestInn,
            HomeTownAvailableDuel,
            GetGreenPotionForLana,
            CallSomeMessageWithAnimation,
            CallSomeMessageWithNotJoinLana,
            ResurrectHalfLife,
        }


        #region "ダンジョン内"
        public static void MessageBackToTown(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ユングの町に戻るか？"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTown);
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

                        messageList.Add("アイン：（駄目だ・・・ラナが許してくれそうにもねえ・・・）"); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationRight); eventList.Add(ActionEvent.None);

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

                                messageList.Add("アイン：（駄目だ・・・ラナが許してくれそうにもねえ・・・）"); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationRight); eventList.Add(ActionEvent.None);

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

                                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationRight); eventList.Add(ActionEvent.None);
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
                messageList.Add("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】"); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.TurnToBlack); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.ReturnToNormal); eventList.Add(ActionEvent.None);

                GroundOne.WE.dungeonEvent27 = true;

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.HomeTown); eventList.Add(ActionEvent.None);
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

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.EncountFlansis); eventList.Add(ActionEvent.None);
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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenLeft); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenLeft); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BigEntranceOpen); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent12KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenTop); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BigEntranceOpen); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenLeft); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent13KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenLeft); eventList.Add(ActionEvent.None);


                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent14KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BigEntranceOpen); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationBottom); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenBottom); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうやら、でかい広間に出たみたいだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へぇ・・・遠くまで見渡せる感じだな。良い造りしてんじゃねえか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おっと、見とれてる場合じゃねえ。探索探索っと！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent14KeyOpen = true;
                }
                else
                {
                    messageList.Add("アイン：おし、扉が見えてきたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationBottom); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ここを開ければ、大広間に繋がってるはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenBottom); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.dungeonEvent11KeyOpen && GroundOne.WE.dungeonEvent12KeyOpen && GroundOne.WE.dungeonEvent13KeyOpen)
                    {
                        messageList.Add("アイン：おし、これで４つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BigEntranceOpen); eventList.Add(ActionEvent.None);

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
                    messageList.Add("アイン：おっと、何だこりゃ。扉か？"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateUnknownTile); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateUnknownTile); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen1); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen1); eventList.Add(ActionEvent.None);

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

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.CenterBlueOpen); eventList.Add(ActionEvent.None);
                        
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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen1); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationTop); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen1); eventList.Add(ActionEvent.None);

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

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.CenterBlueOpen); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen2); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen2); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.UpdateLocationLeft); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.SmallEntranceOpen2); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent22KeyOpen)
                    {
                        messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("    『ゴゴゴォ・・・ゴオオォォン！！』"); eventList.Add(ActionEvent.None);

                        messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.CenterBlueOpen); eventList.Add(ActionEvent.None);

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
            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よし、開けるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.BlueOpenRight); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.AvailableSecondCharacter == false)
            {
                messageList.Add("アイン：（ピリピリと殺気を感じるな。コイツはやばいぜ）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（１階ボスだな。この感触、間違いねえ。危ないと思ったらすぐ引き返すか）"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ラナ：特に、変な所は無さそうね。普通の通路と同じね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：待て、ラナ。下手に進むな。強い殺気を感じる！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そう？そろそろボスって所かしら。気をつけて進みましょ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、気を引き締めていこうぜ。"); eventList.Add(ActionEvent.None);
            }

            GroundOne.WE.dungeonEvent28KeyOpen = true;

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic14); eventList.Add(ActionEvent.None);
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

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic14); eventList.Add(ActionEvent.None);
        }

        public static void Message10050(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent25)
            {
                if (GroundOne.WE.AvailableSecondCharacter == false)
                {
                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic16); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・扉があるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（どうもおかしい、ただならぬ殺気を感じるが）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（ひょっとして１階のボスじゃねえか？この先・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（・・・どうする・・・開けるか・・・開けないべきか・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.DecisionOpenDoor1); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic16); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.DecisionOpenDoor1); eventList.Add(ActionEvent.None);
                }
            }
            else if (!GroundOne.WE.dungeonEvent28KeyOpen)
            {
                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic16); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.DecisionOpenDoor1); eventList.Add(ActionEvent.None);
            }
        }
        public static void Message10051(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：下り階段発見！さっそく降りるとするか？"); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.YesNoGotoDungeon2); eventList.Add(ActionEvent.None);
        }

        public static void Message10051_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.GotoDungeon2); eventList.Add(ActionEvent.None);

            if (!GroundOne.WE.TruthCompleteArea1)
            {
                messageList.Add("アイン：おし、１階制覇した事だし、一度ユングの町へ戻るとするか。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.GotoHomeTown); eventList.Add(ActionEvent.None);
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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.TurnToBlack); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　力こそが全てである ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic15); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.StopMusic); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【アインに対する激しい激痛は少しずつ引いていった。】"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.ReturnToNormal); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.None); eventList.Add(ActionEvent.PlayMusic14); eventList.Add(ActionEvent.None);
            }
        }
        #endregion

        #region "ホームタウン"
        #region "帰還時の自動蘇生"
        public static void HomeTownResurrect(ref List<string> messageList, ref List<ActionEvent> eventList, MainCharacter player)
        {
        	messageList.Add("ダンジョンゲートから不思議な光が" + player.Name + "へと流れ込む。"); eventList.Add(ActionEvent.None);
        	
        	messageList.Add(""); eventList.Add(ActionEvent.ResurrectHalfLife); // todo (MC,SC,TCをどうやって情報を渡せるか？)
        	
        	messageList.Add(player.Name + "は命を吹き返した。"); eventList.Add(ActionEvent.None);
        }
        #endregion
        #region "ホームタウン表示時"
        // 後編初日
        public static void Message20100(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：・・・ここは・・・っ・・・いつつ・・・"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：っ・・・いつつ・・・"); eventList.Add(ActionEvent.None);
            }

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：今、何時ぐらいだ？"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：今・・・何時だ？"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("        『アインは宿屋の寝床から起き上がった。』"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：朝７時ぐらいか。まあ、ちょうど良いぐらいの時間だな。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：朝の６時か・・・起きるには少し早いぐらいだな。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：・・・ん？何か床に落ちてるな。"); eventList.Add(ActionEvent.None);

            // todo
            //using (MessageDisplay md = new MessageDisplay())
            //{
            //    md.Message = "【ラナのイヤリング】を手に入れました。";
            //    GroundOne.playbackMessage.Insert(0, md.Message);
            //    GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.notify);
            //    md.StartPosition = FormStartPosition.CenterParent;
            //    md.ShowDialog();
            //}

            // GetItemFullCheck(GroundOne.MC, Database.RARE_EARRING_OF_LANA); // todo

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：ラナのイヤリングじゃねえか・・・何でこんな物が・・・"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：何だ、ラナのやつ。何でまたこんな所に落としてるんだ。"); eventList.Add(ActionEvent.None);
            }

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：・・・　何であるんだっけ　・・・　ラナが落としたのか？"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);
            }

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：いいや、そんなワケ無えよな・・・じゃあ何でだ・・・"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：しょうがねえ。後で渡しに行ってやるか。"); eventList.Add(ActionEvent.None);
            }

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：まあいいか。とりあえず、目覚めたわけだし、町にでも出てみるか！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：おっしゃ、せっかく目覚めたわけだし、町にでも出てみるか。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownTurnToNormal);

            messageList.Add("アイン：さて、何すっかな。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationFirstHomeTown = true;
            GroundOne.WE.AlreadyRest = true; // 朝起きたときからスタートとする。

            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }
        
        // 看板「始まりの地」を見たとき
        public static void Message20101(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ラナ、お前｛べからず｝って意味知ってるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何よ、突然そんなこと聞いて。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：「～してはいけない。」って事。つまり、やっちゃいけないって事じゃないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうか・・・となると・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやしかし・・・どういう意味だ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っちょ、なに考えちゃってるのよ。ちゃんと言いなさいよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っあ、ああ、看板があったんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("『始まりの地、見落とすべからず。』　ってな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：言葉通りじゃない。「始まりの場所を見落とさないようにしなさい」って意味よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よくわかんねえんだよな。これが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何かひっかかりでもあるわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、特にねえけどさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ何よ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うーん、なんて言うんだ。捉え所が掴みにくいと思ってさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ１階の始めなんだし、冒険者への最初の警告って所じゃないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うーん・・・なんて言うんだ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ッハイ、ポーションでもどうぞ♪"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.GetGreenPotionForLana);

            messageList.Add("アイン：っお！？悪いな、わざわざ。代金はいくらだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：良いわよ、そんなの。とっときなさいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、すまねえな。サンキュー！"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE2.TruthBadEnd1)
            {
                messageList.Add("アイン：・・・なあ、ラナ。ところで話は変わるんだが。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：なによ？"); eventList.Add(ActionEvent.None);

                //using (TruthDecision td = new TruthDecision())
                {
                    //td.MainMessage = "　【　ラナをパーティに誘いますか？　】";
                    //td.FirstMessage = "ラナをパーティに誘う。";
                    //td.SecondMessage = "ラナをパーティに誘わない。";
                    //td.StartPosition = FormStartPosition.CenterParent;
                    //td.ShowDialog(); eventList.Add(ActionEvent.None);
                    //if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        messageList.Add("アイン：ダンジョン、一緒に来ないか？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：う～ん、どうしようかな。"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：お前が居るとポーションとかダンジョンマップが頼りになるからな。っな、頼むぜ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：条件があるわね。聞いてもらえるかしら♪"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：っお、何だよ？言ってみろよ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：【　真実の答え　】　　探してよね？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：っな。何だって？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                        if (GroundOne.WE.AvailablePotionshop)
                        {
                            messageList.Add("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。"); eventList.Add(ActionEvent.None);

                            messageList.Add("ラナ：そんなわけだから、心配ご無用よ♪"); eventList.Add(ActionEvent.None);
                        }

                        messageList.Add("アイン：じゃあ、明日からよろしく頼むぜ！！"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：ハイハイ♪　じゃあまた明日ね。"); eventList.Add(ActionEvent.None);

                        messageList.Add(" "); eventList.Add(ActionEvent.CallSomeMessageWithAnimation); // todo 【ラナがパーティに加わりました。】

                        GroundOne.WE.AvailableSecondCharacter = true;
                        GroundOne.WE.Truth_CommunicationJoinPartyLana = true;
                    }
                    // after revive
                    //else
                    //{
                    //    messageList.Add("アイン：い、いやいや、何でもねえ。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("ラナ：アイン、らしくないわね。言いたい事は正直に言ってよね？"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("アイン：・・・ああ、正直言うとだな。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("アイン：ラナ、お前をパーティに誘おうと思ったんだが。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("アイン：やめた。俺一人で行ってみせるぜ。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("アイン：すまねえな。ラナ。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("ラナ：う～ん、別に良いわよ。ちゃんとそう言ってくれれば。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("アイン：じゃあ、また明日からダンジョンいって来るぜ。"); eventList.Add(ActionEvent.None);

                    //    messageList.Add("ラナ：ハイハイ、頑張ってきてよね♪"); eventList.Add(ActionEvent.None);

                    //    messageList.Add(""); eventList.Add(ActionEvent.CallSomeMessageWithNotJoinLana); // todo 【ラナをパーティに加えませんでした。】

                    //    GroundOne.WE.Truth_CommunicationNotJoinLana = true;
                    //}
                }
            }
            else
            {
                messageList.Add("アイン：・・・なあ、ラナ。ポーションもらったトコ悪いんだが。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：なによ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ダンジョン、一緒に来ないか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：う～ん、どうしようかな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：お前が居るとポーションとかダンジョンマップが頼りになるからな。っな、頼むぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：条件があるわね。聞いてもらえるかしら♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っお、何だよ？言ってみろよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：【　真実の答え　】　　探してよね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っな。何だって？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailablePotionshop)
                {
                    messageList.Add("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：そんなわけだから、心配ご無用よ♪"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("アイン：じゃあ、明日からよろしく頼むぜ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ハイハイ♪　じゃあまた明日ね。"); eventList.Add(ActionEvent.None);

                messageList.Add(" "); eventList.Add(ActionEvent.CallSomeMessageWithAnimation); // todo 【ラナがパーティに加わりました。】

                GroundOne.WE.AvailableSecondCharacter = true;
                GroundOne.WE.Truth_CommunicationJoinPartyLana = true;
            }
        }
        
        // 看板３を見る前でも、大広間に到達した時
        public static void Message20102(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ふう、戻ってきたのは良いが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの大広間、扉だらけで分かったもんじゃねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っくそ・・・こんな時にラナのダンジョンマップがあればな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私のダンジョンマップがどうかしたの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！？　ビックリするじゃねえか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何そんなビビってるわけ？まさか、また隠し事じゃないでしょうね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや。隠してるわけじゃねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でもまあ、そんなわけだ。気にするな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふうん・・・っじゃ、こっちも内緒で♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな！何が内緒なんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ダンジョンマップがどうとか、言ってたじゃない？ちゃんと言いなさいよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　まあ、アレだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッシュゴオオォォ！！』（ラナのエレメンタルキックがアインの胴体に炸裂）　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わわ、分かったって。待て待て。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ダンジョンを進めてる途中、大きな大広間に出くわしたんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：へえ、そんな所があるんだ。っで、どうだったわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：大広間には幾つもの扉があるんだが、これがほとんど鍵付きばかり。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おそらく、違う道筋をたどってくれば、開けるとは思うんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何せ、マップがよくわかんねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：簡単に言えば、マップがよくわかんねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：結論として、マップがよくわかんねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なるほど♪　私、良い事思いついちゃった♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、別にお前に来てくれとは言ってねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マップを書いてくれさえすれば良いんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも、ダンジョンに一緒に行かないとマップは書けないでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いいや、来なくても良い。俺がトランシーバーでラナと通信を行う。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『こちらアイン。ただいま座標ポイント『２２，３３』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（声マネ）『こちらラナ。了解よ♪　マップ更新しといたわ』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『こちらアイン。ただいま座標ポイント『３４，２２』　宝箱を発見！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（声マネ）『こちらラナ。了解よ♪　マップ更新しといたわ』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『こちらアイン・・・ただいま・・・』"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・変な裏声出さないでよね。それ、全然似てないから。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そもそも何でそんな面倒な通信作業しなきゃ行けないのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っほら、こう何かいかにもダンジョン探索を進めてるって感じがするだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そもそもトランシーバーなんてもの、ダンジョンで使えるわけ無いでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ！　っば、そんな馬鹿な！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：はあぁぁ・・・・何でこんなバカ話してるのかしら・・・私、行くわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、待て待て！相談だ、ラナ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：一緒にダンジョン行かねえか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：条件があるわね。聞いてもらえるかしら♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っお、何だよ？言ってみろよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：【　真実の答え　】　　探してよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな。何だって？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.AvailablePotionshop)
            {
                messageList.Add("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そんなわけだから、心配ご無用よ♪"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：じゃあ、明日からよろしく頼むぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ハイハイ♪　じゃあまた明日ね。"); eventList.Add(ActionEvent.None);

            messageList.Add(" "); eventList.Add(ActionEvent.CallSomeMessageWithAnimation); // todo 【ラナがパーティに加わりました。】

            GroundOne.WE.AvailableSecondCharacter = true;
            GroundOne.WE.Truth_CommunicationJoinPartyLana = true;
        }

        // 看板「メンバー構成で変化」を見たとき
        public static void Message20103(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：パーティによってメンバー構成は・・・変化する・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：どうしたのよ？難しい顔して一人でブツブツ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、ラナか。ちょうど良かった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、一緒にダンジョンこねえか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・え？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうやら、あのダンジョンはメンバー構成によって変化するらしいんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っちょ、ちょっと待ってよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：唐突よね。いきなりどうしちゃったわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、別に深い経緯はねえが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんだ、駄目なのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、そういうわけじゃないけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：行こうぜ、ラナ。２人で行くのにデメリットは無いだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあそうだけどね。私、本当に行っても良いの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、当然さ。お前が居ると頼りになるからな。っな、頼むぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：条件があるわね。聞いてもらえるかしら♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っお、何だよ？言ってみろよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：【　真実の答え　】　　探してよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな。何だって？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.AvailablePotionshop)
            {
                messageList.Add("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そんなわけだから、心配ご無用よ♪"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：じゃあ、明日からよろしく頼むぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ハイハイ♪　じゃあまた明日ね。"); eventList.Add(ActionEvent.None);

            messageList.Add(" "); eventList.Add(ActionEvent.CallSomeMessageWithAnimation); // todo 【ラナがパーティに加わりました。】

            GroundOne.WE.AvailableSecondCharacter = true;
            GroundOne.WE.Truth_CommunicationJoinPartyLana = true;
        }

        // 看板「くまなく探せ」を見たとき
        public static void Message20104(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ラナ、｛くまなく｝って意味を教えてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインはくまなくバカ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：待て待て。意味を教えてくれって言ってるだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：言葉どおりよ、アインは隅々まで余すとこなくバカって意味よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほどな・・・なるほど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なるほどって・・・納得されても困るんだけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：また、看板があったんだ。内容はこうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("『くまなく、探したか？』　っと。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なるほど。隅々まで探してみたか？と言われてるみたいね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っで、隅々まで探してみたわけ？"); eventList.Add(ActionEvent.None);

            if (!GroundOne.WE.BeforeSpecialInfo1)
            {
                messageList.Add("アイン：ああ！もちろん全部探したぜ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それも、くまなく隅々まで全部な！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：言葉ダブってるわよ。ホンットバカよね。ハアアァァァ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ねえ、こう何か見落としとかあるんじゃないの？ちゃんと探してみた？"); eventList.Add(ActionEvent.None);

                if (!GroundOne.WE.TruthSpecialInfo1)
                {
                    messageList.Add("アイン：ん？まあ隅々まで全部って言われてもな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そもそも、最下層制覇が目的だろ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：でも最下層に行くための必要な情報があるかも知れないわよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナ、何でまたお前は、そう分かったような言い方をしてるんだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おまえ・・・ひょっとして何か知ってるのか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っべ、っべべべ別に知らないわよ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：分かった、分かったって。そんな動揺すんなって。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、気が向いたらいろいろ探索してみるとするさ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：看板にも書いてある事だし、探索して損はしないはずよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：オーケー、気になる所はまた探索するさ。サンキューな。"); eventList.Add(ActionEvent.None);

                    // [真エンディング分岐]

                    messageList.Add("アイン：・・・なあ、ラナ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：なによ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺だけじゃ、看板の意図が掴めねえ時もある、そこでだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダンジョン、一緒に来ないか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：う～ん、どうしようかな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：お前が居ると頼りになるからな。っな、頼むぜ。"); eventList.Add(ActionEvent.None);

                    //messageList.Add("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】"); eventList.Add(ActionEvent.None);
                    
                    messageList.Add("　　　【　ラナはちょっと考え込むそぶりで、遠くを見るような笑顔をした　】"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　【　それは一瞬のことであり、アインにとってその表情は分からなかった　】"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：条件があるわね。聞いてもらえるかしら♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っお、何だよ？言ってみろよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：【　真実の答え　】　　探してよね？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っな。何だって？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailablePotionshop)
                    {
                        messageList.Add("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：そんなわけだから、心配ご無用よ♪"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：じゃあ、明日からよろしく頼むぜ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ハイハイ♪　じゃあまた明日ね。"); eventList.Add(ActionEvent.None);

                    messageList.Add(" "); eventList.Add(ActionEvent.CallSomeMessageWithAnimation); // todo 【ラナがパーティに加わりました。】

                    GroundOne.WE.AvailableSecondCharacter = true;
                    GroundOne.WE.Truth_CommunicationJoinPartyLana = true;
                }
                else
                {
                    messageList.Add("アイン：【力は力にあらず、力は全てである。】"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っえ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それから・・・【負けられない勝負。　しかし心は満たず。】"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：最後は　【力のみに依存するな。心を対にせよ。】　だったかな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ウソ！？そんなのちゃんと覚えてるの！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：覚えてるというか、思い出した。何かダンジョン内でそんな言葉が出てきたな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだろ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はとくにあの７番目が好きだったしな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：私はよく分かんないけどね、ああいう類のだけは。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アインを殴ると、アインが吹っ飛ぶ。これで十分よね♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：お前のそういう所は何とか治らねえのかよ・・・でもまあ"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（アインはいつになく、真剣な眼差しを見せ始め・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：このダンジョン。少し読めたぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：え？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：下手に進んだら駄目なんだ。このダンジョン。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：どういう意味よ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：この言葉で思い出したことがある。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：何を思い出したの？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：神剣フェルトゥーシュに関してだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（ラナはほんの一瞬だけ、顔を横に逸らしてから・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：フェルトゥーシュがどうしたのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：突き刺された者、純粋な力による死を迎える"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ヒーリング効果が適用されず、蘇生魔法も効かない。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まさに純粋な力そのものだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、俺が思い出したのはそんな事じゃねえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナ、お前が俺に最初にくれた剣。あれが、フェルトゥーシュだろ？。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：・・・いつ頃から気づいてたのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ボケ師匠ランディスに出くわした時だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：そうだったの。それからは、気づかない振りしてたの？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：師匠はどうもあの剣の特性に関して、もう一つ何か知ってるみたいなんだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：心を燈して放たないと、攻撃力は出ない。何かそんな話だった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：心を燈して・・・って事は。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それが分かった時点で、俺の力に対する考えは変わった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ねえ、アイン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ダンジョン、このまま進められる？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・ああ。俺はこのまま進める。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はどうやら、いろいろと忘れてしまってるようだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それを思い出さなきゃならねえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダンジョンをくまなく探索すれば、思い出すべき事が見つかる。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：このダンジョン、どうやら何か他の解き方があるみたいだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はそれを見つけてみせる。必ずな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っそ。何か安心しちゃったわ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アイン、１階制覇のほう、頑張って来てよね♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おお、任せておけ。１階制覇できたら、連絡するからな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：頼んだわよ。１階制覇の時は、お宝どっさり持ってきてもらうから♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：マジかよ。お宝没収かよ・・・、ラナ様にお貢物ってワケかよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っふふふ、ウソよウソ。何まじめに受けちゃってるのよ♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、何か良いものあったら持ってくるよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：じゃあ、１階制覇！やってくるとするか！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：楽しみにしてるわよ。じゃあ、また明日ね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ、またな。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.CompleteTruth1 = true;
                }
            }
            else
            {
                messageList.Add("アイン：【力は力にあらず、力は全てである。】"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っえ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それから・・・【負けられない勝負。　しかし心は満たず。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：最後は　【力のみに依存するな。心を対にせよ。】　だったかな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ウソ！？そんなのちゃんと覚えてるの！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：覚えてるというか、思い出した。何かダンジョン内でそんな言葉が出てきたな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はとくにあの７番目が好きだったしな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：私はよく分かんないけどね、ああいう類のだけは。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アインを殴ると、アインが吹っ飛ぶ。これで十分よね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：お前のそういう所は何とか治らねえのかよ・・・でもまあ"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　（アインはいつになく、真剣な眼差しを見せ始め・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このダンジョン。少し読めたぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：え？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：下手に進んだら駄目なんだ。このダンジョン。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：どういう意味よ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：この言葉で思い出したことがある。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何を思い出したの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：神剣フェルトゥーシュに関してだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　（ラナはほんの一瞬だけ、顔を横に逸らしてから・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：フェルトゥーシュがどうしたのよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：突き刺された者、純粋な力による死を迎える"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ヒーリング効果が適用されず、蘇生魔法も効かない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まさに純粋な力そのものだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だが、俺が思い出したのはそんな事じゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ、お前が俺に最初にくれた剣。あれが、フェルトゥーシュだろ？。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：・・・いつ頃から気づいてたのよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ボケ師匠ランディスに出くわした時だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そうだったの。それからは、気づかない振りしてたの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：師匠はどうもあの剣の特性に関して、もう一つ何かを知ってるみたいなんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：心を燈して放たないと、攻撃力は出ない。何かそんな話だった。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：心を燈して・・・って事は。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それが分かった時点で、俺の力に対する考えは変わった。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ねえ、アイン"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ダンジョン、このまま進められる？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ああ。俺はこのまま進める。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はどうやら、いろいろと忘れてしまってるようだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それを思い出さなきゃならねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ダンジョンをくまなく探索すれば、思い出すべき事が見つかる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このダンジョン、どうやら何か他の解き方があるみたいだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はそれを見つけてみせる。必ずな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っそ。何か安心しちゃったわ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アイン、１階制覇のほう、頑張って来てよね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、任せておけ。１階制覇できたら、連絡するからな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：頼んだわよ。１階制覇の時は、お宝どっさり持ってきてもらうから♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：マジかよ。お宝没収かよ・・・、ラナ様にお貢物ってワケかよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っふふふ、ウソよウソ。何まじめに受けちゃってるのよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、何か良いものあったら持ってくるよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃあ、１階制覇！やってくるとするか！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：楽しみにしてるわよ。じゃあ、また明日ね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、またな。"); eventList.Add(ActionEvent.None);

                GroundOne.WE.CompleteTruth1 = true;
                // 固定メンバーでストーリ１本かどうか・・・どうする！？
            }

            GroundOne.WE.AlreadyCommunicate = true;
        }

        // １階看板最後の情報を入手したとき
        public static void Message20105(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
             messageList.Add("アイン：いっつつつ・・・すまねえな。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：別に良いわよ。でも、本当に大丈夫？"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：ああ、何とかな。大丈夫だ、少しひいてきた。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：汗も少しひいたみたいね。"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：一体何なんだ、あの看板は。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：座標地点を指し示していたみたいだけど？"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：＜始まりの地にて＞　・・・か。"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：そうか。『始まりの地、見落とすべからず。』って看板もあったよな。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：きっとコレの事を示していたのね。"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：ラナ、『４７　２９』と言ったらどの辺になるんだ？"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：おそらくだけどこの数字は座標ポイントＸとＹを示してるのよ。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：Ｘは左右、Ｙを上下とすると、Ｘ方向へ４７、Ｙ方向へ２７。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：つまり、右下のこの辺りを指し示してる事になるわ。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：ちゃんと印付けておいたから。忘れることは無いと思うわ。"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：明日になったら、行ってみるとするか。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：うん、そうね。今日はもう休みましょう。"); eventList.Add(ActionEvent.None);

             messageList.Add("アイン：ああ。それじゃ、ハンナ叔母さんの宿屋へ行こうぜ。"); eventList.Add(ActionEvent.None);

             messageList.Add("ラナ：了解よ。"); eventList.Add(ActionEvent.None);

             GroundOne.WE.Truth_Communication_Dungeon11 = true;
        }

        // １階制覇
        public static void Message20106(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
             if (GroundOne.WE.AvailableSecondCharacter)
             {
                 messageList.Add("アイン：っしゃ！やったぜ！ラナ！"); eventList.Add(ActionEvent.None);

                 messageList.Add("ラナ：上出来なんじゃない♪"); eventList.Add(ActionEvent.None);

                 messageList.Add("アイン：ラナ！お前はやっぱ最高のパートナーだぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

                 messageList.Add("ラナ：また、そんな浮かれてちゃって。ホラホラ、町の住人達に報告しにいきましょ。"); eventList.Add(ActionEvent.None);

                 messageList.Add("アイン：あ、ああ、そうだな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

                 // todo
                 //using (MessageDisplay md = new MessageDisplay())
                 //{
                 //    md.StartPosition = FormStartPosition.CenterParent;
                 //    md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                 //    md.ShowDialog();
                 //    md.Message = "その日の夜、ハンナの宿屋亭にて";
                 //    md.ShowDialog();
                 //}

                 messageList.Add(""); eventList.Add(ActionEvent.StopMusic);
                 messageList.Add(""); eventList.Add(ActionEvent.PlayMusic15);

                 messageList.Add("アイン：ここで、ボスが例の必殺技を出してくる瞬間にだな、ズバズバっと！"); eventList.Add(ActionEvent.None);

                 messageList.Add("ラナ：何言ってんのよ。誰が隙を作ってあげたと思ってるのよ？"); eventList.Add(ActionEvent.None);

                 messageList.Add("アイン：いやいや、そうだったな。サンキューサンキュー！"); eventList.Add(ActionEvent.None);

                 // ラナのイヤリングを渡してしまっていた場合、かつ
                 // 真実解の部屋へ到達していない場合、BADEND
                 if ((GroundOne.WE.Truth_GiveLanaEarring) &&
                     (!GroundOne.WE2.TruthRecollection1))
                 {
                     messageList.Add("ハンナ：おやおや、アイン。バカに騒いでるようだね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：聞いてくれよ、おばちゃん。来たんだよな、ココで・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：典型のヒラメキがな！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ッハッハッハッハッハ！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：まあ典型的なバカよね・・・ハアアァァァ・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：アイン、そろそろ私は部屋に戻って一旦休息するわね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ん？あぁ！了解了解！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ハンナ叔母さん。どうもごちそう様でした♪"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：あいよ、また明日も頑張るんだね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ハイ、それでは失礼します。"); eventList.Add(ActionEvent.None);

                     // todo
                     //using (MessageDisplay md = new MessageDisplay())
                     //{
                     //    md.StartPosition = FormStartPosition.CenterParent;
                     //    md.Message = "ラナは自分が予約していた部屋へ歩いていった。";
                     //    md.ShowDialog(); eventList.Add(ActionEvent.None);
                     //}

                     messageList.Add("アイン：はあ～食った食った。満足だ。おばちゃん、ありがと！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：よく食べたね。明日に差し支えないようにするんだよ。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：はい！！ありがとっした！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：アイン。そういえば、部屋に何か落ちていなかったかい？"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・っはい？"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：イヤリング、部屋に何か落ちていなかったかい？"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・っあ、ああ。あれなら・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ラナに返しておきましたよ。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：確かアイツがいつも付けてたイヤリングですから。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：そうかい。まあ、無くさないように伝えておくんだよ。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：え～っと、分かりました！じゃ、ごちそう様でした！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：はいよ、明日もあるだろう。ゆっくりと休みな。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ありがとうございます、失礼します！"); eventList.Add(ActionEvent.None);

                     messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                     //ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT); eventList.Add(ActionEvent.None); // todo

                     // todo
                     //using (MessageDisplay md = new MessageDisplay())
                     //{
                     //    md.StartPosition = FormStartPosition.CenterParent;
                     //    md.Message = "アインが予約していた部屋にて";
                     //    md.ShowDialog(); eventList.Add(ActionEvent.None);
                     //}

                     messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：明日から２階か・・・っしゃ！気合入れるぜ！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・　なんだろう　・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ラナと一緒にダンジョンへ進んで・・・それから・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                     messageList.Add("アイン：・・・　何か忘れてる気がする　・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：俺、何やってたんだっけ・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：まあいいや、ひとまずダンジョンの最下層へ・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add(" ～　THE　END　～　（虚構へ）"); eventList.Add(ActionEvent.None);

                     GroundOne.WE2.TruthBadEnd1 = true;
                 }
                 // それ以外はGOOD
                 else
                 {
                     messageList.Add("ハンナ：おやおや、アイン。バカに騒いでるようだね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：聞いてくれよ、おばちゃん。来たんだよな、ココで・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：天啓のヒラメキがな！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ッハッハッハッハッハ！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：たまたま突き刺した部分がボスの急所だっただけでしょ？"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：狙ってやったに決まってるだろ？"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ホンットてきとーなんだから・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：まあまあ、良いじゃないかラナちゃん。上手く進めたようだしね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ええ、そうですね。たまにはバカも本来のバカに戻って嬉しいでしょうし♪"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ああ、バカで結構！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：ッハッハッハッハッハ！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ハアアァァァ・・・・大丈夫なのかしら、こんな感じで・・・"); eventList.Add(ActionEvent.None);

                     if (!GroundOne.WE.Truth_GiveLanaEarring)
                     {
                         messageList.Add("アイン：っと、そうだ！忘れてたぜ！"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：っな、何よ突然どうしたのよ？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ラナ、悪ぃ。一つ謝らせてくれ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：何がよ？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：実は、コレなんだが・・・"); eventList.Add(ActionEvent.None);

                         //using (MessageDisplay md = new MessageDisplay())
                         //{
                         //    md.StartPosition = FormStartPosition.CenterParent;
                         //    md.Message = "アインは『ラナのイヤリング』をポケットから取り出した。";
                         //    md.ShowDialog(); eventList.Add(ActionEvent.None);
                         //}
                         GroundOne.MC.DeleteBackPack(new ItemBackPack("ラナのイヤリング")); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：っそ！ソレって！！"); eventList.Add(ActionEvent.None);

                         messageList.Add("ハンナ：おやおや・・・ひょっとしてラナちゃんのアクセサリかい？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：いや、いやいやいやいや！"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：った、たまたま俺の部屋に何故か転がってたんだって！"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：悪かった悪かった悪かった！っな！？"); eventList.Add(ActionEvent.None);

                         //using (MessageDisplay md = new MessageDisplay())
                         //{
                         //    md.StartPosition = FormStartPosition.CenterParent;
                         //    md.Message = "ラナは驚きと戸惑いの表情を隠せないでいる・・・";
                         //    md.ShowDialog();
                         //    md.Message = "・・・　数秒後　・・・";
                         //    md.ShowDialog();
                         //}

                         messageList.Add("ラナ：・・・　・・・　・・・っど・・・"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：怒髪天アルティメットブローとか勘弁な！？っな！？"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：どっ・・・どうも、ありがと。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：・・・ッハ？"); eventList.Add(ActionEvent.None);

                         messageList.Add("ハンナ：アッハハハハハ。よかったじゃないか。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ハンナ：ホラ！こうなったら、アインも存分に謝るんだね。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：っお、おお、悪かったな！いや、ホント悪かった！"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：別に良いわよ。気にしてないから♪"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：っそ、そうか。なら良いんだが・・・とにかく悪かったな。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：良いって言ってるじゃない♪　済んだ事だし。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：ところで、コレどこに落ちてたのよ？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：さっきも言ったと思うが、俺の部屋だ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：" + GroundOne.WE.GameDay.ToString() + "日ぐらい前だったかな。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：朝ふと起きるとさ、ベッドの横に転がってたんだよ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：へえ、そんな所に落ちてたんだ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：そんな所って、じゃあどこに落ちてたと思ったんだよ？"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：っべべべ、別に知らないわよ！！そんなの！！"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：うわわ、っ分かったって。そんなビビんなくて良いだろうが。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：っまったく・・・っあ、そうだ。もう一個聞いて良い？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ん？ああ、何個でも聞いてくれ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：何で最初見つけたとき、渡してくれなかったの？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：何て言ったら良いんだろうな。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：よく考えてみたかったんだ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ハンナ：・・・"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ラナのイヤリング、最初見た時さ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：どことなくだが、よく理解できなかったんだよ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：部屋に落ちてたとか、そういう表面的な事じゃなく。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：何でコレがあるんだっけ・・・"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：どうして落ちてるんだっけ・・・とか"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：そもそも何時からあったんだ・・・？とかな"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：何となくそういう所が、どうしても思い出せねえんだよ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ラナに渡すと、何かこのもやもやした感じがすぐ消えてしまいそうでさ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：それでついつい、渡すのが遅れてしまった、ってワケさ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("ラナ：・・・何か・・・思い出せたの？"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：・・・　・・・　"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ああ、思い出せたぜ。"); eventList.Add(ActionEvent.None);
                     }

                     messageList.Add("ハンナ：アイン、ラナちゃん。そろそろ店じまいだよ。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：あ！そ、そうね。もうこんな時間じゃない。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ハンナおばさん、ごちそうさまでした♪"); eventList.Add(ActionEvent.None);

                     messageList.Add("ハンナ：アインもゆっくりと休むんだね。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：え？あ、あぁ、ありがとうございます。ごちそうさまでした！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：んじゃあ、また明日な。ラナ。"); eventList.Add(ActionEvent.None);

                     messageList.Add("ラナ：ええ、明日から２階ね。この調子で進みましょう。"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：そうだな！じゃあ、またな！"); eventList.Add(ActionEvent.None);

                     messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                     // todo
                     //ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT); eventList.Add(ActionEvent.None); // todo
                     //using (MessageDisplay md = new MessageDisplay())
                     //{
                     //    md.StartPosition = FormStartPosition.CenterParent;
                     //    md.Message = "アインが予約していた部屋にて";
                     //    md.ShowDialog();
                     //}

                     messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：明日から２階か・・・っしゃ！気合入れるぜ！！"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                     messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                     if (GroundOne.WE2.TruthRecollection1)
                     {
                         messageList.Add("アイン：俺はダンジョンへ行こうとしていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：師匠は無理だって言ってたが、俺には出来る気がしていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：魔物討伐は楽々こなせていたし"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：DUELに関しても、かなり上位にランクイン出来ていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：何より、自分自身がようやく強いと感じられるようになっていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ユング街のダンジョン。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：いわゆる「神の試練」ってのが待ち構えているらしい"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：冗談じゃない。俺は「神」とかいう類が大嫌いだ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：神とか名の付く物には、決まってウラがある。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：絶対に正体を暴いてやる。そして、"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：このダンジョンの最下層まで絶対に辿りついてみせる。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：最初はそう考えていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：１階までは何の苦労も無くクリアすることが出来ていた。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：不安要素なんてのは一つも無かった。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：強いて言えば、ラナと一緒にダンジョンへ向かう事になったぐらいだ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ラナは普段の動き自体は良い。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ただ、ラナはいざと言う時に硬直してしまう場合がある。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：まあそん時は、俺がとっさにカバーに入れば良いだけの話。大丈夫だ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：不安要素と呼ぶにはあまりにも小さすぎる不安だ。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ラナと俺は、今までも良く連携を組んでやってきている。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：お互いの事は知り尽くしている。"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：１階ボスには若干手間取ったものの"); eventList.Add(ActionEvent.None);

                         messageList.Add("アイン：ボスの威勢の良さが無くなるまではそんなに時間はかからなかった。"); eventList.Add(ActionEvent.None);

                         if (GroundOne.WE.Truth_GiveLanaEarring)
                         {
                             messageList.Add("アイン：・・・　確かそうだ。　思い出した。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：ラナと一緒にダンジョンへ進んで・・・それから・・・"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：そうだ、ラナのイヤリングだ。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：俺達はまだダンジョンの中にいる。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：ダンジョン内で、ボスを倒した後、俺は確かに見てる。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：ラナはあのイヤリングはあの時、まだ付けていた。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：そもそも俺の部屋に放ってある代物じゃねえ。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：ダンジョン内のどこかで無くした物だ。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：それが俺の部屋にあるってのが考えられない。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：自分で突っ込んでおいて、意味わかんねえけど・・・"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：俺とラナはダンジョンを進める途中で・・・"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：何か重大な失敗をおかした。"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：それも、取り返しのつかない事だ・・・"); eventList.Add(ActionEvent.None);

                             messageList.Add("アイン：ッグ・・・駄目だ。ここがどうしても思い出せねえ・・・"); eventList.Add(ActionEvent.None);
                         }
                         else
                         {
                             GroundOne.WE2.TruthKey1 = true; // これを真実世界へのキーその１とする。
                         }
                     }

                     messageList.Add("　　　【俺たちはその後、２階への階段を発見し】"); eventList.Add(ActionEvent.None);

                     messageList.Add("　　　【そのまま、２階へと足を運んだ】"); eventList.Add(ActionEvent.None);

                     GroundOne.WE.TruthCommunicationCompArea1 = true;
                 }

                 GroundOne.WE.TruthCommunicationCompArea1 = true;
                 GroundOne.WE.AlreadyRest = true;

                 // todo
                 //using (ESCMenu esc = new ESCMenu())
                 //{
                 //    esc.MC = this.MC;
                 //    esc.SC = this.SC;
                 //    esc.TC = this.TC;
                 //    esc.WE = this.we;
                 //    esc.KnownTileInfo = null;
                 //    esc.KnownTileInfo2 = null;
                 //    esc.KnownTileInfo3 = null;
                 //    esc.KnownTileInfo4 = null;
                 //    esc.KnownTileInfo5 = null;
                 //    esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                 //    esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                 //    esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                 //    esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                 //    esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                 //    esc.StartPosition = FormStartPosition.CenterParent;
                 //    esc.TruthStory = true;
                 //    esc.OnlySave = true;
                 //    esc.ShowDialog(); eventList.Add(ActionEvent.None);
                 //}

                 //ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING); eventList.Add(ActionEvent.None); // todo

                 messageList.Add(""); eventList.Add(ActionEvent.HomeTownTurnToNormal);

                 messageList.Add(""); eventList.Add(ActionEvent.None);

                 // SecondCommunicationStart(); eventList.Add(ActionEvent.None); // todo
			}
        }
        
        // DUEL闘技場開催
        public static void Message29000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ラナ：っあ、アイン。こんな所に居たのね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ようラナ、何のようだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインはDUEL闘技場には参加しないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUEL闘技か、あんま参加しようって思った事はねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふ～ん、そうなの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：アレはなんっつうんだ。DUELだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUELっつったら、DUELだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッバグシ！』（ラナのエレメンタルキックが炸裂）　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おぉぉぉぉ・・・ッグ、分かった分かった！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っで、俺に出ろとでも言いたいのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何で積極的に出たがらないのかを聞いてるのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうだな。DUELってのはいわゆる真剣勝負だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ダンジョン行ってる時は真剣勝負じゃないワケ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：別にそうは言ってねえ。だが、DUELとはまた少し別だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ダンジョンのモンスターは適当にぶっ潰せば良いだけだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だが、DUELは明らかに相手はモンスターじゃねえ。人間だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：適当にあしらうのもなんだし、マジでぶっ潰すのもなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：真剣に面と向かってやってやんなきゃ申し訳が立たねえだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん・・・何だかよく分かんないわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：やっぱり、一度ちゃんと参加してみれば？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まいったな・・・どうすっかな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：一つ、条件がある。飲んでくれるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っえ？ソレ、私に対して言ってるの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っそ、そんなの内容次第よ。じゃあ、言ってみなさいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺が勝った直後とか、DUEL前後では出来る限り俺の周囲から離れてくれ。良いな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っえ？　何よそれ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：この条件、飲んでくれればDUELに参加してみるぜ。どうだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、アインってさ。たまに良く分からない事言うわね・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ、でもそんな内容だったら。了解よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ！決まりだ！！腕が鳴るぜ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：申し込みとかの登録申請、早速やってくるとするか！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            // todo
            //CallSomeMessageWithAnimation("アインはDUEL闘技場へと向かっていった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：（アイン・・・あんな嬉しそうに、はしゃいで・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：（・・・　・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownAvailableDuel);
            // todo
            // buttonDuel.Visible = true;
            //CallSomeMessageWithAnimation("【DUEL闘技場へ行く事が出来るようになりました。】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            // todo
            //CallSomeMessageWithAnimation("－－－　DUEL闘技場にて　－－－"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！すげえ歓声だな！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょうど対戦が始まった所なんじゃない？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうみたいだな。ちょっと見ていくか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、私は良いわ、遠慮しとく。アイン登録申請に来たんじゃないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うぉっと！そうだった、忘れてたぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、早速受け付けにでも行ってみるとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：ようこそ、DUEL闘技場へ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っとだな、DUEL参加申し込みをしたいんだが。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：DUEL申請でしたら、こちら登録シートに記入をお願いします。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『名前』っと。っよし・・・Ein・・・Wolence・・っと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『現在までのDUEL申し込み回数』・・・確か、３シーズンっと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『主戦術』？何だこりゃ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：「アタック！！」"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、アイン。「アタック」なんて戦術でも何でもないわよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：良いじゃねえか。テキトーで良いんだよ、こんなもんは。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『魔法習得度』？・・・そうだな、「１００％」っと。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ジィ～～・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わかった、分かったって。「３０％」っと。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『二刀流可否』？・・・あんまり得意じゃねえが、一応『可』っと。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『スタックキャンセル可否』？・・・まあ『可』っかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何よそれ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？ああ、今度また教えてやるよ。次々っと・・・『ライバル』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・そうだな『オル・ランディス』っと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ランディスお師匠さんの名前じゃない。書いてもいいわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：大丈夫だろ。単なるアンケートみたいなもんだろうし。っふう、最後か。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『優勝したら？』。。。そうだなあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("「ッハッハッハ！！！」っとこんなもんか"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ホンット、あきれるぐらいテキトーよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあまあ、良いじゃねえか。よし、ホラよ。これで全部記入したぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：登録シートを受け付けました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：データベースに照合・適用を実施します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：照合判定結果は明日となりますので、明日から対戦登録表に正式エントリーされます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：対戦相手は対象の腕や力量に応じて本闘技場より自動的にピックアップいたします。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：ピックアップされたリスト内の相手と対戦を行ってください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：対戦は原則として、キャンセル・拒否は行えません。必ず本闘技場で競っていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それでも相手や俺が無理やり拒否ったらどうなるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：必ず対戦相手とＤＵＥＬされるよう手筈を整えます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それでも相手が断ったらどうなるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：必ず対戦相手とＤＵＥＬされるよう手筈を整えます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ・・・まあいいか。他に詳細ルールはあるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：詳しいＤＵＥＬルールに関しては、データベース適用が終わり次第お伝えいたします。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：以上となります。明日の連絡をお待ちください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、いろいろとありがとな。サンキュー！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今日は登録までか。まあ続きは明日って事で。そうだ、ラナ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何よ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、お前も参加してみないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ！？私！？　イイわよそんなの。どうせすぐ負けちゃうし。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何言ってんだ。あの無慈悲なライトニングキックなら大概の相手はその場で果てるぞ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：い、いい、いきなり知らない人に対して、あんなキックかませられないわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：んまあ、いいか。っじゃ！明日からはDUELも頑張ってくるとするか！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：頑張って来てよね。期待してるわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、あのクソ師匠にもいつか勝利してみせるぜ！任せておけって！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私、薬素材の集めとかあるから、じゃあまた後で♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、またな。"); eventList.Add(ActionEvent.None);

            // todo
            // CallSomeMessageWithAnimation("ラナは町の中へと歩いていった・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ダンジョンともう一つ、ＤＵＥＬか。。。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ＤＵＥＬ・・・懐かしい感じがするな。。。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（っしゃ、明日からも頑張って行くとするか！）", true); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            GroundOne.WE.AvailableDuelColosseum = true;
        }
        
        // DUEL闘技場、DUEL開始
        public static void Message29001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よお、あの時の受付さんじゃないか！登録申請はどうなった？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：アイン様の登録申請はデータベースへと照合され、正式に承諾されました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっしゃ！わざわざ教えに来てくれてサンキュー！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：本日から、アイン様はＤＵＥＬ闘技場での対戦者リストに登録された事となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：近々予定されている対戦相手リストを確認したい場合は、ＤＵＥＬ闘技場までお越しください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、後で行ってみるとするわ。ありがとな！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：なお、アイン様が「ライバル」欄にオル・ランディスを記載されていたため】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ？あぁ・・・確かに書いたが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：本闘技場のトップランカー、オル様より一言お伝えしておきたい内容があるとの事です】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ！！！　マジかよ！？！？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、本闘技場へとぜひともお越しください。私はこれにて。】"); eventList.Add(ActionEvent.None);

            // todo
            //CallSomeMessageWithAnimation("受付係員は闘技場へと戻っていった・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ・・・ヤ、ヤベェ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ックソ、何だっていきなり来てんだよ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：逃げても・・・おそらく無駄だろうな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ここは闘技場へ行くしかないか。", true); eventList.Add(ActionEvent.None);

            GroundOne.WE.AvailableDuelMatch = true;
        }
        
        // ESCメニュー：バトル設定
        public static void Message29002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ストレートスマッシュに・・・それから・・・フレッシュヒール・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何そんな所で練習してるのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、何となく思い出したのを体に慣れさせようと思ってだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しかし、どうすっかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：やけに考え込んでるわね。相談ならいつでも乗るわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、悪いな。ちょっとこういう話なんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【アインの下手な説明が、ラナへ展開中・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッダメ！っぜんっっっぜん分かんない！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアインの話って全然脈略が無いし、どこがポイントなのよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だからさっきから言ってるじゃねえか、この連続性が大事なんだって。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っちょ、もうそういう抽象的な話は結構よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインの話、かいつまんで話すとこういうことよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：『１．ＥＳＣメニューを開く』"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：『２．新しく追加されている【バトル設定】を選択する』"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：『３．現在習得してる魔法・スキル構成をバトルコマンドに設定する』"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、それはそうなんだが、そういう話をしてんじゃねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：コマンドの順序、そもそもバトルに関する根本的な理解がいまひとつだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今は良いでしょ。そんな話は後でいくらでも出てくるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：とりあえず覚えた魔法・スキルをパパっと設定しちゃいなさいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ホンット、どーでもいい部分でバカアインは凝り出すわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあいいじゃねえか。最初の内にやっておくに越した事はねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、さっそくやってみるぜ！"); eventList.Add(ActionEvent.None);

            // todo
            //CallSomeMessageWithAnimation("【ESCメニューより「バトル設定」が選択できるようになりました。】"); eventList.Add(ActionEvent.None);

            // todo
            CallSomeMessageWithAnimation("【習得した魔法・スキルをバトルコマンドに設定できるようになります。】"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AvailableBattleSettingMenu = true;
        }
        
        // 戦闘：インスタントアクション
        public static void Message29003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：この前は、確かこんな感じでやってた気がするんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何か難しそうな顔してるわね。何か思いついたわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん～いや、以前師匠に教わったヤツなんだけどな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ランディスのお師匠さん？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：インスタントアクションっていう行動らしいが。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：簡単に言うと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：インスタントアクションだ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：言い換えも出来てないじゃない・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあそれは良いとして、出来そうなの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、もうちょいのハズだ。まあ見ててくれよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　アインはストレート・スマッシュの体勢に入った　】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッファイア！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っえ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　アインはダミー素振り君にファイア・ボールを放った！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よっしゃ！完璧だろ？ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っお・・・驚いたわ。良くこんなの出来るわね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：理屈は簡単だ。ラナ、お前にもたぶん出来る内容だぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：要は、最初っからファイア・ボールを放つようにしとけばいいのさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：見た目の素振りだけをストレート・スマッシュにしてたって事？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、ストレート・スマッシュの体勢からは、ストレート・スマッシュは可能だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・私にも出来るのかしら・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：大丈夫だって。やってみろって。"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　ラナは通常攻撃の体勢に入った　】"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん・・・っと、こうかしら。ッハイ！"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　ラナはアイスニードルをダミー素振り君に放った！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・そんな感じだな！出来たじゃねえか！　ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、アインのとは少し違う気がしたんだけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：このやり方さえ出来てれば、戦闘スタイルもかなり幅が拡がるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ確かに通常の戦闘コマンドに加えて、この行動が出来るのは嬉しいわね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：楽しみになってきたな！っしゃ、もういっちょ練習しておくぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            // todo
            //CallSomeMessageWithAnimation("【戦闘中にインスタントアクションが出来るようになりました。】"); eventList.Add(ActionEvent.None);
            // todo
            //CallSomeMessageWithAnimation("【戦闘中、アクションコマンドを右クリックする事で使用可能になります。】"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AvailableInstantCommand = true;
        }
        #endregion
        #region "ダンジョンGO！"
        public static void Message30000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。"); eventList.Add(ActionEvent.None);
        }
        public static void Message30001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：今出てきたばかりだぜ？一休憩させてくれ。"); eventList.Add(ActionEvent.None);
        }
        #endregion
        #region "ラナと会話"
        public static void Message40000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // if (!we.AlreadyRest) // 1日目はアインが起きたばかりなので、本フラグを未使用とします。

            // todo
            //this.imgCharacter1.enabled = true;
            //this.imgCharacter1.gameObject.SetActive(true); eventList.Add(ActionEvent.None);
            //this.imgCharacter2.enabled = true;
            //this.imgCharacter2.gameObject.SetActive(true); eventList.Add(ActionEvent.None);
            messageList.Add("ラナ：っあら、意外と早いじゃない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、何だか寝覚めが良いんだ。今日も調子全快だぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカな事言ってないで、ホラホラ、朝ごはんでも食べましょ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだな！じゃあ、ハンナ叔母さんとこで食べようぜ。"); eventList.Add(ActionEvent.None);

            //using (MessageDisplay md = new MessageDisplay())
            //{
            //    md.StartPosition = FormStartPosition.CenterParent;
            //    md.Message = "ハンナの宿屋（料理亭）にて・・・";
            //    md.ShowDialog(); eventList.Add(ActionEvent.None);
            //}


            messageList.Add("アイン：っさっすが、叔母さん！今日の飯もすげえ旨いよな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハッハ、よく元気に食べるね。まだ沢山あるからね、どんどん食べな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アイン、少しは控えなさいよね。恥ずかしいったら。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、控えるぜ。次からな！ッハッハッハ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドス！』（ラナのサイレントブローがアインの横腹に炸裂）　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うおおぉぉ・・・だから食ってる時にそれをやるなって・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ッムグ・・・ごっそうさん！っでだ、ラナ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オレはダンジョンへ向かうぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そして、その最下層へオレは辿り付いてみせる！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っちょ、何よいきなり唐突に。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：全然脈略が無いじゃない。何よ、本当にそんなトコ行きたいわけ？"); eventList.Add(ActionEvent.None);

            //if (GroundOne.WE2.TruthBadEnd1)
            //{
            //    messageList.Add("アイン：まあ本当に行きたいとか言われてもなあ・・・"); eventList.Add(ActionEvent.None);

            //    messageList.Add("アイン：金を稼いで収支を成り立たせるってのも当然なんだが、"); eventList.Add(ActionEvent.None);

            //    messageList.Add("アイン：伝説のFiveSeekerに追いつきたい気持ちもあるが・・・"); eventList.Add(ActionEvent.None);

            //    messageList.Add("アイン：それは別として、とにかく行かなくちゃならねえ。そんな気がするんだ。"); eventList.Add(ActionEvent.None);

            //    messageList.Add("ラナ：ふーん、何か曖昧な答えね。"); eventList.Add(ActionEvent.None);

            //    messageList.Add("ラナ：まあ、分かったわよ。っじゃあ、はいコレ♪"); eventList.Add(ActionEvent.None);
            //}
            //else
            {
                messageList.Add("アイン：何言ってるんだ、ラナ。俺たちの稼ぎが何なのか忘れたのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺達の収支はダンジョンで成り立ってるだろ。金を稼がないとな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：うん、まあそれは分かってるつもりよ。でも何で最下層に行きたがるの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何でかって？そりゃあ決まってるだろ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：伝説のFiveSeeker様達に追いつくためさ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アインって昔っからFiveSeeker様の事、大好きよね。はしゃいじゃって、ッフフフ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何がおかしい？FiveSeekerはすべての冒険者にとっての憧れの的だろう？目標にして当然だろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：分かったわよ。っじゃあ、はいコレ♪"); eventList.Add(ActionEvent.None);
            }


            //using (MessageDisplay md = new MessageDisplay())
            //{
            //    md.Message = "【遠見の青水晶】を手に入れました。";
            //    md.StartPosition = FormStartPosition.CenterParent;
            //    md.ShowDialog(); eventList.Add(ActionEvent.None);
            //}

            //GetItemFullCheck(mc, Database.RARE_TOOMI_BLUE_SUISYOU); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、【遠見の青水晶】じゃねえか。助かるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：無くさないでよ？それ結構レア物で値段張るものなんだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？おう、任せておけって！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            //messageList.Add("アイン：っと、そうだ。忘れないうちに・・・"); eventList.Add(ActionEvent.None);

            //messageList.Add("アイン：・・・（ごそごそ）・・・"); eventList.Add(ActionEvent.None);

            //messageList.Add("ラナ：何探してるのよ？"); eventList.Add(ActionEvent.None);

            //messageList.Add("アイン：確かポケットに入れたはず・・・"); eventList.Add(ActionEvent.None);

            //using (TruthDecision td = new TruthDecision())
            //{
            //    td.MainMessage = "　【　ラナにイヤリングを渡しますか？　】";
            //    td.FirstMessage = "ラナにイヤリングを渡す。";
            //    td.SecondMessage = "ラナにイヤリングを渡さず、ポケットにしまっておく。";
            //    td.StartPosition = FormStartPosition.CenterParent;
            //    td.ShowDialog(); eventList.Add(ActionEvent.None);
            //    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        messageList.Add("アイン：あったあった。ラナ、こいつを渡しておくぜ。"); eventList.Add(ActionEvent.None);

            //        messageList.Add("ラナ：これ、私のイヤリングじゃない。どこで拾ったのよ？"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：どこって、俺の部屋に落ちてたぞ。ラナが落としていったんだろ？"); eventList.Add(ActionEvent.None);

            //        messageList.Add("ラナ：・・・っええ！？そそそ、そんなワケ無いじゃない！！"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：なんでそんな慌ててんだよ。まあ返しておくぜ。ッホラ！"); eventList.Add(ActionEvent.None);

            //        messageList.Add("ラナ：っとと、・・・アリガト♪"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：お前は変な所で抜けてるからな、しっかり持ってろよな。"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            //        mc.DeleteBackPack(new ItemBackPack("ラナのイヤリング")); eventList.Add(ActionEvent.None);
            //        we.Truth_GiveLanaEarring = true;
            //    }
            //    else
            //    {
            //        if (GroundOne.WE2.TruthBadEnd1)
            //        {
            //            messageList.Add("アイン：（・・・このイヤリング・・・）"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：（これをもってると、何か思い出せそうなんだが・・・）"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：（ラナには悪いが、もう少し持っておこう・・・）"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：いや、何でもねえんだ。"); eventList.Add(ActionEvent.None);

            //            messageList.Add("ラナ：今、ポケットをゴソゴソしてたじゃないの？"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：い、いやいや。何でもねえ、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            //            messageList.Add("ラナ：何よ、あからさまに怪しかったわよ？今のは・・・"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：いざ、ダンジョン！ッハッハッハ！"); eventList.Add(ActionEvent.None);
            //        }
            //        else
            //        {
            //            messageList.Add("アイン：おっかしいな・・・確かにポケットに入れたはずだが・・・"); eventList.Add(ActionEvent.None);

            //            messageList.Add("ラナ：何か探し物でもしてるの？"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：い、いやいや。何でもねえ、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            //            messageList.Add("ラナ：何よ、怪しいわね・・・"); eventList.Add(ActionEvent.None);

            //            messageList.Add("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！"); eventList.Add(ActionEvent.None);
            //        }
            //    }
            //}
            GroundOne.WE.AlreadyCommunicate = true;
            GroundOne.WE.Truth_CommunicationLana1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
        }
        #endregion
        #region "ガンツの武具店"
        public static void Message50000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ガンツ叔父さん、いますかー？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインか。よく来てくれた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：武具店、開いてますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ああ、今月はヴァスタ爺からの物資配給が良くてな。開店しておるので、後で見ていくと良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ！やったぜ！じゃあ開始する前に一回拝見させてもらうとするぜ！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyEquipShop = true;
            GroundOne.WE.AvailableEquipShop = true;
            GroundOne.WE.Truth_CommunicationGanz1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。

            messageList.Add("ガンツ：アインよ。その口ぶりからして、ダンジョンへ向かうのだな？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッマジっすか！？ハハ、やったぜ！ありがとうございます！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ダンジョンで殺したモンスターからは、役に立つ材料がいくつも採れる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：モンスターより得られる部材、素材をワシの所へ持ってくると良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：それら部材、素材を組み合わせ、ワシが腕によりをかけて新しい武具を作ろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：では頼んだぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！　ありがとうございました！！"); eventList.Add(ActionEvent.None);

            messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（待てよ・・・これはひょっとして・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（最後結局「精進しなさい」しか言ってねえよな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（でもまあ、ガンツ叔父さんの新しい武具生成か。楽しみだな。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（モンスターから得られた部材・素材はガンツ叔父さんのトコへ持っていくとするか。）"); eventList.Add(ActionEvent.None);

        }
        #endregion
        #region "ハンナの宿屋店"
        public static void Message60000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：叔母さん、いますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アインじゃないか。何の用だい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、特に用ってわけじゃないんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：どうしたんだい、何か気になる事でもあるのかい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうやって、そんな美味い飯を作れるようになったんですか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：う～ん、どうと言われてもねえ。こればかりは経験を積むしかないよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：経験・・・か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン。ひとつ頼まれてくれないかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アインは今からダンジョンへ向かうんだね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ダンジョンで得たアイテムで、食材になる物をワタシの所へ持ってきてくれないかね？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：そうしたら、これまでよりもっと良い夕飯を出せるようになるからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジっすか！？なら、喜んで持ってきますよ！任せておいてください！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハ、期待して待ってるよ。さあ、いってらっしゃいな。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationHanna1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
        }
        public static void Message69998(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ハンナ：もう朝だよ。今日も頑張ってらっしゃい。"); eventList.Add(ActionEvent.None);

        }
        public static void Message69999(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：おばちゃん。空いてる？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：空いてるよ。泊まってくかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうすっかな・・・泊まるか？"); eventList.Add(ActionEvent.None);

            // todo
            //using (YesNoRequestMini yesno = new YesNoRequestMini())
            {
                // todo
                //yesno.Location = new Point(this.Location.X + 784, this.Location.Y + 708); eventList.Add(ActionEvent.None);
                //yesno.Large = true;
                //yesno.ShowDialog(); eventList.Add(ActionEvent.None);
                //if (yesno.DialogResult == DialogResult.Yes)
                if (true)
                {
                    messageList.Add("ハンナ：はいよ、部屋は空いてるよ。ゆっくりと休みな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：サンキュー、おばちゃん。"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                    messageList.Add("ハンナ：今日は何か食べていくかい？"); eventList.Add(ActionEvent.None);

                    // todo
                    //using (TruthRequestFood trf = new TruthRequestFood())
                    {
                        // todo
                        //trf.StartPosition = FormStartPosition.CenterParent;
                        //trf.MC = this.mc;
                        //trf.SC = this.sc;
                        //trf.TC = this.tc;
                        //trf.WE = this.we;
                        //trf.ShowDialog(); eventList.Add(ActionEvent.None);
                        //this.mc = trf.MC;
                        //this.sc = trf.SC;
                        //this.tc = trf.TC;
                        //this.we = trf.WE;
                        //if (trf.DialogResult == System.Windows.Forms.DialogResult.OK)
                        string tempSelect = "とんかつ定食";
                        {
                            messageList.Add("アイン：おばちゃん、『" + tempSelect + "』を頼むぜ。"); eventList.Add(ActionEvent.None); // todo

                            messageList.Add("ハンナ：『" + tempSelect + "』だね。少し待ってな。"); eventList.Add(ActionEvent.None); // todo

                            messageList.Add("ハンナ：はいよ、お待たせ。たんと召し上がれ。"); eventList.Add(ActionEvent.None);

                            messageList.Add("　　【アインは十分な食事を取りました。】"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：ふう～、食った食った・・・"); eventList.Add(ActionEvent.None);

                            messageList.Add("アイン：おばちゃん、ごちそうさま！"); eventList.Add(ActionEvent.None);

                            messageList.Add("ハンナ：あいよ、後は明日に備えてゆっくり休みな。"); eventList.Add(ActionEvent.None);

                        }
                    }

                    messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallRestInn);
                }
                else
                {
                    messageList.Add("アイン：ごめん。まだ用があるんだ、後でくるよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。"); eventList.Add(ActionEvent.None);
                }
            }
        }
        #endregion


        #endregion

    }
}

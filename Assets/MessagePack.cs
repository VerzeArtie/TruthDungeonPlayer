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
            EncountBoss,
            StopMusic,
            PlayMusic01,
            PlayMusic02,
            PlayMusic03,
            PlayMusic04,
            PlayMusic05,
            PlayMusic06,
            PlayMusic07,
            PlayMusic08,
            PlayMusic09,
            PlayMusic10,
            PlayMusic11,
            PlayMusic12,
            PlayMusic13,
            PlayMusic14,
            PlayMusic15,
            PlayMusic16,
            PlayMusic17,
            PlayMusic18,
            PlayMusic19,
            PlaySound,
            YesNoGotoDungeon,
			YesNoBacktoDungeon,
            GotoHomeTown,
            GotoHomeTownForce,
            DecisionOpenDoor1,
            HomeTownGetItemFullCheck,
            HomeTownBlackOut,
            HomeTownTurnToNormal,
            HomeTownBackToTown,
            HomeTownButtonVisibleControl,
            HomeTownMorning,
            HomeTownNight,
            HomeTownDuelSelect,
            HomeTownShowDuelRule,
            HomeTownFazilCastle,
            HomeTownFazilCastleMenu,
            HomeTownTicketChoice,
            HomeTownGoToKahlhanz,
            HomeTownGotoFirstPlace,
            HomeTownExecRestInn,
            HomeTownExecItemBank,
            HomeTownCallRequestFood,
            HomeTownButtonHidden,
            HomeTownMessageDisplay,
            HomeTownYesNoMessageDisplay,
            HomeTownShowActiveSkillSpell,
            HomeTownCallPotionShop,
            HomeTownCallSaveLoad,
            HomeTownCallDuel,
            HomeTownCallDecision,
            HomeTownGotoRealDungeon,
            HomeTownAddNewCharacter,
            GetGreenPotionForLana,
            CallSomeMessageWithAnimation,
            CallSomeMessageWithNotJoinLana,
            DungeonGetTreasure,
            DungeonAgilityRoomStart,
            DungeonAgilityRoomStop,
            Ending,
        }


        #region "ダンジョン内"
        public static void MessageBackToTown(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ユングの町に戻るか？"); eventList.Add(ActionEvent.GotoHomeTown);
        }

        public static void MessageNotFound(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ん？特に何もなかったと思うが。"); eventList.Add(ActionEvent.None);
        }
        #region "１階"
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

                                messageList.Add("アイン：（駄目だ・・・ラナが許してくれそうにもねえ・・・）"); eventList.Add(ActionEvent.None);

                                messageList.Add("");  eventList.Add(ActionEvent.UpdateLocationRight);

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

                                messageList.Add("");  eventList.Add(ActionEvent.UpdateLocationRight);
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
                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.TurnToBlack);

                messageList.Add("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】"); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
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

            messageList.Add(Database.ENEMY_BOSS_KARAMITUKU_FLANSIS); eventList.Add(ActionEvent.EncountBoss);
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

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

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
                    messageList.Add("アイン：おっと、何だこりゃ。扉か？"); eventList.Add(ActionEvent.None);

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

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：オーケーオーケー。鍵はかかってねえな。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableSecondCharacter)
                    {
                        messageList.Add("ラナ：変なトラップとかも特に無さそうね。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：っしゃ、さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen2);

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

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationLeft);

                    messageList.Add("アイン：さっそく開けるぜ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("    『・・・ギギィ・・・ッバタン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.SmallEntranceOpen2);

                    if (GroundOne.WE.dungeonEvent21KeyOpen && GroundOne.WE.dungeonEvent22KeyOpen)
                    {
                        messageList.Add("アイン：小広間、３つ目の扉が開いたな。"); eventList.Add(ActionEvent.None);

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
        
        public static void Message10050(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent25)
            {
                GroundOne.WE.dungeonEvent25 = true;
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
                messageList.Add("ラナ：特に、変な所は無さそうね。普通の通路と同じね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：待て、ラナ。下手に進むな。強い殺気を感じる！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そう？そろそろボスって所かしら。気をつけて進みましょ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、気を引き締めていこうぜ。"); eventList.Add(ActionEvent.None);
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

        public static void Message10051(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
			messageList.Add(Database.Message_GotoDownstair); eventList.Add(ActionEvent.YesNoGotoDungeon);
        }

        public static void Message10051_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：おし、１階制覇した事だし、一度ユングの町へ戻るとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
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

                messageList.Add(""); eventList.Add(ActionEvent.ReturnToNormal);

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
        #endregion
        #region "２階"
        // １階へ戻る階段
        public static void Message12000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
			messageList.Add(Database.Message_GotoUpstair); eventList.Add (ActionEvent.YesNoBacktoDungeon);
        }

        // 中央４看板
        public static void Message12001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent201)
            {
                GroundOne.WE.dungeonEvent201 = true;

				messageList.Add("アイン：おっ、看板があるぜ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『知を示す者、隠されし通路、秘められた選択』"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ふうん、なんだか意味深な内容ね。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：知力を示せって言ってるんだろ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：そんなの誰だってわかるわよ、ほんっと適当なんだから・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：隠されし通路、秘められた選択・・・どういう意味なのかしら。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：隠し通路見つけて秘密に質問に答えるってトコだろ。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：何かものすごく淡々としてるわね・・・バカに磨きがかかったワケ？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：何を言うか。順当読みだ。ありのまま読んでるだけだって。"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.dungeonEvent201 && GroundOne.WE.dungeonEvent202 && GroundOne.WE.dungeonEvent203 && GroundOne.WE.dungeonEvent204)
                {
					messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent205 = true;
                    // after
                    //blueWallBottom[17 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallTop[18 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallBottom[20 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallTop[21 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallRight[23 * Database.TRUTH_DUNGEON_COLUMN + 30] = false;
                    //blueWallLeft[23 * Database.TRUTH_DUNGEON_COLUMN + 31] = false;
                    //blueWallRight[15 * Database.TRUTH_DUNGEON_COLUMN + 27] = false;
                    //blueWallLeft[15 * Database.TRUTH_DUNGEON_COLUMN + 28] = false;
                    //dungeonField.Invalidate();

					messageList.Add("アイン：おっ！開いたんじゃねえか！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：４つとも調べると、開く仕組みだったワケね♪"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：でも本当気をつけてよね。この先、何が待ち構えてるか分からないし。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、分かってるって。危ないと感じたら戻れるようにしておくさ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
					messageList.Add("ラナ：まあ、良いわ。特に行き道も無さそうだし、他も探索してみましょ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }
            }
            else
            {
				messageList.Add("　　　　『知を示す者、隠されし通路、秘められた選択』"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message12001_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent202)
            {
                GroundOne.WE.dungeonEvent202 = true;
				messageList.Add("アイン：おっ、看板があるぜ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『技を示す者、順序ある通路、閉ざされた選択』"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：技か・・・なるほどな。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：何が「なるほど」なのよ？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：順序ある通路だな・・・なるほど・・・。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：分からないで適当に言ってるでしょ？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：いや、理解して言ってるぜ。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：じゃあ言ってごらんなさいよ。ッホラ。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ッハッハッハッハ！"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　『ズゴゴオオォォン！！！』（ラナのアルタネイティブブローがアインに炸裂）　　"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：イッツツツ・・・わかったって。正直分かってません、ハイハイ・・・"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.dungeonEvent201 && GroundOne.WE.dungeonEvent202 && GroundOne.WE.dungeonEvent203 && GroundOne.WE.dungeonEvent204)
                {
					messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent205 = true;
                    // after
                    //blueWallBottom[17 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallTop[18 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallBottom[20 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallTop[21 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallRight[23 * Database.TRUTH_DUNGEON_COLUMN + 30] = false;
                    //blueWallLeft[23 * Database.TRUTH_DUNGEON_COLUMN + 31] = false;
                    //blueWallRight[15 * Database.TRUTH_DUNGEON_COLUMN + 27] = false;
                    //blueWallLeft[15 * Database.TRUTH_DUNGEON_COLUMN + 28] = false;
                    //dungeonField.Invalidate();

					messageList.Add("アイン：っお！？ホラ見ろ、開いたんじゃねえか！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：４つとも調べると、開く仕組みだったワケね。バカアインは関係ないから"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：マジかよ！蹴られ損かよ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：でも本当気をつけてよね。この先、何が待ち構えてるか分からないし。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あ、あぁ・・・分かってるって。危ないと感じたら迷わず戻るようにするさ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
					messageList.Add("ラナ：まあ、いいわ。特に行き道も無さそうだし、他も探索してみましょ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }

            }
            else
            {
				messageList.Add("　　　　『技を示す者、順序ある通路、閉ざされた選択』"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message12001_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent203)
            {
                GroundOne.WE.dungeonEvent203 = true;
				messageList.Add("アイン：おっ、看板があるぜ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『心を示す者、無間の部屋、存在しない解への選択』"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：無間って・・・何か嫌な感じね。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：存在しない解とか書いてあるな。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：バカアインは意味分かる？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：バカだからわかんねえ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　『ッドグシ！！！』（ラナのサドンキックがアインに炸裂）　　"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ッヴ・・・いやいやいや。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：本当に無間ってのがあるワケが無いだろう？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ん～、まあそうよね。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：答えはひとつって感じもするよな・・・どうだろう。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ん～、今ひとつピンと来ないわね。"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.dungeonEvent201 && GroundOne.WE.dungeonEvent202 && GroundOne.WE.dungeonEvent203 && GroundOne.WE.dungeonEvent204)
                {
					messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent205 = true;
                    // after
                    //blueWallBottom[17 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallTop[18 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallBottom[20 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallTop[21 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallRight[23 * Database.TRUTH_DUNGEON_COLUMN + 30] = false;
                    //blueWallLeft[23 * Database.TRUTH_DUNGEON_COLUMN + 31] = false;
                    //blueWallRight[15 * Database.TRUTH_DUNGEON_COLUMN + 27] = false;
                    //blueWallLeft[15 * Database.TRUTH_DUNGEON_COLUMN + 28] = false;
                    //dungeonField.Invalidate();

					messageList.Add("アイン：おっ！開いたんじゃねえか！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：４つとも調べると、開く仕組みだったワケね♪"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：でも本当気をつけてよね。この先、何が待ち構えてるか分からないし。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、分かってるって。危ないと感じたら戻れるようにしておくさ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
					messageList.Add("ラナ：まあ、いいわ。特に行き道も無さそうだし、他も探索してみましょ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }
            }
            else
            {
				messageList.Add("　　　　『心を示す者、無間の部屋、存在しない解への選択』"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message12001_4(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent204)
            {
                GroundOne.WE.dungeonEvent204 = true;

				messageList.Add("アイン：おっ、看板があるぜ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『力を示す者、戻り無き通路、真実への選択』"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：っしゃ！こういうのは分かりやすいよな！"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ちょっと、戻り無き通路とか書いてあるじゃない。気をつけてよね？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ッハッハッハ！力任せは決してバカではない。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：また、テキトーな理論で突っ込まないでよね、ホント・・・"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.dungeonEvent201 && GroundOne.WE.dungeonEvent202 && GroundOne.WE.dungeonEvent203 && GroundOne.WE.dungeonEvent204)
                {
					messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent205 = true;
                    // after
                    //blueWallBottom[17 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallTop[18 * Database.TRUTH_DUNGEON_COLUMN + 33] = false;
                    //blueWallBottom[20 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallTop[21 * Database.TRUTH_DUNGEON_COLUMN + 25] = false;
                    //blueWallRight[23 * Database.TRUTH_DUNGEON_COLUMN + 30] = false;
                    //blueWallLeft[23 * Database.TRUTH_DUNGEON_COLUMN + 31] = false;
                    //blueWallRight[15 * Database.TRUTH_DUNGEON_COLUMN + 27] = false;
                    //blueWallLeft[15 * Database.TRUTH_DUNGEON_COLUMN + 28] = false;
                    //dungeonField.Invalidate();

					messageList.Add("アイン：おっ！開いたんじゃねえか！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：４つとも調べると、開く仕組みだったワケね♪"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：でも本当気をつけてよね。この先、何が待ち構えてるか分からないし。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、分かってるって。危ないと感じたら戻れるようにしておくさ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
					messageList.Add("ラナ：まあ、いいわ。特に行き道も無さそうだし、他も探索してみましょ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }
            }
            else
            {
				messageList.Add("　　　　『力を示す者、戻り無き通路、真実への選択』"); eventList.Add(ActionEvent.None);
            }
        }

        // 知の部屋、メイン看板、ファースト：３回
        public static void Message12002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent211)
            {
                if (!GroundOne.WE.dungeonEvent206)
                {
                    GroundOne.WE.dungeonEvent206 = true;

					messageList.Add("アイン：おっ、何か書いてあるぜ。なになに？"); eventList.Add(ActionEvent.None);

					messageList.Add("　　　　『  上  』"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何このテキトーな看板・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：よっしゃ、じゃあさっそく上へ行くとするか！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ちょっと待ってよ。アイン。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ん？何だ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：上ってどっちの事を指してるのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：どっち？どっちって言われると困るが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationTop);
                    //UpdateUnknownTile(); // after

					messageList.Add("アイン：こっちじゃねえのか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：今来た道から見て右よね。どうして、そっちが上なのよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationBottom);

					messageList.Add("アイン：最初の看板から進んで来て・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：右３だから、もう１回曲がれば右４つまり元の方向だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：最初向いていた方向を上と仮定すれば、コッチが上で決まりってわけさ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・え？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っあ！　いやいや！　当てずっぽだからな！ッハッハッハッハ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふうん、あながち間違いってワケでも無さそうね。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、そっちに進んでみましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);

                }
                else
                {
                    if (!GroundOne.WE.dungeonEvent207)
                    {
						messageList.Add("　　　　『  上  』"); eventList.Add(ActionEvent.None);
                    }
                    else
                    {
                        if (!GroundOne.WE.dungeonEvent208)
                        {
                            GroundOne.WE.dungeonEvent208 = true;

							messageList.Add("　　　　『  下  』"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：おっ、見ろよ？看板が変わってるじゃねえか！！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：上から下って事は、さすがにこれは楽勝ね♪"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：っしゃ、下へ行ってみようぜ！"); eventList.Add(ActionEvent.None);
                        }
                        else
                        {
                            if (!GroundOne.WE.dungeonEvent209)
                            {
								messageList.Add("　　　　『  下  』"); eventList.Add(ActionEvent.None);
                            }
                            else
                            {
                                if (!GroundOne.WE.dungeonEvent210)
                                {
                                    GroundOne.WE.dungeonEvent210 = true;

									messageList.Add("　　　　『  左  』"); eventList.Add(ActionEvent.None);

									messageList.Add("アイン：よし、左だな！"); eventList.Add(ActionEvent.None);

									messageList.Add("ラナ：コレといった引っ掛け問題でも無さそうね、進みましょ♪"); eventList.Add(ActionEvent.None);

									messageList.Add("アイン：了解了解！"); eventList.Add(ActionEvent.None);

                                }
                                else
                                {
									messageList.Add("　　　　『  左  』"); eventList.Add(ActionEvent.None);
                                }
                            }
                        }
                    }
                }
            }
        }

        // 知の部屋、メイン看板、セカンド：下３上１２左８
        public static void Message12003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent215)
            {
                if (GroundOne.WE.dungeonEvent212 && GroundOne.WE.dungeonEvent213 && GroundOne.WE.dungeonEvent214)
                {
                    GroundOne.WE.dungeonEvent215 = true;

					messageList.Add("　　　　『  下３　上１２　左８  』"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：おっしゃ！見ろよラナ、看板に何か書いてあるぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：これ、レバーに関する法則か何かのヒントだろ？ッハッハッハ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っど、どうしたラナ？？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：えっと・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：凄いじゃない、オメデト。　驚いたわホント。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何で看板の文字が復活するのが分かったのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：何でって言われても困るが・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：なんとなく引っかかるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：答えになってないじゃない。ちゃんと説明しなさいよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：こういうのは、どう説明していいか・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：師匠に習ったんだよ。いろいろとな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ランディスお師匠さん？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何て習ったの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：（声マネ）『ランディス：てめぇは少し考えすぎだ。』"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：（声マネ）『ランディス：思考を捨てろ、ザコアイン。』"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：（声マネ）『ランディス：そんなんじゃ、見えるもんも見えねぇぞ。』"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：考えすぎ！？ジョーダンでしょ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いや、俺なりにだな・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：アンタから思考捨てちゃったら、バカの領域を超えたスーパーバカになるでしょ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いやいや・・・そこは、俺なりにだな・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：見えるもんも見えないって・・・いつも見えてないでしょ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いやいやいや・・・そこも、俺なりにいろいろとだな・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：結局それが、今回の件にどう結びついてるわけよ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：あーーーーーーっ！！！！！！"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：うぉおおぉ！？うるせえな、いきなり大声出すなっつうの。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：確かに今回、当たったわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、まあな。看板ヒントを引き当てたぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：でも・・・でも・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：もう良いわよ！！　バカアインなんか知らない！！"); eventList.Add(ActionEvent.None);

					messageList.Add("　　　『バグシイイイィィィ！！』（ラナのジェノサイドキックがアインの脇腹に炸裂）　　"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ッボグゥ・・・なぜ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ええっと　「下３　上１２　左８」　だから・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：下側通路で３回レバー、上側で１２回レバー、左側で８回レバーやれって事ね♪"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：簡単じゃない♪　とっとと行くわよ。ッホラホラ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：神よ・・・このラナ・アミリアという人間に慈しみの心を・・・"); eventList.Add(ActionEvent.None);
                }
            }
            else if (GroundOne.WE.dungeonEvent215 && !GroundOne.WE.dungeonEvent219)
            {
				messageList.Add("　　　　『  下３　上１２　左８  』"); eventList.Add(ActionEvent.None);
            }
        }

        // 知の部屋、メイン看板、サード：( >10 _6 <7 )  ( <11 ~3 )  ( _3 >7 )
        public static void Message12004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent215 && GroundOne.WE.dungeonEvent219 && !GroundOne.WE.dungeonEvent220)
            {
                GroundOne.WE.dungeonEvent220 = true;

				messageList.Add("ラナ：っあ、見てみてアイン。看板の内容が変わってるわよ♪"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『  ( >10 _6 <7 )  ( <11 ~3 )  ( _3 >7 )  』"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょ・・・何よこれ？？？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：やっかいな内容だな・・・良く見せてくれ。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：・・・  ・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：えっと・・・わかりそう？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：・・・  ・・・　いや。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：なんとなくだが。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：そう？私はお手上げみたい。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：頼んだわよ、アンタのバカ感性だけが頼りなんだから。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：それって褒めて無いよな？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ううん、褒めてるわよ♪"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：サンキュー！任せておけって！　ッハッハッハ！"); eventList.Add(ActionEvent.None);
            }
            else if (GroundOne.WE.dungeonEvent225)
            {
                if (!GroundOne.WE.dungeonEvent227)
                {
                    if (GroundOne.WE.AvailableThirdCharacter)
                    {
						messageList.Add("ランディス：よぉ、ザコアイン。ここか？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ああ、確かここで台座が突然出てきたんだ。"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：突然の出来事でビックリしたわよ、ホント。"); eventList.Add(ActionEvent.None);

						messageList.Add("ランディス：やるじゃねぇか。たいしたもんだ。"); eventList.Add(ActionEvent.None);

						messageList.Add("ランディス：そっちの娘。直接関与は？"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：っえ！？　私ですか！？"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：い、いえ。私は外から見てただけでした。"); eventList.Add(ActionEvent.None);

						messageList.Add("ランディス：なら良い。"); eventList.Add(ActionEvent.None);

						messageList.Add("ランディス：おい、ザコアイン。"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：な、何だよ？"); eventList.Add(ActionEvent.None);

						messageList.Add("ランディス：この場所、終わってねえだろ。とっとと進めろ。"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：わ、分かってるって！今すぐ進めるさ！！"); eventList.Add(ActionEvent.None);

                        GroundOne.WE.dungeonEvent227 = true;
                    }
                    else
                    {
						messageList.Add("台座はもうなくなってる"); eventList.Add(ActionEvent.None);
                    }
                }
                else
                {
					messageList.Add("台座はもうなくなってる"); eventList.Add(ActionEvent.None);
                }
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent221 || !GroundOne.WE.dungeonEvent222 || !GroundOne.WE.dungeonEvent223)
                {
					messageList.Add("　　　　『  ( >10 _6 <7 )  ( <11 ~3 )  ( _3 >7 )  』"); eventList.Add(ActionEvent.None);
                }
                else
                {
					messageList.Add("　　　　『  ( >10 _6 <7 )  ( <11 ~3 )  ( _3 >7 )  』"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：これ、分かったぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ホント？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：今見てきた、部屋への通路ポイントだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まあ、見てろ。"); eventList.Add(ActionEvent.None);

					messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

					messageList.Add("　　　　『突如台座がアインの前に浮き上がった。直後、薄黄色い空間にアインが包まれた。』"); eventList.Add(ActionEvent.None);

					messageList.Add("　　　　『絶対試練：汝、答えを示せ。』"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っちょっと何これ？？アイン！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。任せとけ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：アイン・・・頑張って・・・"); eventList.Add(ActionEvent.None);

					messageList.Add(""); eventList.Add(ActionEvent.PlayMusic09);

                    // after
                    //using (TruthAnswer TA = new TruthAnswer())
                    {
                        //TA.ShowDialog();
                        GroundOne.StopDungeonMusic();

                        // after
                        //if (TA.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        if (true)
                        {
                            GroundOne.WE2.TruthAnswerSuccess = true;
                            GroundOne.WE.dungeonEvent224 = true;

							messageList.Add("　　　　『空間が激しくフラッシュし、凝縮された空間へと連続的に小さくなる！！！』"); eventList.Add(ActionEvent.None);

							messageList.Add("　　　　『パパパパパパパ！！！！ッバシュウウウゥゥゥン！！！！！！！』"); eventList.Add(ActionEvent.None);

							messageList.Add("　　　　『空間は弾け飛んだ後、台座の前にアインの倒れた姿があった』"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：アイン！！大丈夫！？"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：あ、ああ。大丈夫だ。イッツツツ・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：何・・・心配させちゃってんのよ！！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：アンタって本当にバカよ！！何やってんのよホントに！！！"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：わ、悪かったって。大丈夫だって言ってるじゃないか・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：叩くなっつうの、いってぇって・・・"); eventList.Add(ActionEvent.None);

                            messageList.Add("アインとラナはしばらくその場で休息を取った。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

							messageList.Add("アイン：あの詩、全然聞いた事無いけどな。不思議と昔から知ってるような感じだったぜ。すげえ綺麗だった。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：でも、アインが選んでいた選択肢だけど"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：何だ、外から見えていたのか？"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：ええ、見えるってもんじゃなかったわよ。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：文字そのものが空中に浮いててビックリしたんだけど・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：っげ、そうだったのかよ？俺は全然周囲を把握できない状況だったしな。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：そんな事より、さっき順番に選択していた詩だけど。"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：なんだ？"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：う～ん・・・多分私もその詩、知ってるわよ。"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：マジかよ！？"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：私の母さんからよく聞かされていたわ。確かタイトルが・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：『神々の詩』よ。サブタイトルもあったと思うんだけど忘れちゃったわ。"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：神々の詩？へえ、そんなタイトルなんだ・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：って、あれ？？　ラナ、お前知ってたのかよ！？"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：文字を見てたら偶然思い出したわ。思い出に近いって感じかしら。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：私とアインが小さい頃、私の母さんに歌ってもらったヤツよ。覚えてない？"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：そうだったっけ・・・全然覚えに無いが・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：そうだな、昔から知ってるって感覚は、そういう事か。"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：なあ、一辺戻るか？　町へ。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：別に良いけど、どうかしたわけ？"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：何かちょっと気になる事があってな。途中で悪いけど。"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：ん、良いわよ。じゃ戻りましょ♪"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：じゃ、使うぜ！　遠見の青水晶。"); eventList.Add(ActionEvent.None);

                            messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
                        }
                        else
                        {
                            GroundOne.WE2.TruthAnswerFail = true;

							messageList.Add("アイン：駄目だ・・・何かが違う・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：ひょっとして・・・今のは・・・確か・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：やべ・・・何か頭が・・・ッグ・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

							messageList.Add(" ～　THE　END　～　（記憶の断片）"); eventList.Add(ActionEvent.None);

                            // after
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
                            //    esc.ShowDialog();
                            //}

                            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        }
                    }
                }
            }
        }
		#region "知の部屋、フェーズ１"
		public static void Message12005(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (GroundOne.WE.dungeonEvent206 && !GroundOne.WE.dungeonEvent207)
			{
				// 正解
				GroundOne.WE.dungeonEvent207 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：何か今・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょ、アイン。何か踏んでるわよ・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？・・・おお、よく見たら小さいスイッチがあるな。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：悪い悪い、踏んでた！ッハッハッハッハ！"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：もう少し気を配って進んでよね！？何かのトラップだったらどうするのよ！？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：わー、わかった分かった！気をつけて進みます、ハイハイ・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ハイハイ、じゃないわよもう。ホンット気をつけてよね。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ハイ・・・"); eventList.Add(ActionEvent.None);
			}
			else if (GroundOne.WE.dungeonEvent208 && !GroundOne.WE.dungeonEvent209)
			{
				// 不正解
				GroundOne.WE.dungeonEvent209Fail = true;
				GroundOne.WE.dungeonEvent209FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？しまっ、ひょっとして！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょっと！？バカアイン！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			}
			else if (GroundOne.WE.dungeonEvent210 && !GroundOne.WE.dungeonEvent211)
			{
				// 不正解
				GroundOne.WE.dungeonEvent211Fail = true;
				GroundOne.WE.dungeonEvent211FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？なんだ！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょっと！？勝手に踏まないでって！"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			}
			else
			{
				// イベント終了後は特に無いので、記述不要
			}
		}

		public static void Message12006(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (GroundOne.WE.dungeonEvent206 && !GroundOne.WE.dungeonEvent207) {
				// 不正解
				GroundOne.WE.dungeonEvent207Fail = true;
				GroundOne.WE.dungeonEvent207FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？何か今・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょ、コレって！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			} 
			else if (GroundOne.WE.dungeonEvent208 && !GroundOne.WE.dungeonEvent209) {
					// 正解
				GroundOne.WE.dungeonEvent209 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：何か今・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょ、アイン！？今分かってて踏んだでしょ！？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：あ、いやいや、そうじゃねえ・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：気が付いたら、踏んでた！ッハッハッハッハ！"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ちょっともう！あれほど気をつけてって言ってるじゃない！？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：いやあ、下ってココだろ？間違いないと思ってさ。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：たとえ、そうだったとしても、１回ぐらいちゃんと相談しなさいよね！？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：っす、すまねえな・・・次からは気をつけるって・・・"); eventList.Add(ActionEvent.None);
			} else if (GroundOne.WE.dungeonEvent210 && !GroundOne.WE.dungeonEvent211) {
				// 不正解
				GroundOne.WE.dungeonEvent211Fail = true;
				GroundOne.WE.dungeonEvent211FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？なんだ！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょっと！？勝手に踏まないでって！"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			} else {
				// イベント終了後は特に無いので、記述不要
			}
		}

		public static void Message12007(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (GroundOne.WE.dungeonEvent206 && !GroundOne.WE.dungeonEvent207)
			{
				// 不正解
				GroundOne.WE.dungeonEvent207Fail = true;
				GroundOne.WE.dungeonEvent207FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？何か今・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょ、コレって！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			}
			else if (GroundOne.WE.dungeonEvent208 && !GroundOne.WE.dungeonEvent209)
			{
				// 不正解
				GroundOne.WE.dungeonEvent209Fail = true;
				GroundOne.WE.dungeonEvent209FailEvent2 = true;

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？しまっ、ひょっとして！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【アインとラナの周囲を奇妙な光が包み込んだ】"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：っちょっと！？バカアイン！？"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッバシュウウウゥゥゥンン】"); eventList.Add(ActionEvent.None);

				messageList.Add(""); eventList.Add(ActionEvent.GotoHomeTownForce);
			}
			else if (GroundOne.WE.dungeonEvent210 && !GroundOne.WE.dungeonEvent211)
			{
				// 正解
				GroundOne.WE.dungeonEvent211 = true;

				messageList.Add("アイン：よし、ここのくぼみだな。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：今までの通路、青扉が幾つかあったわよね？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ん？ああ、そうだな。まだ開いてなかったみたいだが。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：おそらく、このスイッチで・・・行くぜ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ええ、良いわよ。"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　【ッカチ】"); eventList.Add(ActionEvent.None);

				messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

				// after
//						blueWallLeft[17 * Database.TRUTH_DUNGEON_COLUMN + 50] = false;
//						blueWallRight[17 * Database.TRUTH_DUNGEON_COLUMN + 49] = false;
//						blueWallBottom[2 * Database.TRUTH_DUNGEON_COLUMN + 42] = false;
//						blueWallTop[3 * Database.TRUTH_DUNGEON_COLUMN + 42] = false;
//						blueWallBottom[7 * Database.TRUTH_DUNGEON_COLUMN + 46] = false;
//						blueWallTop[8 * Database.TRUTH_DUNGEON_COLUMN + 46] = false;

				messageList.Add("アイン：っしゃ！どうやら正解みたいだな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：アイン、ちょっと良いかしら？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：何だ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：アインは、なんて言うのかしら・・・その・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：っな・・・何だよ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：どういう理解でダンジョン攻略を進めてるわけ？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：よく分からない質問だな。う～ん、何て言うんだろうな。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：良いから、ちょっと答えてみなさいよ。ッホラ早く！"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：何で質問してる側が・・・まあいいか。ええとだな。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：看板、あるだろ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：ええ、あるわね。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：文字、書いてあるじゃねえか？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：そうね。１文字ばっかりだったけど"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：んで、青扉が並んでるだろ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：う～ん・・・あるけど・・・"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：じゃあ、今のところ１通りしかねえ。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：条件違うと、他の事でも考えられうるわけ？"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：赤扉とか、複雑な迷路であれば違った発想もあるよな。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ってか、どうしたんだ？突然そんなこと聞き始めて。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：うーん、私と考え方がちょっと違うみたいだし。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：ラナ、おまえはどう思ってたんだ？"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：別に良いじゃない。教えない。"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：っう・・・そうか。まあ良いんだ。今後もバンバン聞いてくれ。"); eventList.Add(ActionEvent.None);

				messageList.Add("ラナ：まあ、教えてくれてありがと♪　ダンジョン探索続けましょ♪"); eventList.Add(ActionEvent.None);

				messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
			}
			else
			{
				// イベント終了後は特に無いので、記述不要
			}
		}
		#endregion
		#region "知の部屋、フェーズ２"
		public static void Message12008(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (!GroundOne.WE.dungeonEvent212)
			{
				GroundOne.WE.dungeonEvent212 = true;

				if (!GroundOne.WE.dungeonEvent213 && !GroundOne.WE.dungeonEvent214)
				{
					messageList.Add("アイン：ん？なんだこのレバーは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：見たところ、駆動部は１箇所だけね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：どうする？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～～ん・・・アインはどう思うわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そうだなあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まだ開いてるとこ、あるんじゃねえかな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：そういえば、青扉っていくつかあったわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：他の場所をもう少し探索が要る。そう俺は思うぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：テキトーにレバー引かないワケ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：場合によるな。今回は引かない。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふーん。そう・・・まあ良いけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、他の場所でも探索してみる？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、行ってみようぜ。"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent213 && GroundOne.WE.dungeonEvent214) ||
					(GroundOne.WE.dungeonEvent213 && !GroundOne.WE.dungeonEvent214))
				{
					messageList.Add("アイン：おっ、レバー発見発見！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：やっぱり同じようなレバーね。特徴もまったく一緒よ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どうするの？もうレバー引いちゃうわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：俺はバカではない。今回も見送りだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、やけに慎重ね。頭でも打ったの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：違うっつうの。ただ、今回はアレだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板の文字がさ。もう消えてるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・っえ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あの看板。役割を終えたのかどうかがハッキリしねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：普通さ。役割終えたら、そのままにしておくと思わねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～ん、どうかしら。役割終えたから、文字が消えたんじゃないの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：今までそうだったか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：えっと、う～ん、全然気にしてなかったわよ、そんなの。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：だろ？じゃあ、多分まだ何かあるんだぜ。きっと。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っちょ、だからどうしてそうなるのよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まあ、良いじゃねえか。っしゃ、引き続き探索と行こうぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か無理やり切り上げてるし・・・まあ、慎重なのは良い事だけど。"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("ラナ：レバーがあったわよ？どうするの？アイン。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・そうだなあ。・・・ちょっと待ってくれ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板、見に行こうぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：すまねえな、ちょっと気になってるんだ。すぐソコだし良いだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ホントかしら・・・まあ、アインがそういうなら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：すまねえな。じゃ、看板見に行くとするか！"); eventList.Add(ActionEvent.None);
				}
			}
			else if (GroundOne.WE.dungeonEvent219)
			{
				messageList.Add("　　　　『  レバーはもう動かない  』"); eventList.Add(ActionEvent.None);
			}
			else
			{
				messageList.Add("　　　　『  レバーがある  』"); eventList.Add(ActionEvent.None);

				if (!GroundOne.WE.dungeonEvent215)
				{
					messageList.Add("アイン：（どうすっかな・・・やっぱ挑戦してみるべきか・・・）"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("アイン：（情報も揃ったことだし、挑戦みるか！）"); eventList.Add(ActionEvent.None);
				}

				// after
				//using (YesNoRequest yesno = new YesNoRequest())
				{
					// after
					//yesno.ShowDialog();
					//if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
					if (true)
					{
						if (GroundOne.WE.dungeonEvent215)
						{
							if (!GroundOne.WE.dungeonEvent216_2)
							{
								GroundOne.WE.dungeonEvent216_2 = true;

								messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("ラナ：えっと、分かってるわよね？さっきのヒントで。"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{
								messageList.Add("ラナ：あれ？またやってみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：あ、ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
							}
						}
						else
						{
							if (!GroundOne.WE.dungeonEvent216_2)
							{
								GroundOne.WE.dungeonEvent216_2 = true;
								messageList.Add("ラナ：あれ？試してみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ、ちょっとだけな。"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ッゲ、回数なんてあるのかよ？"); eventList.Add(ActionEvent.None);

								messageList.Add("ラナ：あっ、言ってなかったっけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：回数入力なんてマジかよ。じゃあ・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{
								messageList.Add("ラナ：また、やってみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ・・・"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：回数か。じゃあ・・・"); eventList.Add(ActionEvent.None);
							}
						}

						// after
						//using (RequestInput request = new RequestInput())
						{
							//request.StartPosition = FormStartPosition.CenterScreen;
							//request.InputData = "1";
							//request.ShowDialog();
							//if (request.InputData == "12")
							if (true)
							{
								GroundOne.WE.dungeonEvent216 = true;
							}
							else
							{
								GroundOne.WE.dungeonEvent216 = false;
							}
						}

						messageList.Add("　　　【ッガチャン】"); eventList.Add(ActionEvent.None);

						if (GroundOne.WE.dungeonEvent216 && GroundOne.WE.dungeonEvent217 && GroundOne.WE.dungeonEvent218)
						{
							GroundOne.WE.dungeonEvent219 = true;

							messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

							// after
//							blueWallBottom[7 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallTop[8 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallLeft[11 * Database.TRUTH_DUNGEON_COLUMN + 50] = false;
//							blueWallRight[11 * Database.TRUTH_DUNGEON_COLUMN + 49] = false;
//							blueWallLeft[14 * Database.TRUTH_DUNGEON_COLUMN + 59] = false;
//							blueWallRight[14 * Database.TRUTH_DUNGEON_COLUMN + 58] = false;

							messageList.Add("アイン：っしゃ！来たぜ、ラナ！　ッハッハッハ！！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：まあ順当って感じよね♪"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：また、どっかの青扉が開いているだろうし、行ってみようぜ！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：じゃあ、引き続き探索と行きますか♪"); eventList.Add(ActionEvent.None);
						}
						else
						{
							messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：・・・何も起こらないけど？"); eventList.Add(ActionEvent.None);

							if (GroundOne.WE.dungeonEvent215)
							{
								messageList.Add("アイン：いや、合ってるはずだが・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{
								messageList.Add("アイン：やっぱ、探索を続けるか・・・"); eventList.Add(ActionEvent.None);
							}
						}
					}
					else
					{
						messageList.Add("ラナ：何ボーっとしてんのよ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：あ、いやいや。ッハッハッハ！"); eventList.Add(ActionEvent.None);
					}
				}
			}			
		}

		public static void Message12008_2(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (!GroundOne.WE.dungeonEvent213)
			{
				GroundOne.WE.dungeonEvent213 = true;

				if (!GroundOne.WE.dungeonEvent212 && !GroundOne.WE.dungeonEvent214)
				{
					messageList.Add("アイン：ん？なんだこのレバーは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：見たところ、駆動部は１箇所だけね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：どうする？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～～ん・・・アインはどう思うわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そうだなあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まだ開いてるとこ、あるんじゃねえかな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：そういえば、青扉っていくつかあったわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：他の場所をもう少し探索が要る。そう俺は思うぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：テキトーにレバー引かないワケ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：場合によるな。今回は引かない。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふーん。そう・・・まあ良いけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、他の場所でも探索してみる？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、行ってみようぜ。"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent212 && GroundOne.WE.dungeonEvent214) ||
					(GroundOne.WE.dungeonEvent212 && !GroundOne.WE.dungeonEvent214))
				{
					messageList.Add("アイン：おっ、レバー発見発見！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：やっぱり同じようなレバーね。特徴もまったく一緒よ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どうするの？もうレバー引いちゃうわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：俺はバカではない。今回も見送りだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、やけに慎重ね。頭でも打ったの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：違うっつうの。ただ、今回はアレだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板の文字がさ。もう消えてるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・っえ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あの看板。役割を終えたのかどうかがハッキリしねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：普通さ。役割終えたら、そのままにしておくと思わねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～ん、どうかしら。役割終えたから、文字が消えたんじゃないの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：今までそうだったか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：えっと、う～ん、全然気にしてなかったわよ、そんなの。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：だろ？じゃあ、多分まだ何かあるんだぜ。きっと。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っちょ、だからどうしてそうなるのよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まあ、良いじゃねえか。っしゃ、引き続き探索と行こうぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か無理やり切り上げてるし・・・まあ、慎重なのは良い事だけど。"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("ラナ：レバーがあったわよ？どうするの？アイン。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・そうだなあ。・・・ちょっと待ってくれ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板、見に行こうぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：すまねえな、ちょっと気になってるんだ。すぐソコだし良いだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ホントかしら・・・まあ、アインがそういうなら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：すまねえな。じゃ、看板見に行くとするか！"); eventList.Add(ActionEvent.None);
				}
			}
			else if (GroundOne.WE.dungeonEvent219)
			{
				messageList.Add("　　　　『  レバーはもう動かない  』"); eventList.Add(ActionEvent.None);
			}
			else
			{
				messageList.Add("　　　　『  レバーがある  』"); eventList.Add(ActionEvent.None);

				if (!GroundOne.WE.dungeonEvent215)
				{
					messageList.Add("アイン：（どうすっかな・・・やっぱ挑戦してみるべきか・・・）"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("アイン：（情報も揃ったことだし、挑戦みるか！）"); eventList.Add(ActionEvent.None);
				}

				// after
//				using (YesNoRequest yesno = new YesNoRequest())
//				{
//					yesno.StartPosition = FormStartPosition.CenterScreen;
//					yesno.ShowDialog();
//					if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
				if (true)
				{
					if (GroundOne.WE.dungeonEvent215)
					{
						if (!GroundOne.WE.dungeonEvent217_2)
						{
							GroundOne.WE.dungeonEvent217_2 = true;

						messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

						messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：えっと、分かってるわよね？さっきのヒントで。"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
						}
						else
						{

						messageList.Add("ラナ：あれ？またやってみるわけ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：あ、ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
						}
					}
					else
					{
						if (!GroundOne.WE.dungeonEvent217_2)
						{
							GroundOne.WE.dungeonEvent217_2 = true;
						messageList.Add("ラナ：あれ？試してみるわけ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ああ、ちょっとだけな。"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

						messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ッゲ、回数なんてあるのかよ？"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：あっ、言ってなかったっけ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：回数入力なんてマジかよ。じゃあ・・・"); eventList.Add(ActionEvent.None);
						}
						else
						{
						messageList.Add("ラナ：また、やってみるわけ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：ああ・・・"); eventList.Add(ActionEvent.None);

						messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：回数か。じゃあ・・・"); eventList.Add(ActionEvent.None);
						}
					}

					// after
//					using (RequestInput request = new RequestInput())
//					{
//						request.StartPosition = FormStartPosition.CenterScreen;
//						request.InputData = "1";
//						request.ShowDialog();
//						if (request.InputData == "8")
					if (true)
					{
						GroundOne.WE.dungeonEvent217 = true;
					}
					else
					{
						GroundOne.WE.dungeonEvent217 = false;
					}

					messageList.Add("　　　【ッガチャン】"); eventList.Add(ActionEvent.None);

					if (GroundOne.WE.dungeonEvent216 && GroundOne.WE.dungeonEvent217 && GroundOne.WE.dungeonEvent218)
					{
						GroundOne.WE.dungeonEvent219 = true;

						messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

						// after
//							blueWallBottom[7 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallTop[8 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallLeft[11 * Database.TRUTH_DUNGEON_COLUMN + 50] = false;
//							blueWallRight[11 * Database.TRUTH_DUNGEON_COLUMN + 49] = false;
//							blueWallLeft[14 * Database.TRUTH_DUNGEON_COLUMN + 59] = false;
//							blueWallRight[14 * Database.TRUTH_DUNGEON_COLUMN + 58] = false;

						messageList.Add("アイン：っしゃ！来たぜ、ラナ！　ッハッハッハ！！"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：まあ順当って感じよね♪"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：また、どっかの青扉が開いているだろうし、行ってみようぜ！"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：じゃあ、引き続き探索と行きますか♪"); eventList.Add(ActionEvent.None);
					}
					else
					{
						messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

						messageList.Add("ラナ：・・・何も起こらないけど？"); eventList.Add(ActionEvent.None);

						if (GroundOne.WE.dungeonEvent215)
						{
							messageList.Add("アイン：いや、合ってるはずだが・・・"); eventList.Add(ActionEvent.None);
						}
						else
						{
							messageList.Add("アイン：やっぱ、探索を続けるか・・・"); eventList.Add(ActionEvent.None);
						}
					}
				}
				else
				{
					messageList.Add("ラナ：何ボーっとしてんのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あ、いやいや。ッハッハッハ！"); eventList.Add(ActionEvent.None);
				}
			}
		}

		public static void Message12008_3(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			if (!GroundOne.WE.dungeonEvent214)
			{
				GroundOne.WE.dungeonEvent214 = true;

				if (!GroundOne.WE.dungeonEvent212 && !GroundOne.WE.dungeonEvent213)
				{
					messageList.Add("アイン：ん？なんだこのレバーは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：見たところ、駆動部は１箇所だけね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：どうする？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～～ん・・・アインはどう思うわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そうだなあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まだ開いてるとこ、あるんじゃねえかな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：そういえば、青扉っていくつかあったわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：他の場所をもう少し探索が要る。そう俺は思うぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：テキトーにレバー引かないワケ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：場合によるな。今回は引かない。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふーん。そう・・・まあ良いけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、他の場所でも探索してみる？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、行ってみようぜ。"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent212 && GroundOne.WE.dungeonEvent213) ||
					(GroundOne.WE.dungeonEvent212 && !GroundOne.WE.dungeonEvent213))
				{
					messageList.Add("アイン：おっ、レバー発見発見！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：やっぱり同じようなレバーね。特徴もまったく一緒よ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どうするの？もうレバー引いちゃうわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：俺はバカではない。今回も見送りだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、やけに慎重ね。頭でも打ったの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：違うっつうの。ただ、今回はアレだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板の文字がさ。もう消えてるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・っえ！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あの看板。役割を終えたのかどうかがハッキリしねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：普通さ。役割終えたら、そのままにしておくと思わねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：う～ん、どうかしら。役割終えたから、文字が消えたんじゃないの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：今までそうだったか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：えっと、う～ん、全然気にしてなかったわよ、そんなの。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：だろ？じゃあ、多分まだ何かあるんだぜ。きっと。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っちょ、だからどうしてそうなるのよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：まあ、良いじゃねえか。っしゃ、引き続き探索と行こうぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か無理やり切り上げてるし・・・もう・・・"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("ラナ：レバーがあったわよ？どうするの？アイン。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・そうだなあ。・・・ちょっと待ってくれ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板、見に行こうぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ちょっと気になってるんだ。すぐソコだし良いだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ホントかしら・・・まあ、アインがそういうなら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：すまねえな。じゃ、看板見に行くとするか！"); eventList.Add(ActionEvent.None);
				}
			}
			else if (GroundOne.WE.dungeonEvent219)
			{
				messageList.Add("　　　　『  レバーはもう動かない  』"); eventList.Add(ActionEvent.None);
			}
			else
			{
				messageList.Add("　　　　『  レバーがある  』"); eventList.Add(ActionEvent.None);

				if (!GroundOne.WE.dungeonEvent215)
				{
					messageList.Add("アイン：（どうすっかな・・・やっぱ挑戦してみるべきか・・・）"); eventList.Add(ActionEvent.None);
				}
				else
				{
					messageList.Add("アイン：（情報も揃ったことだし、挑戦みるか！）"); eventList.Add(ActionEvent.None);
				}

				// aftedr
//				using (YesNoRequest yesno = new YesNoRequest())
				{
//					yesno.StartPosition = FormStartPosition.CenterScreen;
//					yesno.ShowDialog();
//					if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
					if (true)
					{
						if (GroundOne.WE.dungeonEvent215)
						{
							if (!GroundOne.WE.dungeonEvent218_2)
							{
								GroundOne.WE.dungeonEvent218_2 = true;

								messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("ラナ：えっと、分かってるわよね？さっきのヒントで。"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{

								messageList.Add("ラナ：あれ？またやってみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：あ、ああ、任せておけ！　回数は・・・"); eventList.Add(ActionEvent.None);
							}
						}
						else
						{
							if (!GroundOne.WE.dungeonEvent218_2)
							{
								GroundOne.WE.dungeonEvent218_2 = true;
								messageList.Add("ラナ：あれ？試してみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ、ちょっとだけな。"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ええっと、何々。注意書きがあるな。"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ッゲ、回数なんてあるのかよ？"); eventList.Add(ActionEvent.None);

								messageList.Add("ラナ：あっ、言ってなかったっけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：回数入力なんてマジかよ。じゃあ・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{
								messageList.Add("ラナ：また、やってみるわけ？"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：ああ・・・"); eventList.Add(ActionEvent.None);

								messageList.Add("　　　　『  レバーの回数を示せ  』"); eventList.Add(ActionEvent.None);

								messageList.Add("アイン：回数か。じゃあ・・・"); eventList.Add(ActionEvent.None);
							}
						}

						// after
//						using (RequestInput request = new RequestInput())
//						{
//							request.StartPosition = FormStartPosition.CenterScreen;
//							request.InputData = "1";
//							request.ShowDialog();
//							if (request.InputData == "3")
//							{
//								GroundOne.WE.dungeonEvent218 = true;
//							}
//							else
//							{
//								GroundOne.WE.dungeonEvent218 = false;
//							}
//						}

						messageList.Add("　　　【ッガチャン】"); eventList.Add(ActionEvent.None);

						if (GroundOne.WE.dungeonEvent216 && GroundOne.WE.dungeonEvent217 && GroundOne.WE.dungeonEvent218)
						{
							GroundOne.WE.dungeonEvent219 = true;

							messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』");

							// after
//							blueWallBottom[7 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallTop[8 * Database.TRUTH_DUNGEON_COLUMN + 38] = false;
//							blueWallLeft[11 * Database.TRUTH_DUNGEON_COLUMN + 50] = false;
//							blueWallRight[11 * Database.TRUTH_DUNGEON_COLUMN + 49] = false;
//							blueWallLeft[14 * Database.TRUTH_DUNGEON_COLUMN + 59] = false;
//							blueWallRight[14 * Database.TRUTH_DUNGEON_COLUMN + 58] = false;

							messageList.Add("アイン：っしゃ！来たぜ、ラナ！　ッハッハッハ！！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：まあ順当って感じよね♪"); eventList.Add(ActionEvent.None);

							messageList.Add("アイン：また、どっかの青扉が開いているだろうし、行ってみようぜ！"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：じゃあ、引き続き探索と行きますか♪"); eventList.Add(ActionEvent.None);
						}
						else
						{
							messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

							messageList.Add("ラナ：・・・何も起こらないけど？"); eventList.Add(ActionEvent.None);

							if (GroundOne.WE.dungeonEvent215)
							{
								messageList.Add("アイン：いや、合ってるはずだが・・・"); eventList.Add(ActionEvent.None);
							}
							else
							{
								messageList.Add("アイン：やっぱ、探索を続けるか・・・"); eventList.Add(ActionEvent.None);
							}
						}
					}
					else
					{
						messageList.Add("ラナ：何ボーっとしてんのよ？"); eventList.Add(ActionEvent.None);

						messageList.Add("アイン：あ、いやいや。ッハッハッハ！"); eventList.Add(ActionEvent.None);
					}
				}
			}
		}
		#endregion
		#region "知の部屋、フェーズ３"
		public static void Message12009(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			// after
			//UpdateUnknownTileArea24();
			if (!GroundOne.WE.dungeonEvent221)
			{
				GroundOne.WE.dungeonEvent221 = true;

				if (!GroundOne.WE.dungeonEvent222 && !GroundOne.WE.dungeonEvent223)
				{
					messageList.Add("ラナ：・・・何も無いわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・何か・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どしたの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：この部屋、何かそこらじゅうに文字が刻まれてねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：え？？　ウソ、どこに！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【鳥が歌い、木々が囁き始める】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【天は青く照らし、地は新緑を謳歌する】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【闇が夜へと誘い、光が昼へと道を示す】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【水、流れ落ち、偉大なる海、天へと還り、無限循環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【火、あらゆる場所、可能な場を生めつくし、創元浄化】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いったい何なんだこれは・・・ぜんぜん分かんねえな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・え？　え？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ちょっとアイン、大丈夫？アンタ、正真正銘のバカになったんじゃない？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ラナこそ、どこ見てんだ？　見えないのか？　そこら中に書いてあるだろ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：いや、ホントぜんぜん見えないけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：じゃあ、俺だけが見えてるってワケか。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、何か私の出る幕なさそうね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あ、いやいや。。。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：良いわよ、そんな気を使わなくても♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そっか、悪いな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：よし、意味ありげな内容だからな。キッチリココは記憶していくぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：大丈夫？メモなら貸してあげるけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent222 && GroundOne.WE.dungeonEvent223) ||
					(GroundOne.WE.dungeonEvent222 && !GroundOne.WE.dungeonEvent223))
				{
					messageList.Add("ラナ：・・・どう？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・ああ、この部屋も同じだせ。ビッシリと書いてある。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【鳥が歌い、木々が囁き始める】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【天は青く照らし、地は新緑を謳歌する】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【闇が夜へと誘い、光が昼へと道を示す】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【水、流れ落ち、偉大なる海、天へと還り、無限循環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【火、あらゆる場所、可能な場を生めつくし、創元浄化】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：前の部屋もそうだが、まったくもって分からねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・そう・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ねえ、何か見たことあるんじゃないの？実は。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いや？初めてだぜ、こんな詩を見るのは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：詩って何で分かるわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そりゃあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　あれ？　何でだ？？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：バカアインはこれだから・・・ハアアアァァァ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：い、いやいや、そうじゃねえって。あれ、何でだっけ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ良いわよ。ッホラ、とっとと記憶しちゃいなさいよ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そんなサクサク記憶出来るかつうの。まあ、待て待て。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：え～っと・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if (GroundOne.WE.dungeonEvent222 && GroundOne.WE.dungeonEvent223)
				{
					messageList.Add("ラナ：・・・この部屋も書いてあるの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【鳥が歌い、木々が囁き始める】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【天は青く照らし、地は新緑を謳歌する】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【闇が夜へと誘い、光が昼へと道を示す】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【水、流れ落ち、偉大なる海、天へと還り、無限循環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【火、あらゆる場所、可能な場を生めつくし、創元浄化】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：以上だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っふううううぅぅ・・・。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：アイン、なんだか汗ビッショリよ。大丈夫なの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、ちょっと記憶量が多いだけだ。大丈夫だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：記憶量が多いって・・・そういう問題なのかしら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だって。任せろ！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か心配だけど・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：気にするな。汗ぐらいどうって事ねえよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っさてと、次行くぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どこに行くのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板に決まってるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っえ、あの看板、また見るの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだ。きっと答えが出る。出せるようになるハズだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ、あんまり無理しないでよね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
				}
			}
			else
			{
				messageList.Add("        【鳥が歌い、木々が囁き始める】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【天は青く照らし、地は新緑を謳歌する】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【闇が夜へと誘い、光が昼へと道を示す】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【水、流れ落ち、偉大なる海、天へと還り、無限循環】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【火、あらゆる場所、可能な場を生めつくし、創元浄化】"); eventList.Add(ActionEvent.None);
			}			
		}

		public static void Message12009_2(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			// after
			//UpdateUnknownTileArea25();

			if (!GroundOne.WE.dungeonEvent222)
			{
				GroundOne.WE.dungeonEvent222 = true;

				if (!GroundOne.WE.dungeonEvent221 && !GroundOne.WE.dungeonEvent223)
				{
					messageList.Add("ラナ：・・・何も無いわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・何か・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どしたの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：この部屋、何かそこらじゅうに文字が刻まれてねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：え？？　ウソ、どこに！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【嵐、万物なる生成要素、一から零へと変化させる】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【死、この世における絶対的な平等の象徴】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【生、偉大なる母、厳格なる父より永久の確約】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【神、全創生、全法則、全にして無条件の存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【人、誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いったい何なんだこれは・・・ぜんぜん分かんねえな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・え？　え？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ちょっとアイン、大丈夫？アンタ、正真正銘のバカになったんじゃない？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ラナこそ、どこ見てんだ？　見えないのか？　そこら中に書いてあるだろ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：いや、ホントぜんぜん見えないけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：じゃあ、俺だけが見えてるってワケか。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、何か私の出る幕なさそうね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あ、いやいや。。。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：良いわよ、そんな気を使わなくても♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そっか、悪いな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：よし、意味ありげな内容だからな。キッチリココは記憶していくぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：大丈夫？メモなら貸してあげるけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent221 && GroundOne.WE.dungeonEvent223) ||
					(GroundOne.WE.dungeonEvent221 && !GroundOne.WE.dungeonEvent223))
				{
					messageList.Add("ラナ：・・・どう？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・ああ、この部屋も同じだせ。ビッシリと書いてある。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【嵐、万物なる生成要素、一から零へと変化させる】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【死、この世における絶対的な平等の象徴】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【生、偉大なる母、厳格なる父より永久の確約】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【神、全創生、全法則、全にして無条件の存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【人、誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：前の部屋もそうだが、まったくもって分からねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・そう・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ねえ、何か見たことあるんじゃないの？実は。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いや？初めてだぜ、こんな詩を見るのは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：詩って何で分かるわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そりゃあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　あれ？　何でだ？？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：バカアインはこれだから・・・ハアアアァァァ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：い、いやいや、そうじゃねえって。あれ、何でだっけ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ良いわよ。ッホラ、とっとと記憶しちゃいなさいよ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そんなサクサク記憶出来るかつうの。まあ、待て待て。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：え～っと・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if (GroundOne.WE.dungeonEvent221 && GroundOne.WE.dungeonEvent223)
				{
					messageList.Add("ラナ：・・・この部屋も書いてあるの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【嵐、万物なる生成要素、一から零へと変化させる】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【死、この世における絶対的な平等の象徴】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【生、偉大なる母、厳格なる父より永久の確約】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【神、全創生、全法則、全にして無条件の存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【人、誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：以上だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っふううううぅぅ・・・。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：アイン、なんだか汗ビッショリよ。大丈夫なの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、ちょっと記憶量が多いだけだ。大丈夫だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：記憶量が多いって・・・そういう問題なのかしら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だって。任せろ！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か心配だけど・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：気にするな。汗ぐらいどうって事ねえよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っさてと、次行くぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どこに行くのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板に決まってるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っえ、あの看板、また見るの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだ。きっと答えが出る。出せるようになるハズだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ、あんまり無理しないでよね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
				}
			}
			else
			{
				messageList.Add("        【嵐、万物なる生成要素、一から零へと変化させる】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【死、この世における絶対的な平等の象徴】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【生、偉大なる母、厳格なる父より永久の確約】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【神、全創生、全法則、全にして無条件の存在】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【人、誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在】"); eventList.Add(ActionEvent.None);
			}
		}

		public static void Message12009_3(ref List<string> messageList, ref List<ActionEvent> eventList)
		{
			// after
			//UpdateUnknownTileArea26();
			if (!GroundOne.WE.dungeonEvent223)
			{
				GroundOne.WE.dungeonEvent223 = true;

				if (!GroundOne.WE.dungeonEvent221 && !GroundOne.WE.dungeonEvent222)
				{
					messageList.Add("ラナ：・・・何も無いわね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・何か・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どしたの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：この部屋、何かそこらじゅうに文字が刻まれてねえか？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：え？？　ウソ、どこに！？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【理、神と人、鳥、木々、全生物における連続の理そこに見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【空、在るべきもの、成るべくして成り、在るべくして現存と見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【相、完全調和への導き、交わることの無い絶対双極、見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【永遠、終わらない所へ、終わりと始まりが連続する永遠環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【世界、あなた、そしてわたしが居た場所へ。無限に続くこの世界】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いったい何なんだこれは・・・ぜんぜん分かんねえな。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・え？　え？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ちょっとアイン、大丈夫？アンタ、正真正銘のバカになったんじゃない？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ラナこそ、どこ見てんだ？　見えないのか？　そこら中に書いてあるだろ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：いや、ホントぜんぜん見えないけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：じゃあ、俺だけが見えてるってワケか。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ふ～ん、何か私の出る幕なさそうね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：あ、いやいや。。。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：良いわよ、そんな気を使わなくても♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そっか、悪いな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：よし、意味ありげな内容だからな。キッチリココは記憶していくぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：大丈夫？メモなら貸してあげるけど。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if ((!GroundOne.WE.dungeonEvent221 && GroundOne.WE.dungeonEvent222) ||
					(GroundOne.WE.dungeonEvent221 && !GroundOne.WE.dungeonEvent222))
				{
					messageList.Add("ラナ：・・・どう？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・ああ、この部屋も同じだせ。ビッシリと書いてある。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【理、神と人、鳥、木々、全生物における連続の理そこに見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【空、在るべきもの、成るべくして成り、在るべくして現存と見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【相、完全調和への導き、交わることの無い絶対双極、見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【永遠、終わらない所へ、終わりと始まりが連続する永遠環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【世界、あなた、そしてわたしが居た場所へ。無限に続くこの世界】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：と、以上だな。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：前の部屋もそうだが、まったくもって分からねえ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：・・・そう・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：ねえ、何か見たことあるんじゃないの？実は。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：いや？初めてだぜ、こんな詩を見るのは。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：詩って何で分かるわけ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そりゃあ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：・・・　・・・　あれ？　何でだ？？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：バカアインはこれだから・・・ハアアアァァァ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：い、いやいや、そうじゃねえって。あれ、何でだっけ・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ良いわよ。ッホラ、とっとと記憶しちゃいなさいよ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：そんなサクサク記憶出来るかつうの。まあ、待て待て。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：え～っと・・・　・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だ。バッチリ記憶したぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：じゃあ、引き続き探索と行きましょ♪"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：オーケー！"); eventList.Add(ActionEvent.None);
				}
				else if (GroundOne.WE.dungeonEvent221 && GroundOne.WE.dungeonEvent222)
				{
					messageList.Add("ラナ：・・・この部屋も書いてあるの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、読むぜ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【理、神と人、鳥、木々、全生物における連続の理そこに見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【空、在るべきもの、成るべくして成り、在るべくして現存と見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【相、完全調和への導き、交わることの無い絶対双極、見つけたり】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【永遠、終わらない所へ、終わりと始まりが連続する永遠環】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：【世界、あなた、そしてわたしが居た場所へ。無限に続くこの世界】"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：以上だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っふううううぅぅ・・・。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：アイン、なんだか汗ビッショリよ。大丈夫なの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、ちょっと記憶量が多いだけだ。大丈夫だ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：記憶量が多いって・・・そういう問題なのかしら。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：大丈夫だって。任せろ！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：何か心配だけど・・・"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：気にするな。汗ぐらいどうって事ねえよ。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：っさてと、次行くぜ！"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：どこに行くのよ？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：看板に決まってるだろ？"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：っえ、あの看板、また見るの？"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ、そうだ。きっと答えが出る。出せるようになるハズだ。"); eventList.Add(ActionEvent.None);

					messageList.Add("ラナ：まあ、あんまり無理しないでよね。"); eventList.Add(ActionEvent.None);

					messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
				}
			}
			else
			{
				messageList.Add("        【理、神と人、鳥、木々、全生物における連続の理そこに見つけたり】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【空、在るべきもの、成るべくして成り、在るべくして現存と見つけたり】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【相、完全調和への導き、交わることの無い絶対双極、見つけたり】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【永遠、終わらない所へ、終わりと始まりが連続する永遠環】"); eventList.Add(ActionEvent.None);

				messageList.Add("        【世界、あなた、そしてわたしが居た場所へ。無限に続くこの世界】"); eventList.Add(ActionEvent.None);
			}
		}
		#endregion
        #region "知の部屋、正しき順序看板"
        public static void Message12010(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // after 番号割り振りを再設定する
            if ((GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 39]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 41]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 43]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 45]) &&
                (GroundOne.Truth_KnownTileInfo2[11 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[12 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[12 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[12 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[12 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[12 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 39]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 41]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 43]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 45]) &&
                (GroundOne.Truth_KnownTileInfo2[13 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[14 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[14 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[14 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[14 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[14 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 39]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 41]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 43]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 45]) &&
                (GroundOne.Truth_KnownTileInfo2[15 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[16 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[16 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[16 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[16 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[16 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 39]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 41]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 43]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 45]) &&
                (GroundOne.Truth_KnownTileInfo2[17 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[18 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[18 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[18 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[18 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[18 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&

                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 38]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 39]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 40]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 41]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 42]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 43]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 44]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 45]) &&
                (GroundOne.Truth_KnownTileInfo2[19 * Database.TRUTH_DUNGEON_COLUMN + 46]) &&
                !GroundOne.WE.dungeonEvent229)
            {
                GroundOne.WE.dungeonEvent229 = true;
                if (!GroundOne.WE.dungeonEvent228)
                {
                    GroundOne.WE.dungeonEvent228 = true;
                    messageList.Add("アイン：ん、看板があるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　　『  正しき順序、正しき道筋を示せ。  』"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("ラナ：数字が床に刻まれていたわね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっ、ラナ。ひょっとして何か分かったのか？"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableThirdCharacter)
                {
                    messageList.Add ("ランディス：（ッオイ、てめぇ）");
                    eventList.Add (ActionEvent.None);

                    messageList.Add ("アイン：（んだよ、良いじゃねえか）");
                    eventList.Add (ActionEvent.None);
                }

                messageList.Add("ラナ：床にある数字があるじゃない？　あれを順序良く踏んで行けば良いのよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はこういう「正しき」って表現はぜんっぜん駄目だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ここはお前に任せたぜ、ラナ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：失敗しても文句言わないでよね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：良いって良いって。じゃあ、頼んだぜ！"); eventList.Add(ActionEvent.None);

                GroundOne.WE.dungeonEvent230_0 = true;
                GroundOne.WE.dungeonEvent230_01 = false;
                GroundOne.WE.dungeonEvent230_1 = false;
                GroundOne.WE.dungeonEvent230_11 = false;
                GroundOne.WE.dungeonEvent230_12 = false;
                GroundOne.WE.dungeonEvent230_2 = false;
                GroundOne.WE.dungeonEvent230_21 = false;
                GroundOne.WE.dungeonEvent230_3 = false;
                GroundOne.WE.dungeonEvent230_31 = false;
                GroundOne.WE.dungeonEvent230_32 = false;
                GroundOne.WE.dungeonEvent230_4 = false;
                GroundOne.WE.dungeonEvent230_41 = false;
                GroundOne.WE.dungeonEvent230_42 = false;
                GroundOne.WE.dungeonEvent230_5 = false;
                GroundOne.WE.dungeonEvent230_51 = false;
                GroundOne.WE.dungeonEvent230_52 = false;
                GroundOne.WE.dungeonEvent230_6 = false;
                GroundOne.WE.dungeonEvent230_61 = false;
                GroundOne.WE.dungeonEvent230_62 = false;
                GroundOne.WE.dungeonEvent230_63 = false;
                GroundOne.WE.dungeonEvent230_64 = false;
                GroundOne.WE.dungeonEvent230_7 = false;
                GroundOne.WE.dungeonEvent230_71 = false;
                GroundOne.WE.dungeonEvent230_72 = false;
                GroundOne.WE.dungeonEvent230_8 = false;
            }
            else
            {
                if (!GroundOne.WE.dungeonEvent228)
                {
                    GroundOne.WE.dungeonEvent228 = true;
                    messageList.Add("アイン：ん、看板があるな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　　『  正しき順序、正しき道筋を示せ。  』"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：正しき順序・・・か・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ねえ、周りを探索してみない？このままじゃ分からないわよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ、そうだな。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("　　　　『  正しき順序、正しき道筋を示せ。  』"); eventList.Add(ActionEvent.None);
                }

                GroundOne.WE.dungeonEvent230_0 = true;
                GroundOne.WE.dungeonEvent230_01 = false;
                GroundOne.WE.dungeonEvent230_1 = false;
                GroundOne.WE.dungeonEvent230_11 = false;
                GroundOne.WE.dungeonEvent230_12 = false;
                GroundOne.WE.dungeonEvent230_2 = false;
                GroundOne.WE.dungeonEvent230_21 = false;
                GroundOne.WE.dungeonEvent230_3 = false;
                GroundOne.WE.dungeonEvent230_31 = false;
                GroundOne.WE.dungeonEvent230_32 = false;
                GroundOne.WE.dungeonEvent230_4 = false;
                GroundOne.WE.dungeonEvent230_41 = false;
                GroundOne.WE.dungeonEvent230_42 = false;
                GroundOne.WE.dungeonEvent230_5 = false;
                GroundOne.WE.dungeonEvent230_51 = false;
                GroundOne.WE.dungeonEvent230_52 = false;
                GroundOne.WE.dungeonEvent230_6 = false;
                GroundOne.WE.dungeonEvent230_61 = false;
                GroundOne.WE.dungeonEvent230_62 = false;
                GroundOne.WE.dungeonEvent230_63 = false;
                GroundOne.WE.dungeonEvent230_64 = false;
                GroundOne.WE.dungeonEvent230_7 = false;
                GroundOne.WE.dungeonEvent230_71 = false;
                GroundOne.WE.dungeonEvent230_72 = false;
                GroundOne.WE.dungeonEvent230_8 = false;
            }            
        }
        #endregion
        #region "知の部屋、正しき順序フラグ"
        public static void Message12011(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && !GroundOne.WE.dungeonEvent230_01)
                {
                    GroundOne.WE.dungeonEvent230_01 = true;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    !GroundOne.WE.dungeonEvent230_1)
                {
                    GroundOne.WE.dungeonEvent230_1 = true;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && !GroundOne.WE.dungeonEvent230_11)
                {
                    GroundOne.WE.dungeonEvent230_11 = true;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_4(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && !GroundOne.WE.dungeonEvent230_12)
                {
                    GroundOne.WE.dungeonEvent230_12 = true;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_5(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    !GroundOne.WE.dungeonEvent230_2)
                {
                    GroundOne.WE.dungeonEvent230_2 = true;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_6(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && !GroundOne.WE.dungeonEvent230_21)
                {
                    GroundOne.WE.dungeonEvent230_21 = true;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_7(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    !GroundOne.WE.dungeonEvent230_3)
                {
                    GroundOne.WE.dungeonEvent230_3 = true;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_8(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && !GroundOne.WE.dungeonEvent230_31)
                {
                    GroundOne.WE.dungeonEvent230_31 = true;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_9(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && !GroundOne.WE.dungeonEvent230_32)
                {
                    GroundOne.WE.dungeonEvent230_32 = true;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_10(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    !GroundOne.WE.dungeonEvent230_4)
                {
                    GroundOne.WE.dungeonEvent230_4 = true;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_11(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && !GroundOne.WE.dungeonEvent230_41)
                {
                    GroundOne.WE.dungeonEvent230_41 = true;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_12(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && !GroundOne.WE.dungeonEvent230_42)
                {
                    GroundOne.WE.dungeonEvent230_42 = true;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_13(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    !GroundOne.WE.dungeonEvent230_5)
                {
                    GroundOne.WE.dungeonEvent230_5 = true;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_14(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && !GroundOne.WE.dungeonEvent230_51)
                {
                    GroundOne.WE.dungeonEvent230_51 = true;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_15(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && !GroundOne.WE.dungeonEvent230_52)
                {
                    GroundOne.WE.dungeonEvent230_52 = true;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_16(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    !GroundOne.WE.dungeonEvent230_6)
                {
                    GroundOne.WE.dungeonEvent230_6 = true;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_17(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && !GroundOne.WE.dungeonEvent230_61)
                {
                    GroundOne.WE.dungeonEvent230_61 = true;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }

        public static void Message12011_18(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && !GroundOne.WE.dungeonEvent230_62)
                {
                    GroundOne.WE.dungeonEvent230_62 = true;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_19(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && !GroundOne.WE.dungeonEvent230_63)
                {
                    GroundOne.WE.dungeonEvent230_63 = true;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_20(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && GroundOne.WE.dungeonEvent230_63 && !GroundOne.WE.dungeonEvent230_64)
                {
                    GroundOne.WE.dungeonEvent230_64 = true;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_21(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && GroundOne.WE.dungeonEvent230_63 && GroundOne.WE.dungeonEvent230_64 &&
                    !GroundOne.WE.dungeonEvent230_7)
                {
                    GroundOne.WE.dungeonEvent230_7 = true;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_22(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && GroundOne.WE.dungeonEvent230_63 && GroundOne.WE.dungeonEvent230_64 &&
                    GroundOne.WE.dungeonEvent230_7 && !GroundOne.WE.dungeonEvent230_71)
                {
                    GroundOne.WE.dungeonEvent230_71 = true;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }

        public static void Message12011_23(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && GroundOne.WE.dungeonEvent230_63 && GroundOne.WE.dungeonEvent230_64 &&
                    GroundOne.WE.dungeonEvent230_7 && GroundOne.WE.dungeonEvent230_71 && !GroundOne.WE.dungeonEvent230_72)
                {
                    GroundOne.WE.dungeonEvent230_72 = true;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }            
        }
        #endregion
        #region "知の部屋、正しき順序解答"
        public static void Message12012(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.dungeonEvent229 && !GroundOne.WE.dungeonEvent230)
            {
                if (GroundOne.WE.dungeonEvent230_0 && GroundOne.WE.dungeonEvent230_01 &&
                    GroundOne.WE.dungeonEvent230_1 && GroundOne.WE.dungeonEvent230_11 && GroundOne.WE.dungeonEvent230_12 &&
                    GroundOne.WE.dungeonEvent230_2 && GroundOne.WE.dungeonEvent230_21 &&
                    GroundOne.WE.dungeonEvent230_3 && GroundOne.WE.dungeonEvent230_31 && GroundOne.WE.dungeonEvent230_32 &&
                    GroundOne.WE.dungeonEvent230_4 && GroundOne.WE.dungeonEvent230_41 && GroundOne.WE.dungeonEvent230_42 &&
                    GroundOne.WE.dungeonEvent230_5 && GroundOne.WE.dungeonEvent230_51 && GroundOne.WE.dungeonEvent230_52 &&
                    GroundOne.WE.dungeonEvent230_6 && GroundOne.WE.dungeonEvent230_61 && GroundOne.WE.dungeonEvent230_62 && GroundOne.WE.dungeonEvent230_63 && GroundOne.WE.dungeonEvent230_64 &&
                    GroundOne.WE.dungeonEvent230_7 && GroundOne.WE.dungeonEvent230_71 && GroundOne.WE.dungeonEvent230_72 &&
                    !GroundOne.WE.dungeonEvent230_8)
                {
                    GroundOne.WE.dungeonEvent230_8 = true;
                    GroundOne.WE.dungeonEvent230 = true;

                    messageList.Add("　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.BlueOpenRight);

                    messageList.Add("ラナ：やったわ！　正解だったみたいね♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：やるじゃねえか！　今のどういう仕組みだったんだよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：簡単よ、１～８までの数字タイルを順序良く踏めば良かったのよ♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へえ、なるほどな・・・確かにそういやそうだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：あっ、ホラホラ。こっちに何かあるわよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：本当だな。どれどれ？"); eventList.Add(ActionEvent.None);

                    messageList.Add(""); eventList.Add(ActionEvent.UpdateLocationRight);

                    messageList.Add("アイン：ん？　これは・・・？"); eventList.Add(ActionEvent.None);

                    messageList.Add(Database.RARE_SOUJUTENSHI_NO_GOFU); eventList.Add(ActionEvent.DungeonGetTreasure);

                    messageList.Add("アイン：何だこれ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：あっ♪　蒼授天使の護符じゃない、やったわねアイン♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何だ知ってるのかよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：って、知らないアンタの方がどーかしてるわよ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ウェクスラー大陸では、このシリーズ物は有名でしょ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、俺そういうの全然知らないんだって、教えてくれよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ホンット疎いわねそういうの・・・ハアアアァァァ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：この大陸には、神の使い手として４種の天使が遣わされた"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：炎授天使、蒼授天使、断罪天使、珀流天使"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：今回見つけたのは、その中の蒼授天使。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：蒼授天使さまは我々に大気・生命・水の恩恵を与えてくれてるわけよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：へえそうなんだ。　全然知らなかったぜ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：バカアインは勉強もそうだけど、もう少し世の中の事を知った方が良いわよ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あ、ああ。分かったわかった。"); eventList.Add(ActionEvent.None);

                    if (GroundOne.WE.AvailableThirdCharacter)
                    {
                        messageList.Add("ランディス：ッカッカッカ、小娘。その雰囲気じゃ、今の話も右から左になってるぞ。"); eventList.Add(ActionEvent.None);

                        messageList.Add("ラナ：っえ！？そうなんですか！？　ちょっとアイン！？"); eventList.Add(ActionEvent.None);

                        messageList.Add("アイン：い、いやいや！　聞いてます、聞いてますって。"); eventList.Add(ActionEvent.None);
                    }

                    messageList.Add("アイン：ま、まあ何かしらの防衛的効果が高いって話だよな？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ハアアァァ・・・"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    GroundOne.WE.dungeonEvent230_0 = false;
                    GroundOne.WE.dungeonEvent230_01 = false;
                    GroundOne.WE.dungeonEvent230_1 = false;
                    GroundOne.WE.dungeonEvent230_11 = false;
                    GroundOne.WE.dungeonEvent230_12 = false;
                    GroundOne.WE.dungeonEvent230_2 = false;
                    GroundOne.WE.dungeonEvent230_21 = false;
                    GroundOne.WE.dungeonEvent230_3 = false;
                    GroundOne.WE.dungeonEvent230_31 = false;
                    GroundOne.WE.dungeonEvent230_32 = false;
                    GroundOne.WE.dungeonEvent230_4 = false;
                    GroundOne.WE.dungeonEvent230_41 = false;
                    GroundOne.WE.dungeonEvent230_42 = false;
                    GroundOne.WE.dungeonEvent230_5 = false;
                    GroundOne.WE.dungeonEvent230_51 = false;
                    GroundOne.WE.dungeonEvent230_52 = false;
                    GroundOne.WE.dungeonEvent230_6 = false;
                    GroundOne.WE.dungeonEvent230_61 = false;
                    GroundOne.WE.dungeonEvent230_62 = false;
                    GroundOne.WE.dungeonEvent230_63 = false;
                    GroundOne.WE.dungeonEvent230_64 = false;
                    GroundOne.WE.dungeonEvent230_7 = false;
                    GroundOne.WE.dungeonEvent230_71 = false;
                    GroundOne.WE.dungeonEvent230_72 = false;
                    GroundOne.WE.dungeonEvent230_8 = false;
                }
            }
        }
        #endregion
        #region "技の部屋、エリアＡ"
        public static void Message12013(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.dungeonEvent233)
            {
                GroundOne.WE.dungeonEvent233 = true;

                messageList.Add("アイン：看板があるぜ。なになに？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『  財宝を求める挑戦者、俊足を持って挑め。』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっしゃ、来たぜ！　お宝だ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：なんだか、ありきたりな内容ね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：挑戦者って事は・・・誰か一人選ぶって事かしら？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃ、頼んだぜ、ラナ！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っちょ、何で私なわけ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：こういうのは、バカアインが適役じゃないかしら♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：マジかよ？　俺は絶対ラナの方が良いと思うんだが。"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.AvailableThirdCharacter)
                {
                    messageList.Add("ラナ：ランディスさんは、どちらが行くのが適任だと思いますか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っあ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ランディス：ザコが先頭切っていく。　当然だろ。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("ラナ：私はこういう挑戦とかそういった類は苦手なのよ。"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("アイン：ッグ・・・はいはい、分かりましたよ。　俺が行きますよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っよし、決まりね♪　応援してるわ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：要は、サクサクと進めって事だろ？　やってやるさ！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("　　　　『  挑戦者、俊足を持って挑め。』"); eventList.Add(ActionEvent.None);
            }
            if (!GroundOne.WE.dungeonEvent233_Complete)
            {
                messageList.Add(""); eventList.Add(ActionEvent.DungeonAgilityRoomStop);
            }            
        }
        #endregion
        #region "知の部屋、正しき順序看板"
        public static void Message12014(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "知の部屋、正しき順序フラグ"
        public static void Message12015(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_2(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_3(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_4(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_5(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_6(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_7(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_8(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_9(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_10(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_11(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_12(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_13(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_14(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_15(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_16(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_17(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_18(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_19(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_20(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_21(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_22(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12015_23(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "知の部屋、正しき順序解答"
        public static void Message12016(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "知の部屋、複合レバーの看板１"
        public static void Message12017(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "知の部屋、複合レバー１－１"
        public static void Message12018(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "知の部屋、複合レバー１－２"
        public static void Message12019(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＡ"
        public static void Message12020(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12021(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12022(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＢ"
        public static void Message12023(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12024(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12025(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＣ"
        public static void Message12026(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12027(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12028(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＤ"
        public static void Message12029(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12030(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12031(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＥ"
        public static void Message12032(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12033(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }

        public static void Message12034(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、エリアＦ（最後一個前）"
        public static void Message12035(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、ヒント１"
        public static void Message12036(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材１"
        public static void Message12037(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材２"
        public static void Message12038(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材３"
        public static void Message12039(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材４"
        public static void Message12040(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材５"
        public static void Message12041(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材６"
        public static void Message12042(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材７"
        public static void Message12043(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材８"
        public static void Message12044(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材９"
        public static void Message12045(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、題材１０"
        public static void Message12046(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス１"
        public static void Message12047(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス２"
        public static void Message12048(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス３"
        public static void Message12049(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス４"
        public static void Message12050(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス５"
        public static void Message12051(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、ボス６"
        public static void Message12052(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "宝箱"
        public static void Message12053(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_2(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_3(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_4(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_5(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_6(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_7(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_8(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_9(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_10(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_11(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_12(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_13(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_14(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_15(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_16(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_17(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        public static void Message12053_18(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、複合レバーの看板１"
        public static void Message12054(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、複合レバー２－１"
        public static void Message12055(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、複合レバー２－２"
        public static void Message12056(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、複合レバーの看板１"
        public static void Message12057(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、複合レバー３－１"
        public static void Message12058(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "力の部屋、複合レバー３－２"
        public static void Message12059(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、複合レバーの看板１"
        public static void Message12060(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、複合レバー４－１"
        public static void Message12061(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "心の部屋、複合レバー４－２"
        public static void Message12062(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "技の部屋、隠し通路発見"
        public static void Message12063(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "３階階段直前"
        public static void Message12064(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "下り階段"
        public static void Message12065(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #region "記憶の回想"
        public static void Message12066(ref List<string> list1, ref List<ActionEvent> list2)
        {
        }
        #endregion
        #endregion
        #endregion

        #region "帰還時の自動蘇生"
        public static void HomeTownResurrect(ref List<string> messageList, ref List<ActionEvent> eventList, MainCharacter player)
        {
            messageList.Add("ダンジョンゲートから不思議な光が" + player.FirstName + "へと流れ込む。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(player.FirstName + "は命を吹き返した。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);
        }
        #endregion

        #region "ホームタウン表示時"
        // 後編初日
        public static void Message20100(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

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

            messageList.Add("【ラナのイヤリング】を手に入れました。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(Database.RARE_EARRING_OF_LANA); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

            messageList.Add("アイン：何だ、ラナのやつ。何でまたこんな所に落としてるんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しょうがねえ。後で渡しに行ってやるか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっしゃ、せっかく目覚めたわけだし、町にでも出てみるか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownTurnToNormal);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("アイン：さて、何すっかな。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationFirstHomeTown = true;
            GroundOne.WE.AlreadyRest = true; // 朝起きたときからスタートとする。
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

                        messageList.Add("【  ラナがパーティに加わりました  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                        messageList.Add(""); eventList.Add(ActionEvent.None);

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

                        messageList.Add("【  ラナがパーティに加えませんでした  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

                messageList.Add("【  ラナがパーティに加わりました  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add(""); eventList.Add(ActionEvent.None);

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

            messageList.Add("【  ラナがパーティに加わりました  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.None);

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

            messageList.Add("【  ラナがパーティに加わりました  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.None);

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

                    messageList.Add("【  ラナがパーティに加わりました  】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                    messageList.Add(""); eventList.Add(ActionEvent.None);

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
            messageList.Add("アイン：っしゃ！やったぜ！ラナ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：上出来なんじゃない♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ！お前はやっぱ最高のパートナーだぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：また、そんな浮かれてちゃって。ホラホラ、町の住人達に報告しにいきましょ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ、そうだな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("その日の夜、ハンナの宿屋亭にて"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

                messageList.Add("ラナは自分が予約していた部屋へ歩いていった。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("アインが予約していた部屋にて"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

                    messageList.Add("アインは『ラナのイヤリング』をポケットから取り出した。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                    GroundOne.MC.DeleteBackPack(new ItemBackPack("ラナのイヤリング"));

                    messageList.Add("ラナ：っそ！ソレって！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ハンナ：おやおや・・・ひょっとしてラナちゃんのアクセサリかい？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、いやいやいやいや！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：った、たまたま俺の部屋に何故か転がってたんだって！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：悪かった悪かった悪かった！っな！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナは驚きと戸惑いの表情を隠せないでいる・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                    messageList.Add("・・・　数秒後　・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

                    messageList.Add("ラナ：まったく・・・っあ、そうだ。もう一個聞いて良い？"); eventList.Add(ActionEvent.None);

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

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("アインが予約していた部屋にて"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：明日から２階か・・・っしゃ！気合入れるぜ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                // after
                if (false)
                //if (GroundOne.WE2.TruthRecollection1)
                {
                    messageList.Add("アイン：俺はダンジョンへ行こうとしていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：師匠は無理だって言ってたが、俺には出来る気がしていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：魔物討伐は楽々こなせていたし"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：DUELに関しても、かなり上位にランクイン出来ていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何より、自分自身がようやく強いと感じられるようになっていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ユング街のダンジョン。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いわゆる「神の試練」ってのが待ち構えているらしい。"); eventList.Add(ActionEvent.None);

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

                    // after
                    //if (GroundOne.WE.Truth_GiveLanaEarring)
                    //{
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

                    //}
                    //else
                    //{
                    GroundOne.WE2.TruthKey1 = true; // これを真実世界へのキーその１とする。
                    //}

                    messageList.Add("　　　【得体のしれない不安を抱えつつ・・・】"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　【そのまま、２階へと足を運んだ】"); eventList.Add(ActionEvent.None);
                }
                GroundOne.WE.TruthCommunicationCompArea1 = true;
            }

            GroundOne.WE.TruthCommunicationCompArea1 = true;
            GroundOne.WE.Truth_CommunicationSecondHomeTown = true;
            GroundOne.WE.AlreadyRest = true;

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallSaveLoad);
        }

        // ２階初日
        public static void Message20200(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // 初日で何か喋らせたい場合、セリフ入れてください。
        }

        // ２階、地の部屋、選択失敗
        public static void Message20201(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent207FailEvent2 = false;
            if (!GroundOne.WE.dungeonEvent207FailEvent)
            {
                GroundOne.WE.dungeonEvent207FailEvent = true;

                messageList.Add("アイン：ッゲ！町に戻ったじゃねえか！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何かの強制転移装置みたいなものかしら。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っわ、悪ぃな。しくじっちまったみたいだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ダンジョンゲート・・・入れないみたいよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：今日は手仕舞いにするしかないみたいね。ハアァァァァ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：くそっ・・・次はミスらないようにするぜ。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：ッゲ！またやっちまった！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ちょっと・・・「上」の意味分かってるわけ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。もちろんだ。悪ぃ悪い。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：しっかりしてよね、バカアイン。ハアァァァァ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：くそっ・・・今度こそ・・・"); eventList.Add(ActionEvent.None);
            }
        }

        // ２階、神の試練クリア後
        public static void Message20202(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent225 = true;

            messageList.Add("アイン：っしゃ、戻ってきたな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあラナ。少しそこら辺、寄っていかないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え？うん。でもどこに行くのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『味商売　天地』でどうだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あの店味が濃くないかしら？　まあいいけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃ、さっそく行くとするか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まったく、あんな味のどこが良いのかしら・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("---- 『味商売　天地』にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：ふ～、やっぱここの味は最高だぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私、あんまり好きじゃないんだけど・・・って、ちょっと聞いてる？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ところで、あの詩だけどさ。ラナの母さんが作ったのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：違うわよ。母さんもその先代から伝承されてきた詩だそうよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：先代？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうよ、紫聡千律道場に代々伝えられてきている詩よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインは小さい頃、たまたま私と一緒に稽古練習してたから。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それで偶然聞いてたのよね。小さい頃だから、覚えてないのが普通よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どおりで思い出せないわけだ。ッハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも、変だよな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何が？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：このダンジョンでさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんでそんな事が起こりうるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・っえ・・・っと・・・"); eventList.Add(ActionEvent.None);

            // after (本当に分岐させたいのかどうか)
            //using (TruthDecision td = new TruthDecision())
            //{
            //    td.MainMessage = "　【　ラナにこのまま問い詰めてみますか？　】";
            //    td.FirstMessage = "問い詰める。";
            //    td.SecondMessage = "問い詰めず、話題を変える。";
            //    td.StartPosition = FormStartPosition.CenterParent;
            //    td.ShowDialog(); eventList.Add(ActionEvent.None);
            //    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //    {
                    messageList.Add("アイン：ラナはこのダンジョンのからくり、どこまで把握してる？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っか、からくりって何の事よ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：知らないわよ、そんなの。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何でも良い。知ってる事があれば教えてくれ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：知らないわ。本当よ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っそっか。。。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：悪いな。何か無理やり問い詰めちまって。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：・・・ねえ、アイン。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ん、何だ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：う～ん、何でもないわ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、良いんだ。悪かったな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っあ、そうじゃないの。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ランディスのお師匠さんに、聞きにいってみない？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あ？　あのボケ師匠にか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：アインが本当に困ってる時、私あんまり力になれてないみたいだし。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：ランディスのお師匠さんなら、何か教えてくれそうじゃない？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうだろうなあ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ちょっとでも設問を間違えりゃ昇天だからな・・・ック・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：まあ、無理にとは言わないけど。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いいや、聞いてみるぜ。ビビってたってしょうがねえからな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いろいろとサンキューな、ラナ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：っふふ、別に良いわよ。大した事はやってないわ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：じゃあ、ちょっとランディスのお師匠さんの所に行ってみましょ♪"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ああ！"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.dungeonEvent226 = true;
            //    }
            //    else
            //    {
            //        messageList.Add("アイン：詩自体は、ラナも知ってたんだよな？"); eventList.Add(ActionEvent.None);

            //        messageList.Add("ラナ：ええ、そうね。私も何度も聞いてたし、よく覚えてるわよ。"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：あの詩、このダンジョンを解くためのヒントになるかも知れねえ。"); eventList.Add(ActionEvent.None);

            //        messageList.Add("アイン：一応メモっといてくれるか？"); eventList.Add(ActionEvent.None);

            //        messageList.Add("ラナ：うん、分かったわ♪"); eventList.Add(ActionEvent.None);

            //    }
            //}
        }

        // ２階制覇
        public static void Message20203(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：よおおぉぉし、到達到達！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフフ、クリアした後だし皆に知らせに行きましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだな！"); eventList.Add(ActionEvent.None);

            messageList.Add("アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("---- その日の夜、ハンナの宿屋亭にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic15);

            // ８レバーが全てFalse、かつ
            // 真実解の部屋へ到達していない場合、BADEND
            if ((!GroundOne.WE2.TruthAnswer2_1) &&
                (!GroundOne.WE2.TruthAnswer2_2) &&
                (!GroundOne.WE2.TruthAnswer2_3) &&
                (!GroundOne.WE2.TruthAnswer2_4) &&
                (!GroundOne.WE2.TruthAnswer2_5) &&
                (!GroundOne.WE2.TruthAnswer2_6) &&
                (!GroundOne.WE2.TruthAnswer2_7) &&
                (!GroundOne.WE2.TruthAnswer2_8) &&
                (!GroundOne.WE2.TruthRecollection2))
            {
                if (!GroundOne.WE2.TruthAnswer2_OK)
                {
                    messageList.Add("アイン：しかし、妙なんだよな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：何がよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：結局さ。あのよく分からないレバーは何だったんだろうな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：触っても特に何も反応が無かったわよね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：空中文字とか出ててさ。演出がすごかったわりに当たりが無かったよな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：次の階で何か影響が出るとかじゃないかしら？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうだろうな。そうだと良いんだが。"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("ラナ：ハンナ叔母さん。豪華なごちそうありがとうございます♪"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：はいよ、今日の夕飯はいつもより豪華にしておいたからね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：あ、ありがとうございます♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おぉぉ！これはスゲェ！いただきます！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アインとラナが夕飯を食べた後 ・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう、もう食えないぜ。おばちゃん、ありがと！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：礼を言うなら、アンタのお師匠さんに言っておくんだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っへ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ああ見えて裏からコッソリ差し入れしてんだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッマジかよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：（声マネ）『ランディス：お祝いだぁ？クソくだらねぇ、勝手にやってろ』"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：とか何とか行って即行でどっかに行っちまったよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：フフフ、ランディスさんってやっぱり良い人じゃない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの自分絶対史上主義のボケ師匠に限ってか・・・ハハハ"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：礼とかはいいから、また殴られてくるんだね、アッハハハ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハ、ッハハハハ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アイン。ところで１階で思い出していた話について少し聞かせておくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ・・っと、１階ですか？"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("アインとラナが夕飯を食べた後 ・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：３階か・・・ここからいよいよ難しくなるんだろうな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　ハンナのおばさん、いきなり何だってんだろう・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：１階で思い出した話？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：２階を解いたこの時になって、いきなり何を・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                messageList.Add("アイン：・・・　何か忘れてる気がする　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺、何やってたんだっけ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあいいや、ひとまずダンジョンの最下層へ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(" ～　THE　END　～　（虚構へ）"); eventList.Add(ActionEvent.None);

                GroundOne.WE2.TruthBadEnd2 = true;
            }
            // それ以外はGOOD
            else
            {
                if (!GroundOne.WE2.TruthAnswer2_OK)
                {
                    messageList.Add("アイン：しかし、妙なんだよな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：何がよ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：結局さ。あのよく分からないレバーは何だったんだろうな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：触っても特に何も反応が無かったわよね。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：空中文字とか出ててさ。演出がすごかったわりに当たりが無かったよな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ラナ：次の階で何か影響が出るとかじゃないかしら？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうだろうな。そうだと良いんだが。"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("ラナ：ハンナ叔母さん。豪華なごちそうありがとうございます♪"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：はいよ、今日の夕飯はいつもより豪華にしておいたからね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：あ、ありがとうございます♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おぉぉ！これはスゲェ！いただきます！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アインとラナが夕飯を食べた後・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう、もう食えないぜ。おばちゃん、ありがと！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：礼を言うなら、アンタのお師匠さんに言っておくんだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っへ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ああ見えて裏からコッソリ差し入れしてんだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッマジかよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：フフフ、ランディスさんってやっぱり良い人じゃない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの自分絶対史上主義のボケ師匠に限ってか・・・ハハハ"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：礼とかはいいから、また殴られてくるんだね、アッハハハ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハ、ッハハハハ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アイン。ところで１階で思い出していた話について少し聞かせておくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ・・っと、１階ですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ええっと、あれ？　何だったっけ、ラナ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ちょっと何でソコで私に振ってんのよ。　自分で思い出しなさいよね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あれ・・・えっと、何だっけ？　アレ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おぉ！そうだ！ラナのイヤリングだ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：悪い！あれは本当に悪かった！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ッフフフ、それはもう良いって言ったじゃない♪"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：その後で思い出した事よ。何を思い出せたのか話してくれない？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：その後・・・？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、オーケーオーケー"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナのイヤリングだが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あれはラナと俺が２階へ降りる所で、ラナが落としたモノだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それを俺が拾って"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ、お前に渡した。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：自分で言ってて頭がおかしくなりそうだが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：これは確かな記憶だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：間違いはねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうだ、だからこそ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：１階を制覇した時点でラナに渡そうと思ったんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：よくやったね。アイン。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：正解だよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おばちゃんはやっぱり知ってるんだな、全てを。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：わたしゃ本当に何も知らないよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：私はねアイン、あんたの手助けをしてるだけだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん、まあそうなんだろうけどさ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ、悪かったな本当に。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何で謝ってんのよ、別に悪い事してるわけじゃないんだし♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、いやいや。こういうことはもっと早めに・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っさて、私はもう部屋に戻ろうかな♪"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：おばさん、おやすみなさい♪"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あいよ、おやすみ。　ゆっくり休むんだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ふう・・・お祝いのハズだったのに、悪い事しちまったかな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アッハハハ、何言ってんだい。アンタは本当にバカだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ッホラ、今日はもうゆっくり休むんだね、明日に備えて。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああぁ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃあ今日はおばちゃん、いろいろサンキューな！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あんまり考えこむんじゃないよ、寝れなくなるからね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、分かってるって。じゃあおやすみなさい！"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("---- アインが予約していた部屋にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：３階か・・・ここからいよいよ難しくなるんだろうな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                if (GroundOne.WE2.TruthRecollection1)
                {
                    messageList.Add("アイン：ラナにイヤリングを渡したのは良いが。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あれだけなら、俺は致命的な現象に遭遇することは無かった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：神の試練とやらは、簡単だった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：『　神々の詩「海と大地、そして天空」　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナの母さんから良く聞かされていたヤツだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：２階を解いている間はハッキリ思い出せなかったが確かそうだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ただ単にそれを声にして発するだけ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：難しくも何とも無かった。これの何処が試練なのか俺には理解が及ばなかった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そして、どうしても思い出せないのが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：『ヴェルゼ・アーティ』の存在。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：伝説のFiveSeeker、技の達人ってのは知ってる。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はダンジョンゲート裏で確か遭遇してる。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：彼はこう言っていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　『ヴェルゼ：アイン君、はじめまして。』"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　『本名はVerze Artieって言うんだ。よろしくね。』　　"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッハハハ・・・そういえばそうだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：今思い出した事がある。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そもそもだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：「はじめまして」　どころの騒ぎじゃねえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：はじめまして、なワケがねえんだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺は・・・この人の事を・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：もっとずっと前から知ってる。"); eventList.Add(ActionEvent.None);

                    GroundOne.WE2.TruthKey2 = true; // これを真実世界へのキーその２とする。
                }

                messageList.Add("　　　【俺はこの物語の真相を知ってるのかも知れない。】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【そんな奇妙な錯覚を覚えつつ、３階へと足を進めた。】"); eventList.Add(ActionEvent.None);

                GroundOne.WE.TruthCommunicationCompArea2 = true;
            }

            GroundOne.WE.TruthCommunicationCompArea2 = true;
            GroundOne.WE.Truth_CommunicationThirdHomeTown = true;
            GroundOne.WE.AlreadyRest = true;

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallSaveLoad);
        }

        // ３階初日
        public static void Message20300(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // 初日で何か喋らせたい場合、セリフ入れてください。
        }

        // ３階、エリア１の鏡をクリア時
        public static void Message20301(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent306 = true;

            messageList.Add("アイン：ふう、戻ったな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも、本当に珍しいわね、バカアインが賢い選択をするなんて。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや、正直な所・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、何でもねえ。悪い悪い。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え、ちょっと！？　何その言いかけて止めるのナシにしてよね！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや・・・まあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：アイン君、すみませんがボクは少しファージル宮殿に用事があるので、宮殿へ戻っていますね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？あ、あぁ。　了解了解！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええとだな・・・じゃあラナ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナは鏡を潜ってみてその・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうだ。　体調に変化はないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・え？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えええええぇぇぇぇ！！？？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うわっ、いきなり大声出すなっつうの。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え、ちょっと何、ひょっとして・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私の体調でも気遣ったって言ってるワケ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、何となく気になっただけだ！　そんな御大層な心配じゃねえって！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ップ"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ップハハハハ、何いってんのこのバカアイン、アンタほんとオカシイんじゃないの♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：体調？？　だーいじょうぶに決まってるじゃないの♪　変な所でカンチガイ気遣いしすぎよホント♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、なら良いんだがな。ッハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、たまにはこんな風に切り上げるのも悪くはないだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、そうね。たまには許してあげるとするわ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、今日はここまでだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っお、どうだ。　飯でも一緒に食べていくか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ううん、それは結構よ、一人で食べるから♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そか・・・まあ、それだけ元気なら良いんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・気のせいだな、きっと）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあまた明日もよろしく頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エエ、おつかれさま♪"); eventList.Add(ActionEvent.None);
        }

        // ３階、鏡エリア２－１をクリアした時
        public static void Message20302(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent314_2 = true;

            messageList.Add("アイン：ラナ、大丈夫か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、大丈夫だとは思う。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：アイン君、ボクはファージル宮殿から応急セットを持ってきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、すまねえな頼むぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、今日は宿屋へ戻って休むんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何かゴメンね、変なトコになっちゃって・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いいって、進めたんだし全然OKだろ。今はとにかく休むんだ。いいな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、わかったわ。じゃあ、また明日ね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、また明日な。"); eventList.Add(ActionEvent.None);
        }

        // ３階、鏡エリア２－２をクリアした時
        public static void Message20303(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent315_2 = true;

            messageList.Add("アイン：ラナ、宿屋まで歩いていけるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、そのぐらいは大丈夫よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ラナさん、応急セットをどうぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ありがとう♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：次の予備の分が必要ですね、応急セットを取ってきておきましょう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、すまねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・この応急セット凄いわね。何だか精神的な疲れが一瞬で飛んでいく感じだわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、さすがファージル宮殿御用達って所なのかしら。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも今日は一旦休もうぜ。それは一時のもんだろうしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、分かったわ。じゃあ、またね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
        }

        // ３階、鏡エリア２－３をクリアした時
        public static void Message20304(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent316_2 = true;

            messageList.Add("アイン：ラナ・・・ほら応急セットだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っふう・・・ありがと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：大丈夫か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、もう何とも無いみたい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：応急セットはファージル宮殿倉庫には山のようにありますから、心配せず使ってください。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、ごめんなさい、ありがとうね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：それでは、すみませんがファージル宮殿へ次の分を取りに行きます。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、宿屋まで送っていこうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：イイって♪　ホンットに大丈夫だから♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ったく、頑固な奴だな本当に、ッハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いいか、ヴェルゼも言ってたが辛い時はちゃんと言うんだぞ、いいな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、分かったわ。ちゃんと言うようにするから心配しないで♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：了解了解、じゃあ、また明日な。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、アインもゆっくり休んでね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
        }

        // ３階、鏡エリア２－４をクリアした時
        public static void Message20305(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent317_2 = true;

            messageList.Add("ヴェルゼ：アイン君、すみませんがファージル宮殿へ行ってます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、頼んだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、おい。起きれるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっ？ここどこよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ダンジョンゲートの入り口だ。ほら、応急セットだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、ありがと♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：で、何でここに戻ってきてるのよ、まさか私気絶しちゃってたワケ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、何か気絶というよりは、スヤスヤと寝てる感じだったぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん、度々ゴメンね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いいって、何か気持ちよさそうね寝てたし。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと・・・見ないでよね、人の寝顔。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグア・・・分かった分かった・・・そんだけ元気がありゃオーケーだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：宿屋まで戻れそうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、大丈夫よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ゆっくり休むんだぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、また明日よろしくね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、またな。"); eventList.Add(ActionEvent.None);
        }

        // ３階、鏡エリア２－５をクリアした時
        public static void Message20306(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.dungeonEvent312_2 = true;

            messageList.Add("アイン：っしゃ、戻ってきたぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：アイン君、ボクは少しエルミ王、ファラ王妃へ現状の報告を行ってきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ、そうか。って現状報告？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい。一応ファージル宮殿で勤めている以上、報告は必須事項ですから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなのか。じゃあ、分かったまた明日な。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ええ、では。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さてと。何か奇妙な台座試練も抜けて来た事だし、明日はいよいよボスって所だな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ようやくって感じよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何か結構グルグルと回された感じだったよな、覚えきれねえぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私もよ、ちょっと今回のはどこがどう繋がってるのか、全然把握できないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はあぁ・・・明日また地道に探すのか・・・キッチリ覚えておきゃ良かったぜ、とほほ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ、明日また行ってみましょ、それしかないんだから♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、じゃあまた明日な！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、またね♪"); eventList.Add(ActionEvent.None);
        }

        // ３階制覇
        public static void Message20307(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：よし、３階も無事クリアしたぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、じゃあ皆に知らせに行きましょうか♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだな！"); eventList.Add(ActionEvent.None);

            messageList.Add("アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("---- その日の夜、ハンナの宿屋亭にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic15);

            messageList.Add("アイン：いやあ・・・しかしすごかったよな、あの鏡の数"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：本当よね、もうどれぐらい通ったか覚えていないぐらいだわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナのマップは本当に助かったぜ。サンキューサンキューな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今回はワープがあったからあまり役にたってなかったけどね・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんな事ねえって！　行った所がちゃんと把握できる時点で大助かりさ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、それなら良いんだけど♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、これからも頼むぜ！ッハッハッハ！  お～い、叔母ちゃん、もう一杯追加できるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ハイよ、いくらでも飲みな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、コレコレ！サンキュー！"); eventList.Add(ActionEvent.None);

            // 原点解を見つけていない（328)
            // バッドエンド扱い
            if (!GroundOne.WE.dungeonEvent328)
            {
                messageList.Add("ラナ：でも、アインはあれで良かったの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：？　何の話だ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：最後の鏡の所よ。何か私に気にかけちゃってたみたいだけど。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、アレか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：鏡を通り過ぎてたせいもあるしな。まあ、気にするなって。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：第一、ヒントらしいヒントがないんだ。どうしようもなかっただろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：こういう場合は、素直に目の前を進めるに限る。　それが１番さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ん～、それなら良いんだけど・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：何か最後にあったのかい？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：う～ん、ボスを倒した後にね。　降り階段の前に看板があったのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：たしかね・・・え～と・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『　正解を導きし者、無限解の探求にて永遠に彷徨い、原点を知ること無く、回り続けるがよい　』"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ありゃ、これはややこしい言い回し方だねぇ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そうなんですよ、おばさんはこれ何か分かります？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：いや、アタシじゃ無理だね。ダンジョンに行ってる本人にしか分からないよ、こういうのは。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だよなあ・・・いくら叔母さんでもこういうのは・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：原点っていうのは、探したのかい？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：う～ん、それなんですけどね、実は最初から次の段階のエリアに入る前に別の看板があってですね・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("---- アインが予約していた部屋にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いよいよ３階も制覇、次から４階か・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナは・・・大丈夫なんだろうか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：鏡をくぐる時に、あんな風に眼の着色が変わって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：でもまあ、ラナにしか聞こえなかった呼び声で、正解がわかったわけだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：実際今もラナの体調も悪いわけじゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：大丈夫なハズ・・・行けるはずだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                messageList.Add("アイン：・・・　しかし、何なんだろうこの不安は　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの正解ルートの脅し看板。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あれは本当に単なる脅しなんだろうか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それとも・・・。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：とはいえ、ラナと一緒にダンジョン最下層には近づいていることは確かだ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：FiveSeekerのヴェルゼさんも今じゃ一緒だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このメンバーなら最下層へ必ず行ける"); eventList.Add(ActionEvent.None);
                
                messageList.Add("アイン：俺は信じてる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：なにか重大な事を見落としている気がする・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それも取り返しの付かない何か、あるいは、もう引き戻せない何か。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、それ以前にだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はもう何度も・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：記憶はねえが、この感覚"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺は以前、どこかで【強くなろう】と誓った"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だがそれがどこから湧いてきた記憶なのか"); eventList.Add(ActionEvent.None);
                
                messageList.Add("アイン：把握できないでいる"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：４階へ向かう俺の足は"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：どんどん深い泥濘にハマっていくようだ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：底の知れない闇へと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                GroundOne.WE2.TruthBadEnd3 = true;
            }
            else if (!GroundOne.WE.dungeonEvent332)
            {
                messageList.Add("ラナ：でも、アインはあれで良かったの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：？　何の話だ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：最後の原点解よ。アレを見つけたのに５つ鏡に挑戦しないなんて。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、良いんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺にとって、どうもあの５つ鏡は進んじゃ行けねえ気がした。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それだけの事さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ん～、それなら良いんだけど・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：何か最後にあったのかい？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：う～ん、ボスを倒した後にね。　降り階段の前に看板があったのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：たしかね・・・え～と・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　　『　正解を導きし者、無限解の探求にて永遠に彷徨い、原点を知ること無く、回り続けるがよい　』"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ありゃ、これはややこしい言い回し方だねぇ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そうなんですよ、おばさん。でも、実はですね！　聞いてくださいよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何とそこの超バカアインが【原点を知ること無く】の意味に相当する原点解を見つけちゃったんですよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：そうなのかい？たまにはやるじゃないか、アッハハハ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、正直な所半信半疑だったけどな、ハハハ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：で、そこまで見つけといて何で５つ鏡には向かわなかったのか、私にはちょっと理解不可能だけど。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：う～ん、まあまあ、それは良いじゃねえか！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おばちゃん、アカシジアのスパゲッティ一つ追加！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：はいよ、待ってな。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

                messageList.Add("---- アインが予約していた部屋にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いよいよ３階も制覇、次から４階か・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナは・・・大丈夫なんだろうか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：鏡をくぐる時に、あんな風に眼の着色が変わって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：でもまあ、ラナにしか聞こえなかった呼び声で、正解がわかったわけだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：実際今もラナの体調も悪いわけじゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：大丈夫なハズ・・・行けるはずだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                messageList.Add("アイン：・・・　しかし、何なんだろうこの不安は　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：原点解を見つけたのは、OKなんだろう"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：しかし、俺にとってはあの５つ鏡がどうしても"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：怖くて先に進めなかった。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：この恐怖の根幹が俺自身分からないでいる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：記憶の回想を幾つかは見ているが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それが何を思い起こさせてくれてるのか、今の俺じゃほとんど掴めないでいる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このメンバーなら最下層へ必ず行ける、その確信はある。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);
                
                messageList.Add("アイン：だが、それだけだ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：なにか重大な事を見落としている気がする・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それも取り返しの付かない何か、あるいは、もう引き戻せない何か。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、それ以前にだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺はもう何度も・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：記憶はねえが、この感覚"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺は以前、どこかで【強くなろう】と誓った"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だがそれがどこから湧いてきた記憶なのか"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：把握できないでいる"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：４階へ向かう俺の足は"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：どんどん深い泥濘にハマっていくようだ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：底の知れない闇へと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                GroundOne.WE2.TruthBadEnd3 = true;
            }
            else
            {
                messageList.Add("アイン：ふぅっと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：しかし、最後の看板・・・気になるんだよな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：【生】【死】って言われてもなあ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：次の階層の何かヒントになってるんじゃないの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、それならそれで、分かりやすいけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：別の何か隠された意味とか？原点解みたいに。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、そういった類じゃなさそうだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああいうのは、解答を求める問いかけじゃねえしな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：最後に何て書いてあったのか、正確に思い出せるかい？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。ええと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：　　　　『　原点を知りし者、　　向かうは　【生】【死】　』"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ふうん、そういう内容だったんだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おばちゃんは何か分かるか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：いや、皆目検討も付かないね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あんまり気にしちゃダメだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：読み切れない時は、素直にそのまま心に留めておくんだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、ありがとうな、おばちゃん。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：一応書き留めておいてあるから、気になった時は言ってちょうだいね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、メモ書き助かるぜ、サンキューな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ねえ、アイン。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？何だ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：う～ん、何でもない♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っだ、またお前それかよ。何だったのか、言ってみてくれよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そう？じゃあ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アイン、ヴェルゼさんの事どう思う？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・え？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ヴェルゼさんの事をどう思うのかって聞いてるのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ヴェルゼは・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：優しいトコがあって、頼りのある人で、何よりスキルコンビネーションが強え。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：う～ん、そうじゃなくて。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：どう思うのかって所を聞いてるんだけど。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ど、どうって言われてもな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：正直よく掴めねえって感じではある。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：掴めないってどういう意味？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：DUEL戦でもそうだったんだが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：基本的に、動作そのもの自体は読めない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：思惑、思慮、方向性・・・なんて言ったら良いんだろうな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そういうのも読めない。ようは考えてる事自体が読めないって話だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：それは、付き合いが短いだけじゃないかしら。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、そうとも考えられるけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：でも・・・確かにアインの言うとおり・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あの子は、昔からそうだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そうなんですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：な、何の話だよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アインが多分ヴェルゼさんの思考を読めないのは、期間的なものをもあるけど"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ヴェルゼさん、自分の事を一切喋らないのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうだっけ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：昔から自分に関する会話はしない子だったからね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：今でもその辺りは変わってないよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：だからアインや私にとって、ヴェルゼさんがどういう人なのか印象付かないわけよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：う～ん、言われてみりゃそうなのかも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、そうだ。俺、ヴェルゼとDUEL対決したんだけど"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ヴェルゼのDUEL戦歴、知らないか？おばちゃん。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：知りたいかい？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：戦歴はね"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・（ゴクリ）"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：０勝４２３敗だよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("【【【　アインは、戦慄を覚えた　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ば・・・バカな！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの内容で全敗ってウソだろ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：戦歴は絶対に詐称出来ないからね、間違いはないと思うわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・どういう事だ・・・信じられねえ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：アインにとって、ヴェルゼさんのDUEL戦術はどう感じられたの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何ていうか・・・あれで実際勝ち続けてきたんだろうな、って印象だったぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あの子は、必ず負ける。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：決して勝利を求めたりしない子だったわ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そういうもんか・・・でも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　う～ん　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：どうかしたの？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：どうだろうな、わかんねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ・・・いいか！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おばちゃん、アカシジアのスパゲッティもう一つ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：はいよ、少し待ってな。"); eventList.Add(ActionEvent.None);

                messageList.Add("　  ＜＜＜　ハンナは厨房へと戻っていった　＞＞＞　"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：ラナ、あんまヴェルゼに首は突っ込むな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：え！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：この話はちょっと長くなりそうだが、強いて言えば"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：ヴェルゼは底が深い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：深淵を覗き込む様な感覚だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：だからやめておけ、良いな？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ええ、別にそういうワケじゃないんだけど。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン（小声）：イイから、普通の会話振りだけにしておけよ、良いな？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：わーかった、分かったわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アカシジアスパゲッティ、お待ちどうさま。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっしゃ、待ってました！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ったく、ゲンキンよねホンット・・・ハアアァァァ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ、４階に行ったらさ、俺からヴェルゼに幾つか聞いてみるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：えっ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っえええええええぇ！！？！？！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うわっ、お前声がデケェっつうの。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ（小声）：ちちちちちょっと、さっきと全然違うじゃないの。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：良いんだって、こういう場合はこれが１番さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何が１番なのよ、まったくもう・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっしゃ、待ってろよヴェルゼ、ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃ、ごちそうさま！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：はいよ、寝る前は、ちゃんとお腹を休めるんだよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、分かった。ありがとうなおばちゃん。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：よし、じゃあ寝床に戻って荷物整理でもすっかな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：じゃあ、また明日ね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

                messageList.Add("---- アインが予約していた部屋にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ふう・・・バックパック整理っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いよいよ次から４階か・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナが導き出した正解、そして原点解への到達。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そのおかげで、無限解も通過した。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：大丈夫なハズ・・・行けるはずだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE2.TruthRecollection3_4)
                {
                    messageList.Add("アイン：記憶をたどると、次々思い出せることがある。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、今俺が遭遇している事象と食い違いが幾つも存在している。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：整理して考えたい。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、そんな気持ちとは裏腹に"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：整理した結果は見たくも無いという気持ちも混在している。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうするか・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、考えるべきだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺は今、そうしなくちゃならない"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：最後の看板には【生】【死】が書かれていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、俺の記憶している限り、あそこの看板は・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：【死】"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それだけのはず"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何故、今回は【生】【死】なのか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：これは人間の生死に関連している。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：記憶が【死】だけで、今みた看板は【生】【死】であるとすれば、"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：悪い方じゃねえ。良い方に考えていいはずだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：まあ、こんな考え方はまた師匠にどやされるのがオチだけどな、ここでは良しとしよう。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：次に、ヴェルゼ・アーティという存在。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺の記憶では、ヴェルゼ・アーティは過去出会った事がある。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そして今現在の俺としては、ヴェルゼ・アーティを見るのは初めてだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：常識的に考えてこの感覚は明らかにおかしい。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何せ記憶上では会っているのだから"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：今現在、俺は初めてヴェルゼの顔を見たというのは"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：記憶が飛んでいるか、もしくは"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：消されているか"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダメだ・・・ここら辺は追いかけても無駄だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ヴェルゼ・アーティに関する過去は断片的な情報の蓄積しか残されていない。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：技の達人。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：伝説のFiveSeekerの一人"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ファージル宮殿に仕えし者"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そして今は、俺と共にパーティを組んでくれている"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、ヴェルゼ・アーティの事を俺は何も知らないままだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そんな中でもかすかなヒントはあった"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：記憶では、ラナが示す可能性。いわゆる女のカンってヤツだが"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ヴェルゼ・アーティには恋人が存在していた時期があり"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そしてその人は不慮の事故で亡くなってしまった。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ヴェルゼ・アーティにとっては恋人を失った世界。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：幻想を望んだとしてもおかしくはない。良識の範囲内だ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：このラナの推察はおそらく正しい。なぜなら"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：記憶を辿り、ファラ様から教えてもらった内容と照らし合わせると"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ピタリと照合するからだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：元国王エルミやファラ王妃、カール、ボケ師匠。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そしてヴェルゼ・アーティ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：6人目、エレマ・セフィーネ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：エレマさんはヴェルゼ・アーティの特性を知り尽くしていた。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それがどういう事なのか、いくらなんでも俺にだって分かる。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：だが、今現在エレマさんは俺の前には存在しない。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：存在しているのは、ヴェルゼ・アーティだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そもそも、何故いま俺の目の前にいるのか。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダメだ・・・ここも結局分からねえ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・落ち着け・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダンジョン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：最後にファラ様に言われた事"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：終わりへと足を運ぶな"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：始まりへと足を進めろ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はなんとなくだが、この言葉に今、恐怖を覚えている"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何故なら、今現在"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：もう４階に向けて足を運んでしまっている"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：帰るべきなのか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いや、ダメだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺はダンジョンへ行く。そう心に誓った"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：何度も何度もそう心に誓っている気がするが・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：その理由はシンプルだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：資金源、戦闘スキル上達、度胸試し、人によって理由は様々だと思うが"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺がダンジョンへ行こうとした理由はただ一つ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナを守るため"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：その証明の一つとして"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ダンジョンは俺一人で完全に制覇するつもりだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：今は、ラナやヴェルゼと共に行動しているが"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：それはそれで成り行きだ、無理に断ったりはしない"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どうあれ、ダンジョン制覇は必ずやってみせる"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：難攻不落のダンジョン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：神々が住まうダンジョン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：神の遺産が得られるダンジョン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：人の心を喰らうダンジョン"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：いろいろと噂はある"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：挑戦した者は数知れないが、制覇した者は極々わずか"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：噂だけじゃFiveSeekerだけじゃねえのかって話もあるぐらいだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：つまり、このダンジョンが俺にも制覇できれば"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：もう俺が何かに負けたりする事もない"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：どんな事柄が起きようと"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ラナを必ず守れるようになる"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：決めた事なんだ"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：退くわけには行かない。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：必ず・・・解くんだ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                    GroundOne.WE2.TruthKey3 = true; // これを真実世界へのキーその３とする。
                }

                messageList.Add("　　　【記憶と事実の矛盾】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【ヴェルゼとエレマ】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【にじり寄ってくる恐怖感】"); eventList.Add(ActionEvent.None);

                if (GroundOne.WE.dungeonEvent328)
                {
                    messageList.Add("　　　【生と死を示した看板】"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("　　　【無限解を示した看板】"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("　　　【分からない事だらけのまま】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【俺は４階へと足を進める】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【答えは多分】"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　【この先にある】"); eventList.Add(ActionEvent.None);
            }

            GroundOne.WE.TruthCommunicationCompArea3 = true;
            GroundOne.WE.Truth_CommunicationFourthHomeTown = true;
            GroundOne.WE.AlreadyRest = true;

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallSaveLoad);
        }

        // 現実世界
        public static void Message20600(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

            messageList.Add("アイン：っ・・・いつつ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今・・・何時だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("        『アインは宿屋の寝床から起き上がった。』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：朝の６時か・・・起きるには少し早いぐらいだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ん？何か床に落ちてるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("【ラナのイヤリング】を手に入れました。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(Database.RARE_EARRING_OF_LANA); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

            messageList.Add("アイン：ラナのイヤリングじゃねえか・・・何でこんな物が・・・"); eventList.Add(ActionEvent.None);
            
            messageList.Add("アイン：・・・　何であるんだっけ　・・・　ラナが落としたのか？"); eventList.Add(ActionEvent.None);
            
            messageList.Add("アイン：いいや、そんなワケ無えよな・・・じゃあ何でだ・・・"); eventList.Add(ActionEvent.None);
            
            messageList.Add("アイン：まあいいか。とりあえず、目覚めたわけだし、町にでも出てみるとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownMorning);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownTurnToNormal);
            
            messageList.Add("アイン：さて、何すっかな。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyRest = true; // 朝起きたときからスタートとする。

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            GroundOne.WE2.SeekerEvent601 = true;
            Method.AutoSaveTruthWorldEnvironment();
            Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
        }

        // エンディング
        public static void Message20601(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownMorning);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownButtonHidden);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　　＜＜＜　号外！！！　＞＞＞　"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【野蛮なオル・ランディス、DUEL闘技場にて、屈辱の敗北！！】　"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【DUELタイムは史上最高】　）　"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【１６分４２秒】　"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【決め手は、ゼータ・エクスプロージョン】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【オル・ランディス、必死にワープゲートを駆使して反撃を繰り広げていたが】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【相手のニゲイト、スタンス・オブ・サッドネッスに幾度となく阻止され】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【オル・ランディスが、タイムストップを発動する瞬間】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【相手に先手のタイムストップを発動されたのが致命的となった】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジか・・・師匠負けちまったのかよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：珍しいわよね、あのランディスさんが普通に戦って負けるなんて。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、まだ続きがあるな。読んでみるか。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【なお、本DUELに関して、記者は自分の生命を賭して、オル・ランディス選手へ突撃取材を行った。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「ランディス選手！あれは敗北だったんでしょうか！？」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「うっせぇな！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「戦略的には勝っていたかどうかだけでも、お聞かせください！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「うっせぇ、知るかボケ！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「体調は万全だったんでしょうか？どこか痛めていたなどは！？」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「うっせぇ、んなんあるか、ボケが！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「最後に、ＤＵＥＬ闘技場の覇者として、今回の負けに関して一言お願いします！！！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「・・・　・・・　・・・」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「小細工は一切無し、体調も互いに万全、試合前の不正取引一切無し」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「俺の負けだ」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「・・・　・・・　・・・」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「・・・　・・・　・・・」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ランディス：「ドケやクソボケ記者がぁ！！食い殺すぞッラァ！！」"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　記者：「ッヒ、ヒエエェェ！！！わたたた、たりがとうございました！！！」"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・こぇ・・・よく聞きにいったなこの記者・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：この映像、ものすごい勢いで逃げてるわね、記者さん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ・・・多分捕まったらマジで病院送りだったろうな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あ、見てみて最後のコメント欄があるわよ、ホラ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ほんとだ、どれどれ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("　　　【なお、記者は猛ダッシュで逃げる際、オル・ランディス選手の最後に振り返る時の顔を目撃した】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【その時の、オル・ランディス選手の顔は】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【どことなく、優しく笑っているように見えた】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("ラナ：笑っているように・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：殺されかけた直後だろ？　悪意ある笑顔と勘違いしたんじゃないのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、そうなのかもね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っさてと、今年もファージル宮殿生誕祭への招待状が届いてたっけ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ウソ、ちゃんと覚えてたわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、もちろん覚えてるさ。ちゃんと招待状もココに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：（ジィ～）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや、宿屋のバックパック管理倉庫に入れたんだった、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：へ～、じゃあ後でちゃんと見に行きましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うっ・・・ッハッハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ねえ、ところでアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？　なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私達ってダンジョンの最後でどうなったのか、覚えてる？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ダンジョンの最後？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何言ってんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：制覇したに決まってるじゃないか！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、まともに答えなさいよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：少なくとも私は覚えてないわよ、最後の方は。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：最後はどんな感じだったの？教えてよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そりゃお前、最下層と言えば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：デカイ竜がドカーンと構えててさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それを俺がズバズバっと撃破したのさ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ふーん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：全然答えになってないわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッギク）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私の事助けてくれたのはどの辺りだったのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：黒いもやもやした煙に囲まれて、真っ暗になって気絶した瞬間は覚えてるんだけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私、その後どうなったのかが全然思い出せないのよ。スッポ抜けた感じで。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：気が付いたら、ダンジョンの外で介抱されてたのよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：こんなの納得が行かないわ。ちゃんと教えてちょうだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ、それはだなあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：４階でお前が倒れてたんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でだ、それを発見した俺は慌ててお前を抱き起こして・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("『ッドスウウゥゥン！！！』（ラナのファイナリティ・キックがアインに炸裂）　　"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：「抱き起こして」とかいう表現は止めてちょうだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：グフォオォ・・・ど、どう言えば良いんだよ・・・ったく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：慌ててお前をそっと起こしてだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ハラハラ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：その次は？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で、後はそのままダンジョンから帰還したって所さ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そんな普通の内容だったら、私だって覚えてると思うんだけど"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ねえ、その時に何かあったんじゃないの？どうだったのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ままま、良いじゃねえかそんな所は！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ダンジョン制覇！無事、完結！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハーッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：絶対何か隠してるわよね・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、ラナ、あれだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：良かったよ、お前と一緒に無事にここまで戻れて・・・本当に。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっ・・・とっとと・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、何しんみりしちゃってるのよ、こんな所でもう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・悪い悪い、なんとなくな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、そういえばラナ、頼み事があるんだが"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なに？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：明日さ。ファージル宮殿の生誕祭２１年があるだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：その時に・・・必ず、寄っておきたい場所があるんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：一緒に、来てくれるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：べつに良いけど、どこに行くつもりなのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：大丈夫、そんな遠い所へ行くわけじゃねえんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：頼む。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、良いわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ、明日またね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、また明日な。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：寝坊してたら、叩き殺すからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いやいや、殺さないようにしてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownMorning);

            messageList.Add("次の日の朝・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：ゴフォオォォォ・・・ッゴホッゴホ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：１分遅れてたわよ。さ、起きなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：イッツツツ・・・容赦ねえな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ファージル宮殿前は朝一で行っても混雑するんだから、早めに行きましょうよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、分かってるって。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、出発だ！！"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic13);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastle);

            messageList.Add("---- ファージル宮殿にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っうおぉぉ！　すげえ人の数だな！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：生誕祭だもの、当たり前じゃない♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：エルミ国王もよくこれだけの人を毎年毎年、宮殿に招き入れるよな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：宮殿がそろそろパンクするんじゃねえのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：宮殿外のガーデン広場もあるんだし、大丈夫らしいわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハハ・・・その辺はさすがと行った所だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さてと、じゃあ列に並んでエルミ国王とファラ王妃にご対面と行きますか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん♪"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ２時間半経過後 ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：・・・　もう少しか　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、もうすぐのはずよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、ホラ次が私達よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：本当だ、もう少しだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあ、所でラナ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何よ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：宮殿内、どこら辺を見てみたいんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：行きたい所を言ってくれ。優先させるからさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・え・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええええええぇぇぇーーーー！！？？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わーー、いきなりうるっさい声出すなって、ったく。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何ひょっとして、気遣ったとでも言うわけ？今のは？？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだよ。悪いかよ、まったく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：信じられないわね、バカアインがそういう事言うの。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：良いからほら、どこに行ってみるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：んーじゃあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ファラ王妃様の謁見が終わってから言うわね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、了解。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、ようやく前のヤツが終わったみたいだぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ、進めましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("---- 国王／王妃　謁見の間にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っとと・・・ここで整列だよな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：よく来たね。アイン君とラナさん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エルミ様、ファラ様、生誕祭おめでとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：アインさんもラナさんも、リラックスしてくださいね（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっ！ありがとうございます！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：（ちょっと、あんまり形式を崩さないでよね）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（いいじゃねえか・・・ああ言ってるんだしさ）"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：いいですよ、いつものアイン君らしく喋ってください。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイ、どうもっす！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっと、そこのバカはちょっと置いといて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エルミ様、本日折り入っての頼みがあってこの謁見の間に参りました。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：どうしたんだい？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：最近ファージル領域へ武力による接触を計る" + Database.VINSGALDE + "王国ですが"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：" + Database.VINSGALDE + "は元々私の母が育った国"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：母からは穀物が育ちにくいエリアが多い国だと聞かされておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そこでお願いがあります。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今から２年の間、私とそこのバカアインに対して"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：国外遠征許可証を発行していただけないでしょうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっ、俺も！？"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：なるほど、" + Database.VINSGALDE + "に行って穀物以外の生産を教えてくるつもりなんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：しかし国外遠征許可証を要請してくるとは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：どうしようか？ファラ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：エルミったら、ひどい男（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：っつ・・・どうしてお前は・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：何よ臆病者、ちゃんと貴方が考えてきた事を言いなさいよね（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：わかってるって、ああやはりお前は少し引っ込んでなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：・・・（＾＾＃"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：ッゴホン、では。えー・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：ラナさん、国外遠征許可証は既に用意してあります（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっ！？　本当ですか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：アインさんの分もありますよ（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ！！いつのまにそんなモノが！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：はい、エルミ解説（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：ッ・・・ック・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("エルミ：実はね、２か月前あたりから考えてたんだよ、" + Database.VINSGALDE + "の件は。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：適任役は誰かなと考えた所"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：君達２人にこの生誕祭が終わった後、正式に依頼しに行こうと思ってたんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("（ラナ：えっ！？）　　（アイン：えっ！！）"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：あのダンジョンを突破してきた君達だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：きみ達なら、きっとこの件を解決してくれる。そう信じたんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：というわけで・・・ファラ"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：はい、何でしょう？（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：順番が逆になってしまったが・・・例のモノをここへ"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：フフフ、もう後ろの手に隠し持ってましたわ、ジャーン（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：だあぁ、お前はもっと王妃らしくしなさい。本当に・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：このぐらい良いじゃない、ッネ、アインさん（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：タ・・・タハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：相変わらず、先回りされてしまいますね。凄いですよ、国王も王妃も。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：本当、驚かされるわ。いくらなんでもここまで私の行動が読み切られるなんて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ちょっと質問。ラナの生真面目さと正義行動は何となく読めるとしても"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何で俺の分まで？？"); eventList.Add(ActionEvent.None);

            messageList.Add("ファラ：・・・ッキャ、野暮だわ（＾＾"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ハイ？？"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：こら、ファラ。話をややこしくしないように。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：アインくん、君に遠征してもらうのは、別の意図があるんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：は、はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("エルミ：アインくん・・・実はね・・・"); eventList.Add(ActionEvent.None);

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin); eventList.Add(ActionEvent.None);

            messageList.Add("---- ファージル宮殿、園芸の広場にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ラナ：あっ、ホラホラ見てアイン。これアカシジアの木よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へえ・・・こんな形してんだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、こっちのも奇妙な形をしてるぞ。ラナわかるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それはヒメリギツユクサよ。成熟するまでは、渦巻き状に葉を広げるの。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へえ・・・へええぇぇ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインって本当に知らないのね、こういう事は。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、まったく知らない。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：こっちの、ゴリュウモクレンは？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：見たことも聞いた事もないな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：この、マラ・ハクジュは？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どっかの本でかろうじて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっと、ファージル宮殿で今年新作の薬草よ・・・本にまだ載ってないわよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、まじかよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：フ・・・ッフフフ♪　あーオカシイー♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：くそう、今に見てろ・・・全部暗記してやるからな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアインには一生無理な課題じゃないかしら♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあラナ。お前、やっぱり園芸とか好きなんだな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、そうね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうやって、薬草とか見て笑ってるお前って・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ！　あっちにも面白い形の木があるぞ、行ってみようぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えーーー、何よ今の。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：行ってみようぜ！　っな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ハイハイ、分かりました♪"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ファージル宮殿、食事の間にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：マジかよ・・・この美味さ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺は幸せ者だ！　まだまだ食わせてもらうぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：すみません、カールハンツ爵。このバカが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：よい、気の済むまで食されよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：それよりも、貴女は数日後、" + Database.VINSGALDE + "へ向かうとの事。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：準備を怠るでないぞ、よいな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ハイ、ありがとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：それと貴君。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッモゴ・・・ハイ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ランディスとはここで会ったかね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いえ、まだですが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっ、やっぱりどこかに潜んでるんですか？生誕祭だし・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：当然の事。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：トホホ・・・殴られたくないんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフ、さすがに生誕祭のど真ん中でバトルを仕掛ける事はなかろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ランディスは貴君に何か話があるようだったぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：この食事が終わったら、会ってみるがよい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：了解です。しかし、この宮殿広いしな・・・どこに行けば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その瞬間。　アインは遥か遠くから伝わってくる殺気を感じ取った。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハッ！　まさかこの悪寒と恐怖！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：この喧騒の中で互いに感知出来るとは、貴君も相当腕が上がったようだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや・・・これは単にボケ師匠の殺気が威圧的すぎるだけです・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも、不思議よね。アインが自分から察知しようとしたら、感知できたんでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、まあ・・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：話が一区切りするまでランディスさんは待っててくれた。って事じゃないかしら♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、絶対にありえないから。あの師匠に限ってないない。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、そんな事言ってると・・・知らないわよー♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？？？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　ヴオオォォォン！！！　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っおわあぁぁぁ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ちょちょちょ、止めてくれよボケ師匠・・・いつの間に後方に忍び寄ったんだ、ったく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：生誕祭のど真ん中でバトルを仕掛ける事がねぇと思ってんじゃねえぞ、ボケが。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ダンジョン、どうだった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・まあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：予想を超えてた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：自分の身の程が分かった、そんな気がした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺、もうちょっといろいろ回ってみようと思うんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ほぉ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：知らない事が多すぎる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生にも教えてもらいたい事が山ほどあるんだが。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：教えてもらう以前に、自分の手足を使って、まずいろいろ拾ってみたいんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃないといつまでたっても、師匠やカール先生には勝てる気がしない。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ちったぁ、成長したって所か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：" + Database.VINSGALDE + "遠征、頑張ってこいや。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：げっ、なんでもう知ってるんだ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：俺様は国王直属だぞ、てめぇに話が行く前から聞かされてんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ・・・そういや直属だったっけ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあ師匠"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺、" + Database.VINSGALDE + "に行って、今度こそ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：腕を磨いて磨いて、師匠に勝ってみせる！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・　ケッ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：せいぜい頑張れや、ッカッカッカ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：笑ってられんのも今のうちだ、絶対だからな。"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ファージル宮殿、休息の間にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：いやあ、しかし宮殿中を歩き回ったな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まだ一周出来てないわよね。本当凄いわね、エルミ様とファラ様は。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、こんだけの広さ。２人自らで設計したらしいからな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ねえアイン"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アンタどこか一緒に来て欲しい所があるとか言ってなかったっけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ、そうだそうだ。忘れる所だった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと・・・そんな忘れるような内容なの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや悪い・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：忘れてはいないさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ただ、ちょっとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ん～、ハッキリしないわね。結局そこには行くの？行かないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　まあ　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：行ってやれ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠、いつのまに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇが今回宮殿に来た一番の目的はそれだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えっ、そうなの？アイン"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ああ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、特に変な内容ってわけじゃないんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ただ俺がそこに行くってのは変かなって思ってる面も・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：変でもなんでもねえ、行ってやれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：何だったら俺も一緒に行ってやる。挨拶がまだだしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ、そか。ハハハ・・・助かる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：どこに行くの？"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：エレマ・セフィーネさんの墓場だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ファージル宮殿、墓地にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ラナ：こんな所があったのね・・・知らなかったわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：隠されるよう設計されてんだ。小娘には見つけられなくて当然だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうなんですか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：確かエレマさんの墓標は・・・あの辺りだったかな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・あっ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：誰か・・・いる・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.Ending);
        }

        public static void Message20601_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // after
            //mainMessage.Visible = false;

            //for (int ii = 0; ii < 10; ii++)
            //{
            //    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE, (ii + 1) * 0.1f); eventList.Add(ActionEvent.None);
            //    System.Threading.Thread.Sleep(400); eventList.Add(ActionEvent.None);
            //    Application.DoEvents(); eventList.Add(ActionEvent.None);
            //}

            //labelEnding.Visible = false;
            //labelEnding2.Visible = false;
            //this.BackColor = Color.White;
            //UpdateEndingMessage2(""); eventList.Add(ActionEvent.None);
            //GroundOne.WE2.SeekerEndingRoll = true;

            //GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("Dungeon Player\r\n ～ The Liberty Seeker ～"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("ストーリー　　【　湯淺　與範　】\r\n　　　　　　　【　辻谷　友紀　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("音楽　　【　湯淺　晋太郎　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("バトルシステム　　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("マップ制作　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("モンスター制作　　【　辻谷　友紀　】\r\n　　　　　　　　　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("魔法／スキル　　　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("サウンドエフェクト　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("アイテム制作　　【　石高　裕介　】\r\n　　　　　　　　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("グラフィック　　　【　辻谷　友紀　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("プログラマー　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("スペシャルサンクス　　【　KANAKO　】"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage2("プロデューサー　　【　湯淺　與範　】"); eventList.Add(ActionEvent.None);


            //UpdateEndingMessage("　ラナ：あれ・・・ヴェルゼさんじゃない？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：ああ・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：なぜ、こんな所に・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　ダンジョンから出る最後　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　あの真っ白な空間の中で、支配竜が最後、俺に告げたこと　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　唯一の例外として　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　ヴェルゼ・アーティを現世に戻してくれると伝えてくれた　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　ラナが助かったと分かった直後　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　俺の勝手なわがままだったかもしれないが　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　俺はあの真っ白な空間に引き込まれる中で必至に願った　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　それを支配竜は例外的に聞き入れてくれたようだ　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　ただし、絶対の条件があった　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　生命としての活動を復活させるだけでしか原理的には行えない　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　復活したとしても、本人の今までの記憶は一切保持されない　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　記憶は完全に消去される　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　最低限の生命活動を行うコア部分のみが魂として吹き込まれる）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　この事象は支配竜を通じて俺にだけ伝えられている　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：師匠やっぱり・・・ヴェルゼは記憶喪失なのか？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：通常の会話でも大体分かるんだが"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：エルミの事やファラ、カールの事、もちろん俺も含めて"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：アーティの野郎は、完全に覚えてねぇ。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：曖昧な雑談の中では読み切れない面もあるが"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：あのDUEL闘技場で、俺が負けた時の試合"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：あの時の戦闘の動きで、きっちり実感させてもらった。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：今のアーティは完全に記憶喪失だ。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：エレマさんとの記憶も・・・全部残ってないのよね・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ランディス：だろうな。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：そんな・・・じゃあ、どうして・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　記憶が一切消去された状態で　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　ヴェルゼは・・・無言で佇んでいる・・・　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　最愛の人が眠る墓所の前で・・・　）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：（　長い事・・・ずっと・・・）"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：ねえ、見てあれ。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：ん？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：ヴェルゼさんの足元、花が咲いてるわ。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：・・・ホントだ・・・何ていう花なんだ？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：アルヴィアナの花"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：アルヴィアナの・・・花？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：この時期には、滅多に咲かない花よ。"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：そうなんだ・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　アイン：花言葉とか、あったりするのか？"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage("　ラナ：うん、花言葉はね・・・"); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage(""); eventList.Add(ActionEvent.None);

            //UpdateEndingMessage(""); eventList.Add(ActionEvent.None);

            //for (int ii = 0; ii < 3000; ii++)
            //{
            //    point = new PointF(point.X, point.Y - 1); eventList.Add(ActionEvent.None);
            //    this.Invalidate(); eventList.Add(ActionEvent.None);

            //    int sleep = 70;
            //    if (ii > 2000) { sleep = 50; }
            //    if (ii > 2500) { sleep = 30; }
            //    if (ii > 2700) { sleep = 15; }
            //    System.Threading.Thread.Sleep(sleep); eventList.Add(ActionEvent.None);
            //    Application.DoEvents(); eventList.Add(ActionEvent.None);
            //}

            //this.endingText3.Add("＜　奇跡の再会　＞　って言うのよ。"); eventList.Add(ActionEvent.None);
            //for (int ii = 0; ii < 800; ii++)
            //{
            //    this.Invalidate(); eventList.Add(ActionEvent.None);
            //    System.Threading.Thread.Sleep(20); eventList.Add(ActionEvent.None);
            //    Application.DoEvents(); eventList.Add(ActionEvent.None);
            //}

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);
            //GroundOne.WE2.SeekerEnd = true;
            //this.we.TruthCompleteArea5 = true;
            //this.we.TruthCompleteArea5Day = this.we.GameDay;
            //using (SaveLoad sl = new SaveLoad())
            //{
            //    sl.MC = this.MC;
            //    sl.SC = this.SC;
            //    sl.TC = this.TC;
            //    sl.WE = this.WE;
            //    sl.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // 後編追加
            //    sl.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // 後編追加
            //    sl.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // 後編追加
            //    sl.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // 後編追加
            //    sl.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // 後編追加
            //    sl.SaveMode = true;
            //    sl.StartPosition = FormStartPosition.CenterParent;
            //    sl.ShowDialog(); eventList.Add(ActionEvent.None);
            //    sl.RealWorldSave(); eventList.Add(ActionEvent.None);
            //}

            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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

            messageList.Add("---- アインはDUEL闘技場へと走っていった ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ラナ：（アイン・・・あんな嬉しそうに、はしゃいで・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：（・・・　・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.Message_DuelAvailable); eventList.Add(ActionEvent.HomeTownMessageDisplay);
            
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("---- DUEL闘技場にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

            messageList.Add("ラナは町の中へと歩いていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：（ダンジョンともう一つ、ＤＵＥＬか。。。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ＤＵＥＬ・・・懐かしい感じがするな。。。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（っしゃ、明日からも頑張って行くとするか！）"); eventList.Add(ActionEvent.None);

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

            messageList.Add("受付係員は闘技場へと戻っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：ッグ・・・ヤ、ヤベェ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ックソ、何だっていきなり来てんだよ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：逃げても・・・おそらく無駄だろうな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ここは闘技場へ行くしかないか。"); eventList.Add(ActionEvent.None);

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

            messageList.Add("ラナ：『１．メニューを開く』"); eventList.Add(ActionEvent.None);

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

            messageList.Add(Database.Message_BattleSettingAvailable); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("【習得した魔法・スキルをバトルコマンドに設定できるようになります】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.None);
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

            messageList.Add("アイン：ファイア・ボール！！"); eventList.Add(ActionEvent.None);

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

            messageList.Add("【戦闘中にインスタントアクションが出来るようになりました】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("【戦闘中、アクションコマンドを右クリックする事で使用可能になります】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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
        public static void Message30002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：（・・・よし・・・行くか！）"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("ラナ：ちょっと、待ちなさいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic19);

            messageList.Add("アイン：・・・ラナか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今からダンジョンに向かう気よね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そのつもりだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：【　真実の答え　】・・・見つかった？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、見つかってる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今言ってみて、聞いてあげるから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わかった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：【力は力にあらず、力は全てである。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：【負けられない勝負。　しかし心は満たず。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：【力のみに依存するな。心を対にせよ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そっか・・・ちゃんと覚えてたのね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。あの時は分からなかったが、今、ようやく分かり始めたんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：力だけじゃ限界がある、それだけじゃダメなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でもだからと言って、信念や想いだけを持ってても駄目だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：両方とも併せもって初めて意味が出てくる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんな感じだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そう・・・私にはよく分からないけど"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインが感じた今の答えが真実なのね、きっと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それを教えてくれたのが、この剣だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：その練習用の剣？　小さい頃母さんからもらったやつよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これが神剣フェルトゥーシュだと知るまでにはずいぶんと時間がかかった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの頃は、どうみても単なるナマクラの剣にしか見えなかったからな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・いつ頃から気づいてたのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ボケ師匠ランディスに出くわした時だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうだったの。それからは、気づかない振りしてたの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠はどうもあの剣の特性に関して、もう一つ何か知ってるみたいだったんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：心を燈して放たないと、威力は発揮されない。何かそんな話だった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：心を燈して・・・って事は。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それが分かった時点で、俺の力に対する考えは変わった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインって・・・凄いわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、いやいや、凄くなんかねえって。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ううん、そういう風に考えが行き届くのは凄いわよ。私じゃ考えもつかないもの。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、俺の勝手な解釈だからな。間違ってる可能性の方が高いぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ううん、解釈が間違ってるとかそういう話じゃないの。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインの雰囲気そのものが、凄く変わるのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：凄く冷静で・・・的を得ていて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いつものアインじゃないみたい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、そういう側面もあるさ！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：良いのよ、無理して雰囲気変えなくて、ッフフ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わ、悪いな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、良いって言ってるじゃないの♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも、ついでに言わせてもらうわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アイン、あんた私に手加減してるでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：手加減？？　一体何の話だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：戦闘スタイルの事よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：戦闘・・・スタイル？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうよ。私レベルが相手ならバレないとでも思ってたのかしら。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、俺は手加減なんてしてないぞ。気のせいじゃないのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：見たわよ、アンタが傭兵訓練所を卒業した後、コッソリ独自で練習している所。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あんな動き・・・見た事もないスピードだったわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、待て。あれはだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いいのよ。私じゃ正直、追いつけないレベルだった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：動作切替タイミング、詠唱速度、剣を振るう速度。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：全てが別次元だったわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：どうして・・・私に見せてくれないのかしら？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：すまねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：謝らないでよ・・・どうなの？本当の所を教えてちょうだいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：やっぱり・・・そういう事よね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まて、そうじゃねえんだ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺が悪いのは本当だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、お前にだけはそういうとこを見せたくなかったんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：知られたく・・・なかったんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お前がもし、俺のそういう側面を知ってしまえば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺の前から・・・居なくなるんじゃないかって・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：力量に差が出てきたら、私がアインから離れていく。そう考えたって事？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ああ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフ、ッフフ♪　なーに言ってんのかしら、バッカじゃないのアンタ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：力量なんて・・・そもそもアンタに私が追いつけるワケないでしょ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラ、ラナ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何よそれ・・・失礼しちゃうわよホント。アンタの実力ってどんだけなのよ本当。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：隠すとか隠さないとか・・・くだらない事ばっかり考えて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：隠さなきゃいけないレベルになっちゃってる、そう言いたいわけ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うっ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あの練習内容の異次元みたいなスピードから察するに、そういう事よね！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私なんかじゃ。。。絶対にあんなの出来っこないもん。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、今は出来なくとも・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そんな風に気を使わないで。　私、自分の事は分かってるつもりだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ・・・おかしいわね。昔の小さい頃のアインってさ、すごく弱かったし。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いっつも泣いてばっかり。で、私がいっつも守ってあげてたのに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いつの間にそんなに腕を上げちゃってたのかしら、信じられないわ本当。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハハ・・・あったな、そういやそんな事も・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いいわ、アイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインが私に対して、変に気を使ってた事は許してあげる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わ・・・悪かったな、マジで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これからは・・・そうだな、あまり気を使わずに・・・。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、そぉーーーだ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！？なんだいきなり！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今、良い事思いついちゃった♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアイン、今から言うのは命令よ。ちゃんと聞きなさいよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、何だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私、今ここでアインにDUEL決闘を申し込むわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：で、条件を一つ付け加えるわ。聞きなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、何だその条件ってのは？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あんた今度こそ本当に、今この場で手加減せずに私に挑んでもらうわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それが絶対の条件よ。どう？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っぐ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：もし万が一手加減してたら・・・どうなる？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：その時は、私はアンタともうコンビは組まないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：手加減されてまで一緒に居たくないから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・分かった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：この一戦、絶対に手加減はしねえ。約束だ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・あっ！ま、まてよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：万が一それで、俺が勝ってしまったらどうなるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：やっぱり・・・その時も・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・ップ"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフフ、アーッハハハハ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何そんな心配してんのよ、大丈夫よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：手加減してない本気のアンタを見たいだけよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アンタ基本的に勝って当然なんだから、またクダラナイ事考えないでよねホント♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：（　どっちにしろ・・・本当に離れたりするわけ・・・　）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっ・・・？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ホーラホラホラホラ、じゃあ行くわよ。ちゃんと構えなさいよね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ。ちょっと待ってくれな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・よし、ＯＫだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私も良いわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、正真正銘の本気だ。手加減抜きで行くぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：始めるわよ、３"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：２"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：１"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：０！！"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.ENEMY_LAST_RANA_AMILIA); eventList.Add(ActionEvent.HomeTownCallDuel);
        }

        public static void Message30003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // 「DUEL最終戦闘：ラナ」で負けた場合
            messageList.Add("アイン：ッグ・・！！"); eventList.Add(ActionEvent.None);

            if (GroundOne.TruthHomeTown_DuelFailCount1 == false)
            {
                GroundOne.TruthHomeTown_DuelFailCount1 = true;

                messageList.Add("ラナ：今ので当たるなんて、アインらしくないわね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ックソ・・・避けたつもりだったんだがな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：今のアイン・・・やっぱり動きが鈍ってるわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：見せてちょうだいよ、本当の動きを。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。今度こそ！"); eventList.Add(ActionEvent.None);
            }
            else if (GroundOne.TruthHomeTown_DuelFailCount2 == false)
            {
                GroundOne.TruthHomeTown_DuelFailCount2 = true;
                messageList.Add("ラナ：手加減が身体に染み込んでいるみたいね。動きが遅かったわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ラナ相手だと・・・動きが縮こまってるのか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、今度こそ！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っく、今度こそ！"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(Database.ENEMY_LAST_RANA_AMILIA); eventList.Add(ActionEvent.HomeTownCallDuel);
        }

        public static void Message30004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // 「DUEL最終戦闘：ラナ」で勝った場合
            messageList.Add("ラナ：ッキャ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しまっっ！！　大丈夫か、ラナ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：いっつつつ・・・大丈夫よ、少し打っただけだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：け、怪我とかしてねえか？大丈夫なのか？痛い所はないか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：だーいじょーぶだって言ってるでしょーが。ホラホラ元気よ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よ・・・良かった。本当に大丈夫だな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：しつこいわね。蹴りかえすわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わわわ、わかった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：で・・・手加減はしてないわよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：もちろんさ！　俺の得意戦術をそのまま使ったからな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも、まさかあんなタイミングから入れてくるとは思わなかったわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインってさ、どこでそういうの覚えてきてるの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どこって言われてもな・・・師匠とやってるうちに自然と・・・かな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふ～ん・・・やっぱりランディスのお師匠さんが影響してるわけね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あとは・・・自分なりに、コソコソっとだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：他にはDUEL闘技場を観察してて、自分にはないトコを観察かな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：傭兵訓練時代の基礎訓練項目もたまに読み返して反復練習はしてる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：モンスター狩りの時も、普段使わない新戦術を取り入れてみたり。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あとは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あ～あ、もうイイ！　私の負け！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うわっ、すまねえ、悪かったって。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ううん、良いの。本気を見せてくれたんだし、スッキリしたわ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハッ・・・ハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ダンジョン、私を誘わないで一人でいくつもりなんでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うっ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアインは嘘作りが下手くそすぎなのよ。そんなのお見通しよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハハ・・・まあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：嘘というか、正直パーティに誘うつもりはあった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これは本当だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも、それじゃ・・・駄目みたいなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺は・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ここを抜けださなきゃならないんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何とか・・・辿りつけそうなの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、お前のイヤリングもホラ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今は、俺が手にしたままの状態だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・分かったんだ、どうしなければいけないか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ありがと。こんな所まで頑張ってくれて。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：バカ言うな。俺自身の問題だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：絶対に何とかしてやる。任せろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、お願い。期待してるから♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあな、行ってくるぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ダンジョンゲートの入り口にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：（・・・ダンジョンへ・・・俺は向かう・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ラナのイヤリングは手にしたままだ）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（俺はこれの意味を知っている）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・　・・・　・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（行こう、ダンジョンへ）"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGotoRealDungeon);
        }
        #endregion

        #region "ラナと会話"
        // after レベルアップによる魔法・スキルコマンドの習得に関しては、前編から会話を持ってくるべきでは？
        // １日目
        public static void Message40000(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // if (!we.AlreadyRest) // 1日目はアインが起きたばかりなので、本フラグを未使用とします。

            messageList.Add("ラナ：っあら、意外と早いじゃない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、何だか寝覚めが良いんだ。今日も調子全快だぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカな事言ってないで、ホラホラ、朝ごはんでも食べましょ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだな！じゃあ、ハンナ叔母さんとこで食べようぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナの宿屋（料理亭）にて・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

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

            // after
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


            messageList.Add("【遠見の青水晶】を手に入れました。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(Database.RARE_TOOMI_BLUE_SUISYOU); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

            messageList.Add("アイン：お、【遠見の青水晶】じゃねえか。助かるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：無くさないでよ？それ結構レア物で値段張るものなんだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？おう、任せておけって！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っと、そうだ。忘れないうちに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・（ごそごそ）・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何探してるのよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：確かポケットに入れたはず・・・"); eventList.Add(ActionEvent.None);

            // after
            //using (TruthDecision td = new TruthDecision())
            //{
            //    td.MainMessage = "　【　ラナにイヤリングを渡しますか？　】";
            //    td.FirstMessage = "ラナにイヤリングを渡す。";
            //    td.SecondMessage = "ラナにイヤリングを渡さず、ポケットにしまっておく。";
            //    td.StartPosition = FormStartPosition.CenterParent;
            //    td.ShowDialog(); eventList.Add(ActionEvent.None);
            //    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                messageList.Add("アイン：あったあった。ラナ、こいつを渡しておくぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：これ、私のイヤリングじゃない。どこで拾ったのよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：どこって、俺の部屋に落ちてたぞ。ラナが落としていったんだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：・・・っええ！？そそそ、そんなワケ無いじゃない！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：なんでそんな慌ててんだよ。まあ返しておくぜ。ッホラ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っとと、・・・アリガト♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：お前は変な所で抜けてるからな、しっかり持ってろよな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！"); eventList.Add(ActionEvent.None);

                GroundOne.MC.DeleteBackPack(new ItemBackPack(Database.RARE_EARRING_OF_LANA)); eventList.Add(ActionEvent.None);
                GroundOne.MC.SortByName();
                GroundOne.WE.Truth_GiveLanaEarring = true;
            }
            // after
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
        // ２日目
        public static void Message40001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：よおラナ！ダンジョンから戻った時に気づいたんだが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：赤ポーションでも欲しいわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな！？　何で分かった！！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：大体ポーションひとつも持たずにダンジョンに突っ込むヤツなんてどこに居るのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：悪ぃ・・・悪かったって、なあココはひとつ頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：しょうがないわね、今回だけよ。ッホラ♪"); eventList.Add(ActionEvent.None);
            
            messageList.Add(GetPotionForLana()); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

            messageList.Add("アイン：っしゃ！サンキューサンキュー！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っあ、っちょっとちょっと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？何だよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、やっぱり秘密かな♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おい、待て待て。すげえ気になるじゃねえか？何だよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：贅沢言わないの。ホラ、とっととバックパックを確認しときなさいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ちぇ、相変わらず秘密主義だよな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃ、またね。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyCommunicate = true;
            GroundOne.WE.Truth_CommunicationLana2 = true;
        }

        private static string GetPotionForLana()
        {
            string potionName = Database.POOR_SMALL_RED_POTION;

            if (!GroundOne.WE.CompleteArea1 && !GroundOne.WE.CompleteArea2 && !GroundOne.WE.CompleteArea3 && !GroundOne.WE.CompleteArea4 && !GroundOne.WE.CompleteArea5)
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            else if (GroundOne.WE.CompleteArea1 && !GroundOne.WE.CompleteArea2 && !GroundOne.WE.CompleteArea3 && !GroundOne.WE.CompleteArea4 && !GroundOne.WE.CompleteArea5)
            {
                potionName = Database.COMMON_NORMAL_RED_POTION;
            }
            else if (GroundOne.WE.CompleteArea1 && GroundOne.WE.CompleteArea2 && !GroundOne.WE.CompleteArea3 && !GroundOne.WE.CompleteArea4 && !GroundOne.WE.CompleteArea5)
            {
                potionName = Database.COMMON_LARGE_RED_POTION;
            }
            else if (GroundOne.WE.CompleteArea1 && GroundOne.WE.CompleteArea2 && GroundOne.WE.CompleteArea3 && !GroundOne.WE.CompleteArea4 && !GroundOne.WE.CompleteArea5)
            {
                potionName = Database.COMMON_HUGE_RED_POTION;
            }
            else if (GroundOne.WE.CompleteArea1 && GroundOne.WE.CompleteArea2 && GroundOne.WE.CompleteArea3 && GroundOne.WE.CompleteArea4 && !GroundOne.WE.CompleteArea5)
            {
                potionName = Database.COMMON_GORGEOUS_RED_POTION;
            }
            else
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            return potionName;
        }

        // ３日目
        public static void Message40002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                messageList.Add("ラナ：っよし、完成完成♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っふう・・・やっぱこのままだと辛いな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：あっ、バカアインじゃない。ちょうど良かったわ。コッチに来てみて♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オレはバカじゃないと言ってるだろ。何だよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：まあ、良いから良いから、早く来なさいよ。ホラホラホラ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おわっとっと！？　分かった、分かったって。そんなひっぱるなって。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツの武具店がある横街道にて・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ん？何だ、ガンツ叔父さんの武具店の前じゃねえか。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っさてと・・・じゃ、私についてきて。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、おいおいどこ行くんだよ！？そっちの角には何にも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・おおぉぉお！？んだこれは！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.Message_PotionShopAvailable); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：っな・・・っどうなってやがる！？・・・いつの間にこんな小屋を！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん！？ラナのヤツ、いつの間に居なくなって！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：待て、待て待て。落ち着け・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ふぅ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの異常なまでに派手な装飾が施されてる小屋に入ってみるしかないか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：こんちわーっす。　（って、何言ってんだ俺）"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：いらっしゃいませ♪　好きな薬品をお買い求・・・めくだ・・さ・・。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：お買い求めくださいませよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：お前、語尾がムチャクチャじゃねえか。ちゃんと練習したのかよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：いいじゃない、別に。大して差は無いわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：せっかく客が来たんだ。笑顔で出迎えろよな？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：最初笑顔を提供したんだから良いじゃない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、いやいや、そういう意味じゃなくて・・・まあ良いか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：へえぇ～。結構揃ってんじゃねえか！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いつの間にこんなに揃えたんだよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：うん、今までの間に結構毎日、調合をして来ていたのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：一応数はそろえたから、思い切って始めようと思ったのよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うん。良いんじゃねえか？　コイツは気に入ったぜ、ラナ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ッフフ、ありがとね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：早速だ。何か買っていっても良いのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：うん。値段はおさえめにしといたから。どうぞ見て行ってね。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallPotionShop);
            }
            else
            {
                messageList.Add("ラナ：っよし、完成完成♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っふう・・・やっぱこのままだと辛いな。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：あっ、バカアインじゃない。ちょうど良かったわ。コッチに来てみて♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オレはバカじゃないと言ってるだろ。何だよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：まあ、良いから良いから、早く来なさいよ。ホラホラホラ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おわっとっと！？　分かった、分かったって。そんなひっぱるなって。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツの武具店がある横街道にて・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：ん？何だ、ガンツ叔父さんの武具店の前じゃねえか。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：っさてと・・・じゃ、私についてきて。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、おいおいどこ行くんだよ！？そっちの角には何にも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・おおぉぉお！？んだこれは！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.Message_PotionShopAvailable); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：っな・・・っどうなってやがる！？・・・いつの間にこんな小屋を！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん！？ラナのヤツ、いつの間に居なくなって！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：待て、待て待て。落ち着け・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ふぅ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの異常なまでに派手な装飾が施されてる小屋に入ってみるしかないか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：こんちわーっす。　（って、何言ってんだ俺）"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：いらっしゃいませ♪　好きな薬品をお買い求・・・めくだ・・さ・・。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：お買い求めくださいませよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：お前、語尾がムチャクチャじゃねえか。ちゃんと練習したのかよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：いいじゃない、別に。大して差は無いわよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：せっかく客が来たんだ。笑顔で出迎えろよな？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：最初笑顔を提供したんだから良いじゃない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、いやいや、そういう意味じゃなくて・・・まあ良いか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：へえぇ～。結構揃ってんじゃねえか！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いつの間にこんなに揃えたんだよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：うん、今までの間に結構毎日、調合をして来ていたのよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：一応数はそろえたから、思い切って始めようと思ったのよ♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うん。良いんじゃねえか？　コイツは気に入ったぜ、ラナ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ッフフ、ありがとね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：早速だ。何か買っていっても良いのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：うん。値段はおさえめにしといたから。どうぞ見て行ってね。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallPotionShop);

            }
            GroundOne.WE.AlreadyCommunicate = true;
            GroundOne.WE.Truth_CommunicationLana3 = true;
        }
        // Level4以降、スタンスの習得会話
        public static void Message40003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ラナ：バカアイン、ちょっといい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺はバカじゃねえって。何だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：フレッシュ・ヒール。覚えたわよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？ああ、そうだな。なんとなく思い出したぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヒール ＆ アタック！ 基本だからな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：昔っからそうよね・・・その単調なノリ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：別にいいじゃねえか。ダメージレースの基本だろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そのスタンス、変えようと思った事は一度もないわけ？"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyCommunicate = true;

            GroundOne.DecisionSequence = 0;
            GroundOne.DecisionMainMessage = "　【　スタンスを変えようと思いますか？　】";
            GroundOne.DecisionFirstMessage = "思わないな。今までどおり行くぜ。";
            GroundOne.DecisionSecondMessage = "正直、一度ぐらいは考えたことがある。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message40003_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：思わないな。今までどおり行くぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そう。まあ、今更変えるとは思ってなかったけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：やっぱダメージをガツガツ相手に与えないとな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ、アインは前衛攻撃型って事になるわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：前衛攻撃型？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：前に出て、ダメージをガツガツやる役の事よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ。何だそういう意味かよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ、今までどおり、しっかり前に出て、攻撃よろしく♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、任せておけ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            // after 前衛後衛の概念を導入する意志はあるか？
            GroundOne.MC.Stance = MainCharacter.PlayerStance.FrontOffence;

            //CallSomeMessageWithAnimation("【アインは前衛攻撃型になりました！】"); eventList.Add(ActionEvent.None);

            //CallSomeMessageWithAnimation("【物理攻撃５％ＵＰ、魔法攻撃５％ＵＰ】"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyCommunicate = true;
            GroundOne.WE.Truth_CommunicationLana1_1 = true;
        }

        public static void Message40003_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：正直、一度ぐらいは考えたことがある。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そう。まあ、今更変えるとは思ってなかったけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・おい、考えた事はあると言ってるだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・えええええぇぇぇええ！！？？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！ビックリさせんなって。お前の驚き方はオーバーすぎるぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちゃんと理解して言ってるわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：相手の方がダメージが強い場合は守るのも大事だしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こちらの体制が崩れないようにして、少しずつ攻撃を織り交ぜる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ディフェンス ＆ アタックって所だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：た・・・確かにそんな所ね・・・ちょっと違うけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインからそんな言葉が出るとは意外だわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナとテンポよく攻撃するのも良いけどな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ほら、たまにピンチな時あるだろ。守ってりゃ良かったって思う時が。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え、ええ、まあそうね。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：じゃあ、前衛防衛型って事になるわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：前衛防衛型？何だそりゃ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：前でしっかりと相手のダメージを受け止める役の事よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それ以外にも、タイミングよく相手をスタンさせたりしてよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それから敵モンスターの注意もちゃんと引いてよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あんまり防御一辺倒にならないでよね。タイミングよく攻撃も混ぜてね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：どうしてもって時はライフ回復もちゃんとやってよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・面倒くせえぇぇ！！！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.Stance = MainCharacter.PlayerStance.FrontDefense;

            //CallSomeMessageWithAnimation("【アインは前衛防衛型になりました！】"); eventList.Add(ActionEvent.None);

            //CallSomeMessageWithAnimation("【物理防御５％ＵＰ、魔法防御５％ＵＰ】"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AlreadyCommunicate = true;
            GroundOne.WE.Truth_CommunicationLana1_1 = true;
        }
        // オル・ランディス遭遇前後
        public static void Message40004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.MeetOlLandisBeforeLana)
            {
                messageList.Add("ラナ：アイン、どうしたの？何か顔が冴えないみたいだけど。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ボケ師匠のランディスが闘技場に来てるみたいだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：えっ、そうなの！？　良かったじゃない♪　早速、会いに行ってきたら♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：出会ったら、俺が死んじまうだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：なにそんなビビってんのよ。だったら、私が先に顔会わせして来ようかな♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：待て！　待て待て！！　それだけは駄目だ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：何でよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：こういった場合、俺が先に顔会わせに行かなきゃならねえんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：じゃねえと、マジで殺されちまう。　俺が先に会ってくる。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：そうなの。まあ良いわ、じゃあ早いとこ会って来ればいいじゃない♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：クッソ、何でお前そんな楽しそうなんだよ。。。まあいいか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっしゃ、踏ん切りが付いたぜ。早速行ってくるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：あ、アインのためにこんなもの用意しておいたわよ。ハイ、ポーション♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アインは" + Database.COMMON_REVIVE_POTION_MINI + "を手に入れた。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：要るかぁぁぁぁ、こんなのおおぉぉぉ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：フフ、じゃあ行ってらっしゃい♪"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.COMMON_REVIVE_POTION_MINI); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

                GroundOne.WE.MeetOlLandisBeforeLana = true;
            }
            else
            {
                messageList.Add("ラナ：行ってらっしゃい♪"); eventList.Add(ActionEvent.None);
            }
        }
        // 絡みつくフランシスに遭遇済みの場合
        public static void Message40005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ２階開始時
        public static void Message40006(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ３階開始時
        public static void Message40007(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ４階開始時
        public static void Message40008(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // 現実世界
        public static void Message40009(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ラナ・複合魔法・スキルの基礎習得
        // ラナ習得済み
        public static void Message40010_1(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ブルー・バレット
        public static void Message40010_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ヴァニッシュ・ウェイブ
        public static void Message40010_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // ダーケン・フィールド
        public static void Message40010_4(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // フューチャー・ヴィジョン
        public static void Message40010_5(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // リカバー
        public static void Message40010_6(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // トラスト・サイレンス
        public static void Message40010_7(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // スカイ・シールド
        public static void Message40010_8(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // スター・ライトニング
        public static void Message40010_9(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // サイキック・トランス
        public static void Message40010_10(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // サイキック・ウェイヴ
        public static void Message40010_11(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // シャープ・グレア
        public static void Message40010_12(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }
        // スタンス・オブ・サッドネス
        public static void Message40010_13(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
        }

        // イベントが特に無い場合
        public static void Message49999(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                messageList.Add("ラナ：どう？調子のほうは？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：当然当然。任せとけって！ッハッハッハ！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ラナ：死なないようにがんばる事ね。"); eventList.Add(ActionEvent.None);
            }
            GroundOne.WE.AlreadyCommunicate = true;
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
        
        public static void Message50001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.MeetOlLandisBeforeGanz)
            {
                messageList.Add("アイン：こんちわー。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：は、はい。なんでしょう？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：オルが挨拶に来ておったぞ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：は、ハハハ・・・そうでしたか。そいつは良かったですね。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：は、はいハイ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ！！！じゃあ、これで失礼いたします。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　（ッバタン・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（はあ・・・先回りされてるじゃねえか・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）"); eventList.Add(ActionEvent.None);
                GroundOne.WE.MeetOlLandisBeforeGanz = true;
            }
            else
            {
                messageList.Add("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message50002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：こんちわー。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アイン。２階へ向かうようだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ええ。今日からそのつもりです。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ならば、これでも持って行くと良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインは" + Database.POOR_PRACTICE_SWORD_ZERO + "を手に入れた。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：これは・・・練習用の剣？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：その武器には特殊な効果が封じ込められておる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ワシなりに考えてみたが、アインよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：・・・いや、お前なりに使ってみると良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと、どういう事でしょうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっと、ハイ！ありがとうございました！！"); eventList.Add(ActionEvent.None);

            messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（どうみてもこれは単なる練習用の剣だが・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・いや、そんなわけがねえよな）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（っしゃ、せっかくなんだし、使ってみるとするか！）"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.POOR_PRACTICE_SWORD_ZERO); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);

            GroundOne.WE.Truth_CommunicationGanz21 = true;
            GroundOne.WE.AlreadyEquipShop = true;
        }

        public static void Message50003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：どうも、こんちわー"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、ちょっとこちらへ来なさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、何でしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：心配はいらん。少しの間だけだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい。それじゃ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（あれ、この道って、ダンジョンゲートへ行くつもりか？）"); eventList.Add(ActionEvent.None);

            messageList.Add("---- ダンジョンゲート裏の広場にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ガンツ：着いたな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと、すいません質問なんですけど？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：なんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こんな裏広場まで来て一体何を？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：・・・アインよ、こちらへ来てみなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？これは・・・変な円紋が・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：空間転移装置、聞いた事ぐらいはあるだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジっすか！？これが・・・へえええぇぇぇ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？　ってかどこかに行くつもりなんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　ドン！！ （アインは突然突き飛ばされた）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ！っちょ！！　っちょちょ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、精進なさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　ッバシュウウゥゥゥゥン"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.Message_GoToAnotherField); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っいで！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッテテテ・・・体勢が悪かったせいか、放り出されちまった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ったく・・・行き先ぐらい教えてくれてもいいのに。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：貴君が、アイン・ウォーレンスか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？あ、あぁそうだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、おわぁぁ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『相手の顔の右目がギョロっと動いている』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ビックリしたなあ・・・擬眼かよ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：どれ、少し見せてもらうぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『擬眼がギョロリと動きはじめた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何かこええなあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：スペル属性『聖  火  理』　それに"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：スキル属性『動  剛  心眼』か。　なるほど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なっ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：大胆な攻撃スタイル、それに繊細な戦術をいくつか。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：挑発には意図的に挑む方だが、肝心な面はいつも冷静"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：物理攻撃だけでなく、魔法にも長ける。全体的なオールラウンダー"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：直感で『決まり』と判断すれば、一気に仕掛けるタイプ。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：ッフハハ、面白い。　ランディスもああ見えて教え好きだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：し、師匠を知ってるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『擬眼が更にギョロリと動いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：しかし、敵を全力で潰しにいかず、様子見の面が強い。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：一人で次々と倒そうとするより、チーム連携を考慮して動くタイプ。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：本来なら一人で出来る素質もあるが、表には決して見せない。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：なるほど、何か特定の事柄を意識しているな。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：この手抜き加減・・・驕りではなく、無意識的にかあるいは。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：たしかに、このままでは致命的な敗北は間逃れんな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいやいや・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ってか、さっきから妙にこの感覚・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは背筋に異常な恐怖感を覚えている。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（あのボケ師匠じゃねえけど、ちょっと違った怖さがある・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（支配、制圧・・・そんな感じか）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あんた、一体誰なんだよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：この距離感を一定に保つあたり、警戒心は貴君なりに最大というわけだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：だが我の射程、貴君の想像を遥かに超えている。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは膨大な汗を体中に感じた。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッヤ、ヤベェ・・・何かヤベェ・・・！！！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、ってか、そろそろ正体を教えろよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：アンタ、敵じゃないんだろ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：この辺が頃合いか。逃げられても困る。"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：我の名は『シニキア・カールハンツ』である。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　『アインは汗がスゥっと引いていくのを感じた。』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッホ・・・なんだったんだ今のは・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・はじめまして、アインと言います。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、シニキアってまさか、伝説のFiveSeeker！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：伝説のFiveSeekerなどという恥ずかしい通り名は止めてもらおう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我はカールとでも呼べばよい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい・・・えっと、じゃあのカールさん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ガンツ叔父さんは何でここへ俺を？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君を鍛えるよう言われている。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺をですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我の場合、鍛える方法は戦闘訓練ではない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：行動よりもまず理論。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君にはそれが欠けている。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと・・・具体的には何を？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我の言う事、すべてを記憶せよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：記憶！？　暗記しろって事ですか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そうだ。では行くぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『カールの講義が延々と小一時間続いたのち・・・』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：複合魔法の基礎に関しては、以上だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ッバタ・・・（アインはその場で静かに落ちた）"); eventList.Add(ActionEvent.None);

            messageList.Add("アインは複合魔法・スキルの基礎を習得した！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            GroundOne.WE.AvailableMixSpellSkill = true;
            GroundOne.WE2.AvailableMixSpellSkill = true;

            messageList.Add("カール：どうした。まだまだ先は長いぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：無理・・・こういうのは駄目だ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあ、ちょっとでも良いからよ。実践で教えてくれよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：駄目だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：要は、聖と火を組み合わせるって事なんだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：違うな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：具体的に一回だけ見せてくれよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：駄目だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：火と聖って相性が良いって事なんだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：違うな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：聖と闇は反対・・・みたいな？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：違うな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生、一回だけ頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：駄目だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：トホホ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：心配か？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？そりゃまあ、やってみた方が覚えるのも早いし"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：イメージの基本は、習得した知識から来る。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：具現化の展開は、それぞれの知恵から派生して成る。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん、ま、まあなんとなくその辺は・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：心配するな。貴君はすでに習得したも同然だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え！？　そんな、１回も確認してないですけど？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：誰に教えを乞うたと思っておる。我を愚弄するか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや！そんなつもりじゃございません！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：まあよい。空間転移装置を復活させておいた。帰るが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイ・・・どうもありがとうございました。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　ッバシュウウゥゥゥゥン"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.Message_GoToAnotherField_Back); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ガンツ：どうだったかね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうも何も・・・すげえ疲れました。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：そうかね。では戻るとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ガンツ叔父さんもこう見えて、強引だよな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはガンツの武具屋まで戻ってきた"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ガンツ：では、ワシはこれで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おじさん、ちょっと質問が"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：何だね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの転移された場所ってどこら辺なんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：それはカール爵の希望により答えられん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなのか・・・いや、何か見たことある場所な気もしたんで"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：なに、お主も良く知っておる場所よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなんですか？　う～ん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、良いや。おじさん、ありがとうございました！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：うむ、精進せいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ガンツは店の中へと戻っていった・・・』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何かグダグダに疲れた気もするが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：複合か・・・俺にも出来るようになるといいな"); eventList.Add(ActionEvent.None);
        }

        public static void Message50004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if ((GroundOne.MC.Level >= 21) && (!GroundOne.MC.FlashBlaze))
            {
                messageList.Add("アイン：どうも、こんちわー"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、ちょっとこちらへ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、はい何でしょう？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：以前から見て、また少し強くなったと見えるな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いやいや、それほどでもありませんが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：カール爵の所へ行って来なさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゲ、またですか！？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：何を言っておるアイン、お主なら複合系など容易いだろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：う～ん・・・あの人苦手なんだよなあ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、精進しに行ってきなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("---- ダンジョンゲート裏の広場にて ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：えっと、確かこの辺だったな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オッケー、発見発見っと！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っしゃ、早速転送してもらおうか！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　ッバシュウウゥゥゥゥン"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.Message_GoToAnotherField); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：えっと・・・カールハンツ爵はどこに・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ココだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：って、おわぁぁ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『カール爵は突然見たこともないファイアボールを放ってきた！』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゲ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『アインはとっさの判断で身をかわした』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ホラホラホラ！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『カール爵は次々と魔法の弾丸を撃ち込んできている！！』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っおわ！！っちょっちょ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『アインは５発のファイアボールらしき弾丸を何とか回避しきった』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッタタ、タンマタンマ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あのボケ師匠も大概だけど、あんたも無茶苦茶だないきなり・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハ、そうとは言えしっかりと回避してるようだが。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そりゃ、こんなもん一回一回食らってたらキリが無いだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：転送装置先では、敵が待ち構えてる場合も多い。気を付けるのだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（ググ・・・この人やっぱり敵なんじゃ・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：というか、見たこと無い魔法だったが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ひょっとして今のが！？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：聖と火の複合魔法「フラッシュ・ブレイズ」だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：やってみるが良い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：い、いきなりですか！？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：先の教え、覚えておるだろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：教えの通りにやると良い、貴君は習得済みであると言ったハズだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そ・・・そうかなあ・・・じゃあ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『アインは魔法詠唱の構えを始めた』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（こう・・・火に明かりを添えるようにして・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　ッバシュ！！　　　"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゲ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：まだまだだが、ひとまず出せたようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っそ、そんな本当に１回目で・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：驚いたか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：す・・・スゲェぜ！！　これ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：直感と感性で習得してきた貴君にとっては、新鮮な感覚であろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・あの講義のおかげですかね？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：当然だ。ずいぶんと無礼な質問だな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いやいやいや！スンマセンでした！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：今回はここまでだな、また来ると良い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ホントどうもありがとうございました！"); eventList.Add(ActionEvent.None);

                GroundOne.MC.FlashBlaze = true;

                messageList.Add("アインはフラッシュ・ブレイズを習得しました！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add(Database.Message_GoToAnotherField_Back); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("ガンツ：どうだったかね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・驚きました！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：その様子、どうやら身に付けたようだね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：これが驚きなんですよ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：始めから、クリーンに詠唱成功したんですよ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：よほど嬉しかったと見える。それほどかね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あんな体験は初めてでしたよ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何せ、はじめっからですよ・・・はじめっから・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、次からは好きなタイミングで彼の元へ訪れるがよい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、はい。また行ってみます！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：うむ、精進せいよ。"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.Message_GateAvailable); eventList.Add(ActionEvent.HomeTownMessageDisplay);
            }
        }

        public static void Message50005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：こんちわー。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ。いよいよ３階へと進むつもりか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、今日からスタートさせるつもりです。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ふむ、ワシから言う事は特にない。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっと、ハイ！ありがとうございました！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：だが、一つ言っておかねばならん事がある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッゲ、特に無いと言ったのに・・・この展開は・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、どこへ向かう？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どこって、ダンジョン３階です。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：バカの振りは不要。しっかりと答えなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：う～ん、そう言われても・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE2.TruthRecollection1 && GroundOne.WE2.TruthRecollection2)
            {
                messageList.Add("アイン：始まりの地へ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：広大な草原と無限に拡がる大空。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そこで俺は、ケリをつける。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：・・・・・・ふむ・・・・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：精進しなさい、アインよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：決して負けてはならん。よいな？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、任せてくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：絶対に今度こそ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：うむ、行ってきなさい。気をつけてな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、了解！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：ダンジョン最下層だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺は絶対にこのダンジョンを制覇してみせる！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：ふむ、その勢い、忘れぬようにな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ガンツ叔父さんと話していると元気が出るよ、サンキュー。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：無理だけはせぬようにな、毎日をしっかり生きなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、じゃあ行ってくるぜ！"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message50006(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：こんちわー。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、相変わらず元気そうじゃの。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：４階へは、やはり進むつもりか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：止めるつもりはなさそうだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええ、なんとか制覇してみるつもりです。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ふむ、精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイ、それでは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：待ちなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、剣を見せてくれんかね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：剣・・・？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：練習用の剣を以前渡したであろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ！　ちょっと待ってください。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええと・・・これだ。ハイ、どうぞ"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ふむ"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：・・・"); eventList.Add(ActionEvent.None);

            string detectName = PracticeSwordLevel(GroundOne.MC); eventList.Add(ActionEvent.None);

            if (detectName == Database.POOR_PRACTICE_SWORD_ZERO)
            {
                messageList.Add("ガンツ：＜　" + detectName + "　＞か。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：剣が成長しておらんようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え・・・？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：今、成長って言いました？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：そう伝えられておる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そ、そうだったんですか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：焦る事はない。興味があれば、また使ってみなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハ、ハイ！どうもすみませんでした！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：謝る必要はない。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ、それでは失礼します。"); eventList.Add(ActionEvent.None);

                messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（この剣・・・そうだったのか・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（まあ、気が向いたら使ってみるか）"); eventList.Add(ActionEvent.None);
            }
            else if ((detectName == Database.POOR_PRACTICE_SWORD_1) ||
                     (detectName == Database.POOR_PRACTICE_SWORD_2) ||
                     (detectName == Database.COMMON_PRACTICE_SWORD_3))
            {
                messageList.Add("ガンツ：＜　" + detectName + "　＞か。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：ほんの少しだけ、成長しておるようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ええ・・・何となくだけど、少しだけマシに振る舞えるようにはなりました。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それより今、成長って言いました？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：そう伝えられておる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そ、そうだったんですか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：焦る事はない。興味があれば、また使ってみなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハ、ハイ！どうもすみませんでした！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：謝る必要はない。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ、それでは失礼します。"); eventList.Add(ActionEvent.None);

                messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（この剣・・・そうだったのか・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（まあ、気が向いたら使ってみるか）"); eventList.Add(ActionEvent.None);
            }
            else if ((detectName == Database.COMMON_PRACTICE_SWORD_4) ||
                     (detectName == Database.RARE_PRACTICE_SWORD_5) ||
                     (detectName == Database.RARE_PRACTICE_SWORD_6))
            {
                messageList.Add("ガンツ：＜　" + detectName + "　＞か。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：なかなか、成長してきておるようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ええ・・・しかし、この剣、不思議ですよね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：使えば使うほど熟練度が上がるっていうか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：使いようによって、どんどん攻撃ダメージが上がってきてる感じがするんですよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：お主の言うとおり。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：そう伝えられておる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そ、そうか。どうりで・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：この調子で、その剣を使いこなしてみなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アイン、お主はきっと強くなれる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハ、ハイ！どうもありがとうございます！"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：謝る必要はない、精進しなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ、それでは失礼します。"); eventList.Add(ActionEvent.None);

                messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（この剣・・・確かに威力がどんどん上がってきている・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（４階層、一気に使いこなせるように振舞ってみるか）"); eventList.Add(ActionEvent.None);
            }
            else if (detectName == Database.EPIC_PRACTICE_SWORD_7)
            {
                messageList.Add("ガンツ：＜　" + detectName + "　＞か。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ。お主はこの剣が、何であるかは理解しているか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：理解・・・ですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いえ、多分理解までは・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：ふむ、良い心構えだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、答えはもう目の前である感覚はあるかね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ええ・・・正直な所、もう何となくは・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、お主はもう十分に強くなった。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：アインよ、常々、精進しなさい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハイ、どうもありがとうございます。"); eventList.Add(ActionEvent.None);

                messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（この剣への・・・理解・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（あと一息な感じはしている・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（もう一超え頑張るとするか！）"); eventList.Add(ActionEvent.None);
            }
            else if (detectName == Database.LEGENDARY_FELTUS)
            {
                messageList.Add("ガンツ：＜　" + detectName + "　＞か。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：よくぞここまで。見事だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いえ、これは俺が単に逃げ続けていただけですから。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：そうではない。向かい続けてきた結果だ。卑下をする事はない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：フェルトゥーシュ、今お主は、その手に所持しておる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ええ、確かにこの手に。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：恐る事なく、進めるが良い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：決して"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：決して、負けるな、アインよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ガンツ：精進を怠らず、進めよ。アイン・ウォーレンス。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：分かりました！"); eventList.Add(ActionEvent.None);

                messageList.Add("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（フェルトゥーシュにより俺は・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（分かってる、進むんだ）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（俺は必ず、この手で）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（決着を付けてみせる。）"); eventList.Add(ActionEvent.None);
            }
        }

        private static string PracticeSwordLevel(MainCharacter player)
        {
            string[] targetName = { Database.POOR_PRACTICE_SWORD_ZERO, Database.POOR_PRACTICE_SWORD_1, Database.POOR_PRACTICE_SWORD_2, Database.COMMON_PRACTICE_SWORD_3, Database.COMMON_PRACTICE_SWORD_4, Database.RARE_PRACTICE_SWORD_5, Database.RARE_PRACTICE_SWORD_6, Database.EPIC_PRACTICE_SWORD_7, Database.LEGENDARY_FELTUS };
            string detectName = String.Empty;

            for (int ii = 0; ii < targetName.Length; ii++)
            {
                if ((player != null) && (player.MainWeapon != null) && (player.MainWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.SubWeapon != null) && (player.SubWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.MainArmor != null) && (player.MainArmor.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory != null) && (player.Accessory.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory2 != null) && (player.Accessory2.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                ItemBackPack[] backpack = player.GetBackPackInfo();
                for (int kk = 0; kk < backpack.Length; kk++)
                {
                    if ((backpack[kk] != null) && (backpack[kk].Name == targetName[ii]))
                    {
                        detectName = targetName[ii];
                        break;
                    }
                }

                if (detectName != string.Empty)
                {
                    // 検知したため、検索不要
                    break;
                }
            }
            return detectName;
        }

        public static void Message50007(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ガンツ叔父さん、いますかー？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインか。よく来てくれた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：武具店、開いてますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：ああ、開店しておるので見ていくと良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ！やったぜ！じゃあ早速見せてもらうとするぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：好きなだけ見ていくと良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ。これからダンジョンへ向かうのだな？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツは両眼を閉じた状態で、誰へともなく、空中へ語り始めた。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：お前はものすごいモノを秘めている。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：お前は間違いなく打ちのめされる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：途中、決してくじけてはならん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：どうしてもしんどい時は一旦休みなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：お前ならきっと叶えられるはずだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：精進しなさい。アイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　　『ガンツは両目を開き、テーブルへ眼を戻した』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：心を決めたようだな。良い雰囲気だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハイ！！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE2.SeekerEvent604 = true;
            Method.AutoSaveTruthWorldEnvironment();
            Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
        }

        public static void Message50008(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ガンツ：アインよ、精進しなさい。"); eventList.Add(ActionEvent.None);
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

        public static void Message60001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.MeetOlLandisBeforeHanna)
            {
                messageList.Add("アイン：ふううぅぅ・・・こんちわーっす・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：あれま、どうしたんだい。らしくないため息なんか付いて。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、実はですね・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あのボケ師匠がＤＵＥＬ闘技場へ来てるみたいなんですよ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：そりゃ本当かい？良かったじゃないか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：はあああぁぁ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ちょっとそこで待ってなさいな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え？あ、はい。"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナは厨房から何かを持ってきた"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：これは一体・・・なんですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：キツ～い辛味スパイスを加えた、激辛カレーだよ、たんと食べな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：マジかよ・・・ッハッハッハ、悪いなおばちゃん。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうだな、考えててもしょうがねえよな。じゃいただきますっと！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：グオォォ！！！か、辛ええええぇ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：今のうちにキツいパンチをもらっておくんだね。アッハハハ。"); eventList.Add(ActionEvent.None);
                GroundOne.WE.MeetOlLandisBeforeHanna = true;
            }
            else
            {
                messageList.Add("ハンナ：っさあ、おとなしくＤＵＥＬ闘技場へ行ってくるんだね。"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message60002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ハンナ：おや、アインじゃないか。どうしたんだい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：叔母ちゃん、エルモラの紅茶一杯ください。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：あいよ。少し待ってるんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：はい、どうぞ召し上がりな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー、叔母ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ふう・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：どうしたんだい。言ってごらん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：２階行ってくるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：そうかい、頑張って来な。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ただ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っつ・・・上手く言えないんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：上手く行ってる証拠と考えたらどうだい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・っはい？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：何も無い状態なら、そんな風にはならないよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：何か想う所がある。違うかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っえ、ええ・・・まあそうです。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：だったら、その通りに進んでみたらどうだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：進まない限り、答えなんて見つかりっこないからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・そうか、なるほど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：叔母ちゃん、ありがとな。今度こそ、２階行ってくるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：あいよ、行ってらっしゃい。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationHanna21 = true;

        }

        public static void Message60003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (GroundOne.WE.Truth_CommunicationHanna31_2 == false)
            {
                messageList.Add("ハンナ：あら、そう言えば、忘れてたわ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アンタの師匠から預かってるわよ。荷物。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。そういや別れ際そんな事言ってたな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オバチャン、荷物管理とかもやってるのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アッハハハハ、やってないわね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ？あ、ああ・・・"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("ハンナ：アイン、ほらこっちだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ホントだ。ちゃんと置いてってくれてたんだな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ見えて、照れ屋だからね。アンタの師匠は。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アンタに期待してるみたいだったよ。感謝しなさい、ッホラ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ、ああ・・・サンキューな、オバチャン。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハハ、アタシじゃなくて、お師匠さんに感謝しなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハ・・・確かに。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しかし突然渡されてもな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オバチャン、少しだけの間、保管しておいてもらえるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、モチロンだよ。少しと言わずしばらくはずっと保管しといてあげるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：好きな時に持って行くんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あと、俺のアイテムも出来れば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：モチロン構わないよ。預けたいモノは預けていきな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあ、ホンット助かるぜ、サンキュー！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.AvailableItemBank = true;

            messageList.Add("ハンナの宿屋で「荷物の預け・受け取り」が可能になりました！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ハンナ：ただ、無限には受け取れないよ。こちらも倉庫は限られてるからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、少しだけでも。本当助かります。ありがとうございます！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：後は、アンタの好きなように整備しな。任せたわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！使わせてもらいます！どうもです！！"); eventList.Add(ActionEvent.None);
        }

        public static void Message60004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationHanna31 = true;

            messageList.Add("ハンナ：あら、どうしたんだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、紅茶一杯もらえるかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：はいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さあて、どうすっかな・・・ホント。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：なんの話だい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：スキルアップの話さ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺はもう十分強くなった、そう思うか？叔母ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハハ、もう十分強いんじゃないのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：んなわけねえよな・・・分かってて聞いてんだけどさ、ハハハ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：何迷ってるかは分からないけど、コレだけは言えるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、あんたは強い部類に入るわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええぇ・・・お世辞なんか良いですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：自分のウィークポイントなんか山ほどあるし、全然強くならないんですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：いいや、数多くの旅の人を見てきたアタシが言うんだから、間違いないよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや、本当・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：いやいや、あんたは本当に強いよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：う～ん、本当ですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：本当の本当ってもんさ、アッハハハハ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハハ・・・ありがとな。叔母ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：もし、３階が解けたらさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：なんだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：またいろいろと教えてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：何言ってんだい、アタシから教えられる事なんて無いよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：まったく。　ッホラホラ、行く前から落ち着いてんじゃないわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：キッチリ３階をクリアしてくるんだね、行ってきな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ！　オーケー！"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.Truth_CommunicationOl31)
            {
                messageList.Add("ハンナ：あらやだ、そう言えば忘れていたわ、アイン。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？　何かあるのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アンタの師匠から預かってるわよ。荷物。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。そういや別れ際そんな事言ってたな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オバチャン、荷物管理とかもやってるのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：アッハハハハ、やってないわね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ？あ、ああ・・・"); eventList.Add(ActionEvent.None);
                GroundOne.WE.Truth_CommunicationHanna31_2 = true;
            }
        }

        public static void Message60005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationHanna41 = true;

            messageList.Add("ハンナ：あら、どうしたんだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：すまねえ、爽快ドリンクを一本もらえるかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：はいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、サンキュー。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：いよいよ、４階に進むのかい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええ、まあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハッハ、何をそんなに怖気づいてるんだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、怖気づいてるわけじゃないんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何となくかな・・・ッハハ"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：そんな所、ラナちゃんには見せられないね。台無しだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや、なんでアイツが出てくるんですか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：おや、出てきちゃ悪いのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、関係ねえ話かなと思って・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：で、どうしたんだい？怖気づいたんじゃないとしたら"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：多分、迷ってるんですよ、俺。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヒトゴトみたいに言ってるのもオカシイんですけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：【迷いが拭えない】って言ったらいいのか・・・なんだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今までのが水の泡になったら、って考えると、先に進めなくなるんですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：４階に今行きたくないんなら、１日伸ばしたらどうだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、行きたくないわけじゃないんですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：行くのが、怖いのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、怖いわけでもなく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・なんとなくですが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：【誤った】っていう感触が襲ってくる感じなんですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：進めば進むほど、その感覚が強くなる感じがして・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：【誤った】というのは感覚の問題だよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：世界から見れば、【誤った】【正しかった】は存在しない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それに関しては、ボケ師匠から嫌というほど知らされてます、分かってるんです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だからこれも理由としては違う気がしてて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、深く掘りすぎない事が肝心だよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：あんたは昔からその独特なクセがあるみたいだからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：クセか、ッハハハ・・・確かにそうかも。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：【誤った】ことを感じたままの状態で、進めなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：【正しかった】で前提で進む心意気が把握できてるのなら"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：今の状態は【誤った】感を察知した上で進めるのも心構えは同じだとは思わないかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：誤った感を察知した上で・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほど・・・なるほど、そうかもな！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうだな！そうだ、そうだ！そうだよ！サンキュー！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあ、おばちゃんのトコ来ると本っっ当に助かるぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハ、そうかい。元気になれたんなら良いよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アンタが冷えると、隣のラナちゃんも冷え込んでくるからね。まあ、気をつけな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや、だからアイツは本っっ当関係ないでしょうが、まったく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハハ、そういう事にしておくわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ッホラ、じゃあ頑張って行ってきなさい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ありがと！　じゃな！"); eventList.Add(ActionEvent.None);
        }

        public static void Message60006(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：叔母さん、いますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アインじゃないか。何の用だい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、特に用ってわけじゃないんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：どうしたんだい、何か気になる事でもあるのかい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうやって、そんな美味い飯を作れるようになったんですか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：う～ん、どうと言われてもねえ。慣れみたいなもんさ。アッハハハ"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：どうしたんだい、今からダンジョンへ向かうんじゃないのかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：悩んでるようだね。言ってみな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、もう行かなくちゃ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：叔母さん、本当にどうもありがとう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アッハハハ、変な子だね。あたしゃ何もしてないよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや、ありがとう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃ、行ってくる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、行ってきなさい。体に気をつけるんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ"); eventList.Add(ActionEvent.None);

            GroundOne.WE2.SeekerEvent603 = true;
            Method.AutoSaveTruthWorldEnvironment(); eventList.Add(ActionEvent.None);
            Method.AutoSaveRealWorld(GroundOne.MC, GroundOne.SC, GroundOne.TC, GroundOne.WE, null, null, null, null, null, GroundOne.Truth_KnownTileInfo, GroundOne.Truth_KnownTileInfo2, GroundOne.Truth_KnownTileInfo3, GroundOne.Truth_KnownTileInfo4, GroundOne.Truth_KnownTileInfo5);
        }

        public static void Message69993(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ハンナ：荷物倉庫かい？ホラ、コッチだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownExecItemBank);

            messageList.Add("ハンナ：また用があったら寄るんだね。"); eventList.Add(ActionEvent.None);
        }

        public static void Message69994(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ハンナ：もう朝だよ。今日も頑張ってらっしゃい。"); eventList.Add(ActionEvent.None);
        }

        public static void Message69995(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("休息を取りました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }

        public static void Message69996(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：ごめん。まだ用があるんだ、後でくるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。"); eventList.Add(ActionEvent.None);
        }

        public static void Message69997(ref List<string> messageList, ref List<ActionEvent> eventList, string tempSelect)
        {
            messageList.Add("アイン：おばちゃん、『" + tempSelect + "』を頼むぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：『" + tempSelect + "』だね。少し待ってな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：はいよ、お待たせ。たんと召し上がれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【アインは十分な食事を取りました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ふう～、食った食った・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おばちゃん、ごちそうさま！"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：あいよ、後は明日に備えてゆっくり休みな。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownExecRestInn);
        }

        public static void Message69998(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ハンナ：はいよ、部屋は空いてるよ。ゆっくりと休みな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownNight);

            messageList.Add("ハンナ：今日は何か食べていくかい？"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallRequestFood);
        }

        public static void Message69999(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：おばちゃん。空いてる？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：空いてるよ。泊まってくかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうすっかな・・・泊まるか？"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.Request_Inn); eventList.Add(ActionEvent.HomeTownYesNoMessageDisplay);
        }
        #endregion

        #region "カールハンツ爵の訓練場"
        public static void Message70001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：先生、質問。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（何だこの異常な威圧感は・・・ぐぬぬ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああっとですね。火と理を融合させる所はなんとなく分かるんですが"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：何となくという理解そのものが危うい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっと、ハイ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：理とは、この世の【自然】、【原理】、【原則】そのものを指す。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そして火とは、【浄化】、【エネルギー】、【進行】そのものを指す。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：原則を発展させるイメージを伴わせるには、その普遍的な概念を構築するが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ハイ。そうなんですけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（この威圧感を何とかしてくれ・・・ッグググ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：先と同じ展開。どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『火』って何かこう・・・まとまりが無くて、危なっかしいイメージじゃないですか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも『理』ってのは何だろ・・・全てが一貫してビシーっと筋が通ってると言うか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それを融合させるって所が何となく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『火』の動作は決してランダムではない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『火』の移りゆく現象、それは予め定められた軌跡を辿る現象である。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『理』とて同義。全ては決定づけられた事象を指す場合もあるが、"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：始まりの条件付けで結果は千差万別。それは『火』の動作そのものでもあるのだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その始まりとなるのは己自身、つまり貴君のイメージが始まりだと考えれば良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　す・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：すげぇ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生、やっぱアンタ天才ですよ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺は講義でこんなにもイメージが行き届くのは今まで味わったことが無いもんで・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、いやいやいや、ホンットどうもです！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：転送装置の時間だ、そろそろ帰るが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：（・・・　・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：（一度でここまで習得してくるとは。ランディスもさぞ楽しい事だろうな）"); eventList.Add(ActionEvent.None);

            GroundOne.MC.EnrageBlast = true;

            messageList.Add(Database.ENRAGE_BLAST); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }

        public static void Message70002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：詠唱スタイルなんですけど、どうもしっくり来ないんですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：違和感を感じるのは、『聖』『理』の相性が良く、厳格さのイメージが強すぎるが故。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君は本来、その気質を有しているはず、しかし普段は出していない。違うかな？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、いやいや。そんなんじゃないです、結構俺って適当派なんで"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ランディスの言ってた貴君の病気。無意識にまで入り込んでるようだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、あのボケ師匠にも言われた事はあるけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、そもそも今回のホーリー・ブレイカーは攻撃を攻撃として跳ね返すって事ですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それだけの事だと思うし、俺自身がしっくり来てないだけです。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『擬眼がギョロリと動きはじめた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我の前でそのような態度、取らぬ方が得策と心得よ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは背筋に更に尋常ではない威圧感を感じた。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しっくり来ないってんじゃなくて・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これは俺自身の問題。そう考えます。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：己自身が一番把握しているのだろう。己自身に対して向きあうと良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ホーリー・ブレイカーは攻撃ダメージの分をそのまま相手に跳ね返す。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その分、自分自身もライフを消費する事には代わりはない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：もし、貴君が真の連携を求めているのであれば、今のスタイルは一旦捨てる事まで考え抜く事だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ・・・それは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、はい。ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.HolyBreaker = true;

            messageList.Add(Database.HOLY_BREAKER); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }

        public static void Message70003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの・・・最初いつも姿が見えないんですけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（何だこの威圧感は・・・ッグ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！！　おぉぉっと！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ほんっと驚かせないでくださいよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：敵の気配ぐらい、事前に察知せよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも、今みたいな雰囲気だと、どうしようもないですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君は『動』と『剛』を兼ね備えておる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：周囲一体をひとまず切り払ってみたらどうだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：急にそんな事、できるわけが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その辺の素質、ランディスが買うだけの事はあるようだな"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインはいつものストレートスマッシュの構えを始めた』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（こっから・・・体の軸をブレさせず、意図的に力をこめれば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おらよっと！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　ヴオオォォン！！　　　"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『サークル・スラッシュ』とでも名づけておくがいい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（何だこれ・・・次々と・・・バカな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール師匠"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我は師匠ではない。　なんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今の俺のサークル・スラッシュって、まだまだですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：当然だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：スンマセン、良かったらもっと講義をお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ずいぶんと殊勝な心がけ、よほど気に入ったようだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：知識の集約だけでここまで来るとは思ってませんでした。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識の無い側からすれば、そう感じられる。当然の反応だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これって知らない人は一生気付けないんじゃないですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：個人の態度次第だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・そうか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：・・・転送装置の時間切れが近い、今日は戻ると良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ！どうもありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.CircleSlash = true;

            messageList.Add(Database.CIRCLE_SLASH); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }

        public static void Message70004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：聞きたい事はあるか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、じゃあ１個だけ。ええとですね・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そもそもどう考えれば良いんですかね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その言い方からして、カウンターの概念を聞きたいのだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっとそうです、スイマセン・・・そうそう、カウンターの概念です。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：このバイオレント・スラッシュはカウンターされないスキルである。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ただ貴君は既に察したようだが、それは相手にヒントも与える。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうなんですよ。カウンターされないからそうする場合はケースが特定される。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：致死ダメージに至るか、またはどうしても物理ダメージに付随する何かを通したい時。でも・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その通り、それこそカウンターの格好の的。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：カウンターはされないが、ヒール魔法のスタックを積む事ぐらい容易であろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：また致死ダメージに至らないのであれば、相手は別の大事な手をそこで発動するだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：で、あるとすればカウンターされない事自体に大した効果は望めない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：カウンターとはどう考えれば良いか、と言うことになる。これで良いかね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ、そうそうそう！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジそうなんですよ！　ソコを教えてください！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君なりの解釈は持っているかね、あれば言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そう・・・ですねえ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうせこういった場合、相手もインスタント値を蓄えている。だとすれば"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヒントらしいヒントを与えない行動。そのタイミングで放てばいい・・・かな？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：筋は良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：やった！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：だが、及第点ではない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッガク・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：圧力をかける行為、それがこのバイオレント・スラッシュの一番の使い道。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：圧力？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：致死に至らない状態から、このスキルを食らう側だとする。だとすれば"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、食らうしか無いって思うぐらいか・・・ダメージは食らわざるをえないとして・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そおおぉぉかあぁぁ、そうか！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：威力倍増！クリティカル！ゲイル・ウィンド！なんとでもやり方はあるじゃねぇか！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：つまりライフ満タンでも、それが激減すりゃそれ自体が脅威そのものって事だ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：致死に至るケースじゃなくて、むしろ始めっから窮地に追い込む事をすり込ませる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：要は、相手に圧力を事前に仕込める最大の手法ってワケだ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：及第点だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：本来、講義でこういった内容までは踏み込まないが、貴君のみ特別である。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、そうなんすね・・・ホントありがとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：まだまだ奥は深い。自分なりのスキル使用構築のスタイルを築くと良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そろそろ転送装置の時間だ、帰るが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.ViolentSlash = true;

            messageList.Add(Database.VIOLENT_SLASH); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }

        public static void Message70005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：敵の注意を引くのには、個が全体を意識して初めて可能である。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：本スキル使用時、必ず自分にダメージもしくは負のＢＵＦＦ効果がかかるため、失敗は許されぬ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：確かに、こういうスキルは使い所を間違えたくは無いな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした、言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：敵が敵パーティにライフ回復させようとしてた場合はどうなる？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：対象外だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：自分対象の時、これを使うとどうなる？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：対象は変わらぬ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：対象を取らない全体系は自分だけを対象に変更することは？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：不可能だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：対象をこちらに向けた直後、敵が対象を選びなおす事は？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：よほどの特例が無い限り、不可能だ。そのためのスキルでもある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー。すげぇ助かるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その抜け目の無さ。若い頃の我と類似する点がある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マ、マジっすか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：しかし、発想の原点がまだまだ稚拙。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：とほほ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.RumbleShout = true;

            messageList.Add(Database.RUMBLE_SHOUT); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70006(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ただし、今回の講義は少々特殊なものとなる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どういう意味ですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君に逆属性に関する原論を今から徹底的に叩きこむ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：逆属性？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：では、その全てを今から教える。心して記憶せよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：本来、インスタント値の回復は生物の自然回復をベースとしている。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『ワード・オブ・アティチュード』この『水』と『理』による複合魔法はそれを可能とするもの。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の場合、『火』ベースであるため、『水』『理』は論理矛盾をきたす。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：しかし、『火』の逆となる『水』をもイメージの源泉とすれば可能となる。後に実行してみるが良かろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：論理矛盾・・・か・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：論理ってのはイマイチ掴めない、そんな感じがしてさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どこまでが論理で、どこからが論理じゃないのか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生の言ってる事は受け入れられない内容じゃない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：むしろ、話自体は筋が根幹から通っていて、聞いててスっと入ってくるし、スゲェ分かる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だからこそ、論理矛盾って言われると、聞きたくなるんですよ。良いですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：矛盾したらソコで終わりじゃないのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：間違いなく終わりだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だったら逆属性そのものに無理があるんじゃ？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：無理が生じる、至極当然。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、マジかよ・・・じゃあ無理でしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：人間は論理的矛盾に陥ったと実感した時、心的ダメージは非常に大きい。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：誰にでも出来るモノではない。そういう事だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君は我の今までの講義を聞き、そして今もここにいる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え、まあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：であれば、貴君に資質はある。それを踏まえるがよい。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインはいつもの表情を崩し、今までにない真剣な表情でこう告げた』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：正直、出来ないヤツも居るって事ですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そういう事だ。不服か？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：不服とかじゃなくて、出来ないヤツはどうすればいいんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：出来ない者は、そもそも我の前に現れる事なく、自然と流れ行く。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：・・・ふむ、なるほど。その動揺、自分以外の誰かを察しての事と見える。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君がココに初めて来た時もそうであったな。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ちょうど良い、我の前にその者をココに連れて来ると良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っえ、良いんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君のその異常なまでの心の気配り、それを直さねばなるまい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、気配りなんてしてないですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：よい、今日はここまでだ。　知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、はい。いろいろ突っ込んだ所までスンマセン、ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.WordOfAttitude = true;

            messageList.Add(Database.WORD_OF_ATTITUDE); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70007(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あら？・・・居ない・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや・・・また変な所から仕掛けてくる可能性も・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君にとっての第一逆属性『水』。では早速実践にうつるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うおおぉぉ！！　ビビびっくりするじゃないですか！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っと・・・実践？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：素質は十分に感じられる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：実践って実践ですよね！？　っしゃ、待ってました！　じゃあ早速！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：少し距離を取るとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッバシュ！！』（カールは一瞬で向こう側へ姿を移動させた！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（何だ今の・・・テレポートみたいな現象だったぞ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：今から貴君にフレイム・ストライクを乱射するとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：実践の中で、我の攻撃を受け止める魔法、新しく発見してみせよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っはい？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：行くぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッボシュ！ッボボボシュ！！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ！？　っちょっちょちょッタンマ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインに一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグハァ！！　ッグ、くそ・・・シャレになってねぇダメージだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。実践では誰も貴君のペース配分など待ってくれはせんぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッボシュ！ッボシュ！ッボボボシュ！！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ックソ・・・回避しきれねぇ、早すぎる！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインにもう一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ！！　ッグ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ホラホラホラ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインにもう一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ、ゲホ・・・ボケ師匠と同じノリじゃねぇか、クソ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こんな中で・・・イメージなんか出来るかっつうの。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言っておくが"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッボシュ！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君が死ぬまでこれは続く。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッボボボシュ！』"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドドドシュ！』（アインに追加で３撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ！！ッグアアァァァ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（駄目だ・・・避けようなんてのは無理がある・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ホーリー・ブレイカー！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインにもう一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：残念だが、それでは魔法ダメージは防げぬ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインにもう一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：イメージを飛躍させよ。貴君なら出来るはず。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドシュ！』（アインにもう一撃が入った！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグ！！・・・たく、ボケ師匠もそうだが、どうしてこう無茶苦茶な・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・イメージの飛躍ってどういう事だよ、逆属性の水で・・・？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッドドドドシュ！』（アインにもう４撃入り、致命的なダメージとなった！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッグアアァァァ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは意識を失う寸前で、あるイメージが浮かばせた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッム！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：では、トドメだ。　ラヴァ・アニヒレーション！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッズゴゴオォォォン・・・』"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『・・・　・・・　・・・』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハァ・・・・ッハァ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：それでこそランディスの弟子と言えよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：魔法ダメージを０にする・・・スカイ・シールド・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ってか・・・もうダメ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君はこれで、火の逆属性となる『水』との複合をまた一つ習得した事となる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：また、本魔法は３回まで蓄積可能な魔法である。後で知識習得の時間を与えよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っちょ・・・倒れさせてください・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・ッバタ）"); eventList.Add(ActionEvent.None);

            GroundOne.MC.SkyShield = true;

            messageList.Add(Database.SKY_SHIELD); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70008(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの、ちょっとタンマ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今日はちょっと実践は良いんで講義でお願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：よほど前回のが堪えたと見える。何ならいつでも実践相手になろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや、ちょっホント勘弁・・・！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフハハハ、楽しみにしているぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハハ・・・（やっぱこの人敵なんじゃ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・つまりこれって、『火』と『水』って事ですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その通りだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：完全なる逆属性同士の複合魔法となるため、詠唱形態は極めて特殊。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：加えて、イメージの源泉も始めから相反するモノをイメージする必要がある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：本当にこんなのが可能なのかよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：魔法効果自体は、剣に氷を付与するのみ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：逆属性の基礎を習得した貴君なら造作も無きこと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そりゃ、そうかも知れませんけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：フレイム・オーラで剣に火属性を付与しておくじゃないですか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で、後付けでこのフローズン・オーラも付与可能だって言ってるんですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：可能かどうかは貴君次第。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：とほほ・・・ボケ師匠とノリが一緒だよなこういうトコ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あれ、まてよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ボケ師匠もこういうの出来るって事ですよね？？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：当然。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・待てよ待てよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフハハハハ、ランディスに対する戦術構築か。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そりゃ、そうですよ！　あのボケ師匠は何か反則っぽい事してる気がしてたんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、フローズン・オーラ絶対に使いこなしてやるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で、フレイム・オーラも付けて、今度こそボコボコにしてやる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：一つ、忠告しておこう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：完全なる逆属性の融合のため、他の複合と比べて、詠唱コストは極めて高い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：マナの枯渇には気をつける事だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほど・・・了解！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.FrozenAura = true;

            messageList.Add(Database.FROZEN_AURA); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70009(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);
            
            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：本日からは、体術の方に専念する。心せよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：体術に関しては、ランディスから何度も訓練は受けているだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：嫌というほど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『静』と『心眼』による複合スキル『シャープ・グレア』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の場合、『動』が基本性質であるため、『静』は逆の性質となる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：しかし、ランディスの実践訓練を積んでいる故、貴君にその懸念は不要。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんなものなのか・・・嬉んでいいんだろうか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：この『シャープ・グレア』は身体への打撃に際し、魔法詠唱を失敗させる効果を持つ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：相手のインスタント行動時に魔法詠唱だった場合も、これでカウンターは可能？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そういう事だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：加えて、沈黙効果が一定期間続く。魔法詠唱メインの者にとっては警戒すべきスキルとなろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：沈黙効果がある程度続くって事は・・・アンチ系のスキルって事になるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その通りだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ただし、ニゲイトに比べ消費コストは多い。スキルポイントのペース配分にも気を配るが良かろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.SharpGlare = true;

            messageList.Add(Database.SHARP_GLARE); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70010(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『静』の要素ってほんとこういうのが多いですよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：効果が見えないつうかなんつうか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：スタン、麻痺、凍結への耐性を持つのは戦術理論上では、極めて重要。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあそうですよね。こんなのが発動出来るとすれば・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：今から攻撃に転じるぜ、って言ってるようなモンだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：・・・フム。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君のその基本センス、高い先天性を有しているようだが。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：それがランディスにとっては、格好の的とも言える。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君は考え方が非常に一貫しており、かつ、洗練されている故"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の行動には乱れや揺らぎが発生しにくいため、我にとっては非常に掴みやすい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：本スキルは完全なる防御に徹するための戦術。そう捉えても良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：守ってても勝てなくないですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ガードスキルが何か別の主戦術を補うものであるとすれば。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：あるいは、多段戦術の一角を匂わせるためのダミー行動。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：更にあるとすれば、２ラインの戦術を交互に行うための布石であるとも考えられる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：確かにボケ師匠はいつも最初同じ格好のクセに、大概やり方が無茶苦茶だよな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうか・・・一つの行動に付き、より多くの選択肢を考慮しろって事ですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その通りだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『静』か・・・防衛的戦闘スタイルとかもありそうだな・・・確かに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.ReflexSpirit = true;

            messageList.Add(Database.REFLEX_SPIRIT); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70011(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：来たか、それでは講義を始めるとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、お願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは集中して講義の内容を聞いた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：完全逆性質となる『動』と『静』、この複合においても極めて特殊。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：動作の一貫として、動へと転じる体型に加え、静の体型も乱してはならない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：体型のイメージは総じて動と静が相殺され、通常行動スタイルと何ら変わりは無くなる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：スキル消費を完全になくし、完全なる通常攻撃。インスタント行動も可能。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：『ニュートラル・スマッシュ』、使いこなしてみると良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：完全なる・・・通常攻撃・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・しかし、これはどうなってるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え・・・っと・・・いや、待てよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインはいつもの表情を崩し、今までにない真剣な表情となった。』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・おいおい、っちょ、待ってくれよコレって・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：スキルポイントはペース配分が肝だ。それなのに、このスキルにはそれが無い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そういやFiveSeekerには、技の達人が居ましたよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：気づきが良いな。そう、彼「ヴェルゼ・アーティ」は好んでそれを多用していた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：技が上がれば、インスタント値の回復は早いって事は・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アインは異常なまでに冷や汗をかき始めた！』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オイオイ・・・おいおいおい！　冗談じゃねえぞコレ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：気づいた様だな。その通りだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：１ターンにおける直接攻撃回数が膨れ上がるじゃねえか！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ、そりゃ確かにインスタント行動中に、隙を見て何か入れられたら嫌だけどさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ってか、スキル消費ねえし、ほとんど任意のタイミングじゃねえか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：その使用方法はほぼ無限。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君も今、それを身につけた事となる。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：思う存分に使用すると良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カ、カール先生！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そのなんて言って良いか・・・ありがとうございました！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君のポテンシャルは非常に高い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我の教えを必ず活かせるようになる事を期待する。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：後は、ランディスとの実践訓練でもすると良いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハ、ハイ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ありがとうございました！"); eventList.Add(ActionEvent.None);

            GroundOne.MC.NeutralSmash = true;

            messageList.Add(Database.NEUTRAL_SMASH); eventList.Add(ActionEvent.HomeTownShowActiveSkillSpell);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add(""); eventList.Add(ActionEvent.None);
        }
        
        public static void Message70012(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            if (GroundOne.WE.Truth_CommunicationSinikia30DuelFail == false)
            {
                messageList.Add("アイン：っとと・・・着いたみたいだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：カール先生、居るか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・いねえかな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（しかし、何となくだが・・・)"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（気配はねえが、妙な威圧感が空気に漂ってやがる）"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　アインは威圧感の源泉を探り始めた。　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：カール先生、居るんだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（絶対にどこかにいる。この感覚、間違いねえ。）"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『その瞬間、アインの目の前に３本のツララが突如発生した！！』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っげぇ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『アインはとっさに避けようとし・・・』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ブルーバレットに続けて、ワード・オブ・アティチュード発動。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：続けて即座に、ブラック・ファイアだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『ッボゥ！！！』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグハ！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『アインの魔法防御力が下げられた！』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：食らった所、大変貴君には申し訳ないが、今から実践講義を行う。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え、えええ！？　マジかよ！？　今既に戦闘中なんじゃねぇのかよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君に"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義『元核』の基礎を授けよう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：奥義？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフ、ダーケン・フィールド！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゲ！シマッた！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハ、集中が切れておるようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：オイオイオイ、どっちなんだよ、ックソ！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義『元核』は一朝一夕でどうにかなるものではない。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：アウステリティ・マトリクス、発動。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っとぉ、そいつはスタンス・オブ・アイズでカウンターだ！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：インスタントで、レッド・ドラゴン・ウィル。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　『カール爵の【火】属性の魔法攻撃力が格段に上昇した！』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っげぇ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハ、このまま蹴散らさせてもらおう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っくそ、奥義の話はどうなったんだよ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：では、このままDUELを執り行う。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おいおいおい、こんな所からかよ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハハ、冗談だ。BUFF効果やライフ、マナなどは全て全快が基本ルールであるからな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッホ・・・（でもやっぱ、この人ムチャクチャだ・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ところで、ランディスは貴君に対し、かなり指導的な行動を取っているようだが、"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゲェ・・・あれのどこが指導的なんだよ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ヤツは貴君に対して、甘すぎる。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：そのような事では、到底、奥義【元核】は習得できないものと思え。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：実際、どうすりゃいいんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ふむ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義【元核】とはその個々の本質そのものを指す。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その個々の本質とは、本人にのみ知りうるものであって、他者が貴君に教えたり授けたりするものではない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ってことは・・・カール先生から教えてもらうってわけには行かないのか？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：そのとおりだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うーん・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：だが、引き出すための指南、ある程度であれば可能である。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：やってみるかね、アイン・ウォーレンス。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお！もちろんですよ、是非！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：で、どうすれば良いんですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：この我自ら、真剣勝負のDUELを貴君に申し込む。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っな！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　アインは突如、背筋に異常な威圧感を感じ始めた　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：DUELっつっても、さっき冗談って"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君に今一度、問おう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：本気で示す戦闘術とは何かを。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それってどういう意味だ？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ランディスに聞く限り、貴君はあらゆる局面において"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：手加減、いわゆる手抜きを行っておる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いやいや、してねえって。DUELでは特にそのつもりだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その心構え、我には筒抜けであることを知れ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：我の問いの意図は、理解はしておるだろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義を会得するかどうかは、あくまで貴君次第。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　異常な威圧感は殺気へと変わり始める　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：我は貴君を殺すつもりで行く。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君も我を殺すつもりで挑むと良いだろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：さもなくば、貴君はこの場で果てる。死あるのみだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（やっべぇ・・・マジで勝てそうにねえ・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（でも・・・やるしか・・・ねえ！！）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：すうぅぅぅ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ふうぅぅぅ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：DUELで、手加減はしねえ。相手に対して失礼だからな。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『アインはカールハンツに対して、無表情の顔付きでッスっと剣を構え始めた』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・行くぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：来るがいい。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：っとと・・・着いたみたいだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：カール先生。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：挑戦させてくれ、奥義『元核』の習得。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：挑む姿勢は認めよう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：だが、貴君が我を殺すつもりで来なければ、奥義習得の道は無いと思え。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、分かった。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『アインはカールハンツに対して、無表情の顔付きでッスっと剣を構え始めた』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：DUEL・・・勝負だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：来るがいい"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(Database.DUEL_SINIKIA_KAHLHANZ); eventList.Add(ActionEvent.HomeTownCallDuel);
        }
 
        public static void Message70012_2(ref List<string> messageList, ref List<ActionEvent> eventList, bool result)
        {
            if ((result) ||
                ((result == false) && (GroundOne.WE.Truth_CommunicationSinikia30DuelFailCount >= 3)))
            {
                // 勝った場合、次の会話へ
                GroundOne.WE2.WinOnceSinikiaKahlHanz = true;
                GroundOne.WE2.AvailableArcheTypeCommand = true;
                GroundOne.MC.Syutyu_Danzetsu = true;
                GroundOne.WE.AvailableArchetypeCommand = true;

                if ((result == false) && (GroundOne.WE.Truth_CommunicationSinikia30DuelFailCount >= 3))
                {
                    messageList.Add("アイン：ッグアァ！！　・・・ッ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　【【【　アインは壊滅的なダメージを喰らい、大量の血を吐きだした　】】】"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ここまでのようだな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッゥ・・・ック！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：コ・・・ココだ！！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　それは　　　　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　一瞬の出来事　　　　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッム！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　カールハンツ爵の瞬速の詠唱開始タイミング　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ワン・イムー・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　アイン・ウォーレンスは　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（見つけた・・・詠唱タイミング！！！）"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　極限の状況の中　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　瞬間的なる時間停止にも等しい刹那　　』　　"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッラアアアァ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　ッドシュ・・・！！！　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッグ・・・ム・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（だ、だめか・・・意識が・・・）"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("カール：ッ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　それは　　　　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っちぃ！！次がヤベェ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　一瞬の出来事　　　　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッム！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　カールハンツ爵の瞬速の詠唱開始タイミング　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ワン・イムー・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　アイン・ウォーレンスは　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（スタティック・バリアからワン・イムーニティに見せかけ・・・ココ！！！）"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　　極限の状況の中　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　瞬間的なる時間停止にも等しい刹那　　』　　"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッラアアアァ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『　　ッドシュ・・・！！！　　』"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ッグ・・・ム・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ックソ、ハズれたか！　しまった！！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ヤベェ！！イムーニティからヴォルカニック・ウェイヴ連発が！！！"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("　　『　　・・・（ドサッ・・・）　　』"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『　　カールハンツの胴体はわずかな音と共に、その場に伏せた。　　』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っえ！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：カ、カール先生大丈夫ですか！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハ、ッフハハハハハハ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え、えーと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：我の負けだ。"); eventList.Add(ActionEvent.None);

                if ((result == false) && (GroundOne.WE.Truth_CommunicationSinikia30DuelFailCount >= 3))
                {
                    messageList.Add("カール：DUELは終了だ、ひとまず回復呪文をかけておいてやろう。"); eventList.Add(ActionEvent.None);

                    messageList.Add("カール：ゲイルウィンド、そしてサークレッドヒール。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　『アインはほんのり回復した気がした』"); eventList.Add(ActionEvent.None);
                }

                messageList.Add("カール：今の一撃、見事なり。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：今の一撃？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『　　カールハンツの胴体がそのまま浮き上がる様にしてもとの立ち姿勢に戻った。　　』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その様子では、自分自身で掴みきれておらぬ感じだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺、カール先生にそんな致命的な一撃を与えていましたか？？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：紛れもなく。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いつ？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：我が伏する直前に。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：どんな風に？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：剣による斬り込み。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・俺自身が？？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その通りだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：お主が名付けると良い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義『元核』は人により千差万別。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君自身が納得の行く名称をつけると良いだろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：えーと・・・名称・・・名称・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何だろう・・・つっても、全然どうやったか思い出せないんだが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：集中・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　集中と　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：断絶"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：フム"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：『集中と断絶』で、どうかな？カール先生。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君の気のゆくままで良かろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アインは奥義【元核】『集中と断絶』を習得した！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

                messageList.Add("アイン：なんかさ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：もっとこう、「やったぜ！！」とか「ついにきた！！！」って感触があると思ったんだが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：今は、全然と言っていいほど実感が無い・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・そんな実感だな。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：本人にのみ、知覚可能な領域であり、本人にとっての唯一無二。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：本人にとって、五感では認識し得ない領域であり、本人の深淵に眠る心にのみ認識しうる。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その本人の心にのみコンタクトが取れた瞬間から発動が可能となる。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：探り、導き出すのではなく、元から存在している心。それを貴君自身が体現させる。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：奥義『元核』は、そういうものである。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ああ・・・確かに。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何か、すげえ自然だ。理論としても、感情面からも自然だった。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：しかし、カール先生を倒したってのがありえないぜ・・・当たった気がしなかったもんな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：カール先生さ、結構オーバーアクションで倒れてくれたんだろ？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：その通りだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：げ、マジかよ！　やっぱり、かすってた程度だったんだろ。っくそぉ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：もう教える事はない。そのまま帰ると良い。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そっか・・・何か名残惜しいけど・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：今日は本当、ありがとうございました！！"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：もうよい、行くがよい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：はい、どうもでした！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『アインは転送装置により町へと戻っていった。　』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッグ・・・ッグホォ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『カールハンツはその場で大量の吐血をし、胴体から赤い線を大量に流し始めた！！　』"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッグ・・・ッムゥ・・・グ、ッグホ！ッゴホ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("？？？：大丈夫ですか？カールハンツ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッグ・・・貴様"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：フ・・・ファラか。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッグ・・・ッゲホ、ッゴホ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：ウフフ、どうやら、かなり食い込まれたみたいですね（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：セレスティアル・ノヴァ・エグゼ"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『カールハンツの致命傷がみるみる回復し始めた』　"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッグ・・・ッフゥ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：はい、もう大丈夫だと思いますよ（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：世話をかけた。"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：ひょっとして、私の回復呪文以外だったら、【死】だったのではありませんか？"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：笑止"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：どうやら当たりのようですね（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：私も見ていましたけど、彼の斬り込み、相当なものでしたわ。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ランディスが目を付ける理由。分からなくもない。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：今の時点で【元核】によるダメージがあの威力となれば、おそらく。"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：そうね、内容は直接攻撃を一撃だけだから。"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：ウフフ、将来はきっと限りなく無限に近いダメージなりそうね（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：よかったわね、本当に死ななくて（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ッフハハハ、冗談が過ぎるのではないか、ファラ王妃。"); eventList.Add(ActionEvent.None);

                messageList.Add("ファラ：ウフフ、無理しないでくださいね、こちらも回復作業が大変ですから（＾＾"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：フム、肝に命じる。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

                messageList.Add("　　『　アインは転送装置から町へと戻ってきて・・・　』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っふうぅ・・・戻りっと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っと、うわっとと！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『　アインは突如、足を崩してしまった。　』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っと、クソ・・・なんでもねえ所で、変に足にきたな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・心なしか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（足に妙に力が入らねえ。）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（さっき発動した奥義は思ったより身体に負担が大きいみたいだな・・・）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（こりゃあ、一日に出来て１回だな。連発はできそうもねえ。）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：（使いどころは難しそうだ、気を付けないとな。）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それはそうと・・・後でラナにも、奥義の話を伝えてやるとするか！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：ッグアァ！！　・・・ッ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　アインは壊滅的なダメージを喰らい、大量の血を吐きだした　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ここまでのようだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッゥ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：今の攻撃で、なお生き存えているのは、賞賛に値する。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ッ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：このまま殺すのは惜しい、回復呪文をかけておいてやろう。"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：ゲイルウィンド、そしてサークレッドヒール。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『アインはほんのり回復した気がした』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグ・・・ッツ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("カール：貴君には素質が無かったと見える。そのまま帰るがよい。"); eventList.Add(ActionEvent.None);

                GroundOne.WE.Truth_CommunicationSinikia30DuelFail = true;
                GroundOne.WE.Truth_CommunicationSinikia30DuelFailCount++;

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);
            }
        }
 
        public static void Message70013(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationSinikia31 = true;
            GroundOne.WE.alreadyCommunicateCahlhanz = true;
            GroundOne.WE.AlreadyCommunicateFazilCastle = true;

            messageList.Add("アイン：よし、転送装置ゲートに着いたぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：確か、この辺りの木の枝よ・・・えっとね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っあ、あったわ、コレね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうみても普通の枝だけどな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：単なる枝だったら少し曲がるぐらいでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、そうだけどな。無理にヘシ折るなよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、失礼ね。植物系はバカアインみたいに頑丈じゃないんだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（俺だったらヘシ折るつもりなのか・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っよっと♪"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【転送装置ゲートが青白く光り始めた！】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【・・・ゥゥゥブゥヴウウゥゥゥン・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッホラ、見て見て！　あたりでしょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へえ、何か紋様が少しだけ変わってるな。すげぇじゃねえか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：これで多分ファージル宮殿へ通じるゲートになった筈よ、行ってみましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっしゃ、それじゃ早速行くか！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【アインとラナは転送装置ゲートへと足を運んだ・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【その瞬間だった】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBlackOut);

            messageList.Add("　　　【・・・ッパキイィィィンン・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッゲ、なんだ今の音！！）"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【イィィィンン・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（な！？　何だ大丈夫なのかよ、この転送！？）"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【アインは突如、転送ゲートから放り出された！！】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGotoFirstPlace);

            messageList.Add("アイン：ッイデ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッツツツ・・・何かムチャクチャな転送だったな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、どこなんだよ、ココは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　その時、一つの風がアインの全体へ触れた。そんな気がした。　＞＞＞　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：気のせいか"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『アイン君。　　始めまして。　　だね。』　　"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その時、アインは自分が死体となるのを直感で感じた。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッな！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜＜＜　アインは【直感】し、即座に後ろへ振り向き、剣を突っ立てた。　＞＞＞　　"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：ようこそ、ファージル宮殿周辺のエスリミア草原区域へ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜＜＜　しかし、後ろには既に人の気配のみが存在するだけだった。　＞＞＞　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッバ・・・馬鹿な！！！）"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインは冷え切った汗を拭えないまま、死に対する激しい葛藤を続けている。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・追従しきれねえ・・・ウソだろ！？）"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：アイン君、すみません、そんなに警戒しなくても良いですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　一つの優しい風がまた、アインの全体へ触れた。　＞＞＞　　"); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：ボクの名はVerze Artie。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　声が先に届き ＞＞＞    "); eventList.Add(ActionEvent.None);

            messageList.Add("？？？：よろしくね、アイン君。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　＜＜＜　そしてようやく、アインに彼を目視する権利が与えられた。　＞＞＞　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で・・・伝説のFiveSeeker、ヴェルゼ・アーティ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：その呼び名は止めてください。単純にヴェルゼで構いませんよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ど、どこなんだココは！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：先ほども述べましたが、ファージル宮殿周辺のエスリミア草原区域です。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜＜＜　アインは少しずつ、死の直感が無くなっていくのを感じた。　＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：エスリミア草原区域・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ファージル南街道の少し右に行ったあの辺りか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・あれ！？　ラナはどこに行ったんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：彼女でしたら、あちらで休息の寝息を立てていますよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：転送装置からの脱出時、少し強い圧力が加わったみたいですね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だ、大丈夫なのかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ええ、命に問題はありません。軽い気絶をしただけの様です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハアアァァァ・・・・まいったぜ、ホント。焦らせるなよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まったく、ラナの奴はたまに変な所に首を突っ込もうとするから・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ところで、どうしてココへ来ようと考えたのですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、師匠が転送装置の前で、奇妙な枝を触ってるのをラナが目撃してだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それでココに来ちまったってわけだ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っと、あ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っす、スミマセン！　何か軽い口調で喋ってしまって！　申し訳ないです！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：いや、気にしないでください。アイン君の事はオル・ランディスから聞いていますから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ッハハハ・・・そうなんだ。いやいや、でも本当にすみません。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ところで、アイン君。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、なんでしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その時、アインは　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　再び、得たいの知れない死の直感を全体で感じ始めた！　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ックソ・・・何だこの感触は！？）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（この人から敵意は全くと言っていいほど感じ取る事ができない。これは確かだ。）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（なのに、何故か死の感触が強く迫ってくる・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（いつ俺の後ろを取ってくるか分からねえ・・・そんな恐怖だ。）"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：実は、オル・ランディスから依頼されている事があります。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ・・・あの師匠からですか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まさか、地獄のトレーニングとか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい、その通りです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ゲゲェ！　マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っとと、言葉がつい・・・すいません。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：本当に気にしなくて良いですよ、いつも通りの感覚で喋ってください。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：そうでないと、本当のアイン君を確認出来ませんからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああぁ・・・了解了解。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、お言葉に甘えて。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で、トレーニングってのはどういうのをやるつもりなんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：簡単ですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ボクとDUEL勝負と行きませんか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え！いきなりDUELですか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい、よろしくお願いしたいと思います。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ウ・・・ヴ～ン・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや。了解です！よろしくお願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ありがとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：それでは早速。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、っちょっとタンマ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい、なんでしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ッホ・・・ヴェルゼさんはちゃんと待ってくれるんだな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：この勝負、勝ち負けに応じて何か発生するのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：いいえ、特に何もありませんよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：純粋な腕試し、それだけです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうか、まあDUELは元々そういうもんだしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、オーケーオーケー！　いつでも良いぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：では、始めるとしましょう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：３"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：２"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：１"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.VERZE_ARTIE); eventList.Add(ActionEvent.HomeTownCallDuel);
        }

        public static void Message70013_2(ref List<string> messageList, ref List<ActionEvent> eventList, bool result)
        {
            if (result)
            {
                messageList.Add("アイン：っしゃ！このタイミングだ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：さて、どうでしょう。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　アインが止めの一撃を繰り出したその瞬間！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ッバシュ！！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っき！　消えただと！？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　アインは、異常なまでの死の直感と旋律を感じた！！　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ヤッ、ヤベェ！！！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：全ては一つとなりて"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　・・・声のみが響き渡り・・・　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：その一つは全てへと拡散する。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　彼の存在がアインの視界に入った瞬間　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：【叡技】Ladarynte・Caotic・Schema！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ファシュゥン・・・　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　刹那の静寂　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　一瞬だった　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：！！　ッしまっ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　声を発する間もなく　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ッドシュッ　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ッドシュ、ッドシュ！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグハ！　よ、避け！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ッドッドドドシュ！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグ、ッゲホ！！ウグッ！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　その攻撃数、方角、タイミング　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　無限連鎖　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ッドシュドシュドシュッドシュドシュドシュドドドシュ！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッグァ！！・・・ァ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：とどめです、ハアァァァァ！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜　ガシュッッ！　＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッガハ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【アインの死がより確実なものとなっていく】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：この辺で十分でしょうか。"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：セレスティアル・ノヴァ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　＜＜＜アインの傷が癒えてゆく・・・＞＞＞"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッハァ・・ッハァ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：すみません、どうやら勝負ありのようですね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：くそ・・・勝ったと思ったんだけどな・・・ッハァ・・・ッハァ・・・"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：ッグハ・・・ック・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("ヴェルゼ：すみません、どうやら勝負ありのようですね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：くそ・・・負けちまったか・・・"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：て言うか、早すぎる・・・全く追いつける気がしねえ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：いえ、すみませんが、それには理由があります。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ボクのアクセサリをお見せしましょう。コレです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：この光り方・・・ひょっとして！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：【天空の翼】　神々の遺産の一つです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ボケ師匠の極悪グローブと同じ類のヤツだよな！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：残念ながらそういう事になります。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：この翼を付けている場合、【技】のパラメタが異常なまでに増幅されます。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：そういう事ですから、公平さは欠けている事になります。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そういうのは関係ねえ、俺の負けだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：良い心構えです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、次までには絶対少しぐらいは追いつけるようになってやるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ハハハ、アイン君はきっと強くなりますよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：さて・・・それはさておき。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：少し以前から起きているのではないでしょうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：？　なんの話だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ラナさんは、すでに起きていますよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ウソ！？　なんでバレちゃってるのよ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おぉ、ラナ！　無事だったんだな！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあよかった良かった！　ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：一体いつ頃から起きていたんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアインが空中散歩してる所からよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイハイ、俺が倒された瞬間は見られたって事ね・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でもアインも結構何ていうか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？なんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ん～、何でも無い。気のせいね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんだ、またソレかよ？　ハッキリ教えてくれよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：大した事じゃないわよ、何でも無いわ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ラナさんは、アイン君と同じ転送装置で来たんですよね？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え！？え、えぇ・・・でもどうしてですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：アイン君とは少し離れた所で倒れていたので、少々気になっただけです。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：アイン君と行き先が同じのため、同時に入ったのではありませんか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：えぇ・・・アインとは同じタイミングで転送装置に入ったのは確かです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：同時に入るのは、あまり良くないのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：一般的には良いとはされていませんね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：転送装置は１人専用のため、２人同時の場合、到達地点予測は不可能です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヤベ・・・次から１人ずつ入るか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ゴメンなさい、私もちょっと浮かれてたかも知れないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：しかし、今回のようなケース自体、非常に稀だと思います。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：誰かが妙なトラップでも仕込まない限り、滅多なアクシデントはありません。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ほんの少しタイミングをズラす程度で大丈夫だと思いますよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：分かりました、気をつけます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さてと・・・この草原区域からファージル宮殿って・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：少し距離がありますね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：転送装置は私が少しメンテナンスをしておきますので"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：今回は一旦戻ってはいかがでしょうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん・・・まあ、そうか。　どうする、ラナ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、一旦戻りましょ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうか、じゃあ戻るとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヴェルゼさん、いろいろありがとうな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：いえ、こちらこそ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：それと気兼ねなく話すためにも、ヴェルゼと呼び捨てで構いませんよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何かFiveSeeker様相手に呼び捨ても気が引けるが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：始めに説明したはずです。オル・ランディスに報告しましょうか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあ、今後ともよろしくな！ヴェルゼ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ハハハ、アイン君は本当に面白いですね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、本当に危ない所をありがとな、またどこかで会おう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：私から先に行ってるわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜＜＜　ラナは転送装置で元の場所へと戻っていった　＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っそれじゃ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownMorning);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownButtonVisibleControl);

            messageList.Add("　　＜＜＜　アインは転送装置で元の場所へと帰ってきた　＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っふぅ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　【・・・ゥゥゥブゥヴウウゥゥゥン・・・】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜＜＜　転送装置が再び光り出した　＞＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：たびたび驚かせてすみません。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：そういえば、言い忘れていた事があります。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おお。えっと、なんでしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：いや、これはお願いなのですが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：この私を、アイン君のパーティに加えてもらえませんか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なっ！！　マジで！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：はい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、どうするのよ。アイン？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：何、ボケっとしてんのよ。答えなさいよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：や・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：やった！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こちらこそ、お願いします！！！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：どうやらOKのようですね、ありがとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ・アーティがパーティに加わりました。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：いやあ、でも本当に良いんですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：どういう意味でしょう？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺達とダンジョンに向かってもなんの得にもならないですよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ハハハハ、これはまた面白い事を言いますね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：アハハ・・・（面白いのか？）"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：得するために、ダンジョンへ向かっているワケではありませんよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：バカアインみたいに皆そーいう事考えてると思ったら大間違いよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイハイ・・・どうせ俺は生活資金源が目的ですよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：ハハハ、そういう意味ではボクも似たようなものですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ホラ見ろ。ヴェルゼだって同じようなもんだって言ってるじゃねえか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ホンット筋金入りバカね、アンタに合わせてるだけでしょうが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：目的は人によって違います。機会があればお話しますよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェルゼ：さて、それではダンジョンへ向いましょう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっしゃ、よろしく頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.VERZE_ARTIE_FULL); eventList.Add(ActionEvent.HomeTownAddNewCharacter);

            GroundOne.WE.AvailableFazilCastle = true;
        }   
    
        public static void Message70014(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationSinikia41 = true;
            GroundOne.WE.alreadyCommunicateCahlhanz = true;

            messageList.Add("アイン：カール爵の訓練場へ赴くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownGoToKahlhanz);

            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あのすいません？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・カール先生？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・居るのはわかるんだが・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生、出てくてきれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ちょっと話があるんだ、頼む。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『ッバシュ！！』（カールは一瞬でその場に姿を現した！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君か。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：頼む、一生のお願いだ。教えてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：言ってみるが良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：FiveSeekerに【強さ】についてだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：【強さ】への問いかけか。申してみよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：FiveSeeker達はどうしてそこまでの強さを手に入れたんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：頼む、教えてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：明確な解は無い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：全ては日々の積み重ね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それは俺もラナもやってるつもりだ。どこが違う？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：練習量はおそらく同程度。その質もおそらく同じであろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：論点を変えさせてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生は魔導部門の専門職だよな？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：いかにも。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあどうして、そこまでのスピードが出せるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：技に長けているヴェルゼとほぼ同クラスのスピードな気がするんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：アーティの事か。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：我自身が、奴ほどのスピードを引き出す事はない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、ヴェルゼほどじゃないにしてもですよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それにしたって、早すぎだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何か強さの秘密があるって事なんじゃないですか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：基礎的な鍛練は怠る事は決してない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：スピードを上げる魔法も多種多様。それに加え基本的な速度を引きだす訓練は日々の積み重ね。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：そのことは貴君とて重々承知のはず。違うかね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、まあそりゃそうだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の言う【強さ】という定義は、何を問おうとしておる？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：う～ん・・・そう言われるとな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：完全無欠な強さなど、この世には存在しない。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：日々の鍛練、そして、幅広い知識の習得、加え・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいやいや。ちょっとタイム！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そういう話は嫌と言うほど聞いてるんだ。そういう話じゃねえんだ。頼む！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：では、今一度申してみよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の知りたい【強さ】とは何か？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生、ファイア・ボール２連発を一度見せてくれないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：容易きこと、では行くぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　カールは、その場で体位をほんの少しだけ変化させ・・・　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッファイア・ボール"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　ッボ、ッボシュウゥゥン・・・　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：頼む！　もう一回だけ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフハハハハ、貴君は本当に面白い。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：良いだろう、では行くぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　カールは、左肩の部分をほんの少しだけ揺らし始め・・・　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッタ、タイム！そこのそれ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：っむ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：その時点で、詠唱は始まっているのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：まだだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っす、すまねえ・・・止めちまって。今度は止めねえから。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：良い、ではもう一度行くぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　カールは、右上の裾を微かに動作させ・・・　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・毎回モーションが微妙に違う・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッファイア・ボール"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（やっぱり・・・詠唱タイミングで既に炎の塊が2つ出ている。）"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　ッボ　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（その瞬間に２つ同時ってワケでもねえから・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("　＜＜　ッボシュウウウゥゥゥン・・・　＞＞"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ゲイル・ウィンドでもねえよな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ってことは、変なカラクリや小細工はねえな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（どうなってんだ・・・魔道士のくせに、このスピード・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：以上"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやぁ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：やっぱ、スゲェよ・・・信じられねえぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：カール先生、やっぱ強すぎだぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：貴君の問いに対する解答は見つけられたか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやっ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ぜんっぜん分かんねぇ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：アーッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：ッフハハ、おかしな奴だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何かほんの少しでも掴めるかと思ったんですが、俺もまだまだです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いつか絶対に、ボケ師匠やカール先生に追いついて見せます！"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：知識は全ての源、忘れるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はい、どうもありがとうございました！"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);
        }

        public static void Message70015(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_Communication_FC31 = true;
            GroundOne.WE.AlreadyCommunicateFazilCastle = true;

            messageList.Add("ラナ：あっ♪　ファージル宮殿に行ってみるの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そのつもりだ。なんだヤケに楽しそうだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：だって、憧れのエルミ様に会える可能性があるんだもの、楽しくなるわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なに・・・そんなものなのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そりゃそうよ。女性なら誰でも憧れるわ。バカアインが知らなすぎなだけよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやまあ・・・そういう事は俺には確かに分からん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ、イイわよ分からなくても。っささ、行きましょ♪　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ったく、めずらしく機嫌が良いな。まあ行くとするか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：”めずらしく”ないからね、いつも機嫌良いでしょ♪　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わ、分かった分かった。じゃ、行くとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastle);

            messageList.Add("アイン：っとぉ、ファージル宮殿到着っと。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アイン、あれを見て！　凄いわ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？どれどれ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おおぉわ！　んだありゃ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("『　　宮殿前の城門ゲートには、一般市民が行列を生成している　　　』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おいおい、こんな並んで、一体なにがあるんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ファージル宮殿名物のリアル相談行列じゃない、知らないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんだそれ、知るわけが無いだろう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：で、結局何で並んでるんだ？　教えてくれよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：え、ちょっとホント知らないわけ？ハアァァァ・・・まあ良いけど"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ファージル宮殿ではエルミ国王およびファラ王妃が民の声に直接耳を傾けるようにしているのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それで、この行列だってのか！？　一体どんだけ聞いてんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：朝方の7:00～12:00。そして12:30～18:00、最後に18:30～22:00までの三部構成ね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オイオイオイ、ちょっと待てよ！！　ほとんど休みねえじゃねえか！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：それだけ、民の事を念頭に置いているって事よね。正直コレは真似できないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：はあぁ・・・マジかよ・・・ただただ感心するばかりだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ってどうすんだよ？こんなの並んでいたらキリが無いぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：大丈夫よ。順番に関しては完全に予約制なの。ホラそこに記入リストがあるでしょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？何だそういうのがあるのか。早く言ってくれよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おしっと・・・記入したぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：この量だとそうね・・・明日の朝方に行くといいわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へぇ、よくそんな正確に分かるな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：当たり前じゃない。私結構昔の頃、ここに通うぐらい行ってたんだから♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ったく、相当気に入ってんだな、エルミ王の事・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ップ・・・ッププ"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あ～オカシイ、フフフ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな、何がおかしい？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：フフフ、なんでも無いわよ♪　っさ、今回はここまでね、一旦帰りましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんなオカシい内容だったか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあいい、確かにこれ以上やることもねえ。戻るとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add("ラナ：じゃあ、明日の朝だからね。忘れないでよねホント。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、了解了解！"); eventList.Add(ActionEvent.None);
        }

        public static void Message70016(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_Communication_FC32 = true;
            GroundOne.WE.AlreadyCommunicateFazilCastle = true;

            messageList.Add("ラナ：じゃあ、ファージル宮殿に行きましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、国王様、王妃様とご対面だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、転送するぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastle);

            messageList.Add("アイン：さて、着いたぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええと予約順はどれどれ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っお、本当だ。後少しで俺達の番だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っでしょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも、完全予約制ならここまでして並ぶ必要はねえんじゃねえのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：予約順序で自分の順番が来た時、該当の人が居なかった場合は、今並んでる人が割り込みで謁見する事が許可されてるのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほど、じゃあ重要な要件を抱え込んでる人は、並んでいる方が謁見までの時間が短縮される場合があるって事か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうね。あと、割り込みが１グループ入るから、その分だけ予約時間帯が大幅にズレる事もなくなるわけよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そこまで計算してのルールってわけか・・・ホントスゴすぎだな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：アイン・ウォーレンス！　アイン・ウォーレンスはこの場に居るか！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっと、呼ばれたみたいだ。行かなくちゃな！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：衛兵のオッサン！俺だオレ！　今そっちに行くぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：国王、王妃に対し、失礼の無きよう最善の心得を持って謁見に望まれたし！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、了解了解！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ行こうぜ、ラナ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ええ、楽しみよね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『　謁見の間にて・・・　』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へえ・・・意外と普通の部屋だな。もっと豪華絢爛なトコかと思ったが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：民との親近感を得るため、意図的にこの部屋の雰囲気を作ってるのよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ・・・そこまでするのか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っあ、ホラ来たわ！　っ静かに！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・（ドキドキ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：アイン・ウォーレンスとラナさんだね。よろしく。"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：エルモアの紅茶を煎じておいたわ。良ければどうぞ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あ、ありがとうございます♪　遠慮なく♪"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エルミ様は、今日も一段とカッコイイですね♪　元気でやってますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：ハハハ、ラナさんはいつもそんな調子だな、このとおり元気でやってるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ファラ様も、もーホント可愛すぎです。私いつもファラ様を参考にしてるんですよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：ウフフ、ありがとう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、要件はですね。ソコにボーっと突っ立っているバカアインが言いますので聞いてください♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・っな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんでそんな日常会話っぽいんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：民と会話する時は、この調子で喋る方が一番意見を引き出しやすいからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エルミ様は、一般日常会話に関しては上級クラスの資格を習得してるのよ。ホント凄いわよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そっ・・・そんなのがあるのか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ってか、やっぱりあれか。お硬いセリフも喋れる上であえて日常会話っぽくしてると・・・？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：まあ、そんな所だね。気にしないで良いよ本当に。"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：ウフフ、では要件をどうぞ、アインさん（＾＾）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、あぁ・・・じ、じゃあええと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッコラ、ちょっと！？　何どぎまぎしてるのよ、もう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッビシっと要件を言いなさいよね。スパスパっと。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おう。じゃあ、改めて。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：要件は簡単だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：討伐の依頼は入ってないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：あるよ。それがどうしたんだい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：出来ればそれを俺達に任せて欲しい。詳細を教えてくれないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：構わないよ。やってくれるんなら、大歓迎だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：報酬は何にしようか。直接的な収入でいいかい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、それが一番助かる。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：それでは、近衛兵に対して、アイン・ウォーレンスの討伐依頼申請受諾権利を認める事を伝えておこう。"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：エルミ。この件なら既に、謁見前に近衛兵サンディに伝えておきましたよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：おっと、そういえばそうだったな。助かるよファラ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな！！？　なんで分かってたんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、さすがよね。　だからエルミ様はカッコイイんじゃない♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っいやいやいや！　そういう意味で言うトコかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：謁見の間まで来るという事で、答えはほぼ限られてくる。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：予約キャンセル待ちの列にも並んでないようだし切羽詰まった内容ではないとすると"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：雑談か、生活資金源か、または雑多関連という事だし、だいたい目安は付くものなんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：アイン君は勇猛果敢な性質。　これ自体は前々から耳に届いているよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：となると。　答えは分かるよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いやいやいや・・・恐れ入ります・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：でもね。アインさんとラナさんに来ていただいて純粋に嬉しいんですよ、私もエルミも（＾＾）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあ・・・いやいやいや、もったいない言葉だ。ありがとうございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：エルミ様、また遊びに来てもいいですか♪"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：もちろんだよ。こんなところで良ければ、何度でも来てくれて構わないよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("王妃ファラ：お待ちしてますね（＾＾/）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あぁ・・・また来ます！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ホーラ、そこで浮かれないの！　ホンットにもう・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、本当にありがとうございました。失礼します。"); eventList.Add(ActionEvent.None);

            messageList.Add("国王エルミ：ああ、またね。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『　城門ゲート前にて・・・　』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ええと、近衛兵サンディさんは・・・と・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：アイン・ウォーレンス！　アイン・ウォーレンスはこの場に居るか！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おわっ！！ああっと、ハイハイ。今そっちに行くぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：アイン・ウォーレンスに通達する！】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：今この時より、アイン・ウォーレンスに討伐依頼申請の受理を行う権利を与える事とする！】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：討伐依頼のリストは、この私エガルト・サンディが所持している！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：リスト内容を見たければ、この私エガルト・サンディを尋ねるとよい！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっ、あ、ああぁ・・・了解了解！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：アイン・ウォーレンスよ！　申したい事があれば何なりと聞くがよい！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あぁ・・・じゃあとりあえず、一つだけ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと、次からはサンディって呼んでも良いか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おーい近衛兵って呼ぶのも何となく変だしな。構わないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：承知いたした！】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【近衛兵：それでは以降、私の事はサンディと呼ぶが良い！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おーし、サンキューサンキュー。じゃあよろしくな！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと、良い感じのトコ悪いんだけど、肝心の討伐依頼リストは見ておかないの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？ああ、それも大事なんだけどな。今回はひとまずココまでって事にさせてくれ。悪いな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふうん、そうなんだ。何か、バカアインって本当に変な時があるわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあまあ、いいじゃねえか。これもちょっとした礼儀の一つさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：・・・それって礼儀になってるわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、ありがとな、サンディ。次また会いに来るから、そん時に討伐リスト見せてくれ！"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【承知いたした！】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);

            messageList.Add("アイン：ふう、戻りっと。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うーん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんだ、どうかしたか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あれのどこが礼儀に当たるワケなのか、ちょっと教えてもらえないかしら？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、その件か。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんて言うんだろうな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：要件だけを言ったとする。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：その要件に相手が応えてくれたとする。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うんうん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っで、要件は満たされるワケだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうよね、それが目的なんだから。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それじゃつまんねえだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だから一旦要件は置いといて、次の機会にするのさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なんで、そーなるのよ？　意味がわからないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんでって言われてもな・・・何となくとしか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：う～ん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ダメ、全っ然わからないわ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハハハ・・・悪い悪い。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まあ、良いわ。あのサンディって人も堅苦しい感じだったし、これでちょうど良いのかも知れないわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあまあ、やり方なんて人それぞれさ。楽しく行こうぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：はああぁ・・・別にいいけど、次からは頼むわねホント。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：了解了解！　任せておけ！"); eventList.Add(ActionEvent.None);
        }

        public static void Message70017(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.AvailableOneDayItem = true;
            GroundOne.WE.AlreadyCommunicateFazilCastle = true;

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastle);

            messageList.Add("アイン：さて、着いたぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、アイン見て見てあっちの方で何か人だかりが出来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、本当だ。なんかあったのかな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ちょっと行ってみましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【皆の者に伝令事項がある！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、サンディだ。今日も元気にやってるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ッフフ、声が大きいわよね、サンディさん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、遠くからでもすげえ耳に残る感じだよな。"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【本日より！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【ファージル宮殿に赴いた際！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【宮殿正面ゲート前の横通りにて！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【ファージル国王から、全ての民に対して！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【感謝と敬意の念を込め！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【毎日１回ずつ、お楽しみ抽選券を発行する！！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、お楽しみ抽選券！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なんだか、面白そうね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【抽選で当たるアイテムは粗品から豪華賞品まで様々！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【是非ともご利用されよ！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ。そいつは嬉しい内容だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：実際にはどんな商品が当たるんだ？一覧リストとかあるのかな？"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【なお、全ての民に応じて、対象賞品は逐一更新され、かつ、その数は膨大！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【ゆえに、賞品リストを公開することは出来ない！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【なにとぞ、ご理解をいただきたい！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：全ての民に応じて、逐一って・・・すげえな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：どういう仕組みなのかしら、想像もつかないわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、やってみてからのお楽しみって所か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アイン、超豪華賞品が当たったら、ちゃんと私に頂戴よね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ゲッ・・・そ、それだけは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：当たるまで、毎日バシバシやって頂戴♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいやいや・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：決まり♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハ、ハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ファージル宮殿で「お楽しみ抽選券」を受け取る事が可能になりました！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);
        }

        public static void Message70018(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：おし、ファージル宮殿にでも行ってみるか。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastle);

            messageList.Add("サンディ：【よくぞ参られた！要件を申すがよい！】"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownFazilCastleMenu);
        }

        public static void Message70019(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.AlreadyGetOneDayItem)
            {
                messageList.Add("サンディ：【お楽しみ抽選券は正面ゲート向かって右側である！】");

                messageList.Add("アイン：サンキュー。じゃ行ってくるぜ。");

                messageList.Add("　・・・　しばらく歩いた後　・・・");

                if (!GroundOne.WE.Truth_FirstOneDayItem)
                {
                    messageList.Add("ラナ：あ、あれじゃないの？");

                    messageList.Add("アイン：お、本当だ！　どれどれ・・・");
                }
                else
                {
                    messageList.Add("アイン：よし、確かこの箱だったな。");
                }

                messageList.Add("　【　お楽しみ抽選券をお求めの方は、『発行』ボタンを押してください　】");

                messageList.Add("アイン：じゃあピっと・・・");

                messageList.Add("　【　ッガガガガ・・・　】");

                messageList.Add("　【　ありがとうございます。無事に発行されました　】");

                if (!GroundOne.WE.Truth_FirstOneDayItem)
                {
                    messageList.Add("アイン：お、おぉ！やったぜ！");
                }

                messageList.Add("　【　抽選券を持って、そのまま右へお進みください　】");

                if (!GroundOne.WE.Truth_FirstOneDayItem)
                {
                    messageList.Add("アイン：っしゃ、次だな！");

                    messageList.Add("ラナ：きっとあれよ。何人か並んでるわ。");

                    messageList.Add("アイン：よし、さっそく並んでみようぜ。");

                    messageList.Add("アイン：・・・なげえな・・・");

                    messageList.Add("ラナ：少し待つしかないわね。");

                    messageList.Add("アイン：ふう・・・");

                    messageList.Add("ラナ：ところで、どっちが券を使うの？");

                    messageList.Add("アイン：いや、それはどっちでも良いだろう。");

                    messageList.Add("ラナ：えー、何言ってんのよバカアイン？　大事なトコじゃないの。");

                    messageList.Add("アイン：いやいやいや、抽選なんだから、誰がやっても同じだろ？");

                    messageList.Add("ラナ：でも、強運の人がやると、立て続けに引き当てる人っているじゃない？");

                    messageList.Add("アイン：確かにたまに居るような、そういう奴は。");

                    messageList.Add("ラナ：でしょ？だから、私かアインのどっちかで、結果が変わるわけよ♪");

                    messageList.Add("アイン：マジか・・・関係ねえ気もするけどなあ・・・");

                    messageList.Add("ラナ：そういうワケだから、どっちが券を使うか決めてちょうだい♪");

                    messageList.Add("アイン：いやいやいや・・・そうだなあ・・・");

                    messageList.Add("アイン：・・・");

                    messageList.Add("アイン：ダメだ、わかんねえ！");

                    messageList.Add("アイン：券を使用する直前で決めよう！！！");

                    messageList.Add("ラナ：えっ、何よそれ。　ちゃんと決めてよね。");

                    messageList.Add("アイン：いやいや、何て言うんだ。決めようが無いぜ。");

                    messageList.Add("アイン：その時その時の直観に頼ろう。っな！？");

                    messageList.Add("ラナ：うーん、何か釈然としないけど・・・");

                    messageList.Add("アイン：おっ、前が開いたぜ！俺たちの番じゃないか？");

                    messageList.Add("ラナ：あ、本当ね。じゃあさっそくやってみましょ♪");

                    messageList.Add("　【　抽選券をシート挿入口に差し込んでください　】");

                    messageList.Add("アイン：よし、じゃあさっそくだが・・・");

                    messageList.Add("ラナ：どっちがやってみる？");
                }
                else
                {
                    messageList.Add("ラナ：ねえ、どっちがやってみる？");
                }

                messageList.Add("アイン：そうだなあ、ここは・・・");

                messageList.Add(""); eventList.Add(ActionEvent.HomeTownTicketChoice);
            }
            else
            {
                messageList.Add("サンディ：【お楽しみ抽選券は本日既に発行済となった！】");

                messageList.Add("アイン：そっか、じゃあまた今度だな。");

                messageList.Add("サンディ：【また、参られよ！】");
            }
        }

        public static void Message70019_2(ref List<string> messageList, ref List<ActionEvent> eventList, int ticketNumber)
        {

            if (ticketNumber == 1)
            {
                messageList.Add("アイン：おし、俺がやろう"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：頑張ってね♪"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：任せておけ！"); eventList.Add(ActionEvent.None);

                messageList.Add("　【　抽選券を認識いたしました。　しばらくお待ちください。　】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おし・・・来い！！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：ラナ、任せた。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：じゃあ、入れてみるわね。"); eventList.Add(ActionEvent.None);

                messageList.Add("　【　抽選券を認識いたしました。　しばらくお待ちください。　】"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：まあ、そんなに期待はしないけど・・・"); eventList.Add(ActionEvent.None);
            }

            GroundOne.StopDungeonMusic();

            messageList.Add("　【　結果を発表します　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　賞品は・・・　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　・・・　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　・・・　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　・・・　】"); eventList.Add(ActionEvent.None);

            string newItem = String.Empty;
            newItem = Method.GetNewItem(Method.NewItemCategory.Lottery, GroundOne.MC, null, 4);

            messageList.Add(Database.SOUND_LEVEL_UP); eventList.Add(ActionEvent.PlaySound);

            messageList.Add("＜ " + newItem + " ＞が当たりました！"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic13);

            messageList.Add("　【　賞品を転送いたしますので、ボックスから受け取ってください　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　ッガコン！！！　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　【　またご利用ください　】"); eventList.Add(ActionEvent.None);

            if (!GroundOne.WE.Truth_FirstOneDayItem)
            {
                messageList.Add("アイン：すげえ・・・このデッパリ穴から即出てくるのかよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：どういう仕掛けなのかしら。全アイテムが入ってるようにも思えないし・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、細かい仕掛けは気にしないでおこう。とにかく貰っておこうぜ！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：っしゃ、貰っておくぜ！"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(newItem + "を手に入れた。"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            if (!GroundOne.WE.Truth_FirstOneDayItem)
            {
                GroundOne.WE.Truth_FirstOneDayItem = true;
                messageList.Add("アイン：また今度やってみようぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ラナ：ええ、そうね。"); eventList.Add(ActionEvent.None);
            }

            GroundOne.WE.AlreadyGetOneDayItem = true;
            GroundOne.WE.AlreadyCommunicateFazilCastle = true;

            messageList.Add(newItem); eventList.Add(ActionEvent.HomeTownGetItemFullCheck);
        }

        public static void Message70020(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // after (モンスター討伐システムを作ってください)
            messageList.Add("アイン：よお、サンディ。良かったら討伐リストを見せてくれないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【すまぬが、討伐リストは未だ作られておらぬ！】"); eventList.Add(ActionEvent.None);

            messageList.Add("サンディ：【今しばらく待たれよ！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そっか・・・じゃあ、しょうがねえ、戻るとするか。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.AlreadyGetMonsterHunt = true;
        }

        public static void Message70021(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // after (「あいさつ」でサンディとの会話を増やすか？)
            // messageList.Add("サンディ：【また、参られよ！】"); eventList.Add(ActionEvent.None);
        }

        public static void Message79999(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：っとと・・・着いたみたいだな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あのすいません？"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：どうした。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：すいません、ちょっと講義でもお願いしたいのですが、"); eventList.Add(ActionEvent.None);

            messageList.Add("カール：今、教える事はない。　帰るがよい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハイ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownBackToTown);
        }
        #endregion

        #region "DUEL闘技場"
        
        public static void Message80001(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.AlreadyRest)
            {
                messageList.Add("アイン：まだ、登録申請中みたいだ。明日まで待つとするか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：受付さんよ。俺の登録申請はまだか？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【受付嬢：もうしばらくお待ちください。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そっか、じゃあまたな。"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message80002(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よお受付さん。今日はちょっと顔を出しただけなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、ッホント。特に用事はねえんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：邪魔したな、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っじゃ、また今度な！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：よぉ、ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・人違いだ。俺はザコアインじゃねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ほぉ、じゃあ誰なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：『いや、いやいやいや。』　か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇ。全っっっっっっ然成長してねえようだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや・・・っちょ、待っ、っちょ、ッタンマ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：はぁ？どうタンマなんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？え、いや、っか、かかってくるんじゃ"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：前祝したいってワケか。よおおおおぉぉぉぉぉし！良い心構えだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、違っ、っちょっちょちょ！タンマタンマタンマ！！"); eventList.Add(ActionEvent.None);

            GroundOne.StopDungeonMusic(); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：死んでこいやああぁぁぁぁぁ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【　ドドドスドスドスドスドドドドドスドスドスドスドス　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【　ドガガガガガガドガガガガドドガガガガガガガガ　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【　ボボボボボボグッシャアアァァァァ・・・　】"); eventList.Add(ActionEvent.None);

            messageList.Add("－－－　アイン気絶から、１時間が経過して　－－－"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("");  eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("アイン：ったく、ムチャクチャだぜ、ホント。。。いっつつつ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇ、ほんっとに成長してねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いきなり突っかかってくるのが悪いんだろうが。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：いきなり突っかかってねえだろぉが。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、何かそのこう・・・グォワアアアアって来るなっつうの。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：シラネエな、んなの。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇが弱すぎる。それだけだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、まだＤＵＥＬする場所でも無い所で突っかかってくるなつうの。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：いつだったら良いんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや・・・いやいや、そうじゃなくて。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：またか。その　『いや、いやいやいや。』"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、違う。そうじゃな・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇ、ＤＵＥＬに参戦するそうだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？ああ、参戦するさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：俺様からザコアインへ、一言送ってやろうと思ってな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな、何だよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜オル・ランディスは自分の足元へ指先を向け・・・＞"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：　　俺んトコまで、来てみせろ。　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ああ・・・当然さ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：当然行ってやるさ！見てろよな！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜オル・ランディスは少し微笑むと・・・＞"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ッフ、まぁガンバレや。ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("オル・ランディスはその場から立ち去っていった・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っくそう。結局、殴られ損かよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っあ、アイン。いたいた♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ。いつのまに来てたんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインが気絶してた場面ぐらいからよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺が気絶してるトコ見られてたって事かよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも本当、あんなに食らってるのに、意外とアイン元気よね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠は生命に危険を及ぼす急所攻撃はしねえタイプなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だから、大概が気絶、もしくは病院送りが関の山ってワケさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：病院送りになっちゃう人もいるのね。まあＤＵＥＬって言う以上しょうがないんだろうけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あ、そうそう。今日から参戦可能になったんでしょ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、そうだな。せっかくだし、今日から対戦してみる所なんだが"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ＤＵＥＬにおける詳細ルールは、見てみた？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、まだだな。ラナは知ってるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ううん、知らないわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ＤＵＥＬ参戦者のみに通達されるみたいだし。私は登録してないからね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、受付に聞いてみるとするか。おーい、受付さん。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：すまねえ、さっきは用事ねえって言ったんだが、ＤＵＥＬ詳細ルールっての見せてもらえるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：アイン様ですね、了解いたしました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　＜受付係員は何か書かれている紙切れを１枚持ってきた。＞"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：アイン様に関するＤＵＥＬ詳細ルールはこの通りです。ご参照ください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキュー！　この紙は、他の奴にも見せていいのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：構いません。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そっか、わざわざありがとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナ、もらってきたぞ。じゃあ、見てみるか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うん、何て書いてある？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どれどれ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownShowDuelRule);

            messageList.Add("アイン：なるほどな。大体分かったぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：一応装備やステータス値なんかは見せてもらえるわけね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうみたいだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そのかわり、こっちも一緒だから、お互い手の内が少し分かっちゃうわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうみたいだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ライフ０になった時点で勝敗が決まる。ってことは単純に相手を倒せば良いのよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうみたいだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：今日は何か一緒に食べてく？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうみたいだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『シャゴオォォン！！』（ラナのドラスティックキックがアインのミゾオチに炸裂）　　"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っつうう・・・分かった、分かったって。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っまあ、ちょっと１回だけ対戦させてくれ。その後で、飯食べにいこうぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ん、じゃあ待ってるわね。やるからには、ちゃんと勝ってよね♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、任せとけって！ッハッハッハ！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.MeetOlLandis = true;
        }
        
        public static void Message80003(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よお受付さん。今日もちょっと顔を出しただけだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っじゃな！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：よぉ。わざわざご苦労なこった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：きょ、今日は用事があって来た。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ほぉ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：解いたぜ、１階。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：やるじゃねえか。大したもんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：このまま、進むぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：２階制覇も楽勝さ！ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ッフ、まあがんばれや。ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：待て、今日はそういう話をしに来たんじゃねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠、お願いがあるんだ。聞いてくれるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：言ってみろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠もダンジョンへ一緒に来てくれないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：駄目だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・そうか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：少しは成長してるみてえじゃねえか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・え？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：何でもねえ。オラ！！とっとと２階制覇してきやがれ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え、っちょっオワワワワ！っちょちょちょ！！タンマタンマタンマ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【　ズドッドドドドドッドドォォドドドド　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【　メキボグッシャアァァァ・・・　】"); eventList.Add(ActionEvent.None);

            messageList.Add("---- アイン気絶から、１時間が経過して ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("ラナ：っで、断られちゃったわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうみたいだな。イッツツツ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でも良く誘う気になれたわね？自殺行為じゃない？？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でもさ。師匠が入れば、神がかり的にパワーアップするだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：うーん、まあランディスのお師匠さんが居てくれたら心強いわね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でもこんな理由じゃ仲間に入ってくれないよな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：そうよね・・・う～ん・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやいや、良いんだ。行こうぜ２階。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：良いの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、今はこのまま進むしかねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いずれ入ってくれるキッカケのようなモノを作ってみせるさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：っそうね。じゃ、２階制覇に向けて頑張りましょ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl21 = true;
        }

        public static void Message80004(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            if (!GroundOne.WE.Truth_CommunicationOl22Fail)
            {
                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

                messageList.Add("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：よお受付さん！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや～～、いやいやいや！ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：おい、受付相手に何いきなり笑ってやがる。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：師匠、教えてくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add("　　『ランディスは一瞬ラナへ視線を移し・・・』"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：何が聞きたい？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：このダンジョン。どうなってる？"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：どうもこうもねえ。単なるダンジョンだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：台座の試練、クリアしたぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：やるじゃねえか。ザコアインにしちゃ大したもんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：知の部屋もクリアまであと一歩だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：てめぇ、何の話をしにきた？"); eventList.Add(ActionEvent.None);

                messageList.Add("　　【【【　アインはさらに背筋に戦慄を感じた。　】】】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：た！　ッタイム！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：教えてくれ。師匠。"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：何を知りてぇんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                GroundOne.DecisionSequence = 0;
                GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                GroundOne.DecisionFirstMessage = "このダンジョン、どう解いていけば良い？";
                GroundOne.DecisionSecondMessage = "このダンジョン、どうすれば解けるんだ？";
                messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
            }
            else if (!GroundOne.WE.Truth_CommunicationOl22Progress1)
            {
                messageList.Add("アイン：師匠、教えてくれ！　頼むぜ！"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：何を知りてぇんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                GroundOne.DecisionSequence = 0;
                GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                GroundOne.DecisionFirstMessage = "このダンジョン、どう解いていけば良い？";
                GroundOne.DecisionSecondMessage = "このダンジョン、どうすれば解けるんだ？";
                messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
            }
            else if (!GroundOne.WE.Truth_CommunicationOl22Progress2)
            {
                messageList.Add("アイン：師匠・・・頼む、もう１回だけチャンスを！"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：っち・・・しょうがねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

                messageList.Add(""); eventList.Add(ActionEvent.PlayMusic16);

                GroundOne.DecisionSequence = 1;
                GroundOne.DecisionMainMessage = "　【　てめぇ、何で俺様の所に来る気になった？　】";
                GroundOne.DecisionFirstMessage = "ラナと相談した結果、師匠に聞こうって事で。";
                GroundOne.DecisionSecondMessage = "台座の回答をした後、不思議とそう感じたからだ。";
                messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
            }
            else
            {
                messageList.Add("ランディス：どぉした。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っまだまだ！　もう一回DUELだ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：何度でもかかってこいや、ザコアイン。"); eventList.Add(ActionEvent.None);

                messageList.Add(Database.DUEL_OL_LANDIS); eventList.Add(ActionEvent.HomeTownCallDuel);
            }
        }

        public static void Message80004_2(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：このダンジョン、どう解いていけば良い？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：どうもこうもねえ。自分なりに解いてみろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：台座の試練ってのがあったんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ほぉ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そこでは、『神々の詩』を回答することになっていた。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：回答は？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：出来たさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：で、それがどうした？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あれをどう捉えて良いのかが、わからねえ。"); eventList.Add(ActionEvent.None);

            GroundOne.DecisionSequence = 2;
            GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            GroundOne.DecisionFirstMessage = "師匠は『神々の詩』に関して、何か知らないか？";
            GroundOne.DecisionSecondMessage = "師匠の時も、ダンジョン攻略時、あんな台座が？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_3(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：師匠は『神々の詩』に関して、何か知らないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：知らねぇな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：頼む。何でも良いから知ってる事を"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ゴチャゴチャとうるせぇ。帰れ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 3;
        }

        public static void Message80004_4(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：師匠の時も、ダンジョン攻略時、あんな台座が？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どんな内容だったんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇには関係ねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：教えてくれても良いだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：言っても意味がねえ。"); eventList.Add(ActionEvent.None);

            GroundOne.DecisionSequence = 4;
            GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            GroundOne.DecisionFirstMessage = "意味がねえって・・・どういう意味だ？";
            GroundOne.DecisionSecondMessage = "意味がねえかどうかは、聞かなきゃ分からないだろ？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_5(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：意味がねえって・・・どういう意味だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：言葉通りだ。言った所で意味はねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺には当てはまらない・・・って事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：良く分かってるじゃねえか。"); eventList.Add(ActionEvent.None);

            GroundOne.DecisionSequence = 5;
            GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            GroundOne.DecisionFirstMessage = "つまり、台座は俺の【未来】に関係してるって事か？";
            GroundOne.DecisionSecondMessage = "つまり、台座は俺の【過去】に関係してるって事か？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_6(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：つまり、台座は俺の【未来】に関係してるって事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：だからてめぇはザコアインだって言ってんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：違うのかよ？頼むから、教えてくれよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ゴチャゴチャとうるせぇ。帰れ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 6;
        }

        public static void Message80004_7(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：つまり、台座は俺の【過去】に関係してるって事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：だとしたら、どうする。"); eventList.Add(ActionEvent.None);

            GroundOne.DecisionSequence = 7;
            GroundOne.DecisionMainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
            GroundOne.DecisionFirstMessage = "過去を基として、解を導き出せって事か？";
            GroundOne.DecisionSecondMessage = "過去を紐解いて、正解を見つけろって事か？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_8(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：過去を基として、解を導き出せって事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：さあな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうなんだよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：自分で考えろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ちったぁ、まともになってきたじゃねえか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・え？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：今度は、俺から幾つか問う。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：答えろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Progress1 = true;
            // 正解

            GroundOne.DecisionSequence = 1;
            GroundOne.DecisionMainMessage = "　【　てめぇ、何で俺様の所に来る気になった？　】";
            GroundOne.DecisionFirstMessage = "ラナと相談した結果、師匠に聞こうって事で。";
            GroundOne.DecisionSecondMessage = "台座の回答をした後、不思議とそう感じたからだ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_9(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：過去を紐解いて、正解を見つけろって事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：正解なんてもんはねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、過去が今回の台座の件とどう関係してるんだよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇ、何を聞きにきた？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っぐ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：話にならねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：帰れ、てめぇに教えることはねえ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 9;
        }

        public static void Message80004_10(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：意味がねえかどうかは、聞かなきゃ分からないだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ッチ・・・話にならねぇ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っま、待ってくれ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：意味がねぇもの、無理に聞いてどうなる？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っぐ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：帰れ、てめぇに教えることはねえ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 10;
        }
 
        public static void Message80004_11(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("アイン：このダンジョン、どうすれば解けるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：知らねぇな。自分で探せ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 11;
        }

        public static void Message80004_12(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 12;
            GroundOne.DecisionMainMessage = "　【　経緯を聞いてんじゃねえ。てめぇはどうなんだ？　】";
            GroundOne.DecisionFirstMessage = "台座はクリアした。だが、妙なひっかかりを覚えた。";
            GroundOne.DecisionSecondMessage = "どうって・・・特にどうってわけじゃないが・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_13(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 13;
            GroundOne.DecisionMainMessage = "　【　何が引っかかったのか、把握はしてんのか？　】";
            GroundOne.DecisionFirstMessage = "把握はできてねえが、それなりの違和感は・・・";
            GroundOne.DecisionSecondMessage = "いや・・・それが何なのかはわからねえ・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_14(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 14;
            GroundOne.DecisionMainMessage = "　【　はっきりしねえな。ドッチなんだ？　】";
            GroundOne.DecisionFirstMessage = "す、すまねえ・・・";
            GroundOne.DecisionSecondMessage = "台座は解けた。でもまだ奥底が見えないんだ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_15(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 15;
            GroundOne.DecisionMainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
            GroundOne.DecisionFirstMessage = "っそ、それは・・・";
            GroundOne.DecisionSecondMessage = "ダンジョンで稼がなくちゃならねえ。それだけさ";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_16(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 16;
        }

        public static void Message80004_17(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 17;
        }

        public static void Message80004_18(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 18;
            GroundOne.DecisionMainMessage = "　【　奥底ってのは何を指して言ってる？　】";
            GroundOne.DecisionFirstMessage = "奥底ってのは、つまり・・・";
            GroundOne.DecisionSecondMessage = "そんなの、俺にだってわからねえよ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_19(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 19;
        }

        public static void Message80004_20(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 20;
        }

        public static void Message80004_21(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 21;
            GroundOne.DecisionMainMessage = "　【　過去と台座の関係ぐらいは分かってんだろうな？　】";
            GroundOne.DecisionFirstMessage = "もちろん、わかってるさ！";
            GroundOne.DecisionSecondMessage = "ッグ・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_22(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 22;
            GroundOne.DecisionMainMessage = "　【　じゃあ、言ってみろ。　】";
            GroundOne.DecisionFirstMessage = "そ、それは・・・";
            GroundOne.DecisionSecondMessage = "過去の出来事が台座での設問になる。そうだろ？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_23(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 23;
        }

        public static void Message80004_24(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 24;
        }

        public static void Message80004_25(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 25;
            GroundOne.DecisionMainMessage = "　【　ダンジョンの攻略方法、把握度合いはどぉなんだ？　】";
            GroundOne.DecisionFirstMessage = "それなりに、探索してるし分かってるつもりだぜ。";
            GroundOne.DecisionSecondMessage = "す・・・少しぐらいなら・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_26(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 26;
        }

        public static void Message80004_27(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 27;
        }

        public static void Message80004_28(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 28;
            GroundOne.DecisionMainMessage = "　【　ダンジョンの攻略具合はどぉなんだ？　】";
            GroundOne.DecisionFirstMessage = "順調だ。滞りなく進んでる。";
            GroundOne.DecisionSecondMessage = "順調ってワケじゃねえ・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_29(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 29;
            GroundOne.DecisionMainMessage = "　【　攻略の意味は分かってんだろうな？　】";
            GroundOne.DecisionFirstMessage = "攻略の・・・意味？";
            GroundOne.DecisionSecondMessage = "最下層へ進めるための謎を解く。そうだろ？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_30(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 30;
            GroundOne.DecisionMainMessage = "　【　ダンジョンの攻略度合いさ。決まってるだろ？　】";
            GroundOne.DecisionFirstMessage = "ああ、これなら楽勝だぜ。";
            GroundOne.DecisionSecondMessage = "台座をクリアした所だが・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_31(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 31;
        }

        public static void Message80004_32(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 32;
        }

        public static void Message80004_33(ref List<string> messageList, ref List<ActionEvent> eventList)
        {        
            // Fail
            GroundOne.DecisionSequence = 33;
            GroundOne.DecisionMainMessage = "　【　最下層？　謎？　何言ってんだテメェは。　】";
            GroundOne.DecisionFirstMessage = "えっ？ち、違うのかよ？";
            GroundOne.DecisionSecondMessage = "え？　っと・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_34(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 34;
        }

        public static void Message80004_35(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 35;
        }

        public static void Message80004_36(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 36;
            GroundOne.DecisionMainMessage = "　【　台座のどこが気になった？言ってみろ。　】";
            GroundOne.DecisionFirstMessage = "詩の内容は過去に聞いた事がある。しかし・・・";
            GroundOne.DecisionSecondMessage = "看板の前に、突然出てきたって事ぐらいかな。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_37(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 37;
            GroundOne.DecisionMainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
            GroundOne.DecisionFirstMessage = "あ、ああ・・・";
            GroundOne.DecisionSecondMessage = "過去との決別をするために！って事だろ？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_38(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 38;
        }

        public static void Message80004_39(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 39;
        }

        public static void Message80004_40(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 40;
            GroundOne.DecisionMainMessage = "　【　それがどぉした？　】";
            GroundOne.DecisionFirstMessage = "あ、っいや・・・";
            GroundOne.DecisionSecondMessage = "・・・　・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_41(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 41;
        }

        public static void Message80004_42(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 42;
        }

        public static void Message80004_43(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            //Sucess
            GroundOne.DecisionSequence = 43;
            GroundOne.DecisionMainMessage = "　【　台座をクリアした意味はわかってるか？　】";
            GroundOne.DecisionFirstMessage = "いや、まだ断片的な事しか、わからねえ。";
            GroundOne.DecisionSecondMessage = "ああ、当然分かっているさ！";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_44(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Success
            GroundOne.DecisionSequence = 44;
            GroundOne.DecisionMainMessage = "　【　今後どうやって進めてくつもりだ？　】";
            GroundOne.DecisionFirstMessage = "次への階段を探し出し、最下層を目指すまでさ。";
            GroundOne.DecisionSecondMessage = "ダンジョン内をくまなく探索しながら進めるさ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_45(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 45;
            GroundOne.DecisionMainMessage = "　【　最下層まで行けたとして、どうするつもりだ？　】";
            GroundOne.DecisionFirstMessage = "どうって・・・もっと強くなってやるさ。";
            GroundOne.DecisionSecondMessage = "どうって・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_46(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 46;
            GroundOne.DecisionMainMessage = "　【　強くなった後はどうするかを聞いてんだ。　】";
            GroundOne.DecisionFirstMessage = "その後は・・・その・・・";
            GroundOne.DecisionSecondMessage = "強ければ良いんだろ？それが師匠の教えじゃねえか。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_47(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 47;
        }

        public static void Message80004_48(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 48;
        }

        public static void Message80004_49(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 49;
            GroundOne.DecisionMainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
            GroundOne.DecisionFirstMessage = "っそ、それは・・・";
            GroundOne.DecisionSecondMessage = "ダンジョン制覇そのものが目的さ、それ以上の意味は無い。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_50(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 50;
        }

        public static void Message80004_51(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 51;
        }

        public static void Message80004_52(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Success
            GroundOne.DecisionSequence = 52;
            GroundOne.DecisionMainMessage = "　【　探索して、ダンジョンの仕掛けは把握したのか？　】";
            GroundOne.DecisionFirstMessage = "台座なら、ちゃんと見つけたぜ。クリアもした。";
            GroundOne.DecisionSecondMessage = "台座の一部ぐらいしか・・・全体像はまだ何とも・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_53(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 53;
            GroundOne.DecisionMainMessage = "　【　てめぇのお望みってのは、台座だったのかよ？　】";
            GroundOne.DecisionFirstMessage = "メインの仕掛けを解いた。探索としては成功だろ？";
            GroundOne.DecisionSecondMessage = "いや・・・そういうわけじゃ・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_54(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 54;
        }

        public static void Message80004_55(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 55;
        }

        public static void Message80004_56(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Success
            GroundOne.DecisionSequence = 56;
            GroundOne.DecisionMainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
            GroundOne.DecisionFirstMessage = "腕を試したかった。それだけだ。";
            GroundOne.DecisionSecondMessage = "・・・　・・・　";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_57(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 57;
        }

        public static void Message80004_58(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            // Success
            messageList.Add("ランディス：・・・ほぉ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.Truth_CommunicationOl22Progress2 = true;

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("アイン：師匠、おりいって頼みがある。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：何だ、言ってみろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠、このダンジョン一緒に来てくれ。頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ちっと腕見せてみろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：３"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：２"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おわっ！マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：１"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ック・・・来い！！"); eventList.Add(ActionEvent.None);

            messageList.Add(Database.DUEL_OL_LANDIS); eventList.Add(ActionEvent.HomeTownCallDuel);
            GroundOne.DecisionSequence = 58;
        }

        public static void Message80004_59(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 59;
            GroundOne.DecisionMainMessage = "　【　じゃあ、言ってみろ。　】";
            GroundOne.DecisionFirstMessage = "台座は過去に関連してる。過去を基に解を導き出せばいい。";
            GroundOne.DecisionSecondMessage = "あの詩がこのダンジョンにおける最大の鍵だ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_60(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 60;
            GroundOne.DecisionMainMessage = "　【　どぉ導き出されるってんだ？言ってみろ。　】";
            GroundOne.DecisionFirstMessage = "過去の出来事を思い出しつつ、ダンジョン正解ルートを導き出す。";
            GroundOne.DecisionSecondMessage = "ど、どうって・・・";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_61(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 61;
            GroundOne.DecisionMainMessage = "　【　正解だと？　てめぇ、俺から一体何を学んだ？　】";
            GroundOne.DecisionFirstMessage = "っあ！い、いやいやいや！！";
            GroundOne.DecisionSecondMessage = "隠さないでくれよ。正解ルートあるんだろ？";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_62(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 62;
        }

        public static void Message80004_63(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 63;
        }

        public static void Message80004_64(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 64;
            GroundOne.DecisionMainMessage = "　【　答えるか、答えないか。ハッキリしろ。　】";
            GroundOne.DecisionFirstMessage = "す、すまねぇ・・・";
            GroundOne.DecisionSecondMessage = "未来へとつながるキーワードを探せば良いんだ！";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_65(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 65;
        }

        public static void Message80004_66(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 66;
        }

        public static void Message80004_67(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 67;
            GroundOne.DecisionMainMessage = "　【　最大の鍵。どこで使うんだ？　】";
            GroundOne.DecisionFirstMessage = "ど、どこって・・・";
            GroundOne.DecisionSecondMessage = "最下層で使うんだろ、こういうのは。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_68(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 68;
            GroundOne.DecisionMainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
            GroundOne.DecisionFirstMessage = "最下層だ！";
            GroundOne.DecisionSecondMessage = "一番最初だ！";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_69(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 60;
        }

        public static void Message80004_70(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：駄目だ。話にならねぇ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 70;
        }

        public static void Message80004_71(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            // Fail
            GroundOne.DecisionSequence = 71;
            GroundOne.DecisionMainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
            GroundOne.DecisionFirstMessage = "師匠の【炎神グローブ】みたいなヤツを俺も欲しいからさ。";
            GroundOne.DecisionSecondMessage = "もちろん、最下層到達で師匠に並ぶためさ。";
            messageList.Add(""); eventList.Add(ActionEvent.HomeTownCallDecision);
        }

        public static void Message80004_72(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 72;
        }

        public static void Message80004_73(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.Truth_CommunicationOl22Fail = true;
            GroundOne.DecisionSequence = 73;
        }
                
        public static void Message80004_74(ref List<string> messageList, ref List<ActionEvent> eventList, bool result)
        {
            if (result)
            {
                // 勝った場合、次の会話へ
                GroundOne.WE2.WinOnceOlLandis = true;
            }
            else
            {
                if (GroundOne.WE.Truth_CommunicationOl22DuelFailCount >= 3)
                {
                    // 負けすぎなので、そのまま通す。ただし、WinOnceOlLandisはつけない。
                }
                else
                {
                    // 負けた場合、強制リトライ
                    messageList.Add("ランディス：帰れ、てめぇに教えることはねえ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッグ・・・"); eventList.Add(ActionEvent.None);

                    GroundOne.WE.Truth_CommunicationOl22Fail = true;
                    GroundOne.WE.Truth_CommunicationOl22DuelFail = true;
                    GroundOne.WE.Truth_CommunicationOl22DuelFailCount++;
                    GroundOne.DecisionSequence = 74;
                    return;
                }
            }

            GroundOne.StopDungeonMusic();
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            messageList.Add("　　【受付嬢：そちらの方々！！今すぐ対戦を中止してください！！　】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッウワ・・・ヤベ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：闘技場内での勝手な対戦は、ルール厳禁となっております。　】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ッチ、分かった分かったって、嬢ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：今から、名前を読み上げます。　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：オル・ランディス様　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：アイン・ウォーレンス様　】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：読み上げられた者は、罰としてDUEL戦歴に１敗が加えられます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ！！マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：くだらんルールだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：ただし、アイン・ウォーレンス様はDUEL参加より" + GroundOne.WE.GameDay.ToString() + "日以内のため、特例除外とします。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：助かったぜ・・・ッホ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：合わせて、オル・ランディス様には累積罰として更に２敗が加えられます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：勝手に付けとけ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：なお、今後も続けて行った場合、DUEL戦歴に累積的な敗北数が加算されます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：くれぐれも闘技場内での勝手なDUELはしないよう、お願いいたします。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、悪かったな、受付さん。次からは気をつけるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：こいつらに気を使う必要はねぇ。ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何でだよ。運営側の受付さんだろ？別に良いじゃねえか。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：てめぇのそういうトコ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：受付さんだって人間だ。良いだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：つくづく甘ちゃんだなテメェは・・・好きにしろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っふぅ・・・マジで疲れたぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：師匠とやるといつも全力だ・・・もう動けねえ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ダンジョン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っえ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：行ってやっても良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おおおお！！！　マジで！？　やった！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：条件がある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、あぁ。教えてくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ラナを外せ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　えっ　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：冗談だ。真に受けんなボケ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな・・・何だよ。つい考えちまったじゃねえか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：ラナと少し距離を置け。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや、何言ってるんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：距離感とかいう話にならない程度の距離でしか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：聞けっつってんだ、ボケ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：このダンジョン、解きてぇんだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、あぁ当然！　目指すは最下層到達だ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：だったら距離を置け。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それのどこがダンジョン攻略に・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：以上だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：パーティに入った以上、死ぬまで鍛えてやる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：覚悟しとけや、ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サンキューな！　師匠！！　"); eventList.Add(ActionEvent.None);

            messageList.Add("【オル・ランディスがパーティに加わりました！】"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            GroundOne.WE.AvailableThirdCharacter = true;
            GroundOne.WE.Truth_CommunicationOl22 = true;

            // 「コメント」初回設計で後編３人目をヴェルゼアーティでセーブしてしまっているため、
            // ここで再設定しなければならなくなった。
            GroundOne.TC.FullName = "オル・ランディス";
            GroundOne.TC.FirstName = "ランディス";
            GroundOne.TC.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
            GroundOne.TC.Agility = Database.OL_LANDIS_FIRST_AGILITY;
            GroundOne.TC.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
            GroundOne.TC.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
            GroundOne.TC.Mind = Database.OL_LANDIS_FIRST_MIND;
            GroundOne.TC.Level = 35;
            GroundOne.TC.Exp = 0;
            GroundOne.TC.BaseLife = 2080;
            GroundOne.TC.CurrentLife = GroundOne.TC.MaxLife;
            GroundOne.TC.BaseSkillPoint = 100;
            GroundOne.TC.CurrentSkillPoint = 100;
            GroundOne.TC.BaseMana = 1290;
            GroundOne.TC.CurrentMana = GroundOne.TC.MaxMana;
            GroundOne.TC.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
            GroundOne.TC.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
            GroundOne.TC.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
            GroundOne.TC.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
            GroundOne.TC.BattleActionCommandList[0] = Database.ATTACK_EN;
            GroundOne.TC.BattleActionCommandList[1] = Database.DEFENSE_EN;
            GroundOne.TC.BattleActionCommandList[2] = Database.STRAIGHT_SMASH;
            GroundOne.TC.BattleActionCommandList[3] = Database.VOLCANIC_WAVE;
            GroundOne.TC.BattleActionCommandList[4] = Database.LIFE_TAP;
            GroundOne.TC.BattleActionCommandList[5] = Database.SEVENTH_MAGIC;
            GroundOne.TC.BattleActionCommandList[6] = Database.ONE_IMMUNITY;

            GroundOne.TC.AvailableMana = true;
            GroundOne.TC.AvailableSkill = true;
            GroundOne.TC.StraightSmash = true;
            GroundOne.TC.FireBall = true;
            GroundOne.TC.DarkBlast = true;
            GroundOne.TC.DoubleSlash = true;
            GroundOne.TC.ShadowPact = true;
            GroundOne.TC.FlameAura = true;
            GroundOne.TC.StanceOfStanding = true;
            GroundOne.TC.DispelMagic = true;
            GroundOne.TC.LifeTap = true;
            GroundOne.TC.HeatBoost = true;
            GroundOne.TC.Negate = true;
            GroundOne.TC.BlackContract = true;
            GroundOne.TC.InnerInspiration = true;
            GroundOne.TC.RiseOfImage = true;
            GroundOne.TC.Deflection = true;
            GroundOne.TC.FlameStrike = true;
            GroundOne.TC.Tranquility = true;
            GroundOne.TC.VoidExtraction = true;
            GroundOne.TC.BlackFire = true;
            GroundOne.TC.Immolate = true;
            GroundOne.TC.DarkenField = true;
            GroundOne.TC.DevouringPlague = true;
            GroundOne.TC.VolcanicWave = true;
            GroundOne.TC.OneImmunity = true;
            GroundOne.TC.CircleSlash = true;
            GroundOne.TC.OuterInspiration = true;
            GroundOne.TC.SmoothingMove = true;
            GroundOne.TC.WordOfMalice = true;
            GroundOne.TC.EnrageBlast = true;
            GroundOne.TC.SwiftStep = true;
            GroundOne.TC.Recover = true;
            GroundOne.TC.SurpriseAttack = true;
            GroundOne.TC.SeventhMagic = true;

            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            GroundOne.DecisionSequence = 75;
        }

        public static void Message80005(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationOl31 = true;

            messageList.Add("アイン：師匠、いるか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：なんだ、ザコアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：３階に向けて、今から少し作戦タイムを・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：そぉだ、言い忘れてた事がある。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何だよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：オレは抜ける。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：以上だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・えええぇぇぇ！？何でだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：急な用事だ。　テメェのお守りはココまでだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：な、何だよ突然！？　用事って何だよ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：っせぇ、黙れ。てめぇには関係ねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：くっそおぉぉ・・・マジかよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：荷物の件だが、【ハンナゆったり宿屋】に預けておいた。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：好きな時に荷物整備しとけ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：以上だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("オル・ランディスはその場から立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            Method.RemoveParty(GroundOne.TC);

            messageList.Add("アイン：・・・くそぉ、なんの前触れも無しかよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：でもランディスさんは、アインが来るのを一応待っていたワケよね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？んん・・・まあ確かにそうなのかも。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：フフフ、何かオカシイわね。アイン結構気に入られてるんじゃないの♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ウソつけ、あんなのテキトー快楽主義者だろ。。。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：で、ダンジョンはどうするわけ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：１人減る事で、ダンジョンのモンスター難易度も調整されるだろ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いまさら止めてもどうかなるわけじゃないしな。続行だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：アインが続行なら、私も引き続きついて行くわよ♪"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうしてくれ。助かるぜ！"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.Truth_CommunicationLana31)
            {
                messageList.Add("ラナ：ところで、転送装置からファージル宮殿に行ってみない？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、そうだったな！　じゃ、行くとするか！"); eventList.Add(ActionEvent.None);
            }
        }

        public static void Message80006(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            GroundOne.WE.Truth_CommunicationOl41 = true;

            string detectSword = PracticeSwordLevel(GroundOne.MC);

            messageList.Add("アイン：ふぅ・・・対戦相手の戦歴チェックっと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・師匠、今頃何してるかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：あっ、アイン。こんな所に居たのね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、ラナか。どうしたんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：なんかね、闘技場の受付の人が、アインを探してたみたいよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そうか。じゃあちょっと受付まで行ってくる。サンキュー。"); eventList.Add(ActionEvent.None);

            messageList.Add("---- アインは受付まで出向いた ----"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よう、受付さん。俺に何か用でもあったのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：はい、新着の伝言が入っております。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：伝言！？そんな制度があるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：はい、あります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、まあいいか。。。で、どんな内容なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：こちらに、識別ID<3-297761 Ol_Landis>の音声データが入ったチップがあります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：へえ、小っさいチップだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、識別IDが・・・ッグ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：音声チップは手前から向かいにあります、あちらのデンデン君にセットしてお使いください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：デンデン君？？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：詳しくは端末にある操作説明を読んでご利用ください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：(しかし、こんなものがあるとは・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：確かコッチだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おし、これだな。えーとどれどれ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン（音読）：「チップを装置横にある差し込み口に挿入し、PUSHスタートを押してください。」"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：これか・・・よし。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　その瞬間。　アインの脳内に直接オル・ランディスの音声が伝わってきた！！　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《よぉ、ザコアイン》"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うお！！びっくりするな。直接聞こえるのかよ、これ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《これを聞いてるって事は、ひとまず、四階へと進め始めたって事だな》"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、まあ聞いてみるか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《いいか、よく聞け》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《今から俺が言う事は、全て事実だ》"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っえ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《てめぇが受け止めるかどうかに関しては、てめぇで決めろ》"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　アインはほんの少しだけ呼吸が止まった　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《今からてめぇに起こりうる事象を全て伝える》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《まず、てめぇの横にいるアーティだが》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《四階開始と同時に姿を消す》"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　それは　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《次に四階の内容だが》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《あっさりと道筋通り進めるはずだ》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《迷うポイントはほとんどねえ、手筋どおりだ》"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　心のどこか奥底で　】】】"); eventList.Add(ActionEvent.None);

            if (detectSword == Database.LEGENDARY_FELTUS)
            {
                messageList.Add("ランディス：《だがてめぇは、自分の知らない間に》"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：《今てめぇが手にしているその神剣フェルトゥーシュを、いつの間にか見失う》"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ランディス：《だがてめぇは、そのまま進み続け》"); eventList.Add(ActionEvent.None);

                messageList.Add("ランディス：《神剣フェルトゥーシュを永遠に手にする機会を失う》"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【【【　既に認識していたかの様な冷たい感触　】】】"); eventList.Add(ActionEvent.None);

            if (detectSword == Database.LEGENDARY_FELTUS)
            {
                messageList.Add("ランディス：《神剣フェルトゥーシュを見失った状態で、ダンジョンを進み続け》"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("ランディス：《神剣フェルトゥーシュを入手出来ていないままの状態で、ダンジョンを進み続け》"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("ランディス：《四階、最後の試練【神の選択肢】に遭遇》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《てめぇは、そこで・・・》"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　心の隅々にまで、真っ黒なインクが染み込むように　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《誤りを選択する》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《【神の選択肢】ってのは、そういうもんだ》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《その後てめぇは》"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【【【　絶望という色彩が体中を覆った　】】】"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《絶望する》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《最下層への到達は、終着点なんかじぇねえ》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《終わりの始まり》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《絶対に、最下層への道を見誤んじゃねえぞ》"); eventList.Add(ActionEvent.None);

            messageList.Add("ランディス：《いいか、わかったな》"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（なんだ・・・これ・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（師匠が、何故これから先に起こりうる事を知ってるんだ）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（い、いやいやいや。そもそも未来なんて分かるはずがねえんだが）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（でもあの言い方は・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（真に迫る言い方、つまりブラフや威嚇じゃねえ）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（本当の事をキッチリ言う時に使う声だ）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（だとしたら・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ねえ、どうしたのよ？さっきから止まってるみたいだけど"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どぅわあぁ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラナか。何だ脅かすなよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：普通に声をかけただけなのに、過剰にビビってんのはそっちじゃない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そ、そうか・・・ッハハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：デンデン君からは、良い情報は得られたの？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、まあな。それなりに。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：ふうん、まあ詮索はしないけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おし、じゃあ頑張って行くとするか！"); eventList.Add(ActionEvent.None);

            messageList.Add("ラナ：まったく相変わらずゲンキンよね。闘技場で忘れ物とかしないでよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、分かってるって。っさ、行くぜ！"); eventList.Add(ActionEvent.None);
        }

        public static void Message89998(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist)
        {
            messageList.Add("　　【受付嬢：３】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：２】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：１】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：０】"); eventList.Add(ActionEvent.None);

            messageList.Add(OpponentDuelist); eventList.Add(ActionEvent.HomeTownCallDuel);
        }
     
        public static void Message89998_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, bool win, bool fromGoDungeon)
        {
            if (win)
            {
                GroundOne.WE2.DuelWin += 1;

                messageList.Add("　　【受付嬢：勝者 アイン・ウォーレンス！】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っしゃ！俺の勝ちだぜ！！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                GroundOne.WE2.DuelLose += 1;
                messageList.Add("　　【受付嬢：勝者 " + OpponentDuelist + "！】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ックソ・・・負けちまったか・・・"); eventList.Add(ActionEvent.None);
            }

            #region "ザルゲのDUEL戦闘後のセリフ"
            if (OpponentDuelist == Database.DUEL_SCOTY_ZALGE)
            {
                if (win)
                {
                    GroundOne.WE.DuelWinZalge = true;

                    messageList.Add("ザルゲ：ッゲホオオォ・・・ッグ・・・こんな雑魚に俺様が・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：雑魚で悪かったな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：ックソォォォ・・・っおい、キーナ嬢！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：今から言う話をよぉぉぉく聞いておけよ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：３０連勝がストップしたからと言って・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（アインは突然大声で・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ＤＵＥＬ大会運営の会長さんよ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：なあ、どっかに居るんだろ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（・・・観客が少しだけ、どよめき始めた・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：受付嬢って確か、ＤＵＥＬ運営サポートが役割なんだろ？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：このままじゃ、ＤＵＥＬの運営全体に支障をきたすんじゃないのか？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：そうなる前にさ。サポートに徹する事が出来る環境にしてやってくれ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：頼んだぜ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（・・・ざわ・・・ザワザワザワ・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：お、おい、何くだらねえこと提言してんだよ！？あぁぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：んな会長なんてココら辺にいるわけねえだろうが！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：俺たちが認識できないほど気配を隠してるだけだ。必ずどこかで見てるはずさ。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：って事は何か？俺に鉄槌でも下るってかぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：・・・・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：って何も起きねぇじゃねえか！！　ゲハハハハハハ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：さぁて、先程の続きだ、良いかぁキーナ嬢ちゃん！？実はなあ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（突然、ザルゲに閃光が放たれた！！！）"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（ッピッッシイイイイイィィィィィ！！！）"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（・・・　・・・　・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：き、消えた・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッサ・・・サンキューサンキュー！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（しばらくの間、闘技場では、アインへ賞賛の拍手が送られた）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（キーナ嬢さん、これから上手く行くと良いな・・・)"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　【受付嬢：・・・（ッゴホン）】"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("ザルゲ：アイン・ウォーレンス様、本日はありがっとォございました。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：これを持ちましてぇ・・・ええ僭越ながらキーナ嬢との婚約を行うが我が運命。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：その時はどうか婚儀の場へ・・・ック、ックククハハハ"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：お越しくださいませってかぁ！？　ゲハハハハハハ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：っく・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：おい、キーナ嬢！　分かってんだろうなぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：逃げんじゃねぇぞ！？　逃げたらご両親様がどうなるか分かってんだろうなぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：おい、もうＤＵＥＬは終了してるんだ。関係の無い話はするな。"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：はあぁぁ！？負けた奴が何をほざいんてんだぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッグ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：負け犬はとっとと退場するんだな！ホレ、そこの受付嬢さん、転送を早く。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（・・・受付嬢は少し小刻みに震えている・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：おっとっと、ここの受付嬢はルール通りに動けずに立ち往生ってかぁ！？"); eventList.Add(ActionEvent.None);

                    messageList.Add("ザルゲ：ゲハハハハハハ！！"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：ッチ・・・早く転送させて終わるんだ、キーナさん。"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　【受付嬢：それではＤＵＥＬは終了となります。両者とも転送させていただきます。】"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　　（ッバシュウウン・・・）"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（ここは・・・そうか、終わったのか）"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　【受付嬢：アイン様、現時点で" + GroundOne.WE2.DuelWin.ToString() + " 勝 " + GroundOne.WE2.DuelLose.ToString() + " 敗となります。】"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：す、すまねえな・・・その・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("　　【受付嬢：今後のご活躍、期待しております。それでは。】"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：あ、あぁ・・・"); eventList.Add(ActionEvent.None);

                    messageList.Add("アイン：（後味の悪い内容だな・・・ックソ・・・）"); eventList.Add(ActionEvent.None);

                    if (GroundOne.MC != null)
                    {
                        GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife;
                        GroundOne.MC.CurrentSkillPoint = GroundOne.MC.MaxSkillPoint;
                        GroundOne.MC.CurrentMana = GroundOne.MC.MaxMana;
                    }
                    return;
                }
            }
            #endregion

            messageList.Add("　　【受付嬢：アイン様、現時点で" + GroundOne.WE2.DuelWin.ToString() + " 勝 " + GroundOne.WE2.DuelLose.ToString() + " 敗となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：今後のご活躍、期待しております。それでは。】"); eventList.Add(ActionEvent.None);

            if (fromGoDungeon)
            {
                messageList.Add("ダンジョンゲートの入り口へと強制送還されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);
            }
            else
            {
                messageList.Add("DUEL闘技場の入り口ゲートへと送還されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);
            }

            if (OpponentDuelist == Database.DUEL_EONE_FULNEA)
            {
                if (fromGoDungeon)
                {
                    messageList.Add("アイン：っと、終わったら即送還かよ。本当に有無を言わさずだな。"); eventList.Add(ActionEvent.None);
                }
                else
                {
                    messageList.Add("アイン：っと、終わったら闘技場入り口へと送還されるのか。便利なシステムだな。"); eventList.Add(ActionEvent.None);
                }
            }

            if (fromGoDungeon)
            {
                messageList.Add("アイン：さて、ダンジョン行くとするか！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：さて、DUELも終えた事だし、ダンジョンにでも行くとするか！"); eventList.Add(ActionEvent.None);
            }

            // [警告]：オブジェクトの参照が全ての場合、クラスにメソッドを用意してそれをコールした方がいい。
            if (GroundOne.MC != null)
            {
                GroundOne.MC.CurrentLife = GroundOne.MC.MaxLife;
                GroundOne.MC.CurrentSkillPoint = GroundOne.MC.MaxSkillPoint;
                GroundOne.MC.CurrentMana = GroundOne.MC.MaxMana;
            }
        }

        public static void Message89999(ref List<string> messageList, ref List<ActionEvent> eventList)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add(""); eventList.Add(ActionEvent.HomeTownDuelSelect);

            messageList.Add("アイン：っしゃ、対戦相手でも確認しておこうか。"); eventList.Add(ActionEvent.None);
        }
        #endregion

        #region "Duelist達の会話"
        #region "エオネ・フルネア"
        public static void Message90001(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・もう６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おばちゃん、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、アインかい。ようやく起きたね。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：そういえば、表に闘技場の受付が来てるよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、マジかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：連絡事項があるそうだよ。早く行ってやんな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わ、わかったぜ。サンキュー、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よう、闘技場の受付さんじゃねえか！　おはようっす！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：おはようございます、アイン様。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUEL対戦シード登録・・・つまり、今日が対戦日って事か？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：はい"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ・・・って、対戦相手は誰なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日は、必ずDUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：えっと、ダンジョンへ直接向かったら駄目なのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日は、必ずDUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っとぉ・・・分かった・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：じゃあ、DUEL闘技場へと向かうぜ。了解了解！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：連絡事項は以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：ふう、今日がDUEL日ってワケか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、DUEL向けに準備でも整えるとするか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90001_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：よお、Duelの受付さんじゃねえか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：ッゲ！いきなりかよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っちょ、待ってくれよ。今からダンジョンなんだが。"); eventList.Add(ActionEvent.None);

                messageList.Add("    【受付嬢：DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：マジかよ。少し戦いの準備支度をしたいんだが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("    【受付嬢：DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：本当有無を言わさずだな・・・次からは気をつけねえと。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：ああ、じゃあ転送してくれ。頼んだぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っと・・・ここは、突然闘技場の中央に飛ばされたのか。"); eventList.Add(ActionEvent.None);

            messageList.Add("エオネ：よろしくお願いしますね。アインさん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っあ、あぁ。　こちらこそ！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("エオネ：はい"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch1 = true;
        }
        #endregion

        #region "マーギ・ゼルキス"
        public static void Message90002(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・もう６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：そうそう、また来てるわよ、闘技場の受付。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：グア・・・DUEL通達か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：待たせちゃ悪いからね、早く行ってやんな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、じゃちょっくら挨拶してくるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よう、受付さん、今日もいい天気だな！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そっか、じゃあ今日がDUEL開始日ってワケだな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ところで、対戦相手は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、分かったぜ。連絡サンキューな！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：連絡は以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：ふう・・・さてと"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オーケー、DUEL準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90002_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ゼルキス：貴様がアインとか言うヤツか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ゼルキス：潰してやるぜ。アイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、やれるもんならやってみろ。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch2 = true;
        }
        #endregion

        #region "セルモイ・ロウ"
        public static void Message90003(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・もう６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、闘技場の受付だよ、行ってやんな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUEL通達か、了解了解。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いつも悪いな、朝早くから"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：いえ"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：連絡サンキュー。　対戦相手の名前は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それはそうと、何でダンジョンへ赴く前じゃないと駄目なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：それは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：ともかく、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ。　オーケー。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：受付だから、余計な事までは喋れないって感じなのか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあいいや、DUELの準備するか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90003_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("セルモイ：よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、よろしくな。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("セルモイ：本気で行きます！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、かかってこい！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch3 = true;
        }
        #endregion

        #region "カーティン・マイ"
        public static void Message90004(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・もう６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、闘技場の受付だよ、行ってやんな"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オーケー"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いつも俺より早く起きてるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：はい"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：対戦相手の名前は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：前にも聞いたけどさ。　何でダンジョン行くのが禁止なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：禁止ではありません。ルールです。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、ダンジョンで少し強くなってからと思うんだが、どうだろう？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：認められておりません。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っちょ、待ってくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なんでしょうか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：対戦相手は男性なのか？　それとも女性なのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：お答えいたしかねます。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：まあ、名前からして女性って気もするが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：女性相手だとやりにくいんだよなぁ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でもまあ、DUELだしな、万全の体制でいくか！"); eventList.Add(ActionEvent.None);
        }
        
        public static void Message90004_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("カーティン：私、最近調子が良いのよ。さあ、私の糧になってちょうだい。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どうかな。そう簡単に糧になりはしないぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("カーティン：容赦しないわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こっちも本気で行くぜ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch4 = true;
        }
        #endregion

        #region "ジェダ・アルス"
        public static void Message90005(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・５時４５分か。ちょっと早いけど起きるかな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、また来てるわよ、受付のお嬢ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：もう来てるんだな。　いつも何時ぐらいに起きてるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ぐぁ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ジェダ！？あの、大金持ち暮らしのボンボンかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：はい"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：不慮の事故で、開催されなかった事って今まであるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：ありません"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：もう１つ聞きたい事があるんだが。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なんでしょうか。　手短にお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUELで勝ち続けると、何か褒美みたいなのはあるのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：ありません"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：それでは"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：しかし、スパっと言い放って帰っていくな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さてと、じゃあDUELの準備でもするかな！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90005_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ジェダ：また弱そうなヤツが来た（笑）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なら、倒してみろよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ジェダ：クスクス・・・じゃ、やろっか（笑）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：行くぜ！"); eventList.Add(ActionEvent.None);

        }
        #endregion

        #region "シニキア・ヴェイルハンツ"
        public static void Message90006(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUEL受付の嬢ちゃんだよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、顔を出してくる。　了解。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よお、受付さん。おはよう！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：おはようございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：さて、アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：シニキア・・・マジかよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUEL闘技場もダンジョンも行かない場合は、どうなるんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：必ずDUEL闘技場へお越しください。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、いやいやちょっと立て込んでて行けない場合は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：私のほうが、そちらへ赴きます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それで強制転送を使うのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：はい"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺が逃げたらどうなるんだよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ひょっとして、絶対逃げ切れないって言ってる？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：・・・逃げないでおくか。逃げるだけ無駄って感じもするし。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：さてと、じゃあDUELの準備でもするかな！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90006_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ヴェイルハンツ：ランディスの弟子か。ちょうど良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：その名前！まさか、カール爵の！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェイルハンツ：父の魔道こそ一番だと、ここで証明してみせよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハッハッハ！あぁ、見せてみろよ！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴェイルハンツ：では、勝負！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しゃ、来い！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch6 = true;
        }
        #endregion

        #region "アデル・ブリガンディ"
        public static void Message90007(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、また来てるわよ、受付のお嬢ちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう！　受付さん。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：おはようございます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：相手は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：聞いた事がねえな・・・誰なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・お答え"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、いやいやひとり言だ、悪い悪ぃ。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：そうですか"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そういえば、聞いておきたかったんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：受付さん、って毎回呼びにくいんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：受付さん、名前は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：連絡は以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：流れるような定型文と立ち去り方・・・ある意味すげぇ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90007_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ブリガンディ：ッハン、こんな奴かよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：悪かったな、こんな奴で。"); eventList.Add(ActionEvent.None);

            messageList.Add("ブリガンディ：言っておくが、お前みたいなタイプは何人も倒してきてる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ブリガンディ：今回も同じように勝たせてもらうぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だったら、同じパターンで倒してみろよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ブリガンディ：一瞬で終わらせてやるぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：負けてたまるか！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch7 = true;
        }
        #endregion

        #region "レネ・コルトス"
        public static void Message90008(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いやあ、おはようおはよう！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・（ゴホン）"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：相手は？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どんな奴なんだ？って聞いても駄目か・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・気弱で、引っ込むタイプのようです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：聞いた俺が悪かった、じゃな。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・（ッゴホン）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：え！？いや、聞いてなかった、もう１回頼む！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手の特徴はお教えする事は出来ません。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：し、しまったあぁぁぁ・・・もう１回後生だぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：気弱で引っ込むタイプです。そのせいか、一度劣勢になると防御に戦術を切り替えます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なるほど・・・気弱で引っ込むタイプ・・・劣勢時は防御・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あれ？何でそんな気弱な奴がＤＵＥＬに参戦してるんだよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：それぞれの想いがあるのでしょう。我々はそこまで関与しておりません。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：連絡は以上です。　それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：特徴を教えたら駄目だったんじゃねえのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・まあいいや、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }
        
        public static void Message90008_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("レネ：スミマセン・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？何がだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("レネ：あの・・・わ、わたし、剣装備なんですけど・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おぉ、そうみたいだな。（何か調子狂うな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("レネ：あ・・・あの、卑怯だと思ったら許してください、スミマセン！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：わ、わかったわかった。卑怯もクソもねえから、心配するなって。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("レネ：ス、スミマセン！　い、い、いきますね！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、かかってこい！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch8 = true;
        }
        #endregion

        #region "スコーティ・ザルゲ"
        public static void Message90009(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add("受付嬢：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、いつも早いな！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ザルゲ・・・聞いた事があるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：残虐非道な行為、狡猾な戦術。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、巷じゃ結構有名だしな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って受付嬢さんは、俺に情報提供しちゃ駄目なんじゃないのか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：というか、受付嬢さんって言い難いんだが・・・名前を教えてくれないか。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：ミシェル・キーナと言います。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ミシェル・キーナさんっと・・・了解了解。じゃあ、キーナさんで良いか？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ・・・そこでお答えできねえってか。まあ良いけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン・ウォーレンス様。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おお何だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：本日の対戦、勝っていただけますか？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おお、いつもそのつもりだぜ。それがどうした？"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：私、あのような非道な人間にいつまでも勝って欲しくはありません。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：アイン様、どうか・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：任せておけって、どんな奴でもＤＵＥＬでは公平だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：必ずぶちのめしてやるさ、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：・・・お願いいたします、アイン様には期待しておりますので。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢：それでは。"); eventList.Add(ActionEvent.None);

            messageList.Add("受付嬢は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：キーナさん、ああやってしゃべると、案外普通の人なんだよな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90009_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ザルゲ：ヒュゥ！　アイン・ウォーレンス様のご登場ってワケか！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、悪かったか？"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：とんっでもゴザイマセン、アイン様。"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：誠心誠意持って、戦わせていただきたく存じます・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：ってかぁ！？　ゲハハハハハハハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：その手には乗らねえぜ。準備は万端だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：っおうおうおう、キッチリしてやがんなぁ。ツマンねえ野郎だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：おう、待て待てって、キーナ嬢ちゃん！！　そう焦るなよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：次に俺が勝つと、てめぇが俺の嫁さん決定って約束だったかなぁ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：！！！】"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：ッヒャヒャヒャ、てめぇのご両親の借金チャラって事にしてやんだぞ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：悪いがこの婚約。ご両親からは快く承諾してもらってるって話だぁ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：しょうがねえから、勝ってやるんだぜ、ありがたく思えよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：ゲハハハハハハハハハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：デュ・・・DUEL開始となります！】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（そういう事か・・・どうりで、朝のあの言い方・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：アイン・ウォーレンス様、お手柔らかに、ックッックククハハハ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：悪いが、俺には関係ない話だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：・・・あぁ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：好きにするが良いさ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だが、ＤＵＥＬは真剣勝負。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：私情は関係ねえ、とにかく勝たせてもらう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ザルゲ：ッッッソッタレ、アイン・ウォーレンスよぉ！　ムカツくぜえぇぇ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サッサと始めるぜ！　勝負だ！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch9 = true;
        }
        #endregion

        #region "ペルマ・ワラミィ"
        public static void Message90010(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、キーナさん。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：・・・おはようございます。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ワラミィさんって、クヴェルタ街の魔法武具専門ショップのオーナーだよな？"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：はい。魔法武器を駆使した戦術を編み出したそうで最近は非常に好調のようです。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：魔法武具のオーナー・・・どんなのが飛び出してくるやら・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なあ、キーナさん。その辺の戦術をコッソリと・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：ア、アイン様！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：前回はありがとうございました！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うお！？何だいきなり、どうしたんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：っその・・・アイン様が負けていたら、わたしは今頃・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：っう・・・うう・・・ありがとう・・・ございました・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("　　　（" + KIINA + "はその場で泣き崩れてしまった！）"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っちょ！っちょちょちょ、タンマ！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：分かった、気持ちは分かった！　だから泣き止んでくれ、っな！？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：でも、もしザルゲが勝っていたらと思うと・・・うう・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あの非道なる男を前に、次々と対戦相手は倒れ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：相手を騙す戦術が得意で、生真面目タイプの対戦相手は大概が・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、確かにそれなりの戦術だったよな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：口は悪いけど、単なる３流だと思って挑んだヤツは負けて当然だろう。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：３０連戦、連勝したら・・・という約束でした。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あんな非道な男、勝てるはずが無い。そう思って約束して・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：バカ、気を付けろよ。ああいうヤツに限って、戦術に長けてるんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：っう・・・うう・・・すいません・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：連戦３０戦目・・・アイン様で・・・わたし、本当によかった！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：っう・・・うう・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：い、いやいやいや、タンマ！！頼むから泣かないでくれ、っな！？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：す、すいません・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ふう・・・とにかく、くだらない約束はするなって事だ、気を付けろよな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：え、ええ・・・"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハハハ・・・まあ、そりゃそうだよな。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：さて、じゃあ魔法対策でもやっておくかな。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：アイン様、このまま勝ち続けてくださる事を祈ってます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：嬉しい事言ってくれるじゃねえか、任せておけ！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ありがとうございました。　今後もがんばってください、それでは。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：連絡は以上です。　それでは。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90010_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ワラミィ：お主がアイン・ウォーレンスじゃな？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、そうだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ワラミィ：ふむ、ガンツめ、良いヤツを近くに置いとるな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッハハ、ガンツ伯父さんと知り合いなんですね？"); eventList.Add(ActionEvent.None);

            messageList.Add("ワラミィ：当たり前じゃ。ワシとガンツは互いにライバルのようなもの。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よろしくお願いします！"); eventList.Add(ActionEvent.None);

            messageList.Add("ワラミィ：良き心構えじゃな。　それでは、始めさせてもらおうかの。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch10 = true;
        }
        #endregion

        #region "キルト・ジョルジュ"
        public static void Message90011(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いつもわざわざ連絡ありがとうな。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：・・・いえ、お構いなく。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っげげ！！　何で現国王の息子と戦わなくちゃならないんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、何でエントリーされてんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、何で俺が対戦相手になってんだよ！？"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：・・・弱りました。お答えいたしかねます。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：お答えいたしかねます。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：い、いやいやビックリしただけだって。　別に答えなくていいけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：しかし、参ったなあぁ・・・国王の家訓ってアレだろ？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：聡明！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：雄大！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：高潔！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：とか何とか、そういう家訓が山のようにあるって話じゃねえか。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：その家訓は日々増え続けており、今では２５０種にも及ぶようです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：うげ・・・あり得ねえ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんなふうに鍛えられたヤツが相手かよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんなのに限って、結構チャランポランなヤツだったりして・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：次期国王の筆頭であるキルト様は、毎晩３時まで武術稽古を欠かさないとの噂です。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：早朝の起床５時。学を習得し、努めてから朝食に入るとの噂もあります。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：その後も、実践経験を積むため、全方位的転送装置で各地方を巡回し、一日を終始されるそうです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハハハ・・・笑えねえ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：でも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アイン様も日々ダンジョンで鍛錬されているそうですね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。鍛錬って言うのとは、少し違うけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：わたしはアイン様を応援してます。　がんばってくださいね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、任せておけ！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：国王の息子だろうが関係ねえ、ぶち当たるまでさ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：クスクス、国王が今のを聞いたらきっと笑うでしょうね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハハハ・・・ちょっと言い過ぎたかもな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あっ、すみません。少し長くなったみたいですので・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：連絡は以上です。　それでは。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、またな。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡も済ませたので、私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ。"); eventList.Add(ActionEvent.None);

            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90011_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：（・・・出たあぁ・・・王家のオーラがプンプンするぜ）"); eventList.Add(ActionEvent.None);

            messageList.Add("キルト：アイン・ウォーレンス、ようやく会う事ができたか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、初顔合わせだよな？"); eventList.Add(ActionEvent.None);

            messageList.Add("キルト：オル・ランディスの弟子と聞いているが？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お、おお、一応それで合ってるはずだ。それがどうかしたか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　（対戦相手キルトは剣の切っ先を丁寧にアインへ向け・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("キルト：相手に不足は無い。"); eventList.Add(ActionEvent.None);

            messageList.Add("キルト：全身全霊を持って挑ませていただく。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　（キルトは凛とした態度で剣を真っ直ぐに構えた！）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（この年でこの雰囲気・・・やるじゃねえか）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こちらも準備オッケー。　いつでも行けるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("キルト：では、勝負！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch11 = true;
        }
        #endregion

        #region "ビリー・ラキ"
        public static void Message90012(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ビリー・ラキ？？・・・誰だ？"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：ビリー・ラキ様は、アイン様の事を良くご存知でおられるようです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうなのか？アレ、誰だったっけ・・・やべ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アイン様は確か、オルガウェイン傭兵訓練施設の卒業者でしたよね？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、そうだな。よく知ってるじゃねえか。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ははーん、そういう事か。そういや、あの頃はライバルがゴロゴロいたっけな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ライバルはよく記憶している者も中には居ます。アイン様は知らない間に倒していたのかもしれませんね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？ああ・・・そうなのかもしれん・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ビリーは私の幼馴馴染みなのですが、どうぞ心ゆくまで戦ってやってください。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おお、そうなのか！？そりゃ奇遇だな、ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ビリーときたら、私じゃなくてラナさんの方にばかり・・・だから私だって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：え？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：っな、何でもありません！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：うわっと、怒るな怒るな・・・ハハッ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・ッコホん"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：お答えいたしかねます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そ、そうか・・・ハハハ・・・"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：ビリーの戦術について少し解説いたします。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：彼の戦術は基本殴って殴って、殴りまくる。それだけがモットーです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それだけなのか？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：そのようです。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ほかには？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：後はご自分でお確かめください。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90012_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ビリー：来たか、アイン・ウォーレンス！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お前は確か・・・っと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ビリーであってるよな？"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：・・・・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ラキ！　そうそう！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ビリー・ラキ！　久しぶりだな、ッハッハッハ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：うおおおぉぉ、まさか忘れたとは言わせねえぞおおぉぉぉ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：悪ぃ、すまねえが後で教えてくれよホント。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：くそ・・・完全に忘れてんのかよ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：そんな忘れグセの激しい貴様なんかに・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：何でラナちゃんがああぁぁぁぁ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ、なんでそこでラナが出てくるんだよ？　関係ねえだろ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：うっせぇ、うっせぇ！　コッチの話だ！"); eventList.Add(ActionEvent.None);

            messageList.Add("ビリー：構えろ！　絶対に貴様は潰す！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あ、ああ・・・じゃあ行くぜ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch12 = true;
        }
        #endregion

        #region "アンナ・ハミルトン"
        public static void Message90013(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハミルトンってどっかで聞いた事あるんだけどな・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：ハミルトン家は代々ファージル宮殿の遊撃騎士団長を務めていますね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おおぉぉ、ソレだ！何か聞いた事あるなと思ってたけど。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：でも、アンナって名前は聞いた事がねえけどな。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：ハミルトン家の現当主クメル・ハミルトンの長女ですね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何でそんなトコまで分かるんだよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：って、そうか。DUEL対戦の経歴とかは受付さんには全部丸わかりだって噂だしな・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：ご存じ無いのも無理はありません。世間一般には知られていませんからね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何かワケありって事なのか？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ファージル宮殿の騎士団長である以上・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、分かった。そういう事か。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：余計な面倒事、危険な事柄に巻き込ませないためか。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：はい、その通りです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っで、その長女さんがこんな大舞台に出てきても大丈夫なのかよ？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：DUEL闘技場に出てきた以上、それはもう世間一般に知られるって事だろ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ハミルトン家は元来より『武』に長けた家系なので、それを示す意味で参戦されてるのだと思われます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：なるほど、DUEL闘技場で戦績を上げていれば、そう簡単に拉致出来る相手じゃねえって事になるな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：しかし・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺は負けられねえ。勝たせてもらうぜ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アンナ・ハミルトンは杖を装備したマジシャンタイプでありながら"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：折々にスキを見て、スタン効果の攻撃も繰り出してくるため"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：単純なコンボではありますが、強敵なのは間違いありません。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：え、えっと・・・頑張ってください！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？あ、ああ頑張ってくるぜ！ッハッハッハ！"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }
        
        public static void Message90013_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っよっと、到着到着っと・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おっ、アンナ・ハミルトンさんだな。よろしく頼むぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：っゆ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：っ許せないわ！！　アイン・ウォーレンス！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：どわぁ！　な、なんだよ一体いきなり！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：あなたさえ、出場しなければ・・・っく・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ま、まあ・・・落ち着け。何の言いがかりか全く分からねえが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：キーナは・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：キーナは私だけのものよ！！　アンタなんかに絶対渡さない！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな、どういう話の展開だよ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：ここであなたは負けるの！そしてキーナには一切近づかないでちょうだい！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いやいや・・・まいったな・・・ハハ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：悪いが、負けるつもりはねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：勝たせてもらうぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：キーナをさらうつもりね。そうはさせないわ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：い、いや・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アンナ：勝負よ！　アイン・ウォーレンス！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、勝負だ！（妙なノリだな・・・）"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch13 = true;
        }
        #endregion

        #region "カルマンズ・オーン"
        public static void Message90014(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：オーン先生！！　出ているのかよ！？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：傭兵訓練施設の誇示が目的で、毎年必ず施設内で選別されて出場をされていますね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジであの先生かよ。。。結構骨が折れそうだな。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：あの、アイン様。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・カルマンズ・オーンの裏戦術。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ご存知ですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おっと、待ってくれよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そいつはさすがに受付嬢としては、規則違反に当たるんじゃねえのか？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、この会話が誰にも聞かれてなければ通るだろうが。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そんな事言ってたんじゃ、試合前の暗殺と同じ類になっちまうな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：す、すみません！　すみませんでした！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：い、いやいやいや。そんな謝らなくて良いって。ッハハハ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺なら大丈夫だって。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：裏戦術、表戦術、あるいはそれ以外の要素。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：それら全部ひっくるめてDUELだ。正々堂々行くぜ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：私の考え方が卑小だったんです。ご、ごめんなさい・・・っう・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だだだ、だから良いって言ってんじゃねえか、っな！？その泣くな、泣くなって！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そそ、そうだ。アレだ、いつもの基本戦術を解説してくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・すみません。。そうですね、公となっている戦術を今から説明いたしましょう。"); eventList.Add(ActionEvent.None);

            }

            messageList.Add(KIINA + "：カルマンズ・オーンの戦術は大きく分けて、２種類あります。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：１つ。　ライフ・コントロール。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "相手を倒しに行くのではなく、自分の方がライフアドバテンテージが高くする方法です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、それは知ってる。あれは嫌でも嫌になるぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：２つ。　五月雨系の連撃。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：タイミングはケースに応じてですが、突如連撃を繰り返し行なってきます。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あれも冗談キツイぜ。うっかり気が緩んでるヤツなら瞬殺だろうな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ってか、やっぱりそうなんだな。。。ッハハ、あの人とやるとはな。厄介だぜホント。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：それでは、アイン様のご武運を祈っています。私はこれで"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それ以外にもカルマンズ・オーンは秘策を持っているとの噂もあります。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90014_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("オーン：来たか、ガキアイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺はガキじゃねえ。久しぶりだな、オーン先生。"); eventList.Add(ActionEvent.None);

            messageList.Add("オーン：悪いが喋る時間は不要だ、とっとと始めよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：即開始かよ。ッハハハ、オーン先生らしいぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("オーン：構えろ、アイン。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、いつでもいいぜ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch14 = true;
        }
        #endregion

        #region "サン・ユウ"
        public static void Message90015(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：う～ん、聞いたことのねえ名前だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：戦績とかは聞けるんだったよな。どうだ、強いのか？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：サン・ユウの戦績は・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：26勝1敗です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ッゲ！！！　マジか！？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ハッキリ言ってFiveSeekerレベルの勝率じゃねえか。冗談きついな。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：必要に応じて、ライフを回復する。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：BUFF構築を適度に行う。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：コンスタントにダメージを与える。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：サン・ユウの戦術は極めて明確です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まいったな・・・そういうのが強いって言うのはある意味相場だよな・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add("アイン：でも26勝1敗って、1敗の相手は誰だったんだよ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：オル・ランディスです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・最悪だな・・・あのボケ師匠、新参モノにも容赦ねえしな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：師匠はその時さ、勝ちセリフで何て言ってた？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：勝ちセリフ・・・ですか？　何か意味があるんでしょうか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、そっけねえがタメになる一言を伝えて終わるのが師匠のクセだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ええと、待って下さいね・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・　・・・　・・・　あ！　思い出しました。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：（声マネ）『ランディス：前提ミエミエなんだよ、ザコが。』"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：前提ミエミエか・・・なるほどな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あの、今のセリフが重要なんでしょうか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、そうだな。かなり重要だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：師匠はガサツで乱暴に見えるが、基本的な戦術論はかなり緻密だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：だから大概普通の戦術論はフツウに見抜いちまう。多分それを伝えたんだと思うぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ええと・・・私には分かりませんが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アイン様の役にたったのであれば・・・私嬉しく思います！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・ん？あ、ああ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：サンキュー、サンキュー、キーナさんの解説はいつも助かるぜ！　ッハッハッハ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・それでは、私はこれで。ご活躍期待しています。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90015_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("サン：来たきた、アインさんだ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、サン・ユウだな。お手柔らかに頼むぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：・・・へえぇ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『サン・ユウの目はスゥっと細くなった』　　"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：ホントだ。。。確かにアインさんは、アインさんだね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？どういう意味だ？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『サン・ユウの顔全体が少しホワっとした笑顔になり・・・』　　"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：ボクの知る限り"); eventList.Add(ActionEvent.None);

            messageList.Add("アインさんはオル・ランディスの愛弟子。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　　『サン・ユウの目は突如カっと目を見開いた！』　　"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：この天才のボクに泥を付けるなんてなぁ！ランディスめ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：しかも説教付きだ！　絶対に許せない！！"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：ランディスの弟子、アイン・ウォーレンス！！貴様だけは絶対に潰すぜ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・悪いが俺には関係ねえ話だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だが、これだけは言える。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：サン。そんなんじゃお前は、あのボケ師匠に一生勝てねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：ッグ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：っく、くそおおおぉぉぉ！！！！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("サン：ボクが２敗なんてあり得ないんだ！！　貴様だけは絶対に絶対に絶対に絶対に絶対に絶対に潰す！！！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、かかって来い。受けて立つぜ！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch15 = true;
        }
        #endregion

        #region "シュヴァルツェ・フローレ"
        public static void Message90016(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・フローレ！！？　まさか！！！"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：そうですね。彼は王妃様にとってのたった一人だけの従弟。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：家族を全て亡くした王妃ファラ様・・・。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そんなファラ様に残されたのは、たった一人の執事役シュヴァルツェ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ファラ様の笑顔を、長年の歳月をかけて戻してくださったのが、"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そのシュヴァルツェってワケか。すげえよなコイツも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ファラ様の笑顔は、この上なく綺麗だからな。見てるコッチが恥ずかしくなるぐらいだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：今でもファラ様を守るために、ほぼ寝ずの状態で鍛錬の日々を過ごされているそうです。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：戦術は既にご存知でしょうが、お聞きになりますか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、頼むぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：基本的には守りの戦術。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：BUFF構築と要所のカウンターでアドバンテージを確立させ、"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そのアドバンテージを拡げていき、敵に降伏させるのが基本戦術です。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何か戦術一つとっても、その人の人柄みたいなのが出るんだな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：守りの戦術か・・・実際手強そうだぜ。気を引き締めないと勝てそうにないな。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：ファラ王妃に対する忠誠心はおそらく彼が一番抱いている事でしょう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ふう、気合勝負になりそうだな。。。戦術は練りに練っておくとするか。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90016_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：・・・よろしく頼むぜ、シュヴァルツェさんとやら。"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：アイン君だね。ようやく会えた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っな、待ってたのかよ？"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：どうしても一つ、聞いておきたい事があってね。"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：君はどうしてこの闘技場へ参加しようと思ったんだい？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ん？DUEL前にそんな話か・・・そうだなあ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：参加理由は、師匠に勝つためだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：DUELは一戦一戦が勝負そのもの。師匠はそれにほぼ勝ち続けている。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そのセンスは俺も同じように持っている。そのセンスを腐らせたくはねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあこんな所だ！　満足のいく内容だったか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：ああ、満足だよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：君のような人がいて本当に良かった。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っへ？？"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：なんでも無い。こちらの話だ。"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：さて、はじめるとしよう。DUEL戦では本気で戦ってもらおうか！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、当然だ。DUELじゃいつも本気だぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("シュヴァルツェ：では！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：っしゃ、来い！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch16 = true;
        }
        #endregion

        #region "ルベル・ゼルキス"
        public static void Message90017(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ゼルキス・・・どっかで聞いた気もするんだが・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：ファージル宮殿にて、ここ最近仕え始めたゼルキス家の長男です。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add("アイン：ああ、そういやそうだった。すっかり忘れてた。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そういや最初の方で、マーギ・ゼルキスってヤツと戦った気がするが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：マーギ・ゼルキスは次男にあたります。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あっ、やっぱりそうなんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：って事はアイツと戦術は大体一緒なのか？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そうですね、ほとんど同じだと言われています。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：次男マーギは、回復ポーションと直接攻撃、火魔法の基本的なスタイルに対し"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：長男ルベルは、回復ポーションと直接攻撃、それと水魔法を駆使してきます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・そうなのか。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：どうかされましたか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、なんて言うのかな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：全然違う気がするな、それ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：水魔法と言っても、基本的には攻撃魔法を主体とした戦術の様です。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。だが、そこの話じゃないんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：キーナさん、ひょっとしてその、ルベル・ゼルキスの公の戦術ってのは"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：プレッシャー・パーミッション戦術だろ、違うか？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：え、えっと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ、悪い。こっちの話なんだ。気にしないでくれ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：すみません・・・せっかく話をして下さったのに・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：い、いいっていいって。　気にするなって。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：・・・あの！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：な、なんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ももも、もしよかったらまた今度そのプレッス・パーミット戦術を教えてください！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あ、ああ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いいぜ、もちろんだ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あ、ありがとうございます！！！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：長男ルベルは、回復ポーションと直接攻撃、それと水魔法を駆使してきます。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：水魔法と言っても、基本的には攻撃魔法を主体とした戦術の様です。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうか、分かった。ありがとな。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90017_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ルベル：アイン・・・ウォーレンスか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：強いな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：直接見てる今、それがハッキリわかる。私の戦術では勝てそうにないな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：俺は、強くなんかねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：そなたに問いたい、そなたの強さの根源はなんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そんなもんは持ってねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：この勝負、万が一にでも私が勝てたら、その時に教えてもらおう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや、悪いがごめんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ここで負けるわけにはいかないんだ、勝たせてもらう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：ック・・・ックハハハ！　面白い！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ルベル：勝負だ！　アイン・ウォーレンスよ！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：こい！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch17 = true;
        }
        #endregion

        #region "ヴァン・ヘーグステル"
        public static void Message90018(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：聞いたことの無い名前だな・・・誰なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：戦歴は非常に浅く、戦術もそれほど公にはなってない、無名の新人です。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：勝敗は１６勝０敗。今の所全勝です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：マジかよ・・・一番やりづらいタイプだな。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：でも私・・・彼の訓練する所を見ました！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：っちょ、待ってくれ。ひょっとして非公式な情報じゃ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：良いんです、大丈夫ですから。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おいおい、よせって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：彼の基本戦術は、ベースは物理と魔法の両方からの攻撃です。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：タイミングに隙があれば、ニュートラル・スラッシュをインスタントで発動。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：魔法は火がベース、ピアッシング・フレイムをメインとしてました。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ダメージ軽減でハーデスト・パリィも練習していましたし、カウンターも揃ってます。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：攻めて良し、守って良し、特に隙が無い感じの鍛錬をされてるようです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いいのかよ、そんなに言っちゃって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・あれ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：どうかされましたか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ニュートラル・スラッシュとピアッシング・フレイムって言ったよな？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ええ、はい。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：おかしいな、それ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：え、えっ、どうしてですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、別に戦術にケチをつけるわけじゃねえんだけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺の考えうる限り、魔法なら魔法、物理なら物理で固めてくるはずなんだ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、そうなんですよね！やっぱり私もそう思ってました！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：【多彩さ】をウリにする素人的発想で長生きはしないはずなんだが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：勝率データからして、弱いとも到底思えない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あのっ、参考にしてもらえたでしょうか！！？？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？あ、ああ、かなり参考になったぜ、サンキュー！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あっ、喜んでもらえて・・・うれしいです！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：頑張ってください！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、ありがとな！　（ハハハ、やたらと元気だな。。。）"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90018_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ヴァン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：戦術は、火と物理。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：ニュートラル・スラッシュとピアッシング・フレイム。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なっ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・惑わされないぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：惑わしてなどいない、今から繰り出す技を教えているだけの事。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：自信があるって事か。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：自信ではない、予め敵に伝えておき、対策の手筋と思考形成を縛らせるのが目的。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・この辺は師匠から嫌と言うほど教えられている。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：ほう・・・彼、オル・ランディスはなんと？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：『行為が思考を上回るレベルで戦え』って言われている。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：なるほど、通用せぬという事だな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：では、そろそろ行為に入るとしよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：よし、こっちは準備いいぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ヴァン：では、参る。"); eventList.Add(ActionEvent.None);
            GroundOne.WE.TruthDuelMatch18 = true;
        }
        #endregion

        #region "オウリュウ・ゲンマ"
        public static void Message90019(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あっ、聞いたことあるぜ！　その名前！"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：闘技場において、最も長く戦歴を残している者です。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：勝率は５６４２勝３９８７敗。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あのオッサン、どれだけの数をこなしてんだよって感じだしな。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：オウリュウ・ゲンマの戦術は固定枠にとらわれていません。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：勝敗結果よりも、プロセスの最中で、常に新しい技を編み出そうとする事で有名です。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：まあ、確かに。何か毎回やらかしてるって感じだもんな。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：でもわたし、昔から見てきていて思うんですけど。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：彼はそれでもやっぱり、勝ちに対する意識は残ってるように思えるんですよ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうだろうな。あれは何ていうか一種のメンタル・トレーニングなんだと思う。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：自分の戦術型が極地にハマりすぎてないか、常にチェックしてるんだと思うぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あの点は、師匠にホントよく似てる。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あっ、そういえば、彼はよくオル・ランディスと実践訓練をされてるそうです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：やっぱり、そうなのか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：しかし、何ていうのかな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ランク上位ってのは、似たような強さを持ってるよな、本当。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あっ、それわたしにも何となく分かります！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：トップランクの皆さんって優しいですよね！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あと、とっても話が面白いです！！トゲが無いっていうか・・・その・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あとあと！　考え方もとっても柔らかい感じがします！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうそう、何か捉えどころが無いんだよな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：でもまあ、それに気圧されてちゃダメだからな。頑張って俺も追いつくぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アインさんも優しくてとっても強い方ですから、きっと大丈夫です！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ハハハ、そう言ってもらえると気休めとしては有り難いな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：い、いいえいえいえ！！　本心に誓って、本当にそう思います！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、頑張ってくるさ。　じゃあ、少しアップに行くから。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あっ・・・ハイ！！頑張ってきてください！！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90019_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ゲンマ：おぬしが、ヴァインか！　ようきた！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ヴァインじゃない、アイン・ウォーレンスだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：あああ、そりゃ失礼！！　ダーーッハッハッハ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：でだ。　おぬし、得意技は？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：いや、特にそういうのはねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：そうかそうか、特になしかと！！　ヴァーーッハッハッハ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：で、恋人は？"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・いや、関係ねえ話だ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：ヴゥワーーッハッハッハッハ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：あーりゃりゃりゃ、もう始まっちまうのかよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：のお、ヴォーレンスよ。おぬし、もっと人生を、満喫せよ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ウォーレンスだ。今はDUELに集中するぜ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：カアァァ！つれないねえぇ～！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：ま、気持ちは分かる。今は、どんっどん、勝つ事！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：ええか、だが、よ～く聞け。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：いずれ、勝っても勝っても、つまらなくなる、そんな時が・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：くる！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：そしたらなぁ、勝つか負けるかより！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：技を、極める事！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：技を、楽しむ事！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：技を、あみ出す事！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：これを、おぬしも、やってみることだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・ああ、わかった。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：技を進化させる際、負ける事も、よくよくある事。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：勝ってばかりじゃあ、技は進化せん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：勝ちは停滞。勝ちは思考の収縮。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：負けそうな時こそ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：自由に、楽しく！　ヴァーッハッハッハッハ！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：あっ、それとだ。技うんぬんよりも前にだ！！"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：人生の広がり！！　男と女！　酒と旅！！　栄光と挫折！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：DUEL開始となります。両者構えてください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：あぁ～、嬢ちゃん、そりゃないぜえぇ！！良いとこなのによおぉ・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：両者構えてください。】"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：けっ・・・じゃ、しょうがねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ゲンマ：おぅ、かかってこぉい！　ヴァイン・ヴォーレンス！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おし、行くぜ！！"); eventList.Add(ActionEvent.None);
            GroundOne.WE.TruthDuelMatch19 = true;
        }
        #endregion

        #region "ラダ・ミストゥルス"
        public static void Message90020(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：何か微妙に雑誌で名前を見たような・・・"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：月刊ファージルハンターによく記載される方です。"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add("アイン：あっ、思い出した！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：大型モンスター討伐を全部一人でやってのけるアイツだろ！？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：はい、まさにその彼です。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：周囲の人達とは折り合いが付かず、行動は荒唐無稽で命知らず"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：彼はよく「相場を知らない者」として罵られる日々を送っていたようです・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：でも彼は徐々に討伐範囲を広げ、今ではダブルＳランクのモンスターまで一人でこなします。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・すげえな・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：もう、彼を罵る人は居ないでしょう。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：で、今度はDUEL闘技場でも・・・というワケか。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：はい、そう考えて間違いはないと思います。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何でもかんでも全部一人でか・・・まあ、DUELはもともと一人が前提だけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あの・・・わたし・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ん？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あの人・・・何となくですが、見ていて怖いです。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：殺気立ってるというか・・・人自体を嫌ってる感触が伝わってきます。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：下手に近づくと平気で殺人でもしそうな雰囲気です・・・怖くて・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・まあ"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：周りのヤツが罵り続けたんだ。そうなるのが、当然じゃないかな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：え！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：悪いのは、そのラダ・ミストゥルスってヤツじゃない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：根本的に悪いのは、周囲の奴らだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、そうなんですか！？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：罵れば人は普通に傷つく。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：表に見えるかどうかだけの違いだ。例外なんていやしないさ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そうやって憎悪の根底が生み出されてきたんだ、殺気の一つや二つ持っててもおかしくないさ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、そんな・・・じゃあまさか、一人で大型モンスター討伐をしてきたのも・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、周囲の人達に対する正当な見せしめ・復讐と言った所だろうな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：抱え込んでるものが深そうだ。かなり手強いな・・・おそらく。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あ・・・あのっ・・・死なないでください！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ッハハハ、大丈夫だって、闘技場だろ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：でも、あるんです！！　ごくまれに！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：おねがいっ・・・おねがいだから・・・うっ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あっ、っちょっタンマ！分かった！！　わかったからここで泣くなって、ホラ・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：分かった分かった、どうしてもヤバイと思ったら降伏宣言すらから・・・なっ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：うっ・・・うっ・・・お願いします。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：約束、絶対ですからね。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ！任せておけ！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：良かった・・・それでは、失礼します。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：またな。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90020_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("ラダ：・・・来たか、待ちわびた。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：噂には聞いてる。だが、一つだけ確認させてもらおう。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：強くなった今の自分だからこそ分かる。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：弱き者は、口を使い、自分のプライドを保持する事にのみ固執する。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：弱者は弱者。抜けられない。そこまでは良い。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：だが何故！！　そんな彼らが我を嘲笑い、罵り、侮辱し続けたのか！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（殺気が・・・ビリビリと伝わってくる・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：答えてもらおう、アイン・ウォーレンス。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：弱き者に価値はない。　死すべき存在だ、そうは思わないか？"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・悪いが、強いとか弱いとかって話は、俺には興味がねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：死する者は自然と死ぬ、生き残る者は自然と生き残る。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：そして、そんな中でお前は生き残った。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：！！！"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：お前は十分過ぎるほど強くなった。もうファージル全域の人々がそれを認めてる。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：弱き者は、もうお前の事を嘲笑せず賞賛を送っている。別に死すべき存在ではないんだ。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：・・・　・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ここからは真剣勝負だ。　言っておくが、俺は負けねえからな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：ッフフフ・・・くだらん戯言だったな。"); eventList.Add(ActionEvent.None);

            messageList.Add("ラダ：勝負は我が勝つ！！来い、アイン・ウォーレンス！！"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch20 = true;
        }
        #endregion

        #region "シン・オスキュレーテ"
        public static void Message90021(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, string KIINA)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add("アイン：ん・・・６時か。そろそろ起きないとな。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おはよう、おばちゃん。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：ああ、おはよう。"); eventList.Add(ActionEvent.None);

            messageList.Add("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、ちょっと顔を出してくる。"); eventList.Add(ActionEvent.None);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic01);

            messageList.Add(KIINA + "：アイン様、お待ちしておりました。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：おう、おはようさん。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：・・・何者なんだ？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：古代賢者オスキュレーテの名を継ぐ者として知られております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：古代・・・賢者？　何の話だ？"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：ファージル全土の黎明紀において、ヒトとしての文明を一から創られた最初の人類。"); eventList.Add(ActionEvent.None);

            messageList.Add(KIINA + "：そう言い伝えられております。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：それが・・・古代賢者・・・"); eventList.Add(ActionEvent.None);

            if (GroundOne.WE.DuelWinZalge)
            {
                messageList.Add(KIINA + "：はい、そして本闘技場でのトップランカー内において、唯一の女性となります。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：マジかよ。まあ、いてもおかしくはないけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ってか、古代賢者っていうのがイマイチ分からないが・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：何か新しい秘術とかを持ってたりするのか？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：いえ、そういった特別な能力を持ち合わせてはいないそうです。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：ただ、公の戦術として、誰でもよく使うあの有名なスキルを駆使してきます。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まさか・・・ストレート・スマッシュじゃないだろうな？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：すごい！！　当たりです。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：で、ゲイル・ウィンドだろ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：はい！それも当たりです！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ワード・オブ・フォーチュンも絡む・・・っと・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：何でそんなにお見通しなんですか！？出会った事は無いんですよね？？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：まあ、そうだけどさ。トップランカーって時点で何となくわかるもんさ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：似通ってくるというか、そうならざるをえない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：でも、似てるだけじゃ生き残れない。ソイツなりのオリジナリティがどこかにあるものさ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：そこら辺になると、実際にＤＵＥＬをしてみない限りは分からないけどな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アインさんは、今までよくＤＵＥＬをされてるのですか？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、特に率先してやったりはしてないな。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ボケ師匠がたまに構ってくれる程度だ。不特定多数を相手してるワケじゃねえ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ＤＵＥＬ数は多分少ない方だと自分では正直思う。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：でも、さっきみたいな話は、歴戦の方々からよくお話を伺います。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アインさんって・・・ひょっとして天才なんじゃないですか！！？？"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：いや、それは無いな。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、そうでしょうか・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：自分は何かに優れているという気はしない。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：成績もそれほど良くは無かったし、可もなく不可もなくって所だ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：あと、肝心の師匠にはほとんど勝てないでいる。今も連敗中さ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：俺は、キーナさんが考えてるような天才なんかじゃない、何処にでもいる普通の傭兵さ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、そんな事ありません！　アインさんは絶対に凄い人なんです！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：わ、わたし、闘技場でアインさんみたいな優しい人に、今まで出会った事ありませんし！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：わ、分かった。分かったから、お、落ち着けって・・・"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：お願いです。わたしと・・・約束してください！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：な、なんだ？"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：アインさん、ゼッタイに闘技場でトップレベルにランクインしてください！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：お願いです！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：・・・　・・・"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：確約はできねえが"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：出来る限りやってみるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：あ・・・ありがとうございます！！！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：キーナさんのためだ、頑張ってみるぜ。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：約束だ。"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：は・・・はい！！！"); eventList.Add(ActionEvent.None);

                messageList.Add(KIINA + "：そ、それでは連絡は以上です！　失礼します！"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ、またな！"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add(KIINA + "：それでは、連絡は以上です。私はこれで。"); eventList.Add(ActionEvent.None);

                messageList.Add("アイン：ああ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add(KIINA + "は立ち去っていった・・・"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("アイン：っさてと、DUELの準備と行くか！"); eventList.Add(ActionEvent.None);
        }

        public static void Message90021_2(ref List<string> messageList, ref List<ActionEvent> eventList, string OpponentDuelist, TruthHomeTown.SupportType type)
        {
            messageList.Add(""); eventList.Add(ActionEvent.StopMusic);

            messageList.Add(""); eventList.Add(ActionEvent.PlayMusic11);

            messageList.Add("　　【受付嬢：アイン様、お待ちしておりました。】"); eventList.Add(ActionEvent.None);

            if (type == TruthHomeTown.SupportType.FromDungeonGate)
            {
                messageList.Add("アイン：Duelタイムってわけか。"); eventList.Add(ActionEvent.None);
            }
            else
            {
                messageList.Add("アイン：よお、受付さん。約束どおり来たぜ。"); eventList.Add(ActionEvent.None);
            }

            messageList.Add("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】"); eventList.Add(ActionEvent.None);

            messageList.Add("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ああ、準備は万端だ。転送してくれ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アインはDUEL闘技場へ転送されました"); eventList.Add(ActionEvent.HomeTownMessageDisplay);

            messageList.Add("シン：アイン・ウォーレンス。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：知恵を超越するもの。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なに？"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：柔軟なスタイルと、異常とも思える潜在性を有する。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：ただし、周囲とのバランス／兼ね合いまでを最適化の対象としてしまう。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：環境さえ揃えば、その能力は計り知れない。だが、今は開花に至らず・・・か。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：なるほど・・・確かに面白い逸材だね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：なっ、何の話をしている！？"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：君は、このプロフェシー・サーガに描かれている通りの人物だ。実に興味深い。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：君自身は気にしなくてもいい話です。"); eventList.Add(ActionEvent.None);

            messageList.Add("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：では、試させてもらいましょう。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：君がこのファージル全域を導く覇者に成り得るかどうか。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（ックソ、なんだこのオーラは・・・シャレになってねえな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（いや、気圧されちゃ駄目だ。ココは・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：言ってる意味が分からねえな。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：・・・"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：ファージル国はエルミ国王が治めてくれている。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：あの人がやってくれてんなら、将来は当分安定だ。俺がどうのこうのと出る幕じゃない。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：だが、今そんな事は関係ない。"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：『そんな事は関係ない』・・・か"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：ッフフフ、なかなか良い考え方ですね。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：（・・・つかめないやつだな・・・）"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：とにかく、今ここで負けるわけにはいかねえ。"); eventList.Add(ActionEvent.None);

            messageList.Add("アイン：勝負だ！　行くぜ！"); eventList.Add(ActionEvent.None);

            messageList.Add("シン：はい、いつでも。"); eventList.Add(ActionEvent.None);

            GroundOne.WE.TruthDuelMatch21 = true;
        }
        #endregion
        #endregion
    }
}

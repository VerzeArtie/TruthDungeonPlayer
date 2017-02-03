using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace DungeonPlayer
{
    public class Tutorial : MotherForm
    {
        public bool toggle2 = false;
        public bool toggle3 = false;
        public bool toggleB2 = false;
        public bool toggleB3 = false;
        public bool toggleDuel = false;
        public Text description = null;
        public Button buttonHomeTown = null;
        public GameObject groupLevel = null;
        public GameObject panelDescription2 = null;
        public Text description2 = null;

        private int selectNumber = 0;
        private int selectLevel = 1;

        public override void Start()
        {
            Home_Click();
            Level_Click(1);
        }

        public override void Update()
        {
        }
        
        public void Home_Click()
        {
            Debug.Log("Home_Click");
            this.selectNumber = 0;
            this.groupLevel.SetActive(false);
            this.panelDescription2.SetActive(false);

            this.description.text = "ホームタウンでの基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "メインメニュー\r\n";
            this.description.text += "  【ステータス】　　　　　　　キャラクターのステータス画面を表示します。\r\n";
            this.description.text += "  【バトル設定】　　　　　　　戦闘コマンドの設定を行う画面を表示します。\r\n";
            this.description.text += "  【セーブ】　　　　　　　　　ゲームをセーブします。\r\n";
            this.description.text += "  【ロード】　　　　　　　　　ゲームをロードします。\r\n";
            this.description.text += "  【ゲーム終了】　　　　　　　本ゲームを終了します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "イベントメニュー\r\n";
            this.description.text += "  【DungeonPlayer!】　　　　　ダンジョンを開始します。\r\n";
            this.description.text += "  【幼なじみのラナと会話】　　ラナと会話を行います。\r\n";
            this.description.text += "  【天下一品ガンツの武具店】　武具の売買を行います。\r\n";
            this.description.text += "  【ハンナのゆったり宿屋】　　宿屋に宿泊します。\r\n";
        }

        public void Dungeon_Click()
        {
            Debug.Log("Dungeon_Click");
            this.selectNumber = 1;
            this.groupLevel.SetActive(false);
            this.panelDescription2.SetActive(false);

            this.description.text = "ダンジョンの基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "基本操作\r\n";
            this.description.text += "　画面上部をタップ　　        上へ移動します。\r\n";
            this.description.text += "　画面左部をタップ　　        左へ移動します。\r\n";
            this.description.text += "　画面右部をタップ　　        右へ移動します。\r\n";
            this.description.text += "　画面下部をタップ　　        下へ移動します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "サブメニュー\r\n";
            this.description.text += "  【ブック】アイコン　　　    本ゲームで登場する戦闘コマンドの説明を表示します。\r\n";
            this.description.text += "  【トーク】アイコン　　　    本編の会話内容を履歴表示します。\r\n";
            this.description.text += "  【マップ】アイコン　　　    ダンジョンを表示モードに切り替えます。\r\n";
            this.description.text += "  【 水晶 】アイコン　　　    ダンジョンからホームタウンへ帰還します。\r\n";
            this.description.text += "  【 索敵 】アイコン　　　    モンスターを索敵するモードに切り替えます。\r\n";
            this.description.text += "  \r\n";
            this.description.text += "メインメニュー\r\n";
            this.description.text += "  （ホームタウンのメニューと同等です。)\r\n";
        }

        public void Battle_Click()
        {
            Debug.Log("Battle_Click");
            this.selectNumber = 2;
            this.groupLevel.SetActive(true);
            this.panelDescription2.SetActive(true);

            this.description.text = "戦闘時の基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "アクションコマンド\r\n";
            this.description.text += "　【戦闘コマンド】リスト　　　戦闘コマンドから行動させるアクションを決定します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "メニュー\r\n";
            this.description.text += "　【アイテム】アイコン　　　　アイテムを使用します。\r\n";
            this.description.text += "　【バトル設定】アイコン　　　戦闘コマンドを再設定します。\r\n";
            this.description.text += "　【逃げる】アイコン　　　　　戦闘から離脱します。\r\n";
            this.description.text += "　【一時停止】アイコン　　　　戦闘を一時停止させます。\r\n";
            this.description.text += "\r\n";
            this.description.text += "その他\r\n";
            this.description.text += "　【戦闘速度】ゲージバー　　　戦闘速度を変更します。\r\n";
            this.description.text += "　【戦闘履歴】ウィンドウ　　　戦闘イベントの履歴を確認できます。\r\n";
        }

        public void Status_Click()
        {
            Debug.Log("Status_Click");
            this.selectNumber = 3;
            this.groupLevel.SetActive(false);
            this.panelDescription2.SetActive(false);

            this.description.text = "キャラクターステータス画面での基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "タブ一覧\r\n";
            this.description.text += "　【ステータス】　　　　　　　キャラクターのコアパラメタ、装備、戦闘値を表示します。\r\n";
            this.description.text += "　【バックパック】　　　　　　現在保持しているアイテムリストを表示します。\r\n";
            this.description.text += "　【スペル】　　　　　　　　　非戦闘時に使用可能な魔法を表示します。\r\n";
            this.description.text += "　【レジスト】　　　　　　　　キャラクターの各種耐性を表示します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "コアパラメタ\r\n";
            this.description.text += "　【力】/【技】/【知】/【体】/【心】\r\n";
            this.description.text += "　　各種コアパラメタの値を表示しています。\r\n";
            this.description.text += "　　レベルアップ時、各種パラメタをタップする事でパラメタをUPさせる事ができます。\r\n";
            this.description.text += "\r\n";
            this.description.text += "装備品\r\n";
            this.description.text += "　【メイン】/【サブ】/【防具】/【アクセサリ１】/【アクセサリ２】\r\n";
            this.description.text += "　　現在装備しているアイテムを表示しています。\r\n";
            this.description.text += "　　各種ボタンをタップする事で装備を変更する事ができます。\r\n";
        }

        public void Duel_Click()
        {
            Debug.Log("Duel_Click");
            this.selectNumber = 5;
            this.groupLevel.SetActive(false);
            this.panelDescription2.SetActive(false);

            this.description.text = "DUEL戦闘時における基本的な操作を練習します。\r\n";
            this.description.text += "\r\n";
            this.description.text += "　DUEL戦においては基本的には通常戦闘と同様に楽しめます。\r\n";
            this.description.text += "　ただし通常戦闘と比べ、いくつかルールの違いがあります。\r\n";
            this.description.text += "　通常とは少し違った感覚でDUEL戦を楽しんでください。\r\n";
            this.description.text += "\r\n";
            this.description.text += "【基本ルール】\r\n";
            this.description.text += "　・必ず１ｖｓ１での戦闘となります。\r\n";
            this.description.text += "　・行動ゲージバーは、一番初め（左部）から開始となります。\r\n";
            this.description.text += "　・DUEL開始後、【一時停止】が選べなくなります。\r\n";
            this.description.text += "　・インスタントで行動を行う際、＜行動スタック＞が画面中央に入ります。\r\n";
            this.description.text += "　・＜行動スタック＞が積まれている間、更に＜行動スタック＞を乗せる事ができます。\r\n";
            this.description.text += "　　一定時間経過した後、一番最後の＜行動スタック＞が一番初めに実行されます。\r\n";
            this.description.text += "\r\n";
            this.description.text += "【Tips】\r\n";
            this.description.text += "　インスタント行動の中には、＜行動スタック＞を打ち消すコマンドも存在します。\r\n";
            this.description.text += "　対戦相手の行動特性をよく見極めて、タイミングよく行動しましょう。\r\n";
        }

        public void Level_Click(int level)
        {
            Debug.Log("Level_Click");
            this.selectLevel = level;
            GroundOne.TutorialLevel = level;
            if (level == 1)
            {
                this.description2.text = "戦闘に関する基本的な事項を確認します。";
            }
            else if (level == 2)
            {
                this.description2.text = "「攻撃」と「防御」をタイミング良く使って、敵を倒します。";
            }
            else if (level == 3)
            {
                this.description2.text = "「フレッシュヒール」や「アイスニードル」などの戦闘コマンドを使用します。";
            }
            else if (level == 4)
            {
                this.description2.text = "「プロテクション」や「クリージング」をインスタントコマンドとして使用します。";
            }
            else if (level == 5)
            {
                this.description2.text = "【DUEL】戦闘における基本ルールの違いと＜行動スタック＞を確認します。";
            }
        }

        public void GoTutorial_Click()
        {
            GroundOne.TutorialMode = true;

            if (this.selectNumber == 0)
            {
                GroundOne.WE.AlreadyRest = true;
                SceneDimension.JumpToTruthHomeTown();
            }
            else if (this.selectNumber == 1)
            {
                GroundOne.MC.FirstName = Database.EIN_WOLENCE;
                GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
                GroundOne.MC.Level = 1;
                GroundOne.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
                GroundOne.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
                GroundOne.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                GroundOne.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
                GroundOne.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
                GroundOne.MC.Dead = false;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

                SceneDimension.JumpToTruthDungeon(false);
            }
            else if (this.selectNumber == 2)
            {
                enemy_click();
            }
            else if (this.selectNumber == 3)
            {
                bool dummy1 = false;
                int dummy2 = 0;
                int dummy3 = 0;

                GroundOne.MC.FirstName = Database.EIN_WOLENCE;
                GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
                GroundOne.MC.Level = 1;
                GroundOne.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
                GroundOne.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
                GroundOne.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                GroundOne.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
                GroundOne.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
                GroundOne.MC.Dead = false;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.RARE_LIFE_SWORD);
                GroundOne.MC.SubWeapon = new ItemBackPack(Database.RARE_ESMERALDA_SHIELD);
                GroundOne.MC.MainArmor = new ItemBackPack(Database.COMMON_GOTHIC_PLATE);
                GroundOne.MC.Accessory = new ItemBackPack(Database.COMMON_COPPER_RING_TORA);
                GroundOne.MC.Accessory2 = new ItemBackPack(Database.RARE_SOUJUTENSHI_NO_GOFU);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_RED_POTION));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.POOR_SMALL_BLUE_POTION));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.COMMON_PURPLE_CHARM));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.RARE_BLUE_LIGHTNING));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.RARE_WILL_HOLY_HAT));
                GroundOne.MC.AddBackPack(new ItemBackPack(Database.RARE_SUN_BRAVE_ARMOR));
                GroundOne.MC.FreshHeal = true;
                SceneDimension.CallTruthStatusPlayer(this, ref dummy1, ref dummy2, ref dummy3, GroundOne.MC.PlayerStatusColor);
            }
            else if (this.selectNumber == 4)
            {
                // DuelはBattleのLevel5へ移行
            }
        }
    
        public void Close_Click()
        {
            GroundOne.TutorialMode = false;
            GroundOne.ReInitializeGroundOne(false);
            Method.ReloadTruthWorldEnvironment();
            SceneDimension.Back(this);
        }

        public void enemy_click()
        {
            GroundOne.MC.FirstName = Database.EIN_WOLENCE;
            GroundOne.MC.FullName = Database.EIN_WOLENCE_FULL;
            GroundOne.SC.FirstName = Database.RANA_AMILIA;
            GroundOne.SC.FullName = Database.RANA_AMILIA_FULL;

            if (this.selectLevel == 1)
            {
                GroundOne.MC.Level = 1;
                GroundOne.MC.Strength = 2;
                GroundOne.MC.Agility = 5;
                GroundOne.MC.Intelligence = 1;
                GroundOne.MC.Stamina = 2;
                GroundOne.MC.Mind = 2;
                GroundOne.MC.Dead = false;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                GroundOne.MC.SubWeapon = null;
                GroundOne.MC.MainArmor = null;
                GroundOne.MC.Accessory = new ItemBackPack(Database.POOR_HINJAKU_ARMRING);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;

                GroundOne.enemyName1 = Database.ENEMY_HIYOWA_BEATLE;
                GroundOne.enemyName2 = String.Empty;
                GroundOne.enemyName3 = String.Empty;
            }
            else if (this.selectLevel == 2)
            {
                GroundOne.MC.Level = 3;
                GroundOne.MC.Strength = 3;
                GroundOne.MC.Agility = 5;
                GroundOne.MC.Intelligence = 1;
                GroundOne.MC.Stamina = 6;
                GroundOne.MC.Mind = 4;
                GroundOne.MC.Dead = false;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_EXCELLENT_BUSTER);
                GroundOne.MC.SubWeapon = null;
                GroundOne.MC.MainArmor = new ItemBackPack(Database.COMMON_GOTHIC_PLATE);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;

                GroundOne.enemyName1 = Database.ENEMY_RED_HOPPER;
                GroundOne.enemyName2 = String.Empty;
                GroundOne.enemyName3 = String.Empty;
            }
            else if (this.selectLevel == 3)
            {
                GroundOne.WE.AvailableSecondCharacter = true;
                GroundOne.MC.Level = 5;
                GroundOne.MC.Strength = 6;
                GroundOne.MC.Agility = 11;
                GroundOne.MC.Intelligence = 5;
                GroundOne.MC.Stamina = 9;
                GroundOne.MC.Mind = 5;
                GroundOne.MC.Dead = false;
                GroundOne.MC.FreshHeal = true;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_EXCELLENT_BUSTER);
                GroundOne.MC.SubWeapon = null;
                GroundOne.MC.MainArmor = new ItemBackPack(Database.COMMON_GOTHIC_PLATE);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.MC.BattleActionCommandList[2] = Database.FRESH_HEAL;
                GroundOne.MC.BattleActionCommandList[3] = Database.STRAIGHT_SMASH;

                GroundOne.SC.Level = 5;
                GroundOne.SC.Strength = 1;
                GroundOne.SC.Agility = 7;
                GroundOne.SC.Intelligence = 30;
                GroundOne.SC.Stamina = 9;
                GroundOne.SC.Mind = 5;
                GroundOne.SC.Dead = false;
                GroundOne.SC.MainWeapon = new ItemBackPack(Database.COMMON_WOOD_ROD);
                GroundOne.SC.SubWeapon = null;
                GroundOne.SC.MainArmor = new ItemBackPack(Database.COMMON_COTTON_ROBE);
                GroundOne.SC.MaxGain();
                GroundOne.SC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.SC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.SC.BattleActionCommandList[2] = Database.ICE_NEEDLE;

                GroundOne.enemyName1 = Database.ENEMY_GIANT_SNAKE;
                GroundOne.enemyName2 = String.Empty;
                GroundOne.enemyName3 = String.Empty;
            }
            else if (this.selectLevel == 4)
            {
                GroundOne.WE.AvailableInstantCommand = true;

                GroundOne.WE.AvailableSecondCharacter = true;
                GroundOne.MC.Level = 10;
                GroundOne.MC.Strength = 50;
                GroundOne.MC.Agility = 30;
                GroundOne.MC.Intelligence = 12;
                GroundOne.MC.Stamina = 45;
                GroundOne.MC.Mind = 7;
                GroundOne.MC.Dead = false;
                GroundOne.MC.FreshHeal = true;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_EXCELLENT_BUSTER);
                GroundOne.MC.SubWeapon = null;
                GroundOne.MC.MainArmor = new ItemBackPack(Database.COMMON_HEAVY_ARMOR);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.MC.BattleActionCommandList[2] = Database.STRAIGHT_SMASH;
                GroundOne.MC.BattleActionCommandList[3] = Database.FRESH_HEAL;
                GroundOne.MC.BattleActionCommandList[4] = Database.PROTECTION;

                GroundOne.SC.Level = 9;
                GroundOne.SC.Strength = 5;
                GroundOne.SC.Agility = 25;
                GroundOne.SC.Intelligence = 45;
                GroundOne.SC.Stamina = 30;
                GroundOne.SC.Mind = 7;
                GroundOne.SC.Dead = false;
                GroundOne.SC.MainWeapon = new ItemBackPack(Database.RARE_AUTUMN_ROD);
                GroundOne.SC.SubWeapon = null;
                GroundOne.SC.MainArmor = new ItemBackPack(Database.COMMON_COTTON_ROBE);
                GroundOne.SC.MaxGain();
                GroundOne.SC.Accessory = new ItemBackPack(Database.RARE_SINTYUU_RING_HAKUTYOU);
                GroundOne.SC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.SC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.SC.BattleActionCommandList[2] = Database.ICE_NEEDLE;
                GroundOne.SC.BattleActionCommandList[3] = Database.CLEANSING;

                GroundOne.enemyName1 = Database.ENEMY_BLOOD_MOSS;
                GroundOne.enemyName2 = Database.ENEMY_BRILLIANT_BUTTERFLY;
                GroundOne.enemyName3 = String.Empty;
            }
            else if (this.selectLevel == 5)
            {
                GroundOne.WE.AvailableInstantCommand = true;
                GroundOne.DuelMode = true;

                GroundOne.MC.Level = 10;
                GroundOne.MC.Strength = 25;
                GroundOne.MC.Agility = 16;
                GroundOne.MC.Intelligence = 7;
                GroundOne.MC.Stamina = 30;
                GroundOne.MC.Mind = 5;
                GroundOne.MC.Dead = false;
                GroundOne.MC.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                GroundOne.MC.SubWeapon = null;
                GroundOne.MC.MainArmor = new ItemBackPack(Database.COMMON_GOTHIC_PLATE);
                GroundOne.MC.MaxGain();
                GroundOne.MC.BattleActionCommandList[0] = Database.ATTACK_EN;
                GroundOne.MC.BattleActionCommandList[1] = Database.DEFENSE_EN;
                GroundOne.MC.BattleActionCommandList[2] = Database.STRAIGHT_SMASH;
                GroundOne.MC.BattleActionCommandList[3] = Database.FRESH_HEAL;
                GroundOne.MC.BattleActionCommandList[4] = Database.PROTECTION;
                GroundOne.MC.BattleActionCommandList[5] = Database.FIRE_BALL;
                GroundOne.MC.BattleActionCommandList[6] = Database.HEAT_BOOST;

                GroundOne.enemyName1 = Database.DUEL_MAGI_ZELKIS;
                GroundOne.enemyName2 = String.Empty;
                GroundOne.enemyName3 = String.Empty;
            }


            SceneDimension.CallTruthBattleEnemy(Database.Title, GroundOne.DuelMode, false, false, false);
        }

    }
}
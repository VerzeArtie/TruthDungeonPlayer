using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace DungeonPlayer
{
    public class TruthBattleSetting : MotherForm
    {
        public Camera cam;
        public GameObject groupArcheType;
        public GameObject btnCharacterGroup;
        public GameObject groupMixCommand;            
        public Button btnFirstChara;
        public Button btnSecondChara;
        public Button btnThirdChara;
        public Text commandName;
        public Text commandNameEn;
        public Text commandCost;
        public Text commandTarget;
        public Text commandTiming;
        public Text description;
        public Image[] pbAction;
        public Image[] pbSorcery;
        public Image[] pbCurrentAction;
        public Image[] pbCurrentActionSorcery;
        public Image moveActionBox;
        public Image moveActionBoxSorcery;
        public GameObject panelBasic;
        public Text txtBasic;
        public GameObject panelSpell;
        public Text txtSpell;
        public GameObject panelSkill;
        public Text txtSkill;
        public GameObject panelClass;
        public Text txtClass;
        public Button btnBasic;
        public Button btnSpell;
        public Button btnExit;
        public Button btnClass;
        public Button command1;
        public Button command2;
        public Button command3;
        public Button command4;
        public Button command5;
        //public Panel commandList;
        public Button back;
        public TruthImage dragObj;

        MainCharacter currentPlayer;
        int currentPlayerNumber = 0;

        private Vector3 screenPoint;
        private Vector3 offset;

        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // 基本行動
        const int MIX_ACTION_NUM = 45; // [警告] 暫定、本来Databaseに記載するべき
        const int MIX_ACTION_NUM_2 = 30; // [警告]暫定、本来Databaseに記載するべき
        const int ARCHETYPE_NUM = 1; // アーキタイプ

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            this.currentPlayer = GroundOne.MC;
            this.currentPlayerNumber = 0;
            this.cam.backgroundColor = GroundOne.MC.PlayerStatusColor;

            if (GroundOne.MC != null) { btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor; }
            if (GroundOne.SC != null) { btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor; }
            if (GroundOne.TC != null) { btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor; }

            if (GroundOne.BattleEnemyFilter != null)
            {
                GroundOne.BattleEnemyFilter.SetActive(true);
                GroundOne.BattleEnemyFilter.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            }

            this.groupMixCommand.SetActive(GroundOne.WE.AvailableMixSpellSkill);

            this.groupArcheType.SetActive(GroundOne.WE.AvailableArchetypeCommand);

            if (GroundOne.DuelMode)
            {
                btnCharacterGroup.SetActive(false);
            }
            else
            {
                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    btnCharacterGroup.SetActive(true);
                    btnFirstChara.gameObject.SetActive(true);
                    btnSecondChara.gameObject.SetActive(true);
                    if (GroundOne.WE.AvailableThirdCharacter)
                    {
                        btnThirdChara.gameObject.SetActive(true);
                    }
                    else
                    {
                        Method.AddEmptyObj(ref btnCharacterGroup, 1);
                    }
                }
                else
                {
                    btnCharacterGroup.SetActive(false);
                }
            }

            SetupAllIcon();
        }
        
        public void TruthBattleSetting_MouseMove(Button sender)
        {
            moveActionBox.gameObject.SetActive(true);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        public void TruthBattleSetting_MouseUp(Button sender)
        {
            moveActionBox.gameObject.SetActive(false);

            float positionX = Input.mousePosition.x;
            float positionY = Input.mousePosition.y;

            for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
            {
                if (pbCurrentAction[ii].gameObject.transform.position.x <= positionX && positionX <= pbCurrentAction[ii].gameObject.transform.position.x + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.width &&
                    pbCurrentAction[ii].gameObject.transform.position.y <= positionY && positionY <= pbCurrentAction[ii].gameObject.transform.position.y + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.height)
                {
                    pbCurrentAction[ii].sprite = sender.image.sprite;
                    pbCurrentAction[ii].name = sender.name;
                    if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Sorcery)
                    {
                        pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.SorceryIcon);
                    }
                    else if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Normal)
                    {
                        pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.NormalIcon);
                    }
                    else
                    {
                        pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.InstantIcon);
                    }

                    this.currentPlayer.BattleActionCommandList[ii] = sender.name;
                    break;
                }
            }
        }

        int adjustX = 0;
        int adjustY = 0;
        public void TruthBattleSetting_MouseDown(Button sender)
        {
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            moveActionBox.sprite = sender.image.sprite;
            moveActionBox.name = sender.name;

            //moveActionBox.sprite = Resources.Load<Sprite>(ssName[ii]);
            if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Sorcery)
            {
                moveActionBoxSorcery.sprite = Resources.Load<Sprite>(Database.SorceryIcon);
            }
            else if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Normal)
            {
                moveActionBoxSorcery.sprite = Resources.Load<Sprite>(Database.NormalIcon);
            }
            else
            {
                moveActionBoxSorcery.sprite = Resources.Load<Sprite>(Database.InstantIcon);
            }


            moveActionBox.gameObject.SetActive(true);
        }

        void OnMouseDown()
        {
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        void OnMouseDrag()
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            transform.position = currentPosition;
        }

        void OnMouseUp()
        {
        }

        public void tapExit()
        {
            SceneDimension.Back(this);
        }

        public void ViewCommandContent(Button sender)
        {
            string command = sender.name;
            this.commandName.text = TruthActionCommand.ConvertToJapanese(command);
            this.commandNameEn.text = sender.name;
            this.commandCost.text = "消費コスト : " + TruthActionCommand.GetCost(sender.name).ToString();

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    commandTarget.text = "対象：場全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    commandTarget.text = "対象：味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    commandTarget.text = "対象：味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    commandTarget.text = "対象：敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    commandTarget.text = "対象：敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    commandTarget.text = "対象：敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    commandTarget.text = "対象：インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    commandTarget.text = "対象：なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    commandTarget.text = "対象：自分";
                    break;
            }

            switch (TruthActionCommand.GetTimingType(sender.name))
            {
                case TruthActionCommand.TimingType.Sorcery:
                    commandTiming.text = "ソーサリー";
                    break;
                case TruthActionCommand.TimingType.Normal:
                    commandTiming.text = "ノーマル";
                    break;
                case TruthActionCommand.TimingType.Instant:
                    commandTiming.text = "インスタント";
                    break;
            }
            this.description.text = TruthActionCommand.GetDescription(command);
        }

        private void SetupAllIcon()
        {
            string fileExt = ".bmp";
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            bool[] ssAvailable = TruthActionCommand.GetAvailableActionList(currentPlayer);
            for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                try { 
                    if (ssAvailable[ii]) {
                        pbAction[ii].gameObject.SetActive(true);
                        pbAction[ii].sprite = Resources.Load<Sprite>(ssName[ii]);
                        if (TruthActionCommand.GetTimingType(ssName[ii]) == TruthActionCommand.TimingType.Sorcery) {
                            pbSorcery[ii].sprite = Resources.Load<Sprite>(Database.SorceryIcon);
                        } else if (TruthActionCommand.GetTimingType(ssName[ii]) == TruthActionCommand.TimingType.Normal) {
                            pbSorcery[ii].sprite = Resources.Load<Sprite>(Database.NormalIcon);
                        } else {
                            pbSorcery[ii].sprite = Resources.Load<Sprite>(Database.InstantIcon);
                        }
                    }
                    else
                    {
                        pbAction[ii].gameObject.SetActive(false);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            for (int ii = 0; ii < currentPlayer.BattleActionCommandList.Length; ii++)
            {
                pbCurrentAction[ii].sprite = Resources.Load<Sprite>(currentPlayer.BattleActionCommandList[ii]);
                pbCurrentAction[ii].name = currentPlayer.BattleActionCommandList[ii];
                if (TruthActionCommand.GetTimingType(currentPlayer.BattleActionCommandList[ii]) == TruthActionCommand.TimingType.Sorcery)
                {
                    pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.SorceryIcon);
                }
                else if (TruthActionCommand.GetTimingType(currentPlayer.BattleActionCommandList[ii]) == TruthActionCommand.TimingType.Normal)
                {
                    pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.NormalIcon);
                }
                else
                {
                    pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>(Database.InstantIcon);
                }
            }
        }
        
        public void FirstChara_Click()
        {
            this.currentPlayer = GroundOne.MC;
            this.currentPlayerNumber = 0;
            this.cam.backgroundColor = GroundOne.MC.PlayerStatusColor;
            if (GroundOne.BattleEnemyFilter != null)
            {
                GroundOne.BattleEnemyFilter.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            }
            SetupAllIcon();
        }

        public void SecondChara_Click()
        {
            this.currentPlayer = GroundOne.SC;
            this.currentPlayerNumber = 1;
            this.cam.backgroundColor = GroundOne.SC.PlayerStatusColor;
            if (GroundOne.BattleEnemyFilter != null)
            {
                GroundOne.BattleEnemyFilter.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            }
            SetupAllIcon();
        }

        public void ThirdChara_Click()
        {
            this.currentPlayer = GroundOne.TC;
            this.currentPlayerNumber = 2;
            this.cam.backgroundColor = GroundOne.TC.PlayerStatusColor;
            if (GroundOne.BattleEnemyFilter != null)
            {
                GroundOne.BattleEnemyFilter.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            }
            SetupAllIcon();
        }
    }
}
